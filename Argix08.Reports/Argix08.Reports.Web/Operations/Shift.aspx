<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Shift.aspx.cs" Inherits="Shift" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0px" cellspacing="3px">
        <tr style="font-size:1px"><td width="96px">&nbsp;</td><td width="384px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td colspan="2">&nbsp;<uc1:DualDateTimePicker ID="ddpPickups" runat="server" Width="336px" LabelWidth="96px" FromVisible="false" ToLabel="Date" DateDaysBack="90" DateDaysForward="0" DateDaysSpread="0" OnDateTimeChanged="OnFromToDateChanged" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Terminal&nbsp;</td>
            <td>
                <asp:DropDownList ID="cboTerminal" runat="server" Width="288px" DataSourceID="odsTerminals" DataTextField="Description" DataValueField="Number" AutoPostBack="True" OnSelectedIndexChanged="OnTerminalSelected"></asp:DropDownList>
                <asp:ObjectDataSource ID="odsTerminals" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetArgixTerminals" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="900" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Shift&nbsp;</td>
            <td>
                <asp:DropDownList id="cboShift" runat="server" Width="96px" DataSourceID="odsShifts" DataTextField="NUMBER" DataValueField="NUMBER" AutoPostBack="True" OnSelectedIndexChanged="OnShiftChanged"></asp:DropDownList>
                <asp:ObjectDataSource ID="odsShifts" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetShifts" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="900" >
                    <SelectParameters>
                        <asp:ControlParameter Name="terminal" ControlID="cboTerminal" PropertyName="SelectedValue" Type="String" />
                        <asp:ControlParameter Name="date" ControlID="ddpPickups" PropertyName="ToDate" Type="DateTime" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Freight Type&nbsp;</td>
            <td>
                <asp:DropDownList ID="cboType" runat="server" Width="134px">
                    <asp:ListItem Text="Tsort" Value="Tsort" Selected="True" />
                    <asp:ListItem Text="Returns" Value="Returns" />
                    <asp:ListItem Text="Both" Value="Both" />
                </asp:DropDownList>            
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
   </table>
</asp:Panel>
</asp:Content>

