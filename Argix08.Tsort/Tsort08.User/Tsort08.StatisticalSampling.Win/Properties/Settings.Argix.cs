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
