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

namespace Tsort05.Utility {
	//
	public class frmMain : System.Windows.Forms.Form {
		//Members
		private FileSvc[] mSvcs=null;		//Array of service instances; one per configuration entry
		private Timer mTimer=null;			//Timer for scheduled operation
		private bool mUseTimer=false;		//Scheduler flag
		private bool mStartup=true;			//Startup flag
		private bool mShutdown=false;		//Shutdown flag
		private TrayIcon mIcon=null;		//Task tray icon when using internal scheduling
		
		private const string KEY_TIMER_ENABLED = "TimerOn";
		private const string KEY_LOG_ENABLED = "LogOn";
		private const string KEY_LOG_PATH = "LogPath";
		private const string KEY_LOG_FILEMAX = "LogFilesMax";
		private const int CTX_OPEN = 0, CTX_PAUSE = 1, CTX_CONTINUE = 2, CTX_STOP = 3, CTX_SEP1 = 4, CTX_LOG = 5;
		
		private System.ComponentModel.Container components = null;
		
		//Interface
		public frmMain() {
			//Constructor
			try {
				//Required for designer support
				InitializeComponent();
				
				//Read configuration parameters for scheduler and log
				try {
					this.mUseTimer = Convert.ToBoolean(ConfigurationManager.AppSettings.Get(KEY_TIMER_ENABLED));
                    FileLog.Enabled = Convert.ToBoolean(ConfigurationManager.AppSettings.Get(KEY_LOG_ENABLED));
                    FileLog.FilePath = ConfigurationManager.AppSettings.Get(KEY_LOG_PATH);
                    FileLog.FileCount = Convert.ToInt32(ConfigurationManager.AppSettings.Get(KEY_LOG_FILEMAX));
				}
				catch(Exception) { }
				
				if(this.mUseTimer) {
					//Create timer for internally scheduled operations
					this.mTimer = new Timer();
					this.mTimer.Enabled = false;
					this.mTimer.Tick += new EventHandler(onIntervalExpired);
					
					//Create task tray icon for user interaction
					this.mIcon = newTrayIcon("File Utility", this.Icon);
				}
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
		
		[STAThread]
		static void Main(string[] args) {
			//The main entry point for the application
            try {
                //Run a single instance of this utility
                Process p = AppServices.RunningInstance();
                if(p == null)
                    Application.Run(new frmMain());
                else {
                    MessageBox.Show("Another instance of this application is running.","File Utility");
                    AppServices.ShowWindow(p.MainWindowHandle,1);
                    AppServices.SetForegroundWindow(p.MainWindowHandle);
                }
            }
            catch(Exception ex) {
                MessageBox.Show("FATAL ERROR\n\n" + ex.ToString() + "\n\n Application will be closed. Please contact the IT department for help.","File Utility",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Application.Exit();
            }
        }		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load
			try {
				//Hide
				this.Visible = false;
				
				//Timed or one shot
				if(this.mUseTimer) {
					//Log application as started; start the scheduler
					FileLog.LogMessage("UTILTIY STARTED (TIMER=" + getTimerInterval().ToString() + "msec)");
					this.mStartup = true;
					this.mTimer.Interval = 100;
					this.mTimer.Enabled = true;
				}
				else {
					//Log application as started; create service instances; run one shot and shutdown
					FileLog.LogMessage("UTILTIY STARTED (TIMER=OFF)");
					initServices();
					for(int i=0; i<this.mSvcs.Length; i++) {
						FileSvc oSvc = this.mSvcs[i];
						oSvc.Execute();
					}
					this.Close();
				}
			}
			catch(Exception ex) { FileLog.LogMessage("STARTUP ERROR\t" + ex.Message); }
		}
		private void OnFormClosed(object sender, System.EventArgs e) {
			//Event handler for form closed
            try {
                //Close task tray icon if applicable; log application as stopped
                if(this.mIcon != null) this.mIcon.Visible = false;
                this.Visible = false;
                FileLog.LogMessage("UTILTIY STOPPED");
            }
            catch(Exception) { }
            finally { Application.Exit(); }
		}
		private void initServices() {
			//Create service instances based upon the configuration file
			try {
				//Read configuration from app.config
				IDictionary oDict = (IDictionary)ConfigurationManager.GetSection("dirs");
				int dirs = Convert.ToInt32(oDict["count"]);
				this.mSvcs = new FileSvc[dirs];
				for(int i=1; i<=dirs; i++) {
                    oDict = (IDictionary)ConfigurationManager.GetSection("dir" + i.ToString());
					string src = oDict["src"].ToString();
					string pattern = oDict["pattern"].ToString();
					string dest = oDict["dest"].ToString();
					bool move = Convert.ToBoolean(oDict["move"]);
					//Debug.Write("src=" + src + "pattern=" + pattern + "; dest=" + dest + "; move=" + move + "\n");
					FileSvc oSvc = new FileSvc(src, pattern, dest,move);
					this.mSvcs[i-1] = oSvc;
				}
			}
            catch(Exception ex) { reportError(ex,false,true); }
        }
        #region Scheduler: onIntervalExpired(), getTimerInterval()
        private void onIntervalExpired(object sender, EventArgs e) {
			//Event handler for self timed operation
			try {
				if(this.mStartup) {
					//Set startup condition- create utility service
					this.mStartup = false;
					this.mTimer.Interval = getTimerInterval();
					initServices();
				}
				if(!this.mShutdown) {
					//Run the service
					this.mIcon.Text = "File Utility running...";
					for(int i=0; i<this.mSvcs.Length; i++) {
						FileSvc oSvc = this.mSvcs[i];
						oSvc.Execute();
					}
				}
				else {
					//Shutdown condition
					this.mTimer.Enabled = false;
					this.Close();
				}
			}
			catch(Exception ex) { FileLog.LogMessage("TIMER ERROR\t" + ex.Message); }
			finally { setServices(); }
		}
		private int getTimerInterval() {
			//Read configuration timer parameter
			int timerInterval=600000;
			try {
				timerInterval = Convert.ToInt32(ConfigurationManager.AppSettings.Get("TimerInterval"));
			}
			catch(Exception ex) { FileLog.LogMessage("CONFIGURATION ERROR (TIMER)\t" + ex.Message); }
			return timerInterval;
		}
		#endregion
        #region Tasktray Icon: newTrayIcon(), OnMenuClick(), OnIconDoubleClick()
        private TrayIcon newTrayIcon(string title,Icon icon) {
			//Create a new instance of a task tray icon complete with context menu
			TrayIcon trayIcon=null;
			try {
				trayIcon = new TrayIcon(title, icon);
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
				trayIcon.MenuItems.AddRange(new MenuItem[] {ctxOpen, ctxPause,ctxContinue,ctxStop,ctxSep1,ctxLog});
				trayIcon.DoubleClick += new System.EventHandler(OnIconDoubleClick);
			}
            catch(Exception ex) { reportError(ex,false,true); }
			return trayIcon;
		}
		private void OnMenuClick(object sender, System.EventArgs e) {
			//Event handler for user clicks on the notify icon
			try  {
				MenuItem menu = (MenuItem)sender;
				switch(menu.Index)  {
					case CTX_OPEN: break;
					case CTX_PAUSE: 
						this.mTimer.Enabled = false;
						this.mIcon.Text = "File Utility paused...";
						break;
					case CTX_CONTINUE: 
						this.mStartup = true;
						this.mTimer.Enabled = true;
						this.mIcon.Text = "File Utility restarting...";
						break;
					case CTX_STOP: 
						this.mShutdown = true;
						this.mTimer.Enabled = false;
						this.mTimer.Interval = 100;
						this.mTimer.Enabled = true;
						break;
					case CTX_SEP1: break;
					case CTX_LOG: 
						Process myProcess = Process.Start("Notepad", FileLog.Filename);
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
					this.mIcon.MenuItems[CTX_PAUSE].Enabled = this.mTimer.Enabled;
					this.mIcon.MenuItems[CTX_CONTINUE].Enabled = !this.mTimer.Enabled;
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
                    MessageBox.Show(msg,App.Product,MessageBoxButtons.OK,MessageBoxIcon.Error);
                if(logMessage)
                    ArgixTrace.WriteLine(new TraceMessage(ex.ToString(),App.EventLogName,level));
            }
            catch(Exception) { }
        }
		#endregion
	}
}
