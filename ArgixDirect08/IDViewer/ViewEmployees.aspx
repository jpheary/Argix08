<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="ViewEmployees.aspx.cs" Inherits="ViewEmployees" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="body">
    <asp:UpdatePanel ID="upnlPage" runat="server" ChildrenAsTriggers="true" RenderMode="Block" UpdateMode="Conditional" >
    <ContentTemplate>
    <asp:MultiView ID="mvPage" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwEmployees" runat="server">
            <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td style="width:25%"><asp:Button ID="btnDrivers" runat="server" Width="100%" Height="20px" Text="Drivers" BorderStyle="Solid" BorderWidth="1px" style="border-bottom-style:solid" BorderColor="Black" CommandName="Drivers" OnCommand="OnChangeIDType" /></td>
                    <td style="width:25%"><asp:Button ID="btnEmployees" runat="server" Width="100%" Height="20px" Text="Employees" BorderStyle="Solid" BorderWidth="1px" style="border-bottom-style:solid" CommandName="Employees" OnCommand="OnChangeIDType" /></td>
                    <td style="width:25%"><asp:Button ID="btnHelpers" runat="server" Width="100%" Height="20px" Text="Helpers" BorderStyle="Solid" BorderWidth="1px" style="border-bottom-style:solid" CommandName="Helpers" OnCommand="OnChangeIDType" /></td>
                    <td style="width:25%"><asp:Button ID="btnVendors" runat="server" Width="100%" Height="20px" Text="Vendors" BorderStyle="Solid" BorderWidth="1px" style="border-bottom-style:solid" CommandName="Vendors" OnCommand="OnChangeIDType" /></td>
                </tr>
                <tr style="font-size:1px; height:12px"><td colspan="4" style="background-color:White; border-left: solid 1px black; border-right:solid 1px black">&nbsp;</td></tr>
                <tr>
                    <td colspan="4" valign="top" align="center" style="background-color:White; border:solid 1px black; border-top:none">
                        <asp:Panel id="pnlEmployees" runat="server" Width="98%" Height="280px" BorderStyle="None" BorderWidth="1px" ScrollBars="Vertical">
                            <asp:GridView ID="grdEmployees" runat="server" Width="100%" DataSourceID="odsEmployees" DataKeyNames="IDNumber" AutoGenerateColumns="False" AllowSorting="True" OnSelectedIndexChanged="OnEmployeeSelected">
                                <Columns>
                                    <asp:CommandField HeaderStyle-Width="24px" ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" />
                                    <asp:BoundField DataField="IDNumber" HeaderText="ID#" Visible="false" />
                                    <asp:BoundField DataField="BadgeNumber" HeaderText="Badge#" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" SortExpression="BadgeNumber" />
                                    <asp:BoundField DataField="LastName" HeaderText="Last" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" SortExpression="LastName" />
                                    <asp:BoundField DataField="FirstName" HeaderText="First" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true" SortExpression="FirstName" />
                                </Columns>
                            </asp:GridView>
                            <asp:ObjectDataSource ID="odsEmployees" runat="server" TypeName="Argix.KronosProxy" SelectMethod="GetEmployeeList" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="900" >
                                <SelectParameters>
                                    <asp:Parameter Name="idType" DefaultValue="" ConvertEmptyStringToNull="true" Type="string" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwEmployee" runat="server">
            <table border="0px" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td style="width:25%"><asp:Button ID="btnBack" runat="server" Width="100%" Height="20px" Text="<< Back" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" OnCommand="OnChangeView" CommandName="Back" /></td>
                    <td style="width:75%">&nbsp;</td>
                </tr>
            </table>
            <br />
            <table border="0px" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td style="width:25%"><asp:Button ID="btnDetail" runat="server" Width="100%" Height="20px" Text="Detail" BorderStyle="Solid" BorderWidth="1px" style="border-bottom-style:none" OnCommand="OnChangeView" CommandName="Detail" /></td>
                    <td style="width:25%"><asp:Button ID="btnPhoto" runat="server" Width="100%" Height="20px" Text="Photo" BorderStyle="Solid" BorderWidth="1px" style="border-bottom-style:solid" OnCommand="OnChangeView" CommandName="Photo" /></td>
                    <td style="width:50%">&nbsp;</td>
                </tr>
                <tr style="font-size:1px; height:12px"><td colspan="3" style="background-color:White; border-left: solid 1px black; border-right:solid 1px black">&nbsp;</td></tr>
                <tr>
                    <td colspan="3" valign="top" align="center" style="background-color:White; border:solid 1px black; border-top:none">
                        <asp:Panel id="pnlEmployee" runat="server" Width="98%" Height="250px" BorderStyle="None" BorderWidth="1px" ScrollBars="Vertical">
                            <asp:MultiView ID="mvEmployee" runat="server" ActiveViewIndex="0">
                                <asp:View ID="vwDetail" runat="server">
                                    <asp:DetailsView ID="dvEmployee" runat="server" Width="100%" DataSourceID="odsEmployee" AutoGenerateRows="false">
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
                                            <asp:BoundField DataField="StatusDate" HeaderText="StatusDate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" />      
                                            <asp:BoundField DataField="IssueDate" HeaderText="IssueDate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" Visible="false" />      
                                            <asp:BoundField DataField="ExpirationDate" HeaderText="ExpirationDate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" Visible="false" />      
                                            <asp:BoundField DataField="DOB" HeaderText="DOB" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" Visible="false" />      
                                            <asp:BoundField DataField="HireDate" HeaderText="HireDate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" />      
                                            <asp:BoundField DataField="HasPhoto" HeaderText="HasPhoto" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />      
                                            <asp:BoundField DataField="HasSignature" HeaderText="HasSignature" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="false" />      
                                        </Fields>
                                    </asp:DetailsView>
                                </asp:View>
                                <asp:View ID="vwPhoto" runat="server"> 
                                    <asp:Image ID="imgPhoto" runat="server" Height="200px" />
                                    <br />
                                    <asp:Image ID="imgSignature" runat="server" Height="40px" />
                               </asp:View>
                            </asp:MultiView>
                            <asp:ObjectDataSource ID="odsEmployee" runat="server" TypeName="Argix.KronosProxy" SelectMethod="GetEmployee" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="900" >
                                <SelectParameters>
                                    <asp:Parameter Name="idType" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                    <asp:Parameter Name="idNumber" DefaultValue="0" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
