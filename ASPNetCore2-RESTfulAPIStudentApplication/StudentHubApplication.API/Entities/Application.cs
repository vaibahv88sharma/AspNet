using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Entities
{
    public class Application
    {

        public Application()
        {
            ApplicationCourseCampuses = new HashSet<ApplicationCourseCampus>();
            ApplicationQualifications = new HashSet<ApplicationQualification>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public DateTimeOffset DateOfBirth { get; set; }

        //public DateTimeOffset? DateOfDeath { get; set; }

        [Required]
        [MaxLength(100)]
        public string Gender { get; set; }

        [ForeignKey("CountryOfResidenceId")]
        public Country CountryOfResidence { get; set; }
        public int CountryOfResidenceId { get; set; }

        [ForeignKey("CountryOfBirthId")]
        public Country CountryOfBirth { get; set; }
        public int CountryOfBirthId { get; set; }

        public ICollection<ApplicationQualification> ApplicationQualifications { get; set; }
        public ICollection<ApplicationCourseCampus> ApplicationCourseCampuses { get; set; }

    }
}
