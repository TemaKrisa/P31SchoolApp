using Microsoft.Office.Interop.Excel;
using System;
using System.Windows.Controls;
using Word = Microsoft.Office.Interop.Word;

namespace P31School.Classes
{
    class ExportClass
    {
        public void ExportToExcel(DataGrid dt, int min)
        {
            try
            {
                Application excel = new Application();
                excel.Visible = true;
                Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

                for (int i = 0; i < dt.Columns.Count - min; i++)
                { 
                    Range myRange = (Range)sheet1.Cells[1, i + 1];
                    sheet1.Columns[i + 1].ColumnWidth = 15;
                    myRange.Value2 = dt.Columns[i].Header;
                    for (int j = 0; j < dt.Items.Count; j++)
                    {
                        TextBlock b = dt.Columns[i].GetCellContent(dt.Items[j]) as TextBlock;
                        Range myRange2 = (Range)sheet1.Cells[j + 2, i + 1];
                        myRange2.Value2 = b.Text;
                    }
                }

                Range tRange = sheet1.UsedRange;
                tRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                tRange.Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThick;
                sheet1.Columns.AutoFit();
            }
            catch (Exception)
            {

            }
        }

        public void ExportToWord(DataGrid dt, int min)
        {

            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            wordApp.Documents.Add();
            Microsoft.Office.Interop.Word.Document doc = wordApp.ActiveDocument;
            var Paragraph = wordApp.ActiveDocument.Paragraphs.Add();
            Word.Table table1 = doc.Tables.Add(wordApp.Selection.Range, dt.Items.Count + 1, dt.Columns.Count - min);
            table1.ApplyStyleFirstColumn = true;
            table1.ApplyStyleHeadingRows = true;
            Word.Range wordcellrange = table1.Cell(1, 2).Range;
            Object style = "Сетка таблицы";
            table1.set_Style(ref style);

            for (int i = 0; i < dt.Columns.Count - min; i++)
            {
                wordcellrange = table1.Cell(1, i + 1).Range;
                wordcellrange.Text = (string)dt.Columns[i].Header;
                for (int j = 0; j < dt.Items.Count; j++)
                {
                    TextBlock b = dt.Columns[i].GetCellContent(dt.Items[j]) as TextBlock;
                    wordcellrange = table1.Cell(j + 2, i + 1).Range;
                    wordcellrange.Text = b.Text;
                }
            }
            wordApp.Visible = true;

        }
    }
}
