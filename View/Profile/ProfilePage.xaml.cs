using System.Windows;
using System.Windows.Controls;

namespace P31School.View.Profile
{
    public partial class ProfilePage : Page
    {
        public int userID;
        public ProfilePage()
        {
            InitializeComponent();
            LoginLabl.Text = Properties.Settings.Default.Login;
            userID = Properties.Settings.Default.UserID;
        }

        private void PasChangeB_Click(object sender, RoutedEventArgs e)
        {
            PasChangeDialog.UserID2 = userID;
            PasChangeDialog.Visibility = Visibility.Visible;
        }

        private void LogChangeB_Click(object sender, RoutedEventArgs e)
        {
            LogChangeDialog.UserID = userID;
            LogChangeDialog.UserLogin = LoginLabl.Text;
            LogChangeDialog.Visibility = Visibility.Visible;
        }

        private void LogChangeDialog_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(LogChangeDialog.Visibility == Visibility.Collapsed) LoginLabl.Text = Properties.Settings.Default.Login;
        }
    }
}
