using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.Repository.Interfaces;
using TechMed.BL.ViewModels;
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

        public async Task<PHCDetailsVM> GetPHCDetailByUserID(int userId)
        {
            PHCDetailsVM pHCDetails = new PHCDetailsVM();           
        //    pHCDetails = (from pm in _teleMedecineContext.Phcmasters
        //                  join cm in _teleMedecineContext.ClusterMasters on pm.Id equals cm.Id
        //                  join zo in _teleMedecineContext.ZoneMasters on pm.ZoneId equals zo.Id
        //                  join ur in _teleMedecineContext.UserMasters on pm.UserId equals ur.Id
        //                  where pm.UserId == userId
        //                  select new PHCDetailsVM
        //                  {
        //                      pHCDetails.Phcname = pm.Phcname;
        //    pHCDetails.ClusterName = pm.Cluster.Cluster;
        //    pHCDetails.ZoneName = pm.Zone.Zone;
        //    pHCDetails.Moname = pm.Moname;
        //    pHCDetails.Address = pm.Address;
        //    pHCDetails.PhoneNo = pm.PhoneNo;
        //    pHCDetails.MailId = pm.MailId;
        //    pHCDetails.UserName = pm.User.Name;
        //    pHCDetails.Id = pm.Id;
        //}).FirstOrDefaultAsync();

        var pm = await _teleMedecineContext.Phcmasters.Include(c => c.Cluster).Include(z => z.Zone).Include(u => u.User).FirstOrDefaultAsync(a => a.UserId == userId);             
            if (pm != null)
            {
                pHCDetails.Phcname = pm.Phcname;
                pHCDetails.ClusterName = pm.Cluster.Cluster;
                pHCDetails.ZoneName = pm.Zone.Zone;
                pHCDetails.Moname = pm.Moname;
                pHCDetails.Address = pm.Address;
                pHCDetails.PhoneNo = pm.PhoneNo;
                pHCDetails.MailId = pm.MailId;
                pHCDetails.UserName = pm.User.Name;
                pHCDetails.Id = pm.Id;
            }           
            return pHCDetails;
        }

        public bool IsPHCExit(string name)
        {
           bool isExist = _teleMedecineContext.Phcmasters.Any(a => a.Phcname == name);
            return isExist;
        }
    }
}
