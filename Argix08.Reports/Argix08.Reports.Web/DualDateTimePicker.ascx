<%@ Control Language="C#" ClassName="DualDateTimePicker" AutoEventWireup="true" CodeFile="DualDateTimePicker.ascx.cs" Inherits="DualDateTimePicker" EnableTheming="true" %>

<asp:Table runat="server" ID="tblControl" Width="100%" BorderStyle="None" BorderWidth="2px" CellPadding="2" CellSpacing="2">
    <asp:TableRow runat="server" ID="rowFrom">
        <asp:TableCell runat="server" ID="colFromLabel" Width="72px" HorizontalAlign="Right">From</asp:TableCell>
        <asp:TableCell runat="server">
            <asp:UpdatePanel ID="upnlFrom" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
                <asp:TextBox runat="server" ID="txtFrom" ReadOnly="True" Width="100%" Text=""></asp:TextBox>
                <asp:Panel runat="server" ID="pnlDateFrom" ToolTip="From" style="display:none;position:absolute; height:172px; z-index:2; background-color:Transparent; border-style:none" >
                    <asp:Calendar runat="server" ID="calDateFrom" BorderStyle="Outset" BorderWidth="2px" style="left:0px; top:0px; z-index:2;" OnSelectionChanged="OnDateSelected" EnableTheming="True">
                        <NextPrevStyle Wrap="True" BackColor="ActiveCaption" ForeColor="ActiveCaptionText" />
                        <SelectedDayStyle BackColor="ActiveCaption" ForeColor="ActiveCaptionText" />
                        <TodayDayStyle BackColor="Info" />
                        <DayStyle BackColor="Menu" ForeColor="MenuText" Font-Bold="False" Font-Italic="False" Font-Underline="False" />
                        <DayHeaderStyle BackColor="Menu" BorderStyle="Solid" BorderWidth="1px" ForeColor="MenuText" />
                        <TitleStyle BackColor="ActiveCaption" ForeColor="ActiveCaptionText" Font-Bold="true" Wrap="false" />
                        <OtherMonthDayStyle BackColor="Menu" ForeColor="GrayText" />
                    </asp:Calendar>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="imgFrom" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="imgTo" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="calDateTo" EventName="SelectionChanged" />
            </Triggers>
            </asp:UpdatePanel>
        </asp:TableCell>
        <asp:TableCell Width="32px">
            &nbsp;<asp:ImageButton ID="imgFrom" runat="server" ImageUrl="~/App_Themes/Reports/Images/calendar.gif" ImageAlign="AbsMiddle" ToolTip="Open the from calendar popup" OnClick="OnShowFromDateCalendar" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow runat="server" ID="rowTo">
        <asp:TableCell runat="server" ID="colToLabel" Width="72px" HorizontalAlign="Right">To</asp:TableCell>
        <asp:TableCell runat="server">
            <asp:UpdatePanel ID="upnlTo" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
                <asp:TextBox runat="server" ID="txtTo" ReadOnly="True" Width="100%" Text=""></asp:TextBox>
                <asp:Panel runat="server" ID="pnlDateTo" ToolTip="From" style="display:none; position:absolute; height:172px; z-index:2; background-color:Transparent; border-style:none" >
                    <asp:Calendar runat="server" ID="calDateTo" BorderStyle="Outset" BorderWidth="2px" style="left:0px; top:0px; z-index:2;" OnSelectionChanged="OnDateSelected" EnableTheming="True">
                        <NextPrevStyle Wrap="True" BackColor="ActiveCaption" ForeColor="ActiveCaptionText" />
                        <SelectedDayStyle BackColor="ActiveCaption" ForeColor="ActiveCaptionText" />
                        <TodayDayStyle BackColor="Info" />
                        <DayStyle BackColor="Menu" ForeColor="MenuText" Font-Bold="False" Font-Italic="False" Font-Underline="False" />
                        <DayHeaderStyle BackColor="Menu" BorderStyle="Solid" BorderWidth="1px" ForeColor="MenuText" />
                        <TitleStyle BackColor="ActiveCaption" ForeColor="ActiveCaptionText" Font-Bold="true" Wrap="false" />
                        <OtherMonthDayStyle BackColor="Menu" ForeColor="GrayText" />
                    </asp:Calendar>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="imgTo" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="imgFrom" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="calDateFrom" EventName="SelectionChanged" />
            </Triggers>
            </asp:UpdatePanel>
        </asp:TableCell>
        <asp:TableCell Width="32px">
            &nbsp;<asp:ImageButton ID="imgTo" runat="server" ImageUrl="~/App_Themes/Reports/Images/calendar.gif" ImageAlign="AbsMiddle" ToolTip="Open the to calendar popup" OnClick="OnShowToDateCalendar" />
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
<asp:XmlDataSource ID="xmlConfig" runat="server" DataFile="~/App_Data/Configuration.xml" EnableCaching="true" CacheExpirationPolicy="Absolute" CacheDuration="Infinite"></asp:XmlDataSource>
