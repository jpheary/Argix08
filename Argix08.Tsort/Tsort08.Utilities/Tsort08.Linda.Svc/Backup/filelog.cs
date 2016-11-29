//	File:	filelog.cs
//	Author:	J. Heary
//	Date:	10/24/05
//	Desc:	Manages message logging to a text file; manages log files.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace Tsort05 {
	//
	//Singleton for (global) log tracing
	public class FileLog {
		//Members
		private static bool _Enabled=true;
		private static string _Filepath=Environment.CurrentDirectory;
		private static int _FilesMax=5;
		
		//Constants
		private const string FILE_FORMAT = "yyyyMMdd";
		private const string FILE_EXT = "log";
		private const string MSG_FORMAT = "yyyy-MM-dd HH:mm:ss";
		
		//Interface		
		static FileLog() { }
		#region Accessors/Modifiers
		public static bool Enabled { get { return _Enabled; } set { _Enabled = value; } }
		public static string FilePath { get { return _Filepath; } set { _Filepath = value; } }
		public static string Filename { get { return _Filepath + DateTime.Today.ToString("yyyyMMdd") + ".log"; } }
		public static int FileCount { get { return _FilesMax; } set { _FilesMax = value; } }
		#endregion
		public static void LogMessage(string sMessage) {
			//Log <sMessage> to the current log file
			StreamWriter oStream=null;
			string sFileName="";
			try { 
				//Validate log file directory; create if does not exist
				if(!Directory.Exists(_Filepath)) Directory.CreateDirectory(_Filepath);
				
				//Determine log filename; if a new file, do a log purge
				sFileName = DateTime.Today.ToString(FILE_FORMAT) + "." + FILE_EXT;
				if(!File.Exists(_Filepath + sFileName)) purgeLogFiles();
				
				//Create/open current log file
				if(_Filepath.EndsWith("\\"))
					oStream = new StreamWriter(_Filepath + sFileName, true);
				else
					oStream = new StreamWriter(_Filepath + "\\" + sFileName, true);
				
				//Print the date and message to the log file
				string logEntry = DateTime.Now.ToString(MSG_FORMAT) + " : " + sMessage;
				oStream.WriteLine(logEntry);
				//Debug.Write(logEntry + "\n");
			}
			catch(Exception ex) { throw ex; }
			finally { oStream.Close(); }
		}
		#region Local services
		private static void purgeLogFiles() { 
			//Delete log files older than <this._FilesMax> days
			try { 
				string[] files = Directory.GetFiles(_Filepath, "*." + FILE_EXT);
				for(int i=0; i<files.Length; i++) {
					FileInfo oFile = new FileInfo(files[i]);
					if(DateTime.Today.AddDays(-_FilesMax).CompareTo(oFile.CreationTime) > 0)
						oFile.Delete();
				}
			}
			catch(Exception) { }
		}
		#endregion
	}
}
