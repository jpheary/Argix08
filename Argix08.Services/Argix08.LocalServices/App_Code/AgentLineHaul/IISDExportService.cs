using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.AgentLineHaul {
    //Enterprise Interfaces
    [ServiceContract(Namespace="http://Argix.AgentLineHaul")]
    public interface IISDExportService {
        //General Interface
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action="http://Argix.ConfigurationFault")]
        UserConfiguration GetUserConfiguration(string application,string[] usernames);
        
        [OperationContract(IsOneWay=true)]
        void WriteLogEntry(TraceMessage m);

        [OperationContract]
        Argix.Enterprise.TerminalInfo GetTerminalInfo();

        [OperationContract]
        Argix.Freight.Pickups GetPickups(DateTime pickupDate);

        [OperationContract]
        Argix.Freight.SortedItems GetSortedItems(string pickupID);

        [OperationContract]
        string GetExportFilename(string counterKey);

        [OperationContract]
        ISDClients GetClients(string clientNumber);

        [OperationContract(Name="GetAllClients")]
        ISDClients GetClients();

        [OperationContract]
        [FaultContractAttribute(typeof(AgentLineHaulFault),Action="http://Argix.AgentLineHaul.AgentLineHaulFault")]
        bool CreateClient(string clid,string format,string path,string key,string client,string scanner,string userid);

        [OperationContract]
        [FaultContractAttribute(typeof(AgentLineHaulFault),Action="http://Argix.AgentLineHaul.AgentLineHaulFault")]
        bool UpdateClient(string clid,string format,string path,string key,string client,string scanner,string userid);

        [OperationContract]
        [FaultContractAttribute(typeof(AgentLineHaulFault),Action="http://Argix.AgentLineHaul.AgentLineHaulFault")]
        bool DeleteClient(string clid,string format,string path,string key,string client,string scanner,string userid);
    }

    [CollectionDataContract]
    public class ISDClients:BindingList<ISDClient> {
        public ISDClients() { }
    }

    [DataContract]
    public class ISDClient {
        //Members
        private string _clid="";
        private string _exportformat="", _exportpath="";
        private string _counterkey="", _client="";
        private string _scanner="", _userID="";

        //Interface
        public ISDClient() : this(null) { }
        public ISDClient(ISDClientDS.ClientTableRow client) {
            //Constructor
            try {
                if(client != null) {
                    this._clid = client.CLID;
                    this._exportformat = client.ExportFormat.Trim();
                    this._exportpath = client.ExportPath.Trim();
                    this._counterkey = client.CounterKey.Trim();
                    if(!client.IsClientNull()) this._client = client.Client.Trim();
                    if(!client.IsScannerNull()) this._scanner = client.Scanner.Trim();
                    if(!client.IsUserIDNull()) this._userID = client.UserID.Trim();
                }
            }
            catch(Exception ex) { throw new ApplicationException("",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string CLID { get { return this._clid; } set { this._clid = value; } }
        [DataMember]
        public string ExportFormat { get { return this._exportformat; } set { this._exportformat = value; } }
        [DataMember]
        public string ExportPath { get { return this._exportpath; } set { this._exportpath = value; } }
        [DataMember]
        public string CounterKey { get { return this._counterkey; } set { this._counterkey = value; } }
        [DataMember]
        public string Client { get { return this._client; } set { this._client = value; } }
        [DataMember]
        public string Scanner { get { return this._scanner; } set { this._scanner = value; } }
        [DataMember]
        public string UserID { get { return this._userID; } set { this._userID = value; } }
        #endregion
    }

    [DataContract]
    public class AgentLineHaulFault {
        private Exception _ex=null;
        public AgentLineHaulFault(Exception ex) { this._ex = ex; }
        [DataMember]
        public Exception Exception { get { return this._ex; } set { this._ex = value; } }
    }
}
