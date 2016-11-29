<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="DeliveryPaperwork.aspx.cs" Inherits="DeliveryPaperwork" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0px" cellspacing="3px">
        <tr style="font-size:1px"><td width="96px">&nbsp;</td><td width="384px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td align="right">Terminal&nbsp;</td>
            <td>&nbsp;
                <asp:DropDownList ID="cboTerminal" runat="server" Width="192px" DataSourceID="odsTerminals" DataTextField="Description" DataValueField="Number" OnSelectedIndexChanged="OnTerminalSelected" AutoPostBack="True"></asp:DropDownList>
                <asp:ObjectDataSource ID="odsTerminals" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetArgixTerminals" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="900" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Report Type&nbsp;</td>
            <td>&nbsp;
                <asp:DropDownList ID="cboType" runat="server" Width="192px">
                    <asp:ListItem Text="Manifest" Value="Manifest" Selected="True" />
                    <asp:ListItem Text="Delivery Bill" Value="Delivery Bill" />
                    <asp:ListItem Text="Delivery Bill w/PO Number" Value="Delivery Bill w/PO Number" />
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Browse By Closed Date&nbsp;</td>
            <td colspan="1">&nbsp;<uc1:DualDateTimePicker ID="ddpTLDate" runat="server" Width="288px" LabelWidth="0px" FromLabel="" FromVisible="true" ToLabel="" ToVisible="true" DateDaysBack="31" DateDaysForward="0" DateDaysSpread="5" OnDateTimeChanged="OnFromToDateChanged" /></td>
            <td>&nbsp;</td>
       </tr>
        <tr style="font-size:1px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Search by TL#&nbsp;</td>
            <td>&nbsp;
                <asp:TextBox ID="txtSearch" runat="server" Width="120px" ToolTip="Search for a TL... <press Enter>" AutoPostBack="True" OnTextChanged="OnSearch"></asp:TextBox>
                <asp:ImageButton ID="imgFind" runat="server" Width="20px" ImageAlign="Middle" ImageUrl="~/App_Themes/Reports/Images/findreplace.gif" ToolTip="Search for a TL..." OnClick="OnFind" />&nbsp;
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px"><td colspan="3">&nbsp;</td></tr>
        <tr style="height:16px">
            <td colspan="3" style="font-size:1.0em; vertical-align:middle; padding-left:6px; background-image:url(../App_Themes/Reports/Images/gridtitle.gif); background-repeat:repeat-x;">Closed TL's</td>
        </tr>
        <tr>
            <td colspan="3" valign="top">
                <asp:UpdatePanel ID="upnlTLs" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                    <asp:Panel ID="pnlTLs" runat="server" Width="100%" Height="256px" BorderStyle="Inset" BorderWidth="1px" ScrollBars="Horizontal">
                        <asp:GridView ID="grdTLs" runat="server" Width="100%" DataSourceID="odsTLs" AutoGenerateColumns="False" AllowPaging="false" PageSize="12" AllowSorting="True" OnSelectedIndexChanged="OnTLSelected">
                            <Columns>
                                <asp:CommandField ButtonType="Image" SelectImageUrl="~/App_Themes/Reports/Images/select.gif" HeaderStyle-Width="12px" ShowSelectButton="True" />
                                <asp:BoundField DataField="NUMBER" HeaderText="Number" HeaderStyle-Width="120px" SortExpression="NUMBER" />
                                <asp:BoundField DataField="ZONE_CODE" HeaderText="Zone" HeaderStyle-Width="72px" SortExpression="ZONE_CODE" />
                                <asp:BoundField DataField="ZONE_TYPE" HeaderText="Type" HeaderStyle-Width="96px" SortExpression="ZONE_TYPE" />
                                <asp:BoundField DataField="AGENT_NUMBER" HeaderText="Agent#" HeaderStyle-Width="96px" SortExpression="AGENT_NUMBER" />
                                <asp:BoundField DataField="OPEN_DATE" HeaderText="Open Date" HeaderStyle-Width="96px" ItemStyle-Wrap="false" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" />
                                <asp:BoundField DataField="OPEN_TIME" HeaderText="Open Time" HeaderStyle-Width="72px" ItemStyle-Wrap="false" DataFormatString="{0:hh:mm tt}" HtmlEncode="False" />
                                <asp:BoundField DataField="CLOSE_DATE" HeaderText="Close Date" HeaderStyle-Width="96px" ItemStyle-Wrap="false" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" />
                                <asp:BoundField DataField="CLOSE_TIME" HeaderText="Close Time" HeaderStyle-Width="72px" ItemStyle-Wrap="false" DataFormatString="{0:hh:mm tt}" HtmlEncode="False" />
                                <asp:BoundField DataField="PRINTED_DATE" HeaderText="Print Date" HeaderStyle-Width="96px" ItemStyle-Wrap="false" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" Visible="true" />
                                <asp:BoundField DataField="PRINTED_TIME" HeaderText="Print Time" HeaderStyle-Width="72px" ItemStyle-Wrap="false" DataFormatString="{0:hh:mm tt}" HtmlEncode="False" Visible="true" />
                                <asp:BoundField DataField="CTM_DATE" HeaderText="CTM Date" HeaderStyle-Width="96px" ItemStyle-Wrap="false" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" Visible="true" />
                                <asp:BoundField DataField="CTM_TIME" HeaderText="CTM Time" HeaderStyle-Width="72px" ItemStyle-Wrap="false" DataFormatString="{0:hh:mm tt}" HtmlEncode="False" Visible="true" />
                            </Columns>
                            <PagerSettings Mode="NextPreviousFirstLast" FirstPageImageUrl="~/App_Themes/Reports/Images/page_first.gif" LastPageImageUrl="~/App_Themes/Reports/Images/page_last.gif" NextPageImageUrl="~/App_Themes/Reports/Images/page_next.gif" PreviousPageImageUrl="~/App_Themes/Reports/Images/page_prev.gif" />
                        </asp:GridView>
                        <asp:ObjectDataSource ID="odsTLs" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetTLs" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="180" >
                            <SelectParameters>
                                <asp:ControlParameter Name="terminal" ControlID="cboTerminal" PropertyName="SelectedValue" Type="string" />
                                <asp:ControlParameter Name="startDate" ControlID="ddpTLDate" PropertyName="FromDate" Type="DateTime" />
                                <asp:ControlParameter Name="endDate" ControlID="ddpTLDate" PropertyName="ToDate" Type="DateTime" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsTL" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="FindTL" EnableCaching="false" >
                            <SelectParameters>
                                <asp:ControlParameter Name="terminal" ControlID="cboTerminal" PropertyName="SelectedValue" Type="string" />
                                <asp:ControlParameter Name="TLNumber" ControlID="txtSearch" PropertyName="Text" Type="string" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboTerminal" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddpTLDate" EventName="DateTimeChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="uprgTLs" runat="server" AssociatedUpdatePanelID="upnlTLs"><ProgressTemplate>updating...</ProgressTemplate></asp:UpdateProgress>
            </td>
        </tr>
   </table>
</asp:Panel>
</asp:Content>

