using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.Adapters;
using TechMed.DL.Models;

namespace TechMed.BL.ModelMaster
{
    public class UserBusinessMaster : BaseAdapter
    {
        private readonly IMapper _mapper;
        private readonly TeleMedecineContext _teleMedecineContext;       
        public UserBusinessMaster(TeleMedecineContext teleMedecineContext, IMapper mapper) : base(teleMedecineContext, mapper)
        {
            this._mapper = mapper;
            this._teleMedecineContext = teleMedecineContext;
        }

        public List<UserMaster> GetUserMasters()
        {
            List<UserMaster> users = new List<UserMaster>();
            users = _teleMedecineContext.UserMasters.ToList();
            return users;
        }
    }
}
