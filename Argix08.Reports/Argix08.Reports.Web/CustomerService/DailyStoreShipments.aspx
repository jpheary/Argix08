<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="DailyStoreShipments.aspx.cs" Inherits="DailyStoreShipments" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ Register Src="../ClientDivisionGrids.ascx" TagName="ClientDivisionGrids" TagPrefix="uc6" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0px" cellspacing="3px">
        <tr style="font-size:1px"><td width="96px">&nbsp;</td><td width="384px">&nbsp;</td><td>&nbsp;</td></tr>
         <tr>
            <td colspan="2">&nbsp;<uc1:DualDateTimePicker ID="ddpShipping" runat="server" Width="336px" LabelWidth="96px" DateDaysBack="90" DateDaysForward="0" DateDaysSpread="30" EnableViewState="true" OnDateTimeChanged="OnFromToDateChanged" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Division&nbsp;</td>
            <td><asp:TextBox ID="txtDivision" runat="server"></asp:TextBox></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr><td colspan="3"><uc6:ClientDivisionGrids ID="dgdClientDivision" runat="server" Height="288px" OnAfterClientSelected="OnClientSelected" OnAfterDivisionSelected="OnDivisionSelected" /></td></tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
   </table>
</asp:Panel>
</asp:Content>

