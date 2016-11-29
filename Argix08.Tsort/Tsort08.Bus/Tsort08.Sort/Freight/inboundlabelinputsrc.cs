//	File:	inboundlabelinputsrc.cs
//	Author:	J. Heary
//	Date:	03/14/05
//	Desc:	Class represents inbound label input source. Responsible for validating 
//			label input at creation, parsing data, and data validation.
//			NOTES:
//			1. Inbound label input src can have any number of data elements.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace Tsort.Freight {
    /// <summary> Class represents inbound label input source. Responsible for validating label input at creation, parsing data, and data validation. </summary>
    internal class InboundLabelInputSrc {
        //Members
        private int mLabelID=0;
        private int mInputNumber=1;
        private short mLength=0;
        private string mValidString="";
        private short mValidStart=0;
        private ArrayList mDataElements=null;
        private string mData="";
        private bool mIsNeeded=true;
        private string mName="";

        //Constants
        //Events
        public event LabelDataEventHandler DataElementValidated=null;

        //Interface
        public InboundLabelInputSrc(int inputNumber,InboundLabelDS label,string name) {
            //Constructor: used for Returns freight
            try {
                //Define input number for this src
                this.mInputNumber = inputNumber;
                if(label != null && label.InboundLabelTable.Rows.Count > 0) {
                    //Init src properties 
                    this.mLabelID = label.InboundLabelTable[0].LabelID;
                    switch(this.mInputNumber) {
                        case 1:
                            this.mLength = label.InboundLabelTable[0].Input1Len;
                            if(!label.InboundLabelTable[0].IsInput1ValidStringNull()) this.mValidString = label.InboundLabelTable[0].Input1ValidString.Trim();
                            this.mValidStart = label.InboundLabelTable[0].Input1ValidStart;
                            break;
                        case 2:
                            if(!label.InboundLabelTable[0].IsInput2LenNull()) this.mLength = label.InboundLabelTable[0].Input2Len;
                            if(!label.InboundLabelTable[0].IsInput2ValidStringNull()) this.mValidString = label.InboundLabelTable[0].Input2ValidString.Trim();
                            if(!label.InboundLabelTable[0].IsInput2ValidStartNull()) this.mValidStart = label.InboundLabelTable[0].Input2ValidStart;
                            break;
                        case 3:
                            if(!label.InboundLabelTable[0].IsInput3LenNull()) this.mLength = label.InboundLabelTable[0].Input3Len;
                            if(!label.InboundLabelTable[0].IsInput3ValidStringNull()) this.mValidString = label.InboundLabelTable[0].Input3ValidString.Trim();
                            if(!label.InboundLabelTable[0].IsInput3ValidStartNull()) this.mValidStart = label.InboundLabelTable[0].Input3ValidStart;
                            break;
                    }
                    this.mDataElements = new ArrayList();
                    this.mName = name;

                    //Attach data elements for this input source
                    if(label.InboundLabelDataElementTable.Rows.Count > 0) {
                        InboundLabelDS elementDS = new InboundLabelDS();
                        elementDS.Merge(label.InboundLabelDataElementTable.Select("InputNumber=" + this.mInputNumber));
                        if(elementDS.InboundLabelDataElementTable.Rows.Count > 0) {
                            for(int i=0;i<elementDS.InboundLabelDataElementTable.Rows.Count;i++) {
                                InboundLabelDataElement element = new InboundLabelDataElement(this,elementDS.InboundLabelDataElementTable[i],0);
                                this.mDataElements.Add(element);
                            }
                        }
                    }
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating Inbound Label Input Src instance.",ex); }
        }
        #region Accessors\Modifiers: InputNumber, Length, InputData, IsNeeded, Name, ToDataSet()
        public int InputNumber { get { return this.mInputNumber; } }
        public short Length { get { return this.mLength; } }
        public string ValidationString { get { return this.mValidString; } }
        public short ValidationStart { get { return this.mValidStart; } }
        public string InputData { get { return this.mData; } }
        public bool IsNeeded { get { return this.mIsNeeded; } set { this.mIsNeeded = value; } }
        public string Name {
            get {
                //If not given a name, create one from the data element types
                string name = this.mName;
                if(name == "") {
                    foreach(InboundLabelDataElement element in this.mDataElements)
                        name += element.ElementType + " ";
                }
                return name + "(" + this.mLength + ")";
            }
        }
        public DataSet ToDataSet() {
            //Return a dataset containing values for this object
            InboundLabelDS input=null;
            try {
                input = new InboundLabelDS();
                InboundLabelDS.InboundLabelTableRow row = input.InboundLabelTable.NewInboundLabelTableRow();
                row.LabelID = this.mLabelID;
                row.SortTypeID = 0;
                row.Input1Len = 0;
                row.Input1ValidStart = 0;
                switch(this.mInputNumber) {
                    case 1:
                        row.Input1Len = this.mLength;
                        if(this.mValidString.Length > 0) row.Input1ValidString = this.mValidString;
                        row.Input1ValidStart = this.mValidStart;
                        break;
                    case 2:
                        row.Input2Len = this.mLength;
                        if(this.mValidString.Length > 0) row.Input2ValidString = this.mValidString;
                        row.Input2ValidStart = this.mValidStart;
                        break;
                    case 3:
                        row.Input3Len = this.mLength;
                        if(this.mValidString.Length > 0) row.Input3ValidString = this.mValidString;
                        row.Input3ValidStart = this.mValidStart;
                        break;
                }
                input.InboundLabelTable.AddInboundLabelTableRow(row);

                InboundLabelDataElement element=null;
                for(int i=0;i<this.mDataElements.Count;i++) {
                    element = (InboundLabelDataElement)this.mDataElements[i];
                    input.Merge(element.ToDataSet());
                }
            }
            catch(Exception ex) { Debug.Write(ex.ToString() + "\n"); }
            return input;
        }
        #endregion
        #region Input Source Composition: AddDataElement(), DataElementsCount(), DataElementItem(), RemoveDataElement()
        public void AddDataElement(InboundLabelDataElement element) {
            //Add a new data element to the collection
            this.mDataElements.Add(element);
        }
        public int DataElementsCount { get { return this.mDataElements.Count; } }
        public InboundLabelDataElement DataElementItem(string elementType) {
            //Get a data element item from the collection by name
            InboundLabelDataElement element=null;
            for(int i=0;i<this.mDataElements.Count;i++) {
                InboundLabelDataElement _element = (InboundLabelDataElement)this.mDataElements[i];
                if(_element.ElementType == elementType) {
                    element = _element;
                    break;
                }
            }
            return element;
        }
        public InboundLabelDataElement DataElementItem(int index) {
            //Get a data element item from the collection by index
            return (InboundLabelDataElement)this.mDataElements[index];
        }
        public void RemoveDataElement(InboundLabelDataElement element) {
            //Remove an existing data element from the collection
            this.mDataElements.Remove(element);
        }
        #endregion
        public void ClearData() {
            this.mData = "";
            this.mIsNeeded = true;
        }
        public bool DataIsAvailable { get { return this.mData.Length == this.mLength; } }
        public bool IsValidData(string inputData) {
            //Determine if inputData is valid for this input
            bool isValid = true;
            string priorData=this.mData;
            try {
                //Ensure data is available
                this.mData = inputData.Trim();
                if(DataIsAvailable) {
                    //Validate the data for this input (i.e. validation string, etc)
                    validateData();
                    if(this.mIsNeeded) {
                        //Perform input data validation on each element (i.e. expression validation, check digit validation)
                        foreach(InboundLabelDataElement element in this.mDataElements)
                            element.ValidateData();
                    }
                }
                else
                    throw new ApplicationException("Inbound label input source " + this.mInputNumber.ToString() + " data unavailable.");
            }
            catch(InboundLabelValidationException ex) { throw ex; }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception) { throw new InboundLabelValidationException("Unexpected exception while validating input data [" + inputData + "] for inbound label input source " + this.mInputNumber.ToString() + "."); }
            finally { this.mData = priorData; }
            return isValid;
        }
        public void GetValues(string inputData) {
            //Ensure data is available
            this.mData = inputData;
            if(DataIsAvailable) {
                //Validate the input data
                validateData();
                if(this.mIsNeeded) {
                    //Sort elements in order of priority and raise event notifications for data received
                    this.mDataElements.Sort(new InLabelDataElementPriorityCompare());
                    foreach(InboundLabelDataElement element in this.mDataElements) {
                        string elemType = element.ElementType;
                        string elemValue = element.GetValue();
                        if(this.DataElementValidated != null) this.DataElementValidated(this,new LabelDataEventArgs(elemType,elemValue));
                    }
                }
            }
            else
                throw new InboundLabelValidationException("Inbound label input source " + this.mInputNumber.ToString() + " data unavailable.");
        }
        public void ValidateDefinition() {
            //Validate definition length and validation string
            if(this.mLength < 1)
                throw new InboundLabelValidationDefinitionException("Inbound label input source " + this.mInputNumber.ToString() + " source length must be greater than 0.");
            if(this.mValidString.Length > 0) {
                //Validate definition for validation string
                if(this.mValidStart < 1)
                    throw new InboundLabelValidationDefinitionException("Inbound label input source " + this.mInputNumber.ToString() + "source validation string must be greater than 0.");
                if((this.mValidStart + this.mValidString.Length - 1) > this.mLength)
                    throw new InboundLabelValidationDefinitionException("Inbound label input source " + this.mInputNumber.ToString() + "source validation string start and length invalid within input source.");
            }
            else {
                if(this.mValidStart > 0)
                    throw new InboundLabelValidationDefinitionException("Inbound label input source " + this.mInputNumber.ToString() + "source invalid validation string start.");
            }
            //Validate definition for each data elements
            foreach(InboundLabelDataElement element in this.mDataElements)
                element.ValidateDefinition();
        }
        public InboundLabelInputSrc Copy() {
            //Return a copy of this object
            InboundLabelInputSrc input=null;
            try {
                InboundLabelDS label = (InboundLabelDS)this.ToDataSet();
                input = new InboundLabelInputSrc(this.mInputNumber,label,this.mName);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating a copy of an Inbound Label Input Source.",ex); }
            return input;
        }
        #region Local Services: validateData()
        private void validateData() {
            //Validate the input data using the validation string (if required)
            if(this.mValidString.Length > 0) {
                if(this.mData.Substring(this.mValidStart - 1,this.mValidString.Length).CompareTo(this.mValidString) != 0)
                    throw new InboundLabelValidationException("Inbound label input source " + this.mInputNumber.ToString() + " validation string failed.");
            }
        }
        #endregion
    }
}
