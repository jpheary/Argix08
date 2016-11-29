//	File:	globals.cs
//	Author:	J. Heary
//	Date:	03/05/09
//	Desc:	Custom Settings class that uses terminal-specific app.config settings
//          based upon the terminal code passed by the Click-Once deployment query string 
//          parameter (accessed by Program.TerminalCode).
//	Rev:	
//	---------------------------------------------------------------------------
namespace Tsort.Properties {
    //
    internal class TsortSettings :global::System.Configuration.ApplicationSettingsBase {
        //Members
        private static TsortSettings Instance = null;

        //Interface
        static TsortSettings() {
            if(Instance == null) {
                Instance = ((TsortSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new TsortSettings())));
                Instance.Context["GroupName"] = "Tsort.Properties." + Program.TerminalCode + "TsortSettings";
                Instance.Reload();
            }
        }
        public static TsortSettings Default { get { return Instance; } }

        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("false")]
        public virtual bool UseWebSvc {
            get {
                return ((bool)(this["UseWebSvc"]));
            }
            set {
                this["UseWebSvc"] = value;
            }
        }

        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("data source=ARGIXDEFAULT;initial catalog=TSORTLOCAL;persist security info=True;user id=sa;password=objects;packet size=4096")]
        public virtual string SQLConnection {
            get {
                return ((string)(this["SQLConnection"]));
            }
            set {
                this["SQLConnection"] = value;
            }
        }

        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://RGXDEVDB/RGXDEVDB.TSORTLOCAL.WebSvc/DataAccess.asmx")]
        public virtual string DataAccessWS {
            get {
                return ((string)(this["DataAccessWS"]));
            }
            set {
                this["DataAccessWS"] = value;
            }
        }
    }
}
