//	File:	inlabeldataelementprioritycompare.cs
//	Author:	J. Heary
//	Date:	03/14/05
//	Desc:	.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Text;

namespace Tsort.Freight {
    //
    internal class InLabelDataElementPriorityCompare:IComparer {
        //Members

        //IComparer implementations
        int IComparer.Compare(object element1,object element2) {
            //Compare two objects
            int diff=0;
            try {
                //Cast objects as InboundLabelDataElement types
                InboundLabelDataElement elem1 = (InboundLabelDataElement)element1;
                InboundLabelDataElement elem2 = (InboundLabelDataElement)element2;

                //Compare priority levels
                diff = elem1.Priority - elem2.Priority;
            }
            catch(Exception ex) { throw ex; }
            return diff;
        }
    }
}
