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

namespace Argix.Terminals {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class MobileDevicesService:IMobileDevicesService {
        //Members
        private const string SQL_CONNID = "MobileDevices";
        private const string USP_LOCAL_TERMINALS = "uspInventoryTerminalGetList",TBL_LOCAL_TERMINALS = "TerminalTable";
        private const string USP_LOCAL_DRIVERS = "uspInventoryDriverGetListForEnterprise2",TBL_LOCAL_DRIVERS = "LocalDriverTable";
        
        private const string USP_DEVICE_VIEW = "uspInventoryMobileDeviceGetList2",TBL_DEVICE_VIEW = "DeviceItemTable";
        private const string USP_DEVICE_PRIORIDS = "uspInventoryMobileDevicePriorGetList",TBL_DEVICE_PRIORIDS = "DeviceItemPriorIDTable";
        private const string USP_DEVICE_CREATE = "uspInventoryMobileDeviceNew";
        private const string USP_DEVICE_UPDATE = "uspInventoryMobileDeviceUpdate";
        private const string USP_DEVICE_ASSIGN = "uspInventoryMobileDeviceAssign";
        private const string USP_DEVICE_UNASSIGN = "uspInventoryMobileDeviceUnAssign";
        
        private const string USP_BATTERY_VIEW = "uspInventoryBatteryGetList",TBL_BATTERY_VIEW = "BatteryItemTable";
        private const string USP_BATTERY_DRIVERS = "uspInventoryBatteryDriverGetList",TBL_BATTERY_DRIVERS = "LocalDriverTable";
        private const string USP_BATTERY_ASSIGNMENTS = "uspInventoryBatteryAssignmentsGetList",TBL_BATTERY_ASSIGNMENTS = "BatteryItemAssignmentTable";
        private const string USP_BATTERY_UNASSIGNED = "uspInventoryBatteryGetListUnAssigned",TBL_BATTERY_UNASSIGNED = "BatteryItemTable";
        private const string USP_BATTERY_CREATE = "uspInventoryBatteryNew";
        private const string USP_BATTERY_UPDATE = "uspInventoryBatteryUpdate";
        private const string USP_BATTERY_STARTCHARGE = "uspInventoryBatteryChargeCycleStart";
        private const string USP_BATTERY_ENDCHARGE = "uspInventoryBatteryChargeCycleEnd";
        private const string USP_BATTERY_ASSIGN = "uspInventoryBatteryAssign";
        private const string USP_BATTERY_UNASSIGN = "uspInventoryBatteryUnAssign",TBL_BATTERY_UNASSIGN = "BatteryItemTable";
        
        private const string USP_COMPONENTTYPE_VIEW = "uspInventoryCategoryTypeGetList",TBL_COMPONENTTYPE = "ComponentTypeTable";
        private const string USP_COMPONENTTYPE_CREATE = "uspInventoryCategoryTypeNew";
        private const string USP_COMPONENTTYPE_UPDATE = "uspInventoryCategoryTypeUpdate";


        //Interface
        public MobileDevicesService() { }
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
        public LocalTerminals GetLocalTerminals() {
            //Get a list of enterprise terminals
            LocalTerminals terminals=null;
            try {
                terminals = new LocalTerminals();
                DataSet ds = fillDataset(USP_LOCAL_TERMINALS,TBL_LOCAL_TERMINALS,new object[] { });
                if(ds!=null) {
                    for(int i=0;i<ds.Tables[TBL_LOCAL_TERMINALS].Rows.Count;i++) {
                        long id = long.Parse(ds.Tables[TBL_LOCAL_TERMINALS].Rows[i]["TerminalID"].ToString());
                        string name = ds.Tables[TBL_LOCAL_TERMINALS].Rows[i]["TerminalName"].ToString();
                        terminals.Add(new LocalTerminal(id,name));
                    }
                }
            }
            catch(Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(new ApplicationException("Unexpected error while reading local terminals.",ex))); }
            return terminals;
        }

        public LocalDrivers GetDrivers(long terminalID,bool mandatoryField) {
            //Return a list of enterprise drivers
            LocalDrivers drivers=null;
            try {
                drivers = new LocalDrivers();
                if(!mandatoryField) drivers.Add(new LocalDriver());
                DataSet ds = fillDataset(USP_LOCAL_DRIVERS,TBL_LOCAL_DRIVERS,new object[]{});
                if(ds!=null) {
                    LocalDriverDS driverDS = new LocalDriverDS();
                    driverDS.Merge(ds);
                    for(int i=0;i<driverDS.LocalDriverTable.Rows.Count;i++) {
                        LocalDriver driver = new LocalDriver(driverDS.LocalDriverTable[i]);
                        if(driver.IsActive == 1 && ((terminalID == 0) || (terminalID > 0 && driver.TerminalID == terminalID))) 
                            drivers.Add(driver);
                    }
                }
            }
            catch(Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(new ApplicationException("Unexpected error while reading enterprise drivers.",ex))); }
            return drivers;
        }
        public LocalDriver GetDriver(int driverID) {
            //Get an exisitng driver
            LocalDriver driver=null;
            try {
                LocalDrivers drivers = GetDrivers(0,true);
                for(int i=0;i<drivers.Count;i++) {
                    if(drivers[i].DriverID == driverID) { 
                        driver = drivers[i];
                        ItemDS itemDS = new ItemDS();
                        DataSet ds = fillDataset(USP_BATTERY_ASSIGNMENTS,TBL_BATTERY_ASSIGNMENTS,new object[] { });
                        if(ds!=null) {
                            itemDS.Merge(ds);
                            BatteryItemAssignments assignments = new BatteryItemAssignments();
                            ItemDS.BatteryItemAssignmentTableRow[] da = (ItemDS.BatteryItemAssignmentTableRow[])itemDS.BatteryItemAssignmentTable.Select("DriverID=" + driver.DriverID);
                            for(int j=0;j<da.Length;j++) {
                                BatteryItemAssignment assignment = new BatteryItemAssignment(da[j]);
                                assignments.Add(assignment);
                            }
                            driver.Assignments = assignments;
                        }
                        break; 
                    }
                }
                //DataContractSerializer dcs = new DataContractSerializer(typeof(LocalDriver));
                //System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //dcs.WriteObject(ms,driver);
                //ms.Seek(0,System.IO.SeekOrigin.Begin);
                //System.IO.StreamReader sr = new System.IO.StreamReader(ms);
                //string xml = sr.ReadToEnd();
            }
            catch(Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(new ApplicationException("Unexpected error while reading enterprise driver.",ex))); }
            return driver;
        }

        public DeviceItems GetDeviceItems() {
            //Update collection of device items
            DeviceItems items=null;
            try {
                items = new DeviceItems();
                DataSet ds = fillDataset(USP_DEVICE_VIEW,TBL_DEVICE_VIEW,new object[]{});
                if(ds!=null) {
                    ItemDS itemDS = new ItemDS();
                    itemDS.Merge(ds);

                    for(int i=0;i<itemDS.DeviceItemTable.Rows.Count;i++) {
                        DeviceItem item = new DeviceItem(itemDS.DeviceItemTable[i]);
                        item.DriverName = itemDS.DeviceItemTable[i].IsLastNameNull() ? "" : itemDS.DeviceItemTable[i].LastName.Trim();
                        item.DriverName += !itemDS.DeviceItemTable[i].IsLastNameNull() && !itemDS.DeviceItemTable[i].IsFirstNameNull() ? ", " : "";
                        item.DriverName += itemDS.DeviceItemTable[i].IsFirstNameNull() ? "" : itemDS.DeviceItemTable[i].FirstName.Trim();
                        items.Add(item);
                    }
                }
            }
            catch(Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(new ApplicationException("Unexpected error while reading device items.",ex))); }
            return items;
        }
        public DeviceItem GetDeviceItem(string itemID) {
            //Get a new or existing device item
            DeviceItem item=null;
            try {
                if(itemID.Length == 0)
                    item = new DeviceItem();
                else {
                    DeviceItems items = GetDeviceItems();
                    for(int i=0;i<items.Count;i++) {
                        if(items[i].ItemID == itemID) { item = items[i]; break; }
                    }
                }
            }
            catch(Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(new ApplicationException("Unexpected error while reading device item.",ex))); }
            return item;
        }
        public DeviceItems GetPriorDeviceItems() {
            //Get a list of all mobile device items available as prior devices
            DeviceItems items=null;
            try {
                items = new DeviceItems();
                DataSet ds = fillDataset(USP_DEVICE_PRIORIDS,TBL_DEVICE_PRIORIDS,new object[]{});
                if(ds!=null) {
                    ItemDS itemDS = new ItemDS();
                    itemDS.Merge(ds);
                    for(int i=0;i<itemDS.DeviceItemTable.Rows.Count;i++) {
                        DeviceItem item = new DeviceItem(itemDS.DeviceItemTable[i]);
                        if(!itemDS.DeviceItemTable[i].IsDriverIDNull()) item.Driver = GetDriver(itemDS.DeviceItemTable[i].DriverID);
                        items.Add(item);
                    }
                }
            }
            catch(Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(new ApplicationException("Unexpected error while reading prior device items.",ex))); }
            return items;
        }
        public DataSet GetInstallationTypes() {
            //Return list of installation types
            DataSet ds=null;
            try {
                ds = new DataSet();
                ds.Tables.Add("SelectionListTable");
                ds.Tables["SelectionListTable"].Columns.Add("ID");
                ds.Tables["SelectionListTable"].Columns.Add("Description");
                ds.Tables["SelectionListTable"].Rows.Add(new object[] { "Hard","Hard"});
                ds.Tables["SelectionListTable"].Rows.Add(new object[] { "Portable","Portable"});
            }
            catch(Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(new ApplicationException("Failed to get device item installation types.",ex))); }
            return ds;
        }
        public bool SaveDeviceItem(DeviceItem item) {
            //Create/update a mobile device
            bool result=false;
            try {
                if(item.ItemID.Length == 0) {
                    executeNonQueryWithReturn(USP_DEVICE_CREATE,
                        new object[]{	"", 
										item.TypeID, 
										item.TerminalID, 
										item.IsActive, 
										item.Comments, 
										item.LastUpdated, 
										item.UserID, 
										item.RowVersion, 
										null, 
										item.DeviceID, 
										item.ModelNumber, 
										item.FirmWareVersion, 
										item.SoftWareVersion, 
										item.ServiceExpiration.ToString(), 
										item.AccountID, null });
                    result = true;
                }
                else {
                    result = executeNonQuery(USP_DEVICE_UPDATE,
                        new object[]{	item.ItemID, 
										item.TerminalID, 
										item.IsActive, 
										item.Comments, 
										item.LastUpdated, 
										item.UserID, 
										item.RowVersion, 
										item.PriorItemID, 
										item.DeviceID, 
										item.ModelNumber, 
										item.FirmWareVersion, 
										item.SoftWareVersion, 
										item.ServiceExpiration.ToString(), 
										item.AccountID, 
										item.PriorAccountID });
                }
            }
            catch(Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(new ApplicationException("Unexpected error while saving device item.",ex))); }
            return result;
        }
        public bool AssignDeviceItem(DeviceItem item,int driverID,string installationType,string installationNumber) {
            //Assign this device item to the specified driver
            bool result=false;
            try {
                result = executeNonQuery(USP_DEVICE_ASSIGN, new object[] { item.ItemID, driverID, installationType, installationNumber, item.LastUpdated, item.UserID, item.RowVersion });
            }
            catch(Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(new ApplicationException("Unexpected error while assigning device to driver.",ex))); }
            return result;
        }
        public bool UnassignDeviceItem(DeviceItem item) {
            //Unassign this device item from its driver
            bool result=false;
            try {
                result = executeNonQuery(USP_DEVICE_UNASSIGN, new object[] { item.ItemID, item.LastUpdated, item.UserID, item.RowVersion });
            }
            catch(Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(new ApplicationException("Unexpected error while unassigning device from driver.",ex))); }
            return result;
        }

        public BatteryItems GetBatteryItems() {
            //Update collection of battery items
            BatteryItems items=null;
            try {
                items = new BatteryItems(); ;
                DataSet ds = fillDataset(USP_BATTERY_VIEW,TBL_BATTERY_VIEW,new object[]{});
                if(ds!=null) {
                    ItemDS itemDS = new ItemDS();
                    itemDS.Merge(ds);
                    for(int i=0;i<itemDS.BatteryItemTable.Rows.Count;i++) {
                        BatteryItem item = new BatteryItem(itemDS.BatteryItemTable[i]);
                        item.DriverName = itemDS.BatteryItemTable[i].IsLastNameNull() ? "" : itemDS.BatteryItemTable[i].LastName.Trim();
                        item.DriverName += !itemDS.BatteryItemTable[i].IsLastNameNull() && !itemDS.BatteryItemTable[i].IsFirstNameNull() ? ", " : "";
                        item.DriverName += itemDS.BatteryItemTable[i].IsFirstNameNull() ? "" : itemDS.BatteryItemTable[i].FirstName.Trim();
                        items.Add(item);
                    }
                }
            }
            catch(Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(new ApplicationException("Unexpected error while reading battery items.",ex))); }
            return items;
        }
        public BatteryItem GetBatteryItem(string itemID) {
            //Get a new or existing battery item
            BatteryItem item=null;
            try {
                if(itemID.Length == 0)
                    item = new BatteryItem();
                else {
                    BatteryItems items = GetBatteryItems();
                    for(int i=0;i<items.Count;i++) {
                        if(items[i].ItemID == itemID) { item = items[i]; break; }
                    }
                }
            }
            catch(Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(new ApplicationException("Unexpected error while reading battery item.",ex))); }
            return item;
        }
        public LocalDrivers GetBatteryItemAssignments() {
            //Get a collection of battery item assignments
            LocalDrivers drivers=null;
            try {
                drivers = new LocalDrivers();
                DataSet ds1 = fillDataset(USP_BATTERY_DRIVERS,TBL_BATTERY_DRIVERS,new object[] { });
                if(ds1!=null) {
                    ItemDS itemDS = new ItemDS();
                    DataSet ds2 = fillDataset(USP_BATTERY_ASSIGNMENTS,TBL_BATTERY_ASSIGNMENTS,new object[] { });
                    if(ds2!=null) itemDS.Merge(ds2);

                    LocalDriverDS driverDS = new LocalDriverDS();
                    driverDS.Merge(ds1);
                    for(int i=0;i<driverDS.LocalDriverTable.Rows.Count;i++) {
                        LocalDriver driver = new LocalDriver(driverDS.LocalDriverTable[i]);
                        BatteryItemAssignments assignments = new BatteryItemAssignments();
                        ItemDS.BatteryItemAssignmentTableRow[] da = (ItemDS.BatteryItemAssignmentTableRow[])itemDS.BatteryItemAssignmentTable.Select("DriverID=" + driver.DriverID);
                        for(int j=0;j<da.Length;j++) {
                            BatteryItemAssignment assignment = new BatteryItemAssignment(da[j]);
                            assignments.Add(assignment);
                        }
                        driver.Assignments = assignments;
                        drivers.Add(driver);
                    }
                }
            }
            catch(Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(new ApplicationException("Unexpected error while reading battery item assignments.",ex))); }
            return drivers;
        }
        public BatteryItems GetUnassignedBatteryItems(long terminalID) {
            //Get unassigned batteries for the specified terminal
            BatteryItems items=null;
            try {
                items = new BatteryItems();
                DataSet ds = fillDataset(USP_BATTERY_UNASSIGNED,TBL_BATTERY_UNASSIGNED,new object[]{});
                if(ds!=null) {
                    ItemDS itemDS = new ItemDS();
                    itemDS.Merge(ds);
                    for(int i=0;i<itemDS.BatteryItemTable.Rows.Count;i++) {
                        BatteryItem item = new BatteryItem(itemDS.BatteryItemTable[i]);
                        if(item.TerminalID == terminalID) {
                            items.Add(item);
                        }
                    }
                }
            }
            catch(Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(new ApplicationException("Unexpected error while reading unassigned battery items.",ex))); }
            return items;
        }
        public bool SaveBatteryItem(BatteryItem item) {
            //Persist this object
            bool result=false;
            try {
                if(item.ItemID.Length == 0) {
                    item.ItemID = (string)executeNonQueryWithReturn(USP_BATTERY_CREATE,
                        new object[]{	item.ItemID, 
										item.TypeID, 
										item.TerminalID, 
										item.IsActive, 
										item.Comments, 
										item.LastUpdated, 
										item.UserID, 
										item.RowVersion,
										item.MinHoursToCharge, 
										null, null });
                    result = true;
                }
                else {
                    result = executeNonQuery(USP_BATTERY_UPDATE,
                        new object[]{	item.ItemID,
										item.TerminalID, 
										item.IsActive, 
										item.Comments, 
										item.LastUpdated, 
										item.UserID, 
										item.RowVersion, 
										item.MinHoursToCharge });
                }
            }
            catch(Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(new ApplicationException("Unexpected error while saving battery item.",ex))); }
            return result;
        }
        public bool StartBatteryItemChargeCycle(BatteryItem item) {
            //Start the charging cycle for this battery item
            bool result=false;
            try {
                result = executeNonQuery(USP_BATTERY_STARTCHARGE,new object[] { item.ItemID,item.LastUpdated,item.UserID,item.RowVersion });
            }
            catch(Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(new ApplicationException("Unexpected error while starting battery item charge cycle.",ex))); }
            return result;
        }
        public bool EndBatteryItemChargeCycle(BatteryItem item) {
            //End the charging cycle for this battery item
            bool result=false;
            try {
                result = executeNonQuery(USP_BATTERY_ENDCHARGE,new object[] { item.ItemID,item.LastUpdated,item.UserID,item.RowVersion });
            }
            catch(Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(new ApplicationException("Unexpected error while ending battery item charge cycle.",ex))); }
            return result;
        }
        public bool AssignBatteryItem(BatteryItemAssignment assignment) {
            //
            bool result=false;
            try {
                result = executeNonQuery(USP_BATTERY_ASSIGN,new object[] { assignment.ItemID,assignment.DriverID,assignment.AssignedDate,assignment.AssignedUser,assignment.RowVersion });
            }
            catch(Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(new ApplicationException("Unexpected error while unassigning battery item.",ex))); }
            return result;
        }
        public bool UnassignBatteryItem(BatteryItemAssignment assignment) {
            //
            bool result=false;
            try {
                result = executeNonQuery(USP_BATTERY_UNASSIGN,new object[] { assignment.ItemID,assignment.AssignedDate,assignment.AssignedUser,assignment.RowVersion });
            }
            catch(Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(new ApplicationException("Unexpected error while unassigning battery item.",ex))); }
            return result;
        }

        public ComponentTypes GetComponentTypes() {
            //Get a collection of component types
            ComponentTypes types=null;
            try {
                types = new ComponentTypes();
                DataSet ds = fillDataset(USP_COMPONENTTYPE_VIEW,TBL_COMPONENTTYPE,new object[]{});
                if(ds!=null) {
                    ComponentTypeDS typeDS = new ComponentTypeDS();
                    typeDS.Merge(ds);
                    for(int i=0;i<typeDS.ComponentTypeTable.Rows.Count;i++) {
                        types.Add(new ComponentType(typeDS.ComponentTypeTable[i]));
                    }
                }
            }
            catch(Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(new ApplicationException("Unexpected error while reading component types.",ex))); }
            return types;
        }
        public ComponentType GetComponentType(string typeID) {
            //Get a new or existing component type
            ComponentType type=null;
            try {
                if(typeID.Length == 0)
                    type = new ComponentType();
                else {
                    ComponentTypes types = GetComponentTypes();
                    for(int i=0;i<types.Count;i++) {
                        if(types[i].TypeID == typeID) { type = types[i]; break; }
                    }
                }
            }
            catch(Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(new ApplicationException("Unexpected error while reading component type.",ex))); }
            return type;
        }
        public ComponentTypes GetComponentTypeList(bool mandatoryField,string categoryID) {
            //Get a list of component types
            ComponentTypes components=null;
            try {
                components = new ComponentTypes();
                if(!mandatoryField) components.Add(new ComponentType("",ComponentType.CATEGORYID_DEVICE,"",0,DateTime.Now,"",null));
                ComponentTypes types = GetComponentTypes();
                for(int i=0;i<types.Count;i++) {
                    if(types[i].CategoryID == categoryID) components.Add(types[i]);
                }
            }
            catch(Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(new ApplicationException("Unexpected error while reading component types list.",ex))); }
            return components;
        }
        public DataSet GetComponentTypeCategories() {
            //Return list of component type categories
            DataSet ds=null;
            try {
                ds = new DataSet();
                ds.Tables.Add("SelectionListTable");
                ds.Tables["SelectionListTable"].Columns.Add("ID");
                ds.Tables["SelectionListTable"].Columns.Add("Description");
                ds.Tables["SelectionListTable"].Rows.Add(new object[]{ComponentType.CATEGORYID_BATTERY,ComponentType.CATEGORYID_BATTERY});
                ds.Tables["SelectionListTable"].Rows.Add(new object[] { ComponentType.CATEGORYID_DEVICE,ComponentType.CATEGORYID_DEVICE });
            }
            catch(Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(new ApplicationException("Unexpected error while reading component types categories.",ex))); }
            return ds;
        }
        public bool SaveComponentType(ComponentType ctype) {
            //Persist this object
            bool result=false;
            try {
                if(ctype.TypeID.Trim().Length == 0) {
                    executeNonQueryWithReturn(USP_COMPONENTTYPE_CREATE,new object[] { ctype.TypeID,ctype.CategoryID,ctype.Description,ctype.IsActive,ctype.LastUpdated,ctype.UserID });
                    result = true;
                }
                else
                    result = executeNonQuery(USP_COMPONENTTYPE_UPDATE,new object[] { ctype.TypeID,ctype.Description,ctype.IsActive,ctype.LastUpdated,ctype.UserID,ctype.RowVersion });
            }
            catch(Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(new ApplicationException("Unexpected error while saving component type.",ex))); }
            return result;
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
}
