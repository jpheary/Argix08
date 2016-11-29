<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login"  Title="Client Login" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <title>ID Viewer</title>
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="viewport" content="user-scalable=no, width=device-width" />
</head>
<body id="Body1" runat="server" style=" position:relative; margin-top:0; " >
<form id="Form1" runat="server" >
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" ScriptMode="Auto" />
    <asp:Table id="idMaster" runat="server" Width="320px" Height="460px" HorizontalAlign="Center" BorderWidth="2px" BorderStyle="Solid" CellPadding="0" CellSpacing="0" >
        <asp:TableRow style="font-size:1px; height:24px"><asp:TableCell>&nbsp;</asp:TableCell></asp:TableRow>
        <asp:TableRow Height="28px">
            <asp:TableCell ID="tcPageTitle" SkinID="PageTitle">
                <asp:Table ID="tblPageTitle" runat="server" Width="100%" BorderStyle="None" BorderWidth="0px" CellPadding="0" CellSpacing="0"> 
                    <asp:TableRow>
                        <asp:TableCell VerticalAlign="Bottom">
                            <asp:Image ID="imgApp" runat="server" ImageUrl="App_Themes/Argix/Images/app.gif" ImageAlign="Middle" />
                            &nbsp;<asp:Label id="lblAppTitle" runat="server" Height="100%" Text="Argix Direct IDViewer" />
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Right" VerticalAlign="Bottom">
                            &nbsp;
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow style="font-size:1px; height:12px"><asp:TableCell>&nbsp;</asp:TableCell></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell VerticalAlign="Top">
                <table width="100%"border="0px" cellpadding="0px" cellspacing="0px" >
                    <tr style="font-size:1px"><td width="192px">&nbsp;</td><td width="288px">&nbsp;</td><td>&nbsp;</td></tr>
                    <tr><td colspan="3">&nbsp;</td></tr>
                    <tr>
                        <td align="right">User ID&nbsp;</td>
                        <td><asp:TextBox ID="txtUserID" runat="server" Width="192px" MaxLength="25"></asp:TextBox></td>
                        <td><asp:RequiredFieldValidator ID="rfvUserID" runat="server" ErrorMessage="Please enter a valid User ID." ControlToValidate="txtUserID" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                    </tr>
                    <tr><td colspan="3">&nbsp;</td></tr>
                    <tr>
                        <td align="right">Password&nbsp;</td>
                        <td><asp:TextBox ID="txtPassword" runat="server" Width="192px" TextMode="Password" MaxLength="20"></asp:TextBox></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr><td colspan="3">&nbsp;</td></tr>
                    <tr>
                        <td colspan="2" align="right"><asp:Button ID="LoginButton" runat="server" Width="72" Height="24" Text="Sign In" OnClick="OnLogin" /></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr><td colspan="3"><asp:ValidationSummary ID="lvsLogin" runat="server" Height="100%" Width="100%" DisplayMode="SingleParagraph" /></td></tr>
                </table>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow style="font-size:1px; height:3px"><asp:TableCell>&nbsp;</asp:TableCell></asp:TableRow>
        <asp:TableRow Height="57px">
            <asp:TableCell>
                <table width="100%" border="1px" cellpadding="2px" cellspacing="2px">
                    <tr>
                        <td style="width:20%; text-align:center; border: solid 1px black">
                            <asp:ImageButton ID="imgProfiles" runat="server" Height="24px" ImageUrl="~/App_Themes/Argix/Images/id-card.png" CommandName="Employees" />
                            <br /><asp:Label ID="lblProfiles" runat="server" Text="Profiles"></asp:Label>
                        </td>
                        <td style="width:20%; text-align:center; border: solid 1px black">
                            <asp:ImageButton ID="imgPhotos" runat="server" Height="24px" ImageUrl="~/App_Themes/Argix/Images/camera.png" CommandName="Photos" />
                            <br /><asp:Label ID="lblPhotos" runat="server" Text="Photos"></asp:Label>
                        </td>
                        <td style="width:20%; text-align:center; border: solid 1px black">
                            <asp:ImageButton ID="imgSearch" runat="server" Height="24px" ImageUrl="~/App_Themes/Argix/Images/search.png" CommandName="Search" />
                            <br /><asp:Label ID="lblSearch" runat="server" Text="Search"></asp:Label>
                        </td>
                        <td style="width:40%; text-align:center; border: solid 1px black">&nbsp;</td>
                    </tr>
                </table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>                
    </form>
</body>
</html>