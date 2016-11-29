//	File:	pickup.cs
//	Author:	jheary
//	Date:	09/29/05
//	Desc:		
//	Rev:		
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Text;
using Argix.Data;

namespace Argix.Dispatch {
	//
	public class Pickup: ScheduleEntry {
		//Members
		private System.Int32 _id=0;
		private System.DateTime _created=DateTime.Now;
		private System.String _createdby=Environment.UserName;
		private System.String _callername="";
		private System.String _clientname="";
		private System.String _shippername="";
		private System.String _shipperaddress="";
		private System.String _deliverywindow="";
		private System.String _terminal="";
		private System.String _drivername="";
		private System.Int32 _amount=0;
		private System.String _amounttype="";
		private System.String _freighttype="";
		private System.Int32 _autonumber=0;
		private System.DateTime _requestdate=DateTime.Today;
		private System.DateTime _pickupdate=DateTime.Today.AddDays(1);
		private System.String _comments="";
		private System.Boolean _updated=false;
		private System.Boolean _mustbeready=false;
		private System.Boolean _pickedup=false;
		
		//Constants
		
		//Events
		
		//Interface
		public Pickup(Mediator mediator): this(null, mediator) { }
		public Pickup(DispatchDS.PickupLogTableRow row, Mediator mediator): base(mediator) { 
			//Constructor
			try { 
				if(row != null) { 
					this._id = row.ID;
					this._created = row.Created;
					this._createdby = row.CreatedBy;
					if(!row.IsCallerNameNull()) this._callername = row.CallerName;
					if(!row.IsClientNameNull()) this._clientname = row.ClientName;
					if(!row.IsShipperNameNull()) this._shippername = row.ShipperName;
					if(!row.IsShipperAddressNull()) this._shipperaddress = row.ShipperAddress;
					if(!row.IsDeliveryWindowNull()) this._deliverywindow = row.DeliveryWindow;
					if(!row.IsTerminalNull()) this._terminal = row.Terminal;
					if(!row.IsDriverNameNull()) this._drivername = row.DriverName;
					if(!row.IsAmountNull()) this._amount = row.Amount;
					if(!row.IsAmountTypeNull()) this._amounttype = row.AmountType;
					if(!row.IsFreightTypeNull()) this._freighttype = row.FreightType;
					if(!row.IsAutoNumberNull()) this._autonumber = row.AutoNumber;
					if(!row.IsRequestDateNull()) this._requestdate = row.RequestDate;
					if(!row.IsPickUpDateNull()) this._pickupdate = row.PickUpDate;
					if(!row.IsCommentsNull()) this._comments = row.Comments;
					if(!row.IsUpdatedNull()) this._updated = row.Updated;
					if(!row.IsMustBeReadyNull()) this._mustbeready = row.MustBeReady;
					if(!row.IsPickedUpNull()) this._pickedup = row.PickedUp;
				}
			}
			catch(Exception ex) { throw ex; }
		}
		#region Accessors\Modifiers, ToDataSet(), ToString()
		public System.Int32 ID { 
			get { return this._id; }
			set { this._id = value; }
		}
		public System.DateTime Created { 
			get { return this._created; }
			set { this._created = value; }
		}
		public System.String CreatedBy { 
			get { return this._createdby; }
			set { this._createdby = value; }
		}
		public System.String CallerName { 
			get { return this._callername; }
			set { this._callername = value; }
		}
		public System.String ClientName { 
			get { return this._clientname; }
			set { this._clientname = value; }
		}
		public System.String ShipperName { 
			get { return this._shippername; }
			set { this._shippername = value; }
		}
		public System.String ShipperAddress { 
			get { return this._shipperaddress; }
			set { this._shipperaddress = value; }
		}
		public System.String DeliveryWindow { 
			get { return this._deliverywindow; }
			set { this._deliverywindow = value; }
		}
		public System.String Terminal { 
			get { return this._terminal; }
			set { this._terminal = value; }
		}
		public System.String DriverName { 
			get { return this._drivername; }
			set { this._drivername = value; }
		}
		public System.Int32 Amount { 
			get { return this._amount; }
			set { this._amount = value; }
		}
		public System.String AmountType { 
			get { return this._amounttype; }
			set { this._amounttype = value; }
		}
		public System.String FreightType { 
			get { return this._freighttype; }
			set { this._freighttype = value; }
		}
		public System.Int32 AutoNumber { 
			get { return this._autonumber; }
			set { this._autonumber = value; }
		}
		public System.DateTime RequestDate { 
			get { return this._requestdate; }
			set { this._requestdate = value; }
		}
		public System.DateTime PickUpDate { 
			get { return this._pickupdate; }
			set { this._pickupdate = value; }
		}
		public System.String Comments { 
			get { return this._comments; }
			set { this._comments = value; }
		}
		public System.Boolean Updated { 
			get { return this._updated; }
			set { this._updated = value; }
		}
		public System.Boolean MustBeReady { 
			get { return this._mustbeready; }
			set { this._mustbeready = value; }
		}
		public System.Boolean PickedUp { 
			get { return this._pickedup; }
			set { this._pickedup = value; }
		}
		public override DataSet ToDataSet() { 
			//Return a dataset containing values for this object
			DispatchDS ds=null;
			try { 
				ds = new DispatchDS();
				DispatchDS.PickupLogTableRow row = ds.PickupLogTable.NewPickupLogTableRow();
				row.ID = this._id;
				row.Created = this._created;
				row.CreatedBy = this._createdby;
				row.CallerName = this._callername;
				row.ClientName = this._clientname;
				row.ShipperName = this._shippername;
				row.ShipperAddress = this._shipperaddress;
				row.DeliveryWindow = this._deliverywindow;
				row.Terminal = this._terminal;
				row.DriverName = this._drivername;
				row.Amount = this._amount;
				row.AmountType = this._amounttype;
				row.FreightType = this._freighttype;
				row.AutoNumber = this._autonumber;
				row.RequestDate = this._requestdate;
				row.PickUpDate = this._pickupdate;
				row.Comments = this._comments;
				row.Updated = this._updated;
				row.MustBeReady = this._mustbeready;
				row.PickedUp = this._pickedup;
				ds.PickupLogTable.AddPickupLogTableRow(row);
			}
			catch(Exception) { }
			return ds;
		}
		public override string ToString() { 
			//Custom ToString() method
			string sThis=base.ToString();
			try { 
				//Form string detail of this object
				StringBuilder builder = new StringBuilder();
				builder.Append("ID=" + this._id.ToString() + "\t");
				builder.Append("Created=" + this._created.ToString() + "\t");
				builder.Append("CreatedBy=" + this._createdby.ToString() + "\t");
				builder.Append("CallerName=" + this._callername.ToString() + "\t");
				builder.Append("ClientName=" + this._clientname.ToString() + "\t");
				builder.Append("ShipperName=" + this._shippername.ToString() + "\t");
				builder.Append("ShipperAddress=" + this._shipperaddress.ToString() + "\t");
				builder.Append("DeliveryWindow=" + this._deliverywindow.ToString() + "\t");
				builder.Append("Terminal=" + this._terminal.ToString() + "\t");
				builder.Append("DriverName=" + this._drivername.ToString() + "\t");
				builder.Append("Amount=" + this._amount.ToString() + "\t");
				builder.Append("AmountType=" + this._amounttype.ToString() + "\t");
				builder.Append("FreightType=" + this._freighttype.ToString() + "\t");
				builder.Append("AutoNumber=" + this._autonumber.ToString() + "\t");
				builder.Append("RequestDate=" + this._requestdate.ToString() + "\t");
				builder.Append("PickUpDate=" + this._pickupdate.ToString() + "\t");
				builder.Append("Comments=" + this._comments.ToString() + "\t");
				builder.Append("Updated=" + this._updated.ToString() + "\t");
				builder.Append("MustBeReady=" + this._mustbeready.ToString() + "\t");
				builder.Append("PickedUp=" + this._pickedup.ToString() + "\t");
				builder.Append("\n");
				sThis = builder.ToString();
			}
			catch(Exception) { }
			return sThis;
		}
		#endregion
		public override string EntryType { get { return "Pickup"; } }
		public override int EntryID { get { return this._id; } }
		public override bool Create() {
			//Save this object
			bool bRet=false;
			try {
				DispatchDS ds = new DispatchDS();
				ds.Merge(base.mMediator.FillDataset(base.mDataFile, "", null));
				if(this._id <= 0) {
					//For new entries only; some entries are moved between schedules
					int ID = 0;
					for(int i=0; i<ds.PickupLogTable.Rows.Count; i++) 
						if(ds.PickupLogTable[i].ID > ID) ID = ds.PickupLogTable[i].ID;
					this._id = ID + 1;
				}
				DispatchDS.PickupLogTableRow row = ds.PickupLogTable.NewPickupLogTableRow();
				row.ID = this._id;
				row.Created = this._created;
				row.CreatedBy = this._createdby;
				row.CallerName = this._callername;
				row.ClientName = this._clientname;
				row.ShipperName = this._shippername;
				row.ShipperAddress = this._shipperaddress;
				row.DeliveryWindow = this._deliverywindow;
				row.Terminal = this._terminal;
				row.DriverName = this._drivername;
				row.Amount = this._amount;
				row.AmountType = this._amounttype;
				row.FreightType = this._freighttype;
				row.AutoNumber = this._autonumber;
				row.RequestDate = this._requestdate;
				if(this._pickupdate != DateTime.MinValue) row.PickUpDate = this._pickupdate;
				row.Comments = this._comments;
				row.Updated = this._updated;
				row.MustBeReady = this._mustbeready;
				row.PickedUp = this._pickedup;
				ds.PickupLogTable.AddPickupLogTableRow(this._id,this._created,this._createdby,this._callername,this._clientname,this._shippername,this._shipperaddress,this._deliverywindow,this._terminal,this._drivername,this._amount,this._amounttype,this._freighttype,this._autonumber,this._requestdate,this._pickupdate,this._comments,this._updated,this._mustbeready,this._pickedup);
				ds.PickupLogTable.AcceptChanges();
				bRet = base.mMediator.ExecuteNonQuery(base.mDataFile, new object[]{ds});
				base.OnEntryChanged(this, new EventArgs());
			}
			catch(Exception ex) { throw ex; }
			return bRet;
		}
		public override bool Update() {
			//Update this object
			bool bRet=false;
			try {
				DispatchDS ds = new DispatchDS();
				ds.Merge(base.mMediator.FillDataset(base.mDataFile, "", null));
				DispatchDS.PickupLogTableRow row = (DispatchDS.PickupLogTableRow)ds.PickupLogTable.Select("ID = " + this._id)[0];
				row.CallerName = this._callername;
				row.ClientName = this._clientname;
				row.ShipperName = this._shippername;
				row.ShipperAddress = this._shipperaddress;
				row.DeliveryWindow = this._deliverywindow;
				row.Terminal = this._terminal;
				row.DriverName = this._drivername;
				row.Amount = this._amount;
				row.AmountType = this._amounttype;
				row.FreightType = this._freighttype;
				row.AutoNumber = this._autonumber;
				row.RequestDate = this._requestdate;
				if(this._pickupdate != DateTime.MinValue) row.PickUpDate = this._pickupdate;
				row.Comments = this._comments;
				row.Updated = this._updated;
				row.MustBeReady = this._mustbeready;
				row.PickedUp = this._pickedup;
				ds.PickupLogTable.AcceptChanges();
				bRet = base.mMediator.ExecuteNonQuery(base.mDataFile, new object[]{ds});
				base.OnEntryChanged(this, new EventArgs());
			}
			catch(Exception ex) { throw ex; }
			return bRet;
		}
		public override bool Delete() {
			//Delete this object
			bool bRet=false;
			try {
				DispatchDS ds = new DispatchDS();
				ds.Merge(base.mMediator.FillDataset(base.mDataFile, "", null));
				DispatchDS.PickupLogTableRow row = (DispatchDS.PickupLogTableRow)ds.PickupLogTable.Select("ID = " + this._id)[0];
				row.Delete();
				ds.PickupLogTable.AcceptChanges();
				bRet = base.mMediator.ExecuteNonQuery(base.mDataFile, new object[]{ds});
				base.OnEntryChanged(this, new EventArgs());
			}
			catch(Exception ex) { throw ex; }
			return bRet;
		}
//		public override ScheduleEntry Copy() {
//			//Return a copy of this object
//			Pickup oEntry=null;
//			try {
//				oEntry = new Pickup((DispatchDS.PickupLogTableRow)this.ToDataSet().Tables[PickupLog.SCHEDULE_TABLENAME].Rows[0], this.mMediator);
//			}
//			catch(Exception ex) { throw ex; }
//			return oEntry;
//		}
	}
}
