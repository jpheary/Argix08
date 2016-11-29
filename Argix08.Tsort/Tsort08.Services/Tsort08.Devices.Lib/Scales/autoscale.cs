//	File:	autoscale.cs
//	Author:	J. Heary
//	Date:	01/24/05
//	Desc:	Implementation of IScale to read weight from the device attached to the 
//          serial port. Serial port is polled using a background thread. Protocol 
//          is based upon WeighTronix 7800 Series scale.
//   Rev:   
//	---------------------------------------------------------------------------
using System;
using System.Net;
using System.Threading;
using System.IO.Ports;

namespace Tsort.Devices.Scales {
    /// <summary>Implementation of IScale to read weight from the device attached to the serial port.</summary>
	public class AutoScale : IScale {
		//Members
		private decimal mWeight=0;
		private bool mIsStable=true;
		private bool mWeighing=true;
        private int mWaitInterval=500;
		private SerialPort mSerialPort=null;
        //private WeightCompleteHandler mWeightComplete = null;

        /// <summary>Read weight command string.</summary>
        public const string WEIGHT_STRING = "WWWWWWWWWWWWWW\r";
        /// <summary>Reset scale command string.</summary>
        public const string WEIGHT_RESET = "ZZZZZZZZZZZZZZ\r";
        private const int DEFAULT_BAUD = 9600;
        private const int DEFAULT_DATABITS = 7;
        private const Parity DEFAULT_PARITY = Parity.Even;
        private const StopBits DEFAULT_STOPBITS = StopBits.One;
		
        private delegate void WeightCompleteHandler(ScaleEventArgs e);
        /// <summary>Event that returns the current scale weight.</summary>
        public event ScaleEventHandler ScaleWeightReading=null;
		
		//Interface
        /// <summary>Constructor.</summary>
        /// <param name="portName"></param>
        public AutoScale(string portName) : this(portName,DEFAULT_BAUD,DEFAULT_PARITY) { }
        /// <summary>Constructor.</summary>
        /// <param name="portName">Serial port name.</param>
        /// <param name="baudRate">Serial port baud rate.</param>
        /// <param name="parity">Serial port parity.</param>
        public AutoScale(string portName,int baudRate,Parity parity) {
			//Constructor
            //this.mWeightComplete = new WeightCompleteHandler(OnWeightComplete);
            this.mSerialPort = new SerialPort(portName,baudRate,parity,DEFAULT_DATABITS,DEFAULT_STOPBITS);
            
            //this.mSerialPort.PinChanged += new SerialPinChangedEventHandler(OnPinChanged);
            //this.mSerialPort.DataReceived += new SerialDataReceivedEventHandler(OnDataReceived);
            //this.mSerialPort.ErrorReceived += new SerialErrorReceivedEventHandler(OnErrorReceived);
        }
        #region IDevice: DefaultSettings, Settings, On, TurnOn, TurnOff
        /// <summary>Gets a PortSettings structure initialized with the default port settings (if applicable).</summary>
        public PortSettings DefaultSettings {
            get {
                PortSettings settings = new PortSettings();
                settings.PortName = this.mSerialPort.PortName;
                settings.BaudRate = DEFAULT_BAUD;
                settings.Parity = DEFAULT_PARITY;
                return settings;
            }
        }
        /// <summary>Gets or sets the the current port settings (if applicable).</summary>
        public PortSettings Settings {
            get {
                PortSettings settings = new PortSettings();
                settings.PortName = this.mSerialPort.PortName;
                settings.BaudRate = this.mSerialPort.BaudRate;
                settings.Parity = this.mSerialPort.Parity;
                return settings;
            }
            set {
                try {
                    this.mSerialPort.BaudRate = value.BaudRate;
                    this.mSerialPort.Parity = value.Parity;
                    if(value.PortName != this.mSerialPort.PortName) {
                        bool bOpen = this.mSerialPort.IsOpen;
                        if(bOpen) this.mSerialPort.Close();
                        this.mSerialPort.PortName = value.PortName;
                        if(bOpen) this.mSerialPort.Open();
                    }
                }
                catch(System.IO.FileNotFoundException ex) { throw new Exception("COM port " + this.mSerialPort.PortName + " not found.",ex); }
                catch(Exception ex) { throw ex; }
            }
        }
        /// <summary>Gets a value indicating if the scale is on.</summary>
        public bool On { get { return this.mSerialPort.IsOpen; } }
        /// <summary>Turns the scale on.</summary>
        public void TurnOn() {
            //Start continous loop to execute ReadAvailable on the ComPort and trigger Weight event.
            try {
                if(!this.mSerialPort.IsOpen) this.mSerialPort.Open();
                this.mWeighing = true;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error opening serial port.",ex); }
            try {
                //Start a new thread and get data
                Thread t = new Thread(new ThreadStart(readWeight));
                t.IsBackground = true;
                t.Start();
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error starting read thread.",ex); }
        }
        /// <summary>Turns the scale off.</summary>
        public void TurnOff() {
            //Stop sending weight event and close the com port
            if(this.mSerialPort.IsOpen) this.mSerialPort.Close();
            this.mWeighing = false;
        }
        #endregion
        #region IScale: Type, Weight, GetWeight, IsStable, Zero
        /// <summary>Gets the type of scale.</summary>
        public string Type { get { return "Automatic"; } }
        /// <summary>Gets or sets the current weight.</summary>
        public decimal Weight { 
			get { return this.mWeight; }
			set { 
				this.mWeight = value; 
				if(this.ScaleWeightReading != null) this.ScaleWeightReading(this, new ScaleEventArgs(this.mWeight, ScaleError.None));
			} 
		}
        /// <summary>Gets a key-based weight (if applicable), or returns the current weight.</summary>
        public decimal GetWeight(string key) { return this.mWeight; }
        /// <summary>Gets a value indicating if the scale is stable.</summary>
        public bool IsStable { get { return this.mIsStable; } }
        /// <summary>Zeros the scale.</summary>
        public void Zero() { 
			this.mSerialPort.Write(WEIGHT_RESET); 
			if(this.ScaleWeightReading != null) this.ScaleWeightReading(this, new ScaleEventArgs(this.mWeight, ScaleError.None));
        }
        #endregion
        #region SerialPort Events: OnPinChanged(), OnDataReceived(), OnErrorReceived()
        private void OnPinChanged(object sender,SerialPinChangedEventArgs e) {
            //Event handler for serial port RS232 pin change
            throw new NotSupportedException("The method or operation is not implemented.");
        }
        private void OnDataReceived(object sender,SerialDataReceivedEventArgs e) {
            //Event handler for serial port data received
            decimal weight = parseWeight(this.mSerialPort.ReadExisting());
            if(this.ScaleWeightReading != null)
                this.ScaleWeightReading(this,new ScaleEventArgs(weight,ScaleError.None));
        }
        private void OnErrorReceived(object sender,SerialErrorReceivedEventArgs e) {
            //Event handler for serial port error
            this.mSerialPort.DiscardInBuffer();
            this.mSerialPort.DiscardOutBuffer();
        }
		#endregion
        #region Polling: readWeight(), getIncomingText(), parseWeight(), parseUnits(), checkStability()
        private void readWeight() {
            //Polling method running on a background thread
			while(this.mWeighing) {
                try {
					if(this.mSerialPort.IsOpen) {
						getIncomingText();
						this.mSerialPort.Write(WEIGHT_STRING);
						Thread.Sleep(this.mWaitInterval);
					}
                }
                catch(Exception) { }
            }
			if(!this.mWeighing) Thread.Sleep(0);
		}
        private void getIncomingText() {
			//Read incoming port buffer
			string buffer="", units="";
			decimal weight=0;
            try {
                try {
                    buffer = this.mSerialPort.ReadExisting();
#if DEBUG           
                    System.Diagnostics.Debug.WriteLine(buffer); 
#endif
				    weight = parseWeight(buffer);
				    units = parseUnits(buffer);
			    }
			    catch(Exception) { }
    			
			    //Send notification of weight reading if scale value indicated a stable reading
			    bool stable = this.mIsStable ? checkStability(buffer) : true;
			    ScaleError error = stable ? ScaleError.None : ScaleError.ScaleUnstable;
                //this.mWeightComplete.BeginInvoke(new ScaleEventArgs(weight,error),null,null);
                new WeightCompleteHandler(OnWeightComplete).BeginInvoke(new ScaleEventArgs(weight,error),null,null);
            }
            catch(ApplicationException) { }
            catch(Exception) { }
        }
        private void OnWeightComplete(ScaleEventArgs e) {
            //This method is called from the background thread; it is called through a BeginInvoke  
            //call so that it is always marshaled to the thread that owns the dataset
            try {
                this.mWeight = e.Weight;
                if(this.ScaleWeightReading != null) this.ScaleWeightReading(this,new ScaleEventArgs(e.Weight,e.Error));
            }
            catch(Exception) { }
        }
        private decimal parseWeight(string text) {
			string sWeight="?";
			decimal weight=0;
			try {
				if(text.Length > 6) {
					sWeight = text.Substring(2,5);
					weight = Convert.ToDecimal(sWeight);
				}
			}
			catch(Exception) { }
			return weight;
		}
		private string parseUnits(string text) {
			string units="  ";
			try {
				if(text.Length > 8) {
					units = text.Substring(7,2);
				}
			}
			catch(Exception) { }
			return units;
		}
		private bool checkStability(string weight) {
			//Validate stability of incoming sclae weight
			bool stable=false;
			string sStability="?";
			try {
				//Make sure we have at least 12 chars
				if(weight.Length > 11) {
					sStability = weight.Substring(10,2);
					stable = Convert.ToBoolean(Convert.ToInt32(sStability));
				}
			}
			catch(Exception) { }
			return stable;
		}
		#endregion
	}
}
