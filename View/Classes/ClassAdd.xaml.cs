using P31School.Classes;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace P31School.View.Classes
{
    public partial class ClassAdd : UserControl
    {
        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        public ClassAdd()
        {
            InitializeComponent();
            TeacherCombo.ItemsSource = dc.Teacher.ToList();
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e) => this.Visibility = Visibility.Collapsed;
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) => ClassNameBox.Text = null;
        private void AddB_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ClassNameBox.Text)) MessageBoxs.Show("Введите наименование класса!", "Добавление класса");
            else if (string.IsNullOrEmpty(TeacherCombo.Text)) MessageBoxs.Show("Выберите классного руководителя класса!", "Добавление класса");
            else
            {
                dynamic r = TeacherCombo.SelectedItem;
                int teach = r.TeacherID;
                var s = dc.Class.Where(u => u.TeacherID == teach).Count();
                if (s > 0) MessageBoxs.Show("Данный преподаватель уже является классным руководителем другого класса!", "Добавление класса");
                else
                {
                    Model.Class c1 = new Model.Class{ClassName = ClassNameBox.Text, TeacherID = r.TeacherID };
                    dc.Class.Add(c1);
                    dc.SaveChanges();
                    MessageBoxs.Show("Добавление прошло успешно");
                    this.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
