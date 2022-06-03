using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P31School.Model
{
    partial class SubjectTeacherView
    {
        public string FIO
        {
            get
            {
                string fio = Surname + " " + Name + " " + MidName;
                return fio;
            }
        }
    }
}
