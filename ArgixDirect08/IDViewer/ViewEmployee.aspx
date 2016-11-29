<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="ViewEmployee.aspx.cs" Inherits="ViewEmployee" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="body">
    <asp:UpdatePanel ID="upnlPage" runat="server" ChildrenAsTriggers="true" RenderMode="Block" UpdateMode="Conditional" >
    <ContentTemplate>
    <table width="100%" border="0px" cellpadding="0px" cellspacing="0px" style="background-color:White">
        <tr>
            <td style="width:25%"><asp:Button ID="btnBack" runat="server" Width="100%" Height="20px" Text="<< Back" BorderStyle="Solid" BorderWidth="1px" style="border-bottom-style:solid" BorderColor="Black" OnCommand="OnChangeView" CommandName="Back" /></td>
            <td style="width:25%"><asp:Button ID="btnDetail" runat="server" Width="100%" Height="20px" Text="Detail" BorderStyle="Solid" BorderWidth="1px" style="border-bottom-style:none" OnCommand="OnChangeView" CommandName="Detail" /></td>
            <td style="width:25%"><asp:Button ID="btnPhoto" runat="server" Width="100%" Height="20px" Text="Photo" BorderStyle="Solid" BorderWidth="1px" style="border-bottom-style:solid" OnCommand="OnChangeView" CommandName="Photo" /></td>
            <td style="width:25%"><asp:Button ID="btnSpare" runat="server" Width="100%" Height="20px" Text="" BorderStyle="Solid" BorderWidth="1px" style="border-bottom-style:solid" /></td>
        </tr>
        <tr style="font-size:1px; height:12px"><td colspan="4">&nbsp;</td></tr>
        <tr>
            <td colspan="4" valign="top" align="center">
                <asp:Panel id="pnlEmployee" runat="server" Width="98%" Height="280px" BorderStyle="None" BorderWidth="1px" ScrollBars="Vertical">
                    <asp:MultiView ID="mvEmployee" runat="server" ActiveViewIndex="0">
                        <asp:View ID="vwDetail" runat="server">
                            <asp:DetailsView ID="DetailsView1" runat="server" Width="100%" DataSourceID="odsEmployee" AutoGenerateRows="false">
                                <Fields>
                                    <asp:BoundField DataField="IDNumber" HeaderText="ID#" HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="true" />
                                    <asp:BoundField DataField="BadgeNumber" HeaderText="Badge#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="LastName" HeaderText="Last Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="FirstName" HeaderText="First Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="Middle" HeaderText="Middle" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                    <asp:BoundField DataField="Suffix" HeaderText="Suffix" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                    <asp:BoundField DataField="Organization" HeaderText="Organization" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                    <asp:BoundField DataField="Department" HeaderText="Department" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                    <asp:BoundField DataField="Faccode" HeaderText="Faccode" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                    <asp:BoundField DataField="Location" HeaderText="Location" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                    <asp:BoundField DataField="SubLocation" HeaderText="SubLocation" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                    <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                    <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                    <asp:BoundField DataField="StatusDate" HeaderText="StatusDate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                    <asp:BoundField DataField="IssueDate" HeaderText="IssueDate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                    <asp:BoundField DataField="ExpirationDate" HeaderText="ExpirationDate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                    <asp:BoundField DataField="DOB" HeaderText="DOB" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                    <asp:BoundField DataField="HireDate" HeaderText="HireDate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                    <asp:BoundField DataField="HasPhoto" HeaderText="HasPhoto" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                    <asp:BoundField DataField="HasSignature" HeaderText="HasSignature" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                                </Fields>
                            </asp:DetailsView>
                        </asp:View>
                        <asp:View ID="vwPhoto" runat="server"> 
                            <asp:Image ID="imgPhoto" runat="server" Height="200px" />
                            <br /><br />
                            <asp:Image ID="imgSignature" runat="server" Height="40px" />
                       </asp:View>
                    </asp:MultiView>
                    <asp:ObjectDataSource ID="odsEmployee" runat="server" TypeName="Argix.KronosProxy" SelectMethod="GetEmployee" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="900" >
                        <SelectParameters>
                            <asp:QueryStringParameter Name="idType" QueryStringField="type" />
                            <asp:QueryStringParameter Name="idNumber" QueryStringField="id" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>