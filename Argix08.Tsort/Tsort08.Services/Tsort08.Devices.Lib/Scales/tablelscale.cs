//	File:	tablescale.cs
//	Author:	J. Heary
//	Date:	02/14/05
//	Desc:	Implementation of IScale where weight is determined by lookup in a remote table.
//	---------------------------------------------------------------------------
using System;
using System.Threading;

namespace Tsort.Devices.Scales {
    /// <summary>Implementation of IScale where weight is determined by lookup in a remote table.</summary>
	public class TableScale : IScale {
		//Members
		private decimal mWeight=0;

        /// <summary>Event that returns the current scale weight.</summary>
        public event ScaleEventHandler ScaleWeightReading=null;
		
		//Interface
		/// <summary>Constructor.</summary>
        public TableScale(): base() { }
        #region IDevice: DefaultSettings, Settings, On, TurnOn, TurnOff
        /// <summary>Gets a PortSettings structure initialized with the default port settings (if applicable).</summary>
        public PortSettings DefaultSettings { get { return new PortSettings(); } }
        /// <summary>Gets or sets the the current port settings (if applicable).</summary>
        public PortSettings Settings { get { return new PortSettings(); } set { } }
        /// <summary>Gets a value indicating if the scale is on.</summary>
        public bool On { get { return true; } }
        /// <summary>Turns the scale on.</summary>
        public void TurnOn() { if(this.ScaleWeightReading != null) this.ScaleWeightReading(this,new ScaleEventArgs(this.mWeight,ScaleError.None)); }
        /// <summary>Turns the scale off.</summary>
        public void TurnOff() { }
        #endregion
        #region IScale: Type, Weight, GetWeight, IsStable, Zero
        /// <summary>Gets the type of scale.</summary>
        public string Type { get { return "Table"; } }
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
        public bool IsStable { get { return true; } }
        /// <summary>Zeros the scale.</summary>
        public void Zero() { if(this.ScaleWeightReading != null) this.ScaleWeightReading(this,new ScaleEventArgs(this.mWeight,ScaleError.None)); }
        #endregion
    }
}
