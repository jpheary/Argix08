using System;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace Argix {
	//
	//Singleton for (global) log tracing
	public class SvcLog {
		//Members
        public static bool Enabled = true;
        public static string Filepath = Environment.CurrentDirectory;
        public static int FilesMax = 7;
		
		private const string FILE_FORMAT = "yyyyMMdd";
		private const string FILE_EXT = "log";
		private const string MSG_FORMAT = "yyyy-MM-dd HH:mm:ss";
		
		//Interface		
		static SvcLog() { }
        private SvcLog() { }
        #region Accessors/Modifiers: CurrentLogFile
        public static string CurrentLogFile { get { return Filepath + (Filepath.EndsWith("\\") ? "" : "\\") + DateTime.Today.ToString(FILE_FORMAT) + "." + FILE_EXT; } }
		#endregion
		public static void LogMessage(string message) {
			//Log the specified message to the current log file
			StreamWriter stream=null;
			try { 
				//Validate log file directory; create if does not exist
				if(!Directory.Exists(Filepath)) Directory.CreateDirectory(Filepath);
				
				//If a current log file is new, do a log purge
                if(!File.Exists(CurrentLogFile)) purgeLogFiles();
				
                //Create/open current log file; print the date and message to the log file
                stream = new StreamWriter(CurrentLogFile,true);
                stream.WriteLine(DateTime.Now.ToString(MSG_FORMAT) + " : " + message);
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
