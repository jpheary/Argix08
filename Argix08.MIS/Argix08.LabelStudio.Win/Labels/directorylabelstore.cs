//	File:	directorylabelstore.cs
//	Author:	J. Heary
//	Date:	08/18/05
//	Desc:	Concrete implementation of the abstract Tsort.Labels.LabelStore class;
//			implements a label store that represents a file system directory 
//			containing a group of label template files (*.zpl).
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Tsort.Labels {
	//
	public class DirectoryLabelStore: LabelStore {
		//Members
		private FileInfo mDirectory=null;
		private const string LABELTEMPLATE_EXT = "zpl";

        //Interface
		public DirectoryLabelStore(string directory) { 
			//Constructor
			try {
				//Set custom attributes
				this.mDirectory = new FileInfo(directory);
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Directory Label Store instance.", ex); }
		}
		#region Accessors\Modifiers: [Members...]
		public FileInfo Directory { get { return this.mDirectory; } }
		#endregion
		public override LabelTemplate NewLabelTemplate() { return new DirectoryLabelTemplate(null); }
		public override LabelTemplate NewLabelTemplate(LabelDS.LabelDetailTableRow row) { return new DirectoryLabelTemplate(row); }
		public override void Refresh() { 
			try {				
				//Clear existing entries
				base.mLabels.Clear();
				FileInfo[] oLabelTemplates = this.mDirectory.Directory.GetFiles("*." + LABELTEMPLATE_EXT);
				for(int i=0; i<oLabelTemplates.Length; i++) {
                    base.mLabels.ReadXml(oLabelTemplates[i].FullName,XmlReadMode.Auto);
				}
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while refreshing labels in the Directory store.", ex); }
			finally { OnStoreChanged(this, new EventArgs()); }
		}
	}
}