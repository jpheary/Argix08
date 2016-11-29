//	File:	dlgfreightpath.cs
//	Author:	J. Heary
//	Date:	05/01/06
//	Desc:	Dialog to create a new or edit an existing Freight Path.
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
	public class dlgFreightPathDetail : System.Windows.Forms.Form {
		//Members
		private object mLocationModel=null;
		private string mPathID="";
		#region Controls
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.CheckBox chkStatus;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.TabControl tabDialog;
		private System.Windows.Forms.ComboBox cboSortCenters;
		private Tsort.Windows.SelectionList mSortCentersDS;
		private System.Windows.Forms.Label _lblSortCenters;
		private System.Windows.Forms.GroupBox fraDetails;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.Label _lblDescription;
		private Tsort.Enterprise.FreightPathDS mFreightPathDetailDS;
		private System.Windows.Forms.GroupBox fraFreightStops;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdStops;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.Button btnDown;
		//private LocationControl.LocationSelector locStop;
		private Tsort.Enterprise.FreightPathDS m_dsFPStops;
		private System.Windows.Forms.Label _lblFreightPathType;
		private System.Windows.Forms.ComboBox cboFPType;
		private Tsort.Windows.SelectionList mFPTypesDS;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
				
		//Constants
		private const string CMD_CANCEL = "&Cancel";
		private const string CMD_OK = "O&K";
		private const string CMD_STOP_ADD = ">>";
		private const string CMD_STOP_REM = "<<";
		private const string CMD_STOP_MOVEUP = "+";
		private const string CMD_STOP_MOVEDOWN = "-";
		private const string PATHTYPE_INTERNAL = "internal";
		private const string PATHTYPE_EXTERNAL = "external";
		
		//Events
		public event ErrorEventHandler ErrorMessage=null;
		
		//Interface
		public dlgFreightPathDetail(ref FreightPathDS freightPath) {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();
				this.btnOk.Text = CMD_OK;
				this.btnCancel.Text = CMD_CANCEL;
				this.btnAdd.Text = CMD_STOP_ADD;
				this.btnRemove.Text = CMD_STOP_REM;
				this.btnUp.Text = CMD_STOP_MOVEUP;
				this.btnDown.Text = CMD_STOP_MOVEDOWN;
				
				//Set mediator service, data, and titlebar caption
				this.mLocationModel = null;	//new LocationModel(true, this.m_oMediator);
				//this.locStop.InitializeLocation(this.mLocationModel);
				this.mFreightPathDetailDS = freightPath;
				if(this.mFreightPathDetailDS.FreightPathDetailTable.Count>0) {
					this.mPathID = this.mFreightPathDetailDS.FreightPathDetailTable[0].PathID;
					this.Text = (this.mPathID!="") ? "Freight Path (" + this.mPathID + ")" : "Freight Path (New)";
				}
				else
					this.Text = "Freight Path (Data Unavailable)";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgFreightPathDetail));
			Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FreightPathStopDetailTable", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PathStopID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PathID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopNumber");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LocationType");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CompanyID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CompanyName");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LocationID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Number");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Mnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Address");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsFinal");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UseMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UseMnemonic2");
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			this.cboSortCenters = new System.Windows.Forms.ComboBox();
			this.mSortCentersDS = new Tsort.Windows.SelectionList();
			this._lblSortCenters = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.fraDetails = new System.Windows.Forms.GroupBox();
			this._lblFreightPathType = new System.Windows.Forms.Label();
			this.cboFPType = new System.Windows.Forms.ComboBox();
			this.mFPTypesDS = new Tsort.Windows.SelectionList();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this._lblDescription = new System.Windows.Forms.Label();
			this.chkStatus = new System.Windows.Forms.CheckBox();
			this.tabDialog = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.fraFreightStops = new System.Windows.Forms.GroupBox();
			this.btnDown = new System.Windows.Forms.Button();
			this.btnUp = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			//this.locStop = new LocationControl.LocationSelector();
			this.grdStops = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.m_dsFPStops = new Tsort.Enterprise.FreightPathDS();
			this.mFreightPathDetailDS = new Tsort.Enterprise.FreightPathDS();
			((System.ComponentModel.ISupportInitialize)(this.mSortCentersDS)).BeginInit();
			this.fraDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mFPTypesDS)).BeginInit();
			this.tabDialog.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.fraFreightStops.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdStops)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_dsFPStops)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mFreightPathDetailDS)).BeginInit();
			this.SuspendLayout();
			// 
			// cboSortCenters
			// 
			this.cboSortCenters.DataSource = this.mSortCentersDS;
			this.cboSortCenters.DisplayMember = "SelectionListTable.Description";
			this.cboSortCenters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSortCenters.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboSortCenters.Location = new System.Drawing.Point(105, 45);
			this.cboSortCenters.Name = "cboSortCenters";
			this.cboSortCenters.Size = new System.Drawing.Size(192, 21);
			this.cboSortCenters.TabIndex = 1;
			this.cboSortCenters.ValueMember = "SelectionListTable.ID";
			this.cboSortCenters.SelectedIndexChanged += new System.EventHandler(this.ValidateForm);
			// 
			// mSortCentersDS
			// 
			this.mSortCentersDS.DataSetName = "SelectionList";
			this.mSortCentersDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// _lblSortCenters
			// 
			this._lblSortCenters.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblSortCenters.Location = new System.Drawing.Point(3, 45);
			this._lblSortCenters.Name = "_lblSortCenters";
			this._lblSortCenters.Size = new System.Drawing.Size(96, 18);
			this._lblSortCenters.TabIndex = 4;
			this._lblSortCenters.Text = "Sort Center";
			this._lblSortCenters.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnCancel
			// 
			this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(663, 336);
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
			this.btnOk.Location = new System.Drawing.Point(561, 336);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(96, 24);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "&OK";
			this.btnOk.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// fraDetails
			// 
			this.fraDetails.Controls.Add(this._lblFreightPathType);
			this.fraDetails.Controls.Add(this.cboFPType);
			this.fraDetails.Controls.Add(this.txtDescription);
			this.fraDetails.Controls.Add(this._lblDescription);
			this.fraDetails.Controls.Add(this._lblSortCenters);
			this.fraDetails.Controls.Add(this.cboSortCenters);
			this.fraDetails.Location = new System.Drawing.Point(3, 3);
			this.fraDetails.Name = "fraDetails";
			this.fraDetails.Size = new System.Drawing.Size(741, 75);
			this.fraDetails.TabIndex = 0;
			this.fraDetails.TabStop = false;
			this.fraDetails.Text = "Freight Path";
			// 
			// _lblFreightPathType
			// 
			this._lblFreightPathType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblFreightPathType.Location = new System.Drawing.Point(420, 18);
			this._lblFreightPathType.Name = "_lblFreightPathType";
			this._lblFreightPathType.Size = new System.Drawing.Size(129, 18);
			this._lblFreightPathType.TabIndex = 6;
			this._lblFreightPathType.Text = "Freight Path Type";
			this._lblFreightPathType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cboFPType
			// 
			this.cboFPType.DataSource = this.mFPTypesDS;
			this.cboFPType.DisplayMember = "SelectionListTable.Description";
			this.cboFPType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboFPType.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboFPType.Location = new System.Drawing.Point(555, 18);
			this.cboFPType.Name = "cboFPType";
			this.cboFPType.Size = new System.Drawing.Size(144, 21);
			this.cboFPType.TabIndex = 5;
			this.cboFPType.ValueMember = "SelectionListTable.ID";
			this.cboFPType.SelectionChangeCommitted += new System.EventHandler(this.OnFreightPathTypeChanged);
			// 
			// mFPTypesDS
			// 
			this.mFPTypesDS.DataSetName = "SelectionList";
			this.mFPTypesDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// txtDescription
			// 
			this.txtDescription.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtDescription.Location = new System.Drawing.Point(105, 18);
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(288, 21);
			this.txtDescription.TabIndex = 0;
			this.txtDescription.Text = "";
			this.txtDescription.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblDescription
			// 
			this._lblDescription.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblDescription.Location = new System.Drawing.Point(3, 18);
			this._lblDescription.Name = "_lblDescription";
			this._lblDescription.Size = new System.Drawing.Size(96, 18);
			this._lblDescription.TabIndex = 3;
			this._lblDescription.Text = "Description";
			this._lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// chkStatus
			// 
			this.chkStatus.Checked = true;
			this.chkStatus.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkStatus.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkStatus.Location = new System.Drawing.Point(108, 276);
			this.chkStatus.Name = "chkStatus";
			this.chkStatus.Size = new System.Drawing.Size(96, 18);
			this.chkStatus.TabIndex = 2;
			this.chkStatus.Text = "Active";
			this.chkStatus.CheckedChanged += new System.EventHandler(this.ValidateForm);
			// 
			// tabDialog
			// 
			this.tabDialog.Controls.Add(this.tabGeneral);
			this.tabDialog.Location = new System.Drawing.Point(3, 3);
			this.tabDialog.Name = "tabDialog";
			this.tabDialog.SelectedIndex = 0;
			this.tabDialog.Size = new System.Drawing.Size(756, 327);
			this.tabDialog.TabIndex = 1;
			// 
			// tabGeneral
			// 
			this.tabGeneral.Controls.Add(this.fraFreightStops);
			this.tabGeneral.Controls.Add(this.fraDetails);
			this.tabGeneral.Controls.Add(this.chkStatus);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Size = new System.Drawing.Size(748, 301);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			this.tabGeneral.ToolTipText = "General information";
			// 
			// fraFreightStops
			// 
			this.fraFreightStops.Controls.Add(this.btnDown);
			this.fraFreightStops.Controls.Add(this.btnUp);
			this.fraFreightStops.Controls.Add(this.btnRemove);
			this.fraFreightStops.Controls.Add(this.btnAdd);
			//this.fraFreightStops.Controls.Add(this.locStop);
			this.fraFreightStops.Controls.Add(this.grdStops);
			this.fraFreightStops.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.fraFreightStops.Location = new System.Drawing.Point(3, 84);
			this.fraFreightStops.Name = "fraFreightStops";
			this.fraFreightStops.Size = new System.Drawing.Size(741, 183);
			this.fraFreightStops.TabIndex = 1;
			this.fraFreightStops.TabStop = false;
			this.fraFreightStops.Text = "Freight Path Stops";
			// 
			// btnDown
			// 
			this.btnDown.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
			this.btnDown.Location = new System.Drawing.Point(708, 96);
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size(24, 32);
			this.btnDown.TabIndex = 5;
			this.btnDown.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// btnUp
			// 
			this.btnUp.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
			this.btnUp.Location = new System.Drawing.Point(708, 57);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(24, 33);
			this.btnUp.TabIndex = 4;
			this.btnUp.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// btnRemove
			// 
			this.btnRemove.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
			this.btnRemove.Location = new System.Drawing.Point(243, 96);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(32, 24);
			this.btnRemove.TabIndex = 3;
			this.btnRemove.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// btnAdd
			// 
			this.btnAdd.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
			this.btnAdd.Location = new System.Drawing.Point(243, 66);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(32, 24);
			this.btnAdd.TabIndex = 2;
			this.btnAdd.Click += new System.EventHandler(this.OnCmdClick);
//			// 
//			// locStop
//			// 
//			this.locStop.Company = "";
//			this.locStop.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
//			this.locStop.ForeColor = System.Drawing.SystemColors.ControlText;
//			this.locStop.Location = new System.Drawing.Point(42, 21);
//			this.locStop.LocationID = "";
//			this.locStop.Name = "locStop";
//			this.locStop.ShipperType = "";
//			this.locStop.Size = new System.Drawing.Size(201, 153);
//			this.locStop.TabIndex = 0;
//			this.locStop.Enter += new System.EventHandler(this.OnLocationEnter);
			// 
			// grdStops
			// 
			this.grdStops.DataMember = "FreightPathStopDetailTable";
			this.grdStops.DataSource = this.m_dsFPStops;
			this.grdStops.DisplayLayout.AddNewBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			appearance1.BackColor = System.Drawing.Color.White;
			appearance1.FontData.Name = "Arial";
			appearance1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.grdStops.DisplayLayout.Appearance = appearance1;
			ultraGridBand1.AddButtonCaption = "FreightAssignmentListForStationTable";
			ultraGridColumn1.Header.Caption = "StopID";
			ultraGridColumn1.Header.VisiblePosition = 1;
			ultraGridColumn1.Hidden = true;
			ultraGridColumn1.Width = 54;
			ultraGridColumn2.Header.VisiblePosition = 0;
			ultraGridColumn2.Hidden = true;
			ultraGridColumn2.Width = 53;
			ultraGridColumn3.Header.Caption = "Stop#";
			ultraGridColumn3.Width = 47;
			ultraGridColumn4.Header.Caption = "LocType";
			ultraGridColumn4.Header.VisiblePosition = 9;
			ultraGridColumn4.Width = 69;
			ultraGridColumn5.Header.Caption = "CoID";
			ultraGridColumn5.Header.VisiblePosition = 10;
			ultraGridColumn5.Hidden = true;
			ultraGridColumn5.Width = 42;
			ultraGridColumn6.Header.Caption = "Company";
			ultraGridColumn6.Header.VisiblePosition = 11;
			ultraGridColumn6.Width = 66;
			ultraGridColumn7.Header.Caption = "LocID";
			ultraGridColumn7.Header.VisiblePosition = 12;
			ultraGridColumn7.Hidden = true;
			ultraGridColumn7.Width = 48;
			ultraGridColumn8.Header.Caption = "Loc#";
			ultraGridColumn8.Width = 53;
			ultraGridColumn9.Header.Caption = "Location";
			ultraGridColumn9.Header.VisiblePosition = 6;
			ultraGridColumn9.Width = 106;
			ultraGridColumn10.Header.Caption = "Mnem";
			ultraGridColumn10.Header.VisiblePosition = 8;
			ultraGridColumn10.Width = 46;
			ultraGridColumn11.Header.VisiblePosition = 13;
			ultraGridColumn11.Width = 64;
			ultraGridColumn12.Header.VisiblePosition = 3;
			ultraGridColumn12.Width = 53;
			ultraGridColumn13.Header.Caption = "Mnem1";
			ultraGridColumn13.Header.VisiblePosition = 4;
			ultraGridColumn13.Width = 55;
			ultraGridColumn14.Header.Caption = "Mnem2";
			ultraGridColumn14.Header.VisiblePosition = 5;
			ultraGridColumn14.Width = 55;
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
			this.grdStops.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
			appearance6.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(128)), ((System.Byte)(255)));
			appearance6.FontData.Name = "Verdana";
			appearance6.FontData.SizeInPoints = 8F;
			appearance6.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance6.TextHAlign = Infragistics.Win.HAlign.Left;
			this.grdStops.DisplayLayout.CaptionAppearance = appearance6;
			this.grdStops.DisplayLayout.GroupByBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			this.grdStops.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			this.grdStops.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
			this.grdStops.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
			this.grdStops.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None;
			this.grdStops.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.None;
			this.grdStops.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
			this.grdStops.DisplayLayout.Override.CellMultiLine = Infragistics.Win.DefaultableBoolean.False;
			this.grdStops.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
			this.grdStops.DisplayLayout.Override.MaxSelectedCells = 1;
			this.grdStops.DisplayLayout.Override.MaxSelectedRows = 1;
			this.grdStops.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			this.grdStops.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
			this.grdStops.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
			this.grdStops.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grdStops.Location = new System.Drawing.Point(282, 21);
			this.grdStops.Name = "grdStops";
			this.grdStops.Size = new System.Drawing.Size(417, 147);
			this.grdStops.TabIndex = 1;
			this.grdStops.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnCellChange;
			this.grdStops.AfterRowUpdate += new Infragistics.Win.UltraWinGrid.RowEventHandler(this.OnStopRowUpdated);
			this.grdStops.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.OnStopCellUpdated);
			this.grdStops.BeforeCellActivate += new Infragistics.Win.UltraWinGrid.CancelableCellEventHandler(this.OnBeforeStopCellActivated);
			// 
			// m_dsFPStops
			// 
			this.m_dsFPStops.DataSetName = "FreightPathDS";
			this.m_dsFPStops.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// mFreightPathDetailDS
			// 
			this.mFreightPathDetailDS.DataSetName = "FreightPathDS";
			this.mFreightPathDetailDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// dlgFreightPathDetail
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(762, 363);
			this.Controls.Add(this.tabDialog);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgFreightPathDetail";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Freight Path Details";
			this.Load += new System.EventHandler(this.OnFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.mSortCentersDS)).EndInit();
			this.fraDetails.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mFPTypesDS)).EndInit();
			this.tabDialog.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.fraFreightStops.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdStops)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_dsFPStops)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mFreightPathDetailDS)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void OnFormLoad(object sender, System.EventArgs e) {
			//Initialize controls - set default values
			this.Cursor = Cursors.WaitCursor;
			try {
				//Show early
				this.Visible = true;
				Application.DoEvents();
				
				//Get selection lists
				this.mSortCentersDS.Merge(EnterpriseFactory.GetSortCenters());
				this.mFPTypesDS.Merge(EnterpriseFactory.GetFreightPathTypes());
				
				//Set control services
				#region Default grid behavior
				this.grdStops.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
				this.grdStops.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.True;
				this.grdStops.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.Edit;
				this.grdStops.DisplayLayout.Bands[0].Columns["PathStopID"].CellActivation = Activation.Disabled;
				this.grdStops.DisplayLayout.Bands[0].Columns["PathID"].CellActivation = Activation.Disabled;
				this.grdStops.DisplayLayout.Bands[0].Columns["StopNumber"].CellActivation = Activation.NoEdit;
				this.grdStops.DisplayLayout.Bands[0].Columns["LocationType"].CellActivation = Activation.NoEdit;
				this.grdStops.DisplayLayout.Bands[0].Columns["CompanyID"].CellActivation = Activation.Disabled;
				this.grdStops.DisplayLayout.Bands[0].Columns["CompanyName"].CellActivation = Activation.NoEdit;
				this.grdStops.DisplayLayout.Bands[0].Columns["LocationID"].CellActivation = Activation.Disabled;
				this.grdStops.DisplayLayout.Bands[0].Columns["Number"].CellActivation = Activation.NoEdit;
				this.grdStops.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
				this.grdStops.DisplayLayout.Bands[0].Columns["Address"].CellActivation = Activation.NoEdit;
				this.grdStops.DisplayLayout.Bands[0].Columns["Mnemonic"].CellActivation = Activation.NoEdit;
				this.grdStops.DisplayLayout.Bands[0].Columns["IsFinal"].CellActivation = Activation.AllowEdit;
				this.grdStops.DisplayLayout.Bands[0].Columns["UseMnemonic"].CellActivation = Activation.AllowEdit;
				this.grdStops.DisplayLayout.Bands[0].Columns["UseMnemonic2"].CellActivation = Activation.AllowEdit;
				this.grdStops.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.Select;
				#endregion
				this.txtDescription.MaxLength = 40;
				this.txtDescription.Text = mFreightPathDetailDS.FreightPathDetailTable[0].Description;
				if(this.mFreightPathDetailDS.FreightPathDetailTable[0].SortCenterID!=0) 
					this.cboSortCenters.SelectedValue = this.mFreightPathDetailDS.FreightPathDetailTable[0].SortCenterID;
				else
					if(this.cboSortCenters.Items.Count>0) this.cboSortCenters.SelectedIndex = 0;
				this.cboSortCenters.Enabled = ((this.mPathID=="") && (this.cboSortCenters.Items.Count>0));
				if(this.mFreightPathDetailDS.FreightPathDetailTable[0].PathType!="") 
					this.cboFPType.SelectedValue = this.mFreightPathDetailDS.FreightPathDetailTable[0].PathType;
				else
					if(this.cboFPType.Items.Count>0) this.cboFPType.SelectedIndex = 0;
				this.cboFPType.Enabled = ((this.mPathID=="") && (this.cboFPType.Items.Count>0));
				OnFreightPathTypeChanged(null, null);
				
				//Display existing freight path stops (non-editable)
				if(this.mPathID!="") {
					//Copy data from each exisiting freight path stop
					for(int i=0; i<this.mFreightPathDetailDS.FreightPathStopDetailTable.Rows.Count; i++) {
						FreightPathDS.FreightPathStopDetailTableRow row = this.m_dsFPStops.FreightPathStopDetailTable.NewFreightPathStopDetailTableRow();
						row.PathStopID = this.mFreightPathDetailDS.FreightPathStopDetailTable[i].PathStopID;
						row.PathID = this.mFreightPathDetailDS.FreightPathStopDetailTable[i].PathID;
						row.StopNumber = this.mFreightPathDetailDS.FreightPathStopDetailTable[i].StopNumber;
						row.LocationType = this.mFreightPathDetailDS.FreightPathStopDetailTable[i].LocationType;
						row.CompanyID = this.mFreightPathDetailDS.FreightPathStopDetailTable[i].CompanyID;
						row.CompanyName = this.mFreightPathDetailDS.FreightPathStopDetailTable[i].CompanyName;
						row.LocationID = this.mFreightPathDetailDS.FreightPathStopDetailTable[i].LocationID;
						row.Number = this.mFreightPathDetailDS.FreightPathStopDetailTable[i].Number;
						row.Description = this.mFreightPathDetailDS.FreightPathStopDetailTable[i].Description;
						row.Mnemonic = this.mFreightPathDetailDS.FreightPathStopDetailTable[i].Mnemonic;
						row.Address = this.mFreightPathDetailDS.FreightPathStopDetailTable[i].Address;
						row.IsFinal = this.mFreightPathDetailDS.FreightPathStopDetailTable[i].IsFinal;
						row.UseMnemonic = this.mFreightPathDetailDS.FreightPathStopDetailTable[i].UseMnemonic;
						row.UseMnemonic2 = this.mFreightPathDetailDS.FreightPathStopDetailTable[i].UseMnemonic2;
						this.m_dsFPStops.FreightPathStopDetailTable.AddFreightPathStopDetailTableRow(row);
					}
				}
				//this.locStop.Enabled = (this.mPathID=="");
				this.grdStops.DisplayLayout.Appearance.BackColor = (this.mPathID=="") ? System.Drawing.SystemColors.Window : System.Drawing.SystemColors.Control;
				this.chkStatus.Checked = this.mFreightPathDetailDS.FreightPathDetailTable[0].IsActive;
				this.btnAdd.Enabled = this.btnRemove.Enabled = false;
				this.btnUp.Enabled = this.btnDown.Enabled = false;
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.btnOk.Enabled = false; this.Cursor = Cursors.Default; }
		}
		private void OnFreightPathTypeChanged(object sender, System.EventArgs e) {
			//Event handler for change in freigth path type
			try {
				//Agents only on external
				//this.mLocationModel.AgentsOnly = (this.cboFPType.SelectedValue.ToString().ToLower()==PATHTYPE_EXTERNAL);
				//this.locStop.RefreshList();
				ValidateForm(null, null);
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnLocationEnter(object sender, System.EventArgs e) {
			//Event handler for change in location control location
			try {
				//Allow addition of a location
				this.btnAdd.Enabled = (this.mPathID=="");
				this.btnRemove.Enabled = false;
			} 
			catch(Exception ex) { reportError(ex); }
		}
		#region Freight Path Stop Grid: OnBeforeStopCellActivated(), OnStopCellUpdated(), OnStopRowUpdated()
		private void OnBeforeStopCellActivated(object sender, Infragistics.Win.UltraWinGrid.CancelableCellEventArgs e) {
			//Event handler for freight path stop cell activated
			//Debug.Write("OnBeforeStopCellActivated()\n");
			string sMnemonic="";
			try {
				//Mnemonic selection avaliable if a mnemonic exists for the selected location
				sMnemonic = e.Cell.Row.Cells["Mnemonic"].Value.ToString();
				switch(e.Cell.Column.Key.ToString()) {
					case "IsFinal":			e.Cell.Activation = (this.mPathID=="") ? Activation.AllowEdit : Activation.NoEdit; break;
					case "UseMnemonic":		e.Cell.Activation = (this.mPathID=="" && sMnemonic!="") ? Activation.AllowEdit : Activation.NoEdit; break;
					case "UseMnemonic2":	e.Cell.Activation = (this.mPathID=="" && sMnemonic!="") ? Activation.AllowEdit : Activation.NoEdit; break;
					default:				e.Cell.Activation = Activation.NoEdit; break;
				}
				//Allow removal of a location
				this.btnAdd.Enabled = false;
				this.btnRemove.Enabled = (this.mPathID=="");
				
				//Allow stop order changes
				e.Cell.Row.Selected = true;
				this.btnUp.Enabled = (this.mPathID=="" && this.grdStops.Selected.Rows[0].VisibleIndex>0);
				this.btnDown.Enabled = (this.mPathID=="" && this.grdStops.Selected.Rows[0].VisibleIndex<this.grdStops.Rows.Count-1);
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnStopCellUpdated(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e) {
			//Event handler for change in a freight path stop cell value
			try {
				//Validate mnemonic selections
				switch(e.Cell.Column.Key.ToString()) {
					case "IsFinal":
						//Clear all others
						for(int i=0; i<this.grdStops.Rows.Count; i++) 
							if(i!=e.Cell.Row.Index) this.grdStops.Rows[i].Cells["IsFinal"].Value = false;
						break;
					case "UseMnemonic":
						//Clear all others
						for(int i=0; i<this.grdStops.Rows.Count; i++) 
							if(i!=e.Cell.Row.Index) this.grdStops.Rows[i].Cells["UseMnemonic"].Value = false;
						break;
					case "UseMnemonic2":
						//Clear all others
						for(int i=0; i<this.grdStops.Rows.Count; i++) 
							if(i!=e.Cell.Row.Index) this.grdStops.Rows[i].Cells["UseMnemonic2"].Value = false;
						break;
				}
				this.grdStops.UpdateData();
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnStopRowUpdated(object sender, Infragistics.Win.UltraWinGrid.RowEventArgs e) {
			//Event handler for change in a freight path stop row
			try {
				//Validate row
				ValidateForm(null, null);
			} 
			catch(Exception ex) { reportError(ex); }
		}
		#endregion
		#region User Services: ValidateForm(), OnCmdClick()
		private void ValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes to control data
			//Debug.Write("ValidateForm()\n");
			bool bFPStopsValid=false, bStopCountValid=false, bUseMnemonicValid=false, bFinalStopValid=false, bLastInternalStopValid=false;
			string locType="";
			
			try {
				if(this.mFreightPathDetailDS.FreightPathDetailTable.Count>0) {
					//Validate freight path stops
					bFPStopsValid = bStopCountValid = bUseMnemonicValid = bFinalStopValid = false;
					bLastInternalStopValid = true;
					for(int i=0; i<this.grdStops.Rows.Count; i++) {
						//1. Validate at least one freight path stop is created
						bStopCountValid = true;
						
						//2. UseMnemonic must be set for one of the stops (10.3); set flag once on success
						if(!bUseMnemonicValid)
							bUseMnemonicValid = Convert.ToBoolean(this.grdStops.Rows[i].Cells["UseMnemonic"].Value);
						
						//3. Validate one stop is marked as final stop (10.4); set flag once on success
						if(!bFinalStopValid)
							bFinalStopValid = Convert.ToBoolean(this.grdStops.Rows[i].Cells["IsFinal"].Value);
						
						//4. Validate last stop is store\vendor for Internal freight path type
						if((this.cboFPType.SelectedValue.ToString().ToLower()==PATHTYPE_INTERNAL) && i==this.grdStops.Rows.Count-1) {
							locType = this.grdStops.Rows[i].Cells["LocationType"].Value.ToString();
							bLastInternalStopValid = (locType.ToLower()=="store" || locType.ToLower()=="warehouse");
						}
					}
					bFPStopsValid = (bStopCountValid && bUseMnemonicValid && bFinalStopValid && bLastInternalStopValid);
					
					//Enable OK service if details have valid changes
					this.btnOk.Enabled = (this.txtDescription.Text!="" && this.cboSortCenters.Text!="" && this.cboFPType.Text!="" && bFPStopsValid);

					//Disable freight type change if stops exists
					this.cboFPType.Enabled = (this.mPathID=="" && this.cboFPType.Items.Count>0 && !bStopCountValid);
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnCmdClick(object sender, System.EventArgs e) {
			//Command button handler
			string sStopID="";
			FreightPathDS.FreightPathStopDetailTableRow _row=null;
			FreightPathDS.FreightPathStopDetailTableRow row=null;
			LocationDS dsLoc=null;
			string sAddress="";
			try {
				Button btn = (Button)sender;
				switch(btn.Text) {
					case CMD_STOP_ADD:
						//Add a stop location if it isn't an existing stop
						dsLoc = null;	//this.locStop.GetLocation();
						if(this.m_dsFPStops.FreightPathStopDetailTable.Select("LocationID='" + dsLoc.LocationListTable[0].LocationID + "'").Length>0) 
							MessageBox.Show("Location " + dsLoc.LocationListTable[0].Number + " is used in an existing freight path stop. Please select another location.", this.Name, MessageBoxButtons.OK);
						else {
							_row = this.m_dsFPStops.FreightPathStopDetailTable.NewFreightPathStopDetailTableRow();
							_row.PathStopID = "";
							_row.PathID = this.mPathID;
							_row.StopNumber = (short)(this.grdStops.Rows.Count + 1);
							_row.LocationType = dsLoc.LocationListTable[0].Type;
							_row.CompanyID = dsLoc.LocationListTable[0].CompanyID;
							_row.CompanyName = "???";
							_row.LocationID = dsLoc.LocationListTable[0].LocationID;
							_row.Number = dsLoc.LocationListTable[0].Number;
							_row.Description = dsLoc.LocationListTable[0].Description;
							_row.Mnemonic = dsLoc.LocationListTable[0].Mnemonic;
							sAddress = dsLoc.LocationListTable[0].AddressLine1.Trim() + " ";
							if(!dsLoc.LocationListTable[0].IsAddressLine2Null())
								if(dsLoc.LocationListTable[0].AddressLine2!="")
									sAddress += dsLoc.LocationListTable[0].AddressLine2.Trim() + " ";
							sAddress += dsLoc.LocationListTable[0].City.Trim() + ", " + dsLoc.LocationListTable[0].StateOrProvince.Trim() + " " + dsLoc.LocationListTable[0].PostalCode.Trim();
							_row.Address = sAddress;
							_row.IsFinal = false;
							_row.UseMnemonic = false;
							_row.UseMnemonic2 = false;
							this.m_dsFPStops.FreightPathStopDetailTable.AddFreightPathStopDetailTableRow(_row);
							this.grdStops.ActiveRow = this.grdStops.Rows[this.grdStops.Rows.Count-1];
							this.grdStops.ActiveRow.Selected = true;
							this.grdStops.Refresh();
							this.btnUp.Enabled = (this.mPathID=="" && this.grdStops.Rows.Count>1);
							this.btnDown.Enabled = false;
						}
						ValidateForm(null, null);
						break;
					case CMD_STOP_REM:
						//Remove a stop location, and re-order remaining stops, and select
						if(this.grdStops.ActiveRow!=null)  
							this.grdStops.ActiveRow.Selected = true;
						this.grdStops.Selected.Rows[0].Delete(false);
						for(int i=0; i<this.m_dsFPStops.FreightPathStopDetailTable.Rows.Count; i++) 
							this.m_dsFPStops.FreightPathStopDetailTable[i].StopNumber = (short)(i + 1);
						if(this.grdStops.Rows.Count>0) {
							this.grdStops.ActiveRow = this.grdStops.Rows[0];
							this.grdStops.ActiveRow.Selected = true;
						}
						this.grdStops.Refresh();
						this.btnUp.Enabled = false;
						this.btnDown.Enabled = (this.mPathID=="" && this.grdStops.Rows.Count>1);
						ValidateForm(null, null);
						break;
					case CMD_STOP_MOVEUP:
						//Move the selected stop location up in the stop order and re-order
						if(this.grdStops.ActiveRow!=null)  
							this.grdStops.ActiveRow.Selected = true;
						if(this.grdStops.Selected.Rows[0].VisibleIndex>0) {
							short iStopNo = Convert.ToInt16(this.grdStops.Selected.Rows[0].Cells["StopNumber"].Value);
							for(int i=this.grdStops.Rows.Count-1; i>=0; i--) { 
								if(Convert.ToInt16(this.grdStops.Rows[i].Cells["StopNumber"].Value)==(iStopNo-1))
									this.grdStops.Rows[i].Cells["StopNumber"].Value = i+2;
								else if(Convert.ToInt16(this.grdStops.Rows[i].Cells["StopNumber"].Value)==iStopNo) 
									this.grdStops.Rows[i].Cells["StopNumber"].Value = i;
								else
									this.grdStops.Rows[i].Cells["StopNumber"].Value = i+1;
							}
							this.grdStops.DisplayLayout.Bands[0].Columns["StopNumber"].SortIndicator = SortIndicator.Ascending;
							this.grdStops.DisplayLayout.Bands[0].Columns["StopNumber"].SortIndicator = SortIndicator.None;
							this.grdStops.Refresh();
							this.btnUp.Enabled = (this.mPathID=="" && this.grdStops.Selected.Rows[0].VisibleIndex>0);
							this.btnDown.Enabled = (this.mPathID=="");
						}
						break;
					case CMD_STOP_MOVEDOWN:
						//Move the selected stop location down in the stop order and re-order
						if(this.grdStops.ActiveRow!=null)  
							this.grdStops.ActiveRow.Selected = true;
						if(this.grdStops.Selected.Rows[0].VisibleIndex<this.grdStops.Rows.Count-1) {
							short iStopNo = Convert.ToInt16(this.grdStops.Selected.Rows[0].Cells["StopNumber"].Value);
							for(int i=0; i<this.grdStops.Rows.Count; i++) { 
								if(Convert.ToInt16(this.grdStops.Rows[i].Cells["StopNumber"].Value)==iStopNo) 
									this.grdStops.Rows[i].Cells["StopNumber"].Value = i+2;
								else if(Convert.ToInt16(this.grdStops.Rows[i].Cells["StopNumber"].Value)==(iStopNo+1))
									this.grdStops.Rows[i].Cells["StopNumber"].Value = i;
								else
									this.grdStops.Rows[i].Cells["StopNumber"].Value = i+1;
							}
							this.grdStops.DisplayLayout.Bands[0].Columns["StopNumber"].SortIndicator = SortIndicator.Ascending;
							this.grdStops.DisplayLayout.Bands[0].Columns["StopNumber"].SortIndicator = SortIndicator.None;
							this.grdStops.Refresh();
							this.btnUp.Enabled = (this.mPathID=="");
							this.btnDown.Enabled = (this.mPathID=="" && this.grdStops.Selected.Rows[0].VisibleIndex<this.grdStops.Rows.Count-1);
						}
						break;
					case CMD_CANCEL:
						//Close the dialog
						this.DialogResult = DialogResult.Cancel;
						this.Close();
						break;
					case CMD_OK:
						//Update details with control values
						this.mFreightPathDetailDS.FreightPathDetailTable[0].Description = this.txtDescription.Text;
						this.mFreightPathDetailDS.FreightPathDetailTable[0].SortCenterID = Convert.ToInt32(this.cboSortCenters.SelectedValue);
						this.mFreightPathDetailDS.FreightPathDetailTable[0].PathType = this.cboFPType.SelectedValue.ToString();
						
						for(int i=0; i<this.m_dsFPStops.FreightPathStopDetailTable.Rows.Count; i++) {
							//Add a new data element
							//*** RESULT MUST MARK ROW AS DataRowState.Added
							row = this.mFreightPathDetailDS.FreightPathStopDetailTable.NewFreightPathStopDetailTableRow();
							row.PathStopID = sStopID;
							row.PathID = this.m_dsFPStops.FreightPathStopDetailTable[i].PathID;
							row.StopNumber = this.m_dsFPStops.FreightPathStopDetailTable[i].StopNumber;
							row.LocationType = this.m_dsFPStops.FreightPathStopDetailTable[i].LocationType;
							row.CompanyName = this.m_dsFPStops.FreightPathStopDetailTable[i].CompanyName;
							row.LocationID = this.m_dsFPStops.FreightPathStopDetailTable[i].LocationID;
							row.Number = this.m_dsFPStops.FreightPathStopDetailTable[i].Number;
							row.Description = this.m_dsFPStops.FreightPathStopDetailTable[i].Description;
							row.IsFinal = this.m_dsFPStops.FreightPathStopDetailTable[i].IsFinal;
							row.UseMnemonic = this.m_dsFPStops.FreightPathStopDetailTable[i].UseMnemonic;
							row.UseMnemonic2 = this.m_dsFPStops.FreightPathStopDetailTable[i].UseMnemonic2;
							this.mFreightPathDetailDS.FreightPathStopDetailTable.AddFreightPathStopDetailTableRow(row);
						}
						#region Test update to mFreightPathDetailDS.FreightPathStopDetailTable
						Debug.Write("\n");
						for(int i=0; i<this.mFreightPathDetailDS.FreightPathStopDetailTable.Rows.Count; i++) {
							switch(this.mFreightPathDetailDS.FreightPathStopDetailTable[i].RowState) {
								case		DataRowState.Added: Debug.Write("Added data element (stopID=)\n"); break;
								default:	Debug.Write(this.mFreightPathDetailDS.FreightPathStopDetailTable[i].RowState.ToString() + " freight path stop (stopID=)\n"); break;
							}
						}
						#endregion
						this.mFreightPathDetailDS.FreightPathDetailTable[0].IsActive = this.chkStatus.Checked;
						this.mFreightPathDetailDS.AcceptChanges();
						this.DialogResult = DialogResult.OK;
						this.Close();
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
