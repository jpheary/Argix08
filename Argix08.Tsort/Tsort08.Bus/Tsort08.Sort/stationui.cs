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
using Tsort.Devices.Scales;
using Tsort.Freight;
using Tsort.Sort;

namespace Tsort.Sort {
	/// <summary>User interface control for the Sort Library.</summary>
	public class StationUI : System.Windows.Forms.UserControl {
		//Members
		private StationOperator mOperator=null;
		private Label[] InputLabels=null;
		
		#region Controls
		private System.Windows.Forms.GroupBox grpSortedItem;
		private System.Windows.Forms.GroupBox grpLine3;
		private System.Windows.Forms.GroupBox grpLine2;
		private System.Windows.Forms.GroupBox grpLine1;
		private System.Windows.Forms.TextBox txtZone;
		private System.Windows.Forms.TextBox txtTLNumber;
		private System.Windows.Forms.TextBox txtDamage;
		private System.Windows.Forms.TextBox txtStoreName;
		private System.Windows.Forms.TextBox txtLabelNumber;
		private System.Windows.Forms.RadioButton rdoPallet;
		private System.Windows.Forms.RadioButton rdoCarton;
		private System.Windows.Forms.GroupBox grpItemsStatistics;
		private System.Windows.Forms.TextBox txtStatisticsItemsPerMinute;
		private System.Windows.Forms.TextBox txtStatisticsItemsPerHour;
		private System.Windows.Forms.TextBox txtStatisticsTotal;
		private System.Windows.Forms.TextBox txtStatisticsDeleted;
		private System.Windows.Forms.TextBox txtStatisticsEntered;
		private System.Windows.Forms.TextBox txtStatisticsDownTime;
		private System.Windows.Forms.TextBox txtStatisticsStartTime;
		private System.Windows.Forms.GroupBox grpScale;
		private System.Windows.Forms.TextBox txtWeight;
		private System.Windows.Forms.ListView lsvAssignments;
		private System.Windows.Forms.ColumnHeader colTDS;
		private System.Windows.Forms.ColumnHeader colClient;
		private System.Windows.Forms.ColumnHeader colShipper;
		private System.Windows.Forms.ColumnHeader colSortType;
		private System.Windows.Forms.ColumnHeader colStation;
		private System.Windows.Forms.ColumnHeader colFreightID;
		private System.Windows.Forms.ColumnHeader colFreigthType;
		private System.Windows.Forms.ColumnHeader colClientNum;
		private System.Windows.Forms.ColumnHeader colClientDiv;
		private System.Windows.Forms.ColumnHeader colShipperNum;
		private System.Windows.Forms.ColumnHeader colPickup;
		private System.Windows.Forms.ColumnHeader colTrailerNum;
		private System.Windows.Forms.Label _lblInput3;
		private System.Windows.Forms.CheckBox chkNonConveyable;
		private System.Windows.Forms.Label _lblZone;
		private System.Windows.Forms.Label _lblDamage;
		private System.Windows.Forms.Label _lblTLNumber;
		private System.Windows.Forms.Label _lblStoreName;
		private System.Windows.Forms.Label _lblInput2;
		private System.Windows.Forms.Label _lblInput1;
		private System.Windows.Forms.Label _lblLableNumber;
		private System.Windows.Forms.Label _lblType;
		private System.Windows.Forms.Label _lblItemsMinute;
		private System.Windows.Forms.Label _lblItemsHour;
		private System.Windows.Forms.Label _lblTotal;
		private System.Windows.Forms.Label _lblDeleted;
		private System.Windows.Forms.Label _lblEntered;
		private System.Windows.Forms.Label _lblDownTime;
		private System.Windows.Forms.Label _lblStartTime;
		private System.Windows.Forms.Label _lblWeight;
		private System.Windows.Forms.TextBox txtInputs;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		public StationUI() {
			//Constructor
			try {
				InitializeComponent();
				InputLabels = new Label[]{ this._lblInput1, this._lblInput2, this._lblInput3 };
			} 
			catch(Exception ex) { throw new ApplicationException("Unexpected error creating new SortUI instance.", ex); }
		}
		protected override void Dispose( bool disposing ) { if( disposing ) { if(components != null)components.Dispose(); } base.Dispose( disposing ); }
		/// <summary>Specifies the Tsort.Sort.StationOperator object that provides data for the control.</summary>
		public Tsort.Sort.StationOperator Operator { 
			get { return this.mOperator; } 
			set { 
				this.mOperator = value;
				if(this.mOperator != null) {
					this.mOperator.StationAssignmentsChanged += new EventHandler(OnAssignmentsChanged);
					this.mOperator.SortedItemCreated += new SortedItemEventHandler(OnSortedItemCreated);
					this.mOperator.SortedItemComplete += new SortedItemEventHandler(OnSortedItemComplete);
					this.mOperator.Station.PrinterChanged += new EventHandler(OnPrinterChanged);
					this.mOperator.Station.ScaleChanged += new EventHandler(OnScaleChanged);
					//this.mOperator.Station.SortStatistics.StatisticsChanged += new EventHandler(OnStatisticsChanged);

				}
			} 
		}
		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.grpSortedItem = new System.Windows.Forms.GroupBox();
			this.grpLine3 = new System.Windows.Forms.GroupBox();
			this.grpLine2 = new System.Windows.Forms.GroupBox();
			this.grpLine1 = new System.Windows.Forms.GroupBox();
			this._lblInput3 = new System.Windows.Forms.Label();
			this.chkNonConveyable = new System.Windows.Forms.CheckBox();
			this.txtZone = new System.Windows.Forms.TextBox();
			this.txtTLNumber = new System.Windows.Forms.TextBox();
			this.txtDamage = new System.Windows.Forms.TextBox();
			this.txtStoreName = new System.Windows.Forms.TextBox();
			this.txtInputs = new System.Windows.Forms.TextBox();
			this._lblZone = new System.Windows.Forms.Label();
			this._lblDamage = new System.Windows.Forms.Label();
			this._lblTLNumber = new System.Windows.Forms.Label();
			this._lblStoreName = new System.Windows.Forms.Label();
			this._lblInput2 = new System.Windows.Forms.Label();
			this._lblInput1 = new System.Windows.Forms.Label();
			this._lblLableNumber = new System.Windows.Forms.Label();
			this.txtLabelNumber = new System.Windows.Forms.TextBox();
			this._lblType = new System.Windows.Forms.Label();
			this.rdoPallet = new System.Windows.Forms.RadioButton();
			this.rdoCarton = new System.Windows.Forms.RadioButton();
			this.grpItemsStatistics = new System.Windows.Forms.GroupBox();
			this.txtStatisticsItemsPerMinute = new System.Windows.Forms.TextBox();
			this.txtStatisticsItemsPerHour = new System.Windows.Forms.TextBox();
			this.txtStatisticsTotal = new System.Windows.Forms.TextBox();
			this.txtStatisticsDeleted = new System.Windows.Forms.TextBox();
			this.txtStatisticsEntered = new System.Windows.Forms.TextBox();
			this.txtStatisticsDownTime = new System.Windows.Forms.TextBox();
			this.txtStatisticsStartTime = new System.Windows.Forms.TextBox();
			this._lblItemsMinute = new System.Windows.Forms.Label();
			this._lblItemsHour = new System.Windows.Forms.Label();
			this._lblTotal = new System.Windows.Forms.Label();
			this._lblDeleted = new System.Windows.Forms.Label();
			this._lblEntered = new System.Windows.Forms.Label();
			this._lblDownTime = new System.Windows.Forms.Label();
			this._lblStartTime = new System.Windows.Forms.Label();
			this.grpScale = new System.Windows.Forms.GroupBox();
			this.txtWeight = new System.Windows.Forms.TextBox();
			this._lblWeight = new System.Windows.Forms.Label();
			this.lsvAssignments = new System.Windows.Forms.ListView();
			this.colStation = new System.Windows.Forms.ColumnHeader();
			this.colSortType = new System.Windows.Forms.ColumnHeader();
			this.colFreightID = new System.Windows.Forms.ColumnHeader();
			this.colFreigthType = new System.Windows.Forms.ColumnHeader();
			this.colTDS = new System.Windows.Forms.ColumnHeader();
			this.colClient = new System.Windows.Forms.ColumnHeader();
			this.colClientNum = new System.Windows.Forms.ColumnHeader();
			this.colClientDiv = new System.Windows.Forms.ColumnHeader();
			this.colShipper = new System.Windows.Forms.ColumnHeader();
			this.colShipperNum = new System.Windows.Forms.ColumnHeader();
			this.colTrailerNum = new System.Windows.Forms.ColumnHeader();
			this.colPickup = new System.Windows.Forms.ColumnHeader();
			this.grpSortedItem.SuspendLayout();
			this.grpItemsStatistics.SuspendLayout();
			this.grpScale.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpSortedItem
			// 
			this.grpSortedItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grpSortedItem.Controls.Add(this.grpLine3);
			this.grpSortedItem.Controls.Add(this.grpLine2);
			this.grpSortedItem.Controls.Add(this.grpLine1);
			this.grpSortedItem.Controls.Add(this._lblInput3);
			this.grpSortedItem.Controls.Add(this.chkNonConveyable);
			this.grpSortedItem.Controls.Add(this.txtZone);
			this.grpSortedItem.Controls.Add(this.txtTLNumber);
			this.grpSortedItem.Controls.Add(this.txtDamage);
			this.grpSortedItem.Controls.Add(this.txtStoreName);
			this.grpSortedItem.Controls.Add(this.txtInputs);
			this.grpSortedItem.Controls.Add(this._lblZone);
			this.grpSortedItem.Controls.Add(this._lblDamage);
			this.grpSortedItem.Controls.Add(this._lblTLNumber);
			this.grpSortedItem.Controls.Add(this._lblStoreName);
			this.grpSortedItem.Controls.Add(this._lblInput2);
			this.grpSortedItem.Controls.Add(this._lblInput1);
			this.grpSortedItem.Controls.Add(this._lblLableNumber);
			this.grpSortedItem.Controls.Add(this.txtLabelNumber);
			this.grpSortedItem.Controls.Add(this._lblType);
			this.grpSortedItem.Controls.Add(this.rdoPallet);
			this.grpSortedItem.Controls.Add(this.rdoCarton);
			this.grpSortedItem.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grpSortedItem.ForeColor = System.Drawing.SystemColors.ControlText;
			this.grpSortedItem.Location = new System.Drawing.Point(3, 144);
			this.grpSortedItem.Name = "grpSortedItem";
			this.grpSortedItem.Size = new System.Drawing.Size(568, 387);
			this.grpSortedItem.TabIndex = 5;
			this.grpSortedItem.TabStop = false;
			this.grpSortedItem.Text = "Sorted Item";
			// 
			// grpLine3
			// 
			this.grpLine3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grpLine3.Location = new System.Drawing.Point(12, 65);
			this.grpLine3.Name = "grpLine3";
			this.grpLine3.Size = new System.Drawing.Size(540, 3);
			this.grpLine3.TabIndex = 21;
			this.grpLine3.TabStop = false;
			// 
			// grpLine2
			// 
			this.grpLine2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grpLine2.Location = new System.Drawing.Point(12, 253);
			this.grpLine2.Name = "grpLine2";
			this.grpLine2.Size = new System.Drawing.Size(540, 3);
			this.grpLine2.TabIndex = 20;
			this.grpLine2.TabStop = false;
			// 
			// grpLine1
			// 
			this.grpLine1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grpLine1.Location = new System.Drawing.Point(12, 115);
			this.grpLine1.Name = "grpLine1";
			this.grpLine1.Size = new System.Drawing.Size(540, 3);
			this.grpLine1.TabIndex = 19;
			this.grpLine1.TabStop = false;
			// 
			// _lblInput3
			// 
			this._lblInput3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblInput3.Location = new System.Drawing.Point(12, 216);
			this._lblInput3.Name = "_lblInput3";
			this._lblInput3.Size = new System.Drawing.Size(168, 24);
			this._lblInput3.TabIndex = 18;
			this._lblInput3.Text = "Purchase Order#";
			this._lblInput3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chkNonConveyable
			// 
			this.chkNonConveyable.Enabled = false;
			this.chkNonConveyable.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkNonConveyable.Location = new System.Drawing.Point(408, 27);
			this.chkNonConveyable.Name = "chkNonConveyable";
			this.chkNonConveyable.Size = new System.Drawing.Size(144, 24);
			this.chkNonConveyable.TabIndex = 15;
			this.chkNonConveyable.Text = "Non-Conveyable";
			this.chkNonConveyable.CheckedChanged += new System.EventHandler(this.OnNonConveyableChanged);
			// 
			// txtZone
			// 
			this.txtZone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtZone.AutoSize = false;
			this.txtZone.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtZone.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtZone.Location = new System.Drawing.Point(420, 310);
			this.txtZone.Name = "txtZone";
			this.txtZone.ReadOnly = true;
			this.txtZone.Size = new System.Drawing.Size(132, 24);
			this.txtZone.TabIndex = 14;
			this.txtZone.Text = "";
			// 
			// txtTLNumber
			// 
			this.txtTLNumber.AutoSize = false;
			this.txtTLNumber.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtTLNumber.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtTLNumber.Location = new System.Drawing.Point(192, 310);
			this.txtTLNumber.Name = "txtTLNumber";
			this.txtTLNumber.ReadOnly = true;
			this.txtTLNumber.Size = new System.Drawing.Size(132, 24);
			this.txtTLNumber.TabIndex = 13;
			this.txtTLNumber.Text = "";
			// 
			// txtDamage
			// 
			this.txtDamage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtDamage.AutoSize = false;
			this.txtDamage.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtDamage.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtDamage.Location = new System.Drawing.Point(192, 352);
			this.txtDamage.Name = "txtDamage";
			this.txtDamage.ReadOnly = true;
			this.txtDamage.Size = new System.Drawing.Size(360, 24);
			this.txtDamage.TabIndex = 12;
			this.txtDamage.Text = "";
			// 
			// txtStoreName
			// 
			this.txtStoreName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtStoreName.AutoSize = false;
			this.txtStoreName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtStoreName.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtStoreName.Location = new System.Drawing.Point(192, 268);
			this.txtStoreName.Name = "txtStoreName";
			this.txtStoreName.ReadOnly = true;
			this.txtStoreName.Size = new System.Drawing.Size(360, 24);
			this.txtStoreName.TabIndex = 11;
			this.txtStoreName.Text = "";
			// 
			// txtInputs
			// 
			this.txtInputs.AcceptsReturn = true;
			this.txtInputs.AcceptsTab = true;
			this.txtInputs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtInputs.AutoSize = false;
			this.txtInputs.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtInputs.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtInputs.Location = new System.Drawing.Point(192, 131);
			this.txtInputs.Multiline = true;
			this.txtInputs.Name = "txtInputs";
			this.txtInputs.Size = new System.Drawing.Size(360, 112);
			this.txtInputs.TabIndex = 9;
			this.txtInputs.Text = "";
			this.txtInputs.WordWrap = false;
			this.txtInputs.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnInputKeyUp);
			// 
			// _lblZone
			// 
			this._lblZone.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblZone.Location = new System.Drawing.Point(351, 310);
			this._lblZone.Name = "_lblZone";
			this._lblZone.Size = new System.Drawing.Size(48, 24);
			this._lblZone.TabIndex = 8;
			this._lblZone.Text = "Zone";
			this._lblZone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblDamage
			// 
			this._lblDamage.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblDamage.Location = new System.Drawing.Point(12, 352);
			this._lblDamage.Name = "_lblDamage";
			this._lblDamage.Size = new System.Drawing.Size(168, 24);
			this._lblDamage.TabIndex = 7;
			this._lblDamage.Text = "Damage";
			this._lblDamage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblTLNumber
			// 
			this._lblTLNumber.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblTLNumber.Location = new System.Drawing.Point(12, 310);
			this._lblTLNumber.Name = "_lblTLNumber";
			this._lblTLNumber.Size = new System.Drawing.Size(168, 24);
			this._lblTLNumber.TabIndex = 6;
			this._lblTLNumber.Text = "TL Number";
			this._lblTLNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblStoreName
			// 
			this._lblStoreName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblStoreName.Location = new System.Drawing.Point(12, 268);
			this._lblStoreName.Name = "_lblStoreName";
			this._lblStoreName.Size = new System.Drawing.Size(168, 24);
			this._lblStoreName.TabIndex = 5;
			this._lblStoreName.Text = "Store Name";
			this._lblStoreName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblInput2
			// 
			this._lblInput2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblInput2.Location = new System.Drawing.Point(12, 173);
			this._lblInput2.Name = "_lblInput2";
			this._lblInput2.Size = new System.Drawing.Size(168, 24);
			this._lblInput2.TabIndex = 4;
			this._lblInput2.Text = "Carton (20-Dyn)";
			this._lblInput2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblInput1
			// 
			this._lblInput1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblInput1.Location = new System.Drawing.Point(12, 131);
			this._lblInput1.Name = "_lblInput1";
			this._lblInput1.Size = new System.Drawing.Size(168, 24);
			this._lblInput1.TabIndex = 3;
			this._lblInput1.Text = "Store (16-Dyn)";
			this._lblInput1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblLableNumber
			// 
			this._lblLableNumber.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblLableNumber.Location = new System.Drawing.Point(12, 79);
			this._lblLableNumber.Name = "_lblLableNumber";
			this._lblLableNumber.Size = new System.Drawing.Size(168, 24);
			this._lblLableNumber.TabIndex = 2;
			this._lblLableNumber.Text = "Label Number";
			this._lblLableNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtLabelNumber
			// 
			this.txtLabelNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtLabelNumber.AutoSize = false;
			this.txtLabelNumber.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtLabelNumber.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtLabelNumber.Location = new System.Drawing.Point(192, 79);
			this.txtLabelNumber.Name = "txtLabelNumber";
			this.txtLabelNumber.ReadOnly = true;
			this.txtLabelNumber.Size = new System.Drawing.Size(360, 24);
			this.txtLabelNumber.TabIndex = 1;
			this.txtLabelNumber.Text = "";
			// 
			// _lblType
			// 
			this._lblType.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblType.Location = new System.Drawing.Point(12, 27);
			this._lblType.Name = "_lblType";
			this._lblType.Size = new System.Drawing.Size(120, 24);
			this._lblType.TabIndex = 0;
			this._lblType.Text = "Item Type";
			this._lblType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// rdoPallet
			// 
			this.rdoPallet.Enabled = false;
			this.rdoPallet.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rdoPallet.Location = new System.Drawing.Point(264, 27);
			this.rdoPallet.Name = "rdoPallet";
			this.rdoPallet.Size = new System.Drawing.Size(96, 24);
			this.rdoPallet.TabIndex = 1;
			this.rdoPallet.Text = "Pallet";
			this.rdoPallet.CheckedChanged += new System.EventHandler(this.OnItemTypeChanged);
			// 
			// rdoCarton
			// 
			this.rdoCarton.Checked = true;
			this.rdoCarton.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rdoCarton.Location = new System.Drawing.Point(153, 27);
			this.rdoCarton.Name = "rdoCarton";
			this.rdoCarton.Size = new System.Drawing.Size(96, 24);
			this.rdoCarton.TabIndex = 0;
			this.rdoCarton.TabStop = true;
			this.rdoCarton.Text = "Carton";
			this.rdoCarton.CheckedChanged += new System.EventHandler(this.OnItemTypeChanged);
			// 
			// grpItemsStatistics
			// 
			this.grpItemsStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grpItemsStatistics.Controls.Add(this.txtStatisticsItemsPerMinute);
			this.grpItemsStatistics.Controls.Add(this.txtStatisticsItemsPerHour);
			this.grpItemsStatistics.Controls.Add(this.txtStatisticsTotal);
			this.grpItemsStatistics.Controls.Add(this.txtStatisticsDeleted);
			this.grpItemsStatistics.Controls.Add(this.txtStatisticsEntered);
			this.grpItemsStatistics.Controls.Add(this.txtStatisticsDownTime);
			this.grpItemsStatistics.Controls.Add(this.txtStatisticsStartTime);
			this.grpItemsStatistics.Controls.Add(this._lblItemsMinute);
			this.grpItemsStatistics.Controls.Add(this._lblItemsHour);
			this.grpItemsStatistics.Controls.Add(this._lblTotal);
			this.grpItemsStatistics.Controls.Add(this._lblDeleted);
			this.grpItemsStatistics.Controls.Add(this._lblEntered);
			this.grpItemsStatistics.Controls.Add(this._lblDownTime);
			this.grpItemsStatistics.Controls.Add(this._lblStartTime);
			this.grpItemsStatistics.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grpItemsStatistics.ForeColor = System.Drawing.SystemColors.ControlText;
			this.grpItemsStatistics.Location = new System.Drawing.Point(579, 249);
			this.grpItemsStatistics.Name = "grpItemsStatistics";
			this.grpItemsStatistics.Size = new System.Drawing.Size(300, 282);
			this.grpItemsStatistics.TabIndex = 7;
			this.grpItemsStatistics.TabStop = false;
			this.grpItemsStatistics.Text = "Items Statistics";
			// 
			// txtStatisticsItemsPerMinute
			// 
			this.txtStatisticsItemsPerMinute.AutoSize = false;
			this.txtStatisticsItemsPerMinute.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtStatisticsItemsPerMinute.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtStatisticsItemsPerMinute.Location = new System.Drawing.Point(141, 243);
			this.txtStatisticsItemsPerMinute.Name = "txtStatisticsItemsPerMinute";
			this.txtStatisticsItemsPerMinute.ReadOnly = true;
			this.txtStatisticsItemsPerMinute.Size = new System.Drawing.Size(144, 24);
			this.txtStatisticsItemsPerMinute.TabIndex = 14;
			this.txtStatisticsItemsPerMinute.Text = "";
			this.txtStatisticsItemsPerMinute.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtStatisticsItemsPerHour
			// 
			this.txtStatisticsItemsPerHour.AutoSize = false;
			this.txtStatisticsItemsPerHour.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtStatisticsItemsPerHour.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtStatisticsItemsPerHour.Location = new System.Drawing.Point(141, 207);
			this.txtStatisticsItemsPerHour.Name = "txtStatisticsItemsPerHour";
			this.txtStatisticsItemsPerHour.ReadOnly = true;
			this.txtStatisticsItemsPerHour.Size = new System.Drawing.Size(144, 24);
			this.txtStatisticsItemsPerHour.TabIndex = 13;
			this.txtStatisticsItemsPerHour.Text = "";
			this.txtStatisticsItemsPerHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtStatisticsTotal
			// 
			this.txtStatisticsTotal.AutoSize = false;
			this.txtStatisticsTotal.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtStatisticsTotal.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtStatisticsTotal.Location = new System.Drawing.Point(141, 171);
			this.txtStatisticsTotal.Name = "txtStatisticsTotal";
			this.txtStatisticsTotal.ReadOnly = true;
			this.txtStatisticsTotal.Size = new System.Drawing.Size(144, 24);
			this.txtStatisticsTotal.TabIndex = 12;
			this.txtStatisticsTotal.Text = "";
			this.txtStatisticsTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtStatisticsDeleted
			// 
			this.txtStatisticsDeleted.AutoSize = false;
			this.txtStatisticsDeleted.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtStatisticsDeleted.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtStatisticsDeleted.Location = new System.Drawing.Point(141, 135);
			this.txtStatisticsDeleted.Name = "txtStatisticsDeleted";
			this.txtStatisticsDeleted.ReadOnly = true;
			this.txtStatisticsDeleted.Size = new System.Drawing.Size(144, 24);
			this.txtStatisticsDeleted.TabIndex = 11;
			this.txtStatisticsDeleted.Text = "";
			this.txtStatisticsDeleted.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtStatisticsEntered
			// 
			this.txtStatisticsEntered.AutoSize = false;
			this.txtStatisticsEntered.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtStatisticsEntered.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtStatisticsEntered.Location = new System.Drawing.Point(141, 99);
			this.txtStatisticsEntered.Name = "txtStatisticsEntered";
			this.txtStatisticsEntered.ReadOnly = true;
			this.txtStatisticsEntered.Size = new System.Drawing.Size(144, 24);
			this.txtStatisticsEntered.TabIndex = 10;
			this.txtStatisticsEntered.Text = "";
			this.txtStatisticsEntered.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtStatisticsDownTime
			// 
			this.txtStatisticsDownTime.AutoSize = false;
			this.txtStatisticsDownTime.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtStatisticsDownTime.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtStatisticsDownTime.Location = new System.Drawing.Point(141, 63);
			this.txtStatisticsDownTime.Name = "txtStatisticsDownTime";
			this.txtStatisticsDownTime.ReadOnly = true;
			this.txtStatisticsDownTime.Size = new System.Drawing.Size(144, 24);
			this.txtStatisticsDownTime.TabIndex = 9;
			this.txtStatisticsDownTime.Text = "";
			this.txtStatisticsDownTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtStatisticsStartTime
			// 
			this.txtStatisticsStartTime.AutoSize = false;
			this.txtStatisticsStartTime.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtStatisticsStartTime.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtStatisticsStartTime.Location = new System.Drawing.Point(141, 27);
			this.txtStatisticsStartTime.Name = "txtStatisticsStartTime";
			this.txtStatisticsStartTime.ReadOnly = true;
			this.txtStatisticsStartTime.Size = new System.Drawing.Size(144, 24);
			this.txtStatisticsStartTime.TabIndex = 8;
			this.txtStatisticsStartTime.Text = "";
			this.txtStatisticsStartTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// _lblItemsMinute
			// 
			this._lblItemsMinute.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblItemsMinute.Location = new System.Drawing.Point(12, 243);
			this._lblItemsMinute.Name = "_lblItemsMinute";
			this._lblItemsMinute.Size = new System.Drawing.Size(120, 24);
			this._lblItemsMinute.TabIndex = 7;
			this._lblItemsMinute.Text = "Items/Minute";
			this._lblItemsMinute.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblItemsHour
			// 
			this._lblItemsHour.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblItemsHour.Location = new System.Drawing.Point(12, 207);
			this._lblItemsHour.Name = "_lblItemsHour";
			this._lblItemsHour.Size = new System.Drawing.Size(120, 24);
			this._lblItemsHour.TabIndex = 6;
			this._lblItemsHour.Text = "Items/Hour";
			this._lblItemsHour.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblTotal
			// 
			this._lblTotal.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblTotal.Location = new System.Drawing.Point(12, 171);
			this._lblTotal.Name = "_lblTotal";
			this._lblTotal.Size = new System.Drawing.Size(120, 24);
			this._lblTotal.TabIndex = 5;
			this._lblTotal.Text = "Total";
			this._lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblDeleted
			// 
			this._lblDeleted.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblDeleted.Location = new System.Drawing.Point(12, 135);
			this._lblDeleted.Name = "_lblDeleted";
			this._lblDeleted.Size = new System.Drawing.Size(120, 24);
			this._lblDeleted.TabIndex = 4;
			this._lblDeleted.Text = "Deleted";
			this._lblDeleted.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblEntered
			// 
			this._lblEntered.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblEntered.Location = new System.Drawing.Point(12, 99);
			this._lblEntered.Name = "_lblEntered";
			this._lblEntered.Size = new System.Drawing.Size(120, 24);
			this._lblEntered.TabIndex = 3;
			this._lblEntered.Text = "Entered";
			this._lblEntered.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblDownTime
			// 
			this._lblDownTime.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblDownTime.Location = new System.Drawing.Point(12, 63);
			this._lblDownTime.Name = "_lblDownTime";
			this._lblDownTime.Size = new System.Drawing.Size(120, 24);
			this._lblDownTime.TabIndex = 2;
			this._lblDownTime.Tag = "";
			this._lblDownTime.Text = "Down Time";
			this._lblDownTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblStartTime
			// 
			this._lblStartTime.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblStartTime.Location = new System.Drawing.Point(12, 27);
			this._lblStartTime.Name = "_lblStartTime";
			this._lblStartTime.Size = new System.Drawing.Size(120, 24);
			this._lblStartTime.TabIndex = 1;
			this._lblStartTime.Text = "Start Time";
			this._lblStartTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// grpScale
			// 
			this.grpScale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.grpScale.Controls.Add(this.txtWeight);
			this.grpScale.Controls.Add(this._lblWeight);
			this.grpScale.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grpScale.ForeColor = System.Drawing.SystemColors.ControlText;
			this.grpScale.Location = new System.Drawing.Point(579, 144);
			this.grpScale.Name = "grpScale";
			this.grpScale.Size = new System.Drawing.Size(300, 96);
			this.grpScale.TabIndex = 6;
			this.grpScale.TabStop = false;
			this.grpScale.Text = "Scale";
			// 
			// txtWeight
			// 
			this.txtWeight.AutoSize = false;
			this.txtWeight.BackColor = System.Drawing.SystemColors.Highlight;
			this.txtWeight.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtWeight.ForeColor = System.Drawing.SystemColors.HighlightText;
			this.txtWeight.Location = new System.Drawing.Point(141, 30);
			this.txtWeight.MaxLength = 3;
			this.txtWeight.Name = "txtWeight";
			this.txtWeight.Size = new System.Drawing.Size(144, 48);
			this.txtWeight.TabIndex = 2;
			this.txtWeight.Text = "250";
			this.txtWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtWeight.TextChanged += new System.EventHandler(this.OnWeightEntered);
			// 
			// _lblWeight
			// 
			this._lblWeight.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblWeight.Location = new System.Drawing.Point(12, 42);
			this._lblWeight.Name = "_lblWeight";
			this._lblWeight.Size = new System.Drawing.Size(120, 24);
			this._lblWeight.TabIndex = 1;
			this._lblWeight.Text = "Weight";
			this._lblWeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lsvAssignments
			// 
			this.lsvAssignments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lsvAssignments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							 this.colStation,
																							 this.colSortType,
																							 this.colFreightID,
																							 this.colFreigthType,
																							 this.colTDS,
																							 this.colClient,
																							 this.colClientNum,
																							 this.colClientDiv,
																							 this.colShipper,
																							 this.colShipperNum,
																							 this.colTrailerNum,
																							 this.colPickup});
			this.lsvAssignments.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lsvAssignments.FullRowSelect = true;
			this.lsvAssignments.GridLines = true;
			this.lsvAssignments.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lsvAssignments.HideSelection = false;
			this.lsvAssignments.LabelWrap = false;
			this.lsvAssignments.Location = new System.Drawing.Point(6, 6);
			this.lsvAssignments.MultiSelect = false;
			this.lsvAssignments.Name = "lsvAssignments";
			this.lsvAssignments.Size = new System.Drawing.Size(870, 135);
			this.lsvAssignments.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lsvAssignments.TabIndex = 8;
			this.lsvAssignments.View = System.Windows.Forms.View.Details;
			// 
			// colStation
			// 
			this.colStation.Text = "Station#";
			// 
			// colSortType
			// 
			this.colSortType.Text = "Sort Type";
			this.colSortType.Width = 72;
			// 
			// colFreightID
			// 
			this.colFreightID.Text = "FreightID";
			this.colFreightID.Width = 96;
			// 
			// colFreigthType
			// 
			this.colFreigthType.Text = "Freigth Type";
			this.colFreigthType.Width = 72;
			// 
			// colTDS
			// 
			this.colTDS.Text = "TDS#";
			this.colTDS.Width = 72;
			// 
			// colClient
			// 
			this.colClient.Text = "Client";
			this.colClient.Width = 144;
			// 
			// colClientNum
			// 
			this.colClientNum.Text = "Client#";
			this.colClientNum.Width = 48;
			// 
			// colClientDiv
			// 
			this.colClientDiv.Text = "Client Div";
			// 
			// colShipper
			// 
			this.colShipper.Text = "Shipper";
			this.colShipper.Width = 120;
			// 
			// colShipperNum
			// 
			this.colShipperNum.Text = "Shipper#";
			// 
			// colTrailerNum
			// 
			this.colTrailerNum.Text = "Trailer#";
			// 
			// colPickup
			// 
			this.colPickup.Text = "Pickup";
			// 
			// StationUI
			// 
			this.Controls.Add(this.lsvAssignments);
			this.Controls.Add(this.grpSortedItem);
			this.Controls.Add(this.grpItemsStatistics);
			this.Controls.Add(this.grpScale);
			this.Name = "StationUI";
			this.Size = new System.Drawing.Size(885, 537);
			this.Resize += new System.EventHandler(this.OnControlResize);
			this.Load += new System.EventHandler(this.OnControlLoad);
			this.grpSortedItem.ResumeLayout(false);
			this.grpItemsStatistics.ResumeLayout(false);
			this.grpScale.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		
		private void OnControlLoad(object sender, System.EventArgs e) {
			//Event handler for control load event
			try { 
				this.grpScale.Enabled = this.grpSortedItem.Enabled = this.grpItemsStatistics.Enabled = false;
				this.rdoCarton.Checked = true;
				OnPrinterChanged(null, null);
				OnScaleChanged(null, null);
			} 
			catch(Exception ex) { reportError(ex); } 
			finally { setServices(); }
		}
		private void OnControlResize(object sender, System.EventArgs e) {
			//Event handler for change in control size
		}
		private void OnItemTypeChanged(object sender, System.EventArgs e) {
			//Event handler for change in item type (carton, pallet)
		}
		private void OnNonConveyableChanged(object sender, System.EventArgs e) {
			//Event handler for change in non-conveyable selection
		}
		#region Station Assignments: OnAssignmentsChanged()
		private void OnAssignmentsChanged(object sender, EventArgs e) {
			//Event handler for change in station assignments
			try {
				//Clear and re-populate the listview of assignments
				this.lsvAssignments.Items.Clear();
				for(int i=0; i<this.mOperator.Assignments.Count; i++) {
					string station = this.mOperator.Station.Number;
					string sortType = this.mOperator.Assignments.Item(i).SortProfile.SortType;
					string freightID = this.mOperator.Assignments.Item(i).InboundFreight.FreightID;
					string freight = this.mOperator.Assignments.Item(i).InboundFreight.FreightType;
					string tds = this.mOperator.Assignments.Item(i).InboundFreight.TDSNumber.ToString();
					string client = this.mOperator.Assignments.Item(i).InboundFreight.Client.Name.Trim();
					string clientNum = this.mOperator.Assignments.Item(i).InboundFreight.Client.Number.Trim();
					string clientDiv = this.mOperator.Assignments.Item(i).InboundFreight.Client.Division.Trim();
					string shipper= this.mOperator.Assignments.Item(i).InboundFreight.Shipper.NAME.Trim();
					string shipperNum = this.mOperator.Assignments.Item(i).InboundFreight.Shipper.NUMBER;
					string trailer = this.mOperator.Assignments.Item(i).InboundFreight.TrailerNumber.Trim();
					string pickup = this.mOperator.Assignments.Item(i).InboundFreight.PickupDate.Trim() + "-" + this.mOperator.Assignments.Item(i).InboundFreight.PickupNumber;
					this.lsvAssignments.Items.Add(new ListViewItem(new string[]{station,sortType,freightID,freight,tds,client,clientNum,clientDiv,shipper,shipperNum,trailer,pickup}));
				}
			}
			catch(Exception ex) { reportError(ex); }
			finally { setServices(); }
		}
		#endregion
		#region Operator: OnSortedItemCreated(), OnSortedItemComplete()
		private void OnSortedItemCreated(object sender, SortedItemEventArgs e) {
			//Event handler for sorted item created
			try {
				if(this.mOperator.Assignments.Count == 0) {
					//Clear
					this.txtInputs.Text = "";
					this.txtLabelNumber.Text = this.txtStoreName.Text = "";
					this.txtTLNumber.Text = this.txtZone.Text = "";
					this.txtDamage.Text = this.txtWeight.Text = "";
//					this.label1.Text = "";
				}
				else {
					//Capture sorted item details
					SortedItem item = e.SortedItem;
					if(item != null) {
						if(item.InboundLabel != null) {
							InboundLabel label = item.InboundLabel;
							this.txtInputs.Text = label.Inputs[0].InputData;
						}
					
						this.txtLabelNumber.Text = item.LabelNumber;
						this.txtStoreName.Text = item.DestinationRouting != null ? item.DestinationRouting.DestinationName : "";
						this.txtTLNumber.Text = item.DestinationRouting != null ? item.DestinationRouting.ZoneTL : "";
						this.txtZone.Text = item.DestinationRouting != null ? item.DestinationRouting.ZoneCode : "";
						this.txtDamage.Text = item.DamageCode;
						this.txtWeight.Text = item.Weight.ToString();
					}
//					this.label1.Text = this.mOperator.SortedItemCount.ToString();
				}
			}
			catch(Exception ex) { reportError(ex); }
			finally { setServices(); }
		}
		private void OnSortedItemComplete(object sender, SortedItemEventArgs e) {
			//Event handler for sorted item completed processing
			try {
				if(this.mOperator.Assignments.Count == 0) {
					//Clear
					this.txtStatisticsStartTime.Text = this.txtStatisticsDownTime.Text = "";
				}
				else {				
					//Update statistics display controls
					this.txtStatisticsStartTime.Text = this.mOperator.Station.SortStatistics.StartTime.ToShortTimeString();
					this.txtStatisticsDownTime.Text = this.mOperator.Station.SortStatistics.DownTimeMinutes.ToString();
					this.txtStatisticsEntered.Text = this.mOperator.Station.SortStatistics.CartonsSorted.ToString();
					this.txtStatisticsDeleted.Text = this.mOperator.Station.SortStatistics.CartonsDeleted.ToString();
					this.txtStatisticsTotal.Text = this.mOperator.Station.SortStatistics.CartonsTotal.ToString();
					this.txtStatisticsItemsPerHour.Text = this.mOperator.Station.SortStatistics.ItemsPerHour.ToString();
					this.txtStatisticsItemsPerMinute.Text = this.mOperator.Station.SortStatistics.ItemsPerMinute.ToString();
				}
			}
			catch(Exception ex) { reportError(ex); }
			finally { setServices(); }
		}
		#endregion
		#region IB Label: OnInputKeyUp()
		private void OnInputKeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			//Event handler for all inputs
//			TextBox txt=null;
			this.Cursor = Cursors.WaitCursor;
			try {
				//Process input scan
//				txt = (TextBox)sender;
//				if(txt.Text.Length >= txt.MaxLength) {
////					this.mMsgMgr.AddMessage("Processing input...");
//					switch(txt.Name) {
//						case "txtInputs":	
//							ArgixTrace.WriteLine(new TraceMessage("Processing first input...",AppLib.EVENTLOGNAME,LogLevel.Debug));
//							this.mOperator.ProcessInput1(txt.Text); 
//							break;
//						default:			
//							ArgixTrace.WriteLine(new TraceMessage("Processing next input...",AppLib.EVENTLOGNAME,LogLevel.Debug));
//							this.mOperator.ProcessInput(txt.Text); 
//							break;
//					}
//				}
			}
            //catch(WorkflowException ex) { reportError(ex); }
			catch(Exception ex) { 
				ArgixTrace.WriteLine(new TraceMessage(ex.ToString(), AppLib.EVENTLOGNAME, LogLevel.Error));
				reportError(ex); 
			}
			finally { this.Cursor = Cursors.Default; }
		}
		#endregion
		#region Devices: OnPrinterChanged(), OnScaleChanged(), OnScaleWeightChanged(), OnWeightEntered()
		private void OnPrinterChanged(object sender, EventArgs e) {
			//Event handler for change to the active printer
			try { 
				if(this.mOperator.Station.Printer != null) {
					//Configure for a new printer type
					ArgixTrace.WriteLine(new TraceMessage("Printer type= " + this.mOperator.Station.Printer.Type,AppLib.EVENTLOGNAME,LogLevel.Information));
				}
				else {
					//Disable printer features
					ArgixTrace.WriteLine(new TraceMessage("Printer type= null" + this.mOperator.Station.Scale.Type,AppLib.EVENTLOGNAME,LogLevel.Information));
				}
			}
			catch(Exception ex) { reportError(ex); }
		}
		private void OnScaleChanged(object sender, EventArgs e) {
			//Event handler for change in the active scale
			try { 
				if(this.mOperator.Station.Scale != null) {
					//Configure for a new scale type
					this.grpScale.Text = this.mOperator.Station.Scale.Type + " Scale";
					this.grpScale.Enabled = true;
					if(this.mOperator.Station.Scale.Type.ToLower() == "manual") {
						this.txtWeight.ReadOnly = false;
						this.txtWeight.BackColor = System.Drawing.SystemColors.Window;
						this.txtWeight.ForeColor = System.Drawing.SystemColors.WindowText;
						this.txtWeight.Text = "0";
					}
					else {
						this.txtWeight.ReadOnly = true;
						this.txtWeight.BackColor = System.Drawing.SystemColors.Highlight;
						this.txtWeight.ForeColor = System.Drawing.SystemColors.HighlightText;
					}
					this.mOperator.Station.Scale.ScaleWeightReading += new ScaleEventHandler(this.OnScaleWeightChanged);
					ArgixTrace.WriteLine(new TraceMessage("Scale type= " + this.mOperator.Station.Scale.Type,AppLib.EVENTLOGNAME,LogLevel.Debug));
				}
				else {
					//Disable scale features
					this.grpScale.Text = "";
					this.grpScale.Enabled = false;
					this.txtWeight.ReadOnly = true;
					this.txtWeight.BackColor = System.Drawing.SystemColors.Highlight;
					this.txtWeight.ForeColor = System.Drawing.SystemColors.HighlightText;
					ArgixTrace.WriteLine(new TraceMessage("Scale type= null",AppLib.EVENTLOGNAME,LogLevel.Debug));
				}
			}
			catch(Exception ex) { reportError(ex); }
		}
		private void OnScaleWeightChanged(object sender, ScaleEventArgs e) {
			//Event handler for change in scale weight
			try { 
				switch(e.Error) {
					case ScaleError.None: 
						this.txtWeight.Text = e.Weight.ToString();
						ArgixTrace.WriteLine(new TraceMessage("Scale weight=" + e.Weight.ToString(),AppLib.EVENTLOGNAME,LogLevel.Debug));
						break;
					case ScaleError.ScaleUnstable: 
						this.txtWeight.Text = "U";
						ArgixTrace.WriteLine(new TraceMessage("Scale unstable error",AppLib.EVENTLOGNAME,LogLevel.Debug));
						break;
					case ScaleError.ScaleStatus: 
						ArgixTrace.WriteLine(new TraceMessage("Scale status error",AppLib.EVENTLOGNAME,LogLevel.Debug));
						break;
					case ScaleError.RS232: 
						ArgixTrace.WriteLine(new TraceMessage("Scale RS232 error",AppLib.EVENTLOGNAME,LogLevel.Debug));
						break;
					default: this.txtWeight.Text = ""; break;
				}
			}
			catch(Exception ex) { reportError(ex); }
		}
		private void OnWeightEntered(object sender, System.EventArgs e) {
			//Event handler for user entered weight with the manual scale
			try { 
				if(this.mOperator.Station.Scale != null) {
					decimal iWeight=this.mOperator.Station.Scale.Weight;
					try {
						iWeight = Convert.ToDecimal(this.txtWeight.Text);
					}
					catch(Exception ex) { reportError(ex); }
					this.mOperator.Station.Scale.Weight = iWeight;
				}
			}
			catch(Exception ex) { reportError(ex); }
		}
		#endregion
		#region Statistics: OnStatisticsChanged()
		private void OnStatisticsChanged(object sender, System.EventArgs e) {
			//Event handler for change in statistics
			try {
				//Update statistics display controls
				this.txtStatisticsStartTime.Text = this.mOperator.Station.SortStatistics.StartTime.ToShortTimeString();
				this.txtStatisticsDownTime.Text = this.mOperator.Station.SortStatistics.DownTimeMinutes.ToString();
				this.txtStatisticsEntered.Text = this.mOperator.Station.SortStatistics.CartonsSorted.ToString();
				this.txtStatisticsDeleted.Text = this.mOperator.Station.SortStatistics.CartonsDeleted.ToString();
				this.txtStatisticsTotal.Text = this.mOperator.Station.SortStatistics.CartonsTotal.ToString();
				this.txtStatisticsItemsPerHour.Text = this.mOperator.Station.SortStatistics.ItemsPerHour.ToString();
				this.txtStatisticsItemsPerMinute.Text = this.mOperator.Station.SortStatistics.ItemsPerMinute.ToString();
			}
			catch(Exception ex) { reportError(ex); }
		}
		#endregion
		#region Local services: setServices(), reportError()
		private void setServices() {
			//Set user services states
			try {
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
