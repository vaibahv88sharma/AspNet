﻿namespace Library.API.Models
{   //  Supporting HATEOAS (Base and Wrapper Class Approach)
    public class LinkDto
    {
        public string Href { get; private set; }
        public string Rel { get; private set; }
        public string Method { get; private set; }

        public LinkDto(string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }
}