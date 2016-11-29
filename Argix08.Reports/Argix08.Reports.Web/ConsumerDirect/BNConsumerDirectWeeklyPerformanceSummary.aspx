<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="BNConsumerDirectWeeklyPerformanceSummary.aspx.cs" Inherits="BNConsumerDirectWeeklyPerformanceSummary" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0" cellspacing="3px">
        <tr style="font-size:1px"><td width="144px">&nbsp;</td><td width="288px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td align="right">Calendar Week&nbsp;</td>
            <td>
                <asp:DropDownList id="cboDateValue" runat="server" Width="240px" DataSourceID="odsDates" DataTextField="Value" DataValueField="Value"></asp:DropDownList>
                <asp:ObjectDataSource ID="odsDates" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetSortDates" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="600" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Type&nbsp;</td>
            <td>
                <asp:DropDownList ID="cboType" runat="server" Width="96px" AutoPostBack="true" OnSelectedIndexChanged="OnTypeChanged">
                    <asp:ListItem Text="DDU" Value="DDU" Selected="true" />
                    <asp:ListItem Text="NDC" Value="NDC" />
                    <asp:ListItem Text="SCF" Value="SCF" />
                </asp:DropDownList><br />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">&nbsp;</td>
            <td><asp:CheckBox ID="chkZips" runat="server" Width="192px" Text="All Zip Codes" Checked="true" /></td>
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

