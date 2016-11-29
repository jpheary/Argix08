//	File:	globals.cs
//	Author:	J. Heary
//	Date:	03/16/06
//	Desc:	Enumerators, event support, exceptions, etc.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Data;
using System.Reflection;

namespace Tsort {
    //Interfaces
    //Enumerators
    //Event delegates and args
    public delegate void LabelDataEventHandler(object source,LabelDataEventArgs e);
    public class LabelDataEventArgs:EventArgs {
        private string _element="";
        private string _data="";
        public LabelDataEventArgs(string element,string data) {
            //Constructor
            this._element = element;
            this._data = data;
        }
        public string Element { get { return this._element; } }
        public string Data { get { return this._data; } }
    }

    //Exceptions
    public class InboundLabelInvalidElementException:ApplicationException {
        public InboundLabelInvalidElementException() : base() { }
        public InboundLabelInvalidElementException(string message) : base(message) { }
        public InboundLabelInvalidElementException(string message,Exception innerException) : base(message,innerException) { }
    }
    public class InboundLabelValueException:ApplicationException {
        public InboundLabelValueException() : base() { }
        public InboundLabelValueException(string message) : base(message) { }
        public InboundLabelValueException(string message,Exception innerException) : base(message,innerException) { }
    }
    public class InboundLabelValidationException:ApplicationException {
        public InboundLabelValidationException() : base() { }
        public InboundLabelValidationException(string message) : base(message) { }
        public InboundLabelValidationException(string message,Exception innerException) : base(message,innerException) { }
    }
    public class InboundLabelValidationDefinitionException:ApplicationException {
        public InboundLabelValidationDefinitionException() : base() { }
        public InboundLabelValidationDefinitionException(string message) : base(message) { }
        public InboundLabelValidationDefinitionException(string message,Exception innerException) : base(message,innerException) { }
    }
}