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
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SPTMiniLauncher
{
    public partial class Form1 : Form
    {
        // Start variables
        // Pre-set path variables
        // Palette
        // Pre-set processes

        public bool isLoneServer = false;
        public string selectedServer;
        public string settingsFile;
        public string firstTime;

        public string core;

        public Color listBackcolor = Color.FromArgb(255, 35, 35, 35);
        public Color listSelectedcolor = Color.FromArgb(255, 50, 50, 50);
        public Color listHovercolor = Color.FromArgb(255, 45, 45, 45);

        public Process server;
        public Process launcher;

        // Lists
        string[] serverOptionsStreets = {
            "- ACTIONS -",
            "Clear cache",
            "Run SPT",
            "Stop SPT (if running)",
            "Delete server",
            "- MODS -",
            "Open server mods",
            "Open modloader JSON",
            "Open client mods",
            "- THIRDPARTY -",
            "Open Load Order Editor (LOE)",
            "Open Profile Editor",
            "Open Server Value Modifier (SVM)",
            "Open SPT Realism"
        };
        string[] serverOptions = {
            "- ACTIONS -",
            "Clear cache",
            "Run SPT",
            "Stop SPT (if running)",
            "Delete server",
            "- MODS -",
            "Open server mods",
            "Open client mods",
            "- THIRDPARTY -",
            "Open Profile Editor",
            "Open Server Value Modifier (SVM)",
            "Open SPT Realism"
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            settingsFile = System.IO.Path.Combine(Environment.CurrentDirectory, "SPT Mini.json");
            firstTime = System.IO.Path.Combine(Environment.CurrentDirectory, "firsttime");

            messageBoard form = new messageBoard();
            RichTextBox messageBox = (RichTextBox)form.Controls["messageBox"];
            Label messageTitle = (Label)form.Controls["messageTitle"];
            messageTitle.ForeColor = Color.LightGray;

            if (File.Exists(settingsFile) && File.Exists(firstTime))
            {
                string readSettings = File.ReadAllText(settingsFile);
                JObject settingsObject = JObject.Parse(readSettings);

                if (settingsObject["showFirstTimeMessage"].ToString().ToLower() == "true")
                {
                    settingsObject.Property("showFirstTimeMessage").Value = "false";

                    messageTitle.Text = "First time setup";
                    messageBox.Text = File.ReadAllText(firstTime);

                    form.ShowDialog();
                    File.WriteAllText(settingsFile, settingsObject.ToString());
                }
                else
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
                            // boxPath.Text = Environment.CurrentDirectory;
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
            }
            else
            {
                messageTitle.Text = "Settings file was not detected!";
                messageBox.Text = $"We could not detect the settings file. Please restart so that the launcher can generate it!";
                form.ShowDialog();
            }
        }

        public void updateOrderJSON(string path)
        {

            // Personal reference: path means user/mods
            //
            // I guess I'll document this for whoever is interested lmao
            // pls no judgerino, I am dumdum with c soft
            //
            // This function will do the following:
            // 
            // 1. Check if order.json exists in the user/mods folder
            //    >> If it doesn't, create and structure it
            //
            // 2. If it exists, check the list of server mods against the order list
            //    >> If the order list is missing an existing mod, it adds said mod
            //
            // 3. Next, check if the order list contains any mods that don't exist anymore
            //    >> If it does, remove said non-existent mods from the order list
            //

            try
            {

                string orderFile = Path.Combine(path, "order.json");

                if (!File.Exists(orderFile))
                {
                    var jsonOrder = new { order = new List<string>() };
                    string json = JsonConvert.SerializeObject(jsonOrder, Formatting.Indented);
                    File.WriteAllText(orderFile, json);
                }

                string orderJSON = File.ReadAllText(orderFile);
                JObject order = JObject.Parse(orderJSON);
                string[] modsFolder = Directory.GetDirectories(path);

                List<JToken> removeMods = new List<JToken>();
                foreach (JToken mod in order["order"])
                {
                    string modName = mod.Value<string>();
                    if (!Array.Exists(modsFolder, s => Path.GetFileName(s).Equals(modName)))
                    {
                        removeMods.Add(mod);
                    }
                }

                foreach (JToken mod in removeMods)
                {
                    mod.Remove();
                }

                foreach (string mod in modsFolder)
                {
                    string name = Path.GetFileName(mod);
                    bool exists = ((JArray)order["order"]).Any(t => t.Value<string>() == name);

                    if (!exists)
                    {
                        ((JArray)order["order"]).Add(name);
                    }
                }

                File.WriteAllText(orderFile, order.ToString());

            }
            catch (Exception err)
            {
                Debug.WriteLine($"ERROR: {err}");
                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
            }
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

        private void checkThirdPartyApps(string path)
        {
            // load order editor (loe)
            string loepath = Path.Combine(path, "user\\mods\\Load Order Editor.exe");

            if (File.Exists(loepath))
            {
                Properties.Settings.Default.loe_path = loepath;
                Properties.Settings.Default.Save();

                serverOptionsStreets[10] = "Open Load Order Editor (LOE)";
            }
            else
            {
                Properties.Settings.Default.loe_path = "";
                Properties.Settings.Default.Save();

                serverOptionsStreets[10] = "LOE not detected - click to download";
            }

            // perform profile editor check
            string progFiles = Environment.ExpandEnvironmentVariables("%ProgramW6432%");

            if (Directory.Exists(Path.Combine(progFiles, "SPT-AKI Profile Editor")) &&
                File.Exists(Path.Combine(progFiles, "SPT-AKI Profile Editor\\SPT-AKI Profile Editor.exe")))
            {
                Properties.Settings.Default.profile_editor_path = Path.Combine(progFiles, "SPT-AKI Profile Editor");
                Properties.Settings.Default.Save();

                serverOptionsStreets[11] = "Open Profile Editor";
                serverOptions[9] = "Open Profile Editor";
            }
            else
            {
                Properties.Settings.Default.profile_editor_path = "";
                Properties.Settings.Default.Save();

                serverOptionsStreets[11] = "Profile Editor not detected - click to fix";
                serverOptions[9] = "Profile Editor not detected - click to fix";
            }

            // server value modifier (svm)
            string svmpath = Path.Combine(path, "user\\mods\\ServerValueModifier");

            if (Directory.Exists(svmpath) &&
                File.Exists(Path.Combine(svmpath, "GFVE.exe")))
            {
                Properties.Settings.Default.svm_path = svmpath;
                Properties.Settings.Default.Save();

                serverOptionsStreets[12] = "Open Server Value Modifier (SVM)";
                serverOptions[10] = "Open Server Value Modifier (SVM)";
            }
            else
            {
                Properties.Settings.Default.svm_path = "";
                Properties.Settings.Default.Save();

                serverOptionsStreets[12] = "SVM not detected - click to download";
                serverOptions[10] = "SVM not detected - click to download";
            }

            // spt realism
            string realismpath = Path.Combine(path, "user\\mods\\SPT-Realism-Mod");

            if (Directory.Exists(realismpath) &&
                File.Exists(Path.Combine(realismpath, "RealismModConfig.exe")))
            {
                Properties.Settings.Default.realism_path = realismpath;
                Properties.Settings.Default.Save();

                serverOptionsStreets[13] = "Open SPT Realism";
                serverOptions[11] = "Open SPT Realism";
            }
            else
            {
                Properties.Settings.Default.realism_path = "";
                Properties.Settings.Default.Save();

                serverOptionsStreets[13] = "SPT Realism not detected - click to download";
                serverOptions[11] = "SPT Realism not detected - click to download";
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
                boxSelectedServerTitle.Text = lbl.Text;

                core = Path.Combine(path, "Aki_Data\\Server\\configs\\core.json");

                if (File.Exists(core))
                {
                    string cacheFolder = Path.Combine(path, "user\\cache");
                    string serverModsFolder = Path.Combine(path, "user\\mods");
                    checkVersion(core);
                    checkThirdPartyApps(Properties.Settings.Default.server_path);
                    if (Directory.Exists(serverModsFolder))
                    {
                        updateOrderJSON(serverModsFolder);
                    }
                }
                else
                {
                    showError($"SPT metadata could not be found for single installation. UI will be cleared, please search for another installation.\n\nExpected path: {core}");
                    clearUI();
                }
            }
            else
            {
                List<string> directories = new List<string>();
                string[] dirs = Directory.GetDirectories(path);

                if (dirs.Length > 0)
                {
                    foreach (string dir in dirs)
                    {
                        selectedServer = dir;
                        string akiFile = Path.Combine(selectedServer, "Aki.Server.exe");
                        string launcherFile = Path.Combine(selectedServer, "Aki.Launcher.exe");
                        string akiData = Path.Combine(selectedServer, "Aki_Data");

                        if (Directory.Exists(akiData) && File.Exists(akiFile) && File.Exists(launcherFile))
                        {
                            directories.Add(Path.GetFileName(dir));
                        }
                    }

                    for (int i = 0; i < directories.Count; i++)
                    {
                        selectedServer = Path.Combine(path, Path.GetFileName(directories[i]));
                        string serverModsFolder = Path.Combine(selectedServer, "user\\mods");
                        Label lbl = new Label();
                        lbl.Text = Path.GetFileName(selectedServer);
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

                        if (Directory.Exists(serverModsFolder))
                        {
                            updateOrderJSON(serverModsFolder);
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
                        core = Path.Combine(Properties.Settings.Default.server_path, "Aki_Data\\Server\\configs\\core.json");
                        if (File.Exists(core))
                        {
                            checkVersion(core);
                        }
                    }
                    else
                    {
                        selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                        core = Path.Combine(selectedServer, "Aki_Data\\Server\\configs\\core.json");
                        if (File.Exists(core))
                        {
                            checkVersion(core);
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
                if (isLoneServer)
                {
                    checkThirdPartyApps(Properties.Settings.Default.server_path);
                }
                else
                {
                    selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                    checkThirdPartyApps(selectedServer);
                }

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

                        if (serverOptionsStreets[i].ToLower() == "run spt")
                        {
                            lbl.Text = "Run SPT";
                            lbl.BackColor = listBackcolor;
                            lbl.ForeColor = Color.DodgerBlue;
                            lbl.Font = new Font("Bahnschrift Light", 9, FontStyle.Regular);
                        }
                        else if (serverOptionsStreets[i].ToLower() == "delete server")
                        {
                            lbl.Text = "Delete server";
                            lbl.BackColor = listBackcolor;
                            lbl.ForeColor = Color.IndianRed;
                            lbl.Font = new Font("Bahnschrift Light", 9, FontStyle.Regular);
                        }
                        else if (serverOptionsStreets[i].ToLower() == "- mods -")
                        {
                            lbl.Text = "  Mods";
                            lbl.Cursor = Cursors.Arrow;
                            lbl.BackColor = this.BackColor;
                            lbl.ForeColor = Color.DodgerBlue;
                            lbl.Font = new Font("Bahnschrift Light", 10, FontStyle.Regular);
                        }
                        else if (serverOptionsStreets[i].ToLower() == "- actions -")
                        {
                            lbl.Text = "  Actions";
                            lbl.Cursor = Cursors.Arrow;
                            lbl.BackColor = this.BackColor;
                            lbl.ForeColor = Color.IndianRed;
                            lbl.Font = new Font("Bahnschrift Light", 10, FontStyle.Regular);
                        }
                        else if (serverOptionsStreets[i].ToLower() == "- thirdparty -")
                        {
                            lbl.Text = "  Third Party Apps";
                            lbl.Cursor = Cursors.Arrow;
                            lbl.BackColor = this.BackColor;
                            lbl.ForeColor = Color.DarkSeaGreen;
                            lbl.Font = new Font("Bahnschrift Light", 10, FontStyle.Regular);
                        }
                        else if (serverOptionsStreets[i].ToLower().Contains("not detected"))
                        {
                            lbl.Text = serverOptionsStreets[i];
                            lbl.BackColor = listBackcolor;
                            lbl.ForeColor = Color.IndianRed;
                            lbl.Font = new Font("Bahnschrift Light", 9, FontStyle.Regular);
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

                        if (serverOptions[i].ToLower() == "run spt")
                        {
                            lbl.Text = "Run SPT";
                            lbl.BackColor = listBackcolor;
                            lbl.ForeColor = Color.DodgerBlue;
                            lbl.Font = new Font("Bahnschrift Light", 9, FontStyle.Regular);
                        }
                        else if (serverOptions[i].ToLower() == "delete server")
                        {
                            lbl.Text = "Delete server";
                            lbl.BackColor = listBackcolor;
                            lbl.ForeColor = Color.IndianRed;
                            lbl.Font = new Font("Bahnschrift Light", 9, FontStyle.Regular);
                        }
                        else if (serverOptions[i].ToLower() == "- mods -")
                        {
                            lbl.Text = "  Mods";
                            lbl.BackColor = this.BackColor;
                            lbl.ForeColor = Color.DodgerBlue;
                            lbl.Font = new Font("Bahnschrift Light", 10, FontStyle.Regular);
                        }
                        else if (serverOptions[i].ToLower() == "- actions -")
                        {
                            lbl.Text = "  Actions";
                            lbl.BackColor = this.BackColor;
                            lbl.ForeColor = Color.IndianRed;
                            lbl.Font = new Font("Bahnschrift Light", 10, FontStyle.Regular);
                        }
                        else if (serverOptions[i].ToLower() == "- thirdparty -")
                        {
                            lbl.Text = "  Third Party Apps";
                            lbl.BackColor = this.BackColor;
                            lbl.ForeColor = Color.DarkSeaGreen;
                            lbl.Font = new Font("Bahnschrift Light", 10, FontStyle.Regular);
                        }
                        else if (serverOptions[i].ToLower().Contains("not detected"))
                        {
                            lbl.Text = serverOptions[i];
                            lbl.BackColor = listBackcolor;
                            lbl.ForeColor = Color.IndianRed;
                            lbl.Font = new Font("Bahnschrift Light", 9, FontStyle.Regular);
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
            if (label.Text != "" && label.Text != "  Mods" && label.Text != "  Actions" && label.Text != "  Third Party Apps")
            {
                label.BackColor = listHovercolor;
            }
        }

        private void lbl2_MouseLeave(object sender, EventArgs e)
        {
            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;
            if (label.Text != "" && label.Text != "  Mods" && label.Text != "  Actions" && label.Text != "  Third Party Apps")
            {
                label.BackColor = listBackcolor;
            }
        }

        private void lbl2_MouseDown(object sender, EventArgs e)
        {
            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;
            if (label.Text != "" && label.Text != "  Mods" && label.Text != "  Actions" && label.Text != "  Third Party Apps")
            {
                string currentDir = Directory.GetCurrentDirectory();
                label.BackColor = listSelectedcolor;

                switch (label.Text.ToLower())
                {
                    case "clear cache":
                        if (isLoneServer)
                        {
                            string cacheFolder = Path.Combine(Properties.Settings.Default.server_path, "user\\cache");
                            if (Directory.Exists(cacheFolder))
                            {
                                try
                                {
                                    Directory.Delete(cacheFolder, true);
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
                            selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                            string cacheFolder = Path.Combine(selectedServer, "user\\cache");

                            if (Directory.Exists(cacheFolder))
                            {
                                try
                                {
                                    Directory.Delete(cacheFolder, true);
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

                    case "run spt":

                        if (chkMinimizeOnRun.Checked)
                        {
                            WindowState = FormWindowState.Minimized;
                        }

                        bool isTrue = false;
                        if (isLoneServer)
                        {
                            Process[] processes = Process.GetProcesses();
                            foreach (Process p in processes)
                            {
                                if (p.ProcessName.ToLower() == "aki.server")
                                {
                                    string dir = Directory.GetParent(p.MainModule.FileName).FullName;
                                    if (Path.GetFileName(dir) == boxSelectedServerTitle.Text)
                                    {
                                        try
                                        {
                                            p.Kill();
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
                                        }
                                        catch (Exception err)
                                        {
                                            Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                            MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                        }
                                    }
                                }
                                else if (p.ProcessName.ToLower() == "escapefromtarkov")
                                {
                                    string dir = Directory.GetParent(p.MainModule.FileName).FullName;
                                    if (Path.GetFileName(dir) == boxSelectedServerTitle.Text)
                                    {
                                        try
                                        {
                                            p.Kill();
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
                        else
                        {
                            Process[] processes = Process.GetProcesses();
                            foreach (Process p in processes)
                            {
                                if (p.ProcessName.ToLower() == "aki.server")
                                {
                                    string dir = Directory.GetParent(p.MainModule.FileName).FullName;
                                    if (Path.GetFileName(dir) == boxSelectedServerTitle.Text)
                                    {
                                        try
                                        {
                                            p.Kill();
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
                                        }
                                        catch (Exception err)
                                        {
                                            Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                            MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                        }
                                    }
                                }
                                else if (p.ProcessName.ToLower() == "escapefromtarkov")
                                {
                                    string dir = Directory.GetParent(p.MainModule.FileName).FullName;
                                    if (Path.GetFileName(dir) == boxSelectedServerTitle.Text)
                                    {
                                        try
                                        {
                                            p.Kill();
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

                        if (isLoneServer)
                        {
                            string cacheFolder = Path.Combine(Properties.Settings.Default.server_path, "user\\cache");
                            if (Directory.Exists(cacheFolder))
                            {
                                try
                                {
                                    Directory.Delete(cacheFolder, true);
                                }
                                catch (Exception err)
                                {
                                    Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                }
                            }

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
                            }
                            catch (Exception err)
                            {
                                Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                            }
                            Directory.SetCurrentDirectory(currentDir);

                            int akiPort;
                            string portPath = Path.Combine(Properties.Settings.Default.server_path, "Aki_Data\\Server\\database\\server.json");
                            bool portExists = File.Exists(portPath);
                            if (portExists)
                            {
                                string readPort = File.ReadAllText(portPath);
                                JObject portObject = JObject.Parse(readPort);
                                akiPort = (int)portObject["port"];
                            }
                            else
                            {
                                akiPort = 6969;
                            }

                            int elapsed = 0;
                            int timeout = 240000;

                            System.Threading.Timer timer = null;
                            timer = new System.Threading.Timer(_ =>
                            {
                                if (elapsed >= timeout)
                                {
                                    timer.Dispose();
                                    showError("We could not detect the Aki Launcher after 20 seconds.\n" +
                                        "\n" +
                                        "Max duration reached, launching SPT-AKI.");

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
                                    using (var client = new TcpClient())
                                    {
                                        try
                                        {
                                            client.Connect("localhost", akiPort);
                                            timer.Dispose();
                                            elapsed = 0;

                                            runLauncher();
                                        }
                                        catch (SocketException)
                                        {
                                            elapsed += 1000;
                                        }
                                    }
                                }
                            }, null, 1000, 1000);
                        }
                        else
                        {
                            selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                            string cacheFolder = Path.Combine(selectedServer, "user\\cache");

                            if (Directory.Exists(cacheFolder))
                            {
                                try
                                {
                                    Directory.Delete(cacheFolder, true);
                                }
                                catch (Exception err)
                                {
                                    Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                }
                            }

                            // server
                            Directory.SetCurrentDirectory(selectedServer);
                            server = new Process();
                            server.StartInfo.WorkingDirectory = selectedServer;
                            server.StartInfo.FileName = "Aki.Server.exe";
                            server.StartInfo.CreateNoWindow = false;
                            server.StartInfo.UseShellExecute = false;
                            server.StartInfo.RedirectStandardOutput = false;
                            try
                            {
                                server.Start();
                            }
                            catch (Exception err)
                            {
                                Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                            }
                            Directory.SetCurrentDirectory(currentDir);

                            int akiPort;
                            selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                            string portPath = Path.Combine(selectedServer, "Aki_Data\\Server\\database\\server.json");
                            bool portExists = File.Exists(portPath);
                            if (portExists)
                            {
                                string readPort = File.ReadAllText(portPath);
                                JObject portObject = JObject.Parse(readPort);
                                akiPort = (int)portObject["port"];
                            }
                            else
                            {
                                akiPort = 6969;
                            }

                            int elapsed = 0;
                            int timeout = 180000;

                            System.Threading.Timer timer = null;
                            timer = new System.Threading.Timer(_ =>
                            {
                                if (elapsed >= timeout)
                                {
                                    showError("We could not detect the Aki Launcher after 20 seconds.\n" +
                                        "\n" +
                                        "Max duration reached, launching SPT-AKI.");

                                    // launcher
                                    Directory.SetCurrentDirectory(selectedServer);
                                    launcher = new Process();
                                    launcher.StartInfo.WorkingDirectory = selectedServer;
                                    launcher.StartInfo.FileName = "Aki.Launcher.exe";
                                    launcher.StartInfo.CreateNoWindow = false;
                                    launcher.StartInfo.UseShellExecute = false;
                                    launcher.StartInfo.RedirectStandardOutput = false;
                                    try
                                    {
                                        launcher.Start();
                                    }
                                    catch (Exception err)
                                    {
                                        Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                    }
                                    Directory.SetCurrentDirectory(currentDir);
                                    timer.Dispose();
                                }
                                else
                                {
                                    using (var client = new TcpClient())
                                    {
                                        try
                                        {
                                            client.Connect("localhost", akiPort);
                                            // launcher
                                            Directory.SetCurrentDirectory(selectedServer);
                                            launcher = new Process();
                                            launcher.StartInfo.WorkingDirectory = selectedServer;
                                            launcher.StartInfo.FileName = "Aki.Launcher.exe";
                                            launcher.StartInfo.CreateNoWindow = false;
                                            launcher.StartInfo.UseShellExecute = false;
                                            launcher.StartInfo.RedirectStandardOutput = false;
                                            try
                                            {
                                                launcher.Start();
                                            }
                                            catch (Exception err)
                                            {
                                                Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                            }
                                            Directory.SetCurrentDirectory(currentDir);
                                            timer.Dispose();
                                        }
                                        catch (SocketException)
                                        {
                                        }
                                    }
                                    elapsed += 1000;
                                }
                            }, null, 1000, 1000);
                        }
                        break;

                    case "open server mods":

                        if ((Control.MouseButtons & MouseButtons.Right) != 0)
                        {
                            label.Text = "Open server modlist";
                        }
                        else
                        {
                            if (isLoneServer)
                            {
                                string modsFolder = Path.Combine(Properties.Settings.Default.server_path, "user\\mods");
                                if (Directory.Exists(modsFolder))
                                {
                                    try
                                    {
                                        Process.Start("explorer.exe", modsFolder);
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
                                selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                                string modsFolder = Path.Combine(selectedServer, "user\\mods");

                                if (Directory.Exists(modsFolder))
                                {
                                    try
                                    {
                                        Process.Start("explorer.exe", modsFolder);
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

                    case "open server modlist":

                        if ((Control.MouseButtons & MouseButtons.Right) != 0)
                        {
                            label.Text = "Open server mods";
                        }
                        else
                        {
                            if (isLoneServer)
                            {
                                string modsFolder = Path.Combine(Properties.Settings.Default.server_path, "user\\mods");
                                if (Directory.Exists(modsFolder))
                                {
                                    try
                                    {
                                        Modlist form = new Modlist();

                                        Label boxPathPlaceholder = (Label)form.Controls["boxPathPlaceholder"];
                                        Label loneServer = (Label)form.Controls["loneServer"];
                                        Label watermark = (Label)form.Controls["watermark"];
                                        GroupBox boxModsType = (GroupBox)form.Controls["boxModsType"];

                                        boxPathPlaceholder.Text = modsFolder;
                                        boxModsType.Text = "Server mods";
                                        watermark.Visible = true;
                                        loneServer.Text = "True";

                                        form.Text = boxSelectedServerTitle.Text;
                                        form.ShowDialog();
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
                                selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                                string modsFolder = Path.Combine(selectedServer, "user\\mods");

                                if (Directory.Exists(modsFolder))
                                {
                                    try
                                    {
                                        Modlist form = new Modlist();

                                        Label boxPathPlaceholder = (Label)form.Controls["boxPathPlaceholder"];
                                        Label loneServer = (Label)form.Controls["loneServer"];
                                        GroupBox boxModsType = (GroupBox)form.Controls["boxModsType"];

                                        boxPathPlaceholder.Text = Path.Combine(selectedServer, "user\\mods");
                                        boxModsType.Text = "Server mods";
                                        loneServer.Text = "False";

                                        form.Text = boxSelectedServerTitle.Text;
                                        form.ShowDialog();
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

                    case "open modloader json":

                        if (isLoneServer)
                        {
                            string orderFile = Path.Combine(Properties.Settings.Default.server_path, "user\\mods\\order.json");
                            if (File.Exists(orderFile))
                            {
                                try
                                {
                                    Process.Start(orderFile);
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
                            selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                            string orderFile = Path.Combine(selectedServer, "user\\mods\\order.json");

                            if (File.Exists(orderFile))
                            {
                                try
                                {
                                    Process.Start(orderFile);
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

                        if ((Control.MouseButtons & MouseButtons.Right) != 0)
                        {
                            label.Text = "Open client modlist";
                        }
                        else
                        {
                            if (isLoneServer)
                            {
                                string pluginsFolder = Path.Combine(Properties.Settings.Default.server_path, "BepInEx\\plugins");
                                if (Directory.Exists(pluginsFolder))
                                {
                                    try
                                    {
                                        Process.Start("explorer.exe", pluginsFolder);
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
                                selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                                string pluginsFolder = Path.Combine(selectedServer, "BepInEx\\plugins");

                                if (Directory.Exists(pluginsFolder))
                                {
                                    try
                                    {
                                        Process.Start("explorer.exe", pluginsFolder);
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

                    case "open client modlist":

                        if ((Control.MouseButtons & MouseButtons.Right) != 0)
                        {
                            label.Text = "Open client mods";
                        }
                        else
                        {
                            if (isLoneServer)
                            {
                                string modsFolder = Path.Combine(Properties.Settings.Default.server_path, "BepInEx\\plugins");
                                if (Directory.Exists(modsFolder))
                                {
                                    try
                                    {
                                        Modlist form = new Modlist();

                                        Label boxPathPlaceholder = (Label)form.Controls["boxPathPlaceholder"];
                                        Label loneServer = (Label)form.Controls["loneServer"];
                                        Label watermark = (Label)form.Controls["watermark"];
                                        GroupBox boxModsType = (GroupBox)form.Controls["boxModsType"];

                                        boxPathPlaceholder.Text = modsFolder;
                                        boxModsType.Text = "Client mods";
                                        watermark.Visible = false;
                                        loneServer.Text = "True";

                                        form.Text = boxSelectedServerTitle.Text;
                                        form.ShowDialog();
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
                                selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                                string modsFolder = Path.Combine(selectedServer, "BepInEx\\plugins");

                                if (Directory.Exists(modsFolder))
                                {
                                    try
                                    {
                                        Modlist form = new Modlist();

                                        Label boxPathPlaceholder = (Label)form.Controls["boxPathPlaceholder"];
                                        Label loneServer = (Label)form.Controls["loneServer"];
                                        Label watermark = (Label)form.Controls["watermark"];
                                        GroupBox boxModsType = (GroupBox)form.Controls["boxModsType"];

                                        boxPathPlaceholder.Text = Path.Combine(selectedServer, "BepInEx\\plugins");
                                        boxModsType.Text = "Client mods";
                                        watermark.Visible = false;
                                        loneServer.Text = "false";

                                        form.Text = boxSelectedServerTitle.Text;
                                        form.ShowDialog();
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

                    case "open load order editor (loe)":

                        if (isLoneServer)
                        {
                            string LOEFile = Properties.Settings.Default.loe_path;
                            if (File.Exists(LOEFile))
                            {
                                try
                                {
                                    string currentDirectory = Directory.GetCurrentDirectory();
                                    string modsFolder = Path.Combine(Properties.Settings.Default.server_path, "user\\mods");
                                    Directory.SetCurrentDirectory(modsFolder);
                                    Process proc = new Process();

                                    proc.StartInfo.WorkingDirectory = modsFolder;
                                    proc.StartInfo.FileName = "Load Order Editor.exe";
                                    proc.StartInfo.CreateNoWindow = false;
                                    proc.StartInfo.UseShellExecute = false;
                                    proc.StartInfo.RedirectStandardOutput = false;
                                    proc.Start();

                                    Directory.SetCurrentDirectory(currentDirectory);
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
                            string LOEFile = Properties.Settings.Default.loe_path;
                            if (File.Exists(LOEFile))
                            {
                                try
                                {
                                    selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                                    string currentDirectory = Directory.GetCurrentDirectory();
                                    string modsFolder = Path.Combine(selectedServer, "user\\mods");
                                    Directory.SetCurrentDirectory(modsFolder);
                                    Process proc = new Process();

                                    proc.StartInfo.WorkingDirectory = modsFolder;
                                    proc.StartInfo.FileName = "Load Order Editor.exe";
                                    proc.StartInfo.CreateNoWindow = false;
                                    proc.StartInfo.UseShellExecute = false;
                                    proc.StartInfo.RedirectStandardOutput = false;
                                    proc.Start();

                                    Directory.SetCurrentDirectory(currentDirectory);
                                }
                                catch (Exception err)
                                {
                                    Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                }
                            }
                        }
                        break;

                    case "loe not detected - click to download":

                        try
                        {
                            if (MessageBox.Show($"LOE is not detected in this {boxSelectedServerTitle.Text}\'s mods folder.\n\nWould you like to download LOE from the workshop?", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                Process.Start("https://hub.sp-tarkov.com/files/file/1082-loe-load-order-editor/");
                            }
                        }
                        catch (Exception err)
                        {
                            Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                            MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                        }
                        break;

                    case "open profile editor":

                        if (Directory.Exists(Properties.Settings.Default.profile_editor_path) && File.Exists(Path.Combine(Properties.Settings.Default.profile_editor_path, "SPT-AKI Profile Editor.exe")))
                        {
                            try
                            {
                                string currentDirectory = Directory.GetCurrentDirectory();
                                Directory.SetCurrentDirectory(Properties.Settings.Default.profile_editor_path);
                                Process proc = new Process();

                                proc.StartInfo.WorkingDirectory = Properties.Settings.Default.profile_editor_path;
                                proc.StartInfo.FileName = "SPT-AKI Profile Editor.exe";
                                proc.StartInfo.CreateNoWindow = false;
                                proc.StartInfo.UseShellExecute = false;
                                proc.StartInfo.RedirectStandardOutput = false;
                                proc.Start();

                                Directory.SetCurrentDirectory(currentDirectory);
                            }
                            catch (Exception err)
                            {
                                Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                            }
                        }
                        break;

                    case "profile editor not detected - click to fix":

                        OpenFileDialog dialog = new OpenFileDialog();
                        dialog.Title = "Select path for SPT-AKI Profile Editor.exe";
                        dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                        if (dialog.ShowDialog() == DialogResult.OK)
                        {

                            if (Path.GetFileNameWithoutExtension(dialog.FileName).ToLower() == "spt-aki profile editor")
                            {
                                // actual SPT installation
                                string fullPath = Path.GetFullPath(dialog.FileName);
                                string parentDir = Path.GetDirectoryName(fullPath);
                                Properties.Settings.Default.profile_editor_path = parentDir;
                                Properties.Settings.Default.Save();

                                if (isLoneServer)
                                {
                                    checkThirdPartyApps(Properties.Settings.Default.server_path);
                                }
                                else
                                {
                                    selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                                    checkThirdPartyApps(selectedServer);
                                }
                            }
                        }
                        break;

                    case "svm not detected - click to download":

                        try
                        {
                            if (MessageBox.Show($"SVM is not detected in this {boxSelectedServerTitle.Text}\'s mods folder.\n\nWould you like to download SVM from the workshop?", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                Process.Start("https://hub.sp-tarkov.com/files/file/379-kmc-server-value-modifier/");
                            }
                        }
                        catch (Exception err)
                        {
                            Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                            MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                        }
                        break;

                    case "open server value modifier (svm)":

                        string SVMFile = Path.Combine(Properties.Settings.Default.svm_path, "GFVE.exe");
                        if (Directory.Exists(Properties.Settings.Default.svm_path) && File.Exists(SVMFile))
                        {
                            try
                            {
                                string currentDirectory = Directory.GetCurrentDirectory();
                                Directory.SetCurrentDirectory(Properties.Settings.Default.svm_path);
                                Process proc = new Process();

                                proc.StartInfo.WorkingDirectory = Properties.Settings.Default.svm_path;
                                proc.StartInfo.FileName = "GFVE.exe";
                                proc.StartInfo.CreateNoWindow = false;
                                proc.StartInfo.UseShellExecute = false;
                                proc.StartInfo.RedirectStandardOutput = false;
                                proc.Start();

                                Directory.SetCurrentDirectory(currentDirectory);
                            }
                            catch (Exception err)
                            {
                                Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                            }
                        }
                        break;

                    case "spt realism not detected - click to download":
                        try
                        {
                            if (MessageBox.Show($"SPT Realism is not detected in {boxSelectedServerTitle.Text}\'s mods folder.\n\nWould you like to download it from the workshop?", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                Process.Start("https://hub.sp-tarkov.com/files/file/606-spt-realism-mod/");
                            }
                        }
                        catch (Exception err)
                        {
                            Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                            MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                        }
                        // nothing lol
                        break;

                    case "open spt realism":

                        string RealismFile = Path.Combine(Properties.Settings.Default.realism_path, "RealismModConfig.exe");
                        if (Directory.Exists(Properties.Settings.Default.realism_path) && File.Exists(RealismFile))
                        {
                            try
                            {
                                string currentDirectory = Directory.GetCurrentDirectory();
                                Directory.SetCurrentDirectory(Properties.Settings.Default.realism_path);
                                Process proc = new Process();

                                proc.StartInfo.WorkingDirectory = Properties.Settings.Default.realism_path;
                                proc.StartInfo.FileName = "RealismModConfig.exe";
                                proc.StartInfo.CreateNoWindow = false;
                                proc.StartInfo.UseShellExecute = false;
                                proc.StartInfo.RedirectStandardOutput = false;
                                proc.Start();

                                Directory.SetCurrentDirectory(currentDirectory);
                            }
                            catch (Exception err)
                            {
                                Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
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
                                    string cacheFolder = Path.Combine(Properties.Settings.Default.server_path, "user\\cache");
                                    if (Directory.Exists(cacheFolder))
                                    {
                                        try
                                        {
                                            Directory.Delete(cacheFolder, true);
                                        }
                                        catch (Exception err)
                                        {
                                            Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                            MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                        }
                                    }

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
                                        else if (p.ProcessName.ToLower() == "escapefromtarkov")
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

                                    if (confirm == 3)
                                    {
                                        showError("Stopped SPT-AKI!\n" +
                                            "\n" +
                                            "Cache cleared!");
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
                            selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);

                            if (Directory.Exists(selectedServer))
                            {
                                string cacheFolder = Path.Combine(selectedServer, "user\\cache");
                                if (Directory.Exists(cacheFolder))
                                {
                                    try
                                    {
                                        Directory.Delete(cacheFolder, true);
                                    }
                                    catch (Exception err)
                                    {
                                        Debug.WriteLine($"ERROR: {err.Message.ToString()}");
                                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                                    }
                                }

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
                                        showError("Server and launcher stopped + cache cleared!");
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
                            selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                            if (Directory.Exists(selectedServer))
                            {
                                if (MessageBox.Show($"Do you wish to delete {boxSelectedServerTitle.Text}?\nThis action is irreversible!", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    try
                                    {
                                        Directory.Delete(selectedServer, true);
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

        public void runLauncher()
        {
            string launcherProcess = "Aki.Launcher";
            Process[] launchers = Process.GetProcessesByName(launcherProcess);

            string currentDir = Directory.GetCurrentDirectory();
            if (isLoneServer)
            {
                if (launchers.Length > 0)
                {
                }
                else
                {
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
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine($"ERROR: {err.ToString()}");
                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                    }
                    Directory.SetCurrentDirectory(currentDir);
                }
            }
            else
            {
                if (launchers.Length > 0)
                {
                }
                else
                {
                    selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                    Directory.SetCurrentDirectory(selectedServer);
                    launcher = new Process();
                    launcher.StartInfo.WorkingDirectory = selectedServer;
                    launcher.StartInfo.FileName = "Aki.Launcher.exe";
                    launcher.StartInfo.CreateNoWindow = false;
                    launcher.StartInfo.UseShellExecute = false;
                    launcher.StartInfo.RedirectStandardOutput = false;
                    try
                    {
                        launcher.Start();
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine($"ERROR: {err.ToString()}");
                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.Message.ToString()}", this.Text, MessageBoxButtons.OK);
                    }
                    Directory.SetCurrentDirectory(currentDir);
                }
            }
        }

        private void lbl2_MouseUp(object sender, EventArgs e)
        {
            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;
            if (label.Text != "" && label.Text != "  Mods" && label.Text != "  Actions" && label.Text != "  Third Party Apps")
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
                    boxPath.Text = fullPath;
                    Properties.Settings.Default.server_path = boxPath.Text;
                    Properties.Settings.Default.Save();

                    if (File.Exists(Path.Combine(fullPath, "Aki.Server.exe")) &&
                        File.Exists(Path.Combine(fullPath, "Aki.Launcher.exe")) &&
                        Directory.Exists(Path.Combine(fullPath, "Aki_Data")))
                    {
                        isLoneServer = true;
                    }
                    else
                    {
                        isLoneServer = false;
                    }

                    listAllServers(Properties.Settings.Default.server_path);
                }

            }
        }

        private void boxOpenIn_Click(object sender, EventArgs e)
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

        private void boxSelectedServerTitle_Click(object sender, EventArgs e)
        {
            if (boxSelectedServerTitle.Text.ToLower() != "spt placeholder" && boxSelectedServerTitle.Text.ToLower() != "no spt detected")
            {
                try
                {
                    selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                    Process.Start("explorer.exe", selectedServer);
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
                if (item.EndsWith(".zip") || item.EndsWith("7.z"))
                { 
                    MessageBox.Show("Please right click \"Open server mods\" and open the modlist before you try to add a mod!", this.Text, MessageBoxButtons.OK);
                }
                else
                {
                    try
                    {
                        if (File.Exists(Path.Combine(Path.GetFullPath(item), "Aki.Server.exe")) &&
                            File.Exists(Path.Combine(Path.GetFullPath(item), "Aki.Launcher.exe")) &&
                            Directory.Exists(Path.Combine(Path.GetFullPath(item), "Aki_Data")))
                        {
                            boxPath.Text = Path.GetFullPath(item);
                            Properties.Settings.Default.server_path = Path.GetFullPath(item);
                            Properties.Settings.Default.Save();

                            isLoneServer = true;
                            listAllServers(Properties.Settings.Default.server_path);
                        }
                        else
                        {
                            try
                            {
                                boxPath.Text = Path.GetFullPath(item);
                                Properties.Settings.Default.server_path = Path.GetFullPath(item);
                                Properties.Settings.Default.Save();

                                isLoneServer = false;
                                listAllServers(Properties.Settings.Default.server_path);
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
                            try
                            {
                                isLoneServer = true;
                                listAllServers(Properties.Settings.Default.server_path);
                                boxPathBox.Select();
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
                                isLoneServer = false;
                                listAllServers(Properties.Settings.Default.server_path);
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
                        selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                        Process.Start("explorer.exe", selectedServer);
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

        private void bResetThirdParty_MouseEnter(object sender, EventArgs e)
        {
            bResetThirdParty.ForeColor = Color.DodgerBlue;
        }

        private void bResetThirdParty_MouseLeave(object sender, EventArgs e)
        {
            bResetThirdParty.ForeColor = Color.LightGray;
        }

        private void bResetThirdParty_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Reset third party apps?", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Properties.Settings.Default.profile_editor_path = "";
                Properties.Settings.Default.svm_path = "";
                Properties.Settings.Default.Save();
                showError("Reset successful");
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.LControlKey | Keys.R))
            {
                e.SuppressKeyPress = true;
                e.Handled = true;

                listAllServers(Properties.Settings.Default.server_path);
                showError("Refreshed!");
            }
        }

        private void bRefresh_Click(object sender, EventArgs e)
        {
            if (boxPath.Text != "" || Directory.Exists(boxPath.Text))
            {
                listAllServers(Properties.Settings.Default.server_path);
            }
            else
            {
                showError("Please have a standalone installation, or gallery of SPT versions, selected. before you refresh.");
            }
        }

        private void bRefresh_MouseEnter(object sender, EventArgs e)
        {
            bRefresh.ForeColor = Color.DodgerBlue;
        }

        private void bRefresh_MouseLeave(object sender, EventArgs e)
        {
            bRefresh.ForeColor = Color.LightGray;
        }
    }
}
