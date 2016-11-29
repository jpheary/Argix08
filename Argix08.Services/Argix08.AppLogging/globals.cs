//	File:	globals.cs
//	Author:	J. Heary
//	Date:	01/02/08
//	Desc:	Globals consumed and exposed by this component.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Diagnostics;

namespace Argix {
	//Enumerators
	/// <summary>Trace logging level enumeration.</summary>
	public enum LogLevel { 
		/// <summary>0</summary>
		None=0,
		/// <summary>1</summary>
		Debug=1, 
		/// <summary>2</summary>
		Information=2, 
		/// <summary>3</summary>
		Warning=3, 
		/// <summary>4</summary>
		Error=4 }
	
	//Interfaces
    /// <summary>Logging exception event handler. </summary>
    /// <param name="source">Error source.</param>
    /// <param name="e">Exception data for the event.</param>
    public delegate void LoggingErrorEventHandler(object source,LoggingErrorEventArgs e);
    /// <summary>Logging error event args for LoggingErrorEventHandler. </summary>
    public class LoggingErrorEventArgs :EventArgs {
        private Exception _ex = null;
        private bool _displayMessage = false;
        private LogLevel _logLevel = LogLevel.None;
        /// <summary> </summary>
        /// <param name="ex"></param>
        public LoggingErrorEventArgs(Exception ex) : this(ex,false,LogLevel.None) { }
        /// <summary> </summary>
        /// <param name="ex"></param>
        /// <param name="displayMessage"></param>
        /// <param name="logLevel"></param>
        public LoggingErrorEventArgs(Exception ex,bool displayMessage,LogLevel logLevel) {
            //Constructor
            this._ex = ex;
            this._displayMessage = displayMessage;
            this._logLevel = logLevel;
        }
        /// <summary> </summary>
        public Exception Exception { get { return this._ex; } set { this._ex = value; } }
        /// <summary> </summary>
        public bool DisplayMessage { get { return this._displayMessage; } set { this._displayMessage = value; } }
        /// <summary> </summary>
        public LogLevel Level { get { return this._logLevel; } set { this._logLevel = value; } }
    }
}
