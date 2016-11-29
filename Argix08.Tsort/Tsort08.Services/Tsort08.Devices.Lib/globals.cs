//	File:	globals.cs
//	Author:	J. Heary
//	Date:	03/22/05
//	Desc:	Device interfaces, enumerators, event delegates, eventargs, exceptions, etc.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Runtime.Serialization;
using System.IO.Ports;

namespace Tsort.Devices {
	//Interfaces
    /// <summary>Specifies the base interface for printer and scale devices.</summary>
    public interface IDevice {
        /// <summary>Gets a PortSettings structure initialized with the default port settings (if applicable).</summary>
        PortSettings DefaultSettings { get; }
        /// <summary>Gets or sets the the current port settings (if applicable).</summary>
        PortSettings Settings { get; set; }
        /// <summary>Gets a value indicating if the device is on.</summary>
        bool On { get; }
        /// <summary>Turns the device on.</summary>
        void TurnOn();
        /// <summary>Turns the device off.</summary>
        void TurnOff();
	}
	
	//UDTs
    /// <summary>Specifies information about how a serial port is configured, including port name, baud rate, data bits, parity, stop bits, and handshake.</summary>
    public struct PortSettings {
		//Members
        /// <summary>Default baud rate value.</summary>
        public const int DEFAULT_BAUD = 19200;
        /// <summary>Default data bits value.</summary>
        public const int DEFAULT_DATABITS = 7;
        /// <summary>Default parity value.</summary>
        public const Parity DEFAULT_PARITY = Parity.None;
        /// <summary>Default stop bits value.</summary>
        public const StopBits DEFAULT_STOPBITS = StopBits.One;
        /// <summary>Default handshake value.</summary>
        public const Handshake DEFAULT_HANDSHAKE = Handshake.None;
        
        /// <summary>Initializes a new instance of Tsort.Devices.PortSettings.</summary>
        /// <param name="portName">The initial port name.</param>
        /// <param name="baudRate">The initial baud rate.</param>
        /// <param name="dataBits">The initial data bits.</param>
        /// <param name="parity">The initial parity.</param>
        /// <param name="stopBits">The initial stop bits.</param>
        /// <param name="handshake">The initial handshake.</param>
        public PortSettings(string portName,int baudRate,int dataBits,Parity parity,StopBits stopBits,Handshake handshake) { PortName = portName; BaudRate=baudRate; DataBits=dataBits; Parity=parity; StopBits=stopBits; Handshake=handshake; }
        /// <summary>Serial port name.</summary>
        public string PortName;
        /// <summary>Serial port baud rate.</summary>
        public int BaudRate;
        /// <summary>Serial port handshake.</summary>
        public Handshake Handshake;
        /// <summary>Serial port data bits.</summary>
        public int DataBits;
        /// <summary>Serial port stop bits.</summary>
        public StopBits StopBits;
        /// <summary>Serial port parity.</summary>
        public Parity Parity;
	}
}
