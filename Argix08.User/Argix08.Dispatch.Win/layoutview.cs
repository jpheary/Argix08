//	File:	layoutview.cs
//	Author:	J. Heary
//	Date:	11/16/05
//	Desc:	Represents a schedule view layout (i.e. a grid layout or a collection 
//			of columns in a view). A schedule can have one or more layouts (views);
//			each view has a unique name within a schedule.
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
	public class LayoutView: IEnumerable, IEnumerator {
		//Members
		private string mViewName="";
		private string mScheduleName="";
		private LayoutDS mLayout=null;
		private int mCurrentIndex=-1;		//Enumeration support
		private Mediator mMediator=null;
		
		//Constants

		//Events
		public event EventHandler ViewChanged=null;
		
		//Interface
		public LayoutView(string viewName, string scheduleName, Mediator mediator) { 
			//Constructor
			try {
				//Set custom attributes
				this.mViewName = viewName;
				this.mScheduleName = scheduleName;
				this.mMediator = mediator;
				this.mLayout = new LayoutDS();
				Read();
			}
			catch(Exception ex) { throw ex; }
		}
		#region Accessors\Modifiers, ToString(), ToDataSet()
		#endregion
		public string ViewName { 
			get { return this.mViewName; }
			set { 
				if(this.mViewName.Length > 0) {
					//Existing view: read the data store
					LayoutDS ds = new LayoutDS();
					ds.Merge(this.mMediator.FillDataset(App.LAYOUT_FILE, "", null));
					
					//Update views entry in the view store
					LayoutDS.ViewTableRow row = (LayoutDS.ViewTableRow)ds.ViewTable.Select("ViewName='" + this.mViewName + "' AND ScheduleName='" + this.mScheduleName + "'")[0];
					row.ViewName = value;
					
					//Update all layout entries in the view store
					LayoutDS.LayoutTableRow[] rows = (LayoutDS.LayoutTableRow[])ds.LayoutTable.Select("ViewName='" + this.mViewName + "' AND ScheduleName='" + this.mScheduleName + "'");
					for(int i=0; i<rows.Length; i++)
						rows[i].ViewName = value;
					
					ds.LayoutTable.AcceptChanges();
					this.mMediator.ExecuteNonQuery(App.LAYOUT_FILE, new object[]{ds});
					this.mViewName = value;
					Read();
				}
				else {
					//New view (not in data store yet)
					this.mViewName = value;
					for(int i=0; i<this.mLayout.LayoutTable.Rows.Count; i++)
						this.mLayout.LayoutTable[i].ViewName = value;
				}
			}
		}
		public string ScheduleName { get { return this.mScheduleName; } }
		public LayoutDS Layout { get { return this.mLayout; } }
		#region Entry Collection Management: Add(), Count, Item(), Item(string), Remove()
		public bool Add(LayoutEntry entry) {
			//Add a new entry to the layout
			throw new ApplicationException("A schedule layout cannot add new members.");
		}
		public int Count { get { return this.mLayout.LayoutTable.Rows.Count; } }
		public LayoutEntry Item() {
			//Return a new blank entry object
			LayoutEntry entry=null;
			try { 
				entry = new LayoutEntry(this.mMediator);
				//entry.DataFile = App.LAYOUT_FILE;
				entry.EntryChanged += new EventHandler(OnEntryChanged);
			}
			catch (Exception ex) { throw ex; }
			return entry;
		}
		public LayoutEntry Item(string key) {
			//Return an existing entry object from the layout view
			LayoutEntry entry=null;
			try { 
				//Merge from collection (dataset)
				if(key.Length > 0) {
					DataRow[] rows = this.mLayout.LayoutTable.Select("Key='" + key + "'");
					if(rows.Length == 0)
						throw new ApplicationException("Entry " + key + " does not exist in this view!\n");
					LayoutDS.LayoutTableRow row = (LayoutDS.LayoutTableRow)rows[0];
					entry = new LayoutEntry(row, this.mMediator);
					//entry.DataFile = App.LAYOUT_FILE;
					entry.EntryChanged += new EventHandler(OnEntryChanged);
				}
				else 
					entry = (LayoutEntry)Item();
			}
			catch (Exception ex) { throw ex; }
			return entry;
		}
		public bool Remove(LayoutEntry entry) {
			//Remove the specified entry from the layout
			throw new ApplicationException("A schedule layout cannot remove existing memebers.");
		}
		protected void OnEntryChanged(object sender, EventArgs e) { Read(); }
		#endregion
		public void Create() { 
			//Create a new layout view
			try {
				//Create new layout in data store
				LayoutDS ds = new LayoutDS();
				ds.Merge(this.mMediator.FillDataset(App.LAYOUT_FILE, "", null));
				
				//Create views entry in the view store
				ds.ViewTable.AddViewTableRow(this.mViewName, this.mScheduleName, false);
				
				//Create layout entries in the data store
				ds.Merge(this.mLayout);
				ds.LayoutTable.AcceptChanges();
				this.mMediator.ExecuteNonQuery(App.LAYOUT_FILE, new object[]{ds});
				Read();
			}
			catch(Exception ex) { throw ex; }
		}
		public void Read() { 
			//Read this layout view
			try {				
				//Read from data store
				this.mLayout.Clear();
				try {
					LayoutDS ds = new LayoutDS();
					ds.Merge(this.mMediator.FillDataset(App.LAYOUT_FILE, "", null));
					if(this.mViewName.Length > 0) {
						//Existing view
						this.mLayout.Merge(ds.LayoutTable.Select("ViewName='" + this.mViewName + "' AND ScheduleName='" + this.mScheduleName + "'"));
					}
					else {
						//New view
						this.mLayout.Merge(ds.LayoutTable.Select("ViewName='Default' AND ScheduleName='" + this.mScheduleName + "'"));
						for(int i=0; i<this.mLayout.LayoutTable.Rows.Count; i++)
							this.mLayout.LayoutTable[i].ViewName = "";
					}
					if(this.ViewChanged != null) this.ViewChanged(this,new EventArgs());
				}
				catch(Exception) { }
			}
			catch(Exception ex) { throw ex; }
		}
		public void Update(LayoutDS layout) {
			//Update this entire layout view
			try {
				//Delete existing layout in data store
				LayoutDS ds = new LayoutDS();
				ds.Merge(this.mMediator.FillDataset(App.LAYOUT_FILE, "", null));
				LayoutDS.LayoutTableRow[] rows = (LayoutDS.LayoutTableRow[])ds.LayoutTable.Select("ViewName='" + this.mViewName + "' AND ScheduleName='" + this.mScheduleName + "'");
				for(int i=0; i<rows.Length; i++)
					rows[i].Delete();
					
				//Add updated layout to data store
				ds.Merge(layout);
				ds.LayoutTable.AcceptChanges();
				this.mMediator.ExecuteNonQuery(App.LAYOUT_FILE, new object[]{ds});
				Read();
			}
			catch(Exception ex) { throw ex; }
		}
		public void Delete() {
			//Delete this entire layout view
			try {
				//Read the data store
				LayoutDS ds = new LayoutDS();
				ds.Merge(this.mMediator.FillDataset(App.LAYOUT_FILE, "", null));
				
				//Delete views entry from the view store
				LayoutDS.ViewTableRow row = (LayoutDS.ViewTableRow)ds.ViewTable.Select("ViewName='" + this.mViewName + "' AND ScheduleName='" + this.mScheduleName + "'")[0];
				row.Delete();
				
				//Delete all layout entries from the view store
				LayoutDS.LayoutTableRow[] rows = (LayoutDS.LayoutTableRow[])ds.LayoutTable.Select("ViewName='" + this.mViewName + "' AND ScheduleName='" + this.mScheduleName + "'");
				for(int i=0; i<rows.Length; i++)
					rows[i].Delete();
				ds.LayoutTable.AcceptChanges();
				this.mMediator.ExecuteNonQuery(App.LAYOUT_FILE, new object[]{ds});
				Read();
			}
			catch(Exception ex) { throw ex; }
		}
		#region Enumeration Support: IEnumerator- GetEnumerator(), IEnumerable- Reset(), Current(), MoveNext()
		public IEnumerator GetEnumerator() { this.mCurrentIndex = -1; return this; }
		public void Reset() { this.mCurrentIndex = -1; }
		public object Current { 
			get { 
				//Allow exception on bad index
				return new LayoutEntry(this.mLayout.LayoutTable[this.mCurrentIndex], this.mMediator);
			} 
		}
		public bool MoveNext() { 
			if(this.mCurrentIndex < this.mLayout.LayoutTable.Rows.Count) this.mCurrentIndex++;
			return (this.mCurrentIndex < this.mLayout.LayoutTable.Rows.Count); 
		}
		#endregion
	}
}