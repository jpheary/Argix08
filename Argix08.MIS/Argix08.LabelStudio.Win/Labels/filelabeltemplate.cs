//	File:	filelabeltemplate.cs
//	Author:	J. Heary
//	Date:	08/17/05
//	Desc:	Concrete implementation of the abstract Tsort.Labels.LabelTemplate 
//			class; implements a label template that represents a single entry 
//			in a file (*.*) containing a collection label templates.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using Argix.Data;

namespace Tsort.Labels {
	//
	public class FileLabelTemplate: LabelTemplate { 
		//Members

        //Interface
		public FileLabelTemplate(LabelDS.LabelDetailTableRow labelTemplate) : base(labelTemplate) { }
		public override bool Create() { return false; }
		public override bool Update() { return false; }
		public override bool Delete() { return false; }
		public override LabelTemplate Copy() { return new FileLabelTemplate((LabelDS.LabelDetailTableRow)this.ToDataSet().Tables["LabelDetailTable"].Rows[0]); }
	}
}