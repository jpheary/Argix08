using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Freight {
    //Shipping Interfaces
    [ServiceContract(Namespace="http://Argix.Freight")]
    public interface IFreightService {
        //General Freight Interface
    
    }

    [CollectionDataContract]
    public class Pickups:BindingList<Pickup> {
        public Pickups() { }
    }

    [DataContract]
    public class Pickup {
        //Members
        private string _id="";
        private string _clientnumber="",_divisionnumber="",_clientname="";
        private string _shippernumber="",_shippername="";
        private string _pickupdate="";
        private int _pickupnumber=0,_tdsnumber=0;
        private string _freighttype="";
        private string _vendorkey="",_trailernumber = "",_sealnumber = "";

        //Interface
        public Pickup() : this(null) { }
        public Pickup(PickupDS.PickupTableRow pickup) {
            //Constructor
            try {
                if(pickup != null) {
                    if(!pickup.IsIDNull()) this._id = pickup.ID;
                    if(!pickup.IsClientNumberNull()) this._clientnumber = pickup.ClientNumber.Trim();
                    if(!pickup.IsDivisionNumberNull()) this._divisionnumber = pickup.DivisionNumber.Trim();
                    if(!pickup.IsClientNameNull()) this._clientname = pickup.ClientName.Trim();
                    if(!pickup.IsShipperNumberNull()) this._shippernumber = pickup.ShipperNumber.Trim();
                    if(!pickup.IsShipperNameNull()) this._shippername = pickup.ShipperName.Trim();
                    if(!pickup.IsPickUpDateNull()) this._pickupdate = pickup.PickUpDate;
                    if(!pickup.IsPickupNumberNull()) this._pickupnumber = pickup.PickupNumber;
                    if(!pickup.IsFreightTypeNull()) this._freighttype = pickup.FreightType.Trim();
                    if(!pickup.IsTDSNumberNull()) this._tdsnumber = pickup.TDSNumber;
                    if(!pickup.IsVendorKeyNull()) this._vendorkey = pickup.VendorKey.Trim();
                    if(!pickup.IsTrailerNumberNull()) this._trailernumber = pickup.TrailerNumber.Trim();
                    if(!pickup.IsSealNumberNull()) this._sealnumber = pickup.SealNumber.Trim();
                }
            }
            catch(Exception ex) { throw new ApplicationException("",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string ID { get { return this._id; } set { this._id = value; } }
        [DataMember]
        public string ClientNumber { get { return this._clientnumber; } set { this._clientnumber = value; } }
        [DataMember]
        public string DivisionNumber { get { return this._divisionnumber; } set { this._divisionnumber = value; } }
        [DataMember]
        public string ClientName { get { return this._clientname; } set { this._clientname = value; } }
        [DataMember]
        public string ShipperNumber { get { return this._shippernumber; } set { this._shippernumber = value; } }
        [DataMember]
        public string ShipperName { get { return this._shippername; } set { this._shippername = value; } }
        [DataMember]
        public string PickUpDate { get { return this._pickupdate; } set { this._pickupdate = value; } }
        [DataMember]
        public int PickupNumber { get { return this._pickupnumber; } set { this._pickupnumber = value; } }
        [DataMember]
        public string FreightType { get { return this._freighttype; } set { this._freighttype = value; } }
        [DataMember]
        public int TDSNumber { get { return this._tdsnumber; } set { this._tdsnumber = value; } }
        [DataMember]
        public string VendorKey { get { return this._vendorkey; } set { this._vendorkey = value; } }
        [DataMember]
        public string TrailerNumber { get { return this._trailernumber; } set { this._trailernumber = value; } }
        [DataMember]
        public string SealNumber { get { return this._sealnumber; } set { this._sealnumber = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class SortedItems:BindingList<SortedItem> {
        public SortedItems() { }
    }

    [DataContract]
    public class SortedItem {
        //Members
        private string _labelseqnumber="";
        private string _clientnumber="",_clientdivnumber="";
        private string _agentnumber="",_vendornumber="";
        private string _sortedlocation="",_damagecode="";
        private DateTime _pickupdate;
        private int _pickupnumber=0;
        private int _store=0;
        private string _zonecode="",_trailerloadnumber="",_itemtype="";
        private int _itemweight=0;
        private string _vendorkey="", _vemdoritemnumber="";
        private string _returnflag="", _returnnumber="", _shiftnumber="";
        private DateTime _shiftdate, _endtime, _arcdate;
        private string _station="";
        private int _itemcube=0;
        private DateTime _sortdate;
        private string _sannumber="";
        private int _elapsedseconds=0, _downseconds=0;
        private string _ponumber="", _ostrackingnumber="", _shippingmethod="";
        private DateTime _sampledate;
        private string _scanstring="";
        private int _inboundlabelid=0;
        private string _freighttype="";

        //Interface
        public SortedItem() : this(null) { }
        public SortedItem(SortedItemDS.SortedItemTableRow sortedItem) {
            //Constructor
            try {
                if(sortedItem != null) {
                    this._labelseqnumber = sortedItem.LABEL_SEQ_NUMBER;
                    if(!sortedItem.IsCLIENT_NUMBERNull()) this._clientnumber = sortedItem.CLIENT_NUMBER.Trim();
                    if(!sortedItem.IsCLIENT_DIV_NUMNull()) this._clientdivnumber = sortedItem.CLIENT_DIV_NUM.Trim();
                    if(!sortedItem.IsAGENT_NUMBERNull()) this._agentnumber = sortedItem.AGENT_NUMBER.Trim();
                    if(!sortedItem.IsVENDOR_NUMBERNull()) this._vendornumber = sortedItem.VENDOR_NUMBER.Trim();
                    if(!sortedItem.IsSORTED_LOCATIONNull()) this._sortedlocation = sortedItem.SORTED_LOCATION.Trim();
                    if(!sortedItem.IsDAMAGE_CODENull()) this._damagecode = sortedItem.DAMAGE_CODE.Trim();
                    if(!sortedItem.IsPICKUP_DATENull()) this._pickupdate = sortedItem.PICKUP_DATE;
                    if(!sortedItem.IsPICKUP_NUMBERNull()) this._pickupnumber = sortedItem.PICKUP_NUMBER;
                    if(!sortedItem.IsSTORENull()) this._store = sortedItem.STORE;
                    if(!sortedItem.IsZONE_CODENull()) this._zonecode = sortedItem.ZONE_CODE.Trim();
                    if(!sortedItem.IsTRAILER_LOAD_NUMNull()) this._trailerloadnumber = sortedItem.TRAILER_LOAD_NUM.Trim();
                    if(!sortedItem.IsITEM_TYPENull()) this._itemtype = sortedItem.ITEM_TYPE.Trim();
                    if(!sortedItem.IsITEM_WEIGHTNull()) this._itemweight = sortedItem.ITEM_WEIGHT;
                    if(!sortedItem.IsVENDOR_KEYNull()) this._vendorkey = sortedItem.VENDOR_KEY.Trim();
                    if(!sortedItem.IsVENDOR_ITEM_NUMBERNull()) this._vemdoritemnumber = sortedItem.VENDOR_ITEM_NUMBER.Trim();
                    if(!sortedItem.IsRETURN_FLAGNull()) this._returnflag = sortedItem.RETURN_FLAG.Trim();
                    if(!sortedItem.IsRETURN_NUMBERNull()) this._returnnumber = sortedItem.RETURN_NUMBER.Trim();
                    if(!sortedItem.IsSHIFT_NUMBERNull()) this._shiftnumber = sortedItem.SHIFT_NUMBER.Trim();
                    if(!sortedItem.IsSHIFT_DATENull()) this._shiftdate = sortedItem.SHIFT_DATE;
                    if(!sortedItem.IsEND_TIMENull()) this._endtime = sortedItem.END_TIME;
                    if(!sortedItem.IsARC_DATENull()) this._arcdate = sortedItem.ARC_DATE;
                    if(!sortedItem.IsSTATIONNull()) this._station = sortedItem.STATION.Trim();
                    if(!sortedItem.IsITEM_CUBENull()) this._itemcube = sortedItem.ITEM_CUBE;
                    if(!sortedItem.IsSORT_DATENull()) this._sortdate = sortedItem.SORT_DATE;
                    if(!sortedItem.IsSAN_NUMBERNull()) this._sannumber = sortedItem.SAN_NUMBER.Trim();
                    if(!sortedItem.IsELAPSED_SECONDSNull()) this._elapsedseconds = sortedItem.ELAPSED_SECONDS;
                    if(!sortedItem.IsDOWN_SECONDSNull()) this._downseconds = sortedItem.DOWN_SECONDS;
                    if(!sortedItem.IsPO_NUMBERNull()) this._ponumber = sortedItem.PO_NUMBER.Trim();
                    if(!sortedItem.IsOS_TRACKING_NUMBERNull()) this._ostrackingnumber = sortedItem.OS_TRACKING_NUMBER.Trim();
                    if(!sortedItem.IsSHIPPING_METHODNull()) this._shippingmethod = sortedItem.SHIPPING_METHOD.Trim();
                    if(!sortedItem.IsSAMPLE_DATENull()) this._sampledate = sortedItem.SAMPLE_DATE;
                    if(!sortedItem.IsScanStringNull()) this._scanstring = sortedItem.ScanString.Trim();
                    if(!sortedItem.IsInboundLabelIDNull()) this._inboundlabelid = sortedItem.InboundLabelID;
                    if(!sortedItem.IsFreightTypeNull()) this._freighttype = sortedItem.FreightType.Trim();
                }
            }
            catch(Exception ex) { throw new ApplicationException("",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string LabelSequenceNumber { get { return this._labelseqnumber; } set { this._labelseqnumber = value; } }
        [DataMember]
        public string ClientNumber { get { return this._clientnumber; } set { this._clientnumber = value; } }
        [DataMember]
        public string ClientDivisionNumber { get { return this._clientdivnumber; } set { this._clientdivnumber = value; } }
        [DataMember]
        public string AgentNumber { get { return this._agentnumber; } set { this._agentnumber = value; } }
        [DataMember]
        public string VendorNumber { get { return this._vendornumber; } set { this._vendornumber = value; } }
        [DataMember]
        public string SortedLocation { get { return this._sortedlocation; } set { this._sortedlocation = value; } }
        [DataMember]
        public string DamageCode { get { return this._damagecode; } set { this._damagecode = value; } }
        [DataMember]
        public DateTime PickupDate { get { return this._pickupdate; } set { this._pickupdate = value; } }
        [DataMember]
        public int PickupNumber { get { return this._pickupnumber; } set { this._pickupnumber = value; } }
        [DataMember]
        public int Store { get { return this._store; } set { this._store = value; } }
        [DataMember]
        public string ZoneCode { get { return this._zonecode; } set { this._zonecode = value; } }
        [DataMember]
        public string TrailerLoadNumber { get { return this._trailerloadnumber; } set { this._trailerloadnumber = value; } }
        [DataMember]
        public string ItemType { get { return this._itemtype; } set { this._itemtype = value; } }
        [DataMember]
        public int ItemWeight { get { return this._itemweight; } set { this._itemweight = value; } }
        [DataMember]
        public string VendorKey { get { return this._vendorkey; } set { this._vendorkey = value; } }
        [DataMember]
        public string VendorItemNumber { get { return this._vemdoritemnumber; } set { this._vemdoritemnumber = value; } }
        [DataMember]
        public string ReturnFlag { get { return this._returnflag; } set { this._returnflag = value; } }
        [DataMember]
        public string ReturnNumber { get { return this._returnnumber; } set { this._returnnumber = value; } }
        [DataMember]
        public string ShiftNumber { get { return this._shiftnumber; } set { this._shiftnumber = value; } }
        [DataMember]
        public DateTime ShiftDate { get { return this._shiftdate; } set { this._shiftdate = value; } }
        [DataMember]
        public DateTime EndTime { get { return this._endtime; } set { this._endtime = value; } }
        [DataMember]
        public DateTime ArcDate { get { return this._arcdate; } set { this._arcdate = value; } }
        [DataMember]
        public string Station { get { return this._station; } set { this._station = value; } }
        [DataMember]
        public int ItemCube { get { return this._itemcube; } set { this._itemcube = value; } }
        [DataMember]
        public DateTime SortDate { get { return this._sortdate; } set { this._sortdate = value; } }
        [DataMember]
        public string SanNumber { get { return this._sannumber; } set { this._sannumber = value; } }
        [DataMember]
        public int ElapsedSeconds { get { return this._elapsedseconds; } set { this._elapsedseconds = value; } }
        [DataMember]
        public int DownSeconds { get { return this._downseconds; } set { this._downseconds = value; } }
        [DataMember]
        public string PONumber { get { return this._ponumber; } set { this._ponumber = value; } }
        [DataMember]
        public string OSTrackingNumber { get { return this._ostrackingnumber; } set { this._ostrackingnumber = value; } }
        [DataMember]
        public string ShippingMethod { get { return this._shippingmethod; } set { this._shippingmethod = value; } }
        [DataMember]
        public DateTime SampleDate { get { return this._sampledate; } set { this._sampledate = value; } }
        [DataMember]
        public string ScanString { get { return this._scanstring; } set { this._scanstring = value; } }
        [DataMember]
        public int InboundLabelID { get { return this._inboundlabelid; } set { this._inboundlabelid = value; } }
        [DataMember]
        public string FreightType { get { return this._freighttype; } set { this._freighttype = value; } }
        #endregion
    }
}
