using AutoMapper;
using TechMed.BL.ModelMaster;
using TechMed.DL.Models;

namespace TechMed.API.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        UserBusinessMaster userBusinessMaster;
        public UserService(IMapper mapper, TeleMedecineContext teleMedecineContext, ILogger<UserService> logger)
        {
            this._mapper = mapper;
            _logger=logger;
            userBusinessMaster = new UserBusinessMaster(teleMedecineContext, mapper);
        }


        public bool IsAnExistingUser(string userName)
        {
            bool isExist = false;
            var userList = userBusinessMaster.GetUserMasters();
            if (userList == null)
            {
                isExist = userList.Any(x => x.Email == userName);
            }
            return isExist;
        }

        public bool IsValidUserCredentials(string userName, string password)
        {
            var userList = userBusinessMaster.GetUserMasters();
            _logger.LogInformation($"Validating user [{userName}]");
            if (string.IsNullOrWhiteSpace(userName))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            return userList.Any(x => x.Email == userName && x.HashPassword == password);
        }
    }
}
