using System;
using System.Collections;
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

namespace Argix.Terminals {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class RoadshowService:IRoadshowService {
        //Members
        private const string SQL_CONNID = "Roadshow";
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
        public RoadshowService() { }
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
            Argix.Enterprise.TerminalInfo ti = new Argix.Enterprise.TerminalInfo();
            ti.Description = "RGXVMRSSQL";
            return ti;
        }

        public Depots GetDepots(string terminalCode) {
            //Return a list of all Roadshow depots (terminalCode='') or just the depot for terminalCode
            Depots depots = null;
            try {
                depots = new Depots();
                DataSet ds = fillDataset(USP_DEPOTS,TBL_DEPOTS,new object[] { });
                if(ds != null) {
                    RoadshowDS _depots = new RoadshowDS();
                    _depots.DepotTable.Merge(ds.Tables[TBL_DEPOTS]);
                    for(int i = 0; i < _depots.DepotTable.Rows.Count; i++) {
                        string orderClass = _depots.DepotTable[i].RS_OrderClass;
                        if(terminalCode.Length == 0 || orderClass == terminalCode) 
                            depots.Add(new Depot(_depots.DepotTable[i]));
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading depots.",ex); }
            return depots;
        }

        public void LoadPickups(DateTime pickupDate,string routeClass) {
            //Load pickup data
            try {
                executeNonQuery(USP_PICKUPS_LOAD,new object[] { pickupDate,routeClass });
            }
            catch(Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(new ApplicationException("Unexpected error when loading pickups.",ex))); }
        }
        public bool CanLoadPickups(DateTime pickupDate,string routeClass) {
            bool ret = false;
            try {
                if(DateTime.Compare(pickupDate,DateTime.Today) <= 0) {
                    Pickups pickups = GetPickups(pickupDate,routeClass);
                    ret = pickups.Count == 0;
                }
            }
            catch(Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(new ApplicationException("Unexpected error when validating pickup loading.",ex))); }
            return ret;
        }
        public Pickups GetPickups(DateTime pickupDate,string routeClass) {
            //
            Pickups pickups = null;
            try {
                pickups = new Pickups();
                DataSet ds = fillDataset(USP_PICKUPS,TBL_PICKUPS,new object[] { pickupDate,routeClass });
                if(ds != null) {
                    RoadshowDS _pickups = new RoadshowDS();
                    _pickups.PickupTable.Merge(ds.Tables[TBL_PICKUPS]);
                    for(int i = 0; i < _pickups.PickupTable.Rows.Count; i++)
                        pickups.Add(new Pickup(_pickups.PickupTable[i]));
                }
            }
            catch(Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(new ApplicationException("Unexpected error while reading pickups.",ex))); }
            return pickups;
        }
        public bool AddPickup(Pickup pickup) {
            //
            bool ret = false;
            try {
                ret = executeNonQuery(USP_PICKUP_NEW,new object[] { 
                    pickup.Rt_Date, pickup.Driver, pickup.Rt_Name, pickup.RetnTime, 
                    pickup.Customer_ID, pickup.CustomerName, pickup.CustType, 
                    pickup.Address, pickup.City, pickup.State, pickup.Zip, 
                    pickup.OrderID, pickup.ActOrdSize, pickup.ActOrdLbs, 
                    pickup.Unsched_PU, pickup.Comments, pickup.OrdTyp, pickup.ActCmdty, pickup.Depot });
            }
            catch(Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(new ApplicationException("Unexpected error adding a pickup.",ex))); }
            return ret;
        }
        public bool UpdatePickups(Pickups pickups) {
            //
            bool ret = false;
            try {
                for(int i = 0; i < pickups.Count; i++) 
                    UpdatePickup(pickups[i]);
                ret = true;
            }
            catch(Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(new ApplicationException("Unexpected error updating pickups.",ex))); }
            return ret;
        }
        public bool UpdatePickup(Pickup pickup) {
            //
            bool ret = false;
            try {
                ret = executeNonQuery(USP_PICKUP_UPDATE,new object[] { 
                    pickup.RecordID, pickup.Driver, pickup.Rt_Name, 
                    pickup.Customer_ID, pickup.CustomerName, pickup.CustType, 
                    pickup.Address, pickup.City, pickup.State, pickup.Zip, 
                    pickup.ActOrdSize, pickup.ActOrdLbs, pickup.Unsched_PU, pickup.Comments, pickup.ActCmdty });
            }
            catch(Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(new ApplicationException("Unexpected error updating pickup.",ex))); }
            return ret;
        }

        public void LoadScanAudit(DateTime routeDate,string routeClass) {
            //Load scan audit data
            try {
                executeNonQuery(USP_SCANAUDIT_LOAD,new object[] { routeDate,routeClass });
            }
            catch(Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(new ApplicationException("Unexpected error when loading scan audit data.",ex))); }
        }
        public bool CanLoadScanAudit(DateTime routeDate,string routeClass) {
            //
            bool ret = false;
            try {
                if(DateTime.Compare(routeDate,DateTime.Today) < 0) {
                    ScanAudits scans = GetScanAudits(routeDate,routeClass);
                    ret = scans.Count == 0;
                }
            }
            catch(Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(new ApplicationException("Unexpected error when validating scan audit data loading.",ex))); }
            return ret;
        }
        public ScanAudits GetScanAudits(DateTime routeDate,string routeClass) {
            //
            ScanAudits scans = null;
            try {
                scans = new ScanAudits();
                DataSet ds =fillDataset(USP_SCANAUDIT,TBL_SCANAUDIT,new object[] { routeDate,routeClass });
                if(ds != null) {
                    RoadshowDS _scans = new RoadshowDS();
                    _scans.ScanAuditTable.Merge(ds.Tables[TBL_SCANAUDIT]);
                    for(int i = 0; i < _scans.ScanAuditTable.Rows.Count; i++)
                        scans.Add(new ScanAudit(_scans.ScanAuditTable[i]));
                }
            }
            catch(Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(new ApplicationException("Unexpected error while reading scan audits.",ex))); }
            return scans;
        }
        public ScanAudits GetScanAudits(DateTime routeDate,string routeClass,string driverName) {
            //
            ScanAudits scans = null;
            try {
                scans = new ScanAudits();
                ScanAudits _scans = GetScanAudits(routeDate,routeClass);
                if(driverName != "All") {
                    for(int i=0; i<_scans.Count; i++) {
                        if(_scans[i].Driver == driverName) scans.Add(_scans[i]);
                    }
                }
                else
                    scans = _scans;
            }
            catch(Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(new ApplicationException("Unexpected error while reading scan audits.",ex))); }
            return scans;
        }
        public Drivers GetScanAuditDrivers(DateTime routeDate,string routeClass) {
            //
            Drivers drivers = null;
            try {
                drivers = new Drivers();
                ScanAudits _scans = GetScanAudits(routeDate,routeClass);
                RoadshowDS _drivers = new RoadshowDS();
                for(int i = 0; i < _scans.Count; i++) {
                    string driver = _scans[i].Driver;
                    if(_drivers.DriverTable.Select("NAME='" + driver + "'").Length == 0)
                        _drivers.DriverTable.AddDriverTableRow(driver,"",0);
                }
                _drivers.AcceptChanges();
                for(int j = 0; j < _drivers.DriverTable.Count; j++) 
                    drivers.Add(new Driver(_drivers.DriverTable[j]));
            }
            catch(Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(new ApplicationException("Unexpected error while reading scan audit drivers.",ex))); }
            return drivers;
        }
        public bool UpdateScanAudits(ScanAudits scans) {
            //
            bool ret = false;
            try {
                for(int i = 0; i < scans.Count; i++) 
                    UpdateScanAudit(scans[i]);
                ret = true;
            }
            catch(Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(new ApplicationException("Unexpected error while updating scan audits.",ex))); }
            return ret;
        }
        public bool UpdateScanAudit(ScanAudit scan) {
            //
            bool ret = false;
            try {
                ret = executeNonQuery(USP_SCANAUDIT_UPDATE,new object[] { 
                    scan.RecordID, scan.Arrive, scan.Bell, scan.DelStart, scan.DelEnd, scan.Depart, scan.TimeEntryBy, scan.OnTimeIssue, scan.AdditComments 
                    });
            ret = true;
            }            
            catch(Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(new ApplicationException("Unexpected error while updating scan audits.",ex))); }

            return ret;
        }

        public Drivers GetDrivers(string routeClass) {
            //
            Drivers drivers = null;
            try {
                drivers = new Drivers();
                DataSet ds = fillDataset(USP_DRIVERS,TBL_DRIVERS,new object[] { routeClass });
                if(ds != null) {
                    RoadshowDS _drivers = new RoadshowDS();
                    _drivers.DriverTable.Merge(ds.Tables[TBL_DRIVERS]);
                    for(int i = 0; i < _drivers.DriverTable.Rows.Count; i++)
                        drivers.Add(new Driver(_drivers.DriverTable[i]));
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading drivers.",ex); }
            return drivers;
        }
        public Customers GetCustomers() {
            //
            Customers customers = null;
            try {
                customers = new Customers();
                DataSet ds = fillDataset(USP_CUSTOMERS,TBL_CUSTOMERS,new object[] { });
                if(ds != null) {
                    RoadshowDS _customers = new RoadshowDS();
                    _customers.CustomerTable.Merge(ds.Tables[TBL_CUSTOMERS]);
                    for(int i = 0; i < _customers.CustomerTable.Rows.Count; i++)
                        customers.Add(new Customer(_customers.CustomerTable[i]));
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading customers.",ex); }
            return customers;
        }
        public OrderTypes GetOrderTypes() {
            //
            OrderTypes types = null;
            try {
                types = new OrderTypes();
                DataSet ds = fillDataset(USP_ORDERTYPES,TBL_ORDERTYPES,new object[] { });
                if(ds != null) {
                    RoadshowDS _types = new RoadshowDS();
                    _types.OrderTypeTable.Merge(ds.Tables[TBL_ORDERTYPES]);
                    for(int i = 0; i < _types.OrderTypeTable.Rows.Count; i++)
                        types.Add(new OrderType(_types.OrderTypeTable[i]));
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading order types.",ex); }
            return types;
        }
        public CommodityClasses GetCommodityClasses() {
            //
            CommodityClasses classes = null;
            try {
                classes = new CommodityClasses();
                DataSet ds = fillDataset(USP_COMMODITYCLASSES,TBL_COMMODITYCLASSES,new object[] { });
                if(ds != null) {
                    RoadshowDS _classes = new RoadshowDS();
                    _classes.CommodityClassTable.Merge(ds.Tables[TBL_COMMODITYCLASSES]);
                    for(int i = 0; i < _classes.CommodityClassTable.Rows.Count; i++)
                        classes.Add(new CommodityClass(_classes.CommodityClassTable[i]));
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading commodity classes.",ex); }
            return classes;
        }
        public UpdateUsers GetUpdateUsers(string routeClass) {
            //
            UpdateUsers users = null;
            try {
                users = new UpdateUsers();
                DataSet ds = fillDataset(USP_UPDATEDBY,TBL_UPDATEDBY,new object[] { routeClass });
                if(ds != null) {
                    RoadshowDS _users = new RoadshowDS();
                    _users.UpdatedByTable.Merge(ds.Tables[TBL_UPDATEDBY]);
                    for(int i = 0; i < _users.UpdatedByTable.Rows.Count; i++)
                        users.Add(new UpdateUser(_users.UpdatedByTable[i]));
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading update users.",ex); }
            return users;
        }
        public OnTimeIssues GetOnTimeIssues() {
            //
            OnTimeIssues issues = null;
            try {
                issues = new OnTimeIssues();
                DataSet ds = fillDataset(USP_ONTIMEISSUES,TBL_ONTIMEISSUES,new object[] { });
                if(ds != null) {
                    RoadshowDS _issues = new RoadshowDS();
                    _issues.OnTimeIssueTable.Merge(ds.Tables[TBL_ONTIMEISSUES]);
                    for(int i=0; i<_issues.OnTimeIssueTable.Rows.Count; i++)
                        issues.Add(new OnTimeIssue(_issues.OnTimeIssueTable[i]));
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading on time issues.",ex); }
            return issues;
        }
        
        #region Data Services: fillDataset(), executeNonQuery(), executeNonQueryWithReturn(), executeScalar()
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
        private object executeScalar(string spName,object[] paramValues) {
            //
            Database db = DatabaseFactory.CreateDatabase(SQL_CONNID);
            return db.ExecuteScalar(spName,paramValues);
        }
        #endregion
    }
}
