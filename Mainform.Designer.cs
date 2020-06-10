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
            this.scanForSavesButton = new System.Windows.Forms.Button();
            this.loadSavegameButton = new System.Windows.Forms.Button();
            this.mainTabHolder = new System.Windows.Forms.TabControl();
            this.pickSaveTab = new System.Windows.Forms.TabPage();
            this.prefsTab = new System.Windows.Forms.TabPage();
            this.outputPane = new System.Windows.Forms.TextBox();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.savegameListView = new System.Windows.Forms.ListView();
            this.filenameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.elapsedTimeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.charNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.locationHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.classHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.questHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.saveTimeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.levelHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.debugHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.welcomeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.welcomeLabel.AutoSize = true;
            this.welcomeLabel.Location = new System.Drawing.Point(3, 258);
            this.welcomeLabel.Name = "welcomeLabel";
            this.welcomeLabel.Size = new System.Drawing.Size(236, 13);
            this.welcomeLabel.TabIndex = 0;
            this.welcomeLabel.Text = "Welcome to KOA Save Editor. It will get better....";
            // 
            // scanForSavesButton
            // 
            this.scanForSavesButton.Location = new System.Drawing.Point(9, 6);
            this.scanForSavesButton.Name = "scanForSavesButton";
            this.scanForSavesButton.Size = new System.Drawing.Size(99, 23);
            this.scanForSavesButton.TabIndex = 3;
            this.scanForSavesButton.Text = "Scan for saves";
            this.scanForSavesButton.UseVisualStyleBackColor = true;
            this.scanForSavesButton.Click += new System.EventHandler(this.scanForSavesButton_Click);
            // 
            // loadSavegameButton
            // 
            this.loadSavegameButton.Location = new System.Drawing.Point(265, 6);
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
            this.pickSaveTab.Controls.Add(this.savegameListView);
            this.pickSaveTab.Controls.Add(this.welcomeLabel);
            this.pickSaveTab.Controls.Add(this.loadSavegameButton);
            this.pickSaveTab.Controls.Add(this.scanForSavesButton);
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
            this.outputPane.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
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
            // savegameListView
            // 
            this.savegameListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.savegameListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.filenameColumnHeader,
            this.saveTimeHeader,
            this.elapsedTimeHeader,
            this.charNameHeader,
            this.locationHeader,
            this.classHeader,
            this.questHeader,
            this.levelHeader,
            this.debugHeader});
            this.savegameListView.HideSelection = false;
            this.savegameListView.Location = new System.Drawing.Point(6, 35);
            this.savegameListView.Name = "savegameListView";
            this.savegameListView.Size = new System.Drawing.Size(778, 202);
            this.savegameListView.TabIndex = 5;
            this.savegameListView.UseCompatibleStateImageBehavior = false;
            this.savegameListView.View = System.Windows.Forms.View.Details;
            // 
            // filenameColumnHeader
            // 
            this.filenameColumnHeader.Text = "Filename";
            // 
            // elapsedTimeHeader
            // 
            this.elapsedTimeHeader.Text = "Time played";
            this.elapsedTimeHeader.Width = 91;
            // 
            // charNameHeader
            // 
            this.charNameHeader.Text = "Character";
            // 
            // locationHeader
            // 
            this.locationHeader.DisplayIndex = 5;
            this.locationHeader.Text = "Location";
            // 
            // classHeader
            // 
            this.classHeader.DisplayIndex = 6;
            this.classHeader.Text = "Class";
            // 
            // questHeader
            // 
            this.questHeader.DisplayIndex = 7;
            this.questHeader.Text = "Quest";
            // 
            // saveTimeHeader
            // 
            this.saveTimeHeader.Text = "Saved at";
            // 
            // levelHeader
            // 
            this.levelHeader.DisplayIndex = 4;
            this.levelHeader.Text = "Level";
            // 
            // debugHeader
            // 
            this.debugHeader.Text = "Debug";
            this.debugHeader.Width = 366;
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
        private System.Windows.Forms.Button scanForSavesButton;
        private System.Windows.Forms.Button loadSavegameButton;
        private System.Windows.Forms.TabControl mainTabHolder;
        private System.Windows.Forms.TabPage pickSaveTab;
        private System.Windows.Forms.TabPage prefsTab;
        private System.Windows.Forms.TextBox outputPane;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.ListView savegameListView;
        private System.Windows.Forms.ColumnHeader filenameColumnHeader;
        private System.Windows.Forms.ColumnHeader saveTimeHeader;
        private System.Windows.Forms.ColumnHeader elapsedTimeHeader;
        private System.Windows.Forms.ColumnHeader charNameHeader;
        private System.Windows.Forms.ColumnHeader locationHeader;
        private System.Windows.Forms.ColumnHeader classHeader;
        private System.Windows.Forms.ColumnHeader questHeader;
        private System.Windows.Forms.ColumnHeader levelHeader;
        private System.Windows.Forms.ColumnHeader debugHeader;
    }
}

