using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoaSaveEditor
{
    /// <summary>
    /// API for the main form. Can be passed to helper classes, etc.
    /// </summary>
    interface IMainFormAPI
    {
        /// <summary>
        /// Adds to the main form's output pane
        /// </summary>
        /// <param name="s"></param>
        void LogLine(string s);

        void LogException(Exception ex, string context = "");

    }
}
