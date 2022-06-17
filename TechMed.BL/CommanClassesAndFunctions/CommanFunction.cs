using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.CommanClassesAndFunctions
{
    public class CommanFunction
    {
        public static int GetAge(DateTime? dob)
        {
            int age = 0;
            if (dob == null)
            {
                return age;
            }
            else
            {
                DateTime dtToday = DateTime.Now.Date;
                DateTime dtOfBirth = dob.Value.Date;
                TimeSpan diffResult = dtToday - dtOfBirth;
                double totalDays = diffResult.TotalDays;

                if (totalDays > 365)
                {
                    int year = (int)(totalDays / 365.2425);
                    return year;
                }
                else
                {
                    return 0;
                }
            }

            

        }
    }
}
