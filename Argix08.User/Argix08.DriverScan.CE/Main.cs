using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Symbol;

namespace Argix {
    //
    public partial class Main:Form {
        //Members
        private bool mIsReaderInitialized = false;  // The flag to track whether the reader has been initialized or not.
        TsortCacheSyncAgent mSyncAgent;            //form instanced SyncAgent to handle any events

        //All the barcode scanner - related operations in this sample would be carried out by using this reference 
        //of mScanAPI which is an instance of the class ScanSampleAPI. Will be initialized later in the code.
        private BarcodeAPI mScanAPI = null;

        private EventHandler mReadNotifyHandler = null;
        private EventHandler mStatusNotifyHandler = null;
        private EventHandler mFormActivatedEventHandler = null;
        private EventHandler mFormDeactivatedEventHandler = null;

        //Interface
        public Main() {
            //Constructor
            try {
                InitializeComponent();
                this.mSyncAgent = new TsortCacheSyncAgent(new Sync.DriverScanService(Settings.Default.WebServiceURL));
                this.mSyncAgent.Client.SyncDirection = Microsoft.Synchronization.Data.SyncDirection.DownloadOnly;
                this.mSyncAgent.Client.CreationOption = Microsoft.Synchronization.Data.TableCreationOption.DropExistingOrCreateNewTable;
                this.mSyncAgent.Store.SyncDirection = Microsoft.Synchronization.Data.SyncDirection.DownloadOnly;
                this.mSyncAgent.Store.CreationOption = Microsoft.Synchronization.Data.TableCreationOption.DropExistingOrCreateNewTable;
                this.mSyncAgent.ScanData.SyncDirection = Microsoft.Synchronization.Data.SyncDirection.UploadOnly;
                this.mSyncAgent.ScanData.CreationOption = Microsoft.Synchronization.Data.TableCreationOption.DropExistingOrCreateNewTable;
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnFormLoad(object sender,EventArgs e) {
            //Event handler for form load event
            try {
                // Initialize the ScanSampleAPI reference.
                this.mScanAPI = new Symbol.BarcodeAPI();
                this.mIsReaderInitialized = this.mScanAPI.InitReader();
                if(!(this.mIsReaderInitialized)) {
                    // Display a message & exit the application.
                    MessageBox.Show("The application will now exit.");
                    Application.Exit();
                }
                else {
                    // Clear the statusbar where subsequent status information would be displayed.
                    this.sbMain.Text = "";

                    //Set scanner behavior
                    switch(this.mScanAPI.Reader.ReaderParameters.ReaderType) {
                        case Symbol.Barcode.READER_TYPE.READER_TYPE_IMAGER: this.mScanAPI.Reader.ReaderParameters.ReaderSpecific.ImagerSpecific.AimType = Symbol.Barcode.AIM_TYPE.AIM_TYPE_TRIGGER; break;
                        case Symbol.Barcode.READER_TYPE.READER_TYPE_LASER: this.mScanAPI.Reader.ReaderParameters.ReaderSpecific.LaserSpecific.AimType = Symbol.Barcode.AIM_TYPE.AIM_TYPE_TRIGGER; break;
                        case Symbol.Barcode.READER_TYPE.READER_TYPE_CONTACT:    /* AimType is not supported by the contact readers. */ break;
                    }
                    this.mScanAPI.Reader.Actions.SetParameters();
                    switch(this.mScanAPI.Reader.ReaderParameters.ReaderType) {
                        case Symbol.Barcode.READER_TYPE.READER_TYPE_IMAGER: this.mScanAPI.Reader.ReaderParameters.ReaderSpecific.ImagerSpecific.AimMode = Symbol.Barcode.AIM_MODE.AIM_MODE_DOT; break;
                        case Symbol.Barcode.READER_TYPE.READER_TYPE_LASER: this.mScanAPI.Reader.ReaderParameters.ReaderSpecific.LaserSpecific.AimMode = Symbol.Barcode.AIM_MODE.AIM_MODE_DOT; break;
                        case Symbol.Barcode.READER_TYPE.READER_TYPE_CONTACT:    /* AimMode is not supported by the contact readers. */ break;
                    }
                    this.mScanAPI.Reader.Actions.SetParameters();
                    this.mScanAPI.Reader.Parameters.ScanType = Symbol.Barcode.ScanTypes.Foreground;
                    this.mScanAPI.Reader.Actions.SetParameters();
                    this.mScanAPI.Reader.Decoders.CODE128.Enabled = true;
                    this.mScanAPI.Reader.Decoders.CODE39.Enabled = false;
                    this.mScanAPI.Reader.Actions.SetParameters();

                    // Attach a status natification handler.
                    this.mStatusNotifyHandler = new EventHandler(OnAPIStatusNotify);
                    this.mScanAPI.AttachStatusNotify(this.mStatusNotifyHandler);

                    //Start a read operation & attach a handler
                    this.mScanAPI.StartRead(false);
                    this.mReadNotifyHandler = new EventHandler(OnAPIReadNotify);
                    this.mScanAPI.AttachReadNotify(this.mReadNotifyHandler);
                }
                this.mFormActivatedEventHandler = new EventHandler(OnFormActivated);
                this.mFormDeactivatedEventHandler = new EventHandler(OnFormDeactivated);
                this.Activated += this.mFormActivatedEventHandler;
                this.Deactivate += this.mFormDeactivatedEventHandler;

                //Populate clients
                this.mClients.Merge(TerminalsFactory.GetClients());
                if(this.cboClient.Items.Count > 0) this.cboClient.SelectedIndex = 0;
                OnClientChanged(null,EventArgs.Empty);
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnFormActivated(object sender,EventArgs e) {
            try {
                this.mScanAPI.StartRead(false);
                this.mScanAPI.AttachStatusNotify(this.mStatusNotifyHandler);
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnFormDeactivated(object sender,EventArgs e) {
            try {
                this.mScanAPI.StopRead();
                this.mScanAPI.DetachStatusNotify();
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnFormClosing(object sender,System.ComponentModel.CancelEventArgs e) {
            // Do not exit the app if not on the main screen. Simply return to the previous screen.
            try {
                if(this.mIsReaderInitialized) {
                    this.mScanAPI.DetachReadNotify();
                    this.mScanAPI.DetachStatusNotify();
                    this.mScanAPI.TermReader();
                }
                this.Activated -= this.mFormActivatedEventHandler;
                this.Deactivate -= this.mFormDeactivatedEventHandler;
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnClientChanged(object sender,EventArgs e) {
            //Event handler for change in client selected value event
            try {
                //Update store selections
                this.mStores.Clear();
                if(this.cboClient.SelectedValue != null)
                    this.mStores.Merge(TerminalsFactory.GetStores(this.cboClient.SelectedValue.ToString()));
                OnStoreChanged(null,EventArgs.Empty);
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnStoreChanged(object sender,EventArgs e) {
            //Event handler for change in store selected value
            try {
                //Clear scan
                this.txtScan.Text = "";
                this.txtScan.Focus();
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnScanChanged(object sender,EventArgs e) {
            //Event handler for change in scan value
            if(this.txtScan.Text.Length == 10 || this.txtScan.Text.Length == 13 || this.txtScan.Text.Length == 26)
                OnItemClick(this.mnuFileSave,EventArgs.Empty);
        }
        private void OnItemClick(object sender,EventArgs e) {
            //Event handler for menu item clicked
            try {
                MenuItem item = (MenuItem)sender;
                switch(item.Text.ToLower()) {
                    case "new": 
                        this.txtScan.Text = ""; 
                        break;
                    case "save": 
                        TerminalsFactory.SaveScan(this.cboClient.SelectedValue.ToString(),this.cboStore.SelectedValue.ToString(),this.txtScan.Text); 
                        break;
                    case "sync":
                        this.mSyncAgent.Synchronize();
                        this.mClients.Merge(TerminalsFactory.GetClients());
                        if(this.cboClient.Items.Count > 0) this.cboClient.SelectedIndex = 0;
                        OnClientChanged(null,EventArgs.Empty);
                        break;
                    case "close": 
                        this.Close(); 
                        break;
                    case "refresh":
                        //Populate clients
                        this.mClients.Merge(TerminalsFactory.GetClients());
                        if(this.cboClient.Items.Count > 0) this.cboClient.SelectedIndex = 0;
                        OnClientChanged(null,EventArgs.Empty);
                        break;
                    case "about": break;
                }
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnAPIReadNotify(object Sender,EventArgs e) {
            // Get ReaderData
            try {
                 Symbol.Barcode.ReaderData readerData = this.mScanAPI.Reader.GetNextReaderData();
                switch(readerData.Result) {
                    case Symbol.Results.SUCCESS:
                        //Handle the data from this read & submit the next read
                        HandleData(readerData);
                        this.mScanAPI.StartRead(false);
                        break;
                    case Symbol.Results.E_SCN_READTIMEOUT:
                        this.mScanAPI.StartRead(false);
                        break;
                    case Symbol.Results.CANCELED:
                        break;
                    case Symbol.Results.E_SCN_DEVICEFAILURE:
                        this.mScanAPI.StopRead();
                        this.mScanAPI.StartRead(false);
                        break;
                    default:
                        string sMsg = "Read Failed\n"  + "Result = "  + (readerData.Result).ToString();
                        if(readerData.Result == Symbol.Results.E_SCN_READINCOMPATIBLE) {
                            // If the failure is E_SCN_READINCOMPATIBLE, exit the application.
                            MessageBox.Show("App exiting","Failure");
                            this.Close();
                            return;
                        }
                        break;
                }
           }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnAPIStatusNotify(object Sender,EventArgs e) {
            // Get ReaderData
            try {
                Symbol.Barcode.BarcodeStatus barcodeStatus = this.mScanAPI.Reader.GetNextStatus();
                switch(barcodeStatus.State) {
                    case Symbol.Barcode.States.WAITING:
                        break;
                    case Symbol.Barcode.States.IDLE:
                        this.sbMain.Text = "Press trigger to scan";
                        break;
                    case Symbol.Barcode.States.READY:
                        this.sbMain.Text = "Aim at barcode to scan";
                        break;
                    default:
                        this.sbMain.Text = barcodeStatus.Text;
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void HandleData(Symbol.Barcode.ReaderData readerData) {
            try {
                this.txtScan.Text = readerData.Text;
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
    }
}