using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    //  Attribute based API Versioning (However, Convension based API Versioning is defined in Startup.cs)
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [Route("api/applications/{applicationId}/applicationQualifications")]
    public class QualificationForApplicationController : Controller
    {
        //private IApplicationInfoRepository _applicationInfoRepository;
        //private ILogger<QualificationForApplicationController> _logger;
        //private IMailService _mailService;

            //  CHANGED ABOVE PRIVATE TO PROTECTED SO THAT IT CAN BE USED BY CHILD CONTROLER CLASS => _2ndVersionQualificationForApplicationController

        protected IApplicationInfoRepository _applicationInfoRepository;
        protected ILogger<QualificationForApplicationController> _logger;
        protected IMailService _mailService;

        public QualificationForApplicationController(IApplicationInfoRepository applicationInfoRepository,
            ILogger<QualificationForApplicationController> logger,
            IMailService mailService
            )
        {
            _applicationInfoRepository = applicationInfoRepository;
            _logger = logger;
            _mailService = mailService;
        }

        /*
         http://localhost:63292/api/applications/58/applicationQualifications
         */
        //[HttpGet("{applicationId}/qualifications")]
        [HttpGet()]
        //  Attribute based API Versioning (However, Convension based API Versioning is defined in Startup.cs)
        [MapToApiVersion("1.0")]
        public IActionResult GetQualifications(int applicationId)
        {
            var qualifications = _applicationInfoRepository.GetQualificationsForApplication(applicationId);

            var results = Mapper.Map<IEnumerable<ApplicationQualificationDto>>(qualifications);
            return Ok(results);
        }

        [HttpGet()]
        //  Attribute based API Versioning (However, Convension based API Versioning is defined in Startup.cs)
        [MapToApiVersion("1.1")]
        public virtual IActionResult GetQualificationsWithCount(int applicationId)
        {
            var qualifications = _applicationInfoRepository.GetQualificationsForApplication(applicationId);
            var count = qualifications.Count();
            var results = Mapper.Map<IEnumerable<ApplicationQualificationDto>>(qualifications);

            return Ok(new { count, results } );
        }

        [HttpGet("{id}")]
        public IActionResult GetQualification(int applicationId, int id)
        {
            var applicationQualification = _applicationInfoRepository.GetApplicationQualificationForApplication(applicationId, id);
            if(applicationQualification == null)
            {
                return NotFound();
            }
            var results = Mapper.Map<ApplicationQualificationDto>(applicationQualification);
            return Ok(results);
        }

        /*
            http://localhost:63292/api/applications/58/applicationQualifications         //    58 => applicationId
            POST => 
            [
	            23,24   => qualificationIds
            ]            
         */
        [HttpPost(Name = "CreateApplicationQualifications")]
        public IActionResult CreateApplicationQualifications(int applicationId,
            [FromBody] List<int> qualificationIds)
        {
            if (!_applicationInfoRepository.ApplicationExists(applicationId))
            {
                return NotFound();
            }

            foreach (int qualificationId in qualificationIds)
            {
                if (!_applicationInfoRepository.QualificationExists(qualificationId))
                {
                    return NotFound();
                }
                else
                {
                    _applicationInfoRepository.AddApplicationQualifications(
                        applicationId,
                        _applicationInfoRepository.GetQualification(qualificationId)
                        );
                }
            }
            if (!_applicationInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
            else
            {
                _mailService.Send("Additional Qualifications are linked to an Applicant",
                       $"The application id is: {applicationId} ");
            }

            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteApplicationQualification")]
        public IActionResult DeleteApplicationQualification(int applicationId, int id)
        {
            if (!_applicationInfoRepository.ApplicationExists(applicationId))
            {
                return NotFound();
            }

            var applicationQualificationFromRepo = _applicationInfoRepository.GetApplicationQualificationForApplication(applicationId, id);
            if (applicationQualificationFromRepo == null)
            {
                return NotFound();
            }

            _applicationInfoRepository.DeleteApplicationQualificationForApplication(applicationQualificationFromRepo);
            if (!_applicationInfoRepository.Save())
            {
                throw new Exception("Deleting ApplicationQualification for Application failed on save.");
            }
            return NoContent();
        }
    }
}
