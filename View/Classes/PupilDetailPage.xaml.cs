using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using P31School.Classes;

namespace P31School.View.Classes
{
    public partial class PupilDetailPage : Page
    {
        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        ExportClass ex = new ExportClass();
        int pup;
        public PupilDetailPage(int pupilID)
        {
            InitializeComponent();
            PupilGrid.ItemsSource = dc.Grade.Where(u => u.PupilID == pupilID).ToList();
            pup = pupilID;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddDialog.PupilIDo = pup;
            AddDialog.Visibility = Visibility.Visible;
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if(PupilGrid.SelectedItem != null)
            {
                Model.Grade r = (Model.Grade)PupilGrid.SelectedItem;
                ChangeDialog.GradeData = r;
                ChangeDialog.Visibility = Visibility.Visible;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (PupilGrid.SelectedItem != null)
            {
                Model.Grade r = (Model.Grade)PupilGrid.SelectedItem;
                var del = dc.Grade.SingleOrDefault(u=> u.GradeID == r.GradeID);
                dc.Grade.Remove(del);
                dc.SaveChanges();
                PupilGrid.ItemsSource = dc.Grade.Where(u => u.PupilID == pup).ToList();
            }
        }

        private void ExcelExport_Click(object sender, RoutedEventArgs e) => ex.ExportToExcel(PupilGrid, 0);

        private void BackBtn_Click(object sender, RoutedEventArgs e) => NavigationService.GoBack();

        private void WordExport_Click(object sender, RoutedEventArgs e) => ex.ExportToWord(PupilGrid, 0);

        private void AddDialog_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) =>
            PupilGrid.ItemsSource = dc.Grade.Where(u => u.PupilID == pup).ToList();

        private void ChangeDialog_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) =>
            PupilGrid.ItemsSource = dc.Grade.Where(u => u.PupilID == pup).ToList();

        private void PupilGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (PupilGrid.SelectedItem != null)
            {
                Model.Grade r = (Model.Grade)PupilGrid.SelectedItem;
                ChangeDialog.GradeData = r;
                ChangeDialog.Visibility = Visibility.Visible;
            }
        }
    }
}
