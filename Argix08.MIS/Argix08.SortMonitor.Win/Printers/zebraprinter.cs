//	File:	zebraprinter.cs
//	Author:	J. Heary
//	Date:	11/17/03
//	Desc:	Represents the state and behavior of a zebra zpl printer.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;

namespace Tsort.Printers {
	//
	public class ZebraPrinter {
		//Members
		private string m_sPrinterType="";
		private string m_sModel="", m_sVersion="", m_sResolution="", m_sMemory="", m_sX="";
		private string m_sRAMTotal="", m_sRAMMax="", m_sRAMAvailable="";
		private string m_sCommSettings="", m_sPaperOut="", m_sPause="", m_sLabelLength="";
		private string m_sFormatsInReceiveBuffer="", m_sBufferFull="", m_sDiagnosticsMode="", m_sPartialFormat="", m_sUnused1="";
		private string m_sCorruptRAM="", m_sUnderTemp="", m_sOverTemp="", m_sFunctionSettings="", m_sUnused2="";
		private string m_sHeadUp="", m_sRibbonOut="", m_sThermalTransMode="", m_sPrintMode="", m_sPrintWidthMode="";
		private string m_sLabelWaiting="", m_sLabelsRemainingInBatch="", m_sFormatWhilePrinting="", m_sGraphicImagesInMemory="";
		private string m_sPassword="", m_sStaticRAM="";
		
		private string m_sBaudRate="";
		private string m_sDataBits="";
		private string m_sParity="";
		private string m_sStopBits="";
		private string m_sHandshake="";
		private string m_sMediaType="";
		private string m_sSensorProfile="";
		private string m_sPrintMode_="";
		
		//Constants
		
		//Interface
		public ZebraPrinter(string printerType) {
			//Constructor
			try {
				this.m_sPrinterType = printerType;
			}
			catch(Exception ex) { throw ex; }
		}
		#region Accessors
		public string Type { get { return this.m_sPrinterType; } }
		public string Model { get { return this.m_sModel; } }
		public string Version { get { return this.m_sVersion; } }
		public string Resolution { get { return this.m_sResolution; } }
		public string Memory { get { return this.m_sMemory; } }
		public string X { get { return this.m_sX; } }
		public string RAMTotal { get { return this.m_sRAMTotal; } }
		public string RAMMax { get { return this.m_sRAMMax; } }
		public string RAMAvailable { get { return this.m_sRAMAvailable; } }
		public string CommSettings { get { return this.m_sCommSettings; } }
		public string PaperOut { get { return this.m_sPaperOut; } }
		public string Pause { get { return this.m_sPause; } }
		public string LabelLength { get { return this.m_sLabelLength; } }
		public string FormatsInReceiveBuffer { get { return this.m_sFormatsInReceiveBuffer; } }
		public string BufferFull { get { return this.m_sBufferFull; } }
		public string DiagnosticsMode { get { return this.m_sDiagnosticsMode; } }
		public string PartialFormat { get { return this.m_sPartialFormat; } }
		public string Unused1 { get { return this.m_sUnused1; } }
		public string CorruptRAM { get { return this.m_sCorruptRAM; } }
		public string UnderTemp { get { return this.m_sUnderTemp; } }
		public string OverTemp { get { return this.m_sOverTemp; } }
		public string FunctionSettings { get { return this.m_sFunctionSettings; } }
		public string Unused2 { get { return this.m_sUnused2; } }
		public string HeadUp { get { return this.m_sHeadUp; } }
		public string RibbonOut { get { return this.m_sRibbonOut; } }
		public string ThermalTransMode { get { return this.m_sThermalTransMode; } }
		public string PrintMode { get { return this.m_sPrintMode; } }
		public string PrintWidthMode { get { return this.m_sPrintWidthMode; } }
		public string LabelWaiting { get { return this.m_sLabelWaiting; } }
		public string LabelsRemainingInBatch { get { return this.m_sLabelsRemainingInBatch; } }
		public string FormatWhilePrinting { get { return this.m_sFormatWhilePrinting; } }
		public string GraphicImagesInMemory { get { return this.m_sGraphicImagesInMemory; } }
		public string Password { get { return this.m_sPassword; } }
		public string StaticRAM { get { return this.m_sStaticRAM; } }
		
		
		public string BaudRate { get { return this.m_sBaudRate; } }
		public string DataBits { get { return this.m_sDataBits; } }
		public string Parity { get { return this.m_sParity; } }
		public string StopBits { get { return this.m_sStopBits; } }
		public string Handshake { get { return this.m_sHandshake; } }
		public string MediaType { get { return this.m_sMediaType; } }
		public string SensorProfile { get { return this.m_sSensorProfile; } }
		public string PrintMode_ { get { return this.m_sPrintMode_; } }
		#endregion
		public void UpdateZebraInformation(string info) {
			//Interpret zebra printer information
			//Keyword2=Zebra Status; Keyword3=Printer Information:
			//170XiIIIPlus,V42.12.2   ,12,12288KB : 12288,12288,12288 : 135,0,0,0918,000,0,0,0,000,0,0,0; 001,0,0,0,1,1,A,0,00000000,1,000; 1234,0
			string s1="", s2="", s3="";
			try {
				string[] sTokens = info.Split(':');
				s1 = (sTokens.Length>0) ? sTokens[0] : "";
				UpdateHostIdentification(s1);
				s2 = (sTokens.Length>1) ? sTokens[1] : "";
				UpdateHostMemoryStatus(s2);
				s3 = (sTokens.Length>2) ? sTokens[2] : "";
				UpdateHostStatus(s3);
			}
			catch(Exception) { }
		}
		public string UpdateHostIdentification(string sBuffer) {
			//Parse host identification	
			if(sBuffer.Length > 0) {
				string[] sTokens = sBuffer.Split((char)',');
				this.m_sModel = (sTokens.Length>0) ? sTokens[0].Trim() : "";
				this.m_sVersion = (sTokens.Length>1) ? sTokens[1].Trim() : "";
				this.m_sResolution = (sTokens.Length>2) ? sTokens[2] : "";
				this.m_sMemory = (sTokens.Length>3) ? sTokens[3] : "";
				this.m_sX = (sTokens.Length>4) ? sTokens[4] : "";
			}
			return sBuffer;
		}
		public string UpdateHostMemoryStatus(string sBuffer) {
			//Parse host memory status
			if(sBuffer!="") {
				string[] sTokens = sBuffer.Split((char)',');
				this.m_sRAMTotal = (sTokens.Length>0) ? sTokens[0].Trim() : "";
				this.m_sRAMMax = (sTokens.Length>1) ? sTokens[1].Trim() : "";
				this.m_sRAMAvailable = (sTokens.Length>2) ? sTokens[2] : "";
			}
			return sBuffer;
		}
		public string UpdateHostStatus(string sBuffer) {
			//Parse host identification
			string s1="", s2="", s3="";
			if(sBuffer!="") {
//				int iPos_stx, iPos_etx;
				string[] sTokens = sBuffer.Split(';');
				s1 = (sTokens.Length>0) ? sTokens[0] : "";
				if(s1!="") {
					string[] s1s = s1.Split((char)',');
					this.m_sCommSettings = (s1s.Length>0) ? s1s[0].Trim() : "";
					updateCOMSettings(this.m_sCommSettings);
					this.m_sPaperOut = (s1s.Length>1) ? s1s[1].Trim() : "";
					this.m_sPause = (s1s.Length>2) ? s1s[2].Trim() : "";
					this.m_sLabelLength = (s1s.Length>3) ? s1s[3].Trim() : "";
					this.m_sFormatsInReceiveBuffer = (s1s.Length>4) ? s1s[4].Trim() : "";
					this.m_sBufferFull = (s1s.Length>5) ? s1s[5].Trim() : "";
					this.m_sDiagnosticsMode = (s1s.Length>6) ? s1s[6].Trim() : "";
					this.m_sPartialFormat = (s1s.Length>7) ? s1s[7].Trim() : "";
					this.m_sUnused1 = (s1s.Length>8) ? s1s[8].Trim() : "";
					this.m_sCorruptRAM = (s1s.Length>9) ? s1s[9].Trim() : "";
					this.m_sUnderTemp = (s1s.Length>10) ? s1s[10].Trim() : "";
					this.m_sOverTemp = (s1s.Length>11) ? s1s[11].Trim() : "";
				}
				s2 = (sTokens.Length>1) ? sTokens[1] : "";
				if(s2!="") {
					string[] s2s = s2.Split((char)',');
					this.m_sFunctionSettings = (s2s.Length>0) ? s2s[0].Trim() : "";
					updateFunctionSettings(this.m_sFunctionSettings);
					this.m_sUnused2 = (s2s.Length>1) ? s2s[1].Trim() : "";
					this.m_sHeadUp = (s2s.Length>2) ? s2s[2].Trim() : "";
					this.m_sRibbonOut = (s2s.Length>3) ? s2s[3].Trim() : "";
					this.m_sThermalTransMode = (s2s.Length>4) ? s2s[4].Trim() : "";
					this.m_sPrintMode = (s2s.Length>5) ? s2s[5].Trim() : "";
					switch(this.m_sPrintMode) {
						case "0": this.m_sPrintMode = "rewind"; break;
						case "1": this.m_sPrintMode = "peel off"; break;
						case "2": this.m_sPrintMode = "tear off"; break;
						case "3": this.m_sPrintMode = "cutter"; break;
						case "4": this.m_sPrintMode = "applictr"; break;
					 }
					this.m_sPrintWidthMode = (s2s.Length>6) ? s2s[6].Trim() : "";
					this.m_sLabelWaiting = (s2s.Length>7) ? s2s[7].Trim() : "";
					this.m_sLabelsRemainingInBatch = (s2s.Length>8) ? s2s[8].Trim() : "";
					this.m_sFormatWhilePrinting = (s2s.Length>9) ? s2s[9].Trim() : "";
					this.m_sGraphicImagesInMemory = (s2s.Length>10) ? s2s[10].Trim() : "";
				}
				s3 = (sTokens.Length>2) ? sTokens[2] : "";
				if(s3!="") {
					string[] s3s = s3.Split((char)',');
					this.m_sPassword = (s3s.Length>0) ? s3s[0].Trim() : "";
					this.m_sStaticRAM = (s3s.Length>1) ? s3s[1].Trim() : "";
				}
			}
			return s1 + "; " + s2 + "; " + s3;
		}

		private void updateCOMSettings(string com) {
			//Update COM settings
			int iCOM=0;
			string a0="", a1="", a2="", a3="", a4="", a5="", a6="", a7="", a8="0";
			try {
				iCOM = Convert.ToInt32(com);
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
					case "0000": this.m_sBaudRate = "110"; break;
					case "0001": this.m_sBaudRate = "300"; break;
					case "0010": this.m_sBaudRate = "600"; break;
					case "0011": this.m_sBaudRate = "1200"; break;
					case "0100": this.m_sBaudRate = "2400"; break;
					case "0101": this.m_sBaudRate = "4800"; break;
					case "0110": this.m_sBaudRate = "9600"; break;
					case "0111": this.m_sBaudRate = "19200"; break;
					case "1000": this.m_sBaudRate = "28800"; break;
					case "1001": this.m_sBaudRate = "38400"; break;
					case "1010": this.m_sBaudRate = "57600"; break;
					case "1011": this.m_sBaudRate = "14400"; break;
				}
				this.m_sDataBits = (a3 == "0") ? "7" : "8";
				this.m_sStopBits = (a4 == "0") ? "2" : "1";
				//this. = (a5 == "0") ? "Disable" : "Enable";
				this.m_sParity = (a6 == "0") ? "Odd" : "Even";
				this.m_sHandshake = (a7 == "0") ? "Xon/Xoff" : "DTR";
			}
			catch(Exception) { }
		}
		private void updateFunctionSettings(string func) {
			//Update function settings
			int iFunc=0;
			string m0="", m1="", m2="", m3="", m4="", m5="", m6="", m7="";
			try {
				iFunc = Convert.ToInt32(func);
				m0 = ((iFunc & 0x01) == 0x01) ? "1" : "0";
				m1 = ((iFunc & 0x02) == 0x02) ? "1" : "0";
				m2 = ((iFunc & 0x04) == 0x04) ? "1" : "0";
				m3 = ((iFunc & 0x08) == 0x08) ? "1" : "0";
				m4 = ((iFunc & 0x10) == 0x10) ? "1" : "0";
				m5 = ((iFunc & 0x20) == 0x20) ? "1" : "0";
				m6 = ((iFunc & 0x40) == 0x40) ? "1" : "0";
				m7 = ((iFunc & 0x80) == 0x80) ? "1" : "0";
				this.m_sMediaType = (m7 == "0") ? "Die Cut" : "Continuous";
				this.m_sSensorProfile = (m6 == "0") ? "Off" : "";
				//this. = (m5 == "0") ? "Off" : "On";
				this.m_sPrintMode_ = (m0 == "0") ? "Direct Thermal" : "Thermal Transfer";
			}
			catch(Exception) { }
		}
	}
}
