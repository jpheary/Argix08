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
using System.ServiceModel;

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tsort.Devices.Scales {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class AutoScale:IScale {
        //Members
        private decimal mWeight=0;
        private bool mIsStable=true;
        private bool mWeighing=true;
        private int mWaitInterval=500;
        private SerialPort mSerialPort=null;
        private DateTime mBorn=DateTime.Now;
        private IScaleEvents mCallback=null;

        private const string WEIGHT_STRING = "WWWWWWWWWWWWWW\r";
        private const string WEIGHT_RESET = "ZZZZZZZZZZZZZZ\r";
        private const int DEFAULT_BAUD = 9600;
        private const int DEFAULT_DATABITS = 7;
        private const Parity DEFAULT_PARITY = Parity.Even;
        private const StopBits DEFAULT_STOPBITS = StopBits.One;

        public delegate void WeightCompleteHandler(ScaleEventArgs e);
        public event ScaleEventHandler WeightChanged=null;

        //Interface
        public AutoScale() : this("COM1",DEFAULT_BAUD,DEFAULT_PARITY) { }
        public AutoScale(string portName) : this(portName,DEFAULT_BAUD,DEFAULT_PARITY) { }
        public AutoScale(string portName,int baudRate,Parity parity) {
            //Constructor
            this.mBorn=DateTime.Now;
            this.mSerialPort = new SerialPort(portName,baudRate,parity,DEFAULT_DATABITS,DEFAULT_STOPBITS);
        }
        public DateTime BornOn() { return this.mBorn; }
        #region IScale: GetScaleType(), GetSettings(), IsOn(), TurnOn(), TurnOff(), GetWeight(), Zero()
        public string GetScaleType() { return "Automatic"; }
        public PortSettings GetSettings() {
            PortSettings settings = new PortSettings();
            try {
                settings.PortName = this.mSerialPort.PortName;
                settings.BaudRate = this.mSerialPort.BaudRate;
                settings.DataBits = this.mSerialPort.DataBits;
                settings.Parity = this.mSerialPort.Parity;
                settings.StopBits = this.mSerialPort.StopBits;
                settings.Handshake = this.mSerialPort.Handshake;
            }
            catch(Exception ex) { throw new FaultException<ScaleFault>(new ScaleFault(ex)); }
            return settings;
        }
        public bool IsOn() { return this.mSerialPort.IsOpen; }
        public void TurnOn() {
            //Start continous loop to execute ReadAvailable on the ComPort and trigger Weight event.
            try {
                if(OperationContext.Current != null)
                    this.mCallback = OperationContext.Current.GetCallbackChannel<IScaleEvents>();
                if(!this.mSerialPort.IsOpen) this.mSerialPort.Open();
                this.mWeighing = true;
            }
            catch(Exception ex) { throw new FaultException<ScaleFault>(new ScaleFault(new ApplicationException("Unexpected error opening serial port.",ex))); }
            try {
                //Start a new thread and get data
                Thread t = new Thread(new ThreadStart(readScale));
                t.IsBackground = true;
                t.Start();
            }
            catch(Exception ex) { throw new FaultException<ScaleFault>(new ScaleFault(new ApplicationException("Unexpected error starting read thread.",ex))); }
        }
        public void TurnOff() {
            //Stop sending weight event and close the com port
            try {
                if(this.mSerialPort.IsOpen) this.mSerialPort.Close();
                this.mWeighing = false;
            }
            catch(Exception ex) { throw new FaultException<ScaleFault>(new ScaleFault(ex)); }
        }
        public decimal GetWeight(ref bool isStable) {
            try {
                isStable = this.mIsStable;
            }
            catch(Exception ex) { throw new FaultException<ScaleFault>(new ScaleFault(ex)); }
            return this.mWeight;
        }
        public void Zero() {
            try {
                this.mSerialPort.Write(WEIGHT_RESET);
                if(WeightChanged != null) WeightChanged(this,new ScaleEventArgs(this.mWeight,ScaleError.None));
            }
            catch(Exception ex) { throw new FaultException<ScaleFault>(new ScaleFault(ex)); }
        }
        #endregion
        #region Polling: readScale(), getResponse()
        private void readScale() {
            //Polling method running on a background thread
            while(this.mWeighing) {
                try {
                    if(this.mSerialPort.IsOpen) {
                        //Check response from last request
                        getResponse();

                        //Request weight reading from scale
                        this.mSerialPort.Write(WEIGHT_STRING);
                        Thread.Sleep(this.mWaitInterval);
                    }
                }
                catch(Exception) { }
            }
            if(!this.mWeighing) Thread.Sleep(0);
        }
        private void getResponse() {
            //Read incoming port buffer
            string buffer="",units="";
            decimal weight=0;
            bool stable=false;
            try {
                try {
                    buffer = this.mSerialPort.ReadExisting();
                    if(buffer.Length > 6)
                        weight = Convert.ToDecimal(buffer.Substring(2,5));
                    if(buffer.Length > 8)
                        units = buffer.Substring(7,2);
                    if(buffer.Length > 11)
                        stable = Convert.ToBoolean(Convert.ToInt32(buffer.Substring(10,2)));
                }
                catch { }

                //Send notification of weight reading if scale value indicated a stable reading
                ScaleError error = stable ? ScaleError.None : ScaleError.ScaleUnstable;
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
                this.mIsStable = (e.Error == ScaleError.None);
                if(WeightChanged != null) WeightChanged(this,e);
                if(this.mCallback != null) this.mCallback.WeightReading(e.Weight,e.Error);
            }
            catch(Exception ex) { System.Diagnostics.Debugger.Log(0,"",ex.ToString()); }
        }
        #endregion
    }
}
