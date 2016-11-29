<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="CartonSummary.aspx.cs" Inherits="CartonSummary" Title="Carton Summary" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cHead" runat="server" ContentPlaceHolderID="cpHead">
    <asp:LinkButton id="lnkHome" runat="server" CausesValidation="False" PostBackUrl="~/Members/Default.aspx" SkinID="lnkNav">Home...</asp:LinkButton>
    &nbsp;<asp:LinkButton id="lnkTracking" runat="server" PostBackUrl="~/Members/TrackByCarton.aspx" SkinID="lnkNav">Tracking...</asp:LinkButton>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cpBody" Runat="Server">
    <asp:panel id="cartonPanel" runat="server">
        <table width="100%" border="0" cellpadding="0" cellspacing="1">
            <tr style="font-size:1px"><td>&nbsp;</td></tr>
            <tr class="HeaderTitle"><td><asp:Label ID="lblTitle" runat="server" Text="Carton Summary" SkinID="lblTitleText"></asp:Label></td></tr>
			<tr><td><asp:Label id="lblSubTitle" runat="server"></asp:Label></td></tr>
            <tr><td>&nbsp;</td></tr>
            <tr valign="top">
                <td>
                    <asp:GridView ID="SummaryGridView" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="ID">
                        <Columns>
                            <asp:HyperLinkField DataTextField="DisplayNumber" HeaderText="Tracking Number" HeaderStyle-HorizontalAlign="Left" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="CartonDetail.aspx?ID={0}" NavigateUrl="~/Members/TrackingDetail.aspx" >
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:HyperLinkField>
                            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" Visible="False" />
                            <asp:BoundField DataField="CTNNumber" HeaderText="Carton#" SortExpression="CTNNumber" Visible="False" />
                            <asp:BoundField DataField="LBLNumber" HeaderText="Label#" SortExpression="LBLNumber" Visible="False" />
                            <asp:BoundField DataField="DateTime" HeaderText="Date/Time" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" SortExpression="DateTime" />
                            <asp:BoundField DataField="Location" HeaderText="Location" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" SortExpression="Location" />
                            <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" SortExpression="Status" />
                            <asp:BoundField DataField="CBOL" HeaderText="CBOL" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" SortExpression="CBOL" />
                        </Columns>
                    </asp:GridView>
                    &nbsp; &nbsp;&nbsp;
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
        </table>
    </asp:panel>
    <asp:panel id="cartonNotFoundPanel" runat="server" HorizontalAlign="Center" Visible="False">
		<table id="DetailTable" width="800px" cellspacing="0" cellpadding="1" border="0">
			<tr class="HeaderTitle">
				<td style="width:50%" align="left">
					<asp:Label id="Label3" runat="server" SkinID="lblTitleText">Carton(s) Not Searched/Not Found</asp:Label></td>
				<td style="width:50%">&nbsp;</td>
			</tr>
			<tr><td colspan="2" style="width:50%" align="left"><asp:Label id="notFoundLabel" runat="server">The following cartons were not found. Please re-submit them after making the necessary corrections.</asp:Label></td></tr>
			<tr>
				<td style="width:50%" align="left"><asp:TextBox id="cartonNotFoundTextBox" runat="server" Width="286px" TextMode="MultiLine" Rows="5" ReadOnly="True" EnableViewState="False"></asp:TextBox></td>
				<td style="width:50%">&nbsp;</td>
			</tr>
			<tr><td style="width:50%">&nbsp;</td><td style="width:50%">&nbsp;</td></tr>
		</table>
	 </asp:panel>
	<table id="ErrorTable" width="768" cellspacing="0" cellpadding="1" border="0">
		<tr><td style="width:100%">&nbsp;</td></tr>
		<tr><td style="width:100%"><asp:label id="messageLabel" runat="server" Width="85%" Font-Underline="True" EnableViewState="False"></asp:label></td></tr>
		<tr><td style="width:100%">&nbsp;</td></tr>
		<tr><td  style="width:100%"><asp:label id="errorLabel" runat="server" EnableViewState="False"></asp:label></td></tr>
	</table> 
</asp:Content>

