﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.DL.Models;

namespace TechMed.BL.Repository.Interfaces
{
    public interface IPatientRepository: IRepository<PatientMaster>
    {
        Task<List<PatientMaster>> GetAllPatient();
        Task<PatientMaster> AddPatient(PatientMaster patientMaster);
        Task<PatientMaster> GetPatientByID(int Id);
        Task<PatientMaster> UpdatePatient(int Id);
        Task<bool> DeletePatient(int Id);
        Task<List<PatientMaster>> GetTodaysPatientList(int Id);
        Task<List<PatientMaster>> GetCheckedPatientList(int Id);
        Task<List<PatientMaster>> GetUnCheckedPatientList(int Id);
        Task<List<PatientMaster>> GetPendingPatientList(int Id);
    }
}