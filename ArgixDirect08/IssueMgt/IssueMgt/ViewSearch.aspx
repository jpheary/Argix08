<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"  CodeFile="ViewSearch.aspx.cs" Inherits="ViewSearch" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="body">
    <asp:UpdatePanel ID="upnlSearch" runat="server" ChildrenAsTriggers="true" RenderMode="Block" UpdateMode="Conditional" >
    <ContentTemplate>
    <asp:MultiView ID="mvPage" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwSearch" runat="server">
            <table border="0px" cellpadding="1px" cellspacing="0" >
                <tr style="height:24px"><td align="right">Zone&nbsp;</td><td><asp:TextBox ID="txtZone" runat="server" Width="72px" ></asp:TextBox></td></tr>
                <tr style="height:24px"><td align="right">Store&nbsp;</td><td><asp:TextBox ID="txtStore" runat="server" Width="96px"></asp:TextBox></td></tr>
                <tr style="height:24px"><td align="right">Agent&nbsp;</td><td><asp:TextBox ID="txtAgent" runat="server" Width="96px"></asp:TextBox></td></tr>
                <tr style="height:24px"><td align="right">Company&nbsp;</td><td><asp:TextBox ID="txtCompany" runat="server" Width="144px"></asp:TextBox></td></tr>
                <tr style="height:24px"><td align="right">Type&nbsp;</td><td><asp:TextBox ID="txtType" runat="server" Width="96px"></asp:TextBox></td></tr>
                <tr style="height:24px"><td align="right">Action&nbsp;</td><td><asp:TextBox ID="txtAction" runat="server" Width="96px"></asp:TextBox></td></tr>
                <tr style="height:24px"><td align="right">Received&nbsp;</td><td><asp:TextBox ID="txtReceived" runat="server" Width="96px"></asp:TextBox></td></tr>
                <tr style="height:24px"><td align="right">Subject&nbsp;</td><td><asp:TextBox ID="txtSubject" runat="server" Width="144px"></asp:TextBox></td></tr>
                <tr style="height:24px"><td align="right">Contact&nbsp;</td><td><asp:TextBox ID="txtContact" runat="server" Width="144px"></asp:TextBox></td></tr>
                <tr style="height:24px"><td align="right">Originator&nbsp;</td><td><asp:TextBox ID="txtOriginator" runat="server" Width="144px"></asp:TextBox></td></tr>
                <tr style="height:24px"><td align="right">Coordinator&nbsp;</td><td><asp:TextBox ID="txtCoordinator" runat="server" Width="144px"></asp:TextBox></td></tr>
                <tr style="height:24px"><td colspan="2">&nbsp;</td></tr>
                <tr style="height:24px"><td>&nbsp;</td><td><asp:Button ID="btnSearch" runat="server" Width="72px" Height="20px" Text="Search" CommandName="Search" OnCommand="OnSearch" /></td></tr>
            </table>
        </asp:View>
        <asp:View ID="vwIssues" runat="server">
            <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td style="width:25%"><asp:Button ID="btnReset" runat="server" Width="100%" Height="20px" Text="<< Search Again" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" OnCommand="OnChangeView" CommandName="Reset" /></td>
                    <td style="width:75%">&nbsp;</td>
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
                                                <td rowspan="3" style="width:24px"><asp:ImageButton ID="btnIssue" runat="server" ImageUrl="App_Themes/Argix/Images/select.gif" CommandName="Issue" CommandArgument='<%# Eval("ID") %>' OnCommand="OnChangeView" /></td></tr>
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
                            <asp:ObjectDataSource ID="odsIssues" runat="server" TypeName="Argix.Customers.CustomerProxy" SelectMethod="SearchIssuesAdvanced">
                                <SelectParameters>
                                    <asp:Parameter Name="agentNumber" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
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