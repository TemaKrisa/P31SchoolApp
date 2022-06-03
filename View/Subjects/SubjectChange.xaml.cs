using P31School.Classes;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace P31School.View.Subjects
{
    /// <summary>
    /// Логика взаимодействия для SubjectChange.xaml
    /// </summary>
    public partial class SubjectChange : UserControl
    {
        public static readonly DependencyProperty subjectName = DependencyProperty.Register("SubjectName", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty subjectID = DependencyProperty.Register("SubjectID", typeof(int), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        public SubjectChange() => InitializeComponent();

        public string SubjectName
        {
            get { return (string)GetValue(subjectName); }
            set { SetValue(subjectName, value); }
        }

        public int SubjectID
        {
            get { return (int)GetValue(subjectID); }
            set { SetValue(subjectID, value); }
        }

        private void DeleteB_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SubjectNameBox.Text)) MessageBoxs.Show("Введите название предмета!", "Удаление предмета");

            var result = dc.Subject.SingleOrDefault(b => b.SubjectName == SubjectName);
            if (result != null)
            {
                dc.Subject.Remove(result);
                dc.SaveChanges();
            }

            this.Visibility = Visibility.Collapsed;
        }

        private void ChangeB_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SubjectNameBox.Text)) { MessageBoxs.Show("Введите название предмета!", "Изменение предмета"); return; }
            var s = dc.Subject.Where(u => u.SubjectName == SubjectNameBox.Text).Count();
            if (SubjectName == SubjectNameBox.Text)
            {
                if (s != 1)
                {
                    MessageBoxs.Show("Предмет с данным наименованием уже существует!", "Изменение предмета"); return;
                }
            }
            else
            {
                if (s != 0)
                {
                    MessageBoxs.Show("Предмет с данным наименованием уже существует!", "Изменение предмета"); return;
                }
            }
            var result = dc.Subject.SingleOrDefault(b => b.SubjectName == SubjectName);
            if (result != null)
            {
                result.SubjectName = SubjectNameBox.Text;
                dc.SaveChanges();
            }
            this.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e) => this.Visibility = Visibility.Collapsed;

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible) SubjectNameBox.Text = SubjectName;
            else SubjectNameBox.Text = null;
        }
    }
}
