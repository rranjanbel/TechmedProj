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
        UserBusinessMaster userBusinessMaster;
        private readonly IUserRepository _userRepository;
        public UserMasterController(IMapper mapper, TeleMedecineContext teleMedecineContext, IUserRepository userRepository)
        {
            this._mapper = mapper;
            userBusinessMaster = new UserBusinessMaster(teleMedecineContext, mapper);
            this._userRepository = userRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<UserLoginDTO>))]
        public IActionResult GetUsers()
        {
            var userList = userBusinessMaster.GetUserMasters();
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
                return BadRequest();
            }

        }

        [HttpPost]
        public async Task<UserLoginDTO> GetAuthenticatedUser(LoginVM login)
        {
            //UserMaster user = new UserMaster();
            return await _userRepository.UserAuthentication(login);
        }

    }
}
