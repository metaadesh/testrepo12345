﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace METAOPTION.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://www.manheimautoauctiononline3.com/hhead/RegisterVehicles.php")]
        public string HeadStartVMS_ManheimAutoAuction_OnlineRegistration_RegisterVehicles {
            get {
                return ((string)(this["HeadStartVMS_ManheimAutoAuction_OnlineRegistration_RegisterVehicles"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://sql/ReportServer/ReportService.asmx")]
        public string HeadStartVMS_ReportingService_ReportingService {
            get {
                return ((string)(this["HeadStartVMS_ReportingService_ReportingService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost:9616/HeadStartVMS/WS/AutoFillCustomers.asmx")]
        public string HeadStartVMS_AutoFillCustomersService_AutoFillNames {
            get {
                return ((string)(this["HeadStartVMS_AutoFillCustomersService_AutoFillNames"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://24.229.49.203/HeadStartVMS.asmx")]
        public string HeadStartVMS_PeachtreeService_HeadStartVMSPtree {
            get {
                return ((string)(this["HeadStartVMS_PeachtreeService_HeadStartVMSPtree"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://cr.rhollensheadautosales.com/ws/LinkCRService.asmx")]
        public string HeadStartVMS_UCR_LinkCRService {
            get {
                return ((string)(this["HeadStartVMS_UCR_LinkCRService"]));
            }
        }
    }
}