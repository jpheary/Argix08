//	File:	ftplog.cs
//	Author:	J. Heary
//	Date:	04/20/09
//	Desc:	Manages message logging to a text file; manages log files.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace Argix.Net {
	//Singleton for (global) log tracing
	public class FTPLog {
		//Members
        public static bool Enabled = true;
        public static string Filepath = System.Windows.Forms.Application.StartupPath; //Environment.CurrentDirectory;
        public static int FilesMax = 7;
		
		private const string FILE_FORMAT = "yyyyMMdd";
		private const string FILE_EXT = "log";
		private const string MSG_FORMAT = "yyyy-MM-dd HH:mm:ss";
		
		//Interface		
		static FTPLog() { }
        private FTPLog() { }
        #region Accessors/Modifiers: Filename
        public static string Filename { get { return Filepath + DateTime.Today.ToString(FILE_FORMAT) + "." + FILE_EXT; } }
		#endregion
		public static void LogMessage(string message) {
			//Log the specified message to the current log file
			StreamWriter stream=null;
			string filename="";
			try { 
				//Validate log file directory; create if does not exist
				if(!Directory.Exists(Filepath)) Directory.CreateDirectory(Filepath);
				
				//Determine log filename; if a new file, do a log purge
				filename = DateTime.Today.ToString(FILE_FORMAT) + "." + FILE_EXT;
				if(!File.Exists(Filepath + filename)) 
					purgeLogFiles();
				
				//Create/open current log file
				if(Filepath.EndsWith("\\"))
					stream = new StreamWriter(Filepath + filename, true);
				else
					stream = new StreamWriter(Filepath + "\\" + filename, true);
				
				//Print the date and message to the log file
				string entry = DateTime.Now.ToString(MSG_FORMAT) + " : " + message;
				stream.WriteLine(entry);
				Debug.Write(entry + "\n");
			}
			catch(Exception ex) { throw ex; }
			finally { stream.Close(); }
        }
        #region Local services: purgeLogFiles()
        private static void purgeLogFiles() { 
			//Delete log files older than _FilesMax days
			try { 
				string[] files = Directory.GetFiles(Filepath, "*." + FILE_EXT);
				for(int i=0; i<files.Length; i++) {
					FileInfo file = new FileInfo(files[i]);
                    if(DateTime.Today.AddDays(-FilesMax).CompareTo(file.CreationTime) > 0)
                        file.Delete();
				}
			}
			catch(Exception) { }
		}
		#endregion
	}
}
