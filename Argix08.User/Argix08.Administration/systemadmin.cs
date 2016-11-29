//	File:	systemadmin.cs
//	Author:	J. Heary
//	Date:	04/27/06
//	Desc:	Class and interface declarations and definitions for admin node 
//			classes.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using Tsort.Data;

namespace Tsort {
	//Class and interface definitions
	public interface INode {
		//Treeview support
		void loadChildNodes();
		void unloadChildNodes();
		void expandNode();
		void collapseNode();
	}
	public interface IItem {
		//Listview support
		ColumnHeader[] header();
		ListViewItem[] list();
		ContextMenu menu();
		void OnItemSelected(object sender, System.EventArgs e);
		void OnItemDoubleClicked(object sender, System.EventArgs e);
		event EventHandler OpenSelectedItem;
		event EventHandler ListChanged;
	}
	
	#region AdminNode: Base class for all specialized Admin Node sub-classes
	public class AdminNode: TreeNode, INode, IItem {
		//Data members
		protected TreeNode[] mChildNodes=null;
		protected ListViewItem mCurrentItem=null;
		protected ContextMenu mnuLSV=null;
		protected MenuItem mnuLSVOpen, mnuLSVSep0, mnuLSVAdd, mnuLSVEdit, mnuLSVRemove, mnuLSVSep1, mnuLSVProps;
		
		//Constants
		private const string _MNU_OPEN = "Open";
		private const string _MNU_ADD = "New";
		private const string _MNU_EDIT = "Update";
		private const string _MNU_REMOVE = "Delete";
		private const string _MNU_PROPS = "Properties";
		
		//Events
		public event EventHandler ListChanged=null;
		public event EventHandler OpenSelectedItem=null;
		public event ErrorEventHandler ErrorMessage=null;

		//Operations
		public AdminNode(string text, int imageIndex, int selectedImageIndex) {
			//Constructor
			try {
				//Set members and base node members
				this.Text = text.Trim();
				this.ImageIndex = imageIndex;
				this.SelectedImageIndex = selectedImageIndex;
				#region Create a listview service menu and set initial states
				this.mnuLSV = new ContextMenu();
				this.mnuLSVOpen = new MenuItem(_MNU_OPEN, new System.EventHandler(this.OnItemMenuClick));
				this.mnuLSVSep0 = new MenuItem("-");
				this.mnuLSVAdd = new MenuItem(_MNU_ADD, new System.EventHandler(this.OnItemMenuClick));
				this.mnuLSVEdit = new MenuItem(_MNU_EDIT, new System.EventHandler(this.OnItemMenuClick));
				this.mnuLSVRemove = new MenuItem(_MNU_REMOVE, new System.EventHandler(this.OnItemMenuClick));
				this.mnuLSVSep1 = new MenuItem("-");
				this.mnuLSVProps = new MenuItem(_MNU_PROPS, new System.EventHandler(this.OnItemMenuClick));
				this.mnuLSV.MenuItems.AddRange(new MenuItem[] {this.mnuLSVOpen, this.mnuLSVSep0, this.mnuLSVAdd, this.mnuLSVEdit, this.mnuLSVRemove, this.mnuLSVSep1, this.mnuLSVProps});
				#endregion
				this.mnuLSVOpen.Enabled = this.mnuLSVAdd.Enabled = this.mnuLSVEdit.Enabled = this.mnuLSVRemove.Enabled = this.mnuLSVProps.Enabled = false;
			}
			catch(Exception ex) { throw ex; }
		}
		
		#region Treeview support: loadChildNodes(), unloadChildNodes(), expandNode(), collapseNode()
		//Override these methods to add support for child nodes to the treeview
		public virtual void loadChildNodes() { return; }
		public virtual void unloadChildNodes() { base.Nodes.Clear(); this.mChildNodes = null; }
		public void expandNode() { 
			//Load [visible] child nodes for each child
			if(this.mChildNodes!=null) {
				foreach(INode node in this.mChildNodes) 
					node.loadChildNodes();
			}
		}
		public void collapseNode() { 
			//Unload [hidden] child nodes for each child
			if(this.mChildNodes!=null) {
				foreach(INode node in this.mChildNodes) 
					node.unloadChildNodes();
			}
		}
		#endregion
		#region Listview Support: header(), list(), OnItemSelected(), OnItemDoubleClicked()
		//Required to support detail list of node\non-node children
		public virtual ColumnHeader[] header() { 
			//Return listview column headers for this object
			ColumnHeader[] headers = null;
			ColumnHeader colName = new ColumnHeader(); colName.Text = "Name"; colName.Width = 144;
			ColumnHeader colDesc = new ColumnHeader(); colDesc.Text = "Description"; colDesc.Width = 288;
			ColumnHeader colActive = new ColumnHeader(); colActive.Text = "Active"; colActive.Width = 72;
			headers = new ColumnHeader[] {colName, colDesc, colActive};
			return headers; 
		}
		public virtual ListViewItem[] list() { return null; }
		public virtual void OnItemSelected(object sender, System.EventArgs e) { 
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
			catch(Exception ex) { if(this.ErrorMessage != null) this.ErrorMessage(this, new ErrorEventArgs(ex)); }
			finally {
				//Set menu services
				this.mnuLSVOpen.Enabled = (isItem && hasChildNodes);
				this.mnuLSVAdd.Enabled = !isItem;
				this.mnuLSVEdit.Enabled = isItem;
				this.mnuLSVRemove.Enabled = false;
				this.mnuLSVProps.Enabled = false;
			}
		}
		public virtual void OnItemDoubleClicked(object sender, System.EventArgs e) { 
			//Event handler for double click of this item
			try {
				this.mnuLSVOpen.PerformClick();
			}
			catch(Exception ex) { if(this.ErrorMessage != null) this.ErrorMessage(this, new ErrorEventArgs(ex)); }
		}
		#endregion
		#region User Services: menu(), OnItemMenuClick()
		public virtual ContextMenu menu() { return this.mnuLSV; }
		protected virtual void OnItemMenuClick(object sender, System.EventArgs e) {
			//Context menu event handler
			try {
				//Context menu item clicked-apply selected service
				MenuItem menu = (MenuItem)sender;
				switch(menu.Text)  {
					case _MNU_OPEN:		
						//Notify client to open the selected child node
						OnOpenSelectedItem(EventArgs.Empty);
						break;
					case _MNU_ADD:		break;
					case _MNU_EDIT:		break;
					case _MNU_REMOVE:	break;
					case _MNU_PROPS:	break;
					default:			break;
				}
			}
			catch(Exception ex) { if(this.ErrorMessage != null) this.ErrorMessage(this, new ErrorEventArgs(ex)); }
		}
		#endregion
		protected virtual void OnListChanged(System.EventArgs e) {
			//Fire ListChanged event
			if(this.ListChanged!=null) this.ListChanged(this, EventArgs.Empty);
		}
		protected virtual void OnOpenSelectedItem(System.EventArgs e) {
			//Fire OpenSelectedItem event
			if(this.OpenSelectedItem != null && this.mCurrentItem != null) {
				for(int i=0; i<this.mChildNodes.Length; i++) {
					if(this.mChildNodes[i].Text == this.mCurrentItem.Text) { this.OpenSelectedItem(this.mChildNodes[i], e); break; }
				}
			}
		}
		protected virtual void OnErrorMessage(Exception ex) {
			//Fire ErrorMessage event
			if(this.ErrorMessage!=null) this.ErrorMessage(this, new ErrorEventArgs(ex));
		}
	}
	#endregion
	#region TerminalMgtRootNode: Root-node of the TerminalMgt tree
	//The TerminalMgtRootNode is the root-node of the TerminalMgt tree and administers a 
	//collection of Terminal Mgt entities including Drivers, Switchers, Trailers, Vehicles, 
	//Yards, Yard Sections, Yard Locations, and Terminals
	public class TerminalMgtRootNode: AdminNode {
		//Members
		//Interface
		public TerminalMgtRootNode(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) { }
		#region Treeview Support Overrides: loadChildNodes()
		public override void loadChildNodes() {
			//Load child nodes of this node (data)
			try {
				base.mChildNodes = new TreeNode[] {	new DriverAdminNode("Drivers", 0, 1), 
													new EntTerminalAdminNode("Terminals", 0, 1),
													new TrailerAdminNode("Trailers", 0, 1), 
													new VehicleAdminNode("Vehicles", 0, 1) };
				base.Nodes.Clear();
				base.Nodes.AddRange(base.mChildNodes);
				
				//Each child must load its children if this node is expanded
				if(base.IsExpanded) {
					foreach(INode node in base.mChildNodes) {
						TreeNode tn = (TreeNode)node;
						node.loadChildNodes();
					}
				}
			} 
			catch(Exception ex) { base.OnErrorMessage(ex); }
		}
		#endregion
		#region Listview Support Overrides: header(), list()
		public override ColumnHeader[] header() { 
			//Return listview column headers for this object
			ColumnHeader[] headers = null;
			ColumnHeader colName = new ColumnHeader(); colName.Text = "Name"; colName.Width = 144;
			ColumnHeader colDesc = new ColumnHeader(); colDesc.Text = "Description"; colDesc.Width = 288;
			headers = new ColumnHeader[] {colName, colDesc};
			return headers; 
		}
		public override ListViewItem[] list() { 
			//Return listview items (data) for this object
			ListViewItem[] items = null;
			string[] subitems = {"Drivers", "Driver Management"};
			ListViewItem item1 = new ListViewItem(subitems, 0);
			subitems = new string[] {"Terminals", "Terminal Management"};
			ListViewItem item2 = new ListViewItem(subitems, 0);
			subitems = new string[] {"Trailers", "Trailer Management"};
			ListViewItem item3 = new ListViewItem(subitems, 0);
			subitems = new string[] {"Vehicles", "Driver Vehicle Management"};
			ListViewItem item4 = new ListViewItem(subitems, 0);
			items = new ListViewItem[] {item1, item2, item3, item4};
			return items;
		}
		#endregion
		public override void OnItemSelected(object sender, System.EventArgs e) { 
			//Override menu service states
			base.OnItemSelected(sender, e);
			base.mnuLSVAdd.Enabled = base.mnuLSVEdit.Enabled = base.mnuLSVRemove.Enabled = false;
		}
	}
	#endregion
	#region BusinessAdminRootNode: Root-node of the Business Admin tree
	//The BusinessAdminRootNode is the root-node of the Business Admin tree and administers 
	//a collection of Business Admin entities including ...
	public class BusinessAdminRootNode: AdminNode {
		//Members
		//Interface
		public BusinessAdminRootNode(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) { }
		#region Treeview Support Overrides: loadChildNodes()
		public override void loadChildNodes() {
			//Load child nodes of this node (data)
			try {
				base.mChildNodes = new TreeNode[] {	new AgentAdminNode("Agents", 0, 1),
													new CarrierAdminNode("Carriers", 0, 1),
													new ClientAdminNode("Clients", 0, 1), 
													new DamageCodeAdminNode("Damage Codes", 0, 1), 
													new FreightPathAdminNode("Freight Paths", 0, 1), 
													new InboundLabelAdminNode("Inbound Labels", 0, 1),
													new MapAdminNode("Maps", 0, 1),
													new PaymentServiceAdminNode("Payment Services", 0, 1), 
													new VendorAdminNode("Vendors", 0, 1),
													new OBFreightTypeAdminNode("Outbound Freight Types", 0, 1)};
				base.Nodes.Clear();
				base.Nodes.AddRange(base.mChildNodes);
				
				//Each child must load its children if this node is expanded
				if(this.IsExpanded) {
					foreach(INode node in this.mChildNodes) {
						TreeNode tn = (TreeNode)node;
						node.loadChildNodes();
					}
				}
			} 
			catch(Exception ex) { base.OnErrorMessage(ex); }
		}
		#endregion
		#region Listview Support Overrides: header(), list()
		public override ColumnHeader[] header() { 
			//Return listview column headers for this object
			ColumnHeader[] headers = null;
			ColumnHeader colName = new ColumnHeader(); colName.Text = "Name"; colName.Width = 144;
			ColumnHeader colDesc = new ColumnHeader(); colDesc.Text = "Description"; colDesc.Width = 288;
			headers = new ColumnHeader[] {colName, colDesc};
			return headers; 
		}
		public override ListViewItem[] list() { 
			//Return listview items (data) for this object
			ListViewItem[] items = null;
			string[] subitems = {"Agents", "Agent Management"};
			ListViewItem item1 = new ListViewItem(subitems, 0);
			subitems = new string[] {"Carriers", "Carrier Management"};
			ListViewItem item2 = new ListViewItem(subitems, 0);
			subitems = new string[] {"Clients", "Client Management"};
			ListViewItem item3 = new ListViewItem(subitems, 0);
			subitems = new string[] {"Damage Codes", "Damage Codes Management"};
			ListViewItem item4 = new ListViewItem(subitems, 0);
			subitems = new string[] {"Freight Paths", "Freight Path Management"};
			ListViewItem item5 = new ListViewItem(subitems, 0);
			subitems = new string[]{"Inbound Labels", "Create and Edit Shipper's Label Profiles for TSort Stations"};
			ListViewItem item6 = new ListViewItem(subitems, 0);
			subitems = new string[]{"Maps", "Map Managment"};
			ListViewItem item7 = new ListViewItem(subitems, 0);
			subitems = new string[] {"Payment Services", "Payment Service Companies Management"};
			ListViewItem item8 = new ListViewItem(subitems, 0);
			subitems = new string[] {"Vendors", "Vendor Management"};
			ListViewItem item9 = new ListViewItem(subitems, 0);
			subitems = new string[] {"Outbound Freight Types", "View Outbound Freight Types in order to manage their Inbound Service Types"};
			ListViewItem item10 = new ListViewItem(subitems, 0);
			items = new ListViewItem[] {item1, item2, item3, item4, item5, item6, item7, item8, item9, item10};
			return items;
		}
		#endregion
		public override void OnItemSelected(object sender, System.EventArgs e) { 
			//Override menu service states
			base.OnItemSelected(sender, e);
			base.mnuLSVAdd.Enabled = base.mnuLSVEdit.Enabled = base.mnuLSVRemove.Enabled = false;
		}
	}
	#endregion
}
