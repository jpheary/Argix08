using System;
using System.Diagnostics;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Argix.Web.UI.WebControls {
    //Custom CheckBoxField
    internal sealed class InputCheckBoxField :CheckBoxField {
        //Members
        public const string CHECKBOXID = "CheckBoxButton";

        //Interface
        public InputCheckBoxField() { }
        protected override void InitializeDataCell(DataControlFieldCell cell,DataControlRowState rowState) {
            //Override 
            base.InitializeDataCell(cell,rowState);
            if(cell.Controls.Count == 0) {
                //Add a checkbox anyway, if not done already
                CheckBox chk = new CheckBox();
                chk.ID = InputCheckBoxField.CHECKBOXID;
                cell.Controls.Add(chk);
            }
        }
    }
}
