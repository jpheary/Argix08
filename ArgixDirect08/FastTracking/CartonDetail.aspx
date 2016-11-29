<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="CartonDetail.aspx.cs" Inherits="CartonDetail" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="cBody" ContentPlaceHolderID="cpBody" Runat="Server">
<div id="detailHeadContainer"  > 
    <asp:Label ID="lblDetail_ID" runat="server" SkinID="lblTextDetail" Text="Package status for: " Width="600px" Height="28px" BackColor="White"></asp:Label>
    <asp:Label ID="lblDetail_Status" runat="server" SkinID="lblStatusDetail" Text="(status text)" Width="600px" Height="28px" BackColor="White" ></asp:Label>
    <asp:Label ID="lblDetailSum" runat="server" SkinID="lblDetailSum" Text="Tracking Results Detail" Width="600px" Height="14px"></asp:Label> 
</div>
<div class="detailGridContainer">
    <asp:Label ID="Label1" runat="server" Width="758px" Height="34px" Text="Tracking History" Backcolor="#EEEEEE" SkinID="lblTextDetail"></asp:Label>
    <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="False" Backcolor="white" Font-Bold="False" Font-Size="14pt" SkinID="GridSkin"  Width="780px" CellPadding="5" CellSpacing="1" BorderStyle="None" CaptionAlign="Left"  >
        <Columns>
            <asp:BoundField DataField="ItemNumber" HeaderText="Tracking Number" Visible="False" />
            <asp:BoundField DataField="DateTime" HeaderText="Date" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False">
                <ItemStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="Status" HeaderText="Status" />
            <asp:BoundField DataField="Location" HeaderText="Location" />
        </Columns>            
        <HeaderStyle Font-Size="10pt" />
    </asp:GridView>
    <br />
</div>
<asp:Panel ID="detailPanel" runat="server" Height="160px" Width="790px" >
    <div class="detailShipmentContainer">
        <table  class="detailShipmentTable">
            <tr><td class="headerBlock" colspan="3" style="height: 34px">&nbsp;Shipment Details</td></tr>
            <tr class="rowHeader" style="height:16px">
                 <td class="detailHeader" style="width:220px;">From             </td>
                 <td class="detailHeader" style="width:220px;">To                 </td>
                 <td class="detailHeader" style="width:327px;">Shipment Information  </td>
            </tr>
            <tr><td colspan="3" style="font-size:1px; height:6px">&nbsp;</td></tr>
            <tr style="height: 90px">
                <td valign="top" class="detailCell">
                    <asp:Label ID="lblFromInfo" CssClass="lineBreak" runat="server" BackColor="White" Width="288px"></asp:Label>
                </td>
                <td valign="top" class="detailCell">
                    <asp:Label ID="lblToInfo" CssClass="lineBreak" runat="server" BackColor="White" Width="288px"></asp:Label>
                </td>
                <td valign="top" class="detailCell">
                    <asp:Label ID="lbShipInfo" CssClass="lineBreak" runat="server" BackColor="White" Width="192px"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
</asp:Content>