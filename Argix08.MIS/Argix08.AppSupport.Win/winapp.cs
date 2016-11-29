using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Configuration;
using Argix.Windows;

namespace Argix.MIS {
    //
	public class winApp : System.Windows.Forms.Form {
		//Members
        private Deployment mTsortApp=null;
		private DBConfigEntry mSelectedConfigEntry=null;
		private TraceLogEntry mSelectedTraceEntry=null;
		private UltraGridSvc mGridSvc=null;
		private System.Windows.Forms.ToolTip mToolTip=null;
		private bool mIsDirty=false;
		
		public event EventHandler ServiceStatesChanged=null;
		public event ErrorEventHandler ErrorMessage=null;
		
		#region Constants
		private const string NODE_ACTIVE = "Active";
		private const string NODE_ROLLBACK = "Rollback";
		private const string MNU_REFRESH = "&Refresh";
		private const string MNU_SAVE = "&Save";
		private const string MNU_ROLLBACK = "&Rollback";
		private const string MNU_CUT = "Cu&t";
		private const string MNU_COPY = "&Copy";
		private const string MNU_PASTE = "&Paste";
		private const string MNU_DELETE = "&Delete";
		#endregion
		#region Controls

		private Infragistics.Win.UltraWinGrid.UltraGrid grdLog;
		private System.Windows.Forms.ContextMenu ctxLog;
		private Argix.MIS.TraceLogsDS mLog;
		private System.Windows.Forms.TabControl tabDialog;
		private System.Windows.Forms.TabPage tabConfig;
		private System.Windows.Forms.TabPage tabTrace;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdDBConfig;
		private Argix.Configuration.AppConfigDS m_dsConfig;
		private System.Windows.Forms.Panel pnlDialog;
		private System.Windows.Forms.Splitter splitterV;
		private System.Windows.Forms.TreeView trvFileConfig;
		private System.Windows.Forms.Panel pnlFileConfig;
		private System.Windows.Forms.Label lblFileConfig;
		private System.Windows.Forms.RichTextBox txtFileConfig;
		private System.Windows.Forms.Splitter splitterH;
		private System.Windows.Forms.MenuItem ctxLogRefresh;
		private System.Windows.Forms.MenuItem ctxLogSep1;
		private System.Windows.Forms.MenuItem ctxLogCut;
		private System.Windows.Forms.MenuItem ctxLogCopy;
		private System.Windows.Forms.MenuItem ctxLogPaste;
		private System.Windows.Forms.MenuItem ctxLogSep2;
		private System.Windows.Forms.MenuItem ctxLogDelete;
		private System.Windows.Forms.ContextMenu ctxConf;
		private System.Windows.Forms.MenuItem ctxConfRefresh;
		private System.Windows.Forms.MenuItem ctxConfSep1;
		private System.Windows.Forms.MenuItem ctxConfSave;
		private System.Windows.Forms.MenuItem ctxConfRollback;
		private System.Windows.Forms.MenuItem ctxConfSep2;
		private System.Windows.Forms.MenuItem ctxConfCut;
		private System.Windows.Forms.MenuItem ctxConfCopy;
		private System.Windows.Forms.MenuItem ctxConfPaste;
		private System.Windows.Forms.MenuItem ctxConfDelete;
		
		private System.ComponentModel.Container components = null;		//Required designer variable
		#endregion
		
		//Interface
		public winApp(ref Deployment app) {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				#region Menu identities (used for onclick handlers) 
				this.ctxConfRefresh.Text = MNU_REFRESH;
				this.ctxConfSave.Text = MNU_SAVE;
				this.ctxConfRollback.Text = MNU_ROLLBACK;
				this.ctxConfCut.Text = MNU_CUT;
				this.ctxConfCopy.Text = MNU_COPY;
				this.ctxConfPaste.Text = MNU_PASTE;
				this.ctxConfDelete.Text = MNU_DELETE;
				this.ctxLogRefresh.Text = MNU_REFRESH;
				this.ctxLogCut.Text = MNU_CUT;
				this.ctxLogCopy.Text = MNU_COPY;
				this.ctxLogPaste.Text = MNU_PASTE;
				this.ctxLogDelete.Text = MNU_DELETE;
				#endregion
				#region Window docking
				this.splitterH.MinExtra = 72;
				this.splitterH.MinSize = 72;
				this.splitterH.Dock = DockStyle.Bottom;
				this.pnlFileConfig.Dock = DockStyle.Top;
				this.splitterV.MinExtra = 72;
				this.splitterV.MinSize = 72;
				this.splitterV.Dock = DockStyle.Left;
				this.trvFileConfig.Dock = DockStyle.Left;
				this.txtFileConfig.Dock = DockStyle.Fill;
				this.pnlDialog.Controls.AddRange(new Control[]{this.txtFileConfig, this.splitterV, this.trvFileConfig, this.pnlFileConfig});
				this.pnlDialog.Dock = DockStyle.Bottom;
				this.grdDBConfig.Dock = DockStyle.Fill;
				this.tabConfig.Controls.AddRange(new Control[]{this.grdDBConfig, this.splitterH, this.pnlDialog});
				#endregion
				
				//Bind to app and register for app events
				this.mTsortApp = app;
				this.mTsortApp.DBConfiguration.Refreshed += new EventHandler(this.OnDBConfigurationRefreshed);
				this.mTsortApp.FileConfiguration.Refreshed += new EventHandler(this.OnFileConfigurationRefreshed);
				this.mTsortApp.TraceLog.Refreshed += new EventHandler(this.OnLogRefreshed);
				this.mGridSvc = new UltraGridSvc(this.grdLog);
				this.mToolTip = new System.Windows.Forms.ToolTip();
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error creating new winApp instance.",ex); }
        }
		protected override void Dispose( bool disposing ) { if( disposing ) { if(components != null) components.Dispose(); } base.Dispose( disposing ); }
		public bool CanSave { get { return (this.txtFileConfig.Focused && this.mIsDirty); } }
		public void Save() { this.ctxConfSave.PerformClick(); }
		public bool CanSaveAs { get { return ((this.txtFileConfig.Focused && this.mIsDirty) || (this.grdLog.Focused && this.grdLog.Selected.Rows.Count > 0)); } }
		public void SaveAs() { 
			SaveFileDialog dlgSave = new SaveFileDialog();
			dlgSave.AddExtension = true;
			dlgSave.FileName = "";
			dlgSave.OverwritePrompt = true;
			if(this.txtFileConfig.Focused) {
				dlgSave.Filter = "Configuration Files (*.exe.config) | *.exe.config";
				dlgSave.FilterIndex = 0;
				dlgSave.Title = "Save Configuration File As...";
				if(dlgSave.ShowDialog(this) == DialogResult.OK) 
					this.mTsortApp.FileConfiguration.SaveAs(dlgSave.FileName, this.txtFileConfig.Text); 
			}
			else if(this.grdLog.Focused) {
				dlgSave.Filter = "Log File (*.log) | *.log";
				dlgSave.FilterIndex = 0;
				dlgSave.Title = "Save Log File As...";
				if(dlgSave.ShowDialog(this) == DialogResult.OK) 
					this.mTsortApp.TraceLog.LogEntries.WriteXml(dlgSave.FileName); 
			}
		}
		public void PageSettings() { UltraGridSvc.PageSettings(); }
		public bool CanPrint { get { return (this.grdLog.Focused && this.grdLog.Rows.Count > 0); } }
		public void Print() { UltraGridSvc.Print(this.grdLog, true); }
		public bool CanCut { get { return this.ctxConfCut.Enabled; } }
		public void Cut() { this.ctxConfCut.PerformClick(); }
		public bool CanCopy { get { return this.ctxConfCopy.Enabled; } }
		public void Copy() { this.ctxConfCopy.PerformClick(); }
		public bool CanPaste { get { return this.ctxConfPaste.Enabled; } }
		public void Paste() { this.ctxConfPaste.PerformClick(); }
		public bool CanDelete { get { return this.ctxConfDelete.Enabled; } }
		public void Delete() { this.ctxConfDelete.PerformClick(); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ArgixLogTable", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Name");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Level");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Date");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Source");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Category");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Event");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("User");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Computer");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Keyword1");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Keyword2");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Keyword3");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Message");
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ConfigTable", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Application");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PCName");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Key");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Value");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Security");
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(winApp));
			this.ctxLog = new System.Windows.Forms.ContextMenu();
			this.ctxLogRefresh = new System.Windows.Forms.MenuItem();
			this.ctxLogSep1 = new System.Windows.Forms.MenuItem();
			this.ctxLogCut = new System.Windows.Forms.MenuItem();
			this.ctxLogCopy = new System.Windows.Forms.MenuItem();
			this.ctxLogPaste = new System.Windows.Forms.MenuItem();
			this.ctxLogSep2 = new System.Windows.Forms.MenuItem();
			this.ctxLogDelete = new System.Windows.Forms.MenuItem();
			this.grdLog = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mLog = new Argix.MIS.TraceLogsDS();
			this.tabDialog = new System.Windows.Forms.TabControl();
			this.tabConfig = new System.Windows.Forms.TabPage();
			this.splitterH = new System.Windows.Forms.Splitter();
			this.grdDBConfig = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.ctxConf = new System.Windows.Forms.ContextMenu();
			this.ctxConfRefresh = new System.Windows.Forms.MenuItem();
			this.ctxConfSep1 = new System.Windows.Forms.MenuItem();
			this.ctxConfSave = new System.Windows.Forms.MenuItem();
			this.ctxConfRollback = new System.Windows.Forms.MenuItem();
			this.ctxConfSep2 = new System.Windows.Forms.MenuItem();
			this.ctxConfCut = new System.Windows.Forms.MenuItem();
			this.ctxConfCopy = new System.Windows.Forms.MenuItem();
			this.ctxConfPaste = new System.Windows.Forms.MenuItem();
			this.ctxConfDelete = new System.Windows.Forms.MenuItem();
            this.m_dsConfig = new Argix.Configuration.AppConfigDS();
			this.pnlDialog = new System.Windows.Forms.Panel();
			this.splitterV = new System.Windows.Forms.Splitter();
			this.trvFileConfig = new System.Windows.Forms.TreeView();
			this.pnlFileConfig = new System.Windows.Forms.Panel();
			this.lblFileConfig = new System.Windows.Forms.Label();
			this.txtFileConfig = new System.Windows.Forms.RichTextBox();
			this.tabTrace = new System.Windows.Forms.TabPage();
			((System.ComponentModel.ISupportInitialize)(this.grdLog)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mLog)).BeginInit();
			this.tabDialog.SuspendLayout();
			this.tabConfig.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdDBConfig)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_dsConfig)).BeginInit();
			this.pnlDialog.SuspendLayout();
			this.pnlFileConfig.SuspendLayout();
			this.tabTrace.SuspendLayout();
			this.SuspendLayout();
			// 
			// ctxLog
			// 
			this.ctxLog.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.ctxLogRefresh,
																				   this.ctxLogSep1,
																				   this.ctxLogCut,
																				   this.ctxLogCopy,
																				   this.ctxLogPaste,
																				   this.ctxLogSep2,
																				   this.ctxLogDelete});
			// 
			// ctxLogRefresh
			// 
			this.ctxLogRefresh.Index = 0;
			this.ctxLogRefresh.Text = "Refresh";
			this.ctxLogRefresh.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxLogSep1
			// 
			this.ctxLogSep1.Index = 1;
			this.ctxLogSep1.Text = "-";
			// 
			// ctxLogCut
			// 
			this.ctxLogCut.Index = 2;
			this.ctxLogCut.Text = "C&ut";
			this.ctxLogCut.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxLogCopy
			// 
			this.ctxLogCopy.Index = 3;
			this.ctxLogCopy.Text = "&Copy";
			this.ctxLogCopy.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxLogPaste
			// 
			this.ctxLogPaste.Index = 4;
			this.ctxLogPaste.Text = "&Paste";
			this.ctxLogPaste.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxLogSep2
			// 
			this.ctxLogSep2.Index = 5;
			this.ctxLogSep2.Text = "-";
			// 
			// ctxLogDelete
			// 
			this.ctxLogDelete.Index = 6;
			this.ctxLogDelete.Text = "Delete";
			this.ctxLogDelete.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// grdLog
			// 
			this.grdLog.ContextMenu = this.ctxLog;
			this.grdLog.Cursor = System.Windows.Forms.Cursors.Default;
			this.grdLog.DataMember = "ArgixLogTable";
			this.grdLog.DataSource = this.mLog;
			appearance1.BackColor = System.Drawing.SystemColors.Window;
			appearance1.FontData.Name = "Verdana";
			appearance1.FontData.SizeInPoints = 8F;
			appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
			appearance1.TextHAlignAsString = "Left";
			this.grdLog.DisplayLayout.Appearance = appearance1;
			ultraGridBand1.AddButtonCaption = "TLViewTable";
			ultraGridColumn1.Header.VisiblePosition = 0;
			ultraGridColumn1.Hidden = true;
			ultraGridColumn2.Header.VisiblePosition = 1;
			ultraGridColumn2.Hidden = true;
			ultraGridColumn2.Width = 120;
			ultraGridColumn3.Header.VisiblePosition = 3;
			ultraGridColumn3.Width = 48;
			ultraGridColumn4.Format = "MM/dd/yyyy HH:mm:ss tt";
			ultraGridColumn4.Header.VisiblePosition = 2;
			ultraGridColumn4.Width = 144;
			ultraGridColumn5.Header.VisiblePosition = 8;
			ultraGridColumn5.Width = 120;
			ultraGridColumn6.Header.VisiblePosition = 11;
			ultraGridColumn6.Hidden = true;
			ultraGridColumn6.Width = 72;
			ultraGridColumn7.Header.VisiblePosition = 12;
			ultraGridColumn7.Hidden = true;
			ultraGridColumn7.Width = 48;
			ultraGridColumn8.Header.VisiblePosition = 10;
			ultraGridColumn8.Width = 96;
			ultraGridColumn9.Header.VisiblePosition = 9;
			ultraGridColumn9.Width = 96;
			ultraGridColumn10.Header.VisiblePosition = 5;
			ultraGridColumn10.Width = 96;
			ultraGridColumn11.Header.VisiblePosition = 6;
			ultraGridColumn11.Width = 96;
			ultraGridColumn12.Header.VisiblePosition = 7;
			ultraGridColumn12.Width = 96;
			ultraGridColumn13.Header.VisiblePosition = 4;
			ultraGridColumn13.Width = 192;
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
															 ultraGridColumn13});
			this.grdLog.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
			this.grdLog.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
			appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
			appearance2.FontData.BoldAsString = "True";
			appearance2.FontData.Name = "Verdana";
			appearance2.FontData.SizeInPoints = 8F;
			appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			appearance2.TextHAlignAsString = "Left";
			this.grdLog.DisplayLayout.CaptionAppearance = appearance2;
			this.grdLog.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			this.grdLog.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
			this.grdLog.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
			this.grdLog.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
			appearance3.BackColor = System.Drawing.SystemColors.Control;
			appearance3.FontData.BoldAsString = "True";
			appearance3.FontData.Name = "Verdana";
			appearance3.FontData.SizeInPoints = 8F;
			appearance3.TextHAlignAsString = "Left";
			this.grdLog.DisplayLayout.Override.HeaderAppearance = appearance3;
			this.grdLog.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
			this.grdLog.DisplayLayout.Override.MaxSelectedRows = 0;
			appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
			this.grdLog.DisplayLayout.Override.RowAppearance = appearance4;
			this.grdLog.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			this.grdLog.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended;
			this.grdLog.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
			this.grdLog.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			this.grdLog.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
			this.grdLog.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
			this.grdLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grdLog.Location = new System.Drawing.Point(0, 0);
			this.grdLog.Name = "grdLog";
			this.grdLog.Size = new System.Drawing.Size(656, 312);
			this.grdLog.TabIndex = 113;
			this.grdLog.Text = "Argix Log";
			this.grdLog.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
			this.grdLog.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnLogEntrySelected);
			this.grdLog.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
			// 
			// mLog
			// 
			this.mLog.DataSetName = "TraceLogsDS";
			this.mLog.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// tabDialog
			// 
			this.tabDialog.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.tabDialog.Controls.Add(this.tabConfig);
			this.tabDialog.Controls.Add(this.tabTrace);
			this.tabDialog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabDialog.Location = new System.Drawing.Point(0, 0);
			this.tabDialog.Name = "tabDialog";
			this.tabDialog.SelectedIndex = 0;
			this.tabDialog.ShowToolTips = true;
			this.tabDialog.Size = new System.Drawing.Size(664, 338);
			this.tabDialog.TabIndex = 114;
			// 
			// tabConfig
			// 
			this.tabConfig.Controls.Add(this.splitterH);
			this.tabConfig.Controls.Add(this.grdDBConfig);
			this.tabConfig.Controls.Add(this.pnlDialog);
			this.tabConfig.Location = new System.Drawing.Point(4, 4);
			this.tabConfig.Name = "tabConfig";
			this.tabConfig.Size = new System.Drawing.Size(656, 312);
			this.tabConfig.TabIndex = 0;
			this.tabConfig.Text = "Configuration";
			// 
			// splitterH
			// 
			this.splitterH.Cursor = System.Windows.Forms.Cursors.HSplit;
			this.splitterH.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitterH.Location = new System.Drawing.Point(0, 156);
			this.splitterH.Name = "splitterH";
			this.splitterH.Size = new System.Drawing.Size(656, 3);
			this.splitterH.TabIndex = 117;
			this.splitterH.TabStop = false;
			// 
			// grdDBConfig
			// 
			this.grdDBConfig.ContextMenu = this.ctxConf;
			this.grdDBConfig.Cursor = System.Windows.Forms.Cursors.Default;
			this.grdDBConfig.DataMember = "ConfigTable";
			this.grdDBConfig.DataSource = this.m_dsConfig;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.FontData.Name = "Verdana";
			appearance5.FontData.SizeInPoints = 8F;
			appearance5.ForeColor = System.Drawing.SystemColors.WindowText;
			appearance5.TextHAlignAsString = "Left";
			this.grdDBConfig.DisplayLayout.Appearance = appearance5;
			ultraGridBand2.AddButtonCaption = "BwareStationTripTable";
			ultraGridColumn14.Header.VisiblePosition = 0;
			ultraGridColumn14.Width = 144;
			ultraGridColumn15.Header.VisiblePosition = 1;
			ultraGridColumn15.Width = 96;
			ultraGridColumn16.Header.VisiblePosition = 2;
			ultraGridColumn16.Width = 144;
			ultraGridColumn17.Header.VisiblePosition = 3;
			ultraGridColumn17.Width = 192;
			ultraGridColumn18.Header.VisiblePosition = 4;
			ultraGridColumn18.Width = 72;
			ultraGridBand2.Columns.AddRange(new object[] {
															 ultraGridColumn14,
															 ultraGridColumn15,
															 ultraGridColumn16,
															 ultraGridColumn17,
															 ultraGridColumn18});
			this.grdDBConfig.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
			appearance6.BackColor = System.Drawing.SystemColors.InactiveCaption;
			appearance6.FontData.BoldAsString = "True";
			appearance6.FontData.Name = "Verdana";
			appearance6.FontData.SizeInPoints = 8F;
			appearance6.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			appearance6.TextHAlignAsString = "Left";
			this.grdDBConfig.DisplayLayout.CaptionAppearance = appearance6;
			this.grdDBConfig.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			this.grdDBConfig.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
			this.grdDBConfig.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
			this.grdDBConfig.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
			appearance7.BackColor = System.Drawing.SystemColors.Control;
			appearance7.FontData.BoldAsString = "True";
			appearance7.FontData.Name = "Verdana";
			appearance7.FontData.SizeInPoints = 8F;
			appearance7.TextHAlignAsString = "Left";
			this.grdDBConfig.DisplayLayout.Override.HeaderAppearance = appearance7;
			this.grdDBConfig.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
			this.grdDBConfig.DisplayLayout.Override.MaxSelectedRows = 1;
			appearance8.BorderColor = System.Drawing.SystemColors.ControlLight;
			this.grdDBConfig.DisplayLayout.Override.RowAppearance = appearance8;
			this.grdDBConfig.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			this.grdDBConfig.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
			this.grdDBConfig.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			this.grdDBConfig.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			this.grdDBConfig.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
			this.grdDBConfig.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
			this.grdDBConfig.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grdDBConfig.Location = new System.Drawing.Point(9, 5);
			this.grdDBConfig.Name = "grdDBConfig";
			this.grdDBConfig.Size = new System.Drawing.Size(648, 153);
			this.grdDBConfig.TabIndex = 116;
			this.grdDBConfig.Text = "Database";
			this.grdDBConfig.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
			this.grdDBConfig.BeforeRowUpdate += new Infragistics.Win.UltraWinGrid.CancelableRowEventHandler(this.OnDBEntryUpdating);
			this.grdDBConfig.BeforeRowsDeleted += new Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventHandler(this.OnDBEntryDeleting);
			this.grdDBConfig.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.OnDBEntryFieldChanged);
			this.grdDBConfig.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnDBEntrySelected);
			this.grdDBConfig.BeforeCellActivate += new Infragistics.Win.UltraWinGrid.CancelableCellEventHandler(this.OnDBEntryFieldActivating);
			this.grdDBConfig.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
			// 
			// ctxConf
			// 
			this.ctxConf.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.ctxConfRefresh,
																					this.ctxConfSep1,
																					this.ctxConfSave,
																					this.ctxConfRollback,
																					this.ctxConfSep2,
																					this.ctxConfCut,
																					this.ctxConfCopy,
																					this.ctxConfPaste,
																					this.ctxConfDelete});
			// 
			// ctxConfRefresh
			// 
			this.ctxConfRefresh.Index = 0;
			this.ctxConfRefresh.Text = "Refresh";
			this.ctxConfRefresh.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxConfSep1
			// 
			this.ctxConfSep1.Index = 1;
			this.ctxConfSep1.Text = "-";
			// 
			// ctxConfSave
			// 
			this.ctxConfSave.Index = 2;
			this.ctxConfSave.Text = "Save";
			this.ctxConfSave.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxConfRollback
			// 
			this.ctxConfRollback.Index = 3;
			this.ctxConfRollback.Text = "Rollback";
			this.ctxConfRollback.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxConfSep2
			// 
			this.ctxConfSep2.Index = 4;
			this.ctxConfSep2.Text = "-";
			// 
			// ctxConfCut
			// 
			this.ctxConfCut.Index = 5;
			this.ctxConfCut.Text = "C&ut";
			this.ctxConfCut.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxConfCopy
			// 
			this.ctxConfCopy.Index = 6;
			this.ctxConfCopy.Text = "&Copy";
			this.ctxConfCopy.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxConfPaste
			// 
			this.ctxConfPaste.Index = 7;
			this.ctxConfPaste.Text = "&Paste";
			this.ctxConfPaste.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxConfDelete
			// 
			this.ctxConfDelete.Index = 8;
			this.ctxConfDelete.Text = "Delete";
			this.ctxConfDelete.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// m_dsConfig
			// 
			this.m_dsConfig.DataSetName = "AppConfigDS";
			this.m_dsConfig.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// pnlDialog
			// 
			this.pnlDialog.Controls.Add(this.splitterV);
			this.pnlDialog.Controls.Add(this.trvFileConfig);
			this.pnlDialog.Controls.Add(this.pnlFileConfig);
			this.pnlDialog.Controls.Add(this.txtFileConfig);
			this.pnlDialog.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlDialog.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pnlDialog.ForeColor = System.Drawing.SystemColors.WindowText;
			this.pnlDialog.Location = new System.Drawing.Point(0, 159);
			this.pnlDialog.Name = "pnlDialog";
			this.pnlDialog.Size = new System.Drawing.Size(656, 153);
			this.pnlDialog.TabIndex = 115;
			// 
			// splitterV
			// 
			this.splitterV.Location = new System.Drawing.Point(0, 24);
			this.splitterV.Name = "splitterV";
			this.splitterV.Size = new System.Drawing.Size(3, 129);
			this.splitterV.TabIndex = 116;
			this.splitterV.TabStop = false;
			// 
			// trvFileConfig
			// 
			this.trvFileConfig.ContextMenu = this.ctxConf;
			this.trvFileConfig.ImageIndex = -1;
			this.trvFileConfig.Location = new System.Drawing.Point(3, 24);
			this.trvFileConfig.Name = "trvFileConfig";
			this.trvFileConfig.SelectedImageIndex = -1;
			this.trvFileConfig.Size = new System.Drawing.Size(192, 129);
			this.trvFileConfig.TabIndex = 115;
			this.trvFileConfig.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnTreeviewMouseDown);
			this.trvFileConfig.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnFileSelected);
			this.trvFileConfig.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.OnFileSelecting);
			// 
			// pnlFileConfig
			// 
			this.pnlFileConfig.Controls.Add(this.lblFileConfig);
			this.pnlFileConfig.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlFileConfig.DockPadding.All = 3;
			this.pnlFileConfig.Location = new System.Drawing.Point(0, 0);
			this.pnlFileConfig.Name = "pnlFileConfig";
			this.pnlFileConfig.Size = new System.Drawing.Size(656, 24);
			this.pnlFileConfig.TabIndex = 114;
			// 
			// lblFileConfig
			// 
			this.lblFileConfig.BackColor = System.Drawing.SystemColors.Highlight;
			this.lblFileConfig.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblFileConfig.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblFileConfig.ForeColor = System.Drawing.SystemColors.HighlightText;
			this.lblFileConfig.Location = new System.Drawing.Point(3, 3);
			this.lblFileConfig.Name = "lblFileConfig";
			this.lblFileConfig.Size = new System.Drawing.Size(650, 18);
			this.lblFileConfig.TabIndex = 113;
			this.lblFileConfig.Text = "File";
			this.lblFileConfig.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtFileConfig
			// 
			this.txtFileConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtFileConfig.AutoWordSelection = true;
			this.txtFileConfig.ContextMenu = this.ctxConf;
			this.txtFileConfig.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtFileConfig.Location = new System.Drawing.Point(207, 24);
			this.txtFileConfig.Name = "txtFileConfig";
			this.txtFileConfig.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
			this.txtFileConfig.ShowSelectionMargin = true;
			this.txtFileConfig.Size = new System.Drawing.Size(448, 129);
			this.txtFileConfig.TabIndex = 111;
			this.txtFileConfig.Text = "";
			this.txtFileConfig.WordWrap = false;
			this.txtFileConfig.SelectionChanged += new System.EventHandler(this.OnFileConfigurationChanged);
			// 
			// tabTrace
			// 
			this.tabTrace.Controls.Add(this.grdLog);
			this.tabTrace.Location = new System.Drawing.Point(4, 4);
			this.tabTrace.Name = "tabTrace";
			this.tabTrace.Size = new System.Drawing.Size(656, 312);
			this.tabTrace.TabIndex = 1;
			this.tabTrace.Text = "Trace Log";
			// 
			// winApp
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(664, 338);
			this.Controls.Add(this.tabDialog);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.SystemColors.WindowText;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "winApp";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "App Support";
			this.Resize += new System.EventHandler(this.OnFormResize);
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.Activated += new System.EventHandler(this.OnActivated);
			this.Deactivate += new System.EventHandler(this.OnDeactivate);
			((System.ComponentModel.ISupportInitialize)(this.grdLog)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mLog)).EndInit();
			this.tabDialog.ResumeLayout(false);
			this.tabConfig.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdDBConfig)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_dsConfig)).EndInit();
			this.pnlDialog.ResumeLayout(false);
			this.pnlFileConfig.ResumeLayout(false);
			this.tabTrace.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
				//Load conditions
				this.Visible = true;
				Application.DoEvents();
				#region Tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
				#endregion
			
				//Load controls
				this.Text = this.mTsortApp.DisplayName;
				this.pnlDialog.Height = this.Height / 2;
				#region Grid customizations from normal layout (to support cell editing)
				this.grdDBConfig.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
				this.grdDBConfig.DisplayLayout.Override.SelectTypeRow = SelectType.Single;
				this.grdDBConfig.DisplayLayout.Override.SelectTypeCell = SelectType.Single;
				this.grdDBConfig.DisplayLayout.TabNavigation = TabNavigation.NextCell;
				this.grdDBConfig.DisplayLayout.Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
				this.grdDBConfig.DisplayLayout.Override.AllowDelete = DefaultableBoolean.True;
				this.grdDBConfig.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
				this.grdDBConfig.DisplayLayout.Override.MaxSelectedCells = 1;
				this.grdDBConfig.DisplayLayout.Override.CellClickAction = CellClickAction.Edit;
				this.grdDBConfig.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.Edit;
				this.grdDBConfig.DisplayLayout.Bands[0].Columns["Application"].CellActivation = Activation.AllowEdit;
				this.grdDBConfig.DisplayLayout.Bands[0].Columns["PCName"].CellActivation = Activation.AllowEdit;
				this.grdDBConfig.DisplayLayout.Bands[0].Columns["Key"].CellActivation = Activation.AllowEdit;
				this.grdDBConfig.DisplayLayout.Bands[0].Columns["Value"].CellActivation = Activation.AllowEdit;
				this.grdDBConfig.DisplayLayout.Bands[0].Columns["Security"].CellActivation = Activation.AllowEdit;
				this.grdDBConfig.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdDBConfig.DisplayLayout.Bands[0].Columns["PCName"].SortIndicator = SortIndicator.Ascending;
				
				this.grdLog.Text = this.mTsortApp.EventLogName + " (" + this.mTsortApp.ConnectionInfo + ")";
				this.grdLog.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdLog.DisplayLayout.Bands[0].Columns["Date"].SortIndicator = SortIndicator.Descending;
				this.grdLog.DataSource = this.mTsortApp.TraceLog.LogEntries;
				#endregion
				this.grdDBConfig.DataSource = this.mTsortApp.DBConfiguration.ConfigurationEntries;
				
				//Get initial views
				this.mTsortApp.Refresh();
				this.mTsortApp.FileConfiguration.Refresh();
				this.mTsortApp.DBConfiguration.Refresh();
				this.grdDBConfig.Text = this.mTsortApp.DBConfiguration.ConnectionInfo;
				this.lblFileConfig.Text = this.mTsortApp.FileConfiguration.ActiveFile.Directory.FullName;
			} 
			catch(Exception ex) { reportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnFormResize(object sender, System.EventArgs e) { }
		private void OnActivated(object sender, System.EventArgs e) {
			//Event handler for window activation
			this.mTsortApp.TreeView.SelectedNode = this.mTsortApp;
		}
		private void OnDeactivate(object sender, System.EventArgs e) {
			//Event handler for window de-activation
		}
		#region Database Configuration
		private void OnDBConfigurationRefreshed(object sender, EventArgs e) {
			//Event handler for refresh of database configuration
			try {
				//Select an entry
				int iIndex = -1;
				if(this.grdDBConfig.Rows.Count > 0) {
					iIndex = 0;
					if(this.mSelectedConfigEntry != null) {
						//Find prior selected entry
						for(int i=0; i<this.grdDBConfig.Rows.Count; i++) {
							if(this.grdDBConfig.Rows[i].Cells["PCName"].Value.ToString() == this.mSelectedConfigEntry.PCName && this.grdDBConfig.Rows[i].Cells["Key"].Value.ToString() == this.mSelectedConfigEntry.Key) {
								iIndex = this.grdDBConfig.Rows[i].Index;
								break;
							}
						}
					}
					this.grdDBConfig.Rows[iIndex].Activate();
					this.grdDBConfig.ActiveRow.Selected = true;
					this.grdDBConfig.Refresh();
				}
				else
					OnDBEntrySelected(null, null);
			}
			catch(Exception ex) { reportError(ex); }
		}
		private void OnDBEntrySelected(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in selected data entry
			try {
				//Clear reference to prior entry object
				this.mSelectedConfigEntry = null;
				if(this.grdDBConfig.Selected.Rows.Count > 0) {
					//Get an entry object for the selected row
					string pcName = this.grdDBConfig.Selected.Rows[0].Cells["PCName"].Value.ToString();
					string key = this.grdDBConfig.Selected.Rows[0].Cells["Key"].Value.ToString();
					this.mSelectedConfigEntry = this.mTsortApp.DBConfiguration.Item(pcName, key);
				}
			}
			catch(Exception ex) { reportError(ex); }
			finally { setUserServices(); }
		}		
		private void OnDBEntryFieldActivating(object sender, Infragistics.Win.UltraWinGrid.CancelableCellEventArgs e) {
			//Event handler for data entry cell activating
			try {
				//Enable\disable cell editing
				switch(e.Cell.Column.Key.ToString()) { 
					case "Application":	e.Cell.Activation = Activation.NoEdit; break;
					case "PCName":		e.Cell.Activation = (e.Cell.Row.Cells["Application"].Value.ToString() == "" || e.Cell.Value.ToString() != "Default") ? Activation.AllowEdit : Activation.NoEdit; break;
					case "Key":			e.Cell.Activation = (e.Cell.Row.Cells["Application"].Value.ToString() == "" || e.Cell.Row.Cells["PCName"].Value.ToString() != "Default") ? Activation.AllowEdit : Activation.NoEdit; break;
					case "Value":		e.Cell.Activation = Activation.AllowEdit; break;
					case "Security":	e.Cell.Activation = Activation.AllowEdit; break;
				}
				e.Cell.Selected = true;
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnDBEntryFieldChanged(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e) {
			//Event handler for change in a data entry cell value
			try {
				//Set application name for new entries
				if(e.Cell.Row.Cells["Application"].Value.ToString() == "")
					e.Cell.Row.Cells["Application"].Value = this.mTsortApp.DBConfiguration.ProductName;
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnDBEntryUpdating(object sender, Infragistics.Win.UltraWinGrid.CancelableRowEventArgs e) {
			//Event handler for data entry row updated
			try {
				//There is no selected row when updating- at a cell level
				string app = e.Row.Cells["Application"].Value.ToString();
				string name = e.Row.Cells["PCName"].Value.ToString();
				string key = e.Row.Cells["Key"].Value.ToString();
				string value = e.Row.Cells["Value"].Value.ToString();
				string sec = e.Row.Cells["Security"].Value.ToString();
				if(app != "" && name != "" && key != "" && value != "" && sec != "") {
					if(e.Row.IsAddRow) {
						//Add new entry
						DBConfigEntry entry = this.mTsortApp.DBConfiguration.Item();
						entry.PCName = name;
						entry.Key = key;
						entry.Value = value;
						entry.Security = sec;
						entry.Create();
						this.mTsortApp.DBConfiguration.Refresh();
					}
					else {
						//Update existing
						DBConfigEntry entry = this.mTsortApp.DBConfiguration.Item(name, key);
						entry.Value = value;
						entry.Security = sec;
						entry.Update();
					}
				}
				else
					e.Cancel = true;
			} 
			catch(Exception ex) { reportError(ex); }
			finally { setUserServices(); }
		}
		private void OnDBEntryDeleting(object sender, BeforeRowsDeletedEventArgs e) {
			//Event hanlder for rows deleting
			try {
				//Cannot delete 'Default' entries or the new row entry
				e.DisplayPromptMsg = false;
				e.Cancel = true;
				if(this.mSelectedConfigEntry.PCName != "Default" && this.mSelectedConfigEntry.PCName != "") 
					this.ctxLogDelete.PerformClick();
			} 
			catch(Exception ex) { reportError(ex); }
		}
		#endregion
		#region File Configuration: 
		private void OnFileConfigurationRefreshed(object sender, EventArgs e) {
			//Event hanlder for refresh of file configuration
			this.Cursor = Cursors.WaitCursor;
			try {
				//
				this.trvFileConfig.Nodes.Clear();
				TreeNode activeNodes = new TreeNode(NODE_ACTIVE);
				this.trvFileConfig.Nodes.Add(activeNodes);
				TreeNode activeNode = new TreeNode(this.mTsortApp.FileConfiguration.ActiveFile.Name);
				activeNodes.Nodes.Add(activeNode);
				TreeNode rollbackNodes = new TreeNode(NODE_ROLLBACK);
				this.trvFileConfig.Nodes.Add(rollbackNodes);
				for(int i=0; i<this.mTsortApp.FileConfiguration.RollbackFileCount; i++) {
					TreeNode rollbackNode = new TreeNode(this.mTsortApp.FileConfiguration.RollbackFile(i).Name);
					rollbackNodes.Nodes.Add(rollbackNode);
				}
				this.trvFileConfig.SelectedNode = activeNode;
				activeNode.Expand();
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnFileSelecting(object sender, System.Windows.Forms.TreeViewCancelEventArgs e) {
			//Event handler for file selecting
		}
		private void OnFileSelected(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			//Event handler for configuration file selected in treeview
			try {
				string fileName = this.trvFileConfig.SelectedNode.Text;
				this.txtFileConfig.Text = this.mTsortApp.FileConfiguration.Read(fileName);
				this.mIsDirty = false;
			}
			catch(Exception ex) { reportError(ex); }
			finally { setUserServices(); }
		}
		private void OnTreeviewMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Event handler for mousedown in treeview
			try {
				//
				if(e.Button == MouseButtons.Right) {
					Control ctl = this.trvFileConfig.GetChildAtPoint(new Point(e.X, e.Y));
				}
			}
			catch(Exception ex) { reportError(ex); }
			finally { setUserServices(); }
		}
		private void OnFileConfigurationChanged(object sender, System.EventArgs e) {
			//Event handler for change in text selection of file configuration
			try {
				//Flag data as changed
				this.mIsDirty = true;
			}
			catch(Exception ex) { reportError(ex); }
			finally { setUserServices(); }
		}
		#endregion
		#region Trace Logs: OnLogRefreshed(), OnLogEntrySelected()
		private void OnLogRefreshed(object sender, EventArgs e) {
			//Event handler for refresh of database configuration
			try {
				//Select an entry
				int iIndex = 0;
				if(this.grdLog.Rows.Count > 0) {
					this.grdLog.Rows[iIndex].Activate();
					this.grdLog.ActiveRow.Selected = true;
					this.grdLog.Refresh();
				}
				OnLogEntrySelected(null, null);
			}
			catch(Exception ex) { reportError(ex); }
		}
		private void OnLogEntrySelected(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in selected data entry
			try {
				//Clear reference to prior entry object
				this.mSelectedTraceEntry = null;
				if(this.grdLog.Selected.Rows.Count > 0) {
					//Get an entry object for the selected row
					int id = Convert.ToInt32(this.grdLog.Selected.Rows[0].Cells["ID"].Value);
					this.mSelectedTraceEntry = this.mTsortApp.TraceLog.Item(id);
				}
			}
			catch(Exception ex) { reportError(ex); }
			finally { setUserServices(); }
		}		
		#endregion
		private void OnGridMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Event handler for mouse down event for all grids
			try {				
				//Select rows on right click
				UltraGrid oGrid = (UltraGrid)sender;
				oGrid.Focus();
				UIElement oUIElement = oGrid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));
				if(oUIElement != null) {
					object oContext = oUIElement.GetContext(typeof(UltraGridRow));
					if(oContext != null) {
						if(e.Button == MouseButtons.Left) {
							//OnDragDropMouseDown(sender, e);
						}
						else if(e.Button == MouseButtons.Right) {
							UltraGridRow oRow = (UltraGridRow)oContext;
							if(!oRow.Selected) oGrid.Selected.Rows.Clear();
							oRow.Selected = true;
						}
					}
					else {
						//Deselect rows in the white space of the grid or deactivate the active   
						//row when in a scroll region to prevent double-click action
						if(oUIElement.Parent.GetType() == typeof(DataAreaUIElement))
							oGrid.Selected.Rows.Clear();
						else if(oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement))
							if(oGrid.Selected.Rows.Count > 0) oGrid.Selected.Rows[0].Activated = false;
					}
				}
			} 
			catch(Exception ex) { reportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		#region User Services: OnMenuClick()
		private void OnMenuClick(object sender, System.EventArgs e) {
			//Event handler for menu selection
			try  {
				MenuItem menu = (MenuItem)sender;
				switch(menu.Text)  {
					case MNU_REFRESH:	
						this.Cursor = Cursors.WaitCursor;
						switch(this.tabDialog.SelectedTab.Name) {
							case "tabConfig":	
								if(this.grdDBConfig.Focused) 
									this.mTsortApp.DBConfiguration.Refresh(); 
								else if(this.trvFileConfig.Focused) 
									this.mTsortApp.FileConfiguration.Refresh(); 
								else if(this.txtFileConfig.Focused) 
									this.mTsortApp.FileConfiguration.Refresh(); 
								break;
							case "tabTrace":	
								this.mTsortApp.TraceLog.Refresh(); 
								break;
						}
						break;
					case MNU_SAVE:		
						this.mTsortApp.FileConfiguration.Save(this.txtFileConfig.Text);
						break;
					case MNU_ROLLBACK:	
						this.mTsortApp.FileConfiguration.Rollback(this.trvFileConfig.SelectedNode.Text);
						break;
					case MNU_CUT:		
						this.txtFileConfig.Cut();
						break;
					case MNU_COPY:		
						#region Copy
						if(this.txtFileConfig.Focused)
							this.txtFileConfig.Copy();
						else if(this.grdDBConfig.Focused) {
							if(this.grdDBConfig.Selected.Rows.Count > 0) {
								string s = this.grdDBConfig.Selected.Rows[0].Cells["Application"].Value.ToString() + ",";
								s += this.grdDBConfig.Selected.Rows[0].Cells["PCName"].Value.ToString() + ",";
								s += this.grdDBConfig.Selected.Rows[0].Cells["Key"].Value.ToString() + ",";
								s += this.grdDBConfig.Selected.Rows[0].Cells["Value"].Value.ToString() + ",";
								s += this.grdDBConfig.Selected.Rows[0].Cells["Security"].Value.ToString();
								Clipboard.SetDataObject(s);
							}
						}
						#endregion
						break;
					case MNU_PASTE:		
						#region Paste
						if(this.txtFileConfig.Focused)
							this.txtFileConfig.Paste();
						else if(this.grdDBConfig.Focused) {
							IDataObject o = Clipboard.GetDataObject();
							string csv = (string)o.GetData("Text");
							string[] tokens = csv.Split((char)',');
							if(tokens.Length == 5) {
								this.grdDBConfig.Selected.Rows[0].Cells["PCName"].Value = tokens[1];
								this.grdDBConfig.Selected.Rows[0].Cells["Key"].Value = tokens[2];
								this.grdDBConfig.Selected.Rows[0].Cells["Value"].Value = tokens[3];
								this.grdDBConfig.Selected.Rows[0].Cells["Security"].Value = tokens[4];
							}
						}
						#endregion
						break;
					case MNU_DELETE:	
						if(this.grdDBConfig.Focused) {
							if(MessageBox.Show(this, "Delete the selected configuration entry?", App.Product, MessageBoxButtons.YesNo) == DialogResult.Yes) 
								this.mSelectedConfigEntry.Delete();
						}
						else if(this.grdLog.Focused) {
							if(MessageBox.Show(this, "Delete the selected log entries?", App.Product, MessageBoxButtons.YesNo) == DialogResult.Yes) {
								//Create entries prior to delete because each delete raises refresh
								//event which changes this.grdLog.Selected.Rows collection
								TraceLogEntry[] entries = new TraceLogEntry[this.grdLog.Selected.Rows.Count];
								for(int i=0; i<this.grdLog.Selected.Rows.Count; i++) {
									int id = Convert.ToInt32(this.grdLog.Selected.Rows[i].Cells["ID"].Value);
									entries[i] = this.mTsortApp.TraceLog.Item(id);
								}
								for(int j=0; j<entries.Length; j++) entries[j].Delete();
							}
						}
						break;
				}
			}
			catch(Exception ex) { reportError(ex); }
			finally  { setUserServices(); this.Cursor = Cursors.Default; }
		}
		#endregion
		#region Local Services: setUserServices(), reportError()
		private void setUserServices() {
			//Set user services
			bool canSave=false, canRollback=false;
			bool canCut=false, canCopy=false, canPaste=false;
			bool canDelete=false;
			try {
				//Determine context and state
				switch(this.tabDialog.SelectedTab.Name) {
					case "tabConfig":	
						if(this.grdDBConfig.Focused) {
							canCopy = (this.mSelectedConfigEntry != null);
							if(this.mSelectedConfigEntry != null) {
								canPaste = (this.mSelectedConfigEntry.PCName == "");
								canDelete = (this.mSelectedConfigEntry.PCName != "Default" && this.mSelectedConfigEntry.PCName != "");
							}
						}
						else if(this.trvFileConfig.Focused) {
							canSave = this.mIsDirty;
							if(this.trvFileConfig.SelectedNode != null) 
								canRollback = this.trvFileConfig.SelectedNode.Parent.Text == "Rollback";
						}
						else if(this.txtFileConfig.Focused) {
							canSave = this.mIsDirty;
							canCut = (this.txtFileConfig.SelectedText.Length > 0);
							canCopy = (this.txtFileConfig.SelectedText.Length > 0);
							canPaste = this.txtFileConfig.CanPaste(DataFormats.GetFormat("Text"));
						}
						break;
					case "tabTrace":	
						if(this.grdLog.Focused) {
							canDelete = (this.mSelectedTraceEntry != null);
						}
						break;
				}
				
				//Set menu/context menu states
				this.ctxConfRefresh.Enabled = true;
				this.ctxConfSave.Enabled = canSave;
				this.ctxConfRollback.Enabled = canRollback;
				this.ctxConfCut.Enabled = canCut;
				this.ctxConfCopy.Enabled = canCopy;
				this.ctxConfPaste.Enabled = canPaste;
				this.ctxConfDelete.Enabled = canDelete;
				
				this.ctxLogRefresh.Enabled = true;
				this.ctxLogCut.Enabled = canCut;
				this.ctxLogCopy.Enabled = canCopy;
				this.ctxLogPaste.Enabled = canPaste;
				this.ctxLogDelete.Enabled = canDelete;
			}
			catch(Exception ex) { reportError(ex); }
			finally { if(this.ServiceStatesChanged!=null) this.ServiceStatesChanged(this, new EventArgs()); }
		}
		private void reportError(Exception ex) { reportError(ex, false, LogLevel.None); }
		private void reportError(Exception ex, bool displayMessage, LogLevel level) { 
			if(this.ErrorMessage != null) this.ErrorMessage(this, new ErrorEventArgs(ex,displayMessage,level));
		}
		#endregion
	}
}
