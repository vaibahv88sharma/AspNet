using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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
    [Route("api/applications/{applicationId}/courseCampusesForApplication")]
    public class CourseCampusesForApplicationController : Controller
    {
        private IApplicationInfoRepository _applicationInfoRepository;
        private ILogger<CourseCampusesForApplicationController> _logger;
        private IUrlHelper _urlHelper;
        public CourseCampusesForApplicationController(IApplicationInfoRepository applicationInfoRepository,
            ILogger<CourseCampusesForApplicationController> logger,
            IUrlHelper urlHelper)
        {
            _applicationInfoRepository = applicationInfoRepository;
            _logger = logger;
            _urlHelper = urlHelper;
        }

        /*
 http://localhost:63292/api/applications/47/courseCampusesForApplication
 */
        //[HttpGet("{applicationId}/courseCampusesForApplication")]
        [HttpGet(Name = "GetApplicationCourseCampuses")]
        [HttpHead]  // Do not transport content/body in response only Returns 200 Status Code & Headers 
        public IActionResult GetApplicationCourseCampuses(int applicationId)
        {
            if (!_applicationInfoRepository.ApplicationExists(applicationId))
            {
                return NotFound();
            }
            var applicationCourseCampuses = _applicationInfoRepository.GetCourseCampusesForApplication(applicationId);
            if (applicationCourseCampuses == null)
            {
                return NotFound();
            }

            var results = Mapper.Map<IEnumerable<ApplicationCourseCampusDto>>(applicationCourseCampuses);

            #region CreateLinksForApplicationCourseCampuses => Supporting HATEOAS (Base and Wrapper Class Approach)
            results = results.Select(acc =>
            {
                acc = CreateLinksForApplicationCourseCampus(acc);
                return acc;
            });

            var wrapper = new LinkedCollectionResourceWrapperDto<ApplicationCourseCampusDto>(results);
            #endregion

            //return Ok(results);
            return Ok(CreateLinksForApplicationCourseCampuses(wrapper));
        }

        [HttpGet("{id}", Name = "GetApplicationCourseCampus")]
        public IActionResult GetApplicationCourseCampus(int applicationId, int id)
        {
            if (!_applicationInfoRepository.ApplicationExists(applicationId))
            {
                return NotFound();
            }
            var applicationCourseCampus = _applicationInfoRepository.GetCourseCampusForApplication(applicationId, id);
            if (applicationCourseCampus == null)
            {
                return NotFound();
            }
            var results = Mapper.Map<ApplicationCourseCampusDto>(applicationCourseCampus);

            #region CreateLinksForApplicationCourseCampus => Supporting HATEOAS (Base and Wrapper Class Approach)
            //return Ok(results);
            return Ok(CreateLinksForApplicationCourseCampus(results));
            #endregion
        }

        /*
            http://localhost:63292/api/applications/58/courseCampusesForApplication         //    58 => applicationId
            POST =>
                [
	                {
		                "CourseCampusId": 618,
		                "NOtes" : "618 CourseCampus Added",
		                "IsPrimaryLocation" : true
	                },
	                {
		                "CourseCampusId": 619,
		                "NOtes" : null,
		                "IsPrimaryLocation" : true
	                }
                ]
         */
        //[HttpPost("{applicationId}/courseCampusesForApplication")]
        [HttpPost(Name = "CreateCourseCampusForApplication")]
        public IActionResult CreateCourseCampusForApplication(int applicationId,
            [FromBody] IEnumerable<CourseCampusForApplicationCreationDto> courseCampusesForApplication)
        {
            if (courseCampusesForApplication == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                //return BadRequest();
                // return 422 
                // Custom Error Handler => returns the Key Value pair Error of the specific property 
                /*
                 Use Case 1 by following CourseCampusForApplicationManipulationDto Validation Rules:-
                 Request Body =>
                    [
	                    {
		                    "CourseCampusId": 0,
		                    "IsPrimaryLocation" : true
	                    },
	                    {
		                    "CourseCampusId": 619,
		                    "IsPrimaryLocation" : true
	                    }
                    ]
                Response =>
                    {
                        "[0].CourseCampusId": [
                            "Please provide valid value for CourseCampusId between 1 to 1000000"
                        ]
                    }
                 */
                return new UnprocessableEntityObjectResult(ModelState);
            }
            if (!_applicationInfoRepository.ApplicationExists(applicationId))
            {
                return NotFound();
            }

            foreach (CourseCampusForApplicationCreationDto courseCampusForApplication in courseCampusesForApplication)
            {
                var acc = Mapper.Map<Entities.ApplicationCourseCampus>(courseCampusForApplication);
                if (!_applicationInfoRepository.CourseCampusExists(acc.CourseCampusId))
                {
                    return NotFound();
                }
                else
                {
                    _applicationInfoRepository.AddCourseCampusForApplication(
                        applicationId,
                        acc
                        //cc.CourseCampus
                        //_applicationInfoRepository.GetCourseCampus(cc.CourseCampusId)
                        );
                }
            }

            if (!_applicationInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return Ok();
        }

        /*
                {
		            "IsPrimaryLocation" : true,
		            "Notes": "ThiLorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc, quis gravida magna mi a libero. Fusce vulputate eleifend sapien. Vestibulum purus quam, scelerisque ut, mollis sed, nonummy id, metus. Nullam accumsan lorem in dui. Cras ultricies mi eu turpis hendrerit fringilla. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; In ac dui quis mi consectetuer lacinia. Nam pretium turpis et arcu. Duis arcu tortor, suscipit eget, imperdiet nec, imperdiet iaculis, ipsum. Sed aliquam ultrices mauris. Integer ante arcu, accumsan a, consectetuer eget, posuere ut, mauris. Praesent adipiscing. Phasellus ullamcorper ipsum rutrum nunc. Nunc nonummy metus. Vestibulum volutpat pretium libero. Cras id dui. Aenean uts is a new note",
                    "CourseCampusId": 0
                }
            http://localhost:63292/api/applications/111/courseCampusesForApplication/102
                                                ApplicationId                       ApplicationCourseCampusId => Id (Primary Key of the ApplicationCourseCampus table)
         */
        [HttpPut("{id}", Name = "UpdateCourseCampusesForApplication")]
        public IActionResult UpdateCourseCampusesForApplication(int applicationId, int id,
                    [FromBody] CourseCampusForApplicationUpdateDto courseCampusForApplication)
        {
            if (courseCampusForApplication == null)
            {
                return BadRequest();
            }
            if (courseCampusForApplication.Notes == "Not Sure")
            {
                ModelState.AddModelError(nameof(CourseCampusForApplicationUpdateDto),
                    "You need to be sure of the campus you are updating");
            }
            if (!ModelState.IsValid)
            {
                //return BadRequest();
                // return 422 
                // Custom Error Handler => returns the Key Value pair Error of the specific property 
                /*
                 Use Case 1 by following CourseCampusForApplicationManipulationDto Validation Rules:-
                 Request Body =>
                    {
		                "IsPrimaryLocation" : true,
		                "Notes": "ThiLorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc, quis gravida magna mi a libero. Fusce vulputate eleifend sapien. Vestibulum purus quam, scelerisque ut, mollis sed, nonummy id, metus. Nullam accumsan lorem in dui. Cras ultricies mi eu turpis hendrerit fringilla. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; In ac dui quis mi consectetuer lacinia. Nam pretium turpis et arcu. Duis arcu tortor, suscipit eget, imperdiet nec, imperdiet iaculis, ipsum. Sed aliquam ultrices mauris. Integer ante arcu, accumsan a, consectetuer eget, posuere ut, mauris. Praesent adipiscing. Phasellus ullamcorper ipsum rutrum nunc. Nunc nonummy metus. Vestibulum volutpat pretium libero. Cras id dui. Aenean uts is a new note",
                        "CourseCampusId": 0
                    }
                Response =>
                    {
                        "Notes": [
                            "Notes shouldn't have more than 250 characters."
                        ],
                        "CourseCampusId": [
                            "Please provide valid value for CourseCampusId between 1 to 1000000"
                        ]
                    }

                Use Case 2 by following CourseCampusForApplicationUpdateDto Validation Rules:-
                 Request Body =>
                    {
		                "IsPrimaryLocation" : true,
                        "CourseCampusId": 0
                    }
                Response =>
                    {
                        "Notes": [
                            "You should fill out a note."
                        ],
                        "CourseCampusId": [
                            "Please provide valid value for CourseCampusId between 1 to 1000000"
                        ]
                    }
                Use Case 3 by adding Validation Rules in this Controller Action:-
                 Request Body =>
	                {
		                "CourseCampusId": 630,
		                "Notes" : "Not Sure",
		                "IsPrimaryLocation" : true
	                }
                Response =>
                    {
                        "CourseCampusForApplicationUpdateDto": [
                            "You need to be sure of the campus you are updating"
                        ]
                    }
                */
                return new UnprocessableEntityObjectResult(ModelState);
            }
            if (!_applicationInfoRepository.ApplicationExists(applicationId))
            {
                return NotFound();
            }
            var courseCampusForApplicationFromRepo = _applicationInfoRepository.GetCourseCampusForApplication(applicationId, id);
            if (courseCampusForApplicationFromRepo == null)
            {
                return NotFound();
            }
            //          SOURCE  =>  DESTINATION
            Mapper.Map(courseCampusForApplication, courseCampusForApplicationFromRepo);

            _applicationInfoRepository.UpdateCourseCampusForApplication(courseCampusForApplicationFromRepo);
            if (!_applicationInfoRepository.Save())
            {
                throw new Exception("Updating Application Course Campus for Application failed on save.");
            }
            //return NoContent();
            return CreatedAtRoute("GetApplicationCourseCampus",
                new { applicationId = applicationId, id = courseCampusForApplicationFromRepo.CourseCampusId });//, courseCampusForApplicationFromRepo);
                //  CreateLinksForBooks => Supporting HATEOAS (Base and Wrapper Class Approach)
                //CreateLinksForBook(bookToReturn));
        }

        /*
            [
                {
                  "op": "replace",
                  "path": "/Notes",
                  "value": "Not Sureaa"
                },
                {
                  "op": "replace",
                  "path": "/IsPrimaryLocation",
                  "value": "false"
                }
            ]         
             */
        [HttpPatch("{id}", Name = "PartiallyUpdateCourseCampusForApplication")]
        public IActionResult PartiallyUpdateCourseCampusForApplication(int applicationId, int id,
            [FromBody] JsonPatchDocument<CourseCampusForApplicationUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            if (!_applicationInfoRepository.ApplicationExists(applicationId))
            {
                return NotFound();
            }

            var courseCampusForApplicationFromRepo = _applicationInfoRepository.GetCourseCampusForApplication(applicationId, id);
            if (courseCampusForApplicationFromRepo == null)
            {
                return NotFound();
            }
            var courseCampusForApplicationToPatch = Mapper.Map<CourseCampusForApplicationUpdateDto>(courseCampusForApplicationFromRepo);
            patchDoc.ApplyTo(courseCampusForApplicationToPatch, ModelState);

            if (courseCampusForApplicationToPatch.Notes == "Not Sure")
            {
                ModelState.AddModelError(nameof(CourseCampusForApplicationUpdateDto),
                    "You need to be sure of the campus you are updating");
            }

            //  Trigger Validation of courseCampusForApplicationToPatch, without TryValidateModel !ModelState.IsValid will work for patchDoc which is JsonPatchDocument but not bookToPatch which is CourseCampusForApplicationUpdateDto
            // All this because input content is of type JsonPatchDocument but not of type CourseCampusForApplicationUpdateDto
            TryValidateModel(courseCampusForApplicationToPatch);
            if (!ModelState.IsValid)
            {
                return new UnprocessableEntityObjectResult(ModelState);
            }

            //          SOURCE  =>  DESTINATION
            Mapper.Map(courseCampusForApplicationToPatch, courseCampusForApplicationFromRepo);

            _applicationInfoRepository.UpdateCourseCampusForApplication(courseCampusForApplicationFromRepo);
            if (!_applicationInfoRepository.Save())
            {
                throw new Exception("Patching Application Course Campus for Application failed on save.");
            }

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteApplicationCourseCampus")]
        public IActionResult DeleteApplicationCourseCampus(int applicationId, int id)
        {
            if (!_applicationInfoRepository.ApplicationExists(applicationId))
            {
                return NotFound();
            }

            var courseCampusForApplicationFromRepo = _applicationInfoRepository.GetCourseCampusForApplication(applicationId, id);
            if (courseCampusForApplicationFromRepo == null)
            {
                return NotFound();
            }

            _applicationInfoRepository.DeleteApplicationCourseCampusForApplication(courseCampusForApplicationFromRepo);
            if (!_applicationInfoRepository.Save())
            {
                throw new Exception("Deleting Application Course Campus for Application failed on save.");
            }
            return NoContent();
        }

        #region Supporting HATEOAS (Static Properties Approach)
        private ApplicationCourseCampusDto CreateLinksForApplicationCourseCampus(ApplicationCourseCampusDto applicationCourseCampus)
        {
            applicationCourseCampus.Links.Add(new LinkDto(_urlHelper.Link("GetApplicationCourseCampus",
                new { id = applicationCourseCampus.Id }),
                "self",
                "GET"));

            applicationCourseCampus.Links.Add(
                new LinkDto(_urlHelper.Link("DeleteApplicationCourseCampus",
                new { id = applicationCourseCampus.Id }),
                "delete_applicationCourseCampus",
                "DELETE"));

            applicationCourseCampus.Links.Add(
                new LinkDto(_urlHelper.Link("UpdateCourseCampusesForApplication",
                new { id = applicationCourseCampus.Id }),
                "update_applicationCourseCampus",
                "PUT"));

            applicationCourseCampus.Links.Add(
                new LinkDto(_urlHelper.Link("PartiallyUpdateCourseCampusForApplication",
                new { id = applicationCourseCampus.Id }),
                "partially_update_applicationCourseCampus",
                "PATCH"));

            return applicationCourseCampus;
        }

        private LinkedCollectionResourceWrapperDto<ApplicationCourseCampusDto> CreateLinksForApplicationCourseCampuses(
            LinkedCollectionResourceWrapperDto<ApplicationCourseCampusDto> applicationCourseCampusWrapper)
        {
            // link to self
            applicationCourseCampusWrapper.Links.Add(
                new LinkDto(_urlHelper.Link("GetApplicationCourseCampuses", new { }),
                "self",
                "GET"));

            return applicationCourseCampusWrapper;
        }
        #endregion

    }
}
