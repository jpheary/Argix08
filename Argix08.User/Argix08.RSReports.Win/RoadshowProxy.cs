using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;

namespace Argix.Terminals {
	//
	public class RoadshowProxy {
		//Members
        private static RoadshowServiceClient _Client = null;
        private static bool _state=false;
        private static string _address="";
        
		//Interface
        static RoadshowProxy() { 
            //
            _Client = new RoadshowServiceClient();
            _state = true;
            _address = _Client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private RoadshowProxy() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config=null;
            try {
                _Client = new RoadshowServiceClient();
                System.ComponentModel.BindingList<string> names = new System.ComponentModel.BindingList<string>(usernames);
                config = _Client.GetUserConfiguration(application,names);
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
                _Client = new RoadshowServiceClient();
                _Client.WriteLogEntry(m);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("WriteLogEntry() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("WriteLogEntry() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("WriteLogEntry() communication error.",ce); }
        }
        public static TerminalInfo GetTerminalInfo() {
            //Get the operating enterprise terminal
            TerminalInfo terminal=null;
            try {
                _Client = new RoadshowServiceClient();
                terminal = _Client.GetTerminalInfo();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetTerminalInfo() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetTerminalInfo() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetTerminalInfo() communication error.",ce); }
            return terminal;
        }

        public static Depots GetDepots(string terminalCode) {
            //
            Depots depots = null;
            try {
                _Client = new RoadshowServiceClient();
                depots = _Client.GetDepots(terminalCode);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetDepots() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetDepots() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetDepots() communication error.",ce); }
            return depots;
        }

        public static void LoadPickups(DateTime pickupDate,string routeClass) {
            //Load pickup data
            try {
                _Client = new RoadshowServiceClient();
                _Client.LoadPickups(pickupDate,routeClass);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("LoadPickups() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("LoadPickups() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("LoadPickups() communication error.",ce); }
        }
        public static bool CanLoadPickups(DateTime pickupDate,string routeClass) {
            bool ret = false;
            try {
                _Client = new RoadshowServiceClient();
                ret = _Client.CanLoadPickups(pickupDate,routeClass);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("CanLoadPickups() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("CanLoadPickups() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("CanLoadPickups() communication error.",ce); }
            return ret;
        }
        public static Pickups GetPickups(DateTime pickupDate,string routeClass) {
            //
            Pickups pickups = null;
            try {
                _Client = new RoadshowServiceClient();
                pickups = _Client.GetPickups(pickupDate,routeClass);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetPickups() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetPickups() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetPickups() communication error.",ce); }
            return pickups;
        }
        public static bool AddPickup(Pickup pickup) {
            //
            bool ret = false;
            try {
                _Client = new RoadshowServiceClient();
                ret = _Client.AddPickup(pickup);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("AddPickup() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("AddPickup() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("AddPickup() communication error.",ce); }
            return ret;
        }
        public static bool UpdatePickups(Pickups pickups) {
            //
            bool ret = false;
            try {
                _Client = new RoadshowServiceClient();
                ret = _Client.UpdatePickups(pickups);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("UpdatePickups() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("UpdatePickups() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("UpdatePickups() communication error.",ce); }
            return ret;
        }
        public static bool UpdatePickup(Pickup pickup) {
            //
            bool ret = false;
            try {
                _Client = new RoadshowServiceClient();
                ret = _Client.UpdatePickup(pickup);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("UpdatePickup() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("UpdatePickup() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("UpdatePickup() communication error.",ce); }
            return ret;
        }

        public static void LoadScanAudit(DateTime routeDate,string routeClass) {
            //Load scan audit data
            try {
                _Client = new RoadshowServiceClient();
                _Client.LoadScanAudit(routeDate,routeClass);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("LoadScanAudit() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("LoadScanAudit() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("LoadScanAudit() communication error.",ce); }
        }
        public static bool CanLoadScanAudit(DateTime routeDate,string routeClass) {
            bool ret = false;
            try {
                _Client = new RoadshowServiceClient();
                ret = _Client.CanLoadScanAudit(routeDate,routeClass);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("CanLoadScanAudit() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("CanLoadScanAudit() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("CanLoadScanAudit() communication error.",ce); }
            return ret;
        }
        public static ScanAudits GetScanAudits(DateTime routeDate,string routeClass) {
            //
            ScanAudits scans = null;
            try {
                _Client = new RoadshowServiceClient();
                scans = _Client.GetScanAudits(routeDate,routeClass);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetScanAudits() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetScanAudits() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetScanAudits() communication error.",ce); }
            return scans;
        }
        public static ScanAudits GetScanAudits(DateTime routeDate,string routeClass,string driverName) {
            //
            ScanAudits scans = null;
            try {
                _Client = new RoadshowServiceClient();
                scans = _Client.GetScanAuditsForDriver(routeDate,routeClass,driverName);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetScanAudits() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetScanAudits() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetScanAudits() communication error.",ce); }
            return scans;
        }
        public static Drivers GetScanAuditDrivers(DateTime routeDate,string routeClass) {
            //
            Drivers drivers = null;
            try {
                _Client = new RoadshowServiceClient();
                drivers = _Client.GetScanAuditDrivers(routeDate,routeClass);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetScanAuditDrivers() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetScanAuditDrivers() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetScanAuditDrivers() communication error.",ce); }
            return drivers;
        }
        public static bool UpdateScanAudits(ScanAudits scans) {
            //
            bool ret = false;
            try {
                _Client = new RoadshowServiceClient();
                ret = _Client.UpdateScanAudits(scans);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("UpdateScanAudits() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("UpdateScanAudits() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("UpdateScanAudits() communication error.",ce); }
            return ret;
        }
        public static bool UpdateScanAudit(ScanAudit scan) {
            //
            bool ret = false;
            try {
                _Client = new RoadshowServiceClient();
                ret = _Client.UpdateScanAudit(scan);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("UpdateScanAudit() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("UpdateScanAudit() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("UpdateScanAudit() communication error.",ce); }
            return ret;
        }

        public static Drivers GetDrivers(string routeClass) {
            //
            Drivers drivers = null;
            try {
                _Client = new RoadshowServiceClient();
                drivers = _Client.GetDrivers(routeClass);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetDrivers() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetDrivers() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetDrivers() communication error.",ce); }
            return drivers;
        }
        public static Customers GetCustomers() {
            //
            Customers customers = null;
            try {
                _Client = new RoadshowServiceClient();
                customers = _Client.GetCustomers();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetCustomers() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetCustomers() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetCustomers() communication error.",ce); }
            return customers;
        }
        public static OrderTypes GetOrderTypes() {
            //
            OrderTypes types = null;
            try {
                _Client = new RoadshowServiceClient();
                types = _Client.GetOrderTypes();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetOrderTypes() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetOrderTypes() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetOrderTypes() communication error.",ce); }
            return types;
        }
        public static CommodityClasses GetCommodityClasses() {
            //
            CommodityClasses classes = null;
            try {
                _Client = new RoadshowServiceClient();
                classes = _Client.GetCommodityClasses();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetCommodityClasses() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetCommodityClasses() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetCommodityClasses() communication error.",ce); }
            return classes;
        }
        public static UpdateUsers GetUpdateUsers(string routeClass) {
            //
            UpdateUsers users = null;
            try {
                _Client = new RoadshowServiceClient();
                users = _Client.GetUpdateUsers(routeClass);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetUpdateUsers() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetUpdateUsers() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetUpdateUsers() communication error.",ce); }
            return users;
        }
        public static OnTimeIssues GetOnTimeIssues() {
            //
            OnTimeIssues issues = null;
            try {
                _Client = new RoadshowServiceClient();
                issues = _Client.GetOnTimeIssues();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetOnTimeIssues() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetOnTimeIssues() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetOnTimeIssues() communication error.",ce); }
            return issues;
        }
    }
}