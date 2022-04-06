﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.DTOMaster;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;

namespace TechMed.BL.Repository.Interfaces
{
    public interface IDoctorRepository : IRepository<UserMaster>
    {
        void AddDoctorDetails();
        public Task<DoctorDTO> GetDoctorDetails(GetDoctorDetailVM getDoctorDetailVM);
        void UpdateDoctorDetails();
        public Task<List<PHCHospitalDTO>> GetListOfPHCHospital();
        public Task<List<NotificationDTO>> GetListOfNotification(GetListOfNotificationVM getListOfNotificationVM);
        public Task<CdssguidelineMasterDTO> GetCDSSGuideLines();
        void GetYesterdayPatientsHistory();
        void GetAfterYesterdayPatientsHistory();
        public void GetTodayesPatients();
        void GetCompletedConsultationPatientsHistory();
        public Task<List<VitalMasterDTO>> GetListOfVital();
        public Task<List<MedicineMasterDTO>> GetListOfMedicine();
        public Task<List<SpecializationDTO>> GetListOfSpecializationMaster();
        public Task<List<SubSpecializationDTO>> GetListOfSubSpecializationMaster(int SpecializationID);
        void PostTreatmentPlan();
        void PatientAbsent();
        void ReferHigherFacilityAbsent();
        void CallPHC();
        void GetPatientCaseDetails();
        void GetPatientCaseFiles();

    }
}
