<%@ Control Language="C#" AutoEventWireup="true" ClassName="ClientDivisionGrids" CodeFile="ClientDivisionGrids.ascx.cs" Inherits="ClientDivisionGrids" EnableTheming="true" %>

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
                                    <asp:BoundField DataField="ClientName" HeaderText="Name" SortExpression="ClientName" HtmlEncode="False" />
                                </Columns>
                            </asp:GridView>
                            <asp:HiddenField ID="hfClientDivision" runat="server" Value="01" />
                            <asp:ObjectDataSource ID="odsClients" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetClients" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="900">
                                <SelectParameters>
                                    <asp:Parameter Name="number" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                    <asp:ControlParameter Name="division" ControlID="hfClientDivision" PropertyName="Value" DefaultValue="01" ConvertEmptyStringToNull="true" Type="String" />
                                    <asp:Parameter Name="activeOnly" DefaultValue="False" Type="Boolean" />
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
            <asp:UpdateProgress ID="upgClients" runat="server" AssociatedUpdatePanelID="upnlClients"><ProgressTemplate>updating divisions...</ProgressTemplate></asp:UpdateProgress>
        </asp:TableCell>
        <asp:TableCell Width="12px">&nbsp;</asp:TableCell>
        <asp:TableCell>
            <asp:Table ID="tblDivisions" runat="server" Width="100%" Height="100%" BorderStyle="None" BorderWidth="0px" CellPadding="0" CellSpacing="0">
                <asp:TableRow ID="thrDivisions" Height="16px" Width="100%">
                    <asp:TableCell SkinID="GridTitle">Divisions</asp:TableCell>
                    <asp:TableCell HorizontalAlign="Right" SkinID="GridTitle">
                        <asp:UpdatePanel ID="upnlDivisionsHeader" runat="server" UpdateMode="Always" >
                        <ContentTemplate>
                            <asp:TextBox ID="txtFindDivision" runat="server" Width="72px" BorderStyle="Inset" BorderWidth="1px" ToolTip="Enter a division number... <press Enter>" AutoPostBack="True" OnTextChanged="OnDivisionSearch"></asp:TextBox>
                            <asp:ImageButton ID="imgFindDivision" runat="server" Height="16px" ImageAlign="Middle" ImageUrl="~/App_Themes/Reports/Images/findreplace.gif" ToolTip="Search for a division..." OnClick="OnFindDivision" />&nbsp;
                        </ContentTemplate>
                        </asp:UpdatePanel>                    
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow Width="100%">
                    <asp:TableCell ColumnSpan="2" VerticalAlign="Top">
                        <asp:Panel ID="pnlDivisions" runat="server" Width="100%" Height="100%" BorderStyle="Inset" BorderWidth="1px" ScrollBars="Auto">
                            <asp:UpdatePanel ID="upnlDivisions" runat="server" ChildrenAsTriggers="true" RenderMode="Block" UpdateMode="Conditional" >
                            <ContentTemplate>
                            <asp:GridView ID="grdDivisions" runat="server" Width="100%" AutoGenerateColumns="False" AllowSorting="True" DataSourceID="odsDivisions" OnSelectedIndexChanged="OnDivisionSelected">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" HeaderStyle-Width="16px" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Reports/Images/select.gif" />
                                    <asp:BoundField DataField="DivisionNumber" HeaderText="Num" HeaderStyle-Width="60px" SortExpression="DivisionNumber" />
                                    <asp:BoundField DataField="ClientName" HeaderText="Name" />
                                </Columns>
                            </asp:GridView>
                            <asp:HiddenField ID="hfClientActiveOnly" runat="server" Value="false" />
                            <asp:ObjectDataSource ID="odsDivisions" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetClients" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="900">
                                <SelectParameters>
                                    <asp:ControlParameter Name="number" ControlID="grdClients" PropertyName="SelectedDataKey.Values[0]" DefaultValue="000" Type="String" />
                                    <asp:Parameter Name="division" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                    <asp:ControlParameter Name="activeOnly" ControlID="hfClientActiveOnly" PropertyName="Value" DefaultValue="False" Type="Boolean" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="grdClients" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="txtFindDivision" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="imgFindDivision" EventName="Click" />
                            </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:UpdateProgress ID="upgDivisions" runat="server" AssociatedUpdatePanelID="upnlDivisions"><ProgressTemplate>updating...</ProgressTemplate></asp:UpdateProgress>
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
