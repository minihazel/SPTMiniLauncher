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
    public partial class controlWindow : Form
    {
        public bool isTrue = false;
        public string fullServerPath = "";
        public string selectedServer = "";
        Form1 mainForm = new Form1();

        public controlWindow()
        {
            InitializeComponent();
        }

        private void controlWindow_Load(object sender, EventArgs e)
        {
            if (isTrue)
            {
                fullServerPath = Properties.Settings.Default.server_path;
            }
            else
            {
                fullServerPath = Path.Combine(Properties.Settings.Default.server_path, selectedServer);
                Debug.WriteLine(selectedServer);
                Debug.WriteLine(fullServerPath);
            }

            AssignEvents();
        }

        private void AssignEvents()
        {
            foreach (Control control in panelFolders.Controls)
            {
                if (control is Button btn)
                {
                    btn.Click += globalLabel_Click;
                    btn.MouseEnter += globalLabel_MouseEnter;
                    btn.MouseLeave += globalLabel_MouseLeave;
                }
            }

            foreach (Control control in panelGlobalFiles.Controls)
            {
                if (control is Button btn)
                {
                    btn.Click += globalLabel_Click;
                    btn.MouseEnter += globalLabel_MouseEnter;
                    btn.MouseLeave += globalLabel_MouseLeave;
                }
            }
        }

        private void globalLabel_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (Properties.Settings.Default.closeControlPanel)
                this.Close();
        }

        private void globalLabel_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.ForeColor = Color.LightGray;
        }

        private void globalLabel_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.ForeColor = Color.DodgerBlue;
        }

        private void bConfigs_Click(object sender, EventArgs e)
        {
            string relevantFolder = Path.Combine(fullServerPath, "Aki_Data\\Server\\configs");
            bool exists = Directory.Exists(relevantFolder);
            if (exists)
                Process.Start(relevantFolder);
        }

        private void bDatabase_Click(object sender, EventArgs e)
        {
            string relevantFolder = Path.Combine(fullServerPath, "Aki_Data\\Server\\database");
            bool exists = Directory.Exists(relevantFolder);
            if (exists)
                Process.Start(relevantFolder);
        }

        private void bBots_Click(object sender, EventArgs e)
        {
            string relevantFolder = Path.Combine(fullServerPath, "Aki_Data\\Server\\database\\bots");
            bool exists = Directory.Exists(relevantFolder);
            if (exists)
                Process.Start(relevantFolder);
        }

        private void bHideout_Click(object sender, EventArgs e)
        {
            string relevantFolder = Path.Combine(fullServerPath, "Aki_Data\\Server\\database\\hideout");
            bool exists = Directory.Exists(relevantFolder);
            if (exists)
                Process.Start(relevantFolder);
        }

        private void bLocales_Click(object sender, EventArgs e)
        {
            string relevantFolder = Path.Combine(fullServerPath, "Aki_Data\\Server\\database\\locales");
            bool exists = Directory.Exists(relevantFolder);
            if (exists)
                Process.Start(relevantFolder);
        }

        private void bMaps_Click(object sender, EventArgs e)
        {
            string relevantFolder = Path.Combine(fullServerPath, "Aki_Data\\Server\\database\\locations");
            bool exists = Directory.Exists(relevantFolder);
            if (exists)
                Process.Start(relevantFolder);
        }

        private void bLoot_Click(object sender, EventArgs e)
        {
            string relevantFolder = Path.Combine(fullServerPath, "Aki_Data\\Server\\database\\loot");
            bool exists = Directory.Exists(relevantFolder);
            if (exists)
                Process.Start(relevantFolder);
        }

        private void bMatch_Click(object sender, EventArgs e)
        {
            string relevantFolder = Path.Combine(fullServerPath, "Aki_Data\\Server\\database\\match");
            bool exists = Directory.Exists(relevantFolder);
            if (exists)
                Process.Start(relevantFolder);
        }

        private void bTemplates_Click(object sender, EventArgs e)
        {
            string relevantFolder = Path.Combine(fullServerPath, "Aki_Data\\Server\\database\\templates");
            bool exists = Directory.Exists(relevantFolder);
            if (exists)
                Process.Start(relevantFolder);
        }

        private void bTraders_Click(object sender, EventArgs e)
        {
            string relevantFolder = Path.Combine(fullServerPath, "Aki_Data\\Server\\database\\traders");
            bool exists = Directory.Exists(relevantFolder);
            if (exists)
                Process.Start(relevantFolder);
        }

        private void bBepCache_Click(object sender, EventArgs e)
        {
            string relevantFolder = Path.Combine(fullServerPath, "BepInEx\\config");
            bool exists = Directory.Exists(relevantFolder);
            if (exists)
                Process.Start(relevantFolder);
        }

        private void bBepPlugins_Click(object sender, EventArgs e)
        {
            string relevantFolder = Path.Combine(fullServerPath, "BepInEx\\plugins");
            bool exists = Directory.Exists(relevantFolder);
            if (exists)
                Process.Start(relevantFolder);
        }

        private void bLauncherConfig_Click(object sender, EventArgs e)
        {
            string relevantFile = Path.Combine(fullServerPath, "user\\launcher\\config.json");
            bool exists = File.Exists(relevantFile);
            if (exists)
                Process.Start(relevantFile);
        }

        private void bLogs_Click(object sender, EventArgs e)
        {
            string relevantFolder = Path.Combine(fullServerPath, "user\\logs");
            bool exists = Directory.Exists(relevantFolder);
            if (exists)
                Process.Start(relevantFolder);
        }

        private void bMods_Click(object sender, EventArgs e)
        {
            string relevantFolder = Path.Combine(fullServerPath, "user\\mods");
            bool exists = Directory.Exists(relevantFolder);
            if (exists)
                Process.Start(relevantFolder);
        }

        private void bProfiles_Click(object sender, EventArgs e)
        {
            string relevantFolder = Path.Combine(fullServerPath, "user\\profiles");
            bool exists = Directory.Exists(relevantFolder);
            if (exists)
                Process.Start(relevantFolder);
        }

        private void bFilesGlobals_Click(object sender, EventArgs e)
        {
            string relevantFile = Path.Combine(fullServerPath, "Aki_Data\\Server\\database\\globals.json");
            bool exists = File.Exists(relevantFile);
            if (exists)
                Process.Start(relevantFile);
        }

        private void bFilesServer_Click(object sender, EventArgs e)
        {
            string relevantFile = Path.Combine(fullServerPath, "Aki_Data\\Server\\database\\server.json");
            bool exists = File.Exists(relevantFile);
            if (exists)
                Process.Start(relevantFile);
        }

        private void bFilesSettings_Click(object sender, EventArgs e)
        {
            string relevantFile = Path.Combine(fullServerPath, "Aki_Data\\Server\\database\\settings.json");
            bool exists = File.Exists(relevantFile);
            if (exists)
                Process.Start(relevantFile);
        }

        private void bFilesItems_Click(object sender, EventArgs e)
        {
            string relevantFile = Path.Combine(fullServerPath, "Aki_Data\\Server\\database\\templates\\items.json");
            bool exists = File.Exists(relevantFile);
            if (exists)
                Process.Start(relevantFile);
        }

        private void bFilesQuests_Click(object sender, EventArgs e)
        {
            string relevantFile = Path.Combine(fullServerPath, "Aki_Data\\Server\\database\\templates\\quests.json");
            bool exists = File.Exists(relevantFile);
            if (exists)
                Process.Start(relevantFile);
        }

        private void bFilesProfiles_Click(object sender, EventArgs e)
        {
            string relevantFile = Path.Combine(fullServerPath, "Aki_Data\\Server\\database\\templates\\character.json");
            bool exists = File.Exists(relevantFile);
            if (exists)
                Process.Start(relevantFile);
        }
    }
}
