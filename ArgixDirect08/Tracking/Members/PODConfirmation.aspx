<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PODConfirmation.aspx.cs" Inherits="PODConfirmation" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head  id="Head1" runat="server">
    <title>POD Request Confirmation</title>
	<style type="text/css" MEDIA="screen">
        <!-- 
		    body { font-family: Verdana, Arial, sans-serif; font-size: 0.8em; font-style: normal; font-weight: normal; color: black; background-color: white; }
            .ctbody { font-family: Verdana, Arial, sans-serif; font-size: 1.0em; font-style: normal; font-weight: normal; color: black; background-color: white; }
        -->
    </style>
</head>
<body>
<form id="form1" runat="server">
    <table width="576px" border="0px" cellpadding="3px" cellspacing="3px">
        <tr>
            <td class="ctbody" width="96px" align="center" valign="middle">
                <img src="../App_Themes/ArgixDirect/Images/info.bmp" height="72" />
            </td>
            <td class="ctbody" align="left" valign="top">
                Thank you. Your POD request has been submitted. You will receive an email confirming your request.<br /><br />
                If you do not receive a confirmation email then contact customer support at:<br />
		        &nbsp;&nbsp;&nbsp;&nbsp;Email: <a class="ctbody" href="mailto:extranet.support@argixdirect.com">extranet.support@argixdirect.com</a><br />
			    &nbsp;&nbsp;&nbsp;&nbsp;Phone: 800-274-4582
            </td>
        </tr>
        <tr><td colspan="2" class="ctbody">&nbsp;</td></tr>
        <tr><td colspan="2" class="ctbody" align="right">
            <asp:Button ID="btnOK" runat="server" Text="    OK    " /></td></tr>
    </table>
</form>
</body>
</html>

