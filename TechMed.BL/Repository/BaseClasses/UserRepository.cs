﻿using AutoMapper;
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

        public bool ResetUserPassword(long UserID)
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
        public bool SetUserPassword(long UserID, string Password)
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

        public bool DeleteUser(long UserId)
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

        public bool ChangeUserPassword(ChangePassword changePassword)
        {
            var existingUser = _teleMedecineContext.UserMasters.FirstOrDefault(x => x.Id == changePassword.UserId && x.IsActive == true);
            if (existingUser != null)
            {
                existingUser.HashPassword = EncodeAndDecordPassword.EncodePassword(changePassword.NewPassword);
                _teleMedecineContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> IsValidUser(LoginVM login)
        {           
            var isValidUser = await _teleMedecineContext.UserMasters.AnyAsync(a => a.Email == login.Email && a.HashPassword == login.Password && a.IsActive == true);
            return isValidUser;
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
    }
}
