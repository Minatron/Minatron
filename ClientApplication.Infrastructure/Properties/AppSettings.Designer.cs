﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Band.Client.Infrastructure.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    public sealed partial class AppSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static AppSettings defaultInstance = ((AppSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new AppSettings())));
        
        public static AppSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("(local)")]
        public string ServerName {
            get {
                return ((string)(this["ServerName"]));
            }
            set {
                this["ServerName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("WeighDatabase")]
        public string DBName {
            get {
                return ((string)(this["DBName"]));
            }
            set {
                this["DBName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("RU")]
        public string Lang {
            get {
                return ((string)(this["Lang"]));
            }
            set {
                this["Lang"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<dictionary>
  <item>
    <key>
      <int>0</int>
    </key>
    <value>
      <ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
        <string>5e73972f-3b78-4c96-b1fb-8e97632bea2e</string>
        <string>ac998b73-38d5-4cdd-bf4a-1d6ca9dce859</string>
        <string>5cdec79f-6552-4e11-995d-1c0736513ecf</string>
      </ArrayOfString>
    </value>
  </item>
  <item>
    <key>
      <int>1</int>
    </key>
    <value>
      <ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
             <string>ef4fc01e-a8f3-474b-b9fb-7ebc81c5364d</string>
             <string>e5b86eb0-07a8-4d57-b2a2-1321ab981955</string>
             <string>b3d506ab-4a7e-4a9b-a47d-0eb87b6cbf98</string>
      </ArrayOfString>
    </value>
  </item>
</dictionary>")]
        public string CamerasConfigurator {
            get {
                return ((string)(this["CamerasConfigurator"]));
            }
            set {
                this["CamerasConfigurator"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://ns.e105.ru:3082")]
        public string TServer {
            get {
                return ((string)(this["TServer"]));
            }
            set {
                this["TServer"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://ns.e105.ru:3084")]
        public string TVideoServer {
            get {
                return ((string)(this["TVideoServer"]));
            }
            set {
                this["TVideoServer"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("admin")]
        public string TLogin {
            get {
                return ((string)(this["TLogin"]));
            }
            set {
                this["TLogin"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string TPass {
            get {
                return ((string)(this["TPass"]));
            }
            set {
                this["TPass"] = value;
            }
        }
    }
}
