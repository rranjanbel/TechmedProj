using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class DashboardReportConsultationVM
    {
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public string? MobileNo { get; set; }
        public string? OPDNo { get; set; }
        public DateTime ReviewDate { get; set; }
        public DateTime ConsultDate { get; set; }
        public DateTime starttime { get; set; }
        public DateTime endtime { get; set; }
        public string? specialization { get; set; }
        public int SrNo { get; set; }
        public string? DistrictName { get; set; }//
        public string? BlockName { get; set; }//
        public string? Complaint { get; set; }//
        public DateTime AssignedOn { get; set; }
        public string? PatientName { get; set; }
        public int Age { get; set; }
        public string? Gender { get; set; }
        public string? PHCName { get; set; }
        public string? PHCTechnician { get; set; }
        public string? Doctor { get; set; }
        public string? PHCAddress { get; set; }
        public int CluserID { get; set; }
        public string? Cluster { get; set; }
        public int BlockID { get; set; }
        public int PHCID { get; set; }
        public int CreatedBy { get; set; }
        public string? PatientCreatedBy { get; set; }
    }
}
