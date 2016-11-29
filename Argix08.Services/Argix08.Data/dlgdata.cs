//	File:	dlgdata.cs
//	Author:	J. Heary
//	Date:	01/02/08
//	Desc:	Dialog for application diagnostics.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Argix.Data {
	/// <summary>Dialog for application diagnostics.</summary>
	public class dlgData : System.Windows.Forms.Form {
		//Members
		private Assembly mAssembly=null;
		private Mediator mMediator=null, mSQLMediator=null, mWebMediator=null;
		private System.Windows.Forms.ToolTip mToolTip=null;
		
		private const string CMD_CLOSE = "&Close";
		private const string CMD_PRINT = "&Print";
        private const string TSORT_APP = "Tsort.App";
		private const string USP_ENUM="uspStoredProceduresGetList", TBL_ENUM = "SPTable";
        private const string USP_PREFIX = "USP_";
		#region Controls

        private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.TabControl tabDialog;
		private System.Windows.Forms.TabPage tabConn;
		private System.Windows.Forms.TabPage tabData;
		private System.Windows.Forms.CheckBox chkUseWebSvc;
		private System.Windows.Forms.Label lblConnection;
		private System.Windows.Forms.GroupBox grpConnection;
		private System.Windows.Forms.PictureBox picOnline;
		private System.Windows.Forms.PictureBox picOn;
		private System.Windows.Forms.PictureBox picOff;
		private System.Windows.Forms.Label lblDB;
		private System.Windows.Forms.ListView lsvDB;
        private Argix.Data.DataDS mDataDS;

		private System.ComponentModel.Container components = null;
		#endregion
        /// <summary> Constructor </summary>
        /// <param name="executingAssembly">Assembly reference to the executing assembly.</param>
        public dlgData(Assembly executingAssembly) : this(executingAssembly,global::Argix.Properties.Settings.Default.SQLConnection) { }
        /// <summary> Constructor </summary>
        /// <param name="executingAssembly">Assembly reference to the executing assembly.</param>
        /// <param name="sqlConnection">SQL Database connection string.</param>
        public dlgData(Assembly executingAssembly,string sqlConnection) : this(executingAssembly,sqlConnection,global::Argix.Properties.Settings.Default.DataAccessWS) { }
        /// <summary> Constructor </summary>
        /// <param name="executingAssembly">Assembly reference to the executing assembly.</param>
        /// <param name="sqlConnection">SQL Database connection string.</param>
        /// <param name="webServiceURL">Tsort.Data.WebSvc web service URL.</param>
		public dlgData(Assembly executingAssembly, string sqlConnection, string webServiceURL) {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				this.Text = "Argix Direct Data Diagnostics";
				this.cmdClose.Text = CMD_CLOSE;
				this.mToolTip = new System.Windows.Forms.ToolTip();
				
				//Set the list view column headers, icon source, and initial view for database
				this.lblDB.Text = "Required Stored Procedures";
				this.lsvDB.Columns.Add("Name", 192, HorizontalAlignment.Left);
				this.lsvDB.Columns.Add("Result", 384, HorizontalAlignment.Left);
				this.lsvDB.View = View.Details;

				//Hold reference objects
				this.mAssembly = executingAssembly;
                this.mSQLMediator = new SQLMediator(sqlConnection);
                this.mWebMediator = new WebSvcMediator(webServiceURL);
			}
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new dlgData instance.",ex); }
        }
		/// <summary></summary>
		/// <param name="disposing"></param>
		protected override void Dispose( bool disposing ) { if( disposing ) { if(components != null) components.Dispose(); } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgData));
            this.cmdClose = new System.Windows.Forms.Button();
            this.tabDialog = new System.Windows.Forms.TabControl();
            this.tabConn = new System.Windows.Forms.TabPage();
            this.grpConnection = new System.Windows.Forms.GroupBox();
            this.picOnline = new System.Windows.Forms.PictureBox();
            this.chkUseWebSvc = new System.Windows.Forms.CheckBox();
            this.lblConnection = new System.Windows.Forms.Label();
            this.tabData = new System.Windows.Forms.TabPage();
            this.lblDB = new System.Windows.Forms.Label();
            this.lsvDB = new System.Windows.Forms.ListView();
            this.picOn = new System.Windows.Forms.PictureBox();
            this.picOff = new System.Windows.Forms.PictureBox();
            this.mDataDS = new Argix.Data.DataDS();
            this.tabDialog.SuspendLayout();
            this.tabConn.SuspendLayout();
            this.grpConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOnline)).BeginInit();
            this.tabData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mDataDS)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdClose.Location = new System.Drawing.Point(374,323);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(96,24);
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // tabDialog
            // 
            this.tabDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabDialog.Controls.Add(this.tabConn);
            this.tabDialog.Controls.Add(this.tabData);
            this.tabDialog.Location = new System.Drawing.Point(6,6);
            this.tabDialog.Name = "tabDialog";
            this.tabDialog.Padding = new System.Drawing.Point(3,3);
            this.tabDialog.SelectedIndex = 0;
            this.tabDialog.Size = new System.Drawing.Size(462,312);
            this.tabDialog.TabIndex = 4;
            // 
            // tabConn
            // 
            this.tabConn.Controls.Add(this.grpConnection);
            this.tabConn.Location = new System.Drawing.Point(4,22);
            this.tabConn.Name = "tabConn";
            this.tabConn.Size = new System.Drawing.Size(454,286);
            this.tabConn.TabIndex = 0;
            this.tabConn.Text = "Connection";
            this.tabConn.UseVisualStyleBackColor = true;
            // 
            // grpConnection
            // 
            this.grpConnection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpConnection.Controls.Add(this.picOnline);
            this.grpConnection.Controls.Add(this.chkUseWebSvc);
            this.grpConnection.Controls.Add(this.lblConnection);
            this.grpConnection.Location = new System.Drawing.Point(6,6);
            this.grpConnection.Name = "grpConnection";
            this.grpConnection.Size = new System.Drawing.Size(444,148);
            this.grpConnection.TabIndex = 5;
            this.grpConnection.TabStop = false;
            this.grpConnection.Text = "Connection";
            // 
            // picOnline
            // 
            this.picOnline.Location = new System.Drawing.Point(411,15);
            this.picOnline.Name = "picOnline";
            this.picOnline.Size = new System.Drawing.Size(24,24);
            this.picOnline.TabIndex = 5;
            this.picOnline.TabStop = false;
            // 
            // chkUseWebSvc
            // 
            this.chkUseWebSvc.Location = new System.Drawing.Point(6,21);
            this.chkUseWebSvc.Name = "chkUseWebSvc";
            this.chkUseWebSvc.Size = new System.Drawing.Size(120,18);
            this.chkUseWebSvc.TabIndex = 0;
            this.chkUseWebSvc.Text = "Use Web Service";
            this.chkUseWebSvc.CheckedChanged += new System.EventHandler(this.OnConnectionChanged);
            // 
            // lblConnection
            // 
            this.lblConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConnection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblConnection.Location = new System.Drawing.Point(6,42);
            this.lblConnection.Name = "lblConnection";
            this.lblConnection.Size = new System.Drawing.Size(432,100);
            this.lblConnection.TabIndex = 4;
            // 
            // tabData
            // 
            this.tabData.Controls.Add(this.lblDB);
            this.tabData.Controls.Add(this.lsvDB);
            this.tabData.Location = new System.Drawing.Point(4,22);
            this.tabData.Name = "tabData";
            this.tabData.Size = new System.Drawing.Size(454,286);
            this.tabData.TabIndex = 1;
            this.tabData.Text = "Database";
            this.tabData.UseVisualStyleBackColor = true;
            // 
            // lblDB
            // 
            this.lblDB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDB.Location = new System.Drawing.Point(6,12);
            this.lblDB.Name = "lblDB";
            this.lblDB.Size = new System.Drawing.Size(444,18);
            this.lblDB.TabIndex = 5;
            // 
            // lsvDB
            // 
            this.lsvDB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvDB.Location = new System.Drawing.Point(3,33);
            this.lsvDB.Name = "lsvDB";
            this.lsvDB.Size = new System.Drawing.Size(447,249);
            this.lsvDB.TabIndex = 4;
            this.lsvDB.UseCompatibleStateImageBehavior = false;
            this.lsvDB.View = System.Windows.Forms.View.Details;
            this.lsvDB.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.OnDBColumnClicked);
            // 
            // picOn
            // 
            this.picOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picOn.Image = ((System.Drawing.Image)(resources.GetObject("picOn.Image")));
            this.picOn.Location = new System.Drawing.Point(15,321);
            this.picOn.Name = "picOn";
            this.picOn.Size = new System.Drawing.Size(16,16);
            this.picOn.TabIndex = 6;
            this.picOn.TabStop = false;
            this.picOn.Visible = false;
            // 
            // picOff
            // 
            this.picOff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picOff.Image = ((System.Drawing.Image)(resources.GetObject("picOff.Image")));
            this.picOff.Location = new System.Drawing.Point(33,321);
            this.picOff.Name = "picOff";
            this.picOff.Size = new System.Drawing.Size(16,16);
            this.picOff.TabIndex = 7;
            this.picOff.TabStop = false;
            this.picOff.Visible = false;
            // 
            // mDataDS
            // 
            this.mDataDS.DataSetName = "DataDS";
            this.mDataDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mDataDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dlgData
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.ClientSize = new System.Drawing.Size(474,352);
            this.Controls.Add(this.picOff);
            this.Controls.Add(this.picOn);
            this.Controls.Add(this.tabDialog);
            this.Controls.Add(this.cmdClose);
            this.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgData";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Diagnostics";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.tabDialog.ResumeLayout(false);
            this.tabConn.ResumeLayout(false);
            this.grpConnection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picOnline)).EndInit();
            this.tabData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mDataDS)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Form load event handler
			Cursor.Current = Cursors.WaitCursor;
			try {
				//Show early
				this.Visible = true;
				Application.DoEvents();
				#region Tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;
				#endregion
				
				//Get configuration and database requirements
				Type type = this.mAssembly.GetType(TSORT_APP, true, true);
				FieldInfo[] infos = type.GetFields();
				foreach(FieldInfo info in infos) {
                    if(info.Name.IndexOf(USP_PREFIX,0,USP_PREFIX.Length) == 0) 
						this.mDataDS.StoredProcedureTable.AddStoredProcedureTableRow(info.GetValue(null).ToString());
				}
				
				//Create data and UI services
				this.chkUseWebSvc.Checked = false;
				OnConnectionChanged(null, null);
			} 
			catch(Exception ex) { throw ex; }
			finally { Cursor.Current = Cursors.Default; }
		}
		private void OnConnectionChanged(object sender, System.EventArgs e) {
			//Event handler for change in connection method
            if(this.mMediator != null) this.mMediator.DataStatusUpdate -= new DataStatusHandler(OnDataStatusUpdate);
            this.mMediator = this.chkUseWebSvc.Checked ? (Mediator)this.mWebMediator : (Mediator)this.mSQLMediator;
			this.mMediator.DataStatusUpdate += new DataStatusHandler(OnDataStatusUpdate);
			this.lsvDB.Items.Clear();
			Application.DoEvents();
			viewDatabase();
		}
		internal void OnDataStatusUpdate(object sender, DataStatusArgs e) {
			//Event handler for notifications from mediator
			this.picOnline.Image = (e.Online) ? this.picOn.Image : this.picOff.Image;
			this.lblConnection.Text = e.Connection;
			this.mToolTip.SetToolTip(this.lblConnection, e.Connection);
		}
        private void OnDBColumnClicked(object sender,System.Windows.Forms.ColumnClickEventArgs e) {
            //Event handler for change in column selection
            this.lsvDB.Sorting = (this.lsvDB.Sorting == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;
            this.lsvDB.Sort();
        }
        private void OnCmdClick(object sender,System.EventArgs e) {
            //Command button handler
            try {
                Button btn = (Button)sender;
                switch(btn.Text) {
                    case CMD_CLOSE:
                        //Close the dialog
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                        break;
                }
            }
            catch(Exception ex) { throw ex; }
        }
		#region Local Services: viewDatabase()
		private void viewDatabase() {
			//
			DataSet ds = new DataSet();
			Cursor.Current = Cursors.WaitCursor;
			try {		    
				//Build the listview list items - (index, key, text, icon, smallIcon)
				if(this.mDataDS.StoredProcedureTable.Rows.Count > 0) {
					//Obtain stored procedures from database
					string sError="";
					try {
						ds = this.mMediator.FillDataset(USP_ENUM, TBL_ENUM, null);
					}
					catch(Exception ex) { sError = ex.Message; }
					for(int i=0; i<this.mDataDS.StoredProcedureTable.Rows.Count; i++) {
						//Add required stored procedure entry
						string sSP = this.mDataDS.StoredProcedureTable[i].Name;
						ListViewItem oItem = this.lsvDB.Items.Add(sSP);
						
						//Check for stored procs
						if(ds.Tables.Count > 0) {
							if(ds.Tables[TBL_ENUM].Rows.Count > 0) {
								try {
									string s = ds.Tables[TBL_ENUM].Select("name='" + sSP + "'")[0].ToString();
									oItem.SubItems.Add("OK");
								}
								catch(Exception) { oItem.SubItems.Add("Procedure not found"); }
							}
						}
						else
							oItem.SubItems.Add(sError);
					}
					this.lsvDB.Sorting = SortOrder.Ascending;
					this.lsvDB.Sort();
					this.lsvDB.Refresh();
				}
			}
			catch(Exception) { }
			finally { Cursor.Current = Cursors.Default; }
		}
		#endregion
	}
}
