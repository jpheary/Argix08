//	File:	clientinboundarchive.cs
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
	public class ClientInboundArchive: DispatchSchedule {
		//Members
		//Constants
		//Events
		//Interface
		public ClientInboundArchive(Mediator mediator): base("Archive of Inbound Sheet","ClientInboundTable","_clientinboundarchive",mediator) { }
		#region Accessors\Modifiers, ToString(), ToDataSet()
		#endregion
		public override dlgSchedule ScheduleDialog(ScheduleEntry entry) { return new dlgClientInboundFreight((ClientInboundFreight)entry,base.mMediator); }
		public override bool Add(ScheduleEntry entry) {
			//Add a new entry
			bool bRet=false;
			try { 
				//Ensure correct data file
				entry.DataFile = base.mDataFile;
				bRet = entry.Create();
				base.OnScheduleChanged(this, new EventArgs());
			}
			catch(Exception ex) { throw ex; }
			return bRet;
		}
		public override int Count { get { return this.mSchedule.ClientInboundTable.Rows.Count; } }
		public override ScheduleEntry Item() {
			//Return a new blank entry object
			ClientInboundFreight entry=null;
			try { 
				entry = new ClientInboundFreight(base.mMediator);
				entry.DataFile = base.mDataFile;
				entry.EntryChanged += new EventHandler(OnEntryChanged);
			}
			catch (Exception ex) { throw ex; }
			return entry;
		}
		public override ScheduleEntry Item(int ID) {
			//Return an existing entry object from the entry schedule
			ClientInboundFreight entry=null;
			try { 
				//Merge from collection (dataset)
				if(ID > 0) {
					DataRow[] rows = this.mSchedule.ClientInboundTable.Select("ID=" + ID);
					if(rows.Length == 0)
						throw new ApplicationException("Entry  " + ID + " does not exist in this schedule!\n");
					DispatchDS.ClientInboundTableRow row = (DispatchDS.ClientInboundTableRow)rows[0];
					entry = new ClientInboundFreight(row, this.mMediator);
					entry.DataFile = base.mDataFile;
					entry.EntryChanged +=new EventHandler(OnEntryChanged);
				}
				else 
					entry = (ClientInboundFreight)Item();
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
				base.OnScheduleChanged(this, new EventArgs());
			}
			catch(Exception ex) { throw ex; }
			return bRet;
		}
		public override void AddList(DispatchDS list) {
			//
			try {
				for(int i=0; i<list.ClientInboundTable.Rows.Count; i++) {
					DispatchDS.ClientInboundTableRow row = list.ClientInboundTable[i];
					ClientInboundFreight entry = (ClientInboundFreight)Item();
					entry.ID = row.ID;
					entry.Created = row.Created;
					entry.CreatedBy = row.CreatedBy;
					if(!row.IsVendorNameNull()) entry.VendorName = row.VendorName;
					if(!row.IsConsigneeNameNull()) entry.ConsigneeName = row.ConsigneeName;
					if(!row.IsETATimeNull()) entry.ETATime = row.ETATime;
					if(!row.IsDriverNameNull()) entry.DriverName = row.DriverName;
					if(!row.IsTrailerNumberNull()) entry.TrailerNumber = row.TrailerNumber;
					if(!row.IsAmountNull()) entry.Amount = row.Amount;
					if(!row.IsAmountTypeNull()) entry.AmountType = row.AmountType;
					if(!row.IsFreightTypeNull()) entry.FreightType = row.FreightType;
					if(!row.IsCommentsNull()) entry.Comments = row.Comments;
					if(!row.IsInNull()) entry.In = row.In;
					Add(entry);
				}
			}
			catch(Exception ex) { throw ex; }
		}
		public override void RemoveList(DispatchDS list) {
			//Remove a list of entries from this schedule
			try {
				for(int i=0; i<list.ClientInboundTable.Rows.Count; i++) {
					DispatchDS.ClientInboundTableRow row = list.ClientInboundTable[i];
					ClientInboundFreight entry = (ClientInboundFreight)Item(row.ID);
					entry.Delete();
				}
			}
			catch(Exception ex) { throw ex; }
		}
	}
}