<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="OutboundTrailerWeightAndCube.aspx.cs" Inherits="OutboundTrailerWeightAndCube" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0px" cellspacing="3px">
        <tr style="font-size:1px"><td width="96px">&nbsp;</td><td width="384px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td colspan="2">&nbsp;<uc1:DualDateTimePicker ID="ddpBOL" runat="server" Width="336px" LabelWidth="96px" FromLabel="BOL Start" ToLabel="BOL End" DateDaysBack="90" DateDaysForward="0" DateDaysSpread="7" OnDateTimeChanged="OnFromToDateChanged" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
         <tr>
            <td align="right">Terminal&nbsp;</td>
            <td>
                <asp:DropDownList id="cboTerminal" runat="server" Width="192px" AppendDataBoundItems="true" DataSourceID="odsTerminals" DataTextField="Description" DataValueField="TerminalCode" AutoPostBack="True" OnSelectedIndexChanged="OnTerminalChanged">
                    <asp:ListItem Text="All" Value="" Selected="True"></asp:ListItem>
                </asp:DropDownList>
                <asp:ObjectDataSource ID="odsTerminals" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetTerminals" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="900" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
         <tr>
            <td align="right">Agent&nbsp;</td>
            <td>
                <asp:DropDownList ID="cboAgent" runat="server" Width="192px" AppendDataBoundItems="true" DataSourceID="odsAgents" DataTextField="AgentName" DataValueField="AgentNumber" AutoPostBack="True" OnSelectedIndexChanged="OnAgentChanged">
                    <asp:ListItem Text="All" Value="" Selected="True" />
                </asp:DropDownList>
                <asp:ObjectDataSource ID="odsAgents" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetAgents" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="900" >
                    <SelectParameters>
                        <asp:Parameter Name="mainZoneOnly" DefaultValue="True" Type="Boolean" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Report Type&nbsp;</td>
            <td>
                <asp:DropDownList ID="cboType" runat="server" Width="192px" AutoPostBack="true" OnSelectedIndexChanged="OnTypeChanged" >
                    <asp:ListItem Text="BOL Weight Cube Report" Value="uspRptBOLWeightCubeReport" Selected="True" />
                    <asp:ListItem Text="BOL Weight Cube Report By Client" Value="uspRptBOLWeightCubeReportByClient" />
               </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
   </table>
</asp:Panel>
</asp:Content>

