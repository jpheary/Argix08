//	File:	svclog.cs
//	Author:	J. Heary
//	Date:	10/24/05
//	Desc:	Manages message logging to a text file; manages log files.
//	Rev:	07/07/10 (jph)
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Argix {
    //
    //Singleton for (global) log tracing
    public class SvcLog {
        //Members
        private static TextBox _TextBox=null;
        private const string MSG_FORMAT = "yyyy-MM-dd HH:mm:ss";

        //Interface		
        static SvcLog() { }
        private SvcLog() { }
        public static TextBox Device { get { return _TextBox; } set { _TextBox = value; } }
        public static void LogMessage(string message) {
            //Log the specified message to the textbox
            _TextBox.AppendText(DateTime.Now.ToString(MSG_FORMAT) + " : " + message);
            _TextBox.AppendText("\r\n");
        }
    }
}
