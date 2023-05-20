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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(optionsWindow));
            this.panelMinimizeOnSPTLaunch = new System.Windows.Forms.GroupBox();
            this.bMinimize = new System.Windows.Forms.Button();
            this.panelEnableTimedAkiLauncher = new System.Windows.Forms.GroupBox();
            this.bEnableTimed = new System.Windows.Forms.Button();
            this.panelMisc = new System.Windows.Forms.GroupBox();
            this.bResetThirdParty = new System.Windows.Forms.Button();
            this.bReset = new System.Windows.Forms.Button();
            this.bRefresh = new System.Windows.Forms.Button();
            this.panelClearCache = new System.Windows.Forms.GroupBox();
            this.bEnableClearCache = new System.Windows.Forms.Button();
            this.panelDetectors = new System.Windows.Forms.GroupBox();
            this.bStartDetector = new System.Windows.Forms.Button();
            this.bEndDetector = new System.Windows.Forms.Button();
            this.panelLogFile = new System.Windows.Forms.GroupBox();
            this.bEnableOpenLog = new System.Windows.Forms.Button();
            this.panelConfirmationQuit = new System.Windows.Forms.GroupBox();
            this.bEnableConfirmation = new System.Windows.Forms.Button();
            this.bEnableAltCache = new System.Windows.Forms.Button();
            this.panelServerOutput = new System.Windows.Forms.GroupBox();
            this.bEnableServerOutput = new System.Windows.Forms.Button();
            this.panelServerError = new System.Windows.Forms.GroupBox();
            this.bEnableServerErrors = new System.Windows.Forms.Button();
            this.panelMinimizeOnSPTLaunch.SuspendLayout();
            this.panelEnableTimedAkiLauncher.SuspendLayout();
            this.panelMisc.SuspendLayout();
            this.panelClearCache.SuspendLayout();
            this.panelDetectors.SuspendLayout();
            this.panelLogFile.SuspendLayout();
            this.panelConfirmationQuit.SuspendLayout();
            this.panelServerOutput.SuspendLayout();
            this.panelServerError.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMinimizeOnSPTLaunch
            // 
            this.panelMinimizeOnSPTLaunch.Controls.Add(this.bMinimize);
            this.panelMinimizeOnSPTLaunch.ForeColor = System.Drawing.Color.LightGray;
            this.panelMinimizeOnSPTLaunch.Location = new System.Drawing.Point(12, 12);
            this.panelMinimizeOnSPTLaunch.Name = "panelMinimizeOnSPTLaunch";
            this.panelMinimizeOnSPTLaunch.Size = new System.Drawing.Size(210, 65);
            this.panelMinimizeOnSPTLaunch.TabIndex = 0;
            this.panelMinimizeOnSPTLaunch.TabStop = false;
            this.panelMinimizeOnSPTLaunch.Text = " Minimize on SPT launch ";
            // 
            // bMinimize
            // 
            this.bMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bMinimize.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bMinimize.FlatAppearance.BorderSize = 0;
            this.bMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMinimize.ForeColor = System.Drawing.Color.IndianRed;
            this.bMinimize.Location = new System.Drawing.Point(15, 23);
            this.bMinimize.Name = "bMinimize";
            this.bMinimize.Size = new System.Drawing.Size(180, 30);
            this.bMinimize.TabIndex = 2;
            this.bMinimize.Text = "Disabled";
            this.bMinimize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bMinimize.UseVisualStyleBackColor = true;
            this.bMinimize.Click += new System.EventHandler(this.bMinimize_Click);
            // 
            // panelEnableTimedAkiLauncher
            // 
            this.panelEnableTimedAkiLauncher.Controls.Add(this.bEnableTimed);
            this.panelEnableTimedAkiLauncher.ForeColor = System.Drawing.Color.LightGray;
            this.panelEnableTimedAkiLauncher.Location = new System.Drawing.Point(239, 12);
            this.panelEnableTimedAkiLauncher.Name = "panelEnableTimedAkiLauncher";
            this.panelEnableTimedAkiLauncher.Size = new System.Drawing.Size(210, 65);
            this.panelEnableTimedAkiLauncher.TabIndex = 1;
            this.panelEnableTimedAkiLauncher.TabStop = false;
            this.panelEnableTimedAkiLauncher.Text = " Enable timed Aki Launcher ";
            // 
            // bEnableTimed
            // 
            this.bEnableTimed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bEnableTimed.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bEnableTimed.FlatAppearance.BorderSize = 0;
            this.bEnableTimed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bEnableTimed.ForeColor = System.Drawing.Color.IndianRed;
            this.bEnableTimed.Location = new System.Drawing.Point(15, 23);
            this.bEnableTimed.Name = "bEnableTimed";
            this.bEnableTimed.Size = new System.Drawing.Size(180, 30);
            this.bEnableTimed.TabIndex = 3;
            this.bEnableTimed.Text = "Disabled";
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
            this.panelMisc.Location = new System.Drawing.Point(12, 172);
            this.panelMisc.Name = "panelMisc";
            this.panelMisc.Size = new System.Drawing.Size(437, 65);
            this.panelMisc.TabIndex = 2;
            this.panelMisc.TabStop = false;
            this.panelMisc.Text = " Misc. ";
            // 
            // bResetThirdParty
            // 
            this.bResetThirdParty.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bResetThirdParty.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bResetThirdParty.FlatAppearance.BorderSize = 0;
            this.bResetThirdParty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bResetThirdParty.ForeColor = System.Drawing.Color.IndianRed;
            this.bResetThirdParty.Location = new System.Drawing.Point(261, 23);
            this.bResetThirdParty.Name = "bResetThirdParty";
            this.bResetThirdParty.Size = new System.Drawing.Size(170, 30);
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
            this.bReset.Location = new System.Drawing.Point(138, 23);
            this.bReset.Name = "bReset";
            this.bReset.Size = new System.Drawing.Size(108, 30);
            this.bReset.TabIndex = 4;
            this.bReset.Text = "Reset";
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
            this.bRefresh.Size = new System.Drawing.Size(108, 30);
            this.bRefresh.TabIndex = 3;
            this.bRefresh.Text = "Refresh";
            this.bRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bRefresh.UseVisualStyleBackColor = true;
            this.bRefresh.Click += new System.EventHandler(this.bRefresh_Click);
            // 
            // panelClearCache
            // 
            this.panelClearCache.Controls.Add(this.bEnableAltCache);
            this.panelClearCache.Controls.Add(this.bEnableClearCache);
            this.panelClearCache.ForeColor = System.Drawing.Color.LightGray;
            this.panelClearCache.Location = new System.Drawing.Point(12, 92);
            this.panelClearCache.Name = "panelClearCache";
            this.panelClearCache.Size = new System.Drawing.Size(437, 65);
            this.panelClearCache.TabIndex = 3;
            this.panelClearCache.TabStop = false;
            this.panelClearCache.Text = " Clear cache ";
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
            this.panelDetectors.Controls.Add(this.bEndDetector);
            this.panelDetectors.Controls.Add(this.bStartDetector);
            this.panelDetectors.ForeColor = System.Drawing.Color.LightGray;
            this.panelDetectors.Location = new System.Drawing.Point(12, 252);
            this.panelDetectors.Name = "panelDetectors";
            this.panelDetectors.Size = new System.Drawing.Size(437, 65);
            this.panelDetectors.TabIndex = 4;
            this.panelDetectors.TabStop = false;
            this.panelDetectors.Text = " Tarkov Detector Intervals (in seconds)";
            // 
            // bStartDetector
            // 
            this.bStartDetector.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bStartDetector.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bStartDetector.FlatAppearance.BorderSize = 0;
            this.bStartDetector.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bStartDetector.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bStartDetector.Location = new System.Drawing.Point(15, 23);
            this.bStartDetector.Name = "bStartDetector";
            this.bStartDetector.Size = new System.Drawing.Size(207, 30);
            this.bStartDetector.TabIndex = 4;
            this.bStartDetector.Text = "Start detector: 0.5 seconds";
            this.bStartDetector.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bStartDetector.UseVisualStyleBackColor = true;
            this.bStartDetector.Click += new System.EventHandler(this.bStartDetector_Click);
            // 
            // bEndDetector
            // 
            this.bEndDetector.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bEndDetector.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bEndDetector.FlatAppearance.BorderSize = 0;
            this.bEndDetector.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bEndDetector.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bEndDetector.Location = new System.Drawing.Point(224, 23);
            this.bEndDetector.Name = "bEndDetector";
            this.bEndDetector.Size = new System.Drawing.Size(207, 30);
            this.bEndDetector.TabIndex = 5;
            this.bEndDetector.Text = "End detector: 0.5 seconds";
            this.bEndDetector.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bEndDetector.UseVisualStyleBackColor = true;
            this.bEndDetector.Click += new System.EventHandler(this.bEndDetector_Click);
            // 
            // panelLogFile
            // 
            this.panelLogFile.Controls.Add(this.bEnableOpenLog);
            this.panelLogFile.ForeColor = System.Drawing.Color.LightGray;
            this.panelLogFile.Location = new System.Drawing.Point(12, 492);
            this.panelLogFile.Name = "panelLogFile";
            this.panelLogFile.Size = new System.Drawing.Size(210, 65);
            this.panelLogFile.TabIndex = 5;
            this.panelLogFile.TabStop = false;
            this.panelLogFile.Text = " Open log file on SPT quit ";
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
            this.panelConfirmationQuit.Location = new System.Drawing.Point(12, 332);
            this.panelConfirmationQuit.Name = "panelConfirmationQuit";
            this.panelConfirmationQuit.Size = new System.Drawing.Size(437, 65);
            this.panelConfirmationQuit.TabIndex = 6;
            this.panelConfirmationQuit.TabStop = false;
            this.panelConfirmationQuit.Text = " Confirmation pop-ups (Stop SPT && Clear Cache) ";
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
            // bEnableAltCache
            // 
            this.bEnableAltCache.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bEnableAltCache.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bEnableAltCache.FlatAppearance.BorderSize = 0;
            this.bEnableAltCache.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bEnableAltCache.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bEnableAltCache.Location = new System.Drawing.Point(224, 23);
            this.bEnableAltCache.Name = "bEnableAltCache";
            this.bEnableAltCache.Size = new System.Drawing.Size(180, 30);
            this.bEnableAltCache.TabIndex = 5;
            this.bEnableAltCache.Text = "On SPT stop";
            this.bEnableAltCache.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bEnableAltCache.UseVisualStyleBackColor = true;
            this.bEnableAltCache.Click += new System.EventHandler(this.bEnableAltCache_Click);
            // 
            // panelServerOutput
            // 
            this.panelServerOutput.Controls.Add(this.bEnableServerOutput);
            this.panelServerOutput.ForeColor = System.Drawing.Color.LightGray;
            this.panelServerOutput.Location = new System.Drawing.Point(12, 412);
            this.panelServerOutput.Name = "panelServerOutput";
            this.panelServerOutput.Size = new System.Drawing.Size(437, 65);
            this.panelServerOutput.TabIndex = 7;
            this.panelServerOutput.TabStop = false;
            this.panelServerOutput.Text = " Automatic server console display ";
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
            this.panelServerError.Location = new System.Drawing.Point(239, 492);
            this.panelServerError.Name = "panelServerError";
            this.panelServerError.Size = new System.Drawing.Size(210, 65);
            this.panelServerError.TabIndex = 8;
            this.panelServerError.TabStop = false;
            this.panelServerError.Text = " Server error pop-ups ";
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
            // optionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(461, 577);
            this.Controls.Add(this.panelServerError);
            this.Controls.Add(this.panelServerOutput);
            this.Controls.Add(this.panelConfirmationQuit);
            this.Controls.Add(this.panelLogFile);
            this.Controls.Add(this.panelDetectors);
            this.Controls.Add(this.panelClearCache);
            this.Controls.Add(this.panelMisc);
            this.Controls.Add(this.panelEnableTimedAkiLauncher);
            this.Controls.Add(this.panelMinimizeOnSPTLaunch);
            this.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.ForeColor = System.Drawing.Color.LightGray;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "optionsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox panelMinimizeOnSPTLaunch;
        private System.Windows.Forms.GroupBox panelEnableTimedAkiLauncher;
        private System.Windows.Forms.Button bMinimize;
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
        private System.Windows.Forms.Button bEnableAltCache;
        private System.Windows.Forms.GroupBox panelServerOutput;
        private System.Windows.Forms.Button bEnableServerOutput;
        private System.Windows.Forms.GroupBox panelServerError;
        private System.Windows.Forms.Button bEnableServerErrors;
    }
}