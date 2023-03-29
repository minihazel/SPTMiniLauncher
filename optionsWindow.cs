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
            }
            else
            {
                bEnableClearCache.Text = "Disabled";
                bEnableClearCache.ForeColor = Color.IndianRed;
            }
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
            }
            else
            {
                bEnableClearCache.Text = "Enabled";
                bEnableClearCache.ForeColor = Color.DodgerBlue;
                Properties.Settings.Default.clearCache = true;
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
    }
}
