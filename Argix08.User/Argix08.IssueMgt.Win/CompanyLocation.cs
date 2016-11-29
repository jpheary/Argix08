//	File:		CompanyLocation.cs
//	Author:	    jheary
//	Date:		03/08/09
//	Desc:		CompanyLocation custom control for selecting a company and an 
//              affiliated company location.
//	Rev:		
//	---------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Argix.Customers {
    //
    public partial class CompanyLocation :UserControl {
        //Members
        private StoreDS mStoreDS = null;
        private ToolTip mStoreDetail = null;
        private bool mReadOnly = false;

        /// <summary>The list name for '' in the location scope selector.</summary>
        public const string SCOPE_NONE = "";
        /// <summary>The list name for 'Districts' in the location scope selector.</summary>
        public const string SCOPE_DISTRICTS = "Districts";
        /// <summary>The list name for 'Regions' in the location scope selector.</summary>
        public const string SCOPE_REGIONS = "Regions";
        /// <summary>The list name for 'Agents' in the location scope selector.</summary>
        public const string SCOPE_AGENTS = "Agents";
        /// <summary>The list name for 'Stores' in the location scope selector.</summary>
        public const string SCOPE_STORES = "Stores";
        /// <summary>The list name for 'Substores' in the location scope selector.</summary>
        public const string SCOPE_SUBSTORES = "Substores";

        /// <summary>Occurs when the value of the Argix.Customers.CompanyLocation.LocationNumber property changes.</summary>
        public event EventHandler CompanyLocationChanged = null;
        /// <summary>Occurs when an error occurs in the Argix.Customers.CompanyLocation control.</summary>
        public event ControlErrorEventHandler Error = null;

        //Interface
        /// <summary>Creates a new instance of the Argix.Customers.CompanyLocation control.</summary>
        public CompanyLocation() {
            //Constructor
            try {
                InitializeComponent();
                this.mStoreDS = new StoreDS();
                this.mStoreDetail = new ToolTip();
                this.mStoreDetail.IsBalloon = false;
                this.mStoreDetail.InitialDelay = 50;
                this.mStoreDetail.ShowAlways = true;
            }
            catch(Exception ex) { throw new ControlException("Unexpected error while creating new CompanyLocation control instance.",ex); }
        }
        #region Accessors/Modifiers: [Members...]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string CompanyText { set { this.cboCompany.Text = value; } }
        
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets or sets the value of the member property specified by the System.Windows.Forms.ListControl.ValueMember property of the company control.")]
        [HelpKeywordAttribute("Argix.Customers.CompanyLocation.CompanySelectedValue")]
        [Localizable(false)]
        public object CompanySelectedValue { 
            get {
                string number = this.cboCompany.SelectedValue.ToString();
                CompanyDS.CompanyTableRow[] rows = (CompanyDS.CompanyTableRow[])this.mCompanyDS.CompanyTable.Select("Number='" + number + "'");
                return rows[0].CompanyID; 
            } 
            set {
                this.cboCompany.SelectedIndex = -1;
                if(value != null) {
                    CompanyDS.CompanyTableRow[] rows = (CompanyDS.CompanyTableRow[])this.mCompanyDS.CompanyTable.Select("CompanyID=" + Convert.ToInt32(value));
                    this.cboCompany.SelectedValue = rows[0].Number;
                }
                OnCompanySelected(null,EventArgs.Empty); 
            } 
        }
        private bool ShouldSerializeCompanySelectedValue() { return false; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string ScopeText { set { this.cboScope.Text = value; } }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets or sets the currently selected location scope.")]
        [HelpKeywordAttribute("Argix.Customers.CompanyLocation.LocationScope")]
        public string LocationScope { 
            get { return this.cboScope.SelectedItem != null ? this.cboScope.SelectedItem.ToString() : SCOPE_NONE; } 
            set {
                string scope = this.cboScope.SelectedItem != null ? this.cboScope.SelectedItem.ToString() : SCOPE_NONE;
                switch(value) {
                    case SCOPE_NONE:    
                        this.cboScope.SelectedItem = null; 
                        break;
                    case SCOPE_DISTRICTS: 
                    case SCOPE_REGIONS: 
                    case SCOPE_AGENTS: 
                    case SCOPE_STORES: 
                    case SCOPE_SUBSTORES: 
                        this.cboScope.SelectedItem = value; 
                        break;
                    default: 
                        this.cboScope.SelectedItem = null;
                        break;
                }
                if(value != scope) OnScopeChanged(null,EventArgs.Empty); 
            } 
        }
        private bool ShouldSerializeLocationScope() { return false; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public string LocationText { set { this.cboLocation.Text = value; } }
        
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets or sets the number associated with the location control.")]
        [HelpKeywordAttribute("Argix.Customers.CompanyLocation.LocationNumber")]
        public string LocationNumber {
            get {
                string locationNumber = "";
                if(this.cboScope.SelectedItem != null) {
                    switch(this.cboScope.SelectedItem.ToString()) {
                        case SCOPE_DISTRICTS:
                        case SCOPE_REGIONS:
                        case SCOPE_AGENTS:
                            locationNumber = (this.cboLocation.SelectedValue != null ? this.cboLocation.SelectedValue.ToString() : "");
                            break;
                        case SCOPE_STORES:
                        case SCOPE_SUBSTORES:
                            locationNumber = this.txtStore.Text;
                            break;
                    }
                }
                return locationNumber;
            }
            set {
                if(this.cboScope.SelectedItem != null) {
                    switch(this.cboScope.SelectedItem.ToString()) {
                        case SCOPE_DISTRICTS:
                        case SCOPE_REGIONS:
                        case SCOPE_AGENTS:
                            this.cboLocation.SelectedValue = (value.Length>0?value:"All");
                            break;
                        case SCOPE_STORES:
                        case SCOPE_SUBSTORES:
                            this.txtStore.Text = value;
                            showStoreDetail();
                            break;
                    }
                }
            }
        }
        private bool ShouldSerializeLocationNumber() { return false; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets the contact for the current store location (AS/400 value).")]
        [Localizable(false)]
        [HelpKeywordAttribute("Argix.Customers.CompanyLocation.StoreContact")]
        public Contact StoreContact { 
            get {
                Contact contact = new Contact();
                if(this.mStoreDS != null && this.mStoreDS.StoreTable.Rows.Count > 0) {
                    contact.FullName = this.mStoreDS.StoreTable[0].ContactName.Trim();
                    string[] names = contact.FullName.Split(new string[] { " " },StringSplitOptions.RemoveEmptyEntries);
                    contact.FirstName = names.Length>0 ? names[0] : "";
                    contact.LastName = names.Length > 1 ? names[1] : "";
                    contact.Phone = this.mStoreDS.StoreTable[0].PhoneNumber;
                }
                return contact;
            } 
        }
        private bool ShouldSerializeStoreContact() { return false; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets details for the current store.")]
        [Localizable(false)]
        [HelpKeywordAttribute("Argix.Customers.CompanyLocation.StoreDetail")]
        public string StoreDetail { get { return this.txtStoreDetail.Text; } }
        private bool ShouldSerializeStoreDetail() { return false; }
        
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets or sets the read only state of the control.")]
        [Category("Behavior")]
        [DefaultValue(typeof(System.Boolean),"False")]
        [ReadOnly(false)]
        [Localizable(false)]
        [HelpKeywordAttribute("Argix.Customers.CompanyLocation.ReadOnly")]
        public bool ReadOnly {
            get { return this.mReadOnly; }
            set {
                this.mReadOnly = value;
                if(this.mReadOnly) {
                    this.cboCompany.DropDownStyle = this.cboScope.DropDownStyle = this.cboLocation.DropDownStyle = ComboBoxStyle.Simple;
                    this.cboCompany.Enabled = this.cboScope.Enabled = this.cboLocation.Enabled = this.txtStore.Enabled = false;
                }
                else {
                    this.cboCompany.DropDownStyle = this.cboScope.DropDownStyle = this.cboLocation.DropDownStyle = ComboBoxStyle.DropDownList;
                    this.cboCompany.Enabled = this.cboScope.Enabled = this.cboLocation.Enabled = this.txtStore.Enabled = true;
                }
            } 
        }
        public void ResetReadOnly() { this.mReadOnly = false; }
        private bool ShouldSerializeReadOnly() { return this.mReadOnly != false; }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets or sets visibility for the store detail textbox.")]
        [Category("Appearance")]
        [DefaultValue(typeof(System.Boolean),"True")]
        [ReadOnly(false)]
        [Localizable(false)]
        [HelpKeywordAttribute("Argix.Customers.CompanyLocation.StoreDetailVisible")]
        public bool StoreDetailVisible {
            get { return this.txtStoreDetail.Visible; }
            set { 
                this._lblStoreDetail.Visible = this.txtStoreDetail.Visible = value;
                this.btnStoreDetail.Visible = !value;
            }
        }
        public void ResetStoreDetailVisible() { this.txtStoreDetail.Visible = true; }
        private bool ShouldSerializeStoreDetailVisible() { return this.txtStoreDetail.Visible != true; }
        
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets or sets font bold for the control label.")]
        [Category("Appearance")]
        [DefaultValue(typeof(System.Boolean),"False")]
        [ReadOnly(false)]
        [Localizable(false)]
        [HelpKeywordAttribute("Argix.Customers.CompanyLocation.FontBold")]
        public bool FontBold { 
            get { return this._lblCompany.Font.Bold; }
            set { this._lblCompany.Font = this._lblScope.Font = this._lblLocation.Font = new Font(this._lblCompany.Font,(value ? FontStyle.Bold : FontStyle.Regular)); } 
        }
        public void ResetFontBold() { this._lblCompany.Font = this._lblScope.Font = this._lblLocation.Font = new Font(this._lblCompany.Font,FontStyle.Regular); }
        private bool ShouldSerializeFontBold() { return (this._lblCompany.Font.Bold != false); }
        #endregion
        private void OnControlLoad(object sender,EventArgs e) {
            //Event handler for load event
            this.Cursor = Cursors.WaitCursor;
            try {
                if(this.mReadOnly)
                    this.mCompanyDS.Merge(CustomerProxy.GetCompanies());
                else
                    this.mCompanyDS.Merge(CustomerProxy.GetCompanies(true));
                this.cboCompany.Enabled = !this.mReadOnly && this.cboCompany.Items.Count > 0;
                this.cboScope.Enabled = this.cboLocation.Enabled = this.txtStore.Enabled = this.btnStoreDetail.Enabled = this.txtStoreDetail.Enabled = false;
                this.cboCompany.SelectedIndex = -1;
                this.cboLocation.DataSource = null;
            }
            catch(Exception ex) { reportError(new ControlException("Unexpected error while loading CompanyLocation control.", ex)); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnCompanySelected(object sender,EventArgs e) {
            //Event handler for change in comapny
            try {
                //Validate
                if(this.cboCompany.SelectedValue == null) return;
                
                //Set applicable scopes for companies (i.e. client, pre-paid vendor)
                string number = this.cboCompany.SelectedValue.ToString();
                CompanyDS.CompanyTableRow[] rows = (CompanyDS.CompanyTableRow[])this.mCompanyDS.CompanyTable.Select("Number='" + number + "'");
                this.cboScope.Items.Clear();
                this.cboScope.Items.AddRange(new object[] { SCOPE_AGENTS });
                //this.cboScope.SelectedItem = SCOPE_AGENTS;
                if(rows[0].CompanyType == "20") {
                    this.cboScope.Items.AddRange(new object[] { SCOPE_DISTRICTS,SCOPE_REGIONS,SCOPE_STORES,SCOPE_SUBSTORES });
                    //this.cboScope.SelectedItem = SCOPE_STORES; 
                }
                
                //Update locations since company changed
                this.cboScope.Enabled = !this.mReadOnly;
                OnScopeChanged(null,EventArgs.Empty);
            }
            catch(Exception ex) { reportError(new ControlException("Unexpected error when company selected.",ex)); }
        }
        private void OnScopeChanged(object sender,EventArgs e) {
            //Event handler for change in scope
            try {
                //Validate
                if(this.cboScope.SelectedItem == null) return;

                //Prepare a location selector for the specified scope
                string scope = this.cboScope.SelectedItem.ToString();
                this.cboLocation.Visible = (scope == SCOPE_DISTRICTS || scope == SCOPE_REGIONS || scope == SCOPE_AGENTS);
                this.txtStore.Visible = (scope == SCOPE_STORES || scope == SCOPE_SUBSTORES);
                this.txtStore.Enabled = !this.mReadOnly;
                this.txtStore.Text = "";
                this.btnStoreDetail.Enabled = (scope == SCOPE_STORES || scope == SCOPE_SUBSTORES);
                this.txtStoreDetail.Enabled = (scope == SCOPE_STORES || scope == SCOPE_SUBSTORES);
                showStoreDetail();

                this.cboLocation.DataSource = null;
                switch(scope) {
                    case SCOPE_DISTRICTS:   loadDistricts(); break;
                    case SCOPE_REGIONS:     loadRegions(); break;
                    case SCOPE_STORES:      
                    case SCOPE_SUBSTORES:
                        break;
                    case SCOPE_AGENTS:      loadAgents(); break;
                }
                this.cboLocation.Enabled = !this.mReadOnly && this.cboLocation.Items.Count > 0;
                if(this.cboLocation.Items.Count > 0) this.cboLocation.SelectedIndex = 0;
                OnCompanyLocationChanged(null,EventArgs.Empty);
            }
            catch(Exception ex) { reportError(new ControlException("Unexpected error when location scope changed.",ex)); }
        }
        private void OnCompanyLocationChanged(object sender,EventArgs e) {
            //Event handler for change in location (i.e. cboLocation.SelectionChangeCommitted, txtStore.TextChanged)
            //Raise a CompanyLocationChanged event with the change in location
            try {
                string location = null;
                string scope = this.cboScope.SelectedItem.ToString();
                switch(scope) {
                    case SCOPE_DISTRICTS:
                    case SCOPE_REGIONS:
                    case SCOPE_AGENTS:
                        if(this.cboLocation.SelectedValue != null) 
                            location = this.cboLocation.SelectedValue.ToString() == "All" ? "" : this.cboLocation.SelectedValue.ToString();
                        break;
                    case SCOPE_STORES:
                    case SCOPE_SUBSTORES:
                        this.mStoreDS = new StoreDS();
                        this.txtStoreDetail.Clear();
                        break;
                }
                if(this.CompanyLocationChanged != null) this.CompanyLocationChanged(this,EventArgs.Empty);
            }
            catch(Exception ex) { reportError(new ControlException("Unexpected error when company location changed.",ex)); }
        }
        private void OnStoreKeyUp(object sender,KeyEventArgs e) {
            //Event handler for store textbox key up event
            try {
                if(e.KeyCode == Keys.Enter) {
                    string scope = this.cboScope.SelectedItem.ToString();
                    string location = "";
                    switch(scope) {
                        case SCOPE_DISTRICTS:
                        case SCOPE_REGIONS:
                        case SCOPE_AGENTS:
                            break;
                        case SCOPE_STORES:
                        case SCOPE_SUBSTORES:
                            location = this.txtStore.Text;
                            showStoreDetail();
                            break;
                    }
                    if(this.CompanyLocationChanged != null) this.CompanyLocationChanged(this,EventArgs.Empty);
                }
            }
            catch(Exception ex) { reportError(new ControlException("Unexpected error when company location changed.",ex)); }
        }
        private void OnViewStoreDetail(object sender,EventArgs e) {
            //Event handler for view store detail button clicked
            try {
                this.mStoreDetail.ToolTipTitle = this.cboCompany.Text.Trim() + " #" + this.txtStore.Text;
                this.mStoreDetail.Show(getStoreDetailString(),this.btnStoreDetail,0,this.btnStoreDetail.Height,60000);
            }
            catch(Exception ex) { reportError(new ControlException("Unexpected error while viewing store detail.",ex)); }
        }
        private void OnHideStoreDetail(object sender,EventArgs e) {
            this.mStoreDetail.Hide(this.btnStoreDetail); 
        }
        #region Data Services: loadDistricts(), loadRegions(), loadAgents()
        private void loadDistricts() {
            //Load a list of district selections
            try {
                LocationDS locationDS = new LocationDS();
                LocationDS _ds = CustomerProxy.GetDistricts(this.cboCompany.SelectedValue.ToString().PadLeft(6,'0').Substring(3,3));
                for(int i = 0;i < _ds.LocationTable.Rows.Count;i++) {
                    _ds.LocationTable[i].Location = _ds.LocationTable[i].Location.ToString().Trim();
                    _ds.LocationTable[i].LocationName = _ds.LocationTable[i].LocationName.ToString().Trim();
                }
                locationDS.Merge(_ds,true);
                this.cboLocation.DisplayMember = "LocationTable.LocationName";
                this.cboLocation.ValueMember = "LocationTable.Location";
                this.cboLocation.DataSource = locationDS;
            }
            catch(Exception ex) { throw new ControlException("Unexpected error while loading company districts.",ex); }
        }
        private void loadRegions() {
            //Load a list of region sslections
            try {
                LocationDS locationDS = new LocationDS();
                LocationDS _ds = CustomerProxy.GetRegions(this.cboCompany.SelectedValue.ToString().PadLeft(6,'0').Substring(3,3));
                System.Collections.Hashtable table = new System.Collections.Hashtable();
                for(int i = 0;i < _ds.LocationTable.Rows.Count;i++) {
                    string region = _ds.LocationTable[i].Location.ToString().Trim();
                    if(region.Length == 0)
                        _ds.LocationTable[i].Delete();
                    else {
                        if(table.ContainsKey(region))
                            _ds.LocationTable[i].Delete();
                        else {
                            table.Add(region,_ds.LocationTable[i].LocationName.ToString().Trim());
                            _ds.LocationTable[i].Location = region;
                            _ds.LocationTable[i].LocationName = _ds.LocationTable[i].LocationName.ToString().Trim();
                        }
                    }
                }
                locationDS.Merge(_ds,true);
                this.cboLocation.DisplayMember = "LocationTable.LocationName";
                this.cboLocation.ValueMember = "LocationTable.Location";
                this.cboLocation.DataSource = locationDS;
            }
            catch(Exception ex) { throw new ControlException("Unexpected error while loading company regions.",ex); }
        }
        private void loadAgents() {
            //Load a list of agent selections
            try {
                AgentDS agentDS = new AgentDS();
                AgentDS ds = CustomerProxy.GetAgents(this.cboCompany.SelectedValue.ToString().PadLeft(6,'0').Substring(3,3));
                agentDS.Merge(ds);
                this.cboLocation.DisplayMember = "AgentTable.AgentSummary";
                this.cboLocation.ValueMember = "AgentTable.AgentNumber";
                this.cboLocation.DataSource = agentDS;
            }
            catch(Exception ex) { throw new ControlException("Unexpected error while loading company agents.",ex); }
        }
        #endregion
        #region Local Services: showStoreDetail(), getStoreDetailString(), getDeliveryDays(), getOFD(), reportError()
        private void showStoreDetail() {
            //
            this.mStoreDS = new StoreDS();
            this.txtStoreDetail.Clear();
            if(this.cboCompany.SelectedValue != null && this.txtStore.Text.Length > 0) {
                StoreDS ds = new StoreDS();
                switch(this.cboScope.SelectedItem.ToString()) {
                    case SCOPE_STORES: ds.Merge(CustomerProxy.GetStoreDetail(Convert.ToInt32(CompanySelectedValue),Convert.ToInt32(this.txtStore.Text))); break;
                    case SCOPE_SUBSTORES: ds.Merge(CustomerProxy.GetStoreDetail(Convert.ToInt32(CompanySelectedValue),this.txtStore.Text)); break;
                }
                if(ds.StoreTable.Rows.Count > 0) {
                    this.mStoreDS = ds;
                    this.txtStoreDetail.Text = getStoreDetailString();
                }
            }
        }
        private string getStoreDetailString() {
            //Return a string of store detail
            StringBuilder detail = new StringBuilder();
            if(this.mStoreDS.StoreTable.Rows.Count > 0) {
                StoreDS.StoreTableRow store = this.mStoreDS.StoreTable[0];
                detail.AppendLine(store.StoreName.Trim() + " (store #" + store.StoreNumber.ToString() + "; substore #" + store.SubStoreNumber.Trim() + ")");
                detail.AppendLine((!store.IsStoreAddressline1Null() ? store.StoreAddressline1.Trim() : ""));
                detail.AppendLine((!store.IsStoreAddressline2Null() ? store.StoreAddressline2.Trim() : ""));
                detail.AppendLine((!store.IsStoreCityNull() ? store.StoreCity.Trim() : "") + ", " + 
                                                (!store.IsStoreStateNull() ? store.StoreState.Trim() : "") + " " +
                                                (!store.IsStoreZipNull() ? store.StoreZip.Trim() : ""));
                detail.AppendLine((!store.IsContactNameNull() ? store.ContactName.Trim() : "") + ", " + (!store.IsPhoneNumberNull() ? store.PhoneNumber.Trim() : ""));
                detail.AppendLine((!store.IsRegionDescriptionNull() ? store.RegionDescription.Trim() : "") + 
                                                " (" + (!store.IsRegionNull() ? store.Region.Trim() : "") + "), " + 
                                                (!store.IsDistrictNameNull() ? store.DistrictName.Trim() : "") + 
                                                " (" + (!store.IsDistrictNull() ? store.District.Trim() : "") + ")");
                detail.AppendLine("Zone " + (!store.IsZoneNull() ? store.Zone.Trim() : "") + ", " +
                                                "Agent " + (!store.IsAgentNumberNull() ? store.AgentNumber.Trim() : "") + " " +
                                                (!store.IsAgentNameNull() ? store.AgentName.Trim() : ""));
                detail.AppendLine("Window " + (!store.IsWindowTimeStartNull() ? store.WindowTimeStart.ToString("HH:mm") : "") + " - " +
                                                (!store.IsWindowTimeEndNull() ? store.WindowTimeEnd.ToString("HH:mm") : "") + ", " +
                                                "Del Days " + getDeliveryDays(store) + ", " + 
                                                (!store.IsScanStatusDescrptionNull() ? store.ScanStatusDescrption.Trim() : ""));
                detail.AppendLine("JA Transit " + (!store.IsStandardTransitNull() ? store.StandardTransit.ToString() : "") + ", " + "OFD " + getOFD(store));
                detail.AppendLine("Special Inst: " + (!store.IsSpecialInstructionsNull() ? store.SpecialInstructions.Trim() : ""));
            }
            return detail.ToString();
        }
        private string getDeliveryDays(StoreDS.StoreTableRow store) {
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
        private string getOFD(StoreDS.StoreTableRow store) {
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
