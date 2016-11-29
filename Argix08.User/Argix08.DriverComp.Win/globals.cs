using System;
using System.ComponentModel;
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
        [STAThread]
        static void Main() {
            //Application entry point
            try {
                //Start app
                Process appInstance = AppServices.RunningInstance("Argix Direct " + App.Product);
                if(appInstance == null) {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Argix.Finance.frmMain());
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
    }

	/// <summary>Global application object</summary>
	public class App: AppBase {
		//Members
        private static Mediator _Mediator = null;
        private static ConfigFactory _ConfigFactory = null;
        private static Config _Config = null;
		
		//Global configuration and database constants		
		public const string USP_TRACE = "uspLogEntryNew";
        public const string USP_LOCALTERMINAL = "uspDCCurrentTerminalGet",TBL_LOCALTERMINAL = "LocalTerminalTable";
        public const string USP_ROADSHOWROUTES = "uspDCRoadshowRoutesGetList",TBL_ROADSHOWROUTES = "RoadshowRouteTable";
        public const string USP_DRIVERROUTES = "uspDCDriverRoutesGetList",TBL_DRIVERROUTES = "DriverRouteTable";
        public const string USP_ROUTECREATE = "uspDCDriverRouteNew";
        public const string USP_ROUTEUPDATE = "uspDCDriverRouteUpdate";
        public const string USP_ROUTEDELETE = "uspDCDriverRouteDelete";

		//Interface
		static App() { 
			//Class constructor: get application configuration
			try {
                _Mediator = (Mediator)new SQLMediator(global::Argix.Properties.Settings.Default.SQLConnection);
                _ConfigFactory = new ConfigFactory(App.Product);
                _Config = (Config)_ConfigFactory.Item(new string[] { Environment.UserName,Environment.MachineName });
			}
			catch(Exception ex) { ReportError(ex); Application.Exit(); }
		}
        internal static Mediator Mediator { get { return _Mediator; } }
        internal static Config Config { get { return _Config; } }
		internal static void ShowConfig() { 
            _ConfigFactory.ShowDialog(_Config.MISPassword);
            _Config = (Config)_ConfigFactory.Item(new string[] { Environment.UserName,Environment.MachineName });
        }
		public static string EventLogName { get { return "DriverComp"; } }

		public static void ReportError(Exception ex) { ReportError(ex, true, LogLevel.None); }
		public static void ReportError(Exception ex, bool displayMessage) { ReportError(ex, displayMessage, LogLevel.None); }
		public static void ReportError(Exception ex, bool displayMessage, LogLevel level) {
			//Report an exception to the user
			try {
				string src = (ex.Source != null) ? ex.Source + "-\n" : "";
				string msg = src + ex.Message;
				if(ex.InnerException != null) {
					if((ex.InnerException.Source != null)) src = ex.InnerException.Source + "-\n";
					msg = src + ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
				}
				if(displayMessage) 
					MessageBox.Show(msg, App.Product, MessageBoxButtons.OK, MessageBoxIcon.Error);
				if(level != LogLevel.None) 
					ArgixTrace.WriteLine(new TraceMessage(ex.ToString(), App.EventLogName, level));
			}
			catch(Exception) { }
		}
	}

    internal class ConfigFactory :AppConfigFactory {
        //Members

        //Interface
        public ConfigFactory(string productName) : base(productName) { }
        protected override Mediator ConfigMediator { get { return App.Mediator;  } }
        protected override AppConfig NewAppConfig(string PCName,DataSet ds) { return new Config(PCName,ds,App.Mediator); }
    }
    internal class Config :AppConfig {
        //Members
        public const string KEY_ADMINISTATOR = "Administrator";
        
        //Interface
        public Config(string PCName,DataSet ds,Mediator mediator) : base(PCName,ds,mediator) { }

        [Category("Accessibility"),Description("Allows access to administrative services.")]
        public bool Administrator {
            get { return GetValueAsBoolean(KEY_ADMINISTATOR); }
            set { SetValue(KEY_ADMINISTATOR,value.ToString()); }
        }
    }

    public delegate void StatusEventHandler(object source,StatusEventArgs e);
    public class StatusEventArgs :EventArgs {
        private string _message = "";
        public StatusEventArgs(string message) { this._message = message; }
        public string Message { get { return this._message; } set { this._message = value; } }
    }
}