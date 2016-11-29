<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="LabelStation.aspx.cs" Inherits="LabelStation" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0px" cellspacing="3px">
        <tr style="font-size:1px"><td width="72px">&nbsp;</td><td width="288px">&nbsp;</td><td>&nbsp;</td></tr>
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
                            <asp:UpdatePanel ID="upnlClients" runat="server" UpdateMode="Conditional" >
                            <ContentTemplate>
                                <asp:Panel ID="pnlClients" runat="server" Width="100%" Height="138px" BorderStyle="Inset" BorderWidth="1px" ScrollBars="Auto">
                                    <asp:GridView ID="grdClients" runat="server" Width="100%" AutoGenerateColumns="False" AllowSorting="True" DataSourceID="odsClients" DataKeyNames="ClientNumber,TerminalCode" OnSelectedIndexChanged="OnClientSelected">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" HeaderStyle-Width="16px" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Reports/Images/select.gif" />
                                            <asp:BoundField DataField="ClientNumber" HeaderText="Num" HeaderStyle-Width="48px" SortExpression="ClientNumber" />
                                            <asp:BoundField DataField="ClientName" HeaderText="Name" SortExpression="ClientName" HtmlEncode="False" />
                                            <asp:BoundField DataField="TerminalCode" HeaderText="Term" HeaderStyle-Width="48px" SortExpression="TerminalCode" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:ObjectDataSource ID="odsClients" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetClients" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="900">
                                        <SelectParameters>
                                            <asp:Parameter Name="number" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                            <asp:Parameter Name="division" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                            <asp:Parameter Name="activeOnly" DefaultValue="True" Type="Boolean" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </asp:Panel>
                           </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtFindClient" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="imgFindClient" EventName="Click" />
                            </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
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
                    &nbsp;<asp:ImageButton ID="btnFind" runat="server" ImageUrl="~/App_Themes/Reports/Images/findreplace.gif" BorderStyle="Outset" BorderWidth="2px" Height="16px" ImageAlign="Middle" ToolTip="Search for Argix cartons" OnClick="OnFindCartons" />
                </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td colspan="3">
                <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
                    <tr style="height:16px"><td style="font-size:1.0em; vertical-align:middle; padding-left:6px; background-image:url(../App_Themes/Reports/Images/gridtitle.gif); background-repeat:repeat-x;">Cartons Found</td></tr>
                    <tr>
                        <td valign="top">
                            <asp:UpdatePanel ID="upnlCartons" runat="server" UpdateMode="Conditional" >
                            <ContentTemplate>
                                <asp:Panel ID="pnlCartons" runat="server" Width="100%" Height="192px" BorderStyle="Inset" BorderWidth="1px" ScrollBars="Horizontal">
                                    <asp:GridView ID="grdCartons" runat="server" Width="100%" DataSourceID="odsCartons" DataKeyNames="LabelSeqNumber" AutoGenerateColumns="False" AllowPaging="false" AllowSorting="True" OnSelectedIndexChanged="OnCartonSelected">
                                        <Columns>
                                            <asp:CommandField ButtonType="Image" SelectImageUrl="~/App_Themes/Reports/Images/select.gif" HeaderStyle-Width="12px" ShowSelectButton="True" />
                                            <asp:BoundField DataField="LabelSeqNumber" HeaderText="LabelSeq#" HeaderStyle-Width="120px" SortExpression="LabelSeqNumber" />
                                            <asp:BoundField DataField="Zone" HeaderText="Zone" HeaderStyle-Width="60px" SortExpression="Zone" />
                                            <asp:BoundField DataField="TLNumber" HeaderText="TL#" HeaderStyle-Width="60px" SortExpression="TLNumber" />
                                            <asp:BoundField DataField="ClientID" HeaderText="Client#" HeaderStyle-Width="60px" SortExpression="ClientID" />
                                            <asp:BoundField DataField="ClientName" HeaderText="Client" HeaderStyle-Width="192px" SortExpression="ClientName" />
                                            <asp:BoundField DataField="PickupDate" HeaderText="Pickup" HeaderStyle-Width="96px" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False" SortExpression="PickupDate" />
                                            <asp:BoundField DataField="PickupNumber" HeaderText="#" HeaderStyle-Width="24px" SortExpression="PickupNumber" />
                                            <asp:BoundField DataField="ManifestNumber" HeaderText="Manifest#" HeaderStyle-Width="72px" SortExpression="ManifestNumber" />
                                            <asp:BoundField DataField="ShipFromLocationID" HeaderText="ShipFrom#" HeaderStyle-Width="60px" SortExpression="ShipFromLocationID" />
                                            <asp:BoundField DataField="ShipFromLocationName" HeaderText="ShipFrom" HeaderStyle-Width="192px" SortExpression="ShipFromLocationName" />
                                            <asp:BoundField DataField="ShipToLocationID" HeaderText="ShipTo#" HeaderStyle-Width="60px" SortExpression="ShipToLocationID" />
                                            <asp:BoundField DataField="ShipToLocationName" HeaderText="ShipTo" HeaderStyle-Width="192px" SortExpression="ShipToLocationName" />
                                            <asp:BoundField DataField="Weight" HeaderText="Weight" HeaderStyle-Width="48px" />
                                            <asp:BoundField DataField="FreightType" HeaderText="Freight" HeaderStyle-Width="60px" SortExpression="FreightType" />
                                            <asp:BoundField DataField="SortCenterNumber" HeaderText="Sort Center#" HeaderStyle-Width="60px" SortExpression="SortCenterNumber" />
                                            <asp:BoundField DataField="SortCenterName" HeaderText="Sort Center" HeaderStyle-Width="144px" SortExpression="SortCenterName" />
                                            <asp:BoundField DataField="DamageCode" HeaderText="Damage Code" HeaderStyle-Width="60px" SortExpression="DamageCode" />
                                            <asp:BoundField DataField="DamageDescription" HeaderText="Damage" HeaderStyle-Width="96px" />
                                            <asp:BoundField DataField="SortDate" HeaderText="Sort Date" HeaderStyle-Width="96px" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False" SortExpression="SortDate" />
                                            <asp:BoundField DataField="SortTime" HeaderText="Sort Time" HeaderStyle-Width="48px" DataFormatString="{0:hh:mm}" HtmlEncode="False" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:ObjectDataSource ID="odsCartons" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetCartons" EnableCaching="false" >
                                        <SelectParameters>
                                            <asp:ControlParameter Name="cartonNumber" ControlID="txtSearchNo" PropertyName="Text" ConvertEmptyStringToNull="true" Type="string" />
                                            <asp:ControlParameter Name="terminalCode" ControlID="grdClients" PropertyName="SelectedDataKey.Values[1]" Type="string" />
                                            <asp:ControlParameter Name="clientNumber" ControlID="grdClients" PropertyName="SelectedDataKey.Values[0]" Type="string" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="grdClients" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="txtSearchNo" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="btnFind" EventName="Click" />
                            </Triggers>
                            </asp:UpdatePanel>
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

