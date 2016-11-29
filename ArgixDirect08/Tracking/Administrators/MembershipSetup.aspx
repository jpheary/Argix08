<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="MembershipSetup.aspx.cs" Inherits="MembershipSetup" Title="Membership Setup" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cHead" runat="server" ContentPlaceHolderID="cpHead">
    <asp:LinkButton id="lnkTracking" runat="server" PostBackUrl="~/Members/Default.aspx" SkinID="lnkNav">Home...</asp:LinkButton>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cpBody" Runat="Server">
<table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
    <tr><td><div class="PanelLine">&nbsp;</div></td></tr>
    <tr><td><div class="PageTitle">Membership Setup</div></td></tr>
    <tr><td>&nbsp;</td></tr>
    <tr>
        <td>
            <table width="100%" border="0px" cellpadding="2px" cellspacing="3px">
                <tr><td width="12px">&nbsp;</td><td width="144px">&nbsp;</td><td width="192px">&nbsp;</td><td width="24px">&nbsp;</td><td width="144px">&nbsp;</td><td width="192px">&nbsp;</td><td>&nbsp;</td></tr>
                <tr>
                    <td>&nbsp;</td>
                    <td align="right">Name</td>
                    <td><asp:TextBox ID="txtAppName" runat="server" MaxLength="96" Width="100%" ReadOnly="true" AutoPostBack="True" ToolTip="Name of the Membership account" Wrap="False"></asp:TextBox></td>
                    <td>&nbsp;</td>
                    <td align="right">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td align="right">Provider</td>
                    <td><asp:TextBox ID="txtProviderName" runat="server" MaxLength="32" Width="100%" ReadOnly="true" AutoPostBack="True" ToolTip="Name of the Membership Provider" Wrap="False"></asp:TextBox></td>
                    <td>&nbsp;</td>
                    <td align="right">Description</td>
                    <td><asp:TextBox ID="txtProviderDesc" runat="server" MaxLength="32" Width="100%" ReadOnly="true" AutoPostBack="True" ToolTip="Description of the Membership Provider" Wrap="False"></asp:TextBox></td>
                    <td>&nbsp;</td>
                </tr>
                <tr><td colspan="7">&nbsp;</td></tr>
                <tr>
                    <td>&nbsp;</td>
                    <td align="right">&nbsp;</td>
                    <td><asp:CheckBox ID="chkUniqueEmail" runat="server" Width="100%" Enabled="false" AutoPostBack="True" Text="Requires Unique Email"></asp:CheckBox></td>
                    <td>&nbsp;</td>
                    <td align="right">Password Format</td>
                    <td><asp:TextBox ID="txtPasswordFormat" runat="server" MaxLength="32" Width="100%" ReadOnly="true" AutoPostBack="True" Wrap="False"></asp:TextBox></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td align="right">&nbsp;</td>
                    <td><asp:CheckBox ID="chkQAndA" runat="server" Width="100%" Enabled="false" AutoPostBack="True" Text="Requires Question & Answer"></asp:CheckBox></td>
                    <td>&nbsp;</td>
                    <td align="right">PW Strength Reg Exp</td>
                    <td><asp:TextBox ID="txtPWStrengthRegEx" runat="server" MaxLength="32" Width="100%" ReadOnly="true" AutoPostBack="True" Wrap="False"></asp:TextBox></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td align="right">&nbsp;</td>
                    <td><asp:CheckBox ID="chkPWRetrieval" runat="server" Width="100%" Enabled="false" AutoPostBack="True" Text="Enable Password Retrieval"></asp:CheckBox></td>
                    <td>&nbsp;</td>
                    <td align="right">Hash Algorithm Type</td>
                    <td><asp:TextBox ID="txtHashAlgorithmType" runat="server" MaxLength="32" Width="100%" ReadOnly="true" AutoPostBack="True" Wrap="False"></asp:TextBox></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td align="right">&nbsp;</td>
                    <td><asp:CheckBox ID="chkPWReset" runat="server" Width="100%" Enabled="false" AutoPostBack="True" Text="Enable Password Reset"></asp:CheckBox></td>
                    <td>&nbsp;</td>
                    <td align="right">Min PW Length</td>
                    <td><asp:TextBox ID="txtMinPWLength" runat="server" MaxLength="32" Width="48px" ReadOnly="true" AutoPostBack="True" Wrap="False"></asp:TextBox></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td align="right">Max PW Attempts</td>
                    <td><asp:TextBox ID="txtMaxInvalidPWAttempts" runat="server" MaxLength="32" Width="48px" ReadOnly="true" AutoPostBack="True"></asp:TextBox></td>
                    <td>&nbsp;</td>
                    <td align="right">Min Non-Alpha Chars</td>
                    <td><asp:TextBox ID="txtMinNonAlphaChars" runat="server" MaxLength="32" Width="48px" ReadOnly="true" AutoPostBack="True" Wrap="False"></asp:TextBox></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td align="right">PW Attempt Window</td>
                    <td><asp:TextBox ID="txtPasswordAttemptWindow" runat="server" MaxLength="32" Width="48px" ReadOnly="true" AutoPostBack="True"></asp:TextBox></td>
                    <td>&nbsp;</td>
                    <td align="right">User Is Online Window</td>
                    <td><asp:TextBox ID="txtUserIsOnlineTimeWindow" runat="server" MaxLength="32" Width="48px" ReadOnly="true" AutoPostBack="True" Wrap="False"></asp:TextBox></td>
                    <td>&nbsp;</td>
                </tr>
                <tr><td colspan="7">&nbsp;</td></tr>
                <tr>
                    <td colspan="6" align="right">
                        <asp:Button ID="btnClose" runat="server" Width="72px" Height="20px" Text="Close" CommandName="Close" OnCommand="OnCommand" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</asp:Content>
