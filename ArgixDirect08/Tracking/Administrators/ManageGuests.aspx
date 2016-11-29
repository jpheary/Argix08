<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="ManageGuests.aspx.cs" Inherits="ManageGuests" Title="Manage Guests" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cHead" runat="server" ContentPlaceHolderID="cpHead">
    <asp:LinkButton id="lnkTracking" runat="server" PostBackUrl="~/Members/Default.aspx" SkinID="lnkNav">Home...</asp:LinkButton>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cpBody" Runat="Server">
<table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
    <tr><td><div class="PanelLine">&nbsp;</div></td></tr>
    <tr><td><div class="PageTitle">Manage Registered Users (Guests)</div></td></tr>
    <tr><td>&nbsp;</td></tr>
    <tr>
        <td>
            <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
                <tr style="font-size:1px"><td width="144px">&nbsp;</td><td width="480px">&nbsp;</td><td>&nbsp;</td></tr>
                <tr><td colspan="3">&nbsp;</td></tr>
                <tr>
                    <td align="right">Guest&nbsp;</td>
                    <td>
                        <asp:DropDownList ID="cboGuest" runat="server" Width="192px" AutoPostBack="True" OnSelectedIndexChanged="OnGuestChanged"></asp:DropDownList>
                        <asp:ObjectDataSource ID="odsGuests" runat="server" TypeName="Roles" SelectMethod="GetUsersInRole" EnableCaching="false" >
                            <SelectParameters>
                                <asp:Parameter Name="roleName" DefaultValue="Guests" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr><td colspan="3">&nbsp;</td></tr>
                <tr>
                    <td align="right">User ID&nbsp;</td>
                    <td><asp:Label ID="txtUserID" runat="server" Width="192px" Height="16px" BorderStyle="Solid" BorderWidth="1"></asp:Label></td>
                    <td>&nbsp;</td>
                </tr>
                 <tr><td colspan="3">&nbsp;</td></tr>
               <tr>
                    <td align="right">User Full Name&nbsp;</td>
                    <td><asp:TextBox ID="txtUserName" runat="server" Width="240px" MaxLength="50" EnableViewState="False"></asp:TextBox></td>
                    <td>&nbsp;</td>
                </tr>
                  <tr><td colspan="3">&nbsp;</td></tr>
              <tr>
                    <td align="right">Company Name&nbsp;</td>
                    <td><asp:TextBox ID="txtCompany" runat="server" Width="288px" MaxLength="100" EnableViewState="False"></asp:TextBox></td>
                    <td>&nbsp;</td>
                </tr>
                 <tr><td colspan="3">&nbsp;</td></tr>
                <tr>
                    <td align="right">Email&nbsp;</td>
                    <td><asp:TextBox ID="txtEmail" runat="server" Width="288px" MaxLength="100" EnableViewState="False"></asp:TextBox></td>
                </tr>
                <tr><td colspan="3">&nbsp;</td></tr>
                <tr>
                    <td align="right">Company Type&nbsp;</td>
                    <td>
                        <asp:DropDownList ID="cboType" runat="server" Width="96px" AutoPostBack="True" OnSelectedIndexChanged="OnCompanyTypeChanged">
                            <asp:ListItem Text="Client" Value="Client" Selected="True" />
                            <asp:ListItem Text="Vendor" Value="Vendor" />
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                 <tr><td colspan="3">&nbsp;</td></tr>
               <tr>
                    <td align="right">Company&nbsp;</td>
                    <td>
                        <asp:DropDownList ID="cboCustomer" runat="server" Width="288px" DataSourceID="odsCustomers" DataTextField="CompanyName" DataValueField="ClientID"></asp:DropDownList>
                        <asp:ObjectDataSource ID="odsCustomers" runat="server" TypeName="TrackingServices" SelectMethod="GetCustomers" EnableCaching="true" CacheExpirationPolicy="Absolute" CacheDuration="1800" >
                            <SelectParameters>
                                <asp:ControlParameter Name="companyType" ControlID="cboType" PropertyName="SelectedValue" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr><td colspan="3">&nbsp;</td></tr>
                <tr>
                    <td align="right" valign="top">Optional Comments&nbsp;</td>
                    <td><asp:TextBox ID="txtComments" runat="server" Rows="2" TextMode="MultiLine" Width="100%" ToolTip="Use comments to give reason to user if you are rejecting registeration." EnableViewState="False"></asp:TextBox></td>
                </tr>
                <tr><td colspan="3">&nbsp;</td></tr>
                <tr>
                    <td colspan="2" align="right">
                        <asp:Button ID="btnReject" runat="server" Width="72px" Height="20px" Text="Reject" ToolTip="User record will be removed permanently." CommandName="Reject" OnCommand="OnCommand" />
                        &nbsp;<asp:Button ID="btnApprove" runat="server" Width="72px" Height="20px" Text="Approve" CommandName="Approve" OnCommand="OnCommand" />
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnClose" runat="server" Width="72px" Height="20px" Text="Close" CommandName="Close" OnCommand="OnCommand" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</asp:Content>

