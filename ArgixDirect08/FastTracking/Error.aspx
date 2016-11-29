<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="cBody" ContentPlaceHolderID="cpBody" Runat="Server">
<div id="detailHeadContainer" style="height:384px" > 
    <table id="tblError" width="768px" align="left" cellspacing="1px" cellpadding="1px" border="0px">
        <tr><td>&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
	        <td class="spacer"> </td>
	        <td class="tableHeader">&nbsp;<asp:Label ID="lblTitle" runat="server" Text="System Error Encountered"></asp:Label></td>
        </tr>
        <tr><td>&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
	        <td class="spacer"> </td>
	        <td class="normalText">
		        <p>An error was encountered while you were using this site. Please go back to the 
		        Fast Tracking Home Page and start over. If the problem persists, 
		        restart your browser and try again.</p>
	        </td>
        </tr>
        <tr><td>&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td class="spacer"></td>
            <td class="body">&nbsp;<asp:Label ID="lblError" runat="server" Text=""></asp:Label></td>
        </tr>
    </table>
</div>
</asp:Content>

