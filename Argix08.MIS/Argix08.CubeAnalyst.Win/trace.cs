using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Text;
using Argix.Data;

namespace Argix.Tools {
	//
	public class TraceLog {
		//Members
		private string mEventLogName="";			//Application key in database configuration table
		private ScannerDS mLogEntries=null;			//Collection of log entries 
		private int mDaysBack=1;
		private Mediator mMediator=null;			//Database connectivity

        private const int MINDAYS_BACK = 1,MAXDAYS_BACK = 365;
        private const string USP_TRACELOG_GETLIST = "uspToolsCubeTraceLogGet",TBL_TRACELOG_GETLIST = "ArgixLogTable";
        public event EventHandler Refreshed = null;
		
		//Interface
		public TraceLog(string logName, int initialDaysback, string DBConnection) { 
			//Constructor
			try {
				//Set custom attributes
				this.mEventLogName = logName;
				this.mDaysBack = initialDaysback;
				this.mMediator = new SQLMediator(DBConnection);
//				this.mMediator.DataStatusUpdate += new DataStatusHandler(frmMain.OnDataStatusUpdate);
				this.mLogEntries = new ScannerDS();
			}
			catch(Exception ex) { throw ex; }
		}
		#region Accessors/Modifiers: [Members...]
		#endregion
		public ScannerDS LogEntries { get { return this.mLogEntries; } }
		public int DaysBack { 
			get { return this.mDaysBack; } 
			set { 
				if(value < MINDAYS_BACK)
					throw new ApplicationException("Days back cannot be leass than " + MINDAYS_BACK.ToString() + " day.");
				if(value > MAXDAYS_BACK)
					throw new ApplicationException("Days back cannot be greater than " + MAXDAYS_BACK.ToString() + " day.");
				if(value != this.mDaysBack) this.mDaysBack = value;
				Refresh();
			} 
		} 
		public void Refresh() { 
			//Refresh data for this object
			try {
				//Clear cuurent collection and refresh
				this.mLogEntries.Clear();
				this.mLogEntries.Merge(this.mMediator.FillDataset(USP_TRACELOG_GETLIST, TBL_TRACELOG_GETLIST, new object[]{this.mEventLogName, DateTime.Today.AddDays(-this.mDaysBack), DateTime.Now}));
			}
			catch(Exception ex) { throw ex; }
			finally { if(this.Refreshed != null)this.Refreshed(this, EventArgs.Empty); }
		}
		public bool Add(TraceLogEntry entry) {
			//Add a new trace log entry
			return false;
		}
		public int Count { get { return this.mLogEntries.ArgixLogTable.Rows.Count; } }
		public TraceLogEntry Item(int id) {
			//Return an existing log entry from the log collection
			TraceLogEntry entry=null;
			try { 
				//Merge from collection (dataset)
				if(id > 0) {
					DataRow[] rows = this.mLogEntries.ArgixLogTable.Select("ID=" + id);
					if(rows.Length == 0)
						throw new ApplicationException("Log entry for id=" + id + " does not exist in this log table!\n");
					ScannerDS.ArgixLogTableRow row = (ScannerDS.ArgixLogTableRow)rows[0];
					entry = new TraceLogEntry(row, this.mMediator);
					entry.Changed += new EventHandler(OnEntryChanged);
				}
			}
			catch (Exception ex) { throw ex; }
			return entry;
		}
		public bool Remove(TraceLogEntry entry) {
			//Remove the specified trace log entry
			bool bRet=false;
			try { 
				bRet = entry.Delete();
			}
			catch(Exception ex) { throw ex; }
			return bRet;
		}
		
		private void OnEntryChanged(object sender, EventArgs e) { try { Refresh(); } catch(Exception) { } }
	}

    public class TraceLogEntry {
        //Members
        private long _id=0;
        private string _name="";
        private int _level=0;
        private DateTime _date;
        private string _source="", _category="", _event="";
        private string _user="", _computer="";
        private string _keyword1="", _keyword2="", _keyword3="", _message="";
        private Mediator mMediator=null;

        private const string USP_TRACELOG_DELETE = "uspToolsTraceLogDelete";
        public event EventHandler Changed = null;

        //Interface
        public TraceLogEntry(Mediator mediator) : this(null,mediator) { }
        public TraceLogEntry(ScannerDS.ArgixLogTableRow logEntry,Mediator mediator) {
            //Constructor
            try {
                this.mMediator = mediator;
                if(logEntry != null) {
                    if(!logEntry.IsIDNull()) this._id = logEntry.ID;
                    if(!logEntry.IsNameNull()) this._name = logEntry.Name;
                    if(!logEntry.IsLevelNull()) this._level = logEntry.Level;
                    if(!logEntry.IsDateNull()) this._date = logEntry.Date;
                    if(!logEntry.IsSourceNull()) this._source = logEntry.Source;
                    if(!logEntry.IsCategoryNull()) this._category = logEntry.Category;
                    if(!logEntry.IsEventNull()) this._event = logEntry.Event;
                    if(!logEntry.IsUserNull()) this._user = logEntry.User;
                    if(!logEntry.IsComputerNull()) this._computer = logEntry.Computer;
                    if(!logEntry.IsKeyword1Null()) this._keyword1 = logEntry.Keyword1;
                    if(!logEntry.IsKeyword2Null()) this._keyword2 = logEntry.Keyword2;
                    if(!logEntry.IsKeyword3Null()) this._keyword3 = logEntry.Keyword3;
                    if(!logEntry.IsMessageNull()) this._message = logEntry.Message;
                }
            }
            catch(Exception ex) { throw ex; }
        }
        #region Accessors\Modifiers: [Members...]
        public long ID { get { return this._id; }  }
        public string Name { get { return this._name; }  }
        public int Level { get { return this._level; }  }
        public DateTime Date { get { return this._date; } }
        public string Source { get { return this._source; } }
        public string Category { get { return this._category; } }
        public string Event { get { return this._event; } }
        public string User { get { return this._user; } }
        public string Computer { get { return this._computer; }  }
        public string Keyword1 {  get { return this._keyword1; } }
        public string Keyword2 { get { return this._keyword2; }  }
        public string Keyword3 {  get { return this._keyword3; }  }
        public string Message { get { return this._message; } }
        #endregion
        public bool Delete() {
            //Delete this log entry
            bool bRet=false;
            try {
                bRet = this.mMediator.ExecuteNonQuery(USP_TRACELOG_DELETE,new object[] { this._id });
                if(this.Changed != null) this.Changed(this,new EventArgs());
            }
            catch(Exception ex) { throw ex; }
            return bRet;
        }
    }
}
