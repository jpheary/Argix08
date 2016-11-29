<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActionNew.aspx.cs" Inherits="ActionNew" StylesheetTheme="Argix" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Action</title>
</head>
<body class="dialogbody">
<form id="form1" runat="server">
<div>
<table id="tblPage" width="672px" height="100%" border="0px" cellpadding="0px" cellspacing="0px">
    <tr style="font-size:1px"><td width="96px">&nbsp;</td><td>&nbsp;</td></tr>
    <tr><td colspan="2" class="WindowTitle"><asp:Label ID="lblTitle" runat="server" Height="18px" Width="100%" Text="New Action" SkinID="PageTitle"></asp:Label></td></tr>
    <tr style="font-size:1px; height:6px"><td colspan="2">&nbsp;</td></tr>
    <tr>
        <td align="right" valign="top">Type&nbsp;</td>
        <td>
            <asp:DropDownList ID="cboActionType" runat="server" Width="144px" DataSourceID="odsActionTypes" DataTextField="Type" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="OnTypeChanged"></asp:DropDownList>
            <asp:ObjectDataSource ID="odsActionTypes" runat="server" SelectMethod="GetIssueActionTypes" TypeName="Argix.Customers.IssueMgtServiceClient">
                <SelectParameters>
                    <asp:QueryStringParameter Name="issueID" QueryStringField="issueID" DefaultValue="0" Type="Int64" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
    <tr style="font-size:1px; height:6px"><td colspan="2">&nbsp;</td></tr>
    <tr>
        <td align="right" valign="top">Comments&nbsp;</td>
        <td><asp:TextBox ID="txtComments" runat="server" Width="100%" Height="288px" BorderStyle="Inset" BorderWidth="2px" TextMode="MultiLine" AutoPostBack="True" OnTextChanged="OnCommentsChanged"></asp:TextBox></td>
    </tr>
    <tr style="font-size:1px; height:6px"><td colspan="2">&nbsp;</td></tr>
    <tr>
        <td>&nbsp;</td>
        <td align="right">
            <asp:Button ID="btnOk" runat="server" Width="96px" Height="20px" Text="   OK   " ToolTip="Create new action" UseSubmitBehavior="False" CommandName="OK" OnCommand="OnCommandClick" />
            &nbsp;
            <asp:Button ID="btnCancel" runat="server" Width="96px" Height="20px" Text=" Cancel " ToolTip="Cancel new action" UseSubmitBehavior="False" CommandName="Cancel" OnCommand="OnCommandClick" />
        </td>
    </tr>
</table>
</div>
</form>
</body>
</html>
