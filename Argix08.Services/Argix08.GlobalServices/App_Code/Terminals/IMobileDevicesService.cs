using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Terminals {
    //Shipping Interfaces
    [ServiceContract(Namespace="http://Argix.Terminals")]
    public interface IMobileDevicesService {
        //Interface
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action="http://Argix.ConfigurationFault")]
        UserConfiguration GetUserConfiguration(string application,string[] usernames);
        [OperationContract(IsOneWay=true)]
        void WriteLogEntry(TraceMessage m);

        [OperationContract]
        Argix.Enterprise.TerminalInfo GetTerminalInfo();
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action="http://Argix.Terminals.MobileDevicesFault")]
        LocalTerminals GetLocalTerminals();
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action="http://Argix.Terminals.MobileDevicesFault")]
        LocalDrivers GetDrivers(long terminalID,bool mandatoryField);
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action="http://Argix.Terminals.MobileDevicesFault")]
        LocalDriver GetDriver(int driverID);

        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action="http://Argix.Terminals.MobileDevicesFault")]
        DeviceItems GetDeviceItems();
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action="http://Argix.Terminals.MobileDevicesFault")]
        DeviceItem GetDeviceItem(string itemID);
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action="http://Argix.Terminals.MobileDevicesFault")]
        DeviceItems GetPriorDeviceItems();
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action="http://Argix.Terminals.MobileDevicesFault")]
        DataSet GetInstallationTypes();
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action="http://Argix.Terminals.MobileDevicesFault")]
        bool SaveDeviceItem(DeviceItem item);
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action="http://Argix.Terminals.MobileDevicesFault")]
        bool AssignDeviceItem(DeviceItem item,int driverID,string installationType,string installationNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action="http://Argix.Terminals.MobileDevicesFault")]
        bool UnassignDeviceItem(DeviceItem item);

        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action="http://Argix.Terminals.MobileDevicesFault")]
        BatteryItems GetBatteryItems();
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action="http://Argix.Terminals.MobileDevicesFault")]
        BatteryItem GetBatteryItem(string itemID);
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action="http://Argix.Terminals.MobileDevicesFault")]
        LocalDrivers GetBatteryItemAssignments();
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action="http://Argix.Terminals.MobileDevicesFault")]
        BatteryItems GetUnassignedBatteryItems(long terminalID);
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action="http://Argix.Terminals.MobileDevicesFault")]
        bool SaveBatteryItem(BatteryItem item);
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action="http://Argix.Terminals.MobileDevicesFault")]
        bool StartBatteryItemChargeCycle(BatteryItem item);
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action="http://Argix.Terminals.MobileDevicesFault")]
        bool EndBatteryItemChargeCycle(BatteryItem item);
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action="http://Argix.Terminals.MobileDevicesFault")]
        bool AssignBatteryItem(BatteryItemAssignment assignment);
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action="http://Argix.Terminals.MobileDevicesFault")]
        bool UnassignBatteryItem(BatteryItemAssignment assignment);

        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action="http://Argix.Terminals.MobileDevicesFault")]
        ComponentTypes GetComponentTypes();
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action="http://Argix.Terminals.MobileDevicesFault")]
        ComponentType GetComponentType(string typeID);
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action="http://Argix.Terminals.MobileDevicesFault")]
        ComponentTypes GetComponentTypeList(bool mandatoryField,string categoryID);
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action="http://Argix.Terminals.MobileDevicesFault")]
        DataSet GetComponentTypeCategories();
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action="http://Argix.Terminals.MobileDevicesFault")]
        bool SaveComponentType(ComponentType ctype);
    }

    [DataContract]
    public class MobileItem {
        //Members
        protected string _itemid="",_typeid="";
        protected long _terminalid=0;
        protected string _terminal="",_status=MobileItem.AVAILABLE,_comments="";
        protected DateTime _created=DateTime.Now;
        protected string _createduserid="";
        protected byte _isactive=(byte)1;
        protected DateTime _lastupdated=DateTime.Now;
        protected string _userid=Environment.UserName,_rowversion="";
        protected string _drivername="";
        protected LocalDriver mDriver=null;
        protected DataSet mStatusList=null;

        public const string ACTIVE = "Active";
        public const string INACTIVE = "InActive";
        public const string AVAILABLE = "Available";
        public const string ISSUED = "Issued";

        //Interface
        public MobileItem() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string ItemID { get { return this._itemid; } set { this._itemid = value; } }
        [DataMember]
        public string TypeID { get { return this._typeid; } set { this._typeid = value; } }
        [DataMember]
        public long TerminalID { get { return this._terminalid; } set { this._terminalid = value; } }
        [DataMember]
        public string Terminal { get { return this._terminal; } set { this._terminal = value; } }
        [DataMember]
        public virtual string Status { get { return this._status; } set { this._status = value; } }
        [DataMember]
        public string Comments { get { return this._comments; } set { this._comments = value; } }
        [DataMember]
        public DateTime Created { get { return this._created; } set { this._created = value; } }
        [DataMember]
        public string CreatedUserID { get { return this._createduserid; } set { this._createduserid = value; } }
        [DataMember]
        public byte IsActive { get { return this._isactive; } set { this._isactive = value; } }
        [DataMember]
        public DateTime LastUpdated { get { return this._lastupdated; } set { this._lastupdated = value; } }
        [DataMember]
        public string UserID { get { return this._userid; } set { this._userid = value; } }
        [DataMember]
        public string RowVersion { get { return this._rowversion; } set { this._rowversion = value; } }
        [DataMember]
        public string DriverName { get { return this._drivername; } set { this._drivername = value; } }
        [DataMember]
        public LocalDriver Driver { get { return this.mDriver; } set { this.mDriver = value; } }
        [DataMember]
        public DataSet StatusList { get { return this.mStatusList; } set { this.mStatusList = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class DeviceItems:BindingList<DeviceItem> {
        public DeviceItems() { }
    }

    [DataContract]
    public class DeviceItem:MobileItem {
        //Members
        private string _deviceid="",_modelnumber="",_description="";
        private string _prioritemid=null,_priordeviceid="";
        private string _installationType="",_installationNumber="";
        private string _firmwareversion="",_softwareversion="";
        private DateTime _serviceexpiration=DateTime.Today.AddYears(1);
        private string _accountid="",_prioraccountid="";

        public const string MOBILE_GATEWAY_DEVICE = "Gateway";


        //Interface
        public DeviceItem() : this(null) { }
        public DeviceItem(ItemDS.DeviceItemTableRow item) : base() {
            //Constructor
            try {
                if(item != null) {
                    if(!item.IsItemIDNull()) base._itemid = item.ItemID;
                    if(!item.IsTypeIDNull()) base._typeid = item.TypeID;
                    if(!item.IsTerminalIDNull()) base._terminalid = item.TerminalID;
                    if(!item.IsTerminalNull()) base._terminal = item.Terminal;
                    if(!item.IsStatusNull()) base._status = item.Status; else base._status = MobileItem.AVAILABLE;
                    if(!item.IsCommentsNull()) base._comments = item.Comments;
                    if(!item.IsCreatedNull()) base._created = item.Created;
                    if(!item.IsCreatedUserIDNull()) base._createduserid = item.CreatedUserID;
                    if(!item.IsIsActiveNull()) base._isactive = item.IsActive;
                    if(!item.IsLastUpdatedNull()) base._lastupdated = item.LastUpdated;
                    if(!item.IsUserIDNull()) base._userid = item.UserID;
                    if(!item.IsRowVersionNull()) base._rowversion = item.RowVersion;
                    if(!item.IsFullNameNull()) base._drivername = item.FullName;
                 
                    if(!item.IsDeviceIDNull()) this._deviceid = item.DeviceID;
                    if(!item.IsModelNumberNull()) this._modelnumber = item.ModelNumber;
                    if(!item.IsDescriptionNull()) this._description = item.Description;
                    if(!item.IsPriorItemIDNull()) this._prioritemid = item.PriorItemID;
                    if(!item.IsPriorDeviceIDNull()) this._priordeviceid = item.PriorDeviceID;
                    if(!item.IsInstallationTypeNull()) this._installationType = item.InstallationType;
                    if(!item.IsInstallationNumberNull()) this._installationNumber = item.InstallationNumber;
                    if(!item.IsFirmWareVersionNull()) this._firmwareversion = item.FirmWareVersion;
                    if(!item.IsSoftWareVersionNull()) this._softwareversion = item.SoftWareVersion;
                    if(!item.IsServiceExpirationNull()) this._serviceexpiration = item.ServiceExpiration;
                    if(!item.IsAccountIDNull()) this._accountid = item.AccountID;
                    if(!item.IsPriorAccountIDNull()) this._prioraccountid = item.PriorAccountID;
                }
                setStatusList();
            }
            catch(Exception ex) { throw new ApplicationException("Failed to create new device item",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string DeviceID { get { return this._deviceid; } set { this._deviceid = value; } }
        [DataMember]
        public string ModelNumber { get { return this._modelnumber; } set { this._modelnumber = value; } }
        [DataMember]
        public string Description { get { return this._description; } set { this._description = value; } }
        [DataMember]
        public string PriorItemID { get { return this._prioritemid; } set { this._prioritemid = value; } }
        [DataMember]
        public string PriorDeviceID { get { return this._priordeviceid; } set { this._priordeviceid = value; } }
        [DataMember]
        public string InstallationType { get { return this._installationType; } set { this._installationType = value; } }
        [DataMember]
        public string InstallationNumber { get { return this._installationNumber; } set { this._installationNumber = value; } }
        [DataMember]
        public string FirmWareVersion { get { return this._firmwareversion; } set { this._firmwareversion = value; } }
        [DataMember]
        public string SoftWareVersion { get { return this._softwareversion; } set { this._softwareversion = value; } }
        [DataMember]
        public DateTime ServiceExpiration { get { return this._serviceexpiration; } set { this._serviceexpiration = value; } }
        [DataMember]
        public string AccountID { get { return this._accountid; } set { this._accountid = value; } }
        [DataMember]
        public string PriorAccountID { get { return this._prioraccountid; } set { this._prioraccountid = value; } }
        #endregion
        private void setStatusList() {
            //Add selection for current state; then add selections for states this object can change to
            base.mStatusList = new DataSet();
            base.mStatusList.Tables.Add("SelectionListTable");
            base.mStatusList.Tables["SelectionListTable"].Columns.Add("ID");
            base.mStatusList.Tables["SelectionListTable"].Columns.Add("Description");
            if(base._itemid.Length > 0) {
                if(base._isactive == 1) {
                    if(base.mDriver != null)
                        base.mStatusList.Tables["SelectionListTable"].Rows.Add(new object[] { MobileItem.ISSUED,MobileItem.ISSUED });
                    else {
                        base.mStatusList.Tables["SelectionListTable"].Rows.Add(new object[] { MobileItem.AVAILABLE,MobileItem.AVAILABLE });
                        base.mStatusList.Tables["SelectionListTable"].Rows.Add(new object[] { MobileItem.INACTIVE,MobileItem.INACTIVE });
                    }
                }
                else {
                    base.mStatusList.Tables["SelectionListTable"].Rows.Add(new object[] { MobileItem.INACTIVE,MobileItem.INACTIVE });
                    base.mStatusList.Tables["SelectionListTable"].Rows.Add(new object[] { MobileItem.AVAILABLE,MobileItem.AVAILABLE });
                }
            }
            else {
                base.mStatusList.Tables["SelectionListTable"].Rows.Add(new object[] { MobileItem.ACTIVE,MobileItem.ACTIVE });
                base.mStatusList.Tables["SelectionListTable"].Rows.Add(new object[] { MobileItem.INACTIVE,MobileItem.INACTIVE });
            }
        }
    }

    [CollectionDataContract]
    public class BatteryItems:BindingList<BatteryItem> {
        public BatteryItems() { }
    }

    [DataContract]
    public class BatteryItem:MobileItem {
        //Members		
        private string _deviceid="",_description="";
        private DateTime _cyclestart=DateTime.MinValue,_cycleend=DateTime.MinValue;
        private string _elapsedtimecharging="",_remainingtimecharging="";
        private int _cyclecomplete=0;
        private string _timeelapsedsincecomplete="";
        private int _minhourstocharge=4,_numberofcycles=0;
        private DateTime _assigneddate=DateTime.MinValue;
        private DateTime _inservicedate=DateTime.Today;

        public const string DISCHARGED = "Discharged",CHARGING = "Charging";
        public const string CHARGED = "Charged",LOWCHARGE = "Low Charge";

        //Interface		
        public BatteryItem() : this(null) { }
        public BatteryItem(ItemDS.BatteryItemTableRow item) : base() {
            //Constructor
            try {
                if(item != null) {
                    if(!item.IsItemIDNull()) base._itemid = item.ItemID;
                    if(!item.IsTypeIDNull()) base._typeid = item.TypeID;
                    if(!item.IsTerminalIDNull()) base._terminalid = item.TerminalID;
                    if(!item.IsTerminalNull()) base._terminal = item.Terminal;
                    if(!item.IsStatusNull()) base._status = item.Status; else base._status = MobileItem.AVAILABLE;
                    if(!item.IsCommentsNull()) base._comments = item.Comments;
                    if(!item.IsCreatedNull()) base._created = item.Created;
                    if(!item.IsCreatedUserIDNull()) base._createduserid = item.CreatedUserID;
                    if(!item.IsIsActiveNull()) base._isactive = item.IsActive;
                    if(!item.IsLastUpdatedNull()) base._lastupdated = item.LastUpdated;
                    if(!item.IsUserIDNull()) base._userid = item.UserID;
                    if(!item.IsRowVersionNull()) base._rowversion = item.RowVersion;
                    if(!item.IsFullNameNull()) base._drivername = item.FullName;
                   
                    if(!item.IsDeviceIDNull()) this._deviceid = item.DeviceID;
                    if(!item.IsDescriptionNull()) this._description = item.Description;
                    if(!item.IsCycleStartNull()) this._cyclestart = item.CycleStart;
                    if(!item.IsElapsedTimeChargingNull()) this._elapsedtimecharging = item.ElapsedTimeCharging;
                    if(!item.IsRemainingTimeChargingNull()) this._remainingtimecharging = item.RemainingTimeCharging;
                    if(!item.IsCycleEndNull()) this._cycleend = item.CycleEnd;
                    if(!item.IsCycleCompleteNull()) this._cyclecomplete = item.CycleComplete;
                    if(!item.IsTimeElapsedSinceCompleteNull()) this._timeelapsedsincecomplete = item.TimeElapsedSinceComplete;
                    if(!item.IsMinHoursToChargeNull()) this._minhourstocharge = item.MinHoursToCharge;
                    if(!item.IsNumberOfCyclesNull()) this._numberofcycles = item.NumberOfCycles;
                    if(!item.IsAssignedDateNull()) this._assigneddate = item.AssignedDate;
                    if(!item.IsInServiceDateNull()) this._inservicedate = item.InServiceDate;
                }
                setStatusList();
            }
            catch(Exception ex) { throw new ApplicationException("Failed to create new battery item",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string DeviceID { get { return this._deviceid; } set { this._deviceid = value; } }
        [DataMember]
        public string Description { get { return this._description; } set { this._description = value; } }
        [DataMember]
        public DateTime CycleStart { get { return this._cyclestart; } set { this._cyclestart = value; } }
        [DataMember]
        public string ElapsedTimeCharging { get { return this._elapsedtimecharging; } set { this._elapsedtimecharging = value; } }
        [DataMember]
        public string RemainingTimeCharging { get { return this._remainingtimecharging; } set { this._remainingtimecharging = value; } }
        [DataMember]
        public DateTime CycleEnd { get { return this._cycleend; } set { this._cycleend = value; } }
        [DataMember]
        public int CycleComplete { get { return this._cyclecomplete; } set { this._cyclecomplete = value; } }
        [DataMember]
        public string TimeElapsedSinceComplete { get { return this._timeelapsedsincecomplete; } set { this._timeelapsedsincecomplete = value; } }
        [DataMember]
        public int MinHoursToCharge { get { return this._minhourstocharge; } set { this._minhourstocharge = value; } }
        [DataMember]
        public int NumberOfCycles { get { return this._numberofcycles; } set { this._numberofcycles = value; } }
        [DataMember]
        public DateTime AssignedDate { get { return this._assigneddate; } set { this._assigneddate = value; } }
        [DataMember]
        public DateTime InServiceDate { get { return this._inservicedate; } set { this._inservicedate = value; } }
        #endregion
        public void setStatusList() {
            //Add selection for current state; then add selections for states this object can change to
            base.mStatusList = new DataSet();
            base.mStatusList.Tables.Add("SelectionListTable");
            base.mStatusList.Tables["SelectionListTable"].Columns.Add("ID");
            base.mStatusList.Tables["SelectionListTable"].Columns.Add("Description");
            if(base._itemid.Length > 0) {
                if(base._isactive == 1) {
                    if(base.mDriver != null) 
                        base.mStatusList.Tables["SelectionListTable"].Rows.Add(new object[] { MobileItem.ISSUED,MobileItem.ISSUED });
                    else {
                        base.mStatusList.Tables["SelectionListTable"].Rows.Add(new object[] { MobileItem.AVAILABLE,MobileItem.AVAILABLE });
                        base.mStatusList.Tables["SelectionListTable"].Rows.Add(new object[] { MobileItem.INACTIVE,MobileItem.INACTIVE });
                    }
                }
                else {
                    base.mStatusList.Tables["SelectionListTable"].Rows.Add(new object[] { MobileItem.INACTIVE,MobileItem.INACTIVE });
                    base.mStatusList.Tables["SelectionListTable"].Rows.Add(new object[] { MobileItem.ACTIVE,MobileItem.ACTIVE });
                }
            }
            else {
                base.mStatusList.Tables["SelectionListTable"].Rows.Add(new object[] { MobileItem.ACTIVE,MobileItem.ACTIVE });
                base.mStatusList.Tables["SelectionListTable"].Rows.Add(new object[] { MobileItem.INACTIVE,MobileItem.INACTIVE });
            }
        }
    }

    [CollectionDataContract]
    public class BatteryItemAssignments:BindingList<BatteryItemAssignment> {
        public BatteryItemAssignments() { }
    }

    [DataContract]
    public class BatteryItemAssignment {
        //Members
        private int _driverid=0;
        private string _itemid="", _comments="";
        private DateTime _assigneddate;
        private string _assigneduser="", _rowversion="";

        //Interface
        public BatteryItemAssignment(ItemDS.BatteryItemAssignmentTableRow assignment) {
            try {
                if(assignment != null) {
                    if(!assignment.IsDriverIDNull()) this._driverid = assignment.DriverID;
                    if(!assignment.IsItemIDNull()) this._itemid = assignment.ItemID;
                    if(!assignment.IsCommentsNull()) this._comments = assignment.Comments;
                    if(!assignment.IsAssignedDateNull()) this._assigneddate = assignment.AssignedDate;
                    if(!assignment.IsAssignedUserNull()) this._assigneduser = assignment.AssignedUser;
                    if(!assignment.IsRowVersionNull()) this._rowversion = assignment.RowVersion;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Failed to create new battery item assignment.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int DriverID { get { return this._driverid; } set { this._driverid = value; } }
        [DataMember]
        public string ItemID { get { return this._itemid; } set { this._itemid = value; } }
        [DataMember]
        public string Comments { get { return this._comments; } set { this._comments = value; } }
        [DataMember]
        public DateTime AssignedDate { get { return this._assigneddate; } set { this._assigneddate = value; } }
        [DataMember]
        public string AssignedUser { get { return this._assigneduser; } set { this._assigneduser = value; } }
        [DataMember]
        public string RowVersion { get { return this._rowversion; } set { this._rowversion = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class ComponentTypes:BindingList<ComponentType> {
        public ComponentTypes() { }
    }

    [DataContract]
    public class ComponentType {
        //Members
        private string _typeid="",_categoryid=CATEGORYID_DEVICE,_description="";
        private byte _isactive=(byte)1;
        private DateTime _lastupdated=DateTime.Now;
        private string _userid=Environment.UserName,_rowversion="";

        public const string CATEGORYID_DEVICE = "MobilDevice";
        public const string CATEGORYID_BATTERY = "Battery";

        //Interface		
        public ComponentType() : this(null) { }
        public ComponentType(ComponentTypeDS.ComponentTypeTableRow type) {
            try {
                if(type != null) {
                    if(!type.IsTypeIDNull()) this._typeid = type.TypeID;
                    if(!type.IsCategoryIDNull()) this._categoryid = type.CategoryID;
                    if(!type.IsDescriptionNull()) this._description = type.Description;
                    if(!type.IsIsActiveNull()) this._isactive = type.IsActive;
                    if(!type.IsLastUpdatedNull()) this._lastupdated = type.LastUpdated;
                    if(!type.IsUserIDNull()) this._userid = type.UserID;
                    if(!type.IsRowVersionNull()) this._rowversion = type.RowVersion;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Failed to create new component type",ex); }
        }
        public ComponentType(string typeID, string categoryID, string description, byte isActive, DateTime lastUpdated, string userID, string rowVersion) {
            this._typeid = typeID;
            this._categoryid = categoryID;
            this._description = description;
            this._isactive = isActive;
            this._lastupdated = lastUpdated;
            this._userid = userID;
            this._rowversion = rowVersion;
        }

        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string TypeID { get { return this._typeid; } set { this._typeid = value; } }
        [DataMember]
        public string CategoryID { get { return this._categoryid; } set { this._categoryid = value; } }
        [DataMember]
        public string Description { get { return this._description; } set { this._description = value; } }
        [DataMember]
        public byte IsActive { get { return this._isactive; } set { this._isactive = value; } }
        [DataMember]
        public DateTime LastUpdated { get { return this._lastupdated; } set { this._lastupdated = value; } }
        [DataMember]
        public string UserID { get { return this._userid; } set { this._userid = value; } }
        [DataMember]
        public string RowVersion { get { return this._rowversion; } set { this._rowversion = value; } }
        #endregion
    }

    [DataContract]
    public class MobileDevicesFault {
        private Exception _ex=null;
        public MobileDevicesFault(Exception ex) { this._ex = ex; }
        [DataMember]
        public Exception Exception { get { return this._ex; } set { this._ex = value; } }
    }
}
