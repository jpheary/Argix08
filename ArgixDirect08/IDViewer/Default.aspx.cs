using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default : System.Web.UI.Page {
    //Members
    
    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //        
        if(!Page.IsPostBack) {
            //TableRow row = new TableRow();
            //TableCell cell=null;
            //Button button=null;
            
            //Argix.KronosProxy kp = new Argix.KronosProxy();
            //object[] types = kp.GetIDTypes();
            //for(int i=0; i<types.Length; i++) {
            //    string type = types[i].ToString();
            //    cell = new TableCell();
            //    cell.Style.Add(HtmlTextWriterStyle.Width, "25%");
            //    button = new Button();
            //    button.ID = "btn" + type;
            //    button.Width = new Unit("100%");
            //    button.Height = new Unit("20px");
            //    button.Text = type;
            //    button.BorderStyle = BorderStyle.Solid;
            //    button.BorderWidth = new Unit("1px");
            //    button.Style.Add("border-bottom-style", "solid");
            //    button.BorderColor = System.Drawing.Color.Black;
            //    button.Command += new CommandEventHandler(OnChangeView);
            //    button.CommandName = type;
            //    cell.Controls.Add(button);
            //    row.Cells.Add(cell);
            //}
            //this.idDefault.Rows.Add(row);

            //((Button)this.idDefault.Rows[0].FindControl("btnDrivers")).BackColor = System.Drawing.Color.LightSteelBlue;
            //((Button)this.idDefault.Rows[0].FindControl("btnEmployees")).BackColor = System.Drawing.Color.LightSteelBlue;
            //((Button)this.idDefault.Rows[0].FindControl("btnHelpers")).BackColor = System.Drawing.Color.LightSteelBlue;
            //((Button)this.idDefault.Rows[0].FindControl("btnVendors")).BackColor = System.Drawing.Color.LightSteelBlue;
        }
    }
}
