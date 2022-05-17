using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    public class GetListOfPHCHospitalVM
    {
        [Required]
        public int BlockID { get; set; }
    }
}
