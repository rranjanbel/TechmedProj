using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.DTOMaster;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;

namespace TechMed.BL.Repository.Interfaces
{
    public interface IUserRepository : IRepository<UserMaster>
    {
        Task<UserLoginDTO> LogedUserDetails(string userEmail);
        Task<bool> IsValidUser(LoginVM login);
        Task<bool> IsAnExistingUser(string userEmail);
        Task<UserMaster> ChangeUserPassword(ChangePassword changePassword);
        bool ResetUserPassword(int UserId);
        bool DeleteUser(int UserId);
        bool IsduplicateUser(string username);
        bool SetUserPassword(int UserId, string Password);
        Task<string> GetUserRole(string userEmail);

    }
}
