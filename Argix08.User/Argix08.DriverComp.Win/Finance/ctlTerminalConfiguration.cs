using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
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
    public partial class ctlTerminalConfiguration :UserControl {
        //Members
        private UltraGridSvc mTermGridSvc = null;

        public event ErrorEventHandler ErrorMessage = null;

        //Interface
        public ctlTerminalConfiguration() {
            //Constructor
            try {
                //Required for Windows Form Designer support
                InitializeComponent();
                this.mTermGridSvc = new UltraGridSvc(this.grdMain);
            }
            catch(Exception ex) { throw new ApplicationException("Failed to create new ctlTerminalConfiguration.",ex); }
        }
        public object DataSource {
            get { return this.grdMain.DataSource; }
            set { this.grdMain.DataSource = value; if(value != null) this.grdMain.DataBind(); }
        }

        private void OnControlLoad(object sender,EventArgs e) {
            //Event handler for control load event
            this.Cursor = Cursors.WaitCursor;
            try {
                #region Grid customizations from normal layout (to support cell editing)
                this.grdMain.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                this.grdMain.DisplayLayout.Override.SelectTypeRow = SelectType.Single;
                this.grdMain.DisplayLayout.Override.SelectTypeCell = SelectType.Single;
                this.grdMain.DisplayLayout.TabNavigation = TabNavigation.NextCell;
                this.grdMain.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
                this.grdMain.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
                this.grdMain.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
                this.grdMain.DisplayLayout.Override.MaxSelectedCells = 1;
                this.grdMain.DisplayLayout.Override.CellClickAction = CellClickAction.Edit;
                this.grdMain.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.Edit;
                this.grdMain.DisplayLayout.Bands[0].Columns["AgentName"].SortIndicator = SortIndicator.Ascending;
                #endregion
            }
            catch(Exception ex) { reportError(ex); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnItemClick(object sender,EventArgs e) {
            //Event handler for menu item selections
            try {
                ToolStripItem menu = (ToolStripItem)sender;
                switch(menu.Text) {
                    case "ctxRefresh":
                    case "btnRefresh":
                        this.Cursor = Cursors.WaitCursor;
                        FinanceFactory.RefreshCache();
                        break;
                }
            }
            catch(Exception ex) { reportError(ex); }
            finally { this.Cursor = Cursors.Default; }
        }
        #region Grid Services: OnGridInitializeLayout(), OnGridInitializeRow(), OnGridBeforeRowFilterDropDownPopulate(), OnGridMouseDown(), OnGridCellChange()
        private void OnGridInitializeLayout(object sender,Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
            //
            try {
                switch(e.Layout.Rows.Band.Key) {
                    case "TerminalConfigurationTable":
                        //e.Layout.Bands["TerminalConfigurationTable"].Columns["ID"].ValueList = this.uddEquipType;
                        break;
                }
            }
            catch(Exception ex) { reportError(ex); }
        }
        private void OnGridInitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e) {
	        //
            try {
                switch(e.Row.Band.Key) {
                    case "TerminalConfigurationTable":
                        e.Row.Cells["AgentNumber"].Activation = Activation.NoEdit;
                        e.Row.Cells["AgentName"].Activation = Activation.NoEdit;
                        e.Row.Cells["GLNumber"].Activation = Activation.NoEdit;
                        e.Row.Cells["AdminGLNumber"].Activation = Activation.NoEdit;
                        e.Row.Cells["AdminFee"].Activation = Activation.NoEdit;
                        e.Row.Cells["FSBase"].Activation = Activation.NoEdit;
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
                //Set cell defaults if applicable
                switch(e.Cell.Row.Band.Key) {
                    case "TerminalConfigurationTable":
                        if(e.Cell.Row.IsAddRow) { ; }
                        break;
                }
            }
            catch(Exception ex) { reportError(ex); }
        }
        private void OnGridBeforeRowUpdate(object sender,Infragistics.Win.UltraWinGrid.CancelableRowEventArgs e) {
            //Event handler for data entry row updated
            try {
                //There is no selected row when updating- at a cell level
                string agentNumber = "",agentName = "",glNumber = "",adminGLNumber = "",adminFee = "",fsBase = "";
                switch(e.Row.Band.Key) {
                    case "TerminalConfigurationTable":
                        agentNumber = e.Row.Cells["AgentNumber"].Value.ToString();
                        agentName = e.Row.Cells["AgentName"].Value.ToString();
                        glNumber = e.Row.Cells["GLNumber"].Value.ToString();
                        adminGLNumber = e.Row.Cells["AdminGLNumber"].Value.ToString();
                        adminFee = e.Row.Cells["AdminFee"].Value.ToString();
                        fsBase = e.Row.Cells["FSBase"].Value.ToString();
                        break;
                }

                if(agentNumber.Length > 0 && glNumber.Length > 0 && adminGLNumber.Length > 0 && adminFee.Length > 0 && fsBase.Length > 0) {
                    if(e.Row.IsAddRow) {
                        //Add new terminal configuration
                        //TerminalConfiguration config = this.mTerminals.Add(agentNumber,AgentName,GLNumber,AdminGLNumber,AdminFee,FSBase);
                        //config.Create();
                    }
                    else {
                        //Update existing
                        //TerminalConfiguration config = this.mTerminals.Item(agentNumber);
                        //config.GLNumber = glNumber;
                        //config.AdminGLNumber = adminGLNumber;
                        //config.AdminFee = adminFee;
                        //config.FSBase = fsBase;
                        //config.Update();
                    }
                }
                else
                    e.Cancel = true;
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
