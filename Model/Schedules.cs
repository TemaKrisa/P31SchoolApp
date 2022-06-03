namespace P31School.Model
{
    partial class Teacher
    {
        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        public string FIO
        {
            get
            {
                string FIO = Surname + " " + Name + " " + MidName;

                return FIO;
            }
        }
    }
}
