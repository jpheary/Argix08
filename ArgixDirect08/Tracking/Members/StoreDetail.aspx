<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="StoreDetail.aspx.cs" Inherits="StoreDetail" Title="Store Detail" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cHead" runat="server" ContentPlaceHolderID="cpHead">
    <asp:LinkButton id="lnkHome" runat="server" CausesValidation="False" PostBackUrl="~/Members/Default.aspx" SkinID="lnkNav">Home...</asp:LinkButton>
    &nbsp;<asp:LinkButton id="lnkTracking" runat="server" CausesValidation="False" PostBackUrl="~/Members/TrackByStore.aspx" SkinID="lnkNav">Track By Store...</asp:LinkButton>
    &nbsp;<asp:LinkButton id="lnkStoreSummary" runat="server" CausesValidation="False" PostBackUrl="~/Members/StoreSummary.aspx" SkinID="lnkNav">Store Summary...</asp:LinkButton>
</asp:Content>
<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
    <table width="100%" border="0px" cellpadding="0px" cellspacing="1px">
		<tr style="font-size:1px"><td>&nbsp;</td></tr>
        <tr class="HeaderTitle"><td><asp:Label ID="lblTitle" runat="server" Text="Store Detail" SkinID="lblTitleText"></asp:Label></td></tr>
        <tr>
            <td>
                <asp:GridView ID="grdTLDetail" runat="server" Width="100%" AutoGenerateColumns="False">
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="CartonNo,LBLNo,TL" ItemStyle-HorizontalAlign="Left" DataNavigateUrlFormatString="StoreDetail.aspx?CTN={0}&amp;LBL={1}&amp;TL={2}" DataTextField="CartonNo"  HeaderText="Carton Number" Target="_self" />
                        <asp:BoundField DataField="Pudt" HeaderText="PU Date" ItemStyle-HorizontalAlign="Left" SortExpression="Pudt" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" />
                        <asp:BoundField DataField="Weight" HeaderText="Weight" ItemStyle-HorizontalAlign="Right" SortExpression="Weight" />
                        <asp:BoundField DataField="ShpName" HeaderText="Shipper" ItemStyle-HorizontalAlign="Left" SortExpression="ShpName" >
                            <ItemStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Style="position: relative" Text='<%# Eval("ShpCity") + "," + Eval("ShpState") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="CtnSts" HeaderText="Carton Status" ItemStyle-HorizontalAlign="Left" SortExpression="CtnSts" />
                        <asp:BoundField DataField="ScnSts" HeaderText="Scan Status" ItemStyle-HorizontalAlign="Left" SortExpression="ScnSts" />
                        <asp:TemplateField HeaderText="Scan Date/Time" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Style="position: relative" Text='<%# Eval("PodDate","{0:MM/dd/yyyy}") + " " + Eval("PodTime","{0:t}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="CBOL" HeaderText="CBOL" ItemStyle-HorizontalAlign="Left" SortExpression="CBOL" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr><td>&nbsp;</td></tr>
    </table>
    <table id="ErrorTable" width="100%" border="0px" cellpadding="1px" cellspacing="0px">
		<tr><td>&nbsp;</td></tr>
		<tr><td><asp:label id="messageLabel" runat="server" Width="85%" Font-Underline="True" EnableViewState="False"></asp:label></td></tr>
		<tr><td>&nbsp;</td></tr>
		<tr><td><asp:label id="errorLabel" runat="server" EnableViewState="False"></asp:label></td></tr>
	</table> 
 </asp:Content>


