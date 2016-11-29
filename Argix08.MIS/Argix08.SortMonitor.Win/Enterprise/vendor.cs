//	File:	vendor.cs
//	Author:	J. Heary
//	Date:	02/27/07
//	Desc:	Represents the state and behavior of an Argix vendor.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;

namespace Tsort.Enterprise {
	//
	internal class Vendor: Shipper {
		//Members
		
		//Constants
		//Events
		//Interface
		public Vendor(): this(null) { }
		public Vendor(VendorDS.VendorDetailTableRow vendor): base(vendor) { 
			//Constructor
			try { 
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new vendor instance.", ex); }
		}
		public Vendor(string number, string name, string addressLine1, string addressLine2, string addressCity, string addressState, string addressZip, string userData): base(number, name, addressLine1, addressLine2, addressCity, addressState, addressZip, userData) { }
		#region Accessors\Modifiers: ToDataSet()
		public override DataSet ToDataSet() { 
			//Return a dataset containing values for this object
			VendorDS ds=null;
			try { 
				ds = new VendorDS();
				VendorDS.VendorDetailTableRow vendor = ds.VendorDetailTable.NewVendorDetailTableRow();
				vendor.NUMBER = base._number;
				vendor.NAME = base._name;
				vendor.STATUS = base._status;
				vendor.ADDRESS_LINE1 = base._address_line1;
				vendor.ADDRESS_LINE2 = base._address_line2;
				vendor.CITY = base._city;
				vendor.STATE = base._state;
				vendor.ZIP = base._zip;
				vendor.ZIP4 = base._zip4;
				vendor.USERDATA = base._userdata;
				ds.VendorDetailTable.AddVendorDetailTableRow(vendor);
				ds.AcceptChanges();
			}
			catch(Exception) { }
			return ds;
		}
		#endregion
	}
}