//	File:	schedule.cs
//	Author:	J. Heary
//	Date:	09/28/05
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
	public abstract class DispatchSchedule {
		//Members
		protected string mScheduleName="";
		protected string mTableName="";
		protected string mDataFile="";
		protected Mediator mMediator=null;
		protected DispatchDS mSchedule=null;
		
		//Constants
		
		//Events
		public event EventHandler ScheduleChanged=null;
		
		//Interface
		public DispatchSchedule(string scheduleName, string tableName, string dataFile, Mediator mediator) { 
			//Constructor
			try {
				//Set custom attributes
				this.mScheduleName = scheduleName;
				this.mTableName = tableName;
				this.mDataFile = dataFile;
				this.mMediator = mediator;
				this.mSchedule = new DispatchDS();
			}
			catch(Exception ex) { throw ex; }
		}
		#region Accessors\Modifiers, ToString(), ToDataSet()
		public DataSet ToDataSet() {
			//Return a dataset containing values for this object
			return this.mSchedule;
		}
		public override string ToString() {
			//Custom ToString() method
			string sThis=base.ToString();
			try {
				//Form string detail of this object
				StringBuilder builder = new StringBuilder();
				builder.Append(this.mSchedule.GetXml());
				sThis = builder.ToString();
			} 
			catch(Exception) { }
			return sThis;
		}
		#endregion
		public DispatchDS Schedule { get { return this.mSchedule; } }
		public string ScheduleName { get { return this.mScheduleName; } }
		public string ScheduleTableName { get { return this.mTableName; } }
		public void Refresh() {
			try {				
				//Clear existing entries and refresh collection
				this.mSchedule.Clear();
				this.mSchedule.Merge(this.mMediator.FillDataset(this.mDataFile, "", null));
				this.OnScheduleChanged(this, new EventArgs());
			}
			catch(Exception ex) { throw ex; }
		}
		public abstract bool Add(ScheduleEntry entry);
		public abstract void AddList(DispatchDS entries);
		public abstract int Count { get; }
		public abstract ScheduleEntry Item();
		public abstract ScheduleEntry Item(int ID);
		public abstract bool Remove(ScheduleEntry entry);
		public abstract void RemoveList(DispatchDS entries);
		public abstract dlgSchedule ScheduleDialog(ScheduleEntry entry);
		protected void OnScheduleChanged(object sender, EventArgs e) { if(this.ScheduleChanged != null) this.ScheduleChanged(sender, e); }
		protected void OnEntryChanged(object sender, EventArgs e) { Refresh(); }
	}
}