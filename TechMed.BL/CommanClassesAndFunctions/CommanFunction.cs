using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.CommanClassesAndFunctions
{
    public class CommanFunction
    {
        public static int GetAge(DateTime dob)
        {
            int age = 0;
            age = DateTime.Now.AddYears(-dob.Year).Year;
            return age;
        }
    }
}
