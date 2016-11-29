using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Finance {
    //Finance Interfaces
    [ServiceContract(Namespace="http://Argix.Finance", SessionMode=SessionMode.Allowed)]
    public interface IRateWareService {
        //General Interface
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action="http://Argix.ConfigurationFault")]
        UserConfiguration GetUserConfiguration(string application,string[] usernames);
        
        [OperationContract(IsOneWay=true)]
        void WriteLogEntry(TraceMessage m);

        [OperationContract]
        TerminalInfo GetTerminalInfo();

        [OperationContract]
        ClassCodes GetClassCodes();

        [OperationContract]
        [FaultContractAttribute(typeof(RateWareFault),Action="http://Argix.Finance.RateWareFault")]
        string[] GetTariffs();
        
        [OperationContract]
        [FaultContractAttribute(typeof(RateWareFault),Action="http://Argix.Finance.RateWareFault")]
        Rates CalculateRates(string tariff,string originZip,string classCode,double discount,int floorMin,string[] destinationZips);
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
    public class ClassCodes:BindingList<ClassCode> {
        public ClassCodes() { }
    }

    [DataContract]
    public class ClassCode {
        //Members
        private string mClass = "",mDescription = "";

        //Interface
        public ClassCode() { }
        public ClassCode(string name,string description) {
            //Constructor
            try {
                    this.mClass = name;
                    this.mDescription = description;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new ClassCode instance.",ex); }
        }

        [DataMember]
        public string Class { get { return this.mClass; } set { this.mClass = value; } }
        [DataMember]
        public string Description { get { return this.mDescription; } set { this.mDescription = value; } }
    }

    [CollectionDataContract]
    public class Rates:BindingList<Rate> {
        public Rates() { }
    }

    [DataContract]
    public class Rate {
        //Members
		private string mOrgZip = "", mDestZip = "", mMinCharge = "";
		private string mRate1 = "", mRate501 = "", mRate1001 = "", mRate2001 = "", mRate5001 = "", mRate10001 = "", mRate20001 = "";

        //Interface
        public Rate() : this(null) { }
        public Rate(RateDS.RateTableRow rate) {
            //Constructor
            try {
                if(rate != null) {
                    if(!rate.IsOrgZipNull()) this.mOrgZip = rate.OrgZip;
                    if(!rate.IsDestZipNull()) this.mDestZip = rate.DestZip;
                    if(!rate.IsMinChargeNull()) this.mMinCharge = rate.MinCharge;
                    if(!rate.IsRate1Null()) this.mRate1 = rate.Rate1;
                    if(!rate.IsRate501Null()) this.mRate501 = rate.Rate501;
                    if(!rate.IsRate1001Null()) this.mRate1001 = rate.Rate1001;
                    if(!rate.IsRate2001Null()) this.mRate2001 = rate.Rate2001;
                    if(!rate.IsRate5001Null()) this.mRate5001 = rate.Rate5001;
                    if(!rate.IsRate10001Null()) this.mRate10001 = rate.Rate10001;
                    if(!rate.IsRate20001Null()) this.mRate20001 = rate.Rate20001;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Rate instance",ex); }
        }

        [DataMember]
        public string OrgZip { get { return this.mOrgZip; } set { this.mOrgZip = value; } }
        [DataMember]
        public string DestZip { get { return this.mDestZip; } set { this.mDestZip = value; } }
        [DataMember]
        public string MinCharge { get { return this.mMinCharge; } set { this.mMinCharge = value; } }
        [DataMember]
        public string Rate1 { get { return this.mRate1; } set { this.mRate1 = value; } }
        [DataMember]
        public string Rate501 { get { return this.mRate501; } set { this.mRate501 = value; } }
        [DataMember]
        public string Rate1001 { get { return this.mRate1001; } set { this.mRate1001 = value; } }
        [DataMember]
        public string Rate2001 { get { return this.mRate2001; } set { this.mRate2001 = value; } }
        [DataMember]
        public string Rate5001 { get { return this.mRate5001; } set { this.mRate5001 = value; } }
        [DataMember]
        public string Rate10001 { get { return this.mRate10001; } set { this.mRate10001 = value; } }
        [DataMember]
        public string Rate20001 { get { return this.mRate20001; } set { this.mRate20001 = value; } }
}

    [DataContract]
    public class RateWareFault {
        private Exception _ex;
        public RateWareFault(Exception ex) { this._ex = ex; }

        [DataMember]
        public Exception Exception { get { return this._ex; } set { this._ex = value; } }
    }
}

