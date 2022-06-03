using P31School.Classes;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace P31School.View.Classroom
{
    /// <summary>
    /// Логика взаимодействия для ClassroomChange.xaml
    /// </summary>
    public partial class ClassroomChange : UserControl
    {
        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        public static readonly DependencyProperty teachFIO = DependencyProperty.Register("TeachFIO", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty className = DependencyProperty.Register("ClassNames1", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty teacherID = DependencyProperty.Register("ID15", typeof(int), typeof(MainWindow), new FrameworkPropertyMetadata(null));

        public ClassroomChange()
        {
            InitializeComponent();
            TeacherCombo.ItemsSource = dc.Teacher.ToList();
        }

        public string ClassNames1
        {
            get { return (string)GetValue(className); }
            set { SetValue(className, value); }
        }

        public string TeachFIO
        {
            get { return (string)GetValue(teachFIO); }
            set { SetValue(teachFIO, value); }
        }        
        
        public int ID15
        {
            get { return (int)GetValue(teacherID); }
            set { SetValue(teacherID, value); }
        }

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Collapsed) ClassroomNameBox.Text = null;
            else
            {
                TeacherCombo.Text = TeachFIO;
                ClassroomNameBox.Text = ClassNames1;
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void ChangeB_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ClassroomNameBox.Text))
            {
                MessageBoxs.Show("Введите наименование класса!", "Изменение класса");
                return;
            }
            else
            {
                var w = dc.Classroom.Where(u => u.ClassroomName == ClassroomNameBox.Text).Count();
                if (ClassNames1 == ClassroomNameBox.Text)
                {
                    if (w != 1)
                    {
                        MessageBoxs.Show("Класс с данным наименованием уже существует!", "Изменение кабинета");
                        return;
                    }
                }
                else
                {
                    if (w != 0)
                    {
                        MessageBoxs.Show("Класс с данным наименованием уже существует!", "Изменение кабинета");
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(TeacherCombo.Text))
                {
                    dynamic r = TeacherCombo.SelectedItem; int teach = r.TeacherID;
                    var s = dc.Classroom.Where(u => u.TeacherID == teach).Count();
                    if (ID15 == teach)
                    {
                        if (s != 1)
                        {
                            MessageBoxs.Show("Данный преподаватель закреплён за другим предметом!", "Изменение кабинета"); return;
                        }
                    }
                    else
                    {
                        if (s > 0) { MessageBoxs.Show("Данный преподаватель закреплён за другим предметом!", "Добавление кабинета"); return; }
                    }
                    var result = dc.Classroom.SingleOrDefault(b => b.ClassroomName == ClassNames1);
                    if (result != null)
                    {
                        result.ClassroomName = ClassroomNameBox.Text;
                        result.TeacherID = teach;
                        dc.SaveChanges();
                    }
                    this.Visibility = Visibility.Collapsed;
                }
                else
                {
                    var result = dc.Classroom.SingleOrDefault(b => b.ClassroomName == ClassNames1);
                    if (result != null)
                    {
                        result.ClassroomName = ClassroomNameBox.Text;
                        result.TeacherID = null;
                        dc.SaveChanges();
                    }
                    this.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void DeleteB_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ClassroomNameBox.Text)) MessageBoxs.Show("Введите название класса!", "Изменение класса");
            var result = dc.Classroom.SingleOrDefault(b => b.ClassroomName == ClassNames1);
            if (result != null)
            {
                dc.Classroom.Remove(result);
                dc.SaveChanges();
            }
            this.Visibility = Visibility.Collapsed;
        }
    }
}
