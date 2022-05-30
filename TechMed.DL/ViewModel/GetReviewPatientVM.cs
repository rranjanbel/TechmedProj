using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class GetReviewPatientVM
    {
        public long SrNo { get; set; }
        public string? DistrictName { get; set; }
        public string? BlockName { get; set; }
        public string? PHCName { get; set; }
        public string? PatientName { get; set; }
        public string? DoctorName { get; set; }
        public DateTime? Consultdate { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string? Complaints { get; set; }
        public string? Prescription { get; set; }
    }
}
