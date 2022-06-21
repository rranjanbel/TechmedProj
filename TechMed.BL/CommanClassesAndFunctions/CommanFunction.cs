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
                DateTime dtToday = UtilityMaster.GetLocalDateTime().Date;
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
        public static string GetAgeGroup(int Age)
        {
            string ageGroup = string.Empty;
            if (Age >= 0 && Age < 15)
            {
                ageGroup = "0-14";
            }
            else if (Age >= 15 && Age < 60)
            {
                ageGroup = "15-60";
            }
            else if (Age >= 60)
            {
                ageGroup = "60 & above";
            }
            else
            {
                ageGroup = Age.ToString();
            }
            return ageGroup;
        }

    }
}
