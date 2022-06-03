using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace P31School.View.Schedule
{
    /// <summary>
    /// Логика взаимодействия для ScheduleChange.xaml
    /// </summary>
    public partial class ScheduleChange : UserControl
    {
        public static readonly DependencyProperty schedule = DependencyProperty.Register("Schedule", typeof(Model.Schedule), typeof(MainWindow), new FrameworkPropertyMetadata(null));

        P31School.Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        public ScheduleChange()
        {
            InitializeComponent();
            SubjectCombo.ItemsSource = dc.Subject.ToList();
            ClassCombo.ItemsSource = dc.Class.ToList();
        }

        public Model.Schedule Schedule
        {
            get { return (Model.Schedule)GetValue(schedule); }
            set { SetValue(schedule, value); }
        }

        private void Grid_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            LessonBox.Text = Schedule.LessonNumber.ToString();
            SubjectCombo.Text = Schedule.Subject.SubjectName;
            TeacherCombo.Text = Schedule.Teacher.FIO;
            ClassroomCombo.Text = Schedule.Classroom.ClassroomName;
            ClassCombo.Text = Schedule.Class.ClassName;
            WeekDayCombo.Text = Schedule.DayOfTheWeek;
        }

        private void DeleteB_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var result = dc.Schedule.SingleOrDefault(b => b.ScheduleID == Schedule.ScheduleID);
            if (result != null)
            {
                dc.Schedule.Remove(result);
                dc.SaveChanges();
            }
        }

        private void ChangeB_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var result = dc.Schedule.SingleOrDefault(b => b.ScheduleID == Schedule.ScheduleID);
            if (result != null)
            {
                dynamic r = ClassCombo.SelectedItem;
                dynamic w = ClassroomCombo.SelectedItem;
                dynamic y = SubjectCombo.SelectedItem;
                dynamic t = TeacherCombo.SelectedItem;
                result.LessonNumber = Convert.ToInt32(LessonBox.Text);
                result.ClassID = r.ClassID;
                result.DayOfTheWeek = WeekDayCombo.Text;
                result.ClassroomID = w.ClassroomID;
                result.SubjectID = y.SubjectID;
                result.TeacherID = t.TeacherID;
                dc.SaveChanges();
                this.Visibility = Visibility.Collapsed;
            }
        }

        private void LessonBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e) { if (!Char.IsDigit(e.Text, 0)) e.Handled = true; }

        private void SubjectCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic r = SubjectCombo.SelectedItem; int id = r.SubjectID;
            TeacherCombo.ItemsSource = dc.SubjectTeacherView.Where(u => u.SubjectID == id).ToList();
        }

        private void TeacherCombo_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {

        }

        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void TeacherCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                dynamic r = TeacherCombo.SelectedItem; int id = r.TeacherID;
                var list = dc.Classroom.SingleOrDefault(u => u.TeacherID == id);
                ClassroomCombo.ItemsSource = dc.Classroom.ToList();
                ClassroomCombo.Text = list.ClassroomName;
            }
            catch
            {

            }
        }
    }
}
