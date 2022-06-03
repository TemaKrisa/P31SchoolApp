using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace P31School.View.Subjects
{
    /// <summary>
    /// Логика взаимодействия для SubjectPage.xaml
    /// </summary>
    public partial class SubjectPage : Page
    {
        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        public SubjectPage()
        {
            InitializeComponent();
            SubjectTable.ItemsSource = dc.Subject.ToList();
        }

        public void Update() => SubjectTable.ItemsSource = dc.Subject.Where(u => u.SubjectName.Contains(SubjectSearch.Text)).ToList();

        private void DeleteB_Click(object sender, RoutedEventArgs e)
        {
            var curItem = ((ListViewItem)SubjectTable.ContainerFromElement((Button)sender));
            SubjectTable.SelectedItem = (ListViewItem)curItem;
            Model.Subject sub = new Model.Subject();
            sub = (Model.Subject)SubjectTable.SelectedItem;
            dc.Subject.Remove(sub);
            dc.SaveChanges();
            Update();
        }

        private void ChangeB_Click(object sender, RoutedEventArgs e)
        {
            var curItem = ((ListViewItem)SubjectTable.ContainerFromElement((Button)sender));
            SubjectTable.SelectedItem = (ListViewItem)curItem;
            Model.Subject sub = new Model.Subject();
            sub = (Model.Subject)SubjectTable.SelectedItem;
            SubChange.SubjectID = sub.SubjectID;
            SubChange.SubjectName = sub.SubjectName;
            SubChange.Visibility = Visibility.Visible;
        }

        private void AddB_Click(object sender, RoutedEventArgs e)
        {
            SubAdd.Visibility = Visibility.Visible;
        }

        private void SubjectSearch_TextChanged(object sender, TextChangedEventArgs e) => SubjectTable.ItemsSource = dc.Subject.Where(u => u.SubjectName.Contains(SubjectSearch.Text)).ToList();

        private void SubChange_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (SubChange.Visibility == Visibility.Collapsed) NavigationService.Navigate(new SubjectPage());
        }

        private void SubAdd_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (SubAdd.Visibility == Visibility.Collapsed) NavigationService.Navigate(new SubjectPage());
        }
    }
}
