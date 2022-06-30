using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TechMed.BL.DTOMaster;

namespace TechMed.BL.Repository.BaseClasses
{
    public class HolidayRepository : Repository<HolidayMaster>, IHolidayRepository
    {
        //private readonly IHolidayRepository _holidayRepository;
        private readonly TeleMedecineContext _teleMedecineContext;
        private readonly IMapper _mapper;
        private readonly ILogger<PHCRepository> _logger;
        public HolidayRepository(ILogger<PHCRepository> logger, IMapper mapper, TeleMedecineContext teleMedecineContext) : base(teleMedecineContext)
        {
            //this._holidayRepository = holidayRepository;
            this._teleMedecineContext = teleMedecineContext;
            this._mapper = mapper;
            this._logger = logger;


        }
        public async Task<bool> CreateHoliday(HolidayDTO holidayDTO)
        {
            HolidayMaster holiday = new HolidayMaster();
            if (holidayDTO != null)
            {
                int currentYear = holidayDTO.HolidayDate.Year;
                string currentMonth = holidayDTO.HolidayDate.ToString("MMMM");

                HolidayMaster holidayMaster = new HolidayMaster();
                holidayMaster.Date = holidayDTO.HolidayDate;
                holidayMaster.Description = holidayDTO.HolidayName;

                holidayMaster.CalenderId = _teleMedecineContext.CalenderMasters.Where(a => a.Year == currentYear && currentMonth.Contains(a.Month)).Select(s => s.Id).FirstOrDefault();
                var holidayMasterDB = _teleMedecineContext.HolidayMasters.Where(a => a.Date == holidayDTO.HolidayDate).FirstOrDefault();
                if (holidayMaster.CalenderId > 0)
                {
                   // _teleMedecineContext.HolidayMasters.Add(holidayMaster);
                }
                else
                {
                    CalenderMaster calenderMaster = new CalenderMaster();
                    calenderMaster.Year = currentYear;
                    calenderMaster.Month = currentMonth;
                    _teleMedecineContext.CalenderMasters.Add(calenderMaster);

                    holidayMaster.CalenderId = calenderMaster.Id;
                   // _teleMedecineContext.HolidayMasters.Add(holidayMaster);
                }

                if (holidayMasterDB!=null)
                {
                    holidayMasterDB.Description = holidayDTO.HolidayName;
                }
                else
                {
                     _teleMedecineContext.HolidayMasters.Add(holidayMaster);
                }
                int i = await _teleMedecineContext.SaveChangesAsync();
                if (i > 0)
                {
                    return true;
                }
                else
                    return false;
            }

            else
                return false;
        }

        public async Task<List<HolidayDTO>> GetHolidayList(int year)
        {
            List<HolidayDTO> holidayMasters = new List<HolidayDTO>();
            HolidayDTO holidayDTO;
            var holidays = await _teleMedecineContext.HolidayMasters.Include(h => h.Calender).Where(x => x.Calender.Year == year).ToListAsync();
            foreach (var holiday in holidays)
            {
                holidayDTO = new HolidayDTO();
                holidayDTO.HolidayDate = holiday.Date;
                holidayDTO.HolidayName = holiday.Description;

                holidayMasters.Add(holidayDTO);
            }
            return holidayMasters.OrderBy(a => a.HolidayDate).ToList();
        }

    }
}
