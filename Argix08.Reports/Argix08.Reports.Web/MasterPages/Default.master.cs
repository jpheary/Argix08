using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Reporting.WebForms;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

public partial class DefaultMaster:System.Web.UI.MasterPage {
    //Members
    private bool mRun=true,mData=true,mExcel=true,mPDFOnly=false;
    private bool mHideNav=false;
    public event CommandEventHandler ButtonCommand=null;
 
    //Interface
    public string ReportTitle { get { return this.lblReportTitle.Text; } set { this.lblReportTitle.Text = value; } }
    public string PageTitleBackImage { get { return this.tcPageTitle.Style[HtmlTextWriterStyle.BackgroundImage]; } set { this.tcPageTitle.Style[HtmlTextWriterStyle.BackgroundImage] = value; } }
    public string NavTitleBackImage { get { return this.tcNavTitle.Style[HtmlTextWriterStyle.BackgroundImage]; } set { this.tcNavTitle.Style[HtmlTextWriterStyle.BackgroundImage] = value; } }
    public bool NavigatorVisible {
        get { return !this.splMain.Panes[0].Collapsed; } 
        set {
            if(!this.mHideNav && value) {
                this.imgExplore.ImageUrl = "~/App_Themes/Reports/Images/explore_on.gif";
                this.tcExplore.Style["border-right-style"] = "none";
                this.splMain.Panes[0].Collapsed = false;
            }
            else {
                this.imgExplore.ImageUrl = "~/App_Themes/Reports/Images/explore_off.gif";
                this.tcExplore.Style["border-right-style"] = "solid";
                this.splMain.Panes[0].Collapsed = true;
            }
        } 
    }
    public ReportViewer Viewer { get { return this.rsViewer; } }
    public bool Validated { 
        set {
            this.btnRun.Enabled = this.mRun && value;
            if(!this.mPDFOnly) {
                this.btnData.Enabled = this.mData && value;
                this.btnExcel.Enabled = this.mExcel && value;
            }
        } 
    }
    public string Status { set { ShowMsgBox(value,false); } }
    public void ReportError(Exception ex) { reportError(ex); }
    public Stream GetReportDefinition(string report) {
        //Return a report definition from the SQL reporting server
        microsoft.ReportingService2010 rs = new microsoft.ReportingService2010();
        rs.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
        byte[] bytes = rs.GetItemDefinition("/Argix08.Reports" + report);
        return new System.IO.MemoryStream(bytes);
    }
    public Stream CreateExportRdl(DataSet ds,string dataSetName,string dataSourceName) {
        //Open a new stream for writing
        MemoryStream stream = new MemoryStream();
        XmlTextWriter writer = new XmlTextWriter(stream,Encoding.UTF8);
        writer.Formatting = Formatting.Indented;

        //Create list of dataset fields
        ArrayList fields = new ArrayList();
        for(int i=0;i<=ds.Tables[0].Columns.Count-1;i++)
            fields.Add(ds.Tables[0].Columns[i].ColumnName);

        //Report element
        writer.WriteProcessingInstruction("xml","version=\"1.0\" encoding=\"utf-8\"");
        writer.WriteStartElement("Report");
        writer.WriteAttributeString("xmlns",null,"http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
        writer.WriteElementString("Width","6.825in");
        #region DataSource element
        writer.WriteStartElement("DataSources");
        writer.WriteStartElement("DataSource");
        writer.WriteAttributeString("Name",dataSourceName);
        writer.WriteElementString("DataSourceReference",dataSourceName);
        writer.WriteEndElement();
        writer.WriteEndElement();
        #endregion
        #region Body element
        writer.WriteStartElement("Body");
        writer.WriteElementString("Height","1.2in");
        writer.WriteStartElement("ReportItems");
        writer.WriteStartElement("Table");
        writer.WriteAttributeString("Name","table1");
        writer.WriteElementString("DataSetName",dataSetName);
        writer.WriteStartElement("Style");
        writer.WriteElementString("FontSize","8pt");
        writer.WriteEndElement(); //Style
        #region Details
        writer.WriteStartElement("Details");
        writer.WriteStartElement("TableRows");
        writer.WriteStartElement("TableRow");
        writer.WriteStartElement("TableCells");
        foreach(string fieldName in fields) {
            //Textbox
            writer.WriteStartElement("TableCell");
            writer.WriteStartElement("ReportItems");
            writer.WriteStartElement("Textbox");
            writer.WriteAttributeString("Name","txt" + System.Xml.XmlConvert.EncodeName(fieldName));
            writer.WriteStartElement("Style");
            writer.WriteElementString("FontSize","8pt");
            writer.WriteStartElement("BorderStyle");
            writer.WriteElementString("Default","Solid");
            writer.WriteEndElement(); //BorderStyle
            writer.WriteStartElement("BorderWidth");
            writer.WriteElementString("Default","0.5pt");
            writer.WriteEndElement(); //BorderWidth
            writer.WriteEndElement(); //Style
            writer.WriteElementString("Value","=Fields!" + System.Xml.XmlConvert.EncodeName(fieldName) + ".Value");
            writer.WriteEndElement(); //Textbox
            writer.WriteEndElement(); //ReportItems
            writer.WriteEndElement(); //TableCell
        }
        writer.WriteEndElement(); //TableCells
        writer.WriteElementString("Height","0.1875in");
        writer.WriteEndElement(); //TableRow
        writer.WriteEndElement(); //TableRows
        writer.WriteEndElement(); //Details
        #endregion
        #region Header
        writer.WriteStartElement("Header");
        writer.WriteStartElement("TableRows");
        writer.WriteStartElement("TableRow");
        writer.WriteElementString("Height","0.25in");
        writer.WriteStartElement("TableCells");
        foreach(string fieldName in fields) {
            //Textbox
            writer.WriteStartElement("TableCell");
            writer.WriteStartElement("ReportItems");
            writer.WriteStartElement("Textbox");
            writer.WriteAttributeString("Name","hdr" + System.Xml.XmlConvert.EncodeName(fieldName));
            writer.WriteStartElement("Style");
            writer.WriteElementString("FontSize","8pt");
            writer.WriteStartElement("BorderStyle");
            writer.WriteElementString("Default","Solid");
            writer.WriteEndElement(); //BorderStyle
            writer.WriteStartElement("BorderWidth");
            writer.WriteElementString("Default","0.5pt");
            writer.WriteEndElement(); //BorderWidth
            writer.WriteEndElement(); //Style
            writer.WriteElementString("CanGrow","false");
            writer.WriteElementString("Value",fieldName);
            writer.WriteEndElement(); //Textbox
            writer.WriteEndElement(); //ReportItems
            writer.WriteEndElement(); //TableCell
        }
        writer.WriteEndElement(); //TableCells
        writer.WriteEndElement(); //TableRow
        writer.WriteEndElement(); //TableRows
        writer.WriteEndElement(); //Header
        #endregion
        #region TableColumns
        writer.WriteStartElement("TableColumns");
        for(int i=0;i<fields.Count;i++) {
            writer.WriteStartElement("TableColumn");
            writer.WriteElementString("Width","0.75in");
            writer.WriteEndElement(); //TableColumn
        }
        writer.WriteEndElement(); //TableColumns
        #endregion
        writer.WriteEndElement(); //Table
        writer.WriteEndElement(); //ReportItems
        writer.WriteEndElement(); //Body
        #endregion
        #region DataSet element
        writer.WriteStartElement("DataSets");
        writer.WriteStartElement("DataSet");
        writer.WriteAttributeString("Name",dataSetName);
        writer.WriteStartElement("Query");
        writer.WriteElementString("CommandText","");
        writer.WriteElementString("DataSourceName",dataSourceName);
        writer.WriteEndElement(); //Query
        writer.WriteStartElement("Fields");
        foreach(string fieldName in fields) {
            writer.WriteStartElement("Field");
            writer.WriteAttributeString("Name",System.Xml.XmlConvert.EncodeName(fieldName));
            writer.WriteElementString("DataField",fieldName);
            writer.WriteEndElement();
        }
        writer.WriteEndElement(); //Fields
        writer.WriteEndElement(); //DataSet
        writer.WriteEndElement(); //DataSets
        #endregion
        writer.WriteEndElement(); //Report
        writer.Flush();
        writer.BaseStream.Seek(0,0);
        return writer.BaseStream;
    }

    protected override void OnInit(EventArgs e) {
        //Event handler for page Init event
        if(!Page.IsPostBack && !ScriptManager.GetCurrent(Page).IsInAsyncPostBack) {
            //Get configuration values for this control
            string path = Page.Request.CurrentExecutionFilePath.Replace(".aspx","");
            string reportName = path.Substring(path.LastIndexOf('/') + 1);
            System.Xml.XmlNode rpt = this.xmlConfig.GetXmlDocument().SelectSingleNode("//report[@name='" + reportName + "']");
            if(rpt != null) {
                System.Xml.XmlNode svcs = rpt.SelectSingleNode("Services");
                if(svcs != null) {
                    this.mRun = bool.Parse(svcs.Attributes["run"].Value);
                    this.mData = bool.Parse(svcs.Attributes["data"].Value);
                    this.mExcel = bool.Parse(svcs.Attributes["excel"].Value);
                    this.mPDFOnly = bool.Parse(svcs.Attributes["pdfonly"].Value);
                }
            }
        }
        base.OnInit(e);
    }
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            ViewState.Add("Run",this.mRun);
            ViewState.Add("Data", this.mData);
            ViewState.Add("Excel", this.mExcel);
            ViewState.Add("PDFOnly",this.mPDFOnly);
            this.mHideNav = bool.Parse(ConfigurationManager.AppSettings["HideNavigator"]);
            ViewState.Add("HideNav",this.mHideNav);

            //Initialize control values
            this.imgExplore.Attributes.Add("onclick","javascript:document.body.style.cursor='wait';");
            this.tblFlyout.Visible = !this.mHideNav; 
            this.splMain.Panes[0].Collapsed = this.mHideNav;
            NavigatorVisible = !this.mHideNav;
        }
        else {
            this.mRun = (bool)ViewState["Run"];
            this.mData = (bool)ViewState["Data"];
            this.mExcel = (bool)ViewState["Excel"];
            this.mPDFOnly = (bool)ViewState["PDFOnly"];
            this.mHideNav = (bool)ViewState["HideNav"];
        }
        if(this.mPDFOnly) this.btnData.Enabled = this.btnExcel.Enabled = false;
    }
    protected void OnExploreTabClicked(object sender,ImageClickEventArgs e) {
        NavigatorVisible = !NavigatorVisible;
        ScriptManager.RegisterStartupScript(this.imgExplore,typeof(ImageButton),"ClearCursor","document.body.style.cursor='default';",true);
    }
    protected void OnTreeNodeDataBound(object sender,TreeNodeEventArgs e) {
        //Event handler for treeview node data bounded
        string url = e.Node.NavigateUrl;
        if(url.Trim().Length > 0) {
            if(e.Node.Text + " Report" == this.lblReportTitle.Text) {
                e.Node.Selected = true;
                e.Node.Parent.Expanded = true;
            }
        }
    }
    protected void OnButtonCommand(object sender,CommandEventArgs e) {
        //Event handler for export button clicked
        DateTime start = DateTime.Now;
        switch(e.CommandName) {
            case "Setup":
                this.btnSetup.BorderStyle = BorderStyle.Inset;
                this.btnRun.BorderStyle = BorderStyle.Outset;
                this.btnData.BorderStyle = BorderStyle.Outset;
                this.btnExcel.BorderStyle = BorderStyle.Outset;
                this.mvMain.SetActiveView(this.vwParams);
                this.rsViewer.Reset();
                break;
            case "Run":
                this.btnSetup.BorderStyle = BorderStyle.Outset;
                this.btnRun.BorderStyle = BorderStyle.Inset;
                this.btnData.BorderStyle = BorderStyle.Outset;
                this.btnExcel.BorderStyle = BorderStyle.Outset;
                this.mvMain.SetActiveView(this.vwReport);
                //NavigatorVisible = false;
                if(this.ButtonCommand != null) this.ButtonCommand(sender,e);
                showRuntime(start);
                if(this.mPDFOnly) {
                    this.rsViewer.ShowExportControls = false;
                    string mimeType="",encoding="",extension="";
                    string[] streamids;
                    Warning[] warnings;
                    byte[] bytes = this.rsViewer.LocalReport.Render("PDF",null,out mimeType,out encoding,out extension,out streamids,out warnings);
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("Accept-Header",bytes.Length.ToString());
                    Response.ContentType = mimeType;
                    Response.OutputStream.Write(bytes,0,Convert.ToInt32(bytes.Length));
                    Response.Flush();
                    Response.End();
                }
                break;
            case "Data":
                this.btnSetup.BorderStyle = BorderStyle.Outset;
                this.btnRun.BorderStyle = BorderStyle.Outset;
                this.btnData.BorderStyle = BorderStyle.Inset;
                this.btnExcel.BorderStyle = BorderStyle.Outset;
                this.mvMain.SetActiveView(this.vwReport);
                //NavigatorVisible = false;
                if(this.ButtonCommand != null) this.ButtonCommand(sender,e);
                showRuntime(start);
                break;
            case "Excel":
                this.btnSetup.BorderStyle = BorderStyle.Outset;
                this.btnRun.BorderStyle = BorderStyle.Outset;
                this.btnData.BorderStyle = BorderStyle.Outset;
                this.btnExcel.BorderStyle = BorderStyle.Inset;
                this.mvMain.SetActiveView(this.vwReport);
                //NavigatorVisible = false;
                if(this.ButtonCommand != null) this.ButtonCommand(sender,e);
                showRuntime(start);
                break;
        }
    }
    protected void OnViewerError(object sender,ReportErrorEventArgs e) { reportError(e.Exception); }
    #region Local Services: fillDataset(), reportError(), ShowMsgBox(), showRuntime()
    protected DataSet fillDataset(string sp,string table,object[] o) {
        //
        DataSet ds = new DataSet();
        Database db = DatabaseFactory.CreateDatabase("SQLConnection");
        DbCommand cmd = db.GetStoredProcCommand(sp,o);
        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeout"]);
        db.LoadDataSet(cmd,ds,table);
        return ds;
    }
    public void reportError(Exception ex) {
        //Report an exception to the user
        try {
            string src = (ex.Source != null) ? ex.Source + "-\n" : "";
            string msg = src + ex.Message;
            if(ex.InnerException != null) {
                if((ex.InnerException.Source != null)) src = ex.InnerException.Source + "-\n";
                msg = src + ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
            }
            ShowMsgBox(msg,true);
        }
        catch(Exception) { }
    }
    public void ShowMsgBox(string message) { ShowMsgBox(message,false); }
    public void ShowMsgBox(string message,bool isStartup) {
        //
        System.Text.StringBuilder script = new System.Text.StringBuilder();
        script.Append("<script language=javascript>");
        script.Append(" alert('" + message + "');");
        script.Append("</script>");
        if(isStartup)
            Page.ClientScript.RegisterStartupScript(typeof(Page),"Error",script.ToString());
        else
            System.Web.HttpContext.Current.Response.Write(script.ToString());
    }
    private void showRuntime(DateTime start) {
        try {
            this.lblStatus.Text = Convert.ToInt32(DateTime.Now.Subtract(start).TotalSeconds).ToString();
        } catch { }
    }
    #endregion
}
