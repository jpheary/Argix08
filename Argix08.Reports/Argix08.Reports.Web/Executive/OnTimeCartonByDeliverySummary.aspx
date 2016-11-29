<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="OnTimeCartonByDeliverySummary.aspx.cs" Inherits="OnTimeCartonByDeliverySummary" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ Register Src="../ClientAgentGrids.ascx" TagName="ClientAgentGrids" TagPrefix="uc4" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0px" cellspacing="3px">
        <tr style="font-size:1px"><td width="96px">&nbsp;</td><td width="384px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td colspan="2">&nbsp;<uc1:DualDateTimePicker ID="ddpDelivery" runat="server" Width="336px" LabelWidth="96px" DateDaysBack="365" DateDaysForward="0" DateDaysSpread="31" EnableViewState="true" OnDateTimeChanged="OnFromToDateChanged" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">On Time %&nbsp;</td>
            <td><asp:TextBox ID="txtPctOnTime" runat="server" Width="72px" Text="98"></asp:TextBox></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr><td colspan="3" align="right"><asp:CheckBox ID="chkAllAgents" runat="server" width="288px" Text="All Agents" TextAlign="Left" Checked="true" AutoPostBack="True" OnCheckedChanged="OnAllAgentsChecked" /></td></tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr><td colspan="3">&nbsp;<uc4:ClientAgentGrids ID="dgdClientAgent" runat="server" Height="264px" ClientDivision="01" ClientActiveOnly="true" ClientsEnabled="true" AgentsEnabled="false" OnAfterClientSelected="OnClientSelected" OnAfterAgentSelected="OnAgentSelected" /></td></tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
   </table>
</asp:Panel>
</asp:Content>
