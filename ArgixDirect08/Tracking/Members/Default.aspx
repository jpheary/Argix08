<%@ Page Language="C#" MasterPageFile="~/MasterPages/Tracking.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" Title="Argix Direct Carton Tracking" %>
<%@ MasterType VirtualPath="~/MasterPages/Tracking.master" %>

<asp:Content ID="cTitle" ContentPlaceHolderID="cpTitle" Runat="Server">
    <div class="PageTitle"><asp:Image ID="imgTitle" runat="server" ImageUrl="~/App_Themes/ArgixDirect/Images/tracking.gif" ImageAlign="AbsMiddle" />&nbsp;Argix Direct Carton Tracking</div>
</asp:Content>
<asp:Content  ID="cForm" ContentPlaceHolderID="cpForm" Runat="Server">
    <p>
        <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
        If you have any difficulty with this application, please contact us by email at
        <br /><br />
        <a href="mailto:extranet.support@argixdirect.com">extranet.support@argixdirect.com</a>
	    &nbsp;or by phone at 1-800-274-4582.
    </p>
</asp:Content>


