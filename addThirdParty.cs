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
using System.Windows.Media;

namespace SPTMiniLauncher
{
    public partial class addThirdParty : Form
    {
        private Form1 mainForm;
        public string thirdPartyFile;
        private bool isChanging;

        public addThirdParty(Form1 mainForm, bool isChanging)
        {
            this.mainForm = mainForm;
            InitializeComponent();
            this.isChanging = isChanging;
        }

        private void addThirdParty_Load(object sender, EventArgs e)
        {
            thirdPartyFile = System.IO.Path.Combine(Environment.CurrentDirectory, "Third Party Apps.json");

            if (!isChanging)
            {
                txtCustomName.Clear();
                txtPathToApp.Clear();
            }

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
            bool isSuccessful = false;

            if (mainForm != null)
            {
                if (!isChanging)
                {
                    JObject newApp = new JObject();
                    newApp["Name"] = txtCustomName.Text;
                    newApp["Path"] = txtPathToApp.Text;
                    bool thirdPartyFileExists = File.Exists(thirdPartyFile);

                    if (thirdPartyFileExists)
                    {
                        string thirdPartycontent = File.ReadAllText(thirdPartyFile);
                        JObject obj = JObject.Parse(thirdPartycontent);
                        JArray thirdPartyApps = (JArray)obj["ThirdPartyApps"];
                        thirdPartyApps.Add(newApp);
                        string updatedJSON = obj.ToString();
                        File.WriteAllText(thirdPartyFile, updatedJSON);
                    }

                    ThirdPartyInfo newAppInfo = new ThirdPartyInfo(txtCustomName.Text, txtPathToApp.Text);
                    string appName = txtCustomName.Text;
                    mainForm.appDict.Add(appName, newAppInfo);

                    Task.Delay(500);
                    mainForm.listServerOptions(true);
                }
                else
                {
                    string appName = txtCustomName.Text;
                    string fullFilePath = txtPathToApp.Text;

                    if (mainForm.appDict.ContainsKey(appName))
                    {
                        string[] parts = txtPathToApp.Text.Split('\\');
                        int userIndex = Array.IndexOf(parts, "user");
                        int modsIndex = Array.IndexOf(parts, "mods");

                        if (userIndex != -1 && modsIndex != -1 && userIndex < modsIndex)
                        {
                            string folderName = parts[modsIndex];
                            folderName = Path.Combine(folderName, string.Join(Path.DirectorySeparatorChar.ToString(), parts, modsIndex + 1, parts.Length - modsIndex - 1));
                            mainForm.appDict[appName].Path = folderName;
                            mainForm.editThirdPartyApp(appName, folderName);
                        }
                        else
                        {
                            mainForm.appDict[appName].Path = fullFilePath;
                            mainForm.editThirdPartyApp(appName, fullFilePath);
                        }

                        isSuccessful = true;
                    }
                    else
                    {
                        mainForm.showError($"Third party tool {appName} was not found, did you perhaps change the name?");
                        isSuccessful = false;
                    }
                }

                if (isSuccessful)
                {
                    mainForm.listServerOptions(true);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Error: mainForm is null");
            }
        }
    }
}
