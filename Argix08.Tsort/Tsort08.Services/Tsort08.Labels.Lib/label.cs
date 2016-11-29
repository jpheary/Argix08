//	File:	label.cs
//	Author:	J. Heary
//	Date:	01/02/08
//	Desc:	Represents the state and behavior of a Tsort outbound label.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;

namespace Tsort.Labels {
	/// <summary> Represents the state and behavior of a Tsort outbound label. </summary>
	public class Label { 
		//Members
		private string mLabelFormat="";
				
		//Interface
		public Label(LabelTemplate labelTemplate, LabelMaker labelMaker) {
			//Constructor
			try {
				//Format the label template using the specified label maker
                this.mLabelFormat = labelMaker.FormatLabelTemplate(labelTemplate.Template);
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Label instance.", ex); }
		}
		public string LabelFormat { get { return this.mLabelFormat.ToString(); } }
	}
}