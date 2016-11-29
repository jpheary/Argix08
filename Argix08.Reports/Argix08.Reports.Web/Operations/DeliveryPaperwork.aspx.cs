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

public partial class DeliveryPaperwork :System.Web.UI.Page {
    //Members
    private static int DaysAllowedInPast = 14;
  
    private const string REPORT_NAME = "Delivery Paperwork";
    private const string REPORT_SRC_DELIVERY = "/Operations/Delivery Bill";
    private const string REPORT_DS_DELIVERY = "DeliveryBillDS";
    private const string USP_REPORT_DELIVERY = "uspRptDeliveryBillMultiColumnGetForTL",TBL_REPORT = "NewTable";
    private const string REPORT_SRC_MANIFEST = "/Operations/Master Manifest";
    private const string REPORT_DS_MANIFEST = "MasterManifestDS";
    private const string USP_REPORT_MANIFEST = "uspRptManifestGetForTLTsort";
    private const string REPORT_SRC_DELIVERY_PO = "/Operations/Delivery Bill WIth PO";
    private const string REPORT_DS_DELIVERY_PO = "DeliveryBillWIthPO_DS";
    private const string USP_REPORT_DELIVERY_PO = "uspRptDeliveryBillMultiColumnWithPOGetForTL";
    private const string USP_CARRIERS = "uspRptCarrierGetForZone",TBL_CARRIERS = "TLCarrierTable";
    private const string USP_SHIPPERS = "uspRptShipperGet",TBL_SHIPPERS = "TLShipperTable";
    
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.Title = Master.ReportTitle = REPORT_NAME + " Report";
            this.cboTerminal.DataBind();
            if(this.cboTerminal.Items.Count > 0) this.cboTerminal.SelectedIndex = 0;
            this.cboTerminal.Enabled = (this.cboTerminal.Items.Count > 0);
            OnTerminalSelected(null,EventArgs.Empty);
            this.cboType.SelectedIndex = 0;
        }
        Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnTerminalSelected(object sender,System.EventArgs e) {
        //Event hanlder for change in selected terminal
        this.grdTLs.DataBind();
        OnTLSelected(this,EventArgs.Empty);
    }
    protected void OnFromToDateChanged(object sender,EventArgs e) {
        this.grdTLs.DataSourceID = "odsTLs";
        this.grdTLs.DataBind();
        OnTLSelected(this,EventArgs.Empty);
    }
    protected void OnTLSelected(object sender,EventArgs e) {
        //Event handler for change in selected trip
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnFind(object sender,ImageClickEventArgs e) {
        //Event handler for vendor search
        OnSearch(sender,EventArgs.Empty);
    }
    protected void OnSearch(object sender,EventArgs e) {
        //Event handler for vendor search
        if(this.txtSearch.Text.Length == 0) return;
        this.grdTLs.DataSourceID = "odsTL";
        this.grdTLs.DataBind();
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        Master.Validated = (this.grdTLs.Rows.Count > 0 && this.grdTLs.SelectedRow != null);
    }
    protected void OnButtonCommand(object sender,CommandEventArgs e) {
        //Event handler for view button clicked
        try {
            //Change view to Viewer and reset to clear existing data
            Master.Viewer.Reset();

            //Get parameters for the query
            string _terminalID = this.cboTerminal.SelectedValue;
            string _type = this.cboType.SelectedValue;
            string _tlNumber = this.grdTLs.SelectedRow.Cells[1].Text;
            string _zone = this.grdTLs.SelectedRow.Cells[2].Text;

            //Initialize control values
            LocalReport report = Master.Viewer.LocalReport;
            report.DisplayName = REPORT_NAME;
            report.EnableExternalImages = true;
            EnterpriseGateway enterprise = new EnterpriseGateway();
            DataSet ds=null;
            switch(_type.ToLower()) {
                case "manifest":                    ds = enterprise.FillDataset(USP_REPORT_MANIFEST,TBL_REPORT,new object[] { _terminalID,_tlNumber }); break;
                case "delivery bill":               ds = enterprise.FillDataset(USP_REPORT_DELIVERY,TBL_REPORT,new object[] { _tlNumber }); break;
                case "delivery bill w/po number":   ds = enterprise.FillDataset(USP_REPORT_DELIVERY_PO,TBL_REPORT,new object[] { _tlNumber }); break;
            }
            if(ds.Tables[TBL_REPORT].Rows.Count >= 0) {
                switch(e.CommandName) {
                    case "Run":
                        ////Set local report and data source
                        System.IO.Stream stream=null;
                        switch(_type.ToLower()) {
                            case "manifest":        stream = Master.GetReportDefinition(REPORT_SRC_MANIFEST); break;
                            case "delivery bill":   stream = Master.GetReportDefinition(REPORT_SRC_DELIVERY); break;
                            case "delivery bill w/po number": stream = Master.GetReportDefinition(REPORT_SRC_DELIVERY_PO); break;
                        }
                        report.LoadReportDefinition(stream);
                        report.DataSources.Clear();
                        switch(_type.ToLower()) {
                            case "manifest": report.DataSources.Add(new ReportDataSource(REPORT_DS_MANIFEST,ds.Tables[TBL_REPORT])); break;
                            case "delivery bill": report.DataSources.Add(new ReportDataSource(REPORT_DS_DELIVERY,ds.Tables[TBL_REPORT])); break;
                            case "delivery bill w/po number": report.DataSources.Add(new ReportDataSource(REPORT_DS_DELIVERY_PO,ds.Tables[TBL_REPORT])); break;
                        }

                        //Set the report parameters for the report
                        ReportParameter terminalID = new ReportParameter("TerminalID",_terminalID);
                        ReportParameter terminal = new ReportParameter("Terminal",this.cboTerminal.SelectedItem.Text);
                        ReportParameter tlNumber = new ReportParameter("TLNumber",_tlNumber);
                        ReportParameter zone = new ReportParameter("Zone",_zone);
                        ReportParameter carrierNumber=null,carrierName=null,carrierAddress1=null,carrierAddress2=null,carrierCity=null,carrierState=null,carrierZip=null;
                        ReportParameter clientNumber=null,clientDiv=null,clientName=null;
                        string _clientNumber="",_clientDiv="",_clientName="",_consNumber="";
                        string _number="",_name="",_addressLine1="",_addressLine2="",_city="",_state="",_zip="";
                        string _shipperName = "",_shipperAddressLine1 = "",_shipperAddressLine2 = "",_shipperCity = "",_shipperState = "",_shipperZip = "";
                        DataSet carriers=null,shippers=null;
                        switch(_type.ToLower()) {
                            case "manifest":
                                if(ds.Tables[TBL_REPORT].Rows.Count > 0) {
                                    _clientNumber = ds.Tables[TBL_REPORT].Rows[0].IsNull("Client") ? "" : ds.Tables[TBL_REPORT].Rows[0]["Client"].ToString().Trim();
                                    _clientName = ds.Tables[TBL_REPORT].Rows[0].IsNull("ClientName") ? "" : ds.Tables[TBL_REPORT].Rows[0]["ClientName"].ToString().Trim();
                                }
                                carriers = enterprise.FillDataset(USP_CARRIERS,TBL_CARRIERS,new object[] { _zone });
                                if(carriers.Tables[TBL_CARRIERS].Rows.Count > 0) {
                                    _number = carriers.Tables[TBL_CARRIERS].Rows[0].IsNull("NUMBER") ? "" : carriers.Tables[TBL_CARRIERS].Rows[0]["NUMBER"].ToString().Trim();
                                    _name = carriers.Tables[TBL_CARRIERS].Rows[0].IsNull("NAME") ? "" : carriers.Tables[TBL_CARRIERS].Rows[0]["NAME"].ToString().Trim();
                                    _addressLine1 = carriers.Tables[TBL_CARRIERS].Rows[0].IsNull("ADDRESS_LINE1") ? "" : carriers.Tables[TBL_CARRIERS].Rows[0]["ADDRESS_LINE1"].ToString().Trim();
                                    _addressLine2 = carriers.Tables[TBL_CARRIERS].Rows[0].IsNull("ADDRESS_LINE2") ? "" : carriers.Tables[TBL_CARRIERS].Rows[0]["ADDRESS_LINE2"].ToString().Trim();
                                    _city = carriers.Tables[TBL_CARRIERS].Rows[0].IsNull("CITY") ? "" : carriers.Tables[TBL_CARRIERS].Rows[0]["CITY"].ToString().Trim();
                                    _state = carriers.Tables[TBL_CARRIERS].Rows[0].IsNull("STATE") ? "" : carriers.Tables[TBL_CARRIERS].Rows[0]["STATE"].ToString().Trim();
                                    _zip = carriers.Tables[TBL_CARRIERS].Rows[0].IsNull("ZIP") ? "" : carriers.Tables[TBL_CARRIERS].Rows[0]["ZIP"].ToString().Trim();
                                }
                                clientNumber = new ReportParameter("ClientNumber",_clientNumber);
                                clientName = new ReportParameter("ClientName",_clientName);
                                carrierNumber = new ReportParameter("CarrierNumber",_number);
                                carrierName = new ReportParameter("CarrierName",_name);
                                carrierAddress1 = new ReportParameter("CarrierAddress1",_addressLine1);
                                carrierAddress2 = new ReportParameter("CarrierAddress2",_addressLine2);
                                carrierCity = new ReportParameter("CarrierCity",_city);
                                carrierState = new ReportParameter("CarrierState",_state);
                                carrierZip = new ReportParameter("CarrierZip",_zip);
                                report.SetParameters(new ReportParameter[] { terminalID,tlNumber,zone,carrierNumber,carrierName,carrierAddress1,carrierAddress2,carrierCity,clientNumber,clientName,carrierState,carrierZip });
                                break;
                            case "delivery bill":
                            case "delivery bill w/po number":
                                if(ds.Tables[TBL_REPORT].Rows.Count > 0) {
                                    _clientNumber = ds.Tables[TBL_REPORT].Rows[0].IsNull("ClientNumber") ? "" : ds.Tables[TBL_REPORT].Rows[0]["ClientNumber"].ToString().Trim();
                                    _clientDiv = ds.Tables[TBL_REPORT].Rows[0].IsNull("ClientDivision") ? "" : ds.Tables[TBL_REPORT].Rows[0]["ClientDivision"].ToString().Trim();
                                    _consNumber = ds.Tables[TBL_REPORT].Rows[0].IsNull("Consignee") ? "" : ds.Tables[TBL_REPORT].Rows[0]["Consignee"].ToString().Trim();
                                }
                                carriers = enterprise.FillDataset(USP_CARRIERS,TBL_CARRIERS,new object[] { _zone });
                                if(carriers.Tables[TBL_CARRIERS].Rows.Count > 0) {
                                    _name = carriers.Tables[TBL_CARRIERS].Rows[0].IsNull("NAME") ? "" : carriers.Tables[TBL_CARRIERS].Rows[0]["NAME"].ToString().Trim();
                                    _addressLine1 = carriers.Tables[TBL_CARRIERS].Rows[0].IsNull("ADDRESS_LINE1") ? "" : carriers.Tables[TBL_CARRIERS].Rows[0]["ADDRESS_LINE1"].ToString().Trim();
                                    _addressLine2 = carriers.Tables[TBL_CARRIERS].Rows[0].IsNull("ADDRESS_LINE2") ? "" : carriers.Tables[TBL_CARRIERS].Rows[0]["ADDRESS_LINE2"].ToString().Trim();
                                    _city = carriers.Tables[TBL_CARRIERS].Rows[0].IsNull("CITY") ? "" : carriers.Tables[TBL_CARRIERS].Rows[0]["CITY"].ToString().Trim();
                                    _state = carriers.Tables[TBL_CARRIERS].Rows[0].IsNull("STATE") ? "" : carriers.Tables[TBL_CARRIERS].Rows[0]["STATE"].ToString().Trim();
                                    _zip = carriers.Tables[TBL_CARRIERS].Rows[0].IsNull("ZIP") ? "" : carriers.Tables[TBL_CARRIERS].Rows[0]["ZIP"].ToString().Trim();
                                }
                                shippers = enterprise.FillDataset(USP_SHIPPERS,TBL_SHIPPERS,new object[] { _terminalID });
                                if(shippers.Tables[TBL_SHIPPERS].Rows.Count > 0) {
                                    _shipperName = shippers.Tables[TBL_SHIPPERS].Rows[0].IsNull("NAME") ? "" : shippers.Tables[TBL_SHIPPERS].Rows[0]["NAME"].ToString().Trim();
                                    _shipperAddressLine1 = shippers.Tables[TBL_SHIPPERS].Rows[0].IsNull("ADDRESS_LINE1") ? "" : shippers.Tables[TBL_SHIPPERS].Rows[0]["ADDRESS_LINE1"].ToString().Trim();
                                    _shipperAddressLine2 = shippers.Tables[TBL_SHIPPERS].Rows[0].IsNull("ADDRESS_LINE2") ? "" : shippers.Tables[TBL_SHIPPERS].Rows[0]["ADDRESS_LINE2"].ToString().Trim();
                                    _shipperCity = shippers.Tables[TBL_SHIPPERS].Rows[0].IsNull("CITY") ? "" : shippers.Tables[TBL_SHIPPERS].Rows[0]["CITY"].ToString().Trim();
                                    _shipperState = shippers.Tables[TBL_SHIPPERS].Rows[0].IsNull("STATE") ? "" : shippers.Tables[TBL_SHIPPERS].Rows[0]["STATE"].ToString().Trim();
                                    _shipperZip = shippers.Tables[TBL_SHIPPERS].Rows[0].IsNull("ZIP") ? "" : shippers.Tables[TBL_SHIPPERS].Rows[0]["ZIP"].ToString().Trim();
                                }
                                carrierName = new ReportParameter("CarrierName",_name);
                                carrierAddress1 = new ReportParameter("CarrierAddr1",_addressLine1);
                                carrierAddress2 = new ReportParameter("CarrierAddr2",_addressLine2);
                                carrierCity = new ReportParameter("CarrierCity",_city);
                                carrierState = new ReportParameter("CarrierState",_state);
                                carrierZip = new ReportParameter("CarrierZip",_zip);
                                ReportParameter shipName = new ReportParameter("ShipName",_shipperName);
                                ReportParameter shipAddr1 = new ReportParameter("ShipAddr1",_shipperAddressLine1);
                                ReportParameter shipAddr2 = new ReportParameter("ShipAddr2",_shipperAddressLine2);
                                ReportParameter shipCity = new ReportParameter("ShipCity",_shipperCity);
                                ReportParameter shipState = new ReportParameter("ShipState",_shipperState);
                                ReportParameter shipZip = new ReportParameter("ShipZip",_shipperZip);
                                clientNumber = new ReportParameter("ClientNumber",_clientNumber);
                                clientDiv = new ReportParameter("ClientDiv",_clientDiv);
                                ReportParameter consNumber = new ReportParameter("ConsNumber",_consNumber);
                                report.SetParameters(new ReportParameter[] { terminalID,tlNumber,zone,carrierName,carrierAddress1,carrierAddress2,carrierCity,carrierState,carrierZip,shipName,shipAddr1,shipAddr2,shipCity,shipState,shipZip,clientNumber,clientDiv,consNumber });
                                break;
                        }
                        ////Update report rendering with new data
                        report.Refresh();
                        
                        if(!Master.Viewer.Enabled) Master.Viewer.CurrentPage = 1;
                        break;
                    case "Data":
                        //Set local export report and data source
                        System.IO.Stream streamD=null;
                        switch(_type.ToLower()) {
                            case "manifest": streamD = Master.CreateExportRdl(ds,REPORT_DS_MANIFEST,"RGXSQLR.TSORT"); break;
                            case "delivery bill": streamD = Master.CreateExportRdl(ds,REPORT_DS_DELIVERY,"RGXSQLR.TSORT"); break;
                            case "delivery bill w/po number": streamD = Master.CreateExportRdl(ds,REPORT_DS_DELIVERY_PO,"RGXSQLR.TSORT"); break;
                        }
                        report.LoadReportDefinition(streamD);
                        report.DataSources.Clear();
                        switch(_type.ToLower()) {
                            case "manifest": report.DataSources.Add(new ReportDataSource(REPORT_DS_MANIFEST,ds.Tables[TBL_REPORT])); break;
                            case "delivery bill": report.DataSources.Add(new ReportDataSource(REPORT_DS_DELIVERY,ds.Tables[TBL_REPORT])); break;
                            case "delivery bill w/po number": report.DataSources.Add(new ReportDataSource(REPORT_DS_DELIVERY_PO,ds.Tables[TBL_REPORT])); break;
                        }
                        report.Refresh();
                        break;
                    case "Excel":
                        //Create Excel mime-type page
                        Response.ClearHeaders();
                        Response.Clear();
                        Response.Charset = "";
                        Response.AddHeader("Content-Disposition","inline; filename=DeliveryPaperwork.xls");
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
        catch(Exception ex) { Master.ReportError(ex); }
    }
}
