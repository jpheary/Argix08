using System;
using System.Data;

namespace Argix.Enterprise {
	//
	public class Pickup {
		//Members
		private string _id="";
		private string _clientnumber="", _divisionnumber="", _clientname="";
		private string _shippernumber="", _shippername="", _pickupdate="";
		private int _pickupnumber=0, _tdsnumber=0;
		private string _freighttype="", _vendorkey="", _trailernumber = "", _sealnumber = "";
		
		//Interface
		public Pickup(): this(null) { }
		public Pickup(PickupDS.PickupTableRow pickup) { 
			//Constructor
			try { 
				if(pickup != null) { 
					if(!pickup.IsIDNull()) this._id = pickup.ID;
					if(!pickup.IsClientNumberNull()) this._clientnumber = pickup.ClientNumber.Trim();
                    if(!pickup.IsDivisionNumberNull()) this._divisionnumber = pickup.DivisionNumber.Trim();
                    if(!pickup.IsClientNameNull()) this._clientname = pickup.ClientName.Trim();
                    if(!pickup.IsShipperNumberNull()) this._shippernumber = pickup.ShipperNumber.Trim();
                    if(!pickup.IsShipperNameNull()) this._shippername = pickup.ShipperName.Trim();
					if(!pickup.IsPickUpDateNull()) this._pickupdate = pickup.PickUpDate;
					if(!pickup.IsPickupNumberNull()) this._pickupnumber = pickup.PickupNumber;
                    if(!pickup.IsFreightTypeNull()) this._freighttype = pickup.FreightType.Trim();
					if(!pickup.IsTDSNumberNull()) this._tdsnumber = pickup.TDSNumber;
                    if(!pickup.IsVendorKeyNull()) this._vendorkey = pickup.VendorKey.Trim();
                    if(!pickup.IsTrailerNumberNull()) this._trailernumber = pickup.TrailerNumber.Trim();
                    if(!pickup.IsSealNumberNull()) this._sealnumber = pickup.SealNumber.Trim();
				}
			}
			catch(Exception ex) { throw ex; }
		}
		#region Accessors\Modifiers: [Members...]
		public string ID { get { return this._id; } }
		public string ClientNumber { get { return this._clientnumber; } set { this._clientnumber = value; } }
		public string DivisionNumber { get { return this._divisionnumber; } set { this._divisionnumber = value; } }
		public string ClientName { get { return this._clientname; } set { this._clientname = value; } }
		public string ShipperNumber { get { return this._shippernumber; } set { this._shippernumber = value; } }
		public string ShipperName { get { return this._shippername; } set { this._shippername = value; } }
		public string PickUpDate { get { return this._pickupdate; } set { this._pickupdate = value; } }
		public int PickupNumber { get { return this._pickupnumber; } set { this._pickupnumber = value; } }
		public string FreightType { get { return this._freighttype; } set { this._freighttype = value; } }
		public int TDSNumber { get { return this._tdsnumber; } set { this._tdsnumber = value; } }
		public string VendorKey { get { return this._vendorkey; } set { this._vendorkey = value; } }
        public string TrailerNumber { get { return this._trailernumber; } set { this._trailernumber = value; } }
        public string SealNumber { get { return this._sealnumber; } set { this._sealnumber = value; } }
		#endregion
	}
}
