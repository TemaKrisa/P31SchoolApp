using System.Windows;
using System.Windows.Controls;

namespace P31School.View
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ClassButton_Checked(object sender, RoutedEventArgs e) => MainFrm.Navigate(new View.Classes.ClassesPage());
        private void AboutProgram_Checked(object sender, RoutedEventArgs e) => MainFrm.Navigate(new AboutProgramPage());
        private void TeacherButton_Checked(object sender, RoutedEventArgs e) => MainFrm.Navigate(new Teachers.TeachersPage());
        private void SubjectButton_Checked(object sender, RoutedEventArgs e) => MainFrm.Navigate(new Subjects.SubjectPage());
        private void ClassroomButton_Checked(object sender, RoutedEventArgs e) => MainFrm.Navigate(new Classroom.ClassroomPage());
        private void GraphicsButton_Checked(object sender, RoutedEventArgs e) => MainFrm.Navigate(new GraphicsPage());
        private void ReportButton_Checked(object sender, RoutedEventArgs e) => MainFrm.Navigate(new ReportPage());
        private void ScheduleButton_Checked(object sender, RoutedEventArgs e) => MainFrm.Navigate(new Schedule.SchedulePage());
        private void ProfileButton_Checked(object sender, RoutedEventArgs e) { MainFrm.Navigate(new Profile.ProfilePage()); }
    }
}
