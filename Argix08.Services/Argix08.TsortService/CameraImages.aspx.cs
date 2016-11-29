using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.Freight;

public partial class CameraImages : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //
    }
    protected void OnImageSelected(object sender, EventArgs e) {
        //
        if (this.grdImages.SelectedDataKey != null) {
            int id = int.Parse(this.grdImages.SelectedDataKey.Values["ID"].ToString());
            this.imgPicture.ImageUrl = "~/Image.aspx?id=" + id.ToString();
        }
    }
}
