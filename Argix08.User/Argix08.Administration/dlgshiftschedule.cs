//	File:	dlgdailyshiftschedule.cs
//	Author:	J. Heary
//	Date:	05/01/06
//	Desc:	Dialog to manage a daily shift schedule (3 shifts) for a single terminal.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Tsort.Freight;

namespace Tsort {
	//
	public class dlgDailyShiftScheduleDetail : System.Windows.Forms.Form {
		//Members
		#region Controls

		private System.Windows.Forms.Label lblTerminal;
		private System.Windows.Forms.Label _lblTerminal;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TabControl tabDialog;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.GroupBox fraShift1;
		private System.Windows.Forms.Label _lblWeekday;
		private System.Windows.Forms.Label lblWeekday;
		private System.Windows.Forms.Label _lblBreak1;
		private System.Windows.Forms.NumericUpDown updBreak1;
		private System.Windows.Forms.NumericUpDown updBreak2;
		private System.Windows.Forms.NumericUpDown updBreak3;
		private Tsort.Freight.ShiftDS mShiftDetailDS;
		private System.Windows.Forms.Label _lblShiftOrder;
		private System.Windows.Forms.ComboBox cboShiftOrder;
		private System.Windows.Forms.DateTimePicker dtpStart3;
		private System.Windows.Forms.DateTimePicker dtpStart2;
		private System.Windows.Forms.DateTimePicker dtpStart1;
		private System.Windows.Forms.ComboBox cboActiveShifts;
		private System.Windows.Forms.Label _lblShiftConfig;
		private System.Windows.Forms.Label lblStatus3;
		private System.Windows.Forms.Label lblStatus2;
		private System.Windows.Forms.Label lblStatus1;
		private System.Windows.Forms.GroupBox fraConfig;
		private System.Windows.Forms.Label _lblEnd1;
		private System.Windows.Forms.Label _lblStart1;
		private System.Windows.Forms.Label _lblStatus1;
		private System.Windows.Forms.GroupBox fraShift2;
		private System.Windows.Forms.GroupBox fraShift3;
		private System.Windows.Forms.Label _lblEnd2;
		private System.Windows.Forms.Label _lblStart2;
		private System.Windows.Forms.Label _lblStatus2;
		private System.Windows.Forms.Label _lblBreak2;
		private System.Windows.Forms.Label _lblEnd3;
		private System.Windows.Forms.Label _lblStart3;
		private System.Windows.Forms.Label _lblStatus3;
		private System.Windows.Forms.Label _lblBreak3;
		private System.Windows.Forms.DateTimePicker dtpEnd1;
		private System.Windows.Forms.DateTimePicker dtpEnd2;
		private System.Windows.Forms.DateTimePicker dtpEnd3;
		private System.Windows.Forms.Label lblDebug;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		
		//Constants
		private const string CMD_CANCEL = "&Cancel";
		private const string CMD_OK = "O&K";
		private const int SHIFT_HOUR_END = 16;
		private const int SHIFT_HOUR_START = 8;
		
		//Events
		public event ErrorEventHandler ErrorMessage=null;
		
		//Interface
		public dlgDailyShiftScheduleDetail(ref ShiftDS shiftDetail) {
			//Constructor
			string terminal="", weekday="";
			try {
				//Set command button and menu identities (used for onclick handler)
				InitializeComponent();
				this.btnOk.Text = CMD_OK;
				this.btnCancel.Text = CMD_CANCEL;
				
				//Set mediator service, data, and titlebar caption
				this.mShiftDetailDS = shiftDetail;
				if(this.mShiftDetailDS.ShiftDetailTable.Count>0) {
					terminal = this.mShiftDetailDS.ShiftDetailTable[0].Terminal;
					weekday = this.mShiftDetailDS.ShiftDetailTable[0].DayOfTheWeek;
					this.Text = "Daily Shift Schedule for " + weekday + " at " + terminal;
				}
				else
					this.Text = "Daily Shift Schedule (Data Unavailable)";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgDailyShiftScheduleDetail));
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblTerminal = new System.Windows.Forms.Label();
			this._lblTerminal = new System.Windows.Forms.Label();
			this.fraShift1 = new System.Windows.Forms.GroupBox();
			this.dtpEnd1 = new System.Windows.Forms.DateTimePicker();
			this.lblStatus1 = new System.Windows.Forms.Label();
			this._lblEnd1 = new System.Windows.Forms.Label();
			this._lblStart1 = new System.Windows.Forms.Label();
			this._lblStatus1 = new System.Windows.Forms.Label();
			this.updBreak1 = new System.Windows.Forms.NumericUpDown();
			this._lblBreak1 = new System.Windows.Forms.Label();
			this.dtpStart1 = new System.Windows.Forms.DateTimePicker();
			this.lblStatus3 = new System.Windows.Forms.Label();
			this.lblStatus2 = new System.Windows.Forms.Label();
			this.updBreak2 = new System.Windows.Forms.NumericUpDown();
			this.updBreak3 = new System.Windows.Forms.NumericUpDown();
			this.dtpStart2 = new System.Windows.Forms.DateTimePicker();
			this.dtpStart3 = new System.Windows.Forms.DateTimePicker();
			this.tabDialog = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.fraShift3 = new System.Windows.Forms.GroupBox();
			this.dtpEnd3 = new System.Windows.Forms.DateTimePicker();
			this._lblEnd3 = new System.Windows.Forms.Label();
			this._lblStart3 = new System.Windows.Forms.Label();
			this._lblStatus3 = new System.Windows.Forms.Label();
			this._lblBreak3 = new System.Windows.Forms.Label();
			this.fraShift2 = new System.Windows.Forms.GroupBox();
			this.dtpEnd2 = new System.Windows.Forms.DateTimePicker();
			this._lblEnd2 = new System.Windows.Forms.Label();
			this._lblStart2 = new System.Windows.Forms.Label();
			this._lblStatus2 = new System.Windows.Forms.Label();
			this._lblBreak2 = new System.Windows.Forms.Label();
			this.fraConfig = new System.Windows.Forms.GroupBox();
			this.cboActiveShifts = new System.Windows.Forms.ComboBox();
			this.cboShiftOrder = new System.Windows.Forms.ComboBox();
			this._lblShiftConfig = new System.Windows.Forms.Label();
			this._lblShiftOrder = new System.Windows.Forms.Label();
			this._lblWeekday = new System.Windows.Forms.Label();
			this.lblWeekday = new System.Windows.Forms.Label();
			this.mShiftDetailDS = new Tsort.Freight.ShiftDS();
			this.lblDebug = new System.Windows.Forms.Label();
			this.fraShift1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.updBreak1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updBreak2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updBreak3)).BeginInit();
			this.tabDialog.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.fraShift3.SuspendLayout();
			this.fraShift2.SuspendLayout();
			this.fraConfig.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mShiftDetailDS)).BeginInit();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.BackColor = System.Drawing.SystemColors.Control;
			this.btnOk.Enabled = false;
			this.btnOk.Location = new System.Drawing.Point(318, 282);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(96, 24);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "&OK";
			this.btnOk.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// btnCancel
			// 
			this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(420, 282);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(96, 24);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// lblTerminal
			// 
			this.lblTerminal.Location = new System.Drawing.Point(84, 12);
			this.lblTerminal.Name = "lblTerminal";
			this.lblTerminal.Size = new System.Drawing.Size(240, 18);
			this.lblTerminal.TabIndex = 19;
			this.lblTerminal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblTerminal
			// 
			this._lblTerminal.Location = new System.Drawing.Point(6, 12);
			this._lblTerminal.Name = "_lblTerminal";
			this._lblTerminal.Size = new System.Drawing.Size(72, 18);
			this._lblTerminal.TabIndex = 18;
			this._lblTerminal.Text = "Terminal: ";
			this._lblTerminal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// fraShift1
			// 
			this.fraShift1.Controls.Add(this.dtpEnd1);
			this.fraShift1.Controls.Add(this.lblStatus1);
			this.fraShift1.Controls.Add(this._lblEnd1);
			this.fraShift1.Controls.Add(this._lblStart1);
			this.fraShift1.Controls.Add(this._lblStatus1);
			this.fraShift1.Controls.Add(this.updBreak1);
			this.fraShift1.Controls.Add(this._lblBreak1);
			this.fraShift1.Controls.Add(this.dtpStart1);
			this.fraShift1.Location = new System.Drawing.Point(6, 42);
			this.fraShift1.Name = "fraShift1";
			this.fraShift1.Size = new System.Drawing.Size(156, 144);
			this.fraShift1.TabIndex = 0;
			this.fraShift1.TabStop = false;
			this.fraShift1.Text = "Shift 1";
			// 
			// dtpEnd1
			// 
			this.dtpEnd1.CustomFormat = "hh:mm tt";
			this.dtpEnd1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpEnd1.Location = new System.Drawing.Point(60, 54);
			this.dtpEnd1.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
			this.dtpEnd1.MinDate = new System.DateTime(2003, 1, 1, 0, 0, 0, 0);
			this.dtpEnd1.Name = "dtpEnd1";
			this.dtpEnd1.ShowUpDown = true;
			this.dtpEnd1.Size = new System.Drawing.Size(78, 21);
			this.dtpEnd1.TabIndex = 1;
			this.dtpEnd1.Value = new System.DateTime(2003, 7, 8, 0, 0, 0, 0);
			// 
			// lblStatus1
			// 
			this.lblStatus1.Location = new System.Drawing.Point(66, 114);
			this.lblStatus1.Name = "lblStatus1";
			this.lblStatus1.Size = new System.Drawing.Size(78, 18);
			this.lblStatus1.TabIndex = 3;
			this.lblStatus1.Text = "Active";
			this.lblStatus1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblEnd1
			// 
			this._lblEnd1.Location = new System.Drawing.Point(6, 54);
			this._lblEnd1.Name = "_lblEnd1";
			this._lblEnd1.Size = new System.Drawing.Size(48, 18);
			this._lblEnd1.TabIndex = 31;
			this._lblEnd1.Text = "End: ";
			this._lblEnd1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblStart1
			// 
			this._lblStart1.Location = new System.Drawing.Point(6, 24);
			this._lblStart1.Name = "_lblStart1";
			this._lblStart1.Size = new System.Drawing.Size(48, 18);
			this._lblStart1.TabIndex = 30;
			this._lblStart1.Text = "Start: ";
			this._lblStart1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblStatus1
			// 
			this._lblStatus1.Location = new System.Drawing.Point(6, 114);
			this._lblStatus1.Name = "_lblStatus1";
			this._lblStatus1.Size = new System.Drawing.Size(48, 18);
			this._lblStatus1.TabIndex = 29;
			this._lblStatus1.Text = "Status: ";
			this._lblStatus1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// updBreak1
			// 
			this.updBreak1.Location = new System.Drawing.Point(60, 84);
			this.updBreak1.Maximum = new System.Decimal(new int[] {
																	  480,
																	  0,
																	  0,
																	  0});
			this.updBreak1.Name = "updBreak1";
			this.updBreak1.Size = new System.Drawing.Size(48, 21);
			this.updBreak1.TabIndex = 2;
			this.updBreak1.Value = new System.Decimal(new int[] {
																	15,
																	0,
																	0,
																	0});
			this.updBreak1.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblBreak1
			// 
			this._lblBreak1.Location = new System.Drawing.Point(6, 84);
			this._lblBreak1.Name = "_lblBreak1";
			this._lblBreak1.Size = new System.Drawing.Size(48, 18);
			this._lblBreak1.TabIndex = 25;
			this._lblBreak1.Text = "Break: ";
			this._lblBreak1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtpStart1
			// 
			this.dtpStart1.CustomFormat = "hh:mm tt";
			this.dtpStart1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpStart1.Location = new System.Drawing.Point(60, 24);
			this.dtpStart1.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
			this.dtpStart1.MinDate = new System.DateTime(2003, 1, 1, 0, 0, 0, 0);
			this.dtpStart1.Name = "dtpStart1";
			this.dtpStart1.ShowUpDown = true;
			this.dtpStart1.Size = new System.Drawing.Size(78, 21);
			this.dtpStart1.TabIndex = 0;
			this.dtpStart1.Value = new System.DateTime(2003, 7, 8, 0, 0, 0, 0);
			this.dtpStart1.TextChanged += new System.EventHandler(this.ValidateForm);
			this.dtpStart1.ValueChanged += new System.EventHandler(this.ShiftChanged);
			// 
			// lblStatus3
			// 
			this.lblStatus3.Location = new System.Drawing.Point(66, 114);
			this.lblStatus3.Name = "lblStatus3";
			this.lblStatus3.Size = new System.Drawing.Size(72, 18);
			this.lblStatus3.TabIndex = 3;
			this.lblStatus3.Text = "Active";
			this.lblStatus3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblStatus2
			// 
			this.lblStatus2.Location = new System.Drawing.Point(66, 114);
			this.lblStatus2.Name = "lblStatus2";
			this.lblStatus2.Size = new System.Drawing.Size(72, 18);
			this.lblStatus2.TabIndex = 3;
			this.lblStatus2.Text = "Active";
			this.lblStatus2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// updBreak2
			// 
			this.updBreak2.Location = new System.Drawing.Point(60, 84);
			this.updBreak2.Maximum = new System.Decimal(new int[] {
																	  480,
																	  0,
																	  0,
																	  0});
			this.updBreak2.Name = "updBreak2";
			this.updBreak2.Size = new System.Drawing.Size(48, 21);
			this.updBreak2.TabIndex = 2;
			this.updBreak2.Value = new System.Decimal(new int[] {
																	15,
																	0,
																	0,
																	0});
			this.updBreak2.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// updBreak3
			// 
			this.updBreak3.Location = new System.Drawing.Point(60, 84);
			this.updBreak3.Maximum = new System.Decimal(new int[] {
																	  480,
																	  0,
																	  0,
																	  0});
			this.updBreak3.Name = "updBreak3";
			this.updBreak3.Size = new System.Drawing.Size(48, 21);
			this.updBreak3.TabIndex = 2;
			this.updBreak3.Value = new System.Decimal(new int[] {
																	15,
																	0,
																	0,
																	0});
			this.updBreak3.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// dtpStart2
			// 
			this.dtpStart2.CustomFormat = "hh:mm tt";
			this.dtpStart2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpStart2.Location = new System.Drawing.Point(60, 24);
			this.dtpStart2.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
			this.dtpStart2.MinDate = new System.DateTime(2003, 1, 1, 0, 0, 0, 0);
			this.dtpStart2.Name = "dtpStart2";
			this.dtpStart2.ShowUpDown = true;
			this.dtpStart2.Size = new System.Drawing.Size(78, 21);
			this.dtpStart2.TabIndex = 0;
			this.dtpStart2.Value = new System.DateTime(2003, 7, 9, 8, 0, 0, 0);
			this.dtpStart2.TextChanged += new System.EventHandler(this.ValidateForm);
			this.dtpStart2.ValueChanged += new System.EventHandler(this.ShiftChanged);
			// 
			// dtpStart3
			// 
			this.dtpStart3.CustomFormat = "hh:mm tt";
			this.dtpStart3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpStart3.Location = new System.Drawing.Point(60, 24);
			this.dtpStart3.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
			this.dtpStart3.MinDate = new System.DateTime(2003, 1, 1, 0, 0, 0, 0);
			this.dtpStart3.Name = "dtpStart3";
			this.dtpStart3.ShowUpDown = true;
			this.dtpStart3.Size = new System.Drawing.Size(78, 21);
			this.dtpStart3.TabIndex = 0;
			this.dtpStart3.Value = new System.DateTime(2003, 1, 1, 16, 0, 0, 0);
			this.dtpStart3.TextChanged += new System.EventHandler(this.ValidateForm);
			this.dtpStart3.ValueChanged += new System.EventHandler(this.ShiftChanged);
			// 
			// tabDialog
			// 
			this.tabDialog.Controls.Add(this.tabGeneral);
			this.tabDialog.Location = new System.Drawing.Point(3, 3);
			this.tabDialog.Name = "tabDialog";
			this.tabDialog.SelectedIndex = 0;
			this.tabDialog.Size = new System.Drawing.Size(513, 273);
			this.tabDialog.TabIndex = 1;
			// 
			// tabGeneral
			// 
			this.tabGeneral.Controls.Add(this.fraShift3);
			this.tabGeneral.Controls.Add(this.fraShift2);
			this.tabGeneral.Controls.Add(this.fraConfig);
			this.tabGeneral.Controls.Add(this._lblWeekday);
			this.tabGeneral.Controls.Add(this.lblWeekday);
			this.tabGeneral.Controls.Add(this.fraShift1);
			this.tabGeneral.Controls.Add(this._lblTerminal);
			this.tabGeneral.Controls.Add(this.lblTerminal);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Size = new System.Drawing.Size(505, 247);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			this.tabGeneral.ToolTipText = "General information";
			// 
			// fraShift3
			// 
			this.fraShift3.Controls.Add(this.dtpEnd3);
			this.fraShift3.Controls.Add(this._lblEnd3);
			this.fraShift3.Controls.Add(this._lblStart3);
			this.fraShift3.Controls.Add(this._lblStatus3);
			this.fraShift3.Controls.Add(this._lblBreak3);
			this.fraShift3.Controls.Add(this.dtpStart3);
			this.fraShift3.Controls.Add(this.updBreak3);
			this.fraShift3.Controls.Add(this.lblStatus3);
			this.fraShift3.Location = new System.Drawing.Point(342, 42);
			this.fraShift3.Name = "fraShift3";
			this.fraShift3.Size = new System.Drawing.Size(156, 144);
			this.fraShift3.TabIndex = 2;
			this.fraShift3.TabStop = false;
			this.fraShift3.Text = "Shift 3";
			// 
			// dtpEnd3
			// 
			this.dtpEnd3.CustomFormat = "hh:mm tt";
			this.dtpEnd3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpEnd3.Location = new System.Drawing.Point(60, 54);
			this.dtpEnd3.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
			this.dtpEnd3.MinDate = new System.DateTime(2003, 1, 1, 0, 0, 0, 0);
			this.dtpEnd3.Name = "dtpEnd3";
			this.dtpEnd3.ShowUpDown = true;
			this.dtpEnd3.Size = new System.Drawing.Size(78, 21);
			this.dtpEnd3.TabIndex = 1;
			this.dtpEnd3.Value = new System.DateTime(2003, 7, 8, 0, 0, 0, 0);
			// 
			// _lblEnd3
			// 
			this._lblEnd3.Location = new System.Drawing.Point(6, 54);
			this._lblEnd3.Name = "_lblEnd3";
			this._lblEnd3.Size = new System.Drawing.Size(48, 18);
			this._lblEnd3.TabIndex = 35;
			this._lblEnd3.Text = "End: ";
			this._lblEnd3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblStart3
			// 
			this._lblStart3.Location = new System.Drawing.Point(6, 24);
			this._lblStart3.Name = "_lblStart3";
			this._lblStart3.Size = new System.Drawing.Size(48, 18);
			this._lblStart3.TabIndex = 34;
			this._lblStart3.Text = "Start: ";
			this._lblStart3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblStatus3
			// 
			this._lblStatus3.Location = new System.Drawing.Point(6, 114);
			this._lblStatus3.Name = "_lblStatus3";
			this._lblStatus3.Size = new System.Drawing.Size(48, 18);
			this._lblStatus3.TabIndex = 33;
			this._lblStatus3.Text = "Status: ";
			this._lblStatus3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblBreak3
			// 
			this._lblBreak3.Location = new System.Drawing.Point(6, 84);
			this._lblBreak3.Name = "_lblBreak3";
			this._lblBreak3.Size = new System.Drawing.Size(48, 18);
			this._lblBreak3.TabIndex = 32;
			this._lblBreak3.Text = "Break: ";
			this._lblBreak3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// fraShift2
			// 
			this.fraShift2.Controls.Add(this.dtpEnd2);
			this.fraShift2.Controls.Add(this._lblEnd2);
			this.fraShift2.Controls.Add(this._lblStart2);
			this.fraShift2.Controls.Add(this._lblStatus2);
			this.fraShift2.Controls.Add(this._lblBreak2);
			this.fraShift2.Controls.Add(this.dtpStart2);
			this.fraShift2.Controls.Add(this.updBreak2);
			this.fraShift2.Controls.Add(this.lblStatus2);
			this.fraShift2.Location = new System.Drawing.Point(174, 42);
			this.fraShift2.Name = "fraShift2";
			this.fraShift2.Size = new System.Drawing.Size(156, 144);
			this.fraShift2.TabIndex = 1;
			this.fraShift2.TabStop = false;
			this.fraShift2.Text = "Shift 2";
			// 
			// dtpEnd2
			// 
			this.dtpEnd2.CustomFormat = "hh:mm tt";
			this.dtpEnd2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpEnd2.Location = new System.Drawing.Point(60, 54);
			this.dtpEnd2.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
			this.dtpEnd2.MinDate = new System.DateTime(2003, 1, 1, 0, 0, 0, 0);
			this.dtpEnd2.Name = "dtpEnd2";
			this.dtpEnd2.ShowUpDown = true;
			this.dtpEnd2.Size = new System.Drawing.Size(78, 21);
			this.dtpEnd2.TabIndex = 1;
			this.dtpEnd2.Value = new System.DateTime(2003, 7, 8, 0, 0, 0, 0);
			// 
			// _lblEnd2
			// 
			this._lblEnd2.Location = new System.Drawing.Point(6, 54);
			this._lblEnd2.Name = "_lblEnd2";
			this._lblEnd2.Size = new System.Drawing.Size(48, 18);
			this._lblEnd2.TabIndex = 35;
			this._lblEnd2.Text = "End: ";
			this._lblEnd2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblStart2
			// 
			this._lblStart2.Location = new System.Drawing.Point(6, 24);
			this._lblStart2.Name = "_lblStart2";
			this._lblStart2.Size = new System.Drawing.Size(48, 18);
			this._lblStart2.TabIndex = 34;
			this._lblStart2.Text = "Start: ";
			this._lblStart2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblStatus2
			// 
			this._lblStatus2.Location = new System.Drawing.Point(6, 114);
			this._lblStatus2.Name = "_lblStatus2";
			this._lblStatus2.Size = new System.Drawing.Size(48, 18);
			this._lblStatus2.TabIndex = 33;
			this._lblStatus2.Text = "Status: ";
			this._lblStatus2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblBreak2
			// 
			this._lblBreak2.Location = new System.Drawing.Point(6, 84);
			this._lblBreak2.Name = "_lblBreak2";
			this._lblBreak2.Size = new System.Drawing.Size(48, 18);
			this._lblBreak2.TabIndex = 32;
			this._lblBreak2.Text = "Break: ";
			this._lblBreak2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// fraConfig
			// 
			this.fraConfig.Controls.Add(this.cboActiveShifts);
			this.fraConfig.Controls.Add(this.cboShiftOrder);
			this.fraConfig.Controls.Add(this._lblShiftConfig);
			this.fraConfig.Controls.Add(this._lblShiftOrder);
			this.fraConfig.Location = new System.Drawing.Point(6, 192);
			this.fraConfig.Name = "fraConfig";
			this.fraConfig.Size = new System.Drawing.Size(492, 48);
			this.fraConfig.TabIndex = 3;
			this.fraConfig.TabStop = false;
			this.fraConfig.Text = "Schedule Configuration";
			// 
			// cboActiveShifts
			// 
			this.cboActiveShifts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboActiveShifts.Location = new System.Drawing.Point(90, 18);
			this.cboActiveShifts.Name = "cboActiveShifts";
			this.cboActiveShifts.Size = new System.Drawing.Size(48, 21);
			this.cboActiveShifts.TabIndex = 0;
			this.cboActiveShifts.SelectedValueChanged += new System.EventHandler(this.ActiveShiftsChange);
			this.cboActiveShifts.SelectedIndexChanged += new System.EventHandler(this.ValidateForm);
			// 
			// cboShiftOrder
			// 
			this.cboShiftOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboShiftOrder.Location = new System.Drawing.Point(396, 18);
			this.cboShiftOrder.Name = "cboShiftOrder";
			this.cboShiftOrder.Size = new System.Drawing.Size(78, 21);
			this.cboShiftOrder.TabIndex = 1;
			this.cboShiftOrder.SelectedValueChanged += new System.EventHandler(this.ShiftOrderChanged);
			this.cboShiftOrder.SelectedIndexChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblShiftConfig
			// 
			this._lblShiftConfig.Location = new System.Drawing.Point(6, 18);
			this._lblShiftConfig.Name = "_lblShiftConfig";
			this._lblShiftConfig.Size = new System.Drawing.Size(78, 18);
			this._lblShiftConfig.TabIndex = 39;
			this._lblShiftConfig.Text = "Active Shifts";
			this._lblShiftConfig.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblShiftOrder
			// 
			this._lblShiftOrder.Location = new System.Drawing.Point(294, 18);
			this._lblShiftOrder.Name = "_lblShiftOrder";
			this._lblShiftOrder.Size = new System.Drawing.Size(96, 18);
			this._lblShiftOrder.TabIndex = 37;
			this._lblShiftOrder.Text = "Shift Order";
			this._lblShiftOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblWeekday
			// 
			this._lblWeekday.Location = new System.Drawing.Point(342, 12);
			this._lblWeekday.Name = "_lblWeekday";
			this._lblWeekday.Size = new System.Drawing.Size(48, 18);
			this._lblWeekday.TabIndex = 21;
			this._lblWeekday.Text = "Day:";
			this._lblWeekday.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblWeekday
			// 
			this.lblWeekday.Location = new System.Drawing.Point(396, 12);
			this.lblWeekday.Name = "lblWeekday";
			this.lblWeekday.Size = new System.Drawing.Size(96, 18);
			this.lblWeekday.TabIndex = 22;
			this.lblWeekday.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// mShiftDetailDS
			// 
			this.mShiftDetailDS.DataSetName = "ShiftDS";
			this.mShiftDetailDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// lblDebug
			// 
			this.lblDebug.Location = new System.Drawing.Point(12, 288);
			this.lblDebug.Name = "lblDebug";
			this.lblDebug.Size = new System.Drawing.Size(156, 18);
			this.lblDebug.TabIndex = 22;
			this.lblDebug.Text = "Debug Label";
			this.lblDebug.Visible = false;
			// 
			// dlgDailyShiftScheduleDetail
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(522, 311);
			this.Controls.Add(this.lblDebug);
			this.Controls.Add(this.tabDialog);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgDailyShiftScheduleDetail";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Daily Shift Schedule";
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.fraShift1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.updBreak1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updBreak2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updBreak3)).EndInit();
			this.tabDialog.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.fraShift3.ResumeLayout(false);
			this.fraShift2.ResumeLayout(false);
			this.fraConfig.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mShiftDetailDS)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Initialize controls - set default values
			int shifts = 0;
			int shiftOrder = 0;
			DateTime dts1, dts2, dts3;
			this.Cursor = Cursors.WaitCursor;
			try {
				//Set initial service states
				this.Visible = true;
				Application.DoEvents();
				
				//Get selection lists
				//N/A
				
				//Init controls
				this.lblTerminal.Text = this.mShiftDetailDS.ShiftDetailTable[0].Terminal;
				this.lblWeekday.Text = this.mShiftDetailDS.ShiftDetailTable[0].DayOfTheWeek;
				this.cboActiveShifts.Items.AddRange(new object[]{"1","2","3"});
				this.cboShiftOrder.Items.AddRange(new object[]{"1 2 3","1 3 2","2 1 3","2 3 1","3 1 2","3 2 1"});
				
				this.dtpStart1.MinDate = new DateTime(2000, 1, 1, 12, 0, 0);
				this.dtpStart1.MaxDate = new DateTime(2000, 1, 3, 12, 0, 0);
				this.dtpEnd1.MinDate = new DateTime(2000, 1, 1, 12, 0, 0);
				this.dtpEnd1.MaxDate = new DateTime(2000, 1, 3, 12, 0, 0);
				this.updBreak1.Minimum = 0;
				this.updBreak1.Maximum = 1440;
				this.lblStatus3.Text = "Inactive";
				this.dtpStart2.MinDate = new DateTime(2000, 1, 1, 12, 0, 0);
				this.dtpStart2.MaxDate = new DateTime(2000, 1, 3, 12, 0, 0);
				this.dtpEnd2.MinDate = new DateTime(2000, 1, 1, 12, 0, 0);
				this.dtpEnd2.MaxDate = new DateTime(2000, 1, 3, 12, 0, 0);
				this.updBreak2.Minimum = 0;
				this.updBreak2.Maximum = 1440;
				this.lblStatus2.Text = "Inactive";
				this.dtpStart3.MinDate = new DateTime(2000, 1, 1, 12, 0, 0);
				this.dtpStart3.MaxDate = new DateTime(2000, 1, 3, 12, 0, 0);
				this.dtpEnd3.MinDate = new DateTime(2000, 1, 1, 12, 0, 0);
				this.dtpEnd3.MaxDate = new DateTime(2000, 1, 3, 12, 0, 0);
				this.updBreak3.Minimum = 0;
				this.updBreak3.Maximum = 1440;
				this.lblStatus3.Text = "Inactive";
				
				//Count the active shifts and determine chronological shift order
				dts1 = new DateTime(2000, 1, 3, 1, 0, 0);
				if(this.mShiftDetailDS.ShiftDetailTable[0].Shift1IsActive) {
					shifts+=1;
					dts1 = new DateTime(this.mShiftDetailDS.ShiftDetailTable[0].Shift1StartTime.Ticks);
				}
				dts2 = new DateTime(2000, 1, 3, 2, 0, 0);
				if(this.mShiftDetailDS.ShiftDetailTable[0].Shift2IsActive) {
					shifts+=1;
					dts2 = new DateTime(this.mShiftDetailDS.ShiftDetailTable[0].Shift2StartTime.Ticks);
				}
				dts3 = new DateTime(2000, 1, 3, 3, 0, 0);
				if(this.mShiftDetailDS.ShiftDetailTable[0].Shift3IsActive) {
					shifts+=1;
					dts3 = new DateTime(this.mShiftDetailDS.ShiftDetailTable[0].Shift3StartTime.Ticks);
				}
				if(dts1.Ticks<=dts2.Ticks && dts2.Ticks<=dts3.Ticks)		shiftOrder = 0;
				else if(dts1.Ticks<=dts3.Ticks && dts3.Ticks<=dts2.Ticks)	shiftOrder = 1;
				else if(dts2.Ticks<=dts1.Ticks && dts1.Ticks<=dts3.Ticks)	shiftOrder = 2;
				else if(dts2.Ticks<=dts3.Ticks && dts3.Ticks<=dts1.Ticks)	shiftOrder = 3;
				else if(dts3.Ticks<=dts1.Ticks && dts1.Ticks<=dts2.Ticks)	shiftOrder = 4;
				else if(dts3.Ticks<=dts2.Ticks && dts2.Ticks<=dts1.Ticks)	shiftOrder = 5;

				//Set current configuration
				this.cboShiftOrder.SelectedIndex = shiftOrder;
				this.cboActiveShifts.SelectedIndex = (shifts-1);
				this.dtpEnd1.Enabled = this.dtpEnd2.Enabled = this.dtpEnd3.Enabled = false;
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.btnOk.Enabled = false; this.Cursor = Cursors.Default; }
		}
		private void ShiftOrderChanged(object sender, System.EventArgs e) {
			//Shift order changed event handler
			try {
				//Update shift names (maintains chronological shift order)
				char[] token = {Convert.ToChar(" ")};
				string[] shiftOrder = this.cboShiftOrder.Text.Split(token, 3);
				this.fraShift1.Text = "Shift " + shiftOrder[0];
				this.fraShift2.Text = "Shift " + shiftOrder[1];
				this.fraShift3.Text = "Shift " + shiftOrder[2];
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void ActiveShiftsChange(object sender, System.EventArgs e) {
			//Change in the number of active shifts event handler
			int shifts = 1;
			DateTime[] dts = null, dte = null;
			int[] bk = null;
			int day = 2;
			try {
				//Init
				dts = new DateTime[3];
				dte = new DateTime[3];
				bk = new int[3];
				shifts = Convert.ToInt32(this.cboActiveShifts.Text);
				char[] token = {Convert.ToChar(" ")};
				string[] shiftOrder = this.cboShiftOrder.Text.Split(token, 3);
				
				//Index the shifts for reference; assume the following:
				//First shift of day can begin on 1/1/2000 or 1/2/2000
				//Last shift of day can end on 1/2/2000 or 1/3/2000
				//All other shifts are on 1/2/2000
				int shift1 = Convert.ToInt32(shiftOrder[0]);
				int shift3 = Convert.ToInt32(shiftOrder[0]);
				if(shifts==2) shift3 = Convert.ToInt32(shiftOrder[1]);
				if(shifts==3) shift3 = Convert.ToInt32(shiftOrder[2]);
				day = (shift1==1 && this.mShiftDetailDS.ShiftDetailTable[0].Shift1StartTime.Hour>=SHIFT_HOUR_END) ? 1 : 2;
				dts[0] = new DateTime(2000, 1, day, this.mShiftDetailDS.ShiftDetailTable[0].Shift1StartTime.Hour, this.mShiftDetailDS.ShiftDetailTable[0].Shift1StartTime.Minute, 0);
				day = (shift3==1 && this.mShiftDetailDS.ShiftDetailTable[0].Shift1EndTime.Hour<=SHIFT_HOUR_START) ? 3 : 2;
				dte[0] = new DateTime(2000, 1, day, this.mShiftDetailDS.ShiftDetailTable[0].Shift1EndTime.Hour, this.mShiftDetailDS.ShiftDetailTable[0].Shift1EndTime.Minute, 0);
				bk[0] = this.mShiftDetailDS.ShiftDetailTable[0].Shift1BreakDuration;
				day = (shift1==2 && this.mShiftDetailDS.ShiftDetailTable[0].Shift2StartTime.Hour>=SHIFT_HOUR_END) ? 1 : 2;
				dts[1] = new DateTime(2000, 1, day, this.mShiftDetailDS.ShiftDetailTable[0].Shift2StartTime.Hour, this.mShiftDetailDS.ShiftDetailTable[0].Shift2StartTime.Minute, 0);
				day = (shift3==2 && this.mShiftDetailDS.ShiftDetailTable[0].Shift2EndTime.Hour<=SHIFT_HOUR_START) ? 3 : 2;
				dte[1] = new DateTime(2000, 1, day, this.mShiftDetailDS.ShiftDetailTable[0].Shift2EndTime.Hour, this.mShiftDetailDS.ShiftDetailTable[0].Shift2EndTime.Minute, 0);
				bk[1] = this.mShiftDetailDS.ShiftDetailTable[0].Shift2BreakDuration;
				day = (shift1==3 && this.mShiftDetailDS.ShiftDetailTable[0].Shift3StartTime.Hour>=SHIFT_HOUR_END) ? 1 : 2;
				dts[2] = new DateTime(2000, 1, day, this.mShiftDetailDS.ShiftDetailTable[0].Shift3StartTime.Hour, this.mShiftDetailDS.ShiftDetailTable[0].Shift3StartTime.Minute, 0);
				day = (shift3==3 && this.mShiftDetailDS.ShiftDetailTable[0].Shift3EndTime.Hour<=SHIFT_HOUR_START) ? 3 : 2;
				dte[2] = new DateTime(2000, 1, day, this.mShiftDetailDS.ShiftDetailTable[0].Shift3EndTime.Hour, this.mShiftDetailDS.ShiftDetailTable[0].Shift3EndTime.Minute, 0);
				bk[2] = this.mShiftDetailDS.ShiftDetailTable[0].Shift3BreakDuration;
				
				//Update configuration for specified number of shifts
				this.dtpStart1.Value = dts[Convert.ToInt32(shiftOrder[0])-1];
				this.dtpEnd1.Value = dte[Convert.ToInt32(shiftOrder[0])-1];
				this.updBreak1.Minimum = 0;
				this.updBreak1.Maximum = 1440/shifts;
				this.updBreak1.Value = bk[Convert.ToInt32(shiftOrder[0])-1];
				this.lblStatus1.Text = "Active";
				this.dtpStart2.Value = dts[Convert.ToInt32(shiftOrder[1])-1];
				this.dtpEnd2.Value = dte[Convert.ToInt32(shiftOrder[1])-1];
				this.updBreak2.Minimum = 0;
				this.updBreak2.Maximum = 1440/shifts;
				this.updBreak2.Value = bk[Convert.ToInt32(shiftOrder[1])-1];
				this.lblStatus2.Text = (shifts>1) ? "Active" : "Inactive";
				this.dtpStart3.Value = dts[Convert.ToInt32(shiftOrder[2])-1];
				this.dtpEnd3.Value = dte[Convert.ToInt32(shiftOrder[2])-1];
				this.updBreak3.Minimum = 0;
				this.updBreak3.Maximum = 1440/shifts;
				this.updBreak3.Value = bk[Convert.ToInt32(shiftOrder[2])-1];
				this.lblStatus3.Text = (shifts>2) ? "Active" : "Inactive";
				this.dtpStart1.Enabled = this.updBreak1.Enabled = this.lblStatus1.Enabled = true;
				this.dtpStart2.Enabled = this.updBreak2.Enabled = this.lblStatus2.Enabled = (shifts>1);
				this.dtpStart3.Enabled = this.updBreak3.Enabled = this.lblStatus3.Enabled = (shifts>2);
				
				//Validate configuration
				ShiftChanged(this.dtpStart1, EventArgs.Empty);
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void ShiftChanged(object sender, System.EventArgs e) {
			//Event handler for changes in shift data
			DateTimePicker dtp = null;
			int shifts = 1;
			DateTime dt1s, dt1e, dt2s, dt2e, dt3s, dt3e;
			DateTime dtStartMax;
			try {
				//Validate\correct shift data
				if(this.cboActiveShifts.Text=="")
					return;
				else
					shifts = Convert.ToInt32(this.cboActiveShifts.Text);
				dtp = (DateTimePicker)sender;
				switch(shifts) {
					case 1:
						//Maintain a 24hr single shift
						//Capture control values
						dt1s = new DateTime(this.dtpStart1.Value.Ticks);
						dtStartMax = new DateTime(dt1s.Year, dt1s.Month, dt1s.Day, SHIFT_HOUR_START, 0, 0);
						dt1e = new DateTime(dt1s.AddMinutes(1440).Ticks);
						
						//Validate and revise user changes
						switch(dtp.Name) {
							case "dtpStart1":
								//- max start of shift is 3AM
								if(DateTime.Compare(dt1s, dtStartMax)>0)
									dt1s = dtStartMax;
								
								//- shift end is 24hrs (1440min) later
								dt1e = new DateTime(dt1s.AddMinutes(1440).Ticks);
								break;
						}
						
						//Update control values
						this.dtpStart1.Value = dt1s;
						this.dtpEnd1.Value = dt1e;
						this.updBreak1.Minimum = 0;
						this.updBreak1.Maximum = 1440;
						break;
					case 2:
						//Maintain a 24hr double shift
						//Capture control values
						dt1s = new DateTime(this.dtpStart1.Value.Ticks);
						dtStartMax = new DateTime(dt1s.Year, dt1s.Month, dt1s.Day, SHIFT_HOUR_START, 0, 0);
						dt2s = new DateTime(this.dtpStart2.Value.Ticks);
						dt1e = dt2s;
						dt2e = new DateTime(dt1s.AddMinutes(1440).Ticks);
						
						//Validate and revise user changes
						switch(dtp.Name) {
							case "dtpStart1":
								//- max start of shift is 3AM
								if(DateTime.Compare(dt1s, dtStartMax)>0)
									dt1s = dtStartMax;
								
								//- second shift end is 24hrs (1440min) later than start of first shift
								dt2e = new DateTime(dt1s.AddMinutes(1440).Ticks);
								
								//- second shift begins after first (no overlap)
								if(dt1s.Ticks>dt2s.Ticks)
									dt2s = dt1s;		//S1 start pushes S2 start
								else if(dt2e.Ticks<dt2s.Ticks)
									dt2s = dt2e;		//S2 end pulls S2 end
								dt1e = dt2s;			//S1 end = S2 start
								break;
							case "dtpStart2":
								//Maintain time border between both shifts
								if(dt2s.Ticks<dt1s.Ticks)
									dt2s = dt1s;		//S2 cannot precede S1
								else if(dt2s.Ticks>dt2e.Ticks)
									dt2s = dt2e;		//S2 cannot exceed E2
								dt1e = dt2s;			//S1 end = S2 start
								break;
						}
						
						//Update control values
						this.dtpStart1.Value = dt1s;
						this.dtpEnd1.Value = dt1e;
						this.dtpStart2.Value = dt2s;
						this.dtpEnd2.Value = dt2e;
						this.updBreak1.Minimum = 0;
						this.updBreak1.Maximum = 10000000*(dt1e.Ticks-dt1s.Ticks);
						this.updBreak2.Minimum = 0;
						this.updBreak2.Maximum = 10000000*(dt2e.Ticks-dt2s.Ticks);
						break;
					case 3:
						//Maintain a 24hr triple shift
						//Capture control values
						dt1s = new DateTime(this.dtpStart1.Value.Ticks);
						dtStartMax = new DateTime(dt1s.Year, dt1s.Month, dt1s.Day, SHIFT_HOUR_START, 0, 0);
						dt2s = new DateTime(this.dtpStart2.Value.Ticks);
						dt3s = new DateTime(this.dtpStart3.Value.Ticks);
						dt1e = dt2s;
						dt2e = dt3s;
						dt3e = new DateTime(dt1s.AddMinutes(1440).Ticks);
						
						//Validate and revise user changes
						switch(dtp.Name) {
							case "dtpStart1":
								//- max start of shift is 3AM
								if(DateTime.Compare(dt1s, dtStartMax)>0)
									dt1s = dtStartMax;
								
								//- second shift end is 24hrs (1440min) later than start of first shift
								dt3e = new DateTime(dt1s.AddMinutes(1440).Ticks);
								
								//- second shift begins after first (no overlap)
								if(dt1s.Ticks>dt2s.Ticks) {
									dt2s = dt1s;		//S1 start pushes S2 start
									if(dt2s.Ticks>dt3s.Ticks)
										dt3s = dt2s;	//S2 start pushes S3 start
								}
								else if(dt3e.Ticks<dt3s.Ticks) {
									dt3s = dt3e;		//S3 end pulls S3 start
									if(dt3s.Ticks<dt2s.Ticks)
										dt2s = dt3s;	//S3 start pulls S2 start
								}
								dt1e = dt2s;			//S1 end = S2 start
								dt2e = dt3s;			//S2 end = S3 start
								break;
							case "dtpStart2":
								//Maintain time border between both shifts
								if(dt2s.Ticks<dt1s.Ticks)
									dt2s = dt1s;		//S2 start cannot precede S1 start
								else if(dt2s.Ticks>dt3s.Ticks)
									dt2s = dt3s;		//S2 start cannot exceed S3 start
								dt1e = dt2s;			//S1 end = S2 start
								dt2e = dt3s;			//S2 end = S3 start
								break;
							case "dtpStart3":
								//Maintain time border between both shifts
								if(dt3s.Ticks<dt2s.Ticks)
									dt3s = dt2s;		//S3 start cannot precede S2 start
								else if(dt3s.Ticks>dt3e.Ticks)
									dt3s = dt3e;		//S3 start cannot exceed E3 end
								dt2e = dt3s;			//S2 end = S3 start
								break;
						}
						
						//Update control values
						this.dtpStart1.Value = dt1s;
						this.dtpEnd1.Value = dt1e;
						this.dtpStart2.Value = dt2s;
						this.dtpEnd2.Value = dt2e;
						this.dtpStart3.Value = dt3s;
						this.dtpEnd3.Value = dt3e;
						this.updBreak1.Minimum = 0;
						this.updBreak1.Maximum = 10000000*(dt1e.Ticks-dt1s.Ticks);
						this.updBreak2.Minimum = 0;
						this.updBreak2.Maximum = 10000000*(dt2e.Ticks-dt2s.Ticks);
						this.updBreak3.Minimum = 0;
						this.updBreak2.Maximum = 10000000*(dt3e.Ticks-dt3s.Ticks);
						break;
				}				
			} 
			catch(Exception ex) { reportError(ex); }
			finally { if(dtp!=null) this.lblDebug.Text = dtp.Value.ToShortDateString() + " " + dtp.Value.ToShortTimeString(); }
		}
		#region User Services: ValidateForm(), OnCmdClick()
		private void ValidateForm(object sender, System.EventArgs e) {
			//Event handler for form data changes
			try {
				if(this.mShiftDetailDS.ShiftDetailTable.Count>0) {
					//Enable OK service if form data is valid
					this.btnOk.Enabled = (true);
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnCmdClick(object sender, System.EventArgs e) {
			//Command button handler
			DateTime[] dts = null, dte = null;
			int[] bk = null;
			bool[] active = null;
			try {
				Button btn = (Button)sender;
				switch(btn.Text) {
					case CMD_CANCEL:
						//Close the dialog
						this.DialogResult = DialogResult.Cancel;
						this.Close();
						break;
					case CMD_OK:
						//Update details with control values
						this.Cursor = Cursors.WaitCursor;
						dts = new DateTime[3];
						dte = new DateTime[3];
						bk = new int[3];
						active = new Boolean[3];
						char[] token = {Convert.ToChar(" ")};
						string[] shiftOrder = this.cboShiftOrder.Text.Split(token, 3);
						dts[Convert.ToInt32(shiftOrder[0])-1] = new DateTime(this.dtpStart1.Value.Ticks);
						dte[Convert.ToInt32(shiftOrder[0])-1] = new DateTime(this.dtpEnd1.Value.Ticks);
						bk[Convert.ToInt32(shiftOrder[0])-1] = (int)this.updBreak1.Value;
						active[Convert.ToInt32(shiftOrder[0])-1] = (this.lblStatus1.Text=="Active") ? true : false;
						dts[Convert.ToInt32(shiftOrder[1])-1] = new DateTime(this.dtpStart2.Value.Ticks);
						dte[Convert.ToInt32(shiftOrder[1])-1] = new DateTime(this.dtpEnd2.Value.Ticks);
						bk[Convert.ToInt32(shiftOrder[1])-1] = (int)this.updBreak2.Value;
						active[Convert.ToInt32(shiftOrder[1])-1] = (this.lblStatus2.Text=="Active") ? true : false;
						dts[Convert.ToInt32(shiftOrder[2])-1] = new DateTime(this.dtpStart3.Value.Ticks);
						dte[Convert.ToInt32(shiftOrder[2])-1] = new DateTime(this.dtpEnd3.Value.Ticks);
						bk[Convert.ToInt32(shiftOrder[2])-1] = (int)this.updBreak3.Value;
						active[Convert.ToInt32(shiftOrder[2])-1] = (this.lblStatus3.Text=="Active") ? true : false;
						
						this.mShiftDetailDS.ShiftDetailTable[0].Shift1StartTime = dts[0];
						this.mShiftDetailDS.ShiftDetailTable[0].Shift1EndTime = dte[0];
						this.mShiftDetailDS.ShiftDetailTable[0].Shift1BreakDuration = bk[0];
						this.mShiftDetailDS.ShiftDetailTable[0].Shift1IsActive = active[0];
						this.mShiftDetailDS.ShiftDetailTable[0].Shift2StartTime = dts[1];
						this.mShiftDetailDS.ShiftDetailTable[0].Shift2EndTime = dte[1];
						this.mShiftDetailDS.ShiftDetailTable[0].Shift2BreakDuration = bk[1];
						this.mShiftDetailDS.ShiftDetailTable[0].Shift2IsActive = active[1];
						this.mShiftDetailDS.ShiftDetailTable[0].Shift3StartTime = dts[2];
						this.mShiftDetailDS.ShiftDetailTable[0].Shift3EndTime = dte[2];
						this.mShiftDetailDS.ShiftDetailTable[0].Shift3BreakDuration = bk[2];
						this.mShiftDetailDS.ShiftDetailTable[0].Shift3IsActive = active[2];
						this.mShiftDetailDS.AcceptChanges();
						Debug.Write(this.mShiftDetailDS.GetXml() + "\n");
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
