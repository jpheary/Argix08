<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="CartonSummary.aspx.cs" Inherits="CartonSummary" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="cBody" ContentPlaceHolderID="cpBody" Runat="Server">
<div>
    &nbsp;<asp:Label ID="Label1" runat="server" SkinID="lblTextDetail" Text="Tracking Results Summary" ></asp:Label>
    <br /><br />
    <asp:GridView ID="grdTrack" runat="server" Width="775px" Height="96px" AutoGenerateColumns="False" DataKeyNames="LabelNumber" SkinID="GridSkin" OnRowDataBound="OnItemDataBound">
        <Columns>
            <asp:HyperLinkField DataTextField="ItemNumber" HeaderText="Tracking Number" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="true" DataNavigateUrlFields="LabelNumber" DataNavigateUrlFormatString="CartonDetail.aspx?item={0}" NavigateUrl="~/CartonDetail.aspx" />
            <asp:BoundField DataField="DateTime" HeaderText="Date" HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" />
            <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-Width="144px" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="Location" HeaderText="Location" HeaderStyle-Width="192px" HeaderStyle-HorizontalAlign="Left" />
        </Columns>
        <HeaderStyle Font-Size="10pt" />
    </asp:GridView>
</div>   
</asp:Content>
