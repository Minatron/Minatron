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
        <string>9f19e4c4-2d7b-463a-82e7-977cd5cb8300</string>
        <string>5c4b35c7-97f6-4bb5-b90f-a96aebb12b23</string>
         </ArrayOfString>
    </value>
  </item>
  <item>
    <key>
      <int>1</int>
    </key>
    <value>
      <ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
              <string>9f19e4c4-2d7b-463a-82e7-977cd5cb8300</string>
        <string>5c4b35c7-97f6-4bb5-b90f-a96aebb12b23</string>

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
    }
}
