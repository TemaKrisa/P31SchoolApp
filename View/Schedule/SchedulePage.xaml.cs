using P31School.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using P31School.Classes;

namespace P31School.View.Schedule
{
    /// <summary>
    /// Логика взаимодействия для SchedulePage.xaml
    /// </summary>
    public partial class SchedulePage : Page
    {
        ExportClass ex = new ExportClass();
        P31SchoolEntities dc = new P31SchoolEntities();
        string teach, weekd;
        public SchedulePage()
        {
            InitializeComponent();
            ClassCombo.ItemsSource = dc.Class.ToList();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ScheAddDialog.WeekDay1 = WeekDayCombo.Text;
            ScheAddDialog.ClassNameq = ClassCombo.Text;
            ScheAddDialog.Visibility = Visibility.Visible;
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if(ScheduleGrid.SelectedItem != null)
            {
                Model.Schedule sche = (Model.Schedule)ScheduleGrid.SelectedItem;
                ScheChangeDialog.Schedule = sche;
                ScheChangeDialog.Visibility = Visibility.Visible;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ScheduleGrid.SelectedItem != null)
            {
                Model.Schedule tch = (Model.Schedule)ScheduleGrid.SelectedItem;
                dc.Schedule.Remove(tch);
                dc.SaveChanges();
                Update();
            }
        }

        private void BackBtn_Checked(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ClassCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ExcelExport_Click(object sender, RoutedEventArgs e)
        {
            ex.ExportToExcel(ScheduleGrid, 0);
        }

        private void WordExport_Click(object sender, RoutedEventArgs e)
        {
            ex.ExportToWord(ScheduleGrid, 0);
        }

        public void Update()
        {
            ScheduleGrid.ItemsSource = dc.Schedule.Where(u => u.DayOfTheWeek == weekd && u.Class.ClassName == teach).ToList();
        }
        private void ScheAddDialog_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (ScheAddDialog.Visibility == Visibility.Collapsed)
            {
                NavigationService.Navigate(new SchedulePage());
            }
        }

        private void ScheChangeDialog_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (ScheChangeDialog.Visibility == Visibility.Collapsed)
            {
                NavigationService.Navigate(new SchedulePage());
            }
        }

        private void ScheduleGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ScheduleGrid.SelectedItem != null)
            {
                Model.Schedule sche = (Model.Schedule)ScheduleGrid.SelectedItem;
                ScheChangeDialog.Schedule = sche;
                ScheChangeDialog.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ClassCombo.Text) && string.IsNullOrEmpty(WeekDayCombo.Text)) { }
            else
            {
                dynamic r = ClassCombo.SelectedItem; string cls = r.ClassName;
                ScheduleGrid.ItemsSource = dc.Schedule.Where(u => u.DayOfTheWeek == WeekDayCombo.Text && u.Class.ClassName == cls).ToList();
                teach = cls; weekd = WeekDayCombo.Text;
            }
        }
    }
}
