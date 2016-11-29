using System;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace Argix.Freight {
    //
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class FreightService:IFreightService {
        //Members
        private const string SQL_CONNID = "SQLConnection";
        private const string USP_LOCALTERMINAL = "uspEnterpriseCurrentTerminalGet",TBL_LOCALTERMINAL = "LocalTerminalTable";
        private const string USP_TERMINALS = "uspFATerminalsGetList", TBL_TERMINALS="TerminalTable";
        private const string USP_FREIGHT = "uspFAInboundFreightGetList",TBL_FREIGHT = "InboundFreightTable";
        private const string USP_SHIPMENT = "uspFAInboundFreightGet";
        private const string USP_ASSIGNMENTS = "uspFAAssignmentGetList",TBL_ASSIGNMENTS = "StationFreightAssignmentTable";
        
        //Interface
        public FreightService() { }
        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get configuration data for the specified application and usernames
            return new Argix.AppService(SQL_CONNID).GetUserConfiguration(application,usernames);
        }
        public void WriteLogEntry(TraceMessage m) {
            //Write o to database log if event level is severe enough
            new Argix.AppService(SQL_CONNID).WriteLogEntry(m);
        }
        public TerminalInfo GetTerminalInfo() {
            //Get information about the local terminal for this service
            TerminalInfo info = null;
            try {
                info = new TerminalInfo();
                info.Connection = DatabaseFactory.CreateDatabase(SQL_CONNID).ConnectionStringWithoutCredentials;
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_LOCALTERMINAL,TBL_LOCALTERMINAL,new object[] { });
                if(ds!=null && ds.Tables[TBL_LOCALTERMINAL].Rows.Count > 0) {
                    info.TerminalID = Convert.ToInt32(ds.Tables[TBL_LOCALTERMINAL].Rows[0]["TerminalID"]);
                    info.Number = ds.Tables[TBL_LOCALTERMINAL].Rows[0]["Number"].ToString().Trim();
                    info.Description = ds.Tables[TBL_LOCALTERMINAL].Rows[0]["Description"].ToString().Trim();
                }
            }
            catch(Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(new ApplicationException("Unexpected error while reading terminal info.",ex))); }
            return info;
        }
        public Terminals GetTerminals() {
            //Get Argix terminals
            Terminals terminals = null;
            try {
                terminals = new Terminals();
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_TERMINALS,TBL_TERMINALS,new object[] { });
                if(ds != null && ds.Tables[TBL_TERMINALS].Rows.Count > 0) {
                    EnterpriseDS _ds = new EnterpriseDS();
                    _ds.Merge(ds);
                    for(int i = 0; i < _ds.TerminalTable.Rows.Count; i++) {
                        Terminal t = new Terminal();
                        t.TerminalID = Convert.ToInt32(ds.Tables[TBL_TERMINALS].Rows[i]["TerminalID"]);
                        t.Number = ds.Tables[TBL_TERMINALS].Rows[i]["Number"].ToString().Trim();
                        t.Description = ds.Tables[TBL_TERMINALS].Rows[i]["Description"].ToString().Trim();
                        t.AgentID = ds.Tables[TBL_TERMINALS].Rows[i]["AgentID"].ToString().Trim();
                        terminals.Add(t);
                    }
                }
            }
            catch(Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(new ApplicationException("Unexpected error while reading Argix terminals.",ex))); }
            return terminals;
        }

        public InboundFreight GetInboundFreight(int terminalID,int sortedRange) {
            //Get a list of inbound shipments for the specified terminal
            InboundFreight shipments = new InboundFreight();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_FREIGHT,TBL_FREIGHT,new object[] { terminalID,DateTime.Today.AddDays(-sortedRange) });
                if(ds != null) {
                    FreightDS freight = new FreightDS();
                    freight.Merge(ds,true);
                    for(int i=0; i<freight.InboundFreightTable.Rows.Count; i++) {
                        shipments.Add(new InboundShipment(freight.InboundFreightTable[i]));
                    }
                }
            }
            catch(Exception ex) { throw new FaultException<FreightFault>(new FreightFault(new ApplicationException("Unexpected error while reading inbound freight.",ex))); }
            return shipments;
        }
        public InboundShipment GetInboundShipment(string freightID) {
            //Return the inbound shipment for the specified freightID
            InboundShipment shipment = null;
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_SHIPMENT,TBL_FREIGHT,new object[] { freightID });
                if(ds != null & ds.Tables[TBL_FREIGHT].Rows.Count > 0) {
                    FreightDS freight = new FreightDS();
                    freight.Merge(ds,false,MissingSchemaAction.Ignore);
                    shipment = new InboundShipment(freight.InboundFreightTable[0]);
                }
            }
            catch(Exception ex) { throw new FaultException<FreightFault>(new FreightFault(new ApplicationException("Unexpected error while reading inbound shipment.",ex))); }
            return shipment;
        }
        public StationAssignments GetStationAssignments() {
            //Get a list of station-freight assignments for the local terminal
            StationAssignments assignments = new StationAssignments();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_ASSIGNMENTS,TBL_ASSIGNMENTS,new object[]{});
                if(ds != null) {
                    FreightDS assignmentsDS = new FreightDS();
                    assignmentsDS.Merge(ds,false,MissingSchemaAction.Ignore);
                    for(int i = 0; i < assignmentsDS.StationFreightAssignmentTable.Rows.Count; i++) {
                        assignments.Add(new StationAssignment(assignmentsDS.StationFreightAssignmentTable[i]));
                    }
                }
            }
            catch(Exception ex) { throw new FaultException<FreightFault>(new FreightFault(new ApplicationException("Unexpected error while reading station assignments.",ex))); }
            return assignments;
        }
    }
}
