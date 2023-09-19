namespace SPTMiniLauncher
{
    partial class addThirdParty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(addThirdParty));
            this.panelCustomName = new System.Windows.Forms.GroupBox();
            this.txtCustomName = new System.Windows.Forms.TextBox();
            this.panelPathToApp = new System.Windows.Forms.GroupBox();
            this.bBrowsePath = new System.Windows.Forms.Button();
            this.txtPathToApp = new System.Windows.Forms.TextBox();
            this.panelApplyThirdPartyApp = new System.Windows.Forms.GroupBox();
            this.bApplyThirdPartyApp = new System.Windows.Forms.Button();
            this.panelToolType = new System.Windows.Forms.GroupBox();
            this.bToolType = new System.Windows.Forms.Button();
            this.panelCustomName.SuspendLayout();
            this.panelPathToApp.SuspendLayout();
            this.panelApplyThirdPartyApp.SuspendLayout();
            this.panelToolType.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCustomName
            // 
            this.panelCustomName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCustomName.Controls.Add(this.txtCustomName);
            this.panelCustomName.ForeColor = System.Drawing.Color.LightGray;
            this.panelCustomName.Location = new System.Drawing.Point(12, 12);
            this.panelCustomName.Name = "panelCustomName";
            this.panelCustomName.Size = new System.Drawing.Size(417, 65);
            this.panelCustomName.TabIndex = 3;
            this.panelCustomName.TabStop = false;
            this.panelCustomName.Text = "Custom name";
            // 
            // txtCustomName
            // 
            this.txtCustomName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.txtCustomName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCustomName.Font = new System.Drawing.Font("Bahnschrift Light", 16F);
            this.txtCustomName.ForeColor = System.Drawing.Color.LightGray;
            this.txtCustomName.Location = new System.Drawing.Point(15, 23);
            this.txtCustomName.Name = "txtCustomName";
            this.txtCustomName.Size = new System.Drawing.Size(396, 26);
            this.txtCustomName.TabIndex = 0;
            this.txtCustomName.Text = "Placeholder";
            this.txtCustomName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomName_KeyDown);
            // 
            // panelPathToApp
            // 
            this.panelPathToApp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPathToApp.Controls.Add(this.bBrowsePath);
            this.panelPathToApp.Controls.Add(this.txtPathToApp);
            this.panelPathToApp.ForeColor = System.Drawing.Color.LightGray;
            this.panelPathToApp.Location = new System.Drawing.Point(12, 92);
            this.panelPathToApp.Name = "panelPathToApp";
            this.panelPathToApp.Size = new System.Drawing.Size(655, 65);
            this.panelPathToApp.TabIndex = 4;
            this.panelPathToApp.TabStop = false;
            this.panelPathToApp.Text = "Path to app";
            // 
            // bBrowsePath
            // 
            this.bBrowsePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bBrowsePath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bBrowsePath.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bBrowsePath.FlatAppearance.BorderSize = 0;
            this.bBrowsePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bBrowsePath.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bBrowsePath.Location = new System.Drawing.Point(529, 22);
            this.bBrowsePath.Name = "bBrowsePath";
            this.bBrowsePath.Size = new System.Drawing.Size(120, 30);
            this.bBrowsePath.TabIndex = 2;
            this.bBrowsePath.Text = "Browse";
            this.bBrowsePath.UseVisualStyleBackColor = true;
            this.bBrowsePath.Click += new System.EventHandler(this.bBrowsePath_Click);
            // 
            // txtPathToApp
            // 
            this.txtPathToApp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPathToApp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.txtPathToApp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPathToApp.Font = new System.Drawing.Font("Bahnschrift Light", 12F);
            this.txtPathToApp.ForeColor = System.Drawing.Color.LightGray;
            this.txtPathToApp.Location = new System.Drawing.Point(15, 26);
            this.txtPathToApp.Name = "txtPathToApp";
            this.txtPathToApp.Size = new System.Drawing.Size(508, 20);
            this.txtPathToApp.TabIndex = 1;
            this.txtPathToApp.Text = "Placeholder";
            this.txtPathToApp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPathToApp_KeyDown);
            // 
            // panelApplyThirdPartyApp
            // 
            this.panelApplyThirdPartyApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panelApplyThirdPartyApp.Controls.Add(this.bApplyThirdPartyApp);
            this.panelApplyThirdPartyApp.ForeColor = System.Drawing.Color.LightGray;
            this.panelApplyThirdPartyApp.Location = new System.Drawing.Point(447, 192);
            this.panelApplyThirdPartyApp.Name = "panelApplyThirdPartyApp";
            this.panelApplyThirdPartyApp.Size = new System.Drawing.Size(220, 65);
            this.panelApplyThirdPartyApp.TabIndex = 7;
            this.panelApplyThirdPartyApp.TabStop = false;
            this.panelApplyThirdPartyApp.Text = "Apply third party app";
            // 
            // bApplyThirdPartyApp
            // 
            this.bApplyThirdPartyApp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bApplyThirdPartyApp.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bApplyThirdPartyApp.FlatAppearance.BorderSize = 0;
            this.bApplyThirdPartyApp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bApplyThirdPartyApp.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bApplyThirdPartyApp.Location = new System.Drawing.Point(15, 23);
            this.bApplyThirdPartyApp.Name = "bApplyThirdPartyApp";
            this.bApplyThirdPartyApp.Size = new System.Drawing.Size(180, 30);
            this.bApplyThirdPartyApp.TabIndex = 3;
            this.bApplyThirdPartyApp.Text = "Apply";
            this.bApplyThirdPartyApp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bApplyThirdPartyApp.UseVisualStyleBackColor = true;
            this.bApplyThirdPartyApp.Click += new System.EventHandler(this.bApplyThirdPartyApp_Click);
            // 
            // panelToolType
            // 
            this.panelToolType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolType.Controls.Add(this.bToolType);
            this.panelToolType.ForeColor = System.Drawing.Color.LightGray;
            this.panelToolType.Location = new System.Drawing.Point(447, 12);
            this.panelToolType.Name = "panelToolType";
            this.panelToolType.Size = new System.Drawing.Size(220, 65);
            this.panelToolType.TabIndex = 8;
            this.panelToolType.TabStop = false;
            this.panelToolType.Text = "Is this custom tool a folder?";
            // 
            // bToolType
            // 
            this.bToolType.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bToolType.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bToolType.FlatAppearance.BorderSize = 0;
            this.bToolType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bToolType.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bToolType.Location = new System.Drawing.Point(15, 23);
            this.bToolType.Name = "bToolType";
            this.bToolType.Size = new System.Drawing.Size(180, 30);
            this.bToolType.TabIndex = 3;
            this.bToolType.Text = "Folder";
            this.bToolType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bToolType.UseVisualStyleBackColor = true;
            this.bToolType.Click += new System.EventHandler(this.bToolType_Click);
            this.bToolType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bToolType_KeyDown);
            this.bToolType.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bToolType_MouseDown);
            // 
            // addThirdParty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(679, 269);
            this.Controls.Add(this.panelToolType);
            this.Controls.Add(this.panelApplyThirdPartyApp);
            this.Controls.Add(this.panelPathToApp);
            this.Controls.Add(this.panelCustomName);
            this.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.ForeColor = System.Drawing.Color.LightGray;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "addThirdParty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add third party content";
            this.Load += new System.EventHandler(this.addThirdParty_Load);
            this.panelCustomName.ResumeLayout(false);
            this.panelCustomName.PerformLayout();
            this.panelPathToApp.ResumeLayout(false);
            this.panelPathToApp.PerformLayout();
            this.panelApplyThirdPartyApp.ResumeLayout(false);
            this.panelToolType.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox panelCustomName;
        public System.Windows.Forms.TextBox txtCustomName;
        public System.Windows.Forms.GroupBox panelPathToApp;
        public System.Windows.Forms.TextBox txtPathToApp;
        public System.Windows.Forms.GroupBox panelApplyThirdPartyApp;
        public System.Windows.Forms.Button bApplyThirdPartyApp;
        public System.Windows.Forms.Button bBrowsePath;
        public System.Windows.Forms.GroupBox panelToolType;
        public System.Windows.Forms.Button bToolType;
    }
}