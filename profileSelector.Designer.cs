namespace SPTMiniLauncher
{
    partial class profileSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(profileSelector));
            this.bCancel = new System.Windows.Forms.Label();
            this.panelProfiles = new System.Windows.Forms.GroupBox();
            this.panelProfilesPlaceholder = new System.Windows.Forms.Label();
            this.bSelection = new System.Windows.Forms.Label();
            this.panelProfiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bCancel.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.bCancel.Location = new System.Drawing.Point(348, 294);
            this.bCancel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(100, 37);
            this.bCancel.TabIndex = 8;
            this.bCancel.Text = "Cancel";
            this.bCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            this.bCancel.MouseEnter += new System.EventHandler(this.bCancel_MouseEnter);
            this.bCancel.MouseLeave += new System.EventHandler(this.bCancel_MouseLeave);
            // 
            // panelProfiles
            // 
            this.panelProfiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelProfiles.Controls.Add(this.panelProfilesPlaceholder);
            this.panelProfiles.ForeColor = System.Drawing.Color.LightGray;
            this.panelProfiles.Location = new System.Drawing.Point(12, 12);
            this.panelProfiles.Name = "panelProfiles";
            this.panelProfiles.Size = new System.Drawing.Size(437, 279);
            this.panelProfiles.TabIndex = 9;
            this.panelProfiles.TabStop = false;
            this.panelProfiles.Text = " Select a profile to open ";
            // 
            // panelProfilesPlaceholder
            // 
            this.panelProfilesPlaceholder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelProfilesPlaceholder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.panelProfilesPlaceholder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelProfilesPlaceholder.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.panelProfilesPlaceholder.Location = new System.Drawing.Point(1, 29);
            this.panelProfilesPlaceholder.Name = "panelProfilesPlaceholder";
            this.panelProfilesPlaceholder.Size = new System.Drawing.Size(435, 30);
            this.panelProfilesPlaceholder.TabIndex = 6;
            this.panelProfilesPlaceholder.Text = "Profile placeholder";
            this.panelProfilesPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.panelProfilesPlaceholder.Visible = false;
            // 
            // bSelection
            // 
            this.bSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bSelection.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bSelection.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.bSelection.Location = new System.Drawing.Point(9, 294);
            this.bSelection.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.bSelection.Name = "bSelection";
            this.bSelection.Size = new System.Drawing.Size(331, 37);
            this.bSelection.TabIndex = 10;
            this.bSelection.Text = "Placeholder";
            this.bSelection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bSelection.Visible = false;
            // 
            // profileSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(461, 340);
            this.Controls.Add(this.bSelection);
            this.Controls.Add(this.panelProfiles);
            this.Controls.Add(this.bCancel);
            this.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.ForeColor = System.Drawing.Color.LightGray;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(477, 379);
            this.Name = "profileSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select profile";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.profileSelector_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.profileSelector_FormClosed);
            this.Load += new System.EventHandler(this.profileSelector_Load);
            this.panelProfiles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label bCancel;
        private System.Windows.Forms.GroupBox panelProfiles;
        private System.Windows.Forms.Label panelProfilesPlaceholder;
        private System.Windows.Forms.Label bSelection;
    }
}