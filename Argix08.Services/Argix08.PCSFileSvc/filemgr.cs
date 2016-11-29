//	File:	pcssvc.cs
//	Author:	J. Heary
//	Date:	10/14/08
//	Desc:	A file utility that moves files from a source folder to a destination
//			folder. The source and destination folders can be on a local drive, a 
//			mapped drive, or a drive described by a UNC path.
//	Rev:	01/27/09 (jph)- modified manageSourceFiles() to add time stamp suffix  
//                          to renamed file since duplicate source files can occur.
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Argix {
	//
	public class FileMgr {
		//Members
		private string mSrcFolder=DEFAULT_SRCFOLDER;
		private string mSrcFilePattern=DEFAULT_FILEPATTERN;
        private string mDestFolder = DEFAULT_DESTFOLDER;
        private string mDestSuffix = DEFAULT_DESTSUFFIX;

        private const string DEFAULT_SRCFOLDER = "c:\\";
        private const string DEFAULT_FILEPATTERN = "*.txt";
        private const string DEFAULT_DESTFOLDER = "\\\\rgxdev\\c$\\";
        private const string DEFAULT_DESTSUFFIX = ".pcs";
				
		//Interface
        public FileMgr() : this(DEFAULT_SRCFOLDER,DEFAULT_FILEPATTERN,DEFAULT_DESTFOLDER,DEFAULT_DESTSUFFIX) { }
        public FileMgr(string srcFolder,string srcFilePattern,string destFolder,string destFileSuffix) {
			//Constructor
			try {
				//Set members
				this.mSrcFolder = srcFolder;
				this.mSrcFilePattern = srcFilePattern;
				this.mDestFolder = destFolder;
                this.mDestSuffix = destFileSuffix;
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new FileMgr instance.",ex); }
        }
        #region Accessors/Modifiers: SourceFolder, SourceFilePattern, DestinationFolder
		public string SourceFolder { get { return mSrcFolder; } }
		public string SourceFilePattern { get { return mSrcFilePattern; } }
		public string DestinationFolder { get { return mDestFolder; } }
		#endregion
        public void Execute() {
            //Execute this service
            try {
                //Check for the specified source folder
                SvcLog.LogMessage("CHECKING FOR FILES IN " + this.mSrcFolder + "...");
                if(Directory.Exists(this.mSrcFolder)) {
                    //Perform file operations in source folder
                    FileInfo oSrcFolder = new FileInfo(this.mSrcFolder);
                    manageSourceFiles(oSrcFolder);
                }
                else
                    SvcLog.LogMessage("DIRECTORY NOT FOUND\t" + this.mSrcFolder);
            }
            catch(Exception ex) { SvcLog.LogMessage("UNEXPECTED ERROR\tFileSvc::Execute(); " + ex.Message); }
        }
        #region Local services: manageSourceFiles(), getDestinationFilename()
		private void manageSourceFiles(FileInfo srcFolder) {
            //Move/copy all files in subfolder <srcFolder> that meet the file pattern
            try {
				//Enumerate source folder files
				string[] srcFiles = Directory.GetFiles(srcFolder.FullName, this.mSrcFilePattern);
				for(int i=0; i<srcFiles.Length; i++) { 
					//Move source file; rename for uniqueness
					FileInfo file = new FileInfo(srcFiles[i]);
                    string destFilename = "";
                    try {
                        if(file.Name.Substring(0,1) != "_" && file.Length > 0) {
                            //Move file and rename source
                            destFilename = getDestinationFilename();
                            file.CopyTo(destFilename,false);
                            SvcLog.LogMessage("MOVED FILE\t" + srcFiles[i] + "\t" + destFilename);
                            file.MoveTo(file.Directory + "\\_" + file.Name + "_" + DateTime.Now.ToString("HHmmss"));
                        }
                    }
                    catch(Exception ex) { SvcLog.LogMessage("MOVE FILE ERROR\t" + srcFiles[i] + "\t" + destFilename + "\t" + ex.Message); }
				}
			} 
			catch(Exception ex) { SvcLog.LogMessage("UNEXPECTED ERROR\tPCSSvc::manageSourceFiles(); " + ex.Message); }
		}
		private string getDestinationFilename() {
			//Determine a unique destination filename
			string filename="";
			try {
                //Ensure time-based filename is unique (i.e. next second)
                int day = DateTime.UtcNow.DayOfYear;
                int seconds = 3600 * DateTime.UtcNow.Hour + 60 * DateTime.UtcNow.Minute + DateTime.UtcNow.Second;
                filename = this.mDestFolder + day.ToString("000") + seconds.ToString("00000") + (this.mDestSuffix.Length > 0 ? this.mDestSuffix : "");
                while(File.Exists(filename)) {
                    //Wait until the next second in time
                    System.Threading.Thread.Sleep(1000 - int.Parse(DateTime.Now.ToString("fff")));
                    
                    //Determine next filename
                    day = DateTime.UtcNow.DayOfYear;
                    seconds = 3600 * DateTime.UtcNow.Hour + 60 * DateTime.UtcNow.Minute + DateTime.UtcNow.Second;
                    filename = this.mDestFolder + day.ToString("000") + seconds.ToString("00000") + (this.mDestSuffix.Length > 0 ? this.mDestSuffix : "");
                }
            }
			catch(Exception ex) { SvcLog.LogMessage("UNEXPECTED ERROR\tPCSSvc::getDestinationFilename(); " + ex.Message); }
			return filename;
		}
		#endregion
	}
}
