//	File:	layoutentry.cs
//	Author:	jheary
//	Date:	11/16/05
//	Desc:	Represents an entry (i.e. a single grid column) in a schedule 
//			layout (i.e. a grid layout).
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
	public class LayoutEntry {
		//Members
		private System.String _viewname="";
		private System.String _schedulename="";
		private System.String _key="";
		private System.Int32 _visibleposition=0;
		private System.Boolean _visible=true;
		private System.String _caption="";
		private System.Int32 _width=96;
		private System.String _alignment="L";
		private System.String _format="";
		private System.String _nulltext="";
		private System.String _sort="A";
		private System.Int32 _sortorder=-1;
		private System.Boolean _groupby=false;
		private Mediator mMediator=null;
		
		//Constants
		
		//Events
		public event EventHandler EntryChanged=null;
		
		//Interface
		public LayoutEntry(Mediator mediator): this(null, mediator) { }
		public LayoutEntry(LayoutDS.LayoutTableRow layout, Mediator mediator) { 
			//Constructor
			try { 
				this.mMediator = mediator;
				if(layout != null) { 
					this._viewname = layout.ViewName;
					this._schedulename = layout.ScheduleName;
					this._key = layout.Key;
					this._visibleposition = layout.VisiblePosition;
					this._visible = (!layout.IsVisibleNull()) ? layout.Visible : true;
					this._caption = (!layout.IsCaptionNull()) ? layout.Caption : layout.Key;
					this._width = (!layout.IsWidthNull()) ? layout.Width : 96;
					this._alignment = (!layout.IsAlignmentNull()) ? layout.Alignment : "L";
					this._format = (!layout.IsFormatNull()) ? layout.Format : "";
					this._nulltext = (!layout.IsNullTextNull()) ? layout.NullText : "";
					this._sort = (!layout.IsSortNull()) ? layout.Sort : "A";
					this._sortorder = (!layout.IsSortOrderNull()) ? layout.SortOrder : -1;
					this._groupby = (!layout.IsGroupByNull()) ? layout.GroupBy : false;
				}
			}
			catch(Exception ex) { throw ex; }
		}
		#region Accessors\Modifiers, ToDataSet(), ToString()
		public System.String ViewName { 
			get { return this._viewname; }
			set { this._viewname = value; }
		}
		public System.String ScheduleName { 
			get { return this._schedulename; }
			set { this._schedulename = value; }
		}
		public System.String Key { 
			get { return this._key; }
			set { this._key = value; }
		}
		public System.Int32 VisiblePosition { 
			get { return this._visibleposition; }
			set { this._visibleposition = value; }
		}
		public System.Boolean Visible { 
			get { return this._visible; }
			set { this._visible = value; }
		}
		public System.String Caption { 
			get { return this._caption; }
			set { this._caption = value; }
		}
		public System.Int32 Width { 
			get { return this._width; }
			set { this._width = value; }
		}
		public System.String Alignment { 
			get { return this._alignment; }
			set { this._alignment = value; }
		}
		public System.String Format { 
			get { return this._format; }
			set { this._format = value; }
		}
		public System.String NullText { 
			get { return this._nulltext; }
			set { this._nulltext = value; }
		}
		public System.String Sort { 
			get { return this._sort; }
			set { this._sort = value; }
		}
		public System.Int32 SortOrder { 
			get { return this._sortorder; }
			set { this._sortorder = value; }
		}
		public System.Boolean GroupBy { 
			get { return this._groupby; }
			set { this._groupby = value; }
		}
		public DataSet ToDataSet() { 
			//Return a dataset containing values for this object
			LayoutDS ds=null;
			try { 
				ds = new LayoutDS();
				LayoutDS.LayoutTableRow layout = ds.LayoutTable.NewLayoutTableRow();
				layout.ViewName = this._viewname;
				layout.ScheduleName = this._schedulename;
				layout.Key = this._key;
				layout.VisiblePosition = this._visibleposition;
				layout.Visible = this._visible;
				layout.Caption = this._caption;
				layout.Width = this._width;
				layout.Alignment = this._alignment;
				layout.Format = this._format;
				layout.NullText = this._nulltext;
				layout.Sort = this._sort;
				layout.SortOrder = this._sortorder;
				layout.GroupBy = this._groupby;
				ds.LayoutTable.AddLayoutTableRow(layout);
			}
			catch(Exception) { }
			return ds;
		}
		public override string ToString() { 
			//Custom ToString() method
			string sThis=base.ToString();
			try { 
				//Form string detail of this object
				StringBuilder builder = new StringBuilder();
				builder.Append("ViewName=" + this._viewname.ToString() + "\t");
				builder.Append("ScheduleName=" + this._schedulename.ToString() + "\t");
				builder.Append("Key=" + this._key.ToString() + "\t");
				builder.Append("VisiblePosition=" + this._visibleposition.ToString() + "\t");
				builder.Append("Visible=" + this._visible.ToString() + "\t");
				builder.Append("Caption=" + this._caption.ToString() + "\t");
				builder.Append("Width=" + this._width.ToString() + "\t");
				builder.Append("Alignment=" + this._alignment.ToString() + "\t");
				builder.Append("Format=" + this._format.ToString() + "\t");
				builder.Append("NullText=" + this._nulltext.ToString() + "\t");
				builder.Append("Sort=" + this._sort.ToString() + "\t");
				builder.Append("SortOrder=" + this._sortorder.ToString() + "\t");
				builder.Append("GroupBy=" + this._groupby.ToString() + "\t");
				builder.Append("\n");
				sThis = builder.ToString();
			}
			catch(Exception) { }
			return sThis;
		}
		#endregion
		public bool Create() {
			//Save this object
			bool bRet=false;
			try {
				LayoutDS ds = new LayoutDS();
				ds.Merge(this.mMediator.FillDataset(App.LAYOUT_FILE, "", null));
				ds.LayoutTable.AddLayoutTableRow(this._viewname,this._schedulename,this._key,this._visibleposition,this._visible,this._caption,this._width,this._alignment,this._format,this._nulltext,this._sort,this._sortorder,this._groupby);
				ds.LayoutTable.AcceptChanges();
				bRet = this.mMediator.ExecuteNonQuery(App.LAYOUT_FILE, new object[]{ds});
				if(this.EntryChanged != null) this.EntryChanged(this,new EventArgs());
			}
			catch(Exception ex) { throw ex; }
			return bRet;
		}
		public bool Update() {
			//Update this object
			bool bRet=false;
			try {
				LayoutDS ds = new LayoutDS();
				ds.Merge(this.mMediator.FillDataset(App.LAYOUT_FILE, "", null));
				LayoutDS.LayoutTableRow row = (LayoutDS.LayoutTableRow)ds.LayoutTable.Select("ViewName='" + this._viewname + "' AND ScheduleName='" + this._schedulename + "' AND Key='" + this._key + "'")[0];
				//row.ViewName = this._viewname;
				//row.ScheduleName = this._schedulename;
				//row.Key = this._key;
				row.VisiblePosition = this._visibleposition;
				row.Visible = this._visible;
				row.Caption = this._caption;
				row.Width = this._width;
				row.Alignment = this._alignment;
				row.Format = this._format;
				row.NullText = this._nulltext;
				row.Sort = this._sort;
				row.SortOrder = this._sortorder;
				row.GroupBy = this._groupby;
				ds.LayoutTable.AcceptChanges();
				bRet = this.mMediator.ExecuteNonQuery(App.LAYOUT_FILE, new object[]{ds});
				if(this.EntryChanged != null) this.EntryChanged(this,new EventArgs());
			}
			catch(Exception ex) { throw ex; }
			return bRet;
		}
		public bool Delete() {
			//Delete this object
			bool bRet=false;
			try {
				LayoutDS ds = new LayoutDS();
				ds.Merge(this.mMediator.FillDataset(App.LAYOUT_FILE, "", null));
				LayoutDS.LayoutTableRow row = (LayoutDS.LayoutTableRow)ds.LayoutTable.Select("ViewName='" + this._viewname + "' AND ScheduleName='" + this._schedulename + "' AND Key='" + this._key + "'")[0];
				row.Delete();
				ds.LayoutTable.AcceptChanges();
				bRet = this.mMediator.ExecuteNonQuery(App.LAYOUT_FILE, new object[]{ds});
				if(this.EntryChanged != null) this.EntryChanged(this,new EventArgs());
			}
			catch(Exception ex) { throw ex; }
			return bRet;
		}
	}
}
