namespace KoaSaveEditor
{
    partial class Mainform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.welcomeLabel = new System.Windows.Forms.Label();
            this.savegameCombobox = new System.Windows.Forms.ComboBox();
            this.saveNameLabel = new System.Windows.Forms.Label();
            this.scanForSavesButton = new System.Windows.Forms.Button();
            this.loadSavegameButton = new System.Windows.Forms.Button();
            this.mainTabHolder = new System.Windows.Forms.TabControl();
            this.pickSaveTab = new System.Windows.Forms.TabPage();
            this.prefsTab = new System.Windows.Forms.TabPage();
            this.outputPane = new System.Windows.Forms.TextBox();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.mainTabHolder.SuspendLayout();
            this.pickSaveTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // welcomeLabel
            // 
            this.welcomeLabel.AutoSize = true;
            this.welcomeLabel.Location = new System.Drawing.Point(6, 3);
            this.welcomeLabel.Name = "welcomeLabel";
            this.welcomeLabel.Size = new System.Drawing.Size(236, 13);
            this.welcomeLabel.TabIndex = 0;
            this.welcomeLabel.Text = "Welcome to KOA Save Editor. It will get better....";
            // 
            // savegameCombobox
            // 
            this.savegameCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.savegameCombobox.FormattingEnabled = true;
            this.savegameCombobox.Location = new System.Drawing.Point(85, 44);
            this.savegameCombobox.Name = "savegameCombobox";
            this.savegameCombobox.Size = new System.Drawing.Size(198, 21);
            this.savegameCombobox.TabIndex = 1;
            // 
            // saveNameLabel
            // 
            this.saveNameLabel.AutoSize = true;
            this.saveNameLabel.Location = new System.Drawing.Point(6, 47);
            this.saveNameLabel.Name = "saveNameLabel";
            this.saveNameLabel.Size = new System.Drawing.Size(61, 13);
            this.saveNameLabel.TabIndex = 2;
            this.saveNameLabel.Text = "Savegame:";
            // 
            // scanForSavesButton
            // 
            this.scanForSavesButton.Location = new System.Drawing.Point(307, 42);
            this.scanForSavesButton.Name = "scanForSavesButton";
            this.scanForSavesButton.Size = new System.Drawing.Size(99, 23);
            this.scanForSavesButton.TabIndex = 3;
            this.scanForSavesButton.Text = "Scan for saves";
            this.scanForSavesButton.UseVisualStyleBackColor = true;
            this.scanForSavesButton.Click += new System.EventHandler(this.scanForSavesButton_Click);
            // 
            // loadSavegameButton
            // 
            this.loadSavegameButton.Location = new System.Drawing.Point(85, 72);
            this.loadSavegameButton.Name = "loadSavegameButton";
            this.loadSavegameButton.Size = new System.Drawing.Size(75, 23);
            this.loadSavegameButton.TabIndex = 4;
            this.loadSavegameButton.Text = "Load";
            this.loadSavegameButton.UseVisualStyleBackColor = true;
            this.loadSavegameButton.Click += new System.EventHandler(this.loadSavegameButton_Click);
            // 
            // mainTabHolder
            // 
            this.mainTabHolder.Controls.Add(this.pickSaveTab);
            this.mainTabHolder.Controls.Add(this.prefsTab);
            this.mainTabHolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabHolder.Location = new System.Drawing.Point(0, 0);
            this.mainTabHolder.Name = "mainTabHolder";
            this.mainTabHolder.SelectedIndex = 0;
            this.mainTabHolder.Size = new System.Drawing.Size(800, 300);
            this.mainTabHolder.TabIndex = 5;
            // 
            // pickSaveTab
            // 
            this.pickSaveTab.Controls.Add(this.welcomeLabel);
            this.pickSaveTab.Controls.Add(this.loadSavegameButton);
            this.pickSaveTab.Controls.Add(this.savegameCombobox);
            this.pickSaveTab.Controls.Add(this.scanForSavesButton);
            this.pickSaveTab.Controls.Add(this.saveNameLabel);
            this.pickSaveTab.Location = new System.Drawing.Point(4, 22);
            this.pickSaveTab.Name = "pickSaveTab";
            this.pickSaveTab.Padding = new System.Windows.Forms.Padding(3);
            this.pickSaveTab.Size = new System.Drawing.Size(792, 274);
            this.pickSaveTab.TabIndex = 0;
            this.pickSaveTab.Text = "Pick savegame";
            this.pickSaveTab.UseVisualStyleBackColor = true;
            // 
            // prefsTab
            // 
            this.prefsTab.Location = new System.Drawing.Point(4, 22);
            this.prefsTab.Name = "prefsTab";
            this.prefsTab.Padding = new System.Windows.Forms.Padding(3);
            this.prefsTab.Size = new System.Drawing.Size(459, 183);
            this.prefsTab.TabIndex = 1;
            this.prefsTab.Text = "Preferences";
            this.prefsTab.UseVisualStyleBackColor = true;
            // 
            // outputPane
            // 
            this.outputPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputPane.Location = new System.Drawing.Point(0, 0);
            this.outputPane.Multiline = true;
            this.outputPane.Name = "outputPane";
            this.outputPane.ReadOnly = true;
            this.outputPane.Size = new System.Drawing.Size(800, 146);
            this.outputPane.TabIndex = 6;
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.mainSplitContainer.Name = "mainSplitContainer";
            this.mainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.mainTabHolder);
            this.mainSplitContainer.Panel1MinSize = 100;
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.outputPane);
            this.mainSplitContainer.Panel2MinSize = 100;
            this.mainSplitContainer.Size = new System.Drawing.Size(800, 450);
            this.mainSplitContainer.SplitterDistance = 300;
            this.mainSplitContainer.TabIndex = 7;
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainSplitContainer);
            this.Name = "Mainform";
            this.Text = "KOA Save Editor";
            this.mainTabHolder.ResumeLayout(false);
            this.pickSaveTab.ResumeLayout(false);
            this.pickSaveTab.PerformLayout();
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            this.mainSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label welcomeLabel;
        private System.Windows.Forms.ComboBox savegameCombobox;
        private System.Windows.Forms.Label saveNameLabel;
        private System.Windows.Forms.Button scanForSavesButton;
        private System.Windows.Forms.Button loadSavegameButton;
        private System.Windows.Forms.TabControl mainTabHolder;
        private System.Windows.Forms.TabPage pickSaveTab;
        private System.Windows.Forms.TabPage prefsTab;
        private System.Windows.Forms.TextBox outputPane;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
    }
}

