using Microsoft.WindowsAPICodePack.Dialogs;
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
using System.Security.Permissions;
using System.Runtime.InteropServices.ComTypes;

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
        public string galleryFile;
        public string firstTime;
        public string core;
        public string selectedAID;
        public string[] thirdPartyContent = { };
        public string[] sptGallery = { };
        public DateTime startTimeTarkov;
        public DateTime startTimeServer;

        public Color listBackcolor = Color.FromArgb(255, 35, 35, 35);
        public Color listSelectedcolor = Color.FromArgb(255, 50, 50, 50);
        public Color listHovercolor = Color.FromArgb(255, 45, 45, 45);

        public Process server;
        public Process launcher;

        public outputWindow outputwindow;
        private List<string> globalProcesses;
        public Dictionary<string, ThirdPartyInfo> appDict { get; set; }
        public Dictionary<string, Gallery> galleryDictionary { get; set; }
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
            "View installed mods",
            "Open server mods",
            "Open client mods",
            "Open modloader JSON",
            "- MISCELLANEOUS -",
            "Open profile -",
            "Open control panel",
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
                galleryFile = System.IO.Path.Combine(Environment.CurrentDirectory, "Gallery.json");
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
                        string type = (string)app["Type"];

                        appDict[name] = new ThirdPartyInfo(name, path, type);
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
                                ["Path"] = "mods\\Load Order Editor.exe",
                                ["Type"] = "App"
                            },
                            new JObject
                            {
                                ["Name"] = "SPT-AKI Profile Editor",
                                ["Path"] = "C:\\Program Files\\SPT-AKI Profile Editor\\SPT-AKI Profile Editor.exe",
                                ["Type"] = "App"
                            },
                            new JObject
                            {
                                ["Name"] = "Server Value Modifier",
                                ["Path"] = "mods\\ServerValueModifier\\GFVE.exe",
                                ["Type"] = "App"
                            },
                            new JObject
                            {
                                ["Name"] = "SPT Realism",
                                ["Path"] = "mods\\SPT-Realism-Mod\\RealismModConfig.exe",
                                ["Type"] = "App"
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
                            string type = (string)app["Type"];

                            appDict[name] = new ThirdPartyInfo(name, path, type);
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

                    if (settingsObject["Developer_Options"] != null)
                    {
                        if (settingsObject["Developer_Options"]["Simple_Mode"] == null)
                        {
                            settingsObject["Developer_Options"]["Simple_Mode"] = false;
                        }
                    }
                    else
                    {
                        settingsObject["Developer_Options"] = new JObject();
                        settingsObject["Developer_Options"]["Simple_Mode"] = false;

                        string updatedJSON = settingsObject.ToString();
                        File.WriteAllText(settingsFile, updatedJSON);
                    }

                    if (settingsObject["timeOptions"] != null)
                    {
                        if (settingsObject["timeOptions"]["serverTime"] == null)
                            settingsObject["timeOptions"]["serverTime"] = 0;

                        if (settingsObject["timeOptions"]["tarkovTime"] == null)
                            settingsObject["timeOptions"]["tarkovTime"] = 0;
                    }
                    else
                    {
                        settingsObject["timeOptions"] = new JObject();
                        settingsObject["timeOptions"]["serverTime"] = 0;
                        settingsObject["timeOptions"]["tarkovTime"] = 0;

                        string updatedJSON = settingsObject.ToString();
                        File.WriteAllText(settingsFile, updatedJSON);
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

                    boxPathBox.Select();
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

                readGallery();
                if (Properties.Settings.Default.currentProfileAID != null)
                {
                    string convertedProfile = fetchProfileFromAID(Properties.Settings.Default.currentProfileAID);
                    if (convertedProfile != null &&
                        convertedProfile != Properties.Settings.Default.currentProfileAID)
                    {
                        if (convertedProfile.Length > 1)
                        {
                            if (convertedProfile != null)
                            {
                                bProfilePlaceholder.Text = $"Profile \'{convertedProfile}' selected";
                            }
                        }
                    }
                }
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

        public void readGallery()
        {
            clearUI(true);
            try
            {
                // instantiate internal array for storing installs and the "add new" button
                sptGallery = new string[] { };
                arrInsert(ref sptGallery, "Add new SPT-AKI install");
            }
            catch (Exception err)
            {
                Debug.WriteLine($"ERROR: {err}");
                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
            }

            Label lastItem = null;
            foreach (Control ctrl in boxServers.Controls)
            {
                if (ctrl is Label lbl)
                {
                    lastItem = lbl;
                }
            }

            bool galleryFileExists = File.Exists(galleryFile);
            if (galleryFileExists)
            {
                galleryDictionary = new Dictionary<string, Gallery>();
                string sptGallery = System.IO.File.ReadAllText(galleryFile);
                JObject galleryObj = JObject.Parse(sptGallery);
                JArray galleryArray = (JArray)galleryObj["Gallery"];

                foreach (JObject folder in galleryArray)
                {
                    string name = (string)folder["Name"];
                    string path = (string)folder["Path"];

                    galleryDictionary[name] = new Gallery(name, path);
                }
            }
            else
            {
                var galleryData = new JObject
                {
                    ["Gallery"] = new JArray { }
                };
                string json = galleryData.ToString();

                try
                {
                    File.WriteAllText(galleryFile, json);

                    galleryDictionary = new Dictionary<string, Gallery>();
                    string sptGallery = System.IO.File.ReadAllText(galleryFile);
                    JObject galleryObj = JObject.Parse(sptGallery);
                    JArray galleryArray = (JArray)galleryObj["Gallery"];

                    foreach (JObject folder in galleryArray)
                    {
                        string name = (string)folder["Name"];
                        string path = (string)folder["Path"];

                        galleryDictionary[name] = new Gallery(name, path);
                    }
                }
                catch (Exception err)
                {
                    Debug.WriteLine($"ERROR: {err}");
                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                }
            }

            if (galleryDictionary != null)
            {
                if (galleryDictionary.Count > 0)
                {
                    foreach (var folder in galleryDictionary)
                    {
                        clearUI(true);
                        string name = folder.Key;
                        Gallery folderInfo = folder.Value;

                        string installName = folderInfo.Name;
                        string installPath = folderInfo.Path;

                        arrInsert(ref sptGallery, name);
                    }
                }
            }

            for (int i = 0; i < sptGallery.Length; i++)
            {
                Label lbl = new Label();
                lbl.AutoSize = false;
                lbl.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
                lbl.TextAlign = ContentAlignment.MiddleLeft;
                lbl.Size = new Size(boxServers.Size.Width, boxServerPlaceholder.Size.Height);
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

                if (sptGallery[i].ToLower() == "add new spt-aki install")
                {
                    lbl.Name = $"gallery_addBtn";
                }
                else
                {
                    lbl.Name = $"install_{sptGallery[i]}";
                }

                lbl.Text = sptGallery[i];
                boxServers.Controls.Add(lbl);

                selectCurrentInstall();
            }
        }

        public void selectCurrentInstall()
        {
            string fullPath = Properties.Settings.Default.server_path;
            string fullName = Properties.Settings.Default.lastUsedInstall;

            if (fullPath != null && fullName != null)
            {
                foreach (Control ctrl in boxServers.Controls)
                {
                    if (ctrl is Label lbl && lbl.Text == fullName)
                    {
                        useInstall(fullName);
                        boxPathBox.Select();
                    }
                }
            }
        }

        public string fetchProfileFromAID(string profileAID)
        {
            string userFolder = Path.Combine(Properties.Settings.Default.server_path, "user");
            bool userFolderExists = Directory.Exists(userFolder);
            if (userFolderExists)
            {
                string profilesFolder = Path.Combine(userFolder, "profiles");
                bool profilesFolderExists = Directory.Exists(profilesFolder);
                if (profilesFolderExists)
                {
                    string fullAID = Path.Combine(profilesFolder, $"{profileAID}.json");
                    bool fullAIDExists = File.Exists(fullAID);
                    if (fullAIDExists)
                    {
                        string fileContent = File.ReadAllText(fullAID);
                        JObject parsedFile = JObject.Parse(fileContent);
                        JObject info = (JObject)parsedFile["info"];
                        string infoAID = (string)info["id"];

                        JObject characters = (JObject)parsedFile["characters"];
                        JObject pmc = (JObject)characters["pmc"];
                        JObject Info = (JObject)pmc["Info"];

                        string Nickname = (string)Info["Nickname"];

                        if (infoAID == profileAID)
                        {
                            return Nickname;
                        }
                    }
                }
            }
            return null;
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
                        if (boxServers.Controls.Count > 0)
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
                }

                // option box
                for (int i = boxSelectedServer.Controls.Count - 1; i >= 0; i--)
                {

                    Label selected = boxSelectedServer.Controls[i] as Label;
                    if ((selected != null) && (selected.Name.ToLower() != "boxselectedservertitle"))
                    {
                        if (boxSelectedServer.Controls.Count > 0)
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

        public static void arrInsert(ref string[] array, string item)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = item;
        }

        public static void arrRemove(ref string[] array, string item)
        {
            int index = Array.IndexOf(array, item);

            if (index != -1)
            {
                for (int i = index; i < array.Length - 1; i++)
                {
                    array[i] = array[i + 1];
                }

                Array.Resize(ref array, array.Length - 1);
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

                Array.Resize(ref thirdPartyContent, thirdPartyContent.Length + 1);
                thirdPartyContent[thirdPartyContent.Length - 1] = "Add new tool";

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

        public void editThirdPartyApp(string appName, string newPath, string type)
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
                            item["Type"] = type;
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
                string userFolder = Path.Combine(Properties.Settings.Default.server_path, "user");
                string modsFolder = Path.Combine(userFolder, "mods");

                foreach (var app in appDict)
                {
                    ThirdPartyInfo appInfo = app.Value;

                    string _name = appInfo.Name;
                    string _path = appInfo.Path;
                    string _type = appInfo.Type;

                    if (_name == appName)
                    {
                        if (_path.ToLower().StartsWith("mods"))
                        {
                            string newPath = _path.Replace("mods", modsFolder);
                            bool newPathExists;

                            if (_type.ToLower() == "folder")
                            {
                                newPathExists = Directory.Exists(newPath);
                            }
                            else
                            {
                                newPathExists = File.Exists(newPath);
                            }

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
                            bool _pathExists;

                            if (_type.ToLower() == "folder")
                            {
                                _pathExists = Directory.Exists(_path);
                            }
                            else
                            {
                                _pathExists = File.Exists(_path);
                            }

                            try
                            {
                                if (_pathExists)
                                {
                                    ProcessStartInfo newApp = new ProcessStartInfo();
                                    newApp.WorkingDirectory = Path.GetDirectoryName(_path);
                                    newApp.FileName = Path.GetFileName(_path);
                                    newApp.UseShellExecute = true;
                                    newApp.Verb = "open";

                                    Process.Start(newApp);
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
            string userFolder = Path.Combine(mainFolder, "user");
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

        public static Gallery fetchInstallByPath(string galleryFile, string installPath)
        {
            try
            {
                string galleryContent = File.ReadAllText(galleryFile);
                JObject galleryObj = JObject.Parse(galleryContent);

                if (galleryObj.ContainsKey("Gallery"))
                {
                    JArray galleryArray = (JArray)galleryObj["Gallery"];
                    foreach (JObject folder in galleryArray)
                    {
                        string installName = folder.Value<string>("Path");
                        if (installName == installPath)
                        {
                            string path = folder.Value<string>("Name");
                            return new Gallery(installPath, path);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine($"ERROR: {err}");
                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", "SPT Mini Launcher", MessageBoxButtons.OK);
            }

            return null;
        }

        public static Gallery fetchInstall(string galleryFile, string name)
        {
            try
            {
                string galleryContent = File.ReadAllText(galleryFile);
                JObject galleryObj = JObject.Parse(galleryContent);

                if (galleryObj.ContainsKey("Gallery"))
                {
                    JArray galleryArray = (JArray)galleryObj["Gallery"];
                    foreach (JObject folder in galleryArray)
                    {
                        string installName = folder.Value<string>("Name");
                        if (installName == name)
                        {
                            string path = folder.Value<string>("Path");
                            return new Gallery(name, path);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine($"ERROR: {err}");
                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", "SPT Mini Launcher", MessageBoxButtons.OK);
            }

            return null;
        }

        public void browseInstallation()
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
                        boxPath.Text = fullPath;
                        Properties.Settings.Default.server_path = boxPath.Text;
                        Properties.Settings.Default.Save();
                        addGalleryInstall(Properties.Settings.Default.server_path);
                    }
                }
            }
        }

        public void removeItemFromGalleryJSON(string fullName)
        {
            bool galleryFileExists = File.Exists(galleryFile);
            if (galleryFileExists)
            {
                JObject galleryObj = loadGalleryObj();

                JArray Gallery = (JArray)galleryObj["Gallery"];
                if (Gallery != null)
                {
                    JObject item = (JObject)Gallery.FirstOrDefault(appItem => appItem["Name"].ToString() == fullName);
                    if (item != null)
                    {
                        Gallery.Remove(item);
                        saveGalleryJSON(galleryObj);
                    }
                }
            }
        }

        public void removeGalleryInstall(string fullName)
        {
            removeItemFromGalleryJSON(fullName);

            if (galleryDictionary.ContainsKey(fullName))
            {
                try
                {
                    galleryDictionary.Remove(fullName);
                }
                catch (Exception err)
                {
                    Debug.WriteLine($"ERROR: {err.ToString()}");
                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                }
            }

            string[] updatedSPTGallery = sptGallery.Where(item => item != fullName).ToArray();
            sptGallery = updatedSPTGallery;
        }

        public void addGalleryInstall(string fullPath)
        {
            bool galleryFileExists = File.Exists(galleryFile);
            if (galleryFileExists)
            {
                galleryDictionary = new Dictionary<string, Gallery>();
                string sptGallery = System.IO.File.ReadAllText(galleryFile);
                JObject galleryObj = JObject.Parse(sptGallery);
                JArray galleryArray = (JArray)galleryObj["Gallery"];

                foreach (JObject folder in galleryArray)
                {
                    string name = (string)folder["Name"];
                    string path = (string)folder["Path"];

                    galleryDictionary[name] = new Gallery(name, path);
                }
            }
            else
            {
                var galleryData = new JObject
                {
                    ["Gallery"] = new JArray { }
                };
                string json = galleryData.ToString();

                try
                {
                    File.WriteAllText(galleryFile, json);

                    galleryDictionary = new Dictionary<string, Gallery>();
                    string sptGallery = System.IO.File.ReadAllText(galleryFile);
                    JObject galleryObj = JObject.Parse(sptGallery);
                    JArray galleryArray = (JArray)galleryObj["Gallery"];

                    foreach (JObject folder in galleryArray)
                    {
                        string name = (string)folder["Name"];
                        string path = (string)folder["Path"];

                        galleryDictionary[name] = new Gallery(name, path);
                    }
                }
                catch (Exception err)
                {
                    Debug.WriteLine($"ERROR: {err}");
                    MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                }
            }


            string installName = Path.GetFileName(fullPath);

            if (galleryDictionary != null)
            {
                if (!sptGallery.Contains(installName) && !galleryDictionary.ContainsKey(installName))
                {
                    galleryDictionary.Add(installName, new Gallery(installName, fullPath));
                    arrInsert(ref sptGallery, installName);

                    JObject galleryObj = loadGalleryObj();
                    JArray galleryArray = (JArray)galleryObj["Gallery"];
                    galleryArray.Add(new JObject(
                        new JProperty("Name", installName),
                        new JProperty("Path", fullPath)));

                    saveGalleryJSON(galleryObj);
                    readGallery();
                }
            }
        }

        private JObject loadGalleryObj()
        {
            bool galleryFileExists = File.Exists(galleryFile);
            if (galleryFileExists)
            {
                string galleryData = File.ReadAllText(galleryFile);
                return JObject.Parse(galleryData);
            }
            return null;
        }

        private JObject loadDevOptions()
        {
            bool settingsFileExists = File.Exists(settingsFile);
            if (settingsFileExists)
            {
                string settingsData = File.ReadAllText(settingsFile);
                JObject settingsContent = JObject.Parse(settingsData);

                JObject devOptions = (JObject)settingsContent["Developer_Options"];
                return devOptions;
            }
            return null;
        }

        private void saveGalleryJSON(JObject galleryObj)
        {
            string json = galleryObj.ToString(Formatting.Indented);
            bool galleryFileExists = File.Exists(galleryFile);
            if (galleryFileExists)
                File.WriteAllText(galleryFile, json);
        }

        public int fetchInstalledMods()
        {
            int clientFolders = 0;
            int clientFiles = 0;
            int serverFolders = 0;

            string clientModsBepinFolder = Path.Combine(Properties.Settings.Default.server_path, "BepInEx");
            string clientModsPluginsFolder = Path.Combine(clientModsBepinFolder, "plugins");

            string serverModsUserFolder = Path.Combine(Properties.Settings.Default.server_path, "user");
            string serverModsModsFolder = Path.Combine(serverModsUserFolder, "mods");

            bool bepinexExists = Directory.Exists(clientModsBepinFolder);
            bool pluginsExists = Directory.Exists(clientModsPluginsFolder);
            if (bepinexExists && pluginsExists)
            {
                clientFolders = Directory.GetDirectories(clientModsPluginsFolder).Length;
                clientFiles = Directory.GetFiles(clientModsPluginsFolder, "*.dll").Length;
            }

            bool userExists = Directory.Exists(serverModsUserFolder);
            bool modsExists = Directory.Exists(serverModsModsFolder);
            if (userExists && modsExists)
            {
                serverFolders = Directory.GetDirectories(serverModsModsFolder).Length;
            }

            int total = clientFiles + clientFiles + serverFolders;
            total = total - 1;

            return total;
        }

        /*
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
        */

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

        public void useInstall(string fullName)
        {
            boxSelectedServerTitle.Text = fullName;

            if (TarkovProcessDetector != null)
                TarkovProcessDetector.Dispose();

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
                string name = fullName;

                Gallery foundInstall = fetchInstall(galleryFile, name);
                if (foundInstall != null)
                {
                    string installName = foundInstall.Name;
                    string installPath = foundInstall.Path;

                    string akiData = Path.Combine(installPath, "Aki_Data");
                    bool akiDataExists = Directory.Exists(akiData);
                    if (akiDataExists)
                    {
                        string akiServer = Path.Combine(akiData, "Server");
                        bool akiServerExists = Directory.Exists(akiServer);
                        if (akiDataExists)
                        {
                            string akiConfigs = Path.Combine(akiServer, "configs");
                            bool akiConfigsExists = Directory.Exists(akiConfigs);
                            if (akiConfigsExists)
                            {
                                core = Path.Combine(akiConfigs, "core.json");
                                if (File.Exists(core))
                                {
                                    string lastUsedName = Path.GetFileName(installPath);
                                    Properties.Settings.Default.server_path = installPath;
                                    Properties.Settings.Default.lastUsedInstall = lastUsedName;
                                    boxPath.Text = Properties.Settings.Default.server_path;
                                    Properties.Settings.Default.Save();

                                    string userFolder = Path.Combine(installPath, "user");
                                    bool userFolderExists = Directory.Exists(userFolder);
                                    if (userFolderExists)
                                    {
                                        string modsFolder = Path.Combine(userFolder, "mods");
                                        bool modsFolderExists = Directory.Exists(modsFolder);
                                        if (modsFolderExists)
                                        {
                                            string orderJSON = Path.Combine(modsFolder, "order.json");
                                            bool orderJSONExists = File.Exists(orderJSON);
                                            if (orderJSONExists)
                                            {
                                                try
                                                {
                                                    updateOrderJSON(modsFolder);
                                                }
                                                catch (Exception err)
                                                {
                                                    Debug.WriteLine($"ERROR: {err.ToString()}");
                                                    // MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                                                }
                                            }
                                        }
                                    }

                                    listServerOptions(true);
                                    checkForSingularProfile(installPath);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine($"ERROR: {err.ToString()}");
                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
            }
        }

        private void lbl_MouseDown(object sender, EventArgs e)
        {
            System.Windows.Forms.Label lbl = (System.Windows.Forms.Label)sender;

            if (lbl.Text != "")
            {
                string fullName = lbl.Text;

                if ((Control.MouseButtons & MouseButtons.Right) != 0)
                {
                    if (lbl.Text.ToLower() != "add new spt-aki install")
                    {
                        if (MessageBox.Show($"Do you wish to remove {fullName} from your Gallery?{Environment.NewLine}{Environment.NewLine}" +
                                        $"This will not delete your install from your computer.", this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            removeGalleryInstall(fullName);
                            Properties.Settings.Default.currentProfileAID = null;
                            Properties.Settings.Default.server_path = null;
                            Properties.Settings.Default.Save();

                            bProfilePlaceholder.Text = "Click here to select a profile";
                            boxPath.Clear();
                            readGallery();
                        }
                    }
                }
                else
                {
                    lbl.BackColor = listSelectedcolor;

                    if (lbl.Text.ToLower() == "add new spt-aki install")
                        browseInstallation();
                    else
                        useInstall(lbl.Text);
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

                    bool settingsFileExists = File.Exists(settingsFile);
                    if (settingsFileExists)
                    {
                        string settingsContent = File.ReadAllText(settingsFile);
                        JObject settingsObj = JObject.Parse(settingsContent);

                        JObject timeOptions = (JObject)settingsObj["timeOptions"];
                        int previousTarkovSeconds = (int)timeOptions["tarkovTime"];

                        TimeSpan elapsedTarkovDuration = TimeSpan.FromSeconds(previousTarkovSeconds);
                        startTimeTarkov = DateTime.Now - elapsedTarkovDuration;
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

                List<string> tempList = new List<string>();

                JObject devOptions = loadDevOptions();
                bool simpleMode = (bool)devOptions["Simple_Mode"];

                if (simpleMode != null)
                {
                    if (!simpleMode)
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
                            else if (serverOptionsStreets[i].ToLower() == "- miscellaneous -")
                            {
                                lbl.Text = "  Miscellaneous";
                                lbl.Cursor = Cursors.Arrow;
                                lbl.BackColor = this.BackColor;
                                lbl.ForeColor = Color.FromArgb(255, 180, 46, 107);
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
                            else if (serverOptionsStreets[i].ToLower() == "view installed mods")
                            {
                                int installedmods = fetchInstalledMods();

                                if (installedmods == 0)
                                    lbl.Text = $"No mods to view";
                                else if (installedmods == 1)
                                    lbl.Text = $"View installed mod - {fetchInstalledMods().ToString()} total";
                                else if (installedmods > 1)
                                    lbl.Text = $"View installed mods - {fetchInstalledMods().ToString()} total";

                                lbl.Name = "launcherViewInstalledMods";
                                lbl.BackColor = listBackcolor;
                                lbl.ForeColor = Color.LightGray;
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
                        string[] serverOptionsStreetsSimple = {
                            "- ACTIONS -",
                            "Clear cache",
                            "Launch SPT-AKI",
                            "Stop SPT-AKI",
                            "- MODS -",
                            "View installed mods",
                            "- THIRDPARTY -"
                        };

                        for (int i = 0; i < serverOptionsStreetsSimple.Length; i++)
                        {
                            Label lbl = new Label();
                            lbl.AutoSize = false;
                            lbl.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
                            lbl.TextAlign = ContentAlignment.MiddleLeft;
                            lbl.Size = new Size(boxSelectedServer.Size.Width, boxSelectedServerPlaceholder.Size.Height);
                            lbl.Location = new Point(boxSelectedServerPlaceholder.Location.X, boxSelectedServerPlaceholder.Location.Y + (i * 30));
                            lbl.Cursor = Cursors.Hand;

                            if (serverOptionsStreetsSimple[i].ToLower() == "launch spt-aki")
                            {
                                lbl.Name = "launcherRunButton";
                                lbl.Text = "Launch SPT-AKI";
                                lbl.BackColor = listBackcolor;
                                lbl.ForeColor = Color.DodgerBlue;
                                lbl.Font = new Font("Bahnschrift Light", 9, FontStyle.Regular);
                            }
                            else if (serverOptionsStreetsSimple[i].ToLower() == "clear cache")
                            {
                                lbl.Name = "launcherClearCacheButton";
                                lbl.Text = "Clear cache";
                                lbl.BackColor = listBackcolor;
                                lbl.ForeColor = Color.LightGray;
                                lbl.Font = new Font("Bahnschrift Light", 9, FontStyle.Regular);
                            }
                            else if (serverOptionsStreetsSimple[i].ToLower() == "stop spt-aki")
                            {
                                lbl.Name = "launcherStopSPTIfRunning";
                                lbl.Text = "Stop SPT-AKI";
                                lbl.BackColor = listBackcolor;
                                lbl.ForeColor = Color.IndianRed;
                                lbl.Font = new Font("Bahnschrift Light", 9, FontStyle.Regular);
                            }
                            else if (serverOptionsStreetsSimple[i].ToLower() == "- mods -")
                            {
                                lbl.Text = "  Mods";
                                lbl.Cursor = Cursors.Arrow;
                                lbl.BackColor = this.BackColor;
                                lbl.ForeColor = Color.DodgerBlue;
                                lbl.Font = new Font("Bahnschrift Light", 10, FontStyle.Regular);
                            }
                            else if (serverOptionsStreetsSimple[i].ToLower() == "- actions -")
                            {
                                lbl.Text = "  Actions";
                                lbl.Cursor = Cursors.Arrow;
                                lbl.BackColor = this.BackColor;
                                lbl.ForeColor = Color.IndianRed;
                                lbl.Font = new Font("Bahnschrift Light", 10, FontStyle.Regular);
                            }
                            else if (serverOptionsStreetsSimple[i].ToLower() == "- thirdparty -")
                            {
                                lbl.Text = "  Third Party Apps";
                                lbl.Cursor = Cursors.Arrow;
                                lbl.BackColor = this.BackColor;
                                lbl.ForeColor = Color.DarkSeaGreen;
                                lbl.Font = new Font("Bahnschrift Light", 10, FontStyle.Regular);
                            }
                            else if (serverOptionsStreetsSimple[i].ToLower() == "view installed mods")
                            {
                                int installedmods = fetchInstalledMods();

                                if (installedmods == 0)
                                    lbl.Text = $"No mods to view";
                                else if (installedmods == 1)
                                    lbl.Text = $"View installed mod - {fetchInstalledMods().ToString()} total";
                                else if (installedmods > 1)
                                    lbl.Text = $"View installed mods - {fetchInstalledMods().ToString()} total";

                                lbl.Name = "launcherViewInstalledMods";
                                lbl.BackColor = listBackcolor;
                                lbl.ForeColor = Color.LightGray;
                                lbl.Font = new Font("Bahnschrift Light", 9, FontStyle.Regular);
                            }
                            else
                            {
                                lbl.Text = serverOptionsStreetsSimple[i];
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
                }
                checkThirdPartyApps(Properties.Settings.Default.server_path);
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
            if (label.Text != "" && label.Text != "  Mods" && label.Text != "  Miscellaneous" && label.Text != "  Actions" && label.Text != "  Third Party Apps")
            {
                label.BackColor = listHovercolor;
            }
        }

        private void lbl2_MouseLeave(object sender, EventArgs e)
        {
            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;
            if (label.Text != "" && label.Text != "  Mods" && label.Text != "  Miscellaneous" && label.Text != "  Actions" && label.Text != "  Third Party Apps")
            {
                label.BackColor = listBackcolor;
            }
        }

        private void lbl2_MouseUp(object sender, EventArgs e)
        {
            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;
            if (label.Text != "" && label.Text != "  Mods" && label.Text != "  Miscellaneous" && label.Text != "  Actions" && label.Text != "  Third Party Apps")
            {
                label.BackColor = label.BackColor = listHovercolor;
            }
        }

        private async void lbl2_MouseDown(object sender, MouseEventArgs e)
        {
            // The main action happens here
            // Detecting when options and tools are used in any way
            // Includes right-click and left-click systems

            System.Windows.Forms.Label label = (System.Windows.Forms.Label)sender;
            if (label.Text != "" && label.Text != "  Mods" && label.Text != "  Miscellaneous" && label.Text != "  Actions" && label.Text != "  Third Party Apps")
            {
                string currentDir = Directory.GetCurrentDirectory();
                label.BackColor = listSelectedcolor;

                if (label.Text.ToLower().Contains("open a profile -"))
                {
                    profileSelector frm = new profileSelector();

                    frm.boxSelectedServerTitle = boxSelectedServerTitle.Text;
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
                                string userFolder = Path.Combine(Properties.Settings.Default.server_path, "user");
                                bool userFolderExists = Directory.Exists(userFolder);
                                if (userFolderExists)
                                {
                                    string cacheFolder = Path.Combine(userFolder, "cache");
                                    bool cacheFolderExists = Directory.Exists(cacheFolder);
                                    if (cacheFolderExists)
                                    {
                                        clearServerCache(cacheFolder);
                                    }
                                }
                            }
                        }
                        else
                        {
                            string userFolder = Path.Combine(Properties.Settings.Default.server_path, "user");
                            bool userFolderExists = Directory.Exists(userFolder);
                            if (userFolderExists)
                            {
                                string cacheFolder = Path.Combine(userFolder, "cache");
                                bool cacheFolderExists = Directory.Exists(cacheFolder);
                                if (cacheFolderExists)
                                {
                                    clearServerCache(cacheFolder);
                                }
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
                        label.Text = "Loading SPT, this may take a few";
                        label.Enabled = false;

                        startLaunch();
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
                    }

                    else if (label.Text.ToLower() ==
                        "open modloader json")
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

                    else if (label.Text.ToLower() ==
                        "export mods into zip")
                    {
                        bool serverFolderExists = Directory.Exists(Properties.Settings.Default.server_path);

                        if (serverFolderExists)
                        {
                            string warning = $"As per the SPT-AKI Workshop rules / file submission guidelines:\n\n";

                            string serverName = boxSelectedServerTitle.Text;
                            modExport exportForm = new modExport(this.Size.Width, this.Size.Height);

                            exportForm.Text = $"Select mods to export from {serverName}";
                            exportForm.ShowDialog();
                        }
                    }

                    else if (label.Text.ToLower() ==
                        "import existing mods via zip")
                    {
                        bool serverFolderExists = Directory.Exists(Properties.Settings.Default.server_path);

                        if (serverFolderExists)
                        {
                            string serverName = boxSelectedServerTitle.Text;

                        }
                    }

                    else if (label.Text.ToLower()
                        .StartsWith("view installed mod"))
                    {
                        string serverName = boxSelectedServerTitle.Text;
                        modExport exportForm = new modExport(this.Size.Width, this.Size.Height);

                        exportForm.Text = $"Mod Viewer - {serverName}";
                        exportForm.ShowDialog();
                    }

                    else if (label.Text.ToLower() ==
                        "add new tool")
                    {
                        addThirdParty addwnd = new addThirdParty(this, false);
                        addwnd.ShowDialog();
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
                                addwnd.ShowDialog();
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

        public void startLaunch()
        {
            bool profileMatchesServerAccount = false;

            if (Properties.Settings.Default.server_path != null)
            {
                if (Properties.Settings.Default.currentProfileAID != null && Properties.Settings.Default.currentProfileAID != "")
                {
                    string fullPath = Properties.Settings.Default.server_path;

                    string userFolder = Path.Combine(fullPath, "user");
                    bool userFolderExists = Directory.Exists(userFolder);
                    if (userFolderExists)
                    {
                        string profilesFolder = Path.Combine(userFolder, "profiles");
                        bool profilesFolderExists = Directory.Exists(profilesFolder);
                        if (profilesFolderExists)
                        {
                            string[] profiles = Directory.GetFiles(profilesFolder, "*.json");
                            foreach (string profile in profiles)
                            {
                                string profileId = Path.GetFileNameWithoutExtension(profile);
                                if (profileId == Properties.Settings.Default.currentProfileAID)
                                {
                                    profileMatchesServerAccount = true;
                                    break;
                                }
                            }
                        }

                        if (Properties.Settings.Default.clearCache == 1)
                        {
                            string cacheFolder = Path.Combine(userFolder, "cache");
                            bool cacheFolderExists = Directory.Exists(cacheFolder);
                            if (cacheFolderExists)
                                clearServerCache(cacheFolder);
                        }
                    }

                    if (profileMatchesServerAccount)
                    {
                        switch (Properties.Settings.Default.hideOptions)
                        {
                            case 1:
                                minimizeLauncherWindow();
                                break;
                        }

                        runServer();
                    }
                    else
                    {
                        string fullProfile = fetchProfileFromAID(Properties.Settings.Default.currentProfileAID);
                        string serverName = boxSelectedServerTitle.Text;

                        showError($"It seems that {fullProfile} does not exist in {serverName}");
                    }
                }
                else
                {
                    showError("You don\'t have a profile selected. Please head into Options -> SPT-AKI Settings and select a profile, then try again!");
                }
            }
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

            bool settingsFileExists = File.Exists(settingsFile);
            if (settingsFileExists)
            {
                string settingsContent = File.ReadAllText(settingsFile);
                JObject settingsObj = JObject.Parse(settingsContent);

                JObject timeOptions = (JObject)settingsObj["timeOptions"];
                int previousServerSeconds = (int)timeOptions["serverTime"];

                TimeSpan elapsedServerDuration = TimeSpan.FromSeconds(previousServerSeconds);
                startTimeServer = DateTime.Now - elapsedServerDuration;
            }
        }

        public void runLauncher()
        {
            string launcherProcess = "Aki.Launcher";
            Process[] launchers = Process.GetProcessesByName(launcherProcess);
            string currentDir = Directory.GetCurrentDirectory();
            int akiPort = 0;
            string ip_address = "";

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
                            ip_address = (string)parsedJson["ip"];
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

            if (Properties.Settings.Default.bypassLauncher)
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
                        _tarkov.Arguments = $"-token={aid} -config={{\"BackendUrl\":\"http://{ip_address}:{akiPort}\",\"Version\":\"live\"}}";
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
                        _tarkov.Arguments = $"-token={Properties.Settings.Default.currentProfileAID} -config={{\"BackendUrl\":\"http://{ip_address}:{akiPort}\",\"Version\":\"live\"}}";
                    }
                    else
                    {

                        _tarkov.Arguments = $"-token={Properties.Settings.Default.currentProfileAID} -config={{\"BackendUrl\":\"http://127.0.0.1:6969\",\"Version\":\"live\"}}";
                    }
                }

                Process tarkovGame = new Process();
                tarkovGame.StartInfo = _tarkov;
                tarkovGame.Start();

                bool settingsFileExists = File.Exists(settingsFile);
                if (settingsFileExists)
                {
                    string settingsContent = File.ReadAllText(settingsFile);
                    JObject settingsObj = JObject.Parse(settingsContent);

                    JObject timeOptions = (JObject)settingsObj["timeOptions"];
                    int previousTarkovSeconds = (int)timeOptions["tarkovTime"];

                    TimeSpan elapsedTarkovDuration = TimeSpan.FromSeconds(previousTarkovSeconds);
                    startTimeTarkov = DateTime.Now - elapsedTarkovDuration;
                }

                TarkovEndDetector = new BackgroundWorker();
                TarkovEndDetector.DoWork += TarkovEndDetector_DoWork;
                TarkovEndDetector.RunWorkerCompleted += TarkovEndDetector_RunWorkerCompleted;
                TarkovEndDetector.RunWorkerAsync();
            }
            else
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

                Directory.SetCurrentDirectory(currentDir);

                TarkovProcessDetector = new BackgroundWorker();
                TarkovProcessDetector.DoWork += TarkovProcessDetector_DoWork;
                TarkovProcessDetector.RunWorkerCompleted += TarkovProcessDetector_RunWorkerCompleted;
                TarkovProcessDetector.RunWorkerAsync();
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

                    bool settingsFileExists = File.Exists(settingsFile);
                    if (settingsFileExists)
                    {
                        string settingsContent = File.ReadAllText(settingsFile);
                        JObject settingsObj = JObject.Parse(settingsContent);

                        if (settingsObj["timeOptions"] != null)
                        {
                            JObject timeOptions = (JObject)settingsObj["timeOptions"];

                            DateTime endTime = DateTime.Now;
                            TimeSpan playtimeServer = endTime - startTimeServer;
                            TimeSpan playtimeTarkov = endTime - startTimeTarkov;

                            int serverPlaytimeSeconds = (int)playtimeServer.TotalSeconds;
                            int tarkovPlaytimeSeconds = (int)playtimeTarkov.TotalSeconds;
                            timeOptions["serverTime"] = (int)serverPlaytimeSeconds;
                            timeOptions["tarkovTime"] = (int)serverPlaytimeSeconds;

                            string updatedJSON = settingsObj.ToString();
                            File.WriteAllText(settingsFile, updatedJSON);
                        }
                    }

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
            catch (System.Net.Sockets.SocketException ex)
            {
                Console.WriteLine($"IGNORE: {ex.Message.ToString()}");
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
            browseInstallation();
        }

        private void boxOpenIn_Click(object sender, EventArgs e)
        {
            if (boxPath.Text != null || boxPath.Text != "" && boxPath.Text.Length > 0)
            {
                Console.WriteLine(boxPath.Text);
                string fullPath = boxPath.Text;
                bool fullPathExists = Directory.Exists(fullPath);
                if (fullPathExists)
                {
                    try
                    {
                        ProcessStartInfo newApp = new ProcessStartInfo();
                        newApp.WorkingDirectory = Properties.Settings.Default.server_path;
                        newApp.FileName = Path.GetFileName(newApp.WorkingDirectory);
                        newApp.UseShellExecute = true;
                        newApp.Verb = "open";

                        Process.Start(newApp);
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine($"ERROR: {err.ToString()}");
                        MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                    }
                }
                else
                {
                    showError("There is no valid path, or the path is empty.");
                }
            }
            else
            {
                showError("There is no valid path, or the path is empty.");
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
                    if (File.Exists(Path.Combine(Path.GetFullPath(item), "Aki.Server.exe")) &&
                            File.Exists(Path.Combine(Path.GetFullPath(item), "Aki.Launcher.exe")) &&
                            Directory.Exists(Path.Combine(Path.GetFullPath(item), "Aki_Data")))
                    {
                        try
                        {
                            boxPath.Text = Path.GetFullPath(item);
                            Properties.Settings.Default.server_path = Path.GetFullPath(item);
                            Properties.Settings.Default.Save();

                            readGallery();
                        }
                        catch (Exception err)
                        {
                            Debug.WriteLine($"ERROR: {err.ToString()}");
                            MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                        }
                    }
                }
            }
            boxPathBox.Select();
        }

        private void bResetApp_Click(object sender, EventArgs e)
        {
            
        }

        private void bResetApp_MouseEnter(object sender, EventArgs e)
        {
        }

        private void bResetApp_MouseLeave(object sender, EventArgs e)
        {
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
                                readGallery();
                            }
                            catch (Exception err)
                            {
                                Debug.WriteLine($"ERROR: {err.ToString()}");
                                MessageBox.Show($"Oops! It seems like we received an error. If you're uncertain what it\'s about, please message the developer with a screenshot:\n\n{err.ToString()}", this.Text, MessageBoxButtons.OK);
                            }
                        }
                    }
                }

                boxPathBox.Select();
            }
        }

        private void boxSelectedServerTitle_Click_1(object sender, EventArgs e)
        {
            if (boxSelectedServerTitle.Text.ToLower() != "spt placeholder" && boxSelectedServerTitle.Text.ToLower() != "no spt detected")
            {
                try
                {
                    string currentName = boxSelectedServerTitle.Text;
                    Gallery foundItem = fetchInstall(galleryFile, currentName);
                    if (foundItem != null)
                    {
                        string installName = foundItem.Name;
                        string installPath = foundItem.Path;
                        Process.Start("explorer.exe", installPath);
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
        }

        private void bResetThirdParty_MouseLeave(object sender, EventArgs e)
        {
        }

        private void bResetThirdParty_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.LControlKey | Keys.R))
            {
                e.SuppressKeyPress = true;
                e.Handled = true;

                readGallery();
                showError("Refreshed!");
            }
        }

        private void bRefresh_Click(object sender, EventArgs e)
        {
            
        }

        private void bRefresh_MouseEnter(object sender, EventArgs e)
        {
        }

        private void bRefresh_MouseLeave(object sender, EventArgs e)
        {
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
            if (Properties.Settings.Default.server_path != null)
            {
                if (boxSelectedServerTitle.Text != "SPT Placeholder")
                {
                    optionsWindow frm = new optionsWindow(bProfilePlaceholder, this, false);
                    frm.selectedServer = boxSelectedServerTitle.Text;

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
        }

        private void bToggleOutputWindow_MouseLeave(object sender, EventArgs e)
        {
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
            
        }

        private void bProfilePlaceholder_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.server_path != null)
            {
                if (boxSelectedServerTitle.Text != "SPT Placeholder")
                {
                    optionsWindow frm = new optionsWindow(bProfilePlaceholder, this, true);
                    frm.selectedServer = boxSelectedServerTitle.Text;

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

        private void bProfilePlaceholder_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void bProfilePlaceholder_MouseEnter(object sender, EventArgs e)
        {
            bProfilePlaceholder.ForeColor = Color.DodgerBlue;
        }

        private void bProfilePlaceholder_MouseLeave(object sender, EventArgs e)
        {
            bProfilePlaceholder.ForeColor = Color.LightGray;
        }
    }

    public class ThirdPartyInfo
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }

        public ThirdPartyInfo(string name, string path, string type)
        {
            Name = name;
            Path = path;
            Type = type;
        }
    }

    public class Gallery
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public Gallery(string name, string path)
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