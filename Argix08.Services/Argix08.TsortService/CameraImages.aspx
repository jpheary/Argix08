<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CameraImages.aspx.cs" Inherits="CameraImages" StylesheetTheme="Argix" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Camera Images</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="subtitle">Camera Images</div>
    <table>
        <tr>
            <td>
                <div style="width:400px; height:400px; margin:0px 0px 0px 0px; padding:0px 0px 0px 0px; overflow-x:hidden; overflow-y:scroll; white-space:nowrap;">
                    <asp:GridView ID="grdImages" runat="server" Width="100%" AutoGenerateColumns="false" DataSourceID="odsImages" DataKeyNames="ID" AllowSorting="true" OnSelectedIndexChanged="OnImageSelected" >
                        <Columns>
                            <asp:CommandField HeaderStyle-Width="16px" ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" />
                            <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-Wrap="False" ItemStyle-Width="25px" Visible="True" />
                            <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-Wrap="False" ItemStyle-Width="75px" SortExpression="Date" DataFormatString="{0:MM/dd/yyyy hh:mm tt}" HtmlEncode="False" Visible="True" />
                            <asp:BoundField DataField="Filename" HeaderText="Filename" ItemStyle-Wrap="False" Visible="True" />
                            <asp:BoundField DataField="File" HeaderText="File" ItemStyle-Wrap="False" Visible="False" />
                        </Columns>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="odsImages" runat="server" TypeName="Argix.Freight.CameraService" SelectMethod="ViewImages" />
                </div>
            </td>
            <td>
                <div style="width:400px;">
                    <asp:Image ID="imgPicture" runat="server" ImageUrl="" Width="400px" Height="400px" />
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
