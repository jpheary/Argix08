//	File:	oblabeltemplate.cs
//	Author:	J. Heary
//	Date:	02/07/05
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

namespace Tsort.Sort {
    //
    internal class OBLabelTemplate:LabelTemplate {
        //Members		
        //Constants
        //Events
        //Interface
        public OBLabelTemplate() : this(null) { }
        public OBLabelTemplate(LabelDS.LabelDetailTableRow labelTemplate)
            : base(labelTemplate) {
            //Constructor
        }
        public override bool Create() { return false; }
        public override bool Update() { return false; }
        public override bool Delete() { return false; }
        public override LabelTemplate Copy() { return new OBLabelTemplate(((LabelDS)base.ToDataSet()).LabelDetailTable[0]); }
    }
}