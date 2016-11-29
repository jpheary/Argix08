//	File:	tokenlibrary.cs
//	Author:	J. Heary
//	Date:	01/02/08
//	Desc:	.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.ComponentModel;

namespace Tsort.Labels {
    /// <summary> </summary>
    public class TokenLibrary {
        //Class members

        //Constants
        #region Token Definitions
        public const string SAN = "<san>";
        public const string LOCALLANE = "<localLane>";
        public const string LOCALROUTELANE = "<localRouteLane>";
        public const string FREIGHTTYPE = "<freightType>";
        public const string CURRENTDATE = "<currentDate>";
        public const string CURRENTTIME = "<currentTime>";
        public const string TIMENOW = "<timeNow>";
        public const string CURRENTYEAR = "<currentYear>";
        public const string FREIGHTPICKUPDATE = "<freightPickupDate>";
        public const string FREIGHTPICKUPINFO = "<freightPickupInfo>";
        public const string FREIGHTPICKUPNUMBER = "<freightPickupNumber>";
        public const string FREIGHTPICKUPNUMBERSTRING = "<freightPickupNumberString>";
        public const string FREIGHTVENDORKEY = "<freightVendorKey>";
        public const string LANEPREFIX = "<lanePrefix>";

        public const string CARTONNUMBER = "<cartonNumber>";
        public const string CARTONNUMBERORPONUMBER = "<cartonNumberOrPoNumber>";
        public const string SORTEDITEMLABELNUMBER = "<sortedItemLabelNumber>";
        public const string SORTEDITEMWEIGHTSTRING = "<sortedItemWeightString>";
        public const string ITEMDAMAGECODE = "<itemDamageCode>";
        public const string ITEMTYPE = "<itemType>";
        public const string PONUMBER = "<poNumber>";
        public const string RETURNNUMBER = "<returnNumber>";
        public const string TLDATE = "<tlDateMMDDYY>";
        public const string TLCLOSENUMBER = "<tlCloseNumber>";

        public const string CLIENTNUMBER = "<clientNumber>";
        public const string CLIENTNAME = "<clientName>";
        public const string CLIENTABBREVIATION = "<clientAbbreviation>";
        public const string CLIENTDIVISIONNUMBER = "<clientDivisionNumber>";
        public const string CLIENTADDRESSLINE1 = "<clientAddressLine1>";
        public const string CLIENTADDRESSLINE2 = "<clientAddressLine2>";
        public const string CLIENTADDRESSCITY = "<clientAddressCity>";
        public const string CLIENTADDRESSSTATE = "<clientAddressState>";
        public const string CLIENTADDRESSZIP = "<clientAddressZip>";
        public const string CLIENTADDRESSCOUNTRYCODE = "<clientAddressCountryCode>";

        public const string STORENUMBER = "<storeNumber>";
        public const string STORENAME = "<storeName>";
        public const string STOREADDRESSLINE1 = "<storeAddressLine1>";
        public const string STOREADDRESSLINE2 = "<storeAddressLine2>";
        public const string STOREADDRESSCITY = "<storeAddressCity>";
        public const string STOREADDRESSSTATE = "<storeAddressState>";
        public const string STOREADDRESSZIP = "<storeAddressZip>";
        public const string STOREZIP = "<storeZip>";
        public const string STOREADDRESSCOUNTRYCODE = "<storeCountryCode>";
        public const string STOREPHONE = "<storePhone>";
        public const string STOREROUTE = "<storeRoute>";
        public const string STOREALTROUTE = "<storeAltRoute>";
        public const string STOREROUTEFIRSTCHARACTER = "<storeRouteFirstCharacter>";
        public const string STOREROUTEFIRSTTWO = "<storeRouteFirstTwo>";
        public const string STOREROUTELASTFOUR = "<storeRouteLastFour>";
        public const string STOREROUTELASTTHREE = "<storeRouteLastThree>";
        public const string STOREUSERLABELDATA = "<storeUserLabelData>";

        public const string DESTINATIONNUMBER = "<destinationNumber>";
        public const string DESTINATIONNAME = "<destinationName>";
        public const string DESTINATIONADDRESSLINE1 = "<destinationAddressLine1>";
        public const string DESTINATIONADDRESSLINE2 = "<destinationAddressLine2>";
        public const string DESTINATIONADDRESSCITY = "<destinationAddressCity>";
        public const string DESTINATIONADDRESSSTATE = "<destinationAddressState>";
        public const string DESTINATIONADDRESSZIP = "<destinationAddressZip>";
        public const string DESTINATIONZIP = "<destinationZip>";
        public const string DESTINATIONADDRESSCOUNTRYCODE = "<destinationCountryCode>";
        public const string DESTINATIONPHONE = "<destinationPhone>";
        public const string DESTINATIONROUTE = "<destinationRoute>";
        public const string DESTINATIONALTROUTE = "<destinationAltRoute>";
        public const string DESTINATIONROUTEFIRSTCHARACTER = "<destinationRouteFirstCharacter>";
        public const string DESTINATIONROUTEFIRSTTWO = "<destinationRouteFirstTwo>";
        public const string DESTINATIONROUTELASTFOUR = "<destinationRouteLastFour>";
        public const string DESTINATIONROUTELASTTHREE = "<destinationRouteLastThree>";
        public const string DESTINATIONUSERLABELDATA = "<destinationUserLabelData>";

        public const string VENDORNUMBER = "<vendorNumber>";
        public const string VENDORNAME = "<vendorName>";
        public const string VENDORADDRESSLINE1 = "<vendorAddressLine1>";
        public const string VENDORADDRESSLINE2 = "<vendorAddressLine2>";
        public const string VENDORADDRESSCITY = "<vendorAddressCity>";
        public const string VENDORADDRESSSTATE = "<vendorAddressState>";
        public const string VENDORADDRESSZIP = "<vendorAddressZip>";
        public const string VENDORUSERDATA = "<vendorUserData>";

        public const string SHIPPERNUMBER = "<shipperNumber>";
        public const string SHIPPERNAME = "<shipperName>";
        public const string SHIPPERADDRESSLINE1 = "<shipperAddressLine1>";
        public const string SHIPPERADDRESSLINE2 = "<shipperAddressLine2>";
        public const string SHIPPERADDRESSCITY = "<shipperAddressCity>";
        public const string SHIPPERADDRESSSTATE = "<shipperAddressState>";
        public const string SHIPPERADDRESSZIP = "<shipperAddressZip>";
        public const string SHIPPERUSERDATA = "<shipperUserData>";

        public const string ZONECODE = "<zoneCode>";
        public const string ZONELABELTYPE = "<zoneLabelType>";
        public const string ZONELANE = "<zoneLane>";
        public const string ZONELANESMALLSORT = "<zoneLaneSmallSort>";
        public const string ZONEOUTBOUNDTRAILERLOADNUMBER = "<zoneOutboundTrailerLoadNumber>";
        public const string ZONEOUTBOUNDTRAILERLOADNUMBERDIGITSONLY = "<zoneOutboundTrailerLoadNumberDigitsOnly>";

        public const string WORKSTATIONID = "<WorkStationID>";
        public const string WORKSTATIONNAME = "<Name>";
        public const string WORKSTATIONNUMBER = "<Number>";
        public const string WORKSTATIONNUMBER2 = "<stationNumber>";
        public const string WORKSTATIONDESCRIPTION = "<Description>";

        public const string OSSERVICETITLE = "<osServiceTitle>";
        public const string OSBARCODE1DATAHUMANFORMAT = "<osBarcode1DataHumanFormat>";
        public const string OSDATAIDENTIFIER = "<osDataIdentifier>";
        public const string OSBARCODE1DATA = "<osBarcode1Data>";
        public const string OSROUTINGCODE = "<osRoutingCode>";
        public const string OSSERVICEINDICATOR = "<osServiceIndicator>";
        public const string OSTRACKINGNUMBER10 = "<osTrackingNumber10>";
        public const string OSSENDERACCOUNTNUMBER = "<osSenderAccountNumber>";
        public const string OSJULIANDATE = "<osJulianDate>";
        public const string OSSERVICEICON = "<osServiceIcon>";

        public const string STATUSCODE = "<statusCode>";
        public const string MESSAGETEXT = "<messageText>";
        #endregion

        //Interface- the following 'get' members are used for displaying the tokens
        //(works good in a property control)
        public TokenLibrary() { }
        [Category("General"),Description("SAN number.")]
        public string San { get { return TokenLibrary.SAN; } }
        [Category("General"),Description("Local lane assignment.")]
        public string LocalLane { get { return TokenLibrary.LOCALLANE; } }
        [Category("General"),Description("Local route lane.")]
        public string LocalRouteLane { get { return TokenLibrary.LOCALROUTELANE; } }
        [Category("General"),Description("Freight type.")]
        public string FreightType { get { return TokenLibrary.FREIGHTTYPE; } }
        [Category("General"),Description("Current date.")]
        public string CurrentDate { get { return TokenLibrary.CURRENTDATE; } }
        [Category("General"),Description("Current time.")]
        public string CurrentTime { get { return TokenLibrary.CURRENTTIME; } }
        [Category("General"),Description("Time now.")]
        public string TimeNow { get { return TokenLibrary.TIMENOW; } }
        [Category("General"),Description("Current year.")]
        public string CurrentYear { get { return TokenLibrary.CURRENTYEAR; } }

        [Category("Freight"),Description("Freight pickup date.")]
        public string FreightPickupDate { get { return TokenLibrary.FREIGHTPICKUPDATE; } }
        [Category("Freight"),Description("Freight pickup information.")]
        public string FreightPickupInfo { get { return TokenLibrary.FREIGHTPICKUPINFO; } }
        [Category("Freight"),Description("Freight pickup number.")]
        public string FreightPickupNumber { get { return TokenLibrary.FREIGHTPICKUPNUMBER; } }
        [Category("Freight"),Description("Freight pickup number string.")]
        public string FreightPickupNumberString { get { return TokenLibrary.FREIGHTPICKUPNUMBERSTRING; } }
        [Category("Freight"),Description("Freight vendor key.")]
        public string FreightVendorKey { get { return TokenLibrary.FREIGHTVENDORKEY; } }
        [Category("Freight"),Description("Freight lane prefix.")]
        public string LanePrefix { get { return TokenLibrary.LANEPREFIX; } }
        [Category("Freight"),Description("Freight TL date.")]
        public string TLDate { get { return TokenLibrary.TLDATE; } }
        [Category("Freight"),Description("Freight TL close number.")]
        public string TLCloseNumber { get { return TokenLibrary.TLCLOSENUMBER; } }

        [Category("Carton"),Description("Carton number.")]
        public string CartonNumber { get { return TokenLibrary.CARTONNUMBER; } }
        [Category("Carton"),Description("Carton purchase order number.")]
        public string PONumber { get { return TokenLibrary.PONUMBER; } }
        [Category("Carton"),Description("Carton (sorted item) return number.")]
        public string ReturnNumber { get { return TokenLibrary.RETURNNUMBER; } }
        [Category("Carton"),Description("Carton number OR purchase order number.")]
        public string CartonNumberOrPoNumber { get { return TokenLibrary.CARTONNUMBERORPONUMBER; } }
        [Category("Carton"),Description("Carton (sorted item) label number.")]
        public string SortedItemLabelNumber { get { return TokenLibrary.SORTEDITEMLABELNUMBER; } }
        [Category("Carton"),Description("Carton (sorted item) weight.")]
        public string SortedItemWeightString { get { return TokenLibrary.SORTEDITEMWEIGHTSTRING; } }
        [Category("Carton"),Description("Carton (sorted item) damage code.")]
        public string ItemDamageCode { get { return TokenLibrary.ITEMDAMAGECODE; } }
        [Category("Carton"),Description("Carton (sorted item) type.")]
        public string ItemType { get { return TokenLibrary.ITEMTYPE; } }

        [Category("Client"),Description("Argix client number.")]
        public string ClientNumber { get { return TokenLibrary.CLIENTNUMBER; } }
        [Category("Client"),Description("Client name.")]
        public string ClientName { get { return TokenLibrary.CLIENTNAME; } }
        [Category("Client"),Description("Client mnemonic.")]
        public string ClientAbbreviation { get { return TokenLibrary.CLIENTABBREVIATION; } }
        [Category("Client"),Description("Client (Argix) division number.")]
        public string ClientDivisionNumber { get { return TokenLibrary.CLIENTDIVISIONNUMBER; } }
        [Category("Client"),Description("Client address line 1.")]
        public string ClientAddressLine1 { get { return TokenLibrary.CLIENTADDRESSLINE1; } }
        [Category("Client"),Description("Client address line 2.")]
        public string ClientAddressLine2 { get { return TokenLibrary.CLIENTADDRESSLINE2; } }
        [Category("Client"),Description("Client address city.")]
        public string ClientAddressCity { get { return TokenLibrary.CLIENTADDRESSCITY; } }
        [Category("Client"),Description("Client address US state or province.")]
        public string ClientAddressState { get { return TokenLibrary.CLIENTADDRESSSTATE; } }
        [Category("Client"),Description("Client address US zip code.")]
        public string ClientAddressZip { get { return TokenLibrary.CLIENTADDRESSZIP; } }
        [Category("Client"),Description("Client address country code.")]
        public string ClientAddressCountryCode { get { return TokenLibrary.CLIENTADDRESSCOUNTRYCODE; } }

        [Category("Store"),Description("Argix store number.")]
        public string StoreNumber { get { return TokenLibrary.STORENUMBER; } }
        [Category("Store"),Description("Store name.")]
        public string StoreName { get { return TokenLibrary.STORENAME; } }
        [Category("Store"),Description("Store address line 1.")]
        public string StoreAddressLine1 { get { return TokenLibrary.STOREADDRESSLINE1; } }
        [Category("Store"),Description("Store address line 2.")]
        public string StoreAddressLine2 { get { return TokenLibrary.STOREADDRESSLINE2; } }
        [Category("Store"),Description("Store address city.")]
        public string StoreAddressCity { get { return TokenLibrary.STOREADDRESSCITY; } }
        [Category("Store"),Description("Store address US state or province.")]
        public string StoreAddressState { get { return TokenLibrary.STOREADDRESSSTATE; } }
        [Category("Store"),Description("Store address US zip code.")]
        public string StoreAddressZip { get { return TokenLibrary.STOREADDRESSZIP; } }
        [Category("Store"),Description("Store address US zip code.")]
        public string StoreZip { get { return TokenLibrary.STOREADDRESSZIP; } }
        [Category("Store"),Description("Store address telephone number.")]
        public string StorePhone { get { return TokenLibrary.STOREPHONE; } }
        [Category("Store"),Description("Store route code.")]
        public string StoreRoute { get { return TokenLibrary.STOREROUTE; } }
        [Category("Store"),Description("First character of store route code.")]
        public string StoreRouteFirstCharacter { get { return TokenLibrary.STOREROUTEFIRSTCHARACTER; } }
        [Category("Store"),Description("First 2 characters of store route code.")]
        public string StoreRouteFirstTwo { get { return TokenLibrary.STOREROUTEFIRSTTWO; } }
        [Category("Store"),Description("Last 3 characters of store route code.")]
        public string StoreRouteLastThree { get { return TokenLibrary.STOREROUTELASTTHREE; } }
        [Category("Store"),Description("Last 4 characters of store route code.")]
        public string StoreRouteLastFour { get { return TokenLibrary.STOREROUTELASTFOUR; } }
        [Category("Store"),Description("Alternate store route code.")]
        public string StoreAltRoute { get { return TokenLibrary.STOREALTROUTE; } }
        [Category("Store"),Description("Store custom label data.")]
        public string StoreUserLabelData { get { return TokenLibrary.STOREUSERLABELDATA; } }

        [Category("Destination"),Description("Argix destination number.")]
        public string DestinationNumber { get { return TokenLibrary.DESTINATIONNUMBER; } }
        [Category("Destination"),Description("Destination name.")]
        public string DestinationName { get { return TokenLibrary.DESTINATIONNAME; } }
        [Category("Destination"),Description("Destination address line 1.")]
        public string DestinationAddressLine1 { get { return TokenLibrary.DESTINATIONADDRESSLINE1; } }
        [Category("Destination"),Description("Destination address line 2.")]
        public string DestinationAddressLine2 { get { return TokenLibrary.DESTINATIONADDRESSLINE2; } }
        [Category("Destination"),Description("Destination address city.")]
        public string DestinationAddressCity { get { return TokenLibrary.DESTINATIONADDRESSCITY; } }
        [Category("Destination"),Description("Destination address US state or province.")]
        public string DestinationAddressState { get { return TokenLibrary.DESTINATIONADDRESSSTATE; } }
        [Category("Destination"),Description("Destination address US zip code.")]
        public string DestinationAddressZip { get { return TokenLibrary.DESTINATIONADDRESSZIP; } }
        [Category("Destination"),Description("Destination address US zip code.")]
        public string DestinationZip { get { return TokenLibrary.DESTINATIONADDRESSZIP; } }
        [Category("Destination"),Description("Destination address telephone number.")]
        public string DestinationPhone { get { return TokenLibrary.DESTINATIONPHONE; } }
        [Category("Destination"),Description("Destination route code.")]
        public string DestinationRoute { get { return TokenLibrary.DESTINATIONROUTE; } }
        [Category("Destination"),Description("First character of store route code.")]
        public string DestinationRouteFirstCharacter { get { return TokenLibrary.DESTINATIONROUTEFIRSTCHARACTER; } }
        [Category("Destination"),Description("First 2 characters of store route code.")]
        public string DestinationRouteFirstTwo { get { return TokenLibrary.DESTINATIONROUTEFIRSTTWO; } }
        [Category("Destination"),Description("Last 3 characters of store route code.")]
        public string DestinationRouteLastThree { get { return TokenLibrary.DESTINATIONROUTELASTTHREE; } }
        [Category("Destination"),Description("Last 4 characters of store route code.")]
        public string DestinationRouteLastFour { get { return TokenLibrary.DESTINATIONROUTELASTFOUR; } }
        [Category("Destination"),Description("Alternate destination route code.")]
        public string DestinationAltRoute { get { return TokenLibrary.DESTINATIONALTROUTE; } }
        [Category("Destination"),Description("Destination custom label data.")]
        public string DestinationUserLabelData { get { return TokenLibrary.DESTINATIONUSERLABELDATA; } }

        [Category("Vendor"),Description("Argix vendor number.")]
        public string VendorNumber { get { return TokenLibrary.VENDORNUMBER; } }
        [Category("Vendor"),Description("Vendor name.")]
        public string VendorName { get { return TokenLibrary.VENDORNAME; } }
        [Category("Vendor"),Description("Vendor address line 1.")]
        public string VendorAddressLine1 { get { return TokenLibrary.VENDORADDRESSLINE1; } }
        [Category("Vendor"),Description("Vendor address line 2.")]
        public string VendorAddressLine2 { get { return TokenLibrary.VENDORADDRESSLINE2; } }
        [Category("Vendor"),Description("Vendor address city.")]
        public string VendorAddressCity { get { return TokenLibrary.VENDORADDRESSCITY; } }
        [Category("Vendor"),Description("Vendor address US state or province.")]
        public string VendorAddressState { get { return TokenLibrary.VENDORADDRESSSTATE; } }
        [Category("Vendor"),Description("Vendor address US zip code.")]
        public string VendorAddressZip { get { return TokenLibrary.VENDORADDRESSZIP; } }
        [Category("Vendor"),Description("Vendor custom label data.")]
        public string VendorUserData { get { return TokenLibrary.VENDORUSERDATA; } }

        [Category("Shipper"),Description("Argix shipper number.")]
        public string ShipperNumber { get { return TokenLibrary.SHIPPERNUMBER; } }
        [Category("Shipper"),Description("Shipper name.")]
        public string ShipperName { get { return TokenLibrary.SHIPPERNAME; } }
        [Category("Shipper"),Description("Shipper address line 1.")]
        public string ShipperAddressLine1 { get { return TokenLibrary.SHIPPERADDRESSLINE1; } }
        [Category("Shipper"),Description("Shipper address line 2.")]
        public string ShipperAddressLine2 { get { return TokenLibrary.SHIPPERADDRESSLINE2; } }
        [Category("Shipper"),Description("Shipper address city.")]
        public string ShipperAddressCity { get { return TokenLibrary.SHIPPERADDRESSCITY; } }
        [Category("Shipper"),Description("Shipper address US state or province.")]
        public string ShipperAddressState { get { return TokenLibrary.SHIPPERADDRESSSTATE; } }
        [Category("Shipper"),Description("Shipper address US zip code.")]
        public string ShipperAddressZip { get { return TokenLibrary.SHIPPERADDRESSZIP; } }
        [Category("Shipper"),Description("Shipper custom label data.")]
        public string ShipperUserData { get { return TokenLibrary.SHIPPERUSERDATA; } }

        [Category("Zone"),Description("Argix zone code.")]
        public string ZoneCode { get { return TokenLibrary.ZONECODE; } }
        [Category("Zone"),Description("Associated label type.")]
        public string ZoneLabelType { get { return TokenLibrary.ZONELABELTYPE; } }
        [Category("Zone"),Description("Associated sort lane.")]
        public string ZoneLane { get { return TokenLibrary.ZONELANE; } }
        [Category("Zone"),Description("Associated small sort lane.")]
        public string ZoneLaneSmallSort { get { return TokenLibrary.ZONELANESMALLSORT; } }
        [Category("Zone"),Description("Outbound trailer load number.")]
        public string ZoneOutboundTrailerLoadNumber { get { return TokenLibrary.ZONEOUTBOUNDTRAILERLOADNUMBER; } }
        [Category("Zone"),Description("Outbound trailer load number (digits only).")]
        public string ZoneOutboundTrailerLoadNumberDigitsOnly { get { return TokenLibrary.ZONEOUTBOUNDTRAILERLOADNUMBERDIGITSONLY; } }

        [Category("WorkStation"),Description("Workstation ID.")]
        public string WorkStationID { get { return TokenLibrary.WORKSTATIONID; } }
        [Category("WorkStation"),Description("Workstation number.")]
        public string WorkStationNumber { get { return TokenLibrary.WORKSTATIONNUMBER; } }
        [Category("WorkStation"),Description("Workstation name.")]
        public string WorkStationName { get { return TokenLibrary.WORKSTATIONNAME; } }
        [Category("WorkStation"),Description("Workstation description.")]
        public string WorkStationDescription { get { return TokenLibrary.WORKSTATIONDESCRIPTION; } }


        [Category("Special Agent"),Description("Service Title.")]
        public string OSServiceTitle { get { return TokenLibrary.OSSERVICETITLE; } }
        [Category("Special Agent"),Description("Barcode 1 Data Human Format.")]
        public string OSBarcode1DataHumanFormat { get { return TokenLibrary.OSBARCODE1DATAHUMANFORMAT; } }
        [Category("Special Agent"),Description("Data Identifier.")]
        public string OSDataIdentifier { get { return TokenLibrary.OSDATAIDENTIFIER; } }
        [Category("Special Agent"),Description("Barcode 1 Data.")]
        public string OSBarcode1Data { get { return TokenLibrary.OSBARCODE1DATA; } }
        [Category("Special Agent"),Description("Routing Code.")]
        public string OSRoutingCode { get { return TokenLibrary.OSROUTINGCODE; } }
        [Category("Special Agent"),Description("Service Indicator.")]
        public string OSServiceIndicator { get { return TokenLibrary.OSSERVICEINDICATOR; } }
        [Category("Special Agent"),Description("Tracking Number 10.")]
        public string OSTrackingNumber10 { get { return TokenLibrary.OSTRACKINGNUMBER10; } }
        [Category("Special Agent"),Description("Sender Account Number.")]
        public string OSSenderAccountNumber { get { return TokenLibrary.OSSENDERACCOUNTNUMBER; } }
        [Category("Special Agent"),Description("Julian Date.")]
        public string OSJulianDate { get { return TokenLibrary.OSJULIANDATE; } }
        [Category("Special Agent"),Description("Service Icon.")]
        public string OSServiceIcon { get { return TokenLibrary.OSSERVICEICON; } }

        [Category("PandA"),Description("PandA status code.")]
        public string StatusCode { get { return TokenLibrary.STATUSCODE; } }
        [Category("PandA"),Description("PandA message text.")]
        public string MessageText { get { return TokenLibrary.MESSAGETEXT; } }
    }
}