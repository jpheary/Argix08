using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix.AgentLineHaul {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    //[ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall,TransactionIsolationLevel=System.Transactions.IsolationLevel.ReadCommitted,TransactionTimeout="00:00:30")]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class ShipScheduleService:IShipScheduleService {
        //Members
        private const string SQL_CONNID = "ShipSchedule";
        public const string USP_TERMINALS = "uspShipScdeTerminalGetList", TBL_TERMINALS = "TerminalTable";
        public const string USP_SHIPPERTERMINALS = "uspShipScdeShipperTerminalGetList", TBL_SHIPPERTERMINALS = "TerminalTable";
        public const string USP_CARRIERS = "uspShipScdeCarrierGetList",TBL_CARRIERS = "CarrierTable";
        public const string USP_AGENTS = "uspShipScdeAgentGetList",TBL_AGENTS = "AgentTable";
        public const string USP_SHIPPERS = "uspShipScdeShipperGetList",TBL_SHIPPERS = "ShipperTable";

        public const string USP_TEMPLATES = "uspShipScdeTemplateGetList",TBL_TEMPLATES = "TemplateTable";
        public const string USP_TEMPLATES_NEW = "uspShipScdeTemplateNew";
        public const string USP_TEMPLATESSTOP_NEW = "uspShipScdeTemplateStopNew";
        public const string USP_TEMPLATES_UPDATE = "uspShipScdeTemplateUpdate";
        public const string USP_TEMPLATESSTOP_UPDATE = "uspShipScdeTemplateStopUpdate";

        //Interface
        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get configuration data for the specified application and usernames
            return new Argix.AppService(SQL_CONNID).GetUserConfiguration(application,usernames);
        }
        public void WriteLogEntry(TraceMessage m) {
            //Write o to database log if event level is severe enough
            new Argix.AppService(SQL_CONNID).WriteLogEntry(m);
        }
        public Argix.Enterprise.TerminalInfo GetTerminalInfo() {
            //Get the operating enterprise terminal
            return new Argix.Enterprise.EnterpriseService(SQL_CONNID).GetTerminalInfo();
        }

        public Argix.Enterprise.Terminals GetTerminals() {
            //Get a list local terminals
            Argix.Enterprise.Terminals terminals=null;
            try {
                terminals = new Argix.Enterprise.Terminals();
                DataSet ds = fillDataset(USP_TERMINALS, TBL_TERMINALS, new object[] { });
                if(ds.Tables[TBL_TERMINALS].Rows.Count > 0) {
                    DataSet _ds = new DataSet();
                    _ds.Merge(ds.Tables[TBL_TERMINALS].Select("","Description ASC"));
                    for(int i=0;i<_ds.Tables[TBL_TERMINALS].Rows.Count;i++) {
                        Argix.Enterprise.Terminal terminal = new Argix.Enterprise.Terminal();
                        terminal.LocationID = Convert.ToInt64(_ds.Tables[TBL_TERMINALS].Rows[i]["ID"]);
                        terminal.Description = _ds.Tables[TBL_TERMINALS].Rows[i]["Description"].ToString().Trim();
                        terminals.Add(terminal);
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading terminals.",ex); }
            return terminals;
        }
        public Argix.Enterprise.Terminals GetShippersAndTerminals() {
            //Get a list of shippers and terminals
            Argix.Enterprise.Terminals terminals = null;
            try {
                terminals = new Argix.Enterprise.Terminals();
                DataSet ds = fillDataset(USP_SHIPPERTERMINALS, TBL_SHIPPERTERMINALS, new object[] { });
                if (ds.Tables[TBL_SHIPPERTERMINALS].Rows.Count > 0) {
                    DataSet _ds = new DataSet();
                    _ds.Merge(ds.Tables[TBL_SHIPPERTERMINALS].Select("", "Description ASC"));
                    for (int i = 0; i < _ds.Tables[TBL_SHIPPERTERMINALS].Rows.Count; i++) {
                        Argix.Enterprise.Terminal terminal = new Argix.Enterprise.Terminal();
                        terminal.LocationID = Convert.ToInt64(_ds.Tables[TBL_SHIPPERTERMINALS].Rows[i]["ID"]);
                        terminal.Description = _ds.Tables[TBL_SHIPPERTERMINALS].Rows[i]["Description"].ToString().Trim();
                        terminals.Add(terminal);
                    }
                }
            }
            catch (Exception ex) { throw new ApplicationException("Unexpected error while reading shippers and terminals.", ex); }
            return terminals;
        }
        public Argix.Enterprise.Carriers GetCarriers() {
            //Get a list of carriers
            Argix.Enterprise.Carriers carriers=null;
            try {
                carriers = new Argix.Enterprise.Carriers();
                DataSet ds = fillDataset(USP_CARRIERS, TBL_CARRIERS, new object[] { });
                if(ds.Tables[TBL_CARRIERS].Rows.Count > 0) {
                    DataSet _ds = new DataSet();
                    _ds.Merge(ds.Tables[TBL_CARRIERS].Select("","Description ASC"));
                    for(int i=0;i<_ds.Tables[TBL_CARRIERS].Rows.Count;i++) {
                        Argix.Enterprise.Carrier carrier = new Argix.Enterprise.Carrier();
                        carrier.CarrierServiceID = Convert.ToInt64(_ds.Tables[TBL_CARRIERS].Rows[i]["ID"]);
                        carrier.Name = _ds.Tables[TBL_CARRIERS].Rows[i]["Description"].ToString().Trim();
                        carriers.Add(carrier);
                    }
                }
            }
            catch (Exception ex) { throw new ApplicationException("Unexpected error while reading carriers.", ex); }
            return carriers;
        }
        public Argix.Enterprise.Agents GetAgents() {
            //Get a list of agents
            Argix.Enterprise.Agents agents=null;
            try {
                agents = new Argix.Enterprise.Agents();
                DataSet ds = fillDataset(USP_AGENTS, TBL_AGENTS, new object[] { });
                if(ds.Tables[TBL_AGENTS].Rows.Count > 0) {
                    DataSet _ds = new DataSet();
                    _ds.Merge(ds.Tables[TBL_AGENTS].Select("","Description ASC"));
                    for(int i=0;i<_ds.Tables[TBL_AGENTS].Rows.Count;i++) {
                        Argix.Enterprise.Agent agent = new Argix.Enterprise.Agent();
                        agent.LocationID = Convert.ToInt64(_ds.Tables[TBL_AGENTS].Rows[i]["ID"]);
                        agent.Name = _ds.Tables[TBL_AGENTS].Rows[i]["Description"].ToString().Trim();
                        agent.Number = _ds.Tables[TBL_AGENTS].Rows[i]["Number"].ToString().Trim();
                        agents.Add(agent);
                    }
                }
            }
            catch (Exception ex) { throw new ApplicationException("Unexpected error while reading agents.", ex); }
            return agents;
        }
        public Argix.Enterprise.Shippers GetShippers() {
            //Get a list of shippers or the local terminal
            Argix.Enterprise.Shippers shippers=null;
            try {
                shippers = new Argix.Enterprise.Shippers();
                DataSet ds = fillDataset(USP_SHIPPERS,TBL_SHIPPERS,new object[]{});
                if(ds.Tables[TBL_SHIPPERS].Rows.Count > 0) {
                    DataSet _ds = new DataSet();
                    _ds.Merge(ds.Tables[TBL_SHIPPERS].Select("","Description ASC"));
                    for(int i=0;i<_ds.Tables[TBL_SHIPPERS].Rows.Count;i++) {
                        Argix.Enterprise.Shipper shipper = new Argix.Enterprise.Shipper();
                        shipper.LocationID = Convert.ToInt64(_ds.Tables[TBL_SHIPPERS].Rows[i]["ID"]);
                        shipper.Name = _ds.Tables[TBL_SHIPPERS].Rows[i]["Description"].ToString().Trim();
                        shippers.Add(shipper);
                    }
                }
            }
            catch (Exception ex) { throw new ApplicationException("Unexpected error while reading shippers.", ex); }
            return shippers;
        }

        public DataSet GetDaysOfWeek() {
            DataSet daysOfWeek = new DataSet();
            daysOfWeek.Tables.Add("SelectionListTable");
            daysOfWeek.Tables["SelectionListTable"].Columns.Add("ID");
            daysOfWeek.Tables["SelectionListTable"].Columns.Add("Description");
            daysOfWeek.Tables["SelectionListTable"].Rows.Add(new object[] { 1,"Mon" });
            daysOfWeek.Tables["SelectionListTable"].Rows.Add(new object[] { 2,"Tue" });
            daysOfWeek.Tables["SelectionListTable"].Rows.Add(new object[] { 3,"Wed" });
            daysOfWeek.Tables["SelectionListTable"].Rows.Add(new object[] { 4,"Thu" });
            daysOfWeek.Tables["SelectionListTable"].Rows.Add(new object[] { 5,"Fri" });
            daysOfWeek.Tables["SelectionListTable"].Rows.Add(new object[] { 6,"Sat" });
            daysOfWeek.Tables["SelectionListTable"].Rows.Add(new object[] { 7,"Sun" });
            daysOfWeek.AcceptChanges();
            return daysOfWeek;
        }
        public int GetWeekday(string weekdayName) {
            int weekday = 0;
            switch(weekdayName.ToLower()) {
                case "mon": weekday = 1; break;
                case "tue": weekday = 2; break;
                case "wed": weekday = 3; break;
                case "thu": weekday = 4; break;
                case "fri": weekday = 5; break;
                case "sat": weekday = 6; break;
                case "sun": weekday = 7; break;
            }
            return weekday;
        }

        public DataSet GetTemplates() {
            //Get a collection of ship schedules templates for all terminals
            DataSet ds = new DataSet();
            try {
                ds.Tables.Add(TBL_TEMPLATES);
                ds = fillDataset(USP_TEMPLATES,TBL_TEMPLATES,new object[] { });
            }
            catch(Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.ToString()),new FaultReason("Failed to read ship schedule templates list: " + ex.Message)); }
            return ds;
        }
        
        //[OperationBehavior(TransactionScopeRequired=true, TransactionAutoComplete=true)]
        public string AddTemplate(ShipScheduleTemplate template) {
            //
            string templateID="";
            if(template != null) {
                SqlConnection sqlConn = null;
                try {
                    sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings[SQL_CONNID].ConnectionString);
                    sqlConn.Open();
                    using(SqlTransaction sqlTrans = sqlConn.BeginTransaction()) {
                        try {
                            //Trip
                            object[] trip = new object[] {  null, template.SortCenterID, template.DayOfTheWeek, template.CarrierServiceID, template.ScheduledCloseDateOffset, 
                                                            template.ScheduledCloseTime, template.ScheduledDepartureDateOffset, template.ScheduledDepartureTime, 
                                                            template.IsMandatory, template.IsActive, DateTime.Now, template.TemplateUser, null };
                            templateID = (string)executeNonQueryWithReturn(sqlTrans, USP_TEMPLATES_NEW,trip);

                            //Stop 1
                            object[] stop1 = new object[] { null, templateID, template.StopNumber, template.AgentTerminalID, template.Tag, template.Notes, 
                                                            template.ScheduledArrivalDateOffset, template.ScheduledArrivalTime, template.ScheduledOFD1Offset, DateTime.Now, template.Stop1User, null };
                            executeNonQuery(sqlTrans, USP_TEMPLATESSTOP_NEW,stop1);

                            //Stop 2
                            if(template.S2MainZone.Trim().Length > 0) {
                                object[] stop2 = new object[] { null, templateID, template.S2StopNumber, template.S2AgentTerminalID, template.S2Tag, template.S2Notes, 
                                                                template.S2ScheduledArrivalDateOffset, template.S2ScheduledArrivalTime, template.S2ScheduledOFD1Offset, DateTime.Now, template.Stop2User, null };
                                executeNonQuery(sqlTrans, USP_TEMPLATESSTOP_NEW, stop2);
                            }
                            sqlTrans.Commit();
                        }
                        catch(Exception ex) {
                            sqlTrans.Rollback();
                            throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.ToString()),new FaultReason("Unexpected error adding ship schedule template: " + ex.Message)); 
                        }
                    }
                }
                finally { if(sqlConn != null) sqlConn.Dispose(); }
            }
            return templateID;
        }

        //[OperationBehavior(TransactionScopeRequired=true,TransactionAutoComplete=true)]
        public bool UpdateTemplate(ShipScheduleTemplate template) {
            //
            bool updated=false;
            if(template != null) {
                SqlConnection sqlConn = null;
                try {
                    sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings[SQL_CONNID].ConnectionString);
                    sqlConn.Open();
                    using(SqlTransaction sqlTrans = sqlConn.BeginTransaction()) {
                        try {
                            //Trip
                            object[] trip = new object[] {  template.TemplateID, template.DayOfTheWeek, template.CarrierServiceID, template.ScheduledCloseDateOffset, 
                                                            template.ScheduledCloseTime, template.ScheduledDepartureDateOffset, template.ScheduledDepartureTime, 
                                                            template.IsMandatory, template.IsActive, DateTime.Now, template.TemplateUser, template.TemplateRowVersion };
                            executeNonQuery(sqlTrans, USP_TEMPLATES_UPDATE,trip);

                            //Stop 1
                            object[] stop1 = new object[] { template.StopID, template.AgentTerminalID, template.Tag, template.Notes, template.ScheduledArrivalDateOffset, 
                                                            template.ScheduledArrivalTime, template.ScheduledOFD1Offset, DateTime.Now,  template.Stop1User, template.Stop1RowVersion };
                            executeNonQuery(sqlTrans, USP_TEMPLATESSTOP_UPDATE,stop1);

                            //Stop 2
                            if(template.S2MainZone != null && template.S2MainZone.Trim().Length > 0) {
                                //New or update?
                                if(template.S2StopID.Trim().Length == 0) {
                                    object[] stop2 = new object[] { null, template.TemplateID, template.S2StopNumber, template.S2AgentTerminalID, template.S2Tag, template.S2Notes, 
                                                                    template.S2ScheduledArrivalDateOffset, template.S2ScheduledArrivalTime, template.S2ScheduledOFD1Offset, DateTime.Now, template.Stop2User, null };
                                    executeNonQuery(sqlTrans, USP_TEMPLATESSTOP_NEW,stop2);
                                }
                                else {
                                    object[] stop2 = new object[] { template.S2StopID, template.S2AgentTerminalID, template.S2Tag, template.S2Notes, template.S2ScheduledArrivalDateOffset, 
                                                                    template.S2ScheduledArrivalTime, template.S2ScheduledOFD1Offset, DateTime.Now, template.Stop2User, template.Stop2RowVersion };
                                    executeNonQuery(sqlTrans, USP_TEMPLATESSTOP_UPDATE,stop2);
                                }
                            }
                            sqlTrans.Commit();
                            updated = true;
                        }
                        catch(Exception ex) {
                            sqlTrans.Rollback();
                            //System.Transactions.Transaction.Current.Rollback(ex);
                            throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.ToString()),new FaultReason("Unexpected error updating ship schedule template: " + ex.Message));
                        }
                    }
                }
                finally { if(sqlConn != null) sqlConn.Dispose(); }
            }
            return updated;
        }

        public System.IO.Stream GetExportDefinition() {
            //
            System.IO.StreamReader sr = new System.IO.StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~\\App_Data\\ExportDS.xsd"));
            return sr.BaseStream;
        }

        #region Data Services: fillDataset(), executeNonQuery(), executeNonQueryWithReturn()
        private DataSet fillDataset(string spName,string table,object[] paramValues) {
            //
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase(SQL_CONNID);
            DbCommand cmd = db.GetStoredProcCommand(spName,paramValues);
            db.LoadDataSet(cmd,ds,table);
            return ds;
        }
        private bool executeNonQuery(string spName,object[] paramValues) {
            //
            bool ret=false;
            Database db = DatabaseFactory.CreateDatabase(SQL_CONNID);
            int i = db.ExecuteNonQuery(spName,paramValues);
            ret = i > 0;
            return ret;
        }
        private bool executeNonQuery(SqlTransaction sqlTrans,string spName,params object[] paramValues) {
            //
            bool ret=false;
            Database db = DatabaseFactory.CreateDatabase(SQL_CONNID);
            int i = db.ExecuteNonQuery(sqlTrans,spName,paramValues);
            ret = i > 0;
            return ret;
        }
        private object executeNonQueryWithReturn(string spName,object[] paramValues) {
            //
            object ret=null;
            if((paramValues != null) && (paramValues.Length > 0)) {
                Database db = DatabaseFactory.CreateDatabase(SQL_CONNID);
                DbCommand cmd = db.GetStoredProcCommand(spName,paramValues);
                int i = db.ExecuteNonQuery(cmd);

                //Find the output parameter and return its value
                foreach(DbParameter param in cmd.Parameters) {
                    if((param.Direction == ParameterDirection.Output) || (param.Direction == ParameterDirection.InputOutput)) {
                        ret = param.Value;
                        break;
                    }
                }
            }
            return ret;
        }
        private object executeNonQueryWithReturn(SqlTransaction sqlTrans,string spName,object[] paramValues) {
            //
            object ret=null;
            if((paramValues != null) && (paramValues.Length > 0)) {
                Database db = DatabaseFactory.CreateDatabase(SQL_CONNID);
                DbCommand cmd = db.GetStoredProcCommand(spName,paramValues);
                int i = db.ExecuteNonQuery(cmd, sqlTrans);

                //Find the output parameter and return its value
                foreach(DbParameter param in cmd.Parameters) {
                    if((param.Direction == ParameterDirection.Output) || (param.Direction == ParameterDirection.InputOutput)) {
                        ret = param.Value;
                        break;
                    }
                }
            }
            return ret;
        }
        #endregion
    }
}
