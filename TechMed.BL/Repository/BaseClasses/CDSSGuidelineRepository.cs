using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.CommanClassesAndFunctions;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;

namespace TechMed.BL.Repository.BaseClasses
{

    public class CDSSGuidelineRepository : Repository<CDSSGuideline>, ICDSSGuidelineRepository
    {
        private readonly TeleMedecineContext _teleMedecineContext;
        private readonly IMapper _mapper;
        private readonly ILogger<DrugsRepository> _logger;
        public CDSSGuidelineRepository(ILogger<DrugsRepository> logger, TeleMedecineContext teleMedecineContext, IMapper mapper) : base(teleMedecineContext)
        {
            this._teleMedecineContext = teleMedecineContext;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<List<CDSSGuidelineVM>> GetAllCDSSGuideline()
        {
            List<CDSSGuidelineVM> guidelineVMs = new List<CDSSGuidelineVM>();
            CDSSGuidelineVM drugsMaster;
            var drugs = await _teleMedecineContext.CDSSGuidelines.ToListAsync();
            foreach (var item in drugs)
            {
                drugsMaster = new CDSSGuidelineVM();
                drugsMaster.ID = item.ID;
                drugsMaster.Age = item.Age;
                drugsMaster.Diseases = item.Diseases;
                drugsMaster.Treatment = item.Treatment;
                guidelineVMs.Add(drugsMaster);
            }
            return guidelineVMs.OrderBy(a => a.ID).ToList();
        }
        public async Task<List<CDSSGuidelineVM>> GetAllCDSSGuidelineByDiseases(string Diseases)
        {
            List<CDSSGuidelineVM> guidelineVMs = new List<CDSSGuidelineVM>();
            CDSSGuidelineVM drugsMaster;
            //var drugs = await _teleMedecineContext.CDSSGuidelines.ToListAsync();

            var matches = from m in _teleMedecineContext.CDSSGuidelines
                          where m.Diseases.Contains(Diseases)
                          select m;
            foreach (var item in matches)
            {
                drugsMaster = new CDSSGuidelineVM();
                drugsMaster.ID = item.ID;
                drugsMaster.Age = item.Age;
                drugsMaster.Diseases = item.Diseases;
                drugsMaster.Treatment = item.Treatment;
                guidelineVMs.Add(drugsMaster);
            }
            return guidelineVMs.OrderBy(a => a.ID).ToList();
        }

        
        public async Task<List<CDSSGuidelineVM>> GetCDSSGuideLinesByDiseasesAndAge(string Diseases,int Age)
        {
            List<CDSSGuidelineVM> guidelineVMs = new List<CDSSGuidelineVM>();
            CDSSGuidelineVM drugsMaster;

           string strage= CommanFunction.GetAgeGroup(Age);

            var matches = from m in _teleMedecineContext.CDSSGuidelines
                          where m.Diseases.Contains(Diseases)
                          &&  m.Age == strage
                          select m;
            foreach (var item in matches)
            {
                drugsMaster = new CDSSGuidelineVM();
                drugsMaster.ID = item.ID;
                drugsMaster.Age = item.Age;
                drugsMaster.Diseases = item.Diseases;
                drugsMaster.Treatment = item.Treatment;
                guidelineVMs.Add(drugsMaster);
            }
            return guidelineVMs.OrderBy(a => a.ID).ToList();
        }
        public async Task<List<CDSSGuidelineVM>> GetDiseases(string Diseases)
        {
            List<CDSSGuidelineVM> guidelineVMs = new List<CDSSGuidelineVM>();
            CDSSGuidelineVM drugsMaster;
            //var drugs = await _teleMedecineContext.CDSSGuidelines.ToListAsync();

            var matches = from m in _teleMedecineContext.CDSSGuidelines
                          where m.Diseases.Contains(Diseases)
                          select m;
            foreach (var item in matches)
            {
                drugsMaster = new CDSSGuidelineVM();
                drugsMaster.ID = item.ID;
                drugsMaster.Age = item.Age;
                drugsMaster.Diseases = item.Diseases;
                drugsMaster.Treatment = item.Treatment;
                guidelineVMs.Add(drugsMaster);
            }
            return guidelineVMs.OrderBy(a => a.ID).ToList();
        }

        public async Task<CDSSGuidelineVM> GetAllCDSSGuidelineByID(Int64 ID)
        {
            List<CDSSGuidelineVM> guidelineVMs = new List<CDSSGuidelineVM>();
            CDSSGuidelineVM drugsMaster = new CDSSGuidelineVM();
            var cDSS = await _teleMedecineContext.CDSSGuidelines.Where(a => a.ID == ID).FirstOrDefaultAsync();
            if (cDSS != null)
            {
                drugsMaster.ID = cDSS.ID;
                drugsMaster.Age = cDSS.Age;
                drugsMaster.Diseases = cDSS.Diseases;
                drugsMaster.Treatment = cDSS.Treatment;
                guidelineVMs.Add(drugsMaster);
            }
            return drugsMaster;
        }

        
    }
}
