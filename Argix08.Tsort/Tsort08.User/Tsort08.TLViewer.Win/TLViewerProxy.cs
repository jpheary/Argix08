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
	public class TLViewerProxy {
		//Members
        private static TLViewerServiceClient _Client=null;
        private static bool _state=false;
        private static string _address="";

		//Interface
        static TLViewerProxy() { 
            //
            _Client = new TLViewerServiceClient();
            _state = true;
            _address = _Client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private TLViewerProxy() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config=null;
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
        public static void WriteLogEntry(TraceMessage m) {
            //Get the operating enterprise terminal
            try {
                _Client = new TLViewerServiceClient();
                _Client.WriteLogEntry(m);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("WriteLogEntry() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("WriteLogEntry() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("WriteLogEntry() communication error.",ce); }
        }
        public static TerminalInfo GetTerminalInfo() {
            //Get the operating enterprise terminal
            TerminalInfo terminal=null;
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

        public static Terminals GetTerminals(int terminalID) {
            //Returns a list of terminals
            Terminals terminals=null;
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
        
        public static TLs GetTLView(int terminalID) {
            //Get a view of TLs for the specified terminal
            TLs tls=null;
            try {
                _Client = new TLViewerServiceClient();
                tls = _Client.GetTLView(terminalID);
                for(int i=0;i<tls.Count;i++) tls[i].TerminalID = terminalID;
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetTLView() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetTLView() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetTLView() communication error.",ce); }
            return tls;
        }
        public static TLs GetTLDetail(int terminalID,string tlNumber) {
            //Get TL detail for the specified TL#
            TLs tls=null;
            try {
                _Client = new TLViewerServiceClient();
                tls = _Client.GetTLDetail(terminalID,tlNumber);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetTLDetail() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetTLDetail() timed out.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetTLDetail() had a communication issue.",ce); }
            return tls;
        }
        public static TLs GetAgentSummary(int terminalID) {
            //Get an agent summary view for the specified terminal
            TLs tls=null;
            try {
                _Client = new TLViewerServiceClient();
                tls = _Client.GetAgentSummary(terminalID);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetAgentSummary() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetAgentSummary() timed out.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetAgentSummary() had a communication issue.",ce); }
            return tls;
        }
    }
}