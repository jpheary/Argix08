<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="StatSample.aspx.cs" Inherits="StatSample" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0px" cellspacing="3px">
        <tr style="font-size:1px"><td width="96px">&nbsp;</td><td width="384px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td colspan="2">&nbsp;<uc1:DualDateTimePicker ID="ddpPickups" runat="server" Width="336px" LabelWidth="96px" DateDaysBack="90" DateDaysForward="0" DateDaysSpread="7" OnDateTimeChanged="OnFromToDateChanged" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
   </table>
</asp:Panel>
</asp:Content>

