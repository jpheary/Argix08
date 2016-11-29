//	File:	globals.cs
//	Author:	J. Heary
//	Date:	04/26/06
//	Desc:	Enumerators, event support, exceptions, etc.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using System.Windows;
using Tsort.Windows;

namespace Tsort {
	//Global classes
	public class App: AppBase {
		//Members
		
		//Global configuration (KEY_) and database (USP_) constants
		public const string KEY_READONLY = "ReadOnly";
		public const string KEY_MISPASSWORD = "MISPassword";
		public const string KEY_TRACELEVEL = "TraceLevel";
		
		public const string USP_CONFIGURATION = "uspConfigurationGetList", TBL_CONFIGURATION = "ConfigTable";
		public const string USP_LOCALTERMINAL = "uspEnterpriseCurrentTerminalGet", TBL_LOCALTERMINAL = "LocalTerminalTable";
		public const string USP_TRACE = "uspLogEntryNew";
		
		//Interface
		static App() { }
		public static string EventLogName { get { return "Admnistration"; } }
	}

	public delegate void ErrorEventHandler(object source, ErrorEventArgs e);
	public class ErrorEventArgs : EventArgs {
		private Exception _ex;
		private string _keyword1="";
		private string _keyword2="";
		private string _keyword3="";
		public ErrorEventArgs(Exception ex): this(ex,"","","") {}
		public ErrorEventArgs(Exception ex, string keyword1, string keyword2, string keyword3) {
			//Constructor
			this._ex = ex;
			this._keyword1 = keyword1;
			this._keyword2 = keyword2;
			this._keyword3 = keyword3;
		}
		public Exception Exception { get { return this._ex; } set { this._ex = value; } }
		public string Keyword1 { get { return this._keyword1; } set { this._keyword1 = value; } }
		public string Keyword2 { get { return this._keyword2; } set { this._keyword2 = value; } }
		public string Keyword3 { get { return this._keyword3; } set { this._keyword3 = value; } }
	}
}