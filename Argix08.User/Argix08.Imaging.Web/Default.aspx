<%@ Page Language="C#" masterpagefile="~/Imaging.master" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" Title="SharePoint.Search" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <table width="768px" border="0px" cellpadding="0px" cellspacing="0px">
        <tr style="font-size:1px"><td style="width:144px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td>Search >>>>>>></td>
            <td>
                <table border="0px" cellpadding="3px" cellspacing="3px">
                    <tr style="height:18px; font-size:1.2em">
                        <td align="center" style="width:192px"><asp:HyperLink ID="lnkFinance" runat="server" NavigateUrl="~/financeimages.aspx">Finance Images</asp:HyperLink></td>
                        <td align="center" style="width:192px"><asp:HyperLink ID="lnkHR" runat="server" NavigateUrl="~/hrimages.aspx">HR Images</asp:HyperLink></td>
                        <td align="center" style="width:192px"><asp:HyperLink ID="lnkTsort" runat="server" NavigateUrl="~/tsortimages.aspx">Tsort Images</asp:HyperLink></td>
                   </tr>
                </table>
            </td>
        </tr>
        <tr><td style="height:48px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr><td valign="top">Site Config Info</td><td>&nbsp;<asp:TextBox ID="txtSearchInfo" runat="server" Width="576px" Height="72px" TextMode="MultiLine" Wrap="true" Text="" ReadOnly="true"></asp:TextBox></td></tr>
        <tr><td>&nbsp;</td><td>&nbsp;</td></tr>
        <tr><td valign="top">Search MetaData</td><td>&nbsp;<asp:TextBox ID="txtMetaData" runat="server" Width="576px" Height="288px" TextMode="MultiLine" Wrap="true" Text="" ReadOnly="true"></asp:TextBox></td></tr>
    </table>
</asp:Content>