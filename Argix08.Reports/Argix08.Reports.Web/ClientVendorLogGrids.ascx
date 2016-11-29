<%@ Control Language="C#" AutoEventWireup="true" ClassName="ClientVendorLogGrids" CodeFile="ClientVendorLogGrids.ascx.cs" Inherits="ClientVendorLogGrids" EnableTheming="true" %>

<asp:Table ID="tblCtl" runat="server" Width="100%" Height="100%" BorderStyle="None" BorderWidth="0px" CellPadding="0" CellSpacing="0">
    <asp:TableRow VerticalAlign="Top">
        <asp:TableCell>
            <asp:Table ID="tblClients" runat="server" Width="100%" Height="100%" BorderStyle="None" BorderWidth="0px" CellPadding="0" CellSpacing="0">
                <asp:TableRow ID="thrClients" Height="16px">
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
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" VerticalAlign="Top">
                        <asp:Panel ID="pnlClients" runat="server" Width="100%" Height="100%" BorderStyle="Inset" BorderWidth="1px" ScrollBars="Auto">
                            <asp:UpdatePanel ID="upnlClients" runat="server" UpdateMode="Conditional" >
                            <ContentTemplate>
                            <asp:GridView ID="grdClients" runat="server" Width="100%" AutoGenerateColumns="False" AllowSorting="True" DataSourceID="odsClients" DataKeyNames="ClientNumber,DivisionNumber" OnSelectedIndexChanged="OnClientSelected">
                                <Columns>
                                    <asp:CommandField HeaderStyle-Width="16px" ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Reports/Images/select.gif" />
                                    <asp:BoundField DataField="ClientNumber" HeaderText="Num" HeaderStyle-Width="48px" SortExpression="ClientNumber" />
                                    <asp:BoundField DataField="DivisionNumber" HeaderText="Div" HeaderStyle-Width="48px" SortExpression="DivisionNumber" />
                                    <asp:BoundField DataField="ClientName" HeaderText="Name" SortExpression="ClientName" HtmlEncode="False" />
                                </Columns>
                            </asp:GridView>
                            <asp:HiddenField ID="hfClientDivision" runat="server" Value="" />
                            <asp:HiddenField ID="hfClientActiveOnly" runat="server" Value="true" />
                            <asp:ObjectDataSource ID="odsClients" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetClients" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="900">
                                <SelectParameters>
                                    <asp:Parameter Name="number" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                    <asp:ControlParameter Name="division" ControlID="hfClientDivision" PropertyName="Value" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                    <asp:ControlParameter Name="activeOnly" ControlID="hfClientActiveOnly" PropertyName="Value" DefaultValue="True" Type="Boolean" />
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
            <asp:UpdateProgress ID="upgClients" runat="server" AssociatedUpdatePanelID="upnlClients"><ProgressTemplate>updating vendor log entries...</ProgressTemplate></asp:UpdateProgress>
       </asp:TableCell>
        <asp:TableCell Width="12px">&nbsp;</asp:TableCell>
        <asp:TableCell>
            <asp:Table ID="tblVendorLog" runat="server" Width="100%" Height="100%" BorderStyle="None" BorderWidth="0px" CellPadding="0" CellSpacing="0">
                <asp:TableRow ID="thrVendorLog" Height="16px">
                    <asp:TableCell SkinID="GridTitle">Vendor Log</asp:TableCell>
                    <asp:TableCell HorizontalAlign="Right" SkinID="GridTitle">
                        <asp:UpdatePanel ID="upnlVendorLogHeader" runat="server" UpdateMode="Always" >
                        <ContentTemplate>
                            <asp:TextBox ID="txtFindLogEntry" runat="server" Width="72px" BorderStyle="Inset" BorderWidth="1px" ToolTip="Enter a vendor log number... <press Enter>" AutoPostBack="True" OnTextChanged="OnVendorLogSearch"></asp:TextBox>
                            <asp:ImageButton ID="imgFindLogEntry" runat="server" Height="16px" ImageAlign="Middle" ImageUrl="~/App_Themes/Reports/Images/findreplace.gif" ToolTip="Search for a vendor log entry..." OnClick="OnFindVendorLogEntry" />&nbsp;
                        </ContentTemplate>
                        </asp:UpdatePanel>
                   </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" VerticalAlign="Top">
                        <asp:Panel ID="pnlVendorLog" runat="server" Width="100%" Height="100%" BorderStyle="Inset" BorderWidth="1px" ScrollBars="Auto">
                            <asp:UpdatePanel ID="upnlVendorLog" runat="server" UpdateMode="Conditional" >
                            <ContentTemplate>
                            <asp:GridView ID="grdVendorLog" runat="server" Width="100%" AutoGenerateColumns="False" DataSourceID="odsVendorLog" DataKeyNames="ID,PickupDate,PickupNumber" AllowSorting="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="24px" >
                                        <ItemTemplate><asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="OnVendorLogEntrySelected"/></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-Width="84px" SortExpression="ID" />
                                    <asp:BoundField DataField="ShipFrom" HeaderText="Shipper#" HeaderStyle-Width="72px" SortExpression="ShipFrom" />
                                    <asp:BoundField DataField="ShipFromName" HeaderText="Shipper Name" SortExpression="ShipFromName" />
                                    <asp:BoundField DataField="PickupDate" HeaderText="Pickup" HeaderStyle-Width="144px" SortExpression="PickupDate" DataFormatString="{0:MM/dd/yy HH:mm tt}" HtmlEncode="False" />
                                    <asp:BoundField DataField="PickupNumber" HeaderText="#" ItemStyle-Width="48px" SortExpression="PickupNumber" />
                                </Columns>
                            </asp:GridView>
                            <asp:HiddenField ID="hfStartDate" runat="server" Value="" />
                            <asp:HiddenField ID="hfEndDate" runat="server" Value="" />
                            <asp:ObjectDataSource ID="odsVendorLog" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetVendorLog" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="180" >
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="grdClients" Name="client" PropertyName="SelectedDataKey.Values[0]" Type="String" />
                                    <asp:ControlParameter ControlID="grdClients" Name="clientDivision" PropertyName="SelectedDataKey.Values[1]" Type="String" />
                                    <asp:ControlParameter ControlID="hfStartDate" Name="startDate" PropertyName="Value" Type="DateTime" />
                                    <asp:ControlParameter ControlID="hfEndDate" Name="endDate" PropertyName="Value" Type="DateTime" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                           </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="grdClients" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="txtFindLogEntry" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="imgFindLogEntry" EventName="Click" />
                            </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:UpdateProgress ID="upgVendorLog" runat="server" AssociatedUpdatePanelID="upnlVendorLog"><ProgressTemplate>updating...</ProgressTemplate></asp:UpdateProgress>
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
