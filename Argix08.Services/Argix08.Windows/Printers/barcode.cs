//	File:	barcode.cs
//	Author:	J. Heary
//	Date:	09/09/04
//	Desc:	Provides barcode encoding of data strings.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Argix.Windows.Printers {
	//
	public class BarCode {
		//Members
		
		//Constants
		//Rivers Edge barcode fonts
		private const char RE_START128_A = (char)8225;		//135 8225;
		private const char RE_START128_B = (char)202;
		private const char RE_START128_C = (char)8240;
		private const char RE_STOP128 = (char)352;			//138 352;
		
		//Elfring barcode fonts
		private const char ELF_START128_A = (char)123;
		private const char ELF_START128_B = (char)124;
		private const char ELF_START128_C = (char)125;
		private const char ELF_STOP128 = (char)126;
		
		//IDAutomation barcode fonts
		private const char IDA_START128_A = (char)203;
		private const char IDA_START128_B = (char)204;
		private const char IDA_START128_C = (char)205;
		private const char IDA_STOP128 = (char)206;
		
		//Interface
		static BarCode() { }
		public static string Encode128AB(string BarCodeData, string BarCodeName, Barcode128 Subset) {
			//Encode BarCodeData for 128 barcode printing
			string sBarCode="", sBarCodeEncoded="";			
			
			BarCodeData = BarCodeData.Trim();
			Debug.Write("BarCodeData=" + BarCodeData);
			sBarCodeEncoded = Encode(BarCodeData, BarCodeName, Subset);
			Debug.Write("\t" + sBarCodeEncoded);
			//Build ouput string; trailing space is for Windows rasterization bug
			switch(BarCodeName) {
				case "Code128A": 
					switch(Subset) {
						case Barcode128.A: sBarCode = Char.ToString(RE_START128_A) + sBarCodeEncoded + CalcChecksum(BarCodeData, Subset, 103) + Char.ToString(RE_STOP128); break;
						case Barcode128.B: sBarCode = Char.ToString(RE_START128_B) + sBarCodeEncoded + CalcChecksum(BarCodeData, Subset, 104) + Char.ToString(RE_STOP128); break;
						case Barcode128.C: sBarCode = Char.ToString(RE_START128_C) + sBarCodeEncoded + CalcChecksum(BarCodeData, Subset, 105) + Char.ToString(RE_STOP128); break;
					}
					break;
				case "Bar Sample 128AB": 
					switch(Subset) {
						case Barcode128.A: sBarCode = Char.ToString(ELF_START128_A) + sBarCodeEncoded + CalcChecksum(BarCodeData, Subset, 103) + Char.ToString(ELF_STOP128); break;
						case Barcode128.B: sBarCode = Char.ToString(ELF_START128_B) + sBarCodeEncoded + CalcChecksum(BarCodeData, Subset, 104) + Char.ToString(ELF_STOP128); break;
						case Barcode128.C: sBarCode = Char.ToString(ELF_START128_C) + sBarCodeEncoded + CalcChecksum(BarCodeData, Subset, 105) + Char.ToString(ELF_STOP128); break;
					}
					break;
				case "Bar Sample 128AB HR": 
					switch(Subset) {
						case Barcode128.A: sBarCode = Char.ToString(ELF_START128_A) + sBarCodeEncoded + CalcChecksum(BarCodeData, Subset, 103) + Char.ToString(ELF_STOP128); break;
						case Barcode128.B: sBarCode = Char.ToString(ELF_START128_B) + sBarCodeEncoded + CalcChecksum(BarCodeData, Subset, 104) + Char.ToString(ELF_STOP128); break;
						case Barcode128.C: sBarCode = Char.ToString(ELF_START128_C) + sBarCodeEncoded + CalcChecksum(BarCodeData, Subset, 105) + Char.ToString(ELF_STOP128); break;
					}
					break;
				case "IDAutomationSC128S": 					
					switch(Subset) {
						case Barcode128.A: sBarCode = Char.ToString(IDA_START128_A) + sBarCodeEncoded + CalcChecksum(BarCodeData, Subset, 103) + Char.ToString(IDA_STOP128); break;
						case Barcode128.B: sBarCode = Char.ToString(IDA_START128_B) + sBarCodeEncoded + CalcChecksum(BarCodeData, Subset, 104) + Char.ToString(IDA_STOP128); break;
						case Barcode128.C: sBarCode = Char.ToString(IDA_START128_C) + sBarCodeEncoded + CalcChecksum(BarCodeData, Subset, 105) + Char.ToString(IDA_STOP128); break;
					}
					break;
			}
			Debug.Write("\t" + sBarCode + "\n");
			return sBarCode;
		}
		public static string Encode(string BarCodeData, string BarCodeName, Barcode128 Subset) {
			//Map ascii values to barcode ascii values
			string sBarCodeEncoded="";			
			
			//Output string - map the ascii set to barcode128 ascii set
			//- no spaces in TrueType fonts; quotes replaced for Word mailmerge bug
			switch(BarCodeName) {
				case "Code128A": 
					if(Subset == Barcode128.C) {
						if(BarCodeData.Length % 2 != 0) BarCodeData = "0" + BarCodeData;
						Debug.Write("\t" + BarCodeData);
						for(int i=0; i<BarCodeData.Length; i+=2) {
							int iVal = Convert.ToInt32(BarCodeData.Substring(i, 2));
							if(iVal == 0)
								sBarCodeEncoded += Char.ToString((char)192);
							else if(iVal == 7)
								sBarCodeEncoded += Char.ToString((char)193);
							else if(iVal == 64)
								sBarCodeEncoded += Char.ToString((char)194);
							else if(iVal >= 95 && iVal <= 101)
								sBarCodeEncoded += Char.ToString((char)(iVal + 100));
							else if(iVal == 102)
								sBarCodeEncoded += Char.ToString((char)134);
							else
								sBarCodeEncoded += Char.ToString((char)(iVal + 32));
						}
					}
					else {
						for(int i=0; i<BarCodeData.Length; i++) {
							char cChar = Convert.ToChar(BarCodeData.Substring(i, 1));
							if(cChar == 32)						//space
								sBarCodeEncoded += Char.ToString((char)192);
							else if(cChar == 39)				//'
								sBarCodeEncoded += Char.ToString((char)193);
							else if(cChar == 96)				//`
								sBarCodeEncoded += Char.ToString((char)194);
							else if(cChar == 127)				//`
								sBarCodeEncoded += Char.ToString((char)195);
							else if(cChar >= 33 && cChar <= 95)	//numbers, capital letters, punctuation- map direct
								sBarCodeEncoded += Char.ToString(cChar);
						}
					}
					break;
				case "Bar Sample 128AB": 
				case "Bar Sample 128AB HR": 
					for(int i=0; i<BarCodeData.Length; i++) {
						char cChar = Convert.ToChar(BarCodeData.Substring(i, 1));
						if(cChar == 32)						//space
							sBarCodeEncoded += Char.ToString((char)228);
						else if(cChar == 34)				//'
							sBarCodeEncoded += Char.ToString((char)226);
						else 
							sBarCodeEncoded += Char.ToString(cChar);
					}
					break;
				case "IDAutomationSC128S": 
					for(int i=0; i<BarCodeData.Length; i++) {
						char cChar = Convert.ToChar(BarCodeData.Substring(i, 1));
						if(cChar == 32)						//space
							sBarCodeEncoded += Char.ToString((char)194);
						else 
							sBarCodeEncoded += Char.ToString(cChar);
					}
					break;
			}
			return sBarCodeEncoded;
		}
		public static string CalcChecksum(string BarCodeData, Barcode128 Subset, int StartValue) {
			//Calculate a checksum for BarCodeData (mod 103)
			int iValue=0, iWeightedValue=0, iCheckSumValue=0;
			string sCheckSum="";
			
			//Initialize weighted value with startcode value
			iWeightedValue = StartValue;
			if(Subset == Barcode128.C) {
				if(BarCodeData.Length % 2 != 0) BarCodeData = "0" + BarCodeData;
				for(int i=0; i<BarCodeData.Length; i+=2) {
					//Find the ASCII value of the each character; determine barcode 128 value
					iValue = Convert.ToInt32(BarCodeData.Substring(i, 2));
					
					//Sum 'weighted' values for checksum calculation
					iWeightedValue += (((i/2)+1) * iValue);
				}
			}
			else {
				for(int i=0; i<BarCodeData.Length; i++) {
					//Find the ASCII value of the each character; determine barcode 128 value
					char cChar = Convert.ToChar(BarCodeData.Substring(i, 1));
					if(cChar >= 32 && cChar <= 126) 
						iValue = cChar - 32;
					else  
						iValue = cChar - 103;
				
					//Sum 'weighted' values for checksum calculation
					iWeightedValue += ((i+1) * iValue);
				}
			}
			
			//Find the remainder when sum is divided by 103
			iCheckSumValue = iWeightedValue % 103;
			Debug.Write("\t" + iCheckSumValue.ToString());
			
			//Translate checksum value to an ASCII character (since checksum is remainder
			//of division by 103, checksum value is in range 0 - 102)
			if(iCheckSumValue == 0)
				sCheckSum = Char.ToString((char)192);
			else if(iCheckSumValue == 7)
				sCheckSum = Char.ToString((char)193);
			else if(iCheckSumValue == 64)
				sCheckSum = Char.ToString((char)194);
			else if(iCheckSumValue >= 95 && iCheckSumValue <= 101)
				sCheckSum = Char.ToString((char)(iCheckSumValue + 100));
			else if(iCheckSumValue == 102)
				sCheckSum = Char.ToString((char)134);
			else
				sCheckSum = Char.ToString((char)(iCheckSumValue + 32));
			return sCheckSum;
		}
	}
}
