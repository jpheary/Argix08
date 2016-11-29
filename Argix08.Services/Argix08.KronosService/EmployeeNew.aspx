<%@ Page Language="C#" masterpagefile="~/Default.master" AutoEventWireup="true" CodeFile="EmployeeNew.aspx.cs" Inherits="EmployeeNew" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<div style="width:480px; border:solid 1px black; margin-left:48px; background-color:white">
    <table width="100%" border="0" cellpadding="0px" cellspacing="0px">
        <tr style="font-size:1px"><td width="24px">&nbsp;</td><td width="96px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr style="height:18px"><td colspan="3" style="font-size:1.0em; vertical-align:middle; padding-left:6px; background-image:url(App_Themes/Argix/Images/gridtitle.gif); background-repeat:repeat-x;">&nbsp;<bold>New Employee</bold></td></tr>
        <tr><td colspan="3" style="font-size:1px; height:24px">&nbsp;</td></tr>
        <tr>
            <td>&nbsp;</td>
            <td align="right">Last Name&nbsp;</td>
            <td>
                <asp:TextBox ID="txtLastName" runat="server" Width="192px" Text=""></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="Last Name is required." ControlToValidate="txtLastName" Display="Static" InitialValue="">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr><td colspan="3" style="font-size:1px; height:12px">&nbsp;</td></tr>
        <tr>
            <td>&nbsp;</td>
            <td align="right">First Name&nbsp;</td>
            <td>
                <asp:TextBox ID="txtFirstName" runat="server" Width="144px" Text=""></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="First Name is required." ControlToValidate="txtFirstName" Display="Static" InitialValue="">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr><td colspan="3" style="font-size:1px; height:12px">&nbsp;</td></tr>
        <tr>
            <td>&nbsp;</td>
            <td align="right">Middle&nbsp;</td>
            <td><asp:TextBox ID="txtMiddle" runat="server" Width="144px" Text=""></asp:TextBox></td>
        </tr>
        <tr><td colspan="3" style="font-size:1px; height:12px">&nbsp;</td></tr>
         <tr>
            <td>&nbsp;</td>
            <td align="right">Suffix&nbsp;</td>
            <td><asp:TextBox ID="txtSuffix" runat="server" Width="192px" Text=""></asp:TextBox></td>
        </tr>
        <tr><td colspan="3" style="font-size:1px; height:12px">&nbsp;</td></tr>
        <tr>
            <td>&nbsp;</td>
            <td align="right">Location&nbsp;</td>
            <td>
                <asp:DropDownList ID="cdoLocation" runat="server" Width="192px">
                    <asp:ListItem Text="Atlanta" Value="Wilmington"></asp:ListItem>
                    <asp:ListItem Text="Charlotte" Value="Wilmington"></asp:ListItem>
                    <asp:ListItem Text="Jamesburg" Value="Jamesburg" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Lakeland" Value="Wilmington"></asp:ListItem>
                    <asp:ListItem Text="Medley" Value="Wilmington"></asp:ListItem>
                    <asp:ListItem Text="Ridgefield" Value="Ridgefield"></asp:ListItem>
                    <asp:ListItem Text="South Windsor" Value="South Windsor"></asp:ListItem>
                    <asp:ListItem Text="Wilmington" Value="Wilmington"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr><td colspan="3" style="font-size:1px; height:12px">&nbsp;</td></tr>
        <tr>
            <td colspan="2">&nbsp;</td>
            <td>
                <asp:ValidationSummary ID="vsRegister" runat="server" Height="100%" Width="100%" DisplayMode="List" />
            </td>
        </tr>
        <tr><td colspan="3" style="font-size:1px; height:24px">&nbsp;</td></tr>
        <tr>            
            <td colspan="2">&nbsp;</td>
            <td style="text-align: right;">
                <asp:Button ID="btnOk" runat="server" Text="Ok" width="72px" OnClick="OnOk"></asp:Button>
                &nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" width="72px" CausesValidation="false" UseSubmitBehavior="false" OnClick="OnCancel"></asp:Button>
                &nbsp;&nbsp;&nbsp;
            </td>
        </tr>
         <tr><td colspan="3" style="font-size:1px; height:24px">&nbsp;</td></tr>
   </table>
</div>
</asp:Content>