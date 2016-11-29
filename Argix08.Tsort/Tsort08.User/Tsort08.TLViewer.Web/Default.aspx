<%@ Page language="c#" CodeFile="Default.aspx.cs" Inherits="Default" AutoEventWireup="true" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Argix Direct TL Viewer</title>
</head>
<body id="TLBody" onunload="javascript:document.body.style.cursor='wait';">
<form id="idForm" runat="server">
<div>
    <asp:ScriptManager ID="smPage" runat="server" EnablePartialRendering="true" ScriptMode="Auto"></asp:ScriptManager>
    <asp:Table ID="tblMaster" runat="server" Width="100%" Height="100%" BorderStyle="None" BorderWidth="0px" CellPadding="0" CellSpacing="2">
        <asp:TableRow style="font-size:1px"><asp:TableCell>&nbsp;</asp:TableCell><asp:TableCell>&nbsp;</asp:TableCell></asp:TableRow>
        <asp:TableRow Height="28px">
            <asp:TableCell>&nbsp;</asp:TableCell>
            <asp:TableCell ID="tcPageTitle" SkinID="PageTitle">
                <asp:Table ID="tblPageTitle" runat="server" Width="100%" BorderStyle="None" BorderWidth="0px" CellPadding="0" CellSpacing="0"> 
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Image ID="imgApp" runat="server" ImageUrl="~/App_Themes/Argix/Images/app.gif" ImageAlign="Middle" />
                            &nbsp;<asp:Label id="lblAppTitle" runat="server" Height="100%" Text="Argix Direct TL Viewer" />
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Right" VerticalAlign="Bottom">
                            &nbsp;
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Height="24px">
            <asp:TableCell>&nbsp;</asp:TableCell>
            <asp:TableCell style="padding-left:114px">
                Terminal&nbsp;
                <asp:DropDownList ID="cboTerminal" runat="server" Width="192px" DataSourceID="odsTerminals" DataTextField = "Description" DataValueField="TerminalID" AutoPostBack="True" ToolTip="Select a terminal" OnSelectedIndexChanged="OnTerminalChanged"></asp:DropDownList>
                &nbsp;<asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/App_Themes/Argix/Images/refresh.gif" ImageAlign="Middle" ToolTip="Refresh the list of TLs" CommandName="Refresh" OnCommand="OnToolbarClick" />
                <asp:ObjectDataSource ID="odsTerminals" runat="server" TypeName="Argix.Freight.FreightProxy" SelectMethod="GetTerminals" CacheExpirationPolicy="Sliding" CacheDuration="900" EnableCaching="true">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="terminalID" QueryStringField="location" DefaultValue="0" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Width="100%">
            <asp:TableCell Width="24px" VerticalAlign="top">
                <asp:UpdatePanel ID="upnlFlyout" runat="server" UpdateMode="Conditional" >
                    <ContentTemplate>
                        <asp:Table ID="tblFlyout" runat="server" Width="24px" BorderStyle="None" BorderWidth="0px" CellPadding="2" CellSpacing="0">
                            <asp:TableRow Height="24px"><asp:TableCell Font-Size="1px" style="border-right:solid 1px; border-color:White">&nbsp;</asp:TableCell></asp:TableRow>
                            <asp:TableRow Height="96px" ID="trTLView">
                                <asp:TableCell ID="tcTLView" VerticalAlign="Top" BorderStyle="Solid" BorderWidth="1px" style="border-right-style:solid; border-color:White">
                                    <asp:ImageButton ID="imgTLView" runat="server" ImageUrl="~/App_Themes/Argix/Images/tlview.gif" ToolTip="Click to view TL View" OnClick="OnTLViewTabClicked" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Height="4px"><asp:TableCell Font-Size="1px" style="border-right:solid 1px; border-color:White">&nbsp;</asp:TableCell></asp:TableRow>
                            <asp:TableRow Height="96px" ID="trAgentView">
                                <asp:TableCell ID="tcAgentView" VerticalAlign="Top" BorderStyle="Solid" BorderWidth="1px" style="border-right-style:solid; border-color:White">
                                    <asp:ImageButton ID="imgAgentView" runat="server" ImageUrl="~/App_Themes/Argix/Images/agentview.gif" ToolTip="Click to view Agent Summary" OnClick="OnAgentViewTabClicked" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Height="4px"><asp:TableCell Font-Size="1px" style="border-right:solid 1px; border-color:White">&nbsp;</asp:TableCell></asp:TableRow>
                            <asp:TableRow Height="226px"><asp:TableCell VerticalAlign="Top" BorderStyle="None" BorderWidth="1px" style="border-right-style:solid; border-color:White">&nbsp;</asp:TableCell></asp:TableRow>                        
                        </asp:Table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="uprgFlyout" runat="server" AssociatedUpdatePanelID="upnlFlyout"><ProgressTemplate>...</ProgressTemplate></asp:UpdateProgress>
            </asp:TableCell>
            <asp:TableCell Width="100%" VerticalAlign="Top" BorderStyle="None" BorderWidth="0">
                <asp:UpdatePanel ID="upnlView" runat="server" UpdateMode="Conditional" >
                    <ContentTemplate>
                        <asp:MultiView ID="mvMain" runat="server" ActiveViewIndex="0">
                            <asp:View ID="vwTLs" runat="server">
 	                            <table id="Table0" width="100%" border="0px" cellspacing="0px" cellpadding="0px">
                                    <tr style="font-size:1px"><td width="168px">&nbsp;</td><td width="3px">&nbsp;</td><td>&nbsp;</td></tr>		                            
                                    <tr>
			                            <td align="center" valign="top">
                                            <asp:UpdatePanel ID="upnlTotals" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <table id="tblTotalsHeader" width="168px" border="0px" cellspacing="0px" cellpadding="0px">
                                                    <tr style="height:18px"><td align="left" class="WindowTitle">Totals</td></tr>
                                                </table>
                                                <table id="tblTotals" width="168px" border="0px" cellspacing="3px" cellpadding="2px">
                                                    <tr><td class="Header" style="width:120px"># of TLs</td><td style="width:72px; border-style:solid; border-width:thin; text-align:right"><div id="TotalTLs">0</div></td></tr>
                                                    <tr><td class="Label">Cartons</td><td class="Data"><div id="TotalCartons">0</div></td></tr>
                                                    <tr><td class="Label">Pallets</td><td class="Data"><div id="TotalPallets">0</div></td></tr>
                                                    <tr><td class="Label">Weight (lbs)</td><td class="Data"><div id="TotalWeight">0</div></td></tr>
                                                    <tr><td class="Label">Cube (in3)</td><td class="Data"><div id="TotalCubeFt">0</div></td> </tr>
                                                    <tr><td colspan="2" style="font-size:3px;">&nbsp;</td></tr>
                                                    <tr><td colspan="2" class="Header">+ ISA</td></tr>
                                                    <tr><td class="Label">Weight (lbs)</td><td><input class="InputData" id="ISAWeight" type="text" value="0" style="width: 72px" /></td></tr>
                                                    <tr><td class="Label">Cube (in3)</td><td class="Data"><div id="ISACubeFt">0</div></td></tr>
                                                    <tr><td colspan="2" style="font-size:3px;"><hr /></td></tr>
                                                    <tr><td colspan="2" class="Header">= Total</td></tr>
                                                    <tr><td class="Label">Weight (lbs)</td><td class="GrandData"><div id="GrandWeight">0</div></td></tr>
                                                    <tr><td class="Label">Cube (in3)</td><td class="GrandData"><div id="GrandCubeFt">0</div></td></tr>
                                                    <tr><td colspan="2" style="font-size:3px;"><hr /><br /><hr /></td></tr>
                                                    <tr><td colspan="2" class="Header">Trailer Load% (53ft)</td></tr>
                                                    <tr><td class="Label">Weight (lbs)</td><td class="Data"><div id="WeightPercent">0</div></td></tr>
                                                    <tr><td class="Label">Cube (in3)</td><td class="Data"><div id="CubePercent">0</div></td></tr>
                                                    <tr><td colspan="2">&nbsp;</td></tr>
                                                </table>
                                            </ContentTemplate>
                                          </asp:UpdatePanel>
			                            </td>
			                            <td>&nbsp;</td>
			                            <td valign="top">
                                            <table id="tblTLHeader" width="100%" border="0px" cellspacing="0px" cellpadding="0px">
                                                <tr style="height:16px">
                                                    <td class="GridTitle"><asp:Image ID="imgTLs" runat="server" ImageUrl="~/App_Themes/Argix/Images/tl.gif" ImageAlign="Middle" />&nbsp;TLs</td>
                                                    <td align="right" class="GridTitle">
                                                        <asp:UpdatePanel ID="upnlTLHeader" runat="server" UpdateMode="Conditional" >
                                                        <ContentTemplate>
                                                            <asp:Image ID="imgFind" runat="server" Height="18px" ImageUrl="~/App_Themes/Argix/Images/search.gif" BorderStyle="None" ImageAlign="AbsMiddle" Visible="true"/>
                                                            <asp:TextBox ID="txtFind" runat="server" Width="96px" ToolTip="Search for a TL... <press Enter>" Visible="true" OnTextChanged="OnFindTL"></asp:TextBox>
                                                        </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Panel ID="pnlTLs" runat="server" Width="100%" Height="432px" BorderStyle="Inset" BorderWidth="1px" ScrollBars="Auto">
                                                <asp:UpdatePanel ID="upnlTLs" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:GridView id="grdTLs" runat="server" Width="100%" DataSourceID="odsTLs" AutoGenerateColumns="False" EnableTheming="True" AllowSorting="True" OnDataBound="OnDataBound" OnRowDataBound="OnRowDataBound" OnSorted="OnSorted">
                                                        <Columns>
                                                            <asp:BoundField DataField="TerminalID" HeaderText="Terminal" HeaderStyle-Width="48px" ItemStyle-Wrap="False" Visible="False" />
			                                                <asp:BoundField DataField="TLNumber" HeaderText="TL#" HeaderStyle-Width="72px" ItemStyle-Wrap="False" SortExpression="TLNumber" />
			                                                <asp:BoundField DataField="TLDate" HeaderText="TL Date" HeaderStyle-Width="75px" ItemStyle-Wrap="False" SortExpression="TLDate" DataFormatString="{0:MMddyy}" HtmlEncode="False" />
			                                                <asp:BoundField DataField="CloseNumber" HeaderText="Close#" HeaderStyle-Width="51px" ItemStyle-Wrap="False" SortExpression="CloseNumber" />
			                                                <asp:BoundField DataField="AgentNumber" HeaderText="Agent#" HeaderStyle-Width="51px" ItemStyle-Wrap="False" SortExpression="AgentNumber" />
			                                                <asp:BoundField DataField="ClientNumber" HeaderText="Client#" HeaderStyle-Width="51px" ItemStyle-Wrap="False" SortExpression="ClientNumber" />
			                                                <asp:BoundField DataField="ClientName" HeaderText="Client" ItemStyle-Wrap="False" SortExpression="Client" />
			                                                <asp:BoundField DataField="Zone" HeaderText="Zone" HeaderStyle-Width="48px" ItemStyle-Wrap="False" SortExpression="Zone" />
			                                                <asp:BoundField DataField="Lane" HeaderText="Lane" HeaderStyle-Width="48px" ItemStyle-Wrap="False" SortExpression="Lane" />
			                                                <asp:BoundField DataField="SmallLane" HeaderText="SmLane" HeaderStyle-Width="48px" ItemStyle-Wrap="False" SortExpression="SmallLane" />
			                                                <asp:BoundField DataField="Cartons" HeaderText="Ctns" HeaderStyle-Width="72px" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="False" SortExpression="Cartons" DataFormatString="{0:N0}" />
			                                                <asp:BoundField DataField="Pallets" HeaderText="Pllts" HeaderStyle-Width="72px" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="False" SortExpression="Pallets" DataFormatString="{0:N0}" />
			                                                <asp:BoundField DataField="Weight" HeaderText="Weight" HeaderStyle-Width="72px" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="False" SortExpression="Weight" DataFormatString="{0:N0}" />
			                                                <asp:BoundField DataField="Cube" HeaderText="Cube" HeaderStyle-Width="72px" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="False" SortExpression="Cube" DataFormatString="{0:N0}" />
                                                            <asp:BoundField DataField="WeightPercent" HeaderText="Weight%" HeaderStyle-Width="48px" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#'%'}" Visible="false" />
                                                            <asp:BoundField DataField="CubePercent" HeaderText="Cube%" HeaderStyle-Width="48px" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#'%'}" Visible="false" />
                                                            <asp:TemplateField HeaderText="TL Detail">
                                                                <ItemTemplate>
                                                                    <a href="" onclick="javascript:var w=window.showModelessDialog('TLDetail.aspx?location=<%# Eval("TerminalID") %>&tl=<%# Eval("TLNumber") %>','','dialogWidth:800px;dialogHeight:200px;center:yes;resizable:yes;scroll:yes;status:no;unadorned:yes');return false;"><%# Eval("TLNumber")%></a>                    
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
		                                                </Columns>
	                                                </asp:GridView>
                                                    <asp:ObjectDataSource ID="odsTLs" runat="server" TypeName="Argix.Freight.FreightProxy" SelectMethod="GetTLView" SortParameterName="sortBy">
                                                        <SelectParameters>
                                                            <asp:ControlParameter Name="terminalID" ControlID="cboTerminal" PropertyName="SelectedValue" Type="Int32" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txtFind" EventName="TextChanged" />
                                                </Triggers>
                                                </asp:UpdatePanel>
                                                <asp:UpdateProgress ID="uprgTLs" runat="server" AssociatedUpdatePanelID="upnlTLs"><ProgressTemplate>Updating view...</ProgressTemplate></asp:UpdateProgress>
                                            </asp:Panel>
                                        </td>
		                            </tr>
	                            </table>
                            </asp:View>
                            <asp:View ID="vwAgents" runat="server">
	                            <table id="tblAgents" width="100%" border="0px" cellspacing="0px" cellpadding="0px">
	                                <tr style="height:16px"><td class="GridTitle"><asp:Image ID="imgAgents" runat="server" ImageUrl="~/App_Themes/Argix/Images/agents.gif" ImageAlign="Middle" />&nbsp;Agent Summary</td></tr>
	                                <tr>
		                                <td>
                                            <asp:UpdatePanel ID="upnlAgents" runat="server" RenderMode="Block" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                            <ContentTemplate>
                                                <asp:Panel ID="pnlAgents" runat="server" Width="100%" Height="450px" BorderStyle="Inset" BorderWidth="1px" ScrollBars="Auto">
                                                    <asp:GridView id="grdSummary" runat="server" Width="100%" DataSourceID="odsSummary" AutoGenerateColumns="False" EnableTheming="True" AllowSorting="True">
	                                                    <Columns>
		                                                    <asp:BoundField DataField="AgentNumber" HeaderText="Agent#" HeaderStyle-Width="48px" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="False" SortExpression="AgentNumber" />
		                                                    <asp:BoundField DataField="AgentName" HeaderText="Agent" ItemStyle-Wrap="False" SortExpression="AgentName" />
		                                                    <asp:BoundField DataField="Zone" HeaderText="Zone" HeaderStyle-Width="48px" ItemStyle-Wrap="False" SortExpression="Zone" />
		                                                    <asp:BoundField DataField="TLNumber" HeaderText="TL#" HeaderStyle-Width="72px" ItemStyle-Wrap="False" SortExpression="TLNumber" />
		                                                    <asp:BoundField DataField="TLDate" HeaderText="TL Date" HeaderStyle-Width="60px" ItemStyle-Wrap="False" SortExpression="TLDate" DataFormatString="{0:MMddyy}" />
		                                                    <asp:BoundField DataField="CloseNumber" HeaderText="Close#" HeaderStyle-Width="51px" ItemStyle-Wrap="False" SortExpression="CloseNumber" />
		                                                    <asp:BoundField DataField="Cartons" HeaderText="Cartons" HeaderStyle-Width="60px" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" />
		                                                    <asp:BoundField DataField="Pallets" HeaderText="Pallets" HeaderStyle-Width="60px" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" />
		                                                    <asp:BoundField DataField="Weight" HeaderText="Weight" HeaderStyle-Width="72px" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" />
		                                                    <asp:BoundField DataField="WeightPercent" HeaderText="Weight%" HeaderStyle-Width="48px" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#'%'}" />
		                                                    <asp:BoundField DataField="CubePercent" HeaderText="Cube%" HeaderStyle-Width="48px" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#'%'}" />
	                                                    </Columns>
                                                    </asp:GridView>
                                                    <asp:ObjectDataSource ID="odsSummary" runat="server" TypeName="Argix.Freight.FreightProxy" SelectMethod="GetAgentSummary" SortParameterName="sortBy">
                                                        <SelectParameters>
                                                            <asp:ControlParameter Name="terminalID" ControlID="cboTerminal" PropertyName="SelectedValue" Type="Int32" />
                                                       </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </asp:Panel>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="uprgAgents" runat="server" AssociatedUpdatePanelID="upnlAgents"><ProgressTemplate>Updating view...</ProgressTemplate></asp:UpdateProgress>
		                                </td>
	                                </tr>
                                </table>
                           </asp:View>
                        </asp:MultiView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="imgTLView" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="imgAgentView" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Height="3px" style="font-size:1px"><asp:TableCell ColumnSpan="2">&nbsp;</asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell ColumnSpan="2" HorizontalAlign="Left">Copyright Argix Direct, Inc. v3.5.1.011111</asp:TableCell></asp:TableRow>
    </asp:Table>
</div>
</form>
</body>
</html>
