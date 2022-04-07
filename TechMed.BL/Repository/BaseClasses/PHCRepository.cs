using AutoMapper;
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
    public class PHCRepository : Repository<Phcmaster>, IPHCRepository
    {
        private readonly TeleMedecineContext _teleMedecineContext;
        private readonly IMapper _mapper;
        private readonly ILogger<PHCRepository> _logger;
        public PHCRepository(ILogger<PHCRepository> logger, TeleMedecineContext teleMedecineContext, IMapper mapper) : base(teleMedecineContext)
        {
            this._teleMedecineContext = teleMedecineContext;
            this._mapper = mapper;
            this._logger = logger;


        }

        public async Task<Phcmaster> GetByID(int id)
        {            
            var phcmaster = await _teleMedecineContext.Phcmasters.FirstOrDefaultAsync(a => a.Id == id);
            return phcmaster;
        }

        public async Task<Phcmaster> GetByPHCUserID(int userId)
        {
            var phcmaster = await _teleMedecineContext.Phcmasters.FirstOrDefaultAsync(a => a.UserId == userId);
            return phcmaster;
        }
    }
}
