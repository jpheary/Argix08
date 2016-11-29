<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="MultipleCartonSearch.aspx.cs" Inherits="MultipleCartonSearch" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0px" cellspacing="3px">
        <tr style="font-size:1px"><td width="96px">&nbsp;</td><td width="384px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td align="right">Terminal&nbsp;</td>
            <td>
                <asp:DropDownList ID="cboTerminal" runat="server" Width="288px" DataSourceID="odsTerminals" DataTextField="Description" DataValueField="Number" OnSelectedIndexChanged="OnTerminalSelected" AutoPostBack="True"></asp:DropDownList>
                <asp:ObjectDataSource ID="odsTerminals" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetArgixTerminals" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="900" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Numbers&nbsp;</td>
            <td><asp:TextBox ID="txtNumbers" runat="server" Width="288px" Height="192px" MaxLength="2048000" TextMode="MultiLine" AutoPostBack="True" OnTextChanged="OnNumbersChanged"></asp:TextBox></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
   </table>
</asp:Panel>
</asp:Content>

