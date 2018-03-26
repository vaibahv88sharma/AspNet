using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Entities
{
    public class Camp
    {
        public Camp()
        {
            Speakers =new HashSet<Speaker>();
        }
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; } = DateTime.MinValue;
        public int Length { get; set; }
        public string Name { get; set; }
        public string Moniker { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        public int? LocationId { get; set; }

        public ICollection<Speaker> Speakers { get; set; }

        [ForeignKey("ApplicationId")]
        public Application Application { get; set; }
        public int? ApplicationId { get; set; }
    }
}
