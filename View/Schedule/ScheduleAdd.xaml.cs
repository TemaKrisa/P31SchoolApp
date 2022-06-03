using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace P31School.View.Schedule
{
    public partial class ScheduleAdd : UserControl
    {
        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        public static readonly DependencyProperty className = DependencyProperty.Register("ClassNameq", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty weekDay = DependencyProperty.Register("WeekDay1", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        public ScheduleAdd()
        {
            InitializeComponent();
            SubjectCombo.ItemsSource = dc.Subject.ToList();
            ClassCombo.ItemsSource = dc.Class.ToList();
        }

        public string ClassNameq
        {
            get { return (string)GetValue(className); }
            set { SetValue(className, value); }
        }

        public string WeekDay1
        {
            get { return (string)GetValue(weekDay); }
            set { SetValue(weekDay, value); }
        }

        private void LessonBox_PreviewTextInput(object sender, TextCompositionEventArgs e) { if (!Char.IsDigit(e.Text, 0)) e.Handled = true; }

        private void SubjectCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic r = SubjectCombo.SelectedItem; int id = r.SubjectID;
            TeacherCombo.ItemsSource = dc.SubjectTeacherView.Where(u => u.SubjectID == id).ToList();
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

        private void AddB_Click(object sender, RoutedEventArgs e)
        {
            dynamic r = ClassroomCombo.SelectedItem; int c = r.ClassroomID;
            r = TeacherCombo.SelectedItem; int t = r.TeacherID;
            r = ClassCombo.SelectedItem; int cl = r.ClassID;
            r = SubjectCombo.SelectedItem; int s = r.SubjectID;
            int lesNum = Convert.ToInt32(LessonBox.Text);
            var counts = dc.Schedule.Where(u => u.ClassID == cl && WeekDay1 == WeekDayCombo.Text && u.LessonNumber == lesNum).Count();
            if (counts > 0) P31School.Classes.MessageBoxs.Show("Расписание на данный урок уже составлено!");
            else { var s1 = new Model.Schedule {ClassroomID = c, DayOfTheWeek = WeekDayCombo.Text.Trim(), TeacherID = t, ClassID = cl, LessonNumber = Convert.ToInt32(LessonBox.Text), SubjectID = s };
                dc.Schedule.Add(s1);
                dc.SaveChanges();
                this.Visibility = Visibility.Collapsed;
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                WeekDayCombo.Text = WeekDay1;
                ClassCombo.Text = ClassNameq;
                TeacherCombo.SelectedIndex = 0;
                LessonBox.Text = "";
            }
            else
            {

            }
        }

        private void TeacherCombo_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
