using System;
using System.Diagnostics;
using System.Drawing;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace Argix.Freight {
	//
	public class dlgAssignmentDetail : System.Windows.Forms.Form {
		//Members
		private DialogActionEnum mDialogAction=DialogActionEnum.DialogActionAssign;
		private IBShipment mShipment=null;
		private int mScreenID=0;
		
		#region Controls

		private System.ComponentModel.Container components = null;		//Required designer variable.
		private System.Windows.Forms.Button cmdNext;
		private System.Windows.Forms.Button cmdCancel;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdSortStations;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdFreightTypes;
		private System.Windows.Forms.Label lblInstructions;
		private System.Windows.Forms.PictureBox picDialog;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdAssignments;
		private Argix.SelectionList mFreightSortTypesDS;
        private Argix.Freight.WorkstationDS mSortStationsDS;
        private Argix.Freight.FreightAssignDS mAssignmentsDS;
		private System.Windows.Forms.Button cmdBack;

		#endregion
		#region Constants
		private const string CMD_CANCEL = "&Cancel";
		private const string CMD_CLOSE = "&Close";
		private const string CMD_BACK = "< &Back";
		private const string CMD_NEXT = "&Next >";
		private const string CMD_FINISH = "&Finish";
		
		private const string INSTR_0 = "Please select a sort type for the freight.";
		private const string INSTR_1 = "Please select one or more sort stations.";
		private const string INSTR_21 = "Click Finish to create new freight assignments on these sort stations.";
		private const string INSTR_22 = "Click Finish to remove these freight assignments from their sort stations.";
		private const string INSTR_23 = "Click Finish to remove this freight assignment from the sort station.";
		#endregion
		private const string EX_RESULT_OK = "OK.";
		private const string EX_RESULT_FAILED = "Failed.";
				
		//Interface
		public dlgAssignmentDetail(DialogActionEnum eDialogAction, IBShipment shipment, string workstationID) {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				this.cmdCancel.Text = CMD_CANCEL;
				this.cmdBack.Text = CMD_BACK;
				this.cmdNext.Text = CMD_NEXT;
				this.mShipment = shipment;
				switch(this.mDialogAction = eDialogAction) {
					case DialogActionEnum.DialogActionAssign:		
						this.Text = "Assign Freight To Sort Stations"; 
						this.mAssignmentsDS.Clear();
						break;
					case DialogActionEnum.DialogActionUnassignAny:	
						this.Text = "Unassign Freight From Sort Stations"; 
						this.mAssignmentsDS.Merge(FreightFactory.GetAssignments(this.mShipment.FreightID, ""));
						break;
					case DialogActionEnum.DialogActionUnassign:		
						this.Text = "Unassign Freight From Sort Station"; 
						this.mAssignmentsDS.Merge(FreightFactory.GetAssignments(this.mShipment.FreightID, workstationID));
						break;
				}
			}
			catch(Exception ex) { throw new ApplicationException("Could not crate new Assignment Detail dialog.", ex); }
		}
		protected override void Dispose( bool disposing ) { if( disposing ) { if(components != null) components.Dispose(); } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("WorkstationTable",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("WorkStationID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Number");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScaleType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScalePort");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PrinterType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PrinterPort");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsVirtual");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Trace");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsActive");
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("SelectionList2Table",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAssignmentDetail));
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("StationFreightAssignmentTable",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("WorkStationID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StationNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FreightID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FreightType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortTypeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TDSNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Client");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Shipper");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Pickup");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Result");
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            this.cmdNext = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.grdSortStations = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mSortStationsDS = new Argix.Freight.WorkstationDS();
            this.grdFreightTypes = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mFreightSortTypesDS = new Argix.SelectionList();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.picDialog = new System.Windows.Forms.PictureBox();
            this.grdAssignments = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mAssignmentsDS = new Argix.Freight.FreightAssignDS();
            this.cmdBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdSortStations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSortStationsDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFreightTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mFreightSortTypesDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDialog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAssignments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mAssignmentsDS)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdNext
            // 
            this.cmdNext.BackColor = System.Drawing.SystemColors.Control;
            this.cmdNext.Enabled = false;
            this.cmdNext.Font = new System.Drawing.Font("Arial",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.cmdNext.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdNext.Location = new System.Drawing.Point(174,234);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(96,24);
            this.cmdNext.TabIndex = 0;
            this.cmdNext.Text = "Next >";
            this.cmdNext.UseVisualStyleBackColor = false;
            this.cmdNext.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // cmdCancel
            // 
            this.cmdCancel.BackColor = System.Drawing.SystemColors.Control;
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Font = new System.Drawing.Font("Arial",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdCancel.Location = new System.Drawing.Point(276,234);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(96,24);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = false;
            this.cmdCancel.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // grdSortStations
            // 
            this.grdSortStations.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdSortStations.DataMember = "WorkstationTable";
            this.grdSortStations.DataSource = this.mSortStationsDS;
            appearance14.BackColor = System.Drawing.SystemColors.Window;
            appearance14.FontData.Name = "Verdana";
            appearance14.FontData.SizeInPoints = 8F;
            appearance14.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance14.TextHAlignAsString = "Left";
            this.grdSortStations.DisplayLayout.Appearance = appearance14;
            ultraGridBand1.AddButtonCaption = "SelectionListTable";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn1.Width = 96;
            ultraGridColumn2.Header.VisiblePosition = 3;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn2.Width = 96;
            ultraGridColumn3.Header.VisiblePosition = 4;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn4.Header.VisiblePosition = 1;
            ultraGridColumn4.Width = 72;
            ultraGridColumn5.Header.VisiblePosition = 2;
            ultraGridColumn5.Width = 96;
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.Hidden = true;
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn7.Hidden = true;
            ultraGridColumn8.Header.VisiblePosition = 7;
            ultraGridColumn8.Hidden = true;
            ultraGridColumn9.Header.VisiblePosition = 8;
            ultraGridColumn9.Hidden = true;
            ultraGridColumn10.Header.VisiblePosition = 9;
            ultraGridColumn10.Hidden = true;
            ultraGridColumn11.Header.VisiblePosition = 10;
            ultraGridColumn11.Hidden = true;
            ultraGridColumn12.Header.VisiblePosition = 11;
            ultraGridColumn12.Hidden = true;
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
            ultraGridColumn12});
            this.grdSortStations.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdSortStations.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
            appearance15.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance15.FontData.BoldAsString = "True";
            appearance15.FontData.Name = "Verdana";
            appearance15.FontData.SizeInPoints = 8F;
            appearance15.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance15.TextHAlignAsString = "Left";
            this.grdSortStations.DisplayLayout.CaptionAppearance = appearance15;
            this.grdSortStations.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdSortStations.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdSortStations.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdSortStations.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance16.BackColor = System.Drawing.SystemColors.Control;
            appearance16.FontData.BoldAsString = "True";
            appearance16.FontData.Name = "Verdana";
            appearance16.FontData.SizeInPoints = 8F;
            appearance16.TextHAlignAsString = "Left";
            this.grdSortStations.DisplayLayout.Override.HeaderAppearance = appearance16;
            this.grdSortStations.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdSortStations.DisplayLayout.Override.MaxSelectedRows = 0;
            appearance17.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdSortStations.DisplayLayout.Override.RowAppearance = appearance17;
            this.grdSortStations.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdSortStations.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended;
            this.grdSortStations.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
            this.grdSortStations.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdSortStations.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdSortStations.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdSortStations.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdSortStations.Font = new System.Drawing.Font("Arial",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.grdSortStations.Location = new System.Drawing.Point(174,57);
            this.grdSortStations.Name = "grdSortStations";
            this.grdSortStations.Size = new System.Drawing.Size(198,168);
            this.grdSortStations.TabIndex = 3;
            this.grdSortStations.Text = "Sort Stations";
            this.grdSortStations.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdSortStations.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnSortStationSelected);
            this.grdSortStations.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnSortStationKeyDown);
            // 
            // mSortStationsDS
            // 
            this.mSortStationsDS.DataSetName = "WorkstationDS";
            this.mSortStationsDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mSortStationsDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grdFreightTypes
            // 
            this.grdFreightTypes.DataMember = "SelectionList2Table";
            this.grdFreightTypes.DataSource = this.mFreightSortTypesDS;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.FontData.Name = "Verdana";
            appearance5.FontData.SizeInPoints = 8F;
            appearance5.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance5.TextHAlignAsString = "Left";
            this.grdFreightTypes.DisplayLayout.Appearance = appearance5;
            ultraGridBand2.AddButtonCaption = "BwareStationTripTable";
            ultraGridColumn13.Header.VisiblePosition = 0;
            ultraGridColumn14.Header.VisiblePosition = 1;
            ultraGridColumn14.Width = 176;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn13,
            ultraGridColumn14});
            this.grdFreightTypes.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            appearance6.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance6.FontData.BoldAsString = "True";
            appearance6.FontData.Name = "Verdana";
            appearance6.FontData.SizeInPoints = 8F;
            appearance6.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance6.TextHAlignAsString = "Left";
            this.grdFreightTypes.DisplayLayout.CaptionAppearance = appearance6;
            this.grdFreightTypes.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdFreightTypes.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdFreightTypes.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdFreightTypes.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance7.BackColor = System.Drawing.SystemColors.Control;
            appearance7.FontData.BoldAsString = "True";
            appearance7.FontData.Name = "Verdana";
            appearance7.FontData.SizeInPoints = 8F;
            appearance7.TextHAlignAsString = "Left";
            this.grdFreightTypes.DisplayLayout.Override.HeaderAppearance = appearance7;
            this.grdFreightTypes.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdFreightTypes.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance8.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdFreightTypes.DisplayLayout.Override.RowAppearance = appearance8;
            this.grdFreightTypes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdFreightTypes.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdFreightTypes.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdFreightTypes.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdFreightTypes.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdFreightTypes.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdFreightTypes.Location = new System.Drawing.Point(174,57);
            this.grdFreightTypes.Name = "grdFreightTypes";
            this.grdFreightTypes.Size = new System.Drawing.Size(198,168);
            this.grdFreightTypes.TabIndex = 2;
            this.grdFreightTypes.Text = "Freight Sort Types";
            this.grdFreightTypes.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdFreightTypes.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnFreightSortTypeSelected);
            // 
            // mFreightSortTypesDS
            // 
            this.mFreightSortTypesDS.DataSetName = "SelectionList";
            this.mFreightSortTypesDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mFreightSortTypesDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // lblInstructions
            // 
            this.lblInstructions.BackColor = System.Drawing.Color.Transparent;
            this.lblInstructions.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblInstructions.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lblInstructions.Location = new System.Drawing.Point(174,12);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(198,36);
            this.lblInstructions.TabIndex = 6;
            this.lblInstructions.Text = "Please select a sort type for this freight.";
            // 
            // picDialog
            // 
            this.picDialog.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picDialog.Image = ((System.Drawing.Image)(resources.GetObject("picDialog.Image")));
            this.picDialog.Location = new System.Drawing.Point(6,12);
            this.picDialog.Name = "picDialog";
            this.picDialog.Size = new System.Drawing.Size(162,213);
            this.picDialog.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDialog.TabIndex = 0;
            this.picDialog.TabStop = false;
            // 
            // grdAssignments
            // 
            this.grdAssignments.DataMember = "StationFreightAssignmentTable";
            this.grdAssignments.DataSource = this.mAssignmentsDS;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.FontData.Name = "Verdana";
            appearance9.FontData.SizeInPoints = 8F;
            appearance9.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance9.TextHAlignAsString = "Left";
            this.grdAssignments.DisplayLayout.Appearance = appearance9;
            ultraGridBand3.AddButtonCaption = "FreightAssignmentDetailTable";
            ultraGridColumn15.Header.VisiblePosition = 5;
            ultraGridColumn15.Hidden = true;
            ultraGridColumn16.Header.Caption = "Station#";
            ultraGridColumn16.Header.VisiblePosition = 1;
            ultraGridColumn16.Width = 60;
            ultraGridColumn17.Header.VisiblePosition = 0;
            ultraGridColumn17.Hidden = true;
            ultraGridColumn18.Header.VisiblePosition = 3;
            ultraGridColumn19.Header.VisiblePosition = 2;
            ultraGridColumn19.Hidden = true;
            ultraGridColumn20.Header.VisiblePosition = 6;
            ultraGridColumn21.Header.Caption = "TDS#";
            ultraGridColumn21.Header.VisiblePosition = 4;
            ultraGridColumn21.Hidden = true;
            ultraGridColumn21.Width = 60;
            ultraGridColumn22.Header.VisiblePosition = 8;
            ultraGridColumn22.Hidden = true;
            ultraGridColumn23.Header.VisiblePosition = 9;
            ultraGridColumn23.Hidden = true;
            ultraGridColumn24.Header.VisiblePosition = 10;
            ultraGridColumn24.Hidden = true;
            ultraGridColumn25.Header.VisiblePosition = 11;
            ultraGridColumn25.Hidden = true;
            ultraGridColumn26.Header.VisiblePosition = 12;
            ultraGridColumn26.Hidden = true;
            ultraGridColumn27.Header.VisiblePosition = 7;
            ultraGridColumn27.Width = 384;
            ultraGridBand3.Columns.AddRange(new object[] {
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
            ultraGridColumn27});
            this.grdAssignments.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
            appearance10.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance10.FontData.BoldAsString = "True";
            appearance10.FontData.Name = "Verdana";
            appearance10.FontData.SizeInPoints = 8F;
            appearance10.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance10.TextHAlignAsString = "Left";
            this.grdAssignments.DisplayLayout.CaptionAppearance = appearance10;
            appearance11.BackColor = System.Drawing.SystemColors.Control;
            appearance11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grdAssignments.DisplayLayout.Override.ActiveRowAppearance = appearance11;
            this.grdAssignments.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdAssignments.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdAssignments.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdAssignments.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.grdAssignments.DisplayLayout.Override.CellMultiLine = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.Control;
            appearance12.FontData.BoldAsString = "True";
            appearance12.FontData.Name = "Verdana";
            appearance12.FontData.SizeInPoints = 8F;
            appearance12.ForeColor = System.Drawing.SystemColors.ControlText;
            appearance12.TextHAlignAsString = "Left";
            this.grdAssignments.DisplayLayout.Override.HeaderAppearance = appearance12;
            this.grdAssignments.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdAssignments.DisplayLayout.Override.MaxSelectedCells = 1;
            this.grdAssignments.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance13.BackColor = System.Drawing.SystemColors.Control;
            appearance13.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.grdAssignments.DisplayLayout.Override.RowAppearance = appearance13;
            this.grdAssignments.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdAssignments.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdAssignments.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdAssignments.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdAssignments.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdAssignments.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdAssignments.Location = new System.Drawing.Point(174,57);
            this.grdAssignments.Name = "grdAssignments";
            this.grdAssignments.Size = new System.Drawing.Size(198,168);
            this.grdAssignments.TabIndex = 4;
            this.grdAssignments.Text = "Assignment Summary";
            this.grdAssignments.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            // 
            // mAssignmentsDS
            // 
            this.mAssignmentsDS.DataSetName = "FreightAssignDS";
            this.mAssignmentsDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mAssignmentsDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cmdBack
            // 
            this.cmdBack.BackColor = System.Drawing.SystemColors.Control;
            this.cmdBack.Enabled = false;
            this.cmdBack.Font = new System.Drawing.Font("Arial",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.cmdBack.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdBack.Location = new System.Drawing.Point(72,234);
            this.cmdBack.Name = "cmdBack";
            this.cmdBack.Size = new System.Drawing.Size(96,24);
            this.cmdBack.TabIndex = 5;
            this.cmdBack.Text = "< Back";
            this.cmdBack.UseVisualStyleBackColor = false;
            this.cmdBack.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // dlgAssignmentDetail
            // 
            this.AcceptButton = this.cmdNext;
            this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(378,263);
            this.Controls.Add(this.cmdBack);
            this.Controls.Add(this.picDialog);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdNext);
            this.Controls.Add(this.grdAssignments);
            this.Controls.Add(this.grdFreightTypes);
            this.Controls.Add(this.grdSortStations);
            this.Font = new System.Drawing.Font("Arial",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAssignmentDetail";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Assign Freight To Sort Stations";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.grdSortStations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSortStationsDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFreightTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mFreightSortTypesDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDialog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAssignments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mAssignmentsDS)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
				//Set initial conditions
				this.grdFreightTypes.DisplayLayout.Bands[0].Columns["ID"].SortIndicator = SortIndicator.Ascending;
				this.grdSortStations.DisplayLayout.Bands[0].Columns["Number"].SortIndicator = SortIndicator.Ascending;
				this.grdAssignments.DisplayLayout.Bands[0].Columns["StationNumber"].SortIndicator = SortIndicator.Ascending;
				switch(this.mDialogAction) {
					case DialogActionEnum.DialogActionAssign: 
						//Assignment- show freight sort types for selection.
						//Note: If only 1 sort type, select for user and go to next screen
						this.mScreenID = 0;
						this.mFreightSortTypesDS.Merge(FreightFactory.GetFreightSortTypes(this.mShipment.FreightID));
						if(this.grdFreightTypes.Rows.Count > 0) {
							this.grdFreightTypes.Rows[0].Selected = true;
							this.grdFreightTypes.Rows[0].Activate();
						}
						if(this.grdFreightTypes.Rows.Count == 1) this.mScreenID = 1;
						break;
					case DialogActionEnum.DialogActionUnassignAny: 
						//Delete one or more assignments (as selected by user)
						//Transfer assignments into sort stations for user selection
						this.mScreenID = 1;
						for(int i=0; i<this.mAssignmentsDS.StationFreightAssignmentTable.Rows.Count; i++) {
							WorkstationDS.WorkstationTableRow row = this.mSortStationsDS.WorkstationTable.NewWorkstationTableRow();
							row.WorkStationID = this.mAssignmentsDS.StationFreightAssignmentTable[i].WorkStationID;
							row.Number = (!this.mAssignmentsDS.StationFreightAssignmentTable[i].IsStationNumberNull()) ? this.mAssignmentsDS.StationFreightAssignmentTable[i].StationNumber : "?";
							row.Description = "";
							this.mSortStationsDS.WorkstationTable.AddWorkstationTableRow(row);
						}
						break;
					case DialogActionEnum.DialogActionUnassign: 
						//Delete a single assignment
						this.mScreenID = 2;
						break;
				}
				setDialogLayout();
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnFreightSortTypeSelected(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for freight sort type selected
			this.cmdNext.Enabled = true;
		}
		private void OnSortStationSelected(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for sort station selected
			try {
				this.cmdNext.Enabled = (this.grdSortStations.Selected.Rows.Count>0);
				this.grdSortStations.Focus();
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnSortStationKeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			//Event handler for sort station key down
			try {
				if(!e.Control && e.KeyValue==32)
					e.Handled = true;
				else if(e.Control && e.KeyValue==32)
					this.grdSortStations.ActiveRow.Selected = (!this.grdSortStations.ActiveRow.Selected);
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnCmdClick(object sender, System.EventArgs e) {
			//Command services
			try {
				Button btn = (Button)sender;
				switch(btn.Text) {
					case CMD_CANCEL:	this.DialogResult = DialogResult.Cancel; this.Close(); break;
					case CMD_CLOSE:		this.DialogResult = DialogResult.OK; this.Close(); break;
					case CMD_BACK:		this.mScreenID -= 1; setDialogLayout(); break;
					case CMD_NEXT:		this.mScreenID += 1; setDialogLayout(); break;
					case CMD_FINISH:
						this.cmdCancel.Text = CMD_CLOSE;
						this.cmdBack.Enabled = this.cmdNext.Enabled = this.cmdCancel.Enabled = false;
						if(this.mDialogAction==DialogActionEnum.DialogActionAssign) { 					
							//Create the requested freight assignments
							if(freightStarted()) {
								if(assignFreight() == true) { 
									this.DialogResult = DialogResult.OK;
									Thread.Sleep(1000);
									this.Close();
								}
							}
							else {
								this.DialogResult = DialogResult.OK;
								this.Close();
							}
						}
						else {
							//Delete the selected station assignments
							if(unassignFreight() == true) {
								this.DialogResult = DialogResult.OK;
								Thread.Sleep(1000);
								this.Close();
							}
						}
						break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { this.cmdCancel.Enabled = true; this.Cursor = Cursors.Default; }
		}
		#region Local Services: freightStarted(),assignFreight(),unassignFreight(),setDialogLayout()
		private bool freightStarted() {
			//Set freight status to started if applicable
			DialogResult res=DialogResult.None;
			bool bStarted=true;
			try {
                if(this.mShipment.Status==ShipmentStatusEnum.Unsorted) {
					//Before we can make assignment we need to make sure we can set the freight status to 
					//sorting (sort started); no exception is thrown if the status cannot be changed
					bStarted=false;
					res = MessageBox.Show(this, "The selected freight must be 'sorting' prior to assignment. Would you like to start sort for this freight?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if(res==DialogResult.Yes) {
						//Update a shipment as sorting
						//Double check result on exception: freight can be assigned if status is sorting
						try {
							bStarted = FreightFactory.StartSort(this.mShipment);
						}
                        catch(Exception) { bStarted = FreightFactory.IsSortStarted(this.mShipment); }
						if(!bStarted) 
							App.ReportError(new ApplicationException("Sorting could not be started for freight " + this.mShipment.FreightID + "(VendorKey=" + this.mShipment.VendorKey + "; Status=" + this.mShipment.Status + "). Please refresh and try again."), true, LogLevel.Error);
					}
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			return bStarted;
		}
		private bool assignFreight() {
			bool bOK=true;
			this.Cursor = Cursors.WaitCursor;
			try {
				//Create station freight assignments
				foreach(FreightAssignDS.StationFreightAssignmentTableRow row in this.mAssignmentsDS.StationFreightAssignmentTable.Rows) {
                    WorkstationDS.WorkstationTableRow ws = new WorkstationDS().WorkstationTable.NewWorkstationTableRow();	
                    ws.WorkStationID = row.WorkStationID;
					ws.Number = row.StationNumber;
                    Workstation station = new Workstation(ws);
                    bool created = false;
					try {
						created = (FreightFactory.CreateAssignment(station, this.mShipment, row.SortTypeID, "Assigned") != null);
					}
					catch(ApplicationException ex) { App.ReportError(ex, true, LogLevel.Error); }
					if(!created) bOK = false;
					row.Result = (!created) ? EX_RESULT_FAILED : EX_RESULT_OK;
					this.grdAssignments.Refresh();
					Application.DoEvents();
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); } 
			return bOK;
		}
		private bool unassignFreight() {
			//Unassign one or more station assignments
			bool bOK=true;
			this.Cursor = Cursors.WaitCursor;
			try {
				foreach(FreightAssignDS.StationFreightAssignmentTableRow row in this.mAssignmentsDS.StationFreightAssignmentTable.Rows) {
                    WorkstationDS.WorkstationTableRow ws = new WorkstationDS().WorkstationTable.NewWorkstationTableRow();
					ws.WorkStationID = row.WorkStationID;
					ws.Number = row.StationNumber;
                    Workstation station = new Workstation(ws);
                    InboundFreightDS.InboundFreightTableRow ibf = new InboundFreightDS().InboundFreightTable.NewInboundFreightTableRow();
                    ibf.FreightID = row.FreightID;
                    ibf.TDSNumber = row.TDSNumber;
                    ibf.ClientNumber = ibf.ClientName = row.Client;
                    IBShipment shipment = new IBShipment(ibf);
                    StationAssignment assignment = new StationAssignment(station,shipment,row.SortTypeID);
					bool deleted=false;
					try {
						deleted = FreightFactory.DeleteAssignment(assignment, "Unassigned");
					}
					catch(ApplicationException ex) { App.ReportError(ex, true, LogLevel.Error); }
					catch(Exception ex) { App.ReportError(new ApplicationException("Failed to unassign freight " + row.FreightID + " from station " + row.WorkStationID + " (sorttypeID= " + row.SortTypeID.ToString() + ").", ex), true, LogLevel.Error); }
					if(!deleted) bOK = false;
					row.Result = (!deleted) ? EX_RESULT_FAILED : EX_RESULT_OK;
					this.grdAssignments.Refresh();
					Application.DoEvents();
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); } 
			return bOK;
		}
		private void setDialogLayout() {
			try {
				this.grdFreightTypes.Visible = (this.mScreenID == 0);
				this.grdSortStations.Visible = (this.mScreenID == 1);
				this.grdAssignments.Visible = (this.mScreenID == 2);
				switch(this.mScreenID) {
					case 0: 
						//Setup for sort type selection
						this.lblInstructions.Text = INSTR_0;
						this.cmdBack.Enabled = false;
						this.cmdNext.Text = CMD_NEXT;
						this.cmdNext.Enabled = (this.grdFreightTypes.Selected.Rows.Count > 0);
						break;
					case 1: 
						//Setup for sort station selection
						this.lblInstructions.Text = INSTR_1;
						this.cmdBack.Enabled = ((this.mDialogAction == DialogActionEnum.DialogActionAssign) && (this.grdFreightTypes.Rows.Count > 1));
						this.cmdNext.Text = CMD_NEXT;
						this.cmdNext.Enabled = (this.grdSortStations.Selected.Rows.Count > 0);
						if(this.mSortStationsDS.WorkstationTable.Rows.Count == 0) {
							int iSortTypeID = Convert.ToInt32(this.grdFreightTypes.Selected.Rows[0].Cells["ID"].Value);
                            this.mSortStationsDS.Merge(FreightFactory.GetAssignableSortStations(this.mShipment.FreightID,iSortTypeID));
							if(this.grdSortStations.Rows.Count > 0) this.grdSortStations.Rows[0].Activate();
						}
						this.grdSortStations.ActiveRow = null;
						this.grdSortStations.Focus();
						break;
					case 2: 
						//Setup for assignments view
						switch(this.mDialogAction) {
							case DialogActionEnum.DialogActionAssign:		this.lblInstructions.Text = INSTR_21; break;
							case DialogActionEnum.DialogActionUnassignAny:	this.lblInstructions.Text = INSTR_22; break;
							case DialogActionEnum.DialogActionUnassign:		this.lblInstructions.Text = INSTR_23; break;
						}
						this.cmdBack.Enabled = (this.mDialogAction != DialogActionEnum.DialogActionUnassign);
						this.cmdNext.Text = CMD_FINISH;
						this.cmdNext.Enabled = true;
						
						//Create station freight assignments dataset for last grid view
						//Note: The single unassign assignment was merged into mAssignmentsDS in the constructor (DO NOT SET HERE)
						if(this.mDialogAction != DialogActionEnum.DialogActionUnassign) {
							this.mAssignmentsDS.Clear();
							for(int i=0; i<this.grdSortStations.Selected.Rows.Count; i++) {
								FreightAssignDS.StationFreightAssignmentTableRow row = this.mAssignmentsDS.StationFreightAssignmentTable.NewStationFreightAssignmentTableRow();
								row.WorkStationID = this.grdSortStations.Selected.Rows[i].Cells["WorkstationID"].Value.ToString();
								row.StationNumber = this.grdSortStations.Selected.Rows[i].Cells["Number"].Value.ToString();
								row.FreightID = this.mShipment.FreightID;
								row.TDSNumber = this.mShipment.TDSNumber;
                                row.Client = this.mShipment.ClientNumber;
								row.SortTypeID = (this.mDialogAction==DialogActionEnum.DialogActionAssign) ? Convert.ToInt32(this.grdFreightTypes.Selected.Rows[0].Cells["ID"].Value) : 0;
								this.mAssignmentsDS.StationFreightAssignmentTable.AddStationFreightAssignmentTableRow(row);
							}
                            this.mAssignmentsDS.AcceptChanges();
						}
						break;
				}
			} 
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); } 
		}
		#endregion
	}
}
