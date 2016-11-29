using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Default : System.Web.UI.Page {
	//Members
    private int mRowIndex=0;

    //Interface
	protected void Page_Load(object sender, System.EventArgs e) {
        //Page load event handler
        if(!Page.IsPostBack) {
            //Initialize control values
            this.imgTLView.Attributes.Add("onclick","javascript:document.body.style.cursor='wait';");
            this.imgAgentView.Attributes.Add("onclick","javascript:document.body.style.cursor='wait';");
            this.cboTerminal.DataBind();
            if(this.cboTerminal.Items.Count > 0) {
                string terminalID = Request.QueryString["location"];
                if(terminalID == null) terminalID = "0";
                if(terminalID.Trim().Length == 0) terminalID = "0";
                this.cboTerminal.SelectedValue = terminalID;
            }
            OnTerminalChanged(null,EventArgs.Empty);
            OnTLViewTabClicked(null,null);
        }
        //Register client-side events and scripts
        if(!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(),"GridScript"))
            Page.ClientScript.RegisterClientScriptInclude(this.GetType(),"GridScript","scripts/GridScript.js");
        if(!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(),"TLVScript"))
            Page.ClientScript.RegisterClientScriptInclude(typeof(Page),"TLVScript","scripts/TLVScript.js");
        Page.ClientScript.RegisterStartupScript(typeof(Page),"AttachEvents",attachEventsScript());
        //this.txtFind.Attributes.Add("onkeyup","findTL();");
    }
    protected void OnTLViewTabClicked(object sender,ImageClickEventArgs e) {
        this.tcTLView.Style["border-right-style"] = "none";
        this.tcAgentView.Style["border-right-style"] = "solid";
        this.mvMain.SetActiveView(this.vwTLs);
        OnTerminalChanged(null,EventArgs.Empty);
        ScriptManager.RegisterStartupScript(this.imgTLView,typeof(ImageButton),"ClearCursor","document.body.style.cursor='default';",true);
    }
    protected void OnAgentViewTabClicked(object sender,ImageClickEventArgs e) {
        this.tcTLView.Style["border-right-style"] = "solid";
        this.tcAgentView.Style["border-right-style"] = "none";
        this.mvMain.SetActiveView(this.vwAgents);
        OnTerminalChanged(null,EventArgs.Empty);
        ScriptManager.RegisterStartupScript(this.imgAgentView,typeof(ImageButton),"ClearCursor","document.body.style.cursor='default';",true);
    }
    protected void OnTerminalChanged(object sender,EventArgs e) {
        //Event handler for change in selected terminal
        switch(this.mvMain.ActiveViewIndex) {
            case 0:
                this.grdTLs.DataBind();
                this.upnlTotals.Update();
                break;
            case 1:
                this.grdSummary.DataBind();
                break;
        }
    }
    protected void OnToolbarClick(object sender,CommandEventArgs e) {
        //Event handler for toolbar button clicked
        switch(e.CommandName) {
            case "Refresh":
                OnTerminalChanged(null, EventArgs.Empty);
                break;
        }
    }
    protected void OnDataBound(object sender,EventArgs e) {
        //Add a grid attribute to identify some needed grid column indexes
        this.grdTLs.Attributes.Add("CartonsCol","9");
        this.grdTLs.Attributes.Add("PalletsCol","10");
        this.grdTLs.Attributes.Add("WeightCol","11");
        this.grdTLs.Attributes.Add("CubeCol","12");
    }
    protected void OnRowDataBound(object sender,GridViewRowEventArgs e) {
        //
        e.Row.Attributes.Add("OnClick","rowSelection();");
        e.Row.Attributes.Add("OnMouseOver","this.style.cursor='hand'");
        e.Row.Attributes.Add("id",this.mRowIndex.ToString() + "row");
        this.mRowIndex++;
        this.grdTLs.Attributes.Add("TotalRows",this.mRowIndex.ToString());
        foreach(TableCell tabCell in e.Row.Cells) {
            tabCell.Attributes.Add("UNSELECTABLE","on");
        }
    }
    protected void OnSorted(object sender,EventArgs e) {
        this.upnlTotals.Update();
    }
    protected void OnFindTL(object sender,EventArgs e) {
        //Event handler for client search
        findRow(this.grdTLs,1,this.txtFind.Text);
        ScriptManager.RegisterStartupScript(this.txtFind,typeof(TextBox),"ScrollTLs","findTL();",true);
    }
    #region Local Services: attachEventsScript(), findRow()
    private string attachEventsScript() {
        //
        System.Text.StringBuilder strBlder = new System.Text.StringBuilder();
        strBlder.Append("<script language='javascript'>");
        //strBlder.Append(" resizeGrid(); window.attachEvent('onresize',resizeGrid);");
        strBlder.Append(" document.body.attachEvent('onkeydown',keyDownEvent);");
        strBlder.Append(" document.body.attachEvent('onkeydown',keyPressed);");
        strBlder.Append(" document.body.attachEvent('onkeyup',keyUpEvent);");
        strBlder.Append(" document.all.ISAWeight.attachEvent('onpropertychange',doCalculation);");
        strBlder.Append("</script>");
        return strBlder.ToString();
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
    #endregion
}
