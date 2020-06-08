using KoaSaveEditor.Savegames;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KoaSaveEditor
{
    public partial class Mainform : Form , IMainFormAPI
    {
        private readonly SavegameManager m_SaveGameManager;

        public Mainform()
        {
            InitializeComponent();
            try
            {
                m_SaveGameManager = new SavegameManager(this);
            }
            catch(Exception ex)
            {
                LogException(ex, "MainForm constructor");
            }
        }

        private void scanForSavesButton_Click(object sender, EventArgs e)
        {
            try
            {
                m_SaveGameManager.ScanForSaves();
                m_SaveGameManager.AddSavesToUI(savegameCombobox);
            }
            catch(Exception ex)
            {
                LogException(ex, "scanForSavesButton_Click");
            }
        }


        private void loadSavegameButton_Click(object sender, EventArgs e)
        {
            try
            {
                LogLine("NMTODO - implement loadSavegameButton_Click");
            }
            catch (Exception ex)
            {
                LogException(ex, "scanForSavesButton_Click");
            }
        }


        #region IMainFormAPI
        /// <summary>
        /// Adds to the main form's output pane
        /// </summary>
        /// <param name="s"></param>
        public void LogLine(string s)
        {
            if(!s.EndsWith(Environment.NewLine))
            {
                s = string.Format("{0}{1}", s, Environment.NewLine);
            }
            if (outputPane != null)
            {
                outputPane.AppendText(s);
            }
        }

        public void LogException(Exception ex, string context = "")
        {
            LogLine(string.Format("Exception in {0}.{1}Context:{1}{2}", context, Environment.NewLine, ex.ToString()));
        }

        /// <summary>
        /// Returns the main form. Useful for *Invoke*() calls.
        /// </summary>
        /// <returns></returns>
        public Form GetMainForm()
        {
            return this;
        }
        #endregion

    }
}
