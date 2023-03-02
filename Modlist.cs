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

        private void Modlist_Load(object sender, EventArgs e)
        {
            addMods();
            boxServerSeparator.Select();
        }

        private void addMods()
        {
            boxServerList.Items.Clear();
            string[] mods = Directory.GetDirectories(boxServerPlaceholder.Text);

            foreach (string item in mods)
            {
                boxServerList.Items.Add(System.IO.Path.GetFileName(item));
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
                                        string modFolder = System.IO.Path.Combine(boxServerPlaceholder.Text, System.IO.Path.GetFileNameWithoutExtension(packageJsonFolderPath));

                                        CopyDirectory(path, modFolder, true);
                                        addMods();
                                        boxServerSeparator.Select();

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

                }
                else if (boxServerOption.Text.ToLower() == "remove")
                {
                    if (MessageBox.Show($"You\'re about to delete {boxServerList.Text}, are you sure?\n\nTHIS ACTION IS IRREVERSIBLE.", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        string fullPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(boxServerPlaceholder.Text, boxServerList.Text));

                        try
                        {
                            Directory.Delete(fullPath);
                            addMods();
                        }
                        catch (Exception err)
                        {
                            Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                            MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                        }
                    }
                }
                else if (boxServerOption.Text.ToLower() == "open" || boxServerOption.Text.ToLower() == "open mods folder")
                {
                    try
                    {
                        Process.Start("explorer.exe", System.IO.Path.Combine(boxServerPlaceholder.Text, boxServerList.Text));
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void boxServerOption_Click(object sender, EventArgs e)
        {
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

                            try
                            {
                                // "Directory"
                                string packageJsonFolderPath = "";
                                FindPackageJsonFolder(path, ref packageJsonFolderPath);

                                if (packageJsonFolderPath != null || packageJsonFolderPath != "")
                                {
                                    string modFolder = System.IO.Path.Combine(boxServerPlaceholder.Text, System.IO.Path.GetFileNameWithoutExtension(packageJsonFolderPath));
                                    CopyDirectory(path, modFolder, true);
                                    addMods();
                                    boxServerSeparator.Select();

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
                        string path = System.IO.Path.Combine(Environment.CurrentDirectory, System.IO.Path.GetFileNameWithoutExtension(fullPath));
                        try
                        {
                            // "Directory"
                            string packageJsonFolderPath = "";
                            FindPackageJsonFolder(fullPath, ref packageJsonFolderPath);

                            if (packageJsonFolderPath != null || packageJsonFolderPath != "")
                            {
                                string modFolder = System.IO.Path.Combine(boxServerPlaceholder.Text, System.IO.Path.GetFileNameWithoutExtension(fullPath));
                                CopyDirectory(fullPath, modFolder, true);
                                addMods();
                                boxServerSeparator.Select();

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
        }

        private void boxServerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (boxServerList.SelectedIndex > -1 && boxServerList.Text.Length > 0)
            {
                boxServerOption.Text = "Open";
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
    }
}
