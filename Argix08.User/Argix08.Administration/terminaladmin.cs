//	File:	terminaladmin.cs
//	Author:	J. Heary
//	Date:	04/27/06
//	Desc:	Class definitions to administer Transportation entities.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using Tsort.Data;
using Tsort.Enterprise;
using Tsort.Freight;
using Tsort.Transportation;

namespace Tsort {
	//Implementation Classes
	#region DriverAdminNode
	//---------------------------------------------------------------------------
	//The DriverAdminNode administers the collection of enterprsie drivers
	public class DriverAdminNode: AdminNode {
		//Members
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Driver";
		private const string MNU_EDIT = "Update Driver";
		private const string MNU_REMOVE = "Delete Driver";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public DriverAdminNode(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) {
			//Constructor
			try {
				//Create a listview service menu
				base.mnuLSV = new ContextMenu();
				base.mnuLSVOpen = new MenuItem(MNU_OPEN, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep0 = new MenuItem("-");
				base.mnuLSVAdd = new MenuItem(MNU_ADD, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVEdit = new MenuItem(MNU_EDIT, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVRemove = new MenuItem(MNU_REMOVE, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep1 = new MenuItem("-");
				base.mnuLSVProps = new MenuItem(MNU_PROPS, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSV.MenuItems.AddRange(new MenuItem[] {base.mnuLSVOpen, base.mnuLSVSep0, base.mnuLSVAdd, base.mnuLSVEdit, base.mnuLSVRemove, base.mnuLSVSep1, base.mnuLSVProps});
				base.mnuLSVOpen.Enabled = false; base.mnuLSVAdd.Enabled = true; base.mnuLSVEdit.Enabled = base.mnuLSVRemove.Enabled = false; base.mnuLSVProps.Enabled = false;
			}
			catch(Exception ex) { throw(ex); }
		}
				
		//Listview support --------------------------------------------------
		public override ColumnHeader[] header() { 
			//Return listview column headers for base object
			ColumnHeader[] headers = null;
			ColumnHeader colLastName = new ColumnHeader(); colLastName.Text = "Last Name"; colLastName.Width = 144;
			ColumnHeader colFirstName = new ColumnHeader(); colFirstName.Text = "First Name"; colFirstName.Width = 96;
			ColumnHeader colPhone = new ColumnHeader(); colPhone.Text = "Phone #"; colPhone.Width = 96;
			ColumnHeader colTerm = new ColumnHeader(); colTerm.Text = "Terminal"; colTerm.Width = 168;
			ColumnHeader colIsActive = new ColumnHeader(); colIsActive.Text = "Active"; colIsActive.Width = 60;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "DriverID"; colID.Width = 0;
			headers = new ColumnHeader[] {colLastName, colFirstName, colPhone, colTerm, colIsActive, colID};
			return headers; 
		}
		public override ListViewItem[] list() { 
			//Return listview items (data) for base object
			DriverDS lst = TransportationFactory.ViewDrivers();
			ListViewItem[] items = new ListViewItem[lst.DriverListTable.Rows.Count];
			for(int i=0; i<lst.DriverListTable.Rows.Count; i++) {
				string[] subitems = {	lst.DriverListTable[i].LastName.Trim(), 
										lst.DriverListTable[i].FirstName.Trim(), 
										lst.DriverListTable[i].Phone, 
										lst.DriverListTable[i].Terminal.Trim(), 
										lst.DriverListTable[i].IsActive.ToString(), 
										lst.DriverListTable[i].DriverID.ToString() };
				ListViewItem item = new ListViewItem(subitems, 2);
				items[i] = item;
			}
			return items;
		}
		
		protected override void OnItemMenuClick(object sender, System.EventArgs e) {
			//Context menu event handler
			MenuItem menu;
			DriverDS dsDriver;
			dlgDriverDetail dlgDriver;
			int driverID = 0;
			string driverName = "";
			DialogResult res;
			bool val;
			
			try {
				//Context menu item clicked-apply selected service; forward to menu handler
				menu = (MenuItem)sender;
				switch(menu.Text)  {
					case MNU_OPEN:		
						base.mnuLSVEdit.PerformClick();
						break;
					case MNU_ADD:		
						//Read new driver details (defaults), forward to dlgDriver for update
						driverID = 0;
						dsDriver = TransportationFactory.GetDriver(driverID);
						dlgDriver = new dlgDriverDetail(ref dsDriver);
						res = dlgDriver.ShowDialog();
						if(res==DialogResult.OK) {
							//Request a new driver
							driverID = TransportationFactory.CreateDriver(dsDriver);
							if(driverID>0) {
								driverName = dsDriver.DriverDetailTable[0].FirstName + " " + dsDriver.DriverDetailTable[0].LastName;
								MessageBox.Show("Driver " + driverName + " was created.", "Driver Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show(" New driver could not be created. Please try again.", "Driver Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_EDIT:		
						//Read existing driver details, forward to dlgDriver for update
						driverID = Convert.ToInt32(mCurrentItem.SubItems[5].Text);
						dsDriver = TransportationFactory.GetDriver(driverID);
						dlgDriver = new dlgDriverDetail(ref dsDriver);
						res = dlgDriver.ShowDialog();
						if(res==DialogResult.OK) {
							//Request an update to the driver
							val = TransportationFactory.UpdateDriver(dsDriver);
							driverName = dsDriver.DriverDetailTable[0].FirstName + " " + dsDriver.DriverDetailTable[0].LastName;
							if(val) {
								MessageBox.Show("Driver " + driverName + " was updated.", "Driver Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Driver " + driverName + " could not be updated. Please try again.", "Driver Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_REMOVE:	break;
					case MNU_PROPS:		break;
					default:			break;
				}
			}
			catch(Exception ex) { base.OnErrorMessage(ex); }
		}
	}
	#endregion
	#region EntTerminalAdminNode
	//---------------------------------------------------------------------------
	//The EntTerminalAdminNode administers a collection of enterprise terminals
	public class EntTerminalAdminNode: AdminNode {
		//Attributes		
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Terminal";
		private const string MNU_EDIT = "Update Terminal";
		private const string MNU_REMOVE = "Delete Terminal";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public EntTerminalAdminNode(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) {
			//Constructor
			try {
				//Create a custom listview service menu
				base.mnuLSV = new ContextMenu();
				base.mnuLSVOpen = new MenuItem(MNU_OPEN, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep0 = new MenuItem("-");
				base.mnuLSVAdd = new MenuItem(MNU_ADD, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVEdit = new MenuItem(MNU_EDIT, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVRemove = new MenuItem(MNU_REMOVE, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep1 = new MenuItem("-");
				base.mnuLSVProps = new MenuItem(MNU_PROPS, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSV.MenuItems.AddRange(new MenuItem[] {base.mnuLSVOpen, base.mnuLSVSep0, base.mnuLSVAdd, base.mnuLSVEdit, base.mnuLSVRemove, base.mnuLSVSep1, base.mnuLSVProps});
				base.mnuLSVOpen.Enabled = false; base.mnuLSVAdd.Enabled = true; base.mnuLSVEdit.Enabled = base.mnuLSVRemove.Enabled = base.mnuLSVProps.Enabled = false;
			}
			catch(Exception ex) { throw(ex); }
		}
		
		//Treeview support --------------------------------------------------
		public override void loadChildNodes() { 
			//Load child nodes of base node (data)
			EnterpriseDS lst = EnterpriseFactory.ViewEntTerminals();
			base.mChildNodes = new TreeNode[lst.EntTerminalViewTable.Rows.Count];
			for(int i=0; i<lst.EntTerminalViewTable.Rows.Count; i++) {
				TerminalChildrenAdminNode node = new TerminalChildrenAdminNode(lst.EntTerminalViewTable[i].Description.Trim(), 0, 1, (int)lst.EntTerminalViewTable[i].LocationID, lst.EntTerminalViewTable[i].IsActive);
				base.mChildNodes[i] = node;
			}
			base.Nodes.Clear();
			base.Nodes.AddRange(base.mChildNodes);

			//Each child must load its children if base node is expanded
			if(base.IsExpanded) {
				foreach(INode node in base.mChildNodes) {
					TreeNode tn = (TreeNode)node;
					node.loadChildNodes();
				}
			}
		}
		
		//Listview support --------------------------------------------------
		public override ColumnHeader[] header() { 
			//Create listview column headers
			ColumnHeader[] headers = null;
			ColumnHeader colName = new ColumnHeader(); colName.Text = "Name"; colName.Width = 192;
			ColumnHeader colNumber = new ColumnHeader(); colNumber.Text = "Number"; colNumber.Width = 60;
			ColumnHeader colPhone = new ColumnHeader(); colPhone.Text = "Phone #"; colPhone.Width = 96;
			ColumnHeader colExt = new ColumnHeader(); colExt.Text = "Ext"; colExt.Width = 36;
			ColumnHeader colAddress = new ColumnHeader(); colAddress.Text = "Address"; colAddress.Width = 288;
			ColumnHeader colContact = new ColumnHeader(); colContact.Text = "Contact"; colContact.Width = 120;
			ColumnHeader colFax = new ColumnHeader(); colFax.Text = "Fax #"; colFax.Width = 96;
			ColumnHeader colIsActive = new ColumnHeader(); colIsActive.Text = "Active"; colIsActive.Width = 60;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "TermID"; colID.Width = 0;
			headers = new ColumnHeader[] {colName, colNumber, colPhone, colExt, colAddress, colContact, colFax, colIsActive, colID};
			return headers;
		}
		public override ListViewItem[] list() { 
			//Set listview items (data)
			EnterpriseDS lst = EnterpriseFactory.ViewEntTerminals();
			ListViewItem[] items = new ListViewItem[lst.EntTerminalViewTable.Rows.Count];
			for(int i=0; i<lst.EntTerminalViewTable.Rows.Count; i++) {
				string[] subitems = {	lst.EntTerminalViewTable[i].Description.Trim(), 
										lst.EntTerminalViewTable[i].Number.Trim(), 
										lst.EntTerminalViewTable[i].Phone.Trim(), 
										lst.EntTerminalViewTable[i].Extension.Trim(), 
										lst.EntTerminalViewTable[i].AddressLine1.Trim() + " " + lst.EntTerminalViewTable[i].AddressLine2.Trim() + " " + lst.EntTerminalViewTable[i].City.Trim() + ", " + lst.EntTerminalViewTable[i].StateOrProvince.Trim() + " " + lst.EntTerminalViewTable[i].PostalCode.Trim(), 
										lst.EntTerminalViewTable[i].ContactName.Trim(), 
										lst.EntTerminalViewTable[i].Fax.Trim(), 
										lst.EntTerminalViewTable[i].IsActive.ToString(), 
										lst.EntTerminalViewTable[i].LocationID.ToString()};
				ListViewItem item = new ListViewItem(subitems, 0);
				items[i] = item;
			}
			return items;
		}
		
		protected override void OnItemMenuClick(object sender, System.EventArgs e) {
			//Context menu event handler
			MenuItem menu;
			EnterpriseDS dsEntTerminal;
			dlgEnterpriseTerminalDetail dlgEntTerminal;
			int terminalID = 0;
			string terminalName = "";
			DialogResult res;
			bool val;
			
			try {
				//Context menu item clicked-apply selected service; forward to menu handler
				menu = (MenuItem)sender;
				switch(menu.Text)  {
					case MNU_OPEN:		
						//Notify client to open the selected child node
						base.OnOpenSelectedItem(EventArgs.Empty);
						break;
					case MNU_ADD:		
						//Read new details (defaults), forward to dialog for update
						terminalID = 0;
						dsEntTerminal = EnterpriseFactory.GetEntTerminal(terminalID);
						dlgEntTerminal = new dlgEnterpriseTerminalDetail(ref dsEntTerminal);
						res = dlgEntTerminal.ShowDialog();
						if(res==DialogResult.OK) {
							//Request a new terminal
							terminalID = EnterpriseFactory.CreateEntTerminal(dsEntTerminal);
							if(terminalID>0) {
								terminalName = dsEntTerminal.EntTerminalDetailTable[0].Description;
								MessageBox.Show("Terminal " + terminalName + " was created.", "Terminal Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("New terminal could not be created. Please try again.", "Terminal Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_EDIT:		
						//Read existing terminal details, forward to dlgEntTerminal for update
						terminalID = Convert.ToInt32(mCurrentItem.SubItems[8].Text);
						dsEntTerminal = EnterpriseFactory.GetEntTerminal(terminalID);
						dlgEntTerminal = new dlgEnterpriseTerminalDetail(ref dsEntTerminal);
						res = dlgEntTerminal.ShowDialog();
						if(res==DialogResult.OK) {
							//Request an update to the terminal 
							val = EnterpriseFactory.UpdateEntTerminal(dsEntTerminal);
							terminalName = dsEntTerminal.EntTerminalDetailTable[0].Description;
							if(val) {
								MessageBox.Show("Terminal " + terminalName + " was updated.", "Terminal Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Terminal " + terminalName + " could not be updated. Please try again.", "Terminal Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_REMOVE:	break;
					case MNU_PROPS:		break;
					default:			break;
				}
			}
			catch(Exception ex) { base.OnErrorMessage(ex); }
		}
	}
	#endregion
		#region TerminalChildrenAdminNode
	//---------------------------------------------------------------------------
	//The EntTerminalAdminNode administers a collection of enterprise terminals
	public class TerminalChildrenAdminNode: AdminNode {
		//Attributes
		private int m_iTerminalID;
		private bool mParentIsActive = true;
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New";
		private const string MNU_EDIT = "Update";
		private const string MNU_REMOVE = "Delete";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public TerminalChildrenAdminNode(string text, int imageIndex, int selectedImageIndex, int terminalID, bool parentIsActive) : base(text, imageIndex, selectedImageIndex) {
			//Constructor
			try {
				//Set custom attributes and create a custom listview service menu
				this.m_iTerminalID = terminalID;
				this.mParentIsActive = parentIsActive;
				base.mnuLSV = new ContextMenu();
				base.mnuLSVOpen = new MenuItem(MNU_OPEN, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep0 = new MenuItem("-");
				base.mnuLSVAdd = new MenuItem(MNU_ADD, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVEdit = new MenuItem(MNU_EDIT, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVRemove = new MenuItem(MNU_REMOVE, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep1 = new MenuItem("-");
				base.mnuLSVProps = new MenuItem(MNU_PROPS, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSV.MenuItems.AddRange(new MenuItem[] {base.mnuLSVOpen, base.mnuLSVSep0, base.mnuLSVAdd, base.mnuLSVEdit, base.mnuLSVRemove, base.mnuLSVSep1, base.mnuLSVProps});
				base.mnuLSVOpen.Enabled = false; base.mnuLSVAdd.Enabled = base.mnuLSVEdit.Enabled = base.mnuLSVRemove.Enabled = base.mnuLSVProps.Enabled = false;
			}
			catch(Exception ex) { throw(ex); }
		}
		
		//Treeview support --------------------------------------------------
		public override void loadChildNodes() { 
			//Load child nodes of base node (data)
			base.mChildNodes = new TreeNode[] {new ShiftScheduleAdminNode("Shift Schedules", 0, 1, this.m_iTerminalID, this.mParentIsActive), 
													new SwitcherAdminNode("Switchers", 0, 1, this.m_iTerminalID, this.mParentIsActive), 
													new TerminalLaneAdminNode("Terminal Lanes", 0, 1, this.m_iTerminalID, this.mParentIsActive), 
													new TerminalWorkstationAdminNode("Terminal Workstations", 0, 1, this.m_iTerminalID, this.mParentIsActive), 
													new YardAdminNode("Yards", 0, 1, this.m_iTerminalID, this.mParentIsActive) };
			base.Nodes.Clear();
			base.Nodes.AddRange(mChildNodes);
			
			//Each child must load its children if base node is expanded
			if(base.IsExpanded) {
				foreach(INode node in base.mChildNodes) {
					TreeNode tn = (TreeNode)node;
					node.loadChildNodes();
				}
			}
		}
		
		//Listview support --------------------------------------------------
		public override ColumnHeader[] header() { 
			//Create listview column headers
			ColumnHeader[] headers = null;
			ColumnHeader colName = new ColumnHeader(); colName.Text = "Name"; colName.Width = 144;
			ColumnHeader colDesc = new ColumnHeader(); colDesc.Text = "Description"; colDesc.Width = 288;
			ColumnHeader colActive = new ColumnHeader(); colActive.Text = "Active"; colActive.Width = 72;
			headers = new ColumnHeader[] {colName, colDesc, colActive};
			return headers; 
		}
		public override ListViewItem[] list() { 
			//Set listview items (data)
			ListViewItem[] items = null;
			string[] subitems = {"Shift Schedules", "Shift Schedule Management"};
			ListViewItem item1 = new ListViewItem(subitems, 0);
			subitems = new string[] {"Switchers", "Switcher Management"};
			ListViewItem item2 = new ListViewItem(subitems, 0);
			subitems = new string[] {"Terminal Lanes", "Terminal Lane Management"};
			ListViewItem item3 = new ListViewItem(subitems, 0);
			subitems = new string[] {"Terminal Workstations", "Terminal Workstation Management"};
			ListViewItem item4 = new ListViewItem(subitems, 0);
			subitems = new string[] {"Yards", "Yard Management"};
			ListViewItem item5 = new ListViewItem(subitems, 0);
			items = new ListViewItem[] {item1, item2, item3, item4, item5};
			return items;
		}
		public override void OnItemSelected(object sender, System.EventArgs e) { 
			//Override menu service states
			base.OnItemSelected(sender, e);
			base.mnuLSVAdd.Enabled = base.mnuLSVEdit.Enabled = base.mnuLSVRemove.Enabled = false;
		}
		
		protected override void OnItemMenuClick(object sender, System.EventArgs e) {
			//Context menu event handler
			MenuItem menu;
			
			try {
				//Context menu item clicked-apply selected service; forward to menu handler
				menu = (MenuItem)sender;
				switch(menu.Text)  {
					case MNU_OPEN:		
						//Notify client to open the selected child node
						base.OnOpenSelectedItem(EventArgs.Empty);
						break;
					case MNU_ADD:		break;
					case MNU_EDIT:		break;
					case MNU_REMOVE:	break;
					case MNU_PROPS:		break;
					default:			break;
				}
			}
			catch(Exception ex) { base.OnErrorMessage(ex); }
		}
	}
	#endregion
			#region ShiftScheduleAdminNode
	//---------------------------------------------------------------------------
	//The ShiftScheduleAdminNode administers the daily shift schedules for one terminal
	public class ShiftScheduleAdminNode: AdminNode {
		//Attributes
		private int m_iTerminalID;
		private bool mParentIsActive = true;
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Daily Shift Schedule";
		private const string MNU_EDIT = "Update Daily Shift Schedule";
		private const string MNU_REMOVE = "Delete Daily Shift Schedule";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public ShiftScheduleAdminNode(string text, int imageIndex, int selectedImageIndex, int terminalID, bool parentIsActive) : base(text, imageIndex, selectedImageIndex) {
			//Constructor
			try {
				//Set custom attributes and create a custom listview service menu
				this.m_iTerminalID = terminalID;
				this.mParentIsActive = parentIsActive;
				base.mnuLSV = new ContextMenu();
				base.mnuLSVOpen = new MenuItem(MNU_OPEN, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep0 = new MenuItem("-");
				base.mnuLSVAdd = new MenuItem(MNU_ADD, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVEdit = new MenuItem(MNU_EDIT, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVRemove = new MenuItem(MNU_REMOVE, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep1 = new MenuItem("-");
				base.mnuLSVProps = new MenuItem(MNU_PROPS, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSV.MenuItems.AddRange(new MenuItem[] {base.mnuLSVOpen, base.mnuLSVSep0, base.mnuLSVAdd, base.mnuLSVEdit, base.mnuLSVRemove, base.mnuLSVSep1, base.mnuLSVProps});
				base.mnuLSVOpen.Enabled = false; base.mnuLSVAdd.Enabled = base.mnuLSVEdit.Enabled = base.mnuLSVRemove.Enabled = false; base.mnuLSVProps.Enabled = false;
			}
			catch(Exception ex) { throw(ex); }
		}
		
		//Treeview support --------------------------------------------------
		
		//Listview support --------------------------------------------------
		public override ColumnHeader[] header() { 
			//Create listview column headers
			ColumnHeader[] headers = null;
			ColumnHeader colDay = new ColumnHeader(); colDay.Text = "Day"; colDay.Width = 120;
			ColumnHeader colShift1 = new ColumnHeader(); colShift1.Text = "Shift 1"; colShift1.Width = 192;
			ColumnHeader colShift2 = new ColumnHeader(); colShift2.Text = "Shift 2"; colShift2.Width = 192;
			ColumnHeader colShift3 = new ColumnHeader(); colShift3.Text = "Shift 3"; colShift3.Width = 192;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "DailyShiftScheduleID"; colID.Width = 0;
			headers = new ColumnHeader[] {colDay, colShift1, colShift2, colShift3, colID};
			return headers; 
		}
		public override ListViewItem[] list() { 
			//Set listview items (data)
			ShiftDS lst = FreightFactory.ViewShiftSchedules(this.m_iTerminalID);
			ListViewItem[] items = new ListViewItem[lst.ShiftViewTable.Rows.Count];
			for(int i=0; i<lst.ShiftViewTable.Rows.Count; i++) {
				string shift1 = (lst.ShiftViewTable[i].Shift1IsActive) ? Convert.ToDateTime(lst.ShiftViewTable[i].Shift1StartTime).ToShortTimeString() + " - " + Convert.ToDateTime(lst.ShiftViewTable[i].Shift1EndTime).ToShortTimeString() : "Inactive";
				string shift2 = (lst.ShiftViewTable[i].Shift2IsActive) ? Convert.ToDateTime(lst.ShiftViewTable[i].Shift2StartTime).ToShortTimeString() + " - " + Convert.ToDateTime(lst.ShiftViewTable[i].Shift2EndTime).ToShortTimeString() : "Inactive";
				string shift3 = (lst.ShiftViewTable[i].Shift3IsActive) ? Convert.ToDateTime(lst.ShiftViewTable[i].Shift3StartTime).ToShortTimeString() + " - " + Convert.ToDateTime(lst.ShiftViewTable[i].Shift3EndTime).ToShortTimeString() : "Inactive";
				string[] subitems = {	lst.ShiftViewTable[i].DayOfTheWeek, 
										shift1, 
										shift2, 
										shift3, 
										lst.ShiftViewTable[i].TerminalID.ToString()};
				ListViewItem item = new ListViewItem(subitems, 2);
				items[i] = item;
			}
			return items;
		}
		public override void OnItemSelected(object sender, System.EventArgs e) { 
			//Override menu service states
			base.OnItemSelected(sender, e);
			base.mnuLSVAdd.Enabled = base.mnuLSVRemove.Enabled = false;
		}
		
		protected override void OnItemMenuClick(object sender, System.EventArgs e) {
			//Context menu event handler
			MenuItem menu;
			ShiftDS dsShiftDetail;
			dlgDailyShiftScheduleDetail dlgDailyShiftSchedule;
			int termID = 0;
			string weekDay = "";
			DialogResult res;
			bool val;
			
			try {
				//Context menu item clicked-apply selected service; forward to menu handler
				menu = (MenuItem)sender;
				switch(menu.Text)  {
					case MNU_OPEN:		
						//Notify client to open the selected child node
						base.OnOpenSelectedItem(EventArgs.Empty);
						break;
					case MNU_ADD:		break;
					case MNU_EDIT:		
						//Read existing yard details, forward to dlgDailyShiftSchedule for update
						termID = Convert.ToInt32(mCurrentItem.SubItems[4].Text);
						weekDay = mCurrentItem.SubItems[0].Text;
						dsShiftDetail = FreightFactory.GetDailyShiftSchedule(this.m_iTerminalID, weekDay);
						dlgDailyShiftSchedule = new dlgDailyShiftScheduleDetail(ref dsShiftDetail);
						res = dlgDailyShiftSchedule.ShowDialog();
						if(res==DialogResult.OK) {
							//Request an update to the daily schedule
							val = FreightFactory.UpdateDailyShiftSchedule(dsShiftDetail);
							if(val) {
								MessageBox.Show("Daily shift schedule for " + weekDay + " was updated.", "Daily Shift Schedule Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Daily shift schedule for " + weekDay + " could not be updated. Please try again.", "Daily Shift Schedule Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_REMOVE:	break;
					case MNU_PROPS:		break;
					default:			break;
				}				
			}
			catch(Exception ex) { base.OnErrorMessage(ex); }
		}
	}
	#endregion
			#region	SwitcherAdminNode
	//---------------------------------------------------------------------------
	//The SwitcherAdminNode administers the collection of LTA switchers
	public class SwitcherAdminNode: AdminNode {
		//Attributes
		private int m_iTerminalID;
		private bool mParentIsActive = true;
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Switcher";
		private const string MNU_EDIT = "Update Switcher";
		private const string MNU_REMOVE = "Delete Switcher";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public SwitcherAdminNode(string text, int imageIndex, int selectedImageIndex, int terminalID, bool parentIsActive) : base(text, imageIndex, selectedImageIndex) {
			//Constructor
			try {
				//Set custom attributes and create a custom listview service menu
				this.m_iTerminalID = terminalID;
				this.mParentIsActive = parentIsActive;
				base.mnuLSV = new ContextMenu();
				base.mnuLSVOpen = new MenuItem(MNU_OPEN, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep0 = new MenuItem("-");
				base.mnuLSVAdd = new MenuItem(MNU_ADD, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVEdit = new MenuItem(MNU_EDIT, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVRemove = new MenuItem(MNU_REMOVE, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep1 = new MenuItem("-");
				base.mnuLSVProps = new MenuItem(MNU_PROPS, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSV.MenuItems.AddRange(new MenuItem[] {base.mnuLSVOpen, base.mnuLSVSep0, base.mnuLSVAdd, base.mnuLSVEdit, base.mnuLSVRemove, base.mnuLSVSep1, base.mnuLSVProps});
				base.mnuLSVOpen.Enabled = false; base.mnuLSVAdd.Enabled = true; base.mnuLSVEdit.Enabled = base.mnuLSVRemove.Enabled = false; base.mnuLSVProps.Enabled = false;
			}
			catch(Exception ex) { throw(ex); }
		}
		
		//Listview support --------------------------------------------------
		public override ColumnHeader[] header() { 
			//Create listview column headers
			ColumnHeader[] headers = null;
			ColumnHeader colNameLast = new ColumnHeader(); colNameLast.Text = "Last Name"; colNameLast.Width = 144;
			ColumnHeader colNameFirst = new ColumnHeader(); colNameFirst.Text = "First Name"; colNameFirst.Width = 96;
			ColumnHeader colPhone = new ColumnHeader(); colPhone.Text = "Phone #"; colPhone.Width = 96;
			ColumnHeader colTerminal = new ColumnHeader(); colTerminal.Text = "Terminal"; colTerminal.Width = 168;
			ColumnHeader colIsActive = new ColumnHeader(); colIsActive.Text = "Active"; colIsActive.Width = 60;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "SwitcherID"; colID.Width = 0;
			headers = new ColumnHeader[] {colNameLast, colNameFirst, colPhone, colTerminal, colIsActive, colID};
			return headers;
		}
		public override ListViewItem[] list() { 
			//Set listview items (data)
			SwitcherDS lst = TransportationFactory.ViewSwitchers(this.m_iTerminalID);
			ListViewItem[] items = new ListViewItem[lst.SwitcherViewTable.Rows.Count];
			for(int i=0; i<lst.SwitcherViewTable.Rows.Count; i++) {
				string[] subitems = {	lst.SwitcherViewTable[i].LastName.Trim(), 
										lst.SwitcherViewTable[i].FirstName.Trim(), 
										lst.SwitcherViewTable[i].Phone.Trim(), 
										lst.SwitcherViewTable[i].Terminal.Trim(), 
										lst.SwitcherViewTable[i].IsActive.ToString(), 
										lst.SwitcherViewTable[i].SwitcherID.ToString() };
				ListViewItem item = new ListViewItem(subitems, 2);
				items[i] = item;
			}
			return items;
		}
		
		protected override void OnItemMenuClick(object sender, System.EventArgs e) {
			//Context menu event handler
			MenuItem menu;
			SwitcherDS dsSwitcher;
			dlgSwitcherDetail dlgSwitcher;
			int switcherID = 0;
			string switcherName = "";
			DialogResult res;
			bool val;
			
			try {
				//Context menu item clicked-apply selected service; forward to menu handler
				menu = (MenuItem)sender;
				switch(menu.Text)  {
					case MNU_OPEN:		break;
					case MNU_ADD:		
						//Read new switcher details (defaults), forward to dlgSwitcher for update
						switcherID = 0;
						dsSwitcher = TransportationFactory.GetSwitcher(switcherID);
						dlgSwitcher = new dlgSwitcherDetail(ref dsSwitcher);
						res = dlgSwitcher.ShowDialog();
						if(res==DialogResult.OK) {
							//Request a new switcher
							switcherID = TransportationFactory.CreateSwitcher(dsSwitcher);
							if(switcherID!=0) {
								switcherName = dsSwitcher.SwitcherDetailTable[0].FirstName + " " + dsSwitcher.SwitcherDetailTable[0].LastName;
								MessageBox.Show("Switcher " + switcherName + " was created.", "Switcher Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("New switcher could not be created. Please try again.", "Switcher Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_EDIT:		
						//Read existing switcher details, forward to dlgSwitcher for update
						switcherID = Convert.ToInt32(mCurrentItem.SubItems[5].Text);
						dsSwitcher = TransportationFactory.GetSwitcher(switcherID);
						dlgSwitcher = new dlgSwitcherDetail(ref dsSwitcher);
						res = dlgSwitcher.ShowDialog();
						if(res==DialogResult.OK) {
							//Request an update to the switcher 
							val = TransportationFactory.UpdateSwitcher(dsSwitcher);
							if(val) {
								switcherName = dsSwitcher.SwitcherDetailTable[0].FirstName + " " + dsSwitcher.SwitcherDetailTable[0].LastName;
								MessageBox.Show("Switcher " + switcherName + " was updated.", "Switcher Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Switcher " + switcherName + " could not be updated. Please try again.", "Switcher Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_REMOVE:	break;
					case MNU_PROPS:		break;
					default:			break;
				}				
			}
			catch(Exception ex) { base.OnErrorMessage(ex); }
		}
	}
	#endregion
			#region TerminalLaneAdminNode
	//---------------------------------------------------------------------------
	//The TerminalLaneAdminNode administers the collection of damage codes
	public class TerminalLaneAdminNode: AdminNode {
		//Attributes
		private int m_iTerminalID;
		private bool mParentIsActive = true;
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Terminal Lane";
		private const string MNU_EDIT = "Update Terminal Lane";
		private const string MNU_REMOVE = "Delete Terminal Lane";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public TerminalLaneAdminNode(string text, int imageIndex, int selectedImageIndex, int terminalID, bool parentIsActive) : base(text, imageIndex, selectedImageIndex) {
			//Constructor

			try {
				//Set custom attributes and create a custom listview service menu
				this.m_iTerminalID = terminalID;
				this.mParentIsActive = parentIsActive;
				base.mnuLSV = new ContextMenu();
				base.mnuLSVOpen = new MenuItem(MNU_OPEN, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep0 = new MenuItem("-");
				base.mnuLSVAdd = new MenuItem(MNU_ADD, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVEdit = new MenuItem(MNU_EDIT, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVRemove = new MenuItem(MNU_REMOVE, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep1 = new MenuItem("-");
				base.mnuLSVProps = new MenuItem(MNU_PROPS, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSV.MenuItems.AddRange(new MenuItem[] {base.mnuLSVOpen, base.mnuLSVSep0, base.mnuLSVAdd, base.mnuLSVEdit, base.mnuLSVRemove, base.mnuLSVSep1, base.mnuLSVProps});
				base.mnuLSVOpen.Enabled = false; base.mnuLSVAdd.Enabled = true; base.mnuLSVEdit.Enabled = base.mnuLSVRemove.Enabled = false; base.mnuLSVProps.Enabled = false;
			}
			catch(Exception ex) { throw(ex); }
		}
		
		//Listview support --------------------------------------------------
		public override ColumnHeader[] header() { 
			//Create listview column headers
			ColumnHeader[] headers = null;
			ColumnHeader colNumber = new ColumnHeader(); colNumber.Text = "Number"; colNumber.Width = 96;
			ColumnHeader colType = new ColumnHeader(); colType.Text = "Lane Type"; colType.Width = 96;
			ColumnHeader colDesc = new ColumnHeader(); colDesc.Text = "Description"; colDesc.Width = 288;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "LaneID"; colID.Width = 0;
			headers = new ColumnHeader[] {colNumber, colType, colDesc, colID};
			return headers; 
		}
		public override ListViewItem[] list() { 
			//Set listview items (data)
			TerminalLaneDS lst = EnterpriseFactory.ViewTerminalLanes(this.m_iTerminalID);
			ListViewItem[] items = new ListViewItem[lst.TerminalLaneTable.Rows.Count];
			for(int i=0; i<lst.TerminalLaneTable.Rows.Count; i++) {
				string[] subitems = {	lst.TerminalLaneTable[i].LaneNumber.Trim(), 
										lst.TerminalLaneTable[i].LaneType.Trim(), 
										lst.TerminalLaneTable[i].Description.Trim(), 
										lst.TerminalLaneTable[i].LaneID };
				ListViewItem item = new ListViewItem(subitems, 2);
				items[i] = item;
			}
			return items;
		}
		public override void OnItemSelected(object sender, System.EventArgs e) { 
			//Event handler for listview display of base object
			ListView lsv;
			bool isItem = false, hasChildNodes = false;
			
			try {
				//Capture current child (lisview item) for base parent
				lsv = (ListView)sender;
				base.mCurrentItem = null;
				isItem = (lsv.SelectedItems.Count>0);
				if(lsv.SelectedItems.Count>0) 
					base.mCurrentItem = lsv.SelectedItems[0];
				isItem = (base.mCurrentItem!=null);
				hasChildNodes = (base.mChildNodes!=null);
			}
			catch(Exception ex) { base.OnErrorMessage(ex); }
			finally {
				//Set menu services
				base.mnuLSVOpen.Enabled = (isItem && hasChildNodes);
				base.mnuLSVAdd.Enabled = !isItem;
				base.mnuLSVEdit.Enabled = isItem;
				base.mnuLSVRemove.Enabled = isItem;
				base.mnuLSVProps.Enabled = false;
			}
		}
		protected override void OnItemMenuClick(object sender, System.EventArgs e) {
			//Context menu event handler
			MenuItem menu=null;
			TerminalLaneDS dsTerminalLane=null;
			dlgTerminalLaneDetail dlgTerminalLane=null;
			string laneID="", lane="";
			DialogResult res=DialogResult.Cancel;
			bool val=false;
			
			try {
				//Context menu item clicked-apply selected service; forward to menu handler
				menu = (MenuItem)sender;
				switch(menu.Text)  {
					case MNU_OPEN:		break;
					case MNU_ADD:		
						//Read new terminal lane details (defaults), forward to dlgTerminalLaneDetail for update
						laneID = "";
						dsTerminalLane = EnterpriseFactory.GetTerminalLane(this.m_iTerminalID, laneID);
						dlgTerminalLane = new dlgTerminalLaneDetail(ref dsTerminalLane);
						res = dlgTerminalLane.ShowDialog();
						if(res==DialogResult.OK) {
							//Request a new terminal lane
							laneID = EnterpriseFactory.CreateTerminalLane(dsTerminalLane);
							lane = dsTerminalLane.TerminalLaneTable[0].LaneNumber;
							if(laneID!="") {
								MessageBox.Show("Terminal lane " + lane + " was created.", "Terminal Lane Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("New terminal lane could not be created. Please try again.", "Terminal Lane Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_EDIT:		
						//Read existing terminal lane details, forward to dlgTerminalLane for update
						laneID = base.mCurrentItem.SubItems[3].Text;
						dsTerminalLane = EnterpriseFactory.GetTerminalLane(this.m_iTerminalID, laneID);
						dlgTerminalLane = new dlgTerminalLaneDetail(ref dsTerminalLane);
						res = dlgTerminalLane.ShowDialog();
						if(res==DialogResult.OK) {
							//Request an update to the terminal lane
							val = EnterpriseFactory.UpdateTerminalLane(dsTerminalLane);
							lane = base.mCurrentItem.SubItems[0].Text;
							if(val) {
								MessageBox.Show("Terminal lane " + lane + " was updated.", "Terminal Lane Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Terminal lane " + lane + " could not be updated. Please try again.", "Terminal Lane Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_REMOVE:	
						//Prompt user for confirmation
						laneID = base.mCurrentItem.SubItems[3].Text;
						res = MessageBox.Show("Cancel terminal lane " + laneID + "?", "Terminal Lane Admin", MessageBoxButtons.OKCancel);
						if(res==DialogResult.OK) {
							//Read existing details for row version and request cancel
							dsTerminalLane = EnterpriseFactory.GetTerminalLane(this.m_iTerminalID, laneID);
							string rowVer = dsTerminalLane.TerminalLaneTable[0].RowVersion;
							val = EnterpriseFactory.DeleteTerminalLane(laneID, rowVer);
							lane = base.mCurrentItem.SubItems[0].Text;
							if(val) {
								MessageBox.Show("Terminal lane " + lane + " was deleted.", "Terminal Lane Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Terminal lane " + lane + " could not be deleted. Please try again.", "Terminal Lane Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_PROPS:		break;
					default:			break;
				}			
			}
			catch(Exception ex) { base.OnErrorMessage(ex); }
		}
	}
	#endregion
			#region TerminalWorkstationAdminNode
	//---------------------------------------------------------------------------
	//The TerminalLaneAdminNode administers the collection of terminal workstations
	public class TerminalWorkstationAdminNode: AdminNode {
		//Attributes
		private int m_iTerminalID;
		private bool mParentIsActive = true;
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Terminal Workstation";
		private const string MNU_EDIT = "Update Terminal Workstation";
		private const string MNU_REMOVE = "Delete Terminal Workstation";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public TerminalWorkstationAdminNode(string text, int imageIndex, int selectedImageIndex, int terminalID, bool parentIsActive) : base(text, imageIndex, selectedImageIndex) {
			//Constructor

			try {
				//Set custom attributes and create a custom listview service menu
				this.m_iTerminalID = terminalID;
				this.mParentIsActive = parentIsActive;
				base.mnuLSV = new ContextMenu();
				base.mnuLSVOpen = new MenuItem(MNU_OPEN, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep0 = new MenuItem("-");
				base.mnuLSVAdd = new MenuItem(MNU_ADD, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVEdit = new MenuItem(MNU_EDIT, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVRemove = new MenuItem(MNU_REMOVE, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep1 = new MenuItem("-");
				base.mnuLSVProps = new MenuItem(MNU_PROPS, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSV.MenuItems.AddRange(new MenuItem[] {base.mnuLSVOpen, base.mnuLSVSep0, base.mnuLSVAdd, base.mnuLSVEdit, base.mnuLSVRemove, base.mnuLSVSep1, base.mnuLSVProps});
				base.mnuLSVOpen.Enabled = false; base.mnuLSVAdd.Enabled = true; base.mnuLSVEdit.Enabled = base.mnuLSVRemove.Enabled = false; base.mnuLSVProps.Enabled = false;
			}
			catch(Exception ex) { throw(ex); }
		}
		
		//Listview support --------------------------------------------------
		public override ColumnHeader[] header() { 
			//Create listview column headers
			ColumnHeader[] headers = null;
			ColumnHeader colNumber = new ColumnHeader(); colNumber.Text = "Number"; colNumber.Width = 96;
			ColumnHeader colName = new ColumnHeader(); colName.Text = "Name"; colName.Width = 144;
			ColumnHeader colDesc = new ColumnHeader(); colDesc.Text = "Location"; colDesc.Width = 288;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "StationID"; colID.Width = 0;
			headers = new ColumnHeader[] {colNumber, colName, colDesc, colID};
			return headers; 
		}
		public override ListViewItem[] list() { 
			//Set listview items (data)
			WorkstationlDS lst = EnterpriseFactory.ViewTerminalWorkstations(this.m_iTerminalID);
			ListViewItem[] items = new ListViewItem[lst.TerminalWorkstationTable.Rows.Count];
			for(int i=0; i<lst.TerminalWorkstationTable.Rows.Count; i++) {
				string[] subitems = {	lst.TerminalWorkstationTable[i].Number.Trim(), 
										lst.TerminalWorkstationTable[i].Name.Trim(), 
										lst.TerminalWorkstationTable[i].Description.Trim(), 
										lst.TerminalWorkstationTable[i].Name };
				ListViewItem item = new ListViewItem(subitems, 2);
				items[i] = item;
			}
			return items;
		}
		public override void OnItemSelected(object sender, System.EventArgs e) { 
			//Event handler for listview display of base object
			ListView lsv;
			bool isItem = false, hasChildNodes = false;
			
			try {
				//Capture current child (lisview item) for base parent
				lsv = (ListView)sender;
				base.mCurrentItem = null;
				isItem = (lsv.SelectedItems.Count>0);
				if(lsv.SelectedItems.Count>0) 
					base.mCurrentItem = lsv.SelectedItems[0];
				isItem = (base.mCurrentItem!=null);
				hasChildNodes = (base.mChildNodes!=null);
			}
			catch(Exception ex) { base.OnErrorMessage(ex); }
			finally {
				//Set menu services
				base.mnuLSVOpen.Enabled = (isItem && hasChildNodes);
				base.mnuLSVAdd.Enabled = !isItem;
				base.mnuLSVEdit.Enabled = isItem;
				base.mnuLSVRemove.Enabled = isItem;
				base.mnuLSVProps.Enabled = false;
			}
		}
		protected override void OnItemMenuClick(object sender, System.EventArgs e) {
			//Context menu event handler
			MenuItem menu=null;
			WorkstationlDS dsTerminalWorkstation=null;
			dlgTerminalWorkstationDetail dlgTerminalWorkstation=null;
			string stationID="", number="";
			DialogResult res=DialogResult.Cancel;
			bool val=false;
			
			try {
				//Context menu item clicked-apply selected service; forward to menu handler
				menu = (MenuItem)sender;
				switch(menu.Text)  {
					case MNU_OPEN:		break;
					case MNU_ADD:		
						//Read new terminal workstation details (defaults), forward to dlgTerminalWorkstationDetail for update
						stationID = "";
						dsTerminalWorkstation = EnterpriseFactory.GetTerminalWorkstation(this.m_iTerminalID, stationID);
						dlgTerminalWorkstation = new dlgTerminalWorkstationDetail(ref dsTerminalWorkstation);
						res = dlgTerminalWorkstation.ShowDialog();
						if(res==DialogResult.OK) {
							//Request a new terminal lane
							stationID = EnterpriseFactory.CreateTerminalWorkstation(dsTerminalWorkstation);
							number = dsTerminalWorkstation.TerminalWorkstationTable[0].Number;
							if(stationID!="") {
								MessageBox.Show("Terminal workstation " + number + " was created.", "Terminal Workstation Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("New terminal workstation could not be created. Please try again.", "Terminal Workstation Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_EDIT:		
						//Read existing terminal lane details, forward to dlgTerminalLane for update
						stationID = base.mCurrentItem.SubItems[3].Text;
						dsTerminalWorkstation = EnterpriseFactory.GetTerminalWorkstation(this.m_iTerminalID, stationID);
						dlgTerminalWorkstation = new dlgTerminalWorkstationDetail(ref dsTerminalWorkstation);
						res = dlgTerminalWorkstation.ShowDialog();
						if(res==DialogResult.OK) {
							//Request an update to the terminal workstation
							val = EnterpriseFactory.UpdateTerminalWorkstation(dsTerminalWorkstation);
							number = base.mCurrentItem.SubItems[0].Text;
							if(val) {
								MessageBox.Show("Terminal workstation " + number + " was updated.", "Terminal Workstation Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Terminal workstation " + number + " could not be updated. Please try again.", "Terminal Workstation Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_REMOVE:	
						//Prompt user for confirmation
						stationID = base.mCurrentItem.SubItems[3].Text;
						res = MessageBox.Show("Cancel terminal workstation " + stationID + "?", "Terminal Workstation Admin", MessageBoxButtons.OKCancel);
						if(res==DialogResult.OK) {
							//Read existing details for row version and request cancel
							dsTerminalWorkstation = EnterpriseFactory.GetTerminalWorkstation(this.m_iTerminalID, stationID);
							string rowVer = dsTerminalWorkstation.TerminalWorkstationTable[0].RowVersion;
							val = EnterpriseFactory.DeleteTerminalWorkstation(stationID, rowVer);
							number = base.mCurrentItem.SubItems[0].Text;
							if(val) {
								MessageBox.Show("Terminal workstation " + number + " was deleted.", "Terminal Workstation Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Terminal workstation " + number + " could not be deleted. Please try again.", "Terminal Workstation Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_PROPS:		break;
					default:			break;
				}			
			}
			catch(Exception ex) { base.OnErrorMessage(ex); }
		}
	}
	#endregion
			#region YardAdminNode
	//---------------------------------------------------------------------------
	//The YardAdminNode administers the collection of terminal yards
	public class YardAdminNode: AdminNode {
		//Attributes
		private int m_iTerminalID;
		private bool mParentIsActive = true;
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Yard";
		private const string MNU_EDIT = "Update Yard";
		private const string MNU_REMOVE = "Delete Yard";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public YardAdminNode(string text, int imageIndex, int selectedImageIndex, int terminalID, bool parentIsActive) : base(text, imageIndex, selectedImageIndex) {
			//Constructor
			try {
				//Set custom attributes and create a custom listview service menu
				this.m_iTerminalID = terminalID;
				this.mParentIsActive = parentIsActive;
				base.mnuLSV = new ContextMenu();
				base.mnuLSVOpen = new MenuItem(MNU_OPEN, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep0 = new MenuItem("-");
				base.mnuLSVAdd = new MenuItem(MNU_ADD, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVEdit = new MenuItem(MNU_EDIT, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVRemove = new MenuItem(MNU_REMOVE, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep1 = new MenuItem("-");
				base.mnuLSVProps = new MenuItem(MNU_PROPS, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSV.MenuItems.AddRange(new MenuItem[] {base.mnuLSVOpen, base.mnuLSVSep0, base.mnuLSVAdd, base.mnuLSVEdit, base.mnuLSVRemove, base.mnuLSVSep1, base.mnuLSVProps});
				base.mnuLSVOpen.Enabled = false; base.mnuLSVAdd.Enabled = true; base.mnuLSVEdit.Enabled = base.mnuLSVRemove.Enabled = false; base.mnuLSVProps.Enabled = false;
			}
			catch(Exception ex) { throw(ex); }
		}
		
		//Treeview support --------------------------------------------------
		public override void loadChildNodes() { 
		//Load child nodes of base node (data)
			YardDS lst = TransportationFactory.ViewYards(m_iTerminalID);
			mChildNodes = new TreeNode[lst.YardDetailTable.Rows.Count];
			for(int i=0; i<lst.YardDetailTable.Rows.Count; i++) {
				YardSectionAdminNode node = new YardSectionAdminNode(lst.YardDetailTable[i].Name.Trim(), 0, 1, (int)lst.YardDetailTable[i].YardID, base.Text, lst.YardDetailTable[i].IsActive);
				mChildNodes[i] = node;
			}
			base.Nodes.Clear();
			base.Nodes.AddRange(mChildNodes);

			//Each child must load its children if base node is expanded
			if(base.IsExpanded) {
				foreach(INode node in base.mChildNodes) {
					TreeNode tn = (TreeNode)node;
					node.loadChildNodes();
				}
			}
		}
		
		//Listview support --------------------------------------------------
		public override ColumnHeader[] header() { 
			//Create listview column headers
			ColumnHeader[] headers = null;
			ColumnHeader colName = new ColumnHeader(); colName.Text = "Name"; colName.Width = 144;
			ColumnHeader colDesc = new ColumnHeader(); colDesc.Text = "Description"; colDesc.Width = 288;
			ColumnHeader colTerm = new ColumnHeader(); colTerm.Text = "Terminal"; colTerm.Width = 0;
			ColumnHeader colIsActive = new ColumnHeader(); colIsActive.Text = "Active"; colIsActive.Width = 60;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "YardID"; colID.Width = 0;
			headers = new ColumnHeader[] {colName, colDesc, colTerm, colIsActive, colID};
			return headers; 
		}
		public override ListViewItem[] list() { 
			//Set listview items (data)
			YardDS lst = TransportationFactory.ViewYards(m_iTerminalID);
			ListViewItem[] items = new ListViewItem[lst.YardDetailTable.Rows.Count];
			for(int i=0; i<lst.YardDetailTable.Rows.Count; i++) {
				string[] subitems = {	lst.YardDetailTable[i].Name.Trim(), 
										lst.YardDetailTable[i].Description.Trim(), 
										lst.YardDetailTable[i].Terminal.Trim(), 
										lst.YardDetailTable[i].IsActive.ToString(), 
										lst.YardDetailTable[i].YardID.ToString() };
				ListViewItem item = new ListViewItem(subitems, 0);
				items[i] = item;
			}
			return items;
		}
		
		protected override void OnItemMenuClick(object sender, System.EventArgs e) {
			//Context menu event handler
			MenuItem menu;
			YardDS dsYard;
			dlgYardDetail dlgYard;
			int yardID = 0;
			string yardName = "";
			DialogResult res;
			bool val;
			
			try {
				//Context menu item clicked-apply selected service; forward to menu handler
				menu = (MenuItem)sender;
				switch(menu.Text)  {
					case MNU_OPEN:		
						//Notify client to open the selected child node
						base.OnOpenSelectedItem(EventArgs.Empty);
						break;
					case MNU_ADD:		
						//Read new yard details (defaults), forward to dlgYard for update
						yardID = 0;
						dsYard = TransportationFactory.GetYard(this.m_iTerminalID, yardID);
						dlgYard = new dlgYardDetail(base.Text, this.mParentIsActive, ref dsYard);
						res = dlgYard.ShowDialog();
						if(res==DialogResult.OK) {
							//Request a new yard
							yardID = TransportationFactory.CreateYard(dsYard);							
							if(yardID>0) {
								yardName = dsYard.YardDetailTable[0].Description;
								MessageBox.Show("Yard " + yardName + " was created.", "Yard Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("New yard could not be created. Please try again.", "Yard Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_EDIT:		
						//Read existing yard details, forward to dlgYard for update
						yardID = Convert.ToInt32(mCurrentItem.SubItems[4].Text);
						dsYard = TransportationFactory.GetYard(this.m_iTerminalID, yardID);
						dlgYard = new dlgYardDetail(base.Text, this.mParentIsActive, ref dsYard);
						res = dlgYard.ShowDialog();
						if(res==DialogResult.OK) {
							//Request an update to the yard
							val = TransportationFactory.UpdateYard(dsYard);
							yardName = dsYard.YardDetailTable[0].Description;
							if(val) {
								MessageBox.Show("Yard " + yardName + " was updated.", "Yard Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Yard " + yardName + " could not be updated. Please try again.", "Yard Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_REMOVE:	break;
					case MNU_PROPS:		break;
					default:			break;
				}				
			}
			catch(Exception ex) { base.OnErrorMessage(ex); }
		}
	}
	#endregion
				#region YardSectionAdminNode
	//---------------------------------------------------------------------------
	//The YardSectionAdminNode administers the collection of terminal yard sections
	public class YardSectionAdminNode: AdminNode {
		//Attributes
		private int m_iYardID;
		private string m_sTerminal = "";
		private bool mParentIsActive = true;
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Yard Section";
		private const string MNU_EDIT = "Update Yard Section";
		private const string MNU_REMOVE = "Delete Yard Section";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public YardSectionAdminNode(string text, int imageIndex, int selectedImageIndex, int yardSectionID, string terminal, bool parentIsActive) : base(text, imageIndex, selectedImageIndex) {
			//Constructor
			try {
				//Set custom attributes and create a custom service menu
				this.m_iYardID = yardSectionID;
				this.m_sTerminal = terminal;
				this.mParentIsActive = parentIsActive;
				base.mnuLSV = new ContextMenu();
				base.mnuLSVOpen = new MenuItem(MNU_OPEN, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep0 = new MenuItem("-");
				base.mnuLSVAdd = new MenuItem(MNU_ADD, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVEdit = new MenuItem(MNU_EDIT, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVRemove = new MenuItem(MNU_REMOVE, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep1 = new MenuItem("-");
				base.mnuLSVProps = new MenuItem(MNU_PROPS, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSV.MenuItems.AddRange(new MenuItem[] {base.mnuLSVOpen, base.mnuLSVSep0, base.mnuLSVAdd, base.mnuLSVEdit, base.mnuLSVRemove, base.mnuLSVSep1, base.mnuLSVProps});
				base.mnuLSVOpen.Enabled = false; base.mnuLSVAdd.Enabled = true; base.mnuLSVEdit.Enabled = base.mnuLSVRemove.Enabled = false; base.mnuLSVProps.Enabled = false;
			}
			catch(Exception ex) { throw(ex); }
		}
		
		//Treeview support --------------------------------------------------
		public override void loadChildNodes() { 
			//Load child nodes of base node (data)
			YardSectionDS lst = TransportationFactory.ViewYardSections(m_iYardID);
			mChildNodes = new TreeNode[lst.YardSectionListTable.Rows.Count];
			for(int i=0; i<lst.YardSectionListTable.Rows.Count; i++) {
				YardLocationAdminNode node = new YardLocationAdminNode(lst.YardSectionListTable[i].SectionNumber.Trim(), 0, 1, (int)lst.YardSectionListTable[i].SectionID, base.Text, m_sTerminal, lst.YardSectionListTable[i].IsActive);
				mChildNodes[i] = node;
			}
			base.Nodes.Clear();
			base.Nodes.AddRange(mChildNodes);

			//Each child must load its children if base node is expanded
			if(base.IsExpanded) {
				foreach(INode node in base.mChildNodes) {
					TreeNode tn = (TreeNode)node;
					node.loadChildNodes();
				}
			}
		}
		
		//Listview support --------------------------------------------------
		public override ColumnHeader[] header() { 
			//Create listview column headers
			ColumnHeader[] headers = null;
			ColumnHeader colName = new ColumnHeader(); colName.Text = "Number"; colName.Width = 96;
			ColumnHeader colDesc = new ColumnHeader(); colDesc.Text = "Description"; colDesc.Width = 288;
			ColumnHeader colTerm = new ColumnHeader(); colTerm.Text = "Terminal"; colTerm.Width = 0;
			ColumnHeader colYard = new ColumnHeader(); colYard.Text = "Yard"; colYard.Width = 0;
			ColumnHeader colIsActive = new ColumnHeader(); colIsActive.Text = "Active"; colIsActive.Width = 60;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "SectionID"; colID.Width = 0;
			headers = new ColumnHeader[] {colName, colDesc, colTerm, colYard, colIsActive, colID};
			return headers; 
		}
		public override ListViewItem[] list() { 
			//Set listview items (data)
			YardSectionDS lst = TransportationFactory.ViewYardSections(m_iYardID);
			ListViewItem[] items = new ListViewItem[lst.YardSectionListTable.Rows.Count];
			for(int i=0; i<lst.YardSectionListTable.Rows.Count; i++) {
				string[] subitems = {	lst.YardSectionListTable[i].SectionNumber.Trim(), 
										lst.YardSectionListTable[i].Description.Trim(), 
										lst.YardSectionListTable[i].Terminal.Trim(), 
										lst.YardSectionListTable[i].YardName.Trim(), 
										lst.YardSectionListTable[i].IsActive.ToString(), 
										lst.YardSectionListTable[i].SectionID.ToString() };
				ListViewItem item = new ListViewItem(subitems, 0);
				items[i] = item;
			}
			return items;
		}
		
		protected override void OnItemMenuClick(object sender, System.EventArgs e) {
			//Context menu event handler
			MenuItem menu;
			YardSectionDS dsYardSection;
			dlgYardSectionDetail dlgYardSection;
			int yardSectionID = 0;
			string yardSectionName = "";
			DialogResult res;
			bool val;
			
			try {
				//Context menu item clicked-apply selected service; forward to menu handler
				menu = (MenuItem)sender;
				switch(menu.Text)  {
					case MNU_OPEN:		
						//Notify client to open the selected child node
						base.OnOpenSelectedItem(EventArgs.Empty);
						break;
					case MNU_ADD:		
						//Read new yardSection details (defaults), forward to dlgYardSection for update
						yardSectionID = 0;
						dsYardSection = TransportationFactory.GetYardSection(this.m_iYardID, yardSectionID);
						dlgYardSection = new dlgYardSectionDetail(this.m_sTerminal, base.Text, this.mParentIsActive, ref dsYardSection);
						res = dlgYardSection.ShowDialog();
						if(res==DialogResult.OK) {
							//Request a new yardSection
							yardSectionID = TransportationFactory.CreateYardSection(dsYardSection);
							yardSectionName = dsYardSection.YardSectionDetailTable[0].SectionNumber;
							if(yardSectionID>0) {

								MessageBox.Show("Yard section " + yardSectionName + " was created.", "Yard Section Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("New yard section could not be created. Please try again.", "Yard Section Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_EDIT:		
						//Read existing yardSection details, forward to dlgYardSection for update
						yardSectionID = Convert.ToInt32(mCurrentItem.SubItems[5].Text);
						dsYardSection = TransportationFactory.GetYardSection(this.m_iYardID, yardSectionID);
						dlgYardSection = new dlgYardSectionDetail(this.m_sTerminal, base.Text, this.mParentIsActive, ref dsYardSection);
						res = dlgYardSection.ShowDialog();
						if(res==DialogResult.OK) {
							//Request an update to the yardSection
							val = TransportationFactory.UpdateYardSection(dsYardSection);
							yardSectionName = mCurrentItem.SubItems[0].Text;
							if(val) {
								MessageBox.Show("Yard section " + yardSectionName + " was updated.", "Yard Section Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Yard section " + yardSectionName + " could not be updated. Please try again.", "Yard Section Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_REMOVE:	break;
					case MNU_PROPS:		break;
					default:			break;
				}			
			}
			catch(Exception ex) { base.OnErrorMessage(ex); }
		}
	}
	#endregion
					#region YardLocationAdminNode
	//---------------------------------------------------------------------------
	//The YardLocationAdminNode administers the collection of terminal yard locations
	public class YardLocationAdminNode: AdminNode {
		//Attributes
		private int m_iSectionID;
		private string m_sYard = "";
		private string m_sTerminal = "";
		private bool mParentIsActive = true;
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Yard Location";
		private const string MNU_EDIT = "Update Yard Location";
		private const string MNU_REMOVE = "Delete Yard Location";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public YardLocationAdminNode(string text, int imageIndex, int selectedImageIndex, int sectionID, string yard, string terminal, bool parentIsActive) : base(text, imageIndex, selectedImageIndex) {
			//Constructor

			try {
				//Set custom attributes and create a custom listview service menu
				this.m_iSectionID = sectionID;
				this.m_sYard = yard;
				this.m_sTerminal = terminal;
				this.mParentIsActive = parentIsActive;
				base.mnuLSV = new ContextMenu();
				base.mnuLSVOpen = new MenuItem(MNU_OPEN, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep0 = new MenuItem("-");
				base.mnuLSVAdd = new MenuItem(MNU_ADD, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVEdit = new MenuItem(MNU_EDIT, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVRemove = new MenuItem(MNU_REMOVE, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep1 = new MenuItem("-");
				base.mnuLSVProps = new MenuItem(MNU_PROPS, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSV.MenuItems.AddRange(new MenuItem[] {base.mnuLSVOpen, base.mnuLSVSep0, base.mnuLSVAdd, base.mnuLSVEdit, base.mnuLSVRemove, base.mnuLSVSep1, base.mnuLSVProps});
				base.mnuLSVOpen.Enabled = false; base.mnuLSVAdd.Enabled = true; base.mnuLSVEdit.Enabled = base.mnuLSVRemove.Enabled = false; base.mnuLSVProps.Enabled = false;
			}
			catch(Exception ex) { throw(ex); }
		}
		
		//Listview support --------------------------------------------------
		public override ColumnHeader[] header() { 
			//Create listview column headers
			ColumnHeader[] headers = null;
			ColumnHeader colName = new ColumnHeader(); colName.Text = "Number"; colName.Width = 96;
			ColumnHeader colLocType = new ColumnHeader(); colLocType.Text = "Description"; colLocType.Width = 288;
			ColumnHeader colTerm = new ColumnHeader(); colTerm.Text = "Terminal"; colTerm.Width = 0;
			ColumnHeader colYard = new ColumnHeader(); colYard.Text = "Yard"; colYard.Width = 0;
			ColumnHeader colSec = new ColumnHeader(); colSec.Text = "SectionNumber"; colSec.Width = 0;
			ColumnHeader colIsActive = new ColumnHeader(); colIsActive.Text = "Active"; colIsActive.Width = 60;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "YardLocationID"; colID.Width = 0;
			headers = new ColumnHeader[] {colName, colLocType, colTerm, colYard, colSec, colIsActive, colID};
			return headers; 
		}
		public override ListViewItem[] list() { 
			//Set listview items (data)
			YardLocationDS lst = TransportationFactory.ViewYardLocations(m_iSectionID);
			ListViewItem[] items = new ListViewItem[lst.YardLocationViewTable.Rows.Count];
			for(int i=0; i<lst.YardLocationViewTable.Rows.Count; i++) {
				string[] subitems = {	lst.YardLocationViewTable[i].Number.Trim(), 
										lst.YardLocationViewTable[i].LocationType.ToString(), 
										lst.YardLocationViewTable[i].Terminal.Trim(), 
										lst.YardLocationViewTable[i].YardName.Trim(), 
										lst.YardLocationViewTable[i].SectionNumber.Trim(), 
										lst.YardLocationViewTable[i].IsActive.ToString(), 
										lst.YardLocationViewTable[i].YardLocationID.ToString() };
				ListViewItem item = new ListViewItem(subitems, 2);
				items[i] = item;
			}
			return items;
		}
		
		protected override void OnItemMenuClick(object sender, System.EventArgs e) {
			//Context menu event handler
			MenuItem menu;
			YardLocationDS dsYardLocation;
			dlgYardLocationDetail dlgYardLocation;
			int yardLocationID = 0;
			string yardLocationName = "";
			DialogResult res;
			bool val;
			
			try {
				//Context menu item clicked-apply selected service; forward to menu handler
				menu = (MenuItem)sender;
				switch(menu.Text)  {
					case MNU_OPEN:		
						base.mnuLSVEdit.PerformClick();
						break;
					case MNU_ADD:		
						//Read new yardLocation details (defaults), forward to dlgYardLocation for update
						yardLocationID = 0;
						dsYardLocation = TransportationFactory.GetYardLocation(this.m_iSectionID, yardLocationID);
						dlgYardLocation = new dlgYardLocationDetail(m_sTerminal, m_sYard, base.Text, this.mParentIsActive, ref dsYardLocation);
						res = dlgYardLocation.ShowDialog();
						if(res==DialogResult.OK) {
							//Request a new yardLocation
							yardLocationID = TransportationFactory.CreateYardLocation(dsYardLocation);
							yardLocationName = dsYardLocation.YardLocationDetailTable[0].Number;
							if(yardLocationID>0) {
								MessageBox.Show("Yard location " + yardLocationName + " was created.", "Yard Location Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("New yard location could not be created. Please try again.", "Yard Location Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_EDIT:		
						//Read existing yardLocation details, forward to dlgYardLocation for update
						yardLocationID = Convert.ToInt32(mCurrentItem.SubItems[6].Text);
						dsYardLocation = TransportationFactory.GetYardLocation(this.m_iSectionID, yardLocationID);
						dlgYardLocation = new dlgYardLocationDetail(m_sTerminal, m_sYard, base.Text, this.mParentIsActive, ref dsYardLocation);
						res = dlgYardLocation.ShowDialog();
						if(res==DialogResult.OK) {
							//Request an update to the yardLocation
							val = TransportationFactory.UpdateYardLocation(dsYardLocation);
							yardLocationName = mCurrentItem.SubItems[0].Text;
							if(val) {
								MessageBox.Show("Yard location " + yardLocationName + " was updated.", "Yard Location Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Yard location " + yardLocationName + " could not be updated. Please try again.", "Yard Location Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_REMOVE:	break;
					case MNU_PROPS:		break;
					default:			break;
				}			
			}
			catch(Exception ex) { base.OnErrorMessage(ex); }
		}
	}
	#endregion
	#region TrailerAdminNode
	//---------------------------------------------------------------------------
	//The TrailerAdminNode administers the collection of enterprise trailers
	public class TrailerAdminNode: AdminNode {
		//Attributes
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Trailer";
		private const string MNU_EDIT = "Update Trailer";
		private const string MNU_REMOVE = "Delete Trailer";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public TrailerAdminNode(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) {
			//Constructor
			try {
				//Create a custom listview service menu
				base.mnuLSV = new ContextMenu();
				base.mnuLSVOpen = new MenuItem(MNU_OPEN, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep0 = new MenuItem("-");
				base.mnuLSVAdd = new MenuItem(MNU_ADD, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVEdit = new MenuItem(MNU_EDIT, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVRemove = new MenuItem(MNU_REMOVE, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep1 = new MenuItem("-");
				base.mnuLSVProps = new MenuItem(MNU_PROPS, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSV.MenuItems.AddRange(new MenuItem[] {base.mnuLSVOpen, base.mnuLSVSep0, base.mnuLSVAdd, base.mnuLSVEdit, base.mnuLSVRemove, base.mnuLSVSep1, base.mnuLSVProps});
				base.mnuLSVOpen.Enabled = false; base.mnuLSVAdd.Enabled = true; base.mnuLSVEdit.Enabled = base.mnuLSVRemove.Enabled = false; base.mnuLSVProps.Enabled = false;
			}
			catch(Exception ex) { throw(ex); }
		}
		
		//Listview support --------------------------------------------------
		public override ColumnHeader[] header() { 
			//Return listview column headers for base object
			ColumnHeader[] headers = null;
			ColumnHeader colNumber = new ColumnHeader(); colNumber.Text = "Number"; colNumber.Width = 96;
			ColumnHeader colDef = new ColumnHeader(); colDef.Text = "Type"; colDef.Width = 192;
			ColumnHeader colOwner = new ColumnHeader(); colOwner.Text = "Owner"; colOwner.Width = 192;
			ColumnHeader colLic = new ColumnHeader(); colLic.Text = "Lic #"; colLic.Width = 0;
			ColumnHeader colIsStorage = new ColumnHeader(); colIsStorage.Text = "Storage"; colIsStorage.Width = 60;
			ColumnHeader colIsActive = new ColumnHeader(); colIsActive.Text = "Active"; colIsActive.Width = 60;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "TrailerID"; colID.Width = 0;
			headers = new ColumnHeader[] {colNumber, colDef, colOwner, colLic, colIsStorage, colIsActive, colID};
			return headers;
		}
		public override ListViewItem[] list() { 
			//Return listview items (data) for base object
			TrailerDS lst = TransportationFactory.ViewTrailers();
			ListViewItem[] items = new ListViewItem[lst.TrailerListTable.Rows.Count];
			for(int i=0; i<lst.TrailerListTable.Rows.Count; i++) {
				string[] subitems = {	lst.TrailerListTable[i].Number.Trim(), 
										lst.TrailerListTable[i].Definition, 
										lst.TrailerListTable[i].CarrierName, 
										lst.TrailerListTable[i].LicPlateNumber, 
										lst.TrailerListTable[i].IsStorage.ToString(), 
										lst.TrailerListTable[i].IsActive.ToString(), 
										lst.TrailerListTable[i].TrailerID.ToString()};
				ListViewItem item = new ListViewItem(subitems, 2);
				items[i] = item;
			}
			return items;
		}
		
		protected override void OnItemMenuClick(object sender, System.EventArgs e) {
			//Context menu event handler
			MenuItem menu;
			Tsort.Transportation.TrailerDS dsTrailer;
			dlgTrailerDetail dlgTrailer;
			int trailerID = 0;
			string trailerNum = "";
			DialogResult res;
			bool val;
			
			try {
				//Context menu item clicked-apply selected service; forward to menu handler
				menu = (MenuItem)sender;
				switch(menu.Text)  {
					case MNU_OPEN:		
						base.mnuLSVEdit.PerformClick();
						break;
					case MNU_ADD:		
						//Read new trailer details (defaults), forward to dlgTrailer for update
						trailerID = 0;
						dsTrailer = TransportationFactory.GetTrailer(trailerID);
						dlgTrailer = new dlgTrailerDetail(ref dsTrailer);
						res = dlgTrailer.ShowDialog();
						if(res==DialogResult.OK) {
							//Request a new trailer
							trailerID = TransportationFactory.CreateTrailer(dsTrailer);
							if(trailerID>0) {
								trailerNum = dsTrailer.TrailerDetailTable[0].Number;
								MessageBox.Show("Trailer " + trailerNum + " was created.", "Trailer Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("New trailer could not be created. Please try again.", "Trailer Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_EDIT:		
						//Read existing trailer details, forward to dlgTrailer for update
						trailerID = Convert.ToInt32(mCurrentItem.SubItems[6].Text);
						dsTrailer = TransportationFactory.GetTrailer(trailerID);
						dlgTrailer = new dlgTrailerDetail(ref dsTrailer);
						res = dlgTrailer.ShowDialog();
						if(res==DialogResult.OK) {
							//Request an update to the trailer 
							val = TransportationFactory.UpdateTrailer(dsTrailer);
							trailerNum = dsTrailer.TrailerDetailTable[0].Number;
							if(val) {
								MessageBox.Show("Trailer " + trailerNum + " was updated.", "Trailer Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Trailer " + trailerNum + " could not be updated. Please try again.", "Trailer Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_REMOVE:	break;
					case MNU_PROPS:		break;
					default:			break;
				}				
			}
			catch(Exception ex) { base.OnErrorMessage(ex); }
		}
	}
	#endregion
	#region VehicleAdminNode
	//---------------------------------------------------------------------------
	//The DriverAdminNode administers the collection of all enterprise vehicles
	public class VehicleAdminNode: AdminNode {
		//Members
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Driver Vehicle";
		private const string MNU_EDIT = "Update Driver Vehicle";
		private const string MNU_REMOVE = "Delete Driver Vehicle";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public VehicleAdminNode(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) {
			//Constructor
			try {
				//Create a custom listview service menu
				base.mnuLSV = new ContextMenu();
				base.mnuLSVOpen = new MenuItem(MNU_OPEN, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep0 = new MenuItem("-");
				base.mnuLSVAdd = new MenuItem(MNU_ADD, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVEdit = new MenuItem(MNU_EDIT, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVRemove = new MenuItem(MNU_REMOVE, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep1 = new MenuItem("-");
				base.mnuLSVProps = new MenuItem(MNU_PROPS, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSV.MenuItems.AddRange(new MenuItem[] {base.mnuLSVOpen, base.mnuLSVSep0, base.mnuLSVAdd, base.mnuLSVEdit, base.mnuLSVRemove, base.mnuLSVSep1, base.mnuLSVProps});
				base.mnuLSVOpen.Enabled = false; base.mnuLSVAdd.Enabled = true; base.mnuLSVEdit.Enabled = base.mnuLSVRemove.Enabled = false; base.mnuLSVProps.Enabled = false;
			}
			catch(Exception ex) { throw(ex); }
		}
		
		//Listview support --------------------------------------------------
		public override ColumnHeader[] header() { 
			//Return listview column headers for base object
			ColumnHeader[] headers = null;
			ColumnHeader colDesc = new ColumnHeader(); colDesc.Text = "Description"; colDesc.Width = 144;
			ColumnHeader colType = new ColumnHeader(); colType.Text = "Type"; colType.Width = 96;
			ColumnHeader colDriver = new ColumnHeader(); colDriver.Text = "Driver"; colDriver.Width = 144;
			ColumnHeader colTerminal = new ColumnHeader(); colTerminal.Text = "Terminal"; colTerminal.Width = 168;
			ColumnHeader colIsActive = new ColumnHeader(); colIsActive.Text = "Active"; colIsActive.Width = 60;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "VehicleID"; colID.Width = 0;
			headers = new ColumnHeader[] {colDesc, colType, colDriver, colTerminal, colIsActive, colID};
			return headers; 
		}
		public override ListViewItem[] list() { 
			//Return listview items (data) for base object
			VehicleDS lst = TransportationFactory.ViewDriverVehicles();
			ListViewItem[] items = new ListViewItem[lst.VehicleListTable.Rows.Count];
			for(int i=0; i<lst.VehicleListTable.Rows.Count; i++) {
				string[] subitems = {	lst.VehicleListTable[i].Description.Trim(), 
										lst.VehicleListTable[i].VehicleType.Trim(), 
										lst.VehicleListTable[i].LastName.Trim() + ", " + lst.VehicleListTable[i].FirstName.Trim(), 
										lst.VehicleListTable[i].Terminal.Trim(), 
										lst.VehicleListTable[i].IsActive.ToString(), 
										lst.VehicleListTable[i].VehicleID.ToString() };
				ListViewItem item = new ListViewItem(subitems, 2);
				items[i] = item;
			}
			return items;
		}
		
		protected override void OnItemMenuClick(object sender, System.EventArgs e) {
			//Context menu event handler
			MenuItem menu;
			VehicleDS dsVehicle;
			dlgVehicleDetail dlgVehicle;
			int vehicleID = 0;
			string vehicleDesc = "", vehicleType = "";
			DialogResult res;
			bool val;
			
			try {
				//Context menu item clicked-apply selected service; forward to menu handler
				menu = (MenuItem)sender;
				switch(menu.Text)  {
					case MNU_OPEN:		
						base.mnuLSVEdit.PerformClick();
						break;
					case MNU_ADD:		
						//Read new vehicle details (defaults), forward to dlgVehicle for update
						vehicleID = 0;
						dsVehicle = TransportationFactory.GetDriverVehicle(vehicleID, "");
						dlgVehicle = new dlgVehicleDetail(ref dsVehicle);
						res = dlgVehicle.ShowDialog();
						if(res==DialogResult.OK) {
							//Request a new vehicle
							vehicleID = TransportationFactory.CreateDriverVehicle(dsVehicle);
							vehicleDesc = dsVehicle.VehicleDetailTable[0].Description;
							if(vehicleID>0) {
								MessageBox.Show("Driver vehicle " + vehicleDesc + " was created.", "Driver Vehicle Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("New driver vehicle could not be created. Please try again.", "Driver Vehicle Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_EDIT:		
						//Read existing vehicle details, forward to dlgDriver for update
						vehicleID = Convert.ToInt32(mCurrentItem.SubItems[5].Text);
						vehicleType = mCurrentItem.SubItems[1].Text;
						dsVehicle = TransportationFactory.GetDriverVehicle(vehicleID, vehicleType);
						dlgVehicle = new dlgVehicleDetail(ref dsVehicle);
						res = dlgVehicle.ShowDialog();
						if(res==DialogResult.OK) {
							//Request an update to the vehicle
							val = TransportationFactory.UpdateDriverVehicle(dsVehicle);
							if(val) {
								vehicleDesc = dsVehicle.VehicleDetailTable[0].Description;
								MessageBox.Show("Driver vehicle " + vehicleDesc + " was updated.", "Driver Vehicle Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Driver vehicle " + vehicleDesc + " could not be updated. Please try again.", "Driver Vehicle Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_REMOVE:	break;
					case MNU_PROPS:		break;
					default:			break;
				}
			}
			catch(Exception ex) { base.OnErrorMessage(ex); }
		}
	}
	#endregion
}
