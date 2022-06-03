using Microsoft.Office.Interop.Word;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using Page = System.Windows.Controls.Page;
using Word = Microsoft.Office.Interop.Word;

namespace P31School.View.Report
{
    /// <summary>
    /// Логика взаимодействия для ReportMain.xaml
    /// </summary>
    public partial class ReportMain : Page
    {
        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        public ReportMain()
        {
            InitializeComponent();
        }

        private void MainReportWord_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            wordApp.Documents.Add();
            Microsoft.Office.Interop.Word.Document doc = wordApp.ActiveDocument;
            var Paragraph = wordApp.ActiveDocument.Paragraphs.Add();
            Paragraph.Range.Font.Size = 14;
            Paragraph.Range.Font.Name = "Times New Roman";
            Paragraph.Range.Text = "Общее количество учеников: " + dc.Pupil.Count();
            Model.Class[] pupils = dc.Class.ToArray();
            Word.Table table1 = doc.Tables.Add(wordApp.Selection.Range, dc.Class.ToList().Count + 1, 2);
            table1.ApplyStyleFirstColumn = true;
            table1.ApplyStyleHeadingRows = true;
            Range wordcellrange = table1.Cell(1, 2).Range;
            wordcellrange = table1.Cell(dc.Class.ToList().Count + 1, 2).Range;
            Object style = "Сетка таблицы";
            table1.set_Style(ref style);
            wordcellrange = table1.Cell(1, 1).Range; wordcellrange.Text = "Класс";
            wordcellrange = table1.Cell(1, 2).Range; wordcellrange.Text = "Численность";
            for (int m = 1; m < table1.Rows.Count; m++)
                for (int n = 0; n < table1.Columns.Count; n++)
                {
                    wordcellrange = table1.Cell(m + 1, 1).Range;
                    dynamic r = pupils[m - 1];
                    wordcellrange.Text = r.ClassName;
                    wordcellrange = table1.Cell(m + 1, 2).Range;
                    wordcellrange.Text = "" + r.Count;
                }
            wordApp.Visible = true;

        }
    }
}
