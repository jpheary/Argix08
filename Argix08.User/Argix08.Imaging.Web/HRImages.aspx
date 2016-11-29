<%@ page language="C#" masterpagefile="~/Imaging.master" autoeventwireup="true" enableeventvalidation="false" CodeFile="HRImages.aspx.cs" inherits="_HRImages" title="HR Images Search 2.0.1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel ID="upnlPage" runat="server" RenderMode="Block" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>
        <table width="800px" border="0" cellpadding="0px" cellspacing="0px">
            <tr style="font-size:1px"><td width="120px">&nbsp;</td><td width="680px">&nbsp;</td></tr>
            <tr><td colspan="2"><h2>HR IMAGES</h2></td></tr>
            <tr><td colspan="2" style="font-size:1px; height:3px">&nbsp;</td></tr>
            <tr><td colspan="2">Select document class, field and enter search text. Use % for prefix or postfix wild card for multiple characters and use _ (underscore) for single. (e.g. %12_4%).</td></tr>
            <tr><td colspan="2" style="font-size:1px; height:24px">&nbsp;</td></tr>
            <tr>
                <td align="right">Document Class&nbsp;</td>
                <td>
                    <asp:DropDownList ID="cboDocClass" runat="server" DataSourceID="odsDocs" DataTextField="ClassName" DataValueField="ClassName" Width="131px" AutoPostBack="True" OnSelectedIndexChanged="OnDocClassChanged"></asp:DropDownList>
                    <asp:ObjectDataSource ID="odsDocs" runat="server" TypeName="Argix.Freight.ImagingService" SelectMethod="GetDocumentClasses" CacheExpirationPolicy="Sliding" CacheDuration="900" EnableCaching="true">
                        <SelectParameters>
                            <asp:Parameter Name="Department" DefaultValue="HR" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
            <tr><td colspan="2" style="font-size:1px; height:6px">&nbsp;</td></tr>
            <tr>
                <td align="right">Search Criteria&nbsp;</td>
                <td>
                    <asp:DropDownList ID="cboProp1" runat="server" DataSourceID="odsMetaData" DataTextField="Property" DataValueField="Property" Width="96px"></asp:DropDownList>
                    <asp:TextBox ID="txtSearch1" runat="server" MaxLength="30"></asp:TextBox>
                    <asp:DropDownList ID="cboOperand1" runat="server" Width="72px" >
                        <asp:ListItem Value="AND">AND</asp:ListItem>
                        <asp:ListItem Value="OR">OR</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr><td colspan="2" style="font-size:1px; height:3px">&nbsp;</td></tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:DropDownList ID="cboProp2" runat="server" DataSourceID="odsMetaData" DataTextField="Property" DataValueField="Property" Width="96px"></asp:DropDownList>
                    <asp:TextBox ID="txtSearch2" runat="server" MaxLength="30"></asp:TextBox>
                    <asp:DropDownList ID="cboOperand2" runat="server"  Width="72px" >
                        <asp:ListItem Value="AND">AND</asp:ListItem>
                        <asp:ListItem Value="OR">OR</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr><td colspan="2" style="font-size:1px; height:3px">&nbsp;</td></tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:DropDownList ID="cboProp3" runat="server" DataSourceID="odsMetaData" DataTextField="Property" DataValueField="Property" Width="96px"></asp:DropDownList>
                    <asp:TextBox ID="txtSearch3" runat="server" MaxLength="30"></asp:TextBox>
                </td>
            </tr>
            <tr><td colspan="2" style="font-size:1px; height:12px">&nbsp;</td></tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="OnSearchClicked" />
                    &nbsp;<asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtSearch1" ErrorMessage="Please enter search text in the first text box."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr><td colspan="2" style="font-size:1px; height:12px">&nbsp;</td></tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="pnlImages" runat="server" Width="100%" Height="192px" ScrollBars="Auto">
                        <asp:GridView ID="grdImages" runat="server" Width="100%" Height="100%" AutoGenerateColumns="False" AllowSorting="True" EmptyDataText="No images found." OnSorting="OnGridSorting" OnSorted="OnGridSorted">
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
     </table>
    <asp:ObjectDataSource ID="odsMetaData" runat="server" TypeName="Argix.Freight.ImagingService" SelectMethod="GetMetaData">
        <SelectParameters>
            <asp:ControlParameter Name="className" ControlID="cboDocClass" PropertyName="SelectedValue" Type="string" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="uprgPage" runat="server" AssociatedUpdatePanelID="upnlPage">
        <ProgressTemplate>Searching for images...</ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>