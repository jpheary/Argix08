//	File:	globals.cs
//	Author:	J. Heary
//	Date:	03/05/09
//	Desc:	Custom Settings class that uses terminal-specific app.config settings
//          based upon the terminal code passed by the Click-Once deployment query string 
//          parameter (accessed by Program.TerminalCode).
//	Rev:	
//	---------------------------------------------------------------------------
namespace Argix.Properties {
    //
    internal class ArgixSettings :global::System.Configuration.ApplicationSettingsBase {
        //Members
        private static ArgixSettings Instance = null;

        //Interface
        static ArgixSettings() {
            if(Instance == null) {
                Instance = ((ArgixSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new ArgixSettings())));
                Instance.Context["GroupName"] = "Argix.Properties." + Program.TerminalCode + "Settings";
                Instance.Reload();
            }
        }
        public static ArgixSettings Default { get { return Instance; } }

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
    }
}
