//	File:	pickuparchive.cs
//	Author:	J. Heary
//	Date:	10/10/05
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
	public class PickupArchive: DispatchSchedule {
		//Members
		//Constants
		//Events
		//Interface
		public PickupArchive(Mediator mediator): base("Pickup Log Archive","PickupLogTable","_pickuparchive",mediator) { }
		#region Accessors\Modifiers, ToString(), ToDataSet()
		#endregion
		public override dlgSchedule ScheduleDialog(ScheduleEntry entry) { return new dlgPickup((Pickup)entry,base.mMediator); }
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
		public override int Count { get { return this.mSchedule.PickupLogTable.Rows.Count; } }
		public override ScheduleEntry Item() {
			//Return a new blank entry object
			Pickup entry=null;
			try { 
				entry = new Pickup(this.mMediator);
				entry.DataFile = base.mDataFile;
				entry.EntryChanged += new EventHandler(OnEntryChanged);
			}
			catch (Exception ex) { throw ex; }
			return entry;
		}
		public override ScheduleEntry Item(int ID) {
			//Return an existing entry object from the entry log
			Pickup entry=null;
			try { 
				//Merge from collection (dataset)
				if(ID > 0) {
					DataRow[] rows = this.mSchedule.PickupLogTable.Select("ID=" + ID);
					if(rows.Length == 0)
						throw new ApplicationException("Entry  " + ID + " does not exist in this schedule!\n");
					DispatchDS.PickupLogTableRow row = (DispatchDS.PickupLogTableRow)rows[0];
					entry = new Pickup(row, this.mMediator);
					entry.DataFile = base.mDataFile;
					entry.EntryChanged +=new EventHandler(OnEntryChanged);
				}
				else 
					entry = (Pickup)Item();
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
				for(int i=0; i<list.PickupLogTable.Rows.Count; i++) {
					DispatchDS.PickupLogTableRow row = list.PickupLogTable[i];
					Pickup entry = (Pickup)Item();
					entry.ID = row.ID;
					entry.Created = row.Created;
					entry.CreatedBy = row.CreatedBy;
					if(!row.IsCallerNameNull()) entry.CallerName = row.CallerName;
					if(!row.IsClientNameNull()) entry.ClientName = row.ClientName;
					if(!row.IsShipperNameNull()) entry.ShipperName = row.ShipperName;
					if(!row.IsShipperAddressNull()) entry.ShipperAddress = row.ShipperAddress;
					if(!row.IsDeliveryWindowNull()) entry.DeliveryWindow = row.DeliveryWindow;
					if(!row.IsTerminalNull()) entry.Terminal = row.Terminal;
					if(!row.IsDriverNameNull()) entry.DriverName = row.DriverName;
					if(!row.IsAmountNull()) entry.Amount = row.Amount;
					if(!row.IsAmountTypeNull()) entry.AmountType = row.AmountType;
					if(!row.IsFreightTypeNull()) entry.FreightType = row.FreightType;
					if(!row.IsAutoNumberNull()) entry.AutoNumber = row.AutoNumber;
					if(!row.IsRequestDateNull()) entry.RequestDate = row.RequestDate;
					if(!row.IsPickUpDateNull()) entry.PickUpDate = row.PickUpDate;
					if(!row.IsCommentsNull()) entry.Comments = row.Comments;
					if(!row.IsUpdatedNull()) entry.Updated = row.Updated;
					if(!row.IsMustBeReadyNull()) entry.MustBeReady = row.MustBeReady;
					if(!row.IsPickedUpNull()) entry.PickedUp = row.PickedUp;
					Add(entry);
				}
			}
			catch(Exception ex) { throw ex; }
		}
		public override void RemoveList(DispatchDS list) {
			//Remove a list of entries from this schedule
			try {
				for(int i=0; i<list.PickupLogTable.Rows.Count; i++) {
					DispatchDS.PickupLogTableRow row = list.PickupLogTable[i];
					Pickup entry = (Pickup)Item(row.ID);
					entry.Delete();
				}
			}
			catch(Exception ex) { throw ex; }
		}
	}
}