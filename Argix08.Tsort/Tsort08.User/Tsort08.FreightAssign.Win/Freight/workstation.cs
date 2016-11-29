using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Argix.Freight {
	//
	public class Workstation {
		//Members
		private string mWorkStationID="";
		private string mName="";
		private int mTerminalID=0;
		private string mNumber="", mDescription="";
		private string mScaleType="", mScalePort="";
		private string mPrinterType="", mPrinterPort="";
		private bool mTrace=false, mIsActive=true;
		
		//Interface
		public Workstation(): this(null) { }
		public Workstation(WorkstationDS.WorkstationTableRow workstation) {
			//Constructor
			try {
				//Configure this station from the station configuration information
                if(workstation != null) {
                    this.mWorkStationID = workstation.WorkStationID;
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
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new workstation instance.", ex); }
		}
		#region Accessors\Modifiers: [Members...]
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
		#endregion
	}
}
