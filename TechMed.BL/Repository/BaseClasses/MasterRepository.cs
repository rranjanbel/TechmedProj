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
            
            return divisionDTOs.OrderBy(o => o.Name).ToList();
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

            return divisionDTOs.OrderBy(o => o.Name).ToList();
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

        public async Task<List<BlockMasterDTO>> GetBlocksByDistrictID(int districtId)
        {
            BlockMasterDTO blockDTO = new BlockMasterDTO();
            List<BlockMasterDTO> blockDTOs = new List<BlockMasterDTO>();
            var districtList = await this._teleMedecineContext.BlockMasters.Where(s => s.DistrictId == districtId).ToListAsync();
            if (districtList != null)
            {
                foreach (var item in districtList)
                {
                    blockDTO = _mapper.Map<BlockMasterDTO>(item);
                    blockDTOs.Add(blockDTO);

                }
            }

            return blockDTOs.OrderBy(o=> o.BlockName).ToList();
        }

        public async Task<List<DistrictMasterDTO>> GetDistrictsByDivisionID(int divisionId)
        {
            DistrictMasterDTO districtMasterDTO = new DistrictMasterDTO();
            List<DistrictMasterDTO> districtMasterDTOs = new List<DistrictMasterDTO>();
            var districts = await this._teleMedecineContext.DistrictMasters.Where(s => s.DivisionId== divisionId).ToListAsync();
            if (districts != null)
            {
                foreach (var item in districts)
                {
                    districtMasterDTO = _mapper.Map<DistrictMasterDTO>(item);
                    districtMasterDTOs.Add(districtMasterDTO);

                }
            }

            return districtMasterDTOs.OrderBy(o => o.DistrictName).ToList();
        }
    }
}
