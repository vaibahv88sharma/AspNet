using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Models
{
    public class CourseCampusForApplicationUpdateDto : CourseCampusForApplicationManipulationDto
    {
        [Required(ErrorMessage = "You should fill out a note.")]
        public override string Notes
        {
            get
            {
                return base.Notes;
            }

            set
            {
                base.Notes = value;
            }
        }
    }
}
