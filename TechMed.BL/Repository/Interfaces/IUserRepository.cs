using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;

namespace TechMed.BL.Repository.Interfaces
{
    public interface IUserRepository : IRepository<UserMaster>
    {
        UserMaster UserAuthentication(LoginVM login);
        bool ChangeUserPassword(ChangePassword changePassword);
        bool ResetUserPassword(long UserId);
        bool DeleteUser(long UserId);
        bool IsduplicateUser(string username);
        bool SetUserPassword(long UserId, string Password);

    }
}
