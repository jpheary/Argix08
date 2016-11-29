using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Argix.Windows;

namespace Argix {
    //The main entry point for the application
    static class Program {
        [STAThread]
        static void Main() {
            //Application entry point
            try {
                //Start app
                Process appInstance = AppServices.RunningInstance("Argix Logistics " + App.Product);
                if(appInstance == null) {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Argix.MIS.frmMain());
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
		public const int ICON_ARGIX = 0;
		public const int ICON_TERMINAL = 1;
		public const int ICON_SCANNER = 2;
		
		//Interface
		static App() { }
		private App() { }
		public static string EventLogName { get { return "Argix08"; } }
        public static void ReportError(Exception ex) { ReportError(ex,true); }
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
            }
            catch(Exception) { }
        }
    }
	
	namespace MIS {
		//TsortNode creates objects that can reside in a TreeView control (i.e. TreeNode)
		//and also provide custom application functionality when selected
		public abstract class TsortNode: TreeNode {
			//Members
			protected TreeNode[] mChildNodes=null;

			//Interface
			public TsortNode(string text, int imageIndex, int selectedImageIndex) {
				//Constructor
				try {
					//Set members and base node members
					this.Text = text.Trim();
					this.ImageIndex = imageIndex;
					this.SelectedImageIndex = selectedImageIndex;
				}
				catch(Exception ex) { throw new ApplicationException("Unexpected error while creating (abstract) Tsort Node instance.", ex); }
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
		}

    }
}