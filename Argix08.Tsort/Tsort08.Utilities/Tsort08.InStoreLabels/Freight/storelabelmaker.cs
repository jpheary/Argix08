//	File:	storelabelmaker.cs
//	Author:	J. Heary
//	Date:	06/16/08
//	Desc:	Concrete implementation of the abstract Tsort.Labels.LabelMaker class;
//			implements a label maker for the InStore Labels utility.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using Tsort.Labels;

namespace Argix.Freight {
	//
	public class StoreLabelMaker: LabelMaker {
		//Members
		//Constants
		//Events
		//Interface
		public StoreLabelMaker() { }
		protected override void setLabelTokenValues() { 
			//Override token values with this objects data
		}
		public override string Name { get { return "InStoreLabels"; } }
		
		[ Category("Store"), Description("Argix store number.")]
		public string StoreNumber { 
			get { return base.mTokens[TokenLibrary.STORENUMBER].ToString(); } 
			set { base.mTokens[TokenLibrary.STORENUMBER] = value; base.OnLabelValuesChanged(this, new EventArgs()); } 
		}
	}
}