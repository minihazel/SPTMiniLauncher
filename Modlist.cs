using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;
using MS.WindowsAPICodePack.Internal;

namespace SPTMiniLauncher
{
    public partial class Modlist : Form
    {
        public Modlist()
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

        private void Modlist_Load(object sender, EventArgs e)
        {
            addMods();
            boxServerSeparator.Select();
        }

        private void addMods()
        {
            boxModList.Items.Clear();

            if (boxModsType.Text.ToLower().Contains("server")) {
                string[] mods = Directory.GetDirectories(boxPathPlaceholder.Text);

                foreach (string item in mods)
                {
                    boxModList.Items.Add(System.IO.Path.GetFileName(item));
                }

            } else if (boxModsType.Text.ToLower().Contains("client"))
            {
                List<string> collectedMods = new List<string>();
                string[] modFiles = Directory.GetFiles(boxPathPlaceholder.Text);
                string[] modFolders = Directory.GetDirectories(boxPathPlaceholder.Text);

                foreach (string item in modFiles)
                {
                    string fileItem = System.IO.Path.GetFileName(item);

                    if (fileItem != "aki-core.dll" &&
                        fileItem != "aki-custom.dll" &&
                        fileItem != "aki-debugging.dll" &&
                        fileItem != "aki-singleplayer.dll" &&
                        fileItem.ToLower() != "configurationmanager.dll")
                    {
                        collectedMods.Add(fileItem);
                    }
                }

                foreach (string item in modFolders)
                {
                    string fileItem = System.IO.Path.GetFileName(item);

                    collectedMods.Add(fileItem);
                }

                for (int i = 0; i < collectedMods.Count; i++)
                {
                    boxModList.Items.Add(collectedMods[i]);
                }
            }
        }

        private void boxServerOption_MouseEnter(object sender, EventArgs e)
        {
            boxServerOption.ForeColor = Color.DodgerBlue;
        }

        private void boxServerOption_MouseLeave(object sender, EventArgs e)
        {
            boxServerOption.ForeColor = Color.LightGray;
        }

        private void boxServerOption_Click(object sender, EventArgs e)
        {
        }

        private void boxServerOption_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            if ((Control.MouseButtons & MouseButtons.Right) != 0)
            {
                if (boxServerOption.Text.ToLower() == "open")
                {
                    boxServerOption.Text = "Add";
                }
                else if (boxServerOption.Text.ToLower() == "add")
                {
                    boxServerOption.Text = "Remove";
                }
                else if (boxServerOption.Text.ToLower() == "remove")
                {
                    boxServerOption.Text = "Open";
                }
                else if (boxServerOption.Text.ToLower() == "open mods folder")
                {
                    boxServerOption.Text = "Add new mod";
                }
                else if (boxServerOption.Text.ToLower() == "add new mod")
                {
                    boxServerOption.Text = "Open mods folder";
                }
            }
            else
            {
                if (boxServerOption.Text.ToLower() == "add" || boxServerOption.Text.ToLower() == "add new mod")
                {
                    if (boxModsType.Text.ToLower().Contains("client"))
                    {
                        showMessage("Unfortunately browsing is only availabe to server mods. We apologize for the inconvenience.\n\nDrag and drop might work in your case.");
                    }
                    else
                    {
                        CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                        dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        dialog.Title = "Select a server mod (folder only)";
                        dialog.IsFolderPicker = true;

                        if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                        {
                            string fullPath = System.IO.Path.GetFullPath(dialog.FileName);

                            if (Directory.Exists(fullPath))
                            {
                                string path = System.IO.Path.Combine(Environment.CurrentDirectory, System.IO.Path.GetFileNameWithoutExtension(fullPath));
                                if (Directory.Exists(path))
                                {
                                    Directory.Delete(path, true);
                                }

                                if (File.Exists(System.IO.Path.Combine(path, "package.json")))
                                {
                                    try
                                    {
                                        // "Directory"
                                        string packageJsonFolderPath = "";
                                        FindPackageJsonFolder(path, ref packageJsonFolderPath);

                                        if (packageJsonFolderPath != null || packageJsonFolderPath != "")
                                        {
                                            string modFolder = System.IO.Path.Combine(boxPathPlaceholder.Text, System.IO.Path.GetFileNameWithoutExtension(packageJsonFolderPath));
                                            if (!Directory.Exists(modFolder))
                                            {
                                                CopyDirectory(path, modFolder, true);
                                                boxServerSeparator.Select();
                                            } else
                                            {
                                                showMessage($"The server mod {System.IO.Path.GetFileName(modFolder)} already exists in user/mods!");
                                            }

                                            if (Directory.Exists(path))
                                            {
                                                Directory.Delete(path, true);
                                            }
                                        }

                                    }
                                    catch (Exception err)
                                    {
                                        Debug.WriteLine(err);
                                        MessageBox.Show($"This mod seems to be already installed! We\'ll cancel this for you.", this.Text, MessageBoxButtons.OK);

                                        if (Directory.Exists(path))
                                        {
                                            Directory.Delete(path, true);
                                        }
                                    }

                                }
                            }
                        }
                        addMods();
                    }
                }
                else if (boxServerOption.Text.ToLower() == "remove")
                {
                    if (MessageBox.Show($"You\'re about to delete {boxModList.Text}, are you sure?\n\nTHIS ACTION IS IRREVERSIBLE.", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        string fullPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(boxPathPlaceholder.Text, boxModList.Text));
                        if (boxModList.Text.Length > 0)
                        {
                            try
                            {
                                FileAttributes attr = File.GetAttributes(fullPath);

                                if (attr.HasFlag(FileAttributes.Directory))
                                {
                                    Directory.Delete(fullPath);
                                    addMods();
                                }
                                else
                                {
                                    File.Delete(fullPath);
                                    addMods();

                                }
                                
                            }
                            catch (Exception err)
                            {
                                Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                            }

                            
                        }
                        
                    }
                }
                else if (boxServerOption.Text.ToLower() == "open" || boxServerOption.Text.ToLower() == "open mods folder")
                {
                    try
                    {
                        if (boxModList.Text.Length > 0)
                        {
                            string fullPath = System.IO.Path.Combine(boxPathPlaceholder.Text, boxModList.Text);
                            FileAttributes attr = File.GetAttributes(fullPath);

                            if (attr.HasFlag(FileAttributes.Directory))
                            {
                                Process.Start("explorer.exe", fullPath);
                            } else
                            {
                                string argument = "/select, \"" + fullPath + "\"";
                                Process.Start("explorer.exe", argument);
                            }
                        }
                        else
                        {
                            string fullPath = boxPathPlaceholder.Text;
                            FileAttributes attr = File.GetAttributes(fullPath);

                            if (attr.HasFlag(FileAttributes.Directory))
                            {
                                Process.Start("explorer.exe", fullPath);
                            }
                            else
                            {
                                string argument = "/select, \"" + fullPath + "\"";
                                Process.Start("explorer.exe", argument);
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                    }
                }
            }
        }

        public void showMessage(string content)
        {
            MessageBox.Show(content, this.Text, MessageBoxButtons.OK);
        }

        private void Modlist_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void Modlist_DragDrop(object sender, DragEventArgs e)
        {

            /*
             * My futile attempt at documentation:
             * 
             * 1. Listing all items dropped into the window.
             * 2. Check what the full path of each item ends in.
             * 3. If it's a regular folder, copy it to the root folder.
             * 4. If it's a file, copy it straight to where it should go.
             * 5. If it's a zip, unpack it and repeat step 2 to 4.
             * 6. Delete the placeholder root folder.
             * 7. Finally, forcibly refresh the in-house mod list to avoid potential conflicts.
             *
             */


            string[] items = (string[])e.Data.GetData(DataFormats.FileDrop);
            int counter = 0;
            string[] arr = { };

            foreach (string item in items)
            {
                string fullPath = item;
                counter++;

                FileAttributes attr = File.GetAttributes(fullPath);
                if (fullPath.EndsWith(".zip"))
                {
                    try
                    {
                        using (ZipArchive archive = ZipFile.OpenRead(fullPath))
                        {
                            string path = System.IO.Path.Combine(Environment.CurrentDirectory, System.IO.Path.GetFileNameWithoutExtension(item));
                            if (Directory.Exists(path))
                            {
                                Directory.Delete(path, true);
                            }
                            archive.ExtractToDirectory(path);

                            FileAttributes arrZip = File.GetAttributes(path);
                            if (arrZip != System.IO.FileAttributes.Directory)
                            {
                                try
                                {
                                    string modFolder = System.IO.Path.Combine(boxPathPlaceholder.Text, $"{System.IO.Path.GetFileNameWithoutExtension(fullPath)}.dll");
                                    if (!File.Exists(modFolder))
                                    {
                                        File.Move(path, modFolder);
                                        showMessage($"Client mod {System.IO.Path.GetFileName(path)} installed successfully!");
                                    }
                                    else
                                    {
                                        showMessage($"Client mod {System.IO.Path.GetFileName(path)} is already installed!");
                                    }
                                }
                                catch (Exception err)
                                {
                                    Debug.WriteLine(err);
                                    MessageBox.Show($"This mod seems to be already installed! We\'ll cancel this for you.", this.Text, MessageBoxButtons.OK);

                                    if (Directory.Exists(path))
                                    {
                                        Directory.Delete(path, true);
                                    }
                                }
                            }
                            else
                            {
                                try
                                {
                                    SearchForPackageJson(path);

                                    if (Directory.Exists(path))
                                    {
                                        Directory.Delete(path, true);
                                    }

                                }
                                catch (Exception err)
                                {
                                    Debug.WriteLine(err);
                                    // MessageBox.Show($"1 This mod seems to be already installed! We\'ll cancel this for you.", this.Text, MessageBoxButtons.OK);
                                    if (Directory.Exists(path))
                                    {
                                        Directory.Delete(path, true);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                        MessageBox.Show($"This mod seems to be already installed! We\'ll cancel this for you.", this.Text, MessageBoxButtons.OK);
                    }
                }
                else if (fullPath.EndsWith(".7z"))
                {
                    MessageBox.Show("Unfortunately, 7z is not supported at the moment. We apologize for the inconvenience.", this.Text, MessageBoxButtons.OK);
                }
                else
                {
                    if (attr == System.IO.FileAttributes.Directory)
                    {
                        if (!File.Exists(System.IO.Path.Combine(fullPath, "package.json")))
                        {
                            try
                            {
                                CopyDirectory(fullPath, System.IO.Path.Combine(boxPathPlaceholder.Text, System.IO.Path.GetFileName(fullPath)), true);
                                showMessage($"Client mod {System.IO.Path.GetFileName(fullPath)} installed successfully!");
                            }
                            catch (Exception err)
                            {
                                Debug.WriteLine(err);
                                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                            }
                        } else
                        {
                            string path = System.IO.Path.Combine(Environment.CurrentDirectory, System.IO.Path.GetFileNameWithoutExtension(fullPath));
                            try
                            {
                                SearchForPackageJson(path);

                                if (Directory.Exists(path))
                                {
                                    Directory.Delete(path, true);
                                }
                            }
                            catch (Exception err)
                            {
                                Debug.WriteLine(err);
                                //MessageBox.Show($"2 This mod seems to be already installed! We\'ll cancel this for you.", this.Text, MessageBoxButtons.OK);

                                if (Directory.Exists(path))
                                {
                                    Directory.Delete(path, true);
                                }
                            }
                        }
                    }
                }
            }
            addMods();
        }

        private void boxServerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (boxModList.SelectedIndex > -1 && boxModList.Text.Length > 0)
            {
                boxServerOption.Text = "Open";
            }
        }

        public void SearchForPackageJson(string rootFolderPath)
        {
            bool isfound = false;
            string[] subDirectories = Directory.GetDirectories(rootFolderPath);

            foreach (string subDirectory in subDirectories)
            {
                SearchForPackageJson(subDirectory);
            }

            if (File.Exists(System.IO.Path.Combine(rootFolderPath, "package.json")))
            {
                Console.WriteLine($"package.json found in {rootFolderPath}");
                string modFolder = System.IO.Path.Combine(boxPathPlaceholder.Text, System.IO.Path.GetFileNameWithoutExtension(rootFolderPath));

                if (!Directory.Exists(modFolder))
                {
                    CopyDirectory(rootFolderPath, modFolder, true);
                    showMessage($"Server mod {System.IO.Path.GetFileNameWithoutExtension(rootFolderPath)} installed successfully!");
                    isfound = true;
                } else
                {
                    showMessage($"Server mod {System.IO.Path.GetFileNameWithoutExtension(rootFolderPath)} is already installed!");
                    isfound = false;
                }
            }

            if (isfound && !Directory.Exists(System.IO.Path.Combine(boxPathPlaceholder.Text, System.IO.Path.GetFileName(rootFolderPath))))
            {
                // showMessage($"Server mod {System.IO.Path.GetFileNameWithoutExtension(rootFolderPath)} does not have a package.json!");
                if (!Directory.Exists(System.IO.Path.Combine(boxPathPlaceholder.Text, System.IO.Path.GetFileName(rootFolderPath))))
                {
                    CopyDirectory(rootFolderPath, System.IO.Path.Combine(boxPathPlaceholder.Text, System.IO.Path.GetFileName(rootFolderPath)), true);
                    showMessage($"Client mod {System.IO.Path.GetFileName(rootFolderPath)} installed successfully!");
                }
                else
                {
                    showMessage($"Client mod {System.IO.Path.GetFileName(rootFolderPath)} is already installed!");
                }
            }
        }

        private void FindPackageJsonFolder(string rootFolderPath, ref string packageJsonFolderPath)
        {
            DirectoryInfo rootFolder = new DirectoryInfo(rootFolderPath);

            if (!rootFolder.Exists)
            {
                throw new DirectoryNotFoundException($"Root folder {rootFolderPath} not found.");
            }

            FileInfo[] packageJsonFiles = rootFolder.GetFiles("package.json", SearchOption.AllDirectories);

            if (packageJsonFiles.Length == 0)
            {
                throw new FileNotFoundException("No package.json files found in root folder and subdirectories.");
            }

            foreach (FileInfo packageJsonFile in packageJsonFiles)
            {
                packageJsonFolderPath = packageJsonFile.Directory.FullName;
                return;
            }
        }

        static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            var dir = new DirectoryInfo(sourceDir);

            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            DirectoryInfo[] dirs = dir.GetDirectories();
            Directory.CreateDirectory(destinationDir);
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = System.IO.Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath);
            }

            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = System.IO.Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }

            Directory.Delete(sourceDir, true);
        }

        private void Modlist_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 form = new Form1();

            if (loneServer.Text.ToLower() == "true")
            {
                string newPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(boxPathPlaceholder.Text, @"..\..\"));
                string modsFolder = System.IO.Path.Combine(newPath, "user\\mods");
                form.updateOrderJSON(modsFolder);
            }
            else if(loneServer.Text.ToLower() == "false")
            {
                string selectedServer = System.IO.Path.Combine(Properties.Settings.Default.server_path, this.Text);
                string modsFolder = System.IO.Path.Combine(selectedServer, "user\\mods");
                form.updateOrderJSON(modsFolder);
            }
        }
    }
}
