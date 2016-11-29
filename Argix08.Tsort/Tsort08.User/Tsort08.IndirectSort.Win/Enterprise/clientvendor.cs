using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Text;

namespace Argix.Enterprise {
	//
	public class ClientVendor {
		//Members
		private string _client_number="", _client_div_num="";
		private string _vendor_number="";
		private string _zone_code="", _zone_type="";
		private string _non_san_label="", _san_label="";
		private string _name="", _address_line1="", _address_line2="", _city="", _state="", _zip="", _zip4="";
		private string _status="", _userdata="";
		private string _clients_vendor="";
		private string _local_lane="", _route="";
				
		//Interface
		public ClientVendor(): this(null) { }
		public ClientVendor(ClientVendorDS.ClientVendorViewTableRow clientvendor) { 
			//Constructor
			try { 
				if(clientvendor != null) { 
					if(!clientvendor.IsCLIENT_NUMBERNull()) this._client_number = clientvendor.CLIENT_NUMBER;
					if(!clientvendor.IsCLIENT_DIV_NUMNull()) this._client_div_num = clientvendor.CLIENT_DIV_NUM;
					if(!clientvendor.IsVENDOR_NUMBERNull()) this._vendor_number = clientvendor.VENDOR_NUMBER;
					if(!clientvendor.IsZONE_CODENull()) this._zone_code = clientvendor.ZONE_CODE;
					if(!clientvendor.IsZONE_TYPENull()) this._zone_type = clientvendor.ZONE_TYPE;
					if(!clientvendor.IsNON_SAN_LABELNull()) this._non_san_label = clientvendor.NON_SAN_LABEL;
					if(!clientvendor.IsSAN_LABELNull()) this._san_label = clientvendor.SAN_LABEL;
					if(!clientvendor.IsNAMENull()) this._name = clientvendor.NAME;
					if(!clientvendor.IsADDRESS_LINE1Null()) this._address_line1 = clientvendor.ADDRESS_LINE1;
					if(!clientvendor.IsADDRESS_LINE2Null()) this._address_line2 = clientvendor.ADDRESS_LINE2;
					if(!clientvendor.IsCITYNull()) this._city = clientvendor.CITY;
					if(!clientvendor.IsSTATENull()) this._state = clientvendor.STATE;
					if(!clientvendor.IsZIPNull()) this._zip = clientvendor.ZIP;
					if(!clientvendor.IsZIP4Null()) this._zip4 = clientvendor.ZIP4;
					if(!clientvendor.IsSTATUSNull()) this._status = clientvendor.STATUS;
					if(!clientvendor.IsUSERDATANull()) this._userdata = clientvendor.USERDATA;
					if(!clientvendor.IsCLIENTS_VENDORNull()) this._clients_vendor = clientvendor.CLIENTS_VENDOR;
					if(!clientvendor.IsLOCAL_LANENull()) this._local_lane = clientvendor.LOCAL_LANE;
					if(!clientvendor.IsROUTENull()) this._route = clientvendor.ROUTE;
				}
			}
			catch(Exception ex) { throw ex; }
        }
        #region Accessors\Modifiers: [Members...]
        public string CLIENT_NUMBER { 
			get { return this._client_number; }
			set { this._client_number = value; }
		}
		public string CLIENT_DIV_NUM { 
			get { return this._client_div_num; }
			set { this._client_div_num = value; }
		}
		public string VENDOR_NUMBER { 
			get { return this._vendor_number; }
			set { this._vendor_number = value; }
		}
		public string ZONE_CODE { 
			get { return this._zone_code; }
			set { this._zone_code = value; }
		}
		public string ZONE_TYPE { 
			get { return this._zone_type; }
			set { this._zone_type = value; }
		}
		public string NON_SAN_LABEL { 
			get { return this._non_san_label; }
			set { this._non_san_label = value; }
		}
		public string SAN_LABEL { 
			get { return this._san_label; }
			set { this._san_label = value; }
		}
		public string NAME { 
			get { return this._name; }
			set { this._name = value; }
		}
		public string ADDRESS_LINE1 { 
			get { return this._address_line1; }
			set { this._address_line1 = value; }
		}
		public string ADDRESS_LINE2 { 
			get { return this._address_line2; }
			set { this._address_line2 = value; }
		}
		public string CITY { 
			get { return this._city; }
			set { this._city = value; }
		}
		public string STATE { 
			get { return this._state; }
			set { this._state = value; }
		}
		public string ZIP { 
			get { return this._zip; }
			set { this._zip = value; }
		}
		public string ZIP4 { 
			get { return this._zip4; }
			set { this._zip4 = value; }
		}
		public string STATUS { 
			get { return this._status; }
			set { this._status = value; }
		}
		public string USERDATA { 
			get { return this._userdata; }
			set { this._userdata = value; }
		}
		public string CLIENTS_VENDOR { 
			get { return this._clients_vendor; }
			set { this._clients_vendor = value; }
		}
		public string LOCAL_LANE { 
			get { return this._local_lane; }
			set { this._local_lane = value; }
		}
		public string ROUTE { 
			get { return this._route; }
			set { this._route = value; }
		}
		#endregion
	}
}
