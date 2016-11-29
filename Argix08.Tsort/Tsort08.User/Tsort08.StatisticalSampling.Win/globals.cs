using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Deployment.Application;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
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
                    Application.Run(new Argix.Freight.frmMain());
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
    public class App :AppBase {
        private static Mediator _Mediator = null;
        private static Config _Config = null;
		
		//Global configuration and database constants
        public const string USP_VENDOR_GETLIST = "uspStatSampleVendorGetList",TBL_VENDOR = "VendorTable";
        public const string USP_VENDOR_NEW = "uspStatSampleVendorNew";
        public const string USP_VENDOR_UPDATE = "uspStatSampleVendorUpdate";
        public const string USP_CARTON_READ = "uspStatSampleHeaderGet",TBL_CARTON_READ = "HeaderTable";
        public const string USP_CARTON_NEW = "uspStatSampleHeaderNew";
        public const string USP_ISBN_NEW = "uspStatSampleDetailNew";
        public const string USP_SAMPLES_GETLIST = "uspStatSampleGet",TBL_SAMPLES_GETLIST = "SampleTable";
        public const string USP_SAMPLES_UPDATE = "uspStatSampleUpdate";

        [DllImport("kernel32.dll")]
        public static extern bool Beep(int freq,int duration);
        [DllImport("user32.dll",SetLastError = true)]
        public static extern bool MessageBeep(MessageBeepType type);

		//Interface
        static App() {
            //Class constructor: get application configuration
            try {
                _Mediator = (Mediator)new SQLMediator(global::Argix.Properties.Settings.Default.SQLConnection);
                _Config = new Config();
            }
            catch(Exception ex) { ReportError(ex); Application.Exit(); }
        }
        internal static Mediator Mediator { get { return _Mediator; } }
        internal static Config Config { get { return _Config; } }
        internal static void ShowConfig() {
            new dlgConfig().ShowDialog();
        }
        public static void ReportError(Exception ex) { ReportError(ex, true); }
        public static void ReportError(Exception ex,bool displayMessage) {
            //Report an exception to the user
            try {
                string src = (ex.Source != null) ? ex.Source + "-\n" : "";
                string msg = src + ex.Message;
                if(ex.InnerException != null) {
                    if((ex.InnerException.Source != null)) src = ex.InnerException.Source + "-\n";
                    msg = src + ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
                }
                if(displayMessage) {
                    Beep(250,2000);
                    //MessageBeep(MessageBeepType.Error);
                    //MessageBox.Show(msg, App.Product, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MsgBox.Show(msg);
                }
            }
            catch(Exception) { }
        }
    }

    internal class Config {
        //Members

        //Interface
        public Config() { }

        [Category("Data"), Description("Length of a carton number string.")]
        public int CartonNumberLength {
            get { return global::Argix.Properties.Settings.Default.CartonNumberLength; }
        }
        [Category("Data"), Description("Length od an EAN scan string.")]
        public int EANScanSize {
            get { return global::Argix.Properties.Settings.Default.EANScanSize; }
        }
        [Category("Data"), Description("Length od an ISBN scan string.")]
        public int ISBNScanSize {
            get { return global::Argix.Properties.Settings.Default.ISBNScanSize; }
        }
        [Category("Data"), Description("Length od an UPC scan string.")]
        public int UPCScanSize {
            get { return global::Argix.Properties.Settings.Default.UPCScanSize; }
        }
        [Category("Data"), Description("Maximum number of books.")]
        public int BookCountMax {
            get { return global::Argix.Properties.Settings.Default.BookCountMax; }
        }
        [Category("Data"), Description("Prefix identifier for an EAN scan.")]
        public string EANPrefix {
            get { return global::Argix.Properties.Settings.Default.EANPrefix; }
        }
    
        [Category("Behavior"), Description("FTP server name.")]
        public string FTPServername {
            get { return global::Argix.Properties.Settings.Default.FTPServername.Trim(); }
        }
        [Category("Security"), Description("FTP server username.")]
        public string FTPUsername {
            get { return global::Argix.Properties.Settings.Default.FTPUsername.Trim(); }
        }
        [Category("Security"), Description("FTP server password.")]
        public string FTPPassword {
            get { return global::Argix.Properties.Settings.Default.FTPPassword.Trim(); }
        }
        [Category("Behavior"), Description("FTP server remote path.")]
        public string FTPRemotePath {
            get { return global::Argix.Properties.Settings.Default.FTPRemotePath.Trim(); }
        }
    }
    public class WorkflowException : Exception {
		public WorkflowException() : base() { }
		public WorkflowException(string message) : base(message) { }
		public WorkflowException(string message, Exception innerException) : base(message, innerException) { }
	}
    public enum MessageBeepType {
        Default = -1,
        Ok = 0x00000000,
        Error = 0x00000010,
        Question = 0x00000020,
        Warning = 0x00000030,
        Information = 0x00000040,
    }
}