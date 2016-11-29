<%@ Page Language="C#"MasterPageFile="~/Finance.master"  AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/Finance.master" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cpHead" Runat="Server">
<div class="PageTitle">Finance: Invoicing</div>
</asp:Content>
<asp:Content  ID="cBody" ContentPlaceHolderID="cpBody" Runat="Server">
<table width="100%" border="0px" cellpadding="0px" cellspacing="3px">
    <tr style="font-size:1px"><td width="24px">&nbsp;</td><td width="480px">&nbsp;</td><td>&nbsp;</td></tr>
    <tr>
        <td align="right">Client&nbsp;</td>
        <td>
            <asp:DropDownList id="cboClient" runat="server" Width="288px" DataSourceID="odsClients" DataTextField="ClientName" DataValueField="ClientNumber" AutoPostBack="True" OnSelectedIndexChanged="OnClientChanged"></asp:DropDownList>
            <asp:ObjectDataSource ID="odsClients" runat="server" TypeName="Argix.InvoiceService" SelectMethod="GetClients" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="600" />
        </td>
        <td align="right">
            <asp:UpdatePanel ID="upnlStatus" runat="server" UpdateMode="Always" >
            <ContentTemplate>
                <asp:Label ID="lblFilterTypes" runat="server" Text="" ToolTip="Supported invoice types"></asp:Label>&nbsp;
            </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
    <tr>
        <td colspan="3">
            <table width="100%" border="0px" cellpadding="0px" cellspacing="3px">
                <tr class="GridTitle">
                    <td>&nbsp;Invoices</td>
                    <td align="right"><asp:TextBox ID="txtSearchInvoices" runat="server" Width="144px" ToolTip="Search invoices" AutoPostBack="True" OnTextChanged="OnInvoiceSearch"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" valign="top">
                        <asp:Panel id="pnlInvoices" runat="server" Width="100%" Height="288px" BorderStyle="none" ScrollBars="Auto">
                        <asp:UpdatePanel ID="upnlInvoices" runat="server" UpdateMode="Conditional" >
                        <ContentTemplate>
                            <asp:GridView ID="grdInvoices" runat="server" Width="100%" DataSourceID="odsInvoices" DataKeyNames="InvoiceNumber" AutoGenerateColumns="False" AllowSorting="True" AutoGenerateCheckBoxColumn="True" CheckAllCheckBoxVisible="False" OnRowDataBound="OnInvoiceDataBinding" OnSelectedIndexChanged="OnInvoiceSelected">
                                <Columns>
                                    <asp:CommandField ButtonType="Image" HeaderStyle-Width="16px" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" SelectText="" ShowSelectButton="True" ShowCancelButton="False" />
                                    <asp:BoundField DataField="InvoiceNumber" HeaderText="Invoice#" HeaderStyle-Width="72px" SortExpression="InvoiceNumber" />
                                    <asp:BoundField DataField="InvoiceDate" HeaderText="Date" HeaderStyle-Width="96px" SortExpression="InvoiceDate" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" />
                                    <asp:BoundField DataField="PostToARDate" HeaderText="AR Post" HeaderStyle-Width="96px" SortExpression="PostToARDate" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" />
                                    <asp:BoundField DataField="Cartons" HeaderText="Ctns" HeaderStyle-Width="48px" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Pallets" HeaderText="Plts" HeaderStyle-Width="48px" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Weight" HeaderText="Weight" HeaderStyle-Width="72px" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-Width="72px" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="ReleaseDate" HeaderText="Released" HeaderStyle-Width="96px" SortExpression="ReleaseDate" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" />
                                    <asp:BoundField DataField="InvoiceTypeCode" HeaderText="Code" HeaderStyle-Width="48px" ItemStyle-HorizontalAlign="Center" SortExpression="InvoiceTypeCode" />
                                    <asp:BoundField DataField="InvoiceTypeDescription" HeaderText="Description" HeaderStyle-Width="96px" />
                                    <asp:BoundField DataField="BillTo" HeaderText="Bill To" HeaderStyle-Width="144px" />
                                    <asp:TemplateField HeaderText="Open">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkOpen" runat="server" Target="_blank" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:HiddenField ID="hfFilter" runat="server" Value="InvoiceTypeCode=''" />
                            <asp:ObjectDataSource ID="odsInvoices" runat="server" TypeName="Argix.InvoiceService" SelectMethod="GetClientInvoices" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="600">
                                <SelectParameters>
                                    <asp:ControlParameter Name="clientNumber" ControlID="cboClient" PropertyName="SelectedValue" Type="String" />
                                    <asp:Parameter Name="clientDivision" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                    <asp:Parameter Name="startDate" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                    <asp:ControlParameter Name="filter" ControlID="hfFilter" PropertyName="Value" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboClient" EventName="SelectedIndexChanged" />
                        </Triggers>
                        </asp:UpdatePanel>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <asp:UpdateProgress ID="upgInvoices" runat="server" AssociatedUpdatePanelID="upnlInvoices"><ProgressTemplate>updating...</ProgressTemplate></asp:UpdateProgress>
        </td>
    </tr>
    <tr style="font-size:1px; height:12px"><td colspan="3">&nbsp;</td></tr>
    <tr>
        <td colspan="3" align="right">
            <asp:UpdatePanel ID="upnlCommand" runat="server" UpdateMode="Always" >
            <ContentTemplate>
                <asp:Button ID="btnOpen" runat="server" Width="72px" Height="20px" Text="Open" CommandName="Open" OnCommand="OnCommand" />
                &nbsp;&nbsp;&nbsp;&nbsp;
            </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td>&nbsp;</td>
    </tr>            
</table>
<asp:XmlDataSource ID="xmlConfig" runat="server" DataFile="~/App_Data/Configuration.xml" EnableCaching="true" CacheExpirationPolicy="Absolute" CacheDuration="Infinite"></asp:XmlDataSource>
<script type="text/javascript" language="javascript">
    function scroll(number) {
        var grd = document.getElementById('ctl00_cpBody_grdInvoices');
        for(var i=1; i<grd.rows.length; i++) {
            var cell = grd.rows[i].cells[1];
            if(cell.innerHTML.substr(0, number.length) == number) {
                var pnl = document.getElementById('ctl00_cpBody_pnlInvoices');
                pnl.scrollTop = i * (grd.clientHeight / grd.rows.length);
                break;
            }
        }
    }
    function openExcel(target) {
		var excel = new ActiveXObject("Excel.Application");
		excel.Visible = true;
		excel.Workbooks.Open(target, false, false);
    }
</script>
</asp:Content>
