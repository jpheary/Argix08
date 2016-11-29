//	File:	globals.cs
//	Author:	J. Heary
//	Date:	01/07/09
//	Desc:	Global aplication object; enumerators, event support, exceptions, etc.
//          NOTES:  App class is globally accessible (static)
//                  App.Config returns globally accessible application configuration object
//                  App requires USP_CONFIGURATION, USP_TRACE, USP_LOCALTERMINAL stored procs
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Deployment.Application;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using Argix;
using Argix.Configuration;
using Argix.Data;
using Argix.Windows;

namespace Tsort {
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
                    Application.Run(new Tsort.Sort.frmMain());
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
        public static string TerminalCode
        {
            get
            {
                if (_TerminalCode == null)
                {
                    Dictionary<string, string> d = GetQueryStringParameters();
                    if (d.Count > 0)
                    {
                        string code = "";
                        _TerminalCode = d.TryGetValue("terminal", out code) ? code : "";
                    }
                    else
                    {
                        Argix.Windows.dlgInputBox ib = new dlgInputBox("Enter a terminal code (i.e. JA)", "", "Terminal Code");
                        ib.StartPosition = FormStartPosition.CenterScreen;
                        ib.TopMost = true;
                        if (ib.ShowDialog() == DialogResult.OK) _TerminalCode = ib.Value;
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
        private static bool _UseWebSvc=false;
        private static ConfigFactory _ConfigFactory = null;
        private static Config _Config = null;
        private static winTrace _Trace = null;
		
		//Global configuration and database constants
        //Required stored procedures for configuration, tracing, and terminal location
        public const string USP_CONFIGURATION = "uspConfigurationGetList",TBL_CONFIGURATION = "ConfigTable";
        public const string USP_TRACE = "uspLogEntryNew";
		public const string USP_LOCALTERMINAL = "uspEnterpriseCurrentTerminalGet", TBL_LOCALTERMINAL = "LocalTerminalTable";
        public const string USP_BMCPALLETGET = "uspBMCPalletGet", TBL_PALLET = "PalletTable";
        public const string USP_BMCPALLETCARTONNEW = "uspBMCPalletCartonInsert";
        public const string USP_BMCPALLETCARTONDELETE = "uspBMCPalletCartonDelete";

		//Interface
        static App() {
            //Class constructor: get application configuration
            try {
                UseWebSvc = false;
                _ConfigFactory = new ConfigFactory(App.Product);
                _Config = (Config)_ConfigFactory.Item(new string[] { Environment.UserName,Environment.MachineName });
                _Trace = new winTrace();
            }
            catch(Exception ex) { ReportError(ex); Application.Exit(); }
        }
        private App() { }
        ~App() {
            //Destructor
            _Trace.Close();
        }
        internal static Mediator Mediator { get { return _Mediator; } }
        internal static bool UseWebSvc {
            get { return _UseWebSvc; }
            set {
                _UseWebSvc = value;
                _Mediator = _UseWebSvc ? (Mediator)new WebSvcMediator() : (Mediator)new SQLMediator(global::Tsort.Properties.TsortSettings.Default.SQLConnection);
            }
        }
        internal static Config Config { get { return _Config; } }
        internal static void ShowConfig() {
            //Show the configuration dialog
            _ConfigFactory.ShowDialog(_Config.MISPassword);
            _Config = (Config)_ConfigFactory.Item(new string[] { Environment.UserName,Environment.MachineName });
        }
        internal static void ShowDiagnostics() {
            //Show the Diagnostics dialog
            dlgLogin login = new dlgLogin("argix" + DateTime.Today.DayOfYear);
            login.StartPosition = FormStartPosition.CenterScreen;
            login.ValidateEntry();
            if(login.IsValid) {
                dlgData dlg = new dlgData(Assembly.GetExecutingAssembly());
                dlg.ShowDialog();
            }
        }
        internal static void ShowTrace() {
            //Show the trace window
            dlgLogin login = new dlgLogin(_Config.MISPassword);
            login.ValidateEntry();
            if(login.IsValid) _Trace.Show();
        }
        internal static void HideTrace() { _Trace.Hide(); }
        internal static string EventLogName { get { return "BMCPallet"; } }
        internal static void ReportError(Exception ex) { ReportError(ex,true,LogLevel.None); }
        internal static void ReportError(Exception ex,bool displayMessage) { ReportError(ex,displayMessage,LogLevel.None); }
        internal static void ReportError(Exception ex,bool displayMessage,LogLevel level) {
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
                    ArgixTrace.WriteLine(new TraceMessage(ex.ToString(),App.EventLogName,level));
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
        //Custom configuration parameters- constant + get{}; add set{} for read/write
        //private const string KEY_ROLE = "Role";
        
        //Interface
        public Config(string PCName,DataSet ds,Mediator mediator) : base(PCName,ds,mediator) { }

        //[Category("Accessibility"),Description("User role.")]
        //public string Role { 
        //    get { return GetValue(KEY_ROLE); } 
        //    set { SetValue(KEY_ROLE,value); }
        //}
    }
	
	public delegate void StatusEventHandler(object source, StatusEventArgs e);
	public class StatusEventArgs : EventArgs {
		private string _message="";
		public StatusEventArgs(string message) { this._message = message; }
		public string Message { get { return this._message; } set { this._message = value; } }
	}
}