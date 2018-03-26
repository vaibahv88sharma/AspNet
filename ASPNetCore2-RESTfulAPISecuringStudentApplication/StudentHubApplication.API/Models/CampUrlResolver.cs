using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentHubApplication.API.Controllers;
using StudentHubApplication.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHubApplication.API.Models
{
    public class CampUrlResolver : IValueResolver<Camp, CampModel, string>
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ILogger<CampUrlResolver> _logger;

        public CampUrlResolver(IHttpContextAccessor httpContextAccessor
            , ILogger<CampUrlResolver> logger
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _logger.LogInformation("Hi from CampUrlResolver");
        }

        public string Resolve(Camp source, CampModel destination, string destMember, ResolutionContext context)
        {
            var url = (IUrlHelper)_httpContextAccessor.HttpContext.Items[BaseController.URLHELPER];
            return url.Link("CampGet", new { id = source.Id });
        }
    }
}
