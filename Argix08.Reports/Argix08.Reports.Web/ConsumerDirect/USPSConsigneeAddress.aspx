<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="USPSConsigneeAddress.aspx.cs" Inherits="USPSConsigneeAddress" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" Height="100%" GroupingText="Setup">
    <table width="100%" border="0px" cellpadding="0" cellspacing="3px">
        <tr style="font-size:1px"><td width="120px">&nbsp;</td><td width="384px">&nbsp;</td><td>&nbsp;</td></tr>
         <tr>
            <td align="right" valign="top">Tracking #s&nbsp;</td>
            <td>
                <asp:RequiredFieldValidator ID="rfvNumbers" runat="server" ErrorMessage="Please enter tracking#'s" ControlToValidate="txtNumbers" ></asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="txtNumbers" runat="server" Width="100%" Height="192px" TextMode="MultiLine" MaxLength="400" Rows="11" AutoPostBack="true"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr><td>&nbsp;</td><td colspan="2">Track up to ten cartons at a time.<br />Enter one tracking# per line, or separate each with a comma.</td></tr>
   </table>
</asp:Panel>

<script type="text/javascript" language="javascript">
    function checkTextLen(field, maxlimit) {
        if (field.value.length > maxlimit) { 
            field.value = field.value.substring(0, maxlimit);
            alert("Length of the carton numbers exceeded the maximum allowed (" + maxlimit + ").");
            return false;
        }
    }

    function checkEmptyTextBox(field) {
        if(field.value.replace(/^\s+/,"").replace(/\s+$/,"") == "") { 
            alert("No valid tracking numbers were entered.");
            return false;
        }
        else 
            return true;
    }

    function removeNonNumerics(evt) {
        var keyCode = evt.which ? evt.which : evt.keyCode;
        if((keyCode > "0".charCodeAt() && keyCode <= "9".charCodeAt()) || (keyCode == 13 || keyCode == 188)) 
            return true;
        else 
            return false;
    }
</script>
</asp:Content>

