using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.DL.Models;

namespace TechMed.BL.Adapters
{
    public class DoctorAdapter : BaseAdapter
    {
        public DoctorAdapter(TeleMedecineContext teleMedecineContext) : base(teleMedecineContext)
        {
        }
        //public GetDoctorDetailResponse GetDoctorDetails(GetDoctorDetailRequest getDoctorDetailRequest)
        //{
        //    GetDoctorDetailResponse getDoctorDetailResponse = new GetDoctorDetailResponse();
        //    return getDoctorDetailResponse;
        //}
        public void UpdateDoctorDetails()
        {
           
        }
        public void GetListOfPHCHospital()
        {
        }
        public void GetListOfNotification()
        {

        }
        public void GetYesterdayPatientsHistory()
        {

        }
        public void GetAfterYesterdayPatientsHistory()
        {

        }
        public void GetTodayesPatients()
        {

        }
        public void GetCompletedConsultationPatientsHistory()
        {

        }
        public void GetListOfVital()
        {

        }
        public void GetListOfMedicine()
        {

        }
        public void GetCDSSGuideLines()
        {

        }
        public void PostTreatmentPlan()
        {

        }
        public void PatientAbsent()
        {

        }
        public void ReferHigherFacilityAbsent()
        {

        }
        public void CallPHC()
        {

        }

        public void GetPatientCaseDetails()
        {

        }
        public void GetPatientCaseFiles()
        {

        }

    }
}
