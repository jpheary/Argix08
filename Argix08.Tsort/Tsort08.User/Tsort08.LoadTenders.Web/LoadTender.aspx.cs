using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _LoadTender : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for form load event
        Response.Write("<html>");
        Response.Write("<head runat='server'>");
        Response.Write("<title>Load Tenders</title>");
        Response.Write("<style> HR { page-break-after: always; } </style>");
        Response.Write("</head>");
        Response.Write("<body>");

        LoadTenderDS ds = (LoadTenderDS)Session["LoadTenders"];
        for(int i=0;i<ds.LoadTenderTable.Rows.Count;i++) {
            Response.Write("<table width=\"600px\" border=\"0\">");
            Response.Write("<tr>");
            Response.Write("<td>Loc #" + ds.LoadTenderTable[i].LocationNumber.ToString() + "</td>");
            Response.Write("<td>" + ds.LoadTenderTable[i].Location.Trim() + "</td>");
            Response.Write("</tr>");
            Response.Write("<tr>");
            Response.Write("<td>ID #" + ds.LoadTenderTable[i].LocationID.Trim() + "</td>");
            Response.Write("<td>" + ds.LoadTenderTable[i].AddressLine1.Trim() + "</td>");
            Response.Write("</tr>");
            Response.Write("<tr>");
            Response.Write("<td>&nbsp;</td>");
            Response.Write("<td>" + ds.LoadTenderTable[i].AddressLine2.Trim() + "</td>");
            Response.Write("</tr>");
            Response.Write("<tr>");
            Response.Write("<td>Ref #" + ds.LoadTenderTable[i].ReferenceNumber.Trim() + "</td>");
            Response.Write("<td>" + ds.LoadTenderTable[i].City.Trim() + ", " + ds.LoadTenderTable[i].StateOrProvince.Trim() + " " + ds.LoadTenderTable[i].PostalCode.Trim() + "</td>");
            Response.Write("</tr>");
            Response.Write("</table>");
            Response.Write("<br />");

            Response.Write("<table width=\"600px\" border=\"0\">");
            Response.Write("<tr><td align=\"center\"><img src=\"Image.aspx?barcode=" + ds.LoadTenderTable[i].Load.Trim() + "\" /></td></tr>");
            Response.Write("<tr><td align=\"center\">" + ds.LoadTenderTable[i].Load.Trim() + "</td></tr>");
            Response.Write("</table>");
            Response.Write("<br />");
            
            if(i < ds.LoadTenderTable.Rows.Count - 1) Response.Write("<hr />");
        }
        Response.Write("</body>");
        Response.Write("</html>");
    }
}
