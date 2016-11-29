//	File:	globals.cs
//	Author:	J. Heary
//	Date:	01/12/05
//	Desc:	Enumerators, event support, exceptions, etc.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                Process appInstance = AppServices.RunningInstance(App.Product);
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
        public static string TerminalCode {
            get {
                if(_TerminalCode == null) {
                    Dictionary<string,string> d = GetQueryStringParameters();
                    string code = "";
                    _TerminalCode = d.TryGetValue("terminal",out code) ? code : "";
                }
                return _TerminalCode;
            }
        }
    }

    //Global application object
    public class App:AppBase {
        //Members
        private static Mediator _Mediator = null;
        private static bool _UseWebSvc = false;
        private static ConfigFactory _ConfigFactory = null;
        private static Config _Config = null;
        private static winTrace _Trace=null;

        //Global configuration and database constants
        public const string USP_CONFIGURATION = "uspConfigurationGetList",TBL_CONFIGURATION = "ConfigTable";
        public const string USP_LOCALTERMINAL = "uspEnterpriseCurrentTerminalGet",TBL_LOCALTERMINAL = "LocalTerminalTable";
        public const string USP_TRACE = "uspSortLogEntryNew";


        //Interface
        static App() {
            //Class constructor: get application configuration
            try {
                Mediator.SqlCommandTimeout = global::Tsort.Properties.Settings.Default.SQLCommandTimeout;
                UseWebSvc = global::Tsort.Properties.TsortSettings.Default.UseWebSvc;
                _ConfigFactory = new ConfigFactory(App.Product);
                _Config = (Config)_ConfigFactory.Item(new string[] { Environment.UserName,Environment.MachineName });
                _Trace = new winTrace();
            }
            catch(Exception ex) { ReportError(ex); Application.Exit(); }
        }
        ~App() {
            //Destructor
            _Trace.Close();
        }
        internal static Mediator Mediator { get { return _Mediator; } }
        internal static bool UseWebSvc {
            get { return _UseWebSvc; }
            set {
                _UseWebSvc = value;
                _Mediator = _UseWebSvc ? (Mediator)new WebSvcMediator() : (Mediator)new SQLMediator();
            }
        }
        internal static Config Config { get { return _Config; } }
        internal static void ShowConfig() {
            _ConfigFactory.ShowDialog(_Config.MISPassword);
            _Config = (Config)_ConfigFactory.Item(new string[] { Environment.UserName,Environment.MachineName });
        }
        internal static void ShowDiagnostics() {
            dlgLogin login = new dlgLogin("argix" + DateTime.Today.DayOfYear);
            login.StartPosition = FormStartPosition.CenterScreen;
            login.ValidateEntry();
            if(login.IsValid) {
                dlgData dlg = new dlgData(Assembly.GetExecutingAssembly(),App.Product);
                dlg.ShowDialog();
            }
        }
        internal static void ShowTrace() {
            dlgLogin login = new dlgLogin(_Config.MISPassword);
            login.ValidateEntry();
            if(login.IsValid) _Trace.Show();
        }
        internal static void HideTrace() { _Trace.Hide(); }
        public static string EventLogName { get { return "Tsort08"; } }

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
                    ArgixTrace.WriteLine(new TraceMessage(ex.ToString(),App.Product,level));
            }
            catch(Exception) { }
        }
    }

    internal class ConfigFactory:AppConfigFactory {
        //Members

        //Interface
        public ConfigFactory(string productName) : base(productName) { }
        protected override Mediator ConfigMediator { get { return App.Mediator; } }
        protected override AppConfig NewAppConfig(string PCName,DataSet ds) { return new Config(PCName,ds,App.Mediator); }
    }
    internal class Config:AppConfig {
        //Members

        //Interface
        public Config(string PCName,DataSet ds,Mediator mediator) : base(PCName,ds,mediator) { }

    }
}