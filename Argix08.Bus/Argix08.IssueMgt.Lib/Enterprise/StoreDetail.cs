using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Argix.Enterprise {
    //
    [HelpKeywordAttribute(typeof(UserControl))]
    [ToolboxItem("System.Windows.Forms.Design.AutoSizeToolboxItem, System.Design")]
    public partial class StoreDetail :UserControl {
        //Members
        private EnterpriseDS mStoreDS = null;
        public event ControlErrorEventHandler Error = null;

        //Interface
        public StoreDetail() {
            //Constructor
            try {
                InitializeComponent();
            }
            catch(Exception ex) { throw new ControlException("Unexpected error while creating new StoreDetail control instance.",ex); }
        }
        
        [Browsable(true)]
        [Category("Behavior")]
        [Description("Gets or sets the read only state of the control.")]
        [DefaultValue(typeof(System.Boolean),"False")]
        [ReadOnly(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(false)]
        [HelpKeywordAttribute("Argix.Enterprise.StoreDetail.ReadOnly")]
        public Boolean ReadOnly { 
            get { return !this.txtDetail.Enabled; } 
            set { 
                this.txtDetail.Enabled = !value;
                this.txtDetail.BackColor = value ? System.Drawing.SystemColors.Control : System.Drawing.SystemColors.Window;
            } 
        }
        public void ResetReadOnly() {  this.txtDetail.Enabled = false; }
        private bool ShouldSerializeReadOnly() { return (this.txtDetail.Enabled != true);  }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets the current store.")]
        [Localizable(false)]
        [HelpKeywordAttribute("Argix.Enterprise.StoreDetail.Store")]
        public EnterpriseDS Store { get { return this.mStoreDS; } }

        public void Clear() { this.txtDetail.Text = ""; this.mStoreDS = null; }
        public void View(int companyID,int storeNumber) {
            //View store detail
            try {
                this.mStoreDS = new EnterpriseDS();
                refreshView();
                EnterpriseDS ds = new EnterpriseDS();
                ds.Merge(EnterpriseFactory.GetStoreDetail(companyID,storeNumber));
                if(ds.StoreTable.Rows.Count > 0) this.mStoreDS = ds;
                refreshView();
            }
            catch(Exception ex) { reportError(new ControlException("Unexpected error while creating store detail view.",ex)); }
        }
        public void View(int companyID,string subStore) {
            //View store detail
            try {
                this.mStoreDS = new EnterpriseDS();
                refreshView();
                EnterpriseDS ds = new EnterpriseDS();
                ds.Merge(EnterpriseFactory.GetStoreDetail(companyID,subStore));
                if(ds.StoreTable.Rows.Count > 0) this.mStoreDS = ds;
                refreshView();
            }
            catch(Exception ex) { reportError(new ControlException("Unexpected error while creating store detail view.",ex)); }
        }
        private void OnControlLoad(object sender,EventArgs e) {
            //
            this.Cursor = Cursors.WaitCursor;
            try {
                //
            }
            catch(Exception ex) { reportError(new ControlException("Unexpected error while loading StoreDetail control.",ex)); }
            finally { this.Cursor = Cursors.Default; }
        }
        #region Local Services: refreshView(), getDeliveryDays(), getOFD(), reportError()
        private void refreshView() {
            //
            this.txtDetail.Text = "";
            if(this.mStoreDS.StoreTable.Rows.Count > 0) {
                //Set header and body
                EnterpriseDS.StoreTableRow store = this.mStoreDS.StoreTable[0];
                this.txtDetail.Text = store.StoreName.Trim() + " (sub store #" + store.SubStoreNumber.Trim() + ")" + "\r\n";

                //Set body
                this.txtDetail.AppendText((!store.IsStoreAddressline1Null() ? store.StoreAddressline1.Trim() : "") + "\r\n");
                this.txtDetail.AppendText((!store.IsStoreAddressline2Null() ? store.StoreAddressline2.Trim() : "") + "\r\n");
                this.txtDetail.AppendText((!store.IsStoreCityNull() ? store.StoreCity.Trim() : "") + ", " + 
                                        (!store.IsStoreStateNull() ? store.StoreState.Trim() : "") + " " +
                                        (!store.IsStoreZipNull() ? store.StoreZip.Trim() : "") + "\r\n");
                this.txtDetail.AppendText((!store.IsContactNameNull() ? store.ContactName.Trim() : "") + ", " + (!store.IsPhoneNumberNull() ? store.PhoneNumber.Trim() : "") + "\r\n");
                this.txtDetail.AppendText((!store.IsRegionDescriptionNull() ? store.RegionDescription.Trim() : "") + 
                                        " (" + (!store.IsRegionNull() ? store.Region.Trim() : "") + "), " + 
                                        (!store.IsDistrictNameNull() ? store.DistrictName.Trim() : "") + 
                                        " (" + (!store.IsDistrictNull() ? store.District.Trim() : "") + ")" + "\r\n");
                this.txtDetail.AppendText("Zone " + (!store.IsZoneNull() ? store.Zone.Trim() : "") + ", " +
                                        "Agent " + (!store.IsAgentNumberNull() ? store.AgentNumber.Trim() : "") + " " +
                                        (!store.IsAgentNameNull() ? store.AgentName.Trim() : "") + "\r\n");
                this.txtDetail.AppendText("Window " + (!store.IsWindowTimeStartNull() ? store.WindowTimeStart.ToString("HH:mm") : "") + " - " +
                                        (!store.IsWindowTimeEndNull() ? store.WindowTimeEnd.ToString("HH:mm") : "") + ", " +
                                        "Del Days " + getDeliveryDays(store) + ", " + 
                                        (!store.IsScanStatusDescrptionNull() ? store.ScanStatusDescrption.Trim() : "") + "\r\n");
                this.txtDetail.AppendText("JA Transit " + (!store.IsStandardTransitNull() ? store.StandardTransit.ToString() : "") + ", " + "OFD " + getOFD(store) + "\r\n");
                this.txtDetail.AppendText("Special Inst: " + (!store.IsSpecialInstructionsNull() ? store.SpecialInstructions.Trim() : "") + "\r\n");
            }
        }
        private string getDeliveryDays(EnterpriseDS.StoreTableRow store) {
            //Return delivery days from the dataset
            string ddays = "";
            
            //Check for overrides
            if(!store.IsIsDeliveryDayMondayNull()) ddays += (store.IsDeliveryDayMonday.Trim() == "Y" ? "M" : "");
            if(!store.IsIsDeliveryDayTuesdayNull()) ddays += (store.IsDeliveryDayTuesday.Trim() == "Y" ? "T" : "");
            if(!store.IsIsDeliveryDayWednesdayNull()) ddays += (store.IsDeliveryDayWednesday.Trim() == "Y" ? "W" : "");
            if(!store.IsIsDeliveryDayThursdayNull()) ddays += (store.IsDeliveryDayThursday.Trim() == "Y" ? "R" : "");
            if(!store.IsIsDeliveryDayFridayNull()) ddays += (store.IsDeliveryDayFriday.Trim() == "Y" ? "F" : "");
            
            //If no overrides, then all days are valid
            if(ddays.Trim().Length == 0) ddays = "MTWRF";
            return ddays;
        }
        private string getOFD(EnterpriseDS.StoreTableRow store) {
            //Return delivery days from the dataset
            string ofd = "";

            //OFD1, OFD2, or NONE
            if(!store.IsIsOutforDeliveryDay1Null())
                ofd += (store.IsOutforDeliveryDay1.Trim() == "Y" ? "DAY1" : "");
            else if(!store.IsIsOutforDeliveryDay2Null())
                ofd += (store.IsOutforDeliveryDay2.Trim() == "Y" ? "DAY2" : "");
            else
                ofd += "";
            return ofd;
        }
        private void reportError(Exception ex) {
            //Error notification
            if(this.Error != null) this.Error(this,new ControlErrorEventArgs(ex));
        }
        #endregion
    }
}
