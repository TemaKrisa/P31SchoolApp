using P31School.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using P31School.Classes;

namespace P31School.View.Teachers
{
    public partial class TeacherSubjects : Page
    {
        P31SchoolEntities dc = new P31SchoolEntities();
        int id;
        ExportClass ex = new ExportClass();
        public TeacherSubjects(int q)
        {
            InitializeComponent();
            TSData.ItemsSource = dc.SubjectTeacherView.Where(u => u.TeacherID == q).ToList();
            id = q;
        }

        private void TSData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TSChangeDialog.TeacherID2 = id;
            dynamic r = TSData.SelectedItem;
            TSChangeDialog.SubjectID3 = r.SubjectID;
            TSChangeDialog.SubjectName1 = r.SubjectName;
            TSChangeDialog.Visibility = Visibility.Visible;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TSData.ItemsSource = dc.SubjectTeacherView.Where(u => u.TeacherID == id && u.SubjectName.Contains(SearchBoxs.Text)).ToList();
        }

        private void AddB_Click(object sender, RoutedEventArgs e)
        {
            TSAddDialog.TeacherID1 = id;
            TSAddDialog.Visibility = Visibility.Visible;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e) => NavigationService.GoBack();

        private void TeacherSubjectAdd_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) => 
            TSData.ItemsSource = dc.SubjectTeacherView.Where(u => u.TeacherID == id).ToList();

        private void TeacherSubjectChange_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) => 
            TSData.ItemsSource = dc.SubjectTeacherView.Where(u => u.TeacherID == id).ToList();

        private void WordExport_Click(object sender, RoutedEventArgs e) => ex.ExportToWord(TSData, 0);
        private void ExcelExport_Click(object sender, RoutedEventArgs e) => ex.ExportToExcel(TSData, 0);
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            TSChangeDialog.TeacherID2 = id;
            dynamic r = TSData.SelectedItem;
            TSChangeDialog.SubjectID3 = r.SubjectID;
            TSChangeDialog.SubjectName1 = r.SubjectName;
            TSChangeDialog.Visibility = Visibility.Visible;
        }
    }
}
