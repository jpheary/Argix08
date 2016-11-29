using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.AgentLineHaul;
using Argix.Data;
using Argix.Windows;

namespace Argix.Freight {
	//
	public class dlgTrip : System.Windows.Forms.Form {
		//Members
		private ShipScheduleTrip mDestinationTrip=null;
		private string mMainZoneCode="";
		private long mAgentTerminalID=0;
		private DateTime mInitialScheduleDate=DateTime.Today;
		private UltraGridSvc mGridSvcScheduleUpdates=null;
		private bool mCalendarOpen=false;
		
		#region Controls

		private System.ComponentModel.IContainer components;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdShipSchedule;
        private Argix.AgentLineHaul.ShipScheduleDS mShipScheduleDS;
		private System.Windows.Forms.DateTimePicker dtpScheduleDate;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;

		#endregion
				
		//Interface
		public dlgTrip(string mainZoneCode, long agentTerminalID, DateTime initialScheduleDate) {
			//Constructor
			try {
				//Required for designer support
				InitializeComponent();
				this.Text = "Destination Trips";
				this.mGridSvcScheduleUpdates = new UltraGridSvc(this.grdShipSchedule);
				this.mMainZoneCode = mainZoneCode;
				this.mAgentTerminalID = agentTerminalID;
				this.mInitialScheduleDate = initialScheduleDate;
			}
			catch(Exception ex) { throw new ApplicationException("Could not create new destination trip dialog.", ex); }
		}
		protected override void Dispose( bool disposing ) { if(disposing) { if(components!= null) components.Dispose(); } base.Dispose(disposing); }
		public ShipScheduleTrip DestinationTrip { get { return this.mDestinationTrip; } }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		/// 
		private void InitializeComponent()
		{
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ShipScheduleMasterTable",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduleID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenterID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenter");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduleDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TripID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TemplateID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("BolNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CarrierServiceID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Carrier");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LoadNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TractorNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledClose");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledDeparture");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsMandatory");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FreightAssigned");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerComplete");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PaperworkComplete");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerDispatched");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Canceled");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SCDEUserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SCDELastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SCDERowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MainZone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Tag");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Notes");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledArrival");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledOFD1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S1UserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S1LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S1RowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2StopID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2StopNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn41 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2MainZone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2Tag");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn43 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2Notes");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn44 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ScheduledArrival");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn45 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ScheduledOFD1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn46 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2UserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn47 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn48 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2RowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn49 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NextCarrier");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn50 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CarrierID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn51 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn52 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn53 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Assignment");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Assignment",0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn54 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TripID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn55 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn56 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn57 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn58 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn59 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MainZone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn60 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Tag");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn61 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Notes");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn62 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledArrival");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn63 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledOFD1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn64 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopUserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn65 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopLastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn66 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopRowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn67 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FreightID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn68 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn69 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn70 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn71 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AssignedUser");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn72 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AssignedDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn73 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn74 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn75 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn76 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgTrip));
            this.grdShipSchedule = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mShipScheduleDS = new Argix.AgentLineHaul.ShipScheduleDS();
            this.dtpScheduleDate = new System.Windows.Forms.DateTimePicker();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdShipSchedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mShipScheduleDS)).BeginInit();
            this.SuspendLayout();
            // 
            // grdShipSchedule
            // 
            this.grdShipSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdShipSchedule.DataMember = "ShipScheduleMasterTable";
            this.grdShipSchedule.DataSource = this.mShipScheduleDS;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.Name = "Verdana";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdShipSchedule.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Hidden = true;
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.Hidden = true;
            ultraGridColumn6.Header.VisiblePosition = 18;
            ultraGridColumn6.Hidden = true;
            ultraGridColumn7.Header.VisiblePosition = 19;
            ultraGridColumn7.Hidden = true;
            ultraGridColumn8.Header.VisiblePosition = 20;
            ultraGridColumn8.Hidden = true;
            ultraGridColumn9.Header.VisiblePosition = 11;
            ultraGridColumn9.Width = 144;
            ultraGridColumn10.Header.Caption = "Load#";
            ultraGridColumn10.Header.VisiblePosition = 15;
            ultraGridColumn10.Width = 72;
            ultraGridColumn11.Header.VisiblePosition = 21;
            ultraGridColumn11.Hidden = true;
            ultraGridColumn12.Header.Caption = "Trailer#";
            ultraGridColumn12.Header.VisiblePosition = 10;
            ultraGridColumn12.Width = 72;
            ultraGridColumn13.Header.VisiblePosition = 22;
            ultraGridColumn13.Hidden = true;
            ultraGridColumn14.Format = "MM/dd/yyyy HH:mmtt";
            ultraGridColumn14.Header.Caption = "Sched Close";
            ultraGridColumn14.Header.VisiblePosition = 9;
            ultraGridColumn14.Width = 120;
            ultraGridColumn15.Format = "MM/dd/yyyy HH:mmtt";
            ultraGridColumn15.Header.Caption = "Sched Departure";
            ultraGridColumn15.Header.VisiblePosition = 12;
            ultraGridColumn15.Width = 120;
            ultraGridColumn16.Header.VisiblePosition = 23;
            ultraGridColumn16.Hidden = true;
            ultraGridColumn17.Format = "MM/dd/yyyy HH:mmtt";
            ultraGridColumn17.Header.Caption = "All Assigned";
            ultraGridColumn17.Header.VisiblePosition = 17;
            ultraGridColumn17.Hidden = true;
            ultraGridColumn17.NullText = "N";
            ultraGridColumn17.Width = 48;
            ultraGridColumn18.Format = "MM/dd/yyyy HH:mmtt";
            ultraGridColumn18.Header.Caption = "Complete";
            ultraGridColumn18.Header.VisiblePosition = 16;
            ultraGridColumn18.Hidden = true;
            ultraGridColumn18.NullText = "N";
            ultraGridColumn18.Width = 48;
            ultraGridColumn19.Header.VisiblePosition = 24;
            ultraGridColumn19.Hidden = true;
            ultraGridColumn20.Header.VisiblePosition = 25;
            ultraGridColumn20.Hidden = true;
            ultraGridColumn21.Header.VisiblePosition = 26;
            ultraGridColumn21.Hidden = true;
            ultraGridColumn22.Header.VisiblePosition = 27;
            ultraGridColumn22.Hidden = true;
            ultraGridColumn23.Header.VisiblePosition = 28;
            ultraGridColumn23.Hidden = true;
            ultraGridColumn24.Header.VisiblePosition = 29;
            ultraGridColumn24.Hidden = true;
            ultraGridColumn25.Header.VisiblePosition = 30;
            ultraGridColumn25.Hidden = true;
            ultraGridColumn26.Header.VisiblePosition = 31;
            ultraGridColumn26.Hidden = true;
            ultraGridColumn27.Header.VisiblePosition = 32;
            ultraGridColumn27.Hidden = true;
            ultraGridColumn28.Header.VisiblePosition = 33;
            ultraGridColumn28.Hidden = true;
            ultraGridColumn29.Header.Caption = "Zone";
            ultraGridColumn29.Header.VisiblePosition = 5;
            ultraGridColumn29.Width = 48;
            ultraGridColumn30.Header.VisiblePosition = 7;
            ultraGridColumn30.Width = 48;
            ultraGridColumn31.Header.VisiblePosition = 13;
            ultraGridColumn31.Width = 96;
            ultraGridColumn32.Header.VisiblePosition = 34;
            ultraGridColumn32.Hidden = true;
            ultraGridColumn33.Header.VisiblePosition = 35;
            ultraGridColumn33.Hidden = true;
            ultraGridColumn34.Header.VisiblePosition = 36;
            ultraGridColumn34.Hidden = true;
            ultraGridColumn35.Header.VisiblePosition = 37;
            ultraGridColumn35.Hidden = true;
            ultraGridColumn36.Header.VisiblePosition = 38;
            ultraGridColumn36.Hidden = true;
            ultraGridColumn37.Header.VisiblePosition = 39;
            ultraGridColumn37.Hidden = true;
            ultraGridColumn38.Header.VisiblePosition = 40;
            ultraGridColumn38.Hidden = true;
            ultraGridColumn39.Header.VisiblePosition = 41;
            ultraGridColumn39.Hidden = true;
            ultraGridColumn40.Header.VisiblePosition = 42;
            ultraGridColumn40.Hidden = true;
            ultraGridColumn41.Header.Caption = "S2 Main Zone";
            ultraGridColumn41.Header.VisiblePosition = 43;
            ultraGridColumn41.Hidden = true;
            ultraGridColumn41.Width = 96;
            ultraGridColumn42.Header.Caption = "S2 Tag";
            ultraGridColumn42.Header.VisiblePosition = 8;
            ultraGridColumn42.Width = 48;
            ultraGridColumn43.Header.Caption = "S2 Notes";
            ultraGridColumn43.Header.VisiblePosition = 14;
            ultraGridColumn43.Width = 96;
            ultraGridColumn44.Header.VisiblePosition = 44;
            ultraGridColumn44.Hidden = true;
            ultraGridColumn45.Header.VisiblePosition = 45;
            ultraGridColumn45.Hidden = true;
            ultraGridColumn46.Header.VisiblePosition = 46;
            ultraGridColumn46.Hidden = true;
            ultraGridColumn47.Header.VisiblePosition = 47;
            ultraGridColumn47.Hidden = true;
            ultraGridColumn48.Header.VisiblePosition = 48;
            ultraGridColumn48.Hidden = true;
            ultraGridColumn49.Header.VisiblePosition = 49;
            ultraGridColumn49.Hidden = true;
            ultraGridColumn50.Header.VisiblePosition = 50;
            ultraGridColumn50.Hidden = true;
            ultraGridColumn51.Header.Caption = "Client#";
            ultraGridColumn51.Header.VisiblePosition = 6;
            ultraGridColumn51.Width = 54;
            ultraGridColumn52.Header.VisiblePosition = 51;
            ultraGridColumn52.Hidden = true;
            ultraGridColumn53.Header.VisiblePosition = 52;
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
            ultraGridColumn53});
            ultraGridBand1.SummaryFooterCaption = "Grand Summaries";
            ultraGridColumn54.Header.VisiblePosition = 0;
            ultraGridColumn55.Header.VisiblePosition = 1;
            ultraGridColumn55.Width = 96;
            ultraGridColumn56.Header.Caption = "Stop#";
            ultraGridColumn56.Header.VisiblePosition = 2;
            ultraGridColumn56.Width = 96;
            ultraGridColumn57.Header.VisiblePosition = 3;
            ultraGridColumn57.Hidden = true;
            ultraGridColumn58.Header.Caption = "Agent#";
            ultraGridColumn58.Header.VisiblePosition = 4;
            ultraGridColumn58.Width = 96;
            ultraGridColumn59.Header.Caption = "Main Zone";
            ultraGridColumn59.Header.VisiblePosition = 5;
            ultraGridColumn59.Width = 115;
            ultraGridColumn60.Header.VisiblePosition = 7;
            ultraGridColumn60.Width = 96;
            ultraGridColumn61.Header.VisiblePosition = 8;
            ultraGridColumn61.Width = 107;
            ultraGridColumn62.Format = "MM/dd/yyyy HH:mmtt";
            ultraGridColumn62.Header.Caption = "Sched Arrival";
            ultraGridColumn62.Header.VisiblePosition = 9;
            ultraGridColumn62.Width = 96;
            ultraGridColumn63.Header.Caption = "Sched OFD1";
            ultraGridColumn63.Header.VisiblePosition = 10;
            ultraGridColumn63.Width = 112;
            ultraGridColumn64.Header.VisiblePosition = 11;
            ultraGridColumn64.Hidden = true;
            ultraGridColumn65.Header.VisiblePosition = 12;
            ultraGridColumn65.Hidden = true;
            ultraGridColumn66.Header.VisiblePosition = 13;
            ultraGridColumn66.Hidden = true;
            ultraGridColumn67.Header.VisiblePosition = 14;
            ultraGridColumn67.Width = 112;
            ultraGridColumn68.Header.VisiblePosition = 15;
            ultraGridColumn69.Header.VisiblePosition = 17;
            ultraGridColumn70.Header.VisiblePosition = 19;
            ultraGridColumn71.Header.Caption = "Assigned User";
            ultraGridColumn71.Header.VisiblePosition = 16;
            ultraGridColumn71.Width = 96;
            ultraGridColumn72.Format = "MM/dd/yyyy HH:mmtt";
            ultraGridColumn72.Header.Caption = "Assigned Date";
            ultraGridColumn72.Header.VisiblePosition = 18;
            ultraGridColumn72.Width = 96;
            ultraGridColumn73.Header.VisiblePosition = 20;
            ultraGridColumn74.Header.VisiblePosition = 21;
            ultraGridColumn75.Header.Caption = "Client#";
            ultraGridColumn75.Header.VisiblePosition = 6;
            ultraGridColumn76.Header.VisiblePosition = 22;
            ultraGridColumn76.Hidden = true;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn54,
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
            ultraGridColumn65,
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
            ultraGridColumn76});
            ultraGridBand2.SummaryFooterCaption = "Summaries for [BANDHEADER]: [SCROLLTIPFIELD]";
            this.grdShipSchedule.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdShipSchedule.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.FontData.Name = "Verdana";
            appearance2.FontData.SizeInPoints = 8F;
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdShipSchedule.DisplayLayout.CaptionAppearance = appearance2;
            this.grdShipSchedule.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdShipSchedule.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdShipSchedule.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdShipSchedule.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.Name = "Verdana";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.TextHAlignAsString = "Left";
            this.grdShipSchedule.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdShipSchedule.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdShipSchedule.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdShipSchedule.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdShipSchedule.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdShipSchedule.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdShipSchedule.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdShipSchedule.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdShipSchedule.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdShipSchedule.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdShipSchedule.Font = new System.Drawing.Font("Verdana",8.25F);
            this.grdShipSchedule.Location = new System.Drawing.Point(0,0);
            this.grdShipSchedule.Name = "grdShipSchedule";
            this.grdShipSchedule.Size = new System.Drawing.Size(570,224);
            this.grdShipSchedule.TabIndex = 1;
            this.grdShipSchedule.Text = "Ship Schedule";
            this.grdShipSchedule.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdShipSchedule.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnGridInitializeRow);
            this.grdShipSchedule.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnGridInitializeLayout);
            this.grdShipSchedule.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnTripSelectionChanged);
            // 
            // mShipScheduleDS
            // 
            this.mShipScheduleDS.DataSetName = "ShipScheduleDS";
            this.mShipScheduleDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mShipScheduleDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dtpScheduleDate
            // 
            this.dtpScheduleDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpScheduleDate.CustomFormat = "MMMM dd, yyyy";
            this.dtpScheduleDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpScheduleDate.Location = new System.Drawing.Point(392,1);
            this.dtpScheduleDate.MaxDate = new System.DateTime(2030,12,31,0,0,0,0);
            this.dtpScheduleDate.MinDate = new System.DateTime(2005,1,1,0,0,0,0);
            this.dtpScheduleDate.Name = "dtpScheduleDate";
            this.dtpScheduleDate.Size = new System.Drawing.Size(177,21);
            this.dtpScheduleDate.TabIndex = 2;
            this.dtpScheduleDate.ValueChanged += new System.EventHandler(this.OnScheduleDateChanged);
            this.dtpScheduleDate.DropDown += new System.EventHandler(this.OnCalendarOpened);
            this.dtpScheduleDate.CloseUp += new System.EventHandler(this.OnCalendarClosed);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOK.Location = new System.Drawing.Point(368,233);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(96,24);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(470,233);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96,24);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // dlgTrip
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.ClientSize = new System.Drawing.Size(570,262);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dtpScheduleDate);
            this.Controls.Add(this.grdShipSchedule);
            this.Font = new System.Drawing.Font("Verdana",8.25F);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgTrip";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Destination Trip";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.grdShipSchedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mShipScheduleDS)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Load conditions			
			this.Cursor = Cursors.WaitCursor;
			try {
				//Initialize controls
				this.Visible = true;
				Application.DoEvents();				
				this.grdShipSchedule.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdShipSchedule.DisplayLayout.Bands[0].Columns["MainZone"].SortIndicator = SortIndicator.Ascending;
				this.dtpScheduleDate.Value = this.mInitialScheduleDate;
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { OnValidateForm(null, null); this.Cursor = Cursors.Default; }
		}
		#region Grid Initialization: OnGridInitializeLayout(), OnGridInitializeRow()
		private void OnGridInitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
			//Event handler for grid layout initialization
			try {
				e.Layout.Bands[0].Columns.Insert(e.Layout.Bands[0].Columns.Count, "Complete");
				e.Layout.Bands[0].Columns["Complete"].DataType = typeof(string);
				e.Layout.Bands[0].Columns["Complete"].Width = 48;
				e.Layout.Bands[0].Columns["Complete"].Header.Appearance.TextHAlign = HAlign.Center;
				e.Layout.Bands[0].Columns["Complete"].CellAppearance.TextHAlign = HAlign.Center;
				e.Layout.Bands[0].Columns["Complete"].SortIndicator = SortIndicator.None;
				e.Layout.Bands[0].Columns.Insert(e.Layout.Bands[0].Columns.Count,"All Assigned");
				e.Layout.Bands[0].Columns["All Assigned"].DataType = typeof(string);
				e.Layout.Bands[0].Columns["All Assigned"].Width = 48;
				e.Layout.Bands[0].Columns["All Assigned"].Header.Appearance.TextHAlign = HAlign.Center;
				e.Layout.Bands[0].Columns["All Assigned"].CellAppearance.TextHAlign = HAlign.Center;
				e.Layout.Bands[0].Columns["All Assigned"].SortIndicator = SortIndicator.None;
			} 
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnGridInitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e) {
			//Event handler for grid row initialization
			try {
				//			
				e.Row.Cells["Complete"].Value = (e.Row.Cells["TrailerComplete"].Value != DBNull.Value ? "Y" : "N");
				e.Row.Cells["All Assigned"].Value = (e.Row.Cells["FreightAssigned"].Value != DBNull.Value ? "Y" : "N");
			} 
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		#endregion
		#region Schedule Grid Events: OnTripSelectionChanged(), OnCalendarOpened(), OnCalendarClosed(), OnScheduleDateChanged()
		private void OnTripSelectionChanged(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in trip selections
			try {
				//Clear reference to prior trip object
				this.mDestinationTrip = null;
				if(this.grdShipSchedule.Selected.Rows.Count > 0) {
					//Get a trip object for the selected trip record
					string id = this.grdShipSchedule.Selected.Rows[0].Cells["TripID"].Value.ToString();
					ShipScheduleDS.ShipScheduleMasterTableRow row = (ShipScheduleDS.ShipScheduleMasterTableRow)this.mShipScheduleDS.ShipScheduleMasterTable.Select("TripID='" + id + "'")[0];
					this.mDestinationTrip = new ShipScheduleTrip(row);
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { OnValidateForm(null, null); }
		}
		private void OnCalendarOpened(object sender, System.EventArgs e) { this.mCalendarOpen = true; }
		private void OnCalendarClosed(object sender, System.EventArgs e) {
			//Event handler for date picker calendar closed
			this.Cursor = Cursors.WaitCursor;
			try {
				//Allow calendar to close
				this.dtpScheduleDate.Refresh();
				Application.DoEvents();
				
				//Flag calendar as closed; refresh ship schedule trips
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
				//Sync calendars & refresh ship schedule trips
				if(!this.mCalendarOpen) {
					this.mShipScheduleDS.Clear();
					this.mShipScheduleDS.Merge(AgentLineHaulFactory.GetAvailableTrips(this.mAgentTerminalID, this.dtpScheduleDate.Value));
					this.grdShipSchedule.Text = "Ship Schedule ( )";
					if(this.mShipScheduleDS.ShipScheduleMasterTable.Rows.Count > 0)
						this.grdShipSchedule.Text = "Ship Schedule (" + this.mShipScheduleDS.ShipScheduleMasterTable[0].ScheduleID + ")";
					OnTripSelectionChanged(null, null);
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { this.Cursor = Cursors.Default; }
		}
		#endregion
        private void OnValidateForm(object sender,System.EventArgs e) {
            //Validate user services
            this.btnOK.Enabled = (this.mDestinationTrip != null);
        }
        private void OnCmdClick(object sender,System.EventArgs e) {
			//Event handler for command buttons
			this.Cursor = Cursors.WaitCursor;
			try {
				//Set dialog result and hide
				Button btn = (Button)sender;
				switch(btn.Name) {
					case "btnCancel":	this.DialogResult = DialogResult.Cancel; break;
					case "btnOK":		this.DialogResult = DialogResult.OK; break;
				}
				this.Close();
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { this.Cursor = Cursors.Default; }
		}
	}
}
