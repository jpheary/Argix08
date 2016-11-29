//	File:	shipper.cs
//	Author:	J. Heary
//	Date:	02/27/07
//	Desc:	.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;

namespace Tsort.Enterprise {
    //
    public abstract class Shipper {
        //Members
        protected string _number="";
        protected string _name="";
        protected string _status="";
        protected string _address_line1="";
        protected string _address_line2="";
        protected string _city="";
        protected string _state="";
        protected string _zip="";
        protected string _zip4="";
        protected string _userdata="";

        //Constants
        //Events
        //Interface	
        public Shipper() { }
        public Shipper(AgentDS.AgentDetailTableRow agent) {
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
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating new shipper instance.",ex); }
        }
        public Shipper(VendorDS.VendorDetailTableRow vendor) {
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
        #region Accessors\Modifiers: [Members]..., ToDataSet()
        public string NUMBER { get { return this._number; } }
        public string NAME { get { return this._name; } }
        public string STATUS { get { return this._status; } }
        public string ADDRESS_LINE1 { get { return this._address_line1; } }
        public string ADDRESS_LINE2 { get { return this._address_line2; } }
        public string CITY { get { return this._city; } }
        public string STATE { get { return this._state; } }
        public string ZIP { get { return this._zip; } }
        public string ZIP4 { get { return this._zip4; } }
        public string USERDATA { get { return this._userdata; } }
        public abstract DataSet ToDataSet();
        #endregion
    }
}