<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="CartonSearch.aspx.cs" Inherits="CartonSearch" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0" cellspacing="3px">
        <tr style="font-size:1px"><td width="96px">&nbsp;</td><td width="288px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td colspan="2">
                <asp:UpdatePanel ID="upnlHeader" runat="server" UpdateMode="Always" >
                <ContentTemplate>
                   <asp:DropDownList ID="cboSearchBy" runat="server" width="144px" AutoPostBack="True" OnSelectedIndexChanged="OnSearchByChanged">
                        <asp:ListItem Text="Carton #"  Value="ByCarton" Selected="True" />
                        <asp:ListItem Text="Label Sequence #" Value="ByLabel" />
                    </asp:DropDownList>
                    &nbsp;
                    <asp:TextBox ID="txtSearchNo" runat="server" Width="240px" Height="16px" AutoPostBack="True" OnTextChanged="OnValidateForm"></asp:TextBox>
                </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td colspan="3">
                <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
                    <tr style="height:16px">
                        <td style="font-size:1.0em; vertical-align:middle; padding-left:6px; background-image:url(../App_Themes/Reports/Images/gridtitle.gif); background-repeat:repeat-x;"><asp:Image ID="imgClients" runat="server" ImageUrl="~/App_Themes/Reports/Images/clients.gif" ImageAlign="Middle" />&nbsp;Clients</td>
                        <td align="right" style="font-size:1.0em; vertical-align:middle; padding-left:6px; background-image:url(../App_Themes/Reports/Images/gridtitle.gif); background-repeat:repeat-x;">
                            <asp:UpdatePanel ID="upnlClientsHeader" runat="server" UpdateMode="Always" >
                            <ContentTemplate>
                                <asp:TextBox ID="txtFindClient" runat="server" Width="72px" BorderStyle="Inset" BorderWidth="1px" ToolTip="Enter a client number... <press Enter>" AutoPostBack="True" OnTextChanged="OnClientSearch"></asp:TextBox>
                                <asp:ImageButton ID="imgFindClient" runat="server" Height="16px" ImageAlign="Middle" ImageUrl="~/App_Themes/Reports/Images/findreplace.gif" ToolTip="Search for a client..." OnClick="OnFindClient" />&nbsp;
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" valign="top">
                            <asp:Panel id="pnlClients" runat="server" Width="100%" Height="380px" BorderStyle="none" ScrollBars="Auto">
                                <asp:UpdatePanel ID="upnlClients" runat="server" UpdateMode="Conditional" >
                                <ContentTemplate>
                                <asp:GridView ID="grdClients" runat="server" Width="100%" DataSourceID="odsClients" AutoGenerateColumns="False" AllowSorting="True" AutoGenerateCheckBoxColumn="True" CheckAllCheckBoxVisible="False" OnSelectedIndexChanged="OnClientSelected">
                                    <Columns>
                                        <asp:CommandField ButtonType="Image" HeaderStyle-Width="16px" SelectImageUrl="~/App_Themes/Reports/Images/select.gif" SelectText="" ShowSelectButton="True" ShowCancelButton="False" />
                                        <asp:BoundField DataField="ClientNumber" HeaderText="Num" HeaderStyle-Width="24px" SortExpression="ClientNumber" />
                                        <asp:BoundField DataField="DivisionNumber" HeaderText="Div" HeaderStyle-Width="0px" SortExpression="DivisionNumber" Visible="False" />
                                        <asp:BoundField DataField="ClientName" HeaderText="Name" SortExpression="ClientName" HtmlEncode="False" />
                                        <asp:BoundField DataField="TerminalCode" HeaderText="Terminal" HeaderStyle-Width="48px" SortExpression="TerminalCode" />
                                    </Columns>
                                </asp:GridView>
                                <asp:ObjectDataSource ID="odsClients" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetClients" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="600">
                                    <SelectParameters>
                                        <asp:Parameter Name="number" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                        <asp:Parameter Name="division" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                        <asp:Parameter Name="activeOnly" DefaultValue="True" Type="Boolean" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                               </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtFindClient" EventName="TextChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="imgFindClient" EventName="Click" />
                                </Triggers>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
   </table>
</asp:Panel>
<script type="text/javascript" language="javascript">
    function scroll(gridname, panelname, number) {
        var grd = document.getElementById(gridname);
        for(var i=1; i<grd.rows.length; i++) {
            var cell = grd.rows[i].cells[1];
            if(cell.innerHTML.substr(0, number.length) == number) {
                var pnl = document.getElementById(panelname);
                pnl.scrollTop = i * (grd.clientHeight / grd.rows.length);
                break;
            }
        }
    }
</script>
</asp:Content>

