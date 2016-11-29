<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="TransitByDay.aspx.cs" Inherits="TransitByDay" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0px" cellspacing="3px">
        <tr style="font-size:1px"><td width="96px">&nbsp;</td><td width="480px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td colspan="2">&nbsp;<uc1:DualDateTimePicker ID="ddpSetup" runat="server" Width="336px" LabelWidth="90px" DateDaysBack="365" DateDaysForward="7" DateDaysSpread="31" EnableViewState="true" OnDateTimeChanged="OnFromToDateChanged" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
         <tr>
            <td align="right">Client&nbsp;</td>
            <td>
                <asp:DropDownList id="cboClient" runat="server" Width="288px" AppendDataBoundItems="true" DataSourceID="odsClients" DataTextField="ClientName" DataValueField="ClientNumber" AutoPostBack="True" OnSelectedIndexChanged="OnClientChanged">
                    <asp:ListItem Text="All" Value="" Selected="True"></asp:ListItem>
                </asp:DropDownList>
                <asp:ObjectDataSource ID="odsClients" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetClientsList" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="600">
                    <SelectParameters>
                        <asp:ControlParameter Name="activeOnly" ControlID="chkActiveOnly" PropertyName="Checked" Type="Boolean" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                &nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="chkActiveOnly" runat="server" Text="Active Only" Checked="true" AutoPostBack="true" OnCheckedChanged="OnActiveOnlyChecked" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
   </table>
</asp:Panel>
</asp:Content>

