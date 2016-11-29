//	File:	sortstation.cs
//	Author:	J. Heary
//	Date:	10/18/07
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

namespace Tsort.Sort {
	//
	public class SortStation {
		//Members
        private string mWorkStationID = "";
        private string mName = "";
        private int mTerminalID = 0;
        private string mNumber = "";
        private string mDescription = "";
        private string mScaleType = "";
        private string mScalePort = "";
        private string mPrinterType = "";
        private string mPrinterPort = "";
        private bool mTrace = false;
        private bool mIsActive = true;
        private ILabelPrinter mLabelPrinter = null;
        private IScale mScale = null;
		
		//Events
		public event EventHandler PrinterChanged=null;
		public event EventHandler ScaleChanged=null;
		
		//Interface
		public SortStation(): this(null) { }
		public SortStation(SortStationDS.SortStationTableRow workstation) {
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
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating Workstation instance.",ex); }
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
		public DataSet ToDataSet() { return new DataSet(); }
		#endregion
		
		public void SetDefaultPrinter() { ChangePrinter(this.mPrinterType,this.mPrinterPort); }
		public void ChangePrinter(string printerType) { ChangePrinter(printerType,this.mPrinterPort); }
		public void ChangePrinter(string printerType, string portName) { 
			//Change station printer
			try {
				if(printerType != "" && portName != "") {
					ILabelPrinter printer = DeviceFactory.CreatePrinter(printerType, portName);
					PortSettings settings = printer.DefaultSettings;
					NameValueCollection nvcPrinter = (NameValueCollection)ConfigurationSettings.GetConfig("station/printer");				
					settings.PortName = portName;
					settings.BaudRate = Convert.ToInt32(nvcPrinter["BaudRate"]);
					settings.DataBits = Convert.ToInt32(nvcPrinter["DataBits"]);
					switch(nvcPrinter["Parity"].ToString().ToLower()) {
						case "none":	settings.Parity = Parity.None; break;
						case "even":	settings.Parity = Parity.Even; break;
						case "odd":		settings.Parity = Parity.Odd; break;
					}
					switch(Convert.ToInt32(nvcPrinter["StopBits"])) {
						case 1:			settings.StopBits = StopBits.One; break;
						case 2:			settings.StopBits = StopBits.Two; break;
					}
					switch(nvcPrinter["Handshake"].ToString().ToLower()) {
						case "none":	settings.Handshake = Handshake.None; break;
						case "rts/cts":	settings.Handshake = Handshake.RequestToSend; break;
						case "xon/xoff":settings.Handshake = Handshake.XOnXOff; break;
					}
					printer.Settings = settings;
					
					//Atach printer to sort station and turn-on
					if(this.mLabelPrinter != null) this.mLabelPrinter.TurnOff();
					this.mLabelPrinter = printer;
					this.mLabelPrinter.TurnOn();
					if(this.PrinterChanged != null) this.PrinterChanged(this, new EventArgs());
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
					IScale scale = DeviceFactory.CreateScale(scaleType, portName);
					PortSettings settings = scale.DefaultSettings;
                    NameValueCollection nvcScale = (NameValueCollection)ConfigurationManager.GetSection("station/scale");				
					settings.PortName = portName;
					settings.BaudRate = Convert.ToInt32(nvcScale["BaudRate"]);
					switch(nvcScale["Parity"].ToString().ToLower()) {
						case "none":	settings.Parity = Parity.None; break;
						case "even":	settings.Parity = Parity.Even; break;
						case "odd":		settings.Parity = Parity.Odd; break;
					}
					scale.Settings = settings;
					
					//Atach scale to sort station and turn-on
					if(this.mScale != null) this.mScale.TurnOff();
					this.mScale = scale;
					this.mScale.TurnOn(500);
					if(this.ScaleChanged != null) this.ScaleChanged(this, new EventArgs());
				}
			} 
			catch(Exception ex) { throw ex; }
		}
	}
}