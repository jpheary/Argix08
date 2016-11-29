using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using Argix.Data;

namespace Argix.Terminals {
    //
    public class TerminalsFactory {
        //Members
        private const string USP_DEPOTS = "uspDepotsGetList",TBL_DEPOTS = "DepotTable";
        private const string USP_PICKUPS_LOAD = "uspPickupsFromOrders2";
        private const string USP_PICKUPS = "uspPickupGetList",TBL_PICKUPS = "PickupTable";
        private const string USP_PICKUP_NEW = "uspPickupNew";
        private const string USP_PICKUP_UPDATE = "uspPickupUpdate2";
        private const string USP_SCANAUDIT_LOAD = "uspScanAuditFromOrders";
        private const string USP_SCANAUDIT = "uspScanAuditGetList",TBL_SCANAUDIT = "ScanAuditTable";
        private const string USP_SCANAUDIT_UPDATE = "uspScanAuditUpdate";
        private const string USP_DRIVERS = "uspDriverGetList",TBL_DRIVERS = "DriverTable";
        private const string USP_CUSTOMERS = "uspCustomerGetList",TBL_CUSTOMERS = "CustomerTable";
        private const string USP_ORDERTYPES = "uspOrderTypeGetList",TBL_ORDERTYPES = "OrderTypeTable";
        private const string USP_COMMODITYCLASSES = "uspCommodityClassGetList",TBL_COMMODITYCLASSES = "CommodityClassTable";
        private const string USP_UPDATEDBY = "uspUpdatedByGetList",TBL_UPDATEDBY = "UpdatedByTable";
        private const string USP_ONTIMEISSUES = "uspOnTimeIssueGetList",TBL_ONTIMEISSUES = "OnTimeIssueTable";

         
        //Interface
        static TerminalsFactory() { }
        private TerminalsFactory() { }
        public static DepotsDS GetDepots(string terminalCode) {
            //
            DepotsDS depots=null;
            try {
                depots = new DepotsDS();
                DataSet ds = App.Mediator.FillDataset(USP_DEPOTS,TBL_DEPOTS,new object[] { });
                if(ds!=null) {
                    depots.Merge(ds);
                    for(int i=0;i<depots.DepotTable.Rows.Count;i++) {
                        string orderClass = depots.DepotTable[i].RS_OrderClass;
                        if(!(terminalCode.Length == 0 || orderClass == terminalCode))
                            depots.DepotTable[i].Delete();
                    }
                    depots.DepotTable.AcceptChanges();
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading depots.",ex); }
            return depots;
        }

        public static void LoadPickups(DateTime pickupDate,string routeClass) {
            //Load pickup data
            App.Mediator.ExecuteNonQuery(USP_PICKUPS_LOAD,new object[] { pickupDate,routeClass });
        }
        public static bool CanLoadPickups(DateTime pickupDate,string routeClass) {
            bool ret = false;
            if(DateTime.Compare(pickupDate,DateTime.Today) <= 0) {
                PickupDS pickups = GetPickups(pickupDate,routeClass);
                ret = pickups.PickupTable.Rows.Count == 0;
            }
            return ret;
        }
        public static PickupDS GetPickups(DateTime pickupDate,string routeClass) {
            //
            PickupDS pickups=null;
            try {
                pickups = new PickupDS();
                DataSet ds = App.Mediator.FillDataset(USP_PICKUPS,TBL_PICKUPS,new object[] { pickupDate,routeClass });
                if(ds!=null) pickups.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading pickups.",ex); }
            return pickups;
        }
        public static bool AddPickup(PickupDS.PickupTableRow pickup) {
            //
            bool ret=false;
            ret = App.Mediator.ExecuteNonQuery(USP_PICKUP_NEW,new object[] { 
                pickup.Rt_Date, 
                pickup.Driver, 
                pickup.Rt_Name, 
                pickup.RetnTime, 
                pickup.Customer_ID, 
                pickup.CustomerName, 
                pickup.CustType, 
                pickup.Address, 
                pickup.City, 
                pickup.State, 
                pickup.Zip, 
                pickup.OrderID, 
                pickup.ActOrdSize, 
                pickup.ActOrdLbs, 
                pickup.Unsched_PU, 
                pickup.Comments, 
                pickup.OrdTyp, 
                pickup.ActCmdty, 
                pickup.Depot
            });
            return ret;
        }
        public static bool UpdatePickups(PickupDS pickups) {
            //
            bool ret = false;
            if(pickups.HasChanges()) {
                PickupDS _pickups = (PickupDS)pickups.GetChanges(DataRowState.Modified);
                for(int i = 0; i < _pickups.PickupTable.Rows.Count; i++) {
                    PickupDS.PickupTableRow pickup = _pickups.PickupTable[i];
                    bool changed = UpdatePickup(pickup);
                }
                pickups.AcceptChanges();
            }
            ret = true;
            return ret;
        }
        public static bool UpdatePickup(PickupDS.PickupTableRow pickup) {
            //
            bool ret=false;
            ret = App.Mediator.ExecuteNonQuery(USP_PICKUP_UPDATE,new object[] { 
                pickup.RecordID, 
                pickup.Driver, 
                pickup.Rt_Name, 
                pickup.Customer_ID, 
                pickup.CustomerName, 
                pickup.CustType, 
                pickup.Address, 
                pickup.City, 
                pickup.State, 
                pickup.Zip, 
                pickup.ActOrdSize, 
                pickup.ActOrdLbs, 
                pickup.Unsched_PU, 
                pickup.Comments, 
                pickup.ActCmdty 
            });
            return ret;
        }

        public static void LoadScanAudit(DateTime routeDate,string routeClass) {
            //Load scan audit data
            App.Mediator.ExecuteNonQuery(USP_SCANAUDIT_LOAD,new object[] { routeDate,routeClass });
        }
        public static bool CanLoadScanAudit(DateTime routeDate,string routeClass) {
            bool ret=false;
            if(DateTime.Compare(routeDate,DateTime.Today) < 0) {
                ScanAuditDS scans = GetScanAudit(routeDate,routeClass);
                ret = scans.ScanAuditTable.Rows.Count == 0;
            }
            return ret; 
        }
        public static ScanAuditDS GetScanAudit(DateTime routeDate,string routeClass) {
            //
            ScanAuditDS scans=null;
            try {
                scans = new ScanAuditDS();
                DataSet ds = App.Mediator.FillDataset(USP_SCANAUDIT,TBL_SCANAUDIT,new object[] { routeDate,routeClass });
                if(ds!=null) scans.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading scan audit.",ex); }
            return scans;
        }
        public static ScanAuditDS GetScanAudit(DateTime routeDate,string routeClass, string driverName) {
            //
            ScanAuditDS scans=null;
            try {
                scans = new ScanAuditDS();
                ScanAuditDS _scans = GetScanAudit(routeDate,routeClass);
                if(driverName != "All")
                    scans.Merge(_scans.ScanAuditTable.Select("Driver ='" + driverName + "'"));
                else
                    scans.Merge(_scans);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading scan audit.",ex); }
            return scans;
        }
        public static DriverDS GetScanAuditDrivers(DateTime routeDate,string routeClass) {
            //
            DriverDS drivers=null;
            try {
                drivers = new DriverDS();
                ScanAuditDS _scans = GetScanAudit(routeDate,routeClass);
                for(int i=0;i<_scans.ScanAuditTable.Rows.Count;i++) {
                    string driver = _scans.ScanAuditTable[i].Driver;
                    if(drivers.DriverTable.Select("NAME='" + driver + "'").Length == 0)
                        drivers.DriverTable.AddDriverTableRow(driver,"",0);
                }
                drivers.AcceptChanges();
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading drivers.",ex); }
            return drivers;
        }
        public static bool UpdateScanAudit(ScanAuditDS scans) {
            //
            bool ret=false;
            if(scans.HasChanges()) {
                ScanAuditDS _scans = (ScanAuditDS)scans.GetChanges(DataRowState.Modified);
                for(int i=0;i<_scans.ScanAuditTable.Rows.Count;i++) {
                    ScanAuditDS.ScanAuditTableRow scan = _scans.ScanAuditTable[i];
                    bool changed = UpdateScanAudit(scan);
                }
                scans.AcceptChanges();
            }
            ret = true;
            return ret;
        }
        public static bool UpdateScanAudit(ScanAuditDS.ScanAuditTableRow scan) {
            //
            bool ret=false;
            ret = App.Mediator.ExecuteNonQuery(USP_SCANAUDIT_UPDATE,new object[] { 
                scan.RecordID, 
                (!scan.IsArriveNull()?scan.Arrive:null), 
                (!scan.IsBellNull()?scan.Bell:null), 
                (!scan.IsDelStartNull()?scan.DelStart:null), 
                (!scan.IsDelEndNull()?scan.DelEnd:null), 
                (!scan.IsDepartNull()?scan.Depart:null), 
                (!scan.IsTimeEntryByNull()?scan.TimeEntryBy:null), 
                (!scan.IsOnTimeIssueNull()?scan.OnTimeIssue:null), 
                (!scan.IsAdditCommentsNull()?scan.AdditComments:null) 
            });
            ret = true;
            return ret;
        }

        public static DriverDS GetDrivers(string routeClass) {
            //
            DriverDS drivers=null;
            try {
                drivers = new DriverDS();
                DataSet ds = App.Mediator.FillDataset(USP_DRIVERS,TBL_DRIVERS,new object[] { routeClass });
                if(ds!=null) {
                    drivers.Merge(ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading drivers.",ex); }
            return drivers;
        }
        public static CustomerDS GetCustomers() {
            //
            CustomerDS customers=null;
            try {
                customers = new CustomerDS();
                DataSet ds = App.Mediator.FillDataset(USP_CUSTOMERS,TBL_CUSTOMERS,new object[] { });
                if(ds!=null) {
                    customers.Merge(ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading customers.",ex); }
            return customers;
        }
        public static OrderTypeDS GetOrderTypes() {
            //
            OrderTypeDS types=null;
            try {
                types = new OrderTypeDS();
                DataSet ds = App.Mediator.FillDataset(USP_ORDERTYPES,TBL_ORDERTYPES,new object[] { });
                if(ds!=null) {
                    types.Merge(ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading order types.",ex); }
            return types;
        }
        public static CommodityClassDS GetCommodityClasses() {
            //
            CommodityClassDS classes=null;
            try {
                classes = new CommodityClassDS();
                DataSet ds = App.Mediator.FillDataset(USP_COMMODITYCLASSES,TBL_COMMODITYCLASSES,new object[] { });
                if(ds!=null) {
                    classes.Merge(ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading commodity classes.",ex); }
            return classes;
        }
        public static DataSet GetUpdateUsers(string routeClass) {
            //
            DataSet users=null;
            try {
                users = new DataSet();
                DataSet ds = App.Mediator.FillDataset(USP_UPDATEDBY,TBL_UPDATEDBY,new object[] { routeClass });
                if(ds!=null) {
                    users.Merge(ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading update users.",ex); }
            return users;
        }
        public static DataSet GetOnTimeIssues() {
            //
            DataSet issues=null;
            try {
                issues = new DataSet();
                DataSet ds = App.Mediator.FillDataset(USP_ONTIMEISSUES,TBL_ONTIMEISSUES,new object[] { });
                if(ds!=null) {
                    issues.Merge(ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading on time issues.",ex); }
            return issues;
        }
    }
}
