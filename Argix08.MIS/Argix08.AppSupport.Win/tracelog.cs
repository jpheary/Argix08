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

namespace Argix.MIS {
    //
	public class TraceLog {
		//Members
		private string mEventLogName="";			//Application key in database configuration table
		private TraceLogsDS mLogEntries=null;		//Collection of log entries 
		private Mediator mMediator=null;			//Database connectivity

        public const string USP_TRACELOG_GETLIST = "uspToolsTraceLogGetList",TBL_TRACELOG_GETLIST = "ArgixLogTable";
        public event EventHandler Refreshed=null;
		
		//Interface
		public TraceLog(string logName, string dbConnection) { 
			//Constructor
			try {
				//Set custom attributes
				this.mEventLogName = logName;
				this.mMediator = new SQLMediator(dbConnection);
				this.mMediator.DataStatusUpdate += new DataStatusHandler(frmMain.OnDataStatusUpdate);
				this.mLogEntries = new TraceLogsDS();
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error creating new TraceLog instance.",ex); }
        }
		#region Accessors/Modifiers:
		#endregion
		public TraceLogsDS LogEntries { get { return this.mLogEntries; } }
		public void Refresh() { 
			//Refresh data for this object
			try {
				//Clear cuurent collection and refresh
				this.mLogEntries.Clear();
				this.mLogEntries.Merge(this.mMediator.FillDataset(USP_TRACELOG_GETLIST, TBL_TRACELOG_GETLIST, new object[]{this.mEventLogName}));
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error refreshing trace log entries.",ex); }
            finally { if(this.Refreshed != null)this.Refreshed(this,EventArgs.Empty); }
		}
		public bool Add(TraceLogEntry entry) { return false; }
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
					TraceLogsDS.ArgixLogTableRow row = (TraceLogsDS.ArgixLogTableRow)rows[0];
					entry = new TraceLogEntry(row, this.mMediator);
					entry.Changed += new EventHandler(OnEntryChanged);
				}
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error getting trace log entry.",ex); }
            return entry;
		}
		public bool Remove(TraceLogEntry entry) {
			//Remove the specified trace log entry
			bool ret=false;
			try { 
				ret = entry.Delete();
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error removing trace log entry.",ex); }
            return ret;
		}
		
		private void OnEntryChanged(object sender, EventArgs e) {
			//Event handler for change to a log entry
			try { Refresh(); } catch(Exception) { }
		}
	}

    public class TraceLogEntry {
        //Members
        private long _id=0;
        private string _name="";
        private int _level=0;
        private DateTime _date;
        private string _source="",_category="",_event="";
        private string _user="",_computer="";
        private string _keyword1="",_keyword2="",_keyword3="";
        private string _message="";
        private Mediator mMediator=null;

        public const string USP_TRACELOG_DELETE = "uspToolsTraceLogDelete";
        public event EventHandler Changed=null;

        //Interface
        public TraceLogEntry(Mediator mediator) : this(null,mediator) { }
        public TraceLogEntry(TraceLogsDS.ArgixLogTableRow logEntry,Mediator mediator) {
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
            catch(Exception ex) { throw new ApplicationException("Unexpected error creating new TraceLogEntry instance.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        public long ID { get { return this._id; } set { this._id = value; } }
        public string Name { get { return this._name; } set { this._name = value; } }
        public int Level { get { return this._level; } set { this._level = value; } }
        public DateTime Date { get { return this._date; } set { this._date = value; } }
        public string Source { get { return this._source; } set { this._source = value; } }
        public string Category { get { return this._category; } set { this._category = value; } }
        public string Event { get { return this._event; } set { this._event = value; } }
        public string User { get { return this._user; } set { this._user = value; } }
        public string Computer { get { return this._computer; } set { this._computer = value; } }
        public string Keyword1 { get { return this._keyword1; } set { this._keyword1 = value; } }
        public string Keyword2 { get { return this._keyword2; } set { this._keyword2 = value; } }
        public string Keyword3 { get { return this._keyword3; } set { this._keyword3 = value; } }
        public string Message { get { return this._message; } set { this._message = value; } }
        #endregion
        public bool Delete() {
            //Delete this log entry
            bool ret=false;
            try {
                ret = this.mMediator.ExecuteNonQuery(USP_TRACELOG_DELETE,new object[] { this._id });
                if(this.Changed != null) this.Changed(this,new EventArgs());
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error deleting log entry #" + this._id.ToString(),ex); }
            return ret;
        }
    }
}
