using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.Models;

namespace TechMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MISController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDashBoardRepository _dashBoardRepository;
        public MISController(IMapper mapper, TeleMedecineContext teleMedecineContext, IDashBoardRepository dashBoardRepository)
        {
            _dashBoardRepository = dashBoardRepository;
            _mapper = mapper;
        }
    }
}
