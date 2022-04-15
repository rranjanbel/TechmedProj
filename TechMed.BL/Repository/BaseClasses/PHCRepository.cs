﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TechMed.BL.Repository.Interfaces;
using TechMed.BL.ViewModels;
using TechMed.DL.Models;

namespace TechMed.BL.Repository.BaseClasses
{
    public class PHCRepository : Repository<Phcmaster>, IPHCRepository
    {
        private readonly TeleMedecineContext _teleMedecineContext;
        //private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PHCRepository> _logger;
        public PHCRepository(ILogger<PHCRepository> logger, TeleMedecineContext teleMedecineContext, IMapper mapper) : base(teleMedecineContext)
        {
            this._teleMedecineContext = teleMedecineContext;
            this._mapper = mapper;
            this._logger = logger;


        }

        public async Task<Phcmaster> AddPHCUser(Phcmaster phcmaster, UserMaster userMaster)
        {
            int i = 0;
            int j = 0;
            Phcmaster phcmasternew = new Phcmaster();            

            using (TeleMedecineContext context = new TeleMedecineContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.UserMasters.AddAsync(userMaster);
                        i = await context.SaveChangesAsync();
                        if (i > 0 && userMaster.Id > 0)
                        {
                            phcmaster.UserId = userMaster.Id;
                            context.Phcmasters.AddAsync(phcmaster);
                            j = await context.SaveChangesAsync(); ;
                        }
                        if (i > 0 && j > 0)
                        {
                            transaction.Commit();

                            phcmasternew = context.Phcmasters.FirstOrDefault(a => a.Id == phcmaster.Id);
                            //phcmasternew = (Phcmaster)newPHC;
                        }
                        else
                        {
                            transaction.Rollback();
                        }
                    }
                    catch (Exception ex)
                    {
                        string excp = ex.Message;
                        transaction.Rollback();
                    }
                }
            }        
           
            return phcmasternew;
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
            var phcresult = await (from pm in _teleMedecineContext.Phcmasters
                          join cm in _teleMedecineContext.ClusterMasters on pm.Id equals cm.Id
                          join zo in _teleMedecineContext.ZoneMasters on pm.ZoneId equals zo.Id
                          join ur in _teleMedecineContext.UserMasters on pm.UserId equals ur.Id
                          where pm.UserId == userId
                          select new PHCDetailsVM
                          {
                              Phcname = pm.Phcname,
                              ClusterName = pm.Cluster.Cluster,
                              ZoneName = pm.Zone.Zone,
                               Moname = pm.Moname,
                               Address = pm.Address,
                               PhoneNo = pm.PhoneNo,
                               MailId = pm.MailId,
                               UserName = pm.User.Name,
                               Id = pm.Id
                          }).FirstOrDefaultAsync();

            pHCDetails = (PHCDetailsVM)phcresult;
        //var pm = await _teleMedecineContext.Phcmasters.Include(c => c.Cluster).Include(z => z.Zone).Include(u => u.User).FirstOrDefaultAsync(a => a.UserId == userId);
        //var pm = await _teleMedecineContext.Phcmasters.FirstOrDefaultAsync(a => a.UserId == userId);
        //    if (pm != null)
        //    {
        //        pHCDetails.Phcname = pm.Phcname;
        //        pHCDetails.ClusterName = pm.Cluster.Cluster;
        //        pHCDetails.ZoneName = pm.Zone.Zone;
        //        pHCDetails.Moname = pm.Moname;
        //        pHCDetails.Address = pm.Address;
        //        pHCDetails.PhoneNo = pm.PhoneNo;
        //        pHCDetails.MailId = pm.MailId;
        //        pHCDetails.UserName = pm.User.Name;
        //        pHCDetails.Id = pm.Id;
        //    }           
            return pHCDetails;
        }

        public bool IsPHCExit(string name)
        {
           bool isExist = _teleMedecineContext.Phcmasters.Any(a => a.Phcname == name);
            return isExist;
        }


    }
}
