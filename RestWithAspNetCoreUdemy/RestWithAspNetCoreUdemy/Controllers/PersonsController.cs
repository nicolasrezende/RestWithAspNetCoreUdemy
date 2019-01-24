using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNetCoreUdemy.Bussines;
using RestWithAspNetCoreUdemy.Data.VO;
using System.Collections.Generic;

namespace RestWithAspNetCoreUdemy.Repository
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonBussines _personBussines;

        public PersonsController(IPersonBussines personBussines)
        {
            _personBussines = personBussines;
        }

        // GET api/persons
        [HttpGet]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(List<PersonVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult Get()
        {
            return Ok(_personBussines.FindAll());
        }

        // GET api/persons/5
        [HttpGet("{id}")]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]       
        public ActionResult Get(int id)
        {
            PersonVO person = _personBussines.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        // POST api/persons
        [HttpPost]
        [Authorize("Bearer")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult Post([FromBody] PersonVO person)
        {
            if (person == null) BadRequest();
            return Ok(_personBussines.Create(person));
        }

        // PUT api/persons/5
        [HttpPut()]
        [Authorize("Bearer")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult Put([FromBody] PersonVO person)
        {
            if (person == null) BadRequest();
            var personUpdate = _personBussines.Update(person);
            if (personUpdate == null) BadRequest();
            return Ok(personUpdate);
        }

        // DELETE api/persons/5
        [HttpDelete("{id}")]
        [Authorize("Bearer")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult Delete(int id)
        {
            _personBussines.Delete(id);
            return NoContent();
        }
    }
}
