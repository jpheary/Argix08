//	File:	layoutviews.cs
//	Author:	jheary
//	Date:	11/17/05
//	Desc:	Manages the collection of layout views for a single schedule.
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
	public class LayoutViews {
		//Members
		private string mScheduleName="";
		private LayoutDS mLayoutViews=null;
		private LayoutView mActiveView=null;
		private Mediator mMediator=null;
		
		//Constants
		
		//Events
		public event EventHandler ViewsChanged=null;
		
		//Interface
		public LayoutViews(string scheduleName, Mediator mediator) { 
			//Constructor
			try { 
				this.mScheduleName = scheduleName;
				this.mMediator = mediator;
				this.mLayoutViews = new LayoutDS();
				Read();
				LayoutDS.ViewTableRow[] rows = (LayoutDS.ViewTableRow[])this.mLayoutViews.ViewTable.Select("ScheduleName='" + this.mScheduleName + "' AND Active=true");
				if(rows.Length > 0)
					this.mActiveView = new LayoutView(rows[0].ViewName, this.mScheduleName, this.mMediator);
				else
					this.mActiveView = new LayoutView("Default", this.mScheduleName, this.mMediator);
				this.mActiveView.ViewChanged += new EventHandler(OnViewChanged);
			}
			catch(Exception ex) { throw ex; }
		}
		public string ScheduleName { get { return this.mScheduleName; } }
		public LayoutDS Layouts { get { return this.mLayoutViews; } }
		public LayoutView ActiveView { get { return this.mActiveView; } }
		public void SetActiveView(string viewName) {
			//
			try {
				//Capture
				this.mActiveView = new LayoutView(viewName, this.mScheduleName, this.mMediator);
				
				//Read data store
				LayoutDS ds = new LayoutDS();
				ds.Merge(this.mMediator.FillDataset(App.LAYOUTVIEWS_FILE, "", null));
				
				//Remove current active layout, and set new active layout
				LayoutDS.ViewTableRow[] rows;
				rows = (LayoutDS.ViewTableRow[])ds.ViewTable.Select("ScheduleName='" + this.mScheduleName + "' AND Active=true");
				if(rows.Length > 0) rows[0].Active = false;
				rows = (LayoutDS.ViewTableRow[])ds.ViewTable.Select("ScheduleName='" + this.mScheduleName + "' AND ViewName='" + viewName + "'");
				if(rows.Length > 0) rows[0].Active = true;
				
				//Update data store
				ds.ViewTable.AcceptChanges();
				this.mMediator.ExecuteNonQuery(App.LAYOUTVIEWS_FILE, new object[]{ds});
				Read();
			}
			catch(Exception ex) { throw ex; }
		}
		#region View Collection Management: Add(), Count, Item(), Item(string), Remove()
		public bool Add(LayoutView view) {
			//Add a new view
			bool bRet=false;
			try { 
				view.Create();
				Read();
			}
			catch(Exception ex) { throw ex; }
			return bRet;
		}
		public int Count { get { return this.mLayoutViews.ViewTable.Rows.Count; } }
		public LayoutView Item() {
			//Return a new blank view object
			LayoutView view=null;
			try { 
				view = new LayoutView("", this.mScheduleName, this.mMediator);
			}
			catch (Exception ex) { throw ex; }
			return view;
		}
		public LayoutView Item(string viewName) {
			//Return an existing view object from the layout view
			LayoutView view=null;
			try { 
				//Merge from collection (dataset)
				if(viewName.Length > 0) {
					DataRow[] rows = this.mLayoutViews.ViewTable.Select("ViewName='" + viewName + "'");
					if(rows.Length == 0)
						throw new ApplicationException("View " + viewName + " does not exist for ti schedule!\n");
					LayoutDS.ViewTableRow row = (LayoutDS.ViewTableRow)rows[0];
					if(row.Active)
						view = this.mActiveView;
					else 
						view = new LayoutView(viewName, this.mScheduleName, this.mMediator);
				}
				else 
					view = (LayoutView)Item();
				view.ViewChanged -= new EventHandler(OnViewChanged);
				view.ViewChanged += new EventHandler(OnViewChanged);
			}
			catch (Exception ex) { throw ex; }
			return view;
		}
		public bool Remove(LayoutView view) {
			//Remove the specified view
			bool bRet=false;
			try { 
				//Delete the views entry, and delete the layout entries
				view.Delete();
				Read();
			}
			catch(Exception ex) { throw ex; }
			return bRet;
		}
		#endregion
		public void Read() { 
			//Read all layout views for this schedule
			try {				
				//Read from data store
				this.mLayoutViews.Clear();
				try {
					LayoutDS ds = new LayoutDS();
					ds.Merge(this.mMediator.FillDataset(App.LAYOUTVIEWS_FILE, "", null));
					this.mLayoutViews.Merge(ds.ViewTable.Select("ScheduleName='" + this.mScheduleName + "'"));
					if(this.ViewsChanged != null) this.ViewsChanged(this,new EventArgs());
				}
				catch(Exception) { }
			}
			catch(Exception ex) { throw ex; }
		}
		private void OnViewChanged(object sender, EventArgs e) {
			//Event handler for change to a view
			try {
				//In case of a change to name or active status
				Read();
			}
			catch(Exception ex) { throw ex; }
		}
	}
}
