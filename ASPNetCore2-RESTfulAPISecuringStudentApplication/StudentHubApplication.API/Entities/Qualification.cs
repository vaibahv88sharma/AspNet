using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Entities
{
    public class Qualification
    {
        public Qualification()
        {
            ApplicationQualifications = new HashSet<ApplicationQualification>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<ApplicationQualification> ApplicationQualifications { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
