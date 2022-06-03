using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using P31School.Classes;

namespace P31School.View.Classes
{
    public partial class PupilDetailPageAdd : UserControl
    {
        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();

        public static readonly DependencyProperty pupilID = DependencyProperty.Register("PupilIDo", typeof(int), typeof(MainWindow), new FrameworkPropertyMetadata(null));

        public PupilDetailPageAdd()
        {
            InitializeComponent();
            SubjectCombo.ItemsSource = dc.Subject.ToList();
        }

        public int PupilIDo
        {
            get { return (int)GetValue(pupilID); }
            set { SetValue(pupilID, value); }
        }


        private void AddB_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(GradeBox.Text)) MessageBoxs.Show("Введите оценку ученика", "Добавление оценки");
            else if (string.IsNullOrEmpty(GradeDate.SelectedDate.ToString())) MessageBoxs.Show("Выберите дату оценки", "Добавление оценки");
            else if (string.IsNullOrEmpty(SubjectCombo.Text)) MessageBoxs.Show("Выберите предмет оценки", "Добавление оценки"); 
            else
            {
                dynamic r = SubjectCombo.SelectedItem;
                int g = Convert.ToInt32(GradeBox.Text);
                Model.Grade g1 = new Model.Grade() { PupilID = PupilIDo, Date = (DateTime)GradeDate.SelectedDate, SubjectID = r.SubjectID, Grade1 = g };
                dc.Grade.Add(g1);
                dc.SaveChanges();
                this.Visibility = Visibility.Collapsed;
            }
        }

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Collapsed)
            {
                GradeBox.Text = "";
                GradeDate.Text = "";
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void GradeBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
            { if (!Char.IsDigit(e.Text, 0)) e.Handled = true; }
    }
}
