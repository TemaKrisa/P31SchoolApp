using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace P31School.View.Teachers
{
    /// <summary>
    /// Логика взаимодействия для TeacherSubjectChange.xaml
    /// </summary>
    public partial class TeacherSubjectChange : UserControl
    {
        public static readonly DependencyProperty subjectName1 = DependencyProperty.Register("SubjectName1", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty teacherID2 = DependencyProperty.Register("TeacherID2", typeof(int), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty subjectID3 = DependencyProperty.Register("SubjectID3", typeof(int), typeof(MainWindow), new FrameworkPropertyMetadata(null));

        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        public TeacherSubjectChange()
        {
            InitializeComponent();
            SubjectCombo.ItemsSource = dc.Subject.ToList();
        }

        public int TeacherID2
        {
            get { return (int)GetValue(teacherID2); }
            set { SetValue(teacherID2, value); }
        }

        public string SubjectName1
        {
            get { return (string)GetValue(subjectName1); }
            set { SetValue(subjectName1, value); }
        }

        public int SubjectID3
        {
            get { return (int)GetValue(subjectID3); }
            set { SetValue(subjectID3, value); }
        }


        private void DeleteB_Click(object sender, RoutedEventArgs e)
        {
            dc.Database.ExecuteSqlCommand($"Delete From SubjectTeacher Where SubjectID = {SubjectID3} And TeacherID = {TeacherID2}");
            this.Visibility = Visibility.Collapsed;
        }

        private void ChangeB_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dynamic r = SubjectCombo.SelectedItem;
                dc.Database.ExecuteSqlCommand($"Update SubjectTeacher Set SubjectID = {r.SubjectID} Where SubjectID = {SubjectID3} And TeacherID = {TeacherID2}");
                this.Visibility = Visibility.Collapsed;
            }
            catch
            {
                P31School.Classes.MessageBoxs.Show("Вы выбрали неверные данные", "Изменение предмета");
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible) SubjectCombo.Text = SubjectName1;
        }
    }
}
