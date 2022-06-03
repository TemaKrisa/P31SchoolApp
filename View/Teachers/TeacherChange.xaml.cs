using P31School.Classes;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace P31School.View.Teachers
{
    /// <summary>
    /// Логика взаимодействия для TeacherChange.xaml
    /// </summary>
    public partial class TeacherChange : UserControl
    {
        public static readonly DependencyProperty teacherSurname = DependencyProperty.Register("TeacherSurname", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty teacherMidname = DependencyProperty.Register("TeacherMidname", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty teacherName = DependencyProperty.Register("TeacherName", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty teacherID = DependencyProperty.Register("TeacherID", typeof(int), typeof(MainWindow), new FrameworkPropertyMetadata(null));

        public TeacherChange() => InitializeComponent();

        public string TeacherName
        {
            get { return (string)GetValue(teacherName); }
            set { SetValue(teacherName, value); }
        }
        public string TeacherSurname
        {
            get { return (string)GetValue(teacherSurname); }
            set { SetValue(teacherSurname, value); }
        }
        public string TeacherMidname
        {
            get { return (string)GetValue(teacherMidname); }
            set { SetValue(teacherMidname, value); }
        }
        public int TeacherID
        {
            get { return (int)GetValue(teacherID); }
            set { SetValue(teacherID, value); }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e) => this.Visibility = Visibility.Collapsed;

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                NameBox.Text = TeacherName;
                SurnameBox.Text = TeacherSurname;
                MidnameBox.Text = TeacherMidname;
            }
            else
            {
                NameBox.Text = null;
                SurnameBox.Text = null;
                MidnameBox.Text = null;
            }

        }

        private void ChangeB_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SurnameBox.Text) && string.IsNullOrEmpty(NameBox.Text) && string.IsNullOrEmpty(MidnameBox.Text)) MessageBoxs.Show("Введите фио преподавателя!", "Изменение преподавателя");
            using (var db = new Model.P31SchoolEntities())
            {
                var result = db.Teacher.SingleOrDefault(b => b.TeacherID == TeacherID);
                if (result != null)
                {
                    result.Name = NameBox.Text;
                    result.Surname = SurnameBox.Text;
                    result.MidName = MidnameBox.Text;
                    db.SaveChanges();
                }
            }
            this.Visibility = Visibility.Collapsed;
        }

        private void DeleteB_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new Model.P31SchoolEntities())
            {
                var result = db.Teacher.SingleOrDefault(b => b.TeacherID == TeacherID);
                if (result != null)
                {
                    db.Teacher.Remove(result);
                    db.SaveChanges();
                }
            }
            this.Visibility = Visibility.Collapsed;
        }
    }
}
