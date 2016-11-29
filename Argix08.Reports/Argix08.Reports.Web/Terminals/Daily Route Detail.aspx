<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Daily Route Detail.aspx.cs" Inherits="DailyRouteDetail" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0" cellspacing="3px">
        <tr style="font-size:1px"><td width="72px">&nbsp;</td><td width="384px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td colspan="2">&nbsp;<uc1:DualDateTimePicker ID="ddpRouteDate" Width="332px" LabelWidth="66px" runat="server" FromLabel="" FromVisible="false" ToLabel="Route Date" DateDaysBack="31" DateDaysForward="0" DateDaysSpread="1" OnDateTimeChanged="OnFromToDateChanged" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
         <tr>
            <td align="right">Depot&nbsp;</td>
            <td>
                <asp:DropDownList ID="cboDepot" runat="server" Width="192px" AppendDataBoundItems="true" DataSourceID="odsDepots" DataTextField="Depotname" DataValueField="RS_OrderClass" OnTextChanged="OnDepotChanged">
                    <asp:ListItem Text="All" Value="" Selected="True"></asp:ListItem>
                </asp:DropDownList>
                <asp:ObjectDataSource ID="odsDepots" runat="server" TypeName="Argix.Terminals.RoadshowGateway" SelectMethod="GetDepots" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="600" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
    </table>
</asp:Panel>
</asp:Content>

