//	File:	globals.cs
//	Author:	J. Heary
//	Date:	01/07/09
//	Desc:	Global aplication object; enumerators, event support, exceptions, etc.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Deployment.Application;
using System.Reflection;
using System.ServiceModel;
using System.Windows.Forms;
using Argix.Support;
using Argix.Windows;

namespace Argix {
    /// <summary>The main entry point for the application.</summary>
    static class Program {
        //Members

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
                    Application.Run(new Argix.Customers.frmMain());
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

	//Global application object
	public class App: AppBase {
		//Members
        private static IssueMgtConfiguration _Config = null;

		//Interface
        static App() {
            //Class constructor: get application configuration
            try {
                _Config = new IssueMgtConfiguration();
            }
            catch(Exception ex) { ReportError(ex); Application.Exit(); }
        }
        private App() { }
        public static IssueMgtConfiguration Config { get { return _Config; } }
        public static void ShowConfig() { new dlgConfig(_Config).ShowDialog(); }
        public static void Trace(string message,LogLevel level) {
            //Trace
            TraceMessage m = new TraceMessage();
            m.Name = "Argix08";
            m.Source = App.Product;
            m.User = Environment.UserName;
            m.Computer = Environment.MachineName;
            m.LogLevel = level;
            m.Message = message;
            AppServiceClient appLog = new AppServiceClient();
            try {
                appLog.WriteLogEntry(m);
                appLog.Close();
            }
            catch(TimeoutException ex) { appLog.Abort(); ReportError(ex,true,LogLevel.None); }
            catch(CommunicationException ex) { appLog.Abort(); ReportError(ex,true,LogLevel.None); }
            catch(Exception ex) { appLog.Abort(); ReportError(ex,true,LogLevel.None); }
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

    public class IssueMgtConfiguration {
        //Members
        private UserConfiguration mUConfig=null;
        
        //Interface
        public IssueMgtConfiguration() {
            //Constructor
            this.mUConfig = new UserConfiguration();
            AppServiceClient configClient = new AppServiceClient();
            UserConfiguration c = configClient.GetUserConfiguration2(App.Product,new string[] { Environment.UserName,Environment.MachineName });
            foreach(KeyValuePair<object,object> e in c) { this.mUConfig.Add(e.Key,e.Value); }
            configClient.Close();
        }
        //[Category("Accessibility"),Description("Password to MIS-related services.")]
        internal string MISPassword { get { return this.mUConfig["MISPassword"].ToString(); } }
        [Category("Accessibility"),Description("Read only rights.")]
        public bool ReadOnly { get { return Convert.ToBoolean(this.mUConfig["ReadOnly"]); } }
        [Category("Behavior"),Description("Application trace logging level.")]
        public int TraceLevel { get { return Convert.ToInt32(this.mUConfig["TraceLevel"]); } }
        [Category("Behavior"),Description("Gets or sets the the number of days for issue header display.")]
        public int IssueDaysBack { get { return Convert.ToInt32(this.mUConfig["IssueDaysBack"]); } }
        [Category("Behavior"),Description("Gets or sets the location of a Temp folder for file attachments.")]
        public string TempFolder { get { return this.mUConfig["TempFolder"].ToString(); } set { this.mUConfig["TempFolder"] = value; } }
        [Category("Behavior"),Description("Gets or sets the auto refresh feature.")]
        public bool AutoRefreshOn { get { return Convert.ToBoolean(this.mUConfig["AutoRefreshOn"]); } }
    }

    public delegate void ControlErrorEventHandler(object source,ControlErrorEventArgs e);
    public class ControlErrorEventArgs:EventArgs {
        private Exception _ex = null;
        public ControlErrorEventArgs(Exception ex) { this._ex = ex; }
        public Exception Exception { get { return this._ex; } set { this._ex = value; } }
    }

    public class ControlException:ApplicationException {
        public ControlException() : this("Unspecified control exception.") { }
        public ControlException(string message) : base(message) { }
        public ControlException(string message,Exception innerException) : base(message,innerException) { }
    }

    public delegate void StatusEventHandler(object source,StatusEventArgs e);
    public class StatusEventArgs:EventArgs {
        private string _message = "";
        public StatusEventArgs(string message) { this._message = message; }
        public string Message { get { return this._message; } set { this._message = value; } }
    }

    namespace Customers {
        public delegate void NewIssueEventHandler(object source,NewIssueEventArgs e);
        public class NewIssueEventArgs:EventArgs {
            private Issue _issue = null;
            private Action _action = null;
            public NewIssueEventArgs(Issue issue, Action action) { this._issue = issue; this._action = action; }
            public Issue Issue { get { return this._issue; } set { this._issue = value; } }
            public Action Action { get { return this._action; } set { this._action = value; } }
        }
        
        public delegate void ContactEventHandler(object source,ContactEventArgs e);
        public class ContactEventArgs:EventArgs {
            private Contact _contact = null;
            public ContactEventArgs(Contact contact) { this._contact = contact; }
            public Contact Contact { get { return this._contact; } set { this._contact = value; } }
        }
    }
}