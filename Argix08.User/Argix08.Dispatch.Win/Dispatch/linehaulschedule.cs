//	File:	linehaulschedule.cs
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
	public class LineHaulSchedule: DispatchSchedule {
		//Members
		//Constants		
		//Events
		//Interface
		public LineHaulSchedule(string scheduleName, string tableName, string dataFile, Mediator mediator): base(scheduleName,tableName,dataFile,mediator) { }
		#region Accessors\Modifiers, ToString(), ToDataSet()
		#endregion
		public override dlgSchedule ScheduleDialog(ScheduleEntry entry) { return new dlgArrival((Arrival)entry,base.mMediator); }
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
		public override int Count { get { return this.mSchedule.LineHaulTable.Rows.Count; } }
		public override ScheduleEntry Item() {
			//Return a new blank entry object
			Arrival entry=null;
			try { 
				entry = new Arrival(this.mMediator);
				entry.DataFile = base.mDataFile;
				entry.EntryChanged += new EventHandler(OnEntryChanged);
			}
			catch (Exception ex) { throw ex; }
			return entry;
		}
		public override ScheduleEntry Item(int ID) {
			//Return an existing entry object from the entry schedule
			Arrival entry=null;
			try { 
				//Merge from collection (dataset)
				if(ID > 0) {
					DataRow[] rows = this.mSchedule.LineHaulTable.Select("ID=" + ID);
					if(rows.Length == 0)
						throw new ApplicationException("Entry  " + ID + " does not exist in this schedule!\n");
					DispatchDS.LineHaulTableRow row = (DispatchDS.LineHaulTableRow)rows[0];
					entry = new Arrival(row, this.mMediator);
					entry.DataFile = base.mDataFile;
					entry.EntryChanged +=new EventHandler(OnEntryChanged);
				}
				else 
					entry = (Arrival)Item();
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
			//
			try {
				for(int i=0; i<list.LineHaulTable.Rows.Count; i++) {
					DispatchDS.LineHaulTableRow row = list.LineHaulTable[i];
					Arrival entry = (Arrival)Item();
					entry.ID = row.ID;
					entry.Created = row.Created;
					entry.CreatedBy = row.CreatedBy;
					if(!row.IsTrailerNumberNull()) entry.TrailerNumber = row.TrailerNumber;
					if(!row.IsTerminalNull()) entry.Terminal = row.Terminal;
					if(!row.IsScheduledArrivalNull()) entry.ScheduledArrival = row.ScheduledArrival;
					if(!row.IsActualArrivalNull()) entry.ActualArrival = row.ActualArrival;
					if(!row.IsCommentsNull()) entry.Comments = row.Comments;
					Add(entry);
				}
			}
			catch(Exception ex) { throw ex; }
		}
		public override void RemoveList(DispatchDS list) {
			//Remove a list of entries from this schedule
			try {
				for(int i=0; i<list.LineHaulTable.Rows.Count; i++) {
					DispatchDS.LineHaulTableRow row = list.LineHaulTable[i];
					Arrival entry = (Arrival)Item(row.ID);
					entry.Delete();
				}
			}
			catch(Exception ex) { throw ex; }
		}
	}
}