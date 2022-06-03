using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace P31School.View.Teachers
{
    public partial class TeacherSubjectAdd : UserControl
    {
        public static readonly DependencyProperty teacherID1 = DependencyProperty.Register("TeacherID1", typeof(int), typeof(MainWindow), new FrameworkPropertyMetadata(null));

        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        public TeacherSubjectAdd()
        {
            InitializeComponent();
            SubjectCombo.ItemsSource = dc.Subject.ToList();
        }

        public int TeacherID1
        {
            get { return (int)GetValue(teacherID1); }
            set { SetValue(teacherID1, value); }
        }


        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void AddB_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectCombo.SelectedItem != null)
            {
                try
                {
                    dynamic r = SubjectCombo.SelectedItem;
                    dc.Database.ExecuteSqlCommand($"Insert into SubjectTeacher(SubjectID,TeacherID) Values ({r.SubjectID},{TeacherID1})");
                    dc.SaveChanges();
                    this.Visibility = Visibility.Collapsed;
                }
                catch
                {
                    P31School.Classes.MessageBoxs.Show("Вы ввели неверные данные", "Добавление предмета");
                }
            }
        }
    }
}
