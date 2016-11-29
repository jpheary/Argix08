using System;
using System.Data;
using System.Diagnostics;
using System.Text;
using Tsort.Labels;

namespace Argix.Freight {
	//
	public class OutboundLabel: LabelTemplate { 
		//Members		

		//Interface
		public OutboundLabel(): this(null) { }
		public OutboundLabel(LabelDS.LabelDetailTableRow labelTemplate) : base(labelTemplate) { }
		public override bool Create() { return false; }
		public override bool Update() { return false; }
		public override bool Delete() { return false; }
		public override LabelTemplate Copy() { return new OutboundLabel((LabelDS.LabelDetailTableRow)this.ToDataSet().Tables["LabelDetailTable"].Rows[0]); }
	}
}