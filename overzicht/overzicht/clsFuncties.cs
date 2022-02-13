using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Tools.Ribbon;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools.Excel;

namespace overzicht
{
	public static class clsFuncties
	{
		public static void WriteCellByCell(int row, int column, Worksheet worksheet, string sMelding)
		{

		    if (row > 0 && column > 0)
		    {
		        var cell = (Excel.Range) worksheet.Cells[row, column];
		        cell.Value2 = sMelding;
		    }
		}

		public static void WriteCellByCell(int row, int column, string worksheetName, string sMelding)
		{
            
		    if (row > 0 && column > 0)
		    {
		        Worksheet worksheet = Globals.Factory.GetVstoObject(
		            Globals.ThisWorkbook.Application.ActiveWorkbook.Worksheets[worksheetName]);
		        var cell = (Excel.Range)worksheet.Cells[row, column];
		        cell.Value2 = sMelding;
		    }
		}

		public static string GetCellValue(int row, int column, string worksheetName)
		{
			Worksheet worksheet = Globals.Factory.GetVstoObject(
				Globals.ThisWorkbook.Application.ActiveWorkbook.Worksheets[worksheetName]);
			var cell = (Excel.Range)worksheet.Cells[row, column];
			string terug = cell.Value.ToString();
			return terug;
		}
	}
}
