//	File:	ClientDivisionGrids.cs
//	Author:	J. Heary
//	Date:	10/04/10
//	Desc:	A composite control of two grids for displaying attributes of 
//          client-division.
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
public partial class ClientDivisionGrids:System.Web.UI.UserControl {
    //Members
    public event EventHandler AfterClientSelected = null;
    public event EventHandler AfterDivisionSelected = null;
    public event EventHandler ControlError = null;

    //Interface
    protected override void OnLoad(EventArgs e) {
        //Event handler for Load event
        if(!Page.IsPostBack && !ScriptManager.GetCurrent(Page).IsInAsyncPostBack) {
            this.odsClients.DataBind();
            this.grdClients.SelectedIndex = -1;
            ClientsEnabled = this.grdClients.Rows.Count > 0;
            DivisionsEnabled = false;
        }
        base.OnLoad(e);
    }
    [Bindable(false),Category("Layout"),DefaultValue("100%"),Description("Control width.")]
    public Unit Width { get { return this.tblCtl.Width; } set { this.tblCtl.Width = value; } }
    [Bindable(false),Category("Layout"),DefaultValue("100%"),Description("Control height.")]
    public Unit Height { get { return this.pnlClients.Height; } set { this.pnlClients.Height = this.pnlDivisions.Height = value; } }
    [Bindable(false),Category("Behavior"),DefaultValue(""),Description("Gets the client division used for filtering the list of clients.")]
    public string ClientDivision { get { return this.hfClientDivision.Value; } }
    [Bindable(false),Category("Behavior"),DefaultValue("false"),Description("Gets or sets the active only used for filtering the list of clients.")]
    public bool ClientActiveOnly { get { return Convert.ToBoolean(this.hfClientActiveOnly.Value); } set { this.hfClientActiveOnly.Value = value.ToString(); } }
    [Bindable(false),Category("Behavior"),DefaultValue("Blue"),Description("BackColor for the control header.")]
    public System.Drawing.Color HeaderBackColor { get { return this.thrClients.BackColor; } set { this.thrClients.BackColor = this.thrDivisions.BackColor = value; } }
    [Bindable(false),Category("Behavior"),DefaultValue("White"),Description("ForeColor for the control header.")]
    public System.Drawing.Color HeaderForeColor { get { return this.thrClients.ForeColor; } set { this.thrClients.ForeColor = this.thrDivisions.ForeColor = value; } }
    [Bindable(false),Category("Data"),DefaultValue(-1),Description("Gets or sets the index of the selected row in the client grid.")]
    public int ClientSelectedIndex { get { return this.grdClients.SelectedIndex; } set { this.grdClients.SelectedIndex = value; } }
    [Bindable(false),Category("Behavior"),DefaultValue("True"),Description("False to disable the client grid.")]
    public bool ClientsEnabled { get { return this.grdClients.Enabled; } set { this.grdClients.Enabled = this.txtFindClient.Enabled = this.imgFindClient.Enabled = value; this.upnlClients.Update(); } }
    [Bindable(false),Category("Data"),DefaultValue(-1),Description("Gets or sets the index of the selected row in the division grid.")]
    public int DivisionSelectedIndex { get { return this.grdDivisions.SelectedIndex; } set { this.grdDivisions.SelectedIndex = value; } }
    [Bindable(false),Category("Behavior"),DefaultValue("True"),Description("False to disable the division grid.")]
    public bool DivisionsEnabled { get { return this.grdDivisions.Enabled; } set { this.grdDivisions.Enabled = this.txtFindDivision.Enabled = this.imgFindDivision.Enabled = value; this.upnlDivisions.Update(); } }
    
    public GridViewRow ClientSelectedRow { get { GridViewRow row = null; try { row = this.grdClients.SelectedRow; } catch { row = null; } return row; } }
    public string ClientNumber { get { return (ClientSelectedRow != null) ? this.grdClients.SelectedRow.Cells[1].Text : ""; } }
    public string ClientName { get { return (ClientSelectedRow != null) ? this.grdClients.SelectedRow.Cells[2].Text : ""; } }
    public GridViewRow DivisionSelectedRow { get { GridViewRow row = null; try { row = this.grdDivisions.SelectedRow; } catch { row = null; } return row; } }
    public string DivisionNumber { get { return (DivisionSelectedRow != null) ? this.grdDivisions.SelectedRow.Cells[1].Text : ""; } }
    public string DivisionName { get { return (DivisionSelectedRow != null) ? this.grdDivisions.SelectedRow.Cells[2].Text : ""; } }

    protected void OnClientSelected(object sender,EventArgs e) {
        //Event handler for change in selected client
        try {
            //Clear division selection; forward event to client
            this.grdDivisions.DataBind();
            this.grdDivisions.SelectedIndex = -1;
            DivisionsEnabled = this.grdDivisions.Rows.Count > 0;
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
    protected void OnDivisionSelected(object sender,EventArgs e) {
        //Event handler for change in selected division
        try {
            //Forward to client
            if(this.AfterDivisionSelected != null) this.AfterDivisionSelected(sender,e);
        }
        catch(Exception ex) { reportError(ex); }
    }
    protected void OnFindDivision(object sender,ImageClickEventArgs e) {
        //Event handler for division search
        OnDivisionSearch(sender,EventArgs.Empty);
    }
    protected void OnDivisionSearch(object sender,EventArgs e) {
        //Event handler for division search
        findRow(this.grdDivisions,1,this.txtFindDivision.Text);
        OnDivisionSelected(this.grdDivisions,EventArgs.Empty);
        ScriptManager.RegisterStartupScript(this.txtFindDivision,typeof(TextBox),"ScrollDivisions","scroll('" + this.grdDivisions.ClientID + "', '" + this.pnlDivisions.ClientID + "', '" + this.txtFindDivision.Text + "');",true);
    }
    protected void OnAfterClientSelected(object sender,EventArgs e) {
        //Event handler for change in selected client
        if(this.AfterClientSelected != null) this.AfterClientSelected(sender,e);
    }
    protected void OnAfterDivisionSelected(object sender,EventArgs e) {
        //Event handler for change in selected division
        if(this.AfterDivisionSelected != null) this.AfterDivisionSelected(sender,e);
    }
    #region Local Services: findRow(), reportError()
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
