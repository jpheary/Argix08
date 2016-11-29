//	File:	schedulenode.cs
//	Author:	J. Heary
//	Date:	09/30/05
//	Desc:	.
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
	public class ScheduleNode: TsortNode { 
		//Members
		private DispatchSchedule mSchedule=null;
		
		//Constants
		
		//Interface
		public ScheduleNode() : base() { }
		public ScheduleNode(string text, int imageIndex, int selectedImageIndex, DispatchSchedule schedule) : base(text, imageIndex, selectedImageIndex) { 
			//Constructor
			try {
				this.mSchedule = schedule;
				this.mSchedule.ScheduleChanged += new EventHandler(OnScheduleChanged);
			}
			catch(Exception ex) { throw ex; }
		}
		public DispatchSchedule Schedule { 
			get { return this.mSchedule; } 
			set { this.mSchedule = value; } 
		}
		#region TsortNode Implementations: LoadChildNodes(), CanOpen, Properties()
		public override void LoadChildNodes() { }
		public override bool CanOpen { get { return true; } }
		public override void Properties() { }
		#endregion
		public override object Clone() {
			//Clone this object for display in another treeview
			ScheduleNode node = (ScheduleNode)base.Clone();
			node.Schedule = this.Schedule;
			return node;
		}

		private void OnScheduleChanged(object sender, EventArgs e) {
			//Event handler for changes in schedule
			//Don't care 
		}
	}
}