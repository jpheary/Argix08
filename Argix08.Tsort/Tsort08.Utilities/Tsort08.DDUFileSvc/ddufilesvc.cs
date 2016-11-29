//	File:	ddufilesvc.cs
//	Author:	J. Heary
//	Date:	11/05/08
//	Desc:	.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.ServiceProcess;
using System.Windows.Forms;
using Argix.Windows;

namespace Tsort05.Utility {
	//
	public class DDUFileSvc : System.ServiceProcess.ServiceBase {
		//Members
        private FileMgr[] mSvcs = null;		            //Array of service instances; one per configuration entry
        private FTPMgr[] mFTPSvcs = null;		        //Array of service instances; one per configuration entry
        private System.Timers.Timer mTimer = null;		//Timer for scheduled operation
        private bool mUseTimer = false;		            //Scheduler flag
        private bool mStartup = true;		            //Startup flag
        private bool mShutdown = false;		            //Shutdown flag

        private const string KEY_TIMER_ENABLED = "TimerOn";
        private const string KEY_LOG_ENABLED = "LogOn";
        private const string KEY_LOG_PATH = "LogPath";
        private const string KEY_LOG_FILEMAX = "LogFilesMax";
        private System.ComponentModel.Container components = null;
		
		//Interface
		public DDUFileSvc() {
			//Constructor
			try {
                //Required by the Windows.Forms Component Designer.
                InitializeComponent();

                //Read configuration parameters
                try {
                    this.mUseTimer = Convert.ToBoolean(ConfigurationManager.AppSettings.Get(KEY_TIMER_ENABLED));
                    SvcLog.Enabled = Convert.ToBoolean(ConfigurationManager.AppSettings.Get(KEY_LOG_ENABLED));
                    SvcLog.Filepath = ConfigurationManager.AppSettings.Get(KEY_LOG_PATH);
                    SvcLog.FilesMax = Convert.ToInt32(ConfigurationManager.AppSettings.Get(KEY_LOG_FILEMAX));
                }
                catch(Exception ex) { reportError(new ApplicationException("Unexpected error while reading configuration parameters.",ex),false,true); }

                //Create services
                if(this.mUseTimer) {
                    this.mTimer = new System.Timers.Timer();
                    this.mTimer.Enabled = false;
                    this.mTimer.Elapsed += new System.Timers.ElapsedEventHandler(onTimerElapsed);
                }
            }
            catch(Exception ex) { reportError(new ApplicationException("Unexpected error while creating new DDUFileSvc instance.",ex), false,true); }
        }
		private void InitializeComponent() {
            // 
            // DDUFileSvc
            // 
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.ServiceName = "DDUFileSvc";

		}
		protected override void Dispose( bool disposing ) { if(disposing) { if(components != null) components.Dispose(); } base.Dispose( disposing ); }
        #region Non-Win Service: Start(), Stop(), Pause(), Continue()
        public void Start() { OnStart(null); }
        public void Stop() { OnStop(); }
        public void Pause() { OnPause(); }
        public void Continue() { OnContinue(); }
        public bool OneShot { get { return !this.mUseTimer; } }
        #endregion
        #region Service Overrides: OnStart(), OnStop(), OnPause(), OnContinue()
        protected override void OnStart(string[] args) {
			//Set things in motion so your service can do its work
			try {				
				//Timed or one shot
                if(this.mUseTimer) {
                    this.mStartup = true;
                    this.mTimer.Interval = 100;
                    this.mTimer.Enabled = true;
                }
                else {
                    initServices();
                    for(int i = 0; i < this.mSvcs.Length; i++) {
                        FileMgr svc = this.mSvcs[i];
                        svc.Execute();
                    }
                    for(int j = 0; j < this.mFTPSvcs.Length; j++) {
                        FTPMgr ftp = this.mFTPSvcs[j];
                        ftp.Execute();
                    }
                }
            }
            catch(Exception ex) { reportError(new ApplicationException("Unexpected error in OnStart.",ex),false,true); }
		}
		protected override void OnStop() {
			//Stop this service
			try {
				//Stop the service
				this.mShutdown = true;
				this.mTimer.Enabled = false;
				this.mTimer.Interval = 100;
				this.mTimer.Enabled = true;
			}
            catch(Exception ex) { reportError(new ApplicationException("Unexpected error in OnStop.",ex),false,true); }
        }
		protected override void OnPause() {
			//Pause this service
			try {
				//Stop the service
				this.mTimer.Enabled = false;
			}
            catch(Exception ex) { reportError(new ApplicationException("Unexpected error in OnPause.",ex),false,true); }
        }
		protected override void OnContinue() {
			//Continue this service
			try {
				//Stop the service
				this.mTimer.Interval = getTimerInterval();
				this.mTimer.Enabled = true;
			}
            catch(Exception ex) { reportError(new ApplicationException("Unexpected error in OnContinue.",ex),false,true); }
        }
        #endregion
        #region Scheduler: onTimerElapsed(), getTimerInterval()
        private void onTimerElapsed(object sender,System.Timers.ElapsedEventArgs e) {
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
                    for(int i = 0; i < this.mSvcs.Length; i++) {
                        FileMgr svc = this.mSvcs[i];
                        svc.Execute();
                    }
                    for(int j = 0; j < this.mFTPSvcs.Length; j++) {
                        FTPMgr ftp = this.mFTPSvcs[j];
                        ftp.Execute();
                    }
                }
				else {
					//Shutdown condition
                    this.mTimer.Enabled = false;
				}
			}
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { reportError(new ApplicationException("Unexpected error in timer interval operations.",ex),false,true); }
        }
		private int getTimerInterval() {
			//Read configuration timer parameter
			int timerInterval=600000;
			try {
                timerInterval = Convert.ToInt32(ConfigurationManager.AppSettings.Get("TimerInterval"));
            }
			catch(Exception ex) { reportError(new ApplicationException("Unexpected error in reading timer interval.", ex), false, true); }
			return timerInterval;
		}
        #endregion
        #region Local Services: initServices(), reportError()
        private void initServices() {
            //Create service instances based upon the configuration file
            try {
                //Read configuration from app.config
                IDictionary oDict = (IDictionary)ConfigurationManager.GetSection("dirs");
                if(oDict != null) {
                    int dirs = Convert.ToInt32(oDict["count"]);
                    this.mSvcs = new FileMgr[dirs];
                    for(int i = 1; i <= dirs; i++) {
                        oDict = (IDictionary)ConfigurationManager.GetSection("dir" + i.ToString());
                        string src = oDict["src"].ToString();
                        string pattern = oDict["pattern"].ToString();
                        string dest = oDict["dest"].ToString();
                        string suffix = oDict["suffix"].ToString();
                        FileMgr oSvc = new FileMgr(src,pattern,dest,suffix);
                        this.mSvcs[i - 1] = oSvc;
                    }
                }

                oDict = (IDictionary)ConfigurationManager.GetSection("ftps");
                if(oDict != null) {
                    int ftps = Convert.ToInt32(oDict["count"]);
                    this.mFTPSvcs = new FTPMgr[ftps];
                    for(int i = 1; i <= ftps; i++) {
                        oDict = (IDictionary)ConfigurationManager.GetSection("ftp" + i.ToString());
                        string server = oDict["server"].ToString();
                        string path = oDict["path"].ToString();
                        string pattern = oDict["pattern"].ToString();
                        string userid = oDict["userid"].ToString();
                        string password = oDict["password"].ToString();
                        string prefix = oDict["prefix"].ToString();
                        string suffix = oDict["suffix"].ToString();
                        FTPMgr ftp = new FTPMgr(server,path,pattern,userid,password,prefix,suffix);
                        this.mFTPSvcs[i - 1] = ftp;
                    }
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { reportError(new ApplicationException("Unexpected error in initializing services.",ex),false,true); }
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
                    MessageBox.Show(msg,"DDU File Utility",MessageBoxButtons.OK,MessageBoxIcon.Error);
                if(logMessage)
                    EventLog.WriteEntry("DDUFileSvc",ex.ToString(),EventLogEntryType.Error);
            }
            catch(Exception _ex) { EventLog.WriteEntry("DDUFileSvc",_ex.ToString(),EventLogEntryType.Error);  }
        }
        #endregion
    }
}
