//	File:	argixeventloglistener.cs
//	Author:	J. Heary
//	Date:	01/02/08
//	Desc:	A System.Diagnostics.TraceListener that logs Argix08.TraceMessage to a 
//			System.Diagnostics.EventLog.
//          NOTE: this.mEventMachineName.Length == 0 disables event logging.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Configuration;

namespace Argix {
	/// <summary>A System.Diagnostics.TraceListener that logs Argix08.TraceMessage to a System.Diagnostics.EventLog.</summary>
	public class ArgixEventLogTraceListener : TraceListener {
		//Members
		private LogLevel mLogLevelFloor=LogLevel.None;
		private string mEventLogName="";
		private string mEventMachineName=".";
		
		//Interface
		/// <summary>Initializes a new instance of the Argix08.ArgixEventLogTraceListener class that listens for Argix08.TraceMessages and logs them to a System.Diagnostics.EventLog.</summary>
		/// <param name="logLevelFloor">The minimum log level (floor) that Argix08.TraceMessage must meet to be logged.</param>
		/// <param name="eventLogName"></param>
		public ArgixEventLogTraceListener(LogLevel logLevelFloor, string eventLogName) : this(logLevelFloor, eventLogName, global::Argix.Properties.Settings.Default.EventLogMachineName) { }
		/// <summary>Initializes a new instance of the Argix08.ArgixEventLogTraceListener class that listens for Argix08.TraceMessages and logs them to a System.Diagnostics.EventLog.</summary>
		/// <param name="logLevelFloor">The minimum log level (floor) that Argix08.TraceMessage must meet to be logged.</param>
		/// <param name="eventLogName">Gets or sets the name of the log to read from or write to. </param>
		/// <param name="machineName">Gets or sets the name of the computer on which to read or write events.</param>
		public ArgixEventLogTraceListener(LogLevel logLevelFloor, string eventLogName, string machineName) {
			//Constructor
			this.mLogLevelFloor = logLevelFloor;
			this.mEventLogName = eventLogName;
			this.mEventMachineName = machineName;
		}
		/// <summary>Logs a string message if this log level floor is no higher than LogLevel.Debug.</summary>
		/// <param name="message">The message to log.</param>
		public override void Write(string message) {
			//Write message to database log if this log level is Debug or less
			try { 
				if(this.mLogLevelFloor<=LogLevel.Debug)
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
				if(this.mLogLevelFloor<=LogLevel.Debug)
					writeToLog(new TraceMessage(message,AppDomain.CurrentDomain.FriendlyName,this.mLogLevelFloor));
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while logging trace message (::WriteLine(string)).", ex); }
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
		#region Local Services: writeToLog(), getEntryType()
		private bool writeToLog(TraceMessage message) {
			//Log the error to the event log
			EventLog log=null;
			try {
                if(this.mEventMachineName.Trim().Length > 0) {
				    //Create event source if it doesn't exist
                    if(!EventLog.SourceExists(message.Source,this.mEventMachineName)) {
                        EventSourceCreationData escd = new EventSourceCreationData(message.Source,this.mEventLogName);
                        escd.MachineName = this.mEventMachineName;
                        EventLog.CreateEventSource(escd);
                    }
			    
				    //Write to the event log
                    log = new EventLog(this.mEventLogName,this.mEventMachineName,message.Source);
				    log.WriteEntry(message.Message, getEntryType(message.EventLogLevel));
                }
			}
			catch(Exception) { return false; }
			finally { if(log != null) log.Close(); }
			return true;
		}
		private EventLogEntryType getEntryType(LogLevel logLevel) {
			EventLogEntryType entryType=EventLogEntryType.Information;
			switch(logLevel) {
				case LogLevel.None:			entryType = EventLogEntryType.Information; break;
				case LogLevel.Error:		entryType = EventLogEntryType.Error; break;
				case LogLevel.Warning:		entryType = EventLogEntryType.Warning; break;
				case LogLevel.Information:	entryType = EventLogEntryType.Information; break;
				case LogLevel.Debug:		entryType = EventLogEntryType.Information; break;
				default:					entryType = EventLogEntryType.Information; break;
			}
			return entryType;
		}
		#endregion
	}
}
