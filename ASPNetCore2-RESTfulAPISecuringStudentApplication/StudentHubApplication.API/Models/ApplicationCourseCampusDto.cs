using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Models
{
    public class ApplicationCourseCampusDto : LinkedResourceBaseDto
    {
        public int Id { get; set; }
        public bool IsPrimaryLocation { get; set; }
        public string Notes { get; set; }
        public CourseCampusDto CourseCampus { get; set; }
    }
}
