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
using P31School.Classes;

namespace P31School.View.Report
{
    /// <summary>
    /// Логика взаимодействия для PupilsReport.xaml
    /// </summary>
    public partial class PupilsReport : Page
    {
        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        ExportClass ex = new ExportClass();
        public PupilsReport()
        {
            InitializeComponent();
            ReportPupil.ItemsSource = dc.Pupil.ToList();
        }

        private void ClassSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReportPupil.ItemsSource = dc.Pupil.Where(u => (u.Surname + " " + u.Name + " " + u.Midname + " " + u.Class.ClassName).Contains(ClassSearch.Text)).ToList();
        }

        private void ClassToExcel_Click(object sender, RoutedEventArgs e)
        {
            ex.ExportToExcel(ReportPupil, 0);
        }

        private void ClassToWord_Click(object sender, RoutedEventArgs e)
        {
            ex.ExportToWord(ReportPupil, 0);
        }
    }
}
