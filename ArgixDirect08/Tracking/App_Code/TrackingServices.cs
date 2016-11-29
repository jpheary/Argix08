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

public class TrackingServices {
    //Members
    private const string USP_CUSTOMERS = "uspTrackingCustomerGetList",TBL_CUSTOMERS = "ClientTable";
    private const string USP_CLIENTS = "uspTrackingClientGetList",TBL_CLIENTS = "ClientTable";
    private const string USP_BYLABEL = "uspTrackingGetListForLabels2", TBL_BYLABEL = "CartonDetailTable";
    private const string USP_BYCARTON = "uspTrackingGetListForCartons2",TBL_BYCARTON = "CartonDetailTable";
    private const string USP_BYPLATE = "uspTrackingGetListForTrackingNumbers",TBL_BYPLATE = "CartonDetailTable";
    private const string USP_BYPICKUP = "uspTrackingPickupCartonsGetListForStore",TBL_BYPICKUP = "CartonDetailForStoreTable";
    private const string USP_BYDELIVERY = "uspTrackingDeliveryCartonsGetListForStore",TBL_BYDELIVERY = "CartonDetailForStoreTable";
    private const string USP_BYPO = "uspTrackingGetListForPO",TBL_BYPO = "CartonDetailTable";
    private const string USP_BYPRO = "uspTrackingGetListForShipment",TBL_BYPRO = "CartonDetailTable";
    private const string USP_STORESFORSUBSTORE = "uspTrackingStoreForSubStoreGetList",TBL_STORESFORSUBSTORE = "StoreTable";

    public const string ID_ARGIX = "000";
    public const string SEARCHBY_LABELNUMBER = "LabelNumber", SEARCHBY_CARTONNUMBER = "CartonNumber", SEARCHBY_PLATENUMBER = "PlateNumber";
    public const string SEARCHBY_STORE = "Store", SEARCHBY_PRO = "Pro", SEARCHBY_PO = "PO";
    
    public const int SCANTYPE_SORT = 0, SCANTYPE_AGENT = 1, SCANTYPE_STORE = 3;

    //Interface
    public TrackingServices() { }
    public EnterpriseDS GetCustomers() {
        //Returns a list of clients and vendors used to identify a users 'customer' affiliation
        EnterpriseDS cutomers = new EnterpriseDS();
        DataSet ds = fillDataset(USP_CUSTOMERS,TBL_CUSTOMERS,new object[] { });
        EnterpriseDS c = new EnterpriseDS();
        c.Merge(ds);
        
        cutomers.ClientTable.AddClientTableRow(ID_ARGIX,"","Argix Direct Inc.","client");
        cutomers.ClientTable.AddClientTableRow("",ID_ARGIX,"Argix Direct Inc.","vendor");
        cutomers.Merge(c.ClientTable.Select("","CompanyName ASC"));
        cutomers.AcceptChanges();
        return cutomers;
    }
    public EnterpriseDS GetCustomers(string companyType) {
        //Get customer list
        EnterpriseDS ds = new EnterpriseDS();
        EnterpriseDS _ds = GetCustomers();
        ds.Merge(_ds.ClientTable.Select("CompanyType='" + companyType + "'"));
        return ds;
    }
    public EnterpriseDS GetClients() {
        //Return a list of all Argix clients
        EnterpriseDS clients = new EnterpriseDS();
        DataSet ds = fillDataset(USP_CLIENTS,TBL_CLIENTS,new object[] { null });
        if(ds.Tables["ClientTable"].Rows.Count > 0) clients.Merge(ds.Tables["ClientTable"].Select("","CompanyName ASC"));
        return clients;
    }
    public EnterpriseDS GetClients(string vendorID) {
        //Return a list of clients filtered by vendorID (vendorID=null returns all Argix clients)
        EnterpriseDS clients = new EnterpriseDS();
        DataSet ds = fillDataset(USP_CLIENTS,TBL_CLIENTS,new object[] { vendorID });
        if(ds.Tables["ClientTable"].Rows.Count > 0) clients.Merge(ds.Tables["ClientTable"].Select("","CompanyName ASC"));
        return clients;
    }
    public EnterpriseDS GetSecureClients() {
        //Get a list of clients
        EnterpriseDS clients = new EnterpriseDS();

        //If user is:
        // Vendor: get list of all it's clients
        // Client: no need to get client's list - fill the drop-down with client's name
        //  Argix: get list of all clients
        MembershipServices membership = new MembershipServices();
        ProfileCommon profile = membership.MemberProfile;
        if(profile.ClientVendorID == TrackingServices.ID_ARGIX || membership.IsAdmin) {
            clients.Merge(GetClients(null));
        }
        else {
            if(profile.Type.ToLower() == "vendor")
                clients.Merge(GetClients(profile.ClientVendorID));
            else
                clients.ClientTable.AddClientTableRow(profile.ClientVendorID,"",profile.Company,"");
        }
        return clients;
    }
    public EnterpriseDS GetStoresForSubStore(string subStoreNumber,string clientID,string vendorID) {
        //Get a list of client\vendor stores for the specified sub-store number
        EnterpriseDS stores = new EnterpriseDS();
        DataSet ds = fillDataset(USP_STORESFORSUBSTORE,TBL_STORESFORSUBSTORE,new object[] { subStoreNumber,clientID,vendorID });
        if(ds != null) {
            stores.Merge(ds);
            for(int i=0;i<stores.StoreTable.Rows.Count;i++)
                stores.StoreTable[i].DESCRIPTION =  stores.StoreTable[i].NAME + " " +
                                                    stores.StoreTable[i].ADDRESSLINE1 + ", " + 
                                                    stores.StoreTable[i].ADDRESS_LINE2 + " " +
                                                    stores.StoreTable[i].CITY + ", " +
                                                    stores.StoreTable[i].STATE + " " +
                                                    stores.StoreTable[i].ZIP;
        }
        return stores;
    }
    
    public TrackingDS GetCartons(string trackingNumbers,string searchBy,string companyType,string companyID) {
        //Get a list of cartons (details) for the specified tracking number (carton or label sequence)
        //One or two records are returned for each carton: ScanType=0: 1; ScanType=1: 1, ScanType=3: 2 (ScanTypes 1, 3)
        TrackingDS cartons = new TrackingDS();
        string usp="", tbl="";
        switch(searchBy) {
            case SEARCHBY_LABELNUMBER: usp=USP_BYLABEL; tbl=TBL_BYLABEL; break;
            case SEARCHBY_CARTONNUMBER: usp=USP_BYCARTON; tbl=TBL_BYCARTON; break;
            case SEARCHBY_PLATENUMBER: usp=USP_BYPLATE; tbl=TBL_BYPLATE; break;
        }
        DataSet ds=null;
        if(companyID != ID_ARGIX && companyType.ToLower() == "client")
            ds = fillDataset(usp,tbl,new object[] { trackingNumbers,companyID,null });
        else if(companyID != ID_ARGIX && companyType.ToLower() == "vendor")
            ds = fillDataset(usp,tbl,new object[] { trackingNumbers,null,companyID });
        else
            ds = fillDataset(usp,tbl,new object[] { trackingNumbers,null,null });
        if(ds != null && ds.Tables[tbl].Rows.Count > 0) {
            DataView filterView = ds.Tables[tbl].DefaultView;
            filterView.Sort = "CTN,BL DESC,SCNTP DESC,SCD DESC,SCT DESC";
            DataTable dataTable = getUniqueRows(filterView);
            cartons.Merge(dataTable.Select());
        }
        return cartons;
    }
    public TrackingDS GetCartonsForStore(string clientID, string store, DateTime fromDate, DateTime toDate, string vendorID, bool searchByPickup) {
        //Get a list of cartons (details) for the specified store by pickup or delivery
        TrackingDS cartons = new TrackingDS();
        string usp = searchByPickup ? USP_BYPICKUP : USP_BYDELIVERY;
        string tbl = searchByPickup ? TBL_BYPICKUP : TBL_BYDELIVERY;
        DataSet ds = fillDataset(usp,tbl,new object[] { clientID,store,fromDate.ToString("yyyy-MM-dd"),toDate.ToString("yyyy-MM-dd"),vendorID });
        cartons.Merge(ds.Tables[tbl]);
        return cartons;
    }
    public TrackingDS GetCartonsForPO(string client, string PO) {
        //Get a list of cartons (details) for the specified client and PO number
        TrackingDS cartons = new TrackingDS();
        TrackingDS _cartons = new TrackingDS();
        DataSet ds = fillDataset(USP_BYPO,TBL_BYPO,new object[] { client,PO });
        _cartons.Merge(ds.Tables[TBL_BYPO]);
        if(_cartons != null && _cartons.Tables[TBL_BYPO].Rows.Count > 0) {
            DataView view = _cartons.Tables[TBL_BYPO].DefaultView;
            view.Sort = "CTN,BL DESC,SCNTP DESC,SCD DESC,SCT DESC";
            DataTable table = getUniqueRows(view);
            cartons.Merge(table.Select());
        }
        return cartons;
    }
    public TrackingDS GetCartonsForPRO(string client, string shipment) {
        //Get a list of cartons (details) for the specified client and PO number
        TrackingDS cartons = new TrackingDS();
        TrackingDS _cartons = new TrackingDS();
        DataSet ds = fillDataset(USP_BYPRO,TBL_BYPRO,new object[] { client,shipment });
        _cartons.Merge(ds.Tables[TBL_BYPRO]);
        if(_cartons != null && _cartons.Tables[TBL_BYPRO].Rows.Count > 0) {
            DataView view = _cartons.Tables[TBL_BYPRO].DefaultView;
            view.Sort = "CTN,BL DESC,SCNTP DESC,SCD DESC,SCT DESC";
            DataTable table = getUniqueRows(view);
            cartons.Merge(table.Select());
        }
        return cartons;
    }

    public DataSet GetReportData(string sp,string tablename,object[] parameters) {
        return fillDataset(sp,tablename,parameters);
    }

    private DataTable getUniqueRows(DataView dataView) {
        //Get 
        DataTable dataTable = dataView.Table.Clone();
        int rowCount = dataView.Count;
        Hashtable ht = new Hashtable();
        for(int i = 0; i < rowCount; i++) {
            //create the unique key which is the combination of Carton Number and Label Sequence number
            string currentValue = dataView[i]["CTN"].ToString().Trim() + dataView[i]["BL"].ToString().Trim();

            //Check if the key already exists in the hashtable before adding to the table
            if(!ht.ContainsKey(currentValue)) {
                ht.Add(currentValue,null);
                dataTable.ImportRow(dataView[i].Row);
            }
        }
        return dataTable;
    }
    public DataSet fillDataset(string sp,string table,object[] o) {
        //
        Database db = DatabaseFactory.CreateDatabase();
        DbCommand cmd = db.GetStoredProcCommand(sp,o);
        cmd.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CommandTimeout"]);
        DataSet ds = new DataSet();
        db.LoadDataSet(cmd,ds,table);
        return ds;
    }
}
