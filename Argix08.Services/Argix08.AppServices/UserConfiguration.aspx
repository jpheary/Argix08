<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserConfiguration.aspx.cs" Inherits="UserConfiguration" StylesheetTheme="Argix" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Configuration</title>
    <script type="text/javascript" language="jscript">
        function pageLoad() {
        }
    </script>
</head>
<body>
<form id="form1" runat="server">
<div>
<asp:ScriptManager ID="smPage" runat="server" EnablePartialRendering="true" ScriptMode="Auto">
</asp:ScriptManager>
<script type="text/javascript" language="jscript">
    var scrollTop;
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(OnBeginRequest);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(OnEndRequest);
    function OnBeginRequest(sender, args) {
        scrollTop = document.getElementById('pnlConfig').scrollTop;
    }
    function OnEndRequest(sender, args) {
        document.getElementById('pnlConfig').scrollTop = scrollTop;
    }
</script>
<asp:UpdatePanel ID="upnlConfig" runat="server" RenderMode="Block" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>
    <asp:Table ID="tblPage" runat="server" Height="100%" Width="100%" BorderStyle="None" BorderWidth="0" CellPadding="0" CellSpacing="0">
        <asp:TableRow><asp:TableCell Width="96px" Font-Size="1px">&nbsp;</asp:TableCell><asp:TableCell Width="600px" Font-Size="1px">&nbsp;</asp:TableCell><asp:TableCell Font-Size="1px">&nbsp;</asp:TableCell></asp:TableRow>
        <asp:TableRow Height="18px" BackColor="Highlight" ForeColor="HighlightText"><asp:TableCell ColumnSpan="3">Configuration from <% =mTerminal %></asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell ColumnSpan="3" Font-Size="1px" Height="6px">&nbsp;</asp:TableCell></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">Application&nbsp;</asp:TableCell>
            <asp:TableCell><asp:Label ID="lblApp" runat="server" Width="192px" Text=""></asp:Label></asp:TableCell>
            <asp:TableCell>&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow><asp:TableCell ColumnSpan="3" Font-Size="1px" Height="6px">&nbsp;</asp:TableCell></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Right">User&nbsp;</asp:TableCell>
            <asp:TableCell><asp:Label ID="lblUser" runat="server" Width="192px" Text=""></asp:Label></asp:TableCell>
            <asp:TableCell>&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow><asp:TableCell ColumnSpan="3" Font-Size="1px" Height="6px">&nbsp;</asp:TableCell></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>&nbsp;</asp:TableCell>
            <asp:TableCell>
                <asp:Panel ID="pnlConfig" runat="server" Width="800px" Height="400px" ScrollBars="Auto">
                    <asp:GridView ID="grdConfig" runat="server" DataSourceID="odsConfig" DataKeyNames="Application,UserName,Key" AutoGenerateColumns="False" AllowSorting="false" OnSelectedIndexChanged="OnConfigurationEntrySelected" OnRowEditing="OnConfigurationEntryEditing" OnRowDataBound="OnConfigurationRowDataBound" OnRowCommand="OnConfigurationRowCommand" >
                        <Columns>
                            <asp:CommandField ButtonType="Image" HeaderStyle-Width="16px" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" ShowSelectButton="True" FooterText="  *" />
                            <asp:TemplateField HeaderText="Application" HeaderStyle-Width="120px" >
                                <ItemTemplate><%# Eval("Application")%></ItemTemplate>
                                <EditItemTemplate><asp:Label ID="lblApplication" runat="server" Text='<%# Bind("Application")%>'></asp:Label></EditItemTemplate>
                                <FooterTemplate><asp:Label ID="lblApplication" runat="server" Text='<%= this.cboApp.SelectedValue%>'></asp:Label></FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UserName" HeaderStyle-Width="96px" SortExpression="UserName" >
                                <ItemTemplate><%# Eval("UserName")%></ItemTemplate>
                                <EditItemTemplate><asp:Label ID="lblUserName" runat="server" Text='<%# Bind("UserName")%>'></asp:Label></EditItemTemplate>
                                <FooterTemplate><asp:Label ID="lblUserName" runat="server" Text='<%= this.cboUser.SelectedValue%>'></asp:Label></FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Key" HeaderStyle-Width="120px" SortExpression="Key" >
                                <ItemTemplate><%# Eval("Key")%></ItemTemplate>
                                <EditItemTemplate><asp:Label ID="lblKey" runat="server" Text='<%# Bind("Key")%>'></asp:Label></EditItemTemplate>
                                <FooterTemplate><asp:TextBox ID="txtKey" runat="server" Width="120px" BorderStyle="None" Text=""></asp:TextBox></FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Value" HeaderStyle-Width="144px">
                                <ItemTemplate><%# Eval("Value") %></ItemTemplate>
                                <EditItemTemplate><asp:TextBox ID="txtValue" runat="Server" BorderStyle="None" Width="144px" Text='<%# Bind("Value") %>'></asp:TextBox></EditItemTemplate>
                                <FooterTemplate><asp:TextBox ID="txtValue" runat="Server" BorderStyle="None" Width="144px" Text=""></asp:TextBox></FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Security" HeaderStyle-Width="72px">
                                <ItemTemplate><%# Eval("Security")%></ItemTemplate>
                                <EditItemTemplate><asp:Label ID="lblSecurity" runat="server" Text='<%# Bind("Security")%>'></asp:Label></EditItemTemplate>
                                <FooterTemplate><asp:Label ID="lblSecurity" runat="server" Text="1"></asp:Label></FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <FooterTemplate><asp:LinkButton ID="lnkInsert" runat="server" ForeColor="HighlightText" CommandName="Insert" Text="Insert" />&nbsp;<asp:LinkButton ID="lnkCancel" runat="server" ForeColor="HighlightText" CommandName="Cancel" Text="Cancel" /></FooterTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Link" HeaderStyle-Width="24px" ShowEditButton="true" />
                        </Columns>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="odsConfig" runat="server" ConflictDetection="OverwriteChanges" TypeName="Argix.AppService" SelectMethod="GetConfiguration" InsertMethod="CreateConfigurationEntry" UpdateMethod="UpdateConfigurationEntry">
                        <SelectParameters>
                            <asp:ControlParameter Name="application" ControlID="lblApp" PropertyName="Text" Type="String" />
                            <asp:ControlParameter Name="username" ControlID="lblUser" PropertyName="Text" Type="String" />
                        </SelectParameters>
                        <InsertParameters>
                            <asp:Parameter Name="application" Type="String" />
                            <asp:Parameter Name="username" Type="String" />
                            <asp:Parameter Name="key" Type="String" />
                            <asp:Parameter Name="value" Type="String" />
                            <asp:Parameter Name="security" Type="String" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="application" Type="String" />
                            <asp:Parameter Name="username" Type="String" />
                            <asp:Parameter Name="key" Type="String" />
                            <asp:Parameter Name="value" Type="String" />
                            <asp:Parameter Name="security" Type="String" />
                        </UpdateParameters>
                    </asp:ObjectDataSource>
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell>&nbsp;</asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    </ContentTemplate>
</asp:UpdatePanel>
</div>
Copyright 2010 Argix Direct, Inc. v3.5.0.082410
</form>
</body>
</html>
