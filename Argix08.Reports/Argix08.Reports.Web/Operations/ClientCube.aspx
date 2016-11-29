<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="ClientCube.aspx.cs" Inherits="ClientCube" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0px" cellspacing="3px">
        <tr style="font-size:1px"><td width="96px">&nbsp;</td><td width="384px">&nbsp;</td><td>&nbsp;</td></tr>
         <tr>
            <td colspan="2">&nbsp;<uc1:DualDateTimePicker ID="ddpSetup" runat="server" Width="336px" LabelWidth="96px" DateDaysBack="7" DateDaysForward="0" DateDaysSpread="1" OnDateTimeChanged="OnFromToDateChanged" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Client&nbsp;</td>
            <td>
                <asp:DropDownList id="cboClient" runat="server" Width="288px" DataSourceID="odsClients" DataTextField="ClientName" DataValueField="ClientNumber" AutoPostBack="True" OnSelectedIndexChanged="OnClientChanged"></asp:DropDownList>
                <asp:ObjectDataSource ID="odsClients" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetClients" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="600">
                    <SelectParameters>
                        <asp:Parameter Name="number" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                        <asp:Parameter Name="division" DefaultValue="01" Type="String" />
                        <asp:Parameter Name="activeOnly" DefaultValue="False" Type="Boolean" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Terminal&nbsp;</td>
            <td>
                <asp:DropDownList id="cboTerminal" runat="server" Width="192px" DataSourceID="odsTerminals" AppendDataBoundItems="true" DataTextField="Description" DataValueField="TerminalCode" AutoPostBack="True" OnSelectedIndexChanged="OnTerminalChanged">
                    <asp:ListItem Text="All" Value="" Selected="True" />
                </asp:DropDownList>
                <asp:ObjectDataSource ID="odsTerminals" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetClientTerminals" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="600"  >
                    <SelectParameters>
                        <asp:ControlParameter Name="number" ControlID="cboClient" PropertyName="SelectedValue" Type="string" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
             <td>&nbsp;</td>
       </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Vendor&nbsp;</td>
            <td>
                <asp:DropDownList id="cboVendor" runat="server" Width="192px" DataSourceID="odsVendors" AppendDataBoundItems="true" DataTextField="VendorName" DataValueField="VendorNumber" AutoPostBack="True" OnSelectedIndexChanged="OnVendorChanged">
                    <asp:ListItem Text="All" Value="" Selected="True" />
                </asp:DropDownList>
                <asp:ObjectDataSource ID="odsVendors" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetVendorsList" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="300"  >
                    <SelectParameters>
                        <asp:ControlParameter Name="clientNumber" ControlID="cboClient" PropertyName="SelectedValue" Type="string" />
                        <asp:ControlParameter Name="clientTerminal" ControlID="cboTerminal" PropertyName="SelectedValue" Type="string" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Zone&nbsp;</td>
            <td>
                <asp:DropDownList id="cboZone" runat="server" Width="96px" DataSourceID="odsZones" AppendDataBoundItems="true" DataTextField="Code" DataValueField="Code" AutoPostBack="True" OnSelectedIndexChanged="OnZoneChanged">
                    <asp:ListItem Text="All" Value="" Selected="True" />
                </asp:DropDownList>
                <asp:ObjectDataSource ID="odsZones" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetZones" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="900" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
   </table>
</asp:Panel>
</asp:Content>

