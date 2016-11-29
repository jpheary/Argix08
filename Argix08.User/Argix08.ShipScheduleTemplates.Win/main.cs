using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Windows;

namespace Argix.AgentLineHaul {
	//
	public class frmMain : System.Windows.Forms.Form {
		//Members
		private PageSettings mPageSettings = null;
		private UltraGridSvc mGridSvc=null;
		private System.Windows.Forms.ToolTip mToolTip=null;
		private MessageManager mMessageMgr=null;
		private NameValueCollection mHelpItems=null;
		
		#region Controls
		private Infragistics.Win.UltraWinGrid.UltraGrid grdTemplates;
		private Infragistics.Win.UltraWinGrid.UltraDropDown uddSortCenter;
		private Infragistics.Win.UltraWinGrid.UltraDropDown uddCarrier;
		private Infragistics.Win.UltraWinGrid.UltraDropDown uddZone;
		private Infragistics.Win.UltraWinGrid.UltraDropDown uddS2Zone;
		private Infragistics.Win.UltraWinGrid.UltraDropDown uddDay;
        private Infragistics.Win.UltraWinGrid.UltraGridRow grdRow;
        private Argix.Windows.ArgixStatusBar stbMain;
        private ToolStrip tsMain;
        private ToolStripButton btnNew;
        private ToolStripButton btnOpen;
        private ToolStripButton btnSave;
        private ToolStripSeparator btnSep1;
        private ToolStripButton btnPrint;
        private ToolStripButton btnPreview;
        private ToolStripSeparator btnSep2;
        private ToolStripButton btnCut;
        private ToolStripButton btnCopy;
        private ToolStripButton btnPaste;
        private ToolStripButton btnSearch;
        private ToolStripSeparator btnSep3;
        private ToolStripButton btnRefresh;
        private MenuStrip msMain;
        private ToolStripMenuItem mnuFile;
        private ToolStripMenuItem mnuFileNew;
        private ToolStripMenuItem mnuFileOpen;
        private ToolStripSeparator mnuFileSep1;
        private ToolStripMenuItem mnuFileSaveAs;
        private ToolStripSeparator mnuFileSep2;
        private ToolStripMenuItem mnuFileExport;
        private ToolStripSeparator mnuFileSep3;
        private ToolStripMenuItem mnuFileSetup;
        private ToolStripMenuItem mnuFilePrint;
        private ToolStripMenuItem mnuFilePreview;
        private ToolStripMenuItem mnuFileExit;
        private ToolStripMenuItem mnuEdit;
        private ToolStripMenuItem mnuEditCut;
        private ToolStripMenuItem mnuEditCopy;
        private ToolStripMenuItem mnuEditPaste;
        private ToolStripSeparator mnuEditSep1;
        private ToolStripMenuItem mnuEditSearch;
        private ToolStripMenuItem mnuView;
        private ToolStripMenuItem mnuViewRefresh;
        private ToolStripSeparator mnuViewSep1;
        private ToolStripMenuItem mnuViewToolbar;
        private ToolStripMenuItem mnuViewStatusBar;
        private ToolStripMenuItem mnuTools;
        private ToolStripMenuItem mnuToolsConfig;
        private ToolStripMenuItem mnuHelp;
        private ToolStripMenuItem mnuHelpAbout;
        private ToolStripSeparator mnuHelpSep1;
        private ContextMenuStrip csMain;
        private ToolStripMenuItem ctxRefresh;
        private ToolStripSeparator ctxSep1;
        private ToolStripMenuItem ctxCut;
        private ToolStripMenuItem ctxCopy;
        private ToolStripMenuItem ctxPaste;
        private BindingSource mTemplates;
        private ToolStripMenuItem mnuFileSave;
        private ToolStripButton btnExport;
        private ToolStripSeparator btnSep4;
        private TemplateDS mTemplateDS;
		private System.ComponentModel.IContainer components;
		#endregion
		//Interface
		public frmMain() {
			try {
				InitializeComponent();
				this.Text = "Argix Direct " + App.Product;
				buildHelpMenu();
				Splash.Start(App.Product, Assembly.GetExecutingAssembly(), App.Copyright);
				Thread.Sleep(3000);
				#region Window docking
                this.msMain.Dock = DockStyle.Top;
				this.tsMain.Dock = DockStyle.Top;
				this.stbMain.Dock = DockStyle.Bottom;
                this.Controls.AddRange(new Control[] { this.tsMain,this.stbMain,this.msMain });
				#endregion
				
				//Create data and UI services
				this.mPageSettings = new PageSettings();
				this.mPageSettings.Landscape = true;
				this.mGridSvc = new UltraGridSvc(this.grdTemplates);
				this.mToolTip = new System.Windows.Forms.ToolTip();
				this.mMessageMgr = new MessageManager(this.stbMain.Panels[0], 500, 3000);
				configApplication();
				
				//Only Line Haul Administrators can run the application
				if(!AppSecurity.UserCanAccessTemplate) {
					throw new ApplicationException("You are not authorized to run the application. Application will shut down.");
				}
				#region Init UltraGrid ValueLists
				Infragistics.Win.ValueListsCollection lists = this.grdTemplates.DisplayLayout.ValueLists;
				Infragistics.Win.ValueList vl=null;
				vl = lists.Add("schCloseDaysOffset"); for(int i=0; i<3; i++) { vl.ValueListItems.Add(i, i.ToString()); }
				vl = lists.Add("schDepartDaysOffset"); for(int i=0; i<4; i++) { vl.ValueListItems.Add(i, i.ToString()); }
				vl = lists.Add("schArrivalDaysOffset"); for(int i=0; i<15; i++) { vl.ValueListItems.Add(i, i.ToString()); }
				vl = lists.Add("schOFD1DaysOffset"); for (int i=0; i<17; i++) { vl.ValueListItems.Add(i, i.ToString()); }
				#endregion
			}
			catch(Exception ex) { Splash.Close(); throw new ApplicationException("Startup Failure", ex); }
		}
		protected override void Dispose( bool disposing ) { if(disposing) { if(components != null) { components.Dispose(); } } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.BindingSource mS2Zones;
            System.Windows.Forms.BindingSource mZones;
            System.Windows.Forms.BindingSource mCarriers;
            System.Windows.Forms.BindingSource mSortCenters;
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Terminal", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn43 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn44 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientDivision");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn45 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DBServerName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn46 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DBtype");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn47 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn48 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsActive");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn49 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LinkedServerName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn50 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LocationID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn51 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Mnemonic");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn52 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Number");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn53 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShipperID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn54 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TerminalID");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Carrier", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn55 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("APNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn56 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CarrierID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn57 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CarrierNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn58 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CarrierServiceID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn59 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsActive");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn60 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn61 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Mode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn62 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn63 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ParentID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn64 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SCAC");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn65 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Agent", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn66 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("APNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn67 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn68 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ContactName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn69 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DeliveryScanStatus");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn70 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Fax");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn71 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn72 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Mnemonic");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn73 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ParentID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn74 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Phone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn75 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TransmitEBOL");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn76 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn77 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AddressLine1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn78 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AddressLine2");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn79 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("City");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn80 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LocationID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn81 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Name", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn82 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Number");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn83 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("State");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn84 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn85 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserData");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn86 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zip");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn87 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zip4");
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand4 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Agent", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn88 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("APNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn89 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn90 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ContactName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn91 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DeliveryScanStatus");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn92 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Fax");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn93 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn94 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Mnemonic");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn95 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ParentID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn96 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Phone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn97 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TransmitEBOL");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn98 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn99 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AddressLine1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn100 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AddressLine2");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn101 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("City");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn102 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LocationID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn103 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn104 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Number");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn105 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("State");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn106 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn107 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserData");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn108 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zip");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn109 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zip4");
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand5 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn110 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn111 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand6 = new Infragistics.Win.UltraWinGrid.UltraGridBand("TemplateTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn112 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TemplateID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn113 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenterID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn114 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenter");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn115 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DayOfTheWeek");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn116 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CarrierServiceID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn117 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Carrier");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn118 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledCloseDateOffset");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn119 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledCloseTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn120 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledDepartureDateOffset");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn121 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledDepartureTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn122 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsMandatory");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn123 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsActive");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn124 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TemplateLastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn125 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TemplateUser");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn126 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TemplateRowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn127 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn128 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn129 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MainZone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn130 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Tag");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn131 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn132 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn133 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledArrivalDateOffset");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn134 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledArrivalTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn135 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledOFD1Offset");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn136 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Notes");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn137 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stop1LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn138 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stop1User");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn139 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stop1RowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn140 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2StopID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn141 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2StopNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn142 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2MainZone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn143 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2Tag");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn144 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn145 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn146 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ScheduledArrivalDateOffset");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn147 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ScheduledArrivalTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn148 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ScheduledOFD1Offset");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn149 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2Notes");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn150 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stop2LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn151 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stop2User");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn152 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stop2RowVersion");
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.uddSortCenter = new Infragistics.Win.UltraWinGrid.UltraDropDown();
            this.uddCarrier = new Infragistics.Win.UltraWinGrid.UltraDropDown();
            this.uddZone = new Infragistics.Win.UltraWinGrid.UltraDropDown();
            this.uddS2Zone = new Infragistics.Win.UltraWinGrid.UltraDropDown();
            this.uddDay = new Infragistics.Win.UltraWinGrid.UltraDropDown();
            this.stbMain = new Argix.Windows.ArgixStatusBar();
            this.grdTemplates = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.csMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxCut = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.mTemplates = new System.Windows.Forms.BindingSource(this.components);
            this.mTemplateDS = new Argix.AgentLineHaul.TemplateDS();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.btnSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnPreview = new System.Windows.Forms.ToolStripButton();
            this.btnSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCut = new System.Windows.Forms.ToolStripButton();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnPaste = new System.Windows.Forms.ToolStripButton();
            this.btnSearch = new System.Windows.Forms.ToolStripButton();
            this.btnSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePreview = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            mS2Zones = new System.Windows.Forms.BindingSource(this.components);
            mZones = new System.Windows.Forms.BindingSource(this.components);
            mCarriers = new System.Windows.Forms.BindingSource(this.components);
            mSortCenters = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(mS2Zones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(mZones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(mCarriers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(mSortCenters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddSortCenter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddCarrier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddZone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddS2Zone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTemplates)).BeginInit();
            this.csMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mTemplates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTemplateDS)).BeginInit();
            this.tsMain.SuspendLayout();
            this.msMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mS2Zones
            // 
            mS2Zones.DataSource = typeof(Argix.AgentLineHaul.Agents);
            // 
            // mZones
            // 
            mZones.DataSource = typeof(Argix.AgentLineHaul.Agents);
            // 
            // mCarriers
            // 
            mCarriers.DataSource = typeof(Argix.AgentLineHaul.Carriers);
            // 
            // mSortCenters
            // 
            mSortCenters.DataSource = typeof(Argix.AgentLineHaul.Terminals);
            // 
            // uddSortCenter
            // 
            this.uddSortCenter.Cursor = System.Windows.Forms.Cursors.Default;
            this.uddSortCenter.DataSource = mSortCenters;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.Name = "Verdana";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.uddSortCenter.DisplayLayout.Appearance = appearance1;
            ultraGridColumn42.Header.VisiblePosition = 0;
            ultraGridColumn42.Hidden = true;
            ultraGridColumn43.Header.VisiblePosition = 1;
            ultraGridColumn43.Hidden = true;
            ultraGridColumn44.Header.VisiblePosition = 2;
            ultraGridColumn44.Hidden = true;
            ultraGridColumn45.Header.VisiblePosition = 3;
            ultraGridColumn45.Hidden = true;
            ultraGridColumn46.Header.VisiblePosition = 4;
            ultraGridColumn46.Hidden = true;
            ultraGridColumn47.Header.Caption = "Name";
            ultraGridColumn47.Header.VisiblePosition = 5;
            ultraGridColumn48.Header.VisiblePosition = 6;
            ultraGridColumn48.Hidden = true;
            ultraGridColumn49.Header.VisiblePosition = 8;
            ultraGridColumn49.Hidden = true;
            ultraGridColumn50.Header.VisiblePosition = 7;
            ultraGridColumn50.Hidden = true;
            ultraGridColumn51.Header.VisiblePosition = 9;
            ultraGridColumn51.Hidden = true;
            ultraGridColumn52.Header.VisiblePosition = 11;
            ultraGridColumn52.Hidden = true;
            ultraGridColumn53.Header.VisiblePosition = 10;
            ultraGridColumn53.Hidden = true;
            ultraGridColumn54.Header.VisiblePosition = 12;
            ultraGridColumn54.Hidden = true;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn42,
            ultraGridColumn43,
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
            ultraGridColumn54});
            this.uddSortCenter.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.FontData.Name = "Verdana";
            appearance2.FontData.SizeInPoints = 8F;
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.uddSortCenter.DisplayLayout.CaptionAppearance = appearance2;
            this.uddSortCenter.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.uddSortCenter.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.uddSortCenter.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.uddSortCenter.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.Name = "Verdana";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.TextHAlignAsString = "Left";
            this.uddSortCenter.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.uddSortCenter.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.uddSortCenter.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.uddSortCenter.DisplayLayout.Override.RowAppearance = appearance4;
            this.uddSortCenter.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.uddSortCenter.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.uddSortCenter.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.uddSortCenter.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.uddSortCenter.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.uddSortCenter.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.uddSortCenter.DisplayMember = "Description";
            this.uddSortCenter.Location = new System.Drawing.Point(12, 206);
            this.uddSortCenter.Name = "uddSortCenter";
            this.uddSortCenter.Size = new System.Drawing.Size(102, 32);
            this.uddSortCenter.TabIndex = 9;
            this.uddSortCenter.ValueMember = "LocationID";
            this.uddSortCenter.Visible = false;
            this.uddSortCenter.RowSelected += new Infragistics.Win.UltraWinGrid.RowSelectedEventHandler(this.OnSortCenterSelected);
            this.uddSortCenter.BeforeDropDown += new System.ComponentModel.CancelEventHandler(this.OnSortCenterBeforeDropDown);
            // 
            // uddCarrier
            // 
            this.uddCarrier.Cursor = System.Windows.Forms.Cursors.Default;
            this.uddCarrier.DataSource = mCarriers;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.FontData.Name = "Verdana";
            appearance5.FontData.SizeInPoints = 8F;
            appearance5.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance5.TextHAlignAsString = "Left";
            this.uddCarrier.DisplayLayout.Appearance = appearance5;
            ultraGridColumn55.Header.VisiblePosition = 0;
            ultraGridColumn55.Hidden = true;
            ultraGridColumn56.Header.VisiblePosition = 1;
            ultraGridColumn56.Hidden = true;
            ultraGridColumn57.Header.VisiblePosition = 2;
            ultraGridColumn57.Hidden = true;
            ultraGridColumn58.Header.VisiblePosition = 3;
            ultraGridColumn58.Hidden = true;
            ultraGridColumn59.Header.VisiblePosition = 4;
            ultraGridColumn59.Hidden = true;
            ultraGridColumn60.Header.VisiblePosition = 5;
            ultraGridColumn60.Hidden = true;
            ultraGridColumn61.Header.VisiblePosition = 6;
            ultraGridColumn61.Hidden = true;
            ultraGridColumn62.Header.VisiblePosition = 7;
            ultraGridColumn63.Header.VisiblePosition = 8;
            ultraGridColumn63.Hidden = true;
            ultraGridColumn64.Header.VisiblePosition = 9;
            ultraGridColumn64.Hidden = true;
            ultraGridColumn65.Header.VisiblePosition = 10;
            ultraGridColumn65.Hidden = true;
            ultraGridBand2.Columns.AddRange(new object[] {
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
            ultraGridColumn65});
            this.uddCarrier.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            appearance6.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance6.FontData.BoldAsString = "True";
            appearance6.FontData.Name = "Verdana";
            appearance6.FontData.SizeInPoints = 8F;
            appearance6.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance6.TextHAlignAsString = "Left";
            this.uddCarrier.DisplayLayout.CaptionAppearance = appearance6;
            this.uddCarrier.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.uddCarrier.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.uddCarrier.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.uddCarrier.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance7.BackColor = System.Drawing.SystemColors.Control;
            appearance7.FontData.BoldAsString = "True";
            appearance7.FontData.Name = "Verdana";
            appearance7.FontData.SizeInPoints = 8F;
            appearance7.TextHAlignAsString = "Left";
            this.uddCarrier.DisplayLayout.Override.HeaderAppearance = appearance7;
            this.uddCarrier.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.uddCarrier.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance8.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.uddCarrier.DisplayLayout.Override.RowAppearance = appearance8;
            this.uddCarrier.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.uddCarrier.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.uddCarrier.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.uddCarrier.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.uddCarrier.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.uddCarrier.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.uddCarrier.Location = new System.Drawing.Point(120, 206);
            this.uddCarrier.Name = "uddCarrier";
            this.uddCarrier.Size = new System.Drawing.Size(112, 32);
            this.uddCarrier.TabIndex = 10;
            this.uddCarrier.Visible = false;
            this.uddCarrier.RowSelected += new Infragistics.Win.UltraWinGrid.RowSelectedEventHandler(this.OnCarrierSelected);
            this.uddCarrier.BeforeDropDown += new System.ComponentModel.CancelEventHandler(this.OnCarrierBeforeDropDown);
            // 
            // uddZone
            // 
            this.uddZone.DataSource = mZones;
            appearance9.BackColor = System.Drawing.SystemColors.Window;
            appearance9.FontData.Name = "Verdana";
            appearance9.FontData.SizeInPoints = 8F;
            appearance9.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance9.TextHAlignAsString = "Left";
            this.uddZone.DisplayLayout.Appearance = appearance9;
            ultraGridColumn66.Header.VisiblePosition = 0;
            ultraGridColumn66.Hidden = true;
            ultraGridColumn67.Header.VisiblePosition = 1;
            ultraGridColumn67.Hidden = true;
            ultraGridColumn68.Header.VisiblePosition = 2;
            ultraGridColumn68.Hidden = true;
            ultraGridColumn69.Header.VisiblePosition = 3;
            ultraGridColumn69.Hidden = true;
            ultraGridColumn70.Header.VisiblePosition = 4;
            ultraGridColumn70.Hidden = true;
            ultraGridColumn71.Header.VisiblePosition = 5;
            ultraGridColumn71.Hidden = true;
            ultraGridColumn72.Header.VisiblePosition = 6;
            ultraGridColumn72.Hidden = true;
            ultraGridColumn73.Header.VisiblePosition = 7;
            ultraGridColumn73.Hidden = true;
            ultraGridColumn74.Header.VisiblePosition = 9;
            ultraGridColumn74.Hidden = true;
            ultraGridColumn75.Header.VisiblePosition = 8;
            ultraGridColumn75.Hidden = true;
            ultraGridColumn76.Header.VisiblePosition = 10;
            ultraGridColumn76.Hidden = true;
            ultraGridColumn77.Header.VisiblePosition = 11;
            ultraGridColumn77.Hidden = true;
            ultraGridColumn78.Header.VisiblePosition = 12;
            ultraGridColumn78.Hidden = true;
            ultraGridColumn79.Header.VisiblePosition = 13;
            ultraGridColumn79.Hidden = true;
            ultraGridColumn80.Header.VisiblePosition = 14;
            ultraGridColumn80.Hidden = true;
            ultraGridColumn81.Header.VisiblePosition = 15;
            ultraGridColumn82.Header.VisiblePosition = 16;
            ultraGridColumn82.Hidden = true;
            ultraGridColumn83.Header.VisiblePosition = 17;
            ultraGridColumn83.Hidden = true;
            ultraGridColumn84.Header.VisiblePosition = 18;
            ultraGridColumn84.Hidden = true;
            ultraGridColumn85.Header.VisiblePosition = 19;
            ultraGridColumn85.Hidden = true;
            ultraGridColumn86.Header.VisiblePosition = 20;
            ultraGridColumn86.Hidden = true;
            ultraGridColumn87.Header.VisiblePosition = 21;
            ultraGridColumn87.Hidden = true;
            ultraGridBand3.Columns.AddRange(new object[] {
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
            ultraGridColumn87});
            this.uddZone.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
            appearance10.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance10.FontData.BoldAsString = "True";
            appearance10.FontData.Name = "Verdana";
            appearance10.FontData.SizeInPoints = 8F;
            appearance10.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance10.TextHAlignAsString = "Left";
            this.uddZone.DisplayLayout.CaptionAppearance = appearance10;
            this.uddZone.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.uddZone.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.uddZone.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.uddZone.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance11.BackColor = System.Drawing.SystemColors.Control;
            appearance11.FontData.BoldAsString = "True";
            appearance11.FontData.Name = "Verdana";
            appearance11.FontData.SizeInPoints = 8F;
            appearance11.TextHAlignAsString = "Left";
            this.uddZone.DisplayLayout.Override.HeaderAppearance = appearance11;
            this.uddZone.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.uddZone.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance12.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.uddZone.DisplayLayout.Override.RowAppearance = appearance12;
            this.uddZone.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.uddZone.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.uddZone.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.uddZone.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.uddZone.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.uddZone.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.uddZone.DisplayMember = "Name";
            this.uddZone.Location = new System.Drawing.Point(234, 206);
            this.uddZone.Name = "uddZone";
            this.uddZone.Size = new System.Drawing.Size(104, 32);
            this.uddZone.TabIndex = 11;
            this.uddZone.ValueMember = "LocationID";
            this.uddZone.Visible = false;
            this.uddZone.RowSelected += new Infragistics.Win.UltraWinGrid.RowSelectedEventHandler(this.OnZoneSelected);
            // 
            // uddS2Zone
            // 
            this.uddS2Zone.Cursor = System.Windows.Forms.Cursors.Default;
            this.uddS2Zone.DataSource = mS2Zones;
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.FontData.Name = "Verdana";
            appearance13.FontData.SizeInPoints = 8F;
            appearance13.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance13.TextHAlignAsString = "Left";
            this.uddS2Zone.DisplayLayout.Appearance = appearance13;
            ultraGridColumn88.Header.VisiblePosition = 0;
            ultraGridColumn88.Hidden = true;
            ultraGridColumn89.Header.VisiblePosition = 1;
            ultraGridColumn89.Hidden = true;
            ultraGridColumn90.Header.VisiblePosition = 2;
            ultraGridColumn90.Hidden = true;
            ultraGridColumn91.Header.VisiblePosition = 3;
            ultraGridColumn91.Hidden = true;
            ultraGridColumn92.Header.VisiblePosition = 4;
            ultraGridColumn92.Hidden = true;
            ultraGridColumn93.Header.VisiblePosition = 5;
            ultraGridColumn93.Hidden = true;
            ultraGridColumn94.Header.VisiblePosition = 6;
            ultraGridColumn94.Hidden = true;
            ultraGridColumn95.Header.VisiblePosition = 7;
            ultraGridColumn95.Hidden = true;
            ultraGridColumn96.Header.VisiblePosition = 8;
            ultraGridColumn96.Hidden = true;
            ultraGridColumn97.Header.VisiblePosition = 9;
            ultraGridColumn97.Hidden = true;
            ultraGridColumn98.Header.VisiblePosition = 10;
            ultraGridColumn98.Hidden = true;
            ultraGridColumn99.Header.VisiblePosition = 11;
            ultraGridColumn99.Hidden = true;
            ultraGridColumn100.Header.VisiblePosition = 12;
            ultraGridColumn100.Hidden = true;
            ultraGridColumn101.Header.VisiblePosition = 13;
            ultraGridColumn101.Hidden = true;
            ultraGridColumn102.Header.VisiblePosition = 14;
            ultraGridColumn102.Hidden = true;
            ultraGridColumn103.Header.VisiblePosition = 15;
            ultraGridColumn104.Header.VisiblePosition = 16;
            ultraGridColumn105.Header.VisiblePosition = 17;
            ultraGridColumn105.Hidden = true;
            ultraGridColumn106.Header.VisiblePosition = 18;
            ultraGridColumn106.Hidden = true;
            ultraGridColumn107.Header.VisiblePosition = 19;
            ultraGridColumn107.Hidden = true;
            ultraGridColumn108.Header.VisiblePosition = 20;
            ultraGridColumn108.Hidden = true;
            ultraGridColumn109.Header.VisiblePosition = 21;
            ultraGridColumn109.Hidden = true;
            ultraGridBand4.Columns.AddRange(new object[] {
            ultraGridColumn88,
            ultraGridColumn89,
            ultraGridColumn90,
            ultraGridColumn91,
            ultraGridColumn92,
            ultraGridColumn93,
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
            ultraGridColumn109});
            this.uddS2Zone.DisplayLayout.BandsSerializer.Add(ultraGridBand4);
            appearance14.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance14.FontData.BoldAsString = "True";
            appearance14.FontData.Name = "Verdana";
            appearance14.FontData.SizeInPoints = 8F;
            appearance14.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance14.TextHAlignAsString = "Left";
            this.uddS2Zone.DisplayLayout.CaptionAppearance = appearance14;
            this.uddS2Zone.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.uddS2Zone.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.uddS2Zone.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.uddS2Zone.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance15.BackColor = System.Drawing.SystemColors.Control;
            appearance15.FontData.BoldAsString = "True";
            appearance15.FontData.Name = "Verdana";
            appearance15.FontData.SizeInPoints = 8F;
            appearance15.TextHAlignAsString = "Left";
            this.uddS2Zone.DisplayLayout.Override.HeaderAppearance = appearance15;
            this.uddS2Zone.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.uddS2Zone.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance16.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.uddS2Zone.DisplayLayout.Override.RowAppearance = appearance16;
            this.uddS2Zone.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.uddS2Zone.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.uddS2Zone.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.uddS2Zone.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.uddS2Zone.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.uddS2Zone.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.uddS2Zone.DisplayMember = "Name";
            this.uddS2Zone.Location = new System.Drawing.Point(348, 206);
            this.uddS2Zone.Name = "uddS2Zone";
            this.uddS2Zone.Size = new System.Drawing.Size(104, 32);
            this.uddS2Zone.TabIndex = 12;
            this.uddS2Zone.ValueMember = "LocationID";
            this.uddS2Zone.Visible = false;
            this.uddS2Zone.RowSelected += new Infragistics.Win.UltraWinGrid.RowSelectedEventHandler(this.OnS2ZoneSelected);
            // 
            // uddDay
            // 
            this.uddDay.Cursor = System.Windows.Forms.Cursors.Default;
            ultraGridColumn110.Header.VisiblePosition = 0;
            ultraGridColumn110.Hidden = true;
            ultraGridColumn111.Header.Caption = "Day";
            ultraGridColumn111.Header.VisiblePosition = 1;
            ultraGridColumn111.Width = 48;
            ultraGridBand5.Columns.AddRange(new object[] {
            ultraGridColumn110,
            ultraGridColumn111});
            this.uddDay.DisplayLayout.BandsSerializer.Add(ultraGridBand5);
            this.uddDay.Location = new System.Drawing.Point(494, 206);
            this.uddDay.Name = "uddDay";
            this.uddDay.Size = new System.Drawing.Size(107, 32);
            this.uddDay.TabIndex = 13;
            this.uddDay.Visible = false;
            // 
            // stbMain
            // 
            this.stbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.stbMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stbMain.Location = new System.Drawing.Point(0, 258);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(1018, 24);
            this.stbMain.StatusText = "";
            this.stbMain.TabIndex = 15;
            this.stbMain.TerminalText = "Terminal";
            // 
            // grdTemplates
            // 
            this.grdTemplates.ContextMenuStrip = this.csMain;
            this.grdTemplates.DataSource = this.mTemplates;
            appearance18.BackColor = System.Drawing.SystemColors.Window;
            appearance18.FontData.Name = "Verdana";
            appearance18.FontData.SizeInPoints = 8F;
            appearance18.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance18.TextHAlignAsString = "Left";
            this.grdTemplates.DisplayLayout.Appearance = appearance18;
            ultraGridBand6.ColHeaderLines = 3;
            ultraGridColumn112.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn112.Header.VisiblePosition = 0;
            ultraGridColumn112.Hidden = true;
            ultraGridColumn113.Header.VisiblePosition = 1;
            ultraGridColumn113.Hidden = true;
            ultraGridColumn114.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn114.Header.Caption = "Term";
            ultraGridColumn114.Header.VisiblePosition = 2;
            ultraGridColumn114.Width = 96;
            ultraGridColumn115.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn115.Header.Caption = "Day";
            ultraGridColumn115.Header.VisiblePosition = 3;
            ultraGridColumn115.Width = 48;
            ultraGridColumn116.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn116.Header.VisiblePosition = 9;
            ultraGridColumn116.Hidden = true;
            ultraGridColumn117.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn117.Header.VisiblePosition = 8;
            ultraGridColumn117.Width = 192;
            ultraGridColumn118.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn118.Header.Caption = "Close\r\nDay";
            ultraGridColumn118.Header.VisiblePosition = 10;
            ultraGridColumn118.Width = 48;
            ultraGridColumn119.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn119.Format = "HH:mm";
            ultraGridColumn119.Header.Caption = "Close\r\nTime";
            ultraGridColumn119.Header.VisiblePosition = 11;
            ultraGridColumn119.MaskInput = "{LOC}hh:mm";
            ultraGridColumn119.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
            ultraGridColumn119.Width = 48;
            ultraGridColumn120.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn120.Header.Caption = "Dep\r\nDay";
            ultraGridColumn120.Header.VisiblePosition = 12;
            ultraGridColumn120.Width = 48;
            ultraGridColumn121.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn121.Format = "HH:mm";
            ultraGridColumn121.Header.Caption = "Dep\r\nTime";
            ultraGridColumn121.Header.VisiblePosition = 13;
            ultraGridColumn121.MaskInput = "{LOC}hh:mm";
            ultraGridColumn121.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
            ultraGridColumn121.Width = 48;
            ultraGridColumn122.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn122.Header.Caption = "Man?";
            ultraGridColumn122.Header.VisiblePosition = 18;
            ultraGridColumn122.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            ultraGridColumn123.Header.Caption = "Act?";
            ultraGridColumn123.Header.VisiblePosition = 19;
            ultraGridColumn123.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            ultraGridColumn123.Width = 48;
            ultraGridColumn124.Header.VisiblePosition = 38;
            ultraGridColumn124.Hidden = true;
            ultraGridColumn125.Header.VisiblePosition = 40;
            ultraGridColumn125.Hidden = true;
            ultraGridColumn126.Header.VisiblePosition = 39;
            ultraGridColumn126.Hidden = true;
            ultraGridColumn127.Header.VisiblePosition = 36;
            ultraGridColumn127.Hidden = true;
            ultraGridColumn128.Header.VisiblePosition = 37;
            ultraGridColumn128.Hidden = true;
            ultraGridColumn129.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn129.Header.Caption = "Zone";
            ultraGridColumn129.Header.VisiblePosition = 4;
            ultraGridColumn129.Width = 60;
            ultraGridColumn130.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn130.Header.VisiblePosition = 5;
            ultraGridColumn130.MaskInput = "999";
            ultraGridColumn130.MaxLength = 3;
            ultraGridColumn130.Width = 36;
            ultraGridColumn131.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn131.Header.VisiblePosition = 6;
            ultraGridColumn131.Hidden = true;
            ultraGridColumn132.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn132.Header.Caption = "Ag#";
            ultraGridColumn132.Header.VisiblePosition = 7;
            ultraGridColumn132.Width = 36;
            ultraGridColumn133.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn133.Header.Caption = "Arr\r\nDay";
            ultraGridColumn133.Header.VisiblePosition = 14;
            ultraGridColumn133.Width = 48;
            ultraGridColumn134.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn134.Format = "HH:mm";
            ultraGridColumn134.Header.Caption = "Arr\r\nTime";
            ultraGridColumn134.Header.VisiblePosition = 15;
            ultraGridColumn134.MaskInput = "{LOC}hh:mm";
            ultraGridColumn134.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
            ultraGridColumn134.Width = 48;
            ultraGridColumn135.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn135.Header.Caption = "OFD\r\nDay";
            ultraGridColumn135.Header.VisiblePosition = 16;
            ultraGridColumn135.Width = 48;
            ultraGridColumn136.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn136.Header.VisiblePosition = 17;
            ultraGridColumn136.MaxLength = 15;
            ultraGridColumn136.Width = 96;
            ultraGridColumn137.Header.VisiblePosition = 30;
            ultraGridColumn137.Hidden = true;
            ultraGridColumn138.Header.VisiblePosition = 32;
            ultraGridColumn138.Hidden = true;
            ultraGridColumn139.Header.VisiblePosition = 31;
            ultraGridColumn139.Hidden = true;
            ultraGridColumn140.Header.VisiblePosition = 28;
            ultraGridColumn140.Hidden = true;
            ultraGridColumn141.Header.VisiblePosition = 29;
            ultraGridColumn141.Hidden = true;
            ultraGridColumn142.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn142.Header.Caption = "S2\r\nZone";
            ultraGridColumn142.Header.VisiblePosition = 20;
            ultraGridColumn142.Width = 60;
            ultraGridColumn143.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn143.Header.Caption = "S2\r\nTag";
            ultraGridColumn143.Header.VisiblePosition = 21;
            ultraGridColumn143.MaskInput = "999";
            ultraGridColumn143.MaxLength = 3;
            ultraGridColumn143.Width = 36;
            ultraGridColumn144.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn144.Header.VisiblePosition = 22;
            ultraGridColumn144.Hidden = true;
            ultraGridColumn145.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn145.Header.Caption = "S2\r\nAg#";
            ultraGridColumn145.Header.VisiblePosition = 23;
            ultraGridColumn145.Width = 36;
            ultraGridColumn146.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn146.Header.Caption = "S2\r\nArr\r\nDay";
            ultraGridColumn146.Header.VisiblePosition = 24;
            ultraGridColumn146.Width = 48;
            ultraGridColumn147.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn147.Format = "HH:mm";
            ultraGridColumn147.Header.Caption = "S2\r\nArr\r\nTime";
            ultraGridColumn147.Header.VisiblePosition = 25;
            ultraGridColumn147.MaskInput = "{LOC}hh:mm";
            ultraGridColumn147.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
            ultraGridColumn147.Width = 48;
            ultraGridColumn148.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn148.Header.Caption = "S2\r\nOFD\r\nDay";
            ultraGridColumn148.Header.VisiblePosition = 26;
            ultraGridColumn148.Width = 48;
            ultraGridColumn149.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn149.Header.Caption = "S2\r\nNotes";
            ultraGridColumn149.Header.VisiblePosition = 27;
            ultraGridColumn149.MaxLength = 15;
            ultraGridColumn150.Header.VisiblePosition = 33;
            ultraGridColumn150.Hidden = true;
            ultraGridColumn151.Header.VisiblePosition = 35;
            ultraGridColumn151.Hidden = true;
            ultraGridColumn152.Header.VisiblePosition = 34;
            ultraGridColumn152.Hidden = true;
            ultraGridBand6.Columns.AddRange(new object[] {
            ultraGridColumn112,
            ultraGridColumn113,
            ultraGridColumn114,
            ultraGridColumn115,
            ultraGridColumn116,
            ultraGridColumn117,
            ultraGridColumn118,
            ultraGridColumn119,
            ultraGridColumn120,
            ultraGridColumn121,
            ultraGridColumn122,
            ultraGridColumn123,
            ultraGridColumn124,
            ultraGridColumn125,
            ultraGridColumn126,
            ultraGridColumn127,
            ultraGridColumn128,
            ultraGridColumn129,
            ultraGridColumn130,
            ultraGridColumn131,
            ultraGridColumn132,
            ultraGridColumn133,
            ultraGridColumn134,
            ultraGridColumn135,
            ultraGridColumn136,
            ultraGridColumn137,
            ultraGridColumn138,
            ultraGridColumn139,
            ultraGridColumn140,
            ultraGridColumn141,
            ultraGridColumn142,
            ultraGridColumn143,
            ultraGridColumn144,
            ultraGridColumn145,
            ultraGridColumn146,
            ultraGridColumn147,
            ultraGridColumn148,
            ultraGridColumn149,
            ultraGridColumn150,
            ultraGridColumn151,
            ultraGridColumn152});
            ultraGridBand6.GroupHeaderLines = 3;
            ultraGridBand6.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdTemplates.DisplayLayout.BandsSerializer.Add(ultraGridBand6);
            appearance19.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance19.FontData.BoldAsString = "True";
            appearance19.FontData.Name = "Verdana";
            appearance19.FontData.SizeInPoints = 8F;
            appearance19.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance19.TextHAlignAsString = "Left";
            this.grdTemplates.DisplayLayout.CaptionAppearance = appearance19;
            this.grdTemplates.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
            this.grdTemplates.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdTemplates.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdTemplates.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
            appearance20.BackColor = System.Drawing.SystemColors.Control;
            appearance20.FontData.BoldAsString = "True";
            appearance20.FontData.Name = "Verdana";
            appearance20.FontData.SizeInPoints = 8F;
            appearance20.TextHAlignAsString = "Left";
            this.grdTemplates.DisplayLayout.Override.HeaderAppearance = appearance20;
            this.grdTemplates.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdTemplates.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance27.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdTemplates.DisplayLayout.Override.RowAppearance = appearance27;
            this.grdTemplates.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            this.grdTemplates.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdTemplates.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdTemplates.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdTemplates.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTemplates.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdTemplates.Location = new System.Drawing.Point(0, 49);
            this.grdTemplates.Name = "grdTemplates";
            this.grdTemplates.RowUpdateCancelAction = Infragistics.Win.UltraWinGrid.RowUpdateCancelAction.RetainDataAndActivation;
            this.grdTemplates.Size = new System.Drawing.Size(1018, 209);
            this.grdTemplates.TabIndex = 0;
            this.grdTemplates.Text = "Templates";
            this.grdTemplates.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdTemplates.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdTemplates.InitializeTemplateAddRow += new Infragistics.Win.UltraWinGrid.InitializeTemplateAddRowEventHandler(this.OnGridInitializeTemplateAddRow);
            this.grdTemplates.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnGridInitializeRow);
            this.grdTemplates.AfterRowUpdate += new Infragistics.Win.UltraWinGrid.RowEventHandler(this.OnGridAfterRowUpdate);
            this.grdTemplates.BeforeRowUpdate += new Infragistics.Win.UltraWinGrid.CancelableRowEventHandler(this.OnGridBeforeRowUpdate);
            this.grdTemplates.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.OnGridBeforeRowFilterDropDownPopulate);
            this.grdTemplates.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnGridInitializeLayout);
            this.grdTemplates.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnGridKeyUp);
            this.grdTemplates.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridSelectionChanged);
            // 
            // csMain
            // 
            this.csMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxRefresh,
            this.ctxSep1,
            this.ctxCut,
            this.ctxCopy,
            this.ctxPaste});
            this.csMain.Name = "ctxMain";
            this.csMain.Size = new System.Drawing.Size(114, 98);
            // 
            // ctxRefresh
            // 
            this.ctxRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.ctxRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxRefresh.Name = "ctxRefresh";
            this.ctxRefresh.Size = new System.Drawing.Size(113, 22);
            this.ctxRefresh.Text = "Refresh";
            this.ctxRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxSep1
            // 
            this.ctxSep1.Name = "ctxSep1";
            this.ctxSep1.Size = new System.Drawing.Size(110, 6);
            // 
            // ctxCut
            // 
            this.ctxCut.Image = global::Argix.Properties.Resources.Cut;
            this.ctxCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxCut.Name = "ctxCut";
            this.ctxCut.Size = new System.Drawing.Size(113, 22);
            this.ctxCut.Text = "Cut";
            this.ctxCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxCopy
            // 
            this.ctxCopy.Image = global::Argix.Properties.Resources.Copy;
            this.ctxCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxCopy.Name = "ctxCopy";
            this.ctxCopy.Size = new System.Drawing.Size(113, 22);
            this.ctxCopy.Text = "Copy";
            this.ctxCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxPaste
            // 
            this.ctxPaste.Image = global::Argix.Properties.Resources.Paste;
            this.ctxPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxPaste.Name = "ctxPaste";
            this.ctxPaste.Size = new System.Drawing.Size(113, 22);
            this.ctxPaste.Text = "Paste";
            this.ctxPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mTemplates
            // 
            this.mTemplates.DataMember = "TemplateTable";
            this.mTemplates.DataSource = typeof(Argix.AgentLineHaul.TemplateDS);
            // 
            // mTemplateDS
            // 
            this.mTemplateDS.DataSetName = "TemplateDS";
            this.mTemplateDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tsMain
            // 
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnOpen,
            this.btnSep1,
            this.btnSave,
            this.btnExport,
            this.btnSep2,
            this.btnPrint,
            this.btnPreview,
            this.btnSep3,
            this.btnCut,
            this.btnCopy,
            this.btnPaste,
            this.btnSearch,
            this.btnSep4,
            this.btnRefresh});
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(1018, 25);
            this.tsMain.Stretch = true;
            this.tsMain.TabIndex = 116;
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(23, 22);
            this.btnNew.ToolTipText = "New...";
            this.btnNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = global::Argix.Properties.Resources.Open;
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23, 22);
            this.btnOpen.ToolTipText = "Open...";
            this.btnOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep1
            // 
            this.btnSep1.Name = "btnSep1";
            this.btnSep1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.ToolTipText = "Save";
            this.btnSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnExport
            // 
            this.btnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(23, 22);
            this.btnExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep2
            // 
            this.btnSep2.Name = "btnSep2";
            this.btnSep2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(23, 22);
            this.btnPrint.ToolTipText = "Print ship schedule...";
            this.btnPrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnPreview
            // 
            this.btnPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPreview.Image = ((System.Drawing.Image)(resources.GetObject("btnPreview.Image")));
            this.btnPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(23, 22);
            this.btnPreview.ToolTipText = "Print preview ship schedule...";
            this.btnPreview.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep3
            // 
            this.btnSep3.Name = "btnSep3";
            this.btnSep3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCut
            // 
            this.btnCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCut.Image = ((System.Drawing.Image)(resources.GetObject("btnCut.Image")));
            this.btnCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(23, 22);
            this.btnCut.ToolTipText = "Cut text";
            this.btnCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnCopy
            // 
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(23, 22);
            this.btnCopy.ToolTipText = "Copy text";
            this.btnCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnPaste
            // 
            this.btnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPaste.Image = ((System.Drawing.Image)(resources.GetObject("btnPaste.Image")));
            this.btnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(23, 22);
            this.btnPaste.ToolTipText = "Paste text";
            this.btnPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSearch
            // 
            this.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSearch.Image = global::Argix.Properties.Resources.Find;
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(23, 22);
            this.btnSearch.ToolTipText = "Find";
            this.btnSearch.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep4
            // 
            this.btnSep4.Name = "btnSep4";
            this.btnSep4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23, 22);
            this.btnRefresh.ToolTipText = "Refresh ship schedule";
            this.btnRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuView,
            this.mnuTools,
            this.mnuHelp});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(1018, 24);
            this.msMain.TabIndex = 118;
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileNew,
            this.mnuFileOpen,
            this.mnuFileSep1,
            this.mnuFileSave,
            this.mnuFileSaveAs,
            this.mnuFileExport,
            this.mnuFileSep2,
            this.mnuFileSetup,
            this.mnuFilePrint,
            this.mnuFilePreview,
            this.mnuFileSep3,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.mnuFileNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnuFileNew.Size = new System.Drawing.Size(152, 22);
            this.mnuFileNew.Text = "&New...";
            this.mnuFileNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Image = global::Argix.Properties.Resources.Open;
            this.mnuFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(152, 22);
            this.mnuFileOpen.Text = "&Open...";
            this.mnuFileOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep1
            // 
            this.mnuFileSep1.Name = "mnuFileSep1";
            this.mnuFileSep1.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Image = global::Argix.Properties.Resources.Save;
            this.mnuFileSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.Size = new System.Drawing.Size(152, 22);
            this.mnuFileSave.Text = "&Save";
            this.mnuFileSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.Size = new System.Drawing.Size(152, 22);
            this.mnuFileSaveAs.Text = "Save &As...";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileExport
            // 
            this.mnuFileExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.mnuFileExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileExport.Name = "mnuFileExport";
            this.mnuFileExport.Size = new System.Drawing.Size(152, 22);
            this.mnuFileExport.Text = "&Export...";
            this.mnuFileExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep2
            // 
            this.mnuFileSep2.Name = "mnuFileSep2";
            this.mnuFileSep2.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuFileSetup
            // 
            this.mnuFileSetup.Image = ((System.Drawing.Image)(resources.GetObject("mnuFileSetup.Image")));
            this.mnuFileSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSetup.Name = "mnuFileSetup";
            this.mnuFileSetup.Size = new System.Drawing.Size(152, 22);
            this.mnuFileSetup.Text = "Page Set&up...";
            this.mnuFileSetup.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFilePrint
            // 
            this.mnuFilePrint.Image = ((System.Drawing.Image)(resources.GetObject("mnuFilePrint.Image")));
            this.mnuFilePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePrint.Name = "mnuFilePrint";
            this.mnuFilePrint.Size = new System.Drawing.Size(152, 22);
            this.mnuFilePrint.Text = "&Print...";
            this.mnuFilePrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFilePreview
            // 
            this.mnuFilePreview.Image = ((System.Drawing.Image)(resources.GetObject("mnuFilePreview.Image")));
            this.mnuFilePreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePreview.Name = "mnuFilePreview";
            this.mnuFilePreview.Size = new System.Drawing.Size(152, 22);
            this.mnuFilePreview.Text = "Print P&review...";
            this.mnuFilePreview.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep3
            // 
            this.mnuFileSep3.Name = "mnuFileSep3";
            this.mnuFileSep3.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(152, 22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditCut,
            this.mnuEditCopy,
            this.mnuEditPaste,
            this.mnuEditSep1,
            this.mnuEditSearch});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(39, 20);
            this.mnuEdit.Text = "Edit";
            // 
            // mnuEditCut
            // 
            this.mnuEditCut.Image = global::Argix.Properties.Resources.Cut;
            this.mnuEditCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditCut.Name = "mnuEditCut";
            this.mnuEditCut.Size = new System.Drawing.Size(109, 22);
            this.mnuEditCut.Text = "Cu&t";
            this.mnuEditCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditCopy
            // 
            this.mnuEditCopy.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditCopy.Image")));
            this.mnuEditCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditCopy.Name = "mnuEditCopy";
            this.mnuEditCopy.Size = new System.Drawing.Size(109, 22);
            this.mnuEditCopy.Text = "&Copy";
            this.mnuEditCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditPaste
            // 
            this.mnuEditPaste.Image = global::Argix.Properties.Resources.Paste;
            this.mnuEditPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditPaste.Name = "mnuEditPaste";
            this.mnuEditPaste.Size = new System.Drawing.Size(109, 22);
            this.mnuEditPaste.Text = "&Paste";
            this.mnuEditPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditSep1
            // 
            this.mnuEditSep1.Name = "mnuEditSep1";
            this.mnuEditSep1.Size = new System.Drawing.Size(106, 6);
            // 
            // mnuEditSearch
            // 
            this.mnuEditSearch.Image = global::Argix.Properties.Resources.Find;
            this.mnuEditSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditSearch.Name = "mnuEditSearch";
            this.mnuEditSearch.Size = new System.Drawing.Size(109, 22);
            this.mnuEditSearch.Text = "&Search";
            this.mnuEditSearch.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewRefresh,
            this.mnuViewSep1,
            this.mnuViewToolbar,
            this.mnuViewStatusBar});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(44, 20);
            this.mnuView.Text = "View";
            // 
            // mnuViewRefresh
            // 
            this.mnuViewRefresh.Image = ((System.Drawing.Image)(resources.GetObject("mnuViewRefresh.Image")));
            this.mnuViewRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewRefresh.Name = "mnuViewRefresh";
            this.mnuViewRefresh.Size = new System.Drawing.Size(126, 22);
            this.mnuViewRefresh.Text = "&Refresh";
            this.mnuViewRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewSep1
            // 
            this.mnuViewSep1.Name = "mnuViewSep1";
            this.mnuViewSep1.Size = new System.Drawing.Size(123, 6);
            // 
            // mnuViewToolbar
            // 
            this.mnuViewToolbar.Name = "mnuViewToolbar";
            this.mnuViewToolbar.Size = new System.Drawing.Size(126, 22);
            this.mnuViewToolbar.Text = "Toolbar";
            this.mnuViewToolbar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewStatusBar
            // 
            this.mnuViewStatusBar.Name = "mnuViewStatusBar";
            this.mnuViewStatusBar.Size = new System.Drawing.Size(126, 22);
            this.mnuViewStatusBar.Text = "Status Bar";
            this.mnuViewStatusBar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsConfig});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(48, 20);
            this.mnuTools.Text = "Tools";
            // 
            // mnuToolsConfig
            // 
            this.mnuToolsConfig.Name = "mnuToolsConfig";
            this.mnuToolsConfig.Size = new System.Drawing.Size(157, 22);
            this.mnuToolsConfig.Text = "&Configuration...";
            this.mnuToolsConfig.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpAbout,
            this.mnuHelpSep1});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(251, 22);
            this.mnuHelpAbout.Text = "&About Ship Schedule Templates...";
            this.mnuHelpAbout.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuHelpSep1
            // 
            this.mnuHelpSep1.Name = "mnuHelpSep1";
            this.mnuHelpSep1.Size = new System.Drawing.Size(248, 6);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1018, 282);
            this.Controls.Add(this.uddS2Zone);
            this.Controls.Add(this.uddZone);
            this.Controls.Add(this.uddDay);
            this.Controls.Add(this.uddCarrier);
            this.Controls.Add(this.uddSortCenter);
            this.Controls.Add(this.grdTemplates);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.Controls.Add(this.stbMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ship Schedule Templates";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(mS2Zones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(mZones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(mCarriers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(mSortCenters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddSortCenter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddCarrier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddZone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddS2Zone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTemplates)).EndInit();
            this.csMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mTemplates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTemplateDS)).EndInit();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Load conditions
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
                    this.mnuViewToolbar.Checked = this.tsMain.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.Toolbar);
                    this.mnuViewStatusBar.Checked = this.stbMain.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.StatusBar);
                    App.CheckVersion();
                }
                catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
                #endregion
				#region Set tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
				//this.mToolTip.SetToolTip(this.cboTerminals, "Select an enterprise terminal for the TL and Agent Summary views.");
				#endregion
				
				//Set control defaults
				#region Grid loading
				this.grdTemplates.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdTemplates.DisplayLayout.Bands[0].Columns["SortCenter"].SortIndicator = SortIndicator.Ascending;

                this.uddSortCenter.DataSource = ShipScheduleProxy.GetShippersAndTerminals();
				this.uddSortCenter.DisplayMember = "Description";
                this.uddSortCenter.ValueMember = "LocationID";
                this.uddSortCenter.DisplayLayout.Bands[0].Columns["Description"].SortIndicator = Infragistics.Win.UltraWinGrid.SortIndicator.Ascending;

                this.uddCarrier.DataSource = ShipScheduleProxy.GetCarriers();
                this.uddCarrier.DisplayMember = "Name";
                this.uddCarrier.ValueMember = "CarrierServiceID";
                this.uddCarrier.DisplayLayout.Bands[0].Columns["Name"].SortIndicator = Infragistics.Win.UltraWinGrid.SortIndicator.Ascending;

                this.uddZone.DataSource = ShipScheduleProxy.GetAgents();
                this.uddZone.DisplayMember = "Name";
                this.uddZone.ValueMember = "LocationID";
                this.uddZone.DisplayLayout.Bands[0].Columns["Name"].SortIndicator = Infragistics.Win.UltraWinGrid.SortIndicator.Ascending;
                this.uddZone.DisplayLayout.Bands[0].Columns["Name"].Width = 60;

                this.uddS2Zone.DataSource = ShipScheduleProxy.GetAgents();
				this.uddS2Zone.DisplayMember = "Name";
                this.uddS2Zone.ValueMember = "LocationID";
                this.uddS2Zone.DisplayLayout.Bands[0].Columns["Name"].SortIndicator = Infragistics.Win.UltraWinGrid.SortIndicator.Ascending;
                this.uddS2Zone.DisplayLayout.Bands[0].Columns["Name"].Width = 60;

                this.uddDay.DataSource = ShipScheduleProxy.GetDaysOfWeek();
                this.uddDay.DataMember = "SelectionListTable";
				this.uddDay.DisplayMember = "Description";
				this.uddDay.ValueMember = "ID";
				#endregion
                TerminalInfo t = ShipScheduleProxy.GetTerminalInfo();
                this.stbMain.SetTerminalPanel(t.TerminalID.ToString(), t.Description);
                this.stbMain.User1Panel.Width = 144;
                this.mnuViewRefresh.PerformClick();
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnFormClosing(object sender, System.ComponentModel.CancelEventArgs e) {
			//Event handler for form closing event
            if(!e.Cancel) {
                global::Argix.Properties.Settings.Default.WindowState = this.WindowState;
                global::Argix.Properties.Settings.Default.Location = this.Location;
                global::Argix.Properties.Settings.Default.Size = this.Size;
                global::Argix.Properties.Settings.Default.Toolbar = this.mnuViewToolbar.Checked;
                global::Argix.Properties.Settings.Default.StatusBar = this.mnuViewStatusBar.Checked;
                global::Argix.Properties.Settings.Default.LastVersion = App.Version;
                global::Argix.Properties.Settings.Default.Save();
            }
		}
		private void OnFormResize(object sender, System.EventArgs e) { 
			//Event handler for form resized event
		}
		private void OnSortCenterBeforeDropDown(object sender, System.ComponentModel.CancelEventArgs e) {
			//
            this.uddSortCenter.DisplayLayout.Bands[0].Columns["Description"].Width = this.grdTemplates.DisplayLayout.Bands[0].Columns["SortCenter"].Width;
		}
		private void OnSortCenterSelected(object sender, Infragistics.Win.UltraWinGrid.RowSelectedEventArgs e) {
			//
			if(e.Row != null)
                this.grdTemplates.ActiveRow.Cells["SortCenterID"].Value = e.Row.Cells["LocationID"].Text;
		}
		private void OnCarrierBeforeDropDown(object sender, System.ComponentModel.CancelEventArgs e) {
			//
            this.uddCarrier.DisplayLayout.Bands[0].Columns["Name"].Width = this.grdTemplates.DisplayLayout.Bands[0].Columns["Carrier"].Width;
        }
		private void OnCarrierSelected(object sender, Infragistics.Win.UltraWinGrid.RowSelectedEventArgs e) {
			//
			if(e.Row != null)
                this.grdTemplates.ActiveRow.Cells["CarrierServiceID"].Value = e.Row.Cells["CarrierServiceID"].Text;
        }
        private void OnZoneSelected(object sender, Infragistics.Win.UltraWinGrid.RowSelectedEventArgs e) {
            //
            if (e.Row != null) {
                this.grdTemplates.ActiveRow.Cells["AgentNumber"].Value = e.Row.Cells["Number"].Text;
                this.grdTemplates.ActiveRow.Cells["AgentTerminalID"].Value = e.Row.Cells["LocationID"].Text;
            }
        }
        private void OnS2ZoneSelected(object sender, Infragistics.Win.UltraWinGrid.RowSelectedEventArgs e) {
            //
            if (e.Row != null) {
                this.grdTemplates.ActiveRow.Cells["S2AgentNumber"].Value = e.Row.Cells["Number"].Text;
                this.grdTemplates.ActiveRow.Cells["S2AgentTerminalID"].Value = e.Row.Cells["LocationID"].Text;
            }
        }
        #region Grid Support: OnGridInitializeLayout(), OnGridInitializeTemplateAddRow(), OnGridInitializeRow(), GridSelectionChanged(), OnGridKeyUp(), GridMouseDown(), OnGridBeforeRowFilterDropDownPopulate(), OnGridBeforeRowUpdate(), OnGridAfterRowUpdate()
        private void OnGridInitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
			//
			try {
				Infragistics.Win.UltraWinGrid.UltraGridBand band = e.Layout.Bands[0];
				band.Columns["ScheduledCloseDateOffset"].ValueList = e.Layout.ValueLists["schCloseDaysOffset"];
				band.Columns["ScheduledDepartureDateOffset"].ValueList = e.Layout.ValueLists["schDepartDaysOffset"];
				band.Columns["ScheduledArrivalDateOffset"].ValueList = e.Layout.ValueLists["schArrivalDaysOffset"];
				band.Columns["ScheduledOFD1Offset"].ValueList = e.Layout.ValueLists["schOFD1DaysOffset"];
				band.Columns["S2ScheduledArrivalDateOffset"].ValueList = e.Layout.ValueLists["schArrivalDaysOffset"];
				band.Columns["S2ScheduledOFD1Offset"].ValueList = e.Layout.ValueLists["schOFD1DaysOffset"];
			
				band.Columns["SortCenter"].ValueList = this.uddSortCenter;
				band.Columns["Carrier"].ValueList = this.uddCarrier;
				band.Columns["MainZone"].ValueList = this.uddZone;
				band.Columns["S2MainZone"].ValueList = this.uddS2Zone;
				band.Columns["DayOfTheWeek"].ValueList = this.uddDay;

				//select the first row
				if(this.grdTemplates.Rows.Count > 0) this.grdTemplates.ActiveRow = this.grdTemplates.Rows[0];
			}
			catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
		}
        private void OnGridInitializeTemplateAddRow(object sender,InitializeTemplateAddRowEventArgs e) {
            //Event handler for initialization of the add row
            e.TemplateAddRow.Cells["SortCenter"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["DayOfTheWeek"].Activation =  Activation.AllowEdit;
            e.TemplateAddRow.Cells["MainZone"].Activation =  Activation.AllowEdit;
            e.TemplateAddRow.Cells["Tag"].Activation =  Activation.AllowEdit;
            e.TemplateAddRow.Cells["AgentNumber"].Activation =  Activation.NoEdit;
            e.TemplateAddRow.Cells["Carrier"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["ScheduledCloseDateOffset"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["ScheduledCloseTime"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["ScheduledDepartureDateOffset"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["ScheduledDepartureTime"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["IsMandatory"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["IsActive"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["S2MainZone"].Activation =  Activation.AllowEdit;
            e.TemplateAddRow.Cells["S2Tag"].Activation =  Activation.AllowEdit;
            e.TemplateAddRow.Cells["S2AgentNumber"].Activation =  Activation.NoEdit;
            e.TemplateAddRow.Cells["S2ScheduledArrivalDateOffset"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["S2ScheduledArrivalTime"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["S2ScheduledOFD1Offset"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["S2Notes"].Activation = Activation.AllowEdit;

            e.TemplateAddRow.Cells["IsMandatory"].Value = false;
            e.TemplateAddRow.Cells["IsActive"].Value = true;
            e.TemplateAddRow.Cells["StopNumber"].Value = "01";
            e.TemplateAddRow.Cells["S2StopNumber"].Value = "02";
        }
        private void OnGridInitializeRow(object sender,Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e) {
            //
            try {
                e.Row.Cells["SortCenter"].Activation = (e.Row.IsAddRow) ? Activation.AllowEdit : Activation.NoEdit;
                e.Row.Cells["DayOfTheWeek"].Activation = (e.Row.IsAddRow) ? Activation.AllowEdit : Activation.NoEdit;
                e.Row.Cells["MainZone"].Activation = (e.Row.IsAddRow) ? Activation.AllowEdit : Activation.NoEdit;
                e.Row.Cells["Tag"].Activation = (e.Row.IsAddRow) ? Activation.AllowEdit : Activation.NoEdit;
                e.Row.Cells["AgentNumber"].Activation = Activation.NoEdit;
                e.Row.Cells["Carrier"].Activation = Activation.AllowEdit;
                e.Row.Cells["ScheduledCloseDateOffset"].Activation = Activation.AllowEdit;
                e.Row.Cells["ScheduledCloseTime"].Activation = Activation.AllowEdit;
                e.Row.Cells["ScheduledDepartureDateOffset"].Activation = Activation.AllowEdit;
                e.Row.Cells["ScheduledDepartureTime"].Activation = Activation.AllowEdit;
                e.Row.Cells["IsMandatory"].Activation = Activation.AllowEdit;
                e.Row.Cells["IsActive"].Activation = Activation.AllowEdit;
                e.Row.Cells["S2MainZone"].Activation = (e.Row.IsAddRow) ? Activation.AllowEdit : Activation.NoEdit;
                e.Row.Cells["S2Tag"].Activation = (e.Row.IsAddRow) ? Activation.AllowEdit : Activation.NoEdit;
                e.Row.Cells["S2AgentNumber"].Activation = Activation.NoEdit;
                e.Row.Cells["S2ScheduledArrivalDateOffset"].Activation = (e.Row.Cells["S2MainZone"].Text.Trim().Length > 0) ? Activation.AllowEdit : Activation.NoEdit;
                e.Row.Cells["S2ScheduledArrivalTime"].Activation = (e.Row.Cells["S2MainZone"].Text.Trim().Length > 0) ? Activation.AllowEdit : Activation.NoEdit;
                e.Row.Cells["S2ScheduledOFD1Offset"].Activation = (e.Row.Cells["S2MainZone"].Text.Trim().Length > 0) ? Activation.AllowEdit : Activation.NoEdit;
                e.Row.Cells["S2Notes"].Activation = (e.Row.Cells["S2MainZone"].Text.Trim().Length > 0) ? Activation.AllowEdit : Activation.NoEdit;
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
        }
        private void OnGridSelectionChanged(object sender,Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
            //Event handler for after selection changes
            setUserServices();
        }
        private void OnGridKeyUp(object sender,System.Windows.Forms.KeyEventArgs e) {
            //Event handler for key up event
            UltraGrid grid = (UltraGrid)sender;
            if(e.KeyCode == System.Windows.Forms.Keys.Enter && grid.ActiveRow != null) {
                grid.ActiveRow.Update();
                e.Handled = true;
            }
            //else if(e.KeyCode == System.Windows.Forms.Keys.Delete) {
            //    this.ctxCDelete.PerformClick();
            //    e.Handled = true;
            //}
            else
                e.Handled = false;
        }
        private void OnGridMouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event
            try {
                //Set menu and toolbar services
                UltraGrid grid = (UltraGrid)sender;
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
        private void OnGridBeforeRowFilterDropDownPopulate(object sender,Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs e) {
			//Removes only (Blanks) and Non Blanks default filter
			e.ValueList.ValueListItems.Remove(3);
			e.ValueList.ValueListItems.Remove(2);
			e.ValueList.ValueListItems.Remove(1);
		}
        private void OnGridBeforeRowUpdate(object sender,Infragistics.Win.UltraWinGrid.CancelableRowEventArgs e) {
			try {
                //Cancel the event, but leave the row data (i.e. don't call row.CancelUpdate())
                e.Cancel = !validateRules(e);
            }
			catch(Exception) { e.Cancel = true; }
		}
		private void OnGridAfterRowUpdate(object sender, Infragistics.Win.UltraWinGrid.RowEventArgs e) {
            try {
                //There is no selected row when updating- at a cell level
                ShipScheduleTemplate template = new ShipScheduleTemplate();
                template.TemplateID = e.Row.Cells["TemplateID"].Value.ToString();
                template.SortCenterID = Convert.ToInt64(e.Row.Cells["SortCenterID"].Value);
                template.DayOfTheWeek = Convert.ToByte(e.Row.Cells["DayOfTheWeek"].Value);
                template.CarrierServiceID = Convert.ToInt64(e.Row.Cells["CarrierServiceID"].Value);
                template.ScheduledCloseDateOffset = Convert.ToByte(e.Row.Cells["ScheduledCloseDateOffset"].Value);
                template.ScheduledCloseTime = Convert.ToDateTime(e.Row.Cells["ScheduledCloseTime"].Value);
                template.ScheduledDepartureDateOffset = Convert.ToByte(e.Row.Cells["ScheduledDepartureDateOffset"].Value);
                template.ScheduledDepartureTime = Convert.ToDateTime(e.Row.Cells["ScheduledDepartureTime"].Value);
                template.IsMandatory = Convert.ToByte(e.Row.Cells["IsMandatory"].Value);
                template.IsActive = Convert.ToByte(e.Row.Cells["IsActive"].Value);
                template.TemplateLastUpdated = DateTime.Now;
                template.TemplateUser = Environment.UserName;
                template.TemplateRowVersion = e.Row.Cells["TemplateRowVersion"].Value.ToString();

                template.StopID = e.Row.Cells["StopID"].Value.ToString();
                template.StopNumber = e.Row.Cells["StopNumber"].Value.ToString();
                template.AgentTerminalID = Convert.ToInt64(e.Row.Cells["AgentTerminalID"].Value.ToString());
                template.MainZone = e.Row.Cells["MainZone"].Value.ToString().Trim();
                template.Tag = e.Row.Cells["Tag"].Value.ToString();
                template.Notes = e.Row.Cells["Notes"].Value.ToString();
                template.ScheduledArrivalDateOffset = Convert.ToByte(e.Row.Cells["ScheduledArrivalDateOffset"].Value.ToString());
                template.ScheduledArrivalTime = Convert.ToDateTime(e.Row.Cells["ScheduledArrivalTime"].Value.ToString());
                template.ScheduledOFD1Offset = Convert.ToByte(e.Row.Cells["ScheduledOFD1Offset"].Value.ToString());
                template.Stop1LastUpdated = DateTime.Now;
                template.Stop1User = Environment.UserName;
                template.Stop1RowVersion = e.Row.Cells["Stop1RowVersion"].Value.ToString();

                template.S2StopID = e.Row.Cells["S2StopID"].Value.ToString().Trim();
                template.S2StopNumber = e.Row.Cells["S2StopNumber"].Value.ToString().Trim();
                if(e.Row.Cells["S2AgentTerminalID"].Value.ToString().Length > 0) template.S2AgentTerminalID = Convert.ToInt64(e.Row.Cells["S2AgentTerminalID"].Value.ToString());
                template.S2MainZone = e.Row.Cells["S2MainZone"].Value.ToString().Trim();
                template.S2Tag = e.Row.Cells["S2Tag"].Value.ToString().Trim();
                template.S2Notes = e.Row.Cells["S2Notes"].Value.ToString().Trim();
                if(e.Row.Cells["S2ScheduledArrivalDateOffset"].Value.ToString().Length > 0) template.S2ScheduledArrivalDateOffset = Convert.ToByte(e.Row.Cells["S2ScheduledArrivalDateOffset"].Value.ToString());
                if(e.Row.Cells["S2ScheduledArrivalTime"].Value.ToString().Length > 0) template.S2ScheduledArrivalTime = Convert.ToDateTime(e.Row.Cells["S2ScheduledArrivalTime"].Value.ToString());
                if(e.Row.Cells["S2ScheduledOFD1Offset"].Value.ToString().Length > 0) template.S2ScheduledOFD1Offset = Convert.ToByte(e.Row.Cells["S2ScheduledOFD1Offset"].Value.ToString());
                template.Stop2LastUpdated = DateTime.Now;
                template.Stop2User = Environment.UserName;
                template.Stop2RowVersion = e.Row.Cells["Stop2RowVersion"].Value.ToString();
                if(template.TemplateID.Trim().Length == 0) {
                    //Add new
                    string templateID = ShipScheduleProxy.AddTemplate(template);
                    if(templateID.Length == 0) 
                        App.ReportError(new ApplicationException("The new template was not added."),true);
                    else 
                        MessageBox.Show(this,"A new template #" + templateID + " has been added.",App.Product,MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else {
                    //Update existing
                    if(!ShipScheduleProxy.UpdateTemplate(template)) 
                        App.ReportError(new ApplicationException("The template was not updated."),true);
                    else 
                        MessageBox.Show(this,"Existing template #" + template.TemplateID + " has been updated.",App.Product,MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { this.mnuViewRefresh.PerformClick(); }
        }
		#endregion
        #region User Services: OnItemClick(), OnHelpMenuClick()
        private void OnItemClick(object sender, System.EventArgs e) {
			//Event handler for menu selection
			try {
                ToolStripItem item = (ToolStripItem)sender;
                switch(item.Name) {
                    case "mnuFileNew":  case "btnNew":  break;
                    case "mnuFileOpen": case "btnOpen": break;
                    case "mnuFileSave": case "btnSave": break;
					case "mnuFileSaveAs":			
						SaveFileDialog dlgSave = new SaveFileDialog();
						dlgSave.AddExtension = true;
						dlgSave.Filter = "Export Files (*.xml) | *.xml | Excel Files (*.xls) | *.xls";
						dlgSave.FilterIndex = 0;
						dlgSave.Title = "Save Templates As...";
						dlgSave.FileName = "Ship Schedule Templates";
						dlgSave.OverwritePrompt = true;
						if(dlgSave.ShowDialog(this)==DialogResult.OK) {
							this.Cursor = Cursors.WaitCursor;
							this.mMessageMgr.AddMessage("Saving to " + dlgSave.FileName + "...");
							Application.DoEvents();
                            if(dlgSave.FileName.EndsWith("xls")) {
                                new Argix.ExcelFormat().Transform(ShipScheduleProxy.GetTemplates(),dlgSave.FileName);
                            }
                            else {
                                FileStream fs = new FileStream(dlgSave.FileName, FileMode.Create, FileAccess.Write);
                                DataContractSerializer dcs = new DataContractSerializer(typeof(ShipScheduleTemplate));
                                dcs.WriteObject(fs, this.mTemplates.DataSource);
                                fs.Flush();
                                fs.Close();
                            }
                        }
						break;
					case "mnuFileExport":
                    case "btnExport":
                        this.Cursor = Cursors.WaitCursor;
                        DataSet ds = new DataSet();
                        ds.ReadXml(ShipScheduleProxy.GetExportDefinition(),XmlReadMode.Auto);
						this.mMessageMgr.AddMessage("Exporting selected schedules to Microsoft Excel...");
                        ds.Merge(ShipScheduleProxy.GetTemplates(),true,MissingSchemaAction.Ignore);
                        new Argix.ExcelFormat().Transform(ds);
						break;
					case "mnuFileSetup":    UltraGridPrinter.PageSettings(); break;
					case "mnuFilePrint":	UltraGridPrinter.Print(this.grdTemplates, "SHIP SCHEDULE TEMPLATES                          " +  DateTime.Today.ToString("dd-MMM-yyyy"), true); break;
                    case "btnPrint":        UltraGridPrinter.Print(this.grdTemplates,"SHIP SCHEDULE TEMPLATES                          " +  DateTime.Today.ToString("dd-MMM-yyyy"),false); break;
                    case "mnuFilePreview":  UltraGridPrinter.PrintPreview(this.grdTemplates,"SHIP SCHEDULE TEMPLATES                          " +  DateTime.Today.ToString("dd-MMM-yyyy")); break;
					case "mnuFileExit":		this.Close(); Application.Exit(); break;
                    case "mnuEditCut":
                    case "ctxCut":
                    case "btnCut": 
                        break;
					case "mnuEditCopy":
                    case "ctxCopy":
                    case "btnCopy": 
                        this.grdRow = this.grdTemplates.ActiveRow; 
                        break;
					case "mnuEditPaste":
			        case "ctxPaste": 
					case "btnPaste": 
                        //Infragistics.Win.UltraWinGrid.UltraGridRow newRow = this.grdTemplates.DisplayLayout.Bands[0].AddNew();
						foreach(Infragistics.Win.UltraWinGrid.UltraGridCell cell in this.grdTemplates.ActiveRow.Cells) {
							if( cell.Column.Key != "TemplateID" && 
								cell.Column.Key != "TemplateRowVersion" &&
								cell.Column.Key != "StopID" &&
								cell.Column.Key != "Stop1RowVersion" &&
								cell.Column.Key != "S2StopID" &&		//cell.Column.Key != "SortCenterID" &&
								cell.Column.Key != "Stop2RowVersion" ) {
								cell.Value = grdRow.Cells[cell.Column.Index].Value;
							}
						}
						grdRow = null;
						break;
					case "mnuEditSearch":   case "btnSearch": break;
					case "mnuViewRefresh":
                    case "ctxRefresh": 
                    case "btnRefresh":
                        this.Cursor = Cursors.WaitCursor;
						this.mMessageMgr.AddMessage("Refreshing templates list...");
                        this.mTemplates.DataSource = ShipScheduleProxy.GetTemplates();
                        //this.grdTemplates.DataBind();
                        this.mMessageMgr.AddMessage("Loading templates...");
						break;
					case "mnuViewToolbar":		this.tsMain.Visible = (this.mnuViewToolbar.Checked = (!this.mnuViewToolbar.Checked)); break;
					case "mnuViewStatusBar":    this.stbMain.Visible = (this.mnuViewStatusBar.Checked = (!this.mnuViewStatusBar.Checked)); break;
                    case "mnuToolsConfig":      App.ShowConfig(); break;
					case "mnuHelpAbout":		new dlgAbout(App.Product + " Application", App.Version, App.Copyright, App.Configuration).ShowDialog(this); break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnHelpMenuClick(object sender,System.EventArgs e) {
            //Event hanlder for configurable help menu items
            try {
                ToolStripItem item = (ToolStripItem)sender;
                Help.ShowHelp(this,this.mHelpItems.GetValues(item.Text)[0]);
            }
            catch(Exception) { }
        }
        #endregion
        #region Local Services: configApplication(), setUserServices(), buildHelpMenu(), validateRules()
        private void configApplication() {
			try {				
                AppSecurity.Role = App.Config.Role;
            }
			catch(ApplicationException ex) { throw ex; } 
			catch(Exception ex) { throw new ApplicationException("Configuration Failure", ex); } 
		}
		private void setUserServices() {
			//Set user services
			try {				
				this.mnuFileNew.Enabled = this.btnNew.Enabled = false;
				this.mnuFileOpen.Enabled = this.btnOpen.Enabled = false;
                this.mnuFileSave.Enabled = this.btnSave.Enabled = false;
				this.mnuFileSaveAs.Enabled = (this.grdTemplates.Rows.Count > 0);
				this.mnuFileExport.Enabled  = (this.grdTemplates.Rows.Count > 0);
				this.mnuFileSetup.Enabled = true;
				this.mnuFilePrint.Enabled = this.btnPrint.Enabled = (this.grdTemplates.Rows.Count > 0);
				this.mnuFilePreview.Enabled = (this.grdTemplates.Rows.Count > 0);
				this.mnuFileExit.Enabled = true;
				this.mnuEditCut.Enabled = this.ctxCut.Enabled = this.btnCut.Enabled = false;
				this.mnuEditCopy.Enabled = this.ctxCopy.Enabled = this.btnCopy.Enabled = ((this.grdTemplates.ActiveRow != null) && (!this.grdTemplates.ActiveRow.IsAddRow));
				this.mnuEditPaste.Enabled = this.ctxPaste.Enabled = this.btnPaste.Enabled = ((this.grdTemplates.ActiveRow != null) && (this.grdTemplates.ActiveRow.IsAddRow) && (this.grdRow != null));
				this.mnuEditSearch.Enabled = this.btnSearch.Enabled = false;
				this.mnuViewRefresh.Enabled = this.ctxRefresh.Enabled = this.btnRefresh.Enabled = true;
				this.mnuViewToolbar.Enabled = this.mnuViewStatusBar.Enabled = true;
                this.mnuToolsConfig.Enabled = true;
				this.mnuHelpAbout.Enabled = true;

                this.stbMain.OnOnlineStatusUpdate(null, new OnlineStatusArgs(ShipScheduleProxy.ServiceState, ShipScheduleProxy.ServiceAddress));
                this.stbMain.User1Panel.Width = 144;
                this.stbMain.User1Panel.Text = AppSecurity.Role;
                this.stbMain.User1Panel.ToolTipText = "User role";
                this.stbMain.User2Panel.Icon = App.Config.ReadOnly ? new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Resources.readonly.ico")) : null;
                this.stbMain.User2Panel.ToolTipText = App.Config.ReadOnly ? "Read only mode; notify IT if you require update permissions." : "";
            }
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Error); }
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
        private bool validateRules(Infragistics.Win.UltraWinGrid.CancelableRowEventArgs e) {
            bool validated = false;
            int schCloseOffset=-1,schDepartOffset=-1,schArrivalOffset=-1,schOFD1Offset=-1;
            DateTime schCloseDate,schDepartDate,schArrivalDate;
            //Biz Rules
            //1 - Schedule Close Days Offset <= Scheduled Departure Days Offset
            StringBuilder message = new StringBuilder();
            if(e.Row.Cells["ScheduledCloseDateOffset"].Text.Trim().Length > 0 && e.Row.Cells["ScheduledDepartureDateOffset"].Text.Trim().Length > 0) {
                schCloseOffset = Convert.ToInt32(e.Row.Cells["ScheduledCloseDateOffset"].Text);
                schDepartOffset = Convert.ToInt32(e.Row.Cells["ScheduledDepartureDateOffset"].Text);
                schCloseDate = Convert.ToDateTime(DateTime.Today.AddDays(schCloseOffset).ToShortDateString() + " " + e.Row.Cells["ScheduledCloseTime"].Text);
                schDepartDate = Convert.ToDateTime(DateTime.Today.AddDays(schDepartOffset).ToShortDateString() + " " + e.Row.Cells["ScheduledDepartureTime"].Text);
                if(schCloseDate > schDepartDate)
                    message.Append("Schedule Close Days Offset and Time must be less than or equal to Scheduled Departure Days Offset and Time." + Environment.NewLine);
            }
            else
                message.Append("Schedule Close and Departure Days Offset cells can't be empty.");
            
            //2- Scheduled arrival days offset <= OFD1 Days Offset
            if(e.Row.Cells["ScheduledArrivalDateOffset"].Text.Trim().Length > 0 && e.Row.Cells["ScheduledOFD1Offset"].Text.Trim().Length > 0) {
                schArrivalOffset = Convert.ToInt32(e.Row.Cells["ScheduledArrivalDateOffset"].Text);
                schOFD1Offset = Convert.ToInt32(e.Row.Cells["ScheduledOFD1Offset"].Text);
                if(schArrivalOffset > schOFD1Offset)
                    message.Append("Scheduled Arrival Days Offset must be less than or equal to Scheduled OFD1 Offset." + Environment.NewLine);
            }
            else
                message.Append("Schedule Close and Departure Days Offset cells can't be empty." + Environment.NewLine);

            //3- Scheduled Departure days offset and Time <= Arrival Days Offset and Time
            if(schDepartOffset != -1  && schArrivalOffset != -1) {
                schDepartDate = Convert.ToDateTime(DateTime.Today.AddDays(schDepartOffset).ToShortDateString() + " " + e.Row.Cells["ScheduledDepartureTime"].Text);
                schArrivalDate = Convert.ToDateTime(DateTime.Today.AddDays(schArrivalOffset).ToShortDateString() + " " + e.Row.Cells["ScheduledArrivalTime"].Text);
                if(schDepartDate > schArrivalDate)
                    message.Append("Scheduled Departure Days Offset and Time must be less than or equal to Scheduled Arrival Days Offset and Time." + Environment.NewLine);
            }
            //4- OfD1 should not fall on a weekend day
            if(e.Row.Cells["DayOfTheWeek"].Text != "" && schOFD1Offset != -1) {
                int remainder1 = 0; int remainder2 = 0;
                int offsetDay = ShipScheduleProxy.GetWeekday(e.Row.Cells["DayOfTheWeek"].Text) + schOFD1Offset;
                //weekends are 6 and 7 days and additions of 7 days like 6+7=13, 7+7=14
                //if the remainder results into 0 by dividing offsetDay by 7 or offsetDay +1 (for saturdays) by 7
                Math.DivRem(offsetDay,7,out remainder1);
                Math.DivRem(offsetDay + 1,7,out remainder2);
                if(remainder1 == 0 || remainder2 == 0)
                    message.Append("OFD1 Days Offset should not fall on the weekend day."  + Environment.NewLine);
            }
            //5- Validate S2
            if(e.Row.Cells["S2MainZone"].Text.Trim().Length > 0) {
                if(e.Row.Cells["S2ScheduledArrivalDateOffset"].Text.Trim().Length > 0 && e.Row.Cells["S2ScheduledOFD1Offset"].Text.Trim().Length > 0) {
                    if(Convert.ToInt32(e.Row.Cells["S2ScheduledArrivalDateOffset"].Text) > Convert.ToInt32(e.Row.Cells["S2ScheduledOFD1Offset"].Text))
                        message.Append("S2 Scheduled Arrival Offset must be less than or equal to S2 Scheduled OFD1 Offset." + Environment.NewLine);
                }
                else
                    message.Append("S2 Scheduled Arrival Offset and S2 Scheduled OFD1 Offset fields can't be empty." + Environment.NewLine);                if(e.Row.Cells["AgentNumber"].Text.Trim() == e.Row.Cells["S2AgentNumber"].Text.Trim()) {
                    message.Append("Agent Number for the second stop can't be the same as the first." + Environment.NewLine);
                }
            }
            if(message.ToString() != "")
                MessageBox.Show(message.ToString(),App.Product);
            else
                validated = true;
            return validated;

        }
        #endregion
	}
}
