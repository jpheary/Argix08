using System;
using System.Data;
using System.Diagnostics;
using System.Text;
using Argix.Data;
using Argix.Enterprise;
using Tsort.Labels;

namespace Argix.Freight {
	//
	public class Carton: LabelMaker {
		//Members
		public static string LanePrefix="";
		
		private string mScanData="";
		private BearwareTrip mInboundFreight=null;
		private Client mClient=null;
		private Store mStore=null;
		private ClientVendor mClientVendor=null;
        private Zone mZone=null;
        private Workstation mWorkstation=null;

		//Interface
		public Carton(): this("",null,null,null,null,null,null){ }
		public Carton(string scan): this(scan,null,null,null,null,null,null){ }
		public Carton(string scan, BearwareTrip shipment, Client client, Store store, ClientVendor shipper, Zone zone, Workstation workstation) {
			//Constructor
			try {
				this.mScanData = scan;
				this.mInboundFreight = shipment;
				this.mClient = client;
				this.mStore = store;
				this.mClientVendor = shipper;
				this.mZone = zone;
				this.mWorkstation = workstation;
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error creating new Carton instance.", ex); }
		}
        public override string Name { get { return "Indirect Sort Label Maker"; } }
        protected override void setLabelTokenValues() { 
			//Override token values with this objects data
			try {
                ArgixTrace.WriteLine(new TraceMessage("Setting token values...",App.Product,LogLevel.Debug));
				if(this.isTsort) {
					//From store or client vendor
					base.mTokens[TokenLibrary.SAN] = (this.mStore != null) ? this.mStore.SANNumber : "";
					base.mTokens[TokenLibrary.LOCALLANE] = (this.mStore != null) ? this.mStore.LocalLane.TrimEnd().PadLeft(2, '0') : "";
				}
				else {
					//from store or client vendor
					base.mTokens[TokenLibrary.LOCALLANE] = this.mClientVendor.LOCAL_LANE.TrimEnd().PadLeft(2, '0');
				}
				base.mTokens[TokenLibrary.FREIGHTTYPE] = this.FreightType;
				base.mTokens[TokenLibrary.CURRENTDATE] = DateTime.Today.ToShortDateString();
				base.mTokens[TokenLibrary.CURRENTTIME] = DateTime.Now.ToString("HH:mm:ss");
				base.mTokens[TokenLibrary.CURRENTYEAR] = DateTime.Today.Year.ToString();
				base.mTokens[TokenLibrary.FREIGHTPICKUPDATE] = ""; 
				base.mTokens[TokenLibrary.FREIGHTPICKUPINFO] = ""; 
				base.mTokens[TokenLibrary.FREIGHTPICKUPNUMBER] = ""; 
				base.mTokens[TokenLibrary.FREIGHTPICKUPNUMBERSTRING] = ""; 
				base.mTokens[TokenLibrary.FREIGHTVENDORKEY] = ""; 
				base.mTokens[TokenLibrary.LANEPREFIX] = Carton.LanePrefix;
				base.mTokens[TokenLibrary.CARTONNUMBER] = ""; 
				base.mTokens[TokenLibrary.CARTONNUMBERORPONUMBER] = ""; 
				base.mTokens[TokenLibrary.SORTEDITEMLABELNUMBER] = this.LabelSeqNumber.TrimEnd(); 
				base.mTokens[TokenLibrary.SORTEDITEMWEIGHTSTRING] = ""; 
				base.mTokens[TokenLibrary.ITEMDAMAGECODE] = ""; 
				base.mTokens[TokenLibrary.ITEMTYPE] = ""; 
				base.mTokens[TokenLibrary.PONUMBER] = ""; 
				base.mTokens[TokenLibrary.RETURNNUMBER] = ""; 
				base.mTokens[TokenLibrary.CLIENTNUMBER] = this.mClient.Number; 
				base.mTokens[TokenLibrary.CLIENTNAME] = this.mClient.Name; 
				base.mTokens[TokenLibrary.CLIENTABBREVIATION] = this.mClient.ABBREVIATION; 
				base.mTokens[TokenLibrary.CLIENTDIVISIONNUMBER] = this.mClient.Division; 
				base.mTokens[TokenLibrary.CLIENTADDRESSLINE1] = this.mClient.AddressLine1; 
				base.mTokens[TokenLibrary.CLIENTADDRESSLINE2] = this.mClient.AddressLine2; 
				base.mTokens[TokenLibrary.CLIENTADDRESSCITY] = this.mClient.City; 
				base.mTokens[TokenLibrary.CLIENTADDRESSSTATE] = this.mClient.State; 
				base.mTokens[TokenLibrary.CLIENTADDRESSZIP] = this.mClient.ZIP; 
				base.mTokens[TokenLibrary.CLIENTADDRESSCOUNTRYCODE] = ""; 
				if(this.mStore != null) {
					base.mTokens[TokenLibrary.STORENUMBER] = this.mStore.Number.ToString().PadLeft(5, '0'); 
					base.mTokens[TokenLibrary.STORENAME] = this.mStore.Name.TrimEnd(); 
					base.mTokens[TokenLibrary.STOREADDRESSLINE1] = this.mStore.AddressLine1.TrimEnd(); 
					base.mTokens[TokenLibrary.STOREADDRESSLINE2] = this.mStore.AddressLine2.TrimEnd(); 
					base.mTokens[TokenLibrary.STOREADDRESSCITY] = this.mStore.City.TrimEnd(); 
					base.mTokens[TokenLibrary.STOREADDRESSSTATE] = this.mStore.State.TrimEnd(); 
					base.mTokens[TokenLibrary.STOREADDRESSZIP] = this.mStore.Zip.TrimEnd(); 
					base.mTokens[TokenLibrary.STOREZIP] = this.mStore.Zip.TrimEnd(); 
					base.mTokens[TokenLibrary.STOREPHONE] = this.mStore.Phone.TrimEnd(); 
					base.mTokens[TokenLibrary.STOREROUTE] = this.mStore.Route.TrimEnd();

                    string routeLane = this.mStore.Route.TrimEnd().PadRight(5).Substring(0,2);
					base.mTokens[TokenLibrary.LOCALROUTELANE] = (char.IsNumber(routeLane, 0) && char.IsNumber(routeLane, 1)) ? routeLane : "00";

                    base.mTokens[TokenLibrary.STOREROUTEFIRSTCHARACTER] = this.mStore.Route.TrimEnd().PadRight(5).Substring(0,1);
                    base.mTokens[TokenLibrary.STOREROUTEFIRSTTWO] = this.mStore.Route.TrimEnd().PadRight(5).Substring(0,2);
                    base.mTokens[TokenLibrary.STOREROUTELASTFOUR] = this.mStore.Route.TrimEnd().PadRight(5).Substring(1,4);
                    base.mTokens[TokenLibrary.STOREROUTELASTTHREE] = this.mStore.Route.TrimEnd().PadRight(5).Substring(2,3); 
					base.mTokens[TokenLibrary.STOREALTROUTE] = this.mStore.AltRoute; 
					base.mTokens[TokenLibrary.STOREUSERLABELDATA] = this.mStore.LabelUserData.TrimEnd(); 
				}
				if(this.mClientVendor != null) {
					base.mTokens[TokenLibrary.VENDORNUMBER] = this.mClientVendor.VENDOR_NUMBER.TrimEnd(); 
					base.mTokens[TokenLibrary.VENDORNAME] = this.mClientVendor.NAME.TrimEnd(); 
					base.mTokens[TokenLibrary.VENDORADDRESSLINE1] = this.mClientVendor.ADDRESS_LINE1.TrimEnd(); 
					base.mTokens[TokenLibrary.VENDORADDRESSLINE2] = this.mClientVendor.ADDRESS_LINE2.TrimEnd(); 
					base.mTokens[TokenLibrary.VENDORADDRESSCITY] = this.mClientVendor.CITY.TrimEnd(); 
					base.mTokens[TokenLibrary.VENDORADDRESSSTATE] = this.mClientVendor.STATE.TrimEnd(); 
					base.mTokens[TokenLibrary.VENDORADDRESSZIP] = this.mClientVendor.ZIP.TrimEnd(); 
					base.mTokens[TokenLibrary.VENDORUSERDATA] = this.mClientVendor.USERDATA.TrimEnd(); 
				
					string routeLane = this.mClientVendor.ROUTE.TrimEnd().PadRight(5).Substring(0,2);
					base.mTokens[TokenLibrary.LOCALROUTELANE] = (char.IsNumber(routeLane, 0) && char.IsNumber(routeLane, 1)) ? routeLane : "00"; 
				}
				base.mTokens[TokenLibrary.ZONECODE] = this.mZone.Code.TrimEnd(); 
				base.mTokens[TokenLibrary.ZONELABELTYPE] = this.mZone.Type.TrimEnd(); 
				base.mTokens[TokenLibrary.ZONELANE] = this.mZone.Lane.TrimEnd(); 
				base.mTokens[TokenLibrary.ZONELANESMALLSORT] = this.mZone.SmallSortLane.TrimEnd(); 
				base.mTokens[TokenLibrary.ZONEOUTBOUNDTRAILERLOADNUMBER] = this.mZone.TrailerLoadNumber.TrimEnd(); 
				base.mTokens[TokenLibrary.ZONEOUTBOUNDTRAILERLOADNUMBERDIGITSONLY] = ""; 
				base.mTokens[TokenLibrary.WORKSTATIONID] = this.mWorkstation.WorkStationID; 
				base.mTokens[TokenLibrary.WORKSTATIONNAME] = this.mWorkstation.Name; 
				base.mTokens[TokenLibrary.WORKSTATIONNUMBER] = this.mWorkstation.Number; 
				base.mTokens[TokenLibrary.WORKSTATIONNUMBER2] = this.mWorkstation.Number; 
				base.mTokens[TokenLibrary.WORKSTATIONDESCRIPTION] = this.mWorkstation.Description; 
			}
			catch(Exception ex) { throw ex; }
		}
        #region Accessors\Modifiers: [Members...]
        public string ScanData { get { return this.mScanData; } }
		public string ScanDataFirst23 { get { return this.mScanData.Length > 23 ? this.mScanData.Substring(0,23) : this.mScanData; } }
		public string ClientNumber { get { return this.mScanData.Substring(0, 3); } }
		public string FreightType { get { return this.mScanData.Substring(3, 2); } }
		public string StoreNumber { get { return this.mScanData.Substring(5, 5); } }
		public string VendorNumber { get { return this.mScanData.Substring(5, 5); } }
		public string LabelSeqNumber { get { return this.mScanData.Substring(10, 13); } }
		public bool isReturns { get { return FreightType == "88"; } }
		public bool isTsort { get { return FreightType == "44"; } }
		public bool isValid { get { return isTsort || isReturns; } }
		public BearwareTrip InboundFreight { get { return this.mInboundFreight; } set { this.mInboundFreight = value; } }
		public Client Client { get { return this.mClient; } set { this.mClient = value; } }
		public Store Store { get { return this.mStore; } set { this.mStore = value; } }
		public ClientVendor ClientVendor { get { return this.mClientVendor; } set { this.mClientVendor = value; } }
		public Zone Zone { get { return this.mZone; } set { this.mZone = value; } }
        public Workstation Workstation { get { return this.mWorkstation; } set { this.mWorkstation = value; } }
		#endregion
	}
}
