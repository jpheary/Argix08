using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.AgentLineHaul;
using Argix.Data;
using Argix.Windows;

namespace Argix.Freight {
	//Application main window
	public class frmMain : System.Windows.Forms.Form {
		//Memebers
        private ShipScheduleTrip mTrip=null;
        private PageSettings mPageSettings = null;
        private UltraGridSvc mGridSvcZones=null,mGridSvcTLs=null,mGridSvcLaneUpdates=null,mGridSvcZoneUpdates=null,mGridSvcShipSchedule=null;
        private System.Windows.Forms.ToolTip mToolTip=null;
        private MessageManager mMessageMgr=null;
        private NameValueCollection mHelpItems=null;
		private bool mIsDragging=false, mCalendarOpen=false;

		private const int KEYSTATE_SHIFT = 5, KEYSTATE_CTL = 9;
        #region Controls

        private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Panel pnlMain;
		private System.Windows.Forms.ComboBox cboSmallLane;
		private System.Windows.Forms.ComboBox cboLane;
		private System.Windows.Forms.TextBox txtSearchSort;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdZones;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdLaneUpdates;
		private Argix.Freight.ZoneDS mZoneDS;
        private Argix.Freight.ZoneDS mUpdateDS;
		private Argix.Windows.ArgixStatusBar stbMain;
		private System.Windows.Forms.TabControl tabMain;
		private System.Windows.Forms.TabPage tabLanes;
		private System.Windows.Forms.TabPage tabZones;
		private System.Windows.Forms.TabPage tabSchedule;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdZoneUpdates;
        private System.Windows.Forms.Splitter splitterV;
		private System.Windows.Forms.DateTimePicker dtpScheduleDate;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdShipSchedule;
        private Argix.AgentLineHaul.ShipScheduleDS mShipScheduleDS;
		private System.Windows.Forms.Splitter splitterH;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdTLs;
        private MenuStrip msMain;
        private ToolStripMenuItem mnuFile;
        private ToolStripMenuItem mnuEdit;
        private ToolStripMenuItem mnuView;
        private ToolStripMenuItem mnuOp;
        private ToolStripMenuItem mnuReports;
        private ToolStripMenuItem mnuTools;
        private ToolStripMenuItem mnuHelp;
        private ToolStripMenuItem mnuFileNew;
        private ToolStripMenuItem mnuFileOpen;
        private ToolStripSeparator mnuFileSep1;
        private ToolStripMenuItem mnuFileSave;
        private ToolStripMenuItem mnuFileSaveAs;
        private ToolStripSeparator mnuFileSep2;
        private ToolStripMenuItem mnuFileSetup;
        private ToolStripMenuItem mnuFilePrint;
        private ToolStripMenuItem mnuFilePreview;
        private ToolStripSeparator mnuFileSep3;
        private ToolStripMenuItem mnuFileExit;
        private ToolStripMenuItem mnuEditCut;
        private ToolStripMenuItem mnuEditCopy;
        private ToolStripMenuItem mnuEditPaste;
        private ToolStripSeparator mnuEditSep1;
        private ToolStripMenuItem mnuEditFind;
        private ToolStripMenuItem mnuEditFindTL;
        private ToolStripMenuItem mnuViewRefresh;
        private ToolStripSeparator mnuViewSep1;
        private ToolStripMenuItem mnuViewZoneType;
        private ToolStripSeparator mnuViewSep2;
        private ToolStripMenuItem mnuViewToolbar;
        private ToolStripMenuItem mnuViewStatusBar;
        private ToolStripMenuItem mnuViewZoneTypeTsort;
        private ToolStripMenuItem mnuViewZoneTypeReturns;
        private ToolStripMenuItem mnuViewZoneTypeAll;
        private ToolStripMenuItem mnuOpAdd;
        private ToolStripMenuItem mnuOpRem;
        private ToolStripMenuItem mnuOpChangeLanes;
        private ToolStripMenuItem mnuOpCloseZones;
        private ToolStripSeparator mnuOpSep1;
        private ToolStripMenuItem mnuOpOpen;
        private ToolStripMenuItem mnuOpCloseAllTLs;
        private ToolStripMenuItem mnuOpClose;
        private ToolStripSeparator mnuOpSep2;
        private ToolStripMenuItem mnuOpAssign;
        private ToolStripMenuItem mnuOpUnassign;
        private ToolStripMenuItem mnuOpMove;
        private ToolStripMenuItem mnuReportsZonesByLane;
        private ToolStripMenuItem mnuToolsConfig;
        private ToolStripMenuItem mnuHelpAbout;
        private ToolStripSeparator mnuHelpSep1;
        private ContextMenuStrip ctxZone;
        private ContextMenuStrip ctxTrip;
        private ToolStripMenuItem ctxZoneAdd;
        private ToolStripSeparator ctxZoneSep1;
        private ToolStripMenuItem ctxZoneAssign;
        private ToolStripMenuItem ctxTripRem;
        private ToolStripSeparator ctxTripSep1;
        private ToolStripMenuItem ctxTripOpen;
        private ToolStripMenuItem ctxTripCloseAllTLs;
        private ToolStripMenuItem ctxTripClose;
        private ToolStripSeparator ctxTripSep2;
        private ToolStripMenuItem ctxTripUnassign;
        private ToolStrip tsMain;
        private ToolStripDropDownButton btnZoneType;
        private ToolStripButton btnNew;
        private ToolStripButton btnOpen_;
        private ToolStripButton btnPrint;
        private ToolStripSeparator btnSep1;
        private ToolStripButton btnSearch;
        private ToolStripSeparator btnSep2;
        private ToolStripButton btnRefresh;
        private ToolStripSeparator btnSep3;
        private ToolStripButton btnAdd;
        private ToolStripButton btnRem;
        private ToolStripButton btnChangeLanes;
        private ToolStripButton btnCloseZones;
        private ToolStripSeparator btnSep4;
        private ToolStripButton btnOpen;
        private ToolStripButton btnClose;
        private ToolStripButton btnAssign;
        private ToolStripButton btnUnassign;
        private ToolStripButton btnMove;
        private LaneDS mLaneDS;
        private ToolStripMenuItem btnZoneTypeTsort;
        private ToolStripMenuItem btnZoneTypeReturns;
        private ToolStripMenuItem btnZoneTypeAll;
        private ToolStripMenuItem ctxTripMove;		
		#endregion
		
		//Interface
		public frmMain() {
			//Constructor
			try {
				//Required for designer support
                InitializeComponent();
                this.Text = App.Product;
                buildHelpMenu();
                #region Set window docking
                this.msMain.Dock = DockStyle.Top;
				this.tsMain.Dock = DockStyle.Top;
				this.splitterV.MinExtra = 288;
				this.splitterV.MinSize = 312;
				this.splitterV.Dock = DockStyle.Left;
				this.pnlMain.Dock = DockStyle.Left;
				this.grdZones.Controls.AddRange(new Control[]{this.txtSearchSort});
				this.txtSearchSort.Top = 1;
				this.txtSearchSort.Left = this.grdZones.Width - this.txtSearchSort.Width - 3;
				this.pnlMain.Controls.AddRange(new Control[]{this.grdZones,this.splitterH,this.grdTLs});
				this.grdShipSchedule.Controls.Add(this.dtpScheduleDate);
				this.tabMain.Dock = DockStyle.Fill;
				this.stbMain.Dock = DockStyle.Bottom;
                this.Controls.AddRange(new Control[] { this.tabMain,this.splitterV,this.pnlMain,this.stbMain, this.tsMain, this.msMain });
				#endregion
                Splash.Start(App.Product,Assembly.GetExecutingAssembly(),App.Copyright);
                Thread.Sleep(3000);

                //Create data and UI services
                this.mPageSettings = new PageSettings();
                this.mPageSettings.Landscape = true;
                this.mGridSvcZones = new UltraGridSvc(this.grdZones,this.txtSearchSort);
                this.mGridSvcTLs = new UltraGridSvc(this.grdTLs);
                this.mGridSvcLaneUpdates = new UltraGridSvc(this.grdLaneUpdates);
                this.mGridSvcZoneUpdates = new UltraGridSvc(this.grdZoneUpdates);
                this.mGridSvcShipSchedule = new UltraGridSvc(this.grdShipSchedule);
                this.mToolTip = new System.Windows.Forms.ToolTip();
                this.mMessageMgr = new MessageManager(this.stbMain.Panels[0],500,3000);
                configApplication();
			}
			catch(Exception ex) { Splash.Close(); throw new ApplicationException("Startup Failure", ex); }
		}
		protected override void Dispose( bool disposing ) { if(disposing) { if(components!= null) components.Dispose(); } base.Dispose(disposing); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		/// 
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ZoneTable",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn153 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn154 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TL#");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn155 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn156 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn157 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NewLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn158 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Lane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn159 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NewSmallSortLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn160 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SmallSortLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn161 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn162 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Type");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn163 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TypeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn164 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn165 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RollbackTL#");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn166 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsExclusive");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn167 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CAN_BE_CLOSED");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn168 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AssignedToShipScde");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn169 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn170 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn171 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ZoneTable",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn172 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn173 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TL#");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn174 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn175 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn176 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NewLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn177 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Lane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn178 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NewSmallSortLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn179 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SmallSortLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn180 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn181 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Type");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn182 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TypeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn183 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn184 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RollbackTL#");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn185 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsExclusive");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn186 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CAN_BE_CLOSED");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn187 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AssignedToShipScde");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn188 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn189 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn190 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ZoneTable",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn191 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn192 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TL#");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn193 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn194 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn195 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NewLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn196 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Lane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn197 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NewSmallSortLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn198 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SmallSortLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn199 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn200 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Type");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn201 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TypeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn202 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn203 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RollbackTL#");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn204 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsExclusive");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn205 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CAN_BE_CLOSED");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn206 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AssignedToShipScde");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn207 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn208 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn209 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand4 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ZoneTable",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn210 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn211 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TL#");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn212 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn213 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn214 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NewLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn215 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Lane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn216 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NewSmallSortLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn217 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SmallSortLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn218 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn219 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Type");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn220 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TypeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn221 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn222 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RollbackTL#");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn223 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsExclusive");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn224 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CAN_BE_CLOSED");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn225 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AssignedToShipScde");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn226 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn227 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn228 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand5 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ShipScheduleMasterTable",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn229 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduleID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn230 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenterID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn231 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenter");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn232 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduleDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn233 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TripID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn234 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TemplateID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn235 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("BolNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn236 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CarrierServiceID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn237 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Carrier");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn238 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LoadNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn239 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn240 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn241 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TractorNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn242 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledClose");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn243 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledDeparture");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn244 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsMandatory");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn245 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FreightAssigned");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn246 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerComplete");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn247 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PaperworkComplete");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn248 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerDispatched");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn249 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Canceled");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn250 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SCDEUserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn251 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SCDELastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn252 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SCDERowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn253 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn254 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn255 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn256 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn257 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MainZone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn258 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Tag");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn259 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Notes");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn260 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledArrival");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn261 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledOFD1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn262 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S1UserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn263 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S1LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn264 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S1RowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn265 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2StopID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn266 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2StopNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn267 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn268 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn269 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2MainZone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn270 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2Tag");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn271 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2Notes");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn272 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ScheduledArrival");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn273 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ScheduledOFD1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn274 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2UserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn275 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn276 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2RowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn277 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NextCarrier");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn278 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CarrierID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn279 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn280 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn281 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Assignment");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand6 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Assignment",0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn282 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TripID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn283 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn284 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn285 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn286 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn287 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MainZone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn288 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Tag");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn289 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Notes");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn290 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledArrival");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn291 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledOFD1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn292 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopUserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn293 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopLastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn294 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopRowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn295 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FreightID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn296 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn297 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn298 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn299 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AssignedUser");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn300 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AssignedDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn301 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn302 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn303 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn304 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.splitterH = new System.Windows.Forms.Splitter();
            this.grdTLs = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ctxZone = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxZoneAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxZoneSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxZoneAssign = new System.Windows.Forms.ToolStripMenuItem();
            this.mZoneDS = new Argix.Freight.ZoneDS();
            this.grdZones = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.txtSearchSort = new System.Windows.Forms.TextBox();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabLanes = new System.Windows.Forms.TabPage();
            this.cboSmallLane = new System.Windows.Forms.ComboBox();
            this.mLaneDS = new Argix.Freight.LaneDS();
            this.cboLane = new System.Windows.Forms.ComboBox();
            this.grdLaneUpdates = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ctxTrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxTripRem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxTripSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxTripOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxTripCloseAllTLs = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxTripClose = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxTripSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxTripUnassign = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxTripMove = new System.Windows.Forms.ToolStripMenuItem();
            this.mUpdateDS = new Argix.Freight.ZoneDS();
            this.tabZones = new System.Windows.Forms.TabPage();
            this.grdZoneUpdates = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.tabSchedule = new System.Windows.Forms.TabPage();
            this.dtpScheduleDate = new System.Windows.Forms.DateTimePicker();
            this.grdShipSchedule = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mShipScheduleDS = new Argix.AgentLineHaul.ShipScheduleDS();
            this.stbMain = new Argix.Windows.ArgixStatusBar();
            this.splitterV = new System.Windows.Forms.Splitter();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
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
            this.mnuEditFind = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditFindTL = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewZoneType = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewZoneTypeTsort = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewZoneTypeReturns = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewZoneTypeAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpRem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpChangeLanes = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpCloseZones = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuOpOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpCloseAllTLs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpClose = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuOpAssign = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpUnassign = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpMove = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReportsZonesByLane = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnZoneType = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnZoneTypeTsort = new System.Windows.Forms.ToolStripMenuItem();
            this.btnZoneTypeReturns = new System.Windows.Forms.ToolStripMenuItem();
            this.btnZoneTypeAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnOpen_ = new System.Windows.Forms.ToolStripButton();
            this.btnSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnSearch = new System.Windows.Forms.ToolStripButton();
            this.btnSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnRem = new System.Windows.Forms.ToolStripButton();
            this.btnChangeLanes = new System.Windows.Forms.ToolStripButton();
            this.btnCloseZones = new System.Windows.Forms.ToolStripButton();
            this.btnSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.btnAssign = new System.Windows.Forms.ToolStripButton();
            this.btnUnassign = new System.Windows.Forms.ToolStripButton();
            this.btnMove = new System.Windows.Forms.ToolStripButton();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTLs)).BeginInit();
            this.ctxZone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mZoneDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdZones)).BeginInit();
            this.grdZones.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabLanes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mLaneDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLaneUpdates)).BeginInit();
            this.ctxTrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mUpdateDS)).BeginInit();
            this.tabZones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdZoneUpdates)).BeginInit();
            this.tabSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdShipSchedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mShipScheduleDS)).BeginInit();
            this.msMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.Navy;
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(72,256);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64,23);
            this.btnCancel.TabIndex = 153;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.ForeColor = System.Drawing.Color.Navy;
            this.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOK.Location = new System.Drawing.Point(8,256);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(64,23);
            this.btnOK.TabIndex = 152;
            this.btnOK.Text = "O&K";
            this.btnOK.UseVisualStyleBackColor = false;
            // 
            // pnlMain
            // 
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMain.Controls.Add(this.splitterH);
            this.pnlMain.Controls.Add(this.grdTLs);
            this.pnlMain.Controls.Add(this.grdZones);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMain.Location = new System.Drawing.Point(0,49);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(327,254);
            this.pnlMain.TabIndex = 2;
            // 
            // splitterH
            // 
            this.splitterH.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitterH.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterH.Location = new System.Drawing.Point(0,103);
            this.splitterH.MinExtra = 48;
            this.splitterH.MinSize = 72;
            this.splitterH.Name = "splitterH";
            this.splitterH.Size = new System.Drawing.Size(323,3);
            this.splitterH.TabIndex = 5;
            this.splitterH.TabStop = false;
            this.splitterH.Visible = false;
            // 
            // grdTLs
            // 
            this.grdTLs.AllowDrop = true;
            this.grdTLs.ContextMenuStrip = this.ctxZone;
            this.grdTLs.DataMember = "ZoneTable";
            this.grdTLs.DataSource = this.mZoneDS;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.FontData.Name = "Verdana";
            appearance5.FontData.SizeInPoints = 8F;
            appearance5.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance5.TextHAlignAsString = "Left";
            this.grdTLs.DisplayLayout.Appearance = appearance5;
            ultraGridColumn153.Header.Caption = "Code";
            ultraGridColumn153.Header.Fixed = true;
            ultraGridColumn153.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn153.Header.VisiblePosition = 0;
            ultraGridColumn153.Width = 54;
            ultraGridColumn154.Header.Fixed = true;
            ultraGridColumn154.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn154.Header.VisiblePosition = 2;
            ultraGridColumn154.Width = 84;
            ultraGridColumn155.Format = "MMddyy";
            ultraGridColumn155.Header.Caption = "TL Date";
            ultraGridColumn155.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn155.Header.VisiblePosition = 3;
            ultraGridColumn155.Width = 60;
            ultraGridColumn156.Header.Caption = "Close#";
            ultraGridColumn156.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn156.Header.VisiblePosition = 4;
            ultraGridColumn156.Width = 51;
            ultraGridColumn157.Header.VisiblePosition = 6;
            ultraGridColumn157.Hidden = true;
            ultraGridColumn158.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn158.Header.VisiblePosition = 5;
            ultraGridColumn158.Width = 60;
            ultraGridColumn159.Header.VisiblePosition = 11;
            ultraGridColumn159.Hidden = true;
            ultraGridColumn160.Header.Caption = "S. Lane";
            ultraGridColumn160.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn160.Header.VisiblePosition = 7;
            ultraGridColumn160.Width = 60;
            ultraGridColumn161.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn161.Header.VisiblePosition = 10;
            ultraGridColumn161.Width = 144;
            ultraGridColumn162.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn162.Header.VisiblePosition = 12;
            ultraGridColumn162.Width = 72;
            ultraGridColumn163.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn163.Header.VisiblePosition = 16;
            ultraGridColumn163.Hidden = true;
            ultraGridColumn163.Width = 72;
            ultraGridColumn164.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn164.Header.VisiblePosition = 15;
            ultraGridColumn164.Width = 72;
            ultraGridColumn165.Header.Caption = "Rollback TL#";
            ultraGridColumn165.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn165.Header.VisiblePosition = 14;
            ultraGridColumn165.Width = 96;
            ultraGridColumn166.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn166.Header.VisiblePosition = 13;
            ultraGridColumn166.Hidden = true;
            ultraGridColumn166.Width = 72;
            ultraGridColumn167.Header.Caption = "Can Close";
            ultraGridColumn167.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn167.Header.VisiblePosition = 17;
            ultraGridColumn167.Width = 72;
            ultraGridColumn168.Header.Caption = "State";
            ultraGridColumn168.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn168.Header.VisiblePosition = 18;
            ultraGridColumn168.Width = 72;
            ultraGridColumn169.Header.Caption = "Agent";
            ultraGridColumn169.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn169.Header.VisiblePosition = 8;
            ultraGridColumn169.Hidden = true;
            ultraGridColumn169.Width = 72;
            ultraGridColumn170.Header.Caption = "Client#";
            ultraGridColumn170.Header.Fixed = true;
            ultraGridColumn170.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn170.Header.VisiblePosition = 1;
            ultraGridColumn170.Width = 60;
            ultraGridColumn171.Header.Caption = "Client";
            ultraGridColumn171.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn171.Header.VisiblePosition = 9;
            ultraGridColumn171.Width = 144;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn153,
            ultraGridColumn154,
            ultraGridColumn155,
            ultraGridColumn156,
            ultraGridColumn157,
            ultraGridColumn158,
            ultraGridColumn159,
            ultraGridColumn160,
            ultraGridColumn161,
            ultraGridColumn162,
            ultraGridColumn163,
            ultraGridColumn164,
            ultraGridColumn165,
            ultraGridColumn166,
            ultraGridColumn167,
            ultraGridColumn168,
            ultraGridColumn169,
            ultraGridColumn170,
            ultraGridColumn171});
            ultraGridBand1.SummaryFooterCaption = "Grand Summaries";
            this.grdTLs.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdTLs.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Etched;
            appearance6.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance6.FontData.BoldAsString = "True";
            appearance6.FontData.Name = "Verdana";
            appearance6.FontData.SizeInPoints = 8F;
            appearance6.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance6.TextHAlignAsString = "Left";
            this.grdTLs.DisplayLayout.CaptionAppearance = appearance6;
            this.grdTLs.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdTLs.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            this.grdTLs.DisplayLayout.Override.AllowColSwapping = Infragistics.Win.UltraWinGrid.AllowColSwapping.NotAllowed;
            this.grdTLs.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdTLs.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdTLs.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance7.BackColor = System.Drawing.SystemColors.Control;
            appearance7.FontData.BoldAsString = "True";
            appearance7.FontData.Name = "Verdana";
            appearance7.FontData.SizeInPoints = 8F;
            appearance7.TextHAlignAsString = "Left";
            this.grdTLs.DisplayLayout.Override.HeaderAppearance = appearance7;
            this.grdTLs.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdTLs.DisplayLayout.Override.MaxSelectedRows = 0;
            appearance8.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdTLs.DisplayLayout.Override.RowAppearance = appearance8;
            this.grdTLs.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdTLs.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.ExtendedAutoDrag;
            this.grdTLs.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
            this.grdTLs.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdTLs.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdTLs.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdTLs.DisplayLayout.UseFixedHeaders = true;
            this.grdTLs.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdTLs.Font = new System.Drawing.Font("Verdana",8.25F);
            this.grdTLs.Location = new System.Drawing.Point(0,106);
            this.grdTLs.Name = "grdTLs";
            this.grdTLs.Size = new System.Drawing.Size(323,144);
            this.grdTLs.TabIndex = 6;
            this.grdTLs.Text = "TLs: Closed (Unassigned)";
            this.grdTLs.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdTLs.Visible = false;
            this.grdTLs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdTLs.Enter += new System.EventHandler(this.OnEnter);
            this.grdTLs.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.grdTLs.Leave += new System.EventHandler(this.OnLeave);
            this.grdTLs.DragOver += new System.Windows.Forms.DragEventHandler(this.OnDragOver);
            this.grdTLs.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
            this.grdTLs.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseUp);
            this.grdTLs.DoubleClick += new System.EventHandler(this.OnTLDoubleClicked);
            this.grdTLs.DragLeave += new System.EventHandler(this.OnDragLeave);
            this.grdTLs.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnTLSelectionChanged);
            this.grdTLs.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseMove);
            // 
            // ctxZone
            // 
            this.ctxZone.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxZoneAdd,
            this.ctxZoneSep1,
            this.ctxZoneAssign});
            this.ctxZone.Name = "ctxZone";
            this.ctxZone.Size = new System.Drawing.Size(126,54);
            // 
            // ctxZoneAdd
            // 
            this.ctxZoneAdd.Image = global::Argix.Properties.Resources.BuilderDialog_add;
            this.ctxZoneAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxZoneAdd.Name = "ctxZoneAdd";
            this.ctxZoneAdd.Size = new System.Drawing.Size(125,22);
            this.ctxZoneAdd.Text = "Add";
            this.ctxZoneAdd.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxZoneSep1
            // 
            this.ctxZoneSep1.Name = "ctxZoneSep1";
            this.ctxZoneSep1.Size = new System.Drawing.Size(122,6);
            // 
            // ctxZoneAssign
            // 
            this.ctxZoneAssign.Image = global::Argix.Properties.Resources.Edit_Redo;
            this.ctxZoneAssign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxZoneAssign.Name = "ctxZoneAssign";
            this.ctxZoneAssign.Size = new System.Drawing.Size(125,22);
            this.ctxZoneAssign.Text = "Assign TL";
            this.ctxZoneAssign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mZoneDS
            // 
            this.mZoneDS.DataSetName = "ZoneDS";
            this.mZoneDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mZoneDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grdZones
            // 
            this.grdZones.AllowDrop = true;
            this.grdZones.ContextMenuStrip = this.ctxZone;
            this.grdZones.Controls.Add(this.txtSearchSort);
            this.grdZones.DataMember = "ZoneTable";
            this.grdZones.DataSource = this.mZoneDS;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.Name = "Verdana";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdZones.DisplayLayout.Appearance = appearance1;
            ultraGridColumn172.Header.Caption = "Code";
            ultraGridColumn172.Header.Fixed = true;
            ultraGridColumn172.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn172.Header.VisiblePosition = 0;
            ultraGridColumn172.Width = 54;
            ultraGridColumn173.Header.Fixed = true;
            ultraGridColumn173.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn173.Header.VisiblePosition = 2;
            ultraGridColumn173.Width = 84;
            ultraGridColumn174.Format = "MMddyy";
            ultraGridColumn174.Header.Caption = "TL Date";
            ultraGridColumn174.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn174.Header.VisiblePosition = 3;
            ultraGridColumn174.Width = 60;
            ultraGridColumn175.Header.Caption = "Close#";
            ultraGridColumn175.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn175.Header.VisiblePosition = 4;
            ultraGridColumn175.Width = 51;
            ultraGridColumn176.Header.VisiblePosition = 8;
            ultraGridColumn176.Hidden = true;
            ultraGridColumn177.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn177.Header.VisiblePosition = 5;
            ultraGridColumn177.Width = 60;
            ultraGridColumn178.Header.VisiblePosition = 11;
            ultraGridColumn178.Hidden = true;
            ultraGridColumn179.Header.Caption = "S. Lane";
            ultraGridColumn179.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn179.Header.VisiblePosition = 6;
            ultraGridColumn179.Width = 60;
            ultraGridColumn180.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn180.Header.VisiblePosition = 10;
            ultraGridColumn180.Width = 144;
            ultraGridColumn181.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn181.Header.VisiblePosition = 12;
            ultraGridColumn181.Width = 72;
            ultraGridColumn182.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn182.Header.VisiblePosition = 16;
            ultraGridColumn182.Hidden = true;
            ultraGridColumn182.Width = 72;
            ultraGridColumn183.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn183.Header.VisiblePosition = 15;
            ultraGridColumn183.Width = 72;
            ultraGridColumn184.Header.Caption = "Rollback TL#";
            ultraGridColumn184.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn184.Header.VisiblePosition = 14;
            ultraGridColumn184.Width = 96;
            ultraGridColumn185.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn185.Header.VisiblePosition = 13;
            ultraGridColumn185.Hidden = true;
            ultraGridColumn185.Width = 72;
            ultraGridColumn186.Header.Caption = "Can Close";
            ultraGridColumn186.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn186.Header.VisiblePosition = 17;
            ultraGridColumn186.Width = 72;
            ultraGridColumn187.Header.Caption = "State";
            ultraGridColumn187.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn187.Header.VisiblePosition = 18;
            ultraGridColumn187.Width = 72;
            ultraGridColumn188.Header.Caption = "Agent";
            ultraGridColumn188.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn188.Header.VisiblePosition = 9;
            ultraGridColumn188.Hidden = true;
            ultraGridColumn188.Width = 72;
            ultraGridColumn189.Header.Caption = "Client#";
            ultraGridColumn189.Header.Fixed = true;
            ultraGridColumn189.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn189.Header.VisiblePosition = 1;
            ultraGridColumn189.Width = 60;
            ultraGridColumn190.Header.Caption = "Client";
            ultraGridColumn190.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn190.Header.VisiblePosition = 7;
            ultraGridColumn190.Width = 144;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn172,
            ultraGridColumn173,
            ultraGridColumn174,
            ultraGridColumn175,
            ultraGridColumn176,
            ultraGridColumn177,
            ultraGridColumn178,
            ultraGridColumn179,
            ultraGridColumn180,
            ultraGridColumn181,
            ultraGridColumn182,
            ultraGridColumn183,
            ultraGridColumn184,
            ultraGridColumn185,
            ultraGridColumn186,
            ultraGridColumn187,
            ultraGridColumn188,
            ultraGridColumn189,
            ultraGridColumn190});
            ultraGridBand2.SummaryFooterCaption = "Grand Summaries";
            this.grdZones.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.grdZones.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Etched;
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.FontData.Name = "Verdana";
            appearance2.FontData.SizeInPoints = 8F;
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdZones.DisplayLayout.CaptionAppearance = appearance2;
            this.grdZones.DisplayLayout.MaxColScrollRegions = 1;
            this.grdZones.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdZones.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            this.grdZones.DisplayLayout.Override.AllowColSwapping = Infragistics.Win.UltraWinGrid.AllowColSwapping.NotAllowed;
            this.grdZones.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdZones.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdZones.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.Name = "Verdana";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.TextHAlignAsString = "Left";
            this.grdZones.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdZones.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdZones.DisplayLayout.Override.MaxSelectedRows = 0;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdZones.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdZones.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdZones.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.ExtendedAutoDrag;
            this.grdZones.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
            this.grdZones.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdZones.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdZones.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdZones.DisplayLayout.UseFixedHeaders = true;
            this.grdZones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdZones.Font = new System.Drawing.Font("Verdana",8.25F);
            this.grdZones.Location = new System.Drawing.Point(0,0);
            this.grdZones.Name = "grdZones";
            this.grdZones.Size = new System.Drawing.Size(323,250);
            this.grdZones.TabIndex = 4;
            this.grdZones.Text = "TLs: Open (All)";
            this.grdZones.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdZones.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdZones.Enter += new System.EventHandler(this.OnEnter);
            this.grdZones.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.grdZones.Leave += new System.EventHandler(this.OnLeave);
            this.grdZones.BeforeSelectChange += new Infragistics.Win.UltraWinGrid.BeforeSelectChangeEventHandler(this.OnBeforeZoneSelectionChanged);
            this.grdZones.DragOver += new System.Windows.Forms.DragEventHandler(this.OnDragOver);
            this.grdZones.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
            this.grdZones.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseUp);
            this.grdZones.DoubleClick += new System.EventHandler(this.OnZoneDoubleClicked);
            this.grdZones.DragLeave += new System.EventHandler(this.OnDragLeave);
            this.grdZones.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnZoneSelectionChanged);
            this.grdZones.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseMove);
            // 
            // txtSearchSort
            // 
            this.txtSearchSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchSort.Location = new System.Drawing.Point(222,0);
            this.txtSearchSort.Name = "txtSearchSort";
            this.txtSearchSort.Size = new System.Drawing.Size(96,21);
            this.txtSearchSort.TabIndex = 2;
            this.txtSearchSort.TextChanged += new System.EventHandler(this.OnSearchValueChanged);
            // 
            // tabMain
            // 
            this.tabMain.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabMain.Controls.Add(this.tabLanes);
            this.tabMain.Controls.Add(this.tabZones);
            this.tabMain.Controls.Add(this.tabSchedule);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.ItemSize = new System.Drawing.Size(93,18);
            this.tabMain.Location = new System.Drawing.Point(330,49);
            this.tabMain.Margin = new System.Windows.Forms.Padding(0);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Drawing.Point(0,0);
            this.tabMain.SelectedIndex = 0;
            this.tabMain.ShowToolTips = true;
            this.tabMain.Size = new System.Drawing.Size(430,254);
            this.tabMain.TabIndex = 10;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.OnTabSelectedIndexChanged);
            // 
            // tabLanes
            // 
            this.tabLanes.Controls.Add(this.cboSmallLane);
            this.tabLanes.Controls.Add(this.cboLane);
            this.tabLanes.Controls.Add(this.grdLaneUpdates);
            this.tabLanes.Location = new System.Drawing.Point(4,4);
            this.tabLanes.Margin = new System.Windows.Forms.Padding(0);
            this.tabLanes.Name = "tabLanes";
            this.tabLanes.Size = new System.Drawing.Size(422,228);
            this.tabLanes.TabIndex = 0;
            this.tabLanes.Text = "Change Lanes";
            this.tabLanes.ToolTipText = "Change lanes only";
            // 
            // cboSmallLane
            // 
            this.cboSmallLane.DataSource = this.mLaneDS;
            this.cboSmallLane.DisplayMember = "SmallLaneTable.SmallSortLane";
            this.cboSmallLane.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSmallLane.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSmallLane.ItemHeight = 13;
            this.cboSmallLane.Location = new System.Drawing.Point(276,0);
            this.cboSmallLane.Margin = new System.Windows.Forms.Padding(0);
            this.cboSmallLane.Name = "cboSmallLane";
            this.cboSmallLane.Size = new System.Drawing.Size(60,21);
            this.cboSmallLane.TabIndex = 8;
            this.cboSmallLane.ValueMember = "SmallLaneTable.SmallSortLane";
            this.cboSmallLane.SelectionChangeCommitted += new System.EventHandler(this.OnSmallLaneSelected);
            // 
            // mLaneDS
            // 
            this.mLaneDS.DataSetName = "LaneDS";
            this.mLaneDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cboLane
            // 
            this.cboLane.DataSource = this.mLaneDS;
            this.cboLane.DisplayMember = "LaneTable.Lane";
            this.cboLane.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLane.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboLane.ItemHeight = 13;
            this.cboLane.Location = new System.Drawing.Point(210,0);
            this.cboLane.Margin = new System.Windows.Forms.Padding(0);
            this.cboLane.Name = "cboLane";
            this.cboLane.Size = new System.Drawing.Size(60,21);
            this.cboLane.TabIndex = 6;
            this.cboLane.ValueMember = "LaneTable.Lane";
            this.cboLane.SelectionChangeCommitted += new System.EventHandler(this.OnLaneSelected);
            // 
            // grdLaneUpdates
            // 
            this.grdLaneUpdates.AllowDrop = true;
            this.grdLaneUpdates.ContextMenuStrip = this.ctxTrip;
            this.grdLaneUpdates.DataMember = "ZoneTable";
            this.grdLaneUpdates.DataSource = this.mUpdateDS;
            appearance30.BackColor = System.Drawing.SystemColors.Window;
            appearance30.FontData.Name = "Verdana";
            appearance30.FontData.SizeInPoints = 8F;
            appearance30.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance30.TextHAlignAsString = "Left";
            this.grdLaneUpdates.DisplayLayout.Appearance = appearance30;
            ultraGridBand3.AddButtonCaption = "TLViewTable";
            ultraGridColumn191.Header.Caption = "Code";
            ultraGridColumn191.Header.VisiblePosition = 0;
            ultraGridColumn191.Width = 54;
            ultraGridColumn192.Header.VisiblePosition = 2;
            ultraGridColumn192.Width = 84;
            ultraGridColumn193.Format = "MMddyy";
            ultraGridColumn193.Header.Caption = "TL Date";
            ultraGridColumn193.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn193.Header.VisiblePosition = 3;
            ultraGridColumn193.Width = 60;
            ultraGridColumn194.Header.Caption = "Close#";
            ultraGridColumn194.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn194.Header.VisiblePosition = 4;
            ultraGridColumn194.Width = 51;
            ultraGridColumn195.Header.Caption = "New Lane";
            ultraGridColumn195.Header.VisiblePosition = 5;
            ultraGridColumn195.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
            ultraGridColumn195.Width = 60;
            ultraGridColumn196.Header.VisiblePosition = 6;
            ultraGridColumn196.Width = 60;
            ultraGridColumn197.Header.Caption = "New S. Lane";
            ultraGridColumn197.Header.VisiblePosition = 7;
            ultraGridColumn197.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
            ultraGridColumn197.Width = 60;
            ultraGridColumn198.Header.Caption = "S. Lane";
            ultraGridColumn198.Header.VisiblePosition = 8;
            ultraGridColumn198.Width = 60;
            ultraGridColumn199.Header.VisiblePosition = 11;
            ultraGridColumn199.Width = 144;
            ultraGridColumn200.Header.VisiblePosition = 12;
            ultraGridColumn200.Width = 72;
            ultraGridColumn201.Header.VisiblePosition = 17;
            ultraGridColumn201.Hidden = true;
            ultraGridColumn201.Width = 72;
            ultraGridColumn202.Header.VisiblePosition = 16;
            ultraGridColumn202.Width = 72;
            ultraGridColumn203.Header.Caption = "Rollback TL#";
            ultraGridColumn203.Header.VisiblePosition = 14;
            ultraGridColumn203.Hidden = true;
            ultraGridColumn203.Width = 96;
            ultraGridColumn204.Header.VisiblePosition = 13;
            ultraGridColumn204.Hidden = true;
            ultraGridColumn204.Width = 72;
            ultraGridColumn205.Header.VisiblePosition = 18;
            ultraGridColumn205.Hidden = true;
            ultraGridColumn205.Width = 72;
            ultraGridColumn206.Header.VisiblePosition = 15;
            ultraGridColumn206.Hidden = true;
            ultraGridColumn207.Header.Caption = "Agent";
            ultraGridColumn207.Header.VisiblePosition = 9;
            ultraGridColumn207.Hidden = true;
            ultraGridColumn208.Header.Caption = "Client#";
            ultraGridColumn208.Header.VisiblePosition = 1;
            ultraGridColumn208.Width = 60;
            ultraGridColumn209.Header.Caption = "Client";
            ultraGridColumn209.Header.VisiblePosition = 10;
            ultraGridColumn209.Width = 144;
            ultraGridBand3.Columns.AddRange(new object[] {
            ultraGridColumn191,
            ultraGridColumn192,
            ultraGridColumn193,
            ultraGridColumn194,
            ultraGridColumn195,
            ultraGridColumn196,
            ultraGridColumn197,
            ultraGridColumn198,
            ultraGridColumn199,
            ultraGridColumn200,
            ultraGridColumn201,
            ultraGridColumn202,
            ultraGridColumn203,
            ultraGridColumn204,
            ultraGridColumn205,
            ultraGridColumn206,
            ultraGridColumn207,
            ultraGridColumn208,
            ultraGridColumn209});
            ultraGridBand3.SummaryFooterCaption = "Grand Summaries";
            this.grdLaneUpdates.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
            this.grdLaneUpdates.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Etched;
            appearance31.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance31.FontData.BoldAsString = "True";
            appearance31.FontData.Name = "Verdana";
            appearance31.FontData.SizeInPoints = 8F;
            appearance31.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance31.TextHAlignAsString = "Left";
            this.grdLaneUpdates.DisplayLayout.CaptionAppearance = appearance31;
            this.grdLaneUpdates.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdLaneUpdates.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            this.grdLaneUpdates.DisplayLayout.Override.AllowColSwapping = Infragistics.Win.UltraWinGrid.AllowColSwapping.NotAllowed;
            this.grdLaneUpdates.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdLaneUpdates.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdLaneUpdates.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance32.BackColor = System.Drawing.SystemColors.Control;
            appearance32.FontData.BoldAsString = "True";
            appearance32.FontData.Name = "Verdana";
            appearance32.FontData.SizeInPoints = 8F;
            appearance32.TextHAlignAsString = "Left";
            this.grdLaneUpdates.DisplayLayout.Override.HeaderAppearance = appearance32;
            this.grdLaneUpdates.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdLaneUpdates.DisplayLayout.Override.MaxSelectedRows = 0;
            appearance33.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdLaneUpdates.DisplayLayout.Override.RowAppearance = appearance33;
            this.grdLaneUpdates.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdLaneUpdates.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.ExtendedAutoDrag;
            this.grdLaneUpdates.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
            this.grdLaneUpdates.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdLaneUpdates.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdLaneUpdates.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdLaneUpdates.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdLaneUpdates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLaneUpdates.Font = new System.Drawing.Font("Verdana",8.25F);
            this.grdLaneUpdates.Location = new System.Drawing.Point(0,0);
            this.grdLaneUpdates.Margin = new System.Windows.Forms.Padding(0);
            this.grdLaneUpdates.Name = "grdLaneUpdates";
            this.grdLaneUpdates.Size = new System.Drawing.Size(422,228);
            this.grdLaneUpdates.TabIndex = 4;
            this.grdLaneUpdates.Text = "Change Lanes Only";
            this.grdLaneUpdates.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdLaneUpdates.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdLaneUpdates.AfterColPosChanged += new Infragistics.Win.UltraWinGrid.AfterColPosChangedEventHandler(this.OnUpdateColumnHeaderResized);
            this.grdLaneUpdates.Enter += new System.EventHandler(this.OnEnter);
            this.grdLaneUpdates.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.grdLaneUpdates.Leave += new System.EventHandler(this.OnLeave);
            this.grdLaneUpdates.DragOver += new System.Windows.Forms.DragEventHandler(this.OnDragOver);
            this.grdLaneUpdates.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
            this.grdLaneUpdates.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseUp);
            this.grdLaneUpdates.DoubleClick += new System.EventHandler(this.OnUpdateDoubleClicked);
            this.grdLaneUpdates.DragLeave += new System.EventHandler(this.OnDragLeave);
            this.grdLaneUpdates.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnUpdateSelectionChanged);
            this.grdLaneUpdates.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseMove);
            // 
            // ctxTrip
            // 
            this.ctxTrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxTripRem,
            this.ctxTripSep1,
            this.ctxTripOpen,
            this.ctxTripCloseAllTLs,
            this.ctxTripClose,
            this.ctxTripSep2,
            this.ctxTripUnassign,
            this.ctxTripMove});
            this.ctxTrip.Name = "ctxTrip";
            this.ctxTrip.Size = new System.Drawing.Size(142,148);
            // 
            // ctxTripRem
            // 
            this.ctxTripRem.Image = global::Argix.Properties.Resources.BuilderDialog_remove;
            this.ctxTripRem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxTripRem.Name = "ctxTripRem";
            this.ctxTripRem.Size = new System.Drawing.Size(141,22);
            this.ctxTripRem.Text = "Remove";
            this.ctxTripRem.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxTripSep1
            // 
            this.ctxTripSep1.Name = "ctxTripSep1";
            this.ctxTripSep1.Size = new System.Drawing.Size(138,6);
            // 
            // ctxTripOpen
            // 
            this.ctxTripOpen.Image = global::Argix.Properties.Resources.book_open;
            this.ctxTripOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxTripOpen.Name = "ctxTripOpen";
            this.ctxTripOpen.Size = new System.Drawing.Size(141,22);
            this.ctxTripOpen.Text = "Open Trip";
            this.ctxTripOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxTripCloseAllTLs
            // 
            this.ctxTripCloseAllTLs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxTripCloseAllTLs.Name = "ctxTripCloseAllTLs";
            this.ctxTripCloseAllTLs.Size = new System.Drawing.Size(141,22);
            this.ctxTripCloseAllTLs.Text = "Close All TLs";
            this.ctxTripCloseAllTLs.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxTripClose
            // 
            this.ctxTripClose.Image = global::Argix.Properties.Resources.book_angle;
            this.ctxTripClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxTripClose.Name = "ctxTripClose";
            this.ctxTripClose.Size = new System.Drawing.Size(141,22);
            this.ctxTripClose.Text = "Close Trip";
            this.ctxTripClose.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxTripSep2
            // 
            this.ctxTripSep2.Name = "ctxTripSep2";
            this.ctxTripSep2.Size = new System.Drawing.Size(138,6);
            // 
            // ctxTripUnassign
            // 
            this.ctxTripUnassign.Image = global::Argix.Properties.Resources.Edit_Undo;
            this.ctxTripUnassign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxTripUnassign.Name = "ctxTripUnassign";
            this.ctxTripUnassign.Size = new System.Drawing.Size(141,22);
            this.ctxTripUnassign.Text = "Unassign";
            this.ctxTripUnassign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxTripMove
            // 
            this.ctxTripMove.Image = global::Argix.Properties.Resources.MoveToFolder;
            this.ctxTripMove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxTripMove.Name = "ctxTripMove";
            this.ctxTripMove.Size = new System.Drawing.Size(141,22);
            this.ctxTripMove.Text = "Move";
            this.ctxTripMove.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mUpdateDS
            // 
            this.mUpdateDS.DataSetName = "ZoneDS";
            this.mUpdateDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mUpdateDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabZones
            // 
            this.tabZones.Controls.Add(this.grdZoneUpdates);
            this.tabZones.Location = new System.Drawing.Point(4,4);
            this.tabZones.Margin = new System.Windows.Forms.Padding(0);
            this.tabZones.Name = "tabZones";
            this.tabZones.Size = new System.Drawing.Size(422,228);
            this.tabZones.TabIndex = 1;
            this.tabZones.Text = "Close Zones/Change Lanes";
            this.tabZones.ToolTipText = "Close zones AND change lanes";
            // 
            // grdZoneUpdates
            // 
            this.grdZoneUpdates.AllowDrop = true;
            this.grdZoneUpdates.ContextMenuStrip = this.ctxTrip;
            this.grdZoneUpdates.DataMember = "ZoneTable";
            this.grdZoneUpdates.DataSource = this.mUpdateDS;
            appearance13.BackColor = System.Drawing.SystemColors.Info;
            appearance13.FontData.Name = "Verdana";
            appearance13.FontData.SizeInPoints = 8F;
            appearance13.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance13.TextHAlignAsString = "Left";
            this.grdZoneUpdates.DisplayLayout.Appearance = appearance13;
            ultraGridBand4.AddButtonCaption = "TLViewTable";
            ultraGridColumn210.Header.Caption = "Code";
            ultraGridColumn210.Header.VisiblePosition = 0;
            ultraGridColumn210.Width = 54;
            ultraGridColumn211.Header.VisiblePosition = 2;
            ultraGridColumn211.Width = 84;
            ultraGridColumn212.Format = "MMddyy";
            ultraGridColumn212.Header.Caption = "TL Date";
            ultraGridColumn212.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn212.Header.VisiblePosition = 3;
            ultraGridColumn212.Width = 60;
            ultraGridColumn213.Header.Caption = "Close#";
            ultraGridColumn213.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn213.Header.VisiblePosition = 4;
            ultraGridColumn213.Width = 51;
            ultraGridColumn214.Header.Caption = "New Lane";
            ultraGridColumn214.Header.VisiblePosition = 5;
            ultraGridColumn214.Width = 60;
            ultraGridColumn215.Header.VisiblePosition = 6;
            ultraGridColumn215.Width = 60;
            ultraGridColumn216.Header.Caption = "New S. Lane";
            ultraGridColumn216.Header.VisiblePosition = 7;
            ultraGridColumn216.Width = 60;
            ultraGridColumn217.Header.Caption = "S. Lane";
            ultraGridColumn217.Header.VisiblePosition = 8;
            ultraGridColumn217.Width = 60;
            ultraGridColumn218.Header.VisiblePosition = 11;
            ultraGridColumn218.Width = 144;
            ultraGridColumn219.Header.VisiblePosition = 12;
            ultraGridColumn219.Width = 72;
            ultraGridColumn220.Header.VisiblePosition = 17;
            ultraGridColumn220.Hidden = true;
            ultraGridColumn220.Width = 72;
            ultraGridColumn221.Header.VisiblePosition = 16;
            ultraGridColumn221.Width = 72;
            ultraGridColumn222.Header.Caption = "Rollback TL#";
            ultraGridColumn222.Header.VisiblePosition = 14;
            ultraGridColumn222.Hidden = true;
            ultraGridColumn222.Width = 96;
            ultraGridColumn223.Header.VisiblePosition = 13;
            ultraGridColumn223.Hidden = true;
            ultraGridColumn223.Width = 72;
            ultraGridColumn224.Header.VisiblePosition = 18;
            ultraGridColumn224.Hidden = true;
            ultraGridColumn224.Width = 72;
            ultraGridColumn225.Header.VisiblePosition = 15;
            ultraGridColumn225.Hidden = true;
            ultraGridColumn226.Header.Caption = "Agent";
            ultraGridColumn226.Header.VisiblePosition = 9;
            ultraGridColumn226.Hidden = true;
            ultraGridColumn227.Header.Caption = "Client#";
            ultraGridColumn227.Header.VisiblePosition = 1;
            ultraGridColumn227.Width = 60;
            ultraGridColumn228.Header.Caption = "Client";
            ultraGridColumn228.Header.VisiblePosition = 10;
            ultraGridColumn228.Width = 144;
            ultraGridBand4.Columns.AddRange(new object[] {
            ultraGridColumn210,
            ultraGridColumn211,
            ultraGridColumn212,
            ultraGridColumn213,
            ultraGridColumn214,
            ultraGridColumn215,
            ultraGridColumn216,
            ultraGridColumn217,
            ultraGridColumn218,
            ultraGridColumn219,
            ultraGridColumn220,
            ultraGridColumn221,
            ultraGridColumn222,
            ultraGridColumn223,
            ultraGridColumn224,
            ultraGridColumn225,
            ultraGridColumn226,
            ultraGridColumn227,
            ultraGridColumn228});
            ultraGridBand4.SummaryFooterCaption = "Grand Summaries";
            this.grdZoneUpdates.DisplayLayout.BandsSerializer.Add(ultraGridBand4);
            this.grdZoneUpdates.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Etched;
            appearance14.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance14.FontData.BoldAsString = "True";
            appearance14.FontData.Name = "Verdana";
            appearance14.FontData.SizeInPoints = 8F;
            appearance14.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance14.TextHAlignAsString = "Left";
            this.grdZoneUpdates.DisplayLayout.CaptionAppearance = appearance14;
            this.grdZoneUpdates.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdZoneUpdates.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            this.grdZoneUpdates.DisplayLayout.Override.AllowColSwapping = Infragistics.Win.UltraWinGrid.AllowColSwapping.NotAllowed;
            this.grdZoneUpdates.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdZoneUpdates.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            appearance15.BackColor = System.Drawing.SystemColors.Info;
            this.grdZoneUpdates.DisplayLayout.Override.CellAppearance = appearance15;
            this.grdZoneUpdates.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance16.BackColor = System.Drawing.SystemColors.Control;
            appearance16.FontData.BoldAsString = "True";
            appearance16.FontData.Name = "Verdana";
            appearance16.FontData.SizeInPoints = 8F;
            appearance16.TextHAlignAsString = "Left";
            this.grdZoneUpdates.DisplayLayout.Override.HeaderAppearance = appearance16;
            this.grdZoneUpdates.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdZoneUpdates.DisplayLayout.Override.MaxSelectedRows = 0;
            appearance17.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdZoneUpdates.DisplayLayout.Override.RowAppearance = appearance17;
            this.grdZoneUpdates.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdZoneUpdates.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.ExtendedAutoDrag;
            this.grdZoneUpdates.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
            this.grdZoneUpdates.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdZoneUpdates.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdZoneUpdates.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdZoneUpdates.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdZoneUpdates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdZoneUpdates.Location = new System.Drawing.Point(0,0);
            this.grdZoneUpdates.Margin = new System.Windows.Forms.Padding(0);
            this.grdZoneUpdates.Name = "grdZoneUpdates";
            this.grdZoneUpdates.Size = new System.Drawing.Size(422,228);
            this.grdZoneUpdates.TabIndex = 5;
            this.grdZoneUpdates.Text = "Close Zones/Change Lanes";
            this.grdZoneUpdates.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdZoneUpdates.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdZoneUpdates.AfterColPosChanged += new Infragistics.Win.UltraWinGrid.AfterColPosChangedEventHandler(this.OnUpdateColumnHeaderResized);
            this.grdZoneUpdates.Enter += new System.EventHandler(this.OnEnter);
            this.grdZoneUpdates.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.grdZoneUpdates.Leave += new System.EventHandler(this.OnLeave);
            this.grdZoneUpdates.DragOver += new System.Windows.Forms.DragEventHandler(this.OnDragOver);
            this.grdZoneUpdates.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
            this.grdZoneUpdates.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseUp);
            this.grdZoneUpdates.DoubleClick += new System.EventHandler(this.OnUpdateDoubleClicked);
            this.grdZoneUpdates.DragLeave += new System.EventHandler(this.OnDragLeave);
            this.grdZoneUpdates.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnUpdateSelectionChanged);
            this.grdZoneUpdates.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseMove);
            // 
            // tabSchedule
            // 
            this.tabSchedule.Controls.Add(this.dtpScheduleDate);
            this.tabSchedule.Controls.Add(this.grdShipSchedule);
            this.tabSchedule.Location = new System.Drawing.Point(4,4);
            this.tabSchedule.Margin = new System.Windows.Forms.Padding(0);
            this.tabSchedule.Name = "tabSchedule";
            this.tabSchedule.Size = new System.Drawing.Size(422,228);
            this.tabSchedule.TabIndex = 2;
            this.tabSchedule.Text = "Ship Schedule";
            this.tabSchedule.ToolTipText = "Assign to ship schedule";
            // 
            // dtpScheduleDate
            // 
            this.dtpScheduleDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpScheduleDate.CustomFormat = "MMMM dd, yyyy";
            this.dtpScheduleDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpScheduleDate.Location = new System.Drawing.Point(243,0);
            this.dtpScheduleDate.MaxDate = new System.DateTime(2030,12,31,0,0,0,0);
            this.dtpScheduleDate.MinDate = new System.DateTime(2005,1,1,0,0,0,0);
            this.dtpScheduleDate.Name = "dtpScheduleDate";
            this.dtpScheduleDate.Size = new System.Drawing.Size(177,21);
            this.dtpScheduleDate.TabIndex = 1;
            this.dtpScheduleDate.ValueChanged += new System.EventHandler(this.OnScheduleDateChanged);
            this.dtpScheduleDate.DropDown += new System.EventHandler(this.OnCalendarOpened);
            this.dtpScheduleDate.CloseUp += new System.EventHandler(this.OnCalendarClosed);
            // 
            // grdShipSchedule
            // 
            this.grdShipSchedule.ContextMenuStrip = this.ctxTrip;
            this.grdShipSchedule.DataMember = "ShipScheduleMasterTable";
            this.grdShipSchedule.DataSource = this.mShipScheduleDS;
            appearance9.BackColor = System.Drawing.SystemColors.Window;
            appearance9.FontData.Name = "Verdana";
            appearance9.FontData.SizeInPoints = 8F;
            appearance9.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance9.TextHAlignAsString = "Left";
            this.grdShipSchedule.DisplayLayout.Appearance = appearance9;
            ultraGridColumn229.Header.VisiblePosition = 0;
            ultraGridColumn229.Hidden = true;
            ultraGridColumn230.Header.VisiblePosition = 1;
            ultraGridColumn230.Hidden = true;
            ultraGridColumn231.Header.VisiblePosition = 2;
            ultraGridColumn231.Hidden = true;
            ultraGridColumn232.Format = "MM/dd/yyyy hh:mmtt";
            ultraGridColumn232.Header.VisiblePosition = 3;
            ultraGridColumn232.Hidden = true;
            ultraGridColumn232.Width = 120;
            ultraGridColumn233.Header.VisiblePosition = 4;
            ultraGridColumn233.Hidden = true;
            ultraGridColumn233.Width = 96;
            ultraGridColumn234.Header.VisiblePosition = 20;
            ultraGridColumn234.Hidden = true;
            ultraGridColumn235.Header.VisiblePosition = 21;
            ultraGridColumn235.Hidden = true;
            ultraGridColumn236.Header.VisiblePosition = 22;
            ultraGridColumn236.Hidden = true;
            ultraGridColumn237.Header.VisiblePosition = 11;
            ultraGridColumn237.Width = 144;
            ultraGridColumn238.Header.Caption = "Load#";
            ultraGridColumn238.Header.VisiblePosition = 15;
            ultraGridColumn238.Width = 112;
            ultraGridColumn239.Header.VisiblePosition = 23;
            ultraGridColumn239.Hidden = true;
            ultraGridColumn240.Header.Caption = "Trailer#";
            ultraGridColumn240.Header.VisiblePosition = 10;
            ultraGridColumn240.Width = 100;
            ultraGridColumn241.Header.VisiblePosition = 24;
            ultraGridColumn241.Hidden = true;
            ultraGridColumn242.Format = "MM/dd/yyyy hh:mmtt";
            ultraGridColumn242.Header.Caption = "Sched Close";
            ultraGridColumn242.Header.VisiblePosition = 9;
            ultraGridColumn242.Width = 120;
            ultraGridColumn243.Format = "MM/dd/yyyy hh:mmtt";
            ultraGridColumn243.Header.Caption = "Sched Depart";
            ultraGridColumn243.Header.VisiblePosition = 12;
            ultraGridColumn243.Width = 144;
            ultraGridColumn244.Header.VisiblePosition = 25;
            ultraGridColumn244.Hidden = true;
            ultraGridColumn245.Format = "MM/dd/yyyy hh:mmtt";
            ultraGridColumn245.Header.Caption = "All Assigned";
            ultraGridColumn245.Header.VisiblePosition = 17;
            ultraGridColumn245.Hidden = true;
            ultraGridColumn245.NullText = "";
            ultraGridColumn245.Width = 48;
            ultraGridColumn246.Format = "MM/dd/yyyy hh:mmtt";
            ultraGridColumn246.Header.Caption = "Complete";
            ultraGridColumn246.Header.VisiblePosition = 16;
            ultraGridColumn246.Hidden = true;
            ultraGridColumn246.NullText = "";
            ultraGridColumn246.Width = 48;
            ultraGridColumn247.Header.VisiblePosition = 26;
            ultraGridColumn247.Hidden = true;
            ultraGridColumn247.Width = 120;
            ultraGridColumn248.Format = "MM/dd/yyyy hh:mmtt";
            ultraGridColumn248.Header.VisiblePosition = 27;
            ultraGridColumn248.Hidden = true;
            ultraGridColumn248.Width = 120;
            ultraGridColumn249.Format = "MM/dd/yyyy hh:mmtt";
            ultraGridColumn249.Header.VisiblePosition = 28;
            ultraGridColumn249.Hidden = true;
            ultraGridColumn250.Header.VisiblePosition = 29;
            ultraGridColumn250.Hidden = true;
            ultraGridColumn251.Header.VisiblePosition = 30;
            ultraGridColumn251.Hidden = true;
            ultraGridColumn252.Header.VisiblePosition = 31;
            ultraGridColumn252.Hidden = true;
            ultraGridColumn253.Header.VisiblePosition = 32;
            ultraGridColumn253.Hidden = true;
            ultraGridColumn253.Width = 96;
            ultraGridColumn254.Header.Caption = "Stop#";
            ultraGridColumn254.Header.VisiblePosition = 33;
            ultraGridColumn254.Hidden = true;
            ultraGridColumn254.Width = 72;
            ultraGridColumn255.Header.Caption = "AgentID";
            ultraGridColumn255.Header.VisiblePosition = 18;
            ultraGridColumn255.Hidden = true;
            ultraGridColumn256.Header.Caption = "Agent#";
            ultraGridColumn256.Header.VisiblePosition = 19;
            ultraGridColumn256.Hidden = true;
            ultraGridColumn256.Width = 72;
            ultraGridColumn257.Header.Caption = "Zone";
            ultraGridColumn257.Header.VisiblePosition = 5;
            ultraGridColumn257.Width = 72;
            ultraGridColumn258.Header.VisiblePosition = 7;
            ultraGridColumn258.Width = 72;
            ultraGridColumn259.Header.VisiblePosition = 13;
            ultraGridColumn259.Width = 112;
            ultraGridColumn260.Format = "MM/dd/yyyy hh:mmtt";
            ultraGridColumn260.Header.VisiblePosition = 34;
            ultraGridColumn260.Hidden = true;
            ultraGridColumn260.Width = 120;
            ultraGridColumn261.Format = "MM/dd/yyyy hh:mmtt";
            ultraGridColumn261.Header.VisiblePosition = 35;
            ultraGridColumn261.Hidden = true;
            ultraGridColumn261.Width = 120;
            ultraGridColumn262.Header.VisiblePosition = 36;
            ultraGridColumn262.Hidden = true;
            ultraGridColumn263.Header.VisiblePosition = 37;
            ultraGridColumn263.Hidden = true;
            ultraGridColumn264.Header.VisiblePosition = 38;
            ultraGridColumn264.Hidden = true;
            ultraGridColumn265.Header.VisiblePosition = 39;
            ultraGridColumn265.Hidden = true;
            ultraGridColumn266.Header.Caption = "S2 Stop#";
            ultraGridColumn266.Header.VisiblePosition = 40;
            ultraGridColumn266.Hidden = true;
            ultraGridColumn266.Width = 72;
            ultraGridColumn267.Header.Caption = "S2AgentlID";
            ultraGridColumn267.Header.VisiblePosition = 41;
            ultraGridColumn267.Hidden = true;
            ultraGridColumn267.Width = 96;
            ultraGridColumn268.Header.VisiblePosition = 42;
            ultraGridColumn268.Hidden = true;
            ultraGridColumn269.Header.Caption = "S2 Main Zone";
            ultraGridColumn269.Header.VisiblePosition = 43;
            ultraGridColumn269.Hidden = true;
            ultraGridColumn269.Width = 96;
            ultraGridColumn270.Header.Caption = "S2 Tag";
            ultraGridColumn270.Header.VisiblePosition = 8;
            ultraGridColumn270.Width = 60;
            ultraGridColumn271.Header.Caption = "S2 Notes";
            ultraGridColumn271.Header.VisiblePosition = 14;
            ultraGridColumn271.Width = 120;
            ultraGridColumn272.Format = "MM/dd/yyyy hh:mmtt";
            ultraGridColumn272.Header.VisiblePosition = 44;
            ultraGridColumn272.Hidden = true;
            ultraGridColumn272.Width = 120;
            ultraGridColumn273.Format = "MM/dd/yyyy hh:mmtt";
            ultraGridColumn273.Header.VisiblePosition = 45;
            ultraGridColumn273.Hidden = true;
            ultraGridColumn273.Width = 120;
            ultraGridColumn274.Header.VisiblePosition = 46;
            ultraGridColumn274.Hidden = true;
            ultraGridColumn275.Header.VisiblePosition = 47;
            ultraGridColumn275.Hidden = true;
            ultraGridColumn276.Header.VisiblePosition = 48;
            ultraGridColumn276.Hidden = true;
            ultraGridColumn277.Header.VisiblePosition = 49;
            ultraGridColumn277.Hidden = true;
            ultraGridColumn278.Header.VisiblePosition = 50;
            ultraGridColumn278.Hidden = true;
            ultraGridColumn279.Header.Caption = "Client#";
            ultraGridColumn279.Header.VisiblePosition = 6;
            ultraGridColumn279.Width = 60;
            ultraGridColumn280.Header.VisiblePosition = 51;
            ultraGridColumn280.Hidden = true;
            ultraGridColumn281.Header.VisiblePosition = 52;
            ultraGridBand5.Columns.AddRange(new object[] {
            ultraGridColumn229,
            ultraGridColumn230,
            ultraGridColumn231,
            ultraGridColumn232,
            ultraGridColumn233,
            ultraGridColumn234,
            ultraGridColumn235,
            ultraGridColumn236,
            ultraGridColumn237,
            ultraGridColumn238,
            ultraGridColumn239,
            ultraGridColumn240,
            ultraGridColumn241,
            ultraGridColumn242,
            ultraGridColumn243,
            ultraGridColumn244,
            ultraGridColumn245,
            ultraGridColumn246,
            ultraGridColumn247,
            ultraGridColumn248,
            ultraGridColumn249,
            ultraGridColumn250,
            ultraGridColumn251,
            ultraGridColumn252,
            ultraGridColumn253,
            ultraGridColumn254,
            ultraGridColumn255,
            ultraGridColumn256,
            ultraGridColumn257,
            ultraGridColumn258,
            ultraGridColumn259,
            ultraGridColumn260,
            ultraGridColumn261,
            ultraGridColumn262,
            ultraGridColumn263,
            ultraGridColumn264,
            ultraGridColumn265,
            ultraGridColumn266,
            ultraGridColumn267,
            ultraGridColumn268,
            ultraGridColumn269,
            ultraGridColumn270,
            ultraGridColumn271,
            ultraGridColumn272,
            ultraGridColumn273,
            ultraGridColumn274,
            ultraGridColumn275,
            ultraGridColumn276,
            ultraGridColumn277,
            ultraGridColumn278,
            ultraGridColumn279,
            ultraGridColumn280,
            ultraGridColumn281});
            ultraGridBand5.SummaryFooterCaption = "Grand Summaries";
            ultraGridColumn282.Header.VisiblePosition = 0;
            ultraGridColumn282.Hidden = true;
            ultraGridColumn283.Header.VisiblePosition = 1;
            ultraGridColumn283.Hidden = true;
            ultraGridColumn284.Header.Caption = "Stop#";
            ultraGridColumn284.Header.VisiblePosition = 10;
            ultraGridColumn285.Header.VisiblePosition = 14;
            ultraGridColumn285.Hidden = true;
            ultraGridColumn286.Header.VisiblePosition = 15;
            ultraGridColumn286.Hidden = true;
            ultraGridColumn287.Header.VisiblePosition = 16;
            ultraGridColumn287.Hidden = true;
            ultraGridColumn288.Header.VisiblePosition = 6;
            ultraGridColumn288.Width = 60;
            ultraGridColumn289.Header.VisiblePosition = 12;
            ultraGridColumn290.Format = "MM/dd/yyyy HH:mm tt";
            ultraGridColumn290.Header.Caption = "Sched Arrival";
            ultraGridColumn290.Header.VisiblePosition = 11;
            ultraGridColumn291.Format = "MM/dd/yyyy HH:mm tt";
            ultraGridColumn291.Header.Caption = "Sched OFD1";
            ultraGridColumn291.Header.VisiblePosition = 13;
            ultraGridColumn292.Header.VisiblePosition = 17;
            ultraGridColumn292.Hidden = true;
            ultraGridColumn293.Header.VisiblePosition = 18;
            ultraGridColumn293.Hidden = true;
            ultraGridColumn294.Header.VisiblePosition = 19;
            ultraGridColumn294.Hidden = true;
            ultraGridColumn295.Header.Caption = "TL#";
            ultraGridColumn295.Header.VisiblePosition = 4;
            ultraGridColumn295.Width = 72;
            ultraGridColumn296.Header.VisiblePosition = 2;
            ultraGridColumn296.Width = 53;
            ultraGridColumn297.Format = "MM/dd/yyyy";
            ultraGridColumn297.Header.Caption = "Close Date";
            ultraGridColumn297.Header.VisiblePosition = 7;
            ultraGridColumn298.Format = "HH:mm tt";
            ultraGridColumn298.Header.Caption = "Close Time";
            ultraGridColumn298.Header.VisiblePosition = 8;
            ultraGridColumn299.Header.VisiblePosition = 20;
            ultraGridColumn299.Hidden = true;
            ultraGridColumn300.Header.VisiblePosition = 21;
            ultraGridColumn300.Hidden = true;
            ultraGridColumn301.Header.VisiblePosition = 5;
            ultraGridColumn301.Hidden = true;
            ultraGridColumn302.Header.VisiblePosition = 9;
            ultraGridColumn302.Hidden = true;
            ultraGridColumn303.Header.Caption = "Client#";
            ultraGridColumn303.Header.VisiblePosition = 3;
            ultraGridColumn303.Width = 60;
            ultraGridColumn304.Header.VisiblePosition = 22;
            ultraGridColumn304.Hidden = true;
            ultraGridBand6.Columns.AddRange(new object[] {
            ultraGridColumn282,
            ultraGridColumn283,
            ultraGridColumn284,
            ultraGridColumn285,
            ultraGridColumn286,
            ultraGridColumn287,
            ultraGridColumn288,
            ultraGridColumn289,
            ultraGridColumn290,
            ultraGridColumn291,
            ultraGridColumn292,
            ultraGridColumn293,
            ultraGridColumn294,
            ultraGridColumn295,
            ultraGridColumn296,
            ultraGridColumn297,
            ultraGridColumn298,
            ultraGridColumn299,
            ultraGridColumn300,
            ultraGridColumn301,
            ultraGridColumn302,
            ultraGridColumn303,
            ultraGridColumn304});
            this.grdShipSchedule.DisplayLayout.BandsSerializer.Add(ultraGridBand5);
            this.grdShipSchedule.DisplayLayout.BandsSerializer.Add(ultraGridBand6);
            this.grdShipSchedule.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Etched;
            appearance10.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance10.FontData.BoldAsString = "True";
            appearance10.FontData.Name = "Verdana";
            appearance10.FontData.SizeInPoints = 8F;
            appearance10.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance10.TextHAlignAsString = "Left";
            this.grdShipSchedule.DisplayLayout.CaptionAppearance = appearance10;
            this.grdShipSchedule.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdShipSchedule.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            this.grdShipSchedule.DisplayLayout.Override.AllowColSwapping = Infragistics.Win.UltraWinGrid.AllowColSwapping.NotAllowed;
            this.grdShipSchedule.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdShipSchedule.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdShipSchedule.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance11.BackColor = System.Drawing.SystemColors.Control;
            appearance11.FontData.BoldAsString = "True";
            appearance11.FontData.Name = "Verdana";
            appearance11.FontData.SizeInPoints = 8F;
            appearance11.TextHAlignAsString = "Left";
            this.grdShipSchedule.DisplayLayout.Override.HeaderAppearance = appearance11;
            this.grdShipSchedule.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdShipSchedule.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance12.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdShipSchedule.DisplayLayout.Override.RowAppearance = appearance12;
            this.grdShipSchedule.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdShipSchedule.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdShipSchedule.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Hide;
            this.grdShipSchedule.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdShipSchedule.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdShipSchedule.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdShipSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdShipSchedule.Font = new System.Drawing.Font("Verdana",8.25F);
            this.grdShipSchedule.Location = new System.Drawing.Point(0,0);
            this.grdShipSchedule.Margin = new System.Windows.Forms.Padding(0);
            this.grdShipSchedule.Name = "grdShipSchedule";
            this.grdShipSchedule.Size = new System.Drawing.Size(422,228);
            this.grdShipSchedule.TabIndex = 0;
            this.grdShipSchedule.Text = "Ship Schedule";
            this.grdShipSchedule.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdShipSchedule.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdShipSchedule.Enter += new System.EventHandler(this.OnEnter);
            this.grdShipSchedule.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.grdShipSchedule.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnGridInitializeRow);
            this.grdShipSchedule.Leave += new System.EventHandler(this.OnLeave);
            this.grdShipSchedule.DragOver += new System.Windows.Forms.DragEventHandler(this.OnDragOver);
            this.grdShipSchedule.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
            this.grdShipSchedule.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseUp);
            this.grdShipSchedule.DoubleClick += new System.EventHandler(this.OnTripDoubleClicked);
            this.grdShipSchedule.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnGridInitializeLayout);
            this.grdShipSchedule.DragLeave += new System.EventHandler(this.OnDragLeave);
            this.grdShipSchedule.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnTripSelectionChanged);
            this.grdShipSchedule.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseMove);
            // 
            // mShipScheduleDS
            // 
            this.mShipScheduleDS.DataSetName = "ShipScheduleDS";
            this.mShipScheduleDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mShipScheduleDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // stbMain
            // 
            this.stbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.stbMain.Font = new System.Drawing.Font("Verdana",8.25F);
            this.stbMain.Location = new System.Drawing.Point(0,303);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(760,24);
            this.stbMain.StatusText = "";
            this.stbMain.TabIndex = 3;
            this.stbMain.TerminalText = "Terminal";
            // 
            // splitterV
            // 
            this.splitterV.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.splitterV.Location = new System.Drawing.Point(327,49);
            this.splitterV.Name = "splitterV";
            this.splitterV.Size = new System.Drawing.Size(3,254);
            this.splitterV.TabIndex = 11;
            this.splitterV.TabStop = false;
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuView,
            this.mnuOp,
            this.mnuReports,
            this.mnuTools,
            this.mnuHelp});
            this.msMain.Location = new System.Drawing.Point(0,0);
            this.msMain.Name = "msMain";
            this.msMain.Padding = new System.Windows.Forms.Padding(0);
            this.msMain.Size = new System.Drawing.Size(760,24);
            this.msMain.TabIndex = 12;
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileNew,
            this.mnuFileOpen,
            this.mnuFileSep1,
            this.mnuFileSave,
            this.mnuFileSaveAs,
            this.mnuFileSep2,
            this.mnuFileSetup,
            this.mnuFilePrint,
            this.mnuFilePreview,
            this.mnuFileSep3,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37,24);
            this.mnuFile.Text = "File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.mnuFileNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.Size = new System.Drawing.Size(152,22);
            this.mnuFileNew.Text = "&New...";
            this.mnuFileNew.ToolTipText = "New";
            this.mnuFileNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Image = global::Argix.Properties.Resources.Open;
            this.mnuFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(152,22);
            this.mnuFileOpen.Text = "&Open...";
            this.mnuFileOpen.ToolTipText = "Open";
            this.mnuFileOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep1
            // 
            this.mnuFileSep1.Name = "mnuFileSep1";
            this.mnuFileSep1.Size = new System.Drawing.Size(149,6);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Image = global::Argix.Properties.Resources.Save;
            this.mnuFileSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.Size = new System.Drawing.Size(152,22);
            this.mnuFileSave.Text = "&Save";
            this.mnuFileSave.ToolTipText = "Save";
            this.mnuFileSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.Size = new System.Drawing.Size(152,22);
            this.mnuFileSaveAs.Text = "Save &As...";
            this.mnuFileSaveAs.ToolTipText = "Save as";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep2
            // 
            this.mnuFileSep2.Name = "mnuFileSep2";
            this.mnuFileSep2.Size = new System.Drawing.Size(149,6);
            // 
            // mnuFileSetup
            // 
            this.mnuFileSetup.Image = global::Argix.Properties.Resources.PrintSetup;
            this.mnuFileSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSetup.Name = "mnuFileSetup";
            this.mnuFileSetup.Size = new System.Drawing.Size(152,22);
            this.mnuFileSetup.Text = "Page Set&up...";
            this.mnuFileSetup.ToolTipText = "Modify print layout";
            this.mnuFileSetup.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFilePrint
            // 
            this.mnuFilePrint.Image = global::Argix.Properties.Resources.Print;
            this.mnuFilePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePrint.Name = "mnuFilePrint";
            this.mnuFilePrint.Size = new System.Drawing.Size(152,22);
            this.mnuFilePrint.Text = "&Print...";
            this.mnuFilePrint.ToolTipText = "Print";
            this.mnuFilePrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFilePreview
            // 
            this.mnuFilePreview.Image = global::Argix.Properties.Resources.PrintPreview;
            this.mnuFilePreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePreview.Name = "mnuFilePreview";
            this.mnuFilePreview.Size = new System.Drawing.Size(152,22);
            this.mnuFilePreview.Text = "Print P&review...";
            this.mnuFilePreview.ToolTipText = "Print preview";
            this.mnuFilePreview.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep3
            // 
            this.mnuFileSep3.Name = "mnuFileSep3";
            this.mnuFileSep3.Size = new System.Drawing.Size(149,6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(152,22);
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
            this.mnuEditFind,
            this.mnuEditFindTL});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(39,24);
            this.mnuEdit.Text = "Edit";
            // 
            // mnuEditCut
            // 
            this.mnuEditCut.Image = global::Argix.Properties.Resources.Cut;
            this.mnuEditCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditCut.Name = "mnuEditCut";
            this.mnuEditCut.Size = new System.Drawing.Size(219,22);
            this.mnuEditCut.Text = "Cu&t";
            this.mnuEditCut.ToolTipText = "Cut the selected text";
            this.mnuEditCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditCopy
            // 
            this.mnuEditCopy.Image = global::Argix.Properties.Resources.Copy;
            this.mnuEditCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditCopy.Name = "mnuEditCopy";
            this.mnuEditCopy.Size = new System.Drawing.Size(219,22);
            this.mnuEditCopy.Text = "&Copy";
            this.mnuEditCopy.ToolTipText = "Copy the selected text";
            this.mnuEditCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditPaste
            // 
            this.mnuEditPaste.Image = global::Argix.Properties.Resources.Paste;
            this.mnuEditPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditPaste.Name = "mnuEditPaste";
            this.mnuEditPaste.Size = new System.Drawing.Size(219,22);
            this.mnuEditPaste.Text = "&Paste";
            this.mnuEditPaste.ToolTipText = "Paste text into the current selection";
            this.mnuEditPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditSep1
            // 
            this.mnuEditSep1.Name = "mnuEditSep1";
            this.mnuEditSep1.Size = new System.Drawing.Size(216,6);
            // 
            // mnuEditFind
            // 
            this.mnuEditFind.Image = global::Argix.Properties.Resources.Find;
            this.mnuEditFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditFind.Name = "mnuEditFind";
            this.mnuEditFind.Size = new System.Drawing.Size(219,22);
            this.mnuEditFind.Text = "Search for a Zone";
            this.mnuEditFind.ToolTipText = "Search for a zone";
            this.mnuEditFind.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditFindTL
            // 
            this.mnuEditFindTL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditFindTL.Name = "mnuEditFindTL";
            this.mnuEditFindTL.Size = new System.Drawing.Size(219,22);
            this.mnuEditFindTL.Text = "Search for an Assigned TL...";
            this.mnuEditFindTL.ToolTipText = "Search for an assigned TL";
            this.mnuEditFindTL.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewRefresh,
            this.mnuViewSep1,
            this.mnuViewZoneType,
            this.mnuViewSep2,
            this.mnuViewToolbar,
            this.mnuViewStatusBar});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(44,24);
            this.mnuView.Text = "View";
            // 
            // mnuViewRefresh
            // 
            this.mnuViewRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.mnuViewRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewRefresh.Name = "mnuViewRefresh";
            this.mnuViewRefresh.Size = new System.Drawing.Size(130,22);
            this.mnuViewRefresh.Text = "&Refresh";
            this.mnuViewRefresh.ToolTipText = "refresh the active grid";
            this.mnuViewRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewSep1
            // 
            this.mnuViewSep1.Name = "mnuViewSep1";
            this.mnuViewSep1.Size = new System.Drawing.Size(127,6);
            // 
            // mnuViewZoneType
            // 
            this.mnuViewZoneType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewZoneTypeTsort,
            this.mnuViewZoneTypeReturns,
            this.mnuViewZoneTypeAll});
            this.mnuViewZoneType.Image = global::Argix.Properties.Resources.Legend;
            this.mnuViewZoneType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewZoneType.Name = "mnuViewZoneType";
            this.mnuViewZoneType.Size = new System.Drawing.Size(130,22);
            this.mnuViewZoneType.Text = "Zone Type";
            this.mnuViewZoneType.ToolTipText = "Filter TLs by zone type";
            // 
            // mnuViewZoneTypeTsort
            // 
            this.mnuViewZoneTypeTsort.Name = "mnuViewZoneTypeTsort";
            this.mnuViewZoneTypeTsort.Size = new System.Drawing.Size(114,22);
            this.mnuViewZoneTypeTsort.Text = "Tsort";
            this.mnuViewZoneTypeTsort.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewZoneTypeReturns
            // 
            this.mnuViewZoneTypeReturns.Name = "mnuViewZoneTypeReturns";
            this.mnuViewZoneTypeReturns.Size = new System.Drawing.Size(114,22);
            this.mnuViewZoneTypeReturns.Text = "Returns";
            this.mnuViewZoneTypeReturns.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewZoneTypeAll
            // 
            this.mnuViewZoneTypeAll.Name = "mnuViewZoneTypeAll";
            this.mnuViewZoneTypeAll.Size = new System.Drawing.Size(114,22);
            this.mnuViewZoneTypeAll.Text = "All";
            this.mnuViewZoneTypeAll.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewSep2
            // 
            this.mnuViewSep2.Name = "mnuViewSep2";
            this.mnuViewSep2.Size = new System.Drawing.Size(127,6);
            // 
            // mnuViewToolbar
            // 
            this.mnuViewToolbar.Name = "mnuViewToolbar";
            this.mnuViewToolbar.Size = new System.Drawing.Size(130,22);
            this.mnuViewToolbar.Text = "&Toolbar";
            this.mnuViewToolbar.ToolTipText = "Open/close the toolbar";
            this.mnuViewToolbar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewStatusBar
            // 
            this.mnuViewStatusBar.Name = "mnuViewStatusBar";
            this.mnuViewStatusBar.Size = new System.Drawing.Size(130,22);
            this.mnuViewStatusBar.Text = "&Status Bar";
            this.mnuViewStatusBar.ToolTipText = "Open/close the status bar";
            this.mnuViewStatusBar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuOp
            // 
            this.mnuOp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpAdd,
            this.mnuOpRem,
            this.mnuOpChangeLanes,
            this.mnuOpCloseZones,
            this.mnuOpSep1,
            this.mnuOpOpen,
            this.mnuOpCloseAllTLs,
            this.mnuOpClose,
            this.mnuOpSep2,
            this.mnuOpAssign,
            this.mnuOpUnassign,
            this.mnuOpMove});
            this.mnuOp.Name = "mnuOp";
            this.mnuOp.Size = new System.Drawing.Size(72,24);
            this.mnuOp.Text = "Operation";
            // 
            // mnuOpAdd
            // 
            this.mnuOpAdd.Image = global::Argix.Properties.Resources.BuilderDialog_add;
            this.mnuOpAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuOpAdd.Name = "mnuOpAdd";
            this.mnuOpAdd.Size = new System.Drawing.Size(148,22);
            this.mnuOpAdd.Text = "Add";
            this.mnuOpAdd.ToolTipText = "Add selected TLs for update";
            this.mnuOpAdd.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuOpRem
            // 
            this.mnuOpRem.Image = global::Argix.Properties.Resources.BuilderDialog_remove;
            this.mnuOpRem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuOpRem.Name = "mnuOpRem";
            this.mnuOpRem.Size = new System.Drawing.Size(148,22);
            this.mnuOpRem.Text = "Remove";
            this.mnuOpRem.ToolTipText = "Remove selected TLs from update";
            this.mnuOpRem.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuOpChangeLanes
            // 
            this.mnuOpChangeLanes.Image = global::Argix.Properties.Resources.lanes;
            this.mnuOpChangeLanes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuOpChangeLanes.Name = "mnuOpChangeLanes";
            this.mnuOpChangeLanes.Size = new System.Drawing.Size(148,22);
            this.mnuOpChangeLanes.Text = "Change Lanes";
            this.mnuOpChangeLanes.ToolTipText = "Change lanes for all TLs in the update grid";
            this.mnuOpChangeLanes.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuOpCloseZones
            // 
            this.mnuOpCloseZones.Image = global::Argix.Properties.Resources.zones;
            this.mnuOpCloseZones.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuOpCloseZones.Name = "mnuOpCloseZones";
            this.mnuOpCloseZones.Size = new System.Drawing.Size(148,22);
            this.mnuOpCloseZones.Text = "Close Zones";
            this.mnuOpCloseZones.ToolTipText = "Close zones and change lanes for all TLs in the update grid";
            this.mnuOpCloseZones.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuOpSep1
            // 
            this.mnuOpSep1.Name = "mnuOpSep1";
            this.mnuOpSep1.Size = new System.Drawing.Size(145,6);
            // 
            // mnuOpOpen
            // 
            this.mnuOpOpen.Image = global::Argix.Properties.Resources.book_open;
            this.mnuOpOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuOpOpen.Name = "mnuOpOpen";
            this.mnuOpOpen.Size = new System.Drawing.Size(148,22);
            this.mnuOpOpen.Text = "Open Trip";
            this.mnuOpOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuOpCloseAllTLs
            // 
            this.mnuOpCloseAllTLs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuOpCloseAllTLs.Name = "mnuOpCloseAllTLs";
            this.mnuOpCloseAllTLs.Size = new System.Drawing.Size(148,22);
            this.mnuOpCloseAllTLs.Text = "Close All TLs";
            this.mnuOpCloseAllTLs.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuOpClose
            // 
            this.mnuOpClose.Image = global::Argix.Properties.Resources.book_angle;
            this.mnuOpClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuOpClose.Name = "mnuOpClose";
            this.mnuOpClose.Size = new System.Drawing.Size(148,22);
            this.mnuOpClose.Text = "Close Trip";
            this.mnuOpClose.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuOpSep2
            // 
            this.mnuOpSep2.Name = "mnuOpSep2";
            this.mnuOpSep2.Size = new System.Drawing.Size(145,6);
            // 
            // mnuOpAssign
            // 
            this.mnuOpAssign.Image = global::Argix.Properties.Resources.Edit_Redo;
            this.mnuOpAssign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuOpAssign.Name = "mnuOpAssign";
            this.mnuOpAssign.Size = new System.Drawing.Size(148,22);
            this.mnuOpAssign.Text = "Assign TL";
            this.mnuOpAssign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuOpUnassign
            // 
            this.mnuOpUnassign.Image = global::Argix.Properties.Resources.Edit_Undo;
            this.mnuOpUnassign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuOpUnassign.Name = "mnuOpUnassign";
            this.mnuOpUnassign.Size = new System.Drawing.Size(148,22);
            this.mnuOpUnassign.Text = "Unassign TL";
            this.mnuOpUnassign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuOpMove
            // 
            this.mnuOpMove.Image = global::Argix.Properties.Resources.MoveToFolder;
            this.mnuOpMove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuOpMove.Name = "mnuOpMove";
            this.mnuOpMove.Size = new System.Drawing.Size(148,22);
            this.mnuOpMove.Text = "Move TL";
            this.mnuOpMove.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuReports
            // 
            this.mnuReports.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuReportsZonesByLane});
            this.mnuReports.Name = "mnuReports";
            this.mnuReports.Size = new System.Drawing.Size(59,24);
            this.mnuReports.Text = "Reports";
            // 
            // mnuReportsZonesByLane
            // 
            this.mnuReportsZonesByLane.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuReportsZonesByLane.Name = "mnuReportsZonesByLane";
            this.mnuReportsZonesByLane.Size = new System.Drawing.Size(159,22);
            this.mnuReportsZonesByLane.Text = "&Zones by Lane...";
            this.mnuReportsZonesByLane.ToolTipText = "Open the Zones by Lane report";
            this.mnuReportsZonesByLane.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsConfig});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(48,24);
            this.mnuTools.Text = "Tools";
            // 
            // mnuToolsConfig
            // 
            this.mnuToolsConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuToolsConfig.Name = "mnuToolsConfig";
            this.mnuToolsConfig.Size = new System.Drawing.Size(157,22);
            this.mnuToolsConfig.Text = "&Configuration...";
            this.mnuToolsConfig.ToolTipText = "Access the application configuration";
            this.mnuToolsConfig.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpAbout,
            this.mnuHelpSep1});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44,24);
            this.mnuHelp.Text = "Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(189,22);
            this.mnuHelpAbout.Text = "&About Zone Closing...";
            this.mnuHelpAbout.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuHelpSep1
            // 
            this.mnuHelpSep1.Name = "mnuHelpSep1";
            this.mnuHelpSep1.Size = new System.Drawing.Size(186,6);
            // 
            // btnZoneType
            // 
            this.btnZoneType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoneType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnZoneTypeTsort,
            this.btnZoneTypeReturns,
            this.btnZoneTypeAll});
            this.btnZoneType.Image = global::Argix.Properties.Resources.Legend;
            this.btnZoneType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoneType.Name = "btnZoneType";
            this.btnZoneType.Size = new System.Drawing.Size(29,22);
            this.btnZoneType.ToolTipText = "Zone Type";
            // 
            // btnZoneTypeTsort
            // 
            this.btnZoneTypeTsort.Name = "btnZoneTypeTsort";
            this.btnZoneTypeTsort.Size = new System.Drawing.Size(114,22);
            this.btnZoneTypeTsort.Text = "Tsort";
            this.btnZoneTypeTsort.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnZoneTypeReturns
            // 
            this.btnZoneTypeReturns.Name = "btnZoneTypeReturns";
            this.btnZoneTypeReturns.Size = new System.Drawing.Size(114,22);
            this.btnZoneTypeReturns.Text = "Returns";
            this.btnZoneTypeReturns.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnZoneTypeAll
            // 
            this.btnZoneTypeAll.Name = "btnZoneTypeAll";
            this.btnZoneTypeAll.Size = new System.Drawing.Size(114,22);
            this.btnZoneTypeAll.Text = "All";
            this.btnZoneTypeAll.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsMain
            // 
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnOpen_,
            this.btnSep1,
            this.btnPrint,
            this.btnSearch,
            this.btnSep2,
            this.btnRefresh,
            this.btnZoneType,
            this.btnSep3,
            this.btnAdd,
            this.btnRem,
            this.btnChangeLanes,
            this.btnCloseZones,
            this.btnSep4,
            this.btnOpen,
            this.btnClose,
            this.btnAssign,
            this.btnUnassign,
            this.btnMove});
            this.tsMain.Location = new System.Drawing.Point(0,24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Padding = new System.Windows.Forms.Padding(0);
            this.tsMain.Size = new System.Drawing.Size(760,25);
            this.tsMain.TabIndex = 13;
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(23,22);
            this.btnNew.ToolTipText = "New...";
            this.btnNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnOpen_
            // 
            this.btnOpen_.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen_.Image = global::Argix.Properties.Resources.Open;
            this.btnOpen_.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen_.Name = "btnOpen_";
            this.btnOpen_.Size = new System.Drawing.Size(23,22);
            this.btnOpen_.ToolTipText = "Open...";
            this.btnOpen_.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep1
            // 
            this.btnSep1.Name = "btnSep1";
            this.btnSep1.Size = new System.Drawing.Size(6,25);
            // 
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrint.Image = global::Argix.Properties.Resources.Print;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(23,22);
            this.btnPrint.ToolTipText = "Print...";
            this.btnPrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSearch
            // 
            this.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSearch.Image = global::Argix.Properties.Resources.Find;
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(23,22);
            this.btnSearch.ToolTipText = "Search...";
            this.btnSearch.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep2
            // 
            this.btnSep2.Name = "btnSep2";
            this.btnSep2.Size = new System.Drawing.Size(6,25);
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23,22);
            this.btnRefresh.ToolTipText = "Refresh view";
            this.btnRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep3
            // 
            this.btnSep3.Name = "btnSep3";
            this.btnSep3.Size = new System.Drawing.Size(6,25);
            // 
            // btnAdd
            // 
            this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI",9F,System.Drawing.FontStyle.Bold);
            this.btnAdd.Image = global::Argix.Properties.Resources.BuilderDialog_add;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(23,22);
            this.btnAdd.ToolTipText = "Add TL to selection";
            this.btnAdd.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnRem
            // 
            this.btnRem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRem.Font = new System.Drawing.Font("Segoe UI",9F,System.Drawing.FontStyle.Bold);
            this.btnRem.Image = global::Argix.Properties.Resources.BuilderDialog_remove;
            this.btnRem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRem.Name = "btnRem";
            this.btnRem.Size = new System.Drawing.Size(23,22);
            this.btnRem.ToolTipText = "Remove TL from selection";
            this.btnRem.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnChangeLanes
            // 
            this.btnChangeLanes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnChangeLanes.Image = global::Argix.Properties.Resources.lanes;
            this.btnChangeLanes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnChangeLanes.Name = "btnChangeLanes";
            this.btnChangeLanes.Size = new System.Drawing.Size(23,22);
            this.btnChangeLanes.ToolTipText = "Change lanes only";
            this.btnChangeLanes.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnCloseZones
            // 
            this.btnCloseZones.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCloseZones.Image = global::Argix.Properties.Resources.zones;
            this.btnCloseZones.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCloseZones.Name = "btnCloseZones";
            this.btnCloseZones.Size = new System.Drawing.Size(23,22);
            this.btnCloseZones.ToolTipText = "Change lane AND close zones";
            this.btnCloseZones.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep4
            // 
            this.btnSep4.Name = "btnSep4";
            this.btnSep4.Size = new System.Drawing.Size(6,25);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = global::Argix.Properties.Resources.book_open;
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23,22);
            this.btnOpen.ToolTipText = "Open the closed trip";
            this.btnOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnClose
            // 
            this.btnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClose.Image = global::Argix.Properties.Resources.book_angle;
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(23,22);
            this.btnClose.ToolTipText = "Close the open trip";
            this.btnClose.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnAssign
            // 
            this.btnAssign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAssign.Image = global::Argix.Properties.Resources.Edit_Redo;
            this.btnAssign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAssign.Name = "btnAssign";
            this.btnAssign.Size = new System.Drawing.Size(23,22);
            this.btnAssign.ToolTipText = "Assign TL to ship schedule";
            this.btnAssign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnUnassign
            // 
            this.btnUnassign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUnassign.Image = global::Argix.Properties.Resources.Edit_Undo;
            this.btnUnassign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnassign.Name = "btnUnassign";
            this.btnUnassign.Size = new System.Drawing.Size(23,22);
            this.btnUnassign.ToolTipText = "Unassign TL from ship schedule";
            this.btnUnassign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnMove
            // 
            this.btnMove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMove.Image = global::Argix.Properties.Resources.MoveToFolder;
            this.btnMove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(23,22);
            this.btnMove.ToolTipText = "Move TL to another ship schedule";
            this.btnMove.Click += new System.EventHandler(this.OnItemClick);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.ClientSize = new System.Drawing.Size(760,327);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.splitterV);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.stbMain);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.Font = new System.Drawing.Font("Verdana",8.25F);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Argix Direct Zone Closing";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTLs)).EndInit();
            this.ctxZone.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mZoneDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdZones)).EndInit();
            this.grdZones.ResumeLayout(false);
            this.grdZones.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabLanes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mLaneDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLaneUpdates)).EndInit();
            this.ctxTrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mUpdateDS)).EndInit();
            this.tabZones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdZoneUpdates)).EndInit();
            this.tabSchedule.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdShipSchedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mShipScheduleDS)).EndInit();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
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
				this.mToolTip.SetToolTip(this.cboLane, "Change lane assignments");
				this.mToolTip.SetToolTip(this.cboSmallLane, "Change small lane assignments");
				#endregion
                
                //Set control defaults
                #region Grid Overrides
				this.grdZones.AllowDrop = true;
				this.grdZones.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdZones.DisplayLayout.Bands[0].Columns["Zone"].SortIndicator = SortIndicator.Ascending;
				this.grdTLs.AllowDrop = false;
				this.grdTLs.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdTLs.DisplayLayout.Bands[0].Columns["Zone"].SortIndicator = SortIndicator.Ascending;
				this.grdLaneUpdates.AllowDrop = true;
				this.grdLaneUpdates.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdLaneUpdates.DisplayLayout.Bands[0].Columns["Zone"].SortIndicator = SortIndicator.Ascending;
				this.grdZoneUpdates.AllowDrop = true;
				this.grdZoneUpdates.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdZoneUpdates.DisplayLayout.Bands[0].Columns["Zone"].SortIndicator = SortIndicator.Ascending;
				this.grdShipSchedule.AllowDrop = true;
				this.grdShipSchedule.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdShipSchedule.DisplayLayout.Bands[0].Columns["MainZone"].SortIndicator = SortIndicator.Ascending;
				this.grdShipSchedule.DisplayLayout.Bands[1].Columns["Zone"].SortIndicator = SortIndicator.Ascending;
				#endregion
                this.mLaneDS.Merge(FreightFactory.GetLanes());

				//Modify display as required by configuration
                if(!App.Config.ShowShipSchedule) 
					this.tabMain.TabPages.Remove(this.tabSchedule);
                if(!App.Config.ShowLaneChanges) {
					this.tabMain.TabPages.Remove(this.tabLanes);
					this.mnuOpChangeLanes.Visible = false;
					this.btnChangeLanes.Visible = false;
					this.cboLane.Visible = this.cboSmallLane.Visible = false;
					this.grdZoneUpdates.DisplayLayout.Bands[0].Columns["NewLane"].Hidden = true;
					this.grdZoneUpdates.DisplayLayout.Bands[0].Columns["NewSmallSortLane"].Hidden = true;
				}
				
				//Configure zone and ship schedule views
                this.grdZones.DataMember = FreightFactory.TBL_ZONES;
				this.grdZones.DataSource = FreightFactory.Zones;
                this.grdTLs.DataMember = FreightFactory.TBL_TLS_CLOSED;
				this.grdTLs.DataSource = FreightFactory.TLs;
				this.mnuViewZoneTypeAll.PerformClick();
                this.grdShipSchedule.DataMember = AgentLineHaulFactory.TBL_SHIPSCHEDULE_SCHEDULE;
				this.grdShipSchedule.DataSource = AgentLineHaulFactory.Trips;
				this.dtpScheduleDate.Value = AgentLineHaulFactory.ScheduleDate;
				OnTabSelectedIndexChanged(this.tabMain, EventArgs.Empty);
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnFormClosing(object sender, FormClosingEventArgs e) {
            //Ask only if there are detail forms open
            if(!e.Cancel) {
                #region Save user preferences
                global::Argix.Properties.Settings.Default.WindowState = this.WindowState;
                global::Argix.Properties.Settings.Default.Location = this.Location;
                global::Argix.Properties.Settings.Default.Size = this.Size;
                global::Argix.Properties.Settings.Default.Toolbar = this.mnuViewToolbar.Checked;
                global::Argix.Properties.Settings.Default.StatusBar = this.mnuViewStatusBar.Checked;
                global::Argix.Properties.Settings.Default.LastVersion = App.Version;
                global::Argix.Properties.Settings.Default.Save();
                #endregion
                ArgixTrace.WriteLine(new TraceMessage(App.Version,App.Product,LogLevel.Information,"App Stopped"));
            }
        }
        private void OnTabSelectedIndexChanged(object sender,System.EventArgs e) {
			//Event handler for change in view tab selected index
			this.Cursor = Cursors.WaitCursor;
			try {
				this.grdZones.DisplayLayout.Bands[0].ColumnFilters["AssignedToShipScde"].FilterConditions.Clear();
				switch(this.tabMain.SelectedTab.Name) {
					case "tabLanes":	
						//Move lane comboboxes to the lane change view; refresh zone view
						this.grdLaneUpdates.Controls.Remove(this.cboLane);
						this.grdLaneUpdates.Controls.Remove(this.cboSmallLane);
						this.grdLaneUpdates.Controls.AddRange(new Control[]{this.cboLane,this.cboSmallLane});
						OnUpdateColumnHeaderResized(this.grdLaneUpdates, null);
						this.grdTLs.Visible = this.splitterH.Visible = false;
						break;
					case "tabZones":	
						//Move lane comboboxes to the lane change/zone update view; refresh zone view
						this.grdZoneUpdates.Controls.Remove(this.cboLane);
						this.grdZoneUpdates.Controls.Remove(this.cboSmallLane);
						this.grdZoneUpdates.Controls.AddRange(new Control[]{this.cboLane,this.cboSmallLane});
						OnUpdateColumnHeaderResized(this.grdZoneUpdates, null);
                        if(App.Config.ShowShipSchedule) this.grdZones.DisplayLayout.Bands[0].ColumnFilters["AssignedToShipScde"].FilterConditions.Add(FilterComparisionOperator.NotEquals,"Always"); 
						this.grdTLs.Visible = this.splitterH.Visible = false;
						break;
					case "tabSchedule":	
						//Refresh ship schedule
                        this.grdTLs.Visible = this.splitterH.Visible = true;
                        AgentLineHaulFactory.AgentTerminalID = 0;
                        AgentLineHaulFactory.RefreshTrips();
						FreightFactory.RefreshTLs();
						this.grdTLs.Selected.Rows.Clear();
						break;
				}
				this.grdZones.DisplayLayout.RefreshFilters();
				FreightFactory.RefreshZones(this.tabMain.SelectedTab.Name == "tabSchedule");
				this.grdZones.Selected.Rows.Clear();
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Warning); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		#region Zone Grid: OnZonesChanged(), OnZoneSelectionChanged(), OnZoneDoubleClicked(), OnSearchValueChanged()
		private void OnZonesChanged(object sender, EventArgs e) {
			//Event handler for change in zones collection
			try {
                //Configure and update zone view; clear update selections since they live again in mZoneDS after refresh
				this.mMessageMgr.AddMessage("Loading zones...");
				if(this.mUpdateDS != null) this.mUpdateDS.Clear();
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		private void OnBeforeZoneSelectionChanged(object sender, Infragistics.Win.UltraWinGrid.BeforeSelectChangeEventArgs e) {
			//Event handler for change in zone row selections
			this.Cursor = Cursors.WaitCursor;
			try {
				//Steal focus from search textbox
				if(this.tabMain.SelectedTab.Name == "tabSchedule") {
					//Validate only TLs for single agent are selected
					if(this.grdZones.Selected.Rows.Count > 0) {
						long _agent = Convert.ToInt64(this.grdZones.Selected.Rows[0].Cells["AgentTerminalID"].Value);
						if(this.grdZones.Selected.Rows.Count > 0 && e.NewSelections.Rows.Count == 1) {
							//Case where user is simply changing selection
							e.Cancel = false;
						}
						else {
							//Verify all new selections have the same agent
							for(int i=0; i<e.NewSelections.Rows.Count; i++) {
								long agent = Convert.ToInt64(e.NewSelections.Rows[i].Cells["AgentTerminalID"].Value);
								if(agent != _agent) {
									//Differnet agent- notify and cancel selecting, drag drop, etc
									MessageBox.Show(this, "When more than one TL is selected, all selections must be from the same agent.", App.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
									e.Cancel = true;
									this.mIsDragging = false; 
									break;
								}
							}
						}
					}
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnZoneSelectionChanged(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in zone row selections
			this.Cursor = Cursors.WaitCursor;
			try {
				//Proceed only if the user changed zone selection or searched for a zone 
				//while using the ship schedule
				if((this.grdZones.Focused || this.txtSearchSort.Focused) && this.tabMain.SelectedTab.Name == "tabSchedule") {
					//Update selected agent on ship schedule (for ship schedule filtering);
					//Clear TL row selections to keep filter condition consistent
					long id = (this.grdZones.Selected.Rows.Count > 0) ? Convert.ToInt64(this.grdZones.Selected.Rows[0].Cells["AgentTerminalID"].Value) : 0;
					AgentLineHaulFactory.AgentTerminalID = id;
					this.grdTLs.Selected.Rows.Clear();
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnZoneDoubleClicked(object sender, System.EventArgs e) {
			//Event handler for zone double clicked
			try {
				//Enforce single selection on double-click
				if(this.grdZones.Selected.Rows.Count > 1) {
					try {
                        this.mGridSvcShipSchedule.CaptureState();
                        this.grdZones.Selected.Rows.Clear();
                        this.grdZones.Selected.Rows[0].Selected = true;
					}
					catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
                    finally { this.mGridSvcShipSchedule.RestoreState(); }
				}
				
				//Move selected records between zone and update grids
				if(this.grdZones.Selected.Rows.Count > 0 && this.grdZones.ActiveRow != null) {
					switch(this.tabMain.SelectedTab.Name) {
						case "tabZones":
						case "tabLanes":	if(this.ctxZoneAdd.Enabled) addZonesToUpdate(); break;
						case "tabSchedule": if(this.ctxZoneAssign.Enabled) assignTL(); break;
					}
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		private void OnSearchValueChanged(object sender, System.EventArgs e) {
			//Event handler for change in search text value; normally, this is handled exclusively by
			//the UltraGridSvc mGridSvcZones; but with multiselect on, we need to clear prior selections
			try { this.grdZones.Selected.Rows.Clear(); this.grdZones.Focus(); } catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		#endregion
		#region TL Grid: OnTLsChanged(), OnTLSelectionChanged(), OnTLDoubleClicked()
		private void OnTLsChanged(object sender, EventArgs e) {
			//Event handler for change in zones collection
			try {
				//Configure and update zone view
				this.mMessageMgr.AddMessage("Loading closed TLs...");
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		private void OnTLSelectionChanged(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in TL row selection
			this.Cursor = Cursors.WaitCursor;
			try {
				//Proceed only if the user changed TL selection while viewing the ship schedule
				if(this.grdTLs.Focused && this.tabMain.SelectedTab.Name == "tabSchedule") {
					//Update selected agent on ship schedule (for ship schedule filtering);
					//Clear zone row selections to keep filter condition consistent
					long id = (this.grdTLs.Selected.Rows.Count > 0) ? Convert.ToInt64(this.grdTLs.Selected.Rows[0].Cells["AgentTerminalID"].Value) : 0;
					AgentLineHaulFactory.AgentTerminalID = id;
					this.grdZones.Selected.Rows.Clear();
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnTLDoubleClicked(object sender, System.EventArgs e) {
			//Event handler for zone double clicked
			try {
				//Enforce single selection on double-click
				if(this.grdTLs.Selected.Rows.Count > 1) {
					try {
                        this.mGridSvcShipSchedule.CaptureState();
                        this.grdTLs.Selected.Rows.Clear();
                        this.grdTLs.Selected.Rows[0].Selected = true;
					}
					catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
                    finally { this.mGridSvcShipSchedule.RestoreState(); }
				}
				
				//Move selected records between zone and update grids
				if(this.grdTLs.Selected.Rows.Count > 0 && this.grdTLs.ActiveRow != null) {
					if(this.ctxZoneAssign.Enabled) assignTL(); 
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		#endregion
		#region Update Grid: OnUpdateSelectionChanged(), OnUpdateColumnHeaderResized(), OnUpdateDoubleClicked(), OnLanesChanged(), OnLaneSelected(), OnSmallLaneSelected()
		private void OnUpdateSelectionChanged(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in update lane/update zone row selections
			try {
				//Sync lane\small lane combobox values to selected row lane values;
				//IF multiple rows selected, show null selection in comboboxes
				UltraGrid grid = (UltraGrid)sender;
				grid.Focus();
				if(grid.Selected.Rows.Count == 1) {
					string lane = grid.Selected.Rows[0].Cells["NewLane"].Value.ToString();
					if(this.cboLane.Items.Count > 0) this.cboLane.SelectedValue = lane;
					string smallLane = grid.Selected.Rows[0].Cells["NewSmallSortLane"].Value.ToString();
					if(this.cboSmallLane.Items.Count > 0) this.cboSmallLane.SelectedValue = smallLane;
				}
				else 
					this.cboLane.SelectedItem = this.cboSmallLane.SelectedItem = null;
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		private void OnUpdateColumnHeaderResized(object sender, Infragistics.Win.UltraWinGrid.AfterColPosChangedEventArgs e) {
			//Event handler for change in column locations
			int loc=0,left1=0,left2=0;
			int vpos1=0, vpos2=0;
			try {
				//Align new sort/small sort lane comboboxes with corresponding columns
				UltraGrid grid = (UltraGrid)sender;
				for(int k=0; k<grid.DisplayLayout.Bands[0].Columns.Count; k++) {
					if(grid.DisplayLayout.Bands[0].Columns[k].Key == "NewLane") {
						vpos1 = grid.DisplayLayout.Bands[0].Columns[k].Header.VisiblePosition;
						left1 = loc;
					}
					if(grid.DisplayLayout.Bands[0].Columns[k].Key == "NewSmallSortLane") {
						vpos2 = grid.DisplayLayout.Bands[0].Columns[k].Header.VisiblePosition;
						left2 = loc;
					}
					if(grid.DisplayLayout.Bands[0].Columns[k].IsVisibleInLayout)
						loc += grid.DisplayLayout.Bands[0].Columns[k].Width;

					//Get the rows after NewLane, NewSmallSortLane
					if(grid.DisplayLayout.Bands[0].Columns[k].Header.VisiblePosition < vpos1)
						left1 += grid.DisplayLayout.Bands[0].Columns[k].Width;
					if(grid.DisplayLayout.Bands[0].Columns[k].Header.VisiblePosition < vpos2)
						left2 += grid.DisplayLayout.Bands[0].Columns[k].Width;
				}
				this.cboLane.Top = this.cboSmallLane.Top = 1;
				this.cboLane.Left = left1;
				this.cboSmallLane.Left = left2;
				this.cboLane.Width = grid.DisplayLayout.Bands[0].Columns["NewLane"].Width;
				this.cboSmallLane.Width = grid.DisplayLayout.Bands[0].Columns["NewSmallSortLane"].Width;
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnUpdateDoubleClicked(object sender, System.EventArgs e) {
			//Event handler for update lane/zone double clicked
			try {
				//Move selected records between zone and lane/zone update grid
				UltraGrid grid = (UltraGrid)sender;
				if(grid.Selected.Rows.Count > 0) {
					//Clear all selected rows except fo first selected row; then remove
					UltraGridRow row = grid.Selected.Rows[0];
					grid.Selected.Rows.Clear();
					row.Selected = true;
					removeZonesFromUpdate(); 
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		private void OnLaneSelected(object sender, System.EventArgs e) {
			//Event handler for change in lane
			try {
				//Update new lane on selected zones in update grid
				object oVal = (this.cboLane.Text.Length == 0) ? null : this.cboLane.SelectedValue;
				switch(this.tabMain.SelectedTab.Name) {
					case "tabLanes":	
						for(int i=0; i<this.grdLaneUpdates.Selected.Rows.Count; i++) 
							this.grdLaneUpdates.Selected.Rows[i].Cells["NewLane"].Value = oVal;
						break;
					case "tabZones":	
						for(int i=0; i<this.grdZoneUpdates.Selected.Rows.Count; i++) 
							this.grdZoneUpdates.Selected.Rows[i].Cells["NewLane"].Value = oVal;
						break;
					case "tabSchedule":	
						break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Warning); }
		}
		private void OnSmallLaneSelected(object sender, System.EventArgs e) {
			//Event handler for change in small sort lane
			try {
				//Update new small lane on selected zones in update grid
				object oVal = (this.cboSmallLane.Text.Length == 0) ? null : this.cboSmallLane.SelectedValue;
				switch(this.tabMain.SelectedTab.Name) {
					case "tabLanes":	
						for(int i=0; i<this.grdLaneUpdates.Selected.Rows.Count; i++) 
							this.grdLaneUpdates.Selected.Rows[i].Cells["NewSmallSortLane"].Value = oVal;
						break;
					case "tabZones":	
						for(int i=0; i<this.grdZoneUpdates.Selected.Rows.Count; i++) 
							this.grdZoneUpdates.Selected.Rows[i].Cells["NewSmallSortLane"].Value = oVal;
						break;
					case "tabSchedule":	
						break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Warning); }
		}
		#endregion
        #region Ship Schedule: OnGridInitializeLayout(), OnGridInitializeRow(), OnShipScheduleChanged(), OnTripSelectionChanged(), OnCalendarOpened(), OnCalendarClosed(), OnScheduleDateChanged()
        private void OnGridInitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
			//Event handler for grid layout initialization
			try {
				e.Layout.Bands[0].Columns.Insert(e.Layout.Bands[0].Columns.Count,"All Assigned");
				e.Layout.Bands[0].Columns["All Assigned"].DataType = typeof(string);
				e.Layout.Bands[0].Columns["All Assigned"].Width = 48;
				e.Layout.Bands[0].Columns["All Assigned"].Header.Appearance.TextHAlign = HAlign.Center;
				e.Layout.Bands[0].Columns["All Assigned"].CellAppearance.TextHAlign = HAlign.Center;
				e.Layout.Bands[0].Columns["All Assigned"].SortIndicator = SortIndicator.None;
				e.Layout.Bands[0].Columns.Insert(e.Layout.Bands[0].Columns.Count, "Complete");
				e.Layout.Bands[0].Columns["Complete"].DataType = typeof(string);
				e.Layout.Bands[0].Columns["Complete"].Width = 48;
				e.Layout.Bands[0].Columns["Complete"].Header.Appearance.TextHAlign = HAlign.Center;
				e.Layout.Bands[0].Columns["Complete"].CellAppearance.TextHAlign = HAlign.Center;
				e.Layout.Bands[0].Columns["Complete"].SortIndicator = SortIndicator.None;
			} 
			catch(ArgumentException) { }
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Debug); }
		}
		private void OnGridInitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e) {
			//Event handler for grid row initialization
			try {
				//			
				e.Row.Cells["Complete"].Value = (e.Row.Cells["TrailerComplete"].Value != DBNull.Value ? "Y" : "N");
				e.Row.Cells["All Assigned"].Value = (e.Row.Cells["FreightAssigned"].Value != DBNull.Value ? "Y" : "N");
			} 
			catch(ArgumentException) { }
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Debug); }
		}
		private void OnShipScheduleChanged(object sender, EventArgs e) {
			//Event handler for change in ship schedule
			try {
				//Configure and update zone view
				this.mMessageMgr.AddMessage("Loading ship schedule for " + this.dtpScheduleDate.Value.ToShortDateString() + "...");
				this.grdShipSchedule.Text = "Ship Schedule (" + AgentLineHaulFactory.ScheduleID + ")";
				OnTripSelectionChanged(null, null);
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		private void OnTripSelectionChanged(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in trip selections
			this.Cursor = Cursors.WaitCursor;
			try {
				//Clear ref to prior trip object and reset if applicable
				this.mTrip = null;
				if(this.grdShipSchedule.Selected.Rows.Count > 0) {
					//Get a trip object for the selected trip record (could be a child TL row)
					string id="";
					if(this.grdShipSchedule.Selected.Rows[0].HasParent())
						id = this.grdShipSchedule.Selected.Rows[0].ParentRow.Cells["TripID"].Value.ToString();
					else
						id = this.grdShipSchedule.Selected.Rows[0].Cells["TripID"].Value.ToString();
					this.mTrip = AgentLineHaulFactory.GetTrip(id);
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnTripDoubleClicked(object sender, System.EventArgs e) {
			//Event handler for trip double clicked
			this.Cursor = Cursors.WaitCursor;
			try {
				//Move selected records between zone grid and ship schedule grid
				if(this.ctxTripUnassign.Enabled) unassignTL();
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnCalendarOpened(object sender, System.EventArgs e) { this.mCalendarOpen = true; }
		private void OnCalendarClosed(object sender, System.EventArgs e) {
			//Event handler for date picker calendar closed
			this.Cursor = Cursors.WaitCursor;
			try {
				//Allow calendar to close
				this.dtpScheduleDate.Refresh();
				Application.DoEvents();
				
				//Flag calendar as closed; refresh ship schedule
				this.mCalendarOpen = false;
				OnScheduleDateChanged(null,null);
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnScheduleDateChanged(object sender, System.EventArgs e) {
			//Event handler for ship schedule date changed
			this.Cursor = Cursors.WaitCursor;
			try {
				//Refresh ship schedule if the calendar is closed
				if(!this.mCalendarOpen) 
					AgentLineHaulFactory.ScheduleDate = this.dtpScheduleDate.Value;
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { this.Cursor = Cursors.Default; }
		}
		#endregion
		#region Common Grid Services: OnEnter(), OnLeave(), OnGridMouseDown()
		private void OnEnter(object sender, System.EventArgs e) { setUserServices(); }
		private void OnLeave(object sender, System.EventArgs e) { setUserServices(); }
		private void OnGridMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Event handler for mouse down event for all grids
			try {
				//Ensure focus when user mouses (embedded child objects sometimes hold focus)
				UltraGrid grid = (UltraGrid)sender;
				grid.Focus();
				
				//Determine grid element pointed to by the mouse
				UIElement uiElement = grid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));
				if(uiElement != null) {
					//Determine if user selected a grid row
					object context = uiElement.GetContext(typeof(UltraGridRow));
					if(context != null) {
						//Row was selected- if mouse button is:
						// left: forward to mouse move event handler
						//right: clear all (multi-)selected rows and select a single row
						if(e.Button == MouseButtons.Left) 
							OnDragDropMouseDown(sender, e);
						else if(e.Button == MouseButtons.Right) {
							UltraGridRow row = (UltraGridRow)context;
							if(!row.Selected) grid.Selected.Rows.Clear();
							row.Selected = true;
						}
					}
					else {
						//Deselect rows in the white space of the grid or deactivate the active   
						//row when in a scroll region to prevent double-click action
						if(uiElement.Parent != null && uiElement.Parent.GetType() == typeof(DataAreaUIElement))
							grid.Selected.Rows.Clear();
						else if(uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement))
							if(grid.Selected.Rows.Count > 0) grid.Selected.Rows[0].Activated = false;
					}
				}
			} 
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		#endregion
		#region Grid Drag/Drop Events: OnDragDropMouseDown(), OnDragDropMouseMove(), OnDragDropMouseUp(), OnDragEnter(), OnDragOver(), OnDragDrop(), OnDragLeave()
		private void OnDragDropMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) { this.mIsDragging = (e.Button==MouseButtons.Left); }
		private void OnDragDropMouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Start drag\drop if user is dragging
			DataObject data=null;
			try {
				if(e.Button == MouseButtons.Left) {
					if(this.mIsDragging) {
						//Initiate drag drop operation from the grid source
						UltraGrid grid = (UltraGrid)sender;
						if(grid.Focused && grid.Selected.Rows.Count > 0) {
							data = new DataObject();
							data.SetData("");
							DragDropEffects effect = grid.DoDragDrop(data, DragDropEffects.All);
							this.mIsDragging = false; 
								
							//After the drop- handled by drop code
							switch(effect) {
								case DragDropEffects.Move:	break;
								case DragDropEffects.Copy:	break;
							}
						}
					}
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnDragDropMouseUp(object sender, System.Windows.Forms.MouseEventArgs e) { this.mIsDragging = false; }
		private void OnDragEnter(object sender, System.Windows.Forms.DragEventArgs e) { }
		private void OnDragOver(object sender, System.Windows.Forms.DragEventArgs e) {
			//Event handler for dragging over the grid
			try {
				//Enable appropriate drag drop effect
				//NOTE: Cannot COPY or MOVE to self
				UltraGrid grid = (UltraGrid)sender;
				DataObject data = (DataObject)e.Data;
				if(!grid.Focused && data.GetDataPresent(DataFormats.StringFormat, true)) {
					bool allowed=false;
					switch(grid.Name) {
						case "grdZones":			
							allowed = (this.ctxTripRem.Enabled || this.ctxTripUnassign.Enabled); 
							break;
						case "grdTLs": 
							allowed = false; 
							break;
						case "grdLaneUpdates":
						case "grdZoneUpdates":	
							allowed = true; 
							break;
						case "grdShipSchedule": 
							Point pt = this.grdShipSchedule.PointToClient(new Point(e.X, e.Y));
							UIElement uiElement = grid.DisplayLayout.UIElement.ElementFromPoint(pt);
							if(uiElement != null) {
								object context = uiElement.GetContext(typeof(UltraGridRow));
								if(context != null) 					
									((UltraGridRow)context).Selected = true;
								else 
									grid.Selected.Rows.Clear();
							}
							allowed = this.ctxZoneAssign.Enabled; 
							break;
					}
					switch(e.KeyState) {
						case KEYSTATE_SHIFT:	e.Effect = (!grid.Focused && allowed) ? DragDropEffects.Move : DragDropEffects.None; break;
						case KEYSTATE_CTL:		e.Effect = (!grid.Focused && allowed) ? DragDropEffects.Copy : DragDropEffects.None; break;
						default:				e.Effect = (!grid.Focused && allowed) ? DragDropEffects.Copy : DragDropEffects.None; break;
					}
				}
				else
					e.Effect = DragDropEffects.None;
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnDragDrop(object sender, System.Windows.Forms.DragEventArgs e) {
			//Event handler for dropping onto the grid
			try {
				DataObject data = (DataObject)e.Data;
				if(data.GetDataPresent(DataFormats.StringFormat, true)) {
					UltraGrid grid = (UltraGrid)sender;
					switch(grid.Name) {
						case "grdZones":			
							switch(this.tabMain.SelectedTab.Name) {
								case "tabZones":
								case "tabLanes":	removeZonesFromUpdate(); break;
								case "tabSchedule":	unassignTL(); break;
							}
							break;
						case "grdTLs":			break;
						case "grdLaneUpdates":
						case "grdZoneUpdates":	addZonesToUpdate(); break;
						case "grdShipSchedule":	assignTL(); break;
					}
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { setUserServices(); }
		}
		private void OnDragLeave(object sender, System.EventArgs e) { }
		private void OnQueryContinueDrag(object sender, System.Windows.Forms.QueryContinueDragEventArgs e) { }
		private void OnSelectionDrag(object sender, System.ComponentModel.CancelEventArgs e) { }
		#endregion
        #region User Services: OnItemClick(), OnHelpMenuClick(), OnDataStatusUpdate()
        private void OnItemClick(object sender,System.EventArgs e) {
            //Event handler for menu selection
            try {
                ToolStripItem item = (ToolStripItem)sender;
                switch(item.Name) {
                    case "mnuFileNew": 
                    case "btnNew": 
                        break;
                    case "mnuFileOpen": 
                    case "btnOpen_": 
                        break;
                    case "mnuFileSave": 
                    case "btnSave": 
                        break;
                    case "mnuFileSaveAs": 
                        break;
                    case "mnuFileSetup":    UltraGridPrinter.PageSettings(); break;
                    case "mnuFilePrint":    UltraGridPrinter.Print(this.grdShipSchedule,AgentLineHaulFactory.ScheduleDate.DayOfWeek.ToString().ToUpper() + Environment.NewLine + "SHIP SCHEDULE FOR " +  AgentLineHaulFactory.ScheduleDate.ToString("dd-MMM-yyyy"),true); break;
                    case "btnPrint":        UltraGridPrinter.Print(this.grdShipSchedule,AgentLineHaulFactory.ScheduleDate.DayOfWeek.ToString().ToUpper() + Environment.NewLine + "SHIP SCHEDULE FOR " +  AgentLineHaulFactory.ScheduleDate.ToString("dd-MMM-yyyy"),false); break;
                    case "mnuFilePreview":  UltraGridPrinter.PrintPreview(this.grdShipSchedule,AgentLineHaulFactory.ScheduleDate.DayOfWeek.ToString().ToUpper() + Environment.NewLine + "SHIP SCHEDULE FOR " +  AgentLineHaulFactory.ScheduleDate.ToString("dd-MMM-yyyy")); break;
                    case "mnuFileExit":     this.Close(); Application.Exit(); break;
                    case "mnuEditCut": case "btnCut": break;
                    case "mnuEditCopy": case "btnCopy": break;
                    case "mnuEditPaste": case "btnPaste": break;
                    case "mnuEditFind": case "btnFind": 
                        this.grdZones.Selected.Rows.Clear();
                        this.mGridSvcZones.FindRow(0,this.grdZones.Tag.ToString(),this.txtSearchSort.Text);
                        this.grdZones.Focus();
                        break;
                    case "mnuEditFindTL":
                        dlgInputBox dlg = new dlgInputBox("Enter a TL# to search for.","","TL Search");
                        dlg.ShowDialog(this);
                        if(dlg.Value.Length > 0) {
                            DataSet ds = AgentLineHaulFactory.FindTL(dlg.Value);
                            if(ds.Tables[AgentLineHaulFactory.TBL_TL_SEARCH].Rows.Count > 0) {
                                //TL exists; check if assigned
                                if(ds.Tables[AgentLineHaulFactory.TBL_TL_SEARCH].Rows[0].IsNull("ScheduleDate"))
                                    MessageBox.Show(this,"TL# " + dlg.Value + " is not assigned to a ship schedule.","TL Search",MessageBoxButtons.OK,MessageBoxIcon.Information);
                                else {
                                    //Navigate to the schedule
                                    this.dtpScheduleDate.Value = DateTime.Parse(ds.Tables[AgentLineHaulFactory.TBL_TL_SEARCH].Rows[0]["ScheduleDate"].ToString());
                                }
                            }
                            else
                                MessageBox.Show(this,"TL# " + dlg.Value + " does not exist.","TL Search",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }
                        break;
                    case "mnuViewRefresh": case "btnRefresh": 
                        //Refresh zones and ship schedule
                        this.Cursor = Cursors.WaitCursor;
                        this.mMessageMgr.AddMessage("Refreshing zone view...");
                        this.mGridSvcZones.CaptureState();
                        FreightFactory.RefreshZones(this.tabMain.SelectedTab.Name == "tabSchedule");
                        if(this.tabMain.SelectedTab.Name == "tabSchedule") {
                            this.mMessageMgr.AddMessage("Refreshing TL view...");
                            this.mGridSvcTLs.CaptureState();
                            FreightFactory.RefreshTLs();
                            this.mGridSvcTLs.RestoreState();

                            this.mMessageMgr.AddMessage("Refreshing ship schedule...");
                            this.mGridSvcShipSchedule.CaptureState();
                            AgentLineHaulFactory.RefreshTrips();
                            this.mGridSvcShipSchedule.RestoreState();
                        }
                        this.mGridSvcZones.RestoreState();
                        break;
                    case "mnuViewZoneTypeTsort": case "btnZoneTypeTsort":
                        this.mnuViewZoneTypeTsort.Checked = this.btnZoneTypeTsort.Checked = true;
                        this.mnuViewZoneTypeReturns.Checked = this.btnZoneTypeReturns.Checked = this.mnuViewZoneTypeAll.Checked = this.btnZoneTypeAll.Checked = false;
                        this.grdZones.Text = "TLs: Open (Tsort)";
                        this.grdZones.DisplayLayout.Bands[0].ColumnFilters["TypeID"].FilterConditions.Clear();
                        this.grdZones.DisplayLayout.Bands[0].ColumnFilters["TypeID"].LogicalOperator = FilterLogicalOperator.Or;
                        this.grdZones.DisplayLayout.Bands[0].ColumnFilters["TypeID"].FilterConditions.Add(FilterComparisionOperator.Equals,"T");
                        this.grdZones.DisplayLayout.Bands[0].ColumnFilters["TypeID"].FilterConditions.Add(FilterComparisionOperator.Equals,"U");
                        this.grdZones.DisplayLayout.Bands[0].ColumnFilters["TypeID"].FilterConditions.Add(FilterComparisionOperator.Equals,"L");
                        this.grdZones.DisplayLayout.RefreshFilters();
                        this.grdZones.Selected.Rows.Clear();
                        break;
                    case "mnuViewZoneTypeReturns": case "btnZoneTypeReturns":
                        this.mnuViewZoneTypeReturns.Checked = this.btnZoneTypeReturns.Checked = true;
                        this.mnuViewZoneTypeTsort.Checked = this.btnZoneTypeTsort.Checked = this.mnuViewZoneTypeAll.Checked = this.btnZoneTypeAll.Checked = false;
                        this.grdZones.Text = "TLs: Open (Returns)";
                        this.grdZones.DisplayLayout.Bands[0].ColumnFilters["TypeID"].FilterConditions.Clear();
                        this.grdZones.DisplayLayout.Bands[0].ColumnFilters["TypeID"].LogicalOperator = FilterLogicalOperator.Or;
                        this.grdZones.DisplayLayout.Bands[0].ColumnFilters["TypeID"].FilterConditions.Add(FilterComparisionOperator.Equals,"V");
                        this.grdZones.DisplayLayout.Bands[0].ColumnFilters["TypeID"].FilterConditions.Add(FilterComparisionOperator.Equals,"X");
                        this.grdZones.DisplayLayout.RefreshFilters();
                        this.grdZones.Selected.Rows.Clear();
                        break;
                    case "mnuViewZoneTypeAll": case "btnZoneTypeAll": 
                        this.mnuViewZoneTypeAll.Checked = this.btnZoneTypeAll.Checked = true;
                        this.mnuViewZoneTypeTsort.Checked = this.btnZoneTypeTsort.Checked = this.mnuViewZoneTypeReturns.Checked = this.btnZoneTypeReturns.Checked = false;
                        this.grdZones.Text = "TLs: Open (All)";
                        this.grdZones.DisplayLayout.Bands[0].ColumnFilters["TypeID"].FilterConditions.Clear();
                        this.grdZones.DisplayLayout.RefreshFilters();
                        this.grdZones.Selected.Rows.Clear();
                        break;
                    case "mnuViewToolbar":      this.tsMain.Visible = (this.mnuViewToolbar.Checked = (!this.mnuViewToolbar.Checked)); break;
                    case "mnuViewStatusBar":    this.stbMain.Visible = (this.mnuViewStatusBar.Checked = (!this.mnuViewStatusBar.Checked)); break;
                    case "mnuOpAdd": case "btnAdd": case "ctxZoneAdd": 
                        switch(this.tabMain.SelectedTab.Name) {
                            case "tabZones": case "tabLanes": addZonesToUpdate(); break;
                            case "tabSchedule": assignTL(); break;
                        }
                        break;
                    case "mnuOpRem": case "btnRem": case "ctxTripRem": 
                        switch(this.tabMain.SelectedTab.Name) {
                            case "tabZones": case "tabLanes": removeZonesFromUpdate(); break;
                            case "tabSchedule": unassignTL(); break;
                        }
                        break;
                    case "mnuOpChangeLanes": case "btnChangeLanes": 
                        //Change lanes for all zones in update dataset
                        if(MessageBox.Show(this,"Update lane assignments for each zone?",App.Product,MessageBoxButtons.OKCancel,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            this.mMessageMgr.AddMessage("Changing lane assignments...");
                            for(int i=0;i<this.mUpdateDS.ZoneTable.Count;i++) {
                                try {
                                    Zone zone = new Zone(this.mUpdateDS.ZoneTable[i]);
                                    FreightFactory.ChangeLanes(zone);
                                }
                                catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
                            }
                            FreightFactory.RefreshZones(false);
                        }
                        break;
                    case "mnuOpCloseZones": case "btnCloseZones": 
                        //Change lanes and close zones for all zones in update dataset
                        if(MessageBox.Show(this,"Change lane assignments and close zones for each selected zone?",App.Product,MessageBoxButtons.OKCancel,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            this.mMessageMgr.AddMessage("Changing lane assignments and closing zones...");
                            for(int i=0;i<this.mUpdateDS.ZoneTable.Count;i++) {
                                try {
                                    Zone zone = new Zone(this.mUpdateDS.ZoneTable[i]);
                                    FreightFactory.Close(zone);
                                }
                                catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
                            }
                            FreightFactory.RefreshZones(false);
                        }
                        break;
                    case "mnuOpOpen": case "btnOpen": case "ctxTripOpen": 
                        openTrip(); 
                        break;
                    case "mnuOpCloseAllTLs": case "ctxTripCloseAllTLs": 
                        closeAllTLs(); 
                        break;
                    case "mnuOpClose": case "btnClose": case "ctxTripClose": 
                        closeTrip(); 
                        break;
                    case "mnuOpAssign": case "btnAssign": case "ctxZoneAssign": 
                        assignTL(); 
                        break;
                    case "mnuOpUnassign": case "btnUnassign": case "ctxTripUnassign": 
                        unassignTL(); 
                        break;
                    case "mnuOpMove": case "btnMove": case "ctxTripMove": 
                        moveTL(); 
                        break;
                    case "mnuReportsZonesByLane": break;
                    case "mnuToolsConfig":      App.ShowConfig(); break;
                    case "mnuHelpAbout":        new dlgAbout(App.Product + " Application",App.Version,App.Copyright,App.Configuration).ShowDialog(this); break;
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnHelpMenuClick(object sender,System.EventArgs e) {
            //Event hanlder for configurable help menu items
            try {
                ToolStripItem menu = (ToolStripItem)sender;
                Help.ShowHelp(this,this.mHelpItems.GetValues(menu.Text)[0]);
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnDataStatusUpdate(object sender,DataStatusArgs e) {
            //Event handler for notifications from mediator
            this.stbMain.OnOnlineStatusUpdate(null,new OnlineStatusArgs(e.Online,e.Connection));
        }
        #endregion
        #region Local Services: addZonesToUpdate(), removeZonesFromUpdate(), openTrip(), closeAllTLs(), closeTrip(), assignTL(), unassignTL(), moveTL()
        private void addZonesToUpdate() {
            //Add the selected TLs to the update grid
            this.Cursor = Cursors.WaitCursor;
            try {
                this.mMessageMgr.AddMessage("Adding selected TLs...");
                for(int i=0;i<this.grdZones.Selected.Rows.Count;i++) {
                    //Add each selected row to the update grid- validate not already added
                    UltraGridRow row = this.grdZones.Selected.Rows[i];
                    string zoneCode = row.Cells["Zone"].Value.ToString();
                    string clientNumber = row.Cells["ClientNumber"].Value.ToString();
                    if (this.mUpdateDS.ZoneTable.Select("Zone='" + zoneCode + "' AND ClientNumber='" + clientNumber + "'").Length == 0) {
                        #region Add the selected TL to the update grid
                        string TL = row.Cells["TL#"].Value.ToString();
                        string lane = row.Cells["Lane"].Value.ToString();
                        string smallLane = row.Cells["SmallSortLane"].Value.ToString();
                        string description = row.Cells["Description"].Value.ToString();
                        string type = row.Cells["Type"].Value.ToString();
                        string typeID = row.Cells["TypeID"].Value.ToString();
                        string status = row.Cells["Status"].Value.ToString();
                        string rollbackTL = row.Cells["RollbackTL#"].Value.ToString();
                        int isExclusive = Convert.ToInt32(row.Cells["IsExclusive"].Value);
                        string canClose = row.Cells["CAN_BE_CLOSED"].Value.ToString();
                        string state = row.Cells["AssignedToShipScde"].Value.ToString();
                        long agent = (row.Cells["AgentTerminalID"].Value != DBNull.Value) ? Convert.ToInt64(row.Cells["AgentTerminalID"].Value) : 0;
                        DateTime tlDate = (row.Cells["TLDate"].Value != DBNull.Value) ? DateTime.Parse(row.Cells["TLDate"].Value.ToString()) : DateTime.Today;
                        string closeNumber = row.Cells["CloseNumber"].Value.ToString();
                        string clientName = row.Cells["ClientName"].Value.ToString();
                        #endregion
                        this.mUpdateDS.ZoneTable.AddZoneTableRow(zoneCode,TL,tlDate,closeNumber,lane,lane,smallLane,smallLane,description,type,typeID,status,rollbackTL,isExclusive,canClose,state,agent,clientNumber,clientName);
                    }
                }
                this.grdZones.DeleteSelectedRows(false);
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void removeZonesFromUpdate() {
            //Remove the selected TLs from the update grid
            this.Cursor = Cursors.WaitCursor;
            try {
                UltraGrid grid=null;
                switch(this.tabMain.SelectedTab.Name) {
                    case "tabLanes": grid = this.grdLaneUpdates; break;
                    case "tabZones": grid = this.grdZoneUpdates; break;
                }
                this.mMessageMgr.AddMessage("Removing selected TLs...");
                for(int i=0;i<grid.Selected.Rows.Count;i++) {
                    //Remove all selected row from the update grid- validate not already removed
                    UltraGridRow row = grid.Selected.Rows[i];
                    string zoneCode = row.Cells["Zone"].Value.ToString();
                    string clientNumber = row.Cells["ClientNumber"].Value.ToString();
                    if (FreightFactory.Zones.ZoneTable.Select("Zone='" + zoneCode + "' AND ClientNumber='" + clientNumber + "'").Length == 0) {
                        #region Remove the selected TL from the update grid
                        string TL = row.Cells["TL#"].Value.ToString();
                        string lane = row.Cells["Lane"].Value.ToString();
                        string smallLane = row.Cells["SmallSortLane"].Value.ToString();
                        string description = row.Cells["Description"].Value.ToString();
                        string type = row.Cells["Type"].Value.ToString();
                        string typeID = row.Cells["TypeID"].Value.ToString();
                        string status = row.Cells["Status"].Value.ToString();
                        string rollbackTL = row.Cells["RollbackTL#"].Value.ToString();
                        int isExclusive = Convert.ToInt32(row.Cells["IsExclusive"].Value);
                        string canClose = row.Cells["CAN_BE_CLOSED"].Value.ToString();
                        string state = row.Cells["AssignedToShipScde"].Value.ToString();
                        long agent = Convert.ToInt64(row.Cells["AgentTerminalID"].Value);
                        DateTime tlDate = DateTime.Parse(row.Cells["TLDate"].Value.ToString());
                        string closeNumber = row.Cells["CloseNumber"].Value.ToString();
                        string clientName = row.Cells["ClientName"].Value.ToString();
                        #endregion
                        FreightFactory.Zones.ZoneTable.AddZoneTableRow(zoneCode,TL,tlDate,closeNumber,"",lane,"",smallLane,description,type,typeID,status,rollbackTL,isExclusive,canClose,state,agent,clientNumber,clientName);
                    }
                }
                grid.DeleteSelectedRows(false);
                this.grdZones.Selected.Rows.Clear();
                this.grdZones.DisplayLayout.Bands[0].SortedColumns.RefreshSort(true);
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void openTrip() {
			//Open a closed trip (set All Assigned=null)
			this.Cursor = Cursors.WaitCursor;
			try  {
				this.mMessageMgr.AddMessage("Opening the selected trip...");
                this.mGridSvcShipSchedule.CaptureState();
                AgentLineHaulFactory.Open(this.mTrip);
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { this.mGridSvcShipSchedule.RestoreState(); setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void closeAllTLs()  {
			//Close all open TLs for the selected trip
			this.Cursor = Cursors.WaitCursor;
			try  {
				this.mMessageMgr.AddMessage("Selecting all open zones for closing...");
				Hashtable openTLs = new Hashtable();
				for(int i=0; i<this.mTrip.AssignedTLs.ShipScheduleDetailTable.Rows.Count; i++) {
					if(this.mTrip.AssignedTLs.ShipScheduleDetailTable[i].IsCloseDateNull())
						openTLs.Add(this.mTrip.AssignedTLs.ShipScheduleDetailTable[i].Zone, "");
				}
				this.tabMain.SelectedIndex = 1;
				for(int i=0; i<this.grdZones.Rows.VisibleRowCount; i++) {
					string zoneCode = this.grdZones.Rows.GetRowAtVisibleIndex(i).Cells["Zone"].Value.ToString();
					if(openTLs.ContainsKey(zoneCode))
						this.grdZones.Rows.GetRowAtVisibleIndex(i).Selected = true;
				}
				addZonesToUpdate();
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void closeTrip()  {
			//Close an open trip (setAll Assigned=Now)
			DialogResult Q=DialogResult.No;
			try  {
				//Warn on closing when 0 TL's assigned
				Q = DialogResult.Yes;
				if(this.mTrip.AssignedTLs.ShipScheduleDetailTable.Rows.Count < 1)
					Q = MessageBox.Show(this, "There are NO TL's assigned to this trip; would you like to continue anyway?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if(Q == DialogResult.No) return;

				//Validate and warn if there are open (unassigned) TL's that may also need be assigned
				//1. Group assigned TLs by first 6 characters (i.e. MMDDYX)
				SortedList list = new SortedList();
				for(int i=0; i<this.mTrip.AssignedTLs.ShipScheduleDetailTable.Rows.Count; i++) {
					//Create key from TLDate and CloseNumber
					DateTime tlDate = this.mTrip.AssignedTLs.ShipScheduleDetailTable[i].TLDate;
					string closeNumber = this.mTrip.AssignedTLs.ShipScheduleDetailTable[i].CloseNumber;
					string key = tlDate.ToString("MMddyyyy") + closeNumber.Trim();
					if(list.ContainsKey(key))
						list[key] = (int)list[key] + 1;
					else
						list.Add(key, 1);
				}
				//2. Determine the count of the largest TL group(s)
				int max=0;
				for(int i=0; i<list.Count; i++) {
					int val = (int)list.GetByIndex(i);
					if(val > max) max = val;
				}
				Debug.Write("Max=" + max.ToString() + "\n");
				//3. Verify there are no unassigned TLs with the same 6 characters as the max groups
				bool tlsExist=false;
				foreach(DictionaryEntry entry in list) {
					if(tlsExist) break;
					string tl6 = (string)entry.Key;
					if((int)entry.Value == max) {
						Debug.Write("Validating " + tl6 + "...\n");
						for(int j=0; j<this.grdZones.Rows.VisibleRowCount; j++) {
							string mainZone = this.grdZones.Rows.GetRowAtVisibleIndex(j).Cells["Zone"].Value.ToString().Substring(0, 1);
							DateTime _tlDate = DateTime.Parse(this.grdZones.Rows.GetRowAtVisibleIndex(j).Cells["TLDate"].Value.ToString());
							string _closeNumber = this.grdZones.Rows.GetRowAtVisibleIndex(j).Cells["CloseNumber"].Value.ToString().Trim();
							//string code = this.grdZones.Rows.GetRowAtVisibleIndex(j).Cells["TL#"].Value.ToString().Substring(0, 6);
							string code = _tlDate.ToString("MMddyyyy") + _closeNumber.Trim();
							if(mainZone == this.mTrip.MainZone.TrimEnd() && code == tl6) { 
								tlsExist = true; break; 
							}
						}
					}
				}
				//4. Warn if unassigned TLs exist
				Q = DialogResult.Yes;
				if(tlsExist)
					Q = MessageBox.Show(this, "There are other TL's that may require assignment to this trip; would you like to continue anyway?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if(Q == DialogResult.Yes) {
                    //Close the trip
                    this.Cursor = Cursors.WaitCursor;
                    try {
						this.mMessageMgr.AddMessage("Closing the selected trip...");
                        this.mGridSvcShipSchedule.CaptureState();
                        AgentLineHaulFactory.Close(this.mTrip);
					}
					catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
					finally { this.mGridSvcShipSchedule.RestoreState(); }
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void assignTL()  {
			//Assign an open TL to a trip
			this.Cursor = Cursors.WaitCursor;
			//Assign one or more TLs from the Zone or TLs grid
			UltraGrid grid = this.grdTLs.Focused ? this.grdTLs : this.grdZones;
            bool ret=false;
			try {
				//Assign all selected TLs to the selected trip
                ShipScheduleTrip trip = this.mTrip;
				string[] tls = new string[grid.Selected.Rows.Count];
				string[] zones = new string[grid.Selected.Rows.Count];
				for(int i=0; i<grid.Selected.Rows.Count; i++) {
					tls[i] = grid.Selected.Rows[i].Cells["TL#"].Value.ToString();
					zones[i] = grid.Selected.Rows[i].Cells["Zone"].Value.ToString();
				}
				for(int i=0; i<tls.Length; i++) {
					try {
						//Prompt user for verification if there is an earlier open trip
						DialogResult Q1=DialogResult.Yes, Q2=DialogResult.Yes, Q3=DialogResult.Yes;
						ShipScheduleTrip earlierTrip = AgentLineHaulFactory.GetEarlierTripFromAPriorSchedule(trip.TripID, tls[i]);
						if(earlierTrip != null) 
							Q1 = MessageBox.Show(this, "There is an earlier trip on the " + earlierTrip.ScheduleDate.ToShortDateString() + " schedule. Are you sure you want to assign TL " + tls[i] + " to this load?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
						earlierTrip = AgentLineHaulFactory.GetEarlierTripFromThisSchedule(trip.TripID, tls[i]);
						if(Q1 == DialogResult.Yes && earlierTrip != null) 
							Q2 = MessageBox.Show(this, "There is an earlier trip on todays schedule. Are you sure you want to assign TL " + tls[i] + " to this load?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        //if(Q1 == DialogResult.Yes && Q2 == DialogResult.Yes && trip.AssignedTLs.ShipScheduleDetailTable.Select("Zone='" + zones[i] + "'").Length > 0) 
                        //    Q3 = MessageBox.Show(this, "There is already a TL assigned to this load for zone " + zones[i] + ". Are you sure you want to assign multiple TL's to the same zone?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
						if(Q1 == DialogResult.Yes && Q2 == DialogResult.Yes && Q3 == DialogResult.Yes) {
							this.Cursor = Cursors.WaitCursor;
                            this.mMessageMgr.AddMessage("Assigning selected TL's...");
                            this.mGridSvcShipSchedule.CaptureState();
                            if(AgentLineHaulFactory.AssignTL(trip, tls[i])) ret = true;
						}
					}
					catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
					finally { this.mGridSvcShipSchedule.RestoreState(); }
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			
            if(ret) {
				//Refresh zones/TLs on success
                long atid = AgentLineHaulFactory.AgentTerminalID;
                if(grid.Name == "grdZones") {
                    try {
                        this.mMessageMgr.AddMessage("Refreshing zone view...");
                        this.mGridSvcZones.CaptureState();
                        FreightFactory.RefreshZones(true);
                    }
                    catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
                    finally { this.mGridSvcZones.RestoreState(); if(AgentLineHaulFactory.AgentTerminalID != atid) this.grdZones.Selected.Rows.Clear(); }
                }
                else if(grid.Name == "grdTLs") {
                    try {
                        this.mMessageMgr.AddMessage("Refreshing TLs view...");
                        this.mGridSvcTLs.CaptureState();
                        FreightFactory.RefreshTLs();
                    }
                    catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
                    finally { this.mGridSvcTLs.RestoreState(); if(AgentLineHaulFactory.AgentTerminalID != atid) AgentLineHaulFactory.AgentTerminalID = 0; }
                }
			}
            this.Cursor = Cursors.Default;
		}
		private void unassignTL()  {
			//Unassign an open TL from a trip
            this.Cursor = Cursors.WaitCursor;
            bool ret=false;
            try {
                //Unassign the selected TL
                this.mMessageMgr.AddMessage("Unassigning selected freight (TL)...");
                this.mGridSvcShipSchedule.CaptureState();
                ret = AgentLineHaulFactory.UnassignTL(this.mTrip, this.grdShipSchedule.Selected.Rows[0].Cells["FreightID"].Value.ToString().TrimEnd());
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { this.mGridSvcShipSchedule.RestoreState(); }
			
            if(ret) {
				//Refresh zone view on success 
				try {
					this.mMessageMgr.AddMessage("Refreshing zone view...");
                    this.mGridSvcZones.CaptureState();
					FreightFactory.RefreshZones(true);
				} 
				catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
                finally { this.mGridSvcZones.RestoreState(); }
			}
            this.Cursor = Cursors.Default;
		}
		private void moveTL()  {
			//Move a closed TL from one trip to another trip
			//Prompt user for a destination trip
			string mainZoneCode = this.grdShipSchedule.Selected.Rows[0].Cells["MainZone"].Value.ToString().TrimEnd();
			long agentTerminalID = Convert.ToInt64(this.grdShipSchedule.Selected.Rows[0].Cells["AgentTerminalID"].Value);
			dlgTrip dlg = new dlgTrip(mainZoneCode, agentTerminalID, this.dtpScheduleDate.Value);
			if(dlg.ShowDialog(this) == DialogResult.OK) {
				bool ret=false;
				try {
					//Move selected freight to the selected trip
					ShipScheduleTrip trip = dlg.DestinationTrip;
					string tl = this.grdShipSchedule.Selected.Rows[0].Cells["FreightID"].Value.ToString().TrimEnd();
					string zone = this.grdShipSchedule.Selected.Rows[0].Cells["Zone"].Value.ToString().TrimEnd();
					
					//Prompt user for verification if there is an earlier open trip
					DialogResult Q1=DialogResult.Yes, Q2=DialogResult.Yes, Q3=DialogResult.Yes;
					ShipScheduleTrip earlierTrip = AgentLineHaulFactory.GetEarlierTripFromAPriorSchedule(trip.TripID, tl);
					if(earlierTrip != null) 
						Q1 = MessageBox.Show(this, "There is an earlier trip on the " + earlierTrip.ScheduleDate.ToShortDateString() + " schedule. Are you sure you want to assign TL " + tl + " to this load?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
					earlierTrip = AgentLineHaulFactory.GetEarlierTripFromThisSchedule(trip.TripID, tl);
					if(Q1 == DialogResult.Yes && earlierTrip != null) 
						Q2 = MessageBox.Show(this, "There is an earlier trip on todays schedule. Are you sure you want to assign TL " + tl + " to this load?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    //if(Q2 == DialogResult.Yes && trip.AssignedTLs.ShipScheduleDetailTable.Select("Zone='" + zone + "'").Length > 0) 
                    //    Q3 = MessageBox.Show(this, "There is already a TL " + tl + " assigned to this load for zone " + zone + ". Are you sure you want to assign multiple TL's to the same zone?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
					if(Q1 == DialogResult.Yes && Q2 == DialogResult.Yes && Q3 == DialogResult.Yes) {
						this.Cursor = Cursors.WaitCursor;
						this.mMessageMgr.AddMessage("Moving selected TL...");
                        this.mGridSvcShipSchedule.CaptureState();
                        ret = AgentLineHaulFactory.MoveTL(trip, tl);
						AgentLineHaulFactory.RefreshTrips();
					}
				}
				catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
                finally { this.mGridSvcShipSchedule.RestoreState(); }
				
                if(ret) {
					//Refresh zone view on success 
					try {
						this.mMessageMgr.AddMessage("Refreshing zone view...");
                        this.mGridSvcZones.CaptureState();
                        FreightFactory.RefreshZones(true);
					} 
					catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
                    finally { this.mGridSvcZones.RestoreState(); }
				}
			}
            this.Cursor = Cursors.Default;
        }
		#endregion
        #region Local Services: configApplication(), setUserServices(), buildHelpMenu()
        private void configApplication() {
            //
            try {
                //Create event log database trace listeners, and log application as started
                try {
                    ArgixTrace.AddListener(new DBTraceListener((LogLevel)App.Config.TraceLevel,App.Mediator,App.USP_TRACE,App.EventLogName));
                }
                catch {
                    ArgixTrace.AddListener(new DBTraceListener(LogLevel.Debug,App.Mediator,App.USP_TRACE,App.EventLogName));
                }
                ArgixTrace.WriteLine(new TraceMessage(App.Version,App.Product,LogLevel.Information,"App Started"));

                //Create business objects with configuration values
                App.Mediator.DataStatusUpdate += new DataStatusHandler(OnDataStatusUpdate);
                FreightFactory.ZonesChanged += new EventHandler(OnZonesChanged);
                FreightFactory.TLsChanged += new EventHandler(OnTLsChanged);
                AgentLineHaulFactory.Changed += new EventHandler(OnShipScheduleChanged);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Configuration Failure",ex); } 
		}
		private void setUserServices() {
			//Set user services depending upon an item selected in the grid
			bool canPrint=false;
			bool canFindTL=false;
			bool canAdd=false, canRemove=false;
			bool canChangeLanes=false, canCloseZones=false;
			bool canOpen=false, canClose=false, canCloseAllTLs=false;
			bool canAssign=false, canUnassign=false, canMove=false;
			try {
				//Determine context and applicable services
				switch(this.tabMain.SelectedTab.Name) {
					case "tabLanes":
						canAdd = (this.grdZones.Focused && this.grdZones.Selected.Rows.Count > 0);
						canRemove = (this.grdLaneUpdates.Focused && this.grdLaneUpdates.Selected.Rows.Count > 0);
                        canChangeLanes = (App.Config.ShowLaneChanges && !App.Config.ReadOnly && this.mUpdateDS.ZoneTable.Rows.Count > 0);
						canCloseZones = false;
						break;
					case "tabZones":
						canAdd = (this.grdZones.Focused && this.grdZones.Selected.Rows.Count > 0);
						canRemove = (this.grdZoneUpdates.Focused && this.grdZoneUpdates.Selected.Rows.Count > 0);
						canChangeLanes = false;
						canCloseZones = (!App.Config.ReadOnly && this.mUpdateDS.ZoneTable.Rows.Count > 0);
						break;
					case "tabSchedule":
						canFindTL = true;
						canPrint = (AgentLineHaulFactory.Trips.ShipScheduleMasterTable.Rows.Count > 0);
                        if(App.Config.ShowShipSchedule && !App.Config.ReadOnly && this.grdShipSchedule.Selected.Rows.Count > 0 && this.mTrip != null) {
							bool isTripRow = !this.grdShipSchedule.Selected.Rows[0].HasParent();
							bool isTLRow = this.grdShipSchedule.Selected.Rows[0].HasParent();
							bool isTLClosed = isTLRow ? (this.grdShipSchedule.Selected.Rows[0].Cells["CloseDate"].Value.ToString().Length > 0) : false;
							canOpen =  isTripRow && !this.mTrip.IsOpen && !this.mTrip.IsComplete;
							canCloseAllTLs = isTripRow && this.mTrip.IsOpen && !this.mTrip.IsComplete && !this.mTrip.AllTLsClosed;
							canClose = isTripRow && this.mTrip.IsOpen && !this.mTrip.IsComplete && (this.mTrip.AssignedTLs.ShipScheduleDetailTable.Rows.Count>0) && this.mTrip.AllTLsClosed;
							canAssign = (this.grdZones.Focused || this.grdTLs.Focused) && isTripRow && this.mTrip.IsOpen;
							canUnassign = this.grdShipSchedule.Focused && isTLRow && this.mTrip.IsOpen && !isTLClosed;
							canMove = this.grdShipSchedule.Focused && isTLRow && this.mTrip.IsOpen && isTLClosed;
						}
						break;
				}
				
				//Set menu and toolbar states
				this.mnuFileNew.Enabled = this.btnNew.Enabled = false;
				this.mnuFileOpen.Enabled = this.btnOpen_.Enabled = false;
                this.mnuFileSave.Enabled = this.mnuFileSaveAs.Enabled = false;
				this.mnuFileSetup.Enabled = true;
				this.mnuFilePrint.Enabled = this.btnPrint.Enabled = canPrint;
                this.mnuFilePreview.Enabled = canPrint;
				this.mnuFileExit.Enabled = true;
				this.mnuEditCut.Enabled = this.mnuEditCopy.Enabled = this.mnuEditPaste.Enabled = false;
				this.mnuEditFind.Enabled = this.btnSearch.Enabled = (this.txtSearchSort.Text.Length > 0);
				this.mnuEditFindTL.Enabled = canFindTL;
				this.mnuViewRefresh.Enabled = this.btnRefresh.Enabled = true;
				this.mnuViewZoneTypeTsort.Enabled = this.mnuViewZoneTypeReturns.Enabled = this.mnuViewZoneTypeAll.Enabled = true;
                this.btnZoneTypeTsort.Enabled = this.btnZoneTypeReturns.Enabled = this.btnZoneTypeAll.Enabled = true;
				this.mnuViewToolbar.Enabled = this.mnuViewStatusBar.Enabled = true;
				this.mnuOpAdd.Enabled = this.ctxZoneAdd.Enabled = this.btnAdd.Enabled = canAdd;
				this.mnuOpRem.Enabled = this.ctxTripRem.Enabled = this.btnRem.Enabled = canRemove;
				this.mnuOpChangeLanes.Enabled = this.btnChangeLanes.Enabled = canChangeLanes;
				this.mnuOpCloseZones.Enabled = this.btnCloseZones.Enabled = canCloseZones;
				this.mnuOpOpen.Enabled = this.ctxTripOpen.Enabled = this.btnOpen.Enabled = canOpen;
				this.mnuOpClose.Enabled = this.ctxTripClose.Enabled = this.btnClose.Enabled = canClose;
				this.mnuOpCloseAllTLs.Enabled = this.ctxTripCloseAllTLs.Enabled = canCloseAllTLs;
				this.mnuOpAssign.Enabled = this.ctxZoneAssign.Enabled = this.btnAssign.Enabled = canAssign;
				this.mnuOpUnassign.Enabled = this.ctxTripUnassign.Enabled = this.btnUnassign.Enabled = canUnassign;
				this.mnuOpMove.Enabled = this.ctxTripMove.Enabled = this.btnMove.Enabled = canMove;
				this.mnuReportsZonesByLane.Enabled = false;
                this.mnuToolsConfig.Enabled = true;
				this.mnuHelpAbout.Enabled = true;
				this.cboLane.Enabled = ((canChangeLanes || canCloseZones) && (this.cboLane.Items.Count > 0));
				this.cboSmallLane.Enabled = ((canChangeLanes || canCloseZones) && (this.cboSmallLane.Items.Count > 0));

                this.stbMain.SetTerminalPanel(App.Mediator.TerminalID.ToString(),App.Mediator.Description);
                this.stbMain.User2Panel.Icon = App.Config.ReadOnly ? new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Resources.readonly.ico")) : null;
                this.stbMain.User2Panel.ToolTipText = App.Config.ReadOnly ? "Read only mode; notify IT if you require update permissions." : "";
            }
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { Application.DoEvents(); }
		}
        private void buildHelpMenu() {
            //Build dynamic help menu from configuration file
            try {
                //Read help menu configuration from app.config
                this.mHelpItems = (NameValueCollection)ConfigurationManager.GetSection("menu/help");
                for(int i = 0;i < this.mHelpItems.Count;i++) {
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
	}
}
