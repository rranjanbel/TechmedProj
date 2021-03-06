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

        public async Task<ChangePassword> ChangeUserPassword(ChangePassword changePassword)
        {
            UserMaster userMaster = new UserMaster();
            ChangePassword change = new ChangePassword();
            userMaster = _teleMedecineContext.UserMasters.FirstOrDefault(x => x.Email == changePassword.UserNameOrEmail && x.IsActive == true);
            if (userMaster != null)
            {
                bool resrult = EncodeAndDecordPassword.MatchPassword(changePassword.OldPassword, userMaster.HashPassword);
                if (resrult)
                {
                    userMaster.HashPassword = EncodeAndDecordPassword.EncodePassword(changePassword.NewPassword);
                    userMaster.IsPasswordChanged = true;

                    userMaster = await Update(userMaster);
                    if (userMaster != null)
                    {
                        change.ErrorMessage = "";
                        change.Status = "Sucess";
                        change.NewPassword = "";
                        change.OldPassword = "";
                        change.UserNameOrEmail = changePassword.UserNameOrEmail;
                        return change;
                    }
                       
                    else
                    {
                        change.ErrorMessage = "Did not update password";
                        change.Status = "Fail";
                        change.NewPassword = "";
                        change.OldPassword = "";
                        change.UserNameOrEmail = changePassword.UserNameOrEmail;
                        return change;
                    }
                        
                }
                else
                {
                    change.ErrorMessage = "Old Password did not match";
                    change.Status = "Fail";
                    change.NewPassword = "";
                    change.OldPassword = "";
                    change.UserNameOrEmail = changePassword.UserNameOrEmail;
                    return change;
                }

            }
            else
            {
                change.ErrorMessage = "user mail did not find";
                change.Status = "Fail";
                change.NewPassword = "";
                change.OldPassword = "";
                change.UserNameOrEmail = changePassword.UserNameOrEmail;
                return change;
            }
        }

        public async Task<bool> IsValidUser(LoginVM login)
        {
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
                        userMaster.LastLoginAt = UtilityMaster.GetLocalDateTime();

                        loginHistory.UserId = userUsertype.UserId;
                        loginHistory.UserTypeId = userUsertype.UserTypeId;
                        loginHistory.LogedInTime = DateTime.UtcNow;

                        try
                        {
                            await Update(userMaster);

                            if (userUsertype.UserTypeId == 4)
                            {
                                DoctorMaster doctor = _teleMedecineContext.DoctorMasters.FirstOrDefault(a => a.UserId == userUsertype.UserId);

                                if (doctor != null)
                                {
                                    doctor.IsOnline = true;
                                    _teleMedecineContext.Entry(doctor).State = EntityState.Modified;
                                }

                            }

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
                        userMaster.LastLoginAt = UtilityMaster.GetLocalDateTime();

                        loginHistory.UserId = userUsertype.UserId;
                        loginHistory.UserTypeId = userUsertype.UserTypeId;
                        loginHistory.LogedInTime = DateTime.UtcNow;

                        userDetails.UserRole = userUsertype.UserType.UserType;
                        userDetails.IsPasswordChanged = userMaster.IsPasswordChanged;

                        if (userUsertype.UserType.UserType == "PHCUser")
                        {
                            Phcmaster phc = _teleMedecineContext.Phcmasters.FirstOrDefault(a => a.UserId == userMaster.Id);
                            userDetails.UserID = phc.Id;
                            userDetails.UserName = phc.Phcname;
                        }
                        else if (userUsertype.UserType.UserType == "Doctor")
                        {
                            DoctorMaster doctor = _teleMedecineContext.DoctorMasters.FirstOrDefault(a => a.UserId == userMaster.Id);
                            userDetails.UserID = doctor.Id;
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

        public async Task<bool> LogoutUsers(string Token)
        {            
            LoginHistory loginHistory = new LoginHistory();
            loginHistory = await _teleMedecineContext.LoginHistories.FirstOrDefaultAsync(a => a.UserToken== Token);

            if (loginHistory != null)
            {
                loginHistory.LogedoutTime = UtilityMaster.GetLocalDateTime();
                DoctorMaster doctor = _teleMedecineContext.DoctorMasters.FirstOrDefault(a => a.UserId == loginHistory.UserId);
                if (doctor != null)
                {
                    doctor.IsOnline = false;
                    _teleMedecineContext.Entry(doctor).State = EntityState.Modified;
                }


                this._teleMedecineContext.Entry(loginHistory).State = EntityState.Modified;
                int i = await this._teleMedecineContext.SaveChangesAsync();
            }
            return true;
        }


        //public async Task<bool> isUserLoggedInAlready(int userId)
        //{

        //}
        public async Task<Tuple<UserMaster?, UserUsertype?, bool>> LoginUser(LoginVM login)
        {
            try
            {
                Tuple<UserMaster?, UserUsertype?, bool> oResponse = new Tuple<UserMaster?, UserUsertype?, bool>(null, null, false);
                if (login == null)
                    return oResponse;

                var userMaster = await _teleMedecineContext.UserMasters.FirstOrDefaultAsync(a => a.Email == login.Email && a.IsActive == true);
                if (userMaster == null)
                    return oResponse;

                bool resrult = EncodeAndDecordPassword.MatchPassword(login.Password, userMaster.HashPassword);
                if (!resrult)
                    return oResponse;


                var userUsertype = _teleMedecineContext.UserUsertypes.Include(a => a.UserType).FirstOrDefault(a => a.UserId == userMaster.Id);


                var isOnOtherDevice = _teleMedecineContext.LoginHistories.Any(x => x.UserId == userMaster.Id && x.LogedInTime > DateTime.UtcNow.AddHours(-1) && !x.LogedoutTime.HasValue);
                if (!isOnOtherDevice)
                {
                    userMaster.LastLoginAt = UtilityMaster.GetLocalDateTime();

                    await Update(userMaster);
                    if (userUsertype.UserTypeId == 4)
                    {
                        DoctorMaster? doctor = await _teleMedecineContext.DoctorMasters.FirstOrDefaultAsync(a => a.UserId == userUsertype.UserId);

                        if (doctor != null)
                        {
                            doctor.IsOnline = true;
                            _teleMedecineContext.Entry(doctor).State = EntityState.Modified;
                        }

                    }
                    _teleMedecineContext.SaveChanges();

                }

                oResponse = new Tuple<UserMaster?, UserUsertype?, bool>(userMaster, userUsertype, isOnOtherDevice);
                return oResponse;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> UpdateLoginHistory(string userEmail, bool logoutFromOtherDevice)
        {
            var userMaster = await _teleMedecineContext.UserMasters.FirstOrDefaultAsync(a => a.Email == userEmail);
            if (userMaster != null)
            {
                if (logoutFromOtherDevice)
                {
                    var history = _teleMedecineContext.LoginHistories.Where(x => x.UserId == userMaster.Id).OrderByDescending(x => x.LogedInTime).FirstOrDefault();
                    if (history != null)
                    {
                        //history.LogedoutTime = DateTime.UtcNow;
                        history.LogedoutTime = UtilityMaster.GetLocalDateTime();
                        _teleMedecineContext.Entry(history).State = EntityState.Modified;
                    }
                }

                var userUsertype = _teleMedecineContext.UserUsertypes.Include(a => a.UserType).FirstOrDefault(a => a.UserId == userMaster.Id);
                userMaster.LastLoginAt = UtilityMaster.GetLocalDateTime();
                await Update(userMaster);
                LoginHistory loginHistory = new LoginHistory();
                loginHistory.UserId = userUsertype.UserId;
                loginHistory.UserTypeId = userUsertype.UserTypeId;
                //loginHistory.LogedInTime = DateTime.UtcNow;
                loginHistory.LogedInTime = UtilityMaster.GetLocalDateTime();

                if (userUsertype.UserTypeId == 4)
                {
                    DoctorMaster? doctor = await _teleMedecineContext.DoctorMasters.FirstOrDefaultAsync(a => a.UserId == userUsertype.UserId);

                    if (doctor != null)
                    {
                        doctor.IsOnline = true;
                        _teleMedecineContext.Entry(doctor).State = EntityState.Modified;
                    }

                }
                _teleMedecineContext.SaveChanges();
                return true;

            }
            else
                return false;
        }

        public async Task<bool> InsertLoginHistory(string userEmail, LoginHistory loginHistory)
        {
            var userMaster = await _teleMedecineContext.UserMasters.FirstOrDefaultAsync(a => a.Email == userEmail);
            if (userMaster != null)
            {
                var userUsertype = _teleMedecineContext.UserUsertypes.Include(a => a.UserType).FirstOrDefault(a => a.UserId == userMaster.Id);

                loginHistory.UserId = userUsertype.UserId;
                loginHistory.UserTypeId = userUsertype.UserTypeId;

                _teleMedecineContext.LoginHistories.Add(loginHistory);
                _teleMedecineContext.Entry(loginHistory).State = EntityState.Added;
                _teleMedecineContext.SaveChanges();
                return true;

            }
            else
                return false;
        }
    }
}
