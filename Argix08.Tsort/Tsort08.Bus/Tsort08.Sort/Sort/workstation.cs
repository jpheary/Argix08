//	File:	workstation.cs
//	Author:	J. Heary
//	Date:	01/12/05
//	Desc:	Represents the state and behavior of a sort workstation.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Collections.Specialized;
using System.IO.Ports;
using System.Reflection;
using System.Text;
using Tsort.Devices;
using Tsort.Devices.Printers;
using Tsort.Devices.Scales;
using Tsort.Enterprise;

namespace Tsort.Sort {
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
		private ILabelPrinter mLabelPrinter=null;
		private IScale mScale=null;
		private Statistics mStatistics=null;
		
		//Constants
		//Events
		public event EventHandler PrinterChanged=null;
		public event EventHandler ScaleChanged=null;
		
		//Interface
		public Workstation(): this(null) { }
		public Workstation(WorkstationDS.WorkstationDetailTableRow workstation) {
			//Constructor
			try {
				//Configure this station from the station configuration information
				if(workstation != null) { 
					this.mWorkStationID = workstation.WorkStationID;
					if(!workstation.IsNameNull()) this.mName = workstation.Name;
					this.mTerminalID = workstation.TerminalID;
					this.mNumber = workstation.Number;
					if(!workstation.IsDescriptionNull()) this.mDescription = workstation.Description;
					if(!workstation.IsScaleTypeNull()) this.mScaleType = workstation.ScaleType;
					if(!workstation.IsScalePortNull()) this.mScalePort = workstation.ScalePort;
					if(!workstation.IsPrinterTypeNull()) this.mPrinterType = workstation.PrinterType;
					if(!workstation.IsPrinterPortNull()) this.mPrinterPort = workstation.PrinterPort;
					if(!workstation.IsTraceNull()) this.mTrace = workstation.Trace;
					this.mIsActive = workstation.IsActive;
				}
				this.mStatistics = new Statistics();
			} 
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating Workstation instance.", ex); }
		}
		#region Accessors\Modifiers: [Members]..., Printer, Scale, ToDataSet()
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
		public ILabelPrinter Printer { 
			get { return this.mLabelPrinter; } 
			set { 
				//Set printer and notify clients
				this.mLabelPrinter = value; 
				if(this.PrinterChanged != null) this.PrinterChanged(this, new EventArgs());
			} 
		}
		public IScale Scale { 
			get { return this.mScale; } 
			set { 
				//Set scale and notify clients
				this.mScale = value;
				if(this.ScaleChanged != null) this.ScaleChanged(this, new EventArgs());
			} 
		}
		public DataSet ToDataSet() {
			//Return a dataset containing values for this workstation
			WorkstationDS ds=null;
			try {
				ds = new WorkstationDS();
				WorkstationDS.WorkstationDetailTableRow workstation = ds.WorkstationDetailTable.NewWorkstationDetailTableRow();
				workstation.WorkStationID = this.mWorkStationID;
				if(this.mName.Length > 0) workstation.Name = this.mName;
				workstation.TerminalID = this.mTerminalID;
				workstation.Number = this.mNumber;
				if(this.mDescription.Length > 0) workstation.Description = this.mDescription;
				if(this.mScaleType.Length > 0) workstation.ScaleType = this.mScaleType;
				if(this.mScalePort.Length > 0) workstation.ScalePort = this.mScalePort;
				if(this.mPrinterType.Length > 0) workstation.PrinterType = this.mPrinterType;
				if(this.mPrinterPort.Length > 0) workstation.PrinterPort = this.mPrinterPort;
				workstation.Trace = this.mTrace;
				workstation.IsActive = this.mIsActive;
				ds.WorkstationDetailTable.AddWorkstationDetailTableRow(workstation);
				ds.AcceptChanges();
			}
			catch(Exception) { }
			return ds;
		}
		#endregion
		public void SetDefaultPrinter() { ChangePrinter(this.mPrinterType,this.mPrinterPort); }
		public void ChangePrinter(string printerType) { ChangePrinter(printerType,this.mPrinterPort); }
		public void ChangePrinter(string printerType, string portName) { 
			//Change station printer
			try {
				if(printerType != "" && portName != "") {
					ILabelPrinter oPrinter = DeviceFactory.CreatePrinter(printerType, portName);
					PortSettings oSettings = oPrinter.DefaultSettings;
					NameValueCollection nvcPrinter = (NameValueCollection)ConfigurationManager.GetSection("station/printer");				
					oSettings.PortName = portName;
					oSettings.BaudRate = Convert.ToInt32(nvcPrinter["BaudRate"]);
					oSettings.DataBits = Convert.ToInt32(nvcPrinter["DataBits"]);
					switch(nvcPrinter["Parity"].ToString().ToLower()) {
                        case "none": oSettings.Parity = Parity.None; break;
						case "even":	oSettings.Parity = Parity.Even; break;
						case "odd":		oSettings.Parity = Parity.Odd; break;
					}
					switch(Convert.ToInt32(nvcPrinter["StopBits"])) {
						case 1:			oSettings.StopBits = StopBits.One; break;
						case 2:			oSettings.StopBits = StopBits.Two; break;
					}
					switch(nvcPrinter["Handshake"].ToString().ToLower()) {
						case "none":	oSettings.Handshake = Handshake.None; break;
						case "rts/cts":	oSettings.Handshake = Handshake.RequestToSend; break;
						case "xon/xoff":oSettings.Handshake = Handshake.XOnXOff; break;
					}
					oPrinter.Settings = oSettings;
					
					//Atach printer to sort station and turn-on
					if(this.mLabelPrinter != null) this.mLabelPrinter.TurnOff();
					this.mLabelPrinter = oPrinter;
					this.mLabelPrinter.TurnOn();
				}
			} 
			catch(System.IO.IOException) { }
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error while setting the printer.", ex); }
			finally { if(this.PrinterChanged != null) this.PrinterChanged(this, new EventArgs()); }
		}
		public void SetDefaultScale() { ChangeScale(this.mScaleType,this.mScalePort); }
		public void ChangeScale(string scaleType) { ChangeScale(scaleType,this.mScalePort); }
		public void ChangeScale(string scaleType, string portName) { 
			//Change station scale
			try {
				if(scaleType != "" && portName != "") {
					//Create and configure a scale
					IScale oScale = DeviceFactory.CreateScale(scaleType, portName);
					PortSettings oSettings = oScale.DefaultSettings;
                    NameValueCollection nvcScale = (NameValueCollection)ConfigurationManager.GetSection("station/scale");				
					oSettings.PortName = portName;
					oSettings.BaudRate = Convert.ToInt32(nvcScale["BaudRate"]);
					switch(nvcScale["Parity"].ToString().ToLower()) {
						case "none":	oSettings.Parity = Parity.None; break;
						case "even":	oSettings.Parity = Parity.Even; break;
						case "odd":		oSettings.Parity = Parity.Odd; break;
					}
					oScale.Settings = oSettings;
					
					//Atach scale to sort station and turn-on
					if(this.mScale != null) this.mScale.TurnOff();
					this.mScale = oScale;
					this.mScale.TurnOn();
				}
			} 
			catch(System.IO.IOException) { }
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error while setting the scale.", ex); }
			finally { if(this.ScaleChanged != null) this.ScaleChanged(this, new EventArgs()); }
		}
		public Statistics SortStatistics { get {return this.mStatistics;} }
	}
}