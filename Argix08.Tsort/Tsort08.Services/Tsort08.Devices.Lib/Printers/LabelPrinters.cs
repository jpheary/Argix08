//	File:	LabelPrinters.cs
//	Author:	J. Heary
//	Date:	11/10/08
//	Desc:	Control for label printers selection and manipulation.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tsort.Devices.Printers {
    /// <summary>Control for label printers selection and manipulation.</summary>
    public partial class LabelPrinters:UserControl {
        //Members
        private ILabelPrinter mPrinter=null;
        public event EventHandler PrinterChanged = null;

        //Interface
        /// <summary></summary>
        public LabelPrinters() {
            InitializeComponent();
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Returns the currently selected label printer.")]
        [HelpKeywordAttribute("Tsort.Devices.Printers.LabelPrinters.Printer")]
        [Localizable(false)]
        public ILabelPrinter Printer {
            get { return this.mPrinter; }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets the number of items in the printer items collection.")]
        [HelpKeywordAttribute("Tsort.Devices.Printers.LabelPrinters.PrinterItemsCount")]
        [Localizable(false)]
        public int PrinterItemsCount { get { return this.cboPrinter.Items.Count; } }
        private bool ShouldSerializePrinterItemsCount() { return false; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets or sets the index specifying the currently selected printer.")]
        [HelpKeywordAttribute("Tsort.Devices.Printers.LabelPrinters.PrinterSelectedIndex")]
        [Localizable(false)]
        public int PrinterSelectedIndex { get { return this.cboPrinter.SelectedIndex; } set { this.cboPrinter.SelectedIndex = value; OnPrinterChanged(null,EventArgs.Empty); } }
        private bool ShouldSerializePrinterSelectedIndex() { return false; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets or sets the value of the member property specified by the System.Windows.Forms.ListControl.ValueMember property of the printer control.")]
        [HelpKeywordAttribute("Tsort.Devices.Printers.LabelPrinters.PrinterSelectedValue")]
        [Localizable(false)]
        public object PrinterSelectedValue { get { return this.cboPrinter.SelectedValue; } set { if(value != null) this.cboPrinter.SelectedValue = value; OnPrinterChanged(null,EventArgs.Empty); } }
        private bool ShouldSerializePrinterSelectedValue() { return false; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets or sets the text associated with the printer control.")]
        [HelpKeywordAttribute("Tsort.Devices.Printers.LabelPrinters.PrinterText")]
        public string PrinterText { get { return this.cboPrinter.Text; } set { this.cboPrinter.Text = value; } }
        private bool ShouldSerializePrinterText() { return false; }

        private void OnControlLoad(object sender,EventArgs e) {
            //Event handler for control load event
            this.cboPrinter.Items.AddRange(DeviceFactory.PrinterTypes);
            this.cboPort.SelectedIndex = 0;
            this.cboBaud.SelectedIndex = 0;
            this.cboDataBits.SelectedIndex = 0;
            this.cboParity.SelectedIndex = 0;
            this.cboStopBits.SelectedIndex = 0;
            
            this.cboPrinter.SelectedIndex = 0;
            OnPrinterChanged(this.cboPrinter,EventArgs.Empty);
        }
        private void OnPrinterChanged(object sender,System.EventArgs e) {
            //Event handler for change in port
            try {
                if(this.mPrinter != null && this.mPrinter.On) this.mPrinter.TurnOff();
                this.mPrinter = DeviceFactory.CreatePrinter(this.cboPrinter.SelectedItem.ToString(),this.cboPort.SelectedItem.ToString());
                if(this.mPrinter != null && this.mPrinter.Settings.PortName != null) {
                    this.cboPort.SelectedItem = this.mPrinter.Settings.PortName;
                    this.cboBaud.SelectedItem = this.mPrinter.Settings.BaudRate.ToString();
                    this.cboDataBits.SelectedItem = this.mPrinter.Settings.DataBits.ToString();
                    this.cboParity.SelectedItem = this.mPrinter.Settings.Parity.ToString();
                    this.cboStopBits.SelectedItem = Convert.ToInt32(this.mPrinter.Settings.StopBits).ToString();
                }
                else {
                    this.cboPort.SelectedIndex = 0;
                    this.cboBaud.SelectedIndex = 0;
                    this.cboDataBits.SelectedIndex = 0;
                    this.cboParity.SelectedIndex = 0;
                    this.cboStopBits.SelectedIndex = 0;
                }
                if(this.PrinterChanged != null) this.PrinterChanged(this.mPrinter,EventArgs.Empty);
            }
            catch(Exception ex) { reportError(ex); }
            finally { setUserServices(); }
        }
        private void OnPortNameChanged(object sender,System.EventArgs e) {
            //Event handler for change in port
            try {
                if(this.mPrinter != null && !this.mPrinter.On) {
                    PortSettings settings = this.mPrinter.Settings;
                    settings.PortName = this.cboPort.Text;
                    this.mPrinter.Settings = settings;
                    //LogEvent(LOG_ID_PORT + ": Name= " + this.mPrinter.Settings.PortName + "\n");
                }
            }
            catch(Exception ex) { reportError(ex); }
            finally { setUserServices(); }
        }
        private void OnPortBaudRateChanged(object sender,System.EventArgs e) {
            //Event handler for change in baud rate
            try {
                if(this.mPrinter != null && this.mPrinter.On) {
                    PortSettings settings = this.mPrinter.Settings;
                    settings.BaudRate = Convert.ToInt32(this.cboBaud.Text);
                    this.mPrinter.Settings = settings;
                    //LogEvent(LOG_ID_PORT + ": BaudRate= " + this.mPrinter.Settings.BaudRate.ToString() + "\n");
                }
            }
            catch(Exception ex) { reportError(ex); }
            finally { setUserServices(); }
        }
        private void OnPortDataBitsChanged(object sender,System.EventArgs e) {
            //Event handler for change in data bits
            try {
                if(this.mPrinter != null && this.mPrinter.On) {
                    PortSettings settings = this.mPrinter.Settings;
                    settings.DataBits = Convert.ToInt32(this.cboDataBits.Text);
                    this.mPrinter.Settings = settings;
                    //LogEvent(LOG_ID_PORT + ": DataBits= " + this.mPrinter.Settings.DataBits.ToString() + "\n");
                }
            }
            catch(Exception ex) { reportError(ex); }
            finally { setUserServices(); }
        }
        private void OnPortParityChanged(object sender,System.EventArgs e) {
            //Event handler for change in parity
            try {
                if(this.mPrinter != null && this.mPrinter.On) {
                    PortSettings settings = this.mPrinter.Settings;
                    switch(this.cboParity.Text.ToLower()) {
                        case "none": settings.Parity = System.IO.Ports.Parity.None; break;
                        case "even": settings.Parity = System.IO.Ports.Parity.Even; break;
                        case "odd": settings.Parity = System.IO.Ports.Parity.Odd; break;
                    }
                    this.mPrinter.Settings = settings;
                    //LogEvent(LOG_ID_PORT + ": Parity= " + this.mPrinter.Settings.Parity.ToString() + "\n");
                }
            }
            catch(Exception ex) { reportError(ex); }
            finally { setUserServices(); }
        }
        private void OnPortStopBitsChanged(object sender,System.EventArgs e) {
            //Event handler for change in stop bits
            try {
                if(this.mPrinter != null && this.mPrinter.On) {
                    PortSettings settings = this.mPrinter.Settings;
                    switch(Convert.ToInt32(this.cboStopBits.Text)) {
                        case 1: settings.StopBits = System.IO.Ports.StopBits.One; break;
                        case 2: settings.StopBits = System.IO.Ports.StopBits.Two; break;
                    }
                    this.mPrinter.Settings = settings;
                    //LogEvent(LOG_ID_PORT + ": StopBits= " + this.mPrinter.Settings.StopBits.ToString() + "\n");
                }
            }
            catch(Exception ex) { reportError(ex); }
            finally { setUserServices(); }
        }
        private void OnAutoFindPrinter(object sender,System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
            //Event handler for user selected to auto find printer
            bool found=false;
            try {
                this.cboPort.Enabled = false;
                this.cboBaud.Enabled = this.cboDataBits.Enabled = this.cboParity.Enabled = this.cboStopBits.Enabled = false;
                for(int i=0;i<this.cboPort.Items.Count;i++) {
                    //Open the printer on each port
                    if(found) break;
                    this.cboPort.SelectedIndex = i;
                    try {
                        if(this.mPrinter.On) this.mPrinter.TurnOff();
                        PortSettings settings = this.mPrinter.Settings;
                        settings.PortName = this.cboPort.Text;
                        this.mPrinter.Settings = settings;
                        this.mPrinter.TurnOn();
                    }
                    catch(Exception) { }
                    if(this.mPrinter.On) {
                        //Vary the port settings if the printer port is open
                        for(int j=0;j<this.cboBaud.Items.Count;j++) {
                            if(found) break;
                            this.cboBaud.SelectedIndex = j;
                            for(int k=0;k<this.cboDataBits.Items.Count;k++) {
                                if(found) break;
                                this.cboDataBits.SelectedIndex = k;
                                for(int m=0;m<this.cboParity.Items.Count;m++) {
                                    if(found) break;
                                    this.cboParity.SelectedIndex = m;
                                    for(int n=0;n<this.cboStopBits.Items.Count;n++) {
                                        this.cboStopBits.SelectedIndex = n;
                                        PortSettings settings = this.mPrinter.Settings;
                                        settings.BaudRate = Convert.ToInt32(this.cboBaud.Text);
                                        settings.DataBits = Convert.ToInt32(this.cboDataBits.Text);
                                        switch(this.cboParity.Text.ToLower()) {
                                            case "none": settings.Parity = System.IO.Ports.Parity.None; break;
                                            case "even": settings.Parity = System.IO.Ports.Parity.Even; break;
                                            case "odd": settings.Parity = System.IO.Ports.Parity.Odd; break;
                                        }
                                        switch(Convert.ToInt32(this.cboStopBits.Text)) {
                                            case 1: settings.StopBits = System.IO.Ports.StopBits.One; break;
                                            case 2: settings.StopBits = System.IO.Ports.StopBits.Two; break;
                                        }
                                        this.mPrinter.Settings = settings;
                                        //LogEvent(LOG_ID_PORT + ": Auto Find Printer @" +  this.mPrinter.Settings.PortName + "," +  this.mPrinter.Settings.BaudRate.ToString() + "," +  this.mPrinter.Settings.DataBits.ToString() + "," +  this.mPrinter.Settings.Parity.ToString() + "," +  this.mPrinter.Settings.StopBits.ToString() + "\n");
                                        Application.DoEvents();
                                        //refreshPrinterStatus();
                                        //found = (this.lblModel.Text.Length > 7);
                                        if(found) break;
                                    }
                                }
                            }
                        }
                    }
                }
                //OnRS232DCEChanged(null,null);
            }
            catch(Exception) { }
        }
        #region Local Services: reportError(), 
        private void setUserServices() {
            //Set user services depending upon an item selected in the grid
            try {
                //Set main menu and context menu states
                this.cboPrinter.Enabled = true;
                this.cboPort.Enabled = !this.mPrinter.On;
                this.cboBaud.Enabled = this.cboDataBits.Enabled = this.cboParity.Enabled = this.cboStopBits.Enabled = true;
                this.lnkFindPrinter.Enabled = !this.mPrinter.On;
            }
            catch(Exception ex) { reportError(ex); }
        }
        private void reportError(Exception ex) {
            //Report an exception to the user
            try {
                string src = (ex.Source != null) ? ex.Source + "-\n" : "";
                string msg = src + ex.Message;
                if(ex.InnerException != null) {
                    if((ex.InnerException.Source != null)) src = ex.InnerException.Source + "-\n";
                    msg = src + ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
                }
            }
            catch(Exception) { }
        }
        #endregion

        private void OnEnter(object sender,EventArgs e) {
            //Event handler for Enter event
        }
        private void OnLeave(object sender,EventArgs e) {
            //Event handler for Enter event
        }
        private void OnMouseEnter(object sender,EventArgs e) {
            //Event handler for MouseEnter event
        }
        private void OnMouseLeave(object sender,EventArgs e) {
            //Event handler for MouseLeave event
        }
    }
}
