using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL
{
    public class Dose
    {
        public bool Morning { get; set; }
        public bool Noon { get; set; }
        public bool Night { get; set; }
        public bool EmptyStomach { get; set; }
        public bool AfterMeal { get; set; }
        public bool OD { get; set; }
        public bool BD { get; set; }
        public bool TD { get; set; }
    }
}
