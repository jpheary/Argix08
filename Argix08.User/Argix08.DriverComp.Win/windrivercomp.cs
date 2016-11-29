using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix;
using Argix.Windows;
using Argix.Enterprise;
using Microsoft.Reporting.WinForms;

namespace Argix.Finance {
	//MDI child window for driver compensation
	public class winDriverComp : System.Windows.Forms.Form {
		//Members
		private DriverComp mDriverComp=null;
        private UltraGridSvc mRouteGridSvc = null, mRoadshowGridSvc;
        private int mLastOperator = 0;
        private int mLastOwner = 0;

        private const int KEYSTATE_SHIFT = 5;
        private const int KEYSTATE_CTL = 9;
        private const string REPORT_ALL_OPERATORS = "* All Operators";
        private const string REPORT_ALL_OWNERS = "* All Owners";
        
        #region Controls

        private Infragistics.Win.UltraWinGrid.UltraGrid grdDriverRoutes;
        private System.ComponentModel.IContainer components;
        private DriverCompDS mDriverCompDS;
        private UltraDropDown uddRateType;
        private UltraDropDown uddEquipType;
        private UltraGrid grdRoadshowRoutes;
        private ContextMenuStrip ctxComp;
        private ToolStripMenuItem ctxCRefresh;
        private ToolStripSeparator ctxCSep1;
        private ToolStripMenuItem ctxCDelete;
        private ContextMenuStrip ctxRoutes;
        private ToolStripMenuItem ctxRRefresh;
        private ToolStripSeparator ctxRSep1;
        private ToolStripMenuItem ctxRAddRoutes;
        private ToolStripMenuItem ctxCCut;
        private ToolStripMenuItem ctxCCopy;
        private ToolStripMenuItem ctxCPaste;
        private ToolStripSeparator ctxCSep2;
        private TabControl tabDialog;
        private TabPage tabRoutes;
        private TabPage tabSummary;
        private TabPage tabDriverComp;
        private Splitter splitterH;
        private Microsoft.Reporting.WinForms.ReportViewer rsvSummary;
        private Microsoft.Reporting.WinForms.ReportViewer rsvDrivers;
        private TabPage tabExport;
        private RichTextBox txtExport;
        private UltraDropDown uddAdjType;
        private Label lblCloseRoutes;
        private ToolStripMenuItem ctxCExport;
        private ComboBox cboOperators;
        private ToolStripMenuItem ctxCSaveAs;
        private ToolStripMenuItem ctxCPrint;
        private ToolStripSeparator ctxCSep3;
        private ToolStripMenuItem ctxRSelectAll;
        private ContextMenuStrip ctxExport;
        private ToolStripMenuItem ctxERefresh;
        private ToolStripSeparator ctxESep1;
        private ToolStripMenuItem ctxEExport;
        private TabPage tabOwnerComp;
        private ReportViewer rsOwners;
        private ComboBox cboOwners;
        private ToolStripSeparator ctxCSep4;
        private ToolStripMenuItem ctxCExpandAll;
        private ToolStripMenuItem ctxCCollapseAll;
        private ToolStripMenuItem ctxCEquipOverride;
        private ToolStripSeparator ctxCSep5;
        private CheckBox chkShowRates;
        #endregion
		public event StatusEventHandler StatusMessage=null;
		public event EventHandler ServiceStatesChanged=null;
		
		//Interface
		public winDriverComp(DriverComp driverComp) {
			//Constructor
			try {
				InitializeComponent();
                #region Window docking
                this.splitterH.Dock = DockStyle.Bottom;
                this.grdRoadshowRoutes.Dock = DockStyle.Bottom;
                this.grdRoadshowRoutes.Controls.Add(this.lblCloseRoutes);
                this.grdRoadshowRoutes.Height = 192;
                this.grdDriverRoutes.Dock = DockStyle.Fill;
                this.grdDriverRoutes.Controls.Add(this.chkShowRates);
                this.tabRoutes.Controls.AddRange(new Control[] { this.grdDriverRoutes,this.splitterH,this.grdRoadshowRoutes });
                this.rsvDrivers.Dock = DockStyle.Fill;
                this.cboOperators.Dock = DockStyle.Top;
                this.tabDriverComp.Controls.AddRange(new Control[] { this.rsvDrivers,this.cboOperators });
                this.rsOwners.Dock = DockStyle.Fill;
                this.cboOwners.Dock = DockStyle.Top;
                this.tabOwnerComp.Controls.AddRange(new Control[] { this.rsOwners,this.cboOwners });
                #endregion
				
				//Init objects
                this.mDriverComp = driverComp;
                this.mDriverComp.DriverRoutesChanged += new EventHandler(OnDriverRoutesChanged);
                this.mDriverComp.RoadshowRoutesChanged += new EventHandler(OnRoadshowRoutesChanged);
                this.mRouteGridSvc = new UltraGridSvc(this.grdDriverRoutes);
                this.mRoadshowGridSvc = new UltraGridSvc(this.grdRoadshowRoutes);
                this.Text = this.grdDriverRoutes.Text = this.mDriverComp.Title;
                FinanceFactory.CacheChanged += new EventHandler(OnCacheChanged);
                Uri uri = new Uri(global::Argix.Properties.Settings.Default.ReportServerUrl);
                this.rsvSummary.ServerReport.ReportServerUrl = uri;
                this.rsvDrivers.ServerReport.ReportServerUrl = uri;
                this.rsOwners.ServerReport.ReportServerUrl = uri;
			}
			catch(Exception ex) { throw new ApplicationException("Failed to create new winSchedule.", ex); }
		}
		protected override void Dispose( bool disposing ) { if(disposing) { if (components != null) { components.Dispose(); } } base.Dispose( disposing ); }
        public override void Refresh() {
            //Refresh selected object
            this.Cursor = Cursors.WaitCursor;
            try {
                setUserServices();
                if(this.tabDialog.SelectedTab == this.tabRoutes) {
                    if(this.grdDriverRoutes.Focused) 
                        this.ctxCRefresh.PerformClick();
                    else if(this.grdRoadshowRoutes.Focused) 
                        this.ctxRRefresh.PerformClick();
                }
                else if(this.tabDialog.SelectedTab == this.tabSummary) {
                    reportStatus(new StatusEventArgs("Refreshing compensation summary..."));
                    ReportParameter p1 = new ReportParameter("AgentNumber",this.mDriverComp.AgentNumber);
                    ReportParameter p2 = new ReportParameter("AgentName",this.mDriverComp.AgentName);
                    ReportParameter p3 = new ReportParameter("StartDate",this.mDriverComp.StartDate.ToString("yyyy-MM-dd"));
                    ReportParameter p4 = new ReportParameter("EndDate",this.mDriverComp.EndDate.ToString("yyyy-MM-dd"));
                    this.rsvSummary.ServerReport.DisplayName = "Driver Compensation Summary";
                    this.rsvSummary.ServerReport.ReportPath = global::Argix.Properties.Settings.Default.ReportPathSummaryComp + this.mDriverComp.AgentNumber;
                    this.rsvSummary.ServerReport.SetParameters(new ReportParameter[] { p1,p2,p3,p4 });
                    this.rsvSummary.RefreshReport();
                }
                else if(this.tabDialog.SelectedTab == this.tabDriverComp)
                    OnOperatorChanged(this.cboOperators,EventArgs.Empty);
                else if(this.tabDialog.SelectedTab == this.tabOwnerComp)
                    OnOwnerChanged(this.cboOwners,EventArgs.Empty);
                else if(this.tabDialog.SelectedTab == this.tabExport)
                    this.ctxERefresh.PerformClick();
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        public DriverComp Compensation { get { return this.mDriverComp; } }
        #region UI Services: Save(), SaveAs(), AddRoutes(), Export(), Cut(), Copy(), Paste(), Delete(), Print(), PrintPreview(), ShowDriverRates(), RefreshX()
        public bool CanSave { get { return this.mDriverComp.IsDirty; } }
        public void Save() { this.mDriverComp.Save(); }
        public bool CanSaveAs { get { return this.ctxCSaveAs.Enabled; } }
        public void SaveAs() { this.ctxCSaveAs.PerformClick(); }
        public bool CanAddRoutes { get { return this.ctxRAddRoutes.Enabled; } }
        public void AddRoutes() { this.ctxRAddRoutes.PerformClick(); }
        public bool CanExport { get { return this.ctxCExport.Enabled || this.ctxEExport.Enabled; } }
        public void Export() { 
            try {
                if(this.tabDialog.SelectedTab == this.tabRoutes) 
                    this.ctxCExport.PerformClick();
                else if(this.tabDialog.SelectedTab == this.tabExport) 
                    this.ctxEExport.PerformClick();
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        public void PrintSetup() {
            //Print setup
            this.Cursor = Cursors.WaitCursor;
            try {
                if(this.tabDialog.SelectedTab == this.tabRoutes) 
                    UltraGridPrinter.PageSettings();
                //else if(this.tabDialog.SelectedTab == this.tabSummary) 
                //  
                //else if(this.tabDialog.SelectedTab == this.tabDriverComp) 
                //  
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        public bool CanPrint { get { return this.ctxCPrint.Enabled; } }
        public void Print(bool showDialog) {
            //Print this schedule
            try {
                if(this.tabDialog.SelectedTab == this.tabRoutes) {
                    this.ctxCPrint.PerformClick();
                }
                else if(this.tabDialog.SelectedTab == this.tabSummary) {
                    reportStatus(new StatusEventArgs("Printing driver paystub..."));
                    this.rsvSummary.PrintDialog();
                }
                else if(this.tabDialog.SelectedTab == this.tabDriverComp) {
                    reportStatus(new StatusEventArgs("Printing driver paystubs..."));
                    this.rsvDrivers.PrintDialog();
                }
                else if(this.tabDialog.SelectedTab == this.tabOwnerComp) {
                    reportStatus(new StatusEventArgs("Printing fleet owner paystubs..."));
                    this.rsOwners.PrintDialog();
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        public bool CanPreview { get { return (this.tabDialog.SelectedTab == this.tabRoutes && this.mDriverComp.DriverRoutes.DriverCompTable.Rows.Count > 0); } }
        public void PrintPreview() {
            try {
                reportStatus(new StatusEventArgs("Print previewing this schedule..."));
                string caption = "DRIVER COMPENSATION" + Environment.NewLine + this.mDriverComp.AgentName.Trim() + " : " + this.mDriverComp.StartDate.ToString("dd-MMM-yyyy") + "-" + this.mDriverComp.EndDate.ToString("dd-MMM-yyyy");
                UltraGridPrinter.PrintPreview(this.grdDriverRoutes,caption);
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        public bool CanCut { get { return this.ctxCCut.Enabled; } }
        public void Cut() { this.ctxCCut.PerformClick(); }
        public bool CanCopy { get { return this.ctxCCopy.Enabled; } }
        public void Copy() { this.ctxCCopy.PerformClick(); }
        public bool CanPaste { get { return this.ctxCPaste.Enabled; } }
        public void Paste() { this.ctxCPaste.PerformClick(); }
        public bool CanDelete { get { return this.ctxCDelete.Enabled; } }
        public void Delete() { this.ctxCDelete.PerformClick(); }
        public bool CanCheckAll { get { return this.ctxRSelectAll.Enabled; } }
        public void CheckAll() { this.ctxRSelectAll.PerformClick(); }
        public bool RoadshowRoutesVisible { get { return this.grdRoadshowRoutes.Visible; } set { this.grdRoadshowRoutes.Visible = this.splitterH.Visible = value; } }
        public bool ExportVisible { get { return this.tabExport.CanSelect; } set { if(value) this.tabExport.Show(); else this.tabExport.Hide(); } }
        public void ShowDriverRates() { new dlgRates(this.mDriverComp.Rates).ShowDialog(); }
        #endregion
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("DriverCompTable",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Select");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsNew");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsCombo");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsAdjust");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FinanceVendorID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FinanceVendor");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Operator");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("EquipmentTypeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("EquipmentType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DayRate");
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DayAmount");
            Infragistics.Win.Appearance appearance97 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance98 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Miles");
            Infragistics.Win.Appearance appearance99 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance100 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MilesBaseRate");
            Infragistics.Win.Appearance appearance101 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance102 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MilesRate");
            Infragistics.Win.Appearance appearance103 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance104 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MilesAmount");
            Infragistics.Win.Appearance appearance105 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance106 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Trip");
            Infragistics.Win.Appearance appearance107 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance108 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TripRate");
            Infragistics.Win.Appearance appearance109 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance110 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TripAmount");
            Infragistics.Win.Appearance appearance111 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance112 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stops");
            Infragistics.Win.Appearance appearance113 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance114 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopsRate");
            Infragistics.Win.Appearance appearance115 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance116 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopsAmount");
            Infragistics.Win.Appearance appearance117 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance118 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Cartons");
            Infragistics.Win.Appearance appearance119 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance120 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CartonsRate");
            Infragistics.Win.Appearance appearance121 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance122 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CartonsAmount");
            Infragistics.Win.Appearance appearance123 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance124 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Pallets");
            Infragistics.Win.Appearance appearance125 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance126 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PalletsRate");
            Infragistics.Win.Appearance appearance127 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance128 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PalletsAmount");
            Infragistics.Win.Appearance appearance129 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance130 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PickupCartons");
            Infragistics.Win.Appearance appearance131 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance132 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PickupCartonsRate");
            Infragistics.Win.Appearance appearance133 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance134 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PickupCartonsAmount");
            Infragistics.Win.Appearance appearance135 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance136 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MinimunAmount");
            Infragistics.Win.Appearance appearance137 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance138 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Amount");
            Infragistics.Win.Appearance appearance139 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance140 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FuelCost");
            Infragistics.Win.Appearance appearance141 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance142 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FSCGal");
            Infragistics.Win.Appearance appearance143 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance144 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FSCBaseRate");
            Infragistics.Win.Appearance appearance145 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance146 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FSC");
            Infragistics.Win.Appearance appearance147 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance148 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AdjustmentAmount1");
            Infragistics.Win.Appearance appearance149 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance150 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AdjustmentAmount2");
            Infragistics.Win.Appearance appearance151 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance152 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AdminCharge");
            Infragistics.Win.Appearance appearance153 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance154 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn41 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TotalAmount");
            Infragistics.Win.Appearance appearance155 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance156 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FSCMiles");
            Infragistics.Win.Appearance appearance157 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance158 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn43 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DriverCompTable_DriverRouteTable");
            Infragistics.Win.Appearance appearance159 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("DriverCompTable_DriverRouteTable",0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn44 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn45 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsNew");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn46 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsCombo");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn47 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsAdjust");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn48 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn49 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FinanceVendorID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn50 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RouteDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn51 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RouteName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn52 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Operator");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn53 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Payee");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn54 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("EquipmentTypeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn55 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RateTypeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn56 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DayRate");
            Infragistics.Win.Appearance appearance160 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance161 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn57 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DayAmount");
            Infragistics.Win.Appearance appearance162 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance163 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn58 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Miles");
            Infragistics.Win.Appearance appearance164 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance165 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn59 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MilesBaseRate");
            Infragistics.Win.Appearance appearance166 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance167 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn60 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MilesRate");
            Infragistics.Win.Appearance appearance168 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance169 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn61 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MilesAmount");
            Infragistics.Win.Appearance appearance170 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance171 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn62 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Trip");
            Infragistics.Win.Appearance appearance172 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance173 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn63 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TripRate");
            Infragistics.Win.Appearance appearance174 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance175 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn64 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TripAmount");
            Infragistics.Win.Appearance appearance176 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance177 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn65 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stops");
            Infragistics.Win.Appearance appearance178 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance179 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn66 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopsRate");
            Infragistics.Win.Appearance appearance180 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance181 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn67 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopsAmount");
            Infragistics.Win.Appearance appearance182 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance183 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn68 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Cartons");
            Infragistics.Win.Appearance appearance184 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance185 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn69 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CartonsRate");
            Infragistics.Win.Appearance appearance186 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance187 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn70 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CartonsAmount");
            Infragistics.Win.Appearance appearance188 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance189 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn71 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Pallets");
            Infragistics.Win.Appearance appearance190 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance191 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn72 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PalletsRate");
            Infragistics.Win.Appearance appearance192 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance193 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn73 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PalletsAmount");
            Infragistics.Win.Appearance appearance194 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance195 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn74 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PickupCartons");
            Infragistics.Win.Appearance appearance196 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance197 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn75 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PickupCartonsRate");
            Infragistics.Win.Appearance appearance198 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance199 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn76 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PickupCartonsAmount");
            Infragistics.Win.Appearance appearance200 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance201 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn77 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MinimunAmount");
            Infragistics.Win.Appearance appearance202 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance203 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn78 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FuelCost");
            Infragistics.Win.Appearance appearance204 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance205 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn79 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FSCGal");
            Infragistics.Win.Appearance appearance206 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance207 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn80 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FSCBaseRate");
            Infragistics.Win.Appearance appearance208 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance209 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn81 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FSC");
            Infragistics.Win.Appearance appearance210 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance211 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn82 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AdjustmentAmount1");
            Infragistics.Win.Appearance appearance212 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance213 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn83 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AdjustmentAmount1TypeID");
            Infragistics.Win.Appearance appearance214 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance215 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn84 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AdjustmentAmount2");
            Infragistics.Win.Appearance appearance216 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance217 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn85 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AdjustmentAmount2TypeID");
            Infragistics.Win.Appearance appearance218 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance219 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn86 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AdminCharge");
            Infragistics.Win.Appearance appearance220 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance221 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn87 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TotalAmount");
            Infragistics.Win.Appearance appearance222 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance223 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn88 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Imported");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn89 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Exported");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn90 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ArgixRtType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn91 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn92 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn93 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FSCMiles");
            Infragistics.Win.Appearance appearance224 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance225 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance226 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("RoadshowRouteTable",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn94 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("New");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn95 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Rt_Date");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn96 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Rt_Name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn97 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Operator");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn98 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("VEHICLE_TYPE1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn99 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Payee");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn100 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FinanceVendID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn101 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TtlMiles");
            Infragistics.Win.Appearance appearance227 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance228 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn102 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MultiTrp");
            Infragistics.Win.Appearance appearance229 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance230 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn103 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UniqueStops");
            Infragistics.Win.Appearance appearance231 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance232 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn104 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DelCtns");
            Infragistics.Win.Appearance appearance233 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance234 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn105 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RtnCtn");
            Infragistics.Win.Appearance appearance235 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance236 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn106 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DelPltsorRcks");
            Infragistics.Win.Appearance appearance237 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance238 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn107 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Depot");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn108 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DepotNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn109 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("EquipmentID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn110 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ArgixRtType");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(winDriverComp));
            this.chkShowRates = new System.Windows.Forms.CheckBox();
            this.uddRateType = new Infragistics.Win.UltraWinGrid.UltraDropDown();
            this.uddEquipType = new Infragistics.Win.UltraWinGrid.UltraDropDown();
            this.ctxRoutes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxRRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxRSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxRSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxRAddRoutes = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxComp = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxCRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxCSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxCSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxCExport = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxCSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxCPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxCSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxCEquipOverride = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxCSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxCCut = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxCCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxCPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxCDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxCSep5 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxCExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxCCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tabDialog = new System.Windows.Forms.TabControl();
            this.tabRoutes = new System.Windows.Forms.TabPage();
            this.grdDriverRoutes = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mDriverCompDS = new Argix.Finance.DriverCompDS();
            this.splitterH = new System.Windows.Forms.Splitter();
            this.grdRoadshowRoutes = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.lblCloseRoutes = new System.Windows.Forms.Label();
            this.tabSummary = new System.Windows.Forms.TabPage();
            this.rsvSummary = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tabDriverComp = new System.Windows.Forms.TabPage();
            this.rsvDrivers = new Microsoft.Reporting.WinForms.ReportViewer();
            this.cboOperators = new System.Windows.Forms.ComboBox();
            this.tabOwnerComp = new System.Windows.Forms.TabPage();
            this.rsOwners = new Microsoft.Reporting.WinForms.ReportViewer();
            this.cboOwners = new System.Windows.Forms.ComboBox();
            this.tabExport = new System.Windows.Forms.TabPage();
            this.txtExport = new System.Windows.Forms.RichTextBox();
            this.ctxExport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxERefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxESep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxEExport = new System.Windows.Forms.ToolStripMenuItem();
            this.uddAdjType = new Infragistics.Win.UltraWinGrid.UltraDropDown();
            ((System.ComponentModel.ISupportInitialize)(this.uddRateType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddEquipType)).BeginInit();
            this.ctxRoutes.SuspendLayout();
            this.ctxComp.SuspendLayout();
            this.tabDialog.SuspendLayout();
            this.tabRoutes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverRoutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mDriverCompDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRoadshowRoutes)).BeginInit();
            this.grdRoadshowRoutes.SuspendLayout();
            this.tabSummary.SuspendLayout();
            this.tabDriverComp.SuspendLayout();
            this.tabOwnerComp.SuspendLayout();
            this.tabExport.SuspendLayout();
            this.ctxExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uddAdjType)).BeginInit();
            this.SuspendLayout();
            // 
            // chkShowRates
            // 
            this.chkShowRates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowRates.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.chkShowRates.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkShowRates.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chkShowRates.Location = new System.Drawing.Point(644,3);
            this.chkShowRates.Margin = new System.Windows.Forms.Padding(0);
            this.chkShowRates.Name = "chkShowRates";
            this.chkShowRates.Size = new System.Drawing.Size(96,16);
            this.chkShowRates.TabIndex = 2;
            this.chkShowRates.Text = "Show Rates";
            this.chkShowRates.UseVisualStyleBackColor = false;
            this.chkShowRates.CheckedChanged += new System.EventHandler(this.OnShowRates);
            // 
            // uddRateType
            // 
            this.uddRateType.Cursor = System.Windows.Forms.Cursors.Default;
            this.uddRateType.Location = new System.Drawing.Point(-2,336);
            this.uddRateType.Name = "uddRateType";
            this.uddRateType.Size = new System.Drawing.Size(104,18);
            this.uddRateType.TabIndex = 5;
            this.uddRateType.Text = "ultraDropDown1";
            this.uddRateType.Visible = false;
            // 
            // uddEquipType
            // 
            this.uddEquipType.Cursor = System.Windows.Forms.Cursors.Default;
            this.uddEquipType.Location = new System.Drawing.Point(108,336);
            this.uddEquipType.Name = "uddEquipType";
            this.uddEquipType.Size = new System.Drawing.Size(104,18);
            this.uddEquipType.TabIndex = 6;
            this.uddEquipType.Text = "ultraDropDown1";
            this.uddEquipType.Visible = false;
            // 
            // ctxRoutes
            // 
            this.ctxRoutes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxRRefresh,
            this.ctxRSep1,
            this.ctxRSelectAll,
            this.ctxRAddRoutes});
            this.ctxRoutes.Name = "ctxRoutes";
            this.ctxRoutes.Size = new System.Drawing.Size(252,76);
            // 
            // ctxRRefresh
            // 
            this.ctxRRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.ctxRRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxRRefresh.Name = "ctxRRefresh";
            this.ctxRRefresh.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.ctxRRefresh.Size = new System.Drawing.Size(251,22);
            this.ctxRRefresh.Text = "&Refresh Roadshow Routes";
            this.ctxRRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxRSep1
            // 
            this.ctxRSep1.Name = "ctxRSep1";
            this.ctxRSep1.Size = new System.Drawing.Size(248,6);
            // 
            // ctxRSelectAll
            // 
            this.ctxRSelectAll.Checked = true;
            this.ctxRSelectAll.CheckOnClick = true;
            this.ctxRSelectAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ctxRSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxRSelectAll.Name = "ctxRSelectAll";
            this.ctxRSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.ctxRSelectAll.Size = new System.Drawing.Size(251,22);
            this.ctxRSelectAll.Text = "Select &All";
            this.ctxRSelectAll.ToolTipText = "Check\\uncheck all";
            this.ctxRSelectAll.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxRAddRoutes
            // 
            this.ctxRAddRoutes.Image = global::Argix.Properties.Resources.AddTable;
            this.ctxRAddRoutes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxRAddRoutes.Name = "ctxRAddRoutes";
            this.ctxRAddRoutes.Size = new System.Drawing.Size(251,22);
            this.ctxRAddRoutes.Text = "Add Roadshow &Routes";
            this.ctxRAddRoutes.ToolTipText = "Add checked routes to compensation";
            this.ctxRAddRoutes.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxComp
            // 
            this.ctxComp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxCRefresh,
            this.ctxCSep1,
            this.ctxCSaveAs,
            this.ctxCExport,
            this.ctxCSep2,
            this.ctxCPrint,
            this.ctxCSep3,
            this.ctxCEquipOverride,
            this.ctxCSep4,
            this.ctxCCut,
            this.ctxCCopy,
            this.ctxCPaste,
            this.ctxCDelete,
            this.ctxCSep5,
            this.ctxCExpandAll,
            this.ctxCCollapseAll});
            this.ctxComp.Name = "ctxComp";
            this.ctxComp.Size = new System.Drawing.Size(239,276);
            // 
            // ctxCRefresh
            // 
            this.ctxCRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.ctxCRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxCRefresh.Name = "ctxCRefresh";
            this.ctxCRefresh.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.ctxCRefresh.Size = new System.Drawing.Size(238,22);
            this.ctxCRefresh.Text = "&Refresh Driver Routes";
            this.ctxCRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxCSep1
            // 
            this.ctxCSep1.Name = "ctxCSep1";
            this.ctxCSep1.Size = new System.Drawing.Size(235,6);
            // 
            // ctxCSaveAs
            // 
            this.ctxCSaveAs.Image = global::Argix.Properties.Resources.Save;
            this.ctxCSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxCSaveAs.Name = "ctxCSaveAs";
            this.ctxCSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.ctxCSaveAs.Size = new System.Drawing.Size(238,22);
            this.ctxCSaveAs.Text = "Save &As...";
            this.ctxCSaveAs.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxCExport
            // 
            this.ctxCExport.Image = global::Argix.Properties.Resources.ImportXML;
            this.ctxCExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxCExport.Name = "ctxCExport";
            this.ctxCExport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.ctxCExport.Size = new System.Drawing.Size(238,22);
            this.ctxCExport.Text = "&Export Compensation...";
            this.ctxCExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxCSep2
            // 
            this.ctxCSep2.Name = "ctxCSep2";
            this.ctxCSep2.Size = new System.Drawing.Size(235,6);
            // 
            // ctxCPrint
            // 
            this.ctxCPrint.Image = global::Argix.Properties.Resources.Print;
            this.ctxCPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxCPrint.Name = "ctxCPrint";
            this.ctxCPrint.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.ctxCPrint.Size = new System.Drawing.Size(238,22);
            this.ctxCPrint.Text = "&Print...";
            this.ctxCPrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxCSep3
            // 
            this.ctxCSep3.Name = "ctxCSep3";
            this.ctxCSep3.Size = new System.Drawing.Size(235,6);
            // 
            // ctxCEquipOverride
            // 
            this.ctxCEquipOverride.Name = "ctxCEquipOverride";
            this.ctxCEquipOverride.Size = new System.Drawing.Size(238,22);
            this.ctxCEquipOverride.Text = "Add Equipment Override";
            // 
            // ctxCSep4
            // 
            this.ctxCSep4.Name = "ctxCSep4";
            this.ctxCSep4.Size = new System.Drawing.Size(235,6);
            // 
            // ctxCCut
            // 
            this.ctxCCut.Image = global::Argix.Properties.Resources.Cut;
            this.ctxCCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxCCut.Name = "ctxCCut";
            this.ctxCCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.ctxCCut.Size = new System.Drawing.Size(238,22);
            this.ctxCCut.Text = "Cu&t";
            this.ctxCCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxCCopy
            // 
            this.ctxCCopy.Image = global::Argix.Properties.Resources.Copy;
            this.ctxCCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxCCopy.Name = "ctxCCopy";
            this.ctxCCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.ctxCCopy.Size = new System.Drawing.Size(238,22);
            this.ctxCCopy.Text = "&Copy";
            this.ctxCCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxCPaste
            // 
            this.ctxCPaste.Image = global::Argix.Properties.Resources.Paste;
            this.ctxCPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxCPaste.Name = "ctxCPaste";
            this.ctxCPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.ctxCPaste.Size = new System.Drawing.Size(238,22);
            this.ctxCPaste.Text = "&Paste";
            this.ctxCPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxCDelete
            // 
            this.ctxCDelete.Image = global::Argix.Properties.Resources.Delete;
            this.ctxCDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxCDelete.Name = "ctxCDelete";
            this.ctxCDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.ctxCDelete.Size = new System.Drawing.Size(238,22);
            this.ctxCDelete.Text = "&Delete Route(s)";
            this.ctxCDelete.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxCSep5
            // 
            this.ctxCSep5.Name = "ctxCSep5";
            this.ctxCSep5.Size = new System.Drawing.Size(235,6);
            // 
            // ctxCExpandAll
            // 
            this.ctxCExpandAll.Name = "ctxCExpandAll";
            this.ctxCExpandAll.Size = new System.Drawing.Size(238,22);
            this.ctxCExpandAll.Text = "Expand To Routes";
            this.ctxCExpandAll.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxCCollapseAll
            // 
            this.ctxCCollapseAll.Name = "ctxCCollapseAll";
            this.ctxCCollapseAll.Size = new System.Drawing.Size(238,22);
            this.ctxCCollapseAll.Text = "Collapse To Summaries";
            this.ctxCCollapseAll.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tabDialog
            // 
            this.tabDialog.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabDialog.Controls.Add(this.tabRoutes);
            this.tabDialog.Controls.Add(this.tabSummary);
            this.tabDialog.Controls.Add(this.tabDriverComp);
            this.tabDialog.Controls.Add(this.tabOwnerComp);
            this.tabDialog.Controls.Add(this.tabExport);
            this.tabDialog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDialog.Location = new System.Drawing.Point(0,0);
            this.tabDialog.Margin = new System.Windows.Forms.Padding(0);
            this.tabDialog.Name = "tabDialog";
            this.tabDialog.Padding = new System.Drawing.Point(2,2);
            this.tabDialog.SelectedIndex = 0;
            this.tabDialog.Size = new System.Drawing.Size(751,355);
            this.tabDialog.TabIndex = 112;
            this.tabDialog.Selected += new System.Windows.Forms.TabControlEventHandler(this.OnTabSelected);
            // 
            // tabRoutes
            // 
            this.tabRoutes.Controls.Add(this.chkShowRates);
            this.tabRoutes.Controls.Add(this.grdDriverRoutes);
            this.tabRoutes.Controls.Add(this.splitterH);
            this.tabRoutes.Controls.Add(this.grdRoadshowRoutes);
            this.tabRoutes.Location = new System.Drawing.Point(4,4);
            this.tabRoutes.Name = "tabRoutes";
            this.tabRoutes.Size = new System.Drawing.Size(743,331);
            this.tabRoutes.TabIndex = 0;
            this.tabRoutes.Text = "Driver Routes";
            this.tabRoutes.UseVisualStyleBackColor = true;
            // 
            // grdDriverRoutes
            // 
            this.grdDriverRoutes.AllowDrop = true;
            this.grdDriverRoutes.ContextMenuStrip = this.ctxComp;
            this.grdDriverRoutes.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdDriverRoutes.DataSource = this.mDriverCompDS;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.FontData.Name = "Verdana";
            appearance5.FontData.SizeInPoints = 8F;
            appearance5.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance5.TextHAlignAsString = "Left";
            this.grdDriverRoutes.DisplayLayout.Appearance = appearance5;
            ultraGridColumn1.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn1.Header.Caption = "Export";
            ultraGridColumn1.Header.Fixed = true;
            ultraGridColumn1.Header.ToolTipText = "Export operator compensation";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 48;
            ultraGridColumn2.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn2.Header.Caption = "N";
            ultraGridColumn2.Header.Fixed = true;
            ultraGridColumn2.Header.ToolTipText = "New Routes";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 32;
            ultraGridColumn3.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn3.Header.Caption = "C";
            ultraGridColumn3.Header.Fixed = true;
            ultraGridColumn3.Header.ToolTipText = "Combo Routes";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 32;
            ultraGridColumn4.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn4.Header.Caption = "A";
            ultraGridColumn4.Header.Fixed = true;
            ultraGridColumn4.Header.ToolTipText = "Adjustment Routes";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Width = 32;
            ultraGridColumn5.Format = "C";
            ultraGridColumn5.Header.VisiblePosition = 7;
            ultraGridColumn5.Hidden = true;
            ultraGridColumn5.Width = 72;
            ultraGridColumn6.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn6.Header.Caption = "Vendor#";
            ultraGridColumn6.Header.Fixed = true;
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.Width = 96;
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn7.Hidden = true;
            ultraGridColumn7.Width = 168;
            ultraGridColumn8.Header.Fixed = true;
            ultraGridColumn8.Header.VisiblePosition = 4;
            ultraGridColumn8.Width = 168;
            ultraGridColumn9.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn9.Header.Caption = "Equip Type";
            ultraGridColumn9.Header.VisiblePosition = 8;
            ultraGridColumn9.Width = 96;
            ultraGridColumn10.Header.VisiblePosition = 9;
            ultraGridColumn10.Hidden = true;
            ultraGridColumn10.Width = 48;
            appearance6.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance6.TextHAlignAsString = "Right";
            ultraGridColumn11.CellAppearance = appearance6;
            appearance7.TextHAlignAsString = "Right";
            ultraGridColumn11.Header.Appearance = appearance7;
            ultraGridColumn11.Header.Caption = "Day Rate";
            ultraGridColumn11.Header.VisiblePosition = 14;
            appearance97.TextHAlignAsString = "Right";
            ultraGridColumn12.CellAppearance = appearance97;
            ultraGridColumn12.Format = "c";
            appearance98.TextHAlignAsString = "Right";
            ultraGridColumn12.Header.Appearance = appearance98;
            ultraGridColumn12.Header.Caption = "Day Amt";
            ultraGridColumn12.Header.VisiblePosition = 15;
            appearance99.TextHAlignAsString = "Right";
            ultraGridColumn13.CellAppearance = appearance99;
            ultraGridColumn13.Format = "#0.0";
            appearance100.TextHAlignAsString = "Right";
            ultraGridColumn13.Header.Appearance = appearance100;
            ultraGridColumn13.Header.VisiblePosition = 10;
            ultraGridColumn13.Width = 72;
            appearance101.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance101.TextHAlignAsString = "Right";
            ultraGridColumn14.CellAppearance = appearance101;
            appearance102.TextHAlignAsString = "Right";
            ultraGridColumn14.Header.Appearance = appearance102;
            ultraGridColumn14.Header.Caption = "Mile Base Rate";
            ultraGridColumn14.Header.VisiblePosition = 11;
            ultraGridColumn14.Width = 72;
            appearance103.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance103.TextHAlignAsString = "Right";
            ultraGridColumn15.CellAppearance = appearance103;
            appearance104.TextHAlignAsString = "Right";
            ultraGridColumn15.Header.Appearance = appearance104;
            ultraGridColumn15.Header.Caption = "Mile Rate";
            ultraGridColumn15.Header.VisiblePosition = 12;
            ultraGridColumn15.Width = 72;
            appearance105.TextHAlignAsString = "Right";
            ultraGridColumn16.CellAppearance = appearance105;
            ultraGridColumn16.Format = "c";
            appearance106.TextHAlignAsString = "Right";
            ultraGridColumn16.Header.Appearance = appearance106;
            ultraGridColumn16.Header.Caption = "Mile Amt";
            ultraGridColumn16.Header.VisiblePosition = 13;
            ultraGridColumn16.Width = 72;
            appearance107.TextHAlignAsString = "Right";
            ultraGridColumn17.CellAppearance = appearance107;
            appearance108.TextHAlignAsString = "Right";
            ultraGridColumn17.Header.Appearance = appearance108;
            ultraGridColumn17.Header.Caption = "Trips";
            ultraGridColumn17.Header.VisiblePosition = 16;
            ultraGridColumn17.Width = 72;
            appearance109.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance109.TextHAlignAsString = "Right";
            ultraGridColumn18.CellAppearance = appearance109;
            appearance110.TextHAlignAsString = "Right";
            ultraGridColumn18.Header.Appearance = appearance110;
            ultraGridColumn18.Header.Caption = "Trip Rate";
            ultraGridColumn18.Header.VisiblePosition = 17;
            ultraGridColumn18.Width = 72;
            appearance111.TextHAlignAsString = "Right";
            ultraGridColumn19.CellAppearance = appearance111;
            ultraGridColumn19.Format = "c";
            appearance112.TextHAlignAsString = "Right";
            ultraGridColumn19.Header.Appearance = appearance112;
            ultraGridColumn19.Header.Caption = "Trip Amt";
            ultraGridColumn19.Header.VisiblePosition = 18;
            ultraGridColumn19.Width = 72;
            appearance113.TextHAlignAsString = "Right";
            ultraGridColumn20.CellAppearance = appearance113;
            appearance114.TextHAlignAsString = "Right";
            ultraGridColumn20.Header.Appearance = appearance114;
            ultraGridColumn20.Header.VisiblePosition = 19;
            ultraGridColumn20.Width = 72;
            appearance115.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance115.TextHAlignAsString = "Right";
            ultraGridColumn21.CellAppearance = appearance115;
            appearance116.TextHAlignAsString = "Right";
            ultraGridColumn21.Header.Appearance = appearance116;
            ultraGridColumn21.Header.Caption = "Stop Rate";
            ultraGridColumn21.Header.VisiblePosition = 20;
            ultraGridColumn21.Width = 72;
            appearance117.TextHAlignAsString = "Right";
            ultraGridColumn22.CellAppearance = appearance117;
            ultraGridColumn22.Format = "c";
            appearance118.TextHAlignAsString = "Right";
            ultraGridColumn22.Header.Appearance = appearance118;
            ultraGridColumn22.Header.Caption = "Stop Amt";
            ultraGridColumn22.Header.VisiblePosition = 21;
            ultraGridColumn22.Width = 72;
            appearance119.TextHAlignAsString = "Right";
            ultraGridColumn23.CellAppearance = appearance119;
            appearance120.TextHAlignAsString = "Right";
            ultraGridColumn23.Header.Appearance = appearance120;
            ultraGridColumn23.Header.Caption = "Ctns";
            ultraGridColumn23.Header.VisiblePosition = 22;
            ultraGridColumn23.Width = 72;
            appearance121.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance121.TextHAlignAsString = "Right";
            ultraGridColumn24.CellAppearance = appearance121;
            appearance122.TextHAlignAsString = "Right";
            ultraGridColumn24.Header.Appearance = appearance122;
            ultraGridColumn24.Header.Caption = "Ctn Rate";
            ultraGridColumn24.Header.VisiblePosition = 23;
            ultraGridColumn24.Width = 72;
            appearance123.TextHAlignAsString = "Right";
            ultraGridColumn25.CellAppearance = appearance123;
            ultraGridColumn25.Format = "c";
            appearance124.TextHAlignAsString = "Right";
            ultraGridColumn25.Header.Appearance = appearance124;
            ultraGridColumn25.Header.Caption = "Ctn Amt";
            ultraGridColumn25.Header.VisiblePosition = 24;
            ultraGridColumn25.Width = 72;
            appearance125.TextHAlignAsString = "Right";
            ultraGridColumn26.CellAppearance = appearance125;
            appearance126.TextHAlignAsString = "Right";
            ultraGridColumn26.Header.Appearance = appearance126;
            ultraGridColumn26.Header.Caption = "Pllts";
            ultraGridColumn26.Header.VisiblePosition = 28;
            ultraGridColumn26.Width = 72;
            appearance127.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance127.TextHAlignAsString = "Right";
            ultraGridColumn27.CellAppearance = appearance127;
            appearance128.TextHAlignAsString = "Right";
            ultraGridColumn27.Header.Appearance = appearance128;
            ultraGridColumn27.Header.Caption = "Pllt Rate";
            ultraGridColumn27.Header.VisiblePosition = 29;
            ultraGridColumn27.Width = 72;
            appearance129.TextHAlignAsString = "Right";
            ultraGridColumn28.CellAppearance = appearance129;
            ultraGridColumn28.Format = "c";
            appearance130.TextHAlignAsString = "Right";
            ultraGridColumn28.Header.Appearance = appearance130;
            ultraGridColumn28.Header.Caption = "Pllt Amt";
            ultraGridColumn28.Header.VisiblePosition = 30;
            ultraGridColumn28.Width = 72;
            appearance131.TextHAlignAsString = "Right";
            ultraGridColumn29.CellAppearance = appearance131;
            appearance132.TextHAlignAsString = "Right";
            ultraGridColumn29.Header.Appearance = appearance132;
            ultraGridColumn29.Header.Caption = "PU Ctns";
            ultraGridColumn29.Header.VisiblePosition = 25;
            ultraGridColumn29.Width = 72;
            appearance133.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance133.TextHAlignAsString = "Right";
            ultraGridColumn30.CellAppearance = appearance133;
            appearance134.TextHAlignAsString = "Right";
            ultraGridColumn30.Header.Appearance = appearance134;
            ultraGridColumn30.Header.Caption = "PU Ctn Rate";
            ultraGridColumn30.Header.VisiblePosition = 26;
            ultraGridColumn30.Width = 72;
            appearance135.TextHAlignAsString = "Right";
            ultraGridColumn31.CellAppearance = appearance135;
            ultraGridColumn31.Format = "c";
            appearance136.TextHAlignAsString = "Right";
            ultraGridColumn31.Header.Appearance = appearance136;
            ultraGridColumn31.Header.Caption = "PU Ctn Amt";
            ultraGridColumn31.Header.VisiblePosition = 27;
            ultraGridColumn31.Width = 72;
            appearance137.TextHAlignAsString = "Right";
            ultraGridColumn32.CellAppearance = appearance137;
            ultraGridColumn32.Format = "c";
            appearance138.TextHAlignAsString = "Right";
            ultraGridColumn32.Header.Appearance = appearance138;
            ultraGridColumn32.Header.Caption = "Min Amt";
            ultraGridColumn32.Header.VisiblePosition = 31;
            ultraGridColumn32.Width = 72;
            appearance139.TextHAlignAsString = "Right";
            ultraGridColumn33.CellAppearance = appearance139;
            ultraGridColumn33.Format = "C";
            appearance140.TextHAlignAsString = "Right";
            ultraGridColumn33.Header.Appearance = appearance140;
            ultraGridColumn33.Header.Caption = "Sum Amt";
            ultraGridColumn33.Header.VisiblePosition = 32;
            ultraGridColumn33.Width = 72;
            appearance141.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance141.TextHAlignAsString = "Right";
            ultraGridColumn34.CellAppearance = appearance141;
            ultraGridColumn34.Format = "C";
            appearance142.TextHAlignAsString = "Right";
            ultraGridColumn34.Header.Appearance = appearance142;
            ultraGridColumn34.Header.Caption = "Fuel Cost";
            ultraGridColumn34.Header.VisiblePosition = 35;
            ultraGridColumn34.Width = 72;
            appearance143.TextHAlignAsString = "Right";
            ultraGridColumn35.CellAppearance = appearance143;
            ultraGridColumn35.Format = "";
            appearance144.TextHAlignAsString = "Right";
            ultraGridColumn35.Header.Appearance = appearance144;
            ultraGridColumn35.Header.Caption = "FSC Gal";
            ultraGridColumn35.Header.VisiblePosition = 34;
            ultraGridColumn35.Width = 72;
            appearance145.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance145.TextHAlignAsString = "Right";
            ultraGridColumn36.CellAppearance = appearance145;
            ultraGridColumn36.Format = "C";
            appearance146.TextHAlignAsString = "Right";
            ultraGridColumn36.Header.Appearance = appearance146;
            ultraGridColumn36.Header.Caption = "FSC Rate";
            ultraGridColumn36.Header.VisiblePosition = 36;
            ultraGridColumn36.Width = 72;
            appearance147.TextHAlignAsString = "Right";
            ultraGridColumn37.CellAppearance = appearance147;
            ultraGridColumn37.Format = "C";
            appearance148.TextHAlignAsString = "Right";
            ultraGridColumn37.Header.Appearance = appearance148;
            ultraGridColumn37.Header.VisiblePosition = 37;
            ultraGridColumn37.Width = 72;
            appearance149.TextHAlignAsString = "Right";
            ultraGridColumn38.CellAppearance = appearance149;
            ultraGridColumn38.Format = "C";
            appearance150.TextHAlignAsString = "Right";
            ultraGridColumn38.Header.Appearance = appearance150;
            ultraGridColumn38.Header.Caption = "Adj1 Amt";
            ultraGridColumn38.Header.VisiblePosition = 39;
            ultraGridColumn38.Width = 72;
            appearance151.TextHAlignAsString = "Right";
            ultraGridColumn39.CellAppearance = appearance151;
            ultraGridColumn39.Format = "C";
            appearance152.TextHAlignAsString = "Right";
            ultraGridColumn39.Header.Appearance = appearance152;
            ultraGridColumn39.Header.Caption = "Adj2 Amt";
            ultraGridColumn39.Header.VisiblePosition = 40;
            ultraGridColumn39.Width = 96;
            appearance153.TextHAlignAsString = "Right";
            ultraGridColumn40.CellAppearance = appearance153;
            ultraGridColumn40.Format = "C";
            appearance154.TextHAlignAsString = "Right";
            ultraGridColumn40.Header.Appearance = appearance154;
            ultraGridColumn40.Header.Caption = "Admin Fee";
            ultraGridColumn40.Header.VisiblePosition = 38;
            ultraGridColumn40.Width = 72;
            appearance155.TextHAlignAsString = "Right";
            ultraGridColumn41.CellAppearance = appearance155;
            ultraGridColumn41.Format = "C";
            appearance156.TextHAlignAsString = "Right";
            ultraGridColumn41.Header.Appearance = appearance156;
            ultraGridColumn41.Header.Caption = "Total Amt";
            ultraGridColumn41.Header.VisiblePosition = 41;
            ultraGridColumn41.Width = 72;
            appearance157.TextHAlignAsString = "Right";
            ultraGridColumn42.CellAppearance = appearance157;
            ultraGridColumn42.Format = "#0.0";
            appearance158.TextHAlignAsString = "Right";
            ultraGridColumn42.Header.Appearance = appearance158;
            ultraGridColumn42.Header.Caption = "FSC Miles";
            ultraGridColumn42.Header.VisiblePosition = 33;
            ultraGridColumn42.Width = 72;
            ultraGridColumn43.Header.VisiblePosition = 42;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9,
            ultraGridColumn10,
            ultraGridColumn11,
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn14,
            ultraGridColumn15,
            ultraGridColumn16,
            ultraGridColumn17,
            ultraGridColumn18,
            ultraGridColumn19,
            ultraGridColumn20,
            ultraGridColumn21,
            ultraGridColumn22,
            ultraGridColumn23,
            ultraGridColumn24,
            ultraGridColumn25,
            ultraGridColumn26,
            ultraGridColumn27,
            ultraGridColumn28,
            ultraGridColumn29,
            ultraGridColumn30,
            ultraGridColumn31,
            ultraGridColumn32,
            ultraGridColumn33,
            ultraGridColumn34,
            ultraGridColumn35,
            ultraGridColumn36,
            ultraGridColumn37,
            ultraGridColumn38,
            ultraGridColumn39,
            ultraGridColumn40,
            ultraGridColumn41,
            ultraGridColumn42,
            ultraGridColumn43});
            appearance159.BackColor = System.Drawing.SystemColors.Info;
            ultraGridBand1.Override.RowAppearance = appearance159;
            ultraGridBand1.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended;
            ultraGridColumn44.Header.Fixed = true;
            ultraGridColumn44.Header.VisiblePosition = 0;
            ultraGridColumn44.Width = 29;
            ultraGridColumn45.Header.Caption = "N";
            ultraGridColumn45.Header.Fixed = true;
            ultraGridColumn45.Header.ToolTipText = "New Routes";
            ultraGridColumn45.Header.VisiblePosition = 1;
            ultraGridColumn45.Width = 32;
            ultraGridColumn46.Header.Caption = "C";
            ultraGridColumn46.Header.Fixed = true;
            ultraGridColumn46.Header.ToolTipText = "Combo Routes";
            ultraGridColumn46.Header.VisiblePosition = 2;
            ultraGridColumn46.Width = 32;
            ultraGridColumn47.Header.Caption = "A";
            ultraGridColumn47.Header.Fixed = true;
            ultraGridColumn47.Header.ToolTipText = "Adjustment Routes";
            ultraGridColumn47.Header.VisiblePosition = 3;
            ultraGridColumn47.Width = 32;
            ultraGridColumn48.Header.VisiblePosition = 9;
            ultraGridColumn48.Hidden = true;
            ultraGridColumn48.Width = 75;
            ultraGridColumn49.Header.VisiblePosition = 8;
            ultraGridColumn49.Hidden = true;
            ultraGridColumn49.Width = 49;
            ultraGridColumn50.Format = "D";
            ultraGridColumn50.Header.Caption = "Date";
            ultraGridColumn50.Header.Fixed = true;
            ultraGridColumn50.Header.VisiblePosition = 4;
            ultraGridColumn50.Width = 168;
            ultraGridColumn51.Header.Caption = "Route";
            ultraGridColumn51.Header.Fixed = true;
            ultraGridColumn51.Header.VisiblePosition = 5;
            ultraGridColumn51.Width = 96;
            ultraGridColumn52.Header.VisiblePosition = 6;
            ultraGridColumn52.Hidden = true;
            ultraGridColumn53.Header.VisiblePosition = 7;
            ultraGridColumn53.Hidden = true;
            ultraGridColumn54.Header.Caption = "Equip Type";
            ultraGridColumn54.Header.VisiblePosition = 10;
            ultraGridColumn55.Header.Caption = "Rate TypeID";
            ultraGridColumn55.Header.VisiblePosition = 44;
            ultraGridColumn55.Width = 100;
            appearance160.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance160.TextHAlignAsString = "Right";
            ultraGridColumn56.CellAppearance = appearance160;
            appearance161.TextHAlignAsString = "Right";
            ultraGridColumn56.Header.Appearance = appearance161;
            ultraGridColumn56.Header.Caption = "Day Rate";
            ultraGridColumn56.Header.VisiblePosition = 15;
            appearance162.TextHAlignAsString = "Right";
            ultraGridColumn57.CellAppearance = appearance162;
            ultraGridColumn57.Format = "c";
            appearance163.TextHAlignAsString = "Right";
            ultraGridColumn57.Header.Appearance = appearance163;
            ultraGridColumn57.Header.Caption = "Day Amt";
            ultraGridColumn57.Header.VisiblePosition = 16;
            appearance164.TextHAlignAsString = "Right";
            ultraGridColumn58.CellAppearance = appearance164;
            ultraGridColumn58.Format = "#0.0";
            appearance165.TextHAlignAsString = "Right";
            ultraGridColumn58.Header.Appearance = appearance165;
            ultraGridColumn58.Header.VisiblePosition = 11;
            ultraGridColumn58.Width = 72;
            appearance166.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance166.TextHAlignAsString = "Right";
            ultraGridColumn59.CellAppearance = appearance166;
            appearance167.TextHAlignAsString = "Right";
            ultraGridColumn59.Header.Appearance = appearance167;
            ultraGridColumn59.Header.Caption = "Mile Base Rate";
            ultraGridColumn59.Header.VisiblePosition = 12;
            ultraGridColumn59.Width = 72;
            appearance168.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance168.TextHAlignAsString = "Right";
            ultraGridColumn60.CellAppearance = appearance168;
            appearance169.TextHAlignAsString = "Right";
            ultraGridColumn60.Header.Appearance = appearance169;
            ultraGridColumn60.Header.Caption = "Mile Rate";
            ultraGridColumn60.Header.VisiblePosition = 13;
            ultraGridColumn60.Width = 72;
            appearance170.TextHAlignAsString = "Right";
            ultraGridColumn61.CellAppearance = appearance170;
            ultraGridColumn61.Format = "c";
            appearance171.TextHAlignAsString = "Right";
            ultraGridColumn61.Header.Appearance = appearance171;
            ultraGridColumn61.Header.Caption = "Mile Amt";
            ultraGridColumn61.Header.VisiblePosition = 14;
            ultraGridColumn61.Width = 72;
            appearance172.TextHAlignAsString = "Right";
            ultraGridColumn62.CellAppearance = appearance172;
            appearance173.TextHAlignAsString = "Right";
            ultraGridColumn62.Header.Appearance = appearance173;
            ultraGridColumn62.Header.Caption = "Trips";
            ultraGridColumn62.Header.VisiblePosition = 17;
            ultraGridColumn62.Width = 72;
            appearance174.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance174.TextHAlignAsString = "Right";
            ultraGridColumn63.CellAppearance = appearance174;
            appearance175.TextHAlignAsString = "Right";
            ultraGridColumn63.Header.Appearance = appearance175;
            ultraGridColumn63.Header.Caption = "Trip Rate";
            ultraGridColumn63.Header.VisiblePosition = 18;
            ultraGridColumn63.Width = 72;
            appearance176.TextHAlignAsString = "Right";
            ultraGridColumn64.CellAppearance = appearance176;
            ultraGridColumn64.Format = "c";
            appearance177.TextHAlignAsString = "Right";
            ultraGridColumn64.Header.Appearance = appearance177;
            ultraGridColumn64.Header.Caption = "Trip Amt";
            ultraGridColumn64.Header.VisiblePosition = 19;
            ultraGridColumn64.Width = 72;
            appearance178.TextHAlignAsString = "Right";
            ultraGridColumn65.CellAppearance = appearance178;
            appearance179.TextHAlignAsString = "Right";
            ultraGridColumn65.Header.Appearance = appearance179;
            ultraGridColumn65.Header.VisiblePosition = 20;
            ultraGridColumn65.Width = 72;
            appearance180.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance180.TextHAlignAsString = "Right";
            ultraGridColumn66.CellAppearance = appearance180;
            appearance181.TextHAlignAsString = "Right";
            ultraGridColumn66.Header.Appearance = appearance181;
            ultraGridColumn66.Header.Caption = "Stop Rate";
            ultraGridColumn66.Header.VisiblePosition = 21;
            ultraGridColumn66.Width = 72;
            appearance182.TextHAlignAsString = "Right";
            ultraGridColumn67.CellAppearance = appearance182;
            ultraGridColumn67.Format = "c";
            appearance183.TextHAlignAsString = "Right";
            ultraGridColumn67.Header.Appearance = appearance183;
            ultraGridColumn67.Header.Caption = "Stop Amt";
            ultraGridColumn67.Header.VisiblePosition = 22;
            ultraGridColumn67.Width = 72;
            appearance184.TextHAlignAsString = "Right";
            ultraGridColumn68.CellAppearance = appearance184;
            appearance185.TextHAlignAsString = "Right";
            ultraGridColumn68.Header.Appearance = appearance185;
            ultraGridColumn68.Header.Caption = "Ctns";
            ultraGridColumn68.Header.VisiblePosition = 23;
            ultraGridColumn68.Width = 72;
            appearance186.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance186.TextHAlignAsString = "Right";
            ultraGridColumn69.CellAppearance = appearance186;
            appearance187.TextHAlignAsString = "Right";
            ultraGridColumn69.Header.Appearance = appearance187;
            ultraGridColumn69.Header.Caption = "Ctn Rate";
            ultraGridColumn69.Header.VisiblePosition = 24;
            ultraGridColumn69.Width = 72;
            appearance188.TextHAlignAsString = "Right";
            ultraGridColumn70.CellAppearance = appearance188;
            ultraGridColumn70.Format = "c";
            appearance189.TextHAlignAsString = "Right";
            ultraGridColumn70.Header.Appearance = appearance189;
            ultraGridColumn70.Header.Caption = "Ctn Amt";
            ultraGridColumn70.Header.VisiblePosition = 25;
            ultraGridColumn70.Width = 72;
            appearance190.TextHAlignAsString = "Right";
            ultraGridColumn71.CellAppearance = appearance190;
            appearance191.TextHAlignAsString = "Right";
            ultraGridColumn71.Header.Appearance = appearance191;
            ultraGridColumn71.Header.Caption = "Pllts";
            ultraGridColumn71.Header.VisiblePosition = 29;
            ultraGridColumn71.Width = 72;
            appearance192.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance192.TextHAlignAsString = "Right";
            ultraGridColumn72.CellAppearance = appearance192;
            appearance193.TextHAlignAsString = "Right";
            ultraGridColumn72.Header.Appearance = appearance193;
            ultraGridColumn72.Header.Caption = "Pllt Rate";
            ultraGridColumn72.Header.VisiblePosition = 30;
            ultraGridColumn72.Width = 72;
            appearance194.TextHAlignAsString = "Right";
            ultraGridColumn73.CellAppearance = appearance194;
            ultraGridColumn73.Format = "c";
            appearance195.TextHAlignAsString = "Right";
            ultraGridColumn73.Header.Appearance = appearance195;
            ultraGridColumn73.Header.Caption = "Pllt Amt";
            ultraGridColumn73.Header.VisiblePosition = 31;
            ultraGridColumn73.Width = 72;
            appearance196.TextHAlignAsString = "Right";
            ultraGridColumn74.CellAppearance = appearance196;
            appearance197.TextHAlignAsString = "Right";
            ultraGridColumn74.Header.Appearance = appearance197;
            ultraGridColumn74.Header.Caption = "PU Ctns";
            ultraGridColumn74.Header.VisiblePosition = 26;
            ultraGridColumn74.Width = 72;
            appearance198.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance198.TextHAlignAsString = "Right";
            ultraGridColumn75.CellAppearance = appearance198;
            appearance199.TextHAlignAsString = "Right";
            ultraGridColumn75.Header.Appearance = appearance199;
            ultraGridColumn75.Header.Caption = "PU Ctn Rate";
            ultraGridColumn75.Header.VisiblePosition = 27;
            ultraGridColumn75.Width = 72;
            appearance200.TextHAlignAsString = "Right";
            ultraGridColumn76.CellAppearance = appearance200;
            ultraGridColumn76.Format = "c";
            appearance201.TextHAlignAsString = "Right";
            ultraGridColumn76.Header.Appearance = appearance201;
            ultraGridColumn76.Header.Caption = "PU Ctn Amt";
            ultraGridColumn76.Header.VisiblePosition = 28;
            ultraGridColumn76.Width = 72;
            appearance202.TextHAlignAsString = "Right";
            ultraGridColumn77.CellAppearance = appearance202;
            ultraGridColumn77.Format = "c";
            appearance203.TextHAlignAsString = "Right";
            ultraGridColumn77.Header.Appearance = appearance203;
            ultraGridColumn77.Header.Caption = "Min Amt";
            ultraGridColumn77.Header.VisiblePosition = 32;
            ultraGridColumn77.Width = 72;
            appearance204.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance204.TextHAlignAsString = "Right";
            ultraGridColumn78.CellAppearance = appearance204;
            ultraGridColumn78.Format = "C";
            appearance205.TextHAlignAsString = "Right";
            ultraGridColumn78.Header.Appearance = appearance205;
            ultraGridColumn78.Header.Caption = "Fuel Cost";
            ultraGridColumn78.Header.VisiblePosition = 36;
            ultraGridColumn78.Width = 72;
            appearance206.TextHAlignAsString = "Right";
            ultraGridColumn79.CellAppearance = appearance206;
            ultraGridColumn79.Format = "";
            appearance207.TextHAlignAsString = "Right";
            ultraGridColumn79.Header.Appearance = appearance207;
            ultraGridColumn79.Header.Caption = "FSC Gal";
            ultraGridColumn79.Header.VisiblePosition = 35;
            ultraGridColumn79.Width = 72;
            appearance208.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance208.TextHAlignAsString = "Right";
            ultraGridColumn80.CellAppearance = appearance208;
            ultraGridColumn80.Format = "C";
            appearance209.TextHAlignAsString = "Right";
            ultraGridColumn80.Header.Appearance = appearance209;
            ultraGridColumn80.Header.Caption = "FSC Rate";
            ultraGridColumn80.Header.VisiblePosition = 37;
            ultraGridColumn80.Width = 72;
            appearance210.TextHAlignAsString = "Right";
            ultraGridColumn81.CellAppearance = appearance210;
            ultraGridColumn81.Format = "C";
            appearance211.TextHAlignAsString = "Right";
            ultraGridColumn81.Header.Appearance = appearance211;
            ultraGridColumn81.Header.VisiblePosition = 38;
            ultraGridColumn81.Width = 72;
            appearance212.TextHAlignAsString = "Right";
            ultraGridColumn82.CellAppearance = appearance212;
            ultraGridColumn82.Format = "C";
            appearance213.TextHAlignAsString = "Right";
            ultraGridColumn82.Header.Appearance = appearance213;
            ultraGridColumn82.Header.Caption = "Adj1 Amt";
            ultraGridColumn82.Header.VisiblePosition = 40;
            ultraGridColumn82.Width = 72;
            appearance214.TextHAlignAsString = "Right";
            ultraGridColumn83.CellAppearance = appearance214;
            appearance215.TextHAlignAsString = "Right";
            ultraGridColumn83.Header.Appearance = appearance215;
            ultraGridColumn83.Header.Caption = "Adj1 Type";
            ultraGridColumn83.Header.VisiblePosition = 41;
            ultraGridColumn83.Width = 96;
            appearance216.TextHAlignAsString = "Right";
            ultraGridColumn84.CellAppearance = appearance216;
            ultraGridColumn84.Format = "C";
            appearance217.TextHAlignAsString = "Right";
            ultraGridColumn84.Header.Appearance = appearance217;
            ultraGridColumn84.Header.Caption = "Adj2 Amt";
            ultraGridColumn84.Header.VisiblePosition = 42;
            ultraGridColumn84.Width = 72;
            appearance218.TextHAlignAsString = "Right";
            ultraGridColumn85.CellAppearance = appearance218;
            appearance219.TextHAlignAsString = "Right";
            ultraGridColumn85.Header.Appearance = appearance219;
            ultraGridColumn85.Header.Caption = "Adj2 Type";
            ultraGridColumn85.Header.VisiblePosition = 43;
            ultraGridColumn85.Width = 96;
            appearance220.TextHAlignAsString = "Right";
            ultraGridColumn86.CellAppearance = appearance220;
            ultraGridColumn86.Format = "C";
            appearance221.TextHAlignAsString = "Right";
            ultraGridColumn86.Header.Appearance = appearance221;
            ultraGridColumn86.Header.Caption = "Admin Fee";
            ultraGridColumn86.Header.VisiblePosition = 39;
            ultraGridColumn86.Width = 72;
            appearance222.TextHAlignAsString = "Right";
            ultraGridColumn87.CellAppearance = appearance222;
            ultraGridColumn87.Format = "C";
            appearance223.TextHAlignAsString = "Right";
            ultraGridColumn87.Header.Appearance = appearance223;
            ultraGridColumn87.Header.Caption = "Daily Amt";
            ultraGridColumn87.Header.VisiblePosition = 33;
            ultraGridColumn87.Width = 72;
            ultraGridColumn88.Header.VisiblePosition = 45;
            ultraGridColumn88.Width = 96;
            ultraGridColumn89.Header.VisiblePosition = 46;
            ultraGridColumn89.Hidden = true;
            ultraGridColumn90.Header.VisiblePosition = 47;
            ultraGridColumn90.Hidden = true;
            ultraGridColumn91.Header.VisiblePosition = 48;
            ultraGridColumn91.Hidden = true;
            ultraGridColumn92.Header.VisiblePosition = 49;
            ultraGridColumn92.Hidden = true;
            appearance224.TextHAlignAsString = "Right";
            ultraGridColumn93.CellAppearance = appearance224;
            ultraGridColumn93.Format = "#0.0";
            appearance225.TextHAlignAsString = "Right";
            ultraGridColumn93.Header.Appearance = appearance225;
            ultraGridColumn93.Header.Caption = "FSC Miles";
            ultraGridColumn93.Header.VisiblePosition = 34;
            ultraGridColumn93.Width = 72;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn44,
            ultraGridColumn45,
            ultraGridColumn46,
            ultraGridColumn47,
            ultraGridColumn48,
            ultraGridColumn49,
            ultraGridColumn50,
            ultraGridColumn51,
            ultraGridColumn52,
            ultraGridColumn53,
            ultraGridColumn54,
            ultraGridColumn55,
            ultraGridColumn56,
            ultraGridColumn57,
            ultraGridColumn58,
            ultraGridColumn59,
            ultraGridColumn60,
            ultraGridColumn61,
            ultraGridColumn62,
            ultraGridColumn63,
            ultraGridColumn64,
            ultraGridColumn65,
            ultraGridColumn66,
            ultraGridColumn67,
            ultraGridColumn68,
            ultraGridColumn69,
            ultraGridColumn70,
            ultraGridColumn71,
            ultraGridColumn72,
            ultraGridColumn73,
            ultraGridColumn74,
            ultraGridColumn75,
            ultraGridColumn76,
            ultraGridColumn77,
            ultraGridColumn78,
            ultraGridColumn79,
            ultraGridColumn80,
            ultraGridColumn81,
            ultraGridColumn82,
            ultraGridColumn83,
            ultraGridColumn84,
            ultraGridColumn85,
            ultraGridColumn86,
            ultraGridColumn87,
            ultraGridColumn88,
            ultraGridColumn89,
            ultraGridColumn90,
            ultraGridColumn91,
            ultraGridColumn92,
            ultraGridColumn93});
            appearance226.BackColor = System.Drawing.SystemColors.ControlLight;
            ultraGridBand2.Override.RowAlternateAppearance = appearance226;
            this.grdDriverRoutes.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdDriverRoutes.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.grdDriverRoutes.DisplayLayout.BorderStyleCaption = Infragistics.Win.UIElementBorderStyle.WindowsVista;
            appearance50.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance50.FontData.BoldAsString = "True";
            appearance50.FontData.Name = "Verdana";
            appearance50.FontData.SizeInPoints = 8F;
            appearance50.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.grdDriverRoutes.DisplayLayout.CaptionAppearance = appearance50;
            this.grdDriverRoutes.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdDriverRoutes.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            this.grdDriverRoutes.DisplayLayout.Override.AllowColSwapping = Infragistics.Win.UltraWinGrid.AllowColSwapping.NotAllowed;
            this.grdDriverRoutes.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdDriverRoutes.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            this.grdDriverRoutes.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
            this.grdDriverRoutes.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.HeaderIcons;
            this.grdDriverRoutes.DisplayLayout.Override.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            appearance51.BackColor = System.Drawing.SystemColors.Control;
            appearance51.FontData.BoldAsString = "True";
            appearance51.FontData.Name = "Verdana";
            appearance51.FontData.SizeInPoints = 8F;
            appearance51.TextHAlignAsString = "Left";
            this.grdDriverRoutes.DisplayLayout.Override.HeaderAppearance = appearance51;
            this.grdDriverRoutes.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            appearance52.BorderColor = System.Drawing.SystemColors.WindowText;
            this.grdDriverRoutes.DisplayLayout.Override.RowAppearance = appearance52;
            this.grdDriverRoutes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            this.grdDriverRoutes.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.Fixed;
            this.grdDriverRoutes.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended;
            this.grdDriverRoutes.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdDriverRoutes.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdDriverRoutes.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdDriverRoutes.DisplayLayout.UseFixedHeaders = true;
            this.grdDriverRoutes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDriverRoutes.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.grdDriverRoutes.Location = new System.Drawing.Point(0,0);
            this.grdDriverRoutes.Name = "grdDriverRoutes";
            this.grdDriverRoutes.RowUpdateCancelAction = Infragistics.Win.UltraWinGrid.RowUpdateCancelAction.RetainDataAndActivation;
            this.grdDriverRoutes.Size = new System.Drawing.Size(743,246);
            this.grdDriverRoutes.TabIndex = 1;
            this.grdDriverRoutes.Text = "Driver Routes";
            this.grdDriverRoutes.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnUpdate;
            this.grdDriverRoutes.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdDriverRoutes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdDriverRoutes.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnGridInitializeRow);
            this.grdDriverRoutes.AfterRowUpdate += new Infragistics.Win.UltraWinGrid.RowEventHandler(this.OnGridAfterRowUpdate);
            this.grdDriverRoutes.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.OnGridBeforeRowFilterDropDownPopulate);
            this.grdDriverRoutes.BeforeExitEditMode += new Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventHandler(this.OnGridBeforeExitEditMode);
            this.grdDriverRoutes.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnGridInitializeLayout);
            this.grdDriverRoutes.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnGridKeyUp);
            // 
            // mDriverCompDS
            // 
            this.mDriverCompDS.DataSetName = "DriverCompDS";
            this.mDriverCompDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // splitterH
            // 
            this.splitterH.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitterH.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterH.Location = new System.Drawing.Point(0,246);
            this.splitterH.Name = "splitterH";
            this.splitterH.Size = new System.Drawing.Size(743,3);
            this.splitterH.TabIndex = 112;
            this.splitterH.TabStop = false;
            // 
            // grdRoadshowRoutes
            // 
            this.grdRoadshowRoutes.ContextMenuStrip = this.ctxRoutes;
            this.grdRoadshowRoutes.Controls.Add(this.lblCloseRoutes);
            this.grdRoadshowRoutes.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdRoadshowRoutes.DataMember = "RoadshowRouteTable";
            this.grdRoadshowRoutes.DataSource = this.mDriverCompDS;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.Name = "Verdana";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdRoadshowRoutes.DisplayLayout.Appearance = appearance1;
            ultraGridColumn94.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn94.Header.Caption = "";
            ultraGridColumn94.Header.VisiblePosition = 0;
            ultraGridColumn94.Width = 24;
            ultraGridColumn95.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn95.Format = "D";
            ultraGridColumn95.Header.Caption = "Date";
            ultraGridColumn95.Header.VisiblePosition = 6;
            ultraGridColumn95.Width = 144;
            ultraGridColumn96.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn96.Header.Caption = "Route";
            ultraGridColumn96.Header.VisiblePosition = 9;
            ultraGridColumn96.Width = 72;
            ultraGridColumn97.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn97.Header.VisiblePosition = 5;
            ultraGridColumn97.Width = 168;
            ultraGridColumn98.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn98.Header.Caption = "EquipmentType";
            ultraGridColumn98.Header.VisiblePosition = 8;
            ultraGridColumn98.Hidden = true;
            ultraGridColumn98.Width = 72;
            ultraGridColumn99.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn99.Header.VisiblePosition = 4;
            ultraGridColumn99.Hidden = true;
            ultraGridColumn99.Width = 96;
            ultraGridColumn100.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn100.Header.Caption = "Vendor#";
            ultraGridColumn100.Header.VisiblePosition = 3;
            ultraGridColumn100.Width = 72;
            ultraGridColumn101.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            appearance227.TextHAlignAsString = "Right";
            ultraGridColumn101.CellAppearance = appearance227;
            ultraGridColumn101.Format = "#0.0";
            appearance228.TextHAlignAsString = "Right";
            ultraGridColumn101.Header.Appearance = appearance228;
            ultraGridColumn101.Header.Caption = "Miles";
            ultraGridColumn101.Header.VisiblePosition = 10;
            ultraGridColumn101.Width = 72;
            ultraGridColumn102.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            appearance229.TextHAlignAsString = "Right";
            ultraGridColumn102.CellAppearance = appearance229;
            appearance230.TextHAlignAsString = "Right";
            ultraGridColumn102.Header.Appearance = appearance230;
            ultraGridColumn102.Header.Caption = "Trips";
            ultraGridColumn102.Header.VisiblePosition = 11;
            ultraGridColumn102.Width = 72;
            ultraGridColumn103.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            appearance231.TextHAlignAsString = "Right";
            ultraGridColumn103.CellAppearance = appearance231;
            appearance232.TextHAlignAsString = "Right";
            ultraGridColumn103.Header.Appearance = appearance232;
            ultraGridColumn103.Header.Caption = "Stops";
            ultraGridColumn103.Header.VisiblePosition = 12;
            ultraGridColumn103.Width = 72;
            ultraGridColumn104.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            appearance233.TextHAlignAsString = "Right";
            ultraGridColumn104.CellAppearance = appearance233;
            appearance234.TextHAlignAsString = "Right";
            ultraGridColumn104.Header.Appearance = appearance234;
            ultraGridColumn104.Header.Caption = "Ctns";
            ultraGridColumn104.Header.VisiblePosition = 13;
            ultraGridColumn104.Width = 72;
            ultraGridColumn105.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            appearance235.TextHAlignAsString = "Right";
            ultraGridColumn105.CellAppearance = appearance235;
            appearance236.TextHAlignAsString = "Right";
            ultraGridColumn105.Header.Appearance = appearance236;
            ultraGridColumn105.Header.Caption = "Rtn Ctns";
            ultraGridColumn105.Header.VisiblePosition = 14;
            ultraGridColumn105.Width = 72;
            ultraGridColumn106.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            appearance237.TextHAlignAsString = "Right";
            ultraGridColumn106.CellAppearance = appearance237;
            appearance238.TextHAlignAsString = "Right";
            ultraGridColumn106.Header.Appearance = appearance238;
            ultraGridColumn106.Header.Caption = "Pllts";
            ultraGridColumn106.Header.VisiblePosition = 15;
            ultraGridColumn106.Width = 72;
            ultraGridColumn107.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn107.Header.VisiblePosition = 2;
            ultraGridColumn107.Hidden = true;
            ultraGridColumn107.Width = 96;
            ultraGridColumn108.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn108.Header.Caption = "Depot#";
            ultraGridColumn108.Header.VisiblePosition = 1;
            ultraGridColumn108.Hidden = true;
            ultraGridColumn108.Width = 72;
            ultraGridColumn109.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn109.Header.Caption = "Equip Type";
            ultraGridColumn109.Header.VisiblePosition = 7;
            ultraGridColumn109.Width = 78;
            ultraGridColumn110.Header.VisiblePosition = 16;
            ultraGridBand3.Columns.AddRange(new object[] {
            ultraGridColumn94,
            ultraGridColumn95,
            ultraGridColumn96,
            ultraGridColumn97,
            ultraGridColumn98,
            ultraGridColumn99,
            ultraGridColumn100,
            ultraGridColumn101,
            ultraGridColumn102,
            ultraGridColumn103,
            ultraGridColumn104,
            ultraGridColumn105,
            ultraGridColumn106,
            ultraGridColumn107,
            ultraGridColumn108,
            ultraGridColumn109,
            ultraGridColumn110});
            ultraGridBand3.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
            ultraGridBand3.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.Edit;
            this.grdRoadshowRoutes.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
            this.grdRoadshowRoutes.DisplayLayout.BorderStyleCaption = Infragistics.Win.UIElementBorderStyle.WindowsVista;
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.FontData.Name = "Verdana";
            appearance2.FontData.SizeInPoints = 8F;
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.grdRoadshowRoutes.DisplayLayout.CaptionAppearance = appearance2;
            this.grdRoadshowRoutes.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdRoadshowRoutes.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdRoadshowRoutes.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdRoadshowRoutes.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
            this.grdRoadshowRoutes.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.Name = "Verdana";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.TextHAlignAsString = "Left";
            this.grdRoadshowRoutes.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdRoadshowRoutes.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdRoadshowRoutes.DisplayLayout.Override.MaxSelectedRows = 0;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdRoadshowRoutes.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdRoadshowRoutes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdRoadshowRoutes.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.ExtendedAutoDrag;
            this.grdRoadshowRoutes.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
            this.grdRoadshowRoutes.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdRoadshowRoutes.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdRoadshowRoutes.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdRoadshowRoutes.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdRoadshowRoutes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdRoadshowRoutes.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.grdRoadshowRoutes.Location = new System.Drawing.Point(0,249);
            this.grdRoadshowRoutes.Name = "grdRoadshowRoutes";
            this.grdRoadshowRoutes.Size = new System.Drawing.Size(743,82);
            this.grdRoadshowRoutes.TabIndex = 119;
            this.grdRoadshowRoutes.Text = "Roadshow Routes";
            this.grdRoadshowRoutes.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnUpdate;
            this.grdRoadshowRoutes.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdRoadshowRoutes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridRMouseDown);
            this.grdRoadshowRoutes.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnGridRInitializeRow);
            this.grdRoadshowRoutes.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.OnGridRBeforeRowFilterDropDownPopulate);
            this.grdRoadshowRoutes.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnGridRInitializeLayout);
            // 
            // lblCloseRoutes
            // 
            this.lblCloseRoutes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCloseRoutes.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lblCloseRoutes.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.lblCloseRoutes.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblCloseRoutes.Location = new System.Drawing.Point(724,3);
            this.lblCloseRoutes.Name = "lblCloseRoutes";
            this.lblCloseRoutes.Size = new System.Drawing.Size(16,16);
            this.lblCloseRoutes.TabIndex = 120;
            this.lblCloseRoutes.Text = "X";
            this.lblCloseRoutes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCloseRoutes.Click += new System.EventHandler(this.OnCloseRoutes);
            // 
            // tabSummary
            // 
            this.tabSummary.Controls.Add(this.rsvSummary);
            this.tabSummary.Location = new System.Drawing.Point(4,4);
            this.tabSummary.Name = "tabSummary";
            this.tabSummary.Padding = new System.Windows.Forms.Padding(3);
            this.tabSummary.Size = new System.Drawing.Size(743,331);
            this.tabSummary.TabIndex = 1;
            this.tabSummary.Text = "Cost/Carton Summary";
            this.tabSummary.UseVisualStyleBackColor = true;
            // 
            // rsvSummary
            // 
            this.rsvSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rsvSummary.Location = new System.Drawing.Point(3,3);
            this.rsvSummary.Name = "rsvSummary";
            this.rsvSummary.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            this.rsvSummary.ServerReport.DisplayName = "Driver Report";
            this.rsvSummary.ServerReport.ReportPath = "/Finance/Driver Summary";
            this.rsvSummary.ShowBackButton = false;
            this.rsvSummary.ShowCredentialPrompts = false;
            this.rsvSummary.ShowDocumentMapButton = false;
            this.rsvSummary.ShowFindControls = false;
            this.rsvSummary.ShowPageNavigationControls = false;
            this.rsvSummary.ShowParameterPrompts = false;
            this.rsvSummary.ShowProgress = false;
            this.rsvSummary.ShowPromptAreaButton = false;
            this.rsvSummary.ShowStopButton = false;
            this.rsvSummary.ShowToolBar = false;
            this.rsvSummary.Size = new System.Drawing.Size(737,325);
            this.rsvSummary.TabIndex = 1;
            // 
            // tabDriverComp
            // 
            this.tabDriverComp.Controls.Add(this.rsvDrivers);
            this.tabDriverComp.Controls.Add(this.cboOperators);
            this.tabDriverComp.Location = new System.Drawing.Point(4,4);
            this.tabDriverComp.Name = "tabDriverComp";
            this.tabDriverComp.Size = new System.Drawing.Size(743,331);
            this.tabDriverComp.TabIndex = 2;
            this.tabDriverComp.Text = "Driver Compensation";
            this.tabDriverComp.UseVisualStyleBackColor = true;
            // 
            // rsvDrivers
            // 
            this.rsvDrivers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rsvDrivers.Location = new System.Drawing.Point(0,21);
            this.rsvDrivers.Name = "rsvDrivers";
            this.rsvDrivers.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            this.rsvDrivers.ServerReport.DisplayName = "Driver Report";
            this.rsvDrivers.ServerReport.ReportPath = "/Finance/Driver Compensation";
            this.rsvDrivers.ShowBackButton = false;
            this.rsvDrivers.ShowCredentialPrompts = false;
            this.rsvDrivers.ShowDocumentMapButton = false;
            this.rsvDrivers.ShowFindControls = false;
            this.rsvDrivers.ShowParameterPrompts = false;
            this.rsvDrivers.ShowPromptAreaButton = false;
            this.rsvDrivers.ShowStopButton = false;
            this.rsvDrivers.Size = new System.Drawing.Size(743,310);
            this.rsvDrivers.TabIndex = 1;
            // 
            // cboOperators
            // 
            this.cboOperators.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboOperators.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOperators.FormattingEnabled = true;
            this.cboOperators.Location = new System.Drawing.Point(0,0);
            this.cboOperators.Name = "cboOperators";
            this.cboOperators.Size = new System.Drawing.Size(743,21);
            this.cboOperators.Sorted = true;
            this.cboOperators.TabIndex = 2;
            this.cboOperators.SelectionChangeCommitted += new System.EventHandler(this.OnOperatorChanged);
            // 
            // tabOwnerComp
            // 
            this.tabOwnerComp.Controls.Add(this.rsOwners);
            this.tabOwnerComp.Controls.Add(this.cboOwners);
            this.tabOwnerComp.Location = new System.Drawing.Point(4,4);
            this.tabOwnerComp.Name = "tabOwnerComp";
            this.tabOwnerComp.Size = new System.Drawing.Size(743,331);
            this.tabOwnerComp.TabIndex = 4;
            this.tabOwnerComp.Text = "Fleet Owner Compensation";
            this.tabOwnerComp.UseVisualStyleBackColor = true;
            // 
            // rsOwners
            // 
            this.rsOwners.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rsOwners.Location = new System.Drawing.Point(0,21);
            this.rsOwners.Name = "rsOwners";
            this.rsOwners.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            this.rsOwners.ServerReport.DisplayName = "Fleet Owner Report";
            this.rsOwners.ServerReport.ReportPath = "/Finance/Fleet Owner Compensation";
            this.rsOwners.ShowBackButton = false;
            this.rsOwners.ShowCredentialPrompts = false;
            this.rsOwners.ShowDocumentMapButton = false;
            this.rsOwners.ShowFindControls = false;
            this.rsOwners.ShowParameterPrompts = false;
            this.rsOwners.ShowPromptAreaButton = false;
            this.rsOwners.ShowStopButton = false;
            this.rsOwners.Size = new System.Drawing.Size(743,310);
            this.rsOwners.TabIndex = 2;
            // 
            // cboOwners
            // 
            this.cboOwners.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboOwners.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOwners.FormattingEnabled = true;
            this.cboOwners.Location = new System.Drawing.Point(0,0);
            this.cboOwners.Name = "cboOwners";
            this.cboOwners.Size = new System.Drawing.Size(743,21);
            this.cboOwners.Sorted = true;
            this.cboOwners.TabIndex = 3;
            this.cboOwners.SelectionChangeCommitted += new System.EventHandler(this.OnOwnerChanged);
            // 
            // tabExport
            // 
            this.tabExport.Controls.Add(this.txtExport);
            this.tabExport.Location = new System.Drawing.Point(4,4);
            this.tabExport.Name = "tabExport";
            this.tabExport.Padding = new System.Windows.Forms.Padding(3);
            this.tabExport.Size = new System.Drawing.Size(743,331);
            this.tabExport.TabIndex = 3;
            this.tabExport.Text = "Export Data";
            this.tabExport.UseVisualStyleBackColor = true;
            // 
            // txtExport
            // 
            this.txtExport.ContextMenuStrip = this.ctxExport;
            this.txtExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExport.Font = new System.Drawing.Font("Courier New",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.txtExport.Location = new System.Drawing.Point(3,3);
            this.txtExport.Name = "txtExport";
            this.txtExport.Size = new System.Drawing.Size(737,325);
            this.txtExport.TabIndex = 0;
            this.txtExport.Text = "";
            this.txtExport.WordWrap = false;
            // 
            // ctxExport
            // 
            this.ctxExport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxERefresh,
            this.ctxESep1,
            this.ctxEExport});
            this.ctxExport.Name = "ctxRoutes";
            this.ctxExport.Size = new System.Drawing.Size(239,76);
            // 
            // ctxERefresh
            // 
            this.ctxERefresh.Image = ((System.Drawing.Image)(resources.GetObject("ctxERefresh.Image")));
            this.ctxERefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxERefresh.Name = "ctxERefresh";
            this.ctxERefresh.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.ctxERefresh.Size = new System.Drawing.Size(238,22);
            this.ctxERefresh.Text = "&Refresh Export";
            this.ctxERefresh.ToolTipText = "Refresh export";
            this.ctxERefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxESep1
            // 
            this.ctxESep1.Name = "ctxESep1";
            this.ctxESep1.Size = new System.Drawing.Size(235,6);
            // 
            // ctxEExport
            // 
            this.ctxEExport.Image = global::Argix.Properties.Resources.ImportXML;
            this.ctxEExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxEExport.Name = "ctxEExport";
            this.ctxEExport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.ctxEExport.Size = new System.Drawing.Size(238,22);
            this.ctxEExport.Text = "&Export Compensation...";
            this.ctxEExport.ToolTipText = "Export compensation";
            this.ctxEExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // uddAdjType
            // 
            this.uddAdjType.Cursor = System.Windows.Forms.Cursors.Default;
            this.uddAdjType.Location = new System.Drawing.Point(215,336);
            this.uddAdjType.Name = "uddAdjType";
            this.uddAdjType.Size = new System.Drawing.Size(104,18);
            this.uddAdjType.TabIndex = 113;
            this.uddAdjType.Text = "ultraDropDown1";
            this.uddAdjType.Visible = false;
            // 
            // winDriverComp
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
            this.ClientSize = new System.Drawing.Size(751,355);
            this.Controls.Add(this.tabDialog);
            this.Controls.Add(this.uddEquipType);
            this.Controls.Add(this.uddRateType);
            this.Controls.Add(this.uddAdjType);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "winDriverComp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Driver Compensation";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.uddRateType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddEquipType)).EndInit();
            this.ctxRoutes.ResumeLayout(false);
            this.ctxComp.ResumeLayout(false);
            this.tabDialog.ResumeLayout(false);
            this.tabRoutes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverRoutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mDriverCompDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRoadshowRoutes)).EndInit();
            this.grdRoadshowRoutes.ResumeLayout(false);
            this.tabSummary.ResumeLayout(false);
            this.tabDriverComp.ResumeLayout(false);
            this.tabOwnerComp.ResumeLayout(false);
            this.tabExport.ResumeLayout(false);
            this.ctxExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uddAdjType)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
                //Create menu selections for adding equipment overrides
                DataSet ds = EnterpriseFactory.DriverEquipmentTypes;
                for(int i = 0; i < ds.Tables[EnterpriseFactory.TBL_EQUIPTYPE].Rows.Count; i++) {
                    ToolStripItem item = new ToolStripMenuItem();
                    item.Text = ds.Tables[EnterpriseFactory.TBL_EQUIPTYPE].Rows[i]["Description"].ToString();
                    item.Tag = ds.Tables[EnterpriseFactory.TBL_EQUIPTYPE].Rows[i]["ID"];
                    item.Click += new EventHandler(OnAddEquipmentOverrideClick);
                    this.ctxCEquipOverride.DropDownItems.Add(item);
                }
				#region Grid Initialization
                this.grdDriverRoutes.DisplayLayout.Bands["DriverCompTable"].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdDriverRoutes.DisplayLayout.Bands["DriverCompTable"].Columns["Operator"].SortIndicator = SortIndicator.Ascending;
                this.grdDriverRoutes.DisplayLayout.Bands["DriverCompTable_DriverRouteTable"].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdDriverRoutes.DisplayLayout.Bands["DriverCompTable_DriverRouteTable"].Columns["RouteDate"].SortIndicator = SortIndicator.Ascending;
				this.grdDriverRoutes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                this.grdDriverRoutes.UpdateMode = (UpdateMode.OnCellChangeOrLostFocus & UpdateMode.OnUpdate);
                this.grdRoadshowRoutes.DisplayLayout.Bands["RoadshowRouteTable"].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdRoadshowRoutes.DisplayLayout.Bands["RoadshowRouteTable"].Columns["Operator"].SortIndicator = SortIndicator.Ascending;
                this.grdRoadshowRoutes.UpdateMode = UpdateMode.OnCellChange;
                this.grdRoadshowRoutes.DisplayLayout.Bands["RoadshowRouteTable"].ColumnFilters["New"].FilterConditions.Add(FilterComparisionOperator.Equals, true);

                this.uddEquipType.DataSource = EnterpriseFactory.DriverEquipmentTypes;
                this.uddEquipType.DataMember = EnterpriseFactory.TBL_EQUIPTYPE;
                this.uddEquipType.DisplayMember = "Description";
                this.uddEquipType.ValueMember = "ID";
                this.uddAdjType.DataSource = FinanceFactory.RouteAdjustmentTypes;
                this.uddAdjType.DataMember = FinanceFactory.TBL_ADJUSTTYPE;
                this.uddAdjType.DisplayMember = "AdjustmentType";
                this.uddAdjType.ValueMember = "AdjustmentType";
                this.uddRateType.DataSource = DriverRatingFactory.RateTypes;
                this.uddRateType.DataMember = DriverRatingFactory.TBL_RATETYPE;
                this.uddRateType.DisplayMember = "Description";
                this.uddRateType.ValueMember = "ID";
                #endregion 
                this.grdDriverRoutes.DataSource = this.mDriverComp.DriverRoutes;
                this.grdRoadshowRoutes.DataSource = this.mDriverComp.RoadshowRoutes;
                RoadshowRoutesVisible = global::Argix.Properties.Settings.Default.RoadshowWindow;
                this.txtExport.ReadOnly = true;
                OnShowRates(this.chkShowRates,EventArgs.Empty);
                this.grdDriverRoutes.Select();
            }
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnFormClosed(object sender,FormClosedEventArgs e) {
            //Event handler for form closed event
            global::Argix.Properties.Settings.Default.RoadshowWindow = RoadshowRoutesVisible;
        }
        private void OnTabSelected(object sender,TabControlEventArgs e) {
            //Event handler for change in tab selection
            this.Cursor = Cursors.WaitCursor;
            try {
                switch(e.TabPage.Name) {
                    case "tabRoutes": 
                        break;
                    case "tabSummary":
                        Refresh();
                        break;
                    case "tabDriverComp":
                        this.cboOperators.Items.Clear();
                        this.cboOperators.Items.Add(REPORT_ALL_OPERATORS);
                        for(int i = 0; i < this.mDriverComp.DriverRoutes.DriverCompTable.Rows.Count; i++) {
                            if(this.mDriverComp.DriverRoutes.DriverCompTable[i].Select)
                                this.cboOperators.Items.Add(this.mDriverComp.DriverRoutes.DriverCompTable[i].Operator);
                        }
                        if(this.cboOperators.Items.Count > 0) this.cboOperators.SelectedIndex = (this.mLastOperator<this.cboOperators.Items.Count?this.mLastOperator:0);
                        this.cboOperators.Enabled = this.rsvDrivers.Enabled = this.cboOperators.Items.Count > 0;
                        Refresh();
                        break;
                    case "tabOwnerComp":
                        this.cboOwners.Items.Clear();
                        this.cboOwners.Items.Add(REPORT_ALL_OWNERS);
                        for(int i = 0;i < this.mDriverComp.DriverRoutes.DriverCompTable.Rows.Count;i++) {
                            string owner = this.mDriverComp.DriverRoutes.DriverCompTable[i].FinanceVendor;
                            if(this.mDriverComp.DriverRoutes.DriverCompTable.Select("FinanceVendor=\'" + owner.Replace("'","") + "\'").Length > 1) {
                                if(this.cboOwners.Items.IndexOf(owner) == -1)
                                    this.cboOwners.Items.Add(owner);
                            }
                        }
                        if(this.cboOwners.Items.Count > 0) this.cboOwners.SelectedIndex = (this.mLastOwner < this.cboOwners.Items.Count ? this.mLastOwner : 0);
                        this.cboOwners.Enabled = this.rsOwners.Enabled = this.cboOwners.Items.Count > 0;
                        Refresh();
                        break;
                    case "tabExport":
                        Refresh(); 
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnShowRates(object sender,EventArgs e) {
            //Event handler for show/hide rate columns
            try {
                bool hideRates = !this.chkShowRates.Checked;
                UltraGridBand summaryBand = this.grdDriverRoutes.DisplayLayout.Bands["DriverCompTable"];
                summaryBand.Columns["MilesRate"].Hidden = hideRates;
                summaryBand.Columns["MilesBaseRate"].Hidden = hideRates;
                summaryBand.Columns["DayRate"].Hidden = hideRates;
                summaryBand.Columns["TripRate"].Hidden = hideRates;
                summaryBand.Columns["StopsRate"].Hidden = hideRates;
                summaryBand.Columns["CartonsRate"].Hidden = hideRates;
                summaryBand.Columns["PalletsRate"].Hidden = hideRates;
                summaryBand.Columns["PickupCartonsRate"].Hidden = hideRates;
                summaryBand.Columns["FuelCost"].Hidden = hideRates;
                summaryBand.Columns["FSCBaseRate"].Hidden = hideRates;
                UltraGridBand routeBand = this.grdDriverRoutes.DisplayLayout.Bands["DriverCompTable_DriverRouteTable"];
                routeBand.Columns["MilesRate"].Hidden = hideRates;
                routeBand.Columns["MilesBaseRate"].Hidden = hideRates;
                routeBand.Columns["DayRate"].Hidden = hideRates;
                routeBand.Columns["TripRate"].Hidden = hideRates;
                routeBand.Columns["StopsRate"].Hidden = hideRates;
                routeBand.Columns["CartonsRate"].Hidden = hideRates;
                routeBand.Columns["PalletsRate"].Hidden = hideRates;
                routeBand.Columns["PickupCartonsRate"].Hidden = hideRates;
                routeBand.Columns["FuelCost"].Hidden = hideRates;
                routeBand.Columns["FSCBaseRate"].Hidden = hideRates;
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnOperatorChanged(object sender,EventArgs e) {
            //Event handler for change in selected operator
            this.Cursor = Cursors.WaitCursor;
            try {
                if(this.cboOperators.Items.Count > 0) {
                    //Display driver compensation for selected operator
                    reportStatus(new StatusEventArgs("Refreshing driver paystubs..."));
                    ReportParameter p1 = new ReportParameter("AgentNumber",this.mDriverComp.AgentNumber.Trim());
                    ReportParameter p2 = new ReportParameter("AgentName",this.mDriverComp.AgentName.Trim());
                    ReportParameter p3 = new ReportParameter("StartDate",this.mDriverComp.StartDate.ToString("yyyy-MM-dd"));
                    ReportParameter p4 = new ReportParameter("EndDate",this.mDriverComp.EndDate.ToString("yyyy-MM-dd"));
                    ReportParameter p5 = new ReportParameter("Operator",(this.cboOperators.Text == REPORT_ALL_OPERATORS ? null : this.cboOperators.Text));
                    this.rsvDrivers.ServerReport.DisplayName = "Driver Compensation";
                    this.rsvDrivers.ServerReport.ReportPath = global::Argix.Properties.Settings.Default.ReportPathDriverComp + this.mDriverComp.AgentNumber;
                    this.rsvDrivers.ServerReport.SetParameters(new ReportParameter[] { p1,p2,p3,p4,p5 });
                    this.rsvDrivers.RefreshReport();

                    //Memory
                    this.mLastOperator = this.cboOperators.SelectedIndex;
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnOwnerChanged(object sender,EventArgs e) {
            //Event handler for change in selected fleet owner
            this.Cursor = Cursors.WaitCursor;
            try {
                if(this.cboOwners.Items.Count > 0) {
                    //Display comapensation for selected Fleet Owner
                    reportStatus(new StatusEventArgs("Refreshing fleet owner paystubs..."));
                    ReportParameter p1 = new ReportParameter("AgentNumber",this.mDriverComp.AgentNumber.Trim());
                    ReportParameter p2 = new ReportParameter("AgentName",this.mDriverComp.AgentName.Trim());
                    ReportParameter p3 = new ReportParameter("StartDate",this.mDriverComp.StartDate.ToString("yyyy-MM-dd"));
                    ReportParameter p4 = new ReportParameter("EndDate",this.mDriverComp.EndDate.ToString("yyyy-MM-dd"));
                    ReportParameter p5 = new ReportParameter("Owner",(this.cboOwners.Text == REPORT_ALL_OWNERS ? null : this.cboOwners.Text));
                    this.rsOwners.ServerReport.DisplayName = "Fleet Owner";
                    this.rsOwners.ServerReport.ReportPath = global::Argix.Properties.Settings.Default.ReportPathFleetComp;
                    this.rsOwners.ServerReport.SetParameters(new ReportParameter[] { p1,p2,p3,p4,p5 });
                    this.rsOwners.RefreshReport();

                    //Memory
                    this.mLastOwner = this.cboOwners.SelectedIndex;
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnDriverRoutesChanged(object sender,EventArgs e) {
            //Event handler for change in driver routes
            setUserServices();
        }
        private void OnRoadshowRoutesChanged(object sender,EventArgs e) {
            //Event handler for change in Roadshow routes
            setUserServices();
        }
        private void OnCacheChanged(object sender,EventArgs e) {
            //Rebind applicable objects
            try {
                this.uddEquipType.DataSource = EnterpriseFactory.DriverEquipmentTypes;
                this.uddEquipType.DataBind();
                this.uddAdjType.DataSource = FinanceFactory.RouteAdjustmentTypes;
                this.uddAdjType.DataBind();
                this.uddRateType.DataSource = DriverRatingFactory.RateTypes;
                this.uddRateType.DataBind();
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        #region Comp Grid Services: OnGridInitializeLayout(), OnGridInitializeRow(), OnGridMouseDown(), OnGridKeyUp()
        private void OnGridInitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
			//
            e.Layout.Bands["DriverCompTable"].Columns["EquipmentTypeID"].ValueList = this.uddEquipType;
            e.Layout.Bands["DriverCompTable_DriverRouteTable"].Columns["EquipmentTypeID"].ValueList = this.uddEquipType;
            e.Layout.Bands["DriverCompTable_DriverRouteTable"].Columns["RateTypeID"].ValueList = this.uddRateType;
            e.Layout.Bands["DriverCompTable_DriverRouteTable"].Columns["AdjustmentAmount1TypeID"].ValueList = this.uddAdjType;
            e.Layout.Bands["DriverCompTable_DriverRouteTable"].Columns["AdjustmentAmount2TypeID"].ValueList = this.uddAdjType;
        }
		private void OnGridInitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e) {
			//
            if(e.Row.Band.Key == "DriverCompTable") {
                e.Row.Cells["Select"].Activation = Activation.AllowEdit;
                e.Row.Cells["IsNew"].Activation = Activation.NoEdit;
                e.Row.Cells["IsCombo"].Activation = Activation.NoEdit;
                e.Row.Cells["IsAdjust"].Activation = Activation.NoEdit;
                e.Row.Cells["AgentNumber"].Activation = Activation.NoEdit;
                e.Row.Cells["FinanceVendorID"].Activation = Activation.NoEdit;
                e.Row.Cells["FinanceVendor"].Activation = Activation.NoEdit;
                e.Row.Cells["Operator"].Activation = Activation.NoEdit;
                e.Row.Cells["EquipmentTypeID"].Activation = Activation.NoEdit;
                e.Row.Cells["EquipmentType"].Activation = Activation.NoEdit;
                e.Row.Cells["DayRate"].Activation = Activation.NoEdit;
                e.Row.Cells["DayAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["Miles"].Activation = Activation.NoEdit;
                e.Row.Cells["MilesBaseRate"].Activation = Activation.NoEdit;
                e.Row.Cells["MilesRate"].Activation = Activation.NoEdit;
                e.Row.Cells["MilesAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["Trip"].Activation = Activation.NoEdit;
                e.Row.Cells["TripRate"].Activation = Activation.NoEdit;
                e.Row.Cells["TripAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["Stops"].Activation = Activation.NoEdit;
                e.Row.Cells["StopsRate"].Activation = Activation.NoEdit;
                e.Row.Cells["StopsAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["Cartons"].Activation = Activation.NoEdit;
                e.Row.Cells["CartonsRate"].Activation = Activation.NoEdit;
                e.Row.Cells["CartonsAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["Pallets"].Activation = Activation.NoEdit;
                e.Row.Cells["PalletsRate"].Activation = Activation.NoEdit;
                e.Row.Cells["PalletsAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["PickupCartons"].Activation = Activation.NoEdit;
                e.Row.Cells["PickupCartonsRate"].Activation = Activation.NoEdit;
                e.Row.Cells["PickupCartonsAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["MinimunAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["Amount"].Activation = Activation.NoEdit;
                e.Row.Cells["FSCMiles"].Activation = Activation.NoEdit;
                e.Row.Cells["FuelCost"].Activation = Activation.NoEdit;
                e.Row.Cells["FSCGal"].Activation = Activation.NoEdit;
                e.Row.Cells["FSCBaseRate"].Activation = Activation.NoEdit;
                e.Row.Cells["FSC"].Activation = Activation.NoEdit;
                e.Row.Cells["AdjustmentAmount1"].Activation = Activation.NoEdit;
                e.Row.Cells["AdjustmentAmount2"].Activation = Activation.NoEdit;
                e.Row.Cells["AdminCharge"].Activation = Activation.NoEdit;
                e.Row.Cells["TotalAmount"].Activation = Activation.NoEdit;
            }
            if(e.Row.Band.Key == "DriverCompTable_DriverRouteTable") {
                e.Row.Cells["ID"].Activation = Activation.NoEdit;
                e.Row.Cells["IsNew"].Activation = Activation.NoEdit;
                e.Row.Cells["IsCombo"].Activation = Activation.NoEdit;
                e.Row.Cells["IsAdjust"].Activation = Activation.NoEdit;
                e.Row.Cells["AgentNumber"].Activation = Activation.NoEdit;
                e.Row.Cells["FinanceVendorID"].Activation = Activation.NoEdit;
                e.Row.Cells["RouteDate"].Activation = Activation.NoEdit;
                e.Row.Cells["RouteName"].Activation = Activation.NoEdit;
                e.Row.Cells["Operator"].Activation = Activation.NoEdit;
                e.Row.Cells["Payee"].Activation = Activation.NoEdit;
                e.Row.Cells["EquipmentTypeID"].Activation = Activation.NoEdit;
                e.Row.Cells["RateTypeID"].Activation = Activation.NoEdit;
                e.Row.Cells["DayRate"].Activation = Activation.NoEdit;
                e.Row.Cells["DayAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["Miles"].Activation = Activation.NoEdit;
                e.Row.Cells["MilesBaseRate"].Activation = Activation.NoEdit;
                e.Row.Cells["MilesRate"].Activation = Activation.NoEdit;
                e.Row.Cells["MilesAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["Trip"].Activation = Activation.NoEdit;
                e.Row.Cells["TripRate"].Activation = Activation.NoEdit;
                e.Row.Cells["TripAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["Stops"].Activation = Activation.NoEdit;
                e.Row.Cells["StopsRate"].Activation = Activation.NoEdit;
                e.Row.Cells["StopsAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["Cartons"].Activation = Activation.NoEdit;
                e.Row.Cells["CartonsRate"].Activation = Activation.NoEdit;
                e.Row.Cells["CartonsAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["Pallets"].Activation = Activation.NoEdit;
                e.Row.Cells["PalletsRate"].Activation = Activation.NoEdit;
                e.Row.Cells["PalletsAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["PickupCartons"].Activation = Activation.NoEdit;
                e.Row.Cells["PickupCartonsRate"].Activation = Activation.NoEdit;
                e.Row.Cells["PickupCartonsAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["MinimunAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["FSCMiles"].Activation = Activation.NoEdit;
                e.Row.Cells["FuelCost"].Activation = Activation.NoEdit;
                e.Row.Cells["FSCGal"].Activation = Activation.NoEdit;
                e.Row.Cells["FSCBaseRate"].Activation = Activation.NoEdit;
                e.Row.Cells["FSC"].Activation = Activation.NoEdit;
                e.Row.Cells["AdjustmentAmount1"].Activation = Activation.AllowEdit;
                e.Row.Cells["AdjustmentAmount1TypeID"].Activation = Convert.ToDecimal(e.Row.Cells["AdjustmentAmount1"].Value) != 0.0M ? Activation.AllowEdit : Activation.NoEdit;
                e.Row.Cells["AdjustmentAmount2"].Activation = Activation.AllowEdit;
                e.Row.Cells["AdjustmentAmount2TypeID"].Activation = Convert.ToDecimal(e.Row.Cells["AdjustmentAmount2"].Value) != 0.0M ? Activation.AllowEdit : Activation.NoEdit;
                e.Row.Cells["AdminCharge"].Activation = Activation.AllowEdit;
                e.Row.Cells["TotalAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["Imported"].Activation = Activation.NoEdit;
                e.Row.Cells["Exported"].Activation = Activation.NoEdit;
                e.Row.Cells["LastUpdated"].Activation = Activation.NoEdit;
                e.Row.Cells["UserID"].Activation = Activation.NoEdit;
            }
		}
        private void OnGridMouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event
            try {
                //Set menu and toolbar services
                UltraGrid grid = (UltraGrid)sender;
                grid.Focus();
                UIElement oUIElement = grid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X,e.Y));
                if(oUIElement != null) {
                    object oContext = oUIElement.GetContext(typeof(UltraGridRow));
                    if(oContext != null) {
                        if(e.Button == MouseButtons.Left) {
                            //OnDragDropMouseDown(sender, e);
                        }
                        else if(e.Button == MouseButtons.Right) {
                            UltraGridRow oRow = (UltraGridRow)oContext;
                            if(!oRow.Selected) grid.Selected.Rows.Clear();
                            oRow.Selected = true;
                            oRow.Activate();
                        }
                    }
                    else {
                        //Deselect rows in the white space of the grid or deactivate the active   
                        //row when in a scroll region to prevent double-click action
                        if(oUIElement.Parent.GetType() == typeof(DataAreaUIElement))
                            grid.Selected.Rows.Clear();
                        else if(oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement))
                            if(grid.Selected.Rows.Count > 0) grid.Selected.Rows[0].Activated = false;
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnGridKeyUp(object sender,System.Windows.Forms.KeyEventArgs e) {
            //Event handler for key up event
            if(e.KeyCode == System.Windows.Forms.Keys.Enter) {
                //Update row on Enter
                this.grdDriverRoutes.ActiveRow.Update();
                e.Handled = true;
            }
            else if(e.KeyCode == System.Windows.Forms.Keys.Delete) {
                this.ctxCDelete.PerformClick();
                e.Handled = true;
            }
        }
        #endregion
        #region Comp Grid Data Services: OnGridBeforeRowFilterDropDownPopulate(), OnGridBeforeExitEditMode(), OnGridAfterRowUpdate()
        private void OnGridBeforeRowFilterDropDownPopulate(object sender,Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs e) {
            //Removes only (Blanks) and Non Blanks default filter
            try {
                e.ValueList.ValueListItems.Remove(3);
                e.ValueList.ValueListItems.Remove(2);
                e.ValueList.ValueListItems.Remove(1);
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnGridBeforeExitEditMode(object sender,Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e) {
			//	
            try {
                this.grdDriverRoutes.ActiveCell.SetValue(this.grdDriverRoutes.ActiveCell.Text,true);
                this.mDriverComp.UpdateSummary(this.grdDriverRoutes.ActiveCell.Row.Cells["Operator"].Text);

                UltraGridCell cell = this.grdDriverRoutes.ActiveCell;
                if(cell.Column.Key == "AdjustmentAmount1") {
                    if(Convert.ToDecimal(cell.Value) == 0.0M) cell.Row.Cells["AdjustmentAmount1TypeID"].Value = "";
                    cell.Row.Cells["AdjustmentAmount1TypeID"].Activation = Convert.ToDecimal(cell.Value) != 0.0M ? Activation.AllowEdit : Activation.NoEdit;
                }
                if(cell.Column.Key == "AdjustmentAmount2") {
                    if(Convert.ToDecimal(cell.Value) == 0.0M) cell.Row.Cells["AdjustmentAmount2TypeID"].Value = "";
                    cell.Row.Cells["AdjustmentAmount2TypeID"].Activation = Convert.ToDecimal(cell.Value) != 0.0M ? Activation.AllowEdit : Activation.NoEdit;
                }
                if(cell.Column.Key == "AdjustmentAmount1TypeID" || cell.Column.Key == "AdjustmentAmount2TypeID") FinanceFactory.RefreshCache();
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
		}
        private void OnGridAfterRowUpdate(object sender,Infragistics.Win.UltraWinGrid.RowEventArgs e) {
            //Event handler for after row update event fires
            try {
                this.mDriverComp.Update();
            }
            catch(Exception ex) {
                e.Row.CancelUpdate();
                App.ReportError(ex,true,LogLevel.Error);
            }
        }
        #endregion
        #region Route Grid Services: OnGridRInitializeLayout(), OnGridRInitializeRow(), OnGridRMouseDown(), OnGridRBeforeRowFilterDropDownPopulate(), OnCloseRoutes()
        private void OnGridRInitializeLayout(object sender,Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
            //
            e.Layout.Bands["RoadshowRouteTable"].Columns["EquipmentID"].ValueList = this.uddEquipType;
        }
        private void OnGridRInitializeRow(object sender,Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e) {
            //
            e.Row.Cells["New"].Activation = Activation.AllowEdit;
            e.Row.Cells["Rt_Date"].Activation = Activation.NoEdit;
            e.Row.Cells["Rt_Name"].Activation = Activation.NoEdit;
            e.Row.Cells["Operator"].Activation = Activation.NoEdit;
            e.Row.Cells["VEHICLE_TYPE1"].Activation = Activation.NoEdit;
            e.Row.Cells["Payee"].Activation = Activation.NoEdit;
            e.Row.Cells["FinanceVendID"].Activation = Activation.NoEdit;
            e.Row.Cells["TtlMiles"].Activation = Activation.NoEdit;
            e.Row.Cells["MultiTrp"].Activation = Activation.NoEdit;
            e.Row.Cells["UniqueStops"].Activation = Activation.NoEdit;
            e.Row.Cells["DelCtns"].Activation = Activation.NoEdit;
            e.Row.Cells["RtnCtn"].Activation = Activation.NoEdit;
            e.Row.Cells["DelPltsorRcks"].Activation = Activation.NoEdit;
            e.Row.Cells["Depot"].Activation = Activation.NoEdit;
            e.Row.Cells["DepotNumber"].Activation = Activation.NoEdit;
            e.Row.Cells["EquipmentID"].Activation = Activation.NoEdit;
        }
        private void OnGridRMouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event
            try {
                //Set menu and toolbar services
                UltraGrid grid = (UltraGrid)sender;
                grid.Focus();
                UIElement oUIElement = grid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X,e.Y));
                if(oUIElement != null) {
                    object oContext = oUIElement.GetContext(typeof(UltraGridRow));
                    if(oContext != null) {
                        if(e.Button == MouseButtons.Left) {
                            //OnDragDropMouseDown(sender, e);
                        }
                        else if(e.Button == MouseButtons.Right) {
                            UltraGridRow oRow = (UltraGridRow)oContext;
                            if(!oRow.Selected) grid.Selected.Rows.Clear();
                            oRow.Selected = true;
                            oRow.Activate();
                        }
                    }
                    else {
                        //Deselect rows in the white space of the grid or deactivate the active   
                        //row when in a scroll region to prevent double-click action
                        if(oUIElement.Parent.GetType() == typeof(DataAreaUIElement))
                            grid.Selected.Rows.Clear();
                        else if(oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement))
                            if(grid.Selected.Rows.Count > 0) grid.Selected.Rows[0].Activated = false;
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnGridRBeforeRowFilterDropDownPopulate(object sender,Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs e) {
            //Removes only (Blanks) and Non Blanks default filter
            try {
                e.ValueList.ValueListItems.Remove(3);
                e.ValueList.ValueListItems.Remove(2);
                e.ValueList.ValueListItems.Remove(1);
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnCloseRoutes(object sender,System.EventArgs e) {
            //Event handler to close routes window
            RoadshowRoutesVisible = false;
            setUserServices();
        }
        #endregion
        #region User Services: OnItemClick(), OnAddEquipmentOverrideClick()
        private void OnItemClick(object sender, System.EventArgs e) {
			//Event handler for mneu item clicked
            UltraGridRow row = null;
            int index = 0;
			try {
                ToolStripItem menu = (ToolStripItem)sender;
                switch(menu.Name) {
					case "ctxCRefresh":
                        this.Cursor = Cursors.WaitCursor;
                        reportStatus(new StatusEventArgs("Refreshing driver routes..."));
                        this.mDriverComp.RefreshDriverRoutes();
                        break;
                    case "ctxCSaveAs":
                        #region Save
                        SaveFileDialog dlgSave = new SaveFileDialog();
                        dlgSave.AddExtension = true;
                        dlgSave.Filter = "Data Files (*.xml) | *.xml";
                        dlgSave.FilterIndex = 0;
                        dlgSave.Title = "Save Driver Compensation As...";
                        dlgSave.FileName = this.mDriverComp.Title;
                        dlgSave.OverwritePrompt = true;
                        if(dlgSave.ShowDialog(this) == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            Application.DoEvents();
                            this.mDriverComp.DriverRoutes.WriteXml(dlgSave.FileName,XmlWriteMode.WriteSchema);
                        }
                        #endregion
                        break;
                    case "ctxCExport":
                    case "ctxEExport": 
                        //Export this driver compensation to file
                        #region Export
                        SaveFileDialog dlgExport = new SaveFileDialog();
                        dlgExport.AddExtension = true;
                        dlgExport.Filter = "Export Files (*.txt) | *.txt";
                        dlgExport.FilterIndex = 0;
                        dlgExport.Title = "Export Driver Compensation To...";
                        dlgExport.FileName = this.mDriverComp.Title;
                        dlgExport.OverwritePrompt = true;
                        if(dlgExport.ShowDialog(this) == DialogResult.OK) {
                            //Validate file is unique
                            if(File.Exists(dlgExport.FileName))
                                throw new ApplicationException("Export file " + dlgExport.FileName + " already exists. ");

                            //Create the new file and save driver compensation to disk
                            this.Cursor = Cursors.WaitCursor;
                            Application.DoEvents();
                            StreamWriter writer = null;
                            try {
                                writer = new StreamWriter(new FileStream(dlgExport.FileName,FileMode.Create,FileAccess.ReadWrite));
                                writer.BaseStream.Seek(0,SeekOrigin.Begin);
                                writer.WriteLine(this.mDriverComp.Export(true));
                                writer.Flush();
                            }
                            catch(Exception ex) { throw ex; }
                            finally { if(writer != null) writer.Close(); this.Cursor = Cursors.Default; }
                        }
                        #endregion
                        break;
                    case "ctxCEquipOverride":     break;
                    case "ctxCPrint":
                        this.Cursor = Cursors.WaitCursor;
                        reportStatus(new StatusEventArgs("Printing this schedule..."));
                        string caption = "DRIVER COMPENSATION" + Environment.NewLine + this.mDriverComp.AgentName.Trim() + " : " + this.mDriverComp.StartDate.ToString("dd-MMM-yyyy") + "-" + this.mDriverComp.EndDate.ToString("dd-MMM-yyyy");
                        UltraGridPrinter.Print(this.grdDriverRoutes,caption,true);
                        break;
                    case "ctxCCut":
                        this.Cursor = Cursors.WaitCursor;
                        Clipboard.SetDataObject(this.grdDriverRoutes.ActiveCell.SelText,false);
						this.grdDriverRoutes.ActiveCell.Value = this.grdDriverRoutes.ActiveCell.Text.Remove(this.grdDriverRoutes.ActiveCell.SelStart, this.grdDriverRoutes.ActiveCell.SelLength);
						break;
                    case "ctxCCopy":
                        this.Cursor = Cursors.WaitCursor;
                        Clipboard.SetDataObject(this.grdDriverRoutes.ActiveCell.SelText,false);
						break;
                    case "ctxCPaste":
                        this.Cursor = Cursors.WaitCursor;
                        IDataObject o = Clipboard.GetDataObject();
						this.grdDriverRoutes.ActiveCell.Value = this.grdDriverRoutes.ActiveCell.Text.Remove(this.grdDriverRoutes.ActiveCell.SelStart, this.grdDriverRoutes.ActiveCell.SelLength).Insert(this.grdDriverRoutes.ActiveCell.SelStart, (string)o.GetData("Text"));
						break;
                    case "ctxCDelete":
                        this.Cursor = Cursors.WaitCursor;
                        row = this.grdDriverRoutes.Selected.Rows[0];
                        index = (row.HasParent()) ? row.ParentRow.Index : row.Index;
                        try {
                            if(this.grdDriverRoutes.Selected.Rows[0].Band.Key == "DriverCompTable") {
                                //Parent (route summary) band- delete all children and then the parent for 
                                //each selected parent
                                int parents = this.grdDriverRoutes.Selected.Rows.Count;
                                for(int k = parents; k > 0; k--) {
                                    UltraGridRow parent = this.grdDriverRoutes.Selected.Rows[k - 1];
                                    int kids = parent.ChildBands[0].Rows.Count;
                                    for(int i = kids; i > 0; i--) {
                                        parent.ChildBands[0].Rows[i - 1].Delete(false);
                                    }
                                    if(parent.HasChild())
                                        this.mDriverComp.UpdateSummary(parent.Cells["Operator"].Text);
                                    else
                                        parent.Delete(false);
                                }
                            }
                            else {
                                //Child (daily route) band- delete each selected route from the single parent;
                                //either re-calculate the (parent) summary or delete the parent if no child routes
                                UltraGridRow parent = this.grdDriverRoutes.Selected.Rows[0].ParentRow;
                                this.grdDriverRoutes.DeleteSelectedRows(false);
                                if(parent.HasChild())
                                    this.mDriverComp.UpdateSummary(parent.Cells["Operator"].Text);
                                else
                                    parent.Delete(false);
                            }
                            this.grdDriverRoutes.UpdateData();
                            this.mDriverComp.Update();
                            this.ctxRRefresh.PerformClick();
                        }
                        finally {
                            if(this.grdDriverRoutes.Rows.VisibleRowCount > 0) {
                                this.grdDriverRoutes.Rows[index].Activate();
                                this.grdDriverRoutes.Rows[index].Selected = true;
                                this.grdDriverRoutes.Rows[index].Expanded = true;
                            }
                        }
                        break;
                    case "ctxCExpandAll":       this.grdDriverRoutes.Rows.ExpandAll(true); break;
                    case "ctxCCollapseAll":     this.grdDriverRoutes.Rows.CollapseAll(true);  break;
                    case "ctxRRefresh":
                        this.Cursor = Cursors.WaitCursor;
                        reportStatus(new StatusEventArgs("Refreshing Roadshow routes..."));
                        this.mDriverComp.RefreshRoadshowRoutes();
                        break;
                    case "ctxRSelectAll":
                        this.Cursor = Cursors.WaitCursor;
                        for(int i = 0; i < this.grdRoadshowRoutes.Rows.Count; i++)
                            this.grdRoadshowRoutes.Rows[i].Cells["New"].Value = this.ctxRSelectAll.Checked;
                        break;
                    case "ctxRAddRoutes":
                        this.Cursor = Cursors.WaitCursor;
                        reportStatus(new StatusEventArgs("Adding Roadshow routes..."));
                        this.mDriverComp.ConvertRoadshowRoutes();
                        break;
                    case "ctxERefresh":
                        this.Cursor = Cursors.WaitCursor;
                        reportStatus(new StatusEventArgs("Refreshing export..."));
                        this.txtExport.Clear();
                        this.txtExport.Text = this.mDriverComp.Export(false);
                        break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnAddEquipmentOverrideClick(object sender,EventArgs e) {
            //Event handler for Add Equipment Override menu item clicked
            UltraGridRow row = this.grdDriverRoutes.Selected.Rows[0];
            int index = (row.HasParent()) ? row.ParentRow.Index : row.Index;
            try {
                //Create the override; if successful, delete and re-add the route
                string vendorID = row.Cells["FinanceVendorID"].Value.ToString();
                string operatorName = row.Cells["Operator"].Value.ToString();
                string routeDate = row.Cells["RouteDate"].Value.ToString();
                int equipID = Convert.ToInt32(((ToolStripItem)sender).Tag);
                if(FinanceFactory.CreateDriverEquipment(vendorID,operatorName,equipID)) {
                    this.ctxCDelete.PerformClick();
                    for(int i = 0; i < this.grdRoadshowRoutes.Rows.Count; i++) {
                        UltraGridRow r = this.grdRoadshowRoutes.Rows[i];
                        bool val = (r.Cells["FinanceVendID"].Value.ToString() == vendorID && r.Cells["Operator"].Value.ToString() == operatorName && r.Cells["Rt_Date"].Value.ToString() == routeDate);
                        this.grdRoadshowRoutes.Rows[i].Cells["New"].Value = val;
                    }
                    this.mDriverComp.ConvertRoadshowRoutes();
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally {
                this.grdDriverRoutes.Rows[index].Activate(); this.grdDriverRoutes.Rows[index].Selected = true; this.grdDriverRoutes.Rows[index].Expanded = true;
                setUserServices(); this.Cursor = Cursors.Default; 
            }
        }
        #endregion
		#region Local Services: setUserServices(), reportStatus()
		private void setUserServices() {
			//Set user services
			try {
                bool isDrivers = (this.tabDialog.SelectedTab == this.tabRoutes && this.grdDriverRoutes.Focused);
                bool isRoadshows = (this.tabDialog.SelectedTab == this.tabRoutes && this.grdRoadshowRoutes.Focused);
                bool hasCompensation = (this.mDriverComp.DriverRoutes.DriverCompTable.Rows.Count > 0);
                bool hasSelection = (this.grdDriverRoutes.Selected != null && this.grdDriverRoutes.Selected.Rows.Count > 0);
                bool isRoute = (hasSelection && this.grdDriverRoutes.Selected.Rows[0].ParentRow != null);
                bool isSummary = (this.tabDialog.SelectedTab == this.tabSummary);
                bool isPaystubs = (this.tabDialog.SelectedTab == this.tabDriverComp);
                bool isExports = (this.tabDialog.SelectedTab == this.tabExport);

                this.ctxCRefresh.Enabled = isDrivers;
                this.ctxCSaveAs.Enabled = isDrivers && hasCompensation && !hasSelection;
                this.ctxCExport.Enabled = !App.Config.ReadOnly && isDrivers && hasCompensation && !hasSelection;
                this.ctxCPrint.Enabled = (isDrivers && hasCompensation && !hasSelection) || isSummary || isPaystubs;
                this.ctxCEquipOverride.Enabled = !App.Config.ReadOnly && App.Config.Administrator && isDrivers && isRoute;
                this.ctxCCut.Enabled = !App.Config.ReadOnly && isDrivers && (this.grdDriverRoutes.ActiveCell != null && this.grdDriverRoutes.ActiveCell.IsInEditMode);
                this.ctxCCopy.Enabled = !App.Config.ReadOnly && isDrivers && (this.grdDriverRoutes.ActiveCell != null);
                this.ctxCPaste.Enabled = !App.Config.ReadOnly && isDrivers && (this.grdDriverRoutes.ActiveCell != null && this.grdDriverRoutes.ActiveCell.IsInEditMode && Clipboard.GetDataObject() != null);
                this.ctxCDelete.Enabled = !App.Config.ReadOnly && isDrivers && hasSelection;
                this.ctxCExpandAll.Enabled = true;
                this.ctxCCollapseAll.Enabled = true;
                this.ctxRRefresh.Enabled = isRoadshows;
                this.ctxRSelectAll.Enabled = isRoadshows;
                this.ctxRAddRoutes.Enabled = !App.Config.ReadOnly && isRoadshows;
                this.ctxERefresh.Enabled = isExports;
                this.ctxEExport.Enabled = !App.Config.ReadOnly && isExports;
            }
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Error); }
			finally { Application.DoEvents(); if(this.ServiceStatesChanged!=null) this.ServiceStatesChanged(this, new EventArgs()); }
		}
		private void reportStatus(StatusEventArgs e) { if(this.StatusMessage != null) this.StatusMessage(this, e); }
		#endregion
    }
}
