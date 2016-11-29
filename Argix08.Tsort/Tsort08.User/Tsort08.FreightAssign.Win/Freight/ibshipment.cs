//	File:	ibshipment.cs
//	Author:	J. Heary
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Argix.Freight {
	//
	public class IBShipment {
		//Members
		private string _freightID="", _freightType="";
		private string _currentLocation="";
		private int _tdsNumber=0;
		private string _trailerNumber="", _storageTrailerNumber="";
		private string _clientNumber="", _clientName="";
		private string _shipperNumber="", _shipperName="";
		private string _pickup="", _status="";
		private int _cartons=0, _pallets=0;
		private int _carrierNumber=0, _driverNumber=0;
		private string _floorStatus="", _sealNumber="", _unloadedStatus="";
		private string _vendorKey="";
		private string _receiveDate="";
		private int _terminalID=0;
		private byte _issortable;
						
		//Interface
		public IBShipment() : this(null) { }
        public IBShipment(InboundFreightDS.InboundFreightTableRow shipment) {
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
			catch(Exception ex) { throw new ApplicationException("Could not create a new inbound shipment.", ex); }
        }
        #region Accessors\modifiers: [Members...]
        public string FreightID { get { return this._freightID; } }
		public string FreightType { get { return this._freightType; } }
		public string CurrentLocation { get { return this._currentLocation; } }
		public int TDSNumber { get { return this._tdsNumber; } }
		public string TrailerNumber { get { return this._trailerNumber; } }
		public string StorageTrailerNumber { get { return this._storageTrailerNumber; } }
		public string ClientNumber { get { return this._clientNumber; } }
		public string ClientName { get { return this._clientName; } }
		public string ShipperNumber { get { return this._shipperNumber; } }
		public string ShipperName { get { return this._shipperName; } }
		public string Pickup { get { return this._pickup; } }
        public ShipmentStatusEnum Status { 
            get {
                switch(this._status.ToLower()) {
                    case "unsorted": return ShipmentStatusEnum.Unsorted;
                    case "sorting": return ShipmentStatusEnum.Sorting;
                    case "sorted": return ShipmentStatusEnum.Sorted;
                    default: return ShipmentStatusEnum.Unknown;
                }
            } 
        }
		public int Cartons { get { return this._cartons; } }
		public int Pallets { get { return this._pallets; } }
		public int CarrierNumber { get { return this._carrierNumber; } }
		public int DriverNumber { get { return this._driverNumber; } }
		public string FloorStatus { get { return this._floorStatus; } }
		public string SealNumber { get { return this._sealNumber; } }
		public string UnloadedStatus { get { return this._unloadedStatus; } }
		public string VendorKey { get { return this._vendorKey; } }
		public string ReceiveDate { get { return this._receiveDate; } }
		public int TerminalID { get { return this._terminalID; } }
		public System.Boolean IsSortable { 
			get { return (this._issortable == 1); }
			set { this._issortable = value ? (byte)1 : (byte)0; }
		}
		#endregion
	}
}
