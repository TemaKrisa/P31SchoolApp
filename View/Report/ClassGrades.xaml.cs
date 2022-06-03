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
    /// Логика взаимодействия для ClassGrades.xaml
    /// </summary>
    public partial class ClassGrades : Page
    {
        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        P31School.Classes.ExportClass ex = new P31School.Classes.ExportClass();
        public ClassGrades()
        {
            InitializeComponent();
            ClassGradesReport.ItemsSource = dc.Grade.GroupBy(u => u.Pupil.Class.ClassName).Select(g => new
            {Grade = g.Average(u => u.Grade1),ClassName = g.Key}).ToList();
        }

        private void ClassToExcel_Click(object sender, RoutedEventArgs e)
        {
            ex.ExportToExcel(ClassGradesReport, 0);
        }

        private void ClassToWord_Click(object sender, RoutedEventArgs e)
        {
            ex.ExportToWord(ClassGradesReport, 0);
        }

        private void ClassSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClassGradesReport.ItemsSource = dc.Grade.GroupBy(u => u.Pupil.Class.ClassName).Select(g => new
            { Grade = g.Average(u => u.Grade1), ClassName = g.Key }).Where(u=>(u.ClassName + " " + u.Grade).Contains(ClassSearch.Text)).ToList();
        }
    }
}
