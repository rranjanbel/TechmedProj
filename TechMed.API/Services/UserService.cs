using AutoMapper;
using TechMed.BL.ModelMaster;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;

namespace TechMed.API.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        UserBusinessMaster userBusinessMaster;
        private readonly IUserRepository _userRepository;
        public UserService(IMapper mapper, TeleMedecineContext teleMedecineContext, ILogger<UserService> logger, IUserRepository userRepository)
        {
            this._mapper = mapper;
            _logger=logger;
            userBusinessMaster = new UserBusinessMaster(teleMedecineContext, mapper);
            this._userRepository=userRepository;
        }


        public async Task<bool> IsAnExistingUser(string userEmail)
        {
            bool isExist = false;
            //var userList = userBusinessMaster.GetUserMasters();
            //if (userList == null)
            //{
            //    isExist = userList.Any(x => x.Email == userName);
            //}
            isExist = await _userRepository.IsAnExistingUser(userEmail);
            return isExist;
        }

        public async Task<bool> IsValidUserCredentials(string userName, string password)
        {
           // var userList = userBusinessMaster.GetUserMasters();
            _logger.LogInformation($"Validating user [{userName}]");
            if (string.IsNullOrWhiteSpace(userName))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            LoginVM login = new LoginVM { Email = userName, Password = password };
            bool isValid = await _userRepository.IsValidUser(login);

            return isValid;
        }
    }
}
