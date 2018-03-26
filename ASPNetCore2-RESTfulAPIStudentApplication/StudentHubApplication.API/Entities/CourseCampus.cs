using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Entities
{
    public class CourseCampus
    {

        public CourseCampus()
        {
            ApplicationCourseCampuses = new HashSet<ApplicationCourseCampus>();
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        public int CourseId { get; set; }

        [ForeignKey("CampusId")]
        public Campus Campus { get; set; }
        public int CampusId { get; set; }

        public ICollection<ApplicationCourseCampus> ApplicationCourseCampuses { get; set; }

    }
}
