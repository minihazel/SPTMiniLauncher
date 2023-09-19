using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPTMiniLauncher
{
    public partial class messageBoard : Form
    {
        public messageBoard()
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

        private void messageBoard_FormClosing(object sender, FormClosingEventArgs e)
        {
            string settingsFile = System.IO.Path.Combine(Environment.CurrentDirectory, "SPT Mini.json");
            if (messageTitle.Text.ToLower().Contains("settings file was not detected") && !System.IO.File.Exists(settingsFile))
            {
                var data = new
                {
                    showFirstTimeMessage = "true",
                    mainWidth = 695,
                    mainHeight = 680,
                };

                var json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(settingsFile, json);

                Application.Restart();
            }

            Form1 form = new Form1();
            Modlist form2 = new Modlist();
            Label boxPathPlaceholder = (Label)form2.Controls["boxPathPlaceholder"];

            string newPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(boxPathPlaceholder.Text, @"..\..\"));
            string modsFolder = System.IO.Path.Combine(newPath, "user\\mods");
            form.updateOrderJSON(modsFolder);
        }

        private void messageBoard_Load(object sender, EventArgs e)
        {
            messageTitle.Select();
        }

        private void messageBox_MouseDown(object sender, MouseEventArgs e)
        {
            messageTitle.Select();
        }

        private void messageBox_MouseMove(object sender, MouseEventArgs e)
        {
        }
    }
}
