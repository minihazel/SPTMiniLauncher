using System;
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

namespace SPTMiniLauncher
{
    public partial class optionsWindow : Form
    {
        public Color selectedColor = Color.FromArgb(255, 38, 38, 38);
        public Color idleColor = Color.FromArgb(255, 28, 28, 28);

        public optionsWindow()
        {
            InitializeComponent();
        }

        private void optionsWindow_Load(object sender, EventArgs e)
        {
            panelLauncherSettings.BringToFront();
            tabLauncherDesc.Select();

            bStartDetector.Text = $"Start detector: {Convert.ToInt32(Properties.Settings.Default.startDetector)} second(s)";
            bEndDetector.Text = $"End detector: {Convert.ToInt32(Properties.Settings.Default.endDetector)} second(s)";

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
                    bHide.Text = "Hide Launcher";
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

            if (Properties.Settings.Default.clearCache)
            {
                bEnableClearCache.Text = "Enabled";
                bEnableClearCache.ForeColor = Color.DodgerBlue;
            }
            else
            {
                bEnableClearCache.Text = "Disabled";
                bEnableClearCache.ForeColor = Color.IndianRed;
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

            switch (Properties.Settings.Default.altCache)
            {
                case true:
                    bEnableClearCache.Text = "On SPT start";
                    bEnableClearCache.ForeColor = Color.DodgerBlue;
                    break;

                case false:
                    bEnableClearCache.Text = "On SPT stop";
                    bEnableClearCache.ForeColor = Color.DodgerBlue;
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
                Properties.Settings.Default.clearCache = true;
                Properties.Settings.Default.altCache = true;
            }
            else if (bEnableClearCache.Text.ToLower() == "on spt start")
            {
                bEnableClearCache.Text = "On SPT stop";
                bEnableClearCache.ForeColor = Color.DodgerBlue;
                Properties.Settings.Default.clearCache = true;
                Properties.Settings.Default.altCache = false;
            }
            else if (bEnableClearCache.Text.ToLower() == "on spt stop")
            {
                bEnableClearCache.Text = "Enabled";
                bEnableClearCache.ForeColor = Color.DodgerBlue;
                Properties.Settings.Default.clearCache = true;
                Properties.Settings.Default.altCache = false;
            }
            else if (bEnableClearCache.Text.ToLower() == "enabled")
            {
                bEnableClearCache.Text = "Disabled";
                bEnableClearCache.ForeColor = Color.IndianRed;
                Properties.Settings.Default.clearCache = false;
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
                frm.showError("Reset successful");
                Application.Restart();
            }
        }

        private void optionsWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void bStartDetector_Click(object sender, EventArgs e)
        {
            /*
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
            */
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

                    bCloseOnSPTExit.Text = "Enabled";
                    bCloseOnSPTExit.ForeColor = Color.DodgerBlue;

                    Properties.Settings.Default.hideOptions = 2;
                    Properties.Settings.Default.closeOnQuit = true;
                    Properties.Settings.Default.timedLauncherToggle = true;

                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                bEnableTimed.Text = "Enabled";
                bEnableTimed.ForeColor = Color.DodgerBlue;

                bHide.Text = "Close Launcher";
                bHide.ForeColor = Color.DodgerBlue;

                bCloseOnSPTExit.Text = "Enabled";
                bCloseOnSPTExit.ForeColor = Color.DodgerBlue;

                Properties.Settings.Default.hideOptions = 2;
                Properties.Settings.Default.closeOnQuit = true;
                Properties.Settings.Default.timedLauncherToggle = true;

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

                    Properties.Settings.Default.hideOptions = 0;
                    Properties.Settings.Default.closeOnQuit = false;
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
                Properties.Settings.Default.timedLauncherToggle = false;

                Properties.Settings.Default.Save();
            }
        }
    }
}
