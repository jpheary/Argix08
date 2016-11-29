//	Roles:
//			LineHaulOperator
//			- view any ship schedule
//			- update Tractor#, Freight Assigned, Trailer Complete, Paper Complete, and Trailer Dispatch, Driver
//			- print and export any ship schedule
//			LineHaulCoordinator
//			- all features of LineHaulOperator
//			- can modify all editable fields including Close Time/Day, Trailer, Carrier, Depart Time/Day, Notes, Arrival Date/Time, OFD Date
//			LineHaulAdministrator
//			- all features of LineHaulCoordinator
//			- create new ship schedules, add new loads (from templates), cancel existing loads, 
//			  and send ship schedules reports to agents and carriers
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix;
using Argix.Windows;
using Argix.Enterprise;
using Argix.Security;

namespace Argix.AgentLineHaul {
	//
	public class winSchedule : System.Windows.Forms.Form {
		//Members
		private ShipSchedule mSchedule=null;
		
		#region Controls

		private Infragistics.Win.UltraWinGrid.UltraGrid grdSchedule;
		private Infragistics.Win.UltraWinGrid.UltraDropDown uddSortCenter;
        private Argix.AgentLineHaul.ShipScheduleDS mLoadDS;
        private Infragistics.Win.UltraWinGrid.UltraDropDown uddCarrier;
        private ContextMenuStrip csLoad;
        private ToolStripMenuItem ctxRefresh;
        private ToolStripSeparator ctxSep1;
        private ToolStripMenuItem ctxCut;
        private ToolStripMenuItem ctxCopy;
        private ToolStripMenuItem ctxPaste;
        private ToolStripSeparator ctxSep2;
        private ToolStripMenuItem ctxCancelLoad;
        private System.ComponentModel.IContainer components;
		#endregion
		private const int KEYSTATE_SHIFT = 5;
		private const int KEYSTATE_CTL = 9;
		
		public event StatusEventHandler StatusMessage=null;
		public event EventHandler ServiceStatesChanged=null;
		
		//Interface
		public winSchedule(ShipSchedule schedule) {
			//Constructor
			try {
				InitializeComponent();
				Infragistics.Win.ValueListsCollection lists = this.grdSchedule.DisplayLayout.ValueLists;
				Infragistics.Win.ValueList vl = lists.Add("YesNoValueList");
				vl.ValueListItems.Add(0,"Yes");
				vl.ValueListItems.Add(0,"No");
				
				//Init objects
				this.mSchedule = schedule;
				this.mSchedule.Changed += new EventHandler(OnScheduleChanged);
				this.Text = this.mSchedule.SortCenter + ", " + this.mSchedule.ScheduleDate.ToShortDateString();
				this.grdSchedule.Text = "Ship Schedule: " + this.mSchedule.SortCenter + ", " + this.mSchedule.ScheduleDate.ToShortDateString();
			}
			catch(Exception ex) { throw new ApplicationException("Failed to create new winSchedule.", ex); }
		}
		protected override void Dispose( bool disposing ) { if(disposing) { if (components != null) { components.Dispose(); } } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ShipScheduleTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduleID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenterID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenter");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduleDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TripID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TemplateID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("BolNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CarrierID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CarrierServiceID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Carrier");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NextCarrier");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LoadNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TractorNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DriverName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledClose");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledDeparture");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsMandatory");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FreightAssigned");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerComplete");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PaperworkComplete");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerDispatched");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Canceled");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SCDEUserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SCDELastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SCDERowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MainZone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Tag");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Notes");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledArrival");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledOFD1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S1UserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S1LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S1RowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2StopID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn41 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2StopNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn43 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn44 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2MainZone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn45 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2Tag");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn46 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2Notes");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn47 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ScheduledArrival");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn48 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ScheduledOFD1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn49 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2UserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn50 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn51 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2RowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn52 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ArrivalDay", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn53 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ArrivalTime", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn54 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ArrivalDay", 2);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn55 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ArrivalTime", 3);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn56 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerComplete2", 4);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn57 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PaperworkComplete2", 5);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn58 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerDispatched2", 6);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn59 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ZoneTag", 7);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn60 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ZoneTag", 8);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn61 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FreightAssigned2", 9);
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(winSchedule));
            this.grdSchedule = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.csLoad = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxCut = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxCancelLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.mLoadDS = new Argix.AgentLineHaul.ShipScheduleDS();
            this.uddSortCenter = new Infragistics.Win.UltraWinGrid.UltraDropDown();
            this.uddCarrier = new Infragistics.Win.UltraWinGrid.UltraDropDown();
            ((System.ComponentModel.ISupportInitialize)(this.grdSchedule)).BeginInit();
            this.csLoad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mLoadDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddSortCenter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddCarrier)).BeginInit();
            this.SuspendLayout();
            // 
            // grdSchedule
            // 
            this.grdSchedule.AllowDrop = true;
            this.grdSchedule.ContextMenuStrip = this.csLoad;
            this.grdSchedule.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdSchedule.DataSource = this.mLoadDS.ShipScheduleTable;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.Name = "Verdana";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdSchedule.DisplayLayout.Appearance = appearance1;
            ultraGridBand1.ColHeaderLines = 3;
            ultraGridColumn1.Header.VisiblePosition = 36;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn2.Header.VisiblePosition = 37;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn3.Header.VisiblePosition = 39;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn4.Header.VisiblePosition = 40;
            ultraGridColumn4.Hidden = true;
            ultraGridColumn5.Header.VisiblePosition = 41;
            ultraGridColumn5.Hidden = true;
            ultraGridColumn6.Header.VisiblePosition = 42;
            ultraGridColumn6.Hidden = true;
            ultraGridColumn7.Header.VisiblePosition = 43;
            ultraGridColumn7.Hidden = true;
            ultraGridColumn8.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn8.Header.VisiblePosition = 7;
            ultraGridColumn8.Hidden = true;
            ultraGridColumn9.Header.VisiblePosition = 44;
            ultraGridColumn9.Hidden = true;
            ultraGridColumn10.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn10.Header.VisiblePosition = 9;
            ultraGridColumn10.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
            ultraGridColumn10.Width = 210;
            ultraGridColumn11.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn11.Header.Caption = "Next Carrier";
            ultraGridColumn11.Header.VisiblePosition = 10;
            ultraGridColumn11.Width = 140;
            ultraGridColumn12.Header.Caption = "Load\r\nNumber";
            ultraGridColumn12.Header.VisiblePosition = 24;
            ultraGridColumn12.Width = 95;
            ultraGridColumn13.Header.VisiblePosition = 45;
            ultraGridColumn13.Hidden = true;
            ultraGridColumn14.Header.Caption = "Trailer";
            ultraGridColumn14.Header.VisiblePosition = 8;
            ultraGridColumn14.MaxLength = 12;
            ultraGridColumn14.Nullable = Infragistics.Win.UltraWinGrid.Nullable.EmptyString;
            ultraGridColumn14.Width = 85;
            ultraGridColumn15.Header.Caption = "Tractor\r\nNumber";
            ultraGridColumn15.Header.VisiblePosition = 25;
            ultraGridColumn15.Nullable = Infragistics.Win.UltraWinGrid.Nullable.EmptyString;
            ultraGridColumn15.Width = 85;
            ultraGridColumn16.Header.Caption = "Driver";
            ultraGridColumn16.Header.VisiblePosition = 26;
            ultraGridColumn16.Nullable = Infragistics.Win.UltraWinGrid.Nullable.EmptyString;
            ultraGridColumn16.Width = 96;
            ultraGridColumn17.Format = "HH:mm/ddd";
            ultraGridColumn17.Header.Caption = "Close\r\nTime/Day";
            ultraGridColumn17.Header.VisiblePosition = 6;
            ultraGridColumn17.MaskInput = "{LOC}mm/dd/yyyy hh:mm";
            ultraGridColumn17.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
            ultraGridColumn17.UseEditorMaskSettings = true;
            ultraGridColumn17.Width = 110;
            ultraGridColumn18.Format = "HH:mm/ddd";
            ultraGridColumn18.Header.Caption = "Depart\r\nTime/Day";
            ultraGridColumn18.Header.VisiblePosition = 11;
            ultraGridColumn18.MaskInput = "{LOC}mm/dd/yyyy hh:mm";
            ultraGridColumn18.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
            ultraGridColumn18.UseEditorMaskSettings = true;
            ultraGridColumn18.Width = 110;
            ultraGridColumn19.Header.VisiblePosition = 46;
            ultraGridColumn19.Hidden = true;
            ultraGridColumn20.Header.VisiblePosition = 28;
            ultraGridColumn20.Hidden = true;
            ultraGridColumn21.ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
            ultraGridColumn21.Header.VisiblePosition = 30;
            ultraGridColumn21.Hidden = true;
            ultraGridColumn22.Header.VisiblePosition = 32;
            ultraGridColumn22.Hidden = true;
            ultraGridColumn23.Header.VisiblePosition = 34;
            ultraGridColumn23.Hidden = true;
            ultraGridColumn24.Header.VisiblePosition = 35;
            ultraGridColumn24.Hidden = true;
            ultraGridColumn25.Header.VisiblePosition = 47;
            ultraGridColumn25.Hidden = true;
            ultraGridColumn26.Header.VisiblePosition = 48;
            ultraGridColumn26.Hidden = true;
            ultraGridColumn27.Header.VisiblePosition = 49;
            ultraGridColumn27.Hidden = true;
            ultraGridColumn28.Header.VisiblePosition = 50;
            ultraGridColumn28.Hidden = true;
            ultraGridColumn29.Header.VisiblePosition = 51;
            ultraGridColumn29.Hidden = true;
            ultraGridColumn30.Header.VisiblePosition = 52;
            ultraGridColumn30.Hidden = true;
            ultraGridColumn31.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn31.Header.Caption = "Agent";
            ultraGridColumn31.Header.VisiblePosition = 13;
            ultraGridColumn31.Hidden = true;
            ultraGridColumn31.Width = 45;
            ultraGridColumn32.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn32.Header.Caption = "Zone";
            ultraGridColumn32.Header.VisiblePosition = 2;
            ultraGridColumn32.Hidden = true;
            ultraGridColumn32.Width = 50;
            ultraGridColumn33.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn33.Header.VisiblePosition = 3;
            ultraGridColumn33.Hidden = true;
            ultraGridColumn33.Width = 35;
            ultraGridColumn34.Header.VisiblePosition = 12;
            ultraGridColumn34.Width = 100;
            ultraGridColumn35.Format = "MM-dd";
            ultraGridColumn35.Header.Caption = "Arr \r\nDate";
            ultraGridColumn35.Header.VisiblePosition = 16;
            ultraGridColumn35.MaskInput = "{LOC}mm/dd/yy";
            ultraGridColumn35.Width = 75;
            ultraGridColumn36.Format = "MM-dd";
            ultraGridColumn36.Header.Caption = "OFD Date";
            ultraGridColumn36.Header.VisiblePosition = 18;
            ultraGridColumn36.MaskInput = "{LOC}mm/dd/yy";
            ultraGridColumn36.Width = 75;
            ultraGridColumn37.Header.VisiblePosition = 53;
            ultraGridColumn37.Hidden = true;
            ultraGridColumn38.Header.VisiblePosition = 54;
            ultraGridColumn38.Hidden = true;
            ultraGridColumn39.Header.VisiblePosition = 55;
            ultraGridColumn39.Hidden = true;
            ultraGridColumn40.Header.VisiblePosition = 56;
            ultraGridColumn40.Hidden = true;
            ultraGridColumn41.Header.VisiblePosition = 57;
            ultraGridColumn41.Hidden = true;
            ultraGridColumn42.Header.VisiblePosition = 38;
            ultraGridColumn42.Hidden = true;
            ultraGridColumn43.Header.Caption = "S2 \r\nAgent";
            ultraGridColumn43.Header.VisiblePosition = 19;
            ultraGridColumn43.Hidden = true;
            ultraGridColumn43.Width = 45;
            ultraGridColumn44.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn44.Header.Caption = "S2\r\nZone";
            ultraGridColumn44.Header.VisiblePosition = 4;
            ultraGridColumn44.Hidden = true;
            ultraGridColumn44.Width = 50;
            ultraGridColumn45.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn45.Header.Caption = "S2\r\nTag";
            ultraGridColumn45.Header.VisiblePosition = 5;
            ultraGridColumn45.Hidden = true;
            ultraGridColumn45.Width = 35;
            ultraGridColumn46.Header.Caption = "S2 Notes";
            ultraGridColumn46.Header.VisiblePosition = 14;
            ultraGridColumn46.Width = 100;
            ultraGridColumn47.Format = "MM-dd";
            ultraGridColumn47.Header.Caption = "S2 Arr\r\nDate";
            ultraGridColumn47.Header.VisiblePosition = 21;
            ultraGridColumn47.MaskInput = "{LOC}mm/dd/yy";
            ultraGridColumn47.Width = 75;
            ultraGridColumn48.Format = "MM-dd";
            ultraGridColumn48.Header.Caption = "S2\r\nOFD\r\nDate";
            ultraGridColumn48.Header.VisiblePosition = 23;
            ultraGridColumn48.MaskInput = "{LOC}mm/dd/yy";
            ultraGridColumn48.Width = 75;
            ultraGridColumn49.Header.VisiblePosition = 58;
            ultraGridColumn49.Hidden = true;
            ultraGridColumn50.Header.VisiblePosition = 59;
            ultraGridColumn50.Hidden = true;
            ultraGridColumn51.Header.VisiblePosition = 60;
            ultraGridColumn51.Hidden = true;
            ultraGridColumn52.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn52.Header.Caption = "Arr\r\nDay";
            ultraGridColumn52.Header.VisiblePosition = 15;
            ultraGridColumn52.Width = 35;
            ultraGridColumn53.DataType = typeof(System.DateTime);
            ultraGridColumn53.Format = "HH:mm";
            ultraGridColumn53.Header.Caption = "Arr\r\nTime";
            ultraGridColumn53.Header.VisiblePosition = 17;
            ultraGridColumn53.MaskInput = "{LOC}hh:mm";
            ultraGridColumn53.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
            ultraGridColumn53.UseEditorMaskSettings = true;
            ultraGridColumn53.Width = 45;
            ultraGridColumn54.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn54.Header.Caption = "S2 \r\nArr\r\nDay";
            ultraGridColumn54.Header.VisiblePosition = 20;
            ultraGridColumn54.Width = 35;
            ultraGridColumn55.DataType = typeof(System.DateTime);
            ultraGridColumn55.Format = "HH:mm";
            ultraGridColumn55.Header.Caption = "S2\r\nArr\r\nTime";
            ultraGridColumn55.Header.VisiblePosition = 22;
            ultraGridColumn55.MaskInput = "{LOC}hh:mm";
            ultraGridColumn55.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
            ultraGridColumn55.UseEditorMaskSettings = true;
            ultraGridColumn55.Width = 45;
            ultraGridColumn56.AllowGroupBy = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn56.Header.Caption = "Trailer\r\nComplete";
            ultraGridColumn56.Header.VisiblePosition = 29;
            ultraGridColumn56.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
            ultraGridColumn56.Width = 70;
            ultraGridColumn57.AllowGroupBy = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn57.Header.Caption = "Paper\r\nComplete";
            ultraGridColumn57.Header.VisiblePosition = 31;
            ultraGridColumn57.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
            ultraGridColumn57.Width = 70;
            ultraGridColumn58.Header.Caption = "Trailer\r\nDispatch";
            ultraGridColumn58.Header.VisiblePosition = 33;
            ultraGridColumn58.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
            ultraGridColumn58.Width = 70;
            ultraGridColumn59.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn59.Header.Caption = "Zone\r\nTag";
            ultraGridColumn59.Header.Fixed = true;
            ultraGridColumn59.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.Button;
            ultraGridColumn59.Header.VisiblePosition = 0;
            ultraGridColumn59.Width = 55;
            ultraGridColumn60.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn60.Header.Caption = "S2 \r\nZone\r\nTag";
            ultraGridColumn60.Header.Fixed = true;
            ultraGridColumn60.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.Button;
            ultraGridColumn60.Header.VisiblePosition = 1;
            ultraGridColumn60.Width = 55;
            ultraGridColumn61.Header.Caption = "Freight\r\nAssigned";
            ultraGridColumn61.Header.VisiblePosition = 27;
            ultraGridColumn61.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
            ultraGridColumn61.Width = 70;
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
            ultraGridColumn19,
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
            ultraGridColumn36,
            ultraGridColumn37,
            ultraGridColumn38,
            ultraGridColumn39,
            ultraGridColumn40,
            ultraGridColumn41,
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
            ultraGridColumn54,
            ultraGridColumn55,
            ultraGridColumn56,
            ultraGridColumn57,
            ultraGridColumn58,
            ultraGridColumn59,
            ultraGridColumn60,
            ultraGridColumn61});
            this.grdSchedule.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.FontData.Name = "Verdana";
            appearance2.FontData.SizeInPoints = 8F;
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdSchedule.DisplayLayout.CaptionAppearance = appearance2;
            this.grdSchedule.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdSchedule.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdSchedule.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
            this.grdSchedule.DisplayLayout.Override.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.Name = "Verdana";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.TextHAlignAsString = "Left";
            this.grdSchedule.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdSchedule.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdSchedule.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance4.BorderColor = System.Drawing.SystemColors.WindowText;
            this.grdSchedule.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdSchedule.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdSchedule.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdSchedule.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdSchedule.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdSchedule.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdSchedule.DisplayLayout.UseFixedHeaders = true;
            this.grdSchedule.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSchedule.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdSchedule.Location = new System.Drawing.Point(0, 0);
            this.grdSchedule.Name = "grdSchedule";
            this.grdSchedule.RowUpdateCancelAction = Infragistics.Win.UltraWinGrid.RowUpdateCancelAction.RetainDataAndActivation;
            this.grdSchedule.Size = new System.Drawing.Size(676, 254);
            this.grdSchedule.TabIndex = 1;
            this.grdSchedule.Text = "Schedule";
            this.grdSchedule.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnUpdate;
            this.grdSchedule.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdSchedule.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdSchedule.AfterRowFilterChanged += new Infragistics.Win.UltraWinGrid.AfterRowFilterChangedEventHandler(this.OnGridAfterRowFilterChanged);
            this.grdSchedule.AfterEnterEditMode += new System.EventHandler(this.OnGridAfterEnterEditMode);
            this.grdSchedule.CellListSelect += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.OnGridCellListSelect);
            this.grdSchedule.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.grdSchedule.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnGridInitializeRow);
            this.grdSchedule.DragOver += new System.Windows.Forms.DragEventHandler(this.OnDragOver);
            this.grdSchedule.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
            this.grdSchedule.AfterRowUpdate += new Infragistics.Win.UltraWinGrid.RowEventHandler(this.OnGridAfterRowUpdate);
            this.grdSchedule.BeforeRowUpdate += new Infragistics.Win.UltraWinGrid.CancelableRowEventHandler(this.OnGridBeforeRowUpdate);
            this.grdSchedule.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.OnGridBeforeRowFilterDropDownPopulate);
            this.grdSchedule.BeforeExitEditMode += new Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventHandler(this.OnGridBeforeExitEditMode);
            this.grdSchedule.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnGridInitializeLayout);
            this.grdSchedule.DragLeave += new System.EventHandler(this.OnDragLeave);
            this.grdSchedule.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnGridKeyUp);
            this.grdSchedule.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridSelectionChanged);
            // 
            // csLoad
            // 
            this.csLoad.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxRefresh,
            this.ctxSep1,
            this.ctxCut,
            this.ctxCopy,
            this.ctxPaste,
            this.ctxSep2,
            this.ctxCancelLoad});
            this.csLoad.Name = "csLoad";
            this.csLoad.Size = new System.Drawing.Size(153, 148);
            // 
            // ctxRefresh
            // 
            this.ctxRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.ctxRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxRefresh.Name = "ctxRefresh";
            this.ctxRefresh.Size = new System.Drawing.Size(152, 22);
            this.ctxRefresh.Text = "&Refresh";
            this.ctxRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxSep1
            // 
            this.ctxSep1.Name = "ctxSep1";
            this.ctxSep1.Size = new System.Drawing.Size(149, 6);
            // 
            // ctxCut
            // 
            this.ctxCut.Image = global::Argix.Properties.Resources.Cut;
            this.ctxCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxCut.Name = "ctxCut";
            this.ctxCut.Size = new System.Drawing.Size(152, 22);
            this.ctxCut.Text = "C&ut";
            this.ctxCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxCopy
            // 
            this.ctxCopy.Image = global::Argix.Properties.Resources.Copy;
            this.ctxCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxCopy.Name = "ctxCopy";
            this.ctxCopy.Size = new System.Drawing.Size(152, 22);
            this.ctxCopy.Text = "&Copy";
            this.ctxCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxPaste
            // 
            this.ctxPaste.Image = global::Argix.Properties.Resources.Paste;
            this.ctxPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxPaste.Name = "ctxPaste";
            this.ctxPaste.Size = new System.Drawing.Size(152, 22);
            this.ctxPaste.Text = "&Paste";
            this.ctxPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxSep2
            // 
            this.ctxSep2.Name = "ctxSep2";
            this.ctxSep2.Size = new System.Drawing.Size(149, 6);
            // 
            // ctxCancelLoad
            // 
            this.ctxCancelLoad.Image = global::Argix.Properties.Resources.Delete;
            this.ctxCancelLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxCancelLoad.Name = "ctxCancelLoad";
            this.ctxCancelLoad.Size = new System.Drawing.Size(152, 22);
            this.ctxCancelLoad.Text = "Cancel &Load";
            this.ctxCancelLoad.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mLoadDS
            // 
            this.mLoadDS.DataSetName = "ShipScheduleDS";
            this.mLoadDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mLoadDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // uddSortCenter
            // 
            this.uddSortCenter.Cursor = System.Windows.Forms.Cursors.Default;
            this.uddSortCenter.Location = new System.Drawing.Point(6, 198);
            this.uddSortCenter.Name = "uddSortCenter";
            this.uddSortCenter.Size = new System.Drawing.Size(104, 18);
            this.uddSortCenter.TabIndex = 4;
            this.uddSortCenter.Text = "ultraDropDown1";
            this.uddSortCenter.Visible = false;
            // 
            // uddCarrier
            // 
            this.uddCarrier.Location = new System.Drawing.Point(108, 198);
            this.uddCarrier.Name = "uddCarrier";
            this.uddCarrier.Size = new System.Drawing.Size(104, 18);
            this.uddCarrier.TabIndex = 5;
            this.uddCarrier.Text = "ultraDropDown1";
            this.uddCarrier.Visible = false;
            this.uddCarrier.RowSelected += new Infragistics.Win.UltraWinGrid.RowSelectedEventHandler(this.OnCarrierSelected);
            this.uddCarrier.BeforeDropDown += new System.ComponentModel.CancelEventHandler(this.OnCarrierBeforeDropDown);
            // 
            // winSchedule
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(676, 254);
            this.Controls.Add(this.uddCarrier);
            this.Controls.Add(this.uddSortCenter);
            this.Controls.Add(this.grdSchedule);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "winSchedule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ship Schedule";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.grdSchedule)).EndInit();
            this.csLoad.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mLoadDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddSortCenter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddCarrier)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
				//Get lists
                this.uddCarrier.DataSource = EnterpriseFactory.GetCarriers();
				this.uddCarrier.DisplayMember = "CarrierTable.Description";
                this.uddCarrier.ValueMember = "CarrierTable.ID";
				
				#region Grid Initialization
				this.grdSchedule.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdSchedule.DisplayLayout.Bands[0].Columns["MainZone"].SortIndicator = SortIndicator.Ascending;
				this.grdSchedule.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
				this.uddCarrier.DisplayLayout.Bands[0].Columns[0].Hidden = true;
				this.uddCarrier.DisplayLayout.Bands[0].Columns[1].Width = 210;
				this.uddCarrier.DisplayLayout.Bands[0].Columns[1].SortIndicator = SortIndicator.Ascending;
				#endregion
				this.grdSchedule.UpdateMode = (UpdateMode.OnCellChangeOrLostFocus & UpdateMode.OnUpdate);
				this.grdSchedule.DataSource = this.mSchedule.Trips;
				this.grdSchedule.Select();
				this.grdSchedule.ActiveRow = this.grdSchedule.Rows.GetRowAtVisibleIndex(0);
				this.grdSchedule.ActiveRow.Selected = true;
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		public ShipSchedule Schedule { get { return this.mSchedule; } }
		public bool CanSave { get { return (this.Schedule.Trips.ShipScheduleTable.Rows.Count > 0); } }
		public bool CanCut { get { return this.ctxCut.Enabled; } }
		public void Cut() { this.ctxCut.PerformClick(); }
		public bool CanCopy { get { return this.ctxCopy.Enabled; } }
		public void Copy() { this.ctxCopy.PerformClick(); }
		public bool CanPaste { get { return this.ctxPaste.Enabled; } }
		public void Paste() { this.ctxPaste.PerformClick(); }
		public bool CanAddLoad { get { return AppSecurity.UserCanAddSchedule; } }
		public void AddLoads() {
			//Make sure we have at least one template to add
			reportStatus(new StatusEventArgs("Adding loads to this schedule..."));
			this.mSchedule.AddLoads();
		}
		public bool CanCancelLoad { get { return (AppSecurity.UserCanAddSchedule && (this.grdSchedule.Selected.Rows.Count > 0)); } }
		public bool IsCancelledLoad { 
			get { 
				if(this.grdSchedule.Selected.Rows.Count > 0) 
					return (this.grdSchedule.Selected.Rows[0].Cells["Canceled"].Value.ToString().Trim().Length > 0); 
				else
					return false;
			} 
		}
		public void CancelLoad() {
			//Cancel the selected load
			try {
				if(this.grdSchedule.Selected.Rows.Count > 0) {
					if(this.grdSchedule.Selected.Rows[0].Cells["Canceled"].Value.ToString().Trim() == "") {
						//Cancel
						if (MessageBox.Show("Do you really want to cancel this schedule?", App.Product,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes) {
							this.Cursor = Cursors.WaitCursor;
							reportStatus(new StatusEventArgs("Cancelling the selected load..."));
							this.grdSchedule.Selected.Rows[0].Cells["Canceled"].Value  = System.DateTime.Now;
							this.grdSchedule.Selected.Rows[0].Cells["Notes"].Value = "Cancelled";
							this.grdSchedule.UpdateData();
						}
					}
					else {
						//Un-cancel
						if(MessageBox.Show("Do you really want to uncancel this schedule?", App.Product,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes) {
							this.Cursor = Cursors.WaitCursor;
							reportStatus(new StatusEventArgs("Uncancelling the selected load..."));
							this.grdSchedule.Selected.Rows[0].Cells["Canceled"].Value = System.DBNull.Value;
							this.grdSchedule.Selected.Rows[0].Cells["Notes"].Value = "";
							this.grdSchedule.UpdateData();
						}
					}
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		public bool CanPrint { get { return (this.Schedule.Trips.ShipScheduleTable.Rows.Count > 0); } }
		public void Print(bool showDialog) { 
			//Print this schedule
			this.Cursor = Cursors.WaitCursor;
			try { 
				reportStatus(new StatusEventArgs("Printing this schedule..."));
				string caption = this.Schedule.ScheduleDate.DayOfWeek.ToString().ToUpper() + "  " + DateTime.Now.ToShortTimeString() + Environment.NewLine + "SHIP SCHEDULE FOR " +  this.Schedule.ScheduleDate.ToString("dd-MMM-yyyy");
				UltraGridPrinter.Print(this.grdSchedule, caption, showDialog);
			} 
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { this.Cursor = Cursors.Default; }
		}
		public bool CanPreview { get { return (this.Schedule.Trips.ShipScheduleTable.Rows.Count > 0); } }
		public void PrintPreview() { 
			//Print preview this schedule
			try {
				reportStatus(new StatusEventArgs("Print previewing this schedule..."));
                string caption = this.Schedule.ScheduleDate.DayOfWeek.ToString().ToUpper() + "  " + DateTime.Now.ToShortTimeString() + Environment.NewLine + "SHIP SCHEDULE FOR " + this.Schedule.ScheduleDate.ToString("dd-MMM-yyyy");
				UltraGridPrinter.PrintPreview(this.grdSchedule, caption);
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			
		}
		public bool CanEmailCarriers { get { return (AppSecurity.UserCanAddSchedule && this.Schedule.Trips.ShipScheduleTable.Rows.Count > 0); } }
		public void EmailCarriers(bool showDialog) {
			try {
				if(showDialog)
                    new dlgSubscriptions(global::Argix.Properties.Settings.Default.CarrierReportPath, this.mSchedule).ShowDialog();
				else {
					if(MessageBox.Show(this, "Do you want to send this schedule to all Carriers?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
						this.Cursor = Cursors.WaitCursor;
						reportStatus(new StatusEventArgs("Emailing this schedule to all carriers..."));
                        new dlgSubscriptions(global::Argix.Properties.Settings.Default.CarrierReportPath, this.mSchedule).SendReport();
					}
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { this.Cursor = Cursors.Default; }
		}
		public bool CanEmailAgents { get { return (AppSecurity.UserCanAddSchedule && this.Schedule.Trips.ShipScheduleTable.Rows.Count > 0); } }
		public void EmailAgents(bool showDialog) {
			try {
				if(showDialog)
                    new dlgSubscriptions(global::Argix.Properties.Settings.Default.AgentReportPath, this.mSchedule).ShowDialog();
				else {
					if(MessageBox.Show(this, "Do you want to send this schedule to all Agents?", App.Product,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes) {
						this.Cursor = Cursors.WaitCursor;
						reportStatus(new StatusEventArgs("Emailing this schedule to all agents..."));
                        new dlgSubscriptions(global::Argix.Properties.Settings.Default.AgentReportPath, this.mSchedule).SendReport();
					}
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { this.Cursor = Cursors.Default; }
		}
		public override void Refresh() { 
			try {
				this.Cursor = Cursors.WaitCursor;
				reportStatus(new StatusEventArgs("Refreshing this schedule..."));
				this.mSchedule.Refresh();
				this.grdSchedule.ActiveRow = this.grdSchedule.Rows.GetRowAtVisibleIndex(0);
				base.Refresh(); 
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnScheduleChanged(object sender, EventArgs e) {
			//Event handler for change in ship schedule
			reportStatus(new StatusEventArgs("Updating schedule loads..."));
		}
		private void OnGridSelectionChanged(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for selection change
			setUserServices();
		}
		#region Grid Services: OnGridInitializeLayout(), OnGridInitializeRow()
		private void OnGridInitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
			//Jan.12, 2005*********
			/// Default sort was changed to inlcude ZoneTag column and MainZone column was removed.
			/// Feb. 01, 2005
			/// GroupBy in the SortedColumns was set to false. This will allow users to sort on any column or combination thereof using the 
			/// shift key.
			/// Updated: Apr.2006 - New drop-down field FreightAssinged added
			try {
				Infragistics.Win.UltraWinGrid.UltraGridBand band = e.Layout.Bands[0];
				band.Columns["TrailerComplete2"].ValueList = e.Layout.ValueLists["YesNoValueList"];
				band.Columns["PaperworkComplete2"].ValueList = e.Layout.ValueLists["YesNoValueList"];
				band.Columns["TrailerDispatched2"].ValueList = e.Layout.ValueLists["YesNoValueList"];
				band.Columns["FreightAssigned2"].ValueList = e.Layout.ValueLists["YesNoValueList"];
				band.Columns["Carrier"].ValueList = uddCarrier;

				//sort by Trailer Complete Desc, Paperwork complete desc Trailer Dispatched Desc. close day asc and zone asc
				band.SortedColumns.Add("TrailerComplete2",true,false);
				band.SortedColumns.Add("PaperworkComplete2",true,false);
				band.SortedColumns.Add("TrailerDispatched2",true,false);
				band.SortedColumns.Add("ScheduledClose",false,false);
				band.SortedColumns.Add("ZoneTag",false,false);
				band.Override.HeaderClickAction = HeaderClickAction.SortMulti;
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
		}
		private void OnGridInitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e) {
			//Rules for new field TractorNumber and FreighAssigned added
			try {
				//Combine Zone and Tag fields (show only last 2 digits of tag field)
				string tag = "";
				if(e.Row.Cells["Tag"].Text.Trim().Length > 2)
					tag = e.Row.Cells["Tag"].Text.Substring(1);
				else
					tag = e.Row.Cells["Tag"].Text;
				e.Row.Cells["ZoneTag"].Value = e.Row.Cells["MainZone"].Text + "  " + tag;
			
				//S2
				//Combine Zone and Tag fields
				string S2Tag; // show only last 2 digits of tag field
				if (e.Row.Cells["S2Tag"].Text.Trim().Length > 2)
					S2Tag = e.Row.Cells["S2Tag"].Text.Substring(1);
				else
					S2Tag = e.Row.Cells["S2Tag"].Text;
				e.Row.Cells["S2ZoneTag"].Value = e.Row.Cells["S2MainZone"].Text + "  " + S2Tag;

				//Scheduled Arrival Day and Time
				if (e.Row.Cells["ScheduledArrival"].Value.ToString() != "") {
					DateTime arrivalDate = Convert.ToDateTime(e.Row.Cells["ScheduledArrival"].Value.ToString());
					string weekDay =  arrivalDate.DayOfWeek.ToString().Substring(0,3);
					e.Row.Cells["ArrivalDay"].Value = weekDay;
					e.Row.Cells["ArrivalTime"].Value = arrivalDate.ToString("HH:mm");
				}

				//S2 Scheduled Arrival Day and Time
				if (e.Row.Cells["S2ScheduledArrival"].Value.ToString() != "") {
					DateTime s2ArrivalDate = Convert.ToDateTime(e.Row.Cells["S2ScheduledArrival"].Value.ToString());
					string s2WeekDay =  s2ArrivalDate.DayOfWeek.ToString().Substring(0,3);
					e.Row.Cells["S2ArrivalDay"].Value = s2WeekDay;
					e.Row.Cells["S2ArrivalTime"].Value = s2ArrivalDate.ToString("HH:mm");
				}
				
				//Freight Assigned
				if (e.Row.Cells["FreightAssigned"].Value.ToString() != "")
					e.Row.Cells["FreightAssigned2"].Value = "Yes";
				else
					e.Row.Cells["FreightAssigned2"].Value = "No";
                if (!global::Argix.Properties.ArgixSettings.Default.CanEditFreightAssigned) e.Row.Cells["FreightAssigned2"].Activation = Activation.NoEdit;

				//Trailer Complete
				if (e.Row.Cells["TrailerComplete"].Value.ToString() != "")
					e.Row.Cells["TrailerComplete2"].Value = "Yes";
				else
					e.Row.Cells["TrailerComplete2"].Value = "No";
                if (!global::Argix.Properties.ArgixSettings.Default.CanEditFreightAssigned && e.Row.Cells["FreightAssigned2"].Value.ToString() == "No") e.Row.Cells["TrailerComplete2"].Activation = Activation.NoEdit;
				
				//Paperwork Complete
				if (e.Row.Cells["PaperworkComplete"].Value.ToString() != "")
					e.Row.Cells["PaperworkComplete2"].Value = "Yes";
				else
					e.Row.Cells["PaperworkComplete2"].Value = "No";

				//Trailer Dispatched
				if (e.Row.Cells["TrailerDispatched"].Value.ToString() != "")
					e.Row.Cells["TrailerDispatched2"].Value = "Yes";
				else
					e.Row.Cells["TrailerDispatched2"].Value = "No";

				if (e.Row.Cells["Canceled"].Value.ToString().Trim() != "") {
					//Cancelled - if cancelled the whole row is uneditable
					e.Row.Appearance.ForeColor = System.Drawing.Color.DarkGray;
					e.Row.Activation = Activation.NoEdit;
				}
				else {
					//User must have appropriate privileges to edit the following fields
					//If Stop 2 Main Zone is NOT empty, then allow any stop 2 fields to be updated
					if (!AppSecurity.UserCanUpdateAll) {
						e.Row.Cells["TrailerNumber"].Activation = Activation.NoEdit;
						e.Row.Cells["Carrier"].Activation = Activation.NoEdit;
						e.Row.Cells["LoadNumber"].Activation = Activation.NoEdit;
						//e.Row.Cells["DriverName"].Activation = Activation.NoEdit;
						e.Row.Cells["ScheduledClose"].Activation = Activation.NoEdit;
						e.Row.Cells["ScheduledDeparture"].Activation = Activation.NoEdit;
						e.Row.Cells["Notes"].Activation = Activation.NoEdit;
						e.Row.Cells["ScheduledArrival"].Activation = Activation.NoEdit;
						e.Row.Cells["ScheduledOFD1"].Activation = Activation.NoEdit;
						e.Row.Cells["ArrivalTime"].Activation = Activation.NoEdit;
					}
					if (!AppSecurity.UserCanUpdateAll || e.Row.Cells["S2MainZone"].Text.Trim() == "" ) {
						e.Row.Cells["S2ScheduledArrival"].Activation = Activation.NoEdit;
						e.Row.Cells["S2ScheduledOFD1"].Activation = Activation.NoEdit;
						e.Row.Cells["S2Notes"].Activation = Activation.NoEdit;
						e.Row.Cells["S2ArrivalTime"].Activation = Activation.NoEdit;
					}
				}
				//get active row //Note: This will fire this event again and will get executed at least twice.
				e.Row.RefreshSortPosition();
			}
			catch(ArgumentException ex) { App.ReportError(ex, false, LogLevel.Debug); }
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		#endregion
		#region Grid Edit Services: OnCarrierBeforeDropDown(), OnCarrierSelected(), OnGridCellListSelect(), OnGridBeforeRowUpdate(), OnGridAfterRowUpdate(), OnGridBeforeRowFilterDropDownPopulate(), OnGridAfterRowFilterChanged(), OnGridBeforeExitEditMode()
		private void OnCarrierBeforeDropDown(object sender, System.ComponentModel.CancelEventArgs e) {
			//
			try {
				this.uddCarrier.DisplayLayout.Bands[0].Columns[1].Width = 210;
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnCarrierSelected(object sender, Infragistics.Win.UltraWinGrid.RowSelectedEventArgs e) {
			//
			try {
				if(e.Row != null)
					grdSchedule.ActiveRow.Cells["CarrierServiceID"].Value = e.Row.Cells["ID"].Text;
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnGridCellListSelect(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e) {
			//Updated: Apr.10, 2006
			//New drop-dwon field FreightAssigned added
			try {
				switch(e.Cell.Column.Key) {
					case "TrailerComplete2": 
						if(e.Cell.Text == "Yes") 
							this.grdSchedule.ActiveRow.Cells["TrailerComplete"].Value = System.DateTime.Now;
						else
							this.grdSchedule.ActiveRow.Cells["TrailerComplete"].Value = System.DBNull.Value;
						break;
					case "PaperworkComplete2": 
						if(e.Cell.Text == "Yes") 
							this.grdSchedule.ActiveRow.Cells["PaperworkComplete"].Value = System.DateTime.Now;
						else
							this.grdSchedule.ActiveRow.Cells["PaperworkComplete"].Value = System.DBNull.Value;
						break;
					case "TrailerDispatched2": 
						if(e.Cell.Text == "Yes") 
							this.grdSchedule.ActiveRow.Cells["TrailerDispatched"].Value = System.DateTime.Now;
						else
							this.grdSchedule.ActiveRow.Cells["TrailerDispatched"].Value = System.DBNull.Value;
						break;
					case "FreightAssigned2": 
						if(e.Cell.Text == "Yes") 
							this.grdSchedule.ActiveRow.Cells["FreightAssigned"].Value = System.DateTime.Now;
						else
							this.grdSchedule.ActiveRow.Cells["FreightAssigned"].Value = System.DBNull.Value;
						break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnGridBeforeRowUpdate(object sender, Infragistics.Win.UltraWinGrid.CancelableRowEventArgs e) {
			//
			try {
				if(!validateRules(e)) e.Cancel = true;
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnGridAfterRowUpdate(object sender, Infragistics.Win.UltraWinGrid.RowEventArgs e) {
			//Updated: August 29, 2005
			//Handles new DuplicateLoadNumberException exception.
			try {
				this.mSchedule.Update();
				e.Row.Appearance.ResetForeColor();
				if(e.Row.Cells["Canceled"].Value.ToString().Trim() != "") {
					e.Row.Appearance.ForeColor = System.Drawing.Color.DarkGray;
					e.Row.Activation = Activation.NoEdit;
				}
				else {
					e.Row.Activation = Activation.AllowEdit;
				}
				e.Row.Refresh(RefreshRow.FireInitializeRow);
			}
			catch(DuplicateLoadNumberException ex) {
				//Don't discard all changes; set Load# to empty string and update the row
				MessageBox.Show(ex.Message + " Row will be saved without Load Number.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Error);
				e.Row.Cells["LoadNumber"].Value = "";
				e.Row.Update();
				e.Row.Appearance.ForeColor = System.Drawing.Color.Red;
			}
			catch(Exception ex) { 
				this.mSchedule.Trips.RejectChanges();
				e.Row.CancelUpdate();
				App.ReportError(ex, true, LogLevel.Error); 
			}
		}
		private void OnGridBeforeRowFilterDropDownPopulate(object sender, Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs e) {
			//Removes only (Blanks) and Non Blanks default filter
			try { 
				e.ValueList.ValueListItems.Remove(3);
				e.ValueList.ValueListItems.Remove(2);
				e.ValueList.ValueListItems.Remove(1);
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnGridAfterRowFilterChanged(object sender, Infragistics.Win.UltraWinGrid.AfterRowFilterChangedEventArgs e) {	
			//
			UltraGridBand band = this.grdSchedule.DisplayLayout.Bands[0];
			try {
				//Check to see if filter condition is clear - it will be clear if user selects All from 
				//the filter list; otherwise, make sure filter is cleared from the CarrierID column as well
				if(band.ColumnFilters["Carrier"].FilterConditions.Count > 0) {			
					//If there is a filter condition, then modify it by filtering it by CarrierID instead of 
					//Carrier which filters by unique carrier service name
					int carrierID = Convert.ToInt32( this.grdSchedule.Rows.GetRowAtVisibleIndex(0).Cells["CarrierID"].Value);
					band.ColumnFilters.ClearAllFilters();
					band.ColumnFilters["CarrierID"].FilterConditions.Add(FilterComparisionOperator.Equals,carrierID);
				}
				else
					band.ColumnFilters.ClearAllFilters();
			}
			catch(Exception ex) { band.ColumnFilters.ClearAllFilters(); App.ReportError(ex, true, LogLevel.Error); }
		}
		private void OnGridBeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e) {
			//This event gets fired just before user is leaving a cell after entering into Edit mode.
			//Two unbound fields Arrival Time and S2 Arrival Time will not be persisted unless we update the bound fields where
			//they get their values in the first place: ScheduledArrival and S2ScheduledArrival.
			//If Time format is incorrect, then UltraGrid automatically throws generic error message;
			try {
				switch (grdSchedule.ActiveCell.Column.Key) {
					case "ArrivalTime":		this.grdSchedule.ActiveRow.Cells["ScheduledArrival"].Value = Convert.ToDateTime(Convert.ToDateTime(grdSchedule.ActiveRow.Cells["ScheduledArrival"].Text).ToShortDateString() + " " + Convert.ToDateTime(grdSchedule.ActiveRow.Cells["ArrivalTime"].Text).ToShortTimeString()); break;
					case "S2ArrivalTime":	this.grdSchedule.ActiveRow.Cells["S2ScheduledArrival"].Value = Convert.ToDateTime(Convert.ToDateTime(grdSchedule.ActiveRow.Cells["S2ScheduledArrival"].Text).ToShortDateString() + " " + Convert.ToDateTime(grdSchedule.ActiveRow.Cells["S2ArrivalTime"].Text).ToShortTimeString()); break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnGridAfterEnterEditMode(object sender, System.EventArgs e) {
			//Event handler for 
			setUserServices();
		}
		#endregion
		#region Grid Support: OnGridMouseDown(), OnGridKeyUp()
		private void OnGridMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Event handler for mouse down event
			try {
				//Set menu and toolbar services
				UltraGrid grid = (UltraGrid)sender;
				grid.Focus();
				UIElement oUIElement = grid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));
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
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		private void OnGridKeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			//Event handler for key up event
			if(e.KeyCode == System.Windows.Forms.Keys.Enter) { 
				//Update row on Enter
				this.grdSchedule.ActiveRow.Update(); 
				e.Handled = true; 
			}
		}
		#endregion
		#region Grid Drag/Drop Services: OnDragEnter(), OnDragOver(), OnDragDrop(), OnDragLeave()
		private void OnDragEnter(object sender, System.Windows.Forms.DragEventArgs e) { }
		private void OnDragOver(object sender, System.Windows.Forms.DragEventArgs e) {
			//Event handler for dragging over the grid
			try {
				//Enable appropriate drag drop effect
				//NOTE: Cannot COPY or MOVE to self
				UltraGrid oGrid = (UltraGrid)sender;
				DataObject oData = (DataObject)e.Data;
				if(CanAddLoad && !oGrid.Focused && oData.GetDataPresent(DataFormats.StringFormat, true)) {
					switch(e.KeyState) {
						case KEYSTATE_SHIFT:	e.Effect = (!oGrid.Focused) ? DragDropEffects.Move : DragDropEffects.None; break;
						case KEYSTATE_CTL:		e.Effect = (!oGrid.Focused) ? DragDropEffects.None : DragDropEffects.None; break;
						default:				e.Effect = (!oGrid.Focused) ? DragDropEffects.Move : DragDropEffects.None; break;
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
				DataObject oData = (DataObject)e.Data;
				if(oData.GetDataPresent(DataFormats.StringFormat, true)) 
					AddLoads();
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnDragLeave(object sender, System.EventArgs e) { }
		#endregion
        #region User Services: OnItemClick()
        private void OnItemClick(object sender, System.EventArgs e) {
			//Event handler for mneu item clicked
			try {
                ToolStripItem item = (ToolStripItem)sender;
                switch (item.Name) {
					case "ctxRefresh":		Refresh(); break;
					case "ctxCut":		
						Clipboard.SetDataObject(this.grdSchedule.ActiveCell.SelText, false);
						this.grdSchedule.ActiveCell.Value = this.grdSchedule.ActiveCell.Text.Remove(this.grdSchedule.ActiveCell.SelStart, this.grdSchedule.ActiveCell.SelLength);
						break;
					case "ctxCopy":		
						Clipboard.SetDataObject(this.grdSchedule.ActiveCell.SelText, false);
						break;
					case "ctxPaste":		
						IDataObject o = Clipboard.GetDataObject();
						this.grdSchedule.ActiveCell.Value = this.grdSchedule.ActiveCell.Text.Remove(this.grdSchedule.ActiveCell.SelStart, this.grdSchedule.ActiveCell.SelLength).Insert(this.grdSchedule.ActiveCell.SelStart, (string)o.GetData("Text"));
						break;
					case "ctxCancelLoad":	CancelLoad(); break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		#endregion
		#region Local Services: validateRules(), setUserServices(), reportStatus()
		private bool validateRules(Infragistics.Win.UltraWinGrid.CancelableRowEventArgs e) {
			//Updated: Apr. 10, 2006 - New rule added
			//Trailer can't be completed if Freight is not assigned.
			bool validated = false;
			DateTime schCloseDate;
			DateTime schDepartDate;
			DateTime schArrivalDate;
			DateTime schOFD1Date;
			DateTime s2SchArrivalDate;
			DateTime s2SchOFD1Date;
            DateTime thisDate = this.mSchedule.ScheduleDate;    // DateTime.Today;
            int valDays = global::Argix.Properties.Settings.Default.ValidationWindow;
			string schCloseDateCellText = e.Row.Cells["ScheduledClose"].Value.ToString().Trim();
			string schDepartDateCellText = e.Row.Cells["ScheduledDeparture"].Value.ToString().Trim();
			string schArrivalDateCellText = e.Row.Cells["ScheduledArrival"].Value.ToString().Trim();
			string schOFD1DateCellText = e.Row.Cells["ScheduledOFD1"].Value.ToString().Trim();
			string arrivalTimeCellText = e.Row.Cells["ArrivalTime"].Value.ToString().Trim();
			string S2MainZoneCellText = e.Row.Cells["S2MainZone"].Value.ToString().Trim();
			string S2ScheduledArrivalCellText = e.Row.Cells["S2ScheduledArrival"].Value.ToString().Trim();
			string S2ScheduledOFD1CellText = e.Row.Cells["S2ScheduledOFD1"].Value.ToString().Trim();
			string trailerComplete = e.Row.Cells["TrailerComplete2"].Value.ToString();
			string freightAssigned = e.Row.Cells["FreightAssigned2"].Value.ToString();
			bool cancel = (e.Row.Cells["Canceled"].Value != System.DBNull.Value);

			//Biz Rules - All dates can be less or greater than 30 days
			//1 - Schedule Close Date <= Scheduled Departure Date
			StringBuilder message = new StringBuilder();
			if(schCloseDateCellText != "") {
				schCloseDate = Convert.ToDateTime(schCloseDateCellText);
				if(schCloseDate.Subtract(thisDate).Days < - valDays || schCloseDate.Subtract(thisDate).Days > valDays)
					message.Append("Scheduled Close date can't be less or greater than " + valDays.ToString() + " days." + Environment.NewLine);
				if( schDepartDateCellText != "") {
					schDepartDate = Convert.ToDateTime(schDepartDateCellText);
					if(schDepartDate.Subtract(thisDate).Days < - valDays || schDepartDate.Subtract(thisDate).Days > valDays)
                        message.Append("Scheduled Departure date can't be less or greater than " + valDays.ToString() + " days." + Environment.NewLine);
					if(schCloseDate > schDepartDate)
						message.Append("Schedule Close Date and Time must be less than or equal to Scheduled Departure Date and Time." + Environment.NewLine);
				}
				else
					message.Append("Scheduled Departure Date cell can't be empty.");
			}
			else
				message.Append("Scheduled Close Date cell can't be empty.");
			
			//2- Scheduled arrival Date <= OFD1 Date
			if(schArrivalDateCellText != "") {
				schArrivalDate = Convert.ToDateTime(schArrivalDateCellText).Date;
				if(schArrivalDate.Subtract(thisDate).Days < - valDays || schArrivalDate.Subtract(thisDate).Days > valDays)
                    message.Append("Scheduled Arrival date can't be less or greater than " + valDays.ToString() + " days." + Environment.NewLine);
				if(schOFD1DateCellText != "") {	
					schOFD1Date = Convert.ToDateTime(schOFD1DateCellText).Date;
					if(schOFD1Date.Subtract(thisDate).Days < - valDays || schOFD1Date.Subtract(thisDate).Days > valDays)
                        message.Append("Scheduled OFD1 date can't be less or greater than " + valDays.ToString() + " days." + Environment.NewLine);

					//Compare just the Date Component 'cause OFD does not have time
					if(schArrivalDate > schOFD1Date)
						message.Append("Scheduled Arrival Date must be less than or equal to Scheduled OFD1 Date." + Environment.NewLine);
				}
				else
					message.Append("Schedule OFD1 Date cell can't be empty." + Environment.NewLine);
			}
			else
				message.Append("Schedule Arrival Date cell can't be empty." + Environment.NewLine);

			//3- Scheduled Departure Date <= Arrival Date
			if(schArrivalDateCellText != "" && schDepartDateCellText != "") {
				//compare date  and time
				schArrivalDate = Convert.ToDateTime(Convert.ToDateTime(schArrivalDateCellText).ToShortDateString() + " " + Convert.ToDateTime(arrivalTimeCellText).ToShortTimeString());
				schDepartDate = Convert.ToDateTime(schDepartDateCellText);
				if(schDepartDate > schArrivalDate)
					message.Append("Scheduled Departure Date and Time must be less than or equal to Scheduled Arrival Date and Time." + Environment.NewLine);
			}
			
			//4- If one field is filled for Stop 2, then all should be filled-in
			if(S2MainZoneCellText != "" || S2ScheduledOFD1CellText != "" || S2ScheduledArrivalCellText != "") {
				if(S2MainZoneCellText == "") message.Append("S2 Main Zone can't be empty."  + Environment.NewLine);
				if(S2ScheduledArrivalCellText != "") {
					s2SchArrivalDate = Convert.ToDateTime(S2ScheduledArrivalCellText).Date;
					if(s2SchArrivalDate.Subtract(thisDate).Days < - valDays || s2SchArrivalDate.Subtract(thisDate).Days > valDays)
                        message.Append("S2 Scheduled Arrival date can't be less or greater than " + valDays.ToString() + " days." + Environment.NewLine);
					if(S2ScheduledOFD1CellText != "") {
						s2SchOFD1Date = Convert.ToDateTime(S2ScheduledOFD1CellText).Date;
						if (s2SchOFD1Date.Subtract(thisDate).Days < - valDays || s2SchOFD1Date.Subtract(thisDate).Days > valDays)
                            message.Append("S2 Scheduled OFD1 date can't be less or greater than " + valDays.ToString() + " days." + Environment.NewLine);
						if(s2SchArrivalDate > s2SchOFD1Date)
							message.Append("S2 Scheduled Arrival Date must be less than or equal to S2 Scheduled OFD1 Date." + Environment.NewLine);
					}
					else
						message.Append("S2 Scheduled OFD1 Date cell can't be empty." + Environment.NewLine);
				}
				else
					message.Append("S2 Scheduled Arrival Date cell can't be empty." + Environment.NewLine);
	
			}
			//5 - TrailerComplete can't be set to yes if FreightAssinged is set to no.
			if(trailerComplete == "Yes" && freightAssigned == "No")
				message.Append("Freight should be assigned first before trailer is marked complete.");
			//************************
			
			//Invalidate and display validation message unless the load is being cancelled
			if(!cancel && message.ToString() != "")
				MessageBox.Show(message.ToString(),App.Product,MessageBoxButtons.OK,MessageBoxIcon.Error);
			else
				validated = true;
			return validated;
		}
		private void setUserServices() {
			//Set user services
			try {
				this.ctxRefresh.Enabled = true;
				this.ctxCut.Enabled = (this.grdSchedule.ActiveCell != null && this.grdSchedule.ActiveCell.IsInEditMode);
				this.ctxCopy.Enabled = (this.grdSchedule.ActiveCell != null);
				this.ctxPaste.Enabled = (this.grdSchedule.ActiveCell != null && this.grdSchedule.ActiveCell.IsInEditMode && Clipboard.GetDataObject() != null);
				this.ctxCancelLoad.Enabled = this.CanCancelLoad;
                this.ctxCancelLoad.Checked = this.IsCancelledLoad;
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Error); }
			finally { Application.DoEvents(); if(this.ServiceStatesChanged!=null) this.ServiceStatesChanged(this, new EventArgs()); }
		}
		private void reportStatus(StatusEventArgs e) { if(this.StatusMessage != null) this.StatusMessage(this, e); }
		#endregion
	}
}
