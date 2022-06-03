using System.Windows.Controls;

namespace P31School.View
{
    public partial class ReportPage : Page
    {
        public ReportPage()
        {
            InitializeComponent();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (Tabs.SelectedIndex)
            {
                case 0: ReportFrame.Navigate(new Report.ClassesReport()); break;
                case 1: ReportFrame.Navigate(new Report.SubjectReport()); break;
                case 2: ReportFrame.Navigate(new Report.PupilsReport()); break;
                case 3: ReportFrame.Navigate(new Report.TeachersReport()); break;
                case 4: ReportFrame.Navigate(new Report.ClassGrades()); break;
                case 5: ReportFrame.Navigate(new Report.ReportMain()); break;
            }
        }
    }
}
