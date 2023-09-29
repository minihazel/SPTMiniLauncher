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
using System.Windows;
using System.Windows.Forms;
using System.IO.Compression;
using System.Security.Cryptography;

namespace SPTMiniLauncher
{
    public partial class modExport : Form
    {
        string mainFolder;
        public string currentDir = Environment.CurrentDirectory;
        string[] serverMods = { };
        string[] clientMods = { };

        //public Color listBackcolor = Color.FromArgb(255, 35, 35, 35);
        public Color listBackcolor = Color.FromArgb(255, 28, 28, 28);

        // public Color listSelectedcolor = Color.FromArgb(255, 50, 50, 50);
        public Color listSelectedcolor = Color.FromArgb(255, 38, 38, 38);

        public Color listHovercolor = Color.FromArgb(255, 33, 33, 33);

        public static string GetRelativePath(string fromPath, string toPath)
        {
            if (string.IsNullOrEmpty(fromPath) || string.IsNullOrEmpty(toPath))
            {
                throw new ArgumentNullException("Paths cannot be null or empty.");
            }

            Uri fromUri = new Uri(fromPath);
            Uri toUri = new Uri(toPath);

            if (fromUri.Scheme != toUri.Scheme)
            {
                return toPath;
            }

            Uri relativeUri = fromUri.MakeRelativeUri(toUri);
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            if (toUri.Scheme.Equals("file", StringComparison.OrdinalIgnoreCase))
            {
                relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            }

            return relativePath;
        }

        public modExport()
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

        private void modExport_Load(object sender, EventArgs e)
        {
            // mainFolder = Path.Combine(currentDir, "Exported mods");
            mainFolder = currentDir;

            loadClientMods();
            loadServerMods();

            generateClientMods();
            generateServerMods();
        }

        public static void arrInsert(ref string[] array, string item)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = item;
        }

        private async Task exportModsAsync()
        {
            int clientMods = int.Parse(bCounterClientMods.Text);
            int serverMods = int.Parse(bCounterServerMods.Text);
            string result = "";

            IEnumerable<Label> selectedClientMods = panelClientMods.Controls
                .OfType<Label>()
                .Where(lbl => lbl.ForeColor == Color.DodgerBlue);
            IEnumerable<Label> selectedServerMods = panelServerMods.Controls
                .OfType<Label>()
                .Where(lbl => lbl.ForeColor == Color.DodgerBlue);

            if (selectedClientMods.Any() && clientMods > 0)
            {
                result = await ExportClientModsAsync();
            }

            if (selectedServerMods.Any() && serverMods > 0)
            {
                result += await ExportServerModsAsync();
            }

            await zipMods();

            result += $"Mods exported successfully. Would you like to open the transferred archive?";

            statusExportingMods.Visible = false;
            if (System.Windows.Forms.MessageBox.Show(result, "Export result", MessageBoxButtons.OK) == DialogResult.Yes)
            {
                try
                {
                    ProcessStartInfo newApp = new ProcessStartInfo();
                    newApp.WorkingDirectory = currentDir;
                    newApp.FileName = Path.GetFileName(newApp.WorkingDirectory);
                    newApp.UseShellExecute = true;
                    newApp.Verb = "open";

                    Process.Start(newApp);
                }
                catch (Exception err)
                {
                    Debug.WriteLine($"ERROR: {err.ToString()}");
                    System.Windows.Forms.MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                }
            }
        }

        public static void AddFolderContentsToZip(ZipArchive archive, string folderPath, string entryPath)
        {
            string[] files = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                string relativePath = GetRelativePath(folderPath, file);
                string entryName = Path.Combine(entryPath, relativePath);
                archive.CreateEntryFromFile(file, entryName);
            }
        }

        private async Task zipMods()
        {
            try
            {
                string homeBepinFolder = Path.Combine(mainFolder, "BepInEx");
                string homePluginsFolder = Path.Combine(homeBepinFolder, "plugins");
                string homeUserFolder = Path.Combine(mainFolder, "user");
                string homeModsFolder = Path.Combine(homeUserFolder, "mods");

                string zipPath = Path.Combine(currentDir, "Exported mods.zip");
                bool fileExists = File.Exists(zipPath);
                if (!fileExists)
                {
                    ZipFile.CreateFromDirectory(homeBepinFolder, zipPath, CompressionLevel.NoCompression, true);
                }

                using (FileStream zipFileStream = new FileStream(zipPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (ZipArchive archive = new ZipArchive(zipFileStream, ZipArchiveMode.Update))
                    {
                        AddFolderContentsToZip(archive, homeUserFolder, "");
                    }
                }

                /*
                bool homeBepinFolderExists = Directory.Exists(homeBepinFolder);
                if (homeBepinFolderExists)
                {
                    Directory.Delete(homeBepinFolder, true);
                }

                bool homeUserFolderExists = Directory.Exists(homeUserFolder);
                if (homeUserFolderExists)
                    Directory.Delete(homeUserFolder, true);
                */
            }
            catch (Exception err)
            {
                Debug.WriteLine($"ERROR: {err.ToString()}");
                System.Windows.Forms.MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
            }
        }

        private async Task<string> ExportClientModsAsync()
        {
            try
            {
                // Prepare client mods
                string homeBepinFolder = Path.Combine(mainFolder, "BepInEx");
                string homePluginsFolder = Path.Combine(homeBepinFolder, "plugins");

                bool homeBepinFolderExists = Directory.Exists(homeBepinFolder);
                if (homeBepinFolderExists)
                {
                    Directory.Delete(homeBepinFolder, true);
                }

                Directory.CreateDirectory(Path.Combine(mainFolder, "BepInEx"));
                Directory.CreateDirectory(Path.Combine(homeBepinFolder, "plugins"));

                // Enumerate and fetch
                IEnumerable<Label> clientMods = panelClientMods.Controls
                    .OfType<Label>()
                    .Where(lbl => lbl.ForeColor == Color.DodgerBlue);

                // Check folders
                string bepinFolder = Path.Combine(Properties.Settings.Default.server_path, "BepInEx");
                bool bepinFolderExists = Directory.Exists(bepinFolder);
                if (bepinFolderExists)
                {
                    string pluginsFolder = Path.Combine(bepinFolder, "plugins");
                    bool pluginsFolderExists = Directory.Exists(pluginsFolder);
                    if (pluginsFolderExists)
                    {
                        List<string> movedItems = new List<string>();

                        // Enumerate and copy
                        foreach (Label lbl in clientMods)
                        {
                            string fileName = lbl.Text.Replace("[*] ", "");
                            string fullPath = Path.Combine(pluginsFolder, fileName);
                            string homeFullPath = Path.Combine(homePluginsFolder, fileName);

                            bool fileExists = File.Exists(fullPath);
                            if (fileExists)
                            {
                                File.Copy(fullPath, homeFullPath);
                                File.SetAttributes(homeFullPath, FileAttributes.Normal);

                                movedItems.Add(Path.GetFileName(homeFullPath));
                            }
                            else
                            {
                                bool folderExists = Directory.Exists(fullPath);
                                if (folderExists)
                                {
                                    CopyDirectory(fullPath, homeFullPath);

                                    movedItems.Add(Path.GetFileName(homeFullPath));
                                }
                            }
                        }

                        return $"Client mods export successful. Moved items:\n\n{string.Join("\n", movedItems)}\n\n";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error during export: {ex.Message}";
            }

            return "Export failed";
        }

        private async Task<string> ExportServerModsAsync()
        {
            try
            {
                // Prepare server mods
                string homeUserFolder = Path.Combine(mainFolder, "user");
                string homeModsFolder = Path.Combine(homeUserFolder, "mods");

                bool homeUserFolderExists = Directory.Exists(homeUserFolder);
                if (homeUserFolderExists)
                {
                    Directory.Delete(homeUserFolder, true);
                }

                Directory.CreateDirectory(Path.Combine(mainFolder, "user"));
                Directory.CreateDirectory(Path.Combine(homeUserFolder, "mods"));

                // Enumerate and fetch
                IEnumerable<Label> serverMods = panelServerMods.Controls
                    .OfType<Label>()
                    .Where(lbl => lbl.ForeColor == Color.DodgerBlue);

                // Check folders
                string userFolder = Path.Combine(Properties.Settings.Default.server_path, "user");
                bool userFolderExists = Directory.Exists(userFolder);
                if (userFolderExists)
                {
                    string modsFolder = Path.Combine(userFolder, "mods");
                    bool modsFolderExists = Directory.Exists(modsFolder);
                    if (modsFolderExists)
                    {
                        List<string> movedItems = new List<string>();

                        // Enumerate and copy
                        foreach (Label lbl in serverMods)
                        {
                            string fileName = lbl.Text.Replace("[*] ", "");
                            string fullPath = Path.Combine(modsFolder, fileName);
                            string homeFullPath = Path.Combine(homeModsFolder, fileName);

                            bool folderExists = Directory.Exists(fullPath);
                            if (folderExists)
                            {
                                CopyDirectory(fullPath, homeFullPath);

                                movedItems.Add(Path.GetFileName(homeFullPath));
                            }
                        }

                        return $"Server export successful. Moved items:\n{string.Join("\n", movedItems)}\n\n";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error during export: {ex.Message}";
            }

            return "Export failed";
        }

        private void exportClientMods()
        {
            // Prepare client mods
            string homeBepinFolder = Path.Combine(mainFolder, "BepInEx");
            string homePluginsFolder = Path.Combine(homeBepinFolder, "plugins");

            bool homeBepinFolderExists = Directory.Exists(homeBepinFolder);
            if (homeBepinFolderExists)
            {
                Directory.Delete(homeBepinFolder, true);
            }

            Directory.CreateDirectory(Path.Combine(mainFolder, "BepInEx"));
            Directory.CreateDirectory(Path.Combine(homeBepinFolder, "plugins"));

            // Enumerate and fetch
            IEnumerable<Label> clientMods = panelClientMods.Controls
                .OfType<Label>()
                .Where(lbl => lbl.ForeColor == Color.DodgerBlue);

            // Check folders
            string bepinFolder = Path.Combine(Properties.Settings.Default.server_path, "BepInEx");
            bool bepinFolderExists = Directory.Exists(bepinFolder);
            if (bepinFolderExists)
            {
                string pluginsFolder = Path.Combine(bepinFolder, "plugins");
                bool pluginsFolderExists = Directory.Exists(pluginsFolder);
                if (pluginsFolderExists)
                {
                    // Enumerate and copy
                    foreach (Label lbl in clientMods)
                    {
                        string fileName = lbl.Text.Replace("[*] ", "");
                        string fullPath = Path.Combine(pluginsFolder, fileName);
                        string homeFullPath = Path.Combine(homePluginsFolder, fileName);

                        bool fileExists = File.Exists(fullPath);
                        if (fileExists)
                        {
                            fullPath = $"{fullPath}.dll";
                            File.Copy(fullPath, homeFullPath);
                            File.SetAttributes(homeFullPath, FileAttributes.Normal);
                        }
                        else
                        {
                            bool folderExists = Directory.Exists(fullPath);
                            if (folderExists)
                            {
                                CopyDirectory(fullPath, homeFullPath);
                            }
                        }
                    }
                }
            }
        }

        private void exportServerMods()
        {
            // Prepare server mods
            string homeUserFolder = Path.Combine(mainFolder, "user");
            string homeModsFolder = Path.Combine(homeUserFolder, "mods");

            bool homeUserFolderExists = Directory.Exists(homeUserFolder);
            if (homeUserFolderExists)
                Directory.Delete(homeUserFolder, true);

            Directory.CreateDirectory(Path.Combine(mainFolder, "user"));
            Directory.CreateDirectory(Path.Combine(homeUserFolder, "mods"));

            // Enumerate and fetch
            IEnumerable<Label> serverMods = panelServerMods.Controls
                .OfType<Label>()
                .Where(lbl => lbl.ForeColor == Color.DodgerBlue);

            // Check folders
            string userFolder = Path.Combine(Properties.Settings.Default.server_path, "user");
            bool userFolderExists = Directory.Exists(userFolder);
            if (userFolderExists)
            {
                string modsFolder = Path.Combine(userFolder, "mods");
                bool modsFolderExists = Directory.Exists(modsFolder);
                if (modsFolderExists)
                {
                    // Enumerate and copy
                    foreach (Label lbl in serverMods)
                    {
                        string fileName = lbl.Text.Replace("[*] ", "");
                        string fullPath = Path.Combine(modsFolder, fileName);
                        string homeFullPath = Path.Combine(homeModsFolder, fileName);

                        bool folderExists = Directory.Exists(fullPath);
                        if (folderExists)
                        {
                            CopyDirectory(fullPath, homeFullPath);
                        }
                    }
                }
            }
        }

        static void CopyDirectory(string source, string destination)
        {
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }

            foreach (string file in Directory.GetFiles(source))
            {
                string destFile = Path.Combine(destination, Path.GetFileName(file));
                File.Copy(file, destFile, true); // Set to true to overwrite files if they already exist
                File.SetAttributes(destFile, FileAttributes.Normal);
            }

            foreach (string subDirectory in Directory.GetDirectories(source))
            {
                string destDirectory = Path.Combine(destination, Path.GetFileName(subDirectory));
                CopyDirectory(subDirectory, destDirectory);
            }
        }

        private void selectAllMods(bool isClient)
        {
            if (isClient)
            {
                foreach (Control ctrl in panelClientMods.Controls)
                {
                    if (ctrl is Label lbl)
                    {
                        lbl.Text = $"[*] {lbl.Text}";
                        lbl.ForeColor = Color.DodgerBlue;
                    }
                }
            }
            else
            {
                foreach (Control ctrl in panelServerMods.Controls)
                {
                    if (ctrl is Label lbl)
                    {
                        lbl.Text = $"[*] {lbl.Text}";
                        lbl.ForeColor = Color.DodgerBlue;
                    }
                }
            }
        }

        private void unselectAllMods(bool isClient)
        {
            if (isClient)
            {
                foreach (Control ctrl in panelClientMods.Controls)
                {
                    if (ctrl is Label lbl)
                    {
                        lbl.Text = lbl.Text.Replace("[*] ", "");
                        lbl.ForeColor = Color.LightGray;
                    }
                }
            }
            else
            {
                foreach (Control ctrl in panelServerMods.Controls)
                {
                    if (ctrl is Label lbl)
                    {
                        lbl.Text = lbl.Text.Replace("[*] ", "");
                        lbl.ForeColor = Color.LightGray;
                    }
                }
            }
        }

        int countLabelsWithColor(Panel panel, Color targetColor)
        {
            int count = 0;

            foreach (Control control in panel.Controls)
            {
                if (control is Label label)
                {
                    if (label.ForeColor == targetColor)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private void refreshClientCounter()
        {
            int counter = countLabelsWithColor(panelClientMods, Color.DodgerBlue);
            bCounterClientMods.Text = counter.ToString();
        }

        private void refreshServerCounter()
        {
            int counter = countLabelsWithColor(panelServerMods, Color.DodgerBlue);
            bCounterServerMods.Text = counter.ToString();
        }

        private void loadClientMods()
        {
            string bepinFolder = Path.Combine(Properties.Settings.Default.server_path, "BepInEx");
            bool bepinFolderExists = Directory.Exists(bepinFolder);
            if (bepinFolderExists)
            {
                string pluginsFolder = Path.Combine(bepinFolder, "plugins");
                bool pluginsFolderExists = Directory.Exists(pluginsFolder);
                if (pluginsFolderExists)
                {
                    panelClientMods.Controls.Clear();

                    string[] modsFolders = Directory.GetDirectories(pluginsFolder);
                    foreach (string mod in modsFolders)
                    {
                        if (Path.GetFileName(mod) == "spt")
                            continue;

                        arrInsert(ref clientMods, Path.GetFileName(mod));
                    }

                    string[] modsFiles = Directory.GetFiles(pluginsFolder, "*.dll");
                    foreach (string mod in modsFiles)
                    {
                        arrInsert(ref clientMods, Path.GetFileName(mod));
                    }
                }
            }
        }

        private void loadServerMods()
        {
            string userFolder = Path.Combine(Properties.Settings.Default.server_path, "user");
            bool userFolderExists = Directory.Exists(userFolder);
            if (userFolderExists)
            {
                string modsFolder = Path.Combine(userFolder, "mods");
                bool modsFolderExists = Directory.Exists(modsFolder);
                if (modsFolderExists)
                {
                    panelServerMods.Controls.Clear();

                    string[] mods = Directory.GetDirectories(modsFolder);
                    foreach (string mod in mods)
                    {
                        arrInsert(ref serverMods, Path.GetFileName(mod));
                    }
                }
            }
        }

        private void generateClientMods()
        {
            for (int i = 0; i < clientMods.Length; i++)
            {
                Label lbl = new Label();
                lbl.AutoSize = false;
                lbl.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
                lbl.TextAlign = ContentAlignment.MiddleLeft;
                lbl.Size = new System.Drawing.Size(panelClientMods.Size.Width, 30);
                lbl.Location = new System.Drawing.Point(0, 0 + (i * 30));

                lbl.Cursor = Cursors.Hand;
                lbl.Text = clientMods[i];
                lbl.BackColor = listBackcolor;
                lbl.ForeColor = Color.LightGray;
                lbl.Font = new System.Drawing.Font("Bahnschrift Light", 9, System.Drawing.FontStyle.Regular);

                lbl.Margin = new Padding(1, 1, 1, 1);
                lbl.MouseEnter += new EventHandler(mods_MouseEnter);
                lbl.MouseLeave += new EventHandler(mods_MouseLeave);
                lbl.MouseDown += new MouseEventHandler(mods_MouseDown);
                panelClientMods.Controls.Add(lbl);
            }
        }

        private void generateServerMods()
        {
            for (int i = 0; i < serverMods.Length; i++)
            {
                Label lbl = new Label();
                lbl.AutoSize = false;
                lbl.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
                lbl.TextAlign = ContentAlignment.MiddleLeft;
                lbl.Size = new System.Drawing.Size(panelServerMods.Size.Width, 30);
                lbl.Location = new System.Drawing.Point(0, 0 + (i * 30));

                lbl.Cursor = Cursors.Hand;
                lbl.Text = serverMods[i];
                lbl.BackColor = listBackcolor;
                lbl.ForeColor = Color.LightGray;
                lbl.Font = new System.Drawing.Font("Bahnschrift Light", 9, System.Drawing.FontStyle.Regular);

                lbl.Margin = new Padding(1, 1, 1, 1);
                lbl.MouseEnter += new EventHandler(mods_MouseEnter);
                lbl.MouseLeave += new EventHandler(mods_MouseLeave);
                lbl.MouseDown += new MouseEventHandler(mods_MouseDown);
                panelServerMods.Controls.Add(lbl);
            }
        }

        private void mods_MouseEnter(object sender, EventArgs e)
        {
            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;
            if (label.Text != "")
            {
                if (label.BackColor != listSelectedcolor)
                {
                    label.BackColor = listHovercolor;
                }
            }
        }

        private void mods_MouseLeave(object sender, EventArgs e)
        {
            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;
            if (label.Text != "")
            {
                if (label.BackColor != listSelectedcolor)
                {
                    label.BackColor = listBackcolor;
                }
            }
        }

        private void mods_MouseDown(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;
            if (label.Text != "")
            {
                if (label.ForeColor == Color.DodgerBlue)
                {
                    label.Text = label.Text.Replace("[*] ", "");
                    label.ForeColor = Color.LightGray;
                }
                else
                {
                    label.Text = $"[*] {label.Text}";
                    label.ForeColor = Color.DodgerBlue;
                }

                Panel pnl = (Panel)label.Parent;
                if (pnl.Name.ToLower() == "panelclientmods")
                    refreshClientCounter();
                else
                    refreshServerCounter();
            }
        }

        private void bSelectAllClientMods_Click(object sender, EventArgs e)
        {
            selectAllMods(true);
            refreshClientCounter();
        }

        private void bUnselectAllClientMods_Click(object sender, EventArgs e)
        {
            unselectAllMods(true);
            refreshClientCounter();
        }

        private void bSelectAllServerMods_Click(object sender, EventArgs e)
        {
            selectAllMods(false);
            refreshServerCounter();
        }

        private void bUnselectAllServerMods_Click(object sender, EventArgs e)
        {
            unselectAllMods(false);
            refreshServerCounter();
        }

        private async void bExportSelectedMods_Click(object sender, EventArgs e)
        {
            int clientMods = int.Parse(bCounterClientMods.Text);
            int serverMods = int.Parse(bCounterServerMods.Text);

            string homeBepinFolder = Path.Combine(mainFolder, "BepInEx");
            string homeUserFolder = Path.Combine(mainFolder, "user");

            string content = $"Do you wish to export {clientMods} client mods and {serverMods} server mods? This may take a minute.";
            if (System.Windows.Forms.MessageBox.Show(content, this.Text, (MessageBoxButtons)MessageBoxButton.YesNo) == DialogResult.Yes)
            {
                statusExportingMods.Visible = true;
                await exportModsAsync();

                bool homeBepinFolderExists = Directory.Exists(homeBepinFolder);
                bool homeUserFolderExists = Directory.Exists(homeUserFolder);

                if (homeBepinFolderExists && homeUserFolderExists)
                {
                    try
                    {
                        Directory.Delete(homeBepinFolder, true);
                        Directory.Delete(homeUserFolder, true);
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine($"ERROR: {err.ToString()}");
                        System.Windows.Forms.MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                    }
                }
            }
        }
    }
}
