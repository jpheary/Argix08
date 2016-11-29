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
using System.IO;

public partial class Downloads:System.Web.UI.Page {
    //
    protected void Page_Load(object sender,EventArgs e) {
        //Eventa handler for page load event
        //Read customer id from query string
        string id = Request.QueryString["id"];
        if(!Page.IsPostBack) {
            //Enumerate files in the specified directory
            this.trvDownloads.Nodes.Clear();
            TreeNode parent = new TreeNode(id + " Downloads");
            this.trvDownloads.Nodes.Add(parent);
            string[] files = Directory.GetFiles(Server.MapPath(id + "/"),"*.*");
            foreach(string file in files) {
                //Add a treeview node for ewach file
                FileInfo info = new FileInfo(file);
                TreeNode node = new TreeNode(info.Name,info.Name);
                node.NavigateUrl = id + "/" + info.Name;
                parent.ChildNodes.Add(node);
            }
            this.trvDownloads.ExpandAll();
            this.trvDownloads.Nodes[0].Select();
        }
    }
}
