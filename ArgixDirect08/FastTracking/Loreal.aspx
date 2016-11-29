<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Loreal.aspx.cs" Inherits="Propak" Title="Argix Direct Fast Tracking" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="cBody" ContentPlaceHolderID="cpBody" Runat="Server">
<table class="default" border="0px" cellpadding="0px" cellspacing="0px" >
    <tr>
        <td style="height: 243px; width: 100%;">
            <table width="100%" cellpadding="0" cellspacing="0">
	            <tr><td class="label" style="height: 25px"><span style="font-weight:bold;">Enter Tracking Numbers:</span></td></tr>
	            <tr><td style="height: 15px" valign="middle"> Enter up to <span style="font-weight:bold;">10 item numbers</span>, one per line.</td></tr>
	            <tr>
		            <td>
			            <table width="480px" height="100%" border="0" cellpadding="0" cellspacing="1">
				            <tr style="height: 154px">
					            <td><asp:TextBox ID="txtNumbers" runat="server" Width="100%" Rows="10" TextMode="MultiLine"></asp:TextBox></td>
                                <td style="width: 48px">
                                    <asp:RequiredFieldValidator id="rfvTrack" runat="server" ErrorMessage="Please enter at least one tracking number." ControlToValidate="txtNumbers">*</asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="cvTrack" runat="server" ClientValidationFunction="ValidateNumbers" ControlToValidate="txtNumbers" ErrorMessage="Each tracking number must be 6 - 30 numeric characters." ValidateEmptyText="True">*</asp:CustomValidator>
                                </td>
				            </tr>
				            <tr>
					            <td><asp:ValidationSummary ID="vsTrack" runat="server" Width="100%" DisplayMode="SingleParagraph" /></td>
                                <td style="width: 48px">&nbsp;</td>
                            </tr>
			            </table>
		            </td>
	            </tr>
	            <tr>
	                <td  align="right" style="height: 19px">
                        <asp:LinkButton ID="btnTrack2" SkinID="linkBtn" OnClick="OnTrack" runat="server" >Track</asp:LinkButton>
                        &nbsp;<asp:ImageButton ID="btnTrack" runat="server" OnClick="OnTrackImg"  ImageUrl="~/App_Themes/ArgixDirect/Images/btn_arrow_red_right.gif" ImageAlign="AbsBottom" /> &nbsp;&nbsp;
		            </td>
	            </tr>
            </table>
        </td>
    </tr>
</table>

<script type="text/jscript" language="jscript">
    <!--
    function ValidateNumbers(sender, e) {
        var s = new String(e.Value);
        var ss = s.split("\n");
        var valid = true;
        for (i = 0; i < ss.length; i++) {
            var _s = trim(ss[i].replace("\r", ""));
            if (_s.length < 6 || _s.length > 30) valid = false;

            //var re = /\w{22}/g;
            //var r = re.test(_s);
            //if(!r) valid = false;
            if (!valid) break;
        }
        e.IsValid = valid;
    }
    function trim(str) {
        str = str.toString();
        while (1) {
            if (str.substring(0, 1) != " ") { break; }
            str = str.substring(1, str.length);
        }
        while (1) {
            if (str.substring(str.length - 1, str.length) != " ") { break; }
            str = str.substring(0, str.length - 1);
        }
        return str;
    }
    -->
    </script>
</asp:Content>
