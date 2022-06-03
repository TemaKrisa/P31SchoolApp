using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using P31School.Classes;

namespace P31School.View.Classes
{
    public partial class PupilDetailChange : UserControl
    {
        public static readonly DependencyProperty gradeData = DependencyProperty.Register("GradeData", typeof(Model.Grade), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        public PupilDetailChange()
        {
            InitializeComponent();
            SubjectCombo.ItemsSource = dc.Subject.ToList();
        }

        public Model.Grade GradeData
        {
            get { return (Model.Grade)GetValue(gradeData); }
            set { SetValue(gradeData, value); }
        }

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            SubjectCombo.Text = GradeData.Subject.SubjectName;
            GradeBox.Text = GradeData.Grade1.ToString();
            GradeDate.SelectedDate = GradeData.Date;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void ChangeB_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(GradeBox.Text)) MessageBoxs.Show("Введите оценку ученика", "Добавление оценки");
            else if (string.IsNullOrEmpty(GradeDate.SelectedDate.ToString())) MessageBoxs.Show("Выберите дату оценки", "Добавление оценки");
            else if (string.IsNullOrEmpty(SubjectCombo.Text)) MessageBoxs.Show("Выберите предмет оценки", "Добавление оценки");
            else
            {
                var result = dc.Grade.SingleOrDefault(b => b.GradeID == GradeData.GradeID);
                if (result != null)
                {
                    dynamic r = SubjectCombo.SelectedItem;
                    result.Grade1 = Convert.ToInt32(GradeBox.Text);
                    result.PupilID = GradeData.PupilID;
                    result.Date = (DateTime)GradeDate.SelectedDate;
                    result.SubjectID = r.SubjectID;
                    dc.SaveChanges();
                }
                this.Visibility = Visibility.Collapsed;
            }
        }

        private void DeleteB_Click(object sender, RoutedEventArgs e)
        {
            var list = dc.Grade.SingleOrDefault(u => u.GradeID == GradeData.GradeID);
            dc.Grade.Remove(list);
            dc.SaveChanges();
            this.Visibility = Visibility.Collapsed;
        }

        private void GradeBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        { if (!Char.IsDigit(e.Text, 0)) e.Handled = true; }
    }
}
