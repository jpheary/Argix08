<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="BNConsumerDirectWeeklyDiscrepancySummary.aspx.cs" Inherits="BNConsumerDirectWeeklyDiscrepancySummary" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="Panel1" runat="server" Width="100%" Height="100%" GroupingText="Setup">
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
   </table>
</asp:Panel>
</asp:Content>

