using System;
using System.Data;
using System.Configuration;
using System.Text;

namespace Argix.Enterprise {
	//
	public class Client {
		//Members
		private int _clientid;
		private string _number="", _division="";
		private string _name="", _status="";
		private string _ups_shipper_nbr="", _abbreviation="";
		private string _address_line1="", _address_line2="", _city="", _state="", _zip="", _zip4="";
		private string _delivery_bill_type="";
		private string _carton_commodity="",  _delivery_bill="";
		private int _dbill_copies=0;
		private string _issan="", _invoiceprogram="";
		private string _contactname="", _phone="", _fax="";
		private string _mnemonic="", _blnumberoninvoice="", _arnumber="", _pickupzip="",  _manifestpertrailer="";
		private DateTime _lastupdated=DateTime.Now;
		private string _userid="";
				
		//Interface
		public Client(): this(null) { }
		public Client(ClientDS.ClientDetailTableRow client) { 
			//Constructor
			try { 
				if(client != null) { 
					if(!client.IsClientIDNull()) this._clientid = client.ClientID;
					if(!client.IsNUMBERNull()) this._number = client.NUMBER;
					if(!client.IsDIVISIONNull()) this._division = client.DIVISION;
					if(!client.IsNAMENull()) this._name = client.NAME.TrimEnd();
					if(!client.IsSTATUSNull()) this._status = client.STATUS;
					if(!client.IsUPS_SHIPPER_NBRNull()) this._ups_shipper_nbr = client.UPS_SHIPPER_NBR;
					if(!client.IsABBREVIATIONNull()) this._abbreviation = client.ABBREVIATION;
					if(!client.IsADDRESS_LINE1Null()) this._address_line1 = client.ADDRESS_LINE1;
					if(!client.IsADDRESS_LINE2Null()) this._address_line2 = client.ADDRESS_LINE2;
					if(!client.IsCITYNull()) this._city = client.CITY;
					if(!client.IsSTATENull()) this._state = client.STATE;
					if(!client.IsZIPNull()) this._zip = client.ZIP;
					if(!client.IsZIP4Null()) this._zip4 = client.ZIP4;
					if(!client.IsDELIVERY_BILL_TYPENull()) this._delivery_bill_type = client.DELIVERY_BILL_TYPE;
					if(!client.IsCARTON_COMMODITYNull()) this._carton_commodity = client.CARTON_COMMODITY;
					if(!client.IsDELIVERY_BILLNull()) this._delivery_bill = client.DELIVERY_BILL;
					if(!client.IsDBILL_COPIESNull()) this._dbill_copies = client.DBILL_COPIES;
					if(!client.IsIsSanNull()) this._issan = client.IsSan;
					if(!client.IsInvoiceProgramNull()) this._invoiceprogram = client.InvoiceProgram;
					if(!client.IsContactNameNull()) this._contactname = client.ContactName;
					if(!client.IsPhoneNull()) this._phone = client.Phone;
					if(!client.IsFaxNull()) this._fax = client.Fax;
					if(!client.IsMnemonicNull()) this._mnemonic = client.Mnemonic;
					if(!client.IsBLNumberOnInvoiceNull()) this._blnumberoninvoice = client.BLNumberOnInvoice;
					if(!client.IsARNumberNull()) this._arnumber = client.ARNumber;
					if(!client.IsPickupZipNull()) this._pickupzip = client.PickupZip;
					if(!client.IsManifestPerTrailerNull()) this._manifestpertrailer = client.ManifestPerTrailer;
					if(!client.IsLastUpdatedNull()) this._lastupdated = client.LastUpdated;
					if(!client.IsUserIDNull()) this._userid = client.UserID;
				}
			}
            catch (Exception ex) { throw new ApplicationException("Unexpected error creating new Client instance.", ex); }
        }
		public Client(string number, string division, string name) {
			//Constructor
			try { 
				this._number = number;
				this._division = division;
				this._name = name;
			}
            catch (Exception ex) { throw new ApplicationException("Unexpected error creating new Client instance.", ex); }
        }
		#region Accessors\Modifiers: Members[...]
		public int ClientID { get { return this._clientid; } }
		public string Number { get { return this._number; } set { this._number = value; } }
		public string Division { get { return this._division; } set { this._division = value; } }
		public string Name { get { return this._name; } set { this._name = value; } }
		public string STATUS { get { return this._status; } set { this._status = value; } }
		public string UPS_SHIPPER_NBR { get { return this._ups_shipper_nbr; } set { this._ups_shipper_nbr = value; } }
		public string ABBREVIATION { get { return this._abbreviation; } set { this._abbreviation = value; } }
		public string AddressLine1 { get { return this._address_line1; } set { this._address_line1 = value; } }
		public string AddressLine2 { get { return this._address_line2; } set { this._address_line2 = value; } }
		public string City { get { return this._city; } set { this._city = value; } }
		public string State { get { return this._state; } set { this._state = value; } }
		public string ZIP { get { return this._zip; } set { this._zip = value; } }
		public string ZIP4 { get { return this._zip4; } set { this._zip4 = value; } }
		public string DeliveryBillType { get { return this._delivery_bill_type; } set { this._delivery_bill_type = value; } }
		public string CartonCommodity { get { return this._carton_commodity; } set { this._carton_commodity = value; } }
		public string DeliveryBill { get { return this._delivery_bill; } set { this._delivery_bill = value; } }
		public int DbillCopies { get { return this._dbill_copies; } set { this._dbill_copies = value; } }
		public string IsSan { get { return this._issan; } set { this._issan = value; } }
		public string InvoiceProgram { get { return this._invoiceprogram; } set { this._invoiceprogram = value; } }
		public string ContactName { get { return this._contactname; } set { this._contactname = value; } }
		public string Phone { get { return this._phone; } set { this._phone = value; } }
		public string Fax { get { return this._fax; } set { this._fax = value; } }
		public string Mnemonic { get { return this._mnemonic; } set { this._mnemonic = value; } }
		public string BLNumberOnInvoice { get { return this._blnumberoninvoice; } set { this._blnumberoninvoice = value; } }
		public string ARNumber { get { return this._arnumber; } set { this._arnumber = value; } }
		public string PickupZip { get { return this._pickupzip; } set { this._pickupzip = value; } }
		public string ManifestPerTrailer { get { return this._manifestpertrailer; } set { this._manifestpertrailer = value; } }
		public DateTime LastUpdated { get { return this._lastupdated; } set { this._lastupdated = value; } }
		public string UserID { get { return this._userid; } set { this._userid = value; } }
		#endregion
	}
}