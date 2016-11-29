using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Text;

namespace Argix {
    // 
    [ServiceContract(Namespace = "http://Argix")]
    public interface ITrackingService {
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.TrackingFault")]
        TrackingItems TrackCartons(string[] itemNumbers,string companyID);
    }

    [CollectionDataContract]
    public class TrackingItems: BindingList<TrackingItem> { }

    [DataContract]
    public class TrackingItem {
        //Members
        private string mItemNumber="", mCartonNumber="", mLabelNumber="";
        private string mDateTime="", mLocation="", mStatus="";
        private string mBOLNumber="", mTLNumber="", mPONumber="";
        private string mVendorKey="", mShipmentNumber="", mCBOL="";
        private string mClient="", mClientName="";
        private string mShipper = "", mSigner="";
        private string mVendor = "",mVendorName = "",mVendorCity = "",mVendorState = "",mVendorZip = "";
        private string mAgentName="", mAgentCity="", mAgentState="", mAgentZip="";
        private string mSubAgentName="", mSubAgentCity="", mSubAgentState="", mSubAgentZip="";
        private string mStoreNumber="", mStoreName="", mStoreAddress1="", mStoreAddress2="", mStoreCity="", mStoreState="", mStoreZip="";
        private string mPickupDate="", mScheduledDeliveryDate="";
        private string mSortFacilityArrivalDate="", mSortFacilityArrivalStatus="", mSortFacilityLocation="";
        private string mActualDepartureDate="", mActualDepartureStatus="", mActualDepartureLocation="";
        private string mActualArrivalDate="", mActualArrivalStatus="", mActualArrivalLocation="";
        private string mActualStoreDeliveryDate="", mActualStoreDeliveryStatus="", mActualStoreDeliveryLocation="";
        private string mPODScanDate="", mPODScanStatus="", mPODScanLocation="";
        private string mTrackingNumber="";
        private int mIsManualEntry=0;
        private decimal mWeight=0, mScanType=0;
        private string mPVNO="";
        private string mErrorMessage="";
    
        //Interface
        public TrackingItem(string itemNumber): this(itemNumber, null) { }
        public TrackingItem(string itemNumber, TrackingDS.TrackingTableRow carton) {
            //Constructor
            this.mItemNumber = itemNumber;
            this.mStatus = "Item Not Found";
            if(carton != null) {
                this.mCartonNumber = !carton.IsCTNNull() ? carton.CTN.Trim() : "";
                this.mLabelNumber = !carton.IsLBLNull() ? carton.LBL.ToString() : "";
                this.mBOLNumber = !carton.IsBLNull() ? carton.BL.ToString() : "";
                this.mTLNumber = !carton.IsTLNull() ? carton.TL.Trim() : "";
                this.mPONumber = !carton.IsPONull() ? carton.PO.Trim() : "";
                this.mVendorKey = !carton.IsVKNull() ? carton.VK.Trim() : "";
                this.mShipmentNumber = !carton.IsShipmentNumberNull() ? carton.ShipmentNumber.Trim() : "";
                this.mCBOL = !carton.IsCBOLNull() ? carton.CBOL : "";
                this.mClient = !carton.IsCLNull() ? carton.CL.Trim() : "";
                this.mClientName = !carton.IsCLNMNull() ? carton.CLNM.Trim() : "";
                this.mShipper = !carton.IsTNull() ? carton.T : "";
                this.mSigner = !carton.IsSignerNull() ? carton.Signer : "";
                this.mVendor = !carton.IsVNull() ? carton.V.Trim() : "";
                this.mVendorName = !carton.IsVNMNull() ? carton.VNM.Trim() : "";
                this.mVendorCity = !carton.IsVCTNull() ? carton.VCT.Trim() : "";
                this.mVendorState = !carton.IsVSTNull() ? carton.VST.Trim() : "";
                this.mVendorZip = !carton.IsVZNull() ? carton.VZ.ToString() : "";
                this.mAgentName = !carton.IsAGNMNull() ? carton.AGNM.Trim() : "";
                this.mAgentCity = !carton.IsAGCTNull() ? carton.AGCT.Trim() : "";
                this.mAgentState = !carton.IsAGSTNull() ? carton.AGST.Trim() : "";
                this.mAgentZip = !carton.IsAGZNull() ? carton.AGZ.ToString() : "";
                this.mSubAgentName = !carton.IsSAGNMNull() ? carton.SAGNM.Trim() : "";
                this.mSubAgentCity = !carton.IsSAGCTNull() ? carton.SAGCT.Trim() : "";
                this.mSubAgentState = !carton.IsSAGSTNull() ? carton.SAGST.Trim() : "";
                this.mSubAgentZip = !carton.IsSAGZNull() ? carton.SAGZ.ToString() : "";
                this.mStoreNumber = !carton.IsSNull() ? carton.S.ToString() : "";
                this.mStoreName = !carton.IsSNMNull() ? carton.SNM.Trim() : "";
                this.mStoreAddress1 = !carton.IsSA1Null() ? carton.SA1.Trim() : "";
                this.mStoreAddress2 = !carton.IsSA2Null() ? carton.SA2.Trim() : "";
                this.mStoreCity = !carton.IsSCTNull() ? carton.SCT.Trim() : "";
                this.mStoreState = !carton.IsSSTNull() ? carton.SST.Trim() : "";
                this.mStoreZip = !carton.IsSZNull() ? carton.SZ.ToString() : "";
                if(!carton.IsPUDNull() && carton.PUD.Trim().Length > 0) this.mPickupDate = carton.PUD.Trim();

                this.mSortFacilityArrivalDate = this.mSortFacilityArrivalStatus = this.mSortFacilityLocation = "";
                if(!carton.IsASFDNull() && carton.ASFD.Trim().Length > 0) {
                    this.mDateTime = this.mSortFacilityArrivalDate = carton.ASFD.Trim() + " " + carton.ASFT.Trim();
                    this.mStatus = this.mSortFacilityArrivalStatus = "Arrived At Sort Facility";
                    this.mLocation = this.mSortFacilityLocation = !carton.IsSRTLOCNull() ? carton.SRTLOC.Trim() : "";
                }

                this.mActualDepartureDate = this.mActualDepartureStatus = this.mActualDepartureLocation = "";
                if(!carton.IsADPDNull() && carton.ADPD.Trim().Length > 0) {
                    this.mDateTime = this.mActualDepartureDate = carton.ADPD.Trim() + " " + carton.ADPT.Trim();
                    this.mStatus = this.mActualDepartureStatus = "Left Sort Facility";
                    this.mLocation = this.mActualDepartureLocation = !carton.IsSRTLOCNull() ? carton.SRTLOC.Trim() : "";
                }

                this.mActualArrivalDate = this.mActualArrivalStatus = this.mActualArrivalLocation = "";
                if(!carton.IsAARDNull() && carton.AARD.Trim().Length > 0) {
                    this.mDateTime = this.mActualArrivalDate = carton.AARD.Trim() + " " + carton.AART.Trim();
                    this.mStatus = this.mActualArrivalStatus = "Arrived At Delivery Terminal";
                    if(!carton.IsSAGCTNull() && carton.SAGCT.Trim().Length > 0)
                        this.mLocation = this.mActualArrivalLocation = carton.SAGCT.Trim() + "/" + carton.SAGST.Trim();
                    else
                        this.mLocation = this.mActualArrivalLocation = !carton.IsAGCTNull() ? carton.AGCT.Trim() + "/" + carton.AGST.Trim() : "";
                }

                this.mActualStoreDeliveryDate = this.mActualStoreDeliveryStatus = this.mActualStoreDeliveryLocation = this.mScheduledDeliveryDate = "";
                if(!carton.IsACTSDDNull() && carton.ACTSDD.Trim().Length > 0) {
                    this.mDateTime = this.mActualStoreDeliveryDate = carton.ACTSDD.Trim();
                    this.mStatus = this.mActualStoreDeliveryStatus = "Out For Delivery";
                    this.mLocation = this.mActualStoreDeliveryLocation = !carton.IsSCTNull() ? carton.SCT.Trim() + "/" + carton.SST.Trim() : "";
                    this.mScheduledDeliveryDate = carton.ACTSDD.Trim();
                }

                this.mPODScanDate = this.mPODScanStatus = this.mPODScanLocation = "";
                if(Convert.ToInt32(carton.SCNTP.ToString()) == 3) {
                    if(!carton.IsSCDNull() && carton.SCD.Trim().Length > 0) {
                        this.mDateTime = this.mPODScanDate = carton.SCD.Trim() + " " + carton.SCTM.Trim();
                        this.mStatus = this.mPODScanStatus = "Delivered";
                        this.mLocation = this.mPODScanLocation = carton.IsSCTNull() ? "" : carton.SCT.Trim() + "/" + carton.SST.Trim();
                    }
                }

                this.mTrackingNumber = !carton.IsTNull() ? carton.T : "";
                this.mIsManualEntry = !carton.IsISMNNull() ? carton.ISMN : 0;
                this.mWeight = carton.WT;
                this.mScanType = !carton.IsSCNTPNull() ? carton.SCNTP : 0m;
                this.mPVNO = !carton.IsPVNONull() ? carton.PVNO : "";
            }
        }
        #region Members [...]
        [DataMember]
        public string ItemNumber { get { return this.mItemNumber; } set { this.mItemNumber = value; } }
        [DataMember]
        public string CartonNumber { get { return this.mCartonNumber; } set { this.mCartonNumber = value; } }
        [DataMember]
        public string LabelNumber { get { return this.mLabelNumber; } set { this.mLabelNumber = value; } }
        [DataMember]
        public string DateTime { get { return this.mDateTime; } set { this.mDateTime = value; } }
        [DataMember]
        public string Location { get { return this.mLocation; } set { this.mLocation = value; } }
        [DataMember]
        public string Status { get { return this.mStatus; } set { this.mStatus = value; } }
        [DataMember]
        public string BOLNumber { get { return this.mBOLNumber; } set { this.mBOLNumber = value; } }
        [DataMember]
        public string TLNumber { get { return this.mTLNumber; } set { this.mTLNumber = value; } }
        [DataMember]
        public string PONumber { get { return this.mPONumber; } set { this.mPONumber = value; } }
        [DataMember]
        public string VendorKey { get { return this.mVendorKey; } set { this.mVendorKey = value; } }
        [DataMember]
        public string ShipmentNumber { get { return this.mShipmentNumber; } set { this.mShipmentNumber = value; } }
        [DataMember]
        public string CBOL { get { return this.mCBOL; } set { this.mCBOL = value; } }
        [DataMember]
        public string Client { get { return this.mClient; } set { this.mClient = value; } }
        [DataMember]
        public string ClientName { get { return this.mClientName; } set { this.mClientName = value; } }
        [DataMember]
        public string Shipper { get { return this.mShipper; } set { this.mShipper = value; } }
        [DataMember]
        public string Signer { get { return this.mSigner; } set { this.mSigner = value; } }
        [DataMember]
        public string Vendor { get { return this.mVendor; } set { this.mVendor = value; } }
        [DataMember]
        public string VendorName { get { return this.mVendorName; } set { this.mVendorName = value; } }
        [DataMember]
        public string VendorCity { get { return this.mVendorCity; } set { this.mVendorCity = value; } }
        [DataMember]
        public string VendorState { get { return this.mVendorState; } set { this.mVendorState = value; } }
        [DataMember]
        public string VendorZip { get { return this.mVendorZip; } set { this.mVendorZip = value; } }
        [DataMember]
        public string AgentName { get { return this.mAgentName; } set { this.mAgentName = value; } }
        [DataMember]
        public string AgentCity { get { return this.mAgentCity; } set { this.mAgentCity = value; } }
        [DataMember]
        public string AgentState { get { return this.mAgentState; } set { this.mAgentState = value; } }
        [DataMember]
        public string AgentZip { get { return this.mAgentZip; } set { this.mAgentZip = value; } }
        [DataMember(EmitDefaultValue=false)]
        public string SubAgentName { get { return this.mSubAgentName; } set { this.mSubAgentName = value; } }
        [DataMember(EmitDefaultValue = false)]
        public string SubAgentCity { get { return this.mSubAgentCity; } set { this.mSubAgentCity = value; } }
        [DataMember(EmitDefaultValue = false)]
        public string SubAgentState { get { return this.mSubAgentState; } set { this.mSubAgentState = value; } }
        [DataMember(EmitDefaultValue = false)]
        public string SubAgentZip { get { return this.mSubAgentZip; } set { this.mSubAgentZip = value; } }
        [DataMember]
        public string StoreNumber { get { return this.mStoreNumber; } set { this.mStoreNumber = value; } }
        [DataMember]
        public string StoreName { get { return this.mStoreName; } set { this.mStoreName = value; } }
        [DataMember]
        public string StoreAddress1 { get { return this.mStoreAddress1; } set { this.mStoreAddress1 = value; } }
        [DataMember]
        public string StoreAddress2 { get { return this.mStoreAddress2; } set { this.mStoreAddress2 = value; } }
        [DataMember]
        public string StoreCity { get { return this.mStoreCity; } set { this.mStoreCity = value; } }
        [DataMember]
        public string StoreState { get { return this.mStoreState; } set { this.mStoreState = value; } }
        [DataMember]
        public string StoreZip { get { return this.mStoreZip; } set { this.mStoreZip = value; } }
        [DataMember]
        public string PickupDate { get { return this.mPickupDate; } set { this.mPickupDate = value; } }
        [DataMember]
        public string ScheduledDeliveryDate { get { return this.mScheduledDeliveryDate; } set { this.mScheduledDeliveryDate = value; } }
        [DataMember]
        public string SortFacilityArrivalDate { get { return this.mSortFacilityArrivalDate; } set { this.mSortFacilityArrivalDate = value; } }
        [DataMember]
        public string SortFacilityArrivalStatus { get { return this.mSortFacilityArrivalStatus; } set { this.mSortFacilityArrivalStatus = value; } }
        [DataMember]
        public string SortFacilityLocation { get { return this.mSortFacilityLocation; } set { this.mSortFacilityLocation = value; } }
        [DataMember]
        public string ActualDepartureDate { get { return this.mActualDepartureDate; } set { this.mActualDepartureDate = value; } }
        [DataMember]
        public string ActualDepartureStatus { get { return this.mActualDepartureStatus; } set { this.mActualDepartureStatus = value; } }
        [DataMember]
        public string ActualDepartureLocation { get { return this.mActualDepartureLocation; } set { this.mActualDepartureLocation = value; } }
        [DataMember]
        public string ActualArrivalDate { get { return this.mActualArrivalDate; } set { this.mActualArrivalDate = value; } }
        [DataMember]
        public string ActualArrivalStatus { get { return this.mActualArrivalStatus; } set { this.mActualArrivalStatus = value; } }
        [DataMember]
        public string ActualArrivalLocation { get { return this.mActualArrivalLocation; } set { this.mActualArrivalLocation = value; } }
        [DataMember]
        public string ActualStoreDeliveryDate { get { return this.mActualStoreDeliveryDate; } set { this.mActualStoreDeliveryDate = value; } }
        [DataMember]
        public string ActualStoreDeliveryStatus { get { return this.mActualStoreDeliveryStatus; } set { this.mActualStoreDeliveryStatus = value; } }
        [DataMember]
        public string ActualStoreDeliveryLocation { get { return this.mActualStoreDeliveryLocation; } set { this.mActualStoreDeliveryLocation = value; } }
        [DataMember]
        public string PODScanDate { get { return this.mPODScanDate; } set { this.mPODScanDate = value; } }
        [DataMember]
        public string PODScanStatus { get { return this.mPODScanStatus; } set { this.mPODScanStatus = value; } }
        [DataMember]
        public string PODScanLocation { get { return this.mPODScanLocation; } set { this.mPODScanLocation = value; } }
        [DataMember]
        public string TrackingNumber { get { return this.mTrackingNumber; } set { this.mTrackingNumber = value; } }
        [DataMember]
        public int IsManualEntry { get { return this.mIsManualEntry; } set { this.mIsManualEntry = value; } }
        [DataMember]
        public decimal Weight { get { return this.mWeight; } set { this.mWeight = value; } }
        [DataMember]
        public decimal ScanType { get { return this.mScanType; } set { this.mScanType = value; } }
        [DataMember]
        public string PVNO { get { return this.mPVNO; } set { this.mPVNO = value; } }
        [DataMember]
        public string ErrorMessage { get { return this.mErrorMessage; } set { this.mErrorMessage = value; } }
        #endregion
    }
    
    [DataContract]
    public class TrackingFault {
        private Exception _ex;
        public TrackingFault(Exception ex) { this._ex = ex; }

        [DataMember]
        public Exception Exception { get { return this._ex; } set { this._ex = value; } }
    }
}
