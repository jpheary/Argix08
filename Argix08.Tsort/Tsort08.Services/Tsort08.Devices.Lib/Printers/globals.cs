//	File:	globals.cs
//	Author:	J. Heary
//	Date:	11/10/08
//	Desc:	Printer interfaces, enumerators, event delegates, eventargs, exceptions, etc.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Runtime.Serialization;
using System.IO.Ports;

namespace Tsort.Devices.Printers {
	//Interfaces
    /// <summary>Interface definition for Tsort printers.</summary>
    public interface ILabelPrinter: IDevice {
        /// <summary>Gets the type of printer.</summary>
        string Type { get; }
        /// <summary>Print the specified label format using SerialPort.Write(string).</summary>
        /// <param name="labelFormat">A string containing label zpl.</param>
        void Print(string labelFormat);

        ///<summary></summary>
        bool CDHolding { get; }
        ///<summary></summary>
        bool CtsHolding { get; }
        ///<summary></summary>
        bool DsrHolding { get; }
        ///<summary></summary>
        bool DtrEnable { get; set; }
        ///<summary></summary>
        bool RtsEnable { get; set; }

        /// <summary>Event notification for printer turned on.</summary>
        event EventHandler PrinterTurnedOn;
        /// <summary>Event notification for printer turned off.</summary>
        event EventHandler PrinterTurnedOff;
        /// <summary>Event notification for printer settings changed.</summary>
        event EventHandler PrinterSettingsChanged;
        /// <summary>Event notification for printer pin and error events.</summary>
        event PrinterEventHandler PrinterEventChanged;
	}
	
    /// <summary>Represents the method that will handle a printer data event.</summary>
    public delegate void PrinterDataHandler(object source,PrinterDataArgs e);
    /// <summary>Represents the method that will handle a printer pin change or error event.</summary>
    public delegate void PrinterEventHandler(object source,PrinterEventArgs e);
    /// <summary>Represents the method that will handle a printer information data event.</summary>
    public delegate void PrinterInfoHandler(object source,PrinterInfoArgs e);
    /// <summary>Represents the method that will handle a printer memory status data event.</summary>
    public delegate void PrinterMemoryStatusHandler(object source,PrinterMemoryStatusArgs e);
    /// <summary>Represents the method that will handle a printer status data event.</summary>
    public delegate void PrinterStatusHandler(object source,PrinterStatusArgs e);

    /// <summary>Provides data for the printer send\receive data events.</summary>
    public class PrinterDataArgs:EventArgs {
		private string _data;
		public PrinterDataArgs(string data) { this._data = data; }
		public string Data { get { return this._data; } set { this._data = value; } }
	}
    /// <summary>Provides data for the printer [serial port] pin\error events.</summary>
    public class PrinterEventArgs:EventArgs {
        private System.IO.Ports.SerialData _eventData;
        private System.IO.Ports.SerialError _eventError;
        private System.IO.Ports.SerialPinChange _eventPinChange;
        public PrinterEventArgs(System.IO.Ports.SerialData eventData) { this._eventData = eventData;  }
        public PrinterEventArgs(System.IO.Ports.SerialError eventError) { this._eventError = eventError;  }
        public PrinterEventArgs(System.IO.Ports.SerialPinChange eventPinChange) { this._eventPinChange = eventPinChange;  }
        public System.IO.Ports.SerialData EventData { get { return this._eventData; } set { this._eventData = value; } }
        public System.IO.Ports.SerialError EventError { get { return this._eventError; } set { this._eventError = value; } }
        public System.IO.Ports.SerialPinChange EventPinChange { get { return this._eventPinChange; } set { this._eventPinChange = value; } }
    }
	#region Zebra Specific
    /// <summary>Provides data for the Zebra printer information event.</summary>
    public class PrinterInfoArgs:EventArgs {
		private string model="",version="",resolution="",memory="",x="";
        /// <summary>Constructor.</summary>
        public PrinterInfoArgs(string model, string version, string resolution, string memory, string x) { this.model = model; this.version = version; this.resolution = resolution; this.memory = memory; this.x = x; }
        /// <summary>Zebra printer model.</summary>
        public string Model { get { return this.model; } }
        /// <summary>Zebra printer version.</summary>
        public string Version { get { return this.version; } }
        /// <summary>Zebra printer resolution.</summary>
        public string Resolution { get { return this.resolution; } }
        /// <summary>Zebra printer memory.</summary>
        public string Memory { get { return this.memory; } }
        /// <summary>Spare.</summary>
        public string X { get { return this.x; } }
	}
    /// <summary>Provides data for the Zebra printer memory status event.</summary>
    public class PrinterMemoryStatusArgs:EventArgs {
        private string ramTotal="",ramMax="",ramAvailable="";
        /// <summary>Constructor.</summary>
        public PrinterMemoryStatusArgs(string total,string max,string available) { this.ramTotal = total; this.ramMax = max; this.ramAvailable = available; }
        /// <summary>Zebra printer total RAM.</summary>
        public string RAMTotal { get { return this.ramTotal; } }
        /// <summary>Zebra printer maximum RAM.</summary>
        public string RAMMax { get { return this.ramMax; } }
        /// <summary>Zebra printer available RAM.</summary>
        public string RAMAvailable { get { return this.ramAvailable; } }
	}
    /// <summary>Provides data for the printer status event.</summary>
    public class PrinterStatusArgs:EventArgs {
        private string commSettings="",paperOut="",pause="",labelLength="",formatsInReceiveBuffer="";
        private string bufferFull="",diagnosticsMode="",partialFormat="",unused1="",corruptRAM="";
        private string underTemp="",overTemp="",functionSettings="",unused2="",headUp="";
        private string ribbonOut="",thermalTransMode="",printMode="",printWidthMode="",labelWaiting="";
        private string labelsRemainingInBatch="",formatWhilePrinting="",graphicImagesInMemory="",password="",staticRAM="";
        /// <summary>Constructor.</summary>
        public PrinterStatusArgs(string commSettings,string paperOut,string pause,string labelLength,string formatsInReceiveBuffer,string bufferFull,string diagnosticsMode,string partialFormat,string unused1,string corruptRAM,string underTemp,string overTemp,string functionSettings,string unused2,string headUp,string ribbonOut,string thermalTransMode,string printMode,string printWidthMode,string labelWaiting,string labelsRemainingInBatch,string formatWhilePrinting,string graphicImagesInMemory,string password,string staticRAM) {
			this.commSettings = commSettings;
			this.paperOut = paperOut;
			this.pause = pause;
			this.labelLength = labelLength;
			this.formatsInReceiveBuffer = formatsInReceiveBuffer;
			this.bufferFull = bufferFull;
			this.diagnosticsMode = diagnosticsMode;
			this.partialFormat = partialFormat;
			this.unused1 = unused1;
			this.corruptRAM = corruptRAM;
			this.underTemp = underTemp;
			this.overTemp = overTemp;
			this.functionSettings = functionSettings;
			this.unused2 = unused2;
			this.headUp = headUp;
			this.ribbonOut = ribbonOut;
			this.thermalTransMode = thermalTransMode;
			this.printMode = printMode;
			this.printWidthMode = printWidthMode;
			this.labelWaiting = labelWaiting;
			this.labelsRemainingInBatch = labelsRemainingInBatch;
			this.formatWhilePrinting = formatWhilePrinting;
			this.graphicImagesInMemory = graphicImagesInMemory;
			this.password = password;
			this.staticRAM = staticRAM;
		}
        /// <summary>Zebra printer comm settings.</summary>
        public string CommSettings { get { return this.commSettings; } }
        /// <summary>Zebra printer paper out flag.</summary>
        public string PaperOut { get { return this.paperOut; } }
        /// <summary>Zebra printer pause status.</summary>
        public string Pause { get { return this.pause; } }
        /// <summary>Zebra printer label length.</summary>
        public string LabelLength { get { return this.labelLength; } }
        /// <summary>Zebra printer formats in the receive buffer.</summary>
        public string FormatsInReceiveBuffer { get { return this.formatsInReceiveBuffer; } }
        /// <summary>Zebra printer buffer full flag.</summary>
        public string BufferFull { get { return this.bufferFull; } }
        /// <summary>Zebra printer daignostic mode flag.</summary>
        public string DiagnosticsMode { get { return this.diagnosticsMode; } }
        /// <summary>Zebra printer partial format flag.</summary>
        public string PartialFormat { get { return this.partialFormat; } }
        /// <summary>Zebra printer spare.</summary>
        public string Unused1 { get { return this.unused1; } }
        /// <summary>Zebra printer corrupt RAM flag.</summary>
        public string CorruptRAM { get { return this.corruptRAM; } }
        /// <summary>Zebra printer under temperature flag.</summary>
        public string UnderTemp { get { return this.underTemp; } }
        /// <summary>Zebra printer over temperature flag.</summary>
        public string OverTemp { get { return this.overTemp; } }
        /// <summary>Zebra printer function settings.</summary>
        public string FunctionSettings { get { return this.functionSettings; } }
        /// <summary>Zebra printer spare.</summary>
        public string Unused2 { get { return this.unused2; } }
        /// <summary>Zebra printer head up flag.</summary>
        public string HeadUp { get { return this.headUp; } }
        /// <summary>Zebra printer ribbon out flag.</summary>
        public string RibbonOut { get { return this.ribbonOut; } }
        /// <summary>Zebra printer thermal transfer mode status.</summary>
        public string ThermalTransMode { get { return this.thermalTransMode; } }
        /// <summary>Zebra printer print mode.</summary>
        public string PrintMode { get { return this.printMode; } }
        /// <summary>Zebra printer print mode width.</summary>
        public string PrintWidthMode { get { return this.printWidthMode; } }
        /// <summary>Zebra printer label waiting flag.</summary>
        public string LabelWaiting { get { return this.labelWaiting; } }
        /// <summary>Zebra printer count of labels remaining in batch.</summary>
        public string LabelsRemainingInBatch { get { return this.labelsRemainingInBatch; } }
        /// <summary>Zebra printer format while printing flag.</summary>
        public string FormatWhilePrinting { get { return this.formatWhilePrinting; } }
        /// <summary>Zebra printer count of graphics in memory.</summary>
        public string GraphicImagesInMemory { get { return this.graphicImagesInMemory; } }
        /// <summary>Zebra printer password.</summary>
        public string Password { get { return this.password; } }
        /// <summary>Zebra printer static RAM.</summary>
        public string StaticRAM { get { return this.staticRAM; } }
    }
    #endregion
}
