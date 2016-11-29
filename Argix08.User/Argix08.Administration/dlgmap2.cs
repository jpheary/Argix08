//	File:	dlgmap2.cs
//	Author:	J. Heary
//	Date:	12/19/03
//	Desc:	Dialog to edit an existing Delivery Map mappings by postal code.
//			mMapViewDS- view for mappings (map header excluded) 
//			mMapDS- map detail (header, mappings) for transactions to middle tier
//			mMappingsDS- bound to grdOverrides for edit of new\update\delete ;
//						  mappings; includes display of prior path\service values
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Tsort.Enterprise;

namespace Tsort {
	//	
	public class dlgMapDetail2 : System.Windows.Forms.Form {
		//Members
		private bool m_bIsDragging=false;
		#region Constants
		private const string CMD_EDIT = "&Edit";
		private const string CMD_DEL = "&Delete";
		private const string CMD_REM = "&Remove";
		private const string CMD_APPLY = "&Apply";
		private const string CMD_CLOSE = "&Close";

		private const string MNU_EDIT = "&Edit MapDS";
		private const string MNU_REMOVE = "&Remove MapDS";
		#endregion
		#region Controls

		private Tsort.Enterprise.MapDS mMapDS;
		private System.Windows.Forms.ComboBox cboCountry;
		private System.Windows.Forms.TextBox txtCode;
		private System.Windows.Forms.GroupBox fraMap;
		private System.Windows.Forms.Button btnEdit;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdMapView;
		private System.Windows.Forms.Button btnDel;
		private Tsort.Enterprise.MapDS mMappingsDS;
		private Tsort.Windows.SelectionList mTsortPathsDS;
		private Tsort.Windows.SelectionList mReturnPathsDS;
		private Tsort.Windows.SelectionList mTsortServicesDS;
		private Tsort.Windows.SelectionList mReturnServicesDS;
		private Tsort.Enterprise.CountryDS mCountriesDS;
		private System.Windows.Forms.Panel pnlMain;
		private System.Windows.Forms.GroupBox fraOverrides;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdOverride;
		private System.Windows.Forms.Button btnApply;
		private Infragistics.Win.UltraWinEditors.UltraComboEditor cboTsortPath;
		private Infragistics.Win.UltraWinEditors.UltraComboEditor cboReturnService;
		private Infragistics.Win.UltraWinEditors.UltraComboEditor cboReturnPath;
		private Infragistics.Win.UltraWinEditors.UltraComboEditor cboTsortService;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnRem;
		private System.Windows.Forms.Splitter splitterH;
		private System.Windows.Forms.ContextMenu ctxMap;
		private System.Windows.Forms.MenuItem ctxMapEdit;
		private System.Windows.Forms.MenuItem ctxMapDelete;
		private System.Windows.Forms.Label _lblCode;
		private System.Windows.Forms.Label _lblCountry;
		private System.Windows.Forms.CheckBox chkAll;
		private Tsort.Enterprise.MapDS mMapViewDS;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		
		//Events
		public event ErrorEventHandler ErrorMessage=null;
		
		//Interface
		public dlgMapDetail2() {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();				
				this.btnEdit.Text = CMD_EDIT;
				this.btnDel.Text = CMD_DEL;
				this.btnRem.Text = CMD_REM;
				this.btnApply.Text = CMD_APPLY;
				this.btnClose.Text = CMD_CLOSE;
				this.ctxMapEdit.Text = MNU_EDIT;
				this.ctxMapDelete.Text = MNU_REMOVE;
				
				//Set split window
				this.splitterH.MinExtra = 24;
				this.splitterH.MinSize = 48;
				this.fraMap.Dock = DockStyle.Fill;
				this.splitterH.Dock = DockStyle.Bottom;
				this.fraOverrides.Dock = DockStyle.Bottom;
				this.pnlMain.Controls.AddRange(new Control[]{this.splitterH, this.fraMap, this.fraOverrides});
				
				//Set title bar
				this.Text = "Postal Map"; 
			} 
			catch(Exception ex) { throw ex; }
		}
		protected override void Dispose( bool disposing ) { if( disposing ) { if (components != null) components.Dispose(); } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("PostalCodeMappingTable", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MapID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenterID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenter");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Number");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsActive");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CountryID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Country");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PostalCode");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StateOrProvince");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OldPathIDTsort");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OldTsortPathMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OldTsortPathLastStopMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OldServiceIDTsort");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OldTsortServiceMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OldPathIDReturns");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OldReturnPathMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OldReturnPathLastStopMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OldServiceIDReturns");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OldReturnServiceMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PathIDTsort");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TsortPathMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TsortPathLastStopMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ServiceIDTsort");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TsortServiceMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PathIDReturns");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReturnPathMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReturnPathLastStopMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ServiceIDReturns");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReturnServiceMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RowVersion");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RowAction");
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("PostalCodeMappingTable", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MapID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenterID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenter");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn41 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Number");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn43 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn44 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn45 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsActive");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn46 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CountryID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn47 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Country");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn48 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PostalCode");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn49 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StateOrProvince");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn50 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PathIDTsort");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn51 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TsortPathMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn52 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TsortPathLastStopMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn53 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ServiceIDTsort");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn54 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TsortServiceMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn55 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PathIDReturns");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn56 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReturnPathMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn57 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReturnPathLastStopMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn58 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ServiceIDReturns");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn59 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReturnServiceMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn60 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn61 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn62 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RowVersion");
			Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
			this.mMapDS = new Tsort.Enterprise.MapDS();
			this.mMappingsDS = new Tsort.Enterprise.MapDS();
			this.pnlMain = new System.Windows.Forms.Panel();
			this.splitterH = new System.Windows.Forms.Splitter();
			this.fraOverrides = new System.Windows.Forms.GroupBox();
			this.btnRem = new System.Windows.Forms.Button();
			this.grdOverride = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.cboTsortPath = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.cboReturnService = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.cboReturnPath = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.cboTsortService = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.fraMap = new System.Windows.Forms.GroupBox();
			this.btnDel = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.grdMapView = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.ctxMap = new System.Windows.Forms.ContextMenu();
			this.ctxMapEdit = new System.Windows.Forms.MenuItem();
			this.ctxMapDelete = new System.Windows.Forms.MenuItem();
			this.mMapViewDS = new Tsort.Enterprise.MapDS();
			this.chkAll = new System.Windows.Forms.CheckBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.cboCountry = new System.Windows.Forms.ComboBox();
			this.mCountriesDS = new Tsort.Enterprise.CountryDS();
			this._lblCode = new System.Windows.Forms.Label();
			this._lblCountry = new System.Windows.Forms.Label();
			this.txtCode = new System.Windows.Forms.TextBox();
			this.btnApply = new System.Windows.Forms.Button();
			this.mTsortPathsDS = new Tsort.Windows.SelectionList();
			this.mReturnPathsDS = new Tsort.Windows.SelectionList();
			this.mTsortServicesDS = new Tsort.Windows.SelectionList();
			this.mReturnServicesDS = new Tsort.Windows.SelectionList();
			((System.ComponentModel.ISupportInitialize)(this.mMapDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mMappingsDS)).BeginInit();
			this.pnlMain.SuspendLayout();
			this.fraOverrides.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdOverride)).BeginInit();
			this.fraMap.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdMapView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mMapViewDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mCountriesDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mTsortPathsDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mReturnPathsDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mTsortServicesDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mReturnServicesDS)).BeginInit();
			this.SuspendLayout();
			// 
			// mMapDS
			// 
			this.mMapDS.DataSetName = "MapDS";
			this.mMapDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// mMappingsDS
			// 
			this.mMappingsDS.DataSetName = "MapDS";
			this.mMappingsDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// pnlMain
			// 
			this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlMain.Controls.Add(this.splitterH);
			this.pnlMain.Controls.Add(this.fraOverrides);
			this.pnlMain.Controls.Add(this.fraMap);
			this.pnlMain.DockPadding.All = 3;
			this.pnlMain.Location = new System.Drawing.Point(0, 63);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(858, 360);
			this.pnlMain.TabIndex = 20;
			// 
			// splitterH
			// 
			this.splitterH.BackColor = System.Drawing.SystemColors.ControlText;
			this.splitterH.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitterH.Cursor = System.Windows.Forms.Cursors.HSplit;
			this.splitterH.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitterH.Location = new System.Drawing.Point(3, 186);
			this.splitterH.MinExtra = 12;
			this.splitterH.MinSize = 12;
			this.splitterH.Name = "splitterH";
			this.splitterH.Size = new System.Drawing.Size(852, 3);
			this.splitterH.TabIndex = 26;
			this.splitterH.TabStop = false;
			// 
			// fraOverrides
			// 
			this.fraOverrides.Controls.Add(this.btnRem);
			this.fraOverrides.Controls.Add(this.grdOverride);
			this.fraOverrides.Controls.Add(this.cboTsortPath);
			this.fraOverrides.Controls.Add(this.cboReturnService);
			this.fraOverrides.Controls.Add(this.cboReturnPath);
			this.fraOverrides.Controls.Add(this.cboTsortService);
			this.fraOverrides.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.fraOverrides.Location = new System.Drawing.Point(3, 189);
			this.fraOverrides.Name = "fraOverrides";
			this.fraOverrides.Size = new System.Drawing.Size(852, 168);
			this.fraOverrides.TabIndex = 25;
			this.fraOverrides.TabStop = false;
			this.fraOverrides.Text = "Updates\\DeliveryLocationDS";
			// 
			// btnRem
			// 
			this.btnRem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRem.BackColor = System.Drawing.SystemColors.Control;
			this.btnRem.Enabled = false;
			this.btnRem.Location = new System.Drawing.Point(750, 138);
			this.btnRem.Name = "btnRem";
			this.btnRem.Size = new System.Drawing.Size(96, 24);
			this.btnRem.TabIndex = 9;
			this.btnRem.Text = "&Remove";
			this.btnRem.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// grdOverride
			// 
			this.grdOverride.AllowDrop = true;
			this.grdOverride.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grdOverride.DataMember = "PostalCodeMappingTable";
			this.grdOverride.DataSource = this.mMappingsDS;
			this.grdOverride.DisplayLayout.AddNewBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			appearance1.BackColor = System.Drawing.Color.White;
			appearance1.FontData.Name = "Arial";
			appearance1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.grdOverride.DisplayLayout.Appearance = appearance1;
			ultraGridBand1.AddButtonCaption = "FreightAssignmentListForStationTable";
			ultraGridColumn1.Hidden = true;
			ultraGridColumn2.Hidden = true;
			ultraGridColumn3.Header.VisiblePosition = 3;
			ultraGridColumn3.Width = 144;
			ultraGridColumn4.Header.VisiblePosition = 23;
			ultraGridColumn4.Hidden = true;
			ultraGridColumn5.Header.Caption = "Client#";
			ultraGridColumn5.Header.VisiblePosition = 7;
			ultraGridColumn5.Width = 60;
			ultraGridColumn6.Header.Caption = "Client";
			ultraGridColumn6.Header.VisiblePosition = 8;
			ultraGridColumn6.Width = 144;
			ultraGridColumn7.Header.VisiblePosition = 9;
			ultraGridColumn7.Width = 96;
			ultraGridColumn8.Header.Caption = "Active";
			ultraGridColumn8.Header.VisiblePosition = 10;
			ultraGridColumn8.Width = 48;
			ultraGridColumn9.Header.Caption = "Country";
			ultraGridColumn9.Header.VisiblePosition = 2;
			ultraGridColumn9.Hidden = true;
			ultraGridColumn9.Width = 60;
			ultraGridColumn10.Header.VisiblePosition = 5;
			ultraGridColumn10.Hidden = true;
			ultraGridColumn11.Header.VisiblePosition = 11;
			ultraGridColumn11.Width = 96;
			ultraGridColumn12.Header.Caption = "State";
			ultraGridColumn12.Header.VisiblePosition = 12;
			ultraGridColumn12.Width = 48;
			ultraGridColumn13.Header.VisiblePosition = 6;
			ultraGridColumn13.Hidden = true;
			ultraGridColumn14.Header.Caption = "TsortPath";
			ultraGridColumn14.Width = 75;
			ultraGridColumn15.Header.VisiblePosition = 15;
			ultraGridColumn15.Hidden = true;
			ultraGridColumn16.Header.VisiblePosition = 4;
			ultraGridColumn16.Hidden = true;
			ultraGridColumn17.Header.Caption = "TsortSvc";
			ultraGridColumn17.Header.VisiblePosition = 17;
			ultraGridColumn17.Width = 75;
			ultraGridColumn18.Header.VisiblePosition = 28;
			ultraGridColumn18.Hidden = true;
			ultraGridColumn19.Header.Caption = "RetrnPath";
			ultraGridColumn19.Header.VisiblePosition = 21;
			ultraGridColumn19.Width = 75;
			ultraGridColumn20.Header.VisiblePosition = 29;
			ultraGridColumn20.Hidden = true;
			ultraGridColumn21.Header.VisiblePosition = 16;
			ultraGridColumn21.Hidden = true;
			ultraGridColumn22.Header.Caption = "RetrnSvc";
			ultraGridColumn22.Header.VisiblePosition = 26;
			ultraGridColumn22.Width = 75;
			ultraGridColumn23.Header.Caption = "_TsortPath";
			ultraGridColumn23.Header.VisiblePosition = 14;
			ultraGridColumn23.Width = 75;
			ultraGridColumn24.Header.VisiblePosition = 19;
			ultraGridColumn24.Hidden = true;
			ultraGridColumn25.Header.VisiblePosition = 20;
			ultraGridColumn25.Hidden = true;
			ultraGridColumn26.Header.Caption = "_TsortSvc";
			ultraGridColumn26.Header.VisiblePosition = 18;
			ultraGridColumn26.Width = 75;
			ultraGridColumn27.Header.VisiblePosition = 30;
			ultraGridColumn27.Hidden = true;
			ultraGridColumn28.Header.Caption = "_RetrnPath";
			ultraGridColumn28.Header.VisiblePosition = 22;
			ultraGridColumn28.Width = 75;
			ultraGridColumn29.Header.VisiblePosition = 31;
			ultraGridColumn29.Hidden = true;
			ultraGridColumn30.Header.VisiblePosition = 25;
			ultraGridColumn30.Hidden = true;
			ultraGridColumn31.Header.Caption = "_RetrnSvc";
			ultraGridColumn31.Header.VisiblePosition = 27;
			ultraGridColumn31.Width = 75;
			ultraGridColumn32.Header.VisiblePosition = 24;
			ultraGridColumn32.Hidden = true;
			ultraGridColumn33.Header.VisiblePosition = 36;
			ultraGridColumn33.Hidden = true;
			ultraGridColumn34.Header.VisiblePosition = 35;
			ultraGridColumn34.Hidden = true;
			ultraGridColumn35.Hidden = true;
			ultraGridColumn36.Header.VisiblePosition = 33;
			ultraGridColumn36.Width = 60;
			ultraGridColumn37.Header.VisiblePosition = 32;
			ultraGridColumn37.Hidden = true;
			ultraGridBand1.Columns.Add(ultraGridColumn1);
			ultraGridBand1.Columns.Add(ultraGridColumn2);
			ultraGridBand1.Columns.Add(ultraGridColumn3);
			ultraGridBand1.Columns.Add(ultraGridColumn4);
			ultraGridBand1.Columns.Add(ultraGridColumn5);
			ultraGridBand1.Columns.Add(ultraGridColumn6);
			ultraGridBand1.Columns.Add(ultraGridColumn7);
			ultraGridBand1.Columns.Add(ultraGridColumn8);
			ultraGridBand1.Columns.Add(ultraGridColumn9);
			ultraGridBand1.Columns.Add(ultraGridColumn10);
			ultraGridBand1.Columns.Add(ultraGridColumn11);
			ultraGridBand1.Columns.Add(ultraGridColumn12);
			ultraGridBand1.Columns.Add(ultraGridColumn13);
			ultraGridBand1.Columns.Add(ultraGridColumn14);
			ultraGridBand1.Columns.Add(ultraGridColumn15);
			ultraGridBand1.Columns.Add(ultraGridColumn16);
			ultraGridBand1.Columns.Add(ultraGridColumn17);
			ultraGridBand1.Columns.Add(ultraGridColumn18);
			ultraGridBand1.Columns.Add(ultraGridColumn19);
			ultraGridBand1.Columns.Add(ultraGridColumn20);
			ultraGridBand1.Columns.Add(ultraGridColumn21);
			ultraGridBand1.Columns.Add(ultraGridColumn22);
			ultraGridBand1.Columns.Add(ultraGridColumn23);
			ultraGridBand1.Columns.Add(ultraGridColumn24);
			ultraGridBand1.Columns.Add(ultraGridColumn25);
			ultraGridBand1.Columns.Add(ultraGridColumn26);
			ultraGridBand1.Columns.Add(ultraGridColumn27);
			ultraGridBand1.Columns.Add(ultraGridColumn28);
			ultraGridBand1.Columns.Add(ultraGridColumn29);
			ultraGridBand1.Columns.Add(ultraGridColumn30);
			ultraGridBand1.Columns.Add(ultraGridColumn31);
			ultraGridBand1.Columns.Add(ultraGridColumn32);
			ultraGridBand1.Columns.Add(ultraGridColumn33);
			ultraGridBand1.Columns.Add(ultraGridColumn34);
			ultraGridBand1.Columns.Add(ultraGridColumn35);
			ultraGridBand1.Columns.Add(ultraGridColumn36);
			ultraGridBand1.Columns.Add(ultraGridColumn37);
			appearance2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(220)), ((System.Byte)(255)), ((System.Byte)(200)));
			appearance2.FontData.Name = "Verdana";
			appearance2.FontData.SizeInPoints = 8F;
			appearance2.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridBand1.Override.ActiveRowAppearance = appearance2;
			appearance3.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
			appearance3.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
			appearance3.FontData.Name = "Verdana";
			appearance3.FontData.SizeInPoints = 8F;
			appearance3.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance3.TextHAlign = Infragistics.Win.HAlign.Left;
			ultraGridBand1.Override.HeaderAppearance = appearance3;
			appearance4.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(240)), ((System.Byte)(240)), ((System.Byte)(255)));
			appearance4.FontData.Name = "Verdana";
			appearance4.FontData.SizeInPoints = 8F;
			appearance4.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridBand1.Override.RowAlternateAppearance = appearance4;
			appearance5.BackColor = System.Drawing.Color.White;
			appearance5.FontData.Name = "Verdana";
			appearance5.FontData.SizeInPoints = 8F;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance5.TextHAlign = Infragistics.Win.HAlign.Left;
			ultraGridBand1.Override.RowAppearance = appearance5;
			this.grdOverride.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
			appearance6.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(128)), ((System.Byte)(255)));
			appearance6.FontData.Name = "Verdana";
			appearance6.FontData.SizeInPoints = 8F;
			appearance6.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance6.TextHAlign = Infragistics.Win.HAlign.Left;
			this.grdOverride.DisplayLayout.CaptionAppearance = appearance6;
			this.grdOverride.DisplayLayout.GroupByBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			this.grdOverride.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			this.grdOverride.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
			this.grdOverride.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
			this.grdOverride.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None;
			this.grdOverride.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.None;
			this.grdOverride.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
			this.grdOverride.DisplayLayout.Override.CellMultiLine = Infragistics.Win.DefaultableBoolean.False;
			this.grdOverride.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
			this.grdOverride.DisplayLayout.Override.MaxSelectedCells = 1;
			this.grdOverride.DisplayLayout.Override.MaxSelectedRows = 1;
			this.grdOverride.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			this.grdOverride.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
			this.grdOverride.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
			this.grdOverride.Location = new System.Drawing.Point(6, 22);
			this.grdOverride.Name = "grdOverride";
			this.grdOverride.Size = new System.Drawing.Size(840, 107);
			this.grdOverride.TabIndex = 3;
			this.grdOverride.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnCellChange;
			this.grdOverride.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnMappingSelectionChanged);
			this.grdOverride.AfterRowUpdate += new Infragistics.Win.UltraWinGrid.RowEventHandler(this.OnMappingRowUpdated);
			this.grdOverride.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.OnMappingCellChanged);
			this.grdOverride.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnMappingDragDrop);
			this.grdOverride.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnMappingDragEnter);
			this.grdOverride.BeforeCellActivate += new Infragistics.Win.UltraWinGrid.CancelableCellEventHandler(this.OnBeforeMappingCellActivated);
			this.grdOverride.DragOver += new System.Windows.Forms.DragEventHandler(this.OnMappingDragOver);
			// 
			// cboTsortPath
			// 
			this.cboTsortPath.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboTsortPath.Location = new System.Drawing.Point(264, 15);
			this.cboTsortPath.Name = "cboTsortPath";
			this.cboTsortPath.Size = new System.Drawing.Size(69, 22);
			this.cboTsortPath.TabIndex = 4;
			this.cboTsortPath.Text = null;
			this.cboTsortPath.Visible = false;
			// 
			// cboReturnService
			// 
			this.cboReturnService.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboReturnService.Location = new System.Drawing.Point(705, 15);
			this.cboReturnService.Name = "cboReturnService";
			this.cboReturnService.Size = new System.Drawing.Size(69, 22);
			this.cboReturnService.TabIndex = 7;
			this.cboReturnService.Text = null;
			this.cboReturnService.Visible = false;
			// 
			// cboReturnPath
			// 
			this.cboReturnPath.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboReturnPath.Location = new System.Drawing.Point(558, 15);
			this.cboReturnPath.Name = "cboReturnPath";
			this.cboReturnPath.Size = new System.Drawing.Size(69, 22);
			this.cboReturnPath.TabIndex = 6;
			this.cboReturnPath.Text = null;
			this.cboReturnPath.Visible = false;
			// 
			// cboTsortService
			// 
			this.cboTsortService.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboTsortService.Location = new System.Drawing.Point(411, 15);
			this.cboTsortService.Name = "cboTsortService";
			this.cboTsortService.Size = new System.Drawing.Size(69, 22);
			this.cboTsortService.TabIndex = 5;
			this.cboTsortService.Text = null;
			this.cboTsortService.Visible = false;
			// 
			// fraMap
			// 
			this.fraMap.Controls.Add(this.btnDel);
			this.fraMap.Controls.Add(this.btnEdit);
			this.fraMap.Controls.Add(this.grdMapView);
			this.fraMap.Controls.Add(this.chkAll);
			this.fraMap.Dock = System.Windows.Forms.DockStyle.Top;
			this.fraMap.Location = new System.Drawing.Point(3, 3);
			this.fraMap.Name = "fraMap";
			this.fraMap.Size = new System.Drawing.Size(852, 168);
			this.fraMap.TabIndex = 18;
			this.fraMap.TabStop = false;
			this.fraMap.Text = "Mappings";
			// 
			// btnDel
			// 
			this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDel.BackColor = System.Drawing.SystemColors.Control;
			this.btnDel.Enabled = false;
			this.btnDel.Location = new System.Drawing.Point(747, 64);
			this.btnDel.Name = "btnDel";
			this.btnDel.Size = new System.Drawing.Size(96, 24);
			this.btnDel.TabIndex = 5;
			this.btnDel.Text = "&Delete";
			this.btnDel.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// btnEdit
			// 
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEdit.BackColor = System.Drawing.SystemColors.Control;
			this.btnEdit.Enabled = false;
			this.btnEdit.Location = new System.Drawing.Point(747, 34);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(96, 24);
			this.btnEdit.TabIndex = 4;
			this.btnEdit.Text = "&Edit";
			this.btnEdit.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// grdMapView
			// 
			this.grdMapView.AllowDrop = true;
			this.grdMapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grdMapView.ContextMenu = this.ctxMap;
			this.grdMapView.DataMember = "PostalCodeMappingTable";
			this.grdMapView.DataSource = this.mMapViewDS;
			this.grdMapView.DisplayLayout.AddNewBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			appearance7.BackColor = System.Drawing.Color.White;
			appearance7.FontData.Name = "Arial";
			appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
			this.grdMapView.DisplayLayout.Appearance = appearance7;
			ultraGridBand2.AddButtonCaption = "FreightAssignmentListForStationTable";
			ultraGridColumn38.Hidden = true;
			ultraGridColumn39.Header.VisiblePosition = 3;
			ultraGridColumn39.Hidden = true;
			ultraGridColumn40.Width = 144;
			ultraGridColumn41.Header.VisiblePosition = 19;
			ultraGridColumn41.Hidden = true;
			ultraGridColumn42.Header.Caption = "Client#";
			ultraGridColumn42.Width = 60;
			ultraGridColumn43.Header.Caption = "Client";
			ultraGridColumn43.Width = 144;
			ultraGridColumn44.Header.VisiblePosition = 7;
			ultraGridColumn44.Width = 96;
			ultraGridColumn45.Header.Caption = "Active";
			ultraGridColumn45.Header.VisiblePosition = 8;
			ultraGridColumn45.Width = 48;
			ultraGridColumn46.Header.VisiblePosition = 1;
			ultraGridColumn46.Hidden = true;
			ultraGridColumn47.Header.VisiblePosition = 12;
			ultraGridColumn47.Hidden = true;
			ultraGridColumn47.Width = 96;
			ultraGridColumn48.Header.VisiblePosition = 9;
			ultraGridColumn48.Width = 96;
			ultraGridColumn49.Header.Caption = "State";
			ultraGridColumn49.Header.VisiblePosition = 10;
			ultraGridColumn49.Width = 60;
			ultraGridColumn50.Header.VisiblePosition = 6;
			ultraGridColumn50.Hidden = true;
			ultraGridColumn51.Header.Caption = "TsortPath";
			ultraGridColumn51.Header.VisiblePosition = 11;
			ultraGridColumn51.Width = 72;
			ultraGridColumn52.Header.VisiblePosition = 23;
			ultraGridColumn52.Hidden = true;
			ultraGridColumn53.Header.VisiblePosition = 13;
			ultraGridColumn53.Hidden = true;
			ultraGridColumn54.Header.Caption = "TsortSvc";
			ultraGridColumn54.Header.VisiblePosition = 14;
			ultraGridColumn54.Width = 72;
			ultraGridColumn55.Header.VisiblePosition = 15;
			ultraGridColumn55.Hidden = true;
			ultraGridColumn56.Header.Caption = "RetrnPath";
			ultraGridColumn56.Header.VisiblePosition = 17;
			ultraGridColumn56.Width = 72;
			ultraGridColumn57.Header.VisiblePosition = 21;
			ultraGridColumn57.Hidden = true;
			ultraGridColumn58.Hidden = true;
			ultraGridColumn59.Header.Caption = "RetrnSvc";
			ultraGridColumn59.Header.VisiblePosition = 18;
			ultraGridColumn59.Width = 72;
			ultraGridColumn60.Header.VisiblePosition = 16;
			ultraGridColumn60.Hidden = true;
			ultraGridColumn61.Header.VisiblePosition = 24;
			ultraGridColumn61.Hidden = true;
			ultraGridColumn62.Header.VisiblePosition = 22;
			ultraGridColumn62.Hidden = true;
			ultraGridBand2.Columns.Add(ultraGridColumn38);
			ultraGridBand2.Columns.Add(ultraGridColumn39);
			ultraGridBand2.Columns.Add(ultraGridColumn40);
			ultraGridBand2.Columns.Add(ultraGridColumn41);
			ultraGridBand2.Columns.Add(ultraGridColumn42);
			ultraGridBand2.Columns.Add(ultraGridColumn43);
			ultraGridBand2.Columns.Add(ultraGridColumn44);
			ultraGridBand2.Columns.Add(ultraGridColumn45);
			ultraGridBand2.Columns.Add(ultraGridColumn46);
			ultraGridBand2.Columns.Add(ultraGridColumn47);
			ultraGridBand2.Columns.Add(ultraGridColumn48);
			ultraGridBand2.Columns.Add(ultraGridColumn49);
			ultraGridBand2.Columns.Add(ultraGridColumn50);
			ultraGridBand2.Columns.Add(ultraGridColumn51);
			ultraGridBand2.Columns.Add(ultraGridColumn52);
			ultraGridBand2.Columns.Add(ultraGridColumn53);
			ultraGridBand2.Columns.Add(ultraGridColumn54);
			ultraGridBand2.Columns.Add(ultraGridColumn55);
			ultraGridBand2.Columns.Add(ultraGridColumn56);
			ultraGridBand2.Columns.Add(ultraGridColumn57);
			ultraGridBand2.Columns.Add(ultraGridColumn58);
			ultraGridBand2.Columns.Add(ultraGridColumn59);
			ultraGridBand2.Columns.Add(ultraGridColumn60);
			ultraGridBand2.Columns.Add(ultraGridColumn61);
			ultraGridBand2.Columns.Add(ultraGridColumn62);
			appearance8.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(220)), ((System.Byte)(255)), ((System.Byte)(200)));
			appearance8.FontData.Name = "Verdana";
			appearance8.FontData.SizeInPoints = 8F;
			appearance8.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridBand2.Override.ActiveRowAppearance = appearance8;
			appearance9.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
			appearance9.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
			appearance9.FontData.Name = "Verdana";
			appearance9.FontData.SizeInPoints = 8F;
			appearance9.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance9.TextHAlign = Infragistics.Win.HAlign.Left;
			ultraGridBand2.Override.HeaderAppearance = appearance9;
			appearance10.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(240)), ((System.Byte)(240)), ((System.Byte)(255)));
			appearance10.FontData.Name = "Verdana";
			appearance10.FontData.SizeInPoints = 8F;
			appearance10.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridBand2.Override.RowAlternateAppearance = appearance10;
			appearance11.BackColor = System.Drawing.Color.White;
			appearance11.FontData.Name = "Verdana";
			appearance11.FontData.SizeInPoints = 8F;
			appearance11.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance11.TextHAlign = Infragistics.Win.HAlign.Left;
			ultraGridBand2.Override.RowAppearance = appearance11;
			this.grdMapView.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
			appearance12.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(128)), ((System.Byte)(255)));
			appearance12.FontData.Name = "Verdana";
			appearance12.FontData.SizeInPoints = 8F;
			appearance12.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance12.TextHAlign = Infragistics.Win.HAlign.Left;
			this.grdMapView.DisplayLayout.CaptionAppearance = appearance12;
			this.grdMapView.DisplayLayout.GroupByBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			this.grdMapView.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			this.grdMapView.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
			this.grdMapView.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
			this.grdMapView.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None;
			this.grdMapView.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.None;
			this.grdMapView.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
			this.grdMapView.DisplayLayout.Override.CellMultiLine = Infragistics.Win.DefaultableBoolean.False;
			this.grdMapView.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
			this.grdMapView.DisplayLayout.Override.MaxSelectedCells = 1;
			this.grdMapView.DisplayLayout.Override.MaxSelectedRows = 1;
			this.grdMapView.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			this.grdMapView.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
			this.grdMapView.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
			this.grdMapView.Location = new System.Drawing.Point(6, 34);
			this.grdMapView.Name = "grdMapView";
			this.grdMapView.Size = new System.Drawing.Size(732, 125);
			this.grdMapView.TabIndex = 0;
			this.grdMapView.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnRowChange;
			this.grdMapView.SelectionDrag += new System.ComponentModel.CancelEventHandler(this.OnMapSelectionDrag);
			this.grdMapView.DoubleClick += new System.EventHandler(this.OnMapDoubleClicked);
			this.grdMapView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMapMouseDown);
			this.grdMapView.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnMapSelectionChanged);
			this.grdMapView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMapMouseUp);
			this.grdMapView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMapMouseMove);
			this.grdMapView.DragLeave += new System.EventHandler(this.OnMapDragLeave);
			// 
			// ctxMap
			// 
			this.ctxMap.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.ctxMapEdit,
																				   this.ctxMapDelete});
			// 
			// ctxMapEdit
			// 
			this.ctxMapEdit.Index = 0;
			this.ctxMapEdit.Text = "&Edit";
			this.ctxMapEdit.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxMapDelete
			// 
			this.ctxMapDelete.Index = 1;
			this.ctxMapDelete.Text = "&Delete";
			this.ctxMapDelete.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mMapViewDS
			// 
			this.mMapViewDS.DataSetName = "MapDS";
			this.mMapViewDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// chkAll
			// 
			this.chkAll.Location = new System.Drawing.Point(660, 12);
			this.chkAll.Name = "chkAll";
			this.chkAll.Size = new System.Drawing.Size(75, 18);
			this.chkAll.TabIndex = 27;
			this.chkAll.Text = "Show All";
			this.chkAll.CheckStateChanged += new System.EventHandler(this.OnShowAllChecked);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.BackColor = System.Drawing.SystemColors.Control;
			this.btnClose.Location = new System.Drawing.Point(759, 427);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(96, 24);
			this.btnClose.TabIndex = 26;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// cboCountry
			// 
			this.cboCountry.DataSource = this.mCountriesDS;
			this.cboCountry.DisplayMember = "CountryDetailTable.Country";
			this.cboCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCountry.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboCountry.Location = new System.Drawing.Point(111, 9);
			this.cboCountry.Name = "cboCountry";
			this.cboCountry.Size = new System.Drawing.Size(174, 21);
			this.cboCountry.TabIndex = 24;
			this.cboCountry.ValueMember = "CountryDetailTable.CountryID";
			this.cboCountry.SelectionChangeCommitted += new System.EventHandler(this.OnCountryChanged);
			// 
			// mCountriesDS
			// 
			this.mCountriesDS.DataSetName = "CountryDS";
			this.mCountriesDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// _lblCode
			// 
			this._lblCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblCode.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblCode.Location = new System.Drawing.Point(12, 36);
			this._lblCode.Name = "_lblCode";
			this._lblCode.Size = new System.Drawing.Size(96, 18);
			this._lblCode.TabIndex = 22;
			this._lblCode.Text = "Postal Code";
			this._lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblCountry
			// 
			this._lblCountry.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblCountry.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblCountry.Location = new System.Drawing.Point(9, 9);
			this._lblCountry.Name = "_lblCountry";
			this._lblCountry.Size = new System.Drawing.Size(96, 18);
			this._lblCountry.TabIndex = 23;
			this._lblCountry.Text = "Country";
			this._lblCountry.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtCode
			// 
			this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtCode.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtCode.Location = new System.Drawing.Point(111, 36);
			this.txtCode.Name = "txtCode";
			this.txtCode.Size = new System.Drawing.Size(72, 21);
			this.txtCode.TabIndex = 17;
			this.txtCode.Text = "";
			this.txtCode.TextChanged += new System.EventHandler(this.OnCodeChanged);
			// 
			// btnApply
			// 
			this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnApply.BackColor = System.Drawing.SystemColors.Control;
			this.btnApply.Enabled = false;
			this.btnApply.Location = new System.Drawing.Point(657, 427);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(96, 24);
			this.btnApply.TabIndex = 2;
			this.btnApply.Text = "&Apply";
			this.btnApply.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// mTsortPathsDS
			// 
			this.mTsortPathsDS.DataSetName = "SelectionList";
			this.mTsortPathsDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// mReturnPathsDS
			// 
			this.mReturnPathsDS.DataSetName = "SelectionList";
			this.mReturnPathsDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// mTsortServicesDS
			// 
			this.mTsortServicesDS.DataSetName = "SelectionList";
			this.mTsortServicesDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// mReturnServicesDS
			// 
			this.mReturnServicesDS.DataSetName = "SelectionList";
			this.mReturnServicesDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// dlgMapDetail2
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(858, 455);
			this.Controls.Add(this.pnlMain);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this._lblCode);
			this.Controls.Add(this._lblCountry);
			this.Controls.Add(this.txtCode);
			this.Controls.Add(this.cboCountry);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgMapDetail2";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Map Details";
			this.Load += new System.EventHandler(this.OnFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.mMapDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mMappingsDS)).EndInit();
			this.pnlMain.ResumeLayout(false);
			this.fraOverrides.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdOverride)).EndInit();
			this.fraMap.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdMapView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mMapViewDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mCountriesDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mTsortPathsDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mReturnPathsDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mTsortServicesDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mReturnServicesDS)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void OnFormLoad(object sender, System.EventArgs e) {
			//Initialize controls - set default values
			this.Cursor = Cursors.WaitCursor;
			try {
				//Set initial service states
				this.Visible = true;
				Application.DoEvents();
				
				//Get selection lists
				this.mCountriesDS.Merge(EnterpriseFactory.GetCountries());
				this.mTsortServicesDS.Merge(EnterpriseFactory.GetRegularOutboundServiceTypes());
				this.mReturnServicesDS.Merge(EnterpriseFactory.GetReturnOutboundServiceTypes());
				
				//Load control data	from detail dataset mMapDS		
				#region Default grid behavior
				this.grdMapView.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdMapView.DisplayLayout.Bands[0].Columns["PostalCode"].SortIndicator = SortIndicator.Ascending;
				this.grdOverride.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
				this.grdOverride.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.True;
				this.grdOverride.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.Edit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["SortCenter"].CellActivation = Activation.NoEdit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["ClientName"].CellActivation = Activation.NoEdit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["PostalCode"].CellActivation = Activation.NoEdit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["OldTsortPathMnemonic"].CellActivation = Activation.NoEdit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["OldTsortServiceMnemonic"].CellActivation = Activation.NoEdit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["OldReturnPathMnemonic"].CellActivation = Activation.NoEdit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["OldReturnServiceMnemonic"].CellActivation = Activation.NoEdit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["PathIDTsort"].CellActivation = Activation.AllowEdit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["ServiceIDTsort"].CellActivation = Activation.AllowEdit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["PathIDReturns"].CellActivation = Activation.AllowEdit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["ServiceIDReturns"].CellActivation = Activation.AllowEdit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["PathIDTsort"].EditorControl = this.cboTsortPath;
				this.grdOverride.DisplayLayout.Bands[0].Columns["ServiceIDTsort"].EditorControl = this.cboTsortService;
				this.grdOverride.DisplayLayout.Bands[0].Columns["PathIDReturns"].EditorControl = this.cboReturnPath;
				this.grdOverride.DisplayLayout.Bands[0].Columns["ServiceIDReturns"].EditorControl = this.cboReturnService;
				//this.grdOverride.DisplayLayout.Bands[0].Columns["PostalCode"].SortIndicator = SortIndicator.Ascending;
				#endregion
				if(this.cboCountry.Items.Count>0) this.cboCountry.SelectedIndex = 1;
				this.cboCountry.Enabled = true;
				OnCountryChanged(null, null);
				this.txtCode.Text = "";
				this.chkAll.Checked = true;
				this.chkAll.Checked = false;
				
				//View mappings in grdMapView; edit mappings in grdOverride (initial records: none)
				this.fraMap.Height = 168;
				this.fraOverrides.Top = 190;
				this.fraOverrides.Height = 168;
				if(this.grdMapView.Rows.Count>0) this.grdMapView.ActiveRow = this.grdMapView.Rows[0];
				if(this.grdOverride.Rows.Count>0) this.grdOverride.ActiveRow = this.grdOverride.Rows[0];
				
				#region UltraComboEditor combo boxes don't support binding- populate manually from dataset
				this.cboTsortService.Items.Clear();
				for(int i=0; i<this.mTsortServicesDS.SelectionListTable.Rows.Count; i++) 
					this.cboTsortService.Items.Add(this.mTsortServicesDS.SelectionListTable[i].ID, this.mTsortServicesDS.SelectionListTable[i].Description);
				this.cboTsortService.Enabled = (this.cboTsortService.Items.Count>0);
				
				this.cboReturnService.Items.Clear();
				for(int i=0; i<this.mReturnServicesDS.SelectionListTable.Rows.Count; i++) 
					this.cboReturnService.Items.Add(this.mReturnServicesDS.SelectionListTable[i].ID, this.mReturnServicesDS.SelectionListTable[i].Description);
				this.cboReturnService.Enabled = (this.cboReturnService.Items.Count>0);
				#endregion
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.btnApply.Enabled = false; this.Cursor = Cursors.Default; }
		}
		private void OnCountryChanged(object sender, System.EventArgs e) {
			//Event handler for change in selected sort center
			try {
				//Set postal code length limit
				this.txtCode.MaxLength = (this.cboCountry.Text=="USA") ? 5 : 10;
				
				//Get delivery mappings
				OnCodeChanged(null, null);
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnCodeChanged(object sender, System.EventArgs e) {
			//Event handler for change in selected postal code
			try {
				//Get delivery mappings for 3\5 position postal codes
				this.mMapDS.Clear();
				this.mMappingsDS.Clear();
				this.mMapViewDS.Clear();
				if(((this.cboCountry.Text=="USA") && (this.txtCode.Text.Length==3 || this.txtCode.Text.Length==5)) || ((this.cboCountry.Text!="USA") && (this.txtCode.Text.Length>0))) {
					this.mMapViewDS.Merge(EnterpriseFactory.GetPostalCodeMap(Convert.ToInt32(this.cboCountry.SelectedValue), this.txtCode.Text)); 
					this.Text = (this.mMapViewDS.PostalCodeMappingTable.Count>0) ? "Postal Map" : "Postal Map (Data Unavailable)";
				}
				this.grdMapView.Rows.Refresh(RefreshRow.ReloadData, true);
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnShowAllChecked(object sender, System.EventArgs e) {
			//Event handler for change in show all status
			try {
				//Filter results
				this.grdMapView.DisplayLayout.Bands[0].ColumnFilters["TsortPathMnemonic"].FilterConditions.Clear();
				if(!this.chkAll.Checked)
					this.grdMapView.DisplayLayout.Bands[0].ColumnFilters["TsortPathMnemonic"].FilterConditions.Add(FilterComparisionOperator.NotEquals, DBNull.Value);
				this.grdMapView.DisplayLayout.RefreshFilters();
			} 
			catch(Exception ex) { reportError(ex); }
		}
		#region MapDS Grid Behavior
		private void OnMapSelectionChanged(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for mapping cell activated
			//Debug.Write("OnMapSelectionChanged()\n");
			string sMapID="", sPostalCode="", sClient="";
			DataRow[] rowExists=null;
			try {
				//Set service states
				sMapID = this.grdMapView.Selected.Rows[0].Cells["MapID"].Value.ToString();
				sPostalCode = this.grdMapView.Selected.Rows[0].Cells["PostalCode"].Value.ToString();
				sClient = this.grdMapView.Selected.Rows[0].Cells["ClientName"].Value.ToString();
				rowExists = this.mMappingsDS.PostalCodeMappingTable.Select("MapID='" + sMapID + "' AND PostalCode='" + sPostalCode + "'");
				if(rowExists.Length==0) {
					//Edit an existing mapping: 3\5 position terminal\client mappings
					this.btnEdit.Enabled = (((this.cboCountry.Text=="USA") && (this.txtCode.Text.Length==3 || this.txtCode.Text.Length==5)) || ((this.cboCountry.Text!="USA") && (this.txtCode.Text.Length>0)));
					
					//Delete an existing mapping: only 5 position terminal; 3\5 position client mappings
					if(sClient!="")
						this.btnDel.Enabled = (((this.cboCountry.Text=="USA") && (this.txtCode.Text.Length==3 || this.txtCode.Text.Length==5)) || ((this.cboCountry.Text!="USA") && (this.txtCode.Text.Length>0)));
					else
						this.btnDel.Enabled = (((this.cboCountry.Text=="USA") && (this.txtCode.Text.Length==5)) || ((this.cboCountry.Text!="USA") && (this.txtCode.Text.Length>0)));
				}
				else 
					this.btnEdit.Enabled = this.btnDel.Enabled = false;
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnMapDoubleClicked(object sender, System.EventArgs e) {
			//Event handler for double-clicking a mapping
			try {
				//Edit the selected record
				if(this.btnEdit.Enabled) OnCmdClick(this.btnEdit, null);
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnMapMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Event handler for ...
			this.m_bIsDragging = (e.Button==MouseButtons.Left);
		}
		private void OnMapMouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Event handler for ...
			DataObject oData=null;
			try {
				//Start drag\drop if user is dragging
				switch(e.Button) {
					case MouseButtons.Left:
						if(this.m_bIsDragging) {
							oData = new DataObject();
							if(this.grdMapView.Selected.Rows.Count>0) {
								oData.SetData("");
								this.grdMapView.DoDragDrop(oData, DragDropEffects.Copy);
							}
						}
						break;
				}

			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnMapMouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Event handler for ...
			this.m_bIsDragging = false;
		}
		private void OnMapSelectionDrag(object sender, System.ComponentModel.CancelEventArgs e) {        
			//Event handler for ...
			e.Cancel = !this.m_bIsDragging;
		}
		private void OnMapDragLeave(object sender, System.EventArgs e) {}
		#endregion
		
		#region Mappings Grid Behavior
		private void OnBeforeMappingCellActivated(object sender, Infragistics.Win.UltraWinGrid.CancelableCellEventArgs e) {
			//Event handler for mapping cell activated
			bool bMapping_Rem=false;
			int iSortCenterID=0;
			try {
				//
				bMapping_Rem = (e.Cell.Row.Cells["RowAction"].Value.ToString()=="D");
				iSortCenterID = Convert.ToInt32(e.Cell.Row.Cells["SortCenterID"].Value);
				switch(e.Cell.Column.Key.ToString()) {
					case "PathIDTsort":
						//Get valid Tsort freight paths for this sort center
						this.cboTsortPath.Items.Clear();
						this.mTsortPathsDS.Clear();
						this.mTsortPathsDS.Merge(EnterpriseFactory.GetFreightPaths(iSortCenterID));
						for(int i=0; i<this.mTsortPathsDS.SelectionListTable.Rows.Count; i++) 
							this.cboTsortPath.Items.Add(this.mTsortPathsDS.SelectionListTable[i].ID, this.mTsortPathsDS.SelectionListTable[i].Description);
						this.cboTsortPath.Enabled = (this.cboTsortPath.Items.Count>0);
						e.Cell.Activation = !bMapping_Rem ? Activation.AllowEdit : Activation.NoEdit; 
						break;
					case "ServiceIDTsort":
						e.Cell.Activation = !bMapping_Rem ? Activation.AllowEdit : Activation.NoEdit; 
						break;
					case "PathIDReturns":
						//Get valid return freight paths for this sort center
						this.cboReturnPath.Items.Clear();
						this.mReturnPathsDS.Clear();
						this.mReturnPathsDS.Merge(EnterpriseFactory.GetFreightPaths(iSortCenterID));
						for(int i=0; i<this.mReturnPathsDS.SelectionListTable.Rows.Count; i++) 
							this.cboReturnPath.Items.Add(this.mReturnPathsDS.SelectionListTable[i].ID, this.mReturnPathsDS.SelectionListTable[i].Description);
						this.cboReturnPath.Enabled = (this.cboReturnPath.Items.Count>0);
						e.Cell.Activation = !bMapping_Rem ? Activation.AllowEdit : Activation.NoEdit; 
						break;
					case "ServiceIDReturns":
						e.Cell.Activation = !bMapping_Rem ? Activation.AllowEdit : Activation.NoEdit; 
						break;
					default:		
						e.Cell.Activation = Activation.NoEdit; 
						break;
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnMappingCellChanged(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e) {
			//Event handler for change in a mapping cell value
			try {
				//Validate cell values
				switch(e.Cell.Column.Key.ToString()) {
					case "PathIDTsort":			e.Cell.Row.Cells["TsortPathMnemonic"].Value = e.Cell.Text;		this.grdOverride.UpdateData(); break;
					case "ServiceIDTsort":		e.Cell.Row.Cells["TsortServiceMnemonic"].Value = e.Cell.Text;	this.grdOverride.UpdateData(); break;
					case "PathIDReturns":		e.Cell.Row.Cells["ReturnPathMnemonic"].Value = e.Cell.Text;		this.grdOverride.UpdateData(); break;
					case "ServiceIDReturns":	e.Cell.Row.Cells["ReturnServiceMnemonic"].Value = e.Cell.Text;	this.grdOverride.UpdateData(); break;
					default:					Debug.Write(": value=" + e.Cell.Value.ToString()); break;
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnMappingRowUpdated(object sender, Infragistics.Win.UltraWinGrid.RowEventArgs e) {
			//Event handler for change in a mapping row
			ValidateForm(null, null);
		}
		private void OnMappingSelectionChanged(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for mapping cell activated
			try {
				//Allow remove if there is a selection
				this.btnRem.Enabled = true;
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnMappingDragEnter(object sender, System.Windows.Forms.DragEventArgs e) {
			//Event handler for dragging a mapping into the grid
			DataObject oData=null;
			try {
				//On drag enter, turn on copy drag drop effect
				oData = (DataObject)e.Data;
				if(oData.GetDataPresent(DataFormats.StringFormat, true))
					e.Effect = DragDropEffects.Copy;
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnMappingDragOver(object sender, System.Windows.Forms.DragEventArgs e) {
			//Event handler for dragging a mapping over the grid
			DataObject oData=null;
			try {
				//Retrieve drag drop data
				oData = (DataObject)e.Data;
				if(oData.GetDataPresent(DataFormats.StringFormat, true)) 
					e.Effect = DragDropEffects.Copy;
				else
					e.Effect = DragDropEffects.None;
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnMappingDragDrop(object sender, System.Windows.Forms.DragEventArgs e) {
			//Event handler for dropping a mapping onto the grid
			DataObject oData=null;
			try {
				//Retrieve data
				oData = (DataObject)e.Data;
				if(oData.GetDataPresent(DataFormats.StringFormat, true)) {
					//
					if(this.btnEdit.Enabled) OnCmdClick(this.btnEdit, null);
				}
			}
			catch(Exception ex) { reportError(ex); }
		}
		#endregion
		#region User Services: ValidateForm(), OnCmdClick(), OnMenuClick()
		private void ValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes to control data
			bool bMappingsValid;
			Object oCell=null;
			string sRowAction="", sTFP="", sRFP="";
			int iTST=0, iRST=0;
			
			try {
				//Validate mappings (freight paths, and service types)
				this.btnApply.Enabled = false;
				bMappingsValid = true;
				for(int i=0; i<this.grdOverride.Rows.Count; i++) {
					sRowAction = this.grdOverride.Rows[i].Cells["RowAction"].Value.ToString();
					if(sRowAction!="D") {
						//Validate edit records (deletes are valid) for freight paths and service type;
						//country and postal code cannot be changed and therefore do NOT require validation
						sTFP = this.grdOverride.Rows[i].Cells["PathIDTsort"].Value.ToString();
						oCell = this.grdOverride.Rows[i].Cells["ServiceIDTsort"].Value;
						iTST = (oCell!=DBNull.Value) ? Convert.ToInt32(oCell) : 0;
						sRFP = this.grdOverride.Rows[i].Cells["PathIDReturns"].Value.ToString();
						oCell = this.grdOverride.Rows[i].Cells["ServiceIDReturns"].Value;
						iRST = (oCell!=DBNull.Value) ? Convert.ToInt32(oCell) : 0;
						
						//Verify freight paths, and service types
						bMappingsValid = ((sTFP!="" && iTST>0) && (sRFP!="" && iRST>0));
					}
				}
				
				//Enable OK service if details have valid changes
				this.btnApply.Enabled = bMappingsValid;
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnCmdClick(object sender, System.EventArgs e) {
			//Command button handler
			MapDS.PostalCodeMappingTableRow _rowMapping;
			int iMappings=0;
			MapDS dsMappings=null;
			bool bUpdated=false;
			try {
				Button btn = (Button)sender;
				switch(btn.Text) {
					case CMD_EDIT: 
						this.ctxMapEdit.PerformClick();
						break;
					case CMD_DEL: 
						this.ctxMapDelete.PerformClick();
						break;
					case CMD_REM: 
						//Remove this record from the (transactional) mappings grid
						if(this.grdOverride.ActiveRow!=null) this.grdOverride.ActiveRow.Selected = true;
						this.grdOverride.Selected.Rows[0].Delete(false);
						this.grdOverride.Update();
						this.mMappingsDS.AcceptChanges();
						this.btnRem.Enabled = false;
						break;
					case CMD_APPLY: 
						//Update existing mappings; apply single updates (edit, delete)
						this.Cursor = Cursors.WaitCursor;
						iMappings = this.mMappingsDS.PostalCodeMappingTable.Rows.Count;
						for(int i=0; i<iMappings; i++) {
							try {
								//Edit, delete?
								_rowMapping = this.mMappingsDS.PostalCodeMappingTable[i];
								this.mMapDS.PostalCodeMappingTable.Clear();
								switch(_rowMapping.RowAction) {
									case "D": 
										Debug.Write(i.ToString() + "-Delete\n" + "mapID=" + _rowMapping.MapID + ", countryID=" + _rowMapping.CountryID.ToString() + ", code=" + _rowMapping.PostalCode + ", rowVer=" + _rowMapping.RowVersion + "\n");
										bUpdated = EnterpriseFactory.DeleteMapping(_rowMapping.MapID, _rowMapping.CountryID, _rowMapping.PostalCode, _rowMapping.RowVersion); 
										break;
									default:  
										this.mMapDS.PostalCodeMappingTable.ImportRow(_rowMapping);
										Debug.Write(i.ToString() + "-Edit\n" + this.mMapDS.GetXml() + "\n");
										bUpdated = EnterpriseFactory.UpdateMapping(this.mMapDS); 
										break;
								}
								if(bUpdated) this.mMappingsDS.PostalCodeMappingTable[i].Status="OK";
							}
							catch(Exception exc) {
								this.mMappingsDS.PostalCodeMappingTable[i].Status = exc.Message;
							}
						}

						//Continue with edit mode- copy edit mappings to a temp dataset and reload view\empty mappings
						dsMappings = new MapDS();
						dsMappings.Merge(this.mMappingsDS);
						OnCodeChanged(null, null);
						//Copy failed mappings back
						for(int i=0; i<dsMappings.PostalCodeMappingTable.Rows.Count; i++) {
							if(dsMappings.PostalCodeMappingTable[i].Status!="OK") 
								this.mMappingsDS.PostalCodeMappingTable.ImportRow(dsMappings.PostalCodeMappingTable[i]);
						}
						
						break;
					case CMD_CLOSE:
						//Close the dialog
						this.DialogResult = DialogResult.OK;
						this.Close();
						break;
				}
			} 
			catch(Exception ex) { reportError(ex); }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnMenuClick(object sender, System.EventArgs e) {
			//Menu item clicked-apply selected service
			string sMapID="", sPostalCode="";
			DataRow[] rowExists=null;
			MapDS.PostalCodeMappingTableRow _rowMapping;
			MapDS.PostalCodeMappingTableRow rowMapping;

			try  {
				//Make sure this postal code has no existing record in grdOverride
				sMapID = this.grdMapView.Selected.Rows[0].Cells["MapID"].Value.ToString();
				sPostalCode = this.grdMapView.Selected.Rows[0].Cells["PostalCode"].Value.ToString();
				rowExists = this.mMappingsDS.PostalCodeMappingTable.Select("MapID='" + sMapID + "' AND PostalCode='" + sPostalCode + "'");
				if(rowExists.Length==0) {
					MenuItem menu = (MenuItem)sender;
					switch(menu.Text)  {
						case MNU_EDIT:
							//Copy selected row to edit mappings grid
							rowMapping = (Tsort.Enterprise.MapDS.PostalCodeMappingTableRow)this.mMapViewDS.PostalCodeMappingTable.Select("MapID='" + sMapID + "' AND PostalCode='" + sPostalCode + "'")[0];
							if(rowMapping!=null) {
								_rowMapping = this.mMappingsDS.PostalCodeMappingTable.NewPostalCodeMappingTableRow();
								#region Copy field-by-field
								_rowMapping.RowAction = "E";
								_rowMapping.MapID = rowMapping.MapID;
								_rowMapping.SortCenterID = rowMapping.SortCenterID;
								_rowMapping.SortCenter = rowMapping.SortCenter;
								if(!rowMapping.IsClientIDNull())_rowMapping.ClientID = rowMapping.ClientID;
								if(!rowMapping.IsNumberNull()) _rowMapping.Number = rowMapping.Number;
								if(!rowMapping.IsClientNameNull()) _rowMapping.ClientName = rowMapping.ClientName;
								_rowMapping.Description = rowMapping.Description;
								_rowMapping.IsActive = rowMapping.IsActive;
								_rowMapping.PostalCode = rowMapping.PostalCode;
								_rowMapping.CountryID = rowMapping.CountryID;
								_rowMapping.Country = rowMapping.Country;
								_rowMapping.StateOrProvince = rowMapping.StateOrProvince;
								if(!rowMapping.IsPathIDTsortNull()) _rowMapping.OldPathIDTsort = _rowMapping.PathIDTsort = rowMapping.PathIDTsort;
								if(!rowMapping.IsTsortPathMnemonicNull()) _rowMapping.OldTsortPathMnemonic = _rowMapping.TsortPathMnemonic = rowMapping.TsortPathMnemonic;
								if(!rowMapping.IsServiceIDTsortNull()) _rowMapping.OldServiceIDTsort = _rowMapping.ServiceIDTsort = rowMapping.ServiceIDTsort;
								if(!rowMapping.IsTsortServiceMnemonicNull()) _rowMapping.OldTsortServiceMnemonic = _rowMapping.TsortServiceMnemonic = rowMapping.TsortServiceMnemonic;
								if(!rowMapping.IsPathIDReturnsNull()) _rowMapping.OldPathIDReturns = _rowMapping.PathIDReturns = rowMapping.PathIDReturns;
								if(!rowMapping.IsReturnPathMnemonicNull()) _rowMapping.OldReturnPathMnemonic = _rowMapping.ReturnPathMnemonic = rowMapping.ReturnPathMnemonic;
								if(!rowMapping.IsServiceIDReturnsNull()) _rowMapping.OldServiceIDReturns = _rowMapping.ServiceIDReturns = rowMapping.ServiceIDReturns;
								if(!rowMapping.IsReturnServiceMnemonicNull()) _rowMapping.OldReturnServiceMnemonic = _rowMapping.ReturnServiceMnemonic = rowMapping.ReturnServiceMnemonic;
								_rowMapping.LastUpdated = DateTime.Now;
								_rowMapping.UserID = Environment.UserName;
								_rowMapping.RowVersion = (!rowMapping.IsRowVersionNull()) ? rowMapping.RowVersion : "";
								_rowMapping.Status = "";
								#endregion
								this.mMappingsDS.PostalCodeMappingTable.AddPostalCodeMappingTableRow(_rowMapping);
							}
							this.btnEdit.Enabled = this.btnDel.Enabled = false;
							break;
						case MNU_REMOVE:
							//Copy selected row to edit mappings grid and mark for delete
							rowMapping = (Tsort.Enterprise.MapDS.PostalCodeMappingTableRow)this.mMapViewDS.PostalCodeMappingTable.Select("MapID='" + sMapID + "' AND PostalCode='" + sPostalCode + "'")[0];
							if(rowMapping!=null) {
								_rowMapping = this.mMappingsDS.PostalCodeMappingTable.NewPostalCodeMappingTableRow();
								#region Copy field-by-field
								_rowMapping.RowAction = "D";
								_rowMapping.MapID = rowMapping.MapID;
								_rowMapping.SortCenterID = rowMapping.SortCenterID;
								_rowMapping.SortCenter = rowMapping.SortCenter;
								if(!rowMapping.IsClientIDNull())_rowMapping.ClientID = rowMapping.ClientID;
								if(!rowMapping.IsNumberNull()) _rowMapping.Number = rowMapping.Number;
								if(!rowMapping.IsClientNameNull()) _rowMapping.ClientName = rowMapping.ClientName;
								_rowMapping.Description = rowMapping.Description;
								_rowMapping.IsActive = rowMapping.IsActive;
								_rowMapping.PostalCode = rowMapping.PostalCode;
								_rowMapping.CountryID = rowMapping.CountryID;
								_rowMapping.Country = rowMapping.Country;
								_rowMapping.StateOrProvince = rowMapping.StateOrProvince;
								if(!rowMapping.IsPathIDTsortNull()) _rowMapping.OldPathIDTsort = rowMapping.PathIDTsort;
								if(!rowMapping.IsTsortPathMnemonicNull()) _rowMapping.OldTsortPathMnemonic = rowMapping.TsortPathMnemonic;
								if(!rowMapping.IsServiceIDTsortNull()) _rowMapping.OldServiceIDTsort = rowMapping.ServiceIDTsort;
								if(!rowMapping.IsTsortServiceMnemonicNull()) _rowMapping.OldTsortServiceMnemonic = rowMapping.TsortServiceMnemonic;
								if(!rowMapping.IsPathIDReturnsNull()) _rowMapping.OldPathIDReturns = rowMapping.PathIDReturns;
								if(!rowMapping.IsReturnPathMnemonicNull()) _rowMapping.OldReturnPathMnemonic = rowMapping.ReturnPathMnemonic;
								if(!rowMapping.IsServiceIDReturnsNull()) _rowMapping.OldServiceIDReturns = rowMapping.ServiceIDReturns;
								if(!rowMapping.IsReturnServiceMnemonicNull()) _rowMapping.OldReturnServiceMnemonic = rowMapping.ReturnServiceMnemonic;
								_rowMapping.LastUpdated = DateTime.Now;
								_rowMapping.UserID = Environment.UserName;
								_rowMapping.RowVersion = (!rowMapping.IsRowVersionNull()) ? rowMapping.RowVersion : "";
								_rowMapping.Status = "";
								#endregion
								this.mMappingsDS.PostalCodeMappingTable.AddPostalCodeMappingTableRow(_rowMapping);
							}
							this.btnEdit.Enabled = this.btnDel.Enabled = false;
							break;
					}
				}
				else {
					//Select row and notify user
					for(int i=0; i<this.grdOverride.Rows.Count; i++) {
						if(this.grdOverride.Rows[i].Cells["PostalCode"].Value.ToString()==sPostalCode) {
							this.grdOverride.ActiveRow = this.grdOverride.Rows[i];
							this.grdOverride.ActiveRow.Selected = true;
							break;
						}
					}
					MessageBox.Show(this, "Postal code is under edit.");
				}
			}
			catch(Exception ex) { reportError(ex); }
		}
		#endregion
		#region Local Services: reportError()
		private void reportError(Exception ex) { reportError(ex, "", "", ""); }
		private void reportError(Exception ex, string keyword1, string keyword2, string keyword3) { 
			if(this.ErrorMessage != null) this.ErrorMessage(this, new ErrorEventArgs(ex,keyword1,keyword2,keyword3));
		}
		#endregion
	}
}
