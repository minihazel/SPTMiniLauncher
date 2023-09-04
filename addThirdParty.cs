using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPTMiniLauncher
{
    public partial class addThirdParty : Form
    {
        public Form1 mainForm;
        public Dictionary<string, ThirdPartyInfo> dictionary { get; private set; }

        public addThirdParty()
        {
            InitializeComponent();
        }

        private void addThirdParty_Load(object sender, EventArgs e)
        {
            dictionary = mainForm.appDict;

            txtCustomName.Clear();
            txtPathToApp.Clear();

            txtCustomName.Select();
        }

        private void txtCustomName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtPathToApp.Select();
            }
        }

        private void txtPathToApp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                bBrowsePath.Select();
            }
        }

        private void bBrowsePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title =
                $"Select a file";
            open.Filter =
                $"All files (*.*|*.*";

            if (open.ShowDialog() == DialogResult.OK)
            {
                string fullFilePath = open.FileName;

                string[] parts = fullFilePath.Split('\\');
                int userIndex = Array.IndexOf(parts, "user");
                int modsIndex = Array.IndexOf(parts, "mods");

                if (userIndex != -1 && modsIndex != -1 && userIndex < modsIndex)
                {
                    string folderName = parts[modsIndex];
                    folderName = Path.Combine(folderName, string.Join(Path.DirectorySeparatorChar.ToString(), parts, modsIndex + 1, parts.Length - modsIndex - 1));

                    txtPathToApp.Text = folderName;
                }
                else if (userIndex == -1 && modsIndex == -1)
                {
                    txtPathToApp.Text = fullFilePath;
                }
            }
        }

        private void bApplyThirdPartyApp_Click(object sender, EventArgs e)
        {
            if (!mainForm.appDict.ContainsKey(txtCustomName.Text))
            {
                JObject newApp = new JObject();
                newApp["Name"] = txtCustomName.Text;
                newApp["Path"] = txtPathToApp.Text;

                bool thirdPartyFileExists = File.Exists(mainForm.thirdPartyFile);
                if (thirdPartyFileExists)
                {
                    string thirdPartycontent = File.ReadAllText(mainForm.thirdPartyFile);
                    JObject obj = JObject.Parse(thirdPartycontent);
                    JArray thirdPartyApps = (JArray)obj["ThirdPartyApps"];
                    thirdPartyApps.Add(newApp);
                    string updatedJSON = obj.ToString();
                    File.WriteAllText(mainForm.thirdPartyFile, updatedJSON);
                }

                ThirdPartyInfo newAppInfo = new ThirdPartyInfo(txtCustomName.Text, txtPathToApp.Text);
                string appName = txtCustomName.Text;
                dictionary.Add(appName, newAppInfo);

                mainForm.listServerOptions(true);
                this.Close();
            }
        }
    }
}
