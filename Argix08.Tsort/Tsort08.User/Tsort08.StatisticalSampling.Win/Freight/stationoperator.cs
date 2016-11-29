using System;
using System.Data;
using Argix.Data;

namespace Argix.Freight {
	//
	public class StationOperator {	
		//Members
		private Carton mCarton=null;
		
		public event EventHandler CartonNumberValidated=null, CartonNumberRequired=null;
		public event EventHandler BooksCounted=null, BookScanned=null;
		public event EventHandler ValidBookSample=null, InvalidBookSample=null;
		public event EventHandler CartonSaved=null, ResetCarton=null;
		
		//Interface
		public StationOperator() {
			//Constructor
			try {
				//Initialize
				Reset();
			}
            catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new StationOperator instance.", ex); }
		}
		public Carton Carton { get { return this.mCarton; } }
		public void Reset() {
			//Create a new empty carton
			try {
				this.mCarton = null;
			}
			catch(Exception ex) { throw new ApplicationException("Failed to clear the current carton sample.", ex); }
			finally { if(this.ResetCarton != null) this.ResetCarton(this, EventArgs.Empty); }
		}
        public void ValidateCartonNumber(string cartonNumber,string shipperNumber) {
			//Validate a carton number exists for this book
			try {
                //Validate carton input
                if(cartonNumber.Length == 0) 
                    if(this.CartonNumberRequired != null) this.CartonNumberRequired(this,EventArgs.Empty);
                
                if(cartonNumber.Length > 0 && shipperNumber.Length > 0) {
					//Validate unique carton number for the specified vendor
                    StatSampleDS sample = new StatSampleDS();
                    sample.Merge(App.Mediator.FillDataset(App.USP_CARTON_READ,App.TBL_CARTON_READ,new object[] { cartonNumber,shipperNumber }));
					if(sample.HeaderTable.Rows.Count > 0)
                        throw new WorkflowException("Carton# " + cartonNumber + " is a duplicate for vendor#" + shipperNumber + ".");
					else {
                        this.mCarton = new Carton(cartonNumber,shipperNumber);
                        if(this.CartonNumberValidated != null) this.CartonNumberValidated(this,EventArgs.Empty);
					}
				}
			}
			catch(ApplicationException ex) { throw ex; }
			catch(WorkflowException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error while validating carton number.", ex); }
		}
		public void EnterBookCount(int count) {
			//Process book count
			try {
                if(this.mCarton == null)
                    return;
				this.mCarton.ExpectedBookCount = count;
				if(this.BooksCounted != null) this.BooksCounted(this, EventArgs.Empty);
			}
			catch(ApplicationException ex) { throw ex; }
			catch(WorkflowException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error when entering expected book count.", ex); }
		}
        public void ProcessBook(string labelScan,bool damaged) {
			//Process a book scan
			try {
                if(this.mCarton == null)
                    return;
                this.mCarton.AddBook(labelScan,damaged);
				if(this.BookScanned != null) this.BookScanned(this, EventArgs.Empty);
			}
			catch(ApplicationException ex) { throw ex; }
			catch(WorkflowException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error when processing book with label# " + labelScan + ".", ex); }
		}
		public bool CanSaveCarton { get { return (this.mCarton != null ? (this.mCarton.ExpectedBookCount >= 0 && this.mCarton.BooksCount >= 0) : false); } }
		public void ValidateCartonSample() {
			//Validate current carton sample
			try {
                if(this.mCarton == null)
                    return;
                if(this.mCarton.ValidateBookCount()) {
					if(this.ValidBookSample != null) this.ValidBookSample(this, EventArgs.Empty);
				}
			}
			catch(ApplicationException ex) { throw ex; }
			catch(WorkflowException ex) {
				this.mCarton.ClearBooks();
				if(this.InvalidBookSample != null) this.InvalidBookSample(this, EventArgs.Empty);
				throw ex;
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error when validating the current carton sample.", ex); }
		}
		public void SaveCartonSample() {
			//Save current carton sample
			try {
                if(this.mCarton == null)
                    return;
                this.mCarton.Save();
				if(this.CartonSaved != null) this.CartonSaved(this, EventArgs.Empty);
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Failed to save the current carton sample.", ex); }
		}
	}
}
