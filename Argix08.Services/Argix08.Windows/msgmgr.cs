//	File:	msgmgr.cs
//	Author:	J. Heary
//	Date:	06/12/08
//	Desc:	Manages status bar message.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace Argix.Windows {
	//
	public class MessageManager {
		//Members		
		private StatusBarPanel mPanel=null;
		private Queue mQue=null;
		private ManualResetEvent mEvent=null;
		private Thread mThread=null;
		private int mInterval=DEFAULT_MESSAGE_INTERVAL;
		private int mCleanupInterval=DEFAULT_CLEANUP_INTERVAL;
        private MessageHandler mMessageDelegate = null;
        private EventHandler mCleanupDelegate = null;
		
		private const int DEFAULT_QUE_SIZE = 10;
		private const int DEFAULT_MESSAGE_INTERVAL = 250;
		private const int DEFAULT_CLEANUP_INTERVAL = 3000;

        private delegate void MessageHandler(string message);

        //Interface
		public MessageManager(): this(null) {}
		public MessageManager(StatusBarPanel panel): this(panel, DEFAULT_MESSAGE_INTERVAL, DEFAULT_CLEANUP_INTERVAL) {}
		public MessageManager(StatusBarPanel panel, int messageInterval): this(panel, messageInterval, DEFAULT_CLEANUP_INTERVAL) {}
		public MessageManager(StatusBarPanel panel, int messageInterval, int cleanupInterval) {
			//Constructor
			try {
				//Set panel object
				this.mPanel = panel;
				this.mPanel.Parent.Click += new System.EventHandler(onPanelClick);
				this.mQue = new Queue(DEFAULT_QUE_SIZE);
				this.mInterval = messageInterval;
				this.mCleanupInterval = cleanupInterval;
				this.mEvent = new ManualResetEvent(false);
                this.mMessageDelegate = new MessageHandler(onDisplay);
                this.mCleanupDelegate = new EventHandler(onCleanup);
				this.mThread = new Thread(new ThreadStart(displayMessages));
				this.mThread.IsBackground = true;
				this.mThread.Start();
			} 
			catch(Exception ex) { throw new ApplicationException("Failed to create a new message manager.", ex); }
		}
		~MessageManager() {
			//Destructor
			this.mEvent.Close();
		}
		public StatusBarPanel Panel { get { return this.mPanel; } }
		public int MessageInterval { get { return this.mInterval; } set { this.mInterval = value; } }
		public int CleanupInterval { get { return this.mCleanupInterval; } set { this.mCleanupInterval = value; } }
		public void AddMessage(string msg) { 
			try {
				//Add a message for display in the panel
				this.mQue.Enqueue(msg);
				this.mEvent.Set();
			}
			catch(Exception ex) { throw new ApplicationException("Failed to add a new message.", ex); }
        }
        #region Local Services: onPanelClick(), displayMessages(), onDisplay(), onCleanup()
        private void onPanelClick(object Sender, EventArgs e) { if(this.mPanel != null) this.mPanel.Text = ""; }
        private void displayMessages() {
			//Event handler for status bar cleanup timer
			do {
				//Don't let this loop quit on an exception
				try {
					//Wait for a signal that a new message has arrived
					this.mEvent.WaitOne();
					this.mEvent.Reset();
					if(this.mPanel != null) {
						//Display all new messages in the que
						while(this.mQue.Count > 0) {
							//Display next message in the panel and pause
                            this.mPanel.Parent.Invoke(this.mMessageDelegate,new object[] { this.mQue.Dequeue().ToString() });
							Thread.Sleep(this.mInterval);
						}
						
						//Pause longer on the last message, then clear it
						Thread.Sleep(this.mCleanupInterval);
                        this.mPanel.Parent.Invoke(this.mCleanupDelegate,new object[] { this,EventArgs.Empty });
					}
					else
						this.mQue.Clear();
				}
                catch(Exception ex) { Debug.Write(ex.ToString() + "\n"); }
			} while(true);
		}
        private void onDisplay(string message) { this.mPanel.Text = message; this.mPanel.Parent.Refresh(); }
        private void onCleanup(object sender, EventArgs e) { this.mPanel.Text = ""; }
        #endregion
    }
}
