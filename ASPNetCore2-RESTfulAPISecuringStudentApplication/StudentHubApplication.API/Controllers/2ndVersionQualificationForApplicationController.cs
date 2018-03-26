using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentHubApplication.API.Models;
using StudentHubApplication.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Controllers
{
    [Route("api/applications/{applicationId}/applicationQualifications")]
    //  Attribute based API Versioning (However, Convension based API Versioning is defined in Startup.cs)
    [ApiVersion("2.0")]
    public class _2ndVersionQualificationForApplicationController : QualificationForApplicationController
    {
        public _2ndVersionQualificationForApplicationController(IApplicationInfoRepository applicationInfoRepository,
            ILogger<QualificationForApplicationController> logger,
            IMailService mailService
            ) : base(applicationInfoRepository, logger, mailService)
        {

        }

        public override IActionResult GetQualificationsWithCount(int applicationId)
        {
            var qualifications = _applicationInfoRepository.GetQualificationsForApplication(applicationId);
            var count = qualifications.Count();
            var results = Mapper.Map<IEnumerable<_2ndVersionApplicationQualificationDto>>(qualifications);

            return Ok(new {
                    count,
                    currentTime = DateTime.Now,
                    results
                }
            );
        }

    }
}
