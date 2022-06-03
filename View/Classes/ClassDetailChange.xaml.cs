using P31School.Classes;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace P31School.View.Classes
{
    public partial class ClassDetailChange : UserControl
    {
        public static readonly DependencyProperty pupName = DependencyProperty.Register("Names", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty pupSurname = DependencyProperty.Register("Surname", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty pupMidname = DependencyProperty.Register("Midname", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty classNames = DependencyProperty.Register("ClassNames", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty pupilID = DependencyProperty.Register("PupilID", typeof(int), typeof(MainWindow), new FrameworkPropertyMetadata(null));

        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        public ClassDetailChange()
        {
            InitializeComponent();
            ClassCombo.ItemsSource = dc.Class.ToList();
        }

        public string ClassNames
        {
            get { return (string)GetValue(classNames); }
            set { SetValue(classNames, value); }
        }
        public string Names
        {
            get { return (string)GetValue(pupName); }
            set { SetValue(pupName, value); }
        }
        public string Surname
        {
            get { return (string)GetValue(pupSurname); }
            set { SetValue(pupSurname, value); }
        }
        public string Midname
        {
            get { return (string)GetValue(pupMidname); }
            set { SetValue(pupMidname, value); }
        }

        public int PupilID
        {
            get { return (int)GetValue(pupilID); }
            set { SetValue(pupilID, value); }
        }

        private void ChangeB_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameBox.Text) && string.IsNullOrEmpty(SurnameBox.Text)) MessageBoxs.Show("Введите данные ученика!", "Добавление ученика");
            else if (string.IsNullOrEmpty(ClassCombo.Text)) MessageBoxs.Show("Выберите класс ученика!", "Добавление ученика");
            {
                var result = dc.Pupil.SingleOrDefault(b => b.PupilID == PupilID);
                if (result != null)
                {
                    dynamic r = ClassCombo.SelectedItem;
                    result.Surname = SurnameBox.Text;
                    result.Name = NameBox.Text;
                    result.Midname = MidnameBox.Text;
                    result.ClassID = r.ClassID;
                    dc.SaveChanges();
                    this.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void DeleteB_Click(object sender, RoutedEventArgs e)
        {
            var result = dc.Pupil.SingleOrDefault(b => b.PupilID == PupilID);
            if (result != null)
            {
                dc.Pupil.Remove(result);
                dc.SaveChanges();
            }
            this.Visibility = Visibility.Collapsed;
        }

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Collapsed)
            {
                NameBox.Text = null;
                SurnameBox.Text = null;
                MidnameBox.Text = null;
                ClassCombo.Text = ClassNames;
            }
            else
            {
                NameBox.Text = Name;
                SurnameBox.Text = Surname;
                MidnameBox.Text = Midname;
                ClassCombo.Text = ClassNames;
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e) => this.Visibility = Visibility.Collapsed;
    }
}
