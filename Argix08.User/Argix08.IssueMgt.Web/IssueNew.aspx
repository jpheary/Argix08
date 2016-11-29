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
    <asp:ScriptManager ID="smPage" runat="server" EnablePartialRendering="true" ScriptMode="Auto" />
    <asp:UpdatePanel ID="upnlNewIssue" runat="server" RenderMode="Block">
    <ContentTemplate>
        <table ID="tblPage" width="672px" height="100%" border="0px" cellpadding="0px" cellspacing="0px">
            <tr style="font-size:1px"><td width="96px">&nbsp;</td><td>&nbsp;</td></tr>
            <tr><td colspan="2" class="WindowTitle"><asp:Label ID="lblTitle" runat="server" Height="18px" Width="100%" Text="New Issue" SkinID="PageTitle"></asp:Label></td></tr>
            <tr style="font-size:1px; height:6px"><td colspan="2">&nbsp;</td></tr>
            <tr>
                <td align="right" valign="top">Company&nbsp;</td>
                <td>
                    <asp:DropDownList ID="cboCompany" runat="server" Width="384px" DataSourceID="odsCompanies" DataTextField="CompanyName" DataValueField="Number" AutoPostBack="true" OnSelectedIndexChanged="OnCompanyChanged"></asp:DropDownList>
                    <asp:ObjectDataSource ID="odsCompanies" runat="server" SelectMethod="GetCompanies" TypeName="Argix.Customers.IssueMgtServiceClient"></asp:ObjectDataSource>
                </td>
            </tr>
            <tr style="font-size:1px; height:6px"><td colspan="2">&nbsp;</td></tr>
            <tr>
                <td align="right" valign="top">Location&nbsp;</td>
                <td>
                    <asp:DropDownList ID="cboScope" runat="server" Width="96px" AutoPostBack="true" OnSelectedIndexChanged="OnScopeChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr style="font-size:1px; height:6px"><td colspan="2">&nbsp;</td></tr>
            <tr>
                <td align="right" valign="top">#&nbsp;</td>
                <td>
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
                            <asp:ObjectDataSource ID="odsAgents" runat="server" SelectMethod="GetAgentsForClient" TypeName="Argix.Customers.CustomerProxy">
                                <SelectParameters>
                                    <asp:ControlParameter Name="clientNumber" ControlID="cboCompany" PropertyName="SelectedValue" Type="String" />
                                    <asp:SessionParameter Name="agentNumber" SessionField="AgentNumber" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </asp:View>
                        <asp:View runat="server" ID="vwStore">
                            <asp:TextBox ID="txtStore" runat="server" Width="72px" BorderStyle="Inset" BorderWidth="2px" TextMode="SingleLine" AutoPostBack="True" OnTextChanged="OnStoreChanged"></asp:TextBox>
                            <asp:TextBox ID="txtStoreDetail" runat="server" Width="100%" Height="192px" BorderStyle="Inset" BorderWidth="2px" TextMode="MultiLine" ReadOnly="true" AutoPostBack="False"></asp:TextBox>                        </asp:View>
                    </asp:MultiView>
                </td>
            </tr>
            <tr style="font-size:1px; height:6px"><td colspan="2">&nbsp;</td></tr>
            <tr>
                <td align="right" valign="top">Type&nbsp;</td>
                <td>
                    <asp:DropDownList ID="cboIssueCategory" runat="server" Width="96px" DataSourceID="odsIssueCategories" DataTextField="Category" DataValueField="Category" AutoPostBack="true" OnSelectedIndexChanged="OnIssueCategoryChanged"></asp:DropDownList>&nbsp;
                    <asp:DropDownList ID="cboIssueType" runat="server" Width="144px" DataSourceID="odsIssueTypes" DataTextField="Type" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="OnIssueTypeChanged"></asp:DropDownList>
                    <asp:ObjectDataSource ID="odsIssueCategories" runat="server" SelectMethod="GetIssueCategorys" TypeName="Argix.Customers.CustomerProxy">
                        <SelectParameters>
                            <asp:SessionParameter Name="agentNumber" SessionField="AgentNumber" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="odsIssueTypes" runat="server" SelectMethod="GetIssueTypes" TypeName="Argix.Customers.IssueMgtServiceClient">
                        <SelectParameters>
                            <asp:ControlParameter Name="issueCategory" ControlID="cboIssueCategory" PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
            </td>
            </tr>
            <tr style="font-size:1px; height:6px"><td colspan="2">&nbsp;</td></tr>
            <tr>
                <td align="right" valign="top">Contact&nbsp;</td>
                <td>
                    <asp:DropDownList ID="cboContact" runat="server" Width="288px" DataSourceID="odsContacts" DataTextField="FullName" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="OnContactChanged"></asp:DropDownList>
                    <asp:ObjectDataSource ID="odsContacts" runat="server" SelectMethod="GetContacts" TypeName="Argix.Customers.IssueMgtServiceClient"></asp:ObjectDataSource>
                </td>
            </tr>
            <tr style="font-size:1px; height:6px"><td colspan="2">&nbsp;</td></tr>
            <tr>
                <td align="right" valign="top">Subject&nbsp;</td>
                <td><asp:TextBox ID="txtSubject" runat="server" Width="100%" BorderStyle="Inset" BorderWidth="2px" TextMode="SingleLine" AutoPostBack="True" OnTextChanged="OnSubjectChanged"></asp:TextBox></td>
            </tr>
            <tr style="font-size:1px; height:24px"><td colspan="2">&nbsp;</td></tr>
            <tr>
                <td>&nbsp;</td>
                <td align="right">
                    <asp:Button ID="btnOk" runat="server" Text="   OK   " ToolTip="Create new issue" Height="20px" Width="96px" UseSubmitBehavior="False" CommandName="OK" OnCommand="OnCommandClick" />
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text=" Cancel " ToolTip="Cancel new issue" Height="20px" Width="96px" UseSubmitBehavior="False" CommandName="Cancel" OnCommand="OnCommandClick" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="uprgNewIssue" runat="server" AssociatedUpdatePanelID="upnlNewIssue"><ProgressTemplate>updating...</ProgressTemplate></asp:UpdateProgress>
</div>
</form>
</body>
</html>
