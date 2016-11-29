<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" Title="System Error" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="cHead" runat="server" ContentPlaceHolderID="cpHead">
</asp:Content>
<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
    <div class="PageTitle">System Error</div>
    <table id="tblPage" width="100%" align="center" border="0px" cellpadding="1px" cellspacing="1px">
			<tr><td width="48px">&nbsp</td><td>&nbsp;</td></tr>
			<tr>
				<td>&nbsp</td>
				<td>
					<p>
					    An unexpected error occurred while you were using this site. Please go back 
					    to the <a href="Members/Default.aspx">Tracking Home Page</a> and try again.
					</p>
					<p>If the problem persists, use the following information to contact Argix Direct Technical Support.</p>
					<p>&nbsp;</p>
					<p><asp:Label ID="lblError" runat="server" Width="100%" Height="100%" Text=""></asp:Label></p>
				</td>
			</tr>
			<tr><td colspan="2">&nbsp</td></tr>
			<tr>
				<td>&nbsp</td>
				<td>
				    <p>
				        <a href="mailto:extranet.support@argixdirect.com">extranet.support@argixdirect.com</a><br>
						Phone: 800-274-4582
					</p>
				</td>
			</tr>
	</table>
</asp:Content>

