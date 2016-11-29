//	File:	dlgconsigneeoverrides.cs
//	Author:	J. Heary
//	Date:	05/01/06
//	Desc:	Dialog to create a new or edit an existing Freight Path.
//	Rev:	
//	
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
	public class dlgConsigneeOverride : System.Windows.Forms.Form { 
		//Members
		private int mLocationID=0;
		private int mIndex=0;
		#region Controls

		private Tsort.Windows.SelectionList mClientsDS;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.GroupBox fraOverrides;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdConsigneeOverrides;
		private System.Windows.Forms.GroupBox fraMappings;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdMappings;
		private System.Windows.Forms.GroupBox fraLocations;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdLocations;
		private System.Windows.Forms.CheckBox chkOverridesOnly;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.ComboBox cboClients;
		private System.Windows.Forms.Label _lblClient;
		private System.Windows.Forms.RadioButton rdoStore;
		private System.Windows.Forms.RadioButton rdoVendor;
		private Tsort.Enterprise.DeliveryLocationDS mLocationsViewDS;
		private Tsort.Enterprise.DeliveryLocationDS mMappingsViewDS;
		private Tsort.Enterprise.DeliveryLocationDS mOverrideDetailDS;
		private Tsort.Enterprise.DeliveryLocationDS mOverridesDS;
		private Infragistics.Win.UltraWinEditors.UltraComboEditor cboPath;
		private Infragistics.Win.UltraWinEditors.UltraComboEditor cboSvc;
		private Tsort.Windows.SelectionList mPathsDS;
		private Tsort.Windows.SelectionList mSvcsDS;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		
		//Constants
		private const string CMD_APPLY = "&Apply";
		private const string CMD_CLOSE = "&Close";
		
		//Events
		public event ErrorEventHandler ErrorMessage=null;
		
		//Interface
		public dlgConsigneeOverride() {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();
				this.btnApply.Text = CMD_APPLY;
				this.btnClose.Text = CMD_CLOSE;
				this.rdoStore.Tag = 1;
				this.rdoVendor.Tag = 3;
			} 
			catch(Exception ex) { throw ex; }
		}
		protected override void Dispose( bool disposing ) { if( disposing ) { if (components != null)  components.Dispose(); } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("DeliveryLocationOverrideDetailTable", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Selected");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenterID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenter");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LocationID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Number");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Address");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OldPathMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OldServiceMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PathID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PathMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PathLastStopMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ServiceID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ServiceMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RowVersion");
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("DeliveryLocationMapTable", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MapID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenter");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsActive");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CountryID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Country");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PostalCode");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StateOrProvince");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PathID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PathMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PathLastStopMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ServiceID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ServiceMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RowVersion");
			Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("DeliveryLocationOverrideViewTable", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LocationID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Number");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LocationType");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn41 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("VendorName");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Address");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn43 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("HasOverride");
			Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgConsigneeOverride));
			this.btnClose = new System.Windows.Forms.Button();
			this.fraOverrides = new System.Windows.Forms.GroupBox();
			this.grdConsigneeOverrides = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.cboPath = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.cboSvc = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.fraMappings = new System.Windows.Forms.GroupBox();
			this.grdMappings = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.fraLocations = new System.Windows.Forms.GroupBox();
			this.grdLocations = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.chkOverridesOnly = new System.Windows.Forms.CheckBox();
			this.rdoStore = new System.Windows.Forms.RadioButton();
			this.rdoVendor = new System.Windows.Forms.RadioButton();
			this.btnApply = new System.Windows.Forms.Button();
			this.cboClients = new System.Windows.Forms.ComboBox();
			this._lblClient = new System.Windows.Forms.Label();
			this.fraOverrides.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdConsigneeOverrides)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboPath)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboSvc)).BeginInit();
			this.fraMappings.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdMappings)).BeginInit();
			this.fraLocations.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdLocations)).BeginInit();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.BackColor = System.Drawing.SystemColors.Control;
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(662, 597);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(96, 24);
			this.btnClose.TabIndex = 8;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// fraOverrides
			// 
			this.fraOverrides.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.fraOverrides.Controls.Add(this.grdConsigneeOverrides);
			this.fraOverrides.Controls.Add(this.cboPath);
			this.fraOverrides.Controls.Add(this.cboSvc);
			this.fraOverrides.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.fraOverrides.Location = new System.Drawing.Point(3, 420);
			this.fraOverrides.Name = "fraOverrides";
			this.fraOverrides.Size = new System.Drawing.Size(755, 168);
			this.fraOverrides.TabIndex = 6;
			this.fraOverrides.TabStop = false;
			this.fraOverrides.Text = "Consignee DeliveryLocationDS";
			// 
			// grdConsigneeOverrides
			// 
			this.grdConsigneeOverrides.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grdConsigneeOverrides.DataMember = "DeliveryLocationOverrideDetailTable";
			this.grdConsigneeOverrides.DisplayLayout.AddNewBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			appearance1.BackColor = System.Drawing.Color.White;
			appearance1.FontData.Name = "Arial";
			appearance1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.grdConsigneeOverrides.DisplayLayout.Appearance = appearance1;
			ultraGridBand1.AddButtonCaption = "FreightAssignmentListForStationTable";
			ultraGridColumn1.Header.Caption = "Select";
			ultraGridColumn1.Header.VisiblePosition = 1;
			ultraGridColumn1.Width = 48;
			ultraGridColumn2.Header.VisiblePosition = 0;
			ultraGridColumn2.Hidden = true;
			ultraGridColumn3.Header.Caption = "Sort Center";
			ultraGridColumn3.Header.VisiblePosition = 3;
			ultraGridColumn3.Width = 144;
			ultraGridColumn4.Header.VisiblePosition = 2;
			ultraGridColumn4.Hidden = true;
			ultraGridColumn5.Header.Caption = "Client";
			ultraGridColumn5.Header.VisiblePosition = 5;
			ultraGridColumn5.Hidden = true;
			ultraGridColumn5.Width = 144;
			ultraGridColumn6.Header.VisiblePosition = 4;
			ultraGridColumn6.Hidden = true;
			ultraGridColumn7.Header.Caption = "Client #";
			ultraGridColumn7.Header.VisiblePosition = 6;
			ultraGridColumn7.Hidden = true;
			ultraGridColumn7.Width = 60;
			ultraGridColumn8.Header.VisiblePosition = 7;
			ultraGridColumn8.Hidden = true;
			ultraGridColumn8.Width = 96;
			ultraGridColumn9.Header.VisiblePosition = 8;
			ultraGridColumn9.Hidden = true;
			ultraGridColumn9.Width = 196;
			ultraGridColumn10.Header.Caption = "Old Path";
			ultraGridColumn10.Header.VisiblePosition = 9;
			ultraGridColumn10.Width = 113;
			ultraGridColumn11.Header.Caption = "Old Service";
			ultraGridColumn11.Header.VisiblePosition = 13;
			ultraGridColumn11.Width = 105;
			ultraGridColumn12.Header.Caption = "New Path";
			ultraGridColumn12.Header.VisiblePosition = 10;
			ultraGridColumn12.Width = 117;
			ultraGridColumn13.Header.VisiblePosition = 11;
			ultraGridColumn13.Hidden = true;
			ultraGridColumn14.Header.VisiblePosition = 12;
			ultraGridColumn14.Hidden = true;
			ultraGridColumn15.Header.Caption = "New Service";
			ultraGridColumn15.Header.VisiblePosition = 14;
			ultraGridColumn15.Width = 118;
			ultraGridColumn16.Header.VisiblePosition = 15;
			ultraGridColumn16.Hidden = true;
			ultraGridColumn17.Header.VisiblePosition = 16;
			ultraGridColumn17.Hidden = true;
			ultraGridColumn18.Header.VisiblePosition = 18;
			ultraGridColumn18.Hidden = true;
			ultraGridColumn19.Header.VisiblePosition = 17;
			ultraGridColumn19.Hidden = true;
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
															 ultraGridColumn19});
			appearance2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(220)), ((System.Byte)(255)), ((System.Byte)(200)));
			appearance2.FontData.Name = "Verdana";
			appearance2.FontData.SizeInPoints = 8F;
			appearance2.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridBand1.Override.ActiveRowAppearance = appearance2;
			appearance3.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
			appearance3.FontData.BoldAsString = "True";
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
			this.grdConsigneeOverrides.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
			appearance6.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(128)), ((System.Byte)(255)));
			appearance6.FontData.Name = "Verdana";
			appearance6.FontData.SizeInPoints = 8F;
			appearance6.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance6.TextHAlign = Infragistics.Win.HAlign.Left;
			this.grdConsigneeOverrides.DisplayLayout.CaptionAppearance = appearance6;
			this.grdConsigneeOverrides.DisplayLayout.GroupByBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			this.grdConsigneeOverrides.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			this.grdConsigneeOverrides.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
			this.grdConsigneeOverrides.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
			this.grdConsigneeOverrides.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None;
			this.grdConsigneeOverrides.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.None;
			this.grdConsigneeOverrides.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.Edit;
			this.grdConsigneeOverrides.DisplayLayout.Override.CellMultiLine = Infragistics.Win.DefaultableBoolean.False;
			this.grdConsigneeOverrides.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
			this.grdConsigneeOverrides.DisplayLayout.Override.MaxSelectedCells = 1;
			this.grdConsigneeOverrides.DisplayLayout.Override.MaxSelectedRows = 1;
			this.grdConsigneeOverrides.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			this.grdConsigneeOverrides.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
			this.grdConsigneeOverrides.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
			this.grdConsigneeOverrides.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grdConsigneeOverrides.Location = new System.Drawing.Point(8, 21);
			this.grdConsigneeOverrides.Name = "grdConsigneeOverrides";
			this.grdConsigneeOverrides.Size = new System.Drawing.Size(742, 139);
			this.grdConsigneeOverrides.TabIndex = 0;
			this.grdConsigneeOverrides.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnCellChange;
			this.grdConsigneeOverrides.AfterRowUpdate += new Infragistics.Win.UltraWinGrid.RowEventHandler(this.OnOverrideRowUpdated);
			this.grdConsigneeOverrides.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.OnOverrideCellUpdated);
			this.grdConsigneeOverrides.BeforeCellActivate += new Infragistics.Win.UltraWinGrid.CancelableCellEventHandler(this.OnOverrideCellActivated);
			// 
			// cboPath
			// 
			this.cboPath.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboPath.Location = new System.Drawing.Point(560, 16);
			this.cboPath.Name = "cboPath";
			this.cboPath.Size = new System.Drawing.Size(72, 22);
			this.cboPath.TabIndex = 1;
			this.cboPath.Visible = false;
			// 
			// cboSvc
			// 
			this.cboSvc.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboSvc.Location = new System.Drawing.Point(648, 16);
			this.cboSvc.Name = "cboSvc";
			this.cboSvc.Size = new System.Drawing.Size(72, 22);
			this.cboSvc.TabIndex = 2;
			this.cboSvc.Visible = false;
			// 
			// fraMappings
			// 
			this.fraMappings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.fraMappings.Controls.Add(this.grdMappings);
			this.fraMappings.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.fraMappings.Location = new System.Drawing.Point(3, 270);
			this.fraMappings.Name = "fraMappings";
			this.fraMappings.Size = new System.Drawing.Size(755, 144);
			this.fraMappings.TabIndex = 5;
			this.fraMappings.TabStop = false;
			this.fraMappings.Text = "Mappings";
			// 
			// grdMappings
			// 
			this.grdMappings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grdMappings.DisplayLayout.AddNewBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			appearance7.BackColor = System.Drawing.Color.White;
			appearance7.FontData.Name = "Arial";
			appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
			this.grdMappings.DisplayLayout.Appearance = appearance7;
			ultraGridBand2.AddButtonCaption = "FreightAssignmentListForStationTable";
			ultraGridColumn20.Header.VisiblePosition = 11;
			ultraGridColumn20.Hidden = true;
			ultraGridColumn21.Header.Caption = "Sort Center";
			ultraGridColumn21.Header.VisiblePosition = 0;
			ultraGridColumn21.Width = 144;
			ultraGridColumn22.Header.Caption = "Client";
			ultraGridColumn22.Header.VisiblePosition = 4;
			ultraGridColumn22.Hidden = true;
			ultraGridColumn22.Width = 144;
			ultraGridColumn23.Header.VisiblePosition = 2;
			ultraGridColumn23.Width = 157;
			ultraGridColumn24.Header.VisiblePosition = 6;
			ultraGridColumn25.Header.VisiblePosition = 5;
			ultraGridColumn25.Hidden = true;
			ultraGridColumn26.Header.VisiblePosition = 8;
			ultraGridColumn26.Hidden = true;
			ultraGridColumn26.Width = 60;
			ultraGridColumn27.Header.VisiblePosition = 7;
			ultraGridColumn27.Hidden = true;
			ultraGridColumn27.Width = 78;
			ultraGridColumn28.Header.Caption = "State";
			ultraGridColumn28.Header.VisiblePosition = 9;
			ultraGridColumn28.Hidden = true;
			ultraGridColumn28.Width = 60;
			ultraGridColumn29.Header.VisiblePosition = 10;
			ultraGridColumn29.Hidden = true;
			ultraGridColumn30.Header.Caption = "Path";
			ultraGridColumn30.Header.VisiblePosition = 1;
			ultraGridColumn30.Width = 102;
			ultraGridColumn31.Header.VisiblePosition = 13;
			ultraGridColumn31.Hidden = true;
			ultraGridColumn32.Header.VisiblePosition = 12;
			ultraGridColumn32.Hidden = true;
			ultraGridColumn33.Header.Caption = "Service";
			ultraGridColumn33.Header.VisiblePosition = 3;
			ultraGridColumn33.Width = 165;
			ultraGridColumn34.Header.VisiblePosition = 14;
			ultraGridColumn34.Hidden = true;
			ultraGridColumn35.Header.VisiblePosition = 15;
			ultraGridColumn35.Hidden = true;
			ultraGridColumn36.Header.VisiblePosition = 16;
			ultraGridColumn36.Hidden = true;
			ultraGridBand2.Columns.AddRange(new object[] {
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
															 ultraGridColumn36});
			appearance8.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(220)), ((System.Byte)(255)), ((System.Byte)(200)));
			appearance8.FontData.Name = "Verdana";
			appearance8.FontData.SizeInPoints = 8F;
			appearance8.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridBand2.Override.ActiveRowAppearance = appearance8;
			appearance9.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
			appearance9.FontData.BoldAsString = "True";
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
			this.grdMappings.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
			appearance12.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(128)), ((System.Byte)(255)));
			appearance12.FontData.Name = "Verdana";
			appearance12.FontData.SizeInPoints = 8F;
			appearance12.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance12.TextHAlign = Infragistics.Win.HAlign.Left;
			this.grdMappings.DisplayLayout.CaptionAppearance = appearance12;
			this.grdMappings.DisplayLayout.GroupByBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			this.grdMappings.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			this.grdMappings.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
			this.grdMappings.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
			this.grdMappings.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None;
			this.grdMappings.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.None;
			this.grdMappings.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
			this.grdMappings.DisplayLayout.Override.CellMultiLine = Infragistics.Win.DefaultableBoolean.False;
			this.grdMappings.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
			this.grdMappings.DisplayLayout.Override.MaxSelectedCells = 1;
			this.grdMappings.DisplayLayout.Override.MaxSelectedRows = 1;
			this.grdMappings.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			this.grdMappings.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
			this.grdMappings.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
			this.grdMappings.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grdMappings.Location = new System.Drawing.Point(8, 24);
			this.grdMappings.Name = "grdMappings";
			this.grdMappings.Size = new System.Drawing.Size(742, 112);
			this.grdMappings.TabIndex = 0;
			this.grdMappings.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnRowChange;
			// 
			// fraLocations
			// 
			this.fraLocations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.fraLocations.Controls.Add(this.grdLocations);
			this.fraLocations.Controls.Add(this.chkOverridesOnly);
			this.fraLocations.Controls.Add(this.rdoStore);
			this.fraLocations.Controls.Add(this.rdoVendor);
			this.fraLocations.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.fraLocations.Location = new System.Drawing.Point(3, 36);
			this.fraLocations.Name = "fraLocations";
			this.fraLocations.Size = new System.Drawing.Size(753, 228);
			this.fraLocations.TabIndex = 4;
			this.fraLocations.TabStop = false;
			this.fraLocations.Text = "Locations";
			// 
			// grdLocations
			// 
			this.grdLocations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grdLocations.DisplayLayout.AddNewBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			appearance13.BackColor = System.Drawing.Color.White;
			appearance13.FontData.Name = "Arial";
			appearance13.ForeColor = System.Drawing.SystemColors.ControlText;
			this.grdLocations.DisplayLayout.Appearance = appearance13;
			ultraGridBand3.AddButtonCaption = "FreightAssignmentListForStationTable";
			ultraGridColumn37.Header.Caption = "Location ID";
			ultraGridColumn37.Header.VisiblePosition = 0;
			ultraGridColumn37.Hidden = true;
			ultraGridColumn38.Header.VisiblePosition = 1;
			ultraGridColumn38.Width = 86;
			ultraGridColumn39.Header.VisiblePosition = 2;
			ultraGridColumn39.Width = 128;
			ultraGridColumn40.Header.Caption = "Location Type";
			ultraGridColumn40.Header.VisiblePosition = 3;
			ultraGridColumn41.Header.Caption = "Vendor Name";
			ultraGridColumn41.Header.VisiblePosition = 4;
			ultraGridColumn41.Hidden = true;
			ultraGridColumn42.Header.VisiblePosition = 5;
			ultraGridColumn42.Width = 318;
			ultraGridColumn43.Header.Caption = "Has Override";
			ultraGridColumn43.Header.VisiblePosition = 6;
			ultraGridColumn43.Width = 99;
			ultraGridBand3.Columns.AddRange(new object[] {
															 ultraGridColumn37,
															 ultraGridColumn38,
															 ultraGridColumn39,
															 ultraGridColumn40,
															 ultraGridColumn41,
															 ultraGridColumn42,
															 ultraGridColumn43});
			appearance14.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(220)), ((System.Byte)(255)), ((System.Byte)(200)));
			appearance14.FontData.Name = "Verdana";
			appearance14.FontData.SizeInPoints = 8F;
			appearance14.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridBand3.Override.ActiveRowAppearance = appearance14;
			appearance15.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
			appearance15.FontData.BoldAsString = "True";
			appearance15.FontData.Name = "Verdana";
			appearance15.FontData.SizeInPoints = 8F;
			appearance15.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance15.TextHAlign = Infragistics.Win.HAlign.Left;
			ultraGridBand3.Override.HeaderAppearance = appearance15;
			appearance16.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(240)), ((System.Byte)(240)), ((System.Byte)(255)));
			appearance16.FontData.Name = "Verdana";
			appearance16.FontData.SizeInPoints = 8F;
			appearance16.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridBand3.Override.RowAlternateAppearance = appearance16;
			appearance17.BackColor = System.Drawing.Color.White;
			appearance17.FontData.Name = "Verdana";
			appearance17.FontData.SizeInPoints = 8F;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance17.TextHAlign = Infragistics.Win.HAlign.Left;
			ultraGridBand3.Override.RowAppearance = appearance17;
			this.grdLocations.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
			appearance18.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(128)), ((System.Byte)(255)));
			appearance18.FontData.Name = "Verdana";
			appearance18.FontData.SizeInPoints = 8F;
			appearance18.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance18.TextHAlign = Infragistics.Win.HAlign.Left;
			this.grdLocations.DisplayLayout.CaptionAppearance = appearance18;
			this.grdLocations.DisplayLayout.GroupByBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			this.grdLocations.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			this.grdLocations.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
			this.grdLocations.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
			this.grdLocations.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None;
			this.grdLocations.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.None;
			this.grdLocations.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
			this.grdLocations.DisplayLayout.Override.CellMultiLine = Infragistics.Win.DefaultableBoolean.False;
			this.grdLocations.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
			this.grdLocations.DisplayLayout.Override.MaxSelectedCells = 1;
			this.grdLocations.DisplayLayout.Override.MaxSelectedRows = 1;
			this.grdLocations.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			this.grdLocations.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
			this.grdLocations.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
			this.grdLocations.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grdLocations.Location = new System.Drawing.Point(8, 40);
			this.grdLocations.Name = "grdLocations";
			this.grdLocations.Size = new System.Drawing.Size(740, 181);
			this.grdLocations.TabIndex = 1;
			this.grdLocations.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnRowChange;
			this.grdLocations.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnLocationChanged);
			// 
			// chkOverridesOnly
			// 
			this.chkOverridesOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkOverridesOnly.Checked = true;
			this.chkOverridesOnly.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkOverridesOnly.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkOverridesOnly.Location = new System.Drawing.Point(628, 16);
			this.chkOverridesOnly.Name = "chkOverridesOnly";
			this.chkOverridesOnly.Size = new System.Drawing.Size(120, 18);
			this.chkOverridesOnly.TabIndex = 0;
			this.chkOverridesOnly.Text = "DeliveryLocationDS Only";
			this.chkOverridesOnly.CheckedChanged += new System.EventHandler(this.OnApplyOverridesOnly);
			// 
			// rdoStore
			// 
			this.rdoStore.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rdoStore.Location = new System.Drawing.Point(56, 16);
			this.rdoStore.Name = "rdoStore";
			this.rdoStore.Size = new System.Drawing.Size(72, 16);
			this.rdoStore.TabIndex = 2;
			this.rdoStore.Tag = "1";
			this.rdoStore.Text = "Store";
			this.rdoStore.CheckedChanged += new System.EventHandler(this.OnLocationTypeChanged);
			// 
			// rdoVendor
			// 
			this.rdoVendor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rdoVendor.Location = new System.Drawing.Point(144, 16);
			this.rdoVendor.Name = "rdoVendor";
			this.rdoVendor.Size = new System.Drawing.Size(72, 16);
			this.rdoVendor.TabIndex = 3;
			this.rdoVendor.Tag = "3";
			this.rdoVendor.Text = "Vendor";
			this.rdoVendor.CheckedChanged += new System.EventHandler(this.OnLocationTypeChanged);
			// 
			// btnApply
			// 
			this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnApply.BackColor = System.Drawing.SystemColors.Control;
			this.btnApply.Enabled = false;
			this.btnApply.Location = new System.Drawing.Point(561, 597);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(96, 24);
			this.btnApply.TabIndex = 7;
			this.btnApply.Text = "&Apply";
			this.btnApply.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// cboClients
			// 
			this.cboClients.DisplayMember = "SelectionListTable.Description";
			this.cboClients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboClients.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboClients.Location = new System.Drawing.Point(57, 9);
			this.cboClients.Name = "cboClients";
			this.cboClients.Size = new System.Drawing.Size(240, 21);
			this.cboClients.TabIndex = 1;
			this.cboClients.ValueMember = "SelectionListTable.ID";
			this.cboClients.SelectedIndexChanged += new System.EventHandler(this.OnClientChanged);
			// 
			// _lblClient
			// 
			this._lblClient.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblClient.Location = new System.Drawing.Point(3, 8);
			this._lblClient.Name = "_lblClient";
			this._lblClient.Size = new System.Drawing.Size(48, 18);
			this._lblClient.TabIndex = 0;
			this._lblClient.Text = "Client";
			this._lblClient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dlgConsigneeOverride
			// 
			this.AcceptButton = this.btnApply;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(760, 645);
			this.Controls.Add(this.fraOverrides);
			this.Controls.Add(this.fraMappings);
			this.Controls.Add(this.fraLocations);
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this.cboClients);
			this.Controls.Add(this._lblClient);
			this.Controls.Add(this.btnClose);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(752, 632);
			this.Name = "dlgConsigneeOverride";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Consignee DeliveryLocationDS";
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.DoubleClick += new System.EventHandler(this.AutoSelectRow);
			this.fraOverrides.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdConsigneeOverrides)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboPath)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboSvc)).EndInit();
			this.fraMappings.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdMappings)).EndInit();
			this.fraLocations.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdLocations)).EndInit();
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
				this.mClientsDS.Merge(EnterpriseFactory.GetClients());
				
				//Set control services
				#region Default grid behavior - edit/noedit, etc...
				this.grdLocations.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
				this.grdMappings.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
				this.grdConsigneeOverrides.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
				this.grdConsigneeOverrides.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.True;
				this.grdConsigneeOverrides.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.Edit;
				this.grdConsigneeOverrides.DisplayLayout.Bands[0].Columns["Selected"].CellActivation = Activation.AllowEdit;
				this.grdConsigneeOverrides.DisplayLayout.Bands[0].Columns["SortCenter"].CellActivation = Activation.NoEdit;
				this.grdConsigneeOverrides.DisplayLayout.Bands[0].Columns["ClientName"].CellActivation = Activation.NoEdit;
				this.grdConsigneeOverrides.DisplayLayout.Bands[0].Columns["Number"].CellActivation = Activation.NoEdit;
				this.grdConsigneeOverrides.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
				this.grdConsigneeOverrides.DisplayLayout.Bands[0].Columns["Address"].CellActivation = Activation.NoEdit;
				this.grdConsigneeOverrides.DisplayLayout.Bands[0].Columns["OldPathMnemonic"].CellActivation = Activation.NoEdit;
				this.grdConsigneeOverrides.DisplayLayout.Bands[0].Columns["OldServiceMnemonic"].CellActivation = Activation.NoEdit;
				this.grdConsigneeOverrides.DisplayLayout.Bands[0].Columns["PathID"].CellActivation = Activation.AllowEdit;
				this.grdConsigneeOverrides.DisplayLayout.Bands[0].Columns["ServiceID"].CellActivation = Activation.AllowEdit;
				this.grdConsigneeOverrides.DisplayLayout.Bands[0].Columns["PathID"].EditorControl = this.cboPath;
				this.grdConsigneeOverrides.DisplayLayout.Bands[0].Columns["ServiceID"].EditorControl = this.cboSvc;
				#endregion
				if(this.cboClients.Items.Count>0) this.cboClients.SelectedIndex = 0;
				this.cboClients.Enabled = (this.cboClients.Items.Count>0);
				OnClientChanged(null, null);
				
				//Reset
				this.rdoStore.Checked = true;
				this.chkOverridesOnly.Checked = true;
				this.AutoSelectRow(null, null);
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.btnApply.Enabled = false; this.Cursor = Cursors.Default; }
		}
		private void OnClientChanged(object sender, System.EventArgs e) {
			//Event handler for change in freigth path type
			this.Cursor = Cursors.WaitCursor;
			try {
				//
				this.mLocationsViewDS.Clear();
				this.mLocationsViewDS.Merge(EnterpriseFactory.ViewClientLocations(Convert.ToInt32(this.cboClients.SelectedValue)));
				if(this.grdLocations.Rows.Count <= 0) {
					MessageBox.Show(this, this.cboClients.Text.Trim() + " currently has no active locations.", "No Locations", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
					this.OnLocationChanged(null, null);	
				}				
				this.mIndex = 0;
				AutoSelectRow(null, null);
				ClearRefillGrids(null, null);
			} 
			catch(Exception ex) { reportError(ex); }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnLocationTypeChanged(object sender, System.EventArgs e) {
			//Event handler for change in freigth path type
			this.Cursor = Cursors.WaitCursor;
			try {
				//Filter locations for the selected type
				string filter = (this.rdoVendor.Checked) ? "WareHouse" : "Store";
				this.grdLocations.DisplayLayout.Bands[0].ColumnFilters["LocationType"].FilterConditions.Clear();
				this.grdLocations.DisplayLayout.Bands[0].ColumnFilters["LocationType"].FilterConditions.Add(FilterComparisionOperator.Equals, filter);
				
				//Update valid service type selections
				if(this.rdoStore.Checked) {
					this.mSvcsDS.Clear();
					this.mSvcsDS.Merge(EnterpriseFactory.GetRegularOutboundServiceTypes());
				}
				else {
					this.mSvcsDS.Clear();
					this.mSvcsDS.Merge(EnterpriseFactory.GetReturnOutboundServiceTypes());
				}
				this.cboSvc.Items.Clear();
				for(int i=0; i<this.mSvcsDS.SelectionListTable.Rows.Count; i++) 
					this.cboSvc.Items.Add(this.mSvcsDS.SelectionListTable[i].ID, this.mSvcsDS.SelectionListTable[i].Description);
				this.cboSvc.Enabled = (this.cboSvc.Items.Count>0);
				ClearRefillGrids(null, null);
				OnApplyOverridesOnly(null, null);
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnApplyOverridesOnly(object sender, System.EventArgs e) {
			//Event handler for change in freigth path type
			this.Cursor = Cursors.WaitCursor;
			try {
				//Filter by overrides
				this.grdLocations.DisplayLayout.Bands[0].ColumnFilters["HasOverride"].FilterConditions.Clear();
				if(this.chkOverridesOnly.Checked)
					this.grdLocations.DisplayLayout.Bands[0].ColumnFilters["HasOverride"].FilterConditions.Add(FilterComparisionOperator.Equals, true);
				ClearRefillGrids(null, null);
				AutoSelectRow(null, null);
			} 
			catch(Exception ex) { reportError(ex); }
			finally { this.Cursor = Cursors.Default; }
		}
		#region Location Grid: OnLocationChanged()
		private void OnLocationChanged(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in location selection
			int iSortCenterID=0;
			DeliveryLocationDS.DeliveryLocationOverrideDetailTableRow rowOverride;
			DeliveryLocationDS.DeliveryLocationOverrideDetailTableRow _rowOverride;
			this.Cursor = Cursors.WaitCursor;
			try {
				this.mMappingsViewDS.Clear();
				this.mOverridesDS.Clear();
				this.mOverrideDetailDS.Clear();
				if(this.grdLocations.Rows.VisibleRowCount>0 && this.grdLocations.Selected.Rows.Count>0) {
					this.mLocationID = Convert.ToInt32(this.grdLocations.Selected.Rows[0].Cells["LocationID"].Value);
					mIndex = this.grdLocations.Selected.Rows[0].Index;
					//1.	Display mappings for the selected location
					try {
						this.mMappingsViewDS.Clear();
						this.mMappingsViewDS.Merge(EnterpriseFactory.ViewClientLocationMappings(Convert.ToInt32(this.cboClients.SelectedValue), this.mLocationID));
					}
					catch(Exception ex) { reportError(ex); }
					finally { this.Cursor = Cursors.Default; }
					
					//2.	Get overrides for the selected location- start with a template (possible overrides)
					//		and update template with any actual overrides
					this.mOverridesDS.Clear();
					this.mOverridesDS.Merge(EnterpriseFactory.GetConsigneeOverridesTemplate(Convert.ToInt32(this.cboClients.SelectedValue), this.cboClients.Text));
					this.mOverrideDetailDS.Clear();
					this.mOverrideDetailDS.Merge(EnterpriseFactory.GetConsigneeOverrides(Convert.ToInt32(this.cboClients.SelectedValue), this.mLocationID));
					for(int i=0; i<this.mOverrideDetailDS.DeliveryLocationOverrideDetailTable.Rows.Count; i++) {
						//Find the corresponding template row by terminal id (unique for each row)
						rowOverride = this.mOverrideDetailDS.DeliveryLocationOverrideDetailTable[i];
						iSortCenterID = rowOverride.SortCenterID;
						_rowOverride = (DeliveryLocationDS.DeliveryLocationOverrideDetailTableRow)this.mOverridesDS.DeliveryLocationOverrideDetailTable.Select("SortCenterID=" + iSortCenterID)[0];
						if(_rowOverride!=null) {
							_rowOverride.Selected = true;
							_rowOverride.SortCenterID = iSortCenterID;
							_rowOverride.SortCenter = rowOverride.SortCenter;
							_rowOverride.ClientID = rowOverride.ClientID;
							_rowOverride.ClientName = rowOverride.ClientName;
							_rowOverride.LocationID = rowOverride.LocationID;
							_rowOverride.Number = rowOverride.Number;
							_rowOverride.Description = rowOverride.Description;
							_rowOverride.Address = rowOverride.Address;
							_rowOverride.PathID = rowOverride.PathID;
							_rowOverride.PathMnemonic = _rowOverride.OldPathMnemonic = rowOverride.PathMnemonic;
							_rowOverride.ServiceID = rowOverride.ServiceID;
							_rowOverride.ServiceMnemonic = _rowOverride.OldServiceMnemonic = rowOverride.ServiceMnemonic;
							_rowOverride.LastUpdated = rowOverride.LastUpdated;
							_rowOverride.UserID = rowOverride.UserID;
							_rowOverride.RowVersion = rowOverride.RowVersion;
						}
					}
					//TODO 
					//this.grdLocations.ActiveRow = this.grdLocations.Rows;
					//this.grdLocations.Rows[0].Selected = true;
					this.btnApply.Enabled = false;
				}
			} 
			catch(Exception ex) { reportError(ex); }
			finally { this.Cursor = Cursors.Default; }
		}
		#endregion
		#region DeliveryLocation Grid: OnOverrideCellActivated(), OnOverrideCellUpdated(), OnOverrideRowUpdated()
		private void OnOverrideCellActivated(object sender, Infragistics.Win.UltraWinGrid.CancelableCellEventArgs e) {
			//Event handler for freight path stop cell activated
			int iSortCenterID=0;
			bool bSelected=false; 
			try {
				//Mnemonic selection avaliable if a mnemonic exists for the selected location
				bSelected = Convert.ToBoolean(e.Cell.Row.Cells["Selected"].Value);
				iSortCenterID = Convert.ToInt32(e.Cell.Row.Cells["SortCenterID"].Value);
				switch(e.Cell.Column.Key.ToString()) {
					case "Selected":
						e.Cell.Activation = Activation.AllowEdit;
						if(!bSelected) {
							//Going selected (this is BEFORE the selected cell value changes)
							this.mPathsDS.Clear();
							this.mPathsDS.Merge(EnterpriseFactory.GetFreightPaths(iSortCenterID));
							e.Cell.Row.Cells["PathID"].Value = (this.mPathsDS.SelectionListTable.Rows.Count>0) ? this.mPathsDS.SelectionListTable[0].ID : "";
							e.Cell.Row.Cells["ServiceID"].Value = (this.mSvcsDS.SelectionListTable.Rows.Count>0) ? Convert.ToInt32(this.mSvcsDS.SelectionListTable[0].ID) : 0;
						}
						else {
							e.Cell.Row.Cells["PathID"].Value = "";
							e.Cell.Row.Cells["ServiceID"].Value = 0;
						}
						break;
					case "PathID":
						if(bSelected) {
							//Get valid freight paths for the sort center and re-populate combobox
							this.cboPath.Items.Clear();
							this.mPathsDS.Clear();
							this.mPathsDS.Merge(EnterpriseFactory.GetFreightPaths(iSortCenterID));
							for(int i=0; i<this.mPathsDS.SelectionListTable.Rows.Count; i++) 
								this.cboPath.Items.Add(this.mPathsDS.SelectionListTable[i].ID, this.mPathsDS.SelectionListTable[i].Description);
							this.cboPath.Enabled = (this.cboPath.Items.Count>0);
							e.Cell.Activation = Activation.AllowEdit;
						}
						else
							e.Cell.Activation = Activation.NoEdit; 
						break;
					case "ServiceID":
						e.Cell.Activation = (bSelected) ? Activation.AllowEdit : Activation.NoEdit;
						break;
					default:
						e.Cell.Activation = Activation.NoEdit;
						break;
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnOverrideCellUpdated(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e) {
			//Event handler for change in a override cell value
			try {
				//Update asap
				switch(e.Cell.Column.Key.ToString()) {
					default:	break;
				}
				this.grdConsigneeOverrides.UpdateData();
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnOverrideRowUpdated(object sender, Infragistics.Win.UltraWinGrid.RowEventArgs e) {
			//Event handler for change in override row
			ValidateForm(null, null);
		}
		#endregion
		private void RefreshForm(object sender, System.EventArgs e) {
			//
			this.OnClientChanged(null, null);
		}
		private void AutoSelectRow(object sender, System.EventArgs e) {
			try {
				if (this.grdLocations.Rows.Count>0) {
					if (this.grdLocations.Rows[mIndex].VisibleIndex >= 0) {
						this.grdLocations.ActiveRow = this.grdLocations.Rows[mIndex];
						this.grdLocations.Rows[mIndex].Selected = true;
					}
					else {
						//selects the first row, takes into account if grid is filtered or not...
						for(int i=0; i<this.grdLocations.Rows.Count; i++) {
							if(this.grdLocations.Rows[i].VisibleIndex >= 0) {
								this.grdLocations.ActiveRow = this.grdLocations.Rows[i];
								this.grdLocations.Rows[i].Selected = true;
								break;
							}
						}
					}
				}
			}
			catch(Exception ex) { reportError(ex); }
		}			
		private void ClearRefillGrids(object sender, System.EventArgs e) {
			try {
				if(this.grdLocations.Rows.VisibleRowCount<=0) {
					this.grdMappings.DisplayLayout.Bands[0].ColumnFilters["SortCenter"].FilterConditions.Clear();
					this.grdMappings.DisplayLayout.Bands[0].ColumnFilters["SortCenter"].FilterConditions.Add(FilterComparisionOperator.Equals, "NONE");
					this.grdConsigneeOverrides.DisplayLayout.Bands[0].ColumnFilters["SortCenter"].FilterConditions.Clear();
					this.grdConsigneeOverrides.DisplayLayout.Bands[0].ColumnFilters["SortCenter"].FilterConditions.Add(FilterComparisionOperator.Equals, "NONE");
				}
				else {
					this.grdMappings.DisplayLayout.Bands[0].ColumnFilters["SortCenter"].FilterConditions.Clear();
					this.grdConsigneeOverrides.DisplayLayout.Bands[0].ColumnFilters["SortCenter"].FilterConditions.Clear();
				}
			}
			catch(Exception ex) { reportError(ex); }
		}
		#region User Services: ValidateForm(), OnCmdClick()
		private void ValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes to control data
			bool bOverridesValid=false, bSelected=false; 
			string sPath="";
			int iSvc=0;
			try {
				if(this.mOverridesDS.DeliveryLocationOverrideDetailTable.Count>0) {
					//Validate overrides
					bOverridesValid = true;
					for(int i=0; i<this.grdConsigneeOverrides.Rows.Count; i++) {
						//Verify freight paths and service types for selected (new, edit) rows
						bSelected = Convert.ToBoolean(this.grdConsigneeOverrides.Rows[i].Cells["Selected"].Value);
						if(bSelected) {
							sPath = this.grdConsigneeOverrides.Rows[i].Cells["PathID"].Value.ToString();
							iSvc = Convert.ToInt32(this.grdConsigneeOverrides.Rows[i].Cells["ServiceID"].Value);
							bOverridesValid = (sPath!="" && iSvc>0);
							if(!bOverridesValid) break;
						}
					}

					//Enable OK service if details have valid changes
					this.btnApply.Enabled = (this.cboClients.Text!="" && bOverridesValid);
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnCmdClick(object sender, System.EventArgs e) {
			//Command button handler
			bool bSelected=false, bHasLocationID=false;
			int iSortCenterID=0;
			DeliveryLocationDS.DeliveryLocationOverrideDetailTableRow rowOverride;
			DeliveryLocationDS.DeliveryLocationOverrideDetailTableRow _rowOverride;
			bool bResult=false;
			try {
				Button btn = (Button)sender;
				switch(btn.Text) {
					case CMD_CLOSE:
						this.DialogResult = DialogResult.Cancel;
						break;
					case CMD_APPLY:
						//Write temlate (mOverridesDS) back to detail (mOverrideDetailDS)
						this.Cursor = Cursors.WaitCursor;
						for(int i=0; i<this.mOverridesDS.DeliveryLocationOverrideDetailTable.Rows.Count; i++) {
							_rowOverride = this.mOverridesDS.DeliveryLocationOverrideDetailTable[i];
							bSelected = _rowOverride.Selected;
							bHasLocationID = (_rowOverride.LocationID>0) ? true : false;
							iSortCenterID = _rowOverride.SortCenterID;
							if(bSelected && !bHasLocationID) {
								//Add a new override
								//*** RESULT MUST MARK ROW AS DataRowState.Added
								rowOverride = this.mOverrideDetailDS.DeliveryLocationOverrideDetailTable.NewDeliveryLocationOverrideDetailTableRow();
								rowOverride.SortCenterID = iSortCenterID;
								rowOverride.ClientID = _rowOverride.ClientID;
								rowOverride.LocationID = this.mLocationID;
								rowOverride.PathID = _rowOverride.PathID;
								rowOverride.PathMnemonic = "";
								rowOverride.PathLastStopMnemonic = "";
								rowOverride.ServiceID = _rowOverride.ServiceID;
								rowOverride.ServiceMnemonic = "";
								rowOverride.LastUpdated = _rowOverride.LastUpdated;
								rowOverride.UserID = _rowOverride.UserID;
								rowOverride.RowVersion = _rowOverride.RowVersion;
								this.mOverrideDetailDS.DeliveryLocationOverrideDetailTable.AddDeliveryLocationOverrideDetailTableRow(rowOverride);
							}
							else if(bSelected && bHasLocationID) {
								//Current override- update freigth path and service type
								rowOverride = (DeliveryLocationDS.DeliveryLocationOverrideDetailTableRow)this.mOverrideDetailDS.DeliveryLocationOverrideDetailTable.Select("SortCenterID=" + iSortCenterID)[0];
								rowOverride.PathID = _rowOverride.PathID;
								rowOverride.ServiceID = _rowOverride.ServiceID;
							}
							else if(!bSelected && bHasLocationID) {
								//Remove current override
								//*** RESULT MUST MARK ROW AS DataRowState.Deleted
								rowOverride = (DeliveryLocationDS.DeliveryLocationOverrideDetailTableRow)this.mOverrideDetailDS.DeliveryLocationOverrideDetailTable.Select("SortCenterID=" + iSortCenterID)[0];
								rowOverride.Delete();
							}
						}

						#region Test update to mOverrideDetailDS.DeliveryLocationOverrideDetailTable
						Debug.Write("\n");
						for(int i=0; i<this.mOverrideDetailDS.DeliveryLocationOverrideDetailTable.Rows.Count; i++) {
							switch(this.mOverrideDetailDS.DeliveryLocationOverrideDetailTable[i].RowState) {
								case		DataRowState.Added: Debug.Write("Added override (SortCenterID=)\n"); break;
								case		DataRowState.Modified: Debug.Write("Modified override (SortCenterID=)\n"); break;
								case		DataRowState.Deleted: Debug.Write("Removed override (SortCenterID=)\n"); break;
								default:	Debug.Write(this.mOverrideDetailDS.DeliveryLocationOverrideDetailTable[i].RowState.ToString() + " override (SortCenterID=)\n"); break;
							}
						}
						#endregion
						bResult = EnterpriseFactory.UpdateConsigneeOverrides(this.mOverrideDetailDS);
						string sDescription = this.grdLocations.Selected.Rows[0].Cells["Description"].Value.ToString().Trim();
						if(bResult) {
							OnLocationChanged(null, null);
							this.btnApply.Enabled = false;
							MessageBox.Show(this, "Consignee MapDS Override for " + sDescription + " has been modified.", this.Text, MessageBoxButtons.OK);
						}
						else {
							MessageBox.Show(this, "Consignee MapDS Override for " + sDescription + " could not be modified at this time.", this.Text, MessageBoxButtons.OK);
						}
						//this.mOverrideDetailDS.AcceptChanges();
						//this.DialogResult = DialogResult.OK;
						//this.Close();
						RefreshForm(null, null);
						break;
				}
			} 
			catch(Exception ex) { reportError(ex); }
			finally { this.Cursor = Cursors.Default; }
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
