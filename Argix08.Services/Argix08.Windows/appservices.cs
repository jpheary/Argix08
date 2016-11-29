//	File:	appservices.cs
//	Author:	J. Heary
//	Date:	01/04/06
//	Desc:	Application services.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Reflection;

namespace Argix.Windows {
	//
	public class AppServices {
		public AppServices() { }
		
		/// <summary>
		/// Call this Win32 function to bring an existing window to foreground.
		/// </summary>
		/// <param name="handle"></param>
		/// <returns></returns>
		[System.Runtime.InteropServices.DllImport("user32.dll",EntryPoint="SetForegroundWindow", CallingConvention= System.Runtime.InteropServices.CallingConvention.StdCall, CharSet=System.Runtime.InteropServices.CharSet.Unicode, SetLastError=true)]
		public static extern bool SetForegroundWindow(IntPtr handle );

		/// <summary>
		/// Call ShowWindow to show existing Application if it's minimized.
		/// </summary>
		/// <param name="handle"></param>
		/// <param name="nCmd"></param>
		/// <returns></returns>
		[System.Runtime.InteropServices.DllImport("user32.dll",EntryPoint="ShowWindow", CallingConvention=System.Runtime.InteropServices.CallingConvention.StdCall, CharSet=System.Runtime.InteropServices.CharSet.Unicode, SetLastError=true)]
		
		public static extern bool ShowWindow(IntPtr handle , Int32 nCmd );
		/// <summary>
		/// Returns running instance of the existing app. Process object will be null
		/// if it's not running.
		/// </summary>
		/// <returns>Process</returns>
		public static Process RunningInstance() { 
			//
			Process current = Process.GetCurrentProcess(); 
			Process[] processes = Process.GetProcessesByName (current.ProcessName); 
			
			//Loop through the running processes in with the same name 
			foreach (Process process in processes) { 
				//Ignore the current process 
				if (process.Id != current.Id) { 
					//Make sure that the process is running from the exe file. 
					//if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName) 
					if (process.ProcessName == current.ProcessName) {  
						//Return the other process instance.  
						return process;
					}  
				}  
			} 
			//No other instance was found, return null.  
			return null;  
		} 
		/// <summary>
		/// This method will check for existing app based on the main window title because process runs 
		/// under IEExe.exe process when run using URL. Without this it will not let you run second instance
		/// of any application running using url.
		/// </summary>
		/// <param name="mainWindowTitle"></param>
		/// <returns></returns>
		public static Process RunningInstance(string mainWindowTitle) { 
			Process current = Process.GetCurrentProcess(); 
			Process[] processes = Process.GetProcessesByName (current.ProcessName); 
			
			//Loop through the running processes in with the same name 
			foreach (Process process in processes) { 
				//Ignore the current process 
				if(process.Id != current.Id) { 
					//Make sure that the process does not have the same Main Window title
					if(process.MainWindowTitle.ToUpper() == mainWindowTitle.ToUpper()) {  
						//Return the other process instance.  
						return process;
					}
					if(process.MainWindowTitle.Length >= mainWindowTitle.Length) { 
						if(process.MainWindowTitle.Substring(0, mainWindowTitle.Length).ToUpper() == mainWindowTitle.ToUpper()) { 
							//Return the other process instance.  
							return process;
						}
					}
				}  
			} 
			//No other instance was found, return null.  
			return null;  
		} 
	}
}
