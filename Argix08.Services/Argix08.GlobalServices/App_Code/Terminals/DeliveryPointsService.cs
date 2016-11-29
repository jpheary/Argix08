using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix.Terminals {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class DeliveryPointsService:IDeliveryPointsService {
        //Members
        private const string SQL_CONNID = "DeliveryPoints";
        private const string USP_DELIVERYPOINTS = "uspRoadshowExportGet",TBL_DELIVERYPOINTS = "DeliveryPointTable";
        private const string USP_EXPORTDATE_GET = "uspRoadshowExportDateGet";
        private const string USP_EXPORTDATE_UPDATE = "uspRoadshowExportDateUpdate";

        //Interface
        public DeliveryPointsService() { }
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

        public DeliveryPoints GetDeliveryPoints(DateTime startDate,DateTime lastUpated) {
            DeliveryPoints points=null;
            const string CSV_DELIM = ",";
            //Update a collection (dataset) of all delivery points
            try {
                //Clear and fetch new data
                points = new DeliveryPoints();
                DataSet ds = fillDataset(USP_DELIVERYPOINTS,TBL_DELIVERYPOINTS,new object[] { startDate });
                if(ds != null) {
                    //Create a dataset of containing unique entries
                    DeliveryPointDS _ds = new DeliveryPointDS();
                    _ds.Merge(ds);
                    DeliveryPointDS rds = new DeliveryPointDS();
                    rds.Merge(_ds.DeliveryPointTable.Select("","Account ASC"));
                    Hashtable ht = new Hashtable();
                    string acct = "";
                    for(int i=0;i<rds.DeliveryPointTable.Rows.Count;i++) {
                        //Remove duplicate account entries
                        acct = rds.DeliveryPointTable[i].Account;
                        if(ht.ContainsKey(acct))
                            rds.DeliveryPointTable[i].Delete();
                        else
                            ht.Add(acct,null);		//Keep track of keys (unique accounts)
                    }
                    rds.AcceptChanges();

                    //Modify data
                    for(int i=0;i<rds.DeliveryPointTable.Rows.Count;i++) {
                        //Set command as follows:
                        //	A:	OpenDate > lastpDated; U: OpenDate <= lastUpdated
                        DateTime opened = rds.DeliveryPointTable[i].OpenDate;
                        rds.DeliveryPointTable[i].Command = (opened.CompareTo(lastUpated) > 0) ? "A" : "U";

                        //Remove commas from address fields
                        rds.DeliveryPointTable[i].Building = rds.DeliveryPointTable[i].Building.Replace(CSV_DELIM," ");
                        rds.DeliveryPointTable[i].Address = rds.DeliveryPointTable[i].Address.Replace(CSV_DELIM," ");
                        rds.DeliveryPointTable[i].StopComment = rds.DeliveryPointTable[i].StopComment.Replace(CSV_DELIM," ");

                        //Add each point to the collection
                        DeliveryPoint point = new DeliveryPoint(rds.DeliveryPointTable[i]);
                        points.Add(point);
                    }
                }
            }
            catch(Exception ex) { throw ex; }
            return points;
        }
        public DateTime GetExportDate() {
            //Get the latest delivery point LastUpdated date from the last export
            return Convert.ToDateTime((string)executeScalar(USP_EXPORTDATE_GET,new object[] { }));
        }
        public bool UpdateExportDate(DateTime lastUpdated) {
            //Update the latest delivery point LastUpdated date from the last export
            return executeNonQuery(USP_EXPORTDATE_UPDATE,new object[] { lastUpdated.ToString("MM-dd-yyyy HH:mm") });
        }

        #region Data Services: fillDataset(), executeNonQuery(), executeNonQueryWithReturn(), executeScalar()
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
        private object executeNonQueryWithReturn(string spName,object[] paramValues) {
            //
            object ret=null;
            if((paramValues != null) && (paramValues.Length > 0)) {
                Database db = DatabaseFactory.CreateDatabase(SQL_CONNID);
                DbCommand cmd = db.GetStoredProcCommand(spName,paramValues);
                ret = db.ExecuteNonQuery(cmd);

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
        private object executeScalar(string spName,object[] paramValues) {
            //
            Database db = DatabaseFactory.CreateDatabase(SQL_CONNID);
            return db.ExecuteScalar(spName,paramValues);
        }
        #endregion
    }
}
