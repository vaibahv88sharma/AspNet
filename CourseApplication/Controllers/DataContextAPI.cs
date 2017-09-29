﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseApplication.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CourseApplication.Controllers
{
    [Produces("application/json")]
    [Route("api/DataContextAPI")]
    public class DataContextAPI : Controller
    {
        private readonly DataContext _context;

        public DataContextAPI(DataContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        [Route("ResultingData")]
        public IEnumerable<AttendanceResulting> GetDataMaster()
        {
            return _context.DataMaster;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
