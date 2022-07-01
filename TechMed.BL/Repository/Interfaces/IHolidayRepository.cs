using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.DTOMaster;
using TechMed.DL.Models;

namespace TechMed.BL.Repository.Interfaces
{
    public interface IHolidayRepository : IRepository<HolidayMaster>
    {
        Task<bool> CreateHoliday(HolidayDTO holidayDTO);
        Task<List<HolidayDTO>> GetHolidayList(int year);
        Task<bool> DeleteHoliday(HolidayDTO holidayDTO);
        
    }
}
