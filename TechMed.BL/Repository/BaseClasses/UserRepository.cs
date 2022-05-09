using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.CommanClassesAndFunctions;
using TechMed.BL.DTOMaster;
using TechMed.BL.Repository.Interfaces;
using TechMed.BL.ViewModels;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;

namespace TechMed.BL.Repository.BaseClasses
{
    public class UserRepository : Repository<UserMaster>, IUserRepository
    {
        private readonly TeleMedecineContext _teleMedecineContext;
        private readonly IMapper _mapper;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(ILogger<UserRepository> logger, TeleMedecineContext teleMedecineContext, IMapper mapper) : base(teleMedecineContext)
        {
            this._teleMedecineContext = teleMedecineContext;
            this._mapper = mapper;
            this._logger = logger;


        }
        public async Task<UserLoginDTO> LogedUserDetails(string userEmail)
        {
            UserLoginDTO userMaster = new UserLoginDTO();
            try
            {

                var user = await _teleMedecineContext.UserMasters.Where(x => x.Email == userEmail && x.IsActive == true).FirstOrDefaultAsync();
                if (user != null)
                {
                    userMaster = _mapper.Map<UserLoginDTO>(user);
                    userMaster.HashPassword = "";
                    //userMaster.Id = user.Id;
                    //userMaster.Name = user.Name;
                    //userMaster.HashPassword = user.HashPassword;
                    //userMaster.Email = user.Email;
                    //userMaster.Mobile = user.Mobile;
                    //userMaster.LastLoginAt = DateTime.Now;
                    //userMaster.IsActive = user.IsActive;
                    //userMaster.LoginAttempts = user.LoginAttempts;
                    //userMaster.IsPasswordChanged = user.IsPasswordChanged;
                    //if (EncodeAndDecordPassword.MatchPassword(login.Password, user.HashPassword))
                    //{
                    //    userMaster.Id = user.Id;
                    //    userMaster.Name = user.Name; 
                    //    userMaster.HashPassword = user.HashPassword;
                    //    userMaster.Email = user.Email;
                    //    userMaster.Mobile = user.Mobile;
                    //    userMaster.LastLoginAt = DateTime.Now;
                    //    userMaster.IsActive = user.IsActive;
                    //    userMaster.LoginAttempts = user.LoginAttempts;
                    //    userMaster.IsPasswordChanged= user.IsPasswordChanged;

                    //    return userMaster;
                    //}
                    //else
                    //{
                    //    userMaster.Id = user.Id;
                    //    userMaster.Name = user.Name;
                    //    userMaster.HashPassword = "not matched";
                    //    userMaster.Email = user.Email;
                    //    userMaster.Mobile = user.Mobile;
                    //    userMaster.LastLoginAt = DateTime.Now;
                    //    userMaster.IsActive = user.IsActive;
                    //    userMaster.LoginAttempts = user.LoginAttempts;
                    //    userMaster.IsPasswordChanged = user.IsPasswordChanged;                      
                    //    return userMaster;
                    //}

                    return userMaster;
                }
                else
                {
                    return userMaster;
                }
            }
            catch (Exception ex)
            {
                userMaster = new UserLoginDTO();
                return userMaster;
            }

        }

        //public bool ChangeUserPassword(ChangePassword changePassword)
        //{
        //    var existingUser = _EIMSDbContext.UserMasters.FirstOrDefault(x => x.Id == changePassword.UserId && x.Status == 1);
        //    if (existingUser != null)
        //    {
        //        existingUser.Password = EncodeAndDecordPassword.EncodePassword(changePassword.NewPassword);
        //        _EIMSDbContext.SaveChanges();
        //        return true;
        //    }
        //    return false;
        //}

        public bool ResetUserPassword(int UserID)
        {
            var existingUser = _teleMedecineContext.UserMasters.Where(x => x.Id == UserID).FirstOrDefault();
            if (existingUser != null)
            {

                existingUser.HashPassword = EncodeAndDecordPassword.EncodePassword("12345");
                _teleMedecineContext.SaveChanges();
                return true;
            }
            return false;
        }
        public bool SetUserPassword(int UserID, string Password)
        {
            var existingUser = _teleMedecineContext.UserMasters.Where(x => x.Id == UserID).FirstOrDefault();
            if (existingUser != null)
            {
                existingUser.HashPassword = EncodeAndDecordPassword.EncodePassword(Password);
                _teleMedecineContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteUser(int UserId)
        {
            if (UserId <= 0)
            {
                return false;
            }
            var removeUser = _teleMedecineContext.UserMasters.Where(x => x.Id == UserId).FirstOrDefault();
            removeUser.IsActive = false;
            var status = _teleMedecineContext.SaveChanges();
            if (status > 0)
            {
                return true;
            }

            return false;
        }      

        public bool IsduplicateUser(string UserEmail)
        {
            var existuser = _teleMedecineContext.UserMasters.Where(x => x.Email.ToUpper() == UserEmail.ToUpper()).ToList();
            if (existuser.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<UserMaster> ChangeUserPassword(ChangePassword changePassword)
        {
            UserMaster userMaster = new UserMaster();
            userMaster = _teleMedecineContext.UserMasters.FirstOrDefault(x => x.Id == changePassword.UserId && x.IsActive == true);
            if (userMaster != null)
            {
                userMaster.HashPassword = EncodeAndDecordPassword.EncodePassword(changePassword.NewPassword);
                userMaster = await  Update(userMaster);  
                if(userMaster != null)
                return userMaster;
                else
                    return userMaster;
            }
            return userMaster;
        }

        public async Task<bool> IsValidUser(LoginVM login)
        {
            if (login != null)
            {
                UserMaster userMaster = new UserMaster();
                LoginHistory loginHistory = new LoginHistory();
                UserUsertype userUsertype = new UserUsertype();
                userUsertype = _teleMedecineContext.UserUsertypes.Include(a => a.User).Include(a => a.UserType).FirstOrDefault( a => a.User.Email == login.Email && a.User.IsActive == true);
                userMaster = _teleMedecineContext.UserMasters.FirstOrDefault(a => a.Email == login.Email && a.IsActive == true);
                if (userMaster !=null)
                {
                    //string hashPwd = _teleMedecineContext.UserMasters.FirstOrDefault(a => a.Email == login.Email).HashPassword;
                    bool resrult = EncodeAndDecordPassword.MatchPassword(login.Password, userMaster.HashPassword);
                    if (resrult)
                    {
                        userMaster.LastLoginAt = DateTime.Now;

                        loginHistory.UserId = userUsertype.UserId;
                        loginHistory.UserTypeId = userUsertype.UserTypeId;
                        loginHistory.LogedInTime = DateTime.Now;

                        try
                        {
                            await Update(userMaster);

                            _teleMedecineContext.Entry(loginHistory).State = EntityState.Added;
                            _teleMedecineContext.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            string msg = ex.Message;
                        }
                        return true;

                    }
                    else
                    {
                        return false;
                    }
                }               
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }


        }

        public Task<bool> IsLoggedIn()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsAnExistingUser(string userEmail)
        {
            var isValidUser = await _teleMedecineContext.UserMasters.AnyAsync(a => a.Email == userEmail && a.IsActive == true);
            return isValidUser;
        }

        public async Task<string> GetUserRole(string userEmail)
        {
            string user = string.Empty;
            user = await _teleMedecineContext.UserUsertypes.Include(u => u.UserType).Include(a => a.User).Where(a => a.User.Email.Contains(userEmail)).Select(s => s.UserType.UserType).FirstOrDefaultAsync();
            return user;
        }

        public async Task<LoggedUserDetails> AuthenticateUser(LoginVM login)
        {
            LoggedUserDetails userDetails = new LoggedUserDetails();
            if (login != null)
            {
                UserMaster userMaster = new UserMaster();
                LoginHistory loginHistory = new LoginHistory();
                UserUsertype userUsertype = new UserUsertype();
                userUsertype = _teleMedecineContext.UserUsertypes.Include(a => a.User).Include(a => a.UserType).FirstOrDefault(a => a.User.Email == login.Email && a.User.IsActive == true);
                userMaster = _teleMedecineContext.UserMasters.FirstOrDefault(a => a.Email == login.Email && a.IsActive == true);
                if (userMaster != null)
                {
                    //string hashPwd = _teleMedecineContext.UserMasters.FirstOrDefault(a => a.Email == login.Email).HashPassword;
                    bool resrult = EncodeAndDecordPassword.MatchPassword(login.Password, userMaster.HashPassword);
                    if (resrult)
                    {
                        userMaster.LastLoginAt = DateTime.Now;

                        loginHistory.UserId = userUsertype.UserId;
                        loginHistory.UserTypeId = userUsertype.UserTypeId;
                        loginHistory.LogedInTime = DateTime.Now;

                        userDetails.UserRole = userUsertype.UserType.UserType;
                        userDetails.IsPasswordChanged = userMaster.IsPasswordChanged;

                        if (userUsertype.UserType.UserType == "PHCUser")
                        {
                            Phcmaster phc = _teleMedecineContext.Phcmasters.FirstOrDefault(a => a.UserId == userMaster.Id);
                            userDetails.UserID = phc.UserId;
                            userDetails.UserName = phc.Phcname;
                        }
                        else if(userUsertype.UserType.UserType == "Doctor")
                        {
                            DoctorMaster doctor = _teleMedecineContext.DoctorMasters.FirstOrDefault(a => a.UserId == userMaster.Id);
                            userDetails.UserID = doctor.UserId;
                            userDetails.UserName = userMaster.Name;
                        }
                        else if (userUsertype.UserType.UserType == "SuperAdmin")
                        {
                            userDetails.UserID = userMaster.Id;
                            userDetails.UserName = userMaster.Name;
                        }
                        else if (userUsertype.UserType.UserType == "MIS")
                        {
                            userDetails.UserID = userMaster.Id;
                            userDetails.UserName = userMaster.Name;

                        }
                        else if (userUsertype.UserType.UserType == "Management")
                        {
                            userDetails.UserID = userMaster.Id;
                            userDetails.UserName = userMaster.Name;
                        }
                        else if (userUsertype.UserType.UserType == "Admin")
                        {
                            userDetails.UserID = userMaster.Id;
                            userDetails.UserName = userMaster.Name;
                        }
                        else
                        {
                            userDetails.UserID = userMaster.Id;
                            userDetails.UserName = userMaster.Name;
                        }
                        
                       

                        try
                        {
                            await Update(userMaster);

                            _teleMedecineContext.Entry(loginHistory).State = EntityState.Added;
                            _teleMedecineContext.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            string msg = ex.Message;
                        }
                        return userDetails;

                    }
                    else
                    {
                        return userDetails;
                    }
                }
                else
                {
                    return userDetails;
                }
            }
            else
            {
                return userDetails;
            }


        }
    }
}
