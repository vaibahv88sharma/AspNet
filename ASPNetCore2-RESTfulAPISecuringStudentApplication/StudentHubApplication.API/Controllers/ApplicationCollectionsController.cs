using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentHubApplication.API.Entities;
using StudentHubApplication.API.Helpers;
using StudentHubApplication.API.Models;
using StudentHubApplication.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Controllers
{
    [Authorize]
    [EnableCors("CRUDOperations")]
    [Route("api/applicationcollections")]
    [ValidateModel]
    public class ApplicationCollectionsController : Controller
    {
        private IApplicationInfoRepository _applicationInfoRepository;
        private ILogger<ApplicationCollectionsController> _logger;
        private IUrlHelper _urlHelper;
        private IPropertyMappingService _propertyMappingService;
        private ITypeHelperService _typeHelperService;
        private UserManager<CampUser> _userMgr;

        public ApplicationCollectionsController(IApplicationInfoRepository applicationInfoRepository,
            ILogger<ApplicationCollectionsController> logger,
            IUrlHelper urlHelper,
            IPropertyMappingService propertyMappingService,
            ITypeHelperService typeHelperService,
            UserManager<CampUser> userMgr)
        {
            _applicationInfoRepository = applicationInfoRepository;
            _logger = logger;
            _urlHelper = urlHelper;
            _propertyMappingService = propertyMappingService;
            _typeHelperService = typeHelperService;
            _userMgr = userMgr;
        }
        /*
        [
            {
                "firstName": "New18",
                "lastName": "Chauhan",
                "Gender": "Male",
                "dateOfBirth" : "1985-03-04T00:00:00",
                "countryOfResidenceId": 67,
                "countryOfBirthId": 68,
                "ApplicationQualifications" : [
                    {
                        "IsPrimaryQualification" : true,
                        "Notes" : "creating new Application Qualifications",
                        "QualificationId": 78
                    },
                    {
                        "IsPrimaryQualification" : true,
                        "Notes" : "creating another new Application Qualifications",
                        "QualificationId": 79
                    }        	
                ],
                "ApplicationCourseCampuses" : [
                    {
                        "IsPrimaryQualification" : true,
                        "Notes" : "creating new Application Course Campus",
                        "CourseCampusId": 144
                    },
                    {
                        "IsPrimaryQualification" : true,
                        "Notes" : "creating new Application Course Campus",
                        "CourseCampusId": 145
                    }    		
                ]
            },
            {
                "firstName": "New19",
                "lastName": "Chauhan",
                "Gender": "Male",
                "dateOfBirth" : "1985-03-04T00:00:00",
                "countryOfResidenceId": 67,
                "countryOfBirthId": 68,
                "ApplicationQualifications" : [
                    {
                        "IsPrimaryQualification" : true,
                        "Notes" : "creating new Application Qualifications",
                        "QualificationId": 78
                    },
                    {
                        "IsPrimaryQualification" : true,
                        "Notes" : "creating another new Application Qualifications",
                        "QualificationId": 79
                    }        	
                ],
                "ApplicationCourseCampuses" : [
                    {
                        "IsPrimaryQualification" : true,
                        "Notes" : "creating new Application Course Campus",
                        "CourseCampusId": 144
                    },
                    {
                        "IsPrimaryQualification" : true,
                        "Notes" : "creating new Application Course Campus",
                        "CourseCampusId": 145
                    }    		
                ]
            }	
        ]
         */
        [HttpPost]
        public async Task<IActionResult>CreateApplicationCollection(
            [FromBody] IEnumerable<ApplicationForCreationDto> applicationCollection)
        {
            if (applicationCollection == null)
            {
                return NotFound();
            }
            // AUTHENTICATION - Using Identity/User Information
            var user = await _userMgr.FindByNameAsync("shawnwildermuth");// shawnwildermuth // New1

            if (user != null)
            {
                var applicationEntities = Mapper.Map<IEnumerable<Application>>(applicationCollection);
                foreach (var application in applicationEntities)
                {
                    application.User = user;
                    _applicationInfoRepository.AddApplication(application);
                }
                if (!await _applicationInfoRepository.SaveAllAsync())
                {
                    throw new Exception("Creating an Application failed on save.");
                }
                var authorCollectionToReturn = Mapper.Map<IEnumerable<ApplicationDto>>(applicationEntities);
            }
            return Ok();
        }
    }
}
