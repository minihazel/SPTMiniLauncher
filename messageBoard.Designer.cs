namespace SPTMiniLauncher
{
    partial class messageBoard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(messageBoard));
            this.messageBox = new System.Windows.Forms.RichTextBox();
            this.messageTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // messageBox
            // 
            this.messageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.messageBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.messageBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.messageBox.Font = new System.Drawing.Font("Bahnschrift Light", 12F);
            this.messageBox.ForeColor = System.Drawing.Color.LightGray;
            this.messageBox.Location = new System.Drawing.Point(11, 55);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(585, 364);
            this.messageBox.TabIndex = 0;
            this.messageBox.Text = "Test";
            this.messageBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.messageBox_MouseDown);
            // 
            // messageTitle
            // 
            this.messageTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageTitle.Font = new System.Drawing.Font("Bahnschrift Light", 14F, System.Drawing.FontStyle.Bold);
            this.messageTitle.Location = new System.Drawing.Point(1, 2);
            this.messageTitle.Name = "messageTitle";
            this.messageTitle.Size = new System.Drawing.Size(605, 50);
            this.messageTitle.TabIndex = 1;
            this.messageTitle.Text = "label1";
            this.messageTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // messageBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(607, 431);
            this.Controls.Add(this.messageTitle);
            this.Controls.Add(this.messageBox);
            this.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.ForeColor = System.Drawing.Color.LightGray;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "messageBoard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.messageBoard_FormClosing);
            this.Load += new System.EventHandler(this.messageBoard_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox messageBox;
        private System.Windows.Forms.Label messageTitle;
    }
}