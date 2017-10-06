using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SamuraiAppCore.Data;
using SamuraiAppCore.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SamuraiWebApi.Controllers
{
    [Route("api/[controller]")]
    public class SamuraiController : Controller
    {
        private DisconnectedData _repo;

        public SamuraiController(DisconnectedData repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<int, string>> Get()
        {
            var list = _repo.GetSamuraiReferenceList();
            return list;
        }

        [HttpGet("{id}")]
        public Samurai Get(int id)
        {
            return _repo.LoadSamuraiGraph(id);
        }

        //example raw json: {"name":"Julie","secretIdentity":{"realName":"Julia"}}
        [HttpPost]
        public void Post([FromBody] Samurai value)
        {
            _repo.SaveSamuraiGraph(value);
        }

        //example raw json:{"id":"3","name":"Julietta","IsDirty":"true","secretIdentity":{"id":"3","realName":"Julia Lerman","IsDirty":"true"}}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Samurai value)
        {
            _repo.SaveSamuraiGraph(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repo.DeleteSamuraiGraph(id);
        }
    }
}
