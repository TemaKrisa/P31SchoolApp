using P31School.Classes;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace P31School.View.Classes
{
    public partial class ClassDetailPage : Page
    {
        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        string cn;

        public ClassDetailPage(string ClassName)
        {
            InitializeComponent();
            cn = ClassName;
            Update();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e) =>
            PupilGrid.ItemsSource = dc.Pupil.Where(u => u.Class.ClassName == cn && (u.Surname + " " + u.Name + " " + u.Midname).Contains(SearchBox.Text)).ToList();
        public void Update() => PupilGrid.ItemsSource = dc.Pupil.Where(u => u.Class.ClassName == cn).ToList();
        private void AddButton_Click(object sender, RoutedEventArgs e) => ClassDetailAddDialog.Visibility = Visibility.Visible;

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (PupilGrid.SelectedItem != null)
            {
                dynamic r = PupilGrid.SelectedItem;
                ClassDetailChangeDialog.Surname = r.Surname;
                ClassDetailChangeDialog.Name = (string)r.Name.Trim();
                ClassDetailChangeDialog.Midname = r.Midname;
                ClassDetailChangeDialog.ClassNames = cn;
                ClassDetailChangeDialog.PupilID = r.PupilID;
                ClassDetailChangeDialog.Visibility = Visibility.Visible;
            }
            else { MessageBoxs.Show("Выберите ученика для изменения!"); }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (PupilGrid.SelectedItem != null)
            {
                dynamic r = PupilGrid.SelectedItem;
                int id = r.PupilID;
                var result = dc.Pupil.SingleOrDefault(b => b.PupilID == id);
                if (result != null)
                {
                    dc.Pupil.Remove(result);
                    dc.SaveChanges();
                    Update();
                }
            }
            else { MessageBoxs.Show("Выберите ученика для удаления!"); }
        }

        private void ExcelExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExportClass ex = new ExportClass();
                ex.ExportToExcel(PupilGrid, 1);
            }
            catch { }
        }
        private void Grades_Click(object sender, RoutedEventArgs e)
        {
            dynamic r = PupilGrid.SelectedItem;
            NavigationService.Navigate(new PupilDetailPage(r.PupilID));
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e) => NavigationService.GoBack();
        private void ClassDetailAddDialog_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (ClassDetailAddDialog.Visibility == Visibility.Collapsed) NavigationService.Navigate(new ClassDetailPage(cn));
        }
        private void ClassDetailChangeDialog_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) 
        {
            if(ClassDetailChangeDialog.Visibility == Visibility.Collapsed) NavigationService.Navigate(new ClassDetailPage(cn));
        }

        private void PupilGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (PupilGrid.SelectedItem != null)
            {
                dynamic r = PupilGrid.SelectedItem;
                ClassDetailChangeDialog.Surname = r.Surname;
                ClassDetailChangeDialog.Name = (string)r.Name.Trim();
                ClassDetailChangeDialog.Midname = r.Midname;
                ClassDetailChangeDialog.ClassNames = cn;
                ClassDetailChangeDialog.PupilID = r.PupilID;
                ClassDetailChangeDialog.Visibility = Visibility.Visible;
            }
        }

        private void WordExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExportClass ex = new ExportClass();
                ex.ExportToExcel(PupilGrid, 1);
            }
            catch { }
        }
    }
}