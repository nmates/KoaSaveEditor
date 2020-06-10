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
        /// Initial bytes that must be present.
        /// </summary>
        private static readonly byte[] s_RequiredHeaderBytes = { 0xc4, 0xdb, 0xd8, 0x00 }; 

        /// <summary>
        /// Initial offset at offset 0x04 in m_Bytes. Purpose not yet known.
        /// </summary>
        private int m_InitialOffset;

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
                m_InitialOffset = Read32Bits(ref offset);

                // Not sure what this byte is, seems to be always 0. 
                int unknownByte = Read8Bits(ref offset);
                ValidateByte(unknownByte, 0, offset);

                ReadTimePlayed(ref offset);
                PlayerName = ReadPString32(ref offset);
                PlayerLocation = ReadPString32(ref offset);
                PlayerClass = ReadPString32(ref offset);
                PlayerQuest = ReadPString32(ref offset);
                PlayerSpecialization = ReadPString32(ref offset);
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
            for(int i=0;i<stringLen;++i)
            {
                int tempInt = Read8Bits(ref offset);
                char tempChar = (char)(tempInt & 0xFF);
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
        private void ValidateByte(int value, int expectedValue, int offset)
        {
            Debug.Assert(value == 0);
            if (value != expectedValue)
            {
                m_AppAPI.LogLine(string.Format("Invalid byte {0} != expected byte {1} at offset {2}", value, expectedValue, offset - SIZEOF_BYTE));
                throw new InvalidDataException();
            }
        }

        /// <summary>
        /// If value != expectedValue, bombs out.
        /// Note: this should be called right after reading a byte; the offset passed in should
        /// be after reading, i.e. need to back up to 
        /// </summary>
        /// <param name="unknownByte"></param>
        /// <param name="v"></param>
        /// <param name="offset"></param>
        private void ValidateInt(int value, int expectedValue, int offset)
        {
            Debug.Assert(value == 0);
            if (value != expectedValue)
            {
                m_AppAPI.LogLine(string.Format("Invalid byte {0} != expected byte {1} at offset {2}", value, expectedValue, offset - SIZEOF_INT32));
                throw new InvalidDataException();
            }
        }

    }
}
