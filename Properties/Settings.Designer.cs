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
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.4.0.0")]
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
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool minimizeToggle {
            get {
                return ((bool)(this["minimizeToggle"]));
            }
            set {
                this["minimizeToggle"] = value;
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
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool clearCache {
            get {
                return ((bool)(this["clearCache"]));
            }
            set {
                this["clearCache"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"
Welcome to my SPT Mini Launcher!
It looks like it's your first time running this launcher, or you just downloaded a new update of it.

This tool has quite a few features. Via the control panel you can do the following:

- Launch SPT   (cache will be automatically cleared)

- Quit and exit SPT   (cache will be automatically cleared)

- Clear cache

- Open client and server mods folders

- Open the load order file (post-3.5 installations only)
   \__ Load Order Editor manages this for you (third party app)

- Manage mods
   \__ Client mods can only be viewed and removed. Server mods can be viewed, removed and added.
  
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
        public bool displayStopConfirmation {
            get {
                return ((bool)(this["displayStopConfirmation"]));
            }
            set {
                this["displayStopConfirmation"] = value;
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
    }
}
