//	File:	threadwithstate.cs
//	Author:	J. Heary
//	Date:	06/08/04
//	Desc:	
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Threading;

namespace Tsort.Windows {
	//Class for a thread with state
	public class ThreadWithState {
		//Members
		private frmViewer m_oViewer=null;
		private IDataMgr m_oDataMgr=null;
		private GetReportDataComplete m_oCallBack=null;
		
		public ThreadWithState(frmViewer aViewer, IDataMgr aDataMgr, GetReportDataComplete aDelegate) {
			//The constructor obtains the state information and the callback delegate
			Debug.Write("ThreadWithState::ThreadWithState()\n");
			this.m_oViewer = aViewer;
			this.m_oDataMgr = aDataMgr;
			this.m_oCallBack = aDelegate;
		}
		public void ThreadProc() {
			//The thread procedure performs the data access, and then invokes the callback 
			//delegate with the filled dataset
			Debug.Write("ThreadWithState::ThreadProc()\n");
			Debug.Write("Thread= " + Thread.CurrentThread.Name + "\n");
			DataSet ds = new DataSet();
			ds.Merge(this.m_oViewer.GetReportData(this.m_oDataMgr));
			if(this.m_oCallBack != null)
				this.m_oCallBack(ds);
		}
	}
}
