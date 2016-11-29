using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;

namespace Argix.Freight {
	//
	public class Workstation {
		//Members
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
		
		//Constants
		//Events
		//Interface
		public Workstation(): this("") { }
		public Workstation(WorkstationDS.WorkstationDetailTableRow workstation) {
			//Constructor
			try {
				//Configure this station from the station configuration information
				if(workstation != null) { 
					if(!workstation.IsWorkStationIDNull()) this.mWorkStationID = workstation.WorkStationID;
					if(!workstation.IsNameNull()) this.mName = workstation.Name;
					if(!workstation.IsTerminalIDNull()) this.mTerminalID = workstation.TerminalID;
					if(!workstation.IsNumberNull()) this.mNumber = workstation.Number;
					if(!workstation.IsDescriptionNull()) this.mDescription = workstation.Description;
					if(!workstation.IsScaleTypeNull()) this.mScaleType = workstation.ScaleType;
					if(!workstation.IsScalePortNull()) this.mScalePort = workstation.ScalePort;
					if(!workstation.IsPrinterTypeNull()) this.mPrinterType = workstation.PrinterType;
					if(!workstation.IsPrinterPortNull()) this.mPrinterPort = workstation.PrinterPort;
					if(!workstation.IsTraceNull()) this.mTrace = workstation.Trace;
					if(!workstation.IsIsActiveNull()) this.mIsActive = workstation.IsActive;
				}
			} 
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Workstation instance.", ex); }
		}
		public Workstation(string number) { 
			this.mNumber = number;
		}
		#region Accessors\Modifiers: ..., ToString(), ToDataSet()
		public string WorkStationID { get { return this.mWorkStationID; } }
		public string Name { get { return this.mName; } }
		public int TerminalID { get { return this.mTerminalID; } }
		public string Number { get { return this.mNumber; } }
		public string Description { get { return this.mDescription; } }
		public bool Trace { get { return this.mTrace; } }
		public bool IsActive { get { return this.mIsActive; } }
		
		public DataSet ToDataSet() {
			//Return a dataset containing values for this workstation
			WorkstationDS ds=null;
			try {
				ds = new WorkstationDS();
				WorkstationDS.WorkstationDetailTableRow workstation = ds.WorkstationDetailTable.NewWorkstationDetailTableRow();
				workstation.WorkStationID = this.mWorkStationID;
				workstation.Name = this.mName;
				workstation.TerminalID = this.mTerminalID;
				workstation.Number = this.mNumber;
				workstation.Description = this.mDescription;
				workstation.ScaleType = this.mScaleType;
				workstation.ScalePort = this.mScalePort;
				workstation.PrinterType = this.mPrinterType;
				workstation.PrinterPort = this.mPrinterPort;
				workstation.Trace = this.mTrace;
				workstation.IsActive = this.mIsActive;
				ds.WorkstationDetailTable.AddWorkstationDetailTableRow(workstation);
				ds.AcceptChanges();
			}
			catch(Exception) { }
			return ds;
		}
		public override string ToString() {
			//Custom ToString() method
			string sThis=base.ToString();
			try {
				//Form string detail of this object
				StringBuilder builder = new StringBuilder();
				builder.Append("Workstation ------------------\n");
				builder.Append("\tWorkStationID=" + this.mWorkStationID + "\n");
				builder.Append("\tName=" + this.mName + "\n");
				builder.Append("\tNumber=" + this.mNumber + "\n");
				builder.Append("\tDescription=" + this.mDescription + "\n");
				builder.Append("\tTerminalID=" + this.mTerminalID.ToString() + "\n");
				builder.Append("\tIsActive=" + this.mIsActive.ToString() + "\n");
				builder.Append("\tScaleType=" + this.mScaleType + "\n");
				builder.Append("\tScalePort=" + this.mScalePort + "\n");
				builder.Append("\tPrinterType=" + this.mPrinterType + "\n");
				builder.Append("\tPrinterPort=" + this.mPrinterPort + "\n");
				builder.Append("\tTrace=" + this.mTrace.ToString() + "\n");
				builder.Append("------------------------------\n");
				sThis = builder.ToString();
			} 
			catch(Exception) { }
			return sThis;
		}
		#endregion
	}
}