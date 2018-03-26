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
    //[Route("api/applications")]
    public class QualificationsController : Controller
    {
        private IApplicationInfoRepository _applicationInfoRepository;
        private ILogger<QualificationsController> _logger;
        private IMailService _mailService;

        public QualificationsController(IApplicationInfoRepository applicationInfoRepository, 
            ILogger<QualificationsController> logger,
            IMailService mailService
            )
        {
            _applicationInfoRepository = applicationInfoRepository;
            _logger = logger;
            _mailService = mailService;
        }

        /*
         http://localhost:63292/api/applications/58/qualifications
         */
        //[HttpGet("{applicationId}/qualifications")]
        //public IActionResult GetQualifications(int applicationId)
        //{
        //        var qualifications = _applicationInfoRepository.GetQualificationsForApplication(applicationId);

        //        var results = Mapper.Map<IEnumerable<ApplicationQualificationDto>>(qualifications);
        //        return Ok(results);
        //}

        /*
            http://localhost:63292/api/applications/58/qualifications         //    58 => applicationId
            POST => 
            [
	            23,24   => qualificationIds
            ]
            
         */
        //[HttpPost("{applicationId}/Qualifications")]
        //public IActionResult CreateApplicationQualifications(int applicationId,
        //    [FromBody] List<int> qualificationIds)
        //{
        //    if (!_applicationInfoRepository.ApplicationExists(applicationId))
        //    {
        //        return NotFound();
        //    }

        //    foreach (int qualificationId in qualificationIds)
        //    {
        //        if (!_applicationInfoRepository.QualificationExists(qualificationId))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            _applicationInfoRepository.AddApplicationQualifications(
        //                applicationId, 
        //                _applicationInfoRepository.GetQualification(qualificationId)
        //                );
        //        }
        //    }
        //    if (!_applicationInfoRepository.Save())
        //    {
        //        return StatusCode(500, "A problem happened while handling your request.");
        //    }
        //    else
        //    {
        //        _mailService.Send("Additional Qualifications are linked to an Applicant",
        //               $"The application id is: {applicationId} ");
        //    }

        //    return Ok();
        //}

        //[HttpDelete("{applicationId}/Qualifications", Name = "DeleteQualification")]
        //public IActionResult DeleteQualification(int applicationId, int id)
        //{
        //    if (_applicationInfoRepository.ApplicationExists(id))
        //    {
        //        return NotFound();
        //    }
        //    var applicationQualificationFromRepo = _applicationInfoRepository.GetQualificationForApplication(applicationId);
        //    if(applicationQualificationFromRepo == null)
        //    _applicationInfoRepository.DeleteQualificationForApplication(applicationQualificationFromRepo);
        //    if (!_applicationInfoRepository.Save())
        //    {
        //        throw new Exception("Creating an Application failed on save.");
        //    }
        //    return NoContent();
        //}

        /*
         http://localhost:63292/api/applications/58/qualifications
         {                                  Request Body => http://localhost:63292/api/applications/58/qualifications
            "Name" : "Diploma"              Request Body => http://localhost:63292/api/applications/58/qualifications
         }                                  Request Body => http://localhost:63292/api/applications/58/qualifications
         */
        [HttpPost("{applicationId}/createQualifications")]
        public IActionResult CreateQualifications(int applicationId,
            [FromBody] QualificationForQualificationCreationDto qualification)
        {
            if (qualification == null)
            {
                return BadRequest();
            }
            // Built-in Error Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_applicationInfoRepository.ApplicationExists(applicationId))
            {
                return NotFound();
            }

            var finalQualification = Mapper.Map<Entities.Qualification>(qualification);
            _applicationInfoRepository.AddQualification(applicationId, finalQualification);
            if (!_applicationInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
            return Ok();
        }
    }
}
