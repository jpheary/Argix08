//	File:	winlabel.cs
//	Author:	J. Heary
//	Date:	08/15/05
//	Desc:	Dialog to view/edit label templates.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Tsort.Labels;
using Tsort.Devices.Printers;

namespace Tsort.Tools {
	//
	public class winLabel : System.Windows.Forms.Form {
		//Members
		private LabelTemplateNode mLabelTemplateNode=null;
		private LabelMaker mLabelMaker=null;
		private Tsort.Labels.Label mLabel=null;
		private bool mIsDirty=false;
				
		private const string MNU_REFRESH = "&Refresh";
		private const string MNU_CUT = "Cu&t";
		private const string MNU_COPY = "&Copy";
		private const string MNU_PASTE = "&Paste";

        public event EventHandler ServiceStatesChanged=null;

		#region Controls

		private System.Windows.Forms.Splitter splitterH;
		private System.Windows.Forms.MenuItem ctxRefresh;
		private System.Windows.Forms.ContextMenu ctxConfig;
		private System.Windows.Forms.MenuItem ctxSep1;
		private System.Windows.Forms.MenuItem ctxCut;
		private System.Windows.Forms.MenuItem ctxCopy;
		private System.Windows.Forms.MenuItem ctxPaste;
		private System.Windows.Forms.Panel pnlBottom;
		private System.Windows.Forms.RichTextBox txtLabel;
		private System.Windows.Forms.Label lblLabel;
		private System.Windows.Forms.Panel pnlLabel;
		private System.Windows.Forms.Panel pnlTop;
		private System.Windows.Forms.Panel pnlTemplate;
		private System.Windows.Forms.Label lblTemplate;
		private System.Windows.Forms.RichTextBox txtTemplate;
		private System.Windows.Forms.Label lblTemplateSize;
		private System.Windows.Forms.Label lblLabelSize;
		
		private System.ComponentModel.Container components = null;		//Required designer variable
		#endregion
		
		//Interface
		public winLabel(LabelTemplateNode node, LabelMaker labelMaker) {
			//Constructor
			//Required for Windows Form Designer support
			InitializeComponent();
			#region Menu identities (used for onclick handlers) 
			this.ctxRefresh.Text = MNU_REFRESH;
			this.ctxCut.Text = MNU_CUT;
			this.ctxCopy.Text = MNU_COPY;
			this.ctxPaste.Text = MNU_PASTE;
			#endregion
			#region Window docking
			this.splitterH.MinExtra = this.splitterH.MinSize = 48;
			this.splitterH.Dock = this.pnlBottom.Dock = DockStyle.Bottom;
			this.pnlTop.Dock = DockStyle.Fill;
			this.Controls.AddRange(new Control[]{this.pnlTop, this.splitterH, this.pnlBottom});
			#endregion
			
			//Bind to template and register for template events
			this.mLabelTemplateNode = node;
			this.mLabelTemplateNode.LabelTemplate.TemplateChanged += new EventHandler(this.OnTemplateChanged);
			this.mLabelMaker = labelMaker;
			this.mLabelMaker.LabelValuesChanged += new EventHandler(OnLabelValuesChanged);
		}
		protected override void Dispose( bool disposing ) { if( disposing ) { if(components != null) components.Dispose(); } base.Dispose( disposing ); }
		public LabelMaker LabelMaker {
			get { return this.mLabelMaker; }
			set { 
				this.mLabelMaker = value;
				this.mLabelMaker.LabelValuesChanged += new EventHandler(OnLabelValuesChanged);
				UpdateLabel();
			}
		}
		public void UpdateLabel() {
			//Update the label due to changes in the label template or labelmaker
			try  {
				//Create a copy of the current label template and set the format to textbox text
				//This way, the label reflects the unsaved template changes
				LabelTemplate oLabelTemplate = this.mLabelTemplateNode.LabelTemplate.Copy();
				oLabelTemplate.LabelString = this.txtTemplate.Text;
				this.mLabel = new Tsort.Labels.Label(oLabelTemplate, this.mLabelMaker);
				this.lblLabel.Text = "Format: (" + this.mLabelMaker.Name + ")";
				this.txtLabel.Text = this.mLabel.LabelFormat;
				this.lblLabelSize.Text = this.txtLabel.Text.Length.ToString() + " chars";
			}
			catch(Exception ex)  { MessageBox.Show(this, ex.Message); }
		}
		public bool CanSave { get { return this.mIsDirty; } }
		public void PrintLabel(ILabelPrinter printer) { 
            //printer.Print(this.txtLabel.Text); 
            printer.Print(this.txtLabel.Text);
        }
		public void SaveLabelTemplate() {
			//Apply changes to label template and persist
            this.mLabelTemplateNode.LabelTemplate.LabelString = this.txtTemplate.Text;
			this.mLabelTemplateNode.LabelTemplate.Update();
			this.mIsDirty = false;
		}
		public void ExportLabelTemplate(string fileName) { this.mLabelTemplateNode.LabelTemplate.Export(fileName); }
		public bool CanCut { get { return this.ctxCut.Enabled; } }
		public void Cut() { this.ctxCut.PerformClick(); }
		public bool CanCopy { get { return this.ctxCopy.Enabled; } }
		public void Copy() { this.ctxCopy.PerformClick(); }
		public bool CanPaste { get { return this.ctxPaste.Enabled; } }
		public void Paste() { this.ctxPaste.PerformClick(); }
        public void RefreshLabelTemplate() { this.mLabelTemplateNode.Refresh(); }
		
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(winLabel));
			this.ctxConfig = new System.Windows.Forms.ContextMenu();
			this.ctxRefresh = new System.Windows.Forms.MenuItem();
			this.ctxSep1 = new System.Windows.Forms.MenuItem();
			this.ctxCut = new System.Windows.Forms.MenuItem();
			this.ctxCopy = new System.Windows.Forms.MenuItem();
			this.ctxPaste = new System.Windows.Forms.MenuItem();
			this.splitterH = new System.Windows.Forms.Splitter();
			this.pnlBottom = new System.Windows.Forms.Panel();
			this.txtLabel = new System.Windows.Forms.RichTextBox();
			this.pnlLabel = new System.Windows.Forms.Panel();
			this.lblLabel = new System.Windows.Forms.Label();
			this.pnlTop = new System.Windows.Forms.Panel();
			this.txtTemplate = new System.Windows.Forms.RichTextBox();
			this.pnlTemplate = new System.Windows.Forms.Panel();
			this.lblTemplate = new System.Windows.Forms.Label();
			this.lblTemplateSize = new System.Windows.Forms.Label();
			this.lblLabelSize = new System.Windows.Forms.Label();
			this.pnlBottom.SuspendLayout();
			this.pnlLabel.SuspendLayout();
			this.pnlTop.SuspendLayout();
			this.pnlTemplate.SuspendLayout();
			this.SuspendLayout();
			// 
			// ctxConfig
			// 
			this.ctxConfig.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.ctxRefresh,
																					  this.ctxSep1,
																					  this.ctxCut,
																					  this.ctxCopy,
																					  this.ctxPaste});
			// 
			// ctxRefresh
			// 
			this.ctxRefresh.Index = 0;
			this.ctxRefresh.Text = "Refresh";
			this.ctxRefresh.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxSep1
			// 
			this.ctxSep1.Index = 1;
			this.ctxSep1.Text = "-";
			// 
			// ctxCut
			// 
			this.ctxCut.Index = 2;
			this.ctxCut.Text = "C&ut";
			this.ctxCut.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxCopy
			// 
			this.ctxCopy.Index = 3;
			this.ctxCopy.Text = "&Copy";
			this.ctxCopy.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxPaste
			// 
			this.ctxPaste.Index = 4;
			this.ctxPaste.Text = "&Paste";
			this.ctxPaste.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// splitterH
			// 
			this.splitterH.Cursor = System.Windows.Forms.Cursors.HSplit;
			this.splitterH.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitterH.Location = new System.Drawing.Point(0, 182);
			this.splitterH.Name = "splitterH";
			this.splitterH.Size = new System.Drawing.Size(664, 3);
			this.splitterH.TabIndex = 114;
			this.splitterH.TabStop = false;
			// 
			// pnlBottom
			// 
			this.pnlBottom.Controls.Add(this.txtLabel);
			this.pnlBottom.Controls.Add(this.pnlLabel);
			this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlBottom.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pnlBottom.ForeColor = System.Drawing.SystemColors.WindowText;
			this.pnlBottom.Location = new System.Drawing.Point(0, 185);
			this.pnlBottom.Name = "pnlBottom";
			this.pnlBottom.Size = new System.Drawing.Size(664, 153);
			this.pnlBottom.TabIndex = 22;
			// 
			// txtLabel
			// 
			this.txtLabel.AutoWordSelection = true;
			this.txtLabel.ContextMenu = this.ctxConfig;
			this.txtLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtLabel.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtLabel.Location = new System.Drawing.Point(0, 24);
			this.txtLabel.Name = "txtLabel";
			this.txtLabel.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
			this.txtLabel.ShowSelectionMargin = true;
			this.txtLabel.Size = new System.Drawing.Size(664, 129);
			this.txtLabel.TabIndex = 111;
			this.txtLabel.Text = "";
			this.txtLabel.WordWrap = false;
			this.txtLabel.Leave += new System.EventHandler(this.OnEnterLeave);
			this.txtLabel.Enter += new System.EventHandler(this.OnEnterLeave);
			// 
			// pnlLabel
			// 
			this.pnlLabel.Controls.Add(this.lblLabelSize);
			this.pnlLabel.Controls.Add(this.lblLabel);
			this.pnlLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlLabel.DockPadding.All = 3;
			this.pnlLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pnlLabel.ForeColor = System.Drawing.SystemColors.WindowText;
			this.pnlLabel.Location = new System.Drawing.Point(0, 0);
			this.pnlLabel.Name = "pnlLabel";
			this.pnlLabel.Size = new System.Drawing.Size(664, 24);
			this.pnlLabel.TabIndex = 114;
			// 
			// lblLabel
			// 
			this.lblLabel.BackColor = System.Drawing.SystemColors.Control;
			this.lblLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblLabel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblLabel.Location = new System.Drawing.Point(3, 3);
			this.lblLabel.Name = "lblLabel";
			this.lblLabel.Size = new System.Drawing.Size(658, 18);
			this.lblLabel.TabIndex = 113;
			this.lblLabel.Text = "Format";
			this.lblLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnlTop
			// 
			this.pnlTop.Controls.Add(this.txtTemplate);
			this.pnlTop.Controls.Add(this.pnlTemplate);
			this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTop.Location = new System.Drawing.Point(0, 0);
			this.pnlTop.Name = "pnlTop";
			this.pnlTop.Size = new System.Drawing.Size(664, 180);
			this.pnlTop.TabIndex = 116;
			// 
			// txtTemplate
			// 
			this.txtTemplate.AutoWordSelection = true;
			this.txtTemplate.ContextMenu = this.ctxConfig;
			this.txtTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtTemplate.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtTemplate.Location = new System.Drawing.Point(0, 24);
			this.txtTemplate.Name = "txtTemplate";
			this.txtTemplate.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
			this.txtTemplate.ShowSelectionMargin = true;
			this.txtTemplate.Size = new System.Drawing.Size(664, 156);
			this.txtTemplate.TabIndex = 117;
			this.txtTemplate.Text = "";
			this.txtTemplate.WordWrap = false;
			this.txtTemplate.Leave += new System.EventHandler(this.OnEnterLeave);
			this.txtTemplate.TextChanged += new System.EventHandler(this.OnTemplateTextChanged);
			this.txtTemplate.Enter += new System.EventHandler(this.OnEnterLeave);
			// 
			// pnlTemplate
			// 
			this.pnlTemplate.Controls.Add(this.lblTemplateSize);
			this.pnlTemplate.Controls.Add(this.lblTemplate);
			this.pnlTemplate.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTemplate.DockPadding.All = 3;
			this.pnlTemplate.Location = new System.Drawing.Point(0, 0);
			this.pnlTemplate.Name = "pnlTemplate";
			this.pnlTemplate.Size = new System.Drawing.Size(664, 24);
			this.pnlTemplate.TabIndex = 116;
			// 
			// lblTemplate
			// 
			this.lblTemplate.BackColor = System.Drawing.SystemColors.Control;
			this.lblTemplate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblTemplate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblTemplate.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblTemplate.Location = new System.Drawing.Point(3, 3);
			this.lblTemplate.Name = "lblTemplate";
			this.lblTemplate.Size = new System.Drawing.Size(658, 18);
			this.lblTemplate.TabIndex = 113;
			this.lblTemplate.Text = "Template";
			this.lblTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblTemplateSize
			// 
			this.lblTemplateSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblTemplateSize.Dock = System.Windows.Forms.DockStyle.Right;
			this.lblTemplateSize.Location = new System.Drawing.Point(589, 3);
			this.lblTemplateSize.Name = "lblTemplateSize";
			this.lblTemplateSize.Size = new System.Drawing.Size(72, 18);
			this.lblTemplateSize.TabIndex = 114;
			this.lblTemplateSize.Text = "9999 chars";
			this.lblTemplateSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblLabelSize
			// 
			this.lblLabelSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblLabelSize.Dock = System.Windows.Forms.DockStyle.Right;
			this.lblLabelSize.Location = new System.Drawing.Point(589, 3);
			this.lblLabelSize.Name = "lblLabelSize";
			this.lblLabelSize.Size = new System.Drawing.Size(72, 18);
			this.lblLabelSize.TabIndex = 115;
			this.lblLabelSize.Text = "9999 chars";
			this.lblLabelSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// winLabel
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(664, 338);
			this.Controls.Add(this.pnlTop);
			this.Controls.Add(this.splitterH);
			this.Controls.Add(this.pnlBottom);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.SystemColors.WindowText;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "winLabel";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Label Editor";
			this.Resize += new System.EventHandler(this.OnFormResize);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.OnClosing);
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.Activated += new System.EventHandler(this.OnActivated);
			this.Deactivate += new System.EventHandler(this.OnDeactivate);
			this.pnlBottom.ResumeLayout(false);
			this.pnlLabel.ResumeLayout(false);
			this.pnlTop.ResumeLayout(false);
			this.pnlTemplate.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
				//Show early
				this.Visible = true;
				Application.DoEvents();				
				this.Text = "Label Template: " + this.mLabelTemplateNode.FullPath;
				this.pnlBottom.Height = this.Height / 2;
				this.txtLabel.ReadOnly = true;
                this.lblTemplate.Text = "Template: " + this.mLabelTemplateNode.LabelTemplate.LabelType + "/" + this.mLabelTemplateNode.LabelTemplate.PrinterType;
				OnTemplateChanged(null, null);
			} 
			catch(Exception ex) { throw ex; }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnActivated(object sender, System.EventArgs e) {
			//Event handler for window activation
			this.mLabelTemplateNode.TreeView.SelectedNode = this.mLabelTemplateNode;
		}
		private void OnDeactivate(object sender, System.EventArgs e) { }
		private void OnFormResize(object sender, System.EventArgs e) { }
		private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e) {
			//Event handler for dialog closing
			try {
				if(this.mIsDirty) {
					if(MessageBox.Show(this, "This label template has unsaved changes. Would you like to save it?", App.Title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
						this.SaveLabelTemplate();
				}
				e.Cancel = false;
			}
			catch(Exception ex)  { MessageBox.Show(this, ex.Message); }
		}
		private void OnTemplateChanged(object sender, EventArgs e) {
			//Event handler for change in label template
			this.Cursor = Cursors.WaitCursor;
			try {
				//
				this.txtTemplate.Text = this.mLabelTemplateNode.LabelTemplate.LabelString;
				this.lblTemplateSize.Text = this.txtTemplate.Text.Length.ToString() + " chars";
				this.mIsDirty = false;
				UpdateLabel();
			}
			catch(Exception) { }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnTemplateTextChanged(object sender, System.EventArgs e) {
			//Event handler for change in lable template
			try {
				this.lblTemplateSize.Text = this.txtTemplate.Text.Length.ToString() + " chars";
				this.mIsDirty = true;
				UpdateLabel();
			}
			catch(Exception ex)  { MessageBox.Show(this, ex.Message); }
			finally  { setUserServices(); }
		}
		private void OnLabelValuesChanged(object sender, EventArgs e) {
			//Event handler for change in a labelmaker token value
			UpdateLabel();
		}
		private void OnMenuClick(object sender, System.EventArgs e) {
			//Event handler for menu selection
			this.Cursor = Cursors.WaitCursor;
			try  {
				MenuItem menu = (MenuItem)sender;
				switch(menu.Text)  {
					case MNU_REFRESH:	
						//Refresh label template to current (persistent) state or label to dialog state
						if(this.txtTemplate.Focused)
                            this.txtTemplate.Text = this.mLabelTemplateNode.LabelTemplate.LabelString;
						else if(this.txtLabel.Focused) 
							UpdateLabel();
						break;
					case MNU_CUT:		this.txtTemplate.Cut(); break;
					case MNU_COPY:		this.txtTemplate.Copy(); break;
					case MNU_PASTE:		this.txtTemplate.Paste(); break;
				}
			}
			catch(Exception ex)  { MessageBox.Show(this, ex.Message); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		#region Local Services: setUserServices(), OnEnterLeave()
		private void setUserServices() {
			//Set user services
			try {
				//Set menu/context menu states
				this.ctxRefresh.Enabled = true;
                this.ctxCut.Enabled = this.txtTemplate.SelectedText.Length > 0;
                this.ctxCopy.Enabled = this.txtTemplate.SelectedText.Length > 0;
                this.ctxPaste.Enabled = this.txtTemplate.CanPaste(DataFormats.GetFormat("Text"));
				
				if(this.ServiceStatesChanged!=null) this.ServiceStatesChanged(this, new EventArgs());
			}
			catch(Exception)  { }
		}
		private void OnEnterLeave(object sender, System.EventArgs e) {
			//Event handler for enter and leave events
			try { 
				this.lblTemplate.BackColor = (this.txtTemplate.Focused) ? SystemColors.ActiveCaption : SystemColors.Control;
				this.lblTemplate.ForeColor = (this.txtTemplate.Focused) ? SystemColors.ActiveCaptionText : SystemColors.ControlText;
				this.lblLabel.BackColor = (this.txtLabel.Focused) ? SystemColors.ActiveCaption : SystemColors.Control;
				this.lblLabel.ForeColor = (this.txtLabel.Focused) ? SystemColors.ActiveCaptionText : SystemColors.ControlText;
			}
			catch(Exception) { }
		}
		#endregion
	}
}
