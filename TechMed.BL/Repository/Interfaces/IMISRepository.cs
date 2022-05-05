using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.DTOMaster;
using TechMed.BL.ViewModels;
using TechMed.DL.ViewModel;

namespace TechMed.BL.Repository.Interfaces
{
    public interface IMISRepository
    {
        public Task<List<CompletedConsultantVM>> CompletedConsultation(CompletedPatientSearchVM completedConsultationSearch);
        List<ConsultedPatientByDoctorAndPHCVM> CompletedConsultationByDoctor(SearchDateVM searchField);
    }
}
