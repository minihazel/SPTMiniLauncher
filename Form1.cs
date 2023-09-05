﻿using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Net.NetworkInformation;
using System.Security.Policy;

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
        public string thirdPartyFile;
        public string firstTime;
        public string core;
        public string selectedAID;
        public string[] thirdPartyContent = { };

        public Color listBackcolor = Color.FromArgb(255, 35, 35, 35);
        public Color listSelectedcolor = Color.FromArgb(255, 50, 50, 50);
        public Color listHovercolor = Color.FromArgb(255, 45, 45, 45);

        public Process server;
        public Process launcher;

        public outputWindow outputwindow;
        private List<string> globalProcesses;
        public Dictionary<string, ThirdPartyInfo> appDict { get; set; }
        public static Form1 Instance { get; private set; }

        // background working
        BackgroundWorker CheckServerWorker;
        public BackgroundWorker TarkovProcessDetector;
        public BackgroundWorker TarkovEndDetector;
        public BackgroundWorker globalProcessDetector;
        public StringBuilder akiServerOutputter;

        // Lists
        string[] serverOptionsStreets = {
            "- ACTIONS -",
            "Clear cache",
            "Launch SPT-AKI",
            "Stop SPT-AKI",
            "- MODS -",
            "Open server mods",
            "Open client mods",
            "Open profile -",
            "Open control panel",
            "Open modloader JSON",
            "- THIRDPARTY -"
        };

        public Form1()
        {
            InitializeComponent();
            appDict = new Dictionary<string, ThirdPartyInfo>();
            Instance = this;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!isLauncherRunning())
            {
                boxPath.Text = "";
                outputwindow = new outputWindow();
                outputwindow.Visible = false;
                outputwindow.Owner = this;

                settingsFile = System.IO.Path.Combine(Environment.CurrentDirectory, "SPT Mini.json");
                thirdPartyFile = System.IO.Path.Combine(Environment.CurrentDirectory, "Third Party Apps.json");
                // firstTime = System.IO.Path.Combine(Environment.CurrentDirectory, "firsttime");

                messageBoard form = new messageBoard();
                RichTextBox messageBox = (RichTextBox)form.Controls["messageBox"];
                Label messageTitle = (Label)form.Controls["messageTitle"];
                messageTitle.ForeColor = Color.LightGray;

                if (File.Exists(thirdPartyFile))
                {
                    appDict = new Dictionary<string, ThirdPartyInfo>();
                    string thirdPartyContent = System.IO.File.ReadAllText(thirdPartyFile);
                    JObject thirdParty = JObject.Parse(thirdPartyContent);
                    JArray appsArray = (JArray)thirdParty["ThirdPartyApps"];

                    foreach (JObject app in appsArray)
                    {
                        string name = (string)app["Name"];
                        string path = (string)app["Path"];

                        appDict[name] = new ThirdPartyInfo(name, path);
                    }
                }
                else
                {
                    var thirdpartyData = new JObject
                    {
                        ["ThirdPartyApps"] = new JArray
                        {
                            new JObject
                            {
                                ["Name"] = "Load Order Editor",
                                ["Path"] = "mods\\Load Order Editor.exe"
                            },
                            new JObject
                            {
                                ["Name"] = "SPT-AKI Profile Editor",
                                ["Path"] = "C:\\Program Files\\SPT-AKI Profile Editor\\SPT-AKI Profile Editor.exe"
                            },
                            new JObject
                            {
                                ["Name"] = "Server Value Modifier",
                                ["Path"] = "mods\\ServerValueModifier\\GFVE.exe"
                            },
                            new JObject
                            {
                                ["Name"] = "SPT Realism",
                                ["Path"] = "mods\\SPT-Realism-Mod\\RealismModConfig.exe"
                            }
                        }
                    };
                    string json = thirdpartyData.ToString();

                    try
                    {
                        File.WriteAllText(thirdPartyFile, json);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }

                    try
                    {
                        string thirdPartyContent = System.IO.File.ReadAllText(thirdPartyFile);
                        JObject thirdParty = JObject.Parse(thirdPartyContent);
                        JArray appsArray = (JArray)thirdParty["ThirdPartyApps"];

                        foreach (JObject app in appsArray)
                        {
                            string name = (string)app["Name"];
                            string path = (string)app["Path"];

                            appDict[name] = new ThirdPartyInfo(name, path);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                }

                if (File.Exists(settingsFile))
                {
                    globalProcesses = new List<string> { "Aki.Server", "Aki.Launcher", "EscapeFromTarkov" };
                    string readSettings = File.ReadAllText(settingsFile);
                    JObject settingsObject = JObject.Parse(readSettings);

                    if (settingsObject.ContainsKey("mainWidth") && settingsObject.ContainsKey("mainHeight"))
                    {
                        this.Width = (int)settingsObject["mainWidth"];
                        this.Height = (int)settingsObject["mainHeight"];
                    }
                    else
                    {
                        saveDimensions();
                    }

                    if (settingsObject["showFirstTimeMessage"].ToString().ToLower() == "true")
                    {
                        settingsObject.Property("showFirstTimeMessage").Value = "false";

                        messageTitle.Text = "First time setup";
                        messageBox.Text = Properties.Settings.Default.firstTimeMessage; /* File.ReadAllText(firstTime); */

                        form.Size = new Size(623, 730);
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
                                    outputwindow.isTrue = true;
                                    listAllServers(boxPath.Text);
                                }
                                else
                                {
                                    isLoneServer = false;
                                    outputwindow.isTrue = false;
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
                                outputwindow.isTrue = true;
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
                    form.Size = new Size(623, 200);
                    form.ShowDialog();
                }

                string logFolder = Path.Combine(Environment.CurrentDirectory, "logs");
                if (!Directory.Exists(logFolder))
                    Directory.CreateDirectory(logFolder);
            }
            else
            {
                MessageBox.Show("It appears that SPT Launcher is already running!\n\n\nWe\'ll close all of them and restart for you.", this.Text, MessageBoxButtons.OK);

                string sptLauncherProcess = "SPT Launcher";
                Process[] procs = Process.GetProcessesByName(sptLauncherProcess);
                if (procs != null && procs.Length > 1)
                {
                    foreach (Process launcher in procs)
                    {
                        if (!launcher.HasExited)
                        {
                            if (!launcher.CloseMainWindow())
                            {
                                launcher.Kill();
                                launcher.WaitForExit();
                            }
                            else
                            {
                                launcher.WaitForExit();
                            }
                        }
                    }

                    Application.Restart();
                }
            }
        }

        public bool isLauncherRunning()
        {
            string sptLauncherProcess = "SPT Launcher";
            Process[] sptLauncher = Process.GetProcessesByName(sptLauncherProcess);
            if (sptLauncher != null && sptLauncher.Length > 1)
            {
                return true;
            }
            return false;
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
                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
            }
        }

        public void showError(string content)
        {
            MessageBox.Show(content, this.Text, MessageBoxButtons.OK);
        }

        public void clearUI(bool all)
        {
            if (all)
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
                            Debug.WriteLine($"ERROR: {err.ToString()}");
                            MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
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
                            Debug.WriteLine($"ERROR: {err.ToString()}");
                            MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                        }
                    }
                }
            }
            else
            {
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
                            Debug.WriteLine($"ERROR: {err.ToString()}");
                            MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                        }
                    }
                }
            }
        }

        public void checkThirdPartyApps(string path)
        {

            if (appDict.Count > 0)
            {
                try
                {
                    thirdPartyContent = new string[] { };
                }
                catch (Exception err)
                {
                    Debug.WriteLine($"ERROR: {err}");
                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                }

                if (isLoneServer)
                {
                    string userFolder = Path.Combine(Properties.Settings.Default.server_path, "user");
                    string modsFolder = Path.Combine(userFolder, "mods");

                    foreach (var app in appDict)
                    {
                        string appName = app.Key;
                        ThirdPartyInfo appInfo = app.Value;

                        string _name = appInfo.Name;
                        string _path = appInfo.Path;

                        if (_path.ToLower().StartsWith("mods"))
                        {
                            string newPath = _path.ToLower().Replace("mods", modsFolder);
                            Array.Resize(ref thirdPartyContent, thirdPartyContent.Length + 1);
                            thirdPartyContent[thirdPartyContent.Length - 1] = _name;
                        }
                        else
                        {
                            Array.Resize(ref thirdPartyContent, thirdPartyContent.Length + 1);
                            thirdPartyContent[thirdPartyContent.Length - 1] = _name;
                        }
                    }

                    Array.Resize(ref thirdPartyContent, thirdPartyContent.Length + 1);
                    thirdPartyContent[thirdPartyContent.Length - 1] = "Add new tool";

                    Label lastItem = null;
                    foreach (Control ctrl in boxSelectedServer.Controls)
                    {
                        if (ctrl is Label lbl)
                        {
                            lastItem = lbl;
                        }
                    }

                    for (int i = 0; i < thirdPartyContent.Length; i++)
                    {
                        Label lbl = new Label();
                        lbl.AutoSize = false;
                        lbl.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
                        lbl.TextAlign = ContentAlignment.MiddleLeft;
                        lbl.Size = new Size(boxSelectedServer.Size.Width, boxSelectedServerPlaceholder.Size.Height);
                        lbl.Location = new Point(lastItem.Location.X, lastItem.Location.Y + 30 + (i * 30));
                        lbl.Cursor = Cursors.Hand;
                        lbl.Margin = new Padding(1, 1, 1, 1);
                        lbl.MouseEnter += new EventHandler(lbl2_MouseEnter);
                        lbl.MouseLeave += new EventHandler(lbl2_MouseLeave);
                        lbl.MouseDown += new MouseEventHandler(lbl2_MouseDown);
                        lbl.MouseUp += new MouseEventHandler(lbl2_MouseUp);

                        lbl.Name = $"thirdparty_{thirdPartyContent[i].ToLower()}";

                        if (thirdPartyContent[i].ToLower() == "add new tool")
                        {
                            lbl.Text = thirdPartyContent[i];
                        }
                        else
                        {
                            lbl.Text = $"Open {thirdPartyContent[i]}";
                        }

                        lbl.BackColor = listBackcolor;
                        lbl.ForeColor = Color.LightGray;
                        lbl.Font = new Font("Bahnschrift Light", 9, FontStyle.Regular);

                        boxSelectedServer.Controls.Add(lbl);
                    }
                }
                else
                {
                    selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                    string userFolder = Path.Combine(selectedServer, "user");
                    string modsFolder = Path.Combine(userFolder, "mods");

                    foreach (var app in appDict)
                    {
                        string appName = app.Key;
                        ThirdPartyInfo appInfo = app.Value;

                        string _name = appInfo.Name;
                        string _path = appInfo.Path;

                        if (_path.ToLower().StartsWith("mods"))
                        {
                            string newPath = _path.ToLower().Replace("mods", modsFolder);
                            Array.Resize(ref thirdPartyContent, thirdPartyContent.Length + 1);
                            thirdPartyContent[thirdPartyContent.Length - 1] = _name;
                        }
                        else
                        {
                            Array.Resize(ref thirdPartyContent, thirdPartyContent.Length + 1);
                            thirdPartyContent[thirdPartyContent.Length - 1] = _name;
                        }
                    }

                    Array.Resize(ref thirdPartyContent, thirdPartyContent.Length + 1);
                    thirdPartyContent[thirdPartyContent.Length - 1] = "Add new tool";

                    Label lastItem = null;
                    foreach (Control ctrl in boxSelectedServer.Controls)
                    {
                        if (ctrl is Label lbl)
                        {
                            lastItem = lbl;
                        }
                    }

                    for (int i = 0; i < thirdPartyContent.Length; i++)
                    {
                        Label lbl = new Label();
                        lbl.AutoSize = false;
                        lbl.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
                        lbl.TextAlign = ContentAlignment.MiddleLeft;
                        lbl.Size = new Size(boxSelectedServer.Size.Width, boxSelectedServerPlaceholder.Size.Height);
                        lbl.Location = new Point(lastItem.Location.X, lastItem.Location.Y + 30 + (i * 30));
                        lbl.Cursor = Cursors.Hand;
                        lbl.Margin = new Padding(1, 1, 1, 1);
                        lbl.MouseEnter += new EventHandler(lbl2_MouseEnter);
                        lbl.MouseLeave += new EventHandler(lbl2_MouseLeave);
                        lbl.MouseDown += new MouseEventHandler(lbl2_MouseDown);
                        lbl.MouseUp += new MouseEventHandler(lbl2_MouseUp);

                        lbl.Name = $"thirdparty_{thirdPartyContent[i].ToLower()}";

                        if (thirdPartyContent[i].ToLower() == "add new tool")
                        {
                            lbl.Text = thirdPartyContent[i];
                        }
                        else
                        {
                            lbl.Text = $"Open {thirdPartyContent[i]}";
                        }

                        lbl.BackColor = listBackcolor;
                        lbl.ForeColor = Color.LightGray;
                        lbl.Font = new Font("Bahnschrift Light", 9, FontStyle.Regular);

                        boxSelectedServer.Controls.Add(lbl);
                    }
                }
            }

            /*
            string loepath = Path.Combine(path, "user\\mods\\Load Order Editor.exe");

            if (File.Exists(loepath))
            {
                Properties.Settings.Default.loe_path = loepath;
                Properties.Settings.Default.Save();

                serverOptionsStreets[11] = "Open Load Order Editor (LOE)";
            }
            else
            {
                Properties.Settings.Default.loe_path = "";
                Properties.Settings.Default.Save();

                serverOptionsStreets[11] = "LOE not detected - click to download";
            }

            string progFiles = Environment.ExpandEnvironmentVariables("%ProgramW6432%");

            if (Directory.Exists(Path.Combine(progFiles, "SPT-AKI Profile Editor")) &&
                File.Exists(Path.Combine(progFiles, "SPT-AKI Profile Editor\\SPT-AKI Profile Editor.exe")))
            {
                Properties.Settings.Default.profile_editor_path = Path.Combine(progFiles, "SPT-AKI Profile Editor");
                Properties.Settings.Default.Save();

                serverOptionsStreets[12] = "Open Profile Editor";
                serverOptions[10] = "Open Profile Editor";
            }
            else if (Properties.Settings.Default.profile_editor_path != null || Properties.Settings.Default.profile_editor_path != "")
            {
                checkProfileEditor(Properties.Settings.Default.profile_editor_path);
            }
            else
            {
                Properties.Settings.Default.profile_editor_path = "";
                Properties.Settings.Default.Save();

                serverOptionsStreets[12] = "Profile Editor not detected - click to fix";
                serverOptions[10] = "Profile Editor not detected - click to fix";
            }

            string svmpath = Path.Combine(path, "user\\mods\\ServerValueModifier");

            if (Directory.Exists(svmpath) &&
                File.Exists(Path.Combine(svmpath, "GFVE.exe")))
            {
                Properties.Settings.Default.svm_path = svmpath;
                Properties.Settings.Default.Save();

                serverOptionsStreets[13] = "Open Server Value Modifier (SVM)";
                serverOptions[11] = "Open Server Value Modifier (SVM)";
            }
            else
            {
                Properties.Settings.Default.svm_path = "";
                Properties.Settings.Default.Save();

                serverOptionsStreets[13] = "SVM not detected - click to download";
                serverOptions[11] = "SVM not detected - click to download";
            }

            string realismpath = Path.Combine(path, "user\\mods\\SPT-Realism-Mod");

            if (Directory.Exists(realismpath) &&
                File.Exists(Path.Combine(realismpath, "RealismModConfig.exe")))
            {
                Properties.Settings.Default.realism_path = realismpath;
                Properties.Settings.Default.Save();

                serverOptionsStreets[14] = "Open SPT Realism";
                serverOptions[12] = "Open SPT Realism";
            }
            else
            {
                Properties.Settings.Default.realism_path = "";
                Properties.Settings.Default.Save();

                serverOptionsStreets[14] = "SPT Realism not detected - click to download";
                serverOptions[12] = "SPT Realism not detected - click to download";
            }*/
        }

        public void editThirdPartyApp(string appName, string newPath)
        {
            try
            {
                bool thirdPartyFileExists = File.Exists(thirdPartyFile);
                if (thirdPartyFileExists)
                {
                    string thirdPartyJSON = File.ReadAllText(thirdPartyFile);
                    JObject obj = JObject.Parse(thirdPartyJSON);

                    foreach (JObject item in (JArray)obj["ThirdPartyApps"])
                    {
                        if (item["Name"].ToString() == appName)
                        {
                            item["Path"] = newPath;
                            break;
                        }
                    }

                    string updatedContent = obj.ToString();
                    File.WriteAllText(thirdPartyFile, updatedContent);

                    listServerOptions(true);
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine($"ERROR: {err}");
                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
            }
        }

        public void runThirdPartyApp(string appName)
        {
            if (appDict.Count > 0)
            {
                if (isLoneServer)
                {
                    string userFolder = Path.Combine(Properties.Settings.Default.server_path, "user");
                    string modsFolder = Path.Combine(userFolder, "mods");

                    foreach (var app in appDict)
                    {
                        ThirdPartyInfo appInfo = app.Value;

                        string _name = appInfo.Name;
                        string _path = appInfo.Path;

                        if (_name == appName)
                        {
                            if (_path.ToLower().StartsWith("mods"))
                            {
                                string newPath = _path.Replace("mods", modsFolder);
                                bool newPathExists = File.Exists(newPath);

                                try
                                {
                                    if (newPathExists)
                                    {
                                        ProcessStartInfo newApp = new ProcessStartInfo();
                                        newApp.WorkingDirectory = Path.GetDirectoryName(newPath);
                                        newApp.FileName = Path.GetFileName(newPath);
                                        newApp.UseShellExecute = true;
                                        newApp.Verb = "open";

                                        Process.Start(newApp);
                                    }
                                    else
                                    {
                                        showError($"It appears that third party tool {_name} doesn't exist in path{Environment.NewLine}{Environment.NewLine}{newPath}" +
                                            $"{Environment.NewLine}{Environment.NewLine}" +
                                            $"Please download and install the tool to use it. Alternatively, remove it from Third Party Apps (right-click until you see Remove).");
                                    }
                                }
                                catch (Exception err)
                                {
                                    Debug.WriteLine($"ERROR: {err.ToString()}");
                                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                                }
                            }
                            else
                            {
                                bool _pathExists = File.Exists(_path);

                                try
                                {
                                    if (_pathExists)
                                    {
                                        Process.Start(_path);
                                    }
                                    else
                                    {
                                        showError($"It appears that third party tool {_name} doesn't exist in path{Environment.NewLine}{Environment.NewLine}{_path}" +
                                            $"{Environment.NewLine}{Environment.NewLine}" +
                                            $"Please download and install the content to use it. Alternatively, remove it from the list of third-party content.");
                                    }
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
                else
                {
                    selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                    string userFolder = Path.Combine(selectedServer, "user");
                    string modsFolder = Path.Combine(userFolder, "mods");

                    foreach (var app in appDict)
                    {
                        ThirdPartyInfo appInfo = app.Value;

                        string _name = appInfo.Name;
                        string _path = appInfo.Path;

                        if (_path.ToLower().StartsWith("mods"))
                        {
                            string newPath = _path.ToLower().Replace("mods", modsFolder);
                            bool newPathExists = File.Exists(newPath);

                            try
                            {
                                if (newPathExists)
                                {
                                    ProcessStartInfo newApp = new ProcessStartInfo();
                                    newApp.WorkingDirectory = Path.GetDirectoryName(newPath);
                                    newApp.FileName = Path.GetFileName(newPath);
                                    newApp.UseShellExecute = true;
                                    newApp.Verb = "open";

                                    Process.Start(newApp);
                                }
                                else
                                {
                                    showError($"It appears that third party tool {_name} doesn't exist in path{Environment.NewLine}{Environment.NewLine}{newPath}" +
                                        $"{Environment.NewLine}{Environment.NewLine}" +
                                        $"Please download and install the content to use it. Alternatively, remove it from the list of third-party content.");
                                }
                            }
                            catch (Exception err)
                            {
                                Debug.WriteLine($"ERROR: {err.ToString()}");
                                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                            }
                        }
                        else
                        {
                            bool _pathExists = File.Exists(_path);

                            try
                            {
                                if (_pathExists)
                                {
                                    Process.Start(_path);
                                }
                                else
                                {
                                    showError($"It appears that third party tool {_name} doesn't exist in path{Environment.NewLine}{Environment.NewLine}{_path}" +
                                        $"{Environment.NewLine}{Environment.NewLine}" +
                                        $"Please download and install the content to use it. Alternatively, remove it from the list of third-party content.");
                                }
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
        }

        public void removeThirdPartyApp(string appName)
        {
            if (appDict.ContainsKey(appName))
            {
                try
                {
                    appDict.Remove(appName);
                }
                catch (Exception err)
                {
                    Debug.WriteLine($"ERROR: {err.ToString()}");
                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                }
            }

            bool thirdPartyFileExists = File.Exists(thirdPartyFile);
            if (thirdPartyFileExists)
            {
                string thirdPartycontent = File.ReadAllText(thirdPartyFile);
                JObject obj = JObject.Parse(thirdPartycontent);

                JArray thirdPartyApps = (JArray)obj["ThirdPartyApps"];
                if (thirdPartyApps != null)
                {
                    JObject item = (JObject)thirdPartyApps.FirstOrDefault(appItem => appItem["Name"].ToString() == appName);
                    if (item != null)
                    {
                        thirdPartyApps.Remove(item); // Remove the item from the JSON file

                        string updatedContent = obj.ToString();
                        File.WriteAllText(thirdPartyFile, updatedContent); // Save the updated JSON content
                    }
                }
            }
            
            listServerOptions(true);
        }

        public void checkForSingularProfile(string path)
        {
            string mainFolder = path;
            string userFolder = Path.Combine(path, "user");
            bool userExists = Directory.Exists(userFolder);
            if (userExists)
            {
                string profilesFolder = Path.Combine(userFolder, "profiles");
                bool profilesExists = Directory.Exists(profilesFolder);
                if (profilesExists)
                {
                    string[] profiles = Directory.GetFiles(profilesFolder);
                    if (profiles.Length == 1)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(profiles[0]);
                        if (fileName != null)
                            Properties.Settings.Default.currentProfileAID = fileName;
                    }
                }
            }
        }

        public void saveDimensions()
        {
            int curWidth = this.Size.Width;
            int curHeight = this.Size.Height;

            bool settingsFileExists = File.Exists(settingsFile);
            if (settingsFileExists)
            {
                string readSettings = File.ReadAllText(settingsFile);
                JObject settingsObject = JObject.Parse(readSettings);

                if (!settingsObject.ContainsKey("mainWidth"))
                    settingsObject.Add("mainWidth", 695);

                if (!settingsObject.ContainsKey("mainHeight"))
                    settingsObject.Add("mainHeight", 690);

                settingsObject["mainWidth"] = curWidth;
                settingsObject["mainHeight"] = curHeight;

                string updatedJSON = settingsObject.ToString();
                File.WriteAllText(settingsFile, updatedJSON);
            }
        }

        public void listAllServers(string path)
        {
            clearUI(true);

            if (isLoneServer)
            {
                checkForSingularProfile(path);

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

                    if (Directory.Exists(serverModsFolder))
                    {
                        updateOrderJSON(serverModsFolder);
                    }
                }
                else
                {
                    showError($"SPT metadata could not be found for single installation. UI will be cleared, please search for another installation.\n\nExpected path: {core}");
                    clearUI(true);
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

                TarkovProcessDetector = new BackgroundWorker();
                TarkovProcessDetector.DoWork += TarkovProcessDetector_DoWork;
                TarkovProcessDetector.RunWorkerCompleted += TarkovProcessDetector_RunWorkerCompleted;
                TarkovProcessDetector.RunWorkerAsync();

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
                            Debug.WriteLine($"ERROR: {err.ToString()}");
                            MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
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
                            checkForSingularProfile(Properties.Settings.Default.server_path);
                        }
                    }
                    else
                    {
                        selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                        core = Path.Combine(selectedServer, "Aki_Data\\Server\\configs\\core.json");
                        if (File.Exists(core))
                        {
                            checkVersion(core);
                            checkForSingularProfile(selectedServer);
                        }
                    }
                }
                catch (Exception err)
                {
                    Debug.WriteLine($"ERROR: {err.ToString()}");
                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
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
                Debug.WriteLine($"ERROR: {err.ToString()}");
                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
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

        public void TarkovProcessDetector_DoWork(object sender, DoWorkEventArgs e)
        {
            string processName = "EscapeFromTarkov";
            while (true)
            {
                Process[] processes = Process.GetProcessesByName(processName);
                if (processes.Length > 0)
                {
                    if (Properties.Settings.Default.tarkovDetector)
                    {
                        TarkovEndDetector = new BackgroundWorker();
                        TarkovEndDetector.DoWork += TarkovEndDetector_DoWork;
                        TarkovEndDetector.RunWorkerCompleted += TarkovEndDetector_RunWorkerCompleted;
                        TarkovEndDetector.RunWorkerAsync();
                    }

                    if (TarkovProcessDetector != null)
                        TarkovProcessDetector.Dispose();

                    break;
                }

                int interval = Convert.ToInt32(Properties.Settings.Default.startDetector);
                System.Threading.Thread.Sleep(interval);
            }
        }

        public void TarkovProcessDetector_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (TarkovProcessDetector != null)
                TarkovProcessDetector.Dispose();
        }

        public void globalProcessDetector_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (globalProcessDetector != null)
                globalProcessDetector.Dispose();
        }

        public void globalProcessDetector_DoWork(object sender, DoWorkEventArgs e)
        {
            if (globalProcessDetector.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            while (!globalProcessDetector.CancellationPending)
            {
                string aki_server = globalProcesses[0];
                // string aki_launcher = globalProcesses[1];
                string eft = globalProcesses[2];

                bool isServerRunning = Process.GetProcesses().Any(p => p.ProcessName.Equals(aki_server, StringComparison.OrdinalIgnoreCase));
                // bool isLauncherRunning = Process.GetProcesses().Any(p => p.ProcessName.Equals(aki_launcher, StringComparison.OrdinalIgnoreCase));
                bool isEFTRunning = Process.GetProcesses().Any(p => p.ProcessName.Equals(eft, StringComparison.OrdinalIgnoreCase));

                if (Properties.Settings.Default.timedLauncherToggle)
                {
                    if (Properties.Settings.Default.tarkovDetector)
                    {
                        if (TarkovEndDetector == null && !isEFTRunning)
                        {
                            Control statusButton = findRun(true, "spt-aki is running");
                            if (statusButton != null)
                            {
                                statusButton.Invoke((MethodInvoker)(() => { statusButton.Text = "SPT-AKI is running, waiting for Escape From Tarkov"; }));
                            }

                            Control cacheBtn = findCache();
                            if (cacheBtn != null)
                            {
                                cacheBtn.Invoke((MethodInvoker)(() => { cacheBtn.Text = "Clear cache"; }));
                            }

                        }
                        /*
                        if (!isLauncherRunning)
                        {
                            Control statusButton = findRun(true, "spt-aki is running");
                            if (statusButton != null)
                            {
                                statusButton.Invoke((MethodInvoker)(() => { statusButton.Text = "AKI Server is running, waiting for launcher"; }));
                            }

                            Control cacheBtn = findCache();
                            if (cacheBtn != null)
                            {
                                cacheBtn.Invoke((MethodInvoker)(() => { cacheBtn.Text = "Click here to run the Aki Launcher"; }));
                            }
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            if (TarkovEndDetector == null && !isEFTRunning)
                            {
                                Control statusButton = findRun(true, "spt-aki is running");
                                if (statusButton != null)
                                {
                                    Task.Delay(2000);
                                    statusButton.Invoke((MethodInvoker)(() => { statusButton.Text = "SPT-AKI is running, waiting for Escape From Tarkov"; }));
                                }

                                Control cacheBtn = findCache();
                                if (cacheBtn != null)
                                {
                                    cacheBtn.Invoke((MethodInvoker)(() => { cacheBtn.Text = "Clear cache"; }));
                                }
                                Thread.Sleep(1000);
                            }
                        }
                        */
                    }
                }
                else
                {
                    if (!isServerRunning /* && !isLauncherRunning */)
                    {
                        OnAllProcessesTerminated();
                        globalProcessDetector.CancelAsync();
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
            }
        }

        private void OnAllProcessesTerminated()
        {
            generateLogFile(Path.Combine(Environment.CurrentDirectory, "logs"));
            clearOutput();

            if (TarkovProcessDetector != null)
                TarkovProcessDetector.Dispose();

            if (globalProcessDetector != null)
            {
                globalProcessDetector.Dispose();
            }

            Debug.WriteLine("Aki Server, Aki Launcher && EFT all terminated manually, committing to fallback");

            if (Properties.Settings.Default.hideOptions > 0)
            {
                flashLauncherWindow();
            }

            resetRunButton();
            if (Properties.Settings.Default.closeOnQuit)
                Application.Exit();
        }

        public void TarkovEndDetector_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Properties.Settings.Default.tarkovDetector)
            {
                string processName = "EscapeFromTarkov";
                while (true)
                {
                    Process[] processes = Process.GetProcessesByName(processName);
                    if (processes.Length == 0)
                    {
                        if (Properties.Settings.Default.hideOptions == 1
                            || Properties.Settings.Default.hideOptions == 2)
                        {
                            flashLauncherWindow();
                        }
                        killProcesses();
                        if (TarkovEndDetector != null)
                            TarkovEndDetector.Dispose();

                        break;
                    }

                    Control statusButton = findRun(true, "spt-aki is running");
                    if (statusButton != null)
                    {
                        statusButton.Invoke((MethodInvoker)(() => { statusButton.Text = "SPT-AKI is running!"; }));
                    }

                    int interval = Convert.ToInt32(Properties.Settings.Default.endDetector);
                    System.Threading.Thread.Sleep(interval);
                }
            }
        }

        public void TarkovEndDetector_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (TarkovEndDetector != null)
                TarkovEndDetector.Dispose();
        }

        public void flashLauncherWindow()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.Show();
                    this.WindowState = FormWindowState.Normal;
                });
            }
            else
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
        }

        public void minimizeLauncherWindow()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.WindowState = FormWindowState.Minimized;
                });
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        public void hideLauncherWindow()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.Hide();
                });
            }
            else
            {
                this.Hide();
            }
        }

        public void listServerOptions(bool isStreets)
        {
            try
            {
                clearUI(false);

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

                        if (serverOptionsStreets[i].ToLower() == "launch spt-aki")
                        {
                            lbl.Name = "launcherRunButton";
                            lbl.Text = "Launch SPT-AKI";
                            lbl.BackColor = listBackcolor;
                            lbl.ForeColor = Color.DodgerBlue;
                            lbl.Font = new Font("Bahnschrift Light", 9, FontStyle.Regular);
                        }
                        else if (serverOptionsStreets[i].ToLower() == "clear cache")
                        {
                            lbl.Name = "launcherClearCacheButton";
                            lbl.Text = "Clear cache";
                            lbl.BackColor = listBackcolor;
                            lbl.ForeColor = Color.LightGray;
                            lbl.Font = new Font("Bahnschrift Light", 9, FontStyle.Regular);
                        }
                        else if (serverOptionsStreets[i].ToLower() == "stop spt-aki")
                        {
                            lbl.Name = "launcherStopSPTIfRunning";
                            lbl.Text = "Stop SPT-AKI";
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
                        else if (serverOptionsStreets[i].ToLower().Contains("open profile -"))
                        {
                            if (isLoneServer)
                            {
                                string profilesFolder = Path.Combine(Properties.Settings.Default.server_path, "user\\profiles");
                                bool profilesFolderExists = Directory.Exists(profilesFolder);
                                if (profilesFolderExists)
                                {
                                    int _countProfiles = Directory.GetFiles(profilesFolder).Length;
                                    lbl.Text = $"Open a profile - {_countProfiles.ToString()} available";
                                    lbl.BackColor = listBackcolor;
                                    lbl.ForeColor = Color.LightGray;
                                    lbl.Font = new Font("Bahnschrift Light", 9, FontStyle.Regular);
                                }
                                else
                                {
                                    lbl.Text = $"Profiles folder unavailable";
                                    lbl.BackColor = listBackcolor;
                                    lbl.ForeColor = Color.IndianRed;
                                    lbl.Font = new Font("Bahnschrift Light", 9, FontStyle.Regular);
                                }
                            }
                            else
                            {
                                selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                                string profilesFolder = Path.Combine(selectedServer, "user\\profiles");
                                bool profilesFolderExists = Directory.Exists(profilesFolder);
                                if (profilesFolderExists)
                                {
                                    int _countProfiles = Directory.GetFiles(profilesFolder).Length;
                                    lbl.Text = $"Open a profile - {_countProfiles.ToString()} available";
                                    lbl.BackColor = listBackcolor;
                                    lbl.ForeColor = Color.LightGray;
                                    lbl.Font = new Font("Bahnschrift Light", 9, FontStyle.Regular);
                                }
                                else
                                {
                                    lbl.Text = $"Profiles folder unavailable";
                                    lbl.BackColor = listBackcolor;
                                    lbl.ForeColor = Color.IndianRed;
                                    lbl.Font = new Font("Bahnschrift Light", 9, FontStyle.Regular);
                                }
                            }
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
            catch (Exception err)
            {
                Debug.WriteLine($"ERROR: {err.ToString()}");
                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
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

        private async void lbl2_MouseDown(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;
            if (label.Text != "" && label.Text != "  Mods" && label.Text != "  Actions" && label.Text != "  Third Party Apps")
            {
                string currentDir = Directory.GetCurrentDirectory();
                label.BackColor = listSelectedcolor;

                if (label.Text.ToLower().Contains("open a profile -"))
                {
                    profileSelector frm = new profileSelector();
                    if (isLoneServer)
                    {
                        frm.isTrue = true;
                    }
                    else
                    {
                        frm.isTrue = false;
                        frm.selectedServer = selectedServer;
                        frm.boxSelectedServerTitle = boxSelectedServerTitle.Text;
                    }

                    frm.selector = "profile_open";
                    frm.ShowDialog();
                }
                else
                {
                    if (label.Text.ToLower() ==
                        "clear cache")
                    {
                        if (Properties.Settings.Default.displayConfirmationMessage)
                        {
                            if (MessageBox.Show("Clear cache?\n\nThis will make the Server load significantly slower next launch.", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                if (isLoneServer)
                                {
                                    clearServerCache(Path.Combine(Properties.Settings.Default.server_path, "user\\cache"));
                                }
                                else
                                {
                                    selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                                    clearServerCache(Path.Combine(selectedServer, "user\\cache"));
                                }
                            }
                        }
                        else
                        {
                            if (isLoneServer)
                            {
                                clearServerCache(Path.Combine(Properties.Settings.Default.server_path, "user\\cache"));
                            }
                            else
                            {
                                selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                                clearServerCache(Path.Combine(selectedServer, "user\\cache"));
                            }
                        }
                    }

                    else if (label.Text.ToLower() ==
                        "click here to run the aki launcher")
                    {
                        runLauncher();
                    }

                    else if (label.Text.ToLower() ==
                        "launch spt-aki")
                    {
                        if (Properties.Settings.Default.currentProfileAID != null && Properties.Settings.Default.currentProfileAID != "")
                        {
                            switch (Properties.Settings.Default.hideOptions)
                            {
                                case 1:
                                    minimizeLauncherWindow();
                                    break;
                            }

                            if (Properties.Settings.Default.clearCache == 1)
                            {
                                if (isLoneServer)
                                {
                                    clearServerCache(Path.Combine(Properties.Settings.Default.server_path, "user\\cache"));
                                }
                                else
                                {
                                    selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                                    clearServerCache(Path.Combine(selectedServer, "user\\cache"));
                                }
                            }

                            runServer();

                            label.Text = "Loading SPT, this may take a few";
                            label.Enabled = false;
                        }
                        else
                        {
                            showError("You don\'t have a profile selected. Please head into Options -> SPT-AKI Settings and select a profile, then try again!");
                        }
                    }

                    else if (label.Text.ToLower() ==
                        "stop spt-aki")
                    {
                        bool akiRunning = isAKIRunning();
                        if (!akiRunning)
                        {
                            Control stopButton = findRun(false, "stop spt-aki");
                            if (stopButton != null)
                            {
                                stopButton.Invoke((MethodInvoker)(() => {
                                    stopButton.Text = "SPT-AKI is not running!";
                                    stopButton.ForeColor = Color.IndianRed;
                                }));
                                await Task.Delay(750);
                                stopButton.Invoke((MethodInvoker)(() => {
                                    stopButton.Text = "Stop SPT-AKI";
                                    stopButton.ForeColor = Color.LightGray;
                                }));
                            }
                        }
                        else
                        {
                            if (Properties.Settings.Default.displayConfirmationMessage)
                            {
                                if (MessageBox.Show("Quit SPT?\n\n\nThis will close all SPT-AKI related processes.", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    killProcesses();
                                }
                            }
                            else
                            {
                                killProcesses();
                            }
                        }
                    }

                    else if (label.Text.ToLower() ==
                        "open control panel")
                    {
                        controlWindow wn = new controlWindow();
                        wn.isTrue = isLoneServer;
                        wn.selectedServer = boxSelectedServerTitle.Text;
                        wn.ShowDialog();
                    }

                    else if (label.Text.ToLower() ==
                        "open server mods")
                    {
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
                                        Debug.WriteLine($"ERROR: {err.ToString()}");
                                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
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
                                        Debug.WriteLine($"ERROR: {err.ToString()}");
                                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                                    }
                                }
                            }
                        }
                    }

                    else if (label.Text.ToLower() ==
                        "open server modlist")
                    {
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
                                        Debug.WriteLine($"ERROR: {err.ToString()}");
                                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
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
                                        Debug.WriteLine($"ERROR: {err.ToString()}");
                                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                                    }
                                }
                            }
                        }
                    }

                    else if (label.Text.ToLower() ==
                        "open client mods")
                    {
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
                                        Debug.WriteLine($"ERROR: {err.ToString()}");
                                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
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
                                        Debug.WriteLine($"ERROR: {err.ToString()}");
                                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                                    }
                                }
                            }
                        }
                    }

                    else if (label.Text.ToLower() ==
                        "open client modlist")
                    {
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
                                        Debug.WriteLine($"ERROR: {err.ToString()}");
                                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
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
                                        Debug.WriteLine($"ERROR: {err.ToString()}");
                                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                                    }
                                }
                            }
                        }
                    }

                    else if (label.Text.ToLower() ==
                        "open modloader json")
                    {
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
                                    Debug.WriteLine($"ERROR: {err.ToString()}");
                                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
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
                                    Debug.WriteLine($"ERROR: {err.ToString()}");
                                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                                }
                            }
                        }
                    }

                    else if (label.Text.ToLower() ==
                        "add new tool")
                    {
                        addThirdParty addwnd = new addThirdParty(this, false);
                        addwnd.Show();
                    }

                    // Third Party Apps section

                    else
                    {
                        if ((Control.MouseButtons & MouseButtons.Right) != 0)
                        {
                            if (label.Text.ToLower().StartsWith("open") &&
                            thirdPartyContent.Contains<string>(label.Text.Replace("Open ", "")))
                            {
                                string trimmedName = label.Text.Replace("Open ", "");

                                label.Text = $"Change {trimmedName}";
                            }

                            else if (label.Text.ToLower().StartsWith("change") &&
                                thirdPartyContent.Contains<string>(label.Text.Replace("Change ", "")))
                            {
                                string trimmedChange = label.Text.Replace("Change ", "");

                                label.Text = $"Remove {trimmedChange}";
                            }

                            else if (label.Text.ToLower().StartsWith("remove") &&
                                thirdPartyContent.Contains<string>(label.Text.Replace("Remove ", "")))
                            {
                                string trimmedRemove = label.Text.Replace("Remove ", "");

                                label.Text = $"Open {trimmedRemove}";
                            }


                        }
                        else
                        {
                            if (label.Text.ToLower().StartsWith("open") &&
                                thirdPartyContent.Contains<string>(label.Text.Replace("Open ", "")))
                            {
                                string trimmedName = label.Text.Replace("Open ", "");
                                runThirdPartyApp(trimmedName);
                            }

                            else if (label.Text.ToLower().StartsWith("change") &&
                                thirdPartyContent.Contains<string>(label.Text.Replace("Change ", "")))
                            {
                                string trimmedName = label.Text.Replace("Change ", "");

                                addThirdParty addwnd = new addThirdParty(this, true);
                                addwnd.txtCustomName.Text = trimmedName;
                                addwnd.txtPathToApp.Text = appDict[trimmedName].Path;
                                addwnd.Show();
                            }

                            else if (label.Text.ToLower().StartsWith("remove") &&
                                thirdPartyContent.Contains<string>(label.Text.Replace("Remove ", "")))
                            {
                                string trimmedName = label.Text.Replace("Remove ", "");

                                if (MessageBox.Show($"Do you wish to remove {trimmedName} from your Third Party Apps?" +
                                    $"{Environment.NewLine}" +
                                    $"{Environment.NewLine}" +
                                    $"Keep in mind that this will not remove it from your system.", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    removeThirdPartyApp(trimmedName);
                                }
                            }
                        }
                    }
                }
            }

            boxPathBox.Select();
        }

        public void clearServerCache(string path)
        {
            string cacheFolder = path;
            if (Directory.Exists(cacheFolder))
            {
                try
                {
                    Directory.Delete(cacheFolder, true);
                }
                catch (Exception err)
                {
                    Debug.WriteLine($"ERROR: {err.ToString()}");
                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                }
            }
        }

        public string GetLocalIPAddress()
        {
            string localIP = string.Empty;
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface nI in networkInterfaces)
            {
                if (nI.OperationalStatus == OperationalStatus.Up && nI.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    IPInterfaceProperties ipProperties = nI.GetIPProperties();
                    foreach (UnicastIPAddressInformation ipInfo in ipProperties.UnicastAddresses)
                    {
                        if (ipInfo.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            localIP = ipInfo.Address.ToString();
                            break;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(localIP))
                {
                    break;
                }
            }
            return localIP;
        }

        public void runServer()
        {
            killAKIProcesses();

            Task.Delay(300);

            string launcherProcess = "Aki.Server";
            Process[] launchers = Process.GetProcessesByName(launcherProcess);
            string currentDir = Directory.GetCurrentDirectory();

            if (Properties.Settings.Default.timedLauncherToggle)
            {
                if (isLoneServer)
                {
                    Directory.SetCurrentDirectory(Properties.Settings.Default.server_path);
                    Process akiServer = new Process();

                    akiServer.StartInfo.WorkingDirectory = Properties.Settings.Default.server_path;
                    akiServer.StartInfo.FileName = "Aki.Server.exe";
                    akiServer.StartInfo.CreateNoWindow = true;
                    akiServer.StartInfo.UseShellExecute = false;
                    akiServer.StartInfo.RedirectStandardOutput = true;
                    akiServer.StartInfo.StandardOutputEncoding = Encoding.UTF8;

                    akiServer.OutputDataReceived += akiServer_OutputDataReceived;
                    akiServer.Exited += akiServer_Exited;

                    try
                    {
                        if (akiServerOutputter != null)
                            akiServerOutputter.Clear();

                        akiServerOutputter = new StringBuilder();
                        akiServer.Start();
                        akiServer.BeginOutputReadLine();
                        checkWorker();

                        if (Properties.Settings.Default.serverOutputting)
                            outputwindow.Show();
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine($"ERROR: {err.ToString()}");
                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                    }
                    Directory.SetCurrentDirectory(currentDir);
                }
                else
                {
                    selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                    Directory.SetCurrentDirectory(selectedServer);
                    Process akiServer = new Process();

                    akiServer.StartInfo.WorkingDirectory = selectedServer;
                    akiServer.StartInfo.FileName = "Aki.Server.exe";
                    akiServer.StartInfo.CreateNoWindow = true;
                    akiServer.StartInfo.UseShellExecute = false;
                    akiServer.StartInfo.RedirectStandardOutput = true;
                    akiServer.StartInfo.StandardOutputEncoding = Encoding.UTF8;

                    akiServer.OutputDataReceived += akiServer_OutputDataReceived;
                    akiServer.Exited += akiServer_Exited;

                    try
                    {
                        if (akiServerOutputter != null)
                            akiServerOutputter.Clear();

                        akiServerOutputter = new StringBuilder();
                        akiServer.Start();
                        akiServer.BeginOutputReadLine();
                        checkWorker();

                        if (Properties.Settings.Default.serverOutputting)
                            outputwindow.Show();
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine($"ERROR: {err.ToString()}");
                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                    }
                    Directory.SetCurrentDirectory(currentDir);
                }
            }
            else
            {
                if (isLoneServer)
                {
                    Directory.SetCurrentDirectory(Properties.Settings.Default.server_path);
                    Process akiServer = new Process();

                    akiServer.StartInfo.WorkingDirectory = Properties.Settings.Default.server_path;
                    akiServer.StartInfo.FileName = "Aki.Server.exe";
                    akiServer.StartInfo.CreateNoWindow = false;
                    akiServer.StartInfo.UseShellExecute = false;
                    akiServer.StartInfo.RedirectStandardOutput = false;

                    try
                    {
                        akiServer.Start();
                        checkWorker();
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine($"ERROR: {err.ToString()}");
                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                    }
                    Directory.SetCurrentDirectory(currentDir);
                }
                else
                {
                    selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                    Directory.SetCurrentDirectory(selectedServer);
                    Process akiServer = new Process();

                    akiServer.StartInfo.WorkingDirectory = selectedServer;
                    akiServer.StartInfo.FileName = "Aki.Server.exe";
                    akiServer.StartInfo.CreateNoWindow = false;
                    akiServer.StartInfo.UseShellExecute = false;
                    akiServer.StartInfo.RedirectStandardOutput = false;

                    try
                    {
                        akiServer.Start();
                        checkWorker();
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine($"ERROR: {err.ToString()}");
                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                    }
                    Directory.SetCurrentDirectory(currentDir);
                }
            }
        }

        public void runLauncher()
        {
            string launcherProcess = "Aki.Launcher";
            Process[] launchers = Process.GetProcessesByName(launcherProcess);
            string currentDir = Directory.GetCurrentDirectory();
            int akiPort = 0;

            if (isLoneServer)
            {
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
                                JObject parsedJson = JObject.Parse(readJson);
                                akiPort = Convert.ToInt32(parsedJson["port"]);
                            }
                        }
                    }
                }
            }
            else
            {
                selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                string akiPath = selectedServer;
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
                                JObject parsedJson = JObject.Parse(readJson);
                                akiPort = Convert.ToInt32(parsedJson["port"]);
                            }
                        }
                    }
                }
            }

            Task.Delay(500);

            // Check if there are 2 or more instances of the Aki Launcher, and if so terminate the "leftovers"
            string akiLauncherProcess = "Aki.Launcher";
            try
            {
                Process[] launcherprocs = Process.GetProcessesByName(akiLauncherProcess);
                if (launcherprocs != null && launcherprocs.Length > 1)
                {
                    foreach (Process akilauncher in launcherprocs)
                    {
                        if (!akilauncher.HasExited)
                        {
                            if (!akilauncher.CloseMainWindow())
                            {
                                akilauncher.Kill();
                                akilauncher.WaitForExit();
                            }
                            else
                            {
                                akilauncher.WaitForExit();
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine($"TERMINATION FAILURE LEFTOVERS AKI LAUNCHER (IGNORE): {err.ToString()}");
            }

            Control cacheBtn = findCache();
            if (cacheBtn != null)
            {
                cacheBtn.Invoke((MethodInvoker)(() => { cacheBtn.Text = "Clear cache"; }));
            }

            switch (Properties.Settings.Default.bypassLauncher)
            {
                case true:
                    if (isLoneServer)
                    {
                        ProcessStartInfo _tarkov = new ProcessStartInfo();

                        string aid = Properties.Settings.Default.currentProfileAID;
                        Console.WriteLine(aid);
                        int index = aid.IndexOf("-");
                        if (index != -1)
                        {
                            aid = aid.Substring(index, aid.Length).Trim();
                            _tarkov.FileName = Path.Combine(Properties.Settings.Default.server_path, "EscapeFromTarkov.exe");
                            if (akiPort != 0)
                            {
                                _tarkov.Arguments = $"-token={aid} -config={{\"BackendUrl\":\"http://127.0.0.1:{akiPort}\",\"Version\":\"live\"}}";
                            }
                            else
                            {

                                _tarkov.Arguments = $"-token={aid} -config={{\"BackendUrl\":\"http://127.0.0.1:6969\",\"Version\":\"live\"}}";
                            }
                        }
                        else
                        {
                            _tarkov.FileName = Path.Combine(Properties.Settings.Default.server_path, "EscapeFromTarkov.exe");
                            if (akiPort != 0)
                            {
                                _tarkov.Arguments = $"-token={Properties.Settings.Default.currentProfileAID} -config={{\"BackendUrl\":\"http://127.0.0.1:{akiPort}\",\"Version\":\"live\"}}";
                            }
                            else
                            {

                                _tarkov.Arguments = $"-token={Properties.Settings.Default.currentProfileAID} -config={{\"BackendUrl\":\"http://127.0.0.1:6969\",\"Version\":\"live\"}}";
                            }
                        }

                        Process tarkovGame = new Process();
                        tarkovGame.StartInfo = _tarkov;
                        tarkovGame.Start();
                    }
                    else
                    {
                        selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);

                        ProcessStartInfo _tarkov = new ProcessStartInfo();

                        string aid = Properties.Settings.Default.currentProfileAID;
                        int index = aid.IndexOf("-");
                        if (index != -1)
                        {
                            aid = aid.Substring(index, aid.Length).Trim();
                            _tarkov.FileName = Path.Combine(selectedServer, "EscapeFromTarkov.exe");
                            if (akiPort != 0)
                            {
                                _tarkov.Arguments = $"-token={aid} -config={{\"BackendUrl\":\"http://127.0.0.1:{akiPort}\",\"Version\":\"live\"}}";
                            }
                            else
                            {

                                _tarkov.Arguments = $"-token={aid} -config={{\"BackendUrl\":\"http://127.0.0.1:6969\",\"Version\":\"live\"}}";
                            }
                        }
                        else
                        {
                            _tarkov.FileName = Path.Combine(selectedServer, "EscapeFromTarkov.exe");
                            if (akiPort != 0)
                            {
                                _tarkov.Arguments = $"-token={Properties.Settings.Default.currentProfileAID} -config={{\"BackendUrl\":\"http://127.0.0.1:{akiPort}\",\"Version\":\"live\"}}";
                            }
                            else
                            {

                                _tarkov.Arguments = $"-token={Properties.Settings.Default.currentProfileAID} -config={{\"BackendUrl\":\"http://127.0.0.1:6969\",\"Version\":\"live\"}}";
                            }
                        }

                        Process tarkovGame = new Process();
                        tarkovGame.StartInfo = _tarkov;
                        tarkovGame.Start();
                    }

                    TarkovEndDetector = new BackgroundWorker();
                    TarkovEndDetector.DoWork += TarkovEndDetector_DoWork;
                    TarkovEndDetector.RunWorkerCompleted += TarkovEndDetector_RunWorkerCompleted;
                    TarkovEndDetector.RunWorkerAsync();
                    break;

                case false:
                    if (isLoneServer)
                    {
                        Process akiLauncher = new Process();
                        akiLauncher.StartInfo.WorkingDirectory = Properties.Settings.Default.server_path;
                        akiLauncher.StartInfo.FileName = Path.Combine(Properties.Settings.Default.server_path, "Aki.Launcher.exe");
                        akiLauncher.StartInfo.CreateNoWindow = false;

                        try
                        {
                            akiLauncher.Start();
                        }
                        catch (Exception err)
                        {
                            Debug.WriteLine($"ERROR: {err}");
                            MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err}", this.Text, MessageBoxButtons.OK);
                        }

                    }
                    else
                    {
                        Process akiLauncher = new Process();
                        selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                        akiLauncher.StartInfo.WorkingDirectory = selectedServer;
                        akiLauncher.StartInfo.FileName = "Aki.Launcher.exe";
                        akiLauncher.StartInfo.FileName = Path.Combine(selectedServer, "Aki.Launcher.exe");
                        akiLauncher.StartInfo.CreateNoWindow = false;
                        akiLauncher.StartInfo.UseShellExecute = false;
                        akiLauncher.StartInfo.RedirectStandardOutput = false;

                        try
                        {
                            akiLauncher.Start();
                        }
                        catch (Exception err)
                        {
                            Debug.WriteLine($"ERROR: {err}");
                            MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                        }
                    
                    }

                    Directory.SetCurrentDirectory(currentDir);

                    TarkovProcessDetector = new BackgroundWorker();
                    TarkovProcessDetector.DoWork += TarkovProcessDetector_DoWork;
                    TarkovProcessDetector.RunWorkerCompleted += TarkovProcessDetector_RunWorkerCompleted;
                    TarkovProcessDetector.RunWorkerAsync();
                    break;
            }

            Task.Delay(5000);
        }

        public void akiServer_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (outputwindow != null && outputwindow.IsHandleCreated)
            {
                RichTextBox sptOutputWindow = outputwindow.sptOutputWindow;

                if (sptOutputWindow != null)
                {
                    string res = e.Data;
                    if (!string.IsNullOrEmpty(res))
                    {
                        res = Regex.Replace(res, @"\[[0-1];[0-9][a-z]|\[[0-9][0-9][a-z]|\[[0-9][a-z]|\[[0-9][A-Z]", String.Empty);
                    }

                    sptOutputWindow.Invoke((MethodInvoker)(() => { sptOutputWindow.AppendText($"{res}\n"); }));
                    outputwindow.Invoke((MethodInvoker)(() => { outputwindow.scrollTextForm(); }));

                    akiServerOutputter.AppendLine(res);
                }
            }

        }

        private void akiServer_Exited(object sender, EventArgs e)
        {
        }

        public void killLauncher()
        {
        }

        public bool isAKIRunning()
        {
            string akiServerProcess = "Aki.Server";
            string akiLauncherProcess = "Aki.Launcher";
            string eftProcess = "EscapeFromTarkov";
            bool akiServerTerminated = false;
            bool akiLauncherTerminated = false;
            bool eftTerminated = false;

            try
            {
                Process[] procs1 = Process.GetProcessesByName(akiServerProcess);
                if (procs1 != null && procs1.Length > 0)
                {
                }
                else
                {
                    akiServerTerminated = true;
                }

                Process[] procs2 = Process.GetProcessesByName(akiLauncherProcess);
                if (procs2 != null && procs2.Length > 0)
                {
                }
                else
                {
                    akiLauncherTerminated = true;
                }

                Process[] procs3 = Process.GetProcessesByName(eftProcess);
                if (procs3 != null && procs3.Length > 0)
                {
                }
                else
                {
                    eftTerminated = true;
                }

                Control attemptedButton = findRun(false, "attempting to exit");

                if (akiServerTerminated && akiLauncherTerminated && eftTerminated)
                {
                    return false;
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine($"TERMINATION FAILURE (IGNORE): {err.ToString()}");
            }
            return true;
        }

        public void killAKIProcesses()
        {
            Control stopButton = findRun(false, "stop spt-aki");

            if (stopButton != null)
            {
                stopButton.Invoke((MethodInvoker)(() => { stopButton.Enabled = false; }));
            }
            string akiServerProcess = "Aki.Server";
            string akiLauncherProcess = "Aki.Launcher";
            string eftProcess = "EscapeFromTarkov";
            
            try
            {
                Process[] procs = Process.GetProcessesByName(akiServerProcess);
                if (procs != null && procs.Length > 0)
                {
                    foreach (Process aki in procs)
                    {
                        if (!aki.HasExited)
                        {
                            if (!aki.CloseMainWindow())
                            {
                                aki.Kill();
                                aki.WaitForExit();
                            }
                            else
                            {
                                aki.WaitForExit();
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine($"TERMINATION FAILURE OF AKI SERVER (IGNORE): {err.ToString()}");
            }

            Task.Delay(200);

            try
            {
                Process[] procs = Process.GetProcessesByName(akiLauncherProcess);
                if (procs != null && procs.Length > 0)
                {
                    foreach (Process aki in procs)
                    {
                        if (!aki.HasExited)
                        {
                            if (!aki.CloseMainWindow())
                            {
                                aki.Kill();
                                aki.WaitForExit();
                            }
                            else
                            {
                                aki.WaitForExit();
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine($"TERMINATION FAILURE OF AKI LAUNCHER (IGNORE): {err.ToString()}");
            }

            Task.Delay(200);

            try
            {
                Process[] procs = Process.GetProcessesByName(eftProcess);
                if (procs != null && procs.Length > 0)
                {
                    foreach (Process aki in procs)
                    {
                        if (!aki.HasExited)
                        {
                            if (!aki.CloseMainWindow())
                            {
                                aki.Kill();
                                aki.WaitForExit();
                            }
                            else
                            {
                                aki.WaitForExit();
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine($"TERMINATION FAILURE OF AKI LAUNCHER (IGNORE): {err.ToString()}");
            }

            Task.Delay(200);

            if (stopButton != null)
            {
                stopButton.Invoke((MethodInvoker)(() => {
                    stopButton.Enabled = true;
                }));
            }
        }

        public async void redirectKill()
        {
            bool akiRunning = isAKIRunning();
            if (!akiRunning)
            {
                Control stopButton = findRun(false, "stop spt-aki");
                if (stopButton != null)
                {
                    stopButton.Invoke((MethodInvoker)(() => {
                        stopButton.Text = "SPT-AKI is not running!";
                        stopButton.ForeColor = Color.IndianRed;
                    }));
                    await Task.Delay(750);
                    stopButton.Invoke((MethodInvoker)(() => {
                        stopButton.Text = "Stop SPT-AKI";
                        stopButton.ForeColor = Color.LightGray;
                    }));
                }
            }
            else
            {
                killProcesses();
            }
        }

        public async void killProcesses()
        {
            Control stopButton = findRun(false, "stop spt-aki");
            bool akiRunning = isAKIRunning();

            if (!akiRunning)
            {
                stopButton.Invoke((MethodInvoker)(() => { stopButton.Text = "SPT-AKI is not running!"; }));
                await Task.Delay(1500);
                stopButton.Invoke((MethodInvoker)(() => { stopButton.Text = "Stop SPT-AKI"; }));
            }
            else
            {
                if (globalProcessDetector != null)
                {
                    globalProcessDetector.CancelAsync();
                    globalProcessDetector.Dispose();
                }

                string akiServerProcess = "Aki.Server";
                string akiLauncherProcess = "Aki.Launcher";
                string eftProcess = "EscapeFromTarkov";
                bool akiServerTerminated = false;
                bool akiLauncherTerminated = false;
                bool eftTerminated = false;

                if (isLoneServer)
                {
                    if (Directory.Exists(Properties.Settings.Default.server_path))
                    {
                        if (Properties.Settings.Default.clearCache == 2)
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
                                    Debug.WriteLine($"ERROR: {err.ToString()}");
                                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                                }
                            }
                        }
                    }
                }
                else
                {
                    selectedServer = Path.Combine(Properties.Settings.Default.server_path, boxSelectedServerTitle.Text);
                    if (Directory.Exists(selectedServer))
                    {
                        if (Properties.Settings.Default.clearCache == 2)
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
                                    Debug.WriteLine($"ERROR: {err.ToString()}");
                                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                                }
                            }
                        }
                    }
                }

                Control statusButton = findRun(true, "spt-aki is running!");
                if (statusButton != null)
                {
                    statusButton.Invoke((MethodInvoker)(() => { statusButton.Text = "Attempting to exit SPT-AKI"; }));
                    stopButton.Invoke((MethodInvoker)(() => { stopButton.Enabled = false; }));
                }
                else
                {
                    statusButton = findRun(true, "loading spt");
                    if (statusButton != null)
                    {
                        statusButton.Invoke((MethodInvoker)(() => { statusButton.Text = "Attempting to exit SPT-AKI"; }));
                        stopButton.Invoke((MethodInvoker)(() => { stopButton.Enabled = false; }));
                    }
                }

                try
                {
                    Process[] procs = Process.GetProcessesByName(akiServerProcess);
                    if (procs != null && procs.Length > 0)
                    {
                        foreach (Process aki in procs)
                        {
                            if (!aki.HasExited)
                            {
                                if (!aki.CloseMainWindow())
                                {
                                    aki.Kill();
                                    aki.WaitForExit();
                                }
                                else
                                {
                                    aki.WaitForExit();
                                }
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    Debug.WriteLine($"TERMINATION FAILURE OF AKI SERVER (IGNORE): {err.ToString()}");
                }

                await Task.Delay(200);

                try
                {
                    Process[] procs = Process.GetProcessesByName(akiLauncherProcess);
                    if (procs != null && procs.Length > 0)
                    {
                        foreach (Process aki in procs)
                        {
                            if (!aki.HasExited)
                            {
                                if (!aki.CloseMainWindow())
                                {
                                    aki.Kill();
                                    aki.WaitForExit();
                                }
                                else
                                {
                                    aki.WaitForExit();
                                }
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    Debug.WriteLine($"TERMINATION FAILURE OF AKI LAUNCHER (IGNORE): {err.ToString()}");
                }

                await Task.Delay(200);

                try
                {
                    Process[] procs = Process.GetProcessesByName(eftProcess);
                    if (procs != null && procs.Length > 0)
                    {
                        foreach (Process aki in procs)
                        {
                            if (!aki.HasExited)
                            {
                                if (!aki.CloseMainWindow())
                                {
                                    aki.Kill();
                                    aki.WaitForExit();
                                }
                                else
                                {
                                    aki.WaitForExit();
                                }
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    Debug.WriteLine($"TERMINATION FAILURE OF AKI LAUNCHER (IGNORE): {err.ToString()}");
                }

                await Task.Delay(500);

                try
                {
                    Process[] procs1 = Process.GetProcessesByName(akiServerProcess);
                    if (procs1 != null && procs1.Length > 0)
                    {
                    }
                    else
                    {
                        akiServerTerminated = true;
                    }

                    Process[] procs2 = Process.GetProcessesByName(akiLauncherProcess);
                    if (procs2 != null && procs2.Length > 0)
                    {
                    }
                    else
                    {
                        akiLauncherTerminated = true;
                    }

                    Process[] procs3 = Process.GetProcessesByName(eftProcess);
                    if (procs3 != null && procs3.Length > 0)
                    {
                    }
                    else
                    {
                        eftTerminated = true;
                    }

                    Control attemptedButton = findRun(false, "attempting to exit");

                    if (akiServerTerminated && akiLauncherTerminated && eftTerminated)
                    {
                        if (attemptedButton != null)
                        {
                            attemptedButton.Invoke((MethodInvoker)(() => { attemptedButton.Text = "SPT-AKI successfully exited, resetting"; }));
                            await Task.Delay(1000);
                            attemptedButton.Invoke((MethodInvoker)(() => { attemptedButton.Enabled = true; }));
                            attemptedButton.Invoke((MethodInvoker)(() => { attemptedButton.Text = "Launch SPT-AKI"; }));
                        }
                    }
                    else
                    {
                        if (attemptedButton != null)
                        {
                            attemptedButton.Invoke((MethodInvoker)(() => { attemptedButton.Text = "Termination failed; one or more instances did not exit"; }));
                            await Task.Delay(1000);
                            attemptedButton.Invoke((MethodInvoker)(() => { attemptedButton.Enabled = true; }));
                            attemptedButton.Invoke((MethodInvoker)(() => { attemptedButton.Text = "Launch SPT-AKI"; }));
                        }
                    }
                }
                catch (Exception err)
                {
                    Debug.WriteLine($"TERMINATION FAILURE (IGNORE): {err.ToString()}");
                }

                stopButton.Invoke((MethodInvoker)(() => { stopButton.Enabled = true; }));
                Control cacheBtn = findCache();
                if (cacheBtn != null)
                {
                    cacheBtn.Invoke((MethodInvoker)(() => { cacheBtn.Text = "Clear cache"; }));
                }

                /*
                Control deleteBtn = findDelete();
                if (deleteBtn != null)
                {
                    deleteBtn.Invoke((MethodInvoker)(() => { deleteBtn.Text = "Delete server"; }));
                    deleteBtn.Invoke((MethodInvoker)(() => { deleteBtn.ForeColor = Color.IndianRed; }));
                }
                */

                try
                {
                    if (globalProcessDetector != null)
                        globalProcessDetector.Dispose();

                    if (CheckServerWorker != null)
                        CheckServerWorker.Dispose();

                    if (TarkovEndDetector != null)
                        TarkovEndDetector.Dispose();

                    if (TarkovProcessDetector != null)
                        TarkovProcessDetector.Dispose();

                    generateLogFile(Path.Combine(Environment.CurrentDirectory, "logs"));
                    resetRunButton();
                    clearOutput();

                    if (Properties.Settings.Default.closeOnQuit)
                        Application.Exit();

                    statusButton.Invoke((MethodInvoker)(() => { statusButton.Enabled = true; }));
                }
                catch (Exception err)
                {
                    Debug.WriteLine($"DISPOSE FAILURE (IGNORE): {err.ToString()}");
                }
            }
            
        }

        public void checkWorker()
        {
            CheckServerWorker = new BackgroundWorker();
            if (CheckServerWorker != null)
                CheckServerWorker.Dispose();

            CheckServerWorker.WorkerSupportsCancellation = true;
            CheckServerWorker.WorkerReportsProgress = false;

            CheckServerWorker.DoWork += CheckServerWorker_DoWork;
            CheckServerWorker.RunWorkerCompleted += CheckServerWorker_RunWorkerCompleted;

            try
            {
                CheckServerWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
        }

        private void CheckServerWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (isLoneServer)
            {
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

                int port = akiPort; // the port to check
                int timeout = 300000; // the maximum time to wait for the port to open in milliseconds
                int delay = 1000; // the delay between port checks in milliseconds
                int elapsed = 0; // the time elapsed since starting to check the port

                while (!CheckPort(port))
                {
                    if (elapsed >= timeout)
                    {
                        // port was not opened within the timeout period, so cancel the operation
                        e.Cancel = true;
                        if (CheckServerWorker != null)
                            CheckServerWorker.Dispose();

                        showError("We could not detect the Aki Launcher after 5 minutes.\n" +
                                  "\n" +
                                  "Max duration reached, launching SPT-AKI.");

                        runLauncher();
                        return;
                    }

                    Thread.Sleep(delay); // wait before checking again
                    elapsed += delay;

                }
            }
            else
            {
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

                int port = akiPort;
                int timeout = 300000;
                int delay = 1000;
                int elapsed = 0;

                while (!CheckPort(port))
                {
                    if (elapsed >= timeout)
                    {
                        // port was not opened within the timeout period, so cancel the operation
                        e.Cancel = true;
                        if (CheckServerWorker != null)
                            CheckServerWorker.Dispose();

                        showError("We could not detect the Aki Launcher after 5 minutes.\n" +
                                  "\n" +
                                  "Max duration reached, launching SPT-AKI.");

                        runLauncher();
                        return;
                    }

                    Thread.Sleep(delay); // wait before checking again
                    elapsed += delay;

                }
            }
        }

        private void CheckServerWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                // port was not opened within the timeout period
                Debug.WriteLine("Port could not be opened within the specified time period.");
            }
            else if (e.Error != null)
            {
                // an error occurred while checking the port
                Debug.WriteLine("An error occurred while checking the port: " + e.Error.Message);
            }
        }

        private bool CheckPort(int port)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    if (CheckServerWorker != null)
                        CheckServerWorker.Dispose();

                    client.Connect("127.0.0.1" /* GetLocalIPAddress() */, Properties.Settings.Default.usePort);

                    runLauncher();
                    confirmLaunched();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GenerateLogName()
        {
            string baseFileName = "logfile_server_";
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            int counter = 0;

            string fileName = $"{baseFileName}{currentDate}.log";
            while (File.Exists(fileName))
            {
                counter++;
                fileName = $"{baseFileName}{currentDate}_{counter}.log";
            }

            return fileName;
        }

        public void generateLogFile(string path)
        {
            if (akiServerOutputter != null)
            {
                string logDir = Path.Combine(Environment.CurrentDirectory, "logs");
                if (!Directory.Exists(logDir))
                    Directory.CreateDirectory(logDir);

                string logFileName = GenerateLogName();
                File.WriteAllText(Path.Combine(path, logFileName), akiServerOutputter.ToString());

                if (Properties.Settings.Default.openLogOnQuit)
                    Process.Start(Path.Combine(path, logFileName));
            }
        }

        public void confirmLaunched()
        {
            if (Properties.Settings.Default.hideOptions == 2)
            {
                hideLauncherWindow();
            }

            globalProcessDetector = new BackgroundWorker();
            globalProcessDetector.WorkerSupportsCancellation = true;
            globalProcessDetector.DoWork += globalProcessDetector_DoWork;
            globalProcessDetector.RunWorkerCompleted += globalProcessDetector_RunWorkerCompleted;
            globalProcessDetector.RunWorkerAsync();
        }

        public void resetRunButton()
        {
            setRunText("Launch SPT-AKI");
        }

        public void clearOutput()
        {
            if (outputwindow != null && outputwindow.IsHandleCreated)
            {
                RichTextBox sptOutputWindow = outputwindow.sptOutputWindow;
                sptOutputWindow.Invoke((MethodInvoker)(() => { sptOutputWindow.Clear(); }));
                outputwindow.Invoke((MethodInvoker)(() => { outputwindow.modProblem = false; }));
                outputwindow.Invoke((MethodInvoker)(() => { outputwindow.Hide(); }));
            }
        }

        public Control findCache()
        {
            // search by name
            Control[] cacheButton = this.Controls.Find("launcherClearCacheButton", true);
            if (cacheButton != null)
            {
                Label cacheBtn = (Label)cacheButton[0];
                return cacheBtn;
            }
            return null;
        }

        /*
        public Control findDelete()
        {
            // search by name
            Control[] deleteButton = this.Controls.Find("launcherDeleteServerButton", true);
            if (deleteButton != null)
            {
                Label deleteBtn = (Label)deleteButton[0];
                return deleteBtn;
            }
            return null;
        }
        */

        public Control findRun(bool isName, string searchText)
        {
            if (!isName)
            {
                // search by text property
                foreach (Control component in boxSelectedServer.Controls)
                {
                    if (component is Label lbl && lbl.Text.ToLower().StartsWith(searchText))
                    {
                        return lbl;
                    }
                }
            }
            else
            {
                // search by name
                Control[] runButton = this.Controls.Find("launcherRunButton", true);
                if (runButton != null && runButton.Length > 0)
                {
                    try
                    {
                        Label runBtn = (Label)runButton[0];
                        return runBtn;
                    }
                    catch (Exception err)
                    {
                        Console.WriteLine(err);
                    }
                }
                else
                {
                    return this.Controls["launcherRunButton"];
                }
            }

            return null;
        }

        public void setRunText(string displayText)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(setRunText), displayText);
                return;
            }

            Control[] runButton = this.Controls.Find("launcherRunButton", true);
            if (runButton != null && runButton.Length > 0)
            {
                Label runBtn = (Label)runButton[0];
                runBtn.Text = displayText;
            }
            else
            {
                try
                {
                    this.Controls["launcherRunButton"].Text = displayText;
                }
                catch (Exception err)
                {
                    Debug.WriteLine($"FIND FAILURE: {err.ToString()}");
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
                Debug.WriteLine($"ERROR: {err.ToString()}");
                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
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
                    Debug.WriteLine($"ERROR: {err.ToString()}");
                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
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
                                Debug.WriteLine($"ERROR: {err.ToString()}");
                                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine($"ERROR: {err.ToString()}");
                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
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
                Debug.WriteLine($"ERROR: {err.ToString()}");
                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
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
                                Debug.WriteLine($"ERROR: {err.ToString()}");
                                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
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
                                Debug.WriteLine($"ERROR: {err.ToString()}");
                                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
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
                        Debug.WriteLine($"ERROR: {err.ToString()}");
                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
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
                        Debug.WriteLine($"ERROR: {err.ToString()}");
                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
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

        private void bOpenOptions_MouseEnter(object sender, EventArgs e)
        {
            bOpenOptions.ForeColor = Color.DodgerBlue;
        }

        private void bOpenOptions_MouseLeave(object sender, EventArgs e)
        {
            bOpenOptions.ForeColor = Color.LightGray;
        }

        private void bOpenOptions_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.server_path != null ||
                Properties.Settings.Default.server_path != "" && boxPath.Text != null || boxPath.Text != "")
            {
                if (boxSelectedServerTitle.Text != "SPT Placeholder")
                {
                    optionsWindow frm = new optionsWindow();
                    frm.selectedServer = boxSelectedServerTitle.Text;

                    Control[] found = frm.Controls.Find("bDeleteServer", true);
                    if (found.Length > 0)
                    {
                        Control bDeleteServer = found[0];
                        bDeleteServer.Text = $"Click to delete {boxSelectedServerTitle.Text}";
                    }

                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please browse for an SPT folder before adjusting settings.\n\n\nHit Browse, navigate to a folder that contains \"Aki.Server.exe\", and select it.", this.Text, MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Please browse for an SPT folder before adjusting settings.\n\n\nHit Browse, navigate to a folder that contains \"Aki.Server.exe\", and select it.", this.Text, MessageBoxButtons.OK);
            }
        }

        private void chkMinimizeOnRun_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveDimensions();
            Properties.Settings.Default.Save();
        }

        private void bToggleOutputWindow_Click(object sender, EventArgs e)
        {
        }

        private void bToggleOutputWindow_MouseEnter(object sender, EventArgs e)
        {
            bToggleOutputWindow.ForeColor = Color.DodgerBlue;
        }

        private void bToggleOutputWindow_MouseLeave(object sender, EventArgs e)
        {
            bToggleOutputWindow.ForeColor = Color.LightGray;
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            /*
            if (outputwindow != null && outputwindow.IsHandleCreated)
            {
                Label detach = (Label)outputwindow.Controls["bDetach"];
                if (detach != null)
                {
                    if (detach.Text.ToLower().Contains("stickied"))
                    {
                        outputwindow.Location = new Point(this.Location.X + this.Width, this.Location.Y);
                        this.Size = new Size(outputwindow.Size.Width, this.Size.Height);
                    }
                }
            }
            */
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            /*
            if (outputwindow != null && !outputwindow.IsDisposed)
            {
                Label detach = (Label)outputwindow.Controls["bDetach"];
                if (detach != null)
                {
                    if (detach.Text.ToLower().Contains("stickied"))
                    {
                        outputwindow.Location = new Point(this.Location.X + this.Width, this.Location.Y);
                        outputwindow.Size = new Size(outputwindow.Size.Width, this.Size.Height);
                    }
                }

                outputwindow.SetBounds(this.Right, this.Top, outputwindow.Width, this.Height);
            }
            */
        }

        private void bToggleOutputWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (outputwindow != null)
            {
                if (!outputwindow.Visible)
                {
                    outputwindow.Show();
                }
                else
                {
                    outputwindow.Hide();
                }
            }
        }
    }

    public class ThirdPartyInfo
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public ThirdPartyInfo(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}

/*
profileSelector frm = new profileSelector();
if (isLoneServer)
{
    frm.isTrue = true;
}
else
{
    frm.isTrue = false;
    frm.selectedServer = selectedServer;
    frm.boxSelectedServerTitle = boxSelectedServerTitle.Text;
}

frm.selector = "run_spt";
frm.ShowDialog();
*/