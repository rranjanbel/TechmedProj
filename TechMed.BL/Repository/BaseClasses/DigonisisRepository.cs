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

namespace TechMed.BL.Repository.BaseClasses
{
    public class DigonisisRepository:Repository<DiagnosticTestMaster>, IDigonisisRepository
    {
        private readonly TeleMedecineContext _teleMedecineContext;
        private readonly IMapper _mapper;
        private readonly ILogger<DiagnosticTestMaster> _logger;
        public DigonisisRepository(ILogger<DiagnosticTestMaster> logger, TeleMedecineContext teleMedecineContext, IMapper mapper) : base(teleMedecineContext)
        {
            this._teleMedecineContext = teleMedecineContext;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<List<DiagnosticTestMaster>> GetAllDignosis()
        {
            List<DiagnosticTestMaster> diagnosticTests = new List<DiagnosticTestMaster>();
            diagnosticTests = await _teleMedecineContext.DiagnosticTestMasters.ToListAsync();
            return diagnosticTests;
        }
    }
}
