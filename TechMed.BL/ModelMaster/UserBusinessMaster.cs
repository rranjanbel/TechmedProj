using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.Adapters;
using TechMed.DL.Models;

namespace TechMed.BL.ModelMaster
{
    public class UserBusinessMaster:BaseAdapter
    {
      

        public UserBusinessMaster(TeleMedecineContext teleMedecineContext) : base(teleMedecineContext)
        {
        }

        public List<UserMaster> GetUserMasters()
        {
            List<UserMaster> users = new List<UserMaster>();
            users = teleMedecineContext.UserMasters.ToList();
            return users;
        }
    }
}
