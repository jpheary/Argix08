<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="CBOLInvoiceSummary.aspx.cs" Inherits="CBOLInvoiceSummary" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cSetup" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0px" cellspacing="3px">
        <tr style="font-size:1px"><td width="96px">&nbsp;</td><td width="384px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td colspan="2">&nbsp;
                <uc1:DualDateTimePicker ID="ddpSetup" runat="server" Width="332px" LabelWidth="90px" DateDaysBack="365" DateDaysForward="0" DateDaysSpread="31" OnDateTimeChanged="OnFromToDateChanged" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Client&nbsp;</td>
            <td>
                <asp:DropDownList id="cboClient" runat="server" Width="288px" DataSourceID="odsClients" DataTextField="ClientName" DataValueField="ClientNumber" AutoPostBack="True" OnSelectedIndexChanged="OnClientChanged"></asp:DropDownList>
                <asp:ObjectDataSource ID="odsClients" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetClientsList" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="600">
                    <SelectParameters>
                        <asp:Parameter Name="activeOnly" DefaultValue="false" Type="Boolean" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
    </table>
</asp:Panel>
</asp:Content>
