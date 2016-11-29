using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using Argix;
using Argix.Data;
using Argix.Windows;

namespace Argix {
    //Global application object
    static class Program {
        //Members
        //Interface
        [STAThread]
        static void Main() {
            //Application entry point
            try {
                //Start app
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Argix.MIS.frmMain());
            }
            catch(Exception ex) {
                MessageBox.Show("FATAL ERROR\n\n" + ex.ToString(),App.Product,MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }

    public class App: AppBase {
		//Members		
		public const int ICON_OPEN = 9;
		public const int ICON_CLOSED = 9;
		public const int ICON_APP = 11;
		
		//Interface
		static App() { }
		private App() { }
		public static string EventLogName { get { return "Argix08"; } }
		public static string RegistryKey { get { return "AppSupport"; } }
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
	
	namespace MIS {
		//AppNode creates objects that can reside in a TreeView control (i.e. TreeNode)
		//and also provide custom application functionality when selected
		public abstract class AppNode: TreeNode {
			//Members, Constants, Events
			protected TreeNode[] mChildNodes=null;
			
			//Interface
            public AppNode(string text,int imageIndex,int selectedImageIndex) {
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
                    foreach(AppNode node in this.mChildNodes) 
						node.LoadChildNodes();
				}
			}
			public void CollapseNode() { 
				//Unload [hidden] child nodes for each child
				if(this.mChildNodes!=null) {
					foreach(AppNode node in this.mChildNodes) 
						node.UnloadChildNodes();
				}
			}
			public virtual void LoadChildNodes() { return; }
			public virtual void UnloadChildNodes() { base.Nodes.Clear(); this.mChildNodes = null; }
			public abstract void Refresh();
		}
	}
	
	//Event support
	public delegate void ErrorEventHandler(object source, ErrorEventArgs e);
	public class ErrorEventArgs : EventArgs {
		private Exception _ex=null;
		private bool _displayMessage=false;
		private LogLevel _logLevel=LogLevel.None; 
		public ErrorEventArgs(Exception ex): this(ex, false, LogLevel.None) {}
		public ErrorEventArgs(Exception ex, bool displayMessage, LogLevel logLevel) {
			this._ex = ex;
			this._displayMessage = displayMessage;
			this._logLevel = logLevel;
		}
		public Exception Exception { get { return this._ex; } set { this._ex = value; } }
		public bool DisplayMessage { get { return this._displayMessage; } set { this._displayMessage = value; } }
		public LogLevel Level { get { return this._logLevel; } set { this._logLevel = value; } }
	}
}