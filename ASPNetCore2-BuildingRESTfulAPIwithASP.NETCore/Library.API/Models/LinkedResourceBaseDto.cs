using System.Collections.Generic;

namespace Library.API.Models
{   //  Supporting HATEOAS (Base and Wrapper Class Approach)
    public abstract class LinkedResourceBaseDto
    {
        public List<LinkDto> Links { get; set; }
        = new List<LinkDto>();
    }
}
