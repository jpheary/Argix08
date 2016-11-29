using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Terminals {
    //Interface
    [ServiceContract(Namespace="http://Argix.Terminals")]
    public interface IRoadshowService {
        //Interface
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action="http://Argix.ConfigurationFault")]
        UserConfiguration GetUserConfiguration(string application,string[] usernames);
        [OperationContract(IsOneWay=true)]
        void WriteLogEntry(TraceMessage m);

        [OperationContract]
        Argix.Enterprise.TerminalInfo GetTerminalInfo();

        [OperationContract]
        Depots GetDepots(string terminalCode);

        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action = "http://Argix.Terminals.RoadshowFault")]
        void LoadPickups(DateTime pickupDate,string routeClass);
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action = "http://Argix.Terminals.RoadshowFault")]
        bool CanLoadPickups(DateTime pickupDate,string routeClass);
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action = "http://Argix.Terminals.RoadshowFault")]
        Pickups GetPickups(DateTime pickupDate,string routeClass);
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action = "http://Argix.Terminals.RoadshowFault")]
        bool AddPickup(Pickup pickup);
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action = "http://Argix.Terminals.RoadshowFault")]
        bool UpdatePickups(Pickups pickups);
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action = "http://Argix.Terminals.RoadshowFault")]
        bool UpdatePickup(Pickup pickup);

        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action = "http://Argix.Terminals.RoadshowFault")]
        void LoadScanAudit(DateTime routeDate,string routeClass);
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action = "http://Argix.Terminals.RoadshowFault")]
        bool CanLoadScanAudit(DateTime routeDate,string routeClass);
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action = "http://Argix.Terminals.RoadshowFault")]
        ScanAudits GetScanAudits(DateTime routeDate,string routeClass);
        [OperationContract(Name = "GetScanAuditsForDriver")]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action = "http://Argix.Terminals.RoadshowFault")]
        ScanAudits GetScanAudits(DateTime routeDate,string routeClass,string driverName);
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action = "http://Argix.Terminals.RoadshowFault")]
        Drivers GetScanAuditDrivers(DateTime routeDate,string routeClass);
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action = "http://Argix.Terminals.RoadshowFault")]
        bool UpdateScanAudits(ScanAudits scans);
        [OperationContract]
        [FaultContractAttribute(typeof(MobileDevicesFault),Action = "http://Argix.Terminals.RoadshowFault")]
        bool UpdateScanAudit(ScanAudit scan);
        
        [OperationContract]
        Drivers GetDrivers(string routeClass);
        [OperationContract]
        Customers GetCustomers();
        [OperationContract]
        OrderTypes GetOrderTypes();
        [OperationContract]
        CommodityClasses GetCommodityClasses();
        [OperationContract]
        UpdateUsers GetUpdateUsers(string routeClass);
        [OperationContract]
        OnTimeIssues GetOnTimeIssues();
    }

    [CollectionDataContract]
    public class Depots:BindingList<Depot> {
        public Depots() { }
    }

    [DataContract]
    public class Depot {
        //Members
        private int _DepotLkupIndx=0;
        private string _Depotname="";
        private int _Stop_ID=0;
        private short _Depotnumber=0, _SortLoc=0;
        private string _RS_OrderClass="", _BWIScanRouteIDMoniker="", _DepotMoniker="";

        public Depot(): this(null) { }
        public Depot(RoadshowDS.DepotTableRow depot) { 
            //Constructor
            try {
                if(depot != null) {
                    if(!depot.IsDepotLkupIndxNull()) this._DepotLkupIndx = depot.DepotLkupIndx;
                    if(!depot.IsDepotnameNull()) this._Depotname = depot.Depotname;
                    if(!depot.IsStop_IDNull()) this._Stop_ID = depot.Stop_ID;
                    if(!depot.IsDepotnumberNull()) this._Depotnumber = depot.Depotnumber;
                    if(!depot.IsSortLocNull()) this._SortLoc = depot.SortLoc;
                    if(!depot.IsRS_OrderClassNull()) this._RS_OrderClass = depot.RS_OrderClass;
                    if(!depot.IsBWIScanRouteIDMonikerNull()) this._BWIScanRouteIDMoniker = depot.BWIScanRouteIDMoniker;
                    if(!depot.IsDepotMonikerNull()) this._DepotMoniker = depot.DepotMoniker;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Failed to create new depot.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember(EmitDefaultValue = false)]
        public int DepotLkupIndx { get { return this._DepotLkupIndx; } set { this._DepotLkupIndx = value; } }
        [DataMember]
        public string Depotname { get { return this._Depotname; } set { this._Depotname = value; } }
        [DataMember(EmitDefaultValue = false)]
        public int Stop_ID { get { return this._Stop_ID; } set { this._Stop_ID = value; } }
        [DataMember]
        public short Depotnumber { get { return this._Depotnumber; } set { this._Depotnumber = value; } }
        [DataMember(EmitDefaultValue = false)]
        public short SortLoc { get { return this._SortLoc; } set { this._SortLoc = value; } }
        [DataMember]
        public string RS_OrderClass { get { return this._RS_OrderClass; } set { this._RS_OrderClass = value; } }
        [DataMember(EmitDefaultValue = false)]
        public string BWIScanRouteIDMoniker { get { return this._BWIScanRouteIDMoniker; } set { this._BWIScanRouteIDMoniker = value; } }
        [DataMember(EmitDefaultValue = false)]
        public string DepotMoniker { get { return this._DepotMoniker; } set { this._DepotMoniker = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class CommodityClasses:BindingList<CommodityClass> {
        public CommodityClasses() { }
    }
    
    [DataContract]
    public class CommodityClass {
        //Members
        private string _Name="", _Description="";

        public CommodityClass(): this(null) { }
        public CommodityClass(RoadshowDS.CommodityClassTableRow comclass) { 
            //Constructor
            try {
                if(comclass != null) {
                    if(!comclass.IsCOMMODITY_CLASSNull()) this._Name = comclass.COMMODITY_CLASS;
                    if(!comclass.IsDESCRIPTIONNull()) this._Description = comclass.DESCRIPTION;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Failed to create new commodity class.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string Name { get { return this._Name; } set { this._Name = value; } }
        [DataMember]
        public string Description { get { return this._Description; } set { this._Description = value; } }
        #endregion
    }
    
    [CollectionDataContract]
    public class Customers:BindingList<Customer> {
        public Customers() { }
    }
    
    [DataContract]
    public class Customer {
        //Members
        private string _WholeAccountID="", _Name="";
        private string _Address="", _City="", _State="", _Zip="";

        public Customer(): this(null) { }
        public Customer(RoadshowDS.CustomerTableRow customer) { 
            //Constructor
            try {
                if(customer != null) {
                    if(!customer.IsWHOLE_ACCOUNT_IDNull()) this._WholeAccountID = customer.WHOLE_ACCOUNT_ID;
                    if(!customer.IsNAMENull()) this._Name = customer.NAME;
                    if(!customer.IsSTREET_ADDRESSNull()) this._Address = customer.STREET_ADDRESS;
                    if(!customer.IsCITYNull()) this._City = customer.CITY;
                    if(!customer.IsSTATENull()) this._State = customer.STATE;
                    if(!customer.IsZIPNull()) this._Zip = customer.ZIP;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Failed to create new customer.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string WholeAccountID { get { return this._WholeAccountID; } set { this._WholeAccountID = value; } }
        [DataMember]
        public string Name { get { return this._Name; } set { this._Name = value; } }
        [DataMember]
        public string Address { get { return this._Address; } set { this._Address = value; } }
        [DataMember]
        public string City { get { return this._City; } set { this._City = value; } }
        [DataMember]
        public string State { get { return this._State; } set { this._State = value; } }
        [DataMember]
        public string Zip { get { return this._Zip; } set { this._Zip = value; } }
        #endregion
    }
    
    [CollectionDataContract]
    public class Drivers:BindingList<Driver> {
        public Drivers() { }
    }
    
    [DataContract]
    public class Driver {
        //Members
        private string _Name="", _RouteName="";
        private int _HandDeviceTrackingNumber=0;

        public Driver(): this(null) { }
        public Driver(RoadshowDS.DriverTableRow driver) { 
            //Constructor
            try {
                if(driver != null) {
                    if(!driver.IsNAMENull()) this._Name = driver.NAME;
                    if(!driver.IsROUTE_NAMENull()) this._RouteName = driver.ROUTE_NAME;
                    if(!driver.IsHANDDEVICETRACKINGNUMBERNull()) this._HandDeviceTrackingNumber = driver.HANDDEVICETRACKINGNUMBER;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Failed to create new driver.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string Name { get { return this._Name; } set { this._Name = value; } }
        [DataMember]
        public string RouteName { get { return this._RouteName; } set { this._RouteName = value; } }
        [DataMember]
        public int HandDeviceTrackingNumber { get { return this._HandDeviceTrackingNumber; } set { this._HandDeviceTrackingNumber = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class OnTimeIssues:BindingList<OnTimeIssue> {
        public OnTimeIssues() { }
    }

    [DataContract]
    public class OnTimeIssue {
        //Members
        private int _Code=0;
        private string _Description="", _Comments="";
        
        public OnTimeIssue(): this(null) { }
        public OnTimeIssue(RoadshowDS.OnTimeIssueTableRow issue) { 
            //Constructor
            try {
                if(issue != null) {
                    if(!issue.IsOnTimeIssueCodeNull()) this._Code = issue.OnTimeIssueCode;
                    if(!issue.IsOnTimeIssueNull()) this._Description = issue.OnTimeIssue;
                    if(!issue.IsAdditionalCommentsNull()) this._Comments = issue.AdditionalComments;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Failed to create new on time issue.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int Code { get { return this._Code; } set { this._Code = value; } }
        [DataMember]
        public string Description { get { return this._Description; } set { this._Description = value; } }
        [DataMember]
        public string Comments { get { return this._Comments; } set { this._Comments = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class OrderTypes:BindingList<OrderType> {
        public OrderTypes() { }
    }

    [DataContract]
    public class OrderType {
        //Members
        private string _Description = "";

        public OrderType() : this(null) { }
        public OrderType(RoadshowDS.OrderTypeTableRow otype) {
            //Constructor
            try {
                if(otype != null) {
                    if(!otype.IsDESCRIPTIONNull()) this._Description = otype.DESCRIPTION;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Failed to create new commodity class.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string Description { get { return this._Description; } set { this._Description = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class Pickups:BindingList<Pickup> {
        public Pickups() { }
    }

    [DataContract]
    public class Pickup {
        //Members
        private int _RecordID = 0;
        private DateTime _Rt_Date;
        private string _Driver = "", _Rt_Name = "";
        private short _RetnTime=0;
        private string _Customer_ID="", _CustomerName="", _CustType="";
        private string _Address="", _City="", _State="", _Zip="", _OrderID = "";
        private float _PlanOrdSize=0, _PlanOrdLbs=0, _PlanOrdCuFt=0, _ActOrdSize=0, _ActOrdLbs=0;
        private string _Unsched_PU="", _Comments="", _OrdTyp="", _PlanCmdty="", _ActCmdty="", _Depot="";

        public Pickup() : this(null) { }
        public Pickup(RoadshowDS.PickupTableRow pickup) {
            //Constructor
            try {
                if(pickup != null) {
                    if(!pickup.IsRecordIDNull()) this._RecordID = pickup.RecordID;
                    if(!pickup.IsRt_DateNull()) this._Rt_Date = pickup.Rt_Date;
                    if(!pickup.IsDriverNull()) this._Driver = pickup.Driver;
                    if(!pickup.IsRt_NameNull()) this._Rt_Name = pickup.Rt_Name;
                    if(!pickup.IsRetnTimeNull()) this._RetnTime = pickup.RetnTime;
                    if(!pickup.IsCustomer_IDNull()) this._Customer_ID = pickup.Customer_ID;
                    if(!pickup.IsCustomerNameNull()) this._CustomerName = pickup.CustomerName;
                    if(!pickup.IsCustTypeNull()) this._CustType = pickup.CustType;
                    if(!pickup.IsAddressNull()) this._Address = pickup.Address;
                    if(!pickup.IsCityNull()) this._City = pickup.City;
                    if(!pickup.IsStateNull()) this._State = pickup.State;
                    if(!pickup.IsZipNull()) this._Zip = pickup.Zip;
                    if(!pickup.IsOrderIDNull()) this._OrderID = pickup.OrderID;
                    if(!pickup.IsPlanOrdSizeNull()) this._PlanOrdSize = pickup.PlanOrdSize;
                    if(!pickup.IsPlanOrdLbsNull()) this._PlanOrdLbs = pickup.PlanOrdLbs;
                    if(!pickup.IsPlanOrdCuFtNull()) this._PlanOrdCuFt = pickup.PlanOrdCuFt;
                    if(!pickup.IsActOrdSizeNull()) this._ActOrdSize = pickup.ActOrdSize;
                    if(!pickup.IsActOrdLbsNull()) this._ActOrdLbs = pickup.ActOrdLbs;
                    if(!pickup.IsUnsched_PUNull()) this._Unsched_PU = pickup.Unsched_PU;
                    if(!pickup.IsCommentsNull()) this._Comments = pickup.Comments;
                    if(!pickup.IsOrdTypNull()) this._OrdTyp = pickup.OrdTyp;
                    if(!pickup.IsPlanCmdtyNull()) this._PlanCmdty = pickup.PlanCmdty;
                    if(!pickup.IsActCmdtyNull()) this._ActCmdty = pickup.ActCmdty;
                    if(!pickup.IsDepotNull()) this._Depot = pickup.Depot;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Failed to create new commodity class.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int RecordID { get { return this._RecordID; } set { this._RecordID = value; } }
        [DataMember]
        public DateTime Rt_Date { get { return this._Rt_Date; } set { this._Rt_Date = value; } }
        [DataMember]
        public string Driver { get { return this._Driver; } set { this._Driver = value; } }
        [DataMember]
        public string Rt_Name { get { return this._Rt_Name; } set { this._Rt_Name = value; } }
        [DataMember]
        public short RetnTime { get { return this._RetnTime; } set { this._RetnTime = value; } }
        [DataMember]
        public string Customer_ID { get { return this._Customer_ID; } set { this._Customer_ID = value; } }
        [DataMember]
        public string CustomerName { get { return this._CustomerName; } set { this._CustomerName = value; } }
        [DataMember]
        public string CustType { get { return this._CustType; } set { this._CustType = value; } }
        [DataMember]
        public string Address { get { return this._Address; } set { this._Address = value; } }
        [DataMember]
        public string City { get { return this._City; } set { this._City = value; } }
        [DataMember]
        public string State { get { return this._State; } set { this._State = value; } }
        [DataMember]
        public string Zip { get { return this._Zip; } set { this._Zip = value; } }
        [DataMember]
        public string OrderID { get { return this._OrderID; } set { this._OrderID = value; } }
        [DataMember]
        public float PlanOrdSize { get { return this._PlanOrdSize; } set { this._PlanOrdSize = value; } }
        [DataMember]
        public float PlanOrdLbs { get { return this._PlanOrdLbs; } set { this._PlanOrdLbs = value; } }
        [DataMember]
        public float PlanOrdCuFt { get { return this._PlanOrdCuFt; } set { this._PlanOrdCuFt = value; } }
        [DataMember]
        public float ActOrdSize { get { return this._ActOrdSize; } set { this._ActOrdSize = value; } }
        [DataMember]
        public float ActOrdLbs { get { return this._ActOrdLbs; } set { this._ActOrdLbs = value; } }
        [DataMember]
        public string Unsched_PU { get { return this._Unsched_PU; } set { this._Unsched_PU = value; } }
        [DataMember]
        public string Comments { get { return this._Comments; } set { this._Comments = value; } }
        [DataMember]
        public string OrdTyp { get { return this._OrdTyp; } set { this._OrdTyp = value; } }
        [DataMember]
        public string PlanCmdty { get { return this._PlanCmdty; } set { this._PlanCmdty = value; } }
        [DataMember]
        public string ActCmdty { get { return this._ActCmdty; } set { this._ActCmdty = value; } }
        [DataMember]
        public string Depot { get { return this._Depot; } set { this._Depot = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class ScanAudits:BindingList<ScanAudit> {
        public ScanAudits() { }
    }

    [DataContract]
    public class ScanAudit {
        //Members
        private int _RecordID = 0;
        private DateTime _RtDate; 
        private string _Driver="", _RtName="";
        private int _RtSeq=0;
        private string _CustAcct="", _CustName="", _MallBldg="";
        private short _OrdOpen=0, _OrdClose=0, _WaitMin=0, _PlnArr=0, _PlnDep=0;
        private string _Arrive="", _Bell="", _DelStart="", _DelEnd="", _Depart="";
        private string _TimeEntryBy="", _OrderID="";
        private float _Pieces=0;
        private string _CmdtyCls="", _CmdtyDesc="", _OrdTyp="";
        private int _CtnsScanned=0;
        private string _ScanUser="", _Payee="";
        private short _Trip=0, _TripStop=0;
        private string _RtCls="", _RtSet="", _OnTimeIssue="", _ScanIssue="", _AdditComments="", _EntryBy="";
        private DateTime _Updated;
        private string _CRGStatus="", _CRGResolution="";

        public ScanAudit() : this(null) { }
        public ScanAudit(RoadshowDS.ScanAuditTableRow audit) {
            //Constructor
            try {
                if(audit != null) {
                    if(!audit.IsRecordIDNull()) this._RecordID = audit.RecordID;
                    if(!audit.IsRtDateNull()) this._RtDate = audit.RtDate;
                    if(!audit.IsDriverNull()) this._Driver = audit.Driver;
                    if(!audit.IsRtNameNull()) this._RtName = audit.RtName;
                    if(!audit.IsRtSeqNull()) this._RtSeq = audit.RtSeq;
                    if(!audit.IsCustAcctNull()) this._CustAcct = audit.CustAcct;
                    if(!audit.IsCustNameNull()) this._CustName = audit.CustName;
                    if(!audit.IsMallBldgNull()) this._MallBldg = audit.MallBldg;
                    if(!audit.IsOrdOpenNull()) this._OrdOpen = audit.OrdOpen;
                    if(!audit.IsOrdCloseNull()) this._OrdClose = audit.OrdClose;
                    if(!audit.IsWaitMinNull()) this._WaitMin = audit.WaitMin;
                    if(!audit.IsPlnArrNull()) this._PlnArr = audit.PlnArr;
                    if(!audit.IsPlnDepNull()) this._PlnDep = audit.PlnDep;
                    if(!audit.IsArriveNull()) this._Arrive = audit.Arrive;
                    if(!audit.IsBellNull()) this._Bell = audit.Bell;
                    if(!audit.IsDelStartNull()) this._DelStart = audit.DelStart;
                    if(!audit.IsDelEndNull()) this._DelEnd = audit.DelEnd;
                    if(!audit.IsDepartNull()) this._Depart = audit.Depart;
                    if(!audit.IsTimeEntryByNull()) this._TimeEntryBy = audit.TimeEntryBy;
                    if(!audit.IsOrderIDNull()) this._OrderID = audit.OrderID;
                    if(!audit.IsPiecesNull()) this._Pieces = audit.Pieces;
                    if(!audit.IsCmdtyClsNull()) this._CmdtyCls = audit.CmdtyCls;
                    if(!audit.IsCmdtyDescNull()) this._CmdtyDesc = audit.CmdtyDesc;
                    if(!audit.IsOrdTypNull()) this._OrdTyp = audit.OrdTyp;
                    if(!audit.IsCtnsScannedNull()) this._CtnsScanned = audit.CtnsScanned;
                    if(!audit.IsScanUserNull()) this._ScanUser = audit.ScanUser;
                    if(!audit.IsPayeeNull()) this._Payee = audit.Payee;
                    if(!audit.IsTripNull()) this._Trip = audit.Trip;
                    if(!audit.IsTripStopNull()) this._TripStop = audit.TripStop;
                    if(!audit.IsRtClsNull()) this._RtCls = audit.RtCls;
                    if(!audit.IsRtSetNull()) this._RtSet = audit.RtSet;
                    if(!audit.IsOnTimeIssueNull()) this._OnTimeIssue = audit.OnTimeIssue;
                    if(!audit.IsScanIssueNull()) this._ScanIssue = audit.ScanIssue;
                    if(!audit.IsAdditCommentsNull()) this._AdditComments = audit.AdditComments;
                    if(!audit.IsEntryByNull()) this._EntryBy = audit.EntryBy;
                    if(!audit.IsUpdatedNull()) this._Updated = audit.Updated;
                    if(!audit.IsCRGStatusNull()) this._CRGStatus = audit.CRGStatus;
                    if(!audit.IsCRGResolutionNull()) this._CRGResolution = audit.CRGResolution;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Failed to create new commodity class.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int RecordID { get { return this._RecordID; } set { this._RecordID = value; } }
        [DataMember]
        public DateTime RtDate { get { return this._RtDate; } set { this._RtDate = value; } }
        [DataMember]
        public string Driver { get { return this._Driver; } set { this._Driver = value; } }
        [DataMember]
        public string RtName { get { return this._RtName; } set { this._RtName = value; } }
        [DataMember]
        public int RtSeq { get { return this._RtSeq; } set { this._RtSeq = value; } }
        [DataMember]
        public string CustAcct { get { return this._CustAcct; } set { this._CustAcct = value; } }
        [DataMember]
        public string CustName { get { return this._CustName; } set { this._CustName = value; } }
        [DataMember]
        public string MallBldg { get { return this._MallBldg; } set { this._MallBldg = value; } }
        [DataMember]
        public short OrdOpen { get { return this._OrdOpen; } set { this._OrdOpen = value; } }
        [DataMember]
        public short OrdClose { get { return this._OrdClose; } set { this._OrdClose = value; } }
        [DataMember]
        public short WaitMin { get { return this._WaitMin; } set { this._WaitMin = value; } }
        [DataMember]
        public short PlnArr { get { return this._PlnArr; } set { this._PlnArr = value; } }
        [DataMember]
        public short PlnDep { get { return this._PlnDep; } set { this._PlnDep = value; } }
        [DataMember]
        public string Arrive { get { return this._Arrive; } set { this._Arrive = value; } }
        [DataMember]
        public string Bell { get { return this._Bell; } set { this._Bell = value; } }
        [DataMember]
        public string DelStart { get { return this._DelStart; } set { this._DelStart = value; } }
        [DataMember]
        public string DelEnd { get { return this._DelEnd; } set { this._DelEnd = value; } }
        [DataMember]
        public string Depart { get { return this._Depart; } set { this._Depart = value; } }
        [DataMember]
        public string TimeEntryBy { get { return this._TimeEntryBy; } set { this._TimeEntryBy = value; } }
        [DataMember]
        public string OrderID { get { return this._OrderID; } set { this._OrderID = value; } }
        [DataMember]
        public float Pieces { get { return this._Pieces; } set { this._Pieces = value; } }
        [DataMember]
        public string CmdtyCls { get { return this._CmdtyCls; } set { this._CmdtyCls = value; } }
        [DataMember]
        public string CmdtyDesc { get { return this._CmdtyDesc; } set { this._CmdtyDesc = value; } }
        [DataMember]
        public string OrdTyp { get { return this._OrdTyp; } set { this._OrdTyp = value; } }
        [DataMember]
        public int CtnsScanned { get { return this._CtnsScanned; } set { this._CtnsScanned = value; } }
        [DataMember]
        public string ScanUser { get { return this._ScanUser; } set { this._ScanUser = value; } }
        [DataMember]
        public string Payee { get { return this._Payee; } set { this._Payee = value; } }
        [DataMember]
        public short Trip { get { return this._Trip; } set { this._Trip = value; } }
        [DataMember]
        public short TripStop { get { return this._TripStop; } set { this._TripStop = value; } }
        [DataMember]
        public string RtCls { get { return this._RtCls; } set { this._RtCls = value; } }
        [DataMember]
        public string RtSet { get { return this._RtSet; } set { this._RtSet = value; } }
        [DataMember]
        public string OnTimeIssue { get { return this._OnTimeIssue; } set { this._OnTimeIssue = value; } }
        [DataMember]
        public string ScanIssue { get { return this._ScanIssue; } set { this._ScanIssue = value; } }
        [DataMember]
        public string AdditComments { get { return this._AdditComments; } set { this._AdditComments = value; } }
        [DataMember]
        public string EntryBy { get { return this._EntryBy; } set { this._EntryBy = value; } }
        [DataMember]
        public DateTime Updated { get { return this._Updated; } set { this._Updated = value; } }
        [DataMember]
        public string CRGStatus { get { return this._CRGStatus; } set { this._CRGStatus = value; } }
        [DataMember]
        public string CRGResolution { get { return this._CRGResolution; } set { this._CRGResolution = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class UpdateUsers:BindingList<UpdateUser> {
        public UpdateUsers() { }
    }
    
    [DataContract]
    public class UpdateUser {
        //Members
        private string _Name="", _RouteClass="";

        public UpdateUser(): this(null) { }
        public UpdateUser(RoadshowDS.UpdatedByTableRow user) { 
            //Constructor
            try {
                if(user != null) {
                    if(!user.IsUpdatedByNull()) this._Name = user.UpdatedBy;
                    if(!user.IsClassNull()) this._RouteClass = user.Class;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Failed to create new commodity class.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string Name { get { return this._Name; } set { this._Name = value; } }
        [DataMember]
        public string RouteClass { get { return this._RouteClass; } set { this._RouteClass = value; } }
        #endregion
    }

    [DataContract]
    public class RoadshowFault {
        private Exception _ex = null;
        public RoadshowFault(Exception ex) { this._ex = ex; }
        [DataMember]
        public Exception Exception { get { return this._ex; } set { this._ex = value; } }
    }
}
