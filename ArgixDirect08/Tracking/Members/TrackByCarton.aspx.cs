using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Members_TrackByCarton : System.Web.UI.Page {
    //Members

    //Inteface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for form load event
        if(!Page.IsPostBack) {
            if(Session["TrackBy"] != null) this.cboSearchBy.SelectedValue = Session["TrackBy"].ToString();
            
            //Attach client-side scripts
            Page.ClientScript.RegisterStartupScript(typeof(Page),"StartupScript",getClientStartupScript());
            this.txtNumbers.Attributes.Add("onkeypress","checkTextLen(this.form." + this.txtNumbers.UniqueID + "," + Application["TrackingCharsMax"].ToString() + ");");
            this.txtNumbers.Attributes.Add("onblur","checkTextLen(this.form." + this.txtNumbers.UniqueID + "," + Application["TrackingCharsMax"].ToString() + ");");
            this.btnTrack.Attributes.Add("onclick","return checkEmptyTextBox(this.form." + this.txtNumbers.UniqueID + ");");
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page),"TrackingScript",getCharLimitScript());
        }
    }
    protected void OnTrack(object sender,EventArgs e) {
        //Track one or more cartons
        string searchBy = TrackingServices.SEARCHBY_CARTONNUMBER;
        switch(this.cboSearchBy.SelectedValue) {
            case "CartonNumber": searchBy = TrackingServices.SEARCHBY_CARTONNUMBER; break;
            case "LabelNumber": searchBy = TrackingServices.SEARCHBY_LABELNUMBER; break;
            case "PlateNumber": searchBy = TrackingServices.SEARCHBY_PLATENUMBER; break;
        }
        Session["TrackBy"] = searchBy;
        Session["SubStore"] = null;

        //Validate
        string input = encodeInput(this.txtNumbers.Text);
        if(input.Length == 0) {
            this.rfvTracking.IsValid = false;
            this.rfvTracking.ErrorMessage = "No valid tracking numbers were entered.";
            return;
        }
        string[] numbers = input.Split(Convert.ToChar(13));
        if(numbers.Length > (int)Application["TrackingNumbersMax"]) {
            this.rfvTracking.IsValid = false;
            this.rfvTracking.ErrorMessage = "You can not search more than 10 items at a time.";
            return;
        }

        //Build search table and validate
        TrackingDS trackingSearch = new TrackingDS();
        for(int i=0;i<numbers.Length;i++) {
            string number = numbers[i].Trim();
            if(trackingSearch.CartonSearchTable.FindBySearchID(number) == null) {
                //Not a duplicate- validate format
                if(searchBy == TrackingServices.SEARCHBY_CARTONNUMBER && number.Length >= (int)Application["CartonLenMin"] && number.Length <= (int)Application["CartonLenMax"])
                    trackingSearch.CartonSearchTable.AddCartonSearchTableRow(number,false,i,0,true);
                else if(searchBy == TrackingServices.SEARCHBY_LABELNUMBER && number.Length > 0 && number.Length == (int)Application["ArgixLabelLen"] && isNumeric(number))
                    trackingSearch.CartonSearchTable.AddCartonSearchTableRow(number,false,i,0,true);
                else if(searchBy == TrackingServices.SEARCHBY_PLATENUMBER && number.Length > 0 && number.Length <= (int)Application["CartonLenMax"])
                    trackingSearch.CartonSearchTable.AddCartonSearchTableRow(number,false,i,0,true);
                else {
                    trackingSearch.CartonSearchTable.AddCartonSearchTableRow(number,false,i,0,false);
                }
            }
        }
        DataRow[] valid = trackingSearch.CartonSearchTable.Select("Valid = true");
        if(valid.Length == 0) {
            //No valid numbers to track
            this.rfvTracking.IsValid = false;
            this.rfvTracking.ErrorMessage = "No valid tracking numbers were entered.";
            return;
        }

        //Get carton detail data
        StringBuilder trackingNumbers = new StringBuilder();
        foreach(TrackingDS.CartonSearchTableRow row in trackingSearch.CartonSearchTable.Rows) {
            if(row.Valid) trackingNumbers.Append(trackingNumbers.Length == 0 ? row.SearchID : "," + row.SearchID);
        }
        ProfileCommon profile = new MembershipServices().MemberProfile;
        TrackingServices svcs = new TrackingServices();
        TrackingDS cartons = new TrackingDS();
        cartons.Merge(svcs.GetCartons(trackingNumbers.ToString(),searchBy,profile.Type,profile.ClientVendorID));
        if(cartons.CartonDetailTable.Rows.Count > 0) {
            //Build a summary for the summary page
            TrackingDS summary = buildSummary(trackingSearch,cartons,searchBy);
            Session["TrackingSearch"] = trackingSearch;
            Session["TrackingSummary"] = summary;
            Session["TrackingDetail"] = cartons;
            Response.Redirect("CartonSummary.aspx");
        }
        else
            Master.ShowMsgBox("No records found. Please try again.");
    }
    #region Local Services: encodeInput(), isNumeric(), buildSummary(), addSummaryRow()
    private string encodeInput(string input) {
        //This method makes sure no invalid chars exist in the user input
        string cn = Server.HtmlEncode(input);
        cn = cn.Replace("'", "''");
        cn = cn.Replace("[", "[[]");
        cn = cn.Replace("%", "[%]");
        cn = cn.Replace("_", "[_]");
        cn = cn.Replace(",", "\r");
        cn = cn.Replace("\r\n", "\r");
        cn = cn.Replace("\n", "\r");
        return cn.Trim();
    }
    private bool isNumeric(string val) {
        //Determine if the specified value is numeric
        bool valIsNumeric=true;
        char[] chars = val.ToCharArray();
        for(int i=0;i<chars.Length;i++) {
            if(!char.IsNumber(val,i)) {
                valIsNumeric = false;
                break;
            }
        }
        return valIsNumeric;
    }
    private TrackingDS buildSummary(TrackingDS trackingSearch,TrackingDS cartons,string searchBy) {
        //Build a summary for the specified carton detail
        TrackingDS summary = new TrackingDS();
        foreach(TrackingDS.CartonDetailTableRow carton in cartons.CartonDetailTable.Rows) {
            //Find the row by displayNumber in cartonsSearch
            string displayNumber="";
            switch(searchBy) {
                case TrackingServices.SEARCHBY_CARTONNUMBER:
                case TrackingServices.SEARCHBY_PLATENUMBER:
                case TrackingServices.SEARCHBY_PRO:
                case TrackingServices.SEARCHBY_PO:
                    displayNumber = carton.CTN.Trim();
                    break;
                case TrackingServices.SEARCHBY_LABELNUMBER:
                    displayNumber = carton.LBL.ToString();
                    break;
                default:
                    displayNumber = carton.CTN.Trim();
                    break;
            }
            string id = "";
            TrackingDS.CartonSearchTableRow searchRow = trackingSearch.CartonSearchTable.FindBySearchID(displayNumber);
            if(searchRow != null) {
                //Already marked found?
                if(searchRow.Found) {
                    //Duplicate carton number, which is possible but lbl seq will be unique within same carton numbers
                    searchRow.Count += 1;
                    id = displayNumber + "-" + searchRow.Count.ToString();
                }
                else {
                    //Mark this carton as found
                    searchRow.Found = true;
                    searchRow.Count = 1;
                    id = displayNumber;
                }
                if(summary.CartonSummaryTable.FindByID(id) == null) addSummaryRow(id,displayNumber,carton,summary);
            }
        }
        return summary;
    }
    private void addSummaryRow(string id,string displayNumber,TrackingDS.CartonDetailTableRow carton,TrackingDS summary) {
        //Create a summary row for the specified carton
        TrackingDS.CartonSummaryTableRow record = summary.CartonSummaryTable.NewCartonSummaryTableRow();
        record.ID = id;
        record.DisplayNumber = displayNumber;
        record.CTNNumber = carton.CTN.Trim();
        record.LBLNumber = carton.LBL.ToString().Trim();
        record.CBOL = carton.IsCBOLNull() ? "" : carton.CBOL.Trim();
        if(carton.SCNTP == 3 && !carton.IsSCDNull() && carton.SCD.Trim().Length > 0) {
            record.DateTime = carton.SCD.Trim() + " " + carton.SCTM.Trim();
            if(carton.T.Trim().Length == 18 && carton.T.Trim().Substring(0, 2).ToLower() == "1z") 
                record.Status = "Rerouted: Tracking # " + carton.T.Trim();
            else {
                switch(carton.OM) {
                    case "M": record.Status = carton.ISMN == 1 ? "Delivered (Scan N/A - Manual Entry)" : "Delivered"; break;
                    case "S": record.Status = "Short At Delivery"; break;
                    case "O": record.Status = "Over At Delivery"; break;
                    case "A": record.Status = "MisRoute At Delivery"; break;
                }
            }
            record.Location = carton.SCT.Trim() + "/" + carton.SST.Trim();
        }
        else {
            if(carton.SCNTP == 3 && !carton.IsACTSDDNull() && carton.ACTSDD.Trim().Length > 0) {
                record.DateTime = carton.ACTSDD.Trim();
                record.Status = "Out For Delivery";
                record.Location = carton.SCT.Trim() + "/" + carton.SST.Trim();
            }
            else {                
                if(!carton.IsAARDNull() && carton.AARD.Trim().Length > 0) {
                    record.DateTime = carton.AARD.Trim() + " " + carton.AART.Trim();
                    if(carton.SCNTP == 1) {
                        switch(carton.OM) {
                            case "M": record.Status = "Scanned At Delivery Terminal"; break;
                            case "S": record.Status = "Short At Delivery Terminal"; break;
                            case "O": record.Status = "Over At Delivery Terminal"; break;
                            case "A": record.Status = "MisRoute At Delivery Terminal"; break;
                        }
                    }
                    else
                        record.Status = "Arrived At Delivery Terminal";
                    if(!carton.IsSAGCTNull())
                        record.Location = carton.SAGCT.Trim() + "/" + (carton.IsSAGSTNull() ? "" : carton.SAGST.Trim());
                    else
                        record.Location = (carton.IsAGCTNull() ? "" : carton.AGCT.Trim()) + "/" + (carton.IsAGSTNull() ? "" : carton.AGST.Trim());
                }
                else {
                    if(!carton.IsADPDNull() && carton.ADPD.Trim().Length > 0) {
                        record.DateTime = carton.ADPD.Trim() + " " + carton.ADPT.Trim();
                        record.Status = "Departed Sort Facility";
                        record.Location = carton.SRTLOC.Trim();
                    }
                    else {
                        if(!carton.IsASFDNull() && carton.ASFD.Trim().Length > 0) {
                            record.DateTime = carton.ASFD.Trim() + " " + carton.ASFT.Trim();
                            record.Status = "Arrived At Sort Facility";
                            record.Location = carton.SRTLOC.Trim();
                        }
                    }
                }
            }
        }
        summary.CartonSummaryTable.AddCartonSummaryTableRow(record);
    }
    #endregion
    #region Client Scripts: getClientStartupScript(), getCharLimitScript()
    private string getClientStartupScript() {
        //Client-side startup script- set focus to carton input textbox
        System.Text.StringBuilder script = new System.Text.StringBuilder();
        script.Append("<script language=javascript>");
        script.Append("document.all." + this.txtNumbers.UniqueID + ".select();");
        script.Append("</script>");
        return script.ToString();
    }
    private string getCharLimitScript() {
        //
        System.Text.StringBuilder script = new System.Text.StringBuilder();
        script.Append("<script language=javascript>");

        //checkTextLen JS Function
        script.Append(" function checkTextLen(field, maxlimit) {");
        script.Append(" if ( field.value.length > maxlimit )");
        script.Append(" { field.value = field.value.substring( 0, maxlimit );");
        script.Append("  alert( '" + "Length of the carton number exceeded the maximum allowed." + "');");
        script.Append(" return false;}");
        script.Append(" }");

        //checkEmptyTextBox JS Function
        script.Append(" function checkEmptyTextBox(field) {");
        script.Append(@" if ( field.value.replace(/^\s+/,'').replace(/\s+$/,'') == '' )");
        script.Append(" { ");
        script.Append("  alert('" + "No valid tracking numbers were entered." + "');");
        script.Append(" return false;}");
        script.Append(" else return true;");
        script.Append(" }");

        //removeNonNumerics JS Function
        script.Append(" function removeNonNumerics(evt) {");
        script.Append(" if (document.all.TrackingSelection[1].checked){");
        script.Append(" var keyCode = evt.which ? evt.which : evt.keyCode;");
        script.Append(" if ((keyCode > '0'.charCodeAt() && keyCode <= '9'.charCodeAt()) || (keyCode == 13 || keyCode == 188)) ");
        script.Append(" return true;");
        script.Append(" else return false;");
        script.Append(" }");
        script.Append(" else return true;");
        script.Append(" }");
        script.Append("</script>");
        return script.ToString();
    }
    #endregion
}
