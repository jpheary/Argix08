using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;
using System.Windows.Forms;
using Argix.Windows;

namespace Argix.MIS {
	//
	public class Enterprise: TsortNode {
		//Members
		private const string KEY_TERMINALS = "enterprise/terminals";
		
		//Interface
		public Enterprise(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) { }
		#region TsortNode Implementations: LoadChildNodes()
		public override void LoadChildNodes() {
			//Load child nodes of this node (data)
			try {
				//Clear existing nodes
				base.Nodes.Clear();
				
				//Read terminals from config file (key=terminalName, value=web server url)
				Hashtable table = (Hashtable)ConfigurationManager.GetSection(KEY_TERMINALS);
                IDictionaryEnumerator oEnum = table.GetEnumerator();
                base.mChildNodes = new TreeNode[table.Count];
				for(int i=0; i<base.mChildNodes.Length; i++) {
                    oEnum.MoveNext();
                    DictionaryEntry entry = (DictionaryEntry)oEnum.Current;
				    try {
					    entry = (DictionaryEntry)oEnum.Current;
					    Terminal terminal = new Terminal(entry.Key.ToString(), App.ICON_TERMINAL, App.ICON_TERMINAL, entry.Value.ToString());
					    base.mChildNodes[i] = terminal;
					    terminal.Expand();
					    if(base.IsExpanded) terminal.LoadChildNodes();
                        oEnum.MoveNext();
                    }
                    catch(Exception ex) { throw new ApplicationException("Unexpected error loading " + (entry.Key != null ? entry.Key.ToString() : "??") + " terminal node.",ex); }
				}
				base.Nodes.AddRange(base.mChildNodes);
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error loading terminal nodes.",ex); }
		}
		#endregion
	}
    
    public class Terminal:TsortNode {
        //Members
        private string mName="";
        private string mConnectionID="";
        private ScannerDS mScannerDS=null;

        //Interface
        public Terminal(string name,int imageIndex,int selectedImageIndex,string connectionID) : base(name,imageIndex,selectedImageIndex) {
            //Constructor
            try {
                //Set custom attributes
                this.mName = name;
                this.mConnectionID = connectionID;
                this.mScannerDS = new ScannerDS();
                IDictionary dict = (IDictionary)ConfigurationManager.GetSection(this.mName.ToLower() + "/scanners");
                int scanners = Convert.ToInt32(dict["count"]);
                for(int i = 1; i <= scanners; i++) {
                    string src = "";
                    try {
                        dict = (IDictionary)ConfigurationManager.GetSection(this.mName.ToLower() + "/scanner" + i.ToString());
                        src = dict["source"].ToString();
                        this.mScannerDS.ScannerTable.AddScannerTableRow(this.mName,src);
                    }
                    catch(Exception ex) { throw new ApplicationException("Unexpected error refreshing " + this.mName + " " + (src.Length > 0 ? src : "??") + " scanner node.",ex); }
                }
                this.mScannerDS.AcceptChanges();
                LoadChildNodes();
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error creating new terminal instance.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        public string Name { get { return this.mName; } }
        public string ConnectionID { get { return this.mConnectionID; } }
        public ScannerDS Scanners { get { return this.mScannerDS; } }
        #endregion
        #region TsortNode Implementations: LoadChildNodes()
        public override void LoadChildNodes() {
            //Load child Scanner nodes of this Terminal node
            try {
                //Clear existing nodes
                base.Nodes.Clear();
                base.mChildNodes = new TreeNode[this.mScannerDS.ScannerTable.Rows.Count];
                for(int i=0;i<this.mScannerDS.ScannerTable.Rows.Count;i++) {
                    Scanner scanner=null;
                    try {
                        scanner = new Scanner(this.mScannerDS.ScannerTable[i].SourceName,App.ICON_SCANNER,App.ICON_SCANNER,this.mScannerDS.ScannerTable[i],this.mConnectionID);
                        base.mChildNodes[i] = scanner;
                        if(base.IsExpanded) scanner.LoadChildNodes();
                    }
                    catch(Exception ex) { throw new ApplicationException("Unexpected error loading " + this.mName + " " + (scanner != null ? scanner.SourceName : "??") + " scanner node.",ex); }
                }
                base.Nodes.AddRange(base.mChildNodes);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error loading " + this.mName + " scanner nodes.",ex); }
        }
        #endregion
    }
}