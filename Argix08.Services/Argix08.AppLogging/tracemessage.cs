//	File:	tracemessage.cs
//	Author:	J. Heary
//	Date:	01/02/08
//	Desc:	Custom trace message.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Text;
using System.Runtime.Serialization;

namespace Argix {
	/// <summary> </summary>
	public class TraceMessage {
		//Members
		private string mMessage="";
		private string mSource="";
		private LogLevel mLogLevel=LogLevel.None;
		private string mKeyData1="";
		private string mKeyData2="";
		private string mKeyData3="";
		
		//Interface
		/// <summary>A new instance of the Argix08.TraceMessage class that contains message information for Trace logging.</summary>
		/// <param name="message">The message to log.</param>
		/// <param name="source"></param>
		/// <param name="logLevel">The severity log level for this message.</param>
		public TraceMessage(string message, string source, LogLevel logLevel) : this(message, source, logLevel, "", "", "") {}
		/// <summary>A new instance of the Argix08.TraceMessage class that contains message information for Trace logging.</summary>
		/// <param name="message">The message to log.</param>
		/// <param name="source"></param>
		/// <param name="logLevel">The severity log level for this message.</param>
		/// <param name="keyData1">An additional data field.</param>
		public TraceMessage(string message, string source, LogLevel logLevel, string keyData1) : this(message, source, logLevel, keyData1, "", "") {}
		/// <summary>A new instance of the Argix08.TraceMessage class that contains message information for Trace logging.</summary>
		/// <param name="message">The message to log.</param>
		/// <param name="source"></param>
		/// <param name="logLevel">The severity log level for this message.</param>
		/// <param name="keyData1">An additional data field.</param>
		/// <param name="keyData2">An additional data field.</param>
		public TraceMessage(string message, string source, LogLevel logLevel, string keyData1, string keyData2) : this(message, source, logLevel, keyData1, keyData2, "") {}
		/// <summary>A new instance of the Argix08.TraceMessage class that contains message information for Trace logging.</summary>
		/// <param name="message">The message to log.</param>
		/// <param name="source"></param>
		/// <param name="logLevel">The severity log level for this message.</param>
		/// <param name="keyData1">An additional data field.</param>
		/// <param name="keyData2">An additional data field.</param>
		/// <param name="keyData3">An additional data field.</param>
		public TraceMessage(string message, string source, LogLevel logLevel, string keyData1, string keyData2, string keyData3) {
			//Constructor
			this.mMessage = message;
			this.mSource = source;
			this.mLogLevel = logLevel;
			this.mKeyData1 = keyData1;
			this.mKeyData2 = keyData2;
			this.mKeyData3 = keyData3;
		}
		/// <summary>Gets and sets the Message field.</summary>
		public string Message { get{ return this.mMessage; } set{ this.mMessage = value; } }
		/// <summary>Gets and sets the Source field.</summary>
		public string Source { get{ return this.mSource; } set{ this.mSource = value; } }
		/// <summary>Gets and sets the EventLogLevel field.</summary>
		public LogLevel EventLogLevel { get{ return this.mLogLevel; } set{ this.mLogLevel = value; } }
		/// <summary>Gets and sets the KeyData1 field.</summary>
		public string KeyData1 { get{ return this.mKeyData1; } set{ this.mKeyData1 = value; } }
		/// <summary>Gets and sets the KeyData2 field.</summary>
		public string KeyData2 { get{ return this.mKeyData2; } set{ this.mKeyData2 = value; } }
		/// <summary>Gets and sets the KeyData3 field.</summary>
		public string KeyData3 { get{ return this.mKeyData3; } set{ this.mKeyData3 = value; } }
		/// <summary>Appends a new message to this message.</summary>
		/// <param name="message">The message to append.</param>
		public void AppendToMessage(string message) {
			//Append a string to the current trace message
			StringBuilder sb = new StringBuilder(this.Message);
			sb.Append(message);
			this.Message = sb.ToString();
		}
		/// <summary>Appends a CR and a new message to this message.</summary>
		/// <param name="message">The message to append.</param>
		public void AppendLineToMessage(string message) {
			//Append a new line of string to the current trace message
			StringBuilder sb = new StringBuilder(this.Message);
			sb.Append(Convert.ToChar(13));
			sb.Append(message);
			this.Message = sb.ToString();
		}
	}
}
