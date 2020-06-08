using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        /// <summary>
        /// Logs an exception, with an optional context message
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="context"></param>
        void LogException(Exception ex, string context = "");

        /// <summary>
        /// Returns the main form. Useful for *Invoke*() calls.
        /// </summary>
        /// <returns></returns>
        Form GetMainForm();

    }
}
