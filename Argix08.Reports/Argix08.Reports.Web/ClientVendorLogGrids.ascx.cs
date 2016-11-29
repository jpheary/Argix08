//	File:	ClientVendorLogGrids.cs
//	Author:	J. Heary
//	Date:	10/01/10
//	Desc:	A composite control of two grids for displaying attributes of 
//          client-vendor.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

[Themeable(true)]
public partial class ClientVendorLogGrids:System.Web.UI.UserControl {
    //Members
    public event EventHandler AfterClientSelected = null;
    public event EventHandler AfterVendorLogEntrySelected = null;
    public event EventHandler ControlError = null;

    //Interface
    protected override void OnLoad(EventArgs e) {
        //Event handler for Load event
        if(!Page.IsPostBack && !ScriptManager.GetCurrent(Page).IsInAsyncPostBack) {
            this.odsClients.DataBind();
            this.grdClients.SelectedIndex = -1;
            ClientsEnabled = this.grdClients.Rows.Count > 0;
            VendorLogEnabled = false;
        }
        base.OnLoad(e);
    }
    [Bindable(false),Category("Layout"),DefaultValue("100%"),Description("Control width.")]
    public Unit Width { get { return this.tblCtl.Width; } set { this.tblCtl.Width = value; } }
    [Bindable(false),Category("Layout"),DefaultValue("100%"),Description("Control height.")]
    public Unit Height { get { return this.pnlClients.Height; } set { this.pnlClients.Height = this.pnlVendorLog.Height = value; } }
    [Bindable(false),Category("Behavior"),Description("Gets or sets the start date for vendor log entries.")]
    public DateTime StartDate { get { return DateTime.Parse(this.hfStartDate.Value); } set { this.hfStartDate.Value = value.ToShortDateString(); this.upnlVendorLog.Update(); } }
    [Bindable(false),Category("Behavior"),Description("Gets or sets end date for vendor log entries.")]
    public DateTime EndDate { get { return DateTime.Parse(this.hfEndDate.Value); } set { this.hfEndDate.Value = value.ToShortDateString(); this.upnlVendorLog.Update(); } }
    [Bindable(false),Category("Behavior"),DefaultValue("Blue"),Description("BackColor for the control header.")]
    public System.Drawing.Color HeaderBackColor { get { return this.thrClients.BackColor; } set { this.thrClients.BackColor = this.thrVendorLog.BackColor = value; } }
    [Bindable(false),Category("Behavior"),DefaultValue("White"),Description("ForeColor for the control header.")]
    public System.Drawing.Color HeaderForeColor { get { return this.thrClients.ForeColor; } set { this.thrClients.ForeColor = this.thrVendorLog.ForeColor = value; } }
    [Bindable(false),Category("Data"),DefaultValue(""),Description("Gets or sets the client division used for filtering the list of clients.")]
    public string ClientDivision {
        get { return this.hfClientDivision.Value; }
        set { this.hfClientDivision.Value = value; this.grdClients.Columns[2].Visible = this.grdClients.Columns[4].Visible = this.hfClientDivision.Value == ""; }
    }
    [Bindable(false),Category("Behavior"),DefaultValue("true"),Description("Gets or sets the active only used for filtering the list of clients.")]
    public bool ClientActiveOnly { get { return Convert.ToBoolean(this.hfClientActiveOnly.Value); } set { this.hfClientActiveOnly.Value = value.ToString(); } }
    [Bindable(false),Category("Data"),DefaultValue(-1),Description("Gets or sets the index of the selected row in the client grid.")]
    public int ClientSelectedIndex { get { return this.grdClients.SelectedIndex; } set { this.grdClients.SelectedIndex = value; } }
    [Bindable(false),Category("Behavior"),DefaultValue("True"),Description("False to disable the client grid.")]
    public bool ClientsEnabled { get { return this.grdClients.Enabled; } set { this.grdClients.Enabled = this.txtFindClient.Enabled = this.imgFindClient.Enabled = value; this.upnlClients.Update(); } }
    [Bindable(false),Category("Data"),DefaultValue(-1),Description("Gets or sets the index of the selected row in the vendor grid.")]
    public int VendorLogSelectedIndex { get { return this.grdVendorLog.SelectedIndex; } set { this.grdVendorLog.SelectedIndex = value; } }
    [Bindable(false),Category("Behavior"),DefaultValue("True"),Description("False to disable the vendor log grid.")]
    public bool VendorLogEnabled { get { return this.grdVendorLog.Enabled; } set { this.grdVendorLog.Enabled = this.txtFindLogEntry.Enabled = this.imgFindLogEntry.Enabled = value; this.upnlVendorLog.Update(); } }
    
    public GridViewRow ClientSelectedRow { get { GridViewRow row = null; try { row = this.grdClients.SelectedRow; } catch { row = null; } return row; } }
    public string ClientNumber { get { return (ClientSelectedRow != null) ? this.grdClients.SelectedRow.Cells[1].Text : ""; } }
    public string ClientDivsionNumber { get { return (ClientSelectedRow != null) ? this.grdClients.SelectedRow.Cells[2].Text : ""; } }
    public string ClientName { get { return (ClientSelectedRow != null) ? this.grdClients.SelectedRow.Cells[3].Text : ""; } }
    public GridViewRow VendorLogSelectedRow { get { GridViewRow row = null; try { row = this.grdVendorLog.SelectedRow; } catch { row = null; } return row; } }
    public VendorLogDS VendorLogEntries {
        get {
            VendorLogDS vendorLog = new VendorLogDS();
            foreach(GridViewRow row in SelectedRows) {
                DataKey dataKey = (DataKey)this.grdVendorLog.DataKeys[row.RowIndex];
                string _manifestID = dataKey["ID"].ToString();
                DateTime _pickupDate = DateTime.Parse(dataKey["PickupDate"].ToString());
                int _pickupNum = Int32.Parse(dataKey["PickupNumber"].ToString());
                vendorLog.VendorLogTable.AddVendorLogTableRow(_manifestID,"","",_pickupDate,_pickupNum);
            }
            vendorLog.AcceptChanges();
            return vendorLog;
        }
    }

    protected void OnClientSelected(object sender,EventArgs e) {
        //Event handler for change in selected client
        try {
            //Clear vendor selection; forward event to client
            this.grdVendorLog.DataBind();
            this.grdVendorLog.SelectedIndex = -1;
            VendorLogEnabled = this.grdVendorLog.Rows.Count > 0;
            if(this.AfterClientSelected != null) this.AfterClientSelected(sender,e);
        }
        catch(Exception ex) { reportError(ex); }
    }
    protected void OnFindClient(object sender,ImageClickEventArgs e) {
        //Event handler for client search
        OnClientSearch(sender,EventArgs.Empty);
    }
    protected void OnClientSearch(object sender,EventArgs e) {
        //Event handler for client search
        findRow(this.grdClients,1,this.txtFindClient.Text);
        OnClientSelected(this.grdClients,EventArgs.Empty);
        ScriptManager.RegisterStartupScript(this.txtFindClient,typeof(TextBox),"ScrollClients","scroll('" + this.grdClients.ClientID + "', '" + this.pnlClients.ClientID + "', '" + this.txtFindClient.Text + "');",true);
    }
    protected void OnVendorLogEntrySelected(object sender,EventArgs e) {
        //Event handler for change in selected vendor
        try {
            //Forward to client
            if(this.AfterVendorLogEntrySelected != null) this.AfterVendorLogEntrySelected(sender,e);
        }
        catch(Exception ex) { reportError(ex); }
    }
    protected void OnFindVendorLogEntry(object sender,ImageClickEventArgs e) {
        //Event handler for vendor search
        OnVendorLogSearch(sender,EventArgs.Empty);
    }
    protected void OnVendorLogSearch(object sender,EventArgs e) {
        //Event handler for vendor search
        findRow(this.grdVendorLog,1,this.txtFindLogEntry.Text);
        OnVendorLogEntrySelected(this.grdVendorLog,EventArgs.Empty);
        ScriptManager.RegisterStartupScript(this.txtFindLogEntry,typeof(TextBox),"ScrollVendorLog","scroll('" + this.grdVendorLog.ClientID + "', '" + this.pnlVendorLog.ClientID + "', '" + this.txtFindLogEntry.Text + "');",true);
    }
    protected void OnAfterClientSelected(object sender,EventArgs e) {
        //Event handler for change in selected client
        if(this.AfterClientSelected != null) this.AfterClientSelected(sender,e);
    }
    protected void OnAfterVendorLogEntrySelected(object sender,EventArgs e) {
        //Event handler for change in selected vendor
        if(this.AfterVendorLogEntrySelected != null) this.AfterVendorLogEntrySelected(sender,e);
    }
    #region Local Services: SelectedRows(), findRow(), reportError()
    private GridViewRow[] SelectedRows {
        get {
            GridViewRow[] rows=new GridViewRow[] { };
            int i=0;
            foreach(GridViewRow row in this.grdVendorLog.Rows) {
                bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
                if(isChecked) i++;
            }
            if(i > 0) {
                rows = new GridViewRow[i];
                int j=0;
                foreach(GridViewRow row in this.grdVendorLog.Rows) {
                    bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
                    if(isChecked) rows[j++] = row;
                }
            }
            return rows;
        }
    }
    private void findRow(GridView grid,int colIndex,string searchWord) {
        //Event handler for change in search text value
        GridViewRow oRow = null,oRowSimiliar = null,oRowMatch = null;
        string sCellText = "";
        long lCellValue = 0,lSearchValue = 0;
        int iLword = 0,iL = 0,iRows = 0,i = 0,j = 0;
        bool bASC = true,bIsNumeric = false,bHigher = false;

        //Validate
        if(grid.Rows.Count == 0) return;

        //Get specifics for search word and grid
        iLword = searchWord.Length;
        iRows = grid.Rows.Count;
        if(colIndex < grid.Columns.Count && searchWord.Length > 0) {
            //Initial search conditions
            bASC = (grid.SortDirection == SortDirection.Ascending);
            bIsNumeric = (grid.Columns[colIndex].GetType() == Type.GetType("System.Int32"));
            i = 0;
            while(i < iRows) {
                //Get next row, cell value, and cell length
                oRow = grid.Rows[i];
                if(bIsNumeric) {
                    lCellValue = Convert.ToInt64(oRow.Cells[colIndex].Text);
                    try { lSearchValue = Convert.ToInt64(searchWord); }
                    catch(FormatException) { lSearchValue = 0; }
                    if(bASC) {
                        if(lSearchValue == lCellValue)
                            oRowMatch = oRow;
                        else if(lSearchValue > lCellValue)
                            oRowSimiliar = oRow;
                    }
                    else {
                        if(lSearchValue == lCellValue)
                            oRowMatch = oRow;
                        else if(lSearchValue < lCellValue)
                            oRowSimiliar = oRow;
                    }
                }
                else {
                    sCellText = oRow.Cells[colIndex].Text;
                    iL = sCellText.Length;
                    if(iL > 0) {
                        //Compare a substring of the cell text with the search word
                        for(j = 1;j <= iL;j++) {
                            if(sCellText.Substring(0,j).ToUpper() == searchWord.Substring(0,j).ToUpper()) {
                                //Look for exact match or closest match
                                if(j == iLword) { 
                                    oRowMatch = oRow; break; 
                                }
                                else {
                                    if(j == iL) { oRowSimiliar = oRow; break; }
                                }
                            }
                            else {
                                //Is search word alphabetically higher than cell?
                                if(bASC)
                                    bHigher = (searchWord.ToUpper().ToCharArray()[j - 1] > sCellText.ToUpper().ToCharArray()[j - 1]);
                                else
                                    bHigher = (searchWord.ToUpper().ToCharArray()[j - 1] < sCellText.ToUpper().ToCharArray()[j - 1]);
                                if(bHigher)
                                    oRowSimiliar = oRow;
                                break;
                            }
                        }
                    }
                }
                if(oRowMatch != null) break;
                i++;
            }
            //Select match or closest row
            if(iRows > 0 && oRowMatch == null)
                oRowMatch = (oRowSimiliar != null) ? oRowSimiliar : grid.Rows[0];
            grid.SelectedIndex = oRowMatch.RowIndex;
        }
    }
    private void reportError(Exception ex) {
        //Report an exception to the client
        try { if(this.ControlError != null) this.ControlError(this,EventArgs.Empty); }
        catch(Exception) { }
    }
    #endregion
}
