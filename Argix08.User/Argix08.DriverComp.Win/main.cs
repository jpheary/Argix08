using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Data;
using Argix.Enterprise;
using Argix.Windows;

namespace Argix.Finance {
    //
    public partial class frmMain :Form {
        //Members
        private System.Windows.Forms.ToolTip mToolTip = null;
        private MessageManager mMessageMgr = null;
        private NameValueCollection mHelpItems = null;
        private winDriverComp mActiveDC = null;
        
        //Interface
		public frmMain() {
			//Constructor			
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				this.Text = "Argix Logistics " + App.Product;
                buildHelpMenu();
				Splash.Start(App.Product, Assembly.GetExecutingAssembly(), App.Copyright);
				Thread.Sleep(3000);
				#region Window docking
                this.mnuMain.Dock = DockStyle.Top;
				this.tlbMain.Dock = DockStyle.Top;
				this.stbMain.Dock = DockStyle.Bottom;
                this.Controls.AddRange(new Control[] { this.tlbMain,this.mnuMain, this.stbMain });
				#endregion
				
				//Create data and UI services
				this.mMessageMgr = new MessageManager(this.stbMain.Panels[0], 500, 3000);
				this.mToolTip = new System.Windows.Forms.ToolTip();
				
				//Set application configuration
				configApplication();
			}
			catch(Exception ex) { Splash.Close(); throw new ApplicationException("Startup Failure", ex); }
		}

        private void OnFormLoad(object sender,System.EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                //Initialize controls
                Splash.Close();
                this.Visible = true;
                Application.DoEvents();
                #region Set user preferences
                try {
                    this.WindowState = global::Argix.Properties.Settings.Default.WindowState;
                    switch(this.WindowState) {
                        case FormWindowState.Maximized: break;
                        case FormWindowState.Minimized: break;
                        case FormWindowState.Normal:
                            this.Location = global::Argix.Properties.Settings.Default.Location;
                            this.Size = global::Argix.Properties.Settings.Default.Size;
                            break;
                    }
                    this.mnuViewToolbar.Checked = this.tlbMain.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.Toolbar);
                    this.mnuViewStatusBar.Checked = this.stbMain.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.StatusBar);
                    this.mnuViewTermConfigs.Checked = !global::Argix.Properties.Settings.Default.TermConfigWindow;
                    this.mnuViewEquip.Checked = !global::Argix.Properties.Settings.Default.DriverEquipWindow;
                    this.mnuViewRates.Checked = !global::Argix.Properties.Settings.Default.DriverRatesWindow;

                    if(global::Argix.Properties.Settings.Default.LastVersion != App.Version) {
                        //New release
                        App.ReportError(new ApplicationException("This is a new release of Driver Compensation. Please contact the IT department immediately if you have a problem."),true,LogLevel.None);
                    }
                }
                catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
                #endregion
                #region Set tooltips
                this.mToolTip.InitialDelay = 500;
                this.mToolTip.AutoPopDelay = 3000;
                this.mToolTip.ReshowDelay = 1000;
                this.mToolTip.ShowAlways = true;		//Even when form is inactve
                #endregion

                //Set control defaults
                this.ctlTerminalConfiguration1.DataSource = FinanceFactory.TerminalConfigurations;
                this.ctlDriverEquipment1.DataSource = FinanceFactory.DriverEquipment;
                this.tabMain.TabPages.Clear();
                this.mnuViewTermConfigs.PerformClick();
                this.mnuViewEquip.PerformClick();
                this.mnuViewRates.PerformClick();
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnFormResize(object sender,System.EventArgs e) {
            //Event handler for form size changes
        }
        private void OnFormClosed(object sender,FormClosedEventArgs e) {
            //Event handler for form closed event
            global::Argix.Properties.Settings.Default.WindowState = this.WindowState;
            global::Argix.Properties.Settings.Default.Location = this.Location;
            global::Argix.Properties.Settings.Default.Size = this.Size;
            global::Argix.Properties.Settings.Default.Toolbar = this.mnuViewToolbar.Checked;
            global::Argix.Properties.Settings.Default.StatusBar = this.mnuViewStatusBar.Checked;
            global::Argix.Properties.Settings.Default.TermConfigWindow = this.mnuViewTermConfigs.Checked;
            global::Argix.Properties.Settings.Default.DriverEquipWindow = this.mnuViewEquip.Checked;
            global::Argix.Properties.Settings.Default.DriverRatesWindow = this.mnuViewRates.Checked;
            global::Argix.Properties.Settings.Default.LastVersion = App.Version;
            global::Argix.Properties.Settings.Default.Save();
            ArgixTrace.WriteLine(new TraceMessage(App.Version,App.EventLogName,LogLevel.Information,"App Stopped"));
        }
        #region User Services: OnItemClick(), OnToolbarButtonClick(), OnHelpMenuClick(), OnDataStatusUpdate()
        private void OnItemClick(object sender,System.EventArgs e) {
            //Menu services
            bool show = false;
            try {
                ToolStripItem menu = (ToolStripItem)sender;
                switch(menu.Name) {
                    case "mnuFileNew":
                    case "btnNew": 
                        break;
                    case "mnuFileOpen":
                    case "btnOpen": 
                        openDriverComp(); 
                        break;
                    case "mnuFileSave":
                    case "btnSave": 
                        this.mActiveDC.Save(); 
                        break;
                    case "mnuFileSaveAs":           
                        this.mActiveDC.SaveAs();  
                        break;
                    case "mnuFileAddRoutes":
                    case "btnAddRoutes": 
                    this.mActiveDC.AddRoutes(); 
                        break;
                    case "mnuFileExport":
                    case "btnExport": 
                        this.mActiveDC.Export(); 
                        break;
                    case "mnuFileSetup":            this.mActiveDC.PrintSetup(); break;
                    case "mnuFilePrint":            this.mActiveDC.Print(true); break;
                    case "btnPrint":                this.mActiveDC.Print(false); ; break;
                    case "mnuFilePreview":          this.mActiveDC.PrintPreview(); break;
                    case "mnuFileExit":             this.Close(); Application.Exit(); break;
                    case "mnuEditCut":
                    case "btnCut":      
                        this.mActiveDC.Cut(); 
                        break;
                    case "mnuEditCopy":
                    case "btnCopy": 
                        this.mActiveDC.Copy(); 
                        break;
                    case "mnuEditPaste":
                    case "btnPaste": 
                        this.mActiveDC.Paste(); 
                        break;
                    case "mnuEditDelete":
                    case "btnDelete": 
                        this.mActiveDC.Delete(); 
                        break;
                    case "mnuEditCheckAll":         
                        this.mActiveDC.CheckAll(); 
                        break;
                    case "mnuEditFind":             
                        break;
                    case "mnuViewRefresh":
                    case "btnRefresh": 
                        this.Cursor = Cursors.WaitCursor; 
                        this.mActiveDC.Refresh(); 
                        break;
                    case "mnuViewRoutes":           
                        this.mActiveDC.RoadshowRoutesVisible = (this.mnuViewRoutes.Checked = (!this.mnuViewRoutes.Checked)); 
                        break;
                    case "mnuViewTermConfigs":
                        show = (this.mnuViewTermConfigs.Checked = (App.Config.Administrator && !this.mnuViewTermConfigs.Checked));
                        if(show) this.tabMain.TabPages.Add(this.tabTerm); else this.tabMain.TabPages.Remove(this.tabTerm);
                        if(show) this.tabMain.SelectedTab = this.tabTerm;
                        this.tabMain.Visible = this.splitterH.Visible = (this.tabMain.TabPages.Count > 0);
                        break;
                    case "mnuViewEquip":
                        show = (this.mnuViewEquip.Checked = (App.Config.Administrator && !this.mnuViewEquip.Checked));
                        if(show) this.tabMain.TabPages.Add(this.tabEquip); else this.tabMain.TabPages.Remove(this.tabEquip);
                        if(show) this.tabMain.SelectedTab = this.tabEquip;
                        this.tabMain.Visible = this.splitterH.Visible = (this.tabMain.TabPages.Count > 0);
                        break;
                    case "mnuViewRates":
                        show = (this.mnuViewRates.Checked = (App.Config.Administrator && !this.mnuViewRates.Checked));
                        if(show) this.tabMain.TabPages.Add(this.tabRates); else this.tabMain.TabPages.Remove(this.tabRates);
                        if(show) this.tabMain.SelectedTab = this.tabRates;
                        this.tabMain.Visible = this.splitterH.Visible = (this.tabMain.TabPages.Count > 0);
                        break;
                    case "mnuViewToolbar":          this.tlbMain.Visible = (this.mnuViewToolbar.Checked = (!this.mnuViewToolbar.Checked)); break;
                    case "mnuViewStatusBar":        this.stbMain.Visible = (this.mnuViewStatusBar.Checked = (!this.mnuViewStatusBar.Checked)); break;
                    case "mnuToolsConfig":          App.ShowConfig(); break;
                    case "mnuWinCascade":           this.LayoutMdi(MdiLayout.Cascade); break;
                    case "mnuWinTileH":             this.LayoutMdi(MdiLayout.TileHorizontal); break;
                    case "mnuWinTileV":             this.LayoutMdi(MdiLayout.TileVertical); break;
                    case "mnuHelpAbout":            new dlgAbout(App.Product + " Application",App.Version,App.Copyright,App.Configuration).ShowDialog(this); break;
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnHelpMenuClick(object sender,System.EventArgs e) {
            //Event hanlder for configurable help menu items
            try {
                ToolStripDropDownItem menu = (ToolStripDropDownItem)sender;
                Help.ShowHelp(this,this.mHelpItems.GetValues(menu.Text)[0]);
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnDataStatusUpdate(object sender,DataStatusArgs e) {
            //Event handler for notifications from mediator
            this.stbMain.OnOnlineStatusUpdate(null,new OnlineStatusArgs(e.Online,e.Connection));
        }
        #endregion
        #region Local Services: configApplication(), setUserServices(), buildHelpMenu()
        private void configApplication() {
            try {
                //Create event log and database trace listeners, and log application as started
                try {
                    ArgixTrace.AddListener(new DBTraceListener((LogLevel)App.Config.TraceLevel,App.Mediator,App.USP_TRACE,App.EventLogName));
                }
                catch {
                    ArgixTrace.AddListener(new DBTraceListener(LogLevel.Debug,App.Mediator,App.USP_TRACE,App.EventLogName));
                    ArgixTrace.WriteLine(new TraceMessage("Log level not found; setting log levels to Debug.",App.EventLogName,LogLevel.Warning,"Log Level"));
                }
                ArgixTrace.WriteLine(new TraceMessage(App.Version,App.EventLogName,LogLevel.Information,"App Started"));

                //Create business objects with configuration values
                App.Mediator.DataStatusUpdate += new DataStatusHandler(OnDataStatusUpdate);
                EnterpriseFactory.Mediator = App.Mediator;
                EnterpriseFactory.RefreshCache();
                FinanceFactory.Mediator = App.Mediator;
                FinanceFactory.RefreshCache();
                DriverRatingFactory.Mediator = App.Mediator;
                this.stbMain.SetTerminalPanel(EnterpriseFactory.LocalTerminal.TerminalID.ToString(),EnterpriseFactory.LocalTerminal.Description);
                bool createError = App.Config.ReadOnly;
            }
            catch(Exception ex) { throw new ApplicationException("Configuration Failure",ex); }
        }
        private void setUserServices() {
            //Set user services
            try {
                //Set menu states
                this.mnuFileNew.Enabled = this.btnNew.Enabled = false;
                this.mnuFileOpen.Enabled = this.btnOpen.Enabled = true;
                this.mnuFileSave.Enabled = this.btnSave.Enabled = (!App.Config.ReadOnly && this.mActiveDC != null && this.mActiveDC.CanSave);
                this.mnuFileSaveAs.Enabled = (!App.Config.ReadOnly && this.mActiveDC != null && this.mActiveDC.CanSaveAs);
                this.mnuFileAddRoutes.Enabled = this.btnAddRoutes.Enabled = (this.mActiveDC != null && this.mActiveDC.CanAddRoutes);
                this.mnuFileExport.Enabled = this.btnExport.Enabled = (!App.Config.ReadOnly && this.mActiveDC != null && this.mActiveDC.CanExport);
                this.mnuFileSetup.Enabled = this.mActiveDC != null;
                this.mnuFilePreview.Enabled = (this.mActiveDC != null && this.mActiveDC.CanPreview);
                this.mnuFilePrint.Enabled = this.btnPrint.Enabled = (this.mActiveDC != null && this.mActiveDC.CanPrint);
                this.mnuFileExit.Enabled = true;
                this.mnuEditCut.Enabled = this.btnCut.Enabled = (!App.Config.ReadOnly && this.mActiveDC != null && this.mActiveDC.CanCut);
                this.mnuEditCopy.Enabled = this.btnCopy.Enabled = (!App.Config.ReadOnly && this.mActiveDC != null && this.mActiveDC.CanCopy);
                this.mnuEditPaste.Enabled = this.btnPaste.Enabled = (!App.Config.ReadOnly && this.mActiveDC != null && this.mActiveDC.CanPaste);
                this.mnuEditDelete.Enabled = this.btnDelete.Enabled = (!App.Config.ReadOnly && this.mActiveDC != null && this.mActiveDC.CanDelete);
                this.mnuEditCheckAll.Enabled = (this.mActiveDC != null && this.mActiveDC.CanCheckAll);
                this.mnuEditFind.Enabled = this.btnFind.Enabled = false;
                this.mnuViewToolbar.Enabled = this.mnuViewStatusBar.Enabled = true;
                this.mnuViewRefresh.Enabled = this.btnRefresh.Enabled = (this.mActiveDC != null);
                this.mnuViewRoutes.Enabled = (this.mActiveDC != null);
                this.mnuViewRoutes.Checked = (this.mActiveDC != null && this.mActiveDC.RoadshowRoutesVisible);
                this.mnuViewTermConfigs.Enabled = App.Config.Administrator;
                this.mnuViewEquip.Enabled = App.Config.Administrator;
                this.mnuViewRates.Enabled = App.Config.Administrator;
                this.mnuToolsConfig.Enabled = true;
                this.mnuHelpAbout.Enabled = true;

                this.stbMain.User1Panel.Text = (App.Config.Administrator ? "Administrator" : "Standard");
                this.stbMain.User1Panel.ToolTipText = "User role";
                this.stbMain.User1Panel.Width = 96;
                //this.stbMain.User1Panel.Text = (this.mActiveDC != null ? this.mActiveDC.Compensation.DriverRoutes.DriverCompTable.Rows.Count.ToString() : "");
                //this.stbMain.User1Panel.ToolTipText = "# of operators";
                this.stbMain.User2Panel.Icon = App.Config.ReadOnly ? new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Resources.readonly.ico")) : null;
                this.stbMain.User2Panel.ToolTipText = App.Config.ReadOnly ? "Read only mode; notify IT if you require update permissions." : "";
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { Application.DoEvents(); }
        }
        private void buildHelpMenu() {
            //Build dynamic help menu from configuration file
            try {
                //Read help menu configuration from app.config
                this.mHelpItems = (NameValueCollection)ConfigurationManager.GetSection("menu/help");
                for(int i = 0; i < this.mHelpItems.Count; i++) {
                    string sKey = this.mHelpItems.GetKey(i);
                    string sValue = this.mHelpItems.GetValues(sKey)[0];
                    ToolStripMenuItem item = new ToolStripMenuItem();
                    //item.Name = "mnuHelp" + sKey;
                    item.Text = sKey;
                    item.Click += new System.EventHandler(this.OnHelpMenuClick);
                    item.Enabled = (sValue != "");
                    this.mnuHelp.DropDownItems.Add(item);
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        #endregion
        #region Driver Comp Services: openDriverComp()
        private void openDriverComp() {
            //Create a new ship schedule
            dlgOpen dlg = new dlgOpen();
            if(dlg.ShowDialog() == DialogResult.OK) {
                //Validate
                DriverComp driverComp = new DriverComp(dlg.AgentNumber,dlg.AgentName,dlg.StartDate,dlg.EndDate,App.Mediator);
                winDriverComp win = null;
                for(int i = 0; i < this.MdiChildren.Length; i++) {
                    win = (winDriverComp)this.MdiChildren[i];
                    if(win.Compensation.Title == driverComp.Title) {
                        //Already open; bring to forefront
                        win.Activate();
                        if(win.WindowState == FormWindowState.Minimized) win.WindowState = FormWindowState.Normal;
                        break;
                    }
                    else
                        win = null;
                }
                if(win == null) {
                    this.mMessageMgr.AddMessage("Opening " + driverComp.AgentName.Trim() + " driver pay for week ending " + driverComp.EndDate + "...");
                    win = new winDriverComp(driverComp);
                    win.WindowState = (this.MdiChildren.Length > 0) ? this.MdiChildren[0].WindowState : FormWindowState.Maximized;
                    win.MdiParent = this;
                    win.Activated += new EventHandler(OnDCActivated);
                    win.Deactivate += new EventHandler(OnDCDeactivated);
                    win.Closing += new CancelEventHandler(OnDCClosing);
                    win.Closed += new EventHandler(OnDCClosed);
                    win.ServiceStatesChanged += new EventHandler(OnDCServiceChange);
                    win.StatusMessage += new StatusEventHandler(OnDCStatus);
                    win.Show();
                }
            }
        }
        #endregion
        #region DriverComp Window Mgt: OnDCActivated(), OnDCDeactivated(), OnDCClosing(), OnDCClosed(), OnDCServiceChange(), OnDCStatus()
        private void OnDCActivated(object sender,System.EventArgs e) {
            //Event handler for activaton of a viewer child window
            try {
                this.mActiveDC = null;
                if(sender != null) {
                    winDriverComp frm = (winDriverComp)sender;
                    this.mActiveDC = frm;
                }
                if(this.mActiveDC != null) {
                    this.ctlRates1.Rates = this.mActiveDC.Compensation.Rates;
                    this.lblRatesTitle.Text = "Driver Rates: " + this.mActiveDC.Compensation.Rates.AgentName.Trim() + " (" + this.mActiveDC.Compensation.Rates.RatesDate.ToString("MM-dd-yyyy") + ")";
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnDCDeactivated(object sender,System.EventArgs e) {
            //Event handler for deactivaton of a viewer child window
            this.mActiveDC = null;
            this.ctlRates1.Rates = null;
            this.lblRatesTitle.Text = "Rates";
            setUserServices();
        }
        private void OnDCClosing(object sender,System.ComponentModel.CancelEventArgs e) {
            //Event handler for form closing via control box; e.Cancel=true keeps window open
            try {
                e.Cancel = false;
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnDCClosed(object sender,System.EventArgs e) {
            //Event handler for closing of a viewer child window
            //if(this.MdiChildren.Length == 1 && this.btnFullScreen.Pushed) this.mnuViewFullScreen.PerformClick();
            this.ctlRates1.Rates = null;
            this.lblRatesTitle.Text = "Rates";
            setUserServices();
        }
        private void OnDCServiceChange(object sender,System.EventArgs e) { setUserServices(); }
        private void OnDCStatus(object sender,StatusEventArgs e) { this.mMessageMgr.AddMessage(e.Message); }
        #endregion
        #region Tool Windows: OnCloseToolWindow(), OnEnterToolWindow(), OnLeaveToolWindow()
        private void OnCloseToolWindow(object sender,EventArgs e) {
            //Event handler for closing rates window
            Label lbl = (Label)sender;
            switch(lbl.Name) {
                case "lblTermClose": this.mnuViewTermConfigs.PerformClick(); break;
                case "lblEquipClose": this.mnuViewEquip.PerformClick(); break;
                case "lblRatesClose":   this.mnuViewRates.PerformClick(); break;
            }
        }
        private void OnEnterToolWindow(object sender,EventArgs e) {
            //Event handler for entering rates window
            try {
                if(this.tabMain.SelectedTab == this.tabTerm) {
                    this.lblTermTitle.BackColor = this.lblTermClose.BackColor = SystemColors.ActiveCaption;
                    this.lblTermTitle.ForeColor = this.lblTermClose.ForeColor = SystemColors.ActiveCaptionText;
                }
                else if(this.tabMain.SelectedTab == this.tabEquip) {
                    this.lblEquipTitle.BackColor = this.lblEquipClose.BackColor = SystemColors.ActiveCaption;
                    this.lblEquipTitle.ForeColor = this.lblEquipClose.ForeColor = SystemColors.ActiveCaptionText;
                }
                else if(this.tabMain.SelectedTab == this.tabRates) {
                    this.lblRatesTitle.BackColor = this.lblRatesClose.BackColor = SystemColors.ActiveCaption;
                    this.lblRatesTitle.ForeColor = this.lblRatesClose.ForeColor = SystemColors.ActiveCaptionText;
                }
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnLeaveToolWindow(object sender,EventArgs e) {
            //Event handler for leaving rates window
            try {
                if(this.tabMain.SelectedTab == this.tabTerm) {
                    this.lblTermTitle.BackColor = this.lblTermClose.BackColor = SystemColors.InactiveCaption;
                    this.lblTermTitle.ForeColor = this.lblTermClose.ForeColor = SystemColors.InactiveCaptionText;
                }
                else if(this.tabMain.SelectedTab == this.tabEquip) {
                    this.lblEquipTitle.BackColor = this.lblEquipClose.BackColor = SystemColors.InactiveCaption;
                    this.lblEquipTitle.ForeColor = this.lblEquipClose.ForeColor = SystemColors.InactiveCaptionText;
                }
                else if(this.tabMain.SelectedTab == this.tabRates) {
                    this.lblRatesTitle.BackColor = this.lblRatesClose.BackColor = SystemColors.InactiveCaption;
                    this.lblRatesTitle.ForeColor = this.lblRatesClose.ForeColor = SystemColors.InactiveCaptionText;
                }
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnGridBeforeRowFilterDropDownPopulate(object sender,Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs e) {
            //Removes only (Blanks) and Non Blanks default filter
            try {
                e.ValueList.ValueListItems.Remove(3);
                e.ValueList.ValueListItems.Remove(2);
                e.ValueList.ValueListItems.Remove(1);
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        #endregion
    }
}