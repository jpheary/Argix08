//	File:	clientinboundfreight.cs
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
	public class ClientInboundFreight: ScheduleEntry {
		//Members
		private System.Int32 _id=0;
		private System.DateTime _created=DateTime.Now;
		private System.String _createdby=Environment.UserName;
		private System.String _vendorname="";
		private System.String _consigneename="";
		private System.DateTime _etatime=DateTime.MinValue;
		private System.String _drivername="";
		private System.String _trailernumber="";
		private System.Int32 _amount=0;
		private System.String _amounttype="";
		private System.String _freighttype="";
		private System.String _comments="";
		private System.Boolean _in=false;
		
		//Constants
		
		//Events
		
		//Interface
		public ClientInboundFreight(Mediator mediator): this(null, mediator) { }
		public ClientInboundFreight(DispatchDS.ClientInboundTableRow row, Mediator mediator): base(mediator) { 
			//Constructor
			try { 
				if(row != null) { 
					this._id = row.ID;
					this._created = row.Created;
					this._createdby = row.CreatedBy;
					if(!row.IsVendorNameNull()) this._vendorname = row.VendorName;
					if(!row.IsConsigneeNameNull()) this._consigneename = row.ConsigneeName;
					if(!row.IsETATimeNull()) this._etatime = row.ETATime;
					if(!row.IsDriverNameNull()) this._drivername = row.DriverName;
					if(!row.IsTrailerNumberNull()) this._trailernumber = row.TrailerNumber;
					if(!row.IsAmountNull()) this._amount = row.Amount;
					if(!row.IsAmountTypeNull()) this._amounttype = row.AmountType;
					if(!row.IsFreightTypeNull()) this._freighttype = row.FreightType;
					if(!row.IsCommentsNull()) this._comments = row.Comments;
					if(!row.IsInNull()) this._in = row.In;
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
		public System.String VendorName { 
			get { return this._vendorname; }
			set { this._vendorname = value; }
		}
		public System.String ConsigneeName { 
			get { return this._consigneename; }
			set { this._consigneename = value; }
		}
		public System.DateTime ETATime { 
			get { return this._etatime; }
			set { this._etatime = value; }
		}
		public System.String DriverName { 
			get { return this._drivername; }
			set { this._drivername = value; }
		}
		public System.String TrailerNumber { 
			get { return this._trailernumber; }
			set { this._trailernumber = value; }
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
		public System.String Comments { 
			get { return this._comments; }
			set { this._comments = value; }
		}
		public System.Boolean In { 
			get { return this._in; }
			set { this._in = value; }
		}
		public override DataSet ToDataSet() { 
			//Return a dataset containing values for this object
			DispatchDS ds=null;
			try { 
				ds = new DispatchDS();
				DispatchDS.ClientInboundTableRow row = ds.ClientInboundTable.NewClientInboundTableRow();
				row.ID = this._id;
				row.Created = this._created;
				row.CreatedBy = this._createdby;
				row.VendorName = this._vendorname;
				row.ConsigneeName = this._consigneename;
				row.ETATime = this._etatime;
				row.DriverName = this._drivername;
				row.TrailerNumber = this._trailernumber;
				row.Amount = this._amount;
				row.AmountType = this._amounttype;
				row.FreightType = this._freighttype;
				row.Comments = this._comments;
				row.In = this._in;
				ds.ClientInboundTable.AddClientInboundTableRow(row);
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
				builder.Append("VendorName=" + this._vendorname.ToString() + "\t");
				builder.Append("ConsigneeName=" + this._consigneename.ToString() + "\t");
				builder.Append("ETATime=" + this._etatime.ToString() + "\t");
				builder.Append("DriverName=" + this._drivername.ToString() + "\t");
				builder.Append("TrailerNumber=" + this._trailernumber.ToString() + "\t");
				builder.Append("Amount=" + this._amount.ToString() + "\t");
				builder.Append("AmountType=" + this._amounttype.ToString() + "\t");
				builder.Append("FreightType=" + this._freighttype.ToString() + "\t");
				builder.Append("Comments=" + this._comments.ToString() + "\t");
				builder.Append("In=" + this._in.ToString() + "\t");
				builder.Append("\n");
				sThis = builder.ToString();
			}
			catch(Exception) { }
			return sThis;
		}
		#endregion
		public override string EntryType { get { return "Client Inbound Freight"; } }
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
					for(int i=0; i<ds.ClientInboundTable.Rows.Count; i++) 
						if(ds.ClientInboundTable[i].ID > ID) ID = ds.ClientInboundTable[i].ID;
					this._id = ID + 1;
				}
				DispatchDS.ClientInboundTableRow row = ds.ClientInboundTable.NewClientInboundTableRow();
				row.ID = this._id;
				row.Created = this._created;
				row.CreatedBy = this._createdby;
				row.VendorName = this._vendorname;
				row.ConsigneeName = this._consigneename;
				if(this._etatime != DateTime.MinValue) row.ETATime = this._etatime;
				row.DriverName = this._drivername;
				row.TrailerNumber = this._trailernumber;
				row.Amount = this._amount;
				row.AmountType = this._amounttype;
				row.FreightType = this._freighttype;
				row.Comments = this._comments;
				row.In = this._in;
				ds.ClientInboundTable.AddClientInboundTableRow(row);
				ds.ClientInboundTable.AcceptChanges();
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
				DispatchDS.ClientInboundTableRow row = (DispatchDS.ClientInboundTableRow)ds.ClientInboundTable.Select("ID = " + this._id)[0];
				row.VendorName = this._vendorname;
				row.ConsigneeName = this._consigneename;
				if(this._etatime != DateTime.MinValue) row.ETATime = this._etatime;
				row.DriverName = this._drivername;
				row.TrailerNumber = this._trailernumber;
				row.Amount = this._amount;
				row.AmountType = this._amounttype;
				row.FreightType = this._freighttype;
				row.Comments = this._comments;
				row.In = this._in;
				ds.ClientInboundTable.AcceptChanges();
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
				DispatchDS.ClientInboundTableRow row = (DispatchDS.ClientInboundTableRow)ds.ClientInboundTable.Select("ID = " + this._id)[0];
				row.Delete();
				ds.ClientInboundTable.AcceptChanges();
				bRet = base.mMediator.ExecuteNonQuery(base.mDataFile, new object[]{ds});
				bRet = true;
				base.OnEntryChanged(this, new EventArgs());
			}
			catch(Exception ex) { throw ex; }
			return bRet;
		}
//		public override ScheduleEntry Copy() {
//			//Return a copy of this object
//			ClientInboundFreight oEntry=null;
//			try {
//				oEntry = new ClientInboundFreight((DispatchDS.ClientInboundTableRow)this.ToDataSet().Tables[ClientInboundSchedule.SCHEDULE_TABLENAME].Rows[0], this.mMediator);
//			}
//			catch(Exception ex) { throw ex; }
//			return oEntry;
//		}
	}
}
