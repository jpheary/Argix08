using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Threading;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix.Freight {
	//
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall,IncludeExceptionDetailInFaults=true)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class FreightService:IFreightService {
        //Members
        public const string USP_DELIVERY = "uspIMDeliveryGetList1 ",TBL_DELIVERY = "DeliveryTable";
        public const string USP_OSDSCANS = "uspIMDeliveryOSDScansGetList ",TBL_OSDSCANS = "ScanTable";
        public const string USP_PODSCANS = "uspIMDeliveryPODScansGetList ",TBL_PODSCANS = "ScanTable";

        //Interface
        public FreightService() { }

        public DeliveryDS GetDeliveries(int companyID,int storeNumber,DateTime from,DateTime to) {
            //Get a list of store locations
            DeliveryDS deliveries = null;
            try {
                deliveries = new DeliveryDS();
                DataSet ds = fillDataset(USP_DELIVERY,TBL_DELIVERY,new object[] { companyID,storeNumber,from,to });
                if(ds.Tables[TBL_DELIVERY].Rows.Count > 0)
                    deliveries.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading deliveries.",ex); }
            return deliveries;
        }
        public DeliveryDS GetDelivery(int companyID,int storeNumber,DateTime from,DateTime to,long proID) {
            //Get a list of store locations
            DeliveryDS delivery = null;
            try {
                delivery = new DeliveryDS();
                DataSet ds = fillDataset(USP_DELIVERY,TBL_DELIVERY,new object[] { companyID,storeNumber,from,to });
                if(ds.Tables[TBL_DELIVERY].Rows.Count > 0) {
                    DeliveryDS d = new DeliveryDS();
                    d.Merge(ds);
                    delivery.Merge(d.DeliveryTable.Select("CPROID=" + proID));
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading delivery.",ex); }
            return delivery;
        }
        public ScanDS GetOSDScans(long cProID) {
            //Get a list of store locations
            ScanDS scans = null;
            try {
                scans = new ScanDS();
                DataSet ds = fillDataset(USP_OSDSCANS,TBL_OSDSCANS,new object[] { cProID });
                if(ds.Tables[TBL_OSDSCANS].Rows.Count > 0) scans.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading OS&D scans.",ex); }
            return scans;
        }
        public ScanDS GetPODScans(long cProID) {
            //Get a list of store locations
            ScanDS scans = null;
            try {
                scans = new ScanDS();
                DataSet ds = fillDataset(USP_PODSCANS,TBL_PODSCANS,new object[] { cProID });
                if(ds.Tables[TBL_PODSCANS].Rows.Count > 0) scans.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading POD scans.",ex); }
            return scans;
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