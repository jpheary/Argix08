//	File:	sorteditems.cs
//	Author:	M. Khasin
//	Date:	05/00/07
//	Desc:	Collection class of sorted items.
//			This class uses label sequence number for sort key and ASSUMES
//			SortedList does not change item order (i.e. FIFO); this assumption  
//			impacts correct operation of method IsDuplicateCarton().
//	Rev:	02/18/08 (jph)- corrected IsDuplicateCarton() method.
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Diagnostics;
using Argix;

namespace Tsort.Sort {
    //
    internal class SortedItems {
        //Members
        public static int MaxItems=256;
        public static int DuplicateCheckCount=10;
        private SortedList mSortedList=null;

        //Constants
        //Events
        //Interface
        public SortedItems() {
            //Constructor
            try {
                //Create objects
                this.mSortedList = new SortedList();
                this.mSortedList.Capacity = MaxItems + 1;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new SortedItems instance.",ex); }
        }
        #region Collection Services: Add(), Item(), Remove()
        public void Add(SortedItem sortedItem) {
            //Add a new sorted item to the collection
            try {
                //Manage collection size; then add new item
                if(this.mSortedList.Count == MaxItems) {
                    this.mSortedList.RemoveAt(0);
                    ArgixTrace.WriteLine(new TraceMessage("SORTED ITEMS PURGE: " + this.mSortedList.Count.ToString() + "...",AppLib.EVENTLOGNAME,LogLevel.Debug,"SortedItems"));
                }
                this.mSortedList.Add(sortedItem.LabelSeqNumber,sortedItem);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while adding new sorted item to the collection.",ex); }
        }
        public int Count { get { return this.mSortedList.Count; } }
        public SortedItem Item(string labelSeqNumber) {
            //Get an existing sorted item from the collection
            SortedItem item=null;
            try {
                item = (SortedItem)this.mSortedList[labelSeqNumber];
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while getting sorted item from the collection.",ex); }
            return item;
        }
        public void Remove(string labelSeqNumber) {
            //Remove an existing sorted item from the collection
            try {
                this.mSortedList.Remove(labelSeqNumber);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while removing sorted item from the collection.",ex); }
        }
        #endregion
        public bool IsDuplicateCarton(SortedItem sortedItem) {
            //Check last DuplicateCheckCount number of Cartons for duplicate, ignore error cartons
            //			int i=0, cartonsToCheck=DuplicateCheckCount;
            bool isDuplicate=false;
            try {
                //Validate if this carton is not already in an error condition
                if(!sortedItem.IsError()) {
                    //while((i++ < cartonsToCheck) && !isDuplicate && (this.mSortedList.Count - i > 0)) {
                    //	SortedItem item = (SortedItem)this.mSortedList[this.mSortedList.Count - i];
                    //	if(item.IsError()) 
                    //		cartonsToCheck++;
                    //	else
                    //		isDuplicate = sortedItem.IsDuplicateCarton(item);
                    //}

                    //Work backwards through the list starting with previous sorted item (last in the list is the current sorted item)
                    if(this.mSortedList.Count > 1) {
                        //At least 2 sorted items exist; determine how many to check
                        int itemsToCheck = (DuplicateCheckCount < this.mSortedList.Count) ? DuplicateCheckCount : this.mSortedList.Count-1;
                        for(int j=1;j<=itemsToCheck;j++) {
                            //Get a previous sorted item and check as duplicate
                            SortedItem item = (SortedItem)this.mSortedList.GetByIndex(this.mSortedList.Count-j-1);
                            if(!item.IsError()) isDuplicate = sortedItem.IsDuplicateCarton(item);
                            if(isDuplicate) break;
                        }
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while checking for duplicate carton in the collection.",ex); }
            return isDuplicate;
        }
    }
}
