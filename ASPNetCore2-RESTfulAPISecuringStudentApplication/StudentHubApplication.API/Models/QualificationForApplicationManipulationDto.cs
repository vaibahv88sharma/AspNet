using StudentHubApplication.API.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Models
{
    public abstract class QualificationForApplicationManipulationDto
    {
        public bool IsPrimaryQualification { get; set; }

        [MaxLength(250, ErrorMessage = "Notes shouldn't have more than 250 characters.")]

        public virtual string Notes { get; set; }

        public DateTime CompletionDate { get; set; }

        [Required(ErrorMessage = "You should fill out QualificationId.")]
        [Range(1, 1000000, ErrorMessage = "Please provide valid value for QualificationId between 1 to 1000000")]
        public int QualificationId { get; set; }

        public Qualification Qualification { get; set; }
    }
}
