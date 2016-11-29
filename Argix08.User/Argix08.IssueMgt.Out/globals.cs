//	File:	globals.cs
//	Author:	J. Heary
//	Date:	08/12/09
//	Desc:	Global enumerators, event support, exceptions, etc.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using Argix.Configuration;
using Argix.Windows;
using Argix.Data;

namespace Argix {
    //
    //Global application object
    public class App {
        //Members
        private static Mediator _Mediator = null;
        private static bool _UseWebSvc = false;
        private static ConfigFactory _ConfigFactory = null;
        private static Config _Config = null;
        private static winTrace _Trace = null;

        //Global configuration and database constants		
        public const string USP_TRACE = "uspLogEntryNew";

        //Interface
        static App() {
            //Class constructor: get application configuration
            try {
                UseWebSvc = global::Argix.CustomerSvc.Settings.Default.UseWebSvc;
                try {
                    _ConfigFactory = new ConfigFactory(App.AssemblyProduct);
                    _Config = (Config)_ConfigFactory.Item(new string[] { Environment.UserName,Environment.MachineName });
                }
                catch(ApplicationException ex) { ReportError(ex,true,LogLevel.None); }
                _Trace = new winTrace();
            }
            catch(Exception ex) { ReportError(ex,true,LogLevel.None); }
        }
        ~App() {
            //Destructor
            _Trace.Close();
        }
        public static Mediator Mediator { get { return _Mediator; } }
        public static bool UseWebSvc {
            get { return _UseWebSvc; }
            set {
                _UseWebSvc = value;
                _Mediator = _UseWebSvc ? (Mediator)new WebSvcMediator(global::Argix.CustomerSvc.Settings.Default.DataAccessWS) : (Mediator)new SQLMediator(global::Argix.CustomerSvc.Settings.Default.SQLConnection);
            }
        }
        public static Config Config { get { return _Config; } }
        public static void ShowConfig() {
            _ConfigFactory.ShowDialog(_Config.MISPassword);
            _Config = (Config)_ConfigFactory.Item(new string[] { Environment.UserName,Environment.MachineName });
        }
        public static void ShowDiagnostics() {
            dlgLogin login = new dlgLogin("argix" + DateTime.Today.DayOfYear);
            login.StartPosition = FormStartPosition.CenterScreen;
            login.ValidateEntry();
            if(login.IsValid) {
                dlgData dlg = new dlgData(Assembly.GetExecutingAssembly());
                dlg.ShowDialog();
            }
        }
        public static void ShowTrace() {
            dlgLogin login = new dlgLogin(_Config.MISPassword);
            login.ValidateEntry();
            if(login.IsValid) _Trace.Show();
        }
        public static void HideTrace() { _Trace.Hide(); }
        public static string AssemblyProduct { get { return "Issue Mgt"; } }
        public static string EventLogName { get { return "IssueMgt"; } }
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
                    MessageBox.Show(msg,App.AssemblyProduct,MessageBoxButtons.OK,MessageBoxIcon.Error);
                if(level != LogLevel.None)
                    ArgixTrace.WriteLine(new TraceMessage(ex.ToString(),App.AssemblyProduct,level));
            }
            catch(Exception) { }
        }
    }

    public class ConfigFactory:AppConfigFactory {
        //Members

        //Interface
        public ConfigFactory(string productName) : base(productName) { }
        protected override Mediator ConfigMediator { get { return App.Mediator; } }
        protected override AppConfig NewAppConfig(string PCName,DataSet ds) { return new Config(PCName,ds,App.Mediator); }
    }
    public class Config:AppConfig {
        //Members
        private const string KEY_ISSUEDAYSBACK = "IssueDaysBack";

        //Interface
        public Config(string PCName,DataSet ds,Mediator mediator) : base(PCName,ds,mediator) { }

        [Category("Data"),Description("The number of days back for displaying closed issues.")]
        public int IssueDaysBack {
            get { return GetValueAsInteger(KEY_ISSUEDAYSBACK); }
            set { SetValue(KEY_ISSUEDAYSBACK,value.ToString()); }
        }
    }
}