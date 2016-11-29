//	File:	pickuplog.cs
//	Author:	J. Heary
//	Date:	09/20/05
//	Desc:	
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Argix.Data;

namespace Argix.Dispatch {
	//
	public class TrailerLog: DispatchSchedule {
		//Members
		//Constants
		//Events
		//Interface
		public TrailerLog(string scheduleName, string tableName, string dataFile, Mediator mediator): base(scheduleName,tableName,dataFile,mediator) { }
		#region Accessors\Modifiers, ToString(), ToDataSet()
		#endregion
		public override dlgSchedule ScheduleDialog(ScheduleEntry entry) { return new dlgTrailerEntry((TrailerEntry)entry,base.mMediator); }
		public override bool Add(ScheduleEntry entry) {
			//Add a new entry
			bool bRet=false;
			try { 
				//Ensure correct data file
				entry.DataFile = base.mDataFile;
				bRet = entry.Create();
			}
			catch(Exception ex) { throw ex; }
			return bRet;
		}
		public override int Count { get { return this.mSchedule.TrailerLogTable.Rows.Count; } }
		public override ScheduleEntry Item() {
			//Return a new blank entry object
			TrailerEntry entry=null;
			try { 
				entry = new TrailerEntry(this.mMediator);
				entry.DataFile = base.mDataFile;
				entry.EntryChanged += new EventHandler(OnEntryChanged);
			}
			catch (Exception ex) { throw ex; }
			return entry;
		}
		public override ScheduleEntry Item(int ID) {
			//Return an existing entry object from the entry log
			TrailerEntry entry=null;
			try { 
				//Merge from collection (dataset)
				if(ID > 0) {
					DataRow[] rows = this.mSchedule.TrailerLogTable.Select("ID=" + ID);
					if(rows.Length == 0)
						throw new ApplicationException("Entry  " + ID + " does not exist in this schedule!\n");
					DispatchDS.TrailerLogTableRow row = (DispatchDS.TrailerLogTableRow)rows[0];
					entry = new TrailerEntry(row, this.mMediator);
					entry.DataFile = base.mDataFile;
					entry.EntryChanged +=new EventHandler(OnEntryChanged);
				}
				else 
					entry = (TrailerEntry)Item();
			}
			catch (Exception ex) { throw ex; }
			return entry;
		}
		public override bool Remove(ScheduleEntry entry) {
			//Remove the specified entry
			bool bRet=false;
			try { 
				//Ensure correct data file
				entry.DataFile = base.mDataFile;
				bRet = entry.Delete();
			}
			catch(Exception ex) { throw ex; }
			return bRet;
		}
		public override void AddList(DispatchDS list) {
			//Add a list of entries to this schedule
			try {
				for(int i=0; i<list.TrailerLogTable.Rows.Count; i++) {
					DispatchDS.TrailerLogTableRow row = list.TrailerLogTable[i];
					TrailerEntry entry = (TrailerEntry)Item();
					entry.ID = row.ID;
					entry.Created = row.Created;
					entry.CreatedBy = row.CreatedBy;
					if(!row.IsTrailerNumberNull()) entry.TrailerNumber = row.TrailerNumber;
					if(!row.IsIncomingDateNull()) entry.IncomingDate = row.IncomingDate;
					if(!row.IsIncomingCarrierNull()) entry.IncomingCarrier = row.IncomingCarrier;
					if(!row.IsIncomingSealNull()) entry.IncomingSeal = row.IncomingSeal;
					if(!row.IsIncomingDriverNameNull()) entry.IncomingDriverName = row.IncomingDriverName;
					if(!row.IsInitialYardLocationNull()) entry.InitialYardLocation = row.InitialYardLocation;
					if(!row.IsLoadSheetReadyNull()) entry.LoadSheetReady = row.LoadSheetReady;
					if(!row.IsMoveInProgressNull()) entry.MoveInProgress = row.MoveInProgress;
					if(!row.IsOutgoingDateNull()) entry.OutgoingDate = row.OutgoingDate;
					if(!row.IsOutgoingCarrierNull()) entry.OutgoingCarrier = row.OutgoingCarrier;
					if(!row.IsOutgoingSealNull()) entry.OutgoingSeal = row.OutgoingSeal;
					if(!row.IsOutgoingDriverNameNull()) entry.OutgoingDriverName = row.OutgoingDriverName;
					if(!row.IsMovedOutNull()) entry.MovedOut = row.MovedOut;
					if(!row.IsCommentsNull()) entry.Comments = row.Comments;
					entry.TrailerMoves.TrailerLogTable.ImportRow(row);		//Enable constraints for next line
					entry.TrailerMoves.Merge(row.GetTrailerMoveTableRows());
					entry.TrailerMoves.TrailerMoveTable.AcceptChanges();
					Add(entry);
				}
			}
			catch(Exception ex) { throw ex; }
		}
		public override void RemoveList(DispatchDS list) {
			//Remove a list of entries from this schedule
			try {
				for(int i=0; i<list.TrailerLogTable.Rows.Count; i++) {
					DispatchDS.TrailerLogTableRow row = list.TrailerLogTable[i];
					TrailerEntry entry = (TrailerEntry)Item(row.ID);
					entry.Delete();
				}
			}
			catch(Exception ex) { throw ex; }
		}
	}
}