using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Models
{
    public class ApplicationForCreationDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTimeOffset DateOfBirth { get; set; }

        public int CountryOfResidenceId { get; set; }
        public int CountryOfBirthId { get; set; }

        [Required]
        public string Gender { get; set; }
        //public ICollection<ApplicationQualificationDto> ApplicationQualifications { get; set; }
        public ICollection<QualificationForApplicationCreationDto> ApplicationQualifications { get; set; }
        public ICollection<CourseCampusForApplicationCreationDto> ApplicationCourseCampuses { get; set; }//ApplicationCourseCampusDto
    }
}
