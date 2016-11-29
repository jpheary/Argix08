using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Deployment.Application;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using Argix.Configuration;
using Argix.Data;
using Argix.Windows;

namespace Argix {
    /// <summary>The main entry point for the application.</summary>
    static class Program {
        //Members
        private static string _TerminalCode = null;

        //Interface
        [STAThread]
        static void Main() {
            //Application entry point
            try {
                //Start app
                Process appInstance = AppServices.RunningInstance("Argix Direct " + App.Product);
                if(appInstance == null) {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Argix.AgentLineHaul.frmMain());
                }
                else {
                    MessageBox.Show("Another instance of this application is already running.",App.Product,MessageBoxButtons.OK,MessageBoxIcon.Information);
                    AppServices.ShowWindow(appInstance.MainWindowHandle,1);
                    AppServices.SetForegroundWindow(appInstance.MainWindowHandle);
                }
            }
            catch(Exception ex) {
                MessageBox.Show("FATAL ERROR\n\n" + ex.ToString() + "\n\n Application will be closed. Please contact the IT department for help.",App.Product,MessageBoxButtons.OK,MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        static Dictionary<string,string> GetQueryStringParameters() {
            //Form a dictionary of the query string launch parameters
            Dictionary<string,string> nameValueTable = new Dictionary<string,string>();
            if(ApplicationDeployment.IsNetworkDeployed) {
                if(AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData != null) {
                    string url = AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData[0];
                    string queryString = (new Uri(url)).Query;
                    queryString = queryString.Replace("?","");
                    string[] nameValuePairs = queryString.Split('&');
                    foreach(string pair in nameValuePairs) {
                        string[] vars = pair.Split('=');
                        if(!nameValueTable.ContainsKey(vars[0])) {
                            nameValueTable.Add(vars[0],vars[1]);
                        }
                    }
                }
            }
            return (nameValueTable);
        }
        public static string TerminalCode {
            get {
                if(_TerminalCode == null) {
                    Dictionary<string,string> d = GetQueryStringParameters();
                    if(d.Count > 0) {
                        string code = "";
                        _TerminalCode = d.TryGetValue("terminal",out code) ? code : "";
                    }
                    else {
                        Argix.Windows.dlgInputBox ib = new dlgInputBox("Enter a terminal code (i.e. JA)","","Terminal Code");
                        ib.StartPosition = FormStartPosition.CenterScreen;
                        ib.TopMost = true;
                        if (ib.ShowDialog() == DialogResult.OK) _TerminalCode = ib.Value; else _TerminalCode = "";
                    }
                }
                return _TerminalCode;
            }
        }
    }

	//Global application object
	public class App: AppBase {
		//Members
        private static Mediator _Mediator = null;
        private static ConfigFactory _ConfigFactory = null;
        private static Config _Config = null;
						
		//Interface
        static App() {
            //Class constructor: get application configuration
            try {
                Mediator.SqlCommandTimeout = int.Parse(global::Argix.Properties.Settings.Default.SQLCommandTimeout);
                _Mediator = new SQLMediator(global::Argix.Properties.ArgixSettings.Default.SQLConnection);
                _ConfigFactory = new ConfigFactory(App.Product);
                _Config = (Config)_ConfigFactory.Item(new string[] { Environment.UserName,Environment.MachineName });
                ArgixTrace.AddListener(new DBTraceListener((LogLevel)_Config.TraceLevel,_Mediator,"uspLogEntryNew","Argix08"));
            }
            catch(Exception ex) { ReportError(ex); Application.Exit(); }
        }
        internal static Mediator Mediator { get { return _Mediator; } }
        internal static Config Config { get { return _Config; } }
        internal static void ShowConfig() {
            _ConfigFactory.ShowDialog(_Config.MISPassword);
            _Config = (Config)_ConfigFactory.Item(new string[] { Environment.UserName,Environment.MachineName });
        }
        public static void Trace(string message,LogLevel level) {
            //Trace
            ArgixTrace.WriteLine(new TraceMessage(message,App.Product,level));
        }
        public static void ReportError(Exception ex) { ReportError(ex,true,LogLevel.None); }
        public static void ReportError(Exception ex,bool displayMessage) { ReportError(ex,displayMessage,LogLevel.None); }
        public static void ReportError(Exception ex,bool displayMessage,LogLevel level) {
            //Report an exception to the user
            try {
                string src = (ex.Source != null) ? ex.Source + "-\n" : "";
                string msg = src + ex.Message;
                if(ex.InnerException != null) {
                    if((ex.InnerException.Source != null)) src = ex.InnerException.Source + "-\n";
                    msg = src + ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
                }
                if(displayMessage)
                    MessageBox.Show(msg,App.Product,MessageBoxButtons.OK,MessageBoxIcon.Error);
                if(level != LogLevel.None)
                    Trace(ex.ToString(),level);
            }
            catch(Exception) { }
        }
        public static void CheckVersion() {
            //Check for a version update
            try {
                if(global::Argix.Properties.Settings.Default.LastVersion != App.Version)
                    MessageBox.Show("This is an updated version of " + App.Product + ". Please refer to Help\\Release Notes for release information.",App.Product,MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch(Exception) { }
        }
    }

    internal class ConfigFactory :AppConfigFactory {
        //Members

        //Interface
        public ConfigFactory(string productName) : base(productName) { }
        protected override Mediator ConfigMediator { get { return App.Mediator; } }
        protected override AppConfig NewAppConfig(string PCName,DataSet ds) { return new Config(PCName,ds,App.Mediator); }
    }
    internal class Config :AppConfig {
        //Members
        private const string KEY_PASTBUSINESSDAYS = "PastBusinessDays";
        private const string KEY_ROLE = "Role";
        private const string KEY_SCHEDULEDAYSBACK = "ScheduleDaysBack";
        private const string KEY_SCHEDULEDAYSFORWARD = "ScheduleDaysForward";
        
        //Interface
        public Config(string PCName,DataSet ds,Mediator mediator) : base(PCName,ds,mediator) { }

        [Category("Behavior"),Description("Shows closed schedules for this many business days back.")]
        public int PastBusinessDays {
            get { return GetValueAsInteger(KEY_PASTBUSINESSDAYS); }
            set { SetValue(KEY_PASTBUSINESSDAYS,value.ToString()); }
        }
        [Category("Accessibility"),Description("User role- grants application access rights (i.e. LineHaulOperator, LineHaulCoordinator, LineHaulAdministrator).")]
        public string Role {
            get { return GetValue(KEY_ROLE); }
            set { SetValue(KEY_ROLE,value); }
        }
        [Category("Behavior"),Description("Hides open schedules older than this many days.")]
        public int ScheduleDaysBack {
            get { return GetValueAsInteger(KEY_SCHEDULEDAYSBACK); }
            set { SetValue(KEY_SCHEDULEDAYSBACK,value.ToString()); }
        }
        [Category("Behavior"),Description("Hides open schedules dated beyond this many days.")]
        public int ScheduleDaysForward {
            get { return GetValueAsInteger(KEY_SCHEDULEDAYSFORWARD); }
            set { SetValue(KEY_SCHEDULEDAYSFORWARD,value.ToString()); }
        }
    }

    public delegate void StatusEventHandler(object source,StatusEventArgs e);
    public class StatusEventArgs:EventArgs {
        private string _message="";
        public StatusEventArgs(string message) { this._message = message; }
        public string Message { get { return this._message; } set { this._message = value; } }
    }
    public class DuplicateLoadNumberException : ApplicationException {
        public const string default_Message = "Duplicate load number found for the same carrier within the past and future one week schedule.";
        public DuplicateLoadNumberException() : this(DuplicateLoadNumberException.default_Message) { }
        public DuplicateLoadNumberException(string message) : base(message) { }
    }
    
    namespace Security {
        //
        public class AppSecurity {
            //Members
            public static string ROLE_NONE = "LineHaulOperator";
            public static string ROLE_COORDINATOR = "LineHaulCoordinator";
            public static string ROLE_ADMINISTRATOR = "LineHaulAdministrator";

            //Interface
            static AppSecurity() { }
            private AppSecurity() { }
            public static bool UserCanUpdateAll { get { return (App.Config.Role.ToLower() == ROLE_COORDINATOR.ToLower() || App.Config.Role.ToLower() == ROLE_ADMINISTRATOR.ToLower()); } }
            public static bool UserCanAddSchedule { get { return (App.Config.Role.ToLower() == ROLE_ADMINISTRATOR.ToLower()); } }
        }
    }
}