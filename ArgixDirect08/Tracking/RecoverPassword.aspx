<%@ Page Language="C#" MasterPageFile="~/MasterPages/Manage.master" AutoEventWireup="true" CodeFile="RecoverPassword.aspx.cs" Inherits="RecoverPassword" Title="Recover Password" %>
<%@ MasterType VirtualPath="~/MasterPages/Manage.master" %>

<asp:Content ID="cTitle" ContentPlaceHolderID="cpTitle" Runat="Server">
    <div class="PageTitle"><asp:Image ID="imgAdmin" runat="server" ImageUrl="~/App_Themes/ArgixDirect/Images/admin.gif" ImageAlign="AbsMiddle" />&nbsp;Recover Password</div>
</asp:Content>
<asp:Content ID="cForm" ContentPlaceHolderID="cpForm" Runat="Server">
    <asp:PasswordRecovery ID="ctlRecoverPW" runat="server" Width="100%" Height="100%" OnSendingMail="OnSendingMail" OnSendMailError="OnSendMailError" OnVerifyingUser="OnVerifyingUser" OnUserLookupError="OnUserLookupError">
        <UserNameTemplate>
            <table border="0px" cellpadding="0px" cellspacing="0px">
                <tr style="font-size:1px"><td width="96px">&nbsp;</td><td width="288px">&nbsp;</td><td>&nbsp;</td></tr>
                <tr><td colspan="3">&nbsp;</td></tr>
                <tr><td>&nbsp;</td><td colspan="2" align="center">Enter your User ID to receive a new password by email.</td></tr>
                <tr><td colspan="3">&nbsp;</td></tr>
                <tr><td colspan="3">&nbsp;</td></tr>
                <tr>
                    <td align="right"><asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">UserID&nbsp;</asp:Label></td>
                    <td colspan="2">
                        <asp:TextBox ID="UserName" runat="server" width="192px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User ID is required." ToolTip="User ID is required." ValidationGroup="ctlRecoverPW">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr><td colspan="3">&nbsp;</td></tr>
                <tr><td>&nbsp;</td><td colspan="2" style="color:red"><asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal></td></tr>
                <tr><td colspan="3">&nbsp;</td></tr>
                <tr>
                    <td colspan="3" align="right">
                        <asp:Button ID="SubmitButton" runat="server" Height="24px" Width="72px" Text="Submit" CommandName="Submit" ValidationGroup="ctlRecoverPW" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Height="24px" Width="72px" Text="Cancel" CausesValidation="false" UseSubmitBehavior="false" OnClick="OnCancel" />
                    </td>
                </tr>
                <tr><td colspan="3">&nbsp;</td></tr>
            </table>
        </UserNameTemplate>
    </asp:PasswordRecovery>
</asp:Content>
<asp:Content ID="cInfo" ContentPlaceHolderID="cpInfo" Runat="Server">
    <div class="PanelInfo">
    Enter your userID and click Submit.
    <br /><br />
    We will send an email to you with a new, temporary password. The next time you login, 
    with the temporary password, you will be required to change the temporary password to 
    a password of your choice.
    </div>
</asp:Content>
