using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Entities
{
    public class ApplicationCourseCampus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public bool IsPrimaryLocation { get; set; }

        [MaxLength(250)]
        public string Notes { get; set; }

        [ForeignKey("ApplicationId")]
        public Application Application { get; set; }
        public int ApplicationId { get; set; }

        [ForeignKey("CourseCampusId")]
        public CourseCampus CourseCampus { get; set; }
        public int CourseCampusId { get; set; }
    }
}