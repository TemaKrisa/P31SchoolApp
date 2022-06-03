using P31School.Classes;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace P31School.View.Classes
{

    public partial class ClassChange : UserControl
    {
        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        public static readonly DependencyProperty className = DependencyProperty.Register("ClassName", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty teacherFIO = DependencyProperty.Register("TeacherFIO", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty teacherID = DependencyProperty.Register("ID1", typeof(int), typeof(MainWindow), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty classID = DependencyProperty.Register("ClassID", typeof(int), typeof(MainWindow), new FrameworkPropertyMetadata(null));


        public ClassChange()
        {
            InitializeComponent();
            TeacherCombo.ItemsSource = dc.Teacher.ToList();
        }

        public string ClassName
        {
            get { return (string)GetValue(className); }
            set { SetValue(className, value); }
        }
        public string TeacherFIO
        {
            get { return (string)GetValue(teacherFIO); }
            set { SetValue(teacherFIO, value); }
        }

        public int ClassID
        {
            get { return (int)GetValue(classID); }
            set { SetValue(classID, value); }
        }        
        public int ID1
        {
            get { return (int)GetValue(teacherID); }
            set { SetValue(teacherID, value); }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e) => this.Visibility = Visibility.Collapsed;

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible) { ClassNameBox.Text = ClassName; TeacherCombo.Text = TeacherFIO; }
            else ClassNameBox.Text = null;
        }

        private void ChangeB_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ClassNameBox.Text)) MessageBoxs.Show("Введите название класса!", "Изменение класса");
            else
            {
                dynamic r = TeacherCombo.SelectedItem;
                string cn = ClassNameBox.Text;
                int teach = r.TeacherID;
                int q = dc.Class.Where(u => u.TeacherID == teach).Count();
                if (ClassNameBox.Text != ClassName)
                {
                    var w = dc.Class.Where(t => t.ClassName == cn).Count();
                    if (w != 0) { MessageBoxs.Show("Класс с таким наименованием уже существует!"); return; }
                }
                if (teach == ID1)
                {
                    if (q != 1)
                    {
                        MessageBoxs.Show("Данный преподаватель уже является классным руководителем другого класса!", "Изменение класса");
                        return;
                    }
                }
                else if (q != 0)
                {
                    MessageBoxs.Show("Данный преподаватель уже является классным руководителем другого класса!", "Изменение класса");
                    return;
                }
                var result = dc.Class.SingleOrDefault(b => b.ClassID == ClassID);
                if (result != null)
                {
                    result.ClassName = ClassNameBox.Text;
                    result.TeacherID = r.TeacherID;
                    dc.SaveChanges();
                }
                this.Visibility = Visibility.Collapsed;
            }
        }

        private void DeleteB_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ClassNameBox.Text)) MessageBoxs.Show("Введите название класса!", "Удаление класса");
            var result = dc.Class.SingleOrDefault(b => b.ClassID == ClassID);
            if (result != null)
            {
                dc.Class.Remove(result);
                dc.SaveChanges();
            }
            this.Visibility = Visibility.Collapsed;
        }
    }
}
