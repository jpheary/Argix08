<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Summary.aspx.cs" Inherits="_Summary" StylesheetTheme="ArgixDirect" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Argix Direct ~ Tracking Summary</title>
    <link href="App_Themes/ArgixDirect/ArgixDirect.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div class="container">
    <div id="topBannerDiv"><img src="App_Themes/ArgixDirect/Images/Argix-Client_banner.jpg"/><br /></div>
    <div class="content">   
        <form id="form1" runat="server">
            <div class="labelContainer">
<asp:Label ID="Label1" runat="server" SkinID="lblTextDetail" Text="Tracking Results Summary" Width="775px" Height="28px" ></asp:Label>
                &nbsp;
                <br />
                <asp:GridView ID="grdTrack" runat="server" AutoGenerateColumns="False"  Font-Bold="False" Font-Size="14pt" Height="96px"  SkinID="GridSkin" Width="775px" CellPadding="5">
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="ItemNumber" DataTextField="ItemNumber" 
                            HeaderText="Tracking Number" DataNavigateUrlFormatString="Detail.aspx?ID={0}" NavigateUrl="~/Detail.aspx">
                            <ItemStyle HorizontalAlign="Left" Width="150px" Font-Bold="True" />
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="Status" HeaderText="Status">
                            <ItemStyle Width="225px" HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ShipDate" HeaderText="Date" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="75px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="LocationName" HeaderText="Location">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="125px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ConsigneeName" HeaderText="Consignee">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="165px" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle Font-Size="10pt" />
                </asp:GridView>
            </div>   

            <div id="buttonContainer" >
                <asp:LinkButton ID="btnTrackNew" SkinID="linkBtn" OnClick="OnTrack" runat="server" >Track new shipment</asp:LinkButton><asp:ImageButton ID="btnImgTrackNew" runat="server" OnClick="OnTrack"  ImageUrl="~/App_Themes/ArgixDirect/Images/btn_arrow_red_right.gif" ImageAlign="AbsBottom" /> 
            </div>     
        </form>
        <div id="labelContainer">
            <asp:Label ID="lblSumFootnote" runat="server" SkinID="lblText" Text="Tracking data provided by Argix Direct." Width="384px"></asp:Label>
        </div>
    </div>
</div>
</body>
</html>
