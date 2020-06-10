using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoaSaveEditor
{
    interface ISavegame
    {
        /// <summary>
        /// How long the user's been playing (from header)
        /// </summary>
        TimeSpan ElapsedPlaytime { get; }

        /// <summary>
        /// When the savefile was saved.
        /// </summary>
        DateTime FileSaveTime { get; }

        /// <summary>
        /// Playername from the header
        /// </summary>
        string PlayerName { get; }

        /// <summary>
        /// Player location from the header
        /// </summary>
        string PlayerLocation { get; }

        /// <summary>
        /// Player class from the header
        /// </summary>
        string PlayerClass { get; }

        /// <summary>
        /// Player quest
        /// </summary>
        string PlayerQuest { get; }

        /// <summary>
        /// Player specialization, e.g. "Int_Generic"
        /// </summary>
        string PlayerSpecialization { get; }

        /// <summary>
        /// Disk filename. 
        /// </summary>
        string FullLocalPath { get; }

    }
}
