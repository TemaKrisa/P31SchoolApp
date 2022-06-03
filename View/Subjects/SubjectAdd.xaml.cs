using P31School.Classes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;

namespace P31School.View.Subjects
{
    public partial class SubjectAdd : UserControl
    {
        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        public SubjectAdd() => InitializeComponent();
        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) => SubjectNameBox.Text = null;
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e) => this.Visibility = Visibility.Collapsed;
        private void AddB_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SubjectNameBox.Text)) MessageBoxs.Show("Введите наименование предмета!", "Добавление предмета");
            else
            {
                var s = dc.Subject.Where(u => u.SubjectName == SubjectNameBox.Text).Count();
                if (s == 0)
                {
                    Model.Subject s1 = new Model.Subject { SubjectName = SubjectNameBox.Text };
                    dc.Subject.Add(s1);
                    dc.SaveChanges();
                    MessageBoxs.Show("Добавление прошло успешно");
                    this.Visibility = Visibility.Collapsed;
                }
                else MessageBoxs.Show("Предмет с данным наименованием уже существует");
            }
        }
    }
}
