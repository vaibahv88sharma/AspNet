using StudentHubApplication.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Models
{
    public class CourseCampusDto
    {
        public int Id { get; set; }
        public CourseDto Course { get; set; }
        public CampusDto Campus { get; set; }
    }
}
