using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Models
{   //  Supporting HATEOAS (Base and Wrapper Class Approach)
    public abstract class LinkedResourceBaseDto
    {
        public List<LinkDto> Links { get; set; }
        = new List<LinkDto>();
    }
}
