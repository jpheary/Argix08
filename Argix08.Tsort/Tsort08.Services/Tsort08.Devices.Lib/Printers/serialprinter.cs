//	File:	comprinter.cs
//	Author:	J. Heary
//	Date:	11/10/08
//	Desc:	Implementation of ILabelPrinter to print to the device attached to the 
//          serial port.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO.Ports;

namespace Tsort.Devices.Printers {
    /// <summary>Implementation of ILabelPrinter to print to the device attached to the serial port.</summary>
	public class SerialPrinter : SerialPort, ILabelPrinter {
		//Members
        private PortSettings mSettings;
		
        /// <summary>Event notification for printer turned on.</summary>
        public event EventHandler PrinterTurnedOn = null;
        /// <summary>Event notification for printer turned off.</summary>
        public event EventHandler PrinterTurnedOff = null;
        /// <summary>Event notification for printer settings changed.</summary>
        public event EventHandler PrinterSettingsChanged = null;
        /// <summary>Event notification for printer pin and error events.</summary>
        public event PrinterEventHandler PrinterEventChanged = null;
		
		//Interface
        /// <summary>Constructor.</summary>
        /// <param name="portName">Name of serial port.</param>
        public SerialPrinter(string portName) : this(portName,PortSettings.DEFAULT_BAUD,PortSettings.DEFAULT_DATABITS,PortSettings.DEFAULT_PARITY,PortSettings.DEFAULT_STOPBITS) { }
        /// <summary>Constructor.</summary>
        /// <param name="portName">Name of serial port.</param>
		/// <param name="baudRate">Initial communications baud rate.</param>
        /// <param name="dataBits">Initial communications data bits.</param>
        /// <param name="parity">Initial communications parity.</param>
        /// <param name="stopBits">Initial communications stop bits.</param>
        public SerialPrinter(string portName, int baudRate, int dataBits, Parity parity, StopBits stopBits) : base(portName, baudRate, parity, dataBits, stopBits) { 
			//Constructor
			try {
                this.mSettings = new PortSettings(portName,baudRate,dataBits,parity,stopBits,PortSettings.DEFAULT_HANDSHAKE);
                //base.DataReceived += new SerialDataReceivedEventHandler(OnDataReceived);
                base.PinChanged += new SerialPinChangedEventHandler(OnPinChanged);
                base.ErrorReceived += new SerialErrorReceivedEventHandler(OnErrorReceived);
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new SerialPrinter instance.", ex); }
		}
        /// <summary>Destructor.</summary>
		~SerialPrinter() { try { base.Close(); } catch (Exception) {} }
        #region SerialPort: 
        public bool CDHolding { get { return base.CDHolding; } }
        public bool CtsHolding { get { return base.CtsHolding; } }
        public bool DsrHolding { get { return base.DsrHolding; } }
        public bool DtrEnable { get { return base.DtrEnable; } set { base.DtrEnable = value; } }
        public bool RtsEnable { get { return base.RtsEnable; } set { base.RtsEnable = value; } }
        #endregion
        #region IDevice: DefaultSettings, Settings, On, TurnOn(), TurnOff()
        /// <summary>Returns a PortSettings structure with the default COM port settings.</summary>
        public PortSettings DefaultSettings { get { return new PortSettings(base.PortName,PortSettings.DEFAULT_BAUD,PortSettings.DEFAULT_DATABITS,PortSettings.DEFAULT_PARITY,PortSettings.DEFAULT_STOPBITS,PortSettings.DEFAULT_HANDSHAKE); } }
        /// <summary>Gets or sets the the current port settings (if applicable).</summary>
        public PortSettings Settings {
            get { return this.mSettings; }
            set {
                try {
                    this.mSettings = value;
                    base.BaudRate = value.BaudRate;
                    base.Parity = value.Parity;
                    base.DataBits = value.DataBits;
                    base.StopBits = value.StopBits;
                    base.Handshake = value.Handshake;
                    if(value.PortName != base.PortName) {
                        bool open = base.IsOpen;
                        if(open) TurnOff();
                        base.PortName = value.PortName;
                        if(open) TurnOn();
                    }
                    if(this.PrinterSettingsChanged != null) this.PrinterSettingsChanged(this,new EventArgs());
                }
                catch(System.IO.FileNotFoundException ex) { throw new ApplicationException("COM port " + base.PortName + " not found.",ex); }
                catch(Exception ex) { new ApplicationException("Unexpected error while changing the COM port settings.",ex); }
            }
        }
        /// <summary>Gets a value indicating if the printer is on.</summary>
        public bool On { get { return (base.IsOpen && (base.IsOpen ? base.DsrHolding : false) && (base.IsOpen ? base.CDHolding : false)); } }
        /// <summary>Turns the printer on.</summary>
        public void TurnOn() {
            if(!base.IsOpen) {
                base.Open();
                if(this.PrinterTurnedOn != null) this.PrinterTurnedOn(this,new EventArgs());
            }
        }
        /// <summary>Turns the printer off.</summary>
        public void TurnOff() {
            if(base.IsOpen) {
                base.Close();
                if(this.PrinterTurnedOff != null) this.PrinterTurnedOff(this,new EventArgs());
            }
        }
        #endregion
        #region ILabelPrinter: Type, Print
        /// <summary>Gets the type of printer.</summary>
        public string Type { get { return "Serial"; } }
        /// <summary>Print the specified label format using SerialPort.Write(string).</summary>
        /// <param name="labelFormat">A string containing label zpl.</param>
        public void Print(string labelFormat) {
            //SerialPrinter.PrintLabel implementation
            try {
                if(!base.IsOpen) throw new ApplicationException("SerialPort is not open.");
                base.Write(labelFormat);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { new ApplicationException("Unexpected error while printing to the COM port.",ex); }
        }
        #endregion
        #region SerialPort Event Handlers: OnDataReceived(), OnPinChanged(), OnErrorReceived()
        private void OnDataReceived(object sender,SerialDataReceivedEventArgs e) {
			//Event handler for data events
		}
		private void OnPinChanged(object sender,SerialPinChangedEventArgs e) {
			//Event handler for pin change events
			if(this.PrinterEventChanged != null) this.PrinterEventChanged(this, new PrinterEventArgs(e.EventType));
		}
		private void OnErrorReceived(object sender,SerialErrorReceivedEventArgs e) {
			//Event handler for error events
			if(this.PrinterEventChanged != null) this.PrinterEventChanged(this, new PrinterEventArgs(e.EventType));
        }
        #endregion
    }
}
