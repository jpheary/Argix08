using System;
using System.Data;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Argix.MIS {
    //
	public class Config {
		//Members
		private FileInfo mConfigFile=null;			//Current configuration file
		private FileInfo[] mRollbackFiles=null;		//List of previous configuration files
		
		private const string ROLLBACK_EXT = "config.bak";
		public const string XML_ATTRIB_SQLCONN = "SQLConnection";
		
		public event EventHandler Refreshed=null;
		public event EventHandler Changed=null;
		
		//Interface
		public Config() {}
		public Config(string file) {
			//Constructor
			try {
				//Configure this file configuration object
				this.mConfigFile = new FileInfo(file);
			} 
			catch(Exception ex) { throw ex; }
		}
		
		#region Accessors\Modifiers: 
		public FileInfo ActiveFile {
			get { return this.mConfigFile; }
			set { this.mConfigFile = value; }
		}
		public int RollbackFileCount { get { return this.mRollbackFiles.Length; } }
		public FileInfo RollbackFile(int index) { return this.mRollbackFiles[index]; }
		public string DBConnectionString { 
			get { 
				XmlTextReader oReader=null;
				string sConn="";
				try {
					oReader = new XmlTextReader(new StreamReader(this.mConfigFile.FullName));
					while(oReader.Read() && sConn == "") {
						if(oReader.NodeType == XmlNodeType.Element) {
							if(oReader.Name == "add") {
								while(oReader.MoveToNextAttribute()) {
									if(oReader.Name == "key" && oReader.Value == XML_ATTRIB_SQLCONN) {
										oReader.MoveToNextAttribute();
										sConn = oReader.Value;
										break;
									}
								}
							}
						}
					}
				}
				catch(Exception) { }
				finally { if(oReader != null) oReader.Close(); }
				return sConn;
			} 
		}
		#endregion
		public void Refresh() { 
			//Refresh configuration data for this object
			try {
				//Build a collection of rollback configuration files
				this.mRollbackFiles = this.mConfigFile.Directory.GetFiles("*." + ROLLBACK_EXT);
			}
			catch(Exception ex) { throw ex; }
			finally { if(this.Refreshed!=null) this.Refreshed(this, new EventArgs()); }
		}
		public string Read(string fileName) { 
			//Read xml from the current config file or a rollback config file
			string sXml="";
			XmlTextReader oReader=null;
			try { 
				//Determine file
				oReader = new XmlTextReader(new StreamReader(this.mConfigFile.DirectoryName + "\\" + fileName));
				oReader.Read();
				sXml = oReader.ReadOuterXml();
			}
			catch(Exception ex) { throw ex; }
			finally { if(oReader != null) oReader.Close(); }
			return sXml;
		}
		public void Rollback(string rollbackFileName) { 
			//Rollback to a prior configuration file version
			try {
				//Validate
				if(rollbackFileName.EndsWith(ROLLBACK_EXT)) {
					//Save rollback file contents to current config file
					string xml = Read(rollbackFileName);
					Save(xml);
				}
				else
					throw new ApplicationException(this.mConfigFile.DirectoryName + "\\" + rollbackFileName + "is an invalid rollback configuration file.");
			}
			catch(Exception ex) { throw ex; }
		}
		public void Save(string xml) {
			//Save this object to disk
			try {
				//Backup current file for rollback
				FileInfo oFile = new FileInfo(this.mConfigFile.FullName);
				oFile.CopyTo(oFile.DirectoryName + "\\" + DateTime.Now.ToString("yyyy-MM-dd HHmmss") + "." + ROLLBACK_EXT);
				
				//Save new file
				StreamWriter oWriter = new StreamWriter(this.mConfigFile.FullName);
				oWriter.Write(xml);
				oWriter.Close();
				Refresh();
			}
			catch(Exception ex) { throw ex; }
			finally { if(this.Changed!=null) this.Changed(this, new EventArgs()); }
		}
		public void SaveAs(string fileName, string xml) {
			//Save this object to disk
			try {
				//Validate
				if(fileName == this.mConfigFile.FullName) {
					//Same as Save()
					Save(xml);
				}
				else {
					//Save to user specified file
					StreamWriter oWriter = new StreamWriter(fileName);
					oWriter.Write(xml);
					oWriter.Close();
				}
			}
			catch(Exception ex) { throw ex; }
		}
	}
}
