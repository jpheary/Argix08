//	File:	sortstation.cs
//	Author:	J. Heary
//	Date:	09/05/07
//	Desc:	Represents the state and behavior of a sort workstation.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;
using Tsort.Devices;
using Tsort.Devices.Printers;
using Tsort.Devices.Scales;
using Tsort.Enterprise;
using Tsort.IO.Ports;

namespace Tsort.Sort {
	//
	public class SortStation: Workstation {
		//Members
		private ILabelPrinter mLabelPrinter=null;
		private IScale mScale=null;
		
		//Events
		public event EventHandler PrinterChanged=null;
		public event EventHandler ScaleChanged=null;
		
		//Interface
		public SortStation(): this(null) { }
		public SortStation(WorkstationDS.WorkstationDetailTableRow workstation): base(workstation) { }
		#region Accessors\Modifiers: [Members]..., Printer, Scale, ToDataSet()
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
		
		public override DataSet ToDataSet() {
			//Return a dataset containing values for this workstation
			WorkstationDS ds=null;
			try {
				ds = new WorkstationDS();
				WorkstationDS.WorkstationDetailTableRow workstation = ds.WorkstationDetailTable.NewWorkstationDetailTableRow();
				workstation.WorkStationID = base.mWorkStationID;
				workstation.Name = base.mName;
				workstation.TerminalID = base.mTerminalID;
				workstation.Number = base.mNumber;
				workstation.Description = base.mDescription;
				if(this.mScale != null) {
					workstation.ScaleType = this.mScale.Type;
					workstation.ScalePort = this.mScale.Settings.PortName;
				}
				if(this.mLabelPrinter != null) {
					workstation.PrinterType = this.mLabelPrinter.Type;
					workstation.PrinterPort = this.mLabelPrinter.Settings.PortName;
				}
				workstation.Trace = base.mTrace;
				workstation.IsActive = base.mIsActive;
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
					NameValueCollection nvcPrinter = (NameValueCollection)ConfigurationSettings.GetConfig("station/printer");				
					oSettings.PortName = portName;
					oSettings.BaudRate = Convert.ToInt32(nvcPrinter["BaudRate"]);
					oSettings.DataBits = Convert.ToInt32(nvcPrinter["DataBits"]);
					switch(nvcPrinter["Parity"].ToString().ToLower()) {
						case "none":	oSettings.Parity = RS232Parity.None; break;
						case "even":	oSettings.Parity = RS232Parity.Even; break;
						case "odd":		oSettings.Parity = RS232Parity.Odd; break;
					}
					switch(Convert.ToInt32(nvcPrinter["StopBits"])) {
						case 1:			oSettings.StopBits = RS232StopBits.One; break;
						case 2:			oSettings.StopBits = RS232StopBits.Two; break;
					}
					switch(nvcPrinter["Handshake"].ToString().ToLower()) {
						case "none":	oSettings.Handshake = RS232Handshake.None; break;
						case "rts/cts":	oSettings.Handshake = RS232Handshake.RequestToSend; break;
						case "xon/xoff":oSettings.Handshake = RS232Handshake.XOnXOff; break;
					}
					oPrinter.Settings = oSettings;
					
					//Atach printer to sort station and turn-on
					if(this.mLabelPrinter != null) this.mLabelPrinter.TurnOff();
					this.mLabelPrinter = oPrinter;
					this.mLabelPrinter.TurnOn();
					
					//Notify clients
					if(this.PrinterChanged != null)
						this.PrinterChanged(this, new EventArgs());
				}
			} 
			catch(Exception ex) { throw ex; }
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
					NameValueCollection nvcScale = (NameValueCollection)ConfigurationSettings.GetConfig("station/scale");				
					oSettings.PortName = portName;
					oSettings.BaudRate = Convert.ToInt32(nvcScale["BaudRate"]);
					switch(nvcScale["Parity"].ToString().ToLower()) {
						case "none":	oSettings.Parity = RS232Parity.None; break;
						case "even":	oSettings.Parity = RS232Parity.Even; break;
						case "odd":		oSettings.Parity = RS232Parity.Odd; break;
					}
					oScale.Settings = oSettings;
					
					//Atach scale to sort station and turn-on
					if(this.mScale != null) this.mScale.TurnOff();
					this.mScale = oScale;
					this.mScale.TurnOn(500);
					
					//Notify clients
					if(this.ScaleChanged != null)
						this.ScaleChanged(this, new EventArgs());
				}
			} 
			catch(Exception ex) { throw ex; }
		}
	}
}