//	File:	businessadmin.cs
//	Author:	J. Heary
//	Date:	04/27/06
//	Desc:	Class definitions to administer Enterprise entities.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using Tsort.Data;
using Tsort.Enterprise;
using Tsort.Freight;
using Tsort.Transportation;

namespace Tsort {	
	//Implementation Classes
	#region ClientAdminNode
	//---------------------------------------------------------------------------
	//
	public class ClientAdminNode: AdminNode {
		//Mmembers
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Client";
		private const string MNU_EDIT = "Update Client";
		private const string MNU_REMOVE = "Delete Client";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public ClientAdminNode(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) {
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
			ClientDS lst = EnterpriseFactory.ViewClients();
			base.mChildNodes = new TreeNode[lst.ClientViewTable.Rows.Count];
			for(int i=0; i<lst.ClientViewTable.Rows.Count; i++) {
				StoreAdminNode node = new StoreAdminNode(lst.ClientViewTable[i].ClientName.Trim(), 0, 1, (int)lst.ClientViewTable[i].ClientID, lst.ClientViewTable[i].IsActive);
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
			ColumnHeader colName = new ColumnHeader(); colName.Text = "Client Name"; colName.Width = 192;
			ColumnHeader colNumber = new ColumnHeader(); colNumber.Text = "Number"; colNumber.Width = 60;
			ColumnHeader colPhone = new ColumnHeader(); colPhone.Text = "Phone #"; colPhone.Width = 102;
			ColumnHeader colExt = new ColumnHeader(); colExt.Text = "Ext"; colExt.Width = 36;
			ColumnHeader colAddress = new ColumnHeader(); colAddress.Text = "Address"; colAddress.Width = 288;
			ColumnHeader colContact = new ColumnHeader(); colContact.Text = "Contact"; colContact.Width = 120;
			ColumnHeader colFax = new ColumnHeader(); colFax.Text = "Fax #"; colFax.Width = 102;
			ColumnHeader colEmail = new ColumnHeader(); colEmail.Text = "eMail"; colEmail.Width = 96;
			ColumnHeader colIsActive = new ColumnHeader(); colIsActive.Text = "Active"; colIsActive.Width = 60;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "ID"; colID.Width = 0;
			headers = new ColumnHeader[] {colName, colNumber, colPhone, colExt, colAddress, colContact, colFax, colEmail, colIsActive, colID};
			return headers;
		}
		public override ListViewItem[] list() { 
			//Set listview items (data)
			ClientDS lst = EnterpriseFactory.ViewClients();
			ListViewItem[] items = new ListViewItem[lst.ClientViewTable.Rows.Count];
			for(int i=0; i<lst.ClientViewTable.Rows.Count; i++) {
				string[] subitems = {	lst.ClientViewTable[i].ClientName.Trim(), 
										lst.ClientViewTable[i].Number.Trim(), 
										lst.ClientViewTable[i].Phone.Trim(), 
										lst.ClientViewTable[i].Extension.Trim(), 
										lst.ClientViewTable[i].AddressLine1.Trim() + " " + lst.ClientViewTable[i].AddressLine2.Trim() + " " + lst.ClientViewTable[i].City.Trim() + ", " + lst.ClientViewTable[i].StateOrProvince.Trim() + " " + lst.ClientViewTable[i].PostalCode.Trim(), 
										lst.ClientViewTable[i].ContactName.Trim(), 
										lst.ClientViewTable[i].Fax.Trim(), 
										lst.ClientViewTable[i].Email.Trim(), 
										lst.ClientViewTable[i].IsActive.ToString(), 
										lst.ClientViewTable[i].ClientID.ToString()};
				ListViewItem item = new ListViewItem(subitems, 0);
				items[i] = item;
			}
			return items;
		}
		
		protected override void OnItemMenuClick(object sender, System.EventArgs e) {
			//Context menu event handler
			MenuItem menu=null;
			ClientDS dsClient=null;
			dlgClientDetail dlgClient=null;
			int clientID=0;
			string clientName="";
			DialogResult res=DialogResult.Cancel;
			bool val=false;
			
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
						clientID = 0;
						dsClient = EnterpriseFactory.GetClient(clientID);
						dlgClient = new dlgClientDetail(ref dsClient);
						res = dlgClient.ShowDialog();
						if(res==DialogResult.OK) {
							//Request a new carrier
							clientID = EnterpriseFactory.CreateClient(dsClient);
							if(clientID>0) {
								clientName = dsClient.ClientDetailTable[0].ClientName;
								MessageBox.Show("Client " + clientName + " was created.", "Client Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("New client could not be created. Please try again.", "Client Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_EDIT:		
						//Read existing carrier details, forward to dlgCarrier for update
						clientID = Convert.ToInt32(base.mCurrentItem.SubItems[9].Text);
						dsClient = EnterpriseFactory.GetClient(clientID);
						dlgClient = new dlgClientDetail(ref dsClient);
						res = dlgClient.ShowDialog();
						if(res==DialogResult.OK) {
							//Request an update to the client 
							val = EnterpriseFactory.UpdateClient(dsClient);
							clientName = dsClient.ClientDetailTable[0].ClientName;
							if(val) {
								MessageBox.Show("Client " + clientName + " was updated.", "Client Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Client " + clientName + " could not be updated. Please try again.", "Client Admin", MessageBoxButtons.OK);
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
	#region AgentAdminNode
	//---------------------------------------------------------------------------
	//
	public class AgentAdminNode: AdminNode {
		//Data members
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Agent";
		private const string MNU_EDIT = "Update Agent";
		private const string MNU_REMOVE = "Delete Agent";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public AgentAdminNode(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) {
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
			AgentDS lst = EnterpriseFactory.ViewAgents();
			base.mChildNodes = new TreeNode[lst.AgentViewTable.Rows.Count];
			for(int i=0; i<lst.AgentViewTable.Rows.Count; i++) {
				TerminalAdminNode node = new TerminalAdminNode(lst.AgentViewTable[i].AgentName.Trim(), 0, 1, (int)lst.AgentViewTable[i].AgentID, lst.AgentViewTable[i].IsActive);
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
			ColumnHeader colName = new ColumnHeader(); colName.Text = "Agent Name"; colName.Width = 192;
			ColumnHeader colNumber = new ColumnHeader(); colNumber.Text = "Number"; colNumber.Width = 60;
			ColumnHeader colPhone = new ColumnHeader(); colPhone.Text = "Phone #"; colPhone.Width = 102;
			ColumnHeader colExt = new ColumnHeader(); colExt.Text = "Ext"; colExt.Width = 36;
			ColumnHeader colAddress = new ColumnHeader(); colAddress.Text = "Address"; colAddress.Width = 288;
			ColumnHeader colContact = new ColumnHeader(); colContact.Text = "Contact"; colContact.Width = 120;
			ColumnHeader colFax = new ColumnHeader(); colFax.Text = "Fax #"; colFax.Width = 102;
			ColumnHeader colEmail = new ColumnHeader(); colEmail.Text = "eMail"; colEmail.Width = 96;
			ColumnHeader colIsActive = new ColumnHeader(); colIsActive.Text = "Active"; colIsActive.Width = 60;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "ID"; colID.Width = 0;
			headers = new ColumnHeader[] {colName, colNumber, colPhone, colExt, colAddress, colContact, colFax, colEmail, colIsActive, colID};
			return headers;
		}
		public override ListViewItem[] list() { 
			//Set listview items (data)
			AgentDS lst = EnterpriseFactory.ViewAgents();
			ListViewItem[] items = new ListViewItem[lst.AgentViewTable.Rows.Count];
			for(int i=0; i<lst.AgentViewTable.Rows.Count; i++) {
				string[] subitems = {	lst.AgentViewTable[i].AgentName.Trim(), 
										lst.AgentViewTable[i].Number.Trim(), 
										lst.AgentViewTable[i].Phone.Trim(), 
										lst.AgentViewTable[i].Extension.Trim(), 
										lst.AgentViewTable[i].AddressLine1.Trim() + " " + lst.AgentViewTable[i].AddressLine2.Trim() + " " + lst.AgentViewTable[i].City.Trim() + ", " + lst.AgentViewTable[i].StateOrProvince.Trim() + " " + lst.AgentViewTable[i].PostalCode.Trim(), 
										lst.AgentViewTable[i].ContactName.Trim(), 
										lst.AgentViewTable[i].Fax.Trim(), 
										lst.AgentViewTable[i].Email.Trim(), 
										lst.AgentViewTable[i].IsActive.ToString(), 
										lst.AgentViewTable[i].AgentID.ToString()};
				ListViewItem item = new ListViewItem(subitems, 0);
				items[i] = item;
			}
			return items;
		}
		
		protected override void OnItemMenuClick(object sender, System.EventArgs e) {
			//Context menu event handler
			MenuItem menu;
			AgentDS agentDS;
			dlgAgentDetail dlgAgent;
			int agentID = 0;
			string agentName = "";
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
						agentID = 0;
						agentDS = EnterpriseFactory.GetAgent(agentID);
						dlgAgent = new dlgAgentDetail(ref agentDS);
						res = dlgAgent.ShowDialog();
						if(res==DialogResult.OK) {
							//Request a new agent
							agentID = EnterpriseFactory.CreateAgent(agentDS);
							if(agentID>0) {
								agentName = agentDS.AgentDetailTable[0].AgentName;
								MessageBox.Show("Agent " + agentName + " was created.", "Agent Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("New agent could not be created. Please try again.", "Agent Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_EDIT:		
						//Read existing carrier details, forward to dlgCarrier for update
						agentID = Convert.ToInt32(base.mCurrentItem.SubItems[9].Text);
						agentDS = EnterpriseFactory.GetAgent(agentID);
						dlgAgent = new dlgAgentDetail(ref agentDS);
						res = dlgAgent.ShowDialog();
						if(res==DialogResult.OK) {
							//Request an update to the agent 
							val = EnterpriseFactory.UpdateAgent(agentDS);
							agentName = agentDS.AgentDetailTable[0].AgentName;
							if(val) {
								MessageBox.Show("Agent " + agentName + " was updated.", "Agent Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Agent " + agentName + " could not be updated. Please try again.", "Agent Admin", MessageBoxButtons.OK);
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
	#region VendorAdminNode
	//---------------------------------------------------------------------------
	//
	public class VendorAdminNode: AdminNode {
		//Data members
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Vendor";
		private const string MNU_EDIT = "Update Vendor";
		private const string MNU_REMOVE = "Delete Vendor";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public VendorAdminNode(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) {
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
			VendorDS lst = EnterpriseFactory.ViewVendors();
			base.mChildNodes = new TreeNode[lst.VendorViewTable.Rows.Count];
			for(int i=0; i<lst.VendorViewTable.Rows.Count; i++) {
				WarehouseAdminNode node = new WarehouseAdminNode(lst.VendorViewTable[i].VendorName.Trim(), 0, 1, (int)lst.VendorViewTable[i].VendorID, lst.VendorViewTable[i].IsActive);
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
			ColumnHeader colName = new ColumnHeader(); colName.Text = "Vendor Name"; colName.Width = 192;
			ColumnHeader colNumber = new ColumnHeader(); colNumber.Text = "Number"; colNumber.Width = 60;
			ColumnHeader colPhone = new ColumnHeader(); colPhone.Text = "Phone #"; colPhone.Width = 102;
			ColumnHeader colExt = new ColumnHeader(); colExt.Text = "Ext"; colExt.Width = 36;
			ColumnHeader colAddress = new ColumnHeader(); colAddress.Text = "Address"; colAddress.Width = 288;
			ColumnHeader colContact = new ColumnHeader(); colContact.Text = "Contact"; colContact.Width = 120;
			ColumnHeader colFax = new ColumnHeader(); colFax.Text = "Fax #"; colFax.Width = 102;
			ColumnHeader colEmail = new ColumnHeader(); colEmail.Text = "eMail"; colEmail.Width = 96;
			ColumnHeader colIsActive = new ColumnHeader(); colIsActive.Text = "Active"; colIsActive.Width = 60;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "ID"; colID.Width = 0;
			headers = new ColumnHeader[] {colName, colNumber, colPhone, colExt, colAddress, colContact, colFax, colEmail, colIsActive, colID};
			return headers;
		}
		public override ListViewItem[] list() { 
			//Set listview items (data)
			VendorDS lst = EnterpriseFactory.ViewVendors();
			ListViewItem[] items = new ListViewItem[lst.VendorViewTable.Rows.Count];
			for(int i=0; i<lst.VendorViewTable.Rows.Count; i++) {
				string[] subitems = {	lst.VendorViewTable[i].VendorName.Trim(), 
										lst.VendorViewTable[i].Number.Trim(), 
										lst.VendorViewTable[i].Phone.Trim(), 
										lst.VendorViewTable[i].Extension.Trim(), 
										lst.VendorViewTable[i].AddressLine1.Trim() + " " + lst.VendorViewTable[i].AddressLine2.Trim() + " " + lst.VendorViewTable[i].City.Trim() + ", " + lst.VendorViewTable[i].StateOrProvince.Trim() + " " + lst.VendorViewTable[i].PostalCode.Trim(), 
										lst.VendorViewTable[i].ContactName.Trim(), 
										lst.VendorViewTable[i].Fax.Trim(), 
										lst.VendorViewTable[i].Email.Trim(), 
										lst.VendorViewTable[i].IsActive.ToString(), 
										lst.VendorViewTable[i].VendorID.ToString()};
				ListViewItem item = new ListViewItem(subitems, 0);
				items[i] = item;
			}
			return items;
		}
		
		protected override void OnItemMenuClick(object sender, System.EventArgs e) {
			//Context menu event handler
			MenuItem menu;
			VendorDS dsVendor;
			dlgVendorDetail dlgVendor;
			int vendorID = 0;
			string vendorName = "";
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
						vendorID = 0;
						dsVendor = EnterpriseFactory.GetVendor(vendorID);
						dlgVendor = new dlgVendorDetail(ref dsVendor);
						res = dlgVendor.ShowDialog();
						if(res==DialogResult.OK) {
							//Request a new vendor
							vendorID = EnterpriseFactory.CreateVendor(dsVendor);
							if(vendorID>0) {
								vendorName = dsVendor.VendorDetailTable[0].VendorName;
								MessageBox.Show("Vendor " + vendorName + " was created.", "Vendor Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("New vendor could not be created. Please try again.", "Vendor Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_EDIT:		
						//Read existing carrier details, forward to dlgCarrier for update
						vendorID = Convert.ToInt32(base.mCurrentItem.SubItems[9].Text);
						dsVendor = EnterpriseFactory.GetVendor(vendorID);
						dlgVendor = new dlgVendorDetail(ref dsVendor);
						res = dlgVendor.ShowDialog();
						if(res==DialogResult.OK) {
							//Request an update to the vendor 
							val = EnterpriseFactory.UpdateVendor(dsVendor);
							vendorName = dsVendor.VendorDetailTable[0].VendorName;
							if(val) {
								MessageBox.Show("Vendor " + vendorName + " was updated.", "Vendor Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Vendor " + vendorName + " could not be updated. Please try again.", "Vendor Admin", MessageBoxButtons.OK);
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
	#region CarrierAdminNode
	//---------------------------------------------------------------------------
	//
	public class CarrierAdminNode: AdminNode {
		//Data members
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Carrier";
		private const string MNU_EDIT = "Update Carrier";
		private const string MNU_REMOVE = "Delete Carrier";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public CarrierAdminNode(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) {
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
		
		//Listview support --------------------------------------------------
		public override ColumnHeader[] header() { 
			//Create listview column headers
			ColumnHeader[] headers = null;
			ColumnHeader colName = new ColumnHeader(); colName.Text = "Carrier Name"; colName.Width = 192;
			ColumnHeader colNumber = new ColumnHeader(); colNumber.Text = "Number"; colNumber.Width = 60;
			ColumnHeader colPhone = new ColumnHeader(); colPhone.Text = "Phone #"; colPhone.Width = 102;
			ColumnHeader colExt = new ColumnHeader(); colExt.Text = "Ext"; colExt.Width = 36;
			ColumnHeader colAddress = new ColumnHeader(); colAddress.Text = "Address"; colAddress.Width = 288;
			ColumnHeader colContact = new ColumnHeader(); colContact.Text = "Contact"; colContact.Width = 120;
			ColumnHeader colFax = new ColumnHeader(); colFax.Text = "Fax #"; colFax.Width = 102;
			ColumnHeader colEmail = new ColumnHeader(); colEmail.Text = "eMail"; colEmail.Width = 96;
			ColumnHeader colIsActive = new ColumnHeader(); colIsActive.Text = "Active"; colIsActive.Width = 60;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "ID"; colID.Width = 0;
			headers = new ColumnHeader[] {colName, colNumber, colPhone, colExt, colAddress, colContact, colFax, colEmail, colIsActive, colID};
			return headers;
		}
		public override ListViewItem[] list() { 
			//Set listview items (data)
			CarrierDS lst = TransportationFactory.ViewCarriers();
			ListViewItem[] items = new ListViewItem[lst.CarrierViewTable.Rows.Count];
			for(int i=0; i<lst.CarrierViewTable.Rows.Count; i++) {
				string[] subitems = {	lst.CarrierViewTable[i].CarrierName.Trim(), 
										lst.CarrierViewTable[i].Number.Trim(), 
										lst.CarrierViewTable[i].Phone.Trim(), 
										lst.CarrierViewTable[i].Extension.Trim(), 
										lst.CarrierViewTable[i].AddressLine1.Trim() + " " + lst.CarrierViewTable[i].AddressLine2.Trim() + " " + lst.CarrierViewTable[i].City.Trim() + ", " + lst.CarrierViewTable[i].StateOrProvince.Trim() + " " + lst.CarrierViewTable[i].PostalCode.Trim(), 
										lst.CarrierViewTable[i].ContactName.Trim(), 
										lst.CarrierViewTable[i].Fax.Trim(), 
										lst.CarrierViewTable[i].Email.Trim(), 
										lst.CarrierViewTable[i].IsActive.ToString(), 
										lst.CarrierViewTable[i].CarrierID.ToString()};
				ListViewItem item = new ListViewItem(subitems, 2);
				items[i] = item;
			}
			return items;
		}
		
		protected override void OnItemMenuClick(object sender, System.EventArgs e) {
			//Context menu event handler
			MenuItem menu;
			CarrierDS dsCarrier;
			dlgCarrierDetail dlgCarrier;
			int carrierID = 0;
			string carrierName = "";
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
						carrierID = 0;
						dsCarrier = TransportationFactory.GetCarrier(carrierID);
						dlgCarrier = new dlgCarrierDetail(ref dsCarrier);
						res = dlgCarrier.ShowDialog();
						if(res==DialogResult.OK) {
							//Request a new carrier
							carrierID = TransportationFactory.CreateCarrier(dsCarrier);
							if(carrierID>0) {
								carrierName = dsCarrier.CarrierDetailTable[0].CarrierName;
								MessageBox.Show("Carrier " + carrierName + " was created.", "Carrier Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("New carrier could not be created. Please try again.", "Carrier Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_EDIT:		
						//Read existing carrier details, forward to dlgCarrier for update
						carrierID = Convert.ToInt32(base.mCurrentItem.SubItems[9].Text);
						dsCarrier = TransportationFactory.GetCarrier(carrierID);
						dlgCarrier = new dlgCarrierDetail(ref dsCarrier);
						res = dlgCarrier.ShowDialog();
						if(res==DialogResult.OK) {
							//Request an update to the carrier 
							val = TransportationFactory.UpdateCarrier(dsCarrier);
							carrierName = dsCarrier.CarrierDetailTable[0].CarrierName;
							if(val) {
								MessageBox.Show("Carrier " + carrierName + " was updated.", "Carrier Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Carrier " + carrierName + " could not be updated. Please try again.", "Carrier Admin", MessageBoxButtons.OK);
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
	#region PaymentServiceAdminNode
	//---------------------------------------------------------------------------
	//
	public class PaymentServiceAdminNode: AdminNode {
		//Data members
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Payment Service";
		private const string MNU_EDIT = "Update Payment Service";
		private const string MNU_REMOVE = "Delete Payment Service";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public PaymentServiceAdminNode(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) {
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
		
		//Listview support --------------------------------------------------
		public override ColumnHeader[] header() { 
			//Create listview column headers
			ColumnHeader[] headers = null;
			ColumnHeader colName = new ColumnHeader(); colName.Text = "Payment Service Name"; colName.Width = 192;
			ColumnHeader colNumber = new ColumnHeader(); colNumber.Text = "Number"; colNumber.Width = 60;
			ColumnHeader colPhone = new ColumnHeader(); colPhone.Text = "Phone #"; colPhone.Width = 102;
			ColumnHeader colExt = new ColumnHeader(); colExt.Text = "Ext"; colExt.Width = 36;
			ColumnHeader colAddress = new ColumnHeader(); colAddress.Text = "Address"; colAddress.Width = 288;
			ColumnHeader colContact = new ColumnHeader(); colContact.Text = "Contact"; colContact.Width = 120;
			ColumnHeader colFax = new ColumnHeader(); colFax.Text = "Fax #"; colFax.Width = 102;
			ColumnHeader colEmail = new ColumnHeader(); colEmail.Text = "eMail"; colEmail.Width = 96;
			ColumnHeader colIsActive = new ColumnHeader(); colIsActive.Text = "Active"; colIsActive.Width = 60;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "ID"; colID.Width = 0;
			headers = new ColumnHeader[] {colName, colNumber, colPhone, colExt, colAddress, colContact, colFax, colEmail, colIsActive, colID};
			return headers;
		}
		public override ListViewItem[] list() { 
			//Set listview items (data)
			PaymentServiceDS lst = EnterpriseFactory.ViewPaymentServices();
			ListViewItem[] items = new ListViewItem[lst.PaymentServiceViewTable.Rows.Count];
			for(int i=0; i<lst.PaymentServiceViewTable.Rows.Count; i++) {
				string[] subitems = {	lst.PaymentServiceViewTable[i].PaymentServiceName.Trim(), 
										lst.PaymentServiceViewTable[i].PaymentServiceNumber.Trim(), 
										lst.PaymentServiceViewTable[i].Phone.Trim(), 
										lst.PaymentServiceViewTable[i].Extension.Trim(), 
										lst.PaymentServiceViewTable[i].AddressLine1.Trim() + " " + lst.PaymentServiceViewTable[i].AddressLine2.Trim() + " " + lst.PaymentServiceViewTable[i].City.Trim() + ", " + lst.PaymentServiceViewTable[i].StateOrProvince.Trim() + " " + lst.PaymentServiceViewTable[i].PostalCode.Trim(), 
										lst.PaymentServiceViewTable[i].ContactName.Trim(), 
										lst.PaymentServiceViewTable[i].Fax.Trim(), 
										lst.PaymentServiceViewTable[i].Email.Trim(), 
										lst.PaymentServiceViewTable[i].IsActive.ToString(), 
										lst.PaymentServiceViewTable[i].PaymentServiceID.ToString()};
				ListViewItem item = new ListViewItem(subitems, 2);
				items[i] = item;
			}
			return items;
		}
		
		protected override void OnItemMenuClick(object sender, System.EventArgs e) {
			//Context menu event handler
			MenuItem menu;
			PaymentServiceDS dsPaymentService;
			dlgPaymentServiceDetail dlgPaymentService;
			int paymentServiceID = 0;
			string paymentServiceName = "";
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
						paymentServiceID = 0;
						dsPaymentService = EnterpriseFactory.GetPaymentService(paymentServiceID);
						dlgPaymentService = new dlgPaymentServiceDetail(ref dsPaymentService);
						res = dlgPaymentService.ShowDialog();
						if(res==DialogResult.OK) {
							//Request a new payment service
							paymentServiceID = EnterpriseFactory.CreatePaymentService(dsPaymentService);
							if(paymentServiceID>0) {
								paymentServiceName = dsPaymentService.PaymentServiceDetailTable[0].PaymentServiceName;
								MessageBox.Show("Payment Service " + paymentServiceName + " was created.", "Payment Service Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("New payment service could not be created. Please try again.", "Payment Service Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_EDIT:		
						//Read existing payment service details, forward to dlgPaymentService for update
						paymentServiceID = Convert.ToInt32(base.mCurrentItem.SubItems[9].Text);
						dsPaymentService = EnterpriseFactory.GetPaymentService(paymentServiceID);
						dlgPaymentService = new dlgPaymentServiceDetail(ref dsPaymentService);
						res = dlgPaymentService.ShowDialog();
						if(res==DialogResult.OK) {
							//Request an update to the payment service 
							val = EnterpriseFactory.UpdatePaymentService(dsPaymentService);
							paymentServiceName = dsPaymentService.PaymentServiceDetailTable[0].PaymentServiceName;
							if(val) {
								MessageBox.Show("Payment Service " + paymentServiceName + " was updated.", "Payment Service Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Payment Service " + paymentServiceName + " could not be updated. Please try again.", "Payment Service Admin", MessageBoxButtons.OK);
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
	#region StoreAdminNode
	//---------------------------------------------------------------------------
	//
	public class StoreAdminNode: AdminNode {
		//Members
		private int m_iClientID;
		private bool m_bClientIsActive = true;
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Store";
		private const string MNU_EDIT = "Update Store";
		private const string MNU_REMOVE = "Delete Store";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public StoreAdminNode(string text, int imageIndex, int selectedImageIndex, int clientID, bool clientIsActive) : base(text, imageIndex, selectedImageIndex) {
			//Constructor
			try {
				//Set custom attributes and create a custom listview service menu
				m_iClientID = clientID;
				m_bClientIsActive = clientIsActive;
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
		
		//Listview support --------------------------------------------------
		public override ColumnHeader[] header() { 
			//Create listview column headers
			ColumnHeader[] headers = null;
			ColumnHeader colName = new ColumnHeader(); colName.Text = "Store Name"; colName.Width = 192;
			ColumnHeader colNumber = new ColumnHeader(); colNumber.Text = "Number"; colNumber.Width = 60;
			ColumnHeader colPhone = new ColumnHeader(); colPhone.Text = "Phone #"; colPhone.Width = 102;
			ColumnHeader colExt = new ColumnHeader(); colExt.Text = "Ext"; colExt.Width = 36;
			ColumnHeader colAddress = new ColumnHeader(); colAddress.Text = "Address"; colAddress.Width = 288;
			ColumnHeader colContact = new ColumnHeader(); colContact.Text = "Contact"; colContact.Width = 120;
			ColumnHeader colFax = new ColumnHeader(); colFax.Text = "Fax #"; colFax.Width = 102;
			ColumnHeader colEmail = new ColumnHeader(); colEmail.Text = "eMail"; colEmail.Width = 96;
			ColumnHeader colIsActive = new ColumnHeader(); colIsActive.Text = "Active"; colIsActive.Width = 60;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "ID"; colID.Width = 0;
			headers = new ColumnHeader[] {colName, colNumber, colPhone, colExt, colAddress, colContact, colFax, colEmail, colIsActive, colID};
			return headers;
		}
		public override ListViewItem[] list() { 
			//Set listview items (data)
			StoreDS lst = EnterpriseFactory.ViewStores(m_iClientID);
			ListViewItem[] items = new ListViewItem[lst.StoreViewTable.Rows.Count];
			for(int i=0; i<lst.StoreViewTable.Rows.Count; i++) {
				string[] subitems = {	lst.StoreViewTable[i].Description.Trim(), 
										lst.StoreViewTable[i].Number.Trim(), 
										lst.StoreViewTable[i].Phone.Trim(), 
										lst.StoreViewTable[i].Extension.Trim(), 
										lst.StoreViewTable[i].AddressLine1.Trim() + " " + lst.StoreViewTable[i].AddressLine2.Trim() + " " + lst.StoreViewTable[i].City.Trim() + ", " + lst.StoreViewTable[i].StateOrProvince.Trim() + " " + lst.StoreViewTable[i].PostalCode.Trim(), 
										lst.StoreViewTable[i].ContactName.Trim(), 
										lst.StoreViewTable[i].Fax.Trim(), 
										lst.StoreViewTable[i].Email.Trim(), 
										lst.StoreViewTable[i].IsActive.ToString(), 
										lst.StoreViewTable[i].LocationID.ToString()};
				ListViewItem item = new ListViewItem(subitems, 2);
				items[i] = item;
			}
			return items;
		}
		
		protected override void OnItemMenuClick(object sender, System.EventArgs e) {
			//Context menu event handler
			MenuItem menu;
			StoreDS dsStore;
			dlgStoreDetail dlgStore;
			int storeID = 0;
			string storeName = "";
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
						storeID = 0;
						dsStore = EnterpriseFactory.GetStore(this.m_iClientID, storeID);
						dlgStore = new dlgStoreDetail(this.m_bClientIsActive, ref dsStore);
						res = dlgStore.ShowDialog();
						if(res==DialogResult.OK) {
							//Request a new store
							storeID = EnterpriseFactory.CreateStore(dsStore);
							if(storeID>0) {
								storeName = dsStore.StoreDetailTable[0].Description;
								MessageBox.Show("Store " + storeName + " was created.", "Store Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("New store could not be created. Please try again.", "Store Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_EDIT:		
						//Read existing carrier details, forward to dlgCarrier for update
						storeID = Convert.ToInt32(base.mCurrentItem.SubItems[9].Text);
						dsStore = EnterpriseFactory.GetStore(this.m_iClientID, storeID);
						dlgStore = new dlgStoreDetail(this.m_bClientIsActive, ref dsStore);
						res = dlgStore.ShowDialog();
						if(res==DialogResult.OK) {
							//Request an update to the store 
							val = EnterpriseFactory.UpdateStore(dsStore);
							storeName = dsStore.StoreDetailTable[0].Description;
							if(val) {
								MessageBox.Show("Store " + storeName + " was updated.", "Store Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Store " + storeName + " could not be updated. Please try again.", "Store Admin", MessageBoxButtons.OK);
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
	#region StoreErrorCodeAdminNode
	//---------------------------------------------------------------------------
	//The StoreErrorCodeAdminNode administers the collection of store error codes
	public class StoreErrorCodeAdminNode: AdminNode {
		//Members
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Store Error Code";
		private const string MNU_EDIT = "Update Store Error Code";
		private const string MNU_REMOVE = "Delete Store Error Code";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public StoreErrorCodeAdminNode(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) {
			//Constructor

			try {
				//Set custom attributes and create a custom listview service menu
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
			ColumnHeader colCode = new ColumnHeader(); colCode.Text = "Code"; colCode.Width = 96;
			ColumnHeader colDesc = new ColumnHeader(); colDesc.Text = "Description"; colDesc.Width = 288;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "CodeID"; colID.Width = 0;
			headers = new ColumnHeader[] {colCode, colDesc, colID};
			return headers; 
		}
		public override ListViewItem[] list() { 
			//Set listview items (data)
			StoreErrorCodeDS lst = EnterpriseFactory.ViewStoreErrorCodes();
			ListViewItem[] items = new ListViewItem[lst.StoreErrorCodeTable.Rows.Count];
			for(int i=0; i<lst.StoreErrorCodeTable.Rows.Count; i++) {
				string[] subitems = {	lst.StoreErrorCodeTable[i].Code.Trim(), 
										lst.StoreErrorCodeTable[i].Description.ToString(), 
										lst.StoreErrorCodeTable[i].CodeID.ToString() };
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
			MenuItem menu;
			StoreErrorCodeDS dsStoreErrorCode;
			dlgStoreErrorCodeDetail dlgStoreErrorCode;
			int codeID = 0;
			string code = "";
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
						//Read new store error details (defaults), forward to dlgStoreErrorCodeDetail for update
						codeID = 0;
						dsStoreErrorCode = EnterpriseFactory.GetStoreErrorCode(codeID);
						dlgStoreErrorCode = new dlgStoreErrorCodeDetail(ref dsStoreErrorCode);
						res = dlgStoreErrorCode.ShowDialog();
						if(res==DialogResult.OK) {
							//Request a new store error code
							codeID = EnterpriseFactory.CreateStoreErrorCode(dsStoreErrorCode);
							code = dsStoreErrorCode.StoreErrorCodeTable[0].Code;
							if(codeID>0) {
								MessageBox.Show("Store error code " + code + " was created.", "Store Error Code Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("New store error code could not be created. Please try again.", "Store Error Code Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_EDIT:		
						//Read existing store error code details, forward to dlgStoreErrorCode for update
						codeID = Convert.ToInt32(base.mCurrentItem.SubItems[2].Text);
						dsStoreErrorCode = EnterpriseFactory.GetStoreErrorCode(codeID);
						dlgStoreErrorCode = new dlgStoreErrorCodeDetail(ref dsStoreErrorCode);
						res = dlgStoreErrorCode.ShowDialog();
						if(res==DialogResult.OK) {
							//Request an update to the store error code
							val = EnterpriseFactory.UpdateStoreErrorCode(dsStoreErrorCode);
							code = base.mCurrentItem.SubItems[0].Text;
							if(val) {
								MessageBox.Show("Store error code " + code + " was updated.", "Store Error Code Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Store error code " + code + " could not be updated. Please try again.", "Store Error Code Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_REMOVE:	
						//Prompt user for confirmation
						codeID = Convert.ToInt32(base.mCurrentItem.SubItems[2].Text);
						res = MessageBox.Show("Cancel store error code " + codeID + "?", "Store Error Code Admin", MessageBoxButtons.OKCancel);
						if(res==DialogResult.OK) {
							//Read existing details for row version and request cancel
							//dsStoreErrorCode = new StoreErrorCodeDS();
							//ret = EnterpriseFactory.GetStoreErrorCode(codeID, ref dsStoreErrorCode);
							//string rowVer = dsStoreErrorCode.StoreErrorCodeTable[0].RowVersion;
							val = EnterpriseFactory.DeleteStoreErrorCode(codeID);
							if(val) {
								MessageBox.Show("Store error code " + code + " was deleted.", "Store Error Code Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Store error Code " + codeID + " could not be deleted. Please try again.", "Store Error Code Admin", MessageBoxButtons.OK);
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
	#region WarehouseAdminNode
	//---------------------------------------------------------------------------
	//
	public class WarehouseAdminNode: AdminNode {
		//Members
		private int m_iVendorID;
		private bool m_bVendorIsActive=true;
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Warehouse";
		private const string MNU_EDIT = "Update Warehouse";
		private const string MNU_REMOVE = "Delete Warehouse";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public WarehouseAdminNode(string text, int imageIndex, int selectedImageIndex, int vendorID, bool vendorIsActive) : base(text, imageIndex, selectedImageIndex) {
			//Constructor
			try {
				//Set custom attributes and create a custom listview service menu
				this.m_iVendorID = vendorID;
				this.m_bVendorIsActive = vendorIsActive;
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
		
		//Listview support --------------------------------------------------
		public override ColumnHeader[] header() { 
			//Create listview column headers
			ColumnHeader[] headers = null;
			ColumnHeader colName = new ColumnHeader(); colName.Text = "Warehouse Name"; colName.Width = 192;
			ColumnHeader colNumber = new ColumnHeader(); colNumber.Text = "Number"; colNumber.Width = 60;
			ColumnHeader colMnemonic = new ColumnHeader(); colMnemonic.Text = "Mnemonic"; colMnemonic.Width = 80;
			ColumnHeader colPhone = new ColumnHeader(); colPhone.Text = "Phone #"; colPhone.Width = 102;
			ColumnHeader colExt = new ColumnHeader(); colExt.Text = "Ext"; colExt.Width = 36;
			ColumnHeader colAddress = new ColumnHeader(); colAddress.Text = "Address"; colAddress.Width = 288;
			ColumnHeader colContact = new ColumnHeader(); colContact.Text = "Contact"; colContact.Width = 120;
			ColumnHeader colFax = new ColumnHeader(); colFax.Text = "Fax #"; colFax.Width = 102;
			ColumnHeader colEmail = new ColumnHeader(); colEmail.Text = "eMail"; colEmail.Width = 96;
			ColumnHeader colIsActive = new ColumnHeader(); colIsActive.Text = "Active"; colIsActive.Width = 60;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "ID"; colID.Width = 0;
			headers = new ColumnHeader[] {colName, colNumber, colMnemonic, colPhone, colExt, colAddress, colContact, colFax, colEmail, colIsActive, colID};
			return headers;
		}
		public override ListViewItem[] list() { 
			//Set listview items (data)
			WarehouseDS lst = EnterpriseFactory.ViewWarehouses(this.m_iVendorID);
			ListViewItem[] items = new ListViewItem[lst.WarehouseViewTable.Rows.Count];
			for(int i=0; i<lst.WarehouseViewTable.Rows.Count; i++) {
				string[] subitems = {	lst.WarehouseViewTable[i].Description.Trim(), 
										lst.WarehouseViewTable[i].Number.Trim(),
										lst.WarehouseViewTable[i].Mnemonic.Trim(),
										lst.WarehouseViewTable[i].Phone.Trim(), 
										lst.WarehouseViewTable[i].Extension.Trim(), 
										lst.WarehouseViewTable[i].AddressLine1.Trim() + " " + lst.WarehouseViewTable[i].AddressLine2.Trim() + " " + lst.WarehouseViewTable[i].City.Trim() + ", " + lst.WarehouseViewTable[i].StateOrProvince.Trim() + " " + lst.WarehouseViewTable[i].PostalCode.Trim(), 
										lst.WarehouseViewTable[i].ContactName.Trim(), 
										lst.WarehouseViewTable[i].Fax.Trim(), 
										lst.WarehouseViewTable[i].EMail.Trim(), 
										lst.WarehouseViewTable[i].IsActive.ToString(), 
										lst.WarehouseViewTable[i].LocationID.ToString()};
				ListViewItem item = new ListViewItem(subitems, 2);
				items[i] = item;
			}
			return items;
		}
		
		protected override void OnItemMenuClick(object sender, System.EventArgs e) {
			//Context menu event handler
			MenuItem menu;
			WarehouseDS dsWarehouse;
			dlgWarehouseDetail dlgWarehouse;
			int iWarehouseID=0;
			string sWarehouse="";
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
						iWarehouseID = 0;
						dsWarehouse = EnterpriseFactory.GetWarehouse(this.m_iVendorID, iWarehouseID);
						dlgWarehouse = new dlgWarehouseDetail(this.m_bVendorIsActive, ref dsWarehouse);
						res = dlgWarehouse.ShowDialog();
						if(res==DialogResult.OK) {
							//Request a new location
							iWarehouseID = EnterpriseFactory.CreateWarehouse(dsWarehouse);
							if(iWarehouseID>0) {
								sWarehouse = dsWarehouse.WarehouseDetailTable[0].Description;
								MessageBox.Show("Warehouse " + sWarehouse + " was created.", "Warehouse Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("New warehouse could not be created. Please try again.", "Warehouse Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_EDIT:		
						//Read existing carrier details, forward to dlgCarrier for update
						iWarehouseID = Convert.ToInt32(base.mCurrentItem.SubItems[10].Text);
						dsWarehouse = EnterpriseFactory.GetWarehouse(this.m_iVendorID, iWarehouseID);
						dlgWarehouse = new dlgWarehouseDetail(this.m_bVendorIsActive, ref dsWarehouse);
						res = dlgWarehouse.ShowDialog();
						if(res==DialogResult.OK) {
							//Request an update to the location 
							val = EnterpriseFactory.UpdateWarehouse(dsWarehouse);
							sWarehouse = dsWarehouse.WarehouseDetailTable[0].Description;
							if(val) {
								MessageBox.Show("Warehouse " + sWarehouse + " was updated.", "Warehouse Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Warehouse " + sWarehouse + " could not be updated. Please try again.", "Warehouse Admin", MessageBoxButtons.OK);
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
	#region TerminalAdminNode
	//---------------------------------------------------------------------------
	//
	public class TerminalAdminNode: AdminNode {
		//Members
		private int m_iAgentID;
		private bool m_bAgentIsActive=true;
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Terminal";
		private const string MNU_EDIT = "Update Terminal";
		private const string MNU_REMOVE = "Delete Terminal";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public TerminalAdminNode(string text, int imageIndex, int selectedImageIndex, int agentID, bool agentIsActive) : base(text, imageIndex, selectedImageIndex) {
			//Constructor
			try {
				//Set custom attributes and create a custom listview service menu
				this.m_iAgentID = agentID;
				this.m_bAgentIsActive = agentIsActive;
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
		
		//Listview support --------------------------------------------------
		public override ColumnHeader[] header() { 
			//Create listview column headers
			ColumnHeader[] headers = null;
			ColumnHeader colName = new ColumnHeader(); colName.Text = "Terminal Name"; colName.Width = 192;
			ColumnHeader colNumber = new ColumnHeader(); colNumber.Text = "Number"; colNumber.Width = 60;
			ColumnHeader colMnemonic = new ColumnHeader(); colMnemonic.Text = "Mnemonic"; colMnemonic.Width = 80;
			ColumnHeader colPhone = new ColumnHeader(); colPhone.Text = "Phone #"; colPhone.Width = 102;
			ColumnHeader colExt = new ColumnHeader(); colExt.Text = "Ext"; colExt.Width = 36;
			ColumnHeader colAddress = new ColumnHeader(); colAddress.Text = "Address"; colAddress.Width = 288;
			ColumnHeader colContact = new ColumnHeader(); colContact.Text = "Contact"; colContact.Width = 120;
			ColumnHeader colFax = new ColumnHeader(); colFax.Text = "Fax #"; colFax.Width = 102;
			ColumnHeader colEmail = new ColumnHeader(); colEmail.Text = "eMail"; colEmail.Width = 96;
			ColumnHeader colIsActive = new ColumnHeader(); colIsActive.Text = "Active"; colIsActive.Width = 60;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "ID"; colID.Width = 0;
			headers = new ColumnHeader[] {colName, colNumber, colMnemonic, colPhone, colExt, colAddress, colContact, colFax, colEmail, colIsActive, colID};
			return headers;
		}
		public override ListViewItem[] list() { 
			//Set listview items (data)
			TerminalDS lst = EnterpriseFactory.ViewTerminals(this.m_iAgentID);
			ListViewItem[] items = new ListViewItem[lst.TerminalViewTable.Rows.Count];
			for(int i=0; i<lst.TerminalViewTable.Rows.Count; i++) {
				string[] subitems = {	lst.TerminalViewTable[i].Description.Trim(), 
										lst.TerminalViewTable[i].Number.Trim(),
										lst.TerminalViewTable[i].Mnemonic.Trim(),
										lst.TerminalViewTable[i].Phone.Trim(), 
										lst.TerminalViewTable[i].Extension.Trim(), 
										lst.TerminalViewTable[i].AddressLine1.Trim() + " " + lst.TerminalViewTable[i].AddressLine2.Trim() + " " + lst.TerminalViewTable[i].City.Trim() + ", " + lst.TerminalViewTable[i].StateOrProvince.Trim() + " " + lst.TerminalViewTable[i].PostalCode.Trim(), 
										lst.TerminalViewTable[i].ContactName.Trim(), 
										lst.TerminalViewTable[i].Fax.Trim(), 
										lst.TerminalViewTable[i].EMail.Trim(), 
										lst.TerminalViewTable[i].IsActive.ToString(), 
										lst.TerminalViewTable[i].LocationID.ToString()};
				ListViewItem item = new ListViewItem(subitems, 2);
				items[i] = item;
			}
			return items;
		}
		
		protected override void OnItemMenuClick(object sender, System.EventArgs e) {
			//Context menu event handler
			MenuItem menu;
			TerminalDS dsTerminal;
			dlgTerminalDetail dlgTerminal;
			int iTerminalID = 0;
			string sTerminal = "";
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
						iTerminalID = 0;
						dsTerminal = EnterpriseFactory.GetTerminal(this.m_iAgentID, iTerminalID);
						dlgTerminal = new dlgTerminalDetail(this.m_bAgentIsActive, ref dsTerminal);
						res = dlgTerminal.ShowDialog();
						if(res==DialogResult.OK) {
							//Request a new location
							iTerminalID = EnterpriseFactory.CreateTerminal(dsTerminal);
							if(iTerminalID>0) {
								sTerminal = dsTerminal.TerminalDetailTable[0].Description;
								MessageBox.Show("Terminal " + sTerminal + " was created.", "Terminal Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("New terminal could not be created. Please try again.", "Terminal Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_EDIT:		
						//Read existing carrier details, forward to dlgCarrier for update
						iTerminalID = Convert.ToInt32(base.mCurrentItem.SubItems[10].Text);
						dsTerminal = EnterpriseFactory.GetTerminal(this.m_iAgentID, iTerminalID);
						dlgTerminal = new dlgTerminalDetail(this.m_bAgentIsActive, ref dsTerminal);
						res = dlgTerminal.ShowDialog();
						if(res==DialogResult.OK) {
							//Request an update to the location 
							val = EnterpriseFactory.UpdateTerminal(dsTerminal);
							sTerminal = dsTerminal.TerminalDetailTable[0].Description;
							if(val) {
								MessageBox.Show("Terminal " + sTerminal + " was updated.", "Terminal Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Terminal " + sTerminal + " could not be updated. Please try again.", "Terminal Admin", MessageBoxButtons.OK);
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
	#region DamageCodeAdminNode
	//---------------------------------------------------------------------------
	//The DamageCodeAdminNode administers the collection of damage codes
	public class DamageCodeAdminNode: AdminNode {
		//Members
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Damage Code";
		private const string MNU_EDIT = "Update Damage Code";
		private const string MNU_REMOVE = "Delete Damage Code";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public DamageCodeAdminNode(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) {
			//Constructor

			try {
				//Set custom attributes and create a custom listview service menu
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
			ColumnHeader colCode = new ColumnHeader(); colCode.Text = "Code"; colCode.Width = 96;
			ColumnHeader colDesc = new ColumnHeader(); colDesc.Text = "Description"; colDesc.Width = 288;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "CodeID"; colID.Width = 0;
			headers = new ColumnHeader[] {colCode, colDesc, colID};
			return headers; 
		}
		public override ListViewItem[] list() { 
			//Set listview items (data)
			DamageCodeDS lst = FreightFactory.ViewDamageCodes();
			ListViewItem[] items = new ListViewItem[lst.DamageCodeTable.Rows.Count];
			for(int i=0; i<lst.DamageCodeTable.Rows.Count; i++) {
				string[] subitems = {	lst.DamageCodeTable[i].Code.ToString(), 
										lst.DamageCodeTable[i].Description.ToString(), 
										lst.DamageCodeTable[i].Code.ToString() };
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
			MenuItem menu;
			DamageCodeDS dsDamageCode=null;
			dlgDamageCodeDetail dlgDamageCode=null;
			short codeID=0;
			DialogResult res=DialogResult.Cancel;
			bool val=false;
			try {
				//Context menu item clicked-apply selected service; forward to menu handler
				menu = (MenuItem)sender;
				switch(menu.Text)  {
					case MNU_OPEN:		
						base.mnuLSVEdit.PerformClick();
						break;
					case MNU_ADD:		
						//Read new damage code details (defaults), forward to dlgDamageCodeDetail for update
						codeID = 0;
						dsDamageCode = FreightFactory.GetDamageCode(codeID);
						dlgDamageCode = new dlgDamageCodeDetail(ref dsDamageCode);
						res = dlgDamageCode.ShowDialog();
						if(res==DialogResult.OK) {
							//Request a new damage code
							codeID = FreightFactory.CreateDamageCode(dsDamageCode);
							if(codeID>0) {
								MessageBox.Show("Damage code " + codeID.ToString() + " was created.", "Damage Code Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("New damage code could not be created. Please try again.", "Damage Code Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_EDIT:		
						//Read existing damage code details, forward to dlgDamageCode for update
						codeID = Convert.ToInt16(base.mCurrentItem.SubItems[2].Text);
						dsDamageCode = FreightFactory.GetDamageCode(codeID);
						dlgDamageCode = new dlgDamageCodeDetail(ref dsDamageCode);
						res = dlgDamageCode.ShowDialog();
						if(res==DialogResult.OK) {
							//Request an update to the damage code
							val = FreightFactory.UpdateDamageCode(dsDamageCode);
							if(val) {
								MessageBox.Show("Damage code " + codeID.ToString() + " was updated.", "Damage Code Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Damage code " + codeID.ToString() + " could not be updated. Please try again.", "Damage Code Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_REMOVE:	
						//Prompt user for confirmation
						codeID = Convert.ToInt16(base.mCurrentItem.SubItems[2].Text);
						res = MessageBox.Show("Cancel damage code " + codeID.ToString() + "?", "Damage Code Admin", MessageBoxButtons.OKCancel);
						if(res==DialogResult.OK) {
							//Read existing details for row version and request cancel
							dsDamageCode = FreightFactory.GetDamageCode(codeID);
							string rowVer = dsDamageCode.DamageCodeTable[0].RowVersion;
							val = FreightFactory.DeleteDamageCode(codeID, rowVer);
							if(val) {
								MessageBox.Show("Damage code " + codeID.ToString() + " was deleted.", "Damage Code Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Damage Code " + codeID + " could not be deleted. Please try again.", "Damage Code Admin", MessageBoxButtons.OK);
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
	#region InboundLabelAdminNode
	//---------------------------------------------------------------------------
	//The InboundLabelAdminNode administers the collection of shipper's inbound label configurations for induction in TSort
	public class InboundLabelAdminNode: AdminNode {
		//Members  
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Inbound Label";
		private const string MNU_EDIT = "Update Inbound Label";
		private const string MNU_REMOVE = "Remove Inbound Label";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public InboundLabelAdminNode(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) {
			//Constructor

			try {
				//Set custom attributes and create a custom listview service menu
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
			ColumnHeader colLabelID = new ColumnHeader(); colLabelID.Text = "Label ID"; colLabelID.Width = 96;
			ColumnHeader colSortType = new ColumnHeader(); colSortType.Text = "Freight Sort Type"; colSortType.Width = 120;
			ColumnHeader colDesc = new ColumnHeader(); colDesc.Text = "Description"; colDesc.Width = 288;
			ColumnHeader colIsActive = new ColumnHeader(); colIsActive.Text = "Active"; colIsActive.Width = 60;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "LabelID"; colID.Width = 0;
			headers = new ColumnHeader[] {colLabelID, colSortType, colDesc, colIsActive, colID};
			return headers; 
		}
		public override ListViewItem[] list()  { 
			//Set listview items (data)
			InboundLabelDS lst = FreightFactory.ViewInboundLabels();
			ListViewItem[] items = new ListViewItem[lst.InboundLabelViewTable.Rows.Count];
			for(int i=0; i<lst.InboundLabelViewTable.Rows.Count; i++) 
			{
				string[] subitems = {	lst.InboundLabelViewTable[i].LabelID.ToString(), 
										lst.InboundLabelViewTable[i].SortTypeID.ToString(), 
										lst.InboundLabelViewTable[i].Description.ToString(),
										lst.InboundLabelViewTable[i].IsActive.ToString(), 
										lst.InboundLabelViewTable[i].LabelID.ToString()};
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
				base.mnuLSVRemove.Enabled = false;
				base.mnuLSVProps.Enabled = false;
			}
		}
		protected override void OnItemMenuClick(object sender, System.EventArgs e) {
			//Context menu event handler
			MenuItem menu;
			InboundLabelDS dsInboundLabelDetail;
			dlgInboundLabel dlgInboundLabelDetail;
			int labelID = 0;
			DialogResult res;
			bool val;
			
			try {
				//Context menu item clicked-apply selected service; forward to menu handler
				menu = (MenuItem)sender;
				switch(menu.Text)  
				{
					case MNU_OPEN:		
						base.mnuLSVEdit.PerformClick();
						break;
					case MNU_ADD:		
						//Read new damage code details (defaults), forward to dlgDamageCodeDetail for update
						labelID = 0;
						dsInboundLabelDetail = FreightFactory.GetInboundLabel(labelID);
						dlgInboundLabelDetail = new dlgInboundLabel(ref dsInboundLabelDetail);
						res = dlgInboundLabelDetail.ShowDialog();
						if(res==DialogResult.OK) {
							//Request a new damage code
							labelID = FreightFactory.CreateInboundLabel(dsInboundLabelDetail);
							if(labelID>0) {
								MessageBox.Show("Inbound label configuration " + labelID.ToString() + " was created.", "Vendor Inbound Label Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("New inbound label configuration could not be created at this time. Please try again.  Thank you.", "Vendor Inbound Label Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_EDIT:		
						//Read existing damage code details, forward to dlgDamageCode for update
						labelID = Convert.ToInt16(base.mCurrentItem.SubItems[4].Text);
						dsInboundLabelDetail = FreightFactory.GetInboundLabel(labelID);
						dlgInboundLabelDetail = new dlgInboundLabel(ref dsInboundLabelDetail);
						res = dlgInboundLabelDetail.ShowDialog();
						if(res==DialogResult.OK) {
							//Request an update to the damage code
							val = FreightFactory.UpdateInboundLabel(dsInboundLabelDetail);
							if(val) {
								MessageBox.Show("Inbound label configuration " + labelID.ToString() + " was updated.", "Vendor Inbound Label Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Inbound label configuration " + labelID.ToString() + " could not be updated. Please try again.  Thank you.", "Vendor Inbound Label Admin", MessageBoxButtons.OK);
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
	#region FreightPathAdminNode
	//---------------------------------------------------------------------------
	//The FreightPathAdminNode administers the collection of shipper's inbound label configurations for induction in TSort
	public class FreightPathAdminNode: AdminNode {
		//Members  
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Freight Path";
		private const string MNU_EDIT = "Update Freight Path";
		private const string MNU_REMOVE = "Remove Freight Path";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public FreightPathAdminNode(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) {
			//Constructor

			try {
				//Set custom attributes and create a custom listview service menu
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
			ColumnHeader colDesc = new ColumnHeader(); colDesc.Text = "Description"; colDesc.Width = 144;
			ColumnHeader colSortCenter = new ColumnHeader(); colSortCenter.Text = "Sort Center"; colSortCenter.Width = 144;
			ColumnHeader colPathType = new ColumnHeader(); colPathType.Text = "Path Type"; colPathType.Width = 72;
			ColumnHeader colFinalStop = new ColumnHeader(); colFinalStop.Text = "Final Stop"; colFinalStop.Width = 144;
			ColumnHeader colMnem1 = new ColumnHeader(); colMnem1.Text = "Mnem"; colMnem1.Width = 60;
			ColumnHeader colMnem2 = new ColumnHeader(); colMnem2.Text = "Mnem2"; colMnem2.Width = 60;
			ColumnHeader colIsActive = new ColumnHeader(); colIsActive.Text = "Active"; colIsActive.Width = 60;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "PathID"; colID.Width = 0;
			headers = new ColumnHeader[] {colDesc, colSortCenter, colPathType, colFinalStop, colMnem1, colMnem2, colIsActive, colID};
			return headers; 
		}
		public override ListViewItem[] list()  { 
			//Set listview items (data)
			FreightPathDS lst = EnterpriseFactory.ViewFreightPaths();
			ListViewItem[] items = new ListViewItem[lst.FreightPathViewTable.Rows.Count];
			for(int i=0; i<lst.FreightPathViewTable.Rows.Count; i++) {
				string[] subitems = {	lst.FreightPathViewTable[i].Description.Trim(), 
										lst.FreightPathViewTable[i].SortCenter.Trim(), 
										lst.FreightPathViewTable[i].PathType.Trim(),
										lst.FreightPathViewTable[i].FinalStop.Trim(),
										lst.FreightPathViewTable[i].Mnemonic, 
										lst.FreightPathViewTable[i].SecondaryMnemonic, 
										lst.FreightPathViewTable[i].IsActive.ToString(), 
										lst.FreightPathViewTable[i].PathID};
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
				base.mnuLSVRemove.Enabled = false;
				base.mnuLSVProps.Enabled = false;
			}
		}
		protected override void OnItemMenuClick(object sender, System.EventArgs e) {
			//Context menu event handler
			MenuItem menu;
			FreightPathDS dsFreightPath;
			dlgFreightPathDetail dlgFreightPath;
			string sPathID = "";
			DialogResult res;
			bool val;
			
			try {
				//Context menu item clicked-apply selected service; forward to menu handler
				menu = (MenuItem)sender;
				switch(menu.Text) {
					case MNU_OPEN:		
						base.mnuLSVEdit.PerformClick();
						break;
					case MNU_ADD:		
						//Read new damage code details (defaults), forward to dlgDamageCodeDetail for update
						sPathID = "";
						dsFreightPath = EnterpriseFactory.GetFreightPath(sPathID);
						dlgFreightPath = new dlgFreightPathDetail(ref dsFreightPath);
						res = dlgFreightPath.ShowDialog();
						if(res==DialogResult.OK) {
							//Request a new damage code
							sPathID = EnterpriseFactory.CreateFreightPath(dsFreightPath);
							if(sPathID!="") {
								MessageBox.Show("Freight path " + sPathID + " was created.", "Freight Path Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("New freight path could not be created at this time. Please try again.  Thank you.", "Freight Path Admin", MessageBoxButtons.OK);
						}
						break;
					case MNU_EDIT:		
						//Read existing damage code details, forward to dlgDamageCode for update
						sPathID = base.mCurrentItem.SubItems[7].Text;
						dsFreightPath = EnterpriseFactory.GetFreightPath(sPathID);
						dlgFreightPath = new dlgFreightPathDetail(ref dsFreightPath);
						res = dlgFreightPath.ShowDialog();
						if(res==DialogResult.OK) {
							//Request an update to the damage code
							val = EnterpriseFactory.UpdateFreightPath(dsFreightPath);
							if(val) {
								MessageBox.Show("Freight path " + sPathID + " was updated.", "Freight Path Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Freight path " + sPathID + " could not be updated. Please try again.  Thank you.", "Freight Path Admin", MessageBoxButtons.OK);
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
	#region OBFreightTypeAdminNode
	//---------------------------------------------------------------------------
	//The OBServiceTypeAdminNode administers a collection of Service Types for Outbound Freight Types
	public class OBFreightTypeAdminNode: AdminNode {
		//Members		
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Outbound Freight Type";
		private const string MNU_EDIT = "Update Outbound Freight Type";
		private const string MNU_REMOVE = "Delete Outbound Freight Type";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public OBFreightTypeAdminNode(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) {
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
				base.mnuLSVOpen.Enabled = false; base.mnuLSVAdd.Enabled = base.mnuLSVEdit.Enabled = base.mnuLSVRemove.Enabled = base.mnuLSVProps.Enabled = false;
			}
			catch(Exception ex) { throw(ex); }
		}
		
		//Treeview support --------------------------------------------------
		public override void loadChildNodes() { 
			//Load child nodes of base node (data)
			OutboundFreightTypeDS lst = EnterpriseFactory.ViewOutboundFreightTypes();
			base.mChildNodes = new TreeNode[lst.OutboundFreightTypeTable.Rows.Count];
			for(int i=0; i<lst.OutboundFreightTypeTable.Rows.Count; i++) {
				OBServiceTypeAdminNode node = new OBServiceTypeAdminNode(lst.OutboundFreightTypeTable[i].FreightType, lst.OutboundFreightTypeTable[i].Description.Trim(), lst.OutboundFreightTypeTable[i].AgentID, 0, 1);  
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
			ColumnHeader colOBFreightType = new ColumnHeader(); colOBFreightType.Text = "Outbound Freight Type"; colOBFreightType.Width = 144;
			ColumnHeader colAgent = new ColumnHeader(); colAgent.Text = "Agent Name"; colAgent.Width = 240;
			ColumnHeader colIBFreightType = new ColumnHeader(); colIBFreightType.Text = "Inbound Freight Type"; colIBFreightType.Width = 144;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "OBFTID"; colID.Width = 0;
			headers = new ColumnHeader[] {colOBFreightType, colAgent, colIBFreightType};
			return headers;
		}
		public override ListViewItem[] list() { 
			//Set listview items (data)
			OutboundFreightTypeDS lst = EnterpriseFactory.ViewOutboundFreightTypes();
			ListViewItem[] items = new ListViewItem[lst.OutboundFreightTypeTable.Rows.Count];
			for(int i=0; i<lst.OutboundFreightTypeTable.Rows.Count; i++) {
				string[] subitems = {	lst.OutboundFreightTypeTable[i].Description.Trim(), 
										lst.OutboundFreightTypeTable[i].AgentName.Trim(), 
										lst.OutboundFreightTypeTable[i].InboundFreightType.Trim(), 
										lst.OutboundFreightTypeTable[i].FreightType.Trim()};
				ListViewItem item = new ListViewItem(subitems, 0);
				items[i] = item;
			}
			return items;
		}
		
		public override void OnItemSelected(object sender, System.EventArgs e) { 
			//Event handler for listview display of this object
			ListView lsv;
			bool isItem = false, hasChildNodes = false;
			
			try {
				//Capture current child (lisview item) for this parent
				lsv = (ListView)sender;
				this.mCurrentItem = null;
				isItem = (lsv.SelectedItems.Count>0);
				if(lsv.SelectedItems.Count>0) 
					this.mCurrentItem = lsv.SelectedItems[0];
				isItem = (this.mCurrentItem!=null);
				hasChildNodes = (this.mChildNodes!=null);
			}
			catch(Exception ex) { base.OnErrorMessage(ex); }
			finally {
				//Set menu services
				this.mnuLSVOpen.Enabled = (isItem && hasChildNodes);
				this.mnuLSVAdd.Enabled = false;
				this.mnuLSVEdit.Enabled = false;
				this.mnuLSVRemove.Enabled = false;
				this.mnuLSVProps.Enabled = false;
			}
		}
		protected override void OnItemMenuClick(object sender, System.EventArgs e) {
			//Context menu event handler
			MenuItem menu;
	
			try {
				//Context menu item clicked-apply selected service; forward to menu handler
				menu = (MenuItem)sender;
				switch(menu.Text) {
					case MNU_OPEN:		
						//Notify client to open the selected child node
						base.OnOpenSelectedItem(EventArgs.Empty);
						break;
					case MNU_ADD:		
						break;
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
	#region OBServiceTypeAdminNode
	//---------------------------------------------------------------------------
	//The OBServiceTypeAdminNode administers a collection of Service Types for Outbound Freight Types
	public class OBServiceTypeAdminNode: AdminNode {
		//Members
		private string m_sOutFreightTypeID="";
		private int m_iAgentID = 0;
		
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Outbound Service Type";
		private const string MNU_EDIT = "Update Outbound Service Type";
		private const string MNU_REMOVE = "Delete Outbound Service Type";
		private const string MNU_PROPS = "Properties";
		//Events
		//Interface
		public OBServiceTypeAdminNode(string obFreightTypeID, string text, int agentID, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) {
			//Constructor
			try {
				//Create a custom listview service menu
				this.m_sOutFreightTypeID = obFreightTypeID;
				this.m_iAgentID = agentID;
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
		
		//Listview support --------------------------------------------------
		public override ColumnHeader[] header() { 
			//Create listview column headers
			ColumnHeader[] headers = null;
			ColumnHeader colCode = new ColumnHeader(); colCode.Text = "Argix Code"; colCode.Width = 96;
			ColumnHeader colDesc = new ColumnHeader(); colDesc.Text = "Outbound Service Type"; colDesc.Width = 192;
			ColumnHeader colMnemonic = new ColumnHeader(); colMnemonic.Text = "Mnemonic"; colMnemonic.Width = 72;
			ColumnHeader colIsPickup = new ColumnHeader(); colIsPickup.Text = "Is Pickup"; colIsPickup.Width = 72;
			ColumnHeader colIsActive = new ColumnHeader(); colIsActive.Text = "Active"; colIsActive.Width = 60;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "ServiceID"; colID.Width = 0;
			headers = new ColumnHeader[] {colCode, colDesc, colMnemonic, colIsPickup, colIsActive, colID};    
			return headers;
		}
		public override ListViewItem[] list() { 
			//Set listview items (data)
			OutboundServiceTypeDS lst = EnterpriseFactory.ViewOutboundServiceTypes(this.m_sOutFreightTypeID);
			ListViewItem[] items = new ListViewItem[lst.OutboundServiceTypeTable.Rows.Count];
			for(int i=0; i<lst.OutboundServiceTypeTable.Rows.Count; i++) {
				string[] subitems = {	lst.OutboundServiceTypeTable[i].ServiceID.ToString(),    
										lst.OutboundServiceTypeTable[i].Description.Trim(), 
										lst.OutboundServiceTypeTable[i].Mnemonic.Trim(), 
										lst.OutboundServiceTypeTable[i].IsPickup.ToString(), 
										lst.OutboundServiceTypeTable[i].IsActive.ToString(),
										lst.OutboundServiceTypeTable[i].ServiceID.ToString()};
				ListViewItem item = new ListViewItem(subitems, 2);
				items[i] = item;
			}
			return items;
		}
		
		protected override void OnItemMenuClick(object sender, System.EventArgs e) {
			OutboundServiceTypeDS dsServiceType;
			dlgServiceTypeDetail dlgServiceType;
			int iServiceTypeID = 0;
			string sServiceTypeName = "";
			DialogResult res;
			bool val;

			//Context menu event handler
			MenuItem menu;
			try {
				//Context menu item clicked-apply selected service; forward to menu handler
				menu = (MenuItem)sender;
				switch(menu.Text) {
					case MNU_OPEN:		break;
					case MNU_ADD:		
						//Read new damage code details (defaults), forward to dlg for update
						iServiceTypeID = 0;
						dsServiceType = EnterpriseFactory.GetOutboundServiceType(iServiceTypeID, this.m_sOutFreightTypeID);
						dlgServiceType = new dlgServiceTypeDetail(this.m_iAgentID, ref dsServiceType);
						res = dlgServiceType.ShowDialog();
						if(res==DialogResult.OK) {
							//Request a new damage code
							iServiceTypeID = EnterpriseFactory.CreateOutboundServiceType(dsServiceType);
							if(iServiceTypeID!=0) {
								MessageBox.Show("Service Type " + iServiceTypeID + " was created.", "Service Type Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("New Service Type could not be created at this time. Please try again.  Thank you.", "Freight Path Admin", MessageBoxButtons.OK);
						}
						break;
						
					case MNU_EDIT:		
						//Read existing carrier details, forward to dlg for update
						iServiceTypeID = Convert.ToInt32(base.mCurrentItem.SubItems[5].Text);
						dsServiceType = EnterpriseFactory.GetOutboundServiceType(iServiceTypeID, this.m_sOutFreightTypeID);
						dlgServiceType = new dlgServiceTypeDetail(this.m_iAgentID, ref dsServiceType);
						res = dlgServiceType.ShowDialog();
						if(res==DialogResult.OK) {
							//Request an update to the location 
							val = EnterpriseFactory.UpdateOutboundServiceType(dsServiceType);
							sServiceTypeName = dsServiceType.OutboundServiceTypeTable[0].Description;
							if(val) {
								MessageBox.Show("Service Type " + sServiceTypeName + " was updated.", "Service Type Admin", MessageBoxButtons.OK);
								base.OnListChanged(EventArgs.Empty);
							}
							else
								MessageBox.Show("Service Type " + sServiceTypeName + " could not be updated. Please try again.", "Service Type Admin", MessageBoxButtons.OK);
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
	#region MapAdminNode
	//---------------------------------------------------------------------------
	//The MapAdminNode administers the collection of maps
	public class MapAdminNode: AdminNode {
		//Members
		MenuItem mnuLSVCopy=null;
		MenuItem mnuLSVEditByCode=null;
		
		//Constants
		private const string MNU_OPEN = "Open";
		private const string MNU_ADD = "New Client Map";
		private const string MNU_COPY = "Copy Map";
		private const string MNU_EDIT = "Update Map";
		private const string MNU_EDITBYCODE = "Update Map By Postal Code";
		private const string MNU_REMOVE = "Delete Map";
		private const string MNU_PROPS = "View Map";
		//Events
		//Interface
		public MapAdminNode(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) {
			//Constructor

			try {
				//Set custom attributes and create a custom listview service menu
				base.mnuLSV = new ContextMenu();
				base.mnuLSVOpen = new MenuItem(MNU_OPEN, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep0 = new MenuItem("-");
				base.mnuLSVAdd = new MenuItem(MNU_ADD, new System.EventHandler(this.OnItemMenuClick));
				this.mnuLSVCopy = new MenuItem(MNU_COPY, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVEdit = new MenuItem(MNU_EDIT, new System.EventHandler(this.OnItemMenuClick));
				this.mnuLSVEditByCode = new MenuItem(MNU_EDITBYCODE, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVRemove = new MenuItem(MNU_REMOVE, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSVSep1 = new MenuItem("-");
				base.mnuLSVProps = new MenuItem(MNU_PROPS, new System.EventHandler(this.OnItemMenuClick));
				base.mnuLSV.MenuItems.AddRange(new MenuItem[] {base.mnuLSVOpen, base.mnuLSVSep0, base.mnuLSVAdd, this.mnuLSVCopy, base.mnuLSVEdit, this.mnuLSVEditByCode, base.mnuLSVRemove, base.mnuLSVSep1, base.mnuLSVProps});
				base.mnuLSVOpen.Enabled = false; base.mnuLSVAdd.Enabled = true; this.mnuLSVCopy.Enabled = base.mnuLSVEdit.Enabled = this.mnuLSVEditByCode.Enabled = base.mnuLSVRemove.Enabled = false; base.mnuLSVProps.Enabled = false;
			}
			catch(Exception ex) { throw(ex); }
		}
		
		//Listview support --------------------------------------------------
		public override ColumnHeader[] header() { 
			//Create listview column headers
			ColumnHeader[] headers = null;
			ColumnHeader colTerm = new ColumnHeader(); colTerm.Text = "Sort Center"; colTerm.Width = 144;
			ColumnHeader colClientNo = new ColumnHeader(); colClientNo.Text = "Client #"; colClientNo.Width = 72;
			ColumnHeader colClient = new ColumnHeader(); colClient.Text = "Client Name"; colClient.Width = 144;
			ColumnHeader colDesc = new ColumnHeader(); colDesc.Text = "Description"; colDesc.Width = 192;
			ColumnHeader colIsActive = new ColumnHeader(); colIsActive.Text = "Active"; colIsActive.Width = 60;
			ColumnHeader colTermID = new ColumnHeader(); colTermID.Text = "TermID"; colTermID.Width = 0;
			ColumnHeader colClientID = new ColumnHeader(); colClientID.Text = "ClientID"; colClientID.Width = 0;
			ColumnHeader colID = new ColumnHeader(); colID.Text = "MapID"; colID.Width = 0;
			headers = new ColumnHeader[] {colTerm, colClientNo, colClient, colDesc, colIsActive, colTermID, colClientID, colID};
			return headers;
		}
		public override ListViewItem[] list() { 
			//Set listview items (data)
			MapDS lst = EnterpriseFactory.ViewMaps();
			ListViewItem[] items = new ListViewItem[lst.MapViewTable.Rows.Count];
			for(int i=0; i<lst.MapViewTable.Rows.Count; i++) {
				string[] subitems = {	lst.MapViewTable[i].SortCenter, 
										lst.MapViewTable[i].Number, 
										lst.MapViewTable[i].ClientName, 
										lst.MapViewTable[i].Description, 
										lst.MapViewTable[i].IsActive.ToString(), 
										lst.MapViewTable[i].SortCenterID.ToString(), 
										lst.MapViewTable[i].ClientID.ToString(), 
										lst.MapViewTable[i].MapID.Trim() };
				ListViewItem item = new ListViewItem(subitems, 2);
				items[i] = item;
			}
			return items;
		}
		public override void OnItemSelected(object sender, System.EventArgs e) { 
			//Event handler for listview display of base object
			ListView lsv;
			bool isItem=false, hasChildNodes=false, isTerminalMap=false;
			
			try {
				//Capture current child (lisview item) for base parent
				lsv = (ListView)sender;
				base.mCurrentItem = null;
				isItem = (lsv.SelectedItems.Count>0);
				if(lsv.SelectedItems.Count>0) 
					base.mCurrentItem = lsv.SelectedItems[0];
				isItem = (base.mCurrentItem!=null);
				hasChildNodes = (base.mChildNodes!=null);
				isTerminalMap = isItem ? (base.mCurrentItem.SubItems[6].Text=="0") : false;
			}
			catch(Exception ex) { base.OnErrorMessage(ex); }
			finally {
				//Set menu services
				base.mnuLSVOpen.Enabled = (isItem && hasChildNodes);
				base.mnuLSVAdd.Enabled = (isItem && isTerminalMap);
				this.mnuLSVCopy.Enabled = isItem;
				base.mnuLSVEdit.Enabled = isItem;
				this.mnuLSVEditByCode.Enabled = !isItem;
				base.mnuLSVRemove.Enabled = false;
				base.mnuLSVProps.Enabled = isItem;
			}
		}
		protected override void OnItemMenuClick(object sender, System.EventArgs e) {
			//Context menu event handler
			MenuItem menu;
			dlgMapDetail dlgMap;
			dlgMapDetail2 dlgMap2;
			string sMapID="";
			int iSortCenterID=0, iClientID=0;
			MapTypeEnum eType=MapTypeEnum.MapTypeTerminal;
			DialogResult res;
			
			try {
				//Context menu item clicked-apply selected service; forward to menu handler
				menu = (MenuItem)sender;
				switch(menu.Text)  {
					case MNU_OPEN:		
						base.mnuLSVEdit.PerformClick();
						break;
					case MNU_ADD:		
						//Create a new client map; use terminal from selected terminal map item
						sMapID = "";
						iSortCenterID = Convert.ToInt32(base.mCurrentItem.SubItems[5].Text);
						eType = MapTypeEnum.MapTypeClient;
						dlgMap = new dlgMapDetail(sMapID, iSortCenterID, eType, MapActionEnum.MapActionCreate);
						res = dlgMap.ShowDialog();
						if(res==DialogResult.OK) base.OnListChanged(EventArgs.Empty);
						break;
					case MNU_COPY:		
						//Copy a terminal or client map
						sMapID = base.mCurrentItem.SubItems[7].Text;
						iSortCenterID = Convert.ToInt32(base.mCurrentItem.SubItems[5].Text);
						iClientID = Convert.ToInt32(base.mCurrentItem.SubItems[6].Text);
						eType = (iClientID>0) ? MapTypeEnum.MapTypeClient : MapTypeEnum.MapTypeTerminal;
						dlgMap = new dlgMapDetail(sMapID, iSortCenterID, eType, MapActionEnum.MapActionCopy);
						res = dlgMap.ShowDialog();
						if(res==DialogResult.OK) base.OnListChanged(EventArgs.Empty);
						break;
					case MNU_EDIT:		
						//Edit a terminal or client map
						sMapID = base.mCurrentItem.SubItems[7].Text;
						iSortCenterID = Convert.ToInt32(base.mCurrentItem.SubItems[5].Text);
						iClientID = Convert.ToInt32(base.mCurrentItem.SubItems[6].Text);
						eType = (iClientID>0) ? MapTypeEnum.MapTypeClient : MapTypeEnum.MapTypeTerminal;
						dlgMap = new dlgMapDetail(sMapID, iSortCenterID, eType, MapActionEnum.MapActionEdit);
						res = dlgMap.ShowDialog();
						if(res==DialogResult.OK) base.OnListChanged(EventArgs.Empty);
						break;
					case MNU_EDITBYCODE:
						//Edit mappings by postal code
						dlgMap2 = new dlgMapDetail2();
						res = dlgMap2.ShowDialog();
						break;
					case MNU_REMOVE:	break;
					case MNU_PROPS:		
						//View a terminal or client map (read-only)
						sMapID = base.mCurrentItem.SubItems[7].Text;
						iSortCenterID = Convert.ToInt32(base.mCurrentItem.SubItems[5].Text);
						iClientID = Convert.ToInt32(base.mCurrentItem.SubItems[6].Text);
						eType = (iClientID>0) ? MapTypeEnum.MapTypeClient : MapTypeEnum.MapTypeTerminal;
						dlgMap = new dlgMapDetail(sMapID, iSortCenterID, eType, MapActionEnum.MapActionView);
						res = dlgMap.ShowDialog();
						break;
				}			
			}
			catch(Exception ex) { base.OnErrorMessage(ex); }
		}
	}
	#endregion
}
