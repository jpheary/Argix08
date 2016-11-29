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

namespace Tsort.Devices.Printers {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class Zebra110:ILabelPrinter {
        //Members
        private SerialPort mSerialPort=null;
        private DateTime mBorn=DateTime.Now;

        private const int DEFAULT_BAUD = 19200;
        private const int DEFAULT_DATABITS = 7;
        private const Parity DEFAULT_PARITY = Parity.None;
        private const StopBits DEFAULT_STOPBITS = StopBits.One;
        private const Handshake DEFAULT_HANDSHAKE = Handshake.None;
        private const int SLEEP = 500;

        //Interface
        public Zebra110() : this("COM1",DEFAULT_BAUD,DEFAULT_PARITY) { }
        public Zebra110(string portName) : this(portName,DEFAULT_BAUD,DEFAULT_PARITY) { }
        public Zebra110(string portName,int baudRate,Parity parity) {
            //Constructor
            this.mBorn=DateTime.Now;
            this.mSerialPort = new SerialPort(portName,baudRate,parity,DEFAULT_DATABITS,DEFAULT_STOPBITS);
        }
        public DateTime BornOn() { return this.mBorn; }
        #region IScale: GetScaleType(), GetSettings(), IsOn(), TurnOn(), TurnOff(), GetWeight(), Zero()
        public string GetLabelPrinterType() { return "110XiIII"; }
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
            catch(Exception ex) { throw new FaultException<LabelPrinterFault>(new LabelPrinterFault(ex)); }
            return settings;
        }
        public bool IsOn() { return this.mSerialPort.IsOpen; }
        public void TurnOn() {
            //Start continous loop to execute ReadAvailable on the ComPort and trigger Weight event.
            try {
                if(!this.mSerialPort.IsOpen) this.mSerialPort.Open();
            }
            catch(Exception ex) { throw new FaultException<LabelPrinterFault>(new LabelPrinterFault(new ApplicationException("Unexpected error opening serial port.",ex))); }
        }
        public void TurnOff() {
            //Stop sending weight event and close the com port
            try {
                if(this.mSerialPort.IsOpen) this.mSerialPort.Close();
            }
            catch(Exception ex) { throw new FaultException<LabelPrinterFault>(new LabelPrinterFault(ex)); }
        }
        public string Print(string labelFormat) {
            //
            char[] cBuffer = new char[128];
            string sBuffer="";
            int _iTimeout=0,iChars=0;
            try {
                if(this.mSerialPort.IsOpen) {
                    //Echo send request
                    string sData = (labelFormat.Length>128) ? labelFormat.Substring(0,128) + "..." : labelFormat;

                    //Turn-off comm events and reduce read timeout
                    _iTimeout = this.mSerialPort.ReadTimeout;
                    this.mSerialPort.ReadTimeout = 50;

                    //Write the request and capture/echo the response
                    this.mSerialPort.Write(labelFormat);
                    Thread.Sleep(250);
                    while((iChars = this.mSerialPort.Read(cBuffer,0,128)) != 0) {
                        for(int i=0;i<iChars;i++)
                            sBuffer += cBuffer[i];
                    }

                    //Restore settings; check host status if required
                    this.mSerialPort.ReadTimeout = _iTimeout;
                    Thread.Sleep(SLEEP);
                }
            }
            catch(Exception ex) { throw new FaultException<LabelPrinterFault>(new LabelPrinterFault(ex)); }
            return sBuffer;
        }
        #endregion
    }
}
