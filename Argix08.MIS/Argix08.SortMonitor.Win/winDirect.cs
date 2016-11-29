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
    public partial class winDirect :Tsort.Tools.winStation {
        //Members

        //Interface
        public winDirect(StationFreightAssignment assignment): base(assignment) { 
            //Constructor
            InitializeComponent();
        }
        public winDirect(SortStation station): base(station) {
            //Constructor
            InitializeComponent();
        }
        public override void Refresh() { base.Refresh(); }
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
				//Display station properties
                if(this.mAssignment != null) {
                    this.lblFreightID.Text = this.mAssignment.Freight.FreightID;
                    this.lblCartons.Text = this.mAssignment.Freight.CartonCount.ToString();
                }
                //this._lblFrieghtID.Visible = this._lblCartons.Visible = this._lblScanned.Visible = (this.mAssignment != null);
                //this.lblFreightID.Visible = this.lblCartons.Visible = this.lblScanned.Visible = (this.mAssignment != null);
				
				//Get initial views
                this.tabDialog.TabPages.Remove(this.tabLog);
                this.tabDialog.TabPages.Remove(this.tabStats);
				this.grdItems.DataMember = App.TBL_ITEMSGET;
			} 
			catch(Exception ex) { reportError(ex); }
			finally { setUserServices(); this.Cursor = Cursors.Default;  }
		}
        #region Base Class Overrides: refresh()
        protected override void refresh() {
            //Request a refresh of all views
            this.lblScanned.Text = this.mStation != null ? this.mStation.Items.SortedItemTable.Rows.Count.ToString() : "";
        }
        #endregion
        #region Local Services:
        #endregion
    }
}

