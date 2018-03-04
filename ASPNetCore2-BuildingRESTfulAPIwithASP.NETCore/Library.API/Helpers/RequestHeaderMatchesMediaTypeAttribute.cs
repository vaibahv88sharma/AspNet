using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;

namespace Library.API.Helpers
{
                                                        // AllowMultiple => Allows Multiple RequestHeaderMatchesMediaType
                                                        // to be called on the Controller Action Methods
                                                        // RequestHeaderMatchesMediaType => Allows multiple headers e.g.
                                                        //  [RequestHeaderMatchesMediaType("Content-Type", new[] { "application/vnd.marvin.authorwithdateofdeath.full+json", "application/vnd.marvin.authorwithdateofdeath.full+xml" })]
                                                        //  [RequestHeaderMatchesMediaType("Accept", new[] { "..." })]
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    public class RequestHeaderMatchesMediaTypeAttribute : Attribute, IActionConstraint
    {
        private readonly string[] _mediaTypes;
        private readonly string _requestHeaderToMatch;

        public RequestHeaderMatchesMediaTypeAttribute(string requestHeaderToMatch,
            string[] mediaTypes)
        {
            _requestHeaderToMatch = requestHeaderToMatch;
            _mediaTypes = mediaTypes;
        }

        public int Order
        {
            get
            {
                return 0;
            }
        }

        public bool Accept(ActionConstraintContext context)
        {
            var requestHeaders = context.RouteContext.HttpContext.Request.Headers;

            if (!requestHeaders.ContainsKey(_requestHeaderToMatch))
            {
                return false;
            }

            // if one of the media types matches, return true
            foreach (var mediaType in _mediaTypes)
            {
                var mediaTypeMatches = string.Equals(requestHeaders[_requestHeaderToMatch].ToString(),
                    mediaType, StringComparison.OrdinalIgnoreCase);

                if (mediaTypeMatches)
                {
                    return true;
                }
            }

            return false;
        }
    }
}