//	File:	main.cs
//	Author:	J. Heary
//	Date:	10/14/08
//	Desc:	Utility interface and scheduler.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Argix.Windows;

namespace Argix {
	//
	public class frmMain : System.Windows.Forms.Form {
		//Members
        private PCSFileSvc mService = null;
		private TrayIcon mIcon=null;		//Task tray icon when using internal scheduling
		
		private const int CTX_OPEN = 0, CTX_PAUSE = 1, CTX_CONTINUE = 2, CTX_STOP = 3, CTX_SEP1 = 4, CTX_LOG = 5;
		private System.ComponentModel.Container components = null;
		
		//Interface
        public frmMain(PCSFileSvc pcsFileSvc) {
			//Constructor
			try {
				//Required for designer support
				InitializeComponent();
                this.mService = pcsFileSvc;
			}
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new frmMain instance.",ex); }
		}
		protected override void Dispose(bool disposing) { if(disposing) { if(components != null) components.Dispose(); } base.Dispose(disposing); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmMain));
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(115, 88);
			this.ControlBox = false;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmMain";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.Closed += new System.EventHandler(this.OnFormClosed);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load
			try {
				//Hide
				this.Visible = false;
                this.mService.Start();
			}
			catch(Exception ex) { SvcLog.LogMessage("STARTUP ERROR\t" + ex.Message); }
		}
		private void OnFormClosed(object sender, System.EventArgs e) {
			//Event handler for form closed
			try {
                //Close task tray icon if applicable; log application as stopped
				if(this.mIcon != null) this.mIcon.Visible = false;
				this.Visible = false;
				SvcLog.LogMessage("UTILTIY STOPPED");
			}
			catch(Exception) { }
            finally { Application.Exit(); }
        }
        #region Tasktray Icon: newTrayIcon(), OnMenuClick(), OnIconDoubleClick()
        private TrayIcon newTrayIcon(string title, Icon icon) {
			//Create a new instance of a task tray icon complete with context menu
			TrayIcon oIcon=null;
			try {
				oIcon = new TrayIcon(title, icon);
				MenuItem ctxOpen = new MenuItem("Open", new System.EventHandler(this.OnMenuClick));
				ctxOpen.Index = CTX_OPEN;
				ctxOpen.Enabled = false;
				ctxOpen.DefaultItem = true;
				ctxOpen.Visible = false;
				MenuItem ctxPause = new MenuItem("Pause", new System.EventHandler(this.OnMenuClick));
				ctxPause.Index = CTX_PAUSE;
				ctxPause.Enabled = false;
                ctxPause.Visible = true;
				MenuItem ctxContinue = new MenuItem("Continue", new System.EventHandler(this.OnMenuClick));
				ctxContinue.Index = CTX_CONTINUE;
				ctxContinue.Enabled = false;
                ctxContinue.Visible = true;
				MenuItem ctxStop = new MenuItem("Stop", new System.EventHandler(this.OnMenuClick));
				ctxStop.Index = CTX_STOP;
				ctxStop.Enabled = true;
                ctxStop.Visible = true;
				MenuItem ctxSep1 = new MenuItem("-", new System.EventHandler(this.OnMenuClick));
				ctxSep1.Index = CTX_SEP1;
				//ctxSep1.Enabled = true;
                ctxSep1.Visible = true;
				MenuItem ctxLog = new MenuItem("View Log", new System.EventHandler(this.OnMenuClick));
				ctxLog.Index = CTX_LOG;
				ctxLog.Enabled = true;
				oIcon.MenuItems.AddRange(new MenuItem[] {ctxOpen, ctxPause,ctxContinue,ctxStop,ctxSep1,ctxLog});
				oIcon.DoubleClick += new System.EventHandler(OnIconDoubleClick);
			}
			catch(Exception) { }
			return oIcon;
		}
		private void OnMenuClick(object sender, System.EventArgs e) {
			//Event handler for user clicks on the notify icon
			try  {
				MenuItem menu = (MenuItem)sender;
				switch(menu.Index)  {
					case CTX_OPEN:      break;
					case CTX_PAUSE:     this.mService.Pause(); break;
					case CTX_CONTINUE:  this.mService.Continue(); break;
					case CTX_STOP:      this.mService.Stop(); break;
					case CTX_SEP1: break;
					case CTX_LOG:
                        Process myProcess = Process.Start("Notepad",SvcLog.CurrentLogFile);
						break;
                }
			}
            catch(Exception ex) { reportError(ex,true,false); }
            finally { setServices(); }
		}
		private void OnIconDoubleClick(object Sender, EventArgs e) {
			//Event handler for user double clicks on the notify icon
		}
        #endregion
        #region Local Services: setServices(), reportError()
        private void setServices() {
			//Set menu states
			try {
				if(this.mIcon != null) {
					this.mIcon.MenuItems[CTX_OPEN].Enabled = false;
                    this.mIcon.MenuItems[CTX_PAUSE].Enabled = false; // this.mTimer.Enabled;
                    this.mIcon.MenuItems[CTX_CONTINUE].Enabled = false; //!this.mTimer.Enabled;
					this.mIcon.MenuItems[CTX_STOP].Enabled = true;
					this.mIcon.MenuItems[CTX_LOG].Enabled = true;
				}
			}
            catch(Exception ex) { reportError(ex,false,true); }
        }
        private void reportError(Exception ex,bool displayMessage,bool logMessage) {
            //Report an exception to the user
            try {
                string src = (ex.Source != null) ? ex.Source + "-\n" : "";
                string msg = src + ex.Message;
                if(ex.InnerException != null) {
                    if((ex.InnerException.Source != null)) src = ex.InnerException.Source + "-\n";
                    msg = src + ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
                }
                if(displayMessage)
                    MessageBox.Show(msg,"PCS File Utility",MessageBoxButtons.OK,MessageBoxIcon.Error);
                if(logMessage)
                    SvcLog.LogMessage("ERROR\t" + ex.ToString());
            }
            catch(Exception) { }
        }
        #endregion
	}
}
