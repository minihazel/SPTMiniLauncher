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
        public optionsWindow()
        {
            InitializeComponent();
        }

        private void optionsWindow_Load(object sender, EventArgs e)
        {

            if (Properties.Settings.Default.minimizeToggle)
            {
                bMinimize.Text = "Enabled";
                bMinimize.ForeColor = Color.DodgerBlue;
            }
            else
            {
                bMinimize.Text = "Disabled";
                bMinimize.ForeColor = Color.IndianRed;
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

                bEnableAltCache.Enabled = true;
            }
            else
            {
                bEnableClearCache.Text = "Disabled";
                bEnableClearCache.ForeColor = Color.IndianRed;

                bEnableAltCache.Enabled = false;
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

            switch (Properties.Settings.Default.displayStopConfirmation)
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
                    bEnableAltCache.Text = "On SPT start";
                    break;

                case false:
                    bEnableAltCache.Text = "On SPT stop";
                    break;
            }

            bStartDetector.Text = $"Start detector: {Convert.ToInt32(Properties.Settings.Default.startDetector)} second(s)";
            bEndDetector.Text = $"End detector: {Convert.ToInt32(Properties.Settings.Default.endDetector)} second(s)";
        }

        private void bMinimize_Click(object sender, EventArgs e)
        {
            if (bMinimize.Text.ToLower() == "enabled")
            {
                bMinimize.Text = "Disabled";
                bMinimize.ForeColor = Color.IndianRed;
                Properties.Settings.Default.minimizeToggle = false;
            }
            else
            {
                bMinimize.Text = "Enabled";
                bMinimize.ForeColor = Color.DodgerBlue;
                Properties.Settings.Default.minimizeToggle = true;
            }
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
        }

        private void bEnableClearCache_Click(object sender, EventArgs e)
        {
            if (bEnableClearCache.Text.ToLower() == "enabled")
            {
                bEnableClearCache.Text = "Disabled";
                bEnableClearCache.ForeColor = Color.IndianRed;
                Properties.Settings.Default.clearCache = false;

                bEnableAltCache.Enabled = false;
            }
            else
            {
                bEnableClearCache.Text = "Enabled";
                bEnableClearCache.ForeColor = Color.DodgerBlue;
                Properties.Settings.Default.clearCache = true;

                bEnableAltCache.Enabled = true;
            }
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
            if (MessageBox.Show("Reset third party apps?", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Properties.Settings.Default.profile_editor_path = "";
                Properties.Settings.Default.svm_path = "";
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
                Properties.Settings.Default.displayStopConfirmation = false;
            }
            else
            {
                bEnableConfirmation.Text = "Enabled";
                bEnableConfirmation.ForeColor = Color.DodgerBlue;
                Properties.Settings.Default.displayStopConfirmation = true;
            }

            Properties.Settings.Default.Save();
        }

        private void bEnableAltCache_Click(object sender, EventArgs e)
        {
            if (bEnableAltCache.Text.ToLower() == "on spt stop")
            {
                bEnableAltCache.Text = "On SPT start";
                Properties.Settings.Default.altCache = true;
            }
            else
            {
                bEnableAltCache.Text = "On SPT stop";
                Properties.Settings.Default.altCache = false;
            }
        }
    }
}
