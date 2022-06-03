using P31School.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace P31School.View.Teachers
{
    /// <summary>
    /// Логика взаимодействия для TeachersPage.xaml
    /// </summary>
    public partial class TeachersPage : Page
    {
        P31SchoolEntities dc = new P31SchoolEntities();
        P31School.Classes.ExportClass ex = new P31School.Classes.ExportClass();
        public TeachersPage()
        {
            InitializeComponent();
            Update();
        }

        public void Update()
        {
            TeachersData.ItemsSource = dc.Teacher.ToList();
        }
        private void TeachersData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (TeachersData.SelectedItem != null)
            {
                dynamic r = TeachersData.SelectedItem;
                TeacherChangeDialog.TeacherSurname = r.Surname;
                TeacherChangeDialog.TeacherName = r.Name;
                TeacherChangeDialog.TeacherMidname = r.MidName;
                TeacherChangeDialog.TeacherID = r.TeacherID;
                TeacherChangeDialog.Visibility = Visibility.Visible;
            }
        }

        private void SelectB_Click(object sender, RoutedEventArgs e)
        {
            dynamic r = TeachersData.SelectedItem;
            NavigationService.Navigate(new TeacherSubjects(r.TeacherID));
        }

        private void SubjectSearch_TextChanged(object sender, TextChangedEventArgs e) =>
          TeachersData.ItemsSource = dc.Teacher.Where(u => (u.Surname + " " + u.Name + " " + u.MidName).Contains(TeacherSearch.Text)).ToList();

        private void AddB_Click(object sender, RoutedEventArgs e) => TeacherAddDialog.Visibility = Visibility.Visible;


        private void TeacherAddDialog_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (TeacherAddDialog.Visibility == Visibility.Collapsed) NavigationService.Navigate(new TeachersPage());
        }

        private void TeacherChangeDialog_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (TeacherChangeDialog.Visibility == Visibility.Collapsed) NavigationService.Navigate(new TeachersPage());
        }

        private void DeleteB_Click(object sender, RoutedEventArgs e)
        {
            if (TeachersData.SelectedItem != null)
            {
                Model.Teacher tch = (Model.Teacher)TeachersData.SelectedItem;
                dc.Teacher.Remove(tch);
                dc.SaveChanges();
                Update();
            }
        }

        private void ChangeB_Click(object sender, RoutedEventArgs e)
        {
            if(TeachersData.SelectedItem != null)
            {
                dynamic r = TeachersData.SelectedItem;
                TeacherChangeDialog.TeacherSurname = r.Surname;
                TeacherChangeDialog.TeacherName = r.Name;
                TeacherChangeDialog.TeacherMidname = r.MidName;
                TeacherChangeDialog.TeacherID = r.TeacherID;
                TeacherChangeDialog.Visibility = Visibility.Visible;
            }
        }
        private void ExcelExport_Click(object sender, RoutedEventArgs e) => ex.ExportToExcel(TeachersData, 1);
        private void WordExport_Click(object sender, RoutedEventArgs e) => ex.ExportToWord(TeachersData, 1);
    }
}
