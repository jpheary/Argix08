using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace Argix.Terminals {
    //
    public class RoadshowGateway {
        //Members
        private const string SQL_CONNID = "Roadshow";
        private const string USP_CUSTOMERS = "uspDeliveryPointsCustomerGetList", TBL_CUSTOMERS = "CustomerTable";

        //Interface
        public RoadshowGateway() { }
        public DataSet GetCustomers() {
            //
            DataSet customers = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_CUSTOMERS,TBL_CUSTOMERS,new object[] { });
                if (ds != null && ds.Tables[TBL_CUSTOMERS].Rows.Count > 0) customers.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading customers.",ex); }
            return customers;
        }
    }
}
