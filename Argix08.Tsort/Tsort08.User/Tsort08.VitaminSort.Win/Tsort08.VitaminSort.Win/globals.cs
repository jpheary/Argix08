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
    }

    //Global application object
    public class App:AppBase {
        //Members
        //private static UserConfiguration _Config = null;

        //Interface
        static App() {
            //Class constructor: get application configuration
            try {
                //ConfigurationClient configClient = new ConfigurationClient();
                //_Config = configClient.GetUserConfiguration2(App.Product,new string[] { Environment.UserName,Environment.MachineName });
                //configClient.Close();
            }
            catch(Exception ex) { ReportError(ex,true); Application.Exit(); }
        }
        private App() { }
        //public static UserConfiguration Config { get { return _Config; } }
        //public static void ShowConfig() {
        //    //
        //    string uri = new ConfigurationClient().Endpoint.Address.Uri.Host;
        //    Process.Start(System.Web.HttpUtility.HtmlDecode("http://" + uri + "/Argix08.AppService/ConfigurationEditor.aspx?app=" + App.Product));
        //}
        public static void ShowDiagnostics() { }
        //public static void ShowTrace() {
        //    string uri = new AppLoggerClient().Endpoint.Address.Uri.Host;
        //    Process.Start(System.Web.HttpUtility.HtmlDecode("http://" + uri + "/Argix08.AppService/AppLoggerEditor.aspx?log=Argix08&src=" + App.Product));
        //}
        //public static void Trace(string message,LogLevel level) {
        //    //Trace
        //    TraceMessage m = new TraceMessage();
        //    m.Name = "Argix08";
        //    m.Source = App.Product;
        //    m.User = Environment.UserName;
        //    m.Computer = Environment.MachineName;
        //    m.LogLevel = level;
        //    m.Message = message;
        //    AppLoggerClient appLog = new AppLoggerClient();
        //    try {
        //        appLog.Write(m);
        //        appLog.Close();
        //    }
        //    catch(TimeoutException ex) { appLog.Abort(); ReportError(ex,true,LogLevel.None); }
        //    catch(CommunicationException ex) { appLog.Abort(); ReportError(ex,true,LogLevel.None); }
        //    catch(Exception ex) { appLog.Abort(); }
        //}
        //public static void ReportError(Exception ex) { ReportError(ex,true,LogLevel.None); }
        //public static void ReportError(Exception ex,bool displayMessage) { ReportError(ex,displayMessage,LogLevel.None); }
        public static void ReportError(Exception ex,bool displayMessage) {
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
                //if(level != LogLevel.None)
                //    Trace(ex.ToString(),level);
            }
            catch(Exception) { }
        }

        public static string MISPassword { get { return "letmein"; } }
        public static bool ReadOnly { get { return false; } }
        public static int TraceLevel { get { return 4; } }
    }
}
