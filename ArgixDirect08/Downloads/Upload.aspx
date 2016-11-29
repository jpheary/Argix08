<%@ Page Language="C#" MasterPageFile="~/MasterPages/Downloads.master" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="Upload" Title="Upload File" StylesheetTheme="ArgixDirect" Theme="ArgixDirect" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <table>
            <tr>
                <td align="right"><asp:Label ID="lblTo" runat="server" width="96px" Text="Client:"></asp:Label></td>
                <td><asp:DropDownList ID="cboFolder" runat="server" Width="480px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td align="right"><asp:Label ID="lblFrom" runat="server" width="96px" Text="File:"></asp:Label></td>
                <td><asp:FileUpload ID="FileUpload1" runat="server" Width="480px" /></td>
            </tr>
            <tr><td colspan="2">&nbsp;</td></tr>
            <tr>
                <td>&nbsp;</td>
                <td align="right"><asp:Button ID="btnUpload" runat="server" OnClick="OnUpload" Text="Upload" Height="20px" Width="72px" /></td>
            </tr>
        </table>
    </div>
</asp:Content>

