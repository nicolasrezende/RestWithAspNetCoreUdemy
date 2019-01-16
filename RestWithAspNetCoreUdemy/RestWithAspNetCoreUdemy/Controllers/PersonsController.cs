using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNetCoreUdemy.Models;
using RestWithAspNetCoreUdemy.Services;

namespace RestWithAspNetCoreUdemy.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        // GET api/persons
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_personService.FindAll());
        }

        // GET api/persons/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            Person person = _personService.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        // POST api/persons
        [HttpPost]
        public ActionResult Post([FromBody] Person person)
        {
            if (person == null) BadRequest();
            return Ok(_personService.Create(person));
        }

        // PUT api/persons/5
        [HttpPut()]
        public ActionResult Put([FromBody] Person person)
        {
            if (person == null) BadRequest();
            return Ok(_personService.Update(person));
        }

        // DELETE api/persons/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _personService.Delete(id);
            return NoContent();
        }
    }
}
