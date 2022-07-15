using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class GetPendingByDoctorPatientDTO
    {
        public GetPendingByDoctorPatientDTO()
        {
            patientDTO= new List<PatientDTO> ();
        }
        public int DoctorID { get; set; }
        public string DoctorFName { get; set; }
        public string DoctorMName { get; set; }
        public string DoctorLName { get; set; }
        public string Specialty { get; set; }
        public string Photo { get; set; }
        public List<PatientDTO> patientDTO { get; set; }
    }

    public class PatientDTO
    {
        public string SerialNo { get; set; }
        public string PHCName { get; set; }
        public string PatientName { get; set; }
        public string Sex { get; set; }
        public string Age { get; set; }
        public string CaseLabel { get; set; }
        public DateTime? RegisteredTime { get; set; }
        public DateTime? LastReferredTime { get; set; }
    }
}
