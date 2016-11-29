//	File:	inboundfreigth.cs
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
	public class InboundFreight: ScheduleEntry {
		//Members
		private System.Int32 _id=0;
		private System.DateTime _created=DateTime.Now;
		private System.String _createdby=Environment.UserName;
		private System.String _drivername="";
		private System.String _trailernumber="";
		private System.String _fromlocation="";
		private System.String _tolocation="";
		private System.DateTime _scheduleddeparture=DateTime.Today;
		private System.DateTime _actualdeparture=DateTime.MinValue;
		private System.DateTime _scheduleddelivery=DateTime.Today;
		private System.DateTime _actualdelivery=DateTime.MinValue;
		private System.String _comments="";
		private System.Boolean _confirmed=false;
		
		//Constants
		
		//Events
		
		//Interface
		public InboundFreight(Mediator mediator): this(null, mediator) { }
		public InboundFreight(DispatchDS.ScheduledInboundTableRow row, Mediator mediator): base(mediator) { 
			//Constructor
			try { 
				if(row != null) { 
					this._id = row.ID;
					this._created = row.Created;
					this._createdby = row.CreatedBy;
					if(!row.IsDriverNameNull()) this._drivername = row.DriverName;
					if(!row.IsTrailerNumberNull()) this._trailernumber = row.TrailerNumber;
					if(!row.IsFromLocationNull()) this._fromlocation = row.FromLocation;
					if(!row.IsToLocationNull()) this._tolocation = row.ToLocation;
					if(!row.IsScheduledDepartureNull()) this._scheduleddeparture = row.ScheduledDeparture;
					if(!row.IsActualDepartureNull()) this._actualdeparture = row.ActualDeparture;
					if(!row.IsScheduledDeliveryNull()) this._scheduleddelivery = row.ScheduledDelivery;
					if(!row.IsActualDeliveryNull()) this._actualdelivery = row.ActualDelivery;
					if(!row.IsCommentsNull()) this._comments = row.Comments;
					if(!row.IsConfirmedNull()) this._confirmed = row.Confirmed;
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
		public System.String DriverName { 
			get { return this._drivername; }
			set { this._drivername = value; }
		}
		public System.String TrailerNumber { 
			get { return this._trailernumber; }
			set { this._trailernumber = value; }
		}
		public System.String FromLocation { 
			get { return this._fromlocation; }
			set { this._fromlocation = value; }
		}
		public System.String ToLocation { 
			get { return this._tolocation; }
			set { this._tolocation = value; }
		}
		public System.DateTime ScheduledDeparture { 
			get { return this._scheduleddeparture; }
			set { this._scheduleddeparture = value; }
		}
		public System.DateTime ActualDeparture { 
			get { return this._actualdeparture; }
			set { this._actualdeparture = value; }
		}
		public System.DateTime ScheduledDelivery { 
			get { return this._scheduleddelivery; }
			set { this._scheduleddelivery = value; }
		}
		public System.DateTime ActualDelivery { 
			get { return this._actualdelivery; }
			set { this._actualdelivery = value; }
		}
		public System.String Comments { 
			get { return this._comments; }
			set { this._comments = value; }
		}
		public System.Boolean Confirmed { 
			get { return this._confirmed; }
			set { this._confirmed = value; }
		}
		public override DataSet ToDataSet() { 
			//Return a dataset containing values for this object
			DispatchDS ds=null;
			try { 
				ds = new DispatchDS();
				DispatchDS.ScheduledInboundTableRow row = ds.ScheduledInboundTable.NewScheduledInboundTableRow();
				row.ID = this._id;
				row.Created = this._created;
				row.CreatedBy = this._createdby;
				row.DriverName = this._drivername;
				row.TrailerNumber = this._trailernumber;
				row.FromLocation = this._fromlocation;
				row.ToLocation = this._tolocation;
				row.ScheduledDeparture = this._scheduleddeparture;
				row.ActualDeparture = this._actualdeparture;
				row.ScheduledDelivery = this._scheduleddelivery;
				row.ActualDelivery = this._actualdelivery;
				row.Comments = this._comments;
				row.Confirmed = this._confirmed;
				ds.ScheduledInboundTable.AddScheduledInboundTableRow(row);
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
				builder.Append("DriverName=" + this._drivername.ToString() + "\t");
				builder.Append("TrailerNumber=" + this._trailernumber.ToString() + "\t");
				builder.Append("FromLocation=" + this._fromlocation.ToString() + "\t");
				builder.Append("ToLocation=" + this._tolocation.ToString() + "\t");
				builder.Append("ScheduledDeparture=" + this._scheduleddeparture.ToString() + "\t");
				builder.Append("ActualDeparture=" + this._actualdeparture.ToString() + "\t");
				builder.Append("ScheduledDelivery=" + this._scheduleddelivery.ToString() + "\t");
				builder.Append("ActualDelivery=" + this._actualdelivery.ToString() + "\t");
				builder.Append("Comments=" + this._comments.ToString() + "\t");
				builder.Append("Confirmed=" + this._confirmed.ToString() + "\t");
				builder.Append("\n");
				sThis = builder.ToString();
			}
			catch(Exception) { }
			return sThis;
		}
		#endregion
		public override string EntryType { get { return "Inbound Freight"; } }
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
					for(int i=0; i<ds.ScheduledInboundTable.Rows.Count; i++) 
						if(ds.ScheduledInboundTable[i].ID > ID) ID = ds.ScheduledInboundTable[i].ID;
					this._id = ID + 1;
				}
				DispatchDS.ScheduledInboundTableRow row = ds.ScheduledInboundTable.NewScheduledInboundTableRow();
				row.ID = this._id;
				row.Created = this._created;
				row.CreatedBy = this._createdby;
				row.DriverName = this._drivername;
				row.TrailerNumber = this._trailernumber;
				row.FromLocation = this._fromlocation;
				row.ToLocation = this._tolocation;
				if(this._scheduleddeparture != DateTime.MinValue) row.ScheduledDeparture = this._scheduleddeparture;
				if(this._actualdeparture != DateTime.MinValue) row.ActualDeparture = this._actualdeparture;
				if(this._scheduleddelivery != DateTime.MinValue) row.ScheduledDelivery = this._scheduleddelivery;
				if(this._actualdelivery != DateTime.MinValue) row.ActualDelivery = this._actualdelivery;
				row.Comments = this._comments;
				row.Confirmed = this._confirmed;
				ds.ScheduledInboundTable.AddScheduledInboundTableRow(row);
				ds.ScheduledInboundTable.AcceptChanges();
				bRet = base.mMediator.ExecuteNonQuery(base.mDataFile, new object[]{ds});
				bRet = true;
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
				DispatchDS.ScheduledInboundTableRow row = (DispatchDS.ScheduledInboundTableRow)ds.ScheduledInboundTable.Select("ID = " + this._id)[0];
				row.DriverName = this._drivername;
				row.TrailerNumber = this._trailernumber;
				row.FromLocation = this._fromlocation;
				row.ToLocation = this._tolocation;
				if(this._scheduleddeparture != DateTime.MinValue) row.ScheduledDeparture = this._scheduleddeparture;
				if(this._actualdeparture != DateTime.MinValue) row.ActualDeparture = this._actualdeparture;
				if(this._scheduleddelivery != DateTime.MinValue) row.ScheduledDelivery = this._scheduleddelivery;
				if(this._actualdelivery != DateTime.MinValue) row.ActualDelivery = this._actualdelivery;
				row.Comments = this._comments;
				row.Confirmed = this._confirmed;
				ds.ScheduledInboundTable.AcceptChanges();
				bRet = base.mMediator.ExecuteNonQuery(base.mDataFile, new object[]{ds});
				bRet = true;
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
				DispatchDS.ScheduledInboundTableRow row = (DispatchDS.ScheduledInboundTableRow)ds.ScheduledInboundTable.Select("ID = " + this._id)[0];
				row.Delete();
				ds.ScheduledInboundTable.AcceptChanges();
				bRet = base.mMediator.ExecuteNonQuery(base.mDataFile, new object[]{ds});
				bRet = true;
				base.OnEntryChanged(this, new EventArgs());
			}
			catch(Exception ex) { throw ex; }
			return bRet;
		}
//		public override ScheduleEntry Copy() {
//			//Return a copy of this object
//			InboundFreight oEntry=null;
//			try {
//				oEntry = new InboundFreight((DispatchDS.ScheduledInboundTableRow)this.ToDataSet().Tables[InboundSchedule.SCHEDULE_TABLENAME].Rows[0], this.mMediator);
//			}
//			catch(Exception ex) { throw ex; }
//			return oEntry;
//		}
	}
}
