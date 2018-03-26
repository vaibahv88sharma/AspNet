using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Models
{
    public abstract class CourseCampusForApplicationManipulationDto
    {
        //public bool? IsPrimaryLocation { get; set; }
        [Required(ErrorMessage = "Please enter IsPrimaryLocation")]
        [Range(0, 1, ErrorMessage = "Please provide valid value for IsPrimaryLocation")]
        public bool IsPrimaryLocation { get; set; }

        [MaxLength(250, ErrorMessage = "Notes shouldn't have more than 250 characters.")]
        public virtual string Notes { get; set; }

        [Required(ErrorMessage = "You should fill out CourseCampusId.")]
        [Range(1, 1000000, ErrorMessage = "Please provide valid value for CourseCampusId between 1 to 1000000")]
        public int CourseCampusId { get; set; }
    }
}
