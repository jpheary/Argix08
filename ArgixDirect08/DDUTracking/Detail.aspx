<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="_Detail" StylesheetTheme="ArgixDirect" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Argix Direct ~ Tracking Detail</title>
    <link href="App_Themes/ArgixDirect/ArgixDirect.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div class="container">
    <div id="topBannerDiv"><img src="App_Themes/ArgixDirect/Images/Argix-Client_banner.jpg" alt=""/><br /></div>
    <div class="content"> 
    <form id="form1" runat="server">
       <div id="detailHeadContainer"  > 
       <asp:Label ID="lblDetail_ID" runat="server" SkinID="lblTextDetail" Text="Package status for: " Width="600px" Height="28px" BackColor="White"></asp:Label>
       <asp:Label ID="lblDetail_Status" runat="server" SkinID="lblStatusDetail" Text="(status text)" Width="600px" Height="28px" BackColor="White" ></asp:Label>
       <asp:Label ID="lblDetailSum" runat="server" SkinID="lblDetailSum" Text="Tracking Results Detail" Width="600px" Height="14px"></asp:Label> 
     </div>   <!--detailHeadContainer--> 

<div class="detailGridContainer">
       <asp:Label ID="Label1" runat="server" SkinID="lblTextDetail" Text="Tracking History" Width="758px" Height="28px" Backcolor="#EEEEEE"></asp:Label>

        <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="False" Backcolor="white" Font-Bold="False" Font-Size="14pt" SkinID="GridSkin"  Width="780px" CellPadding="5" CellSpacing="1" BorderStyle="None" CaptionAlign="Left"  >
            <Columns>
                <asp:BoundField DataField="ItemNumber" HeaderText="Tracking Number" Visible="False" />
                <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="Time" HeaderText="Time"  DataFormatString="{0:hh:mm tt}" HtmlEncode="False">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:BoundField DataField="LocationName" HeaderText="Location" />
            </Columns>            
            <HeaderStyle Font-Size="10pt" />
        </asp:GridView>
        <br />
        <br />
 </div>  <!--  detailGridContainer-->
<p></p>

   
  <asp:Panel ID="detailPanel" runat="server" Height="160px" Width="790px" >
  <div class="detailShipmentContainer">        <!--         -->
     <table  class="detailShipmentTable">
        <tr>   <td class="headerBlock" colspan="3" style="height: 34px">  &nbsp;Shipment Details
        </td>         </tr>
         <tr class="rowHeader">
             <td class="detailHeader" style=" height: 16px; width: 220px;">From             </td>
             <td class="detailHeader" style="height: 16px; width: 220px;">To                 </td>
             <td class="detailHeader" style="height: 16px; width: 327px;">Shipment Information  </td>
         </tr>
         <tr>
             <td class="detailCell" style="height: 97px">             
                 <asp:Label ID="lblFromInfo" CssClass="lineBreak" runat="server" BackColor="White" Height="93px" Width="220px"></asp:Label></td>
             <td class="detailCell" style="height: 97px">             
                 <asp:Label ID="lblToInfo" CssClass="lineBreak" runat="server" BackColor="White" Height="93px" Width="220px"></asp:Label></td>
             <td class="detailCell" style="height: 97px; width: 327px;" >             
                 <asp:Label ID="lbShipInfo" CssClass="lineBreak" runat="server" BackColor="White" Height="93px" Width="320px"></asp:Label></td>
         </tr>
     </table>
  </div>      	 <!-- #detailShipmentContainer       9/30/09 Turned div back on to fix positioning for IE8.   -->
  </asp:Panel>
<p></p>

  <div id="buttonContainerLeft" >
    <asp:LinkButton ID="btnGoSummary" SkinID="linkBtn" OnClick="GoSummary" runat="server" >Return to summary</asp:LinkButton>
    <asp:ImageButton ID="btnImgGoSummary" runat="server" OnClick="GoSummaryImg"  ImageUrl="~/App_Themes/ArgixDirect/Images/btn_arrow_red_right.gif" ImageAlign="AbsBottom" /> 
   </div> 

   <div id="buttonContainer" >
    <asp:LinkButton ID="btnTrackNew" SkinID="linkBtn" OnClick="TrackNew" runat="server" >Track new shipment</asp:LinkButton>
    <asp:ImageButton ID="btnImgTrackNew" runat="server" OnClick="TrackNewImg"  ImageUrl="~/App_Themes/ArgixDirect/Images/btn_arrow_red_right.gif" ImageAlign="AbsBottom" /> 
   </div>
   
   <div id="labelContainer" >
       <asp:Label ID="lblDetailFootnote" runat="server" SkinID="lblText" Text="Tracking data provided by Argix Direct as of  " Width="396px"></asp:Label> <!-- date/time appended in CS...   -->
    </div>
  
   </form>
   </div>        <!-- #content -->
 </div>		<!-- #container -->
 </body>
</html>