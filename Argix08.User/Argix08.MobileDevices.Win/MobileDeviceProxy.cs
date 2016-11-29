using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;
//using Argix.Windows;

namespace Argix.Terminals {
	//
	public class MobileDevicesProxy {
		//Members
        private static MobileDevicesServiceClient _Client=null;
        private static bool _state=false;
        private static string _address="";

        public const string ACTIVE = "Active", INACTIVE = "InActive", AVAILABLE = "Available", ISSUED = "Issued";
        public const string MOBILE_GATEWAY_DEVICE = "Gateway";
        public const string DISCHARGED = "Discharged",CHARGING = "Charging", CHARGED = "Charged",LOWCHARGE = "Low Charge";
        public const string CATEGORYID_DEVICE = "MobilDevice", CATEGORYID_BATTERY = "Battery";


		//Interface
        static MobileDevicesProxy() { 
            //
            _Client = new MobileDevicesServiceClient();
            _state = true;
            _address = _Client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private MobileDevicesProxy() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config=null;
            try {
                _Client = new MobileDevicesServiceClient();
                config = _Client.GetUserConfiguration(application,usernames);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetUserConfiguration() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetUserConfiguration() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetUserConfiguration() communication error.",ce); }
            return config;
        }
        public static void WriteLogEntry(TraceMessage m) {
            //Get the operating enterprise terminal
            try {
                _Client = new MobileDevicesServiceClient();
                _Client.WriteLogEntry(m);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("WriteLogEntry() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("WriteLogEntry() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("WriteLogEntry() communication error.",ce); }
        }
        public static Argix.Terminals.TerminalInfo GetTerminalInfo() {
            //Get the operating enterprise terminal
            Argix.Terminals.TerminalInfo terminal=null;
            try {
                _Client = new MobileDevicesServiceClient();
                terminal = _Client.GetTerminalInfo();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetTerminalInfo() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetTerminalInfo() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetTerminalInfo() communication error.",ce); }
            return terminal;
        }

        public static LocalTerminals GetLocalTerminals() {
            LocalTerminals terminal=null;
            try {
                _Client = new MobileDevicesServiceClient();
                terminal = _Client.GetLocalTerminals();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("() communication error.",ce); }
            return terminal;
        }
        public static LocalDrivers GetDrivers(long terminalID,bool mandatoryField) {
            LocalDrivers drivers=null;
            try {
                _Client = new MobileDevicesServiceClient();
                drivers = _Client.GetDrivers(terminalID,mandatoryField);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetEnterpriseDrivers() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetEnterpriseDrivers() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetEnterpriseDrivers() communication error.",ce); }
            return drivers;
        }
        public static LocalDriver GetDriver(int driverID) {
            LocalDriver driver=null;
            try {
                _Client = new MobileDevicesServiceClient();
                driver = _Client.GetDriver(driverID);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetEnterpriseDriver() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetEnterpriseDriver() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetEnterpriseDriver() communication error.",ce); }
            return driver;
        }

        public static DeviceItems GetDeviceItems() {
            DeviceItems items=null;
            try {
                _Client = new MobileDevicesServiceClient();
                items = _Client.GetDeviceItems();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetDeviceItems() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetDeviceItems() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetDeviceItems() communication error.",ce); }
            return items;
        }
        public static DeviceItem GetDeviceItem(string itemID) {
            DeviceItem item=null;
            try {
                _Client = new MobileDevicesServiceClient();
                item = _Client.GetDeviceItem(itemID);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetDeviceItem() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetDeviceItem() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetDeviceItem() communication error.",ce); }
            return item;
        }
        public static DeviceItems GetPriorDeviceItems() {
            DeviceItems items=null;
            try {
                _Client = new MobileDevicesServiceClient();
                items = _Client.GetPriorDeviceItems();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetPriorDeviceItems() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetPriorDeviceItems() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetPriorDeviceItems() communication error.",ce); }
            return items;
        }
        public static DataSet GetInstallationTypes() {
            DataSet types=null;
            try {
                _Client = new MobileDevicesServiceClient();
                types = _Client.GetInstallationTypes();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetInstallationTypes() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetInstallationTypes() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetInstallationTypes() communication error.",ce); }
            return types;
        }
        public static bool SaveDeviceItem(DeviceItem item) {
            bool ret=false;
            try {
                _Client = new MobileDevicesServiceClient();
                ret = _Client.SaveDeviceItem(item);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("Save() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("Save() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("Save() communication error.",ce); }
            return ret;
        }
        public static bool AssignDeviceItem(DeviceItem item,int driverID,string installationType,string installationNumber) {
            bool ret=false;
            try {
                _Client = new MobileDevicesServiceClient();
                ret = _Client.AssignDeviceItem(item,driverID,installationType,installationNumber);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("AssignToDriver() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("AssignToDriver() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("AssignToDriver() communication error.",ce); }
            return ret;
        }
        public static bool UnassignDeviceItem(DeviceItem item) {
            bool ret=false;
            try {
                _Client = new MobileDevicesServiceClient();
                ret = _Client.UnassignDeviceItem(item);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("UnassignFromDriver() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("UnassignFromDriver() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("UnassignFromDriver() communication error.",ce); }
            return ret;
        }

        public static BatteryItems GetBatteryItems() {
            BatteryItems items=null;
            try {
                _Client = new MobileDevicesServiceClient();
                items = _Client.GetBatteryItems();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetBatteryItems() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetBatteryItems() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetBatteryItems() communication error.",ce); }
            return items;
        }
        public static BatteryItem GetBatteryItem(string itemID) {
            BatteryItem item=null;
            try {
                _Client = new MobileDevicesServiceClient();
                item = _Client.GetBatteryItem(itemID);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetBatteryItem() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetBatteryItem() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetBatteryItem() communication error.",ce); }
            return item;
        }
        public static LocalDrivers GetBatteryItemAssignments() {
            LocalDrivers assignments=null;
            try {
                _Client = new MobileDevicesServiceClient();
                assignments = _Client.GetBatteryItemAssignments();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetBatteryAssignments() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetBatteryAssignments() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetBatteryAssignments() communication error.",ce); }
            return assignments;
        }
        public static BatteryItems GetUnassignedBatteryItems(long terminalID) {
            BatteryItems batteries=null;
            try {
                _Client = new MobileDevicesServiceClient();
                batteries = _Client.GetUnassignedBatteryItems(terminalID);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetUnassignedBatterys() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetUnassignedBatterys() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetUnassignedBatterys() communication error.",ce); }
            return batteries;
        }
        public static bool SaveBatteryItem(BatteryItem item) {
            bool ret=false;
            try {
                _Client = new MobileDevicesServiceClient();
                ret = _Client.SaveBatteryItem(item);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("Save() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("Save() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("Save() communication error.",ce); }
            return ret;
        }
        public static bool StartBatteryItemChargeCycle(BatteryItem item) {
            bool ret=false;
            try {
                _Client = new MobileDevicesServiceClient();
                ret = _Client.StartBatteryItemChargeCycle(item);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("StartChargeCycle() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("StartChargeCycle() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("StartChargeCycle() communication error.",ce); }
            return ret;
        }
        public static bool EndBatteryItemChargeCycle(BatteryItem item) {
            bool ret=false;
            try {
                _Client = new MobileDevicesServiceClient();
                ret = _Client.EndBatteryItemChargeCycle(item);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("EndChargeCycle() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("EndChargeCycle() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("EndChargeCycle() communication error.",ce); }
            return ret;
        }
        public static void ExportBatteryItem(BatteryItem item) {
            //Export this object to a text file
            const string TOKEN = ",";
            System.IO.StreamWriter writer=null;
            try {
                writer = System.IO.File.AppendText("c:\\batterys.txt");
                string sEntry = item.TerminalID + TOKEN + 
								item.Terminal.Trim() + TOKEN + 
								item.ItemID + TOKEN  + 
								item.TypeID + TOKEN + 
								item.InServiceDate.ToShortDateString() + TOKEN + 
								item.Comments + TOKEN + 
								item.IsActive + TOKEN + 
								item.LastUpdated + TOKEN + 
								item.UserID + TOKEN + 
								item.RowVersion;
                writer.WriteLine(sEntry);
                writer.Flush();
            }
            catch(Exception) { }
            finally { if(writer!=null) writer.Close(); }
        }
        public static bool AssignBatteryItem(BatteryItemAssignment assignment) {
            bool ret=false;
            try {
                _Client = new MobileDevicesServiceClient();
                assignment.AssignedDate = DateTime.Now;
                assignment.AssignedUser = Environment.UserName;
                ret = _Client.AssignBatteryItem(assignment);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("AssignBatteryItem() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("AssignBatteryItem() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("AssignBatteryItem() communication error.",ce); }
            return ret;
        }
        public static bool UnassignBatteryItem(BatteryItemAssignment assignment) {
            bool ret=false;
            try {
                _Client = new MobileDevicesServiceClient();
                assignment.AssignedDate = DateTime.Now;
                assignment.AssignedUser = Environment.UserName;
                ret = _Client.UnassignBatteryItem(assignment);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("UnassignBatteryItem() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("UnassignBatteryItem() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("UnassignBatteryItem() communication error.",ce); }
            return ret;
        }

        public static ComponentTypes GetComponentTypes() {
            ComponentTypes types=null;
            try {
                _Client = new MobileDevicesServiceClient();
                types = _Client.GetComponentTypes();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetComponentTypes() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetComponentTypes() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetComponentTypes() communication error.",ce); }
            return types;
        }
        public static ComponentType GetComponentType(string typeID) {
            ComponentType type=null;
            try {
                _Client = new MobileDevicesServiceClient();
                type = _Client.GetComponentType(typeID);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetComponentType() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetComponentType() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetComponentType() communication error.",ce); }
            return type;
        }
        public static ComponentTypes GetComponentTypeList(bool mandatoryField,string categoryID) {
            ComponentTypes types=null;
            try {
                _Client = new MobileDevicesServiceClient();
                types = _Client.GetComponentTypeList(mandatoryField,categoryID);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetComponentTypeList() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetComponentTypeList() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetComponentTypeList() communication error.",ce); }
            return types;
        }
        public static DataSet GetComponentTypeCategories() {
            DataSet cats=null;
            try {
                _Client = new MobileDevicesServiceClient();
                cats = _Client.GetComponentTypeCategories();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetCategoryList() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetCategoryList() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetCategoryList() communication error.",ce); }
            return cats;
        }
        public static bool SaveComponentType(ComponentType ctype) {
            bool ret=false;
            try {
                _Client = new MobileDevicesServiceClient();
                ret = _Client.SaveComponentType(ctype);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("SaveComponentType() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("SaveComponentType() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("SaveComponentType() communication error.",ce); }
            return ret;
        }

    }
}