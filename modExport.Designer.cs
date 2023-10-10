namespace SPTMiniLauncher
{
    partial class modExport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(modExport));
            this.titleClientMods = new System.Windows.Forms.Label();
            this.titleServerMods = new System.Windows.Forms.Label();
            this.bSelectAllClientMods = new System.Windows.Forms.Button();
            this.bUnselectAllClientMods = new System.Windows.Forms.Button();
            this.bUnselectAllServerMods = new System.Windows.Forms.Button();
            this.bSelectAllServerMods = new System.Windows.Forms.Button();
            this.separatorExportMods = new System.Windows.Forms.Panel();
            this.panelExportSelectedMods = new System.Windows.Forms.GroupBox();
            this.bExportSelectedMods = new System.Windows.Forms.Button();
            this.panelClientMods = new System.Windows.Forms.Panel();
            this.panelServerMods = new System.Windows.Forms.Panel();
            this.bCounterClientMods = new System.Windows.Forms.Label();
            this.bCounterServerMods = new System.Windows.Forms.Label();
            this.statusExportingMods = new System.Windows.Forms.Label();
            this.chkOpenToggle = new System.Windows.Forms.CheckBox();
            this.chkPackageToggle = new System.Windows.Forms.CheckBox();
            this.panelExportSelectedMods.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleClientMods
            // 
            this.titleClientMods.BackColor = System.Drawing.Color.Transparent;
            this.titleClientMods.Location = new System.Drawing.Point(12, 30);
            this.titleClientMods.Name = "titleClientMods";
            this.titleClientMods.Padding = new System.Windows.Forms.Padding(3, 0, 0, 10);
            this.titleClientMods.Size = new System.Drawing.Size(275, 40);
            this.titleClientMods.TabIndex = 1;
            this.titleClientMods.Text = "CLIENT MODS";
            this.titleClientMods.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // titleServerMods
            // 
            this.titleServerMods.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.titleServerMods.BackColor = System.Drawing.Color.Transparent;
            this.titleServerMods.Location = new System.Drawing.Point(347, 30);
            this.titleServerMods.Name = "titleServerMods";
            this.titleServerMods.Padding = new System.Windows.Forms.Padding(3, 0, 0, 10);
            this.titleServerMods.Size = new System.Drawing.Size(275, 40);
            this.titleServerMods.TabIndex = 2;
            this.titleServerMods.Text = "SERVER MODS";
            this.titleServerMods.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // bSelectAllClientMods
            // 
            this.bSelectAllClientMods.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bSelectAllClientMods.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bSelectAllClientMods.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bSelectAllClientMods.FlatAppearance.BorderSize = 0;
            this.bSelectAllClientMods.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSelectAllClientMods.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bSelectAllClientMods.Location = new System.Drawing.Point(12, 529);
            this.bSelectAllClientMods.Name = "bSelectAllClientMods";
            this.bSelectAllClientMods.Size = new System.Drawing.Size(150, 30);
            this.bSelectAllClientMods.TabIndex = 3;
            this.bSelectAllClientMods.Text = "Select all";
            this.bSelectAllClientMods.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bSelectAllClientMods.UseVisualStyleBackColor = true;
            this.bSelectAllClientMods.Visible = false;
            this.bSelectAllClientMods.Click += new System.EventHandler(this.bSelectAllClientMods_Click);
            // 
            // bUnselectAllClientMods
            // 
            this.bUnselectAllClientMods.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bUnselectAllClientMods.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bUnselectAllClientMods.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bUnselectAllClientMods.FlatAppearance.BorderSize = 0;
            this.bUnselectAllClientMods.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bUnselectAllClientMods.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bUnselectAllClientMods.Location = new System.Drawing.Point(182, 529);
            this.bUnselectAllClientMods.Name = "bUnselectAllClientMods";
            this.bUnselectAllClientMods.Size = new System.Drawing.Size(150, 30);
            this.bUnselectAllClientMods.TabIndex = 4;
            this.bUnselectAllClientMods.Text = "Unselect all";
            this.bUnselectAllClientMods.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bUnselectAllClientMods.UseVisualStyleBackColor = true;
            this.bUnselectAllClientMods.Visible = false;
            this.bUnselectAllClientMods.Click += new System.EventHandler(this.bUnselectAllClientMods_Click);
            // 
            // bUnselectAllServerMods
            // 
            this.bUnselectAllServerMods.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bUnselectAllServerMods.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bUnselectAllServerMods.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bUnselectAllServerMods.FlatAppearance.BorderSize = 0;
            this.bUnselectAllServerMods.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bUnselectAllServerMods.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bUnselectAllServerMods.Location = new System.Drawing.Point(517, 529);
            this.bUnselectAllServerMods.Name = "bUnselectAllServerMods";
            this.bUnselectAllServerMods.Size = new System.Drawing.Size(150, 30);
            this.bUnselectAllServerMods.TabIndex = 6;
            this.bUnselectAllServerMods.Text = "Unselect all";
            this.bUnselectAllServerMods.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bUnselectAllServerMods.UseVisualStyleBackColor = true;
            this.bUnselectAllServerMods.Visible = false;
            this.bUnselectAllServerMods.Click += new System.EventHandler(this.bUnselectAllServerMods_Click);
            // 
            // bSelectAllServerMods
            // 
            this.bSelectAllServerMods.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bSelectAllServerMods.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bSelectAllServerMods.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bSelectAllServerMods.FlatAppearance.BorderSize = 0;
            this.bSelectAllServerMods.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSelectAllServerMods.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bSelectAllServerMods.Location = new System.Drawing.Point(347, 529);
            this.bSelectAllServerMods.Name = "bSelectAllServerMods";
            this.bSelectAllServerMods.Size = new System.Drawing.Size(150, 30);
            this.bSelectAllServerMods.TabIndex = 5;
            this.bSelectAllServerMods.Text = "Select all";
            this.bSelectAllServerMods.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bSelectAllServerMods.UseVisualStyleBackColor = true;
            this.bSelectAllServerMods.Visible = false;
            this.bSelectAllServerMods.Click += new System.EventHandler(this.bSelectAllServerMods_Click);
            // 
            // separatorExportMods
            // 
            this.separatorExportMods.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.separatorExportMods.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.separatorExportMods.Location = new System.Drawing.Point(339, 15);
            this.separatorExportMods.Name = "separatorExportMods";
            this.separatorExportMods.Size = new System.Drawing.Size(1, 630);
            this.separatorExportMods.TabIndex = 7;
            // 
            // panelExportSelectedMods
            // 
            this.panelExportSelectedMods.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExportSelectedMods.Controls.Add(this.bExportSelectedMods);
            this.panelExportSelectedMods.ForeColor = System.Drawing.Color.LightGray;
            this.panelExportSelectedMods.Location = new System.Drawing.Point(229, 575);
            this.panelExportSelectedMods.Name = "panelExportSelectedMods";
            this.panelExportSelectedMods.Size = new System.Drawing.Size(220, 65);
            this.panelExportSelectedMods.TabIndex = 8;
            this.panelExportSelectedMods.TabStop = false;
            this.panelExportSelectedMods.Text = " Export all selected mods ";
            this.panelExportSelectedMods.Visible = false;
            // 
            // bExportSelectedMods
            // 
            this.bExportSelectedMods.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bExportSelectedMods.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bExportSelectedMods.FlatAppearance.BorderSize = 0;
            this.bExportSelectedMods.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bExportSelectedMods.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bExportSelectedMods.Location = new System.Drawing.Point(15, 23);
            this.bExportSelectedMods.Name = "bExportSelectedMods";
            this.bExportSelectedMods.Size = new System.Drawing.Size(180, 30);
            this.bExportSelectedMods.TabIndex = 3;
            this.bExportSelectedMods.Text = "Export";
            this.bExportSelectedMods.UseVisualStyleBackColor = true;
            this.bExportSelectedMods.Click += new System.EventHandler(this.bExportSelectedMods_Click);
            // 
            // panelClientMods
            // 
            this.panelClientMods.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelClientMods.AutoScroll = true;
            this.panelClientMods.Location = new System.Drawing.Point(12, 81);
            this.panelClientMods.Name = "panelClientMods";
            this.panelClientMods.Size = new System.Drawing.Size(320, 567);
            this.panelClientMods.TabIndex = 9;
            // 
            // panelServerMods
            // 
            this.panelServerMods.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelServerMods.AutoScroll = true;
            this.panelServerMods.Location = new System.Drawing.Point(347, 81);
            this.panelServerMods.Name = "panelServerMods";
            this.panelServerMods.Size = new System.Drawing.Size(320, 567);
            this.panelServerMods.TabIndex = 10;
            // 
            // bCounterClientMods
            // 
            this.bCounterClientMods.BackColor = System.Drawing.Color.Transparent;
            this.bCounterClientMods.Font = new System.Drawing.Font("Bahnschrift Light", 14F);
            this.bCounterClientMods.Location = new System.Drawing.Point(287, 30);
            this.bCounterClientMods.Name = "bCounterClientMods";
            this.bCounterClientMods.Padding = new System.Windows.Forms.Padding(2, 0, 0, 8);
            this.bCounterClientMods.Size = new System.Drawing.Size(45, 40);
            this.bCounterClientMods.TabIndex = 11;
            this.bCounterClientMods.Text = "0";
            this.bCounterClientMods.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // bCounterServerMods
            // 
            this.bCounterServerMods.BackColor = System.Drawing.Color.Transparent;
            this.bCounterServerMods.Font = new System.Drawing.Font("Bahnschrift Light", 14F);
            this.bCounterServerMods.Location = new System.Drawing.Point(622, 30);
            this.bCounterServerMods.Name = "bCounterServerMods";
            this.bCounterServerMods.Padding = new System.Windows.Forms.Padding(2, 0, 0, 8);
            this.bCounterServerMods.Size = new System.Drawing.Size(45, 40);
            this.bCounterServerMods.TabIndex = 12;
            this.bCounterServerMods.Text = "0";
            this.bCounterServerMods.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // statusExportingMods
            // 
            this.statusExportingMods.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.statusExportingMods.BackColor = System.Drawing.Color.Transparent;
            this.statusExportingMods.Location = new System.Drawing.Point(12, 588);
            this.statusExportingMods.Name = "statusExportingMods";
            this.statusExportingMods.Padding = new System.Windows.Forms.Padding(3, 0, 0, 7);
            this.statusExportingMods.Size = new System.Drawing.Size(214, 40);
            this.statusExportingMods.TabIndex = 13;
            this.statusExportingMods.Text = "Exporting mods...";
            this.statusExportingMods.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.statusExportingMods.Visible = false;
            // 
            // chkOpenToggle
            // 
            this.chkOpenToggle.AutoSize = true;
            this.chkOpenToggle.Location = new System.Drawing.Point(15, 10);
            this.chkOpenToggle.Name = "chkOpenToggle";
            this.chkOpenToggle.Size = new System.Drawing.Size(269, 21);
            this.chkOpenToggle.TabIndex = 14;
            this.chkOpenToggle.Text = "Only open mod folder on double-click";
            this.chkOpenToggle.UseVisualStyleBackColor = true;
            // 
            // chkPackageToggle
            // 
            this.chkPackageToggle.AutoSize = true;
            this.chkPackageToggle.Location = new System.Drawing.Point(347, 10);
            this.chkPackageToggle.Name = "chkPackageToggle";
            this.chkPackageToggle.Size = new System.Drawing.Size(202, 21);
            this.chkPackageToggle.TabIndex = 15;
            this.chkPackageToggle.Text = "Open package.json on open";
            this.chkPackageToggle.UseVisualStyleBackColor = true;
            // 
            // modExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(679, 661);
            this.Controls.Add(this.chkPackageToggle);
            this.Controls.Add(this.chkOpenToggle);
            this.Controls.Add(this.separatorExportMods);
            this.Controls.Add(this.bCounterServerMods);
            this.Controls.Add(this.bCounterClientMods);
            this.Controls.Add(this.panelServerMods);
            this.Controls.Add(this.panelClientMods);
            this.Controls.Add(this.panelExportSelectedMods);
            this.Controls.Add(this.bUnselectAllServerMods);
            this.Controls.Add(this.bSelectAllServerMods);
            this.Controls.Add(this.bUnselectAllClientMods);
            this.Controls.Add(this.bSelectAllClientMods);
            this.Controls.Add(this.titleServerMods);
            this.Controls.Add(this.titleClientMods);
            this.Controls.Add(this.statusExportingMods);
            this.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.ForeColor = System.Drawing.Color.LightGray;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(695, 570);
            this.Name = "modExport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mod Viewer";
            this.Load += new System.EventHandler(this.modExport_Load);
            this.panelExportSelectedMods.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label titleClientMods;
        private System.Windows.Forms.Label titleServerMods;
        private System.Windows.Forms.Button bSelectAllClientMods;
        private System.Windows.Forms.Button bUnselectAllClientMods;
        private System.Windows.Forms.Button bUnselectAllServerMods;
        private System.Windows.Forms.Button bSelectAllServerMods;
        private System.Windows.Forms.Panel separatorExportMods;
        private System.Windows.Forms.GroupBox panelExportSelectedMods;
        private System.Windows.Forms.Button bExportSelectedMods;
        private System.Windows.Forms.Panel panelClientMods;
        private System.Windows.Forms.Panel panelServerMods;
        private System.Windows.Forms.Label bCounterClientMods;
        private System.Windows.Forms.Label bCounterServerMods;
        private System.Windows.Forms.Label statusExportingMods;
        private System.Windows.Forms.CheckBox chkOpenToggle;
        private System.Windows.Forms.CheckBox chkPackageToggle;
    }
}