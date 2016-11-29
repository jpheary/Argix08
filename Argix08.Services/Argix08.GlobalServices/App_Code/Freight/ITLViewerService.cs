using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Freight {
    //Shipping Interfaces
    [ServiceContract(Namespace="http://Argix.Freight")]
    public interface ITLViewerService {
        //General Shipping Interface
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action="http://Argix.ConfigurationFault")]
        UserConfiguration GetUserConfiguration(string application,string[] usernames);
        [OperationContract(IsOneWay=true)]
        void WriteLogEntry(TraceMessage m);

        [OperationContract]
        Argix.Enterprise.TerminalInfo GetTerminalInfo();

        [OperationContract]
        Argix.Enterprise.Terminals GetTerminals();
        
        [OperationContract]
        TLs GetTLView(int terminalID);
        
        [OperationContract]
        TLs GetTLDetail(int terminalID,string tlNumber);
        
        [OperationContract]
        TLs GetAgentSummary(int terminalID);
    }

    [CollectionDataContract]
    public class TLs:BindingList<TL> {
        public TLs() { }
    }

    [DataContract]
    public class TL {
        //Members
        private int _TerminalID=0;
        private string _Zone="", _AgentNumber="", _AgentName="";
        private string _TLNumber="",_CloseNumber="";
        private DateTime _TLDate;
        private string _ClientNumber="", _ClientName="";
        private string _ShipToLocationID="", _ShipToLocationName="";
        private string _Lane="", _SmallLane="";
        private int _Cartons=0, _Pallets=0, _Weight=0, _Cube=0;
        private decimal _WeightPercent=0.0M, _CubePercent=0.0M;

        //Interface
        public TL() : this(null) { }
        public TL(TLViewDS.TLTableRow tl) {
            //Constructor
            if(tl != null) {
                if(!tl.IsTerminalIDNull()) this._TerminalID = tl.TerminalID;
                if(!tl.IsZoneNull()) this._Zone = tl.Zone;
                if(!tl.IsAgentNumberNull()) this._AgentNumber = tl.AgentNumber;
                if(!tl.IsAgentNameNull()) this._AgentName = tl.AgentName;
                if(!tl.IsTLNumberNull()) this._TLNumber = tl.TLNumber;
                if(!tl.IsTLDateNull()) this._TLDate = tl.TLDate;
                if(!tl.IsCloseNumberNull()) this._CloseNumber = tl.CloseNumber;
                if(!tl.IsClientNumberNull()) this._ClientNumber = tl.ClientNumber;
                if(!tl.IsClientNameNull()) this._ClientName = tl.ClientName;
                if(!tl.IsShipToLocationIDNull()) this._ShipToLocationID = tl.ShipToLocationID;
                if(!tl.IsShipToLocationNameNull()) this._ShipToLocationName = tl.ShipToLocationName;
                if(!tl.IsLaneNull()) this._Lane = tl.Lane;
                if(!tl.IsSmallLaneNull()) this._SmallLane = tl.SmallLane;
                if(!tl.IsCartonsNull()) this._Cartons = tl.Cartons;
                if(!tl.IsPalletsNull()) this._Pallets = tl.Pallets;
                if(!tl.IsWeightNull()) this._Weight = tl.Weight;
                if(!tl.IsCubeNull()) this._Cube = tl.Cube;
                if(!tl.IsWeightPercentNull()) this._WeightPercent = tl.WeightPercent;
                if(!tl.IsCubePercentNull()) this._CubePercent = tl.CubePercent;
            }
        }

        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int TerminalID { get { return this._TerminalID; } set { this._TerminalID = value; } }
        [DataMember]
        public string Zone { get { return this._Zone; } set { this._Zone = value; } }
        [DataMember]
        public string AgentNumber { get { return this._AgentNumber; } set { this._AgentNumber = value; } }
        [DataMember]
        public string AgentName { get { return this._AgentName; } set { this._AgentName = value; } }
        [DataMember]
        public string TLNumber { get { return this._TLNumber; } set { this._TLNumber = value; } }
        [DataMember]
        public DateTime TLDate { get { return this._TLDate; } set { this._TLDate = value; } }
        [DataMember]
        public string CloseNumber { get { return this._CloseNumber; } set { this._CloseNumber = value; } }
        [DataMember]
        public string ClientNumber { get { return this._ClientNumber; } set { this._ClientNumber = value; } }
        [DataMember]
        public string ClientName { get { return this._ClientName; } set { this._ClientName = value; } }
        [DataMember]
        public string ShipToLocationID { get { return this._ShipToLocationID; } set { this._ShipToLocationID = value; } }
        [DataMember]
        public string ShipToLocationName { get { return this._ShipToLocationName; } set { this._ShipToLocationName = value; } }
        [DataMember]
        public string Lane { get { return this._Lane; } set { this._Lane = value; } }
        [DataMember]
        public string SmallLane { get { return this._SmallLane; } set { this._SmallLane = value; } }
        [DataMember]
        public int Cartons { get { return this._Cartons; } set { this._Cartons = value; } }
        [DataMember]
        public int Pallets { get { return this._Pallets; } set { this._Pallets = value; } }
        [DataMember]
        public int Weight { get { return this._Weight; } set { this._Weight = value; } }
        [DataMember]
        public int Cube { get { return this._Cube; } set { this._Cube = value; } }
        [DataMember]
        public decimal WeightPercent { get { return this._WeightPercent; } set { this._WeightPercent = value; } }
        [DataMember]
        public decimal CubePercent { get { return this._CubePercent; } set { this._CubePercent = value; } }
        #endregion
    }

}
