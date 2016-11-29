<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="TimePeriodCartonCompare.aspx.cs" Inherits="TimePeriodCartonCompare" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ Register Src="../ClientVendorGrids.ascx" TagName="ClientVendorGrids" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0px" cellspacing="3px">
        <tr style="font-size:1px"><td width="96px">&nbsp;</td><td width="384px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td colspan="2">&nbsp;<uc1:DualDateTimePicker ID="ddpPickups" runat="server" Width="384px" LabelWidth="96px" DateDaysBack="365" DateDaysForward="0" DateDaysSpread="31" OnDateTimeChanged="OnFromToDateChanged" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Type&nbsp;</td>
            <td>
                <asp:DropDownList ID="cboType" runat="server" Width="144px">
                    <asp:ListItem Text="Over" Value="Over" Selected="True" />
                    <asp:ListItem Text="Short" Value="Short" />
                </asp:DropDownList>            
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">&nbsp;</td>
            <td><asp:CheckBox ID="chkAllDivs" runat="server" Width="288px" Text="All Divisions (shown as '01')" Checked="true" TextAlign="Right" AutoPostBack="true" OnCheckedChanged="OnAllDivsCheckedChanged" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr><td colspan="3">&nbsp;<uc2:ClientVendorGrids ID="dgdClientVendor" runat="server" Height="270px" ClientDivision="01" OnAfterClientSelected="OnClientSelected" OnAfterVendorSelected="OnVendorSelected" /></td></tr>
   </table>
</asp:Panel>
</asp:Content>

