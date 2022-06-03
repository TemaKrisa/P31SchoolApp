using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace P31School.View
{
    public partial class GraphicsPage : Page
    {
        Model.P31SchoolEntities dvc = new Model.P31SchoolEntities();

        public GraphicsPage()
        {
            InitializeComponent();
            ClassCombo.SelectedIndex = 0;
            Classes.ItemsSource = dvc.Class.ToList();

            int cet = DateTime.Now.Month;
            if (cet >= 9 && cet <= 10) Cetverts.SelectedIndex = 0;
            else if (cet >= 11 && cet <= 12) Cetverts.SelectedIndex = 1;
            else if (cet >= 1 && cet <= 3) Cetverts.SelectedIndex = 2;
            else Cetverts.SelectedIndex = 3;
        }

        private void ClassCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClassLoad();
        }


        public void ClassLoad()
        {
            MainChart.Series[0].Points.Clear();
            List<Model.Class> list = new List<Model.Class>();
            switch (ClassCombo.SelectedIndex)
            {
                case 0: list = dvc.Class.ToList(); break;
                case 1: list = dvc.Class.Where(u => u.ClassName.Substring(0, 1) == "1").ToList(); break;
                case 2: list = dvc.Class.Where(u => u.ClassName.Substring(0, 1) == "2").ToList(); break;
                case 3: list = dvc.Class.Where(u => u.ClassName.Substring(0, 1) == "3").ToList(); break;
                case 4: list = dvc.Class.Where(u => u.ClassName.Substring(0, 1) == "4").ToList(); break;
                case 5: list = dvc.Class.Where(u => u.ClassName.Substring(0, 1) == "5").ToList(); break;
                case 6: list = dvc.Class.Where(u => u.ClassName.Substring(0, 1) == "6").ToList(); break;
                case 7: list = dvc.Class.Where(u => u.ClassName.Substring(0, 1) == "7").ToList(); break;
                case 8: list = dvc.Class.Where(u => u.ClassName.Substring(0, 1) == "8").ToList(); break;
                case 9: list = dvc.Class.Where(u => u.ClassName.Substring(0, 1) == "9").ToList(); break;
                case 10: list = dvc.Class.Where(u => u.ClassName.Substring(0, 2) == "10").ToList(); break;
                case 11: list = dvc.Class.Where(u => u.ClassName.Substring(0, 2) == "11").ToList(); break;
            }

            foreach (var category in list)
            {
                MainChart.Series[0].Points.AddXY(category.ClassName, category.Count);
            }
        }

        public void PupilsLoad()
        {
            if (Pupils.SelectedItem != null)
            {
                MainChart.Series[0].Points.Clear();
                List<Model.Grade> list = new List<Model.Grade>();
                dynamic r = Pupils.SelectedItem;
                int id = r.PupilID;
                switch (Cetverts.SelectedIndex)
                {
                    case 0: list = dvc.Grade.Where(u => u.PupilID == id && u.Date.Month >= 9 && u.Date.Month <= 10).ToList(); break;
                    case 1: list = dvc.Grade.Where(u => u.PupilID == id && u.Date.Month >= 11 && u.Date.Month <= 12).ToList(); break;
                    case 2: list = dvc.Grade.Where(u => u.PupilID == id && u.Date.Month >= 1 && u.Date.Month <= 3).ToList(); break;
                    default: list = dvc.Grade.Where(u => u.PupilID == id && u.Date.Month >= 4 && u.Date.Month <= 8).ToList(); break;
                }
                foreach (var category in list)
                {
                    MainChart.Series[0].Points.AddXY(category.Subject.SubjectName, category.Grade1);
                }
            }
        }

        private void Classes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic r = Classes.SelectedItem;
            int id = r.ClassID;
            Pupils.ItemsSource = dvc.Pupil.Where(u => u.ClassID == id).ToList();
        }

        private void Pupils_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PupilsLoad();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (tc.SelectedIndex)
            {
                case 0: ClassCombo.SelectedIndex = 0; ClassLoad(); break;
                case 1: PupilsLoad(); if (Classes.Text == "") { Classes.SelectedIndex = 0; Pupils.SelectedIndex = 0; } break;
            }
        }
    }
}
