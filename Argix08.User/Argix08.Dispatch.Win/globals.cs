//	File:	globals.cs
//	Author:	J. Heary
//	Date:	09/20/05
//	Desc:	Enumerators, event support, exceptions, etc.
//	Rev:	
//	---------------------------------------------------------------------------
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
                    Application.Run(new Argix.Dispatch.frmMain());
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

    public class App:AppBase {
        //Members
        private static Mediator _Mediator = null;
        private static int _DataAccess = 0;
        private static ConfigFactory _ConfigFactory = null;
        private static Config _Config = null;
        private static winTrace _Trace=null;

        //Global configuration and database constants		
        public const string USP_CONFIGURATION = "uspConfigurationGetList",TBL_CONFIGURATION = "ConfigTable";
        public const string USP_TRACE = "uspLogEntryNew";
        public const string USP_LOCALTERMINAL = "uspEnterpriseCurrentTerminalGet",TBL_LOCALTERMINAL = "LocalTerminalTable";
        //public const string USP_TERMINALS = "uspDispatchTerminalsGetList", TBL_TERMINALS = "TerminalViewTable";
        public const string LAYOUTVIEWS_FILE = "_schedulelayouts";
        public const string LAYOUT_FILE = "_schedulelayouts";
        public const string SELECTION_DATAFILE = "_selections";

        public const int ICON_OPEN = 10;
        public const int ICON_CLOSED = 11;
        public const int ICON_APP = 12;

        //Interface
        static App() {
            //Class constructor: get application configuration
            try {
                DataAccess = Convert.ToInt32(global::Argix.Properties.Settings.Default.DataAccess);
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
        internal static int DataAccess {
            get { return _DataAccess; }
            set {
                _DataAccess = value;
                switch(_DataAccess) {
                    case 0: _Mediator = (Mediator)new SQLMediator(global::Argix.Properties.Settings.Default.SQLConnection); break;
                    case 1: _Mediator = (Mediator)new WebSvcMediator(global::Argix.Properties.Settings.Default.DataAccessWS); break;
                    case 2: _Mediator = new FileMediator(); break;
                }
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
                dlgData dlg = new dlgData(Assembly.GetExecutingAssembly());
                dlg.ShowDialog();
            }
        }
        internal static void ShowTrace() {
            dlgLogin login = new dlgLogin(_Config.MISPassword);
            login.ValidateEntry();
            if(login.IsValid) _Trace.Show();
        }
        internal static void HideTrace() { _Trace.Hide(); }
        public static string EventLogName { get { return "Argix08"; } }

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
                    ArgixTrace.WriteLine(new TraceMessage(ex.ToString(),App.EventLogName,level));
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

    public delegate void StatusEventHandler(object source,StatusEventArgs e);
    public class StatusEventArgs:EventArgs {
        private string _message = "";
        public StatusEventArgs(string message) { this._message = message; }
        public string Message { get { return this._message; } set { this._message = value; } }
    }
    	
	namespace Dispatch {		
		//TsortNode creates objects that can reside in a TreeView control (i.e. TreeNode)
		//and also provide custom application functionality when selected
		public abstract class TsortNode: TreeNode {
			//Members
			protected TreeNode[] mChildNodes=null;
			
			//Operations
			public TsortNode(): this("Node",App.ICON_CLOSED, App.ICON_OPEN) { }
			public TsortNode(string text, int imageIndex, int selectedImageIndex) {
				//Constructor
				try {
					//Set members and base node members
					this.Text = text.Trim();
					this.ImageIndex = imageIndex;
					this.SelectedImageIndex = selectedImageIndex;
				}
				catch(Exception ex) { throw ex; }
			}
			public void ExpandNode() { 
				//Load [visible] child nodes for each child
				if(this.mChildNodes!=null) {
					foreach(TsortNode node in this.mChildNodes) 
						node.LoadChildNodes();
				}
			}
			public void CollapseNode() { 
				//Unload [hidden] child nodes for each child
				if(this.mChildNodes!=null) {
					foreach(TsortNode node in this.mChildNodes) 
						node.UnloadChildNodes();
				}
			}
			public virtual void LoadChildNodes() { return; }
			public virtual void UnloadChildNodes() { base.Nodes.Clear(); this.mChildNodes = null; }
			public abstract bool CanOpen { get; }
			public abstract void Properties();
		}
	}
}