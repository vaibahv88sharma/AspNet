using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Models
{
    public class ApplicationQualificationDto
    {
        //public int Id { get; set; }
        //public bool IsPrimaryQualification { get; set; }
        //public string Notes { get; set; }
        //public DateTime? CompletionDate { get; set; }
        public QualificationDto Qualification { get; set; }
    }
}
