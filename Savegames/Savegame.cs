using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoaSaveEditor.Savegames
{
    internal class Savegame : ISavegame
    {
        private readonly IMainFormAPI m_AppAPI;

        /// <summary>
        /// Full path, on disk, of the backing file.
        /// </summary>
        private readonly string m_Filename;
        private FileInfo m_FileInfo;

        /// <summary>
        /// Bytes read from disk
        /// </summary>
        private byte[] m_FileBytes;

        /// <summary>
        /// Minimum .sav file length we consider. 
        /// @NMTODO - change this from a guesstimate? Needs to be s_RequiredHeaderBytes.Length + sizeof(int64) (time played)
        ///  + 5x PStrings(leadign 32-bit length). Not yet sure what other fields are mandatory
        /// </summary>
        private const int MIN_SAVE_FILE_SIZE = 40;

        /// <summary>
        /// Constant for how many bytes these take. @NMTODO - is this defined anywhere else?
        /// </summary>
        private const int SIZEOF_BYTE = 1;
        private const int SIZEOF_INT32 = 4;
        private const int SIZEOF_INT64 = 8;
        private const int SIZEOF_LONG = 8;

        /// <summary>
        /// How long the user's been playing (from header)
        /// </summary>
        public TimeSpan ElapsedPlaytime { get; private set; } = TimeSpan.Zero;


        /// <summary>
        /// When the savefile was saved.
        /// </summary>
        public DateTime FileSaveTime { get; private set; } = DateTime.MinValue;

        /// <summary>
        /// Playername from the header
        /// </summary>
        public string PlayerName { get; private set; } = string.Empty;

        /// <summary>
        /// Player level
        /// </summary>
        public int PlayerLevel { get { return m_PlayerLevelByte; } }

        /// <summary>
        /// Player location from the header
        /// </summary>
        public string PlayerLocation { get; private set; } = string.Empty;

        /// <summary>
        /// Player class from the header
        /// </summary>
        public string PlayerClass { get; private set; } = string.Empty;

        /// <summary>
        /// Player quest
        /// </summary>
        public string PlayerQuest { get; private set; } = string.Empty;

        /// <summary>
        /// Player specialization, e.g. "Int_Generic"
        /// </summary>
        public string PlayerSpecialization { get; private set; } = string.Empty;

        /// <summary>
        /// Disk filename. 
        /// </summary>
        public string FullLocalPath { get { return m_Filename; } }

        /// <summary>
        /// Debug text for analysis
        /// </summary>
        public string DebugText { get; private set; } = string.Empty;

        /// <summary>
        /// Initial bytes that must be present.
        /// </summary>
        private static readonly byte[] s_RequiredHeaderBytes = { 0xc4, 0xdb, 0xd8, 0x00 };

        /// <summary>
        /// This pattern seems to repeat 6 out of every 12 bytes into the file. Purpose/use: unsure.
        /// </summary>
        private static readonly byte[] s_RepeatingPatternBytes = { 0xf2, 0x44, 0x00, 0x04, 0x00, 0x00 };

        /// <summary>
        /// Initial offset seen in header's m_Bytes[0x04]. Purpose not yet known.
        /// </summary>
        private int m_InitialOffset;

        // Offset in m_Bytes to the byte just after the Player* strings.
        private int m_PostPlayerStringOffset;

        /// <summary>
        /// Player level. Seems to be a byte, only.
        /// </summary>
        private int m_PlayerLevelByte;

        // The 48 bytes after m_PlayerLevelByte seem to not follow the pattern of the subsequent section.
        // These 48 bytes don't seem to be all 24-bit values like the subsequent section.
        private int[] m_PostPlayerBlock = new int[(16 / SIZEOF_INT32) * 3];

        internal Savegame(IMainFormAPI appAPI, string filename)
        {
            Debug.Assert(appAPI != null);
            m_AppAPI = appAPI;

            Debug.Assert(!string.IsNullOrWhiteSpace(filename));
            m_Filename = filename;
        }

        /// <summary>
        /// Tries to load from m_Filename. Returns false on errors.
        /// </summary>
        /// <returns></returns>
        internal bool TryLoad()
        {
            bool bSuccess = false;
            try
            {
                // Very early out if no filename or can't find file
                if (string.IsNullOrWhiteSpace(m_Filename) || !File.Exists(m_Filename))
                {
                    return bSuccess;
                }

                m_FileBytes = File.ReadAllBytes(m_Filename);
                m_FileInfo = new FileInfo(m_Filename);
                FileSaveTime = m_FileInfo.LastWriteTime;

                bSuccess = m_FileBytes.Length > MIN_SAVE_FILE_SIZE;
                if (!bSuccess)
                {
                    return bSuccess;
                }

                // Assume success for now, unless header mismatch
                for (int i = 0; i < s_RequiredHeaderBytes.Length; ++i)
                {
                    if (s_RequiredHeaderBytes[i] != m_FileBytes[i])
                    {
                        m_AppAPI.LogLine(string.Format("Filename '{0}', header byte #{1} mismatch, {2} != {3}", m_Filename, i, s_RequiredHeaderBytes[i], m_FileBytes[i]));
                        bSuccess = false;
                        return bSuccess;
                    }
                }

                // Header passed. Now try and read out fields.
                int offset = s_RequiredHeaderBytes.Length;


                // Not sure what this byte is, seems to be always 0. 
                int versionByte = Read8Bits(ref offset);
                RequireByte(versionByte, 0x0A, offset);

                m_InitialOffset = Read32Bits(ref offset);
                ReadTimePlayed(ref offset);
                PlayerName = ReadPString32(ref offset);
                PlayerLocation = ReadPString32(ref offset);
                PlayerClass = ReadPString32(ref offset);
                PlayerQuest = ReadPString32(ref offset);
                PlayerSpecialization = ReadPString32(ref offset);

                m_PlayerLevelByte = Read8Bits(ref offset);

                // And then the 48 bytes after that that seem unlike the following section
                for (int i = 0; i < m_PostPlayerBlock.Length; ++i)
                {
                    m_PostPlayerBlock[i] = Read32Bits(ref offset);
                }

                // Store for later...
                m_PostPlayerStringOffset = offset;
                DebugText = string.Format("Init 0x{0:X} postPlayer=0x{1:X}", m_InitialOffset, m_PostPlayerStringOffset);

                // Find end of this next block.
                int endOffset = FirstOffsetOf(s_RepeatingPatternBytes, offset);
                if(endOffset < 0)
                {
                    DebugText = "Could not find s_RepeatingPatternBytes";
                    m_AppAPI.LogLine(string.Format("Error: could not find s_RepeatingPatternBytes starting at offset {0} in file", offset));
                }
                else
                {
                    // Seem to have a section of 24-bit values here. Ensure that.
                    int blockCount = (endOffset - offset) / SIZEOF_INT32;
                    // DebugText = string.Format("0x{0:X} ({0}) to s_RepeatingPatternBytes. Count={1}", endOffset, blockCount);
                    for (int i=0;i<blockCount;++i)
                    {
                        int value = Read32Bits(ref offset);
                        bool sane = WarnIfIntIsNotWithin(value, 0, 16777216, offset);
                        if(!sane)
                        {
                            break; // No further logging for this file.
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                m_AppAPI.LogException(ex, string.Format("TryLoad, m_Filename='{0}'", m_Filename));
            }
            return bSuccess;
        }


        /// <summary>
        /// Attempts to read 8 bits.  Adjusts offset to the read position afterwards.
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        private int Read8Bits(ref int offset)
        {
            int finalOffset = offset + SIZEOF_BYTE;
            if (finalOffset > m_FileBytes.Length)
            {
                throw new IndexOutOfRangeException();
            }

            int ret = BitConverter.ToChar(m_FileBytes, offset);
            ret &= 0xFF; // Trim to 1 byte
            offset = finalOffset;
            return ret;
        }

        /// <summary>
        /// Attempts to read 32 bits.  Adjusts offset to the read position afterwards.
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        private int Read32Bits(ref int offset)
        {
            int finalOffset = offset + SIZEOF_INT32;
            if (finalOffset > m_FileBytes.Length)
            {
                throw new IndexOutOfRangeException();
            }

            int ret = BitConverter.ToInt32(m_FileBytes, offset);
            offset = finalOffset;
            return ret;
        }

        /// <summary>
        /// Attempts to read 64 bits.  Adjusts offset to the read position afterwards.
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        private long Read64Bits(ref int offset)
        {
            int finalOffset = offset + SIZEOF_LONG;
            if (finalOffset > m_FileBytes.Length)
            {
                throw new IndexOutOfRangeException();
            }

            long ret = BitConverter.ToInt64(m_FileBytes, offset);
            offset = finalOffset;
            return ret;
        }

        /// <summary>
        /// Attempts to read the time played. Adjusts offset to the read position afterwards.
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        private void ReadTimePlayed(ref int offset)
        {
            long readMS = Read64Bits(ref offset);
            ElapsedPlaytime = TimeSpan.FromMilliseconds(readMS);
        }

        /// <summary>
        /// Attempts to read a 'Pascal' (length upfront, in 32 bits) string and return that. Adjusts offset to the new
        /// position in the file after this is read.
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        private string ReadPString32(ref int offset)
        {
            int stringLen = Read32Bits(ref offset);
            int finalOffset = offset + stringLen;
            if (finalOffset > m_FileBytes.Length)
            {
                throw new IndexOutOfRangeException();
            }

            string retString = string.Empty;
            // @NMTODO - is there a better/safer/faster way to grab a block of bytes to string?
            // C#'s char is utf16, need to read 
            for (int i = 0; i < stringLen; ++i)
            {
                int tempInt = Read8Bits(ref offset);
                char tempChar = (char)(tempInt); // Masking of (& 0xFF) done by Read8Bits
                retString += tempChar;
            }
            return retString;
        }


        /// <summary>
        /// If value != expectedValue, bombs out.
        /// Note: this should be called right after reading a byte; the offset passed in should
        /// be after reading, i.e. need to back up to get actual index.
        /// </summary>
        /// <param name="unknownByte"></param>
        /// <param name="v"></param>
        /// <param name="offset"></param>
        private void RequireByte(int value, int expectedValue, int offset)
        {
            Debug.Assert(value == expectedValue);
            if (value != expectedValue)
            {
                m_AppAPI.LogLine(string.Format("Invalid byte {0} != expected byte {1} at offset {2}", value, expectedValue, offset - SIZEOF_BYTE));
                throw new InvalidDataException();
            }
        }

        /// <summary>
        /// Like RequireByte, but warns if the specified byte is not the expected value
        /// Returns true if value is expected, false if not.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="expectedValue"></param>
        /// <param name="offset"></param>
        private bool WarnIfByteIsNot(int value, int expectedValue, int offset)
        {
            if (value != expectedValue)
            {
                m_AppAPI.LogLine(string.Format("Warning byte 0x{0:X) ({0}) != expected byte 0x{1:X}({1}) at offset 0x{2:X} ({2}). Filename '{3}'", value, expectedValue, offset - SIZEOF_BYTE, m_Filename));
                return false;
            }
            return true;
        }

        /// <summary>
        /// Like RequireByte, but warns if the specified int is not the expected value
        /// Returns true if value is expected, false if not.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="expectedValue"></param>
        /// <param name="offset"></param>
        private bool WarnIfIntIsNot(int value, int expectedValue, int offset)
        {
            if (value != expectedValue)
            {
                m_AppAPI.LogLine(string.Format("Warning int 0x{0:X) ({0}) != expected int 0x{1:X}({1}) at offset 0x{2:X} ({2}). Filename '{3}'", value, expectedValue, offset - SIZEOF_INT32, m_Filename));
                return false;
            }
            return true;
        }

        /// <summary>
        /// Like RequireByte, but warns if the specified int is not in [minValue .. maxValue)
        /// Returns true if value is expected, false if not.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="expectedValue"></param>
        /// <param name="offset"></param>
        private bool WarnIfIntIsNotWithin(int value, int minValue, int maxValue, int offset)
        {
            if ((value < minValue) || (value >= maxValue))
            {
                m_AppAPI.LogLine(string.Format("Warning int 0x{0:X} ({0}) not within [0x{1:X} ({1}) .. 0x{2:X} ({2})) at offset 0x{3:X} ({3}). Filename '{4}'", 
                    value, minValue, maxValue, offset - SIZEOF_INT32, Path.GetFileNameWithoutExtension(m_Filename)));
                return false;
            }

            return true;
        }

        /// <summary>
        /// If value != expectedValue, bombs out.
        /// Note: this should be called right after reading a byte; the offset passed in should
        /// be after reading, i.e. need to back up to 
        /// </summary>
        /// <param name="unknownByte"></param>
        /// <param name="v"></param>
        /// <param name="offset"></param>
        private void RequireInt(int value, int expectedValue, int offset)
        {
            Debug.Assert(value == 0);
            if (value != expectedValue)
            {
                m_AppAPI.LogLine(string.Format("Invalid byte {0} != expected byte {1} at offset {2}", value, expectedValue, offset - SIZEOF_INT32));
                throw new InvalidDataException();
            }
        }


        /// <summary>
        /// Tries to find the first existence of the specified pattern in the file, starting at the given pattern
        /// Returns < 0 if not found.
        /// </summary>
        /// <param name="s_RepeatingPatternBytes"></param>
        /// <returns></returns>
        private int FirstOffsetOf(byte[] pattern, int startOffset = 0)
        {
            int patternLen = pattern.Length;
            int overallLen = m_FileBytes.Length;
            for (int i = startOffset; i < (overallLen - patternLen); ++i)
            {
                bool bFound = true; // Until proven otherwise by the j loop
                for (int j = 0; j < patternLen; ++j)
                {
                    if (pattern[j] != m_FileBytes[i + j])
                    {
                        bFound = false;
                        break;
                    }
                }

                if (bFound)
                {
                    return i;
                }
            }

            return -1; // Not found
        }

    }
}
