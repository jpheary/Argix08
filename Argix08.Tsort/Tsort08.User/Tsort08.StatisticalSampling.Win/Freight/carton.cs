using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using Argix.Data;

namespace Argix.Freight {
	//
	public class Carton {
		//Members		
		private string mNumber="", mShipperNumber = "";
		private int mExpectedBookCount=0;
		private Hashtable mBooks=null, mDamagedBooks=null;

		//Interface
        public Carton(string number,string shipperNumber) {
			//Constructor
			try {
                this.mNumber = number;
                this.mShipperNumber = shipperNumber;
                this.mBooks = new Hashtable();
                this.mDamagedBooks = new Hashtable();
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error creating new carton instance.", ex); }
        }
        #region Accessors/Modifiers: Number, ShipperNumber, ExpectedBookCount
        public string Number { get { return this.mNumber; } }
        public string ShipperNumber { get { return this.mShipperNumber; } }
		public int ExpectedBookCount { 
			get { return this.mExpectedBookCount; } 
			set {
                if (value >= 0 && value <= global::Argix.Properties.Settings.Default.BookCountMax)
					this.mExpectedBookCount = value;
				else
                    throw new ApplicationException("Book count entered must be in the range 0 - " + global::Argix.Properties.Settings.Default.BookCountMax.ToString() + ".");
			} 
		}
		#endregion
		public void AddBook(string bookScan, bool damaged) {
			//Add a book to this carton sample
			string scan="";
			try { 
				//Determine ISBN, UPC, or EAN
                if (bookScan.Length == global::Argix.Properties.Settings.Default.ISBNScanSize) {
					//ISBN: validate; convert to EAN
					if(!Carton.ValidateISBN(bookScan))
						throw new WorkflowException("Book " + bookScan + " failed the ISBN check digit validation.");
                    scan = Carton.ConvertISBNtoEAN(bookScan);
                }
                else if (bookScan.Length == global::Argix.Properties.Settings.Default.UPCScanSize) {
                    //UPC: validate; no conversion
                    if(!Carton.ValidateUPC(bookScan))
                        throw new WorkflowException("Book " + bookScan + " failed the UPC check digit validation.");
                    scan = bookScan;
                }
                else if (bookScan.Length == global::Argix.Properties.Settings.Default.EANScanSize) {
                    //EAN: check for EAN prefix & validate; no conversion
                    if (bookScan.Substring(0, global::Argix.Properties.Settings.Default.EANPrefix.Length) != global::Argix.Properties.Settings.Default.EANPrefix)
                        throw new WorkflowException("An EAN identifier (" + global::Argix.Properties.Settings.Default.EANPrefix + ") was not found in EAN book " + bookScan + ".");
                    if(!Carton.ValidateEAN(bookScan))
                        throw new WorkflowException("Book " + bookScan + " failed the EAN check digit validation.");
                    scan = bookScan;
                }
				else
                    throw new WorkflowException("The scan length was not valid (i.e. EAN=" + global::Argix.Properties.Settings.Default.EANScanSize.ToString() + "; ISBN=" + global::Argix.Properties.Settings.Default.ISBNScanSize.ToString() + ")");
				
				//Add to book/damaged book collection- increment count of existing book or add new isbn#
                if(this.mBooks.ContainsKey(scan)) {
                    this.mBooks[scan] = Convert.ToInt32(this.mBooks[scan]) + 1;
                    this.mDamagedBooks[scan] = Convert.ToInt32(this.mDamagedBooks[scan]) + (damaged ? 1 : 0);
                }
                else {
                    this.mBooks.Add(scan,1);
                    this.mDamagedBooks.Add(scan,(damaged ? 1 : 0));
                }
			}
			catch(WorkflowException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error adding book " + bookScan + ".", ex); }
		}
		public void ClearBooks() {
			//Clear all books in the carton sample
			try {
                this.mBooks.Clear();
                this.mDamagedBooks.Clear();
			}
			catch(Exception ex) { throw new ApplicationException("Failed to clear the  books from the current carton sample.", ex); }
		}
		public int BooksCount { get { return this.mBooks.Count; } }
		public IDictionaryEnumerator BooksEnumerator { get { return this.mBooks.GetEnumerator(); } }
		public void RemoveBook(string scan) {
			//Remove a book from this carton sample
			try { 
				//Remove a book from the collection
				if(this.mBooks.ContainsKey(scan)) {
					int count = Convert.ToInt32(this.mBooks[scan]);
                    if(count == 1) {
                        this.mBooks.Remove(scan);
                        this.mDamagedBooks.Remove(scan);
                    }
                    else {
                        this.mBooks[scan] = count - 1;
                        this.mDamagedBooks[scan] = count - 1;
                    }
				}
				else
					throw new ApplicationException("Cannot remove book " + scan + "; it was not be found in the current carton sample.");
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Failed to remove book " + scan + " from the current carton sample; the book was not found in the sample.", ex); }
		}
		public bool ValidateBookCount() {
			//Validate book count entered agrees with scanned book count
			int scannedBookCount=0;
			bool valid=false;
			try { 
				//Tally scanned books
				foreach(DictionaryEntry book in this.mBooks) 
					scannedBookCount += Convert.ToInt32(book.Value);
				
				//Validate books scanned = expected book count
				if(scannedBookCount == this.mExpectedBookCount)
					valid = true;
				else
					throw new WorkflowException("Number of books entered was " + this.mExpectedBookCount.ToString() + "; you scanned " + scannedBookCount.ToString() + ". You must start over.");
			}
			catch(WorkflowException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error validating the book count in the current carton sample.", ex); }
			return valid;
		}
		public bool Save() {
			//Save sample results
			SqlConnection sqlConn = null;
			bool result=false;
			try {
				sqlConn = new SqlConnection(App.Mediator.Connection);
				sqlConn.Open();
				using(SqlTransaction trans = sqlConn.BeginTransaction()) {
					try {
						//Create sample header
						SqlParameter[] spCartonParams = new SqlParameter[3];
                        spCartonParams[0] = new SqlParameter("@ID",SqlDbType.BigInt, 4);
                        spCartonParams[0].Direction = ParameterDirection.Output;
                        spCartonParams[0].Value = DBNull.Value;
                        spCartonParams[1] = new SqlParameter("@VendorCartonNumber",SqlDbType.Char,25); 
						spCartonParams[1].Value = this.mNumber;
						spCartonParams[2] = new SqlParameter("@VendorNumber", SqlDbType.Char, 5); 
						spCartonParams[2].Value = this.mShipperNumber;
						int i = SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, App.USP_CARTON_NEW, spCartonParams);
						if(i > 0) {
							//Save each book (sample detail)
                            if(this.mBooks.Count > 0) {
                                foreach(DictionaryEntry book in this.mBooks) {
                                    SqlParameter[] spBookParams = new SqlParameter[4];
                                    spBookParams[0] = new SqlParameter("@ID",SqlDbType.BigInt,4);
                                    spBookParams[0].Value = spCartonParams[0].Value;
                                    spBookParams[1] = new SqlParameter("@ISBNNumber",SqlDbType.Char,25);
                                    spBookParams[1].Value = book.Key;
                                    spBookParams[2] = new SqlParameter("@ISBNCount",SqlDbType.Int,2);
                                    spBookParams[2].Value = (Convert.ToInt32(book.Value) - Convert.ToInt32(this.mDamagedBooks[book.Key]));
                                    spBookParams[3] = new SqlParameter("@DamageCount",SqlDbType.Int,2);
                                    spBookParams[3].Value = this.mDamagedBooks[book.Key];
                                    int j = SqlHelper.ExecuteNonQuery(trans,CommandType.StoredProcedure,App.USP_ISBN_NEW,spBookParams);
                                    if(j == 0) throw new ApplicationException("Book " + book.Key.ToString() + " in the sample for carton " + this.mNumber + " could not be saved.");
                                }
                            }
                            else {
                                SqlParameter[] spNoBookParams = new SqlParameter[4];
                                spNoBookParams[0] = new SqlParameter("@ID",SqlDbType.BigInt,4);
                                spNoBookParams[0].Value = spCartonParams[0].Value;
                                spNoBookParams[1] = new SqlParameter("@ISBNNumber",SqlDbType.Char,25);
                                spNoBookParams[1].Value = "0000000000000";
                                spNoBookParams[2] = new SqlParameter("@ISBNCount",SqlDbType.Int,2);
                                spNoBookParams[2].Value = 0;
                                spNoBookParams[3] = new SqlParameter("@DamageCount",SqlDbType.Int,2);
                                spNoBookParams[3].Value = 0;
                                int k = SqlHelper.ExecuteNonQuery(trans,CommandType.StoredProcedure,App.USP_ISBN_NEW,spNoBookParams);
                                if(k == 0) throw new ApplicationException("Book 0000000000000 for carton " + this.mNumber + " could not be saved.");
                            }
						}
						else
                            throw new ApplicationException("The sorted item for carton " + this.mNumber + " could not be updated.");						
						trans.Commit();
						result = true;
					}
					catch(Exception ex) { trans.Rollback(); throw ex; }
				}
			}
			finally { if(sqlConn != null) sqlConn.Dispose(); }
			return result;
		}
		
        public static bool ValidateISBN(string isbnScan) {
			//Validate the check digit for the ISBN scan using Mod11 algorithm
			//Note: Works for 10 character string length (assumes last char is the check digit)
			//ref http://en.wikipedia.org/wiki/ISBN
			bool valid=false;
			try {
				//Validate 10 digit scan length (includes check digit)
				if(isbnScan.Length != 10)
                    throw new ApplicationException("Invalid scan length for ISBN check digit calculation.");
				
				//Compute weighted sum 10*a + 9*b + ... + 2*i + 1*j
				int sum=0, digit=0;
				for(int k=0; k<isbnScan.Length; k++) {
					if(isbnScan.Substring(k, 1).ToLower() == "x")
						digit = 10;
					else
						digit = Convert.ToInt32(Char.GetNumericValue(isbnScan, k));
					sum += (10 - k) * digit;
				}
				valid = (sum % 11 == 0);
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error validating the check digit for ISBN scan " + isbnScan + ".", ex); }
			return valid;
		}
        public static bool ValidateEAN(string eanScan) {
			//Validate the check digit for the EAN scan using Mod10 algorithm
			//Note: Works for any length string (assumes last char is the check digit)
			//ref MK Smalltalk code
			bool valid=false;
			try {
				//Compute weighted sum from right to left; toggle multiplier 3,1,3,1,3,...
				int sum = 0;
				int nDigits = eanScan.Length;
				int parity = nDigits % 2;
				for(int i=nDigits; i>0; i--) {
					int digit = Convert.ToInt32(Char.GetNumericValue(eanScan, i-1));
					if(i % 2 != parity)
						digit *= 3;
					sum += digit;
				}
				valid = (sum % 10 == 0);
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error validating the check digit for EAN scan " + eanScan + ".", ex); }
			return valid;
		}
        public static bool ValidateUPC(string upcScan) {
            //Validate the check digit for the UPC scan using Mod10 algorithm
            //Note: Works for 12 character string length (assumes last char is the check digit)
            //ref http://research.cs.queensu.ca/~bradbury/checkdigit/upccheck.htm
            bool valid = false;
            try {
                //Validate 10 digit scan length (includes check digit)
                if(upcScan.Length != 12)
                    throw new ApplicationException("Invalid scan length for UPC check digit calculation.");

                //Compute weighted sum; toggle multiplier 3,1,3,1,3,...
                int sum = 0;
                for(int i = 0; i < 12; i++) {
                    int digit = Convert.ToInt32(Char.GetNumericValue(upcScan,i));
                    if(i % 2 == 0) digit *= 3;
                    sum += digit;
                }
                valid = (sum % 10 == 0);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error validating the check digit for EAN scan " + upcScan + ".",ex); }
            return valid;
        }
        private static bool _ValidateEAN(string eanScan) {
			//Validate the check digit for the EAN scan using Mod10 algorithm
			//Note: Works for any length string (assumes last char is the check digit)
			//ref http://en.wikipedia.org/wiki/EAN-13, http://en.wikipedia.org/wiki/Luhn_algorithm
			bool valid=false;
			try {
				//Compute weighted sum from right to left; toggle multiplier 3,1,3,1,3,...
				int sum = 0;
				int nDigits = eanScan.Length;
				int parity = nDigits % 2;
				for(int i=0; i<nDigits; i++) {
					int digit = Convert.ToInt32(Char.GetNumericValue(eanScan, i));
					if(i % 2 == parity)
						digit *= 2;
					if(digit > 9)
						digit = digit - 9 ;
					sum += digit;
				}
				valid = (sum % 10 == 0);
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error validating the check digit for EAN scan " + eanScan + ".", ex); }
			return valid;
		}
		public static string ConvertEANtoISBN(string eanScan) {
			//Convert an EAN string to an ISBN string
			string isbnScan="";
			try {
				//Exclude EAN prefix and check digit; append Mod11 check digit
                string scan = eanScan.Substring(global::Argix.Properties.Settings.Default.EANPrefix.Length, global::Argix.Properties.Settings.Default.EANScanSize - global::Argix.Properties.Settings.Default.EANPrefix.Length - 1);
				isbnScan = scan + calcMod11CheckDigit(scan);
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error converting EAN# " + eanScan + " to an ISBN#.", ex); }
			return isbnScan;
		}
        public static string ConvertISBNtoEAN(string isbnScan) {
            //Convert an ISBN string to an EAN string
            string eanScan = "";
            try {
                //Exclude EAN prefix and check digit; append Mod11 check digit
                string scan = global::Argix.Properties.Settings.Default.EANPrefix + isbnScan.Substring(0, global::Argix.Properties.Settings.Default.ISBNScanSize - 1);
                eanScan = scan + calcMod10CheckDigit(scan);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error converting ISBN# " + isbnScan + " to an EAN#.",ex); }
            return eanScan;
        }
		
		#region Local Services: calcMod10CheckDigit(), calcMod11CheckDigit()
		private static string calcMod10CheckDigit(string scan) {
			//Calculate the Mod 10 check digit number for the specified string
			//Note: Works for any string length (assumes last char is the check digit)
			string checkDigit="0";
			try {
				//Compute weighted sum from right to left; toggle multiplier 3,1,3,1,3,...
				int sum=0, rem=0, digit=0;
				int mult=3;
				for(int k=scan.Length; k>0; k--) {
					sum += mult * Convert.ToInt32(Char.GetNumericValue(scan, k - 1));
					mult = 4 - mult;
				}
				
				//Check digit is a # which when added to the mod 10 remainder makes the total = 10
				rem = sum % 10;
				digit = (rem == 0) ? 0 : 10 - rem;
				checkDigit = digit.ToString();
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error calculating the Mod10 check digit.", ex); }
			return checkDigit;
		}
		private static string calcMod11CheckDigit(string scan) {
			//Calculate the Mod 11 check digit number for the specified string
			//Note: Works for 10 character string length (assumes last char is NOT the check digit)
			string checkDigit="0";
			try {
				//Validate 10 digit scan length (includes check digit)
				if(scan.Length != 9)
					throw new Exception("Invalid scan length for Mod 11 check digit calculation.");
				
				//Compute weighted sum 10*a + 9*b + ... + 2*i
				int sum=0, rem=0, digit=0;
				for(int k=0; k<scan.Length; k++) 
					sum += (10 - k) * Convert.ToInt32(Char.GetNumericValue(scan, k));
				
				//Check digit is a # which when added to the mod 11 remainder makes the total = 11
				rem = sum % 11;
				digit = (11 - rem);
				if(digit == 11)			
					checkDigit = "0";
				else if(digit == 10)	
					checkDigit = "X";
				else					
					checkDigit = digit.ToString();
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error calculating the Mod11 check digit.", ex); }
			return checkDigit;
		}
		#endregion
	}
}
