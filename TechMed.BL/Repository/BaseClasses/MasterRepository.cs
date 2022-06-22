using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.DTOMaster;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.Models;

namespace TechMed.BL.Repository.BaseClasses
{
    public class MasterRepository : IMasterRepository
    {
        private readonly TeleMedecineContext _teleMedecineContext;
        private readonly IMapper _mapper;
        private readonly ILogger<MasterRepository> _logger;
        public MasterRepository(ILogger<MasterRepository> logger, TeleMedecineContext teleMedecineContext, IMapper mapper) 
        {
            this._teleMedecineContext = teleMedecineContext;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<List<DivisionDTO>> GetAllDivision()
        {
            
            DivisionDTO divisionDTO = new DivisionDTO();
            List<DivisionDTO> divisionDTOs = new List<DivisionDTO>();
            var divisionList = await _teleMedecineContext.DivisionMasters.ToListAsync();
            if(divisionList != null)
            {
                foreach (var item in divisionList)
                {
                    divisionDTO = _mapper.Map<DivisionDTO>(item);
                    divisionDTOs.Add(divisionDTO);

                }
            }
            
            return divisionDTOs;
        }

        public async Task<List<DivisionDTO>> GetDivisionByClusterID(int clusterId)
        {
            DivisionDTO divisionDTO = new DivisionDTO();
            List<DivisionDTO> divisionDTOs = new List<DivisionDTO>();
            var divisionList = await this._teleMedecineContext.DivisionMasters.Where(s => s.ClusterId == clusterId).ToListAsync();
            if (divisionList != null)
            {
                foreach (var item in divisionList)
                {
                    divisionDTO = _mapper.Map<DivisionDTO>(item);
                    divisionDTOs.Add(divisionDTO);

                }
            }

            return divisionDTOs;
        }

        public async Task<DivisionDTO> GetDivisionById(int Id)
        {
            DivisionDTO divisionDTO = new DivisionDTO();
           
            var divisionList = await this._teleMedecineContext.DivisionMasters.FirstOrDefaultAsync(s => s.Id == Id);
            if (divisionList != null)
            {
                divisionDTO = _mapper.Map<DivisionDTO>(divisionList);
            }

            return divisionDTO;
        }
    }
}
