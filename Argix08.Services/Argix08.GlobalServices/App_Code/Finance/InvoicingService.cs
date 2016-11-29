using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix.Finance {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class InvoicingService: IInvoicingService {
        //Members
        private const string SQL_CONNID = "Invoicing";
        private const string USP_CLIENTS = "uspInvClientGetList",TBL_CLIENTS = "InvoiceClientTable";
        private const string USP_INVOICES = "uspInvClientInvoiceGetListAllTypes",TBL_INVOICES = "ClientInvoiceTable";

        //Interface
        public InvoicingService() { }
        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get configuration data for the specified application and usernames
            return new Argix.AppService(SQL_CONNID).GetUserConfiguration(application,usernames);
        }
        public void WriteLogEntry(TraceMessage m) {
            //Write o to database log if event level is severe enough
            new Argix.AppService(SQL_CONNID).WriteLogEntry(m);
        }
        public Argix.Enterprise.TerminalInfo GetTerminalInfo() {
            //Get the operating enterprise terminal
            return new Argix.Enterprise.EnterpriseService(SQL_CONNID).GetTerminalInfo();
        }

        public Argix.Enterprise.Clients GetClients() {
            //Get a list of clients
            Argix.Enterprise.Clients clients = null;
            try {
                clients = new Argix.Enterprise.Clients();
                DataSet ds = fillDataset(USP_CLIENTS,TBL_CLIENTS,new object[] { });
                if(ds.Tables[TBL_CLIENTS].Rows.Count > 0) {
                    ClientDS __clients = new ClientDS();
                    __clients.Merge(ds);
                    ClientDS _clients = new ClientDS();

                    _clients.Merge(__clients.InvoiceClientTable.Select("DivisionNumber='01'","ClientName ASC"));

                    for(int i=0;i<_clients.InvoiceClientTable.Rows.Count;i++) {
                        if(InvoicingConfig.Document.DocumentElement.SelectSingleNode("//client[@number='" + _clients.InvoiceClientTable[i].ClientNumber + "']") != null) {
                            Argix.Enterprise.Client client = new Argix.Enterprise.Client(_clients.InvoiceClientTable[i]);
                            clients.Add(client);
                        }
                    }
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating client list.",ex); }
            return clients;
        }
        public Invoices GetClientInvoices(string clientNumber,string clientDivision,string startDate) {
            //Get a list of clients invoices filtered for a specific division
            Invoices invoices = null;
            try {
                invoices = new Invoices();
                DataSet ds = fillDataset(USP_INVOICES,TBL_INVOICES,new object[] { clientNumber,clientDivision,startDate });
                if(ds.Tables[TBL_INVOICES].Rows.Count > 0) {
                    InvoiceDS __invoices = new InvoiceDS();
                    __invoices.Merge(ds);

                    string filter = getInvoiceFilter(clientNumber);
                    InvoiceDS _invoices = new InvoiceDS();
                    _invoices.Merge(__invoices.ClientInvoiceTable.Select(filter,"InvoiceNumber ASC"));

                    System.Xml.XmlNode node = InvoicingConfig.Document.DocumentElement.SelectSingleNode("//client[@number='" + clientNumber + "']");
                    if(node != null) {
                        System.Xml.XmlNode inv = node.SelectSingleNode("invoices");
                        if(inv != null) {
                            for(int i=0;i<_invoices.ClientInvoiceTable.Rows.Count;i++) {
                                string invoiceType = _invoices.ClientInvoiceTable[i].InvoiceTypeCode.Trim();
                                if(inv.Attributes[invoiceType] != null) _invoices.ClientInvoiceTable[i].InvoiceTypeTarget = inv.Attributes[invoiceType].Value;

                                Invoice invoice = new Invoice(_invoices.ClientInvoiceTable[i]);
                                invoices.Add(invoice);
                            }
                        }
                    }
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating client invoice list.",ex); }
            return invoices;
        }
        
        private string getInvoiceFilter(string clientNumber) {
            //Get invoice document target for specified client and invoice type
            string filter="InvoiceTypeCode=''";

            System.Xml.XmlNode node = InvoicingConfig.Document.DocumentElement.SelectSingleNode("//client[@number='" + clientNumber + "']");
            if(node != null) {
                System.Xml.XmlNode inv = node.SelectSingleNode("invoices");
                if(inv != null) {
                    string types = inv.Attributes["types"].Value;
                    if(types == "")
                        filter="InvoiceTypeCode=''";
                    else if(types == "*")
                        filter="";
                    else {
                        string[] codes = types.Split(',');
                        filter="";
                        for(int i=0;i<codes.Length;i++) {
                            if(i > 0) filter += " OR ";
                            filter += "InvoiceTypeCode='" + codes[i].Trim() + "'";
                        }
                    }
                }
            }
            return filter;
        }
        private string getTarget(string clientNumber,string invoiceType) {
            //Get invoice document target for specified client and invoice type
            string target="";

            System.Xml.XmlNode node = InvoicingConfig.Document.DocumentElement.SelectSingleNode("//client[@number='" + clientNumber + "']");
            if(node != null) {
                System.Xml.XmlNode inv = node.SelectSingleNode("invoices");
                if(inv != null) {
                    if(inv.Attributes[invoiceType] != null) target = inv.Attributes[invoiceType].Value;
                }
            }
            return target;
        }

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

    internal static class InvoicingConfig {
        //Members
        private static System.Xml.XmlDocument xmlConfig=null;

        //Interface
        static InvoicingConfig() {
            xmlConfig = new System.Xml.XmlDocument();
            string path = System.Web.Hosting.HostingEnvironment.MapPath("~\\App_Data\\Invoicing.xml");
            xmlConfig.Load(path);
        }
        public static System.Xml.XmlDocument Document { get { return xmlConfig; } }
    }
}
