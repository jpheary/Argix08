//	File:	textboxlistener.cs
//	Author:	J. Heary
//	Date:	01/02/08
//	Desc:	System.Diagnostics.TraceListener that logs Argix08.TraceMessage to a textbox.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Argix {
	/// <summary>System.Diagnostics.TraceListener that logs Argix08.TraceMessage to a textbox.</summary>
	public class ArgixTextBoxListener : TraceListener {
		//Members
		private LogLevel mLogLevelFloor=LogLevel.None;
		private TextBox mTextBox=null;
		private RichTextBox mRichTextBox=null;
		
		//Interface
		/// <summary>Initializes a new instance of the Argix08.ArgixTextBoxListener class that listens for Argix08.TraceMessages and logs them to a System.Windows.Forms.Textbox.</summary>
		/// <param name="logLevelFloor">The minimum log level (floor) that Argix08.TraceMessage must meet to be logged.</param>
		/// <param name="textBox">A System.Windows.Forms.Textbox to log messages to.</param>
		public ArgixTextBoxListener(LogLevel logLevelFloor, TextBox textBox) {
			//Constructor
			this.mLogLevelFloor = logLevelFloor;
			this.mTextBox = textBox;
		}
		/// <summary>Initializes a new instance of the Argix08.ArgixTextBoxListener class that listens for Argix08.TraceMessages and logs them to a System.Windows.Forms.RichTextbox.</summary>
		/// <param name="logLevelFloor">The minimum log level (floor) that Argix08.TraceMessage must meet to be logged.</param>
		/// <param name="richTextBox">A System.Windows.Forms.RichTextbox to log messages to.</param>
		public ArgixTextBoxListener(LogLevel logLevelFloor, RichTextBox richTextBox) {
			//Constructor
			this.mLogLevelFloor = logLevelFloor;
			this.mRichTextBox = richTextBox;
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
					writeToLog(new TraceMessage(message, AppDomain.CurrentDomain.FriendlyName, this.mLogLevelFloor));
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
		#region Local Services: writeToLog()
		private void writeToLog(TraceMessage tm) {
			//Write message to textbox
			string source="", keyData1="", keyData2="", keyData3="", message="";
			try {
				source = (tm.Source.Length <= 30) ? tm.Source : tm.Source.Substring(0, 30);
				message = (tm.Message.Length <= 300) ? tm.Message : tm.Message.Substring(0, 300);
				message = message.Replace("\\n", " ");
				message = message.Replace("\r", "");
				message = message.Replace("\n", "");
				keyData1 = (tm.KeyData1.Length <= 30) ? tm.KeyData1 : tm.KeyData1.Substring(0, 30);
				keyData2 = (tm.KeyData2.Length <= 30) ? tm.KeyData2 : tm.KeyData2.Substring(0, 30);
				keyData3 = (tm.KeyData3.Length <= 30) ? tm.KeyData3 : tm.KeyData3.Substring(0, 30);
				if(this.mTextBox != null)
					this.mTextBox.AppendText(DateTime.Now.ToString("HH:mm:ss") + "\t" + source + "\t" + keyData1 + "\t" + keyData2 + "\t" + keyData3 + "\t" + message + "\n");
				else if(this.mRichTextBox != null)
					this.mRichTextBox.AppendText(DateTime.Now.ToString("HH:mm:ss") + "\t" + source + "\t" + keyData1 + "\t" + keyData2 + "\t" + keyData3 + "\t" + message + "\n");
			} 
			catch(Exception) { }
		}
		#endregion
	}
}
