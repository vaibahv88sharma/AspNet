using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentHubApplication.API.Entities;
using StudentHubApplication.API.Models;
using StudentHubApplication.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/applications/{applicationId}/[controller]")]
    public class CampsController : BaseController
    {
        private ICampRepository _campRepository;
        private ILogger<CampsController> _logger;
        //IMapper _mapper;

        public CampsController(ICampRepository campRepository,//, IMapper mapper
            ILogger<CampsController> logger
            )
        {
            _campRepository = campRepository;
            //_mapper = mapper;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var camps = _campRepository.GetAllCamps();
                //return Ok(camps);
                return Ok(Mapper.Map<IEnumerable<CampModel>>(camps));
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                return BadRequest();
            }
        }

        [HttpGet("{id}", Name = "CampGet")]
        public IActionResult Get(int id, bool includeSpeakers = false)
        {
            try
            {
                Camp camp = null;
                if (includeSpeakers)
                {
                    camp = _campRepository.GetCampWithSpeakers(id);
                }
                else
                {
                    camp = _campRepository.GetCamp(id);
                }
                if (camp == null) return NotFound($"Camp {id} was not found");

                //return Ok(camp);
                return Ok(Mapper.Map<CampModel>(camp));
                //return Ok(Mapper.Map<CampModel>(camp, opt => opt.Items["UrlHelper"] = this.Url));
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                return BadRequest();
            }
        }
    }
}
