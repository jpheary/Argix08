<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IssueDetail.aspx.cs" Inherits="IssueDetail" StylesheetTheme="Argix" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Issue Detail</title>
</head>
<body style="background-color:Window">
<form id="form1" runat="server">
<div>
    <asp:Table ID="tblPage" runat="server" Width="100%" Height="100%" BorderStyle="None" BorderWidth="0px" CellPadding="0" CellSpacing="0">
        <asp:TableRow Font-Size="1px" Height="1px"><asp:TableCell>&nbsp;</asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell Font-Size="1px" Height="6px">&nbsp;</asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell>Issue Type:&nbsp;<asp:Label runat="server" ID="lblType"></asp:Label></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell Font-Size="1px" Height="3px">&nbsp;</asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell>Subject:&nbsp;<asp:Label runat="server" ID="lblSubject"></asp:Label></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell Font-Size="1px" Height="3px">&nbsp;</asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell>Contact:&nbsp;<asp:Label runat="server" ID="lblContact"></asp:Label></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell Font-Size="1px" Height="12px">&nbsp;</asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell>Company:&nbsp;<asp:Label runat="server" ID="lblCompany"></asp:Label></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell Font-Size="1px" Height="3px">&nbsp;</asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell>Store:&nbsp;<asp:Label runat="server" ID="lblStore"></asp:Label></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell Font-Size="1px" Height="3px">&nbsp;</asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell>Agent:&nbsp;<asp:Label runat="server" ID="lblAgent"></asp:Label></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell Font-Size="1px" Height="3px">&nbsp;</asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell>Zone:&nbsp;<asp:Label runat="server" ID="lblZone"></asp:Label></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell Font-Size="1px" Height="24px">&nbsp;</asp:TableCell></asp:TableRow>
    </asp:Table>
</div>
</form>
</body>
</html>
