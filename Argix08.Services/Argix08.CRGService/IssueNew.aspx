<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IssueNew.aspx.cs" Inherits="IssueNew" StylesheetTheme="Argix" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Issue</title>
    <script type="text/javascript">
      function pageLoad() { }
    </script>
</head>
<body class="dialogbody">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="smPage" runat="server" EnablePartialRendering="true" ScriptMode="Debug">
            <Services>
                <asp:ServiceReference Path="~/IssueMgtServiceClient.svc" />
                <asp:ServiceReference Path="~/IssueMgtServiceClient.svc" />
            </Services>
        </asp:ScriptManager>
        <asp:UpdatePanel ID="upnlNewIssue" runat="server" RenderMode="Block">
        <ContentTemplate>
        <asp:Table ID="tblPage" runat="server" Width="672px" Height="100%" BorderStyle="None" BorderWidth="0px" CellPadding="0" CellSpacing="0">
            <asp:TableRow><asp:TableCell Font-Size="1px" Width="96px">&nbsp;</asp:TableCell><asp:TableCell Font-Size="1px">&nbsp;</asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2"><asp:Label ID="lblTitle" runat="server" Height="18px" Width="100%" Text="New Issue" SkinID="PageTitle"></asp:Label></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow><asp:TableCell ColumnSpan="2" Font-Size="1px" Height="6px">&nbsp;</asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top">Company&nbsp;</asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="cboCompany" runat="server" Width="384px" DataSourceID="odsCompanies" DataTextField="CompanyName" DataValueField="Number" AutoPostBack="true" OnSelectedIndexChanged="OnCompanyChanged"></asp:DropDownList>
                    <asp:ObjectDataSource ID="odsCompanies" runat="server" SelectMethod="GetCompanies" TypeName="Argix.Customers.IssueMgtServiceClient"></asp:ObjectDataSource>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow><asp:TableCell ColumnSpan="2" Font-Size="1px" Height="6px">&nbsp;</asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top">Location&nbsp;</asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="cboScope" runat="server" Width="96px" AutoPostBack="true" OnSelectedIndexChanged="OnScopeChanged"></asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow><asp:TableCell ColumnSpan="2" Font-Size="1px" Height="6px">&nbsp;</asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top">#&nbsp;</asp:TableCell>
                <asp:TableCell>
                    <asp:MultiView ID="mvLocation" runat="server" ActiveViewIndex="0">
                        <asp:View runat="server" ID="vwOther">
                            <asp:DropDownList ID="cboLocation" runat="server" Width="384px" AutoPostBack="true" OnSelectedIndexChanged="OnLocationChanged"></asp:DropDownList>
                            <asp:ObjectDataSource ID="odsDistricts" runat="server" SelectMethod="GetDistricts" TypeName="Argix.Customers.IssueMgtServiceClient">
                                <SelectParameters>
                                    <asp:ControlParameter Name="clientNumber" ControlID="cboCompany" PropertyName="SelectedValue" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="odsRegions" runat="server" SelectMethod="GetRegions" TypeName="Argix.Customers.IssueMgtServiceClient">
                                <SelectParameters>
                                    <asp:ControlParameter Name="clientNumber" ControlID="cboCompany" PropertyName="SelectedValue" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="odsAgents" runat="server" SelectMethod="GetAgentsForClient" TypeName="Argix.Customers.IssueMgtServiceClient">
                                <SelectParameters>
                                    <asp:ControlParameter Name="clientNumber" ControlID="cboCompany" PropertyName="SelectedValue" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </asp:View>
                        <asp:View runat="server" ID="vwStore">
                            <asp:TextBox ID="txtStore" runat="server" Width="72px" BorderStyle="Inset" BorderWidth="2px" TextMode="SingleLine" AutoPostBack="True" OnTextChanged="OnStoreChanged"></asp:TextBox>
                        </asp:View>
                    </asp:MultiView>
    
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow><asp:TableCell ColumnSpan="2" Font-Size="1px" Height="6px">&nbsp;</asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top">Store&nbsp;</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="txtStoreDetail" runat="server" Width="100%" Height="192px" BorderStyle="Inset" BorderWidth="2px" TextMode="MultiLine" ReadOnly="true" AutoPostBack="False"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow><asp:TableCell ColumnSpan="2" Font-Size="1px" Height="6px">&nbsp;</asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top">Type&nbsp;</asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="cboIssueCategory" runat="server" Width="96px" DataSourceID="odsIssueCategories" DataTextField="Category" DataValueField="Category" AutoPostBack="true" OnSelectedIndexChanged="OnIssueCategoryChanged"></asp:DropDownList>&nbsp;
                    <asp:DropDownList ID="cboIssueType" runat="server" Width="144px" DataSourceID="odsIssueTypes" DataTextField="Type" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="OnIssueTypeChanged"></asp:DropDownList>
                    <asp:ObjectDataSource ID="odsIssueCategories" runat="server" SelectMethod="GetIssueCategorys" TypeName="Argix.Customers.IssueMgtServiceClient"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="odsIssueTypes" runat="server" SelectMethod="GetIssueTypes" TypeName="Argix.Customers.IssueMgtServiceClient">
                        <SelectParameters>
                            <asp:ControlParameter Name="issueCategory" ControlID="cboIssueCategory" PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
            </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow><asp:TableCell ColumnSpan="2" Font-Size="1px" Height="6px">&nbsp;</asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top">Contact&nbsp;</asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="cboContact" runat="server" Width="288px" DataSourceID="odsContacts" DataTextField="FullName" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="OnContactChanged"></asp:DropDownList>
                    <asp:ObjectDataSource ID="odsContacts" runat="server" SelectMethod="GetContacts" TypeName="Argix.Customers.IssueMgtServiceClient"></asp:ObjectDataSource>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow><asp:TableCell ColumnSpan="2" Font-Size="1px" Height="6px">&nbsp;</asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top">Subject&nbsp;</asp:TableCell>
                <asp:TableCell><asp:TextBox ID="txtSubject" runat="server" Width="100%" BorderStyle="Inset" BorderWidth="2px" TextMode="SingleLine" AutoPostBack="True" OnTextChanged="OnSubjectChanged"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow><asp:TableCell ColumnSpan="2" Font-Size="1px" Height="6px">&nbsp;</asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>&nbsp;</asp:TableCell>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Button ID="btnOk" runat="server" Text="   OK   " ToolTip="Create new issue" Height="20px" Width="96px" UseSubmitBehavior="False" OnClick="OnButtonClick" />
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text=" Cancel " ToolTip="Cancel new issue" Height="20px" Width="96px" UseSubmitBehavior="False" OnClick="OnButtonClick" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="uprgNewIssue" runat="server" AssociatedUpdatePanelID="upnlNewIssue">
        <ProgressTemplate>
            Updating...
        </ProgressTemplate>
    </asp:UpdateProgress>
    </div>
    </form>
</body>
</html>
