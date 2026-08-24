// using Microsoft.WindowsAPICodePack.Dialogs;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

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
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                bToolType.Select();
            }
        }

        private void bToolType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                bBrowsePath.PerformClick();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                bToolType.PerformClick();
            }
        }

        private void txtPathToApp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                bBrowsePath.PerformClick();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                bApplyThirdPartyApp.PerformClick();
            }
        }

        private void bBrowsePath_Click(object sender, EventArgs e)
        {
            if (bToolType.Text.ToLower() == "folder")
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    fbd.RootFolder = Environment.SpecialFolder.MyDocuments;
                    fbd.Description = "Browse for a third party folder";
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        string resultFolder = fbd.SelectedPath;
                        string fullPath = Path.GetFullPath(resultFolder);
                        fullPath = fullPath.Replace("\"", "");

                        if (Directory.Exists(fullPath))
                        {
                            string[] parts = fullPath.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                            int runtimeIndex = Array.IndexOf(parts, "SPT_Runtime");
                            int userIndex = Array.IndexOf(parts, "user");
                            int modsIndex = Array.IndexOf(parts, "mods");

                            if (runtimeIndex != -1 && runtimeIndex + 1 < parts.Length)
                            {
                                txtPathToApp.Text = string.Join(
                                    Path.DirectorySeparatorChar.ToString(),
                                    parts,
                                    runtimeIndex + 1,
                                    parts.Length - (runtimeIndex + 1)
                                );
                            }
                            else if (userIndex != -1 && modsIndex != -1 && userIndex < modsIndex)
                            {
                                txtPathToApp.Text = string.Join(
                                    Path.DirectorySeparatorChar.ToString(),
                                    parts,
                                    modsIndex,
                                    parts.Length - modsIndex
                                );
                            }
                            else
                            {
                                txtPathToApp.Text = fullPath;
                            }

                            bBrowsePath.Select();
                        }
                    }
                }
            }
            else
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Title =
                    $"Select a file";
                open.Filter =
                    $"All files (*.*)|*.*";

                if (open.ShowDialog() == DialogResult.OK)
                {
                    string fullFilePath = open.FileName;
                    fullFilePath = fullFilePath.Replace("\"", "");

                    string[] parts = fullFilePath.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                    int runtimeIndex = Array.IndexOf(parts, "SPT_Runtime");
                    int userIndex = Array.IndexOf(parts, "user");
                    int modsIndex = Array.IndexOf(parts, "mods");

                    if (runtimeIndex != -1 && runtimeIndex + 1 < parts.Length)
                    {
                        txtPathToApp.Text = string.Join(
                            Path.DirectorySeparatorChar.ToString(),
                            parts,
                            runtimeIndex + 1,
                            parts.Length - (runtimeIndex + 1)
                        );
                    }
                    else if (userIndex != -1 && modsIndex != -1 && userIndex < modsIndex)
                    {
                        txtPathToApp.Text = string.Join(
                            Path.DirectorySeparatorChar.ToString(),
                            parts,
                            modsIndex,
                            parts.Length - modsIndex
                        );
                    }
                    else
                    {
                        txtPathToApp.Text = fullFilePath;
                    }

                    bBrowsePath.Select();
                }
            }
        }

        private void bApplyThirdPartyApp_Click(object sender, EventArgs e)
        {
            if (txtPathToApp.Text.Contains("\""))
                txtPathToApp.Text.Replace("\"", "");

            bool isSuccessful = false;

            if (mainForm != null)
            {
                if (!isChanging)
                {
                    if (bToolType.Text.ToLower() == "folder")
                    {
                        string type = "Folder";
                        JObject newApp = new JObject();
                        newApp["Name"] = txtCustomName.Text;
                        newApp["Path"] = txtPathToApp.Text;
                        newApp["Type"] = type;
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

                        ThirdPartyInfo newAppInfo = new ThirdPartyInfo(txtCustomName.Text, txtPathToApp.Text, type);
                        string appName = txtCustomName.Text;
                        mainForm.appDict.Add(appName, newAppInfo);

                        Task.Delay(500);
                        mainForm.readGallery();
                        isSuccessful = true;
                    }
                    else
                    {
                        string type = "App";
                        JObject newApp = new JObject();
                        newApp["Name"] = txtCustomName.Text;
                        newApp["Path"] = txtPathToApp.Text;
                        newApp["Type"] = type;
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

                        ThirdPartyInfo newAppInfo = new ThirdPartyInfo(txtCustomName.Text, txtPathToApp.Text, type);
                        string appName = txtCustomName.Text;
                        mainForm.appDict.Add(appName, newAppInfo);

                        Task.Delay(500);
                        mainForm.readGallery();
                        isSuccessful = true;
                    }
                }
                else
                {
                    if (bToolType.Text.ToLower() == "folder")
                    {
                        string type = "Folder";
                        string appName = txtCustomName.Text;
                        string fullFilePath = txtPathToApp.Text;

                        if (mainForm.appDict.ContainsKey(appName))
                        {
                            string[] parts = txtPathToApp.Text.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                            int runtimeIndex = Array.IndexOf(parts, "SPT_Runtime");
                            int userIndex = Array.IndexOf(parts, "user");
                            int modsIndex = Array.IndexOf(parts, "mods");

                            string resolvedPath;

                            if (runtimeIndex != -1 && runtimeIndex + 1 < parts.Length)
                            {
                                resolvedPath = string.Join(
                                    Path.DirectorySeparatorChar.ToString(),
                                    parts,
                                    runtimeIndex + 1,
                                    parts.Length - (runtimeIndex + 1)
                                );
                            }
                            else if (userIndex != -1 && modsIndex != -1 && userIndex < modsIndex)
                            {
                                resolvedPath = string.Join(
                                    Path.DirectorySeparatorChar.ToString(),
                                    parts,
                                    modsIndex,
                                    parts.Length - modsIndex
                                );
                            }
                            else
                            {
                                resolvedPath = fullFilePath;
                            }

                            mainForm.appDict[appName].Path = resolvedPath;
                            mainForm.appDict[appName].Type = type;
                            mainForm.editThirdPartyApp(appName, resolvedPath, type);

                            Task.Delay(500);
                            mainForm.readGallery();
                            isSuccessful = true;
                        }
                        else
                        {
                            mainForm.showError($"Third party tool {appName} was not found, did you perhaps change the name?");
                            isSuccessful = false;
                        }
                    }
                    else
                    {
                        string type = "App";
                        string appName = txtCustomName.Text;
                        string fullFilePath = txtPathToApp.Text;

                        if (mainForm.appDict.ContainsKey(appName))
                        {
                            string[] parts = txtPathToApp.Text.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                            int runtimeIndex = Array.IndexOf(parts, "SPT_Runtime");
                            int userIndex = Array.IndexOf(parts, "user");
                            int modsIndex = Array.IndexOf(parts, "mods");

                            string targetPath;

                            if (runtimeIndex != -1 && runtimeIndex + 1 < parts.Length)
                            {
                                targetPath = string.Join(
                                    Path.DirectorySeparatorChar.ToString(),
                                    parts,
                                    runtimeIndex + 1,
                                    parts.Length - (runtimeIndex + 1)
                                );
                            }
                            else if (userIndex != -1 && modsIndex != -1 && userIndex < modsIndex)
                            {
                                targetPath = string.Join(
                                    Path.DirectorySeparatorChar.ToString(),
                                    parts,
                                    modsIndex,
                                    parts.Length - modsIndex
                                );
                            }
                            else
                            {
                                targetPath = fullFilePath;
                            }

                            mainForm.appDict[appName].Path = targetPath;
                            mainForm.appDict[appName].Type = type;
                            mainForm.editThirdPartyApp(appName, targetPath, type);

                            Task.Delay(500);
                            mainForm.readGallery();
                            isSuccessful = true;
                        }
                        else
                        {
                            mainForm.showError($"Third party tool {appName} was not found, did you perhaps change the name?");
                            isSuccessful = false;
                        }
                    }
                }

                if (isSuccessful)
                {
                    mainForm.readGallery();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Error: mainForm is null");
            }
        }

        private void bToolType_Click(object sender, EventArgs e)
        {
        }

        private void bToolType_MouseDown(object sender, MouseEventArgs e)
        {
            if (bToolType.Text.ToLower() == "folder")
            {
                bToolType.Text = "App";
            }
            else
            {
                bToolType.Text = "Folder";
            }
        }
    }
}
