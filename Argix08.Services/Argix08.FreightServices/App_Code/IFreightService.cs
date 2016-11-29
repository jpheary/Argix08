using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Freight {
    //
    [ServiceContract(Namespace="http://Argix.Freight")]
    public interface IFreightService {
        //Interface
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.ConfigurationFault")]
        UserConfiguration GetUserConfiguration(string application,string[] usernames);

        [OperationContract(IsOneWay = true)]
        void WriteLogEntry(TraceMessage m);

        [OperationContract]
        TerminalInfo GetTerminalInfo();

        [OperationContract]
        Terminals GetTerminals();

        [OperationContract]
        InboundFreight GetInboundFreight(int terminalID,int sortedRange);

        [OperationContract]
        InboundShipment GetInboundShipment(string freightID);
        
        [OperationContract]
        StationAssignments GetStationAssignments();
    }

    [DataContract]
    public class TerminalInfo {
        private int mTerminalID = 0;
        private string mNumber = "",mDescription = "",mConnection = "";

        [DataMember]
        public int TerminalID { get { return this.mTerminalID; } set { this.mTerminalID = value; } }
        [DataMember]
        public string Number { get { return this.mNumber; } set { this.mNumber = value; } }
        [DataMember]
        public string Description { get { return this.mDescription; } set { this.mDescription = value; } }
        [DataMember]
        public string Connection { get { return this.mConnection; } set { this.mConnection = value; } }
    }

    [CollectionDataContract]
    public class Terminals:BindingList<Terminal> {
        public Terminals() { }
    }

    [DataContract]
    public class Terminal {
        private int mTerminalID = 0;
        private string mNumber = "",mDescription = "",mAgentID = "";

        [DataMember]
        public int TerminalID { get { return this.mTerminalID; } set { this.mTerminalID = value; } }
        [DataMember]
        public string Number { get { return this.mNumber; } set { this.mNumber = value; } }
        [DataMember]
        public string Description { get { return this.mDescription; } set { this.mDescription = value; } }
        [DataMember]
        public string AgentID { get { return this.mAgentID; } set { this.mAgentID = value; } }
    }

    [CollectionDataContract]
    public class InboundFreight:BindingList<InboundShipment> { }

    [DataContract]
    public class InboundShipment {
        //Members
        private string _freightID = "",_freightType = "";
        private string _currentLocation = "";
        private int _tdsNumber = 0;
        private string _trailerNumber = "",_storageTrailerNumber = "";
        private string _clientNumber = "",_clientName = "";
        private string _shipperNumber = "",_shipperName = "";
        private string _pickup = "",_status = "";
        private int _cartons = 0,_pallets = 0;
        private int _carrierNumber = 0,_driverNumber = 0;
        private string _floorStatus = "",_sealNumber = "",_unloadedStatus = "";
        private string _vendorKey = "", _receiveDate = "";
        private int _terminalID = 0;
        private byte _issortable;

        //Interface
        public InboundShipment() : this(null) { }
        public InboundShipment(FreightDS.InboundFreightTableRow shipment) {
            //Constructor
            try {
                if(shipment != null) {
                    if(!shipment.IsFreightIDNull()) this._freightID = shipment.FreightID;
                    if(!shipment.IsFreightTypeNull()) this._freightType = shipment.FreightType;
                    if(!shipment.IsCurrentLocationNull()) this._currentLocation = shipment.CurrentLocation;
                    if(!shipment.IsTDSNumberNull()) this._tdsNumber = shipment.TDSNumber;
                    if(!shipment.IsTrailerNumberNull()) this._trailerNumber = shipment.TrailerNumber;
                    if(!shipment.IsStorageTrailerNumberNull()) this._storageTrailerNumber = shipment.StorageTrailerNumber;
                    if(!shipment.IsClientNumberNull()) this._clientNumber = shipment.ClientNumber;
                    if(!shipment.IsClientNameNull()) this._clientName = shipment.ClientName;
                    if(!shipment.IsShipperNumberNull()) this._shipperNumber = shipment.ShipperNumber;
                    if(!shipment.IsShipperNameNull()) this._shipperName = shipment.ShipperName;
                    if(!shipment.IsPickupNull()) this._pickup = shipment.Pickup;
                    if(!shipment.IsStatusNull()) this._status = shipment.Status;
                    if(!shipment.IsCartonsNull()) this._cartons = shipment.Cartons;
                    if(!shipment.IsPalletsNull()) this._pallets = shipment.Pallets;
                    if(!shipment.IsCarrierNumberNull()) this._carrierNumber = shipment.CarrierNumber;
                    if(!shipment.IsDriverNumberNull()) this._driverNumber = shipment.DriverNumber;
                    if(!shipment.IsFloorStatusNull()) this._floorStatus = shipment.FloorStatus;
                    if(!shipment.IsSealNumberNull()) this._sealNumber = shipment.SealNumber;
                    if(!shipment.IsUnloadedStatusNull()) this._unloadedStatus = shipment.UnloadedStatus;
                    if(!shipment.IsVendorKeyNull()) this._vendorKey = shipment.VendorKey;
                    if(!shipment.IsReceiveDateNull()) this._receiveDate = shipment.ReceiveDate;
                    if(!shipment.IsTerminalIDNull()) this._terminalID = shipment.TerminalID;
                    if(!shipment.IsIsSortableNull()) this._issortable = shipment.IsSortable;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Could not create a new inbound shipment.",ex); }
        }
        #region Accessors\modifiers: [Members...]
        [DataMember]
        public string FreightID { get { return this._freightID; } set { this._freightID = value; } }
        [DataMember]
        public string FreightType { get { return this._freightType; } set { this._freightType = value; } }
        [DataMember]
        public string CurrentLocation { get { return this._currentLocation; } set { this._currentLocation = value; } }
        [DataMember]
        public int TDSNumber { get { return this._tdsNumber; } set { this._tdsNumber = value; } }
        [DataMember]
        public string TrailerNumber { get { return this._trailerNumber; } set { this._trailerNumber = value; } }
        [DataMember]
        public string StorageTrailerNumber { get { return this._storageTrailerNumber; } set { this._storageTrailerNumber = value; } }
        [DataMember]
        public string ClientNumber { get { return this._clientNumber; } set { this._clientNumber = value; } }
        [DataMember]
        public string ClientName { get { return this._clientName; } set { this._clientName = value; } }
        [DataMember]
        public string ShipperNumber { get { return this._shipperNumber; } set { this._shipperNumber = value; } }
        [DataMember]
        public string ShipperName { get { return this._shipperName; } set { this._shipperName = value; } }
        [DataMember]
        public string Pickup { get { return this._pickup; } set { this._pickup = value; } }
        [DataMember]
        public string Status { get { return this._status; } set { this._status = value; } }
        [DataMember]
        public int Cartons { get { return this._cartons; } set { this._cartons = value; } }
        [DataMember]
        public int Pallets { get { return this._pallets; } set { this._pallets = value; } }
        [DataMember]
        public int CarrierNumber { get { return this._carrierNumber; } set { this._carrierNumber = value; } }
        [DataMember]
        public int DriverNumber { get { return this._driverNumber; } set { this._driverNumber = value; } }
        [DataMember]
        public string FloorStatus { get { return this._floorStatus; } set { this._floorStatus = value; } }
        [DataMember]
        public string SealNumber { get { return this._sealNumber; } set { this._sealNumber = value; } }
        [DataMember]
        public string UnloadedStatus { get { return this._unloadedStatus; } set { this._unloadedStatus = value; } }
        [DataMember]
        public string VendorKey { get { return this._vendorKey; } set { this._vendorKey = value; } }
        [DataMember]
        public string ReceiveDate { get { return this._receiveDate; } set { this._receiveDate = value; } }
        [DataMember]
        public int TerminalID { get { return this._terminalID; } set { this._terminalID = value; } }
        [DataMember]
        public bool IsSortable { get { return (this._issortable == 1); } set { this._issortable = value ? (byte)1 : (byte)0; } }
        #endregion
    }

    [DataContract]
    public class Workstation {
        //Members
        private string mWorkStationID = "", mName = "";
        private int mTerminalID = 0;
        private string mNumber = "",mDescription = "";
        private string mScaleType = "",mScalePort = "", mPrinterType = "",mPrinterPort = "";
        private bool mTrace = false,mIsActive = true;

        //Interface
        public Workstation() : this(null) { }
        public Workstation(EnterpriseDS.WorkstationTableRow workstation) {
            //Constructor
            try {
                //Configure this station from the station configuration information
                if(workstation != null) {
                    this.mWorkStationID = workstation.WorkStationID;
                    if(!workstation.IsNameNull()) this.mName = workstation.Name;
                    if(!workstation.IsTerminalIDNull()) this.mTerminalID = workstation.TerminalID;
                    if(!workstation.IsNumberNull()) this.mNumber = workstation.Number;
                    if(!workstation.IsDescriptionNull()) this.mDescription = workstation.Description;
                    if(!workstation.IsScaleTypeNull()) this.mScaleType = workstation.ScaleType;
                    if(!workstation.IsScalePortNull()) this.mScalePort = workstation.ScalePort;
                    if(!workstation.IsPrinterTypeNull()) this.mPrinterType = workstation.PrinterType;
                    if(!workstation.IsPrinterPortNull()) this.mPrinterPort = workstation.PrinterPort;
                    if(!workstation.IsTraceNull()) this.mTrace = workstation.Trace;
                    if(!workstation.IsIsActiveNull()) this.mIsActive = workstation.IsActive;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new workstation instance.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string WorkStationID { get { return this.mWorkStationID; } set { this.mWorkStationID = value; } }
        [DataMember]
        public string Name { get { return this.mName; } set { this.mName = value; } }
        [DataMember]
        public int TerminalID { get { return this.mTerminalID; } set { this.mTerminalID = value; } }
        [DataMember]
        public string Number { get { return this.mNumber; } set { this.mNumber = value; } }
        [DataMember]
        public string Description { get { return this.mDescription; } set { this.mDescription = value; } }
        [DataMember]
        public string ScaleType { get { return this.mScaleType; } set { this.mScaleType = value; } }
        [DataMember]
        public string ScalePort { get { return this.mScalePort; } set { this.mScalePort = value; } }
        [DataMember]
        public string PrinterType { get { return this.mPrinterType; } set { this.mPrinterType = value; } }
        [DataMember]
        public string PrinterPort { get { return this.mPrinterPort; } set { this.mPrinterPort = value; } }
        [DataMember]
        public bool Trace { get { return this.mTrace; } set { this.mTrace = value; } }
        [DataMember]
        public bool IsActive { get { return this.mIsActive; } set { this.mIsActive = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class StationAssignments:BindingList<StationAssignment> { }

    [DataContract]
    public class StationAssignment {
        //Members
        private Workstation mWorkStation = null;
        private InboundShipment mInboundFreight = null;
        private int mSortTypeID = 0;
        private string mSortType = "";

        //Interface
        public StationAssignment() { }
        public StationAssignment(Workstation sortStation,InboundShipment inboundFreight,int sortTypeID) {
            //Constructor
            try {
                this.mWorkStation = sortStation;
                this.mInboundFreight = inboundFreight;
                this.mSortTypeID = sortTypeID;
            }
            catch(Exception ex) { throw new ApplicationException("Could not create a new station assignment.",ex); }
        }
        public StationAssignment(FreightDS.StationFreightAssignmentTableRow assignment) {
            //Constructor
            this.mWorkStation = new Workstation();
            this.mWorkStation.WorkStationID = assignment.WorkStationID;
            this.mWorkStation.Number = assignment.StationNumber;
            
            this.mInboundFreight = new InboundShipment();
            this.mInboundFreight.FreightID = assignment.FreightID;
            this.mInboundFreight.FreightType = assignment.FreightType;
            this.mInboundFreight.TDSNumber = assignment.TDSNumber;
            this.mInboundFreight.TrailerNumber = assignment.TrailerNumber;
            this.mInboundFreight.ClientNumber = assignment.Client;
            this.mInboundFreight.ShipperNumber = assignment.Shipper;
            this.mInboundFreight.Pickup = assignment.Pickup;
            this.mInboundFreight.TerminalID = assignment.TerminalID;
            
            this.mSortTypeID = assignment.SortTypeID;
            this.mSortType = assignment.SortType;
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public Workstation SortStation { get { return this.mWorkStation; } set { this.mWorkStation = value; } }
        [DataMember]
        public InboundShipment InboundFreight { get { return this.mInboundFreight; } set { this.mInboundFreight = value; } }
        [DataMember]
        public int SortTypeID { get { return this.mSortTypeID; } set { this.mSortTypeID = value; } }
        [DataMember]
        public string SortType { get { return this.mSortType; } set { this.mSortType = value; } }
        #endregion
    }

    [DataContract]
    public class EnterpriseFault {
        private Exception _ex = null;
        public EnterpriseFault(Exception ex) { this._ex = ex; }
        [DataMember]
        public Exception Exception { get { return this._ex; } set { this._ex = value; } }
    }

    [DataContract]
    public class FreightFault {
        private Exception _ex;
        public FreightFault(Exception ex) { this._ex = ex; }

        [DataMember]
        public Exception Exception { get { return this._ex; } set { this._ex = value; } }
    }
}