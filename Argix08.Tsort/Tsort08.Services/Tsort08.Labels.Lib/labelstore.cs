//	File:	labelstore.cs
//	Author:	J. Heary
//	Date:	01/02/08
//	Desc:	Tsort label store is the parent of a group of label templates.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;

namespace Tsort.Labels {
	//
	public abstract class LabelStore {
		//Members
		protected LabelDS mLabels=null;
		
		public const string PRINTER_170Xi2 = "170Xi2";
		public const string PRINTER_110 = "110";
		public const string PRINTER_110PAX4 = "110PAX4";
		
		public event EventHandler StoreChanged=null;
		
		//Interface
		public LabelStore() { 
			//Constructor
			try {
				this.mLabels = new LabelDS();
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Label Store instance.", ex); }
		}
		public LabelDS Labels { get { return this.mLabels; } }
		public virtual void Refresh() {
			//Refresh label templates for this store;
			//NOTE: Derived classes override to populate label templates collection
			try {
				//Default implementation just clears collection
				this.mLabels.Clear();
				if(this.StoreChanged != null) this.StoreChanged(this, new EventArgs());
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while refreshing the Label Store.", ex); }
		}
		public abstract LabelTemplate NewLabelTemplate();
		public abstract LabelTemplate NewLabelTemplate(LabelDS.LabelDetailTableRow row);
		public bool Add(LabelTemplate template) {
			//Add a new label template
			bool ret=false;
			try { 
				ret = template.Create();
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while adding new label.", ex); }
			return ret;
		}
		public int Count { get { return this.mLabels.LabelDetailTable.Rows.Count; } }
		public LabelTemplate Item() {
			//Return a new blank label template object
			LabelTemplate template=null;
			try { 
				template = NewLabelTemplate();
				template.TemplateChanged += new EventHandler(OnTemplateChanged);
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while getting existing label.", ex); }
			return template;
		}
		public LabelTemplate Item(string labelType, string printerType) {
			//Return an existing template object from the label templates collection
			LabelTemplate label=null;
			try { 
				//Merge from collection (dataset)
				if(labelType != "" && printerType != "") {
					DataRow[] rows = this.mLabels.LabelDetailTable.Select("LABEL_TYPE='" + labelType + "' AND PrinterType='" + printerType + "'");
					if(rows.Length == 0)
						throw new Exception("Label template for label type " + labelType + ", printer type " + printerType + " does not exist in this store!\n");
					LabelDS.LabelDetailTableRow row = (LabelDS.LabelDetailTableRow)rows[0];
					label = NewLabelTemplate(row);	//, this.mMediator);
					label.TemplateChanged +=new EventHandler(OnTemplateChanged);
				}
				else 
					label = Item();
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while getting existing label.", ex); }
			return label;
		}
		public bool Remove(LabelTemplate template) {
			//Remove the specified label template
			bool ret=false;
			try { 
				ret = template.Delete();
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while removing existing label.", ex); }
			return ret;
		}
		private void OnTemplateChanged(object sender, EventArgs e) {
			//Event handler for change in a child template
			try { 
				Refresh();
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error on template change.", ex); }
		}
		protected void OnStoreChanged(object sender, EventArgs e) { 
			//Fire event on behalf of derived classes
			if(this.StoreChanged != null) this.StoreChanged(sender, e);
		}
	}
}