<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppLoggerEditor.aspx.cs" Inherits="AppLoggerEditor" StylesheetTheme="Argix" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Application Log Editor</title>
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
</asp:ScriptManager>
<script type="text/javascript" language="jscript">
    var scrollTop;
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(OnBeginRequest);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(OnEndRequest);
    function OnBeginRequest(sender, args) {
        scrollTop = document.getElementById('pnlLog').scrollTop;
    }
    function OnEndRequest(sender, args) {
        document.getElementById('pnlLog').scrollTop = scrollTop;
    }
</script>
<asp:UpdatePanel ID="upnlLog" runat="server" RenderMode="Block" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>
        <asp:Table ID="tblPage" runat="server" Height="100%" Width="100%" BorderStyle="None" BorderWidth="2" CellPadding="0" CellSpacing="0">
            <asp:TableRow><asp:TableCell Width="96px" Font-Size="1px">&nbsp;</asp:TableCell><asp:TableCell Width="884px" Font-Size="1px">&nbsp;</asp:TableCell><asp:TableCell Font-Size="1px">&nbsp;</asp:TableCell></asp:TableRow>
            <asp:TableRow Height="18px" BackColor="Highlight" ForeColor="HighlightText"><asp:TableCell ColumnSpan="3">Application Log from <% =mTerminal %></asp:TableCell></asp:TableRow>
            <asp:TableRow><asp:TableCell ColumnSpan="3" Font-Size="1px" Height="6px">&nbsp;</asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right">Name&nbsp;</asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="cboLog" runat="server" Width="192px" DataSourceID="odsLogs" DataValueField="Value" DataTextField="Key" AppendDataBoundItems= OnSelectedIndexChanged="OnLogNameChanged" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="odsLogs" runat="server" TypeName="Argix.AppService" SelectMethod="GetLogNames" EnableCaching="false" CacheDuration="600" />
                </asp:TableCell>
                <asp:TableCell>&nbsp;</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow><asp:TableCell ColumnSpan="3" Font-Size="1px" Height="6px">&nbsp;</asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right">Source&nbsp;</asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="cboSource" runat="server" Width="192px" DataSourceID="odsSrcs" DataValueField="Value" DataTextField="Key" OnSelectedIndexChanged="OnLogSourceChanged" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="odsSrcs" runat="server" TypeName="Argix.AppService" SelectMethod="GetLogSources">
                        <SelectParameters>
                            <asp:ControlParameter Name="logName" ControlID="cboLog" PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:TableCell>
                <asp:TableCell>&nbsp;</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow><asp:TableCell ColumnSpan="3">&nbsp;</asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>&nbsp;</asp:TableCell>
                <asp:TableCell>
                    <asp:Panel ID="pnlLog" runat="server" Width="884px" Height="400px" ScrollBars="Auto">
                        <asp:GridView ID="grdLog" runat="server" DataSourceID="odsAppLog" DataKeyNames="ID" AutoGenerateColumns="False" AllowSorting="true" OnSelectedIndexChanged="OnLogEntrySelected">
                            <Columns>
                                <asp:CommandField ButtonType="Image" HeaderStyle-Width="16px" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" ShowSelectButton="True" />
                                <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-Width="48px" Visible="false" />
                                <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-Width="96px" />
                                <asp:BoundField DataField="Source" HeaderText="Source" HeaderStyle-Width="96px" />
                                <asp:BoundField DataField="Date" HeaderText="Date" HeaderStyle-Width="120px" HtmlEncode="true" DataFormatString="{0}" />
                                <asp:BoundField DataField="Category" HeaderText="Category" HeaderStyle-Width="72px" Visible="false" />
                                <asp:BoundField DataField="Event" HeaderText="Event" HeaderStyle-Width="72px" Visible="false" />
                                <asp:BoundField DataField="User" HeaderText="User" HeaderStyle-Width="72px" />
                                <asp:BoundField DataField="Computer" HeaderText="Computer" HeaderStyle-Width="72px" />
                                <asp:BoundField DataField="LogLevel" HeaderText="Level" HeaderStyle-Width="48px" />
                                <asp:BoundField DataField="Keyword1" HeaderText="Keyword1" HeaderStyle-Width="72px" Visible="false" />
                                <asp:BoundField DataField="Keyword2" HeaderText="Keyword2" HeaderStyle-Width="72px" Visible="false" />
                                <asp:BoundField DataField="Keyword3" HeaderText="Keyword3" HeaderStyle-Width="72px" Visible="false" />
                                <asp:BoundField DataField="Message" HeaderText="Message" HeaderStyle-Width="384px" ItemStyle-Width="384px" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                    <asp:ObjectDataSource ID="odsAppLog" runat="server" TypeName="Argix.AppService" SelectMethod="GetAppLog">
                        <SelectParameters>
                            <asp:ControlParameter Name="logName" ControlID="cboLog" PropertyName="SelectedValue" Type="String" />
                            <asp:ControlParameter Name="source" ControlID="cboSource" PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:TableCell>
                <asp:TableCell>&nbsp;</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow><asp:TableCell ColumnSpan="3">&nbsp;</asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>&nbsp;</asp:TableCell>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" ToolTip="Delete selected entry" Width="96px" Height="20px" UseSubmitBehavior="True" OnClientClick="return confirm('Are you sure you want to delete this user?');" OnClick="OnButtonClick" />
                </asp:TableCell>
                <asp:TableCell>&nbsp;</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="uprgLog" runat="server" AssociatedUpdatePanelID="upnlLog">
    <ProgressTemplate>
        Updating log...
    </ProgressTemplate>
</asp:UpdateProgress>
</div>
Copyright 2010 Argix Direct, Inc. v3.5.0.082410
</form>
</body>
</html>
