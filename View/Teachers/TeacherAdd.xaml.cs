using P31School.Classes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace P31School.View.Teachers
{
    /// <summary>
    /// Логика взаимодействия для TeacherAdd.xaml
    /// </summary>
    public partial class TeacherAdd : UserControl
    {
        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        public TeacherAdd() => InitializeComponent();

        private void AddB_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameBox.Text) && string.IsNullOrEmpty(SurnameBox.Text))
                MessageBoxs.Show("Введите данные!", "Добавление учителя");
            else
            {
                Model.Teacher t1 = new Model.Teacher { Surname = SurnameBox.Text, Name = NameBox.Text, MidName = MidnameBox.Text };
                dc.Teacher.Add(t1);
                dc.SaveChanges();
                MessageBoxs.Show("Добавление прошло успешно");
                this.Visibility = Visibility.Collapsed;
            }
        }

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            SurnameBox.Text = null;
            MidnameBox.Text = null;
            NameBox.Text = null;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e) => this.Visibility = Visibility.Collapsed;
    }
}
