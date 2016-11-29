<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="USPSTrackingClient.aspx.cs" Inherits="ArgixDirect._USPSTrackingClient" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Argix Direct USPS Carton Tracking</title>
</head>
<body style="font-size: 8pt; color: buttontext; font-family: Verdana; background-color: buttonface;">
    <form id="form1" runat="server">
        <table width="100%" height="100%" cellpadding="0" cellspacing="0" border="0" bgcolor="buttonface">
            <tr><td style="font-weight: bold; font-size: 10pt; font-style: italic">
                USPS Track & Confirm Test Client (http://production.shippingapis.com/shippingAPI.dll)</td></tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td valign="top">
                    <table border="0" cellpadding="0" cellspacing="3" height="100%" width="100%">
                        <tr>
                            <td align="right"> Carton#:</td>
                            <td colspan="3"><asp:TextBox ID="txtCartonNum" runat="server" Width="342px" ValidationGroup="Tracking" OnTextChanged="OnCartonChanged"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCartonNum" runat="server" ErrorMessage="Please enter a carton number"
                                    Height="8px" SetFocusOnError="True" ValidationGroup="Tracking" Width="8px" ControlToValidate="txtCartonNum">*</asp:RequiredFieldValidator>
                                <asp:CheckBox ID="chkFields" runat="server" Checked="True" Text="Field Request" Width="120px" /></td>
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
            <tr><td><asp:TextBox ID="txtCartonDetail" runat="server" Height="100%" Width="99%" TextMode="MultiLine" BorderStyle="Inset" BorderWidth="1px" EnableTheming="True" Rows="20" Wrap="False" Font-Names="Courier New" Font-Size="8pt" EnableViewState="False" ></asp:TextBox></td></tr>
            <tr><td style="height: 24px">&nbsp;</td></tr>
         </table>
    </form>
</body>
</html>
