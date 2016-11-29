//	File:	agent.cs
//	Author:	J. Heary
//	Date:	02/27/07
//	Desc:	Represents the state and behavior of an Argix agent.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;

namespace Tsort.Enterprise {
    //
    internal class Agent:Shipper {
        //Members
        private int _agentid=0;
        private string _contactname="";
        private string _phone="";
        private string _fax="";
        private string _mnemonic="";
        private string _apnumber="";
        private string _transmitebol="";
        private byte _deliveryscanstatus=0;
        private int _parentid=0;
        private DateTime _lastupdated=DateTime.Now;
        private string _userid="";

        //Constants
        //Events
        //Interface
        public Agent() : this(null) { }
        public Agent(AgentDS.AgentDetailTableRow agent)
            : base(agent) {
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
        #region Accessors\Modifiers: [Members]..., ToDataSet()
        public int AgentID { get { return this._agentid; } }
        public string ContactName { get { return this._contactname; } }
        public string Phone { get { return this._phone; } }
        public string Fax { get { return this._fax; } }
        public string Mnemonic { get { return this._mnemonic; } }
        public string APNumber { get { return this._apnumber; } }
        public string TransmitEBOL { get { return this._transmitebol; } }
        public byte DeliveryScanStatus { get { return this._deliveryscanstatus; } }
        public int ParentID { get { return this._parentid; } }
        public DateTime LastUpdated { get { return this._lastupdated; } }
        public string UserID { get { return this._userid; } }
        public override DataSet ToDataSet() {
            //Return a dataset containing values for this object
            AgentDS ds=null;
            try {
                ds = new AgentDS();
                AgentDS.AgentDetailTableRow agent = ds.AgentDetailTable.NewAgentDetailTableRow();
                agent.AgentID = this._agentid;
                agent.NUMBER = base._number;
                agent.NAME = base._name;
                agent.STATUS = base._status;
                agent.ADDRESS_LINE1 = base._address_line1;
                agent.ADDRESS_LINE2 = base._address_line2;
                agent.CITY = base._city;
                agent.STATE = base._state;
                agent.ZIP = base._zip;
                agent.ZIP4 = base._zip4;
                agent.ContactName = this._contactname;
                agent.Phone = this._phone;
                agent.Fax = this._fax;
                agent.Mnemonic = this._mnemonic;
                agent.APNumber = this._apnumber;
                agent.TransmitEBOL = this._transmitebol;
                agent.DeliveryScanStatus = this._deliveryscanstatus;
                agent.ParentID = this._parentid;
                agent.LastUpdated = this._lastupdated;
                agent.UserID = this._userid;
                ds.AgentDetailTable.AddAgentDetailTableRow(agent);
            }
            catch(Exception) { }
            return ds;
        }
        #endregion
    }
}
