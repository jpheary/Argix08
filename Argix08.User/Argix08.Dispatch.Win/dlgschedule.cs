//	File:	dlgschedule.cs
//	Author:	J. Heary
//	Date:	10/05/05
//	Desc:	Dialog to view/edit entries on a Dispatch schedule.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Argix.Data;

namespace Argix.Dispatch {
	//
	public class dlgSchedule : System.Windows.Forms.Form {
		//Members
		protected ScheduleEntry mEntry=null;
		protected System.Windows.Forms.ToolTip mToolTip=null;
		protected bool mIsDirty=false;
		private Mediator mMediator=null;
		
		//Events
		public event EventHandler ServiceStatesChanged=null;
		public event StatusEventHandler StatusMessage=null;
		
		//Constants
		private const string CMD_CANCEL = "&Cancel";
		private const string CMD_OK = "O&K";
		
		#region Controls

		protected System.Windows.Forms.Button btnOK;
		protected System.Windows.Forms.Button btnCancel;
		
		private System.ComponentModel.Container components = null;		//Required designer variable
		#endregion
		
		//Interface
		public dlgSchedule(): this(null, null) { }
		public dlgSchedule(Mediator mediator): this(null, mediator) { }
		public dlgSchedule(ScheduleEntry entry, Mediator mediator) {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				this.btnCancel.Text = CMD_CANCEL;
				this.btnOK.Text = CMD_OK;
				
				//
				this.mEntry = entry;
				this.mMediator = mediator;
				if(this.mEntry != null)
					this.mEntry.EntryChanged += new EventHandler(this.OnEntryChanged);
				this.mToolTip = new System.Windows.Forms.ToolTip();
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
		protected virtual void UpdateEntry() { }
		
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgSchedule));
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(462, 414);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(96, 24);
			this.btnOK.TabIndex = 118;
			this.btnOK.Text = "&OK";
			this.btnOK.Click += new System.EventHandler(this.OnCommandClick);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(564, 414);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(96, 24);
			this.btnCancel.TabIndex = 121;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.OnCommandClick);
			// 
			// dlgSchedule
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(664, 446);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.SystemColors.WindowText;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "dlgSchedule";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Untitled - Dispatch Schedule Entry";
			this.Resize += new System.EventHandler(this.OnFormResize);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.OnClosing);
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;			
			//Event handler for form load event
			try {
				//Show early
				this.Visible = true;
				Application.DoEvents();
				#region Tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
				#endregion
				
				//Set command button identities
				this.btnCancel.Text = CMD_CANCEL;
				this.btnOK.Text = CMD_OK;
			}
			catch(Exception ex) { throw ex; }
			finally { 
				OnValidateForm(null, null);
				this.Cursor = Cursors.Default; 
			}
		}
		private void OnFormResize(object sender, System.EventArgs e) {
			//Event handler for change in dialog size
		}
		private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e) {
			//Event handler for dialog closing
			try {
				if(this.mIsDirty) {
					if(MessageBox.Show(this, "This schedule has unsaved changes. Would you like to save it?", App.Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
						this.mEntry.Update();
				}
				e.Cancel = false;
			}
			catch(Exception ex)  { MessageBox.Show(this, ex.Message); }
		}
		private void OnEntryChanged(object sender, EventArgs e) {
			//Event handler for change in entry
			try {
				this.mIsDirty = false;
			}
			catch(Exception) { }
			finally  { OnValidateForm(null, null); }
		}
		
		protected virtual void OnValidateForm(object sender, System.EventArgs e) {
			//Validate control data and set servces accordingly
			this.btnOK.Enabled = false;
		}
		protected void reportError(Exception ex) {
			//Report an exception to the user
			OnStatusMessage(this, new StatusEventArgs(ex.Message));
			//ArgixTrace.WriteLine(new TraceMessage(ex.ToString(), App.EventLogName, LogLevel.Error, ex.Source, App.EX_UNEXPECTED, ""));
		}		
		private void OnCommandClick(object sender, System.EventArgs e) {
			//Event handler for button selection
			this.Cursor = Cursors.WaitCursor;
			try {
				Button btn = (Button)sender;
				switch(btn.Text) {
					case CMD_CANCEL:
						this.DialogResult = DialogResult.Cancel;
						break;
					case CMD_OK:
						UpdateEntry();
						this.DialogResult = DialogResult.OK;
						break;
				}
				this.Hide();
				this.Close();
			}
			catch(Exception ex) { throw ex; }
			finally  { this.Cursor = Cursors.Default; }
		}
		protected void LoadSelections(string scheduleName, ComboBox comboBox) {
			//Read user selections for the specified control
			try {
				DataSet selections = ReadSelections(scheduleName, comboBox);
				for(int i=0; i<selections.Tables["SelectionTable"].Rows.Count; i++)
                    comboBox.Items.Add(selections.Tables["SelectionTable"].Rows[i]["Selection"]);
			} catch { }
		}
		protected void SaveSelections(string scheduleName, ComboBox comboBox) {
			//Update user selections for the specified control
			try {
				//Read entire selections file
				SelectionsDS ds = new SelectionsDS();
				ds.Merge(this.mMediator.FillDataset(App.SELECTION_DATAFILE, "", null));
				
				//Add current combobox entry if doesn't exist
				SelectionsDS.SelectionTableRow[] rows = (SelectionsDS.SelectionTableRow[])ds.SelectionTable.Select("ScheduleName='" + scheduleName + "' AND ControlName='" + comboBox.Name + "' AND Selection='" + comboBox.Text + "'");
				if(rows.Length == 0) { 
					ds.SelectionTable.AddSelectionTableRow(scheduleName, comboBox.Name, comboBox.Text);
					ds.SelectionTable.AcceptChanges();
					this.mMediator.ExecuteNonQuery(App.SELECTION_DATAFILE, new object[]{ds});
				}
			} catch { }
		}
		private DataSet ReadSelections(string scheduleName, ComboBox comboBox) {
			//Read user selections for the specified control
			SelectionsDS selections=null;
			try {
				selections = new SelectionsDS();
                DataSet ds = new DataSet();
				ds.Merge(this.mMediator.FillDataset(App.SELECTION_DATAFILE, "", null));
				selections.Merge(ds.Tables["SelectionTable"].Select("ScheduleName='" + scheduleName + "' AND ControlName='" + comboBox.Name + "'"));
			}
			catch(Exception ex) { throw ex; }
			return selections;
		}
		protected void OnServiceStatesChanged(object sender, EventArgs e) { if(this.ServiceStatesChanged != null) this.ServiceStatesChanged(sender, e); }
		protected void OnStatusMessage(object sender, StatusEventArgs e) { if(this.StatusMessage != null) this.StatusMessage(sender, e); }
	}
}
