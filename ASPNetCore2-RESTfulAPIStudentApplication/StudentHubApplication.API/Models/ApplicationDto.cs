using StudentHubApplication.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Models
{
    public class ApplicationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int CountryOfResidenceId { get; set; }
        public int CountryOfBirthId { get; set; }

        public CountryDto CountryOfResidence { get; set; }
        public CountryDto CountryOfBirth { get; set; }

    }
}
