<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="ConsumerDirectDDUWeeklySummaryByAgent.aspx.cs" Inherits="ConsumerDirectDDUWeeklySummaryByAgent" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0" cellspacing="3px">
        <tr style="font-size:1px"><td width="120px">&nbsp;</td><td width="288px">&nbsp;</td><td>&nbsp;</td></tr>
         <tr>
            <td align="right">Span&nbsp;</td>
            <td>
                <asp:DropDownList id="cboDateParam" runat="server" Width="96px" AutoPostBack="True" OnSelectedIndexChanged="OnDateParamChanged">
                    <asp:ListItem Selected="True">Week</asp:ListItem>
                </asp:DropDownList>            
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Est. Sort Date&nbsp;</td>
            <td>
                <asp:DropDownList id="cboDateValue" runat="server" Width="240px" DataSourceID="odsDates" DataTextField="Value" DataValueField="Value"></asp:DropDownList>
                <asp:ObjectDataSource ID="odsDates" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetSortDates" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="600" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
         <tr>
            <td align="right">Delivery Days&nbsp;</td>
            <td>
                <asp:DropDownList id="cboDelivDays" runat="server" Width="48px">
                    <asp:ListItem Text="0" Value="0" />
                    <asp:ListItem Text="1" Value="1" />
                    <asp:ListItem Text="2" Value="2" />
                    <asp:ListItem Text="3" Value="3" />
                    <asp:ListItem Text="4" Value="4" Selected="True" />
                    <asp:ListItem Text="5" Value="5" />
                    <asp:ListItem Text="6" Value="6" />
                    <asp:ListItem Text="7" Value="7" />
                    <asp:ListItem Text="8" Value="8" />
                    <asp:ListItem Text="9" Value="9" />
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Customer&nbsp;</td>
            <td>
                <asp:DropDownList id="cboCustomer" runat="server" Width="240px" DataSourceID="odsCustomers" DataTextField="NumberandName" DataValueField="NUMBER" OnSelectedIndexChanged="OnCustomerChanged"></asp:DropDownList>
                <asp:ObjectDataSource ID="odsCustomers" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetConsumerDirectCustomers" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="600" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
    </table>
</asp:Panel>
</asp:Content>
