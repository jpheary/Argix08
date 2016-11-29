//	File:	globals.cs
//	Author:	J. Heary
//	Date:	10/28/04
//	Desc:	Enumerators, event support, exceptions, etc.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using Argix;
using Argix.Windows;

namespace Tsort {
	//Global application object
	public class App: AppBase {
		//Members
		
		//Constants		
		public const string USP_LOCALTERMINAL = "uspEnterpriseCurrentTerminalGet", TBL_LOCALTERMINAL = "DBATerminalTable";
        public const string USP_SORTSTATIONS = "uspToolsWorkstationsGetList", TBL_SORTSTATIONS = "WorkstationDetailTable";
        public const string USP_DIRECTASSIGNMENTS = "uspToolsSortMonFreightAssignmentGetList", TBL_DIRECTASSIGNMENTS = "DirectAssignmentTable";
        public const string USP_INDIRECTASSIGNMENTS = "uspToolsSortMonTripAssignmentGetList", TBL_INDIRECTASSIGNMENTS = "IndirectAssignmentTable";
		
        public const string USP_LOGGET = "uspToolsSortMonLogGet", TBL_LOGGET = "ArgixLogTable";
		public const string USP_LOGGETFORSTATION = "uspToolsSortMonLogGetForStation";
		public const string USP_LOGGETFOREVENT = "uspToolsSortMonLogGetForEvent";
		public const string USP_SCANFINDINLOG = "uspToolsSortMonScanFindInLog", TBL_SCANFINDINLOG = "ArgixLogTable";
        
        public const string USP_ITEMSGETFORSTATION = "uspToolsSortMonItemsGetForStation", TBL_ITEMSGET = "SortedItemTable";
		public const string USP_ITEMFINDINTABLE = "uspToolsSortMonItemFindInTable", TBL_ITEMFINDINTABLE = "SortedItemTable";
        public const string USP_SCANSGETFORSTATION = "uspToolsSortMonScansGetForStation", TBL_SCANSGET = "BwareScanTable";
		public const string USP_SCANFINDINTABLE = "uspToolsSortMonScanFindInTable", TBL_SCANFINDINTABLE = "BwareScanTable";
        
        public const string SEARCH_SORTEDITEMS = "Sorted Items";
        public const string SEARCH_SORTEDITEMSARCHIVE = "Sorted Items Archive";
        public const string SEARCH_BEARWARESCANS = "Bearware Scans";
        public const string SEARCH_ARGIXLOG = "Argix Log";

        public const string USP_PANDA_DATAREQ = "uspLabelDataRequestGetList", TBL_DATAREQ = "PandaTable";
        public const string USP_PANDA_DATARES = "uspLabelDataResponseGetList", TBL_DATARES = "PandaTable";
        public const string USP_PANDA_VERIFYREQ = "uspVerifyLabelRequestGetList", TBL_VERIFYREQ = "PandaTable";
        public const string USP_PANDA_VERIFYRES = "uspVerifyLabelResponseGetList", TBL_VERIFYRES = "PandaTable";
        public const string USP_PANDA_FINDITEM = "uspFindSortedItem", TBL_FINDITEM = "PandaTable";
        public const string USP_PANDA_FINDCTN = "uspFindCarton", TBL_FINDCTN = "PandaTable";

		//Interface
		static App() { }
		private App() { }
		public static string EventLogName { get { return "SortMonitor"; } }
		public static string RegistryKey { get { return "SortMonitor"; } }
        public static void ReportError(Exception ex) { ReportError(ex,true,LogLevel.None); }
        public static void ReportError(Exception ex,bool displayMessage) { ReportError(ex,displayMessage,LogLevel.None); }
        public static void ReportError(Exception ex,bool displayMessage,LogLevel level) {
            //Report an exception to the user
            try {
                string src = (ex.Source != null) ? ex.Source + "-\n" : "";
                string msg = src + ex.Message;
                if(ex.InnerException != null) {
                    if((ex.InnerException.Source != null)) src = ex.InnerException.Source + "-\n";
                    msg = src + ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
                }
                if(displayMessage)
                    MessageBox.Show(msg,App.Product,MessageBoxButtons.OK,MessageBoxIcon.Error);
                if(level != LogLevel.None)
                    ArgixTrace.WriteLine(new TraceMessage(ex.ToString(),App.EventLogName,level));
            }
            catch(Exception) { }
        }
    }
	
	//Event support
	public delegate void ErrorEventHandler(object source, ErrorEventArgs e);
	public class ErrorEventArgs : EventArgs {
		private Exception _ex=null;
		private bool _displayMessage=false;
		private LogLevel _logLevel=LogLevel.None; 
		public ErrorEventArgs(Exception ex): this(ex, false, LogLevel.None) {}
		public ErrorEventArgs(Exception ex, bool displayMessage, LogLevel logLevel) {
			this._ex = ex;
			this._displayMessage = displayMessage;
			this._logLevel = logLevel;
		}
		public Exception Exception { get { return this._ex; } set { this._ex = value; } }
		public bool DisplayMessage { get { return this._displayMessage; } set { this._displayMessage = value; } }
		public LogLevel Level { get { return this._logLevel; } set { this._logLevel = value; } }
	}

	public delegate void StatusEventHandler(object source, StatusEventArgs e);
	public class StatusEventArgs : EventArgs {
		private string _message;
		public StatusEventArgs(string message) {
			this._message = message;
		}
		public string Message { get { return this._message; } set { this._message = value; } }
	}

}