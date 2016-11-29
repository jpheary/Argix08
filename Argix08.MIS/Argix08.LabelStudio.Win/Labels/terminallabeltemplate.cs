//	File:	terminallabeltemplate.cs
//	Author:	J. Heary
//	Date:	08/17/05
//	Desc:	Concrete implementation of the abstract Tsort.Labels.LabelTemplate 
//			class; implements a label template that represents a single record 
//			in a database table.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using Argix.Data;

namespace Tsort.Labels {
	//
	public class TerminalLabelTemplate: LabelTemplate { 
		//Members
		private Mediator mMediator=null;
		
		//Interface
		public TerminalLabelTemplate(): this(null, null) { }
		public TerminalLabelTemplate(LabelDS.LabelDetailTableRow labelTemplate, Mediator mediator) : base(labelTemplate) { 
			//Constructor
			try { 
				this.mMediator = mediator;
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Terminal Label Template instance.", ex); }
		}
		public override bool Create() {
			//Save this label template
			bool ret=false;
			try {
                ret = this.mMediator.ExecuteNonQuery(App.USP_LABEL_UPDATEORNEW,new object[] { base.LabelType,base.mPrinterType,base.mLabelString });
				base.OnTemplateChanged(this, EventArgs.Empty);
			}
			catch(Exception ex) { throw new ApplicationException("Failed to create Terminal label template " + base.mLabelString + ".", ex); }
			return ret;
		}
		public override bool Update() {
			//Update this label template
			bool ret=false;
			try {
                ret = this.mMediator.ExecuteNonQuery(App.USP_LABEL_UPDATEORNEW,new object[] { base.LabelType,base.mPrinterType,base.mLabelString });
				base.OnTemplateChanged(this, EventArgs.Empty);
			}
			catch(Exception ex) { throw new ApplicationException("Failed to update Terminal label template " + base.mLabelString + ".", ex); }
			return ret;
		}
		public override bool Delete() {
			//Delete this label template
			bool ret=false;
			try {
				ret = this.mMediator.ExecuteNonQuery(App.USP_LABEL_DELETE, new object[]{base.mLabelString, base.mPrinterType});
				base.OnTemplateChanged(this, EventArgs.Empty);
			}
			catch(Exception ex) { throw new ApplicationException("Failed to delete Terminal label template " + base.mLabelString + ".", ex); }
			return ret;
		}
		public override LabelTemplate Copy() { return new TerminalLabelTemplate((LabelDS.LabelDetailTableRow)this.ToDataSet().Tables["LabelDetailTable"].Rows[0], this.mMediator); }
	}
}