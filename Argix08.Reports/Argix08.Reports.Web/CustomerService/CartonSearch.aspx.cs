using System;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using Argix;

public partial class CartonSearch :System.Web.UI.Page {
    //Members
    public static int LabelSequenceChars = 13;
    public static int MaxCartonChars = 30;
   
    private const string REPORT_NAME = "Carton Search";
    private const string REPORT_SRC = "/Customer Service/Carton Search";
    private const string REPORT_DS = "CartonSearchDS";
    private const string USP_REPORT_BYLABELSEQNUMBER = "uspRptSortedItemGetForLabelSequenceNumber";
    private const string USP_REPORT_BYCARTONNUMBER = "uspRptSortedItemGetForCartonNumber",TBL_REPORT = "NewTable";
    
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.Title = Master.ReportTitle = REPORT_NAME + " Report";
            OnSearchByChanged(null,EventArgs.Empty);
            this.grdClients.DataBind();
            if(this.grdClients.Rows.Count > 0) this.grdClients.SelectedIndex = 0;
            OnClientSelected(null,EventArgs.Empty);
        }
        Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
    }
    protected void OnClientSelected(object sender,EventArgs e) {
        //Event handler for change in selected client
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnSearchByChanged(object sender,System.EventArgs e) {
        //Event handler for user changed search by method
        this.txtSearchNo.MaxLength = this.cboSearchBy.SelectedValue == "ByCarton" ? MaxCartonChars : LabelSequenceChars;
        this.txtSearchNo.Text = "";
        this.txtSearchNo.ToolTip = this.cboSearchBy.SelectedValue == "ByCarton" ? "Enter a carton number no more than " + MaxCartonChars.ToString() + " characters" : "Enter a "  + LabelSequenceChars.ToString() + " character label sequence number.";
        OnValidateForm(null,EventArgs.Empty);
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
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        Master.Validated = (this.grdClients.Rows.Count > 0 && this.grdClients.SelectedRow != null) && 
                                            (this.cboSearchBy.SelectedValue == "ByCarton" && this.txtSearchNo.Text.Trim().Length > 0 && this.txtSearchNo.Text.Trim().Length <= MaxCartonChars) ||
                                            (this.cboSearchBy.SelectedValue == "ByLabel" && this.txtSearchNo.Text.Trim().Length == LabelSequenceChars);
    }
    protected void OnButtonCommand(object sender,CommandEventArgs e) {
        //Event handler for command button clicked
        //Change view to Viewer and reset to clear existing data
        Master.Viewer.Reset();

        //Get parameters for the query
        string _cartonID = this.txtSearchNo.Text;
        string _client = this.grdClients.SelectedRow.Cells[1].Text;
        string _div = this.grdClients.SelectedRow.Cells[2].Text;
        string _clientName = this.grdClients.SelectedRow.Cells[3].Text;
        string _terminal = this.grdClients.SelectedRow.Cells[4].Text;

        //Initialize control values
        LocalReport report = Master.Viewer.LocalReport;
        report.DisplayName = REPORT_NAME;
        report.EnableExternalImages = true;
        EnterpriseGateway enterprise = new EnterpriseGateway();
        DataSet ds = enterprise.FillDataset(((this.cboSearchBy.SelectedValue == "ByCarton") ? USP_REPORT_BYCARTONNUMBER : USP_REPORT_BYLABELSEQNUMBER),TBL_REPORT,new object[] { _cartonID,_terminal,_client });
        if(ds.Tables[TBL_REPORT].Rows.Count >= 0) {
            switch(e.CommandName) {
                case "Run":
                    //Set local report and data source
                    System.IO.Stream stream = Master.GetReportDefinition(REPORT_SRC);
                    report.LoadReportDefinition(stream);
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(REPORT_DS,ds.Tables[TBL_REPORT]));

                    //Set the report parameters for the report
                    ReportParameter cartonID = new ReportParameter("SearchNumber",_cartonID);
                    ReportParameter terminal = new ReportParameter("TerminalCode",_terminal);
                    ReportParameter client = new ReportParameter("Client",_client);
                    report.SetParameters(new ReportParameter[] { cartonID,terminal,client });

                    //Update report rendering with new data
                    report.Refresh();
                    
                    if(!Master.Viewer.Enabled) Master.Viewer.CurrentPage = 1;
                    break;
                case "Data":
                    //Set local export report and data source
                    report.LoadReportDefinition(Master.CreateExportRdl(ds,REPORT_DS,"RGXSQLR.TSORT"));
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(REPORT_DS,ds.Tables[TBL_REPORT]));
                    report.Refresh();
                    break;
                case "Excel":
                    //Create Excel mime-type page
                    Response.ClearHeaders();
                    Response.Clear();
                    Response.Charset = "";
                    Response.AddHeader("Content-Disposition","inline; filename=CartonSearch.xls");
                    Response.ContentType = "application/vnd.ms-excel";  //application/octet-stream";
                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
                    DataGrid dg = new DataGrid();
                    dg.DataSource = ds.Tables[TBL_REPORT];
                    dg.DataBind();
                    dg.RenderControl(hw);
                    Response.Write(sw.ToString());
                    Response.End();
                    break;
            }
        }
        else {
            Master.Status = "There were no records found.";
        }
    }
    #region Local Services: findRow()
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
