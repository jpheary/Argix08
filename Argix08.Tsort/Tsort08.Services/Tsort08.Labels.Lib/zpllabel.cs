//	File:	zpllabel.cs
//	Author:	J. Heary
//	Date:	01/02/08
//	Desc:	Represents the state and behavior of a ZPL label.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace Tsort.Labels {
    /// <summary>Represents the state and behavior of a ZPL label.</summary>
	public class ZplLabel {
		//Members
		private ZplLabelDS mLabelDS=null;
		private StringBuilder mLabelFormat=null;
		private bool mIsTsort=true;
		
		//Interface
		public ZplLabel(ZplLabelDS label, bool isTsort) {
			//Constructor
			try {
				if(label.LabelDetailTable.Rows.Count == 0) 
                    throw new ApplicationException("Label specifics missing. Include LABEL_TYPE, PrinterType, LABEL_STRING in ZplLabelDS.LabelDetailTable.");
				this.mLabelDS = label;
				this.mIsTsort = isTsort;
			}
			catch(Exception ex) { throw ex; }
		}
		
		public string LabelType { get { return this.mLabelDS.LabelDetailTable[0].LABEL_TYPE; } }
		public string ZplTemplate { get { return this.mLabelDS.LabelDetailTable[0].LABEL_STRING; } }
		public string ZplFormat {
			//Return the label format (tokens replaced with data)
			get { 
				try {
					//Create a new label format from the label template
					this.mLabelFormat = new StringBuilder(this.mLabelDS.LabelDetailTable[0].LABEL_STRING);
				
					//Replace the data tokens in the label format with values
					convertFreightTokens();
					convertWorkstationTokens();
					convertCartonTokens();
					convertClientTokens();
					convertStoreTokens();
					convertClientVendorTokens();
					convertZoneTokens();
				}
				catch(Exception) { /* Eat it */ }
				return this.mLabelFormat.ToString(); 
			}
		}
		public ZplLabelDS ToDataSet() { return this.mLabelDS; }
		
		#region Token conversions
		private void convertFreightTokens() {
			//Convert general freight-related tokens
			try {
				if(this.mIsTsort) {
					//From store or client vendor
					if(this.mLabelDS.StoreDetailTable.Rows.Count > 0) {
						replaceTokenWithValue("<san>", !this.mLabelDS.StoreDetailTable[0].IsSANNull() ? this.mLabelDS.StoreDetailTable[0].SAN : "");
						replaceTokenWithValue("<localLane>", !this.mLabelDS.StoreDetailTable[0].IsLOCAL_LANENull() ? this.mLabelDS.StoreDetailTable[0].LOCAL_LANE.TrimEnd().PadLeft(2, '0') : "");
					}
					else {
						replaceTokenWithValue("<san>", "");
						replaceTokenWithValue("<localLane>", "");
					}
					replaceTokenWithValue("<freightType>", "44");
				}
				else {
					//from store or client vendor
					if(this.mLabelDS.ClientVendorViewTable.Rows.Count > 0) 
						replaceTokenWithValue("<localLane>", !this.mLabelDS.ClientVendorViewTable[0].IsLOCAL_LANENull() ? this.mLabelDS.ClientVendorViewTable[0].LOCAL_LANE.TrimEnd().PadLeft(2, '0') : "");
					else
						replaceTokenWithValue("<localLane>", "");
					replaceTokenWithValue("<freightType>", "88");
				}
				replaceTokenWithValue("<currentDate>", DateTime.Now.ToShortDateString());
				replaceTokenWithValue("<currentTime>", DateTime.Now.ToString("HH:mm:ss"));
				replaceTokenWithValue("<currentYear>", DateTime.Now.Year.ToString());
				replaceTokenWithValue("<freightPickupDate>", "");
				replaceTokenWithValue("<freightPickupInfo>", "");
				replaceTokenWithValue("<freightPickupNumber>", "");
				replaceTokenWithValue("<freightPickupNumberString>", "");
				replaceTokenWithValue("<freightVendorKey>", "");
				replaceTokenWithValue("<lanePrefix>", !this.mLabelDS.FreightDetailTable[0].IsLanePrefixNull() ? this.mLabelDS.FreightDetailTable[0].LanePrefix.TrimEnd() : "");
			}
			catch(Exception) { /* Eat it */ }
		}
		private void convertWorkstationTokens() {
			//Convert carton-related tokens
			try {
				if(this.mLabelDS.WorkstationDetailTable.Rows.Count > 0) {
					//Use data values
					replaceTokenWithValue("<WorkStationID>", (!this.mLabelDS.WorkstationDetailTable[0].IsWorkStationIDNull() ? this.mLabelDS.WorkstationDetailTable[0].WorkStationID : ""));
					replaceTokenWithValue("<Name>", (!this.mLabelDS.WorkstationDetailTable[0].IsNameNull() ? this.mLabelDS.WorkstationDetailTable[0].Name : ""));
					replaceTokenWithValue("<Number>", (!this.mLabelDS.WorkstationDetailTable[0].IsNumberNull() ? this.mLabelDS.WorkstationDetailTable[0].Number : ""));
					replaceTokenWithValue("<stationNumber>", (!this.mLabelDS.WorkstationDetailTable[0].IsNumberNull() ? this.mLabelDS.WorkstationDetailTable[0].Number : ""));
					replaceTokenWithValue("<Description>", (!this.mLabelDS.WorkstationDetailTable[0].IsDescriptionNull() ? this.mLabelDS.WorkstationDetailTable[0].Description : ""));
				}
				else {
					//No data values; use defaults
					replaceTokenWithValue("<WorkStationID>", "");
					replaceTokenWithValue("<Name>", "");
					replaceTokenWithValue("<Number>", "");
					replaceTokenWithValue("<stationNumber>", "");
					replaceTokenWithValue("<Description>", "");
				}
			}
			catch(Exception) { /* Eat it */ }
		}
		private void convertClientTokens() {
			//Convert client-related tokens
			try {
				if(this.mLabelDS.ClientDetailTable.Rows.Count > 0) {
					//Use data values
					replaceTokenWithValue("<clientAbbreviation>", "");
					replaceTokenWithValue("<clientAddressCity>", "");
					replaceTokenWithValue("<clientAddressCountryCode>", "");
					replaceTokenWithValue("<clientAddressLine1>", "");
					replaceTokenWithValue("<clientAddressLine2>", "");
					replaceTokenWithValue("<clientAddressState>", "");
					replaceTokenWithValue("<clientAddressZip>", "");
					replaceTokenWithValue("<clientDivisionNumber>", (!this.mLabelDS.ClientDetailTable[0].IsDIVISIONNull() ? this.mLabelDS.ClientDetailTable[0].DIVISION : ""));
					replaceTokenWithValue("<clientName>", (!this.mLabelDS.ClientDetailTable[0].IsNAMENull() ? this.mLabelDS.ClientDetailTable[0].NAME.TrimEnd() : ""));
					replaceTokenWithValue("<clientNumber>", (!this.mLabelDS.ClientDetailTable[0].IsNUMBERNull() ? this.mLabelDS.ClientDetailTable[0].NUMBER : ""));
				}
				else {
					//No data values; use defaults
					replaceTokenWithValue("<clientAbbreviation>", "");
					replaceTokenWithValue("<clientAddressCity>", "");
					replaceTokenWithValue("<clientAddressCountryCode>", "");
					replaceTokenWithValue("<clientAddressLine1>", "");
					replaceTokenWithValue("<clientAddressLine2>", "");
					replaceTokenWithValue("<clientAddressState>", "");
					replaceTokenWithValue("<clientAddressZip>", "");
					replaceTokenWithValue("<clientDivisionNumber>", "");
					replaceTokenWithValue("<clientName>", "");
					replaceTokenWithValue("<clientNumber>", "");
				}
			}
			catch(Exception) { /* Eat it */ }
		}
		private void convertCartonTokens() {
			//Convert carton-related tokens
			try {
				if(this.mLabelDS.CartonDetailTable.Rows.Count > 0) {
					//Use data values
					replaceTokenWithValue("<cartonNumber>", "");
					replaceTokenWithValue("<cartonNumberOrPoNumber>", "");
					replaceTokenWithValue("<sortedItemLabelNumber>", (!this.mLabelDS.CartonDetailTable[0].IsLABEL_SEQUENCENUMBERNull() ? this.mLabelDS.CartonDetailTable[0].LABEL_SEQUENCENUMBER.TrimEnd() : ""));
					replaceTokenWithValue("<sortedItemWeightString>", "");
					replaceTokenWithValue("<itemDamageCode>", "");
					replaceTokenWithValue("<itemType>", "");
					replaceTokenWithValue("<poNumber>", "");
					replaceTokenWithValue("<returnNumber>", "");
				}
				else {
					//No data values; use defaults
					replaceTokenWithValue("<cartonNumber>", "");
					replaceTokenWithValue("<cartonNumberOrPoNumber>", "");
					replaceTokenWithValue("<sortedItemLabelNumber>", "");
					replaceTokenWithValue("<sortedItemWeightString>", "");
					replaceTokenWithValue("<itemDamageCode>", "");
					replaceTokenWithValue("<itemType>", "");
					replaceTokenWithValue("<poNumber>", "");
					replaceTokenWithValue("<returnNumber>", "");
				}
			}
			catch(Exception) { /* Eat it */ }
		}
		private void convertStoreTokens() {
			//Convert store-related tokens
			try { 
				string route="";
				replaceTokenWithValue("<storeCountryCode>", "");
				if(this.mLabelDS.StoreDetailTable.Rows.Count > 0) {
					//Use data values
					replaceTokenWithValue("<storeAddressLine1>", (!this.mLabelDS.StoreDetailTable[0].IsADDRESS_LINE1Null() ? this.mLabelDS.StoreDetailTable[0].ADDRESS_LINE1.TrimEnd() : ""));
					replaceTokenWithValue("<storeAddressLine2>", (!this.mLabelDS.StoreDetailTable[0].IsADDRESS_LINE2Null() ? this.mLabelDS.StoreDetailTable[0].ADDRESS_LINE2.TrimEnd() : ""));
					replaceTokenWithValue("<storeAddressCity>", (!this.mLabelDS.StoreDetailTable[0].IsCITYNull() ? this.mLabelDS.StoreDetailTable[0].CITY.TrimEnd() : ""));
					replaceTokenWithValue("<storeAddressState>", (!this.mLabelDS.StoreDetailTable[0].IsSTATENull() ? this.mLabelDS.StoreDetailTable[0].STATE.TrimEnd() : ""));
					replaceTokenWithValue("<storeAddressZip>", (!this.mLabelDS.StoreDetailTable[0].IsZIPNull() ? this.mLabelDS.StoreDetailTable[0].ZIP.TrimEnd() : ""));
					replaceTokenWithValue("<storeZip>", (!this.mLabelDS.StoreDetailTable[0].IsZIPNull() ? this.mLabelDS.StoreDetailTable[0].ZIP.TrimEnd() : ""));
					replaceTokenWithValue("<storeAltRoute>", (!this.mLabelDS.StoreDetailTable[0].IsALTROUTENull() ? this.mLabelDS.StoreDetailTable[0].ALTROUTE : ""));
					replaceTokenWithValue("<storeRoute>", (!this.mLabelDS.StoreDetailTable[0].IsROUTENull() ? this.mLabelDS.StoreDetailTable[0].ROUTE.TrimEnd() : ""));
					replaceTokenWithValue("<storeNumber>", (!this.mLabelDS.StoreDetailTable[0].IsNUMBERNull() ? this.mLabelDS.StoreDetailTable[0].NUMBER.ToString().PadLeft(5, '0') : ""));
					replaceTokenWithValue("<storeName>", (!this.mLabelDS.StoreDetailTable[0].IsNAMENull() ? this.mLabelDS.StoreDetailTable[0].NAME.TrimEnd() : ""));
					replaceTokenWithValue("<storePhone>", (!this.mLabelDS.StoreDetailTable[0].IsPHONENull() ? this.mLabelDS.StoreDetailTable[0].PHONE.TrimEnd() : ""));
					replaceTokenWithValue("<storeUserLabelData>", (!this.mLabelDS.StoreDetailTable[0].IsLBL_USER_DATANull() ? this.mLabelDS.StoreDetailTable[0].LBL_USER_DATA.TrimEnd() : ""));
					if(!this.mLabelDS.StoreDetailTable[0].IsROUTENull())
						route = this.mLabelDS.StoreDetailTable[0].ROUTE.TrimEnd().PadRight(5);
					replaceTokenWithValue("<storeRouteFirstCharacter>", (route!="" ? route.Substring(0,1) : ""));
					replaceTokenWithValue("<storeRouteFirstTwo>", (route!="" ? route.Substring(0,2) : ""));
					replaceTokenWithValue("<storeRouteLastFour>", (route!="" ? route.Substring(1,4) : ""));
					replaceTokenWithValue("<storeRouteLastThree>", (route!="" ? route.Substring(2,3) : ""));				
				}
				else {
					//No data values; use defaults
					replaceTokenWithValue("<storeAddressLine1>", "");
					replaceTokenWithValue("<storeAddressLine2>", "");
					replaceTokenWithValue("<storeAddressCity>", "");
					replaceTokenWithValue("<storeAddressState>", "");
					replaceTokenWithValue("<storeAddressZip>", "");
					replaceTokenWithValue("<storeZip>", "");
					replaceTokenWithValue("<storeAltRoute>", "");
					replaceTokenWithValue("<storeRoute>", "");
					replaceTokenWithValue("<storeNumber>", "");
					replaceTokenWithValue("<storeName>", "");
					replaceTokenWithValue("<storePhone>", "");
					replaceTokenWithValue("<storeUserLabelData>", "");
					replaceTokenWithValue("<storeRouteFirstCharacter>", "");
					replaceTokenWithValue("<storeRouteFirstTwo>", "");
					replaceTokenWithValue("<storeRouteLastFour>", "");
					replaceTokenWithValue("<storeRouteLastThree>", "");
				}
			}
			catch(Exception) { /* Eat it */ }
		}
		private void convertClientVendorTokens () {
			//Convert client-vendor-related tokens
			try { 
				if(this.mLabelDS.ClientVendorViewTable.Rows.Count > 0) {
					//Use data values
					replaceTokenWithValue("<vendorAddressLine1>", (!this.mLabelDS.ClientVendorViewTable[0].IsADDRESS_LINE1Null() ? this.mLabelDS.ClientVendorViewTable[0].ADDRESS_LINE1.TrimEnd() : ""));
					replaceTokenWithValue("<vendorAddressLine2>", (!this.mLabelDS.ClientVendorViewTable[0].IsADDRESS_LINE2Null() ? this.mLabelDS.ClientVendorViewTable[0].ADDRESS_LINE2.TrimEnd() : ""));
					replaceTokenWithValue("<vendorAddressCity>", (!this.mLabelDS.ClientVendorViewTable[0].IsCITYNull() ? this.mLabelDS.ClientVendorViewTable[0].CITY.TrimEnd() : ""));
					replaceTokenWithValue("<vendorAddressState>", (!this.mLabelDS.ClientVendorViewTable[0].IsSTATENull() ? this.mLabelDS.ClientVendorViewTable[0].STATE.TrimEnd() : ""));
					replaceTokenWithValue("<vendorAddressZip>", (!this.mLabelDS.ClientVendorViewTable[0].IsZIPNull() ? this.mLabelDS.ClientVendorViewTable[0].ZIP.TrimEnd() : ""));
					replaceTokenWithValue("<vendorNumber>", (!this.mLabelDS.ClientVendorViewTable[0].IsVENDOR_NUMBERNull() ? this.mLabelDS.ClientVendorViewTable[0].VENDOR_NUMBER.TrimEnd() : ""));
					replaceTokenWithValue("<vendorName>", (!this.mLabelDS.ClientVendorViewTable[0].IsNAMENull() ? this.mLabelDS.ClientVendorViewTable[0].NAME.TrimEnd() : ""));
					replaceTokenWithValue("<vendorUserData>", (!this.mLabelDS.ClientVendorViewTable[0].IsUSERDATANull() ? this.mLabelDS.ClientVendorViewTable[0].USERDATA.TrimEnd() : ""));
				}
				else {
					//No data values; use defaults
					replaceTokenWithValue("<vendorAddressLine1>", "");
					replaceTokenWithValue("<vendorAddressLine2>", "");
					replaceTokenWithValue("<vendorAddressCity>", "");
					replaceTokenWithValue("<vendorAddressState>", "");
					replaceTokenWithValue("<vendorAddressZip>", "");
					replaceTokenWithValue("<vendorNumber>", "");
					replaceTokenWithValue("<vendorName>", "");
					replaceTokenWithValue("<vendorUserData>", "");
				}
			}
			catch(Exception) { /* Eat it */ }
		}
		private void convertZoneTokens() {
			//Convert zone-related tokens
			try { 
				if(this.mLabelDS.ZoneDetailTable.Rows.Count > 0) {
					//Use data values
					replaceTokenWithValue("<zoneCode>", (!this.mLabelDS.ZoneDetailTable[0].IsCODENull() ? this.mLabelDS.ZoneDetailTable[0].CODE.TrimEnd() : ""));
					replaceTokenWithValue("<zoneLabelType>", (!this.mLabelDS.ZoneDetailTable[0].IsLABELTYPENull() ? this.mLabelDS.ZoneDetailTable[0].LABELTYPE.TrimEnd() : ""));
					replaceTokenWithValue("<zoneLane>", (!this.mLabelDS.ZoneDetailTable[0].IsLANENull() ? this.mLabelDS.ZoneDetailTable[0].LANE.TrimEnd() : ""));
					replaceTokenWithValue("<zoneLaneSmallSort>", (!this.mLabelDS.ZoneDetailTable[0].IsLANE_SMALL_SORTNull() ? this.mLabelDS.ZoneDetailTable[0].LANE_SMALL_SORT.TrimEnd() : ""));
					replaceTokenWithValue("<zoneOutboundTrailerLoadNumber>", (!this.mLabelDS.ZoneDetailTable[0].IsTRAILER_LOAD_NUMNull() ? this.mLabelDS.ZoneDetailTable[0].TRAILER_LOAD_NUM.TrimEnd() : ""));
					replaceTokenWithValue("<zoneOutboundTrailerLoadNumberDigitsOnly>", "");
				}
				else {
					//No data values; use defaults
					replaceTokenWithValue("<zoneCode>", "");
					replaceTokenWithValue("<zoneLabelType>", "");
					replaceTokenWithValue("<zoneLane>", "");
					replaceTokenWithValue("<zoneLaneSmallSort>", "");
					replaceTokenWithValue("<zoneOutboundTrailerLoadNumber>", "");
					replaceTokenWithValue("<zoneOutboundTrailerLoadNumberDigitsOnly>", "");
				}
			}
			catch(Exception) { /* Eat it */ }
		}

		private void replaceTokenWithValue(string token, string value) { 
			//Replace token value with data value
			try {
				this.mLabelFormat.Replace(token, value); 
			}
			catch(Exception) { /* Eat it */ }
		}
		#endregion
	}
}
