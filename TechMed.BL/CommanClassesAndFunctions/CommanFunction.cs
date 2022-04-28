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
            if (dob.Year==DateTime.Now.Year)
            {
                return age;
            }
            age = DateTime.Now.AddYears(-dob.Year).Year;
            return age;
        }
    }
}
