namespace SPTMiniLauncher
{
    partial class Modlist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Modlist));
            this.boxServerMods = new System.Windows.Forms.GroupBox();
            this.boxServerOption = new System.Windows.Forms.Label();
            this.boxServerSeparator = new System.Windows.Forms.Panel();
            this.boxServerList = new System.Windows.Forms.ComboBox();
            this.boxServerPlaceholder = new System.Windows.Forms.Label();
            this.boxServerMods.SuspendLayout();
            this.SuspendLayout();
            // 
            // boxServerMods
            // 
            this.boxServerMods.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxServerMods.Controls.Add(this.boxServerList);
            this.boxServerMods.Controls.Add(this.boxServerOption);
            this.boxServerMods.Controls.Add(this.boxServerSeparator);
            this.boxServerMods.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.boxServerMods.ForeColor = System.Drawing.Color.LightGray;
            this.boxServerMods.Location = new System.Drawing.Point(17, 12);
            this.boxServerMods.Name = "boxServerMods";
            this.boxServerMods.Size = new System.Drawing.Size(644, 55);
            this.boxServerMods.TabIndex = 1;
            this.boxServerMods.TabStop = false;
            this.boxServerMods.Text = " Server mods ";
            // 
            // boxServerOption
            // 
            this.boxServerOption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.boxServerOption.Cursor = System.Windows.Forms.Cursors.Hand;
            this.boxServerOption.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.boxServerOption.Location = new System.Drawing.Point(551, 11);
            this.boxServerOption.Name = "boxServerOption";
            this.boxServerOption.Size = new System.Drawing.Size(87, 38);
            this.boxServerOption.TabIndex = 1;
            this.boxServerOption.Text = "Open";
            this.boxServerOption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.boxServerOption.Click += new System.EventHandler(this.boxServerOption_Click);
            this.boxServerOption.MouseDown += new System.Windows.Forms.MouseEventHandler(this.boxServerOption_MouseDown);
            this.boxServerOption.MouseEnter += new System.EventHandler(this.boxServerOption_MouseEnter);
            this.boxServerOption.MouseLeave += new System.EventHandler(this.boxServerOption_MouseLeave);
            // 
            // boxServerSeparator
            // 
            this.boxServerSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxServerSeparator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.boxServerSeparator.Location = new System.Drawing.Point(544, 16);
            this.boxServerSeparator.Name = "boxServerSeparator";
            this.boxServerSeparator.Size = new System.Drawing.Size(1, 30);
            this.boxServerSeparator.TabIndex = 1;
            // 
            // boxServerList
            // 
            this.boxServerList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxServerList.Font = new System.Drawing.Font("Bahnschrift Light", 11F);
            this.boxServerList.FormattingEnabled = true;
            this.boxServerList.Location = new System.Drawing.Point(12, 19);
            this.boxServerList.Name = "boxServerList";
            this.boxServerList.Size = new System.Drawing.Size(516, 26);
            this.boxServerList.TabIndex = 4;
            // 
            // boxServerPlaceholder
            // 
            this.boxServerPlaceholder.AutoSize = true;
            this.boxServerPlaceholder.Location = new System.Drawing.Point(14, -1);
            this.boxServerPlaceholder.Name = "boxServerPlaceholder";
            this.boxServerPlaceholder.Size = new System.Drawing.Size(84, 17);
            this.boxServerPlaceholder.TabIndex = 2;
            this.boxServerPlaceholder.Text = "placeholder";
            this.boxServerPlaceholder.Visible = false;
            // 
            // Modlist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(679, 86);
            this.Controls.Add(this.boxServerPlaceholder);
            this.Controls.Add(this.boxServerMods);
            this.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.ForeColor = System.Drawing.Color.LightGray;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Modlist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modlist";
            this.Load += new System.EventHandler(this.Modlist_Load);
            this.boxServerMods.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox boxServerMods;
        private System.Windows.Forms.Label boxServerOption;
        private System.Windows.Forms.Panel boxServerSeparator;
        private System.Windows.Forms.Label boxServerPlaceholder;
        public System.Windows.Forms.ComboBox boxServerList;
    }
}