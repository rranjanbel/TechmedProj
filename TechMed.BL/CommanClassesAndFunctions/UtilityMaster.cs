using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.CommanClassesAndFunctions
{
    public static class UtilityMaster
    {
        public static int Age = 0;
        public static int GetAgeOfPatient(DateTime dateOfBirth)
        {
            DateTime dtToday = DateTime.Now.Date;
            DateTime dtOfBirth = dateOfBirth.Date;
            TimeSpan diffResult = dtToday - dtOfBirth;
            double totalDays = diffResult.TotalDays;
            if (diffResult != TimeSpan.Zero)
            {
                if (totalDays > 365)
                {
                    int year = (int)(totalDays / 365);
                    return year;
                }
                else if (totalDays < 365)
                {
                    int month = (int)(totalDays / 12);
                    return 365;
                }
                else
                {
                    return 0;
                }

            }
            else
                return 0;
        }
    }
}
