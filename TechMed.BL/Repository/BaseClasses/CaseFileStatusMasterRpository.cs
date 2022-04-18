using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.Models;

namespace TechMed.BL.Repository.BaseClasses
{
  
    public class CaseFileStatusMasterRpository : Repository<CaseFileStatusMaster>, ICaseFileStatusMasterRpository
    {
        private readonly TeleMedecineContext _teleMedecineContext;
        private readonly IMapper _mapper;
        private readonly ILogger<SpecializationRepository> _logger;
        public CaseFileStatusMasterRpository(ILogger<SpecializationRepository> logger, TeleMedecineContext teleMedecineContext, IMapper mapper) : base(teleMedecineContext)
        {
            this._teleMedecineContext = teleMedecineContext;
            this._mapper = mapper;
            this._logger = logger;
        }
    }
}
