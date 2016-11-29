//	File:	labeltemplate.cs
//	Author:	J. Heary
//	Date:	01/02/08
//	Desc:	Represents an outbound Argix label template (a label with tokens not data).
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Text;

namespace Tsort.Labels {
	/// <summary>
	/// 
	/// </summary>
	public abstract class LabelTemplate { 
		//Members
        /// <summary>The name of this label template (i.e. 47A).</summary>
        protected string mLabelType="";
        /// <summary>The printer type this label is desined for (i.e. 110).</summary>
        protected string mPrinterType="";
        /// <summary>The label template format zpl including tokens.</summary>
        protected string mLabelString="";

        /// <summary>The event that occurrs when the label template is changed.</summary>
        public event EventHandler TemplateChanged=null;

        //Interface
        /// <summary>Constructor: creates a label template with default values.</summary>
        public LabelTemplate() : this(null) { }
        /// <summary>Constructor: creates a label template initialized from parameter labelTemplate.</summary>
        public LabelTemplate(LabelDS.LabelDetailTableRow labelTemplate) { 
			//Constructor
			try { 
				if(labelTemplate != null) { 
					this.mLabelType = labelTemplate.LABEL_TYPE;
                    this.mPrinterType = !labelTemplate.IsPrinterTypeNull() ? labelTemplate.PrinterType : "";
                    this.mLabelString = !labelTemplate.IsLABEL_STRINGNull() ? labelTemplate.LABEL_STRING : "";
				}
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Label Template instance.", ex); }
		}
		#region Accessors\Modifiers: [Members...], ToDataSet()
        /// <summary>Gets or sets the label type.</summary>
		public string LabelType { get { return this.mLabelType; } set { this.mLabelType = value; } }
        /// <summary>Gets or sets the printer type.</summary>
        public string PrinterType { get { return this.mPrinterType; } set { this.mPrinterType = value; } }
        /// <summary>Gets or sets the label string.</summary>
        public string LabelString { get { return this.mLabelString; } set { this.mLabelString = value; } }
        /// <summary>Returns the label template as an xml object.</summary>
        public DataSet ToDataSet() {
            //Return a dataset containing values for this object
            LabelDS ds=null;
            try {
                ds = new LabelDS();
                LabelDS.LabelDetailTableRow labelTemplate = ds.LabelDetailTable.NewLabelDetailTableRow();
                labelTemplate.LABEL_TYPE = this.mLabelType;
                labelTemplate.PrinterType = this.mPrinterType;
                labelTemplate.LABEL_STRING = this.mLabelString;
                ds.LabelDetailTable.AddLabelDetailTableRow(labelTemplate);
            }
            catch(Exception) { }
            return ds;
        }
		#endregion

        /// <summary>Returns the label string (i.e. zpl with tokens).</summary>
        public string Template { get { return this.mLabelString; } }
        /// <summary>Create a new label template in the labels store.</summary>
        public abstract bool Create();
        /// <summary>Update an existing label template in the labels store.</summary>
        public abstract bool Update();
        /// <summary>Delete an existing label template from the labels store.</summary>
        public abstract bool Delete();
        /// <summary>Create a copy of this object.</summary>
        public abstract LabelTemplate Copy();
        /// <summary>Exports this object to the specified filename.</summary>
        public virtual bool Export(string fileName) {
			//
			System.IO.StreamWriter writer=null;
			bool ret=false;
			try  {
				writer = System.IO.File.CreateText(fileName);
				writer.Write(this.LabelString);
                ret = true;
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while exporting Label Template.", ex); }
			finally { if(writer!= null) writer.Close();}
			return ret;
		}
        /// <summary>Fires the TemplateChanged event on behalf of derived classes.</summary>
        protected void OnTemplateChanged(object sender,EventArgs e) { 
			//Fire event on behalf of derived classes
			if(this.TemplateChanged != null) this.TemplateChanged(sender, e);
		}
	}
}