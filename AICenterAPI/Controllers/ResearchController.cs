﻿using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AICenterAPI.Controllers
{
    [Route("api/research")]
    [Route("api/{lang:alpha?}/research")]
    [ApiController]
    public class ResearchController : ControllerBase
    {
        // GET: api/<ResearchController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ResearchController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ResearchController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ResearchController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ResearchController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
