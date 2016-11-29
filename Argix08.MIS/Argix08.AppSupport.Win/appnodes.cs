using System;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Text;
using System.Threading;
using Argix.Data;

namespace Argix.MIS {
    //Class and interface definitions	
	public class Enterprise: AppNode {
		//Members
		private const string KEY_TERMINALS = "enterprise/terminals";
		
		//Interface
		public Enterprise(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) { }
		public override void LoadChildNodes() {
			//Load child nodes of this node (data)
			try {
				//Clear existing nodes
				base.Nodes.Clear();
				
				//Read terminals from config file (key=terminalName, value=web server url)
				Hashtable oDict = (Hashtable)ConfigurationManager.GetSection(KEY_TERMINALS);
				IDictionaryEnumerator oEnum = oDict.GetEnumerator();
				base.mChildNodes = new TreeNode[oDict.Count];
				for(int i=0; i<base.mChildNodes.Length; i++) {
					oEnum.MoveNext();
					DictionaryEntry oEntry = (DictionaryEntry)oEnum.Current;
					Terminal terminalNode = new Terminal(oEntry.Key.ToString(), App.ICON_OPEN, App.ICON_CLOSED, oEntry.Value.ToString());
					base.mChildNodes[i] = terminalNode;
					
					//Cascade loading child nodes if this node is expanded (to get the + sign)
					terminalNode.LoadChildNodes();
				}
				base.Nodes.AddRange(base.mChildNodes);
                base.Nodes[0].Expand();
            } 
			catch(Exception ex) { throw ex; }
		}
		public override void Refresh() { LoadChildNodes(); }
	}

    public class Terminal: AppNode {
        //Members
        private int mID=0;
        private string mDescription="";
        private string mWebDirectory="";

        public event EventHandler Refreshed=null;

        //Interface
        public Terminal(string text,int imageIndex,int selectedImageIndex,string webDirectory) : base(text,imageIndex,selectedImageIndex) {
            //Constructor
            try {
                //Set custom attributes
                this.mID = 0;
                this.mDescription = text;
                this.mWebDirectory = webDirectory;
            }
            catch(Exception ex) { throw ex; }
        }
        #region Accessors\Modifiers: [Members...]
        public int TerminalID { get { return this.mID; } }
        public string Description { get { return this.mDescription; } }
        public string WebDirectory { get { return this.mWebDirectory; } }
        #endregion
        public override void LoadChildNodes() {
            //Load child nodes of this node
            try {
                //Create Tsort application objects (inherit from TreeNode); add to node array
                string[] apps = Directory.GetDirectories(this.mWebDirectory);
                base.Nodes.Clear();
                base.mChildNodes = new TreeNode[apps.Length];
                for(int i=0;i<apps.Length;i++) {
                    FileInfo oDir = new FileInfo(apps[i]);
                    Department dept = new Department(oDir.Name,App.ICON_OPEN,App.ICON_CLOSED,oDir.FullName);
                    base.mChildNodes[i] = dept;

                    //Cascade loading child nodes if this node is expanded (to get the + sign)
                    if(base.IsExpanded) dept.LoadChildNodes();
                }
                base.Nodes.AddRange(base.mChildNodes);
                base.Nodes[0].Expand();
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error loading terminal child nodes.",ex); }
            finally { if(this.Refreshed!=null) this.Refreshed(this,new EventArgs()); }
        }
        public override void Refresh() { LoadChildNodes(); }
    }

    public class Department: AppNode {
        //Members
        private int mID=0;
        private string mDescription="";
        private string mWebDirectory="";

        public event EventHandler Refreshed=null;
        
        //Interface
        public Department(string text,int imageIndex,int selectedImageIndex,string webDirectory) : base(text,imageIndex,selectedImageIndex) {
            //Constructor
            try {
                //Set custom attributes
                this.mID = 0;
                this.mDescription = text;
                this.mWebDirectory = webDirectory;
            }
            catch(Exception ex) { throw ex; }
        }
        #region Accessors\Modifiers: [Members...]
        public int TerminalID { get { return this.mID; } }
        public string Description { get { return this.mDescription; } }
        public string WebDirectory { get { return this.mWebDirectory; } }
        #endregion
        public override void LoadChildNodes() {
            //Load child nodes of this node
            try {
                 base.Nodes.Clear();
                string[] apps = Directory.GetDirectories(this.mWebDirectory);
                base.mChildNodes = new TreeNode[apps.Length];
                for(int i=0;i<apps.Length;i++) {
                    FileInfo oDir = new FileInfo(apps[i]);
                    ArgixApp appNode = new ArgixApp(oDir.Name,App.ICON_OPEN,App.ICON_CLOSED,oDir.FullName);
                    base.mChildNodes[i] = appNode;

                    //Cascade loading child nodes if this node is expanded (to get the + sign)
                    if(base.IsExpanded) appNode.LoadChildNodes();
                }
                base.Nodes.AddRange(base.mChildNodes);
                base.Nodes[0].Expand();
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error loading terminal child nodes.",ex); }
            finally { if(this.Refreshed!=null) this.Refreshed(this,new EventArgs()); }
        }
        public override void Refresh() { LoadChildNodes(); }
    }

    public class ArgixApp: AppNode {
        //Members
        private int mID=0;
        private string mDescription="";
        private string mWebDirectory="";

        public event EventHandler Refreshed=null;

        //Interface
        public ArgixApp(string text,int imageIndex,int selectedImageIndex,string webDirectory): base(text,imageIndex,selectedImageIndex) {
            //Constructor
            try {
                //Set custom attributes
                this.mID = 0;
                this.mDescription = text;
                this.mWebDirectory = webDirectory;
            }
            catch(Exception ex) { throw ex; }
        }
        #region Accessors\Modifiers: [Members...]
        public int TerminalID { get { return this.mID; } }
        public string Description { get { return this.mDescription; } }
        public string WebDirectory { get { return this.mWebDirectory; } }
        #endregion
        public override void LoadChildNodes() {
            //Load child nodes of this node
            try {
                base.Nodes.Clear();
                //Determine the current deployment version
                string[] deployManifest = Directory.GetFiles(this.mWebDirectory,"*.application");
                string[] apps = Directory.GetDirectories(this.mWebDirectory + "\\Application Files");

                base.mChildNodes = new TreeNode[deployManifest.Length + apps.Length];
                int j=0;
                if(deployManifest.Length > 0) {
                    j=1;
                    FileInfo fi = new FileInfo(deployManifest[0]);
                    DeploymentManifest dm = new DeploymentManifest(fi.Name,App.ICON_APP,App.ICON_APP,fi.FullName);
                    base.mChildNodes[0] = dm;
                }
                for(int i=0;i<apps.Length;i++) {
                    FileInfo oDir = new FileInfo(apps[i]);
                    Deployment d = new Deployment(oDir.Name,App.ICON_APP,App.ICON_APP,oDir.FullName);
                    base.mChildNodes[i + j] = d;

                    //Cascade loading child nodes if this node is expanded (to get the + sign)
                    if(base.IsExpanded) d.LoadChildNodes();
                }
                base.Nodes.AddRange(base.mChildNodes);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error loading application child nodes.",ex); }
            finally { if(this.Refreshed!=null) this.Refreshed(this,new EventArgs()); }
        }
        public override void Refresh() { LoadChildNodes(); }
    }

    public class DeploymentManifest:AppNode {
        //Members
        private string mUNCPath="";

        //Interface
        public DeploymentManifest(string text,int imageIndex,int selectedImageIndex,string UNCPath) : base(text,imageIndex,selectedImageIndex) {
            //Constructor
            try {
                //Set custom attributes
                base.Name = text;
                this.mUNCPath = UNCPath;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating new ArgixApp instance.",ex); }
        }
        #region Accessors/Modifiers: UNCPath, DisplayName
        public string UNCPath { get { return this.mUNCPath; } }
        public string DisplayName { get { return base.Name + " (" + this.mUNCPath + ")"; } }
        #endregion
        public override void Refresh() { }
    }

    public class Deployment:AppNode {
        //Members
        private string mUNCPath="";
        private string mProductName="";
        private string mEventLogName="";
        private Config mFileConfig=null;
        private DBConfig mDBConfig=null;
        private TraceLog mTraceLog=null;

        private const string TYPENAME = "Tsort.App";
        private const string EVENTLOGPROPERTYNAME = "EventLogName";

        //Interface
        public Deployment(string text,int imageIndex,int selectedImageIndex,string UNCPath) : base(text,imageIndex,selectedImageIndex) {
            //Constructor
            try {
                //Set custom attributes
                base.Name = text;
                this.mUNCPath = UNCPath;
                //initialize();
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating new ArgixApp instance.",ex); }
        }
        public Config FileConfiguration { get { return this.mFileConfig; } }
        public DBConfig DBConfiguration { get { return this.mDBConfig; } }
        public TraceLog TraceLog { get { return this.mTraceLog; } }
        #region Accessors/Modifiers: UNCPath, DisplayName, ProductName, EventLogName, ConnectionInfo
        public string UNCPath { get { return this.mUNCPath; } }
        public string DisplayName { get { return base.Name + " (" + this.mUNCPath + ")"; } }
        public string ProductName {
            get {
                //Return the application product name for specified exe
                try {
                    //Do this once to avoid multiple network access calls
                    if(this.mProductName == "") {
                        Assembly oAssy = Assembly.LoadFile(getExeName());
                        object[] o = oAssy.GetCustomAttributes(typeof(AssemblyProductAttribute),false);
                        AssemblyProductAttribute att = (AssemblyProductAttribute)o[0];
                        this.mProductName = att.Product;
                    }
                }
                catch(Exception ex) { throw new ApplicationException("Unexpected exception returning product name.",ex); }
                return this.mProductName;
            }
        }
        public string EventLogName {
            get {
                //Return the application event log name for specified exe
                try {
                    //Do this once to avoid multiple network access calls
                    if(this.mEventLogName == "") {
                        Assembly oAssy = Assembly.LoadFile(getExeName());
                        Type type = oAssy.GetType(TYPENAME,true,true);
                        PropertyInfo logName = type.GetProperty(EVENTLOGPROPERTYNAME);
                        this.mEventLogName = logName.GetValue(null,null).ToString();
                    }
                }
                catch(Exception ex) { throw new ApplicationException("Unexpected exception returning event log name.",ex); }
                return this.mEventLogName;
            }
        }
        public string ConnectionInfo {
            get {
                //Return connection informaton
                string conn="";
                try {
                    string[] tokens = this.mFileConfig.DBConnectionString.Split((char)';');
                    conn = (tokens.Length > 0) ? tokens[0] : "Data Source=?";
                    conn += "; " + ((tokens.Length > 1) ? tokens[1] : "Initial Catalog=?");
                }
                catch(Exception ex) { throw new ApplicationException("Unexpected exception returning connection info.",ex); }
                return conn;
            }
        }
        #endregion
        public override void Refresh() {
            this.mDBConfig.Refresh();
            this.mFileConfig.Refresh();
            this.mTraceLog.Refresh();
        }
        public void CopyTo() { }		//Copy this application to another server
        public void Delete() { }		//Delete this application from the current server
        public void MoveTo() { }		//Move this application to another server
        public void Properties() { }	//Display properties for this application
        public void Run() { }			//Run this application
        public void Print() { }			//Print the configuration for this database
        #region Local Services: initialize(), getExeName()
        private void initialize() {
            //Initialize application configuration
            try {
                //Determine the current deployment version
                string[] deployManifest = Directory.GetFiles(this.mUNCPath,"*.application");

                string[] configFiles = Directory.GetFiles(this.mUNCPath,"*.config");
                FileInfo file = new FileInfo(configFiles[0]);
                this.mFileConfig = new Config(file.FullName);

                //Database configuration object
                this.mDBConfig = new DBConfig(getExeName(),this.mFileConfig);

                //Trace log object
                this.mTraceLog = new TraceLog(this.EventLogName,this.mFileConfig.DBConnectionString);
            }
            catch(Exception ex) { throw ex; }
        }
        private string getExeName() {
            //Return the exe name for this Tsort application
            string name="";
            try {
                string[] sExeFiles = Directory.GetFiles(this.mUNCPath,"*.exe");
                FileInfo oExe = new FileInfo(sExeFiles[0]);
                name = oExe.FullName;
            }
            catch(Exception) { }
            return name;
        }
        #endregion
    }
}
