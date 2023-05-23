namespace SPTMiniLauncher
{
    partial class optionsWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(optionsWindow));
            this.panelMinimizeOnSPTLaunch = new System.Windows.Forms.GroupBox();
            this.bHide = new System.Windows.Forms.Button();
            this.panelEnableTimedAkiLauncher = new System.Windows.Forms.GroupBox();
            this.bEnableTimed = new System.Windows.Forms.Button();
            this.panelMisc = new System.Windows.Forms.GroupBox();
            this.bResetThirdParty = new System.Windows.Forms.Button();
            this.bReset = new System.Windows.Forms.Button();
            this.bRefresh = new System.Windows.Forms.Button();
            this.panelClearCache = new System.Windows.Forms.GroupBox();
            this.bEnableClearCache = new System.Windows.Forms.Button();
            this.panelDetectors = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bEndDetector = new System.Windows.Forms.Button();
            this.bStartDetector = new System.Windows.Forms.Button();
            this.panelLogFile = new System.Windows.Forms.GroupBox();
            this.bEnableOpenLog = new System.Windows.Forms.Button();
            this.panelConfirmationQuit = new System.Windows.Forms.GroupBox();
            this.bEnableConfirmation = new System.Windows.Forms.Button();
            this.panelServerOutput = new System.Windows.Forms.GroupBox();
            this.bEnableServerOutput = new System.Windows.Forms.Button();
            this.panelServerError = new System.Windows.Forms.GroupBox();
            this.bEnableServerErrors = new System.Windows.Forms.Button();
            this.panelCloseOnExit = new System.Windows.Forms.GroupBox();
            this.bCloseOnSPTExit = new System.Windows.Forms.Button();
            this.bPreset1 = new System.Windows.Forms.Button();
            this.tabLauncher = new System.Windows.Forms.Button();
            this.tabSPTAKI = new System.Windows.Forms.Button();
            this.tabTarkov = new System.Windows.Forms.Button();
            this.panelLauncherSettings = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabLauncherDesc = new System.Windows.Forms.Label();
            this.tabSPTDesc = new System.Windows.Forms.Label();
            this.tabTarkovDesc = new System.Windows.Forms.Label();
            this.panelSPTAKISettings = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panelTarkovSettings = new System.Windows.Forms.Panel();
            this.panelTarkovDetector = new System.Windows.Forms.GroupBox();
            this.bEnableTarkovDetection = new System.Windows.Forms.Button();
            this.tabPresets = new System.Windows.Forms.Button();
            this.panelPresets = new System.Windows.Forms.Panel();
            this.descPreset1 = new System.Windows.Forms.Label();
            this.descPreset2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bPreset2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.optionsToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panelMinimizeOnSPTLaunch.SuspendLayout();
            this.panelEnableTimedAkiLauncher.SuspendLayout();
            this.panelMisc.SuspendLayout();
            this.panelClearCache.SuspendLayout();
            this.panelDetectors.SuspendLayout();
            this.panelLogFile.SuspendLayout();
            this.panelConfirmationQuit.SuspendLayout();
            this.panelServerOutput.SuspendLayout();
            this.panelServerError.SuspendLayout();
            this.panelCloseOnExit.SuspendLayout();
            this.panelLauncherSettings.SuspendLayout();
            this.panelSPTAKISettings.SuspendLayout();
            this.panelTarkovSettings.SuspendLayout();
            this.panelTarkovDetector.SuspendLayout();
            this.panelPresets.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMinimizeOnSPTLaunch
            // 
            this.panelMinimizeOnSPTLaunch.Controls.Add(this.bHide);
            this.panelMinimizeOnSPTLaunch.ForeColor = System.Drawing.Color.LightGray;
            this.panelMinimizeOnSPTLaunch.Location = new System.Drawing.Point(3, 92);
            this.panelMinimizeOnSPTLaunch.Name = "panelMinimizeOnSPTLaunch";
            this.panelMinimizeOnSPTLaunch.Size = new System.Drawing.Size(220, 65);
            this.panelMinimizeOnSPTLaunch.TabIndex = 0;
            this.panelMinimizeOnSPTLaunch.TabStop = false;
            this.panelMinimizeOnSPTLaunch.Text = "SPT Launcher start display";
            this.optionsToolTip.SetToolTip(this.panelMinimizeOnSPTLaunch, "If enabled, SPT Launcher will minimize (alternatively close) while SPT is running" +
        ".");
            // 
            // bHide
            // 
            this.bHide.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bHide.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bHide.FlatAppearance.BorderSize = 0;
            this.bHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bHide.ForeColor = System.Drawing.Color.IndianRed;
            this.bHide.Location = new System.Drawing.Point(15, 23);
            this.bHide.Name = "bHide";
            this.bHide.Size = new System.Drawing.Size(180, 30);
            this.bHide.TabIndex = 2;
            this.bHide.Text = "Disabled";
            this.bHide.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bHide.UseVisualStyleBackColor = true;
            this.bHide.Click += new System.EventHandler(this.bMinimize_Click);
            // 
            // panelEnableTimedAkiLauncher
            // 
            this.panelEnableTimedAkiLauncher.Controls.Add(this.bEnableTimed);
            this.panelEnableTimedAkiLauncher.ForeColor = System.Drawing.Color.LightGray;
            this.panelEnableTimedAkiLauncher.Location = new System.Drawing.Point(3, 3);
            this.panelEnableTimedAkiLauncher.Name = "panelEnableTimedAkiLauncher";
            this.panelEnableTimedAkiLauncher.Size = new System.Drawing.Size(220, 65);
            this.panelEnableTimedAkiLauncher.TabIndex = 1;
            this.panelEnableTimedAkiLauncher.TabStop = false;
            this.panelEnableTimedAkiLauncher.Text = "Enable timed Aki Launcher";
            this.optionsToolTip.SetToolTip(this.panelEnableTimedAkiLauncher, "If enabled, Aki Server will run interally via SPT Launcher. This will require Sto" +
        "p SPT to be used in order to exit properly.");
            // 
            // bEnableTimed
            // 
            this.bEnableTimed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bEnableTimed.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bEnableTimed.FlatAppearance.BorderSize = 0;
            this.bEnableTimed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bEnableTimed.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bEnableTimed.Location = new System.Drawing.Point(15, 23);
            this.bEnableTimed.Name = "bEnableTimed";
            this.bEnableTimed.Size = new System.Drawing.Size(180, 30);
            this.bEnableTimed.TabIndex = 3;
            this.bEnableTimed.Text = "Enabled";
            this.bEnableTimed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bEnableTimed.UseVisualStyleBackColor = true;
            this.bEnableTimed.Click += new System.EventHandler(this.bEnableTimed_Click);
            // 
            // panelMisc
            // 
            this.panelMisc.Controls.Add(this.bResetThirdParty);
            this.panelMisc.Controls.Add(this.bReset);
            this.panelMisc.Controls.Add(this.bRefresh);
            this.panelMisc.ForeColor = System.Drawing.Color.LightGray;
            this.panelMisc.Location = new System.Drawing.Point(94, 271);
            this.panelMisc.Name = "panelMisc";
            this.panelMisc.Size = new System.Drawing.Size(539, 65);
            this.panelMisc.TabIndex = 2;
            this.panelMisc.TabStop = false;
            this.panelMisc.Text = "Miscellaneous Launcher options";
            // 
            // bResetThirdParty
            // 
            this.bResetThirdParty.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bResetThirdParty.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bResetThirdParty.FlatAppearance.BorderSize = 0;
            this.bResetThirdParty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bResetThirdParty.ForeColor = System.Drawing.Color.IndianRed;
            this.bResetThirdParty.Location = new System.Drawing.Point(353, 23);
            this.bResetThirdParty.Name = "bResetThirdParty";
            this.bResetThirdParty.Size = new System.Drawing.Size(180, 30);
            this.bResetThirdParty.TabIndex = 5;
            this.bResetThirdParty.Text = "Reset third party apps";
            this.bResetThirdParty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bResetThirdParty.UseVisualStyleBackColor = true;
            this.bResetThirdParty.Click += new System.EventHandler(this.bResetThirdParty_Click);
            // 
            // bReset
            // 
            this.bReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bReset.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bReset.FlatAppearance.BorderSize = 0;
            this.bReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bReset.ForeColor = System.Drawing.Color.IndianRed;
            this.bReset.Location = new System.Drawing.Point(171, 23);
            this.bReset.Name = "bReset";
            this.bReset.Size = new System.Drawing.Size(176, 30);
            this.bReset.TabIndex = 4;
            this.bReset.Text = "Reset SPT Launcher";
            this.bReset.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bReset.UseVisualStyleBackColor = true;
            this.bReset.Click += new System.EventHandler(this.bReset_Click);
            // 
            // bRefresh
            // 
            this.bRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bRefresh.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bRefresh.FlatAppearance.BorderSize = 0;
            this.bRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bRefresh.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bRefresh.Location = new System.Drawing.Point(15, 23);
            this.bRefresh.Name = "bRefresh";
            this.bRefresh.Size = new System.Drawing.Size(150, 30);
            this.bRefresh.TabIndex = 3;
            this.bRefresh.Text = "Refresh main UI";
            this.bRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bRefresh.UseVisualStyleBackColor = true;
            this.bRefresh.Click += new System.EventHandler(this.bRefresh_Click);
            // 
            // panelClearCache
            // 
            this.panelClearCache.Controls.Add(this.bEnableClearCache);
            this.panelClearCache.ForeColor = System.Drawing.Color.LightGray;
            this.panelClearCache.Location = new System.Drawing.Point(497, 92);
            this.panelClearCache.Name = "panelClearCache";
            this.panelClearCache.Size = new System.Drawing.Size(220, 65);
            this.panelClearCache.TabIndex = 3;
            this.panelClearCache.TabStop = false;
            this.panelClearCache.Text = "Clear cache options";
            this.optionsToolTip.SetToolTip(this.panelClearCache, "If enabled, SPT Launcher will clear the server cache for your specified server.");
            // 
            // bEnableClearCache
            // 
            this.bEnableClearCache.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bEnableClearCache.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bEnableClearCache.FlatAppearance.BorderSize = 0;
            this.bEnableClearCache.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bEnableClearCache.ForeColor = System.Drawing.Color.IndianRed;
            this.bEnableClearCache.Location = new System.Drawing.Point(15, 23);
            this.bEnableClearCache.Name = "bEnableClearCache";
            this.bEnableClearCache.Size = new System.Drawing.Size(180, 30);
            this.bEnableClearCache.TabIndex = 4;
            this.bEnableClearCache.Text = "Disabled";
            this.bEnableClearCache.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bEnableClearCache.UseVisualStyleBackColor = true;
            this.bEnableClearCache.Click += new System.EventHandler(this.bEnableClearCache_Click);
            // 
            // panelDetectors
            // 
            this.panelDetectors.Controls.Add(this.label1);
            this.panelDetectors.Controls.Add(this.bEndDetector);
            this.panelDetectors.Controls.Add(this.bStartDetector);
            this.panelDetectors.ForeColor = System.Drawing.Color.LightGray;
            this.panelDetectors.Location = new System.Drawing.Point(3, 3);
            this.panelDetectors.Name = "panelDetectors";
            this.panelDetectors.Size = new System.Drawing.Size(437, 121);
            this.panelDetectors.TabIndex = 4;
            this.panelDetectors.TabStop = false;
            this.panelDetectors.Text = "Tarkov auto-detection";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Bahnschrift Light", 8F);
            this.label1.Location = new System.Drawing.Point(226, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 89);
            this.label1.TabIndex = 16;
            this.label1.Text = "How often the detector should scan for Tarkov\'s process starting and stopping.\r\n\r" +
    "\nDon\'t change unless you know what you\'re doing!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bEndDetector
            // 
            this.bEndDetector.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bEndDetector.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bEndDetector.FlatAppearance.BorderSize = 0;
            this.bEndDetector.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bEndDetector.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bEndDetector.Location = new System.Drawing.Point(12, 66);
            this.bEndDetector.Name = "bEndDetector";
            this.bEndDetector.Size = new System.Drawing.Size(207, 30);
            this.bEndDetector.TabIndex = 5;
            this.bEndDetector.Text = "End detector: 0.5 seconds";
            this.bEndDetector.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bEndDetector.UseVisualStyleBackColor = true;
            this.bEndDetector.Click += new System.EventHandler(this.bEndDetector_Click);
            // 
            // bStartDetector
            // 
            this.bStartDetector.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bStartDetector.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bStartDetector.FlatAppearance.BorderSize = 0;
            this.bStartDetector.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bStartDetector.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bStartDetector.Location = new System.Drawing.Point(12, 30);
            this.bStartDetector.Name = "bStartDetector";
            this.bStartDetector.Size = new System.Drawing.Size(207, 30);
            this.bStartDetector.TabIndex = 4;
            this.bStartDetector.Text = "Start detector: 0.5 seconds";
            this.bStartDetector.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bStartDetector.UseVisualStyleBackColor = true;
            this.bStartDetector.Click += new System.EventHandler(this.bStartDetector_Click);
            // 
            // panelLogFile
            // 
            this.panelLogFile.Controls.Add(this.bEnableOpenLog);
            this.panelLogFile.ForeColor = System.Drawing.Color.LightGray;
            this.panelLogFile.Location = new System.Drawing.Point(250, 182);
            this.panelLogFile.Name = "panelLogFile";
            this.panelLogFile.Size = new System.Drawing.Size(220, 65);
            this.panelLogFile.TabIndex = 5;
            this.panelLogFile.TabStop = false;
            this.panelLogFile.Text = "Open log file on SPT quit";
            this.optionsToolTip.SetToolTip(this.panelLogFile, "If enabled, SPT Launcher will open the newly generated log file after SPT closes." +
        "\r\n\r\n(Requires SPT-AKI settings -> Enable Timed Aki Launcher)");
            // 
            // bEnableOpenLog
            // 
            this.bEnableOpenLog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bEnableOpenLog.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bEnableOpenLog.FlatAppearance.BorderSize = 0;
            this.bEnableOpenLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bEnableOpenLog.ForeColor = System.Drawing.Color.IndianRed;
            this.bEnableOpenLog.Location = new System.Drawing.Point(15, 23);
            this.bEnableOpenLog.Name = "bEnableOpenLog";
            this.bEnableOpenLog.Size = new System.Drawing.Size(180, 30);
            this.bEnableOpenLog.TabIndex = 4;
            this.bEnableOpenLog.Text = "Disabled";
            this.bEnableOpenLog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bEnableOpenLog.UseVisualStyleBackColor = true;
            this.bEnableOpenLog.Click += new System.EventHandler(this.bEnableOpenLog_Click);
            // 
            // panelConfirmationQuit
            // 
            this.panelConfirmationQuit.Controls.Add(this.bEnableConfirmation);
            this.panelConfirmationQuit.ForeColor = System.Drawing.Color.LightGray;
            this.panelConfirmationQuit.Location = new System.Drawing.Point(3, 3);
            this.panelConfirmationQuit.Name = "panelConfirmationQuit";
            this.panelConfirmationQuit.Size = new System.Drawing.Size(220, 65);
            this.panelConfirmationQuit.TabIndex = 6;
            this.panelConfirmationQuit.TabStop = false;
            this.panelConfirmationQuit.Text = "Button confirmation pop-ups";
            this.optionsToolTip.SetToolTip(this.panelConfirmationQuit, "If enabled, clicking \'Stop SPT\' and \'Clear Cache\' will ask for confirmation to av" +
        "oid mis-clicks.");
            // 
            // bEnableConfirmation
            // 
            this.bEnableConfirmation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bEnableConfirmation.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bEnableConfirmation.FlatAppearance.BorderSize = 0;
            this.bEnableConfirmation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bEnableConfirmation.ForeColor = System.Drawing.Color.IndianRed;
            this.bEnableConfirmation.Location = new System.Drawing.Point(15, 23);
            this.bEnableConfirmation.Name = "bEnableConfirmation";
            this.bEnableConfirmation.Size = new System.Drawing.Size(180, 30);
            this.bEnableConfirmation.TabIndex = 4;
            this.bEnableConfirmation.Text = "Disabled";
            this.bEnableConfirmation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bEnableConfirmation.UseVisualStyleBackColor = true;
            this.bEnableConfirmation.Click += new System.EventHandler(this.bEnableConfirmation_Click);
            // 
            // panelServerOutput
            // 
            this.panelServerOutput.Controls.Add(this.bEnableServerOutput);
            this.panelServerOutput.ForeColor = System.Drawing.Color.LightGray;
            this.panelServerOutput.Location = new System.Drawing.Point(250, 92);
            this.panelServerOutput.Name = "panelServerOutput";
            this.panelServerOutput.Size = new System.Drawing.Size(220, 65);
            this.panelServerOutput.TabIndex = 7;
            this.panelServerOutput.TabStop = false;
            this.panelServerOutput.Text = "Display server output on start";
            this.optionsToolTip.SetToolTip(this.panelServerOutput, "If enabled, an external window will output the Aki Server\'s data for convenient v" +
        "iewing.\r\n\r\n(Requires SPT-AKI settings -> Enable Timed Aki Launcher)");
            // 
            // bEnableServerOutput
            // 
            this.bEnableServerOutput.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bEnableServerOutput.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bEnableServerOutput.FlatAppearance.BorderSize = 0;
            this.bEnableServerOutput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bEnableServerOutput.ForeColor = System.Drawing.Color.IndianRed;
            this.bEnableServerOutput.Location = new System.Drawing.Point(15, 23);
            this.bEnableServerOutput.Name = "bEnableServerOutput";
            this.bEnableServerOutput.Size = new System.Drawing.Size(180, 30);
            this.bEnableServerOutput.TabIndex = 4;
            this.bEnableServerOutput.Text = "Disabled";
            this.bEnableServerOutput.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bEnableServerOutput.UseVisualStyleBackColor = true;
            this.bEnableServerOutput.Click += new System.EventHandler(this.bEnableServerOutput_Click);
            // 
            // panelServerError
            // 
            this.panelServerError.Controls.Add(this.bEnableServerErrors);
            this.panelServerError.ForeColor = System.Drawing.Color.LightGray;
            this.panelServerError.Location = new System.Drawing.Point(250, 3);
            this.panelServerError.Name = "panelServerError";
            this.panelServerError.Size = new System.Drawing.Size(220, 65);
            this.panelServerError.TabIndex = 8;
            this.panelServerError.TabStop = false;
            this.panelServerError.Text = " Server error pop-ups ";
            this.optionsToolTip.SetToolTip(this.panelServerError, "If enabled, if the external output window receives an error, SPT Launcher will no" +
        "tify with a message.");
            // 
            // bEnableServerErrors
            // 
            this.bEnableServerErrors.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bEnableServerErrors.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bEnableServerErrors.FlatAppearance.BorderSize = 0;
            this.bEnableServerErrors.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bEnableServerErrors.ForeColor = System.Drawing.Color.IndianRed;
            this.bEnableServerErrors.Location = new System.Drawing.Point(15, 23);
            this.bEnableServerErrors.Name = "bEnableServerErrors";
            this.bEnableServerErrors.Size = new System.Drawing.Size(180, 30);
            this.bEnableServerErrors.TabIndex = 4;
            this.bEnableServerErrors.Text = "Disabled";
            this.bEnableServerErrors.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bEnableServerErrors.UseVisualStyleBackColor = true;
            this.bEnableServerErrors.Click += new System.EventHandler(this.bEnableServerErrors_Click);
            // 
            // panelCloseOnExit
            // 
            this.panelCloseOnExit.Controls.Add(this.bCloseOnSPTExit);
            this.panelCloseOnExit.ForeColor = System.Drawing.Color.LightGray;
            this.panelCloseOnExit.Location = new System.Drawing.Point(3, 182);
            this.panelCloseOnExit.Name = "panelCloseOnExit";
            this.panelCloseOnExit.Size = new System.Drawing.Size(220, 65);
            this.panelCloseOnExit.TabIndex = 9;
            this.panelCloseOnExit.TabStop = false;
            this.panelCloseOnExit.Text = "Close Launcher on SPT quit";
            this.optionsToolTip.SetToolTip(this.panelCloseOnExit, "If enabled, SPT Launcher will close when the Aki Server & Launcher close.");
            // 
            // bCloseOnSPTExit
            // 
            this.bCloseOnSPTExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bCloseOnSPTExit.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bCloseOnSPTExit.FlatAppearance.BorderSize = 0;
            this.bCloseOnSPTExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCloseOnSPTExit.ForeColor = System.Drawing.Color.IndianRed;
            this.bCloseOnSPTExit.Location = new System.Drawing.Point(15, 23);
            this.bCloseOnSPTExit.Name = "bCloseOnSPTExit";
            this.bCloseOnSPTExit.Size = new System.Drawing.Size(180, 30);
            this.bCloseOnSPTExit.TabIndex = 4;
            this.bCloseOnSPTExit.Text = "Disabled";
            this.bCloseOnSPTExit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCloseOnSPTExit.UseVisualStyleBackColor = true;
            this.bCloseOnSPTExit.Click += new System.EventHandler(this.bCloseOnSPTExit_Click);
            // 
            // bPreset1
            // 
            this.bPreset1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bPreset1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.bPreset1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bPreset1.ForeColor = System.Drawing.Color.LightGray;
            this.bPreset1.Location = new System.Drawing.Point(35, 30);
            this.bPreset1.Name = "bPreset1";
            this.bPreset1.Size = new System.Drawing.Size(220, 50);
            this.bPreset1.TabIndex = 10;
            this.bPreset1.Text = "Apply Live-like settings";
            this.bPreset1.UseVisualStyleBackColor = true;
            this.bPreset1.Click += new System.EventHandler(this.bPreset1_Click);
            // 
            // tabLauncher
            // 
            this.tabLauncher.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.tabLauncher.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tabLauncher.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.tabLauncher.FlatAppearance.BorderSize = 0;
            this.tabLauncher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tabLauncher.ForeColor = System.Drawing.Color.LightGray;
            this.tabLauncher.Location = new System.Drawing.Point(180, 21);
            this.tabLauncher.Name = "tabLauncher";
            this.tabLauncher.Size = new System.Drawing.Size(150, 35);
            this.tabLauncher.TabIndex = 11;
            this.tabLauncher.Text = "Launcher settings";
            this.tabLauncher.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tabLauncher.UseVisualStyleBackColor = false;
            this.tabLauncher.Click += new System.EventHandler(this.tabLauncher_Click);
            // 
            // tabSPTAKI
            // 
            this.tabSPTAKI.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tabSPTAKI.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.tabSPTAKI.FlatAppearance.BorderSize = 0;
            this.tabSPTAKI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tabSPTAKI.ForeColor = System.Drawing.Color.LightGray;
            this.tabSPTAKI.Location = new System.Drawing.Point(336, 21);
            this.tabSPTAKI.Name = "tabSPTAKI";
            this.tabSPTAKI.Size = new System.Drawing.Size(150, 35);
            this.tabSPTAKI.TabIndex = 12;
            this.tabSPTAKI.Text = "SPT-AKI settings";
            this.tabSPTAKI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tabSPTAKI.UseVisualStyleBackColor = true;
            this.tabSPTAKI.Click += new System.EventHandler(this.tabSPTAKI_Click);
            // 
            // tabTarkov
            // 
            this.tabTarkov.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tabTarkov.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.tabTarkov.FlatAppearance.BorderSize = 0;
            this.tabTarkov.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tabTarkov.ForeColor = System.Drawing.Color.LightGray;
            this.tabTarkov.Location = new System.Drawing.Point(492, 21);
            this.tabTarkov.Name = "tabTarkov";
            this.tabTarkov.Size = new System.Drawing.Size(150, 35);
            this.tabTarkov.TabIndex = 13;
            this.tabTarkov.Text = "Tarkov settings";
            this.tabTarkov.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tabTarkov.UseVisualStyleBackColor = true;
            this.tabTarkov.Click += new System.EventHandler(this.tabTarkov_Click);
            // 
            // panelLauncherSettings
            // 
            this.panelLauncherSettings.Controls.Add(this.panel3);
            this.panelLauncherSettings.Controls.Add(this.panel2);
            this.panelLauncherSettings.Controls.Add(this.panel1);
            this.panelLauncherSettings.Controls.Add(this.panelConfirmationQuit);
            this.panelLauncherSettings.Controls.Add(this.panelServerOutput);
            this.panelLauncherSettings.Controls.Add(this.panelLogFile);
            this.panelLauncherSettings.Controls.Add(this.panelCloseOnExit);
            this.panelLauncherSettings.Controls.Add(this.panelServerError);
            this.panelLauncherSettings.Controls.Add(this.panelClearCache);
            this.panelLauncherSettings.Controls.Add(this.panelMinimizeOnSPTLaunch);
            this.panelLauncherSettings.Controls.Add(this.panelMisc);
            this.panelLauncherSettings.Location = new System.Drawing.Point(12, 145);
            this.panelLauncherSettings.Name = "panelLauncherSettings";
            this.panelLauncherSettings.Size = new System.Drawing.Size(798, 348);
            this.panelLauncherSettings.TabIndex = 14;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(3, 261);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(714, 1);
            this.panel3.TabIndex = 19;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(3, 171);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(714, 1);
            this.panel2.TabIndex = 18;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(3, 81);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(714, 1);
            this.panel1.TabIndex = 10;
            // 
            // tabLauncherDesc
            // 
            this.tabLauncherDesc.Font = new System.Drawing.Font("Bahnschrift Light", 8F);
            this.tabLauncherDesc.Location = new System.Drawing.Point(180, 64);
            this.tabLauncherDesc.Name = "tabLauncherDesc";
            this.tabLauncherDesc.Size = new System.Drawing.Size(150, 48);
            this.tabLauncherDesc.TabIndex = 15;
            this.tabLauncherDesc.Text = "Settings and options for the SPT Launcher";
            // 
            // tabSPTDesc
            // 
            this.tabSPTDesc.Font = new System.Drawing.Font("Bahnschrift Light", 8F);
            this.tabSPTDesc.Location = new System.Drawing.Point(333, 64);
            this.tabSPTDesc.Name = "tabSPTDesc";
            this.tabSPTDesc.Size = new System.Drawing.Size(153, 48);
            this.tabSPTDesc.TabIndex = 16;
            this.tabSPTDesc.Text = "Settings and configurations related to SPT-AKI and its Server and Launcher";
            // 
            // tabTarkovDesc
            // 
            this.tabTarkovDesc.Font = new System.Drawing.Font("Bahnschrift Light", 8F);
            this.tabTarkovDesc.Location = new System.Drawing.Point(489, 64);
            this.tabTarkovDesc.Name = "tabTarkovDesc";
            this.tabTarkovDesc.Size = new System.Drawing.Size(153, 48);
            this.tabTarkovDesc.TabIndex = 17;
            this.tabTarkovDesc.Text = "Settings and configurations for Escape From Tarkov";
            // 
            // panelSPTAKISettings
            // 
            this.panelSPTAKISettings.Controls.Add(this.label2);
            this.panelSPTAKISettings.Controls.Add(this.panelEnableTimedAkiLauncher);
            this.panelSPTAKISettings.Location = new System.Drawing.Point(12, 145);
            this.panelSPTAKISettings.Name = "panelSPTAKISettings";
            this.panelSPTAKISettings.Size = new System.Drawing.Size(798, 348);
            this.panelSPTAKISettings.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Bahnschrift Light", 9F);
            this.label2.Location = new System.Drawing.Point(3, 71);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label2.Size = new System.Drawing.Size(220, 97);
            this.label2.TabIndex = 17;
            this.label2.Text = "Enabling this setting will run the Aki Server internally.\r\n\r\nClicking \'Stop SPT\' " +
    "while the Aki Server is running will close it.";
            // 
            // panelTarkovSettings
            // 
            this.panelTarkovSettings.Controls.Add(this.panelTarkovDetector);
            this.panelTarkovSettings.Controls.Add(this.panelDetectors);
            this.panelTarkovSettings.Location = new System.Drawing.Point(12, 145);
            this.panelTarkovSettings.Name = "panelTarkovSettings";
            this.panelTarkovSettings.Size = new System.Drawing.Size(798, 348);
            this.panelTarkovSettings.TabIndex = 16;
            // 
            // panelTarkovDetector
            // 
            this.panelTarkovDetector.Controls.Add(this.bEnableTarkovDetection);
            this.panelTarkovDetector.ForeColor = System.Drawing.Color.LightGray;
            this.panelTarkovDetector.Location = new System.Drawing.Point(3, 134);
            this.panelTarkovDetector.Name = "panelTarkovDetector";
            this.panelTarkovDetector.Size = new System.Drawing.Size(220, 65);
            this.panelTarkovDetector.TabIndex = 8;
            this.panelTarkovDetector.TabStop = false;
            this.panelTarkovDetector.Text = "Allow Tarkov auto-detection";
            this.optionsToolTip.SetToolTip(this.panelTarkovDetector, "If enabled, quitting Tarkov will automatically be detected. Can be used alongside" +
        " the internal Aki Server.");
            // 
            // bEnableTarkovDetection
            // 
            this.bEnableTarkovDetection.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bEnableTarkovDetection.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bEnableTarkovDetection.FlatAppearance.BorderSize = 0;
            this.bEnableTarkovDetection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bEnableTarkovDetection.ForeColor = System.Drawing.Color.IndianRed;
            this.bEnableTarkovDetection.Location = new System.Drawing.Point(15, 23);
            this.bEnableTarkovDetection.Name = "bEnableTarkovDetection";
            this.bEnableTarkovDetection.Size = new System.Drawing.Size(180, 30);
            this.bEnableTarkovDetection.TabIndex = 4;
            this.bEnableTarkovDetection.Text = "Disabled";
            this.bEnableTarkovDetection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bEnableTarkovDetection.UseVisualStyleBackColor = true;
            this.bEnableTarkovDetection.Click += new System.EventHandler(this.bEnableTarkovDetection_Click);
            // 
            // tabPresets
            // 
            this.tabPresets.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tabPresets.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.tabPresets.FlatAppearance.BorderSize = 0;
            this.tabPresets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tabPresets.ForeColor = System.Drawing.Color.LightGray;
            this.tabPresets.Location = new System.Drawing.Point(648, 21);
            this.tabPresets.Name = "tabPresets";
            this.tabPresets.Size = new System.Drawing.Size(150, 35);
            this.tabPresets.TabIndex = 18;
            this.tabPresets.Text = "Presets";
            this.tabPresets.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tabPresets.UseVisualStyleBackColor = true;
            this.tabPresets.Click += new System.EventHandler(this.tabPresets_Click);
            // 
            // panelPresets
            // 
            this.panelPresets.Controls.Add(this.descPreset1);
            this.panelPresets.Controls.Add(this.descPreset2);
            this.panelPresets.Controls.Add(this.groupBox2);
            this.panelPresets.Controls.Add(this.groupBox1);
            this.panelPresets.Location = new System.Drawing.Point(12, 145);
            this.panelPresets.Name = "panelPresets";
            this.panelPresets.Size = new System.Drawing.Size(798, 348);
            this.panelPresets.TabIndex = 19;
            // 
            // descPreset1
            // 
            this.descPreset1.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.descPreset1.Location = new System.Drawing.Point(3, 115);
            this.descPreset1.Name = "descPreset1";
            this.descPreset1.Size = new System.Drawing.Size(290, 57);
            this.descPreset1.TabIndex = 18;
            this.descPreset1.Text = "This preset aims to make launching and exiting SPT in its entirety fluid and seam" +
    "less.";
            this.descPreset1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // descPreset2
            // 
            this.descPreset2.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.descPreset2.Location = new System.Drawing.Point(324, 115);
            this.descPreset2.Name = "descPreset2";
            this.descPreset2.Size = new System.Drawing.Size(290, 57);
            this.descPreset2.TabIndex = 17;
            this.descPreset2.Text = "This preset is useful for a manual feel or if you need to troubleshoot.";
            this.descPreset2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bPreset2);
            this.groupBox2.ForeColor = System.Drawing.Color.LightGray;
            this.groupBox2.Location = new System.Drawing.Point(324, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(290, 100);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "\"Vanilla behavior\" preset";
            // 
            // bPreset2
            // 
            this.bPreset2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bPreset2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.bPreset2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bPreset2.ForeColor = System.Drawing.Color.LightGray;
            this.bPreset2.Location = new System.Drawing.Point(35, 30);
            this.bPreset2.Name = "bPreset2";
            this.bPreset2.Size = new System.Drawing.Size(220, 50);
            this.bPreset2.TabIndex = 10;
            this.bPreset2.Text = "Apply vanilla behavior settings";
            this.bPreset2.UseVisualStyleBackColor = true;
            this.bPreset2.Click += new System.EventHandler(this.bPreset2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bPreset1);
            this.groupBox1.ForeColor = System.Drawing.Color.LightGray;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 100);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "\"Live-like\" preset";
            // 
            // optionsToolTip
            // 
            this.optionsToolTip.AutoPopDelay = 30000;
            this.optionsToolTip.InitialDelay = 500;
            this.optionsToolTip.ReshowDelay = 100;
            this.optionsToolTip.ToolTipTitle = "Setting";
            // 
            // optionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(822, 517);
            this.Controls.Add(this.tabPresets);
            this.Controls.Add(this.tabTarkovDesc);
            this.Controls.Add(this.tabSPTDesc);
            this.Controls.Add(this.tabLauncherDesc);
            this.Controls.Add(this.tabTarkov);
            this.Controls.Add(this.tabSPTAKI);
            this.Controls.Add(this.tabLauncher);
            this.Controls.Add(this.panelLauncherSettings);
            this.Controls.Add(this.panelTarkovSettings);
            this.Controls.Add(this.panelSPTAKISettings);
            this.Controls.Add(this.panelPresets);
            this.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.ForeColor = System.Drawing.Color.LightGray;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "optionsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.optionsWindow_FormClosing);
            this.Load += new System.EventHandler(this.optionsWindow_Load);
            this.panelMinimizeOnSPTLaunch.ResumeLayout(false);
            this.panelEnableTimedAkiLauncher.ResumeLayout(false);
            this.panelMisc.ResumeLayout(false);
            this.panelClearCache.ResumeLayout(false);
            this.panelDetectors.ResumeLayout(false);
            this.panelLogFile.ResumeLayout(false);
            this.panelConfirmationQuit.ResumeLayout(false);
            this.panelServerOutput.ResumeLayout(false);
            this.panelServerError.ResumeLayout(false);
            this.panelCloseOnExit.ResumeLayout(false);
            this.panelLauncherSettings.ResumeLayout(false);
            this.panelSPTAKISettings.ResumeLayout(false);
            this.panelTarkovSettings.ResumeLayout(false);
            this.panelTarkovDetector.ResumeLayout(false);
            this.panelPresets.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox panelMinimizeOnSPTLaunch;
        private System.Windows.Forms.GroupBox panelEnableTimedAkiLauncher;
        private System.Windows.Forms.Button bHide;
        private System.Windows.Forms.Button bEnableTimed;
        private System.Windows.Forms.GroupBox panelMisc;
        private System.Windows.Forms.Button bRefresh;
        private System.Windows.Forms.Button bReset;
        private System.Windows.Forms.Button bResetThirdParty;
        private System.Windows.Forms.GroupBox panelClearCache;
        private System.Windows.Forms.Button bEnableClearCache;
        private System.Windows.Forms.GroupBox panelDetectors;
        private System.Windows.Forms.Button bStartDetector;
        private System.Windows.Forms.Button bEndDetector;
        private System.Windows.Forms.GroupBox panelLogFile;
        private System.Windows.Forms.Button bEnableOpenLog;
        private System.Windows.Forms.GroupBox panelConfirmationQuit;
        private System.Windows.Forms.Button bEnableConfirmation;
        private System.Windows.Forms.GroupBox panelServerOutput;
        private System.Windows.Forms.Button bEnableServerOutput;
        private System.Windows.Forms.GroupBox panelServerError;
        private System.Windows.Forms.Button bEnableServerErrors;
        private System.Windows.Forms.GroupBox panelCloseOnExit;
        private System.Windows.Forms.Button bCloseOnSPTExit;
        private System.Windows.Forms.Button bPreset1;
        private System.Windows.Forms.Button tabLauncher;
        private System.Windows.Forms.Button tabSPTAKI;
        private System.Windows.Forms.Button tabTarkov;
        private System.Windows.Forms.Panel panelLauncherSettings;
        private System.Windows.Forms.Label tabLauncherDesc;
        private System.Windows.Forms.Label tabSPTDesc;
        private System.Windows.Forms.Label tabTarkovDesc;
        private System.Windows.Forms.Panel panelSPTAKISettings;
        private System.Windows.Forms.Panel panelTarkovSettings;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button tabPresets;
        private System.Windows.Forms.Panel panelPresets;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button bPreset2;
        private System.Windows.Forms.Label descPreset2;
        private System.Windows.Forms.Label descPreset1;
        private System.Windows.Forms.GroupBox panelTarkovDetector;
        private System.Windows.Forms.Button bEnableTarkovDetection;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip optionsToolTip;
    }
}