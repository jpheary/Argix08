<%@ Page Language="C#" MasterPageFile="~/MasterPages/Tracking.master" AutoEventWireup="true" CodeFile="TrackByStore.aspx.cs" Inherits="TrackByStore" Title="Track By Store" %>
<%@ MasterType VirtualPath="~/MasterPages/Tracking.master" %>

<asp:Content ID="cTitle" ContentPlaceHolderID="cpTitle" Runat="Server">
    <div class="PageTitle"><asp:Image ID="imgTitle" runat="server" ImageUrl="~/App_Themes/ArgixDirect/Images/tracking.gif" ImageAlign="AbsMiddle" />&nbsp;Track By Store</div>
</asp:Content>
<asp:Content ID="cForm" ContentPlaceHolderID="cpForm" Runat="Server">
    <asp:MultiView ID="mvMain" runat="server" ActiveViewIndex="0">
    <asp:View ID="vwSearchStore" runat="server">
        <!-- Search store view -->
         <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
            <tr style="font-size:1px; height:32px"><td width="96px">&nbsp;</td><td width="432px">&nbsp;</td><td>&nbsp;</td></tr>
            <tr>
                <td align="right">Client&nbsp;</td>
                <td>
                    <asp:DropDownList ID="cboClient" runat="server" Width="288px" DataSourceID="odsClients" DataTextField="CompanyName" DataValueField="ClientID"></asp:DropDownList>
                    <asp:ObjectDataSource ID="odsClients" runat="server" TypeName="TrackingServices" SelectMethod="GetSecureClients" EnableCaching="false"></asp:ObjectDataSource>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
            <tr>
                <td align="right">Store#&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtStore" runat="server" Width="96px" MaxLength="30" AutoPostBack="True" OnTextChanged="OnValidateForm"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkSubSearch" runat="server" Height="18px" Text="Sub-store search" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
            <tr>
                <td align="right">Search By&nbsp;</td>
                <td>
                    <asp:DropDownList ID="cboDateType" runat="server" Width="144px" AutoPostBack="true" OnSelectedIndexChanged="OnValidateForm">
                        <asp:ListItem Text="Pickup Date" Value="Pickup" />
                        <asp:ListItem Text="Delivery Date" Value="Delivery" Selected="True" />
                    </asp:DropDownList></td>
                <td>&nbsp;</td>
            </tr>
            <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <table cellspacing="6px" width="100%">
                        <tr>
                            <td style="width: 50%">From Date</td>
                            <td style="width: 50%">To Date</td>
                        </tr>
                        <tr>
                            <td><asp:Calendar id="dtpFromDate" runat="server" OnSelectionChanged="OnValidateForm"></asp:Calendar></td>
                            <td><asp:Calendar id="dtpToDate" runat="server" OnSelectionChanged="OnValidateForm"></asp:Calendar></td>
                        </tr>
                    </table>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
            <tr><td colspan="3" align="right"><asp:Button ID="btnTrack" runat="server" Width="72px" Height="24px" Text="Track" CommandName="Track" OnCommand="OnCommand" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>
            <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
            <tr>
                <td>&nbsp;</td>
                <td colspan="2">
                    <table border="0px" cellpadding="0px" cellspacing="2px" width="100%">
                        <tr><td><asp:Label ID="msg1Label" runat="server" Text="Pickup date can't be in the future."></asp:Label></td> </tr>
                        <tr><td><asp:Label ID="msg2Label" runat="server" Text="Delivery date can't be more than 7 days in the future."></asp:Label></td></tr>
                        <tr><td><asp:Label ID="msg3Label" runat="server" Text="From date can't be older than 30 Days."></asp:Label></td></tr>
                        <tr><td><asp:Label ID="msg4Label" runat="server" Text="Date range can't be more than 7 days."></asp:Label></td></tr>
                    </table>
                </td>
            </tr>
        </table>
        
    </asp:View>
    <asp:View ID="vwSelectStore" runat="server">
        <!-- Select store view -->
         <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
            <tr><td width="96px">&nbsp;</td><td width="432px">&nbsp;</td><td>&nbsp;</td></tr>
            <tr><td colspan="3">Please select your store...</td></tr>
            <tr><td colspan="3">&nbsp;</td></tr>
            <tr>
                <td align="right">Stores&nbsp;</td>
                <td>
                    <asp:ListBox ID="lstStores" runat="server" Width="100%" Height="192px"></asp:ListBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr><td colspan="3">&nbsp;</td></tr>
            <tr><td colspan="3" align="right">
                <asp:Button ID="btnContinue" runat="server" Width="72px" Height="24px" Text="Continue" CommandName="Continue" OnCommand="OnCommand" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Width="72px" Height="24px" Text="Cancel" CommandName="Cancel" OnCommand="OnCommand" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td></tr>
            <tr><td colspan="3">&nbsp;</td></tr>
        </table>
    </asp:View>
    </asp:MultiView>
</asp:Content>
