using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Collections.Specialized;
using System.IO.Ports;
using System.Text;
using Argix.Data;
using Tsort.Devices;
using Tsort.Devices.Printers;

namespace Argix.Enterprise {
	//
	public class Workstation {
		//Members
		private string mWorkStationID="";
		private string mName=Environment.MachineName;
		private int mTerminalID=0;
		private string mNumber="", mDescription="";
		private string mScaleType="", mScalePort="";
		private string mPrinterType="", mPrinterPort="";
		private bool mTrace=false, mIsActive=true;
		private ILabelPrinter mPrinter=null;
		
		public event EventHandler PrinterChanged=null;
		
		//Interface
		public Workstation() : this(null) { }
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
			catch(Exception ex) { throw new ApplicationException("Could not create a new workstation.", ex); }
		}
		~Workstation() {
			//Destructor
			try {
				if(this.mPrinter.On) this.mPrinter.TurnOff();
				this.mPrinter = null;
			}
			catch(Exception) { }
		}		
		#region Accessors\Modifiers: [Members...], ToDataSet()
		public string WorkStationID { get { return this.mWorkStationID; } set { this.mWorkStationID = value; } }
		public string Name { get { return this.mName; } set { this.mName = value; } }
		public int TerminalID { get { return this.mTerminalID; } set { this.mTerminalID = value; } }
		public string Number { get { return this.mNumber; } set { this.mNumber = value; } }
		public string Description { get { return this.mDescription; } set { this.mDescription = value; } }
		public string ScaleType { get { return this.mScaleType; } set { this.mScaleType = value; } }
		public string ScalePort { get { return this.mScalePort; } set { this.mScalePort = value; } }
		public string PrinterType { get { return this.mPrinterType; } set { this.mPrinterType = value; } }
		public string PrinterPort { get { return this.mPrinterPort; } set { this.mPrinterPort = value; } }
		public bool Trace { get { return this.mTrace; } set { this.mTrace = value; } }
		public bool IsActive { get { return this.mIsActive; } set { this.mIsActive = value; } }
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
		public ILabelPrinter LabelPrinter { get { return this.mPrinter; } }
		public void SetDefaultLabelPrinter() { SetLabelPrinter(this.mPrinterType, this.mPrinterPort); }
		public void SetLabelPrinter(string printerType) { SetLabelPrinter(printerType, this.mPrinterPort); }
		public void SetLabelPrinter(string printerType, string portName) { 
			//
			try {
				//Validate printer type and port name
				if(!DeviceFactory.PrinterTypeExists(printerType)) throw new ArgumentException("'" + printerType + "' is an invalid printer type.");
				if(portName.Length <= 0) throw new ArgumentException("'" + portName + "' is an invalid port name.");
					
				//Create/change the attached label printer
				if(this.mPrinter != null) { 
					//A current printer exists: check for change to printer type
					if(printerType != this.mPrinter.Type) {
						//New printer type: disconnect current printer and attach a new one
						//with the same settings (except use the specified port name)
						bool bOn = this.mPrinter.On;
						if(bOn) this.mPrinter.TurnOff();
						PortSettings settings = this.mPrinter.Settings;
						settings.PortName = portName;
						ILabelPrinter printer = DeviceFactory.CreatePrinter(printerType, portName);
						if(printer == null)
							throw new ApplicationException("Printer " + printerType + " on port " + portName + " could not be created.");
						printer.Settings = settings;
						this.mPrinter = printer;
						if(bOn) this.mPrinter.TurnOn();
					}
					else {
						//Same printer type: check for change in printer port
						if(portName != this.mPrinter.Settings.PortName) {
							//Change port on existing printer
							PortSettings settings = this.mPrinter.Settings;
							settings.PortName = portName;
							this.mPrinter.Settings = settings;
						}
					}
				}
				else {
					//Create a new label printer
					this.mPrinter = DeviceFactory.CreatePrinter(printerType, portName);
					if(this.mPrinter == null) throw new ApplicationException("Printer " + printerType + " on port " + portName + " could not be created.");
										
					//Set printer configuration; start with printer's default settings
					PortSettings settings = this.mPrinter.DefaultSettings;
					NameValueCollection nvcPrinter = (NameValueCollection)ConfigurationManager.GetSection("station/printer");				
					settings.PortName = portName;
					settings.BaudRate = Convert.ToInt32(nvcPrinter["BaudRate"]);
					settings.DataBits = Convert.ToInt32(nvcPrinter["DataBits"]);
					switch(nvcPrinter["Parity"].ToString().ToLower()) {
						case "none":	settings.Parity = System.IO.Ports.Parity.None; break;
						case "even":	settings.Parity = System.IO.Ports.Parity.Even; break;
						case "odd":		settings.Parity = System.IO.Ports.Parity.Odd; break;
					}
					switch(Convert.ToInt32(nvcPrinter["StopBits"])) {
						case 1:			settings.StopBits = System.IO.Ports.StopBits.One; break;
						case 2:			settings.StopBits = System.IO.Ports.StopBits.Two; break;
					}
					switch(nvcPrinter["Handshake"].ToString().ToLower()) {
						case "none":	settings.Handshake = System.IO.Ports.Handshake.None; break;
						case "rts/cts":	settings.Handshake = System.IO.Ports.Handshake.RequestToSend; break;
						case "xon/xoff":settings.Handshake = System.IO.Ports.Handshake.XOnXOff; break;
					}
					this.mPrinter.Settings = settings;
				}	
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error setting the label printer.", ex); }
			finally { if(this.PrinterChanged != null) this.PrinterChanged(this, new EventArgs()); }
		}
		public void ConfigureLabelPrinter() {
			//Configure label printer settings
			try {
				if(this.mPrinter != null) {
                    dlgPortSetup dlg = new dlgPortSetup();
					dlg.PortSettings = this.mPrinter.Settings;
					if(dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
						this.mPrinter.Settings = dlg.PortSettings;
					
					//This will restart the printer if it was off due to an 
					//invalid port name on the last configuration
					if(!this.mPrinter.On) this.mPrinter.TurnOn();
				}
				else
					throw new InvalidOperationException("Printer has not been created.");
			} 
			catch(Exception ex) { throw new ApplicationException("Unexpected error configuring the label printer.", ex); }
		}
	}
}
