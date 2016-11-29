<%@ Page Language="C#" MasterPageFile="~/MasterPages/Executive.master" AutoEventWireup="true" CodeFile="OnTimeDeliveryDetailByStoreAndVendor.aspx.cs" Inherits="OnTimeDeliveryDetailByStoreAndVendor" %>
<%@ MasterType VirtualPath="~/MasterPages/Executive.master" %>

<asp:Content ID="idPrefix1" ContentPlaceHolderID="FilterPrefix1" Runat="Server"></asp:Content>
<asp:Content ID="idControl1" ContentPlaceHolderID="FilterControl1" Runat="Server">
    <asp:CheckBox ID="chkExceptionsOnly" runat="server" Text="Display exceptions only" OnCheckedChanged="OnExceptionsOnlyChecked" />
</asp:Content>
<asp:Content ID="idSuffix1" ContentPlaceHolderID="FilterSuffix1" Runat="Server">&nbsp;</asp:Content>

<asp:Content ID="idPrefix2" ContentPlaceHolderID="FilterPrefix2" Runat="Server"></asp:Content>
<asp:Content ID="idControl2" ContentPlaceHolderID="FilterControl2" Runat="Server">
<asp:DropDownList id="cboVendor" runat="server" Width="288px" DataSourceID="odsVendors" DataTextField="VendorName" DataValueField="VendorNumber" AutoPostBack="True" OnSelectedIndexChanged="OnVendorChanged"></asp:DropDownList>
<asp:DropDownList id="cboVendorLoc" runat="server" Width="288px" DataSourceID="odsVendorLocs" DataTextField="VendorSummary" DataValueField="VendorNumber" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="OnVendorLocChanged">
    <asp:ListItem Text="All" Value="" Selected="True"></asp:ListItem>
</asp:DropDownList>
<asp:ObjectDataSource ID="odsVendors" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetVendorsList" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="300" >
    <SelectParameters>
        <asp:ControlParameter Name="clientNumber" ControlID="cboClient" PropertyName="SelectedValue" Type="String" />
        <asp:Parameter Name="clientTerminal" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsVendorLocs" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetVendorLocations" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="600" >
    <SelectParameters>
        <asp:ControlParameter Name="clientNumber" ControlID="cboClient" PropertyName="SelectedValue" Type="String" />
        <asp:Parameter Name="clientTerminal" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
        <asp:ControlParameter Name="vendorNumber" ControlID="cboVendor" PropertyName="SelectedValue" ConvertEmptyStringToNull="true" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>

</asp:Content>
<asp:Content ID="idSuffix2" ContentPlaceHolderID="FilterSuffix2" Runat="Server">&nbsp;</asp:Content>
