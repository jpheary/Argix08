<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="ASNTracking.aspx.cs" Inherits="ASNTracking" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ Register Src="../ClientVendorGrids.ascx" TagName="ClientVendorGrids" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="1px" cellspacing="3px">
        <tr style="font-size:1px"><td width="96px">&nbsp;</td><td width="384px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td colspan="2">&nbsp;<uc1:DualDateTimePicker ID="ddpSetup" runat="server" Width="332px" LabelWidth="96px" DateDaysBack="180" DateDaysForward="0" DateDaysSpread="14" OnDateTimeChanged="OnFromToDateChanged" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr><td colspan="3" align="right"><asp:CheckBox ID="chkAllVendors" runat="server" width="288px" Text="Pickups for all vendors" TextAlign="Left" Checked="true" AutoPostBack="True" OnCheckedChanged="OnAllVendorsChecked" /></td></tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr><td colspan="3"><uc2:ClientVendorGrids ID="dgdClientVendor" runat="server" Height="126px" ClientsEnabled="true" VendorsEnabled="false" OnAfterClientSelected="OnClientSelected" OnAfterVendorSelected="OnVendorSelected" /></td></tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td colspan="3">
                <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
                    <tr style="height:16px"><td style="font-size:1.0em; vertical-align:middle; padding-left:6px; background-image:url(../App_Themes/Reports/Images/gridtitle.gif); background-repeat:repeat-x;"><asp:Image ID="imgClients" runat="server" ImageUrl="~/App_Themes/Reports/Images/pickups.gif" ImageAlign="Middle" />&nbsp;Pickups</td></tr>
                    <tr>
                        <td valign="top">
                            <asp:Panel id="pnlPickups" runat="server" Width="100%" Height="144px" BorderStyle="Inset" BorderWidth="1px" ScrollBars="Auto">
                                <asp:UpdatePanel ID="upnlPickups" runat="server" ChildrenAsTriggers="true" RenderMode="Block" UpdateMode="Conditional" >
                                <ContentTemplate>
                                    <asp:GridView ID="grdPickups" runat="server" Width="100%" DataSourceID="odsPickups" DataKeyNames="PickupID,TerminalCode" AutoGenerateColumns="False" AllowSorting="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" HeaderStyle-Width="24px" >
                                                <HeaderTemplate><asp:CheckBox ID="chkAll" runat="server" Enabled="true" AutoPostBack="true" OnCheckedChanged="OnAllPickupsSelected"/></HeaderTemplate>
                                                <ItemTemplate><asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="OnPickupSelected"/></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PickupID" HeaderText="ID" HeaderStyle-Width="24px" Visible="false" />
                                            <asp:BoundField DataField="TerminalCode" HeaderText="Term" HeaderStyle-Width="24px" Visible="false" />
                                            <asp:BoundField DataField="VendorNumber" HeaderText="Vendor#" HeaderStyle-Width="60px" SortExpression="VendorNumber" />
                                            <asp:BoundField DataField="VendorName" HeaderText="Vendor" ItemStyle-Wrap="false" SortExpression="VendorName" />
                                            <asp:BoundField DataField="PUDate" HeaderText="Pickup" HeaderStyle-Width="120px" SortExpression="PUDate" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" />
                                            <asp:BoundField DataField="PUNumber" HeaderText="#" HeaderStyle-Width="48px" SortExpression="PUNumber" />
                                            <asp:BoundField DataField="ManifestNumbers" HeaderText="Manifests" HeaderStyle-Width="144px" ItemStyle-Width="144px" ItemStyle-Wrap="true" NullDisplayText=" " />
                                            <asp:BoundField DataField="TrailerNumbers" HeaderText="Trailers" HeaderStyle-Width="144px" NullDisplayText=" " />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:ObjectDataSource ID="odsPickups" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetPickups" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="60" >
                                        <SelectParameters>
                                            <asp:ControlParameter Name="client" ControlID="dgdClientVendor" PropertyName="ClientNumber" Type="string" />
                                            <asp:ControlParameter Name="division" ControlID="dgdClientVendor" PropertyName="ClientDivsionNumber" Type="string" />
                                            <asp:ControlParameter Name="startDate" ControlID="ddpSetup" PropertyName="FromDate" Type="DateTime" />
                                            <asp:ControlParameter Name="endDate" ControlID="ddpSetup" PropertyName="ToDate" Type="DateTime" />
                                            <asp:ControlParameter Name="vendor" ControlID="dgdClientVendor" PropertyName="VendorNumber" Type="string" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddpSetup" EventName="DateTimeChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="chkAllVendors" EventName="CheckedChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="dgdClientVendor" EventName="AfterClientSelected" />
                                    <asp:AsyncPostBackTrigger ControlID="dgdClientVendor" EventName="AfterVendorSelected" />
                                </Triggers>
                                </asp:UpdatePanel>
                                <asp:UpdateProgress ID="uprgPickups" runat="server" AssociatedUpdatePanelID="upnlPickups"><ProgressTemplate>updating...</ProgressTemplate></asp:UpdateProgress>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
</asp:Content>

