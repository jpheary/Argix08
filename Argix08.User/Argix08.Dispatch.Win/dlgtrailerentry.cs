//	File:	dlgtrailerentry.cs
//	Author:	J. Heary
//	Date:	11/02/05
//	Desc:	Dialog to view/edit trailer entries in the Trailer Tracking Log.
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
using Argix.Data;

namespace Argix.Dispatch {
	public class dlgTrailerEntry : Argix.Dispatch.dlgSchedule {
		//Members
		//Constants
		//Events
		#region Controls
		private System.Windows.Forms.Label _lblCreatedDate;
		private System.Windows.Forms.DateTimePicker dtpCreatedDate;
		private System.Windows.Forms.Label _lblCreatedBy;
		private System.Windows.Forms.Label _lblTitle;
		private System.Windows.Forms.Label _lblComments;
		private System.Windows.Forms.TextBox txtComments;
		private System.Windows.Forms.TextBox txtCreatedBy;
		private System.Windows.Forms.TabControl tabMain;
		private System.Windows.Forms.TabPage tabIncoming;
		private System.Windows.Forms.TabPage tabMoves;
		private System.Windows.Forms.TabPage tabOutgoing;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdMoves;
		private Argix.Dispatch.DispatchDS mMovesDS;
		private System.Windows.Forms.Label _lblInCarrier;
		private System.Windows.Forms.Label _lblInDriver;
		private System.Windows.Forms.TextBox txtInDriver;
		private System.Windows.Forms.ComboBox cboInCarrier;
		private System.Windows.Forms.Label _lblInDate;
		private System.Windows.Forms.DateTimePicker dtpInDate;
		private System.Windows.Forms.Label _lblInLoc;
		private System.Windows.Forms.TextBox txtInLoc;
		private System.Windows.Forms.Label _lblInSeal;
		private System.Windows.Forms.TextBox txtInSeal;
		private System.Windows.Forms.CheckBox chkMoveInProgress;
		private System.Windows.Forms.CheckBox chkLoadSheetReady;
		private System.Windows.Forms.CheckBox chkTrailerOut;
		private System.Windows.Forms.Label _lblOutDate;
		private System.Windows.Forms.DateTimePicker dtpOutDate;
		private System.Windows.Forms.Label _lblOutSeal;
		private System.Windows.Forms.TextBox txtOutSeal;
		private System.Windows.Forms.Label _lblOutCarrier;
		private System.Windows.Forms.ComboBox cboOutCarrier;
		private System.Windows.Forms.Label _lblOutDriver;
		private System.Windows.Forms.TextBox txtOutDriver;
		private System.Windows.Forms.Label _lblTrailer;
		private System.Windows.Forms.TextBox txtTrailer;
		private System.ComponentModel.IContainer components = null;
		#endregion
		
		public dlgTrailerEntry(ScheduleEntry entry, Mediator mediator): base(entry, mediator) {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
			} 
			catch(Exception ex) { throw ex; }
		}
		protected override void Dispose( bool disposing ) {
			//Clean up any resources being used
			if( disposing ) {
				if(components != null)
					components.Dispose();
			}
			base.Dispose( disposing );
		}
		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("TrailerMoveTable", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Requested");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RequestedBy");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MoveFrom");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MoveTo");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Switcher");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LoadedWith");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledTime");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ActualTime");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerLogTable_Id");
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			this._lblCreatedDate = new System.Windows.Forms.Label();
			this.dtpCreatedDate = new System.Windows.Forms.DateTimePicker();
			this.txtCreatedBy = new System.Windows.Forms.TextBox();
			this._lblCreatedBy = new System.Windows.Forms.Label();
			this._lblTitle = new System.Windows.Forms.Label();
			this._lblInCarrier = new System.Windows.Forms.Label();
			this._lblInDriver = new System.Windows.Forms.Label();
			this.txtInDriver = new System.Windows.Forms.TextBox();
			this._lblComments = new System.Windows.Forms.Label();
			this.txtComments = new System.Windows.Forms.TextBox();
			this.cboInCarrier = new System.Windows.Forms.ComboBox();
			this.tabMain = new System.Windows.Forms.TabControl();
			this.tabIncoming = new System.Windows.Forms.TabPage();
			this._lblInDate = new System.Windows.Forms.Label();
			this.dtpInDate = new System.Windows.Forms.DateTimePicker();
			this._lblInLoc = new System.Windows.Forms.Label();
			this.txtInLoc = new System.Windows.Forms.TextBox();
			this._lblInSeal = new System.Windows.Forms.Label();
			this.txtInSeal = new System.Windows.Forms.TextBox();
			this.tabMoves = new System.Windows.Forms.TabPage();
			this.chkMoveInProgress = new System.Windows.Forms.CheckBox();
			this.chkLoadSheetReady = new System.Windows.Forms.CheckBox();
			this.grdMoves = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.mMovesDS = new Argix.Dispatch.DispatchDS();
			this.tabOutgoing = new System.Windows.Forms.TabPage();
			this.chkTrailerOut = new System.Windows.Forms.CheckBox();
			this._lblOutDate = new System.Windows.Forms.Label();
			this.dtpOutDate = new System.Windows.Forms.DateTimePicker();
			this._lblOutSeal = new System.Windows.Forms.Label();
			this.txtOutSeal = new System.Windows.Forms.TextBox();
			this._lblOutCarrier = new System.Windows.Forms.Label();
			this.cboOutCarrier = new System.Windows.Forms.ComboBox();
			this._lblOutDriver = new System.Windows.Forms.Label();
			this.txtOutDriver = new System.Windows.Forms.TextBox();
			this._lblTrailer = new System.Windows.Forms.Label();
			this.txtTrailer = new System.Windows.Forms.TextBox();
			this.tabMain.SuspendLayout();
			this.tabIncoming.SuspendLayout();
			this.tabMoves.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdMoves)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mMovesDS)).BeginInit();
			this.tabOutgoing.SuspendLayout();
			this.SuspendLayout();
			// 
			// mToolTip
			// 
			this.mToolTip.AutoPopDelay = 3000;
			this.mToolTip.InitialDelay = 500;
			this.mToolTip.ReshowDelay = 1000;
			this.mToolTip.ShowAlways = true;
			// 
			// btnOK
			// 
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 4;
			// 
			// btnCancel
			// 
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 0;
			// 
			// _lblCreatedDate
			// 
			this._lblCreatedDate.Location = new System.Drawing.Point(354, 60);
			this._lblCreatedDate.Name = "_lblCreatedDate";
			this._lblCreatedDate.Size = new System.Drawing.Size(96, 18);
			this._lblCreatedDate.TabIndex = 131;
			this._lblCreatedDate.Text = "Created: ";
			this._lblCreatedDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtpCreatedDate
			// 
			this.dtpCreatedDate.CustomFormat = "MMM dd, yyyy   hh:mm tt";
			this.dtpCreatedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpCreatedDate.Location = new System.Drawing.Point(459, 60);
			this.dtpCreatedDate.Name = "dtpCreatedDate";
			this.dtpCreatedDate.ShowUpDown = true;
			this.dtpCreatedDate.Size = new System.Drawing.Size(192, 21);
			this.dtpCreatedDate.TabIndex = 2;
			this.dtpCreatedDate.ValueChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// txtCreatedBy
			// 
			this.txtCreatedBy.Location = new System.Drawing.Point(135, 60);
			this.txtCreatedBy.Name = "txtCreatedBy";
			this.txtCreatedBy.Size = new System.Drawing.Size(192, 21);
			this.txtCreatedBy.TabIndex = 1;
			this.txtCreatedBy.Text = "";
			this.txtCreatedBy.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblCreatedBy
			// 
			this._lblCreatedBy.Location = new System.Drawing.Point(30, 60);
			this._lblCreatedBy.Name = "_lblCreatedBy";
			this._lblCreatedBy.Size = new System.Drawing.Size(96, 18);
			this._lblCreatedBy.TabIndex = 128;
			this._lblCreatedBy.Text = "Created By: ";
			this._lblCreatedBy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblTitle
			// 
			this._lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._lblTitle.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblTitle.Location = new System.Drawing.Point(174, 6);
			this._lblTitle.Name = "_lblTitle";
			this._lblTitle.Size = new System.Drawing.Size(318, 32);
			this._lblTitle.TabIndex = 127;
			this._lblTitle.Text = "Trailer/Trailer Moves Log";
			this._lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// _lblInCarrier
			// 
			this._lblInCarrier.Location = new System.Drawing.Point(15, 51);
			this._lblInCarrier.Name = "_lblInCarrier";
			this._lblInCarrier.Size = new System.Drawing.Size(96, 18);
			this._lblInCarrier.TabIndex = 138;
			this._lblInCarrier.Text = "Carrier: ";
			this._lblInCarrier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblInDriver
			// 
			this._lblInDriver.Location = new System.Drawing.Point(15, 81);
			this._lblInDriver.Name = "_lblInDriver";
			this._lblInDriver.Size = new System.Drawing.Size(96, 18);
			this._lblInDriver.TabIndex = 137;
			this._lblInDriver.Text = "Driver: ";
			this._lblInDriver.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtInDriver
			// 
			this.txtInDriver.Location = new System.Drawing.Point(123, 81);
			this.txtInDriver.Name = "txtInDriver";
			this.txtInDriver.Size = new System.Drawing.Size(192, 21);
			this.txtInDriver.TabIndex = 0;
			this.txtInDriver.Text = "";
			this.txtInDriver.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblComments
			// 
			this._lblComments.Location = new System.Drawing.Point(12, 333);
			this._lblComments.Name = "_lblComments";
			this._lblComments.Size = new System.Drawing.Size(114, 18);
			this._lblComments.TabIndex = 129;
			this._lblComments.Text = "Comments: ";
			this._lblComments.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtComments
			// 
			this.txtComments.Location = new System.Drawing.Point(135, 333);
			this.txtComments.Multiline = true;
			this.txtComments.Name = "txtComments";
			this.txtComments.Size = new System.Drawing.Size(516, 69);
			this.txtComments.TabIndex = 4;
			this.txtComments.Text = "";
			this.txtComments.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// cboInCarrier
			// 
			this.cboInCarrier.Location = new System.Drawing.Point(123, 51);
			this.cboInCarrier.Name = "cboInCarrier";
			this.cboInCarrier.Size = new System.Drawing.Size(192, 21);
			this.cboInCarrier.TabIndex = 1;
			this.cboInCarrier.SelectionChangeCommitted += new System.EventHandler(this.OnValidateForm);
			// 
			// tabMain
			// 
			this.tabMain.Controls.Add(this.tabIncoming);
			this.tabMain.Controls.Add(this.tabMoves);
			this.tabMain.Controls.Add(this.tabOutgoing);
			this.tabMain.Location = new System.Drawing.Point(6, 123);
			this.tabMain.Name = "tabMain";
			this.tabMain.SelectedIndex = 0;
			this.tabMain.Size = new System.Drawing.Size(654, 201);
			this.tabMain.TabIndex = 139;
			// 
			// tabIncoming
			// 
			this.tabIncoming.Controls.Add(this._lblInDate);
			this.tabIncoming.Controls.Add(this.dtpInDate);
			this.tabIncoming.Controls.Add(this._lblInLoc);
			this.tabIncoming.Controls.Add(this.txtInLoc);
			this.tabIncoming.Controls.Add(this._lblInSeal);
			this.tabIncoming.Controls.Add(this.txtInSeal);
			this.tabIncoming.Controls.Add(this._lblInCarrier);
			this.tabIncoming.Controls.Add(this.cboInCarrier);
			this.tabIncoming.Controls.Add(this._lblInDriver);
			this.tabIncoming.Controls.Add(this.txtInDriver);
			this.tabIncoming.Location = new System.Drawing.Point(4, 22);
			this.tabIncoming.Name = "tabIncoming";
			this.tabIncoming.Size = new System.Drawing.Size(646, 175);
			this.tabIncoming.TabIndex = 0;
			this.tabIncoming.Text = "Incoming";
			// 
			// _lblInDate
			// 
			this._lblInDate.Location = new System.Drawing.Point(15, 21);
			this._lblInDate.Name = "_lblInDate";
			this._lblInDate.Size = new System.Drawing.Size(96, 18);
			this._lblInDate.TabIndex = 144;
			this._lblInDate.Text = "Incoming:";
			this._lblInDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtpInDate
			// 
			this.dtpInDate.CustomFormat = "MMM dd, yyyy   hh:mm tt";
			this.dtpInDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpInDate.Location = new System.Drawing.Point(123, 21);
			this.dtpInDate.Name = "dtpInDate";
			this.dtpInDate.ShowUpDown = true;
			this.dtpInDate.Size = new System.Drawing.Size(192, 21);
			this.dtpInDate.TabIndex = 143;
			this.dtpInDate.ValueChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblInLoc
			// 
			this._lblInLoc.Location = new System.Drawing.Point(15, 141);
			this._lblInLoc.Name = "_lblInLoc";
			this._lblInLoc.Size = new System.Drawing.Size(96, 18);
			this._lblInLoc.TabIndex = 142;
			this._lblInLoc.Text = "Door\\Yard Loc: ";
			this._lblInLoc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtInLoc
			// 
			this.txtInLoc.Location = new System.Drawing.Point(123, 141);
			this.txtInLoc.Name = "txtInLoc";
			this.txtInLoc.Size = new System.Drawing.Size(192, 21);
			this.txtInLoc.TabIndex = 141;
			this.txtInLoc.Text = "";
			this.txtInLoc.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblInSeal
			// 
			this._lblInSeal.Location = new System.Drawing.Point(15, 111);
			this._lblInSeal.Name = "_lblInSeal";
			this._lblInSeal.Size = new System.Drawing.Size(96, 18);
			this._lblInSeal.TabIndex = 140;
			this._lblInSeal.Text = "Seal: ";
			this._lblInSeal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtInSeal
			// 
			this.txtInSeal.Location = new System.Drawing.Point(123, 111);
			this.txtInSeal.Name = "txtInSeal";
			this.txtInSeal.Size = new System.Drawing.Size(192, 21);
			this.txtInSeal.TabIndex = 139;
			this.txtInSeal.Text = "";
			this.txtInSeal.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// tabMoves
			// 
			this.tabMoves.Controls.Add(this.chkMoveInProgress);
			this.tabMoves.Controls.Add(this.chkLoadSheetReady);
			this.tabMoves.Controls.Add(this.grdMoves);
			this.tabMoves.Location = new System.Drawing.Point(4, 22);
			this.tabMoves.Name = "tabMoves";
			this.tabMoves.Size = new System.Drawing.Size(646, 175);
			this.tabMoves.TabIndex = 1;
			this.tabMoves.Text = "Moves";
			// 
			// chkMoveInProgress
			// 
			this.chkMoveInProgress.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.chkMoveInProgress.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.chkMoveInProgress.Location = new System.Drawing.Point(486, 6);
			this.chkMoveInProgress.Name = "chkMoveInProgress";
			this.chkMoveInProgress.Size = new System.Drawing.Size(144, 16);
			this.chkMoveInProgress.TabIndex = 157;
			this.chkMoveInProgress.Text = "Move In Progress?";
			this.chkMoveInProgress.CheckedChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// chkLoadSheetReady
			// 
			this.chkLoadSheetReady.Location = new System.Drawing.Point(6, 153);
			this.chkLoadSheetReady.Name = "chkLoadSheetReady";
			this.chkLoadSheetReady.Size = new System.Drawing.Size(192, 21);
			this.chkLoadSheetReady.TabIndex = 156;
			this.chkLoadSheetReady.Text = "Load Sheet Ready?";
			this.chkLoadSheetReady.CheckedChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// grdMoves
			// 
			this.grdMoves.DataMember = "TrailerLogTable.TrailerLogTable_TrailerMoveTable";
			this.grdMoves.DataSource = this.mMovesDS;
			appearance1.BackColor = System.Drawing.SystemColors.Window;
			appearance1.FontData.Name = "Verdana";
			appearance1.FontData.SizeInPoints = 8F;
			appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
			appearance1.TextHAlign = Infragistics.Win.HAlign.Left;
			this.grdMoves.DisplayLayout.Appearance = appearance1;
			ultraGridColumn1.Header.VisiblePosition = 0;
			ultraGridColumn1.Hidden = true;
			ultraGridColumn2.Format = "MM/dd/yyyy HH:mm tt";
			ultraGridColumn2.Header.VisiblePosition = 1;
			ultraGridColumn2.Width = 120;
			ultraGridColumn3.Header.Caption = "Requested By";
			ultraGridColumn3.Header.VisiblePosition = 2;
			ultraGridColumn3.Width = 96;
			ultraGridColumn4.Header.Caption = "Move From";
			ultraGridColumn4.Header.VisiblePosition = 3;
			ultraGridColumn4.Width = 96;
			ultraGridColumn5.Header.Caption = "Move To";
			ultraGridColumn5.Header.VisiblePosition = 4;
			ultraGridColumn5.Width = 96;
			ultraGridColumn6.Header.Caption = "Moved By";
			ultraGridColumn6.Header.VisiblePosition = 5;
			ultraGridColumn6.Width = 96;
			ultraGridColumn7.Header.Caption = "Loaded With";
			ultraGridColumn7.Header.VisiblePosition = 6;
			ultraGridColumn7.Width = 96;
			ultraGridColumn8.Format = "HH:mm tt";
			ultraGridColumn8.Header.Caption = "Time Allotted";
			ultraGridColumn8.Header.VisiblePosition = 7;
			ultraGridColumn8.Width = 96;
			ultraGridColumn9.Format = "HH:mm tt";
			ultraGridColumn9.Header.Caption = "Time Spent";
			ultraGridColumn9.Header.VisiblePosition = 8;
			ultraGridColumn9.Width = 96;
			ultraGridColumn10.Header.VisiblePosition = 9;
			ultraGridColumn10.Hidden = true;
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
															 ultraGridColumn10});
			this.grdMoves.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
			appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
			appearance2.FontData.BoldAsString = "True";
			appearance2.FontData.Name = "Verdana";
			appearance2.FontData.SizeInPoints = 8F;
			appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			appearance2.TextHAlign = Infragistics.Win.HAlign.Left;
			this.grdMoves.DisplayLayout.CaptionAppearance = appearance2;
			this.grdMoves.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			this.grdMoves.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
			this.grdMoves.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
			this.grdMoves.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
			appearance3.BackColor = System.Drawing.SystemColors.Control;
			appearance3.FontData.BoldAsString = "True";
			appearance3.FontData.Name = "Verdana";
			appearance3.FontData.SizeInPoints = 8F;
			appearance3.TextHAlign = Infragistics.Win.HAlign.Left;
			this.grdMoves.DisplayLayout.Override.HeaderAppearance = appearance3;
			this.grdMoves.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
			this.grdMoves.DisplayLayout.Override.MaxSelectedRows = 1;
			appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
			this.grdMoves.DisplayLayout.Override.RowAppearance = appearance4;
			this.grdMoves.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			this.grdMoves.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
			this.grdMoves.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			this.grdMoves.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			this.grdMoves.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
			this.grdMoves.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
			this.grdMoves.Location = new System.Drawing.Point(0, 3);
			this.grdMoves.Name = "grdMoves";
			this.grdMoves.Size = new System.Drawing.Size(639, 144);
			this.grdMoves.SupportThemes = false;
			this.grdMoves.TabIndex = 0;
			this.grdMoves.Text = "Moves";
			this.grdMoves.AfterRowUpdate += new Infragistics.Win.UltraWinGrid.RowEventHandler(this.OnRowChanged);
			this.grdMoves.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.OnCellChanged);
			this.grdMoves.AfterCellUpdate += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.OnCellChanged);
			this.grdMoves.BeforeCellActivate += new Infragistics.Win.UltraWinGrid.CancelableCellEventHandler(this.OnCellActivating);
			// 
			// mMovesDS
			// 
			this.mMovesDS.DataSetName = "DispatchDS";
			this.mMovesDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// tabOutgoing
			// 
			this.tabOutgoing.Controls.Add(this.chkTrailerOut);
			this.tabOutgoing.Controls.Add(this._lblOutDate);
			this.tabOutgoing.Controls.Add(this.dtpOutDate);
			this.tabOutgoing.Controls.Add(this._lblOutSeal);
			this.tabOutgoing.Controls.Add(this.txtOutSeal);
			this.tabOutgoing.Controls.Add(this._lblOutCarrier);
			this.tabOutgoing.Controls.Add(this.cboOutCarrier);
			this.tabOutgoing.Controls.Add(this._lblOutDriver);
			this.tabOutgoing.Controls.Add(this.txtOutDriver);
			this.tabOutgoing.Location = new System.Drawing.Point(4, 22);
			this.tabOutgoing.Name = "tabOutgoing";
			this.tabOutgoing.Size = new System.Drawing.Size(646, 175);
			this.tabOutgoing.TabIndex = 2;
			this.tabOutgoing.Text = "Outgoing";
			// 
			// chkTrailerOut
			// 
			this.chkTrailerOut.Location = new System.Drawing.Point(447, 141);
			this.chkTrailerOut.Name = "chkTrailerOut";
			this.chkTrailerOut.Size = new System.Drawing.Size(192, 21);
			this.chkTrailerOut.TabIndex = 155;
			this.chkTrailerOut.Text = "Trailer completed and out?";
			this.chkTrailerOut.CheckedChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblOutDate
			// 
			this._lblOutDate.Location = new System.Drawing.Point(15, 21);
			this._lblOutDate.Name = "_lblOutDate";
			this._lblOutDate.Size = new System.Drawing.Size(96, 18);
			this._lblOutDate.TabIndex = 154;
			this._lblOutDate.Text = "Outgoing:";
			this._lblOutDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtpOutDate
			// 
			this.dtpOutDate.CustomFormat = "MMM dd, yyyy   hh:mm tt";
			this.dtpOutDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpOutDate.Location = new System.Drawing.Point(123, 21);
			this.dtpOutDate.Name = "dtpOutDate";
			this.dtpOutDate.ShowUpDown = true;
			this.dtpOutDate.Size = new System.Drawing.Size(192, 21);
			this.dtpOutDate.TabIndex = 153;
			this.dtpOutDate.ValueChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblOutSeal
			// 
			this._lblOutSeal.Location = new System.Drawing.Point(15, 111);
			this._lblOutSeal.Name = "_lblOutSeal";
			this._lblOutSeal.Size = new System.Drawing.Size(96, 18);
			this._lblOutSeal.TabIndex = 150;
			this._lblOutSeal.Text = "Seal: ";
			this._lblOutSeal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtOutSeal
			// 
			this.txtOutSeal.Location = new System.Drawing.Point(123, 111);
			this.txtOutSeal.Name = "txtOutSeal";
			this.txtOutSeal.Size = new System.Drawing.Size(192, 21);
			this.txtOutSeal.TabIndex = 149;
			this.txtOutSeal.Text = "";
			this.txtOutSeal.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblOutCarrier
			// 
			this._lblOutCarrier.Location = new System.Drawing.Point(15, 51);
			this._lblOutCarrier.Name = "_lblOutCarrier";
			this._lblOutCarrier.Size = new System.Drawing.Size(96, 18);
			this._lblOutCarrier.TabIndex = 148;
			this._lblOutCarrier.Text = "Carrier: ";
			this._lblOutCarrier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cboOutCarrier
			// 
			this.cboOutCarrier.Location = new System.Drawing.Point(123, 51);
			this.cboOutCarrier.Name = "cboOutCarrier";
			this.cboOutCarrier.Size = new System.Drawing.Size(192, 21);
			this.cboOutCarrier.TabIndex = 146;
			this.cboOutCarrier.SelectionChangeCommitted += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblOutDriver
			// 
			this._lblOutDriver.Location = new System.Drawing.Point(15, 81);
			this._lblOutDriver.Name = "_lblOutDriver";
			this._lblOutDriver.Size = new System.Drawing.Size(96, 18);
			this._lblOutDriver.TabIndex = 147;
			this._lblOutDriver.Text = "Driver: ";
			this._lblOutDriver.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtOutDriver
			// 
			this.txtOutDriver.Location = new System.Drawing.Point(123, 81);
			this.txtOutDriver.Name = "txtOutDriver";
			this.txtOutDriver.Size = new System.Drawing.Size(192, 21);
			this.txtOutDriver.TabIndex = 145;
			this.txtOutDriver.Text = "";
			this.txtOutDriver.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblTrailer
			// 
			this._lblTrailer.Location = new System.Drawing.Point(54, 90);
			this._lblTrailer.Name = "_lblTrailer";
			this._lblTrailer.Size = new System.Drawing.Size(72, 18);
			this._lblTrailer.TabIndex = 146;
			this._lblTrailer.Text = "Trailer#: ";
			this._lblTrailer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtTrailer
			// 
			this.txtTrailer.Location = new System.Drawing.Point(135, 93);
			this.txtTrailer.Name = "txtTrailer";
			this.txtTrailer.Size = new System.Drawing.Size(192, 21);
			this.txtTrailer.TabIndex = 145;
			this.txtTrailer.Text = "";
			this.txtTrailer.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// dlgTrailerEntry
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(664, 446);
			this.Controls.Add(this.tabMain);
			this.Controls.Add(this._lblCreatedDate);
			this.Controls.Add(this.dtpCreatedDate);
			this.Controls.Add(this.txtCreatedBy);
			this.Controls.Add(this._lblCreatedBy);
			this.Controls.Add(this._lblTitle);
			this.Controls.Add(this.txtComments);
			this.Controls.Add(this._lblComments);
			this.Controls.Add(this._lblTrailer);
			this.Controls.Add(this.txtTrailer);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.Name = "dlgTrailerEntry";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.Controls.SetChildIndex(this.txtTrailer, 0);
			this.Controls.SetChildIndex(this._lblTrailer, 0);
			this.Controls.SetChildIndex(this._lblComments, 0);
			this.Controls.SetChildIndex(this.txtComments, 0);
			this.Controls.SetChildIndex(this.btnOK, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this._lblTitle, 0);
			this.Controls.SetChildIndex(this._lblCreatedBy, 0);
			this.Controls.SetChildIndex(this.txtCreatedBy, 0);
			this.Controls.SetChildIndex(this.dtpCreatedDate, 0);
			this.Controls.SetChildIndex(this._lblCreatedDate, 0);
			this.Controls.SetChildIndex(this.tabMain, 0);
			this.tabMain.ResumeLayout(false);
			this.tabIncoming.ResumeLayout(false);
			this.tabMoves.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdMoves)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mMovesDS)).EndInit();
			this.tabOutgoing.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
				//Load selection lists
				try {
					base.LoadSelections(base.mEntry.EntryType, this.cboInCarrier);
					base.LoadSelections(base.mEntry.EntryType, this.cboOutCarrier);
				} catch(Exception) { }
				
				#region Grid control
				this.grdMoves.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
				this.grdMoves.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
				this.grdMoves.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.True;
				this.grdMoves.DisplayLayout.Bands[0].Override.AllowDelete = DefaultableBoolean.True;
				this.grdMoves.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.Edit;
				this.grdMoves.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdMoves.DisplayLayout.Bands[0].Columns["ID"].CellActivation = Activation.NoEdit;
				this.grdMoves.DisplayLayout.Bands[0].Columns["Requested"].CellActivation = Activation.NoEdit;
				this.grdMoves.DisplayLayout.Bands[0].Columns["MoveFrom"].CellActivation = Activation.AllowEdit;
				this.grdMoves.DisplayLayout.Bands[0].Columns["MoveTo"].CellActivation = Activation.AllowEdit;
				this.grdMoves.DisplayLayout.Bands[0].Columns["Switcher"].CellActivation = Activation.AllowEdit;
				this.grdMoves.DisplayLayout.Bands[0].Columns["LoadedWith"].CellActivation = Activation.AllowEdit;
				this.grdMoves.DisplayLayout.Bands[0].Columns["ScheduledTime"].CellActivation = Activation.AllowEdit;
				this.grdMoves.DisplayLayout.Bands[0].Columns["ActualTime"].CellActivation = Activation.AllowEdit;
				this.grdMoves.DisplayLayout.Bands[0].Columns["Requested"].SortIndicator = SortIndicator.Ascending;
				#endregion
				//Load controls
				this.Text = base.mEntry.EntryType + "(" + base.mEntry.EntryID.ToString() + ")";
				TrailerEntry trailerEntry = (TrailerEntry)base.mEntry;
				this.txtCreatedBy.Text = trailerEntry.CreatedBy;
				this.dtpCreatedDate.Value = trailerEntry.Created;
				this.txtCreatedBy.Enabled = this.dtpCreatedDate.Enabled = false;
				this.txtTrailer.Text = trailerEntry.TrailerNumber;
				this.dtpInDate.Value = trailerEntry.IncomingDate;
				this.cboInCarrier.Text = trailerEntry.IncomingCarrier;
				this.txtInDriver.Text = trailerEntry.IncomingDriverName;
				this.txtInSeal.Text = trailerEntry.IncomingSeal;
				this.txtInLoc.Text = trailerEntry.InitialYardLocation;
				this.grdMoves.DataSource = trailerEntry.TrailerMoves;
				this.chkMoveInProgress.Checked = trailerEntry.MoveInProgress;
				this.chkLoadSheetReady.Checked = trailerEntry.LoadSheetReady;
				this.dtpOutDate.Value = trailerEntry.OutgoingDate;
				this.cboOutCarrier.Text = trailerEntry.OutgoingCarrier;
				this.txtOutDriver.Text = trailerEntry.OutgoingDriverName;
				this.txtOutSeal.Text = trailerEntry.OutgoingSeal;
				this.chkTrailerOut.Checked = trailerEntry.MovedOut;
				this.txtComments.Text = trailerEntry.Comments;
			} 
			catch(Exception ex) { base.reportError(ex); }
			finally { 
				OnValidateForm(null, null);
				base.btnOK.Enabled = false;
				this.Cursor = Cursors.Default; 
			}
		}
		private void OnFormClosing(object sender, System.ComponentModel.CancelEventArgs e) {
			//Event handler for form closing event
			try {
				//
				try {
					base.SaveSelections(base.mEntry.EntryType, this.cboInCarrier);
					base.SaveSelections(base.mEntry.EntryType, this.cboOutCarrier);
				} catch(Exception) { }
			}
			catch(Exception ex)  { base.reportError(ex); }
		}
		#region Grid Support
		private void OnCellActivating(object sender, Infragistics.Win.UltraWinGrid.CancelableCellEventArgs e) {
			//Event handler for cell activated
			try {
				//Provide dynamic (value-based) activation
				switch(e.Cell.Column.Key.ToString()) {
					case "RequestedBy":
						e.Cell.Activation = (e.Cell.Text.Length > 0) ? Activation.NoEdit : Activation.AllowEdit; 
						break;
					default:		
						e.Cell.Activation = Activation.AllowEdit;
						break;
				}
			} 
			catch(Exception) { }
		}
		private void OnCellChanged(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e) {
			//Event handler for change in a cell value
			try {
				//If "RequestedBy" cell, set or clear "Requested" cell required value
				if(e.Cell.Column.Key.ToString()=="RequestedBy") {
					if(e.Cell.Text.Length > 0) {
						e.Cell.Row.Cells["ID"].Value = 0;
						e.Cell.Row.Cells["Requested"].Value = DateTime.Now;
					}
					else {
						e.Cell.Row.Cells["ID"].Value = DBNull.Value;
						e.Cell.Row.Cells["Requested"].Value = DBNull.Value;
					}
					this.OnValidateForm(null, null);
				}
			} 
			catch(Exception) { }
		}
		private void OnRowChanged(object sender, Infragistics.Win.UltraWinGrid.RowEventArgs e) {
			//
			try {
				//
			} 
			catch(Exception) { }
			finally { OnValidateForm(null, null); }
		}
		#endregion
		protected override void OnValidateForm(object sender, EventArgs e) {
			//Set user services
			try {
				//Set menu/context menu states
				base.btnOK.Enabled = (this.txtCreatedBy.Text!="" && this.dtpCreatedDate.Value>DateTime.MinValue);
			}
			catch(Exception ex)  { base.reportError(ex); }
		}
		protected override void UpdateEntry() { 
			//Update this entry
			try {
				//
				TrailerEntry trailerEntry = (TrailerEntry)base.mEntry;
				trailerEntry.CreatedBy = this.txtCreatedBy.Text;
				trailerEntry.Created = this.dtpCreatedDate.Value;
				trailerEntry.TrailerNumber = this.txtTrailer.Text;
				trailerEntry.IncomingDate = this.dtpInDate.Value;
				trailerEntry.IncomingCarrier = this.cboInCarrier.Text;
				trailerEntry.IncomingDriverName = this.txtInDriver.Text;
				trailerEntry.IncomingSeal = this.txtInSeal.Text;
				trailerEntry.InitialYardLocation = this.txtInLoc.Text;
				//trailerEntry.TrailerMoves.TrailerMoveTable.AcceptChanges();	//Need row states
				trailerEntry.MoveInProgress = this.chkMoveInProgress.Checked;
				trailerEntry.LoadSheetReady = this.chkLoadSheetReady.Checked;
				trailerEntry.OutgoingDate = this.dtpOutDate.Value;
				trailerEntry.OutgoingCarrier = this.cboOutCarrier.Text;
				trailerEntry.OutgoingDriverName = this.txtOutDriver.Text;
				trailerEntry.OutgoingSeal = this.txtOutSeal.Text;
				trailerEntry.MovedOut = this.chkTrailerOut.Checked;
				trailerEntry.Comments = this.txtComments.Text;
			}
			catch(Exception ex)  { base.reportError(ex); }
		}
	}
}

