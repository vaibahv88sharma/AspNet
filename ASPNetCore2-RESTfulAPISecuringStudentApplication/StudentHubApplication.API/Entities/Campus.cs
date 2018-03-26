using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Entities
{
    public class Campus
    {

        public Campus()
        {
            CourseCampus = new HashSet<CourseCampus>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string CampusCode { get; set; }

        public ICollection<CourseCampus> CourseCampus { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
