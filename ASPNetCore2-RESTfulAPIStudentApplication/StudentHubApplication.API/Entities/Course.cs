using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Entities
{
    public class Course
    {

        public Course()
        {
            CourseCampus = new HashSet<CourseCampus>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string CourseCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public bool Active { get; set; }

        public ICollection<CourseCampus> CourseCampus { get; set; }
    }
}
