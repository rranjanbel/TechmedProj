using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TechMed.BL.DTOMaster;
using TechMed.BL.ModelMaster;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;
using TechMed.BL.Repository;
using TechMed.BL.Repository.Interfaces;
using TechMed.BL.Repository.BaseClasses;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserMasterController : ControllerBase
    {
        private readonly IMapper _mapper;
        //UserBusinessMaster userBusinessMaster;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserMasterController> _logger;
        public UserMasterController(IMapper mapper, TeleMedecineContext teleMedecineContext, IUserRepository userRepository, ILogger<UserMasterController> logger)
        {
            this._mapper = mapper;
            //userBusinessMaster = new UserBusinessMaster(teleMedecineContext, mapper);
            this._userRepository = userRepository;
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<UserLoginDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                List<UserMaster> userList = new List<UserMaster>();
                userList = await _userRepository.GetAll();
                var userDTOList = new List<UserLoginDTO>();
                if (userList != null)
                {
                    foreach (var user in userList)
                    {
                        userDTOList.Add(_mapper.Map<UserLoginDTO>(user));
                    }
                    return Ok(userDTOList);
                }
                else
                {
                    ModelState.AddModelError("GetUsers", "User list not found");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("GetUsers", $"Something went wrong when create park {ex.Message}");
                _logger.LogError("Exception in GetUsers API " + ex);
                return StatusCode(500, ModelState);
            }
           

        }
      

        [HttpPost]
        [Route("IsValidUser")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> IsValidUser(LoginVM login)
        {
            bool response = false;
            try
            {
                response = await this._userRepository.IsValidUser(login);
                if (response)
                {
                    return Ok(response);
                }
                else
                {
                    ModelState.AddModelError("IsValidUser", $"User validation fail for email : {login.Email}");
                    return StatusCode(404, ModelState);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("IsValidUser", $"Something went wrong when create park {ex.Message}");
                _logger.LogError("Exception in IsValidUser API " + ex);
                return StatusCode(500, ModelState);
            }

        }



        [HttpGet]
        [Route("LogedUserDetails")]
        [ProducesResponseType(200, Type = typeof(UserLoginDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LogedUserDetails(string userEmail)
        {
            try
            {
                if (userEmail == null)
                {
                    return BadRequest(ModelState);
                }
                var userLoginDTO = new UserLoginDTO();
                userLoginDTO = await _userRepository.LogedUserDetails(userEmail);
                if (userLoginDTO.Id > 0)
                {
                    return Ok(userLoginDTO);
                }
                else
                {
                    ModelState.AddModelError("LogedUserDetails", $"User email did not match");
                    return StatusCode(404,ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("LogedUserDetails", $"Something went wrong when get Loged User Details {ex.Message}");
                _logger.LogError("Exception in LogedUserDetails API " + ex);
                return StatusCode(500, ModelState);
            }
           
            
            

        }

        [HttpPost]
        [Route("ChangePassword")]
        [ProducesResponseType(200, Type = typeof(ChangePassword))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangePassword(ChangePassword login)
        {
            bool response = false;
            try
            {
                if (login.OldPassword==login.NewPassword)
                {
                    ModelState.AddModelError("ChangeUserPassword", $"New password should not be same as old password : {login.UserNameOrEmail}");
                    return StatusCode(400, ModelState);
                }
                ChangePassword userMaster = new ChangePassword();
                userMaster = await this._userRepository.ChangeUserPassword(login);
                if (userMaster != null)
                {
                    response = true;
                    return Ok(userMaster);
                }
                else
                {
                    ModelState.AddModelError("ChangeUserPassword", $"User password change fail for user ID  : {login.UserNameOrEmail}");
                    return StatusCode(404, ModelState);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ChangeUserPassword", $"Something went wrong when change password {ex.Message}");
                _logger.LogError("Exception in ChangeUserPassword API " + ex);
                return StatusCode(500, ModelState);
            }

        }

        [HttpGet]
        [Route("GetUserRole")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserRole(string userEmail)
        {
            try
            {
                if (userEmail == null)
                {
                    ModelState.AddModelError("LogedUserDetails", $"User email has null value");
                    return BadRequest(ModelState);
                }
                string userRole = string.Empty;
                userRole = await _userRepository.GetUserRole(userEmail);
                if (userRole != string.Empty)
                {
                    return Ok(userRole);
                }
                else
                {
                    ModelState.AddModelError("LogedUserDetails", $"User role not found.");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("LogedUserDetails", $"Something went wrong when get user role {ex.Message}");
                _logger.LogError("Exception in LogedUserDetails API " + ex);
                return StatusCode(500, ModelState);
            }
        }

        [HttpGet]
        [Route("UpdateUserLastAlive")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserLastAlive()
        {
            bool response = false;
            try
            {
                response = await this._userRepository.UpdateUserLastAliveUpdate();
                if (response)
                {
                    return Ok(response);
                }
                else
                {
                    ModelState.AddModelError("UpdateUserLastAliveUpdate", $"User validation fail");
                    return StatusCode(404, ModelState);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("UpdateUserLastAliveUpdate", $"Something went wrong : {ex.Message}");
                _logger.LogError("Exception in IsValidUser API " + ex);
                return StatusCode(500, ModelState);
            }

        }

    }
}
