using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Argix {
	//
	public class SearchButton : System.Windows.Forms.UserControl {
		//Members
		
		#region Controls
		private System.Windows.Forms.ToolBar tlbControl;
		private System.Windows.Forms.ToolBarButton btnSearch;
		private System.Windows.Forms.ImageList imgControl;
		private System.Windows.Forms.TextBox txtSearch;
		private System.ComponentModel.IContainer components;
		#endregion
		//Constants
		//Events
		public event EventHandler ValueChanged=null;
		
		//Interface
		public SearchButton() {
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}
		protected override void Dispose( bool disposing ) { if(disposing) { if(components != null) components.Dispose(); } base.Dispose( disposing ); }
		public override string Text { get { return this.txtSearch.Text; } }
		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SearchButton));
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.tlbControl = new System.Windows.Forms.ToolBar();
			this.btnSearch = new System.Windows.Forms.ToolBarButton();
			this.imgControl = new System.Windows.Forms.ImageList(this.components);
			this.tlbControl.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtSearch
			// 
			this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtSearch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtSearch.Location = new System.Drawing.Point(27, 2);
			this.txtSearch.MaxLength = 24;
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(114, 21);
			this.txtSearch.TabIndex = 0;
			this.txtSearch.Text = "";
			this.txtSearch.WordWrap = false;
			this.txtSearch.TextChanged += new System.EventHandler(this.OnSearchTextChange);
			this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnTextBoxKeyUp);
			// 
			// tlbControl
			// 
			this.tlbControl.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.tlbControl.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						  this.btnSearch});
			this.tlbControl.ButtonSize = new System.Drawing.Size(16, 16);
			this.tlbControl.Controls.Add(this.txtSearch);
			this.tlbControl.Divider = false;
			this.tlbControl.DropDownArrows = true;
			this.tlbControl.ImageList = this.imgControl;
			this.tlbControl.Location = new System.Drawing.Point(0, 0);
			this.tlbControl.Name = "tlbControl";
			this.tlbControl.ShowToolTips = true;
			this.tlbControl.Size = new System.Drawing.Size(144, 26);
			this.tlbControl.TabIndex = 1;
			this.tlbControl.Wrappable = false;
			this.tlbControl.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.OnSearchButtonClick);
			// 
			// btnSearch
			// 
			this.btnSearch.ImageIndex = 0;
			this.btnSearch.ToolTipText = "Search";
			// 
			// imgControl
			// 
			this.imgControl.ImageSize = new System.Drawing.Size(16, 16);
			this.imgControl.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgControl.ImageStream")));
			this.imgControl.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// SearchButton
			// 
			this.Controls.Add(this.tlbControl);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "SearchButton";
			this.Size = new System.Drawing.Size(144, 27);
			this.tlbControl.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		
		protected override void OnPaint(PaintEventArgs pe) {
			//TODO: Add custom paint code here
			
			//Calling the base class OnPaint
			base.OnPaint(pe);
		}
		#region Local Services: OnSearchTextChange(), OnSearchButtonClick(), OnTextBoxKeyUp()
		private void OnSearchTextChange(object sender, System.EventArgs e) {
			//Event handler for search text change event
			if(this.ValueChanged != null) this.ValueChanged(this, EventArgs.Empty);
		}
		private void OnSearchButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e) {
			//Event handler for search button click event
			base.OnClick(EventArgs.Empty);
		}
		private void OnTextBoxKeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			//Event handler for textbox keyup event- raise user control KeyUp event
			base.OnKeyUp(e);
		}
		#endregion
	}
}
