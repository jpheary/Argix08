using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Enterprise {
    //
    [ServiceContract(Namespace="http://Argix.Enterprise")]
    public interface IEnterpriseService {
        [OperationContract]
        [FaultContractAttribute(typeof(EnterpriseFault),Action="http://Argix.EnterpriseFault")]
        TerminalInfo GetTerminalInfo();
        [OperationContract]
        [FaultContractAttribute(typeof(EnterpriseFault), Action = "http://Argix.EnterpriseFault")]
        Terminals GetTerminals();

        [OperationContract]
        CompanyDS GetCompanies();
        [OperationContract(Name="GetActiveCompanies")]
        CompanyDS GetCompanies(bool activeOnly);
        [OperationContract]
        string GetCompany(int companyID);
        [OperationContract]
        LocationDS GetDistricts(string clientNumber);
        [OperationContract]
        LocationDS GetRegions(string clientNumber);
        [OperationContract]
        AgentDS GetAgents();
        [OperationContract(Name="GetAgentsForClient")]
        AgentDS GetAgents(string clientNumber);
        [OperationContract]
        StoreDS GetStoreDetail(int companyID,int storeNumber);
        [OperationContract(Name="GetSubStoreDetail")]
        StoreDS GetStoreDetail(int companyID,string subStore);

        [OperationContract]
        ContactDS GetContacts();
        [OperationContract(Name="GetContactsForLocation")]
        ContactDS GetContacts(int companyID,string regionNumber,string districtNumber,string agentNumber,string storeNumber);
        [OperationContract]
        Contact GetContact(int contactID);
        [OperationContract]
        int CreateContact(Contact contact);
        [OperationContract]
        bool UpdateContact(Contact contact);
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
    public class Terminals : BindingList<Terminal> {
        public Terminals() { }
    }

    [DataContract]
    public class Terminal {
        private int mTerminalID=0;
        private string mNumber="",mDescription="",mAgentID="";

        [DataMember]
        public int TerminalID { get { return this.mTerminalID; } set { this.mTerminalID = value; } }
        [DataMember]
        public string Number { get { return this.mNumber; } set { this.mNumber = value; } }
        [DataMember]
        public string Description { get { return this.mDescription; } set { this.mDescription = value; } }
        [DataMember]
        public string AgentID { get { return this.mAgentID; } set { this.mAgentID = value; } }
    }
    
    [CollectionDataContract]
    public class Contacts:BindingList<Contact> {
        public Contacts() { }
    }

    [DataContract]
    public class Contact {
        //Members
        private int _id=0;
        private string _firstname = "",_lastname = "",_fullname = "";
        private string _phone = "",_mobile = "", _fax = "",_email = "";

        //Interface
        public Contact() : this(null) { }
        public Contact(ContactDS.ContactTableRow contact) {
            //Constructor
            try {
                if(contact != null) {
                    if(!contact.IsIDNull()) this._id = contact.ID;
                    if(!contact.IsFirstNameNull()) this._firstname = contact.FirstName;
                    if(!contact.IsLastNameNull()) this._lastname = contact.LastName;
                    if(!contact.IsFullNameNull())
                        this._fullname = contact.FullName;
                    else
                        this._fullname = this._firstname + " " + this._lastname;
                    if(!contact.IsPhoneNull()) this._phone = contact.Phone;
                    if(!contact.IsMobileNull()) this._mobile = contact.Mobile;
                    if(!contact.IsFaxNull()) this._fax = contact.Fax;
                    if(!contact.IsEmailNull()) this._email = contact.Email;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Contact instance.",ex); }
        }
        
        [DataMember]
        public int ID { get { return this._id; } set { this._id = value; } }
        [DataMember]
        public string FirstName { get { return this._firstname; } set { this._firstname = value; this._fullname = this._firstname + " " + this._lastname; } }
        [DataMember]
        public string LastName { get { return this._lastname; } set { this._lastname = value; this._fullname = this._firstname + " " + this._lastname; } }
        [DataMember]
        public string FullName { get { return (this._fullname.Trim().Length>0?this._fullname:this._firstname+" "+this._lastname); } set { this._fullname = value; } }
        [DataMember]
        public string Phone { get { return this._phone; } set { this._phone = value; } }
        [DataMember]
        public string Mobile { get { return this._mobile; } set { this._mobile = value; } }
        [DataMember]
        public string Fax { get { return this._fax; } set { this._fax = value; } }
        [DataMember]
        public string Email { get { return this._email; } set { this._email = value; } }
    }

    [DataContract]
    public class EnterpriseFault {
        private Exception _ex=null;
        public EnterpriseFault(Exception ex) { this._ex = ex; }
        [DataMember]
        public Exception Exception { get { return this._ex; } set { this._ex = value; } }
    }
}
