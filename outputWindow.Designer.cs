namespace SPTMiniLauncher
{
    partial class outputWindow
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
            this.sptOutputWindow = new System.Windows.Forms.RichTextBox();
            this.bDetach = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // sptOutputWindow
            // 
            this.sptOutputWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sptOutputWindow.AutoWordSelection = true;
            this.sptOutputWindow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.sptOutputWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sptOutputWindow.Font = new System.Drawing.Font("Consolas", 10F);
            this.sptOutputWindow.ForeColor = System.Drawing.Color.LightGray;
            this.sptOutputWindow.Location = new System.Drawing.Point(0, 1);
            this.sptOutputWindow.Name = "sptOutputWindow";
            this.sptOutputWindow.ReadOnly = true;
            this.sptOutputWindow.Size = new System.Drawing.Size(454, 621);
            this.sptOutputWindow.TabIndex = 0;
            this.sptOutputWindow.Text = "";
            this.sptOutputWindow.WordWrap = false;
            this.sptOutputWindow.TextChanged += new System.EventHandler(this.sptOutputWindow_TextChanged);
            this.sptOutputWindow.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyDown);
            this.sptOutputWindow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.richTextBox1_MouseDown);
            // 
            // bDetach
            // 
            this.bDetach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bDetach.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bDetach.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.bDetach.Location = new System.Drawing.Point(0, 622);
            this.bDetach.Name = "bDetach";
            this.bDetach.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.bDetach.Size = new System.Drawing.Size(454, 29);
            this.bDetach.TabIndex = 5;
            this.bDetach.Text = "Click to toggle: stickied";
            this.bDetach.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bDetach.Click += new System.EventHandler(this.bDetach_Click);
            this.bDetach.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bDetach_MouseDown);
            this.bDetach.MouseEnter += new System.EventHandler(this.bDetach_MouseEnter);
            this.bDetach.MouseLeave += new System.EventHandler(this.bDetach_MouseLeave);
            // 
            // outputWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(454, 651);
            this.Controls.Add(this.bDetach);
            this.Controls.Add(this.sptOutputWindow);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.ForeColor = System.Drawing.Color.LightGray;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "outputWindow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Server Output";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.outputWindow_FormClosing);
            this.Load += new System.EventHandler(this.outputWindow_Load);
            this.LocationChanged += new System.EventHandler(this.outputWindow_LocationChanged);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox sptOutputWindow;
        private System.Windows.Forms.Label bDetach;
    }
}