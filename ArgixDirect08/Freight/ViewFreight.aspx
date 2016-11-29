<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"  CodeFile="ViewFreight.aspx.cs" Inherits="ViewFreight" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="body">
    <asp:UpdatePanel ID="upnlShipments" runat="server" ChildrenAsTriggers="true" RenderMode="Block" UpdateMode="Conditional" >
    <ContentTemplate>
    <asp:MultiView ID="mvPage" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwShipments" runat="server">
            <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td align="left" style="font-weight:bold">Jamesburg Inbound Freight</td>
                </tr>
            </table>
            <br />
            <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td style="width:25%"><asp:Button ID="btnRegular" runat="server" Width="100%" Height="20px" Text="Regular" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" OnCommand="OnChangeView" CommandName="Regular" /></td>
                    <td style="width:25%"><asp:Button ID="btnReturns" runat="server" Width="100%" Height="20px" Text="Returns" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" OnCommand="OnChangeView" CommandName="Returns" /></td>
                    <td style="width:50%">&nbsp;</td>
                </tr>
            </table>
            <table width="100%" border="0px" cellpadding="0px" cellspacing="0px" style="background-color:White">
                <tr>
                    <td colspan="4" valign="top" align="center" style="background-color:White; border:solid 1px black; border-top:none">
                        <asp:Panel id="pnlShipments" runat="server" Width="100%" Height="280px" BorderStyle="None" BorderWidth="1px" ScrollBars="Vertical">
                            <asp:ListView ID="lsvShipments" runat="server" DataSourceID="odsShipments">
                                <LayoutTemplate>
                                    <div id="itemPlaceholder" runat="server" style="width:100%" ></div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                        <table border="0" cellpadding="3" cellspacing="3" style="width:100%; background-color:White">
                                            <tr><td style="width:120px; text-align:left; font-weight:bold">TDS#&nbsp;<%# Eval("TDSNumber")%></td><td style="text-align:right;"><%# Eval("Status")%></td>
                                                <td rowspan="3" style="width:24px"><asp:ImageButton runat="server" ImageUrl="App_Themes/Argix/Images/select.gif" CommandName="Shipment" CommandArgument='<%# Eval("FreightID") %>' OnCommand="OnChangeView" /></td>
                                            </tr>
                                            <tr><td style="text-align:left;">Trailer#&nbsp;<%# Eval("TrailerNumber")%></td><td style="text-align:right;"><%# GetItemCount(Eval("Cartons"),Eval("Pallets"))%></td></tr>
                                            <tr><td colspan="2" style="text-align:left;"><%# GetClientInfo(Eval("ClientNumber"),Eval("ClientName"))%></td></tr>
                                            <tr><td colspan="3"><hr /></td></tr>
                                        </table>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <table border="0" cellpadding="3" cellspacing="0" style="width:100%; background-color:White">
                                        <tr style="background-color:white; height:48px"><td>&nbsp;</td></tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:ListView>
                            <asp:ObjectDataSource ID="odsShipments" runat="server" TypeName="Argix.Freight.FreightServiceProxy" SelectMethod="GetInboundFreight">
                                <SelectParameters>
                                    <asp:Parameter Name="terminalID" DefaultValue="5" ConvertEmptyStringToNull="true" />
                                    <asp:Parameter Name="freightType" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwShipment" runat="server">
            <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td style="width:25%"><asp:Button ID="btnShipments" runat="server" Width="100%" Height="20px" Text="<< Back" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" OnCommand="OnChangeView" CommandName="Shipments" /></td>
                    <td style="width:75%">&nbsp;</td>
                </tr>
            </table>
            <br />
            <table border="0px" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td style="width:25%"><asp:Button ID="btnFreight" runat="server" Width="100%" Height="20px" Text="Freight" BorderStyle="Solid" BorderWidth="1px" style="border-bottom-style:none" OnCommand="OnChangeView" CommandName="Freight" /></td>
                    <td style="width:25%"><asp:Button ID="btnAssignments" runat="server" Width="100%" Height="20px" Text="Assignments" BorderStyle="Solid" BorderWidth="1px" style="border-bottom-style:solid" OnCommand="OnChangeView" CommandName="Assignments" /></td>
                    <td style="width:50%">&nbsp;</td>
                </tr>
                <tr style="font-size:1px; height:12px"><td colspan="3" style="background-color:White; border-left: solid 1px black; border-right:solid 1px black">&nbsp;</td></tr>
                <tr>
                    <td colspan="3" valign="top" align="center" style="background-color:White; border:solid 1px black; border-top:none">
                        <asp:Panel id="pnlShipment" runat="server" Width="100%" Height="260px" BorderStyle="None" BorderWidth="1px" ScrollBars="Vertical">
                            <asp:MultiView ID="mvShipment" runat="server" ActiveViewIndex="0">
                                <asp:View ID="vwDetail" runat="server">
                                    <asp:DetailsView ID="dvShipment" runat="server" Width="100%" DataSourceID="odsShipment" AutoGenerateRows="false" BorderStyle="None">
                                        <Fields>
                                            <asp:BoundField DataField="TerminalID" HeaderText="TerminalID" HeaderStyle-Width="96px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                            <asp:BoundField DataField="FreightID" HeaderText="FreightID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="true" />
                                            <asp:BoundField DataField="FreightType" HeaderText="Freight Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                            <asp:BoundField DataField="CurrentLocation" HeaderText="Location" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="FloorStatus" HeaderText="Floor Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                            <asp:BoundField DataField="UnloadedStatus" HeaderText="Unloaded Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                            <asp:BoundField DataField="Pickup" HeaderText="Pickup" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" />      
                                            <asp:BoundField DataField="ReceiveDate" HeaderText="Receive Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" />      
                                            <asp:BoundField DataField="Pallets" HeaderText="Pallets" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                            <asp:BoundField DataField="Cartons" HeaderText="Cartons" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                            <asp:BoundField DataField="IsSortable" HeaderText="Sortable" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                        </Fields>
                                    </asp:DetailsView>
                                    <br />
                                    <asp:DetailsView ID="dvShipment1" runat="server" Width="100%" DataSourceID="odsShipment" AutoGenerateRows="false" BorderStyle="None">
                                        <Fields>
                                            <asp:BoundField DataField="ClientNumber" HeaderText="Client#" HeaderStyle-Width="96px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                            <asp:BoundField DataField="ClientName" HeaderText="Client Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                            <asp:BoundField DataField="ShipperNumber" HeaderText="Shipper#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                            <asp:BoundField DataField="ShipperName" HeaderText="Shipper Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                            <asp:BoundField DataField="VendorKey" HeaderText="Vendor Key" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                        </Fields>
                                    </asp:DetailsView>
                                    <br />
                                    <asp:DetailsView ID="dvShipment2" runat="server" Width="100%" DataSourceID="odsShipment" AutoGenerateRows="false" BorderStyle="None">
                                        <Fields>
                                            <asp:BoundField DataField="CarrierNumber" HeaderText="Carrier#" HeaderStyle-Width="96px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                            <asp:BoundField DataField="DriverNumber" HeaderText="Driver#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                            <asp:BoundField DataField="TrailerNumber" HeaderText="Trailer#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                            <asp:BoundField DataField="SealNumber" HeaderText="Seal#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                            <asp:BoundField DataField="StorageTrailerNumber" HeaderText="Storage Trailer#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                            <asp:BoundField DataField="TDSNumber" HeaderText="TDS#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                        </Fields>
                                    </asp:DetailsView>
                                    <asp:ObjectDataSource ID="odsShipment" runat="server" SelectMethod="GetInboundShipment" TypeName="Argix.Freight.FreightServiceProxy">
                                        <SelectParameters>
                                            <asp:Parameter Name="freightID" DefaultValue="" ConvertEmptyStringToNull="true" Type="string" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </asp:View>
                                <asp:View ID="vwAssignments" runat="server">
                                    <asp:ListView ID="lsvAssignments" runat="server" DataSourceID="odsAssignments">
                                        <LayoutTemplate>
                                            <div id="itemPlaceholder" runat="server" style="width:100%" ></div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                                <table border="0" cellpadding="3" cellspacing="3" style="width:100%; background-color:White">
                                                    <tr><td style="width:120px; text-align:left; font-weight:bold">Station#&nbsp;<%# Eval("SortStation.Number")%></td><td style="text-align:right;"><%# Eval("SortType")%></td>
                                                        <td rowspan="2" style="width:24px"></td>
                                                    </tr>
                                                    <tr><td colspan="2" style="text-align:left;"></td></tr>
                                                    <tr><td colspan="3"><hr /></td></tr>
                                                </table>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <table border="0" cellpadding="3" cellspacing="0" style="width:100%; background-color:White">
                                                <tr style="background-color:white; height:48px"><td>&nbsp;</td></tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                    <asp:ObjectDataSource ID="odsAssignments" runat="server" TypeName="Argix.Freight.FreightServiceProxy" SelectMethod="GetStationAssignments">
                                        <SelectParameters>
                                            <asp:Parameter Name="freightID" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </asp:View>
                            </asp:MultiView>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>