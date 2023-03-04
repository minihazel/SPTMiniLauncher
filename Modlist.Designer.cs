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
            this.boxModsType = new System.Windows.Forms.GroupBox();
            this.boxModList = new System.Windows.Forms.ComboBox();
            this.boxServerOption = new System.Windows.Forms.Label();
            this.boxServerSeparator = new System.Windows.Forms.Panel();
            this.boxPathPlaceholder = new System.Windows.Forms.Label();
            this.loneServer = new System.Windows.Forms.Label();
            this.watermark = new System.Windows.Forms.Label();
            this.boxModsType.SuspendLayout();
            this.SuspendLayout();
            // 
            // boxModsType
            // 
            this.boxModsType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxModsType.Controls.Add(this.boxModList);
            this.boxModsType.Controls.Add(this.boxServerOption);
            this.boxModsType.Controls.Add(this.boxServerSeparator);
            this.boxModsType.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.boxModsType.ForeColor = System.Drawing.Color.LightGray;
            this.boxModsType.Location = new System.Drawing.Point(17, 12);
            this.boxModsType.Name = "boxModsType";
            this.boxModsType.Size = new System.Drawing.Size(644, 55);
            this.boxModsType.TabIndex = 1;
            this.boxModsType.TabStop = false;
            this.boxModsType.Text = " Server mods ";
            // 
            // boxModList
            // 
            this.boxModList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.boxModList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxModList.Font = new System.Drawing.Font("Bahnschrift Light", 11F);
            this.boxModList.FormattingEnabled = true;
            this.boxModList.Location = new System.Drawing.Point(12, 19);
            this.boxModList.Name = "boxModList";
            this.boxModList.Size = new System.Drawing.Size(469, 26);
            this.boxModList.TabIndex = 4;
            this.boxModList.SelectedIndexChanged += new System.EventHandler(this.boxServerList_SelectedIndexChanged);
            // 
            // boxServerOption
            // 
            this.boxServerOption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.boxServerOption.Cursor = System.Windows.Forms.Cursors.Hand;
            this.boxServerOption.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.boxServerOption.Location = new System.Drawing.Point(501, 11);
            this.boxServerOption.Name = "boxServerOption";
            this.boxServerOption.Size = new System.Drawing.Size(137, 38);
            this.boxServerOption.TabIndex = 1;
            this.boxServerOption.Text = "Open mods folder";
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
            this.boxServerSeparator.Location = new System.Drawing.Point(496, 16);
            this.boxServerSeparator.Name = "boxServerSeparator";
            this.boxServerSeparator.Size = new System.Drawing.Size(1, 30);
            this.boxServerSeparator.TabIndex = 1;
            // 
            // boxPathPlaceholder
            // 
            this.boxPathPlaceholder.AutoSize = true;
            this.boxPathPlaceholder.Location = new System.Drawing.Point(14, -1);
            this.boxPathPlaceholder.Name = "boxPathPlaceholder";
            this.boxPathPlaceholder.Size = new System.Drawing.Size(84, 17);
            this.boxPathPlaceholder.TabIndex = 2;
            this.boxPathPlaceholder.Text = "placeholder";
            this.boxPathPlaceholder.Visible = false;
            // 
            // loneServer
            // 
            this.loneServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loneServer.AutoSize = true;
            this.loneServer.Location = new System.Drawing.Point(577, -1);
            this.loneServer.Name = "loneServer";
            this.loneServer.Size = new System.Drawing.Size(84, 17);
            this.loneServer.TabIndex = 3;
            this.loneServer.Text = "placeholder";
            this.loneServer.Visible = false;
            // 
            // watermark
            // 
            this.watermark.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.watermark.Location = new System.Drawing.Point(12, 77);
            this.watermark.Name = "watermark";
            this.watermark.Size = new System.Drawing.Size(655, 17);
            this.watermark.TabIndex = 4;
            this.watermark.Text = "↓  Please drag and drop any mods into the empty space  ↓";
            this.watermark.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Modlist
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(679, 167);
            this.Controls.Add(this.watermark);
            this.Controls.Add(this.loneServer);
            this.Controls.Add(this.boxPathPlaceholder);
            this.Controls.Add(this.boxModsType);
            this.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.ForeColor = System.Drawing.Color.LightGray;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Modlist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modlist";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Modlist_FormClosing);
            this.Load += new System.EventHandler(this.Modlist_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Modlist_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Modlist_DragEnter);
            this.boxModsType.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox boxModsType;
        private System.Windows.Forms.Label boxServerOption;
        private System.Windows.Forms.Panel boxServerSeparator;
        private System.Windows.Forms.Label boxPathPlaceholder;
        public System.Windows.Forms.ComboBox boxModList;
        private System.Windows.Forms.Label loneServer;
        private System.Windows.Forms.Label watermark;
    }
}