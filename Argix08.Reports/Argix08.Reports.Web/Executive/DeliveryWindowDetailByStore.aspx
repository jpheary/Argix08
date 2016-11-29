<%@ Page Language="C#" MasterPageFile="~/MasterPages/Executive.master" AutoEventWireup="true" CodeFile="DeliveryWindowDetailByStore.aspx.cs" Inherits="DeliveryWindowDetailByStore" %>
<%@ MasterType VirtualPath="~/MasterPages/Executive.master" %>

<asp:Content ID="idPrefix1" ContentPlaceHolderID="FilterPrefix1" Runat="Server">Exceptions</asp:Content>
<asp:Content ID="idControl1" ContentPlaceHolderID="FilterControl1" Runat="Server">
    <asp:DropDownList id="cboException" runat="server" Width="192px" DataSourceID="odsExceptions" DataTextField="Exception" DataValueField="Exception" AutoPostBack="True" OnSelectedIndexChanged="OnValidateForm"></asp:DropDownList>
    <asp:ObjectDataSource ID="odsExceptions" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetDeliveryExceptions" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="600" />
</asp:Content>
<asp:Content ID="idSuffix1" ContentPlaceHolderID="FilterSuffix1" Runat="Server">&nbsp;</asp:Content>
