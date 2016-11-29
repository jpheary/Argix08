<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IssueMgt.aspx.cs" Inherits="IssueMgt" StylesheetTheme="Argix" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Issue Mgt v3.1.0.062310</title>
    <style type="text/css">
        .FixedHeader {
	        position: relative;
	        top : expression(this.offsetParent.scrollTop - 2);
	        z-index: 10;
        }
    </style>
    <script type="text/javascript" language="jscript">
        function pageLoad() {
        }
    </script>
</head>
<body>
<form id="form1" runat="server">
<div>
<asp:ScriptManager ID="smPage" runat="server" EnablePartialRendering="true" ScriptMode="Debug">
    <Services>
        <asp:ServiceReference Path="~/IssueMgtServiceClient.svc" />
    </Services>
</asp:ScriptManager>
<script type="text/javascript" language="jscript">
    var scrollTop;
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(OnBeginRequest);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(OnEndRequest);
    function OnBeginRequest(sender, args) {
        scrollTop = document.getElementById('pnlIssues').scrollTop;
    }
    function OnEndRequest(sender, args) {
        document.getElementById('pnlIssues').scrollTop = scrollTop;
    }
</script>
<asp:Timer ID="tmrRefresh" runat="server" Interval="30000" Enabled="true" OnTick="OnIssueTimerTick"></asp:Timer>
<asp:Table ID="tblPage" runat="server" Height="100%" Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="0">
    <asp:TableRow><asp:TableCell Font-Size="1px" Height="1px">&nbsp;</asp:TableCell></asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <asp:UpdatePanel ID="upnlIssues" runat="server" RenderMode="Block" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <ContentTemplate>
                <asp:Table ID="tblIssues" runat="server" Height="100%" Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="0">
                    <asp:TableRow ID="trIssuesToolbar" Height="24px">
                        <asp:TableCell>
                            <asp:Table ID="tblIssuesToolbar" runat="server" Height="100%" Width="100%" BorderWidth="0" CellPadding="3" CellSpacing="0" BackColor="MenuBar" >
                                <asp:TableRow>
                                    <asp:TableCell Width="144px"><asp:DropDownList ID="cboView" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="OnViewChanged" ToolTip="Issue views"></asp:DropDownList></asp:TableCell>
                                    <asp:TableCell Width="24px"><asp:ImageButton ID="imgIssuesNew" runat="server" ImageUrl="~/App_Themes/Argix/Images/file.gif" CommandName="New" OnCommand="OnIssueToolbarClick" ToolTip="Create a new issue..." /></asp:TableCell>
                                    <asp:TableCell Width="24px"><asp:ImageButton ID="imgIssuesRefresh" runat="server" ImageUrl="~/App_Themes/Argix/Images/refresh.gif" CommandName="Refresh" OnCommand="OnIssueToolbarClick" ToolTip="Refresh issue view" /></asp:TableCell>
                                    <asp:TableCell Width="24px">&nbsp;</asp:TableCell>
                                    <asp:TableCell Width="24px">&nbsp;</asp:TableCell>
                                    <asp:TableCell>&nbsp;</asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow><asp:TableCell Font-Size="1px" Height="3px">&nbsp;</asp:TableCell></asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell> 
                            <asp:Panel ID="pnlIssues" runat="server" Width="100%" Height="192px" ScrollBars="Auto">
                                <asp:GridView ID="grdIssues" runat="server" Width="100%" Height="100%" AutoGenerateColumns="False" DataSourceID="odsIssues" DataKeyNames="ID" AllowSorting="true" OnRowDataBound="OnIssueRowDataBound" OnSelectedIndexChanged="OnIssueSelected">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" HeaderStyle-Width="16px" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" ShowSelectButton="True" />
                                        <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-Width="48px" ItemStyle-Wrap="false" Visible="False" />
                                        <asp:BoundField DataField="Zone" HeaderText="Zone" HeaderStyle-Width="72px" ItemStyle-Wrap="false" SortExpression="Zone" />
                                        <asp:BoundField DataField="StoreNumber" HeaderText="Store" HeaderStyle-Width="72px" ItemStyle-Wrap="false" SortExpression="StoreNumber" />
                                        <asp:BoundField DataField="AgentNumber" HeaderText="Agent" HeaderStyle-Width="72px" ItemStyle-Wrap="false" SortExpression="AgentNumber" />
                                        <asp:BoundField DataField="CompanyName" HeaderText="Company" HeaderStyle-Width="144px" ItemStyle-Wrap="false" SortExpression="Company" />
                                        <asp:BoundField DataField="Type" HeaderText="Type" HeaderStyle-Width="72px" ItemStyle-Wrap="false" SortExpression="Type" />
                                        <asp:BoundField DataField="LastActionDescription" HeaderText="Action" HeaderStyle-Width="72px" ItemStyle-Wrap="false" SortExpression="LastActionDescription" />
                                        <asp:BoundField DataField="LastActionCreated" HeaderText="Received" HeaderStyle-Width="144px" ItemStyle-Wrap="false" SortExpression="LastActionCreated" HtmlEncode="true" DataFormatString="{0}" />
                                        <asp:BoundField DataField="Subject" HeaderText="Subject" HeaderStyle-Width="288px" ItemStyle-Wrap="false" SortExpression="Subject" />
                                        <asp:BoundField DataField="ContactName" HeaderText="Contact" HeaderStyle-Width="144px" ItemStyle-Wrap="false" SortExpression="ContactName" />
                                        <asp:BoundField DataField="LastActionUserID" HeaderText="Last User" HeaderStyle-Width="96px" ItemStyle-Wrap="false" SortExpression="LastActionUserID" />
                                    </Columns>
                                </asp:GridView>
                                <asp:ObjectDataSource ID="odsIssues" runat="server" TypeName="Argix.Customers.IssueMgtServiceClient" SelectMethod="GetIssues"></asp:ObjectDataSource>
                            </asp:Panel>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="imgIssuesRefresh" EventName="Command" />
                    <asp:AsyncPostBackTrigger ControlID="tmrRefresh" EventName="Tick" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="uprgIssues" runat="server" AssociatedUpdatePanelID="upnlIssues">
                <ProgressTemplate>
                    Updating issues...
                </ProgressTemplate>
            </asp:UpdateProgress>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow><asp:TableCell Font-Size="1px" Height="6px">&nbsp;</asp:TableCell></asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <asp:UpdatePanel ID="upnlActions" runat="server" RenderMode="Block" UpdateMode="Conditional">
                <ContentTemplate>
                <asp:Table ID="tblActions" runat="server" Height="100%" Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="0">
                    <asp:TableRow Font-Size="1px"><asp:TableCell Width="297px">&nbsp;</asp:TableCell><asp:TableCell Width="3px">&nbsp;</asp:TableCell><asp:TableCell Width="780px">&nbsp;</asp:TableCell></asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3" VerticalAlign="Middle">
                            <asp:Label ID="lblTitle" runat="server" Width="100%" Height="18px" Text="[Subject]" SkinID="PageTitle"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow><asp:TableCell ColumnSpan="3" Font-Size="1px" Height="3px">&nbsp;</asp:TableCell></asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell VerticalAlign="Top">
                            <asp:Table ID="tblActionsNav" runat="server" Width="100%" Height="100%" BorderStyle="None" BorderWidth="0px" CellPadding="0" CellSpacing="0">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblActions" runat="server" Height="18px" Width="100%" Text="Actions" SkinID="WindowTitle"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Height="24px">
                                    <asp:TableCell>
                                        <asp:Table ID="tblActionsToolbar" runat="server" Height="100%" Width="100%" BorderWidth="0" CellPadding="3" CellSpacing="0" BackColor="MenuBar" >
                                            <asp:TableRow>
                                                <asp:TableCell Width="24px"><asp:ImageButton ID="imgActionsNew" runat="server" ImageUrl="~/App_Themes/Argix/Images/file.gif" CommandName="New" OnCommand="OnActionToolbarClick" ToolTip="Add a new action..." /></asp:TableCell>
                                                <asp:TableCell Width="24px"><asp:ImageButton ID="imgActionsRefresh" runat="server" ImageUrl="~/App_Themes/Argix/Images/refresh.gif" CommandName="Refresh" OnCommand="OnActionToolbarClick" ToolTip="Refresh action view" /></asp:TableCell>
                                                <asp:TableCell Width="12px">&nbsp;|&nbsp;</asp:TableCell>
                                                <asp:TableCell Width="24px"><asp:ImageButton ID="imgAttachmentNew" runat="server" ImageUrl="~/App_Themes/Argix/Images/attach.gif" CommandName="NewAttachment" OnCommand="OnActionToolbarClick" ToolTip="Add a new attachment..." /></asp:TableCell>
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
                                                <table id="tblActions" runat="server" border="0" cellpadding="2" cellspacing="1">
                                                    <tr runat="server" style="background-color:ButtonFace">
                                                        <th runat="server">&nbsp;</th>
                                                        <th runat="server">&nbsp;</th>
                                                        <th runat="server">Created</th>
                                                        <th runat="server">User</th>
                                                    </tr>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr runat="server">
                                                    <td runat="server" valign="top"><asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/App_Themes/Argix/Images/select.gif" CommandName="Select" /></td>
                                                    <td runat="server" valign="top"><asp:Image ID="imgAttachments" runat="server" ImageUrl="~/App_Themes/Argix/Images/attachment.gif" Visible='<%# (Convert.ToInt32(Eval("Attachments")) > 0 ? true : false) %>' /></td>
                                                    <td runat="server" valign="top"><asp:Label ID="lblCreated" runat="server" Text='<%# Eval("Created") %>' Width="144px" /></td>
                                                    <td runat="server" valign="top"><asp:Label ID="lblUser" runat="server" Text='<%# Eval("UserID") %>' Width="120px" /></td>
                                                </tr>
                                            </ItemTemplate>
                                            <SelectedItemTemplate>
                                                <tr runat="server" style="color:HighlightText; background-color:Highlight">
                                                    <td runat="server" valign="top"><asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/App_Themes/Argix/Images/select.gif" CommandName="Select" /></td>
                                                    <td runat="server" valign="top"><asp:Image ID="imgAttachments" runat="server" ImageUrl="~/App_Themes/Argix/Images/attachment.gif" Visible='<%# (Convert.ToInt32(Eval("Attachments")) > 0 ? true : false) %>' /></td>
                                                    <td runat="server" valign="top"><asp:Label ID="lblCreated" runat="server" Text='<%# Eval("Created") %>' Width="144px" /></td>
                                                    <td runat="server" valign="top"><asp:Label ID="lblUser" runat="server" Text='<%# Eval("UserID") %>' Width="120px" /></td>
                                                </tr>
                                            </SelectedItemTemplate>
                                            <EmptyDataTemplate>
                                                <table runat="server">
                                                    <tr runat="server">
                                                        <td runat="server">&nbsp;</td>
                                                        <td runat="server">&nbsp;</td>
                                                        <td runat="server">&nbsp;</td>
                                                        <td runat="server">&nbsp;</td>
                                                    </tr>
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
                        </asp:TableCell>
                        <asp:TableCell BackColor="ButtonFace">&nbsp;</asp:TableCell>
                        <asp:TableCell VerticalAlign="Top">
                            <asp:Table ID="tblActionDetail" runat="server" Width="100%" Height="100%" BorderStyle="None" BorderWidth="0px" CellPadding="0" CellSpacing="0">
                                <asp:TableRow Height="42px">
                                    <asp:TableCell Width="42px">
                                        &nbsp;<asp:Image ID="imgAttachments" runat="server" ImageUrl="~/App_Themes/Argix/Images/inbox.gif" BorderStyle="None" BorderWidth="1" BorderColor="ButtonFace" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:ListView ID="lsvAttachments" runat="server" DataSourceID="odsAttachments" DataKeyNames="Filename">
                                            <LayoutTemplate>
                                                <table id="tbl1" runat="server" border="0" cellpadding="8" cellspacing="6">
                                                    <tr ID="itemPlaceholderContainer" runat="server">
                                                        <td ID="itemPlaceholder" runat="server"></td>
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <td id="Td1" runat="server" style="border: solid 1px ButtonFace">
                                                    <asp:HyperLink ID="lnkFilename" runat="server" Target="_blank" NavigateUrl='<%# "~/Attachment.aspx?id=" + Eval("ID") + "&name=" + HttpUtility.UrlEncode(Eval("Filename").ToString()) %>'><%# Eval("Filename") %></asp:HyperLink>
                                                </td>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="tbl1" runat="server" border="0" cellpadding="8" cellspacing="6">
                                                    <tr runat="server"><td runat="server">&nbsp;</td></tr>
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
                                        <asp:ListView ID="lsvAction" runat="server" DataSourceID="odsActionDetail">
                                            <LayoutTemplate>
                                                <div id="itemPlaceholder" runat="server" style="width:100%" ></div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <table id="tbl1" runat="server" style="width:100%" cellpadding="3">
                                                    <tr id="tr1" runat="server"><td style="font-weight:bold"><%# Eval("Created") %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%# Eval("UserID") %>,&nbsp;&nbsp;<%# Eval("TypeName") %></td></tr>
                                                    <tr id="tr2" runat="server"><td>&nbsp;</td></tr>
                                                    <tr id="tr3" runat="server"><td style="white-space:normal"><%# Eval("Comment") %></td></tr>
                                                    <tr id="tr4" runat="server"><td><hr /></td></tr>
                                                </table>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <table id="tbl1" runat="server" style="width:100%">
                                                    <tr id="tr1" runat="server"><td>&nbsp;</td></tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                        <asp:ObjectDataSource ID="odsActionDetail" runat="server" SelectMethod="GetActions" TypeName="Argix.Customers.IssueMgtServiceClient">
                                            <SelectParameters>
                                                <asp:ControlParameter Name="issueID" ControlID="grdIssues" PropertyName="SelectedDataKey.Values[0]" Type="Int64" />
                                                <asp:ControlParameter Name="actionID" ControlID="lsvActions" PropertyName="SelectedDataKey.Values[0]" Type="Int64" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="grdIssues" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="imgActionsRefresh" EventName="Command" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="uprgActions" runat="server" AssociatedUpdatePanelID="upnlActions">
                <ProgressTemplate>
                    Updating issue actions...
                </ProgressTemplate>
            </asp:UpdateProgress>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
</div>
</form>
</body>
</html>
