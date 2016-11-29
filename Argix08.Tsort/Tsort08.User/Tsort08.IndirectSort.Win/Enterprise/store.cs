using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Text;

namespace Argix.Enterprise {
	//
	public class Store {
		//Members
		private string _client_number="", _client_division="";
		private int _number=0;
		private string _name="";
		private string _address_line1="", _address_line2="", _city="",_state="",_zip="",_zip4="";
		private string _zone="",_zone_type="",_status="";
		private DateTime _open_date=DateTime.Now;
		private string _route="",_lbl_user_data="",_san_number="";
		private string _phone="",_instructions="",_labeltype="";
		private string _alt_number="", _altroute="",_local_lane="";
		
		public const string STORE_STATUS_ACTIVE = "A";
		public const string STORE_STATUS_INACTIVE = "I";
		
		//Interface
		public Store(): this(null) { }
		public Store(StoreDS.StoreDetailTableRow store) { 
			//Constructor
			try { 
				if(store != null) { 
					if(!store.IsCLIENT_NUMBERNull()) this._client_number = store.CLIENT_NUMBER;
					if(!store.IsCLIENT_DIVISIONNull()) this._client_division = store.CLIENT_DIVISION;
					if(!store.IsNUMBERNull()) this._number = store.NUMBER;
					if(!store.IsNAMENull()) this._name = store.NAME;
					if(!store.IsADDRESS_LINE1Null()) this._address_line1 = store.ADDRESS_LINE1;
					if(!store.IsADDRESS_LINE2Null()) this._address_line2 = store.ADDRESS_LINE2;
					if(!store.IsCITYNull()) this._city = store.CITY;
					if(!store.IsSTATENull()) this._state = store.STATE;
					if(!store.IsZIPNull()) this._zip = store.ZIP;
					if(!store.IsZIP4Null()) this._zip4 = store.ZIP4;
					if(!store.IsZONENull()) this._zone = store.ZONE;
					if(!store.IsZONE_TYPENull()) this._zone_type = store.ZONE_TYPE;
					if(!store.IsSTATUSNull()) this._status = store.STATUS;
					if(!store.IsOPEN_DATENull()) this._open_date = store.OPEN_DATE;
					if(!store.IsROUTENull()) this._route = store.ROUTE;
					if(!store.IsLBL_USER_DATANull()) this._lbl_user_data = store.LBL_USER_DATA;
					if(!store.IsSAN_NUMBERNull()) this._san_number = store.SAN_NUMBER;
					if(!store.IsPHONENull()) this._phone = store.PHONE;
					if(!store.IsINSTRUCTIONSNull()) this._instructions = store.INSTRUCTIONS;
					if(!store.IsLABELTYPENull()) this._labeltype = store.LABELTYPE;
					if(!store.IsALT_NUMBERNull()) this._alt_number = store.ALT_NUMBER;
					if(!store.IsALTROUTENull()) this._altroute = store.ALTROUTE;
					if(!store.IsLOCAL_LANENull()) this._local_lane = store.LOCAL_LANE;
				}
			}
			catch(Exception ex) { throw ex; }
		}
		public Store(string clientNumber, string clientDivision, int number, string name) {
			//Constructor
			try { 
				this._client_number = clientNumber;
				this._client_division = clientDivision;
				this._number = number;
				this._name = name;
			}
			catch(Exception ex) { throw ex; }
		}
		#region Accessors\Modifiers: Members[...]
		public string ClientNumber { get { return this._client_number; } set { this._client_number = value; } }
		public string ClientDivision { get { return this._client_division; } set { this._client_division = value; } }
		public int Number { get { return this._number; } set { this._number = value; } }
		public string Name { get { return this._name; } set { this._name = value; } }
		public string AddressLine1 { get { return this._address_line1; } set { this._address_line1 = value; } }
        public string AddressLine2 { get { return this._address_line2; } set { this._address_line2 = value; } }
		public string City { get { return this._city; } set { this._city = value; } }
		public string State { get { return this._state; } set { this._state = value; } }
		public string Zip { get { return this._zip; } set { this._zip = value; } }
		public string Zip4 { get { return this._zip4; } set { this._zip4 = value; } }
		public string Zone { get { return this._zone; } set { this._zone = value; } }
		public string ZoneType { get { return this._zone_type; } set { this._zone_type = value; } }
		public string Status { get { return this._status; } set { this._status = value; } }
		public DateTime OpenDate { get { return this._open_date; } set { this._open_date = value; } }
		public string Route { get { return this._route; } set { this._route = value; } }
		public string LabelUserData { get { return this._lbl_user_data; } set { this._lbl_user_data = value; } }
		public string SANNumber { get { return this._san_number; } set { this._san_number = value; } }
		public string Phone { get { return this._phone; } set { this._phone = value; } }
		public string Instructions { get { return this._instructions; } set { this._instructions = value; } }
		public string LabelType { get { return this._labeltype; } set { this._labeltype = value; } }
		public string AltNumber { get { return this._alt_number; } set { this._alt_number = value; } }
		public string AltRoute { get { return this._altroute; } set { this._altroute = value; } }
		public string LocalLane { get { return this._local_lane; } set { this._local_lane = value; } }
		#endregion
		public bool IsActive { get { return (this._status.ToUpper() == STORE_STATUS_ACTIVE); } }
	}
}