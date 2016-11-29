using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Argix.CustomerSvc;

namespace Argix {
	//
    //public class Monitor: Form {
    //    //Members
    //    private System.ComponentModel.IContainer components = null;
    //    private int mSleepTimeout=SLEEP_DEFAULT;
    //    private BackgroundWorker mWorker;
    //    private System.Windows.Forms.Timer mTimer;

    //    public const int SLEEP_DEFAULT = 10000;
    //    public event DataEventHandler DataUpdate = null;

    //    //Interface
    //    public Monitor(int sleepTimeout) {
    //        Debug.WriteLine("Monitor- " + Thread.CurrentThread.Name);
    //        InitializeComponent();
    //        this.mSleepTimeout = sleepTimeout > 5000 ? sleepTimeout : 5000;
    //    }
    //    protected override void Dispose(bool disposing) {
    //        if(disposing && (components != null)) {
    //            components.Dispose();
    //        }
    //        base.Dispose(disposing);
    //    }
    //    #region Windows Form Designer generated code

    //    /// <summary>
    //    /// Required method for Designer support - do not modify
    //    /// the contents of this method with the code editor.
    //    /// </summary>
    //    private void InitializeComponent() {
    //        this.components = new System.ComponentModel.Container();
    //        this.mWorker = new System.ComponentModel.BackgroundWorker();
    //        this.mTimer = new System.Windows.Forms.Timer(this.components);
    //        this.SuspendLayout();
    //        // 
    //        // mWorker
    //        // 
    //        this.mWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.OnDoWork);
    //        this.mWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.OnRunWorkerCompleted);
    //        // 
    //        // mTimer
    //        // 
    //        this.mTimer.Interval = 10000;
    //        this.mTimer.Tick += new System.EventHandler(this.OnTick);
    //        // 
    //        // Monitor
    //        // 
    //        this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
    //        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    //        this.ClientSize = new System.Drawing.Size(284,264);
    //        this.Name = "Monitor";
    //        this.ResumeLayout(false);

    //    }

    //    #endregion
    //    public void Start() { this.mTimer.Start(); }
    //    public void Stop() { this.mTimer.Stop(); }
    //    private void OnTick(object sender,EventArgs e) { this.mWorker.RunWorkerAsync(); }
    //    private void OnDoWork(object sender,DoWorkEventArgs e) {
    //        //
    //        Debug.WriteLine("OnDoWork- " + Thread.CurrentThread.Name);
    //        e.Result = CRGFactory.GetIssues();
    //    }
    //    private void OnRunWorkerCompleted(object sender,RunWorkerCompletedEventArgs e) {
    //        //
    //        Debug.WriteLine("OnRunWorkerCompleted- " + Thread.CurrentThread.Name);
    //        DataSet ds=null;
    //        if(this.InvokeRequired) {
    //            this.Invoke(new RunWorkerCompletedEventHandler(OnRunWorkerCompleted),new object[] { sender,e });
    //        }
    //        else {
    //            ds = (DataSet)e.Result;
    //            if(this.DataUpdate != null) this.DataUpdate(this,new DataEventArgs(ds));
    //        }
    //    }
    //}
    public class Monitor {
        //Members
        private Thread mThread=null;
        private bool mRequesting=false;
        private int mSleepTimeout=SLEEP_DEFAULT;

        public const int SLEEP_DEFAULT = 30000;
        public event DataEventHandler DataUpdate = null;
        public event ControlErrorEventHandler DataError=null;

        private delegate void DataCompleteHandler(object data);
        private delegate void DataErrorHandler(Exception ex);

        //Interface
        public Monitor(int sleepTimeout) { this.SleepTimeout = sleepTimeout > 10000 ? sleepTimeout : 10000; }
        public int SleepTimeout { get { return this.mSleepTimeout; } set { this.mSleepTimeout = value; } }
        public void Start() {
            //Start monitor
            try {
                //Validate public access requests
                if(this.mRequesting) return;

                //Start the thread
                this.mRequesting = true;
                this.mThread = new Thread(new ThreadStart(requestData));
                this.mThread.Name = "IssueMonitor";
                this.mThread.IsBackground = true;
                this.mThread.Priority = ThreadPriority.Lowest;
                this.mThread.Start();
            }
            catch(Exception ex) { throw new ApplicationException("Error while starting the Monitor.",ex); }
        }
        public void Stop() {
            //Stop the current search thread
            try {
                //Validate public access requests
                if(!this.mRequesting) return;

                //Abort the thread
                this.mRequesting = false;
                if(this.mThread.IsAlive) {
                    this.mThread.Abort();
                    this.mThread.Join(3000);
                }
                this.mThread = null;
            }
            catch(Exception ex) { reportError(ex); }
        }
        private void requestData() {
            //This is the actual thread procedure; this method runs as a background thread
            //NOTE: Thread.ResetAbort() cancels the abort so it is not rethrown after the catch;
            //      anyway, the exception has caused code to leave the while() {} block
            while(this.mRequesting) {
                try {
                    Thread.Sleep(this.mSleepTimeout);
                    DataSet data = CRGFactory.GetIssues();
                    new DataCompleteHandler(OnDataComplete).BeginInvoke(data,null,null);
                }
                catch(ThreadAbortException) { this.mRequesting = false; Thread.ResetAbort(); }
                catch(Exception ex) { new DataErrorHandler(OnDataError).BeginInvoke(ex,null,null); }
            }
        }
        private void OnDataComplete(object data) {
            //This method is called from the background thread; it is called through a BeginInvoke  
            //call so that it is marshaled to the thread that owns the dataset
            try {
                Debug.WriteLine("OnDataComplete- " + Thread.CurrentThread.Name);
                if(this.DataUpdate != null) this.DataUpdate(this,new DataEventArgs(data));
            }
            catch(Exception ex) { reportError(ex); }
        }
        private void OnDataError(Exception ex) {
            //Event handler for data failed event
            reportError(ex);
        }
        protected void reportError(Exception ex) {
            if(this.DataError != null) this.DataError(this,new ControlErrorEventArgs(ex));
        }
    }

    public delegate void DataEventHandler(object source,DataEventArgs e);
    public class DataEventArgs:EventArgs {
        private object _data=null;
        public DataEventArgs(object data) { this._data = data; }
        public object Data { get { return this._data; } set { this._data = value; } }
    }
}