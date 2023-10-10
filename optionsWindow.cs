using Microsoft.WindowsAPICodePack.Dialogs;
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
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Media.Animation;

namespace SPTMiniLauncher
{
    public partial class optionsWindow : Form
    {
        public string currentDir = Environment.CurrentDirectory;
        public List<string> currentProfiles = new List<string>();
        public Color selectedColor = Color.FromArgb(255, 38, 38, 38);
        public Color idleColor = Color.FromArgb(255, 28, 28, 28);
        private int curIndex;
        public string selectedServer;
        public string settingsFile = Path.Combine(Environment.CurrentDirectory, "SPT Mini.json");

        private bool isProfileSelect = false;
        private Form1 mainForm;
        private Label bProfilePlaceholder;

        public optionsWindow(Label bProfilePlaceholder, Form1 mainForm, bool isProfileSelection)
        {
            InitializeComponent();
            this.isProfileSelect = isProfileSelection;
            this.mainForm = mainForm;
            this.bProfilePlaceholder = bProfilePlaceholder;
        }

        private void optionsWindow_Load(object sender, EventArgs e)
        {
            Form1 mainForm = new Form1();

            if (Properties.Settings.Default.server_path != null)
            {
                // Setting up global boolean for mimicking "isLoneServer" functionality
                string TarkovPath = Path.Combine(Properties.Settings.Default.server_path, "EscapeFromTarkov.exe");
                bool TarkovExists = File.Exists(TarkovPath);

                // Adding all available profiles to array
                string userFolder = Path.Combine(Properties.Settings.Default.server_path, "user");
                string profilesFolder = Path.Combine(userFolder, "profiles");
                bool profilesExists = Directory.Exists(profilesFolder);

                if (profilesExists)
                {
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
                }
                else
                {
                    if (MessageBox.Show("We couldn\'t detect a profiles folder, auto-closing the Options window for you.\n\n\nPlease select another SPT-AKI installation and try again.") == DialogResult.OK)
                    {
                        this.Close();
                    }
                }

                // Sorting profiles for use in the profile cycler
                if (Properties.Settings.Default.currentProfileAID != null && Properties.Settings.Default.currentProfileAID != "")
                {
                    bool isProfileInPool = false;

                    foreach (string profileAID in currentProfiles)
                    {
                        int profileIndex = profileAID.IndexOf("-");
                        string output = profileAID.Substring(0, profileIndex + 5);
                        string cleanOutput = output.Substring(0, output.Length - 5);
                        cleanOutput = cleanOutput.Trim();

                        if (cleanOutput == Properties.Settings.Default.currentProfileAID)
                        {
                            isProfileInPool = true;
                            bSPTAKIProfile.Text = displayProfileName(cleanOutput);
                            break;
                        }
                    }

                    if (!isProfileInPool)
                    {
                        bSPTAKIProfile.Text = currentProfiles[0];

                        int profileIndex = currentProfiles[0].IndexOf("-");
                        string output = currentProfiles[0].Substring(0, profileIndex + 5);
                        string cleanOutput = output.Substring(0, output.Length - 5);
                        cleanOutput = cleanOutput.Trim();

                        Properties.Settings.Default.currentProfileAID = cleanOutput;
                        Properties.Settings.Default.Save();
                    }
                }
                else
                {
                    bSPTAKIProfile.Text = currentProfiles[0];
                }

                // Setting homepage and detection
                panelLauncherSettings.BringToFront();

                // Setting server port
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
                txtPortCheckBar.Visible = false;

                // Labeling
                bDeleteServer.Text = $"Click to delete {selectedServer}";
                bStartDetector.Text = $"Start detector: {Convert.ToInt32(Properties.Settings.Default.startDetector)} second(s)";
                bEndDetector.Text = $"End detector: {Convert.ToInt32(Properties.Settings.Default.endDetector)} second(s)";

                // Standard options
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

                switch (Properties.Settings.Default.timedLauncherToggle)
                {
                    case true:
                        bEnableTimed.Text = "Enabled";
                        bEnableTimed.ForeColor = Color.DodgerBlue;
                        break;

                    case false:
                        bEnableTimed.Text = "Disabled";
                        bEnableTimed.ForeColor = Color.IndianRed;
                        break;
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

                // Playtime counter for server and EFT
                bool settingsFileExists = File.Exists(settingsFile);
                if (settingsFileExists)
                {
                    string settingsContent = File.ReadAllText(settingsFile);
                    JObject settingsObj = JObject.Parse(settingsContent);

                    if (settingsObj["timeOptions"] != null)
                    {
                        JObject timeOptions = (JObject)settingsObj["timeOptions"];

                        int serverPlaytime = (int)timeOptions["serverTime"];
                        int tarkovPlaytime = (int)timeOptions["tarkovTime"];
                        TimeSpan serverPlaytimeInSeconds = TimeSpan.FromSeconds((int)timeOptions["serverTime"]);
                        TimeSpan tarkovPlaytimeInSeconds = TimeSpan.FromSeconds((int)timeOptions["tarkovTime"]);

                        // Check if the time is 0
                        if (serverPlaytime == 0)
                        {
                            bServerTimeCounter.Text = "No playtime recorded";
                            bServerHourCounter.Text = "No playtime recorded";
                        }
                        else
                        {
                            // Format time based on days, hours and minutes
                            string formattedServerPlaytime = "";

                            if (serverPlaytimeInSeconds.TotalDays >= 1)
                            {
                                if (serverPlaytimeInSeconds.TotalHours >= 1 && serverPlaytimeInSeconds.Minutes == 0)
                                {
                                    int days = (int)serverPlaytimeInSeconds.TotalDays;
                                    int hours = serverPlaytimeInSeconds.Hours;
                                    formattedServerPlaytime = $"{days} days and {hours} hours";
                                }
                                else if (serverPlaytimeInSeconds.TotalHours >= 1)
                                {
                                    int days = (int)serverPlaytimeInSeconds.TotalDays;
                                    int hours = serverPlaytimeInSeconds.Hours;
                                    int minutes = serverPlaytimeInSeconds.Minutes;
                                    formattedServerPlaytime = $"{days} days, {hours} hours and {minutes} minutes";
                                }
                                else
                                {
                                    int days = (int)serverPlaytimeInSeconds.TotalDays;
                                    int minutes = serverPlaytimeInSeconds.Minutes;
                                    formattedServerPlaytime = $"{days} days and {minutes} hours";
                                }
                            }
                            else if (serverPlaytimeInSeconds.TotalHours >= 1)
                            {
                                if (serverPlaytimeInSeconds.Minutes == 0)
                                {
                                    int hours = serverPlaytimeInSeconds.Hours;
                                    formattedServerPlaytime = $"{hours} hours";
                                }
                                else
                                {
                                    int hours = serverPlaytimeInSeconds.Hours;
                                    int minutes = serverPlaytimeInSeconds.Minutes;
                                    formattedServerPlaytime = $"{hours} hours and {minutes} minutes";
                                }
                            }
                            else
                            {
                                int minutes = serverPlaytimeInSeconds.Minutes;
                                formattedServerPlaytime = $"{minutes} minutes";
                            }

                            string formattedHour = string.Format("{0:#,##0}", serverPlaytimeInSeconds.TotalHours);
                            bServerHourCounter.Text = $"{formattedHour} hours played";
                            bServerTimeCounter.Text = $"{formattedServerPlaytime} played";
                        }

                        // Check if the time is 0
                        if (tarkovPlaytime == 0)
                        {
                            bTarkovTimeCounter.Text = "No playtime recorded";
                            bTarkovHourCount.Text = "No playtime recorded";
                        }
                        else
                        {
                            // Format time based on days, hours and minutes
                            string formattedTarkovPlaytime = "";

                            if (tarkovPlaytimeInSeconds.TotalDays >= 1)
                            {
                                if (tarkovPlaytimeInSeconds.TotalHours >= 1 && tarkovPlaytimeInSeconds.Minutes == 0)
                                {
                                    int days = (int)tarkovPlaytimeInSeconds.TotalDays;
                                    int hours = tarkovPlaytimeInSeconds.Hours;
                                    formattedTarkovPlaytime = $"{days} days and {hours} hours";
                                }
                                else if (tarkovPlaytimeInSeconds.TotalHours >= 1)
                                {
                                    int days = (int)tarkovPlaytimeInSeconds.TotalDays;
                                    int hours = tarkovPlaytimeInSeconds.Hours;
                                    int minutes = tarkovPlaytimeInSeconds.Minutes;
                                    formattedTarkovPlaytime = $"{days} days, {hours} hours and {minutes} minutes";
                                }
                                else
                                {
                                    int days = (int)tarkovPlaytimeInSeconds.TotalDays;
                                    int minutes = tarkovPlaytimeInSeconds.Minutes;
                                    formattedTarkovPlaytime = $"{days} days and {minutes} hours";
                                }
                            }
                            else if (tarkovPlaytimeInSeconds.TotalHours >= 1)
                            {
                                if (tarkovPlaytimeInSeconds.Minutes == 0)
                                {
                                    int hours = tarkovPlaytimeInSeconds.Hours;
                                    formattedTarkovPlaytime = $"{hours} hours";
                                }
                                else
                                {
                                    int hours = tarkovPlaytimeInSeconds.Hours;
                                    int minutes = tarkovPlaytimeInSeconds.Minutes;
                                    formattedTarkovPlaytime = $"{hours} hours and {minutes} minutes";
                                }
                            }
                            else
                            {
                                int minutes = tarkovPlaytimeInSeconds.Minutes;
                                formattedTarkovPlaytime = $"{minutes} minutes";
                            }

                            string formattedHour = string.Format("{0:#,##0}", tarkovPlaytimeInSeconds.TotalHours);
                            bTarkovHourCount.Text = $"{formattedHour} hours played";
                            bTarkovTimeCounter.Text = $"{formattedTarkovPlaytime} played";
                        }
                    }
                }

                // Profile selection shortcut
                if (isProfileSelect)
                    tabSPTAKI.PerformClick();
            }
            else
            {
                this.Close();
            }
        }

        public string displayProfileName(string input)
        {
            string result = "";
            string TarkovPath = Path.Combine(Properties.Settings.Default.server_path, "EscapeFromTarkov.exe");
            bool TarkovExists = File.Exists(TarkovPath);

            string userFolder = Path.Combine(Properties.Settings.Default.server_path, "user");
            string profilesFolder = Path.Combine(userFolder, "profiles");
            string profileFile = Path.Combine(profilesFolder, $"{input}.json");

            bool profilesFileExists = File.Exists(profileFile);
            if (profilesFileExists)
            {
                string readProfile = File.ReadAllText(profileFile);
                JObject jReadProfile = JObject.Parse(readProfile);
                string _Nickname = jReadProfile["characters"]["pmc"]["Info"]["Nickname"].ToString();

                string nameOutput = Path.GetFileName(profileFile);
                nameOutput = nameOutput.Replace(".json", "");

                result = $"{nameOutput} - [{_Nickname}]";
            }

            return result;
        }

        public string displayProfileAID(string input)
        {
            string result = "";
            int profileIndex = input.IndexOf("-");

            if (profileIndex != -1)
            {
                string output = input.Substring(0, profileIndex + 5);
                string cleanOutput = output.Substring(0, output.Length - 5);
                cleanOutput = cleanOutput.Trim();

                result = cleanOutput;
            }
            else
            {
                result = input;
            }

            return result;
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
                frm.readGallery();
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
            tabTimeDisplay.BackColor = idleColor;

            panelLauncherSettings.BringToFront();
        }

        private void tabSPTAKI_Click(object sender, EventArgs e)
        {
            tabLauncher.BackColor = idleColor;
            tabSPTAKI.BackColor = selectedColor;
            tabTarkov.BackColor = idleColor;
            tabPresets.BackColor = idleColor;
            tabTimeDisplay.BackColor = idleColor;

            panelSPTAKISettings.BringToFront();
        }

        private void tabTarkov_Click(object sender, EventArgs e)
        {
            tabLauncher.BackColor = idleColor;
            tabSPTAKI.BackColor = idleColor;
            tabTarkov.BackColor = selectedColor;
            tabPresets.BackColor = idleColor;
            tabTimeDisplay.BackColor = idleColor;

            panelTarkovSettings.BringToFront();
        }

        private void tabPresets_Click(object sender, EventArgs e)
        {
            tabLauncher.BackColor = idleColor;
            tabSPTAKI.BackColor = idleColor;
            tabTarkov.BackColor = idleColor;
            tabPresets.BackColor = selectedColor;
            tabTimeDisplay.BackColor = idleColor;

            panelPresets.BringToFront();
        }

        private void tabTimeDisplay_Click(object sender, EventArgs e)
        {
            tabLauncher.BackColor = idleColor;
            tabSPTAKI.BackColor = idleColor;
            tabTarkov.BackColor = idleColor;
            tabPresets.BackColor = idleColor;
            tabTimeDisplay.BackColor = selectedColor;

            panelTimeDisplay.BringToFront();
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
            curIndex = index;

            if (curIndex == currentProfiles.Count - 1)
            {
                curIndex = 0;
            }
            else
            {
                curIndex++;
            }

            string currentAid = displayProfileAID(currentProfiles[curIndex]);

            DisplayProfile(currentAid, currentProfiles[curIndex]);
        }

        public void DisplayProfile(string fileName, string fullName)
        {
            bSPTAKIProfile.Text = fullName;

            Properties.Settings.Default.currentProfileAID = fileName;
            Properties.Settings.Default.Save();

            string input = fullName;
            int index = input.IndexOf("-");
            if (index != -1)
            {
                string result = input.Substring(index + 1).Trim();
                result = result.Replace("[", null);
                result = result.Replace("]", null);

                if (bProfilePlaceholder != null)
                {
                    bProfilePlaceholder.Text = $"Profile \'{result}\' selected";
                }
            }
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

                bPortChecking.Text = txtPortCheckBar.Text;
                txtPortCheckBar.Text = "";
                txtPortCheckBar.Visible = false;
                Properties.Settings.Default.Save();
            }
        }

        private void bDeleteServer_Click(object sender, EventArgs e)
        {
            Form1 mainForm = new Form1();

            string server = bDeleteServer.Text.Replace("Click to delete ", "");

            if (MessageBox.Show($"Would you like to delete {server}?", $"{this.Text} - Delete an installation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string TarkovPath = Path.Combine(Properties.Settings.Default.server_path, "EscapeFromTarkov.exe");
                bool TarkovExists = File.Exists(TarkovPath);

                Directory.Delete(Properties.Settings.Default.server_path, true);
                mainForm.showError($"Folder {server} has been deleted.");
                mainForm.clearUI(true);

                this.Close();
            }
        }

        private void btnImportExistingConfig_Click(object sender, EventArgs e)
        {
            string sptminiSuccess = "";
            string tpaSuccess = "";
            string gallerySuccess = "";

            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string fullPath = Path.GetFullPath(dialog.FileName);

                if (Directory.Exists(fullPath))
                {
                    if (File.Exists(Path.Combine(fullPath, "SPT Mini.json")) &&
                        File.Exists(Path.Combine(fullPath, "Third Party Apps.json")) &&
                        File.Exists(Path.Combine(fullPath, "Gallery.json")))
                    {
                        string[] configs = {
                            Path.Combine(fullPath, "SPT Mini.json"),
                            Path.Combine(fullPath, "Third Party Apps.json"),
                            Path.Combine(fullPath, "Gallery.json")
                        };

                        string sptmini_config = configs[0];
                        string tpa_config = configs[1];
                        string galleryjson = configs[2];

                        bool sptminiExists = File.Exists(Path.Combine(currentDir, "SPT Mini.json"));
                        bool tpaExists = File.Exists(Path.Combine(currentDir, "Third Party Apps.json"));
                        bool galleryExists = File.Exists(Path.Combine(currentDir, "Gallery.json"));

                        try
                        {
                            if (sptminiExists)
                            {
                                File.Copy(configs[0], Path.Combine(currentDir, Path.GetFileName(configs[0])), true);
                            }
                            else
                            {
                                try
                                {
                                    File.Copy(configs[0], Path.Combine(currentDir, Path.GetFileName(configs[0])), true);
                                    sptminiSuccess = "Imported successfully";
                                }
                                catch (Exception err)
                                {
                                    sptminiSuccess = "Failed to import";
                                }
                            }

                            if (tpaExists)
                            {
                                File.Copy(configs[1], Path.Combine(currentDir, Path.GetFileName(configs[1])), true);
                            }
                            else
                            {
                                try
                                {
                                    File.Copy(configs[1], Path.Combine(currentDir, Path.GetFileName(configs[1])), true);
                                    tpaSuccess = "Imported successfully";
                                }
                                catch (Exception err)
                                {
                                    tpaSuccess = "Failed to import";
                                }
                            }

                            if (galleryExists)
                            {
                                File.Copy(configs[2], Path.Combine(currentDir, Path.GetFileName(configs[2])), true);
                            }
                            else
                            {
                                try
                                {
                                    File.Copy(configs[2], Path.Combine(currentDir, Path.GetFileName(configs[2])), true);
                                    gallerySuccess = "Imported successfully";
                                }
                                catch (Exception err)
                                {
                                    gallerySuccess = "Failed to import";
                                }
                            }
                        }
                        catch (Exception err)
                        {
                            Debug.WriteLine($"ERROR: {err.ToString()}");
                            MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                        }

                        string content = $"Imported config files:{Environment.NewLine}{Environment.NewLine}" +
                             $"{Environment.NewLine}" +
                             $"Gallery.json: {gallerySuccess}{Environment.NewLine}" +
                             $"SPT Mini.json: {sptminiSuccess}{Environment.NewLine}" +
                             $"Third Party Apps.json: {tpaSuccess}{Environment.NewLine}{Environment.NewLine}" +
                             $"{this.Text} will restart to apply the new settings.";

                        mainForm.showError(content);
                        Application.Restart();
                    }
                }
            }
        }

        private void optionsWindow_DragEnter(object sender, DragEventArgs e)
        {
            
        }

        private void optionsWindow_DragDrop(object sender, DragEventArgs e)
        {
            
        }
    }
}
