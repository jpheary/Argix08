using System;
using System.Data;
using System.Configuration;
using System.Collections.Specialized;
using System.IO.Ports;
using System.Reflection;
using System.Text;
using Tsort.Devices;
using Tsort.Devices.Printers;

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
		private ILabelPrinter mLabelPrinter=null;
		
		public event EventHandler PrinterChanged=null;
		
		//Interface
		public Workstation(): this(null) { }
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
			catch(Exception ex) { throw new ApplicationException("Unexpected error creating new Workstation instance.", ex); }
		}
		#region Accessors\Modifiers: [Members...]
		public string WorkStationID { get { return this.mWorkStationID; } }
		public string Name { get { return this.mName; } set { this.mName = value; } }
		public int TerminalID { get { return this.mTerminalID; } set { this.mTerminalID = value; } }
		public string Number { get { return this.mNumber; } set { this.mNumber = value; } }
		public string Description { get { return this.mDescription; } set { this.mDescription = value; } }
		public bool Trace { get { return this.mTrace; } set { this.mTrace = value; } }
		public bool IsActive { get { return this.mIsActive; } set { this.mIsActive = value; } }
		public string PrinterType { get { return this.mPrinterType; } set { this.mPrinterType = value; } }
		public string PrinterPort { get { return this.mPrinterPort; } set { this.mPrinterPort = value; } }
		public ILabelPrinter Printer { 
			get { return this.mLabelPrinter; } 
			set { 
				//Set printer and notify clients
				this.mLabelPrinter = value; 
				if(this.PrinterChanged != null) this.PrinterChanged(this, new EventArgs());
			} 
		}
		#endregion
		#region Printer Config: SetDefaultPrinter(), ChangePrinter()
		public void SetDefaultPrinter() { ChangePrinter(this.mPrinterType,this.mPrinterPort); }
		public void ChangePrinter(string printerType) { ChangePrinter(printerType,this.mPrinterPort); }
		public void ChangePrinter(string printerType, string portName) { 
			//Change station printer
            try {
                if(printerType != "" && portName != "") {
                    ILabelPrinter oPrinter = DeviceFactory.CreatePrinter(printerType,portName);
                    PortSettings oSettings = oPrinter.DefaultSettings;
                    NameValueCollection nvcPrinter = (NameValueCollection)ConfigurationSettings.GetConfig("station/printer");
                    oSettings.PortName = portName;
                    oSettings.BaudRate = Convert.ToInt32(nvcPrinter["BaudRate"]);
                    oSettings.DataBits = Convert.ToInt32(nvcPrinter["DataBits"]);
                    switch(nvcPrinter["Parity"].ToString().ToLower()) {
                        case "none": oSettings.Parity = Parity.None; break;
                        case "even": oSettings.Parity = Parity.Even; break;
                        case "odd": oSettings.Parity = Parity.Odd; break;
                    }
                    switch(Convert.ToInt32(nvcPrinter["StopBits"])) {
                        case 1: oSettings.StopBits = StopBits.One; break;
                        case 2: oSettings.StopBits = StopBits.Two; break;
                    }
                    switch(nvcPrinter["Handshake"].ToString().ToLower()) {
                        case "none": oSettings.Handshake = Handshake.None; break;
                        case "rts/cts": oSettings.Handshake = Handshake.RequestToSend; break;
                        case "xon/xoff": oSettings.Handshake = Handshake.XOnXOff; break;
                    }
                    oPrinter.Settings = oSettings;

                    //Atach printer to sort station and turn-on
                    if(this.mLabelPrinter != null) this.mLabelPrinter.TurnOff();
                    this.mLabelPrinter = oPrinter;
                    this.mLabelPrinter.TurnOn();

                    //Notify clients
                    if(this.PrinterChanged != null) this.PrinterChanged(this,new EventArgs());
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error changing to workstation printer type " + printerType + " on port " + portName + ".",ex); }
		}
		#endregion
	}
}