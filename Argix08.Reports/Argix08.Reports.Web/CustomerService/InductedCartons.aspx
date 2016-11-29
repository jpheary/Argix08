<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="InductedCartons.aspx.cs" Inherits="InductedCartons" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
<div style="margin: 0px 0px 0px 0px; padding: 0px 0px 0px 0px;">
    <uc1:DualDateTimePicker ID="ddpFreight" runat="server" Width="332px" LabelWidth="96px" DateDaysBack="365" DateDaysForward="0" DateDaysSpread="30" OnDateTimeChanged="OnFromToDateChanged" />
</div>
<div style="margin: 12px 0px 0px 50px; padding: 0px 0px 0px 0px;">
    Terminal&nbsp;
    <asp:DropDownList ID="cboTerminal" runat="server" Width="192px" AutoPostBack="True" OnSelectedIndexChanged="OnTerminalSelected">
        <asp:ListItem Text="Jamesburg" Value="05" Selected="True" />
    </asp:DropDownList>
</div>
<div style="margin: 12px 0px 0px 50px; padding: 0px 0px 0px 0px;">
    <div style="height:16px; margin:0px 0px 1px 0px; padding:3px 0px 0px 6px;">Freight</div>
    <asp:Panel id="pnlFreight" runat="server" Width="100%" Height="300px" BorderStyle="Inset" BorderWidth="1px" ScrollBars="Auto">
        <asp:UpdatePanel ID="upnlFreight" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
            <asp:GridView ID="grdFreight" runat="server" Width="100%" AutoGenerateColumns="false" DataSourceID="odsFreight" DataKeyNames="BLNumber" AllowSorting="True" OnSelectedIndexChanged="OnFreightSelected">
                <Columns>
                    <asp:CommandField HeaderStyle-Width="16px" ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Reports/Images/select.gif" />
                    <asp:BoundField DataField="ShipperNumber" HeaderText="Shipper#" HeaderStyle-Width="72px" SortExpression="ShipperNumber" />
                    <asp:BoundField DataField="ShipperName" HeaderText="Shipper" HeaderStyle-Width="144px" SortExpression="ShipperName" HtmlEncode="False" />
                    <asp:BoundField DataField="BLNumber" HeaderText="BLNumber" HeaderStyle-Width="72px" SortExpression="BLNumber" />
                    <asp:BoundField DataField="Cartons" HeaderText="Cartons" HeaderStyle-Width="48px" SortExpression="Cartons" />
                    <asp:BoundField DataField="CarrierNumber" HeaderText="Carrier#" HeaderStyle-Width="72px" SortExpression="CarrierNumber" />
                    <asp:BoundField DataField="TrailerNumber" HeaderText="Trailer#" HeaderStyle-Width="72px" SortExpression="TrailerNumber" />
                    <asp:BoundField DataField="Started" HeaderText="Started" HeaderStyle-Width="120px" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False" />
                    <asp:BoundField DataField="Stopped" HeaderText="Stopped" HeaderStyle-Width="120px" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False" />
                    <asp:BoundField DataField="Imported" HeaderText="Imported" HeaderStyle-Width="120px" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False" />
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="odsFreight" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetInductedFreight" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="300" >
                <SelectParameters>
                    <asp:ControlParameter Name="startImportedDate" ControlID="ddpFreight" PropertyName="FromDate" Type="DateTime" />
                    <asp:ControlParameter Name="endImportedDate" ControlID="ddpFreight" PropertyName="ToDate" Type="DateTime" />
                    <asp:ControlParameter Name="terminalCode" ControlID="cboTerminal" PropertyName="SelectedValue" Type="string" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddpFreight" EventName="DateTimeChanged" />
            <asp:AsyncPostBackTrigger ControlID="cboTerminal" EventName="SelectedIndexChanged" />
        </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
</div>
</asp:Panel>
</asp:Content>

