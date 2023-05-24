﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SPTMiniLauncher.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.5.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string server_path {
            get {
                return ((string)(this["server_path"]));
            }
            set {
                this["server_path"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string profile_editor_path {
            get {
                return ((string)(this["profile_editor_path"]));
            }
            set {
                this["profile_editor_path"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string svm_path {
            get {
                return ((string)(this["svm_path"]));
            }
            set {
                this["svm_path"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string realism_path {
            get {
                return ((string)(this["realism_path"]));
            }
            set {
                this["realism_path"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string loe_path {
            get {
                return ((string)(this["loe_path"]));
            }
            set {
                this["loe_path"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int hideOptions {
            get {
                return ((int)(this["hideOptions"]));
            }
            set {
                this["hideOptions"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool timedLauncherToggle {
            get {
                return ((bool)(this["timedLauncherToggle"]));
            }
            set {
                this["timedLauncherToggle"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int clearCache {
            get {
                return ((int)(this["clearCache"]));
            }
            set {
                this["clearCache"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"
Welcome to my SPT Launcher!
It looks like it's your first time running this launcher, or you just downloaded a new update of it.

This tool has quite a few features. Via the interface you can do the following:

- Launch SPT
  Includes various customizable options

- Quit and exit SPT

- Clear cache

- Open the control panel
  This serves as a navigation hub for various important locations and files

- Open client and server mods folders

- Open server folder

- Open the load order file (post-3.5 installations only)
  Load Order Editor manages this for you (third party app)

- Manage mods
  Client mods can only be viewed and removed.
  Server mods can be viewed, removed and added.
  
- Launch Load Order Editor (if detected)
- Launch Profile Editor (if detected)
- Launch SVM (if detected)
- Launch SPT Realism (if detected)")]
        public string firstTimeMessage {
            get {
                return ((string)(this["firstTimeMessage"]));
            }
            set {
                this["firstTimeMessage"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2")]
        public int startDetector {
            get {
                return ((int)(this["startDetector"]));
            }
            set {
                this["startDetector"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public int endDetector {
            get {
                return ((int)(this["endDetector"]));
            }
            set {
                this["endDetector"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool openLogOnQuit {
            get {
                return ((bool)(this["openLogOnQuit"]));
            }
            set {
                this["openLogOnQuit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool displayConfirmationMessage {
            get {
                return ((bool)(this["displayConfirmationMessage"]));
            }
            set {
                this["displayConfirmationMessage"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool altCache {
            get {
                return ((bool)(this["altCache"]));
            }
            set {
                this["altCache"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool serverOutputting {
            get {
                return ((bool)(this["serverOutputting"]));
            }
            set {
                this["serverOutputting"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool serverErrorMessages {
            get {
                return ((bool)(this["serverErrorMessages"]));
            }
            set {
                this["serverErrorMessages"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool closeOnQuit {
            get {
                return ((bool)(this["closeOnQuit"]));
            }
            set {
                this["closeOnQuit"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool tarkovDetector {
            get {
                return ((bool)(this["tarkovDetector"]));
            }
            set {
                this["tarkovDetector"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool closeControlPanel {
            get {
                return ((bool)(this["closeControlPanel"]));
            }
            set {
                this["closeControlPanel"] = value;
            }
        }
    }
}
