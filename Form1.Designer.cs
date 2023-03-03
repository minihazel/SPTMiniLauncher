namespace SPTMiniLauncher
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.boxPathBox = new System.Windows.Forms.GroupBox();
            this.boxBrowse = new System.Windows.Forms.Label();
            this.boxOpenIn = new System.Windows.Forms.Label();
            this.boxPathSeparator = new System.Windows.Forms.Panel();
            this.boxPath = new System.Windows.Forms.TextBox();
            this.boxServers = new System.Windows.Forms.Panel();
            this.boxServerPlaceholder = new System.Windows.Forms.Label();
            this.boxServersTitle = new System.Windows.Forms.Label();
            this.boxServersSeparator = new System.Windows.Forms.Panel();
            this.boxSelectedServer = new System.Windows.Forms.Panel();
            this.boxSelectedServerPlaceholder = new System.Windows.Forms.Label();
            this.boxSelectedServerTitle = new System.Windows.Forms.Label();
            this.boxSelectedServerSeparator = new System.Windows.Forms.Panel();
            this.bResetApp = new System.Windows.Forms.Label();
            this.bResetThirdParty = new System.Windows.Forms.Label();
            this.bRefresh = new System.Windows.Forms.Label();
            this.boxPathBox.SuspendLayout();
            this.boxServers.SuspendLayout();
            this.boxSelectedServer.SuspendLayout();
            this.SuspendLayout();
            // 
            // boxPathBox
            // 
            this.boxPathBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxPathBox.Controls.Add(this.boxBrowse);
            this.boxPathBox.Controls.Add(this.boxOpenIn);
            this.boxPathBox.Controls.Add(this.boxPathSeparator);
            this.boxPathBox.Controls.Add(this.boxPath);
            this.boxPathBox.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.boxPathBox.ForeColor = System.Drawing.Color.LightGray;
            this.boxPathBox.Location = new System.Drawing.Point(17, 12);
            this.boxPathBox.Name = "boxPathBox";
            this.boxPathBox.Size = new System.Drawing.Size(644, 55);
            this.boxPathBox.TabIndex = 0;
            this.boxPathBox.TabStop = false;
            this.boxPathBox.Text = " Path ";
            // 
            // boxBrowse
            // 
            this.boxBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.boxBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.boxBrowse.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.boxBrowse.Location = new System.Drawing.Point(451, 11);
            this.boxBrowse.Name = "boxBrowse";
            this.boxBrowse.Size = new System.Drawing.Size(87, 38);
            this.boxBrowse.TabIndex = 3;
            this.boxBrowse.Text = "Browse";
            this.boxBrowse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.boxBrowse.Click += new System.EventHandler(this.boxBrowse_Click);
            this.boxBrowse.MouseEnter += new System.EventHandler(this.boxBrowse_MouseEnter);
            this.boxBrowse.MouseLeave += new System.EventHandler(this.boxBrowse_MouseLeave);
            // 
            // boxOpenIn
            // 
            this.boxOpenIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.boxOpenIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.boxOpenIn.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.boxOpenIn.Location = new System.Drawing.Point(551, 11);
            this.boxOpenIn.Name = "boxOpenIn";
            this.boxOpenIn.Size = new System.Drawing.Size(87, 38);
            this.boxOpenIn.TabIndex = 1;
            this.boxOpenIn.Text = "Open";
            this.boxOpenIn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.boxOpenIn.Click += new System.EventHandler(this.boxOpenIn_Click);
            this.boxOpenIn.MouseEnter += new System.EventHandler(this.boxOpenIn_MouseEnter);
            this.boxOpenIn.MouseLeave += new System.EventHandler(this.boxOpenIn_MouseLeave);
            // 
            // boxPathSeparator
            // 
            this.boxPathSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxPathSeparator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boxPathSeparator.Location = new System.Drawing.Point(544, 16);
            this.boxPathSeparator.Name = "boxPathSeparator";
            this.boxPathSeparator.Size = new System.Drawing.Size(1, 30);
            this.boxPathSeparator.TabIndex = 1;
            // 
            // boxPath
            // 
            this.boxPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.boxPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.boxPath.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.boxPath.ForeColor = System.Drawing.Color.Silver;
            this.boxPath.Location = new System.Drawing.Point(12, 24);
            this.boxPath.Name = "boxPath";
            this.boxPath.Size = new System.Drawing.Size(441, 17);
            this.boxPath.TabIndex = 2;
            this.boxPath.Text = "Placeholder";
            this.boxPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.boxPath_KeyDown);
            // 
            // boxServers
            // 
            this.boxServers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxServers.AutoScroll = true;
            this.boxServers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boxServers.Controls.Add(this.boxServerPlaceholder);
            this.boxServers.Controls.Add(this.boxServersTitle);
            this.boxServers.Controls.Add(this.boxServersSeparator);
            this.boxServers.Location = new System.Drawing.Point(17, 87);
            this.boxServers.Name = "boxServers";
            this.boxServers.Size = new System.Drawing.Size(314, 442);
            this.boxServers.TabIndex = 1;
            // 
            // boxServerPlaceholder
            // 
            this.boxServerPlaceholder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxServerPlaceholder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.boxServerPlaceholder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.boxServerPlaceholder.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.boxServerPlaceholder.Location = new System.Drawing.Point(0, 34);
            this.boxServerPlaceholder.Name = "boxServerPlaceholder";
            this.boxServerPlaceholder.Size = new System.Drawing.Size(312, 30);
            this.boxServerPlaceholder.TabIndex = 5;
            this.boxServerPlaceholder.Text = "Server placeholder";
            this.boxServerPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.boxServerPlaceholder.Visible = false;
            // 
            // boxServersTitle
            // 
            this.boxServersTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxServersTitle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.boxServersTitle.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.boxServersTitle.Location = new System.Drawing.Point(3, 0);
            this.boxServersTitle.Name = "boxServersTitle";
            this.boxServersTitle.Size = new System.Drawing.Size(306, 30);
            this.boxServersTitle.TabIndex = 4;
            this.boxServersTitle.Text = "Listed servers";
            this.boxServersTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // boxServersSeparator
            // 
            this.boxServersSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxServersSeparator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boxServersSeparator.Location = new System.Drawing.Point(-1, 33);
            this.boxServersSeparator.Name = "boxServersSeparator";
            this.boxServersSeparator.Size = new System.Drawing.Size(314, 1);
            this.boxServersSeparator.TabIndex = 2;
            // 
            // boxSelectedServer
            // 
            this.boxSelectedServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxSelectedServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boxSelectedServer.Controls.Add(this.boxSelectedServerPlaceholder);
            this.boxSelectedServer.Controls.Add(this.boxSelectedServerTitle);
            this.boxSelectedServer.Controls.Add(this.boxSelectedServerSeparator);
            this.boxSelectedServer.Location = new System.Drawing.Point(347, 87);
            this.boxSelectedServer.Name = "boxSelectedServer";
            this.boxSelectedServer.Size = new System.Drawing.Size(314, 442);
            this.boxSelectedServer.TabIndex = 2;
            // 
            // boxSelectedServerPlaceholder
            // 
            this.boxSelectedServerPlaceholder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxSelectedServerPlaceholder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.boxSelectedServerPlaceholder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.boxSelectedServerPlaceholder.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.boxSelectedServerPlaceholder.Location = new System.Drawing.Point(0, 34);
            this.boxSelectedServerPlaceholder.Name = "boxSelectedServerPlaceholder";
            this.boxSelectedServerPlaceholder.Size = new System.Drawing.Size(312, 30);
            this.boxSelectedServerPlaceholder.TabIndex = 8;
            this.boxSelectedServerPlaceholder.Text = "Server placeholder";
            this.boxSelectedServerPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.boxSelectedServerPlaceholder.Visible = false;
            // 
            // boxSelectedServerTitle
            // 
            this.boxSelectedServerTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxSelectedServerTitle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.boxSelectedServerTitle.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.boxSelectedServerTitle.Location = new System.Drawing.Point(3, 0);
            this.boxSelectedServerTitle.Name = "boxSelectedServerTitle";
            this.boxSelectedServerTitle.Size = new System.Drawing.Size(306, 30);
            this.boxSelectedServerTitle.TabIndex = 7;
            this.boxSelectedServerTitle.Text = "SPT Placeholder";
            this.boxSelectedServerTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.boxSelectedServerTitle.Click += new System.EventHandler(this.boxSelectedServerTitle_Click_1);
            this.boxSelectedServerTitle.MouseEnter += new System.EventHandler(this.boxSelectedServerTitle_MouseEnter_1);
            this.boxSelectedServerTitle.MouseLeave += new System.EventHandler(this.boxSelectedServerTitle_MouseLeave_1);
            // 
            // boxSelectedServerSeparator
            // 
            this.boxSelectedServerSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxSelectedServerSeparator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boxSelectedServerSeparator.Location = new System.Drawing.Point(-1, 33);
            this.boxSelectedServerSeparator.Name = "boxSelectedServerSeparator";
            this.boxSelectedServerSeparator.Size = new System.Drawing.Size(314, 1);
            this.boxSelectedServerSeparator.TabIndex = 6;
            // 
            // bResetApp
            // 
            this.bResetApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bResetApp.AutoSize = true;
            this.bResetApp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bResetApp.Font = new System.Drawing.Font("Bahnschrift Light", 8F);
            this.bResetApp.Location = new System.Drawing.Point(624, 532);
            this.bResetApp.Name = "bResetApp";
            this.bResetApp.Size = new System.Drawing.Size(36, 13);
            this.bResetApp.TabIndex = 3;
            this.bResetApp.Text = "Reset";
            this.bResetApp.Click += new System.EventHandler(this.bResetApp_Click);
            this.bResetApp.MouseEnter += new System.EventHandler(this.bResetApp_MouseEnter);
            this.bResetApp.MouseLeave += new System.EventHandler(this.bResetApp_MouseLeave);
            // 
            // bResetThirdParty
            // 
            this.bResetThirdParty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bResetThirdParty.AutoSize = true;
            this.bResetThirdParty.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bResetThirdParty.Font = new System.Drawing.Font("Bahnschrift Light", 8F);
            this.bResetThirdParty.Location = new System.Drawing.Point(499, 532);
            this.bResetThirdParty.Name = "bResetThirdParty";
            this.bResetThirdParty.Size = new System.Drawing.Size(119, 13);
            this.bResetThirdParty.TabIndex = 4;
            this.bResetThirdParty.Text = "Reset third party apps";
            this.bResetThirdParty.Click += new System.EventHandler(this.bResetThirdParty_Click);
            this.bResetThirdParty.MouseEnter += new System.EventHandler(this.bResetThirdParty_MouseEnter);
            this.bResetThirdParty.MouseLeave += new System.EventHandler(this.bResetThirdParty_MouseLeave);
            // 
            // bRefresh
            // 
            this.bRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bRefresh.AutoSize = true;
            this.bRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bRefresh.Font = new System.Drawing.Font("Bahnschrift Light", 8F);
            this.bRefresh.Location = new System.Drawing.Point(14, 532);
            this.bRefresh.Name = "bRefresh";
            this.bRefresh.Size = new System.Drawing.Size(46, 13);
            this.bRefresh.TabIndex = 5;
            this.bRefresh.Text = "Refresh";
            this.bRefresh.Click += new System.EventHandler(this.bRefresh_Click);
            this.bRefresh.MouseEnter += new System.EventHandler(this.bRefresh_MouseEnter);
            this.bRefresh.MouseLeave += new System.EventHandler(this.bRefresh_MouseLeave);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(679, 551);
            this.Controls.Add(this.bRefresh);
            this.Controls.Add(this.bResetThirdParty);
            this.Controls.Add(this.bResetApp);
            this.Controls.Add(this.boxSelectedServer);
            this.Controls.Add(this.boxServers);
            this.Controls.Add(this.boxPathBox);
            this.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.ForeColor = System.Drawing.Color.LightGray;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(695, 590);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SPT Mini Launcher";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.boxPathBox.ResumeLayout(false);
            this.boxPathBox.PerformLayout();
            this.boxServers.ResumeLayout(false);
            this.boxSelectedServer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox boxPathBox;
        private System.Windows.Forms.Panel boxPathSeparator;
        private System.Windows.Forms.TextBox boxPath;
        private System.Windows.Forms.Label boxOpenIn;
        private System.Windows.Forms.Label boxBrowse;
        private System.Windows.Forms.Panel boxServers;
        private System.Windows.Forms.Panel boxSelectedServer;
        private System.Windows.Forms.Panel boxServersSeparator;
        private System.Windows.Forms.Label boxServersTitle;
        private System.Windows.Forms.Label boxServerPlaceholder;
        private System.Windows.Forms.Label boxSelectedServerPlaceholder;
        private System.Windows.Forms.Label boxSelectedServerTitle;
        private System.Windows.Forms.Panel boxSelectedServerSeparator;
        private System.Windows.Forms.Label bResetApp;
        private System.Windows.Forms.Label bResetThirdParty;
        private System.Windows.Forms.Label bRefresh;
    }
}

