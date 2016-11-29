//	File:	ibshipment.cs
//	Author:	J. Heary
//	Date:	05/17/04
//	Desc:	User layer business class for a InboundFreight.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Text;
using Tsort.Enterprise;

namespace Tsort.Freight {
    //
    public class InboundFreight {
        //Members
        private int mTerminalID=0;
        private string mFreightID="";
        private string mFreightType="";
        private string mCurrentLocation="";
        private int mTDSNumber=0;
        private string mTrailerNumber="";
        private string mStorageTrailerNumber="";
        private string mPickupDate="";
        private string mPickupNumber="";
        private string mStatus="";
        private int mCartons=0;
        private int mPallets=0;
        private int mCarrierNumber=0;
        private int mDriverNumber=0;
        private string mFloorStatus="";
        private string mSealNumber="";
        private string mUnloadedStatus="";
        private string mVendorKey="";
        private string mReceiveDate="";
        private decimal mCubeRatio=0.0m;
        private Client mClient=null;
        private Shipper mShipper=null;

        //Constants
        public const string TSORT = "tsort";
        public const string RETURNS = "returns";

        //Interface
        public InboundFreight(InboundFreightDS.InboundFreightTableRow shipment,Client client,Shipper shipper) {
            //Constructor
            try {
                if(shipment != null) {
                    if(!shipment.IsFreightIDNull()) this.mFreightID = shipment.FreightID;
                    if(!shipment.IsFreightTypeNull()) this.mFreightType = shipment.FreightType;
                    if(!shipment.IsCurrentLocationNull()) this.mCurrentLocation = shipment.CurrentLocation;
                    if(!shipment.IsTDSNumberNull()) this.mTDSNumber = shipment.TDSNumber;
                    if(!shipment.IsTrailerNumberNull()) this.mTrailerNumber = shipment.TrailerNumber;
                    if(!shipment.IsStorageTrailerNumberNull()) this.mStorageTrailerNumber = shipment.StorageTrailerNumber;
                    if(!shipment.IsPickupDateNull()) this.mPickupDate = shipment.PickupDate;
                    if(!shipment.IsPickupNumberNull()) this.mPickupNumber = shipment.PickupNumber;
                    if(!shipment.IsStatusNull()) this.mStatus = shipment.Status;
                    if(!shipment.IsCartonsNull()) this.mCartons = shipment.Cartons;
                    if(!shipment.IsPalletsNull()) this.mPallets = shipment.Pallets;
                    if(!shipment.IsCarrierNumberNull()) this.mCarrierNumber = shipment.CarrierNumber;
                    if(!shipment.IsDriverNumberNull()) this.mDriverNumber = shipment.DriverNumber;
                    if(!shipment.IsFloorStatusNull()) this.mFloorStatus = shipment.FloorStatus;
                    if(!shipment.IsSealNumberNull()) this.mSealNumber = shipment.SealNumber;
                    if(!shipment.IsUnloadedStatusNull()) this.mUnloadedStatus = shipment.UnloadedStatus;
                    if(!shipment.IsVendorKeyNull()) this.mVendorKey = shipment.VendorKey;
                    if(!shipment.IsReceiveDateNull()) this.mReceiveDate = shipment.ReceiveDate;
                    if(!shipment.IsTerminalIDNull()) this.mTerminalID = shipment.TerminalID;
                    if(!shipment.IsCubeRatioNull()) this.mCubeRatio = shipment.CubeRatio;
                }
                this.mClient = client;
                this.mShipper = shipper;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new inbound shipment instance.",ex); }
        }
        #region Accessors\modifiers: [Members]..., ToDataSet()
        public int TerminalID { get { return this.mTerminalID; } }
        public string FreightID { get { return this.mFreightID; } }
        public string FreightType { get { return this.mFreightType; } }
        public string CurrentLocation { get { return this.mCurrentLocation; } }
        public int TDSNumber { get { return this.mTDSNumber; } }
        public string TrailerNumber { get { return this.mTrailerNumber; } }
        public string StorageTrailerNumber { get { return this.mStorageTrailerNumber; } }
        public string PickupDate { get { return this.mPickupDate; } }
        public string PickupNumber { get { return this.mPickupNumber; } }
        public string Status { get { return this.mStatus; } }
        public int Cartons { get { return this.mCartons; } }
        public int Pallets { get { return this.mPallets; } }
        public int CarrierNumber { get { return this.mCarrierNumber; } }
        public int DriverNumber { get { return this.mDriverNumber; } }
        public string FloorStatus { get { return this.mFloorStatus; } }
        public string SealNumber { get { return this.mSealNumber; } }
        public string UnloadedStatus { get { return this.mUnloadedStatus; } }
        public string VendorKey { get { return this.mVendorKey; } }
        public string ReceiveDate { get { return this.mReceiveDate; } }
        public decimal CubeRatio { get { return this.mCubeRatio; } }

        public DataSet ToDataSet() {
            //Return a dataset containing values for this terminal
            InboundFreightDS ds=null;
            try {
                ds = new InboundFreightDS();
                InboundFreightDS.InboundFreightTableRow shipment = ds.InboundFreightTable.NewInboundFreightTableRow();
                shipment.FreightID = this.mFreightID;
                shipment.FreightType = this.mFreightType;
                shipment.CurrentLocation = this.mCurrentLocation;
                shipment.TDSNumber = this.mTDSNumber;
                shipment.TrailerNumber = this.mTrailerNumber;
                shipment.StorageTrailerNumber = this.mStorageTrailerNumber;
                if(this.mClient != null) shipment.ClientNumber = this.mClient.Number;
                if(this.mClient != null) shipment.ClientName = this.mClient.Name;
                if(this.mShipper != null) shipment.ShipperNumber = this.mShipper.NUMBER;
                if(this.mShipper != null) shipment.ShipperName = this.mShipper.NAME;
                shipment.PickupDate = this.mPickupDate;
                shipment.PickupNumber = this.mPickupNumber;
                shipment.Status = this.mStatus;
                shipment.Cartons = this.mCartons;
                shipment.Pallets = this.mPallets;
                shipment.CarrierNumber = this.mCarrierNumber;
                shipment.DriverNumber = this.mDriverNumber;
                shipment.FloorStatus = this.mFloorStatus;
                shipment.SealNumber = this.mSealNumber;
                shipment.UnloadedStatus = this.mUnloadedStatus;
                shipment.VendorKey = this.mVendorKey;
                shipment.ReceiveDate = this.mReceiveDate;
                shipment.TerminalID = this.mTerminalID;
                shipment.CubeRatio = this.mCubeRatio;
                ds.InboundFreightTable.AddInboundFreightTableRow(shipment);
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
}
