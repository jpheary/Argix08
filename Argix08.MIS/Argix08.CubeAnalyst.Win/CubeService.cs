using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Data.Common;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix.MIS {
	//
	public class CubeService {
		//Members
        private string mConnectionID = "";

        private const string USP_CUBESTATS = "uspToolsCubeStatsGetList",TBL_CUBESTATS = "CubeStatisticsTable";
        private const string USP_CUBESTATSSUM = "uspToolsCubeStatsSummaryGetList",TBL_CUBESTATSSUM = "CubeStatisticsSummaryTable";
        private const string USP_CUBEDETAILS = "uspToolsCubeDetailsGetList",TBL_CUBEDETAILS = "CubeDetailsTable";
        private const string USP_CUBEDETAIL_SEARCH = "uspToolsCubeDetailsSearch";
        private const string USP_TRACELOG_GETLIST = "uspToolsCubeTraceLogGet",TBL_TRACELOG_GETLIST = "ArgixLogTable";
        private const string USP_TRACELOG_DELETE = "uspToolsTraceLogDelete";
        private const string USP_SCANDIRECTGET = "uspToolsCubeSortedItemGet",TBL_SCANDIRECT = "ScanDetailsTable";
        //private const string USP_SCANINDIRECTGET = "", TBL_SCANINDIRECT = "ScanDetailsTable";
        private const int CMD_TIMEOUT = 900;

        //Interface
        public CubeService(string connectionID) {
            //Constructor
            this.mConnectionID = connectionID;
        }
        public ScannerDS GetCubeStats(string sourceName,DateTime startDate,DateTime endDate) {
            //
            ScannerDS stats = null;
            try {
                //Clear cuurent collection and re-populate
                stats = new ScannerDS();
                DataSet ds = fillDataset(USP_CUBESTATS,TBL_CUBESTATS,new object[] { sourceName,startDate,endDate });
                if(ds.Tables[TBL_CUBESTATS].Rows.Count > 0) {
                    ScannerDS _ds = new ScannerDS();
                    _ds.Merge(ds);
                    stats.Merge(_ds.CubeStatisticsTable.Select("","DATE DESC, HOUR DESC"));
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error reading cube statistics.",ex); }
            return stats;
        }
        public ScannerDS GetCubeStatsSummary(string sourceName,DateTime startDate,DateTime endDate) {
            //
            ScannerDS summary = null;
            try {
                //Clear cuurent collection and re-populate
                summary = new ScannerDS();
                DataSet ds = fillDataset(USP_CUBESTATSSUM,TBL_CUBESTATSSUM,new object[] { sourceName,startDate,endDate });
                if(ds != null)
                    summary.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error reading cube statistics summary.",ex); }
            return summary;
        }
        public ScannerDS GetCubeDetails(string sourceName,DateTime startDate, DateTime endDate) {
            //
            ScannerDS details=null; 
            try {
                //Clear cuurent collection and re-populate
                details = new ScannerDS();
                DataSet ds = fillDataset(USP_CUBEDETAILS,TBL_CUBEDETAILS,new object[] { sourceName,startDate,endDate });
                if(ds != null) {
                    ScannerDS _ds = new ScannerDS();
                    _ds.Merge(ds);
                    details.Merge(_ds.Tables[TBL_CUBEDETAILS].Select("","CubeDate DESC"));
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error reading cube details.",ex); }
            return details;
        }
        public ScannerDS GetLogEntries(string eventLogName,DateTime startDate, DateTime endDate) {
            //Refresh data for this object
            ScannerDS logEntries=null;
            try {
                logEntries = new ScannerDS();
                DataSet ds = fillDataset(USP_TRACELOG_GETLIST,TBL_TRACELOG_GETLIST,new object[] { eventLogName,startDate,endDate });
                if(ds != null) 
                    logEntries.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error reading log entries.",ex); }
            return logEntries;
        }
        public ScannerDS GetCubeDetail(string labelSequenceNumber) {
            //Refresh data for this object
            ScannerDS detail = null;
            try {
                detail = new ScannerDS();
                DataSet ds = fillDataset(USP_CUBEDETAIL_SEARCH,TBL_CUBEDETAILS,new object[] { labelSequenceNumber });
                if(ds != null)
                    detail.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error reading cube detail.",ex); }
            return detail;
        }
        public bool DeleteLogEntry(long id) {
            //Delete a log entry
            bool ret = false;
            try {
                ret = executeNonQuery(USP_TRACELOG_DELETE,new object[] { id });
            }
            catch(Exception ex) { throw ex; }
            return ret;
        }
        #region Data Services: fillDataset()
        private DataSet fillDataset(string spName,string table,object[] paramValues) {
            //
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase(this.mConnectionID);
            DbCommand cmd = db.GetStoredProcCommand(spName,paramValues);
            cmd.CommandTimeout = CMD_TIMEOUT;
            db.LoadDataSet(cmd,ds,table);
            return ds;
        }
        private bool executeNonQuery(string spName,object[] paramValues) {
            //
            bool ret = false;
            Database db = DatabaseFactory.CreateDatabase(this.mConnectionID);
            int i = db.ExecuteNonQuery(spName,paramValues);
            ret = i > 0;
            return ret;
        }
        #endregion
    }
}
