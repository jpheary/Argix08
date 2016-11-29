using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Argix;
using Tsort.Devices;

namespace Tsort.Sort {
    //
    public partial class frmMain :Form {
        //Members
        private StationOperator mOperator=null;
        private bool mOperatorWorking=false;
        private Icon icon_idle = null,icon_on = null,icon_off = null;

        #region Carton Status constants
        public const int STATUS_NONE = 0;
        public const int STATUS_CARTON_OK = 1;
        public const int STATUS_SCANERROR_NODATA = 2;
        public const int STATUS_SCANERROR_NOREAD = 3;
        public const int STATUS_SCANERROR_CONFLICT = 4;
        public const int STATUS_SCANERROR_LABELFAILED = 5;
        public const int STATUS_SCANERROR_WEIGHTBAD = 8;
        public const int STATUS_CARTON_VERIFYONLY = 6;
        public const int STATUS_CARTON_IGNORE = 7;
        public const int STATUS_ERROR_UNKNOWN = 9;
        #endregion
        #region Carton Verify constants
        public const string VERIFY_PASS = "1";
        public const string VERIFY_FAIL = "2";
        public const string VERIFY_NOREAD = "3";

        public const string VERIFY_YES = "Y";
        public const string VERIFY_NO = "N";
        #endregion
        
        //Interface
        public frmMain() {
            //Constructor
            InitializeComponent();
            this.Text = "Argix Direct Station Program";
            this.icon_idle = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Tsort.Resources.idle.ico"));
            this.icon_on = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Tsort.Resources.on.ico"));
            this.icon_off = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Tsort.Resources.off.ico"));

            this.mOperator = new StationOperator(App.Mediator.Connection);
        }
        private void OnLoad(object sender,EventArgs e) {
            //Event handler for form load event
            this.mOperator.Station.PrinterChanged += new EventHandler(OnPrinterChanged);
            this.mOperator.Station.ScaleChanged += new EventHandler(OnScaleChanged);
            this.mOperator.Station.SetDefaultPrinter();
            this.mOperator.Station.SetDefaultScale();
            this.txtWeight.Text = "0.0";
            OnInputSelected(this.tsmItem2,EventArgs.Empty);
            reset();
            this.sortUI1.Operator = this.mOperator;
            //this.sortUI1.Operator.DataStatusUpdate += new DataStatusHandler(OnDataStatusUpdate);
            RefreshSortService();

            //Test
            this.txtInput1.Text = "4100799991927440";
            this.txtInput2.Text = "00500497253066300011";
        }
        private void OnInputSelected(object sender,EventArgs e) {
            //Event handler for user changed input requirement
            ToolStripMenuItem tsb = (ToolStripMenuItem)sender;
            this.cboInputCount.Text = tsb.Text;
            this.txtInput1.Enabled = int.Parse(tsb.Text) > 0;
            this.txtInput2.Enabled = int.Parse(tsb.Text) > 1;
            this.txtInput3.Enabled = int.Parse(tsb.Text) > 2;
            this.txtInput4.Enabled = int.Parse(tsb.Text) > 3;
            this.txtInput5.Enabled = int.Parse(tsb.Text) > 4;
            this.txtInput6.Enabled = int.Parse(tsb.Text) > 5;
        }
        private void OnInputChanged(object sender,EventArgs e) {
            //Event handler for change in an input
            TextBox txt = (TextBox)sender;
            //Set next input
            if(txt.Text.EndsWith("@")) {
                if(txt.Name != "txtInput6") setNextInput(txt);
            }
            if(this.txtInput1.Enabled && this.txtInput1.Text.EndsWith("@") &&
                (!this.txtInput2.Enabled || (this.txtInput2.Enabled && this.txtInput2.Text.EndsWith("@"))) &&
                (!this.txtInput3.Enabled || (this.txtInput3.Enabled && this.txtInput3.Text.EndsWith("@"))) &&
                (!this.txtInput4.Enabled || (this.txtInput4.Enabled && this.txtInput4.Text.EndsWith("@"))) &&
                (!this.txtInput5.Enabled || (this.txtInput5.Enabled && this.txtInput5.Text.EndsWith("@"))) &&
                (!this.txtInput6.Enabled || (this.txtInput6.Enabled && this.txtInput6.Text.EndsWith("@")))) {
                
                //Capture
                decimal weight = Convert.ToDecimal(this.txtWeight.Text);
                string[] inputs = new string[] { this.txtInput1.Text,this.txtInput2.Text,this.txtInput3.Text,this.txtInput4.Text,this.txtInput5.Text,this.txtInput6.Text };
                PandaDS ds = ProcessLabelDataRequest(inputs,weight);
                if(ds.PandaTable[0].StatusCode == STATUS_CARTON_OK) {
                    //Success
                    this.mOperator.Station.Printer.Print(ds.PandaTable[0].LabelData);
                    string code = ProcessVerifyLabelRequest(ds.PandaTable[0].CartonID,VERIFY_PASS);
                    if(code == VERIFY_YES) MessageBox.Show("PASS");
                }
                else {
                    //Failure
                    MessageBox.Show("FAIL");
                }
                reset();
            }
        }
        private void reset() {
            this.txtInput1.Text = this.txtInput2.Text = this.txtInput3.Text = this.txtInput4.Text = this.txtInput5.Text = this.txtInput6.Text = "";
            this.txtInput1.Focus();
        }
        private void setNextInput(TextBox txt) {
            if(txt.Name == "txtInput1") this.txtInput2.Focus();
            if(txt.Name == "txtInput2") this.txtInput3.Focus();
            if(txt.Name == "txtInput3") this.txtInput4.Focus();
            if(txt.Name == "txtInput4") this.txtInput5.Focus();
            if(txt.Name == "txtInput5") this.txtInput6.Focus();
        }
        #region Devices: OnPrinterChanged(), OnScaleChanged()
        private void OnPrinterChanged(object sender,EventArgs e) {
            //Event handler for change to the active printer
            try {
                if(this.mOperator.Station.Printer != null) {
                    //Configure for a new printer type
                    this.lblPrinterStatus.Image = this.mOperator.Station.Printer.On ? this.icon_on.ToBitmap() : this.icon_off.ToBitmap();
                    this.lblPrinterStatus.ToolTipText = this.mOperator.Station.Printer.Type + " (" + this.mOperator.Station.Printer.Settings.PortName + ")";
                } else {
                    //Disable printer features
                    this.lblPrinterStatus.Image = this.icon_idle.ToBitmap();
                    this.lblPrinterStatus.ToolTipText = "Printer is not set.";
                }
            }
            catch(Exception ex) { App.ReportError(ex, true); }
        }
        private void OnScaleChanged(object sender,EventArgs e) {
            //Event handler for change in the active scale
            try {
                if(this.mOperator.Station.Scale != null) {
                    //Configure for a new scale type
                    this.lblScaleStatus.Image = this.mOperator.Station.Scale.On ? this.icon_on.ToBitmap() : this.icon_off.ToBitmap();
                    this.lblScaleStatus.ToolTipText = this.mOperator.Station.Scale.Type + " (" + this.mOperator.Station.Scale.Settings.PortName + ")";
                    this.mOperator.Station.Scale.ScaleWeightReading += new Tsort.Devices.Scales.ScaleEventHandler(OnScaleWeightReading);
                } else {
                    //Disable scale features
                    this.lblScaleStatus.Image = this.icon_idle.ToBitmap();
                    this.lblScaleStatus.ToolTipText = "Scale is not set.";
                }
            }
            catch(Exception ex) { App.ReportError(ex,true); }
        }
        void OnScaleWeightReading(object source,Tsort.Devices.Scales.ScaleEventArgs e) {
            try {
                if(this.InvokeRequired) {
                    this.Invoke(new EventHandler<Tsort.Devices.Scales.ScaleEventArgs>(OnScaleWeightReading),new object[] { source,e });
                    return;
                }
                this.txtWeight.Text = e.Weight.ToString();
            }
            catch(Exception ex) { App.ReportError(ex,true); }
        }
        #endregion
        public void RefreshSortService() {
            //
            try {
                if(!this.mOperatorWorking) {
                    this.mOperator.StartWork();
                    this.mOperatorWorking = true;
                }
            }
            catch(ApplicationException ex) { App.ReportError(ex,true); }
            catch(Exception ex) { App.ReportError(new ApplicationException("Unexpected error while starting the Sort Service Operator.",ex),true); }

            try {
                this.mOperator.RefreshStationAssignments();
            }
            catch(ApplicationException ex) { App.ReportError(ex,true); }
            catch(Exception ex) { App.ReportError(new ApplicationException("Unexpected error while refreshing the Sort Service.",ex),true); }
        }
        public PandaDS ProcessLabelDataRequest(string[] inputs,decimal weight) {
            //Create a new carton
            string cartonID="",zpl="";
            int statusCode=STATUS_NONE;
            ApplicationException aex=null;
            PandaDS ds=new PandaDS();

            //Validate for non-carton requests (scanner tripped)
            bool hasInputData=false;
            string _inputs="";
            for(int i=0;i<inputs.Length;i++) {
                if(inputs[i].Trim().Length > 0) hasInputData = true;
                _inputs+= (i+1).ToString() + "=" + inputs[i] + "; ";
            }
            ArgixTrace.WriteLine(new TraceMessage("LABEL DATA REQUEST: " + _inputs + "weight=" + weight.ToString(),App.EventLogName,LogLevel.Information,"PandaSvc"));
            if(!hasInputData && weight == 0) {
                ArgixTrace.WriteLine(new TraceMessage("LABEL DATA RESPONSE: no inputs; no weight; processing aborted...",App.EventLogName,LogLevel.Information,"PandaSvc"));
                return ds;
            }
            try {
                //Begin processing a new message request
                //if(this.LabelDataRequest != null) this.LabelDataRequest(null,EventArgs.Empty);
                try {
                    //Create a default carton identifier
                    DateTime dt = DateTime.Now;
                    int sec = ((3600 * dt.Hour) + (60 * dt.Minute) + dt.Second);
                    cartonID = dt.ToString("yyyy").Substring(3,1) + dt.ToString("MM") +  dt.ToString("dd") + sec.ToString("00000") + "0000";
                    
                    //Get an outbound label for this carton
                    SortedItem sortedItem = this.mOperator.ProcessInputs(inputs,weight);
                    if(sortedItem != null) {
                        try {
                            cartonID = sortedItem.LabelNumber;
                            zpl = sortedItem.LabelFormat;
                            sortedItem.ThrowException();
                            statusCode = STATUS_CARTON_OK;
                            ArgixTrace.WriteLine(new TraceMessage("Carton processing successful [label data]...",App.EventLogName,LogLevel.Debug,"PandaSvc"));
                        }
                        catch(Tsort.InboundLabelException ex) {
                            //Test for various inbound label exceptions
                            bool allInvalid=true;
                            for(int i=0;i<inputs.Length;i++) { if(inputs[i].Trim().Length>0) { allInvalid=false; break; } }
                            if(allInvalid) {
                                statusCode = STATUS_SCANERROR_NODATA;
                                ArgixTrace.WriteLine(new TraceMessage("Message data validation failed [no data from scanner]-->" + ex.Message + (ex.InnerException!=null?"-->" + ex.InnerException.Message:"") + "...",App.EventLogName,LogLevel.Warning,"PandaSvc"));
                            }
                            allInvalid=true;
                            for(int i=0;i<inputs.Length;i++) { if(inputs[i].Replace("?","").Trim().Length>0) { allInvalid=false; break; } }
                            if(allInvalid && statusCode == STATUS_NONE) {
                                statusCode = STATUS_SCANERROR_NOREAD;
                                ArgixTrace.WriteLine(new TraceMessage("Message data validation failed [no read: ?]-->" + ex.Message + (ex.InnerException!=null?"-->" + ex.InnerException.Message:"") + "...",App.EventLogName,LogLevel.Warning,"PandaSvc"));
                            }
                            allInvalid=true;
                            for(int i=0;i<inputs.Length;i++) { if(inputs[i].Replace("#","").Trim().Length>0) { allInvalid=false; break; } }
                            if(allInvalid && statusCode == STATUS_NONE) {
                                statusCode = STATUS_SCANERROR_CONFLICT;
                                ArgixTrace.WriteLine(new TraceMessage("Message data validation failed [label conflict: #]-->" + ex.Message + (ex.InnerException!=null?"-->" + ex.InnerException.Message:"") + "...",App.EventLogName,LogLevel.Warning,"PandaSvc"));
                            }
                            if(statusCode == STATUS_NONE) {
                                statusCode = STATUS_SCANERROR_LABELFAILED;
                                ArgixTrace.WriteLine(new TraceMessage("Message data validation failed [inbound label exception]-->" + ex.Message + (ex.InnerException!=null?"-->" + ex.InnerException.Message:"") + "...",App.EventLogName,LogLevel.Warning,"PandaSvc"));
                            }
                        }
                        catch(Tsort.ZeroWeightException ex) {
                            statusCode = STATUS_SCANERROR_WEIGHTBAD;
                            ArgixTrace.WriteLine(new TraceMessage("Message data validation failed [zero weight exception]-->" + ex.Message + (ex.InnerException!=null?"-->" + ex.InnerException.Message:"") + "...",App.EventLogName,LogLevel.Warning,"PandaSvc"));
                        }
                        catch(Tsort.OverWeightException ex) {
                            statusCode = STATUS_SCANERROR_WEIGHTBAD;
                            ArgixTrace.WriteLine(new TraceMessage("Message data validation failed [over weight exception]-->" + ex.Message + (ex.InnerException!=null?"-->" + ex.InnerException.Message:"") + "...",App.EventLogName,LogLevel.Warning,"PandaSvc"));
                        }
                        catch(Tsort.DestinationRoutingException ex) {
                            statusCode = STATUS_SCANERROR_LABELFAILED;
                            ArgixTrace.WriteLine(new TraceMessage("Sorted item processing failed [destination/routing exception]-->" + ex.Message + (ex.InnerException!=null?"-->" + ex.InnerException.Message:"") + "...",App.EventLogName,LogLevel.Warning,"PandaSvc"));
                        }
                        catch(Exception ex) {
                            statusCode = STATUS_SCANERROR_LABELFAILED;
                            ArgixTrace.WriteLine(new TraceMessage("Sorted item processing failed [unexpected exception]-->" + ex.Message + (ex.InnerException!=null?"-->" + ex.InnerException.Message:"") + "...",App.EventLogName,LogLevel.Warning,"PandaSvc"));
                        }
                        zpl = zpl.Replace(Tsort.Labels.TokenLibrary.STATUSCODE,statusCode.ToString());
                    }
                    else {
                        aex = new ApplicationException("Carton processing failed [no sorted item]...");
                        statusCode = STATUS_SCANERROR_LABELFAILED;
                        zpl = createErrorLabelFormat(STATUS_SCANERROR_LABELFAILED,aex);
                        ArgixTrace.WriteLine(new TraceMessage(aex.Message,App.EventLogName,LogLevel.Warning,"PandaSvc"));
                    }
                }
                catch(Exception ex) {
                    aex = new ApplicationException("Carton processing failed [sorted item processing threw an exception]-->" + ex.Message + (ex.InnerException!=null?"-->" + ex.InnerException.Message:"") + "...",ex);
                    statusCode = STATUS_SCANERROR_LABELFAILED;
                    zpl = createErrorLabelFormat(STATUS_SCANERROR_LABELFAILED,ex);
                    ArgixTrace.WriteLine(new TraceMessage(aex.Message,App.EventLogName,LogLevel.Warning,"PandaSvc"));
                }
                ds.PandaTable.AddPandaTableRow(cartonID,statusCode,zpl);
                ds.AcceptChanges();
            }
            catch(Exception ex) {
                aex = new ApplicationException("Carton processing failed [unexpected exception]-->" + ex.Message + (ex.InnerException!=null?"-->" + ex.InnerException.Message:"") + "...",ex);
                statusCode = STATUS_ERROR_UNKNOWN;
                zpl = createErrorLabelFormat(STATUS_ERROR_UNKNOWN,ex);
                ds.PandaTable.AddPandaTableRow(cartonID,statusCode,zpl);
                ds.AcceptChanges();
                ArgixTrace.WriteLine(new TraceMessage(aex.Message,App.EventLogName,LogLevel.Warning,"PandaSvc"));
            }
            finally {
                ArgixTrace.WriteLine(new TraceMessage("LABEL DATA RESPONSE: " + cartonID + "; " + statusCode.ToString(),App.EventLogName,LogLevel.Information,"PandaSvc"));
            //    if(this.LabelDataRequestComplete != null) {
            //        PandaDS _ds = new PandaDS();
            //        _ds.CartonTable.AddCartonTableRow(cartonID,(inputs.Length>0?inputs[0]:""),(inputs.Length>1?inputs[1]:""),(inputs.Length>2?inputs[2]:""),(inputs.Length>3?inputs[3]:""),(inputs.Length>4?inputs[4]:""),(inputs.Length>5?inputs[5]:""),weight,statusCode,zpl,"",(aex!=null?aex.Message:""),"");
            //        this.LabelDataRequestComplete(this,new PandaLabelDataEventArgs(_ds.CartonTable[0]));
            //    }
            }
            return ds;
        }
        public string ProcessVerifyLabelRequest(string cartonID,string verifyCode) {
            //Verify the requested carton
            string verifyFlag=VERIFY_NO;
            ApplicationException aex=null;
            try {
                //Begin processing a new message request
                ArgixTrace.WriteLine(new TraceMessage("VERIFY LABEL REQUEST: ID=" + cartonID + "; verify code=" + verifyCode,App.EventLogName,LogLevel.Information,"PandaSvc"));

                //Validate
                if(!(cartonID.Length == 14 || cartonID.Length == 24))
                    throw new ApplicationException("Invalid cartonID length (" + cartonID.Length.ToString() + "; must be 14 0r 24)...");

                //Find the carton
                SortedItem item = this.mOperator.GetSortedItem(cartonID.Length == 24 ? cartonID.Substring(10,13) : cartonID.Substring(0,13));
                if(item != null) {
                    //Item found
                    if(verifyCode == VERIFY_PASS) {
                        //Pass: Save carton
                        try {
                            this.mOperator.StoreSortedItem(item);
                            verifyFlag = VERIFY_YES;
                            ArgixTrace.WriteLine(new TraceMessage("Carton save succeeded...",App.EventLogName,LogLevel.Information,"PandaSvc"));
                        }
                        catch(Exception ex) {
                            verifyFlag = VERIFY_NO;
                            aex = new ApplicationException("Carton save failed...-->" + ex.Message,ex);
                            ArgixTrace.WriteLine(new TraceMessage(aex.Message,App.EventLogName,LogLevel.Error,"PandaSvc"));
                        }
                    }
                    else if(verifyCode == VERIFY_FAIL || verifyCode == VERIFY_NOREAD) {
                        //Fail, No Read: Cancel carton
                        verifyFlag = VERIFY_NO;
                        this.mOperator.CancelSortedItem(item.LabelSeqNumber);
                        ArgixTrace.WriteLine(new TraceMessage("Verify flag was fail or no read; carton cancelled...",App.EventLogName,LogLevel.Information,"PandaSvc"));
                    }
                    else {
                        //Fail, No Read: Cancel carton
                        verifyFlag = VERIFY_NO;
                        this.mOperator.CancelSortedItem(item.LabelSeqNumber);
                        ArgixTrace.WriteLine(new TraceMessage("Verify flag was unknown; carton cancelled...",App.EventLogName,LogLevel.Warning,"PandaSvc"));
                    }
                }
                else {
                    //Carton not found
                    verifyFlag = VERIFY_NO;
                    ArgixTrace.WriteLine(new TraceMessage("Carton not found...",App.EventLogName,LogLevel.Warning,"PandaSvc"));
                }
            }
            catch(ApplicationException ex) {
                verifyFlag = VERIFY_NO;
                aex = ex;
                ArgixTrace.WriteLine(new TraceMessage(ex.Message,App.EventLogName,LogLevel.Error,"PandaSvc"));
            }
            catch(Exception ex) {
                verifyFlag = VERIFY_NO;
                aex = new ApplicationException("An unexpected error occurred while processing verify label request-->" + ex.Message + "...",ex);
                ArgixTrace.WriteLine(new TraceMessage("An unexpected error occurred while processing verify label request-->" + ex.Message + "...",App.EventLogName,LogLevel.Error,"PandaSvc"));
            }
            finally {
                ArgixTrace.WriteLine(new TraceMessage("VERIFY LABEL RESPONSE: " + cartonID + "; " + verifyFlag,App.EventLogName,LogLevel.Information,"PandaSvc"));
            //    if(this.VerifyLabelRequestComplete != null) this.VerifyLabelRequestComplete(this,new PandaVerifyLabelEventArgs(cartonID,verifyFlag,aex));
            }
            return verifyFlag;
        }
        #region Local Services: createErrorLabelFormat()
        private string createErrorLabelFormat(int statusCode,Exception ex) {
            //Exception occurred durin carton processing; create an error
            //outbound label for this carton (this will help identify rejects)
            string labelFormat="";
            labelFormat =	"^XA" + 
							"^FWN^CFD,24^PW812" + 
							"^A0B,45,64^FO103,962^FR^FD<lanePrefix><zoneLane>^FS" + 
							"^A0B,23,16^FO752,360^FR^FDCOPYRIGHT <currentYear>FS" + 
							"^A0B,26,22^FO748,207^FR^FD ARGIX DIRECT ^FS" + 
							"^ADB,18,10^FO748,112^FR^FD88-PAX^FS" + 
							"^FO734,39^FR^GB45,1149,45,B,0^FS" + 
							"^A0B,112,72^FO91,498^FR^FDError!!!^FS" + 
							"^A0B,34,34^FO410,90^FR^FD<messageText_Line1> ^FS" + 
							"^A0B,34,34^FO451,90^FR^FD<messageText_Line2> ^FS" + 
							"^A0B,34,34^FO491,90^FR^FD<messageText_Line3> ^FS" + 
							"^A0B,34,34^FO532,90^FR^FD<messageText_Line4> ^FS" + 
							"^A0B,34,34^FO573,90^FR^FD<messageText_Line5> ^FS" + 
							"^A0B,112,72^FO89,211^FR^FD<statusCode>^FS" + 
							"^ADB,18,10^FO58,285^FR^FDMsg#:^FS" + 
							"^A0B,26,22^FO746,878^FR^FD<currentDate>  <currentTime>^FS" + 
							"^BY4,2.0^FO176,951^FR^B2B,227,N,N,N^FD<lanePrefix><zoneLane>^FS" + 
							"^ADB,18,10^FO207,407^FR^FDCarton#:^FS" + 
							"^A0B,45,46^FO236,78^FR^FD<cartonNumber>^FS" + 
							"^A0B,45,46^FO234,585^FR^FD<sortedItemLabelNumber>^FS" + 
							"^ADB,18,10^FO207,794^FR^FDLSeq#:^FS" + 
							"^A0B,26,22^FO467,1101^FR^FDClient:^FS" + 
							"^A0B,26,22^FO505,1101^FR^FDShipper:^FS" + 
							"^A0B,26,22^FO540,1101^FR^FDPU Date:^FS" + 
							"^A0B,26,22^FO575,1101^FR^FDPU #:^FS" + 
							"^A0B,26,22^FO467,1013^FR^FD<clientNumber>-<clientDivisionNumber>^FS" + 
							"^A0B,26,22^FO505,1033^FR^FD<shipperNumber>^FS" + 
							"^A0B,26,22^FO540,1010^FR^FD<freightPickupDate>^FS" + 
							"^A0B,26,22^FO575,1075^FR^FD<freightPickupNumber>^FS" + 
							"^XZ";
            labelFormat = labelFormat.Replace(Tsort.Labels.TokenLibrary.STATUSCODE,statusCode.ToString());
            string message = ex.Message;
            if(ex.InnerException != null) {
                message += " -->" + ex.InnerException.Message;
                if(ex.InnerException.InnerException != null) message += " -->" + ex.InnerException.InnerException.Message;
            }
            labelFormat = labelFormat.Replace(Tsort.Labels.TokenLibrary.MESSAGETEXT,message);
            if(message.Length > 0)
                labelFormat = labelFormat.Replace("<messageText_Line1>",(message.Length < 50 ? message.Substring(0).PadRight(50,' ') : message.Substring(0,50).PadRight(50,' ')));
            else
                labelFormat = labelFormat.Replace("<messageText_Line1>","");
            if(message.Length > 45)
                labelFormat = labelFormat.Replace("<messageText_Line2>",(message.Length < 100 ?  message.Substring(50).PadRight(50,' ') : message.Substring(50,50).PadRight(50,' ')));
            else
                labelFormat = labelFormat.Replace("<messageText_Line2>","");
            if(message.Length > 90)
                labelFormat = labelFormat.Replace("<messageText_Line3>",(message.Length < 150 ? message.Substring(100).PadRight(50,' ') : message.Substring(100,50).PadRight(50,' ')));
            else
                labelFormat = labelFormat.Replace("<messageText_Line3>","");
            if(message.Length > 135)
                labelFormat = labelFormat.Replace("<messageText_Line4>",(message.Length < 200 ? message.Substring(150).PadRight(50,' ') : message.Substring(150,50).PadRight(50,' ')));
            else
                labelFormat = labelFormat.Replace("<messageText_Line4>","");
            if(message.Length > 180)
                labelFormat = labelFormat.Replace("<messageText_Line5>",(message.Length < 250 ? message.Substring(200).PadRight(50,' ') : message.Substring(200,50).PadRight(50,' ')));
            else
                labelFormat = labelFormat.Replace("<messageText_Line5>","");
            labelFormat = labelFormat.Replace(Tsort.Labels.TokenLibrary.LANEPREFIX,"00");
            labelFormat = labelFormat.Replace(Tsort.Labels.TokenLibrary.ZONELANE,"00");
            labelFormat = labelFormat.Replace(Tsort.Labels.TokenLibrary.CURRENTDATE,DateTime.Today.ToString("MM/dd/yyyy"));
            labelFormat = labelFormat.Replace(Tsort.Labels.TokenLibrary.CURRENTTIME,DateTime.Now.ToString("HH:mm"));
            labelFormat = labelFormat.Replace(Tsort.Labels.TokenLibrary.CURRENTYEAR,DateTime.Today.ToString("yyyy"));
            return labelFormat;
        }
        #endregion

        private void OnPrinterClick(object sender,EventArgs e) {
            try {
                string zpl = "^XA" + 
                            "^FWN^CFD,24^PW609^LH0,0" + 
                            "^FO0,1^FR^GB45,1014,45,B,0^FS" + 
                            "^FO489,344^FR^GB76,124,5,B,0^FS" + 
                            "^A0R,25,22^FO6,190^FR^FD3^FS" + 
                            "^A0R,25,22^FO6,234^FR^FDlbs^FS" + 
                            "^A0R,25,22^FO6,338^FR^FD00101^FS" + 
                            "^A0R,25,22^FO6,503^FR^FD^FS" + 
                            "^A0R,26,22^FO6,44^FR^FDC Weight: ^FS" + 
                            "^ADR,18,10^FO301,339^FR^FDTL:^FS" + 
                            "^A0R,53,48^FO244,339^FR^FD121470W ^FS" + 
                            "^A0R,57,78^FO477,475^FR^FD^FS" + 
                            "^A0R,67,64^FO480,357^FR^FDW ^FS" + 
                            "^ADR,18,10^FO546,477^FR^FDStore:^FS" + 
                            "^BY2^FO336,342^FR^BCR,87,N,N,N^FD&gt;:00500497253066300011^FS" + 
                            "^A0R,23,24^FO309,514^FR^FD00500497253066300011^FS" + 
                            "^A0R,42,24^FO430,352^FR^FD^FS" + 
                            "^A0R,42,24^FO252,568^FR^FDB&amp;N DISTRIBUTION #1^FS" + 
                            "^A0R,110,72^FO459,687^FR^FD^FS" + 
                            "^BY3^FO78,339^FR^BCR,153,N,N,N^FD&gt;;0014400112557910930^FS" + 
                            "^A0R,23,30^FO50,411^FR^FD001 44  0011255791093 0^FS" + 
                            "^BY4,2.0^FO262,69^FR^B2R,227,N,N,N^FD995600^FS" + 
                            "^A0R,45,64^FO508,78^FR^FD995600^FS" + 
                            "^BY3^FO79,886^FR^BCN,95,N,N,N^FD&gt;:$$19^FS" + 
                            "^A0N,28,28^FO171,982^FR^FD$$19^FS" + 
                            "^A0R,23,16^FO7,600^FR^FDCOPYRIGHT 2010^FS" + 
                            "^A0R,26,22^FO6,723^FR^FD ARGIX DIRECT ^FS" + 
                            "^A0R,23,18^FO7,923^FR^FD47A^FS" + 
                            "^MCY" + 
                            "^XZ";
                this.mOperator.Station.Printer.Print(zpl);
            }
            catch(Exception ex) { App.ReportError(ex,true); }
        }
    }
}