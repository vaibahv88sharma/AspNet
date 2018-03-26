using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Entities
{
    public class Location
    {
        public Location()
        {
            Camps = new HashSet<Camp>();
        }

        [Key]
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string CityTown { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string StateProvince { get; set; }

        public ICollection<Camp> Camps { get; set; }
    }
}
