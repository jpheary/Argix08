using System;
using System.Data;
using System.Web.Configuration;
using System.Web;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix {
    //
    public class InvoiceService {
        //Members
        private const string USP_CLIENTS = "uspInvClientGetList",TBL_CLIENTS = "ClientTable";
        private const string USP_INVOICES = "uspInvClientInvoiceGetListAllTypes",TBL_INVOICES = "ClientInvoiceTable";

        //Interface
        public InvoiceService() { }
        public ClientDS GetClients() {
            //Get a list of clients filtered for a specific division
            ClientDS clients = null;
            try {
                clients = new ClientDS();
                DataSet ds = fillDataset(USP_CLIENTS,TBL_CLIENTS,new object[] { });
                if(ds.Tables[TBL_CLIENTS].Rows.Count > 0) {
                    ClientDS _clients = new ClientDS();
                    _clients.Merge(ds);
                    clients.Merge(_clients.ClientTable.Select("","ClientName ASC"));
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating client list.",ex); }
            return clients;
        }
        public InvoiceDS GetClientInvoices(string clientNumber,string clientDivision,string startDate, string filter) {
            //Get a list of clients invoices filtered for a specific division
            InvoiceDS invoices = null;
            try {
                invoices = new InvoiceDS();
                DataSet ds = fillDataset(USP_INVOICES,TBL_INVOICES,new object[] { clientNumber,clientDivision,startDate });
                if(ds.Tables[TBL_INVOICES].Rows.Count > 0) {
                    InvoiceDS _invoices = new InvoiceDS();
                    _invoices.Merge(ds);
                    invoices.Merge(_invoices.ClientInvoiceTable.Select(filter,"InvoiceNumber ASC"));
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating client invoice list.",ex); }
            return invoices;
        }

        private DataSet fillDataset(string sp,string table,object[] o) {
            //
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand(sp,o);
            cmd.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CommandTimeout"]);
            DataSet ds = new DataSet();
            db.LoadDataSet(cmd,ds,table);
            return ds;
        }
    }
}
