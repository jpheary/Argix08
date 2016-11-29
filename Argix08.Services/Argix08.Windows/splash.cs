//	File:	splash.cs
//	Author:	J. Heary
//	Date:	01/04/06
//	Desc:	Provides splash screen services.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Argix.Windows {
	//
	public class Splash {
		//Class Members
		private static dlgSplash mSplash=null;
		private static string Title="Argix Direct Application";
		private static string Version="0.0.0.0";
		private static string Copyright="Copyright 2004 - " + DateTime.Today.Year + " Argix Direct";
        private static EventHandler OnClose = new EventHandler(close);
		
		public static event EventHandler ITEvent=null;
		
		//Interface
        static Splash() { }
        private Splash() { }
		public static void Start(string title, Assembly executingAssembly, string copyright) {
			//
			try {
				//Set splash text
				Splash.Title = title;
				Version ver = executingAssembly.GetName().Version;
				Splash.Version = ver.Major + "." + ver.Minor + "." + ver.Build + "." + ver.Revision;
				Splash.Copyright = copyright;
				
				//Launch splash dialog on an independent thread
				Thread t = new Thread(new ThreadStart(Splash.show));
				t.IsBackground = true;
				t.Start();
				Thread.Sleep(500);
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error while opening Splash screen.",ex); }
        }
		public static void Close() {
			//Closes the dialog
			try {
				//Close the splash screen
                if((Splash.mSplash != null) && (!Splash.mSplash.IsDisposed) && (!Splash.mSplash.Closed)) {
                    Splash.mSplash.BeginInvoke(OnClose);
                }
			}
            catch(Exception ex) { Debug.Write("Unexpected error while closing Splash screen- " + ex.ToString() + "\n"); }
        }
        #region Local Services: show(), close(), OnITEvent()
        private static void show() {
			//This is the actual thread procedure. This method runs on a background thread
			try {
				//Show the splash screen
				Splash.mSplash = new dlgSplash(Splash.Title, Splash.Version, Splash.Copyright);
				Splash.mSplash.ITEvent += new EventHandler(OnITEvent);
				Splash.mSplash.ShowDialog();
			} 
			catch(Exception) { }
		}
        private static void close(object sender,EventArgs e) {
            //This is the actual thread procedure. This method runs on a background thread
            try {
                Splash.mSplash.Close();
            }
            catch(Exception) { }
        }
		private static void OnITEvent(object o, EventArgs e) { if(Splash.ITEvent != null) Splash.ITEvent(o, e); }
        #endregion
    }
}
