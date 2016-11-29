//	File:	sorteditem.cs
//	Author:	J. Heary
//	Date:	02/07/05
//	Desc:	Represents a Tsort sorted item.
//	Rev:	12/03/07 (jph)- modified createLabelSeqNumber() to use 5 position time
//						    and 3 position station number.
//	    	01/07/08 (jph)- added support for new tokens TIMENOW, TLDATE, TLCLOSENUMBER.
//			02/29/08 (jph)- CreateLabelSequenceNumber() changed to internal; logic 
//							revised to determine sleep duration instead of 5msec sleep.
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Text;
using Argix;
using Argix.Data;
using Tsort.Enterprise;
using Tsort.Freight;
using Tsort.Labels;


namespace Tsort.Sort {
    //
    public class SortedItem:LabelMaker {
        //Members
        public static int WeightMax=100;
        public static string LanePrefix="88";
        public static LabelTemplate ErrorLabelTemplate = null;
        private static DateTime LastSortedItem=DateTime.Now;

        private string mLabelSeqNumber="";		//Unique identifier for this item
        private string mSANNUmber="";			//
        private string mCartonNumber="";		//Shipper carton number
        private string mPONumber="";			//Shipper PO number
        private string mReturnNumber="";		//
        private string mTrackingNumber="";		//
        private string mItemType="C";			//Type carton
        private int mWeight=0;
        private int mItemCube=0;
        private int mElapsedSeconds=0;
        private int mDownSeconds=0;
        private string mDamageCode="";
        private string mLabelFormat="";			//Outbound label format for this item
        private InboundFreight mFreight=null;
        private Workstation mSortStation=null;
        private DestinationRouting mDestinationRouting=null;
        private SpecialAgent mSpecialAgent=null;
        private LabelTemplate mLabelTemplate=null;
        private Exception mException=null;
        private SortProfile mSortProfile=null;
        private InboundLabel mInboundLabel=null;

        //Constants
        private const int CUBE_FACTOR = 1728;

        //Events
        //Interface
        internal static string CreateLabelSequenceNumber(string stationNumber) {
            //Generate a sorted item number for this object; format: yMMddssssswww
            string labelSeqNumber = "";
            try {
                int i=0,wait=0;
                while(DateTime.Now.ToString("hhmmss").CompareTo(LastSortedItem.ToString("hhmmss")) == 0) {
                    //Determine wait time (msec) until the next second of the current time
                    wait = 1000 - int.Parse(DateTime.Now.ToString("fff"));
                    System.Threading.Thread.Sleep(wait);
                    i++;
                }
                DateTime dt = LastSortedItem = DateTime.Now;
                labelSeqNumber = dt.ToString("yyyy").Substring(3,1) + dt.ToString("MM") +  dt.ToString("dd") + getElapsedSeconds(dt) + stationNumber.Trim().PadLeft(3,'0');
                ArgixTrace.WriteLine(new TraceMessage("Created label sequence number (" + i.ToString() + "x; " + wait.ToString() + "msec)...",AppLib.EVENTLOGNAME,LogLevel.Debug,"SortedItem"));
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating sorted item number.",ex); }
            return labelSeqNumber;
        }
        private static string getElapsedSeconds(DateTime now) {
            //Get the number of elasped seconds since midnight; format: 00000
            int sec = ((3600 * now.Hour) + (60 * now.Minute) + now.Second);
            return sec.ToString("00000");
        }
        public SortedItem(Workstation station) {
            //Constructor
            try {
                ArgixTrace.WriteLine(new TraceMessage("New sorted item...",AppLib.EVENTLOGNAME,LogLevel.Debug,"SortedItem"));
                this.mSortStation = station;
                this.mLabelSeqNumber = CreateLabelSequenceNumber(this.mSortStation.Number);
            }
            catch(Exception ex) { this.mException = ex; }
        }
        public void ThrowException() {
            //Throw an exception if this item has one from processing
            if(this.mException != null) throw mException;
        }
        #region LabelMaker Overrides setTokenValues(), Name
        protected override void setTokenValues() {
            //Override token values with this objects data
            try {
                ArgixTrace.WriteLine(new TraceMessage("Setting token values...",AppLib.EVENTLOGNAME,LogLevel.Debug,"SortedItem"));
                base.mTokens[TokenLibrary.SAN] = TokenLibrary.SAN.Trim();
                base.mTokens[TokenLibrary.CURRENTDATE] = DateTime.Today.ToShortDateString();
                base.mTokens[TokenLibrary.CURRENTTIME] = DateTime.Now.ToString("HH:mm:ss");
                base.mTokens[TokenLibrary.TIMENOW] = DateTime.Now.ToString("HHmmss");
                base.mTokens[TokenLibrary.CURRENTYEAR] = DateTime.Today.Year.ToString();
                base.mTokens[TokenLibrary.LANEPREFIX] = SortedItem.LanePrefix.Trim();
                base.mTokens[TokenLibrary.CARTONNUMBER] = this.mCartonNumber;
                base.mTokens[TokenLibrary.CARTONNUMBERORPONUMBER] = "";
                base.mTokens[TokenLibrary.SORTEDITEMLABELNUMBER] = this.mLabelSeqNumber.Trim();
                base.mTokens[TokenLibrary.SORTEDITEMWEIGHTSTRING] = this.mWeight;
                base.mTokens[TokenLibrary.ITEMDAMAGECODE] = this.mDamageCode.Trim();
                base.mTokens[TokenLibrary.ITEMTYPE] = this.mItemType.Trim();
                base.mTokens[TokenLibrary.PONUMBER] = this.mPONumber.Trim();
                base.mTokens[TokenLibrary.RETURNNUMBER] = "";
                base.mTokens[TokenLibrary.WORKSTATIONID] = this.mSortStation.WorkStationID.Trim();
                base.mTokens[TokenLibrary.WORKSTATIONNAME] = this.mSortStation.Name.Trim();
                base.mTokens[TokenLibrary.WORKSTATIONNUMBER] = this.mSortStation.Number.Trim();
                base.mTokens[TokenLibrary.WORKSTATIONDESCRIPTION] = this.mSortStation.Description.Trim();

                if(this.mFreight != null) {
                    base.mTokens[TokenLibrary.FREIGHTTYPE] = (this.mFreight.IsReturns? "88" : "44");
                    base.mTokens[TokenLibrary.FREIGHTPICKUPDATE] = this.mFreight.PickupDate;
                    base.mTokens[TokenLibrary.FREIGHTPICKUPINFO] = "";
                    base.mTokens[TokenLibrary.FREIGHTPICKUPNUMBER] = this.mFreight.PickupNumber;
                    base.mTokens[TokenLibrary.FREIGHTPICKUPNUMBERSTRING] = this.mFreight.PickupNumber;
                    base.mTokens[TokenLibrary.FREIGHTVENDORKEY] = this.mFreight.VendorKey;

                    base.mTokens[TokenLibrary.CLIENTNUMBER] = this.Client.Number;
                    base.mTokens[TokenLibrary.CLIENTNAME] = this.Client.Name.Trim();
                    base.mTokens[TokenLibrary.CLIENTABBREVIATION] = this.Client.ABBREVIATION;
                    base.mTokens[TokenLibrary.CLIENTDIVISIONNUMBER] = this.Client.Division;
                    base.mTokens[TokenLibrary.CLIENTADDRESSLINE1] = this.Client.AddressLine1.Trim();
                    base.mTokens[TokenLibrary.CLIENTADDRESSLINE2] = this.Client.AddressLine2.Trim();
                    base.mTokens[TokenLibrary.CLIENTADDRESSCITY] = this.Client.City.Trim();
                    base.mTokens[TokenLibrary.CLIENTADDRESSSTATE] = this.Client.State.Trim();
                    base.mTokens[TokenLibrary.CLIENTADDRESSZIP] = this.Client.ZIP.Trim();
                    base.mTokens[TokenLibrary.CLIENTADDRESSCOUNTRYCODE] = "";

                    base.mTokens[TokenLibrary.SHIPPERNUMBER] = this.mFreight.Shipper.NUMBER;
                    base.mTokens[TokenLibrary.VENDORNUMBER] = this.mFreight.Shipper.NUMBER;
                    base.mTokens[TokenLibrary.VENDORNAME] = this.mFreight.Shipper.NAME.Trim();
                    base.mTokens[TokenLibrary.VENDORADDRESSLINE1] = this.mFreight.Shipper.ADDRESS_LINE1.Trim();
                    base.mTokens[TokenLibrary.VENDORADDRESSLINE2] = this.mFreight.Shipper.ADDRESS_LINE2.Trim();
                    base.mTokens[TokenLibrary.VENDORADDRESSCITY] = this.mFreight.Shipper.CITY.Trim();
                    base.mTokens[TokenLibrary.VENDORADDRESSSTATE] = this.mFreight.Shipper.STATE.Trim();
                    base.mTokens[TokenLibrary.VENDORADDRESSZIP] = this.mFreight.Shipper.ZIP.Trim();
                    base.mTokens[TokenLibrary.VENDORUSERDATA] = this.mFreight.Shipper.USERDATA.Trim();
                }
                else {
                    base.mTokens[TokenLibrary.FREIGHTTYPE] = "00";
                    base.mTokens[TokenLibrary.FREIGHTPICKUPDATE] = "N/A";
                    base.mTokens[TokenLibrary.FREIGHTPICKUPNUMBER] = "0";

                    base.mTokens[TokenLibrary.CLIENTNUMBER] = "000";
                    base.mTokens[TokenLibrary.CLIENTDIVISIONNUMBER] = "00";
                }

                if(this.mDestinationRouting != null) {
                    base.mTokens[TokenLibrary.LOCALLANE] = this.mDestinationRouting.LocalLane;
                    base.mTokens[TokenLibrary.DESTINATIONNUMBER] = this.mDestinationRouting.DestinationNumber.ToString().PadLeft(5,'0');
                    base.mTokens[TokenLibrary.DESTINATIONNAME] = this.mDestinationRouting.DestinationName.Trim();
                    base.mTokens[TokenLibrary.DESTINATIONADDRESSLINE1] = this.mDestinationRouting.DestinationAddressLine1.Trim();
                    base.mTokens[TokenLibrary.DESTINATIONADDRESSLINE2] = this.mDestinationRouting.DestinationAddressLine2.Trim();
                    base.mTokens[TokenLibrary.DESTINATIONADDRESSCITY] = this.mDestinationRouting.DestinationAddressCity.Trim();
                    base.mTokens[TokenLibrary.DESTINATIONADDRESSSTATE] = this.mDestinationRouting.DestinationAddressState.Trim();
                    base.mTokens[TokenLibrary.DESTINATIONADDRESSZIP] = this.mDestinationRouting.DestinationAddressZip.Trim();
                    base.mTokens[TokenLibrary.DESTINATIONZIP] = this.mDestinationRouting.DestinationAddressZip.Trim();
                    base.mTokens[TokenLibrary.DESTINATIONADDRESSCOUNTRYCODE] = "US";
                    base.mTokens[TokenLibrary.DESTINATIONPHONE] = this.mDestinationRouting.DestinationPhone.Trim();
                    base.mTokens[TokenLibrary.DESTINATIONROUTE] = this.mDestinationRouting.DestinationRoute;
                    base.mTokens[TokenLibrary.DESTINATIONALTROUTE] = this.mDestinationRouting.DestinationAltRoute;

                    string route = this.mDestinationRouting.DestinationRoute.TrimEnd().PadRight(5);
                    string routeLane = route.Substring(0,2);
                    base.mTokens[TokenLibrary.LOCALROUTELANE] = (char.IsNumber(routeLane,0) && char.IsNumber(routeLane,1)) ? routeLane : "00";

                    base.mTokens[TokenLibrary.DESTINATIONROUTEFIRSTCHARACTER] = route.Substring(0,1);
                    base.mTokens[TokenLibrary.DESTINATIONROUTEFIRSTTWO] = route.Substring(0,2);
                    base.mTokens[TokenLibrary.DESTINATIONROUTELASTFOUR] = route.Substring(1,4);
                    base.mTokens[TokenLibrary.DESTINATIONROUTELASTTHREE] = route.Substring(2,3);
                    base.mTokens[TokenLibrary.DESTINATIONUSERLABELDATA] = this.mDestinationRouting.DestinationUserLabelData.Trim();

                    base.mTokens[TokenLibrary.ZONECODE] = this.mDestinationRouting.ZoneCode;
                    base.mTokens[TokenLibrary.ZONELABELTYPE] = this.mDestinationRouting.ZoneType;
                    base.mTokens[TokenLibrary.ZONELANE] = this.mDestinationRouting.ZoneLane;
                    base.mTokens[TokenLibrary.ZONELANESMALLSORT] = this.mDestinationRouting.ZoneLaneSmallSort;
                    base.mTokens[TokenLibrary.ZONEOUTBOUNDTRAILERLOADNUMBER] = this.mDestinationRouting.ZoneTL;
                    base.mTokens[TokenLibrary.ZONEOUTBOUNDTRAILERLOADNUMBERDIGITSONLY] = "";

                    base.mTokens[TokenLibrary.TLDATE] = this.mDestinationRouting.ZoneTLDate.ToString("MMddyy");
                    base.mTokens[TokenLibrary.TLCLOSENUMBER] = this.mDestinationRouting.ZoneTLCloseNumber.PadLeft(3,'0').Substring(1,2);

                    // special agent
                    if(this.mSpecialAgent != null) {
                        base.mTokens[TokenLibrary.OSSERVICETITLE] = this.mSpecialAgent.OSTitle;
                        base.mTokens[TokenLibrary.OSBARCODE1DATAHUMANFORMAT] = this.mSpecialAgent.OSBarcode1DataHumanFormat(this.mTrackingNumber);
                        base.mTokens[TokenLibrary.OSDATAIDENTIFIER] = this.mSpecialAgent.OSDataIdentifier;
                        base.mTokens[TokenLibrary.OSBARCODE1DATA] = this.mSpecialAgent.OSBarcode1Data(this.mTrackingNumber);
                        base.mTokens[TokenLibrary.OSROUTINGCODE] = this.mSpecialAgent.OSRoutingCode(this.mDestinationRouting.OSURSACode);
                        base.mTokens[TokenLibrary.OSSERVICEINDICATOR] = this.mSpecialAgent.OSServiceIndicator;
                        base.mTokens[TokenLibrary.OSTRACKINGNUMBER10] = this.mSpecialAgent.OSTrackingNumber10(this.mTrackingNumber);
                        base.mTokens[TokenLibrary.OSSENDERACCOUNTNUMBER] = this.mSpecialAgent.OSSenderAccountNumber;
                        base.mTokens[TokenLibrary.OSJULIANDATE] = this.mSpecialAgent.OSJulianDate;
                        base.mTokens[TokenLibrary.OSSERVICEICON] = this.mSpecialAgent.OSServiceIcon;
                    }
                }
                else {
                    base.mTokens[TokenLibrary.DESTINATIONNUMBER] = "00000";
                }

                if(this.IsError()) {
                    base.mTokens[TokenLibrary.LOCALLANE] = "00";
                    base.mTokens[TokenLibrary.ZONELANE] = "00";
                    base.mTokens[TokenLibrary.ZONELANESMALLSORT] = "00";

                    string msg = this.mException.Message;
                    if(this.mException.InnerException != null) {
                        msg += " -->" + this.mException.InnerException.Message;
                        if(this.mException.InnerException.InnerException != null) {
                            msg += " -->" + this.mException.InnerException.InnerException.Message;
                        }
                    }
                    base.mTokens[TokenLibrary.MESSAGETEXT] = msg;
                    if(msg.Length > 0)
                        base.mTokens["<messageText_Line1>"] = (msg.Length < 50 ? msg.Substring(0).PadRight(50,' ') : msg.Substring(0,50).PadRight(50,' '));
                    else
                        base.mTokens["<messageText_Line1>"] = "";
                    if(msg.Length > 50)
                        base.mTokens["<messageText_Line2>"] = (msg.Length < 100 ?  msg.Substring(50).PadRight(50,' ') : msg.Substring(50,50).PadRight(50,' '));
                    else
                        base.mTokens["<messageText_Line2>"] = "";
                    if(msg.Length > 100)
                        base.mTokens["<messageText_Line3>"] = (msg.Length < 150 ? msg.Substring(100).PadRight(50,' ') : msg.Substring(100,50).PadRight(50,' '));
                    else
                        base.mTokens["<messageText_Line3>"] = "";
                    if(msg.Length > 150)
                        base.mTokens["<messageText_Line4>"] = (msg.Length < 200 ? msg.Substring(150).PadRight(50,' ') : msg.Substring(150,50).PadRight(50,' '));
                    else
                        base.mTokens["<messageText_Line4>"] = "";
                    if(msg.Length > 200)
                        base.mTokens["<messageText_Line5>"] = (msg.Length < 250 ? msg.Substring(200).PadRight(50,' ') : msg.Substring(200,50).PadRight(50,' '));
                    else
                        base.mTokens["<messageText_Line5>"] = "";
                }
            }
            catch(Exception) { }
        }
        public override string Name { get { return "Sorted Item Label Maker"; } }
        #endregion
        #region Members: LabelSeqNumber, LabelNumber, CartonNumber, PONumber, SANNUmber, ReturnNumber, TrackingNumber, ItemType, Weight, Cube, ElapsedSeconds, DownSeconds, DamageCode, LabelFormat, SortException, ToDataSet()
        public string LabelSeqNumber { get { return this.mLabelSeqNumber; } }
        public string LabelNumber {
            get {
                string prefix="";
                prefix += this.mFreight != null ? this.mFreight.Client.Number.Trim().PadLeft(3,'0') + (this.mFreight.IsReturns? "88" : "44"): "00099";
                prefix += mDestinationRouting != null ? mDestinationRouting.DestinationNumber.ToString().PadLeft(5,'0') : "00000";
                return ((this.mSpecialAgent != null && this.mSpecialAgent.Type == "UPSSpecialAgent") ? "" : prefix) + this.mLabelSeqNumber + "0";
            }
        }
        public string CartonNumber { get { return this.mCartonNumber; } set { this.mCartonNumber = (value==null?" ":value); } }
        public string PONumber { get { return this.mPONumber; } set { this.mPONumber = (value==null?" ":value); } }
        public string SANNUmber { get { return this.mSANNUmber; } set { this.mSANNUmber = (value==null?" ":value); } }
        public string ReturnNumber { get { return this.mReturnNumber; } }
        public string TrackingNumber { get { return this.mTrackingNumber; } set { this.mTrackingNumber = value; } }
        public string ItemType { get { return this.mItemType; } set { this.mItemType = value; } }
        public int Weight {
            get { return this.mWeight; }
            set {
                this.mWeight = value;
                if(this.mFreight != null)
                    this.mItemCube = (int)(this.mFreight.CubeRatio * CUBE_FACTOR * this.mWeight);
            }
        }
        public int Cube { get { return this.mItemCube; } }
        public int ElapsedSeconds { get { return this.mElapsedSeconds; } set { this.mElapsedSeconds = value; } }
        public int DownSeconds { get { return this.mDownSeconds; } set { this.mDownSeconds = value; } }
        public string DamageCode { get { return this.mDamageCode; } set { this.mDamageCode = value; } }
        public string LabelFormat { get { return this.mLabelFormat; } }
        public Exception SortException { get { return this.mException; } set { this.mException = value; } }
        public DataSet ToDataSet() {
            DataSet ds=null;
            try {
                ds = new DataSet();
                ds.Merge(this.mFreight.ToDataSet());
                ds.Merge(this.mSortStation.ToDataSet());
                ds.Merge(this.mDestinationRouting.ToDataSet());
                ds.Merge(this.mSpecialAgent.ToDataSet());
                ds.Merge(this.mLabelTemplate.ToDataSet());
            }
            catch(Exception) { }
            return ds;
        }
        #endregion
        #region Internal Members: Freight, Client, SortStation, SortProfile, SpecialAgent, DestinationRouting, LabelTemplate, InboundLabel
        internal InboundFreight Freight { get { return this.mFreight; } set { this.mFreight = value; } }
        internal Client Client { get { return this.Freight.Client; } }
        internal Workstation SortStation { get { return this.mSortStation; } set { this.mSortStation = value; } }
        internal SortProfile SortProfile { get { return this.mSortProfile; } set { this.mSortProfile = value; } }
        internal SpecialAgent SpecialAgent { get { return this.mSpecialAgent; } set { this.mSpecialAgent = value; } }
        internal DestinationRouting DestinationRouting { get { return this.mDestinationRouting; } set { this.mDestinationRouting = value; } }
        internal LabelTemplate LabelTemplate { get { return this.mLabelTemplate; } set { this.mLabelTemplate = value; } }
        internal InboundLabel InboundLabel { get { return this.mInboundLabel; } set { this.mInboundLabel = value; } }
        #endregion
        #region Internal Services: ApplyOutboundLabel(), IsDuplicateCarton(), IsError(), ThrowException()
        internal void ApplyOutboundLabel() {
            //Complete label processing (determine final label format) for this sorted item
            try {
                if(this.IsError()) this.mLabelTemplate = ErrorLabelTemplate;
                //Bypass LabelMaker FormatTemplate() method so Panda tokens remain to be replaced by PandaSvc
                //this.mLabelFormat = base.FormatTemplate(this.mLabelTemplate.Template);
                this.mLabelFormat = this.mLabelTemplate.Template;
                setTokenValues();
                foreach(System.Collections.DictionaryEntry token in base.mTokens) {
                    if(token.Key.ToString() != TokenLibrary.STATUSCODE)
                        this.mLabelFormat = this.mLabelFormat.Replace(token.Key.ToString(),token.Value.ToString());
                }
            }
            catch(Exception ex) {
                if(!this.IsError()) {
                    // sorted item is not in error - problem generating regular label
                    this.mException = new OutboundLabelException(ex);
                    this.ApplyOutboundLabel(); //recursive call
                }
                else {
                    // error generating error label
                    this.mException = new ErrorLabelException(ex);
                    this.mLabelFormat = ErrorLabelTemplate.Template; // asssign string without substituting
                }
            }
        }
        internal bool IsDuplicateCarton(SortedItem sortedItem) {
            //Check if the specified sortedItem is a duplicate of this sorted item
            return !this.IsError() && !sortedItem.IsError() &&
				this.CartonNumber == sortedItem.CartonNumber && 
				this.Client.Number == sortedItem.Client.Number && 
				this.Freight.Shipper.NUMBER == sortedItem.Freight.Shipper.NUMBER;
        }
        internal bool IsError() { return this.mException != null; }
        internal void ThrowException(ApplicationException anException) {
            this.mException = anException;
            ThrowException();
        }
        #endregion
        #region Local Services:
        #endregion
    }
}
