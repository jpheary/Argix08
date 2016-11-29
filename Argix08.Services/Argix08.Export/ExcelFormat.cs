using System;
using System.Data;
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;

namespace Argix {
	//
	public class ExcelFormat {
		//Members
		
        //Interface
		public ExcelFormat() { }
		public void Transform(DataSet ds) { Transform(ds, null); }
		public void Transform(DataSet ds, string fileName) {
			//Declare the application, workbook and spreadsheet variables
			Application app=null; 
			Workbook workbook=null;
			Worksheet worksheet=null;
			int rowNum=1, colNum=1;
			
			//Add a blank workbook with blank spreadsheet to the Excel application
			app = new Application();
			workbook = app.Workbooks.Add(Type.Missing);
			worksheet = (Worksheet)workbook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
			
			//Add the formatting necessary and add the column headers.
			for(int i=0; i<ds.Tables[0].Columns.Count; i++) 
				worksheet.Cells[rowNum, i+1] = ds.Tables[0].Columns[i].ColumnName;
			
			foreach(DataColumn col in ds.Tables[0].Columns) {
				System.Diagnostics.Debug.WriteLine(col.ColumnName);
				if((col.DataType.Equals(typeof(System.String)))) {
					((Range)worksheet.Cells[rowNum,colNum]).EntireColumn.NumberFormat ="@";
				}
				else if ((col.DataType.Equals(typeof(System.DateTime)))) {
					//We need to differentiate between Date and Time fields.
					if (ds.Tables[0].Rows[0][col] == System.DBNull.Value) { 
						//If field is null then we can figure out the format based on the column name
						if (col.ColumnName.ToLower().IndexOf("date",0) > -1)
							((Range)worksheet.Cells[rowNum,colNum]).EntireColumn.NumberFormat = "MM/dd/yyyy h:mm";
						else
							((Range)worksheet.Cells[rowNum,colNum]).EntireColumn.NumberFormat = "h:mm";
					}
					else {
						if (((DateTime)ds.Tables[0].Rows[0][col]).Year <= 1900)
							((Range)worksheet.Cells[rowNum,colNum]).EntireColumn.NumberFormat = "h:mm";
						else
							((Range)worksheet.Cells[rowNum,colNum]).EntireColumn.NumberFormat = "MM/dd/yyyy";
					}
						
				}
				colNum++;
			}
			
			//Insert the data into multi-dimentional array
			int rowCount = ds.Tables[0].Rows.Count;
			int colCount = ds.Tables[0].Columns.Count;
			object[,] valArray = new object[rowCount,colCount];
			for(int i=0; i<rowCount; i++) {
				for(int j=0; j<colCount; j++) {	
					if (ds.Tables[0].Rows[i][j].GetType().Equals(typeof(System.String)))
						valArray[i,j] = "'" + ds.Tables[0].Rows[i][j].ToString();
					else
						valArray[i,j] = ds.Tables[0].Rows[i][j];
				}
			}
			worksheet.Visible = XlSheetVisibility.xlSheetVisible;
			worksheet.get_Range(worksheet.Cells[rowNum + 1,1], worksheet.Cells[rowNum + rowCount,colCount]).Value2 = valArray;
			worksheet.get_Range(worksheet.Cells[rowNum + 1,1], worksheet.Cells[rowNum + rowCount,colCount]).EntireColumn.AutoFit();
			
			//Save the spreadsheet as a report
			if(fileName == null)
				app.Visible = true;
			else
				app.ActiveWorkbook.SaveAs(fileName, XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing); 
		}
	}
}
