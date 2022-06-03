using P31School.Classes;
using P31School.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace P31School.View.Report
{
    public partial class ClassesReport : Page
    {
        P31SchoolEntities dc = new P31SchoolEntities();
        ExportClass ex = new ExportClass();
        public ClassesReport()
        {
            InitializeComponent();
            ReportClass.ItemsSource = dc.Class.ToList();
        }

        private void ClassToExcel_Click(object sender, RoutedEventArgs e) => ex.ExportToExcel(ReportClass, 0);

        private void ClassToWord_Click(object sender, RoutedEventArgs e) => ex.ExportToWord(ReportClass, 0);

        private void ClassSearch_TextChanged(object sender, TextChangedEventArgs e) => ReportClass.ItemsSource = dc.Class.Where(u => u.ClassName.Contains(ClassSearch.Text)).ToList();
    }
}
