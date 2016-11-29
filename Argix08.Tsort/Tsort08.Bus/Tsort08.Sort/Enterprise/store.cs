//	File:	store.cs
//	Author:	J. Heary
//	Date:	02/27/07
//	Desc:	Represents the state and behavior of an Argix store.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;

namespace Tsort.Enterprise {
	//
	public class Store {
		//Members
		private string _client_number="";
		private string _client_division="";
		private int _number=0;
		private string _name="";
		private string _address_line1="";
		private string _address_line2="";
		private string _city="";
		private string _state="";
		private string _zip="";
		private string _zip4="";
		private string _zone="";
		private string _zone_type="";
		private string _status="";
		private DateTime _open_date=DateTime.Now;
		private string _route="";
		private string _lbl_user_data="";
		private string _san_number="";
		private string _phone="";
		private string _instructions="";
		private string _labeltype="";
		private string _alt_number="";
		private string _altroute="";
		private string _local_lane="";
		
		//Constants
		public const string STATUS_ACTIVE = "A";
		public const string STATUS_INACTIVE = "I";
		
		//Events
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
			catch(Exception ex) { throw new ApplicationException("Unexpected exception creating new store instance.", ex); }
		}
		#region Accessors\Modifiers: [Members]..., ToDataSet()
		public string CLIENT_NUMBER { get { return this._client_number; } }
		public string CLIENT_DIVISION { get { return this._client_division; } }
		public int NUMBER { get { return this._number; } }
		public string NAME { get { return this._name; } }
		public string ADDRESS_LINE1 { get { return this._address_line1; } }
		public string ADDRESS_LINE2 {  get { return this._address_line2; } }
		public string CITY { get { return this._city; } }
		public string STATE { get { return this._state; } }
		public string ZIP { get { return this._zip; } }
		public string ZIP4 { get { return this._zip4; } }
		public string ZONE { get { return this._zone; } }
		public string ZONE_TYPE { get { return this._zone_type; } }
		public string STATUS { get { return this._status; } }
		public DateTime OPEN_DATE { get { return this._open_date; } }
		public string ROUTE { get { return this._route; } }
		public string LBL_USER_DATA { get { return this._lbl_user_data; } }
		public string SAN_NUMBER { get { return this._san_number; } }
		public string PHONE { get { return this._phone; } }
		public string INSTRUCTIONS { get { return this._instructions; } }
		public string LABELTYPE { get { return this._labeltype; } }
		public string ALT_NUMBER { get { return this._alt_number; } }
		public string ALTROUTE { get { return this._altroute; } }
		public string LOCAL_LANE { get { return this._local_lane; } }
		public DataSet ToDataSet() { 
			//Return a dataset containing values for this object
			StoreDS ds=null;
			try { 
				ds = new StoreDS();
				StoreDS.StoreDetailTableRow store = ds.StoreDetailTable.NewStoreDetailTableRow();
				store.CLIENT_NUMBER = this._client_number;
				store.CLIENT_DIVISION = this._client_division;
				store.NUMBER = this._number;
				store.NAME = this._name;
				store.ADDRESS_LINE1 = this._address_line1;
				store.ADDRESS_LINE2 = this._address_line2;
				store.CITY = this._city;
				store.STATE = this._state;
				store.ZIP = this._zip;
				store.ZIP4 = this._zip4;
				store.ZONE = this._zone;
				store.ZONE_TYPE = this._zone_type;
				store.STATUS = this._status;
				store.OPEN_DATE = this._open_date;
				store.ROUTE = this._route;
				store.LBL_USER_DATA = this._lbl_user_data;
				store.SAN_NUMBER = this._san_number;
				store.PHONE = this._phone;
				store.INSTRUCTIONS = this._instructions;
				store.LABELTYPE = this._labeltype;
				store.ALT_NUMBER = this._alt_number;
				store.ALTROUTE = this._altroute;
				store.LOCAL_LANE = this._local_lane;
				ds.StoreDetailTable.AddStoreDetailTableRow(store);
			}
			catch(Exception) { }
			return ds;
		}
		#endregion
		public bool IsActive { get { return (this._status.ToUpper() == STATUS_ACTIVE); } }
	}
}