//	File:	destinationrouting.cs
//	Author:	J. Heary
//	Date:	05/08/07
//	Desc:	Provides information related to destination and routing.
//	Rev:	01/08/08 (jph)- added members _zonetlDate, _zonetlCloseNumber.
//			07/09/08 (jph)- added Manifestxxx members.
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;

namespace Tsort.Sort {
    //
    internal class DestinationRouting {
        //Members
        private string _clientnumber="";
        private string _destinationname="";
        private int _destinationnumber=0;
        private string _destinationphone="";
        private string _destinationaddressline1="";
        private string _destinationaddressline2="";
        private string _destinationaddresscity="";
        private string _destinationaddressstate="";
        private string _destinationaddresszip="";
        private string _destinationaltroute="";
        private string _destinationroute="";
        private string _destinationuserlabeldata="";
        private string _destinationstatus="";
        private DateTime _destinationopendate;
        private string _locallane="";
        private string _osursacode="";
        private int _ossequence=335291;				//1Z + account_number + indicator + sequenceNumber + checkDigit
        //1Z + 181254 + 03 + 0335291 + 9
        private string _zonecode="";
        private string _zonetype;
        private string _outboundlabeltype="";
        private string _zonelane="";
        private string _zonelanesmallsort="";
        private string _zonetl="";
        private DateTime _zonetlDate=DateTime.MinValue;
        private string _zonetlCloseNumber="";
        private DateTime _shiftdate;
        private string _shiftnumber="";
        private string _manifestCartonNumber="";
        private string _manifestTrackingNumber="";
        private string _manifestPONumber="";
        private int _manifestWeight=0;

        //Interface
        public DestinationRouting() : this(null) { }
        public DestinationRouting(DestinationRoutingDS.DestinationRoutingTableRow destinationrouting) {
            //Constructor
            try {
                if(destinationrouting != null) {
                    if(!destinationrouting.IsclientNumberNull()) this._clientnumber = destinationrouting.clientNumber;
                    if(!destinationrouting.IsdestinationNameNull()) this._destinationname = destinationrouting.destinationName;
                    if(!destinationrouting.IsdestinationNumberNull()) this._destinationnumber = destinationrouting.destinationNumber;
                    if(!destinationrouting.IsdestinationPhoneNull()) this._destinationphone = destinationrouting.destinationPhone;
                    if(!destinationrouting.IsdestinationAddressLine1Null()) this._destinationaddressline1 = destinationrouting.destinationAddressLine1;
                    if(!destinationrouting.IsdestinationAddressLine2Null()) this._destinationaddressline2 = destinationrouting.destinationAddressLine2;
                    if(!destinationrouting.IsdestinationAddressCityNull()) this._destinationaddresscity = destinationrouting.destinationAddressCity;
                    if(!destinationrouting.IsdestinationAddressStateNull()) this._destinationaddressstate = destinationrouting.destinationAddressState;
                    if(!destinationrouting.IsdestinationAddressZipNull()) this._destinationaddresszip = destinationrouting.destinationAddressZip;
                    if(!destinationrouting.IsdestinationAltRouteNull()) this._destinationaltroute = destinationrouting.destinationAltRoute;
                    if(!destinationrouting.IsdestinationRouteNull()) this._destinationroute = destinationrouting.destinationRoute;
                    if(!destinationrouting.IsdestinationUserLabelDataNull()) this._destinationuserlabeldata = destinationrouting.destinationUserLabelData;
                    if(!destinationrouting.IsdestinationStatusNull()) this._destinationstatus = destinationrouting.destinationStatus;
                    if(!destinationrouting.IsdestinationOpenDateNull()) this._destinationopendate = destinationrouting.destinationOpenDate;
                    if(!destinationrouting.IslocalLaneNull()) this._locallane = destinationrouting.localLane;
                    if(!destinationrouting.IsosURSACodeNull()) this._osursacode = destinationrouting.osURSACode;
                    if(!destinationrouting.IsOSSequenceNull()) this._ossequence = destinationrouting.OSSequence;
                    if(!destinationrouting.IszoneCodeNull()) this._zonecode = destinationrouting.zoneCode;
                    if(!destinationrouting.IsZoneTypeNull()) this._zonetype = destinationrouting.ZoneType;
                    if(!destinationrouting.IsoutboundLabelTypeNull()) this._outboundlabeltype = destinationrouting.outboundLabelType;
                    this._zonelane = destinationrouting.zoneLane;
                    this._zonelanesmallsort = destinationrouting.zoneLaneSmallSort;
                    if(!destinationrouting.IszoneTLNull()) this._zonetl = destinationrouting.zoneTL;
                    if(!destinationrouting.IsTLDateNull()) this._zonetlDate = destinationrouting.TLDate;
                    if(!destinationrouting.IsCloseNumberNull()) this._zonetlCloseNumber = destinationrouting.CloseNumber;
                    if(!destinationrouting.IsShiftDateNull()) this._shiftdate = destinationrouting.ShiftDate;
                    this._shiftnumber = destinationrouting.ShiftNumber;
                    if(!destinationrouting.IsManifestCartonNumberNull()) this._manifestCartonNumber = destinationrouting.ManifestCartonNumber.Trim();
                    if(!destinationrouting.IsManifestTrackingNumberNull()) this._manifestTrackingNumber = destinationrouting.ManifestTrackingNumber.Trim();
                    if(!destinationrouting.IsManifestPONumberNull()) this._manifestPONumber = destinationrouting.ManifestPONumber.Trim();
                    if(!destinationrouting.IsManifestWeightNull()) this._manifestWeight = destinationrouting.ManifestWeight;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Destination Routing instance.",ex); }
        }
        #region Accessors\Modifiers: [Members], ToDataSet()
        public string ClientNumber { get { return this._clientnumber; } }
        public string DestinationName { get { return this._destinationname; } }
        public int DestinationNumber { get { return this._destinationnumber; } }
        public string DestinationPhone { get { return this._destinationphone; } }
        public string DestinationAddressLine1 { get { return this._destinationaddressline1; } }
        public string DestinationAddressLine2 { get { return this._destinationaddressline2; } }
        public string DestinationAddressCity { get { return this._destinationaddresscity; } }
        public string DestinationAddressState { get { return this._destinationaddressstate; } }
        public string DestinationAddressZip { get { return this._destinationaddresszip; } }
        public string DestinationAltRoute { get { return this._destinationaltroute; } }
        public string DestinationRoute { get { return this._destinationroute; } }
        public string DestinationUserLabelData { get { return this._destinationuserlabeldata; } }
        public string DestinationStatus { get { return this._destinationstatus; } }
        public DateTime DestinationOpenDate { get { return this._destinationopendate; } }
        public string LocalLane { get { return this._locallane; } }
        public string OSURSACode { get { return this._osursacode; } }
        public int OSSequence { get { return this._ossequence; } }
        public string ZoneCode { get { return this._zonecode; } }
        public string ZoneType { get { return this._zonetype; } }
        public string OutboundLabelType { get { return this._outboundlabeltype; } }
        public string ZoneLane { get { return this._zonelane; } }
        public string ZoneLaneSmallSort { get { return this._zonelanesmallsort; } }
        public string ZoneTL { get { return this._zonetl; } }
        public DateTime ZoneTLDate { get { return this._zonetlDate; } }
        public string ZoneTLCloseNumber { get { return this._zonetlCloseNumber; } }
        public DateTime ShiftDate { get { return this._shiftdate; } }
        public string ShiftNumber { get { return this._shiftnumber; } }
        public string ManifestCartonNumber { get { return this._manifestCartonNumber; } }
        public string ManifestTrackingNumber { get { return this._manifestTrackingNumber; } }
        public string ManifestPONumber { get { return this._manifestPONumber; } }
        public int ManifestWeight { get { return this._manifestWeight; } }

        public DataSet ToDataSet() {
            //Return a dataset containing values for this object
            DestinationRoutingDS destinationRoutingDS=null;
            try {
                destinationRoutingDS = new DestinationRoutingDS();
                DestinationRoutingDS.DestinationRoutingTableRow destinationrouting = destinationRoutingDS.DestinationRoutingTable.NewDestinationRoutingTableRow();
                if(this._clientnumber.Length > 0) destinationrouting.clientNumber = this._clientnumber;
                if(this._destinationname.Length > 0) destinationrouting.destinationName = this._destinationname;
                if(this._destinationnumber > 0) destinationrouting.destinationNumber = this._destinationnumber;
                if(this._destinationphone.Length > 0) destinationrouting.destinationPhone = this._destinationphone;
                if(this._destinationaddressline1.Length > 0) destinationrouting.destinationAddressLine1 = this._destinationaddressline1;
                if(this._destinationaddressline2.Length > 0) destinationrouting.destinationAddressLine2 = this._destinationaddressline2;
                if(this._destinationaddresscity.Length > 0) destinationrouting.destinationAddressCity = this._destinationaddresscity;
                if(this._destinationaddressstate.Length > 0) destinationrouting.destinationAddressState = this._destinationaddressstate;
                if(this._destinationaddresszip.Length > 0) destinationrouting.destinationAddressZip = this._destinationaddresszip;
                if(this._destinationaltroute.Length > 0) destinationrouting.destinationAltRoute = this._destinationaltroute;
                if(this._destinationroute.Length > 0) destinationrouting.destinationRoute = this._destinationroute;
                if(this._destinationuserlabeldata.Length > 0) destinationrouting.destinationUserLabelData = this._destinationuserlabeldata;
                if(this._destinationstatus.Length > 0) destinationrouting.destinationStatus = this._destinationstatus;
                if(this._destinationopendate > DateTime.MinValue) destinationrouting.destinationOpenDate = this._destinationopendate;
                if(this._locallane.Length > 0) destinationrouting.localLane = this._locallane;
                if(this._osursacode.Length > 0) destinationrouting.osURSACode = this._osursacode;
                if(this._ossequence > 0) destinationrouting.OSSequence = this._ossequence;
                if(this._zonecode.Length > 0) destinationrouting.zoneCode = this._zonecode;
                if(this._zonetype.Length > 0) destinationrouting.zoneCode = this._zonetype;
                if(this._outboundlabeltype.Length > 0) destinationrouting.outboundLabelType = this._outboundlabeltype;
                destinationrouting.zoneLane = this._zonelane;
                destinationrouting.zoneLaneSmallSort = this._zonelanesmallsort;
                if(this._zonetl.Length > 0) destinationrouting.zoneTL = this._zonetl;
                if(this._zonetlDate.CompareTo(DateTime.MinValue) > 0) destinationrouting.TLDate = this._zonetlDate;
                if(this._zonetlCloseNumber.Length > 0) destinationrouting.CloseNumber = this._zonetlCloseNumber;

                if(this._clientnumber.Length > 0) destinationrouting.ShiftDate = this._shiftdate;
                destinationrouting.ShiftNumber = this._shiftnumber;
                destinationRoutingDS.DestinationRoutingTable.AddDestinationRoutingTableRow(destinationrouting);
            }
            catch(Exception) { }
            return destinationRoutingDS;
        }
        #endregion
    }
}