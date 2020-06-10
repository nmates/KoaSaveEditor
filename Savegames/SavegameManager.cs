using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoaSaveEditor.Savegames
{
    /// <summary>
    /// Manager for savegames
    /// </summary>
    internal class SavegameManager : ISaveGameManager
    {

        /// <summary>
        /// If files are under here, will be in format C:\Program Files (x86)\Steam\userdata\{STEAM_USERID}\102500\remote
        /// 102500 is the steam app ID for KOA:Reckoning, can see that at https://steamspy.com/app/102500
        /// </summary>
        const string STEAM_BASE_SAVEGAME_FOLDER = @"C:\Program Files (x86)\Steam\userdata";
        const string STEAM_SUFFIX_FOLDER = @"102500\remote";

        // @NMTODO - reportedly, some people have saves under %USERPROFILE\Documents\My Games\Reckoning 
        // Scan under there too, ideally after getting full confirmation of save dir.

        private readonly IMainFormAPI m_AppAPI;
        private IList<ISavegame> m_Savegames = new List<ISavegame>();

        internal SavegameManager(IMainFormAPI appAPI)
        {
            Debug.Assert(appAPI != null);
            m_AppAPI = appAPI;
        }


        /// <summary>
        /// Scans for saves. Returns true if anything was found, or false on any error.
        /// </summary>
        /// <returns></returns>
        public bool ScanForSaves()
        {
            bool bSuccess = false;
            m_Savegames.Clear();
            try
            {
                bSuccess = LoadSteamSaves();
            }
            catch (Exception ex)
            {
                m_AppAPI.LogException(ex, "ScanForSaves");
            }
            return bSuccess;
        }

        /// <summary>
        /// Quick accessor - returns how many .sav files known
        /// </summary>
        /// <returns></returns>
        public int GetNumSaves()
        {
            return m_Savegames.Count;
        }

        /// <summary>
        /// Tries and load steam saves. They'd be STEAM_BASE_SAVEGAME_FOLDER\*\STEAM_SUFFIX_FOLDER , if present
        /// </summary>
        /// <returns></returns>
        private bool LoadSteamSaves()
        {
            bool ret = false;
            if (!Directory.Exists(STEAM_BASE_SAVEGAME_FOLDER))
            {
                return ret;
            }

            // Need to see if STEAM_BASE_SAVEGAME_FOLDER\*\STEAM_SUFFIX_FOLDER exists.
            string[] subDirs = Directory.GetDirectories(STEAM_BASE_SAVEGAME_FOLDER);
            foreach (string subdir in subDirs)
            {
                string fullPath = Path.Combine(subdir, STEAM_SUFFIX_FOLDER);
                if (Directory.Exists(fullPath))
                {
                    ret = LoadSavesFromFolder(fullPath);
                }
            }

            return ret;
        }

        /// <summary>
        /// Attempts to load a list of saves from the specified path.
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        private bool LoadSavesFromFolder(string fullPath)
        {
            bool ret = false;
            if (!Directory.Exists(fullPath))
            {
                return ret;
            }

            string[] savFiles = Directory.GetFiles(fullPath, "*.sav");
            foreach (string filename in savFiles)
            {
                Savegame tempSave = new Savegame(m_AppAPI, filename);
                if (tempSave.TryLoad())
                {
                    m_Savegames.Add(tempSave);
                }
            }

            return ret;
        }


        /// <summary>
        /// Iterator for savegames. Useful for UI, etc.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ISavegame> GetSaves()
        {
            return m_Savegames; //.GetEnumerator();
        }

    }
}
