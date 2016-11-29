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
        private string _agentnumber="",_shipperid="";
        private byte _isactive = (byte)1;

        //Interface
        public Terminal() : this(null) { }
        public Terminal(TerminalDS.LocalTerminalTableRow terminal) {
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
        public byte IsActive { get { return this._isactive; } set { this._isactive = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class Clients:BindingList<Client> {
        public Clients() { }
    }

    [DataContract]
    public class Client {
        //Members
        private string mClientNumber = "";
        private string mDivisionNumber = "";
        private string mClientName = "";
        private string mTerminalCode = "";
        private string mARNumber = "";
        private string mStatus = "";

        //Interface
        public Client() : this(null) { }
        public Client(ClientDS.ClientTableRow client) {
            //Constructor
            try {
                if(client != null) {
                    if(!client.IsClientNumberNull()) this.mClientNumber = client.ClientNumber;
                    if(!client.IsDivisionNumberNull()) this.mDivisionNumber = client.DivisionNumber;
                    if(!client.IsClientNameNull()) this.mClientName = client.ClientName;
                    if(!client.IsTerminalCodeNull()) this.mTerminalCode = client.TerminalCode;
                    if(!client.IsARNumberNull()) this.mARNumber = client.ARNumber;
                    if(!client.IsStatusNull()) this.mStatus = client.Status;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Client instance",ex); }
        }

        [DataMember]
        public string ClientNumber { get { return this.mClientNumber; } set { this.mClientNumber = value; } }
        [DataMember]
        public string DivisionNumber { get { return this.mDivisionNumber; } set { this.mDivisionNumber = value; } }
        [DataMember]
        public string ClientName { get { return this.mClientName; } set { this.mClientName = value; } }
        [DataMember]
        public string TerminalCode { get { return this.mTerminalCode; } set { this.mTerminalCode = value; } }
        [DataMember]
        public string ARNumber { get { return this.mARNumber; } set { this.mARNumber = value; } }
        [DataMember]
        public string Status { get { return this.mStatus; } set { this.mStatus = value; } }
    }

    [DataContract]
    public class Driver {
        //Members
        private int _driverid=0;
        private long _carrierid=0, _terminalid=0;
        private string _carrier, _terminal="";
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

    [DataContract]
    public class EnterpriseFault {
        private Exception _ex=null;
        public EnterpriseFault(Exception ex) { this._ex = ex; }
        [DataMember]
        public Exception Exception { get { return this._ex; } set { this._ex = value; } }
    }
}
