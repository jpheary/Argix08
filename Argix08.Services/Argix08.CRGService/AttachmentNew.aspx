<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AttachmentNew.aspx.cs" Inherits="AttachmentNew" StylesheetTheme="Argix" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Action</title>
</head>
<body class="dialogbody">
<form id="form1" runat="server">
<div>
<asp:Table ID="tblPage" runat="server" Width="672px" Height="100%" BorderStyle="None" BorderWidth="0px" CellPadding="0" CellSpacing="0">
    <asp:TableRow><asp:TableCell Font-Size="1px" Width="96px">&nbsp;</asp:TableCell><asp:TableCell Font-Size="1px">&nbsp;</asp:TableCell></asp:TableRow>
    <asp:TableRow>
        <asp:TableCell ColumnSpan="2"><asp:Label ID="lblTitle" runat="server" Height="18px" Width="100%" Text="New Action" SkinID="PageTitle"></asp:Label></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow><asp:TableCell ColumnSpan="2" Font-Size="1px" Height="60px">&nbsp;</asp:TableCell></asp:TableRow>
    <asp:TableRow>
        <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top">Attachment&nbsp;</asp:TableCell>
        <asp:TableCell>
            <asp:FileUpload ID="fuAttachment" runat="server" ToolTip="Select a file for attachment..." />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow><asp:TableCell ColumnSpan="2" Font-Size="1px" Height="6px">&nbsp;</asp:TableCell></asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>&nbsp;</asp:TableCell>
        <asp:TableCell HorizontalAlign="Right">
            <asp:Button ID="btnOk" runat="server" Text="   OK   " ToolTip="Create new action" Height="20px" Width="96px" UseSubmitBehavior="False" OnClick="OnButtonClick" />
            &nbsp;
            <asp:Button ID="btnCancel" runat="server" Text=" Cancel " ToolTip="Cancel new action" Height="20px" Width="96px" UseSubmitBehavior="False" OnClick="OnButtonClick" />
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
</div>
</form>
</body>
</html>
