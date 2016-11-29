<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Argix Direct Carton Tracking</title>
</head>
<body style="font-size: 8pt; color: buttontext; font-family: Verdana; background-color: buttonface;">
    <form id="form1" runat="server">
        <table width="100%" height="100%" cellpadding="0" cellspacing="0" border="0" bgcolor="buttonface">
            <tr><td style="font-weight: bold; font-size: 12pt; text-transform: uppercase; font-style: italic">Tracking Web Service Client</td></tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td valign="top">
                    <table border="0" cellpadding="0" cellspacing="3" height="100%" width="100%">
                        <tr>
                            <td align="right">Web Service:</td>
                            <td colspan="4">
                                <asp:DropDownList ID="cboService" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="OnServiceSelected">
                                </asp:DropDownList></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" width="96">UserID:</td>
                            <td width="144"><asp:TextBox ID="txtUserID" runat="server" Width="96px" ValidationGroup="Tracking"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvUserID" runat="server" ErrorMessage="Please enter a UserID"
                                    Height="8px" SetFocusOnError="True" ValidationGroup="Tracking" Width="8px" ControlToValidate="txtUserID">*</asp:RequiredFieldValidator></td>
                            <td align="right" width="96">Password:</td>
                            <td width="144"><asp:TextBox ID="txtPassword" runat="server" Width="96px" ValidationGroup="Tracking"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Please enter a password"
                                    Height="8px" SetFocusOnError="True" ValidationGroup="Tracking" Width="8px" ControlToValidate="txtPassword">*</asp:RequiredFieldValidator></td>
                            <td>&nbsp;</td>
                            <td width="24">&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right"> Carton#:</td>
                            <td colspan="3"><asp:TextBox ID="txtCartonNum" runat="server" Width="342px" ValidationGroup="Tracking"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCartonNum" runat="server" ErrorMessage="Please enter a carton number"
                                    Height="8px" SetFocusOnError="True" ValidationGroup="Tracking" Width="8px" ControlToValidate="txtCartonNum">*</asp:RequiredFieldValidator></td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr><td colspan="6">&nbsp;</td></tr>
                        <tr>
                            <td colspan="4"><asp:ValidationSummary ID="vsTracking" runat="server" DisplayMode="List" Height="100%" ValidationGroup="Tracking" Width="100%" /></td>
                            <td align="right"><asp:Button ID="btnTrack" runat="server" Text="Track" OnClick="OnTrack" Width="72px" ValidationGroup="Tracking" /></td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="font-size:5px"><td valign="middle"><hr size="3" width="100%" /></td></tr>
            <tr><td><asp:TextBox ID="txtCartonDetail" runat="server" Height="100%" Width="99%" TextMode="MultiLine" BorderStyle="Inset" BorderWidth="1px" EnableTheming="True" Rows="20" Wrap="False" Font-Names="Courier New" Font-Size="8pt"></asp:TextBox></td></tr>
            <tr><td style="height: 24px">&nbsp;</td></tr>
         </table>
    </form>
</body>
</html>
