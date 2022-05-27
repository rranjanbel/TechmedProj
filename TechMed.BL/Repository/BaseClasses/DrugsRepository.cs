using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.CommanClassesAndFunctions;
using TechMed.BL.Repository.Interfaces;
using TechMed.BL.ViewModels;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TechMed.BL.DTOMaster;

namespace TechMed.BL.Repository.BaseClasses
{
    public class DrugsRepository: Repository<DrugsMaster>, IDrugsRepository
    {
        private readonly TeleMedecineContext _teleMedecineContext;
        private readonly IMapper _mapper;
        private readonly ILogger<DrugsRepository> _logger;
        public DrugsRepository(ILogger<DrugsRepository> logger, TeleMedecineContext teleMedecineContext, IMapper mapper) : base(teleMedecineContext)
        {
            this._teleMedecineContext = teleMedecineContext;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<List<DrugsMasterDTO>> GetAllDrugs()
        {
            List<DrugsMasterDTO> drugsMasterDTOs = new List<DrugsMasterDTO>();
            DrugsMasterDTO drugsMaster;
            var drugs = await _teleMedecineContext.DrugsMasters.ToListAsync();
            foreach (var item in drugs)
            {
                drugsMaster = new DrugsMasterDTO();
                drugsMaster.ID = item.Id;
                drugsMaster.Name = item.NameOfDrug + "," + item.DrugformAndStrength;
                drugsMasterDTOs.Add(drugsMaster);
            }
            return drugsMasterDTOs; 
        }
    }
}
