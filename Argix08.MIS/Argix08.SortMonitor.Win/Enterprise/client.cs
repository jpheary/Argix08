//	File:	client.cs
//	Author:	J. Heary
//	Date:	02/27/07
//	Desc:	Represents the state and behavior of an Argix client.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;

namespace Tsort.Enterprise {
	//
	public class Client {
		//Members
		private int _clientid=0;
		private string _clientnumber="";
		private string _number="";
		private string _division="";
		private string _name="";
		private string _status="";
		private string _ups_shipper_nbr="";
		private string _abbreviation="";
		private string _address_line1="";
		private string _address_line2="";
		private string _city="";
		private string _state="";
		private string _zip="";
		private string _zip4="";
		private string _delivery_bill_type="";
		private string _carton_commodity="";
		private string _delivery_bill="";
		private short _dbill_copies=0;
		private string _issan="";
		private string _invoiceprogram="";
		private string _contactname="";
		private string _phone="";
		private string _fax="";
		private string _mnemonic="";
		private string _blnumberoninvoice="";
		private string _arnumber="";
		private string _pickupzip="";
		private string _manifestpertrailer="";
		private DateTime _lastupdated=DateTime.Now;
		private string _userid="";
		
		//Constants
		//Events
		//Interface
		public Client(): this(null) { }
		public Client(ClientDS.ClientDetailTableRow client) { 
			//Constructor
			try { 
				if(client != null) { 
					if(!client.IsClientIDNull()) this._clientid = client.ClientID;
					if(!client.IsClientNumberNull()) this._clientnumber = client.ClientNumber;
					this._number = client.NUMBER;
					this._division = client.DIVISION;
					if(!client.IsNAMENull()) this._name = client.NAME;
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
			catch(Exception ex) { throw new ApplicationException("Unexpected exception creating new client instance.", ex); }
		}
		public Client(string number, string division, string name, string addressLine1, string addressLine2, string addressCity, string addressState, string addressZip) { 
			//Constructor
			try { 
				this._number = number;
				this._division = division;
				this._name = name;
				this._address_line1 = addressLine1;
				this._address_line2 = addressLine2;
				this._city = addressCity;
				this._state = addressState;
				this._zip = addressZip;
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected exception creating new client instance.", ex); }
		}
		#region Accessors\Modifiers: [Members]..., ToDataSet()
		public int ClientID { get { return this._clientid; } }
		public string Number { get { return this._number; } }
		public string Division { get { return this._division; } }
		public string Name { get { return this._name; } }
		public string STATUS { get { return this._status; } }
		public string UPS_SHIPPER_NBR { get { return this._ups_shipper_nbr; } }
		public string ABBREVIATION { get { return this._abbreviation; } }
		public string AddressLine1 { get { return this._address_line1; } }
		public string AddressLine2 { get { return this._address_line2; } }
		public string City { get { return this._city; } }
		public string State { get { return this._state; } }
		public string ZIP { get { return this._zip; } }
		public string ZIP4 { get { return this._zip4; } }
		public string DeliveryBillType { get { return this._delivery_bill_type; } }
		public string CartonCommodity { get { return this._carton_commodity; } }
		public string DeliveryBill { get { return this._delivery_bill; } }
		public short DbillCopies { get { return this._dbill_copies; } }
		public string IsSan { get { return this._issan; } }
		public string InvoiceProgram { get { return this._invoiceprogram; } }
		public string ContactName { get { return this._contactname; } }
		public string Phone { get { return this._phone; } }
		public string Fax { get { return this._fax; } }
		public string Mnemonic { get { return this._mnemonic; } }
		public string BLNumberOnInvoice { get { return this._blnumberoninvoice; } }
		public string ARNumber { get { return this._arnumber; } }
		public string PickupZip { get { return this._pickupzip; } }
		public string ManifestPerTrailer { get { return this._manifestpertrailer; } }
		public DateTime LastUpdated { get { return this._lastupdated; } }
		public string UserID { get { return this._userid; } }
		public DataSet ToDataSet() { 
			//Return a dataset containing values for this object
			ClientDS ds=null;
			try { 
				ds = new ClientDS();
				ClientDS.ClientDetailTableRow client = ds.ClientDetailTable.NewClientDetailTableRow();
				if(this._clientid > 0) client.ClientID = this._clientid;
				if(this._clientnumber.Length > 0) client.ClientNumber = this._clientnumber;
				if(this._clientnumber.Length > 0) client.NUMBER = this._number;
				if(this._clientnumber.Length > 0) client.DIVISION = this._division;
				if(this._name.Length > 0) client.NAME = this._name;
				if(this._status.Length > 0) client.STATUS = this._status;
				if(this._ups_shipper_nbr.Length > 0) client.UPS_SHIPPER_NBR = this._ups_shipper_nbr;
				if(this._abbreviation.Length > 0) client.ABBREVIATION = this._abbreviation;
				if(this._address_line1.Length > 0) client.ADDRESS_LINE1 = this._address_line1;
				if(this._address_line2.Length > 0) client.ADDRESS_LINE2 = this._address_line2;
				if(this._city.Length > 0) client.CITY = this._city;
				if(this._state.Length > 0) client.STATE = this._state;
				if(this._zip.Length > 0) client.ZIP = this._zip;
				if(this._zip4.Length > 0) client.ZIP4 = this._zip4;
				if(this._delivery_bill_type.Length > 0) client.DELIVERY_BILL_TYPE = this._delivery_bill_type;
				if(this._carton_commodity.Length > 0) client.CARTON_COMMODITY = this._carton_commodity;
				if(this._delivery_bill.Length > 0) client.DELIVERY_BILL_TYPE = this._delivery_bill;
				if(this._dbill_copies > 0) client.DBILL_COPIES = this._dbill_copies;
				if(this._issan.Length > 0) client.IsSan = this._issan;
				if(this._invoiceprogram.Length > 0) client.InvoiceProgram = this._invoiceprogram;
				if(this._contactname.Length > 0) client.ContactName = this._contactname;
				if(this._phone.Length > 0) client.Phone = this._phone;
				if(this._fax.Length > 0) client.Fax = this._fax;
				if(this._mnemonic.Length > 0) client.Mnemonic = this._mnemonic;
				if(this._blnumberoninvoice.Length > 0) client.BLNumberOnInvoice = this._blnumberoninvoice;
				if(this._arnumber.Length > 0) client.ARNumber = this._arnumber;
				if(this._pickupzip.Length > 0) client.PickupZip = this._pickupzip;
				if(this._manifestpertrailer.Length > 0) client.ManifestPerTrailer = this._manifestpertrailer;
				client.LastUpdated = this._lastupdated;
				client.UserID = this._userid;
				ds.ClientDetailTable.AddClientDetailTableRow(client);
				ds.AcceptChanges();
			}
			catch(Exception) { }
			return ds;
		}
		public bool IsSameAs (Client aClient){
			return this.Number == aClient.Number && this.Division == aClient.Division;
		}
		#endregion
	}
}