using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Text;

namespace Argix.MIS {
	//
	public class Scanner: TsortNode {
		//Members
		private string mTerminalName="", mSourceName="";
        private CubeService mCubeService = null;
				
		//Interface
		public Scanner(string text, int imageIndex, int selectedImageIndex, ScannerDS.ScannerTableRow scanner, string dbConnection) : base(text, imageIndex, selectedImageIndex) { 
			//Constructor
			try {
				//Configure this terminal from the terminal configuration information
                this.mCubeService = new CubeService(dbConnection);;
				if(scanner != null) {
					this.mTerminalName = scanner.TerminalName;
					this.mSourceName = scanner.SourceName;
				}
			} 
			catch(Exception ex) { throw new ApplicationException("Unexpected error creating new Scanner instance.", ex); }
		}
        #region Accessors\Modifiers: [Members...]
        public string TerminalName { get { return this.mTerminalName; } }
		public string SourceName { get { return this.mSourceName; } }
		#endregion
		public ScannerDS GetCubeStats(DateTime startDate, DateTime endDate) { 
			//
			try {				
                return this.mCubeService.GetCubeStats(this.mSourceName,startDate,endDate);
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error reading cube statistics.",ex); }
		}
        public ScannerDS GetCubeStatsSummary(DateTime startDate,DateTime endDate) { 
			//
			try {
                return this.mCubeService.GetCubeStatsSummary(this.mSourceName,startDate,endDate);
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error reading cube statistics summary.",ex); }
		}
        public ScannerDS GetCubeDetails(DateTime startDate,DateTime endDate) { 
			//
			try {				
				return this.mCubeService.GetCubeDetails(this.mSourceName,startDate,endDate);
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error reading cube details.",ex); }
		}
        public ScannerDS GetCubeDetails(string labelSeqNumber) {
            //
            try {
                return this.mCubeService.GetCubeDetail(labelSeqNumber);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error reading cube details.",ex); }
        }
        public ScannerDS GetEventLog(DateTime startDate,DateTime endDate) { 
			//
			try {
                return this.mCubeService.GetLogEntries(this.mSourceName,startDate,endDate);
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error reading cube event log.",ex); }
		}
	}
}