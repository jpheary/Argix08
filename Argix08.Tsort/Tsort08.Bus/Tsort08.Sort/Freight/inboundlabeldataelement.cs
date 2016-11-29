//	File:	inboundlabeldataelement.cs
//	Author:	J. Heary
//	Date:	03/14/05
//	Desc:	Defines an inbound label data element which is a substring found
//			within an input source (barcode string).
//	Rev:	03/19/08 (jph)- added Trim() to mConstantValue usage.
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Tsort.Freight {
    //
    internal class InboundLabelDataElement {
        //Members
        private int mLabelID=0;
        private string mElementType="";
        private short mInputNumber=0;
        private short mStart=0;
        private short mLength=0;
        private byte mIsValueRequired=0;
        private byte mIsDuplicateAllowed=0;
        private byte mIsCheckDigitValidation=0;
        private string mConstantValue="";
        private string mValidationExp="";
        private string mCheckDigitType="";
        private int mPriority=0;
        private InboundLabelInputSrc mParent=null;

        //Constants
        //Events
        //Interface
        public InboundLabelDataElement(InboundLabelInputSrc parent,InboundLabelDS.InboundLabelDataElementTableRow element,int priority) {
            //Constructor: used for Returns freight label construction
            try {
                if(parent == null) throw new ApplicationException("Input Src cannot be null.");
                if(element == null) throw new ApplicationException("Data Element cannot be null.");
                if(parent.InputNumber != element.InputNumber) throw new ApplicationException("Input number of Input Src must match input number of Data Element.");
                this.mParent = parent;
                this.mLabelID = element.LabelID;
                this.mElementType = element.ElementType.Trim();
                this.mInputNumber = element.InputNumber;
                this.mStart = element.Start;
                this.mLength = element.Length;
                this.mIsValueRequired = element.IsValueRequired;
                this.mIsDuplicateAllowed = element.IsDuplicateAllowed;
                this.mIsCheckDigitValidation = element.IsCheckDigitValidation;
                this.mConstantValue = element.ConstantValue.Trim();
                this.mValidationExp = element.ValidationExp.Trim();
                this.mCheckDigitType = element.CheckDigitType.Trim();
                this.mPriority = priority;
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating Inbound Label Data Element instance.",ex); }
        }
        #region Accessors\Modifiers: LabelID, ElementType, InputNumber, IsValueRequired, IsDuplicateAllowed, IsCheckDigitValidation, Priority, ToDataSet()
        public int LabelID { get { return this.mLabelID; } }
        public string ElementType { get { return this.mElementType; } }
        public short InputNumber { get { return this.mInputNumber; } }
        public bool IsValueRequired { get { return (this.mIsValueRequired == 1); } }
        public bool IsDuplicateAllowed { get { return (this.mIsDuplicateAllowed == 1); } }
        public bool IsCheckDigitValidation { get { return (this.mIsCheckDigitValidation == 1); } }
        public int Priority { get { return this.mPriority; } }
        public DataSet ToDataSet() {
            //Return a dataset containing values for this object
            InboundLabelDS ds=null;
            try {
                ds = new InboundLabelDS();
                InboundLabelDS.InboundLabelDataElementTableRow element = ds.InboundLabelDataElementTable.NewInboundLabelDataElementTableRow();
                element.LabelID = this.mLabelID;
                element.ElementType = this.mElementType;
                element.InputNumber = this.mInputNumber;
                element.Start = this.mStart;
                element.Length = this.mLength;
                element.IsValueRequired = this.mIsValueRequired;
                element.IsDuplicateAllowed = this.mIsDuplicateAllowed;
                element.IsCheckDigitValidation = this.mIsCheckDigitValidation;
                element.ConstantValue = this.mConstantValue.Trim();
                element.ValidationExp = this.mValidationExp.Trim();
                element.CheckDigitType = this.mCheckDigitType.Trim();
                ds.InboundLabelDataElementTable.AddInboundLabelDataElementTableRow(element);
            }
            catch(Exception ex) { Debug.Write(ex.ToString() + "\n"); }
            return ds;
        }
        #endregion
        public string GetValue() {
            //Return the string value of this data element. Value can be a constant or a 
            //substring of the parent input src.
            ValidateData();
            string val = getValue().Trim();
            if(val.Length == 0 && this.mIsValueRequired == 1)
                throw new InboundLabelValidationException("Could not get required inbound label " + this.mElementType + " element value.");
            return val;
        }
        public void ValidateDefinition() {
            //Validate start, length, and data element within parent input length
            if(this.mConstantValue.Trim().Length == 0) {
                if(this.mStart == 0)
                    throw new InboundLabelValidationDefinitionException("Inbound label " + this.ElementType + " element starting position must be greater than zero.");
                if(this.mLength == 0)
                    throw new InboundLabelValidationDefinitionException("Inbound label " + this.ElementType + " element length must be greater than zero.");
                if(this.mStart + this.mLength - 1 > this.mParent.Length)
                    throw new InboundLabelValidationDefinitionException("Inbound label " + this.ElementType + " element element start and length invalid within input source.");
            }
        }
        public void ValidateData() {
            //Validate the data element within the parent input data string
            if(this.mValidationExp.Trim().Length > 0) {
                //Implement expression validation
                Regex rx = new Regex(this.mValidationExp.Trim());
                if(rx.IsMatch(getValue()))
                    throw new InboundLabelValidationException("Inbound label " + this.mElementType + " element fails expression validation.");
            }
            if(this.mCheckDigitType.Trim().Length > 0) {
                //Implement check digit validation
                switch(this.mCheckDigitType.Trim()) {
                    case "Mod10":
                        if(!Enterprise.Helper.ValidateMod10CheckDigit(getValue()))
                            throw new InboundLabelValidationException("Inbound label " + this.mElementType + " element fails Mod10 validation.");
                        break;
                    default:
                        throw new InboundLabelValidationException("Inbound label " + this.mElementType + " specifies unknown (" + this.mCheckDigitType.Trim() + ") check digit validation.");
                }
            }
        }
        public InboundLabelDataElement Copy() {
            //Return a copy of this object
            InboundLabelDataElement element=null;
            try {
                InboundLabelDS.InboundLabelDataElementTableRow elem = ((InboundLabelDS)this.ToDataSet()).InboundLabelDataElementTable[0];
                element = new InboundLabelDataElement(this.mParent,elem,this.mPriority);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating a copy of an Inbound Label Data Element.",ex); }
            return element;
        }
        #region Local Services: getValue()
        private string getValue() {
            string val ="";
            if(this.mConstantValue.Trim().Length > 0)
                val = this.mConstantValue.Trim();
            else if(this.mParent.DataIsAvailable)
                val = this.mParent.InputData.Substring(this.mStart - 1,this.mLength);
            else
                throw new InboundLabelValueException("Data for inbound label " + this.mElementType + " element was not available in the input source.");
            return val;
        }
        #endregion
    }
}