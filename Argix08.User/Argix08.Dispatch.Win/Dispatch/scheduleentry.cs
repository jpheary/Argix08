//	File:	scheduleentry.cs
//	Author:	jheary
//	Date:	09/30/05
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
	public abstract class ScheduleEntry {
		//Members
		protected Mediator mMediator=null;
		protected string mDataFile="";
		
		//Constants
		
		//Events
		public event EventHandler EntryChanged=null;
		
		//Interface
		public ScheduleEntry(Mediator mediator) { this.mMediator = mediator; }
		#region Accessors\Modifiers, ToDataSet(), ToString()
		public abstract DataSet ToDataSet();
		#endregion
		public string DataFile { 
			get { return this.mDataFile; } 
			set { this.mDataFile = value; }
		}
		public abstract string EntryType { get; }
		public abstract int EntryID { get; }
		public abstract bool Create();
		public abstract bool Update();
		public abstract bool Delete();
//		public abstract ScheduleEntry Copy();
		protected void OnEntryChanged(object sender, EventArgs e) { if(this.EntryChanged != null) this.EntryChanged(sender, e); }
	}
}
