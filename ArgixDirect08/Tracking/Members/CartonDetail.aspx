<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="CartonDetail.aspx.cs" Explicit="false" Inherits="CartonDetail" Title="Carton Detail" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cHead" runat="server" ContentPlaceHolderID="cpHead">
    <asp:LinkButton id="lnkHome" runat="server" CausesValidation="False" PostBackUrl="~/Members/Default.aspx" SkinID="lnkNav">Home...</asp:LinkButton>
    &nbsp;<asp:LinkButton id="lnkTracking" runat="server" PostBackUrl="~/Members/TrackByCarton.aspx" SkinID="lnkNav">Tracking...</asp:LinkButton>
    &nbsp;<asp:LinkButton id="lnkStoreSummary" runat="server" PostBackUrl="~/Members/StoreSummary.aspx" SkinID="lnkNav">Store Summary...</asp:LinkButton>
    &nbsp;<asp:LinkButton id="lnkSummary" runat="server" PostBackUrl="~/Members/CartonSummary.aspx" SkinID="lnkNav">Carton Summary...</asp:LinkButton>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cpBody" Runat="Server">
	<asp:panel id="cartonPanel" runat="server" Wrap="False">
		<table id="tblDetail" width="100%" border="0px" cellpadding="2px" cellspacing="2px">
			<tr style="font-size:1px"><td width="180px">&nbsp;</td><td width="180px">&nbsp;</td><td width="48px">&nbsp;</td><td width="180px">&nbsp;</td><td width="180px">&nbsp;</td></tr>
            <tr class="HeaderTitle"><td colspan="5"><asp:Label id="lblTitle" runat="server" SkinID="lblTitleText">Carton Detail</asp:Label></td></tr> 
			<tr><td colspan="5" >Store#<asp:Label id="StoreNum" runat="server"></asp:Label>&nbsp;<asp:Label id="StoreName" runat="server"></asp:Label></td></tr>
            <tr><td colspan="5">&nbsp;</td></tr>
            <tr>
		        <td align="right"><asp:label id="CartonLabel" runat="server" Text="Carton" SkinID="lblNote"></asp:label>&nbsp;</td>
		        <td><asp:DropDownList id="cboCartons" runat="server" Width="192px" AutoPostBack="True" OnSelectedIndexChanged="OnSelectedCartonChanged" Visible="false"></asp:DropDownList>&nbsp;</td>
				<td>&nbsp;</td>
				<td colspan="2" align="left">
				    <table width="96px" border="0px" cellpadding="0px" cellspacing="0px">
				        <tr>
				            <td><asp:ImageButton ID="btnNavFirst" runat="server" ImageUrl="~/App_Themes/ArgixDirect/Images/first.gif" ToolTip="Go to first carton" CommandName="First" OnCommand="OnNavigate" /></td>
				            <td><asp:ImageButton ID="btnNavPrev" runat="server" ImageUrl="~/App_Themes/ArgixDirect/Images/prev.gif" ToolTip="Go to previous carton" CommandName="Prev" OnCommand="OnNavigate" /></td>
				            <td>&nbsp;</td>
				            <td><asp:ImageButton ID="btnNavNext" runat="server" ImageUrl="~/App_Themes/ArgixDirect/Images/next.gif" ToolTip="Go to next carton" CommandName="Next" OnCommand="OnNavigate" /></td>
				            <td><asp:ImageButton ID="btnNavLast" runat="server" ImageUrl="~/App_Themes/ArgixDirect/Images/last.gif" ToolTip="Go to last carton" CommandName="Last" OnCommand="OnNavigate" /></td>
				        </tr>
				    </table>
				</td>
	        </tr>
            <tr><td colspan="5">&nbsp;</td></tr>
            <tr>
				<td align="right"><asp:Label ID="Label2" runat="server" Text="Carton #" SkinID="lblNote"></asp:Label>&nbsp;</td>
				<td><asp:Label id="CartonNumberValue" runat="server"></asp:Label></td>
				<td>&nbsp;</td>
				<td align="right"><asp:Label ID="Label7" runat="server" Text="Bill of Lading #" SkinID="lblNote"></asp:Label>&nbsp;</td>
				<td><asp:Label id="BOLValue" runat="server"></asp:Label></td>
			</tr> 
			<tr>
				<td align="right"><asp:Label ID="Label3" runat="server" Text="Client" SkinID="lblNote"></asp:Label>&nbsp;</td>
				<td><asp:Label id="ClientNameValue" runat="server"></asp:Label></td>
				<td>&nbsp;</td>
                <td align="right"><asp:Label ID="Label8" runat="server" Text="TL #" SkinID="lblNote"></asp:Label>&nbsp;</td>
				<td><asp:Label id="TLValue" runat="server"></asp:Label></td>
			</tr> 
			<tr>
				<td align="right"><asp:Label ID="Label4" runat="server" Text="DC/Vendor" SkinID="lblNote"></asp:Label>&nbsp;</td>
				<td><asp:Label id="VendorNameValue" runat="server"></asp:Label></td>
				<td>&nbsp;</td>
				<td align="right"><asp:Label ID="Label9" runat="server" Text="Label Sequence #" SkinID="lblNote"></asp:Label>&nbsp;</td>
				<td><asp:Label id="LabelSeqValue" runat="server"></asp:Label></td>
			</tr> 
			<tr>
				<td align="right"><asp:Label ID="Label5" runat="server" Text="Pickup Date" SkinID="lblNote"></asp:Label>&nbsp;</td>
				<td><asp:Label id="PickupDateValue" runat="server"></asp:Label></td>
				<td>&nbsp;</td>
				<td align="right"><asp:Label ID="Label10" runat="server" Text="Purchase Order #" SkinID="lblNote"></asp:Label>&nbsp;</td>
				<td><asp:Label id="PONumberValue" runat="server"></asp:Label></td>
			</tr> 
			<tr>
				<td align="right"><asp:Label ID="Label6" runat="server" Text="Scheduled for Delivery" SkinID="lblNote"></asp:Label>&nbsp;</td>
				<td><asp:Label id="DeliveryValue" runat="server"></asp:Label></td>
				<td>&nbsp;</td>
				<td align="right"><asp:Label ID="Label11" runat="server" Text="Weight" SkinID="lblNote"></asp:Label>&nbsp;</td>
				<td><asp:Label id="WeightValue" runat="server"></asp:Label></td>
			</tr> 
			<tr>
				<td align="right"><asp:Label ID="Label15" runat="server" Text="Shipment #" SkinID="lblNote"></asp:Label>&nbsp;</td>
				<td><asp:Label ID="ShipmentNumber" runat="server"></asp:Label>&nbsp;</td>
				<td>&nbsp;</td>
				<td align="right">&nbsp;</td>
				<td>&nbsp;</td>
			</tr> 
		</table>
		<br /><br />
		<table id="StatusTable" width="100%" border="1px" cellpadding="2px" cellspacing="1px" style="border-color:Silver; ">
            <tr class="HeaderTitle">
				<td width="192px"><asp:Label ID="Label12" runat="server" SkinID="lblTitleText" Text="Date/Time"></asp:Label></td>
				<td width="288px"><asp:Label ID="Label13" runat="server" SkinID="lblTitleText" Text="Status"></asp:Label></td>
				<td><asp:Label ID="Label14" runat="server" SkinID="lblTitleText" Text="Location"></asp:Label></td>
			</tr>
			<tr>
				<td><asp:Label id="TDSDate" runat="server"></asp:Label></td>
				<td><asp:Label id="TDSStatus" runat="server"></asp:Label></td>
				<td><asp:Label id="TDSLocation" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td><asp:Label id="DepartureDate" runat="server"></asp:Label></td>
				<td><asp:Label id="DepartureStatus" runat="server"></asp:Label></td>
				<td><asp:Label id="DepartureLocation" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td><asp:Label id="ArrivalDate" runat="server"></asp:Label></td>
				<td><asp:Label id="ArrivalStatus" runat="server"></asp:Label></td>
				<td><asp:Label id="ArrivalLocation" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td><asp:Label id="StoreDeliveryDate" runat="server"></asp:Label></td>
				<td><asp:Label id="StoreDeliveryStatus" runat="server"></asp:Label></td>
				<td><asp:Label id="StoreDeliveryLocation" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td><asp:Label id="PODDate" runat="server"></asp:Label></td>
				<td><asp:Label id="PODStatus" runat="server"></asp:Label></td>
				<td><asp:Label id="PODLocation" runat="server"></asp:Label></td>
			</tr>
		</table>
		<br /><br />
		<table id="Footer" width="100%" border="0" cellspacing="0" cellpadding="3">
			<tr style="font-size:1px;"><td width="96px">&nbsp;</td><td width="96px">&nbsp;</td><td width="240px"><td width="168px">&nbsp;</td><td width="168px">&nbsp;</td></tr>
			<tr>
				<td colspan="2"></td>
				<td>&nbsp;</td>
				<td align="right"><asp:LinkButton id="lnkPODReq" runat="server" Visible="True" SkinID="lnkNav" OnClick="OnPODRequest">POD Request</asp:LinkButton></td>
				<td align="right"><asp:HyperLink  id="lnkFileClaim" runat="server" Visible="True" SkinID="hlnkNav" Target="_blank">File Claim</asp:HyperLink></td>
			</tr>
	    </table>
	</asp:panel>
</asp:Content>

