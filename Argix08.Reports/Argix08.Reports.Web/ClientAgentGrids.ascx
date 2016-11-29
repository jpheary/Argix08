<%@ Control Language="C#" AutoEventWireup="true" ClassName="ClientAgentGrids" CodeFile="ClientAgentGrids.ascx.cs" Inherits="ClientAgentGrids" EnableTheming="true" %>

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
        </asp:TableCell>
        <asp:TableCell Width="12px">&nbsp;</asp:TableCell>
        <asp:TableCell>
            <asp:Table ID="tblAgents" runat="server" Width="100%" Height="100%" BorderStyle="None" BorderWidth="0px" CellPadding="0" CellSpacing="0">
                <asp:TableRow ID="thrAgents" Height="16px">
                    <asp:TableCell SkinID="GridTitle">Agents</asp:TableCell>
                    <asp:TableCell HorizontalAlign="Right" SkinID="GridTitle">
                        <asp:UpdatePanel ID="upnlAgentsHeader" runat="server" UpdateMode="Always" >
                        <ContentTemplate>
                            <asp:TextBox ID="txtFindAgent" runat="server" Width="72px" BorderStyle="Inset" BorderWidth="1px" ToolTip="Enter an agent number... <press Enter>" AutoPostBack="True" OnTextChanged="OnAgentSearch"></asp:TextBox>
                            <asp:ImageButton ID="imgFindAgent" runat="server" Height="16px" ImageAlign="Middle" ImageUrl="~/App_Themes/Reports/Images/findreplace.gif" ToolTip="Search for an agent..." OnClick="OnFindAgent" />&nbsp;
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" VerticalAlign="Top">
                        <asp:Panel ID="pnlAgents" runat="server" Width="100%" Height="100%" BorderStyle="Inset" BorderWidth="1px" ScrollBars="Auto">
                            <asp:UpdatePanel ID="upnlAgents" runat="server" UpdateMode="Conditional" >
                            <ContentTemplate>
                            <asp:GridView ID="grdAgents" runat="server" Width="100%" AutoGenerateColumns="False" AllowSorting="True" DataSourceID="odsAgents" OnSelectedIndexChanged="OnAgentSelected">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" HeaderStyle-Width="16px" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Reports/Images/select.gif" />
                                    <asp:BoundField DataField="AgentNumber" HeaderText="Num" HeaderStyle-Width="60px" SortExpression="AgentNumber" />
                                    <asp:BoundField DataField="AgentName" HeaderText="Name" SortExpression="AgentName" />
                                    <asp:BoundField DataField="MainZone" HeaderText="Zone" HeaderStyle-Width="72px" SortExpression="MainZone" />
                                </Columns>
                            </asp:GridView>
                            <asp:ObjectDataSource ID="odsAgents" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetAgents" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="900" >
                                <SelectParameters>
                                    <asp:Parameter Name="mainZoneOnly" DefaultValue="false" Type="Boolean" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtFindAgent" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="imgFindAgent" EventName="Click" />
                            </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
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
