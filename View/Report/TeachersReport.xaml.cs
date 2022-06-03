using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace P31School.View.Report
{
    /// <summary>
    /// Логика взаимодействия для TeachersReport.xaml
    /// </summary>
    public partial class TeachersReport : Page
    {
        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        P31School.Classes.ExportClass ex = new P31School.Classes.ExportClass();
        public TeachersReport()
        {
            InitializeComponent();
            ReportTeachers.ItemsSource = dc.Teacher.ToList();
        }

        private void ClassToExcel_Click(object sender, RoutedEventArgs e)
        {
            ex.ExportToExcel(ReportTeachers, 0);
        }

        private void ClassToWord_Click(object sender, RoutedEventArgs e)
        {
            ex.ExportToWord(ReportTeachers, 0);
        }

        private void ClassSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReportTeachers.ItemsSource = dc.Teacher.Where(u=> (u.Surname + " " + u.Name + " "+ u.MidName).Contains(SearchBox.Text)).ToList();
        }

        private void ReportTeachers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
