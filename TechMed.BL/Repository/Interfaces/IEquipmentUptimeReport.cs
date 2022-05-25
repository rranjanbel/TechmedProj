using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.DTOMaster;
using TechMed.DL.Models;

namespace TechMed.BL.Repository.Interfaces
{
    public interface IEquipmentUptimeReport: IRepository<EquipmentUptimeReport>
    {
        Task<EquipmentUptimeReportDTO> AddEquipmentUptimeReport(EquipmentUptimeReportDTO equipmentUptimeReport);
    }
}
