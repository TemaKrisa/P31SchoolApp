using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace P31School.View.Profile
{
    /// <summary>
    /// Логика взаимодействия для LoginChange.xaml
    /// </summary>
    public partial class LoginChange : UserControl
    {
        public static readonly DependencyProperty userLogin = DependencyProperty.Register("UserLogin", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty userPas = DependencyProperty.Register("UserPas", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty userID = DependencyProperty.Register("UserID", typeof(int), typeof(MainWindow), new FrameworkPropertyMetadata(null));

        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        public LoginChange()
        {
            InitializeComponent();
        }

        public string UserLogin
        {
            get { return (string)GetValue(userLogin); }
            set { SetValue(userLogin, value); }
        }
        public string UserPas
        {
            get { return (string)GetValue(userPas); }
            set { SetValue(userPas, value); }
        }
        public int UserID
        {
            get { return (int)GetValue(userID); }
            set { SetValue(userID, value); }
        }

        private void AddB_Click(object sender, RoutedEventArgs e)
        {
            var result = dc.AccountTable.SingleOrDefault(b => b.UserID == UserID);
            if (result.Password == Passwords.Password)
            {
                result.Login = LoginBox.Text;
                Properties.Settings.Default.Login = LoginBox.Text;
                dc.SaveChanges();
                this.Visibility = Visibility.Collapsed;
            }
        }

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Collapsed) { LoginBox.Text = ""; Passwords.Password = ""; }
            else LoginBox.Text = UserLogin;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
