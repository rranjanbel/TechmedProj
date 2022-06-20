using AutoMapper;
using System.Security.Claims;
using TechMed.BL.CommanClassesAndFunctions;
using TechMed.BL.ModelMaster;
using TechMed.BL.Repository.Interfaces;
using TechMed.BL.ViewModels;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;
using TechMedAPI.JwtInfra;

namespace TechMed.API.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        UserBusinessMaster userBusinessMaster;
        private readonly IUserRepository _userRepository;
        private readonly IJwtAuthManager _jwtAuthManager;

        public UserService(IMapper mapper, TeleMedecineContext teleMedecineContext, ILogger<UserService> logger, IUserRepository userRepository, IJwtAuthManager jwtAuthManager)
        {
            this._mapper = mapper;
            _logger = logger;
            userBusinessMaster = new UserBusinessMaster(teleMedecineContext, mapper);
            this._userRepository = userRepository;
            _jwtAuthManager = jwtAuthManager;
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

        public async Task<LoggedUserDetails> Authenticate(LoginVM login)
        {
            LoggedUserDetails userDetails = new LoggedUserDetails();
            if (login != null)
            {
                userDetails = await _userRepository.AuthenticateUser(login);
            }
            return userDetails;

        }

        public async Task<bool> LogoutUsers(string userEmail)
        {
            bool result = await _userRepository.LogoutUsers(userEmail);
            return result;
        }

        public async Task<LoginResult?> LoginUser(LoginVM login)
        {
            var userInfo = await _userRepository.LoginUser(login);
            if (userInfo.Item1 == null)
                return null;

            var claims = new[]{
                new Claim(ClaimTypes.Name,login.Email)
            };

            var jwtResult = _jwtAuthManager.GenerateTokens(login.Email, claims, UtilityMaster.GetLocalDateTime());
            var result = new LoginResult()
            {
                AccessToken = jwtResult.AccessToken,
                RefreshToken = jwtResult.RefreshToken.TokenString,
                isOnOtherDevice = userInfo.Item3,
                UserName = login.Email,
                roleName = userInfo?.Item2?.UserType.UserType,
                isPasswordChanged = userInfo?.Item1?.IsPasswordChanged,
            };
            if (userInfo.Item3 == false)
            {
                await _userRepository.InsertLoginHistory(login.Email, new LoginHistory()
                {
                    //LogedInTime = DateTime.UtcNow,
                    LogedInTime = UtilityMaster.GetLocalDateTime(),
                    UserToken = jwtResult.AccessToken,
                    
                });
            }

            return result;

        }

        public async Task<bool> UpdateLoginHistory(string userEmail, bool logoutFromOtherDevice)
        {
            return await _userRepository.UpdateLoginHistory(userEmail, logoutFromOtherDevice);
        }

        public async Task<bool> InsertLoginHistory(string userEmail, string token)
        {
            return await _userRepository.InsertLoginHistory(userEmail, new LoginHistory()
            {
                LogedInTime = DateTime.UtcNow,
                UserToken = token
            });
        }

    }
}
