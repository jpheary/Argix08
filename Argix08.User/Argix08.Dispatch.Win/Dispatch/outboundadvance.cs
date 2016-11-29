//	File:	outboundadvance.cs
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
	public class OutboundAdvance: DispatchSchedule {
		//Members
		//Constants
		//Events
		//Interface
		public OutboundAdvance(Mediator mediator): base("Advance Outbound","ScheduledOutboundTable","_outboundadvance",mediator) { }
		#region Accessors\Modifiers, ToString(), ToDataSet()
		#endregion
		public override dlgSchedule ScheduleDialog(ScheduleEntry entry) { return new dlgOutboundFreight((OutboundFreight)entry,base.mMediator); }
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
		public override int Count { get { return this.mSchedule.ScheduledOutboundTable.Rows.Count; } }
		public override ScheduleEntry Item() {
			//Return a new blank entry object
			OutboundFreight entry=null;
			try { 
				entry = new OutboundFreight(this.mMediator);
				entry.DataFile = base.mDataFile;
				entry.EntryChanged += new EventHandler(OnEntryChanged);
			}
			catch (Exception ex) { throw ex; }
			return entry;
		}
		public override ScheduleEntry Item(int ID) {
			//Return an existing entry object from the entry schedule
			OutboundFreight entry=null;
			try { 
				//Merge from collection (dataset)
				if(ID > 0) {
					DataRow[] rows = this.mSchedule.ScheduledOutboundTable.Select("ID=" + ID);
					if(rows.Length == 0)
						throw new ApplicationException("Entry  " + ID + " does not exist in this schedule!\n");
					DispatchDS.ScheduledOutboundTableRow row = (DispatchDS.ScheduledOutboundTableRow)rows[0];
					entry = new OutboundFreight(row, this.mMediator);
					entry.DataFile = base.mDataFile;
					entry.EntryChanged +=new EventHandler(OnEntryChanged);
				}
				else 
					entry = (OutboundFreight)Item();
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
				for(int i=0; i<list.ScheduledOutboundTable.Rows.Count; i++) {
					DispatchDS.ScheduledOutboundTableRow row = list.ScheduledOutboundTable[i];
					OutboundFreight entry = (OutboundFreight)Item();
					entry.ID = row.ID;
					entry.Created = row.Created;
					entry.CreatedBy = row.CreatedBy;
					if(!row.IsDriverNameNull()) entry.DriverName = row.DriverName;
					if(!row.IsTrailerNumberNull()) entry.TrailerNumber = row.TrailerNumber;
					if(!row.IsFromLocationNull()) entry.FromLocation = row.FromLocation;
					if(!row.IsToLocationNull()) entry.ToLocation = row.ToLocation;
					if(!row.IsScheduledDepartureNull()) entry.ScheduledDeparture = row.ScheduledDeparture;
					if(!row.IsActualDepartureNull()) entry.ActualDeparture = row.ActualDeparture;
					if(!row.IsScheduledDeliveryNull()) entry.ScheduledDelivery = row.ScheduledDelivery;
					if(!row.IsActualDeliveryNull()) entry.ActualDelivery = row.ActualDelivery;
					if(!row.IsCommentsNull()) entry.Comments = row.Comments;
					if(!row.IsConfirmedNull()) entry.Confirmed = row.Confirmed;
					Add(entry);
				}
			}
			catch(Exception ex) { throw ex; }
		}
		public override void RemoveList(DispatchDS list) {
			//Remove a list of entries from this schedule
			try {
				for(int i=0; i<list.ScheduledOutboundTable.Rows.Count; i++) {
					DispatchDS.ScheduledOutboundTableRow row = list.ScheduledOutboundTable[i];
					OutboundFreight entry = (OutboundFreight)Item(row.ID);
					entry.Delete();
				}
			}
			catch(Exception ex) { throw ex; }
		}
	}
}