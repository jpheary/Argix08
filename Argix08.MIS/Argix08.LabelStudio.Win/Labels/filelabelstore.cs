//	File:	filelabelstore.cs
//	Author:	J. Heary
//	Date:	08/18/05
//	Desc:	Concrete implementation of the abstract Tsort.Labels.LabelStore class;
//			implements a label store that represents a single file (*.*) containing 
//			a collection label templates.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Tsort.Labels {
	//
	public class FileLabelStore: LabelStore {
		//Members
		private FileInfo mFile=null;
		
		//Constants
		//Events
		//Interface
		public FileLabelStore(string file) { 
			//Constructor
			try {
				//Set custom attributes
				this.mFile = new FileInfo(file);
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new File Label Store instance.", ex); }
		}
		#region Accessors\Modifiers: Directory, ToDataSet()
		public FileInfo Directory { get { return this.mFile; } }
		public DataSet ToDataSet() {
			//Return a dataset containing values for this terminal
			DataSet ds=null;
			try {
				ds = new DataSet();
				DataTable table = ds.Tables.Add("DirectoryDetailTable");
				table.Columns.Add("Directory");
				table.Rows.Add(new object[]{this.mFile.FullName});
			}
			catch(Exception) { }
			return ds;
		}
		#endregion
		public override LabelTemplate NewLabelTemplate() { return new FileLabelTemplate(null); }
		public override LabelTemplate NewLabelTemplate(LabelDS.LabelDetailTableRow row) { return new FileLabelTemplate(row); }
		public override void Refresh() { 
			try {				
				//Clear existing entries
				base.mLabels.Clear();
				base.mLabels.ReadXml(this.mFile.FullName, XmlReadMode.Fragment);
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while refreshing labels in the File store.", ex); }
			finally { OnStoreChanged(this, new EventArgs()); }
		}
	}
}