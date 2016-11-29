//	File:	sortui.cs
//	Author:	J. Heary
//	Date:	06/05/07
//	Desc:	User interface control for the Sort Library..
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Argix;
using Tsort.Freight;
using Tsort.Sort;

namespace Tsort.Sort {
	/// <summary>User interface control for the Sort Library.</summary>
	public class SortUI : System.Windows.Forms.UserControl {
		//Members
		private StationOperator mStationOperator=null;
		private LogLevel mLogLevel=LogLevel.None;
		private ArgixTextBoxListener mTraceListener=null;
		private System.Windows.Forms.ToolTip mToolTip = null;
		
		private const string CMD_REFRESH = "Refresh";
		private const string CMD_REFRESHCACHE = "Refresh Cache";
		#region Controls
		private System.Windows.Forms.ContextMenu ctxMain;
		private System.Windows.Forms.MenuItem ctxRefresh;
		private System.Windows.Forms.MenuItem ctxSep1;
		private System.Windows.Forms.MenuItem ctxClear;
		private System.Windows.Forms.ColumnHeader colTDS;
		private System.Windows.Forms.ColumnHeader colClient;
		private System.Windows.Forms.ColumnHeader colShipper;
		private System.Windows.Forms.ColumnHeader colFreight;
		private System.Windows.Forms.ColumnHeader colSort;
		private System.Windows.Forms.ColumnHeader colIBLabel;
		private System.Windows.Forms.ImageList imgDialog;
		private System.Windows.Forms.TabControl tabMain;
		private System.Windows.Forms.TabPage tabSort;
		private System.Windows.Forms.Button btnRefreshCache;
		private System.Windows.Forms.Label _lblAssignments;
		private System.Windows.Forms.ListView lsvAssignments;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.TabPage tabSortedItem;
		private System.Windows.Forms.GroupBox grpItemsStatistics;
		private System.Windows.Forms.TextBox txtStatisticsDownTime;
		private System.Windows.Forms.TextBox txtStatisticsStartTime;
		private System.Windows.Forms.Label lblDownTime;
		private System.Windows.Forms.Label lblStartTime;
		private System.Windows.Forms.GroupBox grpLine2;
		private System.Windows.Forms.GroupBox grpLine1;
		private System.Windows.Forms.Label lblInput3;
		private System.Windows.Forms.TextBox txtInput3;
		private System.Windows.Forms.TextBox txtZone;
		private System.Windows.Forms.TextBox txtTLNumber;
		private System.Windows.Forms.TextBox txtDamage;
		private System.Windows.Forms.TextBox txtStoreName;
		private System.Windows.Forms.TextBox txtInput2;
		private System.Windows.Forms.TextBox txtInput1;
		private System.Windows.Forms.Label lblZone;
		private System.Windows.Forms.Label lblDamage;
		private System.Windows.Forms.Label lblTLNumber;
		private System.Windows.Forms.Label lblStoreName;
		private System.Windows.Forms.Label lblInput2;
		private System.Windows.Forms.Label lblInput1;
		private System.Windows.Forms.Label lblLableNumber;
		private System.Windows.Forms.TextBox txtLabelNumber;
		private System.Windows.Forms.TextBox txtWeight;
		private System.Windows.Forms.Label lblWeight;
		private System.Windows.Forms.TabPage tabTrace;
		private System.Windows.Forms.RichTextBox txtTrace;
		private System.Windows.Forms.Button btnScale;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtStatisticsUpTime;
		private System.Windows.Forms.Label lblUpTime;
		private System.Windows.Forms.TextBox txtStatisticsRunningTime;
		private System.Windows.Forms.TextBox txtStatisticsSorted;
		private System.Windows.Forms.Label lblRunningTime;
		private System.Windows.Forms.TextBox txtStatisticsItemsMinMax;
		private System.Windows.Forms.Label lblItemsMinMax;
		private System.Windows.Forms.Label lblSorted;
		private System.Windows.Forms.TextBox txtStatisticsItemsPerUptime;
		private System.Windows.Forms.Label lblItemsPerUptime;
		private System.Windows.Forms.Button btnResetStats;
		private System.Windows.Forms.TextBox txtStatisticsUnsorted;
		private System.Windows.Forms.Label lblUnsorted;
		private System.Windows.Forms.StatusBar stbAssignments;
		private System.Windows.Forms.StatusBarPanel pnlLanePrefix;
		private System.Windows.Forms.StatusBarPanel pnlShipOverride;
		private System.Windows.Forms.StatusBarPanel pnlUPSAllowed;
		private System.Windows.Forms.StatusBarPanel pnlOperator;
		private System.Windows.Forms.StatusBarPanel pnlStation;
		private System.Windows.Forms.StatusBarPanel pnlSortType;
		private System.Windows.Forms.StatusBarPanel pnlScaleType;
		private System.ComponentModel.IContainer components;
		#endregion
				
		//Interface
		public SortUI() {
			//Constructor
			try {
				InitializeComponent();
				this.btnRefresh.Text = CMD_REFRESH;
				this.btnRefreshCache.Text = CMD_REFRESHCACHE;
				this.mToolTip = new ToolTip();
				this.tabMain.TabPages.Remove(tabTrace);
			} 
			catch(Exception ex) { throw new ApplicationException("Unexpected error creating new SortUI instance.", ex); }
		}
		protected override void Dispose( bool disposing ) { if( disposing ) { if(components != null) { components.Dispose(); } } base.Dispose( disposing ); }
		/// <summary>
		/// Specifies the Tsort.Sort.StationOperator object that provides data for the control.
		/// </summary>
		public Tsort.Sort.StationOperator Operator { 
			get { return this.mStationOperator; } 
			set { 
				this.mStationOperator = value;
				if(this.mStationOperator != null) {
					this.mStationOperator.StationAssignmentsChanged += new EventHandler(OnAssignmentsChanged);
					this.mStationOperator.SortedItemCreated += new SortedItemEventHandler(OnSortedItemCreated);
					this.mStationOperator.SortedItemComplete += new SortedItemEventHandler(OnSortedItemComplete);
				}
			} 
		}
		/// <summary>Turn Trace on\off.</summary>
		/// <remarks>
		/// To turn on:  set to LogLevel.Debug or greater; this will make the Trace tab visible.
		/// To turn-off: set to LogLevel.None; this will hide the Trace tab.
		/// </remarks>
		public LogLevel TraceOn { 
			get { return this.mLogLevel; } 
			set { 
				this.mLogLevel = value;
				if(this.mLogLevel > LogLevel.None) {
					if(this.mTraceListener != null) {
						ArgixTrace.RemoveListener(this.mTraceListener);
						this.mTraceListener.Close();
						this.mTraceListener.Dispose();
					}
					else {
						this.tabMain.TabPages.Add(this.tabTrace);
					}
					this.mTraceListener = new ArgixTextBoxListener(this.mLogLevel, this.txtTrace);
					ArgixTrace.AddListener(this.mTraceListener);
				}
				else {
					if(this.mTraceListener != null) {
						ArgixTrace.RemoveListener(this.mTraceListener);
						this.mTraceListener.Close();
						this.mTraceListener.Dispose();
						this.tabMain.TabPages.Remove(this.tabTrace);
					}
				}
				this.tabMain.SelectedIndex = 0;
			}
		}
		/// <summary>
		/// Gets and sets the visibility of the refresh cache button.
		/// </summary>
		public bool RefreshCacheVisible { get { return this.btnRefreshCache.Visible; } set { this.btnRefreshCache.Visible = value; } }
		/// <summary>
		/// Gets and sets the visibility of the refresh button.
		/// </summary>
		public bool RefreshVisible { get { return this.btnRefresh.Visible; } set { this.btnRefresh.Visible = value; } }
		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SortUI));
			this.ctxMain = new System.Windows.Forms.ContextMenu();
			this.ctxRefresh = new System.Windows.Forms.MenuItem();
			this.ctxSep1 = new System.Windows.Forms.MenuItem();
			this.ctxClear = new System.Windows.Forms.MenuItem();
			this.colTDS = new System.Windows.Forms.ColumnHeader();
			this.colClient = new System.Windows.Forms.ColumnHeader();
			this.colShipper = new System.Windows.Forms.ColumnHeader();
			this.colFreight = new System.Windows.Forms.ColumnHeader();
			this.colSort = new System.Windows.Forms.ColumnHeader();
			this.colIBLabel = new System.Windows.Forms.ColumnHeader();
			this.imgDialog = new System.Windows.Forms.ImageList(this.components);
			this.tabMain = new System.Windows.Forms.TabControl();
			this.tabSort = new System.Windows.Forms.TabPage();
			this.btnScale = new System.Windows.Forms.Button();
			this.stbAssignments = new System.Windows.Forms.StatusBar();
			this.pnlOperator = new System.Windows.Forms.StatusBarPanel();
			this.pnlSortType = new System.Windows.Forms.StatusBarPanel();
			this.pnlLanePrefix = new System.Windows.Forms.StatusBarPanel();
			this.pnlShipOverride = new System.Windows.Forms.StatusBarPanel();
			this.pnlUPSAllowed = new System.Windows.Forms.StatusBarPanel();
			this.pnlScaleType = new System.Windows.Forms.StatusBarPanel();
			this.pnlStation = new System.Windows.Forms.StatusBarPanel();
			this.btnRefreshCache = new System.Windows.Forms.Button();
			this._lblAssignments = new System.Windows.Forms.Label();
			this.lsvAssignments = new System.Windows.Forms.ListView();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.tabSortedItem = new System.Windows.Forms.TabPage();
			this.label1 = new System.Windows.Forms.Label();
			this.grpItemsStatistics = new System.Windows.Forms.GroupBox();
			this.txtStatisticsUnsorted = new System.Windows.Forms.TextBox();
			this.lblUnsorted = new System.Windows.Forms.Label();
			this.btnResetStats = new System.Windows.Forms.Button();
			this.txtStatisticsItemsPerUptime = new System.Windows.Forms.TextBox();
			this.txtStatisticsItemsMinMax = new System.Windows.Forms.TextBox();
			this.txtStatisticsRunningTime = new System.Windows.Forms.TextBox();
			this.txtStatisticsSorted = new System.Windows.Forms.TextBox();
			this.txtStatisticsUpTime = new System.Windows.Forms.TextBox();
			this.txtStatisticsDownTime = new System.Windows.Forms.TextBox();
			this.txtStatisticsStartTime = new System.Windows.Forms.TextBox();
			this.lblItemsPerUptime = new System.Windows.Forms.Label();
			this.lblItemsMinMax = new System.Windows.Forms.Label();
			this.lblRunningTime = new System.Windows.Forms.Label();
			this.lblSorted = new System.Windows.Forms.Label();
			this.lblUpTime = new System.Windows.Forms.Label();
			this.lblDownTime = new System.Windows.Forms.Label();
			this.lblStartTime = new System.Windows.Forms.Label();
			this.grpLine2 = new System.Windows.Forms.GroupBox();
			this.grpLine1 = new System.Windows.Forms.GroupBox();
			this.lblInput3 = new System.Windows.Forms.Label();
			this.txtInput3 = new System.Windows.Forms.TextBox();
			this.txtZone = new System.Windows.Forms.TextBox();
			this.txtTLNumber = new System.Windows.Forms.TextBox();
			this.txtDamage = new System.Windows.Forms.TextBox();
			this.txtStoreName = new System.Windows.Forms.TextBox();
			this.txtInput2 = new System.Windows.Forms.TextBox();
			this.txtInput1 = new System.Windows.Forms.TextBox();
			this.lblZone = new System.Windows.Forms.Label();
			this.lblDamage = new System.Windows.Forms.Label();
			this.lblTLNumber = new System.Windows.Forms.Label();
			this.lblStoreName = new System.Windows.Forms.Label();
			this.lblInput2 = new System.Windows.Forms.Label();
			this.lblInput1 = new System.Windows.Forms.Label();
			this.lblLableNumber = new System.Windows.Forms.Label();
			this.txtLabelNumber = new System.Windows.Forms.TextBox();
			this.txtWeight = new System.Windows.Forms.TextBox();
			this.lblWeight = new System.Windows.Forms.Label();
			this.tabTrace = new System.Windows.Forms.TabPage();
			this.txtTrace = new System.Windows.Forms.RichTextBox();
			this.tabMain.SuspendLayout();
			this.tabSort.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pnlOperator)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlSortType)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlLanePrefix)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlShipOverride)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlUPSAllowed)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlScaleType)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlStation)).BeginInit();
			this.tabSortedItem.SuspendLayout();
			this.grpItemsStatistics.SuspendLayout();
			this.tabTrace.SuspendLayout();
			this.SuspendLayout();
			// 
			// ctxMain
			// 
			this.ctxMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.ctxRefresh,
																					this.ctxSep1,
																					this.ctxClear});
			this.ctxMain.Popup += new System.EventHandler(this.OnMenuPopup);
			// 
			// ctxRefresh
			// 
			this.ctxRefresh.Index = 0;
			this.ctxRefresh.Text = "Refresh";
			this.ctxRefresh.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxSep1
			// 
			this.ctxSep1.Index = 1;
			this.ctxSep1.Text = "-";
			// 
			// ctxClear
			// 
			this.ctxClear.Index = 2;
			this.ctxClear.Text = "Clear";
			this.ctxClear.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// colTDS
			// 
			this.colTDS.Text = "TDS#";
			this.colTDS.Width = 72;
			// 
			// colClient
			// 
			this.colClient.Text = "Client";
			this.colClient.Width = 168;
			// 
			// colShipper
			// 
			this.colShipper.Text = "Shipper";
			this.colShipper.Width = 168;
			// 
			// colFreight
			// 
			this.colFreight.Text = "Freight";
			this.colFreight.Width = 72;
			// 
			// colSort
			// 
			this.colSort.Text = "Sort As";
			this.colSort.Width = 72;
			// 
			// colIBLabel
			// 
			this.colIBLabel.Text = "IB Label";
			// 
			// imgDialog
			// 
			this.imgDialog.ImageSize = new System.Drawing.Size(16, 16);
			this.imgDialog.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgDialog.ImageStream")));
			this.imgDialog.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tabMain
			// 
			this.tabMain.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.tabMain.Controls.Add(this.tabSort);
			this.tabMain.Controls.Add(this.tabSortedItem);
			this.tabMain.Controls.Add(this.tabTrace);
			this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tabMain.ItemSize = new System.Drawing.Size(84, 24);
			this.tabMain.Location = new System.Drawing.Point(0, 0);
			this.tabMain.Multiline = true;
			this.tabMain.Name = "tabMain";
			this.tabMain.SelectedIndex = 0;
			this.tabMain.Size = new System.Drawing.Size(576, 288);
			this.tabMain.TabIndex = 16;
			// 
			// tabSort
			// 
			this.tabSort.Controls.Add(this.btnScale);
			this.tabSort.Controls.Add(this.stbAssignments);
			this.tabSort.Controls.Add(this.btnRefreshCache);
			this.tabSort.Controls.Add(this._lblAssignments);
			this.tabSort.Controls.Add(this.lsvAssignments);
			this.tabSort.Controls.Add(this.btnRefresh);
			this.tabSort.Location = new System.Drawing.Point(4, 4);
			this.tabSort.Name = "tabSort";
			this.tabSort.Size = new System.Drawing.Size(568, 256);
			this.tabSort.TabIndex = 3;
			this.tabSort.Text = "Assignments";
			// 
			// btnScale
			// 
			this.btnScale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnScale.Location = new System.Drawing.Point(444, 237);
			this.btnScale.Name = "btnScale";
			this.btnScale.Size = new System.Drawing.Size(27, 16);
			this.btnScale.TabIndex = 20;
			this.btnScale.Text = "...";
			this.btnScale.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// stbAssignments
			// 
			this.stbAssignments.Location = new System.Drawing.Point(0, 232);
			this.stbAssignments.Name = "stbAssignments";
			this.stbAssignments.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																							  this.pnlOperator,
																							  this.pnlSortType,
																							  this.pnlLanePrefix,
																							  this.pnlShipOverride,
																							  this.pnlUPSAllowed,
																							  this.pnlScaleType,
																							  this.pnlStation});
			this.stbAssignments.ShowPanels = true;
			this.stbAssignments.Size = new System.Drawing.Size(568, 24);
			this.stbAssignments.SizingGrip = false;
			this.stbAssignments.TabIndex = 29;
			// 
			// pnlOperator
			// 
			this.pnlOperator.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.pnlOperator.ToolTipText = "Station Operator";
			this.pnlOperator.Width = 112;
			// 
			// pnlSortType
			// 
			this.pnlSortType.ToolTipText = "Sort Type (Brain)";
			this.pnlSortType.Width = 120;
			// 
			// pnlLanePrefix
			// 
			this.pnlLanePrefix.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
			this.pnlLanePrefix.ToolTipText = "Lane Prefix";
			this.pnlLanePrefix.Width = 24;
			// 
			// pnlShipOverride
			// 
			this.pnlShipOverride.ToolTipText = "Ship Override";
			this.pnlShipOverride.Width = 72;
			// 
			// pnlUPSAllowed
			// 
			this.pnlUPSAllowed.ToolTipText = "UPS Allowed";
			this.pnlUPSAllowed.Width = 48;
			// 
			// pnlScaleType
			// 
			this.pnlScaleType.ToolTipText = "Scale Type";
			this.pnlScaleType.Width = 96;
			// 
			// pnlStation
			// 
			this.pnlStation.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
			this.pnlStation.Width = 96;
			// 
			// btnRefreshCache
			// 
			this.btnRefreshCache.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRefreshCache.Enabled = false;
			this.btnRefreshCache.Location = new System.Drawing.Point(363, 12);
			this.btnRefreshCache.Name = "btnRefreshCache";
			this.btnRefreshCache.Size = new System.Drawing.Size(96, 21);
			this.btnRefreshCache.TabIndex = 17;
			this.btnRefreshCache.Text = "Refresh Cache";
			this.btnRefreshCache.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// _lblAssignments
			// 
			this._lblAssignments.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblAssignments.Location = new System.Drawing.Point(6, 12);
			this._lblAssignments.Name = "_lblAssignments";
			this._lblAssignments.Size = new System.Drawing.Size(192, 18);
			this._lblAssignments.TabIndex = 16;
			this._lblAssignments.Text = "Station-Freight Assignments";
			this._lblAssignments.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lsvAssignments
			// 
			this.lsvAssignments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lsvAssignments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							 this.colTDS,
																							 this.colClient,
																							 this.colShipper,
																							 this.colFreight,
																							 this.colSort,
																							 this.colIBLabel});
			this.lsvAssignments.FullRowSelect = true;
			this.lsvAssignments.GridLines = true;
			this.lsvAssignments.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lsvAssignments.HideSelection = false;
			this.lsvAssignments.LabelWrap = false;
			this.lsvAssignments.Location = new System.Drawing.Point(3, 36);
			this.lsvAssignments.MultiSelect = false;
			this.lsvAssignments.Name = "lsvAssignments";
			this.lsvAssignments.Size = new System.Drawing.Size(561, 195);
			this.lsvAssignments.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lsvAssignments.TabIndex = 6;
			this.lsvAssignments.View = System.Windows.Forms.View.Details;
			// 
			// btnRefresh
			// 
			this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRefresh.Enabled = false;
			this.btnRefresh.Location = new System.Drawing.Point(468, 12);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(96, 21);
			this.btnRefresh.TabIndex = 15;
			this.btnRefresh.Text = "Refresh";
			this.btnRefresh.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// tabSortedItem
			// 
			this.tabSortedItem.Controls.Add(this.label1);
			this.tabSortedItem.Controls.Add(this.grpItemsStatistics);
			this.tabSortedItem.Controls.Add(this.grpLine2);
			this.tabSortedItem.Controls.Add(this.grpLine1);
			this.tabSortedItem.Controls.Add(this.lblInput3);
			this.tabSortedItem.Controls.Add(this.txtInput3);
			this.tabSortedItem.Controls.Add(this.txtZone);
			this.tabSortedItem.Controls.Add(this.txtTLNumber);
			this.tabSortedItem.Controls.Add(this.txtDamage);
			this.tabSortedItem.Controls.Add(this.txtStoreName);
			this.tabSortedItem.Controls.Add(this.txtInput2);
			this.tabSortedItem.Controls.Add(this.txtInput1);
			this.tabSortedItem.Controls.Add(this.lblZone);
			this.tabSortedItem.Controls.Add(this.lblDamage);
			this.tabSortedItem.Controls.Add(this.lblTLNumber);
			this.tabSortedItem.Controls.Add(this.lblStoreName);
			this.tabSortedItem.Controls.Add(this.lblInput2);
			this.tabSortedItem.Controls.Add(this.lblInput1);
			this.tabSortedItem.Controls.Add(this.lblLableNumber);
			this.tabSortedItem.Controls.Add(this.txtLabelNumber);
			this.tabSortedItem.Controls.Add(this.txtWeight);
			this.tabSortedItem.Controls.Add(this.lblWeight);
			this.tabSortedItem.Location = new System.Drawing.Point(4, 4);
			this.tabSortedItem.Name = "tabSortedItem";
			this.tabSortedItem.Size = new System.Drawing.Size(568, 256);
			this.tabSortedItem.TabIndex = 4;
			this.tabSortedItem.Text = "Sorted Item";
			this.tabSortedItem.Visible = false;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(534, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(30, 16);
			this.label1.TabIndex = 61;
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// grpItemsStatistics
			// 
			this.grpItemsStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grpItemsStatistics.Controls.Add(this.txtStatisticsUnsorted);
			this.grpItemsStatistics.Controls.Add(this.lblUnsorted);
			this.grpItemsStatistics.Controls.Add(this.btnResetStats);
			this.grpItemsStatistics.Controls.Add(this.txtStatisticsItemsPerUptime);
			this.grpItemsStatistics.Controls.Add(this.txtStatisticsItemsMinMax);
			this.grpItemsStatistics.Controls.Add(this.txtStatisticsRunningTime);
			this.grpItemsStatistics.Controls.Add(this.txtStatisticsSorted);
			this.grpItemsStatistics.Controls.Add(this.txtStatisticsUpTime);
			this.grpItemsStatistics.Controls.Add(this.txtStatisticsDownTime);
			this.grpItemsStatistics.Controls.Add(this.txtStatisticsStartTime);
			this.grpItemsStatistics.Controls.Add(this.lblItemsPerUptime);
			this.grpItemsStatistics.Controls.Add(this.lblItemsMinMax);
			this.grpItemsStatistics.Controls.Add(this.lblRunningTime);
			this.grpItemsStatistics.Controls.Add(this.lblSorted);
			this.grpItemsStatistics.Controls.Add(this.lblUpTime);
			this.grpItemsStatistics.Controls.Add(this.lblDownTime);
			this.grpItemsStatistics.Controls.Add(this.lblStartTime);
			this.grpItemsStatistics.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grpItemsStatistics.ForeColor = System.Drawing.SystemColors.ControlText;
			this.grpItemsStatistics.Location = new System.Drawing.Point(374, 54);
			this.grpItemsStatistics.Name = "grpItemsStatistics";
			this.grpItemsStatistics.Size = new System.Drawing.Size(186, 192);
			this.grpItemsStatistics.TabIndex = 60;
			this.grpItemsStatistics.TabStop = false;
			this.grpItemsStatistics.Text = "Item Statistics";
			// 
			// txtStatisticsUnsorted
			// 
			this.txtStatisticsUnsorted.AutoSize = false;
			this.txtStatisticsUnsorted.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtStatisticsUnsorted.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtStatisticsUnsorted.Location = new System.Drawing.Point(108, 129);
			this.txtStatisticsUnsorted.Name = "txtStatisticsUnsorted";
			this.txtStatisticsUnsorted.ReadOnly = true;
			this.txtStatisticsUnsorted.Size = new System.Drawing.Size(72, 18);
			this.txtStatisticsUnsorted.TabIndex = 17;
			this.txtStatisticsUnsorted.Text = "";
			this.txtStatisticsUnsorted.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lblUnsorted
			// 
			this.lblUnsorted.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblUnsorted.Location = new System.Drawing.Point(6, 129);
			this.lblUnsorted.Name = "lblUnsorted";
			this.lblUnsorted.Size = new System.Drawing.Size(96, 18);
			this.lblUnsorted.TabIndex = 16;
			this.lblUnsorted.Text = "Unsorted";
			this.lblUnsorted.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnResetStats
			// 
			this.btnResetStats.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnResetStats.Location = new System.Drawing.Point(84, 24);
			this.btnResetStats.Name = "btnResetStats";
			this.btnResetStats.Size = new System.Drawing.Size(18, 18);
			this.btnResetStats.TabIndex = 15;
			this.btnResetStats.Text = "r";
			this.btnResetStats.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// txtStatisticsItemsPerUptime
			// 
			this.txtStatisticsItemsPerUptime.AutoSize = false;
			this.txtStatisticsItemsPerUptime.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtStatisticsItemsPerUptime.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtStatisticsItemsPerUptime.Location = new System.Drawing.Point(108, 171);
			this.txtStatisticsItemsPerUptime.Name = "txtStatisticsItemsPerUptime";
			this.txtStatisticsItemsPerUptime.ReadOnly = true;
			this.txtStatisticsItemsPerUptime.Size = new System.Drawing.Size(72, 18);
			this.txtStatisticsItemsPerUptime.TabIndex = 14;
			this.txtStatisticsItemsPerUptime.Text = "";
			this.txtStatisticsItemsPerUptime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtStatisticsItemsMinMax
			// 
			this.txtStatisticsItemsMinMax.AutoSize = false;
			this.txtStatisticsItemsMinMax.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtStatisticsItemsMinMax.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtStatisticsItemsMinMax.Location = new System.Drawing.Point(108, 150);
			this.txtStatisticsItemsMinMax.Name = "txtStatisticsItemsMinMax";
			this.txtStatisticsItemsMinMax.ReadOnly = true;
			this.txtStatisticsItemsMinMax.Size = new System.Drawing.Size(72, 18);
			this.txtStatisticsItemsMinMax.TabIndex = 13;
			this.txtStatisticsItemsMinMax.Text = "";
			this.txtStatisticsItemsMinMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtStatisticsRunningTime
			// 
			this.txtStatisticsRunningTime.AutoSize = false;
			this.txtStatisticsRunningTime.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtStatisticsRunningTime.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtStatisticsRunningTime.Location = new System.Drawing.Point(108, 45);
			this.txtStatisticsRunningTime.Name = "txtStatisticsRunningTime";
			this.txtStatisticsRunningTime.ReadOnly = true;
			this.txtStatisticsRunningTime.Size = new System.Drawing.Size(72, 18);
			this.txtStatisticsRunningTime.TabIndex = 12;
			this.txtStatisticsRunningTime.Text = "";
			this.txtStatisticsRunningTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtStatisticsSorted
			// 
			this.txtStatisticsSorted.AutoSize = false;
			this.txtStatisticsSorted.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtStatisticsSorted.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtStatisticsSorted.Location = new System.Drawing.Point(108, 108);
			this.txtStatisticsSorted.Name = "txtStatisticsSorted";
			this.txtStatisticsSorted.ReadOnly = true;
			this.txtStatisticsSorted.Size = new System.Drawing.Size(72, 18);
			this.txtStatisticsSorted.TabIndex = 11;
			this.txtStatisticsSorted.Text = "";
			this.txtStatisticsSorted.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtStatisticsUpTime
			// 
			this.txtStatisticsUpTime.AutoSize = false;
			this.txtStatisticsUpTime.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtStatisticsUpTime.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtStatisticsUpTime.Location = new System.Drawing.Point(108, 66);
			this.txtStatisticsUpTime.Name = "txtStatisticsUpTime";
			this.txtStatisticsUpTime.ReadOnly = true;
			this.txtStatisticsUpTime.Size = new System.Drawing.Size(72, 18);
			this.txtStatisticsUpTime.TabIndex = 10;
			this.txtStatisticsUpTime.Text = "";
			this.txtStatisticsUpTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtStatisticsDownTime
			// 
			this.txtStatisticsDownTime.AutoSize = false;
			this.txtStatisticsDownTime.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtStatisticsDownTime.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtStatisticsDownTime.Location = new System.Drawing.Point(108, 87);
			this.txtStatisticsDownTime.Name = "txtStatisticsDownTime";
			this.txtStatisticsDownTime.ReadOnly = true;
			this.txtStatisticsDownTime.Size = new System.Drawing.Size(72, 18);
			this.txtStatisticsDownTime.TabIndex = 9;
			this.txtStatisticsDownTime.Text = "";
			this.txtStatisticsDownTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtStatisticsStartTime
			// 
			this.txtStatisticsStartTime.AutoSize = false;
			this.txtStatisticsStartTime.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtStatisticsStartTime.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtStatisticsStartTime.Location = new System.Drawing.Point(108, 24);
			this.txtStatisticsStartTime.Name = "txtStatisticsStartTime";
			this.txtStatisticsStartTime.ReadOnly = true;
			this.txtStatisticsStartTime.Size = new System.Drawing.Size(72, 18);
			this.txtStatisticsStartTime.TabIndex = 8;
			this.txtStatisticsStartTime.Text = "";
			this.txtStatisticsStartTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lblItemsPerUptime
			// 
			this.lblItemsPerUptime.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblItemsPerUptime.Location = new System.Drawing.Point(6, 171);
			this.lblItemsPerUptime.Name = "lblItemsPerUptime";
			this.lblItemsPerUptime.Size = new System.Drawing.Size(96, 18);
			this.lblItemsPerUptime.TabIndex = 7;
			this.lblItemsPerUptime.Text = "Items/min (avg)";
			this.lblItemsPerUptime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblItemsMinMax
			// 
			this.lblItemsMinMax.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblItemsMinMax.Location = new System.Drawing.Point(6, 150);
			this.lblItemsMinMax.Name = "lblItemsMinMax";
			this.lblItemsMinMax.Size = new System.Drawing.Size(99, 18);
			this.lblItemsMinMax.TabIndex = 6;
			this.lblItemsMinMax.Text = "Items/min (max)";
			this.lblItemsMinMax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblRunningTime
			// 
			this.lblRunningTime.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblRunningTime.Location = new System.Drawing.Point(6, 45);
			this.lblRunningTime.Name = "lblRunningTime";
			this.lblRunningTime.Size = new System.Drawing.Size(96, 18);
			this.lblRunningTime.TabIndex = 5;
			this.lblRunningTime.Text = "Run Time (min)";
			this.lblRunningTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblSorted
			// 
			this.lblSorted.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSorted.Location = new System.Drawing.Point(6, 108);
			this.lblSorted.Name = "lblSorted";
			this.lblSorted.Size = new System.Drawing.Size(96, 18);
			this.lblSorted.TabIndex = 4;
			this.lblSorted.Text = "Sorted";
			this.lblSorted.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblUpTime
			// 
			this.lblUpTime.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblUpTime.Location = new System.Drawing.Point(6, 66);
			this.lblUpTime.Name = "lblUpTime";
			this.lblUpTime.Size = new System.Drawing.Size(96, 18);
			this.lblUpTime.TabIndex = 3;
			this.lblUpTime.Text = "Up Time (min)";
			this.lblUpTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblDownTime
			// 
			this.lblDownTime.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblDownTime.Location = new System.Drawing.Point(6, 87);
			this.lblDownTime.Name = "lblDownTime";
			this.lblDownTime.Size = new System.Drawing.Size(102, 18);
			this.lblDownTime.TabIndex = 2;
			this.lblDownTime.Tag = "";
			this.lblDownTime.Text = "Down Time (min)";
			this.lblDownTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblStartTime
			// 
			this.lblStartTime.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblStartTime.Location = new System.Drawing.Point(6, 24);
			this.lblStartTime.Name = "lblStartTime";
			this.lblStartTime.Size = new System.Drawing.Size(69, 18);
			this.lblStartTime.TabIndex = 1;
			this.lblStartTime.Text = "Start Time";
			this.lblStartTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// grpLine2
			// 
			this.grpLine2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grpLine2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grpLine2.Location = new System.Drawing.Point(-30, 159);
			this.grpLine2.Name = "grpLine2";
			this.grpLine2.Size = new System.Drawing.Size(398, 3);
			this.grpLine2.TabIndex = 59;
			this.grpLine2.TabStop = false;
			// 
			// grpLine1
			// 
			this.grpLine1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grpLine1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grpLine1.Location = new System.Drawing.Point(-30, 57);
			this.grpLine1.Name = "grpLine1";
			this.grpLine1.Size = new System.Drawing.Size(398, 3);
			this.grpLine1.TabIndex = 58;
			this.grpLine1.TabStop = false;
			// 
			// lblInput3
			// 
			this.lblInput3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblInput3.Location = new System.Drawing.Point(6, 132);
			this.lblInput3.Name = "lblInput3";
			this.lblInput3.Size = new System.Drawing.Size(120, 18);
			this.lblInput3.TabIndex = 57;
			this.lblInput3.Text = "Input 3";
			this.lblInput3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtInput3
			// 
			this.txtInput3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtInput3.AutoSize = false;
			this.txtInput3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtInput3.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtInput3.Location = new System.Drawing.Point(129, 129);
			this.txtInput3.Name = "txtInput3";
			this.txtInput3.Size = new System.Drawing.Size(236, 18);
			this.txtInput3.TabIndex = 56;
			this.txtInput3.Text = "";
			// 
			// txtZone
			// 
			this.txtZone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtZone.AutoSize = false;
			this.txtZone.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtZone.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtZone.Location = new System.Drawing.Point(297, 201);
			this.txtZone.Name = "txtZone";
			this.txtZone.ReadOnly = true;
			this.txtZone.Size = new System.Drawing.Size(68, 18);
			this.txtZone.TabIndex = 55;
			this.txtZone.Text = "";
			// 
			// txtTLNumber
			// 
			this.txtTLNumber.AutoSize = false;
			this.txtTLNumber.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtTLNumber.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtTLNumber.Location = new System.Drawing.Point(129, 201);
			this.txtTLNumber.Name = "txtTLNumber";
			this.txtTLNumber.ReadOnly = true;
			this.txtTLNumber.Size = new System.Drawing.Size(96, 18);
			this.txtTLNumber.TabIndex = 54;
			this.txtTLNumber.Text = "";
			// 
			// txtDamage
			// 
			this.txtDamage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtDamage.AutoSize = false;
			this.txtDamage.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtDamage.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtDamage.Location = new System.Drawing.Point(129, 225);
			this.txtDamage.Name = "txtDamage";
			this.txtDamage.ReadOnly = true;
			this.txtDamage.Size = new System.Drawing.Size(236, 18);
			this.txtDamage.TabIndex = 53;
			this.txtDamage.Text = "";
			// 
			// txtStoreName
			// 
			this.txtStoreName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtStoreName.AutoSize = false;
			this.txtStoreName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtStoreName.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtStoreName.Location = new System.Drawing.Point(129, 177);
			this.txtStoreName.Name = "txtStoreName";
			this.txtStoreName.ReadOnly = true;
			this.txtStoreName.Size = new System.Drawing.Size(236, 18);
			this.txtStoreName.TabIndex = 52;
			this.txtStoreName.Text = "";
			// 
			// txtInput2
			// 
			this.txtInput2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtInput2.AutoSize = false;
			this.txtInput2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtInput2.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtInput2.Location = new System.Drawing.Point(129, 105);
			this.txtInput2.Name = "txtInput2";
			this.txtInput2.Size = new System.Drawing.Size(236, 18);
			this.txtInput2.TabIndex = 51;
			this.txtInput2.Text = "";
			// 
			// txtInput1
			// 
			this.txtInput1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtInput1.AutoSize = false;
			this.txtInput1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtInput1.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtInput1.Location = new System.Drawing.Point(129, 81);
			this.txtInput1.Name = "txtInput1";
			this.txtInput1.Size = new System.Drawing.Size(236, 18);
			this.txtInput1.TabIndex = 50;
			this.txtInput1.Text = "";
			// 
			// lblZone
			// 
			this.lblZone.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblZone.Location = new System.Drawing.Point(240, 201);
			this.lblZone.Name = "lblZone";
			this.lblZone.Size = new System.Drawing.Size(48, 18);
			this.lblZone.TabIndex = 49;
			this.lblZone.Text = "Zone";
			this.lblZone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblDamage
			// 
			this.lblDamage.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblDamage.Location = new System.Drawing.Point(6, 225);
			this.lblDamage.Name = "lblDamage";
			this.lblDamage.Size = new System.Drawing.Size(120, 18);
			this.lblDamage.TabIndex = 48;
			this.lblDamage.Text = "Damage";
			this.lblDamage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblTLNumber
			// 
			this.lblTLNumber.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblTLNumber.Location = new System.Drawing.Point(6, 201);
			this.lblTLNumber.Name = "lblTLNumber";
			this.lblTLNumber.Size = new System.Drawing.Size(120, 18);
			this.lblTLNumber.TabIndex = 47;
			this.lblTLNumber.Text = "TL Number";
			this.lblTLNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblStoreName
			// 
			this.lblStoreName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblStoreName.Location = new System.Drawing.Point(6, 177);
			this.lblStoreName.Name = "lblStoreName";
			this.lblStoreName.Size = new System.Drawing.Size(120, 18);
			this.lblStoreName.TabIndex = 46;
			this.lblStoreName.Text = "Store Name";
			this.lblStoreName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblInput2
			// 
			this.lblInput2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblInput2.Location = new System.Drawing.Point(6, 108);
			this.lblInput2.Name = "lblInput2";
			this.lblInput2.Size = new System.Drawing.Size(120, 18);
			this.lblInput2.TabIndex = 45;
			this.lblInput2.Text = "Input 2";
			this.lblInput2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblInput1
			// 
			this.lblInput1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblInput1.Location = new System.Drawing.Point(6, 84);
			this.lblInput1.Name = "lblInput1";
			this.lblInput1.Size = new System.Drawing.Size(120, 18);
			this.lblInput1.TabIndex = 44;
			this.lblInput1.Text = "Input 1";
			this.lblInput1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblLableNumber
			// 
			this.lblLableNumber.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblLableNumber.Location = new System.Drawing.Point(6, 24);
			this.lblLableNumber.Name = "lblLableNumber";
			this.lblLableNumber.Size = new System.Drawing.Size(120, 18);
			this.lblLableNumber.TabIndex = 43;
			this.lblLableNumber.Text = "Label Number";
			this.lblLableNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtLabelNumber
			// 
			this.txtLabelNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtLabelNumber.AutoSize = false;
			this.txtLabelNumber.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtLabelNumber.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtLabelNumber.Location = new System.Drawing.Point(129, 24);
			this.txtLabelNumber.Name = "txtLabelNumber";
			this.txtLabelNumber.ReadOnly = true;
			this.txtLabelNumber.Size = new System.Drawing.Size(236, 18);
			this.txtLabelNumber.TabIndex = 42;
			this.txtLabelNumber.Text = "";
			// 
			// txtWeight
			// 
			this.txtWeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtWeight.AutoSize = false;
			this.txtWeight.BackColor = System.Drawing.SystemColors.Highlight;
			this.txtWeight.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtWeight.ForeColor = System.Drawing.SystemColors.HighlightText;
			this.txtWeight.Location = new System.Drawing.Point(458, 18);
			this.txtWeight.MaxLength = 3;
			this.txtWeight.Name = "txtWeight";
			this.txtWeight.Size = new System.Drawing.Size(72, 24);
			this.txtWeight.TabIndex = 2;
			this.txtWeight.Text = "";
			this.txtWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// lblWeight
			// 
			this.lblWeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblWeight.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblWeight.Location = new System.Drawing.Point(380, 24);
			this.lblWeight.Name = "lblWeight";
			this.lblWeight.Size = new System.Drawing.Size(72, 18);
			this.lblWeight.TabIndex = 1;
			this.lblWeight.Text = "Weight";
			this.lblWeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tabTrace
			// 
			this.tabTrace.Controls.Add(this.txtTrace);
			this.tabTrace.Location = new System.Drawing.Point(4, 4);
			this.tabTrace.Name = "tabTrace";
			this.tabTrace.Size = new System.Drawing.Size(568, 256);
			this.tabTrace.TabIndex = 0;
			this.tabTrace.Text = "Trace";
			this.tabTrace.Visible = false;
			// 
			// txtTrace
			// 
			this.txtTrace.AcceptsTab = true;
			this.txtTrace.ContextMenu = this.ctxMain;
			this.txtTrace.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtTrace.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtTrace.Location = new System.Drawing.Point(0, 0);
			this.txtTrace.Name = "txtTrace";
			this.txtTrace.ReadOnly = true;
			this.txtTrace.Size = new System.Drawing.Size(568, 256);
			this.txtTrace.TabIndex = 0;
			this.txtTrace.Text = "";
			this.txtTrace.WordWrap = false;
			// 
			// SortUI
			// 
			this.Controls.Add(this.tabMain);
			this.Name = "SortUI";
			this.Size = new System.Drawing.Size(576, 288);
			this.Resize += new System.EventHandler(this.OnControlResize);
			this.Load += new System.EventHandler(this.OnControlLoad);
			this.tabMain.ResumeLayout(false);
			this.tabSort.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pnlOperator)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlSortType)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlLanePrefix)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlShipOverride)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlUPSAllowed)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlScaleType)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlStation)).EndInit();
			this.tabSortedItem.ResumeLayout(false);
			this.grpItemsStatistics.ResumeLayout(false);
			this.tabTrace.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		private void OnControlLoad(object sender, System.EventArgs e) {
			//Event handler for control load event
			try { 
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;
				this.mToolTip.SetToolTip(this.btnRefresh, "Refresh station freight assignments");
				this.mToolTip.SetToolTip(this.btnRefreshCache, "Refresh cached items including default SAN label and special agents");
				this.mToolTip.SetToolTip(this.btnResetStats, "Reset sorted item statistics");
				this.mToolTip.SetToolTip(this.btnScale, "Change station scale from startup default scale");

				this.tabMain.SelectedIndex = 0;
				this.txtTrace.MaxLength = 1024000;
				this.pnlLanePrefix.Text = SortedItem.LanePrefix;
				this.pnlShipOverride.Text = Brain.ShipOverride;
				this.pnlUPSAllowed.Text = Brain.UPSAllowed.ToString();
				OnCmdClick(this.btnResetStats, EventArgs.Empty);
			} 
			catch(Exception ex) { reportError(ex); } 
			finally { setServices(); }
		}
		private void OnControlResize(object sender, System.EventArgs e) { }
		private void OnAssignmentsChanged(object sender, EventArgs e) {
			//Event handler for change in station assignments
			try {
				this.pnlOperator.Text = this.pnlStation.Text = this.pnlScaleType.Text = this.pnlSortType.Text = "";
				this.lsvAssignments.Items.Clear();
				this.pnlOperator.Text = this.mStationOperator.Name;
				this.pnlStation.Text = this.mStationOperator.Station.Name;
				this.pnlStation.ToolTipText = "Station# " + this.mStationOperator.Station.Number;
				this.pnlScaleType.Text = this.mStationOperator.ScaleType;
				this.pnlSortType.Text = this.mStationOperator.BrainName;
				for(int i=0; i<this.mStationOperator.Assignments.Count; i++) {
					string freight = this.mStationOperator.Assignments.Item(i).InboundFreight.FreightType;
					string sort = this.mStationOperator.Assignments.Item(i).SortProfile.SortType;
					string tds = this.mStationOperator.Assignments.Item(i).InboundFreight.TDSNumber.ToString();
					string client = this.mStationOperator.Assignments.Item(i).InboundFreight.Client.Number + "-" + this.mStationOperator.Assignments.Item(i).InboundFreight.Client.Name.Trim();
					string shipper = this.mStationOperator.Assignments.Item(i).InboundFreight.Shipper.NUMBER + "-" + this.mStationOperator.Assignments.Item(i).InboundFreight.Shipper.NAME.Trim();
					string label = this.mStationOperator.Assignments.Item(i).SortProfile.InboundLabel.LabelID.ToString();
					this.lsvAssignments.Items.Add(new ListViewItem(new string[]{tds,client,shipper,freight,sort,label}));
				}
			}
			catch(Exception ex) { reportError(ex); }
			finally { setServices(); }
		}
		private void OnSortedItemCreated(object sender, SortedItemEventArgs e) {
			//Event handler for sorted item created
			try {
				if(this.mStationOperator.Assignments.Count == 0) {
					//Clear
					this.txtInput1.Text = this.txtInput2.Text = this.txtInput3.Text = "";
					this.txtLabelNumber.Text = this.txtStoreName.Text = "";
					this.txtTLNumber.Text = this.txtZone.Text = "";
					this.txtDamage.Text = this.txtWeight.Text = "";
					this.label1.Text = "";
				}
				else {
					//Capture sorted item details
					SortedItem item = e.SortedItem;
					if(item != null) {
						if(item.InboundLabel != null) {
							InboundLabel label = item.InboundLabel;
							this.txtInput1.Text = label.Inputs[0].InputData;
							if(label.Inputs[1] != null) this.txtInput2.Text = label.Inputs[1].InputData;
							if(label.Inputs[2] != null) this.txtInput3.Text = label.Inputs[2].InputData;
						}
					
						this.txtLabelNumber.Text = item.LabelNumber;
						this.txtStoreName.Text = item.DestinationRouting != null ? item.DestinationRouting.DestinationName : "";
						this.txtTLNumber.Text = item.DestinationRouting != null ? item.DestinationRouting.ZoneTL : "";
						this.txtZone.Text = item.DestinationRouting != null ? item.DestinationRouting.ZoneCode : "";
						this.txtDamage.Text = item.DamageCode;
						this.txtWeight.Text = item.Weight.ToString();
					}
					this.label1.Text = this.mStationOperator.SortedItemCount.ToString();
				}
			}
			catch(Exception ex) { reportError(ex); }
			finally { setServices(); }
		}
		private void OnSortedItemComplete(object sender, SortedItemEventArgs e) {
			//Event handler for sorted item completed processing
			try {
				if(this.mStationOperator.Assignments.Count == 0) {
					//Clear
					this.txtStatisticsStartTime.Text = this.txtStatisticsRunningTime.Text = this.txtStatisticsUpTime.Text = this.txtStatisticsDownTime.Text = "";
					this.txtStatisticsSorted.Text = this.txtStatisticsItemsMinMax.Text = this.txtStatisticsItemsPerUptime.Text = "";
				}
				else {				
					//Update statistics display controls
					this.txtStatisticsStartTime.Text = this.mStationOperator.Station.SortStatistics.StartTime.ToShortTimeString();
					this.txtStatisticsRunningTime.Text = this.mStationOperator.Station.SortStatistics.RunningTimeMinutes.ToString();
					this.txtStatisticsUpTime.Text = this.mStationOperator.Station.SortStatistics.UpTimeMinutes.ToString();
					this.txtStatisticsDownTime.Text = this.mStationOperator.Station.SortStatistics.DownTimeMinutes.ToString();
					this.txtStatisticsSorted.Text = this.mStationOperator.Station.SortStatistics.CartonsSorted.ToString();
					this.txtStatisticsUnsorted.Text = this.mStationOperator.Station.SortStatistics.CartonsUnsorted.ToString();
					this.txtStatisticsItemsMinMax.Text = this.mStationOperator.Station.SortStatistics.ItemsPerLastMinute.ToString();
					this.txtStatisticsItemsPerUptime.Text = this.mStationOperator.Station.SortStatistics.ItemsPerMinuteUptime.ToString();
				}
				
				//Manage trace textbox
				if(this.txtTrace.Text.Length > this.txtTrace.MaxLength) this.txtTrace.Clear();
			}
			catch(Exception ex) { reportError(ex); }
			finally { setServices(); }
		}
		#region User services: OnCmdClick(), OnMenuClick(), OnMenuPopup()
		private void OnCmdClick(object sender, System.EventArgs e) {
			//Event handler for command button clicked
			try {
				Button btn = (Button)sender;
				switch(btn.Text) {
					case CMD_REFRESH:		this.mStationOperator.RefreshStationAssignments(); break;
					case CMD_REFRESHCACHE:	this.mStationOperator.RefreshCache(); break;
					case "...":				this.mStationOperator.ShowStationSetup(); OnAssignmentsChanged(null, EventArgs.Empty); break;
					case "r":				this.mStationOperator.ResetStatistics(); break;
				}
			}
			catch (Exception ex) { reportError(ex); }
		}
		private void OnMenuClick(object sender, System.EventArgs e) {
			//Event hanlder for menu click events
			try {
				MenuItem mnu = (MenuItem)sender;
				switch(mnu.Text) {
					case "Refresh":	
						break;
					case "Clear":	
						if(this.txtTrace.Focus()) 
							this.txtTrace.Clear();
						break;
				}
			}
			catch (Exception ex) { reportError(ex); }
		}
		private void OnMenuPopup(object sender, System.EventArgs e) {
			//Event handler for menu popup event
			try {
				this.ctxRefresh.Enabled = false;
				this.ctxClear.Enabled = (this.txtTrace.Focus());
			}
			catch (Exception ex) { reportError(ex); }
		}
		#endregion
		#region Local services: setServices(), reportError()
		private void setServices() {
			//Set user services states
			try {
				this.btnRefresh.Enabled = (this.mStationOperator != null && this.mStationOperator.Working);
				this.btnRefreshCache.Enabled = (this.mStationOperator != null && this.mStationOperator.Working);
				this.btnScale.Enabled = (this.mStationOperator != null);
			}
			catch(Exception ex) { reportError(ex); }
		}
		private void reportError(Exception ex) {
			//Report an exception to the user
			ArgixTrace.WriteLine(new TraceMessage(ex.ToString(), AppLib.EVENTLOGNAME, LogLevel.Error, "SortLib"));
		}
		#endregion
	}
}
