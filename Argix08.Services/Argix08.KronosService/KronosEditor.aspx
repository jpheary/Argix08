<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KronosEditor.aspx.cs" Inherits="KronosEditor" StylesheetTheme="Argix" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>App Logger Editor</title>
    <style type="text/css">
        .FixedHeader {
	        position: relative;
	        top : expression(this.offsetParent.scrollTop - 2);
	        z-index: 10;
        }
    </style>
    <script type="text/javascript" language="jscript">
        function pageLoad() {
        }
    </script>
</head>
<body>
<form id="form1" runat="server">
<div>
<asp:ScriptManager ID="smPage" runat="server" EnablePartialRendering="true" ScriptMode="Auto">
    <Services>
        <asp:ServiceReference Path="~/KronosService.svc" />
    </Services>
</asp:ScriptManager>
<script type="text/javascript" language="jscript">
    var scrollTop;
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(OnBeginRequest);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(OnEndRequest);
    function OnBeginRequest(sender, args) {
        scrollTop = document.getElementById('pnlEmployees').scrollTop;
    }
    function OnEndRequest(sender, args) {
        document.getElementById('pnlEmployees').scrollTop = scrollTop;
    }
</script>
<asp:UpdatePanel ID="upnlKronos" runat="server" RenderMode="Block" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>
        <asp:Table ID="tblPage" runat="server" Height="100%" Width="100%" BorderStyle="None" BorderWidth="2" CellPadding="0" CellSpacing="0">
            <asp:TableRow><asp:TableCell Width="96px" Font-Size="1px">&nbsp;</asp:TableCell><asp:TableCell Width="884px" Font-Size="1px">&nbsp;</asp:TableCell><asp:TableCell Font-Size="1px">&nbsp;</asp:TableCell></asp:TableRow>
            <asp:TableRow Height="18px" BackColor="Highlight" ForeColor="HighlightText"><asp:TableCell ColumnSpan="3">Employees from <% =mTerminal %></asp:TableCell></asp:TableRow>
            <asp:TableRow><asp:TableCell ColumnSpan="3" Font-Size="1px" Height="6px">&nbsp;</asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right">Employee Type&nbsp;</asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="cboType" runat="server" Width="192px" DataSourceID="odsTypes" OnSelectedIndexChanged="OnTypeChanged" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="odsTypes" runat="server" TypeName="Argix.Kronos" SelectMethod="GetIDTypes" EnableCaching="true" CacheDuration="600" />
                </asp:TableCell>
                <asp:TableCell>&nbsp;</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow><asp:TableCell ColumnSpan="3">&nbsp;</asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>&nbsp;</asp:TableCell>
                <asp:TableCell>
                    <asp:Panel ID="pnlEmployees" runat="server" Width="884px" Height="400px" ScrollBars="Auto">
                        <asp:GridView ID="grdEmployees" runat="server" DataSourceID="odsEmployees" DataKeyNames="IDNumber" AutoGenerateColumns="False" AllowSorting="true" OnSelectedIndexChanged="OnEmployeeSelected">
                            <Columns>
                                <asp:CommandField ButtonType="Image" HeaderStyle-Width="16px" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" ShowSelectButton="True" />
							    <asp:BoundField DataField="IDNumber" HeaderText="ID" HeaderStyle-Width="48px" />
							    <asp:BoundField DataField="LastName" HeaderText="Last" HeaderStyle-Width="72px" />
							    <asp:BoundField DataField="FirstName" HeaderText="First" HeaderStyle-Width="72px" />
							    <asp:BoundField DataField="Middle" HeaderText="Mid" HeaderStyle-Width="72px" />
							    <asp:BoundField DataField="Suffix" HeaderText="Suffix" HeaderStyle-Width="72px" />
							    <asp:BoundField DataField="Organization" HeaderText="Org" HeaderStyle-Width="72px" />
							    <asp:BoundField DataField="Department" HeaderText="Dept" HeaderStyle-Width="72px" />
							    <asp:BoundField DataField="Faccode" HeaderText="Faccode" HeaderStyle-Width="72px" />
							    <asp:BoundField DataField="Location" HeaderText="Loc" HeaderStyle-Width="72px" />
							    <asp:BoundField DataField="SubLocation" HeaderText="Sub Loc" HeaderStyle-Width="72px" />
							    <asp:BoundField DataField="EmployeeID" HeaderText="Employee#" HeaderStyle-Width="72px" />
							    <asp:BoundField DataField="BadgeNumber" HeaderText="Badge#" HeaderStyle-Width="72px" />
							    <asp:BoundField DataField="Photo" HeaderText="Pic" HeaderStyle-Width="72px" Visible="false" />
							    <asp:BoundField DataField="Signature" HeaderText="Sig" HeaderStyle-Width="72px" Visible="false" />
							    <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-Width="72px" />
							    <asp:BoundField DataField="StatusDate" HeaderText="Status Date" HeaderStyle-Width="120px" HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" />
							    <asp:BoundField DataField="IssueDate" HeaderText="Issued" HeaderStyle-Width="120px" HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" />
							    <asp:BoundField DataField="ExpirationDate" HeaderText="Expires" HeaderStyle-Width="120px" HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" />
							    <asp:BoundField DataField="DOB" HeaderText="Birth" HeaderStyle-Width="120px" HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" />
							    <asp:BoundField DataField="HireDate" HeaderText="Hired" HeaderStyle-Width="120px" HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" />
							    <asp:BoundField DataField="HasPhoto" HeaderText="Pic?" HeaderStyle-Width="48px" />
							    <asp:BoundField DataField="HasSignature" HeaderText="Sig?" HeaderStyle-Width="48px" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                    <asp:ObjectDataSource ID="odsEmployees" runat="server" TypeName="Argix.Kronos" SelectMethod="GetEmployees">
                        <SelectParameters>
                            <asp:ControlParameter Name="idType" ControlID="cboType" PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:TableCell>
                <asp:TableCell>&nbsp;</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow><asp:TableCell ColumnSpan="3">&nbsp;</asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>&nbsp;</asp:TableCell>
                <asp:TableCell HorizontalAlign="Right">
                    &nbsp;
                </asp:TableCell>
                <asp:TableCell>&nbsp;</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="uprgKronos" runat="server" AssociatedUpdatePanelID="upnlKronos">
    <ProgressTemplate>
        Updating employees...
    </ProgressTemplate>
</asp:UpdateProgress>
</div>
Copyright 2010 Argix Direct, Inc. v3.5.0.062810
</form>
</body>
</html>
