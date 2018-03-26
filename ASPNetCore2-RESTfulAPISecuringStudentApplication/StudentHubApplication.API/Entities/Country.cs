using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Entities
{
    public class Country
    {
        public Country()
        {
            ApplicationCountryOfResidence = new HashSet<Application>();
            ApplicationCountryOfBirth = new HashSet<Application>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Code { get; set; }

        public ICollection<Application> ApplicationCountryOfResidence { get; set; }
        public ICollection<Application> ApplicationCountryOfBirth { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
