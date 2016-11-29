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

public partial class Default:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            this.cboClient.DataBind();
            if(this.cboClient.Items.Count > 0) this.cboClient.SelectedIndex = 0;
            OnClientChanged(null,EventArgs.Empty);
        }
    }
    protected void OnClientChanged(object sender,EventArgs e) {
        //Event handler for client selected index event
        this.hfFilter.Value = getInvoiceFilter(this.cboClient.SelectedValue);
        this.grdInvoices.DataBind();
        if(this.grdInvoices.Rows.Count > 0) this.grdInvoices.SelectedIndex = 0;
        OnInvoiceSelected(null,EventArgs.Empty);
    }
    protected void OnInvoiceSearch(object sender,EventArgs e) {
        //Event handler for client search
        for(int i = 0;i < this.grdInvoices.Rows.Count;i++) {
            if(this.grdInvoices.Rows[i].Cells[1].Text == this.txtSearchInvoices.Text) {
                this.grdInvoices.SelectedIndex = i;
                System.Text.StringBuilder script = new System.Text.StringBuilder();
                script.Append("<script language='javascript'>");
                script.Append("scroll('" + this.txtSearchInvoices.Text + "')");
                script.Append("</script>");
                Page.ClientScript.RegisterStartupScript(typeof(Page),"Scroll",script.ToString());
                break;
            }
        }
        OnInvoiceSelected(null,EventArgs.Empty);
    }
    protected void OnInvoiceDataBinding(object sender,GridViewRowEventArgs e) {
        //Event handler for invoice row databinding
        if(e.Row.RowType == DataControlRowType.DataRow) {
            HyperLink lnk = (HyperLink)e.Row.FindControl("lnkOpen");
            if(lnk != null) {
                lnk.Text = e.Row.Cells[1].Text;
                lnk.Target = "_self";
                string target = getTarget(this.cboClient.SelectedValue,e.Row.Cells[9].Text);
                if(target.EndsWith("xltx")) 
                    lnk.NavigateUrl = "javascript:openExcel('" + target + "?clid=" + this.cboClient.SelectedValue + "&invoice=" + e.Row.Cells[1].Text + "')";
                else 
                    lnk.NavigateUrl = "http://rgxsqlrpts05/Reports/Pages/Report.aspx?ItemPath=%2fFinance%2fPOLLOCK+Paper+Invoice";
            }
        }
    }
    protected void OnInvoiceSelected(object sender,EventArgs e) {
        //Event handler for change in selected client
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Determine service availability
        this.btnOpen.Enabled = this.grdInvoices.SelectedDataKey != null;
    }
    protected void OnCommand(object sender,CommandEventArgs e) {
        //Event handler for command click
        switch(e.CommandName) {
            case "Open":
                string target = getTarget(this.cboClient.SelectedValue,this.grdInvoices.SelectedRow.Cells[9].Text);
                if(target.EndsWith("xltx")) {
                    string args = " /t ";
                    args += target;
                    args += "?clid=" + this.cboClient.SelectedValue;
                    args += "&invoice=" + this.grdInvoices.SelectedDataKey.Value.ToString();
                    System.Diagnostics.Process.Start("Excel.exe",args);
                }
                else
                    System.Diagnostics.Process.Start("http://rgxsqlrpts05/Reports/Pages/Report.aspx?ItemPath=%2fFinance%2fPOLLOCK+Paper+Invoice");
                break;
        }
    }

    private string getInvoiceFilter(string clientNumber) {
        //Get invoice document target for specified client and invoice type
        string filter="InvoiceTypeCode=''";
        this.lblFilterTypes.Text = "unsupported";
        System.Xml.XmlNode node = this.xmlConfig.GetXmlDocument().SelectSingleNode("//client[@number='" + clientNumber + "']");
        if(node != null) {
            System.Xml.XmlNode inv = node.SelectSingleNode("invoices");
            if(inv != null) {
                string types = inv.Attributes["types"].Value;
                this.lblFilterTypes.Text = types;
                if(types == "") 
                    filter="InvoiceTypeCode=''";
                else if(types == "*") 
                    filter="";
                else {
                    string[] codes = types.Split(',');
                    filter="";
                    for(int i=0;i<codes.Length;i++) {
                        if(i > 0) filter += " OR ";
                        filter += "InvoiceTypeCode='" + codes[i].Trim() + "'";
                    }
                }
            }
        }
        return filter;
    }
    private string getTarget(string clientNumber,string invoiceType) {
        //Get invoice document target for specified client and invoice type
        string target="";
        System.Xml.XmlNode node = this.xmlConfig.GetXmlDocument().SelectSingleNode("//client[@number='" + clientNumber + "']");
        if(node != null) {
            System.Xml.XmlNode inv = node.SelectSingleNode("invoices");
            if(inv != null) {
                if(inv.Attributes[invoiceType] != null) target = inv.Attributes[invoiceType].Value;
            }
        }
        return target;
    }
}
