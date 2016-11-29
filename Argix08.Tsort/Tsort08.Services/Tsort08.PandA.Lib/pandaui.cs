//	File:	pandaui.cs
//	Author:	J. Heary
//	Date:	06/05/07
//	Desc:	User interface control for the PandA Library.
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
using Tsort.Freight;
using Tsort.Sort;

namespace Tsort.PandA {
	/// <summary>User interface control for the PandA Library.</summary>
	public class PandaUI : System.Windows.Forms.UserControl {
		//Members
		private PandaService mPandaSvc=null;
		private LogLevel mLogLevel=LogLevel.None;
		private ArgixTextBoxListener mTraceListener=null;
		
		//Constants
		private const string CMD_REFRESH = "Refresh";
		private const string CMD_REFRESHCACHE = "Refresh Cache";
		#region Controls

		private System.Windows.Forms.TabControl tabMain;
		private System.Windows.Forms.TabPage tabCtns;
		private System.Windows.Forms.TabPage tabMsgs;
		private System.Windows.Forms.TabPage tabTrace;
		private System.Windows.Forms.RichTextBox txtTrace;
		private System.Windows.Forms.ContextMenu ctxMain;
		private System.Windows.Forms.MenuItem ctxSep1;
		private System.Windows.Forms.MenuItem ctxClear;
		private System.Windows.Forms.MenuItem ctxSave;
		private System.Windows.Forms.ListView lsvCtns;
		private System.Windows.Forms.ListView lsvMsgs;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.ColumnHeader columnHeader13;
		private System.Windows.Forms.ColumnHeader columnHeader14;
		private System.Windows.Forms.ColumnHeader columnHeader15;
		private System.Windows.Forms.ColumnHeader columnHeader16;
		private System.Windows.Forms.ColumnHeader columnHeader17;
		private System.Windows.Forms.ColumnHeader columnHeader18;
		private System.Windows.Forms.ColumnHeader columnHeader19;
		private System.Windows.Forms.ColumnHeader columnHeader20;
		private System.Windows.Forms.ColumnHeader columnHeader21;
		private System.Windows.Forms.ColumnHeader columnHeader22;
		private System.Windows.Forms.ColumnHeader columnHeader23;
		private System.Windows.Forms.ColumnHeader columnHeader24;
		private System.Windows.Forms.ColumnHeader columnHeader25;
		private System.Windows.Forms.ColumnHeader columnHeader26;
		private System.Windows.Forms.MenuItem ctxRun;
		private System.Windows.Forms.MenuItem ctxSep2;
		private System.Windows.Forms.MenuItem ctxOpen;
		private System.ComponentModel.IContainer components=null;
		#endregion
		
		//Events
		//Interface
		/// <summary>Creates a new instance of the Tsort.PandA.PandaUI control.</summary>
		public PandaUI() {
			//Constructor
			try {
				//Initialize members
				InitializeComponent();
				this.tabMain.TabPages.Remove(tabMsgs);
				this.tabMain.TabPages.Remove(tabCtns);
				this.tabMain.TabPages.Remove(tabTrace);
			} 
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new PandaUI instance.", ex); }
		}
		/// <summary></summary>
		/// <param name="disposing"></param>
		protected override void Dispose( bool disposing ) { if( disposing ) { if(components != null) { components.Dispose(); } } base.Dispose( disposing ); }
		/// <summary>Gets and sets the PandA service that this interface interacts with.</summary>
		public PandaService PandaSvc { 
			get { return this.mPandaSvc; } 
			set { 
				this.mPandaSvc = value;
				if(this.mPandaSvc != null) {
					this.mPandaSvc.PandaRequestComplete += new PandaPacketEventHandler(OnPandaRequestComplete);
					this.mPandaSvc.LabelDataRequest += new EventHandler(OnLabelDataRequest);
					this.mPandaSvc.LabelDataRequestComplete += new PandaLabelDataEventHandler(OnLabelDataRequestComplete);
					this.mPandaSvc.VerifyLabelRequestComplete += new PandaVerifyLabelEventHandler(OnVerifyLabelRequestComplete);
				}
			} 
		}
		/// <summary>Show\hide the packet messages tab.</summary>
		/// <remarks>
		/// The packet messages tab shows message packets when clients communicate using Tsort.PandA.PandaPacket messages.
		/// </remarks>
		public bool MessagesOn { 
			get { return this.tabMsgs.Parent != null; } 
			set { 
				if(value) 
					this.tabMain.TabPages.Add(this.tabMsgs);
				else
					this.tabMain.TabPages.Remove(this.tabMsgs);
				if(this.tabMain.TabPages.Count > 0) this.tabMain.SelectedIndex = 0;
			} 
		}
		/// <summary>Show\hide the cartons tab.</summary>
		/// <remarks>
		/// The cartons tab shows carton label data and verify label request results.
		/// </remarks>
		public bool CartonsOn { 
			get { return this.tabCtns.Parent != null; } 
			set { 
				if(value) 
					this.tabMain.TabPages.Add(this.tabCtns);
				else
					this.tabMain.TabPages.Remove(this.tabCtns);
				if(this.tabMain.TabPages.Count > 0) this.tabMain.SelectedIndex = 0;
			} 
		}
		/// <summary>Turn Trace on\off.</summary>
		/// <remarks>
		/// To turn on:  set to LogLevel.Debug or greater; this will make the Trace tab visible.
		/// To turn-off: set to LogLevel.None; this will hide the Trace tab.
		/// </remarks>
		public LogLevel TraceOn { 
			get { return this.mLogLevel; } 
			set { 
				this.mLogLevel = value;
				if(this.mLogLevel > LogLevel.None) {
					if(this.mTraceListener != null) {
						ArgixTrace.RemoveListener(this.mTraceListener);
						this.mTraceListener.Close();
						this.mTraceListener.Dispose();
					}
					else {
						this.tabMain.TabPages.Add(this.tabTrace);
					}
					this.mTraceListener = new ArgixTextBoxListener(this.mLogLevel, this.txtTrace);
					ArgixTrace.AddListener(this.mTraceListener);
				}
				else {
					if(this.mTraceListener != null) {
						ArgixTrace.RemoveListener(this.mTraceListener);
						this.mTraceListener.Close();
						this.mTraceListener.Dispose();
						this.tabMain.TabPages.Remove(this.tabTrace);
					}
				}
				if(this.tabMain.TabPages.Count > 0) this.tabMain.SelectedIndex = 0;
			}
		}
		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabMain = new System.Windows.Forms.TabControl();
			this.tabCtns = new System.Windows.Forms.TabPage();
			this.lsvCtns = new System.Windows.Forms.ListView();
			this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader16 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader17 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader18 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader19 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader20 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader21 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader22 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader23 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader24 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader25 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader26 = new System.Windows.Forms.ColumnHeader();
			this.ctxMain = new System.Windows.Forms.ContextMenu();
			this.ctxOpen = new System.Windows.Forms.MenuItem();
			this.ctxClear = new System.Windows.Forms.MenuItem();
			this.ctxSep1 = new System.Windows.Forms.MenuItem();
			this.ctxSave = new System.Windows.Forms.MenuItem();
			this.ctxSep2 = new System.Windows.Forms.MenuItem();
			this.ctxRun = new System.Windows.Forms.MenuItem();
			this.tabMsgs = new System.Windows.Forms.TabPage();
			this.lsvMsgs = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
			this.tabTrace = new System.Windows.Forms.TabPage();
			this.txtTrace = new System.Windows.Forms.RichTextBox();
			this.tabMain.SuspendLayout();
			this.tabCtns.SuspendLayout();
			this.tabMsgs.SuspendLayout();
			this.tabTrace.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabMain
			// 
			this.tabMain.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.tabMain.Controls.Add(this.tabCtns);
			this.tabMain.Controls.Add(this.tabMsgs);
			this.tabMain.Controls.Add(this.tabTrace);
			this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tabMain.Location = new System.Drawing.Point(0, 0);
			this.tabMain.Name = "tabMain";
			this.tabMain.SelectedIndex = 0;
			this.tabMain.Size = new System.Drawing.Size(576, 288);
			this.tabMain.TabIndex = 16;
			// 
			// tabCtns
			// 
			this.tabCtns.Controls.Add(this.lsvCtns);
			this.tabCtns.Location = new System.Drawing.Point(4, 4);
			this.tabCtns.Name = "tabCtns";
			this.tabCtns.Size = new System.Drawing.Size(568, 262);
			this.tabCtns.TabIndex = 2;
			this.tabCtns.Text = "Cartons";
			// 
			// lsvCtns
			// 
			this.lsvCtns.AutoArrange = false;
			this.lsvCtns.CausesValidation = false;
			this.lsvCtns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					  this.columnHeader14,
																					  this.columnHeader15,
																					  this.columnHeader16,
																					  this.columnHeader17,
																					  this.columnHeader18,
																					  this.columnHeader19,
																					  this.columnHeader20,
																					  this.columnHeader21,
																					  this.columnHeader22,
																					  this.columnHeader23,
																					  this.columnHeader24,
																					  this.columnHeader25,
																					  this.columnHeader26});
			this.lsvCtns.ContextMenu = this.ctxMain;
			this.lsvCtns.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lsvCtns.FullRowSelect = true;
			this.lsvCtns.GridLines = true;
			this.lsvCtns.HideSelection = false;
			this.lsvCtns.LabelWrap = false;
			this.lsvCtns.Location = new System.Drawing.Point(0, 0);
			this.lsvCtns.MultiSelect = false;
			this.lsvCtns.Name = "lsvCtns";
			this.lsvCtns.Size = new System.Drawing.Size(568, 262);
			this.lsvCtns.TabIndex = 0;
			this.lsvCtns.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader14
			// 
			this.columnHeader14.Text = "CartonID";
			this.columnHeader14.Width = 180;
			// 
			// columnHeader15
			// 
			this.columnHeader15.Text = "Status";
			this.columnHeader15.Width = 48;
			// 
			// columnHeader16
			// 
			this.columnHeader16.Text = "Label (zpl)";
			this.columnHeader16.Width = 96;
			// 
			// columnHeader17
			// 
			this.columnHeader17.Text = "Label Exc";
			this.columnHeader17.Width = 96;
			// 
			// columnHeader18
			// 
			this.columnHeader18.Text = "Verify";
			this.columnHeader18.Width = 48;
			// 
			// columnHeader19
			// 
			this.columnHeader19.Text = "Verify Exc";
			this.columnHeader19.Width = 96;
			// 
			// columnHeader20
			// 
			this.columnHeader20.Text = "Barcode1";
			this.columnHeader20.Width = 72;
			// 
			// columnHeader21
			// 
			this.columnHeader21.Text = "Barcode2";
			this.columnHeader21.Width = 72;
			// 
			// columnHeader22
			// 
			this.columnHeader22.Text = "Barcode3";
			this.columnHeader22.Width = 72;
			// 
			// columnHeader23
			// 
			this.columnHeader23.Text = "Barcode4";
			this.columnHeader23.Width = 72;
			// 
			// columnHeader24
			// 
			this.columnHeader24.Text = "Barcode5";
			this.columnHeader24.Width = 72;
			// 
			// columnHeader25
			// 
			this.columnHeader25.Text = "Barcode6";
			this.columnHeader25.Width = 72;
			// 
			// columnHeader26
			// 
			this.columnHeader26.Text = "Weight";
			// 
			// ctxMain
			// 
			this.ctxMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.ctxOpen,
																					this.ctxClear,
																					this.ctxSep1,
																					this.ctxSave,
																					this.ctxSep2,
																					this.ctxRun});
			this.ctxMain.Popup += new System.EventHandler(this.OnMenuPopup);
			// 
			// ctxOpen
			// 
			this.ctxOpen.Index = 0;
			this.ctxOpen.Text = "Open";
			this.ctxOpen.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxClear
			// 
			this.ctxClear.Index = 1;
			this.ctxClear.Text = "Clear";
			this.ctxClear.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxSep1
			// 
			this.ctxSep1.Index = 2;
			this.ctxSep1.Text = "-";
			// 
			// ctxSave
			// 
			this.ctxSave.Index = 3;
			this.ctxSave.Text = "Save";
			this.ctxSave.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxSep2
			// 
			this.ctxSep2.Index = 4;
			this.ctxSep2.Text = "-";
			// 
			// ctxRun
			// 
			this.ctxRun.Index = 5;
			this.ctxRun.Text = "Run";
			this.ctxRun.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// tabMsgs
			// 
			this.tabMsgs.Controls.Add(this.lsvMsgs);
			this.tabMsgs.Location = new System.Drawing.Point(4, 4);
			this.tabMsgs.Name = "tabMsgs";
			this.tabMsgs.Size = new System.Drawing.Size(568, 262);
			this.tabMsgs.TabIndex = 1;
			this.tabMsgs.Text = "Messages";
			this.tabMsgs.Visible = false;
			// 
			// lsvMsgs
			// 
			this.lsvMsgs.AutoArrange = false;
			this.lsvMsgs.CausesValidation = false;
			this.lsvMsgs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					  this.columnHeader1,
																					  this.columnHeader2,
																					  this.columnHeader3,
																					  this.columnHeader4,
																					  this.columnHeader5,
																					  this.columnHeader6,
																					  this.columnHeader8,
																					  this.columnHeader9,
																					  this.columnHeader10,
																					  this.columnHeader11,
																					  this.columnHeader12,
																					  this.columnHeader13});
			this.lsvMsgs.ContextMenu = this.ctxMain;
			this.lsvMsgs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lsvMsgs.FullRowSelect = true;
			this.lsvMsgs.GridLines = true;
			this.lsvMsgs.HideSelection = false;
			this.lsvMsgs.LabelWrap = false;
			this.lsvMsgs.Location = new System.Drawing.Point(0, 0);
			this.lsvMsgs.MultiSelect = false;
			this.lsvMsgs.Name = "lsvMsgs";
			this.lsvMsgs.Size = new System.Drawing.Size(568, 262);
			this.lsvMsgs.TabIndex = 1;
			this.lsvMsgs.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Client";
			this.columnHeader1.Width = 3;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Received";
			this.columnHeader2.Width = 120;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Time";
			this.columnHeader3.Width = 72;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Length";
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Code";
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Number";
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "OrgCode";
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "Flags";
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "Iterator";
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "Body";
			this.columnHeader11.Width = 768;
			// 
			// columnHeader12
			// 
			this.columnHeader12.Text = "Valid";
			// 
			// columnHeader13
			// 
			this.columnHeader13.Text = "Exception";
			// 
			// tabTrace
			// 
			this.tabTrace.Controls.Add(this.txtTrace);
			this.tabTrace.Location = new System.Drawing.Point(4, 4);
			this.tabTrace.Name = "tabTrace";
			this.tabTrace.Size = new System.Drawing.Size(568, 262);
			this.tabTrace.TabIndex = 0;
			this.tabTrace.Text = "Trace";
			this.tabTrace.Visible = false;
			// 
			// txtTrace
			// 
			this.txtTrace.AcceptsTab = true;
			this.txtTrace.ContextMenu = this.ctxMain;
			this.txtTrace.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtTrace.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtTrace.Location = new System.Drawing.Point(0, 0);
			this.txtTrace.MaxLength = 2147483647;
			this.txtTrace.Name = "txtTrace";
			this.txtTrace.ReadOnly = true;
			this.txtTrace.Size = new System.Drawing.Size(568, 262);
			this.txtTrace.TabIndex = 0;
			this.txtTrace.Text = "";
			this.txtTrace.WordWrap = false;
			// 
			// PandaUI
			// 
			this.Controls.Add(this.tabMain);
			this.Name = "PandaUI";
			this.Size = new System.Drawing.Size(576, 288);
			this.Resize += new System.EventHandler(this.OnControlResize);
			this.Load += new System.EventHandler(this.OnControlLoad);
			this.tabMain.ResumeLayout(false);
			this.tabCtns.ResumeLayout(false);
			this.tabMsgs.ResumeLayout(false);
			this.tabTrace.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		private void OnControlLoad(object sender, System.EventArgs e) {
			//Event handler for control load event
			try { 
				this.tabMain.SelectedIndex = 0;
				this.txtTrace.MaxLength = 1024000;
			} 
			catch(Exception ex) { reportError(ex); } 
			finally { setServices(); }
		}
		private void OnControlResize(object sender, System.EventArgs e) {
			//Event handler for change in control size
		}
		#region Packet/Carton Events: OnPandaRequestComplete(), OnLabelDataRequest(), OnLabelDataRequestComplete(), OnVerifyLabelRequestComplete()
		private void OnPandaRequestComplete(object sender, PandaPacketEventArgs e) {
			//Event handler for PandA packet request complete event
			try { 
				//Add the packet to the messsage view
				if(this.tabMsgs.Parent != null) {
					//Manage item size
					if(this.lsvMsgs.Items.Count > 100) this.lsvMsgs.Items.RemoveAt(0);
					
					//Add new item
					this.lsvMsgs.Items.Insert(this.lsvMsgs.Items.Count, new ListViewItem(new string[]{e.Packet.ClientID,e.Packet.Received.ToString("MM-dd-yyyy HH:mm:ss.fff"),e.Packet.ProcessingTime.ToString(),e.Packet.MessageLength.ToString(),e.Packet.MessageCode.ToString(),e.Packet.MessageNumber.ToString(),e.Packet.OriginalMessageCode.ToString(),e.Packet.MessageFlags.ToString(),e.Packet.RecordIterator.ToString(),e.Packet.MessageBody,e.Packet.Valid.ToString(),(e.Packet.Exception!=null?e.Packet.Exception.Message:"")}));
					this.lsvMsgs.Items[this.lsvMsgs.Items.Count-1].Selected = true;
					this.lsvMsgs.Items[this.lsvMsgs.Items.Count-1].EnsureVisible();
				}
			} 
			catch(Exception ex) { reportError(ex); }
			finally { manageTrace(); }
		}
		private void OnLabelDataRequest(object sender, EventArgs e) {
			//Event handler for new label data request event
			manageTrace();
		}
		private void OnLabelDataRequestComplete(object sender, PandaLabelDataEventArgs e) {
			//Event handler for label data request complete event
			try {
				//Add the carton to the carton view
				if(this.tabCtns.Parent != null) {
					//Manage item size
					if(this.lsvCtns.Items.Count > 100) this.lsvCtns.Items.RemoveAt(0);
										
					//Add new item
					switch(this.mLogLevel) {
						case LogLevel.None:
						case LogLevel.Debug:
						case LogLevel.Information: 
							//Log all cartons
							this.lsvCtns.Items.Insert(this.lsvCtns.Items.Count, new ListViewItem(new string[]{e.Carton.CartonID,e.Carton.StatusCode.ToString(),e.Carton.LabelFormat,e.Carton.LabelException,e.Carton.Verify,e.Carton.SaveException,e.Carton.Barcode1,e.Carton.Barcode2,e.Carton.Barcode3,e.Carton.Barcode4,e.Carton.Barcode5,e.Carton.Barcode6,e.Carton.ItemWeight.ToString()}));
							this.lsvCtns.Items[this.lsvCtns.Items.Count-1].Selected = true;
							this.lsvCtns.Items[this.lsvCtns.Items.Count-1].EnsureVisible();
							break;
						case LogLevel.Warning:
						case LogLevel.Error: 
							//Log error carton only
							if(e.Carton.StatusCode != PandaService.STATUS_CARTON_OK) {
								this.lsvCtns.Items.Insert(this.lsvCtns.Items.Count, new ListViewItem(new string[]{e.Carton.CartonID,e.Carton.StatusCode.ToString(),e.Carton.LabelFormat,e.Carton.LabelException,e.Carton.Verify,e.Carton.SaveException,e.Carton.Barcode1,e.Carton.Barcode2,e.Carton.Barcode3,e.Carton.Barcode4,e.Carton.Barcode5,e.Carton.Barcode6,e.Carton.ItemWeight.ToString()}));
								this.lsvCtns.Items[this.lsvCtns.Items.Count-1].Selected = true;
								this.lsvCtns.Items[this.lsvCtns.Items.Count-1].EnsureVisible();
							}
							break;
					}
				}
			}
			catch(Exception ex) { reportError(ex); }
			finally { manageTrace(); }
		}
		private void OnVerifyLabelRequestComplete(object sender, PandaVerifyLabelEventArgs e) {
			//Event handler for verify label data request complete event
			try {
				//Update carton detail
				if(e.CartonID.Length > 0) {
					//Find the row
					for(int i=0; i<this.lsvCtns.Items.Count; i++) {
						if(this.lsvCtns.Items[i].SubItems[0].Text == e.CartonID) {
							//Update
							this.lsvCtns.Items[i].SubItems[4].Text = e.VerifyFlag;
							this.lsvCtns.Items[i].SubItems[5].Text = (e.Exception!=null?e.Exception.Message:"");
							break;
						}
					}
				}
			}
			catch(Exception ex) { reportError(ex); }
			finally { manageTrace(); }
		}
		#endregion
		#region User services: OnMenuClick(), OnMenuPopup()
		private void OnMenuClick(object sender, System.EventArgs e) {
			//Event hanlder for menu click events
			try {
				MenuItem mnu = (MenuItem)sender;
				switch(mnu.Text) {
					case "Open":	
						switch(this.tabMain.SelectedTab.Name) {
							case "tabCtns":		break;
							case "tabMsgs":		
								OpenFileDialog dlgOpen = new OpenFileDialog();
								dlgOpen.Filter = "Text Files (*.xml) | *.xml";
								dlgOpen.FilterIndex = 0;
								if(dlgOpen.ShowDialog(this)==DialogResult.OK) {
									PandaDS packets = new PandaDS();
									packets.ReadXml(dlgOpen.FileName, XmlReadMode.ReadSchema);
									if(packets.PacketTable.Rows.Count == 0)
										MessageBox.Show(this, "No PandA packets found in file " + dlgOpen.FileName + ".", "Open PandA Packets", MessageBoxButtons.OK, MessageBoxIcon.Information);
									else {
										for(int i=0; i<packets.PacketTable.Rows.Count; i++) {
											PandaDS.PacketTableRow packet = packets.PacketTable[i];
											this.lsvMsgs.Items.Add(new ListViewItem(new string[]{packet.ClientID,packet.Received.ToString("MM-dd-yyyy HH:mm:ss.fff"),packet.ProcessingTime.ToString(),packet.MessageLength.ToString(),packet.MessageCode.ToString(),packet.MessageNumber.ToString(),packet.OriginalMessageCode.ToString(),packet.MessageFlags.ToString(),packet.RecordIterator.ToString(),packet.MessageBody,packet.Valid.ToString(),packet.Exception}));
										}
									}
								}
								break;
							case "tabTrace":	break;
						}
						break;
					case "Clear":	
						switch(this.tabMain.SelectedTab.Name) {
							case "tabCtns":		this.lsvCtns.Items.Clear(); break;
							case "tabMsgs":		this.lsvMsgs.Items.Clear(); break;
							case "tabTrace":	this.txtTrace.Clear(); break;
						}
						break;
					case "Save":	
						#region Save a view to file
						SaveFileDialog dlgSave = new SaveFileDialog();
						dlgSave.AddExtension = true;
						dlgSave.FilterIndex = 0;
						dlgSave.OverwritePrompt = true;
						switch(this.tabMain.SelectedTab.Name) {
							case "tabCtns":		
								dlgSave.Filter = "Text Files (*.xml) | *.xml";
								dlgSave.Title = "Save Cartons As...";
								dlgSave.FileName = "cartons_" + DateTime.Today.ToString("yyyyMMdd") + ".xml";
								if(dlgSave.ShowDialog(this)==DialogResult.OK) {
									PandaDS cartons = new PandaDS();
									#region Populate dataset with all cartons that are in lsvCtns
									for(int i=0; i<this.lsvCtns.Items.Count; i++) {
										PandaDS.CartonTableRow carton = cartons.CartonTable.NewCartonTableRow();
										carton.CartonID = this.lsvCtns.Items[i].SubItems[0].Text;
										carton.StatusCode = int.Parse(this.lsvCtns.Items[i].SubItems[1].Text);
										carton.LabelFormat = this.lsvCtns.Items[i].SubItems[2].Text;
										carton.LabelException = this.lsvCtns.Items[i].SubItems[3].Text;
										carton.Verify = this.lsvCtns.Items[i].SubItems[4].Text;
										carton.SaveException = this.lsvCtns.Items[i].SubItems[5].Text;
										carton.Barcode1 = this.lsvCtns.Items[i].SubItems[6].Text;
										carton.Barcode2 = this.lsvCtns.Items[i].SubItems[7].Text;
										carton.Barcode3 = this.lsvCtns.Items[i].SubItems[8].Text;
										carton.Barcode4 = this.lsvCtns.Items[i].SubItems[9].Text;
										carton.Barcode5 = this.lsvCtns.Items[i].SubItems[10].Text;
										carton.Barcode6 = this.lsvCtns.Items[i].SubItems[11].Text;
										carton.ItemWeight = decimal.Parse(this.lsvCtns.Items[i].SubItems[12].Text);
										cartons.CartonTable.AddCartonTableRow(carton);
									}
									#endregion
									cartons.WriteXml(dlgSave.FileName, XmlWriteMode.WriteSchema); 
								}
								break;
							case "tabMsgs":		
								dlgSave.Filter = "Text Files (*.xml) | *.xml";
								dlgSave.Title = "Save Packets As...";
								dlgSave.FileName = "packets_" + DateTime.Today.ToString("yyyyMMdd") + ".xml";
								if(dlgSave.ShowDialog(this)==DialogResult.OK) {
									PandaDS packets = new PandaDS();
									#region Populate dataset with all packets that are in lsvMsgs
									for(int i=0; i<this.lsvMsgs.Items.Count; i++) {
										PandaDS.PacketTableRow packet = packets.PacketTable.NewPacketTableRow();
										packet.ClientID = this.lsvMsgs.Items[i].SubItems[0].Text;
										packet.Received = DateTime.Parse(this.lsvMsgs.Items[i].SubItems[1].Text);
										packet.ProcessingTime = float.Parse(this.lsvMsgs.Items[i].SubItems[2].Text);
										packet.MessageLength = int.Parse(this.lsvMsgs.Items[i].SubItems[3].Text);
										packet.MessageCode = int.Parse(this.lsvMsgs.Items[i].SubItems[4].Text);
										packet.MessageNumber = int.Parse(this.lsvMsgs.Items[i].SubItems[5].Text);
										packet.OriginalMessageCode = int.Parse(this.lsvMsgs.Items[i].SubItems[6].Text);
										packet.MessageFlags = int.Parse(this.lsvMsgs.Items[i].SubItems[7].Text);
										packet.RecordIterator = int.Parse(this.lsvMsgs.Items[i].SubItems[8].Text);
										packet.MessageBody = this.lsvMsgs.Items[i].SubItems[9].Text;
										packet.Valid = bool.Parse(this.lsvMsgs.Items[i].SubItems[10].Text);
										packet.Exception = this.lsvMsgs.Items[i].SubItems[11].Text;
										packets.PacketTable.AddPacketTableRow(packet);
									}
									#endregion
									packets.WriteXml(dlgSave.FileName, XmlWriteMode.WriteSchema); 
								}
								break;
							case "tabTrace":	
								dlgSave.Filter = "Text Files (*.txt) | *.txt";
								dlgSave.Title = "Save Trace Log As...";
								dlgSave.FileName = "tracelog_" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
								if(dlgSave.ShowDialog(this)==DialogResult.OK) {
									this.txtTrace.SaveFile(dlgSave.FileName, RichTextBoxStreamType.PlainText); 
								}
								break;
						}
						#endregion
						break;
					case "Run":	
						switch(this.tabMain.SelectedTab.Name) {
							case "tabCtns":		break;
							case "tabMsgs":		
								#region Populate datarow with all fields that are in lsvMsgs selected item
								PandaDS packets = new PandaDS();
								PandaDS.PacketTableRow packet = packets.PacketTable.NewPacketTableRow();
								packet.ClientID = this.lsvMsgs.SelectedItems[0].SubItems[0].Text;
								packet.Received = DateTime.Parse(this.lsvMsgs.SelectedItems[0].SubItems[1].Text);
								packet.ProcessingTime = float.Parse(this.lsvMsgs.SelectedItems[0].SubItems[2].Text);
								packet.MessageLength = int.Parse(this.lsvMsgs.SelectedItems[0].SubItems[3].Text);
								packet.MessageCode = int.Parse(this.lsvMsgs.SelectedItems[0].SubItems[4].Text);
								packet.MessageNumber = int.Parse(this.lsvMsgs.SelectedItems[0].SubItems[5].Text);
								packet.OriginalMessageCode = int.Parse(this.lsvMsgs.SelectedItems[0].SubItems[6].Text);
								packet.MessageFlags = int.Parse(this.lsvMsgs.SelectedItems[0].SubItems[7].Text);
								packet.RecordIterator = int.Parse(this.lsvMsgs.SelectedItems[0].SubItems[8].Text);
								packet.MessageBody = this.lsvMsgs.SelectedItems[0].SubItems[9].Text;
								packet.Valid = bool.Parse(this.lsvMsgs.SelectedItems[0].SubItems[10].Text);
								packet.Exception = this.lsvMsgs.SelectedItems[0].SubItems[11].Text;
								#endregion
								PandaPacket request = new PandaPacket(packet);
								this.mPandaSvc.ProcessPandARequest("", request);
								break;
							case "tabTrace":	break;
						}
						break;
				}
			}
			catch (Exception ex) { reportError(ex); }
		}
		private void OnMenuPopup(object sender, System.EventArgs e) {
			//Event handler for menu popup event
			setServices();
		}
		#endregion
		#region Local services: setServices(), reportError(), manageTrace()
		private void setServices() {
			//Set user services states
			try { 
				this.ctxOpen.Enabled = false;
				this.ctxRun.Enabled = false;
				this.ctxSave.Enabled = true;
				this.ctxClear.Enabled = true;
				switch(this.tabMain.SelectedTab.Name) {
					case "tabCtns":		
						this.ctxSave.Enabled = this.ctxClear.Enabled = this.lsvCtns.Items.Count > 0;
						break;
					case "tabMsgs":		
						this.ctxOpen.Enabled = true;
						this.ctxRun.Enabled = this.lsvMsgs.SelectedItems.Count == 1;
						this.ctxSave.Enabled = this.ctxClear.Enabled = this.lsvMsgs.Items.Count > 0;
						break;
					case "tabTrace":	
						this.ctxSave.Enabled = this.ctxClear.Enabled = this.txtTrace.Text.Length > 0;
						break;
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void reportError(Exception ex) {
			//Report an exception to the user
			ArgixTrace.WriteLine(new TraceMessage(ex.ToString(), AppLib.PRODUCTNAME, LogLevel.Error, "PandaSvc"));
		}
		private void manageTrace() {
			//Manage trace textbox
			try { 
				if(this.txtTrace.Text.Length > this.txtTrace.MaxLength) this.txtTrace.Clear();
			} 
			catch(Exception ex) { reportError(ex); }
		}
		#endregion
	}
}
