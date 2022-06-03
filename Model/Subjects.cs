using System.Linq;

namespace P31School.Model
{
    partial class Subject
    {
        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        public int Count
        {
            get
            {
                int count = (from s in dc.SubjectTeacherView where s.SubjectID == SubjectID select new { s.TeacherID }).Distinct().Count();
                return count;
            }
        }
    }
}
