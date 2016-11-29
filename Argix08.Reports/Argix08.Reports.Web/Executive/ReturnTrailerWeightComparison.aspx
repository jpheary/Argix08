<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="ReturnTrailerWeightComparison.aspx.cs" Inherits="ReturnTrailerWeightComparison" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0px" cellspacing="3px">
        <tr style="font-size:1px"><td width="126px">&nbsp;</td><td width="384px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td colspan="2">&nbsp;<uc1:DualDateTimePicker ID="ddpArrival" runat="server" Width="336px" LabelWidth="126px" FromLabel="TDS Arrival Start" ToLabel="TDS Arrival End" DateDaysBack="365" DateDaysForward="0" DateDaysSpread="90" OnDateTimeChanged="OnFromToDateChanged" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
         <tr>
            <td align="right">Agent&nbsp;</td>
            <td>
                <asp:DropDownList ID="cboAgent" runat="server" Width="246px" AppendDataBoundItems="true" DataSourceID="odsAgents" DataTextField="AgentName" DataValueField="AgentNumber" OnSelectedIndexChanged="OnAgentChanged">
                    <asp:ListItem Text="All" Value="" Selected="True" />
                </asp:DropDownList>
                <asp:ObjectDataSource ID="odsAgents" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetAgents" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="600">
                    <SelectParameters>
                        <asp:Parameter Name="mainZoneOnly" DefaultValue="true" Type="Boolean" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
   </table>
</asp:Panel>
</asp:Content>