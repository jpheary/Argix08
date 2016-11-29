//	File:	filemgr.cs
//	Author:	J. Heary
//	Date:	07/07/10
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

namespace Tsort.Utility {
	//
	public class FileMgr {
		//Members
		private string mSrcFolder="", mSrcFilePattern="", mDestFolder="";
        private string mTerminal="";
				
		//Interface
        public FileMgr(string srcFolder,string srcFilePattern,string destFolder,string terminal) {
			//Constructor
			try {
				//Set members
				this.mSrcFolder = srcFolder;
				this.mSrcFilePattern = srcFilePattern;
				this.mDestFolder = destFolder;
                this.mTerminal = terminal;
            } 
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new FileSvc instance.", ex); }
        }
        #region Accessors/Modifiers: SourceFolder, SourceFilePattern, DestinationFolder, Terminal
        public string SourceFolder { get { return mSrcFolder; } }
		public string SourceFilePattern { get { return mSrcFilePattern; } }
		public string DestinationFolder { get { return mDestFolder; } }
        public string Terminal { get { return mTerminal; } }
        #endregion
		public void Execute() {
			//Execute this service
			try {
				//Check for the specified source folder
				SvcLog.LogMessage("CHECKING FOR FILES IN " + this.mSrcFolder + "...");
				if(Directory.Exists(this.mSrcFolder)) { 
					//Perform file operations in source folder
					FileInfo srcFolder = new FileInfo(this.mSrcFolder);
					manageSourceFiles(srcFolder);
				}
				else
					SvcLog.LogMessage("DIRECTORY NOT FOUND\t" + this.mSrcFolder);
			} 
			catch(Exception ex) { SvcLog.LogMessage("UNEXPECTED ERROR\tExecute(); " + ex.Message); }
		}
		private void manageSourceFiles(FileInfo srcFolder) {
			//Move/copy all files in subfolder <srcFolder> that meet the file pattern
			try {
				//Enumerate source folder files
				string[] srcFiles = Directory.GetFiles(srcFolder.FullName, this.mSrcFilePattern);
				for(int i=0; i<srcFiles.Length; i++) { 
					//Get a source file
					FileInfo srcFile = new FileInfo(srcFiles[i]);
                    string destFilename = this.mDestFolder + srcFile.Name;
					try {
                        //Ignore previously processed files (i.e. _xxx)
                        if(srcFile.Name.Substring(0,1) != "_" && srcFile.Length > 0) {
                            //Validate destination directory
                            if(Directory.Exists(this.mDestFolder)) {                                
                                //Create an editied version and move to destination folder
                                FileInfo editFile = createEditFile(srcFile);
                                if(editFile != null) {
                                    //Success- move edited copy and archive (rename) original source file
                                    editFile.CopyTo(destFilename, true);
                                    //editFile.Delete();
                                    srcFile.MoveTo(srcFile.Directory + "\\_" + srcFile.Name + "_" + DateTime.Now.ToString("MMddyyyyHHmmss"));
                                    SvcLog.LogMessage("FILE COPIED\t" + srcFiles[i] + "\t" + destFilename);
                                }
                            }
                            else {
                                SvcLog.LogMessage("DESTINATION DIRECTORY NOT FOUND\t" + this.mDestFolder);
                            }
                        }
					}
					catch(Exception ex) { SvcLog.LogMessage("MOVE/COPY ERROR\t" + srcFiles[i] + "\t" + destFilename + "\t" + ex.Message); }
				}
			} 
			catch(Exception ex) { SvcLog.LogMessage("UNEXPECTED ERROR\tmanageSourceFiles(); " + ex.Message); }
		}
        private FileInfo createEditFile(FileInfo srcFile) {
            //Edit file contents
            FileInfo editFile=null;
            StreamReader reader = null;
            StreamWriter writer = null;
            try {
                //Edit per config
                string destFile = srcFile.Directory + "\\_" + srcFile.Name + "_edit";
                Hashtable dict = (Hashtable)ConfigurationManager.GetSection("terminals/" + this.mTerminal);
                writer = new StreamWriter(new FileStream(destFile,FileMode.Create,FileAccess.ReadWrite));
                reader = new StreamReader(new FileStream(srcFile.FullName,FileMode.Open,FileAccess.Read));
                reader.BaseStream.Seek(0,SeekOrigin.Begin);
                string record=null;
                while((record = reader.ReadLine()) != null) {
                    if(dict.Count > 0) {
                        //Parse record into fields; update cust field value if required
                        ArrayList fields = split(record);
                        string newRecord="";
                        for(int i=0;i<fields.Count;i++) {
                            if(i==0)
                                newRecord += fields[i];
                            else if(i==5) {
                                if(dict.ContainsKey(fields[5])) 
                                    newRecord += "," + "\"LTA\"" + "," + dict[fields[5]].ToString();
                                else 
                                    newRecord += "," + fields[5] + "," + fields[6];
                                i++;
                            }
                            else
                                newRecord += "," + fields[i];
                        }
                        writer.WriteLine(newRecord);
                    }
                    else {
                        //Pass through
                        writer.WriteLine(record);
                    }
                }
                writer.Flush();
                editFile = new FileInfo(destFile);
            }
            catch(Exception ex) { SvcLog.LogMessage("EDIT ERROR\teditContents(); " + ex.Message); }
            finally {
                if(reader != null) reader.Close();
                if(writer != null) writer.Close(); 
            }
            return editFile;
        }
        private ArrayList split(string record) {
            //Split method that takes into account a token that is not a token
            //Eg: in the middle of a quoted field like  0123,"jph","Heary, James","Argix","001"
            //NOTE: Only accounts for a single token within the token- better if the code
            //      handled this case as well: "Heary, James P, Mr." 
            ArrayList fields=null;
            try {
                fields = new ArrayList();
                string[] tokens = record.Split(new string[] { "," },StringSplitOptions.None);
                for(int i=0;i<tokens.Length;i++) {
                    //Form correct tokens
                    if(tokens[i].StartsWith("\"") && !tokens[i].EndsWith("\"")) {
                        //Add this and next token (see note above)
                        fields.Add(tokens[i] + "," + tokens[i+1]);
                        i++;
                    }
                    else {
                        fields.Add(tokens[i]);
                    }
                }
            }
            catch(Exception ex) { SvcLog.LogMessage("SPLIT ERROR\tsplit(); " + ex.Message); }
            return fields;
        }
    }
}
