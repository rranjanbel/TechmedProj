using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TechMed.BL.CommanClassesAndFunctions;
using TechMed.BL.DTOMaster;
using TechMed.BL.Repository.Interfaces;
using TechMed.BL.ViewModels;
using TechMed.DL.Models;

namespace TechMed.BL.Repository.BaseClasses
{
    public class EquipmentUptimeReportRepositry : Repository<EquipmentUptimeReport>, IEquipmentUptimeReport
    {
        private readonly TeleMedecineContext _teleMedecineContext;
        // private readonly IEquipmentUptimeReport _equipmentUptime;
        private readonly IMapper _mapper;
        private readonly ILogger<EquipmentUptimeReport> _logger;
        public EquipmentUptimeReportRepositry(ILogger<EquipmentUptimeReport> logger, TeleMedecineContext teleMedecineContext, IMapper mapper): base(teleMedecineContext)
        {
            this._teleMedecineContext = teleMedecineContext;
            this._mapper = mapper;
            this._logger = logger;


        }

        public async Task<EquipmentUptimeReportDTO> AddEquipmentUptimeReport(EquipmentUptimeReportDTO equipmentUptimeReport)
        {
            int i = 0;
            EquipmentUptimeReport equipmentUptime = new EquipmentUptimeReport();

            EquipmentUptimeReportDTO equipmentUptimeReportdto = new EquipmentUptimeReportDTO();
            try
            {
                if (equipmentUptimeReport != null)
                {
                    equipmentUptime = _mapper.Map<EquipmentUptimeReport>(equipmentUptimeReport);
                    equipmentUptime.CreatedOn = UtilityMaster.GetLocalDateTime();
                    equipmentUptime.UpdatedOn = UtilityMaster.GetLocalDateTime();
                    var ressult = _teleMedecineContext.EquipmentUptimeReports.AddAsync(equipmentUptime);
                    i = await _teleMedecineContext.SaveChangesAsync();
                    if (i > 0)
                    {
                        equipmentUptimeReportdto = _mapper.Map<EquipmentUptimeReportDTO>(equipmentUptime);
                    }
                }
            }
            catch (Exception ex)
            {
                string excpMessage = ex.Message;
            }

            return equipmentUptimeReportdto;
        }
    }
}
