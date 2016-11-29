<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="DeliveryInfoByPickup.aspx.cs" Inherits="DeliveryInfoByPickup" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ Register Src="../ClientVendorGrids.ascx" TagName="ClientVendorGrids" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0" cellspacing="3px">
        <tr style="font-size:1px"><td width="72px">&nbsp;</td><td width="384px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td colspan="2">&nbsp;<uc1:DualDateTimePicker ID="ddpPickups" runat="server" Width="336px" LabelWidth="72px" DateDaysBack="90" DateDaysForward="0" DateDaysSpread="7" OnDateTimeChanged="OnFromToDateChanged" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr><td>&nbsp;</td><td><asp:CheckBox ID="chkAllVendors" runat="server" Width="288px" Text="Show pickups for all vendors" TextAlign="Right" Checked="true" AutoPostBack="True" OnCheckedChanged="OnAllVendorsChecked" /></td><td>&nbsp;</td></tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr><td colspan="3"><uc2:ClientVendorGrids ID="dgdClientVendor" runat="server" Height="126px" ClientsEnabled="true" VendorsEnabled="false" OnAfterClientSelected="OnClientSelected" OnAfterVendorSelected="OnVendorSelected" /></td></tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td colspan="3">
                <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
                    <tr style="height:16px"><td style="font-size:1.0em; vertical-align:middle; padding-left:6px; background-image:url(../App_Themes/Reports/Images/gridtitle.gif); background-repeat:repeat-x;"><asp:Image ID="imgClients" runat="server" ImageUrl="~/App_Themes/Reports/Images/pickups.gif" ImageAlign="Middle" />&nbsp;Pickups</td></tr>
                    <tr>
                        <td valign="top">
                            <asp:UpdatePanel ID="upnlPickups" runat="server" ChildrenAsTriggers="true" RenderMode="Block" UpdateMode="Conditional" >
                            <ContentTemplate>
                                <asp:Panel id="pnlPickups" runat="server" Width="100%" Height="144px" BorderStyle="Inset" BorderWidth="1px" ScrollBars="Auto">
                                    <asp:GridView ID="grdPickups" runat="server" Width="100%" AutoGenerateColumns="False" DataSourceID="odsPickups" DataKeyNames="PickupID,TerminalCode" AllowSorting="True">
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
                                    <asp:ObjectDataSource ID="odsPickups" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetPickups" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="300" >
                                        <SelectParameters>
                                            <asp:ControlParameter Name="client" ControlID="dgdClientVendor" PropertyName="ClientNumber" Type="string" />
                                            <asp:ControlParameter Name="division" ControlID="dgdClientVendor" PropertyName="ClientDivsionNumber" Type="string" />
                                            <asp:ControlParameter Name="startDate" ControlID="ddpPickups" PropertyName="FromDate" Type="DateTime" />
                                            <asp:ControlParameter Name="endDate" ControlID="ddpPickups" PropertyName="ToDate" Type="DateTime" />
                                            <asp:ControlParameter Name="vendor" ControlID="dgdClientVendor" PropertyName="VendorNumber" Type="string" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddpPickups" EventName="DateTimeChanged" />
                                <asp:AsyncPostBackTrigger ControlID="chkAllVendors" EventName="CheckedChanged" />
                                <asp:AsyncPostBackTrigger ControlID="dgdClientVendor" EventName="AfterClientSelected" />
                                <asp:AsyncPostBackTrigger ControlID="dgdClientVendor" EventName="AfterVendorSelected" />
                            </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
</asp:Content>

