using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Data.Common;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

public class EnterpriseService {
	//Members	
    private const string SQL_CONNID = "SQLConnection";
    public const string USP_CLIENTS = "uspClientGetList",TBL_CLIENTS = "ClientTable";
    public const string USP_LOADTENDERS = "uspLoadTenderGetList",TBL_LOADTENDERS = "LoadTenderTable";
    public const int CMD_TIMEOUT_DEFAULT = 300;
	
	//Interface
    public ClientDS GetClients() {
		//Get a list of clients
        ClientDS clients = null;
        try {
            clients = new ClientDS();
            clients.ClientTable.AddClientTableRow("012", "01", "L'OCCITANE", "05", "A");
            clients.ClientTable.AddClientTableRow("014", "01", "MELVITA", "05", "A");
            clients.ClientTable.AddClientTableRow("025", "01", "PRATT RETAIL SPECIALTIES", "05", "A");
            clients.ClientTable.AcceptChanges();
        }
        catch(ApplicationException ex) { throw ex; }
        catch(Exception ex) { throw new ApplicationException("Unexpected exception creating client list.",ex); }
        return clients;
    }
    public LoadTenderDS GetLoadTenders(string clientNumber,DateTime startDate,DateTime endDate) {
        //Event handler for change in selected terminal
        LoadTenderDS lts = null;
        try {
            lts = new LoadTenderDS();
            DataSet ds = fillDataset(USP_LOADTENDERS,TBL_LOADTENDERS,new object[] { clientNumber,startDate.ToString("yyyy-MM-dd"),endDate.ToString("yyyy-MM-dd") });
            if(ds.Tables[TBL_LOADTENDERS].Rows.Count > 0)
                lts.Merge(ds);
        }
        catch(ApplicationException ex) { throw ex; }
        catch(Exception ex) { throw new ApplicationException("Unexpected exception while reading load tenders.",ex); }
        return lts;
    }

    #region Data Services: fillDataset()
    private DataSet fillDataset(string spName,string table,object[] paramValues) {
        //
        DataSet ds = new DataSet();
        Database db = DatabaseFactory.CreateDatabase(SQL_CONNID);
        DbCommand cmd = db.GetStoredProcCommand(spName,paramValues);
        db.LoadDataSet(cmd,ds,table);
        return ds;
    }
    #endregion
}
