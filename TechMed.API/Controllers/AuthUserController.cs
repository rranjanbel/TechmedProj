﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechMed.API.Services;
using TechMedAPI.JwtInfra;
using Microsoft.IdentityModel.Tokens;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.ViewModel;
using TechMed.BL.ViewModels;

namespace TechMed.API.Controllers
{
    public class AuthUserController : Controller
    {
        private readonly ILogger<AuthUserController> _logger;
        private readonly IUserService _userService;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly IDoctorRepository _doctorRepository;
        public AuthUserController(ILogger<AuthUserController> logger, IDoctorRepository doctorRepository, IUserService userService, IJwtAuthManager jwtAuthManager)
        {
            _logger = logger;
            _userService = userService;
            _jwtAuthManager = jwtAuthManager;
            _doctorRepository = doctorRepository;
        }
        [AllowAnonymous]
        [HttpPost("api/generatetoken")]
        public async Task<ActionResult> GenerateToken([FromBody] LoginRequest request)
        {           

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }          
            //userDetails = _userService.

            if (!await _userService.IsValidUserCredentials(request.UserName, request.Password))
            {
                return Unauthorized("Unauthorized User");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,request.UserName)
            };

            var jwtResult = _jwtAuthManager.GenerateTokens(request.UserName, claims, DateTime.Now);
            _logger.LogInformation($"User [{request.UserName}] logged in the system.");
            return Ok(new LoginResult
            {
                UserName = request.UserName,
                AccessToken = jwtResult.AccessToken,
                RefreshToken = jwtResult.RefreshToken.TokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("api/AuthenticateUser")]       
        [ProducesResponseType(200, Type = typeof(List<LoggedUserDetails>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AuthenticateUser([FromBody] LoginVM login)
        {
            LoggedUserDetails userDetails = new LoggedUserDetails();           

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
          
            userDetails = await _userService.Authenticate(login);

            if (userDetails == null)
            {
                return Unauthorized("Unauthorized User");
            }
            else
            {
                var claims = new[]
                 {
                    new Claim(ClaimTypes.Name,userDetails.UserName)
                 };

                var jwtResult = _jwtAuthManager.GenerateTokens(userDetails.UserName, claims, DateTime.Now);
                userDetails.AccessToken = jwtResult.AccessToken;
                userDetails.RefreshToken = jwtResult.RefreshToken.TokenString;
                _logger.LogInformation($"User [{userDetails.UserName}] logged in the system.");
                return Ok(userDetails);
            }

           
        }
        [HttpPost("api/logout")]
        [Authorize]
        public ActionResult Logout()
        {
            var userName = User.Identity?.Name;
            _jwtAuthManager.RemoveRefreshTokenByUserName(userName);
            _logger.LogInformation($"User [{userName}] logged out the system.");
            //_doctorRepository.UpdateIsDrOnlineByUserLoginName(new UpdateIsDrOnlineByUserLoginNameVM
            //{
            //    IsOnline = false,
            //    UserLoginName = userName
            //});
            return Ok();
        }
        [HttpPost("api/refresh-token")]
        [Authorize]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var userName = User.Identity?.Name;
                _logger.LogInformation($"User [{userName}] is trying to refresh JWT token.");

                if (string.IsNullOrWhiteSpace(request.RefreshToken))
                {
                    return Unauthorized();
                }

                var accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");
                var jwtResult = _jwtAuthManager.Refresh(request.RefreshToken, accessToken, DateTime.Now);
                _logger.LogInformation($"User [{userName}] has refreshed JWT token.");
                return Ok(new LoginResult
                {
                    UserName = userName,
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString
                });
            }
            catch (SecurityTokenException e)
            {
                return Unauthorized(e.Message); // return 401 so that the client side can redirect the user to login page
            }
        }
    }
}
