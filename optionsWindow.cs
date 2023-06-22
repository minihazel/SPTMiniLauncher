using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPTMiniLauncher
{
    public partial class optionsWindow : Form
    {
        public List<string> currentProfiles = new List<string>();
        public Color selectedColor = Color.FromArgb(255, 38, 38, 38);
        public Color idleColor = Color.FromArgb(255, 28, 28, 28);
        private int curIndex;
        public string selectedServer;

        public optionsWindow()
        {
            InitializeComponent();
        }

        private void optionsWindow_Load(object sender, EventArgs e)
        {
            Form1 mainForm = new Form1();

            string TarkovPath = Path.Combine(Properties.Settings.Default.server_path, "EscapeFromTarkov.exe");
            bool TarkovExists = File.Exists(TarkovPath);

            if (!TarkovExists) // Improvised LoneServer functionality
            {
                string selected = Path.Combine(Properties.Settings.Default.server_path, selectedServer);
                string userFolder = Path.Combine(selected, "user");
                string profilesFolder = Path.Combine(userFolder, "profiles");

                string[] profiles = Directory.GetFiles(profilesFolder);

                for (int i = 0; i < profiles.Length; i++)
                {
                    string profilePath = Path.Combine(profilesFolder, profiles[i]);
                    bool profileExists = File.Exists(profilePath);
                    if (profileExists)
                    {
                        string readProfile = File.ReadAllText(profiles[i]);
                        JObject jReadProfile = JObject.Parse(readProfile);
                        string _Nickname = jReadProfile["characters"]["pmc"]["Info"]["Nickname"].ToString();

                        string nameOutput = Path.GetFileName(profiles[i]);
                        nameOutput = nameOutput.Substring(0, nameOutput.Length - 5);

                        currentProfiles.Add($"{nameOutput} - [{_Nickname}]");
                    }
                }

                bSPTAKIProfile.Text = currentProfiles[0];
            }
            else
            {
                string userFolder = Path.Combine(Properties.Settings.Default.server_path, "user");
                string profilesFolder = Path.Combine(userFolder, "profiles");

                string[] profiles = Directory.GetFiles(profilesFolder);
                for (int i = 0; i < profiles.Length; i++)
                {
                    string profilePath = Path.Combine(profilesFolder, profiles[i]);
                    bool profileExists = File.Exists(profilePath);
                    if (profileExists)
                    {
                        string readProfile = File.ReadAllText(profiles[i]);
                        JObject jReadProfile = JObject.Parse(readProfile);
                        string _Nickname = jReadProfile["characters"]["pmc"]["Info"]["Nickname"].ToString();

                        string nameOutput = Path.GetFileName(profiles[i]);
                        nameOutput = nameOutput.Substring(0, nameOutput.Length - 5);

                        currentProfiles.Add($"{nameOutput} - [{_Nickname}]");
                    }
                }

                bSPTAKIProfile.Text = currentProfiles[0];
            }

            if (Properties.Settings.Default.currentProfileAID != null &&
                Properties.Settings.Default.currentProfileAID != "")
            {
                bool isInPool = false;
                foreach (string profileAID in currentProfiles)
                {
                    if (profileAID == Properties.Settings.Default.currentProfileAID)
                    {
                        isInPool = true;
                    }
                }
                if (!isInPool)
                {
                    bSPTAKIProfile.Text = currentProfiles[0];
                    Properties.Settings.Default.currentProfileAID = currentProfiles[0];
                    Properties.Settings.Default.Save();
                }
                else
                {
                    bSPTAKIProfile.Text = Properties.Settings.Default.currentProfileAID;
                }
            }
            else
            {
                bSPTAKIProfile.Text = currentProfiles[0];
            }

            panelLauncherSettings.BringToFront();
            tabLauncherDesc.Select();

            bStartDetector.Text = $"Start detector: {Convert.ToInt32(Properties.Settings.Default.startDetector)} second(s)";
            bEndDetector.Text = $"End detector: {Convert.ToInt32(Properties.Settings.Default.endDetector)} second(s)";

            if (TarkovExists)
            {
                string akiPath = Properties.Settings.Default.server_path;
                string akiData = Path.Combine(akiPath, "Aki_Data");
                if (Directory.Exists(akiData))
                {
                    string akiDataServer = Path.Combine(akiData, "Server");
                    if (Directory.Exists(akiDataServer))
                    {
                        string akiDatabase = Path.Combine(akiDataServer, "database");
                        if (Directory.Exists(akiDatabase))
                        {
                            string akiServerJson = Path.Combine(akiDatabase, "server.json");
                            if (File.Exists(akiServerJson))
                            {
                                string readJson = File.ReadAllText(akiServerJson);
                                JObject jsonObject = JObject.Parse(readJson);
                                string _port = jsonObject["port"].ToString();
                                bPortChecking.Text = _port;
                                Properties.Settings.Default.usePort = Convert.ToInt32(_port);
                            }
                        }
                    }
                }

            }
            else
            {
                string selected = Path.Combine(Properties.Settings.Default.server_path, selectedServer);
                string akiPath = selected;
                string akiData = Path.Combine(akiPath, "Aki_Data");
                if (Directory.Exists(akiData))
                {
                    string akiDataServer = Path.Combine(akiData, "Server");
                    if (Directory.Exists(akiDataServer))
                    {
                        string akiDatabase = Path.Combine(akiDataServer, "database");
                        if (Directory.Exists(akiDatabase))
                        {
                            string akiServerJson = Path.Combine(akiDatabase, "server.json");
                            if (File.Exists(akiServerJson))
                            {
                                string readJson = File.ReadAllText(akiServerJson);
                                JObject jsonObject = JObject.Parse(readJson);
                                string _port = jsonObject["port"].ToString();
                                bPortChecking.Text = _port;
                                Properties.Settings.Default.usePort = Convert.ToInt32(_port);
                            }
                        }
                    }
                }

            }

            txtPortCheckBar.Visible = false;

            switch (Properties.Settings.Default.bypassLauncher)
            {
                case false:
                    bEnableBypassAkiLauncher.Text = "Disabled";
                    bEnableBypassAkiLauncher.ForeColor = Color.IndianRed;
                    break;
                case true:
                    bEnableBypassAkiLauncher.Text = "Enabled";
                    bEnableBypassAkiLauncher.ForeColor = Color.DodgerBlue;
                    break;
            }

            switch (Properties.Settings.Default.hideOptions)
            {
                case 0:
                    bHide.Text = "Disabled";
                    bHide.ForeColor = Color.IndianRed;
                    break;

                case 1:
                    bHide.Text = "Minimize Launcher";
                    bHide.ForeColor = Color.DodgerBlue;
                    break;

                case 2:
                    bHide.Text = "Close Launcher";
                    bHide.ForeColor = Color.DodgerBlue;
                    break;
            }

            if (Properties.Settings.Default.timedLauncherToggle)
            {
                bEnableTimed.Text = "Enabled";
                bEnableTimed.ForeColor = Color.DodgerBlue;
            }
            else
            {
                bEnableTimed.Text = "Disabled";
                bEnableTimed.ForeColor = Color.IndianRed;
            }

            switch (Properties.Settings.Default.tarkovDetector)
            {
                case false:
                    bEnableTarkovDetection.Text = "Disabled";
                    bEnableTarkovDetection.ForeColor = Color.IndianRed;
                    bEndDetector.Enabled = false;
                    break;

                case true:
                    bEnableTarkovDetection.Text = "Enabled";
                    bEnableTarkovDetection.ForeColor = Color.DodgerBlue;
                    bEndDetector.Enabled = true;
                    break;
            }

            switch (Properties.Settings.Default.clearCache)
            {
                case 0:
                    bEnableClearCache.Text = "Disabled";
                    bEnableClearCache.ForeColor = Color.IndianRed;
                    break;

                case 1:
                    bEnableClearCache.Text = "On SPT start";
                    bEnableClearCache.ForeColor = Color.DodgerBlue;
                    break;

                case 2:
                    bEnableClearCache.Text = "On SPT stop";
                    bEnableClearCache.ForeColor = Color.DodgerBlue;
                    break;
            }

            switch (Properties.Settings.Default.openLogOnQuit)
            {
                case true:
                    bEnableOpenLog.Text = "Enabled";
                    bEnableOpenLog.ForeColor = Color.DodgerBlue;
                    break;

                case false:
                    bEnableOpenLog.Text = "Disabled";
                    bEnableOpenLog.ForeColor = Color.IndianRed;
                    break;
            }

            switch (Properties.Settings.Default.displayConfirmationMessage)
            {
                case true:
                    bEnableConfirmation.Text = "Enabled";
                    bEnableConfirmation.ForeColor = Color.DodgerBlue;
                    break;

                case false:
                    bEnableConfirmation.Text = "Disabled";
                    bEnableConfirmation.ForeColor = Color.IndianRed;
                    break;
            }

            switch (Properties.Settings.Default.serverOutputting)
            {
                case true:
                    bEnableServerOutput.Text = "Enabled";
                    bEnableServerOutput.ForeColor = Color.DodgerBlue;
                    break;

                case false:
                    bEnableServerOutput.Text = "Disabled";
                    bEnableServerOutput.ForeColor = Color.IndianRed;
                    break;
            }

            switch (Properties.Settings.Default.serverErrorMessages)
            {
                case true:
                    bEnableServerErrors.Text = "Enabled";
                    bEnableServerErrors.ForeColor = Color.DodgerBlue;
                    break;

                case false:
                    bEnableServerErrors.Text = "Disabled";
                    bEnableServerErrors.ForeColor = Color.IndianRed;
                    break;
            }

            switch (Properties.Settings.Default.closeOnQuit)
            {
                case true:
                    bCloseOnSPTExit.Text = "Enabled";
                    bCloseOnSPTExit.ForeColor = Color.DodgerBlue;
                    break;

                case false:
                    bCloseOnSPTExit.Text = "Disabled";
                    bCloseOnSPTExit.ForeColor = Color.IndianRed;
                    break;
            }

            switch (Properties.Settings.Default.closeControlPanel)
            {
                case true:
                    bEnableControlPanel.Text = "Enabled";
                    bEnableControlPanel.ForeColor = Color.DodgerBlue;
                    break;

                case false:
                    bEnableControlPanel.Text = "Disabled";
                    bEnableControlPanel.ForeColor = Color.IndianRed;
                    break;
            }
        }

        public void displayProfileName()
        {

        }

        private void bMinimize_Click(object sender, EventArgs e)
        {
            if (bHide.Text.ToLower() == "disabled")
            {
                bHide.Text = "Minimize Launcher";
                bHide.ForeColor = Color.DodgerBlue;
                Properties.Settings.Default.hideOptions = 1;
            }
            else if (bHide.Text.ToLower() == "minimize launcher")
            {
                bHide.Text = "Close Launcher";
                bHide.ForeColor = Color.DodgerBlue;
                Properties.Settings.Default.hideOptions = 2;
            }
            else if (bHide.Text.ToLower() == "close launcher")
            {
                bHide.Text = "Disabled";
                bHide.ForeColor = Color.IndianRed;
                Properties.Settings.Default.hideOptions = 0;
            }

            Properties.Settings.Default.Save();
        }

        private void bEnableTimed_Click(object sender, EventArgs e)
        {
            if (bEnableTimed.Text.ToLower() == "enabled")
            {
                bEnableTimed.Text = "Disabled";
                bEnableTimed.ForeColor = Color.IndianRed;
                Properties.Settings.Default.timedLauncherToggle = false;
            }
            else
            {
                bEnableTimed.Text = "Enabled";
                bEnableTimed.ForeColor = Color.DodgerBlue;
                Properties.Settings.Default.timedLauncherToggle = true;
            }

            Properties.Settings.Default.Save();
        }

        private void bEnableClearCache_Click(object sender, EventArgs e)
        {
            if (bEnableClearCache.Text.ToLower() == "disabled")
            {
                bEnableClearCache.Text = "On SPT start";
                bEnableClearCache.ForeColor = Color.DodgerBlue;
                Properties.Settings.Default.clearCache = 1;
                Properties.Settings.Default.altCache = true;
            }
            else if (bEnableClearCache.Text.ToLower() == "on spt start")
            {
                bEnableClearCache.Text = "On SPT stop";
                bEnableClearCache.ForeColor = Color.DodgerBlue;
                Properties.Settings.Default.clearCache = 2;
                Properties.Settings.Default.altCache = false;
            }
            else if (bEnableClearCache.Text.ToLower() == "on spt stop")
            {
                bEnableClearCache.Text = "Disabled";
                bEnableClearCache.ForeColor = Color.IndianRed;
                Properties.Settings.Default.clearCache = 0;
                Properties.Settings.Default.altCache = false;
            }

            Properties.Settings.Default.Save();
        }

        private void bRefresh_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            if (frm.boxPath.Text != "" || Directory.Exists(frm.boxPath.Text))
            {
                frm.listAllServers(Properties.Settings.Default.server_path);
            }
            else
            {
                frm.showError("Please have a standalone installation, or gallery of SPT versions, selected. before you refresh.");
            }
        }

        private void bReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to restart the launcher?", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Properties.Settings.Default.Reset();
                    Application.Restart();
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine($"ERROR: {err.ToString()}");
                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
            }
        }

        private void bResetThirdParty_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            if (MessageBox.Show("Are you sure you want to reset the connection with third party apps?", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Properties.Settings.Default.profile_editor_path = "";
                Properties.Settings.Default.svm_path = "";
                Properties.Settings.Default.loe_path = "";
                Properties.Settings.Default.realism_path = "";
                Properties.Settings.Default.Save();
                frm.showError("Reset successful!");
                Application.Restart();
            }
        }

        private void optionsWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void bStartDetector_Click(object sender, EventArgs e)
        {
            
            if (bStartDetector.Text.Contains("1"))
            {
                bStartDetector.Text = "Start detector: 2 second(s)";
                Properties.Settings.Default.startDetector = 2;
            }
            else if (bStartDetector.Text.Contains("2"))
            {
                bStartDetector.Text = "Start detector: 5 second(s)";
                Properties.Settings.Default.startDetector = 5;
            }
            else if (bStartDetector.Text.Contains("5"))
            {
                bStartDetector.Text = "Start detector: 1 second(s)";
                Properties.Settings.Default.startDetector = 1;
            }
            
            Properties.Settings.Default.Save();
        }

        private void bEndDetector_Click(object sender, EventArgs e)
        {
            if (bEndDetector.Text.Contains("1"))
            {
                bEndDetector.Text = "End detector: 2 second(s)";
                Properties.Settings.Default.endDetector = 2;
            }
            else if (bEndDetector.Text.Contains("2"))
            {
                bEndDetector.Text = "End detector: 5 second(s)";
                Properties.Settings.Default.endDetector = 5;
            }
            else if (bEndDetector.Text.Contains("5"))
            {
                bEndDetector.Text = "End detector: 1 second(s)";
                Properties.Settings.Default.endDetector = 1;
            }

            Properties.Settings.Default.Save();
        }

        private void bEnableOpenLog_Click(object sender, EventArgs e)
        {
            if (bEnableOpenLog.Text.ToLower() == "enabled")
            {
                bEnableOpenLog.Text = "Disabled";
                bEnableOpenLog.ForeColor = Color.IndianRed;
                Properties.Settings.Default.openLogOnQuit = false;
            }
            else
            {
                bEnableOpenLog.Text = "Enabled";
                bEnableOpenLog.ForeColor = Color.DodgerBlue;
                Properties.Settings.Default.openLogOnQuit = true;
            }

            Properties.Settings.Default.Save();
        }

        private void bEnableConfirmation_Click(object sender, EventArgs e)
        {
            if (bEnableConfirmation.Text.ToLower() == "enabled")
            {
                bEnableConfirmation.Text = "Disabled";
                bEnableConfirmation.ForeColor = Color.IndianRed;
                Properties.Settings.Default.displayConfirmationMessage = false;
            }
            else
            {
                bEnableConfirmation.Text = "Enabled";
                bEnableConfirmation.ForeColor = Color.DodgerBlue;
                Properties.Settings.Default.displayConfirmationMessage = true;
            }

            Properties.Settings.Default.Save();
        }

        private void bEnableAltCache_Click(object sender, EventArgs e)
        {
            
        }

        private void bEnableServerOutput_Click(object sender, EventArgs e)
        {
            if (bEnableServerOutput.Text.ToLower() == "enabled")
            {
                bEnableServerOutput.Text = "Disabled";
                bEnableServerOutput.ForeColor = Color.IndianRed;
                Properties.Settings.Default.serverOutputting = false;
            }
            else
            {
                bEnableServerOutput.Text = "Enabled";
                bEnableServerOutput.ForeColor = Color.DodgerBlue;
                Properties.Settings.Default.serverOutputting = true;
            }

            Properties.Settings.Default.Save();
        }

        private void bEnableServerErrors_Click(object sender, EventArgs e)
        {
            if (bEnableServerErrors.Text.ToLower() == "enabled")
            {
                bEnableServerErrors.Text = "Disabled";
                bEnableServerErrors.ForeColor = Color.IndianRed;
                Properties.Settings.Default.serverErrorMessages = false;
            }
            else
            {
                bEnableServerErrors.Text = "Enabled";
                bEnableServerErrors.ForeColor = Color.DodgerBlue;
                Properties.Settings.Default.serverErrorMessages = true;
            }

            Properties.Settings.Default.Save();
        }

        private void bCloseOnSPTExit_Click(object sender, EventArgs e)
        {
            if (bCloseOnSPTExit.Text.ToLower() == "enabled")
            {
                bCloseOnSPTExit.Text = "Disabled";
                bCloseOnSPTExit.ForeColor = Color.IndianRed;
                Properties.Settings.Default.closeOnQuit = false;
            }
            else
            {
                bCloseOnSPTExit.Text = "Enabled";
                bCloseOnSPTExit.ForeColor = Color.DodgerBlue;
                Properties.Settings.Default.closeOnQuit = true;
            }

            Properties.Settings.Default.Save();
        }

        private void tabLauncher_Click(object sender, EventArgs e)
        {
            tabLauncher.BackColor = selectedColor;
            tabSPTAKI.BackColor = idleColor;
            tabTarkov.BackColor = idleColor;
            tabPresets.BackColor = idleColor;

            panelLauncherSettings.BringToFront();
            tabLauncherDesc.Select();
        }

        private void tabSPTAKI_Click(object sender, EventArgs e)
        {
            tabLauncher.BackColor = idleColor;
            tabSPTAKI.BackColor = selectedColor;
            tabTarkov.BackColor = idleColor;
            tabPresets.BackColor = idleColor;

            panelSPTAKISettings.BringToFront();
            tabLauncherDesc.Select();
        }

        private void tabTarkov_Click(object sender, EventArgs e)
        {
            tabLauncher.BackColor = idleColor;
            tabSPTAKI.BackColor = idleColor;
            tabTarkov.BackColor = selectedColor;
            tabPresets.BackColor = idleColor;

            panelTarkovSettings.BringToFront();
            tabLauncherDesc.Select();
        }

        private void tabPresets_Click(object sender, EventArgs e)
        {
            tabLauncher.BackColor = idleColor;
            tabSPTAKI.BackColor = idleColor;
            tabTarkov.BackColor = idleColor;
            tabPresets.BackColor = selectedColor;

            panelPresets.BringToFront();
            tabLauncherDesc.Select();
        }

        private void bPreset1_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.displayConfirmationMessage)
            {
                if (MessageBox.Show($"Are you sure you want to apply the Live-like preset?\n\n\n{descPreset1.Text}", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bEnableTimed.Text = "Enabled";
                    bEnableTimed.ForeColor = Color.DodgerBlue;

                    bHide.Text = "Close Launcher";
                    bHide.ForeColor = Color.DodgerBlue;

                    bEnableTarkovDetection.Text = "Enabled";
                    bEnableTarkovDetection.ForeColor = Color.DodgerBlue;
                    bEndDetector.Enabled = true;

                    Properties.Settings.Default.hideOptions = 2;
                    Properties.Settings.Default.tarkovDetector = true;

                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                bEnableTimed.Text = "Enabled";
                bEnableTimed.ForeColor = Color.DodgerBlue;

                bHide.Text = "Close Launcher";
                bHide.ForeColor = Color.DodgerBlue;

                Properties.Settings.Default.hideOptions = 2;
                Properties.Settings.Default.tarkovDetector = true;

                Properties.Settings.Default.Save();
            }
        }

        private void bPreset2_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.displayConfirmationMessage)
            {
                if (MessageBox.Show($"Are you sure you want to apply the Vanilla behavior preset?\n\n\n{descPreset2.Text}", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bEnableTimed.Text = "Disabled";
                    bEnableTimed.ForeColor = Color.IndianRed;

                    bHide.Text = "Disabled";
                    bHide.ForeColor = Color.IndianRed;

                    bCloseOnSPTExit.Text = "Disabled";
                    bCloseOnSPTExit.ForeColor = Color.IndianRed;

                    bEnableTarkovDetection.Text = "Disabled";
                    bEnableTarkovDetection.ForeColor = Color.IndianRed;
                    bEndDetector.Enabled = false;

                    Properties.Settings.Default.hideOptions = 0;
                    Properties.Settings.Default.closeOnQuit = false;
                    Properties.Settings.Default.tarkovDetector = false;
                    Properties.Settings.Default.timedLauncherToggle = false;

                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                bEnableTimed.Text = "Disabled";
                bEnableTimed.ForeColor = Color.IndianRed;

                bHide.Text = "Disabled";
                bHide.ForeColor = Color.IndianRed;

                bCloseOnSPTExit.Text = "Disabled";
                bCloseOnSPTExit.ForeColor = Color.IndianRed;

                Properties.Settings.Default.hideOptions = 0;
                Properties.Settings.Default.closeOnQuit = false;
                Properties.Settings.Default.tarkovDetector = false;
                Properties.Settings.Default.timedLauncherToggle = true;

                Properties.Settings.Default.Save();
            }
        }

        private void bEnableTarkovDetection_Click(object sender, EventArgs e)
        {
            if (bEnableTarkovDetection.Text.ToLower() == "enabled")
            {
                bEnableTarkovDetection.Text = "Disabled";
                bEnableTarkovDetection.ForeColor = Color.IndianRed;
                bEndDetector.Enabled = false;
                Properties.Settings.Default.tarkovDetector = false;
            }
            else
            {
                bEnableTarkovDetection.Text = "Enabled";
                bEnableTarkovDetection.ForeColor = Color.DodgerBlue;
                bEndDetector.Enabled = true;
                Properties.Settings.Default.tarkovDetector = true;
            }

            Properties.Settings.Default.Save();
        }

        private void bEnableControlPanel_Click(object sender, EventArgs e)
        {
            if (bEnableControlPanel.Text.ToLower() == "enabled")
            {
                bEnableControlPanel.Text = "Disabled";
                bEnableControlPanel.ForeColor = Color.IndianRed;
                Properties.Settings.Default.closeControlPanel = false;
            }
            else
            {
                bEnableControlPanel.Text = "Enabled";
                bEnableControlPanel.ForeColor = Color.DodgerBlue;
                Properties.Settings.Default.closeControlPanel = true;
            }

            Properties.Settings.Default.Save();
        }

        public void cycleProfiles()
        {
            string searchString = bSPTAKIProfile.Text;
            int index = currentProfiles.FindIndex(file => file.Contains(searchString));
            Debug.WriteLine(currentProfiles.Count);
            curIndex = index;

            if (curIndex == currentProfiles.Count - 1)
            {
                curIndex = 0;
            }
            else
            {
                curIndex++;
            }

            DisplayProfile(currentProfiles[curIndex]);

        }

        public void DisplayProfile(string fileName)
        {
            bSPTAKIProfile.Text = fileName;

            /*
            int index = fileName.IndexOf("-");
            string output = fileName.Substring(0, index + 5);
            string cleanOutput = output.Substring(0, output.Length - 5);
            cleanOutput += ".json";
            */

            Properties.Settings.Default.currentProfileAID = bSPTAKIProfile.Text;
            Properties.Settings.Default.Save();
        }

        private void bSPTAKIProfile_MouseDown(object sender, MouseEventArgs e)
        {
            cycleProfiles();
        }

        private void bSPTAKIProfile_Click(object sender, EventArgs e)
        {
        }

        private void bEnableBypassAkiLauncher_Click(object sender, EventArgs e)
        {
            if (bEnableBypassAkiLauncher.Text.ToLower() == "enabled")
            {
                bEnableBypassAkiLauncher.Text = "Disabled";
                bEnableBypassAkiLauncher.ForeColor = Color.IndianRed;
                Properties.Settings.Default.bypassLauncher = false;
            }
            else
            {
                bEnableBypassAkiLauncher.Text = "Enabled";
                bEnableBypassAkiLauncher.ForeColor = Color.DodgerBlue;
                Properties.Settings.Default.bypassLauncher = true;
            }

            Properties.Settings.Default.Save();
        }

        private void bPortChecking_Click(object sender, EventArgs e)
        {
            txtPortCheckBar.Visible = true;
            txtPortCheckBar.Text = "";
            txtPortCheckBar.Select();
        }

        private void txtPortCheckBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Properties.Settings.Default.usePort = Convert.ToInt32(txtPortCheckBar.Text);
                Properties.Settings.Default.Save();

                string TarkovPath = Path.Combine(Properties.Settings.Default.server_path, "EscapeFromTarkov.exe");
                bool TarkovExists = File.Exists(TarkovPath);

                if (TarkovExists)
                {
                    string akiPath = Properties.Settings.Default.server_path;
                    string akiData = Path.Combine(akiPath, "Aki_Data");
                    if (Directory.Exists(akiData))
                    {
                        string akiDataServer = Path.Combine(akiData, "Server");
                        if (Directory.Exists(akiDataServer))
                        {
                            string akiDatabase = Path.Combine(akiDataServer, "database");
                            if (Directory.Exists(akiDatabase))
                            {
                                string akiServerJson = Path.Combine(akiDatabase, "server.json");
                                if (File.Exists(akiServerJson))
                                {
                                    string readJson = File.ReadAllText(akiServerJson);
                                    dynamic jsonObject = JsonConvert.DeserializeObject(readJson);
                                    jsonObject["port"] = Convert.ToInt32(txtPortCheckBar.Text);
                                    string output = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                                    try
                                    {
                                        File.WriteAllText(akiServerJson, output);
                                        Properties.Settings.Default.usePort = Convert.ToInt32(txtPortCheckBar.Text);
                                    }
                                    catch (Exception err)
                                    {
                                        Debug.WriteLine($"ERROR: {err.ToString()}");
                                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                                    }
                                }
                            }
                        }
                    }

                }
                else
                {
                    string selected = Path.Combine(Properties.Settings.Default.server_path, selectedServer);
                    string akiPath = selected;
                    string akiData = Path.Combine(akiPath, "Aki_Data");
                    if (Directory.Exists(akiData))
                    {
                        string akiDataServer = Path.Combine(akiData, "Server");
                        if (Directory.Exists(akiDataServer))
                        {
                            string akiDatabase = Path.Combine(akiDataServer, "database");
                            if (Directory.Exists(akiDatabase))
                            {
                                string akiServerJson = Path.Combine(akiDatabase, "server.json");
                                if (File.Exists(akiServerJson))
                                {
                                    string readJson = File.ReadAllText(akiServerJson);
                                    dynamic jsonObject = JsonConvert.DeserializeObject(readJson);
                                    jsonObject["port"] = Convert.ToInt32(txtPortCheckBar.Text);
                                    string output = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                                    try
                                    {
                                        File.WriteAllText(akiServerJson, output);
                                        Properties.Settings.Default.usePort = Convert.ToInt32(txtPortCheckBar.Text);
                                    }
                                    catch (Exception err)
                                    {
                                        Debug.WriteLine($"ERROR: {err.ToString()}");
                                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                                    }
                                }
                            }
                        }
                    }

                }

                bPortChecking.Text = txtPortCheckBar.Text;
                txtPortCheckBar.Text = "";
                txtPortCheckBar.Visible = false;
                Properties.Settings.Default.Save();
            }
        }
    }
}
