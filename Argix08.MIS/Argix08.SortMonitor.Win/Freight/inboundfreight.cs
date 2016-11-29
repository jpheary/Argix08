//	File:	inboundfreight.cs
//	Author:	J. Heary
//	Date:	05/21/08
//	Desc:	Inbound freight base class.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Text;
using Tsort.Enterprise;

namespace Tsort.Freight {
	//
	public abstract class InboundFreight {
		//Members
		protected string mFreightID="";
        protected int mTerminalID=0;
        protected string mCarrier="";
		protected string mTrailerNumber="";
		protected int mCartonCount=0;
		
		//Interface
        public InboundFreight(): this("", 0, "", "", 0) { }
		public InboundFreight(string freightID, int terminalID, string carrier, string trailerNumber, int cartonCount) {
			//Constructor
			try { 
					this.mFreightID = freightID.Trim();
					this.mTerminalID = terminalID;
					this.mCarrier = carrier.Trim();
					this.mTrailerNumber = trailerNumber.Trim();
					this.mCartonCount = cartonCount;
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new InboundFreight instance.", ex); }
		}
		#region Accessors\modifiers: [Members]..., ToDataSet()
		public string FreightID { get { return this.mFreightID; } }
        public int TerminalID { get { return this.mTerminalID; } }
		public string Carrier { get { return this.mCarrier; } }
        public string TrailerNumber { get { return this.mTrailerNumber; } }
		public int CartonCount { get { return this.mCartonCount; } }
		public virtual DataSet ToDataSet() {
			//Return a dataset containing values for this terminal
			return null;
		}
		#endregion
	}

	public class DirectInboundFreight: InboundFreight {
		//Members
		private string mFreightType="";
		private string mCurrentLocation="";
		private int mTDSNumber=0;
		private string mStorageTrailerNumber="";
		private string mPickupDate="";
		private string mPickupNumber="";
		private string mStatus="";
		private int mPallets=0;
		private int mDriverNumber=0;
		private string mFloorStatus="";
		private string mSealNumber="";
		private string mUnloadedStatus="";
		private string mVendorKey="";
		private string mReceiveDate="";
		private decimal mCubeRatio=0.0m;
		private Client mClient=null;
		private Shipper mShipper=null;
		
		public const string TSORT = "tsort";
		public const string RETURNS = "returns";
		
		//Interface
		public DirectInboundFreight(InboundFreightDS.DirectInboundFreightTableRow shipment, Client client, Shipper shipper) {
			//Constructor
			try { 
				if(shipment != null) {
					if(!shipment.IsFreightIDNull()) base.mFreightID = shipment.FreightID.Trim();
					if(!shipment.IsTerminalIDNull()) base.mTerminalID = shipment.TerminalID;
                    if(!shipment.IsFreightTypeNull()) this.mFreightType = shipment.FreightType.Trim();
					if(!shipment.IsCurrentLocationNull()) this.mCurrentLocation = shipment.CurrentLocation.Trim();
					if(!shipment.IsTDSNumberNull()) this.mTDSNumber = shipment.TDSNumber;
					if(!shipment.IsTrailerNumberNull()) base.mTrailerNumber = shipment.TrailerNumber.Trim();
					if(!shipment.IsStorageTrailerNumberNull()) this.mStorageTrailerNumber = shipment.StorageTrailerNumber.Trim();
					if(!shipment.IsPickupDateNull()) this.mPickupDate = shipment.PickupDate.Trim();
					if(!shipment.IsPickupNumberNull()) this.mPickupNumber = shipment.PickupNumber.Trim();
					if(!shipment.IsStatusNull()) this.mStatus = shipment.Status.Trim();
					if(!shipment.IsCartonsNull()) base.mCartonCount = shipment.Cartons;
					if(!shipment.IsPalletsNull()) this.mPallets = shipment.Pallets;
					if(!shipment.IsCarrierNumberNull()) base.mCarrier = shipment.CarrierNumber.ToString().Trim();
					if(!shipment.IsDriverNumberNull()) this.mDriverNumber = shipment.DriverNumber;
					if(!shipment.IsFloorStatusNull()) this.mFloorStatus = shipment.FloorStatus.Trim();
					if(!shipment.IsSealNumberNull()) this.mSealNumber = shipment.SealNumber.Trim();
					if(!shipment.IsUnloadedStatusNull()) this.mUnloadedStatus = shipment.UnloadedStatus.Trim();
					if(!shipment.IsVendorKeyNull()) this.mVendorKey = shipment.VendorKey.Trim();
					if(!shipment.IsReceiveDateNull()) this.mReceiveDate = shipment.ReceiveDate.Trim();
					if(!shipment.IsCubeRatioNull()) this.mCubeRatio = shipment.CubeRatio;
				}
				this.mClient = client;
				this.mShipper = shipper;
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new inbound shipment instance.", ex); }
		}
		#region Accessors\modifiers: [Members]..., ToDataSet()
		public string FreightType { get { return this.mFreightType; } }
		public string CurrentLocation { get { return this.mCurrentLocation; } }
		public int TDSNumber { get { return this.mTDSNumber; } }
		public string StorageTrailerNumber { get { return this.mStorageTrailerNumber; } }
		public string PickupDate { get { return this.mPickupDate; } }
		public string PickupNumber { get { return this.mPickupNumber; } }
		public string Status { get { return this.mStatus; } }
		public int Pallets { get { return this.mPallets; } }
		public int DriverNumber { get { return this.mDriverNumber; } }
		public string FloorStatus { get { return this.mFloorStatus; } }
		public string SealNumber { get { return this.mSealNumber; } }
		public string UnloadedStatus { get { return this.mUnloadedStatus; } }
		public string VendorKey { get { return this.mVendorKey; } }
		public string ReceiveDate { get { return this.mReceiveDate; } }
		public decimal CubeRatio { get { return this.mCubeRatio; } }
		public override DataSet ToDataSet() {
			//Return a dataset containing values for this terminal
			InboundFreightDS ds=null;
			try {
				ds = new InboundFreightDS();
				InboundFreightDS.DirectInboundFreightTableRow shipment = ds.DirectInboundFreightTable.NewDirectInboundFreightTableRow();
				shipment.FreightID = base.mFreightID;
				shipment.TerminalID = base.mTerminalID;
                shipment.FreightType = this.mFreightType;
				shipment.CurrentLocation = this.mCurrentLocation;
				shipment.TDSNumber = this.mTDSNumber;
				shipment.TrailerNumber = base.mTrailerNumber;
				shipment.StorageTrailerNumber = this.mStorageTrailerNumber;
				if(this.mClient != null) shipment.ClientNumber = this.mClient.Number;
				if(this.mClient != null) shipment.ClientName = this.mClient.Name;
				if(this.mShipper != null) shipment.ShipperNumber = this.mShipper.NUMBER;
				if(this.mShipper != null) shipment.ShipperName = this.mShipper.NAME;
				shipment.PickupDate = this.mPickupDate;
				shipment.PickupNumber = this.mPickupNumber;
				shipment.Status = this.mStatus;
				shipment.Cartons = base.mCartonCount;
				shipment.Pallets = this.mPallets;
				shipment.CarrierNumber = int.Parse(base.mCarrier);
				shipment.DriverNumber = this.mDriverNumber;
				shipment.FloorStatus = this.mFloorStatus;
				shipment.SealNumber = this.mSealNumber;
				shipment.UnloadedStatus = this.mUnloadedStatus;
				shipment.VendorKey = this.mVendorKey;
				shipment.ReceiveDate = this.mReceiveDate;
				shipment.CubeRatio = this.mCubeRatio;
				ds.DirectInboundFreightTable.AddDirectInboundFreightTableRow(shipment);
				if(this.mClient != null) ds.Merge(this.mClient.ToDataSet());
				if(this.mShipper != null) ds.Merge(this.mShipper.ToDataSet());
				ds.AcceptChanges();
			}
			catch(Exception) { }
			return ds;
		}
		#endregion
		public Client Client { get { return this.mClient; } }
		public Shipper Shipper { get { return this.mShipper; } }
		public bool IsTsort { get { return mFreightType.ToLower().CompareTo(TSORT) == 0; } }
		public bool IsReturns { get { return mFreightType.ToLower().CompareTo(RETURNS) == 0; } }
	}

	public class IndirectInboundFreight: InboundFreight {
		//Members
		private DateTime mStarted=Convert.ToDateTime(null);
		private DateTime mStopped=Convert.ToDateTime(null);
		private DateTime mExported=Convert.ToDateTime(null);
		private DateTime mImported=Convert.ToDateTime(null);
		private DateTime mScanned=Convert.ToDateTime(null);
		private DateTime mOSDSend=Convert.ToDateTime(null);
		private DateTime mReceived=Convert.ToDateTime(null);
		private int mCartonsExported=0;
		
		//Interface
		public IndirectInboundFreight() {}
		public IndirectInboundFreight(InboundFreightDS.IndirectInboundFreightTableRow trip) {
			//Constructor
			try {
				//Configure this trip from the trip configuration information
				if(trip != null) {
					if(!trip.IsNumberNull()) base.mFreightID = trip.Number.Trim();
					if(!trip.IsCartonCountNull()) base.mCartonCount = trip.CartonCount;
					if(!trip.IsCarrierNull()) base.mCarrier = trip.Carrier.Trim();
					if(!trip.IsTrailerNumberNull()) base.mTrailerNumber = trip.TrailerNumber.Trim();
					if(!trip.IsStartedNull()) this.mStarted = trip.Started;
					if(!trip.IsStoppedNull()) this.mStopped = trip.Stopped;
					if(!trip.IsExportedNull()) this.mExported = trip.Exported;
					if(!trip.IsImportedNull()) this.mImported = trip.Imported;
					if(!trip.IsScannedNull()) this.mScanned = trip.Scanned;
					if(!trip.IsOSDSendNull()) this.mOSDSend = trip.OSDSend;
					if(!trip.IsReceivedNull()) this.mReceived = trip.Received;
					if(!trip.IsCartonsExportedNull()) this.mCartonsExported = trip.CartonsExported;
				}
			} 
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new IndirectInboundFreight instance.", ex); }
		}
		#region Accessors\Modifiers: Number, ToDataSet()
		public string Number { get { return base.mFreightID; } }
		public override DataSet ToDataSet() {
			//Return a dataset containing values for this terminal
			InboundFreightDS ds=null;
			try {
				ds = new InboundFreightDS();
				InboundFreightDS.IndirectInboundFreightTableRow trip = ds.IndirectInboundFreightTable.NewIndirectInboundFreightTableRow();
				trip.Number = base.mFreightID;
				trip.CartonCount = base.mCartonCount;
				trip.Carrier = base.mCarrier;
				trip.TrailerNumber = base.mTrailerNumber;
				ds.IndirectInboundFreightTable.AddIndirectInboundFreightTableRow(trip);
			}
			catch(Exception) { }
			return ds;
		}
		#endregion
	}
}
