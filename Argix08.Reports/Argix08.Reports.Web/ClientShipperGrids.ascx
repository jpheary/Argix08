<%@ Control Language="C#" AutoEventWireup="true" ClassName="ClientShipperGrids" CodeFile="ClientShipperGrids.ascx.cs" Inherits="ClientShipperGrids" EnableTheming="true" %>

<asp:Table ID="tblCtl" runat="server" Width="100%" Height="100%" BorderStyle="None" BorderWidth="0px" CellPadding="0" CellSpacing="0">
    <asp:TableRow VerticalAlign="Top">
        <asp:TableCell>
            <asp:Table ID="tblClients" runat="server" Width="100%" Height="100%" BorderStyle="None" BorderWidth="0px" CellPadding="0" CellSpacing="0">
                <asp:TableRow ID="thrClients" Width="100%" Height="16px">
                    <asp:TableCell SkinID="GridTitle"><asp:Image ID="imgClients" runat="server" ImageUrl="~/App_Themes/Reports/Images/clients.gif" ImageAlign="Middle" />&nbsp;Clients</asp:TableCell>
                    <asp:TableCell HorizontalAlign="Right" SkinID="GridTitle">
                        <asp:UpdatePanel ID="upnlClientsHeader" runat="server" UpdateMode="Always" >
                        <ContentTemplate>
                            <asp:TextBox ID="txtFindClient" runat="server" Width="72px" BorderStyle="Inset" BorderWidth="1px" ToolTip="Enter a client number... <press Enter>" AutoPostBack="True" OnTextChanged="OnClientSearch"></asp:TextBox>
                            <asp:ImageButton ID="imgFindClient" runat="server" Height="16px" ImageAlign="Middle" ImageUrl="~/App_Themes/Reports/Images/findreplace.gif" ToolTip="Search for a client..." OnClick="OnFindClient" />&nbsp;
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow Width="100%" >
                    <asp:TableCell ColumnSpan="2" VerticalAlign="Top">
                        <asp:Panel ID="pnlClients" runat="server" Width="100%" Height="100%" BorderStyle="Inset" BorderWidth="1px" ScrollBars="Auto">
                            <asp:UpdatePanel ID="upnlClients" runat="server" UpdateMode="Conditional" >
                            <ContentTemplate>
                            <asp:GridView ID="grdClients" runat="server" Width="100%" AutoGenerateColumns="False" AllowSorting="True" DataSourceID="odsClients" DataKeyNames="ClientNumber,TerminalCode" OnSelectedIndexChanged="OnClientSelected">
                                <Columns>
                                    <asp:CommandField HeaderStyle-Width="16px" ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Reports/Images/select.gif" />
                                    <asp:BoundField DataField="ClientNumber" HeaderText="Num" HeaderStyle-Width="48px" SortExpression="ClientNumber" />
                                    <asp:BoundField DataField="DivisionNumber" HeaderText="Div" HeaderStyle-Width="48px" SortExpression="DivisionNumber" />
                                    <asp:BoundField DataField="ClientName" HeaderText="Name" SortExpression="ClientName" HtmlEncode="False" />
                                    <asp:BoundField DataField="TerminalCode" HeaderText="Term" HeaderStyle-Width="48px" SortExpression="TerminalCode" />
                                </Columns>
                            </asp:GridView>
                            <asp:HiddenField ID="hfClientDivision" runat="server" Value="" />
                            <asp:HiddenField ID="hfClientActiveOnly" runat="server" Value="false" />
                            <asp:ObjectDataSource ID="odsClients" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetClients" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="900">
                                <SelectParameters>
                                    <asp:Parameter Name="number" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                    <asp:ControlParameter Name="division" ControlID="hfClientDivision" PropertyName="Value" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                    <asp:ControlParameter Name="activeOnly" ControlID="hfClientActiveOnly" PropertyName="Value" DefaultValue="False" Type="Boolean" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtFindClient" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="imgFindClient" EventName="Click" />
                            </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:UpdateProgress ID="upgClients" runat="server" AssociatedUpdatePanelID="upnlClients"><ProgressTemplate>updating shippers...</ProgressTemplate></asp:UpdateProgress>
        </asp:TableCell>
        <asp:TableCell Width="12px">&nbsp;</asp:TableCell>
        <asp:TableCell>
            <asp:Table ID="tblShippers" runat="server" Width="100%" Height="100%" BorderStyle="None" BorderWidth="0px" CellPadding="0" CellSpacing="0">
                <asp:TableRow ID="thrShippers" Width="100%" Height="16px">
                    <asp:TableCell SkinID="GridTitle" >
                        <asp:UpdatePanel ID="upnlShippersHeader1" runat="server" UpdateMode="Always" >
                        <ContentTemplate>
                            <asp:DropDownList ID="cboFreightType" runat="server" Width="72px" ToolTip="Shipper" AutoPostBack="True" OnSelectedIndexChanged="OnFreightTypeChanged">
                                <asp:ListItem Text="Vendors" Value="0" Selected="True" />
                                <asp:ListItem Text="Agents" Value="1" />
                            </asp:DropDownList>                    
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Right" SkinID="GridTitle">
                        <asp:UpdatePanel ID="upnlShippersHeader2" runat="server" UpdateMode="Always" >
                        <ContentTemplate>
                            <asp:TextBox ID="txtFindShipper" runat="server" Width="72px" BorderStyle="Inset" BorderWidth="1px" ToolTip="Enter a shipper number... <press Enter>" AutoPostBack="True" OnTextChanged="OnShipperSearch"></asp:TextBox>
                            <asp:ImageButton ID="imgFindShipper" runat="server" Height="16px" ImageAlign="Middle" ImageUrl="~/App_Themes/Reports/Images/findreplace.gif" ToolTip="Search for a shipper..." OnClick="OnFindShipper" />&nbsp;
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow Width="100%">
                    <asp:TableCell ColumnSpan="2" VerticalAlign="Top">
                        <asp:Panel ID="pnlShippers" runat="server" Width="100%" Height="100%" BorderStyle="Inset" BorderWidth="1px" ScrollBars="Auto">
                            <asp:UpdatePanel ID="upnlShippers" runat="server" ChildrenAsTriggers="true" RenderMode="Block" UpdateMode="Conditional" >
                            <ContentTemplate>
                            <asp:GridView ID="grdShippers" runat="server" Width="100%" AutoGenerateColumns="False" AllowSorting="True" DataSourceID="odsShippers" OnSelectedIndexChanged="OnShipperSelected">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" HeaderStyle-Width="16px" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Reports/Images/select.gif" />
                                    <asp:BoundField DataField="ShipperNumber" HeaderText="Num" HeaderStyle-Width="60px" SortExpression="ShipperNumber" />
                                    <asp:BoundField DataField="ShipperName" HeaderText="Name" SortExpression="ShipperName" HtmlEncode="False" />
                                </Columns>
                            </asp:GridView>
                            <asp:ObjectDataSource ID="odsShippers" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetShippers" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="900">
                                <SelectParameters>
                                    <asp:ControlParameter Name="freightType" ControlID="cboFreightType" DefaultValue="0" PropertyName="SelectedValue" Type="Object" />
                                    <asp:ControlParameter Name="clientNumber" ControlID="grdClients" PropertyName="SelectedDataKey.Values[0]" DefaultValue="000" Type="String" />
                                    <asp:ControlParameter Name="clientTerminal" ControlID="grdClients" PropertyName="SelectedDataKey.Values[1]" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="grdClients" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="cboFreightType" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="txtFindShipper" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="imgFindShipper" EventName="Click" />
                            </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:UpdateProgress ID="upgShippers" runat="server" AssociatedUpdatePanelID="upnlShippers"><ProgressTemplate>updating...</ProgressTemplate></asp:UpdateProgress>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
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
