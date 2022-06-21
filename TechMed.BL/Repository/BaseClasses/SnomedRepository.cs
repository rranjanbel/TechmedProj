using Microsoft.EntityFrameworkCore;
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
        public class SnomedRepository : Repository<SnomedCTCode>, ISnomedRepository
    {
            private readonly TeleMedecineContext _teleMedecineContext;
            private readonly ILogger<SnomedCTCode> _logger;
            public SnomedRepository(ILogger<SnomedCTCode> logger, TeleMedecineContext teleMedecineContext) : base(teleMedecineContext)
            {
                this._teleMedecineContext = teleMedecineContext;
              
                this._logger = logger;
            }

            public async Task<List<SnomedCTCode>> SearchSnomedCodeByName(string searchText)
            {
                List<SnomedCTCode> diagnosticTests = new List<SnomedCTCode>();
                return await _teleMedecineContext.SnomedCTCodes.Where(x=>x.CodeName.ToLower().Contains(searchText.ToLower())).Take(20).ToListAsync();
            }


        }
    }

