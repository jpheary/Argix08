//	File:	filesvc.cs
//	Author:	J. Heary
//	Date:	10/14/08
//	Desc:	A file utitlity that moves or copies files from a source folder to 
//			a destination folder. The source and destination folders can be on 
//			a local drive, a mapped drive, or a drive described by a UNC path.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Tsort05.Utility {
	//
	public class FileSvc {
		//Members
		private string mSrcFolder=DEFAULT_SRCFOLDER;
		private string mSrcFilePattern=DEFAULT_FILEPATTERN;
		private string mDestFolder=DEFAULT_DESTFOLDER;
		private bool mMoveFile=false;
		
		private const string DEFAULT_SRCFOLDER = "c:\\";
		private const string DEFAULT_FILEPATTERN = "*.txt";
		private const string DEFAULT_DESTFOLDER = "\\\\rgxdev\\c$\\";
				
		//Interface
		public FileSvc() : this(DEFAULT_SRCFOLDER, DEFAULT_FILEPATTERN, DEFAULT_DESTFOLDER,false) { }
		public FileSvc(string srcFolder, string srcFilePattern, string destFolder, bool moveFile) {
			//Constructor
			try {
				//Set members
				this.mSrcFolder = srcFolder;
				this.mSrcFilePattern = srcFilePattern;
				this.mDestFolder = destFolder;
				this.mMoveFile = moveFile;
			} 
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new FileSvc instance.", ex); }
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
				FileLog.LogMessage("CHECKING FOR FILES IN " + this.mSrcFolder + "...");
				if(Directory.Exists(this.mSrcFolder)) { 
					//Perform file operations in source folder
					FileInfo oSrcFolder = new FileInfo(this.mSrcFolder);
					manageSourceFiles(oSrcFolder);
				}
				else
					FileLog.LogMessage("DIRECTORY NOT FOUND\t" + this.mSrcFolder);
			} 
			catch(Exception ex) { FileLog.LogMessage("UNEXPECTED ERROR\tFileSvc::Execute(); " + ex.Message); }
		}
		private void manageSourceFiles(FileInfo srcFolder) {
			//Move/copy all files in subfolder <srcFolder> that meet the file pattern
			try {
				//Enumerate source folder files
				string[] srcFiles = Directory.GetFiles(srcFolder.FullName, this.mSrcFilePattern);
				for(int i=0; i<srcFiles.Length; i++) { 
					//Move/copy source file
					FileInfo oFile = new FileInfo(srcFiles[i]);
					string destFilename="";
					try { 
						//Move/copy file
						destFilename = this.mDestFolder + oFile.Name;
						if(!Directory.Exists(this.mDestFolder)) 
							Directory.CreateDirectory(this.mDestFolder);
						if(this.mMoveFile) {
							oFile.MoveTo(this.mDestFolder + oFile.Name);
							FileLog.LogMessage("MOVED FILE\t" + srcFiles[i] + "\t" + destFilename);
						}
						else {
							oFile.CopyTo(this.mDestFolder + oFile.Name, true);
							FileLog.LogMessage("COPIED FILE\t" + srcFiles[i] + "\t" + destFilename);
						}
					}
					catch(Exception ex) { FileLog.LogMessage("MOVE/COPY ERROR\t" + srcFiles[i] + "\t" + destFilename + "\t" + ex.Message); }
				}
			} 
			catch(Exception ex) { FileLog.LogMessage("UNEXPECTED ERROR\tFileSvc::manageSourceFiles(); " + ex.Message); }
		}
	}
}
