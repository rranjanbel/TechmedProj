using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.CommanClassesAndFunctions;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;

namespace TechMed.BL.Repository.BaseClasses
{
    public class UserRepository : Repository<UserMaster>, IUserRepository
    {
        private readonly TeleMedecineContext _teleMedecineContext;       
        public UserRepository(TeleMedecineContext teleMedecineContext):base(teleMedecineContext) 
        {
            this._teleMedecineContext = teleMedecineContext;
        }
        public async Task<UserMaster> UserAuthentication(LoginVM login)
        {
            UserMaster userMaster = new UserMaster();
            try
            {

                var user = await _teleMedecineContext.UserMasters.Where(x => x.Email == login.Email && x.IsActive == true).FirstOrDefaultAsync();              
                if (user != null)
                {
                    if (EncodeAndDecordPassword.MatchPassword(login.Password, user.HashPassword))
                    {
                        userMaster.Id = user.Id;
                        userMaster.Name = user.Name; 
                        userMaster.HashPassword = user.HashPassword;
                        userMaster.Email = user.Email;
                        userMaster.Mobile = user.Mobile;
                        userMaster.LastLoginAt = DateTime.Now;
                        userMaster.IsActive = user.IsActive;
                        userMaster.LoginAttempts = user.LoginAttempts;
                        userMaster.IsPasswordChanged= user.IsPasswordChanged;

                        return userMaster;
                    }
                    else
                    {
                        userMaster.Id = user.Id;
                        userMaster.Name = user.Name;
                        userMaster.HashPassword = "not matched";
                        userMaster.Email = user.Email;
                        userMaster.Mobile = user.Mobile;
                        userMaster.LastLoginAt = DateTime.Now;
                        userMaster.IsActive = user.IsActive;
                        userMaster.LoginAttempts = user.LoginAttempts;
                        userMaster.IsPasswordChanged = user.IsPasswordChanged;                      
                        return userMaster;
                    }

                  
                }
                else
                {                                  
                    return userMaster;
                }
            }
            catch (Exception ex)
            {
                userMaster = new UserMaster();
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

        public override IQueryable<UserMaster> Add(UserMaster entity)
        {
            var existuser = _teleMedecineContext.UserMasters.Where(x => x.Email.ToUpper() == entity.Email.ToUpper()).ToList();
            if (existuser.Any())
            {
                var result = Context.Set<UserMaster>().Where(x => x.Id == entity.Id).AsQueryable<UserMaster>();
                return result;
            }
            else
            {

                _teleMedecineContext.UserMasters.Add(entity);
                _teleMedecineContext.SaveChanges();

                var result = Context.Set<UserMaster>().Where(x => x.Id == entity.Id).AsQueryable<UserMaster>();
                return result;
            }
        }
        public override IQueryable<UserMaster> Update(UserMaster entity)
        {
            var existuser = _teleMedecineContext.UserMasters.Find(entity.Id);
            if (existuser != null)
            {
                //if (entity.FirstName != null)
                //{
                //    existuser.FirstName = entity.FirstName;
                //}
                //if (entity.MiddleName != null)
                //{
                //    existuser.MiddleName = entity.MiddleName;
                //}
                //if (entity.LastName != null)
                //{
                //    existuser.LastName = entity.LastName;
                //}
                //if (entity.FatherName != null)
                //{
                //    existuser.FatherName = entity.FatherName;
                //}
                //if (entity.MotherName != null)
                //{
                //    existuser.MotherName = entity.MotherName;
                //}
                //if (entity.MobileNo > 0 && entity.MobileNo <= 12)
                //{
                //    existuser.MobileNo = entity.MobileNo;
                //}
                //if (entity.WhatsAppNo > 0 && entity.WhatsAppNo <= 12)
                //{
                //    existuser.WhatsAppNo = entity.WhatsAppNo;
                //}
                //if (entity.Address != null)
                //{
                //    existuser.Address = entity.Address;
                //}
                //if (entity.BlockId != null)
                //{
                //    existuser.BlockId = entity.BlockId;
                //}
                //if (entity.CountryId > 0)
                //{
                //    existuser.CountryId = entity.CountryId;
                //}
                //if (entity.StateId > 0)
                //{
                //    existuser.StateId = entity.StateId;
                //}
                //if (entity.DistrictId > 0)
                //{
                //    existuser.DistrictId = entity.DistrictId;
                //}


                _teleMedecineContext.SaveChanges();
                var result = Context.Set<UserMaster>().Where(x => x.Id == entity.Id).AsQueryable<UserMaster>();
                return result;
            }
            else
            {
                var result = Context.Set<UserMaster>().Where(x => x.Id == entity.Id).AsQueryable<UserMaster>();
                return result;
            }
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
    }
}
