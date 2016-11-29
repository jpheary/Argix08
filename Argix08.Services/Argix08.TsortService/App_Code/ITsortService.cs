using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Freight.Tsort {
    //Enterprise Interfaces
    [ServiceContract(Namespace="http://Argix.Freight.Tsort")]
    public interface ITsortService {
        //Interface
	    [OperationContract]
        Workstation GetStation(string machinName);

        [OperationContract]
        DataSet GetFreightAssignments(string worhstationID);

        [OperationContract]
        SortedItem ProcessInputs(string[] inputs,decimal weight,string damageCode,string storeOverride,string freightID);
    }

    [DataContract]
    public class Workstation {
        //Members
        private string _workStationID="", _name="";
        private int _terminalID=0;
        private string _number="", _description="";

        //Interface
        public Workstation() { }
        public Workstation(string workStationID,string name,int terminalID,string number,string description) { 
            //Construxtor
            this._workStationID = workStationID;
            this._name = name;
            this._terminalID = terminalID;
            this._number = number;
            this._description = description;
        }
        [DataMember]
        public string WorkStationID { get { return this._workStationID; } set { this._workStationID = value; } }
        [DataMember]
        public string Name { get { return this._name; } set { this._name = value; } }
        [DataMember]
        public int TerminalID { get { return this._terminalID; } set { this._terminalID = value; } }
        [DataMember]
        public string Number { get { return this._number; } set { this._number = value; } }
        [DataMember]
        public string Description { get { return this._description; } set { this._description = value; } }
    }

    [DataContract]
    public class SortedItem {
        //Members
        private string _labelZPL="";
        
        //Interface
        public SortedItem() { }
        [DataMember]
        public string LabelZPL { get { return this._labelZPL; } set { this._labelZPL = value; } }
}
}