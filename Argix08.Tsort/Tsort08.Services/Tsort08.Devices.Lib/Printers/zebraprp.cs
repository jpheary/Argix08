//	File:	zebraprp.cs
//	Author:	J. Heary
//	Date:	03/12/04
//	Desc:	.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Reflection;
using System.Diagnostics;
using System.Data;
using System.Threading;
using System.Text;
using System.IO.Ports;

namespace Tsort.Devices.Printers {	
	/// <summary></summary>
	public class ZebraPRP: Zebra {
		//Members
		private bool mEDPInitialized=false;
		private ushort mCRC=0;
		private byte mTransID=(byte)'0';
		#region CRC Table
		private ushort[] crc_table = {
		/*   0 -- */  0x0000, 0x1021, 0x2042, 0x3063, 0x4084, 0x50A5, 0x60C6, 0x70E7,
		/*   8 -- */  0x8108, 0x9129, 0xA14A, 0xB16B, 0xC18C, 0xD1AD, 0xE1CE, 0xF1EF,
		/*  16 -- */  0x1231, 0x0210, 0x3273, 0x2252, 0x52B5, 0x4294, 0x72F7, 0x62D6,
		/*  24 -- */  0x9339, 0x8318, 0xB37B, 0xA35A, 0xD3BD, 0xC39C, 0xF3FF, 0xE3DE,
		/*  32 -- */  0x2462, 0x3443, 0x0420, 0x1401, 0x64E6, 0x74C7, 0x44A4, 0x5485,
		/*  40 -- */  0xA56A, 0xB54B, 0x8528, 0x9509, 0xE5EE, 0xF5CF, 0xC5AC, 0xD58D,
		/*  48 -- */  0x3653, 0x2672, 0x1611, 0x0630, 0x76D7, 0x66F6, 0x5695, 0x46B4,
		/*  56 -- */  0xB75B, 0xA77A, 0x9719, 0x8738, 0xF7DF, 0xE7FE, 0xD79D, 0xC7BC,
		/*  64 -- */  0x48C4, 0x58E5, 0x6886, 0x78A7, 0x0840, 0x1861, 0x2802, 0x3823,
		/*  72 -- */  0xC9CC, 0xD9ED, 0xE98E, 0xF9AF, 0x8948, 0x9969, 0xA90A, 0xB92B,
		/*  80 -- */  0x5AF5, 0x4AD4, 0x7AB7, 0x6A96, 0x1A71, 0x0A50, 0x3A33, 0x2A12,
		/*  88 -- */  0xDBFD, 0xCBDC, 0xFBBF, 0xEB9E, 0x9B79, 0x8B58, 0xBB3B, 0xAB1A,
		/*  96 -- */  0x6CA6, 0x7C87, 0x4CE4, 0x5CC5, 0x2C22, 0x3C03, 0x0C60, 0x1C41,
		/* 104 -- */  0xEDAE, 0xFD8F, 0xCDEC, 0xDDCD, 0xAD2A, 0xBD0B, 0x8D68, 0x9D49,
		/* 112 -- */  0x7E97, 0x6EB6, 0x5ED5, 0x4EF4, 0x3E13, 0x2E32, 0x1E51, 0x0E70,
		/* 120 -- */  0xFF9F, 0xEFBE, 0xDFDD, 0xCFFC, 0xBF1B, 0xAF3A, 0x9F59, 0x8F78,
		/* 128 -- */  0x9188, 0x81A9, 0xB1CA, 0xA1EB, 0xD10C, 0xC12D, 0xF14E, 0xE16F,
		/* 136 -- */  0x1080, 0x00A1, 0x30C2, 0x20E3, 0x5004, 0x4025, 0x7046, 0x6067,
		/* 144 -- */  0x83B9, 0x9398, 0xA3FB, 0xB3DA, 0xC33D, 0xD31C, 0xE37F, 0xF35E,
		/* 152 -- */  0x02B1, 0x1290, 0x22F3, 0x32D2, 0x4235, 0x5214, 0x6277, 0x7256,
		/* 160 -- */  0xB5EA, 0xA5CB, 0x95A8, 0x8589, 0xF56E, 0xE54F, 0xD52C, 0xC50D,
		/* 168 -- */  0x34E2, 0x24C3, 0x14A0, 0x0481, 0x7466, 0x6447, 0x5424, 0x4405,
		/* 176 -- */  0xA7DB, 0xB7FA, 0x8799, 0x97B8, 0xE75F, 0xF77E, 0xC71D, 0xD73C,
		/* 184 -- */  0x26D3, 0x36F2, 0x0691, 0x16B0, 0x6657, 0x7676, 0x4615, 0x5634,
		/* 192 -- */  0xD94C, 0xC96D, 0xF90E, 0xE92F, 0x99C8, 0x89E9, 0xB98A, 0xA9AB,
		/* 200 -- */  0x5844, 0x4865, 0x7806, 0x6827, 0x18C0, 0x08E1, 0x3882, 0x28A3,
		/* 208 -- */  0xCB7D, 0xDB5C, 0xEB3F, 0xFB1E, 0x8BF9, 0x9BD8, 0xABBB, 0xBB9A,
		/* 216 -- */  0x4A75, 0x5A54, 0x6A37, 0x7A16, 0x0AF1, 0x1AD0, 0x2AB3, 0x3A92,
		/* 224 -- */  0xFD2E, 0xED0F, 0xDD6C, 0xCD4D, 0xBDAA, 0xAD8B, 0x9DE8, 0x8DC9,
		/* 232 -- */  0x7C26, 0x6C07, 0x5C64, 0x4C45, 0x3CA2, 0x2C83, 0x1CE0, 0x0CC1,
		/* 240 -- */  0xEF1F, 0xFF3E, 0xCF5D, 0xDF7C, 0xAF9B, 0xBFBA, 0x8FD9, 0x9FF8,
		/* 248 -- */  0x6E17, 0x7E36, 0x4E55, 0x5E74, 0x2E93, 0x3EB2, 0x0ED1, 0x1EF0 };
		#endregion
		
		private const int MIN_PACKET_LEN = 14;
		private const int SLEEP = 500;
		private const byte SOH = 0x01;
		private const byte STX = 0x02;
		private const byte ETX = 0x03;
		private const byte EOT = 0x04;
		private const byte SUB = 0x1a;
		
		//Interface		
		public ZebraPRP(string portName) : base(portName) { }
		public ZebraPRP(string portName, int baudRate, int dataBits, Parity parity, StopBits stopBits) : base(portName, baudRate, dataBits, parity, stopBits) {
			//Constructor to create a new printer object that opens the port and reads printer status
			try {
				//Set initial printer state
				base.DataBits = 8;			//EDP only works with 8 data bits
			} 
			catch(Exception ex) { throw new ApplicationException("Could not create new ZebraPRP printer.", ex); }
		}
		~ZebraPRP() { }
		public override string WriteCommand(string cmd) {
			//Override base implementation to use Error Detection Protocol
			char[] cBuffer = new char[128];
			string response="";
			try {
				if(!this.mEDPInitialized) 
					this.mEDPInitialized = sendEDPInitRequest();
				response = sendEDPRequest(cmd);
				Thread.Sleep(SLEEP);
				base.GetHostStatus();
			} 
			catch(Exception ex) { throw ex; }
			return response;
		}
		#region EDP support
		private bool sendEDPInitRequest() {
			//Error Detection Protocol
			Encoding oEncoding = new ASCIIEncoding();
			byte[] packet;
			string packet_init="", buffer="", response="";
			char[] cBuffer = new char[32];
			int _iTimeout=0, iChars=0;
			bool result=false;
			try {
				if(base.IsOpen) {
					//
					_iTimeout = base.ReadTimeout;
					base.ReadTimeout = 50;
					packet = encodeInitializePacket();
					packet_init = oEncoding.GetString(packet);
					base.OnSentData(new PrinterDataArgs("ZebraPRP: WRITE\t" + packet_init));
					base.Write(packet, 0, packet.Length);
					Thread.Sleep(250);
					while((iChars = base.Read(cBuffer, 0, 32)) != 0) {
						for(int i=0; i<iChars; i++)
							buffer += cBuffer[i];
					}
					response = decodeResponsePacket(buffer);
					result = true;
					base.OnReceivedData(new PrinterDataArgs("ZebraPRP: READ\t" + buffer));
					base.ReadTimeout = _iTimeout;
				}
			}
			catch(Exception ex) { throw ex; }
			return result;
		}
		private string sendEDPRequest(string format) {
			//Error Detection Protocol
			Encoding oEncoding = new ASCIIEncoding();
			byte[] packet;
			string packet_req="";
			string buffer="", response="";
			char[] cBuffer = new char[128];
			int _iTimeout=0, iChars=0;
			try {
				if(base.IsOpen) {
					//
					_iTimeout = base.ReadTimeout;
					base.ReadTimeout = 50;
					packet = encodeRequestPacket(format);
					packet_req = oEncoding.GetString(packet);
					base.OnSentData(new PrinterDataArgs("ZebraPRP: WRITE\t" + packet_req));
					base.Write(packet, 0, packet.Length);
					Thread.Sleep(250);
					while((iChars = base.Read(cBuffer, 0, 128)) != 0) {
						for(int i=0; i<iChars; i++)
							buffer += cBuffer[i];
					}
					response = decodeResponsePacket(buffer);
					base.OnReceivedData(new PrinterDataArgs("ZebraPRP: READ\t" + buffer));
					base.ReadTimeout = _iTimeout;
				}
			}
			catch(Exception ex) { throw ex; }
			return response;
		}
		private byte[] encodeInitializePacket() {
			//
			byte[] packet = new byte[MIN_PACKET_LEN];
			ushort ch=0;
			
			//Create the initialize packet
			this.mTransID = (byte)'0';
			packet[0] = SOH;
			packet[1] = (byte)'0';
			packet[2] = (byte)'0';
			packet[3] = (byte)'0';
			packet[4] = (byte)'1';
			packet[5] = (byte)'2';
			packet[6] = (byte)'3';
			packet[7] = (byte)'I';
			packet[8] = this.mTransID++;
			packet[9] = STX;
			packet[10] = ETX;
			this.mCRC = 0;
			for(int j=1; j<=10; j++) {
				ch = (ushort)(packet[j] & 0xff);
				this.mCRC = (ushort)(((ushort)(this.mCRC << 8)) ^ crc_table[(this.mCRC >> 8) ^ ch]);
			}
			packet[11] = (byte)(this.mCRC >> 8);
			packet[12] = (byte)(this.mCRC & 0xff);
			packet[13] = EOT;
			return packet;
		}
		private byte[] encodeRequestPacket(string format) {
			//Encode the request packet to the printer
			byte[] packet = new byte[MIN_PACKET_LEN + 1024];
			ushort ch=0;
			int count=0, i=0;
			
			//Create the request packet
			if(format.Length < 3) throw new Exception("Format must be at least 3 characters");
			if(format.Length > 1024) throw new Exception("Format must be no more than 1024 characters");
			packet[0] = SOH;
			packet[1] = (byte)'0';
			packet[2] = (byte)'0';
			packet[3] = (byte)'0';
			packet[4] = (byte)'1';
			packet[5] = (byte)'2';
			packet[6] = (byte)'3';
			packet[7] = (byte)'P';
			packet[8] = this.mTransID;
			packet[9] = STX;
			do {  
				//Build packets for data request
				packet[8] = this.mTransID++;
				if(this.mTransID > (byte)'9') this.mTransID = (byte)'0';
				count = 10;
				do {
					ch = 0;
					if(i < format.Length)
						ch = (ushort)Convert.ToChar(format.Substring(i++, 1));
					if(ch == 0) break;
					if(ch < 0x20) {
						packet[count++] = SUB;
						packet[count++] = (byte)(ch + 0x40);
					}
					else
						packet[count++] = (byte)ch;
				} while(count < 1025);
				packet[count++] = ETX;
				if(count > 13) {
					this.mCRC = 0;
					for(int j=1 ; j<count ; j++) {
						ch = (ushort)(packet[j] & 0xff);
						this.mCRC = (ushort)(((ushort)(this.mCRC << 8)) ^ crc_table[(this.mCRC >> 8) ^ ch]);
					}
					packet[count++] = (byte)(this.mCRC >> 8);
					packet[count++] = (byte)(this.mCRC & 0xff);
					packet[count] = EOT;
					return packet;
				}
			} while(count > 11);
			return packet;
		}
		private string decodeResponsePacket(string response) {
			//Decode the response packet from the printer
			Encoding oEncoding=new ASCIIEncoding();
			byte[] packet = new byte[response.Length];
			string buffer="";
			ushort ch=0;
			int count=0;
			
			//Validate response length
			if(response.Length < MIN_PACKET_LEN) throw new ApplicationException("Invalid EDP response length.");
			
			//Convert disguised characters
			for(int i=0; i<response.Length; i++) {
				ch = (ushort)Convert.ToChar(response.Substring(i, 1));
				if(ch == 0) break;
				if(ch == SUB) {
					//Get next char after SUB (0x1a) and convert
					ch = (ushort)Convert.ToChar(response.Substring(++i, 1));
					packet[count++] = (byte)(ch - 0x40);
				}
				else
					packet[count++] = (byte)ch;
			}
			
			//Check packet acknowledgement
			switch(packet[7]) {
				case 0x41: break;	//OK
				case 0x4e: throw new ApplicationException("Negative EDP packet response.");
			}
			
			//Decode the data response
			if(packet.Length > MIN_PACKET_LEN) {
				byte[] b = new byte[128];
				bool reading=false, atEnd=false;
				int j=0;
				for(int i=MIN_PACKET_LEN; i<packet.Length; i++) {
					if(reading && !atEnd)					b[j++] = packet[i];
					if(!reading && packet[i]==STX)			reading = true;
					if(packet[i]==ETX && packet[i+3]==EOT)	atEnd = true;
					if(atEnd) {								buffer = oEncoding.GetString(b); break; }
				}
			}
			return buffer;
		}
		#endregion
	}
}