using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;

namespace _4333Project.MaximPackage {
    public static class Main {
        public static void Import(Action<SqlConnection, string[,]> ExecuteCommand) {
            // Getting data from Excel
            OpenFileDialog openFileDialog = new OpenFileDialog() {
                DefaultExt = "*.xls;*.xlsx",
                Filter = "файл Excel (Spisok.xlsx)|*.xlsx",
                Title = "Выберите файл базы данных"
            };
            openFileDialog.ShowDialog(); // implicitly changes `FileName` property of the openFileDialog object
            var app = new Excel.Application();
            var workbook = app.Workbooks.Open(openFileDialog.FileName);
            var sheet = (Excel.Worksheet)workbook.Sheets[1];
            var data = ExcelReader.ExcelData(sheet);
            workbook.Close(false, Type.Missing, Type.Missing);
            app.Quit();

            var connectionString = "Server=localhost\\MSSQLSERVER01;Database=test_DB;Trusted_Connection=True;";
            using(var connection = new SqlConnection(connectionString)) {
                connection.Open();
                ExecuteCommand(connection, data);
            }
        }
    }
}
