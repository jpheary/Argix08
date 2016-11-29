<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Discrepancy.aspx.cs" Inherits="Discrepancy" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ Register Src="../ClientShipperGrids.ascx" TagName="ClientShipperGrids" TagPrefix="uc3" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0" cellspacing="3px">
        <tr style="font-size:1px"><td width="96px">&nbsp;</td><td width="384px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td colspan="2">&nbsp;<uc1:DualDateTimePicker ID="ddpPickups" Width="336px" LabelWidth="108px" runat="server" DateDaysBack="90" DateDaysForward="0" DateDaysSpread="7" EnableViewState="true" OnDateTimeChanged="OnFromToDateChanged" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">&nbsp;</td>
            <td>
                <asp:DropDownList ID="cboFreightType" runat="server" Width="72px" AutoPostBack="True" OnSelectedIndexChanged="OnFreightTypeChanged">
                    <asp:ListItem Text="Tsort" Value="0" Selected="True" />
                    <asp:ListItem Text="Returns" Value="1" />
                </asp:DropDownList>
                &nbsp;freight viewed by&nbsp;
                <asp:DropDownList ID="cboViewBy" runat="server" Width="72px">
                    <asp:ListItem Selected="True">Store</asp:ListItem>
                    <asp:ListItem>Zone</asp:ListItem>
                    <asp:ListItem>State</asp:ListItem>
                </asp:DropDownList>            
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr><td colspan="3">&nbsp;<uc3:ClientShipperGrids ID="dgdClientShipper" runat="server" Height="300px" ClientsEnabled="true" ShippersEnabled="false" FreightType="Regular" FreightTypeEnabled="false" OnAfterClientSelected="OnClientSelected" OnAfterShipperSelected="OnShipperSelected" OnControlError="OnControlError" /></td></tr>
   </table>
</asp:Panel>
</asp:Content>

