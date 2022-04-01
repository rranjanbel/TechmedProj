using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TechMed.BL.DTOMaster;
using TechMed.BL.ModelMaster;

namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMasterController : ControllerBase
    {
        private readonly IMapper _mapper;
        UserBusinessMaster userBusinessMaster = new UserBusinessMaster();
        public UserMasterController( IMapper mapper)
        {
           this._mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200,Type = typeof(List<UserLoginDTO>))]
        public IActionResult GetUsers()
        {
            var userList = userBusinessMaster.GetUserMasters();
            var userDTOList = new List<UserLoginDTO>();
            if(userList != null)
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
    }
}
