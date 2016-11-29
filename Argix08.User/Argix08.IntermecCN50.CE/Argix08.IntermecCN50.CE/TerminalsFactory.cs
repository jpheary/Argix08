using System;
using System.Collections;
using System.Data;
using System.Data.SqlServerCe;
using System.Reflection;

namespace Argix {
    //
    public class TerminalsFactory {
        //Members
        private const string DB_NAME = @"\Program Files\DriverScan\Tsort.sdf";
        private const string SQL_CONN = @"Data Source=\Program Files\DriverScan\Tsort.sdf;";

        private const string USP_CLIENTS = "SELECT * FROM CLIENT",TBL_CLIENTS = "Client";
        private const string SQL_STORES = "SELECT * FROM STORE WHERE CLIENT_NUMBER='@ClientNumber'",TBL_STORES = "Store";
        private const string SQL_SCANDATA = "INSERT INTO SCANDATA (ScanDateTime, ClientNumber, StoreNumber, TrackingNumber) VALUES ('@ScanDataTime','@ClientNumber','@StoreNumber','@TrackingNumber')";
            
        //Interface
        static TerminalsFactory() { }
        private TerminalsFactory() { }
        public static DataSet GetClients() {
            //
            DataSet clients=null;
            try {
                clients = new DataSet();
                DataSet ds = fillDataset(USP_CLIENTS,TBL_CLIENTS);
                if(ds!=null) {
                    clients.Merge(ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading clients.",ex); }
            return clients;
        }
        public static DataSet GetStores(string clientNumber) {
            //
            DataSet stores=null;
            try {
                stores = new DataSet();
                string sql = SQL_STORES;
                sql = sql.Replace("@ClientNumber",clientNumber);
                DataSet ds = fillDataset(sql,TBL_STORES);
                if(ds!=null) stores.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading stores.",ex); }
            return stores;
        }
        public static bool SaveScan(string clientNumber, string storeNumber, string trackingNumber) {
            //
            string sql = SQL_SCANDATA;
            sql = sql.Replace("@ScanDataTime",DateTime.Now.ToString());
            sql = sql.Replace("@ClientNumber",clientNumber).Replace("@StoreNumber",storeNumber);
            sql = sql.Replace("@TrackingNumber",trackingNumber);
            return executeNonQuery(sql);
        }
   
        #region Data Services: fillDataset(), executeNonQuery()
        private static DataSet fillDataset(string sql,string table) {
            //
            DataSet ds = new DataSet();
            if(System.IO.File.Exists(DB_NAME)) {
                SqlCeConnection oConn = new SqlCeConnection(SQL_CONN);
                SqlCeDataAdapter oAdap = new SqlCeDataAdapter(sql,oConn);
                oAdap.Fill(ds,table);
            }
            return ds;
        }
        private static bool executeNonQuery(string sql) {
        //    //
            bool ret=false;
            if(System.IO.File.Exists(DB_NAME)) {
                SqlCeConnection oConn = new SqlCeConnection(SQL_CONN);
                try {
                    oConn.Open();
                    SqlCeCommand oCmd = new SqlCeCommand(sql,oConn);
                    int rows = oCmd.ExecuteNonQuery();
                    ret = rows > 0;
                }
                finally { oConn.Close(); }
            }
            return ret;
        }
        #endregion
    }
}
