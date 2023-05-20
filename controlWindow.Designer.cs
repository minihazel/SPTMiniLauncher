namespace SPTMiniLauncher
{
    partial class controlWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelFolders = new System.Windows.Forms.GroupBox();
            this.bConfigs = new System.Windows.Forms.Button();
            this.bDatabase = new System.Windows.Forms.Button();
            this.bBots = new System.Windows.Forms.Button();
            this.bHideout = new System.Windows.Forms.Button();
            this.bLocales = new System.Windows.Forms.Button();
            this.bMaps = new System.Windows.Forms.Button();
            this.bLoot = new System.Windows.Forms.Button();
            this.bMatch = new System.Windows.Forms.Button();
            this.bTemplates = new System.Windows.Forms.Button();
            this.bTraders = new System.Windows.Forms.Button();
            this.panelGlobalFiles = new System.Windows.Forms.GroupBox();
            this.bFilesGlobals = new System.Windows.Forms.Button();
            this.bFilesServer = new System.Windows.Forms.Button();
            this.bFilesSettings = new System.Windows.Forms.Button();
            this.bFilesItems = new System.Windows.Forms.Button();
            this.bFilesQuests = new System.Windows.Forms.Button();
            this.bFilesProfiles = new System.Windows.Forms.Button();
            this.bBepCache = new System.Windows.Forms.Button();
            this.bBepPlugins = new System.Windows.Forms.Button();
            this.bLogs = new System.Windows.Forms.Button();
            this.bLauncherConfig = new System.Windows.Forms.Button();
            this.bMods = new System.Windows.Forms.Button();
            this.bProfiles = new System.Windows.Forms.Button();
            this.panelFolders.SuspendLayout();
            this.panelGlobalFiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelFolders
            // 
            this.panelFolders.Controls.Add(this.bProfiles);
            this.panelFolders.Controls.Add(this.bMods);
            this.panelFolders.Controls.Add(this.bLogs);
            this.panelFolders.Controls.Add(this.bLauncherConfig);
            this.panelFolders.Controls.Add(this.bBepPlugins);
            this.panelFolders.Controls.Add(this.bBepCache);
            this.panelFolders.Controls.Add(this.bTraders);
            this.panelFolders.Controls.Add(this.bTemplates);
            this.panelFolders.Controls.Add(this.bMatch);
            this.panelFolders.Controls.Add(this.bLoot);
            this.panelFolders.Controls.Add(this.bMaps);
            this.panelFolders.Controls.Add(this.bLocales);
            this.panelFolders.Controls.Add(this.bHideout);
            this.panelFolders.Controls.Add(this.bBots);
            this.panelFolders.Controls.Add(this.bDatabase);
            this.panelFolders.Controls.Add(this.bConfigs);
            this.panelFolders.ForeColor = System.Drawing.Color.LightGray;
            this.panelFolders.Location = new System.Drawing.Point(12, 12);
            this.panelFolders.Name = "panelFolders";
            this.panelFolders.Size = new System.Drawing.Size(437, 377);
            this.panelFolders.TabIndex = 5;
            this.panelFolders.TabStop = false;
            this.panelFolders.Text = " Folders ";
            // 
            // bConfigs
            // 
            this.bConfigs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bConfigs.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bConfigs.FlatAppearance.BorderSize = 0;
            this.bConfigs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bConfigs.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bConfigs.Location = new System.Drawing.Point(15, 23);
            this.bConfigs.Name = "bConfigs";
            this.bConfigs.Size = new System.Drawing.Size(207, 30);
            this.bConfigs.TabIndex = 4;
            this.bConfigs.Text = "Configs folder";
            this.bConfigs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bConfigs.UseVisualStyleBackColor = true;
            this.bConfigs.Click += new System.EventHandler(this.bConfigs_Click);
            // 
            // bDatabase
            // 
            this.bDatabase.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bDatabase.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bDatabase.FlatAppearance.BorderSize = 0;
            this.bDatabase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDatabase.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bDatabase.Location = new System.Drawing.Point(15, 56);
            this.bDatabase.Name = "bDatabase";
            this.bDatabase.Size = new System.Drawing.Size(207, 30);
            this.bDatabase.TabIndex = 5;
            this.bDatabase.Text = "Database folder";
            this.bDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bDatabase.UseVisualStyleBackColor = true;
            this.bDatabase.Click += new System.EventHandler(this.bDatabase_Click);
            // 
            // bBots
            // 
            this.bBots.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bBots.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bBots.FlatAppearance.BorderSize = 0;
            this.bBots.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bBots.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bBots.Location = new System.Drawing.Point(15, 89);
            this.bBots.Name = "bBots";
            this.bBots.Size = new System.Drawing.Size(207, 30);
            this.bBots.TabIndex = 6;
            this.bBots.Text = "└── Bots";
            this.bBots.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bBots.UseVisualStyleBackColor = true;
            this.bBots.Click += new System.EventHandler(this.bBots_Click);
            // 
            // bHideout
            // 
            this.bHideout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bHideout.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bHideout.FlatAppearance.BorderSize = 0;
            this.bHideout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bHideout.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bHideout.Location = new System.Drawing.Point(15, 122);
            this.bHideout.Name = "bHideout";
            this.bHideout.Size = new System.Drawing.Size(207, 30);
            this.bHideout.TabIndex = 7;
            this.bHideout.Text = "└── Hideout";
            this.bHideout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bHideout.UseVisualStyleBackColor = true;
            this.bHideout.Click += new System.EventHandler(this.bHideout_Click);
            // 
            // bLocales
            // 
            this.bLocales.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bLocales.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bLocales.FlatAppearance.BorderSize = 0;
            this.bLocales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bLocales.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bLocales.Location = new System.Drawing.Point(15, 155);
            this.bLocales.Name = "bLocales";
            this.bLocales.Size = new System.Drawing.Size(207, 30);
            this.bLocales.TabIndex = 8;
            this.bLocales.Text = "└── Locales";
            this.bLocales.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bLocales.UseVisualStyleBackColor = true;
            this.bLocales.Click += new System.EventHandler(this.bLocales_Click);
            // 
            // bMaps
            // 
            this.bMaps.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bMaps.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bMaps.FlatAppearance.BorderSize = 0;
            this.bMaps.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMaps.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bMaps.Location = new System.Drawing.Point(15, 188);
            this.bMaps.Name = "bMaps";
            this.bMaps.Size = new System.Drawing.Size(207, 30);
            this.bMaps.TabIndex = 9;
            this.bMaps.Text = "└── Maps";
            this.bMaps.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bMaps.UseVisualStyleBackColor = true;
            this.bMaps.Click += new System.EventHandler(this.bMaps_Click);
            // 
            // bLoot
            // 
            this.bLoot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bLoot.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bLoot.FlatAppearance.BorderSize = 0;
            this.bLoot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bLoot.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bLoot.Location = new System.Drawing.Point(15, 221);
            this.bLoot.Name = "bLoot";
            this.bLoot.Size = new System.Drawing.Size(207, 30);
            this.bLoot.TabIndex = 10;
            this.bLoot.Text = "└── Loot";
            this.bLoot.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bLoot.UseVisualStyleBackColor = true;
            this.bLoot.Click += new System.EventHandler(this.bLoot_Click);
            // 
            // bMatch
            // 
            this.bMatch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bMatch.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bMatch.FlatAppearance.BorderSize = 0;
            this.bMatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMatch.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bMatch.Location = new System.Drawing.Point(15, 254);
            this.bMatch.Name = "bMatch";
            this.bMatch.Size = new System.Drawing.Size(207, 30);
            this.bMatch.TabIndex = 11;
            this.bMatch.Text = "└── Match";
            this.bMatch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bMatch.UseVisualStyleBackColor = true;
            this.bMatch.Click += new System.EventHandler(this.bMatch_Click);
            // 
            // bTemplates
            // 
            this.bTemplates.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bTemplates.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bTemplates.FlatAppearance.BorderSize = 0;
            this.bTemplates.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bTemplates.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bTemplates.Location = new System.Drawing.Point(15, 287);
            this.bTemplates.Name = "bTemplates";
            this.bTemplates.Size = new System.Drawing.Size(207, 30);
            this.bTemplates.TabIndex = 12;
            this.bTemplates.Text = "└── Templates";
            this.bTemplates.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bTemplates.UseVisualStyleBackColor = true;
            this.bTemplates.Click += new System.EventHandler(this.bTemplates_Click);
            // 
            // bTraders
            // 
            this.bTraders.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bTraders.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bTraders.FlatAppearance.BorderSize = 0;
            this.bTraders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bTraders.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bTraders.Location = new System.Drawing.Point(15, 320);
            this.bTraders.Name = "bTraders";
            this.bTraders.Size = new System.Drawing.Size(207, 30);
            this.bTraders.TabIndex = 13;
            this.bTraders.Text = "└── Traders";
            this.bTraders.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bTraders.UseVisualStyleBackColor = true;
            this.bTraders.Click += new System.EventHandler(this.bTraders_Click);
            // 
            // panelGlobalFiles
            // 
            this.panelGlobalFiles.Controls.Add(this.bFilesProfiles);
            this.panelGlobalFiles.Controls.Add(this.bFilesQuests);
            this.panelGlobalFiles.Controls.Add(this.bFilesItems);
            this.panelGlobalFiles.Controls.Add(this.bFilesSettings);
            this.panelGlobalFiles.Controls.Add(this.bFilesServer);
            this.panelGlobalFiles.Controls.Add(this.bFilesGlobals);
            this.panelGlobalFiles.ForeColor = System.Drawing.Color.LightGray;
            this.panelGlobalFiles.Location = new System.Drawing.Point(12, 404);
            this.panelGlobalFiles.Name = "panelGlobalFiles";
            this.panelGlobalFiles.Size = new System.Drawing.Size(437, 137);
            this.panelGlobalFiles.TabIndex = 6;
            this.panelGlobalFiles.TabStop = false;
            this.panelGlobalFiles.Text = " Global files ";
            // 
            // bFilesGlobals
            // 
            this.bFilesGlobals.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bFilesGlobals.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bFilesGlobals.FlatAppearance.BorderSize = 0;
            this.bFilesGlobals.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bFilesGlobals.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bFilesGlobals.Location = new System.Drawing.Point(15, 23);
            this.bFilesGlobals.Name = "bFilesGlobals";
            this.bFilesGlobals.Size = new System.Drawing.Size(207, 30);
            this.bFilesGlobals.TabIndex = 3;
            this.bFilesGlobals.Text = "globals.json";
            this.bFilesGlobals.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bFilesGlobals.UseVisualStyleBackColor = true;
            this.bFilesGlobals.Click += new System.EventHandler(this.bFilesGlobals_Click);
            // 
            // bFilesServer
            // 
            this.bFilesServer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bFilesServer.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bFilesServer.FlatAppearance.BorderSize = 0;
            this.bFilesServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bFilesServer.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bFilesServer.Location = new System.Drawing.Point(15, 56);
            this.bFilesServer.Name = "bFilesServer";
            this.bFilesServer.Size = new System.Drawing.Size(207, 30);
            this.bFilesServer.TabIndex = 4;
            this.bFilesServer.Text = "server.json";
            this.bFilesServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bFilesServer.UseVisualStyleBackColor = true;
            this.bFilesServer.Click += new System.EventHandler(this.bFilesServer_Click);
            // 
            // bFilesSettings
            // 
            this.bFilesSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bFilesSettings.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bFilesSettings.FlatAppearance.BorderSize = 0;
            this.bFilesSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bFilesSettings.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bFilesSettings.Location = new System.Drawing.Point(15, 89);
            this.bFilesSettings.Name = "bFilesSettings";
            this.bFilesSettings.Size = new System.Drawing.Size(207, 30);
            this.bFilesSettings.TabIndex = 5;
            this.bFilesSettings.Text = "settings.json";
            this.bFilesSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bFilesSettings.UseVisualStyleBackColor = true;
            this.bFilesSettings.Click += new System.EventHandler(this.bFilesSettings_Click);
            // 
            // bFilesItems
            // 
            this.bFilesItems.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bFilesItems.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bFilesItems.FlatAppearance.BorderSize = 0;
            this.bFilesItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bFilesItems.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bFilesItems.Location = new System.Drawing.Point(224, 23);
            this.bFilesItems.Name = "bFilesItems";
            this.bFilesItems.Size = new System.Drawing.Size(207, 30);
            this.bFilesItems.TabIndex = 6;
            this.bFilesItems.Text = "Items database";
            this.bFilesItems.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bFilesItems.UseVisualStyleBackColor = true;
            this.bFilesItems.Click += new System.EventHandler(this.bFilesItems_Click);
            // 
            // bFilesQuests
            // 
            this.bFilesQuests.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bFilesQuests.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bFilesQuests.FlatAppearance.BorderSize = 0;
            this.bFilesQuests.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bFilesQuests.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bFilesQuests.Location = new System.Drawing.Point(224, 56);
            this.bFilesQuests.Name = "bFilesQuests";
            this.bFilesQuests.Size = new System.Drawing.Size(207, 30);
            this.bFilesQuests.TabIndex = 7;
            this.bFilesQuests.Text = "Quests database";
            this.bFilesQuests.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bFilesQuests.UseVisualStyleBackColor = true;
            this.bFilesQuests.Click += new System.EventHandler(this.bFilesQuests_Click);
            // 
            // bFilesProfiles
            // 
            this.bFilesProfiles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bFilesProfiles.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bFilesProfiles.FlatAppearance.BorderSize = 0;
            this.bFilesProfiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bFilesProfiles.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bFilesProfiles.Location = new System.Drawing.Point(224, 89);
            this.bFilesProfiles.Name = "bFilesProfiles";
            this.bFilesProfiles.Size = new System.Drawing.Size(207, 30);
            this.bFilesProfiles.TabIndex = 8;
            this.bFilesProfiles.Text = "Profiles database";
            this.bFilesProfiles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bFilesProfiles.UseVisualStyleBackColor = true;
            this.bFilesProfiles.Click += new System.EventHandler(this.bFilesProfiles_Click);
            // 
            // bBepCache
            // 
            this.bBepCache.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bBepCache.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bBepCache.FlatAppearance.BorderSize = 0;
            this.bBepCache.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bBepCache.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bBepCache.Location = new System.Drawing.Point(224, 23);
            this.bBepCache.Name = "bBepCache";
            this.bBepCache.Size = new System.Drawing.Size(207, 30);
            this.bBepCache.TabIndex = 14;
            this.bBepCache.Text = "BepInEx » config";
            this.bBepCache.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bBepCache.UseVisualStyleBackColor = true;
            this.bBepCache.Click += new System.EventHandler(this.bBepCache_Click);
            // 
            // bBepPlugins
            // 
            this.bBepPlugins.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bBepPlugins.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bBepPlugins.FlatAppearance.BorderSize = 0;
            this.bBepPlugins.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bBepPlugins.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bBepPlugins.Location = new System.Drawing.Point(224, 56);
            this.bBepPlugins.Name = "bBepPlugins";
            this.bBepPlugins.Size = new System.Drawing.Size(207, 30);
            this.bBepPlugins.TabIndex = 15;
            this.bBepPlugins.Text = "BepInEx » plugins";
            this.bBepPlugins.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bBepPlugins.UseVisualStyleBackColor = true;
            this.bBepPlugins.Click += new System.EventHandler(this.bBepPlugins_Click);
            // 
            // bLogs
            // 
            this.bLogs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bLogs.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bLogs.FlatAppearance.BorderSize = 0;
            this.bLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bLogs.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bLogs.Location = new System.Drawing.Point(224, 122);
            this.bLogs.Name = "bLogs";
            this.bLogs.Size = new System.Drawing.Size(207, 30);
            this.bLogs.TabIndex = 17;
            this.bLogs.Text = "Logs folder";
            this.bLogs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bLogs.UseVisualStyleBackColor = true;
            this.bLogs.Click += new System.EventHandler(this.bLogs_Click);
            // 
            // bLauncherConfig
            // 
            this.bLauncherConfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bLauncherConfig.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bLauncherConfig.FlatAppearance.BorderSize = 0;
            this.bLauncherConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bLauncherConfig.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bLauncherConfig.Location = new System.Drawing.Point(224, 89);
            this.bLauncherConfig.Name = "bLauncherConfig";
            this.bLauncherConfig.Size = new System.Drawing.Size(207, 30);
            this.bLauncherConfig.TabIndex = 16;
            this.bLauncherConfig.Text = "Launcher » config";
            this.bLauncherConfig.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bLauncherConfig.UseVisualStyleBackColor = true;
            this.bLauncherConfig.Click += new System.EventHandler(this.bLauncherConfig_Click);
            // 
            // bMods
            // 
            this.bMods.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bMods.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bMods.FlatAppearance.BorderSize = 0;
            this.bMods.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bMods.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bMods.Location = new System.Drawing.Point(224, 155);
            this.bMods.Name = "bMods";
            this.bMods.Size = new System.Drawing.Size(207, 30);
            this.bMods.TabIndex = 18;
            this.bMods.Text = "Mods folder";
            this.bMods.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bMods.UseVisualStyleBackColor = true;
            this.bMods.Click += new System.EventHandler(this.bMods_Click);
            // 
            // bProfiles
            // 
            this.bProfiles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bProfiles.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.bProfiles.FlatAppearance.BorderSize = 0;
            this.bProfiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bProfiles.ForeColor = System.Drawing.Color.DodgerBlue;
            this.bProfiles.Location = new System.Drawing.Point(224, 188);
            this.bProfiles.Name = "bProfiles";
            this.bProfiles.Size = new System.Drawing.Size(207, 30);
            this.bProfiles.TabIndex = 19;
            this.bProfiles.Text = "Profiles folder";
            this.bProfiles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bProfiles.UseVisualStyleBackColor = true;
            this.bProfiles.Click += new System.EventHandler(this.bProfiles_Click);
            // 
            // controlWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(679, 556);
            this.Controls.Add(this.panelGlobalFiles);
            this.Controls.Add(this.panelFolders);
            this.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.ForeColor = System.Drawing.Color.LightGray;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "controlWindow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Control Panel";
            this.Load += new System.EventHandler(this.controlWindow_Load);
            this.panelFolders.ResumeLayout(false);
            this.panelGlobalFiles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox panelFolders;
        private System.Windows.Forms.Button bConfigs;
        private System.Windows.Forms.Button bDatabase;
        private System.Windows.Forms.Button bBots;
        private System.Windows.Forms.Button bHideout;
        private System.Windows.Forms.Button bLocales;
        private System.Windows.Forms.Button bMaps;
        private System.Windows.Forms.Button bLoot;
        private System.Windows.Forms.Button bMatch;
        private System.Windows.Forms.Button bTemplates;
        private System.Windows.Forms.Button bTraders;
        private System.Windows.Forms.GroupBox panelGlobalFiles;
        private System.Windows.Forms.Button bFilesGlobals;
        private System.Windows.Forms.Button bFilesServer;
        private System.Windows.Forms.Button bFilesSettings;
        private System.Windows.Forms.Button bFilesItems;
        private System.Windows.Forms.Button bFilesQuests;
        private System.Windows.Forms.Button bFilesProfiles;
        private System.Windows.Forms.Button bBepCache;
        private System.Windows.Forms.Button bBepPlugins;
        private System.Windows.Forms.Button bLogs;
        private System.Windows.Forms.Button bLauncherConfig;
        private System.Windows.Forms.Button bProfiles;
        private System.Windows.Forms.Button bMods;
    }
}