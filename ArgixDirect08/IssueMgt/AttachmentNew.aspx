<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AttachmentNew.aspx.cs" Inherits="AttachmentNew" StylesheetTheme="Argix" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Action</title>
</head>
<body class="dialogbody">
<form id="form1" runat="server">
<div>
<table ID="tblPage" width="672px" height="100%" border="0px" cellpadding="0px" cellspacing="0px">
    <tr style="font-size:1px"><td width="96px">&nbsp;</td><td>&nbsp;</td></tr>
    <tr><td colspan="2" class="WindowTitle"><asp:Label ID="lblTitle" runat="server" Height="18px" Width="100%" Text="New Attachment" SkinID="PageTitle"></asp:Label></td></tr>
    <tr style="font-size:1px; height:6px"><td colspan="2">&nbsp;</td></tr>
    <tr>
        <td align="right" valign="top">Attachment&nbsp;</td>
        <td><asp:FileUpload ID="fuAttachment" runat="server" Width="384px" ToolTip="Select a file for attachment..." /></td>
    </tr>
    <tr style="font-size:1px; height:6px"><td colspan="2">&nbsp;</td></tr>
    <tr>
        <td>&nbsp;</td>
        <td align="right">
            <asp:Button ID="btnOk" runat="server" Text="   OK   " ToolTip="Create new action" Height="20px" Width="96px" UseSubmitBehavior="False" CommandName="OK" OnCommand="OnCommandClick" />
            &nbsp;
            <asp:Button ID="btnCancel" runat="server" Text=" Cancel " ToolTip="Cancel new action" Height="20px" Width="96px" UseSubmitBehavior="False" CommandName="Cancel" OnCommand="OnCommandClick" />
        </td>
    </tr>
</table>
</div>
</form>
</body>
</html>
