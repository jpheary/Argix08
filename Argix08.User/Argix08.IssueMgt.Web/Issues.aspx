<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Issues.aspx.cs" Inherits="Issues" Theme="Argix" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Issues</title>
</head>
<body class="PrintBody">
    <form id="form1" runat="server">
    <div>
    <asp:Table ID="tblPage" runat="server" BorderStyle="Solid" BorderWidth="0px" CellPadding="2" CellSpacing="0">
        <asp:TableRow>
            <asp:TableCell style="width:60px; border:inset 1px ButtonShadow;">Zone</asp:TableCell>
            <asp:TableCell style="width:60px; border:inset 1px ButtonShadow;">Store</asp:TableCell>
            <asp:TableCell style="width:60px; border:inset 1px ButtonShadow;">Agent</asp:TableCell>
            <asp:TableCell style="width:144px; border:inset 1px ButtonShadow;">Company</asp:TableCell>
            <asp:TableCell style="width:96px; border:inset 1px ButtonShadow;">Type</asp:TableCell>
            <asp:TableCell style="width:96px; border:inset 1px ButtonShadow;">Action</asp:TableCell>
            <asp:TableCell style="width:144px; border:inset 1px ButtonShadow;">Received</asp:TableCell>
            <asp:TableCell style="width:192px; border:inset 1px ButtonShadow;">Subject</asp:TableCell>
            <asp:TableCell style="width:144px; border:inset 1px ButtonShadow;">Contact</asp:TableCell>
            <asp:TableCell style="width:96px; border:inset 1px ButtonShadow;">Last User</asp:TableCell>
            <asp:TableCell style="width:96px; border:inset 1px ButtonShadow;">Coordinator</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Font-Size="1px" Height="3px"><asp:TableCell ColumnSpan="11">&nbsp;</asp:TableCell></asp:TableRow>
    </asp:Table>
    </div>
    </form>
</body>
</html>
