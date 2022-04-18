using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    public class CompletedConsultationVM
    {
        [Required]
        public DateTime fromDate { get; set; }
        [Required]
        public DateTime Todate { get; set; }
        [Required]
        public string Referral { get; set; }
    }
}
