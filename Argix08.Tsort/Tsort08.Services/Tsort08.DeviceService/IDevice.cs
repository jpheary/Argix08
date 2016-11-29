using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.IO.Ports;

namespace Tsort.Devices {
    //    
    [DataContract]
    public struct PortSettings {
        //Members
        public const int DEFAULT_BAUD = 19200;
        public const int DEFAULT_DATABITS = 7;
        public const Parity DEFAULT_PARITY = Parity.None;
        public const StopBits DEFAULT_STOPBITS = StopBits.One;
        public const Handshake DEFAULT_HANDSHAKE = Handshake.None;

        //Interface
        public PortSettings(string portName,int baudRate,int dataBits,Parity parity,StopBits stopBits,Handshake handshake) { PortName = portName; BaudRate=baudRate; DataBits=dataBits; Parity=parity; StopBits=stopBits; Handshake=handshake; }
        [DataMember] public string PortName;
        [DataMember] public int BaudRate;
        [DataMember] public Handshake Handshake;
        [DataMember] public int DataBits;
        [DataMember] public StopBits StopBits;
        [DataMember] public Parity Parity;
    }
}
