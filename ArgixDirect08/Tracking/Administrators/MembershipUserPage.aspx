<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="MembershipUserPage.aspx.cs" Inherits="MembershipUserPage" Title="Membership User" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cHead" runat="server" ContentPlaceHolderID="cpHead">
    <asp:LinkButton id="lnkTracking" runat="server" SkinID="lnkNav" PostBackUrl="~/Members/Default.aspx">Home...</asp:LinkButton>&nbsp;
    <asp:LinkButton id="lnkMembership" runat="server" SkinID="lnkNav" PostBackUrl="~/Administrators/Memberships.aspx?username=" >Manage Membership...</asp:LinkButton>&nbsp;<asp:Label ID="_lblCurrentPage" runat="server" Text="Membership User..." SkinID="lblNav"></asp:Label>			
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cpBody" Runat="Server">
<table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
    <tr><td><div class="PanelLine">&nbsp;</div></td></tr>
    <tr><td><div class="PageTitle">Membership User</div></td></tr>
    <tr><td>&nbsp;</td></tr>
    <tr>
        <td>
            <table width="100%" border="0" cellpadding="0" cellspacing="3px" >
                <tr>
                    <td width="552px" valign="top">
                        <asp:Panel ID="pnlUser" runat="server" Width="100%" Height="100%" GroupingText="Membership">
                            <table width="100%" border="0" cellpadding="1px" cellspacing="6px">
                                <tr><td width="120px">&nbsp;</td><td width="240px">&nbsp;</td><td width="24px">&nbsp;</td><td width="96px">&nbsp;</td><td width="192px">&nbsp;</td><td>&nbsp;</td></tr>
                                <tr>
                                    <td align="right">User Name&nbsp;</td>
                                    <td><asp:TextBox ID="txtUserName" runat="server" MaxLength="32" Width="100%" OnTextChanged="OnUserNameChanged" AutoPostBack="True"></asp:TextBox></td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvUserID" runat="server" ControlToValidate="txtUserName" ErrorMessage="Please enter your UserID. It can be your email address." Style="position: relative; left: 0px;">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revUserID" runat="server" ControlToValidate="txtUserName" ErrorMessage="UserID must be at least 4 characters long and without spaces." ValidationExpression="^\w{4}[^\s]*$">*</asp:RegularExpressionValidator>
                                    </td>
                                    <td align="right"></td>
                                    <td><asp:CheckBox ID="chkApproved" runat="server" Width="100%" Text="Approved" AutoPostBack="True" OnCheckedChanged="OnApprovedChanged" /></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="right">Email&nbsp;</td>
                                    <td><asp:TextBox ID="txtEmail" runat="server" MaxLength="100" Width="100%" AutoCompleteType="Email" AutoPostBack="True" OnTextChanged="OnEmailChanged"></asp:TextBox></td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Please enter your email address." ControlToValidate="txtEmail">*</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Please enter a valid email address." ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                    </td>
                                    <td align="right">&nbsp;</td>
                                    <td><asp:CheckBox ID="chkLockedOut" runat="server" Width="100%" Enabled="False" Text="Locked Out" AutoPostBack="True" /></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="right">Password&nbsp;</td>
                                    <td><asp:TextBox ID="txtPassword" runat="server" Width="100%" MaxLength="20" AutoPostBack="True" OnTextChanged="OnPasswordChanged"></asp:TextBox></td>
                                    <td><asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Please choose a password at least 6 characters long." ControlToValidate="txtPassword">*</asp:RequiredFieldValidator></td>
                                    <td align="right">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="right">Comments&nbsp;</td>
                                    <td colspan="4"><asp:TextBox ID="txtComments" runat="server" Width="100%" AutoPostBack="True" OnTextChanged="OnCommentsChanged"></asp:TextBox></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr><td colspan="6">&nbsp;</td></tr>
                            </table>
                        </asp:Panel>
                        <br />
                        <asp:Panel ID="pnlProfile" runat="server" Width="100%" Height="100%" GroupingText="Profile">
                            <table width="100%" border="0" cellpadding="1px" cellspacing="6px">
                                <tr><td width="120px">&nbsp;</td><td width="240px">&nbsp;</td><td width="24px">&nbsp;</td><td>&nbsp;</td></tr>
                                <tr>
                                    <td align="right">Full Name&nbsp;</td>
                                    <td><asp:TextBox ID="txtFullName" runat="server" MaxLength="50" Width="100%" AutoPostBack="True" OnTextChanged="OnFullNameChanged"></asp:TextBox></td>
                                    <td><asp:RequiredFieldValidator ID="rfvFullName" runat="server" ErrorMessage="Please enter your first and last name." ControlToValidate="txtFullName" Display="Static" InitialValue=""  >*</asp:RequiredFieldValidator></td>
                                    <td>&nbsp;<asp:CheckBox ID="chkPWReset" runat="server" Text="Password Reset" AutoPostBack="True" OnCheckedChanged="OnPWResetChanged" /></td>
                                </tr>
                                <tr>
                                    <td align="right">Company&nbsp;</td>
                                    <td><asp:TextBox ID="txtCompany" runat="server" MaxLength="100" Width="100%" Enabled="false"></asp:TextBox></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="right">Company Type&nbsp;</td>
                                    <td>
                                        <asp:DropDownList ID="cboType" runat="server" Width="120px" AutoPostBack="True" OnSelectedIndexChanged="OnTypeChanged">
                                            <asp:ListItem Text="Client" Value="Client" Selected="True" />
                                            <asp:ListItem Text="Vendor" Value="Vendor" />
                                        </asp:DropDownList></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="right">Company&nbsp;</td>
                                    <td>
                                        <asp:DropDownList ID="cboCustomer" runat="server" Width="100%" DataSourceID="odsCustomer" DataTextField="CompanyName" DataValueField="ClientID" AutoPostBack="True" OnSelectedIndexChanged="OnCustomerChanged"></asp:DropDownList>
                                        <asp:ObjectDataSource ID="odsCustomer" runat="server" TypeName="TrackingServices" SelectMethod="GetCustomers" EnableCaching="true" CacheExpirationPolicy="Absolute" CacheDuration="1800" >
                                            <SelectParameters>
                                                <asp:ControlParameter Name="companyType" ControlID="cboType" PropertyName="SelectedValue" Type="String" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="right">Store Search Type&nbsp;</td>
                                    <td><asp:DropDownList ID="cboStoreSearchType" runat="server" Width="100%" AutoPostBack="True">
                                        <asp:ListItem Text="Argix Store Numbers" Value="Argix" Selected="True" />
                                        <asp:ListItem Text="Sub Store Numbers" Value="Sub" />
                                    </asp:DropDownList></td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr><td colspan="4">&nbsp;</td></tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td width="18px">&nbsp;</td>
                    <td valign="top">
                        <asp:Panel ID="pnlRole" runat="server" Width="100%" Height="100%" GroupingText="Role">
                            <table width="100%" border="0" cellpadding="1px" cellspacing="6px">
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="optRole" runat="server" Width="100%" Height="100%" AutoPostBack="True" OnSelectedIndexChanged="OnRoleChanged">
                                            <asp:ListItem Text="Guest" Value="guests" />
                                            <asp:ListItem Text="Administrator" Value="administrators" />
                                            <asp:ListItem Text="Member" Value="members" />
                                            <asp:ListItem Text="Web Service Member" Value="wsmembers" />
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr><td>&nbsp;</td></tr>
                            </table>
                        </asp:Panel>
                        <br />
                         <asp:Panel ID="pnlRoles" runat="server" Width="100%" Height="100%" GroupingText="Supplemental Roles">
                            <table width="100%" border="0" cellpadding="1px" cellspacing="6px">
                                <tr>
                                    <td>
                                        <asp:CheckBoxList ID="chkRoles" runat="server" Width="100%" Height="100%">
                                            <asp:ListItem Text="File Claims Member" Value="FileClaimsMember" />
                                            <asp:ListItem Text="PO\PRO Search Member" Value="pomembers" />
                                            <asp:ListItem Text="POD Image Member" Value="PODImageMember" />
                                            <asp:ListItem Text="Reports Member" Value="rsmembers" />
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                                <tr><td>&nbsp;</td></tr>
                            </table>
                        </asp:Panel>
                   </td>
                </tr>
                <tr><td colspan="3">&nbsp;<asp:ValidationSummary ID="vsRegister" runat="server" Width="100%" DisplayMode="List" /></td></tr>
                <tr>
                   <td colspan="3" align="right">
                        <asp:Button ID="btnUnlock" runat="server" Width="72px" Height="20px" Text="Unlock" CausesValidation="False" CommandName="Unlock" OnCommand="OnCommand" />
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSubmit" runat="server" Width="72px" Height="20px" Text="Submit" CommandName="OK" OnCommand="OnCommand" />
                        &nbsp;&nbsp;<asp:Button ID="btnClose" runat="server" Width="72px" Height="20px" Text="Close" CausesValidation="False" CommandName="Close" OnCommand="OnCommand" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</asp:Content>

