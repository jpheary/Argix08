//	File:	dualdatetimepicker.cs
//	Author:	J. Heary
//	Date:	01/14/08
//	Desc:	A composite control of two datetime pickers for selecting a date range.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

[Themeable(true)]
public partial class DualDateTimePicker:UserControl {
    //Members
    private int mDateDaysBack=0,mDateDaysForward=0,mDateDaysSpread=0;
    public event EventHandler DateTimeChanged = null;
    public event EventHandler ControlError = null;

    //Interface
    protected override void OnInit(EventArgs e) {
        //Event handler for page Init event
        if(!Page.IsPostBack && !ScriptManager.GetCurrent(Page).IsInAsyncPostBack) {
            //Get configuration values for this control
            string path = Page.Request.CurrentExecutionFilePath.Replace(".aspx","");
            string reportName = path.Substring(path.LastIndexOf('/') + 1);
            System.Xml.XmlNode rpt = this.xmlConfig.GetXmlDocument().SelectSingleNode("//report[@name='" + reportName + "']");
            if(rpt != null) {
                System.Xml.XmlNode ddp = rpt.SelectSingleNode("DualDateTimePicker");
                if(ddp != null) {
                    this.mDateDaysBack = int.Parse(ddp.Attributes["datedaysback"].Value);
                    this.mDateDaysForward = int.Parse(ddp.Attributes["datedaysforward"].Value);
                    this.mDateDaysSpread = int.Parse(ddp.Attributes["datedaysspread"].Value);
                }
            }
           
            //Set defaults if not set by host page
            if(this.txtTo.Text.Length == 0) this.txtTo.Text = DateTime.Now.ToLongDateString();
            if(this.txtFrom.Text.Length == 0) this.txtFrom.Text = this.txtTo.Text;
        }
        base.OnInit(e);
    }
    protected override void OnLoad(EventArgs e) {
        //Event handler for page Load event
        if(!Page.IsPostBack && !ScriptManager.GetCurrent(Page).IsInAsyncPostBack) {
            ViewState.Add("DateDaysBack", this.mDateDaysBack);
            ViewState.Add("DateDaysForward", this.mDateDaysForward);
            ViewState.Add("DateDaysSpread",this.mDateDaysSpread);
        }
        else {
            this.mDateDaysBack = (int)ViewState["DateDaysBack"];
            this.mDateDaysForward = (int)ViewState["DateDaysForward"];
            this.mDateDaysSpread = (int)ViewState["DateDaysSpread"];
        }
        base.OnLoad(e);
    }

    [Bindable(false),Category("Appearance"),DefaultValue("From:"),Description("From date label.")]
    public string FromLabel { get { return this.colFromLabel.Text; } set { this.colFromLabel.Text = value; } }
    [Bindable(false),Category("Behavior"),DefaultValue(true),Description("Enabled state of the From control.")]
    public bool FromEnabled { get { return this.txtFrom.Enabled; } set { this.txtFrom.Enabled = value; } }
    [Bindable(false),Category("Behavior"),DefaultValue(true),Description("Indicates whether the From controls are visible and rendered.")]
    public bool FromVisible { get { return this.txtFrom.Visible; } set { this.rowFrom.Visible = value; } }
    [Bindable(false),Category("Appearance"),DefaultValue("To:"),Description("To date label.")]
    public string ToLabel { get { return this.colToLabel.Text; } set { this.colToLabel.Text = value; } }
    [Bindable(false),Category("Behavior"),DefaultValue(true),Description("Enabled state of the To control.")]
    public bool ToEnabled { get { return this.txtTo.Enabled; } set { this.txtTo.Enabled = value; } }
    [Bindable(false),Category("Behavior"),DefaultValue(true),Description("Indicates whether the To controls are visible and rendered.")]
    public bool ToVisible { get { return this.txtTo.Visible; } set { this.rowTo.Visible = value; } }
    [Bindable(false),Category("Layout"),DefaultValue("72px"),Description("From/To label width.")]
    public Unit LabelWidth { get { return this.colFromLabel.Width; } set { this.colFromLabel.Width = this.colToLabel.Width = value; } }
    [Bindable(false),Category("Layout"),DefaultValue("100%"),Description("Control width.")]
    public Unit Width { get { return this.tblControl.Width; } set { this.tblControl.Width = value; } }

    [Bindable(false),Category("Behavior"),DefaultValue(0),Description("Number of selectable days in the past.")]
    public virtual int DateDaysBack {
        get { return this.mDateDaysBack; }
        set {
            //Validate value: days back is a + number from today to no limit back
            if(value != this.mDateDaysBack) {
                this.mDateDaysBack = value;
                if(this.mDateDaysSpread > this.mDateDaysBack + this.mDateDaysForward) this.mDateDaysSpread = this.mDateDaysBack + this.mDateDaysForward;
            }
        }
    }
    [Bindable(false),Category("Behavior"),DefaultValue(0),Description("Number of selectable days in the future.")]
    public virtual int DateDaysForward {
        get { return this.mDateDaysForward; }
        set {
            //Validate value: days forward is a + number from today to no limit forward
            if(value != this.mDateDaysForward) {
                if(this.mDateDaysSpread > this.mDateDaysBack + this.mDateDaysForward) this.mDateDaysSpread = this.mDateDaysBack + this.mDateDaysForward;
            }
        }
    }
    [Bindable(false),Category("Behavior"),DefaultValue(0),Description("Number of days between From and To dates.")]
    public virtual int DateDaysSpread {
        get { return this.mDateDaysSpread; }
        set {
            //Validate value
            this.mDateDaysSpread = (value > this.mDateDaysBack + this.mDateDaysForward) ? this.mDateDaysBack + this.mDateDaysForward : value;
        }
    }
    [Bindable(false),Category("Data"),Description("From date value.")]
    public DateTime FromDate {
        get { return DateTime.Parse(this.txtFrom.Text); }
        set {
            if(this.txtFrom.Text.Length == 0 || FromDate.CompareTo(value) != 0) {
                //Only on change in value
                this.calDateFrom.SelectedDate = value;
                OnDateSelected(this.calDateFrom,EventArgs.Empty);
            }
        }
    }
    [Bindable(false),Category("Data"),Description("To date value.")]
    public DateTime ToDate {
        get { return DateTime.Parse(this.txtTo.Text); }
        set {
            if(this.txtTo.Text.Length == 0 || ToDate.CompareTo(value) != 0) {
                //Only on change in value
                this.calDateTo.SelectedDate = value;
                OnDateSelected(this.calDateTo,EventArgs.Empty);
            }
        }
    }

    protected void OnShowFromDateCalendar(object sender,ImageClickEventArgs e) {
        //Event handler for show From date calendar
        if(this.pnlDateFrom.Style["display"] == "block") {
            this.pnlDateFrom.Style["display"] = "none";
        }
        else {
            this.calDateFrom.SelectedDate = this.calDateFrom.VisibleDate = DateTime.Parse(this.txtFrom.Text);
            this.pnlDateFrom.Style["display"] = "block";
            this.pnlDateTo.Style["display"] = "none";
        }
    }
    protected void OnShowToDateCalendar(object sender,ImageClickEventArgs e) {
        //Event handler for show To date calendar
        if(this.pnlDateTo.Style["display"] == "block") {
            this.pnlDateTo.Style["display"] = "none";
        }
        else {
            this.calDateTo.SelectedDate = this.calDateTo.VisibleDate = DateTime.Parse(this.txtTo.Text);
            this.pnlDateTo.Style["display"] = "block";
            this.pnlDateFrom.Style["display"] = "none";
        }
    }
    protected void OnDateSelected(object sender,EventArgs e) {
        //Event handler for user selected a From/To date from the calendar
        try {
            //Rules:
            //1. From & To dates cannot be earlier than the endpoint DateTime.Today.AddDays(-DateDaysBack)
            //   - set changed date to endpoint if invalid
            //2. From & To dates cannot be later than endpoint DateTime.Today.AddDays(DateDaysForward)
            //   - set changed date to endpoint if invalid
            //4. (From - To) spread cannot be less than 0 or greater than DateDaysSpread
            //   - From date can push To date forward; To date can push From date backward
            //   - From date can pull To date backward; To date can pull From date forward
            Calendar cal = (Calendar)sender;
            DateTime dtFrom = this.txtFrom.Text.Length > 0 ? DateTime.Parse(this.txtFrom.Text) : DateTime.Today;
            DateTime dtTo = this.txtTo.Text.Length > 0 ? DateTime.Parse(this.txtTo.Text) : DateTime.Now;
            DateTime dt = cal == this.calDateFrom ? this.calDateFrom.SelectedDate : this.calDateTo.SelectedDate;
            if(cal == this.calDateFrom) {
                //Hide the calendar popup
                this.pnlDateFrom.Style["display"] = "none";

                //Validate end points and set new From date
                if(dt.CompareTo(DateTime.Today.AddDays(this.mDateDaysForward + 1).AddSeconds(-1)) > 0)
                    dtFrom = DateTime.Today.AddDays(this.mDateDaysForward);
                else if(dt.CompareTo(DateTime.Today.AddDays(-this.mDateDaysBack)) < 0)
                    dtFrom = DateTime.Today.AddDays(-this.mDateDaysBack);
                else
                    dtFrom = dt;
                this.txtFrom.Text = dtFrom.ToLongDateString();

                //Validate spread- adjust To date as required to validate
                if(dtFrom.CompareTo(dtTo) > 0)
                    this.txtTo.Text = this.txtFrom.Text;
                else if(dtFrom.CompareTo(dtTo.AddDays(-this.mDateDaysSpread)) < 0)
                    this.txtTo.Text = dtFrom.AddDays(this.mDateDaysSpread).ToLongDateString();
            }
            else {
                //Hide the calendar popup
                this.pnlDateTo.Style["display"] = "none";

                //Validate end points and set new To date
                if(dt.CompareTo(DateTime.Today.AddDays(this.mDateDaysForward + 1).AddSeconds(-1)) > 0)
                    dtTo = DateTime.Today.AddDays(this.mDateDaysForward);
                else if(dt.CompareTo(DateTime.Today.AddDays(-this.mDateDaysBack)) < 0)
                    dtTo = DateTime.Today.AddDays(-this.mDateDaysBack);
                else
                    dtTo = dt;
                this.txtTo.Text = dtTo.ToLongDateString();

                //Validate spread- adjust From date as required to validate
                if(dtTo.CompareTo(dtFrom) < 0)
                    this.txtFrom.Text = this.txtTo.Text;
                else if(dtTo.CompareTo(dtFrom.AddDays(this.mDateDaysSpread)) > 0)
                    this.txtFrom.Text = dtTo.AddDays(-this.mDateDaysSpread).ToLongDateString();
            }
        }
        catch(Exception ex) { reportError(ex); }
        finally { if(this.DateTimeChanged != null) this.DateTimeChanged(this,e); }
    }
    protected void OnDateTimeChanged(EventArgs e) { if(DateTimeChanged != null) DateTimeChanged(this,e); }
    #region Local Services: reportError()
    private void reportError(Exception ex) {
        //Report an exception to the client
        try { if(this.ControlError != null) this.ControlError(this,EventArgs.Empty); }
        catch(Exception) { }
    }
    #endregion
}
