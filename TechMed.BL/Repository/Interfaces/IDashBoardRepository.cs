using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.DTOMaster;
using TechMed.DL.ViewModel;

namespace TechMed.BL.Repository.Interfaces
{
    public interface IDashBoardRepository
    {
        public Task<List<DoctorDTO>> DoctorsLoggedInToday(DoctorsLoggedInTodayVM doctorsLoggedInTodayVM);
    }
}
