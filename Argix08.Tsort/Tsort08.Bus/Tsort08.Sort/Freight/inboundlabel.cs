//	File:	inboundlabel.cs
//	Author:	J. Heary
//	Date:	02/07/05
//	Desc:	Represents an inbound label. This is the new label design, used for Returns freight.
//			Responsible for validating label definition (at creation), parsing data, and data validation.
//			There are two ways to get data:
//			1.	Query: provides interface to query data via GetElementValue method.
//			2.	Events: raises events concerning when/what data is received.
//			Label can have any number of inputs with any number of data elements.
//	Rev:	02/18/08 (jph)- corrected cast exception in IsDuplicateElementAllowed();
//			changed default return to false;
//	---------------------------------------------------------------------------
using System;
using System.Configuration;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace Tsort.Freight {
    //
    internal class InboundLabel {
        //Members
        protected int mLabelID=0;
        protected int mSortTypeID=0;
        protected string mDescription="";
        protected InboundLabelInputSrc[] mInputs=null;
        protected int mCurrentInput=1;
        protected Hashtable mElements=null;

        //Constants 
        //Events
        public event LabelDataEventHandler HasDataChanged=null;
        public event LabelDataEventHandler InputReceived=null;
        public event EventHandler AllInputsReceived=null;

        //Interface
        public InboundLabel() : this(null) { }
        public InboundLabel(InboundLabelDS label) {
            //Constructor
            try {
                this.mElements = new Hashtable();
                this.mCurrentInput = 1;
                this.mInputs = new InboundLabelInputSrc[3] { null,null,null };
                if(label != null) {
                    if(label.InboundLabelTable.Rows.Count > 0) {
                        //Init label properties
                        this.mLabelID = label.InboundLabelTable[0].LabelID;
                        this.mSortTypeID = label.InboundLabelTable[0].SortTypeID;
                        if(!label.InboundLabelTable[0].IsDescriptionNull()) this.mDescription = label.InboundLabelTable[0].Description;

                        //Create label composition: add input src's as specified
                        AddInputSrc(new InboundLabelInputSrc(1,label,"Input1"));
                        if(!label.InboundLabelTable[0].IsInput2LenNull() && label.InboundLabelTable[0].Input2Len > 0) AddInputSrc(new InboundLabelInputSrc(2,label,"Input2"));
                        if(!label.InboundLabelTable[0].IsInput3LenNull() && label.InboundLabelTable[0].Input3Len > 0) AddInputSrc(new InboundLabelInputSrc(3,label,"Input3"));

                        //Validate label
                        ValidateDefinition();
                    }
                }
            }
            catch(InboundLabelValidationDefinitionException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating new Inbound Label instance.",ex); }
        }
        public InboundLabel Copy() {
            //Return a copy of this object
            InboundLabel input=null;
            try {
                InboundLabelDS label = (InboundLabelDS)this.ToDataSet();
                input = new InboundLabel(label);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating a copy of an Inbound Label.",ex); }
            return input;
        }
        #region Accessors\Modifiers: LabelID, SortTypeID, Description, Inputs, CurrentInputNumber, ToDataSet()
        public int LabelID { get { return this.mLabelID; } }
        public int SortTypeID { get { return this.mSortTypeID; } }
        public string Description { get { return this.mDescription; } set { this.mDescription = value; } }
        public InboundLabelInputSrc[] Inputs { get { return this.mInputs; } }
        public int CurrentInputNumber { get { return this.mCurrentInput; } }
        //public Hashtable DataElements { get { return this.mElements; } }
        public virtual DataSet ToDataSet() {
            //Return a dataset containing values for this inbound label
            InboundLabelDS ds=null;
            try {
                ds = new InboundLabelDS();
                InboundLabelDS.InboundLabelTableRow label = ds.InboundLabelTable.NewInboundLabelTableRow();
                label.LabelID = this.mLabelID;
                label.SortTypeID = this.mSortTypeID;
                label.Description = this.mDescription;
                label.Input1Len = this.mInputs[0].Length;
                label.Input1ValidString = this.mInputs[0].ValidationString;
                label.Input1ValidStart = this.mInputs[0].ValidationStart;
                if(this.mInputs[1] != null) {
                    label.Input2Len = this.mInputs[1].Length;
                    label.Input2ValidString = this.mInputs[1].ValidationString;
                    label.Input2ValidStart = this.mInputs[1].ValidationStart;
                }
                if(this.mInputs[2] != null) {
                    label.Input3Len = this.mInputs[2].Length;
                    label.Input3ValidString = this.mInputs[2].ValidationString;
                    label.Input3ValidStart = this.mInputs[2].ValidationStart;
                }
                //label.IsActive = 1;
                ds.InboundLabelTable.AddInboundLabelTableRow(label);
                for(int i=0;i<this.mInputs.Length;i++) {
                    if(this.mInputs[i] != null)
                        ds.Merge(this.mInputs[i].ToDataSet());
                }
            }
            catch(Exception ex) { Debug.Write(ex.ToString() + "\n"); }
            return ds;
        }
        #endregion
        #region Label Composition: AddInputSrc(), InputSrcsCount(), InputSrcItem(), AddDataElement()
        public void AddInputSrc(InboundLabelInputSrc inputSrc) {
            //Add an input src definition to this label.
            //NOTE : Need to think about calling Validate Definition here to verify
            //		that input number corresponds to the order in the linked array
            if(this.mInputs[inputSrc.InputNumber-1] != null)
                throw new ApplicationException("There is already an input defined for input position  " + inputSrc.InputNumber);
            this.mInputs[inputSrc.InputNumber-1] = inputSrc;
            inputSrc.DataElementValidated += new LabelDataEventHandler(this.OnDataElementValdated);
        }
        public int InputSrcsCount {
            get {
                //Count non-null inputs
                int i=0;
                for(i=0;i<this.mInputs.Length;i++)
                    if(this.mInputs[i] == null) break;
                return i;
            }
        }
        public InboundLabelInputSrc InputSrcItem(int inputNumber) {
            //Get the input src with the specified input number from this label
            InboundLabelInputSrc input=null;
            for(int i=0;i<this.mInputs.Length;i++) {
                if(this.mInputs[i].InputNumber == inputNumber) {
                    input = this.mInputs[i];
                    break;
                }
            }
            return input;
        }
        public void RemoveInputSrc(InboundLabelInputSrc inputSrc) {
            //Remove an input src definition from this label
            this.mInputs[inputSrc.InputNumber-1] = null;
        }
        #endregion
        public void ClearData() {
            //Clears data in inputs srcs; MUST call this method before processing a new carton.
            this.mCurrentInput = 1;
            this.mElements.Clear();
            foreach(InboundLabelInputSrc input in this.mInputs) {
                if(input != null) input.ClearData();
            }
        }
        public string GetInputName(int inputNumber) {
            //Return input name for the specified input number (if applicable)
            if(inputNumber > 0 && inputNumber <= this.mInputs.Length)
                return ((InboundLabelInputSrc)this.mInputs[inputNumber - 1]).Name;
            else
                return "";
        }
        public int GetInputLength(int inputNumber) {
            //Return input length for the specified input number (if applicable)
            if(inputNumber > 0 && inputNumber <= this.mInputs.Length)
                return ((InboundLabelInputSrc)this.mInputs[inputNumber - 1]).Length;
            else
                return 0;
        }
        public void ValidateDefinition() {
            //Validates definition of the label, inputs, and elements
            foreach(InboundLabelInputSrc input in this.mInputs)
                if(input != null) input.ValidateDefinition();
        }
        public void NextInputData(string inputData) {
            //Parses out data from input based on data element definitions
            //021508 (joh)- changed return from Hashtable to void
            if(this.mInputs[this.mCurrentInput - 1] != null) {
                if(this.InputReceived != null) this.InputReceived(this,new LabelDataEventArgs(this.mInputs[this.mCurrentInput - 1].InputNumber.ToString(),inputData));
                this.mInputs[this.mCurrentInput - 1].GetValues(inputData);
                determineNextInputNumber();
            }
            //return this.mElements;
        }
        public bool AllInputsProcessed { get { return this.mCurrentInput > InputSrcsCount; } }
        public string GetElementValue(string elementName) {
            if(this.mElements.Count == 0)
                throw new ApplicationException("The inbound label (#" + this.mLabelID.ToString() + ") does not have any data elements associated with it.");
            string elementValue = this.mElements.ContainsKey(elementName) ? (string)this.mElements[elementName] : "";
            return elementValue;
        }
        public bool IsDuplicateElementAllowed(string elementName) {
            bool isAllowed=false;
            try {
                InboundLabelDataElement element=null;
                foreach(InboundLabelInputSrc input in this.mInputs) {
                    if(input != null) {
                        //Find the data element in this input
                        InboundLabelDataElement elem = input.DataElementItem(elementName);
                        if(elem != null) {
                            element = elem;
                            break;
                        }
                    }
                }
                if(element != null) isAllowed = element.IsDuplicateAllowed;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception in IsDuplicateElementAllowed().",ex); }
            return isAllowed;
        }
        public void DetermineInputs(string[] inputs) {
            //Pick and use all values for label inputs from the inputs array
            ArrayList selectedInputs = new ArrayList();

            //Clear (reset) the label; then map inputs to inbound label inputs
            ClearData();
            while(!AllInputsProcessed) {
                //Determine an input value that meets validation requirements for the current input
                string input="";
                try {
                    input = DetermineInputValue(this.mCurrentInput,inputs);
                }
                catch(ApplicationException ex) { throw ex; }
                catch(Exception ex) { throw new ApplicationException("Unexpected exception while determining input value.",ex); }
                if(input == "")
                    throw new ApplicationException("No data for inbound label input #" + this.mCurrentInput.ToString() + ".");
                if(selectedInputs.Contains(input))
                    throw new ApplicationException("Inbound label input values can be used in multiple label inputs.");
                selectedInputs.Add(input);

                //Process the current input
                NextInputData(input);
            }
        }
        public string DetermineInputValue(int inputNumber,string[] inputs) {
            //Return a value from inputs that can be used as valid data for the specified label input
            //Throw an error if more that one string can be used
            string match="";
            InboundLabelValidationException ilex=null;
            foreach(string inputData in inputs) {
                //Determine inputData as valid for label input #inputNumber
                bool valid=false;
                try {
                    valid = (this.mInputs[inputNumber-1] != null && this.mInputs[inputNumber-1].IsValidData(inputData));
                }
                catch(InboundLabelValidationException ex) { valid = false; ilex = ex; }
                catch(ApplicationException) { valid = false; }
                catch(Exception ex) { throw new ApplicationException("Unexpected exception while determining input data [" + inputData + "] as valid for label input #" + inputNumber + ".",ex); }
                if(valid) {
                    if(match.Length > 0)
                        throw new ApplicationException("Multiple input data valid for the same label input [#" + inputNumber + "].");
                    else
                        match = inputData;
                }
            }
            if(match.Length == 0 && ilex != null) throw ilex;
            return match;
        }
        private void OnDataElementValdated(object sender,LabelDataEventArgs e) {
            //Event handler for data element validated
            this.mElements.Add(e.Element,e.Data);
            if(this.HasDataChanged != null) this.HasDataChanged(this,new LabelDataEventArgs(e.Element,e.Data));
        }
        #region Local Services: determineNextInputNumber()
        private void determineNextInputNumber() {
            //Determines next input number; used after input is processed, or set as not needed
            this.mCurrentInput += 1;
            if(AllInputsProcessed) {
                //Notify clients that all input are processed
                if(this.AllInputsReceived != null) this.AllInputsReceived(this,EventArgs.Empty);
            }
            else {
                //Recurse
                if(!this.mInputs[this.mCurrentInput - 1].IsNeeded)
                    determineNextInputNumber();
            }
        }
        #endregion
    }
}
