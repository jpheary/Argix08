//	File:	ctlRates.cs
//	Author:	jheary
//	Date:	08/27/08
//	Desc:	User control for managing Driver Rates configuration data.
//	Rev:	02/26/09 (jph)- revised methods OnGridInitializeRow(), OnGridCellChange(), 
//                          and OnGridBeforeRowUpdate() to use new DriverRatesDS fields
//                          MaximumAmount, MaximumTriggerField, & MaximumTriggerValue.
//          07/17/09 (jph)- updated grids to reflect revised fields types in DriverRatesDS;
//                          applied horizontal alignments to numeric cells. 
//	---------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix;
using Argix.Enterprise;
using Argix.Windows;

namespace Argix.Finance {
    //
    public partial class ctlRates :UserControl {
        //Members
        private DriverRates mRates = null;
        public event ErrorEventHandler ErrorMessage = null;

        //Interface
        public ctlRates() {
            //Constructor
            try {
                //Required for Windows Form Designer support
                InitializeComponent();
            }
            catch(Exception ex) { throw new ApplicationException("Failed to create new ctlRates.",ex); }
        }
        public DriverRates Rates {
            get { return this.mRates; }
            set {
                this.Cursor = Cursors.WaitCursor;
                try {
                    this.mRates = value;
                    if(this.mRates != null) {
                        this.grdMileageRates.DataSource = this.mRates.Rates;
                        this.grdMileageRates.DataBind();

                        this.grdUnitRates.DataSource = this.mRates.Rates;
                        this.grdUnitRates.DataBind();

                        this.grdMileageRouteRates.DataSource = this.mRates.Rates;
                        this.grdMileageRouteRates.DataBind();

                        this.grdUnitRouteRates.DataSource = this.mRates.Rates;
                        this.grdUnitRouteRates.DataBind();
                    }
                    else {
                        //Prefer to set to null, but the column filters disappear; so set
                        //to an instance, and block add new if this.mRates==null;
                        this.grdMileageRates.DataSource = this.grdUnitRates.DataSource = this.grdMileageRouteRates.DataSource = this.grdUnitRouteRates.DataSource = new DriverRatesDS();
                    }
                }
                catch(Exception ex) { throw new ApplicationException("Unexpected error while setting rates reference.",ex); }
                finally { this.Cursor = Cursors.Default; }
            }
        }

        private void OnLoad(object sender,EventArgs e) {
            //Event handler for control load event
            this.Cursor = Cursors.WaitCursor;
            try {
                this.grdMileageRates.Visible = this.grdUnitRates.Visible = this.grdMileageRouteRates.Visible = this.grdUnitRouteRates.Visible = false;
                #region Grid customizations from normal layout (to support cell editing)
                this.grdMileageRates.UpdateMode = UpdateMode.OnRowChangeOrLostFocus & UpdateMode.OnUpdate;
                this.grdMileageRates.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                this.grdMileageRates.DisplayLayout.Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdMileageRates.DisplayLayout.Override.SelectTypeRow = SelectType.Single;
                this.grdMileageRates.DisplayLayout.Override.SelectTypeCell = SelectType.Single;
                this.grdMileageRates.DisplayLayout.TabNavigation = TabNavigation.NextCell;
                this.grdMileageRates.DisplayLayout.Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
                this.grdMileageRates.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
                this.grdMileageRates.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
                this.grdMileageRates.DisplayLayout.Override.MaxSelectedCells = 1;
                this.grdMileageRates.DisplayLayout.Override.CellClickAction = CellClickAction.Edit;
                this.grdMileageRates.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.Edit;
                this.grdMileageRates.DisplayLayout.Bands[0].Columns["AgentNumber"].SortIndicator = SortIndicator.Ascending;

                this.grdUnitRates.UpdateMode = UpdateMode.OnRowChangeOrLostFocus | UpdateMode.OnUpdate;
                this.grdUnitRates.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                this.grdUnitRates.DisplayLayout.Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdUnitRates.DisplayLayout.Override.SelectTypeRow = SelectType.Single;
                this.grdUnitRates.DisplayLayout.Override.SelectTypeCell = SelectType.Single;
                this.grdUnitRates.DisplayLayout.TabNavigation = TabNavigation.NextCell;
                this.grdUnitRates.DisplayLayout.Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
                this.grdUnitRates.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
                this.grdUnitRates.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
                this.grdUnitRates.DisplayLayout.Override.MaxSelectedCells = 1;
                this.grdUnitRates.DisplayLayout.Override.CellClickAction = CellClickAction.Edit;
                this.grdUnitRates.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.Edit;
                this.grdUnitRates.DisplayLayout.Bands[0].Columns["AgentNumber"].SortIndicator = SortIndicator.Ascending;

                this.grdMileageRouteRates.UpdateMode = UpdateMode.OnRowChangeOrLostFocus | UpdateMode.OnUpdate;
                this.grdMileageRouteRates.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                this.grdMileageRouteRates.DisplayLayout.Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdMileageRouteRates.DisplayLayout.Override.SelectTypeRow = SelectType.Single;
                this.grdMileageRouteRates.DisplayLayout.Override.SelectTypeCell = SelectType.Single;
                this.grdMileageRouteRates.DisplayLayout.TabNavigation = TabNavigation.NextCell;
                this.grdMileageRouteRates.DisplayLayout.Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
                this.grdMileageRouteRates.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
                this.grdMileageRouteRates.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
                this.grdMileageRouteRates.DisplayLayout.Override.MaxSelectedCells = 1;
                this.grdMileageRouteRates.DisplayLayout.Override.CellClickAction = CellClickAction.Edit;
                this.grdMileageRouteRates.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.Edit;
                this.grdMileageRouteRates.DisplayLayout.Bands[0].Columns["AgentNumber"].SortIndicator = SortIndicator.Ascending;

                this.grdUnitRouteRates.UpdateMode = UpdateMode.OnRowChangeOrLostFocus | UpdateMode.OnUpdate;
                this.grdUnitRouteRates.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                this.grdUnitRouteRates.DisplayLayout.Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdUnitRouteRates.DisplayLayout.Override.SelectTypeRow = SelectType.Single;
                this.grdUnitRouteRates.DisplayLayout.Override.SelectTypeCell = SelectType.Single;
                this.grdUnitRouteRates.DisplayLayout.TabNavigation = TabNavigation.NextCell;
                this.grdUnitRouteRates.DisplayLayout.Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
                this.grdUnitRouteRates.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
                this.grdUnitRouteRates.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
                this.grdUnitRouteRates.DisplayLayout.Override.MaxSelectedCells = 1;
                this.grdUnitRouteRates.DisplayLayout.Override.CellClickAction = CellClickAction.Edit;
                this.grdUnitRouteRates.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.Edit;
                this.grdUnitRouteRates.DisplayLayout.Bands[0].Columns["AgentNumber"].SortIndicator = SortIndicator.Ascending;
                #endregion
                this.uddAgentType.DataSource = new EnterpriseService("").GetEnterpriseAgents();
                this.uddAgentType.DisplayMember = "Description";
                this.uddAgentType.ValueMember = "AgentNumber";

                this.uddEquipType.DataSource = new EnterpriseService("").GetDriverEquipmentTypes();
                this.uddEquipType.DisplayMember = "Description";
                this.uddEquipType.ValueMember = "ID";

                this.btnRateType.SelectedIndex = 0;
            }
            catch(Exception ex) { reportError(ex); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnMenuClick(object sender,EventArgs e) {
            //Event handler for menu item selections
            try {
                ToolStripDropDownItem menu = (ToolStripDropDownItem)sender;
                switch(menu.Text) {
                    case "Refresh":
                        this.Cursor = Cursors.WaitCursor; 
                        this.mRates.Refresh();
                        break;
                }
            }
            catch(Exception ex) { reportError(ex); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnItemClicked(object sender,ToolStripItemClickedEventArgs e) {
            //Event handler for rate toolbar item clicked
            try {
                switch(e.ClickedItem.Name) {
                    case "btnRefresh": this.ctxRefresh.PerformClick(); break;
                }
            }
            catch(Exception ex) { reportError(ex); }
        }
        private void OnRateTypeSelected(object sender,System.EventArgs e) {
            //Event handler for change in combobox terminal selection
            try {
                this.grdMileageRates.Visible = this.grdUnitRates.Visible = this.grdMileageRouteRates.Visible = this.grdUnitRouteRates.Visible = false;
                this.grdMileageRates.Dock = this.grdUnitRates.Dock = this.grdMileageRouteRates.Dock = this.grdUnitRouteRates.Dock = DockStyle.None;
                UltraGrid grid = null;
                switch(this.btnRateType.SelectedItem.ToString()) {
                    case "Mileage Rates":           grid = this.grdMileageRates; break;
                    case "Unit Rates":              grid = this.grdUnitRates; break;
                    case "Mileage Route Rates":     grid = this.grdMileageRouteRates; break;
                    case "Unit Route Rates":        grid = this.grdUnitRouteRates; break;
                }
                grid.Visible = true;
                grid.Dock = DockStyle.Fill;
            }
            catch(Exception ex) { reportError(ex); }
        }
        #region Grid Services: OnGridInitializeLayout(), OnGridInitializeRow(), OnGridBeforeRowFilterDropDownPopulate(), OnGridMouseDown(), OnGridCellChange()
        private void OnGridInitializeLayout(object sender,Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
            //
            try {
                switch(e.Layout.Rows.Band.Key) {
                    case "RateMileageTable":
                        e.Layout.Bands["RateMileageTable"].Columns["AgentNumber"].ValueList = this.uddAgentType;
                        e.Layout.Bands["RateMileageTable"].Columns["EquipmentTypeID"].ValueList = this.uddEquipType;
                        break;
                    case "RateUnitTable":
                        e.Layout.Bands["RateUnitTable"].Columns["AgentNumber"].ValueList = this.uddAgentType;
                        e.Layout.Bands["RateUnitTable"].Columns["EquipmentTypeID"].ValueList = this.uddEquipType;
                        break;
                    case "RateMileageRouteTable":
                        e.Layout.Bands["RateMileageRouteTable"].Columns["AgentNumber"].ValueList = this.uddAgentType;
                        break;
                    case "RateUnitRouteTable":
                        e.Layout.Bands["RateUnitRouteTable"].Columns["AgentNumber"].ValueList = this.uddAgentType;
                        break;
                }
            }
            catch(Exception ex) { reportError(ex); }
        }
        private void OnGridInitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e) {
	        //
            try {
                bool canEdit = ((this.mRates != null) && (e.Row.IsAddRow || (this.mRates.RatesDate.CompareTo(Convert.ToDateTime(e.Row.Cells["EffectiveDate"].Value)) == 0)));
                switch(e.Row.Band.Key) {
                    case "RateMileageTable":
                        e.Row.Cells["ID"].Activation = Activation.NoEdit;
                        e.Row.Cells["AgentNumber"].Activation = Activation.NoEdit;
                        e.Row.Cells["EquipmentTypeID"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        e.Row.Cells["EffectiveDate"].Activation = Activation.NoEdit;
                        e.Row.Cells["Mile"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        e.Row.Cells["BaseRate"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        e.Row.Cells["Rate"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        break;
                    case "RateUnitTable":
                        e.Row.Cells["ID"].Activation = Activation.NoEdit;
                        e.Row.Cells["AgentNumber"].Activation = Activation.NoEdit;
                        e.Row.Cells["EquipmentTypeID"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        e.Row.Cells["EffectiveDate"].Activation = Activation.NoEdit;
                        e.Row.Cells["DayRate"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        e.Row.Cells["MultiTripRate"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        e.Row.Cells["StopRate"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        e.Row.Cells["CartonRate"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        e.Row.Cells["PalletRate"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        e.Row.Cells["PickupCartonRate"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        e.Row.Cells["MinimumAmount"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        e.Row.Cells["MaximumAmount"].Activation = Activation.NoEdit;
                        e.Row.Cells["MaximumTriggerField"].Activation = Activation.NoEdit;
                        e.Row.Cells["MaximumTriggerValue"].Activation = Activation.NoEdit;
                        e.Row.Cells["FSBase"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        break;
                    case "RateMileageRouteTable":
                        e.Row.Cells["ID"].Activation = Activation.NoEdit;
                        e.Row.Cells["AgentNumber"].Activation = Activation.NoEdit;
                        e.Row.Cells["Route"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        e.Row.Cells["EffectiveDate"].Activation = Activation.NoEdit;
                        e.Row.Cells["Mile"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        e.Row.Cells["BaseRate"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        e.Row.Cells["Rate"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        e.Row.Cells["Status"].Activation = Activation.NoEdit;
                        break;
                    case "RateUnitRouteTable":
                        e.Row.Cells["ID"].Activation = Activation.NoEdit;
                        e.Row.Cells["AgentNumber"].Activation = Activation.NoEdit;
                        e.Row.Cells["Route"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        e.Row.Cells["EffectiveDate"].Activation = Activation.NoEdit;
                        e.Row.Cells["DayRate"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        e.Row.Cells["MultiTripRate"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        e.Row.Cells["StopRate"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        e.Row.Cells["CartonRate"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        e.Row.Cells["PalletRate"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        e.Row.Cells["PickupCartonRate"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        e.Row.Cells["MinimumAmount"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        e.Row.Cells["MaximumAmount"].Activation = Activation.NoEdit;
                        e.Row.Cells["MaximumTriggerField"].Activation = Activation.NoEdit;
                        e.Row.Cells["MaximumTriggerValue"].Activation = Activation.NoEdit;
                        e.Row.Cells["FSBase"].Activation = (canEdit ? Activation.AllowEdit : Activation.NoEdit);
                        e.Row.Cells["Status"].Activation = Activation.NoEdit;
                        break;
                }
            }
            catch(Exception ex) { reportError(ex); }
        }
        private void OnGridBeforeRowFilterDropDownPopulate(object sender,Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs e) {
            //Removes only (Blanks) and Non Blanks default filter
            try {
                e.ValueList.ValueListItems.Remove(3);
                e.ValueList.ValueListItems.Remove(2);
                e.ValueList.ValueListItems.Remove(1);
            }
            catch(Exception ex) { reportError(ex); }
        }
        private void OnGridMouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event
            try {
                //Set menu and toolbar services
                UltraGrid grid = (UltraGrid)sender;
                grid.Focus();
                UIElement oUIElement = grid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X,e.Y));
                if(oUIElement != null) {
                    object oContext = oUIElement.GetContext(typeof(UltraGridRow));
                    if(oContext != null) {
                        if(e.Button == MouseButtons.Left) {
                            //OnDragDropMouseDown(sender, e);
                        }
                        else if(e.Button == MouseButtons.Right) {
                            UltraGridRow oRow = (UltraGridRow)oContext;
                            if(!oRow.Selected) grid.Selected.Rows.Clear();
                            oRow.Selected = true;
                            oRow.Activate();
                        }
                    }
                    else {
                        //Deselect rows in the white space of the grid or deactivate the active   
                        //row when in a scroll region to prevent double-click action
                        if(oUIElement.Parent.GetType() == typeof(DataAreaUIElement))
                            grid.Selected.Rows.Clear();
                        else if(oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement))
                            if(grid.Selected.Rows.Count > 0) grid.Selected.Rows[0].Activated = false;
                    }
                }
            }
            catch(Exception ex) { reportError(ex); }
        }
        private void OnGridCellChange(object sender,Infragistics.Win.UltraWinGrid.CellEventArgs e) {
            //Event handler for change in a cell value
            try {
                //Set effective date for new entries
                switch(e.Cell.Row.Band.Key) {
                    case "RateMileageTable":
                        if(e.Cell.Row.IsAddRow) {
                            if(e.Cell.Row.Cells["AgentNumber"].Value.ToString() == "") {
                                e.Cell.Row.Cells["ID"].Value = 0;
                                e.Cell.Row.Cells["AgentNumber"].Value = this.mRates.AgentNumber;
                                e.Cell.Row.Cells["EffectiveDate"].Value = this.mRates.RatesDate;
                                e.Cell.Row.Cells["Mile"].Value = 0.0;
                                e.Cell.Row.Cells["BaseRate"].Value = 0.0;
                                e.Cell.Row.Cells["Rate"].Value = 0.0;
                            }
                        }
                        break;
                    case "RateUnitTable":
                        if(e.Cell.Row.IsAddRow) {
                            if(e.Cell.Row.Cells["AgentNumber"].Value.ToString() == "") {
                                e.Cell.Row.Cells["ID"].Value = 0;
                                e.Cell.Row.Cells["AgentNumber"].Value = this.mRates.AgentNumber;
                                e.Cell.Row.Cells["EffectiveDate"].Value = this.mRates.RatesDate;
                                e.Cell.Row.Cells["DayRate"].Value = 0.0;
                                e.Cell.Row.Cells["MultiTripRate"].Value = 0.0;
                                e.Cell.Row.Cells["StopRate"].Value = 0.0;
                                e.Cell.Row.Cells["CartonRate"].Value = 0.0;
                                e.Cell.Row.Cells["PalletRate"].Value = 0.0;
                                e.Cell.Row.Cells["PickupCartonRate"].Value = 0.0;
                                e.Cell.Row.Cells["MinimumAmount"].Value = 0.0;
                                e.Cell.Row.Cells["MaximumAmount"].Value = 0.0;
                                e.Cell.Row.Cells["MaximumTriggerField"].Value = "";
                                e.Cell.Row.Cells["MaximumTriggerValue"].Value = 0;
                                e.Cell.Row.Cells["FSBase"].Value = 0.0;
                            }
                        }
                        break;
                    case "RateMileageRouteTable":
                        if(e.Cell.Row.IsAddRow) {
                            if(e.Cell.Row.Cells["AgentNumber"].Value.ToString() == "") {
                                e.Cell.Row.Cells["ID"].Value = 0;
                                e.Cell.Row.Cells["AgentNumber"].Value = this.mRates.AgentNumber;
                                e.Cell.Row.Cells["EffectiveDate"].Value = this.mRates.RatesDate;
                                e.Cell.Row.Cells["Mile"].Value = 0.0;
                                e.Cell.Row.Cells["BaseRate"].Value = 0.0;
                                e.Cell.Row.Cells["Rate"].Value = 0.0;
                                e.Cell.Row.Cells["Status"].Value = "1";
                            }
                        }
                        break;
                    case "RateUnitRouteTable":
                        if(e.Cell.Row.IsAddRow) {
                            if(e.Cell.Row.Cells["AgentNumber"].Value.ToString() == "") {
                                e.Cell.Row.Cells["ID"].Value = 0;
                                e.Cell.Row.Cells["AgentNumber"].Value = this.mRates.AgentNumber;
                                e.Cell.Row.Cells["EffectiveDate"].Value = this.mRates.RatesDate;
                                e.Cell.Row.Cells["DayRate"].Value = 0.0;
                                e.Cell.Row.Cells["MultiTripRate"].Value = 0.0;
                                e.Cell.Row.Cells["StopRate"].Value = 0.0;
                                e.Cell.Row.Cells["CartonRate"].Value = 0.0;
                                e.Cell.Row.Cells["PalletRate"].Value = 0.0;
                                e.Cell.Row.Cells["PickupCartonRate"].Value = 0.0;
                                e.Cell.Row.Cells["MinimumAmount"].Value = 0.0;
                                e.Cell.Row.Cells["MaximumAmount"].Value = 0.0;
                                e.Cell.Row.Cells["MaximumTriggerField"].Value = "";
                                e.Cell.Row.Cells["MaximumTriggerValue"].Value = 0;
                                e.Cell.Row.Cells["FSBase"].Value = 0.0;
                                e.Cell.Row.Cells["Status"].Value = "1";
                            }
                        }
                        break;
                }
            }
            catch(Exception ex) { reportError(ex); }
        }
        private void OnGridKeyUp(object sender,System.Windows.Forms.KeyEventArgs e) {
            //Event handler for key up event
            if(this.mRates == null)
                return;
            UltraGrid grid = (UltraGrid)sender;
            if(e.KeyCode == System.Windows.Forms.Keys.Enter) {
                //Update row on Enter
                grid.ActiveRow.Update();
                e.Handled = true;
            }
            else if(e.KeyCode == System.Windows.Forms.Keys.Escape) {
                //Cancel update on ESC
                grid.ActiveRow.CancelUpdate();
                e.Handled = true;
            }
        }
        private void OnGridBeforeRowUpdate(object sender,Infragistics.Win.UltraWinGrid.CancelableRowEventArgs e) {
            //Event handler for data entry row updated
            try {
                //There is no selected row when updating- at a cell level
                int id = 0;
                string agent = "",route = "", maxTrigFld="";
                DateTime date;
                double mile = 0.0;
                decimal baseRate = 0.0M,rate = 0.0M;
                int equip = 0,status = 1, maxTrigVal=0;
                decimal dayRate = 0.0M,tripRate = 0.0M,stopRate = 0.0M,ctnRate = 0.0M,palletRate = 0.0M,pickupCtnRate = 0.0M,minAmt = 0.0M,maxAmt = 0.0M,fsBase = 0.0M;
                switch(e.Row.Band.Key) {
                    case "RateMileageTable":
                        id = Convert.ToInt32(e.Row.Cells["ID"].Value);
                        agent = e.Row.Cells["AgentNumber"].Value.ToString();
                        equip = Convert.ToInt32(e.Row.Cells["EquipmentTypeID"].Value);
                        date = Convert.ToDateTime(e.Row.Cells["EffectiveDate"].Value);
                        mile = Convert.ToDouble(e.Row.Cells["Mile"].Value);
                        baseRate = Convert.ToDecimal(e.Row.Cells["BaseRate"].Value);
                        rate = Convert.ToDecimal(e.Row.Cells["Rate"].Value);
                        if(agent.Length > 0 && equip > 0 && date > DateTime.MinValue) {
                            if(e.Row.IsAddRow)
                                new DriverRatingService("").CreateMileageRate(agent,equip,date,mile,baseRate,rate);
                            else
                                new DriverRatingService("").UpdateMileageRate(id,equip,mile,baseRate,rate);
                        }
                        else
                            e.Cancel = true;
                        break;
                    case "RateUnitTable":
                        id = Convert.ToInt32(e.Row.Cells["ID"].Value);
                        agent = e.Row.Cells["AgentNumber"].Value.ToString();
                        equip = Convert.ToInt32(e.Row.Cells["EquipmentTypeID"].Value);
                        date = Convert.ToDateTime(e.Row.Cells["EffectiveDate"].Value);
                        dayRate = Convert.ToDecimal(e.Row.Cells["DayRate"].Value);
                        tripRate = Convert.ToDecimal(e.Row.Cells["MultiTripRate"].Value);
                        stopRate = Convert.ToDecimal(e.Row.Cells["StopRate"].Value);
                        ctnRate = Convert.ToDecimal(e.Row.Cells["CartonRate"].Value);
                        palletRate = Convert.ToDecimal(e.Row.Cells["PalletRate"].Value);
                        pickupCtnRate = Convert.ToDecimal(e.Row.Cells["PickupCartonRate"].Value);
                        minAmt = Convert.ToDecimal(e.Row.Cells["MinimumAmount"].Value);
                        maxAmt = Convert.ToDecimal(e.Row.Cells["MaximumAmount"].Value);
                        maxTrigFld = e.Row.Cells["MaximumTriggerField"].Value.ToString();
                        maxTrigVal = Convert.ToInt32(e.Row.Cells["MaximumTriggerValue"].Value);
                        fsBase = Convert.ToDecimal(e.Row.Cells["FSBase"].Value);
                        if(agent.Length > 0 && equip > 0 && date > DateTime.MinValue) {
                            if(e.Row.IsAddRow)
                                new DriverRatingService("").CreateUnitRate(agent,equip,date,dayRate,tripRate,stopRate,ctnRate,palletRate,pickupCtnRate,minAmt,maxAmt,maxTrigFld,maxTrigVal,fsBase);
                            else
                                new DriverRatingService("").UpdateUnitRate(id,equip,dayRate,tripRate,stopRate,ctnRate,palletRate,pickupCtnRate,minAmt,maxAmt,maxTrigFld,maxTrigVal,fsBase);
                        }
                        else
                            e.Cancel = true;
                        break;
                    case "RateMileageRouteTable":
                        id = Convert.ToInt32(e.Row.Cells["ID"].Value);
                        agent = e.Row.Cells["AgentNumber"].Value.ToString();
                        route = e.Row.Cells["Route"].Value.ToString();
                        date = Convert.ToDateTime(e.Row.Cells["EffectiveDate"].Value);
                        mile = Convert.ToDouble(e.Row.Cells["Mile"].Value);
                        baseRate = Convert.ToDecimal(e.Row.Cells["BaseRate"].Value);
                        rate = Convert.ToDecimal(e.Row.Cells["Rate"].Value);
                        status = Convert.ToInt32(e.Row.Cells["Status"].Value);
                        if(agent.Length > 0 && route.Length > 0 && date > DateTime.MinValue) {
                            if(e.Row.IsAddRow)
                                new DriverRatingService("").CreateMileageRouteRate(agent,route,date,mile,baseRate,rate,status);
                            else
                                new DriverRatingService("").UpdateMileageRouteRate(id,route,mile,baseRate,rate,status);
                        }
                        else
                            e.Cancel = true;
                        break;
                    case "RateUnitRouteTable":
                        id = Convert.ToInt32(e.Row.Cells["ID"].Value);
                        agent = e.Row.Cells["AgentNumber"].Value.ToString();
                        route = e.Row.Cells["Route"].Value.ToString();
                        date = Convert.ToDateTime(e.Row.Cells["EffectiveDate"].Value);
                        dayRate = Convert.ToDecimal(e.Row.Cells["DayRate"].Value);
                        tripRate = Convert.ToDecimal(e.Row.Cells["MultiTripRate"].Value);
                        stopRate = Convert.ToDecimal(e.Row.Cells["StopRate"].Value);
                        ctnRate = Convert.ToDecimal(e.Row.Cells["CartonRate"].Value);
                        palletRate = Convert.ToDecimal(e.Row.Cells["PalletRate"].Value);
                        pickupCtnRate = Convert.ToDecimal(e.Row.Cells["PickupCartonRate"].Value);
                        minAmt = Convert.ToDecimal(e.Row.Cells["MinimumAmount"].Value);
                        maxAmt = Convert.ToDecimal(e.Row.Cells["MaximumAmount"].Value);
                        maxTrigFld = e.Row.Cells["MaximumTriggerField"].Value.ToString();
                        maxTrigVal = Convert.ToInt32(e.Row.Cells["MaximumTriggerValue"].Value);
                        fsBase = Convert.ToDecimal(e.Row.Cells["FSBase"].Value);
                        status = Convert.ToInt32(e.Row.Cells["Status"].Value);
                        if(agent.Length > 0 && route.Length > 0 && date > DateTime.MinValue) {
                            if(e.Row.IsAddRow)
                                new DriverRatingService("").CreateUnitRouteRate(agent,route,date,dayRate,tripRate,stopRate,ctnRate,palletRate,pickupCtnRate,minAmt,maxAmt,maxTrigFld,maxTrigVal,fsBase,status);
                            else
                                new DriverRatingService("").UpdateUnitRouteRate(id,route,dayRate,tripRate,stopRate,ctnRate,palletRate,pickupCtnRate,minAmt,maxAmt,maxTrigFld,maxTrigVal,fsBase,status);
                        }
                        else
                            e.Cancel = true;
                        break;
                }
                this.mRates.Refresh();
            }
            catch(Exception ex) { reportError(ex); }
        }
        #endregion
        #region Local Services: reportError()
        private void reportError(Exception ex) {
            if(this.ErrorMessage != null) this.ErrorMessage(this,new ErrorEventArgs(ex));
        }
        #endregion
    }
}
