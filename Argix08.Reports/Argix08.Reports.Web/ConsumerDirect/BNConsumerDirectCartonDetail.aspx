<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="BNConsumerDirectCartonDetail.aspx.cs" Inherits="BNConsumerDirectCartonDetail" %>
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
                    <asp:ListItem>0</asp:ListItem>
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem Selected="True">4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>6</asp:ListItem>
                    <asp:ListItem>7</asp:ListItem>
                    <asp:ListItem>8</asp:ListItem>
                    <asp:ListItem>9</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
         <tr>
            <td align="right">Location Type&nbsp;</td>
            <td>
                <asp:DropDownList id="cboLocType" runat="server" Width="72px">
                    <asp:ListItem>BMC</asp:ListItem>
                    <asp:ListItem Selected="True">DDU</asp:ListItem>
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
