<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"  CodeFile="ViewIssue.aspx.cs" Inherits="ViewIssue" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="body">
    <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
        <tr>
            <td style="width:25%"><asp:Button ID="btnBack" runat="server" Width="100%" Height="20px" Text="<< Back" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" OnCommand="OnChangeView" CommandName="Back" /></td>
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
                            <asp:QueryStringParameter Name="issueID" QueryStringField="issueID" Type="Int64" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
