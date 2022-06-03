using P31School.Classes;
using P31School.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace P31School.View.Classroom
{
    /// <summary>
    /// Логика взаимодействия для ClassroomPage.xaml
    /// </summary>
    public partial class ClassroomPage : Page
    {
        P31SchoolEntities dc = new P31SchoolEntities();
        public ClassroomPage()
        {
            InitializeComponent();
            ListClassrooms.ItemsSource = dc.Classroom.ToList();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)=>
            ListClassrooms.ItemsSource = dc.Classroom.Where(u => (u.ClassroomName + " " + u.Teacher.Surname + " " + u.Teacher.Name + " " + u.Teacher.MidName).Contains(SearchBox.Text)).ToList();

        private void ListClasses_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListClassrooms.SelectedItem != null)
            {
                dynamic r = ListClassrooms.SelectedItem;
                ClassroomChangeDialog.ClassNames1 = r.ClassroomName;
                try { ClassroomChangeDialog.TeachFIO = r.Teacher.FIO; ClassroomChangeDialog.ID15 = r.TeacherID; } catch { }
                ClassroomChangeDialog.Visibility = Visibility.Visible;

            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) => ClassroomAddDialog.Visibility = Visibility.Visible;
        private void ClassroomChange_Click(object sender, RoutedEventArgs e)
        {
            dynamic r = ListClassrooms.SelectedItem;
            ClassroomChangeDialog.ClassNames1 = r.ClassroomName;
            try { ClassroomChangeDialog.TeachFIO = r.Teacher.FIO; ClassroomChangeDialog.ID15 = r.TeacherID; } catch { }
            ClassroomChangeDialog.Visibility = Visibility.Visible;
        }

        private void ClassroomDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxs.Show("Вы точно хотите удалить?", "Удаление класса", MessageBoxs.Buttons.Yes_No, MessageBoxs.Icon.Info) == "1")
            {
                var curItem = ((ListViewItem)ListClassrooms.ContainerFromElement((Button)sender));
                ListClassrooms.SelectedItem = (ListViewItem)curItem;
                Model.Classroom cls = (Model.Classroom)ListClassrooms.SelectedItem;
                dc.Classroom.Remove(cls);
                dc.SaveChanges();
                NavigationService.Navigate(new ClassroomPage());
            }
        }

        private void ClassroomAddDialog_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (ClassroomAddDialog.Visibility == Visibility.Collapsed) NavigationService.Navigate(new ClassroomPage());
        }

        private void ClassroomChangeDialog_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (ClassroomChangeDialog.Visibility == Visibility.Collapsed) NavigationService.Navigate(new ClassroomPage());
        }
    }
}
