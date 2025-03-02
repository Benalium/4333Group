using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace _4333Project
{
    public static class ExcelReader
    {
        public static string [,] ExcelData(Worksheet sheet) {
            var lastCell = sheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell);
            string[,] mash = new string[lastCell.Row, lastCell.Column];
            for (int i = 0; i < lastCell.Row; i++) for (int j = 0; j < lastCell.Column; j++) {
                mash[i, j] = sheet.Cells[i + 1, j + 1].Text;
            }
            return mash;
        }
    }
}
