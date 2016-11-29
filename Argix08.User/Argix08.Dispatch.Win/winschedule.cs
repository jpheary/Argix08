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
using Argix.Windows;

namespace Argix.Dispatch {
	//
	public class winSchedule : System.Windows.Forms.Form {
		//Members
		private ScheduleNode mNode=null;
		private DispatchSchedule mSchedule=null;
		private LayoutViews mViews=null;
		private ScheduleEntry mEntry=null;
        private UltraGridSvc mGridSvc=null;
		private bool mIsDragging=false;
		
		#region Constants
		private const string CTX_OPEN = "&Open";
		private const string CTX_PRINT = "&Print";
		private const string CTX_UNDO = "&Undo";
		private const string CTX_CUT = "Cu&t";
		private const string CTX_COPY = "&Copy";
		private const string CTX_PASTE = "&Paste";
		private const string CTX_DELETE = "&Delete";
		private const string CTX_MOVETO = "&Move To Folder...";
		private const string CTX_COPYTO = "&Copy To Folder...";
		
		private const int KEYSTATE_SHIFT = 5;
		private const int KEYSTATE_CTL = 9;
		#endregion
		//Events
		public event EventHandler ServiceStatesChanged=null;
		public event StatusEventHandler StatusMessage=null;
		#region Controls
		private Infragistics.Win.UltraWinGrid.UltraGrid grdSchedule;
		private System.Windows.Forms.ContextMenu ctxSchedule;
		private System.Windows.Forms.MenuItem ctxOpen;
		private System.Windows.Forms.MenuItem ctxPrint;
		private System.Windows.Forms.MenuItem ctxDelete;
		private System.Windows.Forms.MenuItem ctxSep1;
		private System.Windows.Forms.MenuItem ctxSep2;
		private System.Windows.Forms.MenuItem ctxCut;
		private System.Windows.Forms.MenuItem ctxCopy;
		private System.Windows.Forms.MenuItem ctxPaste;
		private System.Windows.Forms.MenuItem ctxSep3;
		private System.Windows.Forms.MenuItem ctxMoveTo;
		private System.Windows.Forms.MenuItem ctxCopyTo;
		private System.Windows.Forms.MenuItem ctxUndo;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		
		//Interface
		public winSchedule(ScheduleNode node, Mediator mediator) {
			//Constructor
			try {
				//Required for designer support
				InitializeComponent();
				#region Menu identities (used for onclick handlers) 
				this.ctxOpen.Text = CTX_OPEN;
				this.ctxPrint.Text = CTX_PRINT;
				this.ctxUndo.Text = CTX_UNDO;
				this.ctxCut.Text = CTX_CUT;
				this.ctxCopy.Text = CTX_COPY;
				this.ctxPaste.Text = CTX_PASTE;
				this.ctxDelete.Text = CTX_DELETE;
				this.ctxMoveTo.Text = CTX_MOVETO;
				this.ctxCopyTo.Text = CTX_COPYTO;
				#endregion

				//Set references and services
				this.mNode = node;
				this.mSchedule = node.Schedule;
				this.mSchedule.ScheduleChanged += new EventHandler(OnScheduleChanged);
				this.mViews = new LayoutViews(this.mSchedule.ScheduleName, mediator);
				this.mViews.ViewsChanged += new EventHandler(OnViewsChanged);
				this.mViews.ActiveView.ViewChanged += new EventHandler(OnViewChanged);
                this.mGridSvc = new UltraGridSvc(this.grdSchedule);
				this.Text = this.mSchedule.ScheduleName;
			}
			catch(Exception ex) { throw ex; }
		}
		protected override void Dispose( bool disposing ) {
			//Clean up any resources being used
			if(disposing) {
				if(components != null) 
					components.Dispose();
			}
			base.Dispose(disposing);
		}
		public DispatchSchedule Schedule { get { return this.mSchedule; } }
		public ScheduleEntry SelectedEntry { get { return this.mEntry; } }
		public override void Refresh() { this.mSchedule.Refresh(); }
		#region FILE Menu: New(), Open(), SaveAs(), PageSettings(), Print()
		public bool CanNew { get { return true; } }
		public void New() { 
			//Create a new entry
			ScheduleEntry entry = this.mSchedule.Item();
			dlgSchedule dlg = this.mSchedule.ScheduleDialog(entry);
			dlg.StatusMessage += new StatusEventHandler(OnStatusMessage);
			if(dlg.ShowDialog(this) == DialogResult.OK) 
				this.mSchedule.Add(entry);
		}
		public bool CanOpen { get { return (this.ctxOpen.Enabled); } }
		public void Open() { 
			//Open the selected entry for update
			dlgSchedule dlg = this.mSchedule.ScheduleDialog(this.mEntry);
			dlg.StatusMessage += new StatusEventHandler(OnStatusMessage);
			if(dlg.ShowDialog(this) == DialogResult.OK) 
				this.mEntry.Update();
		}
		public bool CanSaveAs { get { return this.grdSchedule.Selected.Rows.Count > 0; } }
		public void SaveAs(string fileName) { this.mEntry.ToDataSet().WriteXml(fileName); }
		public void PageSettings() { UltraGridSvc.PageSettings(); }
		public bool CanPrint { get { return (this.ctxPrint.Enabled); } }
		public void Print() { UltraGridSvc.Print(this.grdSchedule, true); }
		#endregion
		#region Edit Menu: Undo(), Cut(), Copy(), Paste(), Delete(), MoveToFolder(), CopyToFolder()
		public bool CanUndo { get { return this.ctxUndo.Enabled; } }
		public void Undo() { }
		public bool CanCut { get { return this.ctxCut.Enabled; } }
		public void Cut() { }
		public bool CanCopy { get { return this.ctxCopy.Enabled; } }
		public void Copy() { }
		public bool CanPaste { get { return this.ctxPaste.Enabled; } }
		public void Paste() { }
		public bool CanDelete { get { return (this.ctxDelete.Enabled); } }
		public void Delete() { this.mEntry.Delete(); }
		public bool CanMoveToFolder { get { return (this.grdSchedule.Selected.Rows.Count > 0); } }
		public void MoveToFolder() { 
			//Move the selected entries to another schedule
			dlgMoveEntry dlgMoveTo = new dlgMoveEntry();
			if(dlgMoveTo.ShowDialog(this) == DialogResult.OK) {
				//Package data
				DispatchDS ds = new DispatchDS();
				for(int i=0; i<this.grdSchedule.Selected.Rows.Count; i++) {
					int ID = Convert.ToInt32(this.grdSchedule.Selected.Rows[i].Cells["ID"].Value);
					ScheduleEntry entry = this.mSchedule.Item(ID);
					ds.Merge(entry.ToDataSet());
				}
				
				//Get destination schedule and move data
				DispatchSchedule schedule = dlgMoveTo.Schedule;
				schedule.AddList(ds);
				this.mSchedule.RemoveList(ds);
			}
		}
		public bool CanCopyToFolder { get { return (this.grdSchedule.Selected.Rows.Count > 0); } }
		public void CopyToFolder() { 
			//Copy the selected entries to another schedule
			dlgMoveEntry dlgCopyTo = new dlgMoveEntry();
			if(dlgCopyTo.ShowDialog(this) == DialogResult.OK)  {
				//Package data
				DispatchDS ds = new DispatchDS();
				for(int i=0; i<this.grdSchedule.Selected.Rows.Count; i++) {
					int ID = Convert.ToInt32(this.grdSchedule.Selected.Rows[i].Cells["ID"].Value);
					ScheduleEntry entry = this.mSchedule.Item(ID);
					ds.Merge(entry.ToDataSet());
				}
				
				//Get destination schedule and copy data
				DispatchSchedule schedule = dlgCopyTo.Schedule;
				schedule.AddList(ds);
			}
		}
		#endregion
		#region View Menu: ViewMenuItems(), DefineViews(), OnCurrentViewChanged(), CustomizeCurrentView()
		public MenuItem[] ViewMenuItems { 
			get { 
				MenuItem[] menus=null;
				try {
					LayoutDS views = this.mViews.Layouts;
					menus = new MenuItem[this.mViews.Layouts.ViewTable.Rows.Count];
					for(int i=0; i<this.mViews.Layouts.ViewTable.Rows.Count; i++) {
						MenuItem menu = new MenuItem(this.mViews.Layouts.ViewTable[i].ViewName, new EventHandler(OnCurrentViewChanged));
						menu.Checked = this.mViews.Layouts.ViewTable[i].Active;
						menus[i] = menu;
					}
				}
				catch(Exception ex) { reportError(ex); }
				return menus; 
			} 
		}
		private void OnCurrentViewChanged(object sender, EventArgs e) {
			//Event handler for change in current view
			try {
				MenuItem menu = (MenuItem)sender;
				if(menu.Text != this.mViews.ActiveView.ViewName) {
					//Check new menu selection, and set new active view
					for(int i=0; i<menu.Parent.MenuItems.Count; i++) 
						menu.Parent.MenuItems[i].Checked = false;
					menu.Checked = true;
					this.mViews.SetActiveView(menu.Text);
					this.mViews.ActiveView.ViewChanged -= new EventHandler(OnViewChanged);
					this.mViews.ActiveView.ViewChanged += new EventHandler(OnViewChanged);
				}
			}
			catch(Exception ex) { reportError(ex); }
		}
		public void DefineViews() { 
			//Allow user management of the views for this schedule
			try {
				dlgViews dlg = new dlgViews(this.mViews);
				dlg.ShowDialog(this);
			}
			catch(Exception ex) { reportError(ex); }
		}
		private void OnViewsChanged(object sender, EventArgs e) {
			//Event handler for change in view layouts collection
			try {
				this.mViews.ActiveView.ViewChanged -= new EventHandler(OnViewChanged);
				this.mViews.ActiveView.ViewChanged += new EventHandler(OnViewChanged);
				OnViewChanged(null,null);
			}
			catch(Exception ex) { reportError(ex); }
		}
		public void CustomizeCurrentView() { 
			//Allow user customization of the current view
			try {
				dlgLayout dlg = new dlgLayout(this.mViews.ActiveView);
				if(dlg.ShowDialog(this) == DialogResult.OK) 
					this.mViews.ActiveView.Update(dlg.LayoutData);
			}
			catch(Exception ex) { reportError(ex); }
		}
		private void OnViewChanged(object sender, EventArgs e) { 
			//Event handler for change in active view
			try {
				setLayout();
			}
			catch(Exception ex) { reportError(ex); }
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(winSchedule));
			this.grdSchedule = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.ctxSchedule = new System.Windows.Forms.ContextMenu();
			this.ctxOpen = new System.Windows.Forms.MenuItem();
			this.ctxSep1 = new System.Windows.Forms.MenuItem();
			this.ctxPrint = new System.Windows.Forms.MenuItem();
			this.ctxSep2 = new System.Windows.Forms.MenuItem();
			this.ctxUndo = new System.Windows.Forms.MenuItem();
			this.ctxCut = new System.Windows.Forms.MenuItem();
			this.ctxCopy = new System.Windows.Forms.MenuItem();
			this.ctxPaste = new System.Windows.Forms.MenuItem();
			this.ctxSep3 = new System.Windows.Forms.MenuItem();
			this.ctxDelete = new System.Windows.Forms.MenuItem();
			this.ctxMoveTo = new System.Windows.Forms.MenuItem();
			this.ctxCopyTo = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.grdSchedule)).BeginInit();
			this.SuspendLayout();
			// 
			// grdSchedule
			// 
			this.grdSchedule.ContextMenu = this.ctxSchedule;
			appearance1.BackColor = System.Drawing.SystemColors.Window;
			appearance1.FontData.Name = "Verdana";
			appearance1.FontData.SizeInPoints = 8F;
			appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
			appearance1.TextHAlign = Infragistics.Win.HAlign.Left;
			this.grdSchedule.DisplayLayout.Appearance = appearance1;
			appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
			appearance2.FontData.BoldAsString = "True";
			appearance2.FontData.Name = "Verdana";
			appearance2.FontData.SizeInPoints = 8F;
			appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			appearance2.TextHAlign = Infragistics.Win.HAlign.Left;
			this.grdSchedule.DisplayLayout.CaptionAppearance = appearance2;
			this.grdSchedule.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			this.grdSchedule.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
			this.grdSchedule.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
			this.grdSchedule.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
			appearance3.BackColor = System.Drawing.SystemColors.Control;
			appearance3.FontData.BoldAsString = "True";
			appearance3.FontData.Name = "Verdana";
			appearance3.FontData.SizeInPoints = 8F;
			appearance3.TextHAlign = Infragistics.Win.HAlign.Left;
			this.grdSchedule.DisplayLayout.Override.HeaderAppearance = appearance3;
			this.grdSchedule.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
			this.grdSchedule.DisplayLayout.Override.MaxSelectedRows = 1;
			appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
			this.grdSchedule.DisplayLayout.Override.RowAppearance = appearance4;
			this.grdSchedule.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			this.grdSchedule.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
			this.grdSchedule.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			this.grdSchedule.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			this.grdSchedule.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
			this.grdSchedule.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
			this.grdSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdSchedule.Location = new System.Drawing.Point(0, 0);
			this.grdSchedule.Name = "grdSchedule";
			this.grdSchedule.Size = new System.Drawing.Size(448, 269);
			this.grdSchedule.SupportThemes = false;
			this.grdSchedule.TabIndex = 1;
			this.grdSchedule.Text = "Schedule";
			this.grdSchedule.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
			this.grdSchedule.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.OnQueryContinueDrag);
			this.grdSchedule.DragOver += new System.Windows.Forms.DragEventHandler(this.OnDragOver);
			this.grdSchedule.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseMove);
			this.grdSchedule.DoubleClick += new System.EventHandler(this.OnGridDoubleClicked);
			this.grdSchedule.DragLeave += new System.EventHandler(this.OnDragLeave);
			this.grdSchedule.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
			this.grdSchedule.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridSelectionChanged);
			this.grdSchedule.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
			this.grdSchedule.SelectionDrag += new System.ComponentModel.CancelEventHandler(this.OnSelectionDrag);
			this.grdSchedule.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseUp);
			// 
			// ctxSchedule
			// 
			this.ctxSchedule.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.ctxOpen,
																						this.ctxSep1,
																						this.ctxPrint,
																						this.ctxSep2,
																						this.ctxUndo,
																						this.ctxCut,
																						this.ctxCopy,
																						this.ctxPaste,
																						this.ctxSep3,
																						this.ctxDelete,
																						this.ctxMoveTo,
																						this.ctxCopyTo});
			// 
			// ctxOpen
			// 
			this.ctxOpen.Index = 0;
			this.ctxOpen.Text = "Open";
			this.ctxOpen.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxSep1
			// 
			this.ctxSep1.Index = 1;
			this.ctxSep1.Text = "-";
			// 
			// ctxPrint
			// 
			this.ctxPrint.Index = 2;
			this.ctxPrint.Text = "Print";
			this.ctxPrint.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxSep2
			// 
			this.ctxSep2.Index = 3;
			this.ctxSep2.Text = "-";
			// 
			// ctxUndo
			// 
			this.ctxUndo.Index = 4;
			this.ctxUndo.Text = "Undo";
			this.ctxUndo.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxCut
			// 
			this.ctxCut.Index = 5;
			this.ctxCut.Text = "Cut";
			this.ctxCut.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxCopy
			// 
			this.ctxCopy.Index = 6;
			this.ctxCopy.Text = "Copy";
			this.ctxCopy.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxPaste
			// 
			this.ctxPaste.Index = 7;
			this.ctxPaste.Text = "Paste";
			this.ctxPaste.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxSep3
			// 
			this.ctxSep3.Index = 8;
			this.ctxSep3.Text = "-";
			// 
			// ctxDelete
			// 
			this.ctxDelete.Index = 9;
			this.ctxDelete.Text = "Delete";
			this.ctxDelete.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxMoveTo
			// 
			this.ctxMoveTo.Index = 10;
			this.ctxMoveTo.Text = "Move To Folder";
			this.ctxMoveTo.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxCopyTo
			// 
			this.ctxCopyTo.Index = 11;
			this.ctxCopyTo.Text = "Copy To Folder";
			this.ctxCopyTo.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// winSchedule
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(448, 269);
			this.Controls.Add(this.grdSchedule);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "winSchedule";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Resize += new System.EventHandler(this.OnFormResize);
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.Activated += new System.EventHandler(this.OnFormActivated);
			this.Deactivate += new System.EventHandler(this.OnFormDeactivated);
			((System.ComponentModel.ISupportInitialize)(this.grdSchedule)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Load conditions
			this.Cursor = Cursors.WaitCursor;
			try {
				//Set grid layout for this schedule, and refresh schedule
				setLayout();
				this.mSchedule.Refresh();
			}
			catch(Exception ex) { reportError(ex); }
			finally { 
				setUserServices();
				this.Cursor = Cursors.Default; 
			}			
		}
		private void OnFormActivated(object sender, System.EventArgs e) { this.mNode.TreeView.SelectedNode = this.mNode; }
		private void OnFormDeactivated(object sender, System.EventArgs e) { }
		private void OnFormResize(object sender, System.EventArgs e) { }
		private void OnFormClosing(object sender, System.ComponentModel.CancelEventArgs e) { }
		private void OnScheduleChanged(object sender, EventArgs e) {
			//Event handler for change in schedule
			int iIndex=0;
			this.Cursor = Cursors.WaitCursor;
			try {
				//Configure and update device view
				sendStatusMessage(new StatusEventArgs("Loading schedule..."));
				iIndex = (this.grdSchedule.Selected.Rows.Count > 0) ? this.grdSchedule.Selected.Rows[0].VisibleIndex : 0;
				this.grdSchedule.DisplayLayout.Bands[0].SortedColumns.RefreshSort(true);
				if(this.grdSchedule.Rows.VisibleRowCount > 0) {
					if(iIndex >= 0 && iIndex < this.grdSchedule.Rows.VisibleRowCount) 
						this.grdSchedule.Rows.GetRowAtVisibleIndex(iIndex).Selected = true;
					else
						this.grdSchedule.Rows.GetRowAtVisibleIndex(0).Selected = true;
					this.grdSchedule.Selected.Rows[0].Activate();
				}
				else
					OnGridSelectionChanged(this.grdSchedule, null);
				sendStatusMessage(new StatusEventArgs(""));
			}
			catch(Exception ex) { reportError(ex); }
			finally { 
				setUserServices();
				this.Cursor = Cursors.Default; 
			}
		}
		#region Grid Support: GridSelectionChanged, GridMouseDown, GridDoubleClick
		private void OnGridSelectionChanged(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for after selection changes
			try {
				//Select grid and forward to update
				this.mEntry = null;
				if(this.grdSchedule.Selected.Rows.Count > 0) {
					if(!this.grdSchedule.Selected.Rows[0].IsGroupByRow) {
						int ID = Convert.ToInt32(this.grdSchedule.Selected.Rows[0].Cells["ID"].Value);
						this.mEntry = this.mSchedule.Item(ID);
					}
				}
			} 
			catch(Exception) { }
			finally { setUserServices(); }
		}
		private void OnGridMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Event handler for mouse down event
			try {
				//Check drag\drop
				OnDragDropMouseDown(sender, e);
				
				//Set menu and toolbar services
				UltraGrid oGrid = (UltraGrid)sender;
				UIElement oUIElement = oGrid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));
				object oContext=null;
				if(e.Button == MouseButtons.Right) {
					oContext = oUIElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridCell));
					if(oContext != null) {
						//On row
						UltraGridCell oCell = (UltraGridCell)oContext;
						oGrid.ActiveRow = oCell.Row;
						oGrid.ActiveRow.Selected = true;
					}
					else {
						//Off row
						oContext = oUIElement.GetContext(typeof(RowScrollRegion));
						if(oContext != null) {
							oGrid.ActiveRow = null;
							if(oGrid.Selected.Rows.Count > 0) oGrid.Selected.Rows[0].Selected = false;
						}
					}
					oGrid.Focus();
				}
				else if(e.Button == MouseButtons.Left) {
					//Remove selected item if scrolling
					oContext = oUIElement.GetContext(typeof(RowScrollRegion));
					if(oContext != null) {
						oGrid.ActiveRow = null;
						if(oGrid.Selected.Rows.Count > 0) oGrid.Selected.Rows[0].Selected = false;
					}
				}
			} 
			catch(Exception) { }
			finally { setUserServices(); }
		}
		private void OnGridDoubleClicked(object sender, System.EventArgs e) {
			//Event handler for double-click event
			try {
				//Select grid and forward to update
				if(this.ctxOpen.Enabled) this.ctxOpen.PerformClick();
			} 
			catch(Exception) { }
		}
		#endregion
		#region User Services: OnMenuClick(), OnStatusMessage()
		private void OnMenuClick(object sender, System.EventArgs e) {
			//Menu itemclicked-apply selected service
			try  {
				MenuItem menu = (MenuItem)sender;
				switch(menu.Text) {
					case CTX_OPEN:		Open(); break;
					case CTX_PRINT:		Print(); break;
					case CTX_UNDO:		Undo(); break;
					case CTX_CUT:		Cut(); break;
					case CTX_COPY:		Copy(); break;
					case CTX_PASTE:		Paste(); break;
					case CTX_DELETE:	Delete(); break;
					case CTX_MOVETO:	MoveToFolder(); break;
					case CTX_COPYTO:	CopyToFolder(); break;
				}
			}
			catch(Exception ex) { reportError(ex); }
			finally { 
				setUserServices();
				this.Cursor = Cursors.Default; 
			}		
		}
		private void OnStatusMessage(object source, StatusEventArgs e) {
			//Event hanlder for status messages from dialog
			sendStatusMessage(e);
		}
		#endregion
		#region Local Services: setUserServices(), reportError(), sendStatusMessage(), setLayout()
		private void setUserServices() {
			//Set user services depending upon the selected entry
			try {
				//Determine state for the selected entry (if applicable)
				this.ctxOpen.Enabled = (this.mEntry != null);
				this.ctxPrint.Enabled = (this.grdSchedule.Selected.Rows.Count > 0);
				this.ctxUndo.Enabled = false;
				this.ctxCut.Enabled = false;
				this.ctxCopy.Enabled = false;
				this.ctxPaste.Enabled = false;
				this.ctxDelete.Enabled = (this.mEntry != null);
				this.ctxMoveTo.Enabled = (this.grdSchedule.Selected.Rows.Count > 0);
				this.ctxCopyTo.Enabled = (this.grdSchedule.Selected.Rows.Count > 0);
			}
			catch(Exception ex) { reportError(ex); }
			finally { if(this.ServiceStatesChanged != null) this.ServiceStatesChanged(this, new EventArgs()); }
		}
		private void reportError(Exception ex) { sendStatusMessage(new StatusEventArgs(ex.Message)); }		
		private void sendStatusMessage(StatusEventArgs e) { if(this.StatusMessage!=null) this.StatusMessage(this, e); }
		private void setLayout() {
			//Set grid column layout
			try {
				#region A Hack
				this.Controls.Clear();
				Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
				Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
				Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
				Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
				this.grdSchedule = new Infragistics.Win.UltraWinGrid.UltraGrid();
				((System.ComponentModel.ISupportInitialize)(this.grdSchedule)).BeginInit();
				this.SuspendLayout();
				this.grdSchedule.ContextMenu = this.ctxSchedule;
				appearance1.BackColor = System.Drawing.SystemColors.Window;
				appearance1.FontData.Name = "Verdana";
				appearance1.FontData.SizeInPoints = 8F;
				appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
				appearance1.TextHAlign = Infragistics.Win.HAlign.Left;
				this.grdSchedule.DisplayLayout.Appearance = appearance1;
				appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
				appearance2.FontData.BoldAsString = "True";
				appearance2.FontData.Name = "Verdana";
				appearance2.FontData.SizeInPoints = 8F;
				appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
				appearance2.TextHAlign = Infragistics.Win.HAlign.Left;
				this.grdSchedule.DisplayLayout.CaptionAppearance = appearance2;
				this.grdSchedule.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
				this.grdSchedule.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
				this.grdSchedule.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
				this.grdSchedule.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
				appearance3.BackColor = System.Drawing.SystemColors.Control;
				appearance3.FontData.BoldAsString = "True";
				appearance3.FontData.Name = "Verdana";
				appearance3.FontData.SizeInPoints = 8F;
				appearance3.TextHAlign = Infragistics.Win.HAlign.Left;
				this.grdSchedule.DisplayLayout.Override.HeaderAppearance = appearance3;
				this.grdSchedule.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
				this.grdSchedule.DisplayLayout.Override.MaxSelectedRows = 1;
				appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
				this.grdSchedule.DisplayLayout.Override.RowAppearance = appearance4;
				this.grdSchedule.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
				this.grdSchedule.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
				this.grdSchedule.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
				this.grdSchedule.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
				this.grdSchedule.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
				this.grdSchedule.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
				this.grdSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
				this.grdSchedule.Location = new System.Drawing.Point(0, 0);
				this.grdSchedule.Name = "grdSchedule";
				this.grdSchedule.Size = new System.Drawing.Size(448, 269);
				this.grdSchedule.SupportThemes = false;
				this.grdSchedule.TabIndex = 1;
				this.grdSchedule.Text = "Schedule";
				this.grdSchedule.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
				this.grdSchedule.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.OnQueryContinueDrag);
				this.grdSchedule.DragOver += new System.Windows.Forms.DragEventHandler(this.OnDragOver);
				this.grdSchedule.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseMove);
				this.grdSchedule.DoubleClick += new System.EventHandler(this.OnGridDoubleClicked);
				this.grdSchedule.DragLeave += new System.EventHandler(this.OnDragLeave);
				this.grdSchedule.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
				this.grdSchedule.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridSelectionChanged);
				this.grdSchedule.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
				this.grdSchedule.SelectionDrag += new System.ComponentModel.CancelEventHandler(this.OnSelectionDrag);
				this.grdSchedule.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseUp);
				this.Controls.Add(this.grdSchedule);
				((System.ComponentModel.ISupportInitialize)(this.grdSchedule)).EndInit();
				this.ResumeLayout(false);
				#endregion
				((System.ComponentModel.ISupportInitialize)(this.grdSchedule)).BeginInit();
				this.grdSchedule.DataMember = this.mSchedule.ScheduleTableName;
				this.grdSchedule.DataSource = this.mSchedule.Schedule;
				UltraGridBand band = new UltraGridBand(this.mSchedule.ScheduleTableName, -1);
				object[] o = new object[this.mViews.ActiveView.Count];
				int i=0;
				foreach(LayoutEntry entry in this.mViews.ActiveView) {
					//Layout grid columns
					UltraGridColumn col=null;
					HAlign align = HAlign.Default;
					switch(entry.Alignment) {
						case "L": align = HAlign.Left; break;
						case "C": align = HAlign.Center; break;
						case "R": align = HAlign.Right; break;
					}
					SortIndicator sortDir = SortIndicator.None;
					if(entry.Sort != "") sortDir = (entry.Sort == "A") ? SortIndicator.Ascending : SortIndicator.Descending;
					
					col = (entry.GroupBy || sortDir != SortIndicator.None) ? new UltraGridColumn(entry.Key, -1, null, entry.SortOrder, sortDir, entry.GroupBy) : new UltraGridColumn(entry.Key);
					col.Header.VisiblePosition = entry.VisiblePosition;
					col.Hidden = !entry.Visible;
					col.HiddenWhenGroupBy = entry.Visible ? DefaultableBoolean.False : DefaultableBoolean.True;
					col.Header.Caption = entry.Caption;
					col.Width = entry.Width;
					if(entry.Format != "") col.Format = entry.Format;
					Infragistics.Win.Appearance a = new Infragistics.Win.Appearance();
					a.TextHAlign = align;
					col.Header.Appearance = col.CellAppearance = a;
					col.NullText = entry.NullText;
					o[i++] = col;
				}
				band.Columns.AddRange(o);
				this.grdSchedule.AllowDrop = true;
				this.grdSchedule.DisplayLayout.Override.SelectTypeRow = SelectType.ExtendedAutoDrag;
				this.grdSchedule.DisplayLayout.Override.MaxSelectedRows = -1;
				this.grdSchedule.DisplayLayout.BandsSerializer.Add(band);
				this.grdSchedule.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortMulti;
				this.grdSchedule.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
				this.grdSchedule.DisplayLayout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
				this.grdSchedule.Text = this.mSchedule.ScheduleName;
				((System.ComponentModel.ISupportInitialize)(this.grdSchedule)).EndInit();
				
				this.grdSchedule.Size = this.ClientSize;
				//this.grdSchedule.Dock = DockStyle.Fill;
				this.grdSchedule.Refresh();
				this.Refresh();
			}
			catch(Exception ex) { reportError(ex); }
		}
		#endregion
		#region Drag/Drop: OnDragDropMouseDown(), OnDragDropMouseMove(), OnDragDropMouseUp(), ...
		private void OnDragDropMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) { 
			if(this.grdSchedule.Selected.Rows.Count > 0) 
				this.mIsDragging = (e.Button==MouseButtons.Left && !this.grdSchedule.Selected.Rows[0].IsGroupByRow);
		}
		private void OnDragDropMouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Start drag\drop if user is dragging
			DataObject oData=null;
			try {
				switch(e.Button) {
					case MouseButtons.Left:
						if(this.mIsDragging) {
							//Initiate drag drop operation from this schedule
							DispatchDS ds = new DispatchDS();
							for(int i=0; i<this.grdSchedule.Selected.Rows.Count; i++) {
								int ID = Convert.ToInt32(this.grdSchedule.Selected.Rows[i].Cells["ID"].Value);
								ScheduleEntry entry = this.mSchedule.Item(ID);
								ds.Merge(entry.ToDataSet());
							}
							oData = new DataObject();
							oData.SetData(ds);
							DragDropEffects effect = this.grdSchedule.DoDragDrop(oData, DragDropEffects.All);
							
							//After the drop- remove from this schedule on move
							switch(effect) {
								case DragDropEffects.Move:	this.mSchedule.RemoveList(ds); break;
								case DragDropEffects.Copy:	break;
							}
						}
						break;
				}
			}
			catch(Exception ex) { reportError(ex); }
		}
		private void OnDragDropMouseUp(object sender, System.Windows.Forms.MouseEventArgs e) { this.mIsDragging = false; }
		private void OnDragDrop(object sender, System.Windows.Forms.DragEventArgs e) {
			//Event handler for dropping onto the window
			try {
				DataObject oData = (DataObject)e.Data;
				if(oData.GetDataPresent(DataFormats.Serializable, true)) {
					DispatchDS ds = (DispatchDS)oData.GetData(DataFormats.Serializable);
					this.mSchedule.AddList(ds);
				}
			}
			catch(Exception ex) { reportError(ex); }
		}
		private void OnDragEnter(object sender, System.Windows.Forms.DragEventArgs e) {
			//Event handler for dragging into the window
			try {
				//Enable appropriate drag drop effect
				//NOTE: Cannot MOVE to self; default to COPY if this is self
				DataObject oData = (DataObject)e.Data;
				if(oData.GetDataPresent(DataFormats.Serializable, true)) {
					switch(e.KeyState) {
						case KEYSTATE_SHIFT:	e.Effect = (this.mIsDragging) ? DragDropEffects.None : DragDropEffects.Move; break;
						case KEYSTATE_CTL:		e.Effect = DragDropEffects.Copy; break;
						default:				e.Effect = (this.mIsDragging) ? DragDropEffects.None : DragDropEffects.Copy; break;
					}
				}
				else
					e.Effect = DragDropEffects.None;
			}
			catch(Exception ex) { reportError(ex); }
		}
		private void OnDragLeave(object sender, System.EventArgs e) { }
		private void OnDragOver(object sender, System.Windows.Forms.DragEventArgs e) {
			//Event handler for dragging over the window
			try {
				//Enable appropriate drag drop effect
				//NOTE: Cannot MOVE to self; default to COPY if this is self
				DataObject oData = (DataObject)e.Data;
				if(oData.GetDataPresent(DataFormats.Serializable, true)) {
					switch(e.KeyState) {
						case KEYSTATE_SHIFT:	e.Effect = (this.mIsDragging) ? DragDropEffects.None : e.Effect = DragDropEffects.Move; break;
						case KEYSTATE_CTL:		e.Effect = DragDropEffects.Copy; break;
						default:				e.Effect = (this.mIsDragging) ? DragDropEffects.None : DragDropEffects.Copy; break;
					}
				}
				else
					e.Effect = DragDropEffects.None;
			}
			catch(Exception ex) { reportError(ex); }
		}
		private void OnQueryContinueDrag(object sender, System.Windows.Forms.QueryContinueDragEventArgs e) { if(!this.mIsDragging) e.Action = DragAction.Cancel; }
		private void OnSelectionDrag(object sender, System.ComponentModel.CancelEventArgs e) { e.Cancel = !this.mIsDragging; }
		#endregion
	}
}
