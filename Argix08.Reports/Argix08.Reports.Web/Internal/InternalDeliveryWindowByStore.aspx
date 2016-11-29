<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="InternalDeliveryWindowByStore.aspx.cs" Inherits="InternalDeliveryWindowByStore" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0" cellspacing="3px">
        <tr><td width="72px">&nbsp;</td><td width="288px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td align="right" valign="top">Retail&nbsp;</td>
            <td>
                <asp:DropDownList id="cboDateParam" runat="server" Width="96px" AutoPostBack="True" OnSelectedIndexChanged="OnDateParamChanged">
                    <asp:ListItem Text="Week" Value="Week" Selected="True" />
                </asp:DropDownList>
                &nbsp;
                <asp:DropDownList id="cboDateValue" runat="server" Width="180px" DataSourceID="odsDates" DataTextField="Value" DataValueField="Value" AutoPostBack="True"></asp:DropDownList>
                <asp:ObjectDataSource ID="odsDates" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetSortDates" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="600" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Client&nbsp;</td>
            <td colspan="2" valign="middle">
                <asp:DropDownList id="cboClient" runat="server" Width="288px" DataSourceID="odsClients" DataTextField="ClientName" DataValueField="ClientNumber" AutoPostBack="True" OnSelectedIndexChanged="OnClientChanged"></asp:DropDownList>
                <asp:ObjectDataSource ID="odsClients" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetSecureClients" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="600">
                    <SelectParameters>
                        <asp:ControlParameter Name="activeOnly" ControlID="chkActiveOnly" PropertyName="Checked" Type="Boolean" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                &nbsp;&nbsp;
                <asp:CheckBox ID="chkActiveOnly" runat="server" AutoPostBack="true" Text="Active Only" Checked="true" OnCheckedChanged="OnActiveOnlyChecked" />
            
            </td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right" valign="top">Scope&nbsp;</td>
            <td colspan="2">
                <asp:DropDownList id="cboParam" runat="server" Width="96px" AutoPostBack="True" OnSelectedIndexChanged="OnScopeParamChanged">
                    <asp:ListItem Text="Divisions" Value="Divisions" Selected="True" />
                    <asp:ListItem Text="Agents" Value="Agents" />
                    <asp:ListItem Text="Regions" Value="Regions" />
                    <asp:ListItem Text="Districts" Value="Districts" />
                    <asp:ListItem Text="Stores" Value="Stores" />
                </asp:DropDownList>
                &nbsp;
                <asp:DropDownList id="cboValue" runat="server" Width="288px" DataSourceID="odsValues" DataTextField="DistrictName" DataValueField="District" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="OnScopeValueChanged">
                    <asp:ListItem Text="All" Value="" Selected="True"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtStore" runat="server" Width="120px" Visible="false" AutoPostBack="True" OnTextChanged="OnStoreChanged"></asp:TextBox>
                <asp:ObjectDataSource ID="odsValues" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetClientDistricts" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="600" >
                    <SelectParameters>
                        <asp:ControlParameter Name="number" ControlID="cboClient" PropertyName="SelectedValue" ConvertEmptyStringToNull="true" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right" valign="top">Filters&nbsp;</td>
            <td colspan="2">
                <hr />
                <table width="100%" border="0" cellpadding="0px" cellspacing="3px">
                    <tr><td width="144px">&nbsp;</td><td width="192px">&nbsp;</td><td>&nbsp;</td></tr>
                     <tr>
                        <td align="right">Not-On-Time Deliveries&nbsp;</td>
                        <td><asp:TextBox ID="txtFilter" runat="server" Text="2" MaxLength="3" Width="48px"></asp:TextBox></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
                </table>
            </td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
    </table>
</asp:Panel>
</asp:Content>
