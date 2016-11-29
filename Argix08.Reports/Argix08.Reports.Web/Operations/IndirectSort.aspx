<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="IndirectSort.aspx.cs" Inherits="IndirectSort" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0px" cellspacing="3px">
        <tr style="font-size:1px"><td width="96px">&nbsp;</td><td width="384px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td align="right">Terminal&nbsp;</td>
            <td>
                <asp:DropDownList ID="cboTerminal" runat="server" Width="288px" DataSourceID="odsTerminals" DataTextField="Description" DataValueField="Number" OnSelectedIndexChanged="OnTerminalSelected" AutoPostBack="True"></asp:DropDownList>
                <asp:ObjectDataSource ID="odsTerminals" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetArgixTerminals" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="900" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">&nbsp;</td>
            <td><asp:CheckBox ID="chkOverShort" runat="server" Text="Over/Short Only" width="100%" Checked="True" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Trips for last&nbsp;</td>
            <td><asp:TextBox ID="txtTripDaysBack" runat="server" width="24px" MaxLength="2" Text="1" AutoPostBack="True" OnTextChanged="OnTripRangeChanged"></asp:TextBox>&nbsp;days.</td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td colspan="3">
                <table width="100%" border="0px" cellpadding="0px" cellspacing="01px">
                    <tr style="height:16px"><td style="font-size:1.0em; vertical-align:middle; padding-left:6px; background-image:url(../App_Themes/Reports/Images/gridtitle.gif); background-repeat:repeat-x;">Indirect Trips</td></tr>
                    <tr>
                        <td valign="top">
                            <asp:UpdatePanel ID="upnlTrips" runat="server" UpdateMode="Conditional" >
                            <ContentTemplate>
                                <asp:Panel ID="pnlTrips" runat="server" Width="100%" Height="288px" BorderStyle="Inset" BorderWidth="1px" ScrollBars="Horizontal">
                                    <asp:GridView ID="grdTrips" runat="server" Width="1524px" DataSourceID="odsTrips" DataKeyNames="BolNumber" AutoGenerateColumns="False" AllowSorting="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" HeaderStyle-Width="24px" >
                                                <HeaderTemplate><asp:CheckBox ID="chkAll" runat="server" Enabled="true" AutoPostBack="true" OnCheckedChanged="OnAllTripsSelected"/></HeaderTemplate>
                                                <ItemTemplate><asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="OnTripSelected"/></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="BolNumber" HeaderText="BOL#" HeaderStyle-Width="96px" SortExpression="BolNumber" HtmlEncode="False"  />
                                            <asp:BoundField DataField="CartonCount" HeaderText="Cartons" HeaderStyle-Width="72px" SortExpression="CartonCount"  />
                                            <asp:BoundField DataField="Carrier" HeaderText="Carrier" HeaderStyle-Width="72px" SortExpression="Carrier"  />
                                            <asp:BoundField DataField="TrailerNumber" HeaderText="Trailer#" HeaderStyle-Width="96px" SortExpression="TrailerNumber"  />
                                            <asp:BoundField DataField="Started" HeaderText="Started" HeaderStyle-Width="156px" ItemStyle-Wrap="False" SortExpression="Started" DataFormatString="{0:MM-dd-yyyy hh:mm tt}" HtmlEncode="False"  />
                                            <asp:BoundField DataField="Stopped" HeaderText="Stopped" HeaderStyle-Width="156px" ItemStyle-Wrap="False" SortExpression="Stopped" DataFormatString="{0:MM-dd-yyyy hh:mm tt}" HtmlEncode="False"  />
                                            <asp:BoundField DataField="Exported" HeaderText="Exported" HeaderStyle-Width="156px" ItemStyle-Wrap="False" SortExpression="Exported" DataFormatString="{0:MM-dd-yyyy hh:mm tt}" HtmlEncode="False"  />
                                            <asp:BoundField DataField="Imported" HeaderText="Imported" HeaderStyle-Width="156px" ItemStyle-Wrap="False" SortExpression="Imported" DataFormatString="{0:MM-dd-yyyy hh:mm tt}" HtmlEncode="False"  />
                                            <asp:BoundField DataField="Scanned" HeaderText="Scanned" HeaderStyle-Width="156px" ItemStyle-Wrap="False" SortExpression="Scanned" DataFormatString="{0:MM-dd-yyyy hh:mm tt}" HtmlEncode="False"  />
                                            <asp:BoundField DataField="OSDSend" HeaderText="OS&amp;D Sent" HeaderStyle-Width="156px" ItemStyle-Wrap="False" SortExpression="OSDSend" DataFormatString="{0:MM-dd-yyyy hh:mm tt}" HtmlEncode="False"  />
                                            <asp:BoundField DataField="Received" HeaderText="Received" HeaderStyle-Width="156px" ItemStyle-Wrap="False" SortExpression="Received" DataFormatString="{0:MM-dd-yyyy hh:mm tt}" HtmlEncode="False"  />
                                            <asp:BoundField DataField="CartonsExported" HeaderText="Cartons Exported" HeaderStyle-Width="84px" SortExpression="CartonsExported"  />
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
                            <asp:UpdateProgress ID="uprgTrips" runat="server" AssociatedUpdatePanelID="upnlTrips"><ProgressTemplate>updating...</ProgressTemplate></asp:UpdateProgress>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
   </table>
</asp:Panel>
</asp:Content>
