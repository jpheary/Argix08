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

public partial class USPSConsigneeAddress:System.Web.UI.Page {
    //Members
    private const string REPORT_NAME = "USPS Consignee Address";
    private const string REPORT_SRC = "/Consumer Direct/USPS Consignee Address";
    private const string REPORT_DS = "DataSet1";
    private const string USP_REPORT = "uspRptUSPSConsigneeAddressGetList",TBL_REPORT = "NewTable";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.Title = Master.ReportTitle = REPORT_NAME + " Report";
            OnValidateForm(null,EventArgs.Empty);

            //Attach client-side scripts
            Page.ClientScript.RegisterStartupScript(typeof(Page),"StartupScript","<script language='javascript'>document.all." + this.txtNumbers.ClientID + ".select();</script>");
            this.txtNumbers.Attributes.Add("onkeypress","checkTextLen(" + this.txtNumbers.ClientID + ",400);");
            this.txtNumbers.Attributes.Add("onblur","checkTextLen(" + this.txtNumbers.ClientID + ",400);");
        }
        Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        Master.Validated = true;
    }
    protected void OnButtonCommand(object sender,CommandEventArgs e) {
        //Event handler for command button clicked
        //Change view to Viewer and reset to clear existing data
        Master.Viewer.Reset();

        //Get parameters for the query
        //Validate
        string input = encodeInput(this.txtNumbers.Text);
        if(input.Length == 0) return;
        string[] numbers = input.Split(Convert.ToChar(13));
        if(numbers.Length > 10) return;

        //Build search table and validate
        string _text = "";
        for(int i=0;i<numbers.Length;i++) {
            string number = numbers[i].Trim();
            if(i > 0) _text += ",";
            if(number.Length > 0 && number.Length <= 30 && isNumeric(number))
                _text += number;
        }

        //Initialize control values
        LocalReport report = Master.Viewer.LocalReport;
        report.DisplayName = REPORT_NAME;
        report.EnableExternalImages = true;
        EnterpriseGateway enterprise = new EnterpriseGateway();
        DataSet ds = enterprise.FillDataset(USP_REPORT,TBL_REPORT,new object[] { _text });
        if(ds.Tables[TBL_REPORT].Rows.Count >= 0) {
            switch(e.CommandName) {
                case "Run":
                    //Set local report and data source
                    System.IO.Stream stream = Master.GetReportDefinition(REPORT_SRC);
                    report.LoadReportDefinition(stream);
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(REPORT_DS,ds.Tables[TBL_REPORT]));

                    //Set the report parameters for the report
                    ReportParameter text = new ReportParameter("Text",_text);
                    report.SetParameters(new ReportParameter[] { text });

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
                    Response.AddHeader("Content-Disposition","inline; filename=BNConsumerDirectDDUPerformanceDetail.xls");
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
    #region Local Services: encodeInput(), isNumeric()
    private string encodeInput(string input) {
        //This method makes sure no invalid chars exist in the user input
        string cn = Server.HtmlEncode(input);
        cn = cn.Replace("'","''");
        cn = cn.Replace("[","[[]");
        cn = cn.Replace("%","[%]");
        cn = cn.Replace("_","[_]");
        cn = cn.Replace(Convert.ToChar(','),Convert.ToChar(13));
        return cn.Trim();
    }
    private bool isNumeric(string val) {
        //Determine if the specified value is numeric
        bool valIsNumeric=true;
        char[] chars = val.ToCharArray();
        for(int i=0;i<chars.Length;i++) {
            if(!char.IsNumber(val,i)) {
                valIsNumeric = false;
                break;
            }
        }
        return valIsNumeric;
    }
    #endregion
}
