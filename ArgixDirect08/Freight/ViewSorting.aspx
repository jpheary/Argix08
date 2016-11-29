<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"  CodeFile="ViewSorting.aspx.cs" Inherits="ViewSorting" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="body">
    <asp:UpdatePanel ID="upnlAssignments" runat="server" ChildrenAsTriggers="true" RenderMode="Block" UpdateMode="Conditional" >
    <ContentTemplate>
    <asp:MultiView ID="mvPage" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwAssignments" runat="server">
            <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td align="left" style="font-weight:bold">Jamesburg Station Assignments</td>
                </tr>
            </table>
            <br />
            <table width="100%" border="0px" cellpadding="0px" cellspacing="0px" style="background-color:White">
                <tr>
                    <td colspan="4" valign="top" align="center">
                        <asp:Panel id="pnlAssignments" runat="server" Width="100%" Height="280px" BorderStyle="None" BorderWidth="1px" ScrollBars="Vertical">
                            <asp:ListView ID="lsvAssignments" runat="server" DataSourceID="odsAssignments">
                                <LayoutTemplate>
                                    <div id="itemPlaceholder" runat="server" style="width:100%" ></div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                        <table border="0" cellpadding="3" cellspacing="3" style="width:100%; background-color:White">
                                            <tr><td style="width:120px; text-align:left; font-weight:bold">Station#&nbsp;<%# Eval("SortStation.Number")%></td><td style="text-align:right;"><%# Eval("SortType")%></td>
                                                <td rowspan="3" style="width:24px">&nbsp;</td>
                                            </tr>
                                            <tr><td style="text-align:left;">TDS#&nbsp;<%# Eval("InboundFreight.TDSNumber")%></td><td style="text-align:right;">Trailer#&nbsp;<%# Eval("InboundFreight.TrailerNumber")%></td></tr>
                                            <tr><td colspan="2" style="text-align:left;"><%# GetClientInfo(Eval("InboundFreight.ClientNumber"))%></td></tr>
                                            <tr><td colspan="2" style="text-align:left;"><%# GetClientInfo(Eval("InboundFreight.ShipperNumber"))%></td></tr>
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
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwAssignment" runat="server">
            <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td style="width:25%"><asp:Button ID="btnAssignments" runat="server" Width="100%" Height="20px" Text="<< Back" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" OnCommand="OnChangeView" CommandName="Assignments" /></td>
                    <td style="width:75%">&nbsp;</td>
                </tr>
            </table>
            <br />
            <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
                <tr><td><asp:Label ID="lblTDSNumber" runat="server" Width="100%" Height="18px" Text=""></asp:Label></td></tr>
                <tr><td><asp:Label ID="lblStatus" runat="server" Width="100%" Height="18px" Text=""></asp:Label></td></tr>
                <tr>
                    <td valign="top" align="center" style="background-color:White; border:solid 1px black; border-top:none">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>