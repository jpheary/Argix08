using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Services;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

[WebService(Namespace="http://tempuri.org/")]
[WebServiceBinding(ConformsTo=WsiProfiles.BasicProfile1_1)]
public class ScannerService:System.Web.Services.WebService {
    //Members
    private const string SQL_CONNID = "Scanner";
    public const string USP_ASSIGNMENT_GET = "uspScannerAssignmentGet",TBL_ASSIGNMENT = "ScannerAssignmentTable";
    public const string USP_SORTEDITEM_NEW = "uspScannedItemNew";
    public const string USP_SORTEDITEM_UPDATE = "uspScannedItemDelete";

    //Interface
    public ScannerService() { }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World!"; ;
    }

    //[WebMethod]
    //public void WriteLogEntry(TraceMessage m) {
    //    //Write o to database log if event level is severe enough
    //    new Argix.AppService(SQL_CONNID).WriteLogEntry(m);
    //}

    [WebMethod]
    public ScannerAssignmentDS GetScannerAssignment(string scannerNumber) {
        //
        ScannerAssignmentDS assignment=null;
        try {
            assignment = new ScannerAssignmentDS();
            DataSet ds = fillDataset(USP_ASSIGNMENT_GET,TBL_ASSIGNMENT,new object[] { scannerNumber });
            if(ds.Tables[TBL_ASSIGNMENT].Rows.Count > 0) {
                assignment.Merge(ds);
            }
        }
        catch(Exception ex) { throw new ApplicationException("Failed to read scanner assignment.", ex); }
        return assignment;
    }
    [WebMethod]
    public bool CreateSortedItem(ScannedItemDS items) {
        //
        bool ret=false;
        try {
            if(items.ScannedItemTable.Rows.Count > 0) {
                ScannedItemDS.ScannedItemTableRow item = items.ScannedItemTable[0];
                object[] _item = new object[] { item.ItemNumber,item.TerminalID,item.ScannerID,item.FreightID,item.SortDate,item.ScanString,item.UserID };
                ret = executeNonQuery(USP_SORTEDITEM_NEW,_item);
            }
            else
                throw new ApplicationException("No scanned item to create.");
        }
        catch(Exception ex) { throw new ApplicationException("Failed to create scanned item.",ex); }
        return ret;
    }
    //[WebMethod]
    //public bool DeleteSortedItem(ScannedItem item) {
    //    //
    //    bool ret=false;
    //    try {
    //        object[] _item = new object[] { item.ItemNumber };
    //        ret = executeNonQuery(USP_SORTEDITEM_UPDATE,_item);
    //    }
    //    catch(Exception ex) { throw new ApplicationException("Failed to delete scanned item.",ex); }
    //    return ret;
    //}

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

