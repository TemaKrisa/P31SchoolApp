using P31School.Classes;
using System;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace P31School.View.Authorization
{
    public partial class LoginPage : Page, INotifyPropertyChanged
    {
        DateTime dt2 = new DateTime();

        public LoginPage()
        {
            InitializeComponent();
            this.DataContext = this;

        }
        #region Timer
        public event PropertyChangedEventHandler PropertyChanged;
        public string Time
        {
            get
            {
                DateTime dt1 = DateTime.Now;
                TimeSpan ts = dt2 - dt1;
                if (ts.Seconds > 0)
                {
                    LoginButton.IsEnabled = false;
                    return string.Format("Осталось {0} мин {1}сек", ts.Minutes, ts.Seconds);
                }
                else
                {
                    LoginButton.IsEnabled = true;
                    return string.Format("С возвращением!");
                }
            }
        }

        public void AddDelay(int sec) => dt2 = DateTime.Now.AddSeconds(sec);

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Timer time = new Timer();
            time.Interval = 1000;
            time.Elapsed += Timer_Elapsed;
            time.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e) => PropertyChange("Time");

        private void PropertyChange(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(LoginBox.Text) && string.IsNullOrEmpty(Passwords.Password)) MessageBoxs.Show("Введите данные", "Авторизация", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);
            else
            {
                CaptchCheckDialog.Visibility = Visibility.Visible;
                CaptchCheckDialog.Log2 = LoginBox.Text;
                CaptchCheckDialog.Pas2 = Passwords.Password;
                CaptchCheckDialog.LogFailed = false;
            }
        }

        private void CaptchCheckDialog_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (CaptchCheckDialog.LogFailed) AddDelay(30);
        }
    }
}   
