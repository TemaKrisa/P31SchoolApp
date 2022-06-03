namespace P31School.Model
{
    partial class Pupil
    {
        public string FIO
        {
            get
            {
                string fio = Surname + " " + Name + " " + Midname;
                return fio;
            }
        }
    }
}
