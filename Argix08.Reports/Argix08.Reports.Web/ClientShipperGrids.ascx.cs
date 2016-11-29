//	File:	ClientShipperGrids.cs
//	Author:	J. Heary
//	Date:	04/21/10
//	Desc:	A composite control of two grids for displaying attributes of 
//          associated objects (i.e. client-shipper).
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
using Argix;

[Themeable(true)]
public partial class ClientShipperGrids:System.Web.UI.UserControl {
    //Members
    public event EventHandler AfterClientSelected = null;
    public event EventHandler AfterShipperSelected = null;
    public event EventHandler ControlError = null;

    //Interface
    protected override void OnLoad(EventArgs e) {
        //Event ahndler for control load event
        if(!Page.IsPostBack && !ScriptManager.GetCurrent(Page).IsInAsyncPostBack) {
            this.odsClients.DataBind();
            this.grdClients.SelectedIndex = -1;
            ClientsEnabled = this.grdClients.Rows.Count > 0;
            ShippersEnabled = false;
        }
        base.OnLoad(e);
    }
    [Bindable(false),Category("Behavior"),DefaultValue(""),Description("Gets or sets the client division used for filtering the list of clients.")]
    public string ClientDivision {
        get { return this.hfClientDivision.Value; }
        set { this.hfClientDivision.Value = value; this.grdClients.Columns[2].Visible = this.grdClients.Columns[4].Visible = this.hfClientDivision.Value == ""; }
    }
    [Bindable(false),Category("Behavior"),DefaultValue("false"),Description("Gets or sets the active only used for filtering the list of clients.")]
    public bool ClientActiveOnly { get { return Convert.ToBoolean(this.hfClientActiveOnly.Value); } set { this.hfClientActiveOnly.Value = value.ToString(); } }
    [Bindable(false),Category("Layout"),DefaultValue("100%"),Description("Control width.")]
    public Unit Width { get { return this.tblCtl.Width; } set { this.tblCtl.Width = value; } }
    [Bindable(false),Category("Layout"),DefaultValue("100%"),Description("Control height.")]
    public Unit Height { get { return this.pnlClients.Height; } set { this.pnlClients.Height = this.pnlShippers.Height = value; } }
    [WebBrowsable(true)]
    public FreightType FreightType {
        get { return (this.cboFreightType.SelectedValue == "0" ? FreightType.Regular : FreightType.Returns); }
        set { if(value == FreightType.Returns) this.cboFreightType.SelectedValue = "1"; else this.cboFreightType.SelectedValue = "0"; }
    }
    [Bindable(false),Category("Behavior"),DefaultValue("true"),Description("Gets or sets user selection of shippers as vendors or agents.")]
    public bool FreightTypeEnabled { get { return this.cboFreightType.Enabled; } set { this.cboFreightType.Enabled = value; } }
    public GridViewRow ClientSelectedRow { get { GridViewRow row = null; try { row = this.grdClients.SelectedRow; } catch { row = null; } return row; } }
    public string ClientNumber { get { return (ClientSelectedRow != null) ? this.grdClients.SelectedRow.Cells[1].Text : ""; } }
    public string ClientDivsionNumber { get { return (ClientSelectedRow != null) ? this.grdClients.SelectedRow.Cells[2].Text : ""; } }
    public string ClientName { get { return (ClientSelectedRow != null) ? this.grdClients.SelectedRow.Cells[3].Text : ""; } }
    public string ClientTerminalCode { get { return (ClientSelectedRow != null) ? this.grdClients.SelectedRow.Cells[4].Text : ""; } }
    public bool ClientsEnabled { get { return this.grdClients.Enabled; } set { this.grdClients.Enabled = this.txtFindClient.Enabled = this.imgFindClient.Enabled = value; this.upnlClients.Update(); } }
    public GridViewRow ShipperSelectedRow { get { GridViewRow row = null; try { row = this.grdShippers.SelectedRow; } catch { row = null; } return row; } }
    public string ShipperNumber { get { return (ShipperSelectedRow != null) ? this.grdShippers.SelectedRow.Cells[1].Text : ""; } }
    public string ShipperName { get { return (ShipperSelectedRow != null) ? this.grdShippers.SelectedRow.Cells[2].Text : ""; } }
    public bool ShippersEnabled { get { return this.grdShippers.Enabled; } set { this.grdShippers.Enabled = this.txtFindShipper.Enabled = this.imgFindShipper.Enabled = value; this.upnlShippers.Update(); } }

    protected void OnClientSelected(object sender,EventArgs e) {
        //Event handler for change in selected client
        try {
            //Forward to client
            OnFreightTypeChanged(this.cboFreightType, EventArgs.Empty);
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
    protected void OnFreightTypeChanged(object sender,EventArgs e) {
        //Event hanler for change in freight type
        try {
            this.grdShippers.DataBind();
            this.grdShippers.SelectedIndex = -1;
            ShippersEnabled = this.grdShippers.Rows.Count > 0;
        }
        catch(Exception ex) { reportError(ex); }
    }
    protected void OnShipperSelected(object sender,EventArgs e) {
        //Event handler for change in selected vendor
        try {
            //Forward to client
            if(this.AfterShipperSelected != null) this.AfterShipperSelected(sender,e);
        }
        catch(Exception ex) { reportError(ex); }
    }
    protected void OnFindShipper(object sender,ImageClickEventArgs e) {
        //Event handler for vendor search
        OnShipperSearch(sender,EventArgs.Empty);
    }
    protected void OnShipperSearch(object sender,EventArgs e) {
        //Event handler for vendor search
        findRow(this.grdShippers,1,this.txtFindShipper.Text);
        OnShipperSelected(this.grdShippers,EventArgs.Empty);
        ScriptManager.RegisterStartupScript(this.txtFindShipper,typeof(TextBox),"ScrollShippers","scroll('" + this.grdShippers.ClientID + "', '" + this.pnlShippers.ClientID + "', '" + this.txtFindShipper.Text + "');",true);
    }
    #region Local Services: findRow(), reportError()
    private void findRow(GridView grid,int colIndex,string searchWord) {
        //Event handler for change in search text value
        GridViewRow oRow = null,oRowSimiliar = null,oRowMatch = null;
        string sCellText = "";
        long lCellValue = 0,lSearchValue = 0;
        int iLword = 0,iL = 0,iRows = 0,i = 0,j = 0;
        bool bASC = true,bIsNumeric = false,bHigher = false;

        //Check for rows
        if(grid.Rows.Count == 0)
            return;

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
                    try { lSearchValue = Convert.ToInt64(searchWord); } catch(FormatException) { lSearchValue = 0; }
                    if(bASC) {
                        if(lSearchValue == lCellValue)
                            oRowMatch = oRow;
                        else if(lSearchValue > lCellValue)
                            oRowSimiliar = oRow;
                    } else {
                        if(lSearchValue == lCellValue)
                            oRowMatch = oRow;
                        else if(lSearchValue < lCellValue)
                            oRowSimiliar = oRow;
                    }
                } else {
                    sCellText = oRow.Cells[colIndex].Text;
                    iL = sCellText.Length;
                    if(iL > 0) {
                        //Compare a substring of the cell text with the search word
                        for(j = 1; j <= iL; j++) {
                            if(sCellText.Substring(0,j).ToUpper() == searchWord.Substring(0,j).ToUpper()) {
                                if(j == iLword) {
                                    //Exact match
                                    oRowMatch = oRow;
                                    break;
                                } else {
                                    if(j == iL) {
                                        //Close match
                                        oRowSimiliar = oRow;
                                        break;
                                    }
                                }
                            } else {
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
