using System;
using System.Collections.Generic;
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

namespace Argix.AgentLineHaul {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class ISDExportService: IISDExportService {
        //Members
        private const string SQL_CONNID = "ISDExport";
        public const string USP_PICKUPS = "uspISDExportPickupGetListForPUDate",TBL_PICKUPS = "PickupTable";
        public const string USP_SORTEDITEMS = "uspISDExportSortedItemGetList",TBL_SORTEDITEMS = "SortedItemTable";
        public const string USP_EXPORTFILECONFIG = "uspISDExportFileConfigurationGet",TBL_EXPORTFILECONFIG = "ClientTable";
        public const string USP_EXPORTFILECONFIGCREATE = "uspISDExportFileConfigurationCreate";
        public const string USP_EXPORTFILECONFIGUPDATE = "uspISDExportFileConfigurationUpdate";
        public const string USP_EXPORTFILECONFIGDELETE = "uspISDExportFileConfigurationDelete";
        public const string USP_EXPORTFILENAME = "uspISDExportFileNameGet",TBL_EXPORTFILENAME = "ClientTable";
               
        //Interface
        private ISDExportService() { }
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
        public Argix.Freight.Pickups GetPickups(DateTime pickupDate) {
            //Get a collection of all pickups for the terminal on the local LAN database
            Argix.Freight.Pickups pickups=null;
            try {
                pickups = new Argix.Freight.Pickups();
                DataSet ds = fillDataset(USP_PICKUPS,TBL_PICKUPS,new object[] { pickupDate });
                if(ds.Tables[TBL_PICKUPS].Rows.Count > 0) {
                    PickupDS _pickups = new PickupDS();
                    _pickups.Merge(ds);
                    for(int i=0;i<_pickups.PickupTable.Rows.Count;i++) {
                        Argix.Freight.Pickup pickup = new Argix.Freight.Pickup(_pickups.PickupTable[i]);
                        pickups.Add(pickup);
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading pickups.", ex); }
            return pickups;
        }
        public Argix.Freight.SortedItems GetSortedItems(string pickupID) {
            //Get sorted items for a pickup
            Argix.Freight.SortedItems sortedItems=null;
            try {
                sortedItems = new Argix.Freight.SortedItems();
                DataSet ds = fillDataset(USP_SORTEDITEMS,TBL_SORTEDITEMS,new object[] { pickupID });
                if(ds != null) {
                    SortedItemDS _items = new SortedItemDS();
                    _items.Merge(ds);
                    for(int i=0;i<_items.SortedItemTable.Rows.Count;i++) {
                        Argix.Freight.SortedItem item = new Argix.Freight.SortedItem(_items.SortedItemTable[i]);
                        sortedItems.Add(item);
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading sorted items.",ex); }
            return sortedItems;
        }
        public string GetExportFilename(string counterKey) {
            string filename="";
            try {
                DataSet ds = fillDataset(USP_EXPORTFILENAME,TBL_EXPORTFILENAME,new object[] { counterKey });
                filename = ds.Tables[TBL_EXPORTFILENAME].Rows[0]["FILENAME"].ToString();
            }
            catch(Exception ex) { throw new ApplicationException("",ex); }
            return filename;
        }
        public ISDClients GetClients(string clientNumber) {
            ISDClients clients=null;
            try {
                clients = new ISDClients();
                DataSet ds = fillDataset(USP_EXPORTFILECONFIG,TBL_EXPORTFILECONFIG,new object[] { clientNumber });
                if(ds != null) {
                    ISDClientDS _clients = new ISDClientDS();
                    _clients.Merge(ds);
                    for(int i=0;i<_clients.ClientTable.Rows.Count;i++) {
                        ISDClient client = new ISDClient(_clients.ClientTable[i]);
                        clients.Add(client);
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("",ex); }
            return clients;
        }
        public ISDClients GetClients() {
            //Refresh view
            ISDClients clients=null;
            try {
                clients = new ISDClients();
                DataSet ds = fillDataset(USP_EXPORTFILECONFIG,TBL_EXPORTFILECONFIG,new object[] { null });
                if(ds != null) {
                    ISDClientDS _clients = new ISDClientDS();
                    _clients.Merge(ds);
                    for(int i=0;i<_clients.ClientTable.Rows.Count;i++) {
                        ISDClient client = new ISDClient(_clients.ClientTable[i]);
                        clients.Add(client);
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("",ex); }
            return clients;
        }
        public bool CreateClient(string clid,string format,string path,string key,string client,string scanner,string userid) {
            //
            bool created=false;
            created = executeNonQuery(USP_EXPORTFILECONFIGCREATE,new object[] { clid,format,path,key,client,scanner,userid });
            return created;
        }
        public bool UpdateClient(string clid,string format,string path,string key,string client,string scanner,string userid) {
            //
            bool updated=false;
            updated = executeNonQuery(USP_EXPORTFILECONFIGUPDATE,new object[] { clid,format,path,key,client,scanner,userid });
            return updated;
        }
        public bool DeleteClient(string clid,string format,string path,string key,string client,string scanner,string userid) {
            //
            bool deleted=false;
            deleted = executeNonQuery(USP_EXPORTFILECONFIGDELETE,new object[] { clid,format,path,key,client,scanner,userid });
            return deleted;
        }

        #region Data Services: fillDataset(), executeNonQuery()
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
        #endregion
    }
}
