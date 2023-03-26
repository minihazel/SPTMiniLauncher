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
            this.panelEnableTimedAkiLauncher = new System.Windows.Forms.GroupBox();
            this.bMinimize = new System.Windows.Forms.Button();
            this.bEnableTimed = new System.Windows.Forms.Button();
            this.panelMisc = new System.Windows.Forms.GroupBox();
            this.bRefresh = new System.Windows.Forms.Button();
            this.bReset = new System.Windows.Forms.Button();
            this.bResetThirdParty = new System.Windows.Forms.Button();
            this.panelMinimizeOnSPTLaunch.SuspendLayout();
            this.panelEnableTimedAkiLauncher.SuspendLayout();
            this.panelMisc.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMinimizeOnSPTLaunch
            // 
            this.panelMinimizeOnSPTLaunch.Controls.Add(this.bMinimize);
            this.panelMinimizeOnSPTLaunch.ForeColor = System.Drawing.Color.LightGray;
            this.panelMinimizeOnSPTLaunch.Location = new System.Drawing.Point(12, 12);
            this.panelMinimizeOnSPTLaunch.Name = "panelMinimizeOnSPTLaunch";
            this.panelMinimizeOnSPTLaunch.Size = new System.Drawing.Size(437, 65);
            this.panelMinimizeOnSPTLaunch.TabIndex = 0;
            this.panelMinimizeOnSPTLaunch.TabStop = false;
            this.panelMinimizeOnSPTLaunch.Text = " Minimize on SPT launch ";
            // 
            // panelEnableTimedAkiLauncher
            // 
            this.panelEnableTimedAkiLauncher.Controls.Add(this.bEnableTimed);
            this.panelEnableTimedAkiLauncher.ForeColor = System.Drawing.Color.LightGray;
            this.panelEnableTimedAkiLauncher.Location = new System.Drawing.Point(12, 92);
            this.panelEnableTimedAkiLauncher.Name = "panelEnableTimedAkiLauncher";
            this.panelEnableTimedAkiLauncher.Size = new System.Drawing.Size(437, 65);
            this.panelEnableTimedAkiLauncher.TabIndex = 1;
            this.panelEnableTimedAkiLauncher.TabStop = false;
            this.panelEnableTimedAkiLauncher.Text = " Enable timed Aki Launcher ";
            // 
            // bMinimize
            // 
            this.bMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bMinimize.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMinimize.ForeColor = System.Drawing.Color.IndianRed;
            this.bMinimize.Location = new System.Drawing.Point(15, 23);
            this.bMinimize.Name = "bMinimize";
            this.bMinimize.Size = new System.Drawing.Size(207, 30);
            this.bMinimize.TabIndex = 2;
            this.bMinimize.Text = "Disabled";
            this.bMinimize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bMinimize.UseVisualStyleBackColor = true;
            this.bMinimize.Click += new System.EventHandler(this.bMinimize_Click);
            // 
            // bEnableTimed
            // 
            this.bEnableTimed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bEnableTimed.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bEnableTimed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bEnableTimed.ForeColor = System.Drawing.Color.IndianRed;
            this.bEnableTimed.Location = new System.Drawing.Point(15, 23);
            this.bEnableTimed.Name = "bEnableTimed";
            this.bEnableTimed.Size = new System.Drawing.Size(207, 30);
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
            // bRefresh
            // 
            this.bRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bRefresh.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
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
            // bReset
            // 
            this.bReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bReset.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
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
            // bResetThirdParty
            // 
            this.bResetThirdParty.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bResetThirdParty.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
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
            // optionsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(461, 263);
            this.Controls.Add(this.panelMisc);
            this.Controls.Add(this.panelEnableTimedAkiLauncher);
            this.Controls.Add(this.panelMinimizeOnSPTLaunch);
            this.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.ForeColor = System.Drawing.Color.LightGray;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
    }
}