using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Entities
{
    public class ApplicationQualification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public bool IsPrimaryQualification { get; set; }

        [MaxLength(250)]
        public string Notes { get; set; }

        public DateTime? CompletionDate { get; set; }

        [ForeignKey("ApplicationId")]
        public Application Application { get; set; }
        public int ApplicationId { get; set; }

        [ForeignKey("QualificationId")]
        public Qualification Qualification { get; set; }
        public int QualificationId { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
