//	File:	ftpmgr.cs
//	Author:	J. Heary
//	Date:	11/14/08
//	Desc:	An FTP file utility that renames files on an FTP Server.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace Tsort05.Utility {
	//
	public class FTPMgr {
		//Members
        private FtpClient mFtpClient = null;
        private string mFilePattern = DEFAULT_FILEPATTERN;
        private string mFilePrefix = DEFAULT_FILEPREFIX;
        private string mFileSuffix = DEFAULT_FILESUFFIX;

        private const string DEFAULT_FILEPATTERN = "*.man";
        private const string DEFAULT_FILEPREFIX = "M";
        private const string DEFAULT_FILESUFFIX = "";
				
		//Interface
        public FTPMgr() : this("localhost",".",DEFAULT_FILEPATTERN,"anonymous","anonymous@argixdirect.com") { }
        public FTPMgr(string serverName,string remotePath,string srcFilePattern,string userID,string password) : this(serverName,remotePath,srcFilePattern,userID,password,DEFAULT_FILEPREFIX,DEFAULT_FILESUFFIX) { }
        public FTPMgr(string serverName,string remotePath,string srcFilePattern,string userID,string password,string prefix,string suffix) {
			//Constructor
			try {
				//Set members
                this.mFtpClient = new FtpClient(serverName,userID,password);
                this.mFtpClient.RemotePath = remotePath;
                this.mFilePattern = srcFilePattern;
                this.mFilePrefix = prefix;
                this.mFileSuffix = suffix;
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new FTPMgr instance.",ex); }
        }
        #region Accessors/Modifiers: FilePattern
		public string FilePattern { get { return mFilePattern; } }
		#endregion
        public void Execute() {
            //Execute this service
            try {
                //Perform file operations in source folder
                SvcLog.LogMessage("CHECKING FOR FILES ON " + this.mFtpClient.Server + "/" + this.mFtpClient.RemotePath + "...");
                this.mFtpClient.Login();
                string[] srcFiles = this.mFtpClient.GetFileList(this.mFilePattern);
                for(int i = 0; i < srcFiles.Length; i++) {
                    //Rename for uniqueness
                    string srcFile = srcFiles[i];
                    string destFile = "";
                    try {
                        if(srcFile.Trim().Length > 0 && srcFile.Substring(0,1) != "_") {
                            //Move file and rename source
                            destFile = getDestinationFilename();
                            this.mFtpClient.RenameFile(srcFile,destFile,true);
                            DBLog.LogFileMove(srcFile,destFile);
                            SvcLog.LogMessage("RENAMED FILE\t" + srcFile + "\t" + destFile);
                        }
                    }
                    catch(Exception ex) { SvcLog.LogMessage("MOVE FILE ERROR\t" + srcFiles[i] + "\t" + destFile + "\t" + ex.Message); }
                }
                this.mFtpClient.Close();
            }
            catch(Exception ex) { SvcLog.LogMessage("UNEXPECTED ERROR\t" + ex.Message); }
        }
        #region Local services: getDestinationFilename()
        private string getDestinationFilename() {
            //Determine a unique destination filename
            string filename = "";
            try {
                //Ensure time-based filename is unique (i.e. next second)
                System.Threading.Thread.Sleep(1000);
                string year = DateTime.UtcNow.ToString("yy").Substring(1,1);
                int day = DateTime.UtcNow.DayOfYear;
                int seconds = 3600 * DateTime.UtcNow.Hour + 60 * DateTime.UtcNow.Minute + DateTime.UtcNow.Second;
                filename = this.mFilePrefix + year + day.ToString("000") + seconds.ToString("00000") + this.mFileSuffix;
            }
            catch(Exception ex) { SvcLog.LogMessage("UNEXPECTED ERROR\t" + ex.Message); }
            return filename;
        }
        #endregion
    }
}
