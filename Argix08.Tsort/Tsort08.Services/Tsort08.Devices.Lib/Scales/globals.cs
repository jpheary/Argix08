//	File:	globals.cs
//	Author:	J. Heary
//	Date:	04/07/04
//	Desc:	Scale interfaces, enumerators, event delegates, eventargs, exceptions, etc.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Runtime.Serialization;
using System.IO.Ports;

namespace Tsort.Devices.Scales {
	//Interfaces
    /// <summary>Interface definition for Tsort scales.</summary>
	public interface IScale: IDevice {
        /// <summary>Gets the type of scale.</summary>
        string Type { get; }
        /// <summary>Gets a key-based weight (if applicable), or returns the current weight.</summary>
        decimal GetWeight(string key);
        /// <summary>Gets or sets the current weight.</summary>
        decimal Weight { get; set; }
        /// <summary>Gets a value indicating if the scale is stable.</summary>
        bool IsStable { get; }
        /// <summary>Zeros the scale.</summary>
        void Zero();

        /// <summary>Event that returns the current scale weight.</summary>
        event ScaleEventHandler ScaleWeightReading;
	}
		
    /// <summary>Scale errors enumeration.</summary>
    [Flags]
    public enum ScaleError {
		None = 0x0,
		RS232 = 0x00000001,
		ScaleStatus = 0x00000002, 
		ScaleUnstable = 0x00000004
	}

    /// <summary>Delegate for scale events containing ScaleEventArgs.</summary>
    public delegate void ScaleEventHandler(object source,ScaleEventArgs e);
    /// <summary>Scale data for scale events.</summary>
    public class ScaleEventArgs:EventArgs {
		private decimal _weight=0;
		private ScaleError _error;
		public ScaleEventArgs(decimal weight, ScaleError error) { this._weight = weight; this._error = error; }
		public decimal Weight { get { return this._weight; } set { this._weight = value; } }
		public ScaleError Error { get { return this._error; } set { this._error = value; } }
	}
}
