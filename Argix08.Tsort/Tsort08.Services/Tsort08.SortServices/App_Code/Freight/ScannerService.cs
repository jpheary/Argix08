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

namespace Argix.Freight {
    //
    //[ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    //[ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall,TransactionIsolationLevel=System.Transactions.IsolationLevel.ReadCommitted,TransactionTimeout="00:00:30")]
    public class ScannerService:IScannerService {
        //Members
        private const string SQL_CONNID = "Scanner";
        public const string USP_ASSIGNMENT_GET = "uspScannerAssignmentGet",TBL_ASSIGNMENT = "AssignmentTable";
        public const string USP_SORTEDITEM_NEW = "uspScannedItemNew";
        public const string USP_SORTEDITEM_UPDATE = "uspScannedItemDelete";

        //Interface
        public void WriteLogEntry(TraceMessage m) {
            //Write o to database log if event level is severe enough
            new Argix.AppService(SQL_CONNID).WriteLogEntry(m);
        }

        public ScannerAssignment GetScannerAssignment(string scannerNumber) {
            //
            ScannerAssignment assignment=null;
            try {
                DataSet ds = fillDataset(USP_ASSIGNMENT_GET,TBL_ASSIGNMENT,new object[] { scannerNumber });
                if(ds.Tables[TBL_ASSIGNMENT].Rows.Count > 0) {
                    assignment = new ScannerAssignment();
                    assignment.ScannerID = ds.Tables[TBL_ASSIGNMENT].Rows[0]["ScannerID"].ToString();
                    assignment.ScannerNumber = ds.Tables[TBL_ASSIGNMENT].Rows[0]["Number"].ToString();
                    assignment.TerminalID = Convert.ToInt32(ds.Tables[TBL_ASSIGNMENT].Rows[0]["TerminalID"]);
                    assignment.FreightID = ds.Tables[TBL_ASSIGNMENT].Rows[0]["FreightID"].ToString();
                    assignment.ClientNumber = ds.Tables[TBL_ASSIGNMENT].Rows[0]["ClientNumber"].ToString();
                    assignment.ClientDivisionNumber = ds.Tables[TBL_ASSIGNMENT].Rows[0]["ClientDivisionNumber"].ToString();
                    assignment.Cartons = Convert.ToInt32(ds.Tables[TBL_ASSIGNMENT].Rows[0]["Cartons"]);
                    assignment.SortTypeID = Convert.ToInt32(ds.Tables[TBL_ASSIGNMENT].Rows[0]["SortTypeID"]);
                }
            }
            catch(Exception ex) { throw new FaultException(new FaultReason("Failed to read scanner assignment: " + ex.Message)); }
            return assignment;
        }
        public bool CreateSortedItem(ScannedItem item) {
            //
           bool ret=false;
            try {
                object[] _item = new object[] { item.ItemNumber, item.TerminalID, item.ScannerID, item.FreightID, item.SortDate, item.ScanString, item.UserID };
                ret = executeNonQuery(USP_SORTEDITEM_NEW, _item);
            }
            catch(Exception ex) { throw new FaultException(new FaultReason("Failed to create scanned item: " + ex.Message)); }
            return ret;
        }
        public bool DeleteSortedItem(ScannedItem item) {
            //
            bool ret=false;
            try {
                object[] _item = new object[] { item.ItemNumber };
                ret = executeNonQuery(USP_SORTEDITEM_UPDATE, _item);
            }
            catch(Exception ex) { throw new FaultException(new FaultReason("Failed to delete scanned item: " + ex.Message)); }
            return ret;
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
        #endregion
    }
}