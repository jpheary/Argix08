<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="AuditTrailer.aspx.cs" Inherits="AuditTrailer" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0px" cellspacing="3px">
        <tr style="font-size:1px"><td width="96px">&nbsp;</td><td width="384px">&nbsp;</td><td>&nbsp;</td></tr>
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
            <td align="right">&nbsp;</td>
            <td><asp:CheckBox ID="chkOverShort" runat="server" Width="192px" Text="Over/Short Only" Checked="True" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
         <tr>
            <td align="right">Trips for&nbsp;</td>
            <td><asp:TextBox ID="txtTripDaysBack" runat="server" width="24px" MaxLength="2" AutoPostBack="True" OnTextChanged="OnTripRangeChanged">1</asp:TextBox>&nbsp;days back.</td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td colspan="3">
                <table width="100%" border="0px" cellpadding="0px" cellspacing="3px">
                    <tr style="height:16px"><td style="font-size:1.0em; vertical-align:middle; padding-left:6px; background-image:url(../App_Themes/Reports/Images/gridtitle.gif); background-repeat:repeat-x;">Indirect Trips</td></tr>
                    <tr>
                        <td valign="top">
                            <asp:UpdatePanel ID="upnlTrips" runat="server" ChildrenAsTriggers="true" RenderMode="Block" UpdateMode="Conditional" >
                            <ContentTemplate>
                                <asp:Panel ID="pnlTrips" runat="server" Width="100%" Height="288px" ScrollBars="Horizontal">
                                    <asp:GridView ID="grdTrips" runat="server" Width="100%" DataSourceID="odsTrips" DataKeyNames="BolNumber" AutoGenerateColumns="False" AllowSorting="True" OnSelectedIndexChanged="OnTripSelected">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" SelectImageUrl="~/App_Themes/Reports/Images/select.gif" HeaderStyle-Width="12px" ShowSelectButton="True" />
                                            <asp:BoundField DataField="BolNumber" HeaderText="BOL#" HeaderStyle-Width="96px" SortExpression="BolNumber" HtmlEncode="False" />
                                            <asp:BoundField DataField="CartonCount" HeaderText="Cartons" HeaderStyle-Width="72px" SortExpression="CartonCount" />
                                            <asp:BoundField DataField="Carrier" HeaderText="Carrier" HeaderStyle-Width="72px" SortExpression="Carrier" />
                                            <asp:BoundField DataField="TrailerNumber" HeaderText="Trailer#" HeaderStyle-Width="96px" SortExpression="TrailerNumber" />
                                            <asp:BoundField DataField="Started" HeaderText="Started" HeaderStyle-Width="144px" SortExpression="Started" DataFormatString="{0:MM-dd-yyyy hh:mm tt}" HtmlEncode="False" />
                                            <asp:BoundField DataField="Stopped" HeaderText="Stopped" HeaderStyle-Width="144px" SortExpression="Stopped" DataFormatString="{0:MM-dd-yyyy hh:mm tt}" HtmlEncode="False" />
                                            <asp:BoundField DataField="Exported" HeaderText="Exported" HeaderStyle-Width="144px" SortExpression="Exported" DataFormatString="{0:MM-dd-yyyy hh:mm tt}" HtmlEncode="False" />
                                            <asp:BoundField DataField="Imported" HeaderText="Imported" HeaderStyle-Width="144px" SortExpression="Imported" DataFormatString="{0:MM-dd-yyyy hh:mm tt}" HtmlEncode="False" />
                                            <asp:BoundField DataField="Scanned" HeaderText="Scanned" HeaderStyle-Width="144px" SortExpression="Scanned" DataFormatString="{0:MM-dd-yyyy hh:mm tt}" HtmlEncode="False" />
                                            <asp:BoundField DataField="OSDSend" HeaderText="OS&amp;D Sent" HeaderStyle-Width="144px" SortExpression="OSDSend" DataFormatString="{0:MM-dd-yyyy hh:mm tt}" HtmlEncode="False" />
                                            <asp:BoundField DataField="Received" HeaderText="Received" HeaderStyle-Width="144px" SortExpression="Received" DataFormatString="{0:MM-dd-yyyy hh:mm tt}" HtmlEncode="False" />
                                            <asp:BoundField DataField="CartonsExported" HeaderText="Ctns Exported" HeaderStyle-Width="72px" SortExpression="CartonsExported" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:ObjectDataSource ID="odsTrips" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetIndirectTrips" EnableCaching="false" >
                                        <SelectParameters>
                                            <asp:ControlParameter Name="terminal" ControlID="cboTerminal" PropertyName="SelectedValue" Type="string" />
                                            <asp:ControlParameter Name="daysBack" ControlID="txtTripDaysBack" PropertyName="Text" Type="string" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cboTerminal" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="txtTripDaysBack" EventName="TextChanged" />
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
