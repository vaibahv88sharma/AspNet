using AutoMapper;
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
    [Route("api/applications")]
    public class ApplicationsController : Controller
    {
        private IApplicationInfoRepository _applicationInfoRepository;
        private ILogger<ApplicationsController> _logger;
        private IUrlHelper _urlHelper;
        private IPropertyMappingService _propertyMappingService;
        private ITypeHelperService _typeHelperService;

        public ApplicationsController(IApplicationInfoRepository applicationInfoRepository, 
            ILogger<ApplicationsController> logger,
            IUrlHelper urlHelper,
            IPropertyMappingService propertyMappingService,
            ITypeHelperService typeHelperService)
        {
            _applicationInfoRepository = applicationInfoRepository;
            _logger = logger;
            _urlHelper = urlHelper;
            _propertyMappingService = propertyMappingService;
            _typeHelperService = typeHelperService;
        }

        /*
         http://localhost:63292/api/applications?searchQuery=Priyanka&gender=Female&pageNumber=1&pageSize=5
         */
        [HttpGet(Name = "GetApplications")]
        public IActionResult GetApplications(
            ApplicationsResourceParameters applicationsResourceParameters, //  AuthorsResourceParameters => URL Optional Input Parameters for Paging and filtering
            [FromHeader(Name = "Accept")] string mediaType                  //  HATEOAS and Content Negotiation => Accept : //application/vnd.vaibhav.hateoas+json
            )
        {
            try
            {
                // Check if non existing property has been requested
                //  This is dynamic Mapping of AuthorDto to Entity.Author
                // e.g. http://localhost:6058/api/applications?orderBy=dateofbirth   => In this case => 'dateofbirth' does not exist
                if (!_propertyMappingService.ValidMappingExistsFor<ApplicationDto, Application>
                   (applicationsResourceParameters.OrderBy))
                {
                    return BadRequest();
                }

                if (!_typeHelperService.TypeHasProperties<ApplicationDto>
                    (applicationsResourceParameters.Fields))
                {
                    return BadRequest();
                }
                var applicationsFromRepo = _applicationInfoRepository.GetApplications(applicationsResourceParameters);
                var results = Mapper.Map<IEnumerable<ApplicationDto>>(applicationsFromRepo);

                #region Wrapping  HATEOAS (Dynamic Approach) in Content Negotiation => Accept : application/vnd.vaibhav.hateoas+json
                if (mediaType == "application/vnd.vaibhav.hateoas+json")
                {
                    var paginationMetadata = new
                    {
                        totalCount = applicationsFromRepo.TotalCount,
                        pageSize = applicationsFromRepo.PageSize,
                        currentPage = applicationsFromRepo.CurrentPage,
                        totalPages = applicationsFromRepo.TotalPages
                    };
                    Response.Headers.Add("X-Pagination",
                        Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

                    #region Supporting HATEOAS (Dynamic Approach)
                    var links = CreateLinksForApplications(applicationsResourceParameters,
                                        applicationsFromRepo.HasNext, applicationsFromRepo.HasPrevious);
                    var shapedApplications = results.ShapeData(applicationsResourceParameters.Fields);
                    var shapedApplicationsWithLinks = shapedApplications.Select(application =>
                    {
                        var applicationsAsDictionary = application as IDictionary<string, object>;
                        var authorLinks = CreateLinksForApplication(
                            (Int32)applicationsAsDictionary["Id"], applicationsResourceParameters.Fields);

                        applicationsAsDictionary.Add("links", authorLinks);

                        return applicationsAsDictionary;
                    });

                    var linkedCollectionResource = new
                    {
                        value = shapedApplicationsWithLinks,
                        links = links
                    };

                    return Ok(linkedCollectionResource);
                    #endregion
                }
                #endregion
                #region Accept : application/json
                else
                {
                    var previousPageLink = applicationsFromRepo.HasPrevious ?
                        CreateApplicationsResourceUri(applicationsResourceParameters,
                        ResourceUriType.PreviousPage) : null;

                    var nextPageLink = applicationsFromRepo.HasNext ?
                        CreateApplicationsResourceUri(applicationsResourceParameters,
                        ResourceUriType.NextPage) : null;

                    var paginationMetadata = new
                    {
                        totalCount = applicationsFromRepo.TotalCount,
                        pageSize = applicationsFromRepo.PageSize,
                        currentPage = applicationsFromRepo.CurrentPage,
                        totalPages = applicationsFromRepo.TotalPages,
                        previousPageLink = previousPageLink,
                        nextPageLink = nextPageLink
                    };
                    Response.Headers.Add("X-Pagination",
                        Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
                    return Ok(results.ShapeData(applicationsResourceParameters.Fields));
                }
                #endregion

                //return Ok(results.ShapeData(applicationsResourceParameters.Fields));

                // Before SHAPING of data
                //return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                throw;
            }
        }

        /*
         http://localhost:63292/api/applications/61
         */
        [HttpGet("{id}", Name = "GetApplication")]
        public IActionResult GetApplication(int id, [FromQuery] string fields, bool includeQualification = false)
        {
            if (!_typeHelperService.TypeHasProperties<ApplicationDto>
              (fields))
            {
                return BadRequest();
            }
            var applicationFromRepo = _applicationInfoRepository.GetApplication(id, includeQualification);
            if (applicationFromRepo == null)
            {
                return NotFound();
            }
            if (includeQualification)
            {
                //var resultsWithQualifications = Mapper.Map<ApplicationWithQualificationDto>(applications);        NOT USED, but should be used for http://localhost:63292/api/applications/69?includeQualification=true
                //return Ok(resultsWithQualifications);                                                             NOT USED, but should be used for http://localhost:63292/api/applications/69?includeQualification=true
            }
            var results = Mapper.Map<ApplicationDto>(applicationFromRepo);

            #region Supporting HATEOAS (Dynamic Approach)

            var links = CreateLinksForApplication(id, fields);
            var linkedResourceToReturn = results.ShapeData(fields)
                as IDictionary<string, object>;
            linkedResourceToReturn.Add("links", links);

            #endregion

            return Ok(linkedResourceToReturn);
            //return Ok(results);
        }

        /*
         http://localhost:63292/api/applications/
            {
                "firstName": "Vibhor",
                "lastName": "Chauhan",
		        "dateOfBirth" : "1985-03-04T00:00:00",
                "countryOfResidenceId": 173,
                "countryOfBirthId": 174,
                "ApplicationQualifications" : [
        	        {
        		        "IsPrimaryQualification" : true,
        		        "Notes" : "creating new Application Qualifications",
        		        "QualificationId": 387
        	        },
        	        {
        		        "IsPrimaryQualification" : true,
        		        "Notes" : "creating another new Application Qualifications",
        		        "QualificationId": 388
        	        }        	
    	        ],
    	        "ApplicationCourseCampuses" : [
    		        {
        		        "IsPrimaryQualification" : true,
        		        "Notes" : "creating new Application Course Campus",
        		        "CourseCampusId": 718
    		        },
    		        {
        		        "IsPrimaryQualification" : true,
        		        "Notes" : "creating new Application Course Campus",
        		        "CourseCampusId": 719
    		        }    		
		        ]
            }         
             */
        [HttpPost(Name = "CreateApplication")]
        public IActionResult CreateApplication([FromBody] ApplicationForCreationDto application)
        {
            if (application == null)
            {
                return NotFound();
            }
            var applicationEntity = Mapper.Map<Application>(application);
            _applicationInfoRepository.AddApplication(applicationEntity);//, applicationEntity.CountryOfResidenceId, applicationEntity.CountryOfBirthId);

            if (!_applicationInfoRepository.Save())
            {
                throw new Exception("Creating an Application failed on save.");
            }
            var applicationCreated = _applicationInfoRepository.GetApplication(applicationEntity.Id, false);
            var applicationToReturn = Mapper.Map<ApplicationDto>(applicationCreated);

            return CreatedAtRoute("GetApplication",
                new { id = applicationToReturn.Id },
                applicationToReturn);
        }

        [HttpDelete("{id}", Name = "DeleteApplication")]
        public IActionResult DeleteApplication(int id)
        {
            var application = _applicationInfoRepository.GetApplication(id, false);
            if (application == null)
            {
                return NotFound();
            }
            _applicationInfoRepository.DeleteApplication(application);
            if (!_applicationInfoRepository.Save())
            {
                throw new Exception("Creating an Application failed on save.");
            }
            return NoContent();
        }

        private string CreateApplicationsResourceUri(
            ApplicationsResourceParameters applicationsResourceParameters,
            ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return _urlHelper.Link("GetApplications",
                      new
                      {
                          fields = applicationsResourceParameters.Fields,
                          orderBy = applicationsResourceParameters.OrderBy,
                          searchQuery = applicationsResourceParameters.SearchQuery,
                          gender = applicationsResourceParameters.Gender,
                          pageNumber = applicationsResourceParameters.PageNumber - 1,
                          pageSize = applicationsResourceParameters.PageSize
                      });
                case ResourceUriType.NextPage:
                    return _urlHelper.Link("GetApplications",
                      new
                      {
                          fields = applicationsResourceParameters.Fields,
                          orderBy = applicationsResourceParameters.OrderBy,
                          searchQuery = applicationsResourceParameters.SearchQuery,
                          gender = applicationsResourceParameters.Gender,
                          pageNumber = applicationsResourceParameters.PageNumber + 1,
                          pageSize = applicationsResourceParameters.PageSize
                      });

                default:
                    return _urlHelper.Link("GetApplications",
                    new
                    {
                        fields = applicationsResourceParameters.Fields,
                        orderBy = applicationsResourceParameters.OrderBy,
                        searchQuery = applicationsResourceParameters.SearchQuery,
                        gender = applicationsResourceParameters.Gender,
                        pageNumber = applicationsResourceParameters.PageNumber,
                        pageSize = applicationsResourceParameters.PageSize
                    });
            }
        }

        #region Supporting HATEOAS (Dynamic Approach)
        private IEnumerable<LinkDto> CreateLinksForApplication(int id, string fields)
        {
            var links = new List<LinkDto>();
            if (string.IsNullOrWhiteSpace(fields))
            {
                links.Add(
                  new LinkDto(_urlHelper.Link("GetApplication", new { id = id }),
                  "self",
                  "GET"));
            }
            else
            {
                links.Add(
                  new LinkDto(_urlHelper.Link("GetApplication", new { id = id, fields = fields }),
                  "self",
                  "GET"));
            }

            links.Add(
              new LinkDto(_urlHelper.Link("DeleteApplication", new { id = id }),
              "delete_application",
              "DELETE"));

            links.Add(
              new LinkDto(_urlHelper.Link("CreateCourseCampusForApplication", new { applicationId = id }),
              "create_coursecampus_for_application",
              "POST"));

            links.Add(
               new LinkDto(_urlHelper.Link("GetApplicationCourseCampuses", new { applicationId = id }),
               "coursecampuses",
               "GET"));

            return links;
        }

        private IEnumerable<LinkDto> CreateLinksForApplications(
            ApplicationsResourceParameters applicationsResourceParameters,
            bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>();

            // self 
            links.Add(
               new LinkDto(CreateApplicationsResourceUri(applicationsResourceParameters,
               ResourceUriType.Current)
               , "self", "GET"));

            if (hasNext)
            {
                links.Add(
                  new LinkDto(CreateApplicationsResourceUri(applicationsResourceParameters,
                  ResourceUriType.NextPage),
                  "nextPage", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(
                    new LinkDto(CreateApplicationsResourceUri(applicationsResourceParameters,
                    ResourceUriType.PreviousPage),
                    "previousPage", "GET"));
            }

            return links;
        }
        #endregion

    }
}
