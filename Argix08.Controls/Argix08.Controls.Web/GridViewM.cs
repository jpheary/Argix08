//	File:	GridViewM.cs
//	Author:	J. Heary
//	Date:	06/05/08
//	Desc:	Multi-select GridView control.
//          REF: MSDN: May, 2006, Cutting Edge- Extending the GridView Control
//               http://msdn.microsoft.com/en-us/magazine/cc163612.aspx
//	Rev:	07/29/08 (jph)- added member mPageChanged; modified OnPageIndexChanged(),
//                          OnDataBound() in order to ensure Session[SESSION_DATAKEYS]
//                          is cleared when data binding is other than from paging.
//          09/26/08 (jph)- corrected bug with selection memory in OnDataBound(),
//                          OnPageIndexChanged(), and OnPageIndexChanging()- had to get
//                          the mPageChanged flag in the right places.
//	---------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Drawing;
using System.Web;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace Argix.Web.UI.WebControls {
    //Custom GridView
    [DefaultProperty("Text")]
    [Themeable(true)]
    [ToolboxData("<{0}:GridViewM runat='server'></{0}:GridViewM>")]
    public partial class GridViewM : System.Web.UI.WebControls.GridView {
        //Members
        private bool mPageChanged=false;
        private const string GRIDVIEWM_JS = "Argix.Web.UI.WebControls.GridViewM.js";

        //Interface
        /// <summary>Multi-select GridView control.</summary>
        public GridViewM() { }
        #region Properties: AutoGenerateCheckBoxColumn, CheckBoxColumnIndex, CheckAllCheckBoxVisible, GetSelectedIndices()
        /// <summary>Whether a checkbox column is generated automatically at runtime.</summary>
        [Category("Behavior")]
        [Description("Whether a checkbox column is generated automatically at runtime.")]
        [DefaultValue(false)]
        public bool AutoGenerateCheckBoxColumn {
            get {
                object o = ViewState["AutoGenerateCheckBoxColumn"];
                if (o == null)
                    return false;
                return (bool)o;
            }
            set { ViewState["AutoGenerateCheckBoxColumn"] = value; }
        }
        /// <summary>Whether a check all checkbox is visible to the column header.</summary>
        [Category("Behavior")]
        [Description("Whether a check all checkbox is visible to the column header.")]
        [DefaultValue(true)]
        public bool CheckAllCheckBoxVisible {
            get {
                object o = ViewState["CheckAllCheckBoxVisible"];
                if(o == null)
                    return true;
                return (bool)o;
            }
            set { ViewState["CheckAllCheckBoxVisible"] = value; }
        }
        /// <summary>Indicates the 0-based position of the checkbox column.</summary>
        [Category("Behavior")]
        [Description("Indicates the 0-based position of the checkbox column.")]
        [DefaultValue(0)]
        public int CheckBoxColumnIndex {
            get {
                object o = ViewState["CheckBoxColumnIndex"];
                if(o == null)
                    return 0;
                return (int)o;
            }
            set { ViewState["CheckBoxColumnIndex"] = (value < 0 ? 0 : value); }
        }
        /// <summary>Gets a collection of DataKey objects for the checked rows.</summary>
        [Category("Behavior")]
        [Description("Gets a collection of DataKey objects for the checked rows.")]
        public virtual Hashtable SelectedDataKeys {
            get {
                //Save checked state of current page (in case not done by a page event)
                saveCheckedState();
                Hashtable dataKeys = new Hashtable();
                if(Page.Session[SESSION_DATAKEYS] != null) dataKeys = (Hashtable)Page.Session[SESSION_DATAKEYS];
                return dataKeys;
            }
        }
        #endregion
        #region Member Overrides: CreateColumns(), OnLoad(), OnPreRender(), OnDataBound(), OnPageIndexChanging(), OnPageIndexChanged()
        /// <summary>Creates the set of column fields used to build the control hierarchy.</summary>
        /// <param name="dataSource">A System.Web.UI.WebControls.PagedDataSource that represents the data source.</param>
        /// <param name="useDataSource">true to use the data source specified by the dataSource parameter; otherwise, false.</param>
        /// <returns>A System.Collections.ICollection that contains the fields used to build the control hierarchy.</returns>
        protected override ICollection CreateColumns(PagedDataSource dataSource, bool useDataSource) {
            //GridView creates a default set of columns
            ICollection columnList = base.CreateColumns(dataSource, useDataSource);
            if(!AutoGenerateCheckBoxColumn) return columnList;

            //Add a checkbox column if required
            ArrayList extendedColumnList = AddCheckBoxColumn(columnList);
            return extendedColumnList;
        }
        /// <summary>Raises the System.Web.UI.WebControls.GridView.OnLoad event.</summary>
        /// <param name="e">An System.EventArgs that contains event data.</param>
        protected override void OnLoad(EventArgs e) {
            //Override of GridView::OnLoad() event handler
            base.OnLoad(e);
            
            //Ensure client-side JavaScript is available
            Type t = this.GetType();
            string url = Page.ClientScript.GetWebResourceUrl(t, GRIDVIEWM_JS);
            if(!Page.ClientScript.IsClientScriptIncludeRegistered(t, GRIDVIEWM_JS))
                Page.ClientScript.RegisterClientScriptInclude(t, GRIDVIEWM_JS, url);
        }
        /// <summary>Raises the System.Web.UI.WebControls.GridView.OnPreRender event.</summary>
        /// <param name="e">An System.EventArgs that contains event data.</param>
        protected override void OnPreRender(EventArgs e)  {
            //Override of GridView::OnPreRender() event handler
            base.OnPreRender(e);
            
            //Adjust header row
            if(this.HeaderRow != null) {
                Control ctl = this.HeaderRow.FindControl(InputCheckBoxField.CHECKBOXID);
                if(ctl != null) {
                    CheckBox hcb = (CheckBox)ctl;
                }
            }

            //Adjust each data row
            foreach(GridViewRow row in Rows) {
                //Get the appropriate style object for the row
                TableItemStyle tableStyle = GetRowStyleFromState(row.RowState);

                //Retrieve a reference to the checkbox, and build the ID of the checkbox in the header
                //Add script code to enable selection
                CheckBox cb = (CheckBox)row.FindControl(InputCheckBoxField.CHECKBOXID);
                cb.Attributes["onclick"] = String.Format("ApplyStyle(this, '{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                        ColorTranslator.ToHtml(this.SelectedRowStyle.ForeColor),
                        ColorTranslator.ToHtml(this.SelectedRowStyle.BackColor),
                        ColorTranslator.ToHtml(tableStyle.ForeColor),
                        ColorTranslator.ToHtml(tableStyle.BackColor),
                        (tableStyle.Font.Bold ? 700 : 400),String.Format(CHECKBOXCOLUMHEADERID,this.ClientID));
                
                //Update the style of the row if checked
                row.BackColor = (cb.Checked) ? this.SelectedRowStyle.BackColor : tableStyle.BackColor;
                row.ForeColor = (cb.Checked) ? this.SelectedRowStyle.ForeColor : tableStyle.ForeColor;
                row.Font.Bold = (cb.Checked) ? this.SelectedRowStyle.Font.Bold : tableStyle.Font.Bold;
            }
        }
        /// <summary>Raises the System.Web.UI.WebControls.GridView.OnDataBound event.</summary>
        /// <param name="e">An System.EventArgs that contains event data.</param>
        protected override void OnDataBound(EventArgs e) {
            //Override of GridView::OnDataBound() event handler
            base.OnDataBound(e);

            //Clear checked rows memory if there are no rows (i.e. binding changed)
            if(!this.mPageChanged || this.Rows.Count == 0) Page.Session[SESSION_DATAKEYS] = null;
        }
        /// <summary>Raises the System.Web.UI.WebControls.GridView.PageIndexChanging event.</summary>
        /// <param name="e">A System.Web.UI.WebControls.GridViewPageEventArgs that contains event data.</param>
        protected override void OnPageIndexChanging(GridViewPageEventArgs e) {
            //Keep track of selected rows, then set new page
            base.OnPageIndexChanging(e);
            this.mPageChanged = true;
            saveCheckedState();
            this.PageIndex = e.NewPageIndex;
        }
        /// <summary>Raises the System.Web.UI.WebControls.GridView.PageIndexChanged event.</summary>
        /// <param name="e">An System.EventArgs that contains event data.</param>
        protected override void OnPageIndexChanged(EventArgs e) {
            //Update check marks for selected rows
            base.OnPageIndexChanged(e);
            this.mPageChanged = false;
            loadCheckedState();
        }
        #endregion
   }
}