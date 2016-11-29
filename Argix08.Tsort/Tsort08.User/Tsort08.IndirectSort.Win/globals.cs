using System;
using System.Collections.Generic;
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
                Process appInstance = AppServices.RunningInstance(App.Product);
                if (appInstance == null) {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Argix.Freight.frmMain());
                }
                else {
                    MessageBox.Show("Another instance of this application is already running.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AppServices.ShowWindow(appInstance.MainWindowHandle, 1);
                    AppServices.SetForegroundWindow(appInstance.MainWindowHandle);
                }
            }
            catch (Exception ex) {
                MessageBox.Show("FATAL ERROR\n\n" + ex.ToString() + "\n\n Application will be closed. Please contact the IT department for help.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        static Dictionary<string, string> GetQueryStringParameters() {
            //Form a dictionary of the query string launch parameters
            Dictionary<string, string> nameValueTable = new Dictionary<string, string>();
            if (ApplicationDeployment.IsNetworkDeployed) {
                if (AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData != null) {
                    string url = AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData[0];
                    string queryString = (new Uri(url)).Query;
                    queryString = queryString.Replace("?", "");
                    string[] nameValuePairs = queryString.Split('&');
                    foreach (string pair in nameValuePairs) {
                        string[] vars = pair.Split('=');
                        if (!nameValueTable.ContainsKey(vars[0])) {
                            nameValueTable.Add(vars[0], vars[1]);
                        }
                    }
                }
            }
            return (nameValueTable);
        }
        public static string TerminalCode {
            get {
                if (_TerminalCode == null) {
                    Dictionary<string, string> d = GetQueryStringParameters();
                    if (d.Count > 0) {
                        string code = "";
                        _TerminalCode = d.TryGetValue("terminal", out code) ? code : "";
                    }
                    else {
                        Argix.Windows.dlgInputBox ib = new dlgInputBox("Enter a terminal code (i.e. JA)", "", "Terminal Code");
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
    public class App : AppBase {
		//Members
        private static Mediator _Mediator = null;
        private static ConfigFactory _ConfigFactory = null;
        private static Config _Config = null;
				
		public const string USP_TRACE = "uspLogEntryNew";
				
		//Interface
        static App() {
            //Class constructor: get application configuration
            try {
                Mediator.SqlCommandTimeout = global::Argix.Properties.Settings.Default.SQLCommandTimeout;
                _Mediator = new SQLMediator(global::Argix.Properties.ArgixSettings.Default.SQLConnection);
                _ConfigFactory = new ConfigFactory(App.Product);
                _Config = (Config)_ConfigFactory.Item(new string[] { Environment.UserName, Environment.MachineName });
            }
            catch (Exception ex) { ReportError(ex); Application.Exit(); }
        }
        internal static Mediator Mediator { get { return _Mediator; } }
        internal static Config Config { get { return _Config; } }
        internal static void ShowConfig() {
            _ConfigFactory.ShowDialog(_Config.MISPassword);
            _Config = (Config)_ConfigFactory.Item(new string[] { Environment.UserName, Environment.MachineName });
        }
        public static string EventLogName { get { return "Argix08"; } }
        public static void ReportError(Exception ex) { ReportError(ex, true, LogLevel.None); }
        public static void ReportError(Exception ex, bool displayMessage) { ReportError(ex, displayMessage, LogLevel.None); }
        public static void ReportError(Exception ex, bool displayMessage, LogLevel level) {
            //Report an exception to the user
            try {
                string src = (ex.Source != null) ? ex.Source + "-\n" : "";
                string msg = src + ex.Message;
                if (ex.InnerException != null) {
                    if ((ex.InnerException.Source != null)) src = ex.InnerException.Source + "-\n";
                    msg = src + ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
                }
                if (displayMessage)
                    MessageBox.Show(msg, App.Product, MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (level != LogLevel.None)
                    ArgixTrace.WriteLine(new TraceMessage(ex.ToString(), App.Product, level));
            }
            catch (Exception) { }
        }
        public static void CheckVersion() {
            //Check for a version update
            try {
                if (global::Argix.Properties.Settings.Default.LastVersion != App.Version)
                    MessageBox.Show("This is an updated version of " + App.Product + ". Please refer to Help\\Release Notes for release information.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception) { }
        }
    }

    internal class ConfigFactory : AppConfigFactory {
        //Members

        //Interface
        public ConfigFactory(string productName) : base(productName) { }
        protected override Mediator ConfigMediator { get { return App.Mediator; } }
        protected override AppConfig NewAppConfig(string PCName, DataSet ds) { return new Config(PCName, ds, App.Mediator); }
    }
    internal class Config : AppConfig {
        //Members
		public const string KEY_LABELORIDEREG = "LabelOverrideRegular";
		public const string KEY_LABELORIDERET = "LabelOverrideReturn";
		public const string KEY_LANEPREFIX = "LanePrefix";
		public const string KEY_SCANSIZE = "ScanSize";
		public const string KEY_VALIDATELANE = "ValidateLane";
		public const string KEY_VALIDATESMALLLANE = "ValidateSmallLane";

        //Interface
        public Config(string PCName, DataSet ds, Mediator mediator) : base(PCName, ds, mediator) { }

        [Category("Behavior"), Description(".")]
        public string LabelOverrideRegular {
            get { return GetValue(KEY_LABELORIDEREG); }
        }
        [Category("Behavior"), Description(".")]
        public string LabelOverrideReturn {
            get { return GetValue(KEY_LABELORIDERET); }
        }
        [Category("Behavior"), Description(".")]
        public string LanePrefix {
            get { return GetValue(KEY_LANEPREFIX); }
        }
        [Category("Behavior"), Description(".")]
        public int ScanSize {
            get { return GetValueAsInteger(KEY_SCANSIZE); }
        }
        [Category("Behavior"), Description(".")]
        public bool ValidateLane {
            get { return GetValueAsBoolean(KEY_VALIDATELANE); }
        }
        [Category("Behavior"), Description(".")]
        public bool ValidateSmallLane {
            get { return GetValueAsBoolean(KEY_VALIDATESMALLLANE); }
        }
    }
		
	public delegate void CartonEventHandler(object source, CartonEventArgs e);
	public class CartonEventArgs : EventArgs {
		private string cartonScan;
		public CartonEventArgs(string cartonScan) {
			//Constructor
			this.cartonScan = cartonScan;
		}
		public string CartonScan {
			get { return this.cartonScan; }
			set { this.cartonScan = value; }
		}
	}
	
	public class WorkflowException : ApplicationException {
		public WorkflowException() : base() { }
		public WorkflowException(string message) : base(message) { }
		public WorkflowException(string message, Exception innerException) : base(message, innerException) { }
	}
	public class LabelException : ApplicationException {
		public LabelException() : base() { }
        public LabelException(string message) : base(message) { }
        public LabelException(string message,Exception innerException) : base(message,innerException) { }
	}
	public class PrintException : ApplicationException {
		public PrintException() : base() { }
        public PrintException(string message) : base(message) { }
        public PrintException(string message,Exception innerException) : base(message,innerException) { }
	}
}