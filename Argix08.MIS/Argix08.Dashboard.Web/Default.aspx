<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Argix Direct Dashboard</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="smMain" runat="server" ScriptMode="Debug" EnablePartialRendering="true" AllowCustomErrorsRedirect="true" ></asp:ScriptManager>
    <div>
        <table width="1050px" border="0" cellpadding="0px" cellspacing="0px">
            <tr style="height:1px; font-size:1px"><td style="width:500px">&nbsp;</td><td style="width:50px">&nbsp;</td><td style="width:500px">&nbsp;</td></tr>
            <tr>
                <td>
                    <table width="100%" border="0" cellpadding="0px" cellspacing="6px">
                        <tr style="height:1px; font-size:1px"><td style="width:200px">&nbsp;</td><td style="width:25px">&nbsp;</td><td style="width:200px">&nbsp;</td></tr>
                        <tr>
                            <td colspan="3"><div>&nbsp;Sorted Items:&nbsp;<asp:Label ID="lblSortCount" runat="server" Text=""></asp:Label></div></td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <rsweb:ReportViewer ID="rsSortedItems" runat="server" ShowToolBar="False" ProcessingMode="Remote" ShowParameterPrompts="False" SizeToReportContent="True">
                                    <ServerReport DisplayName="Sorted Items" ReportServerUrl="http://tpjheary7/reportserver" ReportPath="/argix/sorteditems" />
                                </rsweb:ReportViewer>
                            </td>
                        </tr>
                        <tr><td colspan="3">&nbsp;</td></tr>
                        <tr>
                            <td>
                                <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
                                    <tr style="height:16px"><td style="vertical-align:middle;"><div style="background-color:Highlight; border-style:solid; border-width:thin; border-color:Navy; margin-bottom:6px; padding-left:6px">Terminals</div></td></tr>
                                    <tr>
                                        <td valign="top">
                                            <div style="background-color:Highlight; border-style:solid; border-width:thin; border-color:Navy; padding:3px 3px 3px 3px">
                                            <asp:Panel id="pnlItemsT" runat="server" Width="100%" Height="192px" BorderStyle="None" BorderWidth="1px" ScrollBars="Auto">
                                                <asp:UpdatePanel ID="upnlItemsT" runat="server" ChildrenAsTriggers="true" RenderMode="Block" UpdateMode="Conditional" >
                                                <ContentTemplate>
                                                    <asp:GridView ID="grdItemsT" runat="server" Width="100%" DataSourceID="odsItemsT" AutoGenerateColumns="false" AllowSorting="True">
                                                        <Columns>
                                                            <asp:BoundField DataField="TerminalNumber" HeaderText="#" HeaderStyle-Width="24px" NullDisplayText="" />
                                                            <asp:BoundField DataField="TerminalName" HeaderText="Name" HeaderStyle-Width="96px" NullDisplayText="" />
                                                            <asp:BoundField DataField="SortSize" HeaderText="Items" HeaderStyle-Width="72px" NullDisplayText="" SortExpression="SortSize DESC" />
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:ObjectDataSource ID="odsItemsT" runat="server" SelectMethod="GetSortedItemsByTerminal" TypeName="Argix.ArgixService" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="900">
                                                        <SelectParameters>
                                                            <asp:Parameter Name="startSortDate" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                                            <asp:Parameter Name="endSortDate" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                                            <asp:Parameter Name="terminalNumber" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
                                    <tr style="height:16px"><td style="vertical-align:middle;">&nbsp;Sorted Items for 09-26-2011</td></tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:Panel id="pnlItemsC" runat="server" Width="100%" Height="192px" BorderStyle="None" BorderWidth="1px" ScrollBars="Auto">
                                                <asp:UpdatePanel ID="upnlItemsC" runat="server" ChildrenAsTriggers="true" RenderMode="Block" UpdateMode="Conditional" >
                                                <ContentTemplate>
                                                    <asp:GridView ID="grdItemsC" runat="server" Width="100%" DataSourceID="odsItemsC" AutoGenerateColumns="false" AllowSorting="True">
                                                        <Columns>
                                                            <asp:BoundField DataField="ClientNumber" HeaderText="#" HeaderStyle-Width="24px" NullDisplayText="" />
                                                            <asp:BoundField DataField="ClientName" HeaderText="Name" HeaderStyle-Width="96px" NullDisplayText="" />
                                                            <asp:BoundField DataField="SortSize" HeaderText="Items" HeaderStyle-Width="72px" NullDisplayText="" SortExpression="SortSize DESC" />
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:ObjectDataSource ID="odsItemsC" runat="server" SelectMethod="GetSortedItemsByClient" TypeName="Argix.ArgixService" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="900">
                                                        <SelectParameters>
                                                            <asp:Parameter Name="startSortDate" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                                            <asp:Parameter Name="endSortDate" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                                            <asp:Parameter Name="clientNumber" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr><td colspan="3">&nbsp;</td></tr>
                    </table>
                </td>
                <td>&nbsp;</td>
                <td>
                    <table width="100%" border="0" cellpadding="0px" cellspacing="6px">
                        <tr style="height:1px; font-size:1px"><td style="width:200px">&nbsp;</td><td style="width:25px">&nbsp;</td><td style="width:200px">&nbsp;</td></tr>
                        <tr>
                            <td colspan="3"><div>&nbsp;Deliveries:&nbsp;<asp:Label ID="lblDeliveryCount" runat="server" Text="816,000"></asp:Label></div></td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <rsweb:ReportViewer ID="rsDeliveries" runat="server" ShowToolBar="False" ProcessingMode="Remote" ShowParameterPrompts="False" SizeToReportContent="True">
                                    <ServerReport DisplayName="Sorted Items" ReportServerUrl="http://tpjheary7/reportserver" ReportPath="/argix/deliveries" />
                                </rsweb:ReportViewer>
                            </td>
                        </tr>
                        <tr><td colspan="3">&nbsp;</td></tr>
                        <tr>
                            <td>
                                <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
                                    <tr style="height:16px"><td style="vertical-align:middle;">&nbsp;Delivery Orders for 09-26-2011</td></tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:Panel id="pnlOrdersT" runat="server" Width="100%" Height="192px" BorderStyle="None" BorderWidth="1px" ScrollBars="Auto">
                                                <asp:UpdatePanel ID="upnlOrdersT" runat="server" ChildrenAsTriggers="true" RenderMode="Block" UpdateMode="Conditional" >
                                                <ContentTemplate>
                                                    <asp:GridView ID="grdOrdersT" runat="server" Width="100%" DataSourceID="odsOrdersT" AutoGenerateColumns="false" AllowSorting="True">
                                                        <Columns>
                                                            <asp:BoundField DataField="TerminalNumber" HeaderText="#" HeaderStyle-Width="24px" NullDisplayText="" />
                                                            <asp:BoundField DataField="TerminalName" HeaderText="Name" HeaderStyle-Width="96px" NullDisplayText="" />
                                                            <asp:BoundField DataField="OrderSize" HeaderText="Items" HeaderStyle-Width="72px" NullDisplayText="" SortExpression="OrderSize DESC" />
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:ObjectDataSource ID="odsOrdersT" runat="server" SelectMethod="GetDeliveryOrdersByTerminal" TypeName="Argix.ArgixService" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="900">
                                                        <SelectParameters>
                                                            <asp:Parameter Name="startRoutingDate" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                                            <asp:Parameter Name="endRoutingDate" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                                            <asp:Parameter Name="terminalNumber" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>&nbsp;</td>
                            <td>
                                <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
                                    <tr style="height:16px"><td style="vertical-align:middle;">&nbsp;Delivery Orders for 09-26-2011</td></tr>
                                    <tr>
                                        <td valign="top">
                                            <asp:Panel id="pnlOrdersC" runat="server" Width="100%" Height="192px" BorderStyle="None" BorderWidth="1px" ScrollBars="Auto">
                                                <asp:UpdatePanel ID="upnlOrdersC" runat="server" ChildrenAsTriggers="true" RenderMode="Block" UpdateMode="Conditional" >
                                                <ContentTemplate>
                                                    <asp:GridView ID="grdOrdersC" runat="server" Width="100%" DataSourceID="odsOrdersC" AutoGenerateColumns="false" AllowSorting="True">
                                                        <Columns>
                                                            <asp:BoundField DataField="ClientNumber" HeaderText="#" HeaderStyle-Width="24px" NullDisplayText="" />
                                                            <asp:BoundField DataField="ClientName" HeaderText="Name" HeaderStyle-Width="96px" NullDisplayText="" />
                                                            <asp:BoundField DataField="OrderSize" HeaderText="Items" HeaderStyle-Width="72px" NullDisplayText="" SortExpression="OrderSize DESC" />
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:ObjectDataSource ID="odsOrdersC" runat="server" SelectMethod="GetDeliveryOrdersByClient" TypeName="Argix.ArgixService" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="900">
                                                        <SelectParameters>
                                                            <asp:Parameter Name="startRoutingDate" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                                            <asp:Parameter Name="endRoutingDate" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                                            <asp:Parameter Name="clientNumber" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr><td colspan="3">&nbsp;</td></tr>
                    </table>
                </td>
            </tr>
            <tr><td colspan="3">&nbsp;</td></tr>
        </table>
    </div>
    </form>
</body>
</html>
