//	File:	dlglayout.cs
//	Author:	J. Heary
//	Date:	10/13/05
//	Desc:	Dialog to view/edit a schedule layout.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Argix.Dispatch {
	//
	public class dlgLayout : System.Windows.Forms.Form {
		//Members
		LayoutView mLayout=null;
		LayoutDS mLayoutDS=null;
		
		//Constants
		private const string NONE = "(none)";
		
		#region Controls
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabShowFields;
		private System.Windows.Forms.TabPage tabGroupBy;
		private System.Windows.Forms.TabPage tabSort;
		private System.Windows.Forms.TabPage tabFormatColumns;
		private System.Windows.Forms.Label _lblAvailableFields;
		private System.Windows.Forms.Label _lblSelectedFields;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Button btnMoveDown;
		private System.Windows.Forms.Button btnMoveUp;
		private System.Windows.Forms.GroupBox grpGroupBy3;
		private System.Windows.Forms.ComboBox cboGroupByField3;
		private System.Windows.Forms.CheckBox chkShowInView3;
		private System.Windows.Forms.RadioButton rdoGroupByDesc3;
		private System.Windows.Forms.RadioButton rdoGroupByAsc3;
		private System.Windows.Forms.GroupBox grpGroupBy2;
		private System.Windows.Forms.ComboBox cboGroupByField2;
		private System.Windows.Forms.CheckBox chkShowInView2;
		private System.Windows.Forms.RadioButton rdoGroupByDesc2;
		private System.Windows.Forms.RadioButton rdoGroupByAsc2;
		private System.Windows.Forms.GroupBox grpGroupBy1;
		private System.Windows.Forms.ComboBox cboGroupByField1;
		private System.Windows.Forms.CheckBox chkShowInView1;
		private System.Windows.Forms.RadioButton rdoGroupByDesc1;
		private System.Windows.Forms.RadioButton rdoGroupByAsc1;
		private System.Windows.Forms.CheckBox chkAutomaticallyGroup;
		private System.Windows.Forms.Button btnClearAllGroups;
		private System.Windows.Forms.GroupBox grpSortBy3;
		private System.Windows.Forms.ComboBox cboSortByField3;
		private System.Windows.Forms.RadioButton rdoSortByDesc3;
		private System.Windows.Forms.RadioButton rdoSortByAsc3;
		private System.Windows.Forms.GroupBox grpSortBy2;
		private System.Windows.Forms.ComboBox cboSortByField2;
		private System.Windows.Forms.RadioButton rdoSortByDesc2;
		private System.Windows.Forms.RadioButton rdoSortByAsc2;
		private System.Windows.Forms.GroupBox grpSortBy1;
		private System.Windows.Forms.ComboBox cboSortByField1;
		private System.Windows.Forms.RadioButton rdoSortByDesc1;
		private System.Windows.Forms.RadioButton rdoSortByAsc1;
		private System.Windows.Forms.Button btnClearAllSorts;
		private System.Windows.Forms.GroupBox grpFormat;
		private System.Windows.Forms.ListBox lstAvailableFormatFields;
		private System.Windows.Forms.Label _lblAvailableFormatFields;
		private System.Windows.Forms.ListBox lstSelectedShowFields;
		private System.Windows.Forms.ListBox lstAvailableShowFields;
		private System.Windows.Forms.Label _lblFormat;
		private System.Windows.Forms.Label _lblLabel;
		private System.Windows.Forms.RadioButton rdoAlignLeft;
		private System.Windows.Forms.RadioButton rdoWidthSpecific;
		private System.Windows.Forms.RadioButton rdoWidthBestFit;
		private System.Windows.Forms.Label _lblWidthUnits;
		private System.Windows.Forms.RadioButton rdoAlignCenter;
		private System.Windows.Forms.RadioButton rdoAlignRight;
		private System.Windows.Forms.TextBox txtLabel;
		private System.Windows.Forms.TextBox txtWidthSpecific;
		private System.Windows.Forms.GroupBox grpAlignment;
		private System.Windows.Forms.GroupBox grpWidth;
		private System.Windows.Forms.TextBox txtFormat;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		
		//Interface
		public dlgLayout(LayoutView view) {
			//Required for Windows Form Designer support
			try {
				InitializeComponent();
				this.mLayout = view;
				this.mLayoutDS = new LayoutDS();
				this.Text = this.mLayout.ScheduleName + " (" + this.mLayout.ViewName + ")";
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
		public LayoutView ScheduleLayout { get { return this.mLayout; } }
		public LayoutDS LayoutData { get { return this.mLayoutDS; } }
		
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgLayout));
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabShowFields = new System.Windows.Forms.TabPage();
			this.btnMoveDown = new System.Windows.Forms.Button();
			this.btnMoveUp = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this._lblSelectedFields = new System.Windows.Forms.Label();
			this.lstSelectedShowFields = new System.Windows.Forms.ListBox();
			this.lstAvailableShowFields = new System.Windows.Forms.ListBox();
			this._lblAvailableFields = new System.Windows.Forms.Label();
			this.tabGroupBy = new System.Windows.Forms.TabPage();
			this.grpGroupBy3 = new System.Windows.Forms.GroupBox();
			this.cboGroupByField3 = new System.Windows.Forms.ComboBox();
			this.chkShowInView3 = new System.Windows.Forms.CheckBox();
			this.rdoGroupByDesc3 = new System.Windows.Forms.RadioButton();
			this.rdoGroupByAsc3 = new System.Windows.Forms.RadioButton();
			this.grpGroupBy2 = new System.Windows.Forms.GroupBox();
			this.cboGroupByField2 = new System.Windows.Forms.ComboBox();
			this.chkShowInView2 = new System.Windows.Forms.CheckBox();
			this.rdoGroupByDesc2 = new System.Windows.Forms.RadioButton();
			this.rdoGroupByAsc2 = new System.Windows.Forms.RadioButton();
			this.grpGroupBy1 = new System.Windows.Forms.GroupBox();
			this.cboGroupByField1 = new System.Windows.Forms.ComboBox();
			this.chkShowInView1 = new System.Windows.Forms.CheckBox();
			this.rdoGroupByDesc1 = new System.Windows.Forms.RadioButton();
			this.rdoGroupByAsc1 = new System.Windows.Forms.RadioButton();
			this.chkAutomaticallyGroup = new System.Windows.Forms.CheckBox();
			this.btnClearAllGroups = new System.Windows.Forms.Button();
			this.tabSort = new System.Windows.Forms.TabPage();
			this.grpSortBy3 = new System.Windows.Forms.GroupBox();
			this.cboSortByField3 = new System.Windows.Forms.ComboBox();
			this.rdoSortByDesc3 = new System.Windows.Forms.RadioButton();
			this.rdoSortByAsc3 = new System.Windows.Forms.RadioButton();
			this.grpSortBy2 = new System.Windows.Forms.GroupBox();
			this.cboSortByField2 = new System.Windows.Forms.ComboBox();
			this.rdoSortByDesc2 = new System.Windows.Forms.RadioButton();
			this.rdoSortByAsc2 = new System.Windows.Forms.RadioButton();
			this.grpSortBy1 = new System.Windows.Forms.GroupBox();
			this.cboSortByField1 = new System.Windows.Forms.ComboBox();
			this.rdoSortByDesc1 = new System.Windows.Forms.RadioButton();
			this.rdoSortByAsc1 = new System.Windows.Forms.RadioButton();
			this.btnClearAllSorts = new System.Windows.Forms.Button();
			this.tabFormatColumns = new System.Windows.Forms.TabPage();
			this.grpFormat = new System.Windows.Forms.GroupBox();
			this.txtFormat = new System.Windows.Forms.TextBox();
			this.grpWidth = new System.Windows.Forms.GroupBox();
			this._lblWidthUnits = new System.Windows.Forms.Label();
			this.rdoWidthBestFit = new System.Windows.Forms.RadioButton();
			this.txtWidthSpecific = new System.Windows.Forms.TextBox();
			this.rdoWidthSpecific = new System.Windows.Forms.RadioButton();
			this.grpAlignment = new System.Windows.Forms.GroupBox();
			this.rdoAlignLeft = new System.Windows.Forms.RadioButton();
			this.rdoAlignRight = new System.Windows.Forms.RadioButton();
			this.rdoAlignCenter = new System.Windows.Forms.RadioButton();
			this.txtLabel = new System.Windows.Forms.TextBox();
			this._lblLabel = new System.Windows.Forms.Label();
			this._lblFormat = new System.Windows.Forms.Label();
			this.lstAvailableFormatFields = new System.Windows.Forms.ListBox();
			this._lblAvailableFormatFields = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.tabShowFields.SuspendLayout();
			this.tabGroupBy.SuspendLayout();
			this.grpGroupBy3.SuspendLayout();
			this.grpGroupBy2.SuspendLayout();
			this.grpGroupBy1.SuspendLayout();
			this.tabSort.SuspendLayout();
			this.grpSortBy3.SuspendLayout();
			this.grpSortBy2.SuspendLayout();
			this.grpSortBy1.SuspendLayout();
			this.tabFormatColumns.SuspendLayout();
			this.grpFormat.SuspendLayout();
			this.grpWidth.SuspendLayout();
			this.grpAlignment.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(375, 324);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(96, 24);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.OnButtonClick);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(273, 324);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(96, 24);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "O&K";
			this.btnOK.Click += new System.EventHandler(this.OnButtonClick);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabShowFields);
			this.tabControl1.Controls.Add(this.tabGroupBy);
			this.tabControl1.Controls.Add(this.tabSort);
			this.tabControl1.Controls.Add(this.tabFormatColumns);
			this.tabControl1.Location = new System.Drawing.Point(6, 3);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(462, 315);
			this.tabControl1.TabIndex = 3;
			// 
			// tabShowFields
			// 
			this.tabShowFields.Controls.Add(this.btnMoveDown);
			this.tabShowFields.Controls.Add(this.btnMoveUp);
			this.tabShowFields.Controls.Add(this.btnRemove);
			this.tabShowFields.Controls.Add(this.btnAdd);
			this.tabShowFields.Controls.Add(this._lblSelectedFields);
			this.tabShowFields.Controls.Add(this.lstSelectedShowFields);
			this.tabShowFields.Controls.Add(this.lstAvailableShowFields);
			this.tabShowFields.Controls.Add(this._lblAvailableFields);
			this.tabShowFields.Location = new System.Drawing.Point(4, 22);
			this.tabShowFields.Name = "tabShowFields";
			this.tabShowFields.Size = new System.Drawing.Size(454, 289);
			this.tabShowFields.TabIndex = 0;
			this.tabShowFields.Text = "Show Fields";
			// 
			// btnMoveDown
			// 
			this.btnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMoveDown.Location = new System.Drawing.Point(375, 252);
			this.btnMoveDown.Name = "btnMoveDown";
			this.btnMoveDown.Size = new System.Drawing.Size(72, 24);
			this.btnMoveDown.TabIndex = 9;
			this.btnMoveDown.Text = "Move Down";
			this.btnMoveDown.Click += new System.EventHandler(this.OnMoveFieldDown);
			// 
			// btnMoveUp
			// 
			this.btnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMoveUp.Location = new System.Drawing.Point(279, 252);
			this.btnMoveUp.Name = "btnMoveUp";
			this.btnMoveUp.Size = new System.Drawing.Size(72, 24);
			this.btnMoveUp.TabIndex = 8;
			this.btnMoveUp.Text = "Move Up";
			this.btnMoveUp.Click += new System.EventHandler(this.OnMoveFieldUp);
			// 
			// btnRemove
			// 
			this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemove.Location = new System.Drawing.Point(192, 138);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(72, 24);
			this.btnRemove.TabIndex = 7;
			this.btnRemove.Text = "<- Remove";
			this.btnRemove.Click += new System.EventHandler(this.OnRemoveField);
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.Location = new System.Drawing.Point(192, 105);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(72, 24);
			this.btnAdd.TabIndex = 6;
			this.btnAdd.Text = "Add ->";
			this.btnAdd.Click += new System.EventHandler(this.OnAddField);
			// 
			// _lblSelectedFields
			// 
			this._lblSelectedFields.Location = new System.Drawing.Point(279, 12);
			this._lblSelectedFields.Name = "_lblSelectedFields";
			this._lblSelectedFields.Size = new System.Drawing.Size(168, 18);
			this._lblSelectedFields.TabIndex = 5;
			this._lblSelectedFields.Text = "Show these fields in this order:";
			this._lblSelectedFields.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lstSelectedShowFields
			// 
			this.lstSelectedShowFields.Location = new System.Drawing.Point(279, 33);
			this.lstSelectedShowFields.Name = "lstSelectedShowFields";
			this.lstSelectedShowFields.ScrollAlwaysVisible = true;
			this.lstSelectedShowFields.Size = new System.Drawing.Size(168, 212);
			this.lstSelectedShowFields.TabIndex = 4;
			this.lstSelectedShowFields.SelectedIndexChanged += new System.EventHandler(this.OnSelectedFieldSelected);
			// 
			// lstAvailableShowFields
			// 
			this.lstAvailableShowFields.Location = new System.Drawing.Point(6, 33);
			this.lstAvailableShowFields.Name = "lstAvailableShowFields";
			this.lstAvailableShowFields.ScrollAlwaysVisible = true;
			this.lstAvailableShowFields.Size = new System.Drawing.Size(168, 212);
			this.lstAvailableShowFields.TabIndex = 3;
			this.lstAvailableShowFields.SelectedIndexChanged += new System.EventHandler(this.OnAvailableFieldSelected);
			// 
			// _lblAvailableFields
			// 
			this._lblAvailableFields.Location = new System.Drawing.Point(6, 12);
			this._lblAvailableFields.Name = "_lblAvailableFields";
			this._lblAvailableFields.Size = new System.Drawing.Size(168, 18);
			this._lblAvailableFields.TabIndex = 2;
			this._lblAvailableFields.Text = "Available fields:";
			this._lblAvailableFields.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tabGroupBy
			// 
			this.tabGroupBy.Controls.Add(this.grpGroupBy3);
			this.tabGroupBy.Controls.Add(this.grpGroupBy2);
			this.tabGroupBy.Controls.Add(this.grpGroupBy1);
			this.tabGroupBy.Controls.Add(this.chkAutomaticallyGroup);
			this.tabGroupBy.Controls.Add(this.btnClearAllGroups);
			this.tabGroupBy.Location = new System.Drawing.Point(4, 22);
			this.tabGroupBy.Name = "tabGroupBy";
			this.tabGroupBy.Size = new System.Drawing.Size(454, 289);
			this.tabGroupBy.TabIndex = 1;
			this.tabGroupBy.Text = "Group By";
			// 
			// grpGroupBy3
			// 
			this.grpGroupBy3.Controls.Add(this.cboGroupByField3);
			this.grpGroupBy3.Controls.Add(this.chkShowInView3);
			this.grpGroupBy3.Controls.Add(this.rdoGroupByDesc3);
			this.grpGroupBy3.Controls.Add(this.rdoGroupByAsc3);
			this.grpGroupBy3.Location = new System.Drawing.Point(6, 186);
			this.grpGroupBy3.Name = "grpGroupBy3";
			this.grpGroupBy3.Size = new System.Drawing.Size(327, 72);
			this.grpGroupBy3.TabIndex = 8;
			this.grpGroupBy3.TabStop = false;
			this.grpGroupBy3.Text = "Then By";
			// 
			// cboGroupByField3
			// 
			this.cboGroupByField3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboGroupByField3.Location = new System.Drawing.Point(6, 21);
			this.cboGroupByField3.Name = "cboGroupByField3";
			this.cboGroupByField3.Size = new System.Drawing.Size(195, 21);
			this.cboGroupByField3.TabIndex = 7;
			this.cboGroupByField3.SelectionChangeCommitted += new System.EventHandler(this.OnGroupByField3Selected);
			// 
			// chkShowInView3
			// 
			this.chkShowInView3.Location = new System.Drawing.Point(6, 48);
			this.chkShowInView3.Name = "chkShowInView3";
			this.chkShowInView3.Size = new System.Drawing.Size(144, 18);
			this.chkShowInView3.TabIndex = 6;
			this.chkShowInView3.Text = "Show field in view";
			// 
			// rdoGroupByDesc3
			// 
			this.rdoGroupByDesc3.Location = new System.Drawing.Point(219, 48);
			this.rdoGroupByDesc3.Name = "rdoGroupByDesc3";
			this.rdoGroupByDesc3.Size = new System.Drawing.Size(96, 18);
			this.rdoGroupByDesc3.TabIndex = 1;
			this.rdoGroupByDesc3.Text = "Descending";
			this.rdoGroupByDesc3.CheckedChanged += new System.EventHandler(this.OnSortDirectionChanged);
			// 
			// rdoGroupByAsc3
			// 
			this.rdoGroupByAsc3.Location = new System.Drawing.Point(219, 21);
			this.rdoGroupByAsc3.Name = "rdoGroupByAsc3";
			this.rdoGroupByAsc3.Size = new System.Drawing.Size(96, 18);
			this.rdoGroupByAsc3.TabIndex = 0;
			this.rdoGroupByAsc3.Text = "Ascending";
			this.rdoGroupByAsc3.CheckedChanged += new System.EventHandler(this.OnSortDirectionChanged);
			// 
			// grpGroupBy2
			// 
			this.grpGroupBy2.Controls.Add(this.cboGroupByField2);
			this.grpGroupBy2.Controls.Add(this.chkShowInView2);
			this.grpGroupBy2.Controls.Add(this.rdoGroupByDesc2);
			this.grpGroupBy2.Controls.Add(this.rdoGroupByAsc2);
			this.grpGroupBy2.Location = new System.Drawing.Point(6, 108);
			this.grpGroupBy2.Name = "grpGroupBy2";
			this.grpGroupBy2.Size = new System.Drawing.Size(327, 72);
			this.grpGroupBy2.TabIndex = 7;
			this.grpGroupBy2.TabStop = false;
			this.grpGroupBy2.Text = "Then By";
			// 
			// cboGroupByField2
			// 
			this.cboGroupByField2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboGroupByField2.Location = new System.Drawing.Point(6, 21);
			this.cboGroupByField2.Name = "cboGroupByField2";
			this.cboGroupByField2.Size = new System.Drawing.Size(195, 21);
			this.cboGroupByField2.TabIndex = 7;
			this.cboGroupByField2.SelectionChangeCommitted += new System.EventHandler(this.OnGroupByField2Selected);
			// 
			// chkShowInView2
			// 
			this.chkShowInView2.Location = new System.Drawing.Point(6, 48);
			this.chkShowInView2.Name = "chkShowInView2";
			this.chkShowInView2.Size = new System.Drawing.Size(144, 18);
			this.chkShowInView2.TabIndex = 6;
			this.chkShowInView2.Text = "Show field in view";
			// 
			// rdoGroupByDesc2
			// 
			this.rdoGroupByDesc2.Location = new System.Drawing.Point(219, 48);
			this.rdoGroupByDesc2.Name = "rdoGroupByDesc2";
			this.rdoGroupByDesc2.Size = new System.Drawing.Size(96, 18);
			this.rdoGroupByDesc2.TabIndex = 1;
			this.rdoGroupByDesc2.Text = "Descending";
			this.rdoGroupByDesc2.CheckedChanged += new System.EventHandler(this.OnSortDirectionChanged);
			// 
			// rdoGroupByAsc2
			// 
			this.rdoGroupByAsc2.Location = new System.Drawing.Point(219, 21);
			this.rdoGroupByAsc2.Name = "rdoGroupByAsc2";
			this.rdoGroupByAsc2.Size = new System.Drawing.Size(96, 18);
			this.rdoGroupByAsc2.TabIndex = 0;
			this.rdoGroupByAsc2.Text = "Ascending";
			this.rdoGroupByAsc2.CheckedChanged += new System.EventHandler(this.OnSortDirectionChanged);
			// 
			// grpGroupBy1
			// 
			this.grpGroupBy1.Controls.Add(this.cboGroupByField1);
			this.grpGroupBy1.Controls.Add(this.chkShowInView1);
			this.grpGroupBy1.Controls.Add(this.rdoGroupByDesc1);
			this.grpGroupBy1.Controls.Add(this.rdoGroupByAsc1);
			this.grpGroupBy1.Location = new System.Drawing.Point(6, 30);
			this.grpGroupBy1.Name = "grpGroupBy1";
			this.grpGroupBy1.Size = new System.Drawing.Size(327, 72);
			this.grpGroupBy1.TabIndex = 6;
			this.grpGroupBy1.TabStop = false;
			this.grpGroupBy1.Text = "Group Items By";
			// 
			// cboGroupByField1
			// 
			this.cboGroupByField1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboGroupByField1.Location = new System.Drawing.Point(6, 21);
			this.cboGroupByField1.Name = "cboGroupByField1";
			this.cboGroupByField1.Size = new System.Drawing.Size(195, 21);
			this.cboGroupByField1.TabIndex = 7;
			this.cboGroupByField1.SelectionChangeCommitted += new System.EventHandler(this.OnGroupByField1Selected);
			// 
			// chkShowInView1
			// 
			this.chkShowInView1.Location = new System.Drawing.Point(6, 48);
			this.chkShowInView1.Name = "chkShowInView1";
			this.chkShowInView1.Size = new System.Drawing.Size(144, 18);
			this.chkShowInView1.TabIndex = 6;
			this.chkShowInView1.Text = "Show field in view";
			// 
			// rdoGroupByDesc1
			// 
			this.rdoGroupByDesc1.Location = new System.Drawing.Point(219, 48);
			this.rdoGroupByDesc1.Name = "rdoGroupByDesc1";
			this.rdoGroupByDesc1.Size = new System.Drawing.Size(96, 18);
			this.rdoGroupByDesc1.TabIndex = 1;
			this.rdoGroupByDesc1.Text = "Descending";
			this.rdoGroupByDesc1.CheckedChanged += new System.EventHandler(this.OnSortDirectionChanged);
			// 
			// rdoGroupByAsc1
			// 
			this.rdoGroupByAsc1.Location = new System.Drawing.Point(219, 21);
			this.rdoGroupByAsc1.Name = "rdoGroupByAsc1";
			this.rdoGroupByAsc1.Size = new System.Drawing.Size(96, 18);
			this.rdoGroupByAsc1.TabIndex = 0;
			this.rdoGroupByAsc1.Text = "Ascending";
			this.rdoGroupByAsc1.CheckedChanged += new System.EventHandler(this.OnSortDirectionChanged);
			// 
			// chkAutomaticallyGroup
			// 
			this.chkAutomaticallyGroup.Location = new System.Drawing.Point(6, 6);
			this.chkAutomaticallyGroup.Name = "chkAutomaticallyGroup";
			this.chkAutomaticallyGroup.Size = new System.Drawing.Size(288, 18);
			this.chkAutomaticallyGroup.TabIndex = 5;
			this.chkAutomaticallyGroup.Text = "Automatically group according to arrangement";
			this.chkAutomaticallyGroup.CheckedChanged += new System.EventHandler(this.OnGroupAutoChecked);
			// 
			// btnClearAllGroups
			// 
			this.btnClearAllGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClearAllGroups.Location = new System.Drawing.Point(348, 33);
			this.btnClearAllGroups.Name = "btnClearAllGroups";
			this.btnClearAllGroups.Size = new System.Drawing.Size(96, 24);
			this.btnClearAllGroups.TabIndex = 4;
			this.btnClearAllGroups.Text = "Clear All";
			this.btnClearAllGroups.Click += new System.EventHandler(this.OnClearAllGroups);
			// 
			// tabSort
			// 
			this.tabSort.Controls.Add(this.grpSortBy3);
			this.tabSort.Controls.Add(this.grpSortBy2);
			this.tabSort.Controls.Add(this.grpSortBy1);
			this.tabSort.Controls.Add(this.btnClearAllSorts);
			this.tabSort.Location = new System.Drawing.Point(4, 22);
			this.tabSort.Name = "tabSort";
			this.tabSort.Size = new System.Drawing.Size(454, 289);
			this.tabSort.TabIndex = 2;
			this.tabSort.Text = "Sort";
			// 
			// grpSortBy3
			// 
			this.grpSortBy3.Controls.Add(this.cboSortByField3);
			this.grpSortBy3.Controls.Add(this.rdoSortByDesc3);
			this.grpSortBy3.Controls.Add(this.rdoSortByAsc3);
			this.grpSortBy3.Location = new System.Drawing.Point(6, 186);
			this.grpSortBy3.Name = "grpSortBy3";
			this.grpSortBy3.Size = new System.Drawing.Size(327, 72);
			this.grpSortBy3.TabIndex = 9;
			this.grpSortBy3.TabStop = false;
			this.grpSortBy3.Text = "Then By";
			// 
			// cboSortByField3
			// 
			this.cboSortByField3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSortByField3.Location = new System.Drawing.Point(6, 21);
			this.cboSortByField3.Name = "cboSortByField3";
			this.cboSortByField3.Size = new System.Drawing.Size(195, 21);
			this.cboSortByField3.TabIndex = 7;
			this.cboSortByField3.SelectionChangeCommitted += new System.EventHandler(this.OnSortByField3Selected);
			// 
			// rdoSortByDesc3
			// 
			this.rdoSortByDesc3.Location = new System.Drawing.Point(219, 48);
			this.rdoSortByDesc3.Name = "rdoSortByDesc3";
			this.rdoSortByDesc3.Size = new System.Drawing.Size(96, 18);
			this.rdoSortByDesc3.TabIndex = 1;
			this.rdoSortByDesc3.Text = "Descending";
			this.rdoSortByDesc3.CheckedChanged += new System.EventHandler(this.OnSortDirectionChanged);
			// 
			// rdoSortByAsc3
			// 
			this.rdoSortByAsc3.Location = new System.Drawing.Point(219, 21);
			this.rdoSortByAsc3.Name = "rdoSortByAsc3";
			this.rdoSortByAsc3.Size = new System.Drawing.Size(96, 18);
			this.rdoSortByAsc3.TabIndex = 0;
			this.rdoSortByAsc3.Text = "Ascending";
			this.rdoSortByAsc3.CheckedChanged += new System.EventHandler(this.OnSortDirectionChanged);
			// 
			// grpSortBy2
			// 
			this.grpSortBy2.Controls.Add(this.cboSortByField2);
			this.grpSortBy2.Controls.Add(this.rdoSortByDesc2);
			this.grpSortBy2.Controls.Add(this.rdoSortByAsc2);
			this.grpSortBy2.Location = new System.Drawing.Point(6, 108);
			this.grpSortBy2.Name = "grpSortBy2";
			this.grpSortBy2.Size = new System.Drawing.Size(327, 72);
			this.grpSortBy2.TabIndex = 8;
			this.grpSortBy2.TabStop = false;
			this.grpSortBy2.Text = "Then By";
			// 
			// cboSortByField2
			// 
			this.cboSortByField2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSortByField2.Location = new System.Drawing.Point(6, 21);
			this.cboSortByField2.Name = "cboSortByField2";
			this.cboSortByField2.Size = new System.Drawing.Size(195, 21);
			this.cboSortByField2.TabIndex = 7;
			this.cboSortByField2.SelectionChangeCommitted += new System.EventHandler(this.OnSortByField2Selected);
			// 
			// rdoSortByDesc2
			// 
			this.rdoSortByDesc2.Location = new System.Drawing.Point(219, 48);
			this.rdoSortByDesc2.Name = "rdoSortByDesc2";
			this.rdoSortByDesc2.Size = new System.Drawing.Size(96, 18);
			this.rdoSortByDesc2.TabIndex = 1;
			this.rdoSortByDesc2.Text = "Descending";
			this.rdoSortByDesc2.CheckedChanged += new System.EventHandler(this.OnSortDirectionChanged);
			// 
			// rdoSortByAsc2
			// 
			this.rdoSortByAsc2.Location = new System.Drawing.Point(219, 21);
			this.rdoSortByAsc2.Name = "rdoSortByAsc2";
			this.rdoSortByAsc2.Size = new System.Drawing.Size(96, 18);
			this.rdoSortByAsc2.TabIndex = 0;
			this.rdoSortByAsc2.Text = "Ascending";
			this.rdoSortByAsc2.CheckedChanged += new System.EventHandler(this.OnSortDirectionChanged);
			// 
			// grpSortBy1
			// 
			this.grpSortBy1.Controls.Add(this.cboSortByField1);
			this.grpSortBy1.Controls.Add(this.rdoSortByDesc1);
			this.grpSortBy1.Controls.Add(this.rdoSortByAsc1);
			this.grpSortBy1.Location = new System.Drawing.Point(6, 30);
			this.grpSortBy1.Name = "grpSortBy1";
			this.grpSortBy1.Size = new System.Drawing.Size(327, 72);
			this.grpSortBy1.TabIndex = 7;
			this.grpSortBy1.TabStop = false;
			this.grpSortBy1.Text = "Sort Items By";
			// 
			// cboSortByField1
			// 
			this.cboSortByField1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSortByField1.Location = new System.Drawing.Point(6, 21);
			this.cboSortByField1.Name = "cboSortByField1";
			this.cboSortByField1.Size = new System.Drawing.Size(195, 21);
			this.cboSortByField1.TabIndex = 7;
			this.cboSortByField1.SelectionChangeCommitted += new System.EventHandler(this.OnSortByField1Selected);
			// 
			// rdoSortByDesc1
			// 
			this.rdoSortByDesc1.Location = new System.Drawing.Point(219, 48);
			this.rdoSortByDesc1.Name = "rdoSortByDesc1";
			this.rdoSortByDesc1.Size = new System.Drawing.Size(96, 18);
			this.rdoSortByDesc1.TabIndex = 1;
			this.rdoSortByDesc1.Text = "Descending";
			this.rdoSortByDesc1.CheckedChanged += new System.EventHandler(this.OnSortDirectionChanged);
			// 
			// rdoSortByAsc1
			// 
			this.rdoSortByAsc1.Location = new System.Drawing.Point(219, 21);
			this.rdoSortByAsc1.Name = "rdoSortByAsc1";
			this.rdoSortByAsc1.Size = new System.Drawing.Size(96, 18);
			this.rdoSortByAsc1.TabIndex = 0;
			this.rdoSortByAsc1.Text = "Ascending";
			this.rdoSortByAsc1.CheckedChanged += new System.EventHandler(this.OnSortDirectionChanged);
			// 
			// btnClearAllSorts
			// 
			this.btnClearAllSorts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClearAllSorts.Location = new System.Drawing.Point(348, 33);
			this.btnClearAllSorts.Name = "btnClearAllSorts";
			this.btnClearAllSorts.Size = new System.Drawing.Size(96, 24);
			this.btnClearAllSorts.TabIndex = 5;
			this.btnClearAllSorts.Text = "Clear All";
			this.btnClearAllSorts.Click += new System.EventHandler(this.OnClearAllSorts);
			// 
			// tabFormatColumns
			// 
			this.tabFormatColumns.Controls.Add(this.grpFormat);
			this.tabFormatColumns.Controls.Add(this.lstAvailableFormatFields);
			this.tabFormatColumns.Controls.Add(this._lblAvailableFormatFields);
			this.tabFormatColumns.Location = new System.Drawing.Point(4, 22);
			this.tabFormatColumns.Name = "tabFormatColumns";
			this.tabFormatColumns.Size = new System.Drawing.Size(454, 289);
			this.tabFormatColumns.TabIndex = 3;
			this.tabFormatColumns.Text = "Format Columns";
			// 
			// grpFormat
			// 
			this.grpFormat.Controls.Add(this.txtFormat);
			this.grpFormat.Controls.Add(this.grpWidth);
			this.grpFormat.Controls.Add(this.grpAlignment);
			this.grpFormat.Controls.Add(this.txtLabel);
			this.grpFormat.Controls.Add(this._lblLabel);
			this.grpFormat.Controls.Add(this._lblFormat);
			this.grpFormat.Location = new System.Drawing.Point(183, 9);
			this.grpFormat.Name = "grpFormat";
			this.grpFormat.Size = new System.Drawing.Size(267, 270);
			this.grpFormat.TabIndex = 6;
			this.grpFormat.TabStop = false;
			// 
			// txtFormat
			// 
			this.txtFormat.Location = new System.Drawing.Point(69, 24);
			this.txtFormat.Name = "txtFormat";
			this.txtFormat.Size = new System.Drawing.Size(192, 20);
			this.txtFormat.TabIndex = 18;
			this.txtFormat.Text = "";
			this.txtFormat.TextChanged += new System.EventHandler(this.OnFormatChanged);
			// 
			// grpWidth
			// 
			this.grpWidth.Controls.Add(this._lblWidthUnits);
			this.grpWidth.Controls.Add(this.rdoWidthBestFit);
			this.grpWidth.Controls.Add(this.txtWidthSpecific);
			this.grpWidth.Controls.Add(this.rdoWidthSpecific);
			this.grpWidth.Location = new System.Drawing.Point(6, 93);
			this.grpWidth.Name = "grpWidth";
			this.grpWidth.Size = new System.Drawing.Size(255, 96);
			this.grpWidth.TabIndex = 17;
			this.grpWidth.TabStop = false;
			this.grpWidth.Text = "Width";
			// 
			// _lblWidthUnits
			// 
			this._lblWidthUnits.Location = new System.Drawing.Point(201, 24);
			this._lblWidthUnits.Name = "_lblWidthUnits";
			this._lblWidthUnits.Size = new System.Drawing.Size(48, 18);
			this._lblWidthUnits.TabIndex = 10;
			this._lblWidthUnits.Text = "pixels";
			this._lblWidthUnits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// rdoWidthBestFit
			// 
			this.rdoWidthBestFit.Enabled = false;
			this.rdoWidthBestFit.Location = new System.Drawing.Point(63, 57);
			this.rdoWidthBestFit.Name = "rdoWidthBestFit";
			this.rdoWidthBestFit.Size = new System.Drawing.Size(72, 18);
			this.rdoWidthBestFit.TabIndex = 9;
			this.rdoWidthBestFit.Text = "Best Fit";
			// 
			// txtWidthSpecific
			// 
			this.txtWidthSpecific.Location = new System.Drawing.Point(144, 24);
			this.txtWidthSpecific.Name = "txtWidthSpecific";
			this.txtWidthSpecific.Size = new System.Drawing.Size(50, 20);
			this.txtWidthSpecific.TabIndex = 15;
			this.txtWidthSpecific.Text = "";
			this.txtWidthSpecific.TextChanged += new System.EventHandler(this.OnWidthChanged);
			// 
			// rdoWidthSpecific
			// 
			this.rdoWidthSpecific.Checked = true;
			this.rdoWidthSpecific.Location = new System.Drawing.Point(63, 24);
			this.rdoWidthSpecific.Name = "rdoWidthSpecific";
			this.rdoWidthSpecific.Size = new System.Drawing.Size(72, 18);
			this.rdoWidthSpecific.TabIndex = 8;
			this.rdoWidthSpecific.TabStop = true;
			this.rdoWidthSpecific.Text = "Specific";
			// 
			// grpAlignment
			// 
			this.grpAlignment.Controls.Add(this.rdoAlignLeft);
			this.grpAlignment.Controls.Add(this.rdoAlignRight);
			this.grpAlignment.Controls.Add(this.rdoAlignCenter);
			this.grpAlignment.Location = new System.Drawing.Point(6, 198);
			this.grpAlignment.Name = "grpAlignment";
			this.grpAlignment.Size = new System.Drawing.Size(255, 60);
			this.grpAlignment.TabIndex = 16;
			this.grpAlignment.TabStop = false;
			this.grpAlignment.Text = "Alignment";
			// 
			// rdoAlignLeft
			// 
			this.rdoAlignLeft.Checked = true;
			this.rdoAlignLeft.Location = new System.Drawing.Point(63, 24);
			this.rdoAlignLeft.Name = "rdoAlignLeft";
			this.rdoAlignLeft.Size = new System.Drawing.Size(60, 18);
			this.rdoAlignLeft.TabIndex = 7;
			this.rdoAlignLeft.TabStop = true;
			this.rdoAlignLeft.Text = "Left";
			this.rdoAlignLeft.CheckedChanged += new System.EventHandler(this.OnAlignmentChanged);
			// 
			// rdoAlignRight
			// 
			this.rdoAlignRight.Location = new System.Drawing.Point(189, 24);
			this.rdoAlignRight.Name = "rdoAlignRight";
			this.rdoAlignRight.Size = new System.Drawing.Size(60, 18);
			this.rdoAlignRight.TabIndex = 13;
			this.rdoAlignRight.Text = "Right";
			this.rdoAlignRight.CheckedChanged += new System.EventHandler(this.OnAlignmentChanged);
			// 
			// rdoAlignCenter
			// 
			this.rdoAlignCenter.Location = new System.Drawing.Point(123, 24);
			this.rdoAlignCenter.Name = "rdoAlignCenter";
			this.rdoAlignCenter.Size = new System.Drawing.Size(60, 18);
			this.rdoAlignCenter.TabIndex = 12;
			this.rdoAlignCenter.Text = "Center";
			this.rdoAlignCenter.CheckedChanged += new System.EventHandler(this.OnAlignmentChanged);
			// 
			// txtLabel
			// 
			this.txtLabel.Location = new System.Drawing.Point(69, 54);
			this.txtLabel.Name = "txtLabel";
			this.txtLabel.Size = new System.Drawing.Size(192, 20);
			this.txtLabel.TabIndex = 14;
			this.txtLabel.Text = "";
			this.txtLabel.TextChanged += new System.EventHandler(this.OnLabelChanged);
			// 
			// _lblLabel
			// 
			this._lblLabel.Location = new System.Drawing.Point(6, 54);
			this._lblLabel.Name = "_lblLabel";
			this._lblLabel.Size = new System.Drawing.Size(54, 18);
			this._lblLabel.TabIndex = 4;
			this._lblLabel.Text = "Label:";
			this._lblLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblFormat
			// 
			this._lblFormat.Location = new System.Drawing.Point(6, 24);
			this._lblFormat.Name = "_lblFormat";
			this._lblFormat.Size = new System.Drawing.Size(54, 18);
			this._lblFormat.TabIndex = 3;
			this._lblFormat.Text = "Format:";
			this._lblFormat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lstAvailableFormatFields
			// 
			this.lstAvailableFormatFields.Location = new System.Drawing.Point(6, 27);
			this.lstAvailableFormatFields.Name = "lstAvailableFormatFields";
			this.lstAvailableFormatFields.ScrollAlwaysVisible = true;
			this.lstAvailableFormatFields.Size = new System.Drawing.Size(168, 251);
			this.lstAvailableFormatFields.TabIndex = 5;
			this.lstAvailableFormatFields.SelectedIndexChanged += new System.EventHandler(this.OnAvailableFormatFieldSelected);
			// 
			// _lblAvailableFormatFields
			// 
			this._lblAvailableFormatFields.Location = new System.Drawing.Point(6, 6);
			this._lblAvailableFormatFields.Name = "_lblAvailableFormatFields";
			this._lblAvailableFormatFields.Size = new System.Drawing.Size(168, 18);
			this._lblAvailableFormatFields.TabIndex = 4;
			this._lblAvailableFormatFields.Text = "Available fields:";
			this._lblAvailableFormatFields.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dlgLayout
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(474, 352);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgLayout";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Schedule Layout";
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.tabControl1.ResumeLayout(false);
			this.tabShowFields.ResumeLayout(false);
			this.tabGroupBy.ResumeLayout(false);
			this.grpGroupBy3.ResumeLayout(false);
			this.grpGroupBy2.ResumeLayout(false);
			this.grpGroupBy1.ResumeLayout(false);
			this.tabSort.ResumeLayout(false);
			this.grpSortBy3.ResumeLayout(false);
			this.grpSortBy2.ResumeLayout(false);
			this.grpSortBy1.ResumeLayout(false);
			this.tabFormatColumns.ResumeLayout(false);
			this.grpFormat.ResumeLayout(false);
			this.grpWidth.ResumeLayout(false);
			this.grpAlignment.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Form load event handler
			try {
				#region Show Fields
				foreach(LayoutEntry entry in this.mLayout) {
					if(entry.Visible)
						this.lstSelectedShowFields.Items.Add(entry.Key);
					else
						this.lstAvailableShowFields.Items.Add(entry.Key);
				}
				this.btnMoveUp.Enabled = this.btnMoveDown.Enabled = false;
				this.btnAdd.Enabled = this.btnRemove.Enabled = false;
				if(this.lstAvailableShowFields.Items.Count > 0) this.lstAvailableShowFields.SelectedIndex = 0;
				#endregion
				#region GroupBy
				bool hasGroupBy=false;
				foreach(LayoutEntry entry in this.mLayout) {
					if(hasGroupBy = entry.GroupBy) break;
				}
				this.chkAutomaticallyGroup.Checked = !hasGroupBy;
				OnGroupAutoChecked(null,null);
				this.btnClearAllGroups.Enabled = false;
				#endregion
				#region Sort By
				//Add field selections and select first sort by field if applicable
				//NOTE: All group by fields must have a sort order value less than the 
				//		value for non-group by fields; int k counts group by fields
				this.cboSortByField1.Items.Clear();
				this.cboSortByField1.Items.Add(NONE);
				int k=0;
				foreach(LayoutEntry entry in this.mLayout) {
					bool groupBy = entry.GroupBy;
					if(groupBy) k++;
					this.cboSortByField1.Items.Add(entry.Key);
				}
				foreach(LayoutEntry entry in this.mLayout) {
					//Sort order value must be next permissible
					if(entry.Sort != "" && entry.SortOrder == k) {
						//Set the first sort by field
						this.cboSortByField1.SelectedItem = entry.Key;
						this.rdoSortByAsc1.Checked = (entry.Sort == "A");
						this.rdoSortByDesc1.Checked = (entry.Sort == "D");
						break;
					}
				}
				if(this.cboSortByField1.SelectedItem == null) {
					this.cboSortByField1.SelectedIndex = 0;
					this.rdoSortByAsc1.Checked = true;
				}
				OnSortByField1Selected(null,null);	//Cascade
				this.grpSortBy1.Enabled = this.cboSortByField1.Enabled = true;
				this.btnClearAllSorts.Enabled = false;
				#endregion
				#region Format
				this.lstAvailableFormatFields.ValueMember = "Key";
				foreach(LayoutEntry entry in this.mLayout) 
					this.lstAvailableFormatFields.Items.Add(entry);
				this.rdoWidthSpecific.Checked = true;
				this.rdoWidthBestFit.Checked = false;
				this.rdoWidthBestFit.Enabled = false;
				this.lstAvailableFormatFields.SelectedIndex = 0;
				OnAvailableFormatFieldSelected(null,null);
				#endregion
			}
			catch(Exception) { }
			finally { this.btnOK.Enabled = false; }
		}
		#region Show Fields Handlers
		private void OnAvailableFieldSelected(object sender, System.EventArgs e) {
			//Event handler for 
			try { 
				this.btnAdd.Enabled = true;
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		private void OnSelectedFieldSelected(object sender, System.EventArgs e) {
			//Event handler for 
			try { 
				this.btnRemove.Enabled = true;
				this.btnMoveUp.Enabled = this.btnMoveDown.Enabled = true;
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		private void OnAddField(object sender, System.EventArgs e) {
			//Event handler for 
			try { 
				object o = this.lstAvailableShowFields.SelectedItem;
				this.lstAvailableShowFields.Items.Remove(o);
				if(this.lstAvailableShowFields.Items.Count > 0) this.lstAvailableShowFields.SelectedIndex = 0;
				this.lstSelectedShowFields.Items.Add(o);
				this.lstSelectedShowFields.SelectedIndex = this.lstSelectedShowFields.Items.Count - 1;
				this.btnAdd.Enabled = (this.lstAvailableShowFields.Items.Count > 0);
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		private void OnRemoveField(object sender, System.EventArgs e) {
			//Event handler for 
			try { 
				object o = this.lstSelectedShowFields.SelectedItem;
				this.lstSelectedShowFields.Items.Remove(o);
				if(this.lstSelectedShowFields.Items.Count > 0) this.lstSelectedShowFields.SelectedIndex = this.lstSelectedShowFields.Items.Count - 1;
				this.lstAvailableShowFields.Items.Add(o);
				this.lstAvailableShowFields.SelectedIndex = 0;
				this.btnRemove.Enabled = (this.lstSelectedShowFields.Items.Count > 0);
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		private void OnMoveFieldUp(object sender, System.EventArgs e) {
			//Event handler for 
			try { 
				object o = this.lstSelectedShowFields.SelectedItem;
				int index = this.lstSelectedShowFields.Items.IndexOf(o);
				if(index > 0) {
					this.lstSelectedShowFields.Items.RemoveAt(index);
					this.lstSelectedShowFields.Items.Insert(index - 1, o);
					this.lstSelectedShowFields.SelectedIndex = index - 1;
				}
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		private void OnMoveFieldDown(object sender, System.EventArgs e) {
			//Event handler for 
			try { 
				object o = this.lstSelectedShowFields.SelectedItem;
				int index = this.lstSelectedShowFields.Items.IndexOf(o);
				if(index < this.lstSelectedShowFields.Items.Count - 1) {
					this.lstSelectedShowFields.Items.RemoveAt(index);
					this.lstSelectedShowFields.Items.Insert(index + 1, o);
					this.lstSelectedShowFields.SelectedIndex = index + 1;
				}
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		#endregion
		#region GroupBy Handlers
		private void OnGroupAutoChecked(object sender, System.EventArgs e) {
			//Event handler for auto-grouping on\off
			try { 
				//Init field selections and enable\disable selection 1
				this.cboGroupByField1.Items.Clear();
				this.cboGroupByField1.Items.Add(NONE);
				if(this.chkAutomaticallyGroup.Checked) {
					this.cboGroupByField1.SelectedIndex = 0;
				}
				else {
					//Add field selections and select first group by field if applicable
					foreach(LayoutEntry entry in this.mLayout) 
						this.cboGroupByField1.Items.Add(entry.Key);
					foreach(LayoutEntry entry in this.mLayout) {
						if(entry.GroupBy && entry.SortOrder == 0) {
							//Set the first group by field and it's layout attributes
							this.cboGroupByField1.SelectedItem = entry.Key;
							this.chkShowInView1.Checked = entry.Visible;
							this.rdoGroupByAsc1.Checked = entry.Sort == "A";
							this.rdoGroupByDesc1.Checked = entry.Sort == "D";
							break;
						}
					}
					if(this.cboGroupByField1.SelectedItem == null) {
						this.cboGroupByField1.SelectedIndex = 0;
						this.chkShowInView1.Checked = this.rdoGroupByAsc1.Checked = true;
					}
				}
				OnGroupByField1Selected(null,null);	//Cascade
				this.grpGroupBy1.Enabled = this.cboGroupByField1.Enabled = (!this.chkAutomaticallyGroup.Checked);
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		private void OnGroupByField1Selected(object sender, System.EventArgs e) {
			//Event handler for the first group by field selection
			try { 
				//Enable\disable changes to show-in-view & sort order; enable\disable next group by field (2)
				this.cboGroupByField2.Items.Clear();
				this.cboGroupByField2.Items.Add(NONE);
				if(this.cboGroupByField1.SelectedText != NONE) {
					//Add field selections (excluding first group by field) and select second group by field if applicable
					foreach(LayoutEntry entry in this.mLayout) {
						if(entry.Key != this.cboGroupByField1.Text)
							this.cboGroupByField2.Items.Add(entry.Key);
					}
					foreach(LayoutEntry entry in this.mLayout) {
						if(entry.GroupBy && entry.SortOrder == 1) {
							//Set the second group by field and it's layout attributes
							this.cboGroupByField2.SelectedItem = entry.Key;
							this.chkShowInView2.Checked = entry.Visible;
							this.rdoGroupByAsc2.Checked = entry.Sort == "A";
							this.rdoGroupByDesc2.Checked = entry.Sort == "D";
							break;
						}
					}
					if(this.cboGroupByField2.SelectedItem == null) {
						this.cboGroupByField2.SelectedIndex = 0;
						this.chkShowInView2.Checked = this.rdoGroupByAsc2.Checked = true;
					}
				}
				else {
					this.cboGroupByField2.SelectedIndex = 0;
					this.chkShowInView2.Checked = this.rdoGroupByAsc2.Checked = true;
				}
				OnGroupByField2Selected(null,null);	//Cascade
				this.grpGroupBy2.Enabled = this.cboGroupByField2.Enabled = (this.cboGroupByField1.Text != NONE);
				this.chkShowInView1.Enabled = this.rdoGroupByAsc1.Enabled = this.rdoGroupByDesc1.Enabled = (this.cboGroupByField1.Text != NONE);
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		private void OnGroupByField2Selected(object sender, System.EventArgs e) {
			//Event handler for the second group by field selection
			try {
				//Enable\disable changes to show-in-view & sort order; enable\disable next group by field (3)
				this.cboGroupByField3.Items.Clear();
				this.cboGroupByField3.Items.Add(NONE);
				if(this.cboGroupByField2.SelectedText != NONE) {
					//Add field selections (excluding first & second group by fields) and select third group by field if applicable
					foreach(LayoutEntry entry in this.mLayout) {
						if(entry.Key != this.cboGroupByField1.Text && entry.Key != this.cboGroupByField2.Text)
							this.cboGroupByField3.Items.Add(entry.Key);
					}
					foreach(LayoutEntry entry in this.mLayout) {
						if(entry.GroupBy && entry.SortOrder == 2) {
							//Set the third group by field and it's layout attributes
							this.cboGroupByField3.SelectedItem = entry.Key;
							this.chkShowInView3.Checked = entry.Visible;
							this.rdoGroupByAsc3.Checked = entry.Sort == "A";
							this.rdoGroupByDesc3.Checked = entry.Sort == "D";
							break;
						}
					}
					if(this.cboGroupByField3.SelectedItem == null) {
						this.cboGroupByField3.SelectedIndex = 0;
						this.chkShowInView3.Checked = this.rdoGroupByAsc3.Checked = true;
					}
				}
				else {
					this.cboGroupByField3.SelectedIndex = 0;
					this.chkShowInView3.Checked = this.rdoGroupByAsc3.Checked = true;
				}
				OnGroupByField3Selected(null,null);	//Cascade
				this.grpGroupBy3.Enabled = this.cboGroupByField3.Enabled = (this.cboGroupByField2.Text != NONE);
				this.chkShowInView2.Enabled = this.rdoGroupByAsc2.Enabled = this.rdoGroupByDesc2.Enabled = (this.cboGroupByField2.Text != NONE);
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		private void OnGroupByField3Selected(object sender, System.EventArgs e) {
			//Event handler for the third group by field selection
			try {
				//Enable\disable changes to show-in-view & sort order
				this.chkShowInView3.Enabled = this.rdoGroupByAsc3.Enabled = this.rdoGroupByDesc3.Enabled = (this.cboGroupByField3.Text != NONE);
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		private void OnClearAllGroups(object sender, System.EventArgs e) {
			//Event handler for user cleared all current group by fields
			try {
				//Add field selections for first group
				this.cboGroupByField1.Items.Clear();
				this.cboGroupByField1.Items.Add(NONE);
				foreach(LayoutEntry entry in this.mLayout) 
					this.cboGroupByField1.Items.Add(entry.Key);
				this.cboGroupByField1.SelectedIndex = 0;
				OnGroupByField1Selected(null,null);	//Cascade
				this.grpGroupBy1.Enabled = this.cboGroupByField1.Enabled = (!this.chkAutomaticallyGroup.Checked);
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		#endregion
		#region Sort By Handlers
		private void OnSortByField1Selected(object sender, System.EventArgs e) {
			//Event handler for the first group by field selection
			try { 
				//Enable\disable changes to sort order; enable\disable next sort by field (2)
				this.cboSortByField2.Items.Clear();
				this.cboSortByField2.Items.Add(NONE);
				if(this.cboSortByField1.SelectedText != NONE) {
					//Add field selections (excluding first sort by field) and select second sort by field if applicable
					int k=0;
					foreach(LayoutEntry entry in this.mLayout) {
						if(entry.GroupBy) k++;
						if(entry.Key != this.cboSortByField1.Text)
							this.cboSortByField2.Items.Add(entry.Key);
					}
					foreach(LayoutEntry entry in this.mLayout) {
						//Don't count group by fields (which have a sort dirction); sort order value must be next permissible
						if(entry.Sort != "" && entry.SortOrder == (k+1)) {
							//Set the second sort by field
							this.cboSortByField2.SelectedItem = entry.Key;
							this.rdoSortByAsc2.Checked = (entry.Sort == "A");
							this.rdoSortByDesc2.Checked = (entry.Sort == "D");
							break;
						}
					}
					if(this.cboSortByField2.SelectedItem == null) {
						this.cboSortByField2.SelectedIndex = 0;
						this.rdoSortByAsc2.Checked = true;
					}
				}
				else {
					this.cboSortByField2.SelectedIndex = 0;
					this.rdoSortByAsc2.Checked = true;
				}
				OnSortByField2Selected(null,null);	//Cascade
				this.grpSortBy2.Enabled = this.cboSortByField2.Enabled = (this.cboSortByField1.Text != NONE);
				this.rdoSortByAsc1.Enabled = this.rdoSortByDesc1.Enabled = (this.cboSortByField1.Text != NONE);
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		private void OnSortByField2Selected(object sender, System.EventArgs e) {
			//Event handler for the second group by field selection
			try {
				//Enable\disable changes to sort order; enable\disable next sort by field (3)
				this.cboSortByField3.Items.Clear();
				this.cboSortByField3.Items.Add(NONE);
				int k=0;
				if(this.cboSortByField2.SelectedText != NONE) {
					//Add field selections (excluding first & second sort by fields) and select third sort by field if applicable
					foreach(LayoutEntry entry in this.mLayout) {
						if(entry.GroupBy) k++;
						if(entry.Key != this.cboSortByField1.Text && entry.Key != this.cboSortByField2.Text)
							this.cboSortByField3.Items.Add(entry.Key);
					}
					foreach(LayoutEntry entry in this.mLayout) {
						//Don't count group by fields (which have a sort dirction); sort order value must be next permissible
						if(entry.Sort != "" && entry.SortOrder == (k+2)) {
							//Set the second sort by field
							this.cboSortByField2.SelectedItem = entry.Key;
							this.rdoSortByAsc2.Checked = (entry.Sort == "A");
							this.rdoSortByDesc2.Checked = (entry.Sort == "D");
							break;
						}
					}
					if(this.cboSortByField3.SelectedItem == null) {
						this.cboSortByField3.SelectedIndex = 0;
						this.rdoSortByAsc3.Checked = true;
					}
				}
				else {
					this.cboSortByField3.SelectedIndex = 0;
					this.rdoSortByAsc3.Checked = true;
				}
				OnSortByField3Selected(null,null);	//Cascade
				this.grpSortBy3.Enabled = this.cboSortByField3.Enabled = (this.cboSortByField2.Text != NONE);
				this.rdoSortByAsc2.Enabled = this.rdoSortByDesc2.Enabled = (this.cboSortByField2.Text != NONE);
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		private void OnSortByField3Selected(object sender, System.EventArgs e) {
			//Event handler for the third sort by field selection
			try {
				//Enable\disable changes to sort order
				this.rdoSortByAsc3.Enabled = this.rdoSortByDesc3.Enabled = (this.cboSortByField3.Text != NONE);
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		private void OnClearAllSorts(object sender, System.EventArgs e) {
			//Event handler for user cleared all current group by fields
			try {
				//Add field selections for first group
				this.cboSortByField1.Items.Clear();
				this.cboSortByField1.Items.Add(NONE);
				foreach(LayoutEntry entry in this.mLayout) 
					this.cboSortByField1.Items.Add(entry.Key);
				this.cboSortByField1.SelectedIndex = 0;
				OnSortByField1Selected(null,null);	//Cascade
				this.grpSortBy1.Enabled = this.cboSortByField1.Enabled = true;
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		#endregion
		#region Format Handlers
		private void OnAvailableFormatFieldSelected(object sender, System.EventArgs e) {
			//Event handler for selcted format field changed
			try {
				LayoutEntry entry = (LayoutEntry)this.lstAvailableFormatFields.SelectedItem;
				this.txtFormat.Text = entry.Format;
				this.txtLabel.Text = entry.Caption;
				this.txtWidthSpecific.Text = entry.Width.ToString();
				switch(entry.Alignment) {
					case "L": this.rdoAlignLeft.Checked = true; break;
					case "C": this.rdoAlignCenter.Checked = true; break;
					case "R": this.rdoAlignRight.Checked = true; break;
				}
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		private void OnFormatChanged(object sender, System.EventArgs e) {
			//Event handler for 
			try {
				LayoutEntry entry = (LayoutEntry)this.lstAvailableFormatFields.SelectedItem;
				entry.Format = this.txtFormat.Text;
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		private void OnLabelChanged(object sender, System.EventArgs e) {
			//Event handler for 
			try {
				LayoutEntry entry = (LayoutEntry)this.lstAvailableFormatFields.SelectedItem;
				entry.Caption = this.txtLabel.Text;
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		private void OnWidthChanged(object sender, System.EventArgs e) {
			//Event handler for 
			try {
				LayoutEntry entry = (LayoutEntry)this.lstAvailableFormatFields.SelectedItem;
				entry.Width = Convert.ToInt32(this.txtWidthSpecific.Text);
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		private void OnAlignmentChanged(object sender, System.EventArgs e) {
			//Event handler for 
			try {
				LayoutEntry entry = (LayoutEntry)this.lstAvailableFormatFields.SelectedItem;
				if(this.rdoAlignLeft.Checked) entry.Alignment = "L";
				if(this.rdoAlignCenter.Checked) entry.Alignment = "C";
				if(this.rdoAlignRight.Checked) entry.Alignment = "R";
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		#endregion
		#region Common Handlers
		private void OnSortDirectionChanged(object sender, System.EventArgs e) {
			//Event handler for change in sort direction
			try {
				RadioButton rdo = (RadioButton)sender;
				this.rdoGroupByAsc1.CheckedChanged -= new System.EventHandler(this.OnSortDirectionChanged);
				this.rdoGroupByAsc2.CheckedChanged -= new System.EventHandler(this.OnSortDirectionChanged);
				this.rdoGroupByAsc3.CheckedChanged -= new System.EventHandler(this.OnSortDirectionChanged);
				this.rdoGroupByDesc1.CheckedChanged -= new System.EventHandler(this.OnSortDirectionChanged);
				this.rdoGroupByDesc2.CheckedChanged -= new System.EventHandler(this.OnSortDirectionChanged);
				this.rdoGroupByDesc3.CheckedChanged -= new System.EventHandler(this.OnSortDirectionChanged);
				this.rdoSortByAsc1.CheckedChanged -= new System.EventHandler(this.OnSortDirectionChanged);
				this.rdoSortByAsc2.CheckedChanged -= new System.EventHandler(this.OnSortDirectionChanged);
				this.rdoSortByAsc3.CheckedChanged -= new System.EventHandler(this.OnSortDirectionChanged);
				this.rdoSortByDesc1.CheckedChanged -= new System.EventHandler(this.OnSortDirectionChanged);
				this.rdoSortByDesc2.CheckedChanged -= new System.EventHandler(this.OnSortDirectionChanged);
				this.rdoSortByDesc3.CheckedChanged -= new System.EventHandler(this.OnSortDirectionChanged);
				switch(rdo.Name) {
					case "rdoGroupByAsc1":
						if(this.cboSortByField1.Text == this.cboGroupByField1.Text) this.rdoSortByAsc1.Checked = true;
						if(this.cboSortByField2.Text == this.cboGroupByField1.Text) this.rdoSortByAsc2.Checked = true;
						if(this.cboSortByField3.Text == this.cboGroupByField1.Text) this.rdoSortByAsc3.Checked = true;
						break;
					case "rdoGroupByDesc1":
						if(this.cboSortByField1.Text == this.cboGroupByField1.Text) this.rdoSortByDesc1.Checked = true;
						if(this.cboSortByField2.Text == this.cboGroupByField1.Text) this.rdoSortByDesc2.Checked = true;
						if(this.cboSortByField3.Text == this.cboGroupByField1.Text) this.rdoSortByDesc3.Checked = true;
						break;
					case "rdoGroupByAsc2":
						if(this.cboSortByField1.Text == this.cboGroupByField2.Text) this.rdoSortByAsc1.Checked = true;
						if(this.cboSortByField2.Text == this.cboGroupByField2.Text) this.rdoSortByAsc2.Checked = true;
						if(this.cboSortByField3.Text == this.cboGroupByField2.Text) this.rdoSortByAsc3.Checked = true;
						break;
					case "rdoGroupByDesc2":
						if(this.cboSortByField1.Text == this.cboGroupByField2.Text) this.rdoSortByDesc1.Checked = true;
						if(this.cboSortByField2.Text == this.cboGroupByField2.Text) this.rdoSortByDesc2.Checked = true;
						if(this.cboSortByField3.Text == this.cboGroupByField2.Text) this.rdoSortByDesc3.Checked = true;
						break;
					case "rdoGroupByAsc3":
						if(this.cboSortByField1.Text == this.cboGroupByField3.Text) this.rdoSortByAsc1.Checked = true;
						if(this.cboSortByField2.Text == this.cboGroupByField3.Text) this.rdoSortByAsc2.Checked = true;
						if(this.cboSortByField3.Text == this.cboGroupByField3.Text) this.rdoSortByAsc3.Checked = true;
						break;
					case "rdoGroupByDesc3":
						if(this.cboSortByField1.Text == this.cboGroupByField3.Text) this.rdoSortByDesc1.Checked = true;
						if(this.cboSortByField2.Text == this.cboGroupByField3.Text) this.rdoSortByDesc2.Checked = true;
						if(this.cboSortByField3.Text == this.cboGroupByField3.Text) this.rdoSortByDesc3.Checked = true;
						break;
					case "rdoSortByAsc1":
						if(this.cboGroupByField1.Text == this.cboSortByField1.Text) this.rdoGroupByAsc1.Checked = true;
						if(this.cboGroupByField2.Text == this.cboSortByField1.Text) this.rdoGroupByAsc2.Checked = true;
						if(this.cboGroupByField3.Text == this.cboSortByField1.Text) this.rdoGroupByAsc3.Checked = true;
						break;
					case "rdoSortByDesc1":
						if(this.cboGroupByField1.Text == this.cboSortByField1.Text) this.rdoGroupByDesc1.Checked = true;
						if(this.cboGroupByField2.Text == this.cboSortByField1.Text) this.rdoGroupByDesc2.Checked = true;
						if(this.cboGroupByField3.Text == this.cboSortByField1.Text) this.rdoGroupByDesc3.Checked = true;
						break;
					case "rdoSortByAsc2":
						if(this.cboGroupByField1.Text == this.cboSortByField2.Text) this.rdoGroupByAsc1.Checked = true;
						if(this.cboGroupByField2.Text == this.cboSortByField2.Text) this.rdoGroupByAsc2.Checked = true;
						if(this.cboGroupByField3.Text == this.cboSortByField2.Text) this.rdoGroupByAsc3.Checked = true;
						break;
					case "rdoSortByDesc2":
						if(this.cboGroupByField1.Text == this.cboSortByField2.Text) this.rdoGroupByDesc1.Checked = true;
						if(this.cboGroupByField2.Text == this.cboSortByField2.Text) this.rdoGroupByDesc2.Checked = true;
						if(this.cboGroupByField3.Text == this.cboSortByField2.Text) this.rdoGroupByDesc3.Checked = true;
						break;
					case "rdoSortByAsc3":
						if(this.cboGroupByField1.Text == this.cboSortByField3.Text) this.rdoGroupByAsc1.Checked = true;
						if(this.cboGroupByField2.Text == this.cboSortByField3.Text) this.rdoGroupByAsc2.Checked = true;
						if(this.cboGroupByField3.Text == this.cboSortByField3.Text) this.rdoGroupByAsc3.Checked = true;
						break;
					case "rdoSortByDesc3":
						if(this.cboGroupByField1.Text == this.cboSortByField3.Text) this.rdoGroupByDesc1.Checked = true;
						if(this.cboGroupByField2.Text == this.cboSortByField3.Text) this.rdoGroupByDesc2.Checked = true;
						if(this.cboGroupByField3.Text == this.cboSortByField3.Text) this.rdoGroupByDesc3.Checked = true;
						break;
				}
				this.rdoGroupByAsc1.CheckedChanged += new System.EventHandler(this.OnSortDirectionChanged);
				this.rdoGroupByAsc2.CheckedChanged += new System.EventHandler(this.OnSortDirectionChanged);
				this.rdoGroupByAsc3.CheckedChanged += new System.EventHandler(this.OnSortDirectionChanged);
				this.rdoGroupByDesc1.CheckedChanged += new System.EventHandler(this.OnSortDirectionChanged);
				this.rdoGroupByDesc2.CheckedChanged += new System.EventHandler(this.OnSortDirectionChanged);
				this.rdoGroupByDesc3.CheckedChanged += new System.EventHandler(this.OnSortDirectionChanged);
				this.rdoSortByAsc1.CheckedChanged += new System.EventHandler(this.OnSortDirectionChanged);
				this.rdoSortByAsc2.CheckedChanged += new System.EventHandler(this.OnSortDirectionChanged);
				this.rdoSortByAsc3.CheckedChanged += new System.EventHandler(this.OnSortDirectionChanged);
				this.rdoSortByDesc1.CheckedChanged += new System.EventHandler(this.OnSortDirectionChanged);
				this.rdoSortByDesc2.CheckedChanged += new System.EventHandler(this.OnSortDirectionChanged);
				this.rdoSortByDesc3.CheckedChanged += new System.EventHandler(this.OnSortDirectionChanged);
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		#endregion
		private void OnButtonClick(object sender, System.EventArgs e) {
			//Button click event handler
			int k=0;
			Button btn = (Button)sender;
			switch(btn.Text) {
				case "&Cancel":	this.DialogResult = DialogResult.Cancel; break;
				case "O&K":		
					LayoutDS layout = new LayoutDS();
					string viewName = this.mLayout.ViewName;
					string scheduleName = this.mLayout.ScheduleName;
					#region Show Fields
					//Create new layout; set visible, visible position
					for(int i=0; i<this.lstAvailableShowFields.Items.Count; i++) 
						layout.LayoutTable.AddLayoutTableRow(viewName,scheduleName,this.lstAvailableShowFields.Items[i].ToString(),i,false,this.lstAvailableShowFields.Items[i].ToString(),96,"L","","","",0,false);
					for(int i=0; i<this.lstSelectedShowFields.Items.Count; i++) 
						layout.LayoutTable.AddLayoutTableRow(viewName,scheduleName,this.lstSelectedShowFields.Items[i].ToString(),i,true,this.lstSelectedShowFields.Items[i].ToString(),96,"L","","","",0,false);
					#endregion
					#region Group By
					//Set group by, visible, sort direction, and sort order for group by field
					if(this.cboGroupByField1.Text != NONE) {
						LayoutDS.LayoutTableRow row = (LayoutDS.LayoutTableRow)layout.LayoutTable.Select("Key = '" + this.cboGroupByField1.Text + "'")[0];
						row.GroupBy = true;
						row.Visible = this.chkShowInView1.Checked;
						row.SortOrder = 0;
						row.Sort = (this.rdoGroupByDesc1.Checked) ? "D" : "A";
						k = 1;
					}
					if(this.cboGroupByField2.Text != NONE) {
						LayoutDS.LayoutTableRow row = (LayoutDS.LayoutTableRow)layout.LayoutTable.Select("Key = '" + this.cboGroupByField2.Text + "'")[0];
						row.GroupBy = true;
						row.Visible = this.chkShowInView2.Checked;
						row.SortOrder = 1;
						row.Sort = (this.rdoGroupByDesc2.Checked) ? "D" : "A";
						k = 2;
					}
					if(this.cboGroupByField3.Text != NONE) {
						LayoutDS.LayoutTableRow row = (LayoutDS.LayoutTableRow)layout.LayoutTable.Select("Key = '" + this.cboGroupByField3.Text + "'")[0];
						row.GroupBy = true;
						row.Visible = this.chkShowInView3.Checked;
						row.SortOrder = 2;
						row.Sort = (this.rdoGroupByDesc3.Checked) ? "D" : "A";
						k = 3;
					}
					#endregion
					#region Sort By
					//Set sort order and sort direction for sort only field
					if(this.cboSortByField1.Text != NONE) {
						LayoutDS.LayoutTableRow row = (LayoutDS.LayoutTableRow)layout.LayoutTable.Select("Key = '" + this.cboSortByField1.Text + "'")[0];
						if(!row.GroupBy) {
							row.SortOrder = k;
							row.Sort = (this.rdoSortByDesc1.Checked) ? "D" : "A";
							k++;
						}
					}
					if(this.cboSortByField2.Text != NONE) {
						LayoutDS.LayoutTableRow row = (LayoutDS.LayoutTableRow)layout.LayoutTable.Select("Key = '" + this.cboSortByField2.Text + "'")[0];
						if(!row.GroupBy) {
							row.SortOrder = k;
							row.Sort = (this.rdoSortByDesc2.Checked) ? "D" : "A";
							k++;
						}
					}
					if(this.cboSortByField3.Text != NONE) {
						LayoutDS.LayoutTableRow row = (LayoutDS.LayoutTableRow)layout.LayoutTable.Select("Key = '" + this.cboSortByField3.Text + "'")[0];
						if(!row.GroupBy) {
							row.SortOrder = k;
							row.Sort = (this.rdoSortByDesc3.Checked) ? "D" : "A";
							k++;
						}
					}
					#endregion
					#region Format
					//Set format, label (caption), width, and alignment
					for(int i=0; i<this.lstAvailableFormatFields.Items.Count; i++) {
						LayoutEntry entry = (LayoutEntry)this.lstAvailableFormatFields.Items[i];
						LayoutDS.LayoutTableRow row = (LayoutDS.LayoutTableRow)layout.LayoutTable.Select("Key = '" + entry.Key + "'")[0];
						row.Format = entry.Format;
						row.Caption = entry.Caption;
						row.Width = entry.Width;
						row.Alignment = entry.Alignment;
					}
					#endregion
					this.mLayoutDS.Clear();
					this.mLayoutDS.Merge(layout);
					this.DialogResult = DialogResult.OK; 
					break;
			}
			this.Close();
		}
		private void OnValidateForm(object sender, System.EventArgs e) {
			//Valiate state of user services
			try {
				this.btnOK.Enabled = true;
			}
			catch(Exception) { }
		}
	}
}
