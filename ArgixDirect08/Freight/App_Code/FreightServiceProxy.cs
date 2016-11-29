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
	public class FreightServiceProxy {
		//Members

		//Interface
        public FreightServiceProxy() { }
        public CommunicationState ServiceState { get { return new FreightServiceClient().State; } }
        public string ServiceAddress { get { return new FreightServiceClient().Endpoint.Address.Uri.AbsoluteUri; } }

        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config = null;
            FreightServiceClient _Client = null;
            try {
                _Client = new FreightServiceClient();
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
            FreightServiceClient _Client = null;
            try {
                _Client = new FreightServiceClient();
                _Client.WriteLogEntry(m);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("WriteLogEntry() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("WriteLogEntry() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("WriteLogEntry() communication error.",ce); }
        }
        public TerminalInfo GetTerminalInfo() {
            //Get the operating enterprise terminal
            TerminalInfo terminal = null;
            FreightServiceClient _Client = null;
            try {
                _Client = new FreightServiceClient();
                terminal = _Client.GetTerminalInfo();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetTerminalInfo() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetTerminalInfo() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetTerminalInfo() communication error.",ce); }
            return terminal;
        }
        public Terminals GetTerminals() {
            //Get a list of Argix terminals
            Terminals terminals = null;
            FreightServiceClient _Client = null;
            try {
                _Client = new FreightServiceClient();
                terminals = _Client.GetTerminals();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetTerminals() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetTerminals() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetTerminals() communication error.",ce); }
            return terminals;
        }

        public InboundFreight GetInboundFreight(int terminalID,string freightType) {
            //Get the operating enterprise terminal
            InboundFreight shipments = null;
            FreightServiceClient _Client = null;
            try {
                _Client = new FreightServiceClient();
                InboundFreight _shipments = _Client.GetInboundFreight(terminalID,0);
                if(freightType != null && freightType.Length > 0) {
                    shipments = new InboundFreight();
                    for(int i = 0; i < _shipments.Count; i++) {
                        if(_shipments[i].FreightType.Trim().ToLower() == freightType.ToLower()) shipments.Add(_shipments[i]);
                    }
                }
                else
                    shipments = _shipments;
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetInboundFreight() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetInboundFreight() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetInboundFreight() communication error.",ce); }
            return shipments;
        }
        public InboundShipment GetInboundShipment(string freightID) {
            //Get the operating enterprise terminal
            InboundShipment shipment = null;
            FreightServiceClient _Client = null;
            try {
                _Client = new FreightServiceClient();
                shipment = _Client.GetInboundShipment(freightID);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetInboundShipment() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetInboundShipment() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetInboundShipment() communication error.",ce); }
            return shipment;
        }
        public StationAssignments GetStationAssignments(string freightID) {
            //Get the operating enterprise terminal
            StationAssignments assignments = null;
            FreightServiceClient _Client = null;
            try {
                _Client = new FreightServiceClient();
                StationAssignments _assignments = _Client.GetStationAssignments();
                if(freightID != null && freightID.Length > 0) {
                    assignments = new StationAssignments();
                    for(int i = 0; i < _assignments.Count; i++) {
                        if(_assignments[i].InboundFreight.FreightID == freightID) assignments.Add(_assignments[i]);
                    }
                }
                else
                    assignments = _assignments;
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetStationAssignments() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetStationAssignments() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetStationAssignments() communication error.",ce); }
            return assignments;
        }
    }
}