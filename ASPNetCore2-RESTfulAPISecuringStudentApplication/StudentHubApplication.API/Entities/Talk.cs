using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Entities
{
    public class Talk
    {
        [Key]
        public int Id { get; set; }
        public string Abstract { get; set; }
        public string Category { get; set; }
        public string Level { get; set; }
        public string Prerequisites { get; set; }
        public string Room { get; set; }
        public DateTime StartingTime { get; set; }
        public string Title { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [ForeignKey("SpeakerId")]
        public Speaker Speaker { get; set; }
        public int? SpeakerId { get; set; }
    }
}
