//	File:	consoleprinter.cs
//	Author:	J. Heary
//	Date:	11/10/08
//	Desc:	Implementation of ILabelPrinter where printing is directed to the console.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Diagnostics;

namespace Tsort.Devices.Printers {
    /// <summary>Implementation of ILabelPrinter where printing is directed to the console.</summary>
	public class ConsolePrinter : ILabelPrinter {
		//Members
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
        public ConsolePrinter() { }
        #region IDevice: DefaultSettings, Settings, On, TurnOn(), TurnOff()
        /// <summary>Gets a PortSettings structure initialized with the default port settings (if applicable).</summary>
        public PortSettings DefaultSettings { get { return new PortSettings(); } }
        /// <summary>Gets or sets the the current port settings (if applicable).</summary>
        public PortSettings Settings {
            get { return new PortSettings(); }
            set { if(this.PrinterSettingsChanged != null) this.PrinterSettingsChanged(this,new EventArgs()); }
        }
        /// <summary>Gets a value indicating if the printer is on.</summary>
        public bool On { get { return true; } }
        /// <summary>Turns the printer on.</summary>
        public void TurnOn() { if(this.PrinterTurnedOn != null) this.PrinterTurnedOn(this,new EventArgs()); }
        /// <summary>Turns the printer off.</summary>
        public void TurnOff() { }
        #endregion
        #region ILabelPrinter: Type, Print
        /// <summary>Gets the type of printer.</summary>
        public string Type { get { return "Console"; } }
        /// <summary>Sends labelFormat to the console.</summary>
        /// <param name="labelFormat">A string containing label data.</param>
        public void Print(string labelFormat) { Console.WriteLine(labelFormat); }
        public bool CDHolding { get { return false; } }
        public bool CtsHolding { get { return false; } }
        public bool DsrHolding { get { return false; } }
        public bool DtrEnable { get { return false; } set { ; } }
        public bool RtsEnable { get { return false; } set { ; } }
        #endregion
    }
}
