using P31School.Classes;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace P31School.View.Authorization
{
    public partial class CaptchChecker : UserControl
    {
        Model.P31SchoolEntities dc = new Model.P31SchoolEntities(); Random r = new Random(); string b;
        public static readonly DependencyProperty Login_Prop = DependencyProperty.Register("Log2", typeof(string), typeof(CaptchChecker), new PropertyMetadata(" "));
        public static readonly DependencyProperty Pas_Prop = DependencyProperty.Register("Pas2", typeof(string), typeof(CaptchChecker), new PropertyMetadata(" "));
        public static readonly DependencyProperty LogFailed_Prop = DependencyProperty.Register("LogFailed", typeof(bool), typeof(CaptchChecker), new PropertyMetadata(false));
        public CaptchChecker() => InitializeComponent();

        public string Log2
        {
            get { return (string)GetValue(Login_Prop); }
            set { SetValue(Login_Prop, value); }
        }

        public string Pas2
        {
            get { return (string)GetValue(Pas_Prop); }
            set { SetValue(Pas_Prop, value); }
        }

        public bool LogFailed
        {
            get { return (bool)GetValue(LogFailed_Prop); }
            set { SetValue(LogFailed_Prop, value); }
        }

        private void EnterCaptch_Click(object sender, RoutedEventArgs e)
        {
            NavigationService svc = NavigationService.GetNavigationService(this.EnterCaptch);

            if (CaptchBox.Text != b) MessageBoxs.Show("Капча введена неверно", "Ошибка ввода", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);
            else
            {
                Model.AccountTable userObj = dc.AccountTable.Where(x => x.Login == Log2.Trim() && x.Password == Pas2.Trim()).FirstOrDefault();
                if (userObj == null)
                {
                    MessageBoxs.Show("Такого пользователя не существует!", "Авторизация", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);
                    LogFailed = true;
                    this.Visibility = Visibility.Hidden;
                }
                else
                {
                    P31School.Properties.Settings.Default.Login = Log2;
                    P31School.Properties.Settings.Default.UserID = userObj.UserID;
                    P31School.Properties.Settings.Default.Save();
                    this.Visibility = Visibility.Hidden;
                    svc.Navigate(new MainPage());
                }
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Update(); CaptchBox.Text = "";
        }

        public void Update()
        {
            SPanelSymbols.Children.Clear();
            CanvasNoise.Children.Clear();
            GenerateSymbols(4);
            GenerateNoise(15);
        }

        private void GenerateSymbols(int count)
        {
            b = "";
            string alphabet = "QWERTYUIPASDFGHJKLZXCVBNM23456789";
            for (int i = 0; i < count; i++)
            {
                string symbol = alphabet.ElementAt(r.Next(0, alphabet.Length)).ToString();
                TextBlock lbl = new TextBlock();
                lbl.Text = symbol;
                lbl.FontSize = r.Next(40, 80);
                lbl.RenderTransform = new RotateTransform(r.Next(-45, 45));
                lbl.Margin = new Thickness(20, 0, 20, 0);
                SPanelSymbols.Children.Add(lbl);
                b += symbol;
            }
        }


        private void GenerateNoise(int volumeNoise)
        {
            for (int i = 0; i < volumeNoise; i++)
            {
                Border border = new Border(); border.Background = new SolidColorBrush(Color.FromArgb((byte)r.Next(100, 200), (byte)r.Next(0, 256), (byte)r.Next(0, 256), (byte)r.Next(0, 256)));
                border.Height = r.Next(2, 10); border.Width = r.Next(10, 150);
                border.RenderTransform = new RotateTransform(r.Next(0, 360));
                CanvasNoise.Children.Add(border);
                Canvas.SetLeft(border, r.Next(0, 300)); Canvas.SetTop(border, r.Next(0, 150));
                Ellipse ellipse = new Ellipse();
                ellipse.Fill = new SolidColorBrush(Color.FromArgb((byte)r.Next(100, 200), (byte)r.Next(0, 256), (byte)r.Next(0, 256), (byte)r.Next(0, 256)));
                ellipse.Height = ellipse.Width = r.Next(20, 40);
                CanvasNoise.Children.Add(ellipse); Canvas.SetLeft(ellipse, r.Next(0, 200)); Canvas.SetTop(ellipse, r.Next(0, 100));
            }
        }

        private void UpdateCaptch_Click(object sender, RoutedEventArgs e) => Update();

        private void Button_Click(object sender, RoutedEventArgs e) => this.Visibility = Visibility.Collapsed;
    }
}
