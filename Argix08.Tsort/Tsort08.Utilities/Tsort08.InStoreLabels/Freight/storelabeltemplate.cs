//	File:	storelabeltemplate.cs
//	Author:	J. Heary
//	Date:	06/16/08
//	Desc:	Concrete implementation of the abstract Tsort.Labels.LabelTemplate 
//			class; implements a label template that represents a single record 
//			in a database table.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Text;
using Tsort.Labels;
using Argix.Data;

namespace Argix.Freight {
    //
	public class StoreLabelTemplate: LabelTemplate { 
		//Members
		private Mediator mMediator=null;
				
		//Interface
		public StoreLabelTemplate(): this(null, null) { }
        public StoreLabelTemplate(LabelDS.LabelDetailTableRow labelTemplate,Mediator mediator)
            : base(labelTemplate) { 
			//Constructor
			try { 
				this.mMediator = mediator;
			}
			catch(Exception ex) { throw ex; }
		}
		public override bool Create() { return false; }
		public override bool Update() { return false; }
		public override bool Delete() { return false; }
		public override LabelTemplate Copy() { return null; }
	}
}