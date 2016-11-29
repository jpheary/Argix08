using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace Argix {
    //
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main() {
            //Validate the database exists
            //If the local database doesn't exist, the app requires initilization
            try {
                using(SqlCeConnection conn = new SqlCeConnection(Settings.Default.LocalConnectionString)) {
                    if(!System.IO.File.Exists(conn.Database)) {
                        DialogResult result = MessageBox.Show("The application requires a first time sync to continue.  Would you like to Sync Now?","Fist Time Run",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
                        if(result == DialogResult.OK) {
                            try {
                                Cursor.Current = Cursors.WaitCursor;
                                Cursor.Show();
                                ScannerCacheSyncAgent syncAgent = new ScannerCacheSyncAgent(new ScannerCacheSyncServiceProxy(Settings.Default.WebServiceURL));
                                syncAgent.GPSData.SyncDirection = Microsoft.Synchronization.Data.SyncDirection.UploadOnly;
                                syncAgent.GPSData.CreationOption = Microsoft.Synchronization.Data.TableCreationOption.DropExistingOrCreateNewTable;
                                syncAgent.Synchronize();
                            }
                            catch(Exception ex) { App.ReportError(ex); }
                            finally { Cursor.Current = Cursors.Default; }
                        }
                        else
                            return;
                    }
                }
                Application.Run(new Main());
            }
            catch(Exception ex) { App.ReportError(ex); Application.Exit(); }
        }
    }

    //Global application object
    public class App {
        //Members

        //Interface
        static App() {
            //Class constructor: get application configuration
            try {
            }
            catch(Exception ex) { ReportError(ex); Application.Exit(); }
        }
        private App() { }
        public static void ReportError(Exception ex) { 
            //Report an exception to the user
            try {
                string msg = ex.Message;
                if(ex.InnerException != null) {
                    msg = ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
                }
                MessageBox.Show(msg,"Driver Scan",MessageBoxButtons.OK,MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            catch(Exception) { }
        }
    }
}