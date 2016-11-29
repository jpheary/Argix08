//	File:	dlgviews.cs
//	Author:	J. Heary
//	Date:	11/18/05
//	Desc:	Dialog to manage layout views.
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
	public class dlgViews : System.Windows.Forms.Form {
		//Members
		LayoutViews mViews=null;
		LayoutView mSelectedView=null;
		LayoutDS mLayoutDS=null;
		
		//Constants
		
		#region Controls
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.Label _lblTitle;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdViews;
		private Argix.Dispatch.LayoutDS mViewsDS;
		private System.Windows.Forms.GroupBox grpDescription;
		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.Button btnModify;
		private System.Windows.Forms.Button btnRename;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Label lblFields;
		private System.Windows.Forms.Label _lblFields;
		private System.Windows.Forms.Label _lblGroupBy;
		private System.Windows.Forms.Label lblGroupBy;
		private System.Windows.Forms.Label _lblSort;
		private System.Windows.Forms.Label lblSort;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		
		//Interface
		public dlgViews(LayoutViews views) {
			//Required for Windows Form Designer support
			try {
				InitializeComponent();
				this.mViews = views;
				this.mViews.ViewsChanged += new EventHandler(OnViewsChanged);
				this.mLayoutDS = new LayoutDS();
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
		public LayoutDS ViewsData { get { return this.mLayoutDS; } }
		
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ViewTable", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ViewName");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduleName");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Active");
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgViews));
			this.btnClose = new System.Windows.Forms.Button();
			this.btnApply = new System.Windows.Forms.Button();
			this._lblTitle = new System.Windows.Forms.Label();
			this.grdViews = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.mViewsDS = new Argix.Dispatch.LayoutDS();
			this.grpDescription = new System.Windows.Forms.GroupBox();
			this._lblSort = new System.Windows.Forms.Label();
			this.lblSort = new System.Windows.Forms.Label();
			this._lblGroupBy = new System.Windows.Forms.Label();
			this.lblGroupBy = new System.Windows.Forms.Label();
			this._lblFields = new System.Windows.Forms.Label();
			this.lblFields = new System.Windows.Forms.Label();
			this.btnNew = new System.Windows.Forms.Button();
			this.btnCopy = new System.Windows.Forms.Button();
			this.btnModify = new System.Windows.Forms.Button();
			this.btnRename = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.grdViews)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mViewsDS)).BeginInit();
			this.grpDescription.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Location = new System.Drawing.Point(375, 324);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(96, 24);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.OnButtonClick);
			// 
			// btnApply
			// 
			this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnApply.Location = new System.Drawing.Point(273, 324);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(96, 24);
			this.btnApply.TabIndex = 2;
			this.btnApply.Text = "&Apply View";
			this.btnApply.Click += new System.EventHandler(this.OnButtonClick);
			// 
			// _lblTitle
			// 
			this._lblTitle.Location = new System.Drawing.Point(6, 6);
			this._lblTitle.Name = "_lblTitle";
			this._lblTitle.Size = new System.Drawing.Size(288, 18);
			this._lblTitle.TabIndex = 2;
			this._lblTitle.Text = "Views for ";
			this._lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// grdViews
			// 
			this.grdViews.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grdViews.DataMember = "ViewTable";
			this.grdViews.DataSource = this.mViewsDS;
			appearance1.BackColor = System.Drawing.SystemColors.Window;
			appearance1.FontData.Name = "Verdana";
			appearance1.FontData.SizeInPoints = 8F;
			appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
			appearance1.TextHAlign = Infragistics.Win.HAlign.Left;
			this.grdViews.DisplayLayout.Appearance = appearance1;
			ultraGridColumn1.Header.Caption = "View Name";
			ultraGridColumn1.Header.VisiblePosition = 0;
			ultraGridColumn1.Width = 282;
			ultraGridColumn2.Header.VisiblePosition = 1;
			ultraGridColumn2.Hidden = true;
			appearance2.ImageHAlign = Infragistics.Win.HAlign.Center;
			ultraGridColumn3.CellAppearance = appearance2;
			appearance3.TextHAlign = Infragistics.Win.HAlign.Center;
			ultraGridColumn3.Header.Appearance = appearance3;
			ultraGridColumn3.Header.VisiblePosition = 2;
			ultraGridColumn3.Width = 72;
			ultraGridBand1.Columns.AddRange(new object[] {
															 ultraGridColumn1,
															 ultraGridColumn2,
															 ultraGridColumn3});
			this.grdViews.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
			appearance4.BackColor = System.Drawing.SystemColors.InactiveCaption;
			appearance4.FontData.BoldAsString = "True";
			appearance4.FontData.Name = "Verdana";
			appearance4.FontData.SizeInPoints = 8F;
			appearance4.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			appearance4.TextHAlign = Infragistics.Win.HAlign.Left;
			this.grdViews.DisplayLayout.CaptionAppearance = appearance4;
			this.grdViews.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			this.grdViews.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
			this.grdViews.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
			this.grdViews.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
			appearance5.BackColor = System.Drawing.SystemColors.Control;
			appearance5.FontData.BoldAsString = "True";
			appearance5.FontData.Name = "Verdana";
			appearance5.FontData.SizeInPoints = 8F;
			appearance5.TextHAlign = Infragistics.Win.HAlign.Left;
			this.grdViews.DisplayLayout.Override.HeaderAppearance = appearance5;
			this.grdViews.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
			this.grdViews.DisplayLayout.Override.MaxSelectedRows = 1;
			appearance6.BorderColor = System.Drawing.SystemColors.ControlLight;
			this.grdViews.DisplayLayout.Override.RowAppearance = appearance6;
			this.grdViews.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			this.grdViews.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
			this.grdViews.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			this.grdViews.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			this.grdViews.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
			this.grdViews.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
			this.grdViews.Location = new System.Drawing.Point(6, 30);
			this.grdViews.Name = "grdViews";
			this.grdViews.Size = new System.Drawing.Size(357, 144);
			this.grdViews.SupportThemes = false;
			this.grdViews.TabIndex = 3;
			this.grdViews.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnViewSelected);
			// 
			// mViewsDS
			// 
			this.mViewsDS.DataSetName = "LayoutDS";
			this.mViewsDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// grpDescription
			// 
			this.grpDescription.Controls.Add(this._lblSort);
			this.grpDescription.Controls.Add(this.lblSort);
			this.grpDescription.Controls.Add(this._lblGroupBy);
			this.grpDescription.Controls.Add(this.lblGroupBy);
			this.grpDescription.Controls.Add(this._lblFields);
			this.grpDescription.Controls.Add(this.lblFields);
			this.grpDescription.Location = new System.Drawing.Point(6, 180);
			this.grpDescription.Name = "grpDescription";
			this.grpDescription.Size = new System.Drawing.Size(462, 135);
			this.grpDescription.TabIndex = 4;
			this.grpDescription.TabStop = false;
			this.grpDescription.Text = "Description";
			// 
			// _lblSort
			// 
			this._lblSort.Location = new System.Drawing.Point(9, 105);
			this._lblSort.Name = "_lblSort";
			this._lblSort.Size = new System.Drawing.Size(54, 18);
			this._lblSort.TabIndex = 8;
			this._lblSort.Text = "Sort: ";
			this._lblSort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblSort
			// 
			this.lblSort.Location = new System.Drawing.Point(75, 105);
			this.lblSort.Name = "lblSort";
			this.lblSort.Size = new System.Drawing.Size(378, 18);
			this.lblSort.TabIndex = 7;
			this.lblSort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblGroupBy
			// 
			this._lblGroupBy.Location = new System.Drawing.Point(9, 72);
			this._lblGroupBy.Name = "_lblGroupBy";
			this._lblGroupBy.Size = new System.Drawing.Size(54, 18);
			this._lblGroupBy.TabIndex = 6;
			this._lblGroupBy.Text = "GroupBy: ";
			this._lblGroupBy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblGroupBy
			// 
			this.lblGroupBy.Location = new System.Drawing.Point(75, 72);
			this.lblGroupBy.Name = "lblGroupBy";
			this.lblGroupBy.Size = new System.Drawing.Size(378, 18);
			this.lblGroupBy.TabIndex = 5;
			this.lblGroupBy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblFields
			// 
			this._lblFields.Location = new System.Drawing.Point(9, 21);
			this._lblFields.Name = "_lblFields";
			this._lblFields.Size = new System.Drawing.Size(54, 18);
			this._lblFields.TabIndex = 4;
			this._lblFields.Text = "Fields: ";
			this._lblFields.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblFields
			// 
			this.lblFields.Location = new System.Drawing.Point(75, 21);
			this.lblFields.Name = "lblFields";
			this.lblFields.Size = new System.Drawing.Size(378, 32);
			this.lblFields.TabIndex = 3;
			this.lblFields.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnNew
			// 
			this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNew.Location = new System.Drawing.Point(372, 30);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(96, 24);
			this.btnNew.TabIndex = 5;
			this.btnNew.Text = "&New...";
			this.btnNew.Click += new System.EventHandler(this.OnButtonClick);
			// 
			// btnCopy
			// 
			this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCopy.Location = new System.Drawing.Point(372, 60);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(96, 24);
			this.btnCopy.TabIndex = 6;
			this.btnCopy.Text = "&Copy...";
			this.btnCopy.Click += new System.EventHandler(this.OnButtonClick);
			// 
			// btnModify
			// 
			this.btnModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnModify.Location = new System.Drawing.Point(372, 90);
			this.btnModify.Name = "btnModify";
			this.btnModify.Size = new System.Drawing.Size(96, 24);
			this.btnModify.TabIndex = 7;
			this.btnModify.Text = "&Modify...";
			this.btnModify.Click += new System.EventHandler(this.OnButtonClick);
			// 
			// btnRename
			// 
			this.btnRename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRename.Location = new System.Drawing.Point(372, 120);
			this.btnRename.Name = "btnRename";
			this.btnRename.Size = new System.Drawing.Size(96, 24);
			this.btnRename.TabIndex = 8;
			this.btnRename.Text = "&Rename...";
			this.btnRename.Click += new System.EventHandler(this.OnButtonClick);
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDelete.Location = new System.Drawing.Point(372, 150);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(96, 24);
			this.btnDelete.TabIndex = 9;
			this.btnDelete.Text = "&Delete";
			this.btnDelete.Click += new System.EventHandler(this.OnButtonClick);
			// 
			// dlgViews
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(474, 352);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnRename);
			this.Controls.Add(this.btnModify);
			this.Controls.Add(this.btnCopy);
			this.Controls.Add(this.btnNew);
			this.Controls.Add(this.grpDescription);
			this.Controls.Add(this.grdViews);
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this._lblTitle);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgViews";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Custom View Organizer";
			this.Load += new System.EventHandler(this.OnFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.grdViews)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mViewsDS)).EndInit();
			this.grpDescription.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Form load event handler
			try {
				this._lblTitle.Text = "Views for " + this.mViews.ScheduleName + ": ";
				this.grdViews.DataSource = this.mViews.Layouts;
				if(this.grdViews.Rows.Count > 0) this.grdViews.Rows[0].Selected = true;
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		private void OnViewsChanged(object sender, EventArgs e) {
			//Event handler for views changed
			try {
				//
				this.grdViews.Refresh();
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		private void OnViewSelected(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for view selected
			try {
				this.mSelectedView = null;
				if(this.grdViews.Selected.Rows.Count > 0) {
					string viewName = this.grdViews.Selected.Rows[0].Cells["ViewName"].Value.ToString();
					this.mSelectedView = this.mViews.Item(viewName);
					this.mSelectedView.ViewChanged += new EventHandler(OnViewChanged);
					OnViewChanged(null,null);
				}
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		private void OnViewChanged(object sender, EventArgs e) {
			//Event handler for change in a view
			try {
				//Update view descriptions
				this.lblFields.Text = this.lblGroupBy.Text = this.lblSort.Text = "";
				foreach(LayoutEntry entry in this.mSelectedView) {
					if(entry.Visible) {
						if(this.lblFields.Text.Length > 0) this.lblFields.Text += ", ";
						this.lblFields.Text += entry.Caption;
					}
				}
				foreach(LayoutEntry entry in this.mSelectedView) {
					if(entry.GroupBy) {
						if(this.lblGroupBy.Text.Length > 0) this.lblGroupBy.Text += ", ";
						this.lblGroupBy.Text += entry.Caption;
					}
				}
				foreach(LayoutEntry entry in this.mSelectedView) {
					if(entry.Sort != "") {
						if(this.lblSort.Text.Length > 0) this.lblSort.Text += ", ";
						this.lblSort.Text += entry.Caption;
					}
				}
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		private void OnButtonClick(object sender, System.EventArgs e) {
			//Button click event handler
			Button btn = (Button)sender;
			switch(btn.Text) {
				case "&New...":		
					dlgInputBox dlgInp = new dlgInputBox("Name of view:", this.mSelectedView.ViewName, "New View");
					if(dlgInp.ShowDialog(this) == DialogResult.OK) {
						LayoutView view = this.mViews.Item();
						view.ViewName = dlgInp.Value;
						dlgLayout dlgNew = new dlgLayout(view);
						if(dlgNew.ShowDialog(this) == DialogResult.OK) 
							this.mViews.Add(view);
					}
					break;
				case "&Copy...":	break;
				case "&Modify...":	
					dlgLayout dlgMod = new dlgLayout(this.mSelectedView);
					if(dlgMod.ShowDialog(this) == DialogResult.OK) 
						this.mSelectedView.Update(dlgMod.LayoutData);
					break;
				case "&Rename...":	
					dlgInputBox dlgInput = new dlgInputBox("New name of view:", this.mSelectedView.ViewName, "Rename View");
					if(dlgInput.ShowDialog(this) == DialogResult.OK)	
						this.mSelectedView.ViewName = dlgInput.Value;
					break;
				case "&Delete":		
					if(MessageBox.Show(this, "Delete the selected view?", "Delete View", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
						this.mViews.Remove(this.mSelectedView);
					break;
				case "&Apply View":	
					this.mViews.SetActiveView(this.mSelectedView.ViewName);
					break;
				case "&Close":		
					this.Close();
					break;
			}
		}
		private void OnValidateForm(object sender, System.EventArgs e) {
			//Valiate state of user services
			try {
				this.btnNew.Enabled = true;
				this.btnCopy.Enabled = false;
				this.btnModify.Enabled = (this.mSelectedView != null);
				this.btnRename.Enabled = (this.mSelectedView.ViewName != "Default");
				this.btnDelete.Enabled = (this.mSelectedView.ViewName != "Default" && this.mSelectedView.ViewName != this.mViews.ActiveView.ViewName);
				this.btnApply.Enabled = (this.mSelectedView != null);
			}
			catch(Exception) { }
		}
	}
}
