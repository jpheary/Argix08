<%@ page language="C#" masterpagefile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="StoreSummary.aspx.cs" Inherits="StoreSummary" stylesheettheme="ArgixDirect" Title="Store Summary" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cHead" runat="server" ContentPlaceHolderID="cpHead">
    <asp:LinkButton id="lnkHome" runat="server" CausesValidation="False" PostBackUrl="~/Members/Default.aspx" SkinID="lnkNav">Home...</asp:LinkButton>
    &nbsp;<asp:LinkButton id="lnkTracking" CausesValidation="False" runat="server" PostBackUrl="~/Members/TrackByStore.aspx" SkinID="lnkNav">Track By Store...</asp:LinkButton>
</asp:Content>
<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
    <table width="100%" border="0px" cellpadding="0px" cellspacing="1px">
		<tr style="font-size:1px"><td>&nbsp;</td></tr>
        <tr class="HeaderTitle"><td>&nbsp;<asp:Label ID="lblTitle" runat="server" Text="Store Summary" SkinID="lblTitleText"></asp:Label></td></tr>
        <tr>
            <td>
                <asp:GridView ID="grdSummary" runat="server" Width="100%" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="TL" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkTL" runat="server" NavigateUrl='<%# "StoreDetail.aspx?TL=" + HttpUtility.UrlEncode(Eval("TL").ToString()) %>' Text='<%# Eval("TL").ToString() %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CartonCount" HeaderText="Cartons" ItemStyle-HorizontalAlign="Right" SortExpression="CartonCount" />
                        <asp:BoundField DataField="Weight" HeaderText="Weight (lbs)" ItemStyle-HorizontalAlign="Right" SortExpression="Weight" />
                        <asp:BoundField DataField="CBOL" HeaderText="CBOL" ItemStyle-HorizontalAlign="Left" SortExpression="CBOL" />
                        <asp:BoundField DataField="PodDate" HeaderText="ETA or POD" ItemStyle-HorizontalAlign="Left" SortExpression="PodDate" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" />
                        <asp:BoundField DataField="AgName" HeaderText="Terminal" ItemStyle-HorizontalAlign="Left" SortExpression="AgName" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
    </table>
    <table id="ErrorTable" width="100%" border="0px" cellpadding="1px" cellspacing="0px">
		<tr><td>&nbsp;</td></tr>
		<tr><td class="ctLabel"><asp:label id="messageLabel" runat="server" Width="85%" Font-Underline="True" EnableViewState="False"></asp:label></td></tr>
		<tr><td>&nbsp;</td></tr>
		<tr><td class="ctLabel"><asp:label id="errorLabel" runat="server" EnableViewState="False"></asp:label></td></tr>
	</table> 
 </asp:Content>

