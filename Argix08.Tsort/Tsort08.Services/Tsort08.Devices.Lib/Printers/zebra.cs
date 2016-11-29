//	File:	zebra.cs
//	Author:	J. Heary
//	Date:	03/12/04
//	Desc:	.
//	Rev:	04/27/04 (jph)- modified to use mSatinCable flag.
//			09/22/04 (jph)- revised for change from Zebra_ enume to RS232_ enums.
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace Tsort.Devices.Printers {	
	//
	public class Zebra: SerialPort, ILabelPrinter {
		//Members
        protected bool mReprintAfterError = true;
		protected bool mUseZebraNetAlert=false;
		protected bool mSatinCable=false;

        private const int DEFAULT_BAUD = 19200;
        private const int DEFAULT_DATABITS = 7;
        private const Parity DEFAULT_PARITY = Parity.None;
        private const StopBits DEFAULT_STOPBITS = StopBits.One;
        private const Handshake DEFAULT_HANDSHAKE = Handshake.None;
        private const int SLEEP = 500;
		
		//Host ID/Mem/Status members
		protected string mModel="", mVersion="", mResolution="", mMemory="", mX="";
		protected string mRAMTotal="", mRAMMax="", mRAMAvailable="";
		protected string mCommSettings="", mPaperOut="", mPause="", mLabelLength="";
		protected string mFormatsInReceiveBuffer="", mBufferFull="", mDiagnosticsMode="", mPartialFormat="", mUnused1="";
		protected string mCorruptRAM="", mUnderTemp="", mOverTemp="", mFunctionSettings="", mUnused2="";
		protected string mHeadUp="", mRibbonOut="", mThermalTransMode="", mPrintMode="", mPrintWidthMode="";
		protected string mLabelWaiting="", mLabelsRemainingInBatch="", mFormatWhilePrinting="", mGraphicImagesInMemory="";
		protected string mPassword="", mStaticRAM="";
		private string mMediaType="", mSensorProfile="", mPrintMode_="";

        /// <summary>Event notification for printer turned on.</summary>
        public event EventHandler PrinterTurnedOn = null;
        /// <summary>Event notification for printer turned off.</summary>
        public event EventHandler PrinterTurnedOff = null;
        /// <summary>Event notification for printer settings changed.</summary>
        public event EventHandler PrinterSettingsChanged = null;
        /// <summary>Event notification for printer pin and error events.</summary>
        public event PrinterEventHandler PrinterEventChanged = null;

        /// <summary></summary>
        public event PrinterDataHandler ReceivedDataEvent = null;
        /// <summary></summary>
        public event PrinterDataHandler SentDataEvent = null;
        /// <summary></summary>
        public event PrinterInfoHandler HostInfoChanged = null;
        /// <summary></summary>
        public event PrinterMemoryStatusHandler HostMemoryStatusChanged = null;
        /// <summary></summary>
        public event PrinterStatusHandler HostStatusChanged = null;
		
		//Interface
		/// <summary>Constructor.</summary>
		/// <param name="portName"></param>
        public Zebra(string portName) : this(portName, DEFAULT_BAUD, DEFAULT_DATABITS, DEFAULT_PARITY, DEFAULT_STOPBITS) { }
        /// <summary>Constructor.</summary>
        /// <param name="portName"></param>
		/// <param name="baudRate"></param>
		/// <param name="dataBits"></param>
		/// <param name="parity"></param>
		/// <param name="stopBits"></param>
        public Zebra(string portName, int baudRate, int dataBits, Parity parity, StopBits stopBits) : base(portName, baudRate, parity, dataBits, stopBits) {
			//Constructor to create a new printer object that opens the port and reads printer status
			try {				
				//Ref serial port and port events of interest
                base.DataReceived += new SerialDataReceivedEventHandler(OnDataReceived);
                base.PinChanged += new SerialPinChangedEventHandler(OnPinChanged);
                base.ErrorReceived += new SerialErrorReceivedEventHandler(OnErrorReceived);
				
				//Determine cable limitations
				if(base.IsOpen)
					this.mSatinCable = (!this.DsrHolding && !this.CDHolding && !this.CtsHolding);
			} 
			catch(Exception ex) { throw new ApplicationException("Could not create new Zebra printer.", ex); }
		}
        /// <summary>Destructor.</summary>
        ~Zebra() { try { base.Close(); } catch (Exception) { } }		
        #region SerialPort: 
        public bool CDHolding { get { return base.CDHolding; } }
        public bool CtsHolding { get { return base.CtsHolding; } }
        public bool DsrHolding { get { return base.DsrHolding; } }
        public bool DtrEnable { get { return base.DtrEnable; } set { base.DtrEnable = value; } }
        public bool RtsEnable { get { return base.RtsEnable; } set { base.RtsEnable = value; } }
        #endregion
		#region Members: [...]
		public bool ReprintAfterError {
			get { return this.mReprintAfterError; }
			set {
				//Execute reprint after error command on the printer
				string a="";
				switch(value) {
					case true:	a = "Y"; break;
					case false:	a = "N"; break;
				}
				//this.WriteCommand("^JZ" + a, false);
				this.WriteCommand("^XA^JZ" + a + "^XZ^XA^JUS^XZ");
				this.mReprintAfterError = value;
			}
		}
		public bool UseZebraNetAlert {
			get { return this.mUseZebraNetAlert; }
			set {
				//Execute set ZebarNet ALert command on the printer
				string a="R";		//Condition type: all errors(R)
				string b="A";		//Route alert: serial port(A)
				string c="Y";		//Condition set: enable(Y/N)
				string d="Y";		//Condition clear: enable(Y/N)
				switch(value) {
					case true:	c = d = "Y"; break;
					case false:	c = d = "N"; break;
				}
				this.WriteCommand("^XA^SX" + a + "," + b + "," + c + "," + d + "^JUS^XZ");
				this.mUseZebraNetAlert = value;
			}
		}
		public string Model { get { return this.mModel; } }
		public string Version { get { return this.mVersion; } }
		public string Resolution { get { return this.mResolution; } }
		public string Memory { get { return this.mMemory; } }
		public string X { get { return this.mX; } }
		
		public string RAMTotal { get { return this.mRAMTotal; } }
		public string RAMMax { get { return this.mRAMMax; } }
		public string RAMAvailable { get { return this.mRAMAvailable; } }

		public string CommSettings { get { return this.mCommSettings; } }
		public string PaperOut { get { return this.mPaperOut; } }
		public string Pause { get { return this.mPause; } }
		public string LabelLength { get { return this.mLabelLength; } }
		public string FormatsInReceiveBuffer { get { return this.mFormatsInReceiveBuffer; } }
		public string BufferFull { get { return this.mBufferFull; } }
		public string DiagnosticsMode { get { return this.mDiagnosticsMode; } }
		public string PartialFormat { get { return this.mPartialFormat; } }
		public string Unused1 { get { return this.mUnused1; } }
		public string CorruptRAM { get { return this.mCorruptRAM; } }
		public string UnderTemp { get { return this.mUnderTemp; } }
		public string OverTemp { get { return this.mOverTemp; } }
		public string FunctionSettings { get { return this.mFunctionSettings; } }
		public string Unused2 { get { return this.mUnused2; } }
		public string HeadUp { get { return this.mHeadUp; } }
		public string RibbonOut { get { return this.mRibbonOut; } }
		public string ThermalTransMode { get { return this.mThermalTransMode; } }
		public string PrintMode { get { return this.mPrintMode; } }
		public string PrintWidthMode { get { return this.mPrintWidthMode; } }
		public string LabelWaiting { get { return this.mLabelWaiting; } }
		public string LabelsRemainingInBatch { get { return this.mLabelsRemainingInBatch; } }
		public string FormatWhilePrinting { get { return this.mFormatWhilePrinting; } }
		public string GraphicImagesInMemory { get { return this.mGraphicImagesInMemory; } }
		public string Password { get { return this.mPassword; } }
		public string StaticRAM { get { return this.mStaticRAM; } }
		public string MediaType { get { return this.mMediaType; } }
		public string SensorProfile { get { return this.mSensorProfile; } }
		public string PrintMode_ { get { return this.mPrintMode_; } }
		#endregion
		#region Control Commands: GetHostInfo(), GetHostIdentification(), GetHostMemoryStatus(), GetHostStatus()
		public void GetHostInfo() {
			//Execute host identification command on the printer
			string sBuf1="", sBuf2="", sBuf3="";
			try {
				sBuf1 = parseHostIdentification(this.WriteCommand("~HI"));
				sBuf2 = parseHostMemoryStatus(this.WriteCommand("~HM"));
				sBuf3 = parseHostStatus(this.WriteCommand("~HS"));
				//AppTrace.Instance().TheTrace.WriteLine(new TraceMessage(sBuf1 + " : " + sBuf2+ " : " + sBuf3, "Tsort.Printers", LogLevel.Information, "Zebra::GetHostInfo()", "Zebra Status", "Printer Information"));
			}
			catch(Exception) { }
		}
		public void GetHostIdentification() {
			//Execute host identification command on the printer
			string sBuffer="";
			try {
				sBuffer = this.WriteCommand("~HI");
				parseHostIdentification(sBuffer);
			}
			catch(Exception) { }
		}
		private string parseHostIdentification(string sBuffer) {
			//Parse host identification	
			if(sBuffer.Length > 0) {
				int iPos_stx = sBuffer.IndexOf((char)0x02);
				int iPos_etx = sBuffer.IndexOf((char)0x03);
				sBuffer = sBuffer.Substring(iPos_stx+1, iPos_etx-iPos_stx-1);
				string[] sTokens = sBuffer.Split((char)',');
				this.mModel = (sTokens.Length>0) ? sTokens[0].Trim() : "";
				this.mVersion = (sTokens.Length>1) ? sTokens[1].Trim() : "";
				this.mResolution = (sTokens.Length>2) ? sTokens[2] : "";
				this.mMemory = (sTokens.Length>3) ? sTokens[3] : "";
				this.mX = (sTokens.Length>4) ? sTokens[4] : "";
				if(HostInfoChanged!=null)
					HostInfoChanged(this, new PrinterInfoArgs(this.mModel, this.mVersion, this.mResolution, this.mMemory, this.mX));
			}
			return sBuffer;
		}
		public void GetHostMemoryStatus() {
			//Execute host memory status command on the printer
			string sBuffer="";
			try {
				sBuffer = this.WriteCommand("~HM");
				parseHostMemoryStatus(sBuffer);
			}
			catch(Exception) { }
		}
		private string parseHostMemoryStatus(string sBuffer) {
			//Parse host memory status
			if(sBuffer!="") {
				int iPos_stx = sBuffer.IndexOf((char)0x02);
				int iPos_etx = sBuffer.IndexOf((char)0x03);
				sBuffer = sBuffer.Substring(iPos_stx+1, iPos_etx-iPos_stx-1);
				string[] sTokens = sBuffer.Split((char)',');
				this.mRAMTotal = (sTokens.Length>0) ? sTokens[0].Trim() : "";
				this.mRAMMax = (sTokens.Length>1) ? sTokens[1].Trim() : "";
				this.mRAMAvailable = (sTokens.Length>2) ? sTokens[2] : "";
				if(HostMemoryStatusChanged!=null)
					HostMemoryStatusChanged(this, new PrinterMemoryStatusArgs(this.mRAMTotal, this.mRAMMax, this.mRAMAvailable));
			}
			return sBuffer;
		}
		public void GetHostStatus() {
			//Execute host status return command on the printer
			string sBuffer="";
			try {
				sBuffer = this.WriteCommand("~HS");
				parseHostStatus(sBuffer);
			}
			catch(Exception) { }
		}
		private string parseHostStatus(string sBuffer) {
			//Parse host identification
			string s1="", s2="", s3="";
			bool bStatusChanged=false;
			if(sBuffer!="") {
				int iPos_stx, iPos_etx;
				string[] sTokens = sBuffer.Split('\n');
				s1 = (sTokens.Length>0) ? sTokens[0] : "";
				if(s1!="") {
					iPos_stx = s1.IndexOf((char)0x02);
					iPos_etx = s1.IndexOf((char)0x03);
					s1 = s1.Substring(iPos_stx+1, iPos_etx-iPos_stx-1);
					string[] s1s = s1.Split((char)',');
					this.mCommSettings = (s1s.Length>0) ? s1s[0].Trim() : "";
					updateCOMSettings();
					this.mPaperOut = (s1s.Length>1) ? s1s[1].Trim() : "";
					this.mPause = (s1s.Length>2) ? s1s[2].Trim() : "";
					this.mLabelLength = (s1s.Length>3) ? s1s[3].Trim() : "";
					this.mFormatsInReceiveBuffer = (s1s.Length>4) ? s1s[4].Trim() : "";
					this.mBufferFull = (s1s.Length>5) ? s1s[5].Trim() : "";
					this.mDiagnosticsMode = (s1s.Length>6) ? s1s[6].Trim() : "";
					this.mPartialFormat = (s1s.Length>7) ? s1s[7].Trim() : "";
					this.mUnused1 = (s1s.Length>8) ? s1s[8].Trim() : "";
					this.mCorruptRAM = (s1s.Length>9) ? s1s[9].Trim() : "";
					this.mUnderTemp = (s1s.Length>10) ? s1s[10].Trim() : "";
					this.mOverTemp = (s1s.Length>11) ? s1s[11].Trim() : "";
				}
				bStatusChanged = (this.mPaperOut=="1" || this.mPause=="1" || this.mBufferFull=="1" || this.mCorruptRAM=="1" || this.mUnderTemp=="1" || this.mOverTemp=="1");
				s2 = (sTokens.Length>1) ? sTokens[1] : "";
				if(s2!="") {
					iPos_stx = s2.IndexOf((char)0x02);
					iPos_etx = s2.IndexOf((char)0x03);
					s2 = s2.Substring(iPos_stx+1, iPos_etx-iPos_stx-1);
					string[] s2s = s2.Split((char)',');
					this.mFunctionSettings = (s2s.Length>0) ? s2s[0].Trim() : "";
					updateFunctionSettings();
					this.mUnused2 = (s2s.Length>1) ? s2s[1].Trim() : "";
					this.mHeadUp = (s2s.Length>2) ? s2s[2].Trim() : "";
					this.mRibbonOut = (s2s.Length>3) ? s2s[3].Trim() : "";
					this.mThermalTransMode = (s2s.Length>4) ? s2s[4].Trim() : "";
					this.mPrintMode = (s2s.Length>5) ? s2s[5].Trim() : "";
					this.mPrintWidthMode = (s2s.Length>6) ? s2s[6].Trim() : "";
					this.mLabelWaiting = (s2s.Length>7) ? s2s[7].Trim() : "";
					this.mLabelsRemainingInBatch = (s2s.Length>8) ? s2s[8].Trim() : "";
					this.mFormatWhilePrinting = (s2s.Length>9) ? s2s[9].Trim() : "";
					this.mGraphicImagesInMemory = (s2s.Length>10) ? s2s[10].Trim() : "";
				}
				bStatusChanged = (this.mHeadUp=="1" || this.mRibbonOut=="1");	// || this.mLabelWaiting=="1");
				s3 = (sTokens.Length>2) ? sTokens[2] : "";
				if(s3!="") {
					iPos_stx = s3.IndexOf((char)0x02);
					iPos_etx = s3.IndexOf((char)0x03);
					s3 = s3.Substring(iPos_stx+1, iPos_etx-iPos_stx-1);
					string[] s3s = s3.Split((char)',');
					this.mPassword = (s3s.Length>0) ? s3s[0].Trim() : "";
					this.mStaticRAM = (s3s.Length>1) ? s3s[1].Trim() : "";
				}
				if(HostStatusChanged!=null)
					HostStatusChanged(this, new PrinterStatusArgs(this.mCommSettings, this.mPaperOut, this.mPause, this.mLabelLength, this.mFormatsInReceiveBuffer, this.mBufferFull, this.mDiagnosticsMode, this.mPartialFormat, this.mUnused1, this.mCorruptRAM, this.mUnderTemp, this.mOverTemp, 
						this.mFunctionSettings, this.mUnused2, this.mHeadUp, this.mRibbonOut, this.mThermalTransMode, this.mPrintMode, this.mPrintWidthMode, this.mLabelWaiting, this.mLabelsRemainingInBatch, this.mFormatWhilePrinting, this.mGraphicImagesInMemory, 
						this.mPassword, this.mStaticRAM));
			}
			return s1 + "; " + s2 + "; " + s3;
		}
		#endregion
		#region Other Commands: 
		public void CancelAll() {
			//Execute cancel all command on the printer
			this.WriteCommand("~JA");
		}
		public void CancelCurrentPartiallyInputFormat() {
			//Execute cancel current partially input format command on the printer
			this.WriteCommand("~JX");
		}
		public void PauseAndCancelFormat() {
			//Execute pause and cancel format command on the printer
			this.WriteCommand("~JP");
		}
		public void PowerOnReset() {
			//Execute power on reset command on the printer
			this.WriteCommand("~JR");
		}
		public void SetCommunications(int baud, int word, int parity, int stop, int handshake, int protocol) {
			//Execute set communications command on the printer
			this.WriteCommand("^SC" + baud.ToString() + "," + word.ToString() + "," + parity.ToString() + "," + stop.ToString() + "," + handshake.ToString() + "," + protocol.ToString());
		}
		public void SetPrintheadResistance(int resisitance) {
			//Execute set printheadd resistance command on the printer
			if(resisitance<488 || resisitance>1175) 
				throw new Exception("Invalid printhead resisitance value (valid values: 488-1175).");
			string r = resisitance.ToString();
			this.WriteCommand("^SR" + r);
		}
		#endregion
		//Event forwards for sub-classes
		protected void OnReceivedData(PrinterDataArgs e) { if(ReceivedDataEvent != null) ReceivedDataEvent(this, e); }
		protected void OnSentData(PrinterDataArgs e) { if(SentDataEvent != null) SentDataEvent(this, e); }
        /// <summary></summary>
        /// <param name="sCmd"></param>
		/// <returns></returns>
		public virtual string WriteCommand(string sCmd) {
			//Write the request and return any response
			char[] cBuffer = new char[128];
			string sBuffer="";
			int _iTimeout=0, iChars=0;
			
			try {
				//Echo send request
				string sData = (sCmd.Length>128) ? sCmd.Substring(0, 128) + "..." : sCmd;
				if(SentDataEvent!=null) 
					SentDataEvent(this, new PrinterDataArgs("Zebra: WRITE\t" + sData));
				
				//Turn-off comm events and reduce read timeout
				_iTimeout = base.ReadTimeout;
				base.ReadTimeout = 50;

				//Write the request and capture/echo the response
				base.Write(sCmd);
				Thread.Sleep(250);
				while((iChars = base.Read(cBuffer, 0, 128)) != 0) {
					for(int i=0; i<iChars; i++)
						sBuffer += cBuffer[i];
				}
				if(ReceivedDataEvent!=null) ReceivedDataEvent(this, new PrinterDataArgs("Zebra: READW\t" + sBuffer));
				
				//Restore settings; check host status if required
				base.ReadTimeout = _iTimeout;
				Thread.Sleep(SLEEP);
				GetHostStatus();
			} 
			catch(Exception ex) { throw ex; }
			return sBuffer;
		}

        #region IDevice: DefaultSettings, Settings, On, TurnOn(), TurnOff()
        /// <summary>Returns a PortSettings structure with the default COM port settings.</summary>
        public PortSettings DefaultSettings { 
			get { 
				PortSettings settings = new PortSettings();
				settings.PortName = base.PortName;
				settings.BaudRate = DEFAULT_BAUD;
				settings.Parity = DEFAULT_PARITY;
				settings.DataBits = DEFAULT_DATABITS;
				settings.StopBits = DEFAULT_STOPBITS;
				settings.Handshake = DEFAULT_HANDSHAKE;
				return settings;
			} 
		}
        /// <summary>Gets or sets the the current port settings (if applicable).</summary>
        public PortSettings Settings {
			get { 
				PortSettings settings = new PortSettings();
				settings.PortName = base.PortName;
				settings.BaudRate = base.BaudRate;
				settings.Parity = base.Parity;
				settings.DataBits = base.DataBits;
				settings.StopBits = base.StopBits;
				settings.Handshake = base.Handshake;
				return settings;
			} 
			set {
                try {
                    base.BaudRate = value.BaudRate;
                    base.Parity = value.Parity;
                    base.DataBits = value.DataBits;
                    base.StopBits = value.StopBits;
                    base.Handshake = value.Handshake;
                    if(value.PortName != base.PortName) {
                        bool bOpen = base.IsOpen;
                        if(bOpen) TurnOff();
                        base.PortName = value.PortName;
                        if(bOpen) TurnOn();
                    }
                }
                catch(System.IO.FileNotFoundException ex) { throw new Exception("COM port " + base.PortName + " not found.",ex); }
                catch(Exception ex) { throw ex; }
                finally { if(this.PrinterSettingsChanged != null) this.PrinterSettingsChanged(this,new EventArgs()); }
			} 
		}
        /// <summary>Gets a value indicating if the printer is on.</summary>
        public bool On { get { return (base.IsOpen && (base.IsOpen ? base.DsrHolding : false) && (base.IsOpen ? base.CDHolding : false)); } }
        /// <summary>Turns the printer on.</summary>
        public void TurnOn() { 
			if(!base.IsOpen) { 
				base.Open();
				if(this.PrinterTurnedOn != null) this.PrinterTurnedOn(this, new EventArgs());
			}
		}
        /// <summary>Turns the printer off.</summary>
        public void TurnOff() { 
			if(base.IsOpen) { 
				base.Close();
				if(this.PrinterTurnedOff != null) this.PrinterTurnedOff(this, new EventArgs());
			}
		}
        #endregion
        #region ILabelPrinter: Type, Print
        /// <summary>Gets the type of printer.</summary>
        public string Type { get { return "Zebra"; } }
        /// <summary>Print the specified label format using SerialPort.Write(string).</summary>
        /// <param name="labelFormat">A string containing label zpl.</param>
        public void Print(string labelFormat) {
			//Zebra.PrintLabel implementation
			if(base.IsOpen) WriteCommand(labelFormat);
		}
		#endregion
        #region Serial Port events: OnDataReceived(), OnPinChanged(), OnErrorReceived()
        private void OnDataReceived(object sender,SerialDataReceivedEventArgs e) {
            //Event handler for arrival of a character in the serial port buffer
            string buffer = "";
            try {
                for(; ; ) {
                    buffer = base.ReadExisting();
                    if(base.BytesToRead == 0) {
                        if(buffer.Length > 0) {
                            if(ReceivedDataEvent != null) ReceivedDataEvent(this,new PrinterDataArgs("Zebra: READ\t" + buffer + "\n"));
                        }
                        break;
                    }
                }
            }
            catch(Exception) { }
        }
        private void OnPinChanged(object sender,SerialPinChangedEventArgs e) {
            //Event handler for a change in one of the serial port pin values
            if((e.EventType & SerialPinChange.DsrChanged) > 0) {
                if(this.PrinterEventChanged != null) this.PrinterEventChanged(this,new PrinterEventArgs(e.EventType));
            }
            if((e.EventType & SerialPinChange.CDChanged) > 0) {
                if(this.PrinterEventChanged != null) this.PrinterEventChanged(this,new PrinterEventArgs(e.EventType));
            }
            if((e.EventType & SerialPinChange.CtsChanged) > 0) {
                if(this.PrinterEventChanged != null) this.PrinterEventChanged(this,new PrinterEventArgs(e.EventType));
            }
        }
        private void OnErrorReceived(object sender,SerialErrorReceivedEventArgs e) {
            //Event handler for a serial port error
            switch(e.EventType) {
                case SerialError.Frame:
                    if(this.PrinterEventChanged != null) this.PrinterEventChanged(this,new PrinterEventArgs(e.EventType));
                    break;
                case SerialError.Overrun:
                    if(this.PrinterEventChanged != null) this.PrinterEventChanged(this,new PrinterEventArgs(e.EventType));
                    break;
                case SerialError.RXOver:
                    base.DiscardInBuffer();
                    if(this.PrinterEventChanged != null) this.PrinterEventChanged(this,new PrinterEventArgs(e.EventType));
                    break;
                case SerialError.RXParity:
                    base.DiscardInBuffer();
                    if(this.PrinterEventChanged != null) this.PrinterEventChanged(this,new PrinterEventArgs(e.EventType));
                    break;
                case SerialError.TXFull:
                    base.DiscardOutBuffer();
                    if(this.PrinterEventChanged != null) this.PrinterEventChanged(this,new PrinterEventArgs(e.EventType));
                    break;
            }
        }
        #endregion
        #region Local Services: updateCOMSettings(), updateFunctionSettings()
        private void updateCOMSettings() {
			//Update COM settings
			int iCOM=0;
			string a0="", a1="", a2="", a3="", a4="", a5="", a6="", a7="", a8="0";
			try {
				iCOM = Convert.ToInt32(this.mCommSettings);
				if(iCOM >= 256) {
					iCOM -= 256;
					a8 = "1";
				}
				a0 = ((iCOM & 0x01) == 0x01) ? "1" : "0";
				a1 = ((iCOM & 0x02) == 0x02) ? "1" : "0";
				a2 = ((iCOM & 0x04) == 0x04) ? "1" : "0";
				a3 = ((iCOM & 0x08) == 0x08) ? "1" : "0";
				a4 = ((iCOM & 0x10) == 0x10) ? "1" : "0";
				a5 = ((iCOM & 0x20) == 0x20) ? "1" : "0";
				a6 = ((iCOM & 0x40) == 0x40) ? "1" : "0";
				a7 = ((iCOM & 0x80) == 0x80) ? "1" : "0";
				switch(a8 + a2 + a1 + a0) {
					case "0000": base.BaudRate = 110; break;
					case "0001": base.BaudRate = 300; break;
					case "0010": base.BaudRate = 600; break;
					case "0011": base.BaudRate = 1200; break;
					case "0100": base.BaudRate = 2400; break;
					case "0101": base.BaudRate = 4800; break;
					case "0110": base.BaudRate = 9600; break;
					case "0111": base.BaudRate = 19200; break;
					case "1000": base.BaudRate = 28800; break;
					case "1001": base.BaudRate = 38400; break;
					case "1010": base.BaudRate = 57600; break;
					case "1011": base.BaudRate = 14400; break;
				}
				base.DataBits = (a3 == "0") ? 7 : 8;
				base.StopBits = (a4 == "0") ? StopBits.Two : StopBits.One;
				//this. = (a5 == "0") ? "Disable" : "Enable";
				base.Parity = (a6 == "0") ? Parity.Odd : Parity.Even;
				base.Handshake = (a7 == "0") ? Handshake.XOnXOff : Handshake.RequestToSend;
			}
			catch(Exception) { }
		}
		private void updateFunctionSettings() {
			//Update function settings
			int iFunc=0;
			string m0="", m1="", m2="", m3="", m4="", m5="", m6="", m7="";
			try {
				iFunc = Convert.ToInt32(this.mFunctionSettings);
				m0 = ((iFunc & 0x01) == 0x01) ? "1" : "0";
				m1 = ((iFunc & 0x02) == 0x02) ? "1" : "0";
				m2 = ((iFunc & 0x04) == 0x04) ? "1" : "0";
				m3 = ((iFunc & 0x08) == 0x08) ? "1" : "0";
				m4 = ((iFunc & 0x10) == 0x10) ? "1" : "0";
				m5 = ((iFunc & 0x20) == 0x20) ? "1" : "0";
				m6 = ((iFunc & 0x40) == 0x40) ? "1" : "0";
				m7 = ((iFunc & 0x80) == 0x80) ? "1" : "0";
				this.mMediaType = (m7 == "0") ? "Die Cut" : "Continuous";
				this.mSensorProfile = (m6 == "0") ? "Off" : "";
				//this. = (m5 == "0") ? "Off" : "On";
				this.mPrintMode_ = (m0 == "0") ? "Direct Thermal" : "Thermal Transfer";
			}
			catch(Exception) { }
		}
        #endregion
    }
}