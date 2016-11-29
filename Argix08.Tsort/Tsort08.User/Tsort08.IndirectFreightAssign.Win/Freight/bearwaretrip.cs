using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;
using Argix.Data;

namespace Argix.Freight {
	//
	public class BearwareTrip {
		//Class members
		public static bool AllowNew=true;
		public static bool AllowImport=true;
		public static string ImportFile="c:\\tripsumm.xml";
		public static string ExportPath="c:\\";
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
				
		public event EventHandler Changed=null; 
		
		//Interface
		public BearwareTrip() : this(null) { }
		public BearwareTrip(BearwareDS.BwareTripTableRow trip) {
			//Constructor
			try {
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
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new BearwareTrip instance.", ex); }
		}
		public BearwareTrip(string number, int cartonCount, string carrier, string trailerNumber) {
			//Constructor
			try {
				this.mNumber = number.Trim();
				this.mCartonCount = cartonCount;
				this.mCarrier = carrier;
				this.mTrailerNumber = trailerNumber;
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new BearwareTrip instance.", ex); }
		}
		#region Accessors\modifiers: ..., ToDataSet(). ToString()
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
		
		public DataSet ToDataSet() {
			//Return a dataset containing values for this terminal
			BearwareDS ds=null;
			try {
				ds = new BearwareDS();
				BearwareDS.BwareTripTableRow trip = ds.BwareTripTable.NewBwareTripTableRow();
				trip.Number = this.mNumber;
				trip.CartonCount = this.mCartonCount;
				trip.Carrier = this.mCarrier;
				trip.TrailerNumber = this.mTrailerNumber;
				if(this.mStarted != Convert.ToDateTime(null)) trip.Started = this.mStarted;
				if(this.mExported != Convert.ToDateTime(null)) trip.Exported = this.mExported;
				if(this.mStopped != Convert.ToDateTime(null)) trip.Stopped = this.mStopped;
				if(this.mImported != Convert.ToDateTime(null)) trip.Imported = this.mImported;
				if(this.mScanned != Convert.ToDateTime(null)) trip.Scanned = this.mScanned;
				if(this.mOSDSend != Convert.ToDateTime(null)) trip.OSDSend = this.mOSDSend;
				if(this.mReceived != Convert.ToDateTime(null)) trip.Received = this.mReceived;
				if(this.mCartonsExported != -1) trip.CartonsExported = this.mCartonsExported;
				ds.BwareTripTable.AddBwareTripTableRow(trip);
			}
			catch(Exception) { }
			return ds;
		}
		public override string ToString() {
			//Custom ToString() method
			string sThis=base.ToString();
			try {
				//Form string detail of this object
				StringBuilder builder = new StringBuilder();
				builder.Append("Number=" + this.mNumber + "\t");
				builder.Append("CartonCount=" + this.mCartonCount.ToString() + "\t");
				builder.Append("Carrier=" + this.mCarrier + "\t");
				builder.Append("TrailerNumber=" + this.mTrailerNumber + "\n");
				sThis = builder.ToString();
			} 
			catch(Exception) { }
			return sThis;
		}
		#endregion
		public bool Create() {
			//Create a new trip
			object received=null, scanned=null, osdSend=null;
			bool bRet=false;
			try {
				DateTime imported = DateTime.Now;
				if(this.mReceived != Convert.ToDateTime(null)) received = this.mReceived;
				if(this.mScanned != Convert.ToDateTime(null)) scanned = this.mScanned;
				if(this.mOSDSend != Convert.ToDateTime(null)) osdSend = this.mOSDSend;
                bRet = App.Mediator.ExecuteNonQuery(FreightFactory.USP_TRIPCREATE, new object[] { this.mNumber, this.mCartonCount, this.mCarrier, this.mTrailerNumber, imported, received, scanned, osdSend });
				if(bRet) {
					this.mImported = imported;
					if(this.Changed != null) this.Changed(this, EventArgs.Empty);
				}
			}
			catch(Exception ex) { throw ex; }
			return bRet;
		}
		public bool StartSort() {
			//Start sort for this trip
			bool bRet=false;
			try {
				DateTime started = DateTime.Now;
                bRet = App.Mediator.ExecuteNonQuery(FreightFactory.USP_TRIPSTARTSORT, new object[] { started, this.mNumber });
				if(bRet) {
					this.mStarted = started;
					if(this.Changed != null) this.Changed(this, EventArgs.Empty);
				}
			}
			catch (Exception ex) { throw ex; }
			return bRet;
		}
		public bool StopSort() {
			//Stop sort for this trip
			bool bRet=false;
			try {
				DateTime stopped = DateTime.Now;
                bRet = App.Mediator.ExecuteNonQuery(FreightFactory.USP_TRIPSTOPSORT, new object[] { stopped, this.mNumber });
				if(bRet) {
					this.mStopped = stopped;
					if(this.Changed != null) this.Changed(this, EventArgs.Empty);
				}
			}
			catch (Exception ex) { throw ex; }
			return bRet;
		}
		public int Export() {
			//Export this trip to an xml file and return number of cartons exported
			int iCartons=0;
			try {
				//Create xml
				TripCartons trips = new TripCartons();
				trips.Trip.AddTripRow(this.mNumber);
                DataSet ds = App.Mediator.FillDataset(FreightFactory.USP_SCANDETAIL, FreightFactory.TBL_SCANDETAIL, new object[] { this.mNumber });
				if(ds != null) {
                    for (int i = 0; i < ds.Tables[FreightFactory.TBL_SCANDETAIL].Rows.Count; i++) {
						TripCartons.CartonRow row = trips.Carton.NewCartonRow();
                        row.Barcode = ds.Tables[FreightFactory.TBL_SCANDETAIL].Rows[i]["Barcode"].ToString();
                        row.Cube = Convert.ToInt32(ds.Tables[FreightFactory.TBL_SCANDETAIL].Rows[i]["Cube"]);
                        row.ScanTime = Convert.ToDateTime(ds.Tables[FreightFactory.TBL_SCANDETAIL].Rows[i]["ScanTime"]);
						row.TripRow = trips.Trip[0];
						trips.Carton.AddCartonRow(row);
					}
				}
				iCartons = trips.Carton.Rows.Count;
				
				//Export to file; and update exported field in trip record
				trips.WriteXml(BearwareTrip.ExportPath + this.mNumber + ".xml");
				DateTime exported = DateTime.Now;
                bool bRet = App.Mediator.ExecuteNonQuery(FreightFactory.USP_TRIPUPDATE, new object[] { exported, this.mCartonCount, this.mNumber });
				if(bRet) {
					this.mExported = exported;
					if(this.Changed != null) this.Changed(this, EventArgs.Empty);
				}
			}
			catch (Exception ex) { throw ex; }
			return iCartons;
		}
	}
}
