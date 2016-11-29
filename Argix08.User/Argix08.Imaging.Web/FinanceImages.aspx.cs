using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.Schema;
using Microsoft.SharePoint.Utilities;

public partial class _FinanceImages : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Page load event handler
        if(!Page.IsPostBack) {
            ViewState.Add("SortDir","D");
            this.cboDocClass.DataBind();
            OnDocClassChanged(this.cboDocClass,EventArgs.Empty);
            this.txtSearch1.Focus();
        }
    }
    protected void OnDocClassChanged(object sender,EventArgs e) {
        //Event handler for change in selected document class
        //Reset
        this.txtSearch1.Text = this.txtSearch2.Text = "";
        this.grdImages.DataSource = null;
        this.grdImages.DataBind();
    }
    protected void OnSearchClicked(object sender, EventArgs e) {
        //Event handler for search button clicked
        this.grdImages.Columns.Clear();
        this.grdImages.DataSource = null;
        this.grdImages.DataBind();
        DataSet ds = getSearchData();
        
        //Configures grid columns based upon new data and sets new data source
        this.grdImages.Columns.Clear();
        DataColumnCollection cols = ds.Tables[0].Columns;
        for(int i = 2; i < cols.Count; i++) {
            BoundField bField = new BoundField();
            string colName = cols[i].ColumnName;
            bField.DataField = colName;
            bField.HeaderText = XmlConvert.DecodeName(colName.Substring(colName.IndexOf("_") + 1));
            bField.SortExpression = XmlConvert.EncodeName(colName);
            bField.ControlStyle.Width = 144;
            this.grdImages.Columns.Add(bField);
        }
        HyperLinkField hlField = new HyperLinkField();
        hlField.DataTextField = cols[0].ColumnName;
        hlField.DataNavigateUrlFields = new string[] { cols[1].ColumnName };
        hlField.HeaderText = "Image Link";
        hlField.Target = "_blank";
        this.grdImages.Columns.Add(hlField);
        this.grdImages.DataSource = ds.Tables[0];
        this.grdImages.DataBind();
    }
    protected void OnGridSorting(object sender,GridViewSortEventArgs e) {
        //Event handler for grid sorting event
        DataSet ds = new DataSet();
        DataSet _ds = getSearchData();
        if(ViewState["SortDir"].ToString() == "A")
            ds.Merge(_ds.Tables[0].Select("",XmlConvert.DecodeName(e.SortExpression) + " DESC"));
        else
            ds.Merge(_ds.Tables[0].Select("",XmlConvert.DecodeName(e.SortExpression) + " ASC"));
        this.grdImages.DataSource = ds;
        this.grdImages.DataBind();
    }
    protected void OnGridSorted(object sender,EventArgs e) {
        //Event handler for grid completed sorting event
        ViewState["SortDir"] = ViewState["SortDir"].ToString() == "D" ? "A" : "D";
    }
    protected DataSet getSearchData() {
        //Get image data
        SearchDS searchDS = new SearchDS();
        SearchDS.SearchTableRow searchRow = searchDS.SearchTable.NewSearchTableRow();
        searchRow.ScopeName = this.cboDocClass.SelectedValue;
        searchRow.DocumentClass = this.cboDocClass.SelectedValue;
        searchRow.PropertyName = this.cboProp1.SelectedValue;
        searchRow.PropertyValue = this.txtSearch1.Text.Trim();
        if(this.txtSearch2.Text.Trim().Length > 0) {
            searchRow.Operand1 = this.cboOperand1.SelectedValue;
            searchRow.PropertyName1 = this.cboProp2.SelectedValue;
            searchRow.PropertyValue1 = this.txtSearch2.Text.Trim();
        }
        searchDS.SearchTable.AddSearchTableRow(searchRow);
        Argix.Freight.ImagingService isvc = new Argix.Freight.ImagingService();
        return isvc.SearchSharePointImageStore(searchDS);
    }
}
