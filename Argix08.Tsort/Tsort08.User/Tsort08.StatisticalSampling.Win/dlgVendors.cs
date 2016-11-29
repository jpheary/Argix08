using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.ApplicationBlocks.Data;

namespace Argix.Freight {
    //
    public partial class dlgVendors :Form {
        //Members

        //Interface
        public dlgVendors() {
            //Constructor
            try {
                InitializeComponent();
            }
            catch(Exception ex) { throw new ApplicationException("Error while creating new dlgVendors instance.",ex); }
        }
        private void OnFormLoad(object sender,EventArgs e) {
            //Event handler for form load event
            try {
                OnRefresh(null,EventArgs.Empty);
            }
            catch(Exception ex) { App.ReportError(new ApplicationException("Unexpected error on loading dlgSample.", ex), true); }
        }
        private void OnRefresh(object sender,EventArgs e) {
            //Event handler for refresh button clicked
            try {
                this.mVendorDS.Clear();
                DataSet ds = App.Mediator.FillDataset(App.USP_VENDOR_GETLIST,App.TBL_VENDOR,null);
                this.mVendorDS.Merge(ds);
            }
            catch(Exception ex) { App.ReportError(new ApplicationException("Unexpected error when refreshing sample data.",ex),true); }
        }
        private void OnClose(object sender,EventArgs e) {
            //Event handler for close button clicked
            try {
                this.Close();
            }
            catch(Exception ex) { App.ReportError(ex,true); }
        }
        private void OnRowValidated(object sender,DataGridViewCellEventArgs e) {
            update();
        }
        private void update() {
            bool changes = this.mVendorDS.HasChanges();
            if(changes) {
                if(this.mVendorDS.HasChanges(DataRowState.Added)) {
                    ShipperDS addedDS = (ShipperDS)this.mVendorDS.GetChanges(DataRowState.Added);
                    //System.Diagnostics.Debug.WriteLine("Added..........");
                    //System.Diagnostics.Debug.Write(addedDS.GetXml());
                    //System.Diagnostics.Debug.Write("\n");
                    foreach(ShipperDS.VendorTableRow v in addedDS.VendorTable.Rows) {
                        //Add each new vendor
                        try {
                            bool ret = App.Mediator.ExecuteNonQuery(App.USP_VENDOR_NEW,new object[] { v.NUMBER.Trim(),v.NAME.Trim(),v.ADDRESS.Trim(),"",v.CITY.Trim(),v.STATE.Trim(),v.ZIP.Trim(),"0000" });
                        }
                        catch(Exception ex) { App.ReportError(new Exception("Exception creating vendor# " + v.NUMBER.Trim(),ex),true); }
                    }
                }

                if(this.mVendorDS.HasChanges(DataRowState.Modified)) {
                    ShipperDS modifiedDS = (ShipperDS)this.mVendorDS.GetChanges(DataRowState.Modified);
                    //System.Diagnostics.Debug.WriteLine("Modified..........");
                    //System.Diagnostics.Debug.Write(modifiedDS.GetXml());
                    //System.Diagnostics.Debug.Write("\n");
                    foreach(ShipperDS.VendorTableRow v in modifiedDS.VendorTable.Rows) {
                        //Update each modified vendor
                        try {
                            bool ret = App.Mediator.ExecuteNonQuery(App.USP_VENDOR_UPDATE,new object[] { v.NUMBER.Trim(),v.STATUS,v.NAME.Trim(),v.ADDRESS.Trim(),"",v.CITY.Trim(),v.STATE.Trim(),v.ZIP.Trim(),"0000" });
                        }
                        catch(Exception ex) { App.ReportError(new Exception("Exception updating vendor# " + v.NUMBER.Trim(),ex),true); }
                    }
                }
                OnRefresh(null,EventArgs.Empty);
            }
        }
    }
}