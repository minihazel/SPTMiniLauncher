﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SPTMiniLauncher
{
    public partial class profileSelector : Form
    {
        public bool isTrue = false;
        public string selectedServer;
        public string boxSelectedServerTitle;
        public string fullProfilesPath;
        public string selector;

        public Color listBackcolor = Color.FromArgb(255, 35, 35, 35);
        public Color listSelectedcolor = Color.FromArgb(255, 50, 50, 50);
        public Color listHovercolor = Color.FromArgb(255, 45, 45, 45);

        public profileSelector()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case 0x84: //WM_NCHITTEST
                    var result = (HitTest)m.Result.ToInt32();
                    if (result == HitTest.Left || result == HitTest.Right)
                        m.Result = new IntPtr((int)HitTest.Caption);
                    if (result == HitTest.TopLeft || result == HitTest.TopRight)
                        m.Result = new IntPtr((int)HitTest.Top);
                    if (result == HitTest.BottomLeft || result == HitTest.BottomRight)
                        m.Result = new IntPtr((int)HitTest.Bottom);

                    break;
            }
        }
        enum HitTest
        {
            Caption = 2,
            Transparent = -1,
            Nowhere = 0,
            Client = 1,
            Left = 10,
            Right = 11,
            Top = 12,
            TopLeft = 13,
            TopRight = 14,
            Bottom = 15,
            BottomLeft = 16,
            BottomRight = 17,
            Border = 18
        }

        private void profileSelector_Load(object sender, EventArgs e)
        {
            Form1 mainForm = new Form1();

            if (isTrue)
            {
                string profilesFolder = Path.Combine(Properties.Settings.Default.server_path, "user\\profiles");
                bool profilesFolderExists = Directory.Exists(profilesFolder);
                if (profilesFolderExists)
                {
                    fullProfilesPath = profilesFolder;
                    listProfiles(profilesFolder);
                }
            }
            else
            {
                selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle);
                string profilesFolder = Path.Combine(selectedServer, "user\\profiles");
                bool profilesFolderExists = Directory.Exists(profilesFolder);
                if (profilesFolderExists)
                {
                    fullProfilesPath = profilesFolder;
                    listProfiles(profilesFolder);
                }
            }

            bSelection.Text = selector;
        }

        public void listProfiles(string path)
        {
            string[] _countProfiles = Directory.GetFiles(path);

            for (int i = 0; i < _countProfiles.Length; i++)
            {
                string profilePath = Path.Combine(path, _countProfiles[i]);

                Label lbl = new Label();
                lbl.AutoSize = false;
                lbl.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
                lbl.TextAlign = ContentAlignment.MiddleLeft;
                lbl.Size = new Size(panelProfilesPlaceholder.Size.Width, panelProfilesPlaceholder.Size.Height);
                lbl.Location = new Point(panelProfilesPlaceholder.Location.X, panelProfilesPlaceholder.Location.Y + (i * 30));
                lbl.Font = new Font("Bahnschrift Light", 9, FontStyle.Regular);
                lbl.BackColor = listBackcolor;
                lbl.ForeColor = Color.LightGray;
                lbl.Margin = new Padding(1, 1, 1, 1);
                lbl.Cursor = Cursors.Hand;
                lbl.MouseEnter += new EventHandler(lbl_MouseEnter);
                lbl.MouseLeave += new EventHandler(lbl_MouseLeave);
                lbl.MouseDown += new MouseEventHandler(lbl_MouseDown);
                lbl.MouseUp += new MouseEventHandler(lbl_MouseUp);
                lbl.Visible = true;

                bool profileExists = File.Exists(_countProfiles[i]);
                if (profileExists)
                {
                    string readProfile = File.ReadAllText(_countProfiles[i]);
                    JObject jReadProfile = JObject.Parse(readProfile);
                    string _Nickname = jReadProfile["characters"]["pmc"]["Info"]["Nickname"].ToString();

                    lbl.Text = $"{Path.GetFileName(_countProfiles[i])}  -  {_Nickname}";
                }

                panelProfiles.Controls.Add(lbl);
            }
        }

        private void lbl_MouseEnter(object sender, EventArgs e)
        {
            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;
            if (label.Text != "")
            {
                label.BackColor = listHovercolor;
            }
        }

        private void lbl_MouseLeave(object sender, EventArgs e)
        {
            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;
            if (label.Text != "")
            {
                label.BackColor = listBackcolor;
            }
        }

        private void lbl_MouseDown(object sender, EventArgs e)
        {
            System.Windows.Forms.Label lbl = (System.Windows.Forms.Label)sender;

            if (lbl.Text != "")
            {
                if (bSelection.Text.ToLower().Contains("profile_open"))
                {
                    int index = lbl.Text.IndexOf(".json");
                    string output = lbl.Text.Substring(0, index + 5);

                    bool profileExists = File.Exists(Path.Combine(fullProfilesPath, output));
                    if (profileExists)
                    {
                        Process.Start(Path.Combine(fullProfilesPath, output));
                        this.Close();
                    }
                }
                else if (bSelection.Text.ToLower().Contains("run_spt"))
                {
                    
                }

            }
        }

        private void lbl_MouseUp(object sender, EventArgs e)
        {
            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;
            if (label.Text != "")
            {
                label.BackColor = label.BackColor = listHovercolor;
            }
        }

        private void bCancel_MouseEnter(object sender, EventArgs e)
        {
            bCancel.ForeColor = Color.DodgerBlue;
        }

        private void bCancel_MouseLeave(object sender, EventArgs e)
        {
            bCancel.ForeColor = Color.LightGray;
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            clearUI();
            this.Close();
        }

        private void clearUI()
        {
            // server box
            for (int i = panelProfiles.Controls.Count - 1; i >= 0; i--)
            {
                Label selected = panelProfiles.Controls[i] as Label;
                if (selected != null)
                {
                    try
                    {
                        panelProfiles.Controls.RemoveAt(i);
                        selected.Dispose();
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine($"ERROR: {err.ToString()}");
                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void profileSelector_FormClosing(object sender, FormClosingEventArgs e)
        {
            clearUI();
        }
    }
}