<%@ Page Language="C#" MasterPageFile="~/MasterPages/Executive.master" AutoEventWireup="true" CodeFile="ScanningDetailByStore.aspx.cs" Inherits="ScanningDetailByStore" %>
<%@ MasterType VirtualPath="~/MasterPages/Executive.master" %>

<asp:Content ID="idPrefix1" ContentPlaceHolderID="FilterPrefix1" Runat="Server"></asp:Content>
<asp:Content ID="idControl1" ContentPlaceHolderID="FilterControl1" Runat="Server">
    <asp:CheckBox ID="chkExceptionsOnly" runat="server" Text="Display exceptions only" OnCheckedChanged="OnExceptionsOnlyChecked" />
</asp:Content>
<asp:Content ID="idSuffix1" ContentPlaceHolderID="FilterSuffix1" Runat="Server">&nbsp;</asp:Content>
