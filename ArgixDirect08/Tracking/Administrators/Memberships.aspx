<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Memberships.aspx.cs" Inherits="Memberships" Title="Memberships" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cpHead" Runat="Server">
    <asp:LinkButton id="lnkTracking" runat="server" PostBackUrl="~/Members/Default.aspx" SkinID="lnkNav">Home...</asp:LinkButton>
</asp:Content>

<asp:Content ID="cBody" ContentPlaceHolderID="cpBody" Runat="Server">
<table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
    <tr><td><div class="PanelLine">&nbsp;</div></td></tr>
    <tr><td><div class="PageTitle">Manage Membership</div></td></tr>
    <tr><td>&nbsp;</td></tr>
    <tr>
        <td>
            <table width="100%" border="0px" cellpadding="0px" cellspacing="2px">
                <tr height="16px" class="GridTitle">
                    <td>&nbsp;Membership</td>
                    <td align="right" valign="middle">
                        <asp:TextBox ID="txtFindUser" runat="server" Width="144px" BorderStyle="Inset" BorderWidth="1px" ToolTip="Search for a user... <press Enter>" AutoPostBack="True" OnTextChanged="OnSearchUser"></asp:TextBox>
                        <asp:ImageButton ID="imgFindUser" runat="server" Height="16px" ImageAlign="Middle" BorderStyle="None" ImageUrl="~/App_Themes/ArgixDirect/Images/search.gif" ToolTip="Search for a user..." />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" valign="top">
                        <asp:Panel id="pnlMembers" runat="server" Width="864px" Height="288px" BorderStyle="none" ScrollBars="Auto">
                            <asp:UpdatePanel ID="upnlMembers" runat="server" UpdateMode="Conditional" >
                            <ContentTemplate>
                                <asp:GridView ID="grdMembers" runat="server" Width="100%" DataSourceID="odsMembers" DataKeyNames="UserName" AutoGenerateColumns="False" AllowSorting="True" OnSelectedIndexChanged="OnMemberSelected" >
                                    <Columns>
                                        <asp:CommandField HeaderStyle-Width="16px" ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/App_Themes/ArgixDirect/Images/select.gif" />
                                        <asp:BoundField DataField="UserName" HeaderText="User" HeaderStyle-Width="96px" ItemStyle-Wrap="false" SortExpression="UserName ASC" />
                                        <asp:BoundField DataField="UserFullName" HeaderText="Name" HeaderStyle-Width="144px" ItemStyle-Wrap="false" SortExpression="UserFullName ASC" />
                                        <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-Width="144px" ItemStyle-Wrap="false" SortExpression="Email ASC" Visible="true" />
                                        <asp:BoundField DataField="Company" HeaderText="Company" HeaderStyle-Width="144px" ItemStyle-Wrap="false" SortExpression="Company ASC" />
                                        <asp:BoundField DataField="IsLockedOut" HeaderText="Locked" HeaderStyle-Width="48px" SortExpression="IsLockedOut ASC" />
                                        <asp:BoundField DataField="LastLoginDate" HeaderText="Logon" HeaderStyle-Width="96px" DataFormatString="{0:MMddyyyy}" SortExpression="LastLoginDate ASC" HtmlEncode="False" />
                                    </Columns>
                                </asp:GridView>
                                <asp:ObjectDataSource ID="odsMembers" runat="server" TypeName="MembershipServices" SelectMethod="GetMembers" DeleteMethod="DeleteUser" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="600" CacheKeyDependency="Memberships">
                                </asp:ObjectDataSource>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="txtFindUser" />
                            </Triggers>
                            </asp:UpdatePanel>
                            <asp:UpdateProgress ID="upgMembers" runat="server" AssociatedUpdatePanelID="upnlMembers"><ProgressTemplate>updating...</ProgressTemplate></asp:UpdateProgress>
                        </asp:Panel>
                    </td>
                </tr>
                <tr><td colspan="2">&nbsp;</td></tr>
                <tr>
                   <td colspan="2" align="right">
                        <asp:UpdatePanel ID="upnlCommand" runat="server" UpdateMode="Always" >
                            <ContentTemplate>
                                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" ToolTip="Refresh membership cache" Width="72px" Height="20px" Visible="true" UseSubmitBehavior="False" CommandName="Refresh" OnCommand="OnCommand" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnDelete" runat="server" Text="Delete" ToolTip="Delete selected mebership user" Width="72px" Height="20px" UseSubmitBehavior="true" OnClientClick="return confirm('Are you sure you want to delete this user?');" CommandName="Delete" OnCommand="OnCommand" />
                                &nbsp;&nbsp;<asp:Button ID="btnUpdate" runat="server" Text="Edit" ToolTip="Edit selected membership user" Width="72px" Height="20px" UseSubmitBehavior="False" CommandName="Edit" OnCommand="OnCommand" />
                                &nbsp;&nbsp;<asp:Button ID="btnAdd" runat="server" Text="Add" ToolTip="Add a new membership user" Width="72px" Height="20px" UseSubmitBehavior="False" CommandName="Add" OnCommand="OnCommand" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnRefresh" />
                                <asp:PostBackTrigger ControlID="btnDelete" />
                                <asp:PostBackTrigger ControlID="btnUpdate" />
                                <asp:PostBackTrigger ControlID="btnAdd" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<script type="text/javascript" language="javascript">
    function scroll(username) {
        var grd = document.getElementById('ctl00_cpBody_grdMembers');
        for(var i=1; i<grd.rows.length; i++) {
            var cell = grd.rows[i].cells[1];
            if(cell.innerHTML.substr(0, username.length) == username) {
                var pnl = document.getElementById('ctl00_cpBody_pnlMembers');
                pnl.scrollTop = i * (grd.clientHeight / grd.rows.length);
                break;
            }
        }
    }
</script>
</asp:Content>
