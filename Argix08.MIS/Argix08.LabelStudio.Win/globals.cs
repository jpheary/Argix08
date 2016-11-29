using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using Argix.Data;
using Argix.Windows;
using Tsort.Labels;

namespace Tsort {
    /// <summary>The main entry point for the application.</summary>
    static class Program {
        //Members
        //Interface
        [STAThread]
        static void Main() {
            //Application entry point
            try {
                //Start app
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Tsort.Tools.frmMain());
            }
            catch(Exception ex) {
                MessageBox.Show("FATAL ERROR\n\n" + ex.ToString(),App.Product,MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }

    //Global application object
    public class App: AppBase {
        //Members
        private static Mediator _Mediator = null;
		
		public const string USP_LABELSTORE_VIEW  ="uspToolsLabelStudioLabelStoreView", TBL_LABELSTORE_VIEW = "LabelDetailTable";
		public const string USP_LABEL_UPDATEORNEW = "uspToolsLabelStudioLabelUpdateOrNew";
		public const string USP_LABEL_DELETE ="uspToolsLabelStudioLabelDelete";
		
		public const int ICON_OPEN = 10;
		public const int ICON_CLOSED = 11;
		public const int ICON_APP = 12;
		
		//Interface
        static App() {
            //Class constructor: get application configuration
            try {
                _Mediator = new SQLMediator();
            }
            catch(Exception ex) { ReportError(ex); Application.Exit(); }
        }
        internal static Mediator Mediator { get { return _Mediator; } }
        public static string EventLogName { get { return "Tsort08"; } }
        public static string RegistryKey { get { return "LabelStudio"; } }
        public static void ReportError(Exception ex) { ReportError(ex,true); }
        public static void ReportError(Exception ex,bool displayMessage) {
            //Report an exception to the user
            try {
                string src = (ex.Source != null) ? ex.Source + "-\n" : "";
                string msg = src + ex.Message;
                if(ex.InnerException != null) {
                    if((ex.InnerException.Source != null)) src = ex.InnerException.Source + "-\n";
                    msg = src + ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
                }
                if(displayMessage)
                    MessageBox.Show(msg,App.Product,MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            catch(Exception) { }
        }
    }
	
	namespace Tools { 
		//TsortNode creates objects that can reside in a TreeView control (i.e. TreeNode)
		//and also provide custom application functionality when selected
		public abstract class TsortNode: TreeNode {
			//Members
			protected TreeNode[] mChildNodes=null;
			//Constants
			//Events
			//Interface
			public TsortNode(string text, int imageIndex, int selectedImageIndex) {
				//Constructor
				try {
					//Set members and base node members
					this.Text = text.Trim();
					this.ImageIndex = imageIndex;
					this.SelectedImageIndex = selectedImageIndex;
				}
				catch(Exception ex) { throw new ApplicationException("Unexpected error while creating (abstract) Tsort Node instance.", ex); }
			}
			public void ExpandNode() { 
				//Load [visible] child nodes for each child
				if(this.mChildNodes!=null) {
					foreach(TsortNode node in this.mChildNodes) 
						node.LoadChildNodes();
				}
			}
			public void CollapseNode() { 
				//Unload [hidden] child nodes for each child
				if(this.mChildNodes!=null) {
					foreach(TsortNode node in this.mChildNodes) 
						node.UnloadChildNodes();
				}
			}
			public virtual void LoadChildNodes() { return; }
			public virtual void UnloadChildNodes() { base.Nodes.Clear(); this.mChildNodes = null; }
			public abstract void Refresh();
			public abstract bool CanNew { get; }
			public abstract void New();
			public abstract bool CanOpen { get; }
			public abstract bool HasProperties { get; }
		}
        
        public class EnterpriseNode: TsortNode {
            //Members
            private const string KEY_TERMINALS = "labelStores/terminals";
            private const string KEY_TEMPLATES = "labelStores/templates";
            private const string KEY_BACKUPS = "labelStores/backups";

            //Interface
        public EnterpriseNode(string text,int imageIndex,int selectedImageIndex) : base(text,imageIndex,selectedImageIndex) { }
            #region TsortNode Implementations: LoadChildNodes(), Refresh()
            public override void LoadChildNodes() {
                //Load child nodes of this node (data)
                Hashtable oDict=null;
                IDictionaryEnumerator oEnum=null;

                //Clear nodes and populate
                base.Nodes.Clear();
                #region Read terminals from config file (key=terminal name/value=database connection)
                try {
                    oDict = (Hashtable)ConfigurationManager.GetSection(KEY_TERMINALS);
                    oEnum = oDict.GetEnumerator();
                    base.mChildNodes = new TreeNode[oDict.Count];
                    for(int i=0;i<base.mChildNodes.Length;i++) {
                        //Create a terminal store for each entry; wrap in a custom treenode
                        oEnum.MoveNext();
                        DictionaryEntry oEntry = (DictionaryEntry)oEnum.Current;
                        LabelStoreNode treeNode=null;
                        try {
                            LabelStore oStore = new TerminalLabelStore(oEntry.Value.ToString());
                            treeNode = new LabelStoreNode(oEntry.Key.ToString(),App.ICON_CLOSED,App.ICON_CLOSED,oStore);
                            treeNode.Refresh();
                        }
                        catch(ApplicationException ex) { throw ex; }
                        catch(Exception ex) { throw new ApplicationException("Unexpected error loading enterprise child nodes.",ex); }
                        base.mChildNodes[i] = treeNode;
                        base.Nodes.Add(treeNode);
                        treeNode.LoadChildNodes();
                    }
                }
                catch(Exception ex) { throw new ApplicationException("Failed to load all terminal label store nodes for enterprise node " + base.Text + ".",ex); }
                #endregion
                #region Read directory label stores from config file
                try {
                    oDict = (Hashtable)ConfigurationManager.GetSection(KEY_TEMPLATES);
                    oEnum = oDict.GetEnumerator();
                    base.mChildNodes = new TreeNode[oDict.Count];
                    for(int i=0;i<base.mChildNodes.Length;i++) {
                        //Create a terminal store for each entry; wrap in a custom treenode
                        oEnum.MoveNext();
                        DictionaryEntry oEntry = (DictionaryEntry)oEnum.Current;
                        LabelStoreNode treeNode=null;
                        try {
                            LabelStore oStore = new DirectoryLabelStore(oEntry.Value.ToString());
                            treeNode = new LabelStoreNode(oEntry.Key.ToString(),App.ICON_CLOSED,App.ICON_CLOSED,oStore);
                            treeNode.Refresh();
                        }
                        catch(Exception) { }
                        base.mChildNodes[i] = treeNode;
                        base.Nodes.Add(treeNode);
                        treeNode.LoadChildNodes();
                    }
                }
                catch(Exception ex) { throw new ApplicationException("Failed to load all directory label store nodes for enterprise node " + base.Text + ".",ex); }
                #endregion
                #region Read file label stores from config file
                try {
                    oDict = (Hashtable)ConfigurationManager.GetSection(KEY_BACKUPS);
                    oEnum = oDict.GetEnumerator();
                    base.mChildNodes = new TreeNode[oDict.Count];
                    for(int i=0;i<base.mChildNodes.Length;i++) {
                        //Create a terminal store for each entry; wrap in a custom treenode
                        oEnum.MoveNext();
                        DictionaryEntry oEntry = (DictionaryEntry)oEnum.Current;
                        LabelStoreNode treeNode=null;
                        try {
                            LabelStore oStore = new FileLabelStore(oEntry.Value.ToString());
                            treeNode = new LabelStoreNode(oEntry.Key.ToString(),App.ICON_CLOSED,App.ICON_CLOSED,oStore);
                            treeNode.Refresh();
                        }
                        catch(Exception) { }
                        base.mChildNodes[i] = treeNode;
                        base.Nodes.Add(treeNode);
                        treeNode.LoadChildNodes();
                    }
                }
                catch(Exception ex) { throw new ApplicationException("Failed to load all file label store nodes for enterprise node " + base.Text + ".",ex); }
                #endregion
            }
            public override void Refresh() { LoadChildNodes(); }
            public override bool CanNew { get { return false; } }
            public override void New() { }
            public override bool CanOpen { get { return false; } }
            public override bool HasProperties { get { return false; } }
            #endregion
        }
        public class LabelStoreNode:TsortNode {
            //Members
            private LabelStore mLabelStore=null;

            //Interface
            public LabelStoreNode(string text,int imageIndex,int selectedImageIndex,LabelStore labelStore): base(text,imageIndex,selectedImageIndex) {
                //Constructor
                try {
                    this.mLabelStore = labelStore;
                    this.mLabelStore.StoreChanged += new EventHandler(OnStoreChanged);
                }
                catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Label Store Node instance.",ex); }
            }
            public LabelStore LabelStore { get { return this.mLabelStore; } }
            #region TsortNode Implementations: LoadChildNodes(), Refresh()
            public override void LoadChildNodes() {
                //
                try {
                    //Clear existing child nodes (label templates)
                    base.Nodes.Clear();

                    //Create label template objects (inherit from TreeNode); add to node array
                    base.mChildNodes = new TreeNode[this.mLabelStore.Labels.LabelDetailTable.Rows.Count];
                    for(int i=0;i<this.mLabelStore.Labels.LabelDetailTable.Rows.Count;i++) {
                        LabelTemplate template=null;
                        try {
                            template = this.mLabelStore.NewLabelTemplate((LabelDS.LabelDetailTableRow)this.mLabelStore.Labels.LabelDetailTable.Rows[i]);
                            LabelTemplateNode node = new LabelTemplateNode(template.LabelType + " (" + template.PrinterType + ")",App.ICON_APP,App.ICON_APP,template);
                            base.mChildNodes[i] = node;
                            base.Nodes.Add(node);

                            //Cascade loading child nodes if this node is expanded (to get the + sign)
                            if(base.IsExpanded) node.LoadChildNodes();
                        }
                        catch(Exception) { MessageBox.Show("Error on label template loading; continuing..."); }
                    }
                }
                catch(Exception ex) { throw new ApplicationException("Failed to load all child nodes for label store " + base.Text + ".",ex); }
            }
            public override void Refresh() { this.mLabelStore.Refresh(); }
            public override bool CanNew { get { return true; } }
            public override void New() {
                //Create a new label template
                try {
                    LabelTemplate template = this.mLabelStore.NewLabelTemplate();
                    dlgLabelTemplate dlg = new dlgLabelTemplate(template);
                    if(dlg.ShowDialog() == DialogResult.OK) {
                        this.mLabelStore.Labels.LabelDetailTable.AddLabelDetailTableRow(template.LabelType,template.PrinterType,template.LabelString);
                        LoadChildNodes();
                    }
                }
                catch(Exception ex) { throw new ApplicationException("Failed to create a new label template for label store " + base.Text + ".",ex); }
            }
            public override bool CanOpen { get { return false; } }
            public override bool HasProperties { get { return false; } }
            #endregion
            private void OnStoreChanged(object sender,EventArgs e) { LoadChildNodes(); }
        }
        public class LabelTemplateNode:TsortNode {
            //Members
            private LabelTemplate mLabelTemplate=null;

            //Interface
            public LabelTemplateNode(string text,int imageIndex,int selectedImageIndex,LabelTemplate labelTemplate): base(text,imageIndex,selectedImageIndex) {
                //Constructor
                try {
                    this.mLabelTemplate = labelTemplate;
                }
                catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Label Template Node instance.",ex); }
            }
            public LabelTemplate LabelTemplate { get { return this.mLabelTemplate; } }
            #region TsortNode Implementations: LoadChildNodes(), Refresh()
            public override void LoadChildNodes() { }
            public override void Refresh() { }
            public override bool CanNew { get { return false; } }
            public override void New() { }
            public override bool CanOpen { get { return true; } }
            public override bool HasProperties { get { return false; } }
            #endregion
        }
    }
}