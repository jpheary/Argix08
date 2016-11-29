using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Argix {
    //
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main() {
            //
            try {
                Application.Run(new Argix.Freight.Main());
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
                MessageBox.Show(msg,"Scanner Sort",MessageBoxButtons.OK,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
            }
            catch(Exception) { }
        }
    }
}