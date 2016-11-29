//	File:	manuallabelmaker.cs
//	Author:	J. Heary
//	Date:	08/17/05
//	Desc:	Concrete implementation of the abstract Tsort.Labels.LabelMaker class;
//			implements a label maker that requires user specified token values.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;

namespace Tsort.Labels {
	//
	public class ManualLabelMaker: LabelMaker {
		//Members

		//Interface
		public ManualLabelMaker() { }
		protected override void setLabelTokenValues() { 
			//Override token values with this objects data
		}
		public override string Name { get { return "User Specified"; } }
		
		[ Category("General"), Description("Current date.")]
		public string CurrentDate { 
			get { return base.mTokens[TokenLibrary.CURRENTDATE].ToString(); } 
			set { base.mTokens[TokenLibrary.CURRENTDATE] = value; base.OnLabelValuesChanged(this, new EventArgs()); }
		}
		[ Category("General"), Description("Current time.")]
		public string CurrentTime { 
			get { return base.mTokens[TokenLibrary.CURRENTTIME].ToString(); } 
			set { base.mTokens[TokenLibrary.CURRENTTIME] = value; base.OnLabelValuesChanged(this, new EventArgs()); }
		}
		[ Category("General"), Description("Current year.")]
		public string CurrentYear { 
			get { return base.mTokens[TokenLibrary.CURRENTYEAR].ToString(); } 
			set { base.mTokens[TokenLibrary.CURRENTYEAR] = value; base.OnLabelValuesChanged(this, new EventArgs()); }
		}
        [Category("General"),Description("Freight type.")]
        public string FreightType {
            get { return base.mTokens[TokenLibrary.FREIGHTTYPE].ToString(); }
            set { base.mTokens[TokenLibrary.FREIGHTTYPE] = value; base.OnLabelValuesChanged(this,new EventArgs()); }
        }
        [Category("General"),Description("Local lane assignment.")]
        public string LocalLane {
            get { return base.mTokens[TokenLibrary.LOCALLANE].ToString(); }
            set { base.mTokens[TokenLibrary.LOCALLANE] = value; base.OnLabelValuesChanged(this,new EventArgs()); }
        }
        [Category("General"),Description("Local route lane assignment.")]
        public string LocalRouteLane {
            get { return base.mTokens[TokenLibrary.LOCALROUTELANE].ToString(); }
            set { base.mTokens[TokenLibrary.LOCALROUTELANE] = value; base.OnLabelValuesChanged(this,new EventArgs()); }
        }
        [Category("General"),Description("Now (time).")]
        public string TimeNow {
            get { return base.mTokens[TokenLibrary.TIMENOW].ToString(); }
            set { base.mTokens[TokenLibrary.TIMENOW] = value; base.OnLabelValuesChanged(this,new EventArgs()); }
        }
        [Category("General"),Description("SAN number.")]
        public string San {
            get { return base.mTokens[TokenLibrary.SAN].ToString(); }
            set { base.mTokens[TokenLibrary.SAN] = value; base.OnLabelValuesChanged(this,new EventArgs()); }
        }
		
		[ Category("Freight"), Description("Freight pickup date.")]
		public string FreightPickupDate { 
			get { return base.mTokens[TokenLibrary.FREIGHTPICKUPDATE].ToString(); } 
			set { base.mTokens[TokenLibrary.FREIGHTPICKUPDATE] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Freight"), Description("Freight pickup information.")]
		public string FreightPickupInfo { 
			get { return base.mTokens[TokenLibrary.FREIGHTPICKUPINFO].ToString(); } 
			set { base.mTokens[TokenLibrary.FREIGHTPICKUPINFO] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Freight"), Description("Freight pickup number.")]
		public string FreightPickupNumber { 
			get { return base.mTokens[TokenLibrary.FREIGHTPICKUPNUMBER].ToString(); } 
			set { base.mTokens[TokenLibrary.FREIGHTPICKUPNUMBER] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Freight"), Description("Freight pickup number string.")]
		public string FreightPickupNumberString { 
			get { return base.mTokens[TokenLibrary.FREIGHTPICKUPNUMBERSTRING].ToString(); } 
			set { base.mTokens[TokenLibrary.FREIGHTPICKUPNUMBERSTRING] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Freight"), Description("Freight vendor key.")]
		public string FreightVendorKey { 
			get { return base.mTokens[TokenLibrary.FREIGHTVENDORKEY].ToString(); } 
			set { base.mTokens[TokenLibrary.FREIGHTVENDORKEY] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Freight"), Description("Freight lane prefix.")]
		public string LanePrefix { 
			get { return base.mTokens[TokenLibrary.LANEPREFIX].ToString(); } 
			set { base.mTokens[TokenLibrary.LANEPREFIX] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
        [Category("Freight"),Description("TL date.")]
        public string TLDate {
            get { return base.mTokens[TokenLibrary.TLDATE].ToString(); }
            set { base.mTokens[TokenLibrary.TLDATE] = value; base.OnLabelValuesChanged(this,new EventArgs()); }
        }
        [Category("Freight"),Description("TL close number.")]
        public string TLCloseNumber {
            get { return base.mTokens[TokenLibrary.TLCLOSENUMBER].ToString(); }
            set { base.mTokens[TokenLibrary.TLCLOSENUMBER] = value; base.OnLabelValuesChanged(this,new EventArgs()); }
        }
		
		
		[ Category("Carton"), Description("Carton number.")]
		public string CartonNumber { 
			get { return base.mTokens[TokenLibrary.CARTONNUMBER].ToString(); } 
			set { base.mTokens[TokenLibrary.CARTONNUMBER] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Carton"), Description("Carton purchase order number.")]
		public string CartonNumberOrPoNumber { 
			get { return base.mTokens[TokenLibrary.CARTONNUMBERORPONUMBER].ToString(); } 
			set { base.mTokens[TokenLibrary.CARTONNUMBERORPONUMBER] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Carton"), Description("Carton (sorted item) return number.")]
		public string SortedItemLabelNumber { 
			get { return base.mTokens[TokenLibrary.SORTEDITEMLABELNUMBER].ToString(); } 
			set { base.mTokens[TokenLibrary.SORTEDITEMLABELNUMBER] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Carton"), Description("Carton number OR purchase order number.")]
		public string SortedItemWeightString { 
			get { return base.mTokens[TokenLibrary.SORTEDITEMWEIGHTSTRING].ToString(); } 
			set { base.mTokens[TokenLibrary.SORTEDITEMWEIGHTSTRING] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Carton"), Description("Carton (sorted item) label number.")]
		public string ItemDamageCode { 
			get { return base.mTokens[TokenLibrary.ITEMDAMAGECODE].ToString(); } 
			set { base.mTokens[TokenLibrary.ITEMDAMAGECODE] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Carton"), Description("Carton (sorted item) weight.")]
		public string ItemType { 
			get { return base.mTokens[TokenLibrary.ITEMTYPE].ToString(); } 
			set { base.mTokens[TokenLibrary.ITEMTYPE] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Carton"), Description("Carton (sorted item) damage code.")]
		public string PONumber { 
			get { return base.mTokens[TokenLibrary.PONUMBER].ToString(); } 
			set { base.mTokens[TokenLibrary.PONUMBER] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Carton"), Description("Carton (sorted item) type.")]
		public string ReturnNumber { 
			get { return base.mTokens[TokenLibrary.RETURNNUMBER].ToString(); } 
			set { base.mTokens[TokenLibrary.RETURNNUMBER] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		
		
		[ Category("Client"), Description("Argix client number.")]
		public string ClientNumber { 
			get { return base.mTokens[TokenLibrary.CLIENTNUMBER].ToString(); } 
			set { base.mTokens[TokenLibrary.CLIENTNUMBER] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Client"), Description("Client name.")]
		public string ClientName { 
			get { return base.mTokens[TokenLibrary.CLIENTNAME].ToString(); } 
			set { base.mTokens[TokenLibrary.CLIENTNAME] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Client"), Description("Client mnemonic.")]
		public string ClientAbbreviation { 
			get { return base.mTokens[TokenLibrary.CLIENTABBREVIATION].ToString(); } 
			set { base.mTokens[TokenLibrary.CLIENTABBREVIATION] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Client"), Description("Client (Argix) division number.")]
		public string ClientDivisionNumber { 
			get { return base.mTokens[TokenLibrary.CLIENTDIVISIONNUMBER].ToString(); } 
			set { base.mTokens[TokenLibrary.CLIENTDIVISIONNUMBER] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Client"), Description("Client address line 1.")]
		public string ClientAddressLine1 { 
			get { return base.mTokens[TokenLibrary.CLIENTADDRESSLINE1].ToString(); } 
			set { base.mTokens[TokenLibrary.CLIENTADDRESSLINE1] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Client"), Description("Client address line 2.")]
		public string ClientAddressLine2 { 
			get { return base.mTokens[TokenLibrary.CLIENTADDRESSLINE2].ToString(); } 
			set { base.mTokens[TokenLibrary.CLIENTADDRESSLINE2] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Client"), Description("Client address city.")]
		public string ClientAddressCity { 
			get { return base.mTokens[TokenLibrary.CLIENTADDRESSCITY].ToString(); } 
			set { base.mTokens[TokenLibrary.CLIENTADDRESSCITY] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Client"), Description("Client address US state or province.")]
		public string ClientAddressState { 
			get { return base.mTokens[TokenLibrary.CLIENTADDRESSSTATE].ToString(); } 
			set { base.mTokens[TokenLibrary.CLIENTADDRESSSTATE] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Client"), Description("Client address US zip code.")]
		public string ClientAddressZip { 
			get { return base.mTokens[TokenLibrary.CLIENTADDRESSZIP].ToString(); } 
			set { base.mTokens[TokenLibrary.CLIENTADDRESSZIP] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Client"), Description("Client address country code.")]
		public string ClientAddressCountryCode { 
			get { return base.mTokens[TokenLibrary.CLIENTADDRESSCOUNTRYCODE].ToString(); } 
			set { base.mTokens[TokenLibrary.CLIENTADDRESSCOUNTRYCODE] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		
		[ Category("Store"), Description("Argix store number.")]
		public string StoreNumber { 
			get { return base.mTokens[TokenLibrary.STORENUMBER].ToString(); } 
			set { base.mTokens[TokenLibrary.STORENUMBER] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Store"), Description("Store name.")]
		public string StoreName { 
			get { return base.mTokens[TokenLibrary.STORENAME].ToString(); } 
			set { base.mTokens[TokenLibrary.STORENAME] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Store"), Description("Store address line 1.")]
		public string StoreAddressLine1 { 
			get { return base.mTokens[TokenLibrary.STOREADDRESSLINE1].ToString(); } 
			set { base.mTokens[TokenLibrary.STOREADDRESSLINE1] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Store"), Description("Store address line 2.")]
		public string StoreAddressLine2 { 
			get { return base.mTokens[TokenLibrary.STOREADDRESSLINE2].ToString(); } 
			set { base.mTokens[TokenLibrary.STOREADDRESSLINE2] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Store"), Description("Store address city.")]
		public string StoreAddressCity { 
			get { return base.mTokens[TokenLibrary.STOREADDRESSCITY].ToString(); } 
			set { base.mTokens[TokenLibrary.STOREADDRESSCITY] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Store"), Description("Store address US state or province.")]
		public string StoreAddressState { 
			get { return base.mTokens[TokenLibrary.STOREADDRESSSTATE].ToString(); } 
			set { base.mTokens[TokenLibrary.STOREADDRESSSTATE] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Store"), Description("Store address US zip code.")]
		public string StoreAddressZip { 
			get { return base.mTokens[TokenLibrary.STOREADDRESSZIP].ToString(); } 
			set { base.mTokens[TokenLibrary.STOREADDRESSZIP] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Store"), Description("Store address US zip code.")]
		public string StoreZip { 
			get { return base.mTokens[TokenLibrary.STOREADDRESSZIP].ToString(); } 
			set { base.mTokens[TokenLibrary.STOREADDRESSZIP] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Store"), Description("Store address telephone number.")]
		public string StorePhone { 
			get { return base.mTokens[TokenLibrary.STOREPHONE].ToString(); } 
			set { base.mTokens[TokenLibrary.STOREPHONE] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Store"), Description("Store route code.")]
		public string StoreRoute { 
			get { return base.mTokens[TokenLibrary.STOREROUTE].ToString(); } 
			set { base.mTokens[TokenLibrary.STOREROUTE] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Store"), Description("First character of store route code.")]
		public string StoreRouteFirstCharacter { 
			get { return base.mTokens[TokenLibrary.STOREROUTEFIRSTCHARACTER].ToString(); } 
			set { base.mTokens[TokenLibrary.STOREROUTEFIRSTCHARACTER] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Store"), Description("First 2 characters of store route code.")]
		public string StoreRouteFirstTwo { 
			get { return base.mTokens[TokenLibrary.STOREROUTEFIRSTTWO].ToString(); } 
			set { base.mTokens[TokenLibrary.STOREROUTEFIRSTTWO] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Store"), Description("Last 3 characters of store route code.")]
		public string StoreRouteLastFour { 
			get { return base.mTokens[TokenLibrary.STOREROUTELASTFOUR].ToString(); } 
			set { base.mTokens[TokenLibrary.STOREROUTELASTFOUR] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Store"), Description("Last 4 characters of store route code.")]
		public string StoreRouteLastThree { 
			get { return base.mTokens[TokenLibrary.STOREROUTELASTTHREE].ToString(); } 
			set { base.mTokens[TokenLibrary.STOREROUTELASTTHREE] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Store"), Description("Alternate store route code.")]
		public string StoreAltRoute { 
			get { return base.mTokens[TokenLibrary.STOREALTROUTE].ToString(); } 
			set { base.mTokens[TokenLibrary.STOREALTROUTE] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Store"), Description("Store custom label data.")]
		public string StoreUserLabelData { 
			get { return base.mTokens[TokenLibrary.STOREUSERLABELDATA].ToString(); } 
			set { base.mTokens[TokenLibrary.STOREUSERLABELDATA] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		
		[ Category("Destination"), Description("Argix destination number.")]
		public string DestinationNumber { 
			get { return base.mTokens[TokenLibrary.DESTINATIONNUMBER].ToString(); } 
			set { base.mTokens[TokenLibrary.DESTINATIONNUMBER] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Destination"), Description("Destination name.")]
		public string DestinationName { 
			get { return base.mTokens[TokenLibrary.DESTINATIONNAME].ToString(); } 
			set { base.mTokens[TokenLibrary.DESTINATIONNAME] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Destination"), Description("Destination address line 1.")]
		public string DestinationAddressLine1 { 
			get { return base.mTokens[TokenLibrary.DESTINATIONADDRESSLINE1].ToString(); } 
			set { base.mTokens[TokenLibrary.DESTINATIONADDRESSLINE1] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Destination"), Description("Destination address line 2.")]
		public string DestinationAddressLine2 { 
			get { return base.mTokens[TokenLibrary.DESTINATIONADDRESSLINE2].ToString(); } 
			set { base.mTokens[TokenLibrary.DESTINATIONADDRESSLINE2] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Destination"), Description("Destination address city.")]
		public string DestinationAddressCity { 
			get { return base.mTokens[TokenLibrary.DESTINATIONADDRESSCITY].ToString(); } 
			set { base.mTokens[TokenLibrary.DESTINATIONADDRESSCITY] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Destination"), Description("Destination address US state or province.")]
		public string DestinationAddressState { 
			get { return base.mTokens[TokenLibrary.DESTINATIONADDRESSSTATE].ToString(); } 
			set { base.mTokens[TokenLibrary.DESTINATIONADDRESSSTATE] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Destination"), Description("Destination address US zip code.")]
		public string DestinationAddressZip { 
			get { return base.mTokens[TokenLibrary.DESTINATIONADDRESSZIP].ToString(); } 
			set { base.mTokens[TokenLibrary.DESTINATIONADDRESSZIP] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Destination"), Description("Destination address US zip code.")]
		public string DestinationZip { 
			get { return base.mTokens[TokenLibrary.DESTINATIONADDRESSZIP].ToString(); } 
			set { base.mTokens[TokenLibrary.DESTINATIONADDRESSZIP] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Destination"), Description("Destination address telephone number.")]
		public string DestinationPhone { 
			get { return base.mTokens[TokenLibrary.DESTINATIONPHONE].ToString(); } 
			set { base.mTokens[TokenLibrary.DESTINATIONPHONE] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Destination"), Description("Destination route code.")]
		public string DestinationRoute { 
			get { return base.mTokens[TokenLibrary.DESTINATIONROUTE].ToString(); } 
			set { base.mTokens[TokenLibrary.DESTINATIONROUTE] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Destination"), Description("First character of store route code.")]
		public string DestinationRouteFirstCharacter { 
			get { return base.mTokens[TokenLibrary.DESTINATIONROUTEFIRSTCHARACTER].ToString(); } 
			set { base.mTokens[TokenLibrary.DESTINATIONROUTEFIRSTCHARACTER] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Destination"), Description("First 2 characters of store route code.")]
		public string DestinationRouteFirstTwo { 
			get { return base.mTokens[TokenLibrary.DESTINATIONROUTEFIRSTTWO].ToString(); } 
			set { base.mTokens[TokenLibrary.DESTINATIONROUTEFIRSTTWO] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Destination"), Description("Last 3 characters of store route code.")]
		public string DestinationRouteLastThree { 
			get { return base.mTokens[TokenLibrary.DESTINATIONROUTELASTTHREE].ToString(); } 
			set { base.mTokens[TokenLibrary.DESTINATIONROUTELASTTHREE] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Destination"), Description("Last 4 characters of store route code.")]
		public string DestinationRouteLastFour { 
			get { return base.mTokens[TokenLibrary.DESTINATIONROUTELASTFOUR].ToString(); } 
			set { base.mTokens[TokenLibrary.DESTINATIONROUTELASTFOUR] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Destination"), Description("Alternate destination route code.")]
		public string DestinationAltRoute { 
			get { return base.mTokens[TokenLibrary.DESTINATIONALTROUTE].ToString(); } 
			set { base.mTokens[TokenLibrary.DESTINATIONALTROUTE] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Destination"), Description("Destination custom label data.")]
		public string DestinationUserLabelData { 
			get { return base.mTokens[TokenLibrary.DESTINATIONUSERLABELDATA].ToString(); } 
			set { base.mTokens[TokenLibrary.DESTINATIONUSERLABELDATA] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}

		[ Category("Vendor"), Description("Argix vendor number.")]
		public string VendorNumber { 
			get { return base.mTokens[TokenLibrary.VENDORNUMBER].ToString(); } 
			set { base.mTokens[TokenLibrary.VENDORNUMBER] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Vendor"), Description("Vendor name.")]
		public string VendorName { 
			get { return base.mTokens[TokenLibrary.VENDORNAME].ToString(); } 
			set { base.mTokens[TokenLibrary.VENDORNAME] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Vendor"), Description("Vendor address line 1.")]
		public string VendorAddressLine1 { 
			get { return base.mTokens[TokenLibrary.VENDORADDRESSLINE1].ToString(); } 
			set { base.mTokens[TokenLibrary.VENDORADDRESSLINE1] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Vendor"), Description("Vendor address line 2.")]
		public string VendorAddressLine2 { 
			get { return base.mTokens[TokenLibrary.VENDORADDRESSLINE2].ToString(); } 
			set { base.mTokens[TokenLibrary.VENDORADDRESSLINE2] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Vendor"), Description("Vendor address city.")]
		public string VendorAddressCity { 
			get { return base.mTokens[TokenLibrary.VENDORADDRESSCITY].ToString(); } 
			set { base.mTokens[TokenLibrary.VENDORADDRESSCITY] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Vendor"), Description("Vendor address US state or province.")]
		public string VendorAddressState { 
			get { return base.mTokens[TokenLibrary.VENDORADDRESSSTATE].ToString(); } 
			set { base.mTokens[TokenLibrary.VENDORADDRESSSTATE] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Vendor"), Description("Vendor address US zip code.")]
		public string VendorAddressZip { 
			get { return base.mTokens[TokenLibrary.VENDORNUMBER].ToString(); } 
			set { base.mTokens[TokenLibrary.VENDORNUMBER] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Vendor"), Description("Vendor custom label data.")]
		public string VendorUserData { 
			get { return base.mTokens[TokenLibrary.VENDORUSERDATA].ToString(); } 
			set { base.mTokens[TokenLibrary.VENDORUSERDATA] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}		
		
		[ Category("Shipper"), Description("Argix shipper number.")]
		public string ShipperNumber { 
			get { return base.mTokens[TokenLibrary.SHIPPERNUMBER].ToString(); } 
			set { base.mTokens[TokenLibrary.SHIPPERNUMBER] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Shipper"), Description("Shipper name.")]
		public string ShipperName { 
			get { return base.mTokens[TokenLibrary.SHIPPERNAME].ToString(); } 
			set { base.mTokens[TokenLibrary.SHIPPERNAME] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Shipper"), Description("Shipper address line 1.")]
		public string ShipperAddressLine1 { 
			get { return base.mTokens[TokenLibrary.SHIPPERADDRESSLINE1].ToString(); } 
			set { base.mTokens[TokenLibrary.SHIPPERADDRESSLINE1] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Shipper"), Description("Shipper address line 2.")]
		public string ShipperAddressLine2 { 
			get { return base.mTokens[TokenLibrary.SHIPPERADDRESSLINE2].ToString(); } 
			set { base.mTokens[TokenLibrary.SHIPPERADDRESSLINE2] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Shipper"), Description("Shipper address city.")]
		public string ShipperAddressCity { 
			get { return base.mTokens[TokenLibrary.SHIPPERADDRESSCITY].ToString(); } 
			set { base.mTokens[TokenLibrary.SHIPPERADDRESSCITY] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Shipper"), Description("Shipper address US state or province.")]
		public string ShipperAddressState { 
			get { return base.mTokens[TokenLibrary.SHIPPERADDRESSSTATE].ToString(); } 
			set { base.mTokens[TokenLibrary.SHIPPERADDRESSSTATE] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Shipper"), Description("Shipper address US zip code.")]
		public string ShipperAddressZip { 
			get { return base.mTokens[TokenLibrary.SHIPPERADDRESSZIP].ToString(); } 
			set { base.mTokens[TokenLibrary.SHIPPERADDRESSZIP] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Shipper"), Description("Shipper custom label data.")]
		public string ShipperUserData { 
			get { return base.mTokens[TokenLibrary.SHIPPERUSERDATA].ToString(); } 
			set { base.mTokens[TokenLibrary.SHIPPERUSERDATA] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}

		[ Category("Zone"), Description("Argix zone code.")]
		public string ZoneCode { 
			get { return base.mTokens[TokenLibrary.ZONECODE].ToString(); } 
			set { base.mTokens[TokenLibrary.ZONECODE] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Zone"), Description("Associated label type.")]
		public string ZoneLabelType { 
			get { return base.mTokens[TokenLibrary.ZONELABELTYPE].ToString(); } 
			set { base.mTokens[TokenLibrary.ZONELABELTYPE] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Zone"), Description("Associated sort lane.")]
		public string ZoneLane { 
			get { return base.mTokens[TokenLibrary.ZONELANE].ToString(); } 
			set { base.mTokens[TokenLibrary.ZONELANE] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Zone"), Description("Associated small sort lane.")]
		public string ZoneLaneSmallSort { 
			get { return base.mTokens[TokenLibrary.ZONELANESMALLSORT].ToString(); } 
			set { base.mTokens[TokenLibrary.ZONELANESMALLSORT] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Zone"), Description("Outbound trailer load number.")]
		public string ZoneOutboundTrailerLoadNumber { 
			get { return base.mTokens[TokenLibrary.ZONEOUTBOUNDTRAILERLOADNUMBER].ToString(); } 
			set { base.mTokens[TokenLibrary.ZONEOUTBOUNDTRAILERLOADNUMBER] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Zone"), Description("Outbound trailer load number (digits only).")]
		public string ZoneOutboundTrailerLoadNumberDigitsOnly { 
			get { return base.mTokens[TokenLibrary.ZONEOUTBOUNDTRAILERLOADNUMBERDIGITSONLY].ToString(); } 
			set { base.mTokens[TokenLibrary.ZONEOUTBOUNDTRAILERLOADNUMBERDIGITSONLY] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		
		[ Category("WorkStation"), Description("Workstation ID.")]
		public string WorkStationID { 
			get { return base.mTokens[TokenLibrary.WORKSTATIONID].ToString(); } 
			set { base.mTokens[TokenLibrary.WORKSTATIONID] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("WorkStation"), Description("Workstation number.")]
		public string WorkStationName { 
			get { return base.mTokens[TokenLibrary.WORKSTATIONNAME].ToString(); } 
			set { base.mTokens[TokenLibrary.WORKSTATIONNAME] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("WorkStation"), Description("Workstation name.")]
		public string WorkStationNumber { 
			get { return base.mTokens[TokenLibrary.WORKSTATIONNUMBER].ToString(); } 
			set { base.mTokens[TokenLibrary.WORKSTATIONNUMBER] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("WorkStation"), Description("Workstation description.")]
		public string WorkStationDescription { 
			get { return base.mTokens[TokenLibrary.WORKSTATIONDESCRIPTION].ToString(); } 
			set { base.mTokens[TokenLibrary.WORKSTATIONDESCRIPTION] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
	
		[ Category("Special Agent"), Description("Service Title.")]
		public string OSServiceTitle { 
			get { return base.mTokens[TokenLibrary.OSSERVICETITLE].ToString(); } 
			set { base.mTokens[TokenLibrary.OSSERVICETITLE] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Special Agent"), Description("Barcode 1 Data Human Format.")]
		public string OSBarcode1DataHumanFormat { 
			get { return base.mTokens[TokenLibrary.OSBARCODE1DATAHUMANFORMAT].ToString(); } 
			set { base.mTokens[TokenLibrary.OSBARCODE1DATAHUMANFORMAT] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Special Agent"), Description("Data Identifier.")]
		public string OSDataIdentifier { 
			get { return base.mTokens[TokenLibrary.OSDATAIDENTIFIER].ToString(); } 
			set { base.mTokens[TokenLibrary.OSDATAIDENTIFIER] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Special Agent"), Description("Barcode 1 Data.")]
		public string OSBarcode1Data { 
			get { return base.mTokens[TokenLibrary.OSBARCODE1DATA].ToString(); } 
			set { base.mTokens[TokenLibrary.OSBARCODE1DATA] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Special Agent"), Description("Routing Code.")]
		public string OSRoutingCode { 
			get { return base.mTokens[TokenLibrary.OSROUTINGCODE].ToString(); } 
			set { base.mTokens[TokenLibrary.OSROUTINGCODE] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Special Agent"), Description("Service Indicator.")]
		public string OSServiceIndicator { 
			get { return base.mTokens[TokenLibrary.OSSERVICEINDICATOR].ToString(); } 
			set { base.mTokens[TokenLibrary.OSSERVICEINDICATOR] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Special Agent"), Description("Tracking Number 10.")]
		public string OSTrackingNumber10 { 
			get { return base.mTokens[TokenLibrary.OSTRACKINGNUMBER10].ToString(); } 
			set { base.mTokens[TokenLibrary.OSTRACKINGNUMBER10] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Special Agent"), Description("Sender Account Number.")]
		public string OSSenderAccountNumber { 
			get { return base.mTokens[TokenLibrary.OSSENDERACCOUNTNUMBER].ToString(); } 
			set { base.mTokens[TokenLibrary.OSSENDERACCOUNTNUMBER] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Special Agent"), Description("Julian Date.")]
		public string OSJulianDate { 
			get { return base.mTokens[TokenLibrary.OSJULIANDATE].ToString(); } 
			set { base.mTokens[TokenLibrary.OSJULIANDATE] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("Special Agent"), Description("Service Icon.")]
		public string OSServiceIcon { 
			get { return base.mTokens[TokenLibrary.OSSERVICEICON].ToString(); } 
			set { base.mTokens[TokenLibrary.OSSERVICEICON] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		
		[ Category("PandA"), Description("Panda status code.")]
		public string StatusCode { 
			get { return base.mTokens[TokenLibrary.STATUSCODE].ToString(); } 
			set { base.mTokens[TokenLibrary.STATUSCODE] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
		[ Category("PandA"), Description("Panda message text.")]
		public string MessageText { 
			get { return base.mTokens[TokenLibrary.MESSAGETEXT].ToString(); } 
			set { base.mTokens[TokenLibrary.MESSAGETEXT] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
	}
}