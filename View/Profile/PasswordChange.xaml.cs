using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace P31School.View.Profile
{
    /// <summary>
    /// Логика взаимодействия для PasswordChange.xaml
    /// </summary>
    public partial class PasswordChange : UserControl
    {
        public static readonly DependencyProperty userID2 = DependencyProperty.Register("UserID2", typeof(int), typeof(MainWindow), new FrameworkPropertyMetadata(null));

        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        public PasswordChange()
        {
            InitializeComponent();
        }

        public int UserID2
        {
            get { return (int)GetValue(userID2); }
            set { SetValue(userID2, value); }
        }

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Collapsed)
            {
                CurPas.Password = "";
                NewPas.Password = "";
                ConfirmNewPas.Password = "";
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void AddB_Click(object sender, RoutedEventArgs e)
        {
            var result = dc.AccountTable.SingleOrDefault(b => b.UserID == UserID2);
            if (NewPas.Password != ConfirmNewPas.Password) P31School.Classes.MessageBoxs.Show("Пароли не совпадают!", "Изменение пароля");
            else if (result.Password == CurPas.Password)
            {
                result.Password = NewPas.Password;
                dc.SaveChanges();
                this.Visibility = Visibility.Collapsed;
            }
        }
    }
}
