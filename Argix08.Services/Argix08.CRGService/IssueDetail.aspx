<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IssueDetail.aspx.cs" Inherits="IssueDetail" StylesheetTheme="Argix" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Issue</title>
    <script type="text/javascript">
        function pageLoad() {
        }
    
    </script>
</head>
<body>
<form id="form1" runat="server">
<div>
<asp:ScriptManager ID="smPage" runat="server" />
<asp:Table ID="tblPage" runat="server" Width="100%" Height="100%" BorderStyle="None" BorderWidth="0px" CellPadding="0" CellSpacing="0">
    <asp:TableRow><asp:TableCell Font-Size="1px" Width="48px">&nbsp;</asp:TableCell><asp:TableCell Font-Size="1px">&nbsp;</asp:TableCell></asp:TableRow>
    <asp:TableRow>
        <asp:TableCell ColumnSpan="2"><asp:Label ID="lblTitle" runat="server" Height="18px" Width="100%" Text="Issue" SkinID="PageTitle"></asp:Label></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow><asp:TableCell ColumnSpan="2" Font-Size="1px" Height="6px">&nbsp;</asp:TableCell></asp:TableRow>
    <asp:TableRow>
        <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top">&nbsp;</asp:TableCell>
        <asp:TableCell>
            <asp:DetailsView ID="dvIssue" runat="server" Width="600px" Height="100%" AutoGenerateRows="False" DataSourceID="odsIssue">
                <Fields>
                    <asp:BoundField DataField="Type" HeaderText="Type" />
                    <asp:BoundField DataField="Subject" HeaderText="Subject" />
                    <asp:BoundField DataField="ContactName" HeaderText="ContactName" />
                    <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" />
                    <asp:BoundField DataField="RegionNumber" HeaderText="RegionNumber" />
                    <asp:BoundField DataField="DistrictNumber" HeaderText="DistrictNumber" />
                    <asp:BoundField DataField="AgentNumber" HeaderText="AgentNumber" />
                    <asp:BoundField DataField="StoreNumber" HeaderText="StoreNumber" />
                    <asp:BoundField DataField="PROID" HeaderText="PROID" />
                    <asp:BoundField DataField="Zone" HeaderText="Zone" />
                </Fields>
            </asp:DetailsView>
            <asp:ObjectDataSource ID="odsIssue" runat="server" SelectMethod="GetIssue" TypeName="Argix.Customers.IssueMgtServiceClient">
                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="issueID" QueryStringField="issueID" Type="Int64" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow><asp:TableCell ColumnSpan="2" Font-Size="1px" Height="6px">&nbsp;</asp:TableCell></asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>&nbsp;</asp:TableCell>
        <asp:TableCell>
            <asp:ListView ID="lsvAction" runat="server" DataSourceID="odsActions">
                <LayoutTemplate>
                    <table id="tblActions" runat="server" border="1" cellpadding="3" cellspacing="0" style="border-style:inset">
                        <tr id="Tr1" runat="server" style="background-color:ButtonFace">
                            <th id="Th1" runat="server">Created</th>
                            <th id="Th2" runat="server">User</th>
                            <th id="Th3" runat="server">Action</th>
                            <th id="Th4" runat="server">Comment</th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr id="Tr2" runat="server">
                        <td valign="top" style="white-space:nowrap"><%# Eval("Created") %></td>
                        <td valign="top" style="white-space:nowrap"><%# Eval("UserID") %></td>
                        <td valign="top" style="white-space:nowrap"><%# Eval("TypeName") %></td>
                        <td valign="top" style="white-space:normal"><%# Eval("Comment") %>&nbsp;</td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <tr id="Tr2" runat="server">
                        <td valign="top">&nbsp;</td>
                        <td valign="top">&nbsp;</td>
                        <td valign="top">&nbsp;</td>
                        <td valign="top">&nbsp;</td>
                    </tr>
                </EmptyDataTemplate>
            </asp:ListView>
            <asp:ObjectDataSource ID="odsActions" runat="server" SelectMethod="GetIssueActions" TypeName="Argix.Customers.IssueMgtServiceClient">
                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="issueID" QueryStringField="issueID" Type="Int64" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow><asp:TableCell ColumnSpan="2" Font-Size="1px" Height="6px">&nbsp;</asp:TableCell></asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>&nbsp;</asp:TableCell>
        <asp:TableCell HorizontalAlign="Right">
            <asp:Button ID="btnClose" runat="server" Text=" Close " ToolTip="Close" Height="20px" Width="96px" UseSubmitBehavior="False" OnClick="OnButtonClick" />
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
</div>
</form>
</body>
</html>
