using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json.Linq;
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
using System.Xml.Linq;

namespace SPTMiniLauncher
{
    public partial class Form1 : Form
    {
        public string serverPath;
        public bool isLoneServer = false;

        public Color listBackcolor = Color.FromArgb(255, 35, 35, 35);
        public Color listSelectedcolor = Color.FromArgb(255, 50, 50, 50);
        public Color listHovercolor = Color.FromArgb(255, 45, 45, 45);

        public Process server;
        public Process launcher;

        string[] serverOptionsStreets = {
            "-", // - is Actions
            "Clear cache",
            "Clear cache + Run SPT",
            "Stop SPT (if running)",
            "Delete server",
            "", // blank is Mods
            "Open server mods",
            "Open modloader JSON",
            "Open client mods",
        };

        string[] serverOptions = {
            "-", // - is Actions
            "Clear cache",
            "Clear cache + Run SPT",
            "Stop SPT (if running)",
            "Delete server",
            "", // blank is Mods
            "Open server mods",
            "Open client mods",
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.server_path != null || Properties.Settings.Default.server_path != "" || Properties.Settings.Default.server_path.Length > 0)
            {
                boxPath.Text = Properties.Settings.Default.server_path;
                if (Directory.Exists(boxPath.Text))
                {
                    if (File.Exists(Path.Combine(boxPath.Text, "Aki.Server.exe")) &&
                        File.Exists(Path.Combine(boxPath.Text, "Aki.Launcher.exe")) &&
                        Directory.Exists(Path.Combine(boxPath.Text, "Aki_Data")))
                    {
                        isLoneServer = true;
                        listAllServers(boxPath.Text);
                    }
                    else
                    {
                        isLoneServer = false;
                        listAllServers(boxPath.Text);
                    }
                }
            }
            else
            {
                if (File.Exists(Path.Combine(Environment.CurrentDirectory, "Aki.Server.exe")) &&
                        File.Exists(Path.Combine(Environment.CurrentDirectory, "Aki.Launcher.exe")) &&
                        Directory.Exists(Path.Combine(Environment.CurrentDirectory, "Aki_Data")))
                {
                    // actual app is in an SPT installation folder
                    isLoneServer = true;
                    boxPath.Text = Environment.CurrentDirectory;
                    Properties.Settings.Default.server_path = boxPath.Text;
                    Properties.Settings.Default.Save();
                    listAllServers(boxPath.Text);
                }
                else
                {
                    showError("It looks like this you have reset the app, or it\'s your first time. Please drag and drop or browse for an SPT folder to begin!");
                    /*
                    isLoneServer = false;
                    Properties.Settings.Default.server_path = boxPath.Text;
                    Properties.Settings.Default.Save();
                    listAllServers(boxPath.Text);
                    */
                }
            }
            boxPathBox.Select();
        }

        private void showError(string content)
        {
            MessageBox.Show(content, this.Text, MessageBoxButtons.OK);
        }

        private void clearUI()
        {
            // server box
            for (int i = boxServers.Controls.Count - 1; i >= 0; i--)
            {
                Label selected = boxServers.Controls[i] as Label;
                if ((selected != null) && (selected.Name.ToLower() != "boxserverstitle"))
                {
                    try
                    {
                        boxServers.Controls.RemoveAt(i);
                        selected.Dispose();
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                    }
                }
            }

            // option box
            for (int i = boxSelectedServer.Controls.Count - 1; i >= 0; i--)
            {

                Label selected = boxSelectedServer.Controls[i] as Label;
                if ((selected != null) && (selected.Name.ToLower() != "boxselectedservertitle"))
                {
                    try
                    {
                        boxSelectedServer.Controls.RemoveAt(i);
                        selected.Dispose();
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void listAllServers(string path)
        {
            clearUI();

            if (isLoneServer)
            {
                Label lbl = new Label();
                lbl.Text = Path.GetFileName(path);
                lbl.AutoSize = false;
                lbl.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
                lbl.TextAlign = ContentAlignment.MiddleLeft;
                lbl.Size = new Size(boxServerPlaceholder.Size.Width, boxServerPlaceholder.Size.Height);
                lbl.Location = new Point(boxServerPlaceholder.Location.X, boxServerPlaceholder.Location.Y);
                lbl.Font = new Font("Bahnschrift Light", 9, FontStyle.Regular);
                lbl.BackColor = listBackcolor;
                lbl.ForeColor = Color.LightGray;
                lbl.Margin = new Padding(1, 1, 1, 1);
                lbl.Cursor = Cursors.Hand;
                lbl.MouseEnter += new EventHandler(lbl_MouseEnter);
                lbl.MouseLeave += new EventHandler(lbl_MouseLeave);
                lbl.MouseDown += new MouseEventHandler(lbl_MouseDown);
                lbl.MouseUp += new MouseEventHandler(lbl_MouseUp);
                boxServers.Controls.Add(lbl);

                boxServersTitle.Text = "Listed server";
                checkVersion(Path.Combine(Properties.Settings.Default.server_path, "Aki_Data\\Server\\configs\\core.json"));
            }
            else
            {
                string[] dirs = Directory.GetDirectories(path);
                if (dirs.Length > 0)
                {
                    for (int i = 0; i < dirs.Length; i++)
                    {
                        if (File.Exists(Path.Combine(Properties.Settings.Default.server_path, $"{dirs[i]}\\Aki.Server.exe")))
                        {
                            Label lbl = new Label();
                            lbl.Text = Path.GetFileName(dirs[i]);
                            lbl.AutoSize = false;
                            lbl.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
                            lbl.TextAlign = ContentAlignment.MiddleLeft;
                            lbl.Size = new Size(boxServerPlaceholder.Size.Width, boxServerPlaceholder.Size.Height);
                            lbl.Location = new Point(boxServerPlaceholder.Location.X, boxServerPlaceholder.Location.Y + (i * 30));
                            lbl.Font = new Font("Bahnschrift Light", 9, FontStyle.Regular);
                            lbl.BackColor = listBackcolor;
                            lbl.ForeColor = Color.LightGray;
                            lbl.Margin = new Padding(1, 1, 1, 1);
                            lbl.Cursor = Cursors.Hand;
                            lbl.MouseEnter += new EventHandler(lbl_MouseEnter);
                            lbl.MouseLeave += new EventHandler(lbl_MouseLeave);
                            lbl.MouseDown += new MouseEventHandler(lbl_MouseDown);
                            lbl.MouseUp += new MouseEventHandler(lbl_MouseUp);
                            boxServers.Controls.Add(lbl);
                        }
                        else
                        {
                            Label lbl = new Label();
                            lbl.Text = $"No SPT detected";
                            lbl.AutoSize = false;
                            lbl.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
                            lbl.TextAlign = ContentAlignment.MiddleLeft;
                            lbl.Size = new Size(boxServerPlaceholder.Size.Width, boxServerPlaceholder.Size.Height);
                            lbl.Location = new Point(boxServerPlaceholder.Location.X, boxServerPlaceholder.Location.Y + (i * 30));
                            lbl.Font = new Font("Bahnschrift Light", 9, FontStyle.Regular);
                            lbl.BackColor = listBackcolor;
                            lbl.ForeColor = Color.LightGray;
                            lbl.Margin = new Padding(1, 1, 1, 1);
                            lbl.Cursor = Cursors.Hand;
                            lbl.MouseEnter += new EventHandler(lbl_MouseEnter);
                            lbl.MouseLeave += new EventHandler(lbl_MouseLeave);
                            lbl.MouseDown += new MouseEventHandler(lbl_MouseDown);
                            lbl.MouseUp += new MouseEventHandler(lbl_MouseUp);
                            boxServers.Controls.Add(lbl);
                        }
                    }

                    boxServersTitle.Text = "Listed servers";
                }
            }
        }

        private void lbl_MouseEnter(object sender, EventArgs e)
        {
            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;
            if (label.Text != "")
            {
                label.BackColor = listHovercolor;
            }
        }

        private void lbl_MouseLeave(object sender, EventArgs e)
        {
            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;
            if (label.Text != "")
            {
                label.BackColor = listBackcolor;
            }
        }

        private void lbl_MouseDown(object sender, EventArgs e)
        {
            System.Windows.Forms.Label lbl = (System.Windows.Forms.Label)sender;
            if (lbl.Text != "")
            {
                lbl.BackColor = listSelectedcolor;
                boxSelectedServerTitle.Text = lbl.Text;

                for (int i = boxSelectedServer.Controls.Count - 1; i >= 0; i--)
                {
                    if ((boxSelectedServer.Controls[i] is Label selected) && (selected.Name.ToLower() != "boxselectedservertitle"))
                    {
                        try
                        {
                            boxSelectedServer.Controls.RemoveAt(i);
                            selected.Dispose();
                        }
                        catch (Exception err)
                        {
                            Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                            MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                        }
                    }
                }

                try
                {
                    if (isLoneServer)
                    {
                        if (File.Exists(Path.Combine(Properties.Settings.Default.server_path, "Aki_Data\\Server\\configs\\core.json")))
                        {
                            checkVersion(Path.Combine(Properties.Settings.Default.server_path, "Aki_Data\\Server\\configs\\core.json"));
                        }
                    }
                    else
                    {

                        if (File.Exists(Path.Combine(Properties.Settings.Default.server_path, $"{lbl.Text}\\Aki_Data\\Server\\configs\\core.json")))
                        {
                            checkVersion(Path.Combine(Properties.Settings.Default.server_path, $"{lbl.Text}\\Aki_Data\\Server\\configs\\core.json"));
                        }
                    }
                }
                catch (Exception err)
                {
                    Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                }
            }
            boxPathBox.Select();
        }

        private void checkVersion(string path)
        {
            try
            {
                string read = File.ReadAllText(path);
                JObject parsed = JObject.Parse(read);

                if (parsed["compatibleTarkovVersion"].ToString().Contains("0.13"))
                {
                    listServerOptions(true);
                }
                else
                {
                    listServerOptions(false);
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
            }
        }

        private void lbl_MouseUp(object sender, EventArgs e)
        {
            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;
            if (label.Text != "")
            {
                label.BackColor = label.BackColor = listHovercolor;
            }
        }

        private void listServerOptions(bool isStreets)
        {
            try
            {
                if (isStreets)
                {
                    for (int i = 0; i < serverOptionsStreets.Length; i++)
                    {
                        Label lbl = new Label();
                        lbl.AutoSize = false;
                        lbl.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
                        lbl.TextAlign = ContentAlignment.MiddleLeft;
                        lbl.Size = new Size(boxSelectedServer.Size.Width, boxSelectedServerPlaceholder.Size.Height);
                        lbl.Location = new Point(boxSelectedServerPlaceholder.Location.X, boxSelectedServerPlaceholder.Location.Y + (i * 30));
                        lbl.Cursor = Cursors.Hand;

                        if (serverOptionsStreets[i] == "")
                        {
                            lbl.Text = "  Mods";
                            lbl.Cursor = Cursors.Arrow;
                            lbl.BackColor = this.BackColor;
                            lbl.ForeColor = Color.DodgerBlue;
                            lbl.Font = new Font("Bahnschrift Light", 10, FontStyle.Regular);
                        }
                        else if (serverOptionsStreets[i] == "-")
                        {
                            lbl.Text = "  Actions";
                            lbl.Cursor = Cursors.Arrow;
                            lbl.BackColor = this.BackColor;
                            lbl.ForeColor = Color.IndianRed;
                            lbl.Font = new Font("Bahnschrift Light", 10, FontStyle.Regular);
                        }
                        else
                        {
                            lbl.Text = serverOptionsStreets[i];
                            lbl.BackColor = listBackcolor;
                            lbl.ForeColor = Color.LightGray;
                            lbl.Font = new Font("Bahnschrift Light", 9, FontStyle.Regular);
                        }

                        lbl.Margin = new Padding(1, 1, 1, 1);
                        lbl.MouseEnter += new EventHandler(lbl2_MouseEnter);
                        lbl.MouseLeave += new EventHandler(lbl2_MouseLeave);
                        lbl.MouseDown += new MouseEventHandler(lbl2_MouseDown);
                        lbl.MouseUp += new MouseEventHandler(lbl2_MouseUp);
                        boxSelectedServer.Controls.Add(lbl);
                    }
                }
                else
                {

                    for (int i = 0; i < serverOptions.Length; i++)
                    {
                        Label lbl = new Label();
                        lbl.AutoSize = false;
                        lbl.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
                        lbl.TextAlign = ContentAlignment.MiddleLeft;
                        lbl.Size = new Size(boxSelectedServer.Size.Width, boxSelectedServerPlaceholder.Size.Height);
                        lbl.Location = new Point(boxSelectedServerPlaceholder.Location.X, boxSelectedServerPlaceholder.Location.Y + (i * 30));

                        if (serverOptions[i] == "")
                        {
                            lbl.Text = "  Mods";
                            lbl.BackColor = this.BackColor;
                            lbl.ForeColor = Color.DodgerBlue;
                            lbl.Font = new Font("Bahnschrift Light", 10, FontStyle.Regular);
                        }
                        else if (serverOptions[i] == "-")
                        {
                            lbl.Text = "  Actions";
                            lbl.BackColor = this.BackColor;
                            lbl.ForeColor = Color.IndianRed;
                            lbl.Font = new Font("Bahnschrift Light", 10, FontStyle.Regular);
                        }
                        else
                        {
                            lbl.Text = serverOptions[i];
                            lbl.BackColor = listBackcolor;
                            lbl.ForeColor = Color.LightGray;
                            lbl.Font = new Font("Bahnschrift Light", 9, FontStyle.Regular);
                        }

                        lbl.Margin = new Padding(1, 1, 1, 1);
                        lbl.Cursor = Cursors.Hand;
                        lbl.MouseEnter += new EventHandler(lbl2_MouseEnter);
                        lbl.MouseLeave += new EventHandler(lbl2_MouseLeave);
                        lbl.MouseDown += new MouseEventHandler(lbl2_MouseDown);
                        lbl.MouseUp += new MouseEventHandler(lbl2_MouseUp);
                        boxSelectedServer.Controls.Add(lbl);
                    }

                }

            }
            catch (Exception err)
            {
                Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
            }
        }

        private void lbl2_MouseEnter(object sender, EventArgs e)
        {
            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;
            if (label.Text != "" && label.Text != "  Mods" && label.Text != "  Actions")
            {
                label.BackColor = listHovercolor;
            }
        }

        private void lbl2_MouseLeave(object sender, EventArgs e)
        {
            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;
            if (label.Text != "" && label.Text != "  Mods" && label.Text != "  Actions")
            {
                label.BackColor = listBackcolor;
            }
        }

        private void lbl2_MouseDown(object sender, EventArgs e)
        {
            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;
            if (label.Text != "" && label.Text != "  Mods" && label.Text != "  Actions")
            {
                string currentDir = Directory.GetCurrentDirectory();
                label.BackColor = listSelectedcolor;

                switch (label.Text.ToLower())
                {
                    case "clear cache":
                        if (isLoneServer)
                        {
                            if (Directory.Exists($"{Properties.Settings.Default.server_path}\\user\\cache"))
                            {
                                try
                                {
                                    Directory.Delete($"{Properties.Settings.Default.server_path}\\user\\cache", true);
                                    showError("Cache cleared!");
                                }
                                catch (Exception err)
                                {
                                    Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                }
                            }

                        }
                        else
                        {
                            if (Directory.Exists($"{Properties.Settings.Default.server_path}\\{boxSelectedServerTitle.Text}\\user\\cache"))
                            {
                                try
                                {
                                    Directory.Delete($"{Properties.Settings.Default.server_path}\\{boxSelectedServerTitle.Text}\\user\\cache", true);
                                    showError("Cache cleared!");
                                }
                                catch (Exception err)
                                {
                                    Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                }
                            }
                        }
                        break;

                    case "clear cache + run spt":

                        if (isLoneServer)
                        {
                            if (Directory.Exists($"{Properties.Settings.Default.server_path}\\user\\cache"))
                            {
                                try
                                {
                                    Directory.Delete($"{Properties.Settings.Default.server_path}\\user\\cache", true);
                                }
                                catch (Exception err)
                                {
                                    Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                }
                            }

                            // server
                            Directory.SetCurrentDirectory(Properties.Settings.Default.server_path);
                            server = new Process();
                            server.StartInfo.WorkingDirectory = Properties.Settings.Default.server_path;
                            server.StartInfo.FileName = "Aki.Server.exe";
                            server.StartInfo.CreateNoWindow = false;
                            server.StartInfo.UseShellExecute = false;
                            server.StartInfo.RedirectStandardOutput = false;
                            try
                            {
                                server.Start();
                                this.WindowState = FormWindowState.Normal;
                                this.Focus();
                            }
                            catch (Exception err)
                            {
                                Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                            }

                            Directory.SetCurrentDirectory(currentDir);

                            // launcher
                            Directory.SetCurrentDirectory(Properties.Settings.Default.server_path);
                            launcher = new Process();
                            launcher.StartInfo.WorkingDirectory = Properties.Settings.Default.server_path;
                            launcher.StartInfo.FileName = "Aki.Launcher.exe";
                            launcher.StartInfo.CreateNoWindow = false;
                            launcher.StartInfo.UseShellExecute = false;
                            launcher.StartInfo.RedirectStandardOutput = false;
                            try
                            {
                                launcher.Start();
                                this.WindowState = FormWindowState.Normal;
                                this.Focus();
                            }
                            catch (Exception err)
                            {
                                Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                            }

                            Directory.SetCurrentDirectory(currentDir);

                        }
                        else
                        {
                            if (Directory.Exists($"{Properties.Settings.Default.server_path}\\{boxSelectedServerPlaceholder.Text}\\user\\cache"))
                            {
                                try
                                {
                                    Directory.Delete($"{Properties.Settings.Default.server_path}\\{boxSelectedServerPlaceholder.Text}\\user\\cache", true);
                                }
                                catch (Exception err)
                                {
                                    Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                }
                            }

                            // server
                            Directory.SetCurrentDirectory($"{Properties.Settings.Default.server_path}\\{boxSelectedServerPlaceholder.Text}");
                            server = new Process();
                            server.StartInfo.WorkingDirectory = $"{Properties.Settings.Default.server_path}\\{boxSelectedServerPlaceholder.Text}";
                            server.StartInfo.FileName = "Aki.Server.exe";
                            server.StartInfo.CreateNoWindow = false;
                            server.StartInfo.UseShellExecute = false;
                            server.StartInfo.RedirectStandardOutput = false;
                            try
                            {
                                server.Start();
                                this.WindowState = FormWindowState.Normal;
                                this.Focus();
                            }
                            catch (Exception err)
                            {
                                Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                            }

                            Directory.SetCurrentDirectory(currentDir);

                            // launcher
                            Directory.SetCurrentDirectory($"{Properties.Settings.Default.server_path}\\{boxSelectedServerPlaceholder.Text}");
                            launcher = new Process();
                            launcher.StartInfo.WorkingDirectory = $"{Properties.Settings.Default.server_path}\\{boxSelectedServerPlaceholder.Text}";
                            launcher.StartInfo.FileName = "Aki.Launcher.exe";
                            launcher.StartInfo.CreateNoWindow = false;
                            launcher.StartInfo.UseShellExecute = false;
                            launcher.StartInfo.RedirectStandardOutput = false;
                            try
                            {
                                launcher.Start();
                                this.WindowState = FormWindowState.Normal;
                                this.Focus();
                            }
                            catch (Exception err)
                            {
                                Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                            }

                            Directory.SetCurrentDirectory(currentDir);

                        }

                        WindowState = FormWindowState.Minimized;
                        break;

                    case "run spt":
                        // unused
                        if (isLoneServer)
                        {
                            // server
                            Directory.SetCurrentDirectory(Properties.Settings.Default.server_path);
                            server = new Process();
                            server.StartInfo.WorkingDirectory = Properties.Settings.Default.server_path;
                            server.StartInfo.FileName = "Aki.Server.exe";
                            server.StartInfo.CreateNoWindow = false;
                            server.StartInfo.UseShellExecute = false;
                            server.StartInfo.RedirectStandardOutput = false;
                            try
                            {
                                server.Start();
                                this.WindowState = FormWindowState.Normal;
                                this.Focus();
                            }
                            catch (Exception err)
                            {
                                Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                            }
                            Directory.SetCurrentDirectory(currentDir);

                            // launcher
                            Directory.SetCurrentDirectory(Properties.Settings.Default.server_path);
                            launcher = new Process();
                            launcher.StartInfo.WorkingDirectory = Properties.Settings.Default.server_path;
                            launcher.StartInfo.FileName = "Aki.Launcher.exe";
                            launcher.StartInfo.CreateNoWindow = false;
                            launcher.StartInfo.UseShellExecute = false;
                            launcher.StartInfo.RedirectStandardOutput = false;
                            try
                            {
                                launcher.Start();
                                this.WindowState = FormWindowState.Normal;
                                this.Focus();
                            }
                            catch (Exception err)
                            {
                                Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                            }
                            Directory.SetCurrentDirectory(currentDir);
                        }
                        else
                        {
                            // server
                            Directory.SetCurrentDirectory($"{Properties.Settings.Default.server_path}\\{boxSelectedServerTitle.Text}");
                            server = new Process();
                            server.StartInfo.WorkingDirectory = $"{Properties.Settings.Default.server_path}\\{boxSelectedServerTitle.Text}";
                            server.StartInfo.FileName = "Aki.Server.exe";
                            server.StartInfo.CreateNoWindow = false;
                            server.StartInfo.UseShellExecute = false;
                            server.StartInfo.RedirectStandardOutput = false;
                            try
                            {
                                server.Start();
                                this.WindowState = FormWindowState.Normal;
                                this.Focus();
                            }
                            catch (Exception err)
                            {
                                Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                            }
                            Directory.SetCurrentDirectory(currentDir);

                            // launcher
                            Directory.SetCurrentDirectory($"{Properties.Settings.Default.server_path}\\{boxSelectedServerTitle.Text}");
                            launcher = new Process();
                            launcher.StartInfo.WorkingDirectory = $"{Properties.Settings.Default.server_path}\\{boxSelectedServerTitle.Text}";
                            launcher.StartInfo.FileName = "Aki.Launcher.exe";
                            launcher.StartInfo.CreateNoWindow = false;
                            launcher.StartInfo.UseShellExecute = false;
                            launcher.StartInfo.RedirectStandardOutput = false;
                            try
                            {
                                launcher.Start();
                                this.WindowState = FormWindowState.Normal;
                                this.Focus();
                            }
                            catch (Exception err)
                            {
                                Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                            }
                            Directory.SetCurrentDirectory(currentDir);
                        }
                        break;

                    case "open server mods":

                        if (isLoneServer)
                        {
                            if (Directory.Exists($"{Properties.Settings.Default.server_path}\\user\\mods"))
                            {
                                try
                                {
                                    Process.Start("explorer.exe", $"{Properties.Settings.Default.server_path}\\user\\mods");
                                }
                                catch (Exception err)
                                {
                                    Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                }
                            }
                        }
                        else
                        {
                            if (Directory.Exists($"{Properties.Settings.Default.server_path}\\{boxSelectedServerTitle.Text}\\user\\mods"))
                            {
                                try
                                {
                                    Process.Start("explorer.exe", $"{Properties.Settings.Default.server_path}\\{boxSelectedServerTitle.Text}\\user\\mods");
                                }
                                catch (Exception err)
                                {
                                    Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                }
                            }
                        }
                        break;

                    case "open modloader json":

                        if (isLoneServer)
                        {
                            if (File.Exists($"{Properties.Settings.Default.server_path}\\user\\mods\\order.json"))
                            {
                                try
                                {

                                    Process.Start(Path.Combine(Properties.Settings.Default.server_path, "user\\mods\\order.json"));
                                }
                                catch (Exception err)
                                {
                                    Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                }
                            }
                        }
                        else
                        {
                            if (File.Exists($"{Properties.Settings.Default.server_path}\\{boxSelectedServerTitle.Text}\\user\\mods\\order.json"))
                            {
                                try
                                {

                                    Process.Start(Path.Combine(Properties.Settings.Default.server_path, $"{boxSelectedServerTitle.Text}\\user\\mods\\order.json"));
                                }
                                catch (Exception err)
                                {
                                    Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                }
                            }
                        }
                        break;

                    case "open client mods":

                        if (isLoneServer)
                        {
                            if (Directory.Exists($"{Properties.Settings.Default.server_path}\\BepInEx\\plugins"))
                            {
                                try
                                {
                                    Process.Start("explorer.exe", $"{Properties.Settings.Default.server_path}\\BepInEx\\plugins");
                                }
                                catch (Exception err)
                                {
                                    Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                }
                            }
                        }
                        else
                        {
                            if (Directory.Exists($"{Properties.Settings.Default.server_path}\\{boxSelectedServerTitle.Text}\\BepInEx\\plugins"))
                            {
                                try
                                {
                                    Process.Start("explorer.exe", $"{Properties.Settings.Default.server_path}\\{boxSelectedServerTitle.Text}\\BepInEx\\plugins");
                                }
                                catch (Exception err)
                                {
                                    Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                }
                            }
                        }
                        break;

                    case "stop spt (if running)":

                        if (isLoneServer)
                        {
                            int confirm = 0;
                            if (Directory.Exists(Properties.Settings.Default.server_path))
                            {
                                try
                                {
                                    Process[] proc = Process.GetProcesses();
                                    foreach (Process p in proc)
                                    {
                                        if (p.ProcessName.ToLower() == "aki.server")
                                        {
                                            string dir = Directory.GetParent(p.MainModule.FileName).FullName;
                                            if (Path.GetFileName(dir) == boxSelectedServerTitle.Text)
                                            {
                                                try
                                                {
                                                    p.Kill();
                                                    confirm++;
                                                }
                                                catch (Exception err)
                                                {
                                                    Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                                }
                                            }
                                        }
                                        else if (p.ProcessName.ToLower() == "aki.launcher")
                                        {
                                            string dir = Directory.GetParent(p.MainModule.FileName).FullName;
                                            if (Path.GetFileName(dir) == boxSelectedServerTitle.Text)
                                            {
                                                try
                                                {
                                                    p.Kill();
                                                    confirm++;
                                                }
                                                catch (Exception err)
                                                {
                                                    Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                                }
                                            }
                                        }
                                    }

                                    if (confirm == 2)
                                    {
                                        showError("Server and launcher stopped!");
                                    }
                                }
                                catch (Exception err)
                                {
                                    Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                }
                            }
                        }
                        else
                        {
                            int confirm = 0;
                            if (Directory.Exists($"{Properties.Settings.Default.server_path}\\{boxSelectedServerTitle.Text}"))
                            {
                                try
                                {
                                    Process[] proc = Process.GetProcesses();
                                    foreach (Process p in proc)
                                    {
                                        if (p.ProcessName.ToLower() == "aki.server")
                                        {
                                            string dir = Directory.GetParent(p.MainModule.FileName).FullName;
                                            if (Path.GetFileName(dir) == boxSelectedServerTitle.Text)
                                            {
                                                try
                                                {
                                                    p.Kill();
                                                    confirm++;
                                                }
                                                catch (Exception err)
                                                {
                                                    Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                                }
                                            }
                                        }
                                        else if (p.ProcessName.ToLower() == "aki.launcher")
                                        {
                                            string dir = Directory.GetParent(p.MainModule.FileName).FullName;
                                            if (Path.GetFileName(dir) == boxSelectedServerTitle.Text)
                                            {
                                                try
                                                {
                                                    p.Kill();
                                                    confirm++;
                                                }
                                                catch (Exception err)
                                                {
                                                    Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                                }
                                            }
                                        }
                                    }

                                    if (confirm == 2)
                                    {
                                        showError("Server and launcher stopped!");
                                    }
                                }
                                catch (Exception err)
                                {
                                    Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                }
                            }
                        }
                        break;

                    case "delete server":

                        if (isLoneServer)
                        {
                            if (Directory.Exists(Properties.Settings.Default.server_path))
                            {
                                if (MessageBox.Show($"Do you wish to delete {boxSelectedServerTitle.Text}?\nThis action is irreversible!", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    try
                                    {
                                        Directory.Delete(Properties.Settings.Default.server_path, true);
                                        showError($"Server {boxSelectedServerTitle.Text} has been deleted. Please drag and drop a server folder, or type the path to one.");
                                        clearUI();
                                        //listAllServers(Properties.Settings.Default.server_path);
                                    }
                                    catch (Exception err)
                                    {
                                        Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                    }
                                }

                            }
                        }
                        else
                        {
                            if (Directory.Exists($"{Properties.Settings.Default.server_path}\\{boxSelectedServerTitle.Text}"))
                            {
                                if (MessageBox.Show($"Do you wish to delete {boxSelectedServerTitle.Text}?\nThis action is irreversible!", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    try
                                    {
                                        Directory.Delete($"{Properties.Settings.Default.server_path}\\{boxSelectedServerTitle.Text}", true);
                                        listAllServers(Properties.Settings.Default.server_path);
                                    }
                                    catch (Exception err)
                                    {
                                        Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                    }
                                }

                            }
                        }
                        break;
                }
            }
            boxPathBox.Select();
        }

        private void lbl2_MouseUp(object sender, EventArgs e)
        {
            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;
            if (label.Text != "" && label.Text != "  Mods" && label.Text != "  Actions")
            {
                label.BackColor = label.BackColor = listHovercolor;
            }
        }

        private void boxOpenIn_MouseEnter(object sender, EventArgs e)
        {
            boxOpenIn.ForeColor = Color.DodgerBlue;
        }

        private void boxOpenIn_MouseLeave(object sender, EventArgs e)
        {
            boxOpenIn.ForeColor = Color.LightGray;
        }

        private void boxBrowse_MouseEnter(object sender, EventArgs e)
        {
            boxBrowse.ForeColor = Color.DodgerBlue;
        }

        private void boxBrowse_MouseLeave(object sender, EventArgs e)
        {
            boxBrowse.ForeColor = Color.LightGray;
        }

        private void boxBrowse_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string fullPath = Path.GetFullPath(dialog.FileName);
                if (Directory.Exists(fullPath))
                {

                    if (File.Exists(Path.Combine(fullPath, "Aki.Server.exe")) &&
                        File.Exists(Path.Combine(fullPath, "Aki.Launcher.exe")) &&
                        Directory.Exists(Path.Combine(fullPath, "Aki_Data")))
                    {
                        // actual SPT installation
                        boxPath.Text = fullPath;
                        Properties.Settings.Default.server_path = boxPath.Text;
                        Properties.Settings.Default.Save();

                        isLoneServer = true;
                        listAllServers(boxPath.Text);
                    }
                    else
                    {
                        boxPath.Text = fullPath;
                        Properties.Settings.Default.server_path = boxPath.Text;
                        Properties.Settings.Default.Save();

                        isLoneServer = false;
                        listAllServers(boxPath.Text);
                    }
                }

            }

            /*
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                boxPath.Text = dialog.FileName;
                Properties.Settings.Default.server_path = boxPath.Text;

                listAllServers(boxPath.Text);
            }
            */
        }

        private void boxOpenIn_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", boxPath.Text);
            }
            catch (Exception err)
            {
                Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
            }
        }

        private void boxSelectedServerTitle_Click(object sender, EventArgs e)
        {
            if (boxSelectedServerTitle.Text.ToLower() != "spt placeholder" && boxSelectedServerTitle.Text.ToLower() != "no spt detected")
            {
                try
                {
                    Process.Start("explorer.exe", $"{Properties.Settings.Default.server_path}\\{boxSelectedServerTitle.Text}");
                }
                catch (Exception err)
                {
                    Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                }
            }
            else
            {
                showError("Please select a valid server first!");
            }
        }

        private void boxSelectedServerTitle_MouseEnter(object sender, EventArgs e)
        {
            boxSelectedServerTitle.ForeColor = Color.DodgerBlue;
        }

        private void boxSelectedServerTitle_MouseLeave(object sender, EventArgs e)
        {
            boxSelectedServerTitle.ForeColor = Color.LightGray;
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] items = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string item in items)
            {
                try
                {
                    if (File.Exists(Path.Combine(Path.GetFullPath(item), "Aki.Server.exe")) &&
                        File.Exists(Path.Combine(Path.GetFullPath(item), "Aki.Launcher.exe")) &&
                        Directory.Exists(Path.Combine(Path.GetFullPath(item), "Aki_Data")))
                    {
                        // Chosen folder actually contains an SPT installation
                        boxPath.Text = Path.GetFullPath(item);
                        Properties.Settings.Default.server_path = boxPath.Text;
                        Properties.Settings.Default.Save();
                        isLoneServer = true;
                        listAllServers(boxPath.Text);
                    }
                    else
                    {
                        try
                        {
                            boxPath.Text = Path.GetFullPath(item);
                            Properties.Settings.Default.server_path = boxPath.Text;
                            Properties.Settings.Default.Save();
                            isLoneServer = false;
                            listAllServers(boxPath.Text);
                        }
                        catch (Exception err)
                        {
                            Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                            MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                        }
                    }
                }
                catch (Exception err)
                {
                    Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                }
            }
            boxPathBox.Select();
        }

        private void bResetApp_Click(object sender, EventArgs e)
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
                Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
            }
        }

        private void bResetApp_MouseEnter(object sender, EventArgs e)
        {
            bResetApp.ForeColor = Color.DodgerBlue;
        }

        private void bResetApp_MouseLeave(object sender, EventArgs e)
        {
            bResetApp.ForeColor = Color.DarkGray;
        }

        private void boxPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (boxPath.Text.Length > 0)
            {

                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;

                    Properties.Settings.Default.server_path = boxPath.Text;
                    Properties.Settings.Default.Save();

                    if (Directory.Exists(boxPath.Text))
                    {
                        if (File.Exists(Path.Combine(boxPath.Text, "Aki.Server.exe")) &&
                        File.Exists(Path.Combine(boxPath.Text, "Aki.Launcher.exe")) &&
                        Directory.Exists(Path.Combine(boxPath.Text, "Aki_Data")))
                        {
                            // Chosen folder actually contains an SPT installation
                            isLoneServer = true;
                            listAllServers(boxPath.Text);
                            boxPathBox.Select();

                        }
                        else
                        {
                            try
                            {
                                isLoneServer = false;
                                listAllServers(boxPath.Text);
                                boxPathBox.Select();
                            }
                            catch (Exception err)
                            {
                                Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                            }
                        }

                    }
                }
            }
        }

        private void boxSelectedServerTitle_Click_1(object sender, EventArgs e)
        {
            if (boxSelectedServerTitle.Text.ToLower() != "spt placeholder" && boxSelectedServerTitle.Text.ToLower() != "no spt detected")
            {
                if (isLoneServer)
                {
                    try
                    {
                        Process.Start("explorer.exe", Properties.Settings.Default.server_path);
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                    }
                }
                else
                {
                    try
                    {
                        Process.Start("explorer.exe", $"{Properties.Settings.Default.server_path}\\{boxSelectedServerTitle.Text}");
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                    }
                }
            }
            else
            {
                showError("Please select a valid server first!");
            }
        }

        private void boxSelectedServerTitle_MouseEnter_1(object sender, EventArgs e)
        {
            boxSelectedServerTitle.ForeColor = Color.DodgerBlue;
        }

        private void boxSelectedServerTitle_MouseLeave_1(object sender, EventArgs e)
        {
            boxSelectedServerTitle.ForeColor = Color.LightGray;
        }
    }
}
