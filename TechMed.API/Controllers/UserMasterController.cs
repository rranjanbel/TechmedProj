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
    //[Authorize]
    public class UserMasterController : ControllerBase
    {
        private readonly IMapper _mapper;
        //UserBusinessMaster userBusinessMaster;
        private readonly IUserRepository _userRepository;
        public UserMasterController(IMapper mapper, TeleMedecineContext teleMedecineContext, IUserRepository userRepository)
        {
            this._mapper = mapper;
            //userBusinessMaster = new UserBusinessMaster(teleMedecineContext, mapper);
            this._userRepository = userRepository;
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
                    ModelState.AddModelError("", "User list not found");
                    return StatusCode(404, ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong when create park {ex.Message}");
                return StatusCode(500, ModelState);
            }
           

        }

        [HttpGet]
        [Route("IsValidUser")]
        public async Task<bool> IsValidUser(LoginVM login)
        {
            return await this._userRepository.IsValidUser(login);
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
                    ModelState.AddModelError("", $"User email did not match");
                    return StatusCode(404,ModelState);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", $"Something went wrong when create park {ex.Message}");
                return StatusCode(500, ModelState);
            }
           
            
            

        }

    }
}
