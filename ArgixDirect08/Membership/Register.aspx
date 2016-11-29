<%@ Page Language="C#" MasterPageFile="~/Manage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" Title="Registration" %>
<%@ MasterType VirtualPath="~/Manage.master" %>

<asp:Content ID="cTitle" ContentPlaceHolderID="cpTitle" Runat="Server">
    <div class="PageTitle"><asp:Image ID="imgAdmin" runat="server" ImageUrl="~/App_Themes/ArgixDirect/Images/admin.gif" ImageAlign="AbsMiddle" />&nbsp;New User Registration</div>
</asp:Content>
<asp:Content ID="cForm" ContentPlaceHolderID="cpForm" Runat="Server">
    <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
        <tr style="font-size:1px;"><td width="120px">&nbsp;</td><td width="384px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr><td colspan="3">&nbsp;</td></tr>
        <tr><td>&nbsp;</td><td colspan="2">Enter your first and last name, company name, and email.</td></tr>
        <tr><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Full Name&nbsp;</td>
            <td align="left">
                <asp:TextBox ID="txtFullName" runat="server" Width="240px" MaxLength="50" ></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="rfvFullName" runat="server" ErrorMessage="Please enter your first and last name." ControlToValidate="txtFullName" Display="Static" InitialValue=""  >*</asp:RequiredFieldValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Company&nbsp;</td>
            <td align="left">
                <asp:TextBox ID="txtCompany" runat="server" Width="240px" MaxLength="100"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="rfvCompany" runat="server" ErrorMessage="Please enter company name." ControlToValidate="txtCompany">*</asp:RequiredFieldValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Email&nbsp;</td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" Width="240px" MaxLength="100"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Please enter your email address." ControlToValidate="txtEmail">*</asp:RequiredFieldValidator>
                &nbsp;<asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Please enter a valid email address." ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr><td colspan="3">&nbsp;</td></tr>
        <tr><td colspan="3">&nbsp;</td></tr>
        <tr><td>&nbsp;</td><td colspan="2">Enter a User ID at least 4 characers long without spaces;<br /> and a password at least 6 characters long.</td></tr>
        <tr><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">User ID&nbsp;</td>
            <td>
                <asp:TextBox ID="txtUserID" runat="server" Width="144px" MaxLength="25"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="rfvUserID" runat="server" ControlToValidate="txtUserID" ErrorMessage="Please enter your UserID. It can be your email address." Style="position: relative; left: 0px;">*</asp:RequiredFieldValidator>
                &nbsp;<asp:RegularExpressionValidator ID="revUserID" runat="server" ControlToValidate="txtUserID" ErrorMessage="UserID must be at least 4 characters long and without spaces." ValidationExpression="^\w{4}[^\s]*$">*</asp:RegularExpressionValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Password&nbsp;</td>
            <td>
                <asp:TextBox ID="txtPassword" runat="server" Width="192px" MaxLength="20" TextMode="Password" EnableViewState="False"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Please choose a password at least 6 characters long." ControlToValidate="txtPassword">*</asp:RequiredFieldValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right" >Retype Password&nbsp;</td>
            <td>
                <asp:TextBox ID="txtPasswordConfirm" runat="server" Width="192px" MaxLength="20" TextMode="Password" EnableViewState="False"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="rfvPasswordConfirm" runat="server" ControlToValidate="txtPasswordConfirm" ErrorMessage="Please retype the password.">*</asp:RequiredFieldValidator>
                &nbsp;<asp:CompareValidator ID="cvPasswordConfirm" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtPasswordConfirm" ErrorMessage="Passwords don't match. Please retype password." SetFocusOnError="True" ValueToCompare="Text">*</asp:CompareValidator>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="submitButton" runat="server" Text="Register" Width="72px" Height="24px" OnClick="OnSubmit" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Height="24px" Width="72px" Text="Cancel" CausesValidation="false" UseSubmitBehavior="false" OnClick="OnCancel" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr><td colspan="3">&nbsp;</td></tr>
        <tr><td colspan="3"><asp:ValidationSummary ID="vsRegister" runat="server" Height="100%" Width="100%" DisplayMode="List" /></td></tr>
    </table>
</asp:Content>
<asp:Content ID="cInfo" ContentPlaceHolderID="cpInfo" Runat="Server">
    <div class="PanelInfo">
        Enter the information at left and click the Register button to request
        access to our tracking web site.
        <br /><br />
        You will receive an email confirming your registration.
    </div>
</asp:Content>

