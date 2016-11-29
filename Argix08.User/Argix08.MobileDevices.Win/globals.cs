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
using System.ServiceModel;
using System.Windows.Forms;
using Argix.Terminals;
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
                    Application.Run(new Argix.Terminals.winMain());
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
    public class App:AppBase {
        //Members
        private static MobileDevicesConfiguration _Config = null;

        //Interface
        static App() {
            //Class constructor: get application configuration
            try {
                _Config = new MobileDevicesConfiguration();
            }
            catch(Exception ex) { ReportError(ex); Application.Exit(); }
        }
        private App() { }
        public static MobileDevicesConfiguration Config { get { return _Config; } }
        public static void ShowConfig() {
            dlgLogin login = new dlgLogin(_Config.MISPassword);
            login.ValidateEntry();
            if(login.IsValid) new Argix.Support.dlgConfig(_Config).ShowDialog();
        }
        public static void Trace(string message,LogLevel level) {
            //Trace
            if(level >= _Config.TraceLevel) {
                TraceMessage m = new TraceMessage();
                m.Name = "Argix08";
                m.Source = App.Product;
                m.User = Environment.UserName;
                m.Computer = Environment.MachineName;
                m.LogLevel = level;
                m.Message = message;
                MobileDevicesProxy.WriteLogEntry(m);
            }
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

    public class MobileDevicesConfiguration {
        //Members
        private UserConfiguration mConfig=null;
        public MobileDevicesConfiguration() {
            //Constructor
            this.mConfig = MobileDevicesProxy.GetUserConfiguration(App.Product,new string[] { Environment.UserName,Environment.MachineName });
        }
        [Category("Accessibility"),Description("Gets the password to MIS-related services.")]
        internal string MISPassword { get { return this.mConfig["MISPassword"].ToString(); } }
        [Category("Accessibility"),Description("Gets or sets read-only rights.")]
        public bool ReadOnly { get { return Convert.ToBoolean(this.mConfig["ReadOnly"]); } set { this.mConfig["ReadOnly"] = value.ToString(); } }
        [Category("Behavior"),Description("Gets or sets the application trace logging level.")]
        public LogLevel TraceLevel {
            get {
                switch(Convert.ToInt32(this.mConfig["TraceLevel"])) {
                    case 0: return LogLevel.None;
                    case 1: return LogLevel.Debug;
                    case 2: return LogLevel.Information;
                    case 3: return LogLevel.Warning;
                    case 4: return LogLevel.Error;
                    default: return LogLevel.Warning;
                }
            }
            set {
                switch(value) {
                    case LogLevel.None: this.mConfig["TraceLevel"] = "0"; break;
                    case LogLevel.Debug: this.mConfig["TraceLevel"] = "1"; break;
                    case LogLevel.Information: this.mConfig["TraceLevel"] = "2"; break;
                    case LogLevel.Warning: this.mConfig["TraceLevel"] = "3"; break;
                    case LogLevel.Error: this.mConfig["TraceLevel"] = "4"; break;
                }
            }
        }
        
        [Category("Accessibility"),Description("Gets or sets the battery In-Service date access. When true, In-Service dates can be applied to batteries.")]
        public bool AllowBatteryInService { get { return Convert.ToBoolean(this.mConfig["AllowBatteryInService"]); } set { this.mConfig["AllowBatteryInService"] = value.ToString(); } }
        [Category("Accessibility"),Description("Gets or sets terminal change access for mobile items. When true, device items and bettery items can be moved between terminals.")]
        public bool AllowTerminalChange { get { return Convert.ToBoolean(this.mConfig["AllowTerminalChange"]); } set { this.mConfig["AllowTerminalChange"] = value.ToString(); } }

        [Category("Setup"),Description("Gets the Windows barcode labeler name (i.e. Brother III).")]
        public string PrinterName { get { return this.mConfig["PrinterName"].ToString(); } }
        [Category("Setup"),Description("Gets the barcode labeler font name.")]
        public string PrinterFontName { get { return this.mConfig["PrinterFontName"].ToString(); } }
        [Category("Setup"),Description("Gets the barcode labeler font size.")]
        public int PrinterFontSize { get { return Convert.ToInt32(this.mConfig["PrinterFontSize"]); } }
        [Category("Setup"),Description("Gets the Barcode128 subset algorythm for barcode label printing.")]
        public string Barcode128Subset { get { return this.mConfig["Barcode128Subset"].ToString(); } }
        [Category("Setup"),Description("Gets the local terminal ID of the current user.")]
        public long LocalTerminalID { get { return Convert.ToInt64(this.mConfig["LocalTerminalID"]); ; } }

        [Category("Data"),Description("Gets the number of characters in a device item barcode.")]
        public int DeviceScanLength { get { return Convert.ToInt32(global::Argix.Properties.Settings.Default.DeviceScanLength); ; } }
        [Category("Data"),Description("Gets the number of characters in a battery item barcode.")]
        public int BatteryScanLength { get { return Convert.ToInt32(global::Argix.Properties.Settings.Default.BatteryScanLength); ; } }
        [Category("Data"),Description("Gets the number of hours of expected runtime for an available battery.")]
        public int BatteryRunTimeAvailable { get { return Convert.ToInt32(global::Argix.Properties.Settings.Default.BatteryRunTimeAvailable); ; } }
        [Category("Data"),Description("Gets the number of hours of expected runtime for an issued battery.")]
        public int BatteryRunTimeIssued { get { return Convert.ToInt32(global::Argix.Properties.Settings.Default.BatteryRunTimeIssued); ; } }
        [Category("Data"),Description("Gets the number of hours of runtime that begins end of life warnings for an issued battery.")]
        public int BatteryRunTimeWarning { get { return Convert.ToInt32(global::Argix.Properties.Settings.Default.BatteryRunTimeWarning); ; } }
        [Category("Data"),Description("Gets the maximum number of battery assignments for a single driver.")]
        public int AssignmentsMax { get { return Convert.ToInt32(global::Argix.Properties.Settings.Default.AssignmentsMax); ; } }
    }
}
