<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default"  StylesheetTheme="Argix" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register Src="DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Load Tenders</title>
</head>
<body>
<form id="form1" runat="server">
    <asp:ScriptManager ID="smPage" runat="server" EnablePartialRendering="true" ScriptMode="Auto"></asp:ScriptManager>
    <table width="100%" border="0" cellpadding="0px" cellspacing="0px">
        <tr><td style="font-size:1px; width:24px">&nbsp;</td><td style="font-size:1px">&nbsp;</td></tr>
        <tr style="height:32px">
            <td>&nbsp;</td>
            <td style="font-size:1.0em; vertical-align:middle; padding-left:6px; background-image:url(App_Themes/Argix/Images/pagetitle.gif); background-repeat:repeat-x;">
                <table width="100%" border="0" cellpadding="0px" cellspacing="0px"> 
                    <tr>
                        <td valign="top" style="font-size:1.5em">
                            <img src="App_Themes/Argix/Images/app.gif" alt="Argix Direct Load Tenders" />&nbsp;Argix Direct Load Tenders
                        </td>
                        <td align="right" valign="bottom">&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td colspan="2" style="font-size:1px; height:12px">&nbsp;</td></tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Panel ID="pnlLoads" runat="server" width="480px" GroupingText="Setup" ForeColor="ControlText">
                    <table width="100%" border="0" cellpadding="0" cellspacing="3px">
                        <tr style="height:12px"><td colspan="3">&nbsp;</td></tr>
                        <tr style="height:24px">
                            <td width="72px" align="right">Client&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="cboClient" runat="server" width="240px" DataSourceID="odsClients" DataTextField="ClientName" DataValueField="ClientNumber" AutoPostBack="true" OnSelectedIndexChanged="OnClientChanged"></asp:DropDownList>
                                <asp:ObjectDataSource ID="odsClients" runat="server" TypeName="EnterpriseService" SelectMethod="GetClients" EnableCaching="true" CacheExpirationPolicy="Absolute" CacheDuration="0" />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr style="height:12px"><td colspan="3">&nbsp;</td></tr>
                        <tr style="height:48px">
                            <td colspan="2" width="288px">
                                <uc1:DualDateTimePicker ID="ddpTenders" runat="server" EnableTheming="true" DateDaysBack="30" DateDaysForward="0" DateDaysSpread="0" OnDateTimeChanged="OnFromToDateChanged" />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr><td colspan="2" style="font-size:1px; height:24px">&nbsp;</td></tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
                    <tr style="height:18px"><td style="font-size:1.0em; vertical-align:middle; padding-left:6px; background-image:url(App_Themes/Argix/Images/gridtitle.gif); background-repeat:repeat-x;">&nbsp;Load Tenders</td></tr>
                    <tr>
                        <td valign="top">
                            <asp:Panel id="pnlTenders" runat="server" Width="100%" Height="288px" BorderStyle="Inset" BorderWidth="1px" ScrollBars="Auto">
                                <asp:UpdatePanel ID="upnlTenders" runat="server" UpdateMode="Conditional" >
                                <ContentTemplate>
                                    <asp:GridView ID="grdTenders" runat="server" width="100%" AutoGenerateColumns="False" DataSourceID="odsTenders" DataKeyNames="Load" AllowSorting="True">
                                        <Columns>
                                             <asp:TemplateField HeaderText="" HeaderStyle-Width="24px" >
                                                <HeaderTemplate><asp:CheckBox ID="chkAll" runat="server" Enabled="true" AutoPostBack="true" OnCheckedChanged="OnAllTendersSelected"/></HeaderTemplate>
                                                <ItemTemplate><asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="OnTenderSelected"/></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Load" HeaderText="Load#" HeaderStyle-Width="72px" SortExpression="Load ASC" />
                                            <asp:BoundField DataField="ReferenceNumber" HeaderText="Ref #" HeaderStyle-Width="72px" SortExpression="ReferenceNumber ASC" />
                                            <asp:BoundField DataField="Location" HeaderText="Location" HeaderStyle-Width="144px" ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="AddressLine1" HeaderText="Address Line 1" HtmlEncode="False" NullDisplayText=" " ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="AddressLine2" HeaderText="Address Line 2" HtmlEncode="False" NullDisplayText=" " ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="City" HeaderText="City" HeaderStyle-Width="120px" />
                                            <asp:BoundField DataField="StateOrProvince" HeaderText="State" HeaderStyle-Width="36px" />
                                            <asp:BoundField DataField="PostalCode" HeaderText="Zip" HeaderStyle-Width="48px" />
                                            <asp:BoundField DataField="LocationNumber" HeaderText="Loc #" HeaderStyle-Width="48px" SortExpression="LocationNumber ASC" />
                                            <asp:BoundField DataField="LocationID" HeaderText="Loc ID" HeaderStyle-Width="72px" SortExpression="LocationID ASC" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:ObjectDataSource ID="odsTenders" runat="server" TypeName="EnterpriseService" SelectMethod="GetLoadTenders" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="300" >
                                        <SelectParameters>
                                            <asp:ControlParameter Name="clientNumber" ControlID="cboClient" PropertyName="SelectedValue" Type="string" />
                                            <asp:ControlParameter Name="startDate" ControlID="ddpTenders" PropertyName="FromDate" Type="DateTime" />
                                            <asp:ControlParameter Name="endDate" ControlID="ddpTenders" PropertyName="ToDate" Type="DateTime" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cboClient" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddpTenders" EventName="DateTimeChanged" />
                                </Triggers>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td colspan="2" style="font-size:1px; height:12px">&nbsp;</td></tr>
        <tr>            
            <td>&nbsp;</td>
            <td style="text-align: left;">
                <asp:Button ID="btnGo" runat="server" Text="GO" width="72px" OnClick="OnOK"></asp:Button>
            </td>
        </tr>
    </table>
</form>
</body>
</html>
