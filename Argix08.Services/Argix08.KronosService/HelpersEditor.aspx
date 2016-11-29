<%@ Page Language="C#" masterpagefile="~/Default.master" AutoEventWireup="true" CodeFile="HelpersEditor.aspx.cs" Inherits="HelpersEditor" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <table width="100%" border="0" cellpadding="0px" cellspacing="0px">
        <tr style="font-size:1px"><td width="24px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
                    <tr style="height:18px"><td style="font-size:1.0em; vertical-align:middle; padding-left:6px; background-image:url(App_Themes/Argix/Images/gridtitle.gif); background-repeat:repeat-x;">&nbsp;<bold>Helpers</bold></td></tr>
                    <tr>
                        <td valign="top">
                            <asp:Panel id="pnlEmployees" runat="server" Width="100%" Height="384px" BorderStyle="Inset" BorderWidth="1px" ScrollBars="Auto">
                                <asp:UpdatePanel ID="upnlEmployees" runat="server" UpdateMode="Conditional" >
                                <ContentTemplate>
                                    <asp:GridView ID="grdEmployees" runat="server" width="100%" AutoGenerateColumns="False" DataSourceID="odsEmployees" DataKeyNames="IDNumber" AllowSorting="true" OnSelectedIndexChanged="OnEmployeeSelected">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" HeaderStyle-Width="16px" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" ShowSelectButton="True" />
						                    <asp:BoundField DataField="IDNumber" HeaderText="ID" HeaderStyle-Width="48px" SortExpression="IDNumber" />
						                    <asp:BoundField DataField="LastName" HeaderText="Last" HeaderStyle-Width="72px" SortExpression="LastName" />
						                    <asp:BoundField DataField="FirstName" HeaderText="First" HeaderStyle-Width="72px" SortExpression="FirstName" />
						                    <asp:BoundField DataField="Middle" HeaderText="Mid" HeaderStyle-Width="72px" />
						                    <asp:BoundField DataField="Suffix" HeaderText="Suffix" HeaderStyle-Width="72px" />
						                    <asp:BoundField DataField="Organization" HeaderText="Org" HeaderStyle-Width="72px" SortExpression="Organization" />
						                    <asp:BoundField DataField="Department" HeaderText="Dept" HeaderStyle-Width="72px" SortExpression="Department" />
						                    <asp:BoundField DataField="Faccode" HeaderText="Faccode" HeaderStyle-Width="72px" />
						                    <asp:BoundField DataField="Location" HeaderText="Loc" HeaderStyle-Width="72px" />
						                    <asp:BoundField DataField="SubLocation" HeaderText="Sub Loc" HeaderStyle-Width="72px" />
						                    <asp:BoundField DataField="EmployeeID" HeaderText="Employee#" HeaderStyle-Width="72px" />
						                    <asp:BoundField DataField="BadgeNumber" HeaderText="Badge#" HeaderStyle-Width="72px" />
						                    <asp:BoundField DataField="Photo" HeaderText="Pic" HeaderStyle-Width="72px" Visible="false" />
						                    <asp:BoundField DataField="Signature" HeaderText="Sig" HeaderStyle-Width="72px" Visible="false" />
						                    <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-Width="72px" />
						                    <asp:BoundField DataField="StatusDate" HeaderText="Status Date" HeaderStyle-Width="120px" HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" />
						                    <asp:BoundField DataField="IssueDate" HeaderText="Issued" HeaderStyle-Width="120px" HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" />
						                    <asp:BoundField DataField="ExpirationDate" HeaderText="Expires" HeaderStyle-Width="120px" HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" />
						                    <asp:BoundField DataField="DOB" HeaderText="Birth" HeaderStyle-Width="120px" HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" />
						                    <asp:BoundField DataField="HireDate" HeaderText="Hired" HeaderStyle-Width="120px" HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" />
						                    <asp:BoundField DataField="HasPhoto" HeaderText="Pic?" HeaderStyle-Width="48px" />
						                    <asp:BoundField DataField="HasSignature" HeaderText="Sig?" HeaderStyle-Width="48px" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:ObjectDataSource ID="odsEmployees" runat="server" TypeName="Argix.Kronos" SelectMethod="GetEmployees">
                                        <SelectParameters>
                                            <asp:Parameter Name="idType" DefaultValue="Helpers" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td colspan="2" style="font-size:1px; height:12px">&nbsp;</td></tr>
        <tr>            
            <td>&nbsp;</td>
            <td style="text-align: left;">
                <asp:Button ID="btnAdd" runat="server" Text="Add" width="72px" OnClick="OnAdd"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>