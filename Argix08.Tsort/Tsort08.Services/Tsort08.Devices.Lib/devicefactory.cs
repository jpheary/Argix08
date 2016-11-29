//	File:	devicefactory.cs
//	Author:	J. Heary
//	Date:	03/22/05
//	Desc:	Provides printer and scale services.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using Tsort.Devices.Scales;
using Tsort.Devices.Printers;

namespace Tsort.Devices {
    /// <summary>Provides printer and scale services.</summary>
	public class DeviceFactory {
		//Members
		private static string[] _printerTypes=null;
		private static string[] _scaleTypes=null;
		
		//Interface
        /// <summary>Class constructor.</summary>
        static DeviceFactory() {
			//Static constructor (insures single instance of this class)
			try {
                _printerTypes = new string[] { "Console","Serial","Zebra","ZebraPrp","ZebraWin" };
				_scaleTypes = new string[] { "Automatic", "Constant", "Manual", "Table" };
			}
			catch(Exception) { }
		}
        private DeviceFactory() { }
        /// <summary>Returns supported printer types.</summary>
        public static string[] PrinterTypes { get { return _printerTypes; } }
        /// <summary>Determines if a printer type is supported.</summary>
        public static bool PrinterTypeExists(string printerType) {
			//Validate printer type
			bool exists=false;
			try {
				for(int i=0; i<DeviceFactory._printerTypes.Length; i++) {
					if(DeviceFactory._printerTypes[i].ToLower() == printerType.ToLower()) {
						exists = true;
						break;
					}
				}
			}
			catch(Exception ex) { throw ex; }
			return exists;
		}
        /// <summary>Creates a new instance of a printer for the specified printer type.</summary>
        public static ILabelPrinter CreatePrinter(string printerType,string portName) {
            //Create a printer of type ILabelPrinter
            ILabelPrinter printer=null;
            switch(printerType.ToLower()) {
                case "console":     printer = new ConsolePrinter(); break;
                case "serial":      printer = new SerialPrinter(portName); break;
                case "zebra":
                case "110":
                    printer = new Zebra(portName); break;
                case "zebraprp":    printer = new Zebra(portName); break;
                case "zebrawin":    printer = new ZebraWin(); break;
                default:            throw new Exception("Printer type not found.");
            }
            return printer;
        }
        /// <summary>Returns supported scale types.</summary>
        public static string[] ScaleTypes { get { return _scaleTypes; } }
        /// <summary>Determines if a scale type is supported.</summary>
        public static bool ScaleTypeExists(string scaleType) {
			//Validate printer type
			bool exists=false;
			try {
				for(int i=0; i<DeviceFactory._scaleTypes.Length; i++) {
					if(DeviceFactory._scaleTypes[i].ToLower() == scaleType.ToLower()) {
						exists = true;
						break;
					}
				}
			}
			catch(Exception ex) { throw ex; }
			return exists;
		}
        /// <summary>Creates a new instance of a scale for the specified scale type.</summary>
        public static IScale CreateScale(string scaleType,string comPort) { 
			//Create a scale of type IScale
			IScale scale=null;
			switch(scaleType.ToLower()) {
				case "automatic":	scale = new AutoScale(comPort); break;
                case "manual":      scale = new ManualScale(); break;
                case "constant":    scale = new ConstantScale(); break;
                case "table":       scale = new TableScale(); break;
				default:			throw new Exception("Scale type not found.");
			}
            return scale;
		}
	}
}
