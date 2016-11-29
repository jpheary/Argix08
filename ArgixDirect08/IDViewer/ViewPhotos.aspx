<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="ViewPhotos.aspx.cs" Inherits="ViewPhotos" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="body">
    <asp:UpdatePanel ID="upnlPage" runat="server" ChildrenAsTriggers="true" RenderMode="Block" UpdateMode="Conditional" >
    <ContentTemplate>
    <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
        <tr>
            <td style="width:25%"><asp:Button ID="btnDrivers" runat="server" Width="100%" Height="20px" Text="Drivers" BorderStyle="Solid" BorderWidth="1px" style="border-bottom-style:solid" BorderColor="Black" OnCommand="OnChangeView" CommandName="Drivers" /></td>
            <td style="width:25%"><asp:Button ID="btnEmployees" runat="server" Width="100%" Height="20px" Text="Employees" BorderStyle="Solid" BorderWidth="1px" style="border-bottom-style:solid" OnCommand="OnChangeView" CommandName="Employees" /></td>
            <td style="width:25%"><asp:Button ID="btnHelpers" runat="server" Width="100%" Height="20px" Text="Helpers" BorderStyle="Solid" BorderWidth="1px" style="border-bottom-style:solid" OnCommand="OnChangeView" CommandName="Helpers" /></td>
            <td style="width:25%"><asp:Button ID="btnVendors" runat="server" Width="100%" Height="20px" Text="Vendors" BorderStyle="Solid" BorderWidth="1px" style="border-bottom-style:solid" OnCommand="OnChangeView" CommandName="Vendors" /></td>
        </tr>
        <tr style="font-size:1px; height:12px"><td colspan="4" style="background-color:White; border-left: solid 1px black; border-right:solid 1px black">&nbsp;</td></tr>
        <tr>
            <td colspan="4" valign="top" align="center" style="background-color:White; border:solid 1px black; border-top:none">
                <asp:Panel id="pnlEmployee" runat="server" Width="98%" Height="280px" BorderStyle="None" BorderWidth="1px" ScrollBars="Vertical">
                    <asp:Image ID="imgPhoto" runat="server" Height="200px" />
                    <br /><br />
                    <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
                        <tr>
                            <td style="width:20%"><asp:ImageButton ID="btnBack" runat="server" Height="24px" ImageUrl="~/App_Themes/Argix/Images/back.gif" OnCommand="OnChangePhoto" CommandName="Back" /></td>
                            <td style="width:60%"><asp:Label ID="lblName" runat="server"></asp:Label></td>
                            <td style="width:20%"><asp:ImageButton ID="imgNext" runat="server" Height="24px" ImageUrl="~/App_Themes/Argix/Images/select.gif" OnCommand="OnChangePhoto" CommandName="Next" /></td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>