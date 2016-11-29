//	File:	dlgclientvendor.cs
//	Author:	J. Heary
//	Date:	04/28/06
//	Desc:	Dialog to create a new client-vendor association or edit an existing 
//			client-vendor association.
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
using Tsort.Windows;

namespace Tsort {
	//
	public class dlgClientVendorDetail : System.Windows.Forms.Form {
		//Members
		private int mClientID=0;
		private int mAssocID=0;
		private bool mParentIsActive=true;
		#region Controls
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private Tsort.Windows.SelectionList mVendorsDS;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.TabControl tabDialog;
		private Tsort.Enterprise.CVLocationDS mAssociationDS;
		private System.Windows.Forms.CheckBox chkStatus;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdSortProfile;
		private System.Windows.Forms.GroupBox grpVendorLocation;
		private System.Windows.Forms.GroupBox grpFSA;
		private System.Windows.Forms.Label _lblClient;
		private System.Windows.Forms.Label _lblLocation;
		private System.Windows.Forms.Label _lblAddress;
		private System.Windows.Forms.ComboBox cboVendor;
		private System.Windows.Forms.ComboBox cboVendorLoc;
		private System.Windows.Forms.Label lblAddress;
		private System.Windows.Forms.GroupBox fraCVNumbers;
		private System.Windows.Forms.ComboBox cboClient;
		private Tsort.Windows.SelectionList mClientsDS;
		private Tsort.Windows.SelectionList mInboundLabelsDS;
		private Tsort.Enterprise.SortProfileDS mSortProfileDS;
		private Infragistics.Win.UltraWinEditors.UltraComboEditor cboIBLabel;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdCVNumbers;
		private System.Windows.Forms.Label _lblVendor;
		private System.Windows.Forms.ContextMenu ctxNumbers;
		private System.Windows.Forms.MenuItem ctxNumberAdd;
		private System.Windows.Forms.MenuItem ctxNumberEdit;
		private System.Windows.Forms.MenuItem ctxNumberRemove;
		private System.Windows.Forms.Button btnNumberAdd;
		private System.Windows.Forms.Button btnNumberRemove;
		private System.Windows.Forms.Button btnNumberEdit;
		private Tsort.Enterprise.LocationDS mLocationsDS;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		
		//Constants
		private const string CMD_CANCEL = "&Cancel";
		private const string CMD_OK = "O&K";
		private const string CMD_ADD = "&Add";
		private const string CMD_EDIT = "&Edit";
		private const string CMD_REMOVE = "&Remove";
		private const string MNU_ADD = "&Add Number";
		private const string MNU_EDIT = "&Edit Number";
		private const string MNU_REMOVE = "&Remove Number";
		private const string GRID_TYPE_NUMBERS = "CV Numbers";
		
		//Events
		public event ErrorEventHandler ErrorMessage=null;
		
		//Interface
		public dlgClientVendorDetail(int clientID, bool parentIsActive, ref CVLocationDS association) {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();
				this.btnOk.Text = CMD_OK;
				this.btnCancel.Text = CMD_CANCEL;
				this.btnNumberAdd.Text = CMD_ADD;
				this.btnNumberEdit.Text = CMD_EDIT;
				this.btnNumberRemove.Text = CMD_REMOVE;
				this.ctxNumberAdd.Text = MNU_ADD;
				this.ctxNumberEdit.Text = MNU_EDIT;
				this.ctxNumberRemove.Text = MNU_REMOVE;
				this.grdCVNumbers.Tag = GRID_TYPE_NUMBERS;
				
				//Set mediator service, data, and titlebar caption
				this.mClientID = clientID;
				this.mParentIsActive = parentIsActive;
				this.mAssociationDS = association;
				if(this.mAssociationDS.CVLocationDetailTable.Count > 0) {
					this.mAssocID = this.mAssociationDS.CVLocationDetailTable[0].LinkID;
					this.Text = (this.mAssocID>0) ? "Vendor Location Association (" + this.mAssocID + ")" : "Vendor Location Association (New)";
				}
				else
					this.Text = "Vendor Location Association(Data Unavailable)";
			} 
			catch(Exception ex) { throw ex; }
		}
		protected override void Dispose( bool disposing ) { if( disposing ) { if (components != null) components.Dispose(); } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("CVLocationNumberTable", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LinkID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Number");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CVLocationDetailTable_Id");
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("SortProfileTable", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Selected");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ProfileID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LinkID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LabelID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortTypeID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortType");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsElectronic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ManifestPerTrailer");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsActive");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RowVersion");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortProfileTable_Id");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortProfileTable_SortProfileTerminalTable");
			Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("SortProfileTable_SortProfileTerminalTable", 0);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Selected");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ProfileID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TerminalID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Terminal");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortProfileTable_Id");
			Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgClientVendorDetail));
			this.mVendorsDS = new Tsort.Windows.SelectionList();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.fraCVNumbers = new System.Windows.Forms.GroupBox();
			this.btnNumberRemove = new System.Windows.Forms.Button();
			this.btnNumberAdd = new System.Windows.Forms.Button();
			this.btnNumberEdit = new System.Windows.Forms.Button();
			this.grdCVNumbers = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.ctxNumbers = new System.Windows.Forms.ContextMenu();
			this.ctxNumberAdd = new System.Windows.Forms.MenuItem();
			this.ctxNumberEdit = new System.Windows.Forms.MenuItem();
			this.ctxNumberRemove = new System.Windows.Forms.MenuItem();
			this.mAssociationDS = new Tsort.Enterprise.CVLocationDS();
			this.chkStatus = new System.Windows.Forms.CheckBox();
			this.tabDialog = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.cboClient = new System.Windows.Forms.ComboBox();
			this.mClientsDS = new Tsort.Windows.SelectionList();
			this._lblClient = new System.Windows.Forms.Label();
			this.grpFSA = new System.Windows.Forms.GroupBox();
			this.grdSortProfile = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.mSortProfileDS = new Tsort.Enterprise.SortProfileDS();
			this.cboIBLabel = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.grpVendorLocation = new System.Windows.Forms.GroupBox();
			this.lblAddress = new System.Windows.Forms.Label();
			this.cboVendorLoc = new System.Windows.Forms.ComboBox();
			this.mLocationsDS = new Tsort.Enterprise.LocationDS();
			this.cboVendor = new System.Windows.Forms.ComboBox();
			this._lblAddress = new System.Windows.Forms.Label();
			this._lblLocation = new System.Windows.Forms.Label();
			this._lblVendor = new System.Windows.Forms.Label();
			this.mInboundLabelsDS = new Tsort.Windows.SelectionList();
			((System.ComponentModel.ISupportInitialize)(this.mVendorsDS)).BeginInit();
			this.fraCVNumbers.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdCVNumbers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mAssociationDS)).BeginInit();
			this.tabDialog.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mClientsDS)).BeginInit();
			this.grpFSA.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdSortProfile)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mSortProfileDS)).BeginInit();
			this.grpVendorLocation.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mLocationsDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mInboundLabelsDS)).BeginInit();
			this.SuspendLayout();
			// 
			// mVendorsDS
			// 
			this.mVendorsDS.DataSetName = "SelectionList";
			this.mVendorsDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// btnCancel
			// 
			this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(471, 525);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(96, 24);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// btnOk
			// 
			this.btnOk.BackColor = System.Drawing.SystemColors.Control;
			this.btnOk.Enabled = false;
			this.btnOk.Location = new System.Drawing.Point(372, 525);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(96, 24);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "&OK";
			this.btnOk.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// fraCVNumbers
			// 
			this.fraCVNumbers.Controls.Add(this.btnNumberRemove);
			this.fraCVNumbers.Controls.Add(this.btnNumberAdd);
			this.fraCVNumbers.Controls.Add(this.btnNumberEdit);
			this.fraCVNumbers.Controls.Add(this.grdCVNumbers);
			this.fraCVNumbers.Location = new System.Drawing.Point(354, 39);
			this.fraCVNumbers.Name = "fraCVNumbers";
			this.fraCVNumbers.Size = new System.Drawing.Size(195, 129);
			this.fraCVNumbers.TabIndex = 2;
			this.fraCVNumbers.TabStop = false;
			this.fraCVNumbers.Text = "Client-Vendor Numbers";
			// 
			// btnNumberRemove
			// 
			this.btnNumberRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNumberRemove.BackColor = System.Drawing.SystemColors.Control;
			this.btnNumberRemove.Enabled = false;
			this.btnNumberRemove.Location = new System.Drawing.Point(126, 78);
			this.btnNumberRemove.Name = "btnNumberRemove";
			this.btnNumberRemove.Size = new System.Drawing.Size(60, 18);
			this.btnNumberRemove.TabIndex = 3;
			this.btnNumberRemove.Text = "&Remove";
			this.btnNumberRemove.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// btnNumberAdd
			// 
			this.btnNumberAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNumberAdd.BackColor = System.Drawing.SystemColors.Control;
			this.btnNumberAdd.Enabled = false;
			this.btnNumberAdd.Location = new System.Drawing.Point(126, 30);
			this.btnNumberAdd.Name = "btnNumberAdd";
			this.btnNumberAdd.Size = new System.Drawing.Size(60, 18);
			this.btnNumberAdd.TabIndex = 1;
			this.btnNumberAdd.Text = "&Add";
			this.btnNumberAdd.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// btnNumberEdit
			// 
			this.btnNumberEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNumberEdit.BackColor = System.Drawing.SystemColors.Control;
			this.btnNumberEdit.Enabled = false;
			this.btnNumberEdit.Location = new System.Drawing.Point(126, 54);
			this.btnNumberEdit.Name = "btnNumberEdit";
			this.btnNumberEdit.Size = new System.Drawing.Size(60, 18);
			this.btnNumberEdit.TabIndex = 2;
			this.btnNumberEdit.Text = "&Edit";
			this.btnNumberEdit.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// grdCVNumbers
			// 
			this.grdCVNumbers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grdCVNumbers.ContextMenu = this.ctxNumbers;
			this.grdCVNumbers.DataMember = "CVLocationDetailTable.CVLocationDetailTable_CVLocationNumberTable";
			this.grdCVNumbers.DataSource = this.mAssociationDS;
			this.grdCVNumbers.DisplayLayout.AddNewBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			this.grdCVNumbers.DisplayLayout.AddNewBox.ButtonConnectorStyle = Infragistics.Win.UIElementBorderStyle.None;
			this.grdCVNumbers.DisplayLayout.AddNewBox.Prompt = " ";
			appearance1.BackColor = System.Drawing.Color.White;
			appearance1.FontData.Name = "Arial";
			appearance1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.grdCVNumbers.DisplayLayout.Appearance = appearance1;
			ultraGridColumn1.Hidden = true;
			ultraGridColumn1.Width = 50;
			ultraGridColumn2.Width = 108;
			ultraGridColumn3.Hidden = true;
			ultraGridBand1.Columns.Add(ultraGridColumn1);
			ultraGridBand1.Columns.Add(ultraGridColumn2);
			ultraGridBand1.Columns.Add(ultraGridColumn3);
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
			this.grdCVNumbers.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
			appearance6.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(128)), ((System.Byte)(255)));
			appearance6.FontData.Name = "Verdana";
			appearance6.FontData.SizeInPoints = 8F;
			appearance6.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance6.TextHAlign = Infragistics.Win.HAlign.Left;
			this.grdCVNumbers.DisplayLayout.CaptionAppearance = appearance6;
			this.grdCVNumbers.DisplayLayout.GroupByBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			this.grdCVNumbers.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.Yes;
			this.grdCVNumbers.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.True;
			this.grdCVNumbers.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
			this.grdCVNumbers.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None;
			this.grdCVNumbers.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.None;
			this.grdCVNumbers.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
			this.grdCVNumbers.DisplayLayout.Override.CellMultiLine = Infragistics.Win.DefaultableBoolean.False;
			this.grdCVNumbers.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
			this.grdCVNumbers.DisplayLayout.Override.MaxSelectedCells = 1;
			this.grdCVNumbers.DisplayLayout.Override.MaxSelectedRows = 1;
			this.grdCVNumbers.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			this.grdCVNumbers.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
			this.grdCVNumbers.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.Vertical;
			this.grdCVNumbers.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
			this.grdCVNumbers.Location = new System.Drawing.Point(9, 30);
			this.grdCVNumbers.Name = "grdCVNumbers";
			this.grdCVNumbers.Size = new System.Drawing.Size(108, 91);
			this.grdCVNumbers.TabIndex = 0;
			this.grdCVNumbers.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnCellChangeOrLostFocus;
			this.grdCVNumbers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
			this.grdCVNumbers.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridSelectionChanged);
			this.grdCVNumbers.AfterRowUpdate += new Infragistics.Win.UltraWinGrid.RowEventHandler(this.OnCVNumberRowUpdated);
			this.grdCVNumbers.BeforeCellActivate += new Infragistics.Win.UltraWinGrid.CancelableCellEventHandler(this.OnBeforeCVNumberCellActivated);
			this.grdCVNumbers.AfterCellUpdate += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.OnCVNumberCellUpdated);
			// 
			// ctxNumbers
			// 
			this.ctxNumbers.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.ctxNumberAdd,
																					   this.ctxNumberEdit,
																					   this.ctxNumberRemove});
			// 
			// ctxNumberAdd
			// 
			this.ctxNumberAdd.Index = 0;
			this.ctxNumberAdd.Text = "&Add";
			this.ctxNumberAdd.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxNumberEdit
			// 
			this.ctxNumberEdit.Index = 1;
			this.ctxNumberEdit.Text = "&Edit";
			this.ctxNumberEdit.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxNumberRemove
			// 
			this.ctxNumberRemove.Index = 2;
			this.ctxNumberRemove.Text = "&Remove";
			this.ctxNumberRemove.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mAssociationDS
			// 
			this.mAssociationDS.DataSetName = "CVLocationDS";
			this.mAssociationDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// chkStatus
			// 
			this.chkStatus.Checked = true;
			this.chkStatus.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkStatus.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkStatus.Location = new System.Drawing.Point(8, 465);
			this.chkStatus.Name = "chkStatus";
			this.chkStatus.Size = new System.Drawing.Size(72, 18);
			this.chkStatus.TabIndex = 4;
			this.chkStatus.Text = "Active";
			this.chkStatus.CheckedChanged += new System.EventHandler(this.ValidateForm);
			// 
			// tabDialog
			// 
			this.tabDialog.Controls.Add(this.tabGeneral);
			this.tabDialog.Location = new System.Drawing.Point(3, 3);
			this.tabDialog.Name = "tabDialog";
			this.tabDialog.SelectedIndex = 0;
			this.tabDialog.Size = new System.Drawing.Size(564, 516);
			this.tabDialog.TabIndex = 1;
			// 
			// tabGeneral
			// 
			this.tabGeneral.Controls.Add(this.cboClient);
			this.tabGeneral.Controls.Add(this._lblClient);
			this.tabGeneral.Controls.Add(this.grpFSA);
			this.tabGeneral.Controls.Add(this.fraCVNumbers);
			this.tabGeneral.Controls.Add(this.grpVendorLocation);
			this.tabGeneral.Controls.Add(this.chkStatus);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Size = new System.Drawing.Size(556, 490);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			this.tabGeneral.ToolTipText = "General information";
			// 
			// cboClient
			// 
			this.cboClient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cboClient.DataSource = this.mClientsDS;
			this.cboClient.DisplayMember = "SelectionListTable.Description";
			this.cboClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboClient.Location = new System.Drawing.Point(93, 15);
			this.cboClient.Name = "cboClient";
			this.cboClient.Size = new System.Drawing.Size(240, 21);
			this.cboClient.TabIndex = 0;
			this.cboClient.ValueMember = "SelectionListTable.ID";
			this.cboClient.SelectionChangeCommitted += new System.EventHandler(this.ValidateForm);
			// 
			// mClientsDS
			// 
			this.mClientsDS.DataSetName = "SelectionList";
			this.mClientsDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// _lblClient
			// 
			this._lblClient.Location = new System.Drawing.Point(16, 16);
			this._lblClient.Name = "_lblClient";
			this._lblClient.Size = new System.Drawing.Size(72, 16);
			this._lblClient.TabIndex = 6;
			this._lblClient.Text = "Client";
			this._lblClient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// grpFSA
			// 
			this.grpFSA.Controls.Add(this.grdSortProfile);
			this.grpFSA.Controls.Add(this.cboIBLabel);
			this.grpFSA.Location = new System.Drawing.Point(6, 174);
			this.grpFSA.Name = "grpFSA";
			this.grpFSA.Size = new System.Drawing.Size(543, 285);
			this.grpFSA.TabIndex = 3;
			this.grpFSA.TabStop = false;
			this.grpFSA.Text = "Freight Sort Types";
			// 
			// grdSortProfile
			// 
			this.grdSortProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grdSortProfile.DataMember = "SortProfileTable";
			this.grdSortProfile.DataSource = this.mSortProfileDS;
			this.grdSortProfile.DisplayLayout.AddNewBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			appearance7.BackColor = System.Drawing.Color.White;
			appearance7.FontData.Name = "Arial";
			appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
			this.grdSortProfile.DisplayLayout.Appearance = appearance7;
			ultraGridColumn4.Header.Caption = "Select";
			ultraGridColumn4.Width = 65;
			ultraGridColumn5.Header.VisiblePosition = 2;
			ultraGridColumn5.Hidden = true;
			ultraGridColumn5.Width = 80;
			ultraGridColumn6.Header.VisiblePosition = 1;
			ultraGridColumn6.Hidden = true;
			ultraGridColumn6.Width = 67;
			ultraGridColumn7.Header.Caption = "Inbound Label";
			ultraGridColumn7.Header.VisiblePosition = 4;
			ultraGridColumn7.Width = 141;
			ultraGridColumn8.Header.Caption = "FSTID";
			ultraGridColumn8.Header.VisiblePosition = 5;
			ultraGridColumn8.Hidden = true;
			ultraGridColumn8.Width = 51;
			ultraGridColumn9.Header.VisiblePosition = 3;
			ultraGridColumn9.Width = 139;
			ultraGridColumn10.Header.Caption = "Electronic";
			ultraGridColumn10.Width = 72;
			ultraGridColumn11.Header.Caption = "Manifest\\Trailer";
			ultraGridColumn11.Width = 69;
			ultraGridColumn12.Header.Caption = "Active";
			ultraGridColumn12.Width = 48;
			ultraGridColumn13.Hidden = true;
			ultraGridColumn14.Hidden = true;
			ultraGridColumn15.Hidden = true;
			ultraGridColumn16.Hidden = true;
			ultraGridBand2.Columns.Add(ultraGridColumn4);
			ultraGridBand2.Columns.Add(ultraGridColumn5);
			ultraGridBand2.Columns.Add(ultraGridColumn6);
			ultraGridBand2.Columns.Add(ultraGridColumn7);
			ultraGridBand2.Columns.Add(ultraGridColumn8);
			ultraGridBand2.Columns.Add(ultraGridColumn9);
			ultraGridBand2.Columns.Add(ultraGridColumn10);
			ultraGridBand2.Columns.Add(ultraGridColumn11);
			ultraGridBand2.Columns.Add(ultraGridColumn12);
			ultraGridBand2.Columns.Add(ultraGridColumn13);
			ultraGridBand2.Columns.Add(ultraGridColumn14);
			ultraGridBand2.Columns.Add(ultraGridColumn15);
			ultraGridBand2.Columns.Add(ultraGridColumn16);
			ultraGridBand2.Columns.Add(ultraGridColumn17);
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
			ultraGridColumn18.Header.Caption = "Select";
			ultraGridColumn18.Width = 46;
			ultraGridColumn19.Hidden = true;
			ultraGridColumn19.Width = 67;
			ultraGridColumn20.Hidden = true;
			ultraGridColumn20.Width = 80;
			ultraGridColumn21.Header.Caption = "Processing Terminal";
			ultraGridColumn21.Width = 139;
			ultraGridColumn22.Hidden = true;
			ultraGridBand3.Columns.Add(ultraGridColumn18);
			ultraGridBand3.Columns.Add(ultraGridColumn19);
			ultraGridBand3.Columns.Add(ultraGridColumn20);
			ultraGridBand3.Columns.Add(ultraGridColumn21);
			ultraGridBand3.Columns.Add(ultraGridColumn22);
			appearance12.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(220)), ((System.Byte)(255)), ((System.Byte)(200)));
			appearance12.FontData.Name = "Verdana";
			appearance12.FontData.SizeInPoints = 8F;
			appearance12.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridBand3.Override.ActiveRowAppearance = appearance12;
			appearance13.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
			appearance13.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
			appearance13.FontData.Name = "Verdana";
			appearance13.FontData.SizeInPoints = 8F;
			appearance13.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance13.TextHAlign = Infragistics.Win.HAlign.Left;
			ultraGridBand3.Override.HeaderAppearance = appearance13;
			appearance14.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(240)), ((System.Byte)(240)), ((System.Byte)(255)));
			appearance14.FontData.Name = "Verdana";
			appearance14.FontData.SizeInPoints = 8F;
			appearance14.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridBand3.Override.RowAlternateAppearance = appearance14;
			appearance15.BackColor = System.Drawing.Color.White;
			appearance15.FontData.Name = "Verdana";
			appearance15.FontData.SizeInPoints = 8F;
			appearance15.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance15.TextHAlign = Infragistics.Win.HAlign.Left;
			ultraGridBand3.Override.RowAppearance = appearance15;
			this.grdSortProfile.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
			this.grdSortProfile.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
			appearance16.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(128)), ((System.Byte)(255)));
			appearance16.FontData.Name = "Verdana";
			appearance16.FontData.SizeInPoints = 8F;
			appearance16.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance16.TextHAlign = Infragistics.Win.HAlign.Left;
			this.grdSortProfile.DisplayLayout.CaptionAppearance = appearance16;
			this.grdSortProfile.DisplayLayout.GroupByBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			this.grdSortProfile.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			this.grdSortProfile.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
			this.grdSortProfile.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
			this.grdSortProfile.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None;
			this.grdSortProfile.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.None;
			this.grdSortProfile.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
			this.grdSortProfile.DisplayLayout.Override.CellMultiLine = Infragistics.Win.DefaultableBoolean.False;
			this.grdSortProfile.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
			this.grdSortProfile.DisplayLayout.Override.MaxSelectedCells = 1;
			this.grdSortProfile.DisplayLayout.Override.MaxSelectedRows = 1;
			this.grdSortProfile.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			this.grdSortProfile.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
			this.grdSortProfile.Location = new System.Drawing.Point(9, 32);
			this.grdSortProfile.Name = "grdSortProfile";
			this.grdSortProfile.Size = new System.Drawing.Size(525, 244);
			this.grdSortProfile.TabIndex = 0;
			this.grdSortProfile.AfterRowUpdate += new Infragistics.Win.UltraWinGrid.RowEventHandler(this.OnSortProfileRowUpdated);
			this.grdSortProfile.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.OnSortProfileCellUpdated);
			this.grdSortProfile.BeforeCellActivate += new Infragistics.Win.UltraWinGrid.CancelableCellEventHandler(this.OnBeforeSortProfileCellActivated);
			this.grdSortProfile.AfterCellUpdate += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.OnSortProfileCellUpdated);
			// 
			// mSortProfileDS
			// 
			this.mSortProfileDS.DataSetName = "SortProfileDS";
			this.mSortProfileDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// cboIBLabel
			// 
			this.cboIBLabel.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboIBLabel.Location = new System.Drawing.Point(96, 15);
			this.cboIBLabel.Name = "cboIBLabel";
			this.cboIBLabel.Size = new System.Drawing.Size(129, 22);
			this.cboIBLabel.TabIndex = 1;
			this.cboIBLabel.Text = null;
			this.cboIBLabel.Visible = false;
			// 
			// grpVendorLocation
			// 
			this.grpVendorLocation.Controls.Add(this.lblAddress);
			this.grpVendorLocation.Controls.Add(this.cboVendorLoc);
			this.grpVendorLocation.Controls.Add(this.cboVendor);
			this.grpVendorLocation.Controls.Add(this._lblAddress);
			this.grpVendorLocation.Controls.Add(this._lblLocation);
			this.grpVendorLocation.Controls.Add(this._lblVendor);
			this.grpVendorLocation.Location = new System.Drawing.Point(6, 39);
			this.grpVendorLocation.Name = "grpVendorLocation";
			this.grpVendorLocation.Size = new System.Drawing.Size(336, 128);
			this.grpVendorLocation.TabIndex = 1;
			this.grpVendorLocation.TabStop = false;
			this.grpVendorLocation.Text = "Vendor Location";
			// 
			// lblAddress
			// 
			this.lblAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblAddress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblAddress.Location = new System.Drawing.Point(88, 78);
			this.lblAddress.Name = "lblAddress";
			this.lblAddress.Size = new System.Drawing.Size(240, 42);
			this.lblAddress.TabIndex = 2;
			// 
			// cboVendorLoc
			// 
			this.cboVendorLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cboVendorLoc.DataSource = this.mLocationsDS;
			this.cboVendorLoc.DisplayMember = "LocationListTable.Description";
			this.cboVendorLoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboVendorLoc.Location = new System.Drawing.Point(88, 54);
			this.cboVendorLoc.Name = "cboVendorLoc";
			this.cboVendorLoc.Size = new System.Drawing.Size(240, 21);
			this.cboVendorLoc.TabIndex = 1;
			this.cboVendorLoc.ValueMember = "LocationListTable.LocationID";
			this.cboVendorLoc.SelectionChangeCommitted += new System.EventHandler(this.OnVendorLocationChanged);
			// 
			// mLocationsDS
			// 
			this.mLocationsDS.DataSetName = "LocationDS";
			this.mLocationsDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// cboVendor
			// 
			this.cboVendor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cboVendor.DataSource = this.mVendorsDS;
			this.cboVendor.DisplayMember = "SelectionListTable.Description";
			this.cboVendor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboVendor.Location = new System.Drawing.Point(88, 30);
			this.cboVendor.Name = "cboVendor";
			this.cboVendor.Size = new System.Drawing.Size(240, 21);
			this.cboVendor.TabIndex = 0;
			this.cboVendor.ValueMember = "SelectionListTable.ID";
			this.cboVendor.SelectionChangeCommitted += new System.EventHandler(this.OnVendorChanged);
			// 
			// _lblAddress
			// 
			this._lblAddress.Location = new System.Drawing.Point(8, 72);
			this._lblAddress.Name = "_lblAddress";
			this._lblAddress.Size = new System.Drawing.Size(72, 18);
			this._lblAddress.TabIndex = 9;
			this._lblAddress.Text = "Address";
			this._lblAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblLocation
			// 
			this._lblLocation.Location = new System.Drawing.Point(8, 48);
			this._lblLocation.Name = "_lblLocation";
			this._lblLocation.Size = new System.Drawing.Size(72, 18);
			this._lblLocation.TabIndex = 8;
			this._lblLocation.Text = "Location";
			this._lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblVendor
			// 
			this._lblVendor.Location = new System.Drawing.Point(8, 24);
			this._lblVendor.Name = "_lblVendor";
			this._lblVendor.Size = new System.Drawing.Size(72, 18);
			this._lblVendor.TabIndex = 7;
			this._lblVendor.Text = "Vendor";
			this._lblVendor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// mInboundLabelsDS
			// 
			this.mInboundLabelsDS.DataSetName = "SelectionList";
			this.mInboundLabelsDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// dlgClientVendorDetail
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(570, 551);
			this.Controls.Add(this.tabDialog);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgClientVendorDetail";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Client-Vendor Location Association Details";
			this.Load += new System.EventHandler(this.OnFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.mVendorsDS)).EndInit();
			this.fraCVNumbers.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdCVNumbers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mAssociationDS)).EndInit();
			this.tabDialog.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mClientsDS)).EndInit();
			this.grpFSA.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdSortProfile)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mSortProfileDS)).EndInit();
			this.grpVendorLocation.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mLocationsDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mInboundLabelsDS)).EndInit();
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
				
				#region Set default grid behavior
				this.grdCVNumbers.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
				this.grdCVNumbers.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.True;
				//this.grdCVNumbers.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.Edit;
				//this.grdCVNumbers.DisplayLayout.Bands[0].Columns["LinkID"].CellActivation = Activation.Disabled;
				//this.grdCVNumbers.DisplayLayout.Bands[0].Columns["Number"].CellActivation = Activation.AllowEdit;
				this.grdSortProfile.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
				this.grdSortProfile.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.True;
				this.grdSortProfile.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.Edit;
				this.grdSortProfile.DisplayLayout.Bands[0].Columns["Selected"].CellActivation = Activation.AllowEdit;
				this.grdSortProfile.DisplayLayout.Bands[0].Columns["ProfileID"].CellActivation = Activation.Disabled;
				this.grdSortProfile.DisplayLayout.Bands[0].Columns["LinkID"].CellActivation = Activation.Disabled;
				this.grdSortProfile.DisplayLayout.Bands[0].Columns["LabelID"].CellActivation = Activation.AllowEdit;
				this.grdSortProfile.DisplayLayout.Bands[0].Columns["SortTypeID"].CellActivation = Activation.Disabled;
				this.grdSortProfile.DisplayLayout.Bands[0].Columns["SortType"].CellActivation = Activation.Disabled;
				this.grdSortProfile.DisplayLayout.Bands[0].Columns["IsActive"].CellActivation = (this.mParentIsActive) ? Activation.AllowEdit : Activation.Disabled;
				this.grdSortProfile.DisplayLayout.Bands[0].Columns["LabelID"].EditorControl = this.cboIBLabel;
				this.grdSortProfile.DisplayLayout.Bands[1].Override.AllowUpdate = DefaultableBoolean.True;
				this.grdSortProfile.DisplayLayout.Bands[1].Override.CellClickAction = CellClickAction.Edit;
				this.grdSortProfile.DisplayLayout.Bands[1].Columns["Selected"].CellActivation = Activation.AllowEdit;
				this.grdSortProfile.DisplayLayout.Bands[1].Columns["ProfileID"].CellActivation = Activation.Disabled;
				this.grdSortProfile.DisplayLayout.Bands[1].Columns["TerminalID"].CellActivation = Activation.Disabled;
				this.grdSortProfile.DisplayLayout.Bands[1].Columns["Terminal"].CellActivation = Activation.Disabled;
				#endregion
				//CVLocationDS
				if(this.cboClient.Items.Count>0) this.cboClient.SelectedValue = this.mClientID;
				this.cboClient.Enabled = false;
				
				//On edit vendor is not in GetAvailableVendors(); build the vendor row from mAssociationDS
				this.mVendorsDS.Clear();
				if(this.mAssocID>0) 
					this.mVendorsDS.SelectionListTable.AddSelectionListTableRow(this.mAssociationDS.CVLocationDetailTable[0].VendorID.ToString(), this.mAssociationDS.CVLocationDetailTable[0].VendorName);
				else 
					this.mVendorsDS.Merge(EnterpriseFactory.GetAvailableVendors(this.mClientID));
				if(!this.mAssociationDS.CVLocationDetailTable[0].IsVendorIDNull())
					this.cboVendor.SelectedValue = this.mAssociationDS.CVLocationDetailTable[0].VendorID;
				else
					if(this.cboVendor.Items.Count>0) this.cboVendor.SelectedIndex = 0;
				this.cboVendor.Enabled = (this.cboVendor.Items.Count>0 && this.mAssocID==0);
				OnVendorChanged(null, null);
				
				//CV Numbers- reset grid binding (InitializeComponent() sets binding to the new instance
				//created by the designer- required for named grid columns)
				this.grdCVNumbers.DataSource = this.mAssociationDS;
				
				//Sort Profile- get template; copy data from embedded mAssociationDS.CVLocationSortProfileTable 
				//into its corresponding row in SortProfileDS.SortProfileTable template
				this.mSortProfileDS.Merge(EnterpriseFactory.GetCVLocationSortProfileTemplate(this.mClientID, this.mAssocID));
				for(int i=0; i<this.mAssociationDS.CVLocationDetailTable[0].GetCVLocationSortProfileTableRows().Length; i++) {
					//Find the corresponding sort profile template row by sort type id (unique for each row) 
					CVLocationDS.CVLocationSortProfileTableRow rowProfile = this.mAssociationDS.CVLocationDetailTable[0].GetCVLocationSortProfileTableRows()[i];
					int sortTypeID = rowProfile.SortTypeID;
					SortProfileDS.SortProfileTableRow _rowProfile = (SortProfileDS.SortProfileTableRow)this.mSortProfileDS.SortProfileTable.Select("SortTypeID=" + sortTypeID.ToString())[0];
					if(_rowProfile!=null) {
						_rowProfile.Selected = true;
						_rowProfile.ProfileID = rowProfile.ProfileID;
						_rowProfile.LinkID = rowProfile.LinkID;
						_rowProfile.LabelID = rowProfile.LabelID;
						_rowProfile.SortTypeID = sortTypeID;
						_rowProfile.SortType = rowProfile.SortType;
						_rowProfile.IsElectronic = rowProfile.IsElectronic;
						_rowProfile.ManifestPerTrailer = rowProfile.ManifestPerTrailer;
						_rowProfile.IsActive = rowProfile.IsActive;
						_rowProfile.LastUpdated = rowProfile.LastUpdated;
						_rowProfile.UserID = rowProfile.UserID;
						_rowProfile.RowVersion = rowProfile.RowVersion;
						
						//Copy embedded terminal rows
						for(int j=0; j<rowProfile.GetCVLocationSortProfileTerminalTableRows().Length; j++) {
							//Find the corresponding terminal template row by terminal id (unique for each profile 
							//row) and copy the data into the template
							CVLocationDS.CVLocationSortProfileTerminalTableRow rowTerminal = rowProfile.GetCVLocationSortProfileTerminalTableRows()[j];
							int iTerminalID = rowTerminal.TerminalID;
							SortProfileDS.SortProfileTerminalTableRow _rowTerminal = null;	//(SortProfileDS.SortProfileTerminalTableRow)this.mSortProfileDS.SortProfileTerminalTable.Select("TerminalID=" + iTerminalID.ToString())[0];
							for(int k=0; k<_rowProfile.GetSortProfileTerminalTableRows().Length; k++) {
								//Search (loop) for the correct row
								_rowTerminal = _rowProfile.GetSortProfileTerminalTableRows()[k];
								if(_rowTerminal.TerminalID==iTerminalID) {
									_rowTerminal.Selected = true;
									_rowTerminal.ProfileID = rowTerminal.ProfileID;
									_rowTerminal.TerminalID = iTerminalID;
									_rowTerminal.Terminal = rowTerminal.Terminal;
									break;
								}
							}
						}
					}
				}
				this.chkStatus.Checked = this.mAssociationDS.CVLocationDetailTable[0].IsActive;
				if(!mParentIsActive) {
					//If parent is inactive: 1. Status MUST be inactive for new
					//					     2. Status cannot be changed for new or existing
					if(this.mAssocID==0) this.chkStatus.Checked = false;
					this.chkStatus.Enabled = false;
				}
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.btnOk.Enabled = false; this.Cursor = Cursors.Default; }
		}
		private void OnVendorChanged(object sender, System.EventArgs e) {
			//Event handler for change in vendor company
			int vendorID=0;
			CVLocationDS.CVLocationDetailTableRow rowCVL;
			WarehouseDS lst=null;
			WarehouseDS.AddressDetailTableRow rowAdd;
			bool bOriginalVendor;
			try {
				//Update vendor locations
				vendorID = Convert.ToInt32(this.cboVendor.SelectedValue);
				this.mLocationsDS.Clear();
				if(this.mAssocID>0) {
					rowCVL = this.mAssociationDS.CVLocationDetailTable[0];
					lst = new WarehouseDS();
					lst = EnterpriseFactory.GetWarehouse(this.mClientID, vendorID);
					rowAdd = lst.AddressDetailTable[0];
					this.mLocationsDS.LocationListTable.AddLocationListTableRow(rowCVL.LocationID, this.mClientID, rowCVL.Number, "Vendor", rowCVL.Description, rowAdd.AddressLine1, rowAdd.AddressLine2, rowAdd.City, rowAdd.StateOrProvince, rowAdd.PostalCode, "", DateTime.Now, DateTime.Now, "");
				}
				else 
					this.mLocationsDS.Merge(EnterpriseFactory.GetAvailableVendorLocations(this.mClientID, vendorID));
				
				//Select first in list UNLESS this is the original vendor in which case use original location
				bOriginalVendor=false;
				if(!this.mAssociationDS.CVLocationDetailTable[0].IsVendorIDNull())
					bOriginalVendor = (vendorID==this.mAssociationDS.CVLocationDetailTable[0].VendorID);
				if(bOriginalVendor && !this.mAssociationDS.CVLocationDetailTable[0].IsLocationIDNull()) 
					this.cboVendorLoc.SelectedValue = this.mAssociationDS.CVLocationDetailTable[0].LocationID;
				else
					if(this.cboVendorLoc.Items.Count>0) this.cboVendorLoc.SelectedIndex = 0;
				this.cboVendorLoc.Enabled = (this.cboVendorLoc.Items.Count>0 && this.mAssocID==0);
				OnVendorLocationChanged(null, null);
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnVendorLocationChanged(object sender, System.EventArgs e) {
			//Event handler for change in vendor location (warehouse)
			try {
				//Update vendor location address
				//TODO: Determine index
				string sAddress = this.mLocationsDS.LocationListTable[0].AddressLine1.Trim() + "\n";
				if(!this.mLocationsDS.LocationListTable[0].IsAddressLine2Null())
					if(this.mLocationsDS.LocationListTable[0].AddressLine2!="")
						sAddress += this.mLocationsDS.LocationListTable[0].AddressLine2.Trim() + "\n";
				sAddress += this.mLocationsDS.LocationListTable[0].City.Trim() + ", " + this.mLocationsDS.LocationListTable[0].StateOrProvince.Trim() + " " + this.mLocationsDS.LocationListTable[0].PostalCode.Trim();
				this.lblAddress.Text = sAddress;
				ValidateForm(null, null);
			} 
			catch(Exception ex) { reportError(ex); }
		}
		#region CVNumbers: OnBeforeCVNumberCellActivated(), OnCVNumberCellUpdated(), OnCVNumberRowUpdated(), OnGridSelectionChanged(), OnGridMouseDown(), SetGridRowActive()
		private void OnBeforeCVNumberCellActivated(object sender, Infragistics.Win.UltraWinGrid.CancelableCellEventArgs e) {
			//Event handler for cv number cell activated
			//Debug.Write("OnBeforeCVNumberCellActivated()\n");
			int iLinkID=0;
			try {
				iLinkID = this.mAssociationDS.CVLocationDetailTable[0].LinkID;
				switch(e.Cell.Column.Key.ToString()) {
					case "Number":	e.Cell.Activation = Activation.AllowEdit; break;
					default:		e.Cell.Activation = Activation.NoEdit; break;
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnCVNumberCellUpdated(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e) {
			//Event handler for change in a cv number cell value
			try {
				//Validate cell value
				switch(e.Cell.Column.Key.ToString()) {
					case "Number":	if(e.Cell.Text=="") e.Cell.Value = e.Cell.OriginalValue; break;
					default:		break;
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnCVNumberRowUpdated(object sender, Infragistics.Win.UltraWinGrid.RowEventArgs e) {
			//Event handler for change in a cv number row
			try {
				//Validate row
				ValidateForm(null, null);
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnGridSelectionChanged(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for after selection changes
			//This event will cover mouse left-click and keyboard changes
			try  {
				//Set menu and toolbar sevices for selected row
				UltraGrid grd = (UltraGrid)sender;
				if(Convert.ToString(grd.Tag)==GRID_TYPE_NUMBERS) 
					setUserServices(true, GRID_TYPE_NUMBERS);
			}
			catch(Exception ex) { reportError(ex); }
		}
		private void OnGridMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Event handler for mouse down event
			bool isItem = false;
			try {
				//Set menu and toolbar services
				UltraGrid grd = (UltraGrid)sender;
				if(Convert.ToString(grd.Tag)==GRID_TYPE_NUMBERS) {
					isItem = SetGridRowActive(e, GRID_TYPE_NUMBERS);
					setUserServices(isItem, GRID_TYPE_NUMBERS);
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private bool SetGridRowActive(System.Windows.Forms.MouseEventArgs e, string type) {
			//Set the active row in a grid
			bool isItem = false;
			object oContext = null;
			UltraGridCell oCell;
			UIElement infraUIElement;
			UltraGrid formGrid = null;
			try {
				//Set active grid row per right mouse
				if(type==GRID_TYPE_NUMBERS) 
					formGrid = this.grdCVNumbers;
				infraUIElement = formGrid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));
				oContext = infraUIElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridCell));
				if (oContext != null) {
					oCell = (Infragistics.Win.UltraWinGrid.UltraGridCell)oContext;
					formGrid.ActiveRow = oCell.Row;
					formGrid.ActiveRow.Selected = true;
					isItem = true;
				}
				else {
					//This happens when you click on the divider area between rows and cells;
					//if the active row changes, make sure it's also selected
					if(formGrid.ActiveRow!=null)
						formGrid.ActiveRow.Selected = true;
					isItem = false;
				}
			}
			catch(Exception ex) { reportError(ex); }
			return isItem;
		}
		#endregion
		#region Sort Profile: OnBeforeSortProfileCellActivated(), OnSortProfileCellUpdated(), OnSortProfileRowUpdated()
		private void OnBeforeSortProfileCellActivated(object sender, Infragistics.Win.UltraWinGrid.CancelableCellEventArgs e) {
			//Event handler for sort profile cell activated
			Debug.Write("OnBeforeSortProfileCellActivated()\n");
			bool bSelected=true;
			int iProfileID=0, sortTypeID=0, iLabelID=0;
			try {
				//1. Cannot unselect an originally selected row
				//2. Cannot edit row cells unless row is selected
				bSelected = Convert.ToBoolean(e.Cell.Row.Cells["Selected"].Value);
				iProfileID = Convert.ToInt32(e.Cell.Row.Cells["ProfileID"].Value);
				switch(e.Cell.Column.Key) {
					case "Selected":
						//Set activation; also, set labelID to a valid value if selected
						if(e.Cell.Band.Key=="SortProfileTable") 
							e.Cell.Activation = (bSelected && iProfileID>0) ? Activation.NoEdit : Activation.AllowEdit;
						if(e.Cell.Band.Key=="SortProfileTable_SortProfileTerminalTable")
							e.Cell.Activation = Activation.AllowEdit;
						break;
					case "LabelID":	
						if(bSelected) {
							//Get valid label selections for sort type and re-populate combobox
							sortTypeID = Convert.ToInt32(e.Cell.Row.Cells["SortTypeID"].Value);
							iLabelID = Convert.ToInt32(e.Cell.Row.Cells["LabelID"].Value);
							this.cboIBLabel.Items.Clear();
							this.mInboundLabelsDS.Clear();
							this.mInboundLabelsDS.Merge(EnterpriseFactory.GetInboundLabels(sortTypeID));
							for(int i=0; i<this.mInboundLabelsDS.SelectionListTable.Rows.Count; i++) {
								this.cboIBLabel.Items.Add(this.mInboundLabelsDS.SelectionListTable[i].ID, this.mInboundLabelsDS.SelectionListTable[i].Description);
							}
							this.cboIBLabel.Enabled = (this.cboIBLabel.Items.Count>0);
							e.Cell.Activation = Activation.AllowEdit;
						}
						else
							e.Cell.Activation = Activation.NoEdit; 
						break;
					default:			
						e.Cell.Activation = bSelected ? Activation.AllowEdit : Activation.NoEdit; 
						break;
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnSortProfileCellUpdated(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e) {
			//Event handler for change in a sort profile cell value
			Debug.Write("OnSortProfileCellUpdated()\n");
			bool bSelected=true;
			int sortTypeID=0;
			try {
				//Validate cell value
				//bSelected = Convert.ToBoolean(e.Cell.Row.Cells["Selected"].Value);
				switch(e.Cell.Column.Key) {
					case "Selected":
						//Set activation; also, set labelID to a valid value if selected
						bSelected = Convert.ToBoolean(e.Cell.Value);
						if(e.Cell.Band.Key=="SortProfileTable") {
							if(bSelected) {
								sortTypeID = Convert.ToInt32(e.Cell.Row.Cells["SortTypeID"].Value);
								this.mInboundLabelsDS.Clear();
								this.mInboundLabelsDS.Merge(EnterpriseFactory.GetInboundLabels(sortTypeID));
								e.Cell.Row.Cells["LabelID"].Value = mInboundLabelsDS.SelectionListTable[0].ID;
							}
							else
								e.Cell.Row.Cells["LabelID"].Value = 0;
						}
						ValidateForm(null, null);
						break;
					case "LabelID":		
						Debug.Write("LabelID cell=" + e.Cell.Value.ToString() + "\n");
						//e.Cell.Value = this.cboIBLabel.Value;
						break;
					case "ManifestPerTrailer":
						//Input 'Y' or 'N' or restore original value
						if(e.Cell.Text!="Y" && e.Cell.Text!="N")
							e.Cell.Value = e.Cell.OriginalValue;
							break;
					default: break;
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnSortProfileRowUpdated(object sender, Infragistics.Win.UltraWinGrid.RowEventArgs e) {
			//Event handler for change in a sort profile row
			ValidateForm(null, null);
		}
		#endregion
		#region User Services: ValidateForm(), OnCmdClick(), OnMenuClick()
		private void ValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes to control data
			bool bSortProfilesValid=false, bSelected=false, bSelectedT=false;
			try {
				if(this.mAssociationDS.CVLocationDetailTable.Count>0) {
					//Validate at least one sort profile is selected, and at least one terminal is selected
					bSortProfilesValid=false;
					for(int i=0; i<this.grdSortProfile.Rows.Count; i++) {
						bSelected = Convert.ToBoolean(this.grdSortProfile.Rows[i].Cells["Selected"].Value);
						if(bSelected) {
							//Validate at least one terminal
							for(int j=0; j<this.grdSortProfile.Rows[i].ChildBands["SortProfileTable_SortProfileTerminalTable"].Rows.Count; j++) {
								bSelectedT = Convert.ToBoolean(this.grdSortProfile.Rows[i].ChildBands["SortProfileTable_SortProfileTerminalTable"].Rows[j].Cells["Selected"].Value);
								if(bSelectedT) {
									bSortProfilesValid = true;
									break;
								}
							}
						}
					}
					this.btnOk.Enabled = (this.cboClient.Text!="" && this.cboVendorLoc.Text!="" && bSortProfilesValid);
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnCmdClick(object sender, System.EventArgs e) {
			//Command button handler
			int iLinkID=0;
			bool bSelected=false, bHasProfileID=false;
			bool bSelectedT=false, bHasProfileIDT=false;
			CVLocationDS.CVLocationSortProfileTableRow rowProfile;
			CVLocationDS.CVLocationSortProfileTerminalTableRow rowTerminal;
			SortProfileDS.SortProfileTableRow _rowProfile;
			SortProfileDS.SortProfileTerminalTableRow _rowTerminal;
			try {
				Button btn = (Button)sender;
				switch(btn.Text) {
					case CMD_ADD:
						this.ctxNumberAdd.PerformClick(); break;
					case CMD_EDIT:
						this.ctxNumberEdit.PerformClick(); break;
					case CMD_REMOVE:
						this.ctxNumberRemove.PerformClick(); break;
					case CMD_CANCEL:
						//Close the dialog
						this.DialogResult = DialogResult.Cancel;
						this.Close();
						break;
					case CMD_OK:
						this.Cursor = Cursors.WaitCursor;
						//Update CVLocation details with control values
						this.mAssociationDS.CVLocationDetailTable[0].VendorID = Convert.ToInt32(this.cboVendor.SelectedValue);
						this.mAssociationDS.CVLocationDetailTable[0].VendorName = this.cboVendor.Text;
						this.mAssociationDS.CVLocationDetailTable[0].LocationID = Convert.ToInt32(this.cboVendorLoc.SelectedValue);
						this.mAssociationDS.CVLocationDetailTable[0].Number = this.cboVendorLoc.Text;
						
						//CV numbers- updated by grid binding
						#region Test update to mAssociationDS.CVLocationNumberTable
						Debug.Write("\n");
						for(int i=0; i<this.mAssociationDS.CVLocationNumberTable.Rows.Count; i++) {
							switch(this.mAssociationDS.CVLocationNumberTable[i].RowState) {
								case		DataRowState.Added: Debug.Write("Added cv number (number=)\n"); break;
								case		DataRowState.Modified: Debug.Write("Modified cv number (number=)\n"); break;
								case		DataRowState.Deleted: Debug.Write("Removed cv number (number=)\n"); break;
								default:	Debug.Write(this.mAssociationDS.CVLocationNumberTable[i].RowState.ToString() + " cv number (number=)\n"); break;
							}
						}
						#endregion
						
						//Sort profiles
						iLinkID = this.mAssociationDS.CVLocationDetailTable[0].LinkID;
						for(int i=0; i<this.mSortProfileDS.SortProfileTable.Rows.Count; i++) {
							_rowProfile = this.mSortProfileDS.SortProfileTable[i];
							bSelected = _rowProfile.Selected;
							bHasProfileID = (_rowProfile.ProfileID>0) ? true : false;
							if(bSelected && !bHasProfileID) {
								//Add a new sort profile
								//*** RESULT MUST MARK ROW AS DataRowState.Added
								rowProfile = this.mAssociationDS.CVLocationSortProfileTable.AddCVLocationSortProfileTableRow(
									_rowProfile.ProfileID, _rowProfile.LinkID, _rowProfile.LabelID, _rowProfile.SortType, _rowProfile.SortTypeID, _rowProfile.IsElectronic, _rowProfile.ManifestPerTrailer, 
									_rowProfile.IsActive, _rowProfile.LastUpdated, _rowProfile.UserID, _rowProfile.RowVersion, 
									this.mAssociationDS.CVLocationDetailTable[0]);
								for(int j=0; j<_rowProfile.GetSortProfileTerminalTableRows().Length; j++) {
									//Add terminal rows to rowProfile
									_rowTerminal = _rowProfile.GetSortProfileTerminalTableRows()[j];
									if(_rowTerminal.Selected)
										this.mAssociationDS.CVLocationSortProfileTerminalTable.AddCVLocationSortProfileTerminalTableRow(_rowProfile.ProfileID, _rowTerminal.TerminalID, _rowTerminal.Terminal, rowProfile);
								}
							}
							else if(bSelected && bHasProfileID) {
								//Existing sort profile- update as required
								rowProfile = (CVLocationDS.CVLocationSortProfileTableRow)this.mAssociationDS.CVLocationSortProfileTable.Select("ProfileID=" + this.mSortProfileDS.SortProfileTable[i].ProfileID)[0];
								//rowProfile.LinkID = N/A;
								//rowProfile.ProfileID = N/A;
								rowProfile.LabelID = _rowProfile.LabelID;
								//rowProfile.SortType = N/A;
								//rowProfile.SortTypeID = N/A;
								rowProfile.IsElectronic = _rowProfile.IsElectronic;
								rowProfile.ManifestPerTrailer = _rowProfile.ManifestPerTrailer;
								rowProfile.IsActive = _rowProfile.IsActive;
								rowProfile.LastUpdated = _rowProfile.LastUpdated;
								rowProfile.UserID = _rowProfile.UserID;
								rowProfile.RowVersion = _rowProfile.RowVersion;
								for(int j=0; j<_rowProfile.GetSortProfileTerminalTableRows().Length; j++) {
									//Edit terminal rows in rowProfile
									_rowTerminal = _rowProfile.GetSortProfileTerminalTableRows()[j];
									bSelectedT = _rowTerminal.Selected;
									bHasProfileIDT = (_rowTerminal.ProfileID>0) ? true : false;
									if(bSelectedT && !bHasProfileIDT) {
										//Add a new sort profile terminal
										//*** RESULT MUST MARK ROW AS DataRowState.Added
										this.mAssociationDS.CVLocationSortProfileTerminalTable.AddCVLocationSortProfileTerminalTableRow(
											_rowTerminal.ProfileID, _rowTerminal.TerminalID, _rowTerminal.Terminal, rowProfile);
									}
									else if(!bSelectedT && bHasProfileIDT) {
										//Remove current sort profile terminal - N/A
										//*** RESULT MUST MARK ROW AS DataRowState.Deleted
										rowTerminal = (CVLocationDS.CVLocationSortProfileTerminalTableRow)this.mAssociationDS.CVLocationSortProfileTerminalTable.Select("ProfileID=" + _rowTerminal.ProfileID + " AND TerminalID=" + _rowTerminal.TerminalID)[0];
										rowTerminal.Delete();
									}
								}
							}
							else if(!bSelected && bHasProfileID) {
								//Remove current sort profile - N/A
								//*** RESULT MUST MARK ROW AS DataRowState.Deleted
							}
						}
						#region Test update to mAssociationDS.CVLocationSortProfileTable
						Debug.Write("\n");
						for(int i=0; i<this.mAssociationDS.CVLocationSortProfileTable.Rows.Count; i++) {
							switch(this.mAssociationDS.CVLocationSortProfileTable[i].RowState) {
								case		DataRowState.Added: Debug.Write("Added sort profile (profileID=)\n"); break;
								case		DataRowState.Modified: Debug.Write("Modified sort profile (profileID=)\n"); break;
								case		DataRowState.Deleted: Debug.Write("Removed sort profile (profileID=)\n"); break;
								default:	Debug.Write(this.mAssociationDS.CVLocationSortProfileTable[i].RowState.ToString() + " sort profile (profileID=)\n"); break;
							}
						}
						#endregion
						this.mAssociationDS.CVLocationDetailTable[0].IsActive = this.chkStatus.Checked;
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
			string sNumber="";
			dlgInputBox dlgInput;
			DialogResult res;
			try  {
				MenuItem menu = (MenuItem)sender;
				switch(menu.Text)  {
					case MNU_ADD:
						//Delete an existing number in the list
						sNumber = "";
						dlgInput = new dlgInputBox("Add a new client-vendor number.", "", "Client-Vendor Number");
						res = dlgInput.ShowDialog();
						if(res==DialogResult.OK) 
							this.mAssociationDS.CVLocationNumberTable.AddCVLocationNumberTableRow(0, dlgInput.Value, this.mAssociationDS.CVLocationDetailTable[0]);
						break;
					case MNU_EDIT:
						//Delete an existing number in the list
						sNumber = this.grdCVNumbers.Selected.Rows[0].Cells["Number"].Value.ToString();
						dlgInput = new dlgInputBox("Edit exisiting client-vendor number.", sNumber, "Client-Vendor Number");
						res = dlgInput.ShowDialog();
						if(res==DialogResult.OK) 
							this.grdCVNumbers.Selected.Rows[0].Cells["Number"].Value = dlgInput.Value;
						break;
					case MNU_REMOVE:
						//Delete an existing client-vendor number from the list
						sNumber = this.grdCVNumbers.Selected.Rows[0].Cells["Number"].Value.ToString();
						res = MessageBox.Show(this, "Delete client-vendor number " + sNumber + "?", this.Name, MessageBoxButtons.OKCancel);
						if(res==DialogResult.OK) 
							this.grdCVNumbers.Selected.Rows[0].Delete();
						break;				}
			}
			catch(Exception ex) { reportError(ex); }
		}
		#endregion
		#region Local Services: setUserServices(), reportError()
		private void setUserServices(bool isItem, string type) {
			//Set user services depending upon an item selected in the grid
			try  {
				//Set main menu, context menu, and toolbar states
				if(type==GRID_TYPE_NUMBERS) {
					this.ctxNumberAdd.Enabled = this.btnNumberAdd.Enabled = !isItem;
					this.ctxNumberEdit.Enabled = this.btnNumberEdit.Enabled = isItem;
					this.ctxNumberRemove.Enabled = this.btnNumberRemove.Enabled = isItem;
				}
			}
			catch(Exception ex) { reportError(ex); }
		}
		private void reportError(Exception ex) { reportError(ex, "", "", ""); }
		private void reportError(Exception ex, string keyword1, string keyword2, string keyword3) { 
			if(this.ErrorMessage != null) this.ErrorMessage(this, new ErrorEventArgs(ex,keyword1,keyword2,keyword3));
		}
		#endregion
	}
}
