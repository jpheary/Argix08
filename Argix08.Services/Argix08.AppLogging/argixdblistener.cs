//	File:	argixdblistener.cs.cs
//	Author:	J. Heary
//	Date:	01/02/08
//	Desc:	System.Diagnostics.TraceListener that logs Argix08.TraceMessage to a database.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Diagnostics;
using Argix.Data;

namespace Argix {
    /// <summary>System.Diagnostics.TraceListener that logs Argix08.TraceMessage to a database.</summary>
	public class DBTraceListener : TraceListener {
		//Members
		private LogLevel mLogLevelFloor=LogLevel.None;
        private Mediator mMediator=null;
        private string mLogSPName=ARGIXLOG_SPNAME;
        private string mLogEntryName=ARGIXLOG_ENTRYNAME;

        /// <summary>Trace database stored procedure name for database logging.</summary>
        public const string ARGIXLOG_SPNAME="uspLogEntryNew";
        /// <summary>Entry name in database log.</summary>
        public const string ARGIXLOG_ENTRYNAME="Argix08";

		
		//Interface
        /// <summary>Initializes a new instance of the Argix08.DBTraceListener class that listens for Argix08.TraceMessages and logs them to database through a Tsort05.ITraceDB object.</summary>
        /// <param name="logLevelFloor">The minimum log level (floor) that Argix08.TraceMessage must meet to be logged.</param>
        /// <param name="mediator">An Argix.Data.Mediator instance for data access.</param>
        public DBTraceListener(LogLevel logLevelFloor,Mediator mediator) : this(logLevelFloor,mediator,ARGIXLOG_SPNAME,ARGIXLOG_ENTRYNAME) { }
        /// <summary>Initializes a new instance of the Argix08.DBTraceListener class that listens for Argix08.TraceMessages and logs them to database through a Tsort05.ITraceDB object.</summary>
        /// <param name="logLevelFloor">The minimum log level (floor) that Argix08.TraceMessage must meet to be logged.</param>
        /// <param name="mediator">An Argix.Data.Mediator instance for data access.</param>
        /// <param name="spName"></param>
        /// <param name="entryName"></param>
        public DBTraceListener(LogLevel logLevelFloor,Mediator mediator, string spName, string entryName) {
            //Constructor
            try {
                //Set members
                this.mLogLevelFloor = logLevelFloor;
                this.mMediator = mediator;
                this.mLogSPName = spName;
                this.mLogEntryName = entryName;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new DBTraceListener instance.",ex); }
        }

        /// <summary>Logs a string message if this log level floor is no higher than LogLevel.Debug.</summary>
		/// <param name="message">The message to log.</param>
		public override void Write(string message) {
			//Write message to database log if this log level is Debug or less
			try { 
				if(this.mLogLevelFloor <= LogLevel.Debug)
					writeToLog(new TraceMessage(message, AppDomain.CurrentDomain.FriendlyName, this.mLogLevelFloor));
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while logging trace message (::WriteLine(string)).", ex); }
		}
		/// <summary>Logs a message if the message LogLevel exceeds this log level floor.</summary>
        /// <param name="message">A message object that derives from Argix08.TraceMessage.</param>
		public override void Write(object message) {
			//Write o to database log if event level is severe enough
			try { 
				TraceMessage tm = (TraceMessage)message;
				if(tm.EventLogLevel >= this.mLogLevelFloor) writeToLog(tm);
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while logging trace message (::WriteLine(object)).", ex); }
		}
		/// <summary>Logs a string message if this log level floor is no higher than LogLevel.Debug.</summary>
		/// <param name="message">The message to log.</param>
		public override void WriteLine(string message) {
			//Write message to database log if this log level is Debug or less
            try {
                if(this.mLogLevelFloor <= LogLevel.Debug)
                    writeToLog(new TraceMessage(message,AppDomain.CurrentDomain.FriendlyName,this.mLogLevelFloor));
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while logging trace message (::WriteLine(string)).",ex); }
		}
		/// <summary>Logs a message if the message LogLevel exceeds this log level floor.</summary>
        /// <param name="message">A message object that derives from Argix08.TraceMessage.</param>
		public override void WriteLine(object message) {
			//Write message to database log if event level is severe enough
			try { 
				TraceMessage tm = (TraceMessage)message;
				if(tm.EventLogLevel >= this.mLogLevelFloor) writeToLog(tm);
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while logging trace message (::WriteLine(object)).", ex); }
        }
        #region Local Services: writeToLog()
        private void writeToLog(TraceMessage msg) {
            //Write trace message to database log
            string sSource = (msg.Source.Length<=30) ? msg.Source : msg.Source.Substring(0,30);
            string sKeyData1 = (msg.KeyData1.Length<=30) ? msg.KeyData1 : msg.KeyData1.Substring(0,30);
            string sKeyData2 = (msg.KeyData2.Length<=30) ? msg.KeyData2 : msg.KeyData2.Substring(0,30);
            string sKeyData3 = (msg.KeyData3.Length<=30) ? msg.KeyData3 : msg.KeyData3.Substring(0,30);
            string sMessage = (msg.Message.Length<=300) ? msg.Message : msg.Message.Substring(0,300);
            sMessage = sMessage.Replace("\\n"," ");
            sMessage = sMessage.Replace("\r","");
            sMessage = sMessage.Replace("\n","");
            this.mMediator.ExecuteNonQuery(this.mLogSPName,new object[] { this.mLogEntryName,Convert.ToInt32(msg.EventLogLevel),DateTime.Now,sSource,"None","0",Environment.UserName,Environment.MachineName,sKeyData1,sKeyData2,sKeyData3,sMessage });
        }
        #endregion
    }
}
