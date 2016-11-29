<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="ArgixDirect.Error" MasterPageFile="~/App_MasterPages/Tracking.master" Title="System Error" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <table id="Table4" align="center" cellspacing="1" cellpadding="1" width="792" border="0">
			<tr>
				<td class="spacer"> </td>
				<td class="tableHeader">&nbsp;<asp:Label ID="Label1" runat="server" Text="System Error Encountered:"></asp:Label></td>
			</tr>
			<tr>
				<td class="spacer"> </td>
				<td class="normalText">
					<p>An error has encountered while you were using this site.</p>
					<p>Please go back to the <a href="Default.aspx">Tracking Home Page</a> and try again.</p>
					<p>If the problem persists, use the following information to contact Argix Direct Technical Support.</p>
				</td>
			</tr>
			<tr>
				<td class="spacer"> </td>
				<td class="body">&nbsp;</td>
			</tr>
			<tr>
				<td class="spacer"></td>
				<td><p class="normalText"><a href="mailto:extranet.support@argixdirect.com">extranet.support@argixdirect.com</a><br> Phone: (800) 274-4582</p></td>
			</tr>
	</table>
</asp:Content>

