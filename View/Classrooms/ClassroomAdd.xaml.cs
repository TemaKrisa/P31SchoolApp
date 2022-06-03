using P31School.Classes;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace P31School.View.Classroom
{
    public partial class ClassroomAdd : UserControl
    {
        P31School.Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        public ClassroomAdd()
        {
            InitializeComponent();
            TeacherCombo.ItemsSource = dc.Teacher.ToList();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e) => this.Visibility = Visibility.Collapsed;
        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Collapsed) ClassNameBox = null;
        }

        private void AddB_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ClassNameBox.Text)) MessageBoxs.Show("Введите наименование класса!", "Добавление кабинета");
            else
            {
                var w = dc.Classroom.Where(u => u.ClassroomName == ClassNameBox.Text).Count();
                if (w > 0) MessageBoxs.Show("Класс с данным наименованием уже существует!", "Добавление кабинета");
                else
                {
                    if (!string.IsNullOrEmpty(TeacherCombo.Text))
                    {
                        dynamic r = TeacherCombo.SelectedItem; int teach = r.TeacherID;
                        var s = dc.Class.Where(u => u.TeacherID == teach).Count();
                        if (s > 0) { MessageBoxs.Show("Данный преподаватель закреплён за другим предметом!", "Добавление кабинета"); return; }
                        Model.Classroom c1 = new Model.Classroom { ClassroomName = ClassNameBox.Text, TeacherID = r.TeacherID };
                        dc.Classroom.Add(c1);
                    }
                    else 
                    {
                        Model.Classroom c1 = new Model.Classroom { ClassroomName = ClassNameBox.Text };
                        dc.Classroom.Add(c1);
                    }
                    dc.SaveChanges();
                    MessageBoxs.Show("Добавление прошло успешно");
                    this.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
