using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix.Freight.Tsort {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class TsortService:ITsortService {
        //Members
        public const string USP_STATION_READ = "uspSortWorkstationGet",TBL_STATION_READ = "StationTable";
        public const string USP_FREIGHTASSIGNMENTS_READ = "uspSortFreightClientShipperGetListForStation",TBL_FREIGHTASSIGNMENTS_READ = "AssignmentsTable";

        //Interface
        public Workstation GetStation(string machinName) {
            //
            Workstation workstation=null;
            try {
                workstation = new Workstation();
                DataSet ds = fillDataset(USP_STATION_READ,TBL_STATION_READ,new object[] { machinName });
                if(ds != null && ds.Tables[0].Rows.Count > 0) {
                    workstation.WorkStationID = ds.Tables[TBL_STATION_READ].Rows[0]["WorkStationID"].ToString();
                    workstation.Name = ds.Tables[TBL_STATION_READ].Rows[0]["Name"].ToString();
                    workstation.TerminalID = Convert.ToInt32(ds.Tables[TBL_STATION_READ].Rows[0]["TerminalID"]);
                    workstation.Number = ds.Tables[TBL_STATION_READ].Rows[0]["Number"].ToString();
                    workstation.Description = ds.Tables[TBL_STATION_READ].Rows[0]["Description"].ToString();
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading station.",ex); }
            return workstation;
        }
        public DataSet GetFreightAssignments(string worhstationID) {
            //
            DataSet assignments=null;
            try {
                assignments = new DataSet();
                DataSet ds = fillDataset(USP_FREIGHTASSIGNMENTS_READ,TBL_FREIGHTASSIGNMENTS_READ,new object[] { worhstationID });
                if(ds != null) assignments.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading station assignments.",ex); }
            return assignments;
        }
        public SortedItem ProcessInputs(string[] inputs,decimal weight, string damageCode, string storeOverride, string freightID) {
            //Process the first input scan
            SortedItem sortedItem=null;
            try {
                //Create a sorted item and re-initialize the inbound label template
                sortedItem = new SortedItem();
                sortedItem.LabelZPL = "";
                sortedItem.LabelZPL += "^XA";
                sortedItem.LabelZPL += "^FWN^CFD,24^PW609^LH0,0";
                sortedItem.LabelZPL += "^FO0,4^FR^GB45,1008,45,B,0^FS";
                sortedItem.LabelZPL += "^AAR,9,5^FO601,6^FR^FD ^FS";
                sortedItem.LabelZPL += "^A0R,25,22^FO7,393^FR^FD^FS";
                sortedItem.LabelZPL += "^A0R,112,74^FO275,360^FR^FD^FS";
                sortedItem.LabelZPL += "^A0R,57,78^FO393,249^FR^FD^FS";
                sortedItem.LabelZPL += "^ADR,18,10^FO458,249^FR^FDStore:^FS";
                sortedItem.LabelZPL += "^A0R,42,24^FO478,249^FR^FD^FS";
                sortedItem.LabelZPL += "^A0R,40,36^FO524,249^FR^FD^FS";
                sortedItem.LabelZPL += "^A0R,67,64^FO161,805^FR^FD^FS";
                sortedItem.LabelZPL += "^A0R,28,28^FO282,822^FR^FD<localRouteLane>^FS";
                sortedItem.LabelZPL += "^FO155,793^FR^GB90,134,5,B,0^FS";
                sortedItem.LabelZPL += "^BY4,2.0^FO315,756^FR^B2R,227,N,N,N^FD<localRouteLane>^FS";
                sortedItem.LabelZPL += "^BY3^FO283,74^FR^BCN,95,N,N,N^FD>:$$^FS";
                sortedItem.LabelZPL += "^A0N,28,28^FO374,170^FR^FD$$^FS";
                sortedItem.LabelZPL += "^A0R,23,30^FO52,290^FR^FD    0^FS";
                sortedItem.LabelZPL += "^BY3^FO80,217^FR^BCR,153,N,N,N^FD>;0^FS";
                sortedItem.LabelZPL += "^A0R,23,16^FO7,600^FR^FDCOPYRIGHT 2010^FS";
                sortedItem.LabelZPL += "^A0R,26,22^FO7,723^FR^FD ARGIX DIRECT ^FS";
                sortedItem.LabelZPL += "^A0R,23,18^FO7,923^FR^FD54A^FS";
                sortedItem.LabelZPL += "^MCY";
                sortedItem.LabelZPL += "^XZ";

            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while processing carton inputs.",ex); }
            return sortedItem;
        }

        #region Data Services: fillDataset(), executeNonQuery(), executeNonQueryWithReturn()
        private DataSet fillDataset(string spName,string table,object[] paramValues) {
            //
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("SQLConnection");
            DbCommand cmd = db.GetStoredProcCommand(spName,paramValues);
            db.LoadDataSet(cmd,ds,table);
            return ds;
        }
        private bool executeNonQuery(string spName,object[] paramValues) {
            //
            bool ret=false;
            Database db = DatabaseFactory.CreateDatabase("SQLConnection");
            int i = db.ExecuteNonQuery(spName,paramValues);
            ret = i > 0;
            return ret;
        }
        private object executeNonQueryWithReturn(string spName,object[] paramValues) {
            //
            object ret=null;
            if((paramValues != null) && (paramValues.Length > 0)) {
                Database db = DatabaseFactory.CreateDatabase("SQLConnection");
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
        #endregion
    }
}
