<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="ViewSearch.aspx.cs" Inherits="ViewSearch" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <asp:UpdatePanel ID="upnlSearch" runat="server" ChildrenAsTriggers="true" RenderMode="Block" UpdateMode="Conditional" >
    <ContentTemplate>
    <asp:MultiView ID="mvPage" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwSearch" runat="server">
            <table width="100%" border="0px" cellpadding="2px" cellspacing="0px">
                <tr>
                    <td align="right">ID Type&nbsp;</td>
                    <td>
                        <asp:DropDownList ID="cboType" runat="server" Width="144px" DataSourceID="odsTypes" ToolTip="ID Type" AutoPostBack="true" OnSelectedIndexChanged="OnIDTypeChanged" ></asp:DropDownList>
                        <asp:ObjectDataSource ID="odsTypes" runat="server" SelectMethod="GetIDTypes" TypeName="Argix.KronosProxy" CacheExpirationPolicy="Absolute" CacheDuration="900" EnableCaching="true" />
                    </td>
                </tr>
                <tr style="height:24px"><td align="right">Last Name&nbsp;</td><td><asp:TextBox ID="txtLastName" runat="server" Width="144px" ></asp:TextBox></td></tr>
                <tr style="height:24px"><td align="right">First Name&nbsp;</td><td><asp:TextBox ID="txtFirstName" runat="server" Width="96px"></asp:TextBox></td></tr>
                <tr style="height:24px"><td align="right">Location&nbsp;</td><td><asp:TextBox ID="txtLocation" runat="server" Width="120px"></asp:TextBox></td></tr>
                <tr style="height:24px"><td align="right">Badge#&nbsp;</td><td><asp:TextBox ID="txtBadgeNumber" runat="server" Width="72px"></asp:TextBox></td></tr>
                <tr style="height:24px"><td colspan="2">&nbsp;</td></tr>
                <tr style="height:24px"><td>&nbsp;</td><td><asp:Button ID="btnSearch" runat="server" Width="72px" Height="24px" Text="Search" CommandName="Search" OnCommand="OnChangeView" /></td></tr>
            </table>
        </asp:View>
        <asp:View ID="vwEmployees" runat="server">
            <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td style="width:25%"><asp:Button ID="btnReset" runat="server" Width="100%" Height="20px" Text="<< Search" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" OnCommand="OnChangeView" CommandName="Reset" /></td>
                    <td style="width:75%">&nbsp;</td>
                </tr>
            </table>
            <br />
            <table width="100%" border="0px" cellpadding="0px" cellspacing="0px" style="background-color:White">
                <tr>
                    <td colspan="4" valign="top" align="center">
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
                            <asp:ObjectDataSource ID="odsEmployees" runat="server" TypeName="Argix.KronosProxy" SelectMethod="SearchEmployees">
                                <SelectParameters>
                                    <asp:Parameter Name="idType" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                    <asp:ControlParameter Name="lastName" ControlID="txtLastName" PropertyName="Text" ConvertEmptyStringToNull="true" Type="String" />
                                    <asp:ControlParameter Name="firstName" ControlID="txtFirstName" PropertyName="Text" ConvertEmptyStringToNull="true" Type="String" />
                                    <asp:ControlParameter Name="location" ControlID="txtLocation" PropertyName="Text" ConvertEmptyStringToNull="true" Type="String" />
                                    <asp:ControlParameter Name="badgeNumber" ControlID="txtBadgeNumber" PropertyName="Text" ConvertEmptyStringToNull="true" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>        
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="vwEmployee" runat="server">
            <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td style="width:25%"><asp:Button ID="btnBack" runat="server" Width="100%" Height="20px" Text="<< Back" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" OnCommand="OnChangeView" CommandName="Back" /></td>
                    <td style="width:75%">&nbsp;</td>
                </tr>
            </table>
            <br />
            <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
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

