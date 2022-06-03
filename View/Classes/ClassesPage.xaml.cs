using P31School.Classes;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace P31School.View.Classes
{
    /// <summary>
    /// Логика взаимодействия для ClassesPage.xaml
    /// </summary>
    public partial class ClassesPage : Page
    {
        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();

        public ClassesPage()
        {
            InitializeComponent();
            Update();
        }

        public void Update() => ListClasses.ItemsSource = dc.Class.ToList();

        private void ClassesEnter_Click(object sender, RoutedEventArgs e)
        {
            var curItem = ((ListViewItem)ListClasses.ContainerFromElement((Button)sender));
            ListClasses.SelectedItem = (ListViewItem)curItem;
            dynamic r = ListClasses.SelectedItem;
            NavigationService.Navigate(new ClassDetailPage(r.ClassName));
        }

        private void ClassesDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxs.Show("Вы хотите удалить?", "Удаление класса", MessageBoxs.Buttons.Yes_No, MessageBoxs.Icon.Info) == "1")
            {
                var curItem = ((ListViewItem)ListClasses.ContainerFromElement((Button)sender));
                ListClasses.SelectedItem = (ListViewItem)curItem;
                Model.Class cls = (Model.Class)ListClasses.SelectedItem;
                dc.Class.Remove(cls);
                dc.SaveChanges();
                Update();
            }
        }

        private void ClassesChange_Click(object sender, RoutedEventArgs e) => Changer();
        private void AddButton_Click(object sender, RoutedEventArgs e) => ClassAddDialog.Visibility = Visibility.Visible;
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e) => 
            ListClasses.ItemsSource = dc.Class.Where(u =>  (u.ClassName + " " + u.Teacher.Surname + " " + u.Teacher.Name + " " + u.Teacher.MidName).Contains(SearchBox.Text)).ToList();
        private void ListClasses_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) => Changer();

        public void Changer()
        {
            if (ListClasses.SelectedItem != null)
            {
                dynamic r = ListClasses.SelectedItem;
                ClassChangeDialog.ClassName = r.ClassName;
                ClassChangeDialog.ClassID = r.ClassID;
                ClassChangeDialog.TeacherFIO = r.Teacher.FIO;
                ClassChangeDialog.ID1 = r.TeacherID;
                ClassChangeDialog.Visibility = Visibility.Visible;
            }
        }

        private void ClassChangeDialog_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (ClassChangeDialog.Visibility == Visibility.Collapsed) NavigationService.Navigate(new ClassesPage());
        }

        private void ClassAddDialog_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (ClassAddDialog.Visibility == Visibility.Collapsed) Update();
        }
    }
}
