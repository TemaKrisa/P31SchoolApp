using P31School.Classes;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace P31School.View.Classes
{
    public partial class ClassDetailAdd : UserControl
    {
        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        public ClassDetailAdd()
        {
            InitializeComponent();
            ClassCombo.ItemsSource = dc.Class.ToList();
        }

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            SurnameBox.Text = null;
            NameBox.Text = null;
            MidnameBox.Text = null;
        }

        private void AddB_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameBox.Text) && string.IsNullOrEmpty(SurnameBox.Text)) MessageBoxs.Show("Введите данные ученика!", "Добавление ученика");
            else if(string.IsNullOrEmpty(ClassCombo.Text)) MessageBoxs.Show("Выберите класс ученика!", "Добавление ученика");
            else
            {
                dynamic r = ClassCombo.SelectedItem;
                int id = r.ClassID;
                Model.Pupil p1 = new Model.Pupil { ClassID = id, Name = NameBox.Text, Midname = MidnameBox.Text, Surname = SurnameBox.Text };
                dc.Pupil.Add(p1);
                dc.SaveChanges();
                MessageBoxs.Show("Добавление прошло успешно");
                this.Visibility = Visibility.Collapsed;
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e) => this.Visibility = Visibility.Collapsed;
    }
}
