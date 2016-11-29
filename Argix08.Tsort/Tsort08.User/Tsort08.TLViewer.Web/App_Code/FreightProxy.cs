using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;

namespace Argix.Freight {
	//
	public class FreightProxy {
		//Members

		//Interface
        public FreightProxy() { }
        public CommunicationState ServiceState { get { return new TLViewerServiceClient().State; } }
        public string ServiceAddress { get { return new TLViewerServiceClient().Endpoint.Address.Uri.AbsoluteUri; } }

        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config=null;
            TLViewerServiceClient _Client=null;
            try {
                _Client = new TLViewerServiceClient();
                config = _Client.GetUserConfiguration(application,usernames);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetUserConfiguration() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetUserConfiguration() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetUserConfiguration() communication error.",ce); }
            return config;
        }
        public void WriteLogEntry(TraceMessage m) {
            //Get the operating enterprise terminal
            TLViewerServiceClient _Client=null;
            try {
                _Client = new TLViewerServiceClient();
                _Client.WriteLogEntry(m);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("WriteLogEntry() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("WriteLogEntry() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("WriteLogEntry() communication error.",ce); }
        }
        public TerminalInfo GetTerminalInfo() {
            //Get the operating enterprise terminal
            TerminalInfo terminal=null;
            TLViewerServiceClient _Client=null;
            try {
                _Client = new TLViewerServiceClient();
                terminal = _Client.GetTerminalInfo();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetTerminalInfo() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetTerminalInfo() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetTerminalInfo() communication error.",ce); }
            return terminal;
        }

        public Terminals GetTerminals(int terminalID) {
            //Returns a list of terminals
            Terminals terminals=null;
            TLViewerServiceClient _Client=null;
            try {
                terminals = new Terminals();
                _Client = new TLViewerServiceClient();
                Terminals ts = _Client.GetTerminals();
                for(int i=0;i<ts.Count;i++) {
                    Terminal t = ts[i];
                    if(terminalID == 0 || t.TerminalID == terminalID)
                        terminals.Add(t);
                }
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetTerminals() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetTerminals() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetTerminals() communication error.",ce); }
            return terminals;
        }
        public TLs GetTLView(int terminalID, string sortBy) {
            //Get a view of TLs for the specified terminal
            TLs tls=null;
            TLViewerServiceClient _Client=null;
            try {
                _Client = new TLViewerServiceClient();
                tls = _Client.GetTLView(terminalID);
                for(int i=0;i<tls.Count;i++) tls[i].TerminalID = terminalID;

                if(sortBy.Trim().Length == 0) sortBy = "TLNumber";
                TLComparer comparer = new TLComparer(sortBy);
                tls.Sort(comparer);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetTLView() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetTLView() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetTLView() communication error.",ce); }
            return tls;
        }
        public TLs GetTLDetail(int terminalID,string tlNumber,string sortBy) {
            //Get TL detail for the specified TL#
            TLs tls=null;
            TLViewerServiceClient _Client=null;
            try {
                _Client = new TLViewerServiceClient();
                tls = _Client.GetTLDetail(terminalID,tlNumber);

                if(sortBy.Trim().Length == 0) sortBy = "ClientNumber";
                TLComparer comparer = new TLComparer(sortBy);
                tls.Sort(comparer);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetTLDetail() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetTLDetail() timed out.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetTLDetail() had a communication issue.",ce); }
            return tls;
        }
        public TLs GetAgentSummary(int terminalID,string sortBy) {
            //Get an agent summary view for the specified terminal
            TLs tls=null;
            TLViewerServiceClient _Client=null;
            try {
                _Client = new TLViewerServiceClient();
                tls = _Client.GetAgentSummary(terminalID);

                if(sortBy.Trim().Length == 0) sortBy = "AgentNumber";
                TLComparer comparer = new TLComparer(sortBy);
                tls.Sort(comparer);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetAgentSummary() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetAgentSummary() timed out.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetAgentSummary() had a communication issue.",ce); }
            return tls;
        }
    }

    internal class TLComparer:System.Collections.Generic.IComparer<TL> {
        //
        private string mField="";
        public TLComparer(string field) { this.mField = field; }
        public int Compare(TL tl1,TL tl2) {
            switch(this.mField) {
                case "AgentName": return tl1.AgentName.CompareTo(tl2.AgentName);
                case "AgentNumber": return tl1.AgentNumber.CompareTo(tl2.AgentNumber);
                case "ClientName": return tl1.ClientName.CompareTo(tl2.ClientName);
                case "ClientNumber": return tl1.ClientNumber.CompareTo(tl2.ClientNumber);
                case "CloseNumber": return tl1.CloseNumber.CompareTo(tl2.CloseNumber);
                case "Lane": return tl1.Lane.CompareTo(tl2.Lane);
                case "ShipToLocationID": return tl1.ShipToLocationID.CompareTo(tl2.ShipToLocationID);
                case "ShipToLocationName": return tl1.ShipToLocationName.CompareTo(tl2.ShipToLocationName);
                case "SmallLane": return tl1.SmallLane.CompareTo(tl2.SmallLane);
                case "TLNumber": return tl1.TLNumber.Trim().CompareTo(tl2.TLNumber.Trim());
                case "Zone": return tl1.Zone.CompareTo(tl2.Zone);
                case "Cartons": return tl1.Cartons.CompareTo(tl2.Cartons);
                case "Cube": return tl1.Cube.CompareTo(tl2.Cube);
                case "CubePercent": return tl1.CubePercent.CompareTo(tl2.CubePercent);
                case "Pallets": return tl1.Pallets.CompareTo(tl2.Pallets);
                case "TerminalID": return tl1.TerminalID.CompareTo(tl2.TerminalID);
                case "Weight": return tl1.Weight.CompareTo(tl2.Weight);
                case "WeightPercent": return tl1.WeightPercent.CompareTo(tl2.WeightPercent);
                case "TLDate": return tl1.TLDate.CompareTo(tl2.TLDate);
                default: return 0;
            }
        }
    }

}