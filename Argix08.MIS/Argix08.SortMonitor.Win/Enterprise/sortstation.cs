//	File:	workstation.cs
//	Author:	J. Heary
//	Date:	10/14/04
//	Desc:	Represents the state and behavior of a sort workstation.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;
using Argix.Data;
using Tsort.Freight;
using Tsort.Printers;

namespace Tsort.Enterprise {
	//
	public class SortStation {
		//Class members
		private string mWorkStationID="";
		private string mName="";
		private int mTerminalID=0;
		private string mNumber="";
		private string mDescription="";
		private string mScaleType="";
		private string mScalePort="";
		private string mPrinterType="";
		private string mPrinterPort="";
		private bool mTrace=false;
		private bool mIsActive=true;
        private EnterpriseTerminal mTerminal=null;
		private ZebraPrinter mZebraPrinter=null;
		
        private ArgixLogDS mLogDS=null;
        private string mTraceSQLConnection="";
		private OutboundFreightDS mItemDS=null;
		
		public event EventHandler LogEventsRefreshed=null;
        public event EventHandler ItemsRefreshed=null;
		
		//Interface
		public SortStation(): this(null, null) { }
		public SortStation(WorkstationDS.WorkstationDetailTableRow workstation, EnterpriseTerminal terminal) {
			//Constructor
			try {
				//Configure this station from the station configuration information
				this.mTerminal = terminal;
				if(workstation != null) {
					if(!workstation.IsWorkStationIDNull()) this.mWorkStationID = workstation.WorkStationID;
					if(!workstation.IsNameNull()) this.mName = workstation.Name.Trim();
					if(!workstation.IsTerminalIDNull()) this.mTerminalID = workstation.TerminalID;
					if(!workstation.IsNumberNull()) this.mNumber = workstation.Number.Trim();
					if(!workstation.IsDescriptionNull()) this.mDescription = workstation.Description.Trim();
					if(!workstation.IsScaleTypeNull()) this.mScaleType = workstation.ScaleType.Trim();
					if(!workstation.IsScalePortNull()) this.mScalePort = workstation.ScalePort.Trim();
					if(!workstation.IsPrinterTypeNull()) this.mPrinterType = workstation.PrinterType.Trim();
					if(!workstation.IsPrinterPortNull()) this.mPrinterPort = workstation.PrinterPort.Trim();
					if(!workstation.IsTraceNull()) this.mTrace = workstation.Trace;
					if(!workstation.IsIsActiveNull()) this.mIsActive = workstation.IsActive;
				}
				this.mZebraPrinter = new ZebraPrinter(this.mPrinterType);
				this.mLogDS = new ArgixLogDS();
                if(ConfigurationManager.AppSettings.Get(this.mName) != null) this.mTraceSQLConnection = ConfigurationManager.AppSettings.Get(this.mName);
				this.mItemDS = new OutboundFreightDS();
            } 
			catch(Exception ex) { throw ex; }
		}
		~SortStation() { }
		#region Accessors\Modifiers: [Members]..., ToDataSet()
		public string WorkStationID { get { return this.mWorkStationID; } }
		public string Name { get { return this.mName; } }
		public int TerminalID { get { return this.mTerminalID; } }
		public string Number { get { return this.mNumber; } }
		public string Description { get { return this.mDescription; } }
		public string ScaleType { get { return this.mScaleType; } }
		public string ScalePort { get { return this.mScalePort; } }
		public string PrinterType { get { return this.mPrinterType; } }
		public string PrinterPort { get { return this.mPrinterPort; } }
		public bool Trace { get { return this.mTrace; } }
		public bool IsActive { get { return this.mIsActive; } }		
		public DataSet ToDataSet() {
			//Return a dataset containing values for this workstation
			WorkstationDS ds=null;
			try {
				ds = new WorkstationDS();
				WorkstationDS.WorkstationDetailTableRow workstation = ds.WorkstationDetailTable.NewWorkstationDetailTableRow();
				if(this.mWorkStationID != "") workstation.WorkStationID = this.mWorkStationID;
				if(this.mName != "") workstation.Name = this.mName;
				if(this.mTerminalID > 0) workstation.TerminalID = this.mTerminalID;
				if(this.mNumber != "") workstation.Number = this.mNumber;
				if(this.mDescription != "") workstation.Description = this.mDescription;
				if(this.mScaleType != "") workstation.ScaleType = this.mScaleType;
				if(this.mScalePort != "") workstation.ScalePort = this.mScalePort;
				if(this.mPrinterType != "") workstation.PrinterType = this.mPrinterType;
				if(this.mPrinterPort != "") workstation.PrinterPort = this.mPrinterPort;
				workstation.Trace = this.mTrace;
				workstation.IsActive = this.mIsActive;
				ds.WorkstationDetailTable.AddWorkstationDetailTableRow(workstation);
			}
			catch(Exception) { }
			return ds;
		}
		#endregion
		public EnterpriseTerminal Terminal { get { return this.mTerminal; } }
		public ZebraPrinter Printer { get { return this.mZebraPrinter; } }
		public ArgixLogDS Log { get { return this.mLogDS; } }
        public string TraceSQLConnection { get { return this.mTraceSQLConnection; } }
        public OutboundFreightDS Items { get { return this.mItemDS; } }
        public void GetPrinterInfo() {
			//Check printer info
			//Keyword2=Zebra Status; Keyword3=Printer Information:
            //DataRow[] rows = logDS.ArgixLogTable.Select("Keyword2='Zebra Status' AND Keyword3='Printer Information'");
            //if(rows.Length > 0) {
            //    ArgixLogDS dsZebraEvents = new ArgixLogDS();
            //    dsZebraEvents.Merge(rows);
            //    this.mZebraPrinter.UpdateZebraInformation(dsZebraEvents.ArgixLogTable[dsZebraEvents.ArgixLogTable.Count-1].Message);
            //}
		}
		public void RefreshItems(DateTime start, DateTime end) {
			//Refresh sorted\scanned items
			try {
				this.mItemDS.Clear();
                if(this.mTerminal.GetType().Name == "TsortTerminal") {
                    TsortTerminal tterminal = (TsortTerminal)this.mTerminal;
				    this.mItemDS.Merge(tterminal.GetSortedItems(this.mNumber, start, end));
                }
                if(this.mTerminal.GetType().Name == "LocalTerminal") {
                    LocalTerminal lterminal = (LocalTerminal)this.mTerminal;
                	this.mItemDS.Merge(lterminal.GetSortedItems(this.mNumber, start, end));
                	this.mItemDS.Merge(lterminal.GetScannedItems(this.mNumber, start, end));
                }
			}
			catch (Exception ex) { throw ex; }
			finally { if(this.ItemsRefreshed!=null) this.ItemsRefreshed(this, EventArgs.Empty); }
		}
		public void RefreshLogEvents(DateTime start, DateTime end) {
			//Refresh log events
			try {
				this.mLogDS.Clear();
                if(this.mTraceSQLConnection.Length > 0)
                    this.mLogDS.Merge(getLogEvents(start, end));
                else
				    this.mLogDS.Merge(this.mTerminal.GetLogEvents(start, end, this.mName));
			}
			catch(Exception ex) { throw ex; }
			finally { if(this.LogEventsRefreshed!=null) this.LogEventsRefreshed(this, EventArgs.Empty); }
		}

        private ArgixLogDS getLogEvents(DateTime startDate, DateTime endDate) {
			//Read log events
            ArgixLogDS logDS=null;
			try {
				logDS = new ArgixLogDS();
                Mediator mediator = new SQLMediator(this.mTraceSQLConnection);
				DataSet ds = mediator.FillDataset(App.USP_LOGGET, App.TBL_LOGGET, new object[]{startDate.ToString("yyyy-MM-dd HH:mm:ss"), endDate.ToString("yyyy-MM-dd HH:mm:ss")});
				if(ds != null) logDS.Merge(ds);
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while reading log events.", ex); }
		    return logDS;
        }
    }
}
