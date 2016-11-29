<%@ Page Language="C#" MasterPageFile="~/MasterPages/Manage.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" Title="Change Password" %>
<%@ MasterType VirtualPath="~/MasterPages/Manage.master" %>

<asp:Content ID="cTitle" ContentPlaceHolderID="cpTitle" Runat="Server">
    <div class="PageTitle"><asp:Image ID="imgAdmin" runat="server" ImageUrl="~/App_Themes/ArgixDirect/Images/admin.gif" ImageAlign="AbsMiddle" />&nbsp;Change Password</div>
</asp:Content>
<asp:Content ID="cForm" ContentPlaceHolderID="cpForm" Runat="Server">
    <table id="tblPage" width="100%" border="0px" cellpadding="0px" cellspacing="0px" >
        <tr style="font-size:1px"><td width="144px">&nbsp;</td><td width="332px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr><td colspan="3"><asp:Label ID="lblExpired" runat="server" Text="Yor current password has expired. Please create a new password." Visible="false"></asp:Label></td></tr>
        <tr><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">User ID&nbsp;</td>
            <td><asp:TextBox ID="txtUserID" runat="server" Width="144px" ReadOnly="True" MaxLength="25"></asp:TextBox></td>
            <td><asp:RequiredFieldValidator ID="rfvUserID" runat="server" ControlToValidate="txtUserID" EnableViewState="False" SetFocusOnError="True" ErrorMessage="Please enter a valid User ID.">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Old Password&nbsp;</td>
            <td><asp:TextBox ID="txtOldPassword" runat="server" Width="144px" EnableViewState="False" TextMode="Password" MaxLength="20"></asp:TextBox></td>
            <td><asp:RequiredFieldValidator ID="rfvOldPassword" runat="server" ControlToValidate="txtOldPassword" EnableViewState="False" SetFocusOnError="True" ErrorMessage="Please enter your current password.">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">New Password&nbsp;</td>
            <td><asp:TextBox ID="txtNewPassword" runat="server" Width="144px" EnableViewState="False" TextMode="Password" MaxLength="20"></asp:TextBox></td>
            <td>
                <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="txtNewPassword" EnableViewState="False" SetFocusOnError="True" ErrorMessage="Please enter a valid new password.">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvNewPassword" runat="server" ControlToCompare="txtOldPassword" ControlToValidate="txtNewPassword" EnableViewState="False" ErrorMessage="Your new password must be different from your current password." Operator="NotEqual">*</asp:CompareValidator>
            </td>
        </tr>
        <tr><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Retype Password&nbsp;</td>
            <td><asp:TextBox ID="txtConfirmPassword" runat="server" Width="144px" EnableViewState="False" TextMode="Password" MaxLength="20"></asp:TextBox></td>
            <td><asp:CompareValidator ID="cvConfirmPassword" runat="server" ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPassword" EnableViewState="False" ErrorMessage="New and confirm password fields should match.">*</asp:CompareValidator></td>
        </tr>
        <tr><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="btnSubmit" runat="server" Width="72px" Height="24px" Text="Submit" CausesValidation="true" CommandName="Submit" OnCommand="OnCommand" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Width="72px" Height="24px" Text="Cancel" CausesValidation="false" CommandName="Cancel" OnCommand="OnCommand" />
           </td>
            <td>&nbsp;</td>
        </tr>
        <tr><td colspan="3">&nbsp;</td></tr>
        <tr style="height: 24px"><td colspan="3" align="left"><asp:ValidationSummary ID="vsSummary" runat="server" Width="100%" DisplayMode="SingleParagraph" /></td></tr>
    </table>
</asp:Content>
<asp:Content ID="cInfo" ContentPlaceHolderID="cpInfo" Runat="Server">
    <div class="PanelInfo">
    To change your password: 
    <ul>
        <li>enter your current (old) password </li>
        <li>enter a new password </li> 
        <li>confirm (retype) your new password </li>
        <li>click Submit </li>
    </ul>
    </div>
</asp:Content>

