using System;
using System.Data;
using System.Configuration;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;

namespace Argix.Freight {
	//
	public class BearwareTrip {
		//Class members
		private string mNumber="";
		private int mCartonCount=0;
		private string mCarrier="";
		private string mTrailerNumber="";
		private DateTime mImported = Convert.ToDateTime(null);
		private DateTime mStarted = Convert.ToDateTime(null);
		private DateTime mStopped = Convert.ToDateTime(null);
		private DateTime mReceived = Convert.ToDateTime(null);
		private DateTime mScanned = Convert.ToDateTime(null);
		private DateTime mOSDSend = Convert.ToDateTime(null);
		private DateTime mExported = Convert.ToDateTime(null);
		private int mCartonsExported=-1;
				
		//public event EventHandler Changed=null; 
		
		//Interface
		public BearwareTrip() : this(null) { }
		public BearwareTrip(BearwareDS.BwareTripTableRow trip) {
			//Constructor
			if(trip != null) {
				this.mNumber = trip.Number.Trim();
				if(!trip.IsCartonCountNull()) this.mCartonCount = trip.CartonCount;
				if(!trip.IsCarrierNull()) this.mCarrier = trip.Carrier;
				if(!trip.IsTrailerNumberNull()) this.mTrailerNumber = trip.TrailerNumber;
				if(!trip.IsStartedNull()) this.mStarted = trip.Started;
				if(!trip.IsExportedNull()) this.mExported = trip.Exported;
				if(!trip.IsStoppedNull()) this.mStopped = trip.Stopped;
				if(!trip.IsImportedNull()) this.mImported = trip.Imported;
				if(!trip.IsScannedNull()) this.mScanned = trip.Scanned;
				if(!trip.IsOSDSendNull()) this.mOSDSend = trip.OSDSend;
				if(!trip.IsReceivedNull()) this.mReceived = trip.Received;
				if(!trip.IsCartonsExportedNull()) this.mCartonsExported = trip.CartonsExported;
			}
		}
		public BearwareTrip(string number, int cartonCount, string carrier, string trailerNumber) {
			//Constructor
			this.mNumber = number.Trim();
			this.mCartonCount = cartonCount;
			this.mCarrier = carrier;
			this.mTrailerNumber = trailerNumber;
		}
		#region Accessors\modifiers [Members...]
		public string Number { get { return this.mNumber; } set { this.mNumber = value; } }
		public int CartonCount { get { return this.mCartonCount; } set { this.mCartonCount = value; } }
		public string Carrier { get { return this.mCarrier; } set { this.mCarrier = value; } }
		public string TrailerNumber { get { return this.mTrailerNumber; } set { this.mTrailerNumber = value; } }
		public DateTime Imported { get { return this.mImported; } set { this.mImported = value; } }
		public bool IsImported { get { return this.mImported != Convert.ToDateTime(null); } }
		public DateTime Started { get { return this.mStarted; } set { this.mStarted = value; } }
		public bool IsStarted { get { return this.mStarted != Convert.ToDateTime(null); } }
		public DateTime Stopped { get { return this.mStopped; } set { this.mStopped = value; } }
		public bool IsStopped { get { return this.mStopped != Convert.ToDateTime(null); } }
		public DateTime Exported { get { return this.mExported; } set { this.mExported = value; } }
		public bool IsExported { get { return this.mExported != Convert.ToDateTime(null); } }
		public DateTime Received { get { return this.mReceived; } set { this.mReceived = value; } }
		public bool IsReceived { get { return this.mReceived != Convert.ToDateTime(null); } }
		public DateTime Scanned { get { return this.mScanned; } set { this.mScanned = value; } }
		public bool IsScanned { get { return this.mScanned != Convert.ToDateTime(null); } }
		public DateTime OSDSend { get { return this.mOSDSend; } set { this.mOSDSend = value; } }
		public bool IsOSDSent { get { return this.mOSDSend != Convert.ToDateTime(null); } }
		public int CartonsExported { get { return this.mCartonsExported; } }
		#endregion
	}
}
