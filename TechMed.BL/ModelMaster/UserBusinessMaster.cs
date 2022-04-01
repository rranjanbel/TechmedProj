using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.DL.Models;

namespace TechMed.BL.ModelMaster
{
    public class UserBusinessMaster
    {
        TeleMedecineContext context = new TeleMedecineContext();
        public List<UserMaster> GetUserMasters()
        {
            List<UserMaster> users = new List<UserMaster>();
            users = context.UserMasters.ToList();
            return users;
        }
    }
}
