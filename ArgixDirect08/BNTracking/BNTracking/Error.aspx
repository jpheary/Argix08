<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="_Error" StylesheetTheme="ArgixDirect" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>B&amp;N ~ DDU Tracking</title>
    <link href="App_Themes/ArgixDirect/ArgixDirect.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <table id="tblError" align="center" cellspacing="1" cellpadding="1" width="792" border="0">
			<tr>
				<td class="spacer"> </td>
				<td class="tableHeader">&nbsp;<asp:Label ID="lblTitle" runat="server" Text="System Error Encountered"></asp:Label></td>
			</tr>
			<tr>
				<td class="spacer"> </td>
				<td class="normalText">
					<p>An error has encountered while you were using this site.</p>
					<p>Please go back to the <a href="Default.aspx">Tracking Home Page</a> and start over.</p>
					<p>If the problem persists, restart your browser and try again..</p>
				</td>
			</tr>
			<tr>
			    <td class="spacer"></td>
			    <td class="body">&nbsp;<asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
			</tr>
	</table>
</body>
</html>


