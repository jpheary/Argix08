<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"  CodeFile="ViewIssues.aspx.cs" Inherits="ViewIssues" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="body">
    <asp:UpdatePanel ID="upnlIssues" runat="server" ChildrenAsTriggers="true" RenderMode="Block" UpdateMode="Conditional" >
    <ContentTemplate>
    <asp:MultiView ID="mvPage" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwIssues" runat="server">
            <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td align="right">Terminal&nbsp;</td>
                    <td colspan="3">
                       <asp:DropDownList ID="cboTerminal" runat="server" Width="144px" DataSourceID="odsTerminals" DatatextField="Description" DataValueField="AgentID" ToolTip="Local Terminals" AutoPostBack="True" OnSelectedIndexChanged="OnTerminalChanged"></asp:DropDownList>
                        <asp:ObjectDataSource ID="odsTerminals" runat="server" SelectMethod="GetTerminals" TypeName="Argix.Customers.CustomerProxy" CacheExpirationPolicy="Absolute" CacheDuration="900" EnableCaching="true" >
                            <SelectParameters>
                                <asp:SessionParameter Name="agentNumber" SessionField="AgentNumber" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        &nbsp;&nbsp;&nbsp;<asp:ImageButton ID="imgRefresh" runat="server" Height="16px" ImageUrl="~/App_Themes/Argix/Images/refresh.png" AlternateText="Refresh" OnClick="OnRefresh" />
                    </td>
                </tr>
            </table>
            <br />
            <table width="100%" border="0px" cellpadding="0px" cellspacing="0px" style="background-color:White">
                <tr>
                    <td colspan="4" valign="top" align="center">
                        <asp:Panel id="pnlIssues" runat="server" Width="100%" Height="280px" BorderStyle="None" BorderWidth="1px" ScrollBars="Vertical">
                            <asp:ListView ID="lsvIssues" runat="server" DataSourceID="odsIssues">
                                <LayoutTemplate>
                                    <div id="itemPlaceholder" runat="server" style="width:100%" ></div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                        <table border="0" cellpadding="3" cellspacing="3" style="width:100%; background-color:White">
                                            <tr><td style="width:120px; text-align:left; font-weight:bold"><%# Eval("LastActionUserID")%></td><td style="text-align:right;"><%# GetDateInfo(Eval("LastActionCreated"))%></td>
                                                <td rowspan="3" style="width:24px"><asp:ImageButton runat="server" ImageUrl="App_Themes/Argix/Images/select.gif" CommandName="Issue" CommandArgument='<%# Eval("ID") %>' OnCommand="OnChangeView" /></td></tr>
                                            <tr><td style="text-align:left;"><%# Eval("Type") %></td><td style="text-align:right;"><%# Eval("LastActionDescription")%></td></tr>
                                            <tr><td colspan="2" style="text-align:left;"><%# GetCompanyInfo(Eval("CompanyName"),Eval("StoreNumber"),Eval("AgentNumber"))%></td></tr>
                                            <tr><td colspan="3"><hr /></td></tr>
                                        </table>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <table border="0" cellpadding="3" cellspacing="0" style="width:100%; background-color:White">
                                        <tr style="background-color:white; height:48px"><td>&nbsp;</td></tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:ListView>
                            <asp:ObjectDataSource ID="odsIssues" runat="server" TypeName="Argix.Customers.CustomerProxy" SelectMethod="GetIssues">
                                <SelectParameters>
                                    <asp:ControlParameter Name="agentNumber" ControlID="cboTerminal" PropertyName="SelectedValue" DefaultValue="" ConvertEmptyStringToNull="true" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwIssue" runat="server">
            <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td style="width:25%"><asp:Button ID="btnIssues" runat="server" Width="100%" Height="20px" Text="<< Back" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" OnCommand="OnChangeView" CommandName="Issues" /></td>
                    <td style="width:75%">&nbsp;</td>
                </tr>
            </table>
            <br />
            <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
                <tr><td><asp:Label ID="lblType" runat="server" Width="100%" Height="18px" Text=""></asp:Label></td></tr>
                <tr><td><asp:Label ID="lblCompany" runat="server" Width="100%" Height="18px" Text=""></asp:Label></td></tr>
                <tr><td><asp:Label ID="lblSubject" runat="server" Width="100%" Height="18px" Text=""></asp:Label></td></tr>
                <tr>
                    <td>
                        <asp:Panel id="pnlIssue" runat="server" Width="100%" Height="230px" BorderStyle="None" BorderWidth="1px" ScrollBars="Vertical">
                            <asp:ListView ID="lsvAction" runat="server"  DataSourceID="odsActions">
                                <LayoutTemplate>
                                    <div id="itemPlaceholder" style="width:100%" runat="server" ></div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <table border="0" cellpadding="3" cellspacing="0" style="width:100%; background-color:White">
                                        <tr><td style="width:100%; text-align:left; font-weight:bold"><%# Eval("Created") %>&nbsp;&nbsp;&nbsp;<%# Eval("UserID") %></td></tr>
                                        <tr><td style="width:100%; text-align:left; font-weight:bold"><%# Eval("TypeName") %></td></tr>
                                        <tr><td>&nbsp;</td></tr>
                                        <tr><td style="text-align:left; white-space:normal"><%# Eval("Comment") %></td></tr>
                                        <tr><td><hr /></td></tr>
                                    </table>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <table border="0" cellpadding="3" cellspacing="0" style="width:100%; background-color:White">
                                        <tr style="background-color:white; height:192px"><td>&nbsp;</td></tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:ListView>
                            <asp:ObjectDataSource ID="odsActions" runat="server" SelectMethod="GetIssueActions" TypeName="Argix.Customers.CustomerProxy">
                                <SelectParameters>
                                    <asp:Parameter Name="issueID" DefaultValue="" ConvertEmptyStringToNull="true" Type="Int64" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>