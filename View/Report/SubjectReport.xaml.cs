using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace P31School.View.Report
{
    /// <summary>
    /// Логика взаимодействия для SubjectReport.xaml
    /// </summary>
    public partial class SubjectReport : Page
    {
        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        P31School.Classes.ExportClass ex = new P31School.Classes.ExportClass();
        public SubjectReport()
        {
            InitializeComponent();
            ReportSubject.ItemsSource = dc.Subject.ToList();
        }

        private void ClassToExcel_Click(object sender, RoutedEventArgs e)
        {
            ex.ExportToExcel(ReportSubject, 0);
        }

        private void ClassToWord_Click(object sender, RoutedEventArgs e)
        {
            ex.ExportToWord(ReportSubject, 0);
        }

        private void ClassSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReportSubject.ItemsSource = dc.Subject.Where(u=> u.SubjectName.Contains(SearchBox.Text)).ToList();
        }
    }
}
