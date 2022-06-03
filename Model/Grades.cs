namespace P31School.Model
{
    partial class Grade
    {
        public int Cetvert
        {
            get
            {
                int cet;
                if (Date.Month >= 9 && Date.Month <= 10) cet = 1;
                else if (Date.Month >= 11 && Date.Month <= 12) cet = 2;
                else if (Date.Month >= 1 && Date.Month <= 3) cet = 3;
                else cet = 4;
                return cet;
            }
        }
        public int Year
        {
            get
            {
                return Date.Year;
            }
        }
    }
}
