<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TLDetail.aspx.cs" Inherits="TLDetail" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>TL Detail</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:GridView id="grdDetail" runat="server" Width="100%" Height="100%" DataSourceID="odsDetail" AutoGenerateColumns="False" EnableTheming="True">
        <Columns>
            <asp:BoundField DataField="ClientNumber" HeaderText="Client#" HeaderStyle-Width="48px" ItemStyle-Wrap="False" />
            <asp:BoundField DataField="ClientName" HeaderText="Client" HeaderStyle-Width="192px" ItemStyle-Wrap="false" />
            <asp:BoundField DataField="ShipToLocationID" HeaderText="Ship To ID" HeaderStyle-Width="48px" ItemStyle-Wrap="False" />
            <asp:BoundField DataField="ShipToLocationName" HeaderText="Ship To" HeaderStyle-Width="240px" ItemStyle-Wrap="False" />
            <asp:BoundField DataField="Cartons" HeaderText="Cartons" HeaderStyle-Width="72px" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField DataField="Pallets" HeaderText="Pallets" HeaderStyle-Width="72px" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField DataField="Weight" HeaderText="Weight" HeaderStyle-Width="72px" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField DataField="Cube" HeaderText="Cube" HeaderStyle-Width="72px" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="odsDetail" runat="server" TypeName="Argix.Freight.FreightProxy" SelectMethod="GetTLDetail" SortParameterName="sortBy">
        <SelectParameters>
            <asp:QueryStringParameter Name="terminalID" QueryStringField="location" Type="Int32" />
            <asp:QueryStringParameter Name="tlNumber" QueryStringField="tl" Type="string" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
