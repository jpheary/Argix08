//	File:	arrival.cs
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
	public class Arrival: ScheduleEntry {
		//Members
		private System.Int32 _id=0;
		private System.DateTime _created=DateTime.Now;
		private System.String _createdby=Environment.UserName;
		private System.String _trailernumber="";
		private System.String _terminal="";
		private System.DateTime _scheduledarrival=DateTime.Today;
		private System.DateTime _actualarrival=DateTime.MinValue;
		private System.String _comments="";
		
		//Constants
		
		//Events
		
		//Interface
		public Arrival(Mediator mediator): this(null, mediator) { }
		public Arrival(DispatchDS.LineHaulTableRow row, Mediator mediator): base(mediator) { 
			//Constructor
			try { 
				if(row != null) { 
					this._id = row.ID;
					this._created = row.Created;
					this._createdby = row.CreatedBy;
					if(!row.IsTrailerNumberNull()) this._trailernumber = row.TrailerNumber;
					if(!row.IsTerminalNull()) this._terminal = row.Terminal;
					if(!row.IsScheduledArrivalNull()) this._scheduledarrival = row.ScheduledArrival;
					if(!row.IsActualArrivalNull()) this._actualarrival = row.ActualArrival;
					if(!row.IsCommentsNull()) this._comments = row.Comments;
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
		public System.String TrailerNumber { 
			get { return this._trailernumber; }
			set { this._trailernumber = value; }
		}
		public System.String Terminal { 
			get { return this._terminal; }
			set { this._terminal = value; }
		}
		public System.DateTime ScheduledArrival { 
			get { return this._scheduledarrival; }
			set { this._scheduledarrival = value; }
		}
		public System.DateTime ActualArrival { 
			get { return this._actualarrival; }
			set { this._actualarrival = value; }
		}
		public System.String Comments { 
			get { return this._comments; }
			set { this._comments = value; }
		}
		public override DataSet ToDataSet() { 
			//Return a dataset containing values for this object
			DispatchDS ds=null;
			try { 
				ds = new DispatchDS();
				DispatchDS.LineHaulTableRow row = ds.LineHaulTable.NewLineHaulTableRow();
				row.ID = this._id;
				row.Created = this._created;
				row.CreatedBy = this._createdby;
				row.TrailerNumber = this._trailernumber;
				row.Terminal = this._terminal;
				row.ScheduledArrival = this._scheduledarrival;
				row.ActualArrival = this._actualarrival;
				row.Comments = this._comments;
				ds.LineHaulTable.AddLineHaulTableRow(row);
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
				builder.Append("TrailerNumber=" + this._trailernumber.ToString() + "\t");
				builder.Append("Terminal=" + this._terminal.ToString() + "\t");
				builder.Append("ScheduledArrival=" + this._scheduledarrival.ToString() + "\t");
				builder.Append("ActualArrival=" + this._actualarrival.ToString() + "\t");
				builder.Append("Comments=" + this._comments.ToString() + "\t");
				builder.Append("\n");
				sThis = builder.ToString();
			}
			catch(Exception) { }
			return sThis;
		}
		#endregion
		public override string EntryType { get { return "Arrival"; } }
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
					for(int i=0; i<ds.LineHaulTable.Rows.Count; i++) 
						if(ds.LineHaulTable[i].ID > ID) ID = ds.LineHaulTable[i].ID;
					this._id = ID + 1;
				}
				ds.LineHaulTable.AddLineHaulTableRow(this._id,this._created,this._createdby,this._trailernumber,this._terminal,this._scheduledarrival,this._actualarrival,this._comments);
				ds.LineHaulTable.AcceptChanges();
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
				DispatchDS.LineHaulTableRow row = (DispatchDS.LineHaulTableRow)ds.LineHaulTable.Select("ID = " + this._id)[0];
				row.TrailerNumber = this._trailernumber;
				row.Terminal = this._terminal;
				row.ScheduledArrival = this._scheduledarrival;
				row.ActualArrival = this._actualarrival;
				row.Comments = this._comments;
				ds.LineHaulTable.AcceptChanges();
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
				DispatchDS.LineHaulTableRow row = (DispatchDS.LineHaulTableRow)ds.LineHaulTable.Select("ID = " + this._id)[0];
				row.Delete();
				ds.LineHaulTable.AcceptChanges();
				bRet = base.mMediator.ExecuteNonQuery(base.mDataFile, new object[]{ds});
				bRet = true;
				base.OnEntryChanged(this, new EventArgs());
			}
			catch(Exception ex) { throw ex; }
			return bRet;
		}
//		public override ScheduleEntry Copy() {
//			//Return a copy of this object
//			Arrival oEntry=null;
//			try {
//				oEntry = new Arrival((DispatchDS.LineHaulTableRow)this.ToDataSet().Tables[LineHaulSchedule.SCHEDULE_TABLENAME].Rows[0], this.mMediator);
//			}
//			catch(Exception ex) { throw ex; }
//			return oEntry;
//		}
	}
}
