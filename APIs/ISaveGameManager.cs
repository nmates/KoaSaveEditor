using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoaSaveEditor
{
    /// <summary>
    /// API for savegame management
    /// </summary>
    interface ISaveGameManager
    {
        /// <summary>
        /// Scans for saves. Returns true if anything was found, or false on any error.
        /// </summary>
        /// <returns></returns>
        bool ScanForSaves();

        /// <summary>
        /// Quick accessor - returns how many .sav files known
        /// </summary>
        /// <returns></returns>
        int GetNumSaves();

        /// <summary>
        /// Iterator for savegames. Useful for UI, etc.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ISavegame> GetSaves();
    }
}
