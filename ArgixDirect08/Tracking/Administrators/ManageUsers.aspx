<%@ page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="ManageUsers.aspx.cs" inherits="ManageUsers" title="Manage Users" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cHead" runat="server" ContentPlaceHolderID="cpHead">
    <asp:LinkButton id="lnkTracking" runat="server" PostBackUrl="~/Members/Default.aspx" SkinID="lnkNav">Home...</asp:LinkButton>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cpBody" Runat="Server">
<table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
    <tr><td><div class="PanelLine">&nbsp;</div></td></tr>
    <tr><td><div class="PageTitle">Manage Users</div></td></tr>
    <tr><td>&nbsp;</td></tr>
    <tr>
        <td>
            <table width="100%" border="0px" cellpadding="0px" cellspacing="3px">
                <tr height="18px" class="GridTitle"><td colspan="2">&nbsp;<%= Membership.Provider.ApplicationName %> Membership</td></tr>
                <tr>
                    <td colspan="2" valign="top">
                        <asp:Panel id="pnlUsers" runat="server" Width="100%" Height="332px" BorderStyle="None" ScrollBars="Auto">
                            <asp:UpdatePanel ID="upnlUsers" runat="server" UpdateMode="Conditional" >
                            <ContentTemplate>
                                <asp:GridView ID="grdUsers" runat="server" Width="100%" Height="100%" DataSourceID="odsUsers" DataKeyNames="UserID" AutoGenerateColumns="False" AllowPaging="false" AllowSorting="True" OnSelectedIndexChanged="OnUserChanged" OnRowEditing="OnGridRowEditing" OnRowCancelingEdit="OnGridRowCancelingEdit" OnRowUpdating="OnGridRowUpdating" OnRowUpdated="OnGridRowUpdated" OnRowDeleting="OnGridRowDeleting">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" HeaderStyle-Width="16px" ShowSelectButton="True" SelectImageUrl="~/App_Themes/ArgixDirect/Images/select.gif" />
                                        <asp:BoundField DataField="UserId" HeaderText="User ID" ItemStyle-Wrap="False" ReadOnly="True" SortExpression="UserID" />
                                        <asp:BoundField DataField="UserFullName" HeaderText="Full Name" HeaderStyle-Width="96px" ItemStyle-Wrap="False" SortExpression="UserFullName" />
                                        <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-Width="96px" SortExpression="Email" />
                                        <asp:BoundField DataField="CompanyID" HeaderText="CompanyID" HeaderStyle-Width="72px" SortExpression="CompanyID" Visible="false" />
                                        <asp:BoundField DataField="Company" HeaderText="Company" HeaderStyle-Width="96px" SortExpression="Company" Visible="false" />
                                        <asp:BoundField DataField="Type" HeaderText="Type" HeaderStyle-Width="72px" ReadOnly="True" SortExpression="Type" />
                                        <asp:TemplateField HeaderText="Customer" HeaderStyle-Width="96px" ItemStyle-Wrap="false" SortExpression="Company">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompany" runat="server" Text='<%# Eval("Company") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="cboCustomers" runat="server" DataSourceID="odsCustomers" DataTextField="CompanyName" DataValueField="ClientID" SelectedValue='<%# Bind("CompanyID") %>' AutoPostBack="True" OnDataBinding="OnCustomersDataBinding"></asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowEditButton="True" />
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" Text="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this user?');"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:ObjectDataSource ID="odsUsers" runat="server" TypeName="MembershipServices" SelectMethod="GetTrackingUsers" UpdateMethod="UpdateUser" DeleteMethod="DeleteUser" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="600" CacheKeyDependency="Users">
                                    <UpdateParameters>
                                        <asp:ControlParameter Name="userID" ControlID="grdUsers" PropertyName="SelectedValue" Type="String" />
                                        <asp:ControlParameter Name="userFullName" ControlID="grdUsers" PropertyName="SelectedValue" Type="String" />
                                        <asp:ControlParameter Name="email" ControlID="grdUsers" PropertyName="SelectedValue" Type="String" />
                                        <asp:Parameter Name="company" Type="String" />
                                        <asp:ControlParameter Name="companyID" ControlID="grdUsers" PropertyName="SelectedValue" Type="String" />
                                    </UpdateParameters>
                                    <DeleteParameters>
                                        <asp:ControlParameter Name="userID" ControlID="grdUsers" PropertyName="SelectedDataKey.Values[0]" Type="String" />
                                    </DeleteParameters>
                               </asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsCustomers" runat="server" TypeName="TrackingServices" SelectMethod="GetCustomers" EnableCaching="true" CacheExpirationPolicy="Absolute" CacheDuration="1800" >
                                    <SelectParameters>
                                        <asp:Parameter Name="companyType" DefaultValue="client" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdateProgress ID="upgUsers" runat="server" AssociatedUpdatePanelID="upnlUsers"><ProgressTemplate>updating...</ProgressTemplate></asp:UpdateProgress>
                        </asp:Panel>
                    </td>
                </tr>
                <tr><td colspan="2">&nbsp;</td></tr>
                 <tr>
                    <td colspan="2" align="right">
                        <asp:UpdatePanel ID="upnlCommand" runat="server" UpdateMode="Always" >
                            <ContentTemplate>
                                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" ToolTip="Refresh membership cache" Width="72px" Height="20px" UseSubmitBehavior="False" CommandName="Refresh" OnCommand="OnCommand" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnWelcomeMessage" runat="server" Height="20px" Text="Send Welcome Message" ToolTip="No password reset, only welcome message is sent. " CommandName="Welcome" OnCommand="OnCommand" />
                                &nbsp;&nbsp;<asp:Button ID="btnResetPassword" Height="20px" runat="server" Text="Reset Password" ToolTip="Password is reset and mailed to the user." CommandName="Reset" OnCommand="OnCommand" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnRefresh" />
                                <asp:PostBackTrigger ControlID="btnWelcomeMessage" />
                                <asp:PostBackTrigger ControlID="btnResetPassword" />
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
        var grd = document.getElementById('ctl00_cpBody_grdUsers');
        for(var i=1; i<grd.rows.length; i++) {
            var cell = grd.rows[i].cells[1];
            if(cell.innerHTML.substr(0, username.length) == username) {
                var pnl = document.getElementById('ctl00_cpBody_pnlUsers');
                pnl.scrollTop = i * (grd.clientHeight / grd.rows.length);
                break;
            }
        }
    }
</script>
</asp:Content>
