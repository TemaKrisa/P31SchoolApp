using System.Linq;

namespace P31School.Model
{
    partial class Class
    {
        Model.P31SchoolEntities dc = new Model.P31SchoolEntities();
        public int Count
        {
            get
            {
                int count = dc.Pupil.Where(u => u.ClassID == ClassID).Count();

                return count;
            }
        }
    }
}
