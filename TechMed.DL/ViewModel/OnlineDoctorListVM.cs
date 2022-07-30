using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    public class OnlineDoctorListVM
    {
        public List<OnlineDoctorVM> OnlineDoctors { get; set; }
        public string Status { get; set; }
        public int StatusID { get; set; } = 0;
        public string Message { get; set; }
    }
}
