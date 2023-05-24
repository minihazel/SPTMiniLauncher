using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPTMiniLauncher
{
    public partial class outputWindow : Form
    {
        public string currentDir = Environment.CurrentDirectory;

        public bool isTrue = false;
        public string selectedServer;
        public string boxSelectedServerTitle;
        public string fullProfilesPath;
        public bool modProblem = false;

        Form1 mainForm = new Form1();

        public outputWindow()
        {
            InitializeComponent();
        }

        private void outputWindow_Load(object sender, EventArgs e)
        {
        }

        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.C)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void outputWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
            // base.OnFormClosing(e);
        }

        private void outputWindow_LocationChanged(object sender, EventArgs e)
        {
            if (bDetach.Text.Contains("stickied"))
            {
                if (Owner != null && !Owner.IsDisposed)
                {
                    this.Left = Owner.Left + Owner.Width;
                    this.Top = Owner.Top;
                }
            }
        }

        private void sptOutputWindow_TextChanged(object sender, EventArgs e)
        {
            string fullString = sptOutputWindow.Text;

            if (Properties.Settings.Default.serverErrorMessages)
            {
                if (!Owner.IsDisposed)
                {
                    if (isTrue)
                    {
                        string fullServerPath = Properties.Settings.Default.server_path;
                        string userFolder = Path.Combine(fullServerPath, "user");
                        string modsFolder = Path.Combine(userFolder, "mods");

                        bool userExists = Directory.Exists(userFolder);
                        bool modsExists = Directory.Exists(modsFolder);

                        if (userExists && modsExists)
                        {
                            string[] mods = Directory.GetDirectories(modsFolder, "*", SearchOption.TopDirectoryOnly);
                            for (int i = 0; i < mods.Length; i++)
                            {
                                if (!modProblem)
                                {
                                    // string pattern = @":(\d+):";
                                    // string pattern = @"\((.+?\.([tj]s)):";
                                    string pattern = @"\((.+?\.([tj]s)):(\d+):";
                                    string keyword = Path.GetFileName(mods[i]);
                                    Match match = Regex.Match(fullString, pattern);
                                    if (match.Success)
                                    {
                                        string filePath = match.Groups[1].Value;
                                        string fileLineNmbr = match.Groups[3].Value;

                                        if (int.TryParse(fileLineNmbr, out int lineNumber))
                                        {
                                            if (MessageBox.Show($"It appears that the mod \"{Path.GetFileName(mods[i])}\" has an issue with the source code.\n" +
                                                                $"\n" +
                                                                $"Line: {lineNumber.ToString()}\n" +
                                                                $"File: {Path.GetFileName(filePath)}\n" +
                                                                $"\n" +
                                                                $"Full path:\n{filePath}\n" +
                                                                $"\n" +
                                                                $"Would you like to open the file?", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                                            {
                                                Process.Start(filePath);
                                            }
                                        }

                                        modProblem = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        string fullServerPath = Properties.Settings.Default.server_path;
                        selectedServer = Path.Combine(Properties.Settings.Default.server_path, mainForm.boxSelectedServerTitle.Text);
                    }
                }
            }
        }

        public void scrollTextForm()
        {
            sptOutputWindow.ScrollToCaret();
        }

        public void matchModFolder(string path)
        {

        }

        private void bDetach_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void bDetach_MouseEnter(object sender, EventArgs e)
        {
            bDetach.ForeColor = Color.DodgerBlue;
        }

        private void bDetach_MouseLeave(object sender, EventArgs e)
        {
            bDetach.ForeColor = Color.LightGray;
        }

        private void bDetach_Click(object sender, EventArgs e)
        {
            if (bDetach.Text.ToLower() == "click to toggle: stickied")
            {
                bDetach.Text = "Click to toggle: detached";
            }
            else
            {
                bDetach.Text = "Click to toggle: stickied";
                this.Location = new Point(mainForm.Location.X + mainForm.Width, mainForm.Location.Y);
                this.Size = new Size(this.Size.Width, mainForm.Size.Height);
            }
        }
    }
}
