<%@ Page Language="C#" MasterPageFile="~/MasterPages/Tracking.master" AutoEventWireup="true" CodeFile="TrackByCarton.aspx.cs" Inherits="Members_TrackByCarton" Title="Track By Carton" %>
<%@ MasterType VirtualPath="~/MasterPages/Tracking.master" %>

<asp:Content ID="cTitle" ContentPlaceHolderID="cpTitle" Runat="Server">
    <div class="PageTitle"><asp:Image ID="imgTitle" runat="server" ImageUrl="~/App_Themes/ArgixDirect/Images/tracking.gif" ImageAlign="AbsMiddle" />&nbsp;Track By Carton</div>
</asp:Content>
<asp:Content ID="cForm" ContentPlaceHolderID="cpForm" Runat="Server">
    <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
        <tr style="font-size:1px; height:32px"><td width="96px">&nbsp;</td><td width="384px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td align="right">Track By&nbsp;</td>
            <td>
                <asp:DropDownList ID="cboSearchBy" runat="server" Width="168px">
                    <asp:ListItem Text="Carton Number" Value="CartonNumber" Selected="True" />
                    <asp:ListItem Text="Argix Label Number" Value="LabelNumber" />
                    <asp:ListItem Text="License Plate Number" Value="PlateNumber" />
                </asp:DropDownList></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right" valign="top">Tracking #&nbsp;</td>
            <td><asp:TextBox ID="txtNumbers" runat="server" Width="240px" MaxLength="400" Rows="11" TextMode="MultiLine"></asp:TextBox></td>
            <td><asp:RequiredFieldValidator ID="rfvTracking" runat="server" ControlToValidate="txtNumbers" ErrorMessage="Please enter at least one tracking number.">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr><td>&nbsp;</td><td colspan="2">Track up to ten cartons at a time.<br />Enter one tracking# per line, or separate each with a comma.</td></tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td colspan="3" align="right"><asp:Button ID="btnTrack" runat="server" Width="72px" Height="24px" Text="Track" OnClick="OnTrack" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr><td colspan="3" align="left"><asp:ValidationSummary ID="vsTracking" runat="server" /></td></tr>
    </table>
</asp:Content>

