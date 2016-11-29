using System;
using System.Data;
using System.Diagnostics;
using System.Text;
using Tsort.Labels;
using Argix.Data;

namespace Argix.Enterprise {
	//
	public class EnterpriseTerminal {
		//Members
		private StoreDS mStores=null;

        public const string USP_STORES_VIEW = "uspInStoreLabelStoreGetList",TBL_STORES_VIEW = "StoreTable";
        public const string USP_LABELTEMPLATE = "uspInStoreLabelOutboundLabelGet",TBL_LABELTEMPLATE = "LabelDetailTable";
        public event EventHandler StoresChanged=null;
		
		//Interface
		public EnterpriseTerminal() : this(null) { }
		public EnterpriseTerminal(TerminalDS.LocalTerminalTableRow terminal) {
			//Constructor
			try {
				//Configure this terminal from the terminal configuration information
				this.mStores = new StoreDS();
			} 
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new terminal.", ex); }
		}
		public StoreDS Stores { get { return this.mStores; } }
		public void RefreshStores() {
			//Update collection of stores
			try {
				this.mStores.Clear();
				DataSet ds = App.Mediator.FillDataset(USP_STORES_VIEW, TBL_STORES_VIEW, null);
				if(ds!=null) 
					this.mStores.Merge(ds);
			}
			catch(Exception ex) { throw new ApplicationException("Failed to refresh stores.", ex); }
			finally { if(this.StoresChanged != null) this.StoresChanged(this, new EventArgs()); }
		}
		public LabelDS.LabelDetailTableRow GetLabelTemplate(string labelType, string printerType) {
			//Get a zpl label template for the specified labelType and printerType
			LabelDS.LabelDetailTableRow template=null;
			try {
				DataSet ds = App.Mediator.FillDataset(USP_LABELTEMPLATE, TBL_LABELTEMPLATE, new object[]{labelType, printerType});
				if(ds != null) {
					LabelDS templates = new LabelDS();
					templates.LabelDetailTable.AddLabelDetailTableRow(labelType, printerType, "");
					templates.LabelDetailTable[0].LABEL_STRING = ds.Tables[TBL_LABELTEMPLATE].Rows[0]["LABEL_STRING"].ToString();
					template = templates.LabelDetailTable[0];
				}
			}
			catch (IndexOutOfRangeException ex) { throw new ApplicationException("Label template not found for label " + labelType + " on " + printerType + " printer", ex); }
			catch(Exception ex) { throw new ApplicationException("Failed to get label template.", ex); }
			return template;
        }
    }
}