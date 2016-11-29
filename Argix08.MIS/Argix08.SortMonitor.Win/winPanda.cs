using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Argix.Data;
using Tsort.Enterprise;
using Tsort.Sort;

namespace Tsort.Tools {
    //
    public partial class winPanda :Tsort.Tools.winStation {
        //Members

        //Interface
        public winPanda(StationFreightAssignment assignment): base(assignment) { 
            //Constructor
            InitializeComponent();
        }
        public winPanda(SortStation station): base(station) {
            //Constructor
            InitializeComponent();
        }
        private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
				//Display station properties
                if(this.mAssignment != null) {
                    this.lblFreightID.Text = this.mAssignment.Freight.FreightID;
                    this.lblCartons.Text = this.mAssignment.Freight.CartonCount.ToString();
                }

                //Set log event filter entries
                //this.cboFilter.Items.Add("All");
                //this.cboFilter.SelectedIndex = 0;
                
                //Get initial views
				this.grdItems.DataMember = App.TBL_ITEMSGET;
			} 
			catch(Exception ex) { reportError(ex); }
			finally { setUserServices(); this.Cursor = Cursors.Default;  }
		}
        public override void Refresh() { base.Refresh(); }
        #region Base Class Overrides: refresh()
        protected override void refresh() {
            //Request a refresh of all views
            this.lblScanned.Text = this.mStation.Items.SortedItemTable.Rows.Count.ToString();
            refreshStatistics(base.StartDate,base.EndDate);
        }
        #endregion
        #region Local Services: refreshStatistics()
        private void refreshStatistics(DateTime startDate,DateTime endDate) {
            //Read log events
            try {
                Mediator mediator = new SQLMediator(this.mStation.TraceSQLConnection);
                DataSet ds = null;
                ds = mediator.FillDataset(App.USP_PANDA_DATAREQ,App.TBL_DATAREQ,new object[] { startDate.ToString("yyyy-MM-dd HH:mm:ss"),endDate.ToString("yyyy-MM-dd HH:mm:ss") });
                if(ds != null) this.lblLDReq.Text = ds.Tables[App.TBL_DATAREQ].Rows.Count.ToString().PadLeft(5, ' ');
                ds = mediator.FillDataset(App.USP_PANDA_DATARES,App.TBL_DATARES,new object[] { startDate.ToString("yyyy-MM-dd HH:mm:ss"),endDate.ToString("yyyy-MM-dd HH:mm:ss"), null });
                if(ds != null) this.lblLDRes.Text = ds.Tables[App.TBL_DATARES].Rows.Count.ToString().PadLeft(5,' ');
                ds = mediator.FillDataset(App.USP_PANDA_DATARES,App.TBL_DATARES,new object[] { startDate.ToString("yyyy-MM-dd HH:mm:ss"),endDate.ToString("yyyy-MM-dd HH:mm:ss"),"1" });
                if(ds != null) this.lblLDRes01.Text = ds.Tables[App.TBL_DATARES].Rows.Count.ToString().PadLeft(5,' ');
                ds = mediator.FillDataset(App.USP_PANDA_DATARES,App.TBL_DATARES,new object[] { startDate.ToString("yyyy-MM-dd HH:mm:ss"),endDate.ToString("yyyy-MM-dd HH:mm:ss"),"2" });
                if(ds != null) this.lblLDRes02.Text = ds.Tables[App.TBL_DATARES].Rows.Count.ToString().PadLeft(5,' ');
                ds = mediator.FillDataset(App.USP_PANDA_DATARES,App.TBL_DATARES,new object[] { startDate.ToString("yyyy-MM-dd HH:mm:ss"),endDate.ToString("yyyy-MM-dd HH:mm:ss"),"3" });
                if(ds != null) this.lblLDRes03.Text = ds.Tables[App.TBL_DATARES].Rows.Count.ToString().PadLeft(5,' ');
                ds = mediator.FillDataset(App.USP_PANDA_DATARES,App.TBL_DATARES,new object[] { startDate.ToString("yyyy-MM-dd HH:mm:ss"),endDate.ToString("yyyy-MM-dd HH:mm:ss"),"4" });
                if(ds != null) this.lblLDRes04.Text = ds.Tables[App.TBL_DATARES].Rows.Count.ToString().PadLeft(5,' ');
                ds = mediator.FillDataset(App.USP_PANDA_DATARES,App.TBL_DATARES,new object[] { startDate.ToString("yyyy-MM-dd HH:mm:ss"),endDate.ToString("yyyy-MM-dd HH:mm:ss"),"5" });
                if(ds != null) this.lblLDRes05.Text = ds.Tables[App.TBL_DATARES].Rows.Count.ToString().PadLeft(5,' ');
                ds = mediator.FillDataset(App.USP_PANDA_DATARES,App.TBL_DATARES,new object[] { startDate.ToString("yyyy-MM-dd HH:mm:ss"),endDate.ToString("yyyy-MM-dd HH:mm:ss"),"6" });
                if(ds != null) this.lblLDRes06.Text = ds.Tables[App.TBL_DATARES].Rows.Count.ToString().PadLeft(5,' ');
                ds = mediator.FillDataset(App.USP_PANDA_DATARES,App.TBL_DATARES,new object[] { startDate.ToString("yyyy-MM-dd HH:mm:ss"),endDate.ToString("yyyy-MM-dd HH:mm:ss"),"7" });
                if(ds != null) this.lblLDRes07.Text = ds.Tables[App.TBL_DATARES].Rows.Count.ToString().PadLeft(5,' ');
                ds = mediator.FillDataset(App.USP_PANDA_DATARES,App.TBL_DATARES,new object[] { startDate.ToString("yyyy-MM-dd HH:mm:ss"),endDate.ToString("yyyy-MM-dd HH:mm:ss"),"8" });
                if(ds != null) this.lblLDRes08.Text = ds.Tables[App.TBL_DATARES].Rows.Count.ToString().PadLeft(5,' ');
                ds = mediator.FillDataset(App.USP_PANDA_DATARES,App.TBL_DATARES,new object[] { startDate.ToString("yyyy-MM-dd HH:mm:ss"),endDate.ToString("yyyy-MM-dd HH:mm:ss"),"9" });
                if(ds != null) this.lblLDRes09.Text = ds.Tables[App.TBL_DATARES].Rows.Count.ToString().PadLeft(5,' ');

                ds = mediator.FillDataset(App.USP_PANDA_VERIFYREQ,App.TBL_VERIFYREQ,new object[] { startDate.ToString("yyyy-MM-dd HH:mm:ss"),endDate.ToString("yyyy-MM-dd HH:mm:ss"),null });
                if(ds != null) this.lblVLReq.Text = ds.Tables[App.TBL_VERIFYREQ].Rows.Count.ToString().PadLeft(5,' ');
                ds = mediator.FillDataset(App.USP_PANDA_VERIFYREQ,App.TBL_VERIFYREQ,new object[] { startDate.ToString("yyyy-MM-dd HH:mm:ss"),endDate.ToString("yyyy-MM-dd HH:mm:ss"), "1" });
                if(ds != null) this.lblVLReq1.Text = ds.Tables[App.TBL_VERIFYREQ].Rows.Count.ToString().PadLeft(5,' ');
                ds = mediator.FillDataset(App.USP_PANDA_VERIFYREQ,App.TBL_VERIFYREQ,new object[] { startDate.ToString("yyyy-MM-dd HH:mm:ss"),endDate.ToString("yyyy-MM-dd HH:mm:ss"), "2" });
                if(ds != null) this.lblVLReq2.Text = ds.Tables[App.TBL_VERIFYREQ].Rows.Count.ToString().PadLeft(5,' ');
                ds = mediator.FillDataset(App.USP_PANDA_VERIFYREQ,App.TBL_VERIFYREQ,new object[] { startDate.ToString("yyyy-MM-dd HH:mm:ss"),endDate.ToString("yyyy-MM-dd HH:mm:ss"),"3" });
                if(ds != null) this.lblVLReq3.Text = ds.Tables[App.TBL_VERIFYREQ].Rows.Count.ToString().PadLeft(5,' ');
                
                ds = mediator.FillDataset(App.USP_PANDA_VERIFYRES,App.TBL_VERIFYRES,new object[] { startDate.ToString("yyyy-MM-dd HH:mm:ss"),endDate.ToString("yyyy-MM-dd HH:mm:ss"),null });
                if(ds != null) this.lblVLRes.Text = ds.Tables[App.TBL_VERIFYRES].Rows.Count.ToString().PadLeft(5,' ');
                ds = mediator.FillDataset(App.USP_PANDA_VERIFYRES,App.TBL_VERIFYRES,new object[] { startDate.ToString("yyyy-MM-dd HH:mm:ss"),endDate.ToString("yyyy-MM-dd HH:mm:ss"), "N" });
                if(ds != null) this.lblVLResN.Text = ds.Tables[App.TBL_VERIFYRES].Rows.Count.ToString().PadLeft(5,' ');
                ds = mediator.FillDataset(App.USP_PANDA_VERIFYRES,App.TBL_VERIFYRES,new object[] { startDate.ToString("yyyy-MM-dd HH:mm:ss"),endDate.ToString("yyyy-MM-dd HH:mm:ss"), "Y" });
                if(ds != null) this.lblVLResY.Text = ds.Tables[App.TBL_VERIFYRES].Rows.Count.ToString().PadLeft(5,' ');

            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while refreshing statistics.",ex); }
        }
        #endregion
    }
}

