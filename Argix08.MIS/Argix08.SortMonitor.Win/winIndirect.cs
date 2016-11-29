using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tsort.Enterprise;
using Tsort.Sort;

namespace Tsort.Tools {
    //
    public partial class winIndirect :Tsort.Tools.winStation {
        //Members
		private const string MNU_INTERPRETZEBRASTAUS = "Interpret Zebra Status";

        //Interface
        public winIndirect(StationFreightAssignment assignment): base(assignment) { 
            //Constructor
            InitializeComponent();
        }
        public winIndirect(SortStation station): base(station) {
            //Constructor
            InitializeComponent();
        }
        public override void Refresh() {
			//Request a refresh of all views
            base.Refresh();
		}
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
				//Display station properties
                if(this.mAssignment != null) {
                    this.lblTrip.Text = this.mAssignment.Freight.FreightID;
                    this.lblCartons.Text = this.mAssignment.Freight.CartonCount.ToString();
                }
				
				//Get initial views
                this.tabDialog.TabPages.Remove(this.tabStats);
				this.mStation.GetPrinterInfo();
                this.grdItems.DataMember = App.TBL_SCANSGET;
			} 
			catch(Exception ex) { reportError(ex); }
			finally { setUserServices(); this.Cursor = Cursors.Default;  }
		}
        #region Base Class Overrides: refresh()
        protected override void refresh() {
            //Request a refresh of all views
            this.lblScanned.Text = this.mStation != null ? this.mStation.Items.BwareScanTable.Rows.Count.ToString() : "";
        }
        #endregion
        #region Local Services: 
        #endregion
    }
}

