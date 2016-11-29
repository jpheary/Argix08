//	File:	argixtrace.cs
//	Author:	J. Heary
//	Date:	01/02/08
//	Desc:	Custom System.Diagnostics.Trace class.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Diagnostics;

namespace Argix {
	/// <summary>Custom System.Diagnostics.Trace class.</summary>
	public class ArgixTrace {
        /// <summary>Threshold level for message severity to exceed in order to log the message.</summary>
        public static LogLevel LogLevelFloor = LogLevel.None;
		
		//Interface
		static ArgixTrace() { }
        private ArgixTrace() { }
		/// <summary>Adds a System.Diagnostics.TraceListener to the System.Diagnostics.Trace.Listeners collection.</summary>
		/// <param name="listener">A Argix08.TraceListener instance.</param>
        public static void AddListener(TraceListener listener) {
			//Add a trace listener to Trace instance
			Trace.Listeners.Add(listener);
		}
		/// <summary>Clears all System.Diagnostics.TraceListeners from the System.Diagnostics.Trace.Listeners collection.</summary>
        public static void ClearListeners() {
			//Clear all trace listeners
			Trace.Listeners.Clear();
		}
		/// <summary>Determines if the System.Diagnostics.TraceListener exists in the System.Diagnostics.Trace.Listeners collection.</summary>
		/// <param name="listener">A Argix08.TraceListener instance.</param>
		/// <returns>Returns true if the listener exists.</returns>
        public static bool ContainsListener(TraceListener listener) {
			//Clear all trace listeners
			return Trace.Listeners.Contains(listener);
		}
		/// <summary>A count of all System.Diagnostics.TraceListener in the System.Diagnostics.Trace.Listeners collection.</summary>
        public static int ListenersCount { get { return Trace.Listeners.Count; } }
		/// <summary>Removes a System.Diagnostics.TraceListener from the System.Diagnostics.Trace.Listeners collection.</summary>
		/// <param name="listener">A Argix08.TraceListener instance.</param>
        public static void RemoveListener(TraceListener listener) {
			//Remove the trace listener from Trace instance
			Trace.Listeners.Remove(listener);
		}
		/// <summary>Removes a System.Diagnostics.TraceListener from the System.Diagnostics.Trace.Listeners collection by name.</summary>
		/// <param name="name">The name of the System.Diagnostics.TraceListener.</param>
        public static void RemoveListener(string name) {
			//Remove a trace listener from Trace instance by name
			Trace.Listeners.Remove(name);
		}
		/// <summary>Logs a message if the message LogLevel exceeds this log level floor.</summary>
		/// <param name="message">A Argix08.TraceMessage message.</param>
        public static void Write(TraceMessage message) {
			//Send to the listeners if the log level exceeds the severity level set for this instance
            if(message.EventLogLevel >= LogLevelFloor) Trace.Write(message);
			Trace.Flush();
		}
		/// <summary>Logs a message if the message LogLevel exceeds this log level floor.</summary>
		/// <param name="message">A Argix08.TraceMessage message.</param>
        public static void WriteLine(TraceMessage message) {
			//Send to the listeners if the log level exceeds the severity level set for this instance
            if(message.EventLogLevel >= LogLevelFloor) Trace.WriteLine(message);
			Trace.Flush();
		}
	}
}
