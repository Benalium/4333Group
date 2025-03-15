using _4333Project.MaximPackage;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace _4333Project {
    /// <summary>
    /// Interaction logic for _4333_Tazyukov.xaml
    /// </summary>
    public partial class _4333_Tazyukov : System.Windows.Window {
        public _4333_Tazyukov() {
            InitializeComponent();
        }
        private void ButtonImportExcel_Click(object sender, RoutedEventArgs e) {
            var sheet = (Excel.Worksheet)new Excel.Application { Visible = true }
                .Workbooks
                .Open(Procedures.DialogFileName())
                .Sheets[1];
            Procedures.AddUsers(
                Enumerable.Range(2, 
                    sheet
                        .Cells
                        .SpecialCells(Excel.XlCellType.xlCellTypeLastCell)
                        .Row - 1
                )
                    .ToList()
                    .Select(i => new BadUser(
                        sheet.Cells[i, 2].Text, 
                        sheet.Cells[i, 3].Text,
                        sheet.Cells[i, 4].Text,
                        sheet.Cells[i, 1].Text
                    ))
                    .ToList()
            );
        }
        private void ButtonExportExcel_Click(object sender, RoutedEventArgs e) {
            (List<Role> roles, List<User> users) = Procedures.RolesAndUsersFromDb();
            var workbook = new Excel.Application { Visible = true }.Workbooks.Add();
            roles.ForEach(role => {
                var worksheet = workbook.Worksheets.Add();
                worksheet.Cells[1][1] = "Login";
                worksheet.Cells[2][1] = "Password";
                worksheet.Name = role.name;
                users
                    .Where(u => u.roleId == role.id)
                    .Select((user, index) => new {user, index})
                    .ToList()
                    .ForEach(u => {
                        worksheet.Cells[1][u.index + 2] = u.user.login;
                        worksheet.Cells[2][u.index + 2] = u.user.password;
                    });
                worksheet.Columns.AutoFit();
            });
        }
        private void ButtonImportJson_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog() {
                DefaultExt = "*.json;*.json",
                Filter = "JSON file|*.json",
                Title = "Choose a file to convert."
            };
            openFileDialog.ShowDialog();
            var badUsers = new List<BadUser>();
            var options = new JsonSerializerOptions { IncludeFields = true };
            using(FileStream fs = new FileStream(openFileDialog.FileName, FileMode.OpenOrCreate)) {
                badUsers = JsonSerializer.Deserialize<List<BadUser>>(fs, new JsonSerializerOptions { IncludeFields = true });
            }
            Procedures.AddUsers(badUsers);
        }
        private void ButtonExportJson_Click(object sender, RoutedEventArgs e) {
            (List<Role> roles, List<User> users) = Procedures.RolesAndUsersFromDb();
            var app = new Word.Application() { Visible = true };
            Word.Document document = app.Documents.Add();
            Word.Paragraph paragraph = document.Paragraphs.Add();
            roles.ForEach(role => {
                paragraph.Range.Text = Convert.ToString(role.name);
                paragraph.set_Style("Heading 1");
                paragraph.Range.InsertParagraphAfter();
                Word.Table usersTable = document.Tables.Add(
                    paragraph.Range, 
                    users.Where(u => u.roleId == role.id).Count() + 1, 
                3);
                usersTable.Borders.InsideLineStyle = usersTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                usersTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                usersTable.Cell(1, 1).Range.Text = "Login";
                usersTable.Cell(1, 2).Range.Text = "Password";
                usersTable.Cell(1, 3).Range.Text = "E-mail";
                usersTable.Rows[1].Range.Bold = 1;
                usersTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                users
                    .Where(u => u.roleId == role.id)
                    .Select((@object, index) => new {@object, index}).ToList()
                    .ForEach(u => {
                        Word.Cell cellRange = usersTable.Cell(u.index + 2, 1);
                        cellRange.Range.Text = u.@object.fullName;
                        cellRange.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        cellRange = usersTable.Cell(u.index + 2, 2);
                        cellRange.Range.Text = u.@object.password;
                        cellRange.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        cellRange = usersTable.Cell(u.index + 2, 3);
                        cellRange.Range.Text = u.@object.login;
                        cellRange.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    }
                );
            });
        }
    }
}
