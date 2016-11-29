//	File:	argixtextlistener.cs
//	Author:	J. Heary
//	Date:	01/02/08
//	Desc:	A System.Diagnostics.TextWriterTraceListener that logs Argix08.TraceMessage 
//			to a System.IO.TextWriter or to a System.IO.Stream, such as System.Console.Out 
//			or System.IO.FileStream.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Argix {
	/// <summary>A System.Diagnostics.TextWriterTraceListener that logs Argix08.TraceMessage to a System.IO.TextWriter or to a System.IO.Stream, such as System.Console.Out or System.IO.FileStream.</summary>
	public class ArgixTextWriterTraceListener : TextWriterTraceListener {
		//Members
		private LogLevel mLogLevelFloor=LogLevel.None;
		
		//Interface
		/// <summary>Initializes a new instance of the Argix08.ArgixTextWriterTraceListener class that listens for Argix08.TraceMessages and logs them to a System.IO.Stream.</summary>
		/// <param name="stream">A System.IO.Stream that represents the stream the Argix08.ArgixTextWriterTraceListener writes to.</param>
		/// <param name="name">The name of the new instance.</param>
		/// <param name="logLevel">The minimum log level (floor) that Argix08.TraceMessage must meet to be logged.</param>
		public ArgixTextWriterTraceListener(Stream stream, string name, LogLevel logLevel) : base(stream, name) { this.mLogLevelFloor = logLevel; }
		/// <summary>Initializes a new instance of the Argix08.ArgixTextWriterTraceListener class that listens for Argix08.TraceMessages and logs them to a System.IO.Stream.</summary>
		/// <param name="stream">A System.IO.Stream that represents the stream the Argix08.ArgixTextWriterTraceListener writes to.</param>
		/// <param name="logLevel">The minimum log level (floor) that Argix08.TraceMessage must meet to be logged.</param>
		public ArgixTextWriterTraceListener(Stream stream, LogLevel logLevel) : base(stream) { this.mLogLevelFloor = logLevel; }
		/// <summary>Initializes a new instance of the Argix08.ArgixTextWriterTraceListener class with the specified name, using the file as the recipient of the debugging and tracing output.</summary>
		/// <param name="fileName">The name of the file the Argix08.ArgixTextWriterTraceListener writes to.</param>
		/// <param name="name">The name of the new instance.</param>
		/// <param name="logLevel">The minimum log level (floor) that Argix08.TraceMessage must meet to be logged.</param>
		public ArgixTextWriterTraceListener(string fileName, string name, LogLevel logLevel) : base(fileName, name) { this.mLogLevelFloor = logLevel; }
		/// <summary>Initializes a new instance of the Argix08.ArgixTextWriterTraceListener class with the specified name, using the file as the recipient of the debugging and tracing output.</summary>
		/// <param name="fileName">The name of the file the Argix08.ArgixTextWriterTraceListener writes to.</param>
		/// <param name="logLevel"></param>
		public ArgixTextWriterTraceListener(string fileName, LogLevel logLevel) : base(fileName) {this.mLogLevelFloor = logLevel; }
		/// <summary>Initializes a new instance of the Argix08.ArgixTextWriterTraceListener class, using the file as the recipient of the debugging and tracing output.</summary>
		/// <param name="writer">A System.IO.TextWriter that receives the output from the Argix08.ArgixTextWriterTraceListener.</param>
		/// <param name="name">The name of the new instance.</param>
		/// <param name="logLevel">The minimum log level (floor) that Argix08.TraceMessage must meet to be logged.</param>
		public ArgixTextWriterTraceListener(TextWriter writer, string name, LogLevel logLevel) : base(writer, name) { this.mLogLevelFloor = logLevel; }
		/// <summary>Initializes a new instance of the Argix08.ArgixTextWriterTraceListener class that listens for Argix08.TraceMessages and logs them to a System.IO.TextWriter.</summary>
		/// <param name="writer"></param>
		/// <param name="logLevel">The minimum log level (floor) that Argix08.TraceMessage must meet to be logged.</param>
		public ArgixTextWriterTraceListener(TextWriter writer, LogLevel logLevel) : base(writer) { this.mLogLevelFloor = logLevel; }
		/// <summary>Logs a string message if this log level floor is no higher than LogLevel.Debug.</summary>
		/// <param name="message">The message to log.</param>
		public override void Write(string message) {
			//Write message to database log if this log level is Debug or less
			try { 
				if(this.mLogLevelFloor <= LogLevel.Debug) base.Write(formatMessage(message));
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while logging trace message (::WriteLine(string)).", ex); }
		}
		/// <summary>Logs a message if the message LogLevel exceeds this log level floor.</summary>
		/// <param name="message">A message object that derives from Argix08.TraceMessage.</param>
		public override void Write(object message) {
			//Write o to database log if event level is severe enough
			try { 
				TraceMessage tm = (TraceMessage)message;
				if(tm.EventLogLevel >= this.mLogLevelFloor) base.Write(formatMessage(tm));
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while logging trace message (::WriteLine(object)).", ex); }
		}
		/// <summary>Logs a string message if this log level floor is no higher than LogLevel.Debug.</summary>
		/// <param name="message">The message to log.</param>
		public override void WriteLine(string message) {
			//Write message to database log if this log level is Debug or less
			try { 
				if(this.mLogLevelFloor <= LogLevel.Debug) base.WriteLine(formatMessage(message));
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while logging trace message (::WriteLine(string)).", ex); }
		}
		/// <summary>Logs a message if the message LogLevel exceeds this log level floor.</summary>
		/// <param name="message">A message object that derives from Argix08.TraceMessage.</param>
		public override void WriteLine(object message) {
			//Write message to database log if event level is severe enough
			try { 
				TraceMessage tm = (TraceMessage)message;
				if(tm.EventLogLevel >= this.mLogLevelFloor) base.WriteLine(formatMessage(tm));
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while logging trace message (::WriteLine(object)).", ex); }
		}
		#region  Local Services: formatMessage()
		private string formatMessage(string message) {
			//
			StringBuilder preamble = new StringBuilder();
			preamble.Append(DateTime.Now.ToString()+ "\t");
			preamble.Append(AppDomain.CurrentDomain.FriendlyName + "\t");
			preamble.Append(message + "\t");
			preamble.Append(this.mLogLevelFloor.ToString() + "\t");

			return preamble.ToString();
		}
		private string formatMessage(TraceMessage msg) {
			//
			StringBuilder preamble = new StringBuilder();
			preamble.Append(DateTime.Now.ToString()+ "\t");
			preamble.Append(msg.Source + "\t");
			preamble.Append(msg.Message + "\t");
			preamble.Append(msg.EventLogLevel.ToString() + "\t");
			return preamble.ToString();
		}
		#endregion
	}
}
