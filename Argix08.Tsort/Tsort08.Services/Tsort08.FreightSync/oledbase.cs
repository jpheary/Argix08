//	File:	oledbase.cs
//	Author:	S. Virk
//	Date:	01/15/04
//	Desc:	Removed unused methods; modified to be consistent with SQLHelper.
//	Rev:	10/17/06 (jph)- revised to accomodate changes in base class.
//	---------------------------------------------------------------------------
using System;
using System.Data.OleDb;
using System.Data;

namespace Tsort.Sort {
	//
	internal class OleDBase {
		//Members
		private OleDbConnection mCONN=null;
		
		//Interface
		public OleDBase() { }
		protected string Connection { set { this.mCONN = new OleDbConnection(value); } }
		protected DataSet FillDataSet(string sql, string table) {
			OleDbCommand CMD=null;
			DataSet ds = new DataSet();
			try {
				CMD = new OleDbCommand(sql, this.mCONN);  
				CMD.CommandType = CommandType.Text; 
				OleDbDataAdapter da = new OleDbDataAdapter();
				da.SelectCommand = CMD;
				da.Fill(ds, table);
			}
			catch(Exception ex) { throw ex; }
			finally { CMD.Connection.Close(); }
			return ds;
		}
		protected int ExecuteNonQuery(string sql) {
			//
			OleDbCommand CMD=null;
			int rowsAffected = 0;
			try {
				CMD = new OleDbCommand(sql, this.mCONN);  
				CMD.CommandType = CommandType.Text; 
				CMD.Connection.Open();
				rowsAffected = CMD.ExecuteNonQuery();
			}
			catch(Exception ex) { throw ex; }
			finally { CMD.Connection.Close(); }
			return rowsAffected;
		}
	}
}
