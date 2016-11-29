<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="VirtualvsOptimizedTrailerLoad.aspx.cs" Inherits="VirtualvsOptimizedTrailerLoad" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0px" cellspacing="3px">
        <tr style="font-size:1px"><td width="120px">&nbsp;</td><td width="384px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td colspan="2">&nbsp;<uc1:DualDateTimePicker ID="ddpPickups" runat="server" Width="336px" LabelWidth="120px" DateDaysBack="365" DateDaysForward="0" DateDaysSpread="90" OnDateTimeChanged="OnFromToDateChanged" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Optimum Weight&nbsp;</td>
            <td>
                <asp:TextBox ID="txtWeight" runat="server" Width="96px" ToolTip="Enter an optimized weight and press 'Enter'" OnTextChanged="OnWeightChanged">36000</asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="rfvWeight" runat="server" ControlToValidate="txtWeight" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="rvWeight" runat="server" ControlToValidate="txtWeight" ErrorMessage="Please enter a valid weight (1 - 999999)" MaximumValue="999999" MinimumValue="1" Type="Integer" SetFocusOnError="True"></asp:RangeValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Optimum Cube&nbsp;</td>
            <td>
                <asp:TextBox ID="txtCube" runat="server" Width="96px" ToolTip="Enter an optimized cube and press 'Enter'" OnTextChanged="OnWeightChanged">5293555</asp:TextBox>&nbsp;
                <asp:RequiredFieldValidator ID="rfvCube" runat="server" ControlToValidate="txtCube" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="rvCube" runat="server" ControlToValidate="txtCube" ErrorMessage="Please enter a valid cube (1 - 9999999)" MaximumValue="9999999" MinimumValue="1" Type="Integer" SetFocusOnError="True"></asp:RangeValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
         <tr>
            <td align="right">Main Zone&nbsp;</td>
            <td>
                <asp:DropDownList ID="cboAgent" runat="server" Width="96px" AppendDataBoundItems="true" DataSourceID="odsAgents" DataTextField="MainZone" DataValueField="MainZone" OnSelectedIndexChanged="OnAgentChanged">
                    <asp:ListItem Text="All" Value="" Selected="True" />
                </asp:DropDownList>
                <asp:ObjectDataSource ID="odsAgents" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetAgents" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="600">
                    <SelectParameters>
                        <asp:Parameter Name="mainZoneOnly" DefaultValue="true" Type="Boolean" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
   </table>
</asp:Panel>
</asp:Content>

