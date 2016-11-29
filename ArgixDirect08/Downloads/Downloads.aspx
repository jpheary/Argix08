<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Downloads.aspx.cs" Inherits="Downloads" MasterPageFile="~/MasterPages/Downloads.master" Title="Customer Downloads" StylesheetTheme="ArgixDirect" Theme="ArgixDirect" %>

<asp:Content  ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <asp:TreeView ID="trvDownloads" runat="server" BackColor="Window" BorderStyle="Inset"
            BorderWidth="1px" Font-Names="Verdana" Font-Size="10pt" ForeColor="WindowText"
            NodeIndent="6" PopulateNodesFromClient="False" ShowLines="True" Width="384px" BorderColor="WindowFrame" Height="100%">
            <Nodes>
                <asp:TreeNode Text="Customer Downloads" Value="Customer Downloads"></asp:TreeNode>
            </Nodes>
        </asp:TreeView>
    
    </div>
</asp:Content>
