using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.ApplicationBlocks.Data;
using Argix.Data;
//using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
//using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix.AgentLineHaul {
    //
    public class TemplateFactory {
        //Members
        public static Mediator Mediator = null;
        private string mConnectionID = "";
        public const string USP_TEMPLATES = "uspShipScdeTemplateGetList", TBL_TEMPLATES = "TemplateTable";
        public const string USP_TEMPLATES_NEW = "uspShipScdeTemplateNew";
        public const string USP_TEMPLATESSTOP_NEW = "uspShipScdeTemplateStopNew";
        public const string USP_TEMPLATES_UPDATE = "uspShipScdeTemplateUpdate";
        public const string USP_TEMPLATESSTOP_UPDATE = "uspShipScdeTemplateStopUpdate";

        //Interface
        static TemplateFactory() { }
        private TemplateFactory(string connectionID) {
            //Constructor
            this.mConnectionID = connectionID;
        }
        
        public static TemplateDS GetTemplates() {
            //Get a collection of all ship schedules templates for all terminals
            TemplateDS templates=null;
            try {
                //Clear and update ship schedules
                templates = new TemplateDS();
                DataSet ds = fillDataset(USP_TEMPLATES,TBL_TEMPLATES,null);
                if(ds.Tables[TBL_TEMPLATES].Rows.Count > 0) templates.Merge(ds,true,MissingSchemaAction.Ignore);
            }
            catch(Exception ex) { throw new ApplicationException("Failed to refresh templates list.",ex); }
            return templates;
        }
        public static bool AddTemplate(TemplateDS.TemplateTableRow template) {
            addTemplate(template,populateStopTemplateRow(template));
            return true;
        }
        public static bool UpdateTemplate(TemplateDS.TemplateTableRow template) {
            updateTemplate(template,populateStopTemplateRow(template));
            return true;
        }

        #region Template Services: addTemplate(), updateTemplate(), deleteTemplate(), addStop()
        private static void addTemplate(TemplateDS.TemplateTableRow template,TemplateDS stops) {
            SqlConnection sqlConnection = null;
            try {
                sqlConnection = new SqlConnection(Mediator.Connection);
                sqlConnection.Open();
                using(SqlTransaction trans = sqlConnection.BeginTransaction()) {
                    try {
                        template.TemplateLastUpdated = DateTime.Now;
                        template.TemplateUser = Environment.UserName;
                        SqlParameter[] spParams = setTemplateParameters(template);
                        executeNonQuery(trans,USP_TEMPLATES_NEW,spParams);
                        string templateID = spParams[0].Value.ToString();
                        foreach(TemplateDS.TemplateTableRow stop in stops.TemplateTable.Rows) {
                            stop.TemplateID = templateID;
                            executeNonQueryTypedParams(trans,USP_TEMPLATESSTOP_NEW,stop);
                        }
                        trans.Commit();
                    }
                    catch(Exception ex) { trans.Rollback(); throw ex; }
                }
            }
            finally { if(sqlConnection != null) sqlConnection.Dispose(); }
        }
        private static void updateTemplate(TemplateDS.TemplateTableRow template,TemplateDS stops) {
            //
            SqlConnection sqlConnection = null;
            try {
                sqlConnection = new SqlConnection(Mediator.Connection);
                sqlConnection.Open();
                using(SqlTransaction trans = sqlConnection.BeginTransaction()) {
                    try {
                        executeNonQueryTypedParams(trans,USP_TEMPLATES_UPDATE,template);
                        foreach(TemplateDS.TemplateTableRow stop in stops.TemplateTable.Rows) {
                            if(stop.IsStopIDNull()) {
                                //We need to add Stop here instead of update
                                stop.TemplateID = template.TemplateID;
                                executeNonQueryTypedParams(trans,USP_TEMPLATESSTOP_NEW,stop);
                            }
                            else
                                executeNonQueryTypedParams(trans,USP_TEMPLATESSTOP_UPDATE,stop);
                        }
                        trans.Commit();
                    }
                    catch(Exception ex) { trans.Rollback(); throw ex; }
                }
            }
            finally { if(sqlConnection != null) sqlConnection.Dispose(); }
        }
        private static SqlParameter[] setTemplateParameters(TemplateDS.TemplateTableRow template) {
            SqlParameter[] parms = new SqlParameter[13];
            parms[0] = new SqlParameter("@TemplateID",SqlDbType.Char,8);
            parms[0].Direction = ParameterDirection.Output;
            parms[1] = new SqlParameter("@SortCenterID",SqlDbType.BigInt);
            parms[1].Value = template.SortCenterID;
            parms[2] = new SqlParameter("@DayOfWeek",SqlDbType.TinyInt);
            parms[2].Value = template.DayOfTheWeek;
            parms[3] = new SqlParameter("@CarrierServiceID",SqlDbType.BigInt);
            parms[3].Value = template.CarrierServiceID;
            parms[4] = new SqlParameter("@ScheduledCloseDateOffset",SqlDbType.TinyInt);
            parms[4].Value = template.ScheduledCloseDateOffset;
            parms[5] = new SqlParameter("@ScheduledCloseTime",SqlDbType.DateTime);
            parms[5].Value = template.ScheduledCloseTime;
            parms[6] = new SqlParameter("@ScheduledDepartureDateOffset",SqlDbType.TinyInt);
            parms[6].Value = template.ScheduledDepartureDateOffset;
            parms[7] = new SqlParameter("@ScheduledDepartureTime",SqlDbType.DateTime);
            parms[7].Value = template.@ScheduledDepartureTime;
            parms[8] = new SqlParameter("@IsMandatory",SqlDbType.TinyInt);
            parms[8].Value = template.IsMandatory;
            parms[9] = new SqlParameter("@IsActive",SqlDbType.TinyInt);
            parms[9].Value = template.IsActive;
            parms[10] = new SqlParameter("@LastUpdated",SqlDbType.DateTime);
            parms[10].Value = template.TemplateLastUpdated;
            parms[11] = new SqlParameter("@UserID",SqlDbType.VarChar,50);
            parms[11].Value = template.TemplateUser;
            parms[12] = new SqlParameter("@RowVersion",SqlDbType.Char,24);
            parms[12].Direction = ParameterDirection.Output;
            return parms;
        }
        private static TemplateDS populateStopTemplateRow(TemplateDS.TemplateTableRow template) {
            //Build First Stop row
            TemplateDS ds = new TemplateDS();
            TemplateDS.TemplateTableRow stop1 = ds.TemplateTable.NewTemplateTableRow();
            stop1.StopID = !template.IsStopIDNull() ? template.StopID : "";
            stop1.StopNumber = "1";
            stop1.Tag = template.Tag;
            stop1.AgentTerminalID = template.AgentTerminalID;
            stop1.ScheduledArrivalDateOffset = template.ScheduledArrivalDateOffset;
            stop1.ScheduledArrivalTime = template.ScheduledArrivalTime;
            stop1.ScheduledOFD1Offset = template.ScheduledOFD1Offset;
            stop1.Notes = template.IsNotesNull() ? "" : template.Notes;
            stop1.Stop1LastUpdated = DateTime.Today;
            stop1.Stop1User = Environment.UserName;
            stop1.Stop1RowVersion = !template.IsStop1RowVersionNull() ? template.Stop1RowVersion : "";
            ds.TemplateTable.AddTemplateTableRow(stop1);

            //check for the second stop
            if(!template.IsS2MainZoneNull() && template.S2MainZone.Trim() != "") {
                TemplateDS.TemplateTableRow stop2 = ds.TemplateTable.NewTemplateTableRow();
                stop2.StopID = !template.IsS2StopIDNull() ? template.S2StopID : "";
                stop2.StopNumber = "2";
                stop2.Tag = template.S2Tag;
                stop2.AgentTerminalID = template.S2AgentTerminalID;
                stop2.ScheduledArrivalDateOffset = template.S2ScheduledArrivalDateOffset;
                stop2.ScheduledArrivalTime = template.S2ScheduledArrivalTime;
                stop2.ScheduledOFD1Offset = template.S2ScheduledOFD1Offset;
                stop2.Notes = template.IsS2NotesNull() ? "" : template.S2Notes;
                stop2.Stop1LastUpdated = DateTime.Today;
                stop2.Stop1User = Environment.UserName;
                stop2.Stop1RowVersion = !template.IsStop2RowVersionNull() ? template.Stop2RowVersion : "";
                ds.TemplateTable.AddTemplateTableRow(stop2);
            }
            return ds;
        }
        #endregion

        #region Data Services: fillDataset(), executeNonQuery(), executeNonQueryTypedParams(), executeNonQueryWithReturn()
        private static DataSet fillDataset(string spName, string table, object[] paramValues) {
            //
            //DataSet ds = new DataSet();
            //Database db = DatabaseFactory.CreateDatabase(this.mConnectionID);
            //DbCommand cmd = db.GetStoredProcCommand(spName, paramValues);
            //db.LoadDataSet(cmd, ds, table);
            //return ds;
            return Mediator.FillDataset(spName, table, paramValues);
        }
        private static bool executeNonQuery(string spName, object[] paramValues) {
            //
            //bool ret = false;
            //Database db = DatabaseFactory.CreateDatabase(this.mConnectionID);
            //int i = db.ExecuteNonQuery(spName, paramValues);
            //ret = i > 0;
            //return ret;
            return Mediator.ExecuteNonQuery(spName, paramValues);
        }
        private static bool executeNonQuery(SqlTransaction transaction, string spName, SqlParameter[] spParams) { 
            //
            //bool ret = false;
            //int i = db.ExecuteNonQuery(spName, paramValues);
            //ret = i > 0;
            //return ret;
            return SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParams) > 0;
        }
        private static bool executeNonQueryTypedParams(SqlTransaction transaction, string spName, DataRow dataRow) {
            //
            //bool ret = false;
            //int i = db.ExecuteNonQuery(spName, paramValues);
            //ret = i > 0;
            //return ret;
            return SqlHelper.ExecuteNonQueryTypedParams(transaction, spName, dataRow) > 0;
        }
        private static object executeNonQueryWithReturn(string spName, object[] paramValues) {
            //
            object ret = null;
            if ((paramValues != null) && (paramValues.Length > 0)) {
                //Database db = DatabaseFactory.CreateDatabase(this.mConnectionID);
                //DbCommand cmd = db.GetStoredProcCommand(spName, paramValues);
                //ret = db.ExecuteNonQuery(cmd);

                //Find the output parameter and return its value
                //foreach (DbParameter param in cmd.Parameters) {
                //    if ((param.Direction == ParameterDirection.Output) || (param.Direction == ParameterDirection.InputOutput)) {
                //        ret = param.Value;
                //        break;
                //    }
                //}
                ret = Mediator.ExecuteNonQueryWithReturn(spName, paramValues);
            }
            return ret;
        }
        #endregion
    }
}
