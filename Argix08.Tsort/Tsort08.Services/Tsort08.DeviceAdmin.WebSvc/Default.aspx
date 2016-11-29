<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" StylesheetTheme="Argix" Theme="Argix" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tsort Scale Admin</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Table ID="tblBody" runat="server" Width="100%" Height="100%" BorderStyle="Inset" BorderWidth="1px" CellPadding="0" CellSpacing="1">
            <asp:TableHeaderRow><asp:TableHeaderCell>Tsort Scale Service</asp:TableHeaderCell></asp:TableHeaderRow>
            <asp:TableRow Font-Size="1px"><asp:TableCell>&nbsp;</asp:TableCell><asp:TableCell Width="192px">&nbsp;</asp:TableCell></asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Table ID="tblService" runat="server" Width="100%" Height="100%" BorderStyle="Inset" BorderWidth="0px" CellPadding="1" CellSpacing="2">
                        <asp:TableRow Font-Size="1px"><asp:TableCell>&nbsp;</asp:TableCell><asp:TableCell>&nbsp;</asp:TableCell></asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Service</asp:TableCell><asp:TableCell><asp:Label ID="lblService" runat="server" Text=""></asp:Label></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Is On</asp:TableCell><asp:TableCell><asp:Label ID="lblIsOn" runat="server" Text=""></asp:Label></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow><asp:TableCell ColumnSpan="2">&nbsp;</asp:TableCell></asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>&nbsp;</asp:TableCell>
                            <asp:TableCell>
                                <asp:Button ID="btnStart" runat="server" Text="Start" OnClick="TurnOn" />
                                &nbsp;<asp:Button ID="btnStop" runat="server" Text="Stop" OnClick="TurnOff" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow><asp:TableCell ColumnSpan="2"><hr /></asp:TableCell></asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Weight</asp:TableCell>
                            <asp:TableCell><asp:Label ID="lblWeight" runat="server" Height="72px" Width="96px" SkinID="ScaleSkin"></asp:Label></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>&nbsp;</asp:TableCell>
                            <asp:TableCell>
                                <asp:Button ID="btnWeight" runat="server" Text="Get Weight" OnClick="GetWeight" />
                                &nbsp;<asp:Button ID="btnZero" runat="server" Text="Zero" OnClick="Zero" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
                <asp:TableCell VerticalAlign="Top">
                    Born On: <asp:Label ID="lblBornOn" runat="server" Text=""></asp:Label>
                    <br /><br /><br />
                    <asp:Panel ID="pnlSettings" runat="server" GroupingText="Settings" BorderStyle="Inset" BorderWidth="1px">
                        <asp:Table ID="tblSettings" runat="server" Width="100%" Height="100%" BorderStyle="Inset" BorderWidth="0px" CellPadding="1" CellSpacing="2">
                            <asp:TableRow Font-Size="1px"><asp:TableCell Width="96px">&nbsp;</asp:TableCell><asp:TableCell>&nbsp;</asp:TableCell></asp:TableRow>
                            <asp:TableRow><asp:TableCell>Port Name</asp:TableCell><asp:TableCell><asp:Label ID="lblPort" runat="server" Text=""></asp:Label></asp:TableCell></asp:TableRow>
                            <asp:TableRow><asp:TableCell>Baud Rate</asp:TableCell><asp:TableCell><asp:Label ID="lblBaud" runat="server" Text=""></asp:Label></asp:TableCell></asp:TableRow>
                            <asp:TableRow><asp:TableCell>Data Bits</asp:TableCell><asp:TableCell><asp:Label ID="lblData" runat="server" Text=""></asp:Label></asp:TableCell></asp:TableRow>
                            <asp:TableRow><asp:TableCell>Stop Bits</asp:TableCell><asp:TableCell><asp:Label ID="lblStop" runat="server" Text=""></asp:Label></asp:TableCell></asp:TableRow>
                            <asp:TableRow><asp:TableCell>Parity</asp:TableCell><asp:TableCell><asp:Label ID="lblParity" runat="server" Text=""></asp:Label></asp:TableCell></asp:TableRow>
                            <asp:TableRow><asp:TableCell>Handshake</asp:TableCell><asp:TableCell><asp:Label ID="lblHandshake" runat="server" Text=""></asp:Label></asp:TableCell></asp:TableRow>
                        </asp:Table>
                    </asp:Panel>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    </form>
</body>
</html>
