<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="BNConsumerDirectNDCPerformanceDetail.aspx.cs" Inherits="BNConsumerDirectNDCPerformanceDetail" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0" cellspacing="3px">
        <tr style="font-size:1px"><td width="72px">&nbsp;</td><td width="384px">&nbsp;</td><td>&nbsp;</td></tr>
         <tr>
            <td colspan="2"><uc1:DualDateTimePicker ID="ddpSetup" runat="server" Width="332px" LabelWidth="72px" DateDaysBack="365" DateDaysForward="0" DateDaysSpread="90" OnDateTimeChanged="OnFromToDateChanged" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
   </table>
</asp:Panel>
</asp:Content>

