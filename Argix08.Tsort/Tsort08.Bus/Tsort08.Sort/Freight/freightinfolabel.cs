//	File:	freightinfolabel.cs
//	Author:	J. Heary
//	Date:	02/16/05
//	Desc:	Adapts a FreightInfo object into an Inbound Label object.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace Tsort.Freight {
	//
	public class FreightInfoLabel: InboundLabel {
		//Members
		private FreightInfo mFreightInfo=null;
		
		//Constants
		//Events
		//Interface
		public FreightInfoLabel(InboundLabelDS.FreightInfoRegTableRow label): base(null) { 
			//Constructor
			try { 
				//Create freight underlying info object
				this.mFreightInfo = new FreightInfo(label);
				
				//Map freight info object to Inbound Label base type
				base.mLabelID = Convert.ToInt32(this.mFreightInfo.Type);
				base.mSortTypeID = 0;
				base.mDescription = "";
				adapt();
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Freight Info Label instance.", ex); }
		}
		#region Accessors\Modifiers: [Members]..., ToDataSet()
		public override DataSet ToDataSet() {
			//Return a dataset containing values for this object
			DataSet ds=null;
			try {
				ds = new DataSet();
				ds.Merge(base.ToDataSet());
				ds.Merge(this.mFreightInfo.ToDataSet());
			}
			catch(Exception) { }
			return ds;
		}
		#endregion
		public FreightInfo FreightInfo { get { return this.mFreightInfo; } }
		
		#region Local Services: adapt(), createDataElement()
		private void adapt() {
			//Event handler for change in label data
			InboundLabelInputSrc inputSrc1=null, inputSrc2=null, inputSrc3=null;
			InboundLabelDataElement element=null;
			base.mInputs[0] = base.mInputs[1] = base.mInputs[2] = null;
			
			//Create input 1 (mandatory)
			inputSrc1 = new InboundLabelInputSrc(1, this.mFreightInfo.Input1Length, this.mFreightInfo.ValidationString, (short)this.mFreightInfo.ValidationBegin, "");
			element = createDataElement(inputSrc1, base.mLabelID, InboundLabelDataElement.DATAELEM_STORE, 1, this.mFreightInfo.StoreBeginPos, (short)(this.mFreightInfo.StoreEndPos - this.mFreightInfo.StoreBeginPos + 1), false, false, false);
			inputSrc1.AddDataElement(element);
			if(this.mFreightInfo.CartonInInput1 == "Y") {
				element = createDataElement(inputSrc1, base.mLabelID, InboundLabelDataElement.DATAELEM_CARTON, 1, this.mFreightInfo.CartonBeginPos,(short)(this.mFreightInfo.CartonEndPos - this.mFreightInfo.CartonBeginPos + 1), (this.mFreightInfo.CartonsNbrReq=="Y"), (this.mFreightInfo.DupCartonsOK=="Y"), (this.mFreightInfo.CartonValidation=="Y"));
				inputSrc1.AddDataElement(element);
			}
			if(this.mFreightInfo.POInputNumber == 1) {
				element = createDataElement(inputSrc1, base.mLabelID, InboundLabelDataElement.DATAELEM_PO, 1, (short)this.mFreightInfo.POStart, (short)this.mFreightInfo.POLength, false, false, false);
				inputSrc1.AddDataElement(element);
			}
			base.AddInputSrc(inputSrc1);
			
			//Create input 2
			if(this.mFreightInfo.Input2Length > 0) { 
				inputSrc2 = new InboundLabelInputSrc(2, this.mFreightInfo.Input2Length, "", 0, "");
				if(this.mFreightInfo.CartonInInput1 == "N") {
					element = createDataElement(inputSrc2, base.mLabelID, InboundLabelDataElement.DATAELEM_CARTON, 2, this.mFreightInfo.CartonBeginPos,(short)(this.mFreightInfo.CartonEndPos - this.mFreightInfo.CartonBeginPos + 1), false, false, false);
					inputSrc2.AddDataElement(element);
				}
				if(this.mFreightInfo.POInputNumber == 2) {
					element = createDataElement(inputSrc2, base.mLabelID, InboundLabelDataElement.DATAELEM_PO, 2, (short)this.mFreightInfo.POStart, (short)this.mFreightInfo.POLength, false, false, false);
					inputSrc2.AddDataElement(element);
				}
				base.AddInputSrc(inputSrc2);
			}
			
			//Create input 3
			if(this.mFreightInfo.Input3Length > 0) { 
				inputSrc3 = new InboundLabelInputSrc(3, this.mFreightInfo.Input3Length, "", 0, "");
				if(this.mFreightInfo.POInputNumber == 3) {
					element = createDataElement(inputSrc3, base.mLabelID, InboundLabelDataElement.DATAELEM_PO, 3, (short)this.mFreightInfo.POStart, (short)this.mFreightInfo.POLength, false, false, false);
					inputSrc3.AddDataElement(element);
				}
				base.AddInputSrc(inputSrc3);
			}
		}
		private InboundLabelDataElement createDataElement(InboundLabelInputSrc parent, int labelID, string type, short inputNumber, short start, short len, bool isValueRequired, bool isDuplicateAllowed, bool isCheckDigitValidation) {
			//
			InboundLabelDataElement element=null;
			try {
				InboundLabelDS label = new InboundLabelDS();
				InboundLabelDS.InboundLabelDataElementTableRow elem = label.InboundLabelDataElementTable.NewInboundLabelDataElementTableRow();
				elem.LabelID = labelID;
				elem.ElementType = type;
				elem.InputNumber = inputNumber;
				elem.Start = start;
				elem.Length = len;
				elem.IsValueRequired = (isValueRequired ? (byte)1 : (byte)0);
				elem.IsDuplicateAllowed = (isDuplicateAllowed ? (byte)1 : (byte)0);
				elem.IsCheckDigitValidation = (isCheckDigitValidation ? (byte)1 : (byte)0);
				elem.ConstantValue = "";
				label.InboundLabelDataElementTable.AddInboundLabelDataElementTableRow(elem);
				element = new InboundLabelDataElement(parent, label.InboundLabelDataElementTable[0], 0);
			}
			catch(Exception ex) { throw ex; }
			return element;
		}
		#endregion
	}
}
