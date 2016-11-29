<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" StylesheetTheme="Argix" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Issue Management</title>
    <script type="text/javascript" language="jscript">
        function pageLoad() {
        }
    </script>
</head>
<body>
<form id="form1" runat="server">
<div>
<asp:ScriptManager ID="smPage" runat="server" EnablePartialRendering="true" ScriptMode="Auto" />
<asp:UpdatePanel ID="upnlTimer" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Timer ID="tmrRefresh" runat="server" Interval="30000" Enabled="false" OnTick="OnIssueTimerTick"></asp:Timer>
        </ContentTemplate>
</asp:UpdatePanel>
<asp:Table ID="tblPage" runat="server" Height="100%" Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="2">
    <asp:TableRow Font-Size="1px" Height="1px"><asp:TableCell>&nbsp;</asp:TableCell><asp:TableCell>&nbsp;</asp:TableCell><asp:TableCell>&nbsp;</asp:TableCell></asp:TableRow>
    <asp:TableRow Height="36px">
        <asp:TableCell>&nbsp;</asp:TableCell>
        <asp:TableCell ID="tcTitle" ColumnSpan="2" SkinID="PageTitle">
            <asp:Table ID="tblTitle" runat="server" Width="100%" BorderStyle="None" BorderWidth="0px" CellPadding="0" CellSpacing="0"> 
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Image ID="imgTitle" runat="server" ImageUrl="~/App_Themes/Argix/Images/app.gif" ImageAlign="Middle" />
                        &nbsp;<asp:Label id="lblTitle" runat="server" Height="100%" Text="">&nbsp;Argix Direct Issue Management</asp:Label>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Right" VerticalAlign="Middle">
                       <asp:LinkButton ID="lnkReports" runat="server" PostBackUrl="" OnClientClick="javascript:window.open('http://rgxvmweb/argix08/reports'); return false;" ToolTip="Opens the Argix Reports application" SkinID="GlobalLink">Reports</asp:LinkButton>
                        &nbsp;
                       <asp:LinkButton ID="lnkTracking" runat="server" PostBackUrl="" OnClientClick="javascript:window.open('http://rgxvmweb/argix08/tracking/login.aspx'); return false;" ToolTip="Opens the Carton Tracking application" SkinID="GlobalLink">Carton Tracking</asp:LinkButton>
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell Width="24px" Height="100%" VerticalAlign="Top">
            <asp:UpdatePanel ID="upnlFlyout" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                    <asp:Table ID="tblFlyout" runat="server" Width="24px" BorderStyle="None" BorderWidth="0px" CellPadding="2" CellSpacing="0">
                        <asp:TableRow Height="24px"><asp:TableCell Font-Size="1px" style="border-right:solid 1px white;" >&nbsp;</asp:TableCell></asp:TableRow>
                        <asp:TableRow Height="96px" ID="trSearch">
                            <asp:TableCell ID="tcSearchTab" VerticalAlign="Top" style="border:solid 1px white; border-right:solid 1px white;" >
                                <asp:ImageButton ID="imgSearchTab" runat="server" ImageUrl="~/App_Themes/Argix/Images/searchtab.gif" ToolTip="Click to toggle open/close" OnClick="OnSearchTabClicked" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow Height="4px"><asp:TableCell Font-Size="1px" style="border-right:solid 1px white;" >&nbsp;</asp:TableCell></asp:TableRow>
                        <asp:TableRow Height="396px"><asp:TableCell VerticalAlign="Top" style="border-right:solid 1px white;" >&nbsp;</asp:TableCell></asp:TableRow>                        
                    </asp:Table>
                </ContentTemplate>
           </asp:UpdatePanel>
            <asp:UpdateProgress ID="uprgFlyout" runat="server" AssociatedUpdatePanelID="upnlFlyout"><ProgressTemplate>...</ProgressTemplate></asp:UpdateProgress>
        </asp:TableCell>
        <asp:TableCell ID="tcSearch" Height="100%" VerticalAlign="Top">
            <asp:UpdatePanel ID="upnlSearch" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                    <asp:Table ID="tblSearch" runat="server" Width="192px" Height="100%" BorderStyle="Inset" BorderWidth="2px" CellPadding="1" CellSpacing="0" >
	                    <asp:TableRow Height="16px"><asp:TableCell ColumnSpan="2" SkinID="GridTitle"><asp:Image ID="imgSearch" runat="server" ImageUrl="App_Themes/Argix/Images/search.gif" ImageAlign="Middle" />&nbsp;Search</asp:TableCell></asp:TableRow>
                        <asp:TableRow Height="24px"><asp:TableCell ColumnSpan="2">&nbsp;</asp:TableCell></asp:TableRow>
                        <asp:TableRow Height="24px" VerticalAlign="top"><asp:TableCell HorizontalAlign="right">Zone&nbsp;</asp:TableCell><asp:TableCell><asp:TextBox ID="txtZone" runat="server" Width="72px" ></asp:TextBox></asp:TableCell></asp:TableRow>
                        <asp:TableRow Height="24px" VerticalAlign="top"><asp:TableCell HorizontalAlign="right">Store&nbsp;</asp:TableCell><asp:TableCell><asp:TextBox ID="txtStore" runat="server" Width="72px"></asp:TextBox></asp:TableCell></asp:TableRow>
                        <asp:TableRow Height="24px" VerticalAlign="top"><asp:TableCell HorizontalAlign="right">Agent&nbsp;</asp:TableCell><asp:TableCell><asp:TextBox ID="txtAgent" runat="server" Width="72px"></asp:TextBox></asp:TableCell></asp:TableRow>
                        <asp:TableRow Height="24px" VerticalAlign="top"><asp:TableCell HorizontalAlign="right">Company&nbsp;</asp:TableCell><asp:TableCell><asp:TextBox ID="txtCompany" runat="server" Width="72px"></asp:TextBox></asp:TableCell></asp:TableRow>
                        <asp:TableRow Height="24px" VerticalAlign="top"><asp:TableCell HorizontalAlign="right">Type&nbsp;</asp:TableCell><asp:TableCell><asp:TextBox ID="txtType" runat="server" Width="72px"></asp:TextBox></asp:TableCell></asp:TableRow>
                        <asp:TableRow Height="24px" VerticalAlign="top"><asp:TableCell HorizontalAlign="right">Action&nbsp;</asp:TableCell><asp:TableCell><asp:TextBox ID="txtAction" runat="server" Width="72px"></asp:TextBox></asp:TableCell></asp:TableRow>
                        <asp:TableRow Height="24px" VerticalAlign="top"><asp:TableCell HorizontalAlign="right">Received&nbsp;</asp:TableCell><asp:TableCell><asp:TextBox ID="txtReceived" runat="server" Width="72px"></asp:TextBox></asp:TableCell></asp:TableRow>
                        <asp:TableRow Height="24px" VerticalAlign="top"><asp:TableCell HorizontalAlign="right">Subject&nbsp;</asp:TableCell><asp:TableCell><asp:TextBox ID="txtSubject" runat="server" Width="72px"></asp:TextBox></asp:TableCell></asp:TableRow>
                        <asp:TableRow Height="24px" VerticalAlign="top"><asp:TableCell HorizontalAlign="right">Contact&nbsp;</asp:TableCell><asp:TableCell><asp:TextBox ID="txtContact" runat="server" Width="72px"></asp:TextBox></asp:TableCell></asp:TableRow>
                        <asp:TableRow Height="24px" VerticalAlign="top"><asp:TableCell HorizontalAlign="right">Originator&nbsp;</asp:TableCell><asp:TableCell><asp:TextBox ID="txtOriginator" runat="server" Width="72px"></asp:TextBox></asp:TableCell></asp:TableRow>
                        <asp:TableRow Height="24px" VerticalAlign="top"><asp:TableCell HorizontalAlign="right">Coordinator&nbsp;</asp:TableCell><asp:TableCell><asp:TextBox ID="txtCoordinator" runat="server" Width="72px"></asp:TextBox></asp:TableCell></asp:TableRow>
                        <asp:TableRow Height="24px"><asp:TableCell ColumnSpan="2">&nbsp;</asp:TableCell></asp:TableRow>
                        <asp:TableRow Height="24px">
                            <asp:TableCell>
                                &nbsp;<asp:Button ID="cmdSearch" runat="server" Width="72px" Height="20px" Text="Search" CommandName="Search" OnCommand="OnSearch" />
                            </asp:TableCell>
                            <asp:TableCell>
                                &nbsp;<asp:Button ID="cmdReset" runat="server" Width="72px" Height="20px" Text="Reset" CommandName="Reset" OnCommand="OnSearch" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow Height="168px"><asp:TableCell ColumnSpan="2">&nbsp;</asp:TableCell></asp:TableRow>
                    </asp:Table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="imgSearchTab" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="uprgSearch" runat="server" AssociatedUpdatePanelID="upnlSearch"><ProgressTemplate>Searching...</ProgressTemplate></asp:UpdateProgress>
        </asp:TableCell>
        <asp:TableCell Width="100%" VerticalAlign="Top">
            <asp:UpdatePanel ID="upnlIssues" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="100%" border="0px" cellpadding="0px" cellspacing="0px" style="vertical-align:top">
                        <tr>
                            <td>
                                <asp:Table ID="tblMainToolbar" runat="server" Height="24px" Width="100%" BorderWidth="1px" CellPadding="3" CellSpacing="0" BackColor="MenuBar" BorderStyle="Solid" BorderColor="#CC3333" >
                                    <asp:TableRow>
                                        <asp:TableCell Width="216px">
                                            Terminal&nbsp;
                                            <asp:DropDownList ID="cboTerminal" runat="server" Width="144px" DataSourceID="odsTerminals" DatatextField="Description" DataValueField="AgentID" ToolTip="Local Terminals" AutoPostBack="True" OnSelectedIndexChanged="OnTerminalChanged"></asp:DropDownList>
                                            <asp:ObjectDataSource ID="odsTerminals" runat="server" SelectMethod="GetTerminals" TypeName="Argix.Customers.CustomerProxy" CacheExpirationPolicy="Absolute" CacheDuration="900" EnableCaching="true" >
                                                <SelectParameters>
                                                    <asp:SessionParameter Name="agentNumber" SessionField="AgentNumber" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </asp:TableCell>
                                        <asp:TableCell Width="6px">&nbsp;|&nbsp;</asp:TableCell>
                                        <asp:TableCell Width="192px">
                                            View&nbsp;
                                            <asp:DropDownList ID="cboView" runat="server" Width="144px" ToolTip="Issue views" AutoPostBack="True" OnSelectedIndexChanged="OnViewChanged">
                                                <asp:ListItem Text="Current Issues" Value="Current" Selected="True" />
                                                <asp:ListItem Text="Search Issues" Value="Search" />
                                            </asp:DropDownList>
                                        </asp:TableCell>
                                        <asp:TableCell Width="6px">&nbsp;|&nbsp;</asp:TableCell>
                                        <asp:TableCell Width="24px"><asp:ImageButton ID="btnIssuesNew" runat="server" ImageUrl="~/App_Themes/Argix/Images/file.gif" ToolTip="Create a new issue..." CommandName="New" OnCommand="OnIssueToolbarClick" /></asp:TableCell>
                                        <asp:TableCell Width="6px">&nbsp;|&nbsp;</asp:TableCell>
                                        <asp:TableCell Width="24px"><asp:ImageButton ID="btnIssuesPrint" runat="server" ImageUrl="~/App_Themes/Argix/Images/print.gif" ToolTip="Print issue view" /></asp:TableCell>
                                        <asp:TableCell Width="24px"><asp:ImageButton ID="btnIssuesRefresh" runat="server" ImageUrl="~/App_Themes/Argix/Images/refresh.gif" ToolTip="Refresh issue view" CommandName="Refresh" OnCommand="OnIssueToolbarClick" /></asp:TableCell>
                                        <asp:TableCell Width="6px">&nbsp;|&nbsp;</asp:TableCell>
                                        <asp:TableCell Width="240px">
                                            Find
                                            &nbsp;<asp:TextBox ID="txtSearch" Width="144px" ToolTip="Search issue text" runat="server"></asp:TextBox>
                                            &nbsp;<asp:ImageButton ID="btnIssuesSearch" ImageAlign="Middle" runat="server" ImageUrl="~/App_Themes/Argix/Images/findreplace.gif" ToolTip="Search issue text" CommandName="Search" OnCommand="OnIssueToolbarClick" />
                                        </asp:TableCell>
                                        <asp:TableCell>&nbsp;</asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </td>
                        </tr>
                        <tr style="font-size:1px; height:3px"><td>&nbsp;</td></tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlIssues" runat="server" Width="100%" Height="192px" BorderStyle="Inset" BorderWidth="1px" BorderColor="Control" BackColor="White" ScrollBars="Vertical">
                                <asp:GridView ID="grdIssues" runat="server" Width="100%" BackColor="Window" AutoGenerateColumns="False" DataSourceID="odsIssues" DataKeyNames="ID"  AllowSorting="true" OnRowDataBound="OnIssueRowDataBound" OnSelectedIndexChanged="OnIssueSelected" OnSorting="OnIssuesSorting" OnSorted="OnIssuesSorted" >
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" HeaderStyle-Width="16px" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" ShowSelectButton="True" />
                                        <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-Width="0px" ItemStyle-Width="0px" Visible="True" />
                                        <asp:BoundField DataField="Zone" HeaderText="Zone" HeaderStyle-Width="72px" ItemStyle-Wrap="false" SortExpression="Zone" />
                                        <asp:BoundField DataField="StoreNumber" HeaderText="Store" HeaderStyle-Width="72px" ItemStyle-Wrap="false" SortExpression="StoreNumber" />
                                        <asp:BoundField DataField="AgentNumber" HeaderText="Agent" HeaderStyle-Width="72px" ItemStyle-Wrap="false" SortExpression="AgentNumber" />
                                        <asp:BoundField DataField="CompanyName" HeaderText="Company" HeaderStyle-Width="144px" ItemStyle-Wrap="false" SortExpression="Company" />
                                        <asp:BoundField DataField="Type" HeaderText="Type" HeaderStyle-Width="72px" ItemStyle-Wrap="false" SortExpression="Type" />
                                        <asp:BoundField DataField="LastActionDescription" HeaderText="Action" HeaderStyle-Width="72px" ItemStyle-Wrap="false" SortExpression="LastActionDescription" />
                                        <asp:BoundField DataField="LastActionCreated" HeaderText="Received" HeaderStyle-Width="144px" ItemStyle-Wrap="false" SortExpression="LastActionCreated" HtmlEncode="true" DataFormatString="{0}" />
                                        <asp:BoundField DataField="Subject" HeaderText="Subject" HeaderStyle-Width="240px" ItemStyle-Width="240px" ItemStyle-Wrap="true" SortExpression="Subject" />
                                        <asp:BoundField DataField="ContactName" HeaderText="Contact" HeaderStyle-Width="144px" ItemStyle-Wrap="false" SortExpression="ContactName" />
                                        <asp:BoundField DataField="LastActionUserID" HeaderText="Last User" HeaderStyle-Width="96px" ItemStyle-Wrap="false" SortExpression="LastActionUserID" />
                                        <asp:BoundField DataField="FirstActionUserID" HeaderText="Originator" HeaderStyle-Width="96px" ItemStyle-Wrap="false" SortExpression="FirstActionUserID" ></asp:BoundField>
                                        <asp:BoundField DataField="Coordinator" HeaderText="Coordinator" HeaderStyle-Width="96px" ItemStyle-Wrap="false" SortExpression="Coordinator" ></asp:BoundField>
                                   </Columns>
                                </asp:GridView>
                                <asp:ObjectDataSource ID="odsIssues" runat="server" TypeName="Argix.Customers.CustomerProxy" SelectMethod="GetIssues">
                                    <SelectParameters>
                                        <asp:ControlParameter Name="agentNumber" ControlID="cboTerminal" PropertyName="SelectedValue" DefaultValue="" ConvertEmptyStringToNull="true" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsSearch" runat="server" TypeName="Argix.Customers.CustomerProxy" SelectMethod="SearchIssues">
                                    <SelectParameters>
                                        <asp:ControlParameter Name="agentNumber" ControlID="cboTerminal" PropertyName="SelectedValue" DefaultValue="" ConvertEmptyStringToNull="true" />
                                       <asp:ControlParameter Name="searchText" ControlID="txtSearch" PropertyName="Text" ConvertEmptyStringToNull="true" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsSearchAdv" runat="server" TypeName="Argix.Customers.CustomerProxy" SelectMethod="SearchIssuesAdvanced">
                                    <SelectParameters>
                                        <asp:ControlParameter Name="agentNumber" ControlID="cboTerminal" PropertyName="SelectedValue" DefaultValue="" ConvertEmptyStringToNull="true" />
                                        <asp:ControlParameter Name="zone" ControlID="txtZone" PropertyName="Text" ConvertEmptyStringToNull="true" Type="String" />
                                        <asp:ControlParameter Name="store" ControlID="txtStore" PropertyName="Text" ConvertEmptyStringToNull="true" Type="String" />
                                        <asp:ControlParameter Name="agent" ControlID="txtAgent" PropertyName="Text" ConvertEmptyStringToNull="true" Type="String" />
                                        <asp:ControlParameter Name="company" ControlID="txtCompany" PropertyName="Text" ConvertEmptyStringToNull="true" Type="String" />
                                        <asp:ControlParameter Name="type" ControlID="txtType" PropertyName="Text" ConvertEmptyStringToNull="true" Type="String" />
                                        <asp:ControlParameter Name="action" ControlID="txtAction" PropertyName="Text" ConvertEmptyStringToNull="true" Type="String" />
                                        <asp:ControlParameter Name="received" ControlID="txtReceived" PropertyName="Text" ConvertEmptyStringToNull="true" Type="String" />
                                        <asp:ControlParameter Name="subject" ControlID="txtSubject" PropertyName="Text" ConvertEmptyStringToNull="true" Type="String" />
                                        <asp:ControlParameter Name="contact" ControlID="txtContact" PropertyName="Text" ConvertEmptyStringToNull="true" Type="String" />
                                        <asp:ControlParameter Name="originator" ControlID="txtOriginator" PropertyName="Text" ConvertEmptyStringToNull="true" Type="String" />
                                        <asp:ControlParameter Name="coordinator" ControlID="txtCoordinator" PropertyName="Text" ConvertEmptyStringToNull="true" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>                                
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0px" cellpadding="0px" cellspacing="0px" style="vertical-align:top">
                        <tr style="font-size:1px; height:1px"><td width="288px">&nbsp;</td><td width="3px">&nbsp;</td><td>&nbsp;</td></tr>
                        <tr>
                            <td valign="top" style="width:288px">
                                <asp:Table ID="tblActions" runat="server" Height="100%" BorderStyle="None" BorderWidth="0px" CellPadding="0" CellSpacing="0">
                                    <asp:TableRow>
                                        <asp:TableCell SkinID="WindowTitle">
                                            <asp:Label ID="lblActions" runat="server" Height="18px" Width="100%" Text="Actions"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow Height="24px">
                                        <asp:TableCell>
                                            <asp:Table ID="tblActionsToolbar" runat="server" Width="100%" Height="24px" CellPadding="3" CellSpacing="0" BackColor="MenuBar" BorderStyle="Solid" BorderWidth="1px" BorderColor="#CC3333" >
                                                <asp:TableRow>
                                                    <asp:TableCell Width="24px"><asp:ImageButton ID="btnActionsNew" runat="server" ImageUrl="~/App_Themes/Argix/Images/file.gif" ToolTip="Add a new action..." CommandName="New" OnCommand="OnActionToolbarClick" /></asp:TableCell>
                                                    <asp:TableCell Width="6px">&nbsp;|&nbsp;</asp:TableCell>
                                                    <asp:TableCell Width="24px"><asp:ImageButton ID="btnActionsPrint" runat="server" ImageUrl="~/App_Themes/Argix/Images/print.gif" ToolTip="Print actions" /></asp:TableCell>
                                                    <asp:TableCell Width="24px"><asp:ImageButton ID="btnActionsRefresh" runat="server" ImageUrl="~/App_Themes/Argix/Images/refresh.gif" ToolTip="Refresh action view" CommandName="Refresh" OnCommand="OnActionToolbarClick" /></asp:TableCell>
                                                    <asp:TableCell Width="6px">&nbsp;|&nbsp;</asp:TableCell>
                                                    <asp:TableCell Width="24px"><asp:ImageButton ID="btnAttachmentNew" runat="server" ImageUrl="~/App_Themes/Argix/Images/attach.gif" ToolTip="Add a new attachment to the selected action..." CommandName="NewAttachment" OnCommand="OnActionToolbarClick" /></asp:TableCell>
                                                    <asp:TableCell>&nbsp;</asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow><asp:TableCell Font-Size="1px" Height="3px">&nbsp;</asp:TableCell></asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:ListView ID="lsvActions" runat="server" DataSourceID="odsActions" DataKeyNames="ID, IssueID" OnSelectedIndexChanged="OnActionSelected" >
                                                <LayoutTemplate>
                                                    <table id="Table1" runat="server" border="0" cellpadding="1" cellspacing="1">
                                                        <tr id="Tr1" runat="server" style="background-color:ButtonFace;">
                                                            <td id="Td1" runat="server" style="width:24px; border:inset 1px ButtonShadow;">&nbsp;</td>
                                                            <td id="Td2" runat="server" style="width:24px; border:inset 1px ButtonShadow;">&nbsp;</td>
                                                            <td id="Td3" runat="server" style="width:120px; border:inset 1px ButtonShadow;">&nbsp;Created</td>
                                                            <td id="Td4" runat="server" style="width:120px; border:inset 1px ButtonShadow;">&nbsp;User</td>
                                                        </tr>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                        <tr><td>&nbsp;</td></tr>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr id="Tr2" runat="server" style="background-color:white">
                                                        <td id="Td5" runat="server" valign="top"><asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/App_Themes/Argix/Images/select.gif" CommandName="Select" /></td>
                                                        <td id="Td6" runat="server" valign="top"><asp:Image ID="imgAttachments" runat="server" ImageUrl="~/App_Themes/Argix/Images/attachment.gif" Visible='<%# (Convert.ToInt32(Eval("Attachments")) > 0 ? true : false) %>' /></td>
                                                        <td id="Td7" runat="server" valign="top"><asp:Label ID="lblCreated" runat="server" Text='<%# Eval("Created") %>' Width="144px" /></td>
                                                        <td id="Td8" runat="server" valign="top"><asp:Label ID="lblUser" runat="server" Text='<%# Eval("UserID") %>' Width="120px" /></td>
                                                    </tr>
                                                </ItemTemplate>
                                                <SelectedItemTemplate>
                                                    <tr id="Tr3" runat="server" style="color:HighlightText; background-color:Highlight">
                                                        <td id="Td9" runat="server" valign="top"><asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/App_Themes/Argix/Images/select.gif" CommandName="Select" /></td>
                                                        <td id="Td10" runat="server" valign="top"><asp:Image ID="imgAttachments" runat="server" ImageUrl="~/App_Themes/Argix/Images/attachment.gif" Visible='<%# (Convert.ToInt32(Eval("Attachments")) > 0 ? true : false) %>' /></td>
                                                        <td id="Td11" runat="server" valign="top"><asp:Label ID="lblCreated" runat="server" Text='<%# Eval("Created") %>' Width="144px" /></td>
                                                        <td id="Td12" runat="server" valign="top"><asp:Label ID="lblUser" runat="server" Text='<%# Eval("UserID") %>' Width="120px" /></td>
                                                    </tr>
                                                </SelectedItemTemplate>
                                                <EmptyDataTemplate>
                                                    <table id="Table2" runat="server" border="0" cellpadding="1" cellspacing="1">
                                                        <tr id="Tr4" runat="server" style="background-color:ButtonFace;">
                                                            <td id="Td13" runat="server" style="width:24px; border:inset 1px ButtonShadow;">&nbsp;</td>
                                                            <td id="Td14" runat="server" style="width:24px; border:inset 1px ButtonShadow;">&nbsp;</td>
                                                            <td id="Td15" runat="server" style="width:120px; border:inset 1px ButtonShadow;">&nbsp;Created</td>
                                                            <td id="Td16" runat="server" style="width:120px; border:inset 1px ButtonShadow;">&nbsp;User</td>
                                                        </tr>
                                                        <tr id="Tr5" runat="server" style="background-color:white; height:192px"><td id="Td17" runat="server" style="width:24px;">&nbsp;</td><td id="Td18" runat="server" style="width:24px;">&nbsp;</td><td id="Td19" runat="server" style="width:120px;">&nbsp;</td><td id="Td20" runat="server" style="width:120px;">&nbsp;</td></tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                            <asp:ObjectDataSource ID="odsActions" runat="server" SelectMethod="GetIssueActions" TypeName="Argix.Customers.IssueMgtServiceClient">
                                                <SelectParameters>
                                                    <asp:ControlParameter Name="issueID" ControlID="grdIssues" PropertyName="SelectedDataKey.Values[0]" Type="Int64" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow><asp:TableCell>&nbsp;</asp:TableCell></asp:TableRow>
                                </asp:Table>
                            </td>
                            <td valign="top" style="width:3px; background-color:ButtonFace">&nbsp;</td>
                            <td valign="top">
                                <asp:Table ID="tblAction" runat="server" Width="100%" Height="100%" BorderStyle="None" BorderWidth="0px" CellPadding="0" CellSpacing="0">
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="2" SkinID="WindowTitle">
                                            <asp:Label ID="lblSubject" runat="server" Width="100%" Height="18px" Text="[Subject]"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow Height="42px">
                                        <asp:TableCell Width="42px" BackColor="Window">
                                            &nbsp;<asp:Image ID="imgAttachments" runat="server" ImageUrl="~/App_Themes/Argix/Images/attachments.gif" ToolTip="Attachments" BorderStyle="None" />
                                        </asp:TableCell>
                                        <asp:TableCell Width="100%" BackColor="Window">
                                            <asp:ListView ID="lsvAttachments" runat="server" DataSourceID="odsAttachments" DataKeyNames="Filename">
                                                <LayoutTemplate>
                                                    <table id="Table3" runat="server" border="0" cellpadding="8" cellspacing="6" >
                                                        <tr ID="itemPlaceholderContainer" runat="server">
                                                            <td ID="itemPlaceholder" runat="server"></td>
                                                        </tr>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <td id="Td21" runat="server" style="border: solid 1px ButtonFace">
                                                        <asp:HyperLink ID="lnkFilename" runat="server" Target="_blank" NavigateUrl='<%# "~/Attachment.aspx?id=" + Eval("ID") + "&name=" + HttpUtility.UrlEncode(Eval("Filename").ToString()) %>'><%# Eval("Filename") %></asp:HyperLink>
                                                    </td>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <table id="Table4" runat="server" border="0" cellpadding="8" cellspacing="6">
                                                        <tr id="Tr6" runat="server"><td id="Td22" runat="server">&nbsp;</td></tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                            <asp:ObjectDataSource ID="odsAttachments" runat="server" SelectMethod="GetAttachments" TypeName="Argix.Customers.IssueMgtServiceClient">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="lsvActions" Name="issueID" PropertyName="SelectedDataKey.Values[1]" Type="Int64" />
                                                    <asp:ControlParameter ControlID="lsvActions" Name="actionID" PropertyName="SelectedDataKey.Values[0]" Type="Int64" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow><asp:TableCell ColumnSpan="2" Height="3px" Font-Size="1px" BackColor="ButtonFace">&nbsp;</asp:TableCell></asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="2">
                                            <asp:ListView ID="lsvAction" runat="server"  DataSourceID="odsActionDetail">
                                                <LayoutTemplate>
                                                    <div id="itemPlaceholder" style="width:100%" runat="server" ></div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <table id="Table5" runat="server" border="0" cellpadding="3" cellspacing="0" style="width:100%; background-color:Window">
                                                        <tr id="Tr7" runat="server"><td style="font-weight:bold"><%# Eval("Created") %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%# Eval("UserID") %>,&nbsp;&nbsp;<%# Eval("TypeName") %></td></tr>
                                                        <tr id="Tr8" runat="server"><td>&nbsp;</td></tr>
                                                        <tr id="Tr9" runat="server"><td style="white-space:normal"><%# Eval("Comment") %></td></tr>
                                                        <tr id="Tr10" runat="server"><td><hr /></td></tr>
                                                    </table>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <table id="Table6" runat="server" border="0" cellpadding="3" cellspacing="0" style="width:100%; background-color:Window">
                                                        <tr id="Tr11" runat="server" style="background-color:white; height:192px"><td>&nbsp;</td></tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                            <asp:ObjectDataSource ID="odsActionDetail" runat="server" SelectMethod="GetActions" TypeName="Argix.Customers.CustomerProxy">
                                                <SelectParameters>
                                                    <asp:ControlParameter Name="issueID" ControlID="grdIssues" PropertyName="SelectedDataKey.Values[0]" Type="Int64" />
                                                    <asp:ControlParameter Name="actionID" ControlID="lsvActions" PropertyName="SelectedDataKey.Values[0]" Type="Int64" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow><asp:TableCell ColumnSpan="2">&nbsp;</asp:TableCell></asp:TableRow>
                                </asp:Table>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="tmrRefresh" EventName="Tick" />
                    <asp:AsyncPostBackTrigger ControlID="cmdSearch" EventName="Command" />
                    <asp:AsyncPostBackTrigger ControlID="imgSearchTab" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
</div>
</form>
</body>
</html>
