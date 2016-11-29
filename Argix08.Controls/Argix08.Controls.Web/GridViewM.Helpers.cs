//	File:	GridViewM.Helpers.cs
//	Author:	J. Heary
//	Date:	06/05/08
//	Desc:	Multi-select GridView control partial class.
//          NOTE:   Uses first DataKeyName field as primary key; other fields are
//                  retained is Session state for later access to checked row data.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Drawing;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Argix.Web.UI.WebControls {
    /// <summary>Multi-select GridView control partial class with helper implementations. </summary>
    public partial class GridViewM :System.Web.UI.WebControls.GridView {
        //Members
        private const string SESSION_DATAKEYS = "GridViewMDataKeys";
        private const string CHECKBOXCOLUMHEADERTEMPLATE = "<input type='checkbox' hidefocus='true' id='{0}' name='{0}' {1} onclick='CheckAll(this, \"{2}\")'>";
        private const string CHECKBOXCOLUMHEADERID = "{0}_HeaderButton";
        
        //Interface
        /// <summary>Add a brand new checkbox column to the column collection. </summary>
        /// <param name="columnList">Grid column list.</param>
        /// <returns>An ArrayList containing grid columns.</returns>
        protected virtual ArrayList AddCheckBoxColumn(ICollection columnList) {
            //Add a brand new checkbox column to the column collection
            //Get a new container of type ArrayList that holds the given collection; this 
            //is required because ICollection doesn't include Add methods
            ArrayList list = new ArrayList(columnList);
            
            //Determine the check state for the header checkbox
            string checkBoxID = String.Format(CHECKBOXCOLUMHEADERID, this.ClientID);
            string shouldCheck="";
            if(!this.DesignMode) {
                object o = Page.Request[checkBoxID];
                if(o != null) shouldCheck = "checked=\"checked\"";
            }
            
            //Create a new custom CheckBoxField object
            //A custom data control class is used to display a checkbox in the column. ASP.NET 2.0 
            //has a brand new CheckBoxField class that seems perfect, so why not use it instead? The 
            //problem is that CheckBoxField puts a checkbox in each cell only if it is bound to a 
            //valid data source field. In this case, you do want a checkbox in each column, but you 
            //also need to keep the column unbound and use it only to collect some input.
            InputCheckBoxField field = new InputCheckBoxField();
            if(this.CheckAllCheckBoxVisible)
                field.HeaderText = String.Format(CHECKBOXCOLUMHEADERTEMPLATE, checkBoxID, shouldCheck, this.ClientID);
            field.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            field.HeaderStyle.Width = Unit.Pixel(18);
            field.HeaderStyle.Font.Size = FontUnit.Point(8);
            field.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            field.ItemStyle.Font.Size = FontUnit.Point(8);
            field.ReadOnly = true;

            //Insert the checkbox field into the list at the specified position
            if(CheckBoxColumnIndex > list.Count) {
                //If desired position exceeds the # of columns add the checkbox field to the right.
                //NOTE: This check can only be made here because only now we know exactly HOW 
                //      MANY columns we're going to have. Checking Columns.Count in the property 
                //      setter doesn't work if columns are auto-generated
                list.Add(field);
                CheckBoxColumnIndex = list.Count - 1;
            }
            else
                list.Insert(CheckBoxColumnIndex, field);
            return list;
        }
        /// <summary>Retrieve a style object based on the row state </summary>
        /// <param name="state">Requested row state (i.e. RowStyle, SelectedRowStyle, etc).</param>
        /// <returns>TableItemStyle object containing the appearance for the requested row state.</returns>
        protected virtual TableItemStyle GetRowStyleFromState(DataControlRowState state) {
            //Retrieve the style object based on the row state
            switch(state) {
                case DataControlRowState.Alternate: return this.AlternatingRowStyle;
                case DataControlRowState.Edit:      return this.EditRowStyle;
                case DataControlRowState.Selected:  return this.SelectedRowStyle;
                default:                            return this.RowStyle;
                //DataControlRowState.Insert is not relevant here
            }
        }
        #region Local Services: loadCheckedState(), saveCheckedState()
        private void loadCheckedState() {
            //Set checked state for each row
            
            //Determine the check state for the header checkbox
            string checkBoxID = String.Format(CHECKBOXCOLUMHEADERID,this.ClientID);
            bool shouldCheck=false;
            if(!this.DesignMode) {
                object o = Page.Request[checkBoxID];
                shouldCheck = (o != null);
            }

            Hashtable dataKeys = (Hashtable)Page.Session[SESSION_DATAKEYS];
            if(shouldCheck || (dataKeys != null && dataKeys.Count > 0)) {
                foreach(GridViewRow row in this.Rows) {
                    //Get the value of the data key for each row
                    //Set checked if found in the session cache or checkAll on
                    DataKey dataKey = (DataKey)this.DataKeys[row.RowIndex];
                    if(shouldCheck || dataKeys.ContainsKey(dataKey.Value)) {
                        CheckBox checkbox = (CheckBox)row.FindControl("CheckBoxButton");
                        checkbox.Checked = true;
                    }
                }
            }
        }
        private void saveCheckedState() {
            //Save the checked rows to Session state
            Hashtable dataKeys = new Hashtable();
            if(Page.Session[SESSION_DATAKEYS] != null) dataKeys = (Hashtable)Page.Session[SESSION_DATAKEYS];
            foreach(GridViewRow row in this.Rows) {
                //Get the value of the data key for this row and update session state
                bool isChecked = ((CheckBox)row.FindControl("CheckBoxButton")).Checked;
                DataKey dataKey = (DataKey)this.DataKeys[row.RowIndex];
                if(isChecked) {
                    if(!dataKeys.ContainsKey(dataKey.Value)) dataKeys.Add(dataKey.Value,dataKey);
                }
                else {
                    if(dataKeys.ContainsKey(dataKey.Value)) dataKeys.Remove(dataKey.Value);
                }
            }
            //Update selected in Session state
            Page.Session[SESSION_DATAKEYS] = dataKeys;
        }
        #endregion
    }
}