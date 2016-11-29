<%@ Page Language="C#" MasterPageFile="~/MasterPages/Tracking.master" AutoEventWireup="true" CodeFile="TrackByPOPRO.aspx.cs" Inherits="TrackByPOPRO" Title="Track By PO/PRO" %>
<%@ MasterType VirtualPath="~/MasterPages/Tracking.master" %>

<asp:Content ID="cTitle" ContentPlaceHolderID="cpTitle" Runat="Server">
    <div class="PageTitle"><asp:Image ID="imgTitle" runat="server" ImageUrl="~/App_Themes/ArgixDirect/Images/tracking.gif" ImageAlign="AbsMiddle" />&nbsp;Track By PO/PRO</div>
</asp:Content>
<asp:Content ID="cForm" ContentPlaceHolderID="cpForm" Runat="Server">
    <table id="tblPage" width="100%" border="0px" cellpadding="0px" cellspacing="0px">
        <tr style="font-size:1px; height:32px"><td width="96px">&nbsp;</td><td width="384px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td align="right">Client&nbsp;</td>
            <td>
                <asp:DropDownList ID="cboClient" runat="server" Width="288px" DataSourceID="odsClients" DataTextField="CompanyName" DataValueField="ClientID"></asp:DropDownList>
                <asp:ObjectDataSource ID="odsClients" runat="server" TypeName="TrackingServices" SelectMethod="GetSecureClients" EnableCaching="false"></asp:ObjectDataSource>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Tracking#&nbsp;</td>
            <td><asp:TextBox ID="txtNumber" runat="server" Width="192px" MaxLength="30"></asp:TextBox></td>
            <td>&nbsp;<asp:RequiredFieldValidator ID="storeFieldValidator" runat="server" ControlToValidate="txtNumber" EnableViewState="False" ErrorMessage="Please enter a valid tracking number." ForeColor="" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Search By&nbsp;</td>
            <td>
                <asp:DropDownList ID="cboSearchType" runat="server" Width="144px">
                    <asp:ListItem Text="PRO Number" Value="PRO Number" Selected="True" />
                    <asp:ListItem Text="PO Number" Value="PO Number" />
                </asp:DropDownList></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr><td colspan="3" align="right">&nbsp;<asp:Button ID="btnTrack" runat="server" Text="Track" Height="24px" Width="72px" OnClick="OnTrack" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr><td colspan="3" align="left"><asp:ValidationSummary ID="trackValidationSummary" runat="server" Width="100%" DisplayMode="SingleParagraph" /></td></tr>
    </table>
</asp:Content>
