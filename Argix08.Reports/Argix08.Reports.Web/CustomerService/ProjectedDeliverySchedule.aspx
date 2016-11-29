<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="ProjectedDeliverySchedule.aspx.cs" Inherits="ProjectedDeliverySchedule" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="idDateRange" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0" cellspacing="3px">
        <tr style="font-size:1px"><td width="96px">&nbsp;</td><td width="480px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td colspan="2">&nbsp;<uc1:DualDateTimePicker ID="ddpScheduleDate" runat="server" Width="336px" LabelWidth="90px" DateDaysBack="90" DateDaysForward="90" DateDaysSpread="21" EnableViewState="true" OnDateTimeChanged="OnFromToDateChanged" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Client&nbsp;</td>
            <td>
                <asp:DropDownList id="cboClient" runat="server" Width="288px" DataSourceID="odsClients" DataTextField="ClientName" DataValueField="ClientNumber" AutoPostBack="True" OnSelectedIndexChanged="OnClientChanged"></asp:DropDownList>
                <asp:ObjectDataSource ID="odsClients" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetSecureClients" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="600">
                    <SelectParameters>
                        <asp:ControlParameter Name="activeOnly" ControlID="chkActiveOnly" PropertyName="Checked" Type="Boolean" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                &nbsp;&nbsp;
                <asp:CheckBox ID="chkActiveOnly" runat="server" Width="96px" Text="Active Only" Checked="true" AutoPostBack="true" OnCheckedChanged="OnActiveOnlyChecked" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Agent&nbsp;</td>
            <td>
                <asp:DropDownList ID="cboAgent" runat="server" Width="288px" AppendDataBoundItems="true" DataSourceID="odsAgents" DataTextField="AgentSummary" DataValueField="AgentNumber" OnSelectedIndexChanged="OnAgentChanged">
                    <asp:ListItem Text="All" Value="" Selected="True"></asp:ListItem>
                </asp:DropDownList>
                <asp:ObjectDataSource ID="odsAgents" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetAgents" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="600">
                    <SelectParameters>
                        <asp:Parameter Name="mainZoneOnly" DefaultValue="false" Type="Boolean" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
   </table>
</asp:Panel>
</asp:Content>
