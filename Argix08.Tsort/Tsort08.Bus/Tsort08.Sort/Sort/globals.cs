//	File:	globals.cs
//	Author:	J. Heary
//	Date:	04/27/07
//	Desc:	Global (library-wide) definitions.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using Tsort.Sort;

namespace Tsort {
    //Interfaces
    //Enumerators
    //Event delegates and args
    public delegate void SortedItemEventHandler(object sender,SortedItemEventArgs e);
    public class SortedItemEventArgs:EventArgs {
        private SortedItem _sortedItem=null;
        public SortedItemEventArgs(SortedItem sortedItem) { this._sortedItem = sortedItem; }
        public SortedItem SortedItem { get { return this._sortedItem; } }
    }

    //Custom Exceptions
    public class NoAssignmentException:ApplicationException {
        public NoAssignmentException() : base("No assignments on station") { }
    }
    public class InboundLabelException:ApplicationException {
        public InboundLabelException(Exception ex) : base("Inbound label exception",ex) { }
    }
    public class HaveNoIdeaWhatItIsException:ApplicationException {
        public HaveNoIdeaWhatItIsException(Exception ex) : base("Unhandled exception",ex) { }
    }
    public class ZeroWeightException:ApplicationException {
        public ZeroWeightException() : base("Zero weight is not valid") { }
    }
    public class OverWeightException:ApplicationException {
        public OverWeightException(decimal weight) : base(weight.ToString() + " weight is not valid (exceeds maximum allowed (" + SortedItem.WeightMax.ToString() + ")") { }
    }
    public class DuplicateCartonException:ApplicationException {
        public DuplicateCartonException() : base("Duplicate carton number") { }
    }
    public class ClientForSanException:ApplicationException {
        public ClientForSanException() : base("Can not get client for san number") { }
    }
    public class DestinationRoutingException:ApplicationException {
        public DestinationRoutingException(Exception ex) : base("Can not get destination and routing info",ex) { }
    }
    public class OutboundLabelException:ApplicationException {
        public OutboundLabelException(Exception ex) : base("Error generating outbound label",ex) { }
    }
    public class ErrorLabelException:ApplicationException {
        public ErrorLabelException(Exception ex) : base("Error generating outbound label",ex) { }
    }
    public class SavingException:ApplicationException {
        public SavingException(string msg) : base(msg) { }
        public SavingException(string msg,Exception ex) : base(msg,ex) { }
    }
}
