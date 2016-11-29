using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web.Security;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix {
    //    
    [ServiceBehavior(IncludeExceptionDetailInFaults=true)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class ArgixService {
        //Members
        public const string USP_SORTEDITEMS_BYTERMINAL = "uspSortedItemsByTerminal",TBL_ITEMS = "ItemsTable";
        public const string USP_SORTEDITEMS_BYCLIENT = "uspSortedItemsByClient";
        public const string USP_DELIVERYORDERS_BYTERMINAL = "uspDeliveryOrdersByTerminal";
        public const string USP_DELIVERYORDERS_BYCLIENT = "uspDeliveryOrdersByClient";

        //Interface
        public DataSet GetSortedItemsByTerminal(string startSortDate,string endSortDate,string terminalNumber) {
            //
            DataSet items = null;
            try {
                items = new DataSet();
                DataSet ds = FillDataset(USP_SORTEDITEMS_BYTERMINAL,TBL_ITEMS,new object[] { startSortDate,endSortDate,terminalNumber });
                if(ds.Tables[TBL_ITEMS].Rows.Count > 0) {
                    items.Merge(ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception reading items...",ex); }
            return items;
        }
        public DataSet GetSortedItemsByClient(string startSortDate,string endSortDate,string clientNumber) {
            //
            DataSet items = null;
            try {
                items = new DataSet();
                DataSet ds = FillDataset(USP_SORTEDITEMS_BYCLIENT,TBL_ITEMS,new object[] { startSortDate,endSortDate,clientNumber });
                if(ds.Tables[TBL_ITEMS].Rows.Count > 0) {
                    items.Merge(ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception reading items...",ex); }
            return items;
        }
        public DataSet GetDeliveryOrdersByTerminal(string startRoutingDate,string endRoutingDate,string terminalNumber) {
            //
            DataSet items = null;
            try {
                items = new DataSet();
                DataSet ds = FillDataset(USP_DELIVERYORDERS_BYTERMINAL,TBL_ITEMS,new object[] { startRoutingDate,endRoutingDate,terminalNumber });
                if(ds.Tables[TBL_ITEMS].Rows.Count > 0) {
                    items.Merge(ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception reading orders...",ex); }
            return items;
        }
        public DataSet GetDeliveryOrdersByClient(string startRoutingDate,string endRoutingDate,string clientNumber) {
            //
            DataSet items = null;
            try {
                items = new DataSet();
                DataSet ds = FillDataset(USP_DELIVERYORDERS_BYCLIENT,TBL_ITEMS,new object[] { startRoutingDate,endRoutingDate,clientNumber });
                if(ds.Tables[TBL_ITEMS].Rows.Count > 0) {
                    items.Merge(ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception reading orders...",ex); }
            return items;
        }

        public DataSet FillDataset(string sp,string table,object[] o) {
            //
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("SQLConnection");
            DbCommand cmd = db.GetStoredProcCommand(sp,o);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeout"]);
            db.LoadDataSet(cmd,ds,table);
            return ds;
        }
    }
}