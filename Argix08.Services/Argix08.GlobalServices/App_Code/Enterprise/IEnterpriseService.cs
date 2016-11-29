using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Enterprise {
    //Enterprise Interfaces
    [ServiceContract(Namespace="http://Argix.Enterprise")]
    public interface IEnterpriseService {
        //General Interface
        [OperationContract]
        [FaultContractAttribute(typeof(EnterpriseFault),Action="http://Argix.Enterprise.EnterpriseFault")]
        Argix.Enterprise.TerminalInfo GetTerminalInfo();

        [OperationContract]
        Terminals GetTerminals();
    }

    [DataContract]
    public class TerminalInfo {
        private int mTerminalID=0;
        private string mNumber="",mDescription="",mConnection="";

        [DataMember]
        public int TerminalID { get { return this.mTerminalID; } set { this.mTerminalID = value; } }
        [DataMember]
        public string Number { get { return this.mNumber; } set { this.mNumber = value; } }
        [DataMember]
        public string Description { get { return this.mDescription; } set { this.mDescription = value; } }
        [DataMember]
        public string Connection { get { return this.mConnection; } set { this.mConnection = value; } }
    }

    [CollectionDataContract]
    public class Terminals:BindingList<Terminal> {
        public Terminals() { }
    }
    
    [DataContract]
    public class Terminal {
        //Members
        private int _terminalID=0;
        private string _number = "",_description = "";
        private string _dbservername="",_linkedservername="",_dbtype="";
        private int _agentID = 0;
        private string _agentnumber="",_shipperid="", _clientdivision="", _mnemonic="";
        private long _locationID=0;
        private byte _isactive = (byte)1;

        //Interface
        public Terminal() : this(null) { }
        public Terminal(TerminalDS.TerminalTableRow terminal) {
            //Constructor
            if(terminal != null) {
                if(!terminal.IsTerminalIDNull()) this._terminalID = terminal.TerminalID;
                if(!terminal.IsNumberNull()) this._number = terminal.Number;
                if(!terminal.IsDescriptionNull()) this._description = terminal.Description;
                if(!terminal.IsDBServerNameNull()) this._dbservername = terminal.DBServerName;
                if(!terminal.IsLinkedServerNameNull()) this._linkedservername = terminal.LinkedServerName;
                if(!terminal.IsDBTypeNull()) this._dbtype = terminal.DBType;
                if(!terminal.IsAgentIDNull()) this._agentID = terminal.AgentID;
                if(!terminal.IsAgentNumberNull()) this._agentnumber = terminal.AgentNumber;
                if(!terminal.IsShipperIDNull()) this._shipperid = terminal.ShipperID;
                if(!terminal.IsClientDivisionNull()) this._clientdivision = terminal.ClientDivision;
                if(!terminal.IsLocationIDNull()) this._locationID = terminal.LocationID;
                if(!terminal.IsMnemonicNull()) this._mnemonic = terminal.Mnemonic;
                if(!terminal.IsIsActiveNull()) this._isactive = terminal.IsActive;
            }
        }
        public Terminal(int terminalID,string number,string description,int agentID,string agentNumber) {
            //Constructor
            this._terminalID = terminalID;
            this._number = number;
            this._description = description;
            this._agentID = agentID;
            this._agentnumber = agentNumber;
        }
        public Terminal(long locationID,string description) {
            //Constructor
            this._locationID = locationID;
            this._description = description;
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int TerminalID { get { return this._terminalID; } set { this._terminalID = value; } }
        [DataMember]
        public string Number { get { return this._number; } set { this._number = value; } }
        [DataMember]
        public string Description { get { return this._description; } set { this._description = value; } }
        [DataMember]
        public string DBServerName { get { return this._dbservername; } set { this._dbservername = value; } }
        [DataMember]
        public string LinkedServerName { get { return this._linkedservername; } set { this._linkedservername = value; } }
        [DataMember]
        public string DBtype { get { return this._dbtype; } set { this._dbtype = value; } }
        [DataMember]
        public int AgentID { get { return this._agentID; } set { this._agentID = value; } }
        [DataMember]
        public string AgentNumber { get { return this._agentnumber; } set { this._agentnumber = value; } }
        [DataMember]
        public string ShipperID { get { return this._shipperid; } set { this._shipperid = value; } }
        [DataMember]
        public string ClientDivision { get { return this._clientdivision; } set { this._clientdivision = value; } }
        [DataMember]
        public long LocationID { get { return this._locationID; } set { this._locationID = value; } }
        [DataMember]
        public string Mnemonic { get { return this._mnemonic; } set { this._mnemonic = value; } }
        [DataMember]
        public byte IsActive { get { return this._isactive; } set { this._isactive = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class Agents:BindingList<Agent> {
        public Agents() { }
    }

    [DataContract]
    public class Agent:Shipper {
        //Members
        private int _agentid=0;
        private string _contactname="", _phone="", _fax="";
        private string _mnemonic="", _apnumber="", _transmitebol="";
        private byte _deliveryscanstatus=0;
        private int _parentid=0;
        private DateTime _lastupdated=DateTime.Now;
        private string _userid="";

        //Interface
        public Agent() : this(null) { }
        public Agent(AgentDS.AgentTableRow agent): base(agent) {
            //Constructor
            try {
                if(agent != null) {
                    if(!agent.IsAgentIDNull()) this._agentid = agent.AgentID;
                    if(!agent.IsContactNameNull()) this._contactname = agent.ContactName;
                    if(!agent.IsPhoneNull()) this._phone = agent.Phone;
                    if(!agent.IsFaxNull()) this._fax = agent.Fax;
                    if(!agent.IsMnemonicNull()) this._mnemonic = agent.Mnemonic;
                    if(!agent.IsAPNumberNull()) this._apnumber = agent.APNumber;
                    if(!agent.IsTransmitEBOLNull()) this._transmitebol = agent.TransmitEBOL;
                    if(!agent.IsDeliveryScanStatusNull()) this._deliveryscanstatus = agent.DeliveryScanStatus;
                    if(!agent.IsParentIDNull()) this._parentid = agent.ParentID;
                    if(!agent.IsLastUpdatedNull()) this._lastupdated = agent.LastUpdated;
                    if(!agent.IsUserIDNull()) this._userid = agent.UserID;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating new agent instance.",ex); }
        }
        public Agent(string number,string name,string addressLine1,string addressLine2,string addressCity,string addressState,string addressZip) : base(number,name,addressLine1,addressLine2,addressCity,addressState,addressZip,"") { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int AgentID { get { return this._agentid; } set { this._agentid = value; } }
        [DataMember]
        public string ContactName { get { return this._contactname; } set { this._contactname = value; } }
        [DataMember]
        public string Phone { get { return this._phone; } set { this._phone = value; } }
        [DataMember]
        public string Fax { get { return this._fax; } set { this._fax = value; } }
        [DataMember]
        public string Mnemonic { get { return this._mnemonic; } set { this._mnemonic = value; } }
        [DataMember]
        public string APNumber { get { return this._apnumber; } set { this._apnumber = value; } }
        [DataMember]
        public string TransmitEBOL { get { return this._transmitebol; } set { this._transmitebol = value; } }
        [DataMember]
        public byte DeliveryScanStatus { get { return this._deliveryscanstatus; } set { this._deliveryscanstatus = value; } }
        [DataMember]
        public int ParentID { get { return this._parentid; } set { this._parentid = value; } }
        [DataMember]
        public DateTime LastUpdated { get { return this._lastupdated; } set { this._lastupdated = value; } }
        [DataMember]
        public string UserID { get { return this._userid; } set { this._userid = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class Carriers:BindingList<Carrier> {
        public Carriers() { }
    }

    [DataContract]
    public class Carrier {
        //Members
        private int _carrierid=0;
        private string _carriernumber="",_name="",_scac="", _apnumber="";
        private byte _isactive=0;
        private int _parentid=0;
        private string _mode="";
        private DateTime _lastupdated=DateTime.Now;
        private string _userid="";
        private long _carrierserviceid=0;

        //Interface
        public Carrier() : this(null) { }
        public Carrier(CarrierDS.CarrierTableRow carrier) {
            //Constructor
            try {
                if(carrier != null) {
                    if(!carrier.IsCarrierIDNull()) this._carrierid = carrier.CarrierID;
                    if(!carrier.IsCarrierNumberNull()) this._carriernumber = carrier.CarrierNumber;
                    if(!carrier.IsNameNull()) this._name = carrier.Name;
                    if(!carrier.IsSCACNull()) this._scac = carrier.SCAC;
                    if(!carrier.IsAPNumberNull()) this._apnumber = carrier.APNumber;
                    if(!carrier.IsIsActiveNull()) this._isactive = carrier.IsActive;
                    if(!carrier.IsParentIDNull()) this._parentid = carrier.ParentID;
                    if(!carrier.IsModeNull()) this._mode = carrier.Mode;
                    if(!carrier.IsLastUpdatedNull()) this._lastupdated = carrier.LastUpdated;
                    if(!carrier.IsUserIDNull()) this._userid = carrier.UserID;
                    if(!carrier.IsCarrierServiceIDNull()) this._carrierserviceid = carrier.CarrierServiceID;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating new agent instance.",ex); }
        }
        public Carrier(int carrierID, string number, string name) {
            //Constructor
            this._carrierid = carrierID;
            this._carriernumber = number;
            this._name = name;
        }
        public Carrier(long carrierServiceID,string description) {
            //Constructor
            this._carrierserviceid = carrierServiceID;
            this._name = description;
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int CarrierID { get { return this._carrierid; } set { this._carrierid = value; } }
        [DataMember]
        public string CarrierNumber { get { return this._carriernumber; } set { this._carriernumber = value; } }
        [DataMember]
        public string Name { get { return this._name; } set { this._name = value; } }
        [DataMember]
        public string SCAC { get { return this._scac; } set { this._scac = value; } }
        [DataMember]
        public string APNumber { get { return this._apnumber; } set { this._apnumber = value; } }
        [DataMember]
        public byte IsActive { get { return this._isactive; } set { this._isactive = value; } }
        [DataMember]
        public int ParentID { get { return this._parentid; } set { this._parentid = value; } }
        [DataMember]
        public string Mode { get { return this._mode; } set { this._mode = value; } }
        [DataMember]
        public DateTime LastUpdated { get { return this._lastupdated; } set { this._lastupdated = value; } }
        [DataMember]
        public string UserID { get { return this._userid; } set { this._userid = value; } }
        [DataMember]
        public long CarrierServiceID { get { return this._carrierserviceid; } set { this._carrierserviceid = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class Clients:BindingList<Client> {
        public Clients() { }
    }

    [DataContract]
    public class Client {
        //Members        
        private int _clientid=0;
        private string _clientnumber="", _number="", _division="";
        private string _name="", _status="";
        private string _ups_shipper_nbr="", _abbreviation="";
        private string _address_line1="", _address_line2="", _city="", _state="", _zip="", _zip4="";
        private string _delivery_bill_type="", _carton_commodity="", _delivery_bill="";
        private short _dbill_copies=0;
        private string _issan="", _invoiceprogram="";
        private string _contactname="", _phone="", _fax="", _mnemonic="";
        private string _blnumberoninvoice="", _arnumber="", _pickupzip="", _manifestpertrailer="";
        private DateTime _lastupdated=DateTime.Now;
        private string _userid="";
        private string _divisionnumber = "",_clientname = "",_terminalcode = "";

        //Interface
        public Client() { }
        public Client(ClientDS.ClientTableRow client) {
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
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Client instance",ex); }
        }
        public Client(ClientDS.InvoiceClientTableRow client) {
            //Constructor
            try {
                if(client != null) {
                    if(!client.IsClientNumberNull()) this._clientnumber = client.ClientNumber;
                    if(!client.IsDivisionNumberNull()) this._divisionnumber = client.DivisionNumber;
                    if(!client.IsClientNameNull()) this._clientname = client.ClientName;
                    if(!client.IsTerminalCodeNull()) this._terminalcode = client.TerminalCode;
                    if(!client.IsARNumberNull()) this._arnumber = client.ARNumber;
                    if(!client.IsStatusNull()) this._status = client.Status;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Client instance",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int ClientID { get { return this._clientid; } set { this._clientid = value; } }
        [DataMember]
        public string ClientNumber { get { return this._clientnumber; } set { this._clientnumber = value; } }
        [DataMember]
        public string Number { get { return this._number; } set { this._number = value; } }
        [DataMember]
        public string Division { get { return this._division; } set { this._division = value; } }
        [DataMember]
        public string Name { get { return this._name; } set { this._name = value; } }
        [DataMember]
        public string Status { get { return this._status; } set { this._status = value; } }
        [DataMember]
        public string UPSShipperNumber { get { return this._ups_shipper_nbr; } set { this._ups_shipper_nbr = value; } }
        [DataMember]
        public string Abbreviation { get { return this._abbreviation; } set { this._abbreviation = value; } }
        [DataMember]
        public string AddressLine1 { get { return this._address_line1; } set { this._address_line1 = value; } }
        [DataMember]
        public string AddressLine2 { get { return this._address_line2; } set { this._address_line2 = value; } }
        [DataMember]
        public string City { get { return this._city; } set { this._city = value; } }
        [DataMember]
        public string State { get { return this._state; } set { this._state = value; } }
        [DataMember]
        public string Zip { get { return this._zip; } set { this._zip = value; } }
        [DataMember]
        public string Zip4 { get { return this._zip4; } set { this._zip4 = value; } }
        [DataMember]
        public string DeliveryBillType { get { return this._delivery_bill_type; } set { this._delivery_bill_type = value; } }
        [DataMember]
        public string CartonCommodity { get { return this._carton_commodity; } set { this._carton_commodity = value; } }
        [DataMember]
        public string DeliveryBill { get { return this._delivery_bill; } set { this._delivery_bill = value; } }
        [DataMember]
        public short DbillCopies { get { return this._dbill_copies; } set { this._dbill_copies = value; } }
        [DataMember]
        public string IsSan { get { return this._issan; } set { this._issan = value; } }
        [DataMember]
        public string InvoiceProgram { get { return this._invoiceprogram; } set { this._invoiceprogram = value; } }
        [DataMember]
        public string ContactName { get { return this._contactname; } set { this._contactname = value; } }
        [DataMember]
        public string Phone { get { return this._phone; } set { this._phone = value; } }
        [DataMember]
        public string Fax { get { return this._fax; } set { this._fax = value; } }
        [DataMember]
        public string Mnemonic { get { return this._mnemonic; } set { this._mnemonic = value; } }
        [DataMember]
        public string BLNumberOnInvoice { get { return this._blnumberoninvoice; } set { this._blnumberoninvoice = value; } }
        [DataMember]
        public string ARNumber { get { return this._arnumber; } set { this._arnumber = value; } }
        [DataMember]
        public string PickupZip { get { return this._pickupzip; } set { this._pickupzip = value; } }
        [DataMember]
        public string ManifestPerTrailer { get { return this._manifestpertrailer; } set { this._manifestpertrailer = value; } }
        [DataMember]
        public DateTime LastUpdated { get { return this._lastupdated; } set { this._lastupdated = value; } }
        [DataMember]
        public string UserID { get { return this._userid; } set { this._userid = value; } }
        
        [DataMember]
        public string DivisionNumber { get { return this._divisionnumber; } set { this._divisionnumber = value; } }
        [DataMember]
        public string ClientName { get { return this._clientname; } set { this._clientname = value; } }
        [DataMember]
        public string TerminalCode { get { return this._terminalcode; } set { this._terminalcode = value; } }
        #endregion
    }

    [DataContract]
    public class Driver {
        //Members
        private int _driverid=0;
        private long _carrierid=0,_terminalid=0;
        private string _carrier,_terminal="";
        private string _firstname="",_lastname="",_fullname="";
        private string _phone="";
        private int _badgeid=0;
        private byte _isactive=(byte)1;
        protected DateTime _lastupdated=DateTime.Now;
        protected string _userid=Environment.UserName,_rowversion="";

        //Interface
        public Driver() : this(null) { }
        public Driver(DriverDS.DriverTableRow driver) {
            if(driver != null) {
                if(!driver.IsDriverIDNull()) this._driverid = driver.DriverID;
                if(!driver.IsCarrierIDNull()) this._carrierid = driver.CarrierID;
                if(!driver.IsCarrierNull()) this._carrier = driver.Carrier;
                if(!driver.IsTerminalIDNull()) this._terminalid = driver.TerminalID;
                if(!driver.IsTerminalNull()) this._terminal = driver.Terminal;
                if(!driver.IsFirstNameNull()) this._firstname = driver.FirstName;
                if(!driver.IsLastNameNull()) this._lastname = driver.LastName;
                if(!driver.IsFullNameNull())
                    this._fullname = driver.FullName;
                else {
                    this._fullname = (!driver.IsLastNameNull() ? driver.LastName.Trim() : "") + ", " + (!driver.IsFirstNameNull() ? driver.FirstName.Trim() : "");
                    if(this._fullname == ", ") this._fullname = "";
                }
                if(!driver.IsPhoneNull()) this._phone = driver.Phone;
                if(!driver.IsBadgeIDNull()) this._badgeid = driver.BadgeID;
                if(!driver.IsIsActiveNull()) this._isactive = driver.IsActive;
                if(!driver.IsLastUpdatedNull()) this._lastupdated = driver.LastUpdated;
                if(!driver.IsUserIDNull()) this._userid = driver.UserID;
                if(!driver.IsRowVersionNull()) this._rowversion = driver.RowVersion;
            }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int DriverID { get { return this._driverid; } set { this._driverid = value; } }
        [DataMember]
        public long CarrierID { get { return this._terminalid; } set { this._terminalid = value; } }
        [DataMember]
        public string Carrier { get { return this._terminal; } set { this._terminal = value; } }
        [DataMember]
        public long TerminalID { get { return this._terminalid; } set { this._terminalid = value; } }
        [DataMember]
        public string Terminal { get { return this._terminal; } set { this._terminal = value; } }
        [DataMember]
        public string FirstName { get { return this._firstname; } set { this._firstname = value; } }
        [DataMember]
        public string LastName { get { return this._lastname; } set { this._lastname = value; } }
        [DataMember]
        public string FullName { get { return this._fullname; } set { this._fullname = value; } }
        [DataMember]
        public string Phone { get { return this._phone; } set { this._phone = value; } }
        [DataMember]
        public int BadgeID { get { return this._badgeid; } set { this._badgeid = value; } }
        [DataMember]
        public byte IsActive { get { return this._isactive; } set { this._isactive = value; } }
        [DataMember]
        public DateTime LastUpdated { get { return this._lastupdated; } set { this._lastupdated = value; } }
        [DataMember]
        public string UserID { get { return this._userid; } set { this._userid = value; } }
        [DataMember]
        public string RowVersion { get { return this._rowversion; } set { this._rowversion = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class Shippers:BindingList<Shipper> {
        public Shippers() { }
    }
    
    [DataContract]
    public class Shipper {
        //Members
        protected string _number="", _name="", _status="";
        protected string _address_line1="", _address_line2="", _city="", _state="", _zip="", _zip4="";
        protected string _userdata="";
        protected long _locationID=0;

        //Interface	
        public Shipper() { }
        public Shipper(AgentDS.AgentTableRow agent) {
            //Constructor
            try {
                if(agent != null) {
                    this._number = agent.NUMBER;
                    if(!agent.IsNAMENull()) this._name = agent.NAME;
                    if(!agent.IsSTATUSNull()) this._status = agent.STATUS;
                    if(!agent.IsADDRESS_LINE1Null()) this._address_line1 = agent.ADDRESS_LINE1;
                    if(!agent.IsADDRESS_LINE2Null()) this._address_line2 = agent.ADDRESS_LINE2;
                    if(!agent.IsCITYNull()) this._city = agent.CITY;
                    if(!agent.IsSTATENull()) this._state = agent.STATE;
                    if(!agent.IsZIPNull()) this._zip = agent.ZIP;
                    if(!agent.IsZIP4Null()) this._zip4 = agent.ZIP4;
                    if(!agent.IsLocationIDNull()) this._locationID = agent.LocationID;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating new shipper instance.",ex); }
        }
        public Shipper(VendorDS.VendorTableRow vendor) {
            //Constructor
            try {
                if(vendor != null) {
                    this._number = vendor.NUMBER;
                    if(!vendor.IsNAMENull()) this._name = vendor.NAME;
                    if(!vendor.IsSTATUSNull()) this._status = vendor.STATUS;
                    if(!vendor.IsADDRESS_LINE1Null()) this._address_line1 = vendor.ADDRESS_LINE1;
                    if(!vendor.IsADDRESS_LINE2Null()) this._address_line2 = vendor.ADDRESS_LINE2;
                    if(!vendor.IsCITYNull()) this._city = vendor.CITY;
                    if(!vendor.IsSTATENull()) this._state = vendor.STATE;
                    if(!vendor.IsZIPNull()) this._zip = vendor.ZIP;
                    if(!vendor.IsZIP4Null()) this._zip4 = vendor.ZIP4;
                    if(!vendor.IsUSERDATANull()) this._userdata = vendor.USERDATA;
                    if(!vendor.IsLocationIDNull()) this._locationID = vendor.LocationID;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating new shipper instance.",ex); }
        }
        public Shipper(string number,string name,string addressLine1,string addressLine2,string addressCity,string addressState,string addressZip,string userData) {
            //Constructor
            try {
                this._number = number;
                this._name = name;
                this._address_line1 = addressLine1;
                this._address_line2 = addressLine2;
                this._city = addressCity;
                this._state = addressState;
                this._zip = addressZip;
                this._userdata = userData;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating new shipper instance.",ex); }
        }
        public Shipper(long locationID,string description) {
            //Constructor
            try {
                this._locationID = locationID;
                this._name = description;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating new shipper instance.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string Number { get { return this._number; } set { this._number = value; } }
        [DataMember]
        public string Name { get { return this._name; } set { this._name = value; } }
        [DataMember]
        public string Status { get { return this._status; } set { this._status = value; } }
        [DataMember]
        public string AddressLine1 { get { return this._address_line1; } set { this._address_line1 = value; } }
        [DataMember]
        public string AddressLine2 { get { return this._address_line2; } set { this._address_line2 = value; } }
        [DataMember]
        public string City { get { return this._city; } set { this._city = value; } }
        [DataMember]
        public string State { get { return this._state; } set { this._state = value; } }
        [DataMember]
        public string Zip { get { return this._zip; } set { this._zip = value; } }
        [DataMember]
        public string Zip4 { get { return this._zip4; } set { this._zip4 = value; } }
        [DataMember]
        public string UserData { get { return this._userdata; } set { this._userdata = value; } }
        [DataMember]
        public long LocationID { get { return this._locationID; } set { this._locationID = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class Stores:BindingList<Store> {
        public Stores() { }
    }

    [DataContract]
    public class Store {
        //Members
        private string _client_number="", _client_division="";
        private int _number=0;
        private string _name="";
        private string _address_line1="", _address_line2="", _city="", _state="", _zip="", _zip4="";
        private string _zone="", _zone_type="", _status="";
        private DateTime _open_date=DateTime.Now;
        private string _route="", _lbl_user_data="", _san_number="";
        private string _phone="", _instructions="", _labeltype="";
        private string _alt_number="", _altroute="", _local_lane="";

        //Interface
        public Store() : this(null) { }
        public Store(StoreDS.StoreTableRow store) {
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
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating new store instance.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string ClientNumber { get { return this._client_number; } set { this._client_number = value; } }
        [DataMember]
        public string ClientDivision { get { return this._client_division; } set { this._client_division = value; } }
        [DataMember]
        public int Number { get { return this._number; } set { this._number = value; } }
        [DataMember]
        public string Name { get { return this._name; } set { this._name = value; } }
        [DataMember]
        public string AddressLine1 { get { return this._address_line1; } set { this._address_line1 = value; } }
        [DataMember]
        public string AddressLine2 { get { return this._address_line2; } set { this._address_line2 = value; } }
        [DataMember]
        public string City { get { return this._city; } set { this._city = value; } }
        [DataMember]
        public string State { get { return this._state; } set { this._state = value; } }
        [DataMember]
        public string Zip { get { return this._zip; } set { this._zip = value; } }
        [DataMember]
        public string Zip4 { get { return this._zip4; } set { this._zip4 = value; } }
        [DataMember]
        public string Zone { get { return this._zone; } set { this._zone = value; } }
        [DataMember]
        public string ZoneType { get { return this._zone_type; } set { this._zone_type = value; } }
        [DataMember]
        public string Status { get { return this._status; } set { this._status = value; } }
        [DataMember]
        public DateTime OpenDate { get { return this._open_date; } set { this._open_date = value; } }
        [DataMember]
        public string Route { get { return this._route; } set { this._route = value; } }
        [DataMember]
        public string LabelUserData { get { return this._lbl_user_data; } set { this._lbl_user_data = value; } }
        [DataMember]
        public string SANNumber { get { return this._san_number; } set { this._san_number = value; } }
        [DataMember]
        public string Phone { get { return this._phone; } set { this._phone = value; } }
        [DataMember]
        public string Instructions { get { return this._instructions; } set { this._instructions = value; } }
        [DataMember]
        public string LableType { get { return this._labeltype; } set { this._labeltype = value; } }
        [DataMember]
        public string AltNumber { get { return this._alt_number; } set { this._alt_number = value; } }
        [DataMember]
        public string AltRoute { get { return this._altroute; } set { this._altroute = value; } }
        [DataMember]
        public string LocalLane { get { return this._local_lane; } set { this._local_lane = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class Vendors:BindingList<Vendor> {
        public Vendors() { }
    }

    [DataContract]
    public class Vendor:Shipper {
        //Members

        //Interface
        public Vendor() : this(null) { }
        public Vendor(VendorDS.VendorTableRow vendor): base(vendor) {
            //Constructor
            try { } catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new vendor instance.",ex); }
        }
        public Vendor(string number,string name,string addressLine1,string addressLine2,string addressCity,string addressState,string addressZip,string userData) : base(number,name,addressLine1,addressLine2,addressCity,addressState,addressZip,userData) { }
        #region Accessors\Modifiers: [Members...]
        #endregion
    }

    [DataContract]
    public class EnterpriseFault {
        private Exception _ex=null;
        public EnterpriseFault(Exception ex) { this._ex = ex; }
        [DataMember]
        public Exception Exception { get { return this._ex; } set { this._ex = value; } }
    }
}
