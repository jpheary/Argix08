using System;
using System.Collections;
using System.Data;
using System.Data.SqlServerCe;
using System.Reflection;

namespace Argix {
    //
    public class TerminalsFactory {
        //Members
        private const string DB_NAME = @"\Program Files\argix08.intermeccn50.wm\Tsort.sdf";
        private const string SQL_CONN = @"Data Source=\Program Files\argix08.intermeccn50.wm\Tsort.sdf;";

        private const string SQL_GPSDATA = "INSERT INTO GPSDATA (Latitude, Longitude, Altitude, LocationDateTime) VALUES ('@Latitude','@Longitude','@Altitude','@LocationDateTime')";
            
        //Interface
        static TerminalsFactory() { }
        private TerminalsFactory() { }
        public static bool SaveGPSData(int latitude, int longitude, int altitude, DateTime locationDateTime) {
            //
            string sql = SQL_GPSDATA;
            sql = sql.Replace("@Latitude",latitude.ToString());
            sql = sql.Replace("@Longitude",longitude.ToString());
            sql = sql.Replace("@Altitude",altitude.ToString());
            sql = sql.Replace("@LocationDateTime",locationDateTime.ToString());
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
