//	File:	trailerentry.cs
//	Author:	jheary
//	Date:	11/03/05
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
	public class TrailerEntry: ScheduleEntry {
		//Members
		private System.Int32 _id=0;
		private System.DateTime _created=DateTime.Now;
		private System.String _createdby="";
		private System.String _trailernumber="";
		private System.DateTime _incomingdate=DateTime.MinValue;
		private System.String _incomingcarrier="";
		private System.String _incomingseal="";
		private System.String _incomingdrivername="";
		private System.String _initialyardlocation="";
		private System.Boolean _loadsheetready=false;
		private System.Boolean _moveinprogress=false;
		private System.DateTime _outgoingdate=DateTime.MinValue;
		private System.String _outgoingcarrier="";
		private System.String _outgoingseal="";
		private System.String _outgoingdrivername="";
		private System.Boolean _movedout=false;
		private System.String _comments="";
		private DispatchDS mTrailerMoves=null;
		
		//Constants
		
		//Events
		
		//Interface
		public TrailerEntry(Mediator mediator): this(null, mediator) { }
		public TrailerEntry(DispatchDS.TrailerLogTableRow trailerEntry, Mediator mediator): base(mediator) { 
			//Constructor
			try { 
				if(trailerEntry != null) { 
					this._id = trailerEntry.ID;
					this._created = trailerEntry.Created;
					this._createdby = trailerEntry.CreatedBy;
					if(!trailerEntry.IsTrailerNumberNull()) this._trailernumber = trailerEntry.TrailerNumber;
					if(!trailerEntry.IsIncomingDateNull()) this._incomingdate = trailerEntry.IncomingDate;
					if(!trailerEntry.IsIncomingCarrierNull()) this._incomingcarrier = trailerEntry.IncomingCarrier;
					if(!trailerEntry.IsIncomingSealNull()) this._incomingseal = trailerEntry.IncomingSeal;
					if(!trailerEntry.IsIncomingDriverNameNull()) this._incomingdrivername = trailerEntry.IncomingDriverName;
					if(!trailerEntry.IsInitialYardLocationNull()) this._initialyardlocation = trailerEntry.InitialYardLocation;
					if(!trailerEntry.IsLoadSheetReadyNull()) this._loadsheetready = trailerEntry.LoadSheetReady;
					if(!trailerEntry.IsMoveInProgressNull()) this._moveinprogress = trailerEntry.MoveInProgress;
					if(!trailerEntry.IsOutgoingDateNull()) this._outgoingdate = trailerEntry.OutgoingDate;
					if(!trailerEntry.IsOutgoingCarrierNull()) this._outgoingcarrier = trailerEntry.OutgoingCarrier;
					if(!trailerEntry.IsOutgoingSealNull()) this._outgoingseal = trailerEntry.OutgoingSeal;
					if(!trailerEntry.IsOutgoingDriverNameNull()) this._outgoingdrivername = trailerEntry.OutgoingDriverName;
					if(!trailerEntry.IsMovedOutNull()) this._movedout = trailerEntry.MovedOut;
					if(!trailerEntry.IsCommentsNull()) this._comments = trailerEntry.Comments;
					
					this.mTrailerMoves = new DispatchDS();
					this.mTrailerMoves.TrailerLogTable.ImportRow(trailerEntry);		//Enable constraints for next line
					this.mTrailerMoves.Merge(trailerEntry.GetTrailerMoveTableRows());
					this.mTrailerMoves.TrailerMoveTable.AcceptChanges();
				}
				else
					this.mTrailerMoves = new DispatchDS();
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
		public System.DateTime IncomingDate { 
			get { return this._incomingdate; }
			set { this._incomingdate = value; }
		}
		public System.String IncomingCarrier { 
			get { return this._incomingcarrier; }
			set { this._incomingcarrier = value; }
		}
		public System.String IncomingSeal { 
			get { return this._incomingseal; }
			set { this._incomingseal = value; }
		}
		public System.String IncomingDriverName { 
			get { return this._incomingdrivername; }
			set { this._incomingdrivername = value; }
		}
		public System.String InitialYardLocation { 
			get { return this._initialyardlocation; }
			set { this._initialyardlocation = value; }
		}
		public System.Boolean LoadSheetReady { 
			get { return this._loadsheetready; }
			set { this._loadsheetready = value; }
		}
		public System.Boolean MoveInProgress { 
			get { return this._moveinprogress; }
			set { this._moveinprogress = value; }
		}
		public System.DateTime OutgoingDate { 
			get { return this._outgoingdate; }
			set { this._outgoingdate = value; }
		}
		public System.String OutgoingCarrier { 
			get { return this._outgoingcarrier; }
			set { this._outgoingcarrier = value; }
		}
		public System.String OutgoingSeal { 
			get { return this._outgoingseal; }
			set { this._outgoingseal = value; }
		}
		public System.String OutgoingDriverName { 
			get { return this._outgoingdrivername; }
			set { this._outgoingdrivername = value; }
		}
		public System.Boolean MovedOut { 
			get { return this._movedout; }
			set { this._movedout = value; }
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
				DispatchDS.TrailerLogTableRow trailerEntry = ds.TrailerLogTable.NewTrailerLogTableRow();
				trailerEntry.ID = this._id;
				trailerEntry.Created = this._created;
				trailerEntry.CreatedBy = this._createdby;
				trailerEntry.TrailerNumber = this._trailernumber;
				trailerEntry.IncomingDate = this._incomingdate;
				trailerEntry.IncomingCarrier = this._incomingcarrier;
				trailerEntry.IncomingSeal = this._incomingseal;
				trailerEntry.IncomingDriverName = this._incomingdrivername;
				trailerEntry.InitialYardLocation = this._initialyardlocation;
				trailerEntry.LoadSheetReady = this._loadsheetready;
				trailerEntry.MoveInProgress = this._moveinprogress;
				trailerEntry.OutgoingDate = this._outgoingdate;
				trailerEntry.OutgoingCarrier = this._outgoingcarrier;
				trailerEntry.OutgoingSeal = this._outgoingseal;
				trailerEntry.OutgoingDriverName = this._outgoingdrivername;
				trailerEntry.MovedOut = this._movedout;
				trailerEntry.Comments = this._comments;
				ds.TrailerLogTable.AddTrailerLogTableRow(trailerEntry);
				ds.Merge(this.mTrailerMoves);
				ds.AcceptChanges();
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
				builder.Append("IncomingDate=" + this._incomingdate.ToString() + "\t");
				builder.Append("IncomingCarrier=" + this._incomingcarrier.ToString() + "\t");
				builder.Append("IncomingSeal=" + this._incomingseal.ToString() + "\t");
				builder.Append("IncomingDriverName=" + this._incomingdrivername.ToString() + "\t");
				builder.Append("InitialYardLocation=" + this._initialyardlocation.ToString() + "\t");
				builder.Append("LoadSheetReady=" + this._loadsheetready.ToString() + "\t");
				builder.Append("MoveInProgress=" + this._moveinprogress.ToString() + "\t");
				builder.Append("OutgoingDate=" + this._outgoingdate.ToString() + "\t");
				builder.Append("OutgoingCarrier=" + this._outgoingcarrier.ToString() + "\t");
				builder.Append("OutgoingSeal=" + this._outgoingseal.ToString() + "\t");
				builder.Append("OutgoingDriverName=" + this._outgoingdrivername.ToString() + "\t");
				builder.Append("MovedOut=" + this._movedout.ToString() + "\t");
				builder.Append("Comments=" + this._comments.ToString() + "\t");
				builder.Append("\n");
				sThis = builder.ToString();
			}
			catch(Exception) { }
			return sThis;
		}
		#endregion
		public DispatchDS TrailerMoves { get { return this.mTrailerMoves; } }
		public override string EntryType { get { return "Trailer Entry"; } }
		public override int EntryID { get { return this._id; } }
		public override bool Create() {
			//Save this object and all child objects
			bool bRet=false;
			try {
				DispatchDS ds = new DispatchDS();
				ds.Merge(base.mMediator.FillDataset(base.mDataFile, "", null));
				if(this._id <= 0) {
					//For new entries only; some entries are moved between schedules
					int ID = 0;
					for(int i=0; i<ds.TrailerLogTable.Rows.Count; i++) 
						if(ds.TrailerLogTable[i].ID > ID) ID = ds.TrailerLogTable[i].ID;
					this._id = ID + 1;
				}
				DispatchDS.TrailerLogTableRow row = ds.TrailerLogTable.AddTrailerLogTableRow(this._id,this._created,this._createdby,this._trailernumber,this._incomingdate,this._incomingcarrier,this._incomingseal,this._incomingdrivername,this._initialyardlocation,this._loadsheetready,this._moveinprogress,this._outgoingdate,this._outgoingcarrier,this._outgoingseal,this._outgoingdrivername,this._movedout,this._comments);
				for(int i=0; i<this.mTrailerMoves.TrailerMoveTable.Rows.Count; i++) {
					DispatchDS.TrailerMoveTableRow _row = (DispatchDS.TrailerMoveTableRow)this.mTrailerMoves.TrailerMoveTable[i];					
					DispatchDS.TrailerMoveTableRow __row = (DispatchDS.TrailerMoveTableRow)ds.TrailerMoveTable.NewTrailerMoveTableRow();
					__row.ID = _row.ID;
					__row.Requested = _row.Requested;
					__row.RequestedBy = _row.RequestedBy;
					if(!_row.IsMoveFromNull()) __row.MoveFrom = _row.MoveFrom;
					if(!_row.IsMoveToNull()) __row.MoveTo = _row.MoveTo;
					if(!_row.IsSwitcherNull()) __row.Switcher = _row.Switcher;
					if(!_row.IsLoadedWithNull()) __row.LoadedWith = _row.LoadedWith;
					if(!_row.IsScheduledTimeNull()) __row.ScheduledTime = _row.ScheduledTime;
					if(!_row.IsActualTimeNull()) __row.ActualTime = _row.ActualTime;
					__row.TrailerLogTableRow = row;
					ds.TrailerMoveTable.AddTrailerMoveTableRow(__row);
				}
				ds.TrailerMoveTable.AcceptChanges();
				ds.TrailerLogTable.AcceptChanges();
				bRet = base.mMediator.ExecuteNonQuery(base.mDataFile, new object[]{ds});
				bRet = true;
				base.OnEntryChanged(this, new EventArgs());
			}
			catch(Exception ex) { throw ex; }
			return bRet;
		}
		public override bool Update() {
			//Update this object and all child objects
			bool bRet=false;
			try {
				DispatchDS ds = new DispatchDS();
				ds.Merge(base.mMediator.FillDataset(base.mDataFile, "", null));
				DispatchDS.TrailerLogTableRow row = (DispatchDS.TrailerLogTableRow)ds.TrailerLogTable.Select("ID = " + this._id)[0];
				row.Created = this._created;
				row.CreatedBy = this._createdby;
				row.TrailerNumber = this._trailernumber;
				row.IncomingDate = this._incomingdate;
				row.IncomingCarrier = this._incomingcarrier;
				row.IncomingSeal = this._incomingseal;
				row.IncomingDriverName = this._incomingdrivername;
				row.InitialYardLocation = this._initialyardlocation;
				row.LoadSheetReady = this._loadsheetready;
				row.MoveInProgress = this._moveinprogress;
				row.OutgoingDate = this._outgoingdate;
				row.OutgoingCarrier = this._outgoingcarrier;
				row.OutgoingSeal = this._outgoingseal;
				row.OutgoingDriverName = this._outgoingdrivername;
				row.MovedOut = this._movedout;
				row.Comments = this._comments;
				for(int i=0; i<this.mTrailerMoves.TrailerMoveTable.Rows.Count; i++) {
					DispatchDS.TrailerMoveTableRow _row = (DispatchDS.TrailerMoveTableRow)this.mTrailerMoves.TrailerMoveTable[i];
					if(_row.RowState == DataRowState.Added) {
						DispatchDS.TrailerMoveTableRow __row = (DispatchDS.TrailerMoveTableRow)ds.TrailerMoveTable.NewTrailerMoveTableRow();
						__row.ID = _row.ID;
						__row.Requested = _row.Requested;
						__row.RequestedBy = _row.RequestedBy;
						if(!_row.IsMoveFromNull()) __row.MoveFrom = _row.MoveFrom;
						if(!_row.IsMoveToNull()) __row.MoveTo = _row.MoveTo;
						if(!_row.IsSwitcherNull()) __row.Switcher = _row.Switcher;
						if(!_row.IsLoadedWithNull()) __row.LoadedWith = _row.LoadedWith;
						if(!_row.IsScheduledTimeNull()) __row.ScheduledTime = _row.ScheduledTime;
						if(!_row.IsActualTimeNull()) __row.ActualTime = _row.ActualTime;
						__row.TrailerLogTableRow = row;
						ds.TrailerMoveTable.AddTrailerMoveTableRow(__row);
					}
					else if(_row.RowState == DataRowState.Modified) {
						DispatchDS.TrailerMoveTableRow __row = (DispatchDS.TrailerMoveTableRow)ds.TrailerMoveTable.Select("ID = " + _row.ID)[0];
						if(!_row.IsMoveFromNull()) __row.MoveFrom = _row.MoveFrom;
						if(!_row.IsMoveToNull()) __row.MoveTo = _row.MoveTo;
						if(!_row.IsSwitcherNull()) __row.Switcher = _row.Switcher;
						if(!_row.IsLoadedWithNull()) __row.LoadedWith = _row.LoadedWith;
						if(!_row.IsScheduledTimeNull()) __row.ScheduledTime = _row.ScheduledTime;
						if(!_row.IsActualTimeNull()) __row.ActualTime = _row.ActualTime;
					}
					else if(_row.RowState == DataRowState.Deleted) {
						DispatchDS.TrailerMoveTableRow __row = (DispatchDS.TrailerMoveTableRow)ds.TrailerMoveTable.Select("ID = " + _row.ID)[0];
						__row.Delete();
					}
				}
				ds.TrailerMoveTable.AcceptChanges();
				ds.TrailerLogTable.AcceptChanges();
				bRet = base.mMediator.ExecuteNonQuery(base.mDataFile, new object[]{ds});
				bRet = true;
				base.OnEntryChanged(this, new EventArgs());
			}
			catch(Exception ex) { throw ex; }
			return bRet;
		}
		public override bool Delete() {
			//Delete this object and all child objects
			bool bRet=false;
			try {
				DispatchDS ds = new DispatchDS();
				ds.Merge(base.mMediator.FillDataset(base.mDataFile, "", null));
				DispatchDS.TrailerLogTableRow row = (DispatchDS.TrailerLogTableRow)ds.TrailerLogTable.Select("ID = " + this._id)[0];
				row.Delete();
				for(int i=0; i<this.mTrailerMoves.TrailerMoveTable.Rows.Count; i++) {
					DispatchDS.TrailerMoveTableRow _row = (DispatchDS.TrailerMoveTableRow)ds.TrailerMoveTable.Select("ID = " + this.mTrailerMoves.TrailerMoveTable[i].ID)[0];
					_row.Delete();
				}
				ds.TrailerMoveTable.AcceptChanges();
				ds.TrailerLogTable.AcceptChanges();
				bRet = base.mMediator.ExecuteNonQuery(base.mDataFile, new object[]{ds});
				bRet = true;
				base.OnEntryChanged(this, new EventArgs());
			}
			catch(Exception ex) { throw ex; }
			return bRet;
		}
//		public override ScheduleEntry Copy() {
//			//Return a copy of this object
//			TrailerEntry oEntry=null;
//			try {
//				oEntry = new TrailerEntry((DispatchDS.TrailerLogTableRow)this.ToDataSet().Tables[TrailerLog.SCHEDULE_TABLENAME].Rows[0], this.mMediator);
//			}
//			catch(Exception ex) { throw ex; }
//			return oEntry;
//		}
	}
}
