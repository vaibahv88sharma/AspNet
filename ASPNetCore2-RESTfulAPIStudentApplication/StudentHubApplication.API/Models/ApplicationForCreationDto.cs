using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Models
{
    public class ApplicationForCreationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public int CountryOfResidenceId { get; set; }
        public int CountryOfBirthId { get; set; }
        //public ICollection<ApplicationQualificationDto> ApplicationQualifications { get; set; }
        public ICollection<QualificationForApplicationCreationDto> ApplicationQualifications { get; set; }
        public ICollection<CourseCampusForApplicationCreationDto> ApplicationCourseCampuses { get; set; }//ApplicationCourseCampusDto
    }
}
