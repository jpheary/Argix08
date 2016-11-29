//	File:	ClientAgentGrids.cs
//	Author:	J. Heary
//	Date:	10/27/10
//	Desc:	A composite control of two grids for displaying attributes of 
//          clients and agents.
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
public partial class ClientAgentGrids:System.Web.UI.UserControl {
    //Members
    public event EventHandler AfterClientSelected = null;
    public event EventHandler AfterAgentSelected = null;
    public event EventHandler ControlError = null;

    //Interface
    protected override void OnLoad(EventArgs e) {
        //Event handler for Load event
        if(!Page.IsPostBack && !ScriptManager.GetCurrent(Page).IsInAsyncPostBack) {
            this.odsClients.DataBind();
            this.grdClients.SelectedIndex = -1;
            ClientsEnabled = this.grdClients.Rows.Count > 0;
            this.odsAgents.DataBind();
            this.grdAgents.SelectedIndex = -1;
            AgentsEnabled = this.grdAgents.Rows.Count > 0;
        }
        base.OnLoad(e);
    }
    [Bindable(false),Category("Layout"),DefaultValue("100%"),Description("Control width.")]
    public Unit Width { get { return this.tblCtl.Width; } set { this.tblCtl.Width = value; } }
    [Bindable(false),Category("Layout"),DefaultValue("100%"),Description("Control height.")]
    public Unit Height { get { return this.pnlClients.Height; } set { this.pnlClients.Height = this.pnlAgents.Height = value; } }
    [Bindable(false),Category("Behavior"),DefaultValue(""),Description("Gets or sets the client division used for filtering the list of clients.")]
    public string ClientDivision { 
        get { return this.hfClientDivision.Value; } 
        set { this.hfClientDivision.Value = value; this.grdClients.Columns[2].Visible = this.grdClients.Columns[4].Visible = this.hfClientDivision.Value == ""; } 
    }
    [Bindable(false),Category("Behavior"),DefaultValue("false"),Description("Gets or sets the active only used for filtering the list of clients.")]
    public bool ClientActiveOnly { get { return Convert.ToBoolean(this.hfClientActiveOnly.Value); } set { this.hfClientActiveOnly.Value = value.ToString(); } }
    [Bindable(false),Category("Behavior"),DefaultValue("Blue"),Description("BackColor for the control header.")]
    public System.Drawing.Color HeaderBackColor { get { return this.thrClients.BackColor; } set { this.thrClients.BackColor = this.thrAgents.BackColor = value; } }
    [Bindable(false),Category("Behavior"),DefaultValue("White"),Description("ForeColor for the control header.")]
    public System.Drawing.Color HeaderForeColor { get { return this.thrClients.ForeColor; } set { this.thrClients.ForeColor = this.thrAgents.ForeColor = value; } }
    [Bindable(false),Category("Data"),DefaultValue(-1),Description("Gets or sets the index of the selected row in the client grid.")]
    public int ClientSelectedIndex { get { return this.grdClients.SelectedIndex; } set { this.grdClients.SelectedIndex = value; } }
    [Bindable(false),Category("Behavior"),DefaultValue("True"),Description("False to disable the client grid.")]
    public bool ClientsEnabled { get { return this.grdClients.Enabled; } set { this.grdClients.Enabled = this.txtFindClient.Enabled = this.imgFindClient.Enabled = value; } }
    [Bindable(false),Category("Data"),DefaultValue(-1),Description("Gets or sets the index of the selected row in the vendor grid.")]
    public int AgentSelectedIndex { get { return this.grdAgents.SelectedIndex; } set { this.grdAgents.SelectedIndex = value; } }
    [Bindable(false),Category("Behavior"),DefaultValue("True"),Description("False to disable the agent grid.")]
    public bool AgentsEnabled { get { return this.grdAgents.Enabled; } set { this.grdAgents.Enabled = this.txtFindAgent.Enabled = this.imgFindAgent.Enabled = value; this.upnlAgents.Update(); } }
    
    public GridViewRow ClientSelectedRow { get { GridViewRow row = null; try { row = this.grdClients.SelectedRow; } catch { row = null; } return row; } }
    public string ClientNumber { get { return (ClientSelectedRow != null) ? this.grdClients.SelectedRow.Cells[1].Text : ""; } }
    public string ClientDivsionNumber { get { return (ClientSelectedRow != null) ? this.grdClients.SelectedRow.Cells[2].Text : ""; } }
    public string ClientName { get { return (ClientSelectedRow != null) ? this.grdClients.SelectedRow.Cells[3].Text : ""; } }
    public string ClientTerminalCode { get { return (ClientSelectedRow != null) ? this.grdClients.SelectedRow.Cells[4].Text : ""; } }
    public GridViewRow AgentSelectedRow { get { GridViewRow row = null; try { row = this.grdAgents.SelectedRow; } catch { row = null; } return row; } }
    public string AgentNumber { get { return (AgentSelectedRow != null) ? this.grdAgents.SelectedRow.Cells[1].Text : ""; } }
    public string AgentName { get { return (AgentSelectedRow != null) ? this.grdAgents.SelectedRow.Cells[2].Text : ""; } }
    public void AgentSort(string field,SortDirection direction) { this.grdAgents.Sort(field,direction); this.upnlAgents.Update(); }
    
    protected void OnClientSelected(object sender,EventArgs e) {
        //Event handler for change in selected client
        try {
            //Clear vendor selection; forward event to client
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
    protected void OnAgentSelected(object sender,EventArgs e) {
        //Event handler for change in selected vendor
        try {
            //Forward to client
            if(this.AfterAgentSelected != null) this.AfterAgentSelected(sender,e);
        }
        catch(Exception ex) { reportError(ex); }
    }
    protected void OnFindAgent(object sender,ImageClickEventArgs e) {
        //Event handler for vendor search
        OnAgentSearch(sender,EventArgs.Empty);
    }
    protected void OnAgentSearch(object sender,EventArgs e) {
        //Event handler for vendor search
        findRow(this.grdAgents,1,this.txtFindAgent.Text);
        OnAgentSelected(this.grdAgents,EventArgs.Empty);
        ScriptManager.RegisterStartupScript(this.txtFindAgent,typeof(TextBox),"ScrollVendors","scroll('" + this.grdAgents.ClientID + "', '" + this.pnlAgents.ClientID + "', '" + this.txtFindAgent.Text + "');",true);
    }
    protected void OnAfterClientSelected(object sender,EventArgs e) {
        //Event handler for change in selected client
        if(this.AfterClientSelected != null) this.AfterClientSelected(sender,e);
    }
    protected void OnAfterAgentSelected(object sender,EventArgs e) {
        //Event handler for change in selected vendor
        if(this.AfterAgentSelected != null) this.AfterAgentSelected(sender,e);
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
